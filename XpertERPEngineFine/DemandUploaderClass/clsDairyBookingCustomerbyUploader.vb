Imports common
Imports System.Data.SqlClient
Imports System.IO
Public Class clsDairyBookingCustomerbyUploader
#Region "Variable"
    Public IsSampling As Integer = 0
    Public AgainstGatePass As Integer = 0
    Public Cust_Group_Code As String = Nothing
    Public Document_No As String = Nothing
    Public location_code As String = Nothing
    Public Against_DemandBooking_No As String = String.Empty
    Public Against_DCSBooking_No As String = String.Empty
    Public BookingThrough As String = Nothing
    Public Ship_To_Location As String = String.Empty
    Public Document_Date As DateTime?
    Public Posted As Integer = 0
    Public Arr As List(Of clsDairyBookingCustomerDetailbyUploader) = Nothing
    Public CreateDO_Automatic As Integer = 0
    Public Is_Taxable As Integer = 0
    Public TRANSACTION_TYPE As String = Nothing
    Public Ex_Factory_Date As Date?
    Public From_Screen_code As String = ""
    Public SalesmanCode As String = ""
    Public Podate As Date? = Nothing

    Public Cust_PO_No As String = ""
    Public TotalCAN As Integer = 0
    Public Trip_No As Integer = 0
    Public TotalBox As Integer = 0
    Public RoundOffAmount As Double = 0
    Public FAT_Per As Double = 0
    Public SNF_Per As Double = 0
    Public Acidity As Double = 0
    Public Temperature As Double = 0
    Public MBRT_Hours As Double = 0
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
    Public Distributor_Code As String = Nothing
    Public GatePass_Type As String = String.Empty
    Public Is_DCS As Integer = 0
    Public Is_BPL As Integer = 0
    Public Is_GHEE As Integer = 0
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
    Public Transporter_Commission_TotalAmt As Decimal = 0
    Public Security_TotalAmt As Decimal = 0


    Public arrBookingDetailDairySalePaymentMode As List(Of clsDairyBookingCustomerDetailbyUploaderPaymentMode) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsDairyBookingCustomerbyUploader, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsDairyBookingCustomerbyUploader, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Return SaveData(obj, isNewEntry, trans, "", False)
    End Function
    Public Function SaveData(ByVal obj As clsDairyBookingCustomerbyUploader, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, ByVal strBookingDocNo As String, ByVal IsDemandUploader As Boolean) As Boolean
        Dim qry As String = String.Empty
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For i As Integer = 0 To Arr.Count - 1
                    clsRCDFRateControl.CheckRCDFRateControl(clsCommon.myCstr(Arr(i).Item_Code), clsCommon.myCstr(Arr(i).Unit_code), clsCommon.myCDecimal(Arr(i).Item_Rate), clsCommon.myCDate(obj.Document_Date), trans)
                Next
            End If
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
            clsBatchInventory.DeleteData("PS-SH", obj.Document_No, trans)


            Dim isSaved As Boolean = True

            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_TEMP_BOOKING_MATSER", "Document_No", "TSPL_TEMP_BOOKING_DETAIL", "Document_No", "TSPL_TEMP_BOOKING_PAYMENT_MODE_DETAIL", "Document_No", trans)
            End If


            qry = "delete from TSPL_TEMP_BOOKING_DETAIL where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_TEMP_BOOKING_PAYMENT_MODE_DETAIL where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry AndAlso clsCommon.myLen(clsCommon.myCstr(strBookingDocNo)) > 0 Then
                obj.Document_No = strBookingDocNo
            ElseIf isNewEntry = True Then
                If IsDemandUploader Then
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmDairySaleBookingUploader, "", obj.location_code)
                Else
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmDairySaleBooking, "", obj.location_code)
                End If
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
            clsCommon.AddColumnsForChange(coll, "Against_DCSBooking_No", obj.Against_DCSBooking_No, True)

            clsCommon.AddColumnsForChange(coll, "Credit_Limit", obj.Credit_Limit)
            clsCommon.AddColumnsForChange(coll, "FAT_Per", obj.FAT_Per)
            clsCommon.AddColumnsForChange(coll, "SNF_Per", obj.SNF_Per)
            clsCommon.AddColumnsForChange(coll, "Acidity", obj.Acidity)
            clsCommon.AddColumnsForChange(coll, "Temperature", obj.Temperature)
            clsCommon.AddColumnsForChange(coll, "MBRT_Hours", obj.MBRT_Hours)
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
            clsCommon.AddColumnsForChange(coll, "Is_GHEE", obj.Is_GHEE, True)
            clsCommon.AddColumnsForChange(coll, "Is_Distributor", obj.Is_Distributor, True)
            clsCommon.AddColumnsForChange(coll, "BPL_Coupon_Code", obj.BPL_Coupon_Code, True)
            clsCommon.AddColumnsForChange(coll, "BPL_Name", obj.BPL_Name, True)
            clsCommon.AddColumnsForChange(coll, "BPL_Category", obj.BPL_Category, True)
            clsCommon.AddColumnsForChange(coll, "BPL_Remark", obj.BPL_Remark, True)
            clsCommon.AddColumnsForChange(coll, "Sub_Location_code", obj.Sub_Location_code, True)
            clsCommon.AddColumnsForChange(coll, "Trip_No", obj.Trip_No, True)
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
            clsCommon.AddColumnsForChange(coll, "Transporter_Commission_TotalAmt", obj.Transporter_Commission_TotalAmt)
            clsCommon.AddColumnsForChange(coll, "Security_TotalAmt", obj.Security_TotalAmt)
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", obj.RoundOffAmount)
            clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
            clsCommon.AddColumnsForChange(coll, "CreateDO_Automatic", obj.CreateDO_Automatic)
            clsCommon.AddColumnsForChange(coll, "Amount_After_Commission", 0)
            clsCommon.AddColumnsForChange(coll, "OTP_Confirm", "0")
            clsCommon.AddColumnsForChange(coll, "IsDispatched", "0")
            clsCommon.AddColumnsForChange(coll, "Distributor_Code", obj.Distributor_Code, True)
            clsCommon.AddColumnsForChange(coll, "User_Lock_For_Edit", 0)
            clsCommon.AddColumnsForChange(coll, "LockedBy_UserCode", "")
            clsCommon.AddColumnsForChange(coll, "Is_Cancelled", 0)
            'clsCommon.AddColumnsForChange(coll, "AdvanceAmount", 0)
            clsCommon.AddColumnsForChange(coll, "TruckSheetGenerate", 0)
            'clsCommon.AddColumnsForChange(coll, "AgainstGatePass", 0)
            clsCommon.AddColumnsForChange(coll, "isBookingCreatedForNextDay", 0)
            'clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)

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
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TEMP_BOOKING_MATSER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TEMP_BOOKING_MATSER", OMInsertOrUpdate.Update, "TSPL_TEMP_BOOKING_MATSER.Document_No='" + obj.Document_No + "'", trans)
            End If

            isSaved = isSaved AndAlso clsDairyBookingCustomerDetailbyUploader.SaveData(obj.Document_No, Arr, trans, isNewEntry, obj.Document_Date, obj.Sub_Location_code)
            isSaved = isSaved AndAlso clsDairyBookingCustomerDetailbyUploaderPaymentMode.saveData(obj.arrBookingDetailDairySalePaymentMode, obj.Document_No, obj.Document_Date, trans)
            If isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_TEMP_BOOKING_MATSER", "Document_No", "TSPL_TEMP_BOOKING_DETAIL", "Document_No", "TSPL_TEMP_BOOKING_PAYMENT_MODE_DETAIL", "Document_No", trans)

            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            qry = Nothing
            obj = Nothing
        End Try
    End Function

