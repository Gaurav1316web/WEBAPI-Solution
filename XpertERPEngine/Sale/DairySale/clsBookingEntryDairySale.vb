Imports common
Imports System.Data.SqlClient
Public Class clsBookingEntryDairySale

#Region "Variable"
    Public IsSampling As Integer = 0
    Public AgainstGatePass As Integer = 0
    Public Cust_Group_Code As String = Nothing
    Public Document_No As String = Nothing
    Public location_code As String = Nothing
    Public Against_DemandBooking_No As String = String.Empty
    Public BookingThrough As String = Nothing
    Public Ship_To_Location As String = String.Empty
    Public Document_Date As DateTime?
    Public Posted As Integer = 0
    Public Arr As List(Of clsBookingDetailDairySale) = Nothing
    Public CreateDO_Automatic As Integer = 0
    Public Is_Taxable As Integer = 0
    Public TRANSACTION_TYPE As String = Nothing
    Public Ex_Factory_Date As Date?
    Public From_Screen_code As String = ""
    Public SalesmanCode As String = ""
    Public Podate As Date? = Nothing

    Public Cust_PO_No As String = ""
    Public TotalCAN As Integer = 0
    Public TotalBox As Integer = 0
    Public TotalCrate As Integer = 0
    Public Is_Cancelled As Integer = 0
    Public Booking_Type As String = String.Empty
    Public Card_SALE_No As String = String.Empty
    Public CardSale_FROM_DATE As DateTime?
    Public CardSale_TO_DATE As DateTime?
    Public Uploading_date As DateTime?
    Public Payment_Mode As String = Nothing
    Public Reference_No As String = Nothing
    Public Counter_No As String = Nothing
    Public Against_Booking_No As String = Nothing
    Public Against_Receipt_No As String = String.Empty
    Public AdvanceAmount As Double = 0
    Public Created_Date As DateTime?
    Public Created_By As String = String.Empty
    Public Credit_Limit As Double = 0
    Public Advance_Security As Double = 0
    Public Revese_Adv_Security As Double = 0
    Public AR_Credit_Security As Double = 0
    Public Pending_Posted_DO As Double = 0
    Public UnPostedDispatch As Double = 0
    Public Ledger_Outstansing As Double = 0
    Public Refund_Security As Double = 0
    Public Reverse_Refund_Sec As Double = 0
    Public Total_Outstanding As Double = 0
    Public Login_User_Zone_Code As String = Nothing
    Public GatePass_Type As String = String.Empty

    Public arrBookingDetailDairySalePaymentMode As List(Of clsBookingDetailDairySalePaymentMode) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsBookingEntryDairySale, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsBookingEntryDairySale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Return SaveData(obj, isNewEntry, trans, "")
    End Function
    Public Function SaveData(ByVal obj As clsBookingEntryDairySale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, ByVal strBookingDocNo As String) As Boolean
        Dim qry As String = String.Empty
        Try
            ''richa 4 Aug,2021 optimization related
            'If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            '    For Each obj1 As clsBookingDetailDairySale In Arr
            '        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Dairy Sale", "Fresh Booking Entry", obj1.Loc_Code, obj.Document_Date, trans)

            '        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Dairy Sale", "Dairy Booking Entry", obj1.Loc_Code, obj.Document_Date, trans)
            '    Next
            'End If

            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Dairy Sale", "Fresh Booking Entry", obj.location_code, obj.Document_Date, trans)
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmbookingdairyFreshSale, obj.location_code, obj.Document_Date, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmbookingdairy, obj.location_code, obj.Document_Date, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmDairyBookingCustomer, obj.location_code, obj.Document_Date, trans)


            Dim isSaved As Boolean = True

            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_BOOKING_MATSER", "Document_No", "TSPL_BOOKING_DETAIL", "Document_No", "TSPL_BOOKING_PAYMENT_MODE_DETAIL", "Document_No", trans)
            End If


            qry = "delete from TSPL_BOOKING_DETAIL where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_BOOKING_PAYMENT_MODE_DETAIL where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry AndAlso clsCommon.myLen(clsCommon.myCstr(strBookingDocNo)) > 0 Then
                obj.Document_No = strBookingDocNo
            ElseIf isNewEntry = True Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmDairySaleBooking, "", obj.location_code)
            End If

            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)

            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            End If

            If clsCommon.myLen(obj.Ex_Factory_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Ex_Factory_Date", clsCommon.GetPrintDate(obj.Ex_Factory_Date, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "Ex_Factory_Date", obj.Ex_Factory_Date, True)
            End If

            'clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "location_code", obj.location_code)
            clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", obj.Cust_Group_Code)
            clsCommon.AddColumnsForChange(coll, "IsSampling", obj.IsSampling)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "AgainstGatePass", obj.AgainstGatePass)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            clsCommon.AddColumnsForChange(coll, "Is_Taxable", obj.Is_Taxable)
            clsCommon.AddColumnsForChange(coll, "TRANSACTION_TYPE", obj.TRANSACTION_TYPE)
            clsCommon.AddColumnsForChange(coll, "Against_DemandBooking_No", obj.Against_DemandBooking_No, True)
            clsCommon.AddColumnsForChange(coll, "From_Screen_code", obj.From_Screen_code)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Location", obj.Ship_To_Location, True)
            clsCommon.AddColumnsForChange(coll, "SalesmanCode", obj.SalesmanCode, True)
            clsCommon.AddColumnsForChange(coll, "BookingThrough", obj.BookingThrough, True)
            clsCommon.AddColumnsForChange(coll, "CustPO_No", obj.Cust_PO_No, True)
            clsCommon.AddColumnsForChange(coll, "Booking_Type", obj.Booking_Type, True)
            clsCommon.AddColumnsForChange(coll, "AdvanceAmount", obj.AdvanceAmount)
            If obj.Podate IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "custpo_date", clsCommon.GetPrintDate(obj.Podate, "dd/MMM/yyyy hh:mm tt"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "custpo_date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Total_CAN", obj.TotalCAN)
            clsCommon.AddColumnsForChange(coll, "Total_Box", obj.TotalBox)
            clsCommon.AddColumnsForChange(coll, "Total_Crate", obj.TotalCrate)
            ''richa agarwal 16 Oct,2019
            clsCommon.AddColumnsForChange(coll, "Card_SALE_No", obj.Card_SALE_No, True)
            If clsCommon.myLen(obj.CardSale_FROM_DATE) > 0 Then
                clsCommon.AddColumnsForChange(coll, "CardSale_FROM_DATE", clsCommon.GetPrintDate(obj.CardSale_FROM_DATE, "dd/MMM/yyyy hh:mm tt"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "CardSale_FROM_DATE", obj.CardSale_FROM_DATE, True)
            End If
            If clsCommon.myLen(obj.CardSale_TO_DATE) > 0 Then
                clsCommon.AddColumnsForChange(coll, "CardSale_TO_DATE", clsCommon.GetPrintDate(obj.CardSale_TO_DATE, "dd/MMM/yyyy hh:mm tt"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "CardSale_TO_DATE", obj.CardSale_TO_DATE, True)
            End If
            If clsCommon.myLen(obj.Uploading_date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Uploading_date", clsCommon.GetPrintDate(obj.Uploading_date, "dd/MMM/yyyy hh:mm tt"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "Uploading_date", obj.Uploading_date, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Payment_Mode", obj.Payment_Mode, True)
            clsCommon.AddColumnsForChange(coll, "Reference_No", obj.Reference_No, True)
            clsCommon.AddColumnsForChange(coll, "Counter_No", obj.Counter_No, True)
            clsCommon.AddColumnsForChange(coll, "Against_Booking_No", obj.Against_Booking_No, True)
            clsCommon.AddColumnsForChange(coll, "Against_Receipt_No", obj.Against_Receipt_No, True)

            clsCommon.AddColumnsForChange(coll, "Credit_Limit", obj.Credit_Limit)
            clsCommon.AddColumnsForChange(coll, "Advance_Security", obj.Advance_Security)
            clsCommon.AddColumnsForChange(coll, "Revese_Adv_Security", obj.Revese_Adv_Security)
            clsCommon.AddColumnsForChange(coll, "AR_Credit_Security", obj.AR_Credit_Security)
            clsCommon.AddColumnsForChange(coll, "Pending_Posted_DO", obj.Pending_Posted_DO)
            clsCommon.AddColumnsForChange(coll, "UnPostedDispatch", obj.UnPostedDispatch)
            clsCommon.AddColumnsForChange(coll, "Ledger_Outstansing", obj.Ledger_Outstansing)
            clsCommon.AddColumnsForChange(coll, "Refund_Security", obj.Refund_Security)
            clsCommon.AddColumnsForChange(coll, "Reverse_Refund_Sec", obj.Reverse_Refund_Sec)
            clsCommon.AddColumnsForChange(coll, "Total_Outstanding", obj.Total_Outstanding) ' 
            clsCommon.AddColumnsForChange(coll, "Login_User_Zone_Code", obj.Login_User_Zone_Code, True)
            clsCommon.AddColumnsForChange(coll, "GatePass_Type", obj.GatePass_Type, True)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOKING_MATSER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOKING_MATSER", OMInsertOrUpdate.Update, "TSPL_BOOKING_MATSER.Document_No='" + obj.Document_No + "'", trans)
            End If

            isSaved = isSaved AndAlso clsBookingDetailDairySale.SaveData(obj.Document_No, Arr, trans, isNewEntry, obj.Document_Date)
            isSaved = isSaved AndAlso clsBookingDetailDairySalePaymentMode.saveData(obj.arrBookingDetailDairySalePaymentMode, obj.Document_No, obj.Document_Date, trans)


            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            qry = Nothing
            obj = Nothing
        End Try
    End Function

    Public Shared Function CustomerOutstandingAmount(ByVal strCustomer As String, ByVal strDocno As String, ByVal dblDocumentAmount As Double, ByVal strDocDate As DateTime, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(strCustomer) & "' ", trans))
            If clsCommon.myLen(strCustomer) > 0 AndAlso (clsCommon.CompairString(strCustomerCategory, "Institution CR") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCustomerCategory, "Institution SO") = CompairStringResult.Equal) Then
                Return True
            End If

            Dim dblOutstandingAmt As Double = 0
            Dim dblCreditLimit As Double = 0
            Dim dblSecurityAmount As Double = 0
            Dim dblRefundAmount As Double = 0
            Dim dblReverseSecurityAmount As Double = 0
            Dim dblReverseRefundAmount As Double = 0
            Dim dblPendingDeliveryAmt As Double = 0
            Dim dblARSecurityAmt As Double = 0
            Dim dblAmt As Double = 0
            Dim qry As String = ""
            Dim dblShortCloseDoDispatch As Double = 0
            Dim dblBookingAmt As Double = 0
            Dim strCreditLimit As String = ""
            Dim DonotIncludeSecurityInCustomerOutstanding As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DonotIncludeSecurityInCustomerOutstanding, clsFixedParameterCode.DonotIncludeSecurityInCustomerOutstanding, trans)) = 1, True, False)
            Dim DoNotConsiderCustomerCreditLimit As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotConsiderCustomerCreditLimit, clsFixedParameterCode.DoNotConsiderCustomerCreditLimit, trans))
            If DoNotConsiderCustomerCreditLimit = 1 Then
                strCreditLimit = " and CheckCreditLimit=1 "
            End If
            Dim strIsParent As String = clsDBFuncationality.getSingleValue("select Parent_Customer_YN from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "' ", trans)
            Dim strParentCust As String = clsDBFuncationality.getSingleValue("select Parent_Customer_No from TSPL_CUSTOMER_MASTER where Parent_Customer_YN='N' and Parent_Customer_No <> '' and Cust_Code='" & strCustomer & "'   and Credit_Limit=0", trans)
            If clsCommon.CompairString(strIsParent, "Y") = CompairStringResult.Equal Then
                strParentCust = strCustomer
            End If
            Dim strcustomerfilter As String = String.Empty
            strcustomerfilter = "'" + strCustomer + "'"
            If clsCommon.myLen(strParentCust) > 0 OrElse clsCommon.CompairString(strIsParent, "Y") = CompairStringResult.Equal Then

                Dim strDate As Date = strDocDate.AddDays(1)

                qry = "Select  ( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as BalAmt From ( " &
                    "Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as CurrencyCode,  " &
                    "null as ConvRate, SUM(DrAmt* Final.ConvRate)-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote,  " &
                    "0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,  " &
                    "MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from   " &
                    "(" & clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, "ConvRate", strcustomerfilter, True, clsCommon.GetPrintDate(strDocDate.AddDays(1), "dd/MMM/yyyy"), "", False, False, True, trans, False) & "   " &
                    " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                    "Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " &
                    "where  CONVERT(DATE,final.DocDate,103) <= '" & clsCommon.GetPrintDate(strDocDate, "dd/MMM/yyyy") & "' AND LEN(ACode)>0 and ACode in ('" & strCustomer & "')   AND TSPL_CUSTOMER_MASTER.Status='N' GROUP BY ACode " &
                    ") XXX GROUP BY ACode ORDER BY ACode"


                dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "' " & strCreditLimit & "", trans))

                If DonotIncludeSecurityInCustomerOutstanding = False Then
                    dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and  SecurityDeposit='Y'  and Posted='Y' and Cust_Code='" & strCustomer & "'", trans))
                    dblReverseSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_BANK_REVERSE inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No where Source_Type='AR' and  Receipt_Type='P' and  SecurityDeposit='Y'  and isnull(TSPL_BANK_REVERSE.Post,'N')='P' and TSPL_BANK_REVERSE.Cust_Code='" & strCustomer & "'", trans))

                    dblRefundAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='F' and SecurityDeposit='Y'  and Posted='Y' and Cust_Code='" & strCustomer & "'", trans))
                    dblReverseRefundAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_BANK_REVERSE inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No where Source_Type='AR' and Receipt_Type='F' and SecurityDeposit='Y'  and isnull(TSPL_BANK_REVERSE.Post,'N')='P' and TSPL_BANK_REVERSE.Cust_Code='" & strCustomer & "'", trans))
                    dblARSecurityAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Document_Total) from tspl_customer_invoice_head where document_type='C'  and isnull(Against_Security_Receipt_No,'') <> '' and Status=1  and TSPL_Customer_Invoice_Head.Customer_Code='" & strCustomer & "'", trans))
                End If


                qry = "select ISNULL(sum(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Total_Amt),0) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE 
