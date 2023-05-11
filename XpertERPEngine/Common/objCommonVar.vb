Imports common
Public Class objCommonVar
#Region "Variables"
    Private Shared _currComp_Code1 As String = ""
    Private Shared _currUserCode As String = ""
    Private Shared _currUserName As String = ""
    Private Shared _currCompanyCode As String = ""
    Private Shared _currCompanyName As String = ""
    Private Shared _currDatabase As String = ""
    Private Shared _currLocationCode As String = ""
    Private Shared _currLocationName As String = ""
    Private Shared _currLoginId As String = ""
    Private Shared _arrCurrUserLocations As List(Of String) = Nothing
    Private Shared _arrCurrUserGLAccount As List(Of String) = Nothing
    Private Shared _SelectedUser As String = ""
    Private Shared _PORptOrder As String = ""

    Private Shared _strCurrUserLocations As String = ""
    Private Shared _strCurrUserGLAccount As String = ""
    Private Shared _strCurrUserLocationsSegment As String = ""
    Private Shared _strConnString As String = ""

    Private Shared _RoundOffTaxToZeroDecimal As Boolean = False
    Private Shared _CalculateFIFOAndLIFOCosting As Boolean = False
    Private Shared _currUserLevel As Integer = 0
    Private Shared _IsDemoERP As Boolean = False
    Private Shared _IsKDIL As Boolean = False
    Private Shared _IsSendToTally As Boolean = False
    Private Shared _IsPromptForTally As Boolean = False
    Private Shared _TallyCompany As String = ""
    Private Shared _TallyIP As String = ""
    Private Shared _TallyPort As String = ""
    Private Shared _BaseCurrencyCode As String = ""
    Private Shared _IsMultiCurrencyCompany As Boolean = False
    Private Shared _CurrentIndustryType As String = ""
    Private Shared _IsAutoTabOrdering As Boolean = False
    Private Shared _CurrentTabOrderPattern As Integer = 1
    Private Shared _AutoRestoreGridLayout As Integer = 1
    Private Shared _AutoSetTabStopFalseToReadOnlyControls As Integer = 1

    Private Shared _CurrFiscalYear As String
    Private Shared _CurrFiscalStartDate As Date
    Private Shared _CurrFiscalEndDate As Date
    Private Shared _GSTApplicableDate As Date?
    Private Shared _NoOfJournalEntery As Integer = 1
    Private Shared _NoOfUser As Integer = 1
    Private Shared _GSTApplicable As Boolean
    Private Shared _GSTActiveTaxGroup As Boolean
    Public Shared LicenceMessageContactPersion As String = "Please Contact : " + Environment.NewLine + "Mr. Rakesh Sharma : +91-9899578949 Email ID : rakesh.sharma@tecxpert.in Or GoTo www.tecxpert.in"
    Private Shared _IsMailSend = False
    Private Shared _TreatUnregisteredVendorAsRegisteredVendor As Boolean = False
    Private Shared _is_Cancel_Allowed As Integer = 0
    Private Shared _is_AllowDesignAtRunTime As Boolean = False
    Private Shared _is_ItemSturctureMandatoryOnWeightConversion As Boolean = False
    Private Shared _DefaultMilkItemCode As String
    Private Shared _DefaultMilkItemCodeCow As String
    Private Shared _DefaultMilkItemCodeBuffalo As String
    Public Shared ScreenToOpen As String
    Public Shared ScreenToOpenDocNo As String
    Public Shared ScreenToOpenUOM As String
    Public Shared ScreenToOpenIsMRPMandatory As String
    Public Shared ScreenToOpen_Text As String
    Public Shared ScreenToOpenQry As String

    '' app server
    Private Shared _Database_Server_UserName As String
    Private Shared _Database_Server_Password As String
    Private Shared _Database_Server As String
    Private Shared _App_ServerId As String
    Private Shared _Application_Server As String
    Private Shared _App_IP As String
    Private Shared _Binding As Object
    Private Shared _EndPointAddress As Object
    Private Shared _OperationTimeout As Integer

    Private Shared _maxBufferPoolSize As Integer
    Private Shared _maxReceivedMessageSize As Integer
    Private Shared _transferMode As Integer
    Private Shared _maxStringContentLength As Integer
    Private Shared _maxArrayLength As Integer
    Private Shared _maxBytesPerRead As Integer
    Private Shared _maxNameTableCharCount As Integer

    Private Shared _SepratePriceChartForCow As Boolean
    Private Shared _DisplayTypeInMilkReceipt As Boolean
    Private Shared _ApplyStdFATSNFRate As Boolean
    Private Shared _ApplyTransFATSNFRateForCalculateFATSNFRate As Boolean
    Private Shared _AutoStartReading As Boolean

    Private Shared _ParameterForSNFatQC As Decimal
    Private Shared _ObjVar1 As String = Nothing
    Private Shared _ObjVar2 As Date? = Nothing
    Private Shared _ObjVar3 As Double = Nothing
    Private Shared _ObjVar4 As String = Nothing
    Private Shared _ObjVar5 As String = Nothing

    Private Shared _AddValidationofMilkTypeinsample As String = Nothing
    Private Shared _FatMinCow As String
    Private Shared _FatMaxCow As Decimal
    Private Shared _SNFMinCow As Decimal
    Private Shared _SNFMaxCow As Decimal
    Private Shared _FatMinBuff As Decimal
    Private Shared _FatMaxBuff As Decimal
    Private Shared _SNFMinBuff As Decimal
    Private Shared _SNFMaxBuff As Decimal
    Private Shared _FatMinMix As Decimal
    Private Shared _FatMaxMix As Decimal
    Private Shared _SNFMinMix As Decimal
    Private Shared _SNFMaxMix As Decimal
    Private Shared _ControlMForHelp As Boolean
    Private Shared _InternalSMSEmailinPurchaseModule As Boolean
    Private Shared _MilkProcurementSNF2DecimalPlaces As Boolean
    Private Shared _ShowMCCFinderInPaymentProcess As Boolean
    Private Shared _MilkSRNFATSNFDecimalPlaces As Integer
    Private Shared _PricePlan As Integer
    Private Shared _EInvoiceImplementationDate As Date?
    Private Shared _GenerateEWayBillWithEInvoice As Boolean
    Private Shared _TDSValidationFrom As Date?
    Private Shared _ApplyGovtRulesInTDS As Boolean
    Private Shared _IsLoginUserHRAdmin As Boolean
    Private Shared _ApplyDefaultsInMaster As Boolean
    Private Shared _RCDFCFP As Boolean = False
    Private Shared _ApplyLocationFilterBasedOnPermission As Boolean = False
    Private Shared _strCurrUserZones As String = ""
    Private Shared _strCurrUserCustomers As String = ""
    Private Shared _MilkRateRoundOffType As Integer = 0

    Private Shared _DCSAddDedRODecimalPlace As Integer = 0
    Private Shared _DCSAddDedROIncreaseAfter As Integer = 0
    Private Shared _DCSAddDedROHeaderLevel As Boolean = False
