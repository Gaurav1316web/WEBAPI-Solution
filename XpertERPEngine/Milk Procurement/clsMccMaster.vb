Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class clsMccMaster
    Public SILOIn_Location As String = Nothing
    Public AutoIn_Location As String = Nothing
    Public AllowAutoMilkIn As Integer = 0
    Public IsDefault As Boolean = False
    Public MCC_Code As String = Nothing
    Public MCC_Code_VLC_Uploader = String.Empty
    Public MCC_Type As String = Nothing
    Public MCC_NAME As String = Nothing
    Public MCC_Name_Hindi As String = Nothing
    Public Short_Description As String = Nothing
    Public Chilling_Vendor As String = Nothing
    Public Add1 As String = Nothing
    Public Add2 As String = Nothing
    Public Tehsil As String = Nothing
    Public City_code As String = Nothing
    Public Unit_Code As String = Nothing
    Public Unit_Desc As String = Nothing
    Public Mcc_In_Charge As String = Nothing
    Public State_Code As String = Nothing
    Public Country_code As String = Nothing
    Public Pin_code As String = Nothing
    Public Pan_No As String = Nothing
    Public Telphone As String = Nothing
    Public Email As String = Nothing
    Public Fax As String = Nothing
    Public MCC_Area As Double = 0
    Public Area_Of_Store As Double = 0
    Public Area_Of_Office As Double = 0
    Public Open_Area_For_tanker As Double = 0
    Public Area_Of_LAB As Double = 0
    Public No_Of_SILO As Double = 0
    Public SILO_Capacity As Double = 0
    Public Total_Storage_capacity As Double = 0
    Public Area_Of_Receiving_DOCK As Double = 0
    Public No_Of_Chiller As Integer = 0
    Public Chiller_Brand_Name As String = Nothing
    Public Chiller_Capacity As Double = 0
    Public No_Of_MilkPump As Integer = 0
    Public MilkPump_Capacity As Double = 0
    Public DripSaver As String = Nothing
    Public CanWasher As String = Nothing
    Public CanScrubber As String = Nothing
    Public FSSAI_NO As String = Nothing
    Public ETP As String = Nothing
    Public Earthing As String = Nothing
    Public Coil_Length As Double = 0
    Public Electricity_Connection As String = Nothing
    Public Boiler As String = Nothing
    Public NoOfDG As Integer = 0
    Public NoOfCompressor As Integer = 0
    Public PayeeName As String = Nothing
    Public BankName As String = Nothing
    Public BankBranch As String = Nothing
    Public BankCityCode As String = Nothing
    Public BankStateCode As String = Nothing

    Public Start_Date As String = Nothing
    Public End_Date As String = Nothing
    Public Guarantee_Amount As Double
    Public Security_Amount As Double

    Public IFCICode As String = Nothing
    Public AccountNO As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As String = Nothing
    Public Comp_Code As String = Nothing
    Public isNewEntry As Boolean = False
    Public arrGenSetDetail As List(Of clsGenSetDetail) = Nothing
    Public arrMccEmployee As List(Of clsMccEmployee) = Nothing
    Public arrCompressorDetail As List(Of clsCompressorDetail) = Nothing
    Public arrPaymentDetail As List(Of clsPayment_Detail_MCC) = Nothing

    Public arrSiloDetail As List(Of clsSiloDetail) = Nothing
    Public arrMilkPumpDetail As List(Of clsMilkPumpDetail) = Nothing
    Public arrChillerDetail As List(Of clsChillerDetail) = Nothing
    Public ArrUomDetails As List(Of clsMccUOMDetails) = Nothing
    Public arrChequeDetail As List(Of clsMCCChequeDetails) = Nothing
    '-----------------------------------------------
    Public agreemnt As String = Nothing
    Public agrmnt_date As Date = Nothing
    Public expired_date As String = Nothing
    Public secutiy As String = Nothing
    Public chq_amt As Decimal = Nothing
    Public chq_no As Decimal = Nothing
    Public chq_date As Date = Nothing
    Public industry_type As String = Nothing
    Public industry_ppersn As String = ""
    Public chilling_rate As Decimal = Nothing
    Public is_Chilling_Provision_Monthly = False
    Public chilling_qty As Decimal = Nothing
    Public chilling_assur_qty As Decimal = Nothing
    Public chilling_assur_period As Decimal = Nothing
    Public chilling_kg_ltr As Decimal = Nothing
    Public lease_rate As Decimal = Nothing
    Public bankcode As String = Nothing
    Public FAT_SNF_SAVED_DECIMAL As Double = 0
    Public FAT_SNF_CALC_DECIMAL As Double = 0
    Public Payment_Cycle As String = String.Empty
    Public Incentive_Code As String = String.Empty
    Public Shift_Opening_Time As String = String.Empty
    Public Shift_Closing_Time As String = String.Empty
    Public Shift_Eve_Opening_Time As String = String.Empty
    Public Shift_Eve_Closing_Time As String = String.Empty
    '============Rohit add this to Store Unit of Area,Period,Qty=============
    Public Unit_MccSuperArea As String = Nothing
    Public Unit_AreaOfStore As String = Nothing
    Public Unit_AreaOfOffice As String = Nothing
    Public Unit_OpenAreaForTankerMovement As String = Nothing
    Public Unit_AreaOfLab As String = Nothing
    Public Unit_AreaOfReceivingDock As String = Nothing
    Public Unit_ChillingOn As String = Nothing
    Public Unit_ChillingOnQty As String = Nothing
    Public Unit_ChillingMinGuaranteePeriod As String = Nothing
    Public Unit_RateOfLeasedCharges As String = Nothing
    '====================================================
    '=======================Vendor Bank Details=====================
    Public Vendor_Bank_Code As String = Nothing
    Public Vendor_Bank_name As String = Nothing
    Public Vendor_Bank_City_Code As String = Nothing
    Public Vendor_Bank_City_Name As String = Nothing
    Public Vendor_Bank_State_Code As String = Nothing
    Public Vendor_Bank_State_Name As String = Nothing
    Public Vendor_Account_No As String = Nothing
    Public Vendor_IFSC_Code As String = Nothing
    Public Vendor_Branch_Name As String = Nothing
    Public Chilling_Period_Starting_Date As String = Nothing
    Public Days_For_FSSAI As String = "0"
    '======================================================
    '===========Add By Rohit on Dec 02,2014============
    Public Weighing_Machine As String = Nothing
    Public Sample_Machine As String = Nothing
    Public Default_Sample_Machine_2 As String = Nothing
    Public Default_Sample_Machine_3 As String = Nothing
    Public Default_Sample_Machine_4 As String = Nothing
    Public Weighing_Comport As String = Nothing
    Public Sample_comport As String = Nothing
    Public Default_Sample_Comport_2 As String = Nothing
    Public Default_Sample_Comport_3 As String = Nothing
    Public Default_Sample_Comport_4 As String = Nothing


    Public Is_Seprate_Dock_Cow_Buffalo As Boolean = False
    Public Weighing_Machine_Cow As String = Nothing
    Public Sample_Machine_Cow As String = Nothing
    Public Default_Sample_Machine_2_Cow As String = Nothing
    Public Weighing_Comport_Cow As String = Nothing
    Public Sample_comport_Cow As String = Nothing
    Public Default_Sample_Comport_2_Cow As String = Nothing
    Public AskSiloatShiftEnd As Boolean = False

    Public Flusing_Adj_Qty_Shift_End As Decimal
    Public MCC_in_Plant As Boolean


    Public Is_Truck_Sheet As String = Nothing
    Public Inactive As String = Nothing
    Public EmpOnAmountOnly As String = Nothing
    Public Is_MCC As Boolean = False
    '==================================================
    '========Add by rohit on Dec 26,2014 to save Comport for Selected Mcc======






    Public Deafault_MP_Grp_Code As String = String.Empty
    Public Deafault_MP_Payment_Cycle As String = String.Empty
    Public Deafault_MP_Terms_Code As String = String.Empty
    Public Deafault_MP_Payment_Code As String = String.Empty
    '===================================
    Public Arr_Payment_Entry As ArrayList
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public EMP_CODE As String = Nothing
    Public EMP_Name As String = Nothing
    Public is_Reuired_Gate_Entry As Boolean = False   '---------------04/08/2016 ticket no BM00000009396 by balwinder


    Public Receipt_Weight_tolerance_Apply As Boolean
    Public Receipt_Weight_tolerance_Value As Decimal


    Public Shift_Default_Time_Morning As DateTime?
    Public Shift_Default_Time_Evening As DateTime?
    Public Collection_Method As Integer
    Public IsSuspense As Boolean
    Public Failed_Sample_Apply As Boolean
    Public Failed_Sample_FAT As Decimal
    Public Failed_Sample_SNF As Decimal
    Public Loc_Segment_Code As String = Nothing
    Public Plant_Code As String = Nothing
    Public Area_Location_Code As String = Nothing
    Public Commission_Rate As Decimal
    Public Commission_Minimum_Shift_In_Payment_Cycle As Integer
    Public Commission_Minimum_Qty_In_Shift As Integer
    Public Commission_No_Of_Payment_Cycle_For_New_VSP As Integer

    Public Deduction_Rate As Decimal
    Public Deduction_Minimum_FAT_Per As Decimal
    Public Deduction_Minimum_SNF_Per As Decimal
    Public Deduction_No_Of_Payment_Cycle_For_New_VSP As Integer


    Public Day_Wise_Incentive_From_1 As Decimal
    Public Day_Wise_Incentive_From_2 As Decimal
    Public Day_Wise_Incentive_From_3 As Decimal
    Public Day_Wise_Incentive_From_4 As Decimal
    Public Day_Wise_Incentive_From_5 As Decimal

    Public Day_Wise_Incentive_To_1 As Decimal
    Public Day_Wise_Incentive_To_2 As Decimal
    Public Day_Wise_Incentive_To_3 As Decimal
    Public Day_Wise_Incentive_To_4 As Decimal
    Public Day_Wise_Incentive_To_5 As Decimal

    Public Day_Wise_Incentive_Rate_1 As Decimal
    Public Day_Wise_Incentive_Rate_2 As Decimal
    Public Day_Wise_Incentive_Rate_3 As Decimal
    Public Day_Wise_Incentive_Rate_4 As Decimal
    Public Day_Wise_Incentive_Rate_5 As Decimal

    Public Company_VSP_Deduction As Decimal
    Public Non_Company_VSP_Deduction As Decimal

    Public Loc_Segment_Description As String = String.Empty
    Public Tub_Capacity As Integer = 0

    Public Shared Function ToEnglishInput()
        Try
            Dim CName As String = ""

            For Each lang As InputLanguage In InputLanguage.InstalledInputLanguages
                CName = lang.Culture.EnglishName.ToString()

                If CName.StartsWith("English") Then
                    InputLanguage.CurrentInputLanguage = lang
                End If
            Next
        Catch ex As Exception
        End Try
        Return True
    End Function

    Public Shared Function ToHindiInput()
        Try
            Dim CName As String = ""

            For Each lang As InputLanguage In InputLanguage.InstalledInputLanguages
                CName = lang.Culture.EnglishName.ToString()

                If CName.StartsWith("Hindi") Then
                    InputLanguage.CurrentInputLanguage = lang
                End If
            Next
        Catch ex As Exception
        End Try
        Return True
    End Function

    Public Shared Function GetPaymentCycleToDate(ByVal strMCC As String, ByVal txtFromDate As Date) As Date
        Dim txtToDate As Date
        Try
            Dim PaymentCycleType As String = ""

            Dim PaymentCycleValue As Integer = 0
            If clsCommon.myLen(strMCC) <= 0 Then
                Throw New Exception("Please select the MCC first")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select Payment_Cycle,PC_TYPE,PC_VALUE from ( select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in ('" + strMCC + "') ) xx group by Payment_Cycle,PC_TYPE,PC_VALUE")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Payment Cycle found on current MCC/Location")
            End If
            If dt.Rows.Count > 1 Then
                Throw New Exception("Selected MCC's Payment Cycle Should be Same")
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If txtFromDate.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    txtFromDate = New Date(dtCurr.Year, dtCurr.Month, 1)
                    txtToDate = txtFromDate
                    Throw New Exception("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                End If
                txtToDate = txtFromDate.AddDays(PaymentCycleValue - 1)

                If txtFromDate.Month <> txtToDate.Month Then
                    txtToDate = New Date(txtFromDate.Year, txtFromDate.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = txtToDate.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If txtFromDate.Month <> dtNxtPay.Month Then
                    txtToDate = New Date(txtFromDate.Year, txtFromDate.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate, "dd")) <> 1 Then

                    txtFromDate = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Throw New Exception("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                End If
                txtToDate = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate, "dd")) <> 1 Then
                    txtFromDate = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Throw New Exception("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                End If
                txtToDate = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As Date = txtFromDate
                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                txtFromDate = today.AddDays(-dayDiff)
                txtToDate = txtFromDate.AddDays(6)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try
        Return txtToDate
    End Function
    Public Shared Function isMccChillingBasis(ByVal MccCode As String) As Boolean
        Dim rValue As Boolean = False
        Dim strType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Type  from TSPL_MCC_MASTER where MCC_Code='" & MccCode & "'"))
        If clsCommon.CompairString("Chilling Basis", strType) = CompairStringResult.Equal Then
            rValue = True
        Else
            rValue = False
        End If
        Return rValue
    End Function
    Public Shared Function isMccOnRent(ByVal MccCode As String) As Boolean
        Dim rValue As Boolean = False
        Dim strType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Type  from TSPL_MCC_MASTER where MCC_Code='" & MccCode & "'"))
        If clsCommon.CompairString("Co. Leased", strType) = CompairStringResult.Equal Then
            rValue = True
        Else
            rValue = False
        End If
        Return rValue
    End Function

    Public Shared Function GetName(ByVal mccCode As String, ByVal trans As SqlTransaction) As String
        Dim mccName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from TSPL_MCC_MASTER where MCC_Code='" & mccCode & "'", trans))
        Return mccName
    End Function
    Public Shared Function getMccNameOnMccCodeForVLCUplader(ByVal mccCode As String, ByVal trans As SqlTransaction) As String
        Dim mccName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from TSPL_MCC_MASTER where mcc_code_vlc_uploader='" & mccCode & "'", trans))
        Return mccName
    End Function

    Public Shared Function getMccCodeForVLCUplader(ByVal mccCode As String, ByVal trans As SqlTransaction) As String
        Dim _mccCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_Code from TSPL_MCC_MASTER where mcc_code_vlc_uploader='" & mccCode & "'", trans))
        Return _mccCode
    End Function
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as [Mcc Name] ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.mcc_code_vlc_uploader as [MCC Code For VLC Uploder],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER.Location_Desc AS [Plant Name] From tspl_mcc_master LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=tspl_mcc_master.Plant_Code"

            str = clsCommon.ShowSelectForm("MCCMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str

    End Function
    Public Shared Function getFinderUploader(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select tspl_mcc_master.mcc_code_vlc_uploader as UploderCode,tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as MccName ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER.Location_Desc AS [Plant Name] From tspl_mcc_master Left outer join TSPL_BULK_ROUTE_MASTER_MCC on TSPL_BULK_ROUTE_MASTER_MCC.MCC_Code=TSPL_MCC_MASTER.MCC_Code LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=tspl_mcc_master.Plant_Code"
            str = clsCommon.ShowSelectForm("MCCUMST", qry, "UploderCode", whrcls, curcode, "UploderCode", isButtonClicked, "")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function



    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function deleteData(ByVal strcode As String, ByVal progcode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isdeleted As Boolean = True
            isdeleted = isdeleted And clsGenSetDetail.deleteData(progcode, strcode, trans)
            isdeleted = isdeleted And clsCompressorDetail.deleteData(progcode, strcode, trans)
            Dim qry As String = "delete from tspl_mcc_master where  mcc_code='" & strcode & "'"
            isdeleted = isdeleted And clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from tspl_location_master where  location_code='" & strcode & "'"
            isdeleted = isdeleted And clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
            Return isdeleted
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try

    End Function
    Public Shared Function isCurrentUserHO() As Boolean
        Return isCurrentUserHO(Nothing)
    End Function
    Public Shared Function isCurrentUserHO(ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select tspl_location_master.location_category from tspl_user_master left outer join tspl_location_master on tspl_location_master.location_code=tspl_user_master.default_location where tspl_user_master.user_code='" + objCommonVar.CurrentUserCode + "'"
        Dim value As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        If clsCommon.CompairString(value, "MCC") = CompairStringResult.Equal OrElse clsCommon.myLen(value) <= 0 Then
            Return False
        ElseIf clsCommon.CompairString(value, "HO") = CompairStringResult.Equal Then
            Return True
        Else
            Return False

        End If
    End Function
    Public Shared Function loadData(ByVal strcode As String, ByVal navtype As NavigatorType, ByVal isShowAllMCC As Boolean, ByVal progcode As String, ByVal arrLoc As String) As clsMccMaster
        Dim obj As New clsMccMaster
        Try
            obj.arrGenSetDetail = New List(Of clsGenSetDetail)
            obj.arrSiloDetail = New List(Of clsSiloDetail)
            obj.arrMilkPumpDetail = New List(Of clsMilkPumpDetail)
            obj.arrChillerDetail = New List(Of clsChillerDetail)
            obj.arrChequeDetail = New List(Of clsMCCChequeDetails)
            obj.arrCompressorDetail = New List(Of clsCompressorDetail)
            obj.ArrUomDetails = New List(Of clsMccUOMDetails)
            obj.arrMccEmployee = New List(Of clsMccEmployee)
            Dim qst As String = " select * From tspl_mcc_master   where 1=1"
            Dim whrcls As String = ""
            If clsCommon.myLen(arrLoc) > 0 Then
                whrcls = " and mcc_code in (" + arrLoc + ")"
            End If
            qst = qst + " " + whrcls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_mcc_master.MCC_Code in ('" + strcode + "')"
                Case NavigatorType.Next
                    qst += " and tspl_mcc_master.MCC_Code in (select min(MCC_Code ) from tspl_mcc_master where MCC_Code  >'" + strcode + "' " + whrcls + ")"
                Case NavigatorType.First
                    qst += " and tspl_mcc_master.MCC_Code in (select MIN(MCC_Code ) from tspl_mcc_master where 2=2 " + whrcls + ")"
                Case NavigatorType.Last
                    qst += " and tspl_mcc_master.MCC_Code in (select Max(MCC_Code ) from tspl_mcc_master where 2=2 " + whrcls + ")"
                Case NavigatorType.Previous
                    qst += " and tspl_mcc_master.MCC_Code in (select Max(MCC_Code ) from tspl_mcc_master where MCC_Code  <'" + strcode + "' " + whrcls + ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Area_Location_Code = clsCommon.myCstr(dt.Rows(0)("Area_Location_Code"))
                obj.Plant_Code = clsCommon.myCstr(dt.Rows(0)("Plant_Code"))
                obj.AllowAutoMilkIn = clsCommon.myCdbl(dt.Rows(0)("AllowAutoMilkIn"))
                obj.IsDefault = (clsCommon.myCdbl(dt.Rows(0)("IsDefault")) = 1)
                obj.AutoIn_Location = clsCommon.myCstr(dt.Rows(0)("AutoIn_Location"))
                obj.SILOIn_Location = clsCommon.myCstr(dt.Rows(0)("SILOIn_Location"))
                obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
                obj.MCC_Type = clsCommon.myCstr(dt.Rows(0)("MCC_Type"))
                obj.MCC_NAME = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
                obj.MCC_Name_Hindi = clsCommon.myCstr(dt.Rows(0)("MCC_NAME_Hindi"))
                obj.Short_Description = clsCommon.myCstr(dt.Rows(0)("Short_Description"))
                obj.Chilling_Vendor = clsCommon.myCstr(dt.Rows(0)("Chilling_Vendor"))
                obj.Add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
                obj.Add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
                obj.Tehsil = clsCommon.myCstr(dt.Rows(0)("Tehsil"))
                obj.City_code = clsCommon.myCstr(dt.Rows(0)("City_code"))
                obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                obj.Unit_Desc = clsCommon.myCstr(dt.Rows(0)("Unit_Desc"))
                obj.Mcc_In_Charge = clsCommon.myCstr(dt.Rows(0)("MCC_In_Charge"))
                obj.State_Code = clsCommon.myCstr(dt.Rows(0)("State_Code"))
                obj.Country_code = clsCommon.myCstr(dt.Rows(0)("Country_code"))
                obj.Pin_code = clsCommon.myCstr(dt.Rows(0)("Pin_code"))
                obj.Pan_No = clsCommon.myCstr(dt.Rows(0)("Pan_No"))
                obj.Telphone = clsCommon.myCstr(dt.Rows(0)("Telphone"))
                obj.Email = clsCommon.myCstr(dt.Rows(0)("Email"))
                obj.Fax = clsCommon.myCstr(dt.Rows(0)("Fax"))
                obj.MCC_Area = clsCommon.myCdbl(dt.Rows(0)("MCC_Area"))
                obj.Area_Of_Store = clsCommon.myCdbl(dt.Rows(0)("Area_Of_Store"))
                obj.Area_Of_Office = clsCommon.myCdbl(dt.Rows(0)("Area_Of_Office"))
                obj.Area_Of_LAB = clsCommon.myCdbl(dt.Rows(0)("Area_Of_LAB"))
                obj.Open_Area_For_tanker = clsCommon.myCdbl(dt.Rows(0)("Open_Area_For_tanker"))
                obj.No_Of_SILO = clsCommon.myCdbl(dt.Rows(0)("No_Of_SILO"))
                obj.SILO_Capacity = clsCommon.myCdbl(dt.Rows(0)("SILO_Capacity"))
                obj.Total_Storage_capacity = clsCommon.myCdbl(dt.Rows(0)("Total_Storage_capacity"))
                obj.Area_Of_Receiving_DOCK = clsCommon.myCdbl(dt.Rows(0)("Area_Of_Receiving_DOCK"))
                obj.No_Of_Chiller = clsCommon.myCdbl(dt.Rows(0)("No_Of_Chiller"))
                obj.Chiller_Brand_Name = clsCommon.myCstr(dt.Rows(0)("Chiller_Brand_Name"))
                obj.Chiller_Capacity = clsCommon.myCdbl(dt.Rows(0)("Chiller_Capacity"))
                obj.No_Of_MilkPump = clsCommon.myCdbl(dt.Rows(0)("No_Of_MilkPump"))
                obj.MilkPump_Capacity = clsCommon.myCdbl(dt.Rows(0)("MilkPump_Capacity"))
                obj.DripSaver = clsCommon.myCstr(dt.Rows(0)("DripSaver"))
                obj.CanWasher = clsCommon.myCstr(dt.Rows(0)("CanWasher"))
                obj.CanScrubber = clsCommon.myCstr(dt.Rows(0)("CanScrubber"))
                obj.FSSAI_NO = clsCommon.myCstr(dt.Rows(0)("FSSAI_NO"))
                obj.ETP = clsCommon.myCstr(dt.Rows(0)("ETP"))
                obj.Earthing = clsCommon.myCstr(dt.Rows(0)("Earthing"))
                obj.Coil_Length = clsCommon.myCdbl(dt.Rows(0)("Coil_Length"))
                obj.Electricity_Connection = clsCommon.myCstr(dt.Rows(0)("Electricity_Connection"))
                obj.Boiler = clsCommon.myCstr(dt.Rows(0)("Boiler"))
                obj.NoOfDG = clsCommon.myCstr(dt.Rows(0)("NoOfDG"))
                obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
                If clsCommon.myLen(obj.EMP_CODE) > 0 Then
                    obj.EMP_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_EMPLOYEE_MASTER.emp_name from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" + obj.EMP_CODE + "'"))
                End If

                obj.Commission_Rate = clsCommon.myCdbl(dt.Rows(0)("Commission_Rate"))
                obj.Commission_Minimum_Shift_In_Payment_Cycle = clsCommon.myCdbl(dt.Rows(0)("Commission_Minimum_Shift_In_Payment_Cycle"))
                obj.Commission_Minimum_Qty_In_Shift = clsCommon.myCdbl(dt.Rows(0)("Commission_Minimum_Qty_In_Shift"))
                obj.Commission_No_Of_Payment_Cycle_For_New_VSP = clsCommon.myCdbl(dt.Rows(0)("Commission_No_Of_Payment_Cycle_For_New_VSP"))
                obj.Tub_Capacity = clsCommon.myCdbl(dt.Rows(0)("Tub_Capacity"))
                obj.Deduction_Rate = clsCommon.myCdbl(dt.Rows(0)("Deduction_Rate"))
                obj.Deduction_Minimum_FAT_Per = clsCommon.myCdbl(dt.Rows(0)("Deduction_Minimum_FAT_Per"))
                obj.Deduction_Minimum_SNF_Per = clsCommon.myCdbl(dt.Rows(0)("Deduction_Minimum_SNF_Per"))
                obj.Deduction_No_Of_Payment_Cycle_For_New_VSP = clsCommon.myCdbl(dt.Rows(0)("Deduction_No_Of_Payment_Cycle_For_New_VSP"))

                obj.Day_Wise_Incentive_From_1 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_From_1"))
                obj.Day_Wise_Incentive_From_2 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_From_2"))
                obj.Day_Wise_Incentive_From_3 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_From_3"))
                obj.Day_Wise_Incentive_From_4 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_From_4"))
                obj.Day_Wise_Incentive_From_5 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_From_5"))

                obj.Day_Wise_Incentive_To_1 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_To_1"))
                obj.Day_Wise_Incentive_To_2 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_To_2"))
                obj.Day_Wise_Incentive_To_3 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_To_3"))
                obj.Day_Wise_Incentive_To_4 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_To_4"))
                obj.Day_Wise_Incentive_To_5 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_To_5"))

                obj.Day_Wise_Incentive_Rate_1 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_Rate_1"))
                obj.Day_Wise_Incentive_Rate_2 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_Rate_2"))
                obj.Day_Wise_Incentive_Rate_3 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_Rate_3"))
                obj.Day_Wise_Incentive_Rate_4 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_Rate_4"))
                obj.Day_Wise_Incentive_Rate_5 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_Rate_5"))

                obj.Company_VSP_Deduction = clsCommon.myCdbl(dt.Rows(0)("Company_VSP_Deduction"))
                obj.Non_Company_VSP_Deduction = clsCommon.myCdbl(dt.Rows(0)("Non_Company_VSP_Deduction"))


                obj.arrGenSetDetail = clsGenSetDetail.LoadData(progcode, obj.MCC_Code)
                obj.arrMccEmployee = clsMccEmployee.LoadData(progcode, obj.MCC_Code)
                obj.arrSiloDetail = clsSiloDetail.LoadData(progcode, obj.MCC_Code)
                obj.arrMilkPumpDetail = clsMilkPumpDetail.LoadData(progcode, obj.MCC_Code)
                obj.arrChillerDetail = clsChillerDetail.LoadData(progcode, obj.MCC_Code)
                obj.arrChequeDetail = clsMCCChequeDetails.LoadData(obj.MCC_Code)
                obj.arrCompressorDetail = clsCompressorDetail.LoadData(progcode, obj.MCC_Code)
                obj.ArrUomDetails = clsMccUOMDetails.LoadData(obj.MCC_Code)

                obj.is_Reuired_Gate_Entry = IIf(clsCommon.myCdbl(dt.Rows(0)("is_Reuired_Gate_Entry")) = 1, True, False)
                obj.NoOfCompressor = clsCommon.myCdbl(dt.Rows(0)("NoOfCompressor"))
                obj.PayeeName = clsCommon.myCstr(dt.Rows(0)("PayeeName"))
                obj.BankName = clsCommon.myCstr(dt.Rows(0)("BankName"))
                obj.BankBranch = clsCommon.myCstr(dt.Rows(0)("BankBranch"))
                obj.BankCityCode = clsCommon.myCstr(dt.Rows(0)("BankCityCode"))
                obj.BankStateCode = clsCommon.myCstr(dt.Rows(0)("BankStateCode"))

                obj.Start_Date = clsCommon.myCstr(dt.Rows(0)("Start_Date"))
                obj.End_Date = clsCommon.myCstr(dt.Rows(0)("End_Date"))
                obj.Guarantee_Amount = clsCommon.myCdbl(dt.Rows(0)("Guarantee_Amount"))
                obj.Security_Amount = clsCommon.myCdbl(dt.Rows(0)("Standard_Security_Amount"))

                obj.IFCICode = clsCommon.myCstr(dt.Rows(0)("IFCICode"))
                obj.AccountNO = clsCommon.myCstr(dt.Rows(0)("AccountNO"))
                obj.MCC_Code_VLC_Uploader = clsCommon.myCstr(dt.Rows(0)("MCC_Code_VLC_Uploader"))
                obj.FAT_SNF_SAVED_DECIMAL = clsCommon.myCdbl(dt.Rows(0)("FAT_SNF_SAVE"))
                obj.FAT_SNF_CALC_DECIMAL = clsCommon.myCdbl(dt.Rows(0)("FAT_SNF_CALC"))
                obj.Payment_Cycle = clsCommon.myCstr(dt.Rows(0)("Payment_Cycle"))
                obj.Shift_Opening_Time = clsCommon.myCstr(dt.Rows(0)("Shift_Opening_Time"))
                obj.Shift_Closing_Time = clsCommon.myCstr(dt.Rows(0)("Shift_Closing_Time"))
                obj.Shift_Eve_Opening_Time = clsCommon.myCstr(dt.Rows(0)("Shift_Eve_Opening_Time"))
                obj.Shift_Eve_Closing_Time = clsCommon.myCstr(dt.Rows(0)("Shift_Eve_Closing_Time"))
                obj.Incentive_Code = clsCommon.myCstr(dt.Rows(0)("Incentive_Code"))

                '--------------25/06/2014 Monika
                obj.bankcode = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
                obj.agreemnt = clsCommon.myCstr(dt.Rows(0)("Agreement_Status"))

                If dt.Rows(0)("Agreement_Date") Is DBNull.Value Then
                    obj.agrmnt_date = clsCommon.GETSERVERDATE()
                Else
                    obj.agrmnt_date = clsCommon.myCDate(dt.Rows(0)("Agreement_Date"))
                End If

                If dt.Rows(0)("Agrmnt_Expired_Date") Is DBNull.Value Then
                    obj.expired_date = clsCommon.GETSERVERDATE()
                Else
                    obj.expired_date = clsCommon.myCDate(dt.Rows(0)("Agrmnt_Expired_Date"))
                End If

                obj.secutiy = clsCommon.myCstr(dt.Rows(0)("Security_Status"))
                obj.chq_amt = clsCommon.myCdbl(dt.Rows(0)("Cheque_Amt"))
                obj.chq_no = clsCommon.myCdbl(dt.Rows(0)("Cheque_No"))

                If dt.Rows(0)("Cheque_Date") Is DBNull.Value Then
                    obj.chq_date = clsCommon.GETSERVERDATE()
                Else
                    obj.chq_date = clsCommon.myCDate(dt.Rows(0)("Cheque_Date"))
                End If

                obj.industry_ppersn = clsCommon.myCstr(dt.Rows(0)("Industry_Person"))
                obj.industry_type = clsCommon.myCstr(dt.Rows(0)("Industry_Type"))
                obj.chilling_assur_period = clsCommon.myCdbl(dt.Rows(0)("Chilling_Assure_Period"))
                obj.chilling_assur_qty = clsCommon.myCdbl(dt.Rows(0)("Chilling_Assure_Qty"))
                obj.chilling_kg_ltr = clsCommon.myCdbl(dt.Rows(0)("Chilling_KG_Ltr"))
                obj.chilling_qty = clsCommon.myCdbl(dt.Rows(0)("Chilling_Dispatch_Qty"))
                obj.chilling_rate = clsCommon.myCdbl(dt.Rows(0)("Chilling_Rate"))
                obj.is_Chilling_Provision_Monthly = IIf(clsCommon.myCdbl(dt.Rows(0)("is_Chilling_Provision_Monthly")) = 1, True, False)
                obj.lease_rate = clsCommon.myCdbl(dt.Rows(0)("Lease_Rate"))
                If clsCommon.myLen(dt.Rows(0)("Chilling_Period_Starting_Date")) > 0 Then
                    obj.Chilling_Period_Starting_Date = clsCommon.myCDate(dt.Rows(0)("Chilling_Period_Starting_Date"))
                Else
                    obj.Chilling_Period_Starting_Date = clsCommon.GETSERVERDATE()
                End If

                '=====================Rohit==============================
                obj.Unit_AreaOfLab = clsCommon.myCstr(dt.Rows(0)("Unit_AreaOfLab"))
                obj.Unit_AreaOfOffice = clsCommon.myCstr(dt.Rows(0)("Unit_AreaOfOffice"))
                obj.Unit_AreaOfReceivingDock = clsCommon.myCstr(dt.Rows(0)("Unit_AreaOfReceivingDock"))
                obj.Unit_AreaOfStore = clsCommon.myCstr(dt.Rows(0)("Unit_AreaOfStore"))
                obj.Unit_ChillingMinGuaranteePeriod = clsCommon.myCstr(dt.Rows(0)("Unit_ChillingMinGuaranteePeriod"))
                obj.Unit_RateOfLeasedCharges = clsCommon.myCstr(dt.Rows(0)("Unit_RateOfLeasedCharges"))
                obj.Unit_ChillingOn = clsCommon.myCstr(dt.Rows(0)("Unit_ChillingOn"))
                obj.Unit_ChillingOnQty = clsCommon.myCstr(dt.Rows(0)("Unit_ChillingOnQty"))
                obj.Unit_MccSuperArea = clsCommon.myCstr(dt.Rows(0)("Unit_MccSuperArea"))
                obj.Unit_OpenAreaForTankerMovement = clsCommon.myCstr(dt.Rows(0)("Unit_OpenAreaForTankerMovement"))

                obj.Weighing_Machine = clsCommon.myCstr(dt.Rows(0)("Default_weighing_machine"))
                obj.Sample_Machine = clsCommon.myCstr(dt.Rows(0)("Default_Sample_machine"))
                obj.Default_Sample_Machine_2 = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Machine_2"))
                obj.Default_Sample_Machine_3 = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Machine_3"))
                obj.Default_Sample_Machine_4 = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Machine_4"))
                obj.Weighing_Comport = clsCommon.myCstr(dt.Rows(0)("Default_Weighing_comport"))
                obj.Sample_comport = clsCommon.myCstr(dt.Rows(0)("Default_Sample_comport"))
                obj.Default_Sample_Comport_2 = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Comport_2"))
                obj.Default_Sample_Comport_3 = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Comport_3"))
                obj.Default_Sample_Comport_4 = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Comport_4"))


                obj.Is_Seprate_Dock_Cow_Buffalo = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Seprate_Dock_Cow_Buffalo")) = 1, True, False)
                obj.AskSiloatShiftEnd = IIf(clsCommon.myCdbl(dt.Rows(0)("AskSiloatShiftEnd")) = 1, True, False)
                obj.Weighing_Machine_Cow = clsCommon.myCstr(dt.Rows(0)("Weighing_Machine_Cow"))
                obj.Sample_Machine_Cow = clsCommon.myCstr(dt.Rows(0)("Sample_Machine_Cow"))
                obj.Default_Sample_Machine_2_Cow = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Machine_2_Cow"))
                obj.Weighing_Comport_Cow = clsCommon.myCstr(dt.Rows(0)("Weighing_Comport_Cow"))
                obj.Sample_comport_Cow = clsCommon.myCstr(dt.Rows(0)("Sample_comport_Cow"))
                obj.Default_Sample_Comport_2_Cow = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Comport_2_Cow"))

                obj.Flusing_Adj_Qty_Shift_End = clsCommon.myCdbl(dt.Rows(0)("Flusing_Adj_Qty_Shift_End"))
                obj.MCC_in_Plant = IIf(clsCommon.myCdbl(dt.Rows(0)("MCC_in_Plant")) = 1, True, False)

                obj.Is_Truck_Sheet = clsCommon.myCstr(dt.Rows(0)("Is_truck_Sheet_Mandatory"))
                obj.Inactive = clsCommon.myCstr(dt.Rows(0)("In_active"))
                obj.EmpOnAmountOnly = clsCommon.myCstr(dt.Rows(0)("EmpOnAmountOnly"))
                obj.Is_MCC = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_MCC")) = 1, True, False)

                obj.Deafault_MP_Grp_Code = clsCommon.myCstr(dt.Rows(0)("Deafault_MP_Grp_Code"))
                obj.Deafault_MP_Payment_Code = clsCommon.myCstr(dt.Rows(0)("Deafault_MP_Payment_Code"))
                obj.Deafault_MP_Payment_Cycle = clsCommon.myCstr(dt.Rows(0)("Deafault_MP_Payment_Cycle"))
                obj.Deafault_MP_Terms_Code = clsCommon.myCstr(dt.Rows(0)("Deafault_MP_Terms_Code"))

                obj.Collection_Method = clsCommon.myCdbl(dt.Rows(0)("Collection_Method"))

                obj.Receipt_Weight_tolerance_Apply = IIf(clsCommon.myCdbl(dt.Rows(0)("Receipt_Weight_tolerance_Apply")) = 1, True, False)
                obj.Receipt_Weight_tolerance_Value = clsCommon.myCdbl(dt.Rows(0)("Receipt_Weight_tolerance_Value"))

                If dt.Rows(0)("Shift_Default_Time_Morning") IsNot DBNull.Value Then
                    obj.Shift_Default_Time_Morning = clsCommon.myCDate(dt.Rows(0)("Shift_Default_Time_Morning"))
                End If
                If dt.Rows(0)("Shift_Default_Time_Evening") IsNot DBNull.Value Then
                    obj.Shift_Default_Time_Evening = clsCommon.myCDate(dt.Rows(0)("Shift_Default_Time_Evening"))
                End If
                obj.Failed_Sample_Apply = (clsCommon.myCdbl(dt.Rows(0)("Failed_Sample_Apply")) = 1)
                obj.IsSuspense = (clsCommon.myCdbl(dt.Rows(0)("IsSuspense")) = 1)
                obj.Failed_Sample_FAT = clsCommon.myCdbl(dt.Rows(0)("Failed_Sample_FAT"))
                obj.Failed_Sample_SNF = clsCommon.myCdbl(dt.Rows(0)("Failed_Sample_SNF"))
                '====================================================
                obj.arrPaymentDetail = clsPayment_Detail_MCC.GetPaymentData(obj.Chilling_Vendor)
                '-Code Ended-------------------------------
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function SetPayment_and_Incentive_On_VSP(ByVal Mcc_code As String, ByVal Pc_Code As String, ByVal Incentive_code As String, ByVal trans As SqlTransaction) As Boolean
        Dim sQuery As String = String.Empty
        If clsCommon.myLen(Pc_Code) > 0 Then
            sQuery = "update tspl_vendor_Master set pc_code='" & Pc_Code & "' where vendor_code in(select Distinct vendor_code from tspl_vlc_master_Head inner join " _
            & " tspl_vendor_Master on vendor_code=vsp_code inner join tspl_mcc_master on tspl_mcc_master.mcc_code=tspl_vlc_Master_Head.mcc where " _
            & " tspl_vlc_Master_Head.mcc='" & Mcc_code & "')"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        End If
        If clsCommon.myLen(Incentive_code) > 0 Then
            sQuery = "update tspl_vendor_Master set incentive='" & Incentive_code & "' where vendor_code in(select Distinct vendor_code from tspl_vlc_master_Head " _
            & " inner join tspl_vendor_Master on vendor_code=vsp_code inner join tspl_mcc_master on tspl_mcc_master.mcc_code=tspl_vlc_Master_Head.mcc " _
            & " where tspl_vlc_Master_Head.mcc='" & Mcc_code & "')"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        End If
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsMccMaster) As Boolean
        Dim issaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            If obj.IsSuspense = True Then
                issaved = issaved And clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_MCC_MASTER SET IsSuspense=0 where IsSuspense=1", trans)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Area_Location_Code", obj.Area_Location_Code)
            clsCommon.AddColumnsForChange(coll, "Plant_Code", obj.Plant_Code, True)
            clsCommon.AddColumnsForChange(coll, "AutoIn_Location", obj.AutoIn_Location)
            clsCommon.AddColumnsForChange(coll, "SILOIn_Location", obj.SILOIn_Location)
            clsCommon.AddColumnsForChange(coll, "AllowAutoMilkIn", obj.AllowAutoMilkIn)
            clsCommon.AddColumnsForChange(coll, "IsDefault", IIf(obj.IsDefault, 1, 0))
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "MCC_Type", obj.MCC_Type)
            clsCommon.AddColumnsForChange(coll, "MCC_NAME", obj.MCC_NAME)
            clsCommon.AddColumnsForChange(coll, "MCC_NAME_Hindi", obj.MCC_Name_Hindi, True, True)
            clsCommon.AddColumnsForChange(coll, "Short_Description", obj.Short_Description)
            clsCommon.AddColumnsForChange(coll, "Chilling_Vendor", obj.Chilling_Vendor)
            clsCommon.AddColumnsForChange(coll, "Add1", obj.Add1)
            clsCommon.AddColumnsForChange(coll, "Add2", obj.Add2)
            clsCommon.AddColumnsForChange(coll, "Tehsil", obj.Tehsil)
            clsCommon.AddColumnsForChange(coll, "City_code", obj.City_code)
            clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
            clsCommon.AddColumnsForChange(coll, "Unit_Desc", obj.Unit_Desc)
            clsCommon.AddColumnsForChange(coll, "Mcc_In_Charge", obj.Mcc_In_Charge)
            clsCommon.AddColumnsForChange(coll, "State_Code", obj.State_Code)
            clsCommon.AddColumnsForChange(coll, "Pin_Code", obj.Pin_code)
            clsCommon.AddColumnsForChange(coll, "Pan_No", obj.Pan_No)
            clsCommon.AddColumnsForChange(coll, "Country_code", obj.Country_code)
            clsCommon.AddColumnsForChange(coll, "Telphone", obj.Telphone)
            clsCommon.AddColumnsForChange(coll, "Email", obj.Email)
            clsCommon.AddColumnsForChange(coll, "Fax", obj.Fax)
            clsCommon.AddColumnsForChange(coll, "Area_Of_Store", obj.Area_Of_Store)
            clsCommon.AddColumnsForChange(coll, "MCC_Area", obj.MCC_Area)
            clsCommon.AddColumnsForChange(coll, "Area_Of_Office", obj.Area_Of_Office)
            clsCommon.AddColumnsForChange(coll, "Open_Area_For_tanker", obj.Open_Area_For_tanker)
            clsCommon.AddColumnsForChange(coll, "Area_Of_LAB", obj.Area_Of_LAB)
            clsCommon.AddColumnsForChange(coll, "No_Of_SILO", obj.No_Of_SILO)
            clsCommon.AddColumnsForChange(coll, "SILO_Capacity", obj.SILO_Capacity)
            clsCommon.AddColumnsForChange(coll, "Total_Storage_capacity", obj.Total_Storage_capacity)
            clsCommon.AddColumnsForChange(coll, "Area_Of_Receiving_DOCK", obj.Area_Of_Receiving_DOCK)
            clsCommon.AddColumnsForChange(coll, "No_Of_Chiller", obj.No_Of_Chiller)
            clsCommon.AddColumnsForChange(coll, "Chiller_Brand_Name", obj.Chiller_Brand_Name)
            clsCommon.AddColumnsForChange(coll, "Chiller_Capacity", obj.Chiller_Capacity)
            clsCommon.AddColumnsForChange(coll, "No_Of_MilkPump", obj.No_Of_MilkPump)
            clsCommon.AddColumnsForChange(coll, "MilkPump_Capacity", obj.MilkPump_Capacity)
            clsCommon.AddColumnsForChange(coll, "DripSaver", obj.DripSaver)
            clsCommon.AddColumnsForChange(coll, "CanWasher", obj.CanWasher)
            clsCommon.AddColumnsForChange(coll, "CanScrubber", obj.CanScrubber)
            clsCommon.AddColumnsForChange(coll, "FSSAI_NO", obj.FSSAI_NO)
            clsCommon.AddColumnsForChange(coll, "ETP", obj.ETP)
            clsCommon.AddColumnsForChange(coll, "MCC_Code_VLC_Uploader", clsCommon.myCstr(obj.MCC_Code_VLC_Uploader))
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", clsCommon.myCstr(obj.EMP_CODE), True)

            clsCommon.AddColumnsForChange(coll, "Earthing", obj.Earthing)
            clsCommon.AddColumnsForChange(coll, "Coil_Length", obj.Coil_Length)
            clsCommon.AddColumnsForChange(coll, "Electricity_Connection", obj.Electricity_Connection)
            clsCommon.AddColumnsForChange(coll, "Boiler", obj.Boiler)
            clsCommon.AddColumnsForChange(coll, "NoOfDG", obj.NoOfDG)
            clsCommon.AddColumnsForChange(coll, "NoOfCompressor", obj.NoOfCompressor)
            clsCommon.AddColumnsForChange(coll, "PayeeName", obj.PayeeName)
            clsCommon.AddColumnsForChange(coll, "BankName", obj.BankName)
            clsCommon.AddColumnsForChange(coll, "BankBranch", obj.BankBranch)
            clsCommon.AddColumnsForChange(coll, "BankCityCode", obj.BankCityCode)
            clsCommon.AddColumnsForChange(coll, "BankStateCode", obj.BankStateCode)

            clsCommon.AddColumnsForChange(coll, "Start_Date", obj.Start_Date)
            clsCommon.AddColumnsForChange(coll, "END_date", obj.End_Date)
            clsCommon.AddColumnsForChange(coll, "Guarantee_Amount", obj.Guarantee_Amount)
            clsCommon.AddColumnsForChange(coll, "Standard_Security_Amount", obj.Security_Amount)

            clsCommon.AddColumnsForChange(coll, "IFCICode", obj.IFCICode)
            clsCommon.AddColumnsForChange(coll, "AccountNO", obj.AccountNO)
            clsCommon.AddColumnsForChange(coll, "FAT_SNF_SAVE", obj.FAT_SNF_SAVED_DECIMAL)
            clsCommon.AddColumnsForChange(coll, "FAT_SNF_CALC", obj.FAT_SNF_CALC_DECIMAL)
            clsCommon.AddColumnsForChange(coll, "Payment_Cycle", obj.Payment_Cycle)
            clsCommon.AddColumnsForChange(coll, "Shift_Opening_Time", obj.Shift_Opening_Time, True)
            clsCommon.AddColumnsForChange(coll, "Shift_Closing_Time", obj.Shift_Closing_Time, True)
            clsCommon.AddColumnsForChange(coll, "Shift_Eve_Opening_Time", obj.Shift_Eve_Opening_Time, True)
            clsCommon.AddColumnsForChange(coll, "Shift_Eve_Closing_Time", obj.Shift_Eve_Closing_Time, True)
            clsCommon.AddColumnsForChange(coll, "Incentive_Code", obj.Incentive_Code)
            clsCommon.AddColumnsForChange(coll, "Modified_By", obj.Modified_By)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", obj.Modified_Date)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.Comp_Code)

            clsCommon.AddColumnsForChange(coll, "is_Reuired_Gate_Entry", IIf(obj.is_Reuired_Gate_Entry, 1, 0))
            '---------------25/06/2014 Monika
            clsCommon.AddColumnsForChange(coll, "bank_code", obj.bankcode)
            clsCommon.AddColumnsForChange(coll, "Agreement_Status", obj.agreemnt)


            clsCommon.AddColumnsForChange(coll, "Agreement_Date", clsCommon.GetPrintDate(obj.agrmnt_date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Agrmnt_Expired_Date", clsCommon.GetPrintDate(obj.expired_date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Security_Status", obj.secutiy)
            clsCommon.AddColumnsForChange(coll, "Cheque_Amt", obj.chq_amt)
            clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.chq_no)
            clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(obj.chq_date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Industry_Type", obj.industry_type)
            clsCommon.AddColumnsForChange(coll, "Industry_Person", obj.industry_ppersn)
            clsCommon.AddColumnsForChange(coll, "Chilling_Rate", obj.chilling_rate)
            clsCommon.AddColumnsForChange(coll, "is_Chilling_Provision_Monthly", IIf(obj.is_Chilling_Provision_Monthly, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Chilling_Dispatch_Qty", obj.chilling_qty)
            clsCommon.AddColumnsForChange(coll, "Chilling_KG_Ltr", obj.chilling_kg_ltr)
            clsCommon.AddColumnsForChange(coll, "Chilling_Assure_Qty", obj.chilling_assur_qty)
            clsCommon.AddColumnsForChange(coll, "Chilling_Assure_Period", obj.chilling_assur_period)
            clsCommon.AddColumnsForChange(coll, "Chilling_Period_Starting_Date", obj.Chilling_Period_Starting_Date, True)
            clsCommon.AddColumnsForChange(coll, "Lease_Rate", obj.lease_rate)
            '-------code ended------------------------------
            '==================Rohit=================================
            clsCommon.AddColumnsForChange(coll, "Unit_AreaOfLab", obj.Unit_AreaOfLab)
            clsCommon.AddColumnsForChange(coll, "Unit_AreaOfOffice", obj.Unit_AreaOfOffice)
            clsCommon.AddColumnsForChange(coll, "Unit_AreaOfReceivingDock", obj.Unit_AreaOfReceivingDock)
            clsCommon.AddColumnsForChange(coll, "Unit_AreaOfStore", obj.Unit_AreaOfStore)
            clsCommon.AddColumnsForChange(coll, "Unit_ChillingMinGuaranteePeriod", obj.Unit_ChillingMinGuaranteePeriod)
            clsCommon.AddColumnsForChange(coll, "Unit_RateOfLeasedCharges", obj.Unit_RateOfLeasedCharges)
            clsCommon.AddColumnsForChange(coll, "Unit_ChillingOn", obj.Unit_ChillingOn)
            clsCommon.AddColumnsForChange(coll, "Unit_ChillingOnQty", obj.Unit_ChillingOnQty)
            clsCommon.AddColumnsForChange(coll, "Unit_MccSuperArea", obj.Unit_MccSuperArea)
            clsCommon.AddColumnsForChange(coll, "Unit_OpenAreaForTankerMovement", obj.Unit_OpenAreaForTankerMovement)


            clsCommon.AddColumnsForChange(coll, "Default_weighing_machine", obj.Weighing_Machine, True)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_machine", obj.Sample_Machine, True)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Machine_2", obj.Default_Sample_Machine_2)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Machine_3", obj.Default_Sample_Machine_3)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Machine_4", obj.Default_Sample_Machine_4)
            clsCommon.AddColumnsForChange(coll, "Default_weighing_Comport", obj.Weighing_Comport, True)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Comport", obj.Sample_comport, True)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Comport_2", obj.Default_Sample_Comport_2)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Comport_3", obj.Default_Sample_Comport_3)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Comport_4", obj.Default_Sample_Comport_4)


            clsCommon.AddColumnsForChange(coll, "Is_Seprate_Dock_Cow_Buffalo", IIf(obj.Is_Seprate_Dock_Cow_Buffalo, 1, 0))
            clsCommon.AddColumnsForChange(coll, "AskSiloatShiftEnd", IIf(obj.AskSiloatShiftEnd, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Weighing_Machine_Cow", obj.Weighing_Machine_Cow)
            clsCommon.AddColumnsForChange(coll, "Sample_Machine_Cow", obj.Sample_Machine_Cow)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Machine_2_Cow", obj.Default_Sample_Machine_2_Cow)
            clsCommon.AddColumnsForChange(coll, "Weighing_Comport_Cow", obj.Weighing_Comport_Cow)
            clsCommon.AddColumnsForChange(coll, "Sample_comport_Cow", obj.Sample_comport_Cow)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Comport_2_Cow", obj.Default_Sample_Comport_2_Cow)

            clsCommon.AddColumnsForChange(coll, "Flusing_Adj_Qty_Shift_End", obj.Flusing_Adj_Qty_Shift_End)
            clsCommon.AddColumnsForChange(coll, "MCC_in_Plant", IIf(obj.MCC_in_Plant, 1, 0))

            clsCommon.AddColumnsForChange(coll, "Is_Truck_Sheet_Mandatory", obj.Is_Truck_Sheet)
            clsCommon.AddColumnsForChange(coll, "In_active", obj.Inactive)
            clsCommon.AddColumnsForChange(coll, "EmpOnAmountOnly", obj.EmpOnAmountOnly)
            clsCommon.AddColumnsForChange(coll, "Deafault_MP_Terms_Code", obj.Deafault_MP_Terms_Code, True)
            clsCommon.AddColumnsForChange(coll, "Deafault_MP_Payment_Cycle", obj.Deafault_MP_Payment_Cycle, True)
            clsCommon.AddColumnsForChange(coll, "Deafault_MP_Payment_Code", obj.Deafault_MP_Payment_Code, True)
            clsCommon.AddColumnsForChange(coll, "Deafault_MP_Grp_Code", obj.Deafault_MP_Grp_Code, True)
            '========================================
            clsCommon.AddColumnsForChange(coll, "is_MCC", IIf(obj.Is_MCC, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Collection_Method", obj.Collection_Method)

            clsCommon.AddColumnsForChange(coll, "Receipt_Weight_tolerance_Apply", IIf(obj.Receipt_Weight_tolerance_Apply, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Receipt_Weight_tolerance_Value", IIf(obj.Receipt_Weight_tolerance_Apply, obj.Receipt_Weight_tolerance_Value, 0))
            If obj.Shift_Default_Time_Morning Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "Shift_Default_Time_Morning", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "Shift_Default_Time_Morning", clsCommon.GetPrintDate(obj.Shift_Default_Time_Morning, "dd/MMM/yyyy hh: mm tt"))
            End If
            If obj.Shift_Default_Time_Evening Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "Shift_Default_Time_Evening", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "Shift_Default_Time_Evening", clsCommon.GetPrintDate(obj.Shift_Default_Time_Evening, "dd/MMM/yyyy hh:mm tt"))
            End If
            clsCommon.AddColumnsForChange(coll, "IsSuspense", IIf(obj.IsSuspense, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Failed_Sample_Apply", IIf(obj.Failed_Sample_Apply, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Failed_Sample_FAT", IIf(obj.Failed_Sample_Apply, obj.Failed_Sample_FAT, 0))
            clsCommon.AddColumnsForChange(coll, "Failed_Sample_SNF", IIf(obj.Failed_Sample_Apply, obj.Failed_Sample_SNF, 0))


            clsCommon.AddColumnsForChange(coll, "Commission_Rate", obj.Commission_Rate)
            clsCommon.AddColumnsForChange(coll, "Commission_Minimum_Shift_In_Payment_Cycle", obj.Commission_Minimum_Shift_In_Payment_Cycle)
            clsCommon.AddColumnsForChange(coll, "Commission_Minimum_Qty_In_Shift", obj.Commission_Minimum_Qty_In_Shift)
            clsCommon.AddColumnsForChange(coll, "Commission_No_Of_Payment_Cycle_For_New_VSP", obj.Commission_No_Of_Payment_Cycle_For_New_VSP)

            clsCommon.AddColumnsForChange(coll, "Tub_Capacity", obj.Tub_Capacity, True)
            clsCommon.AddColumnsForChange(coll, "Deduction_Rate", obj.Deduction_Rate)
            clsCommon.AddColumnsForChange(coll, "Deduction_Minimum_FAT_Per", obj.Deduction_Minimum_FAT_Per)
            clsCommon.AddColumnsForChange(coll, "Deduction_Minimum_SNF_Per", obj.Deduction_Minimum_SNF_Per)
            clsCommon.AddColumnsForChange(coll, "Deduction_No_Of_Payment_Cycle_For_New_VSP", obj.Deduction_No_Of_Payment_Cycle_For_New_VSP)


            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_From_1", obj.Day_Wise_Incentive_From_1)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_From_2", obj.Day_Wise_Incentive_From_2)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_From_3", obj.Day_Wise_Incentive_From_3)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_From_4", obj.Day_Wise_Incentive_From_4)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_From_5", obj.Day_Wise_Incentive_From_5)

            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_To_1", obj.Day_Wise_Incentive_To_1)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_To_2", obj.Day_Wise_Incentive_To_2)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_To_3", obj.Day_Wise_Incentive_To_3)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_To_4", obj.Day_Wise_Incentive_To_4)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_To_5", obj.Day_Wise_Incentive_To_5)

            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_Rate_1", obj.Day_Wise_Incentive_Rate_1)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_Rate_2", obj.Day_Wise_Incentive_Rate_2)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_Rate_3", obj.Day_Wise_Incentive_Rate_3)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_Rate_4", obj.Day_Wise_Incentive_Rate_4)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_Rate_5", obj.Day_Wise_Incentive_Rate_5)

            clsCommon.AddColumnsForChange(coll, "Company_VSP_Deduction", obj.Company_VSP_Deduction)
            clsCommon.AddColumnsForChange(coll, "Non_Company_VSP_Deduction", obj.Non_Company_VSP_Deduction)

            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                clsCommon.AddColumnsForChange(coll, "Created_Date", obj.Created_Date)
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.MCC_Code, "TSPL_MCC_MASTER", "mcc_code", trans)
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_MASTER", OMInsertOrUpdate.Update, "tspl_mcc_master.mcc_code='" + obj.MCC_Code + "'", trans)
            End If
            issaved = issaved And clsGenSetDetail.SaveData(obj.arrGenSetDetail, trans)
            issaved = issaved And clsCompressorDetail.SaveData(obj.arrCompressorDetail, trans)
            issaved = issaved And clsSiloDetail.SaveData(obj.arrSiloDetail, trans)
            issaved = issaved And clsMilkPumpDetail.SaveData(obj.arrMilkPumpDetail, trans)
            issaved = issaved And clsChillerDetail.SaveData(obj.arrChillerDetail, trans)
            issaved = issaved And clsMCCChequeDetails.SaveData(obj.arrChequeDetail, trans)
            issaved = issaved AndAlso clsMccUOMDetails.SaveData(obj.MCC_Code, obj.ArrUomDetails, trans)
            issaved = issaved AndAlso clsMccMaster.SetPayment_and_Incentive_On_VSP(obj.MCC_Code, obj.Payment_Cycle, obj.Incentive_Code, trans)
            If obj.arrMccEmployee IsNot Nothing AndAlso obj.arrMccEmployee.Count > 0 Then
                issaved = issaved AndAlso clsMccEmployee.SaveData(obj.arrMccEmployee, trans)
            End If
            coll = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "Location_desc", obj.MCC_NAME)
            clsCommon.AddColumnsForChange(coll, "Add1", obj.Add1)
            clsCommon.AddColumnsForChange(coll, "Add2", obj.Add2)

            clsCommon.AddColumnsForChange(coll, "City_code", clsDBFuncationality.getSingleValue(" SELECT ISNULL(City_Name,'') from TSPL_CITY_MASTER where City_Code='" + obj.City_code + "'", trans))
            'clsCommon.AddColumnsForChange(coll, "City_code", obj.City_code )
            clsCommon.AddColumnsForChange(coll, "State", obj.State_Code)
            clsCommon.AddColumnsForChange(coll, "Pin_Code", obj.Pin_code)
            clsCommon.AddColumnsForChange(coll, "Country", obj.Country_code)
            clsCommon.AddColumnsForChange(coll, "Telphone", obj.Telphone)
            clsCommon.AddColumnsForChange(coll, "Email", obj.Email)
            clsCommon.AddColumnsForChange(coll, "Location_Type", "Physical")
            clsCommon.AddColumnsForChange(coll, "Location_Category", "MCC")
            clsCommon.AddColumnsForChange(coll, "Modify_By", obj.Modified_By)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(obj.Modified_Date, "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.Comp_Code)
            ' Ticket No : TEC/16/07/19-000945 By Prabhakar
            If clsCommon.myLen(clsCommon.myCstr(obj.Loc_Segment_Code)) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Loc_Segment_Code", obj.Loc_Segment_Code)
            End If

            Dim squery As String = "select Count(*) from tspl_location_master where tspl_location_master.location_code='" + obj.MCC_Code + "'"
            Dim chkloc As Integer = clsDBFuncationality.getSingleValue(squery, trans)
            If chkloc > 0 Then
                obj.isNewEntry = False
            Else
                obj.isNewEntry = True
            End If

            If obj.isNewEntry AndAlso clsCommon.myLen(obj.Loc_Segment_Code) > 0 AndAlso clsCommon.myLen(obj.Loc_Segment_Description) > 0 Then
                Dim Strsegmentname As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Seg_Name from TSPL_GL_SEGMENT where Seg_No='7'", trans))
                clsDBFuncationality.SaveAStorePorcedure(trans, "sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", "7"), New SqlParameter("@segmentname", Strsegmentname), New SqlParameter("@segmentcode", obj.Loc_Segment_Code), New SqlParameter("@desc", obj.Loc_Segment_Description), New SqlParameter("@acccode", ""), New SqlParameter("@createdby", obj.Created_By), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", obj.Created_By), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", obj.Comp_Code))
                connectSql.RunSpTransaction(trans, "SP_TSPL_GL_SEGMENT_PERMISSION_INSERT", New SqlParameter("@usercode", obj.Created_By), New SqlParameter("@glsegment", "7"), New SqlParameter("@segmentcode", obj.Loc_Segment_Code), New SqlParameter("@createdby", obj.Created_By), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", obj.Created_By), New SqlParameter("@compcode", obj.Comp_Code), New SqlParameter("@Default_Segment", "N"))
                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_GL_SEGMENT_CODE SET GIT='N',STATE_CODE='" + obj.State_Code + "' WHERE Seg_No='7' and Segment_name ='" + Strsegmentname + "' and Segment_code ='" & obj.Loc_Segment_Code & "'", trans)
            End If

            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Type", "Depot")
                clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(obj.Created_Date, "dd/MM/yyyy"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_location_master", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_Location_master", OMInsertOrUpdate.Update, "tspl_location_master.location_code='" + obj.MCC_Code + "'", trans)
            End If
            If clsCommon.myLen(obj.Chilling_Vendor) > 0 Then
                squery = "update tspl_vendor_Master set Pan='" & obj.Pan_No & "' where vendor_code='" & obj.Chilling_Vendor & "'"
                clsDBFuncationality.ExecuteNonQuery(squery, trans)
            End If
            issaved = issaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.MCC_Code, obj.arrCustomFields, trans)
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function GetBankDetails(ByVal StrVendorName As String) As clsMccMaster
        Dim obj As clsMccMaster = Nothing
        'Dim sQuery As String = "select vm.Bank_Code,bm.Bank_Name,bm.IFSC_Code,Account_No,bm.Branch_Code,bm.Branch_Name,bm.CITY_Code,City_Name,bm.State_Code," _
        '& " STATE_NAME,PAN from  TSPL_Vendor_MASTER  vm left join TSPL_Vendor_Bank_MASTER bm on bm.BANK_CODE=vm.Bank_Code left join TSPL_CITY_MASTER cm on " _
        '& " cm.City_Code=bm.city_COde left  join TSPL_State_MASTER sm on sm.STATE_CODE=bm.state_COde where vendor_code='" & StrVendorName & "'"

        Dim sQuery As String = "select vm.Bank_Code,bm.Bank_Name,vm.IFSC_Code,Account_No,vm.Branch_Code,vm.Branch_Name,bm.CITY_Code,City_Name,bm.State_Code," _
        & " STATE_NAME,PAN from  TSPL_Vendor_MASTER  vm left join TSPL_Vendor_Bank_MASTER bm on bm.BANK_CODE=vm.Bank_Code left join TSPL_CITY_MASTER cm on " _
        & " cm.City_Code=bm.city_COde left  join TSPL_State_MASTER sm on sm.STATE_CODE=bm.state_COde where vendor_code='" & StrVendorName & "'"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsMccMaster
            obj.Vendor_Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.Vendor_Bank_name = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
            obj.Vendor_Bank_City_Code = clsCommon.myCstr(dt.Rows(0)("City_Code"))
            obj.Vendor_Bank_City_Name = clsCommon.myCstr(dt.Rows(0)("City_Name"))
            obj.Vendor_Bank_State_Code = clsCommon.myCstr(dt.Rows(0)("State_Code"))
            obj.Vendor_Bank_State_Name = clsCommon.myCstr(dt.Rows(0)("State_name"))
            obj.Vendor_Branch_Name = clsCommon.myCstr(dt.Rows(0)("Branch_name"))
            obj.Vendor_Account_No = clsCommon.myCstr(dt.Rows(0)("Account_No"))
            obj.Vendor_IFSC_Code = clsCommon.myCstr(dt.Rows(0)("IFSC_CODE"))
            obj.Pan_No = clsCommon.myCstr(dt.Rows(0)("PAN"))
        End If
        Return obj
    End Function

    Public Shared Function DefaultWeighingMachine(ByVal mcc_code As String, ByVal trans As SqlTransaction) As String
        Return DefaultWeighingMachine(mcc_code, "M", trans)
    End Function

    Public Shared Function DefaultWeighingMachine(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction) As String
        Dim sQuery As String = "select "
        If clsCommon.CompairString(strDocCollectionMilkType, "C") = CompairStringResult.Equal Then
            sQuery += " coalesce(Weighing_Machine_Cow,'P') "
        Else
            sQuery += " coalesce(Default_weighing_machine,'P') "
        End If
        sQuery += " from tspl_mcc_master where mcc_code='" & mcc_code & "'"

        Dim WeighingMachine As String = clsDBFuncationality.getSingleValue(sQuery, trans)
        Return WeighingMachine
    End Function

    Public Shared Function DefaultWeighingComport(ByVal mcc_code As String, ByVal trans As SqlTransaction) As String
        Return DefaultWeighingComport(mcc_code, "M", trans)
    End Function

    Public Shared Function DefaultWeighingComport(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction) As String
        Dim sQuery As String = "select "
        If clsCommon.CompairString(strDocCollectionMilkType, "C") = CompairStringResult.Equal Then
            sQuery += " Weighing_Comport_Cow "
        Else
            sQuery += " Default_weighing_Comport "
        End If
        sQuery += "  from tspl_mcc_master where mcc_code='" & mcc_code & "'"

        Dim WeighingComport As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sQuery, trans))
        Return WeighingComport
    End Function



    Public Shared Function DefaultSampleMachine(ByVal mcc_code As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleMachine(mcc_code, "M", trans)
    End Function
    Public Shared Function DefaultSampleMachine(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleMachine(mcc_code, strDocCollectionMilkType, trans, "")
    End Function
    Public Shared Function DefaultSampleMachine(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction, ByVal strDockCode As String) As String
        Dim sQuery As String
        Dim sampleMachine As String
        If clsCommon.myLen(strDockCode) > 0 Then
            sampleMachine = clsDockMaster.DefaultSampleMachine(1, strDockCode, trans)
        Else
            sQuery = "select  "
            If clsCommon.CompairString(strDocCollectionMilkType, "C") = CompairStringResult.Equal Then
                sQuery += " coalesce(Sample_Machine_Cow,'E') "
            Else
                sQuery += " coalesce(Default_Sample_machine,'E') "
            End If
            sQuery += " from tspl_mcc_master where mcc_code='" & mcc_code & "'"
            sampleMachine = clsDBFuncationality.getSingleValue(sQuery, trans)
        End If


        Return sampleMachine
    End Function


    Public Shared Function DefaultSampleComport(ByVal mcc_code As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleComport(mcc_code, "M", trans)
    End Function
    Public Shared Function DefaultSampleComport(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleComport(mcc_code, strDocCollectionMilkType, trans, "")
    End Function
    Public Shared Function DefaultSampleComport(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction, ByVal strDockCode As String) As String
        Dim sampleComport As String = Nothing
        Dim sQuery As String
        If clsCommon.myLen(strDockCode) > 0 Then
            sampleComport = clsDockMaster.DefaultSampleComport(1, strDockCode, trans)
        Else
            sQuery = "select "
            If clsCommon.CompairString(strDocCollectionMilkType, "C") = CompairStringResult.Equal Then
                sQuery += " Sample_comport_Cow "
            Else
                sQuery += " Default_Sample_Comport "
            End If
            sQuery += " from tspl_mcc_master where mcc_code='" & mcc_code & "'"
            sampleComport = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sQuery, trans))
        End If
        Return sampleComport
    End Function


    Public Shared Function DefaultSampleMachine2(ByVal mcc_code As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleMachine2(mcc_code, "M", trans)
    End Function
    Public Shared Function DefaultSampleMachine2(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleMachine2(mcc_code, strDocCollectionMilkType, trans, "")
    End Function
    Public Shared Function DefaultSampleMachine2(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction, ByVal strDockCode As String) As String
        Dim sQuery As String
        Dim sampleMachine As String
        If clsCommon.myLen(strDockCode) > 0 Then
            sampleMachine = clsDockMaster.DefaultSampleMachine(2, strDockCode, trans)
        Else
            sQuery = "select  "
            If clsCommon.CompairString(strDocCollectionMilkType, "C") = CompairStringResult.Equal Then
                sQuery += " coalesce(Default_Sample_Machine_2_Cow,'E') "
            Else
                sQuery += " coalesce(Default_Sample_machine_2,'E') "
            End If
            sQuery += " from tspl_mcc_master where mcc_code='" & mcc_code & "'"
            sampleMachine = clsDBFuncationality.getSingleValue(sQuery, trans)
        End If
        Return sampleMachine
    End Function

    Public Shared Function DefaultSampleComport2(ByVal mcc_code As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleComport2(mcc_code, "M", trans)
    End Function
    Public Shared Function DefaultSampleComport2(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleComport2(mcc_code, strDocCollectionMilkType, trans, "")
    End Function
    Public Shared Function DefaultSampleComport2(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction, ByVal strDockCode As String) As String
        Dim sQuery As String
        Dim sampleComport As String
        If clsCommon.myLen(strDockCode) > 0 Then
            sampleComport = clsDockMaster.DefaultSampleComport(2, strDockCode, trans)
        Else
            sQuery = "select  "
            If clsCommon.CompairString(strDocCollectionMilkType, "C") = CompairStringResult.Equal Then
                sQuery += " Default_Sample_Comport_2_Cow "
            Else
                sQuery += " Default_Sample_Comport_2 "
            End If
            sQuery += " from tspl_mcc_master where mcc_code='" & mcc_code & "' "
            sampleComport = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sQuery, trans))
        End If
        Return sampleComport
    End Function

    Public Shared Function DefaultSampleMachine3(ByVal mcc_code As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleMachine3(mcc_code, "M", trans)
    End Function
    Public Shared Function DefaultSampleMachine3(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleMachine3(mcc_code, strDocCollectionMilkType, trans, "")
    End Function
    Public Shared Function DefaultSampleMachine3(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction, ByVal strDockCode As String) As String
        Dim sQuery As String
        Dim sampleMachine As String
        If clsCommon.myLen(strDockCode) > 0 Then
            sampleMachine = clsDockMaster.DefaultSampleMachine(3, strDockCode, trans)
        Else
            sQuery = "select  "
            'If clsCommon.CompairString(strDocCollectionMilkType, "C") = CompairStringResult.Equal Then
            '    sQuery += " coalesce(Default_Sample_Machine_3_Cow,'E') "
            'Else
            sQuery += " coalesce(Default_Sample_machine_3,'E') "
            'End If
            sQuery += " from tspl_mcc_master where mcc_code='" & mcc_code & "'"
            sampleMachine = clsDBFuncationality.getSingleValue(sQuery, trans)
        End If
        Return sampleMachine
    End Function

    Public Shared Function DefaultSampleComport3(ByVal mcc_code As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleComport3(mcc_code, "M", trans)
    End Function
    Public Shared Function DefaultSampleComport3(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleComport3(mcc_code, strDocCollectionMilkType, trans, "")
    End Function
    Public Shared Function DefaultSampleComport3(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction, ByVal strDockCode As String) As String
        Dim sQuery As String
        Dim sampleComport As String
        If clsCommon.myLen(strDockCode) > 0 Then
            sampleComport = clsDockMaster.DefaultSampleComport(3, strDockCode, trans)
        Else
            sQuery = "select  "
            'If clsCommon.CompairString(strDocCollectionMilkType, "C") = CompairStringResult.Equal Then
            '    sQuery += " Default_Sample_Comport_3_Cow "
            'Else
            sQuery += " Default_Sample_Comport_3 "
            'End If
            sQuery += " from tspl_mcc_master where mcc_code='" & mcc_code & "' "
            sampleComport = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sQuery, trans))
        End If
        Return sampleComport
    End Function

    Public Shared Function DefaultSampleMachine4(ByVal mcc_code As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleMachine4(mcc_code, "M", trans)
    End Function
    Public Shared Function DefaultSampleMachine4(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleMachine4(mcc_code, strDocCollectionMilkType, trans, "")
    End Function
    Public Shared Function DefaultSampleMachine4(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction, ByVal strDockCode As String) As String
        Dim sQuery As String
        Dim sampleMachine As String
        If clsCommon.myLen(strDockCode) > 0 Then
            sampleMachine = clsDockMaster.DefaultSampleMachine(4, strDockCode, trans)
        Else
            sQuery = "select  "
            'If clsCommon.CompairString(strDocCollectionMilkType, "C") = CompairStringResult.Equal Then
            '    sQuery += " coalesce(Default_Sample_Machine_4_Cow,'E') "
            'Else
            sQuery += " coalesce(Default_Sample_machine_4,'E') "
            'End If
            sQuery += " from tspl_mcc_master where mcc_code='" & mcc_code & "'"
            sampleMachine = clsDBFuncationality.getSingleValue(sQuery, trans)
        End If
        Return sampleMachine
    End Function

    Public Shared Function DefaultSampleComport4(ByVal mcc_code As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleComport4(mcc_code, "M", trans)
    End Function
    Public Shared Function DefaultSampleComport4(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction) As String
        Return DefaultSampleComport4(mcc_code, strDocCollectionMilkType, trans, "")
    End Function
    Public Shared Function DefaultSampleComport4(ByVal mcc_code As String, ByVal strDocCollectionMilkType As String, ByVal trans As SqlTransaction, ByVal strDockCode As String) As String
        Dim sQuery As String
        Dim sampleComport As String
        If clsCommon.myLen(strDockCode) > 0 Then
            sampleComport = clsDockMaster.DefaultSampleComport(4, strDockCode, trans)
        Else
            sQuery = "select  "
            'If clsCommon.CompairString(strDocCollectionMilkType, "C") = CompairStringResult.Equal Then
            '    sQuery += " Default_Sample_Comport_4_Cow "
            'Else
            sQuery += " Default_Sample_Comport_4 "
            'End If
            sQuery += " from tspl_mcc_master where mcc_code='" & mcc_code & "' "
            sampleComport = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sQuery, trans))
        End If
        Return sampleComport
    End Function

    Public Shared Function GetDataBankGuarantee(ByVal strcompid As String, ByVal Navtype As NavigatorType) As List(Of clsBankGuaranteeMaster)
        Dim obj As clsBankGuaranteeMaster = Nothing
        Dim objarr As New List(Of clsBankGuaranteeMaster)
        Dim qry As String = "select distinct * from tspl_bank_guarantee_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and "

        Select Case Navtype
            Case NavigatorType.Current
                qry += "  vendor_code='" + strcompid + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows()
                obj = New clsBankGuaranteeMaster
                obj.code = clsCommon.myCstr(row("docno"))
                obj.docdate = clsCommon.myCDate(row("date"))
                obj.desc = clsCommon.myCstr(row("description"))
                obj.strtdate = clsCommon.myCstr(row("start_date"))
                obj.enddate = clsCommon.myCstr(row("end_date"))
                obj.extnddate = clsCommon.myCstr(row("extended_date"))
                obj.amount = clsCommon.myCstr(row("amount"))
                obj.remarks = clsCommon.myCstr(row("remarks"))

                obj.bankcode = clsCommon.myCstr(row("bank_code"))
                obj.bankdesc = clsDBFuncationality.getSingleValue("select distinct description from tspl_bank_master where bank_code='" + obj.bankcode + "'")
                obj.vndrcode = clsCommon.myCstr(row("vendor_code"))
                obj.vndrname = clsDBFuncationality.getSingleValue("select distinct vendor_name from tspl_vendor_master where vendor_code='" + obj.vndrcode + "'")

                obj.rimnder = clsCommon.myCstr(row("reminder_days"))
                obj.post = clsCommon.myCstr(row("status"))
                obj.extndreminder = clsCommon.myCstr(row("Extnd_Reminder_Days"))
                obj.Bank_Guarantee_Type = IIf(clsCommon.myCstr(row("Bank_Guarantee_type")) = "RT", "Return", "Receiving")
                objarr.Add(obj)
            Next
        End If
        Return objarr
    End Function


End Class

Public Class clsGenSetDetail
    Public Prog_Code As String = Nothing
    Public Trans_Code As String = Nothing
    Public Line_No As Integer = Nothing
    Public Gen_Set_Desc As String = Nothing
    Public Gen_Set_Make As String = Nothing
    Public Gen_Set_KVA As String = Nothing
    Public Gen_Set_Year As String = Nothing

    Public Shared Function SaveData(ByVal arr As List(Of clsGenSetDetail), ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0
            If arr.Count > 0 Then
                deleteData(arr.Item(0).Prog_Code, arr.Item(0).Trans_Code, trans)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Prog_Code", arr.Item(i).Prog_Code)
                    clsCommon.AddColumnsForChange(coll, "Trans_Code", arr.Item(i).Trans_Code)
                    clsCommon.AddColumnsForChange(coll, "Line_No", arr.Item(i).Line_No)
                    clsCommon.AddColumnsForChange(coll, "Gen_Set_Desc", arr.Item(i).Gen_Set_Desc)

                    clsCommon.AddColumnsForChange(coll, "Gen_Set_Make", arr.Item(i).Gen_Set_Make)
                    clsCommon.AddColumnsForChange(coll, "Gen_Set_KVA", arr.Item(i).Gen_Set_KVA)
                    clsCommon.AddColumnsForChange(coll, "Gen_Set_Year", arr.Item(i).Gen_Set_Year)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_gen_set_detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            'trans.Commit()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            trans.Rollback()
        End Try
        Return issaved
    End Function

    Public Shared Function SaveData(ByVal arr As clsGenSetDetail) As Boolean
        Dim issaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            issaved = issaved And SaveData(arr, trans)
            If issaved = True Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
            trans.Rollback()
        End Try
        Return issaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsGenSetDetail, ByVal Trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Prog_Code", obj.Prog_Code)
            clsCommon.AddColumnsForChange(coll, "Trans_Code", obj.Trans_Code)
            clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
            clsCommon.AddColumnsForChange(coll, "Gen_Set_Desc", obj.Gen_Set_Desc)

            clsCommon.AddColumnsForChange(coll, "Gen_Set_Make", obj.Gen_Set_Make)
            clsCommon.AddColumnsForChange(coll, "Gen_Set_KVA", obj.Gen_Set_KVA)
            clsCommon.AddColumnsForChange(coll, "Gen_Set_Year", obj.Gen_Set_Year)

            If clsDBFuncationality.getSingleValue("select count(*) from TSPL_Gen_Set_Detail where prog_code='" & obj.Prog_Code & "' and trans_code='" & obj.Trans_Code & "' and Line_No='" & obj.Line_No & "'", Trans) = 0 Then
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_gen_set_detail", OMInsertOrUpdate.Insert, "", Trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_gen_set_detail", OMInsertOrUpdate.Update, "  prog_code='" & obj.Prog_Code & "' and trans_code='" & obj.Trans_Code & "' and Line_No='" & obj.Line_No & "'", Trans)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function LoadData(ByVal pcode As String, ByVal tcode As String) As List(Of clsGenSetDetail)
        Dim arr As New List(Of clsGenSetDetail)
        Try
            Dim obj As New clsGenSetDetail
            Dim q As String = "select * from tspl_gen_set_detail where Prog_Code='" & pcode & "' and Trans_Code='" & tcode & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsGenSetDetail
                    obj.Prog_Code = clsCommon.myCstr(dtbl.Rows(i)("Prog_Code"))
                    obj.Trans_Code = clsCommon.myCstr(dtbl.Rows(i)("Trans_Code"))
                    obj.Line_No = clsCommon.myCdbl(dtbl.Rows(i)("Line_No"))
                    obj.Gen_Set_Desc = clsCommon.myCstr(dtbl.Rows(i)("Gen_Set_Desc"))

                    obj.Gen_Set_Make = clsCommon.myCstr(dtbl.Rows(i)("Gen_Set_Make"))
                    obj.Gen_Set_KVA = clsCommon.myCstr(dtbl.Rows(i)("Gen_Set_KVA"))
                    obj.Gen_Set_Year = clsCommon.myCstr(dtbl.Rows(i)("Gen_Set_Year"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function
    Public Shared Function deleteData(ByVal pcode As String, ByVal tcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from tspl_gen_set_detail where prog_code='" & pcode & "' and trans_code='" & tcode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function


End Class
Public Class clsMccEmployee
    Public Prog_Code As String = Nothing
    Public Trans_Code As String = Nothing
    Public Line_No As Integer = Nothing
    Public Emp_Code As String = Nothing
    Public Emp_Name As String = Nothing


    Public Shared Function SaveData(ByVal arr As List(Of clsMccEmployee), ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0
            If arr.Count > 0 Then
                deleteData(arr.Item(0).Prog_Code, arr.Item(0).Trans_Code, trans)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Prog_Code", arr.Item(i).Prog_Code)
                    clsCommon.AddColumnsForChange(coll, "Trans_Code", arr.Item(i).Trans_Code)
                    clsCommon.AddColumnsForChange(coll, "Line_No", arr.Item(i).Line_No)
                    clsCommon.AddColumnsForChange(coll, "Emp_Code", arr.Item(i).Emp_Code)
                    clsCommon.AddColumnsForChange(coll, "Emp_Name", arr.Item(i).Emp_Name)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_Mcc_Employee", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            'trans.Commit()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            trans.Rollback()
        End Try
        Return issaved
    End Function

    Public Shared Function SaveData(ByVal arr As clsMccEmployee) As Boolean
        Dim issaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            issaved = issaved And SaveData(arr, trans)
            If issaved = True Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.ToString)
        End Try
        Return issaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsMccEmployee, ByVal Trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Prog_Code", obj.Prog_Code)
            clsCommon.AddColumnsForChange(coll, "Trans_Code", obj.Trans_Code)
            clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
            clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
            clsCommon.AddColumnsForChange(coll, "Emp_Name", obj.Emp_Name)
            If clsDBFuncationality.getSingleValue("select count(*) from TSPL_Mcc_Employee where prog_code='" & obj.Prog_Code & "' and trans_code='" & obj.Trans_Code & "' and Line_No='" & obj.Line_No & "'", Trans) = 0 Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Employee", OMInsertOrUpdate.Insert, "", Trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Employee", OMInsertOrUpdate.Update, "  prog_code='" & obj.Prog_Code & "' and trans_code='" & obj.Trans_Code & "' and Line_No='" & obj.Line_No & "'", Trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function LoadData(ByVal pcode As String, ByVal tcode As String) As List(Of clsMccEmployee)
        Dim arr As New List(Of clsMccEmployee)
        Try
            Dim obj As New clsMccEmployee
            Dim q As String = "select * from tspl_Mcc_Employee where Prog_Code='" & pcode & "' and Trans_Code='" & tcode & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsMccEmployee
                    obj.Prog_Code = clsCommon.myCstr(dtbl.Rows(i)("Prog_Code"))
                    obj.Trans_Code = clsCommon.myCstr(dtbl.Rows(i)("Trans_Code"))
                    obj.Line_No = clsCommon.myCdbl(dtbl.Rows(i)("Line_No"))
                    obj.Emp_Code = clsCommon.myCstr(dtbl.Rows(i)("Emp_Code"))
                    obj.Emp_Name = clsCommon.myCstr(dtbl.Rows(i)("Emp_Name"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function
    Public Shared Function deleteData(ByVal pcode As String, ByVal tcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from tspl_Mcc_Employee where prog_code='" & pcode & "' and trans_code='" & tcode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function


End Class

Public Class clsCompressorDetail
    Public Prog_Code As String = Nothing
    Public Trans_Code As String = Nothing
    Public Line_No As Integer = Nothing
    Public Compressor_Desc As String = Nothing
    Public Compressor_Make As String = Nothing
    Public Compressor_KVA As String = Nothing
    Public Compressor_Year As String = Nothing

    Public Shared Function LoadData(ByVal pcode As String, ByVal tcode As String) As List(Of clsCompressorDetail)
        Dim arr As New List(Of clsCompressorDetail)
        Try
            Dim obj As New clsCompressorDetail
            Dim q As String = "select * from tspl_compressor_detail where Prog_Code='" & pcode & "' and Trans_Code='" & tcode & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsCompressorDetail
                    obj.Prog_Code = clsCommon.myCstr(dtbl.Rows(i)("Prog_Code"))
                    obj.Trans_Code = clsCommon.myCstr(dtbl.Rows(i)("Trans_Code"))
                    obj.Line_No = clsCommon.myCdbl(dtbl.Rows(i)("Line_No"))
                    obj.Compressor_Desc = clsCommon.myCstr(dtbl.Rows(i)("Compressor_Desc"))

                    obj.Compressor_Make = clsCommon.myCstr(dtbl.Rows(i)("Compressor_Make"))
                    obj.Compressor_KVA = clsCommon.myCstr(dtbl.Rows(i)("Compressor_KVA"))
                    obj.Compressor_Year = clsCommon.myCstr(dtbl.Rows(i)("Compressor_Year"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function

    Public Shared Function SaveData(ByVal arr As clsCompressorDetail) As Boolean
        Dim issaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            issaved = issaved And SaveData(arr, trans)
            If issaved = True Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.ToString)
        End Try
        Return issaved
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsCompressorDetail), ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0

            If arr.Count > 0 Then
                deleteData(arr.Item(0).Prog_Code, arr.Item(0).Trans_Code, trans)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Prog_Code", arr.Item(i).Prog_Code)
                    clsCommon.AddColumnsForChange(coll, "Trans_Code", arr.Item(i).Trans_Code)
                    clsCommon.AddColumnsForChange(coll, "Line_No", arr.Item(i).Line_No)
                    clsCommon.AddColumnsForChange(coll, "Compressor_Desc", arr.Item(i).Compressor_Desc)

                    clsCommon.AddColumnsForChange(coll, "Compressor_Make", arr.Item(i).Compressor_Make)
                    clsCommon.AddColumnsForChange(coll, "Compressor_KVA", arr.Item(i).Compressor_KVA)
                    clsCommon.AddColumnsForChange(coll, "Compressor_Year", arr.Item(i).Compressor_Year)

                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_compressor_detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            ' trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function deleteData(ByVal pcode As String, ByVal tcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from tspl_compressor_detail where prog_code='" & pcode & "' and trans_code='" & tcode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function SaveData(ByVal obj As clsCompressorDetail, ByVal Trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Prog_Code", obj.Prog_Code)
            clsCommon.AddColumnsForChange(coll, "Trans_Code", obj.Trans_Code)
            clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
            clsCommon.AddColumnsForChange(coll, "Compressor_Desc", obj.Compressor_Desc)

            clsCommon.AddColumnsForChange(coll, "Compressor_Make", obj.Compressor_Make)
            clsCommon.AddColumnsForChange(coll, "Compressor_KVA", obj.Compressor_KVA)
            clsCommon.AddColumnsForChange(coll, "Compressor_Year", obj.Compressor_Year)


            If clsDBFuncationality.getSingleValue("select count(*) from TSPL_Compressor_Detail where prog_code='" & obj.Prog_Code & "' and trans_code='" & obj.Trans_Code & "' and Line_No='" & obj.Line_No & "'", Trans) = 0 Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Compressor_Detail", OMInsertOrUpdate.Insert, "", Trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Compressor_Detail", OMInsertOrUpdate.Update, "  prog_code='" & obj.Prog_Code & "' and trans_code='" & obj.Trans_Code & "' and Line_No='" & obj.Line_No & "'", Trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsSiloDetail
    Public Prog_Code As String = Nothing
    Public Trans_Code As String = Nothing
    Public Line_No As Integer = Nothing
    Public Silo_Desc As String = Nothing
    Public Silo_Area As String = Nothing
    Public Silo_Unit As String = Nothing
    Public Gaze_Reading_Code As String = Nothing


    Public Shared Function LoadData(ByVal pcode As String, ByVal tcode As String) As List(Of clsSiloDetail)
        Dim arr As New List(Of clsSiloDetail)
        Try
            Dim obj As New clsSiloDetail
            Dim q As String = "select * from tspl_Silo_detail where Prog_Code='" & pcode & "' and Trans_Code='" & tcode & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsSiloDetail
                    obj.Prog_Code = clsCommon.myCstr(dtbl.Rows(i)("Prog_Code"))
                    obj.Trans_Code = clsCommon.myCstr(dtbl.Rows(i)("Trans_Code"))
                    obj.Line_No = clsCommon.myCdbl(dtbl.Rows(i)("Line_No"))
                    obj.Silo_Desc = clsCommon.myCstr(dtbl.Rows(i)("Silo_Desc"))
                    obj.Silo_Area = clsCommon.myCstr(dtbl.Rows(i)("Silo_Area"))
                    obj.Silo_Unit = clsCommon.myCstr(dtbl.Rows(i)("Silo_Unit"))
                    obj.Gaze_Reading_Code = clsCommon.myCstr(dtbl.Rows(i)("Gaze_Reading_Code"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function

    Public Shared Function SaveData(ByVal arr As clsSiloDetail) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved And SaveData(arr, trans)
            If issaved = True Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.ToString)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsSiloDetail), ByVal Trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0
            If arr.Count > 0 Then
                deleteData(arr.Item(0).Prog_Code, arr.Item(0).Trans_Code, Trans)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Prog_Code", arr.Item(i).Prog_Code)
                    clsCommon.AddColumnsForChange(coll, "Trans_Code", arr.Item(i).Trans_Code)
                    clsCommon.AddColumnsForChange(coll, "Line_No", arr.Item(i).Line_No)
                    clsCommon.AddColumnsForChange(coll, "Silo_Desc", arr.Item(i).Silo_Desc)

                    clsCommon.AddColumnsForChange(coll, "Silo_Area", arr.Item(i).Silo_Area)
                    clsCommon.AddColumnsForChange(coll, "Silo_Unit", arr.Item(i).Silo_Unit)
                    clsCommon.AddColumnsForChange(coll, "Gaze_Reading_Code", arr.Item(i).Gaze_Reading_Code, True)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILO_DETAIL", OMInsertOrUpdate.Insert, "", Trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function deleteData(ByVal pcode As String, ByVal tcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from tspl_Silo_detail where prog_code='" & pcode & "' and trans_code='" & tcode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function SaveData(ByVal obj As clsSiloDetail, ByVal Trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Prog_Code", obj.Prog_Code)
            clsCommon.AddColumnsForChange(coll, "Trans_Code", obj.Trans_Code)
            clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
            clsCommon.AddColumnsForChange(coll, "Silo_Desc", obj.Silo_Desc)

            clsCommon.AddColumnsForChange(coll, "Silo_Area", obj.Silo_Area)
            clsCommon.AddColumnsForChange(coll, "Silo_Unit", obj.Silo_Unit)
            clsCommon.AddColumnsForChange(coll, "Gaze_Reading_Code", obj.Gaze_Reading_Code, True)
            If clsDBFuncationality.getSingleValue("select count(*) from TSPL_Silo_Detail where prog_code='" & obj.Prog_Code & "' and trans_code='" & obj.Trans_Code & "' and Line_No='" & obj.Line_No & "'", Trans) = 0 Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Silo_Detail", OMInsertOrUpdate.Insert, "", Trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Silo_Detail", OMInsertOrUpdate.Update, "  prog_code='" & obj.Prog_Code & "' and trans_code='" & obj.Trans_Code & "' and Line_No='" & obj.Line_No & "'", Trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsMilkPumpDetail
    Public Prog_Code As String = Nothing
    Public Trans_Code As String = Nothing
    Public Line_No As Integer = Nothing
    Public Pump_Desc As String = Nothing
    Public Pump_Area As String = Nothing
    Public Pump_Unit As String = Nothing


    Public Shared Function LoadData(ByVal pcode As String, ByVal tcode As String) As List(Of clsMilkPumpDetail)
        Dim arr As New List(Of clsMilkPumpDetail)
        Try
            Dim obj As New clsMilkPumpDetail
            Dim q As String = "select * from TSPL_Milk_Pump_Detail where Prog_Code='" & pcode & "' and Trans_Code='" & tcode & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsMilkPumpDetail
                    obj.Prog_Code = clsCommon.myCstr(dtbl.Rows(i)("Prog_Code"))
                    obj.Trans_Code = clsCommon.myCstr(dtbl.Rows(i)("Trans_Code"))
                    obj.Line_No = clsCommon.myCdbl(dtbl.Rows(i)("Line_No"))
                    obj.Pump_Desc = clsCommon.myCstr(dtbl.Rows(i)("Milk_Pump_Desc"))
                    obj.Pump_Area = clsCommon.myCstr(dtbl.Rows(i)("Milk_Pump_Area"))
                    obj.Pump_Unit = clsCommon.myCstr(dtbl.Rows(i)("Milk_Pump_Unit"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function

    Public Shared Function SaveData(ByVal arr As clsMilkPumpDetail) As Boolean
        Dim issaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            issaved = issaved And SaveData(arr, trans)
            If issaved = True Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.ToString)
        End Try
        Return issaved
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsMilkPumpDetail), ByVal Trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0
            If arr.Count > 0 Then
                deleteData(arr.Item(0).Prog_Code, arr.Item(0).Trans_Code, Trans)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Prog_Code", arr.Item(i).Prog_Code)
                    clsCommon.AddColumnsForChange(coll, "Trans_Code", arr.Item(i).Trans_Code)
                    clsCommon.AddColumnsForChange(coll, "Line_No", arr.Item(i).Line_No)
                    clsCommon.AddColumnsForChange(coll, "Milk_Pump_Desc", arr.Item(i).Pump_Desc)

                    clsCommon.AddColumnsForChange(coll, "Milk_Pump_Area", arr.Item(i).Pump_Area)
                    clsCommon.AddColumnsForChange(coll, "Milk_Pump_Unit", arr.Item(i).Pump_Unit)

                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Milk_Pump_Detail", OMInsertOrUpdate.Insert, "", Trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function deleteData(ByVal pcode As String, ByVal tcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_Milk_Pump_Detail where prog_code='" & pcode & "' and trans_code='" & tcode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function SaveData(ByVal obj As clsMilkPumpDetail, ByVal Trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Prog_Code", obj.Prog_Code)
            clsCommon.AddColumnsForChange(coll, "Trans_Code", obj.Trans_Code)
            clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
            clsCommon.AddColumnsForChange(coll, "Milk_Pump_Desc", obj.Pump_Desc)

            clsCommon.AddColumnsForChange(coll, "Milk_Pump_Area", obj.Pump_Area)
            clsCommon.AddColumnsForChange(coll, "Milk_Pump_Unit", obj.Pump_Unit)

            If clsDBFuncationality.getSingleValue("select count(*) from TSPL_Milk_Pump_Detail where prog_code='" & obj.Prog_Code & "' and trans_code='" & obj.Trans_Code & "' and Line_No='" & obj.Line_No & "'", Trans) = 0 Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Milk_Pump_Detail", OMInsertOrUpdate.Insert, "", Trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Milk_Pump_Detail", OMInsertOrUpdate.Update, "  prog_code='" & obj.Prog_Code & "' and trans_code='" & obj.Trans_Code & "' and Line_No='" & obj.Line_No & "'", Trans)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsChillerDetail
    Public Prog_Code As String = Nothing
    Public Trans_Code As String = Nothing
    Public Line_No As Integer = Nothing
    Public Chiller_Desc As String = Nothing
    Public Chiller_Brand As String = Nothing
    Public Chiller_Capacity As String = Nothing


    Public Shared Function LoadData(ByVal pcode As String, ByVal tcode As String) As List(Of clsChillerDetail)
        Dim arr As New List(Of clsChillerDetail)
        Try
            Dim obj As New clsChillerDetail
            Dim q As String = "select * from TSPL_Chiller_Detail where Prog_Code='" & pcode & "' and Trans_Code='" & tcode & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsChillerDetail
                    obj.Prog_Code = clsCommon.myCstr(dtbl.Rows(i)("Prog_Code"))
                    obj.Trans_Code = clsCommon.myCstr(dtbl.Rows(i)("Trans_Code"))
                    obj.Line_No = clsCommon.myCdbl(dtbl.Rows(i)("Line_No"))
                    obj.Chiller_Desc = clsCommon.myCstr(dtbl.Rows(i)("Chiller_Desc"))
                    obj.Chiller_Brand = clsCommon.myCstr(dtbl.Rows(i)("Chiller_Brand"))
                    obj.Chiller_Capacity = clsCommon.myCstr(dtbl.Rows(i)("Chiller_Capacity"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function

    Public Shared Function SaveData(ByVal arr As clsChillerDetail) As Boolean
        Dim issaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            issaved = issaved And SaveData(arr, trans)
            If issaved = True Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.ToString)
        End Try
        Return issaved
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsChillerDetail), ByVal Trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0
            If arr.Count > 0 Then
                deleteData(arr.Item(0).Prog_Code, arr.Item(0).Trans_Code, Trans)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Prog_Code", arr.Item(i).Prog_Code)
                    clsCommon.AddColumnsForChange(coll, "Trans_Code", arr.Item(i).Trans_Code)
                    clsCommon.AddColumnsForChange(coll, "Line_No", arr.Item(i).Line_No)
                    clsCommon.AddColumnsForChange(coll, "Chiller_Desc", arr.Item(i).Chiller_Desc)
                    clsCommon.AddColumnsForChange(coll, "Chiller_Brand", arr.Item(i).Chiller_Brand)
                    clsCommon.AddColumnsForChange(coll, "Chiller_Capacity", arr.Item(i).Chiller_Capacity)

                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Chiller_Detail", OMInsertOrUpdate.Insert, "", Trans)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function deleteData(ByVal pcode As String, ByVal tcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_Chiller_Detail where prog_code='" & pcode & "' and trans_code='" & tcode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function SaveData(ByVal obj As clsChillerDetail, ByVal Trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Prog_Code", obj.Prog_Code)
            clsCommon.AddColumnsForChange(coll, "Trans_Code", obj.Trans_Code)
            clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
            clsCommon.AddColumnsForChange(coll, "Chiller_Desc", obj.Chiller_Desc)
            clsCommon.AddColumnsForChange(coll, "Chiller_Brand", obj.Chiller_Brand)
            clsCommon.AddColumnsForChange(coll, "Chiller_Capacity", obj.Chiller_Capacity)

            If clsDBFuncationality.getSingleValue("select count(*) from TSPL_Chiller_Detail where prog_code='" & obj.Prog_Code & "' and trans_code='" & obj.Trans_Code & "' and Line_No='" & obj.Line_No & "'", Trans) = 0 Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Chiller_Detail", OMInsertOrUpdate.Insert, "", Trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Chiller_Detail", OMInsertOrUpdate.Update, "  prog_code='" & obj.Prog_Code & "' and trans_code='" & obj.Trans_Code & "' and Line_No='" & obj.Line_No & "'", Trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsErrorControl
    Public errProv As New ErrorProvider
    Public Sub SetError(ByRef cntrl As Control, ByVal msg As String)
        errProv.SetError(TryCast(cntrl, Control), msg)
    End Sub
    Public Sub ResetError(ByRef cntrl As Control)
        errProv.SetError(TryCast(cntrl, Control), "")
    End Sub
End Class

Public Class clsPayment_Detail_MCC
    Public Payment_No As String = Nothing
    Public Payment_Date As String = Nothing
    Public Description As String = Nothing
    Public Bank_Name As String = Nothing
    Public Payment_Type As String = Nothing
    Public Bank_Charges As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Cheque_No As String = Nothing
    Public Cheque_Date As String = Nothing
    Public Payment_Mode As String = Nothing
    Public Payment_Amount As Double = 0

    Public Shared Function GetPaymentData(ByVal pcode As String) As List(Of clsPayment_Detail_MCC)
        Dim arr As New List(Of clsPayment_Detail_MCC)
        Try
            Dim obj As New clsPayment_Detail_MCC
            ' or (Payment_Type  in ('RC') and coalesce(is_security,0)='1'))
            Dim q As String = " select Payment_No,Payment_Date,Bank_Code,Entry_Desc,Vendor_Code,Vendor_Name,case when Payment_Type='PY' then 'Payment' " _
            & " when Payment_Type='AV' then 'Advance' when Payment_Type='RC' then 'Receipt' when Payment_Type='SR' then 'Refund'  when Payment_Type='OA' then 'On Account' end as " _
            & " Payment_Type,Bank_Charges,Payment_Code,Cheque_No,Cheque_Date, case when Payment_Type='RC' then Payment_Amount  when Payment_Type='SR' " _
            & " then  -PAYMENT_AMOUNT_BASE_CURRENCY end as Payment_Amount from TSPL_PAYMENT_HEADER where Payment_Type  in ('RC','SR') and " _
            & " coalesce(is_security,0)='1' and vendor_Code='" & pcode & "' " _
            & " Union " _
            & " select Document_No,convert(date,Invoice_Entry_Date,103) ,null,Description,Vendor_Code,Vendor_Name,'Debit Note',0,null,null,NULL, " _
            & " Total_Landed_Amt  from TSPL_VENDOR_INVOICE_HEAD where Document_Type='D' and coalesce(is_security,0)=1  and vendor_Code='" & pcode & "' "
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsPayment_Detail_MCC
                    obj.Payment_No = clsCommon.myCstr(dtbl.Rows(i)("Payment_No"))
                    obj.Payment_Date = clsCommon.myCstr(dtbl.Rows(i)("Payment_Date"))
                    obj.Description = clsCommon.myCstr(dtbl.Rows(i)("Entry_Desc"))
                    obj.Bank_Name = clsCommon.myCstr(dtbl.Rows(i)("Bank_Code"))
                    obj.Payment_Type = clsCommon.myCstr(dtbl.Rows(i)("Payment_Type"))
                    obj.Bank_Charges = clsCommon.myCstr(dtbl.Rows(i)("Bank_Charges"))
                    obj.Vendor_Code = clsCommon.myCstr(dtbl.Rows(i)("Vendor_Code"))
                    obj.Vendor_Name = clsCommon.myCstr(dtbl.Rows(i)("Vendor_Name"))
                    obj.Cheque_No = clsCommon.myCstr(dtbl.Rows(i)("Cheque_No"))
                    obj.Cheque_Date = clsCommon.myCstr(dtbl.Rows(i)("Cheque_Date"))
                    obj.Payment_Mode = clsCommon.myCstr(dtbl.Rows(i)("Payment_Code"))
                    obj.Payment_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Payment_Amount"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function
End Class

Public Class clsMccUOMDetails
    Public Mcc_Code As String = ""
    Public UOM_Code As String = ""
    Public UOM_Description As String = ""
    Public Conversion_Factor As Double = 0
    Public Stocking_Unit As String = ""


    Public Shared Function SaveData(ByVal strICode As String, ByVal ArrUomDetails As List(Of clsMccUOMDetails), ByVal trans As SqlTransaction) As Boolean
        SaveDataEditLog(strICode, ArrUomDetails, trans)
        Dim isSaved As Boolean = True
        For Each obj As clsMccUOMDetails In ArrUomDetails
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Mcc_Code", strICode)
            clsCommon.AddColumnsForChange(coll, "UOM_Code", obj.UOM_Code)
            clsCommon.AddColumnsForChange(coll, "UOM_Description", obj.UOM_Description)
            clsCommon.AddColumnsForChange(coll, "Conversion_Factor", obj.Conversion_Factor)
            clsCommon.AddColumnsForChange(coll, "Stocking_Unit", obj.Stocking_Unit)
            If clsDBFuncationality.getSingleValue("select count(*) from TSPL_MCC_UOM_DETAIL where Mcc_code='" & strICode & "' and Uom_code='" & obj.UOM_Code & "' ", trans) = 0 Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_UOM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_UOM_DETAIL", OMInsertOrUpdate.Update, "Mcc_code='" & strICode & "' and Uom_code='" & obj.UOM_Code & "'", trans)
            End If
        Next
        Return isSaved
    End Function

    Public Shared Function SaveDataEditLog(ByVal strICode As String, ByVal ArrUomDetails As List(Of clsMccUOMDetails), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        For Each obj As clsMccUOMDetails In ArrUomDetails
            Dim sQuery As String = "select count(*) from tspl_Mcc_UOM_Detail where mcc_Code='" & strICode & "' and UOM_Code='" & obj.UOM_Code & "' and Stocking_Unit='" & obj.Stocking_Unit & "'"
            Dim isExits As Integer = clsDBFuncationality.getSingleValue(sQuery, trans)
            If isExits <= 0 Then
                sQuery = " update tspl_Mcc_Master set edit_log = cast(coalesce(edit_log,'') as varchar(mAX)) + '  '   + cast( '" & clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MMM/yyyy hh:mm:ss")) & " - " & obj.UOM_Code & "(" & obj.Stocking_Unit & ")' as varchar) where mcc_Code='" & strICode & "'"
                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            End If
        Next
        Return isSaved
    End Function

    Public Shared Function GetName(ByVal strcode As String, ByVal trans As SqlTransaction)
        Dim qry As String = "select Unit_Desc from TSPL_UNIT_MASTER where Unit_Code='" + strcode + "'"
        Return clsDBFuncationality.getSingleValue(qry, trans)
    End Function

    Public Shared Function LoadData(ByVal pcode As String) As List(Of clsMccUOMDetails)
        Dim arr As New List(Of clsMccUOMDetails)
        Try
            Dim obj As New clsMccUOMDetails
            Dim q As String = " select Mcc_Code,UOM_Code,UOM_Description,Conversion_Factor,Stocking_Unit from TSPL_Mcc_UOM_DETAIL where Mcc_Code='" + pcode + "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsMccUOMDetails
                    obj.Mcc_Code = clsCommon.myCstr(dtbl.Rows(i)("Mcc_Code"))
                    obj.UOM_Code = clsCommon.myCstr(dtbl.Rows(i)("UOM_Code"))
                    obj.UOM_Description = clsCommon.myCstr(dtbl.Rows(i)("UOM_Description"))
                    obj.Conversion_Factor = clsCommon.myCdbl(dtbl.Rows(i)("Conversion_Factor"))
                    obj.Stocking_Unit = clsCommon.myCstr(dtbl.Rows(i)("Stocking_Unit"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function
End Class

Public Class clsMCCChequeDetails
    Public Prog_Code As String = Nothing
    Public Check_No As String = Nothing
    Public Check_date As String = Nothing

    Public Shared Function LoadData(ByVal pcode As String) As List(Of clsMCCChequeDetails)
        Dim arr As New List(Of clsMCCChequeDetails)
        Try
            Dim obj As New clsMCCChequeDetails
            Dim q As String = "select * from TSPL_MCC_Cheque_Detail where Prog_Code='" & pcode & "' "
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsMCCChequeDetails
                    obj.Prog_Code = clsCommon.myCstr(dtbl.Rows(i)("Prog_Code"))
                    obj.Check_No = clsCommon.myCstr(dtbl.Rows(i)("Cheque_No"))
                    obj.Check_date = clsCommon.myCDate(dtbl.Rows(i)("Cheque_Date"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function
    Public Shared Function SaveData(ByVal arr As List(Of clsMCCChequeDetails), ByVal Trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0
            If arr.Count > 0 Then
                deleteData(arr.Item(0).Prog_Code, Trans)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Prog_Code", arr.Item(i).Prog_Code)
                    clsCommon.AddColumnsForChange(coll, "Cheque_No", arr.Item(i).Check_No)
                    clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(arr.Item(i).Check_date, "dd-MMM-yyyy"))
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_Cheque_Detail", OMInsertOrUpdate.Insert, "", Trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function deleteData(ByVal pcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_MCC_Cheque_Detail where prog_code='" & pcode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function SaveData(ByVal obj As clsMCCChequeDetails, ByVal Trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Prog_Code", obj.Prog_Code)
            clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.Check_No)
            clsCommon.AddColumnsForChange(coll, "Cheque_date", clsCommon.GetPrintDate(obj.Check_date, "dd-MMM-yyyy"))

            If clsDBFuncationality.getSingleValue("select count(*) from TSPL_MCC_Cheque_Detail where prog_code='" & obj.Prog_Code & "' ", Trans) = 0 Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_Cheque_Detail", OMInsertOrUpdate.Insert, "", Trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_Cheque_Detail", OMInsertOrUpdate.Update, "  prog_code='" & obj.Prog_Code & "' ", Trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsEkoPro
    Public Shared FAT As Double = 0
    Public Shared SNF As Double = 0
    Public Shared DEN As Double = 0
    Public Shared AWM As Double = 0
    Public Shared FP As Double = 0
    Public Shared PROT As Double = 0


    Public Function getData(ByVal comPort As String, ByVal bRate As String, ByVal Parity As String, ByVal dBits As String, ByVal sBits As String, Optional ByVal Data_Form As String = "", Optional ByRef lFat As common.Controls.MyLabel = Nothing, Optional ByRef lSNf As common.Controls.MyLabel = Nothing) As clsEkoPro
        Try
            Dim objSerail As clsSerialPort = Nothing
            Dim obj As clsEkoPro = New clsEkoPro()
            objSerail = New clsSerialPort()
            objSerail.PortName = comPort
            objSerail.BaudRate = bRate
            objSerail.Parity = Parity
            objSerail.DataBits = dBits
            objSerail.StopBits = sBits
            objSerail.DataForm = Data_Form
            objSerail.isWeighingMachine = False
            objSerail.isEkoProMachine = True
            objSerail.OpenPort()
        Catch ex As Exception
            Return Nothing
        End Try
        Return Nothing
    End Function
    Public Shared Function FormattedNumber(ByVal n As Double) As String
        Dim intPart As Integer = 0
        Dim FracPart As Integer = 0
        intPart = CInt(n)
        FracPart = (n - intPart) * 100
        Dim rValue As String = "00.00"
        rValue = IIf(intPart > 9, intPart, "0" & intPart) & "." & IIf(FracPart > 9, FracPart, "0" & FracPart)
        Return rValue
    End Function
    Public Function getDataMachineWise(ByVal comPort As String, ByVal comMac As String) As clsEkoPro
        Dim Obj As clsPortSetting = clsPortSetting.getDataMachineWise(0, comMac)
        If Obj IsNot Nothing Then
            Return getData(comPort, Obj.baud_rate, Obj.parity, Obj.data_bits, Obj.stop_bits, Obj.Data_Form)
        End If
        Return Nothing
    End Function
    Public Function getDataMachineWise(ByVal comPort As String, ByVal comMac As String, ByRef lFat As common.Controls.MyLabel, ByRef lSNf As common.Controls.MyLabel) As clsEkoPro
        Dim Obj As clsPortSetting = clsPortSetting.getDataMachineWise(0, comMac)
        If Obj IsNot Nothing Then
            getData(comPort, Obj.baud_rate, Obj.parity, Obj.data_bits, Obj.stop_bits, Obj.Data_Form, lFat, lSNf)
        End If
        Return Nothing
    End Function

    Public Shared Function getSnfOnCalculation(ByVal FatPer As Double, ByVal CLR As Double, ByVal CorrectionFactor As Double, Optional ByVal DeciPace As Integer = -1) As Double
        Dim ParameterForSNFatQC As Decimal = objCommonVar.ParameterForSNFatQC
        If ParameterForSNFatQC = 0 Then
            ParameterForSNFatQC = 0.2
        End If
        Dim SNF As Double = 0
        If DeciPace <> -1 Then
            FatPer = Microsoft.VisualBasic.Left(FatPer.ToString, InStr(0, FatPer.ToString, ".") + 1)
        End If
        SNF = CLR / 4 + ParameterForSNFatQC * FatPer + CorrectionFactor
        If ParameterForSNFatQC = 0.2 Then
            SNF = MyMath.RoundDown(SNF, 2)
        Else
            SNF = Math.Round(SNF, 2, MidpointRounding.ToEven)
        End If
        Return SNF
    End Function

    Public Shared Function getClrOnCalculation(ByVal FatPer As Double, ByVal SNF As Double, ByVal CorrectionFactor As Double) As Double
        Dim CLR As Double = 0
        CLR = (SNF - CorrectionFactor - (0.2 * FatPer)) * 4
        Return CLR
    End Function

    Public Shared Function getRateFromUploader(ByVal FatPer As Double, ByVal SNF As Double, ByVal MccCode As String, ByVal vlcCode As String, ByVal Doc_Date As Date) As Double
        Dim Rate As Double = 0
        Try
            If Not clsVendorMaster.IsVLCDripSaver(vlcCode, Nothing) Then
                Rate = clsDBFuncationality.getSingleValue("select top 1 rate from TSPL_FAT_SNF_UPLOADER_MASTER inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" & MccCode & "' and " _
               & " TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code inner join TSPL_FAT_SNF_UPLOADER_VLC on VLC_Code='" & vlcCode & "' and " _
               & " TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_VLC.Code where posted='1' and fat=" & FatPer & " and SNF=" & GetSNFForPrice(SNF) & " and date<= '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' and ( TSPL_FAT_SNF_UPLOADER_MASTER.In_Active_From is null or TSPL_FAT_SNF_UPLOADER_MASTER.In_Active_From > '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' )  order by date desc ,TSPL_FAT_SNF_UPLOADER_MASTER.code desc")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Rate
    End Function

    Public Shared Function getPriceCodeFromUploader(ByVal FatPer As Double, ByVal SNF As Double, ByVal MccCode As String, ByVal vlcCode As String, ByVal Doc_Date As Date) As String
        Dim Price_Code As String = ""
        If Not clsVendorMaster.IsVLCDripSaver(vlcCode, Nothing) Then
            Price_Code = clsDBFuncationality.getSingleValue("select top 1 TSPL_FAT_SNF_UPLOADER_MASTER.code from TSPL_FAT_SNF_UPLOADER_MASTER inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" & MccCode & "' and " _
            & " TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code inner join TSPL_FAT_SNF_UPLOADER_VLC on VLC_Code='" & vlcCode & "' and " _
            & " TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_VLC.Code where  posted='1' and  fat=" & FatPer & " and SNF=" & GetSNFForPrice(SNF) & " and date<= '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' and and ( In_Active_From is null or In_Active_From > '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' )  order by date desc ,TSPL_FAT_SNF_UPLOADER_MASTER.code desc")
        End If
        Return Price_Code
    End Function

    Public Shared Function getRateFromUploaderShiftWise(ByVal FatPer As Double, ByVal SNF As Double, ByVal MccCode As String, ByVal vlcCode As String, ByVal Shift As String, ByVal Doc_Date As Date) As Double
        Return getRateFromUploaderShiftWise(FatPer, SNF, MccCode, vlcCode, Shift, Doc_Date, Nothing)
    End Function

    Public Shared Function getRateFromUploaderShiftWise(ByVal FatPer As Double, ByVal SNF As Double, ByVal MccCode As String, ByVal vlcCode As String, ByVal Shift As String, ByVal Doc_Date As Date, ByVal tran As SqlTransaction) As Double
        Return getRateFromUploaderShiftWise(FatPer, SNF, MccCode, vlcCode, Shift, Doc_Date, tran, "M")
    End Function

    Public Shared Function getRateFromUploaderShiftWise(ByVal FatPer As Double, ByVal SNF As Double, ByVal MccCode As String, ByVal vlcCode As String, ByVal Shift As String, ByVal Doc_Date As Date, ByVal tran As SqlTransaction, ByVal strMilkType As String) As Double
        Return getRateFromUploaderShiftWise(FatPer, SNF, MccCode, vlcCode, Shift, Doc_Date, tran, strMilkType, False)
    End Function
    Public Shared Function getRateFromUploaderShiftWise(ByVal FatPer As Double, ByVal SNF As Double, ByVal MccCode As String, ByVal vlcCode As String, ByVal Shift As String, ByVal Doc_Date As Date, ByVal tran As SqlTransaction, ByVal strMilkType As String, ByVal IsPlaningCodeNull As Boolean) As Double
        Dim Rate As Double = 0
        Try
            If Not clsVendorMaster.IsVLCDripSaver(vlcCode, tran) Then
                Dim qry As String = "select top 1 rate from TSPL_FAT_SNF_UPLOADER_MASTER inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" & MccCode & "' and " _
                & " TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code inner join TSPL_FAT_SNF_UPLOADER_VLC on VLC_Code='" & vlcCode & "' and " _
                & " TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_VLC.Code where  posted='1'"
                If objCommonVar.DisplayTypeInMilkReceipt Then
                    qry += " and TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type='" + strMilkType + "' "
                ElseIf objCommonVar.SepratePriceChartForCow AndAlso clsCommon.CompairString(strMilkType, "C") = CompairStringResult.Equal Then
                    qry += " and TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type='C' "
                Else
                    qry += " and TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type='M' "
                End If
                If IsPlaningCodeNull Then
                    qry += " and TSPL_FAT_SNF_UPLOADER_MASTER.Planning_Code is null"
                End If
                qry += "  and  fat=" & FatPer & " and SNF=" & GetSNFForPrice(SNF) & " and (date< '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' or (date= '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' and Price_code_shift>='" & Shift & "')) and ( TSPL_FAT_SNF_UPLOADER_MASTER.In_Active_From is null or TSPL_FAT_SNF_UPLOADER_MASTER.In_Active_From > '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' ) order by date desc ,TSPL_FAT_SNF_UPLOADER_MASTER.code desc"
                Rate = clsDBFuncationality.getSingleValue(qry, tran)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Rate
    End Function

    Public Shared Function GetLatestPlaningQry(ByVal Doc_Date As Date, ByVal Shift As String, ByVal strMilkType As String) As String
        Return "select Planning_Code,Price_Chart_Code,Single_Axis_FAT_Per,Single_Axis_SNFDed_FAT_Per,Single_Axis_SNF_Per,Single_Axis_SNFDed_SNF_Per,TSDDCS_Rate
 from TSPL_PRICE_CHART_PLANNING where Dock_Collection_Milk_Type='" + strMilkType + "' and Status=1 and  
 ((convert(Date,Planning_Date,103)< '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' or (convert(Date,Planning_Date,103)= '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' and shift>='" & Shift & "')))
  order by Planning_Date desc,Planning_Code desc"
    End Function

    Public Shared Function GetRateCalculatedRAJ(ByRef strPlanningCode As String, ByVal Doc_Date As Date, ByVal Shift As String, ByVal VLC_Code As String, ByVal strMilkType As String, ByVal Qty As Decimal, ByVal FatPer As Double, ByVal SNFPer As Double, ByVal tran As SqlTransaction) As Decimal
        strPlanningCode = ""
        Dim strPriceMasterCode As String = Nothing
        If Qty <= 0 Then
            Return 0
        End If
        Dim qry As String = ""
        Dim dt As DataTable = Nothing
        Dim ApplyMixMilk As Boolean = True
        If objCommonVar.SepratePriceChartForCow Then
            'clsCommon.CompairString(strMilkType, "C") = CompairStringResult.Equal AndAlso
            If clsfrmVLCMaster.CowPriceApplied(VLC_Code, Doc_Date, tran) Then
                qry = GetLatestPlaningQry(Doc_Date, Shift, "C")
                dt = clsDBFuncationality.GetDataTable(qry, tran)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.myCDecimal(dt.Rows(0)("Single_Axis_FAT_Per")) <= FatPer AndAlso
                       clsCommon.myCDecimal(dt.Rows(0)("Single_Axis_SNFDed_FAT_Per")) >= FatPer AndAlso
                       clsCommon.myCDecimal(dt.Rows(0)("Single_Axis_SNF_Per")) <= SNFPer AndAlso
                       clsCommon.myCDecimal(dt.Rows(0)("Single_Axis_SNFDed_SNF_Per")) >= SNFPer Then
                        ApplyMixMilk = False
                        strPlanningCode = clsCommon.myCstr(dt.Rows(0)("Planning_Code"))
                        strPriceMasterCode = clsCommon.myCstr(dt.Rows(0)("Price_Chart_Code"))
                    End If
                End If
            End If
        End If
        If ApplyMixMilk Then
            qry = GetLatestPlaningQry(Doc_Date, Shift, "M")
            dt = clsDBFuncationality.GetDataTable(qry, tran)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                strPlanningCode = clsCommon.myCstr(dt.Rows(0)("Planning_Code"))
                strPriceMasterCode = clsCommon.myCstr(dt.Rows(0)("Price_Chart_Code"))
            End If
        End If
        qry = "select  Rate  from TSPL_PRICE_CHART_PLANNING_EXCEPTION where Planning_Code='" + strPlanningCode + "' and FAT='" + clsCommon.myCstr(FatPer) + "' and SNF ='" + clsCommon.myCstr(SNFPer) + "'"
        dt = clsDBFuncationality.GetDataTable(qry, tran)
        Dim RetVal As Decimal = 0
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            RetVal = clsCommon.myCdbl(dt.Rows(0)("Rate"))
        Else
            qry = "select Price_code, Ratio,SNF_Ratio,FAT_Pers,SNF_Pers,Milk_Rate from TSPL_MILK_PRICE_MASTER where Price_code='" + strPriceMasterCode + "'"
            dt = clsDBFuncationality.GetDataTable(qry, tran)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Milk Price Master Code [" + strPriceMasterCode + "] not exists")
            End If
            If clsCommon.myLen(strPlanningCode) > 0 Then
                qry = "select GK_Min_FAT_Per,GK_Max_FAT_Per,GK_Min_SNF_Per,GK_Max_SNF_Per,GK_Is_FAT_Rate_Zero_After_Max,GK_Is_SNF_Rate_Zero_After_Max,TSPL_PRICE_CHART_PLANNING.UCDF_SNF_Ded_Below,TSPL_PRICE_CHART_PLANNING.UCDF_SNF_Ded_Rate,TSPL_PRICE_CHART_PLANNING.TSDDCS_Rate,TSPL_PRICE_CHART_PLANNING.Single_Axis_FAT_Per,TSPL_PRICE_CHART_PLANNING.Single_Axis_SNFDed_FAT_Per,TSPL_PRICE_CHART_PLANNING.Single_Axis_SNF_Per,TSPL_PRICE_CHART_PLANNING.Single_Axis_SNFDed_SNF_Per from TSPL_PRICE_CHART_PLANNING where Planning_Code='" + strPlanningCode + "'"
                Dim dtPlanning As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                If dtPlanning IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If FatPer >= clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Min_FAT_Per")) Then
                        Dim dclTempFatPer As Decimal = FatPer
                        If FatPer > clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Max_FAT_Per")) Then
                            If clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Is_FAT_Rate_Zero_After_Max")) > 0 Then
                                dclTempFatPer = 0
                            Else
                                dclTempFatPer = clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Max_FAT_Per"))
                            End If
                        End If
                        FatPer = dclTempFatPer
                    Else
                        FatPer = 0
                        SNFPer = 0
                    End If

                    If SNFPer > 0 Then
                        If SNFPer >= clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Min_SNF_Per")) Then
                            Dim dclTempSNFPer As Decimal = SNFPer
                            If SNFPer > clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Max_SNF_Per")) Then
                                If clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Is_SNF_Rate_Zero_After_Max")) > 0 Then
                                    dclTempSNFPer = 0
                                Else
                                    dclTempSNFPer = clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Max_SNF_Per"))
                                End If
                            End If
                            SNFPer = dclTempSNFPer
                        Else
                            FatPer = 0
                            SNFPer = 0
                        End If
                    End If
                End If
                Dim dblMilkRateFAT As Decimal = 0
                Dim dblMilkRateSNF As Decimal = 0
                Dim FATKg As Decimal = (Qty * FatPer) / 100
                Dim SNFKg As Decimal = (Qty * SNFPer) / 100
                Dim ApplyCowPrice As Boolean = False
                If Not objCommonVar.SepratePriceChartForCow Then
                    If FatPer >= clsCommon.myCdbl(dtPlanning.Rows(0)("Single_Axis_FAT_Per")) AndAlso FatPer <= clsCommon.myCdbl(dtPlanning.Rows(0)("Single_Axis_SNFDed_FAT_Per")) Then
                        If SNFPer >= clsCommon.myCdbl(dtPlanning.Rows(0)("Single_Axis_SNF_Per")) AndAlso SNFPer <= clsCommon.myCdbl(dtPlanning.Rows(0)("Single_Axis_SNFDed_SNF_Per")) Then
                            ApplyCowPrice = True
                            dblMilkRateFAT = clsCommon.myCDecimal(dtPlanning.Rows(0)("TSDDCS_Rate"))
                            dblMilkRateSNF = clsCommon.myCDecimal(dtPlanning.Rows(0)("UCDF_SNF_Ded_Rate"))
                        End If
                    End If
                End If

                If Not ApplyCowPrice Then
                    qry = "select SNo, FAT_From,FAT_To,SNF_From,SNF_To,Rate_Per from TSPL_PRICE_CHART_PLANNING_TSDDCF where Planning_Code='" + strPlanningCode + "' order by SNo"
                    Dim dtSlab As DataTable = clsDBFuncationality.GetDataTable(qry, tran)

                    If dtSlab IsNot Nothing AndAlso dtSlab.Rows.Count > 0 Then
                        If ApplyMixMilk Then
                            For Each drSlab As DataRow In dtSlab.Rows
                                If clsCommon.myCDecimal(drSlab("SNF_From")) > 0 AndAlso clsCommon.myCDecimal(drSlab("SNF_To")) > 0 Then
                                    If clsCommon.myCDecimal(drSlab("SNF_From")) <= SNFPer AndAlso clsCommon.myCDecimal(drSlab("SNF_To")) >= SNFPer Then
                                        dblMilkRateFAT = clsCommon.myCDecimal(drSlab("Rate_Per"))
                                        Exit For
                                    End If
                                End If
                            Next
                        Else
                            For Each drSlab As DataRow In dtSlab.Rows
                                If clsCommon.myCDecimal(drSlab("FAT_From")) > 0 AndAlso clsCommon.myCDecimal(drSlab("FAT_To")) > 0 Then
                                    If clsCommon.myCDecimal(drSlab("FAT_From")) <= FatPer AndAlso clsCommon.myCDecimal(drSlab("FAT_To")) >= FatPer Then
                                        dblMilkRateFAT = clsCommon.myCDecimal(drSlab("Rate_Per"))
                                        Exit For
                                    End If
                                End If
                            Next
                            For Each drSlab As DataRow In dtSlab.Rows
                                If clsCommon.myCDecimal(drSlab("SNF_From")) > 0 AndAlso clsCommon.myCDecimal(drSlab("SNF_To")) > 0 Then
                                    If clsCommon.myCDecimal(drSlab("SNF_From")) <= SNFPer AndAlso clsCommon.myCDecimal(drSlab("SNF_To")) >= SNFPer Then
                                        dblMilkRateSNF = clsCommon.myCDecimal(drSlab("Rate_Per"))
                                        Exit For
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If

                Dim FATPart As Decimal = dblMilkRateFAT * FATKg
                Dim SNFPart As Decimal = dblMilkRateSNF * SNFKg
                RetVal = ((FATPart + SNFPart) / Qty)
                If objCommonVar.MilkRateRoundOffType = 1 Then
                    RetVal = Math.Round(RetVal, 2, MidpointRounding.AwayFromZero)
                Else
                    RetVal = Math.Round(RetVal, 2, MidpointRounding.ToEven)
                End If
                'Throw New Exception("Milk Price not exists")
            Else
                Throw New Exception("Milk Price not exists")
            End If
        End If
        Return RetVal
    End Function
    Public Shared Function getRateAndPriceCodeFromUploaderShiftWise(ByVal qty As Decimal, ByRef PriceCode As String, ByVal FatPer As Double, ByVal SNFPer As Double, ByVal MccCode As String, ByVal vlcCode As String, ByVal Shift As String, ByVal Doc_Date As Date, ByVal tran As SqlTransaction, ByVal strMilkType As String) As Double
        Return getRateAndPriceCodeFromUploaderShiftWise(qty, PriceCode, FatPer, SNFPer, MccCode, vlcCode, Shift, Doc_Date, tran, strMilkType, 0, 0)
    End Function
    Public Shared Function getRateAndPriceCodeFromUploaderShiftWise(ByVal qty As Decimal, ByRef PriceCode As String, ByVal FatPer As Double, ByVal SNFPer As Double, ByVal MccCode As String, ByVal vlcCode As String, ByVal Shift As String, ByVal Doc_Date As Date, ByVal tran As SqlTransaction, ByVal strMilkType As String, ByRef dclRefQATRate As Decimal, ByRef dclRefNegativeRate As Decimal) As Double
        Dim Rate As Decimal = 0
        PriceCode = ""
        dclRefNegativeRate = 0
        Try
            If Not clsVendorMaster.IsVLCDripSaver(vlcCode, tran) Then
                If objCommonVar.PricePlan = 6 Then
                    Rate = GetRateCalculatedRAJ(PriceCode, Doc_Date, Shift, vlcCode, strMilkType, qty, FatPer, SNFPer, tran)
                ElseIf objCommonVar.PricePlan = 7 Then
                    Rate = GetRateCalculatedJPR(PriceCode, Doc_Date, Shift, vlcCode, strMilkType, qty, FatPer, SNFPer, tran, dclRefQATRate, dclRefNegativeRate)
                Else
                    Dim settMilkCollectionPickBulkRoute As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionPickBulkRoute, clsFixedParameterCode.MilkCollectionPickBulkRoute, tran)) = 1)
                    Dim qry As String = "select top 1 TSPL_FAT_SNF_UPLOADER_MASTER.Rate,TSPL_FAT_SNF_UPLOADER_MASTER.Code,TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code ,TSPL_VLC_MASTER_HEAD.Apply_Price_Chart_Uploader,TSPL_FAT_SNF_UPLOADER_MASTER.Planning_Code from TSPL_FAT_SNF_UPLOADER_MASTER " + Environment.NewLine
                    If settMilkCollectionPickBulkRoute Then
                        qry += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code='" + vlcCode + "' "
                    Else
                        qry += " inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code and TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" & MccCode & "'
inner Join TSPL_FAT_SNF_UPLOADER_VLC on TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_VLC.Code  And TSPL_FAT_SNF_UPLOADER_VLC.VLC_Code='" & vlcCode & "'
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_FAT_SNF_UPLOADER_VLC.VLC_Code "
                    End If
                    qry += " left outer join  (select Code,max(FAT) as MaxFAT ,max(SNF) as MaxSNF from TSPL_FAT_SNF_UPLOADER_MASTER group by Code) as TabMAXFATSNF on TabMAXFATSNF.Code=TSPL_FAT_SNF_UPLOADER_MASTER.Code
                where  TSPL_FAT_SNF_UPLOADER_MASTER.Posted='1'"
                    If objCommonVar.DisplayTypeInMilkReceipt Then
                        qry += " and TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type='" + strMilkType + "' "
                    ElseIf objCommonVar.SepratePriceChartForCow AndAlso clsCommon.CompairString(strMilkType, "C") = CompairStringResult.Equal Then
                        qry += " and TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type='C' "
                    Else
                        qry += " and TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type='M' "
                    End If
                    qry += "  and TSPL_FAT_SNF_UPLOADER_MASTER.fat= (case when " & FatPer & ">TabMAXFATSNF.MaxFAT then TabMAXFATSNF.MaxFAT else " & FatPer & " end ) 
 and  TSPL_FAT_SNF_UPLOADER_MASTER.SNF=(case when " & GetSNFForPrice(SNFPer) & ">TabMAXFATSNF.MaxSNF then TabMAXFATSNF.MaxSNF else " & GetSNFForPrice(SNFPer) & " end ) 
 and (TSPL_FAT_SNF_UPLOADER_MASTER.Date< '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' or (TSPL_FAT_SNF_UPLOADER_MASTER.Date= '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' and Price_code_shift>='" & Shift & "')) and ( TSPL_FAT_SNF_UPLOADER_MASTER.In_Active_From is null or TSPL_FAT_SNF_UPLOADER_MASTER.In_Active_From > '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' ) " + Environment.NewLine +
                    " order by date desc ,TSPL_FAT_SNF_UPLOADER_MASTER.code desc"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        PriceCode = clsCommon.myCstr(dt.Rows(0)("Code"))
                        If objCommonVar.PricePlan = 4 Then
                            If clsCommon.myCdbl(dt.Rows(0)("Apply_Price_Chart_Uploader")) > 0 Then
                                Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
                            ElseIf clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Planning_Code"))) > 0 Then
                                Rate = GetRateCalculated(clsCommon.myCstr(dt.Rows(0)("Planning_Code")), clsCommon.myCstr(dt.Rows(0)("Price_Code")), qty, FatPer, GetSNFForPrice(SNFPer), tran)
                            Else
                                Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
                            End If
                        ElseIf objCommonVar.PricePlan = 5 Then
                            If clsCommon.myCdbl(dt.Rows(0)("Apply_Price_Chart_Uploader")) > 0 Then
                                Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
                            ElseIf clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Planning_Code"))) > 0 Then
                                Rate = GetRateCalculatedUCDF(clsCommon.myCstr(dt.Rows(0)("Planning_Code")), clsCommon.myCstr(dt.Rows(0)("Price_Code")), qty, FatPer, SNFPer, tran)
                            Else
                                Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
                            End If
                        Else
                            Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Rate
    End Function

    Public Shared Function GetRateCalculatedJPR(ByRef strPlanningCode As String, ByVal Doc_Date As Date, ByVal Shift As String, ByVal VLC_Code As String, ByVal strMilkType As String, ByVal Qty As Decimal, ByVal dblFATPer As Double, ByVal dblSNFPer As Double, ByVal tran As SqlTransaction, ByRef dclRefQATRate As Decimal, ByRef dclRefNegativeRate As Decimal) As Decimal
        'For Jaipur and Alwar
        If Qty <= 0 Then
            Return 0
        End If
        dclRefQATRate = 0
        dclRefNegativeRate = 0
        strPlanningCode = ""
        Dim strPriceMasterCode As String = Nothing
        Dim dclReturnMilkValue As Decimal = 0
        Dim dclTSDDCSRate As Decimal = 0
        Dim qry As String = ""
        Dim dt As DataTable = Nothing
        qry = clsEkoPro.GetLatestPlaningQry(Doc_Date, Shift, "M")
        dt = clsDBFuncationality.GetDataTable(qry, tran)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            strPlanningCode = clsCommon.myCstr(dt.Rows(0)("Planning_Code"))
            strPriceMasterCode = clsCommon.myCstr(dt.Rows(0)("Price_Chart_Code"))
            dclTSDDCSRate = clsCommon.myCstr(dt.Rows(0)("TSDDCS_Rate"))
        End If
        If clsCommon.myLen(strPlanningCode) > 0 Then
            Dim dclRate As Decimal = 0
            Dim dclDedPer As Decimal = 0
            Dim dclTSDDCSRateSlab As Decimal = 0
            qry = "select SNo, FAT_From,FAT_To,Apply_FAT,SNF_From,SNF_To,Apply_SNF,Rate_Per,Fixed_Rate,Below_SNF_Rate,Deduction_Per from TSPL_PRICE_CHART_PLANNING_TSDDCF where Planning_Code='" + strPlanningCode + "' order by SNo"
            Dim dtSlab As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            If dtSlab IsNot Nothing AndAlso dtSlab.Rows.Count > 0 Then
                For Each drSlab As DataRow In dtSlab.Rows
                    If dblFATPer >= clsCommon.myCDecimal(drSlab("FAT_From")) AndAlso dblFATPer <= clsCommon.myCDecimal(drSlab("FAT_To")) Then
                        If dclTSDDCSRate > 0 Then
                            If Not (dblSNFPer >= clsCommon.myCDecimal(drSlab("SNF_From")) AndAlso dblSNFPer <= clsCommon.myCDecimal(drSlab("SNF_To"))) Then
                                Continue For
                            End If
                        End If
                        If clsCommon.myCDecimal(drSlab("Apply_FAT")) > 0 Then
                            If dblFATPer > clsCommon.myCDecimal(drSlab("Apply_FAT")) Then
                                dblFATPer = clsCommon.myCDecimal(drSlab("Apply_FAT"))
                            End If
                        End If
                        If clsCommon.myCDecimal(drSlab("Apply_SNF")) > 0 Then
                            If dblSNFPer > clsCommon.myCDecimal(drSlab("Apply_SNF")) Then
                                dblSNFPer = clsCommon.myCDecimal(drSlab("Apply_SNF"))
                            End If
                        End If
                        If dblSNFPer < clsCommon.myCDecimal(drSlab("SNF_From")) Then
                            dclReturnMilkValue = clsCommon.myCDecimal(drSlab("Below_SNF_Rate"))
                        Else
                            dclRate = clsCommon.myCDecimal(drSlab("Rate_Per"))
                            dclTSDDCSRateSlab = clsCommon.myCDecimal(drSlab("Rate_Per"))
                            dclReturnMilkValue = ((dclRate * dblFATPer) / 100)
                            dclReturnMilkValue += clsCommon.myCDecimal(drSlab("Fixed_Rate"))

                            Dim arr As Dictionary(Of Decimal, Decimal) = clsPriceChartPlanningTSDDCFSNFDed.GetData(strPlanningCode, clsCommon.myCDecimal(drSlab("SNo")), tran)
                            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                                If arr.ContainsKey(dblSNFPer) Then
                                    dclReturnMilkValue += arr.Item(dblSNFPer)
                                End If
                            End If
                            dclDedPer = clsCommon.myCDecimal(drSlab("Deduction_Per"))
                        End If
                        Exit For
                    End If
                Next
                
                If dclTSDDCSRate > 0 AndAlso dclTSDDCSRateSlab > 0 Then
                    dclRefQATRate = clsCommon.myRoundOFF(((dclTSDDCSRate - dclTSDDCSRateSlab) * dblFATPer / 100), 3, 4)
                End If
                If dclRefQATRate < 0 Then
                    dclRefQATRate = 0
                End If
            End If

            dclReturnMilkValue = (dclReturnMilkValue * ((100 - dclDedPer) / 100))
            'If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "ALW") = CompairStringResult.Equal Then ''By Kislay on 30/05/2023
            '    dclReturnMilkValue = clsCommon.myRoundOFF(dclReturnMilkValue, 2, 9)
            'Else
            '    dclReturnMilkValue = clsCommon.myRoundOFF(dclReturnMilkValue, 2, 4)
            'End If
            dclReturnMilkValue = clsCommon.myRoundOFF(dclReturnMilkValue, 2, 4)
            If dclReturnMilkValue < 0 Then
                dclRefNegativeRate = -1 * dclReturnMilkValue
                dclReturnMilkValue = 0
            End If
        Else
            Throw New Exception("Milk Price not exists")
        End If
        Return dclReturnMilkValue
    End Function

    Public Shared Function GetRateCalculated(ByVal strPlanningCode As String, ByVal strPriceMasterCode As String, ByVal Qty As Decimal, ByVal FatPer As Double, ByVal SNFPer As Double, ByVal tran As SqlTransaction) As Decimal
        Return GetRateCalculated(strPlanningCode, strPriceMasterCode, Qty, FatPer, SNFPer, tran, -1)
    End Function

    Public Shared Function GetRateCalculated(ByVal strPlanningCode As String, ByVal strPriceMasterCode As String, ByVal Qty As Decimal, ByVal FatPer As Double, ByVal SNFPer As Double, ByVal tran As SqlTransaction, ByVal EMPRate As Decimal) As Decimal
        Return GetRateCalculated(strPlanningCode, strPriceMasterCode, Qty, FatPer, SNFPer, tran, EMPRate, 0)
    End Function

    Public Shared Function GetRateCalculated(ByVal strPlanningCode As String, ByVal strPriceMasterCode As String, ByVal Qty As Decimal, ByVal FatPer As Double, ByVal SNFPer As Double, ByVal tran As SqlTransaction, ByVal EMPRate As Decimal, ByRef FATRatio As Decimal) As Decimal
        If Qty <= 0 Then
            Return 0
        End If
        Dim qry As String = "select Price_code, Ratio,SNF_Ratio,FAT_Pers,SNF_Pers,Milk_Rate from TSPL_MILK_PRICE_MASTER where Price_code='" + strPriceMasterCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("Milk Price Master Code [" + strPriceMasterCode + "] not exists")
        End If
        If clsCommon.myLen(strPlanningCode) > 0 AndAlso EMPRate < 0 Then
            qry = "select GK_Min_FAT_Per,GK_Max_FAT_Per,GK_Min_SNF_Per,GK_Max_SNF_Per,GK_Is_FAT_Rate_Zero_After_Max,GK_Is_SNF_Rate_Zero_After_Max from TSPL_PRICE_CHART_PLANNING where Planning_Code='" + strPlanningCode + "'"
            Dim dtPlanning As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            If dtPlanning IsNot Nothing AndAlso dtPlanning.Rows.Count > 0 Then
                If FatPer >= clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Min_FAT_Per")) Then
                    Dim dclTempFatPer As Decimal = FatPer
                    If FatPer > clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Max_FAT_Per")) Then
                        If clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Is_FAT_Rate_Zero_After_Max")) > 0 Then
                            dclTempFatPer = 0
                        Else
                            dclTempFatPer = clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Max_FAT_Per"))
                        End If
                    End If
                    FatPer = dclTempFatPer
                Else
                    FatPer = 0
                    SNFPer = 0
                End If

                If SNFPer > 0 Then
                    If SNFPer >= clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Min_SNF_Per")) Then
                        Dim dclTempSNFPer As Decimal = SNFPer
                        If SNFPer > clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Max_SNF_Per")) Then
                            If clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Is_SNF_Rate_Zero_After_Max")) > 0 Then
                                dclTempSNFPer = 0
                            Else
                                dclTempSNFPer = clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Max_SNF_Per"))
                            End If
                        End If
                        SNFPer = dclTempSNFPer
                    Else
                        FatPer = 0
                        SNFPer = 0
                    End If
                End If
            End If
        End If
        Dim FATKg As Decimal = clsERPFuncationality.myFloor((Qty * FatPer) / 100, 2)
        Dim SNFKg As Decimal = clsERPFuncationality.myFloor((Qty * SNFPer) / 100, 2)
        Dim dblMilkRate As Decimal = clsCommon.myCdbl(dt.Rows(0)("Milk_Rate"))
        '------------------For FAT Ratio Only
        Dim FATPart As Decimal = clsERPFuncationality.myFloor((dblMilkRate * clsCommon.myCdbl(dt.Rows(0)("Ratio"))) / clsCommon.myCdbl(dt.Rows(0)("FAT_Pers")), 2)
        Dim SNFPart As Decimal = clsERPFuncationality.myFloor((dblMilkRate * clsCommon.myCdbl(dt.Rows(0)("SNF_Ratio"))) / clsCommon.myCdbl(dt.Rows(0)("SNF_Pers")), 2)
        FATPart = clsERPFuncationality.myFloor(FATPart * FATKg, 0)
        SNFPart = clsERPFuncationality.myFloor(SNFPart * SNFKg, 0)
        If (FATPart + SNFPart) > 0 Then
            FATRatio = (FATPart / (FATPart + SNFPart))
        End If
        '------------------End of For FAT Ratio Only
        If EMPRate >= 0 Then
            dblMilkRate = EMPRate
        End If
        FATPart = clsERPFuncationality.myFloor((dblMilkRate * clsCommon.myCdbl(dt.Rows(0)("Ratio"))) / clsCommon.myCdbl(dt.Rows(0)("FAT_Pers")), 2)
        SNFPart = clsERPFuncationality.myFloor((dblMilkRate * clsCommon.myCdbl(dt.Rows(0)("SNF_Ratio"))) / clsCommon.myCdbl(dt.Rows(0)("SNF_Pers")), 2)
        If (FATPart + SNFPart) > 0 Then
            FATRatio = (FATRatio / (FATPart + SNFPart))
        End If
        FATPart = clsERPFuncationality.myFloor(FATPart * FATKg, 0)
        SNFPart = clsERPFuncationality.myFloor(SNFPart * SNFKg, 0)
        Dim RetVal As Decimal = 0
        If EMPRate >= 0 Then
            RetVal = Math.Round((FATPart + SNFPart), 10, MidpointRounding.ToEven)
        Else
            RetVal = Math.Round((FATPart + SNFPart) / Qty, 10, MidpointRounding.ToEven)
        End If
        Return RetVal
    End Function


    Public Shared Function GetRateCalculatedUCDF(ByVal strPlanningCode As String, ByVal strPriceMasterCode As String, ByVal Qty As Decimal, ByVal FatPer As Double, ByVal SNFPer As Double, ByVal tran As SqlTransaction) As Decimal
        If Qty <= 0 Then
            Return 0
        End If
        Dim RetVal As Decimal = 0
        Dim qry As String = "select Price_code, Ratio,SNF_Ratio,FAT_Pers,SNF_Pers,Milk_Rate from TSPL_MILK_PRICE_MASTER where Price_code='" + strPriceMasterCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("Milk Price Master Code [" + strPriceMasterCode + "] not exists")
        End If
        If clsCommon.myLen(strPlanningCode) > 0 Then
            qry = "select GK_Min_FAT_Per,GK_Max_FAT_Per,GK_Min_SNF_Per,GK_Max_SNF_Per,GK_Is_FAT_Rate_Zero_After_Max,GK_Is_SNF_Rate_Zero_After_Max,TSPL_PRICE_CHART_PLANNING.UCDF_SNF_Ded_Below,TSPL_PRICE_CHART_PLANNING.UCDF_SNF_Ded_Rate from TSPL_PRICE_CHART_PLANNING where Planning_Code='" + strPlanningCode + "'"
            Dim dtPlanning As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            If dtPlanning IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If FatPer >= clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Min_FAT_Per")) Then
                    Dim dclTempFatPer As Decimal = FatPer
                    If FatPer > clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Max_FAT_Per")) Then
                        If clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Is_FAT_Rate_Zero_After_Max")) > 0 Then
                            dclTempFatPer = 0
                        Else
                            dclTempFatPer = clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Max_FAT_Per"))
                        End If
                    End If
                    FatPer = dclTempFatPer
                Else
                    FatPer = 0
                    SNFPer = 0
                End If

                If SNFPer > 0 Then
                    If SNFPer >= clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Min_SNF_Per")) Then
                        Dim dclTempSNFPer As Decimal = SNFPer
                        If SNFPer > clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Max_SNF_Per")) Then
                            If clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Is_SNF_Rate_Zero_After_Max")) > 0 Then
                                dclTempSNFPer = 0
                            Else
                                dclTempSNFPer = clsCommon.myCdbl(dtPlanning.Rows(0)("GK_Max_SNF_Per"))
                            End If
                        End If
                        SNFPer = dclTempSNFPer
                    Else
                        FatPer = 0
                        SNFPer = 0
                    End If
                End If
            End If

            Dim FATKg As Decimal = (Qty * FatPer) / 100
            Dim SNFKg As Decimal = (Qty * SNFPer) / 100
            Dim dblMilkRate As Decimal = clsCommon.myCdbl(dt.Rows(0)("Milk_Rate"))

            Dim FATPart As Decimal = (dblMilkRate * clsCommon.myCdbl(dt.Rows(0)("Ratio"))) / clsCommon.myCdbl(dt.Rows(0)("FAT_Pers"))
            Dim SNFPart As Decimal = (dblMilkRate * clsCommon.myCdbl(dt.Rows(0)("SNF_Ratio"))) / clsCommon.myCdbl(dt.Rows(0)("SNF_Pers"))

            FATPart = FATPart * FATKg
            SNFPart = SNFPart * SNFKg
            RetVal = ((FATPart + SNFPart) / Qty)

            If SNFPer < clsCommon.myCdbl(dtPlanning.Rows(0)("UCDF_SNF_Ded_Below")) Then
                RetVal = (RetVal - (RetVal * clsCommon.myCdbl(dtPlanning.Rows(0)("UCDF_SNF_Ded_Rate")) / 100))
            End If
            RetVal = Math.Round(RetVal, 10, MidpointRounding.ToEven)
        End If

        Return RetVal
    End Function



    Public Shared Function GetRateCalculatedExact(ByVal strPriceMasterCode As String, ByVal Qty As Decimal, ByVal FatPer As Double, ByVal SNFPer As Double, ByVal tran As SqlTransaction, ByVal EMPRate As Decimal) As Decimal
        If Qty <= 0 Then
            Return 0
        End If
        Dim qry As String = "select Price_code, Ratio,SNF_Ratio,FAT_Pers,SNF_Pers,Milk_Rate from TSPL_MILK_PRICE_MASTER where Price_code='" + strPriceMasterCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("Milk Price Master Code [" + strPriceMasterCode + "] not exists")
        End If
        Dim FATKg As Decimal = clsERPFuncationality.myFloor((Qty * FatPer) / 100, 2)
        Dim SNFKg As Decimal = clsERPFuncationality.myFloor((Qty * SNFPer) / 100, 2)
        Dim dblMilkRate As Decimal = clsCommon.myCdbl(dt.Rows(0)("Milk_Rate"))


        Dim FATPart As Decimal = clsERPFuncationality.myFloor(((dblMilkRate + EMPRate) * clsCommon.myCdbl(dt.Rows(0)("Ratio"))) / clsCommon.myCdbl(dt.Rows(0)("FAT_Pers")), 2)
        Dim SNFPart As Decimal = clsERPFuncationality.myFloor(((dblMilkRate + EMPRate) * clsCommon.myCdbl(dt.Rows(0)("SNF_Ratio"))) / clsCommon.myCdbl(dt.Rows(0)("SNF_Pers")), 2)

        FATPart = clsERPFuncationality.myFloor(FATPart * FATKg, 0)
        SNFPart = clsERPFuncationality.myFloor(SNFPart * SNFKg, 0)
        Dim RetVal As Decimal = 0

        RetVal = Math.Round((FATPart + SNFPart), 10, MidpointRounding.ToEven)

        Return RetVal
    End Function

    Public Shared Function getPriceCodeFromUploaderShiftwise(ByVal FatPer As Double, ByVal SNF As Double, ByVal MccCode As String, ByVal vlcCode As String, ByVal Shift As String, ByVal Doc_Date As Date) As String
        Return getPriceCodeFromUploaderShiftwise(FatPer, SNF, MccCode, vlcCode, Shift, Doc_Date, Nothing)
    End Function
    Public Shared Function getPriceCodeFromUploaderShiftwise(ByVal FatPer As Double, ByVal SNF As Double, ByVal MccCode As String, ByVal vlcCode As String, ByVal Shift As String, ByVal Doc_Date As Date, ByVal trans As SqlTransaction) As String
        Return getPriceCodeFromUploaderShiftwise(FatPer, SNF, MccCode, vlcCode, Shift, Doc_Date, trans, "M")
    End Function

    Public Shared Function getPriceCodeFromUploaderShiftwise(ByVal FatPer As Double, ByVal SNF As Double, ByVal MccCode As String, ByVal vlcCode As String, ByVal Shift As String, ByVal Doc_Date As Date, ByVal trans As SqlTransaction, ByVal strMilkType As String) As String
        Dim Price_Code As String = ""
        If Not clsVendorMaster.IsVLCDripSaver(vlcCode, trans) Then
            Dim qry As String = "select top 1 TSPL_FAT_SNF_UPLOADER_MASTER.code from TSPL_FAT_SNF_UPLOADER_MASTER inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" & MccCode & "' and " _
           & " TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code inner join TSPL_FAT_SNF_UPLOADER_VLC on VLC_Code='" & vlcCode & "' and " _
           & " TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_VLC.Code where  posted='1' "
            If objCommonVar.DisplayTypeInMilkReceipt Then
                qry += " and TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type='" + strMilkType + "' "
            ElseIf objCommonVar.SepratePriceChartForCow AndAlso clsCommon.CompairString(strMilkType, "C") = CompairStringResult.Equal Then
                qry += " and TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type='C' "
            Else
                qry += " and TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type='M' "
            End If
            qry += "  and  fat=" & FatPer & " and SNF=" & GetSNFForPrice(SNF) & " and (date< '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' or (date= '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' and Price_code_shift>='" & Shift & "')) and ( TSPL_FAT_SNF_UPLOADER_MASTER.In_Active_From is null or TSPL_FAT_SNF_UPLOADER_MASTER.In_Active_From > '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' ) order by date desc ,TSPL_FAT_SNF_UPLOADER_MASTER.code desc"
            Price_Code = clsDBFuncationality.getSingleValue(qry, trans)
        End If
        Return Price_Code
    End Function

    Public Shared Function getPriceCodeDatatableFromUploader(ByVal MccCode As String, ByVal vlcCode As String, ByVal Shift As String, ByVal Doc_Date As Date, ByVal strMilkType As String) As DataTable
        Dim Price_code As String = clsEkoPro.getPriceCodeFromUploaderShiftwise(1, 1, MccCode, vlcCode, Shift, Doc_Date, Nothing, strMilkType)
        Dim Price_Code_Dt As DataTable
        Price_Code_Dt = clsDBFuncationality.GetDataTable("select  TSPL_FAT_SNF_UPLOADER_MASTER.* from TSPL_FAT_SNF_UPLOADER_MASTER where code='" & Price_code & "'")
        Return Price_Code_Dt
    End Function

    Public Shared Function getRateFromUploaderShiftWiseCLR(ByVal FatPer As Double, ByVal CLR As Double, ByVal MccCode As String, ByVal vlcCode As String, ByVal Shift As String, ByVal Doc_Date As Date) As Double
        Return getRateFromUploaderShiftWiseCLR(FatPer, CLR, MccCode, vlcCode, Shift, Doc_Date, Nothing)
    End Function

    Public Shared Function getRateFromUploaderShiftWiseCLR(ByVal FatPer As Double, ByVal CLR As Double, ByVal MccCode As String, ByVal vlcCode As String, ByVal Shift As String, ByVal Doc_Date As Date, ByVal tran As SqlTransaction) As Double
        Return getRateFromUploaderShiftWise(FatPer, CLR, MccCode, vlcCode, Shift, Doc_Date, tran, "M")
    End Function

    Public Shared Function getRateFromUploaderShiftWiseCLR(ByVal FatPer As Double, ByVal CLR As Double, ByVal MccCode As String, ByVal vlcCode As String, ByVal Shift As String, ByVal Doc_Date As Date, ByVal tran As SqlTransaction, ByVal strMilkType As String, ByRef PriceCode As String) As Double
        PriceCode = ""
        Dim Rate As Double = 0
        Try
            CLR = clsERPFuncationality.myDclInZeroPointFive(CLR)
            If Not clsVendorMaster.IsVLCDripSaver(vlcCode, tran) Then
                Dim qry As String = "select top 1 rate,TSPL_FAT_SNF_UPLOADER_MASTER.Code from TSPL_FAT_SNF_UPLOADER_MASTER "
                'inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" & MccCode & "' and  TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code 
                'inner join TSPL_FAT_SNF_UPLOADER_VLC on VLC_Code='" & vlcCode & "' and  TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_VLC.Code 

                qry += " where  posted='1'"
                If objCommonVar.DisplayTypeInMilkReceipt Then
                    qry += " and TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type='" + strMilkType + "' "
                ElseIf objCommonVar.SepratePriceChartForCow AndAlso clsCommon.CompairString(strMilkType, "C") = CompairStringResult.Equal Then
                    qry += " and TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type='C' "
                Else
                    qry += " and TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type='M' "
                End If
                qry += "  and  fat=" & FatPer & " and CLR=" & CLR & " and (date< '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' or (date= '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' and Price_code_shift>='" & Shift & "')) and ( TSPL_FAT_SNF_UPLOADER_MASTER.In_Active_From is null or TSPL_FAT_SNF_UPLOADER_MASTER.In_Active_From > '" & clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") & "' ) order by date desc ,TSPL_FAT_SNF_UPLOADER_MASTER.code desc"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Rate = clsCommon.myCdbl(dt.Rows(0)("rate"))
                    PriceCode = clsCommon.myCstr(dt.Rows(0)("Code"))
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Rate
    End Function

    ''TEC/21/05/18-000242 by balwinder on 25/05/2018 
    Public Shared Function GetRateMccSale(ByVal mccCode As String, ByVal Itemcode As String, ByVal Unitcode As String, ByVal Effctv_date As Date)
        Dim tranDate As String = clsCommon.GetPrintDate(Effctv_date, "dd/MMM/yyyy")
        Dim Rate As Double = 0
        Dim qry As String = "select top 1 Price from TSPL_MCC_RATE_UPLOADER_master inner join TSPL_MCC_RATE_UPLOADER_MCC on " _
              & " TSPL_MCC_RATE_UPLOADER_MCC.MCC_Code='" & mccCode & "' and   TSPL_MCC_RATE_UPLOADER_master.Code=TSPL_MCC_RATE_UPLOADER_MCC.Code " _
              & " left join TSPL_MCC_RATE_UPLOADER_Detail on TSPL_MCC_RATE_UPLOADER_Detail.Code=TSPL_MCC_RATE_UPLOADER_MASTER.code where Item_Code='" & Itemcode & "' " _
              & " and TSPL_MCC_RATE_UPLOADER_Detail.RATE_UOM='" & Unitcode & "' and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Date,103) <=convert(date,'" & tranDate & "',103) and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Effective_date,103) >=convert(date,'" & tranDate & "',103) order by date desc ,TSPL_MCC_RATE_UPLOADER_master.code desc "
        Rate = clsDBFuncationality.getSingleValue(qry)
        If Rate <= 0 Then
            qry = "select top 1 TSPL_ITEM_UOM_DETAIL.Item_Code,Price,TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_MCC_RATE_UPLOADER_master inner join TSPL_MCC_RATE_UPLOADER_MCC on " _
             & " TSPL_MCC_RATE_UPLOADER_MCC.MCC_Code='" & mccCode & "' and   TSPL_MCC_RATE_UPLOADER_master.Code=TSPL_MCC_RATE_UPLOADER_MCC.Code " _
             & " left join TSPL_MCC_RATE_UPLOADER_Detail on TSPL_MCC_RATE_UPLOADER_Detail.Code=TSPL_MCC_RATE_UPLOADER_MASTER.code inner join TSPL_ITEM_UOM_DETAIL on " _
             & " TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_MCC_RATE_UPLOADER_Detail.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_MCC_RATE_UPLOADER_Detail.RATE_UOM where TSPL_MCC_RATE_UPLOADER_Detail.Item_Code='" & Itemcode & "' " _
             & " and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Date,103) <=convert(date,'" & tranDate & "',103) and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Effective_date,103) >=convert(date,'" & tranDate & "',103) order by date desc ,TSPL_MCC_RATE_UPLOADER_master.code desc "
            Dim Dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If Dt.Rows.Count > 0 Then
                Dim Conv_Fac As Double = clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" & Itemcode & "'  and Uom_Code='" & Unitcode & "' ")
                Rate = Conv_Fac * clsCommon.myCdbl(Dt.Rows(0)("Price")) / IIf(clsCommon.myCdbl(Dt.Rows(0)("Conversion_Factor")) > 0, clsCommon.myCdbl(Dt.Rows(0)("Conversion_Factor")), 1)
                Return Rate
            Else
                Return Rate
            End If
        End If
        Return Rate
    End Function

    Public Shared Function GetPriceMccSale(ByVal mccCode As String, ByVal Itemcode As String, ByVal Unitcode As String, ByVal Effctv_date As Date)
        Dim Rate As Double = 0
        Dim PriceCode As String = 0
        Rate = clsDBFuncationality.getSingleValue("select top 1 Price from TSPL_MCC_RATE_UPLOADER_master inner join TSPL_MCC_RATE_UPLOADER_MCC on " _
              & " TSPL_MCC_RATE_UPLOADER_MCC.MCC_Code='" & mccCode & "' and   TSPL_MCC_RATE_UPLOADER_master.Code=TSPL_MCC_RATE_UPLOADER_MCC.Code " _
              & " left join TSPL_MCC_RATE_UPLOADER_Detail on TSPL_MCC_RATE_UPLOADER_Detail.Code=TSPL_MCC_RATE_UPLOADER_MASTER.code where Item_Code='" & Itemcode & "' " _
              & " and TSPL_MCC_RATE_UPLOADER_Detail.RATE_UOM='" & Unitcode & "' and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Effective_date,103) >=convert(date,'" & Effctv_date & "',103) order by date desc ,TSPL_MCC_RATE_UPLOADER_master.code desc ")
        PriceCode = clsDBFuncationality.getSingleValue("select top 1 TSPL_MCC_RATE_UPLOADER_master.code from TSPL_MCC_RATE_UPLOADER_master inner join TSPL_MCC_RATE_UPLOADER_MCC on " _
              & " TSPL_MCC_RATE_UPLOADER_MCC.MCC_Code='" & mccCode & "' and   TSPL_MCC_RATE_UPLOADER_master.Code=TSPL_MCC_RATE_UPLOADER_MCC.Code " _
              & " left join TSPL_MCC_RATE_UPLOADER_Detail on TSPL_MCC_RATE_UPLOADER_Detail.Code=TSPL_MCC_RATE_UPLOADER_MASTER.code where Item_Code='" & Itemcode & "' " _
              & " and TSPL_MCC_RATE_UPLOADER_Detail.RATE_UOM='" & Unitcode & "' and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Effective_date,103) >=convert(date,'" & Effctv_date & "',103) order by date desc ,TSPL_MCC_RATE_UPLOADER_master.code desc ")
        If Rate <= 0 Then
            Dim Dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MCC_RATE_UPLOADER_MCC.code, TSPL_ITEM_UOM_DETAIL.Item_Code,Price,TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_MCC_RATE_UPLOADER_master inner join TSPL_MCC_RATE_UPLOADER_MCC on " _
             & " TSPL_MCC_RATE_UPLOADER_MCC.MCC_Code='" & mccCode & "' and   TSPL_MCC_RATE_UPLOADER_master.Code=TSPL_MCC_RATE_UPLOADER_MCC.Code " _
             & " left join TSPL_MCC_RATE_UPLOADER_Detail on TSPL_MCC_RATE_UPLOADER_Detail.Code=TSPL_MCC_RATE_UPLOADER_MASTER.code inner join TSPL_ITEM_UOM_DETAIL on " _
             & " TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_MCC_RATE_UPLOADER_Detail.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_MCC_RATE_UPLOADER_Detail.RATE_UOM where TSPL_MCC_RATE_UPLOADER_Detail.Item_Code='" & Itemcode & "' " _
             & " and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Effective_date,103) >=convert(date,'" & Effctv_date & "',103) order by date desc ,TSPL_MCC_RATE_UPLOADER_master.code desc ")
            If Dt.Rows.Count > 0 Then
                Dim Conv_Fac As Double = clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" & Itemcode & "'  and Uom_Code='" & Unitcode & "' ")
                Rate = Conv_Fac * clsCommon.myCdbl(Dt.Rows(0)("Price")) / IIf(clsCommon.myCdbl(Dt.Rows(0)("Conversion_Factor")) > 0, clsCommon.myCdbl(Dt.Rows(0)("Conversion_Factor")), 1)
                PriceCode = clsCommon.myCdbl(Dt.Rows(0)("code"))
                Return PriceCode
            Else
                Return PriceCode
            End If
        End If
        Return PriceCode
    End Function

    Public Function getData(ByVal comPort As String, ByVal brate As String) As clsEkoPro
        Dim Obj As clsPortSetting = clsPortSetting.getData(0)
        If Obj IsNot Nothing Then
            Return getData(comPort, brate, Obj.parity, Obj.data_bits, Obj.stop_bits)
        Else
            clsCommon.MyMessageBoxShow("Default Port Settings is Not Saved  For Your Machine to connect to Eko Pro Machine." & Environment.NewLine & "  Please Do it using Port setting Screen or Contact to administrator")
        End If
        Return Nothing
    End Function

    Public Function getData(ByVal comPort As String, ByVal brate As String, ByVal parity As String) As clsEkoPro
        Dim Obj As clsPortSetting = clsPortSetting.getData(0)
        If Obj IsNot Nothing Then
            Return getData(comPort, brate, parity, Obj.data_bits, Obj.stop_bits)
        Else
            clsCommon.MyMessageBoxShow("Default Port Settings is Not Saved  For Your Machine to connect to Eko Pro Machine." & Environment.NewLine & "  Please Do it using Port setting Screen or Contact to administrator")
        End If
        Return Nothing
    End Function

    Public Function getData(ByVal comPort As String, ByVal brate As String, ByVal parity As String, ByVal dbits As String) As clsEkoPro
        Dim Obj As clsPortSetting = clsPortSetting.getData(0)
        If Obj IsNot Nothing Then
            Return getData(comPort, brate, parity, dbits, Obj.stop_bits)
        Else
            clsCommon.MyMessageBoxShow("Default Port Settings is Not Saved  For Your Machine to connect to Eko Pro Machine." & Environment.NewLine & "  Please Do it using Port setting Screen or Contact to administrator")
        End If
        Return Nothing
    End Function


    Public Shared Function GetSNFForPrice(ByVal SNF As Double) As Decimal
        If objCommonVar.MilkProcurementSNF2DecimalPlaces Then
            SNF = Math.Round(SNF, 1, MidpointRounding.AwayFromZero)
        End If
        Return SNF
    End Function
     
End Class
Public Class MyMath

    Private Delegate Function RoundingFunction(ByVal value As Double) As Double

    Public Enum RoundingDirection
        Up
        Down
    End Enum

    Public Shared Function RoundUp(ByVal value As Double, ByVal precision As Integer) As Double
        Return Round(value, precision, RoundingDirection.Up)
    End Function

    Public Shared Function RoundDown(ByVal value As Double, ByVal precision As Integer) _
 As Double
        Try
            Return Round(value, precision, RoundingDirection.Down)
        Catch ex As Exception
            Return 0
        End Try

    End Function

    Private Shared Function Round(ByVal value As Decimal, ByVal precision As Integer,
 ByVal roundingDirection As RoundingDirection) As Decimal
        Dim roundingFunction As RoundingFunction
        If roundingDirection = MyMath.RoundingDirection.Up Then
            roundingFunction = AddressOf Math.Ceiling
        Else
            roundingFunction = AddressOf Math.Floor
        End If
        value = value * Math.Pow(10, precision)
        value = roundingFunction(value)
        Return value * Math.Pow(10, -1 * precision)
    End Function


End Class

Public Class clsMccMasterDetailReport
    Public Shared Function getQueryMccMasterDetailVSP(ByVal ShowActiveStatus As Boolean) As String
        'TSPL_MILK_Shift_End_Route_DETAIL

        Dim qry As String = "select tspl_vlc_master_head.Route_Code,TSPL_MCC_MASTER.MCC_CODE,isnull(TSPL_MCC_MASTER.MCC_NAME,'') AS [Center]
                        ,tspl_vlc_master_head.VLC_Code_VLC_Uploader as [VLC Uploader Code]
                        ,tspl_vlc_master_head.VLC_Code as [VLC Code],tspl_vlc_master_head.VLC_Name as [VLC Name],tspl_vlc_master_head.VSP_Code as [VSP Code]
                        ,TSPL_VENDOR_MASTER.VENDOR_Name as [VSP Name],isnull(TSPL_VENDOR_MASTER.Care_Of,'') as [Care Of]
                        ,convert(varchar,TSPL_VENDOR_MASTER.Created_Date,103) as [Registration Date]
                        ,ISNULL(TSPL_VENDOR_MASTER.ADD1,'')+ISNULL(TSPL_VENDOR_MASTER.ADD2,'')+ISNULL(TSPL_VENDOR_MASTER.ADD3,'') AS [Address]
                        ,isnull(TSPL_VENDOR_MASTER.Aadhar_No,'') as [Aadhar No],case when Attachment.AttachmentCount>0 then 'Y' else 'N' end as [File]
                        , isnull(TSPL_VENDOR_MASTER.SecChequeNoLac1,'') as [Security Cheque 100000],isnull(TSPL_VENDOR_MASTER.SecChequeNoRs100,'') as [Security Cheque 100]
                        ,COALESCE(TSPL_VENDOR_MASTER.Bank_Code,TSPL_VENDOR_MASTER.Bank_Code_Desc) as [Bank], isnull(TSPL_VENDOR_MASTER.Branch_Name,'') as Branch
                        ,ISNULL(TSPL_VENDOR_MASTER.IFSC_Code,'') AS [IFSC Code],isnull(TSPL_VENDOR_MASTER.Account_No,'') as [Account No]
                        ,case when Asset.AssetCount>0 then 'Y' else 'N' end as [Asset]
                        from TSPL_VENDOR_MASTER LEFT OUTER JOIN tspl_vlc_master_head ON TSPL_VENDOR_MASTER.VENDOR_CODE=tspl_vlc_master_head.VSP_CODE
                        AND ISNULL(TSPL_VENDOR_MASTER.Form_Type,'')='VSP'
                        LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_CODE=tspl_vlc_master_head.MCC
                        left outer join (select TransactionId as VSP_CODE,count(1) as AttachmentCount from TSPL_ATTACHMENTS
                        where FormId='VSP-MST' group by TransactionId)Attachment on Attachment.vsp_code=TSPL_VENDOR_MASTER.vendor_code
                        left outer join (select Issue_To as Vendor_Code,count(*) as AssetCount from tspl_vsPasset_Head group by Issue_To)Asset on Asset.Vendor_Code=TSPL_VENDOR_MASTER.vendor_code            where ISNULL(TSPL_VENDOR_MASTER.Form_Type,'')='VSP' "
        If ShowActiveStatus = True Then
            qry = "select OuterQry.Center,OuterQry.[VLC Uploader Code],OuterQry.[VLC Code],OuterQry.[VLC Name],OuterQry.[VSP Code] ,OuterQry.[VSP Name]
                        ,OuterQry.[Care Of],OuterQry.[Registration Date],OuterQry.Address,OuterQry.[Aadhar No],OuterQry.[File],OuterQry.[Security Cheque 100000],OuterQry.[Security Cheque 100]
                        ,OuterQry.Bank,OuterQry.Branch,OuterQry.[IFSC Code],OuterQry.[Account No],OuterQry.Asset
                        ,case when (select MAX(DOC_CODE) AS AA from TSPL_MILK_RECEIPT_DETAIL 
                        where TSPL_MILK_RECEIPT_DETAIL.DOC_DATE >= DATEADD(DAY,-3, OuterQry.MAXDate)  AND TSPL_MILK_RECEIPT_DETAIL.VSP_CODE=OuterQry.[VSP Code] 
                        AND TSPL_MILK_RECEIPT_DETAIL.MCC_CODE=OuterQry.MCC_Code) IS NOT NULL then 'Active' else 'In Active' end as [VSP Status]
                        from (select MainQry.* ,Mdate.MAXDate
                        from (" + qry + ")MainQry
                        left outer join(select MCC_CODE,max(DOC_DATE) as MAXDate from TSPL_MILK_Shift_End_HEAD group by MCC_CODE)Mdate on Mdate.MCC_CODE=MainQry.MCC_Code
                        )OuterQry WHERE [VSP Code] IS NOT NULL ORDER BY [VSP Code]"
        Else
            qry = "select OuterQry.Center,OuterQry.[VLC Uploader Code],OuterQry.[VLC Code],OuterQry.[VLC Name],OuterQry.[VSP Code] ,OuterQry.[VSP Name]
                        ,OuterQry.[Care Of],OuterQry.[Registration Date],OuterQry.Address,OuterQry.[Aadhar No],OuterQry.[File],OuterQry.[Security Cheque 100000],OuterQry.[Security Cheque 100]
                        ,OuterQry.Bank,OuterQry.Branch,OuterQry.[IFSC Code],OuterQry.[Account No],OuterQry.Asset               
                        from (" + qry + " )OuterQry WHERE [VSP Code] IS NOT NULL ORDER BY [VSP Code]"
        End If
        Return qry
    End Function

    Public Shared Function getQueryMccMasterDetailTransporter(ByVal ShowActiveStatus As Boolean) As String

        Dim qry As String = " Select tspl_mcc_route_master.route_code ,TSPL_MCC_MASTER.MCC_Code,isnull(TSPL_MCC_MASTER.MCC_NAME,'') AS [Center],isnull(TSPL_Primary_Vehicle_Master.Vehicle_Code,'') as [Vehicle No]
                            ,convert(varchar,TSPL_VENDOR_MASTER.Created_Date,103) as [Registration Date]
                            ,ISNULL(TSPL_VENDOR_MASTER.Pan,'') as [PAN],TSPL_VENDOR_MASTER.VENDOR_Code as [Transporter Code]
                            ,TSPL_VENDOR_MASTER.VENDOR_Name as [Transporter Name],isnull(TSPL_VENDOR_MASTER.Care_Of,'') as [Care Of]
                            ,ISNULL(TSPL_VENDOR_MASTER.ADD1,'')+ISNULL(TSPL_VENDOR_MASTER.ADD2,'')+ISNULL(TSPL_VENDOR_MASTER.ADD3,'') AS [Address]
                            ,isnull(TSPL_VENDOR_MASTER.Aadhar_No,'') as [Aadhar No],case when Attachment.AttachmentCount>0 then 'Y' else 'N' end as [File]
                            , isnull(TSPL_VENDOR_MASTER.SecChequeNoLac1,'') as [Security Cheque 100000],isnull(TSPL_VENDOR_MASTER.SecChequeNoRs100,'') as [Security Cheque 100]
                            ,COALESCE(TSPL_VENDOR_MASTER.Bank_Code,TSPL_VENDOR_MASTER.Bank_Code_Desc) as [Bank], isnull(TSPL_VENDOR_MASTER.Branch_Name,'') as Branch
                            ,ISNULL(TSPL_VENDOR_MASTER.IFSC_Code,'') AS [IFSC Code],isnull(TSPL_VENDOR_MASTER.Account_No,'') as [Account No]
                            ,case when Asset.AssetCount>0 then 'Y' else 'N' end as [Asset],TSPL_Primary_Vehicle_Master.Vehicle
                            from TSPL_VENDOR_MASTER left join TSPL_Primary_Vehicle_Master on TSPL_VENDOR_MASTER.vendor_code =TSPL_Primary_Vehicle_Master.VENDOR_CODE
                            AND ISNULL(TSPL_VENDOR_MASTER.Form_Type,'')='PTM'
                            left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_Primary_Vehicle_Master.MCC_Code
                            left join tspl_mcc_route_master on TSPL_Primary_Vehicle_Master.MCC_Code=tspl_mcc_route_master.mcc_code
							 and  TSPL_Primary_Vehicle_Master.Vehicle_Code=tspl_mcc_route_master.Vehicle_Code
                            left outer join (select TransactionId as VSP_CODE,count(1) as AttachmentCount from TSPL_ATTACHMENTS
                            where FormId='PTM-MST' group by TransactionId)Attachment on Attachment.vsp_code=TSPL_VENDOR_MASTER.vendor_code
                            left outer join (select Issue_To as Vendor_Code,count(*) as AssetCount from tspl_vsPasset_Head group by Issue_To)Asset on Asset.Vendor_Code=TSPL_VENDOR_MASTER.vendor_code
                             where ISNULL(TSPL_VENDOR_MASTER.Form_Type,'')='PTM' "

        If ShowActiveStatus = True Then
            qry = "select OuterQry.Center,OuterQry.[Vehicle No],OuterQry.[Registration Date],OuterQry.PAN,OuterQry.[Transporter Code],OuterQry.[Transporter Name]
                            ,OuterQry.[Care Of],OuterQry.Address,OuterQry.[Aadhar No],OuterQry.[File],OuterQry.[Security Cheque 100000],OuterQry.[Security Cheque 100],OuterQry.Bank
                            ,OuterQry.Branch,OuterQry.[IFSC Code],OuterQry.[Account No],OuterQry.Asset,OuterQry.Vehicle
                            ,case when (select MAX(DOC_CODE) AS AA from TSPL_MILK_Shift_End_Route_DETAIL 
                            where TSPL_MILK_Shift_End_Route_DETAIL.DOC_DATE >= DATEADD(DAY,-1, OuterQry.MAXDate)  AND TSPL_MILK_Shift_End_Route_DETAIL.VEHICLE_CODE=OuterQry.[Vehicle No] 
					        and TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE =OuterQry.Route_CODE)
                            IS NOT NULL then 'Active' else 'In Active' end as [TPT Status]
                            from (select MainQry.* ,Mdate.MAXDate
                            from ( " + qry + " )MainQry
                            left outer join(select MCC_CODE,max(DOC_DATE) as MAXDate from TSPL_MILK_Shift_End_HEAD group by MCC_CODE)Mdate on Mdate.MCC_CODE=MainQry.MCC_Code
                            )OuterQry WHERE [Transporter Code] IS NOT NULL ORDER BY [Transporter Code]"
        Else
            qry = "select OuterQry.Center,OuterQry.[Vehicle No],OuterQry.[Registration Date],OuterQry.PAN,OuterQry.[Transporter Code],OuterQry.[Transporter Name]
                            ,OuterQry.[Care Of],OuterQry.Address,OuterQry.[Aadhar No],OuterQry.[File],OuterQry.[Security Cheque 100000],OuterQry.[Security Cheque 100],OuterQry.Bank
                            ,OuterQry.Branch,OuterQry.[IFSC Code],OuterQry.[Account No],OuterQry.Asset,OuterQry.Vehicle                         
                            from ( " + qry + " )OuterQry WHERE [Transporter Code] IS NOT NULL ORDER BY [Transporter Code]"
        End If

        Return qry
    End Function

    Public Shared Function getQueryMccMasterDetailEmployee() As String
        Dim qry As String = " select isnull(tspl_location_master.Location_Desc,'') AS [Center],isnull(TSPL_EMPLOYEE_MASTER.Designation,'') as Designation
                            ,convert(varchar,TSPL_EMPLOYEE_MASTER.Joining_date,103) as [Registration Date],TSPL_EMPLOYEE_MASTER.PAN_NO as [PAN]
                            ,TSPL_EMPLOYEE_MASTER.EMP_CODE as [Employee Code],TSPL_EMPLOYEE_MASTER.Emp_Name as [Employee Name]
                            ,isnull(FATHERS_NAME,'') +isnull(MOTHERS_NAME,'')+isnull(SPOUSE_NAME,'') as [Care Of],ISNULL(TSPL_EMPLOYEE_MASTER.ADD1,'')+ISNULL(TSPL_EMPLOYEE_MASTER.ADD2,'') AS [Address]
                            ,isnull(TSPL_EMPLOYEE_MASTER.Adhar_No,'') as [Aadhar No],case when Document.DocumentCount>0 then 'Y' else 'N' end as [File]
                            , isnull(TSPL_EMPLOYEE_MASTER.SecChequeNoLac1,'') as [Security Cheque 100000],isnull(TSPL_EMPLOYEE_MASTER.SecChequeNoRs100,'') as [Security Cheque 100]
                            ,isnull(TSPL_EMPLOYEE_MASTER.Bank_Name,'') as [Bank], isnull(TSPL_EMPLOYEE_MASTER.Bank_Branch_Name,'') as Branch
                             ,ISNULL(TSPL_EMPLOYEE_MASTER.Bank_Branch,'') AS [IFSC Code],isnull(TSPL_EMPLOYEE_MASTER.BANK_ACC_NO,'') as [Account No]
                             ,case when TSPL_EMPLOYEE_MASTER.RELIEVING_DATE is null then 'On Duty' else 'Left' end as [Current Status]
                            from TSPL_EMPLOYEE_MASTER
                            left outer join tspl_location_master on tspl_location_master.location_code=TSPL_EMPLOYEE_MASTER.location_code
                            left outer join (select  EMP_CODE,count(*) as DocumentCount from TSPL_EMPLOYEE_DOCUMENTS
                            where 1=1 group by EMP_CODE) Document on Document.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE "
        Return qry
    End Function
End Class