INNER  JOIN (SELECT DISTINCT TSPL_BOOKING_DETAIL.DELIVERY_NO,TSPL_BOOKING_MATSER.From_Screen_Code FROM TSPL_BOOKING_DETAIL INNER JOIN TSPL_BOOKING_MATSER ON  TSPL_BOOKING_MATSER.dOCUMENT_nO=TSPL_BOOKING_DETAIL.Document_No  where TSPL_BOOKING_MATSER.From_Screen_Code<>'BOOK-DS_FSH' AND TSPL_BOOKING_DETAIL.CUST_CODE='" & strCustomer & "' AND ISNULL(TSPL_BOOKING_DETAIL.DELIVERY_NO,'')<>'' ) CASHINDENTBOOKING ON CASHINDENTBOOKING.DELIVERY_NO= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No
where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.posted=1 and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No not in (Select isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code,'') from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_SD_SHIPMENT_DETAIL on 
TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_SD_SHIPMENT_HEAD.Customer_Code='" & strCustomer & "' )
and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.customer_code='" & strCustomer & "' and  
isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N')='N' "
                dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

                qry = "select SUM(isnull(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_SD_SHIPMENT_HEAD  " &
                "left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " &
                "left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No " &
                "where  TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & strCustomer & "' and  " &
                "isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N')='Y' and  TSPL_SD_SHIPMENT_HEAD.Status=0  " &
                " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_No in " &
                " ( SELECT DISTINCT TSPL_BOOKING_DETAIL.DELIVERY_NO FROM TSPL_BOOKING_DETAIL INNER JOIN TSPL_BOOKING_MATSER ON  TSPL_BOOKING_MATSER.dOCUMENT_nO=TSPL_BOOKING_DETAIL.Document_No  where TSPL_BOOKING_MATSER.From_Screen_Code<>'BOOK-DS_FSH' AND TSPL_BOOKING_DETAIL.CUST_CODE='" & strCustomer & "' AND ISNULL(TSPL_BOOKING_DETAIL.DELIVERY_NO,'')<>'' )"
                dblShortCloseDoDispatch = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))



            Else

                qry = "Select  ( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as BalAmt From ( " &
                    "Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as CurrencyCode,  " &
                    "null as ConvRate, SUM(DrAmt* Final.ConvRate)-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote,  " &
                    "0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,  " &
                    "MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from   " &
                    "(" & clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, "ConvRate", strcustomerfilter, True, clsCommon.GetPrintDate(strDocDate.AddDays(1), "dd/MMM/yyyy"), "", False, False, True, trans, False) & "   " &
                    " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                    "Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " &
                    "where  CONVERT(DATE,final.DocDate,103) <= '" & clsCommon.GetPrintDate(strDocDate, "dd/MMM/yyyy") & "' AND LEN(ACode)>0 and ACode in ('" & strCustomer & "')   AND TSPL_CUSTOMER_MASTER.Status='N' GROUP BY ACode " &
                    ") XXX GROUP BY ACode ORDER BY ACode"


                dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "' " & strCreditLimit & "", trans))

                If DonotIncludeSecurityInCustomerOutstanding = False Then
                    dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and SecurityDeposit='Y'  and Posted='Y' and Cust_Code='" & strCustomer & "'", trans))
                    dblReverseSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_BANK_REVERSE inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No where Source_Type='AR' and  Receipt_Type='P' and  SecurityDeposit='Y'  and isnull(TSPL_BANK_REVERSE.Post,'N')='P' and TSPL_BANK_REVERSE.Cust_Code='" & strCustomer & "'", trans))

                    dblRefundAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='F' and SecurityDeposit='Y'  and Posted='Y' and Cust_Code='" & strCustomer & "'", trans))
                    dblReverseRefundAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_BANK_REVERSE inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No where Source_Type='AR' and Receipt_Type='F' and SecurityDeposit='Y'  and isnull(TSPL_BANK_REVERSE.Post,'N')='P' and TSPL_BANK_REVERSE.Cust_Code='" & strCustomer & "'", trans))
                    dblARSecurityAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Document_Total) from tspl_customer_invoice_head where document_type='C'  and isnull(Against_Security_Receipt_No,'') <> '' and Status=1  and TSPL_Customer_Invoice_Head.Customer_Code='" & strCustomer & "'", trans))
                End If


                qry = "select ISNULL(sum(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Total_Amt),0) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE 