End Class
Public Class clsDairyBookingCustomerDetailbyUploader
#Region "Variable"
    Public Price_IdStartDate As DateTime?
    Public PricePlanNo As String = String.Empty
    Public Item_Price_ID As Integer = 0
    Public Document_No As String = Nothing
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
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
    Public QtyinKg As Double = 0
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
    Public Transporter_Commission_Rate As Decimal = 0
    Public Transporter_Commission_Amt As Decimal = 0
    Public Security_Rate As Decimal = 0
    Public Security_Amt As Decimal = 0
    Public Batch_No As String = ""
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsDairyBookingCustomerDetailbyUploader), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean, ByVal Docdate As String, ByVal SubLocation As String) As Boolean
        Dim LineNo As Integer = 0
        Dim SchemeType As String = Nothing
        Dim Scheme_Item_Code As String = Nothing
        Dim Scheme_Qty As Double = 0
        Dim Scheme_Item_UOM As String = Nothing
        Dim arrRepeat As New List(Of String)
        Dim qry As String = ""
        Dim dtBooking As DataTable = Nothing
        Dim dtGatePass As DataTable = Nothing
        Dim ArrGP As New List(Of clsDairyBookingCustomerDetailbyUploader)
        Dim dtBookingScheme As DataTable = Nothing
        Dim dtGatePassScheme As DataTable = Nothing
        Dim ArrGPScheme As New List(Of clsDairyBookingCustomerDetailbyUploader)
        Dim IsDairyModule As Boolean = False
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsDairyBookingCustomerDetailbyUploader In Arr
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
                clsCommon.AddColumnsForChange(coll, "QtyinKg", obj.QtyinKg)
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
                clsCommon.AddColumnsForChange(coll, "Transporter_Commission_Rate", obj.Transporter_Commission_Rate, True)
                clsCommon.AddColumnsForChange(coll, "Transporter_Commission_Amt", obj.Transporter_Commission_Amt, True)
                clsCommon.AddColumnsForChange(coll, "Security_Rate", obj.Security_Rate, True)
                clsCommon.AddColumnsForChange(coll, "Security_Amt", obj.Security_Amt, True)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No, True)
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
                clsCommon.AddColumnsForChange(coll, "DO_Posted", 0)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item_Code", "")
                clsCommon.AddColumnsForChange(coll, "Posted", 0)

                If clsCommon.myLen(clsCommon.myCstr(obj.SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(obj.SchemeType), "Quantitive") = CompairStringResult.Equal Then

                End If


                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TEMP_BOOKING_DETAIL", OMInsertOrUpdate.Insert, "", trans)


            Next



        End If
        Return True
        Arr = Nothing
    End Function

End Class
Public Class clsDairyBookingCustomerDetailbyUploaderPaymentMode
    Public TR_CODE As String = String.Empty
    Public SNo As Integer = 0
    Public Document_No As String = String.Empty
    Public Payment_Mode As String = String.Empty
    Public Amount As Double = 0
    Public Against_Receipt_No As String = String.Empty

    Public Shared Function saveData(ByVal arrObj As List(Of clsDairyBookingCustomerDetailbyUploaderPaymentMode), ByVal strQCNo As String, ByVal strDocDate As Date, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                For Each obj As clsDairyBookingCustomerDetailbyUploaderPaymentMode In arrObj
                    coll = New Hashtable()
                    obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, strDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                    clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                    clsCommon.AddColumnsForChange(coll, "Document_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Payment_Mode", obj.Payment_Mode)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "Against_Receipt_No", obj.Against_Receipt_No, True)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TEMP_BOOKING_PAYMENT_MODE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function

    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction) As List(Of clsDairyBookingCustomerDetailbyUploaderPaymentMode)
        Try
            Dim arrObj As List(Of clsDairyBookingCustomerDetailbyUploaderPaymentMode) = Nothing
            Dim obj As clsDairyBookingCustomerDetailbyUploaderPaymentMode = Nothing
            Dim qry As String = "Select * from TSPL_TEMP_BOOKING_PAYMENT_MODE_DETAIL where Document_No='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsDairyBookingCustomerDetailbyUploaderPaymentMode)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsDairyBookingCustomerDetailbyUploaderPaymentMode()
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