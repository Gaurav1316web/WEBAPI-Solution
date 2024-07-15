
Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls

Public Class clsSNShipmentHead
#Region "Variables"
    Public Electronic_Ref_No As String = Nothing
    Public EWayBillDate As DateTime? = Nothing
    Public EWayBillNo As String = Nothing
    Public is_taxable As Integer = 0
    Public Is_Delivered As Integer = 0
    Public Podate As DateTime? = Nothing
    Public Form_38_No As String = Nothing
    Public Cust_PO_No As String = Nothing
    Public Price_Group_Code As String = Nothing
    Public Invoice_No As String = Nothing
    Public Invoice_Type As String = Nothing
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime? = Nothing
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = Nothing  'Not a table field
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing
    Public Ref_No As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
    Public Bill_To_Location As String = Nothing
    Public BillToLocationName As String = Nothing 'Not a table field
    Public Ship_To_Location As String = Nothing
    Public ShipToLocationName As String = Nothing 'Not a table field
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = 0
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = 0
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = 0
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = 0
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = 0
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Amt As Double = 0
    Public Discount_Base As Double = 0
    Public Discount_Amt As Double = 0
    Public Amount_Less_Discount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Total_Amt As Double = 0
    Public Comments As String = Nothing
    Public Comp_Code As String = Nothing
    Public Terms_Code As String = Nothing
    Public TermsName As String = Nothing
    Public Due_Date As DateTime? = Nothing
    Public Posting_Date As DateTime? = Nothing
    Public Carrier As String = Nothing
    Public transport_id As String = Nothing
    Public VehicleNo As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public GRNo As String = Nothing
    Public GENo As String = Nothing
    Public GEDate As Date? = Nothing
    Public Add_Charge_Code1 As String = Nothing
    Public Add_Charge_Name1 As String = Nothing
    Public Add_Charge_Amt1 As Double = 0
    Public Add_Charge_Code2 As String = Nothing
    Public Add_Charge_Name2 As String = Nothing
    Public Add_Charge_Amt2 As Double = 0
    Public Add_Charge_Code3 As String = Nothing
    Public Add_Charge_Name3 As String = Nothing
    Public Add_Charge_Amt3 As Double = 0
    Public Add_Charge_Code4 As String = Nothing
    Public Add_Charge_Name4 As String = Nothing
    Public Add_Charge_Amt4 As Double = 0
    Public Add_Charge_Code5 As String = Nothing
    Public Add_Charge_Name5 As String = Nothing
    Public Add_Charge_Amt5 As Double = 0
    Public Add_Charge_Code6 As String = Nothing
    Public Add_Charge_Name6 As String = Nothing
    Public Add_Charge_Amt6 As Double = 0
    Public Add_Charge_Code7 As String = Nothing
    Public Add_Charge_Name7 As String = Nothing
    Public Add_Charge_Amt7 As Double = 0
    Public Add_Charge_Code8 As String = Nothing
    Public Add_Charge_Name8 As String = Nothing
    Public Add_Charge_Amt8 As Double = 0
    Public Add_Charge_Code9 As String = Nothing
    Public Add_Charge_Name9 As String = Nothing
    Public Add_Charge_Amt9 As Double = 0
    Public Add_Charge_Code10 As String = Nothing
    Public Add_Charge_Name10 As String = Nothing
    Public Add_Charge_Amt10 As Double = 0
    Public Total_Add_Charge As Double = 0
    Public Dept As String = Nothing
    Public Dept_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public Challan_No As String = Nothing
    Public Challan_Date As DateTime? = Nothing
    Public Inv_No As String = Nothing
    Public Inv_Date As DateTime? = Nothing
    Public Against_Sales_Order As String = Nothing
    Public Is_Internal As Boolean = False
    Public Tax_Calculation_Type As EnumTaxCalucationType

    Public Is_Create_Auto_Invoice As Boolean = False
    Public Sale_Invoice_No As String = Nothing
    Public Is_Create_Auto_Receipt As Boolean = False
    Public Against_POS As String = Nothing

    Public Salesman_Code As String = Nothing
    Public Salesman_Name As String = Nothing
    Public Arr As List(Of clsSNShipmentDetail) = Nothing
    Public ArrWeighmentList As ArrayList = Nothing
    Public ArrDCSItem As List(Of clsSNShipmentDCSItemDetail) = Nothing
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public PROJECT_ID As String = Nothing

    Public Price_Code As String = Nothing
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public HeadDisc_Per As Double = 0
    Public HeadDisc_Amt As Double = 0
    Public TotCashDiscAmt As Double = 0
    Public HeadDisc_PerAmt As Double = 0
    Public Mannual_Invoice_No As Integer = 0
    Public InvoiceManualNowithPrefix As String = Nothing

    'Public Credit_Limit As Double = 0
    'Public Advance_Security As Double = 0
    'Public Revese_Adv_Security As Double = 0
    'Public AR_Credit_Security As Double = 0
    ' Public Pending_Posted_DO As Double = 0
    'Public UnPostedShipment As Double = 0
    'Public Ledger_Outstansing As Double = 0
    'Public Refund_Security As Double = 0
    'Public Reverse_Refund_Sec As Double = 0
    Public Total_Outstanding As Double = 0

    Public Weighment_Code As String
    Public Beejak_No As String
    Public LR_GR_NO As String = Nothing
    Public LR_GR_Date As Date? = Nothing
    Public SHIP_TO_DELIVERY_AT As String = Nothing
    Public Order_Qty As Double = 0
    Public count As Integer = 0