#End Region

    Public Shared Property CurrComp_Code1() As String
        Get
            Return _currComp_Code1
        End Get
        Set(ByVal Value As String)
            _currComp_Code1 = Value
        End Set
    End Property

    Public Shared Property DCSAddDedROHeaderLevel() As Boolean
        Get
            Return _DCSAddDedROHeaderLevel
        End Get
        Set(ByVal Value As Boolean)
            _DCSAddDedROHeaderLevel = Value
        End Set
    End Property
    Public Shared Property DCSAddDedRODecimalPlace() As Integer
        Get
            Return _DCSAddDedRODecimalPlace
        End Get
        Set(ByVal Value As Integer)
            _DCSAddDedRODecimalPlace = Value
        End Set
    End Property

    Public Shared Property DCSAddDedROIncreaseAfter() As Integer
        Get
            Return _DCSAddDedROIncreaseAfter
        End Get
        Set(ByVal Value As Integer)
            _DCSAddDedROIncreaseAfter = Value
        End Set
    End Property



    Public Shared Property strCurrUserCustomers() As String
        Get
            Return _strCurrUserCustomers
        End Get
        Set(ByVal Value As String)
            _strCurrUserCustomers = Value
        End Set
    End Property
    Public Shared Property strCurrUserZones() As String
        Get
            Return _strCurrUserZones
        End Get
        Set(ByVal Value As String)
            _strCurrUserZones = Value
        End Set
    End Property

    Public Shared Property ApplyLocationFilterBasedOnPermission() As Boolean
        Get
            Return _ApplyLocationFilterBasedOnPermission
        End Get
        Set(ByVal Value As Boolean)
            _ApplyLocationFilterBasedOnPermission = Value
        End Set
    End Property
    Public Shared Property RCDFCFP() As Boolean
        Get
            Return _RCDFCFP
        End Get
        Set(ByVal Value As Boolean)
            _RCDFCFP = Value
        End Set
    End Property
    Public Shared Property ApplyDefaultsInMaster() As Boolean
        Get
            Return _ApplyDefaultsInMaster
        End Get
        Set(ByVal Value As Boolean)
            _ApplyDefaultsInMaster = Value
        End Set
    End Property
    Public Shared Property IsLoginUserHRAdmin() As Boolean
        Get
            Return _IsLoginUserHRAdmin
        End Get
        Set(ByVal Value As Boolean)
            _IsLoginUserHRAdmin = Value
        End Set
    End Property

    Public Shared Property ApplyGovtRulesInTDS() As Boolean
        Get
            Return _ApplyGovtRulesInTDS
        End Get
        Set(ByVal Value As Boolean)
            _ApplyGovtRulesInTDS = Value
        End Set
    End Property

    Public Shared Property TDSValidationFrom() As Date?
        Get
            Return _TDSValidationFrom
        End Get
        Set(ByVal Value As Date?)
            _TDSValidationFrom = Value
        End Set
    End Property
    Public Shared Property GenerateEWayBillWithEInvoice() As Boolean
        Get
            Return _GenerateEWayBillWithEInvoice
        End Get
        Set(ByVal Value As Boolean)
            _GenerateEWayBillWithEInvoice = Value
        End Set
    End Property

    Public Shared Property EInvoiceImplementationDate() As Date
        Get
            Return _EInvoiceImplementationDate
        End Get
        Set(ByVal Value As Date)
            _EInvoiceImplementationDate = Value
        End Set
    End Property

    Public Shared Property PricePlan() As Integer
        Get
            Return _PricePlan
        End Get
        Set(ByVal Value As Integer)
            _PricePlan = Value
        End Set
    End Property
    Public Shared Property MilkSRNFATSNFDecimalPlaces() As Integer
        Get
            Return _MilkSRNFATSNFDecimalPlaces
        End Get
        Set(ByVal Value As Integer)
            _MilkSRNFATSNFDecimalPlaces = Value
        End Set
    End Property

    Public Shared Property ShowMCCFinderInPaymentProcess() As Boolean
        Get
            Return _ShowMCCFinderInPaymentProcess
        End Get
        Set(ByVal Value As Boolean)
            _ShowMCCFinderInPaymentProcess = Value
        End Set
    End Property

    Public Shared Property MilkProcurementSNF2DecimalPlaces() As Boolean
        Get
            Return _MilkProcurementSNF2DecimalPlaces
        End Get
        Set(ByVal Value As Boolean)
            _MilkProcurementSNF2DecimalPlaces = Value
        End Set
    End Property

    Public Shared Property InternalSMSEmailinPurchaseModule() As Boolean
        Get
            Return _InternalSMSEmailinPurchaseModule
        End Get
        Set(ByVal Value As Boolean)
            _InternalSMSEmailinPurchaseModule = Value
        End Set
    End Property

    Public Shared Property ControlMForHelp() As Boolean
        Get
            Return _ControlMForHelp
        End Get
        Set(ByVal Value As Boolean)
            _ControlMForHelp = Value
        End Set
    End Property

    Public Shared Property ParameterForSNFatQC() As Decimal
        Get
            Return _ParameterForSNFatQC
        End Get
        Set(ByVal Value As Decimal)
            _ParameterForSNFatQC = Value
        End Set
    End Property

    Public Shared Property AutoStartReading() As Boolean
        Get
            Return _AutoStartReading
        End Get
        Set(ByVal Value As Boolean)
            _AutoStartReading = Value
        End Set
    End Property

    Public Shared Property ApplyStdFATSNFRate() As Boolean
        Get
            Return _ApplyStdFATSNFRate
        End Get
        Set(ByVal Value As Boolean)
            _ApplyStdFATSNFRate = Value
        End Set
    End Property

    Public Shared Property ApplyTransFATSNFRateForCalculateFATSNFRate() As Boolean
        Get
            Return _ApplyTransFATSNFRateForCalculateFATSNFRate
        End Get
        Set(ByVal Value As Boolean)
            _ApplyTransFATSNFRateForCalculateFATSNFRate = Value
        End Set
    End Property

    Public Shared Property SepratePriceChartForCow() As Boolean
        Get
            Return _SepratePriceChartForCow
        End Get
        Set(ByVal Value As Boolean)
            _SepratePriceChartForCow = Value
        End Set
    End Property

    Public Shared Property DisplayTypeInMilkReceipt() As Boolean
        Get
            Return _DisplayTypeInMilkReceipt
        End Get
        Set(ByVal Value As Boolean)
            _DisplayTypeInMilkReceipt = Value
        End Set
    End Property

    Public Shared Property Database_Server_UserName() As String
        Get
            Return _Database_Server_UserName
        End Get
        Set(ByVal Value As String)
            _Database_Server_UserName = Value
        End Set
    End Property

    Public Shared Property Database_Server_Password() As String
        Get
            Return _Database_Server_Password
        End Get
        Set(ByVal Value As String)
            _Database_Server_Password = Value
        End Set
    End Property

    Public Shared Property Database_Server() As String
        Get
            Return _Database_Server
        End Get
        Set(ByVal Value As String)
            _Database_Server = Value
        End Set
    End Property
    Public Shared Property App_ServerId() As Integer
        Get
            Return _App_ServerId
        End Get
        Set(ByVal Value As Integer)
            _App_ServerId = Value
        End Set
    End Property
    Public Shared Property Application_Server() As String
        Get
            Return _Application_Server
        End Get
        Set(ByVal Value As String)
            _Application_Server = Value
        End Set
    End Property
    Public Shared Property App_IP() As String
        Get
            Return _App_IP
        End Get
        Set(ByVal Value As String)
            _App_IP = Value
        End Set
    End Property
    Public Shared Property Binding() As Object
        Get
            Return _Binding
        End Get
        Set(ByVal Value As Object)
            _Binding = Value
        End Set
    End Property
    Public Shared Property EndPointAddress() As Object
        Get
            Return _EndPointAddress
        End Get
        Set(ByVal Value As Object)
            _EndPointAddress = Value
        End Set
    End Property
    Public Shared Property OperationTimeout() As Integer
        Get
            Return _OperationTimeout
        End Get
        Set(ByVal Value As Integer)
            _OperationTimeout = Value
        End Set
    End Property
    Public Shared Property maxBufferPoolSize() As Integer
        Get
            Return _maxBufferPoolSize
        End Get
        Set(ByVal Value As Integer)
            _maxBufferPoolSize = Value
        End Set
    End Property
    Public Shared Property maxReceivedMessageSize() As Integer
        Get
            Return _maxReceivedMessageSize
        End Get
        Set(ByVal Value As Integer)
            _maxReceivedMessageSize = Value
        End Set
    End Property
    Public Shared Property maxArrayLength() As Integer
        Get
            Return _maxArrayLength
        End Get
        Set(ByVal Value As Integer)
            _maxArrayLength = Value
        End Set
    End Property
    Public Shared Property maxBytesPerRead() As Integer
        Get
            Return _maxBytesPerRead
        End Get
        Set(ByVal Value As Integer)
            _maxBytesPerRead = Value
        End Set
    End Property
    Public Shared Property maxNameTableCharCount() As Integer
        Get
            Return _maxNameTableCharCount
        End Get
        Set(ByVal Value As Integer)
            _maxNameTableCharCount = Value
        End Set
    End Property
    Public Shared Property maxStringContentLength() As Integer
        Get
            Return _maxStringContentLength
        End Get
        Set(ByVal Value As Integer)
            _maxStringContentLength = Value
        End Set
    End Property
    Public Shared Property is_Cancel_Allowed() As Integer
        Get
            Return _is_Cancel_Allowed
        End Get
        Set(ByVal Value As Integer)
            _is_Cancel_Allowed = Value
        End Set
    End Property

    Public Shared Property AllowDesignAtRunTime() As Boolean
        Get
            Return _is_AllowDesignAtRunTime
        End Get
        Set(ByVal Value As Boolean)
            _is_AllowDesignAtRunTime = Value
        End Set
    End Property

    Public Shared Property ItemSturctureMandatoryOnWeightConversion() As Boolean
        Get
            Return _is_ItemSturctureMandatoryOnWeightConversion
        End Get
        Set(ByVal Value As Boolean)
            _is_ItemSturctureMandatoryOnWeightConversion = Value
        End Set
    End Property

    Public Shared Property DefaultMilkItemCode() As String
        Get
            Return _DefaultMilkItemCode
        End Get
        Set(ByVal Value As String)
            _DefaultMilkItemCode = Value
        End Set
    End Property

    Public Shared Property DefaultMilkItemCodeCow() As String
        Get
            Return _DefaultMilkItemCodeCow
        End Get
        Set(ByVal Value As String)
            _DefaultMilkItemCodeCow = Value
        End Set
    End Property
    Public Shared Property DefaultMilkItemCodeBuffalo() As String
        Get
            Return _DefaultMilkItemCodeBuffalo
        End Get
        Set(ByVal Value As String)
            _DefaultMilkItemCodeBuffalo = Value
        End Set
    End Property

    Public Shared Property NoOfJournalEnteryLicence() As Integer
        Get
            Return _NoOfJournalEntery
        End Get
        Set(ByVal Value As Integer)
            _NoOfJournalEntery = Value
        End Set
    End Property

    Public Shared Property NoOfUserLicence() As Integer
        Get
            Return _NoOfUser
        End Get
        Set(ByVal Value As Integer)
            _NoOfUser = Value
        End Set
    End Property
    Public Shared Property GSTApplicable() As Boolean
        Get
            Return _GSTApplicable
        End Get
        Set(ByVal Value As Boolean)
            _GSTApplicable = Value
        End Set
    End Property
    Public Shared Property GSTActiveTaxGroup() As Boolean
        Get
            Return _GSTActiveTaxGroup
        End Get
        Set(ByVal Value As Boolean)
            _GSTActiveTaxGroup = Value
        End Set
    End Property
    Public Shared Property GSTApplicableDate() As Date
        Get
            Return _GSTApplicableDate
        End Get
        Set(ByVal Value As Date)
            _GSTApplicableDate = Value
        End Set
    End Property
    Public Shared Property CurrFiscalYear() As String
        Get
            Return _CurrFiscalYear
        End Get
        Set(ByVal Value As String)
            _CurrFiscalYear = Value
        End Set
    End Property

    Public Shared Property CurrFiscalStartDate() As Date
        Get
            Return _CurrFiscalStartDate
        End Get
        Set(ByVal Value As Date)
            _CurrFiscalStartDate = Value
        End Set
    End Property

    Public Shared Property CurrFiscalEndDate() As Date
        Get
            Return _CurrFiscalEndDate
        End Get
        Set(ByVal Value As Date)
            _CurrFiscalEndDate = Value
        End Set
    End Property



    Public Shared Property AddValidationofMilkTypeinsample() As Boolean
        Get
            Return _AddValidationofMilkTypeinsample
        End Get
        Set(ByVal Value As Boolean)
            _AddValidationofMilkTypeinsample = Value
        End Set
    End Property
    Public Shared Property MilkRateRoundOffType() As Integer
        Get
            Return _MilkRateRoundOffType
        End Get
        Set(ByVal Value As Integer)
            _MilkRateRoundOffType = Value
        End Set
    End Property
    Public Shared Property FatMinCow() As Decimal
        Get
            Return _FatMinCow
        End Get
        Set(ByVal Value As Decimal)
            _FatMinCow = Value
        End Set
    End Property
    Public Shared Property FatMaxCow() As Decimal
        Get
            Return _FatMaxCow
        End Get
        Set(ByVal Value As Decimal)
            _FatMaxCow = Value
        End Set
    End Property
    Public Shared Property SNFMinCow() As Decimal
        Get
            Return _SNFMinCow
        End Get
        Set(ByVal Value As Decimal)
            _SNFMinCow = Value
        End Set
    End Property
    Public Shared Property SNFMaxCow() As Decimal
        Get
            Return _SNFMaxCow
        End Get
        Set(ByVal Value As Decimal)
            _SNFMaxCow = Value
        End Set
    End Property
    Public Shared Property FatMinBuff() As Decimal
        Get
            Return _FatMinBuff
        End Get
        Set(ByVal Value As Decimal)
            _FatMinBuff = Value
        End Set
    End Property
    Public Shared Property FatMaxBuff() As Decimal
        Get
            Return _FatMaxBuff
        End Get
        Set(ByVal Value As Decimal)
            _FatMaxBuff = Value
        End Set
    End Property
    Public Shared Property SNFMinBuff() As Decimal
        Get
            Return _SNFMinBuff
        End Get
        Set(ByVal Value As Decimal)
            _SNFMinBuff = Value
        End Set
    End Property
    Public Shared Property SNFMaxBuff() As Decimal
        Get
            Return _SNFMaxBuff
        End Get
        Set(ByVal Value As Decimal)
            _SNFMaxBuff = Value
        End Set
    End Property
    Public Shared Property FatMinMix() As Decimal
        Get
            Return _FatMinMix
        End Get
        Set(ByVal Value As Decimal)
            _FatMinMix = Value
        End Set
    End Property
    Public Shared Property FatMaxMix() As Decimal
        Get
            Return _FatMaxMix
        End Get
        Set(ByVal Value As Decimal)
            _FatMaxMix = Value
        End Set
    End Property
    Public Shared Property SNFMinMix() As Decimal
        Get
            Return _SNFMinMix
        End Get
        Set(ByVal Value As Decimal)
            _SNFMinMix = Value
        End Set
    End Property
    Public Shared Property SNFMaxMix() As Decimal
        Get
            Return _SNFMaxMix
        End Get
        Set(ByVal Value As Decimal)
            _SNFMaxMix = Value
        End Set
    End Property



    Public Shared Sub RefreshCommonVar()
        Dim strtempvar As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DateOfEInvoiceImplementation, clsFixedParameterCode.DateOfEInvoiceImplementation, Nothing))
        If clsCommon.myLen(strtempvar) > 0 Then
            objCommonVar.EInvoiceImplementationDate = clsCommon.myCDate(strtempvar)
        End If
        strtempvar = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTDSValidationFrom, clsFixedParameterCode.ApplyTDSValidationFrom, Nothing))
        If clsCommon.myLen(strtempvar) > 0 Then
            objCommonVar.TDSValidationFrom = clsCommon.myCDate(strtempvar)
        Else
            objCommonVar.TDSValidationFrom = Nothing
        End If
        objCommonVar.ApplyGovtRulesInTDS = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyGovtRulesInTDS, clsFixedParameterCode.ApplyGovtRulesInTDS, Nothing)) = 1, True, False)
        objCommonVar.PricePlan = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.OpenPriceChartPlanningScreenOnTotalSolid, clsFixedParameterCode.OpenPriceChartPlanningScreenOnTotalSolid, Nothing))
        objCommonVar.MilkSRNFATSNFDecimalPlaces = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkSRNFATSNFDecimalPlaces, clsFixedParameterCode.MilkSRNFATSNFDecimalPlaces, Nothing))
        objCommonVar.IsDemoERP = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunDemoERP, clsFixedParameterCode.RunDemoERP, Nothing)) = 1, True, False)
        objCommonVar.IsSendToTally = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SendToTally, clsFixedParameterCode.SendToTally, Nothing)) = 1, True, False)
        objCommonVar.TallyCompany = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.TallyCompany, clsFixedParameterCode.TallyCompany, Nothing))
        objCommonVar.TallyIP = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.TallyIP, clsFixedParameterCode.TallyIP, Nothing))
        objCommonVar.TallyPort = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.TallyPort, clsFixedParameterCode.TallyPort, Nothing))
        objCommonVar.IsRoundOffTaxToZeroDecimal = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TaxRoundOffToZeroDecimalPlace, clsFixedParameterCode.TaxRoundOffToZeroDecimalPlace, Nothing)) = 1, True, False)
        objCommonVar.IsPromptForTally = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PromptForTally, clsFixedParameterCode.PromptForTally, Nothing)) = 1, True, False)
        objCommonVar.CurrentIndustryType = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing))
        objCommonVar.IsAutoTabOrdering = IIf(clsFixedParameter.GetData(clsFixedParameterType.TabOrder, clsFixedParameterCode.AutoTabOrdering, Nothing) = "1", True, False)
        objCommonVar.CurrentTabOrderPattern = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TabOrder, clsFixedParameterCode.AutoTabOrderingPattern, Nothing))
        objCommonVar.AutoRestoreGridLayout = IIf(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AutoRestoreGridLayout, Nothing) = "1", True, False)
        objCommonVar.AutoSetTabStopForReadOnlyControls = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AutoSetTabStopFalseForReadonlyControls, Nothing))
        objCommonVar.IsKDIL = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsKDIL, clsFixedParameterCode.IsKDIL, Nothing)) = 1, True, False)
        objCommonVar.IsMailSend = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MAILOFF, clsFixedParameterCode.MAILOFF, Nothing)) = 1, True, False)

        objCommonVar.AllowDesignAtRunTime = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowDesignAtRunTime, clsFixedParameterCode.AllowDesignAtRunTime, Nothing)) = 1, True, False)
        objCommonVar.is_Cancel_Allowed = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.is_Allow_cancel_Transaction, clsFixedParameterCode.Is_Allow_Cancel_Transaction, Nothing))

        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Fiscal_Code,Start_Date,End_Date from TSPL_Fiscal_Year_Master where Is_Current_Year=1")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objCommonVar.CurrFiscalYear = clsCommon.myCstr(dt.Rows(0)("Fiscal_Code"))
            objCommonVar._CurrFiscalStartDate = clsCommon.GetDateWithStartTime(clsCommon.myCDate(dt.Rows(0)("Start_Date")))
            objCommonVar.CurrFiscalEndDate = clsCommon.GetDateWithEndTime(clsCommon.myCDate(dt.Rows(0)("End_Date")))
        End If

        objCommonVar.NoOfJournalEnteryLicence = clsCommon.myCdbl(clsCommon.DecryptString(clsFixedParameter.GetData(clsFixedParameterType.LicenceNoOfJournalEntry, clsFixedParameterCode.LicenceNoOfJournalEntry, Nothing), objCommonVar.CurrentCompanyCode + "D"))
        objCommonVar.NoOfUserLicence = clsCommon.myCdbl(clsCommon.DecryptString(clsFixedParameter.GetData(clsFixedParameterType.LicenceNoOfUser, clsFixedParameterCode.LicenceNoOfUser, Nothing), objCommonVar.CurrentCompanyCode + "E"))
        objCommonVar.CalculateFIFOAndLIFOCosting = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateFIFOAndLIFOCosting, clsFixedParameterCode.CalculateFIFOAndLIFOCosting, Nothing)) = 1
        objCommonVar.GSTApplicable = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GSTApplicable, clsFixedParameterCode.GSTApplicable, Nothing)) > 0
        Dim strtemp As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.GSTApplicableDate, clsFixedParameterCode.GSTApplicableDate, Nothing))
        If clsCommon.myLen(strtemp) > 0 Then
            objCommonVar.GSTApplicableDate = clsCommon.myCDate(strtemp)
        End If
        objCommonVar.GSTActiveTaxGroup = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GSTActiveTaxesRatesGroup, clsFixedParameterCode.GSTActiveTaxesRatesGroup, Nothing)) > 0
        objCommonVar.SepratePriceChartForCow = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SepratePriceChartForCowMilk, clsFixedParameterCode.SepratePriceChartForCowMilk, Nothing)) = 1
        objCommonVar.DisplayTypeInMilkReceipt = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DisplayTypeInMilkReceipt, clsFixedParameterCode.DisplayTypeInMilkReceipt, Nothing)) = 1
        objCommonVar.ApplyStdFATSNFRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyStdFATSNFRate, clsFixedParameterCode.ApplyStdFATSNFRate, Nothing)) = 1
        objCommonVar.TreatUnregisteredVendorAsRegisteredVendor = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TreatUnregisteredVendorAsRegisteredVendor, clsFixedParameterCode.TreatUnregisteredVendorAsRegisteredVendor, Nothing)) = 1, True, False)
        objCommonVar.AutoStartReading = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsAutoStartReading, clsFixedParameterCode.IsAutoReceiptPayment, Nothing)) = 1, True, False)
        objCommonVar.ParameterForSNFatQC = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ParameterForSNFatQC, clsFixedParameterCode.ParameterForSNFatQC, Nothing))

        objCommonVar.ApplyTransFATSNFRateForCalculateFATSNFRate = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyTransFATSNFRateForCalculateFATSNFRate, clsFixedParameterCode.ApplyTransFATSNFRateForCalculateFATSNFRate, Nothing)) = 1)
        objCommonVar.ItemSturctureMandatoryOnWeightConversion = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1)
        objCommonVar.DefaultMilkItemCode = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)
        objCommonVar.DefaultMilkItemCodeCow = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItemCow, clsFixedParameterCode.MilkSetting, Nothing)
        objCommonVar.DefaultMilkItemCodeBuffalo = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItemBuffalo, clsFixedParameterCode.MilkSetting, Nothing)
        objCommonVar.DisplayTypeInMilkReceipt = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DisplayTypeInMilkReceipt, clsFixedParameterCode.DisplayTypeInMilkReceipt, Nothing)) = 1)

        objCommonVar.MilkRateRoundOffType = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkRateRoundOffType, clsFixedParameterCode.MilkRateRoundOffType, Nothing))
        objCommonVar.AddValidationofMilkTypeinsample = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AddValidationofMilkTypeinsample, clsFixedParameterCode.AddValidationofMilkTypeinsample, Nothing)) = 1)
        objCommonVar.FatMinCow = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMinCow, clsFixedParameterCode.FatMinCow, Nothing))
        objCommonVar.FatMaxCow = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMaxCow, clsFixedParameterCode.FatMaxCow, Nothing))
        objCommonVar.SNFMinCow = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMinCow, clsFixedParameterCode.SNFMinCow, Nothing))
        objCommonVar.SNFMaxCow = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMaxCow, clsFixedParameterCode.SNFMaxCow, Nothing))

        objCommonVar.FatMinBuff = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMinBuff, clsFixedParameterCode.FatMinBuff, Nothing))
        objCommonVar.FatMaxBuff = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMaxBuff, clsFixedParameterCode.FatMaxBuff, Nothing))
        objCommonVar.SNFMinBuff = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMinBuff, clsFixedParameterCode.SNFMinBuff, Nothing))
        objCommonVar.SNFMaxBuff = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMaxBuff, clsFixedParameterCode.SNFMaxBuff, Nothing))

        objCommonVar.FatMinMix = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMinMix, clsFixedParameterCode.FatMinMix, Nothing))
        objCommonVar.FatMaxMix = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMaxMix, clsFixedParameterCode.FatMaxMix, Nothing))
        objCommonVar.SNFMinMix = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMinMix, clsFixedParameterCode.SNFMinMix, Nothing))
        objCommonVar.SNFMaxMix = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMaxMix, clsFixedParameterCode.SNFMaxMix, Nothing))
        objCommonVar.ControlMForHelp = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UseControlMForHelp, clsFixedParameterCode.UseControlMForHelp, Nothing)) = 1, True, False)
        objCommonVar.InternalSMSEmailinPurchaseModule = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SendInternalSMSEmailinPurchaseModule, clsFixedParameterCode.SendInternalSMSEmailinPurchaseModule, Nothing)) = 1, True, False)


        objCommonVar.MilkProcurementSNF2DecimalPlaces = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcurementSNF2DecimalPlaces, clsFixedParameterCode.MilkProcurementSNF2DecimalPlaces, Nothing)) = 1)
        objCommonVar.ShowMCCFinderInPaymentProcess = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMCCFinderInPaymentProcess, clsFixedParameterCode.ShowMCCFinderInPaymentProcess, Nothing)) = 1)
        objCommonVar.GenerateEWayBillWithEInvoice = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GenerateEWayBillWithEInvoice, clsFixedParameterCode.GenerateEWayBillWithEInvoice, Nothing)) = 1, True, False)
        objCommonVar.ApplyDefaultsInMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyDefaultsInMaster, clsFixedParameterCode.ApplyDefaultsInMaster, Nothing)) = 1, True, False)
        objCommonVar.RCDFCFP = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RCDFCFP, clsFixedParameterCode.RCDFCFP, Nothing)) = 1, True, False)
        objCommonVar.ApplyLocationFilterBasedOnPermission = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyLocationFilterBasedOnPermission, clsFixedParameterCode.ApplyLocationFilterBasedOnPermission, Nothing)) = 1, True, False)

        objCommonVar.DCSAddDedRODecimalPlace = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DCSAddDedRODecimalPlace, clsFixedParameterCode.DCSAddDedRODecimalPlace, Nothing))
        objCommonVar.DCSAddDedROIncreaseAfter = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DCSAddDedROIncreaseAfter, clsFixedParameterCode.DCSAddDedROIncreaseAfter, Nothing))
        objCommonVar.DCSAddDedROHeaderLevel = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DCSAddDedROHeaderLevel, clsFixedParameterCode.DCSAddDedROHeaderLevel, Nothing))
        dt.Dispose()
    End Sub

    Public Shared Property IsDemoERP() As Boolean
        Get
            Return _IsDemoERP
        End Get
        Set(ByVal Value As Boolean)
            _IsDemoERP = Value
        End Set
    End Property

    Public Shared Property CalculateFIFOAndLIFOCosting() As Boolean
        Get
            Return _CalculateFIFOAndLIFOCosting
        End Get
        Set(ByVal Value As Boolean)
            _CalculateFIFOAndLIFOCosting = Value
        End Set
    End Property

    Public Shared Property IsKDIL() As Boolean
        Get
            Return _IsKDIL
        End Get
        Set(ByVal Value As Boolean)
            _IsKDIL = Value
        End Set
    End Property

    Public Shared Property IsSendToTally() As Boolean
        Get
            Return _IsSendToTally
        End Get
        Set(ByVal Value As Boolean)
            _IsSendToTally = Value
        End Set
    End Property

    Public Shared Property IsPromptForTally() As Boolean
        Get
            Return _IsPromptForTally
        End Get
        Set(ByVal Value As Boolean)
            _IsPromptForTally = Value
        End Set
    End Property

    Public Shared Property TallyCompany() As String
        Get
            Return _TallyCompany
        End Get
        Set(ByVal Value As String)
            _TallyCompany = Value
        End Set
    End Property

    Public Shared Property TallyIP() As String
        Get
            Return _TallyIP
        End Get
        Set(ByVal Value As String)
            _TallyIP = Value
        End Set
    End Property

    Public Shared Property TallyPort() As String
        Get
            Return _TallyPort
        End Get
        Set(ByVal Value As String)
            _TallyPort = Value
        End Set
    End Property

    Public Shared Property CurrUserLevel() As Integer
        Get
            Return _currUserLevel
        End Get
        Set(ByVal Value As Integer)
            _currUserLevel = Value
        End Set
    End Property

    Public Shared Property CurrentUserCode() As String
        Get
            Return _currUserCode
        End Get
        Set(ByVal Value As String)
            _currUserCode = Value
        End Set
    End Property

    Public Shared Property SelectedUser() As String
        Get
            Return _SelectedUser
        End Get
        Set(ByVal Value As String)
            _SelectedUser = Value
        End Set
    End Property

    Public Shared Property CurrentLoginID() As String
        Get
            Return _currLoginId
        End Get
        Set(ByVal Value As String)
            _currLoginId = Value
        End Set
    End Property

    Public Shared Property CurrentUser() As String
        Get
            Return _currUserName
        End Get
        Set(ByVal Value As String)
            _currUserName = Value
        End Set
    End Property

    Public Shared Property CurrentCompanyCode() As String
        Get
            Return _currCompanyCode
        End Get
        Set(ByVal Value As String)
            _currCompanyCode = Value
        End Set
    End Property

    Public Shared Property CurrentCompanyName() As String
        Get
            Return _currCompanyName
        End Get
        Set(ByVal Value As String)
            _currCompanyName = Value
        End Set
    End Property

    Public Shared Property CurrDatabase() As String
        Get
            Return _currDatabase
        End Get
        Set(ByVal Value As String)
            _currDatabase = Value.Trim()
        End Set
    End Property

    Public Shared Property CurrLocationCode() As String
        Get
            Return _currLocationCode
        End Get
        Set(ByVal Value As String)
            _currLocationCode = Value
        End Set
    End Property

    Public Shared Property CurrLocationName() As String
        Get
            Return _currLocationName
        End Get
        Set(ByVal Value As String)
            _currLocationName = Value
        End Set
    End Property

    Public Shared Property arrCurrUserLocations() As List(Of String)
        Get
            Return _arrCurrUserLocations
        End Get
        Set(ByVal Value As List(Of String))
            _arrCurrUserLocations = Value
        End Set
    End Property

    ''Public Shared Property arrCurrUserGLAccount() As List(Of String)
    ''    Get
    ''        Return _arrCurrUserGLAccount
    ''    End Get
    ''    Set(ByVal Value As List(Of String))
    ''        _arrCurrUserGLAccount = Value
    ''    End Set
    ''End Property

    Public Shared Property strCurrUserLocations() As String
        Get
            Return _strCurrUserLocations
        End Get
        Set(ByVal Value As String)
            _strCurrUserLocations = Value
        End Set
    End Property

    Public Shared Property strCurrUserGLAccount() As String
        Get
            Return _strCurrUserGLAccount
        End Get
        Set(ByVal Value As String)
            _strCurrUserGLAccount = Value
        End Set
    End Property

    Public Shared Property strCurrUserLocationsSegment() As String
        Get
            Return _strCurrUserLocationsSegment
        End Get
        Set(ByVal Value As String)
            _strCurrUserLocationsSegment = Value
        End Set
    End Property

    Public Shared Property ConnString() As String
        Get
            Return _strConnString
        End Get
        Set(ByVal Value As String)
            _strConnString = Value
        End Set
    End Property

    Public Shared Property PORptOrderChk() As String
        Get
            Return _PORptOrder
        End Get
        Set(ByVal Value As String)
            _PORptOrder = Value
        End Set
    End Property

    Public Shared Property IsRoundOffTaxToZeroDecimal() As Boolean
        Get
            Return _RoundOffTaxToZeroDecimal
        End Get
        Set(ByVal Value As Boolean)
            _RoundOffTaxToZeroDecimal = Value
        End Set
    End Property

    Public Shared Property BaseCurrencyCode() As String
        Get
            Return _BaseCurrencyCode
        End Get
        Set(ByVal Value As String)
            _BaseCurrencyCode = Value
        End Set
    End Property

    Public Shared Property IsMultiCurrencyCompany() As Boolean
        Get
            Return _IsMultiCurrencyCompany
        End Get
        Set(ByVal Value As Boolean)
            _IsMultiCurrencyCompany = Value
        End Set
    End Property

    Public Shared Property CurrentIndustryType() As String
        Get
            Return _CurrentIndustryType
        End Get
        Set(ByVal Value As String)
            _CurrentIndustryType = Value
        End Set
    End Property

    Public Shared Property IsAutoTabOrdering() As Boolean
        Get
            Return _IsAutoTabOrdering
        End Get
        Set(ByVal Value As Boolean)
            _IsAutoTabOrdering = Value
        End Set
    End Property

    Public Shared Property CurrentTabOrderPattern() As Integer
        Get
            Return _CurrentTabOrderPattern
        End Get
        Set(ByVal Value As Integer)
            _CurrentTabOrderPattern = Value
        End Set
    End Property

    Public Shared Property AutoRestoreGridLayout() As Boolean
        Get
            Return _AutoRestoreGridLayout
        End Get
        Set(ByVal Value As Boolean)
            _AutoRestoreGridLayout = Value
        End Set
    End Property

    Public Shared Property AutoSetTabStopForReadOnlyControls() As Integer
        Get
            Return _AutoSetTabStopFalseToReadOnlyControls
        End Get
        Set(ByVal Value As Integer)
            _AutoSetTabStopFalseToReadOnlyControls = Value
        End Set
    End Property

    Public Shared Property IsMailSend As Boolean
        Get
            Return _IsMailSend
        End Get
        Set(ByVal Value As Boolean)
            _IsMailSend = Value
        End Set
    End Property

    Public Shared Property TreatUnregisteredVendorAsRegisteredVendor As Boolean
        Get
            Return _TreatUnregisteredVendorAsRegisteredVendor
        End Get
        Set(ByVal Value As Boolean)
            _TreatUnregisteredVendorAsRegisteredVendor = Value
        End Set
    End Property
    Public Shared Property ObjVar1() As Object
        Get
            Return _ObjVar1
        End Get
        Set(ByVal Value As Object)
            _ObjVar1 = Value
        End Set
    End Property
    Public Shared Property ObjVar2() As Object
        Get
            Return _ObjVar2
        End Get
        Set(ByVal Value As Object)
            _ObjVar2 = Value
        End Set
    End Property
    Public Shared Property ObjVar3() As Object
        Get
            Return _ObjVar3
        End Get
        Set(ByVal Value As Object)
            _ObjVar3 = Value
        End Set
    End Property
    Public Shared Property ObjVar4() As Object
        Get
            Return _ObjVar4
        End Get
        Set(ByVal Value As Object)
            _ObjVar4 = Value
        End Set
    End Property
    Public Shared Property ObjVar5() As Object
        Get
            Return _ObjVar5
        End Get
        Set(ByVal Value As Object)
            _ObjVar5 = Value
        End Set
    End Property
End Class
