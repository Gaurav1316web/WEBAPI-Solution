' ----------------------------- Changes by Anubhooti 14-July-2014 (BM00000003162) ------------------------- '
Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsEmployeeMaster
#Region "Variables"
    Public EMP_Band_Code As String
    Public Working_City_Code As String
    Public EMP_CODE As String
    Public Emp_Name As String
    Public Designation As String = ""
    Public Add1 As String
    Public Add2 As String
    Public Pin_Code As String
    Public Phone As String
    Public Birth_date As String
    Public Cash As Double
    Public Card_No As String
    Public Joining_date As String
    Public Emp_type As String
    Public ExDate As String
    Public Emp_Status As String
    Public rel_date As String
    Public Payroll_Code As String
    Public Empty_Ex As Double
    Public Created_By As String
    Public Created_Date As String
    Public Modify_By As String
    Public Modify_Date As String
    Public Tot_exp As String
    Public Comp_Code As String
    Public GL_Account As String
    Public EMail_ID As String
    Public SEX As String
    Public MARITAL_STATUS As String
    Public ANNIVERSARY_DATE As DateTime?
    Public DEPARTMENT_CODE As String
    Public SUB_DEPARTMENT_CODE As String
    Public OCCUPATION_CODE As String
    Public DEVISION_CODE As String
    Public GRADE_CODE As String
    Public LOCATION_CODE As String
    Public WORKING_LOCATION_CODE As String
    Public ATTENDANCE_CODE As String
    Public PAYMENT_MODE As String
    Public BANK_ACC_NO As String
    Public BANK_CODE As String
    Public CONFIRMATION_DATE As DateTime?
    Public PROBATION_END_DATE As DateTime?
    Public SHIFT_CODE As String
    Public RELIEVING_DATE As DateTime?
    Public LEAVING_REASON As String
    Public CAST_CATEGORY_CODE As String
    Public RELIGION_CODE As String
    Public PRESENT_COUNTRY_CODE As String
    Public PRESENT_STATE_CODE As String
    Public PRESENT_CITY_CODE As String
    Public PRESENT_MOBILE_NO As String
    Public PERMA_COUNTRY_CODE As String
    Public PERMA_STATE_CODE As String
    Public PERMA_CITY_CODE As String
    Public PERMA_PHONE_NO As String
    Public PERMA_MOBILE_NO As String
    Public PERMA_PIN_CODE As String
    Public PAN_NO As String
    Public PASPORT_NO As String
    Public DESCRIPTION As String
    Public SALARY_ACCOUNT_CODE As String
    Public ADVANCE_TO_STAFF As String
    Public Bank_Branch As String
    Public RESINATION_SUBMIT_DATE As Date?
    Public NOTICE_IN_DAYS As Integer
    Public FATHERS_NAME As String
    Public MOTHERS_NAME As String
    Public SPOUSE_NAME As String
    Public ISESI As Boolean
    Public ESI_NO As String
    Public ESI_DISPENSARY As String
    Public ISPF As Boolean
    Public PF_NO As String
    Public PF_NO_DEPT_FILE As String
    Public WARD_CIRCLE As String
    Public ISRESTRICT_PF As Boolean
    Public ISZERO_PENSION As Boolean
    Public ISDIRECTOR As Boolean
    Public ISZERO_PT As Boolean
    Public strFranchiseCode As String
    Public BioMetricEmpID As String
    Public EmpBasisType As String
    '===shivani============================='
    Public Adhar_No As String
    '==============================
    Public HRApplicant_Code As String = String.Empty
    Public Bank_Branch_Name As String = String.Empty
    Public Bank_Name As String = String.Empty
    Dim objEmployeeStatus As New clsEmployeeStatus()

    Public ObjListEmpFamilieDetails As List(Of clsEmpFamilieDetails) = Nothing
    Dim objEmpFamilieDetails As New clsEmpFamilieDetails()

    Public ObjListEmpLangDetails As List(Of clsEmpLanguageDetails) = Nothing
    Dim objEmpLanguageDetails As New clsEmpLanguageDetails()

    Public ObjListEmpQualiDetails As List(Of clsEmpQualiDetails) = Nothing
    Dim objEmpQualiDetails As New clsEmpQualiDetails()

    Public ObjListEmpExpDetails As List(Of clsEmpExpDetails) = Nothing
    Dim objEmpExpDetails As New clsEmpExpDetails()

    Public ObjListEmpDocuments As List(Of clsEmpDocuments) = Nothing
    Dim objEmpDocuments As New clsEmpDocuments()

    Public ObjListEmpAssets As List(Of clsEmpAssets) = Nothing
    Dim objEmpAssets As New clsEmpAssets()

    '' FIELDS OF PJC MODULE
    Public EARNING_CODE As String = ""
    Public UNIT_COST As Decimal = 0
    Public BILLING_RATE As Decimal = 0
    Public APPLY_ALL_CUST As Boolean = False
    Public USER_CODE As String = ""
    Public COMMENTS As String = ""
    Public isPJCModule As Boolean = False
    ''' new columns for kdil and Viney
    Public CONV_TYPE As String
    Public EMPLOYMENT_NATURE As String
    Public IS_OT_APPL As Boolean
    Public IS_OD_APPL As Boolean
    Public DISPLAY_IN_STATUTORY As Boolean
    Public MINIMUM_BASIC_SALARY As Decimal = 0
    Public VENDOR_CODE As String = ""
    Public AGENCY_CODE As String = ""
    Public AgeForPension As String = String.Empty

    '' kdil new fields
    '' general tab
    Public BLOOD_GROUP As String
    Public UIN_NO As String
    Public ADD1_TYPE As String
    Public ADD1_VERIFIED As Boolean = False
    Public ADD1_VERIFIED_REMARKS As String
    Public ADD1_TEHSIL As String
    Public ADD1_VILLAGE As String
    Public ADD1_POST_OFFICE As String
    Public ADD1_POLICE_STATION As String
    Public ADD2_TYPE As String
    Public ADD2_VERIFIED As Boolean = False
    Public ADD2_VERIFIED_REMARKS As String
    Public ADD2_TEHSIL As String
    Public ADD2_VILLAGE As String
    Public ADD2_POST_OFFICE As String
    Public ADD2_POLICE_STATION As String
    Public ALTERNATE_EMAIL_ID As String
    Public NO_DUES As String = "0"
    Public FDContactNo As String
    Public Hold_Slary As String = "0"


    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public ObjListBRDetails As List(Of clsEmpCustomerBillingRateDetails) = Nothing
    '===================added by shivani
    Public Pf_Calculation_Type As String
    Public Max_Amount_EPF As Double = 0
    Public EPF_Rate As Decimal = 0

    Public Votercard_No As String
    Public Rationcard_No As String
    Public DL_No As String
    Public SecChequeNoLac1 As String
    Public SecChequeNoRs100 As String
    Public UANNo As String
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select EMP_CODE as [Code],Emp_Name as [Employee Name],Designation,Add1,Add2,Pin_Code as [Pin Code],Phone,Birth_date as [Date Of Birth],Cash,Card_No as [Card No],Joining_date as [Joining Date],Emp_type as [Employee Type],ExDate [Expiry Date],Emp_Status as [Employee Status],rel_date as [Releving Date],Payroll_Code as [Payroll Code],Empty_Ex as [Empty Ex],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],GL_Account as [GL Account],EMail_ID as [Email ID],SEX,MARITAL_STATUS as [Marital Status],ANNIVERSARY_DATE as [Anniversary Date],DEPARTMENT_CODE as [Department Code],OCCUPATION_CODE as [Occupation Code],DEVISION_CODE as [Division Code],GRADE_CODE as [Grade Code],LOCATION_CODE as [Branch Code],ATTENDANCE_CODE as [Attandance Code],PAYMENT_MODE_NEW as [Payment Mode],BANK_ACC_NO as [Bank Account No],BANK_CODE as [Bank Code],CONFIRMATION_DATE as [Confirmation Date],PROBATION_END_DATE as [Probation End Date],SHIFT_CODE as [Shift Code],RELIEVING_DATE as [Relieving Date],LEAVING_REASON as [Leaving Reason],CAST_CATEGORY_CODE as [Cast Category Code],RELIGION_CODE as [Religion Code],PRESENT_COUNTRY_CODE as [Present Country Code],PRESENT_STATE_CODE as [Present State Code,PRESENT_CITY_CODE as [Present City Code],PRESENT_MOBILE_NO as [Present Mobile No],PERMA_COUNTRY_CODE as [Permanent Country Code],PERMA_STATE_CODE as [Permanent State Code],PERMA_CITY_CODE as [Permanent City Code],PERMA_PHONE_NO as [Permanent Phone No],PERMA_MOBILE_NO as [Permanent Mobile No],PERMA_PIN_CODE as [Permanent Pin Code],PAN_NO as [Pan No],PASPORT_NO as [Passport No],DESCRIPTION as [Description],FATHERS_NAME as [Fathers Name],MOTHERS_NAME as [Mothers Name],SPOUSE_NAME as [Spouse Name],ISESI as [Is ESI],ESI_NO as [ESI No],ESI_DISPENSARY as [ESI Dispensary],ISPF as [Is PF],PF_NO as [PF No],PF_NO_DEPT_FILE as [PF No Department File],WARD_CIRCLE as [Ward Circle],ISRESTRICT_PF as [Is Restrict PF],ISZERO_PENSION as [Is Zero Pension],ISDIRECTOR as [Is Director],ISZERO_PT as [Is Zero PT],EARNING_CODE as [Earning Code],UNIT_COST as [Unit Cost],BILLING_RATE as [Billing Rate],APPLY_ALL_CUST as [Apply All Customer],USER_CODE as [User Code],COMMENTS as [Comments],Franchise_Code as [Franchise Code],RESIGNATION_SUBMIT_DATE as [Resignation Submission Date],NOTICE_PERIOD_IN_DAYS as [Notice Period In Days],EMPLOYMENT_NATURE as [Employment Nature],CONV_TYPE as [Conveyance Type],VENDOR_CODE as [Vendor Code],Agency_Code as [Agency Code],ISNULL(HRApplicant_Code,'') AS [HRApplicant Code],Age_For_Pension AS [Age For Pension] from tspl_employee_master "
        str = clsCommon.ShowSelectForm("RPTEMPFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function DeleteData(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        'Try
        isSaved = False

        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If
        isSaved = clsEmpFamilieDetails.DeleteData(strCode, trans)
        isSaved = clsEmpLanguageDetails.DeleteData(strCode, trans)
        isSaved = clsEmpQualiDetails.DeleteData(strCode, trans)
        isSaved = clsEmpExpDetails.DeleteData(strCode, trans)
        isSaved = clsEmpDocuments.DeleteDataAllDoc(strCode, trans)

        Dim qry As String
        qry = "delete from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" + strCode + "'"
        isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        '    Return False
        'End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsEmployeeMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsEmployeeMaster
        Dim obj As clsEmployeeMaster = Nothing
        Dim whrcls As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls = " And TSPL_EMPLOYEE_MASTER.LOCATION_CODE=" + objCommonVar.strCurrUserLocations + ""
        End If
        Dim qry As String = " select TSPL_EMPLOYEE_MASTER.* from TSPL_EMPLOYEE_MASTER where 2=2" + whrcls
        Select Case NavType
            Case NavigatorType.First
                qry += " and EMP_CODE = (select MIN(EMP_CODE) from TSPL_EMPLOYEE_MASTER)"
            Case NavigatorType.Last
                qry += " and EMP_CODE = (select Max(EMP_CODE) from TSPL_EMPLOYEE_MASTER)"
            Case NavigatorType.Next
                qry += " and EMP_CODE = (select Min(EMP_CODE) from TSPL_EMPLOYEE_MASTER where  EMP_CODE > '" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and EMP_CODE = (select Max(EMP_CODE) from TSPL_EMPLOYEE_MASTER where EMP_CODE < '" + strCode + "')"
            Case NavigatorType.Current
                qry += " and EMP_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsEmployeeMaster()
            obj.UANNo = clsCommon.myCstr(dt.Rows(0)("UANNo"))
            obj.Working_City_Code = clsCommon.myCstr(dt.Rows(0)("Working_City_Code"))
            obj.EMP_Band_Code = clsCommon.myCstr(dt.Rows(0)("Employee_BandCode"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.Emp_Name = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
            obj.Designation = clsCommon.myCstr(dt.Rows(0)("Designation"))
            obj.Add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
            obj.Add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
            obj.Pin_Code = clsCommon.myCstr(dt.Rows(0)("Pin_Code"))
            obj.Phone = clsCommon.myCstr(dt.Rows(0)("Phone"))
            obj.Birth_date = clsCommon.myCstr(dt.Rows(0)("Birth_date"))
            obj.Cash = clsCommon.myCdbl(dt.Rows(0)("Cash"))
            obj.Card_No = clsCommon.myCstr(dt.Rows(0)("Card_No"))
            obj.Joining_date = clsCommon.myCstr(dt.Rows(0)("Joining_date"))
            obj.Emp_type = clsCommon.myCstr(dt.Rows(0)("Emp_type"))
            obj.ExDate = clsCommon.myCstr(dt.Rows(0)("ExDate"))
            obj.Emp_Status = clsCommon.myCstr(dt.Rows(0)("Emp_Status"))
            obj.rel_date = clsCommon.myCstr(dt.Rows(0)("rel_date"))
            obj.Payroll_Code = clsCommon.myCstr(dt.Rows(0)("Payroll_Code"))
            obj.Empty_Ex = clsCommon.myCdbl(dt.Rows(0)("Empty_Ex"))
            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
            obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))
            obj.Modify_By = clsCommon.myCstr(dt.Rows(0)("Modify_By"))
            obj.Modify_Date = clsCommon.myCstr(dt.Rows(0)("Modify_Date"))
            obj.strFranchiseCode = clsCommon.myCstr(dt.Rows(0)("Franchise_Code"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.GL_Account = clsCommon.myCstr(dt.Rows(0)("GL_Account"))
            obj.EMail_ID = clsCommon.myCstr(dt.Rows(0)("EMail_ID"))
            obj.SEX = clsCommon.myCstr(dt.Rows(0)("SEX"))
            obj.MARITAL_STATUS = clsCommon.myCstr(dt.Rows(0)("MARITAL_STATUS"))
            If clsCommon.myLen(dt.Rows(0)("RELIEVING_DATE")) > 0 Then
                obj.RELIEVING_DATE = clsCommon.myCstr(dt.Rows(0)("RELIEVING_DATE"))
            End If


            '' CODE REPLACED FROM PEPSI TO XPERT ERP
            obj.SALARY_ACCOUNT_CODE = clsCommon.myCstr(dt.Rows(0)("SALARY_ACCOUNT_CODE"))
            obj.ADVANCE_TO_STAFF = clsCommon.myCstr(dt.Rows(0)("ADVANCE_TO_STAFF"))

            If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("ANNIVERSARY_DATE"))) > 0 Then
                obj.ANNIVERSARY_DATE = clsCommon.GetPrintDate(dt.Rows(0)("ANNIVERSARY_DATE"), "dd/MMM/yyyy")
            Else
                obj.ANNIVERSARY_DATE = Nothing
            End If
            obj.DEPARTMENT_CODE = clsCommon.myCstr(dt.Rows(0)("DEPARTMENT_CODE"))
            obj.SUB_DEPARTMENT_CODE = clsCommon.myCstr(dt.Rows(0)("SUB_DEPARTMENT_CODE"))
            obj.OCCUPATION_CODE = clsCommon.myCstr(dt.Rows(0)("OCCUPATION_CODE"))
            obj.DEVISION_CODE = clsCommon.myCstr(dt.Rows(0)("DEVISION_CODE"))
            obj.GRADE_CODE = clsCommon.myCstr(dt.Rows(0)("GRADE_CODE"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.WORKING_LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("WORKING_LOCATION_CODE"))
            obj.ATTENDANCE_CODE = clsCommon.myCstr(dt.Rows(0)("ATTENDANCE_CODE"))
            obj.PAYMENT_MODE = clsCommon.myCstr(dt.Rows(0)("PAYMENT_MODE_NEW"))
            obj.BANK_ACC_NO = clsCommon.myCstr(dt.Rows(0)("BANK_ACC_NO"))
            obj.BANK_CODE = clsCommon.myCstr(dt.Rows(0)("BANK_CODE"))
            If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("CONFIRMATION_DATE"))) > 0 Then
                obj.CONFIRMATION_DATE = clsCommon.GetPrintDate(dt.Rows(0)("CONFIRMATION_DATE"), "dd/MMM/yyyy")
            Else
                obj.CONFIRMATION_DATE = Nothing
            End If

            If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("PROBATION_END_DATE"))) > 0 Then
                obj.PROBATION_END_DATE = clsCommon.GetPrintDate(dt.Rows(0)("PROBATION_END_DATE"), "dd/MMM/yyyy")
            Else
                obj.PROBATION_END_DATE = Nothing
            End If

            obj.SHIFT_CODE = clsCommon.myCstr(dt.Rows(0)("SHIFT_CODE"))
            If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("RELIEVING_DATE"))) > 0 Then
                obj.RELIEVING_DATE = clsCommon.GetPrintDate(dt.Rows(0)("RELIEVING_DATE"), "dd/MMM/yyyy")
            Else
                obj.RELIEVING_DATE = Nothing
            End If


            obj.LEAVING_REASON = clsCommon.myCstr(dt.Rows(0)("LEAVING_REASON"))
            obj.CAST_CATEGORY_CODE = clsCommon.myCstr(dt.Rows(0)("CAST_CATEGORY_CODE"))
            obj.RELIGION_CODE = clsCommon.myCstr(dt.Rows(0)("RELIGION_CODE"))
            obj.PRESENT_COUNTRY_CODE = clsCommon.myCstr(dt.Rows(0)("PRESENT_COUNTRY_CODE"))
            obj.PRESENT_STATE_CODE = clsCommon.myCstr(dt.Rows(0)("PRESENT_STATE_CODE"))
            obj.PRESENT_CITY_CODE = clsCommon.myCstr(dt.Rows(0)("PRESENT_CITY_CODE"))
            obj.PRESENT_MOBILE_NO = clsCommon.myCstr(dt.Rows(0)("PRESENT_MOBILE_NO"))
            obj.PERMA_COUNTRY_CODE = clsCommon.myCstr(dt.Rows(0)("PERMA_COUNTRY_CODE"))
            obj.PERMA_STATE_CODE = clsCommon.myCstr(dt.Rows(0)("PERMA_STATE_CODE"))
            obj.PERMA_CITY_CODE = clsCommon.myCstr(dt.Rows(0)("PERMA_CITY_CODE"))
            obj.PERMA_PHONE_NO = clsCommon.myCstr(dt.Rows(0)("PERMA_PHONE_NO"))
            obj.PERMA_MOBILE_NO = clsCommon.myCstr(dt.Rows(0)("PERMA_MOBILE_NO"))
            obj.PERMA_PIN_CODE = clsCommon.myCstr(dt.Rows(0)("PERMA_PIN_CODE"))
            obj.PAN_NO = clsCommon.myCstr(dt.Rows(0)("PAN_NO"))
            obj.PASPORT_NO = clsCommon.myCstr(dt.Rows(0)("PASPORT_NO"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))

            obj.FATHERS_NAME = clsCommon.myCstr(dt.Rows(0)("FATHERS_NAME"))
            obj.MOTHERS_NAME = clsCommon.myCstr(dt.Rows(0)("MOTHERS_NAME"))
            obj.SPOUSE_NAME = clsCommon.myCstr(dt.Rows(0)("SPOUSE_NAME"))
            obj.ISESI = clsCommon.myCBool(dt.Rows(0)("ISESI"))
            obj.ESI_NO = clsCommon.myCstr(dt.Rows(0)("ESI_NO"))
            obj.ESI_DISPENSARY = clsCommon.myCstr(dt.Rows(0)("ESI_DISPENSARY"))
            obj.ISPF = clsCommon.myCBool(dt.Rows(0)("ISPF"))
            obj.PF_NO = clsCommon.myCstr(dt.Rows(0)("PF_NO"))
            obj.PF_NO_DEPT_FILE = clsCommon.myCstr(dt.Rows(0)("PF_NO_DEPT_FILE"))
            obj.WARD_CIRCLE = clsCommon.myCstr(dt.Rows(0)("WARD_CIRCLE"))
            obj.ISRESTRICT_PF = clsCommon.myCBool(dt.Rows(0)("ISRESTRICT_PF"))
            obj.ISZERO_PENSION = clsCommon.myCBool(dt.Rows(0)("ISZERO_PENSION"))
            obj.ISDIRECTOR = clsCommon.myCBool(dt.Rows(0)("ISDIRECTOR"))
            obj.ISZERO_PT = clsCommon.myCBool(dt.Rows(0)("ISZERO_PT"))
            If Not IsDBNull(dt.Rows(0)("RESIGNATION_SUBMIT_DATE")) Then
                obj.RESINATION_SUBMIT_DATE = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("RESIGNATION_SUBMIT_DATE")), "dd/MMM/yyyy")
            End If
            obj.NOTICE_IN_DAYS = CInt(clsCommon.myCdbl(dt.Rows(0)("NOTICE_PERIOD_IN_DAYS")))
            '' pjc module fields
            obj.EARNING_CODE = clsCommon.myCstr(dt.Rows(0)("EARNING_CODE"))
            obj.UNIT_COST = clsCommon.myCdbl(dt.Rows(0)("UNIT_COST"))
            obj.BILLING_RATE = clsCommon.myCdbl(dt.Rows(0)("BILLING_RATE"))
            obj.USER_CODE = clsCommon.myCstr(dt.Rows(0)("USER_CODE"))
            obj.COMMENTS = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))
            obj.Hold_Slary = clsCommon.myCstr(dt.Rows(0)("Hold_Salary"))
            If IsDBNull(dt.Rows(0)("APPLY_ALL_CUST")) = True Then
                obj.APPLY_ALL_CUST = False
            Else
                obj.APPLY_ALL_CUST = dt.Rows(0)("APPLY_ALL_CUST")
            End If

            '' end pjc module fields
            obj.ObjListEmpFamilieDetails = clsEmpFamilieDetails.GetData(obj.EMP_CODE, trans)
            obj.ObjListEmpLangDetails = clsEmpLanguageDetails.GetData(obj.EMP_CODE, trans)
            obj.ObjListEmpQualiDetails = clsEmpQualiDetails.GetData(obj.EMP_CODE, trans)
            obj.ObjListEmpExpDetails = clsEmpExpDetails.GetData(obj.EMP_CODE, trans)
            obj.ObjListEmpDocuments = clsEmpDocuments.GetDataForGrid(obj.EMP_CODE, trans)
            obj.ObjListEmpAssets = clsEmpAssets.GetDataForGrid(obj.EMP_CODE, trans)

            '' kdil and viney 
            obj.CONV_TYPE = clsCommon.myCstr(dt.Rows(0)("CONV_TYPE"))
            obj.EMPLOYMENT_NATURE = clsCommon.myCstr(dt.Rows(0)("EMPLOYMENT_NATURE"))
            obj.IS_OT_APPL = clsCommon.myCBool(dt.Rows(0)("IS_OT_APPL"))
            obj.IS_OD_APPL = clsCommon.myCBool(dt.Rows(0)("IS_OD_APPL"))
            obj.DISPLAY_IN_STATUTORY = clsCommon.myCBool(dt.Rows(0)("DISPLAY_IN_STATUTORY"))
            obj.MINIMUM_BASIC_SALARY = clsCommon.myCdbl(dt.Rows(0)("MINIMUM_BASIC_SALARY"))
            obj.VENDOR_CODE = clsCommon.myCstr(dt.Rows(0)("VENDOR_CODE"))
            obj.AGENCY_CODE = clsCommon.myCstr(dt.Rows(0)("AGENCY_CODE"))
            obj.AgeForPension = clsCommon.myCdbl(dt.Rows(0)("Age_For_Pension"))
            '==
            obj.Bank_Branch = clsCommon.myCstr(dt.Rows(0)("Bank_Branch"))
            '' kdil new dated - 10/dec/2014
            obj.BLOOD_GROUP = clsCommon.myCstr(dt.Rows(0)("BLOOD_GROUP"))
            obj.UIN_NO = clsCommon.myCstr(dt.Rows(0)("UIN_NO"))
            obj.ADD1_TYPE = clsCommon.myCstr(dt.Rows(0)("ADD1_TYPE"))
            obj.ADD1_VERIFIED = IIf(clsCommon.myCdbl(dt.Rows(0)("ADD1_VERIFIED")) = 1, True, False)
            obj.ADD1_VERIFIED_REMARKS = clsCommon.myCstr(dt.Rows(0)("ADD1_VERIFIED_REMARKS"))
            obj.ADD1_TEHSIL = clsCommon.myCstr(dt.Rows(0)("ADD1_TEHSIL"))
            obj.ADD1_VILLAGE = clsCommon.myCstr(dt.Rows(0)("ADD1_VILLAGE"))
            obj.ADD1_POST_OFFICE = clsCommon.myCstr(dt.Rows(0)("ADD1_POST_OFFICE"))
            obj.ADD1_POLICE_STATION = clsCommon.myCstr(dt.Rows(0)("ADD1_POLICE_STATION"))
            obj.ADD2_TYPE = clsCommon.myCstr(dt.Rows(0)("ADD2_TYPE"))
            obj.ADD2_VERIFIED = IIf(clsCommon.myCdbl(dt.Rows(0)("ADD2_VERIFIED")) = 1, True, False)
            obj.ADD2_VERIFIED_REMARKS = clsCommon.myCstr(dt.Rows(0)("ADD2_VERIFIED_REMARKS"))
            obj.ADD2_TEHSIL = clsCommon.myCstr(dt.Rows(0)("ADD2_TEHSIL"))
            obj.ADD2_VILLAGE = clsCommon.myCstr(dt.Rows(0)("ADD2_VILLAGE"))
            obj.ADD2_POST_OFFICE = clsCommon.myCstr(dt.Rows(0)("ADD2_POST_OFFICE"))
            obj.ADD2_POLICE_STATION = clsCommon.myCstr(dt.Rows(0)("ADD2_POLICE_STATION"))
            obj.ALTERNATE_EMAIL_ID = clsCommon.myCstr(dt.Rows(0)("ALTERNATE_EMAIL_ID"))
            obj.NO_DUES = clsCommon.myCstr(dt.Rows(0)("NO_DUES"))
            obj.HRApplicant_Code = clsCommon.myCstr(dt.Rows(0)("HRApplicant_Code"))
            obj.Bank_Branch_Name = clsCommon.myCstr(dt.Rows(0)("Bank_Branch_Name"))
            obj.Bank_Name = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
            obj.Adhar_No = clsCommon.myCstr(dt.Rows(0)("Adhar_No"))
            '============
            obj.Pf_Calculation_Type = clsCommon.myCstr(dt.Rows(0)("Pf_Calculation_Type"))
            obj.Max_Amount_EPF = clsCommon.myCdbl(dt.Rows(0)("Max_Amount_EPF"))
            obj.EPF_Rate = clsCommon.myCdbl(dt.Rows(0)("EPF_Rate"))


            obj.Votercard_No = clsCommon.myCstr(dt.Rows(0)("Votercard_No"))
            obj.Rationcard_No = clsCommon.myCstr(dt.Rows(0)("RationCard_No"))
            obj.DL_No = clsCommon.myCstr(dt.Rows(0)("DL_No"))
            obj.BioMetricEmpID = clsCommon.myCstr(dt.Rows(0)("BioMetricEmpID"))
            obj.EmpBasisType = clsCommon.myCstr(dt.Rows(0)("EmpBasisType"))
            obj.SecChequeNoLac1 = clsCommon.myCstr(dt.Rows(0)("SecChequeNoLac1"))
            obj.SecChequeNoRs100 = clsCommon.myCstr(dt.Rows(0)("SecChequeNoRs100"))
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsEmployeeMaster, ByVal isNewEntry As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim CreateEmpCodeAsPerEmployeeBasisType = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateEmpCodeAsPerEmployeeBasisType, clsFixedParameterCode.CreateEmpCodeAsPerEmployeeBasisType, Nothing)) = "1", True, False)
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            If Not obj.strFranchiseCode = "" Then
                clsCommon.AddColumnsForChange(coll, "Franchise_Code", obj.strFranchiseCode)
            End If
            clsCommon.AddColumnsForChange(coll, "Working_City_Code", obj.Working_City_Code, True)
            clsCommon.AddColumnsForChange(coll, "Employee_BandCode", obj.EMP_Band_Code, True)
            clsCommon.AddColumnsForChange(coll, "Emp_Name", obj.Emp_Name)
            clsCommon.AddColumnsForChange(coll, "Designation", obj.Designation, True)
            clsCommon.AddColumnsForChange(coll, "Add1", obj.Add1)
            clsCommon.AddColumnsForChange(coll, "Add2", obj.Add2)
            clsCommon.AddColumnsForChange(coll, "Pin_Code", obj.Pin_Code)
            clsCommon.AddColumnsForChange(coll, "Phone", obj.Phone)
            clsCommon.AddColumnsForChange(coll, "Birth_date", obj.Birth_date)
            'clsCommon.AddColumnsForChange(coll, "dob", clsCommon.GetPrintDate(obj.Birth_date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Cash", obj.Cash)
            clsCommon.AddColumnsForChange(coll, "Card_No", obj.Card_No)
            clsCommon.AddColumnsForChange(coll, "Joining_date", obj.Joining_date)
            clsCommon.AddColumnsForChange(coll, "Emp_type", obj.Emp_type)
            clsCommon.AddColumnsForChange(coll, "ExDate", obj.ExDate)
            clsCommon.AddColumnsForChange(coll, "Emp_Status", obj.Emp_Status)
            clsCommon.AddColumnsForChange(coll, "rel_date", obj.rel_date)
            clsCommon.AddColumnsForChange(coll, "Payroll_Code", obj.Payroll_Code)
            clsCommon.AddColumnsForChange(coll, "Empty_Ex", obj.Empty_Ex)

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "GL_Account", obj.GL_Account)
            clsCommon.AddColumnsForChange(coll, "EMail_ID", obj.EMail_ID)
            clsCommon.AddColumnsForChange(coll, "SEX", obj.SEX)
            clsCommon.AddColumnsForChange(coll, "MARITAL_STATUS", obj.MARITAL_STATUS)

            '' PANCH RAJ 14-OCT-2014 CODE FROM PEPSI TO XPERT
            clsCommon.AddColumnsForChange(coll, "SALARY_ACCOUNT_CODE", obj.SALARY_ACCOUNT_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ADVANCE_TO_STAFF", obj.ADVANCE_TO_STAFF, True)

            If obj.ANNIVERSARY_DATE Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "ANNIVERSARY_DATE", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "ANNIVERSARY_DATE", clsCommon.GetPrintDate(obj.ANNIVERSARY_DATE, "dd/MMM/yyyy"), True)
            End If

            clsCommon.AddColumnsForChange(coll, "DEPARTMENT_CODE", obj.DEPARTMENT_CODE, True)
            clsCommon.AddColumnsForChange(coll, "SUB_DEPARTMENT_CODE", obj.SUB_DEPARTMENT_CODE, True)
            clsCommon.AddColumnsForChange(coll, "OCCUPATION_CODE", obj.OCCUPATION_CODE, True)
            clsCommon.AddColumnsForChange(coll, "DEVISION_CODE", obj.DEVISION_CODE, True)
            clsCommon.AddColumnsForChange(coll, "GRADE_CODE", obj.GRADE_CODE, True)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE, True)
            clsCommon.AddColumnsForChange(coll, "WORKING_LOCATION_CODE", obj.WORKING_LOCATION_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ATTENDANCE_CODE", obj.ATTENDANCE_CODE, True)
            clsCommon.AddColumnsForChange(coll, "PAYMENT_MODE_NEW", obj.PAYMENT_MODE, True)
            clsCommon.AddColumnsForChange(coll, "BANK_ACC_NO", obj.BANK_ACC_NO)
            clsCommon.AddColumnsForChange(coll, "BANK_CODE", obj.BANK_CODE, True)

            If obj.CONFIRMATION_DATE Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "CONFIRMATION_DATE", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "CONFIRMATION_DATE", clsCommon.GetPrintDate(obj.CONFIRMATION_DATE, "dd/MMM/yyyy"), True)
            End If
            If obj.RESINATION_SUBMIT_DATE Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "RESIGNATION_SUBMIT_DATE", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "RESIGNATION_SUBMIT_DATE", clsCommon.GetPrintDate(obj.RESINATION_SUBMIT_DATE, "dd/MMM/yyyy"), True)
            End If

            clsCommon.AddColumnsForChange(coll, "NOTICE_PERIOD_IN_DAYS", clsCommon.myCdbl(obj.NOTICE_IN_DAYS))
            'clsCommon.AddColumnsForChange(coll, "LEAVING_REASON", clsCommon.myCstr(obj.LEAVING_REASON), True)
            If obj.PROBATION_END_DATE Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "PROBATION_END_DATE", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "PROBATION_END_DATE", clsCommon.GetPrintDate(obj.PROBATION_END_DATE, "dd/MMM/yyyy"), True)
            End If

            clsCommon.AddColumnsForChange(coll, "SHIFT_CODE", obj.SHIFT_CODE, True)
            If obj.RELIEVING_DATE Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "RELIEVING_DATE", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "RELIEVING_DATE", clsCommon.GetPrintDate(obj.RELIEVING_DATE, "dd/MMM/yyyy"), True)
            End If

            clsCommon.AddColumnsForChange(coll, "LEAVING_REASON", obj.LEAVING_REASON)
            clsCommon.AddColumnsForChange(coll, "CAST_CATEGORY_CODE", obj.CAST_CATEGORY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "RELIGION_CODE", obj.RELIGION_CODE, True)
            clsCommon.AddColumnsForChange(coll, "PRESENT_COUNTRY_CODE", obj.PRESENT_COUNTRY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "PRESENT_STATE_CODE", obj.PRESENT_STATE_CODE, True)
            clsCommon.AddColumnsForChange(coll, "PRESENT_CITY_CODE", obj.PRESENT_CITY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "PRESENT_MOBILE_NO", obj.PRESENT_MOBILE_NO, True)
            clsCommon.AddColumnsForChange(coll, "PERMA_COUNTRY_CODE", obj.PERMA_COUNTRY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "PERMA_STATE_CODE", obj.PERMA_STATE_CODE, True)
            clsCommon.AddColumnsForChange(coll, "PERMA_CITY_CODE", obj.PERMA_CITY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "PERMA_PHONE_NO", obj.PERMA_PHONE_NO)
            clsCommon.AddColumnsForChange(coll, "PERMA_MOBILE_NO", obj.PERMA_MOBILE_NO)
            clsCommon.AddColumnsForChange(coll, "PERMA_PIN_CODE", obj.PERMA_PIN_CODE)
            clsCommon.AddColumnsForChange(coll, "PAN_NO ", obj.PAN_NO)
            clsCommon.AddColumnsForChange(coll, "PASPORT_NO", obj.PASPORT_NO)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)

            clsCommon.AddColumnsForChange(coll, "FATHERS_NAME", obj.FATHERS_NAME)
            clsCommon.AddColumnsForChange(coll, "MOTHERS_NAME", obj.MOTHERS_NAME)
            clsCommon.AddColumnsForChange(coll, "SPOUSE_NAME", obj.SPOUSE_NAME)
            clsCommon.AddColumnsForChange(coll, "ISESI", obj.ISESI)
            clsCommon.AddColumnsForChange(coll, "ESI_NO", obj.ESI_NO)
            clsCommon.AddColumnsForChange(coll, "ESI_DISPENSARY", obj.ESI_DISPENSARY)
            clsCommon.AddColumnsForChange(coll, "ISPF", obj.ISPF)
            clsCommon.AddColumnsForChange(coll, "PF_NO", obj.PF_NO)
            clsCommon.AddColumnsForChange(coll, "PF_NO_DEPT_FILE", obj.PF_NO_DEPT_FILE)
            clsCommon.AddColumnsForChange(coll, "WARD_CIRCLE", obj.WARD_CIRCLE)
            clsCommon.AddColumnsForChange(coll, "ISRESTRICT_PF", obj.ISRESTRICT_PF)
            clsCommon.AddColumnsForChange(coll, "ISZERO_PENSION", obj.ISZERO_PENSION)
            clsCommon.AddColumnsForChange(coll, "ISDIRECTOR", obj.ISDIRECTOR)
            clsCommon.AddColumnsForChange(coll, "ISZERO_PT", obj.ISZERO_PT)

            '' PJC MODULE FIELDS
            clsCommon.AddColumnsForChange(coll, "EARNING_CODE", obj.EARNING_CODE, True)
            clsCommon.AddColumnsForChange(coll, "UNIT_COST", obj.UNIT_COST)
            clsCommon.AddColumnsForChange(coll, "BILLING_RATE", obj.BILLING_RATE)
            clsCommon.AddColumnsForChange(coll, "APPLY_ALL_CUST", obj.APPLY_ALL_CUST)
            clsCommon.AddColumnsForChange(coll, "USER_CODE", obj.USER_CODE, True)
            clsCommon.AddColumnsForChange(coll, "COMMENTS", obj.COMMENTS, True)
            ''END PJC MODULE FIELDS

            '' kdil and viney
            clsCommon.AddColumnsForChange(coll, "CONV_TYPE", obj.CONV_TYPE)
            clsCommon.AddColumnsForChange(coll, "EMPLOYMENT_NATURE", obj.EMPLOYMENT_NATURE)
            clsCommon.AddColumnsForChange(coll, "IS_OT_APPL", obj.IS_OT_APPL)
            clsCommon.AddColumnsForChange(coll, "IS_OD_APPL", obj.IS_OD_APPL)
            clsCommon.AddColumnsForChange(coll, "DISPLAY_IN_STATUTORY", obj.DISPLAY_IN_STATUTORY)
            clsCommon.AddColumnsForChange(coll, "MINIMUM_BASIC_SALARY", obj.MINIMUM_BASIC_SALARY)
            clsCommon.AddColumnsForChange(coll, "VENDOR_CODE", obj.VENDOR_CODE, True)
            clsCommon.AddColumnsForChange(coll, "AGENCY_CODE", obj.AGENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Age_For_Pension", obj.AgeForPension, True)
            clsCommon.AddColumnsForChange(coll, "Bank_Branch", obj.Bank_Branch)
            '' end kdil and viney

            '' kdil new dated - 10/dec/2014
            clsCommon.AddColumnsForChange(coll, "BLOOD_GROUP", obj.BLOOD_GROUP, True)
            clsCommon.AddColumnsForChange(coll, "UIN_NO", obj.UIN_NO, True)
            clsCommon.AddColumnsForChange(coll, "ADD1_TYPE", obj.ADD1_TYPE, True)
            clsCommon.AddColumnsForChange(coll, "ADD1_VERIFIED", obj.ADD1_VERIFIED)
            clsCommon.AddColumnsForChange(coll, "ADD1_VERIFIED_REMARKS", obj.ADD1_VERIFIED_REMARKS)
            clsCommon.AddColumnsForChange(coll, "ADD1_TEHSIL", obj.ADD1_TEHSIL, True)
            clsCommon.AddColumnsForChange(coll, "ADD1_VILLAGE", obj.ADD1_VILLAGE, True)
            clsCommon.AddColumnsForChange(coll, "ADD1_POST_OFFICE", obj.ADD1_POST_OFFICE, True)
            clsCommon.AddColumnsForChange(coll, "ADD1_POLICE_STATION", obj.ADD1_POLICE_STATION, True)
            clsCommon.AddColumnsForChange(coll, "ADD2_TYPE", obj.ADD2_TYPE, True)
            clsCommon.AddColumnsForChange(coll, "ADD2_VERIFIED", obj.ADD2_VERIFIED)
            clsCommon.AddColumnsForChange(coll, "ADD2_VERIFIED_REMARKS", obj.ADD2_VERIFIED_REMARKS, True)
            clsCommon.AddColumnsForChange(coll, "ADD2_TEHSIL", obj.ADD2_TEHSIL, True)
            clsCommon.AddColumnsForChange(coll, "ADD2_VILLAGE", obj.ADD2_VILLAGE, True)
            clsCommon.AddColumnsForChange(coll, "ADD2_POST_OFFICE", obj.ADD2_POST_OFFICE, True)
            clsCommon.AddColumnsForChange(coll, "ADD2_POLICE_STATION", obj.ADD2_POLICE_STATION, True)
            clsCommon.AddColumnsForChange(coll, "ALTERNATE_EMAIL_ID", obj.ALTERNATE_EMAIL_ID, True)
            clsCommon.AddColumnsForChange(coll, "NO_DUES", obj.NO_DUES)
            clsCommon.AddColumnsForChange(coll, "HRApplicant_Code", obj.HRApplicant_Code, True)
            clsCommon.AddColumnsForChange(coll, "Bank_Branch_Name", obj.Bank_Branch_Name)
            clsCommon.AddColumnsForChange(coll, "Bank_Name", obj.Bank_Name)
            clsCommon.AddColumnsForChange(coll, "hold_salary", obj.Hold_Slary)
            clsCommon.AddColumnsForChange(coll, "Adhar_No", obj.Adhar_No)
            '===
            clsCommon.AddColumnsForChange(coll, "Pf_Calculation_Type", obj.Pf_Calculation_Type)
            clsCommon.AddColumnsForChange(coll, "Max_Amount_EPF", obj.Max_Amount_EPF)
            clsCommon.AddColumnsForChange(coll, "EPF_Rate", obj.EPF_Rate)
            clsCommon.AddColumnsForChange(coll, "UANNo", obj.UANNo, True)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))


            clsCommon.AddColumnsForChange(coll, "Votercard_No", obj.Votercard_No)
            clsCommon.AddColumnsForChange(coll, "RationCard_No", obj.Rationcard_No)
            clsCommon.AddColumnsForChange(coll, "DL_No", obj.DL_No)
            clsCommon.AddColumnsForChange(coll, "BioMetricEmpID", obj.BioMetricEmpID)
            clsCommon.AddColumnsForChange(coll, "EmpBasisType", obj.EmpBasisType)
            clsCommon.AddColumnsForChange(coll, "SecChequeNoLac1", obj.SecChequeNoLac1)
            clsCommon.AddColumnsForChange(coll, "SecChequeNoRs100", obj.SecChequeNoRs100)
            If isNewEntry Then
                If clsCommon.myLen(obj.EMP_CODE) <= 0 Then
                    'Ticket No- BHA/24/09/18-000564 Emp code as per employee type
                    If CreateEmpCodeAsPerEmployeeBasisType = True Then
                        If obj.EmpBasisType = "PB" Then
                            obj.EMP_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.Employee_Master, clsDocTransactionType.PermanentBasis, "")
                        ElseIf obj.EmpBasisType = "CB" Then
                            obj.EMP_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.Employee_Master, clsDocTransactionType.ContractBasis, "")
                        ElseIf obj.EmpBasisType = "DB" Then
                            obj.EMP_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.Employee_Master, clsDocTransactionType.DailyBasis, "")
                        End If
                    Else
                        obj.EMP_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.Employee_Master, clsDocTransactionType.All, "")
                    End If
                    If clsCommon.myLen(obj.EMP_CODE) <= 0 Then
                        Throw New Exception("Error in Code Genration")
                    End If
                End If

                clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_EMPLOYEE_MASTER where EMP_CODE= '" & obj.EMP_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_MASTER", OMInsertOrUpdate.Update, "EMP_CODE='" + obj.EMP_CODE + "'", trans)
            End If
            If obj.isPJCModule = False Then
                'Dim qry_Count As String = "SELECT Count(*) FROM TSPL_EMPLOYEE_STATUS where EMP_CODE= '" & obj.EMP_CODE & "'"
                'Dim check_Exist As Integer = clsDBFuncationality.getSingleValue(qry_Count, trans)
                'If check_Exist = 0 Then
                '    isSaved = objEmployeeStatus.SaveData_FromEmpMaster(obj, isNewEntry)
                'End If
                isSaved = objEmployeeStatus.SaveData_FromEmpMaster(obj, trans)
                Try
                    isSaved = objEmpFamilieDetails.SaveData(obj.EMP_CODE, obj.ObjListEmpFamilieDetails)
                    isSaved = objEmpLanguageDetails.SaveData(obj.EMP_CODE, obj.ObjListEmpLangDetails)
                    isSaved = objEmpQualiDetails.SaveData(obj.EMP_CODE, obj.ObjListEmpQualiDetails)
                    isSaved = objEmpExpDetails.SaveData(obj.EMP_CODE, obj.ObjListEmpExpDetails)
                    isSaved = objEmpDocuments.SaveData(obj.EMP_CODE, obj.ObjListEmpDocuments)
                    isSaved = objEmpAssets.SaveData(obj.EMP_CODE, obj.ObjListEmpAssets)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                Dim objCustBRate As New clsEmpCustomerBillingRateDetails
                isSaved = objCustBRate.SaveData(obj.EMP_CODE, obj.ObjListBRDetails, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Function SaveDataFromExcelSheet(ByVal obj As clsEmployeeMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        'Try
        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Emp_Name", obj.Emp_Name)
        If Not obj.strFranchiseCode = "" Then
            clsCommon.AddColumnsForChange(coll, "Franchise_Code", obj.strFranchiseCode)
        End If
        clsCommon.AddColumnsForChange(coll, "Working_City_Code", obj.Working_City_Code, True)
        clsCommon.AddColumnsForChange(coll, "Employee_BandCode", obj.EMP_Band_Code, True)
        clsCommon.AddColumnsForChange(coll, "Designation", obj.Designation, True)
        clsCommon.AddColumnsForChange(coll, "Add1", obj.Add1)
        clsCommon.AddColumnsForChange(coll, "Add2", obj.Add2)
        clsCommon.AddColumnsForChange(coll, "Pin_Code", obj.Pin_Code)
        clsCommon.AddColumnsForChange(coll, "Phone", obj.Phone)
        clsCommon.AddColumnsForChange(coll, "Birth_date", obj.Birth_date)
        'clsCommon.AddColumnsForChange(coll, "dob", clsCommon.GetPrintDate(obj.Birth_date, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Cash", obj.Cash)
        clsCommon.AddColumnsForChange(coll, "Card_No", obj.Card_No)
        clsCommon.AddColumnsForChange(coll, "Joining_date", obj.Joining_date)
        clsCommon.AddColumnsForChange(coll, "Emp_type", obj.Emp_type)
        clsCommon.AddColumnsForChange(coll, "ExDate", obj.ExDate)
        clsCommon.AddColumnsForChange(coll, "Emp_Status", obj.Emp_Status)
        clsCommon.AddColumnsForChange(coll, "rel_date", obj.rel_date)
        clsCommon.AddColumnsForChange(coll, "Payroll_Code", obj.Payroll_Code)
        clsCommon.AddColumnsForChange(coll, "Empty_Ex", obj.Empty_Ex)

        If obj.RESINATION_SUBMIT_DATE Is Nothing Then
        Else
            clsCommon.AddColumnsForChange(coll, "RESIGNATION_SUBMIT_DATE", clsCommon.GetPrintDate(obj.RESINATION_SUBMIT_DATE, "dd/MMM/yyyy"), True)
        End If
        clsCommon.AddColumnsForChange(coll, "NOTICE_PERIOD_IN_DAYS", clsCommon.myCdbl(obj.NOTICE_IN_DAYS))

        If clsCommon.myLen(obj.Comp_Code) <= 0 Then
            obj.Comp_Code = objCommonVar.CurrentCompanyCode
        Else
            obj.Comp_Code = obj.Comp_Code
        End If
        clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.Comp_Code)
        clsCommon.AddColumnsForChange(coll, "GL_Account", obj.GL_Account)
        clsCommon.AddColumnsForChange(coll, "EMail_ID", obj.EMail_ID)
        clsCommon.AddColumnsForChange(coll, "SEX", obj.SEX)
        clsCommon.AddColumnsForChange(coll, "MARITAL_STATUS", obj.MARITAL_STATUS)

        '' PANCHRAJ 14-OCT-2014 
        clsCommon.AddColumnsForChange(coll, "SALARY_ACCOUNT_CODE", obj.SALARY_ACCOUNT_CODE, True)
        clsCommon.AddColumnsForChange(coll, "ADVANCE_TO_STAFF", obj.ADVANCE_TO_STAFF, True)

        If obj.ANNIVERSARY_DATE Is Nothing Then
        Else
            clsCommon.AddColumnsForChange(coll, "ANNIVERSARY_DATE", clsCommon.GetPrintDate(obj.ANNIVERSARY_DATE, "dd/MMM/yyyy"), True)
        End If

        clsCommon.AddColumnsForChange(coll, "DEPARTMENT_CODE", obj.DEPARTMENT_CODE, True)
        clsCommon.AddColumnsForChange(coll, "SUB_DEPARTMENT_CODE", obj.SUB_DEPARTMENT_CODE, True)
        clsCommon.AddColumnsForChange(coll, "OCCUPATION_CODE", obj.OCCUPATION_CODE, True)
        clsCommon.AddColumnsForChange(coll, "DEVISION_CODE", obj.DEVISION_CODE, True)
        clsCommon.AddColumnsForChange(coll, "GRADE_CODE", obj.GRADE_CODE, True)
        clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE, True)
        clsCommon.AddColumnsForChange(coll, "WORKING_LOCATION_CODE", obj.WORKING_LOCATION_CODE, True)
        clsCommon.AddColumnsForChange(coll, "ATTENDANCE_CODE", obj.ATTENDANCE_CODE, True)
        clsCommon.AddColumnsForChange(coll, "PAYMENT_MODE_New", obj.PAYMENT_MODE, True)
        clsCommon.AddColumnsForChange(coll, "BANK_ACC_NO", obj.BANK_ACC_NO)
        clsCommon.AddColumnsForChange(coll, "BANK_CODE", obj.BANK_CODE, True)

        If obj.CONFIRMATION_DATE Is Nothing Then
        Else
            clsCommon.AddColumnsForChange(coll, "CONFIRMATION_DATE", clsCommon.GetPrintDate(obj.CONFIRMATION_DATE, "dd/MMM/yyyy"), True)
        End If

        If obj.PROBATION_END_DATE Is Nothing Then
        Else
            clsCommon.AddColumnsForChange(coll, "PROBATION_END_DATE", clsCommon.GetPrintDate(obj.PROBATION_END_DATE, "dd/MMM/yyyy"), True)
        End If

        clsCommon.AddColumnsForChange(coll, "SHIFT_CODE", obj.SHIFT_CODE, True)
        If obj.RELIEVING_DATE Is Nothing Then
            clsCommon.AddColumnsForChange(coll, "RELIEVING_DATE", "", True)
        Else
            clsCommon.AddColumnsForChange(coll, "RELIEVING_DATE", clsCommon.GetPrintDate(obj.RELIEVING_DATE, "dd/MMM/yyyy"), True)
        End If

        clsCommon.AddColumnsForChange(coll, "LEAVING_REASON", obj.LEAVING_REASON)
        clsCommon.AddColumnsForChange(coll, "CAST_CATEGORY_CODE", obj.CAST_CATEGORY_CODE, True)
        clsCommon.AddColumnsForChange(coll, "RELIGION_CODE", obj.RELIGION_CODE, True)
        clsCommon.AddColumnsForChange(coll, "PRESENT_COUNTRY_CODE", obj.PRESENT_COUNTRY_CODE, True)
        clsCommon.AddColumnsForChange(coll, "PRESENT_STATE_CODE", obj.PRESENT_STATE_CODE, True)
        clsCommon.AddColumnsForChange(coll, "PRESENT_CITY_CODE", obj.PRESENT_CITY_CODE, True)
        clsCommon.AddColumnsForChange(coll, "PRESENT_MOBILE_NO", obj.PRESENT_MOBILE_NO, True)
        clsCommon.AddColumnsForChange(coll, "PERMA_COUNTRY_CODE", obj.PERMA_COUNTRY_CODE, True)
        clsCommon.AddColumnsForChange(coll, "PERMA_STATE_CODE", obj.PERMA_STATE_CODE, True)
        clsCommon.AddColumnsForChange(coll, "PERMA_CITY_CODE", obj.PERMA_CITY_CODE, True)
        clsCommon.AddColumnsForChange(coll, "PERMA_PHONE_NO", obj.PERMA_PHONE_NO)
        clsCommon.AddColumnsForChange(coll, "PERMA_MOBILE_NO", obj.PERMA_MOBILE_NO)
        clsCommon.AddColumnsForChange(coll, "PERMA_PIN_CODE", obj.PERMA_PIN_CODE)
        clsCommon.AddColumnsForChange(coll, "PAN_NO ", obj.PAN_NO)
        clsCommon.AddColumnsForChange(coll, "PASPORT_NO", obj.PASPORT_NO)
        clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)

        clsCommon.AddColumnsForChange(coll, "FATHERS_NAME", obj.FATHERS_NAME)
        clsCommon.AddColumnsForChange(coll, "MOTHERS_NAME", obj.MOTHERS_NAME)
        clsCommon.AddColumnsForChange(coll, "SPOUSE_NAME", obj.SPOUSE_NAME)
        clsCommon.AddColumnsForChange(coll, "ISESI", obj.ISESI)
        clsCommon.AddColumnsForChange(coll, "ESI_NO", obj.ESI_NO)
        clsCommon.AddColumnsForChange(coll, "ESI_DISPENSARY", obj.ESI_DISPENSARY)
        clsCommon.AddColumnsForChange(coll, "ISPF", obj.ISPF)
        clsCommon.AddColumnsForChange(coll, "PF_NO", obj.PF_NO)
        clsCommon.AddColumnsForChange(coll, "PF_NO_DEPT_FILE", obj.PF_NO_DEPT_FILE)
        clsCommon.AddColumnsForChange(coll, "WARD_CIRCLE", obj.WARD_CIRCLE)
        clsCommon.AddColumnsForChange(coll, "ISRESTRICT_PF", obj.ISRESTRICT_PF)
        clsCommon.AddColumnsForChange(coll, "ISZERO_PENSION", obj.ISZERO_PENSION)
        clsCommon.AddColumnsForChange(coll, "ISDIRECTOR", obj.ISDIRECTOR)
        clsCommon.AddColumnsForChange(coll, "ISZERO_PT", obj.ISZERO_PT)

        '' kdil and viney
        clsCommon.AddColumnsForChange(coll, "CONV_TYPE", obj.CONV_TYPE)
        clsCommon.AddColumnsForChange(coll, "EMPLOYMENT_NATURE", obj.EMPLOYMENT_NATURE)
        clsCommon.AddColumnsForChange(coll, "IS_OT_APPL", obj.IS_OT_APPL)
        clsCommon.AddColumnsForChange(coll, "IS_OD_APPL", obj.IS_OD_APPL)
        clsCommon.AddColumnsForChange(coll, "DISPLAY_IN_STATUTORY", obj.DISPLAY_IN_STATUTORY)
        clsCommon.AddColumnsForChange(coll, "MINIMUM_BASIC_SALARY", obj.MINIMUM_BASIC_SALARY)
        clsCommon.AddColumnsForChange(coll, "VENDOR_CODE", obj.VENDOR_CODE, True)
        clsCommon.AddColumnsForChange(coll, "AGENCY_CODE", obj.AGENCY_CODE, True)
        clsCommon.AddColumnsForChange(coll, "Age_For_Pension", obj.AgeForPension, True)
        clsCommon.AddColumnsForChange(coll, "USER_CODE", obj.USER_CODE, True)
        clsCommon.AddColumnsForChange(coll, "Bank_Branch", obj.Bank_Branch)
        '' end kdil and viney

        '' kdil new dated - 10/dec/2014
        clsCommon.AddColumnsForChange(coll, "BLOOD_GROUP", obj.BLOOD_GROUP, True)
        clsCommon.AddColumnsForChange(coll, "UIN_NO", obj.UIN_NO, True)
        clsCommon.AddColumnsForChange(coll, "ADD1_TYPE", obj.ADD1_TYPE, True)
        clsCommon.AddColumnsForChange(coll, "ADD1_VERIFIED", obj.ADD1_VERIFIED)
        clsCommon.AddColumnsForChange(coll, "ADD1_VERIFIED_REMARKS", obj.ADD1_VERIFIED_REMARKS)
        clsCommon.AddColumnsForChange(coll, "ADD1_TEHSIL", obj.ADD1_TEHSIL, True)
        clsCommon.AddColumnsForChange(coll, "ADD1_VILLAGE", obj.ADD1_VILLAGE, True)
        clsCommon.AddColumnsForChange(coll, "ADD1_POST_OFFICE", obj.ADD1_POST_OFFICE, True)
        clsCommon.AddColumnsForChange(coll, "ADD1_POLICE_STATION", obj.ADD1_POLICE_STATION, True)
        clsCommon.AddColumnsForChange(coll, "ADD2_TYPE", obj.ADD2_TYPE, True)
        clsCommon.AddColumnsForChange(coll, "ADD2_VERIFIED", obj.ADD2_VERIFIED)
        clsCommon.AddColumnsForChange(coll, "ADD2_VERIFIED_REMARKS", obj.ADD2_VERIFIED_REMARKS, True)
        clsCommon.AddColumnsForChange(coll, "ADD2_TEHSIL", obj.ADD2_TEHSIL, True)
        clsCommon.AddColumnsForChange(coll, "ADD2_VILLAGE", obj.ADD2_VILLAGE, True)
        clsCommon.AddColumnsForChange(coll, "ADD2_POST_OFFICE", obj.ADD2_POST_OFFICE, True)
        clsCommon.AddColumnsForChange(coll, "ADD2_POLICE_STATION", obj.ADD2_POLICE_STATION, True)
        clsCommon.AddColumnsForChange(coll, "ALTERNATE_EMAIL_ID", obj.ALTERNATE_EMAIL_ID, True)
        clsCommon.AddColumnsForChange(coll, "NO_DUES", obj.NO_DUES)
        clsCommon.AddColumnsForChange(coll, "Bank_Branch_Name", obj.Bank_Branch_Name)
        clsCommon.AddColumnsForChange(coll, "Bank_Name", obj.Bank_Name)
        clsCommon.AddColumnsForChange(coll, "Adhar_No", obj.Adhar_No)
        '====
        clsCommon.AddColumnsForChange(coll, "Pf_Calculation_Type", obj.Pf_Calculation_Type)
        clsCommon.AddColumnsForChange(coll, "Max_Amount_EPF", obj.Max_Amount_EPF)
        clsCommon.AddColumnsForChange(coll, "EPF_Rate", obj.EPF_Rate)
        clsCommon.AddColumnsForChange(coll, "BioMetricEmpID", obj.BioMetricEmpID)
        clsCommon.AddColumnsForChange(coll, "SecChequeNoLac1", obj.SecChequeNoLac1)
        clsCommon.AddColumnsForChange(coll, "SecChequeNoRs100", obj.SecChequeNoRs100)
        clsCommon.AddColumnsForChange(coll, "UANNo", obj.UANNo, True)
        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
        If clsCommon.myLen(obj.EMP_CODE) <= 0 Then
            obj.EMP_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.myCDate(clsCommon.GETSERVERDATE()), clsDocType.Employee_Master, "", "")
            If clsCommon.myLen(obj.EMP_CODE) <= 0 Then
                Throw New Exception("Error in Code Genration")
            End If
        End If
        clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)

        Dim qry As String = "SELECT Count(*) FROM TSPL_EMPLOYEE_MASTER where EMP_CODE= '" & obj.EMP_CODE & "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        If check = 0 Then
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_MASTER", OMInsertOrUpdate.Insert, "")
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_MASTER", OMInsertOrUpdate.Update, "EMP_CODE='" + obj.EMP_CODE + "'")
        End If

        isSaved = objEmployeeStatus.SaveData_FromEmpMaster(obj, Nothing)
        'Dim qry_Count As String = "SELECT Count(*) FROM TSPL_EMPLOYEE_STATUS where EMP_CODE= '" & obj.EMP_CODE & "'"
        'Dim check_Exist As Integer = clsDBFuncationality.getSingleValue(qry_Count)
        'If check_Exist = 0 Then
        '    isSaved = objEmployeeStatus.SaveData_FromEmpMaster(obj, isNewEntry)
        'End If
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
        Return isSaved
    End Function
    Public Shared Function FinderForEmployeeInGL_Segment_Code(ByVal strCode As String, ByVal isButtonClicked As Boolean) As clsEmployeeMaster
        Dim obj As clsEmployeeMaster = Nothing
        Dim qry As String = "select segment_code as [Code],description as [Name] from tspl_Gl_segment_code"
        Dim WhrCls As String = " Seg_no ='4'"
        strCode = clsCommon.ShowSelectForm("EmployeeFinder", qry, "Code", WhrCls, strCode, "segment_code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select segment_code as [Code],description as [Name] from tspl_Gl_segment_code where segment_code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsEmployeeMaster()
                obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Emp_Name = clsCommon.myCstr(dt.Rows(0)("Name"))
                'obj.Designation = clsCommon.myCstr(dt.Rows(0)("Designation"))
            End If
        End If
        Return obj
    End Function

    Public Shared Function FinderForEmployee(ByVal strCode As String, ByVal isButtonClicked As Boolean, Optional ByVal WhrCls As String = "") As clsEmployeeMaster
        Dim obj As clsEmployeeMaster = Nothing
        Dim qry As String = "select EMP_CODE as Code,Emp_Name as Name,Designation  from TSPL_EMPLOYEE_MASTER"
        'Dim WhrCls As String = ""
        strCode = clsCommon.ShowSelectForm("EmployeeFinder", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select EMP_CODE as Code,Emp_Name as Name,Designation,Phone,EMail_ID  from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsEmployeeMaster()
                obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Emp_Name = clsCommon.myCstr(dt.Rows(0)("Name"))
                obj.Designation = clsCommon.myCstr(dt.Rows(0)("Designation"))
                obj.Phone = clsCommon.myCstr(dt.Rows(0)("Phone"))
                obj.EMail_ID = clsCommon.myCstr(dt.Rows(0)("EMail_ID"))
            End If
        End If
        Return obj
    End Function
    Public Shared Function GetName(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" & strCode & "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetDateofJoining(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Joining_date from TSPL_EMPLOYEE_MASTER where EMP_CODE='" & strCode & "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetDesignationName(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select TSPL_DESIGNATION_MASTER.Designation_Desc from TSPL_EMPLOYEE_MASTER left join TSPL_DESIGNATION_MASTER on TSPL_EMPLOYEE_MASTER.Designation=TSPL_DESIGNATION_MASTER.Designation_id where EMP_CODE='" & strCode & "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetDepartmentName(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "  select TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME from TSPL_EMPLOYEE_MASTER left join TSPL_DEPARTMENT_MASTER on TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE=TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE where EMP_CODE='" & strCode & "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetSubDepartmentName(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "  select TSPL_SUB_DEPARTMENT_MASTER.Description from TSPL_EMPLOYEE_MASTER left join TSPL_SUB_DEPARTMENT_MASTER on TSPL_EMPLOYEE_MASTER.SUB_DEPARTMENT_CODE=TSPL_SUB_DEPARTMENT_MASTER.SUB_DEPARTMENT_CODE where EMP_CODE='" & strCode & "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetWorkingLocationName(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " select TSPL_LOCATION_MASTER.Location_Desc from TSPL_EMPLOYEE_MASTER left join TSPL_LOCATION_MASTER on TSPL_EMPLOYEE_MASTER.LOCATION_CODE=TSPL_LOCATION_MASTER.Location_Code where EMP_CODE='" & strCode & "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetLocationName(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " select TSPL_LOCATION_MASTER.Location_Desc from TSPL_EMPLOYEE_MASTER left join TSPL_LOCATION_MASTER on TSPL_EMPLOYEE_MASTER.LOCATION_CODE=TSPL_LOCATION_MASTER.Location_Code where EMP_CODE='" & strCode & "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function GetLocation(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Location_Code from TSPL_EMPLOYEE_MASTER where EMP_CODE='" & strCode & "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function GetDesignationCodeByDesignationName(ByVal strName As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " Select DESIGNATION_id from TSPL_DESIGNATION_MASTER where DESIGNATION_desc = '" & strName & "' "
        Dim StrCode As String = clsDBFuncationality.getSingleValue(qry, trans)
        Return StrCode
    End Function
    Public Shared Function CheckExistence(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select EMP_CODE from TSPL_EMPLOYEE_MASTER where EMP_CODE='" & strCode & "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function GetEmployeeRegister(ByVal StrLocCode As String, ByVal StrStatus As String, ByVal ArrEmp As ArrayList, ByVal StrDesignationCode As String, ByVal StrDepartmentCode As String) As DataTable
        Dim qry As String = " "

        qry += " SELECT [EMP_CODE] as 'Employee Code',[Emp_Name] as  'Employee Name' ,[FATHERS_NAME] as 'Fathers Name' ,[MOTHERS_NAME] as 'Mothers Name' ,[MARITAL_STATUS] as 'Marital Status' ,[SPOUSE_NAME] as 'Spouse Name' ,CONVERT(VARCHAR,[ANNIVERSARY_DATE],103) as 'Anniversary Date' ,TSPL_DESIGNATION_MASTER.Designation_Desc  as 'Designation',"
        qry += " CONVERT(VARCHAR,[Birth_date],103) as 'Date of Birth' ,[Card_No] as 'Card No' , CONVERT(VARCHAR,[Joining_date],103) as 'Joining Date',[Emp_type] as 'Employee Type', [Emp_Status] as 'Employee Status' ,CONVERT(VARCHAR,[rel_date],103) as 'Releaving Date' ,CONVERT(VARCHAR,[ExDate],103) as 'Sh/Ex Date', [Cash] as 'Cash Sh/Ex', [Payroll_Code] as 'PayRoll Code' ,[Empty_Ex] as 'Empty Ex' ,TSPL_EMPLOYEE_MASTER.Comp_Code  as 'Company Code' , [GL_Account] as 'GL Account' , [SEX] AS 'Gender' ,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME as 'Department',"
        qry += " TSPL_OCCUPATION_MASTER.OCCUPATION_NAME as 'Occupation',TSPL_LOCATION_MASTER.Location_Code as [Location Code],TSPL_DEVISION_MASTER.DEVISION_NAME as 'Devision',TSPL_GRADE_MASTER.GRADE_NAME as 'Grade',TSPL_LOCATION_MASTER.Location_Desc  as 'Location Name',[ATTENDANCE_CODE] as 'Attendance Code' , [PAYMENT_MODE_NEW] as 'Payment Mode' ,TSPL_BANK_MASTER.DESCRIPTION as 'Bank',[Bank_Branch] as 'Bank Branch Name',[BANK_ACC_NO] as 'Bank A/C No.' ,CONVERT(VARCHAR,[CONFIRMATION_DATE],103)  AS 'Confifmation Date' ,CONVERT(VARCHAR,[PROBATION_END_DATE],103) as 'Probation End Date' ,TSPL_SHIFT_MASTER.SHIFT_NAME as 'Shift',CONVERT(VARCHAR,[RELIEVING_DATE],103)  as 'Relieving Date' ,[LEAVING_REASON] as 'Leaving Reason' ,TSPL_CAST_CATEGORY_MASTER.CAST_CATEGORY_NAME  as 'Cast Category',"
        qry += " TSPL_RELIGION_MASTER.RELIGION_NAME as 'Religion',"
        qry += " CONVERT (VARCHAR(MAX),isnull(TSPL_EMPLOYEE_MASTER.[Add1],'')+ isnull((SELECT  City_Name FROM TSPL_CITY_MASTER WHERE City_Code= [PRESENT_CITY_CODE]),'') + ' '+isnull((SELECT STATE_NAME  FROM TSPL_STATE_MASTER WHERE STATE_CODE =[PRESENT_STATE_CODE]),'')+ ' '+isnull((SELECT COUNTRY_NAME  FROM TSPL_COUNTRY_MASTER  WHERE COUNTRY_CODE  =[PRESENT_COUNTRY_CODE]),'')+' ' +isnull(TSPL_EMPLOYEE_MASTER.[Pin_Code],'')) AS 'Present Address'  , "
        qry += " TSPL_EMPLOYEE_MASTER.[Phone] as 'Present Phone No' , [PRESENT_MOBILE_NO] AS 'Present Mobile No',"
        qry += " CONVERT (VARCHAR(MAX),isnull(TSPL_EMPLOYEE_MASTER.[Add2],'')+ ' ' + isnull((SELECT  City_Name FROM TSPL_CITY_MASTER WHERE City_Code= [PERMA_CITY_CODE]),'')+' '+isnull((SELECT STATE_NAME  FROM TSPL_STATE_MASTER WHERE STATE_CODE =[PERMA_STATE_CODE]),'')+' '+isnull((SELECT COUNTRY_NAME  FROM TSPL_COUNTRY_MASTER  WHERE COUNTRY_CODE  =[PERMA_COUNTRY_CODE]),'')+' '+isnull([PERMA_PIN_CODE],'')) as 'Premanent Address',"
        qry += " [PERMA_PHONE_NO] as 'Phone no 2' ,[PERMA_MOBILE_NO] as 'Mobile No 2' ,TSPL_EMPLOYEE_MASTER.[EMail_ID] as 'Email Id' ,[PAN_NO] as 'Pan No' ,[PASPORT_NO] as 'Pasport No' , TSPL_EMPLOYEE_MASTER.[DESCRIPTION] ,[ISESI] as 'Is ESI' ,[ESI_NO] as 'ESI No' ,[ESI_DISPENSARY] as 'ESI Dispensary' ,[ISPF] as 'Is PF' ,TSPL_EMPLOYEE_MASTER.[PF_NO] AS 'PF No' ,[PF_NO_DEPT_FILE] as 'PF No(Dep. File)' ,[WARD_CIRCLE] as 'Ward Circle' ,[ISRESTRICT_PF] as 'Is Restrict PF', [ISZERO_PENSION] AS 'Is Zero Pension' , [ISDIRECTOR] as 'Is Director' ,[ISZERO_PT] as 'Is Zero PT'  
                 ,TSPL_EMPLOYEE_MASTER.Adhar_No as [Aadhaar No] 
                FROM [TSPL_EMPLOYEE_MASTER]"
        qry += " left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id =TSPL_EMPLOYEE_MASTER.Designation "
        qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE  =TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE"
        qry += " left outer join TSPL_OCCUPATION_MASTER on TSPL_OCCUPATION_MASTER.OCCUPATION_CODE =TSPL_EMPLOYEE_MASTER.OCCUPATION_CODE"
        qry += " left outer join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE  =TSPL_EMPLOYEE_MASTER.DEVISION_CODE"
        qry += " left outer join TSPL_GRADE_MASTER on TSPL_GRADE_MASTER.GRADE_CODE   =TSPL_EMPLOYEE_MASTER.GRADE_CODE"
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.LOCATION_CODE    =TSPL_EMPLOYEE_MASTER.LOCATION_CODE"
        qry += " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE     =TSPL_EMPLOYEE_MASTER.BANK_CODE"
        qry += " left outer join TSPL_SHIFT_MASTER on TSPL_SHIFT_MASTER.SHIFT_CODE=TSPL_EMPLOYEE_MASTER.SHIFT_CODE"
        qry += " left outer join TSPL_CAST_CATEGORY_MASTER on TSPL_CAST_CATEGORY_MASTER.CAST_CATEGORY_CODE=TSPL_EMPLOYEE_MASTER.CAST_CATEGORY_CODE"
        qry += " left outer join TSPL_RELIGION_MASTER  on TSPL_RELIGION_MASTER.RELIGION_CODE =TSPL_EMPLOYEE_MASTER.RELIGION_CODE"
        qry += " where 1=1 "
        If clsCommon.myLen(StrLocCode) > 0 Then
            qry += " and TSPL_EMPLOYEE_MASTER.location_code ='" + StrLocCode + "' "
        End If
        If clsCommon.myLen(StrDesignationCode) > 0 Then
            qry += " and TSPL_EMPLOYEE_MASTER.Designation ='" + StrDesignationCode + "' "
        End If
        If clsCommon.myLen(StrDepartmentCode) > 0 Then
            qry += " and TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE ='" + StrDepartmentCode + "' "
        End If
        If clsCommon.CompairString(StrStatus, "Active") = CompairStringResult.Equal OrElse clsCommon.CompairString(StrStatus, "InActive") = CompairStringResult.Equal Then
            qry += " and TSPL_EMPLOYEE_MASTER.EMP_Status ='" + StrStatus + "' "
        End If
        If ArrEmp IsNot Nothing AndAlso ArrEmp.Count > 0 Then
            qry += " and TSPL_EMPLOYEE_MASTER.Emp_Code in (" + clsCommon.GetMulcallString(ArrEmp) + ") "
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Public Shared Function CheckMappedUser(ByVal UserCode As String, ByVal ExcludeEmpCode As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim strq As String
        strq = "select EMP_CODE from TSPL_EMPLOYEE_MASTER where USER_CODE='" & UserCode & "' and EMP_CODE<> '" & ExcludeEmpCode & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("EMP_CODE")
        Else
            Return ""
        End If
    End Function
    Public Shared Function CheckRejoinEmployee(ByVal FATHERS_NAME As String, ByVal Birth_date As String, ByVal PRESENT_MOBILE_NO As String, ByVal EMail_ID As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim strq As String
        If clsCommon.myLen(FATHERS_NAME) > 0 AndAlso clsCommon.myLen(Birth_date) > 0 AndAlso clsCommon.myLen(PRESENT_MOBILE_NO) > 0 AndAlso clsCommon.myLen(EMail_ID) > 0 Then
            strq = "select EMP_CODE from TSPL_EMPLOYEE_MASTER where FATHERS_NAME='" & FATHERS_NAME & "' and Birth_date= '" & Birth_date & "' and PRESENT_MOBILE_NO='" & PRESENT_MOBILE_NO & "' and EMail_ID='" & EMail_ID & "'"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strq, trans)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("EMP_CODE")
            Else
                Return ""
            End If
        Else
            Return ""
        End If

    End Function
    Public Shared Function GetCboEmploymentNatureDataTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))
        Dim DR As DataRow = DT.NewRow()
        DR("Code") = "Permanent"
        DR("Name") = "Permanent"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Code") = "Contractual"
        DR("Name") = "Contractual"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Code") = "Casual"
        DR("Name") = "Casual"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Code") = "Other"
        DR("Name") = "Other"
        DT.Rows.Add(DR)

        DT.AcceptChanges()
        Return DT
    End Function
    Public Shared Function GetCboEmpStatusDataTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))
        Dim DR As DataRow = DT.NewRow()
        DR("Code") = "Active"
        DR("Name") = "Active"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Code") = "Inactive"
        DR("Name") = "Inactive"
        DT.Rows.Add(DR)
        DT.AcceptChanges()
        Return DT
    End Function
    Public Shared Function GetEMPBdayAnniversaryDT() As DataTable
        Dim serverDate As Date
        serverDate = clsCommon.GETSERVERDATE()
        Dim qry As String = "select emp.*,TSPL_DEPARTMENT_MASTER.DESCRIPTION as Department_Desc,TSPL_DESIGNATION_MASTER.Designation_Desc from ( " &
        " select EMP_CODE,Emp_Name,EMail_ID,DEPARTMENT_CODE,Designation,CONVERT(date,Birth_date,105) as BDay_Anniversary,'B' as Rem_Type  from TSPL_EMPLOYEE_MASTER" &
        " where DAY(CONVERT(date,Birth_date,105))=" & serverDate.Day & " and MONTH(CONVERT(date,Birth_date,105))=" & serverDate.Month & " " &
        " union all " &
        " select EMP_CODE,Emp_Name,EMail_ID,DEPARTMENT_CODE,Designation,CONVERT(date,Birth_date,105) as ANNIVERSARY_DATE,'A' as Rem_Type  from TSPL_EMPLOYEE_MASTER " &
        " where DAY(CONVERT(date,ANNIVERSARY_DATE,105))=" & serverDate.Day & " and MONTH(CONVERT(date,ANNIVERSARY_DATE,105))=" & serverDate.Month & " ) as emp " &
        " left join TSPL_DEPARTMENT_MASTER on emp.DEPARTMENT_CODE=TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE " &
        " left join TSPL_DESIGNATION_MASTER on emp.Designation=TSPL_DESIGNATION_MASTER.Designation_id "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Public Shared Function GetBdayAnniversaryMSG() As String
        Dim dt As DataTable = GetEMPBdayAnniversaryDT()
        Dim BDayMsg As String = "Have Birth Day Today."
        Dim AnnMsg As String = "Have Marriage Anniversary Today."
        Dim BdayEmp As String = ""
        Dim AnnEmps As String = ""
        Dim FinalMsg As String = ""
        '' make message for bdate/anniversary
        For Each dr As DataRow In dt.Rows
            If clsCommon.CompairString(dr.Item("Rem_Type"), "B") = CompairStringResult.Equal Then
                If clsCommon.myLen(BdayEmp) <= 0 Then
                    BdayEmp = dr.Item("Emp_Name") & "(Employee Id-" & dr.Item("EMP_CODE") & " , Department- " & clsCommon.myCstr(dr.Item("Department_Desc")) & ")"
                Else
                    BdayEmp = BdayEmp & Environment.NewLine & dr.Item("Emp_Name") & "(Employee Id-" & dr.Item("EMP_CODE") & " , Department- " & clsCommon.myCstr(dr.Item("Department_Desc")) & ")"
                End If
            Else
                If clsCommon.myLen(AnnEmps) <= 0 Then
                    AnnEmps = dr.Item("Emp_Name") & "(Employee Id-" & dr.Item("EMP_CODE") & " , Department- " & clsCommon.myCstr(dr.Item("Department_Desc")) & ")"
                Else
                    AnnEmps = AnnEmps & Environment.NewLine & dr.Item("Emp_Name") & "(Employee Id-" & dr.Item("EMP_CODE") & " , Department- " & clsCommon.myCstr(dr.Item("Department_Desc")) & ")"
                End If
            End If
        Next
        If clsCommon.myLen(BdayEmp) > 0 Then
            FinalMsg = BdayEmp & Environment.NewLine & BDayMsg
        End If
        If clsCommon.myLen(AnnEmps) > 0 Then
            FinalMsg = FinalMsg & Environment.NewLine & AnnEmps & Environment.NewLine & AnnMsg
        End If
        If clsCommon.myLen(FinalMsg) > 0 Then
            Return FinalMsg
        Else
            Return ""
        End If

    End Function
    Public Shared Function SendBdayAnniversaryEmail(ByVal Form_ID As String) As Boolean
        'Dim objSett As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmEmployee_Master)

        'If objSett Is Nothing Then
        '    'clsCommon.MyMessageBoxShow("First do email and sms setting", "Employee Master")
        '    Return False
        'End If
        'If clsCommon.myLen(objSett.mailsubjct) <= 0 Then
        '    'clsCommon.MyMessageBoxShow("First do email and sms setting", "Employee Master")
        '    Return False
        'End If
        'Dim dt As DataTable = GetEMPBdayAnniversaryDT()
        'Dim obj As New clsEmailSMSRecipients
        'Try
        '    For Each dr As DataRow In dt.Rows
        '        If clsCommon.myLen(dr.Item("EMail_ID")) <= 0 Then
        '            Continue For
        '        End If

        '        Dim strSubject As String = objSett.mailsubjct.Replace(clsEmailSMSConstants.EMP_CODE, dr.Item("Emp_Code"))
        '        strSubject = strSubject.Replace(clsEmailSMSConstants.Employee_Name, dr.Item("Emp_Name"))
        '        strSubject = strSubject.Replace(clsEmailSMSConstants.Birth_Date, clsCommon.GetPrintDate(dr.Item("BDay_Anniversary"), "dd/MMM/yyyy"))
        '        strSubject = strSubject.Replace(clsEmailSMSConstants.AnniversaryDate, clsCommon.GetPrintDate(dr.Item("BDay_Anniversary"), "dd/MMM/yyyy"))

        '        Dim strbody As String = objSett.mailbody.Replace(clsEmailSMSConstants.EMP_CODE, dr.Item("Emp_Code"))
        '        strbody = strbody.Replace(clsEmailSMSConstants.Birth_Date, clsCommon.GetPrintDate(dr.Item("BDay_Anniversary"), "dd/MMM/yyyy"))
        '        strbody = strbody.Replace(clsEmailSMSConstants.AnniversaryDate, clsCommon.GetPrintDate(dr.Item("BDay_Anniversary"), "dd/MMM/yyyy"))
        '        strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, Form_ID)
        '        strbody = strbody.Replace(clsEmailSMSConstants.Employee_Name, dr.Item("Emp_Name"))

        '        Dim lstReceiptents As New List(Of String)
        '        lstReceiptents.Add(dr.Item("EMail_ID"))
        '        clsMailViaOutlook.SendEmail(strSubject, strbody, lstReceiptents, Nothing, "")
        '    Next

        '---------Sanjay-------------------
        Try
            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + Form_ID + "'", Nothing)
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                Dim dt As DataTable = GetEMPBdayAnniversaryDT()

                For Each dr As DataRow In dt.Rows
                    If clsCommon.myLen(dr.Item("EMail_ID")) <= 0 Then
                        Continue For
                    End If


                    Dim objEmailH As New clsEMailHead()
                    objEmailH.arrEMail = New List(Of String)()

                    objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.EMP_CODE, dr.Item("Emp_Code"))
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Employee_Name, dr.Item("Emp_Name"))
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Birth_Date, clsCommon.GetPrintDate(dr.Item("BDay_Anniversary"), "dd/MMM/yyyy"))
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.AnniversaryDate, clsCommon.GetPrintDate(dr.Item("BDay_Anniversary"), "dd/MMM/yyyy"))

                    objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.EMP_CODE, dr.Item("Emp_Code"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Birth_Date, clsCommon.GetPrintDate(dr.Item("BDay_Anniversary"), "dd/MMM/yyyy"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.AnniversaryDate, clsCommon.GetPrintDate(dr.Item("BDay_Anniversary"), "dd/MMM/yyyy"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Form_Code, Form_ID)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Employee_Name, dr.Item("Emp_Name"))

                    objEmailH.arrEMail.Add(dr.Item("EMail_ID"))
                    objEmailH.SaveData(Form_ID, objEmailH, Nothing)
                    objEmailH = Nothing
                Next


            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function CheckUserForHRDepartment(ByVal UserCode As String)
        Dim qry As String = " select COUNT(TSPL_EMPLOYEE_MASTER.EMP_CODE) as Tot from TSPL_EMPLOYEE_MASTER " &
                            " left join TSPL_USER_MASTER on TSPL_EMPLOYEE_MASTER.USER_CODE=TSPL_USER_MASTER.User_Code " &
                            " left join TSPL_DEPARTMENT_MASTER on TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE=TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE " &
                            " where TSPL_USER_MASTER.User_Code='" & UserCode & "' and TSPL_DEPARTMENT_MASTER.DEPARTMENT_TYPE='HR'"
        Dim tot As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If tot > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class

Public Class clsEmpFamilieDetails

#Region "Variables"
    Public EMP_CODE As String
    Public LINE_NO As Int16
    Public MEMBER_NAME As String
    Public RELATION_WITH_EMP As String
    Public MEMBER_AGE As Double
    Public MEMBER_SEX As String
    Public DESCRIPTION As String
    Public IS_DEPENDENT As String = ""
    Public FDContactNo As String

    Public Member_DOB As Date?
    Public Member_Occupation As String
    Public Dependent_Living_With_Emp As String

#End Region

    Public Shared Function DeleteData(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_EMPLOYEE_FAMILIES where EMP_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsEmpFamilieDetails)
        Dim obj As clsEmpFamilieDetails = Nothing
        Dim ObjList As New List(Of clsEmpFamilieDetails)
        Dim qry As String = " select *  from TSPL_EMPLOYEE_FAMILIES "
        qry += " where EMP_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsEmpFamilieDetails()
                obj.EMP_CODE = clsCommon.myCstr(dr("EMP_CODE"))
                obj.LINE_NO = Convert.ToInt16(clsCommon.myCdbl(dr("LINE_NO")))
                obj.MEMBER_AGE = clsCommon.myCdbl(dr("MEMBER_AGE"))
                obj.RELATION_WITH_EMP = clsCommon.myCstr(dr("RELATION_WITH_EMP"))
                obj.MEMBER_NAME = clsCommon.myCstr(dr("MEMBER_NAME"))
                obj.MEMBER_SEX = clsCommon.myCstr(dr("MEMBER_SEX"))
                obj.DESCRIPTION = clsCommon.myCstr(dr("DESCRIPTION"))
                obj.IS_DEPENDENT = clsCommon.myCstr(dr("IS_DEPENDENT"))
                If IsDBNull(dr("Member_DOB")) = False Then
                    obj.Member_DOB = clsCommon.myCDate(dr("Member_DOB"))
                End If
                obj.Member_Occupation = clsCommon.myCstr(dr("Member_Occupation"))
                obj.Dependent_Living_With_Emp = clsCommon.myCdbl(dr("Dependent_Living_With_Emp"))
                obj.FDContactNo = clsCommon.myCdbl(dr("CONTACT_NO"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList

    End Function

    Public Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of clsEmpFamilieDetails)) As Boolean
        Dim isSaved As Boolean = True
        Try
            If ObjList IsNot Nothing AndAlso ObjList.Count > 0 Then
                For Each obj As clsEmpFamilieDetails In ObjList
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "MEMBER_AGE", Math.Round(obj.MEMBER_AGE, 2))
                    clsCommon.AddColumnsForChange(coll, "RELATION_WITH_EMP", obj.RELATION_WITH_EMP)
                    clsCommon.AddColumnsForChange(coll, "MEMBER_NAME", obj.MEMBER_NAME)
                    clsCommon.AddColumnsForChange(coll, "MEMBER_SEX", obj.MEMBER_SEX)
                    clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
                    clsCommon.AddColumnsForChange(coll, "IS_DEPENDENT", obj.IS_DEPENDENT)
                    clsCommon.AddColumnsForChange(coll, "CONTACT_NO", obj.FDContactNo)
                    If Not obj.Member_DOB Is Nothing Then
                        clsCommon.AddColumnsForChange(coll, "Member_DOB", obj.Member_DOB)
                    End If
                    clsCommon.AddColumnsForChange(coll, "Member_Occupation", obj.Member_Occupation, True)
                    clsCommon.AddColumnsForChange(coll, "Dependent_Living_With_Emp", obj.Dependent_Living_With_Emp)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))

                    Dim qry As String = "SELECT Count(*) FROM TSPL_EMPLOYEE_FAMILIES where EMP_CODE = '" & strCode & "' and LINE_NO = '" & obj.LINE_NO & "' "
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                    If check = 0 Then
                        clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                        clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_FAMILIES", OMInsertOrUpdate.Insert, "")
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_FAMILIES", OMInsertOrUpdate.Update, " EMP_CODE = '" & strCode & "' and LINE_NO = '" & obj.LINE_NO & "'  ")
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class


Public Class clsEmpLanguageDetails

#Region "Variables"
    Public EMP_CODE As String
    Public LINE_NO As Int16
    Public LANGUAGE_CODE As String
    Public READING_LEVEL As String
    Public WRITTING_LEVEL As String
    Public SPEAKING_LEVEL As String
    Public DESCRIPTION As String

#End Region

    Public Shared Function DeleteData(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_EMPLOYEE_LANGUAGES where EMP_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsEmpLanguageDetails)
        Dim obj As clsEmpLanguageDetails = Nothing
        Dim ObjList As New List(Of clsEmpLanguageDetails)
        Dim qry As String = " select *  from TSPL_EMPLOYEE_LANGUAGES "
        qry += " where EMP_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsEmpLanguageDetails()
                obj.EMP_CODE = clsCommon.myCstr(dr("EMP_CODE"))
                obj.LINE_NO = Convert.ToInt16(clsCommon.myCdbl(dr("LINE_NO")))
                obj.LANGUAGE_CODE = clsCommon.myCstr(dr("LANGUAGE_CODE"))
                obj.READING_LEVEL = clsCommon.myCstr(dr("READING_LEVEL"))
                obj.WRITTING_LEVEL = clsCommon.myCstr(dr("WRITTING_LEVEL"))
                obj.SPEAKING_LEVEL = clsCommon.myCstr(dr("SPEAKING_LEVEL"))
                obj.DESCRIPTION = clsCommon.myCstr(dr("DESCRIPTION"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList

    End Function

    Public Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of clsEmpLanguageDetails)) As Boolean
        Dim isSaved As Boolean = True
        Try
            If ObjList IsNot Nothing AndAlso ObjList.Count > 0 Then

                For Each obj As clsEmpLanguageDetails In ObjList
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "LANGUAGE_CODE", obj.LANGUAGE_CODE)
                    clsCommon.AddColumnsForChange(coll, "READING_LEVEL", obj.READING_LEVEL)
                    clsCommon.AddColumnsForChange(coll, "WRITTING_LEVEL", obj.WRITTING_LEVEL)
                    clsCommon.AddColumnsForChange(coll, "SPEAKING_LEVEL", obj.SPEAKING_LEVEL)
                    clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))

                    Dim qry As String = "SELECT Count(*) FROM TSPL_EMPLOYEE_LANGUAGES where EMP_CODE = '" & strCode & "' and LINE_NO = '" & obj.LINE_NO & "' "
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                    If check = 0 Then

                        clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                        clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_LANGUAGES", OMInsertOrUpdate.Insert, "")
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_LANGUAGES", OMInsertOrUpdate.Update, " EMP_CODE = '" & strCode & "' and LINE_NO = '" & obj.LINE_NO & "'  ")
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class


Public Class clsEmpQualiDetails

#Region "Variables"
    Public EMP_CODE As String
    Public LINE_NO As Int16
    Public COURSE_CODE As String
    Public JOINING_DATE As DateTime
    Public COMPLETION_DATE As DateTime
    Public COLLEGE_UNIVERSITY As String
    Public GRADE_PERCENTAGE As String
    Public DESCRIPTION As String
    Public VERIFICATION_DONE As String = ""

    Public University_Address As String
    Public University_Website As String



#End Region

    Public Shared Function DeleteData(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_EMPLOYEE_QUALIFICATION where EMP_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsEmpQualiDetails)
        Dim obj As clsEmpQualiDetails = Nothing
        Dim ObjList As New List(Of clsEmpQualiDetails)
        Dim qry As String = " select *  from TSPL_EMPLOYEE_QUALIFICATION "
        qry += " where EMP_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsEmpQualiDetails()
                obj.EMP_CODE = clsCommon.myCstr(dr("EMP_CODE"))
                obj.LINE_NO = Convert.ToInt16(clsCommon.myCdbl(dr("LINE_NO")))
                obj.COURSE_CODE = clsCommon.myCstr(dr("COURSE_CODE"))
                obj.JOINING_DATE = clsCommon.myCDate(dr("JOINING_DATE"))
                obj.COMPLETION_DATE = clsCommon.myCDate(dr("COMPLETION_DATE"))
                obj.COLLEGE_UNIVERSITY = clsCommon.myCstr(dr("COLLEGE_UNIVERSITY"))
                obj.GRADE_PERCENTAGE = clsCommon.myCstr(dr("GRADE_PERCENTAGE"))
                obj.DESCRIPTION = clsCommon.myCstr(dr("DESCRIPTION"))
                obj.VERIFICATION_DONE = clsCommon.myCstr(dr("VERIFICATION_DONE"))
                obj.University_Address = clsCommon.myCstr(dr("University_Address"))
                obj.University_Website = clsCommon.myCstr(dr("University_Website"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList

    End Function

    Public Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of clsEmpQualiDetails)) As Boolean
        Dim isSaved As Boolean = True
        Try
            If ObjList IsNot Nothing AndAlso ObjList.Count > 0 Then

                For Each obj As clsEmpQualiDetails In ObjList
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "COURSE_CODE", obj.COURSE_CODE)
                    clsCommon.AddColumnsForChange(coll, "COLLEGE_UNIVERSITY", obj.COLLEGE_UNIVERSITY)
                    clsCommon.AddColumnsForChange(coll, "GRADE_PERCENTAGE", obj.GRADE_PERCENTAGE)
                    clsCommon.AddColumnsForChange(coll, "JOINING_DATE", clsCommon.GetPrintDate(obj.JOINING_DATE, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "COMPLETION_DATE", clsCommon.GetPrintDate(obj.COMPLETION_DATE, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
                    clsCommon.AddColumnsForChange(coll, "VERIFICATION_DONE", obj.VERIFICATION_DONE)
                    clsCommon.AddColumnsForChange(coll, "University_Address", obj.University_Address, True)
                    clsCommon.AddColumnsForChange(coll, "University_Website", obj.University_Website, True)

                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))

                    Dim qry As String = "SELECT Count(*) FROM TSPL_EMPLOYEE_QUALIFICATION where EMP_CODE = '" & strCode & "' and LINE_NO = '" & obj.LINE_NO & "' "
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                    If check = 0 Then

                        clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                        clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_QUALIFICATION", OMInsertOrUpdate.Insert, "")
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_QUALIFICATION", OMInsertOrUpdate.Update, " EMP_CODE = '" & strCode & "' and LINE_NO = '" & obj.LINE_NO & "'  ")
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class


Public Class clsEmpExpDetails

#Region "Variables"
    Public EMP_CODE As String
    Public LINE_NO As Int16
    Public EMPLOYER_NAME As String
    Public JOINING_DATE As DateTime
    Public LEAVING_DATE As DateTime
    Public EMPLOYER_ADDRESS As String
    Public JOINING_DESIGNATION_ID As String
    Public LEAVING_DESIGNATION_ID As String
    Public JOINING_SALARY As Double
    Public LEAVING_SALARY As Double
    Public ACHIEVEMENTS As String
    Public DESCRIPTION As String
    '' fields added by panch raj on 16/10/2014 for KDIL
    Public VERIFICATION_DONE As String = ""
    '' fields added by panch raj on 10/11/2014 for KDIL
    Public Reporting_Person_Name As String
    Public Reporting_Person_Designation As String
    Public Reporting_Person_Phone As String
    Public Reporting_Person_Email As String
    Public Reporting_Person_Mobile As String
    Public VERIFICATION_STATUS As String
    Public VERIFICATION_MODE As String
    Public VERIFICATION_REMARKS As String


#End Region

    Public Shared Function DeleteData(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_EMPLOYEE_EXPERIENCE where EMP_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsEmpExpDetails)
        Dim obj As clsEmpExpDetails = Nothing
        Dim ObjList As New List(Of clsEmpExpDetails)
        Dim qry As String = " select *  from TSPL_EMPLOYEE_EXPERIENCE "
        qry += " where EMP_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsEmpExpDetails()
                obj.EMP_CODE = clsCommon.myCstr(dr("EMP_CODE"))
                obj.LINE_NO = Convert.ToInt16(clsCommon.myCdbl(dr("LINE_NO")))
                obj.EMPLOYER_NAME = clsCommon.myCstr(dr("EMPLOYER_NAME"))
                obj.JOINING_DATE = clsCommon.myCDate(dr("JOINING_DATE"))
                obj.LEAVING_DATE = clsCommon.myCDate(dr("LEAVING_DATE"))
                obj.EMPLOYER_ADDRESS = clsCommon.myCstr(dr("EMPLOYER_ADDRESS"))
                obj.JOINING_DESIGNATION_ID = clsCommon.myCstr(dr("JOINING_DESIGNATION_ID"))
                obj.LEAVING_DESIGNATION_ID = clsCommon.myCstr(dr("LEAVING_DESIGNATION_ID"))
                obj.JOINING_SALARY = clsCommon.myCdbl(dr("JOINING_SALARY"))
                obj.LEAVING_SALARY = clsCommon.myCdbl(dr("LEAVING_SALARY"))
                obj.ACHIEVEMENTS = clsCommon.myCstr(dr("ACHIEVEMENTS"))
                obj.DESCRIPTION = clsCommon.myCstr(dr("DESCRIPTION"))
                obj.VERIFICATION_DONE = clsCommon.myCstr(dr("VERIFICATION_DONE"))

                '' new columns for kdil
                obj.Reporting_Person_Name = clsCommon.myCstr(dr("Reporting_Person_Name"))
                obj.Reporting_Person_Designation = clsCommon.myCstr(dr("Reporting_Person_Designation"))
                obj.Reporting_Person_Phone = clsCommon.myCstr(dr("Reporting_Person_Phone"))
                obj.Reporting_Person_Email = clsCommon.myCstr(dr("Reporting_Person_Email"))
                obj.Reporting_Person_Mobile = clsCommon.myCstr(dr("Reporting_Person_Mobile"))
                obj.VERIFICATION_STATUS = clsCommon.myCstr(dr("VERIFICATION_STATUS"))
                obj.VERIFICATION_MODE = clsCommon.myCstr(dr("VERIFICATION_MODE"))
                obj.VERIFICATION_REMARKS = clsCommon.myCstr(dr("VERIFICATION_REMARKS"))

                ObjList.Add(obj)
            Next
        End If
        Return ObjList

    End Function

    Public Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of clsEmpExpDetails)) As Boolean
        Dim isSaved As Boolean = True
        Try
            If ObjList IsNot Nothing AndAlso ObjList.Count > 0 Then

                For Each obj As clsEmpExpDetails In ObjList
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "EMPLOYER_NAME", obj.EMPLOYER_NAME)
                    clsCommon.AddColumnsForChange(coll, "EMPLOYER_ADDRESS", obj.EMPLOYER_ADDRESS)
                    clsCommon.AddColumnsForChange(coll, "JOINING_DESIGNATION_ID", obj.JOINING_DESIGNATION_ID)
                    clsCommon.AddColumnsForChange(coll, "LEAVING_DESIGNATION_ID", obj.LEAVING_DESIGNATION_ID)
                    clsCommon.AddColumnsForChange(coll, "LEAVING_SALARY", obj.LEAVING_SALARY)
                    clsCommon.AddColumnsForChange(coll, "JOINING_SALARY", obj.JOINING_SALARY)
                    clsCommon.AddColumnsForChange(coll, "JOINING_DATE", clsCommon.GetPrintDate(obj.JOINING_DATE, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "LEAVING_DATE", clsCommon.GetPrintDate(obj.LEAVING_DATE, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
                    clsCommon.AddColumnsForChange(coll, "VERIFICATION_DONE", obj.VERIFICATION_DONE)
                    clsCommon.AddColumnsForChange(coll, "ACHIEVEMENTS", obj.ACHIEVEMENTS)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))

                    '' new columns for kdil
                    clsCommon.AddColumnsForChange(coll, "Reporting_Person_Name", obj.Reporting_Person_Name, True)
                    clsCommon.AddColumnsForChange(coll, "Reporting_Person_Designation", obj.Reporting_Person_Designation, True)
                    clsCommon.AddColumnsForChange(coll, "Reporting_Person_Phone", obj.Reporting_Person_Phone, True)
                    clsCommon.AddColumnsForChange(coll, "Reporting_Person_Email", obj.Reporting_Person_Email, True)
                    clsCommon.AddColumnsForChange(coll, "Reporting_Person_Mobile", obj.Reporting_Person_Mobile, True)
                    clsCommon.AddColumnsForChange(coll, "VERIFICATION_STATUS", obj.VERIFICATION_STATUS, True)
                    clsCommon.AddColumnsForChange(coll, "VERIFICATION_MODE", obj.VERIFICATION_MODE, True)
                    clsCommon.AddColumnsForChange(coll, "VERIFICATION_REMARKS", obj.VERIFICATION_REMARKS, True)

                    Dim qry As String = "SELECT Count(*) FROM TSPL_EMPLOYEE_EXPERIENCE where EMP_CODE = '" & strCode & "' and LINE_NO = '" & obj.LINE_NO & "' "
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                    If check = 0 Then

                        clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                        clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_EXPERIENCE", OMInsertOrUpdate.Insert, "")
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_EXPERIENCE", OMInsertOrUpdate.Update, " EMP_CODE = '" & strCode & "' and LINE_NO = '" & obj.LINE_NO & "'  ")
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class

Public Class clsEmpDocuments

#Region "Variables"
    Public EMP_CODE As String
    Public LINE_NO As Int16
    Public DOCUMENT_CODE As String
    Public DocName As String
    Public SUBMIT_DATE As DateTime
    Public DOCUMENT_FILE As Byte()
    Public DESCRIPTION As String

#End Region

    Public Shared Function DeleteDataAllDoc(ByVal strEmpCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strEmpCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_EMPLOYEE_DOCUMENTS where EMP_CODE ='" + strEmpCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function DeleteData(ByVal strEmpCode As String, ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_EMPLOYEE_DOCUMENTS where EMP_CODE ='" + strEmpCode + "' and DOCUMENT_CODE='" + strCode + "' "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetDataForGrid(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsEmpDocuments)
        Dim obj As clsEmpDocuments = Nothing
        Dim ObjList As New List(Of clsEmpDocuments)
        Dim qry As String = " select *  from TSPL_EMPLOYEE_DOCUMENTS "
        qry += " where EMP_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim counter As Int16 = 0
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                counter += 1
                obj = New clsEmpDocuments()
                obj.EMP_CODE = clsCommon.myCstr(dr("EMP_CODE"))
                obj.LINE_NO = counter
                obj.DOCUMENT_CODE = clsCommon.myCstr(dr("DOCUMENT_CODE"))
                obj.DocName = clsCommon.myCstr(dr("DocName"))
                obj.SUBMIT_DATE = clsCommon.myCDate(dr("SUBMIT_DATE"))
                obj.DESCRIPTION = clsCommon.myCstr(dr("DESCRIPTION"))

                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function

    Public Shared Function GetDocument(ByVal strEmpCode As String, ByVal strCode As String) As DataTable
        Dim qry As String = " select * from  TSPL_EMPLOYEE_DOCUMENTS where EMP_CODE ='" + strEmpCode + "' and DOCUMENT_CODE='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim data As Byte() = dt.Rows(0)("DOCUMENT_FILE")
        Return dt
    End Function


    Public Function SaveData(ByVal strCode As String, ByVal objList As List(Of clsEmpDocuments)) As Boolean
        Dim isSaved As Boolean = True
        Try
            If objList IsNot Nothing AndAlso objList.Count > 0 Then
                For Each obj As clsEmpDocuments In objList
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                    clsCommon.AddColumnsForChange(coll, "DOCUMENT_CODE", obj.DOCUMENT_CODE)
                    'clsCommon.AddColumnsForChange(coll, "DocName", obj.DocName)
                    clsCommon.AddColumnsForChange(coll, "SUBMIT_DATE", clsCommon.GetPrintDate(obj.SUBMIT_DATE, "dd/MMM/yyyy"))
                    'clsCommon.AddColumnsForChange(coll, "DOCUMENT_FILE", obj.DOCUMENT_FILE, True)
                    clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                    Dim qry As String = "SELECT Count(*) FROM TSPL_EMPLOYEE_DOCUMENTS where EMP_CODE = '" + strCode + "' and DOCUMENT_CODE = '" + obj.DOCUMENT_CODE + "' "
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                    If check = 0 Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_DOCUMENTS", OMInsertOrUpdate.Insert, "")

                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_DOCUMENTS", OMInsertOrUpdate.Update, " EMP_CODE = '" + strCode + "' and DOCUMENT_CODE = '" + obj.DOCUMENT_CODE + "'  ")
                    End If
                    If Not obj.DOCUMENT_FILE Is Nothing AndAlso obj.DOCUMENT_FILE.Count > 0 Then
                        Dim str As String
                        str = "UPDATE TSPL_EMPLOYEE_DOCUMENTS set DOCUMENT_FILE = @BLOBData,DocName='" & obj.DocName & "' where EMP_CODE = '" + obj.EMP_CODE + "' and DOCUMENT_CODE='" + obj.DOCUMENT_CODE + "'"
                        Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection)
                        Dim prm As New SqlParameter("@BLOBData", obj.DOCUMENT_FILE)
                        cmd.Parameters.Add(prm)
                        cmd.ExecuteNonQuery()
                    End If

                Next
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class
Public Class clsEmpCustomerBillingRateDetails

#Region "Variables"
    Public EMP_CODE As String
    Public Cust_Code As String
    Public BILLING_RATE As String
    Public COMMENTS As String

#End Region

    Public Shared Function DeleteData(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_PJC_CUSTOMER_BILLING_RATE where EMP_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsEmpCustomerBillingRateDetails)
        Dim obj As clsEmpCustomerBillingRateDetails = Nothing
        Dim ObjList As New List(Of clsEmpCustomerBillingRateDetails)
        Dim qry As String = " select *  from TSPL_PJC_CUSTOMER_BILLING_RATE "
        qry += " where EMP_CODE = '" + strCode + "' order by cust_code "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsEmpCustomerBillingRateDetails()
                obj.EMP_CODE = clsCommon.myCstr(dr("EMP_CODE"))
                obj.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                obj.BILLING_RATE = clsCommon.myCdbl(dr("BILLING_RATE"))
                obj.COMMENTS = clsCommon.myCstr(dr("COMMENTS"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList

    End Function

    Public Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of clsEmpCustomerBillingRateDetails), Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = True
        Try
            If ObjList Is Nothing Then
                Return isSaved
            End If
            For Each obj As clsEmpCustomerBillingRateDetails In ObjList
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "BILLING_RATE", obj.BILLING_RATE)
                clsCommon.AddColumnsForChange(coll, "COMMENTS", obj.COMMENTS)

                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                Dim qry As String = "SELECT Count(*) FROM TSPL_PJC_CUSTOMER_BILLING_RATE where EMP_CODE = '" & strCode & "' and CUST_CODE = '" & obj.Cust_Code & "' "
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

                If check = 0 Then

                    clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                    clsCommon.AddColumnsForChange(coll, "CUST_CODE", obj.Cust_Code)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJC_CUSTOMER_BILLING_RATE", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJC_CUSTOMER_BILLING_RATE", OMInsertOrUpdate.Update, " EMP_CODE = '" & strCode & "' and CUST_CODE = '" & obj.Cust_Code & "'  ", trans)
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class
Public Class clsDesignationMaster

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Designation_id as [Code],Designation_Desc as [Description],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code] from tspl_designation_master "
        str = clsCommon.ShowSelectForm("RPTDESIGFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

End Class

Public Class clsUserMaster



    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select User_Code as [Code],User_Name as [User Name],User_Type as [User Type],EMP_CODE as [Employee Code],Emp_Name as [Employee Name],Level1_Code as [Level1 Code],Level2_Code as [Level2 Code],Level3_Code as [Level3 Code],Level4_Code as [Level4 Code],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],segment_code as [Department],E_Mail as [EMail],Mob_No as [Mobile No],Level,ApprovalLevel as [Approval Level] from tspl_user_master "
        str = clsCommon.ShowSelectForm("RPTUSRFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function GetSubbordinateUsersQry(ByVal strUserCode As String) As String
        Dim qry As String = ""
        Try
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
                qry = "Select User_Code, User_Name from TSPL_User_MASTER Where 1=1 "
            Else
                qry = " select  distinct  User_Code, User_Name from (Select User_Code, User_Name from TSPL_User_MASTER Where Level4_Code='" + strUserCode + "' OR User_Code='" + strUserCode + "' " & Environment.NewLine &
                " union " & Environment.NewLine &
                " select TSPL_USER_MAPPING_DETAIL.Mapped_UserCode AS User_Code,TSPL_User_MASTER.User_Name  from TSPL_USER_MAPPING_DETAIL " & Environment.NewLine &
                "  left outer join TSPL_User_MASTER on TSPL_User_MASTER.User_Code =TSPL_USER_MAPPING_DETAIL.Mapped_UserCode  Where TSPL_USER_MAPPING_DETAIL.User_Code='" + strUserCode + "' ) Final"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return qry
    End Function
    Public Shared Function GetName(ByVal Code As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select User_Name from tspl_User_Master where User_Code='" & Code & "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
Public Class clsUserGroupMaster
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Group_Code as [Code],Group_Desc as [Description],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code] from tspl_user_Group_master "
        str = clsCommon.ShowSelectForm("RPTUGRPFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsEmpAssets

#Region "Variables"
    Public EMP_CODE As String
    Public LINE_NO As Integer
    Public ASSET_CODE As String
    Public ASSET_NAME As String
    Public ALLOCATE_DATE As Date?
    Public DESCRIPTION As String
    Public RETURNED As String


#End Region

    Public Shared Function DeleteDataAllDoc(ByVal strEmpCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strEmpCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_EMPLOYEE_ASSETS where EMP_CODE ='" + strEmpCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function DeleteData(ByVal strEmpCode As String, ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_EMPLOYEE_ASSETS where EMP_CODE ='" + strEmpCode + "' and ASSET_CODE='" + strCode + "' "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetDataForGrid(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsEmpAssets)
        Dim obj As clsEmpAssets = Nothing
        Dim ObjList As New List(Of clsEmpAssets)
        Dim qry As String = " select *  from TSPL_EMPLOYEE_ASSETS "
        qry += " where EMP_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim counter As Int16 = 0
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                counter += 1
                obj = New clsEmpAssets()
                obj.EMP_CODE = clsCommon.myCstr(dr("EMP_CODE"))
                obj.LINE_NO = counter
                obj.ASSET_CODE = clsCommon.myCstr(dr("ASSET_CODE"))
                obj.ASSET_NAME = clsCommon.myCstr(dr("ASSET_NAME"))
                If Not IsDBNull(dr("ALLOCATE_DATE")) Then
                    obj.ALLOCATE_DATE = clsCommon.myCDate(dr("ALLOCATE_DATE"))
                Else
                    obj.ALLOCATE_DATE = Nothing
                End If

                obj.DESCRIPTION = clsCommon.myCstr(dr("DESCRIPTION"))
                obj.RETURNED = clsCommon.myCstr(dr("RETURNED"))

                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function


    Public Function SaveData(ByVal strCode As String, ByVal objList As List(Of clsEmpAssets)) As Boolean
        Dim isSaved As Boolean = True
        Try
            If objList IsNot Nothing AndAlso objList.Count > 0 Then
                For Each obj As clsEmpAssets In objList
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                    clsCommon.AddColumnsForChange(coll, "ASSET_CODE", obj.ASSET_CODE)
                    clsCommon.AddColumnsForChange(coll, "ASSET_NAME", obj.ASSET_NAME)
                    clsCommon.AddColumnsForChange(coll, "RETURNED", obj.RETURNED)
                    If Not obj.ALLOCATE_DATE Is Nothing Then
                        clsCommon.AddColumnsForChange(coll, "ALLOCATE_DATE", clsCommon.GetPrintDate(obj.ALLOCATE_DATE, "dd/MMM/yyyy"))
                    End If

                    clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)

                    Dim qry As String = "SELECT Count(*) FROM TSPL_EMPLOYEE_ASSETS where EMP_CODE = '" + strCode + "' and ASSET_CODE = '" + obj.ASSET_CODE + "' "
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                    If check = 0 Then
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_ASSETS", OMInsertOrUpdate.Insert, "")

                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_ASSETS", OMInsertOrUpdate.Update, " EMP_CODE = '" + strCode + "' and ASSET_CODE = '" + obj.ASSET_CODE + "'  ")
                    End If

                Next
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class