#End Region

    Public Function SaveData(ByVal obj As clsSNShipmentHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetCustomerBaseQry(ByVal ItemWisecheck As Boolean, ByVal IsSecurity As Boolean, ByVal SecurityType As String, ByVal Docwise As Boolean, ByVal CurrencyType As String, ByVal strCustomer As String, ByVal isOpening As Boolean, ByVal strfromdate As String, ByVal strtodate As String, Optional ByVal IsCSA As Boolean = False, Optional ByVal IsMISDebtorReport As Boolean = False, Optional ByVal IsShowApplyDocument As Boolean = False, Optional ByVal trans As SqlTransaction = Nothing) As String
        Return GetCustomerBaseQry(ItemWisecheck, IsSecurity, SecurityType, Docwise, CurrencyType, strCustomer, isOpening, strfromdate, strtodate, IsCSA, IsMISDebtorReport, IsShowApplyDocument, trans, True, Nothing)
    End Function

    Public Shared Function GetCustomerBaseQry(ByVal ItemWisecheck As Boolean, ByVal IsSecurity As Boolean, ByVal SecurityType As String, ByVal Docwise As Boolean, ByVal CurrencyType As String, ByVal strCustomer As String, ByVal isOpening As Boolean, ByVal strfromdate As String, ByVal strtodate As String, ByVal IsCSA As Boolean, ByVal IsMISDebtorReport As Boolean, ByVal IsShowApplyDocument As Boolean, ByVal trans As SqlTransaction, ByVal IsIncludecardIndent As Boolean) As String
        Return GetCustomerBaseQry(ItemWisecheck, IsSecurity, SecurityType, Docwise, CurrencyType, strCustomer, isOpening, strfromdate, strtodate, IsCSA, IsMISDebtorReport, IsShowApplyDocument, trans, IsIncludecardIndent, "")
    End Function
    Public Shared Function GetCustomerBaseQry(ByVal ItemWisecheck As Boolean, ByVal IsSecurity As Boolean, ByVal SecurityType As String, ByVal Docwise As Boolean, ByVal CurrencyType As String, ByVal strCustomer As String, ByVal isOpening As Boolean, ByVal strfromdate As String, ByVal strtodate As String, ByVal IsCSA As Boolean, ByVal IsMISDebtorReport As Boolean, ByVal IsShowApplyDocument As Boolean, ByVal trans As SqlTransaction, ByVal IsIncludecardIndent As Boolean, ByVal DocNo As String) As String
        Dim strtempBaseQryforopening As String = ""
        Dim strtempBaseQry As String = ""
        strtempBaseQryforopening = GetCustomerBaseQryForOpening(ItemWisecheck, IsSecurity, SecurityType, Docwise, CurrencyType, strCustomer, isOpening, strfromdate, strtodate, IsCSA, IsShowApplyDocument, trans, IsIncludecardIndent, DocNo)
        Dim strExcludeEXcforApplyDocumnets As String = " and InnQuery.DocNo not in ( Select Receipt_No  from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Receipt_Type ='A' and   TSPL_RECEIPT_HEADER.Posted='Y' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT >0)   "
        If isOpening = True Then
            strExcludeEXcforApplyDocumnets += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
        Else
            strExcludeEXcforApplyDocumnets += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + strtodate + "' " + Environment.NewLine
        End If
        If clsCommon.myLen(strCustomer) > 0 Then
            strExcludeEXcforApplyDocumnets += " and TSPL_RECEIPT_HEADER.Cust_Code in (" & strCustomer & ")"
        End If
        strExcludeEXcforApplyDocumnets += Environment.NewLine & " Union All" & Environment.NewLine &
        " Select Reverse_Code  from TSPL_BANK_REVERSE where Document_No in ( Select Receipt_No  from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Receipt_Type ='A' and   TSPL_RECEIPT_HEADER.Posted='Y' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT >0)   "
        If isOpening = True Then
            strExcludeEXcforApplyDocumnets += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
        Else
            strExcludeEXcforApplyDocumnets += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + strtodate + "' " + Environment.NewLine
        End If
        If clsCommon.myLen(strCustomer) > 0 Then
            strExcludeEXcforApplyDocumnets += " and TSPL_RECEIPT_HEADER.Cust_Code in (" & strCustomer & ")"
        End If

        strExcludeEXcforApplyDocumnets += "  ) ) "
        Dim strExcludeCardIndentAdvanceDoc As String = Nothing
        If IsIncludecardIndent = False Then
            strExcludeCardIndentAdvanceDoc = " and InnQuery.DocNo not in ( " & Environment.NewLine &
        " Select tspl_receipt_header.receipt_no  from tspl_receipt_header where tspl_receipt_header.receipt_no in (select distinct Against_Receipt_No  from TSPL_BOOKING_PAYMENT_MODE_DETAIL ) and tspl_receipt_header.Receipt_Type ='P' and tspl_receipt_header.Posted ='Y' and isnull(tspl_receipt_header.IsChkReverse,'')='N'  "
            If isOpening = True Then
                strExcludeCardIndentAdvanceDoc += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
            Else
                strExcludeCardIndentAdvanceDoc += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + strtodate + "' " + Environment.NewLine
            End If
            If clsCommon.myLen(strCustomer) > 0 Then
                strExcludeCardIndentAdvanceDoc += " and TSPL_RECEIPT_HEADER.Cust_Code in (" & strCustomer & ")"
            End If
            strExcludeCardIndentAdvanceDoc += "  ) "
        End If

        If Docwise = True Then
            strtempBaseQry = "  Select InnQuery.ACode AS ACode,InnQuery.Child AS Child, tspl_customer_master.customer_name AS AName,InnQuery.DocNo as DocNo,InnQuery.AgainstInvoiceNo AS AgainstInvoiceNo,InnQuery.DocDate ,InnQuery.DocType ,InnQuery.DocNarr ,InnQuery.ChequeDetails ,InnQuery.Currency_Code ,InnQuery.ConvRate,InnQuery.DrAmt ," + Environment.NewLine &
            "case when len( (isnull(TSPL_RECEIPT_HEADER.Location_GL_Code,'')))<=0 then " + Environment.NewLine &
            " case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.Location) then " + Environment.NewLine &
            " case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (CrAmt*  " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & " ) +isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0) else " + Environment.NewLine &
            " case when isnull((CrAmt),0)>0 then (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") -isnull((TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ),0) else 0 end end else " + Environment.NewLine &
            " case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND DocType<>'IM'  then  (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") +isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0) else (CrAmt*  " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") -isnull((TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ),0)  end end else " + Environment.NewLine &
            " case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_RECEIPT_HEADER.Location_GL_Code,'') then " + Environment.NewLine &
            " case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then   (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") +isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0) else  (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") -isnull((TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ),0) end else " + Environment.NewLine &
            " case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") +isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0) else  (CrAmt*  " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") -isnull((TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ),0) end end end as CrAmt, " + Environment.NewLine &
            " InnQuery.SecurityDrAmt ,InnQuery.SecurityCrAmt ,InnQuery.Sales ,InnQuery.CollectionRefund,InnQuery.DrNote,InnQuery.CrNote,InnQuery.Location,InnQuery.SourceCode ,InnQuery.Item_Code,InnQuery.Item_Desc,InnQuery.Receipt_Type,InnQuery.Bank_Code,InnQuery.Cust_Type_Code ,InnQuery.Cust_Type_Desc,InnQuery.Cust_Category_Code ,InnQuery.CUST_CATEGORY_DESC    from  (" & strtempBaseQryforopening & " ) InnQuery " + Environment.NewLine &
            "  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=InnQuery.Bank_Code left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =InnQuery.DocNo  "
        Else
            strtempBaseQry = "Select InnQuery.ACode AS ACode,InnQuery.Child AS Child, tspl_customer_master.customer_name AS AName,InnQuery.DocNo as DocNo,InnQuery.AgainstInvoiceNo AS AgainstInvoiceNo,InnQuery.DocDate ,InnQuery.DocType ,InnQuery.DocNarr ,InnQuery.ChequeDetails ,InnQuery.Currency_Code ,InnQuery.ConvRate,"

            If IsMISDebtorReport = True Then
                strtempBaseQry += " case when InnQuery.DocType NOT IN  ('IN','RF','RV-TA') THEN convert(decimal(18,2),InnQuery.DrAmt) ELSE 0 END AS DrAmt,case when InnQuery.DocType NOT IN ('OA','RC','PR','RV-TA') THEN convert(decimal(18,2),InnQuery.CrAmt) ELSE 0 END AS CrAmt,case when InnQuery.DocType='IN' THEN convert(decimal(18,2),InnQuery.Sales) ELSE 0 END AS Sales,case when InnQuery.DocType in ('OA','RC','PR','RF','RV-TA') THEN CASE WHEN InnQuery.DocType ='RV-TA' AND (sELECT Receipt_Type FROM TSPL_RECEIPT_HEADER WHERE Receipt_No =InnQuery.DocNarr )='F' THEN convert(decimal(18,2),InnQuery.CollectionRefund) * -1 ELSE convert(decimal(18,2),InnQuery.CollectionRefund) END ELSE 0 END AS CollectionRefund , " + Environment.NewLine
            Else
                strtempBaseQry += " convert(decimal(18,2),InnQuery.DrAmt) as DrAmt,convert(decimal(18,2),InnQuery.CrAmt) as CrAmt,convert(decimal(18,2),InnQuery.Sales) as Sales,case when InnQuery.DocType='IM' then case when InnQuery.CrAmt>0 then  convert(decimal(18,2),InnQuery.CrAmt) else convert(decimal(18,2),InnQuery.DrAmt) * -1 end  else convert(decimal(18,2),InnQuery.CollectionRefund) end  as CollectionRefund , " + Environment.NewLine
            End If
            strtempBaseQry += " InnQuery.SecurityDrAmt ,InnQuery.SecurityCrAmt ,InnQuery.DrNote,InnQuery.CrNote,InnQuery.Location,InnQuery.SourceCode ,InnQuery.Item_Code,InnQuery.Item_Desc,InnQuery.Receipt_Type,InnQuery.Bank_Code,InnQuery.Cust_Type_Code ,InnQuery.Cust_Type_Desc,InnQuery.Cust_Category_Code ,InnQuery.CUST_CATEGORY_DESC,InnQuery .EXCHANGE_GAIN_AMT ,InnQuery .EXCHANGE_LOSS_AMT  " &
     " from (Select InnQuery.ACode AS ACode,InnQuery.Child  , InnQuery.AName AS AName,InnQuery.DocNo as DocNo,InnQuery.AgainstInvoiceNo AS AgainstInvoiceNo,InnQuery.DocDate ,InnQuery.DocType ,InnQuery.DocNarr ,InnQuery.ChequeDetails ,InnQuery.Currency_Code ,InnQuery.ConvRate,InnQuery.DrAmt ," + Environment.NewLine &
     "case when len( (isnull(TSPL_RECEIPT_HEADER.Location_GL_Code,'')))<=0 then " + Environment.NewLine &
     " case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.Location) then " + Environment.NewLine &
     " case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' and (DocType)<>'IM' AND  (InnQuery.Receipt_Type )<>'U'  then  (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") else " + Environment.NewLine &
     " case when isnull((CrAmt),0)>0 AND (DocType)<>'IM' then (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") when isnull((CrAmt),0)>0  AND (DocType)='IM'  then (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") WHEN isnull((CrAmt),0)<0  AND (DocType)='IM'  then (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ")  else 0 end end else " + Environment.NewLine &
     " case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ")  else (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ")  end end else " + Environment.NewLine &
     " case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_RECEIPT_HEADER.Location_GL_Code,'') then " + Environment.NewLine &
     " case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then   (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") +isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0) else  (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") -isnull((TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ),0) end else " + Environment.NewLine &
     " case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ")  else  (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ")  end end end as CrAmt, " + Environment.NewLine &
     " InnQuery.SecurityDrAmt ,InnQuery.SecurityCrAmt ,InnQuery.Sales ,InnQuery.CollectionRefund,InnQuery.DrNote,InnQuery.CrNote,InnQuery.Location,InnQuery.SourceCode ,InnQuery.Item_Code,InnQuery.Item_Desc,InnQuery.Receipt_Type,InnQuery.Bank_Code,InnQuery.Cust_Type_Code ,InnQuery.Cust_Type_Desc,InnQuery.Cust_Category_Code ,InnQuery.CUST_CATEGORY_DESC,isnull(TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT,0) as EXCHANGE_LOSS_AMT,isnull(TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ,0) as EXCHANGE_GAIN_AMT    from  (" & strtempBaseQryforopening & " ) InnQuery " + Environment.NewLine &
     " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=InnQuery.Bank_Code left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =InnQuery.DocNo where 1=1 " & strExcludeCardIndentAdvanceDoc & " " & IIf(IsShowApplyDocument = False, " and InnQuery.DocType<>'IM' " & strExcludeEXcforApplyDocumnets & " ", "") & "  )InnQuery   "
        End If
        If IsCSA = True Then
            strtempBaseQry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code =InnQuery.ACode WHERE TSPL_CUSTOMER_MASTER.CSA_Type ='Y'"
        Else
            strtempBaseQry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code =InnQuery.ACode"
        End If
        Return strtempBaseQry
    End Function
    Public Shared Function GetCustomerBaseQryForOpening(ByVal ItemWisecheck As Boolean, ByVal IsSecurity As Boolean, ByVal SecurityType As String, ByVal Docwise As Boolean, ByVal CurrencyType As String, ByVal strCustomer As String, ByVal isOpening As Boolean, ByVal strfromdate As String, ByVal strtodate As String, Optional ByVal IsCSA As Boolean = False, Optional ByVal IsShowApplyDocument As Boolean = False, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal IsIncludecardIndent As Boolean = True, Optional ByVal DocNo As String = "") As String
        Dim strtempBaseQry As String = String.Empty
        Dim AllowtoSHOWParentChildCustomer As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSHOWParentChildCustomer, clsFixedParameterCode.AllowtoSHOWParentChildCustomer, trans)) = 1, True, False))
        Try
            Dim STRsecurityfilterquery As String = "AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'"
            If IsSecurity Then
                If clsCommon.myLen(SecurityType) > 0 Then
                    STRsecurityfilterquery = " And ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'') IN (" & clsCommon.myCstr(SecurityType) & ") "
                Else
                    STRsecurityfilterquery = ""
                End If
            End If
            If Not ItemWisecheck Then
                strtempBaseQry = "SELECT " + Environment.NewLine

                If Docwise Then
                    strtempBaseQry += "Max(ACode) AS ACode,max(Child) as Child,  " + Environment.NewLine
                Else
                    strtempBaseQry += " ACode AS ACode,max(Child) as Child, " + Environment.NewLine
                End If

                strtempBaseQry += "MAX(AName) AS AName,MAX(DocNo) AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType " &
                                " ,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(DrAmt) AS DrAmt, SUM(CrAmt) AS CrAmt, SUM(SecurityDrAmt) as SecurityDrAmt, SUM(SecurityCrAmt) as SecurityCrAmt, SUM(Sales) as Sales,"

                strtempBaseQry += "case when MAX(DocType)='IM' then case when SUM(CrAmt)>0 then  SUM(CrAmt) else  0 end  else  Sum(CollectionRefund) end  as CollectionRefund , SUM(DrNote) as DrNote,case when MAX(DocType)='IM' then 0 else  Sum(CrNote) end  as CrNote," '' SUM(CrNote) as CrNote,"

                If Docwise Then
                    strtempBaseQry += "MAX(Location) AS Location,  " + Environment.NewLine
                Else
                    strtempBaseQry += " Location AS Location, " + Environment.NewLine
                End If
                strtempBaseQry += " MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code, MAX(Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM ( "
                If AllowtoSHOWParentChildCustomer = True Then
                    strtempBaseQry += " Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then TSPL_RECEIPT_DETAIL.Child_Cust_Code else TSPL_CUSTOMER_MASTER.Cust_Code end as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_Customer_Invoice_Head.Document_No as AgainstInvoiceNo, Receipt_Date as DocDate,case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UR' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'PR' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA' else null end as DocType,"
                Else
                    strtempBaseQry += " Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_Customer_Invoice_Head.Document_No as AgainstInvoiceNo, Receipt_Date as DocDate,case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UR' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'PR' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA' else null end as DocType,"
                End If

                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Description FROM TSPL_FIXED_PARAMETER WHERE TYPE='Industry Type'", trans)), "D") = CompairStringResult.Equal Then
                    strtempBaseQry += "case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='S' THEN 'Security' ELSE CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='C' THEN 'Crate Security' ELSE  CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='R' THEN 'Refrigerator' ELSE CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='O' THEN 'Others' END END END END Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr,"
                Else
                    strtempBaseQry += " case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr,"
                End If

                strtempBaseQry += " (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate, " &
                " Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then (case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then Receipt_Amount else 0 end) Else 0 End as DrAmt," &
                " Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then (CASE WHEN   TSPL_RECEIPT_HEADER.Receipt_Type='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type<>'C' THEN TSPL_RECEIPT_DETAIL.Applied_Amount  WHEN TSPL_RECEIPT_HEADER.Receipt_Type ='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type='C' THEN  -1 * TSPL_RECEIPT_DETAIL.Applied_Amount when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 0 else Receipt_Amount END) Else 0 End AS CrAmt," &
                " Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then (case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then Receipt_Amount else 0 end) Else 0 End as SecurityDrAmt," &
                " Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then (CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN TSPL_RECEIPT_DETAIL.Applied_Amount when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 0 else Receipt_Amount END) Else 0 End AS SecurityCrAmt," &
                " 0 as [Sales], case when TSPL_RECEIPT_HEADER.Receipt_Type IN ('F') Then Receipt_Amount*-1 When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R') AND TSPL_RECEIPT_DETAIL.Receipt_Type<>'C' Then TSPL_RECEIPT_DETAIL.Applied_Amount WHEN TSPL_RECEIPT_HEADER.Receipt_Type ='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type='C' THEN  -1 * TSPL_RECEIPT_DETAIL.Applied_Amount Else Receipt_Amount end as [CollectionRefund], 0 as [DrNote], 0 as [CrNote]," &
                " CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN TSPL_Customer_Invoice_Head.Loc_Code  WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('O','P','F') THEN CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Location_GL_Code,'')<>'' THEN TSPL_RECEIPT_HEADER.Location_GL_Code ELSE substring(TSPL_RECEIPT_HEADER.Dr_Account, len(TSPL_RECEIPT_HEADER.Dr_Account)-2,3) END  ELSE substring(TSPL_RECEIPT_HEADER.Dr_Account, len(TSPL_RECEIPT_HEADER.Dr_Account)-2,3) END as [Location], (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' end) as SourceCode, '' as Item_Code, '' as Item_Desc" &
                ",TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC  " &
                " from  TSPL_RECEIPT_HEADER " &
                " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No " &
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No "
                If AllowtoSHOWParentChildCustomer = True Then
                    strtempBaseQry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code"
                Else
                    strtempBaseQry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=Case When TSPL_RECEIPT_HEADER.Receipt_Type='R' Then TSPL_Customer_Invoice_Head.Customer_Code Else TSPL_RECEIPT_HEADER.Cust_Code End"
                End If
                strtempBaseQry += " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " &
                " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE " &
                " where  TSPL_RECEIPT_HEADER.Posted='Y' " &
                "  and  TSPL_RECEIPT_HEADER.Receipt_Type not in ('M','A','U','K') " & STRsecurityfilterquery & " "
                If isOpening = True Then
                    strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                Else
                    strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                End If
                If clsCommon.myLen(strCustomer) > 0 Then
                    If AllowtoSHOWParentChildCustomer = True Then
                        strtempBaseQry += " and (TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ") or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                    Else
                        strtempBaseQry += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ")"
                    End If
                End If

                strtempBaseQry += Environment.NewLine + "----------------- Apply Document --------------------------------" + Environment.NewLine
                strtempBaseQry += Environment.NewLine + " UNION ALL " + Environment.NewLine &
                              "select TSPL_SD_SHIPMENT_head.Customer_Code as ACode,(TSPL_SD_SHIPMENT_head.Customer_Code) as Child,'' as AName,TSPL_SD_SHIPMENT_head.Document_Code as DocNo,'' as AgainstInvoiceNo,(CONVERT(DATE, TSPL_SD_SHIPMENT_head.Document_Date ,103)) as DocDate,'MI' as docType,(TSPL_SD_SHIPMENT_head.Remarks) as DocNarr,'' as ChequeDetails,
                             'INR' as Currency_Code, 1 as ConvRate,case when isnull(TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,0)=0 then TSPL_SD_SHIPMENT_head.Total_Amt else  TSPL_SD_SALE_INVOICE_HEAD.Total_Amt end as  DrAmt,0 as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund], 0 as [DrNote],0 as [CrNote],(TSPL_SD_SHIPMENT_head.Bill_To_Location) as Location,'' as SourceCode,'' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, '' as Bank_Code,(TSPL_CUSTOMER_MASTER.Cust_Type_Code) as Cust_Type_Code,(TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc) as  Cust_Type_Desc,(TSPL_CUSTOMER_MASTER.Cust_Category_Code) as Cust_Category_Code,(TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC ) as  CUST_CATEGORY_DESC
                             from TSPL_SD_SHIPMENT_head
                             left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_head.Customer_Code
                             LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE
                             LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   
							 left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_head.Document_Code
                             where (TSPL_SD_SHIPMENT_head.Document_Code  not in (select TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code from TSPL_SD_SALE_INVOICE_DETAIL )
							 or  TSPL_SD_SALE_INVOICE_HEAD.Document_Code not in (select Against_Sale_No from TSPL_Customer_Invoice_Head)) "
                If clsCommon.myLen(strCustomer) > 0 Then
                    strtempBaseQry += " and  TSPL_SD_SHIPMENT_head.Customer_Code  in(" & strCustomer & ")"

                End If
                strtempBaseQry += "    and (CONVERT(DATE, TSPL_SD_SHIPMENT_head.Document_Date ,103)) <'" + strfromdate + "'  " + Environment.NewLine
                strtempBaseQry += "    and TSPL_SD_SHIPMENT_head.Document_Code not in ('" + DocNo + "') "
                If Docwise = False Then
                    strtempBaseQry += Environment.NewLine + " UNION ALL " + Environment.NewLine &
                    "  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, ''  as AgainstInvoiceNo, Receipt_Date as DocDate,'EXC'  as DocType  , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, 1 as ConvRate,   TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT as DrAmt, TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT AS CrAmt, 0  as SecurityDrAmt, 0 AS SecurityCrAmt  , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], isnull( TSPL_Customer_Invoice_Head.Loc_Code,'') as [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER   " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code  " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   " + Environment.NewLine &
                    " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_RECEIPT_HEADER.Receipt_No " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No and TSPL_RECEIPT_DETAIL.Receipt_Line_No =1 " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No " + Environment.NewLine &
                    " where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type in ('R','A') " + Environment.NewLine &
                    " AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT  <>0) " + Environment.NewLine
                    If isOpening = True Then
                        strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                    Else
                        strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                    End If
                    If clsCommon.myLen(strCustomer) > 0 Then
                        If AllowtoSHOWParentChildCustomer = True Then
                            strtempBaseQry += " and (TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                        Else
                            strtempBaseQry += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ") "
                        End If
                    End If

                    strtempBaseQry += Environment.NewLine + "----------------------to find gain or loss amount FOR RECEIPT ---------------" + Environment.NewLine &
                    " UNION ALL " + Environment.NewLine &
                    " Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo, ''  as AgainstInvoiceNo, Reversal_Date as DocDate,'EXC'  as DocType  , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(TSPL_RECEIPT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_BANK_REVERSE.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_BANK_REVERSE.Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, 1 as ConvRate,   TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT as DrAmt, TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT AS CrAmt, 0  as SecurityDrAmt, 0 AS SecurityCrAmt  , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],isnull( TSPL_Customer_Invoice_Head.Loc_Code,'') as [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_BANK_REVERSE.Source_Type As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC from TSPL_BANK_REVERSE  " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BANK_REVERSE.Cust_Code  " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   " + Environment.NewLine &
                    " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_BANK_REVERSE.Reverse_Code  " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_BANK_REVERSE.Document_No " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No and TSPL_RECEIPT_DETAIL.Receipt_Line_No =1 " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No " + Environment.NewLine &
                    " where  TSPL_BANK_REVERSE.Reverse_Document='Receipts' AND TSPL_BANK_REVERSE.Post ='P'" + Environment.NewLine &
                    " and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT  <>0) " + Environment.NewLine
                    If isOpening = True Then
                        strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                    Else
                        strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                    End If
                    If clsCommon.myLen(strCustomer) > 0 Then
                        If AllowtoSHOWParentChildCustomer = True Then
                            strtempBaseQry += " and (TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                        Else
                            strtempBaseQry += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ") "
                        End If
                    End If
                    strtempBaseQry += "----------------------to find gain or loss amount FOR BANK REVERSE ---------------" + Environment.NewLine

                End If
                ''richa agarwal 13 Feb,2019 skip in case of parent child functionlity when include apply document check box is on ERO/05/02/19-000485
                If AllowtoSHOWParentChildCustomer = True AndAlso IsShowApplyDocument = True Then
                Else
                    strtempBaseQry += Environment.NewLine + " UNION ALL " + Environment.NewLine &
                  " Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, ''  as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM' else null end as DocType " &
                  " , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,  "   'Receipt_Amount  as DrAmt,

                    strtempBaseQry += "  case when RD.Receipt_Type='C' then 0 else RD.Applied_Amount  end as DrAmt,case when RD.Receipt_Type='C' then RD.Applied_Amount  else 0  end  AS CrAmt, Receipt_Amount as SecurityDrAmt, 0 AS SecurityCrAmt " &
                     " , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],  " &
                     "   CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE  CASE WHEN (SELECT ISNULL( TSPL_Customer_Invoice_Head.Loc_Code,'') FROM TSPL_Customer_Invoice_Head  WHERE TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_HEADER.Applied_Receipt )<>''  THEN  (SELECT ISNULL( TSPL_Customer_Invoice_Head.Loc_Code,'') FROM TSPL_Customer_Invoice_Head  WHERE TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_HEADER.Applied_Receipt) ELSE  substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END END AS [Location],  " & Environment.NewLine &
                     " (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC   " &
                     " from TSPL_RECEIPT_HEADER  " &
                     " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code " &
                     " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code " &
                     " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  " &
                     " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON TSPL_RECEIPT_HEADER.Receipt_No =RD.Receipt_No " &
                     " LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No " &
                     " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code " + Environment.NewLine &
                     " where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type ='A' AND CIH.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N' " & IIf(IsShowApplyDocument = False, " AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A' ", "") & "  "
                    If isOpening = True Then
                        strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                    Else
                        strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                    End If
                    If clsCommon.myLen(strCustomer) > 0 Then
                        If AllowtoSHOWParentChildCustomer = True Then
                            strtempBaseQry += " and (TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                        Else
                            strtempBaseQry += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ") "
                        End If
                    End If
                End If
                strtempBaseQry += Environment.NewLine + " ----------------- Invoices Against Apply Document --------------------------------" + Environment.NewLine &
                 " UNION ALL " + Environment.NewLine &
                 " SELECT MAX(ACode) AS Cust_Code,max(Child) as Child,MAX(AName) as AName,DocNo AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails " &
                 " ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,MAX(SecurityDrAmt) AS SecurityDrAmt,SUM(SecurityCrAmt) AS SecurityCrAmt " &
                 " ,MAX([Sales]) AS [Sales],MAX([CollectionRefund]) AS [CollectionRefund],MAX([DrNote]),MAX([CrNote]) AS [CrNote], "
                If Docwise Then
                    strtempBaseQry += "MAX([Location]) as Location , " + Environment.NewLine
                Else
                    strtempBaseQry += "   [Location], " + Environment.NewLine
                End If
                strtempBaseQry += " MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code,MAX(Cust_Type_Code) AS Cust_Type_Code " &
                 " ,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC " &
                 " FROM ( " &
                 " Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_RECEIPT_DETAIL.Document_No   as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM' else null end as DocType , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,    " 'TSPL_RECEIPT_DETAIL.Applied_Amount AS CrAmt, 
                strtempBaseQry += "case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE 0  end  as DrAmt,  case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then 0 else TSPL_RECEIPT_DETAIL.Applied_Amount  end as CrAmt,  0 as SecurityDrAmt,TSPL_RECEIPT_DETAIL.Applied_Amount  AS SecurityCrAmt , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], " &
                 " ISNULL(TSPL_Customer_Invoice_Head.Loc_Code ,'') AS [Location], " &
                 " (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_DETAIL.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER   " &
                 " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No  " &
                 " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code  " &
                 " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code    " &
                 " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   " &
                 " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_DETAIL.Document_No  " &
                 " where TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type ='A' AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N' " & IIf(IsShowApplyDocument = False, " AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A' ", "") & "  "

                If isOpening = True Then
                    strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                Else
                    strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                End If
                If clsCommon.myLen(strCustomer) > 0 Then
                    If AllowtoSHOWParentChildCustomer = True Then
                        strtempBaseQry += " and (TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                    Else
                        strtempBaseQry += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ") "
                    End If
                End If
                strtempBaseQry += " )INV "
                If Docwise Then
                    strtempBaseQry += " GROUP BY  DocNo " + Environment.NewLine
                Else
                    strtempBaseQry += " GROUP BY  DocNo,Location " + Environment.NewLine
                End If

                strtempBaseQry += Environment.NewLine + " ----------------- Invoices Against Set Off--------------------------------" + Environment.NewLine &
                 " UNION ALL " + Environment.NewLine &
                 " SELECT MAX(ACode) AS Cust_Code,max(Child) as Child,MAX(AName) as AName,DocNo AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails " &
                 " ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,MAX(SecurityDrAmt) AS SecurityDrAmt,SUM(SecurityCrAmt) AS SecurityCrAmt " &
                 " ,MAX([Sales]) AS [Sales],MAX([CollectionRefund]) AS [CollectionRefund],MAX([DrNote]),MAX([CrNote]) AS [CrNote], "
                If Docwise Then
                    strtempBaseQry += "MAX([Location]) as Location , " + Environment.NewLine
                Else
                    strtempBaseQry += "   [Location], " + Environment.NewLine
                End If
                strtempBaseQry += " MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code,MAX(Cust_Type_Code) AS Cust_Type_Code " &
                 " ,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC " &
                 " FROM ( " &
                 " Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_RECEIPT_DETAIL.Document_No   as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM'  WHEN TSPL_RECEIPT_HEADER.Receipt_Type='K' THEN 'KN' else null end as DocType , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,    " 'TSPL_RECEIPT_DETAIL.Applied_Amount AS CrAmt, 

                strtempBaseQry += "case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE case when TSPL_RECEIPT_DETAIL.Applied_Amount>0 then 0 else TSPL_RECEIPT_DETAIL.Applied_Amount *-1  end  end  as DrAmt,  case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then 0 else case when TSPL_RECEIPT_DETAIL.Applied_Amount>0 then TSPL_RECEIPT_DETAIL.Applied_Amount else 0 end  end as CrAmt,  0 as SecurityDrAmt,TSPL_RECEIPT_DETAIL.Applied_Amount  AS SecurityCrAmt , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], " &
                 " ISNULL(TSPL_Customer_Invoice_Head.Loc_Code ,'') AS [Location], " &
                 " (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' when TSPL_RECEIPT_HEADER.Receipt_Type='K' then 'AR-KN' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_DETAIL.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER   " &
                 " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No  " &
                 " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code  " &
                 " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code    " &
                 " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   " &
                 " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_DETAIL.Document_No  " &
                 " where TSPL_RECEIPT_HEADER.Posted='Y'  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N' " & IIf(IsShowApplyDocument = False, " AND TSPL_RECEIPT_HEADER.Receipt_Type <>'K' ", " and  TSPL_RECEIPT_HEADER.Receipt_Type ='K' ") & "  "

                If isOpening = True Then
                    strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                Else
                    strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                End If
                If clsCommon.myLen(strCustomer) > 0 Then
                    If AllowtoSHOWParentChildCustomer = True Then
                        strtempBaseQry += " and (TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                    Else
                        strtempBaseQry += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ") "
                    End If
                End If
                strtempBaseQry += " )INV "
                If Docwise Then
                    strtempBaseQry += " GROUP BY  DocNo " + Environment.NewLine
                Else
                    strtempBaseQry += " GROUP BY  DocNo,Location " + Environment.NewLine
                End If
                strtempBaseQry += Environment.NewLine + " UNION ALL " &
             " Select TSPL_RECEIPT_HEADER.Cust_Code as ACode,TSPL_RECEIPT_HEADER.Cust_Code as Child,TSPL_RECEIPT_HEADER.Customer_Name as AName, (Select r1.Receipt_No from TSPL_RECEIPT_HEADER r1 where r1 .UnApplied_No  =TSPL_RECEIPT_HEADER.Receipt_No)  as DocNo,'' as AgainstInvoiceNo,TSPL_RECEIPT_HEADER.Receipt_Date as DocDate,'RC' as DocType,'' as DocNarr,(rtrim(TSPL_RECEIPT_HEADER.Entry_Desc) + (case when len(RTRIM(TSPL_RECEIPT_HEADER.Entry_Desc))>0 and len(RTRIM(TSPL_RECEIPT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_RECEIPT_HEADER.Cheque_No +  ' - '+TSPL_RECEIPT_HEADER.Cheque_Date else '' end)) as ChequeDetails, RH.Currency_Code, RH.ConvRate, 0 as DrAmt, " &
             " TSPL_RECEIPT_HEADER.Receipt_Amount  AS CrAmt, 0 as SecurityDrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then TSPL_RECEIPT_HEADER.Receipt_Amount Else 0 End  AS SecurityCrAmt, 0 as [Sales], TSPL_RECEIPT_HEADER.Receipt_Amount as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], Right(TSPL_RECEIPT_HEADER.Dr_Account,3) as [Location], " &
             " 'AR-RC' as SourceCode, '' as Item_Code, '' as Item_Desc, TSPL_RECEIPT_HEADER.Receipt_Type As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code," &
             " TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC  " &
             " from  TSPL_RECEIPT_HEADER " &
             " LEFT OUTER JOIN TSPL_RECEIPT_HEADER RH ON TSPL_RECEIPT_HEADER.Receipt_No =RH.UnApplied_No  " &
             " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON RH.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code  " &
             " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " &
             " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE " &
             " where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type in ('U') " & STRsecurityfilterquery & ""
                If isOpening = True Then
                    strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                Else
                    strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                End If
                If clsCommon.myLen(strCustomer) > 0 Then
                    If AllowtoSHOWParentChildCustomer = True Then
                        strtempBaseQry += " and (TSPL_RECEIPT_HEADER.Cust_Code in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                    Else
                        strtempBaseQry += " and TSPL_RECEIPT_HEADER.Cust_Code in (" & strCustomer & ") "
                    End If
                End If
                strtempBaseQry += " )XX GROUP BY XX.ACode, XX.Location, XX.DocNo,XX.DocType " + Environment.NewLine
                strtempBaseQry += "  UNION ALL" + Environment.NewLine &
                " Select ACode,Child, AName, DocNo, AgainstInvoiceNo, DocDate, DocType, Narration, ChequeDetails, CURRENCY_CODE, ConvRate, DrAmt, CrAmt, SecurityDrAmt, SecurityCrAmt, Sales, Collectionrefund, DrNote, CrNote, Location, SourceCode, Item_Code, Item_Desc, Receipt_Type, Bank_Code, Cust_Type_Code, Cust_Type_Desc, Cust_Category_Code, CUST_CATEGORY_DESC from (" + Environment.NewLine &
                " Select TSPL_RECEIPT_HEADER.Cust_Code as ACode,TSPL_RECEIPT_HEADER.Cust_Code as Child, CIH.Customer_Code, CM.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, '' as AgainstInvoiceNo, TSPL_RECEIPT_HEADER.Receipt_Date as DocDate, 'IM' as DocType, TSPL_RECEIPT_HEADER.Narration, Case WHEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,'')<>'' Then TSPL_RECEIPT_HEADER.Cheque_No+' - '+CONVERT(VARCHAR,TSPL_RECEIPT_HEADER.Cheque_Date,103) Else '' End as ChequeDetails, TSPL_RECEIPT_HEADER.CURRENCY_CODE, TSPL_RECEIPT_HEADER.ConvRate, Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then RD.Applied_Amount Else 0 end as DrAmt, 0 as CrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then TSPL_RECEIPT_HEADER.Receipt_Amount Else 0 end as SecurityDrAmt, 0 as SecurityCrAmt, 0 as Sales, 0 as Collectionrefund, TSPL_RECEIPT_HEADER.Receipt_Amount as DrNote, 0 as CrNote, " & Environment.NewLine &
                "  CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END  AS [Location], 'AR-IM' as SourceCode, '' as Item_Code, '' as Item_Desc, 'A' as Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, CM.Cust_Type_Code, CTM.Cust_Type_Desc, CM.Cust_Category_Code, CCM.CUST_CATEGORY_DESC, ROW_NUMBER() OVER (Partition By TSPL_RECEIPT_HEADER.Receipt_No ORDER BY TSPL_RECEIPT_HEADER.Receipt_No) as RowNo  from TSPL_RECEIPT_HEADER TSPL_RECEIPT_HEADER" + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON RD.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No" + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No" + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM ON CM.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code" + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER CTM ON CTM.Cust_Type_Code = CM.Cust_Type_Code" + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER CCM ON CCM.Cust_Category_Code = CM.CUST_CATEGORY_CODE" + Environment.NewLine &
                " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code " + Environment.NewLine &
                " WHERE TSPL_RECEIPT_HEADER.Receipt_Type='A'  AND ISNULL(CIH.Customer_Code,'')<>TSPL_RECEIPT_HEADER.Cust_Code "

                strtempBaseQry += "" & STRsecurityfilterquery & "" + Environment.NewLine
                If isOpening = True Then
                    strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                Else
                    strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                End If
                If clsCommon.myLen(strCustomer) > 0 Then
                    If AllowtoSHOWParentChildCustomer = True Then
                        strtempBaseQry += " and (TSPL_RECEIPT_HEADER.Cust_Code in (" & strCustomer & ")  or isnull(CM.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                    Else
                        strtempBaseQry += " and TSPL_RECEIPT_HEADER.Cust_Code in (" & strCustomer & ") "
                    End If
                End If
                strtempBaseQry += " ) XX --WHERE RowNo=1" + Environment.NewLine &
         " UNION ALL" + Environment.NewLine &
          " Select max(ACode) as ACode ,max(Child) as Child , max(AName) as AName, max(DocNo) as DocNo, max(AgainstInvoiceNo) as AgainstInvoiceNo,max( DocDate) as DocDate, max(DocType) as DocType, max(Narration) as Narration, max(ChequeDetails) as Narration, max(CURRENCY_CODE) as CURRENCY_CODE, max(ConvRate) as ConvRate, Sum(DrAmt) as Dramt, Sum(CrAmt) as CrAmt, sum(SecurityDrAmt) as SecurityDrAmt, sum(SecurityCrAmt) as SecurityCrAmt, sum(Sales) as Sales, Sum(Collectionrefund) as Collectionrefund, Sum(DrNote) as DrNote, sum(CrNote) as CrNote, max(Location) as Location, max(SourceCode) as SourceCode, max(Item_Code) as Item_Code,max( Item_Desc) as Item_Desc, max(Receipt_Type) as Receipt_Type , max(Bank_Code) as Bank_Code, max(Cust_Type_Code) as Cust_Type_Code, max(Cust_Type_Desc) as Cust_Type_Desc, max(Cust_Category_Code) as Cust_Category_Code,max( CUST_CATEGORY_DESC) as  CUST_CATEGORY_DESC from (" & Environment.NewLine &
           " Select  isnull(CIH.Customer_Code,TSPL_RECEIPT_HEADER.Cust_Code )  as ACode,case when TSPL_RECEIPT_HEADER.Receipt_Type='A' then RD.Child_Cust_Code else CIH.Customer_Code end as Child, CM.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, CIH.Document_No as AgainstInvoiceNo, TSPL_RECEIPT_HEADER.Receipt_Date as DocDate, 'IM' as DocType, TSPL_RECEIPT_HEADER.Narration, Case WHEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,'')<>'' Then TSPL_RECEIPT_HEADER.Cheque_No+' - '+CONVERT(VARCHAR,TSPL_RECEIPT_HEADER.Cheque_Date,103) Else '' End as ChequeDetails, TSPL_RECEIPT_HEADER.CURRENCY_CODE, TSPL_RECEIPT_HEADER.ConvRate, 0 as DrAmt, RD.Applied_Amount as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as Sales," &
           "RD.Applied_Amount as CollectionRefund,0 as DrNote, 0 as CrNote," + Environment.NewLine &
          " CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END  AS [Location], 'AR-IM' as SourceCode, '' as Item_Code, '' as Item_Desc, 'A' as Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, CM.Cust_Type_Code, CTM.Cust_Type_Desc, CM.Cust_Category_Code, CCM.CUST_CATEGORY_DESC   from TSPL_RECEIPT_HEADER TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON RD.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No" + Environment.NewLine &
         " LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No" + Environment.NewLine &
         " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM ON CM.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code" + Environment.NewLine &
         " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER CTM ON CTM.Cust_Type_Code = CM.Cust_Type_Code" + Environment.NewLine &
         " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER CCM ON CCM.Cust_Category_Code = CM.CUST_CATEGORY_CODE" + Environment.NewLine &
         " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code " + Environment.NewLine &
         " WHERE TSPL_RECEIPT_HEADER.Receipt_Type='A' AND isnull(CIH.Customer_Code,'')<>TSPL_RECEIPT_HEADER.Cust_Code "
                strtempBaseQry += "" & STRsecurityfilterquery & ""
                If isOpening = True Then
                    strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                Else
                    strtempBaseQry += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                End If
                If clsCommon.myLen(strCustomer) > 0 Then
                    If AllowtoSHOWParentChildCustomer = True Then
                        strtempBaseQry += " and (CM.Cust_Code in (" & strCustomer & ")  or isnull(CM.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                    Else
                        strtempBaseQry += " and isnull(CIH.Customer_Code,TSPL_RECEIPT_HEADER.Cust_Code ) in (" & strCustomer & ") "
                    End If
                End If
                strtempBaseQry += " )z group by DocNo ,Location,ACode" & Environment.NewLine

                strtempBaseQry += Environment.NewLine + " UNION ALL" + Environment.NewLine &
                  " SELECT max( TSPL_ADJUSTMENT_HEADER.Customer_CODE) as ACode,max( TSPL_ADJUSTMENT_HEADER.Customer_CODE) as Child,max( TSPL_ADJUSTMENT_HEADER.Customer_NAME ) as AName, Final.Adjustment_No AS DocNo,'' as AgainstInvoiceNo,  CONVERT(DATE,MAX(Final.Adjustment_Date),103) AS DocDate, 'AD' AS DocType, MAX(Final.Description) AS DocNarr, '' AS ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when max(Final.Trans_Type) = 'In' then 0 else  SUM(Final.Cost)  end  AS DrAmt , case when max(Final.Trans_Type) ='In' then  SUM(Final.Cost) else 0 end AS CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], case when max(Final.Trans_Type) ='In' then  SUM(Final.Cost)*-1 else SUM(Final.Cost) end as Collectionrefund, 0 as [drNote], 0 as [CrNote], max(Final.Location) as [Location], Max(SourceCode)as SourceCode, Item_Code, MAX(Item_Description) as Item_Desc " &
                  " ,MAX(Receipt_Type) As Receipt_Type, '' as Bank_Code,MAX(Cust_Type_Code) as Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC " &
                  " FROM (SELECT  TSPL_ADJUSTMENT_HEADER.Adjustment_No,  TSPL_ADJUSTMENT_HEADER.Adjustment_Date,  TSPL_ADJUSTMENT_HEADER.Description + (CASE WHEN  TSPL_ADJUSTMENT_HEADER.Description = '' THEN '' ELSE ' - ' END) AS Description, case when tspl_customer_master.cust_type_code ='F' or tspl_customer_master.cust_type_code ='S' then 0 else  ISNULL( TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0) end  + ISNULL( TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) AS Cost,  TSPL_ADJUSTMENT_HEADER.Document_No , TSPL_LOCATION_MASTER.Loc_Segment_Code as [Location],TSPL_ADJUSTMENT_HEADER.Trans_Type ,'IC-AD' as SourceCode, TSPL_ADJUSTMENT_DETAIL.Item_Code, TSPL_ADJUSTMENT_DETAIL.Item_Description " &
                  " ,'' As Receipt_Type , TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC   " &
                  " FROM TSPL_ADJUSTMENT_HEADER " &
                  " LEFT OUTER JOIN  TSPL_ADJUSTMENT_DETAIL ON  TSPL_ADJUSTMENT_HEADER.Adjustment_No =  TSPL_ADJUSTMENT_DETAIL.Adjustment_No Left OUTER JOIN  TSPL_LOCATION_MASTER on  TSPL_ADJUSTMENT_DETAIL.Location_Code= TSPL_LOCATION_MASTER.Location_Code inner join  tspl_customer_master on  tspl_adjustment_header.customer_code= tspl_customer_master.cust_code " &
                  " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " &
                  " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  " &
                  " WHERE ( ( TSPL_ADJUSTMENT_HEADER.Reference_Document = '') AND ( TSPL_ADJUSTMENT_HEADER.Document_No= '' and  TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '') or TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice')  AND (ISNULL( TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0) + ISNULL( TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) <> 0)and  TSPL_ADJUSTMENT_HEADER.Posted='Y' "
                If isOpening = True Then
                    strtempBaseQry += "  and  convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                Else
                    strtempBaseQry += "  and  convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                End If
                If clsCommon.myLen(strCustomer) > 0 Then
                    If AllowtoSHOWParentChildCustomer = True Then
                        strtempBaseQry += " and (TSPL_ADJUSTMENT_HEADER.Customer_CODE in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                    Else
                        strtempBaseQry += " and TSPL_ADJUSTMENT_HEADER.Customer_CODE in (" & strCustomer & ") "
                    End If
                End If
                strtempBaseQry += " ) AS Final INNER JOIN  TSPL_ADJUSTMENT_HEADER  ON Final.Adjustment_No =  TSPL_ADJUSTMENT_HEADER.Adjustment_No  GROUP BY Final.Adjustment_No, Final.Item_Code" &
                Environment.NewLine + " UNION ALL" + Environment.NewLine
                If IsCSA = True Then
                    strtempBaseQry += "select TSPL_Customer_Invoice_Head.Customer_Code,TSPL_Customer_Invoice_Head.Customer_Code as Child, TSPL_Customer_Invoice_Head.Customer_Name ,case when len(TSPL_Customer_Invoice_Head.AgainstScrap)>0 then  TSPL_Customer_Invoice_Head.AgainstScrap  else  TSPL_Customer_Invoice_Head.Document_No  end DocNo," &
                   " CASE WHEN TSPL_Customer_Invoice_Head.Document_Type ='C' AND len(TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No)>0 THEN (Select Top 1 TSPL_SD_SALE_RETURN_HEAD.Document_Code  FROM TSPL_SD_SALE_RETURN_HEAD where TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No) WHEN (TSPL_Customer_Invoice_Head.Document_Type ='D' OR  TSPL_Customer_Invoice_Head.Document_Type ='I') AND LEN(TSPL_Customer_Invoice_Head.Against_Sale_No)>0 THEN (Select Top 1 TSPL_SD_SALE_INVOICE_HEAD.Document_Code  FROM TSPL_SD_SALE_INVOICE_HEAD where TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No ) END AS AgainstInvoiceNo," &
                   " CONVERT(Date,TSPL_Customer_Invoice_Head.Document_Date,103) as DocDate ,(case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DR' end) as [DocType], TSPL_Customer_Invoice_Head.Description ,'' AS ChequeDetails,'INR' AS Currency_Code, 1 AS ConvRate,case when TSPL_Customer_Invoice_Head.Document_Type ='C' AND len(TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No)<=0 then 0 when TSPL_Customer_Invoice_Head.Document_Type ='C' AND len(TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No)>0 then TSPL_Customer_Invoice_Head.Document_Total else (case when TSPL_Customer_Invoice_Head.Document_Type ='I' and len(InvGainLoss.Document_Code)>0 Then coalesce(InvGainLoss.CSA_Gain_Loss,0) else TSPL_Customer_Invoice_Head.Document_Total end) end as [DRAmt],case when TSPL_Customer_Invoice_Head.Document_Type ='C' AND len(TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No)<=0 then TSPL_Customer_Invoice_Head.Document_Total else 0 end as [CRAmt],0 as SecurityDr,0 as SecurityCr, (case when TSPL_Customer_Invoice_Head.Document_Type ='I' then (case when  len(InvGainLoss.Document_Code)>0 Then coalesce(InvGainLoss.CSA_Gain_Loss,0) Else TSPL_Customer_Invoice_Head.Document_Total End) else 0 end) as [Sales], 0 as [CollectionRefund],0 as DrNote, case when (TSPL_Customer_Invoice_Head.Document_Type ='D' or TSPL_Customer_Invoice_Head.Document_Type ='C' AND len(TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No)>0) Then TSPL_Customer_Invoice_Head.Document_Total when TSPL_Customer_Invoice_Head.Document_Type ='C' Then TSPL_Customer_Invoice_Head.Document_Total*-1 Else 0 End as [DrCrNote], TSPL_Customer_Invoice_Head.Loc_Code, case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'AR-IN'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'AR-CR'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'AR-DN' end as SourceCode, '' as Item_Code, '' as Item_Desc " &
                   " ,'' As Receipt_Type, '' as Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC " &
                   " from  TSPL_Customer_Invoice_Head " &
                   " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code  " &
                   " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " &
                   " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE " &
                   " LEFT OUTER JOIN (select Inv.Document_Code,sum(Invd.CSA_Gain_Loss) as CSA_Gain_Loss  from TSPL_SD_SALE_INVOICE_HEAD Inv " &
                   " inner join TSPL_SD_SALE_INVOICE_DETAIL Invd on Inv.Document_Code=Invd.DOCUMENT_CODE " &
                   " where Trans_Type='CSA' group by Inv.Document_Code) AS InvGainLoss on TSPL_Customer_Invoice_Head.Against_Sale_No=InvGainLoss.Document_Code " &
                   " where TSPL_Customer_Invoice_Head.Status=1  AND ISNULL(Against_VCGL ,'') ='' and TSPL_CUSTOMER_MASTER.CSA_TYPE='Y'"
                    If isOpening = True Then
                        strtempBaseQry += "  and  convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                    Else
                        strtempBaseQry += "  and  convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                    End If
                    If clsCommon.myLen(strCustomer) > 0 Then
                        If AllowtoSHOWParentChildCustomer = True Then
                            strtempBaseQry += " and (TSPL_Customer_Invoice_Head.Customer_Code in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                        Else
                            strtempBaseQry += " and TSPL_Customer_Invoice_Head.Customer_Code in (" & strCustomer & ") "
                        End If
                    End If
                    ''richa remove total tax amount with document amount becuase tax amount already included with document amt. 12 Sep,2018 KDI/13/09/18-000428
                    strtempBaseQry += Environment.NewLine & " UNION ALL" & Environment.NewLine &
                    " select TSPL_CSA_TRANSFER_HEAD.Cust_Code AS Customer_Code,TSPL_CSA_TRANSFER_HEAD.Cust_Code AS Child, TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CSA_TRANSFER_HEAD.DOC_CODE DocNo,'' AS AgainstInvoiceNo, " &
                    " CONVERT(Date,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) as DocDate ,TSPL_CSA_TRANSFER_HEAD.Document_Type as [DocType], TSPL_CSA_TRANSFER_HEAD.Description ,'' AS ChequeDetails,TSPL_CSA_TRANSFER_HEAD.CURRENCY_CODE,(case when TSPL_CSA_TRANSFER_HEAD.ConvRate=0 then 1 else coalesce(TSPL_CSA_TRANSFER_HEAD.ConvRate,1) end) as ConvRate," &
                    " TSPL_CSA_TRANSFER_HEAD.Document_Amount as [DRAmt],0 as [CRAmt],0 as SecurityDr,0 as SecurityCr, TSPL_CSA_TRANSFER_HEAD.Document_Amount as [Sales], 0 as [CollectionRefund],0 as DrNote, " &
                    " 0 as [DrCrNote], TSPL_CSA_TRANSFER_HEAD.From_Location_Code AS Loc_Code, 'SD-CSATRANS' as SourceCode, '' as Item_Code, " &
                    " '' as Item_Desc ,'' As Receipt_Type, '' as Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ," &
                    " TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC from TSPL_CSA_TRANSFER_HEAD " &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CSA_TRANSFER_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code  " &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code " &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE " &
                    " where TSPL_CSA_TRANSFER_HEAD.Status=1 "
                    If isOpening = True Then
                        strtempBaseQry += "  and  convert(date,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                    Else
                        strtempBaseQry += "  and  convert(date,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                    End If
                    If clsCommon.myLen(strCustomer) > 0 Then
                        If AllowtoSHOWParentChildCustomer = True Then
                            strtempBaseQry += " and (TSPL_CSA_TRANSFER_HEAD.Cust_Code in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                        Else
                            strtempBaseQry += " and TSPL_CSA_TRANSFER_HEAD.Cust_Code in (" & strCustomer & ") "
                        End If
                    End If
                    strtempBaseQry += Environment.NewLine & " UNION ALL" & Environment.NewLine &
                     " select TSPL_SD_SALE_RETURN_HEAD.Customer_Code, TSPL_SD_SALE_RETURN_HEAD.Customer_Code as Child,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SALE_RETURN_HEAD.Document_Code DocNo,'' AS AgainstInvoiceNo, " &
                     " CONVERT(Date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) as DocDate ,'CSA Transfer Return' as [DocType], TSPL_SD_SALE_RETURN_HEAD.Description,'' AS ChequeDetails,TSPL_SD_SALE_RETURN_HEAD.CURRENCY_CODE,(case when TSPL_SD_SALE_RETURN_HEAD.ConvRate=0 then 1 else coalesce(TSPL_SD_SALE_RETURN_HEAD.ConvRate,1) end) as ConvRate , " &
                     " 0 as [DRAmt],TSPL_SD_SALE_RETURN_HEAD.Total_Amt as [CRAmt],0 as SecurityDr,0 as SecurityCr, 0 as [Sales], 0 as [CollectionRefund],0 as DrNote,  -TSPL_SD_SALE_RETURN_HEAD.Total_Amt as [DrCrNote], " &
                     " TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location AS Loc_Code, 'SD-CSATRANS-RETURN' as SourceCode, '' as Item_Code,  " &
                     " '' as Item_Desc ,'' As Receipt_Type, '' as Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc , " &
                     " TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC from TSPL_SD_SALE_RETURN_HEAD  " &
                     " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_SD_SALE_RETURN_HEAD.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code   " &
                     " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " &
                     " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE " &
                     " where TSPL_SD_SALE_RETURN_HEAD.Status=1  and TSPL_SD_SALE_RETURN_HEAD.trans_type='CSA'  "
                    If isOpening = True Then
                        strtempBaseQry += "  and  convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                    Else
                        strtempBaseQry += "  and  convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                    End If
                    If clsCommon.myLen(strCustomer) > 0 Then
                        If AllowtoSHOWParentChildCustomer = True Then
                            strtempBaseQry += " and (TSPL_SD_SALE_RETURN_HEAD.Customer_Code in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                        Else
                            strtempBaseQry += " and TSPL_SD_SALE_RETURN_HEAD.Customer_Code in (" & strCustomer & ") "
                        End If
                    End If

                    strtempBaseQry += Environment.NewLine & " UNION ALL" & Environment.NewLine &
                    " select TSPL_VCGL_Head.VC_Code as ACode,TSPL_VCGL_Head.VC_Code as Child, TSPL_VCGL_Head.VC_Name as AName, TSPL_VCGL_Head.Document_No as DocNo,'' as AgainstInvoiceNo, CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) as DocDate,'VCGL' as docType,  TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails,'' as CURRENCY_CODE,1 as ConvRate,case when  TSPL_CUSTOMER_INVOICE_HEAD.Document_Type='D' then  TSPL_VCGL_Head.Amount else 0 end  as DrAmt,case when  TSPL_CUSTOMER_INVOICE_HEAD.Document_Type='C' then  TSPL_VCGL_Head.Amount else 0 end as CrAmt,0 as SecurityDr,0 as SecurityCr , 0 as [Sales], 0 as [CollectionRefund],0 as DrNote, case when TSPL_CUSTOMER_INVOICE_HEAD.Document_Type='D' then  TSPL_VCGL_Head.Amount Else TSPL_VCGL_Head.Amount*-1 End as [DrCrNote], TSPL_VCGL_Head.Location_Segment as Location, 'VC-GL' as SourceCode, '' as Item_Code, '' as Item_Desc " &
                    " ,'' As Receipt_Type, '' as Bank_Code,TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC " &
                    " from TSPL_VCGL_Head " &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code = TSPL_CUSTOMER_MASTER.Cust_Code " &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE " &
                    " left outer join TSPL_CUSTOMER_INVOICE_HEAD on TSPL_VCGL_Head.Document_No=TSPL_CUSTOMER_INVOICE_HEAD.Against_VCGL " &
                    " and TSPL_CUSTOMER_INVOICE_HEAD.Customer_Code=TSPL_VCGL_Head.VC_Code " &
                    " where TSPL_VCGL_Head.Document_Type='C' and  TSPL_VCGL_Head.Status=1 and TSPL_CUSTOMER_MASTER.CSA_TYPE='Y' "
                    If isOpening = True Then
                        strtempBaseQry += "  and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                    Else
                        strtempBaseQry += "  and  convert(date,TSPL_VCGL_Head.Document_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                    End If
                    If clsCommon.myLen(strCustomer) > 0 Then
                        If AllowtoSHOWParentChildCustomer = True Then
                            strtempBaseQry += " and  (TSPL_VCGL_Head.VC_Code in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                        Else
                            strtempBaseQry += " and  TSPL_VCGL_Head.VC_Code in (" & strCustomer & ") "
                        End If
                    End If
                    strtempBaseQry += Environment.NewLine & " UNION ALL" & Environment.NewLine &
                   " select TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_VCGL_Detail.VCGL_Code as Child, TSPL_VCGL_Detail.VCGL_Name as AName, TSPL_VCGL_Head.Document_No as DocNo,'' as AgainstInvoiceNo, CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) as DocDate,'VCGL' as docType,  TSPL_VCGL_Detail.Remarks as DocNarr,'' as ChequeDetails,'' as CURRENCY_CODE,1 as ConvRate, TSPL_VCGL_Detail.Dr_Amount as DrAmt , TSPL_VCGL_Detail.Cr_Amount as CrAmt,0 as SecurityDr,0 as SecurityCr, 0 as [Sales], 0 as [CollectionRefund],0 as DrNote, (TSPL_VCGL_Detail.Dr_Amount-TSPL_VCGL_Detail.Cr_Amount) as [DrCrNote], TSPL_VCGL_Head.Location_Segment as Location, 'VC-GL' as SourceCode, '' as Item_Code, '' as Item_Desc " &
                   " ,'' As Receipt_Type,'' as Bank_Code,TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC " &
                   " from  TSPL_VCGL_Detail" &
                   " left outer join  TSPL_VCGL_Head on  TSPL_VCGL_Head .Document_No= TSPL_VCGL_Detail.Document_No " &
                   " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code = TSPL_CUSTOMER_MASTER.Cust_Code " &
                   " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " &
                   " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  " &
                   " where TSPL_VCGL_Head.Status=1 and  TSPL_VCGL_Detail.Row_Type='Customer' and TSPL_CUSTOMER_MASTER.CSA_TYPE='Y'"
                    If isOpening = True Then
                        strtempBaseQry += "  and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                    Else
                        strtempBaseQry += "  and  convert(date,TSPL_VCGL_Head.Document_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                    End If
                    If clsCommon.myLen(strCustomer) > 0 Then
                        If AllowtoSHOWParentChildCustomer = True Then
                            strtempBaseQry += " and ( TSPL_VCGL_Detail.VCGL_Code in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                        Else
                            strtempBaseQry += " and TSPL_VCGL_Detail.VCGL_Code in (" & strCustomer & ")  "
                        End If
                    End If
                    strtempBaseQry += Environment.NewLine & " UNION ALL" & Environment.NewLine

                Else
                    strtempBaseQry += "select TSPL_Customer_Invoice_Head.Customer_Code, TSPL_Customer_Invoice_Head.Customer_Code as Child,TSPL_Customer_Invoice_Head.Customer_Name , TSPL_Customer_Invoice_Head.Document_No  AS DocNo," &
                      " CASE WHEN TSPL_Customer_Invoice_Head.Document_Type ='C' AND len(TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No)>0 THEN (Select Top 1 TSPL_SD_SALE_RETURN_HEAD.Document_Code  FROM TSPL_SD_SALE_RETURN_HEAD where TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No) WHEN (TSPL_Customer_Invoice_Head.Document_Type ='D' OR  TSPL_Customer_Invoice_Head.Document_Type ='I') AND LEN(TSPL_Customer_Invoice_Head.Against_Sale_No)>0 THEN "
                    strtempBaseQry += " CASE WHEN LEN(ISNULL( (Select Top 1 ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Document_Code,'')  FROM TSPL_SD_SALE_INVOICE_HEAD where TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No),''))>0 THEN (Select Top 1 ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Document_Code,'')  FROM TSPL_SD_SALE_INVOICE_HEAD where TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No) ELSE (Select Top 1 ISNULL(TSPL_INVOICE_MASTER_BULKSALE.Document_No,'')  FROM TSPL_INVOICE_MASTER_BULKSALE  where TSPL_INVOICE_MASTER_BULKSALE.Document_No  =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No) END   WHEN TSPL_Customer_Invoice_Head.Document_Type ='I' AND len(TSPL_Customer_Invoice_Head.AgainstScrap)>0 THEN (Select Top 1 TSPL_SCRAPINVOICE_HEAD.invoice_No  FROM TSPL_SCRAPINVOICE_HEAD where TSPL_SCRAPINVOICE_HEAD.invoice_No  =TSPL_CUSTOMER_Invoice_Head.AgainstScrap ) END AS AgainstInvoiceNo, " &
                    " CONVERT(Date,TSPL_Customer_Invoice_Head.Document_Date,103) as DocDate ,(case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DR' end) as [DocType], TSPL_Customer_Invoice_Head.Description ,'', TSPL_Customer_Invoice_Head.Currency_Code, TSPL_Customer_Invoice_Head.ConvRate, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then 0 else TSPL_Customer_Invoice_Head.Document_Total end as [DRAmt],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total else 0 end as [CRAmt], 0 as SecurityDrAmt, 0 as SecurityCrAmt, case when TSPL_Customer_Invoice_Head.Document_Type ='I' Then TSPL_Customer_Invoice_Head.Document_Total Else 0 End as [Sales], 0 as [CollectionRefund], case when TSPL_Customer_Invoice_Head.Document_Type ='D' Then TSPL_Customer_Invoice_Head.Document_Total Else 0 End as [DrNote], Case when TSPL_Customer_Invoice_Head.Document_Type ='C' Then TSPL_Customer_Invoice_Head.Document_Total*-1 Else 0 End as [CrNote], TSPL_Customer_Invoice_Head.Loc_Code, case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'AR-IN'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'AR-CR'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'AR-DN' end as SourceCode, '' as Item_Code, '' as Item_Desc " &
                    " ,'' As Receipt_Type, '' as Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC  " &
                    " from  TSPL_Customer_Invoice_Head " &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code  " &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE " &
                    " where TSPL_Customer_Invoice_Head.Status=1  "
                    If isOpening = True Then
                        strtempBaseQry += "  and  convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                    Else
                        strtempBaseQry += "  and  convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                    End If
                    If clsCommon.myLen(strCustomer) > 0 Then
                        If AllowtoSHOWParentChildCustomer = True Then
                            strtempBaseQry += " and (TSPL_Customer_Invoice_Head.Customer_Code in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                        Else
                            strtempBaseQry += " and TSPL_Customer_Invoice_Head.Customer_Code in (" & strCustomer & ") "
                        End If
                    End If
                    strtempBaseQry += Environment.NewLine + " UNION ALL" + Environment.NewLine
                End If
                strtempBaseQry += " select TSPL_Receipt_Adjustment_Header.Customer_No  as ACode,TSPL_Receipt_Adjustment_Header.Customer_No as Child, TSPL_Receipt_Adjustment_Header.Customer_Name  as AName, TSPL_Receipt_Adjustment_Header.Adjustment_No  as DocNo, " &
                " TSPL_Receipt_Adjustment_Header.ARInvoiceNo as AgainstInvoiceNo, CONVERT(DATE, TSPL_Receipt_Adjustment_Header.Adjustment_Date ,103) as DocDate,'Adjustment' as docType,  TSPL_Receipt_Adjustment_Header.Remarks as DocNarr, " &
                " '' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end   as DrAmt ,case when TSPL_Customer_Invoice_Head.Document_Type<>'C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end   as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund],  case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end *-1 as [DrNote], " &
                " case when TSPL_Customer_Invoice_Head.Document_Type<>'C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end  * -1 as [CrNote], TSPL_Customer_Invoice_Head.Loc_Code as Location, '' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, '' as Bank_Code, " &
                " TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    " &
                "  from TSPL_Receipt_Adjustment_Header  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Receipt_Adjustment_Header.Customer_No = TSPL_CUSTOMER_MASTER.Cust_Code  " &
                " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " &
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Document_No ,'') = TSPL_Receipt_Adjustment_Header.ARInvoiceNo  " &
                " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  " &
                " where isnull(TSPL_Receipt_Adjustment_Header.Is_Post,'')='Y' "
                If isOpening = True Then
                    strtempBaseQry += "  and  convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                Else
                    strtempBaseQry += "  and  convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                End If
                If clsCommon.myLen(strCustomer) > 0 Then
                    If AllowtoSHOWParentChildCustomer = True Then
                        strtempBaseQry += " and (TSPL_Receipt_Adjustment_Header.Customer_No  in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                    Else
                        strtempBaseQry += " and TSPL_Receipt_Adjustment_Header.Customer_No  in (" & strCustomer & ") "
                    End If
                End If
                strtempBaseQry += Environment.NewLine + " UNION ALL" + Environment.NewLine &
                     " ---------- INVOICES AGAINST BANK REVERSE --------- " + Environment.NewLine &
              " select (" & IIf(IsCSA = True Or Docwise = True, "TSPL_BANK_REVERSE.Cust_Code", "coalesce(TSPL_Customer_Invoice_Head.Customer_Code,TSPL_BANK_REVERSE.Cust_Code)") & ")   as ACode,(" & IIf(IsCSA = True Or Docwise = True, "TSPL_BANK_REVERSE.Cust_Code", "coalesce(TSPL_Customer_Invoice_Head.Customer_Code,TSPL_BANK_REVERSE.Cust_Code)") & ")   as Child,(" & IIf(IsCSA = True Or Docwise = True, "TSPL_BANK_REVERSE.Cust_Name", "coalesce( TSPL_Customer_Invoice_Head.Customer_Name,TSPL_BANK_REVERSE.Cust_Name)") & ")  as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo, " + Environment.NewLine

                If Docwise = True Then
                    strtempBaseQry += " '' as AgainstInvoiceNo, "
                Else
                    strtempBaseQry += " TSPL_Customer_Invoice_Head.Document_No as AgainstInvoiceNo, "
                End If
                strtempBaseQry += "TSPL_BANK_REVERSE.Reversal_Date as DocDate , 'RV-TA' as DocType,  TSPL_BANK_REVERSE.Document_No as DocNarr, TSPL_BANK_REVERSE.Cheque_No as ChequeDetails, TSPL_Receipt_Header.Currency_Code, TSPL_Receipt_Header.ConvRate, "
                If Docwise = True Then
                    strtempBaseQry += " CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type<>'F' THEN TSPL_BANK_REVERSE.Amount  ELSE 0  END , "
                Else
                    strtempBaseQry += " Case When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount * -1 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END When TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  0 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END Else CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type<>'F' THEN TSPL_BANK_REVERSE.Amount  ELSE 0  END End as DrAmt, "
                End If
                strtempBaseQry += " CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN TSPL_BANK_REVERSE.Amount When TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE 0 END ELSE 0 END as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales]," + Environment.NewLine

                If Docwise = True Then
                    strtempBaseQry += " TSPL_BANK_REVERSE.Amount *-1  as [CollectionRefund], "
                Else
                    strtempBaseQry += " Case When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount * -1 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END Else TSPL_BANK_REVERSE.Amount End*-1 as [CollectionRefund], "
                End If

                strtempBaseQry += " 0 as [DrNote], 0 as [CrNote]," + Environment.NewLine

                If Docwise = True Then
                    strtempBaseQry += " '' AS [Location], "
                Else
                    strtempBaseQry += " CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Location_GL_Code,'')<>'' THEN  CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('O','P','F') THEN TSPL_RECEIPT_HEADER.Location_GL_Code END WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') THEN TSPL_Customer_Invoice_Head.Loc_Code  ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END AS [Location], " + Environment.NewLine
                End If

                strtempBaseQry += " 'RV-TA'as SourceCode, '' as Item_Code, '' as Item_Desc " &
                " ,'' As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC     " &
                " from  TSPL_BANK_REVERSE  left outer join  TSPL_BANK_MASTER on  TSPL_BANK_REVERSE .Bank_Code = TSPL_BANK_MASTER.BANK_CODE " &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code  " &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE " &
                    " LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No " + Environment.NewLine

                If Docwise = False Then
                    strtempBaseQry += " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL On TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No "
                End If

                strtempBaseQry += " where  TSPL_BANK_REVERSE.Source_Type='AR' and  TSPL_BANK_REVERSE.post='P'" &
                    " AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' " & IIf(IsShowApplyDocument = False, " AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A' ", "") & " " + Environment.NewLine

                If isOpening = True Then
                    strtempBaseQry += "  and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                Else
                    strtempBaseQry += "  and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                End If
                If clsCommon.myLen(strCustomer) > 0 Then
                    If AllowtoSHOWParentChildCustomer = True Then
                        strtempBaseQry += " and ((coalesce(TSPL_Customer_Invoice_Head.Customer_Code,TSPL_BANK_REVERSE.Cust_Code))  in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                    Else
                        strtempBaseQry += " and (coalesce(TSPL_Customer_Invoice_Head.Customer_Code,TSPL_BANK_REVERSE.Cust_Code))  in (" & strCustomer & ") "
                    End If

                End If


                If IsShowApplyDocument = True Then
                    strtempBaseQry += " union all" + Environment.NewLine &
                " select TSPL_BANK_REVERSE.Cust_Code as ACode,TSPL_BANK_REVERSE.Cust_Code as Child, TSPL_BANK_REVERSE.Cust_Name as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo, " + Environment.NewLine &
                " TSPL_RECEIPT_HEADER.UnApplied_No as AgainstInvoiceNo, TSPL_BANK_REVERSE.Reversal_Date as DocDate , 'RV-TA' as DocType,  TSPL_BANK_REVERSE.Document_No as DocNarr, TSPL_BANK_REVERSE.Cheque_No as ChequeDetails, TSPL_Receipt_Header.Currency_Code, TSPL_Receipt_Header.ConvRate, Case When TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE 0 END " + Environment.NewLine &
 " Else 0 End as DrAmt, CASE When TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  0 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END ELSE TSPL_BANK_REVERSE.Amount END as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales],TSPL_RECEIPT_HEADER.UnApplied_Balance as [CollectionRefund],  0 as [DrNote], 0 as [CrNote]," + Environment.NewLine &
                " CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Location_GL_Code,'')<>'' THEN  CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('O','P','F') THEN TSPL_RECEIPT_HEADER.Location_GL_Code END WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') THEN TSPL_Customer_Invoice_Head.Loc_Code  ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END AS [Location] ,    'AR-UN' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code,TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC from  TSPL_BANK_REVERSE  left outer join  TSPL_BANK_MASTER on  TSPL_BANK_REVERSE .Bank_Code = TSPL_BANK_MASTER.BANK_CODE  " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code  " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No " + Environment.NewLine &
                "LEFT OUTER JOIN TSPL_RECEIPT_DETAIL On TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No " + Environment.NewLine &
                " where  TSPL_BANK_REVERSE.Source_Type='AR' and  TSPL_BANK_REVERSE.post='P' AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' and TSPL_RECEIPT_HEADER .Receipt_Type ='A' and isnull(TSPL_RECEIPT_HEADER.Receipt_No ,'')<>''  " + Environment.NewLine
                    If isOpening = True Then
                        strtempBaseQry += "  and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                    Else
                        strtempBaseQry += "  and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                    End If
                    If clsCommon.myLen(strCustomer) > 0 Then
                        If AllowtoSHOWParentChildCustomer = True Then
                            strtempBaseQry += " and (TSPL_BANK_REVERSE.Cust_Code  in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                        Else
                            strtempBaseQry += " and TSPL_BANK_REVERSE.Cust_Code  in (" & strCustomer & ") "
                        End If
                    End If
                    strtempBaseQry += Environment.NewLine + " ------------- BANK REVERSE ENTRY AGAINST APPLY DOCUMENT" + Environment.NewLine
                End If


                ' '' ------- unapplied entry
                If Docwise = False Then
                    strtempBaseQry += " union all" + Environment.NewLine &
                    "select TSPL_BANK_REVERSE.Cust_Code as ACode,TSPL_BANK_REVERSE.Cust_Code as Child, TSPL_BANK_REVERSE.Cust_Name as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo," + Environment.NewLine &
                    "TSPL_RECEIPT_HEADER.UnApplied_No as AgainstInvoiceNo, TSPL_BANK_REVERSE.Reversal_Date as DocDate , 'UA' as DocType,  TSPL_BANK_REVERSE.Document_No as DocNarr, TSPL_BANK_REVERSE.Cheque_No as ChequeDetails, TSPL_Receipt_Header.Currency_Code, TSPL_Receipt_Header.ConvRate,  TSPL_RECEIPT_HEADER.UnApplied_Balance as DrAmt,  0 as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales]," + Environment.NewLine &
                    "TSPL_RECEIPT_HEADER.UnApplied_Balance as [CollectionRefund],  0 as [DrNote], 0 as [CrNote]," + Environment.NewLine &
                    "substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) AS [Location], " + Environment.NewLine &
                    " 'AR-UN' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from  TSPL_BANK_REVERSE  left outer join  TSPL_BANK_MASTER on  TSPL_BANK_REVERSE .Bank_Code = TSPL_BANK_MASTER.BANK_CODE  " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code  " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_RECEIPT_HEADER as UnappliedReceipt ON UnappliedReceipt.Receipt_No = TSPL_RECEIPT_HEADER .UnApplied_No  " + Environment.NewLine &
                    " where  TSPL_BANK_REVERSE.Source_Type='AR' and  TSPL_BANK_REVERSE.post='P' AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' and UnappliedReceipt.Receipt_Type ='U' and isnull(TSPL_RECEIPT_HEADER.Receipt_No ,'')<>'' " + Environment.NewLine
                    If isOpening = True Then
                        strtempBaseQry += "  and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                    Else
                        strtempBaseQry += "  and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                    End If
                    If clsCommon.myLen(strCustomer) > 0 Then
                        If AllowtoSHOWParentChildCustomer = True Then
                            strtempBaseQry += " and (TSPL_BANK_REVERSE.Cust_Code  in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                        Else
                            strtempBaseQry += " and TSPL_BANK_REVERSE.Cust_Code  in (" & strCustomer & ") "
                        End If
                    End If
                End If

                strtempBaseQry += Environment.NewLine + " UNION ALL" + Environment.NewLine &
                        " select TSPL_VCGL_Head.VC_Code as ACode,TSPL_VCGL_Head.VC_Code as Child, TSPL_VCGL_Head.VC_Name as AName, TSPL_VCGL_Head.Document_No as DocNo,'' as AgainstInvoiceNo, CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) as DocDate,'VCGL' as docType,  TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when  TSPL_VCGL_Head.Amount_Type='Cr' then  TSPL_VCGL_Head.Amount else 0 end  as DrAmt ,case when  TSPL_VCGL_Head.Amount_Type='Dr' then  TSPL_VCGL_Head.Amount else 0 end as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund], case when TSPL_VCGL_Head.Amount_Type='Dr' then TSPL_VCGL_Head.Amount Else 0 End as [DrNote], case when TSPL_VCGL_Head.Amount_Type='Cr' then TSPL_VCGL_Head.Amount*-1 Else 0 End as [CrNote], TSPL_VCGL_Head.Location_Segment as Location, 'VC-GL' as SourceCode, '' as Item_Code, '' as Item_Desc " &
                    " ,'' As Receipt_Type, '' as Bank_Code,TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC     " &
                    " from TSPL_VCGL_Head " &
                        " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code = TSPL_CUSTOMER_MASTER.Cust_Code " &
                        " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " &
                        " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No" &
                        " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE " &
                        " where TSPL_VCGL_Head.Document_Type='C' and  TSPL_VCGL_Head.Status=1 AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') ='' "
                If isOpening = True Then
                    strtempBaseQry += "  and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                Else
                    strtempBaseQry += "  and  convert(date,TSPL_VCGL_Head.Document_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                End If
                If clsCommon.myLen(strCustomer) > 0 Then
                    If AllowtoSHOWParentChildCustomer = True Then
                        strtempBaseQry += " and  (TSPL_VCGL_Head.VC_Code  in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                    Else
                        strtempBaseQry += " and  TSPL_VCGL_Head.VC_Code  in (" & strCustomer & ") "
                    End If
                End If
                strtempBaseQry += Environment.NewLine + " UNION ALL" + Environment.NewLine &
                   " select TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_VCGL_Detail.VCGL_Code as Child, TSPL_VCGL_Detail.VCGL_Name as AName, TSPL_VCGL_Head.Document_No as DocNo,'' as AgainstInvoiceNo, CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) as DocDate,'VCGL' as docType,  TSPL_VCGL_Detail.Remarks as DocNarr,'' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, TSPL_VCGL_Detail.Dr_Amount as DrAmt , TSPL_VCGL_Detail.Cr_Amount as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund], TSPL_VCGL_Detail.Dr_Amount as DrNote, TSPL_VCGL_Detail.Cr_Amount as [CrNote], TSPL_VCGL_Head.Location_Segment as Location, 'VC-GL' as SourceCode, '' as Item_Code, '' as Item_Desc " &
                   " ,'' As Receipt_Type,'' as Bank_Code,TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    " &
                   " from  TSPL_VCGL_Detail" &
                       " left outer join  TSPL_VCGL_Head on  TSPL_VCGL_Head .Document_No= TSPL_VCGL_Detail.Document_No " &
                       " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code = TSPL_CUSTOMER_MASTER.Cust_Code " &
                       " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  " &
                       " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  " &
                       " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Detail.Document_No      " &
                       " where TSPL_VCGL_Head.Status=1 and  TSPL_VCGL_Detail.Row_Type='Customer' AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') ='' "
                If isOpening = True Then
                    strtempBaseQry += "  and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                Else
                    strtempBaseQry += "  and  convert(date,TSPL_VCGL_Head.Document_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                End If
                If clsCommon.myLen(strCustomer) > 0 Then
                    If AllowtoSHOWParentChildCustomer = True Then
                        strtempBaseQry += " and  (TSPL_VCGL_Detail.VCGL_Code  in (" & strCustomer & ")  or isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')  in (" & strCustomer & ") )"
                    Else
                        strtempBaseQry += " and  TSPL_VCGL_Detail.VCGL_Code  in (" & strCustomer & ")  "
                    End If
                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return strtempBaseQry
    End Function

    Public Function GetInvoiceType(ByVal strCustCode As String, ByVal strLocation As String, ByVal strType As String, ByVal trans As SqlTransaction)
        Dim dt As DataTable


        Dim qry As String

        If clsCommon.CompairString(strType, "S") = CompairStringResult.Equal Then

            qry = "select State from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" & strLocation & "'"
        Else

            qry = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State, " & _
              "TSPL_LOCATION_MASTER.Sales_Tax_Group as LocalTaxGroup,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Local_Tax_GroupName, " & _
              "TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as InterstateTaxGroup,TSPL_TAX_GROUP_MASTERIS.Tax_Group_Desc as Interstate_Tax_GroupName " & _
              "FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_TAX_GROUP_MASTER as TSPL_TAX_GROUP_MASTERIS on TSPL_TAX_GROUP_MASTERIS.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTERIS.Tax_Group_Type='S' " & _
              "WHERE TSPL_LOCATION_MASTER.Location_Code = '" + strLocation + "'"
        End If
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strLocState As String = clsCommon.myCstr(dt.Rows(0)("State"))

        qry = "select Price_Code,price_CodeNon,State,Tin_No from TSPL_CUSTOMER_MASTER where Cust_Code='" + strCustCode + "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strCustState As String = clsCommon.myCstr(dt.Rows(0)("State"))
        Dim strTinNo As String = clsCommon.myCstr(dt.Rows(0)("Tin_No"))
        If clsCommon.myLen(strTinNo) > 0 AndAlso clsCommon.CompairString(strLocState, strCustState) = CompairStringResult.Equal Then
            Invoice_Type = "T"
        Else
            Invoice_Type = "R"
        End If
        Return Invoice_Type
    End Function
    Public Function SaveData(ByVal obj As clsSNShipmentHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For i As Integer = 0 To obj.Arr.Count - 1
                    clsRCDFRateControl.CheckRCDFRateControl(clsCommon.myCstr(obj.Arr(i).Item_Code), clsCommon.myCstr(obj.Arr(i).Unit_code), clsCommon.myCDecimal(obj.Arr(i).Item_Cost), clsCommon.myCDate(obj.Document_Date), trans)
                Next
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Shipment/Sale Invoice", obj.Bill_To_Location, obj.Document_Date, trans)
            clsSerializeInvenotry.DeleteData("SD-IN", obj.Document_Code, trans)
            Dim qry1 As String = "delete from TSPL_SD_SHIPMENT_WEIGHMENT_MAPPING where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry1, trans)
            Dim qry As String = "delete from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNShipment, "", obj.Bill_To_Location)
            End If
            '''' for Invoice Type
            Dim AllowChangeInvoiceType As Boolean = False
            AllowChangeInvoiceType = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Allow_Change_InvoiceType from TSPL_inv_parameters", trans)) = 0, False, True)
            If obj.Is_Create_Auto_Invoice Then
                If AllowChangeInvoiceType Then
                    If clsCommon.myLen(obj.Invoice_Type) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select invoice  Type for creating auto invoice")
                        Return False
                    End If
                Else
                    If clsCommon.myLen(obj.Ship_To_Location) > 0 Then
                        obj.Invoice_Type = GetInvoiceType(obj.Customer_Code, obj.Ship_To_Location, "S", trans)
                    Else
                        obj.Invoice_Type = GetInvoiceType(obj.Customer_Code, obj.Bill_To_Location, "B", trans)
                    End If
                End If
            End If
            ''''' invoice type ends here

            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            ''richa 13/08/2014 against Ticket No BM00000003502
            'If IsDBNull("cust_po_date") Then
            If clsCommon.myLen(obj.Podate) <= 0 Then
                clsCommon.AddColumnsForChange(coll, "cust_po_date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "cust_po_date", clsCommon.GetPrintDate(obj.Podate, "dd/MMM/yyyy hh:mm tt"))
            End If
            '--------------------------------------------------------
            '-----------------richa 27/06/2014 Ticket No .BM00000002982--------------------------------
            Dim isIncrementCounter As Boolean = True
            If obj.Mannual_Invoice_No > 0 OrElse clsCommon.myLen(obj.InvoiceManualNowithPrefix) > 0 Then
                isIncrementCounter = False
            End If
            Dim Doc_Code As String = Nothing


            If obj.Mannual_Invoice_No > 0 Then
                If clsCommon.CompairString(obj.Invoice_Type, "T") = CompairStringResult.Equal Then
                    Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceTax, obj.Bill_To_Location, False, isIncrementCounter)
                ElseIf clsCommon.CompairString(obj.Invoice_Type, "R") = CompairStringResult.Equal Then
                    Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceRetail, obj.Bill_To_Location, False, isIncrementCounter)
                ElseIf clsCommon.CompairString(obj.Invoice_Type, "S") = CompairStringResult.Equal Then
                    Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceService, obj.Bill_To_Location, False, isIncrementCounter)
                End If

                Dim strDocLike As String = ""
                Dim arr As Array = Doc_Code.ToCharArray()
                For jj As Integer = 0 To arr.Length - clsCommon.myLen(obj.Mannual_Invoice_No) - 1
                    strDocLike += clsCommon.myCstr(arr(jj))
                Next
                strDocLike += clsCommon.myCstr(obj.Mannual_Invoice_No)
                Doc_Code = strDocLike
                obj.Mannual_Invoice_No = Doc_Code

            ElseIf clsCommon.myLen(obj.InvoiceManualNowithPrefix) > 0 Then
                Doc_Code = obj.InvoiceManualNowithPrefix
                obj.InvoiceManualNowithPrefix = Doc_Code
            End If

            '---------------------------------------------------------------------------------------
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Internal", IIf(obj.Is_Internal, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Inv_No", obj.Inv_No)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Bill_To_Location", obj.Bill_To_Location)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Location", obj.Ship_To_Location)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
            clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
            clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
            clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
            clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
            clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Discount_Base", obj.Discount_Base)
            clsCommon.AddColumnsForChange(coll, "Discount_Amt", obj.Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", obj.Amount_Less_Discount)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "PROJECT_ID", obj.PROJECT_ID, True)
            clsCommon.AddColumnsForChange(coll, "is_taxable", obj.is_taxable)
            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Transporter_Name", obj.Carrier)
            clsCommon.AddColumnsForChange(coll, "Transport_Id", obj.transport_id)
            clsCommon.AddColumnsForChange(coll, "VehicleNo", obj.VehicleNo)
            'clsCommon.AddColumnsForChange(coll, "Description", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "GRNo", obj.GRNo)
            clsCommon.AddColumnsForChange(coll, "GENo", obj.GENo)
            clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
            clsCommon.AddColumnsForChange(coll, "Against_Sales_Order", obj.Against_Sales_Order, True)
            clsCommon.AddColumnsForChange(coll, "Against_POS", obj.Against_POS, True)
            If obj.GEDate.HasValue Then
                clsCommon.AddColumnsForChange(coll, "GEDate", clsCommon.GetPrintDate(obj.GEDate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "GEDate", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code1", obj.Add_Charge_Code1)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name1", obj.Add_Charge_Name1)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt1", obj.Add_Charge_Amt1)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code2", obj.Add_Charge_Code2)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name2", obj.Add_Charge_Name2)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt2", obj.Add_Charge_Amt2)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code3", obj.Add_Charge_Code3)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name3", obj.Add_Charge_Name3)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt3", obj.Add_Charge_Amt3)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code4", obj.Add_Charge_Code4)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name4", obj.Add_Charge_Name4)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt4", obj.Add_Charge_Amt4)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code5", obj.Add_Charge_Code5)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name5", obj.Add_Charge_Name5)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt5", obj.Add_Charge_Amt5)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code6", obj.Add_Charge_Code6)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name6", obj.Add_Charge_Name6)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt6", obj.Add_Charge_Amt6)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code7", obj.Add_Charge_Code7)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name7", obj.Add_Charge_Name7)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt7", obj.Add_Charge_Amt7)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code8", obj.Add_Charge_Code8)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name8", obj.Add_Charge_Name8)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt8", obj.Add_Charge_Amt8)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code9", obj.Add_Charge_Code9)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name9", obj.Add_Charge_Name9)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt9", obj.Add_Charge_Amt9)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code10", obj.Add_Charge_Code10)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name10", obj.Add_Charge_Name10)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt10", obj.Add_Charge_Amt10)
            clsCommon.AddColumnsForChange(coll, "Total_Add_Charge", obj.Total_Add_Charge)
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))

            If obj.Challan_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"))
            End If
            If obj.Inv_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Inv_Date", clsCommon.GetPrintDate(obj.Inv_Date, "dd/MMM/yyyy"))
            End If


            clsCommon.AddColumnsForChange(coll, "Is_Create_Auto_Invoice", IIf(obj.Is_Create_Auto_Invoice, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Sale_Invoice_No", obj.Sale_Invoice_No)
            clsCommon.AddColumnsForChange(coll, "Is_Create_Auto_Receipt", IIf(obj.Is_Create_Auto_Receipt, 1, 0))

            clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code, True)
            clsCommon.AddColumnsForChange(coll, "Salesman_Name", obj.Salesman_Name)

            '' currencyconversion
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            clsCommon.AddColumnsForChange(coll, "ApplicableFrom", obj.ApplicableFrom, True)
            '' End currencyconversion
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "Route_Desc", obj.Route_Desc)
            clsCommon.AddColumnsForChange(coll, "HeadDisc_Per", obj.HeadDisc_Per)
            clsCommon.AddColumnsForChange(coll, "HeadDisc_PerAmt", obj.HeadDisc_PerAmt)
            clsCommon.AddColumnsForChange(coll, "HeadDisc_Amt", obj.HeadDisc_Amt)
            clsCommon.AddColumnsForChange(coll, "TotCashDiscAmt", obj.TotCashDiscAmt)
            clsCommon.AddColumnsForChange(coll, "Invoice_Type", obj.Invoice_Type)
            clsCommon.AddColumnsForChange(coll, "Price_Group_Code", obj.Price_Group_Code)
            clsCommon.AddColumnsForChange(coll, "Cust_PO_No", obj.Cust_PO_No)
            clsCommon.AddColumnsForChange(coll, "Form_38_No", obj.Form_38_No)
            '-----------------richa 27/06/2014 Ticket No .BM00000002982--------------------------------
            clsCommon.AddColumnsForChange(coll, "Mannual_Invoice_No", obj.Mannual_Invoice_No)
            clsCommon.AddColumnsForChange(coll, "Mannual_Invoice_No_StringType", obj.InvoiceManualNowithPrefix)

            'clsCommon.AddColumnsForChange(coll, "Credit_Limit", obj.Credit_Limit)
            'clsCommon.AddColumnsForChange(coll, "Advance_Security", obj.Advance_Security)
            'clsCommon.AddColumnsForChange(coll, "Revese_Adv_Security", obj.Revese_Adv_Security)
            'clsCommon.AddColumnsForChange(coll, "AR_Credit_Security", obj.AR_Credit_Security)
            'clsCommon.AddColumnsForChange(coll, "Pending_Posted_DO", obj.Pending_Posted_DO)
            'clsCommon.AddColumnsForChange(coll, "UnPostedShipment", obj.UnPostedShipment)
            'clsCommon.AddColumnsForChange(coll, "Ledger_Outstansing", obj.Ledger_Outstansing)
            'clsCommon.AddColumnsForChange(coll, "Refund_Security", obj.Refund_Security)
            'clsCommon.AddColumnsForChange(coll, "Reverse_Refund_Sec", obj.Reverse_Refund_Sec)
            clsCommon.AddColumnsForChange(coll, "Total_Outstanding", obj.Total_Outstanding)
            '
            clsCommon.AddColumnsForChange(coll, "Order_Qty", obj.Order_Qty, True)
            clsCommon.AddColumnsForChange(coll, "Weighment_Code", obj.Weighment_Code, True)
            clsCommon.AddColumnsForChange(coll, "Beejak_No", obj.Beejak_No)
            clsCommon.AddColumnsForChange(coll, "LR_GR_NO", obj.LR_GR_NO)
            If obj.LR_GR_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "LR_GR_Date", clsCommon.GetPrintDate(obj.LR_GR_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "LR_GR_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "SHIP_TO_DELIVERY_AT", obj.SHIP_TO_DELIVERY_AT)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SHIPMENT_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsSNShipmentDetail.SaveData(obj.Document_Code, Arr, trans, obj.Document_Date)
            isSaved = isSaved AndAlso clsSNShipmentWeighment.SaveData(obj.Document_Code, obj.ArrWeighmentList, trans)
            isSaved = isSaved AndAlso clsSNShipmentDCSItemDetail.SaveData(obj.Document_Code, obj.ArrDCSItem, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_Code, obj.arrCustomFields, trans)
            '''' to save item weight unit
            qry = "update TSPL_SD_shipment_DETAIL set Weight_UOM= (select Weight_UOM from TSPL_ITEM_MASTER where Item_Code=TSPL_SD_shipment_DETAIL.Item_Code)  where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Document_Code", obj.Document_Code, "TSPL_SD_SHIPMENT_HEAD", trans)
            '''' 
        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function UpdateAfterPosting(ByVal obj As clsSNShipmentHead, ByVal trans As SqlTransaction) As Boolean
        Try
            If obj IsNot Nothing And clsCommon.myLen(obj.Document_Code) > 0 Then
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "EWayBillNo", obj.EWayBillNo)
                clsCommon.AddColumnsForChange(coll, "Electronic_Ref_No", obj.Electronic_Ref_No)

                If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", Nothing, True)
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SHIPMENT_HEAD.Document_Code='" + obj.Document_Code + "'", trans)

                Dim coll2 As New Hashtable()
                Dim objInvoice As New clsSNInvoiceHead
                objInvoice.Document_Code = clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" + obj.Document_Code + "'", trans)
                clsCommon.AddColumnsForChange(coll2, "EWayBillNo", obj.EWayBillNo)
                clsCommon.AddColumnsForChange(coll2, "Electronic_Ref_No", obj.Electronic_Ref_No)
                If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll2, "EWayBillDate", clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll2, "EWayBillDate", Nothing, True)
                End If
                clsCommonFunctionality.UpdateDataTable(coll2, "TSPL_SD_SALE_INVOICE_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SALE_INVOICE_HEAD.Document_Code='" + objInvoice.Document_Code + "'", trans)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String, ByVal InvoiceNo As String, ByVal NavType As NavigatorType) As Boolean
        '' created by Sanjay
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim obj As clsSNShipmentHead = clsSNShipmentHead.GetData(Doc_No, NavType, trans)

            If obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If

            ''richa agarwal 24 Dec,2020
            ''----------



            qry = "delete from TSPL_SD_SHIPMENT_DETAIL where document_code ='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SD_SHIPMENT_HEAD where document_code ='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            obj = Nothing
            qry = Nothing

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function checkSaveNotification(ByVal obj As clsSNShipmentHead, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Count As Integer = 0
            Dim CreditLimit As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Credit_Limit from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Customer_Code + "'", trans))
            Dim qry As String
            Dim dt As DataTable = clsScreenNotificationSchedule.GetScreenNotificationInfo(clsUserMgtCode.frmSNShipment, trans)
            For Each dr As DataRow In dt.Rows
                'Criteria, Notification, Validation
                If clsCommon.CompairString(dr("Criteria"), "Credit days") = CompairStringResult.Equal Then
                    qry = "Select COUNT(*) from TSPL_SD_SHIPMENT_HEAD" & _
        " LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code" & _
        " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & _
        " WHERE TSPL_SD_SHIPMENT_HEAD.Status = 1" & _
        " AND TSPL_SD_SHIPMENT_HEAD.Customer_Code='" + obj.Customer_Code + "'" & _
        " AND TSPL_SD_SHIPMENT_HEAD.Due_Date<'" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "'" & _
        " AND ISNULL(TSPL_Customer_Invoice_Head.Balance_Amt,0)<>0"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                        If clsCommon.CompairString(dr("Validation"), "Required Approval") = CompairStringResult.Equal Then
                            'clsCommon.MyMessageBoxShow(clsCommon.myCstr(dt.Rows(0)("Notification")))
                            If common.clsCommon.MyMessageBoxShow(clsCommon.myCstr(dr("Notification")) + Environment.NewLine + "Do you want to continue?.", "Load Out", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                                Dim frm As New FrmPWD(trans)
                                frm.strCode = clsFixedParameterCode.CreditLimitApproval
                                frm.strType = clsFixedParameterType.CreditLimitApproval
                                frm.ShowDialog()
                                If frm.isPasswordCorrect Then
                                    Count += 1
                                End If
                            Else
                                Return False
                            End If
                        Else
                            Throw New Exception(clsCommon.myCstr(dt.Rows(0)("Notification")))
                        End If
                    End If
                ElseIf clsCommon.CompairString(dr("Criteria"), "Credit Amount") = CompairStringResult.Equal Then
                    qry = "Select SUM(TSPL_Customer_Invoice_Head.Balance_Amt) from TSPL_SD_SHIPMENT_HEAD" & _
        " LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code" & _
        " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & _
        " WHERE TSPL_SD_SHIPMENT_HEAD.Status = 1" & _
        " AND TSPL_SD_SHIPMENT_HEAD.Customer_Code='" + obj.Customer_Code + "'" & _
        " AND TSPL_SD_SHIPMENT_HEAD.Document_Date<'" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "'" & _
        " AND ISNULL(TSPL_Customer_Invoice_Head.Balance_Amt,0)<>0"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > CreditLimit Then
                        If clsCommon.CompairString(dr("Validation"), "Required Approval") = CompairStringResult.Equal Then
                            'clsCommon.MyMessageBoxShow(clsCommon.myCstr(dt.Rows(0)("Notification")))
                            If common.clsCommon.MyMessageBoxShow(clsCommon.myCstr(dr("Notification")) + Environment.NewLine + "Do you want to continue?.", "Load Out", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                                Dim frm As New FrmPWD(trans)
                                frm.strCode = clsFixedParameterCode.CreditLimitApproval
                                frm.strType = clsFixedParameterType.CreditLimitApproval
                                frm.ShowDialog()
                                If frm.isPasswordCorrect Then
                                    Count += 1
                                End If
                            Else
                                Return False
                            End If
                        Else
                            Throw New Exception(clsCommon.myCstr(dt.Rows(0)("Notification")))
                        End If
                    End If
                End If
            Next
            If Count >= 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsSNShipmentHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSNShipmentHead
        Dim obj As clsSNShipmentHead = Nothing
        Dim qry As String = "SELECT TSPL_SD_SHIPMENT_HEAD.Order_Qty,TSPL_SD_SHIPMENT_HEAD.Weighment_Code,TSPL_SD_SHIPMENT_HEAD.Beejak_No,TSPL_SD_SHIPMENT_HEAD.LR_GR_NO,TSPL_SD_SHIPMENT_HEAD.LR_GR_Date,TSPL_SD_SHIPMENT_HEAD.SHIP_TO_DELIVERY_AT,TSPL_SD_SHIPMENT_HEAD.Credit_Limit,TSPL_SD_SHIPMENT_HEAD.Advance_Security,TSPL_SD_SHIPMENT_HEAD.Revese_Adv_Security,TSPL_SD_SHIPMENT_HEAD.AR_Credit_Security,TSPL_SD_SHIPMENT_HEAD.Ledger_Outstansing,TSPL_SD_SHIPMENT_HEAD.Refund_Security,TSPL_SD_SHIPMENT_HEAD.Reverse_Refund_Sec,TSPL_SD_SHIPMENT_HEAD.Total_Outstanding,  TSPL_SD_SHIPMENT_HEAD.Electronic_Ref_No,TSPL_SD_SHIPMENT_HEAD.EWayBillNo,TSPL_SD_SHIPMENT_HEAD.EWayBillDate,TSPL_SD_SHIPMENT_HEAD.is_taxable,TSPL_SD_SHIPMENT_HEAD.Is_Delivered,TSPL_SD_SHIPMENT_HEAD.HeadDisc_PerAmt,TSPL_SD_SHIPMENT_HEAD.cust_po_date,TSPL_SD_SHIPMENT_HEAD.Cust_PO_No,TSPL_SD_SHIPMENT_HEAD.Vehicle_Code,TSPL_SD_SHIPMENT_HEAD.price_group_code,TSPL_SD_SHIPMENT_HEAD.Invoice_Type,TSPL_SD_SHIPMENT_HEAD.HeadDisc_Per,TSPL_SD_SHIPMENT_HEAD.HeadDisc_Amt,TSPL_SD_SHIPMENT_HEAD.TotCashDiscAmt,TSPL_SD_SHIPMENT_HEAD.Route_No,TSPL_SD_SHIPMENT_HEAD.Route_Desc,TSPL_SD_SHIPMENT_HEAD.Price_Code,TSPL_SD_SHIPMENT_HEAD.Document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SHIPMENT_HEAD.Status,TSPL_SD_SHIPMENT_HEAD.On_Hold,TSPL_SD_SHIPMENT_HEAD.Ref_No,TSPL_SD_SHIPMENT_HEAD.Description,TSPL_SD_SHIPMENT_HEAD.Remarks,TSPL_SD_SHIPMENT_HEAD.Tax_Group,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SHIPMENT_HEAD.Ship_To_Location,TSPL_SD_SHIPMENT_HEAD.TAX1,TSPL_SD_SHIPMENT_HEAD.TAX1_Rate,TSPL_SD_SHIPMENT_HEAD.TAX1_Amt,TSPL_SD_SHIPMENT_HEAD.TAX1_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX2,TSPL_SD_SHIPMENT_HEAD.TAX2_Rate,TSPL_SD_SHIPMENT_HEAD.TAX2_Amt,TSPL_SD_SHIPMENT_HEAD.TAX2_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX3,TSPL_SD_SHIPMENT_HEAD.TAX3_Rate,TSPL_SD_SHIPMENT_HEAD.TAX3_Amt,TSPL_SD_SHIPMENT_HEAD.TAX3_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX4,TSPL_SD_SHIPMENT_HEAD.TAX4_Rate,TSPL_SD_SHIPMENT_HEAD.TAX4_Amt,TSPL_SD_SHIPMENT_HEAD.TAX4_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX5,TSPL_SD_SHIPMENT_HEAD.TAX5_Rate,TSPL_SD_SHIPMENT_HEAD.TAX5_Amt,TSPL_SD_SHIPMENT_HEAD.TAX5_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX6,TSPL_SD_SHIPMENT_HEAD.TAX6_Rate,TSPL_SD_SHIPMENT_HEAD.TAX6_Amt,TSPL_SD_SHIPMENT_HEAD.TAX6_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX7,TSPL_SD_SHIPMENT_HEAD.TAX7_Rate,TSPL_SD_SHIPMENT_HEAD.TAX7_Amt,TSPL_SD_SHIPMENT_HEAD.TAX7_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX8,TSPL_SD_SHIPMENT_HEAD.TAX8_Rate,TSPL_SD_SHIPMENT_HEAD.TAX8_Amt,TSPL_SD_SHIPMENT_HEAD.TAX8_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX9,TSPL_SD_SHIPMENT_HEAD.TAX9_Rate,TSPL_SD_SHIPMENT_HEAD.TAX9_Amt,TSPL_SD_SHIPMENT_HEAD.TAX9_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX10,TSPL_SD_SHIPMENT_HEAD.TAX10_Rate,TSPL_SD_SHIPMENT_HEAD.TAX10_Amt,TSPL_SD_SHIPMENT_HEAD.TAX10_Base_Amt,TSPL_SD_SHIPMENT_HEAD.Discount_Base,TSPL_SD_SHIPMENT_HEAD.Discount_Amt,TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount,TSPL_SD_SHIPMENT_HEAD.Total_Tax_Amt,TSPL_SD_SHIPMENT_HEAD.Comments,TSPL_SD_SHIPMENT_HEAD.Comp_Code,TSPL_SD_SHIPMENT_HEAD.Terms_Code,TSPL_SD_SHIPMENT_HEAD.Due_Date ,TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_SD_SHIPMENT_HEAD.Posting_Date,TSPL_SD_SHIPMENT_HEAD.Total_Amt,TSPL_SD_SHIPMENT_HEAD.transporter_name,TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.VehicleNo,TSPL_SD_SHIPMENT_HEAD.GRNo,TSPL_SD_SHIPMENT_HEAD.GENo,TSPL_SD_SHIPMENT_HEAD.GEDate, TSPL_SD_SHIPMENT_HEAD.Dept,TSPL_SD_SHIPMENT_HEAD.Dept_Desc,TSPL_SD_SHIPMENT_HEAD.Item_Type,TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order ,TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code1,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name1,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt1,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code2,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name2,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt2,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code3,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name3,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt3,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code4,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name4,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt4,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code5,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name5,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt5,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code6,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name6,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt6,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code7,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name7,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt7,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code8,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name8,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt8,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code9 ,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name9,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt9 ,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code10 ,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name10,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt10,TSPL_SD_SHIPMENT_HEAD.Total_Add_Charge,TSPL_SD_SHIPMENT_HEAD.Tax_Calculation_Type,TSPL_SD_SHIPMENT_HEAD.Challan_No, TSPL_SD_SHIPMENT_HEAD.Challan_Date, TSPL_SD_SHIPMENT_HEAD.Inv_Date,TSPL_SD_SHIPMENT_HEAD.Inv_No,TSPL_SD_SHIPMENT_HEAD.Is_Internal,TSPL_SD_SHIPMENT_HEAD.Is_Create_Auto_Invoice,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Is_Create_Auto_Receipt,TSPL_SD_SHIPMENT_HEAD.Salesman_Code ,TSPL_SD_SHIPMENT_HEAD.Salesman_Name,  "
        qry += " TSPL_SD_SHIPMENT_HEAD.CURRENCY_CODE,TSPL_SD_SHIPMENT_HEAD.CONVRATE,TSPL_SD_SHIPMENT_HEAD.APPLICABLEFROM,TSPL_SD_SHIPMENT_HEAD.PRoject_ID ,TSPL_SD_SHIPMENT_HEAD.Mannual_Invoice_No,TSPL_SD_SHIPMENT_HEAD. Mannual_Invoice_No_StringType,TSPL_SD_SHIPMENT_HEAD.Form_38_No"
        qry += "  FROM TSPL_SD_SHIPMENT_HEAD"
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_HEAD.Bill_To_Location "
        qry += " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SD_SHIPMENT_HEAD.Ship_To_Location "
        qry += " left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_SD_SHIPMENT_HEAD.Tax_Group "
        qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_SD_SHIPMENT_HEAD.Terms_Code "
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code where 2=2"
        Dim whrCls As String = ""
        '-------richa 30/07/2014 Ticket No. BM00000003242---------  
        Dim strwherecls As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(NavType).ToUpper(), "CURRENT") <> CompairStringResult.Equal Then
            strwherecls = FrmMainTranScreen.CustomerPermission()
        End If
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrCls = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrCls = " AND TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + strwherecls + ")"
        End If
        '-----------------------------------------------------

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_SD_SHIPMENT_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_SHIPMENT_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = (select Min(Document_Code) from TSPL_SD_SHIPMENT_HEAD where Document_Code>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_SHIPMENT_HEAD where Document_Code<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSNShipmentHead()
            If dt.Rows(0)("EWayBillDate") IsNot DBNull.Value Then
                obj.EWayBillDate = clsCommon.myCDate(dt.Rows(0)("EWayBillDate"))
            End If
            obj.Electronic_Ref_No = clsCommon.myCstr(dt.Rows(0)("Electronic_Ref_No"))
            obj.EWayBillNo = clsCommon.myCstr(dt.Rows(0)("EWayBillNo"))
            obj.Is_Delivered = clsCommon.myCdbl(dt.Rows(0)("Is_Delivered"))
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Is_Internal = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Internal")) = 1, True, False)
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Bill_To_Location = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
            obj.Ship_To_Location = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
            obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Base_Amt"))
            obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
            obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
            obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Base_Amt"))
            obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Base_Amt"))
            obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
            obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
            obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Base_Amt"))
            obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
            obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
            obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
            obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Base_Amt"))
            obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
            obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
            obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
            obj.TAX6_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Base_Amt"))
            obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
            obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
            obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
            obj.TAX7_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Base_Amt"))
            obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
            obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
            obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
            obj.TAX8_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Base_Amt"))
            obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
            obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
            obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
            obj.TAX9_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Base_Amt"))
            obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
            obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
            obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
            obj.TAX10_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Base_Amt"))
            obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))
            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            obj.Discount_Base = clsCommon.myCdbl(dt.Rows(0)("Discount_Base"))
            obj.Discount_Amt = clsCommon.myCdbl(dt.Rows(0)("Discount_Amt"))
            obj.Amount_Less_Discount = clsCommon.myCdbl(dt.Rows(0)("Amount_Less_Discount"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.PROJECT_ID = clsCommon.myCstr(dt.Rows(0)("PROJECT_ID"))

            If dt.Rows(0)("Due_Date") IsNot DBNull.Value Then
                obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            End If
            obj.is_taxable = clsCommon.myCdbl(dt.Rows(0)("is_taxable"))
            '-----------------richa 26/06/2014 Ticket No .BM00000002982----------------------------
            obj.InvoiceManualNowithPrefix = clsCommon.myCstr(dt.Rows(0)("Mannual_Invoice_No_StringType"))
            obj.Mannual_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Mannual_Invoice_No"))

            '--------------------------------------------------------------------------------------

            obj.BillToLocationName = clsCommon.myCstr(dt.Rows(0)("BillToLocationName"))
            obj.ShipToLocationName = clsCommon.myCstr(dt.Rows(0)("ShipToLocationName"))
            obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
            obj.TermsName = clsCommon.myCstr(dt.Rows(0)("TermsName"))

            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If


            obj.Challan_No = clsCommon.myCdbl(dt.Rows(0)("Challan_No"))
            obj.Carrier = clsCommon.myCstr(dt.Rows(0)("transporter_name"))
            obj.transport_id = clsCommon.myCstr(dt.Rows(0)("Transport_Id"))
            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
            'obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.GRNo = clsCommon.myCstr(dt.Rows(0)("GRNo"))
            obj.GENo = clsCommon.myCstr(dt.Rows(0)("GENo"))
            If dt.Rows(0)("GEDate") IsNot DBNull.Value Then
                obj.GEDate = clsCommon.myCDate(dt.Rows(0)("GEDate"))
            End If
            obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
            obj.Against_Sales_Order = clsCommon.myCstr(dt.Rows(0)("Against_Sales_Order"))
            obj.Add_Charge_Code1 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code1"))
            obj.Add_Charge_Name1 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name1"))
            obj.Add_Charge_Amt1 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt1"))

            obj.Add_Charge_Code2 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code2"))
            obj.Add_Charge_Name2 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name2"))
            obj.Add_Charge_Amt2 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt2"))

            obj.Add_Charge_Code3 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code3"))
            obj.Add_Charge_Name3 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name3"))
            obj.Add_Charge_Amt3 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt3"))

            obj.Add_Charge_Code4 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code4"))
            obj.Add_Charge_Name4 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name4"))
            obj.Add_Charge_Amt4 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt4"))

            obj.Add_Charge_Code5 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code5"))
            obj.Add_Charge_Name5 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name5"))
            obj.Add_Charge_Amt5 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt5"))

            obj.Add_Charge_Code6 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code6"))
            obj.Add_Charge_Name6 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name6"))
            obj.Add_Charge_Amt6 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt6"))

            obj.Add_Charge_Code7 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code7"))
            obj.Add_Charge_Name7 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name7"))
            obj.Add_Charge_Amt7 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt7"))

            obj.Add_Charge_Code8 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code8"))
            obj.Add_Charge_Name8 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name8"))
            obj.Add_Charge_Amt8 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt8"))

            obj.Add_Charge_Code9 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code9"))
            obj.Add_Charge_Name9 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name9"))
            obj.Add_Charge_Amt9 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt9"))

            obj.Add_Charge_Code10 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code10"))
            obj.Add_Charge_Name10 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name10"))
            obj.Add_Charge_Amt10 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt10"))

            obj.Total_Add_Charge = clsCommon.myCdbl(dt.Rows(0)("Total_Add_Charge"))
            obj.Inv_No = clsCommon.myCstr(dt.Rows(0)("Inv_No"))
            If dt.Rows(0)("Challan_Date") IsNot DBNull.Value Then

                obj.Challan_Date = clsCommon.GetPrintDate((dt.Rows(0)("Challan_Date")), "dd/MMM/yyyy")
            End If

            If dt.Rows(0)("Inv_Date") IsNot DBNull.Value Then
                obj.Inv_Date = clsCommon.GetPrintDate((dt.Rows(0)("Inv_Date")), "dd/MMM/yyyy")
            End If

            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.Is_Create_Auto_Invoice = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Create_Auto_Invoice")) = 1, True, False)
            obj.Sale_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Sale_Invoice_No"))
            obj.Is_Create_Auto_Receipt = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Create_Auto_Receipt")) = 1, True, False)

            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            obj.Salesman_Name = clsCommon.myCstr(dt.Rows(0)("Salesman_Name"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Route_Desc = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            obj.HeadDisc_Per = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_Per"))
            obj.HeadDisc_PerAmt = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_PerAmt"))
            obj.HeadDisc_Amt = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_Amt"))
            obj.TotCashDiscAmt = clsCommon.myCdbl(dt.Rows(0)("TotCashDiscAmt"))
            obj.Invoice_Type = clsCommon.myCstr(dt.Rows(0)("Invoice_Type"))
            obj.Price_Group_Code = clsCommon.myCstr(dt.Rows(0)("Price_Group_Code"))
            obj.Cust_PO_No = clsCommon.myCstr(dt.Rows(0)("Cust_PO_No"))
            If dt.Rows(0)("cust_po_date") IsNot DBNull.Value Then
                obj.Podate = clsCommon.myCDate(dt.Rows(0)("cust_po_date"))
            End If
            obj.Form_38_No = clsCommon.myCstr(dt.Rows(0)("Form_38_No"))
            obj.Mannual_Invoice_No = clsCommon.myCdbl(dt.Rows(0)("Mannual_Invoice_No"))
            'richa Ticket No.BM00000002982-------------
            obj.InvoiceManualNowithPrefix = clsCommon.myCstr(dt.Rows(0)("Mannual_Invoice_No_StringType"))
            '  ---------------------------------------------
            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If

            'obj.Credit_Limit = clsCommon.myCdbl(dt.Rows(0)("Credit_Limit"))
            'obj.Advance_Security = clsCommon.myCdbl(dt.Rows(0)("Advance_Security"))
            'obj.Revese_Adv_Security = clsCommon.myCdbl(dt.Rows(0)("Revese_Adv_Security"))
            'obj.AR_Credit_Security = clsCommon.myCdbl(dt.Rows(0)("AR_Credit_Security"))
            'Credit_Limit,Advance_Security,Revese_Adv_Security,AR_Credit_Security,UnPostedDispatch,Ledger_Outstansing,Refund_Security,Reverse_Refund_Sec,Total_Outstanding
            'obj.UnPostedShipment = clsCommon.myCdbl(dt.Rows(0)("UnPostedDispatch"))
            'obj.Ledger_Outstansing = clsCommon.myCdbl(dt.Rows(0)("Ledger_Outstansing"))
            'obj.Refund_Security = clsCommon.myCdbl(dt.Rows(0)("Refund_Security"))
            'obj.Reverse_Refund_Sec = clsCommon.myCdbl(dt.Rows(0)("Reverse_Refund_Sec"))
            obj.Total_Outstanding = clsCommon.myCdbl(dt.Rows(0)("Total_Outstanding"))
            obj.Order_Qty = clsCommon.myCdbl(dt.Rows(0)("Order_Qty"))
            obj.Weighment_Code = clsCommon.myCstr(dt.Rows(0)("Weighment_Code"))
            obj.Beejak_No = clsCommon.myCstr(dt.Rows(0)("Beejak_No"))
            obj.LR_GR_NO = clsCommon.myCstr(dt.Rows(0)("LR_GR_NO"))
            If dt.Rows(0)("LR_GR_Date") IsNot DBNull.Value Then
                obj.LR_GR_Date = clsCommon.myCDate(dt.Rows(0)("LR_GR_Date"))
            End If
            obj.SHIP_TO_DELIVERY_AT = clsCommon.myCstr(dt.Rows(0)("SHIP_TO_DELIVERY_AT"))

            '' END CURRENCYCONVERSION 
            obj.Invoice_No = clsDBFuncationality.getSingleValue("Select Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" & obj.Document_Code & "' ", trans)

            qry = "SELECT  TSPL_SD_SHIPMENT_DETAIL.ItemwiseTaxCode,TSPL_SD_SHIPMENT_DETAIL.Is_Mannual_Amt,TSPL_SD_SHIPMENT_DETAIL.Document_Code,TSPL_SD_SHIPMENT_DETAIL.Line_No, " &
            "TSPL_SD_SHIPMENT_DETAIL.Status,TSPL_SD_SHIPMENT_DETAIL.Row_Type,TSPL_SD_SHIPMENT_DETAIL.status,TSPL_SD_SHIPMENT_DETAIL.Item_Code, " &
            "TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SHIPMENT_DETAIL.Qty,TSPL_SD_SHIPMENT_DETAIL.Free_Qty,TSPL_SD_SHIPMENT_DETAIL.Order_Code, " &
            "TSPL_SD_SHIPMENT_DETAIL.Order_Code,TSPL_SD_SHIPMENT_DETAIL.Balance_Qty,TSPL_SD_SHIPMENT_DETAIL.Unit_code, " &
            "TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_DETAIL.Item_Cost,TSPL_SD_SHIPMENT_DETAIL.TAX1,TSPL_SD_SHIPMENT_DETAIL.TAX1_Rate, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX2,TSPL_SD_SHIPMENT_DETAIL.TAX2_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX3,TSPL_SD_SHIPMENT_DETAIL.TAX3_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX4 , " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX4_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX5,TSPL_SD_SHIPMENT_DETAIL.TAX5_Rate , " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX6,TSPL_SD_SHIPMENT_DETAIL.TAX6_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX7,TSPL_SD_SHIPMENT_DETAIL.TAX7_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX8, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX8_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX9,TSPL_SD_SHIPMENT_DETAIL.TAX9_Rate, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX10,TSPL_SD_SHIPMENT_DETAIL.TAX10_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt, " &
            "TSPL_SD_SHIPMENT_DETAIL.Amount,TSPL_SD_SHIPMENT_DETAIL.Disc_Per,TSPL_SD_SHIPMENT_DETAIL.Disc_Amt,TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount, " &
            "TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt,TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt,TSPL_LOCATION_MASTER.Location_Desc as LocationName, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX1_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX2_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX3_Base_Amt , " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX4_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX5_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX6_Base_Amt, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX7_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX8_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX9_Base_Amt, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX10_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.MRP,TSPL_SD_SHIPMENT_DETAIL.Batch_No,TSPL_SD_SHIPMENT_DETAIL.MFG_Date, " &
            "TSPL_SD_SHIPMENT_DETAIL.Expiry_Date,TSPL_SD_SHIPMENT_DETAIL.Specification,TSPL_SD_SHIPMENT_DETAIL.Remarks,TSPL_SD_SHIPMENT_DETAIL.Assessable, " &
            "TSPL_SD_SHIPMENT_DETAIL.AssessableAmt,TSPL_SD_SHIPMENT_DETAIL.Bar_Code, " &
            "TSPL_SD_SHIPMENT_DETAIL.Scheme_Applicable,TSPL_SD_SHIPMENT_DETAIL.Scheme_Code, " &
            "TSPL_SD_SHIPMENT_DETAIL.Scheme_Item,TSPL_SD_SHIPMENT_DETAIL.Item_Tax,TSPL_SD_SHIPMENT_DETAIL.Total_MRP_Amt, " &
            "TSPL_SD_SHIPMENT_DETAIL.Total_Basic_Amt,TSPL_SD_SHIPMENT_DETAIL.Total_Disc_Amt,TSPL_SD_SHIPMENT_DETAIL.Cust_Discount, " &
            "TSPL_SD_SHIPMENT_DETAIL.Total_Cust_Discount,TSPL_SD_SHIPMENT_DETAIL.ActualRate,TSPL_SD_SHIPMENT_DETAIL.Cust_DiscountQty, " &
            "TSPL_SD_SHIPMENT_DETAIL.Price_code,TSPL_SD_SHIPMENT_DETAIL.Abatement_Per,TSPL_SD_SHIPMENT_DETAIL.Abatement_Amt, " &
            "TSPL_SD_SHIPMENT_DETAIL.FOC_Item,TSPL_SD_SHIPMENT_DETAIL.Item_Weight,TSPL_SD_SHIPMENT_DETAIL.Price_Date, " &
            "TSPL_SD_SHIPMENT_DETAIL.HeadDiscPer,TSPL_SD_SHIPMENT_DETAIL.HeadDiscPerAmt,TSPL_SD_SHIPMENT_DETAIL.Bin_No,TSPL_SD_SHIPMENT_DETAIL.TotalItem_Weight,TSPL_SD_SHIPMENT_DETAIL.Conv_Factor,TSPL_SD_SHIPMENT_DETAIL.Purchase_Cost,TSPL_SD_SHIPMENT_DETAIL.OrgRate,  " &
            "TSPL_SD_SHIPMENT_DETAIL.vendor_code,TSPL_SD_SHIPMENT_DETAIL.vendor_desc,TSPL_SD_SHIPMENT_DETAIL.PrincipleCode,TSPL_SD_SHIPMENT_DETAIL.PrincipleDesc,TSPL_SD_SHIPMENT_DETAIL.Markup_On,TSPL_SD_SHIPMENT_DETAIL.Markup_Percent,TSPL_SD_SHIPMENT_DETAIL.Landing_Cost,TSPL_SD_SHIPMENT_DETAIL.HeadDiscAmt,TSPL_SD_SHIPMENT_DETAIL.CustDiscPer,TSPL_SD_SHIPMENT_DETAIL.CasdDiscScheme_Code "
            qry += " FROM TSPL_SD_SHIPMENT_DETAIL "
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_DETAIL.Location "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code"
            qry += " where TSPL_SD_SHIPMENT_DETAIL.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_SD_SHIPMENT_DETAIL.Line_No  asc"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsSNShipmentDetail)
                Dim objTr As clsSNShipmentDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsSNShipmentDetail
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Bar_Code = clsCommon.myCstr(dr("Bar_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))


                    objTr.Free_Qty = clsCommon.myCdbl(dr("Free_Qty"))
                    objTr.Order_Code = clsCommon.myCstr(dr("Order_Code"))

                    objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Location = clsCommon.myCstr(dr("Location"))
                    objTr.LocationName = clsCommon.myCstr(dr("LocationName"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                    objTr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objTr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objTr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objTr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    objTr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objTr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                    objTr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    objTr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objTr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                    objTr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    objTr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objTr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                    objTr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    objTr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    objTr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                    objTr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                    objTr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    objTr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    objTr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                    objTr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                    objTr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    objTr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    objTr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                    objTr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                    objTr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    objTr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    objTr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                    objTr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                    objTr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    objTr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                    objTr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    objTr.TAX10 = clsCommon.myCstr(dr("TAX10"))
                    objTr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))
                    objTr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                    objTr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Disc_Per = clsCommon.myCdbl(dr("Disc_Per"))
                    objTr.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))
                    objTr.Amt_Less_Discount = clsCommon.myCdbl(dr("Amt_Less_Discount"))
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))


                    objTr.Is_Mannual_Amt = clsCommon.myCdbl(dr("Is_Mannual_Amt"))

                    ' ''objTr.Landed_Cost_Rate = clsCommon.myCdbl(dr("Landed_Cost_Rate"))
                    ' ''objTr.Landed_Cost_Amount = clsCommon.myCdbl(dr("Landed_Cost_Amount"))

                    objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.Assessable = clsCommon.myCdbl(dr("Assessable"))
                    objTr.AssessableAmt = clsCommon.myCdbl(dr("AssessableAmt"))
                    objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                    If dr("MFG_Date") IsNot DBNull.Value Then
                        objTr.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                    End If
                    If dr("Expiry_Date") IsNot DBNull.Value Then
                        objTr.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                    End If
                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    'objTr.Unit_Cogs = clsCommon.myCdbl(dr("Unit_Cogs"))

                    objTr.Scheme_Applicable = clsCommon.myCstr(dr("Scheme_Applicable"))
                    objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                    objTr.Scheme_Item = clsCommon.myCstr(dr("Scheme_Item"))
                    objTr.Item_Tax = clsCommon.myCdbl(dr("Item_Tax"))
                    objTr.Total_MRP_Amt = clsCommon.myCdbl(dr("Total_MRP_Amt"))
                    objTr.Total_Basic_Amt = clsCommon.myCdbl(dr("Total_Basic_Amt"))
                    objTr.Total_Disc_Amt = clsCommon.myCdbl(dr("Total_Disc_Amt"))
                    objTr.Cust_Discount = clsCommon.myCdbl(dr("Cust_Discount"))
                    objTr.Total_Cust_Discount = clsCommon.myCdbl(dr("Total_Cust_Discount"))
                    objTr.ActualRate = clsCommon.myCdbl(dr("ActualRate"))
                    objTr.Cust_DiscountQty = clsCommon.myCdbl(dr("Cust_DiscountQty"))
                    objTr.Price_code = clsCommon.myCstr(dr("Price_code"))
                    If dr("Price_Date") IsNot DBNull.Value Then
                        objTr.Price_Date = clsCommon.myCDate(dr("Price_Date"))
                    End If

                    objTr.Abatement_Per = clsCommon.myCdbl(dr("Abatement_Per"))
                    objTr.Abatement_Amt = clsCommon.myCdbl(dr("Abatement_Amt"))
                    objTr.FOC_Item = clsCommon.myCdbl(dr("FOC_Item"))
                    objTr.Markup_On = clsCommon.myCstr(dr("Markup_On"))
                    objTr.Markup_Percent = clsCommon.myCdbl(dr("Markup_Percent"))
                    objTr.Landing_Cost = clsCommon.myCdbl(dr("Landing_Cost"))
                    objTr.HeadDiscAmt = clsCommon.myCdbl(dr("HeadDiscAmt"))
                    objTr.CustDiscPer = clsCommon.myCdbl(dr("CustDiscPer"))
                    objTr.CasdDiscScheme_Code = clsCommon.myCstr(dr("CasdDiscScheme_Code"))
                    objTr.Item_Weight = clsCommon.myCdbl(dr("Item_Weight"))
                    objTr.TotalItem_Weight = clsCommon.myCdbl(dr("TotalItem_Weight"))
                    objTr.Purchase_Cost = clsCommon.myCdbl(dr("Purchase_Cost"))
                    objTr.OrgRate = clsCommon.myCdbl(dr("OrgRate"))
                    objTr.Conv_Factor = clsCommon.myCdbl(dr("Conv_Factor"))
                    objTr.PrincipleCode = clsCommon.myCstr(dr("PrincipleCode"))
                    objTr.PrincipleDesc = clsCommon.myCstr(dr("PrincipleDesc"))
                    objTr.vendor_code = clsCommon.myCstr(dr("vendor_code"))
                    objTr.vendor_desc = clsCommon.myCstr(dr("vendor_desc"))
                    objTr.Bin_No = clsCommon.myCstr(dr("Bin_No"))
                    objTr.HeadDiscPer = clsCommon.myCdbl(dr("HeadDiscPer"))
                    objTr.HeadDiscPerAmt = clsCommon.myCdbl(dr("HeadDiscPerAmt"))
                    objTr.arrSrItem = clsSerializeInvenotry.GetData("SD-IN", objTr.Document_Code, objTr.Item_Code, objTr.Line_No, trans)
                    objTr.ItemwiseTaxCode = clsCommon.myCstr(dr("ItemwiseTaxCode"))
                    obj.Arr.Add(objTr)
                Next
            End If

            qry = "select Weighment_Code FROM TSPL_SD_SHIPMENT_WEIGHMENT_MAPPING where TSPL_SD_SHIPMENT_WEIGHMENT_MAPPING.Document_Code='" + obj.Document_Code + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrWeighmentList = New ArrayList()
                For Each dr As DataRow In dt.Rows
                    obj.ArrWeighmentList.Add(clsCommon.myCstr(dr("Weighment_Code")))
                Next
            End If

            qry = "select TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.DCS_Code,TSPL_DCS_FOR_SALE.Uploader_No,TSPL_DCS_FOR_SALE.Name,TSPL_DCS_FOR_SALE.Zone as DCS_Zone
,TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.ICode,TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.Qty,TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.UOM,TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.FPKID,TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.Frieght_Rate,TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.Frieght_Amt,(select TSPL_DCS_FOR_SALE_FRIEGHT_DETAIL.UOM_Code from TSPL_DCS_FOR_SALE_FRIEGHT_DETAIL where PK_ID in(TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.FPKID)) as Frieght_UOM from TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL 
left outer join TSPL_DCS_FOR_SALE on TSPL_DCS_FOR_SALE.Code=TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.DCS_Code
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.ICode
where DOCUMENT_CODE='" + obj.Document_Code + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrDCSItem = New List(Of clsSNShipmentDCSItemDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objTRDID As New clsSNShipmentDCSItemDetail
                    objTRDID.DCS_Code = clsCommon.myCstr(dr("DCS_Code"))
                    objTRDID.UploaderNo = clsCommon.myCstr(dr("Uploader_No"))
                    objTRDID.DCSName = clsCommon.myCstr(dr("Name"))
                    objTRDID.DCSZone = clsCommon.myCstr(dr("DCS_Zone"))
                    objTRDID.ICode = clsCommon.myCstr(dr("ICode"))
                    objTRDID.IName = clsCommon.myCstr(dr("Item_Desc"))
                    objTRDID.Qty = clsCommon.myCstr(dr("Qty"))
                    objTRDID.UOM = clsCommon.myCstr(dr("UOM"))
                    objTRDID.FPKID = clsCommon.myCstr(dr("FPKID"))
                    objTRDID.Frieght_UOM = clsCommon.myCstr(dr("Frieght_UOM"))
                    objTRDID.Frieght_Rate = clsCommon.myCdbl(dr("Frieght_Rate"))
                    objTRDID.Frieght_Amt = clsCommon.myCdbl(dr("Frieght_Amt"))
                    obj.ArrDCSItem.Add(objTRDID)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function GetOriginalQty(ByVal strMrnNo As String, ByVal strICode As String, ByVal strUOM As String, ByVal dblAssessable As Double, ByVal dblMRP As Double, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = "Select TSPL_MRN_DETAIL.MRN_No,(TSPL_MRN_DETAIL.MRN_Qty+ISNULL(TSPL_MRN_DETAIL.Leak_Qty,0) +ISNULL(TSPL_MRN_DETAIL.Burst_Qty,0)+ISNULL(TSPL_MRN_DETAIL.Short_Qty,0)) as MRN_Qty,TSPL_GRN_DETAIL.GRN_No,(TSPL_GRN_DETAIL.GRN_Qty+ISNULL(TSPL_GRN_DETAIL.Leak_Qty,0) +ISNULL(TSPL_GRN_DETAIL.Burst_Qty,0)+ISNULL(TSPL_GRN_DETAIL.Short_Qty,0)) as GRN_Qty, TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty from TSPL_MRN_DETAIL left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_MRN_DETAIL.GRN_Id and TSPL_GRN_DETAIL.Item_Code=TSPL_MRN_DETAIL.Item_Code and TSPL_GRN_DETAIL.Unit_code=TSPL_MRN_DETAIL.Unit_code and isnull(TSPL_GRN_DETAIL.Assessable,0)=isnull(TSPL_MRN_DETAIL.Assessable,0) and isnull(TSPL_GRN_DETAIL.Item_Code,0)=isnull(TSPL_MRN_DETAIL.Item_Code ,0) left outer join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No= TSPL_GRN_DETAIL.PO_Id and TSPL_PURCHASE_ORDER_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code and TSPL_PURCHASE_ORDER_DETAIL.Unit_code=  TSPL_GRN_DETAIL.Unit_code and isnull(TSPL_PURCHASE_ORDER_DETAIL.Assessable,0)=  isnull(TSPL_GRN_DETAIL.Assessable,0) and isnull(TSPL_PURCHASE_ORDER_DETAIL.MRP,0)=  isnull(TSPL_GRN_DETAIL.MRP,0) where TSPL_MRN_DETAIL.MRN_No='" + strMrnNo + "' and TSPL_MRN_DETAIL.Item_Code='" + strICode + "' and TSPL_MRN_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_MRN_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_MRN_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'"
        Return clsDBFuncationality.GetDataTable(qry, trans)
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal isCreateAutoInvoice As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isCreateAutoInvoice Then
                Dim qry As String = "Update TSPL_SD_SHIPMENT_HEAD set Is_Create_Auto_Invoice=1 where Document_Code='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            Dim strEx As String = ex.Message
            Dim qry As String = "select IRN_No,qr_code,ack_no,ack_date,EWayBillNo, EwayBillDate,EwayBillValidDate,EWayBillRemarks from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" + strDocNo + "'"
            Dim dtPortalInfo As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            trans.Rollback()
            Try
                If dtPortalInfo IsNot Nothing AndAlso dtPortalInfo.Rows.Count > 0 Then
                    Dim coll As New Hashtable()
                    If clsCommon.myLen(dtPortalInfo.Rows(0)("IRN_No")) > 0 Then
                        clsCommon.AddColumnsForChange(coll, "IRN_No", clsCommon.myCstr(dtPortalInfo.Rows(0)("IRN_No")))
                        clsCommon.AddColumnsForChange(coll, "qr_code", clsCommon.myCstr(dtPortalInfo.Rows(0)("qr_code")))
                        clsCommon.AddColumnsForChange(coll, "ack_no", dtPortalInfo.Rows(0)("ack_no"))
                        If dtPortalInfo.Rows(0)("ack_date") IsNot DBNull.Value Then
                            clsCommon.AddColumnsForChange(coll, "ack_date", clsCommon.GetPrintDate(clsCommon.myCDate(dtPortalInfo.Rows(0)("ack_date")), "dd/MMM/yyyy hh:mm:ss tt"))
                        End If
                    End If

                    If clsCommon.myLen(dtPortalInfo.Rows(0)("EWayBillNo")) > 0 Then
                        clsCommon.AddColumnsForChange(coll, "EWayBillNo", clsCommon.myCstr(dtPortalInfo.Rows(0)("EWayBillNo")))
                        If dtPortalInfo.Rows(0)("EwayBillDate") IsNot DBNull.Value Then
                            clsCommon.AddColumnsForChange(coll, "EwayBillDate", clsCommon.GetPrintDate(clsCommon.myCDate(dtPortalInfo.Rows(0)("EwayBillDate")), "dd/MMM/yyyy hh:mm:ss tt"))
                        End If
                        If dtPortalInfo.Rows(0)("EwayBillValidDate") IsNot DBNull.Value Then
                            clsCommon.AddColumnsForChange(coll, "EwayBillValidDate", clsCommon.GetPrintDate(clsCommon.myCDate(dtPortalInfo.Rows(0)("EwayBillValidDate")), "dd/MMM/yyyy hh:mm:ss tt"))
                        End If
                        clsCommon.AddColumnsForChange(coll, "EWayBillRemarks", clsCommon.myCstr(dtPortalInfo.Rows(0)("EWayBillRemarks")))
                    End If
                    If coll.Count > 0 Then
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_INVOICE_HEAD", OMInsertOrUpdate.Update, "Against_Shipment_No='" + strDocNo + "'")
                    End If
                End If
            Catch ex2 As Exception
                strEx += Environment.NewLine + "Portal Info [" + ex2.Message + "]"
            End Try
            Try
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Error_Msg", strEx)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_INVOICE_EXCEPTION", OMInsertOrUpdate.Insert, "")
            Catch ex1 As Exception
            End Try
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoRecreatedOnly As String = Nothing) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("SRN No not found to Post")
            End If
            Dim obj As clsSNShipmentHead = clsSNShipmentHead.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            '' Anubhooti 06-Sep-2014 BM00000003735 (Locked Transaction)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Shipment/Sale Invoice", obj.Bill_To_Location, obj.Document_Date, trans)
            ''
            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("SRN No " + obj.Document_Code + " Is On Hold.Can't Post it")
            End If
            Dim qry As String = ""

            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_SD_SHIPMENT_HEAD", "Document_Code", obj.Document_Code, trans)
            If isResult = False Then
                Return False
            End If
            HitInventory(obj, trans)
            obj.Sale_Invoice_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from  TSPL_SD_SALE_INVOICE_HEad where Against_Shipment_No='" + obj.Document_Code + "'", trans))
            CreateJournalEntry(obj.Document_Code, trans, strVoucherNoRecreatedOnly)

            qry = "Update TSPL_SD_SHIPMENT_HEAD set Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "',Sale_Invoice_No ='" + obj.Sale_Invoice_No + "' "
            qry += " where Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If obj.Is_Create_Auto_Invoice Then
                If clsCommon.myLen(obj.Sale_Invoice_No) <= 0 Then
                    Dim objSI As clsSNInvoiceHead = ConvertShipmentToSaleInvoice(obj)
                    objSI.SaveData(objSI, True, trans)
                    obj.Sale_Invoice_No = objSI.Document_Code
                End If
                clsSNInvoiceHead.PostData("", obj.Sale_Invoice_No, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function HitInventory(obj As clsSNShipmentHead, trans As SqlTransaction) As Boolean
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        For Each objTr As clsSNShipmentDetail In obj.Arr
            If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Dim strItemType As String = clsItemMaster.GetItemType(objTr.Item_Code, trans)
                Dim strItemTypeToSave As String = ""
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                Else
                    strItemTypeToSave = strItemType
                    'Throw New Exception("Item Type not found: " + strItemType)
                End If
                Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_code, trans)
                If ConvFac = 0 Then
                    Throw New Exception("Conversion Factor found zero for item :" + objTr.Item_Code + " and Uom:'" + objTr.Unit_code)
                End If

                Dim objInventoryMovemnt As New clsInventoryMovement()
                objInventoryMovemnt.InOut = "O"
                objInventoryMovemnt.Location_Code = objTr.Location

                objInventoryMovemnt.Cust_Code = obj.Customer_Code
                objInventoryMovemnt.Cust_Name = obj.Customer_Name

                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                objInventoryMovemnt.Qty = objTr.Qty + objTr.Free_Qty
                objInventoryMovemnt.UOM = objTr.Unit_code
                objInventoryMovemnt.Basic_Cost = objTr.Item_Cost
                objInventoryMovemnt.MRP = objTr.MRP
                objInventoryMovemnt.Add_Cost = objTr.Total_Tax_Amt
                objInventoryMovemnt.Net_Cost = objTr.Total_Tax_Amt
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "FT"
                End If
                objInventoryMovemnt.ItemType = strItemTypeToSave
                ArrInventoryMovement.Add(objInventoryMovemnt)
            End If
        Next
        clsInventoryMovement.SaveData("SD-SH", obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
        Return True
    End Function

    Public Shared Sub CreateJournalEntry(ByVal strCode As String, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoRecreatedOnly As String = Nothing)
        Dim obj As New clsSNShipmentHead
        obj = clsSNShipmentHead.GetData(strCode, NavigatorType.Current, trans)
        Dim ArryLstGLAC As ArrayList = New ArrayList()
        Dim strInventoryControlAc As String = ""
        Dim strShipmentClearingAC As String = ""
        Dim dblTotalCost As Double = 0

        strShipmentClearingAC = clsDBFuncationality.getSingleValue("SELECT PA.Shipment_Clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
          " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
           " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'", trans)
        strShipmentClearingAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentClearingAC, obj.Bill_To_Location, trans)

        If clsCommon.myLen(strShipmentClearingAC) = 0 Then
            Throw New Exception("Please set Shipment clearing Account for first item")
        End If

        Dim dblCogsCost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end) as COst from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Source_Doc_No='" & obj.Document_Code & "'", trans))

        Dim Acc() As String = {strShipmentClearingAC, dblCogsCost}
        ArryLstGLAC.Add(Acc)

        Dim strSql As String = "select TSPL_INVENTORY_MOVEMENT.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code  where Source_Doc_No='" & obj.Document_Code & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                strInventoryControlAc = clsDBFuncationality.getSingleValue("SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans)
                strInventoryControlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControlAc, obj.Bill_To_Location, trans)

                If clsCommon.myLen(strInventoryControlAc) = 0 Then
                    Throw New Exception("Please set Inventory Control Account for first item")
                End If
                Dim Acc1() As String = {strInventoryControlAc, -1 * clsCommon.myCdbl(dr("Cost"))}
                ArryLstGLAC.Add(Acc1)
            Next
        End If
        If strVoucherNoRecreatedOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoRecreatedOnly) > 0 Then
            clsJournalMaster.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, strVoucherNoRecreatedOnly, trans, obj.Document_Date, obj.Remarks, "SD-SH", "Shipment", obj.Document_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , obj.Description, obj.Remarks)
        Else
            clsJournalMaster.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, trans, obj.Document_Date, obj.Remarks, "SD-SH", "Shipment", obj.Document_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , obj.Description, obj.Remarks)
        End If
    End Sub

    Private Shared Function ConvertShipmentToSaleInvoice(ByVal objShipment As clsSNShipmentHead) As clsSNInvoiceHead
        Dim obj As New clsSNInvoiceHead()
        obj = New clsSNInvoiceHead()
        obj.podate = objShipment.Document_Date
        obj.Invoice_Type = objShipment.Invoice_Type
        'obj.Document_Code = objShipment.Document_Code
        obj.Document_Date = objShipment.Document_Date
        obj.Customer_Code = objShipment.Customer_Code
        obj.Customer_Name = objShipment.Customer_Name
        obj.Status = IIf(objShipment.Status = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        obj.On_Hold = IIf(objShipment.On_Hold = 1, True, False)
        obj.Is_Internal = IIf(objShipment.Is_Internal = 1, True, False)
        obj.is_taxable = objShipment.is_taxable ' Ticket No : ADV/17/07/19-000037 By Prabhakar 
        obj.Ref_No = objShipment.Ref_No
        obj.Description = objShipment.Description
        obj.Remarks = objShipment.Remarks
        obj.Bill_To_Location = objShipment.Bill_To_Location
        obj.Ship_To_Location = objShipment.Ship_To_Location
        obj.Tax_Group = objShipment.Tax_Group
        obj.TAX1 = objShipment.TAX1
        obj.TAX1_Rate = objShipment.TAX1_Rate
        obj.TAX1_Base_Amt = objShipment.TAX1_Base_Amt
        obj.TAX1_Amt = objShipment.TAX1_Amt
        obj.TAX2 = objShipment.TAX2
        obj.TAX2_Rate = objShipment.TAX2_Rate
        obj.TAX2_Base_Amt = objShipment.TAX2_Base_Amt
        obj.TAX2_Amt = objShipment.TAX2_Amt
        obj.TAX3 = objShipment.TAX3
        obj.TAX3_Base_Amt = objShipment.TAX3_Base_Amt
        obj.TAX3_Rate = objShipment.TAX3_Rate
        obj.TAX3_Amt = objShipment.TAX3_Amt
        obj.TAX4 = objShipment.TAX4
        obj.TAX4_Rate = objShipment.TAX4_Rate
        obj.TAX4_Base_Amt = objShipment.TAX4_Base_Amt
        obj.TAX4_Amt = objShipment.TAX4_Amt
        obj.TAX5 = objShipment.TAX5
        obj.TAX5_Rate = objShipment.TAX5_Rate
        obj.TAX5_Base_Amt = objShipment.TAX5_Base_Amt
        obj.TAX5_Amt = objShipment.TAX5_Amt
        obj.TAX6 = objShipment.TAX6
        obj.TAX6_Rate = objShipment.TAX6_Rate
        obj.TAX6_Base_Amt = objShipment.TAX6_Base_Amt
        obj.TAX6_Amt = objShipment.TAX6_Amt
        obj.TAX7 = objShipment.TAX7
        obj.TAX7_Rate = objShipment.TAX7_Rate
        obj.TAX7_Base_Amt = objShipment.TAX7_Base_Amt
        obj.TAX7_Amt = objShipment.TAX7_Amt
        obj.TAX8 = objShipment.TAX8
        obj.TAX8_Rate = objShipment.TAX8_Rate
        obj.TAX8_Base_Amt = objShipment.TAX8_Base_Amt
        obj.TAX8_Amt = objShipment.TAX8_Amt
        obj.TAX9 = objShipment.TAX9
        obj.TAX9_Rate = objShipment.TAX9_Rate
        obj.TAX9_Base_Amt = objShipment.TAX9_Base_Amt
        obj.TAX9_Amt = objShipment.TAX9_Amt
        obj.TAX10 = objShipment.TAX10
        obj.TAX10_Rate = objShipment.TAX10_Rate
        obj.TAX10_Base_Amt = objShipment.TAX10_Base_Amt
        obj.TAX10_Amt = objShipment.TAX10_Amt
        obj.Total_Tax_Amt = objShipment.Total_Tax_Amt
        obj.Discount_Base = objShipment.Discount_Base
        obj.Discount_Amt = objShipment.Discount_Amt
        obj.Amount_Less_Discount = objShipment.Amount_Less_Discount
        obj.Total_Amt = objShipment.Total_Amt
        obj.Comments = objShipment.Comments
        obj.Comp_Code = objShipment.Comp_Code
        obj.Terms_Code = objShipment.Terms_Code
        obj.Due_Date = objShipment.Due_Date
        obj.BillToLocationName = objShipment.BillToLocationName
        obj.ShipToLocationName = objShipment.ShipToLocationName
        obj.TaxGroupName = objShipment.TaxGroupName
        obj.TermsName = objShipment.TermsName
        obj.PROJECT_ID = objShipment.PROJECT_ID
        obj.Route_No = objShipment.Route_No
        obj.Route_Desc = objShipment.Route_Desc
        obj.Price_Code = objShipment.Price_Code
        obj.HeadDisc_Per = objShipment.HeadDisc_Per
        obj.HeadDisc_Amt = objShipment.HeadDisc_Amt
        obj.HeadDisc_PerAmt = objShipment.HeadDisc_PerAmt
        obj.TotCashDiscAmt = objShipment.TotCashDiscAmt
        obj.Cust_PO_No = objShipment.Cust_PO_No
        If objShipment.Podate Is Nothing Then
            obj.podate = objShipment.Document_Date
        Else
            obj.podate = objShipment.Podate
        End If




        If objShipment.Posting_Date IsNot Nothing Then
            obj.Posting_Date = objShipment.Posting_Date
        End If

        obj.Salesman_Code = objShipment.Salesman_Code
        obj.Salesman_Name = objShipment.Salesman_Name

        obj.Challan_No = objShipment.Challan_No
        obj.Carrier = objShipment.Carrier
        obj.transport_id = objShipment.transport_id
        'obj.Vehicle_Code = objShipment.Vehicle_Code
        obj.VehicleNo = objShipment.VehicleNo
        obj.GRNo = objShipment.GRNo
        obj.GENo = objShipment.GENo
        If objShipment.GEDate IsNot Nothing Then
            obj.GEDate = objShipment.GEDate
        End If




        obj.Dept = objShipment.Dept
        obj.Dept_Desc = objShipment.Dept_Desc
        obj.Item_Type = objShipment.Item_Type

        obj.Against_Shipment_No = objShipment.Document_Code


        obj.Add_Charge_Code1 = objShipment.Add_Charge_Code1
        obj.Add_Charge_Name1 = objShipment.Add_Charge_Name1
        obj.Add_Charge_Amt1 = objShipment.Add_Charge_Amt1

        obj.Add_Charge_Code2 = objShipment.Add_Charge_Code2
        obj.Add_Charge_Name2 = objShipment.Add_Charge_Name2
        obj.Add_Charge_Amt2 = objShipment.Add_Charge_Amt2

        obj.Add_Charge_Code3 = objShipment.Add_Charge_Code3
        obj.Add_Charge_Name3 = objShipment.Add_Charge_Name3
        obj.Add_Charge_Amt3 = objShipment.Add_Charge_Amt3

        obj.Add_Charge_Code4 = objShipment.Add_Charge_Code4
        obj.Add_Charge_Name4 = objShipment.Add_Charge_Name4
        obj.Add_Charge_Amt4 = objShipment.Add_Charge_Amt4

        obj.Add_Charge_Code5 = objShipment.Add_Charge_Code5
        obj.Add_Charge_Name5 = objShipment.Add_Charge_Name5
        obj.Add_Charge_Amt5 = objShipment.Add_Charge_Amt5

        obj.Add_Charge_Code6 = objShipment.Add_Charge_Code6
        obj.Add_Charge_Name6 = objShipment.Add_Charge_Name6
        obj.Add_Charge_Amt6 = objShipment.Add_Charge_Amt6

        obj.Add_Charge_Code7 = objShipment.Add_Charge_Code7
        obj.Add_Charge_Name7 = objShipment.Add_Charge_Name7
        obj.Add_Charge_Amt7 = objShipment.Add_Charge_Amt7

        obj.Add_Charge_Code8 = objShipment.Add_Charge_Code8
        obj.Add_Charge_Name8 = objShipment.Add_Charge_Name8
        obj.Add_Charge_Amt8 = objShipment.Add_Charge_Amt8

        obj.Add_Charge_Code9 = objShipment.Add_Charge_Code9
        obj.Add_Charge_Name9 = objShipment.Add_Charge_Name9
        obj.Add_Charge_Amt9 = objShipment.Add_Charge_Amt9

        obj.Add_Charge_Code10 = objShipment.Add_Charge_Code10
        obj.Add_Charge_Name10 = objShipment.Add_Charge_Name10
        obj.Add_Charge_Amt10 = objShipment.Add_Charge_Amt10
        obj.Invoice_Type = "R"
        obj.Total_Add_Charge = objShipment.Total_Add_Charge
        obj.Inv_No = objShipment.Inv_No
        If clsCommon.myLen(objShipment.Challan_Date) <= 0 Then
            obj.Challan_Date = ""
        Else
            obj.Challan_Date = clsCommon.GetPrintDate(objShipment.Challan_Date, "dd/MMM/yyyy")
        End If

        If clsCommon.myLen(objShipment.Inv_Date) <= 0 Then
            obj.Inv_Date = ""
        Else
            obj.Inv_Date = clsCommon.GetPrintDate(objShipment.Inv_Date, "dd/MMM/yyyy")
        End If

        obj.Tax_Calculation_Type = IIf(objShipment.Tax_Calculation_Type = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
        obj.Is_Create_Auto_Receipt = objShipment.Is_Create_Auto_Receipt
        '-----------------richa 27/06/2014 Ticket No .BM00000002982----------
        obj.Mannual_Document_Code = objShipment.Mannual_Invoice_No
        obj.InvoiceManualNowithPrefix = objShipment.InvoiceManualNowithPrefix
        '-------------------------------------------------------------------
        If (objShipment.Arr IsNot Nothing AndAlso objShipment.Arr.Count > 0) Then
            obj.Arr = New List(Of clsSNInvoiceDetail)
            Dim objTr As clsSNInvoiceDetail
            For Each objShipmentDetail As clsSNShipmentDetail In objShipment.Arr
                objTr = New clsSNInvoiceDetail
                objTr.PrincipleCode = objShipmentDetail.PrincipleCode
                objTr.PrincipleDesc = objShipmentDetail.PrincipleDesc
                objTr.vendor_code = objShipmentDetail.vendor_code
                objTr.vendor_desc = objShipmentDetail.vendor_desc
                objTr.Document_Code = objShipmentDetail.Document_Code
                objTr.Row_Type = objShipmentDetail.Row_Type
                objTr.Line_No = objShipmentDetail.Line_No
                objTr.Status = Convert.ToInt32(objShipmentDetail.Status)
                objTr.Item_Code = objShipmentDetail.Item_Code
                objTr.Item_Desc = objShipmentDetail.Item_Desc
                objTr.Qty = objShipmentDetail.Qty
                objTr.Free_Qty = objShipmentDetail.Free_Qty
                objTr.Shipment_Code = objShipment.Document_Code
                objTr.Balance_Qty = objShipmentDetail.Balance_Qty
                objTr.Unit_code = objShipmentDetail.Unit_code
                objTr.Location = objShipmentDetail.Location
                objTr.LocationName = objShipmentDetail.LocationName
                objTr.Item_Cost = objShipmentDetail.Item_Cost
                objTr.TAX1 = objShipmentDetail.TAX1
                objTr.TAX1_Base_Amt = objShipmentDetail.TAX1_Base_Amt
                objTr.TAX1_Rate = objShipmentDetail.TAX1_Rate
                objTr.TAX1_Amt = objShipmentDetail.TAX1_Amt
                objTr.TAX2 = objShipmentDetail.TAX2
                objTr.TAX2_Base_Amt = objShipmentDetail.TAX2_Base_Amt
                objTr.TAX2_Rate = objShipmentDetail.TAX2_Rate
                objTr.TAX2_Amt = objShipmentDetail.TAX2_Amt
                objTr.TAX3 = objShipmentDetail.TAX3
                objTr.TAX3_Base_Amt = objShipmentDetail.TAX3_Base_Amt
                objTr.TAX3_Rate = objShipmentDetail.TAX3_Rate
                objTr.TAX3_Amt = objShipmentDetail.TAX3_Amt
                objTr.TAX4 = objShipmentDetail.TAX4
                objTr.TAX4_Base_Amt = objShipmentDetail.TAX4_Base_Amt
                objTr.TAX4_Rate = objShipmentDetail.TAX4_Rate
                objTr.TAX4_Amt = objShipmentDetail.TAX4_Amt
                objTr.TAX5 = objShipmentDetail.TAX5
                objTr.TAX5_Base_Amt = objShipmentDetail.TAX5_Base_Amt
                objTr.TAX5_Rate = objShipmentDetail.TAX5_Rate
                objTr.TAX5_Amt = objShipmentDetail.TAX5_Amt
                objTr.TAX6 = objShipmentDetail.TAX6
                objTr.TAX6_Base_Amt = objShipmentDetail.TAX6_Base_Amt
                objTr.TAX6_Rate = objShipmentDetail.TAX6_Rate
                objTr.TAX6_Amt = objShipmentDetail.TAX6_Amt
                objTr.TAX7 = objShipmentDetail.TAX7
                objTr.TAX7_Base_Amt = objShipmentDetail.TAX7_Base_Amt
                objTr.TAX7_Rate = objShipmentDetail.TAX7_Rate
                objTr.TAX7_Amt = objShipmentDetail.TAX7_Amt
                objTr.TAX8 = objShipmentDetail.TAX8
                objTr.TAX8_Base_Amt = objShipmentDetail.TAX8_Base_Amt
                objTr.TAX8_Rate = objShipmentDetail.TAX8_Rate
                objTr.TAX8_Amt = objShipmentDetail.TAX8_Amt
                objTr.TAX9 = objShipmentDetail.TAX9
                objTr.TAX9_Base_Amt = objShipmentDetail.TAX9_Base_Amt
                objTr.TAX9_Rate = objShipmentDetail.TAX9_Rate
                objTr.TAX9_Amt = objShipmentDetail.TAX9_Amt
                objTr.TAX10 = objShipmentDetail.TAX10
                objTr.TAX10_Base_Amt = objShipmentDetail.TAX10_Base_Amt
                objTr.TAX10_Rate = objShipmentDetail.TAX10_Rate
                objTr.TAX10_Amt = objShipmentDetail.TAX10_Amt
                objTr.Amount = objShipmentDetail.Amount
                objTr.Disc_Per = objShipmentDetail.Disc_Per
                objTr.Disc_Amt = objShipmentDetail.Disc_Amt
                objTr.Amt_Less_Discount = objShipmentDetail.Amt_Less_Discount
                objTr.Total_Tax_Amt = objShipmentDetail.Total_Tax_Amt
                objTr.Item_Net_Amt = objShipmentDetail.Item_Net_Amt


                objTr.Is_Mannual_Amt = objShipmentDetail.Is_Mannual_Amt

                objTr.MRP = objShipmentDetail.MRP
                objTr.Assessable = objShipmentDetail.Assessable
                objTr.AssessableAmt = objShipmentDetail.AssessableAmt
                objTr.Batch_No = objShipmentDetail.Batch_No
                If objShipmentDetail.MFG_Date IsNot Nothing Then
                    objTr.MFG_Date = objShipmentDetail.MFG_Date
                End If
                If objShipmentDetail.Expiry_Date IsNot Nothing Then
                    objTr.Expiry_Date = objShipmentDetail.Expiry_Date
                End If
                objTr.Specification = objShipmentDetail.Specification
                objTr.Remarks = objShipmentDetail.Remarks

                objTr.Scheme_Applicable = IIf(objShipmentDetail.Scheme_Applicable = "Y", "Yes", "No")
                objTr.Scheme_Code = objShipmentDetail.Scheme_Code
                objTr.Scheme_Item = IIf(objShipmentDetail.Scheme_Item = "Y", "Yes", "No")
                objTr.Item_Tax = objShipmentDetail.Item_Tax
                objTr.Total_MRP_Amt = objShipmentDetail.Total_MRP_Amt
                objTr.Total_Basic_Amt = objShipmentDetail.Total_Basic_Amt
                objTr.Total_Disc_Amt = objShipmentDetail.Total_Disc_Amt
                objTr.Cust_Discount = objShipmentDetail.Cust_Discount
                objTr.Total_Cust_Discount = objShipmentDetail.Total_Cust_Discount
                objTr.ActualRate = objShipmentDetail.ActualRate
                objTr.Cust_DiscountQty = objShipmentDetail.Cust_DiscountQty
                objTr.Price_code = objShipmentDetail.Price_code
                objTr.Price_Date = objShipmentDetail.Price_Date
                objTr.Abatement_Per = objShipmentDetail.Abatement_Per
                objTr.Abatement_Amt = objShipmentDetail.Abatement_Amt
                objTr.FOC_Item = objShipmentDetail.FOC_Item
                objTr.Markup_On = objShipmentDetail.Markup_On
                objTr.Markup_Percent = objShipmentDetail.Markup_Percent
                objTr.Landing_Cost = objShipmentDetail.Landing_Cost
                objTr.HeadDiscAmt = objShipmentDetail.HeadDiscAmt
                objTr.HeadDiscPer = objShipmentDetail.HeadDiscPer
                objTr.HeadDiscPerAmt = objShipmentDetail.HeadDiscPerAmt
                objTr.CustDiscPer = objShipmentDetail.CustDiscPer
                objTr.CasdDiscScheme_Code = objShipmentDetail.CasdDiscScheme_Code

                objTr.Item_Weight = objShipmentDetail.Item_Weight
                objTr.TotalItem_Weight = objShipmentDetail.TotalItem_Weight
                objTr.Conv_Factor = objShipmentDetail.Conv_Factor
                objTr.Purchase_Cost = objShipmentDetail.Purchase_Cost
                objTr.OrgRate = objShipmentDetail.OrgRate

                objTr.Price_Amount1 = objShipmentDetail.Price_Amount1
                objTr.Price_Amount2 = objShipmentDetail.Price_Amount2
                objTr.Price_Amount3 = objShipmentDetail.Price_Amount3
                objTr.Price_Amount4 = objShipmentDetail.Price_Amount4
                objTr.Price_Amount5 = objShipmentDetail.Price_Amount5
                objTr.Price_Amount6 = objShipmentDetail.Price_Amount6
                objTr.Price_Amount7 = objShipmentDetail.Price_Amount7
                objTr.Price_Amount8 = objShipmentDetail.Price_Amount8
                objTr.Price_Amount9 = objShipmentDetail.Price_Amount9
                objTr.Price_Amount10 = objShipmentDetail.Price_Amount10

                objTr.TAX1_Base_Amt = objShipmentDetail.TAX1_Base_Amt
                objTr.TAX2_Base_Amt = objShipmentDetail.TAX2_Base_Amt
                objTr.TAX3_Base_Amt = objShipmentDetail.TAX3_Base_Amt
                objTr.TAX4_Base_Amt = objShipmentDetail.TAX4_Base_Amt
                objTr.TAX5_Base_Amt = objShipmentDetail.TAX5_Base_Amt
                objTr.TAX6_Base_Amt = objShipmentDetail.TAX6_Base_Amt
                objTr.TAX7_Base_Amt = objShipmentDetail.TAX7_Base_Amt
                objTr.TAX8_Base_Amt = objShipmentDetail.TAX8_Base_Amt
                objTr.TAX9_Base_Amt = objShipmentDetail.TAX9_Base_Amt
                objTr.TAX10_Base_Amt = objShipmentDetail.TAX10_Base_Amt

                obj.Arr.Add(objTr)
            Next
        End If
        Return obj
    End Function


    Private Shared Function GetFirstItemCode(ByVal Arr As List(Of clsSNShipmentDetail)) As String


        For Each objtr As clsSNShipmentDetail In Arr
            If clsCommon.CompairString(objtr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Return objtr.Item_Code
            End If
        Next
        Return ""
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsSNShipmentHead = clsSNShipmentHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                '' Anubhooti 06-Sep-2014 BM00000003735 (Locked Transaction)
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Shipment/Sale Invoice", obj.Bill_To_Location, obj.Document_Date, trans)
                ''
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsSerializeInvenotry.DeleteData("SD-IN", strCode, trans)

                Dim qry As String = "delete from TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL where DOCUMENT_CODE='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)


                Dim qry1 As String = "delete from TSPL_SD_SHIPMENT_WEIGHMENT_MAPPING where Document_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry1, trans)

                Dim qry2 As String = "delete from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry2, trans)

                Dim qry3 As String = "delete from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry3, trans)

                clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function

    Public Shared Function UnpostData(ByVal strCode As String, ByVal FormId As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved AndAlso UnpostData(strCode, FormId, trans)
            trans.Commit()
            Return issaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function UnpostData(ByVal strCode As String, ByVal FormId As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As clsSNShipmentHead = clsSNShipmentHead.GetData(strCode, NavigatorType.Current, trans)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Code,Bill_To_Location from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProductionEntry, clsCommon.myCstr(dt.Rows(0)("Bill_To_Location")), clsCommon.myCDate(dt.Rows(0)("Document_Code")), trans)
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Shipment/Sale Invoice", obj.Bill_To_Location, obj.Document_Date, trans)
            End If

            Dim qry As String = "select  Against_Shipment_No from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No ='" + strCode + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = "SaleInvoice is created"
                Throw New Exception(qry)
                'clsCommon.MyMessageBoxShow("SaleInvoice is created")
                Return True
                Exit Function
            End If

            Dim issaved As Boolean = True

            qry = "update TSPL_SD_SHIPMENT_HEAD set status='0',Modify_By='" + objCommonVar.CurrentUserCode + "',Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function IsValidCustomer(ByVal Arr As List(Of String), ByVal strCustomerCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select TSPL_SD_SHIPMENT_HEAD.Document_Code,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code where Document_Code in (" + clsCommon.GetMulcallString(Arr) + ") and Customer_Code not in ('" + strCustomerCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "SRN No:" + clsCommon.myCstr(dr("Document_Code")) + " Is For Vendor Code: " + clsCommon.myCstr(dr("Customer_Code")) + " Vendor Name:" + clsCommon.myCstr(dr("Customer_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans, False)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal isReverseOnly As Boolean) As Boolean
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If

            Dim Qry As String = "select Status from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            If Not isReverseOnly Then
                Qry = "select distinct DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_DETAIL where Shipment_Code='" + strCode + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Qry = "Current Shipment is used in following Sale invoice -"
                    For Each dr As DataRow In dt.Rows
                        Qry += Environment.NewLine + clsCommon.myCstr(dr("DOCUMENT_CODE"))
                    Next
                    Throw New Exception(Qry)
                End If
            End If

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='SD-SH' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type='SD-SH'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            If Not isReverseOnly Then
                Qry = "Update TSPL_SD_SHIPMENT_HEAD set Status = 0 where Document_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "update TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL set is_reverse=1 where document_code='" + strCode + "' and trans_code='" + clsCommon.myCstr(clsUserMgtCode.frmSNShipment) + "' and is_reverse=0"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = " update TSPL_SD_SHIPMENT_HEAD set is_create_auto_invoice=0 where Document_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsSNShipmentDetail
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Line_No As Integer = 0
    Public Row_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing 'Not a Table Field
    Public Bar_Code As String = Nothing
    Public Qty As Double = 0
    Public Balance_Qty As Double = 0
    Public Free_Qty As Double = 0
    Public Order_Code As String = Nothing
    Public Unit_code As String = Nothing '
    Public Location As String = Nothing '
    Public LocationName As String = Nothing 'Not a Table Field
    Public Item_Cost As Double = 0
    Public TAX1 As String = Nothing
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
    Public Amount As Double = 0
    Public Disc_Per As Double = 0
    Public Disc_Amt As Double = 0
    Public Amt_Less_Discount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
    Public Status As Integer = 0
    Public MRP As Double = 0
    Public MFG_Date As Date? = Nothing
    Public Batch_No As String = Nothing
    Public Expiry_Date As Date? = Nothing
    Public Specification As String = Nothing
    Public Remarks As String = Nothing
    Public Assessable As Double = 0
    Public AssessableAmt As Double = 0
    Public Is_Mannual_Amt As Integer = Nothing
    'Public Unit_Cogs As Double = 0
    Public SRNTax_Group As String = Nothing 'Not a Table Field

    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing


    Public Scheme_Applicable As String = Nothing
    Public Scheme_Code As String = Nothing
    Public Scheme_Item As String = Nothing
    Public Item_Tax As Double = 0
    Public Total_MRP_Amt As Double = 0
    Public Total_Basic_Amt As Double = 0
    Public Total_Disc_Amt As Double = 0
    Public Cust_Discount As Double = 0
    Public Total_Cust_Discount As Double = 0
    Public Price_Amount1 As Double = 0
    Public Price_Amount2 As Double = 0
    Public Price_Amount3 As Double = 0
    Public Price_Amount4 As Double = 0
    Public Price_Amount5 As Double = 0
    Public Price_Amount6 As Double = 0
    Public Price_Amount7 As Double = 0
    Public Price_Amount8 As Double = 0
    Public Price_Amount9 As Double = 0
    Public Price_Amount10 As Double = 0
    Public ActualRate As Double = 0
    Public Cust_DiscountQty As Double = 0
    Public Price_code As String = Nothing
    Public Abatement_Per As Double = 0
    Public Abatement_Amt As Double = 0
    Public FOC_Item As Double = 0
    Public Price_Date As String = Nothing

    Public Item_Weight As Double = 0
    Public Conv_Factor As Double = 0
    Public TotalItem_Weight As Double = 0
    Public Markup_On As String = Nothing
    Public Markup_Percent As Double = 0
    Public Landing_Cost As Double = 0
    Public HeadDiscAmt As Double = 0
    Public CustDiscPer As Double = 0
    Public CasdDiscScheme_Code As String = Nothing
    Public Purchase_Cost As Double = 0
    Public OrgRate As Double = 0
    Public PrincipleCode As String = Nothing
    Public PrincipleDesc As String = Nothing
    Public vendor_code As String = Nothing
    Public vendor_desc As String = Nothing
    Public Bin_No As String = Nothing
    Public HeadDiscPer As Double = 0
    Public HeadDiscPerAmt As Double = 0
    Public ItemwiseTaxCode As String = ""
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsSNShipmentDetail), ByVal trans As SqlTransaction, ByVal DocDate As DateTime) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSNShipmentDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Bar_Code", obj.Bar_Code, True)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)

                clsCommon.AddColumnsForChange(coll, "Free_qty", obj.Free_Qty)

                clsCommon.AddColumnsForChange(coll, "Order_Code", obj.Order_Code, True)

                clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)

                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Per", obj.Disc_Per)
                clsCommon.AddColumnsForChange(coll, "Disc_Amt", obj.Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Amt_Less_Discount", obj.Amt_Less_Discount)
                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)

                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Is_Mannual_Amt", obj.Is_Mannual_Amt)
                If obj.MFG_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "MFG_Date", clsCommon.GetPrintDate(obj.MFG_Date, "dd-MMM-yyyy"))
                End If
                If obj.Expiry_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd-MMM-yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Assessable", obj.Assessable)
                clsCommon.AddColumnsForChange(coll, "AssessableAmt", obj.AssessableAmt)
                Dim strSql As String = "select top 1 TSPL_ITEM_PRICE_MASTER.Price_Amount1 ,TSPL_ITEM_PRICE_MASTER.Price_Amount2 , " &
              "TSPL_ITEM_PRICE_MASTER.Price_Amount3 ,TSPL_ITEM_PRICE_MASTER.Price_Amount4 ,TSPL_ITEM_PRICE_MASTER.Price_Amount5 , " &
              "TSPL_ITEM_PRICE_MASTER.Price_Amount6 ,TSPL_ITEM_PRICE_MASTER.Price_Amount7 ,TSPL_ITEM_PRICE_MASTER.Price_Amount8 , " &
              "TSPL_ITEM_PRICE_MASTER.Price_Amount9 ,TSPL_ITEM_PRICE_MASTER.Price_Amount10 from TSPL_ITEM_PRICE_MASTER " &
             "where  TSPL_ITEM_PRICE_MASTER.Price_Code='" & obj.Price_code & "' and  TSPL_ITEM_PRICE_MASTER.Item_Code='" & obj.Item_Code & "' and  " &
             "TSPL_ITEM_PRICE_MASTER.Item_Basic_Net=" & obj.MRP & " and TSPL_ITEM_PRICE_MASTER.UOM='" & obj.Unit_code & "' "
                Dim dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(strSql, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Price_Amount1 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount1"))
                    obj.Price_Amount2 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount2"))
                    obj.Price_Amount3 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount3"))
                    obj.Price_Amount4 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount4"))
                    obj.Price_Amount5 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount5"))
                    obj.Price_Amount6 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount6"))
                    obj.Price_Amount7 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount7"))
                    obj.Price_Amount8 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount8"))
                    obj.Price_Amount9 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount9"))
                    obj.Price_Amount10 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount10"))
                End If

                clsCommon.AddColumnsForChange(coll, "Scheme_Applicable", IIf(obj.Scheme_Applicable = "Yes", "Y", "N"))
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", obj.Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item", IIf(obj.Scheme_Item = "Yes", "Y", "N"))
                clsCommon.AddColumnsForChange(coll, "Item_Tax", obj.Item_Tax)
                clsCommon.AddColumnsForChange(coll, "Total_MRP_Amt", obj.Total_MRP_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Basic_Amt", obj.Total_Basic_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Disc_Amt", obj.Total_Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Cust_Discount", obj.Cust_Discount)
                clsCommon.AddColumnsForChange(coll, "Total_Cust_Discount", obj.Total_Cust_Discount)
                clsCommon.AddColumnsForChange(coll, "Price_Amount1", obj.Price_Amount1)
                clsCommon.AddColumnsForChange(coll, "Price_Amount2", obj.Price_Amount2)
                clsCommon.AddColumnsForChange(coll, "Price_Amount3", obj.Price_Amount3)
                clsCommon.AddColumnsForChange(coll, "Price_Amount4", obj.Price_Amount4)
                clsCommon.AddColumnsForChange(coll, "Price_Amount5", obj.Price_Amount5)
                clsCommon.AddColumnsForChange(coll, "Price_Amount6", obj.Price_Amount6)
                clsCommon.AddColumnsForChange(coll, "Price_Amount7", obj.Price_Amount7)
                clsCommon.AddColumnsForChange(coll, "Price_Amount8", obj.Price_Amount8)
                clsCommon.AddColumnsForChange(coll, "Price_Amount9", obj.Price_Amount9)
                clsCommon.AddColumnsForChange(coll, "Price_Amount10", obj.Price_Amount10)
                clsCommon.AddColumnsForChange(coll, "ActualRate", obj.ActualRate)
                clsCommon.AddColumnsForChange(coll, "Cust_DiscountQty", obj.Cust_DiscountQty)
                clsCommon.AddColumnsForChange(coll, "Price_code", obj.Price_code)
                If clsCommon.myLen(obj.Price_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Abatement_Per", obj.Abatement_Per)
                clsCommon.AddColumnsForChange(coll, "Abatement_Amt", obj.Abatement_Amt)
                clsCommon.AddColumnsForChange(coll, "FOC_Item", obj.FOC_Item)

                clsCommon.AddColumnsForChange(coll, "Item_Weight", obj.Item_Weight)
                clsCommon.AddColumnsForChange(coll, "Conv_Factor", obj.Conv_Factor)
                clsCommon.AddColumnsForChange(coll, "TotalItem_Weight", obj.TotalItem_Weight)
                clsCommon.AddColumnsForChange(coll, "Markup_On", obj.Markup_On)
                clsCommon.AddColumnsForChange(coll, "Markup_Percent", obj.Markup_Percent)
                clsCommon.AddColumnsForChange(coll, "Landing_Cost", obj.Landing_Cost)
                clsCommon.AddColumnsForChange(coll, "HeadDiscAmt", obj.HeadDiscAmt)
                clsCommon.AddColumnsForChange(coll, "CustDiscPer", obj.CustDiscPer)
                clsCommon.AddColumnsForChange(coll, "CasdDiscScheme_Code", obj.CasdDiscScheme_Code)
                clsCommon.AddColumnsForChange(coll, "Purchase_Cost", obj.Purchase_Cost)
                clsCommon.AddColumnsForChange(coll, "OrgRate", obj.OrgRate)
                clsCommon.AddColumnsForChange(coll, "PrincipleCode", obj.PrincipleCode)
                clsCommon.AddColumnsForChange(coll, "PrincipleDesc", obj.PrincipleDesc)
                clsCommon.AddColumnsForChange(coll, "vendor_code", obj.vendor_code)
                clsCommon.AddColumnsForChange(coll, "vendor_desc", obj.vendor_desc)
                clsCommon.AddColumnsForChange(coll, "Bin_No", obj.Bin_No)
                clsCommon.AddColumnsForChange(coll, "HeadDiscPer", obj.HeadDiscPer)
                clsCommon.AddColumnsForChange(coll, "HeadDiscPerAmt", obj.HeadDiscPerAmt)
                'clsCommon.AddColumnsForChange(coll, "Unit_Cogs", clsItemLocationDetails.GetUnitCogs(obj.Item_Code, obj.Location, trans))
                clsCommon.AddColumnsForChange(coll, "ItemwiseTaxCode", obj.ItemwiseTaxCode)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                clsSerializeInvenotry.SaveData("SD-IN", strDocNo, DocDate, "O", obj.Item_Code, obj.Location, obj.Line_No, obj.arrSrItem, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceSRNQty(ByVal strSRNCode As String, ByVal strICode As String, ByVal strCurrPINNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " &
            " select TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,TSPL_SD_SHIPMENT_DETAIL.Qty as Qty,1 as RI from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Document_Code where TSPL_SD_SHIPMENT_DETAIL.Status=0 and TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_SD_SHIPMENT_DETAIL.Document_Code ='" + strSRNCode + "' and TSPL_SD_SHIPMENT_DETAIL.Item_Code='" + strICode + "' and  TSPL_SD_SHIPMENT_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_SD_SHIPMENT_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_SD_SHIPMENT_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' " &
            " union all " &
            " select TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.PI_Qty as Qty,-1 as RI from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_DETAIL.SRN_Id='" + strSRNCode + "'   and TSPL_PI_DETAIL.Item_Code='" + strICode + "'  and  TSPL_PI_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_PI_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_PI_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'  and TSPL_PI_DETAIL.PI_No not in ('" + strCurrPINNo + "')  " &
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function CompleteSRN(ByVal strDoccode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_SD_SHIPMENT_DETAIL set Status=1 where Document_Code='" + strDoccode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
End Class


Public Class clsSNShipmentWeighment
#Region "Variables"
    Public Weighment_Code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As ArrayList, ByVal tran As SqlTransaction) As Boolean
        Try

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For i As Integer = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", DocNo)
                    clsCommon.AddColumnsForChange(coll, "Weighment_Code", arr.Item(i))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_WEIGHMENT_MAPPING", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsMargeDistributorItems
    Public keys As String = Nothing
    Public ICode As String = Nothing
    Public Qty As Decimal
    Public UOM As String = Nothing
End Class
Public Class clsSNShipmentDCSItemDetail
#Region "Variables"
    Public DOCUMENT_CODE As String = Nothing
    Public DCS_Code As String = Nothing
    Public UploaderNo As String = Nothing ''Not a Table Column
    Public DCSName As String = Nothing ''Not a Table Column
    Public DCSZone As String = Nothing ''Not a Table Column
    Public ICode As String = Nothing
    Public IName As String = Nothing ''Not a Table Column
    Public Qty As Decimal
    Public UOM As String = Nothing
    Public Frieght_UOM As String = Nothing
    Public FPKID As Double = 0
    Public Frieght_Rate As Double = 0
    Public Frieght_Amt As Double = 0

#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As List(Of clsSNShipmentDCSItemDetail), ByVal tran As SqlTransaction) As Boolean
        Try
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsSNShipmentDCSItemDetail In arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "DOCUMENT_CODE", DocNo)
                    clsCommon.AddColumnsForChange(coll, "DCS_Code", objtr.DCS_Code)
                    clsCommon.AddColumnsForChange(coll, "ICode", objtr.ICode)
                    clsCommon.AddColumnsForChange(coll, "Qty", objtr.Qty, True)
                    clsCommon.AddColumnsForChange(coll, "UOM", objtr.UOM)
                    clsCommon.AddColumnsForChange(coll, "FPKID", objtr.FPKID)
                    clsCommon.AddColumnsForChange(coll, "Frieght_Rate", objtr.Frieght_Rate, True)
                    clsCommon.AddColumnsForChange(coll, "Frieght_Amt", objtr.Frieght_Amt, True)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class


