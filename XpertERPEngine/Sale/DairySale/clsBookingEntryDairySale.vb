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
    Public RoundOffAmount As Double = 0
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
    Public Modified_Date As DateTime?
    Public Modified_By As String = String.Empty
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
    Public Is_DCS As Integer = 0
    Public Is_BPL As Integer = 0
    Public Is_Distributor As Integer = 0
    Public BPL_Coupon_Code As String = String.Empty
    Public BPL_Name As String = String.Empty
    Public BPL_Remark As String = String.Empty
    Public BPL_Category As String = String.Empty
    Public BPL_Coupon_Date As Date? = Nothing
    Public Is_Credit_Customer As String = String.Empty
    Public Sub_Location_code As String = String.Empty
    Public LastCollectionDate As Date? = Nothing
    Public TCSBaseAmt As Double = 0
    Public TCSAmount As Double = 0
    Public Total_Amt As Double = 0
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing
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
    Public Terms_Code As String = Nothing
    Public TermsName As String = Nothing
    Public Due_Date As DateTime? = Nothing
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public Distributor_Commission_TotalAmt As Decimal = 0
    Public Security_TotalAmt As Decimal = 0



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
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy HH:mm:ss"))

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
            clsCommon.AddColumnsForChange(coll, "Is_DCS", obj.Is_DCS, True)
            clsCommon.AddColumnsForChange(coll, "Is_BPL", obj.Is_BPL, True)
            clsCommon.AddColumnsForChange(coll, "Is_Distributor", obj.Is_Distributor, True)
            clsCommon.AddColumnsForChange(coll, "BPL_Coupon_Code", obj.BPL_Coupon_Code, True)
            clsCommon.AddColumnsForChange(coll, "BPL_Name", obj.BPL_Name, True)
            clsCommon.AddColumnsForChange(coll, "BPL_Category", obj.BPL_Category, True)
            clsCommon.AddColumnsForChange(coll, "BPL_Remark", obj.BPL_Remark, True)
            clsCommon.AddColumnsForChange(coll, "Sub_Location_code", obj.Sub_Location_code, True)
            If obj.BPL_Coupon_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "BPL_Coupon_Date", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "BPL_Coupon_Date", clsCommon.GetPrintDate(obj.BPL_Coupon_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "TCSBaseAmt", obj.TCSBaseAmt, True)
            clsCommon.AddColumnsForChange(coll, "TCSAmount", obj.TCSAmount, True)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt, True)
            clsCommon.AddColumnsForChange(coll, "Is_Credit_Customer", obj.Is_Credit_Customer, True)
            If obj.LastCollectionDate Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "LastCollectionDate", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "LastCollectionDate", clsCommon.GetPrintDate(obj.LastCollectionDate, "dd/MMM/yyyy"))
            End If
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
            clsCommon.AddColumnsForChange(coll, "Distributor_Commission_TotalAmt", obj.Distributor_Commission_TotalAmt)
            clsCommon.AddColumnsForChange(coll, "Security_TotalAmt", obj.Security_TotalAmt)
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", obj.RoundOffAmount)

            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)


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
            If isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_BOOKING_MATSER", "Document_No", "TSPL_BOOKING_DETAIL", "Document_No", "TSPL_BOOKING_PAYMENT_MODE_DETAIL", "Document_No", trans)

            End If

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
        Dim ShowDemandDoc As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowDemandDoc, clsFixedParameterCode.ShowDemandDoc, Trans)) = 1, True, False)

        Dim qry As String = "select distinct TSPL_BOOKING_MATSER.Against_DemandBooking_No,TSPL_BOOKING_MATSER.Ship_To_Location,TSPL_BOOKING_MATSER.Created_Date,TSPL_BOOKING_MATSER.AdvanceAmount,TSPL_BOOKING_MATSER.Against_Receipt_No,TSPL_BOOKING_MATSER.Against_Booking_No,TSPL_BOOKING_MATSER.Payment_Mode,TSPL_BOOKING_MATSER.Reference_No,TSPL_BOOKING_MATSER.Counter_No,TSPL_BOOKING_MATSER.IsSampling,TSPL_BOOKING_MATSER.AgainstGatePass,Document_No,Document_Date,Posted,CreateDO_Automatic,location_code,Cust_Group_Code,Is_Taxable,TRANSACTION_TYPE,Ex_Factory_Date,isnull(CustPO_No,'') as CustPO_No,custpo_date,isnull(SalesmanCode,'') as SalesmanCode,Total_Can,total_Box,Total_Crate,isnull(Is_Cancelled,0) as Is_Cancelled, isnull(Booking_Type,'') as Booking_Type,isnull(Card_SALE_No,'') as Card_SALE_No,CardSale_FROM_DATE,CardSale_TO_DATE,Uploading_date " &
            " ,isnull(Credit_Limit,0) as Credit_Limit,isnull(Advance_Security,0) as Advance_Security,isnull(Revese_Adv_Security,0) as Revese_Adv_Security,isnull(AR_Credit_Security,0) as AR_Credit_Security,isnull(Pending_Posted_DO,0) as Pending_Posted_DO,isnull(UnPostedDispatch,0) as UnPostedDispatch,isnull(Ledger_Outstansing,0) as Ledger_Outstansing,isnull(Refund_Security,0) as Refund_Security,isnull(Reverse_Refund_Sec,0) as Reverse_Refund_Sec,isnull(Total_Outstanding,0) as Total_Outstanding, isnull(GatePass_Type,'') as GatePass_Type,Created_By,Is_DCS,Is_BPL,BPL_Coupon_Code,BPL_Name,BPL_Remark,BPL_Coupon_Date,Is_Distributor,BPL_Category,TCSAmount,TCSBaseAmt,Total_Amt,TSPL_BOOKING_MATSER.LastCollectionDate " &
            ",Tax_Group,TaxGroupName,Tax1,Tax1_Rate,Tax1_Base_Amt,TAX1_Amt,Tax2,Tax2_Rate,Tax2_Base_Amt,TAX2_Amt,Tax3,Tax3_Rate,Tax3_Base_Amt,TAX3_Amt,Tax4,Tax4_Rate,Tax4_Base_Amt,TAX4_Amt,Tax5,Tax5_Rate,Tax5_Base_Amt,TAX5_Amt,Discount_Base,Discount_Amt,Amount_Less_Discount,Total_Tax_Amt,Total_Amt,Distributor_Commission_TotalAmt,Security_TotalAmt,RoundOffAmount,Sub_Location_code from TSPL_BOOKING_MATSER where 2=2 and "

        '-------richa 12/08/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        Dim whrClas As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(NavType).ToUpper(), "CURRENT") <> CompairStringResult.Equal Then
            strwherecls = Xtra.CustomerPermission()
            If clsCommon.myLen(strwherecls) > 0 Then
                whrClas = " and TSPL_BOOKING_DETAIL.Cust_Code in (" + strwherecls + ") "
            End If
        End If
        If ShowDemandDoc Then
            whrClas += " and TSPL_BOOKING_MATSER.Against_DemandBooking_No is null "
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

        If ShowDemandDoc Then
            Select Case NavType

                Case NavigatorType.Current
                    qry += "  Document_No='" + strDocumentNo + "'"
                Case NavigatorType.Next
                    qry += " Document_No in (select isnull(min(TSPL_BOOKING_MATSER.Document_No),'') from TSPL_BOOKING_MATSER   where TSPL_BOOKING_MATSER.From_Screen_code='" + FormId + "' and TSPL_BOOKING_MATSER.Document_No  >'" + strDocumentNo + "'  " + whrClas + ")"
                Case NavigatorType.First
                    qry += " Document_No in (select isnull(min(TSPL_BOOKING_MATSER.Document_No),'') from TSPL_BOOKING_MATSER    where TSPL_BOOKING_MATSER.From_Screen_code='" + FormId + "'  " + whrClas + ")"
                Case NavigatorType.Last
                    qry += " Document_No in (select isnull(max(TSPL_BOOKING_MATSER.Document_No),'') from TSPL_BOOKING_MATSER   where TSPL_BOOKING_MATSER.From_Screen_code='" + FormId + "'  " + whrClas + " )"
                Case NavigatorType.Previous
                    qry += " Document_No in (select isnull(max(TSPL_BOOKING_MATSER.Document_No),'') from TSPL_BOOKING_MATSER  where TSPL_BOOKING_MATSER.From_Screen_code='" + FormId + "' and  TSPL_BOOKING_MATSER.Document_No  <'" + strDocumentNo + "'  " + whrClas + ")"
            End Select
        Else
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
        End If


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
            obj.Is_DCS = clsCommon.myCdbl(dt.Rows(0)("Is_DCS"))
            obj.Is_BPL = clsCommon.myCdbl(dt.Rows(0)("Is_BPL"))
            obj.Is_Distributor = clsCommon.myCdbl(dt.Rows(0)("Is_Distributor"))
            obj.BPL_Coupon_Code = clsCommon.myCstr(dt.Rows(0)("BPL_Coupon_Code"))
            obj.BPL_Name = clsCommon.myCstr(dt.Rows(0)("BPL_Name"))
            obj.BPL_Category = clsCommon.myCstr(dt.Rows(0)("BPL_Category"))
            obj.BPL_Remark = clsCommon.myCstr(dt.Rows(0)("BPL_Remark"))
            obj.TCSBaseAmt = clsCommon.myCdbl(dt.Rows(0)("TCSBaseAmt"))
            obj.TCSAmount = clsCommon.myCdbl(dt.Rows(0)("TCSAmount"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
            obj.Sub_Location_code = clsCommon.myCstr(dt.Rows(0)("Sub_Location_code"))
            If dt.Rows(0)("BPL_Coupon_Date") IsNot DBNull.Value Then
                obj.BPL_Coupon_Date = clsCommon.myCDate(dt.Rows(0)("BPL_Coupon_Date"))
            End If
            If dt.Rows(0)("LastCollectionDate") IsNot DBNull.Value Then
                obj.LastCollectionDate = clsCommon.myCDate(dt.Rows(0)("LastCollectionDate"))
            End If


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
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
            obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Base_Amt"))
            obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
            obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
            obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Base_Amt"))
            obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
            obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Base_Amt"))
            obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
            obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Base_Amt"))
            obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
            obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
            obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
            obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Base_Amt"))
            obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
            obj.Discount_Base = clsCommon.myCdbl(dt.Rows(0)("Discount_Base"))
            obj.Discount_Amt = clsCommon.myCdbl(dt.Rows(0)("Discount_Amt"))
            obj.Amount_Less_Discount = clsCommon.myCdbl(dt.Rows(0)("Amount_Less_Discount"))
            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.Distributor_Commission_TotalAmt = clsCommon.myCdbl(dt.Rows(0)("Distributor_Commission_TotalAmt"))
            obj.Security_TotalAmt = clsCommon.myCdbl(dt.Rows(0)("Security_TotalAmt"))
            obj.Arr = clsBookingDetailDairySale.getData(obj.Document_No, Trans)
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
            End If

            dt = clsDBFuncationality.GetDataTable("select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No 
from TSPL_VENDOR_INVOICE_HEAD 
left outer join TSPL_PAYMENT_PROCESS_DEDUCTION on TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No = TSPL_VENDOR_INVOICE_HEAD.Document_No
where TSPL_VENDOR_INVOICE_HEAD.RefDocType='BOK-CRD' and TSPL_VENDOR_INVOICE_HEAD.RefDocNo='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.myLen(dt.Rows(0)("Doc_No")) > 0 Then
                    Throw New Exception("Document used in payment process no [" + clsCommon.myCstr(dt.Rows(0)("Doc_No")) + "]")
                End If
                clsVedorInvoiceHead.ReverseAndUnpost(clsCommon.myCstr(dt.Rows(0)("Document_No")), trans)
                clsVedorInvoiceHead.DeleteData(clsCommon.myCstr(dt.Rows(0)("Document_No")), trans)
            End If


            dt = Nothing
            '' to check Delivery order
            Qry = " select Document_No from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.booking_no='" & strCode & "'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(dt.Rows(0)("Document_No")), "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE", "Document_No", "TSPL_DELIVERY_NOTE_detail_FRESHSALE", "Document_No", trans)
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

    Public Shared Function CreateDebitNote(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "select isnull(sum(TSPL_BOOKING_DETAIL.Amount_with_Tax),0) as Amount_with_Tax,max(TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code) as Vendor_Code ,max(TSPL_VENDOR_MASTER.Vendor_Name) as Vendor_Name,max(TSPL_BOOKING_MATSER.Document_Date) as Document_Date,max(TSPL_LOCATION_MASTER.Loc_Segment_Code) as Loc_Segment_Code
from TSPL_BOOKING_DETAIL 
left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No
left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BOOKING_MATSER.location_code
where TSPL_BOOKING_MATSER.Document_No='" + strCode + "' and TSPL_BOOKING_MATSER.Is_DCS=1 and TSPL_BOOKING_MATSER.Booking_Type='CREDIT' "
            Dim dtBooking As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Dim dblAmt As Decimal = clsCommon.myCDecimal(dtBooking.Rows(0)("Amount_with_Tax"))
            If dblAmt > 0 Then
                Dim objVendorInvHead As New clsVedorInvoiceHead()
                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                objVendorInvHead.isDeduction = 1
                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code from TSPL_DEDUCTION_MASTER  where Is_Default_VSP_Deduction=1", trans)
                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                    Throw New Exception("Please set default VSP deduction code")
                End If
                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))

                'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(clsCommon.myCDate(dtBooking.Rows(0)("Document_Date")), "dd/MMM/yyyy")
                objVendorInvHead.Vendor_Code = clsCommon.myCstr(dtBooking.Rows(0)("Vendor_Code"))
                objVendorInvHead.Vendor_Name = clsCommon.myCstr(dtBooking.Rows(0)("Vendor_Name"))
                objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                objVendorInvHead.Invoice_Type = "AP"
                objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                objVendorInvHead.loc_code = clsCommon.myCstr(dtBooking.Rows(0)("Loc_Segment_Code"))
                'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
                objVendorInvHead.Description = "AP Debit Note Against Credit Booking : " & strCode & ""
                'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                End If

                objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                ''objVendorInvHead.PO_Number = obj.p

                '' ''added by priti
                objVendorInvHead.RefDocType = "BOK-CRD"
                objVendorInvHead.RefDocNo = strCode
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
                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objVendorInvHead.loc_code, True, trans)
                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objVendorInvHead.loc_code, True, trans)
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

                Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Deduction_ACCOUNT"))
                If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                    Throw New Exception("Please set Deduction Account for Vendor Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Acct_Set_Code")))
                End If
                strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, objVendorInvHead.loc_code, True, trans)

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
    Public IsKKFTax As String = String.Empty
    Public IsMNDTax As String = String.Empty
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
    Public Disc_Amt As Double = 0
    Public Amt_Less_Discount As Double = 0
    Public FreshAmbient As String = ""
    Public Remarks As String = ""
    Public Route_No As String = Nothing
    Public Price_with_Tax As Double = 0
    Public Amount_with_Tax As Double = 0

    Public TAX_Group As String = ""
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
    Public Distributor_Commission_PKID As String = ""
    Public Distributor_Commission_Rate As Decimal = 0
    Public Distributor_Commission_RateWithTax As Decimal = 0
    Public Distributor_Commission_Amt As Decimal = 0
    Public Security_Rate As Decimal = 0
    Public Security_Amt As Decimal = 0
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
                clsCommon.AddColumnsForChange(coll, "IsKKFTax", obj.IsKKFTax) ' obj.Line_No
                clsCommon.AddColumnsForChange(coll, "IsMNDTax", obj.IsMNDTax) ' obj.Line_No
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
                clsCommon.AddColumnsForChange(coll, "Disc_Amt", obj.Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Amt_Less_Discount", obj.Amt_Less_Discount)
                clsCommon.AddColumnsForChange(coll, "TAX_Group", obj.TAX_Group)
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
                clsCommon.AddColumnsForChange(coll, "Distributor_Commission_PKID", obj.Distributor_Commission_PKID, True)
                clsCommon.AddColumnsForChange(coll, "Distributor_Commission_Rate", obj.Distributor_Commission_Rate, True)
                clsCommon.AddColumnsForChange(coll, "Distributor_Commission_RateWithTax", obj.Distributor_Commission_RateWithTax, True)
                clsCommon.AddColumnsForChange(coll, "Distributor_Commission_Amt", obj.Distributor_Commission_Amt, True)
                clsCommon.AddColumnsForChange(coll, "Security_Rate", obj.Security_Rate, True)
                clsCommon.AddColumnsForChange(coll, "Security_Amt", obj.Security_Amt, True)
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
            qry = "update " &
            "TSPL_GATEPASS_DETAIL_DAIRYSALE SET " &
            "TSPL_GATEPASS_DETAIL_DAIRYSALE.Qty = BD.Booking_Qty " &
            "from " &
            "(select Document_No,Item_Code,Unit_code,Vehicle_Code,sum(Booking_Qty) Booking_Qty from TSPL_BOOKING_DETAIL " &
            "where TSPL_BOOKING_DETAIL.FOC_Item <> 1 " &
            "and TSPL_BOOKING_DETAIL.document_no='" + strDocNo + "'" &
            "group by Document_No,Item_Code,Unit_code,Vehicle_Code " &
            ")BD " &
            "inner join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=BD.Document_No " &
            "and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code=bd.Vehicle_Code " &
            "where BD.Document_No = TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " &
            "and BD.Document_No=TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code " &
            "and BD.Item_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code " &
            "and BD.Unit_code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code " &
            "and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code=BD.Vehicle_Code " &
            "and TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " &
            "and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No " &
            "and TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item<>1 " &
            "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code='" + strDocNo + "' "

            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'Scheme
            qry = "update " &
        "TSPL_GATEPASS_DETAIL_DAIRYSALE SET " &
        "TSPL_GATEPASS_DETAIL_DAIRYSALE.Qty = BD.Booking_Qty " &
        "from " &
        "(select Document_No,Item_Code,Unit_code,Vehicle_Code,sum(Booking_Qty) Booking_Qty from TSPL_BOOKING_DETAIL " &
        "where TSPL_BOOKING_DETAIL.FOC_Item = 1 " &
        "and TSPL_BOOKING_DETAIL.document_no='" + strDocNo + "'" &
        "group by Document_No,Item_Code,Unit_code,Vehicle_Code " &
        ")BD " &
        "inner join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=BD.Document_No " &
        "and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code=bd.Vehicle_Code " &
        "where BD.Document_No = TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " &
        "and BD.Document_No=TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code " &
        "and BD.Item_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code " &
        "and BD.Unit_code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code " &
        "and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code=BD.Vehicle_Code " &
        "and TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " &
        "and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No " &
        "and TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item=1 " &
        "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code='" + strDocNo + "' "

            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'Scheme


            qry = "delete from TSPL_GATEPASS_DETAIL_DAIRYSALE where " &
                "Not exists " &
                "( " &
                "select TSPL_BOOKING_DETAIL.Document_No,TSPL_BOOKING_DETAIL.Item_Code,TSPL_BOOKING_DETAIL.Unit_code,TSPL_BOOKING_DETAIL.Vehicle_Code " &
                ",sum(Booking_Qty) Booking_Qty from TSPL_BOOKING_DETAIL " &
                "inner join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=TSPL_BOOKING_DETAIL.Document_No " &
                "and  TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code = TSPL_BOOKING_DETAIL.Vehicle_Code " &
                "where TSPL_BOOKING_DETAIL.FOC_Item <> 1 " &
                "and TSPL_BOOKING_DETAIL.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " &
                "and TSPL_BOOKING_DETAIL.Item_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code " &
                "and TSPL_BOOKING_DETAIL.Unit_code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code " &
                "and TSPL_BOOKING_DETAIL.document_no='" + strDocNo + "' " &
                "and TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " &
                "and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No " &
                "group by TSPL_BOOKING_DETAIL.Document_No,TSPL_BOOKING_DETAIL.Item_Code,TSPL_BOOKING_DETAIL.Unit_code,TSPL_BOOKING_DETAIL.Vehicle_Code " &
                ") " &
                "and TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item<>1 " &
                "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code='" + strDocNo + "' "

            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'Scheme
            qry = "delete from TSPL_GATEPASS_DETAIL_DAIRYSALE where " &
                "Not exists " &
                "( " &
                "select TSPL_BOOKING_DETAIL.Document_No,TSPL_BOOKING_DETAIL.Item_Code,TSPL_BOOKING_DETAIL.Unit_code,TSPL_BOOKING_DETAIL.Vehicle_Code " &
                ",sum(Booking_Qty) Booking_Qty from TSPL_BOOKING_DETAIL " &
                "inner join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=TSPL_BOOKING_DETAIL.Document_No " &
                "and  TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code = TSPL_BOOKING_DETAIL.Vehicle_Code " &
                "where TSPL_BOOKING_DETAIL.FOC_Item = 1 " &
                "and TSPL_BOOKING_DETAIL.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " &
                "and TSPL_BOOKING_DETAIL.Item_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code " &
                "and TSPL_BOOKING_DETAIL.Unit_code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code " &
                "and TSPL_BOOKING_DETAIL.document_no='" + strDocNo + "' " &
                "and TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code " &
                "and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No " &
                "group by TSPL_BOOKING_DETAIL.Document_No,TSPL_BOOKING_DETAIL.Item_Code,TSPL_BOOKING_DETAIL.Unit_code,TSPL_BOOKING_DETAIL.Vehicle_Code " &
                ") " &
                "and TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item=1 " &
                "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code='" + strDocNo + "' "

            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'Scheme

            qry = "delete from TSPL_GATEPASS_MASTER_DAIRYSALE where " &
                  " Not exists " &
                  "(select Document_No,Delivery_Code from TSPL_GATEPASS_DETAIL_DAIRYSALE " &
                  "where TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No = TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No " &
                  "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code) " &
                  "and TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code='" + strDocNo + "' "

            clsDBFuncationality.ExecuteNonQuery(qry, trans)



            'Insert New Item in a dispatch
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_GATEPASS_MASTER_DAIRYSALE where Delivery_Code='" + strDocNo + "'", trans)) > 0 Then

                qry = "select Document_No AS BKNO,Vehicle_Code,Item_Code,Unit_code,sum(Booking_Qty) as Booking_Qty from TSPL_BOOKING_DETAIL where TSPL_BOOKING_DETAIL.FOC_Item<>1 and Document_No='" + strDocNo + "' group by Document_No,Vehicle_Code,Item_Code,Unit_code order by Item_Code"
                dtBooking = clsDBFuncationality.GetDataTable(qry, trans)

                qry = "select TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No AS GPNO,TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code as BKNO,TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code " &
                   ",TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code " &
                   "from TSPL_GATEPASS_MASTER_DAIRYSALE inner join TSPL_GATEPASS_DETAIL_DAIRYSALE " &
                   "on TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No=TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No " &
                   "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code " &
                   "where TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item<>1 and TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code='" + strDocNo + "'  order by TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code"
                dtGatePass = clsDBFuncationality.GetDataTable(qry, trans)

                For jj As Integer = 0 To dtBooking.Rows.Count() - 1

                    Dim result As DataRow() = dtGatePass.Select("BKNO ='" & clsCommon.myCstr(dtBooking.Rows(jj).Item("BKNO")) & "' AND Vehicle_Code = '" & clsCommon.myCstr(dtBooking.Rows(jj).Item("Vehicle_Code")) & "' AND Item_Code='" & clsCommon.myCstr(dtBooking.Rows(jj).Item("Item_Code")) & "' AND Unit_code='" & clsCommon.myCstr(dtBooking.Rows(jj).Item("Unit_code")) & "'")

                    If ((clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_GATEPASS_MASTER_DAIRYSALE where Delivery_Code='" & clsCommon.myCstr(dtBooking.Rows(jj).Item("BKNO")) & "' AND Vehicle_Code = '" & clsCommon.myCstr(dtBooking.Rows(jj).Item("Vehicle_Code")) & "'", trans)) > 0) And (result.Count = 0)) Then
                        ''''
                        Dim objTr As New clsGatePassDairySaleDetail()
                        'If (clsCommon.myCdbl(grow.Cells(colBookQty).Value)) > 0 Then
                        qry = "Select max(Line_No) + 1 " &
                            "from TSPL_GATEPASS_MASTER_DAIRYSALE inner join TSPL_GATEPASS_DETAIL_DAIRYSALE " &
                            "on TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No=TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No " &
                            "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code " &
                            "where TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code='" & clsCommon.myCstr(dtBooking.Rows(jj).Item("BKNO")) & "' and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code='" & clsCommon.myCstr(dtBooking.Rows(jj).Item("Vehicle_Code")) & "'"
                        objTr.Line_No = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans) + ArrGP.Count)
                        'clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.Item_Code = clsCommon.myCstr(dtBooking.Rows(jj).Item("Item_Code"))
                        objTr.Delivery_Code = clsCommon.myCstr(dtBooking.Rows(jj).Item("BKNO"))
                        objTr.Unit_code = clsCommon.myCstr(dtBooking.Rows(jj).Item("Unit_code"))
                        objTr.Qty = clsCommon.myCdbl(dtBooking.Rows(jj).Item("Booking_Qty"))
                        objTr.Scheme_Item = "N"
                        objTr.FOC_Item = 0
                        qry = "select Document_No from TSPL_GATEPASS_MASTER_DAIRYSALE " &
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

                qry = "select TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No AS GPNO,TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code as BKNO,TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code " &
                   ",TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code " &
                   "from TSPL_GATEPASS_MASTER_DAIRYSALE inner join TSPL_GATEPASS_DETAIL_DAIRYSALE " &
                   "on TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No=TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No " &
                   "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code " &
                   "where TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item=1 and TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code='" + strDocNo + "'  order by TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code"
                dtGatePassScheme = clsDBFuncationality.GetDataTable(qry, trans)

                If dtBookingScheme.Rows.Count() > 0 Then

                    For jj As Integer = 0 To dtBookingScheme.Rows.Count() - 1

                        Dim result As DataRow() = dtGatePassScheme.Select("BKNO ='" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("BKNO")) & "' AND Vehicle_Code = '" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Vehicle_Code")) & "' AND Item_Code='" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Item_Code")) & "'  AND Unit_code='" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Unit_code")) & "'")

                        If ((clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_GATEPASS_MASTER_DAIRYSALE where Delivery_Code='" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("BKNO")) & "' AND Vehicle_Code = '" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Vehicle_Code")) & "'", trans)) > 0) And (result.Count = 0)) Then
                            ''''
                            Dim objTr As New clsGatePassDairySaleDetail()
                            'If (clsCommon.myCdbl(grow.Cells(colBookQty).Value)) > 0 Then
                            qry = "Select max(Line_No) + 1 " &
                                "from TSPL_GATEPASS_MASTER_DAIRYSALE inner join TSPL_GATEPASS_DETAIL_DAIRYSALE " &
                                "on TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No=TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No " &
                                "and TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code=TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code " &
                                "where TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code='" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("BKNO")) & "' and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code='" & clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Vehicle_Code")) & "'"
                            objTr.Line_No = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans) + ArrGPScheme.Count)
                            'clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                            objTr.Item_Code = clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Item_Code"))
                            objTr.Delivery_Code = clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("BKNO"))
                            objTr.Unit_code = clsCommon.myCstr(dtBookingScheme.Rows(jj).Item("Unit_code"))
                            objTr.Qty = clsCommon.myCdbl(dtBookingScheme.Rows(jj).Item("Booking_Qty"))
                            objTr.Scheme_Item = "Y"
                            objTr.FOC_Item = 1

                            qry = "select Document_No from TSPL_GATEPASS_MASTER_DAIRYSALE " &
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
    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction) As List(Of clsBookingDetailDairySale)
        Try
            Dim arrObj As List(Of clsBookingDetailDairySale) = Nothing
            Dim obj As clsBookingDetailDairySale = Nothing
            Dim qry As String = "Select * from TSPL_BOOKING_DETAIL where Document_No='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsBookingDetailDairySale)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsBookingDetailDairySale()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.Cust_Code = clsCommon.myCstr(dt.Rows(i)("Cust_Code"))
                    obj.Booking_Qty = 0
                    obj.Against_DemandBooking_No = clsCommon.myCstr(dt.Rows(i)("Against_DemandBooking_No"))
                    obj.Against_DemandBooking_TR_Code = clsCommon.myCstr(dt.Rows(i)("Against_DemandBooking_TR_Code"))
                    obj.Line_No = clsCommon.myCdbl(dt.Rows(i)("Line_No"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Total_Qty = 0
                    obj.Unit_code = clsCommon.myCstr(dt.Rows(i)("Unit_code"))
                    obj.Item_Price_ID = clsCommon.myCdbl(dt.Rows(i)("Item_Price_ID"))
                    obj.Item_Rate = clsCommon.myCdbl(dt.Rows(i)("Item_Rate"))
                    obj.Loc_Code = clsCommon.myCstr(dt.Rows(i)("Loc_Code"))
                    obj.Amount_with_Tax = clsCommon.myCdbl(dt.Rows(i)("Amount_with_Tax"))
                    obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(i)("Vehicle_Code"))
                    obj.Route_No = clsCommon.myCstr(dt.Rows(i)("Route_No"))
                    obj.Distributor_Commission_PKID = clsCommon.myCstr(dt.Rows(i)("Distributor_Commission_PKID"))
                    obj.Distributor_Commission_Rate = clsCommon.myCdbl(dt.Rows(i)("Distributor_Commission_Rate"))
                    obj.Distributor_Commission_RateWithTax = clsCommon.myCdbl(dt.Rows(i)("Distributor_Commission_RateWithTax"))
                    obj.Distributor_Commission_Amt = clsCommon.myCdbl(dt.Rows(i)("Distributor_Commission_Amt"))
                    obj.Security_Rate = clsCommon.myCdbl(dt.Rows(i)("Security_Rate"))
                    obj.Security_Amt = clsCommon.myCdbl(dt.Rows(i)("Security_Amt"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
                    'obj.is = clsCommon.myCstr(dt.Rows(i)("Payment_Mode"))
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