INNER  JOIN (SELECT DISTINCT TSPL_BOOKING_DETAIL.DELIVERY_NO,TSPL_BOOKING_MATSER.From_Screen_Code FROM TSPL_BOOKING_DETAIL INNER JOIN TSPL_BOOKING_MATSER ON  TSPL_BOOKING_MATSER.dOCUMENT_nO=TSPL_BOOKING_DETAIL.Document_No  where TSPL_BOOKING_MATSER.From_Screen_Code<>'BOOK-DS_FSH' AND TSPL_BOOKING_DETAIL.CUST_CODE='" & strCustomer & "' AND ISNULL(TSPL_BOOKING_DETAIL.DELIVERY_NO,'')<>'' ) CASHINDENTBOOKING ON CASHINDENTBOOKING.DELIVERY_NO= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No
where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.posted=1 and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No not in (Select isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code,'') from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_SD_SHIPMENT_DETAIL on 
TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_SD_SHIPMENT_HEAD.Customer_Code='" & strCustomer & "' )
and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.customer_code='" & strCustomer & "' and  
isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N')='N' "
                dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

                qry = "select SUM(isnull(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_SD_SHIPMENT_HEAD  " &
                "left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " &
                "left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No " &
                "where  TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & strCustomer & "' and  " &
                "isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N')='Y' and  TSPL_SD_SHIPMENT_HEAD.Status=0  " &
                " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_No in " &
                " ( SELECT DISTINCT TSPL_BOOKING_DETAIL.DELIVERY_NO FROM TSPL_BOOKING_DETAIL INNER JOIN TSPL_BOOKING_MATSER ON  TSPL_BOOKING_MATSER.dOCUMENT_nO=TSPL_BOOKING_DETAIL.Document_No  where TSPL_BOOKING_MATSER.From_Screen_Code<>'BOOK-DS_FSH' AND TSPL_BOOKING_DETAIL.CUST_CODE='" & strCustomer & "' AND ISNULL(TSPL_BOOKING_DETAIL.DELIVERY_NO,'')<>'' )"
                dblShortCloseDoDispatch = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

            End If
            dblBookingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull( sum(TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_BOOKING_DETAIL.Item_Rate) ,0 ) from TSPL_BOOKING_DETAIL INNER JOIN TSPL_BOOKING_MATSER ON  TSPL_BOOKING_MATSER.dOCUMENT_nO=TSPL_BOOKING_DETAIL.Document_No where TSPL_BOOKING_DETAIL.Booking_Status=4 and isnull(TSPL_BOOKING_DETAIL.DO_Posted,0)=0 and  TSPL_BOOKING_DETAIL.Cust_Code='" & strCustomer & "'  and " &
                            "TSPL_BOOKING_DETAIL.Document_No not in ('" & strDocno & "') and TSPL_BOOKING_MATSER.From_Screen_Code<>'BOOK-DS_FSH' ", trans))
            dblAmt = dblCreditLimit + dblSecurityAmount - dblReverseSecurityAmount - dblPendingDeliveryAmt - dblOutstandingAmt - dblShortCloseDoDispatch - dblRefundAmount + dblReverseRefundAmount - dblARSecurityAmt - dblBookingAmt


            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  isnull(tspl_customer_master.CheckCreditLimit,0) as CheckCreditLimit from tspl_customer_master where cust_code='" + strCustomer + "'", trans)) = 0 Then
                Return True
            End If

            If dblAmt < clsCommon.myCdbl(dblDocumentAmount) Then
                Dim dblNewCredtitLimit = dblAmt - clsCommon.myCdbl(dblDocumentAmount)
                'common.clsCommon.MyMessageBoxShow("Please send for approval for increasing credit limit " + clsCommon.myCstr(dblNewCredtitLimit))
                Return False

            End If
            'Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, Optional ByVal FormId As String = "") As clsBookingEntryDairySale
        Return GetData(strDocumentNo, NavType, Nothing, FormId)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal Trans As SqlTransaction, Optional ByVal FormId As String = "") As clsBookingEntryDairySale
        Dim obj As clsBookingEntryDairySale = Nothing
        Dim qry As String = "select distinct TSPL_BOOKING_MATSER.Against_DemandBooking_No,TSPL_BOOKING_MATSER.Ship_To_Location,TSPL_BOOKING_MATSER.Created_Date,TSPL_BOOKING_MATSER.AdvanceAmount,TSPL_BOOKING_MATSER.Against_Receipt_No,TSPL_BOOKING_MATSER.Against_Booking_No,TSPL_BOOKING_MATSER.Payment_Mode,TSPL_BOOKING_MATSER.Reference_No,TSPL_BOOKING_MATSER.Counter_No,TSPL_BOOKING_MATSER.IsSampling,TSPL_BOOKING_MATSER.AgainstGatePass,Document_No,Document_Date,Posted,CreateDO_Automatic,location_code,Cust_Group_Code,Is_Taxable,TRANSACTION_TYPE,Ex_Factory_Date,isnull(CustPO_No,'') as CustPO_No,custpo_date,isnull(SalesmanCode,'') as SalesmanCode,Total_Can,total_Box,Total_Crate,isnull(Is_Cancelled,0) as Is_Cancelled, isnull(Booking_Type,'') as Booking_Type,isnull(Card_SALE_No,'') as Card_SALE_No,CardSale_FROM_DATE,CardSale_TO_DATE,Uploading_date " &
            " ,isnull(Credit_Limit,0) as Credit_Limit,isnull(Advance_Security,0) as Advance_Security,isnull(Revese_Adv_Security,0) as Revese_Adv_Security,isnull(AR_Credit_Security,0) as AR_Credit_Security,isnull(Pending_Posted_DO,0) as Pending_Posted_DO,isnull(UnPostedDispatch,0) as UnPostedDispatch,isnull(Ledger_Outstansing,0) as Ledger_Outstansing,isnull(Refund_Security,0) as Refund_Security,isnull(Reverse_Refund_Sec,0) as Reverse_Refund_Sec,isnull(Total_Outstanding,0) as Total_Outstanding, isnull(GatePass_Type,'') as GatePass_Type,Created_By " &
            " from TSPL_BOOKING_MATSER where comp_code='" + objCommonVar.CurrentCompanyCode + "' and "

        '-------richa 12/08/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        Dim whrClas As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(NavType).ToUpper(), "CURRENT") <> CompairStringResult.Equal Then
            strwherecls = Xtra.CustomerPermission()
            If clsCommon.myLen(strwherecls) > 0 Then
                whrClas = " and TSPL_BOOKING_DETAIL.Cust_Code in (" + strwherecls + ") "
            End If
        End If

        'Select Case NavType
        '    Case NavigatorType.Current
        '        qry += "  Document_No='" + strDocumentNo + "'"
        '    Case NavigatorType.Next
        '        qry += " Document_No in (select isnull(min(t.Document_No),'') from TSPL_BOOKING_MATSER  as t where t.From_Screen_code='" + FormId + "' and t.Document_No  >'" + strDocumentNo + "')"
        '    Case NavigatorType.First
        '        qry += " Document_No in (select isnull(min(t.Document_No),'') from TSPL_BOOKING_MATSER  as t where t.From_Screen_code='" + FormId + "' )"
        '    Case NavigatorType.Last
        '        qry += " Document_No in (select isnull(max(t.Document_No),'') from TSPL_BOOKING_MATSER  as t where t.From_Screen_code='" + FormId + "' )"
        '    Case NavigatorType.Previous
        '        qry += " Document_No in (select isnull(max(t.Document_No),'') from TSPL_BOOKING_MATSER  as t where t.From_Screen_code='" + FormId + "' and  t.Document_No  <'" + strDocumentNo + "')"
        'End Select


        Select Case NavType
            Case NavigatorType.Current
                qry += "  Document_No='" + strDocumentNo + "'"
            Case NavigatorType.Next
                qry += " Document_No in (select isnull(min(t.Document_No),'') from TSPL_BOOKING_MATSER  as t inner join TSPL_BOOKING_DETAIL on t .Document_No =TSPL_BOOKING_DETAIL .Document_No  where t.From_Screen_code='" + FormId + "' and t.Document_No  >'" + strDocumentNo + "'  " + whrClas + ")"
            Case NavigatorType.First
                qry += " Document_No in (select isnull(min(t.Document_No),'') from TSPL_BOOKING_MATSER  as t inner join TSPL_BOOKING_DETAIL on t .Document_No =TSPL_BOOKING_DETAIL .Document_No  where t.From_Screen_code='" + FormId + "'  " + whrClas + ")"
            Case NavigatorType.Last
                qry += " Document_No in (select isnull(max(t.Document_No),'') from TSPL_BOOKING_MATSER  as t inner join TSPL_BOOKING_DETAIL on t .Document_No =TSPL_BOOKING_DETAIL .Document_No  where t.From_Screen_code='" + FormId + "'  " + whrClas + " )"
            Case NavigatorType.Previous
                qry += " Document_No in (select isnull(max(t.Document_No),'') from TSPL_BOOKING_MATSER  as t inner join TSPL_BOOKING_DETAIL on t .Document_No =TSPL_BOOKING_DETAIL .Document_No  where t.From_Screen_code='" + FormId + "' and  t.Document_No  <'" + strDocumentNo + "'  " + whrClas + ")"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsBookingEntryDairySale
            obj.IsSampling = clsCommon.myCdbl(dt.Rows(0)("IsSampling"))
            obj.AgainstGatePass = clsCommon.myCdbl(dt.Rows(0)("AgainstGatePass"))
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Against_DemandBooking_No = clsCommon.myCstr(dt.Rows(0)("Against_DemandBooking_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.CreateDO_Automatic = clsCommon.myCdbl(dt.Rows(0)("CreateDO_Automatic"))
            obj.location_code = clsCommon.myCstr(dt.Rows(0)("location_code"))
            obj.Ship_To_Location = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location"))
            obj.Cust_Group_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Group_Code"))
            obj.Is_Taxable = clsCommon.myCstr(dt.Rows(0)("Is_Taxable"))
            obj.TRANSACTION_TYPE = clsCommon.myCstr(dt.Rows(0)("TRANSACTION_TYPE"))
            obj.Booking_Type = clsCommon.myCstr(dt.Rows(0)("Booking_Type"))
            If clsCommon.myLen(dt.Rows(0)("Ex_Factory_Date")) > 0 Then
                obj.Ex_Factory_Date = clsCommon.myCDate(dt.Rows(0)("Ex_Factory_Date"))
            End If
            ''richa agarwal 16 Oct,2019
            obj.Card_SALE_No = clsCommon.myCstr(dt.Rows(0)("Card_SALE_No"))
            If clsCommon.myLen(dt.Rows(0)("CardSale_TO_DATE")) > 0 Then
                obj.CardSale_TO_DATE = clsCommon.myCDate(dt.Rows(0)("CardSale_TO_DATE"))
            End If
            If clsCommon.myLen(dt.Rows(0)("CardSale_FROM_DATE")) > 0 Then
                obj.CardSale_FROM_DATE = clsCommon.myCDate(dt.Rows(0)("CardSale_FROM_DATE"))
            End If
            If clsCommon.myLen(dt.Rows(0)("Uploading_date")) > 0 Then
                obj.Uploading_date = clsCommon.myCDate(dt.Rows(0)("Uploading_date"))
            End If
            obj.Reference_No = clsCommon.myCstr(dt.Rows(0)("Reference_No"))
            obj.Counter_No = clsCommon.myCstr(dt.Rows(0)("Counter_No"))
            obj.Payment_Mode = clsCommon.myCstr(dt.Rows(0)("Payment_Mode"))
            obj.Against_Booking_No = clsCommon.myCstr(dt.Rows(0)("Against_Booking_No"))
            obj.Against_Receipt_No = clsCommon.myCstr(dt.Rows(0)("Against_Receipt_No"))
            obj.AdvanceAmount = clsCommon.myCdbl(dt.Rows(0)("AdvanceAmount"))

            obj.SalesmanCode = clsCommon.myCstr(dt.Rows(0)("SalesmanCode"))
            obj.Cust_PO_No = clsCommon.myCstr(dt.Rows(0)("CustPO_No"))
            If clsCommon.myLen(dt.Rows(0)("custpo_date")) > 0 Then
                obj.Podate = clsCommon.myCDate(dt.Rows(0)("custpo_date"))
            End If
            obj.TotalCrate = clsCommon.myCdbl(dt.Rows(0)("Total_Crate"))
            obj.TotalCAN = clsCommon.myCdbl(dt.Rows(0)("Total_CAN"))
            obj.TotalBox = clsCommon.myCdbl(dt.Rows(0)("Total_Box"))
            'Sanjay Ticket No- ERO/12/07/18-000371
            obj.Is_Cancelled = clsCommon.myCdbl(dt.Rows(0)("Is_Cancelled"))
            'Sanjay Ticket No- ERO/12/07/18-000371

            obj.Credit_Limit = clsCommon.myCdbl(dt.Rows(0)("Credit_Limit"))
            obj.Advance_Security = clsCommon.myCdbl(dt.Rows(0)("Advance_Security"))
            obj.Revese_Adv_Security = clsCommon.myCdbl(dt.Rows(0)("Revese_Adv_Security"))
            obj.AR_Credit_Security = clsCommon.myCdbl(dt.Rows(0)("AR_Credit_Security"))
            obj.Pending_Posted_DO = clsCommon.myCdbl(dt.Rows(0)("Pending_Posted_DO"))
            obj.UnPostedDispatch = clsCommon.myCdbl(dt.Rows(0)("UnPostedDispatch"))
            obj.Ledger_Outstansing = clsCommon.myCdbl(dt.Rows(0)("Ledger_Outstansing"))
            obj.Refund_Security = clsCommon.myCdbl(dt.Rows(0)("Refund_Security"))
            obj.Reverse_Refund_Sec = clsCommon.myCdbl(dt.Rows(0)("Reverse_Refund_Sec"))
            obj.Total_Outstanding = clsCommon.myCdbl(dt.Rows(0)("Total_Outstanding"))
            obj.GatePass_Type = clsCommon.myCstr(dt.Rows(0)("GatePass_Type"))
            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
            obj.arrBookingDetailDairySalePaymentMode = clsBookingDetailDairySalePaymentMode.getData(obj.Document_No, Trans)
        End If
        Return obj


    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Booking Order No not found to Delete")
        End If
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsBookingEntryDairySale = clsBookingEntryDairySale.GetData(strCode, NavigatorType.Current, trans)

        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmbookingdairy, obj.location_code, obj.Document_Date, trans)

        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmDairyBookingCustomer, obj.location_code, obj.Document_Date, trans)
        'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmbookingdairyFreshSale, obj.location_code, obj.Document_Date, trans)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try


                Dim qryReceipt As String = "Select distinct Against_Receipt_No  from " &
                " (select * from TSPL_BOOKING_PAYMENT_MODE_DETAIL where Against_Receipt_No in (select distinct Against_Receipt_No  from TSPL_BOOKING_PAYMENT_MODE_DETAIL where Document_No ='" + strCode + "'" &
                " ) " & Environment.NewLine &
                "       union " &
                " select * from TSPL_BOOKING_PAYMENT_MODE_DETAIL where  Document_No in ( " &
                " select distinct Document_No  from TSPL_BOOKING_PAYMENT_MODE_DETAIL where Against_Receipt_No in (select distinct Against_Receipt_No  from TSPL_BOOKING_PAYMENT_MODE_DETAIL where Document_No ='" + strCode + "'" &
                " ))) Final"
                Dim dtReceipt As DataTable = clsDBFuncationality.GetDataTable(qryReceipt, trans)

                Dim qryDoc As String = "Select distinct Document_No  from " &
               " (select * from TSPL_BOOKING_PAYMENT_MODE_DETAIL where Against_Receipt_No in (select distinct Against_Receipt_No  from TSPL_BOOKING_PAYMENT_MODE_DETAIL where Document_No ='" + strCode + "'" &
               " ) " & Environment.NewLine &
               "       union " &
               " select * from TSPL_BOOKING_PAYMENT_MODE_DETAIL where  Document_No in ( " &
               " select distinct Document_No  from TSPL_BOOKING_PAYMENT_MODE_DETAIL where Against_Receipt_No in (select distinct Against_Receipt_No  from TSPL_BOOKING_PAYMENT_MODE_DETAIL where Document_No ='" + strCode + "'" &
               " ))) Final where Document_No<>'" + strCode + "' "
                Dim dtDoc As DataTable = clsDBFuncationality.GetDataTable(qryDoc, trans)

                If dtReceipt IsNot Nothing AndAlso dtReceipt.Rows.Count > 0 Then
                    For Each dr3 As DataRow In dtReceipt.Rows
                        qry = "Update TSPL_BOOKING_PAYMENT_MODE_DETAIL set Against_Receipt_No  =null where Against_Receipt_No='" + clsCommon.myCstr(dr3("Against_Receipt_No")) + "'"
                        isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        isSaved = clsRcptEntryHeader.ReverseAndUnpost(clsCommon.myCstr(dr3("Against_Receipt_No")), trans)
                        isSaved = clsRcptEntryHeader.fundelete(clsCommon.myCstr(dr3("Against_Receipt_No")), trans)
                    Next
                End If

                If dtDoc IsNot Nothing AndAlso dtDoc.Rows.Count > 0 Then
                    For Each dr3 As DataRow In dtDoc.Rows
                        qry = "select Posted  from TSPL_BOOKING_MATSER where Document_No ='" + clsCommon.myCstr(dr3("Document_No")) + "'"
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)), "1") = CompairStringResult.Equal Then
                            isSaved = clsBookingEntryDairySale.ReverseAndUnpost(clsCommon.myCstr(dr3("Document_No")), trans)
                        End If
                    Next
                End If
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_BOOKING_MATSER", "Document_No", "TSPL_BOOKING_DETAIL", "Document_No", "TSPL_BOOKING_PAYMENT_MODE_DETAIL", "Document_No", trans)

                'GatePass Entry
                qry = "delete from TSPL_GATEPASS_DETAIL_DAIRYSALE where Delivery_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_GATEPASS_MASTER_DAIRYSALE where Delivery_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                'GatePass Entry

                qry = "delete from TSPL_BOOKING_PAYMENT_MODE_DETAIL where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_BOOKING_DETAIL where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_BOOKING_MATSER where Document_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                'If isSaved = True Then
                '    trans.Commit()
                'Else
                '    trans.Rollback()
                'End If
            Catch ex As Exception
                'trans.Rollback()
                Throw New Exception(ex.Message)
            Finally
                qry = Nothing
            End Try
        End If
        Return isSaved

    End Function
    Public Shared Function UpdateCustomerAfterPosting_CardSale(ByVal strDocNo As String, ByVal strCustomer As String, ByVal strCustomerName As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try


            Dim strBookingNo As String = String.Empty
            Dim strDeliveryNo As String = String.Empty
            Dim strARInvoiceNo As String = String.Empty
            Dim strDispatchNo As String = String.Empty
            Dim strInvoiceNo As String = String.Empty
            Dim strRemarks As String = String.Empty
            Dim strSaleReturnNo As String = String.Empty
            Dim strqry As String = String.Empty
            Dim strReceiptNo As String = String.Empty

            '' to check bank reco
            Dim Qry As String = " select Document_No from tspl_booking_matser where  Against_Booking_No='" + strDocNo + "'  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                Qry = "Current document is used in following Booking No -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Document_No"))
                Next
                Throw New Exception(Qry)
            End If

            Qry = " select Receipt_No from TSPL_RECEIPT_HEADER where applied_receipt in (select distinct Against_Receipt_No  from TSPL_BOOKING_PAYMENT_MODE_DETAIL where Document_No='" + strDocNo + "' ) "
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Receipt Entry of Current document is used in following Apply Document of Receipt -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Receipt_No"))
                Next
                Throw New Exception(Qry)
            End If



            Qry = " select Document_No from tspl_booking_matser where  Against_Booking_No='" + strDocNo + "'  "
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    strBookingNo = clsCommon.myCstr(dr("Document_No"))
                    strDeliveryNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Delivery_No  from tspl_booking_detail where  Document_No ='" & strBookingNo & "' ", trans))
                    If clsCommon.myLen(strDeliveryNo) > 0 Then
                        strDispatchNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SHIPMENT_HEAD where Against_Delivery_Code ='" & strDeliveryNo & "' ", trans))
                        If clsCommon.myLen(strDispatchNo) > 0 Then
                            strInvoiceNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where against_shipment_no ='" & strDispatchNo & "' ", trans))
                            strARInvoiceNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_No  from TSPL_Customer_Invoice_Head where Against_Sale_No ='" & strInvoiceNo & "' ", trans))

                            strSaleReturnNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Code from TSPL_SD_SALE_RETURN_DETAIL where Invoice_Code='" & strInvoiceNo & "'", trans))
                            If clsCommon.myLen(strSaleReturnNo) > 0 Then
                                Throw New Exception("Sale Return has been created for this invoice " & strInvoiceNo)
                            End If
                        End If
                    End If


                    strRemarks = " AR invoice for customer: " + strCustomer + " - " + strCustomerName + "  For Sale Invoice No " & strInvoiceNo & " "
                    If clsCommon.myLen(strDispatchNo) > 0 Then
                        strqry = "update TSPL_JOURNAL_MASTER  set CustVend_Code= '" & strCustomer & "',CustVend_Name='" & strCustomerName & "',Remarks='" & strRemarks & "'  where Source_Doc_No='" + clsCommon.myCstr(strARInvoiceNo) + "'"
                        clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                        strqry = "update TSPL_Customer_Invoice_Head  set Customer_Code= '" & strCustomer & "',Customer_Name='" & strCustomerName & "'  where Document_No='" + clsCommon.myCstr(strARInvoiceNo) + "'"
                        clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                        strqry = "update TSPL_SD_SALE_INVOICE_HEAD  set Customer_Code= '" & strCustomer & "',Is_CustomerChanged=1 where Document_Code='" + clsCommon.myCstr(strInvoiceNo) + "'"
                        clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                        '' to update journal master ,inventory and dispatch sale table 
                        strqry = "update TSPL_JOURNAL_MASTER  set CustVend_Code= '" & strCustomer & "',CustVend_Name='" & strCustomerName & "'  where Source_Doc_No = '" & strDispatchNo & "' "
                        clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                        strqry = "update TSPL_INVENTORY_MOVEMENT_NEW  set Cust_Code= '" & strCustomer & "',Cust_Name='" & strCustomerName & "'  where Source_Doc_No = '" & strDispatchNo & "' "
                        clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                        strqry = "update TSPL_INVENTORY_MOVEMENT  set Cust_Code= '" & strCustomer & "',Cust_Name='" & strCustomerName & "'  where Source_Doc_No = '" & strDispatchNo & "' "
                        clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                        strqry = "update TSPL_SD_SHIPMENT_HEAD  set Customer_Code= '" & strCustomer & "',Is_CustomerChanged=1  where Document_Code = '" & strDispatchNo & "'  "
                        clsDBFuncationality.ExecuteNonQuery(strqry, trans)


                    End If

                    strqry = "update TSPL_DELIVERY_NOTE_master_FRESHSALE  set Customer_Code= '" & strCustomer & "',Is_CustomerChanged=1  where Document_No='" + clsCommon.myCstr(strDeliveryNo) + "'"
                    clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                    strqry = "update tspl_booking_detail  set Cust_Code= '" & strCustomer & "'  where Document_No='" + clsCommon.myCstr(strBookingNo) + "'"
                    clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                    strqry = "update tspl_booking_matser  set Is_CustomerChanged=1  where Document_No='" + clsCommon.myCstr(strBookingNo) + "'"
                    clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                Next


            End If




            If clsCommon.myLen(strDocNo) > 0 Then
                strBookingNo = strDocNo
                strDeliveryNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Delivery_No  from tspl_booking_detail where  Document_No ='" & strBookingNo & "' ", trans))
                strReceiptNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Against_Receipt_No  from TSPL_BOOKING_PAYMENT_MODE_DETAIL where  Document_No ='" & strBookingNo & "' ", trans))
                If clsCommon.myLen(strDeliveryNo) > 0 Then
                    strDispatchNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SHIPMENT_HEAD where Against_Delivery_Code ='" & strDeliveryNo & "' ", trans))
                    If clsCommon.myLen(strDispatchNo) > 0 Then
                        strInvoiceNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where against_shipment_no ='" & strDispatchNo & "' ", trans))
                        strARInvoiceNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_No  from TSPL_Customer_Invoice_Head where Against_Sale_No ='" & strInvoiceNo & "' ", trans))

                        strSaleReturnNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Code from TSPL_SD_SALE_RETURN_DETAIL where Invoice_Code='" & strInvoiceNo & "'", trans))
                        If clsCommon.myLen(strSaleReturnNo) > 0 Then
                            Throw New Exception("Sale Return has been created for this invoice " & strInvoiceNo)
                        End If
                    End If
                End If

                If clsCommon.myLen(strReceiptNo) > 0 Then
                    strqry = "update TSPL_JOURNAL_MASTER  set CustVend_Code= '" & strCustomer & "',CustVend_Name='" & strCustomerName & "'  where Source_Doc_No='" + clsCommon.myCstr(strReceiptNo) + "'"
                    clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                    strqry = "update TSPL_RECEIPT_HEADER  set Cust_Code= '" & strCustomer & "',Customer_Name='" & strCustomerName & "'  where Receipt_No='" + clsCommon.myCstr(strReceiptNo) + "'"
                    clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                End If
                strRemarks = " AR invoice for customer: " + strCustomer + " - " + strCustomerName + "  For Sale Invoice No " & strInvoiceNo & " "
                If clsCommon.myLen(strDispatchNo) > 0 Then
                    strqry = "update TSPL_JOURNAL_MASTER  set CustVend_Code= '" & strCustomer & "',CustVend_Name='" & strCustomerName & "',Remarks='" & strRemarks & "'  where Source_Doc_No='" + clsCommon.myCstr(strARInvoiceNo) + "'"
                    clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                    strqry = "update TSPL_Customer_Invoice_Head  set Customer_Code= '" & strCustomer & "',Customer_Name='" & strCustomerName & "'  where Document_No='" + clsCommon.myCstr(strARInvoiceNo) + "'"
                    clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                    strqry = "update TSPL_SD_SALE_INVOICE_HEAD  set Customer_Code= '" & strCustomer & "',Is_CustomerChanged=1 where Document_Code='" + clsCommon.myCstr(strInvoiceNo) + "'"
                    clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                    '' to update journal master ,inventory and dispatch sale table 
                    strqry = "update TSPL_JOURNAL_MASTER  set CustVend_Code= '" & strCustomer & "',CustVend_Name='" & strCustomerName & "'  where Source_Doc_No = '" & strDispatchNo & "' "
                    clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                    strqry = "update TSPL_INVENTORY_MOVEMENT_NEW  set Cust_Code= '" & strCustomer & "',Cust_Name='" & strCustomerName & "'  where Source_Doc_No = '" & strDispatchNo & "' "
                    clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                    strqry = "update TSPL_INVENTORY_MOVEMENT  set Cust_Code= '" & strCustomer & "',Cust_Name='" & strCustomerName & "'  where Source_Doc_No = '" & strDispatchNo & "' "
                    clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                    strqry = "update TSPL_SD_SHIPMENT_HEAD  set Customer_Code= '" & strCustomer & "',Is_CustomerChanged=1  where Document_Code = '" & strDispatchNo & "'  "
                    clsDBFuncationality.ExecuteNonQuery(strqry, trans)


                End If
                If clsCommon.myLen(strDeliveryNo) > 0 Then
                    strqry = "update TSPL_DELIVERY_NOTE_master_FRESHSALE  set Customer_Code= '" & strCustomer & "',Is_CustomerChanged=1  where Document_No='" + clsCommon.myCstr(strDeliveryNo) + "'"
                    clsDBFuncationality.ExecuteNonQuery(strqry, trans)
                End If

                strqry = "update tspl_booking_detail  set Cust_Code= '" & strCustomer & "'  where Document_No='" + clsCommon.myCstr(strBookingNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                strqry = "update tspl_booking_matser  set Is_CustomerChanged=1  where Document_No='" + clsCommon.myCstr(strBookingNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(strqry, trans)


            End If
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function


    ''richa MIL/14/05/19-000082
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Qry As String = "select Posted from TSPL_BOOKING_MATSER where Document_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select location_code,Document_Date from TSPL_BOOKING_MATSER where Document_No='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmbookingdairy, clsCommon.myCstr(dt.Rows(0)("location_code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmDairyBookingCustomer, clsCommon.myCstr(dt.Rows(0)("location_code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)
                'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmbookingdairyFreshSale, clsCommon.myCstr(dt.Rows(0)("location_code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)

            End If

            dt = Nothing
            '' to check Delivery order
            Qry = " select Document_No from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.booking_no='" & strCode & "'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(dt.Rows(0)("Document_No")), "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE", "Document_No", "TSPL_DELIVERY_NOTE_detail_FRESHSALE", "Document_No", trans)
                ''richa agarwal no need to ask this 
                'Qry = "Current document is used in following Delivery Order -"
                'For Each dr As DataRow In dt.Rows
                '    Qry += Environment.NewLine + clsCommon.myCstr(dr("Document_No"))
                'Next
                'If clsCommon.MyMessageBoxShow(Qry + " .Do u want to reverse its Delivery order ?", "ReverseOfBooking", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                '' to check Dispatch
                Qry = " select distinct document_code from TSPL_SD_SHIPMENT_DETAIL where TSPL_SD_SHIPMENT_DETAIL.Delivery_Code in (select Document_No from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.booking_no='" & strCode & "')"
                dt = clsDBFuncationality.GetDataTable(Qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Qry = "Current document is used in following Dairy Dispatch -"
                    For Each dr As DataRow In dt.Rows
                        Qry += Environment.NewLine + clsCommon.myCstr(dr("document_code"))
                    Next
                    Throw New Exception(Qry)
                Else
                    Qry = "delete from TSPL_TRANSACTION_APPROVAL where Document_No in (select Document_No from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.booking_no='" & strCode & "')  and Program_Code ='DEL-NOTE-FS' "
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                    Qry = "delete from TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE where Document_No in (select Document_No from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.booking_no='" & strCode & "')"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                    Qry = "delete from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where Document_No  in (select Document_No from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.booking_no='" & strCode & "')"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                    Qry = "delete from TSPL_CUSTOM_FIELD_VALUES where Program_Code='DEL-NOTE-FS' and Transaction_Code  in (select Document_No from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.booking_no='" & strCode & "') "
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                    Qry = "Update TSPL_BOOKING_MATSER set Posted = 0 where Document_No='" + strCode + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    '' to set booking status =1 means Unposted in detail table of booking
                    Qry = "Update TSPL_BOOKING_DETAIL set Booking_Status = 1,Delivery_No =null where Document_No='" + strCode + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                End If
                'End If
            Else

                Qry = "Update TSPL_BOOKING_MATSER set Posted = 0 where Document_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                '' to set booking status =1 means Unposted in detail table of booking
                Qry = "Update TSPL_BOOKING_DETAIL set Booking_Status = 1 where Document_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_BOOKING_MATSER", "Document_No", "TSPL_BOOKING_DETAIL", "Document_No", "TSPL_BOOKING_PAYMENT_MODE_DETAIL", "Document_No", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsBookingDetailDairySale
#Region "Variable"
    Public Price_IdStartDate As DateTime?
    Public PricePlanNo As String = String.Empty
    Public Item_Price_ID As Integer = 0
    Public Document_No As String = Nothing
    Public Against_DemandBooking_TR_Code As String = String.Empty
    Public Against_DemandBooking_No As String = String.Empty
    Public Line_No As Integer
    Public Cust_Code As String = Nothing
    Public Loc_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Booking_Qty As Double = 0
    Public DO_Qty As Double = 0
    Public DocumentAmount As Double = 0
    Public Short_Description As String = Nothing
    Public Unit_code As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public Item_Rate As Double = 0
    Public PreviousBookingQty As Double = 0
    Public Total_Qty As Double = 0
    Public Sampling As Integer = 0
    Public Disc_Scheme_Code As String = Nothing
    Public Disc_Scheme_Type As String = Nothing
    Public Disc_Scheme_Pers As Double = 0
    Public Disc_Scheme_Amount As Double = 0
    Public OrgRate As Double = 0
    Public Booking_Status As Integer = 1
    Public CreditApproval_Reqd As String = Nothing
    Public Posted As Integer = 0
    Public SchemeType As String = Nothing
    Public Scheme_Item_Code As String = Nothing
    Public Scheme_Qty As Double = 0
    Public Scheme_Item_UOM As String = Nothing
    Public Item_Basic_Rate As Double = 0
    Public SellingPrice As Double = 0
    Public Scheme_Code As String = Nothing
    Public Tax_On_Amount As Double = 0
    Public Tax_Amount As Double = 0
    Public Tax_NonTax As Double = 0
    Public FreshAmbient As String = ""
    Public Remarks As String = ""
    Public Route_No As String = Nothing
    Public Price_with_Tax As Double = 0
    Public Amount_with_Tax As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsBookingDetailDairySale), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean, ByVal Docdate As String) As Boolean
        Dim LineNo As Integer = 0
        Dim SchemeType As String = Nothing
        Dim Scheme_Item_Code As String = Nothing
        Dim Scheme_Qty As Double = 0
        Dim Scheme_Item_UOM As String = Nothing
        Dim arrRepeat As New List(Of String)
        Dim qry As String = ""
        Dim dtBooking As DataTable = Nothing
        Dim dtGatePass As DataTable = Nothing
        Dim ArrGP As New List(Of clsGatePassDairySaleDetail)
        Dim dtBookingScheme As DataTable = Nothing
        Dim dtGatePassScheme As DataTable = Nothing
        Dim ArrGPScheme As New List(Of clsGatePassDairySaleDetail)
        
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBookingDetailDairySale In Arr
                If arrRepeat.Contains(obj.Cust_Code) Then
                    LineNo += 1
                Else
                    arrRepeat.Add(obj.Cust_Code)
                    LineNo = 0
                    LineNo += 1
                End If


                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", LineNo) ' obj.Line_No
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "PreviousBookingQty", obj.PreviousBookingQty)
                clsCommon.AddColumnsForChange(coll, "Booking_Qty", obj.Booking_Qty)
                clsCommon.AddColumnsForChange(coll, "DO_Qty", obj.Booking_Qty)
                clsCommon.AddColumnsForChange(coll, "Short_Description", obj.Short_Description)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
                clsCommon.AddColumnsForChange(coll, "DocumentAmount", obj.DocumentAmount)
                clsCommon.AddColumnsForChange(coll, "Item_Rate", obj.Item_Rate)
                clsCommon.AddColumnsForChange(coll, "Total_Qty", obj.Total_Qty)
                clsCommon.AddColumnsForChange(coll, "Sampling", obj.Sampling)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Amount", obj.Disc_Scheme_Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Code", obj.Disc_Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Pers", obj.Disc_Scheme_Pers)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Type", obj.Disc_Scheme_Type)
                clsCommon.AddColumnsForChange(coll, "Against_DemandBooking_No", obj.Against_DemandBooking_No, True)
                clsCommon.AddColumnsForChange(coll, "Against_DemandBooking_TR_Code", obj.Against_DemandBooking_TR_Code, True)

                clsCommon.AddColumnsForChange(coll, "OrgRate", obj.OrgRate)
                clsCommon.AddColumnsForChange(coll, "Booking_Status", obj.Booking_Status)
                clsCommon.AddColumnsForChange(coll, "CreditApproval_Reqd", obj.CreditApproval_Reqd)
                clsCommon.AddColumnsForChange(coll, "Scheme_Type", obj.SchemeType)
                clsCommon.AddColumnsForChange(coll, "Item_Selling_Price", obj.SellingPrice)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item", "N")
                clsCommon.AddColumnsForChange(coll, "FOC_Item", "0")

                clsCommon.AddColumnsForChange(coll, "Tax_On_Amount", obj.Tax_On_Amount)
                clsCommon.AddColumnsForChange(coll, "Tax_Amount", obj.Tax_Amount)

                clsCommon.AddColumnsForChange(coll, "Tax_NonTax", obj.Tax_NonTax)
                clsCommon.AddColumnsForChange(coll, "FreshAmbient", obj.FreshAmbient)
                clsCommon.AddColumnsForChange(coll, "Price_with_Tax", obj.Price_with_Tax)
                clsCommon.AddColumnsForChange(coll, "Amount_with_Tax", obj.Amount_with_Tax)

                'Ticket No- ERO/12/07/18-000371
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                'Ticket No- ERO/12/07/18-000371
                '==============Added by preeti Gupta Against Ticket No[BHA/01/08/18-000206]
                clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
                '==========================================================
                If clsCommon.myLen(obj.Price_IdStartDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Price_IdStartDate", clsCommon.GetPrintDate(obj.Price_IdStartDate, "dd/MMM/yyyy"), True)
                Else
                    clsCommon.AddColumnsForChange(coll, "Price_IdStartDate", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll, "PricePlanNo", obj.PricePlanNo, True)
                clsCommon.AddColumnsForChange(coll, "Item_Price_ID", obj.Item_Price_ID)

                If clsCommon.myLen(clsCommon.myCstr(obj.SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(obj.SchemeType), "Quantitive") = CompairStringResult.Equal Then

                End If


                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOKING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Dim objD As clsSchemeApplyOnDairy = Nothing
                'If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then ''richa use obj.schemetype instead of SchemeType variable becuase scheme quantity will remain always 0 due to this 
                objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(obj.Item_Code), clsCommon.myCstr(obj.Unit_code), clsCommon.myCstr(obj.Booking_Qty), obj.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)
                If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then

                    For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                        If clsCommon.myLen(clsCommon.myCstr(obj.SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(obj.SchemeType), "Quantitive") = CompairStringResult.Equal Then
                            Dim colll As New Hashtable()
                            Dim DocAmt As Integer = 0
                            LineNo += 1
                            clsCommon.AddColumnsForChange(colll, "Document_No", strDocNo)
                            clsCommon.AddColumnsForChange(colll, "Line_No", LineNo) 'obj.Line_No
                            clsCommon.AddColumnsForChange(colll, "Cust_Code", obj.Cust_Code)
                            clsCommon.AddColumnsForChange(colll, "Loc_Code", obj.Loc_Code)
                            clsCommon.AddColumnsForChange(colll, "Item_Code", objtrScheme.Schm_Icode)
                            clsCommon.AddColumnsForChange(colll, "Unit_code", objtrScheme.Schm_Item_Uom)
                            clsCommon.AddColumnsForChange(colll, "Booking_Qty", obj.Scheme_Qty)
                            clsCommon.AddColumnsForChange(colll, "DO_Qty", obj.Scheme_Qty)
                            ' clsCommon.AddColumnsForChange(colll, "Short_Description", obj.Short_Description)
                            clsCommon.AddColumnsForChange(colll, "Vehicle_Code", obj.Vehicle_Code)
                            clsCommon.AddColumnsForChange(colll, "Booking_Status", obj.Booking_Status)
                            clsCommon.AddColumnsForChange(colll, "Total_Qty", obj.Scheme_Qty)

                            clsCommon.AddColumnsForChange(colll, "Scheme_Type", "")
                            clsCommon.AddColumnsForChange(colll, "Scheme_Item_Code", obj.Item_Code)
                            clsCommon.AddColumnsForChange(colll, "Scheme_Qty", obj.Booking_Qty)
                            clsCommon.AddColumnsForChange(colll, "Scheme_Item_UOM", obj.Unit_code)
                            clsCommon.AddColumnsForChange(colll, "Scheme_Code", objtrScheme.Schm_Code)

                            clsCommon.AddColumnsForChange(colll, "Scheme_Item", "Y")
                            clsCommon.AddColumnsForChange(colll, "FOC_Item", "1")
                            '================Added by preeti Gupta======================
                            clsCommon.AddColumnsForChange(colll, "Route_No", obj.Route_No)
                            '==============================================================
                            If clsCommon.myLen(obj.Price_IdStartDate) > 0 Then
                                clsCommon.AddColumnsForChange(colll, "Price_IdStartDate", clsCommon.GetPrintDate(obj.Price_IdStartDate, "dd/MMM/yyyy"), True)
                            Else
                                clsCommon.AddColumnsForChange(colll, "Price_IdStartDate", Nothing, True)
                            End If
                            clsCommon.AddColumnsForChange(colll, "PricePlanNo", obj.PricePlanNo, True)
                            clsCommon.AddColumnsForChange(colll, "Item_Price_ID", obj.Item_Price_ID)

                            clsCommonFunctionality.UpdateDataTable(colll, "TSPL_BOOKING_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                        End If
                    Next
                End If
                
            Next

            'GatePass Entry
            qry = "update " & _
            "TSPL_GATEPASS_DETAIL_DAIRYSALE SET " & _
            "TSPL_GATEPASS_DETAIL_DAIRYSALE.Qty = BD.Booking_Qty " & _
            "from " & _
            "(select Document_No,Item_Code,Unit_code,Vehicle_Code,sum(Booking_Qty) Booking_Qty from TSPL_BOOKING_DETAIL " & _
            "where TSPL_BOOKING_DETAIL.FOC_Item <> 1 " & _
            "and TSPL_BOOKING_DETAIL.document_no='" + strDocNo + "'" & _
            "group by Document_No,Item_Code,Unit_code,Vehicle_Code " & _
            ")BD " & _
            "inner join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=BD.Document_No " & _
            "and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code=bd.Vehicle_Code " & _
            "where BD.Document_No = TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " & _
            "and BD.Document_No=TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code " & _
            "and BD.Item_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code " & _
            "and BD.Unit_code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code " & _
            "and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code=BD.Vehicle_Code " & _
            "and TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " & _
            "and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No " & _
            "and TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item<>1 " & _
            "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code='" + strDocNo + "' "

            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'Scheme
            qry = "update " & _
        "TSPL_GATEPASS_DETAIL_DAIRYSALE SET " & _
        "TSPL_GATEPASS_DETAIL_DAIRYSALE.Qty = BD.Booking_Qty " & _
        "from " & _
        "(select Document_No,Item_Code,Unit_code,Vehicle_Code,sum(Booking_Qty) Booking_Qty from TSPL_BOOKING_DETAIL " & _
        "where TSPL_BOOKING_DETAIL.FOC_Item = 1 " & _
        "and TSPL_BOOKING_DETAIL.document_no='" + strDocNo + "'" & _
        "group by Document_No,Item_Code,Unit_code,Vehicle_Code " & _
        ")BD " & _
        "inner join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=BD.Document_No " & _
        "and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code=bd.Vehicle_Code " & _
        "where BD.Document_No = TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " & _
        "and BD.Document_No=TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code " & _
        "and BD.Item_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code " & _
        "and BD.Unit_code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code " & _
        "and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code=BD.Vehicle_Code " & _
        "and TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " & _
        "and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No " & _
        "and TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item=1 " & _
        "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code='" + strDocNo + "' "

            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'Scheme


            qry = "delete from TSPL_GATEPASS_DETAIL_DAIRYSALE where " & _
                "Not exists " & _
                "( " & _
                "select TSPL_BOOKING_DETAIL.Document_No,TSPL_BOOKING_DETAIL.Item_Code,TSPL_BOOKING_DETAIL.Unit_code,TSPL_BOOKING_DETAIL.Vehicle_Code " & _
                ",sum(Booking_Qty) Booking_Qty from TSPL_BOOKING_DETAIL " & _
                "inner join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=TSPL_BOOKING_DETAIL.Document_No " & _
                "and  TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code = TSPL_BOOKING_DETAIL.Vehicle_Code " & _
                "where TSPL_BOOKING_DETAIL.FOC_Item <> 1 " & _
                "and TSPL_BOOKING_DETAIL.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " & _
                "and TSPL_BOOKING_DETAIL.Item_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code " & _
                "and TSPL_BOOKING_DETAIL.Unit_code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code " & _
                "and TSPL_BOOKING_DETAIL.document_no='" + strDocNo + "' " & _
                "and TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " & _
                "and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No " & _
                "group by TSPL_BOOKING_DETAIL.Document_No,TSPL_BOOKING_DETAIL.Item_Code,TSPL_BOOKING_DETAIL.Unit_code,TSPL_BOOKING_DETAIL.Vehicle_Code " & _
                ") " & _
                "and TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item<>1 " & _
                "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code='" + strDocNo + "' "

            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'Scheme
            qry = "delete from TSPL_GATEPASS_DETAIL_DAIRYSALE where " & _
                "Not exists " & _
                "( " & _
                "select TSPL_BOOKING_DETAIL.Document_No,TSPL_BOOKING_DETAIL.Item_Code,TSPL_BOOKING_DETAIL.Unit_code,TSPL_BOOKING_DETAIL.Vehicle_Code " & _
                ",sum(Booking_Qty) Booking_Qty from TSPL_BOOKING_DETAIL " & _
                "inner join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=TSPL_BOOKING_DETAIL.Document_No " & _
                "and  TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code = TSPL_BOOKING_DETAIL.Vehicle_Code " & _
                "where TSPL_BOOKING_DETAIL.FOC_Item = 1 " & _
                "and TSPL_BOOKING_DETAIL.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " & _
                "and TSPL_BOOKING_DETAIL.Item_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code " & _
                "and TSPL_BOOKING_DETAIL.Unit_code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code " & _
                "and TSPL_BOOKING_DETAIL.document_no='" + strDocNo + "' " & _
                "and TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " & _
                "and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No " & _
                "group by TSPL_BOOKING_DETAIL.Document_No,TSPL_BOOKING_DETAIL.Item_Code,TSPL_BOOKING_DETAIL.Unit_code,TSPL_BOOKING_DETAIL.Vehicle_Code " & _
                ") " & _
                "and TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item=1 " & _
                "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code='" + strDocNo + "' "

            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'Scheme

            qry = "delete from TSPL_GATEPASS_MASTER_DAIRYSALE where " & _
                  " Not exists " & _
                  "(select Document_No,Delivery_Code from TSPL_GATEPASS_DETAIL_DAIRYSALE " & _
                  "where TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No = TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No " & _
                  "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code) " & _
                  "and TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code='" + strDocNo + "' "

            clsDBFuncationality.ExecuteNonQuery(qry, trans)



            'Insert New Item in a dispatch
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_GATEPASS_MASTER_DAIRYSALE where Delivery_Code='" + strDocNo + "'", trans)) > 0 Then

                qry = "select Document_No AS BKNO,Vehicle_Code,Item_Code,Unit_code,sum(Booking_Qty) as Booking_Qty from TSPL_BOOKING_DETAIL where TSPL_BOOKING_DETAIL.FOC_Item<>1 and Document_No='" + strDocNo + "' group by Document_No,Vehicle_Code,Item_Code,Unit_code order by Item_Code"
                dtBooking = clsDBFuncationality.GetDataTable(qry, trans)

                qry = "select TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No AS GPNO,TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code as BKNO,TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code " & _
                   ",TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code " & _
                   "from TSPL_GATEPASS_MASTER_DAIRYSALE inner join TSPL_GATEPASS_DETAIL_DAIRYSALE " & _
                   "on TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No=TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No " & _
                   "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code " & _
                   "where TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item<>1 and TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code='" + strDocNo + "'  order by TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code"
                dtGatePass = clsDBFuncationality.GetDataTable(qry, trans)

                For jj As Integer = 0 To dtBooking.Rows.Count() - 1

                    Dim result As DataRow() = dtGatePass.Select("BKNO ='" & clsCommon.myCstr(dtBooking.Rows(jj).Item("BKNO")) & "' AND Vehicle_Code = '" & clsCommon.myCstr(dtBooking.Rows(jj).Item("Vehicle_Code")) & "' AND Item_Code='" & clsCommon.myCstr(dtBooking.Rows(jj).Item("Item_Code")) & "' AND Unit_code='" & clsCommon.myCstr(dtBooking.Rows(jj).Item("Unit_code")) & "'")

                    If ((clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_GATEPASS_MASTER_DAIRYSALE where Delivery_Code='" & clsCommon.myCstr(dtBooking.Rows(jj).Item("BKNO")) & "' AND Vehicle_Code = '" & clsCommon.myCstr(dtBooking.Rows(jj).Item("Vehicle_Code")) & "'", trans)) > 0) And (result.Count = 0)) Then
                        ''''
                        Dim objTr As New clsGatePassDairySaleDetail()
                        'If (clsCommon.myCdbl(grow.Cells(colBookQty).Value)) > 0 Then
                        qry = "Select max(Line_No) + 1 " & _
                            "from TSPL_GATEPASS_MASTER_DAIRYSALE inner join TSPL_GATEPASS_DETAIL_DAIRYSALE " & _
                            "on TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No=TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No " & _
                            "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code " & _
                            "where TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code='" & clsCommon.myCstr(dtBooking.Rows(jj).Item("BKNO")) & "' and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code='" & clsCommon.myCstr(dtBooking.Rows(jj).Item("Vehicle_Code")) & "'"
                        objTr.Line_No = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans) + ArrGP.Count)
                        'clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.Item_Code = clsCommon.myCstr(dtBooking.Rows(jj).Item("Item_Code"))
                        objTr.Delivery_Code = clsCommon.myCstr(dtBooking.Rows(jj).Item("BKNO"))
                        objTr.Unit_code = clsCommon.myCstr(dtBooking.Rows(jj).Item("Unit_code"))
                        objTr.Qty = clsCommon.myCdbl(dtBooking.Rows(jj).Item("Booking_Qty"))
                        objTr.Scheme_Item = "N"
                        objTr.FOC_Item = 0
                        qry = "select Document_No from TSPL_GATEPASS_MASTER_DAIRYSALE " & _
                               "where TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code='" & clsCommon.myCstr(dtBooking.Rows(jj).Item("BKNO")) & "' and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code='" & clsCommon.myCstr(dtBooking.Rows(jj).Item("Vehicle_Code")) & "'"
                        objTr.Document_No = clsDBFuncationality.getSingleValue(qry, trans)

                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            ArrGP.Add(objTr)
                        End If
                        'End If
                        ''''
                    End If
                Next

                If ArrGP.Count > 0 Then
                    clsGatePassDairySaleDetail.SaveData("", ArrGP, trans, "")
                End If

                'Scheme
                qry = "select Document_No AS BKNO,Vehicle_Code,Item_Code,Unit_code,sum(Booking_Qty) as Booking_Qty from TSPL_BOOKING_DETAIL where TSPL_BOOKING_DETAIL.FOC_Item=1 and Document_No='" + strDocNo + "' group by Document_No,Vehicle_Code,Item_Code,Unit_code order by Item_Code"
                dtBookingScheme = clsDBFuncationality.GetDataTable(qry, trans)

                qry = "select TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No AS GPNO,TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code as BKNO,TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code " & _
                   ",TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code " & _
                   "from TSPL_GATEPASS_MASTER_DAIRYSALE inner join TSPL_GATEPASS_DETAIL_DAIRYSALE " & _
                   "on TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No=TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No " & _
                   "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code " & _
                   "where TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item=1 and TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code='" + strDocNo + "'  order by TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code"
                dtGatePassScheme = clsDBFuncationality.GetDataTable(qry, trans)

                If dtBookingScheme.Rows.Count() > 0 Then

                    For jj As Integer = 0 To dtBookingScheme.Rows.Count() - 1

                        Dim result As DataRow() = dtGatePassScheme.Select("BKNO ='" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("BKNO")) & "' AND Vehicle_Code = '" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Vehicle_Code")) & "' AND Item_Code='" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Item_Code")) & "'  AND Unit_code='" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Unit_code")) & "'")

                        If ((clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_GATEPASS_MASTER_DAIRYSALE where Delivery_Code='" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("BKNO")) & "' AND Vehicle_Code = '" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Vehicle_Code")) & "'", trans)) > 0) And (result.Count = 0)) Then
                            ''''
                            Dim objTr As New clsGatePassDairySaleDetail()
                            'If (clsCommon.myCdbl(grow.Cells(colBookQty).Value)) > 0 Then
                            qry = "Select max(Line_No) + 1 " & _
                                "from TSPL_GATEPASS_MASTER_DAIRYSALE inner join TSPL_GATEPASS_DETAIL_DAIRYSALE " & _
                                "on TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No=TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No " & _
                                "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code " & _
                                "where TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code='" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("BKNO")) & "' and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code='" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Vehicle_Code")) & "'"
                            objTr.Line_No = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans) + ArrGPScheme.Count)
                            'clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                            objTr.Item_Code = clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Item_Code"))
                            objTr.Delivery_Code = clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("BKNO"))
                            objTr.Unit_code = clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Unit_code"))
                            objTr.Qty = clsCommon.myCdbl(dtBookingScheme.Rows(jj).Item("Booking_Qty"))
                            objTr.Scheme_Item = "Y"
                            objTr.FOC_Item = 1

                            qry = "select Document_No from TSPL_GATEPASS_MASTER_DAIRYSALE " & _
                                   "where TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code='" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("BKNO")) & "' and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code='" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Vehicle_Code")) & "'"
                            objTr.Document_No = clsDBFuncationality.getSingleValue(qry, trans)

                            If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                                ArrGPScheme.Add(objTr)
                            End If
                            'End If
                            ''''
                        End If
                    Next
                End If

                If ArrGPScheme.Count > 0 Then
                    clsGatePassDairySaleDetail.SaveData("", ArrGPScheme, trans, "")
                End If
                'Scheme
            End If

            'Insert New Item in a dispatch
            'GatePass Entry
        End If
        Return True
        Arr = Nothing
    End Function

End Class

Public Class clsBookingDetailDairySalePaymentMode
    Public TR_CODE As String = String.Empty
    Public SNo As Integer = 0
    Public Document_No As String = String.Empty
    Public Payment_Mode As String = String.Empty
    Public Amount As Double = 0
    Public Against_Receipt_No As String = String.Empty

    Public Shared Function saveData(ByVal arrObj As List(Of clsBookingDetailDairySalePaymentMode), ByVal strQCNo As String, ByVal strDocDate As Date, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                For Each obj As clsBookingDetailDairySalePaymentMode In arrObj
                    coll = New Hashtable()
                    obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, strDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                    clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                    clsCommon.AddColumnsForChange(coll, "Document_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Payment_Mode", obj.Payment_Mode)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "Against_Receipt_No", obj.Against_Receipt_No, True)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOKING_PAYMENT_MODE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function

    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction) As List(Of clsBookingDetailDairySalePaymentMode)
        Try
            Dim arrObj As List(Of clsBookingDetailDairySalePaymentMode) = Nothing
            Dim obj As clsBookingDetailDairySalePaymentMode = Nothing
            Dim qry As String = "Select * from TSPL_BOOKING_PAYMENT_MODE_DETAIL where Document_No='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsBookingDetailDairySalePaymentMode)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsBookingDetailDairySalePaymentMode()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.Payment_Mode = clsCommon.myCstr(dt.Rows(i)("Payment_Mode"))
                    obj.Amount = clsCommon.myCdbl(dt.Rows(i)("Amount"))
                    obj.SNo = clsCommon.myCdbl(dt.Rows(i)("SNo"))
                    obj.Against_Receipt_No = clsCommon.myCstr(dt.Rows(i)("Against_Receipt_No"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class


