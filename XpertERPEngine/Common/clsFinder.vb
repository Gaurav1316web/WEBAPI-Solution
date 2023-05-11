Imports common
Imports System.Data.SqlClient


Public Class clsAbatementMaster


#Region "Variables"
    Public Abatement_Code As String
    Public Abatement_Desc As String
    Public Start_Date As Date
    Public End_Date As Date
    Public Abatement_Percent As Double

#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Abatement_Code as [Code],Abatement_Desc as [Description],Start_Date as [Start Date],End_Date as [End Date],Abatement_Percent as [Abatement Percent],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code]   from TSPL_ABATEMENT_MASTER     "
        str = clsCommon.ShowSelectForm("ABTMSTRFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    ''richa Ticket No BM00000002902 19/06/2014
    '----------------Code For Save Data--------------------------------------------------------------------'
    Public Function SaveData(ByVal obj As clsAbatementMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Abatement_Code", obj.Abatement_Code)
            clsCommon.AddColumnsForChange(coll, "Abatement_Desc", obj.Abatement_Desc)
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Abatement_Percent", obj.Abatement_Percent)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ABATEMENT_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ABATEMENT_MASTER", OMInsertOrUpdate.Update, "Abatement_Code='" + obj.Abatement_Code + "' ", trans)
            End If
            'If isSaved Then
            '    trans.Commit()
            'End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    '----------------End of Code For SaveData--------------------------------------------------------------'
    '------------------------------------------------------------------------------------------------------'
End Class
Public Class clsFinder

    Public Shared Function ShowCustomerFinder(Optional ByVal ReportId As String = "RPTCUSTFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select Cust_Code as [Code],Customer_Name as [Customer Name],Add1 ,Add2,Add3,Closing_Date as [Closing Date],Cust_Category_Code as [Customer Category Code],Cust_Group_Code as [Customer Group Code],Cust_Type_Code as [Customer Type Code],Route_No as [Route No],Route_Desc as [Route Description],Price_Code as [Price Code],City_Code as [City Code],State,Country,Phone1,Phone2,Fax,Email,WebSite,Contact_Person_Name as [Contact Person Name],Contact_Person_Phone as [Contact Person Phone],Contact_Person_Fax as [Contact Person Fax],Contact_Person_Website as [Contact Person Website],Contact_Person_Email as [Contact Person Email],Terms_Code as [Terms Code],Cust_Account as [Customer Account],Tax_Group as [Tax Group],TAX1,TAX1_Rate as [Tax1 Rate],TAX2,TAX2_Rate as [Tax2 Rate],TAX3,TAX3_Rate as [Tax3 Rate],TAX4,TAX4_Rate as [Tax4 Rate],TAX5,TAX5_Rate as [Tax5 Rate],TAX6,TAX6_Rate as [Tax6 Rate],TAX7,TAX7_Rate as [Tax7 Rate],TAX8,TAX8_Rate as [Tax8 Rate],TAX9,TAX9_Rate as [Tax9 Rate],TAX10,TAX10_Rate as [Tax10 Rate],Payment_Code as [Payment Code],Service_Tax_No as [Service Tax No],Tin_No as [Tin No],Lst_No as [LST No],Form_Type as [Form Type],Channel_Code as [Channel Code],Channel_Desc as [Channel Description],(select case when Status='N' then 'Active' else 'In Active' end ) as [Status],OnHold as [On Hold],Remarks1,Remarks2,Additional1,Additional2,Additional3,Salesman_Code as [Salesman Code],Salesman_Desc as [Salesman Description],Visi_Id as [Visi ID],Visi_Desc as [Visi Description],OutLet_Commossion as [Outlet Commission], Balance_ToDate as [Balance To Date],Credit_Limit as [Credit Limit],Created_By as [Created By],Created_Date as [Created Date],Modify_By as[Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Route_Group as [Route Group],CST,ECC,Range,Collectorate,PAN,Division,Parent_Customer_No as [Parent Customer No],Customer_Class as [Customer Class],Credit_Customer as [Credit Customer],LastInvoice_No as [Last Invoice No],LastInvoice_Date as [Last Invoice Date],price_CodeNon as [Price Code Non],Inter_Branch as [Inter Branch],TRANSACTION_TYPE as [Transaction Type],Credit_Limit_Alert_Type as [Credit Limit alert Type],PIN_Code as [Pin Code],Cust_DOB as [Customer DOB],Cust_Spouse_DOB as [Customer Spouse DOB],Anniversary_Date as [Anniversary Date],Gender,Occation,Agg_Made_Date as [Agg Made Date],Agg_Close_Date as [Agg Close Date],CURRENCY_CODE as [Currency Code],Parent_Customer_YN as [Is Parent Customer],Service_Dealer_Code as [Service Dealer Code],TDM_Code as [TDM Code],Distributor_Code as [Distributor Code],IsDistributor as [Is Distributor],Price_Group_Code as [Price Group Code] from tspl_customer_master"
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str

    End Function
    Public Shared Function ShowVendorFinder(Optional ByVal ReportId As String = "RPTVENDFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select Vendor_Code as [Code],Vendor_Name as [Vendor Name],Add1,Add2,Add3,Closing_Date as [Closing Date],Vendor_Group_Code as [Vendor Group Code],Vendor_Group_Code_Desc as [Vendor Group Description],City_Code as [City Code],City_Code_Desc as [City Description],State,Country,Phone1,Phone2,Fax,Email,WebSite,Contact_Person_Name as [Contact Person Name],Contact_Person_Phone as [Contact Person Phone],Contact_Person_Fax as [Contact Person FAX],Contact_Person_Website as [Contact Person Website],Contact_Person_Email as [Contact Person Email],Terms_Code as [Terms Code],Terms_Code_Desc as [Terms Code Description],Vendor_Account as [Vendor Account],Vendor_Account_Desc as [Vendor Account Description],Payment_Code as [Payment Code],Payment_Code_Desc as [Payment Code Description],Bank_Code as [Bank Code],Bank_Code_Desc as [Bank Description],Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Ven_Type_Code as [Vendor Type Code],Ven_Type_Desc as [Vendor Type Description],TAX1,TAX1_Rate as [TAX1 Rate],TAX2,TAX2_Rate as [Tax2 Rate],TAX3,TAX3_Rate as [Tax3 Rate],TAX4,TAX4_Rate as [Tax4 Rate],TAX5,TAX5_Rate as [Tax5 Rate],TAX6,TAX6_Rate as [Tax6 Rate],TAX7,TAX7_Rate as [Tax7 Rate],TAX8,TAX8_Rate as [Tax8 Rate],TAX9,TAX9_Rate as [Tax9 Rate],TAX10,TAX10_Rate as [Tax10 Rate],Service_Tax_No as [Service Tax No],Tin_No as [TIN No],Lst_No as [LST No],(select case when Status='N' then 'Active' else 'In Active' end ) as Status,OnHold as [On Hold],Transporter,Remarks1,Remarks2,Additional1,Additional2,Additional3,Credit_Limit as [Credit Limit],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],CST,ECC,Range,Collectorate,PAN,Is_Gross_Receipt as [Is Gross Receipt],Inter_Branch as [Inter Branch],CURRENCY_CODE as [Currency Code],franchise_yn as [Is Franchise] from tspl_vendor_master"
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function
    Public Shared Function ShowEmployeeFinder(Optional ByVal ReportId As String = "RPTEMPFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select EMP_CODE as [Code],Emp_Name as [Employee Name],Designation,Add1,Add2,Pin_Code as [Pin Code],Phone,Birth_date as [Date Of Birth],Cash,Card_No as [Card No],Joining_date as [Joining Date],Emp_type as [Employee Type],ExDate [Expiry Date],Emp_Status as [Employee Status],RELIEVING_DATE as [Releving Date],Payroll_Code as [Payroll Code],Empty_Ex as [Empty Ex],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],GL_Account as [GL Account],EMail_ID as [Email ID],SEX,MARITAL_STATUS as [Marital Status],ANNIVERSARY_DATE as [Anniversary Date],DEPARTMENT_CODE as [Department Code],OCCUPATION_CODE as [Occupation Code],DEVISION_CODE as [Division Code],GRADE_CODE as [Grade Code],BRANCH_CODE as [Branch Code],ATTENDANCE_CODE as [Attandance Code],PAYMENT_MODE as [Payment Mode],BANK_ACC_NO as [Bank Account No],BANK_CODE as [Bank Code],CONFIRMATION_DATE as [Confirmation Date],PROBATION_END_DATE as [Probation End Date],SHIFT_CODE as [Shift Code],RELIEVING_DATE as [Relieving Date],LEAVING_REASON as [Leaving Reason],CAST_CATEGORY_CODE as [Cast Category Code],RELIGION_CODE as [Religion Code],PRESENT_COUNTRY_CODE as [Present Country Code],PRESENT_STATE_CODE as [Present State Code,PRESENT_CITY_CODE as [Present City Code],PRESENT_MOBILE_NO as [Present Mobile No],PERMA_COUNTRY_CODE as [Permanent Country Code],PERMA_STATE_CODE as [Permanent State Code],PERMA_CITY_CODE as [Permanent City Code],PERMA_PHONE_NO as [Permanent Phone No],PERMA_MOBILE_NO as [Permanent Mobile No],PERMA_PIN_CODE as [Permanent Pin Code],PAN_NO as [Pan No],PASPORT_NO as [Passport No],DESCRIPTION as [Description],FATHERS_NAME as [Fathers Name],MOTHERS_NAME as [Mothers Name],SPOUSE_NAME as [Spouse Name],ISESI as [Is ESI],ESI_NO as [ESI No],ESI_DISPENSARY as [ESI Dispensary],ISPF as [Is PF],PF_NO as [PF No],PF_NO_DEPT_FILE as [PF No Department File],WARD_CIRCLE as [Ward Circle],ISRESTRICT_PF as [Is Restrict PF],ISZERO_PENSION as [Is Zero Pension],ISDIRECTOR as [Is Director],ISZERO_PT as [Is Zero PT],EARNING_CODE as [Earning Code],UNIT_COST as [Unit Cost],BILLING_RATE as [Billing Rate],APPLY_ALL_CUST as [Apply All Customer],USER_CODE as [User Code],COMMENTS as [Comments],Franchise_Code as [Franchise Code],RESIGNATION_SUBMIT_DATE as [Resignation Submission Date],NOTICE_PERIOD_IN_DAYS as [Notice Period In Days] from tspl_employee_master"
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function
    Public Shared Function ShowDesignationMasterFinder(Optional ByVal ReportId As String = "RPTDESIGFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select Designation_id as [Code],Designation_Desc as [Description],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code] from tspl_designation_master"
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function
    Public Shared Function ShowUserMasterFinder(Optional ByVal ReportId As String = "RPTUSRFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select User_Code as [Code],User_Name as [User Name],User_Type as [User Type],EMP_CODE as [Employee Code],Emp_Name as [Employee Name],Level1_Code as [Level1 Code],Level2_Code as [Level2 Code],Level3_Code as [Level3 Code],Level4_Code as [Level4 Code],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Dept as [Department],E_Mail as [EMail],Mob_No as [Mobile No],Level,ApprovalLevel as [Approval Level] from tspl_user_master"
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function
    Public Shared Function ShowUserGroupMasterFinder(Optional ByVal ReportId As String = "RPTUGRPFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select Group_Code as [Code],Group_Desc as [Description],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code] from tspl_user_Group_master"
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function
    Public Shared Function ShowTaxRateFinder(Optional ByVal ReportId As String = "RPTXRTFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select Tax_Code as [Code],Tax_Type as [Tax Type],Tax_Rate_Code as [Tax Rate Code],Tax_Rate_Desc as [Tax Rate Description],Tax_Rate as [Tax Rate],Created_By as [Created By],Created_Date as [Created By],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code] from tspl_tax_rates"
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function
    Public Shared Function ShowTaxGroupMasterFinder(Optional ByVal ReportId As String = "RPTXGRPFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select Tax_Group_Code as [Code],Tax_Group_Desc as [Description],Tax_Group_Type as [Tax Group Type],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Excisable as [Excisable],VAT,STax ,Tax_Group_Code_InterState as [Tax Group Code Inter State],Tax_Group_Description_InterState as [Tax Group Description Inter State],Is_Transfer as [Is Transfer] from TSPL_TAX_GROUP_MASTER "
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function
    Public Shared Function ShowPaymentCodeFinder(Optional ByVal ReportId As String = "RPTPMTCDFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select Payment_Code as [Code],Payment_Desc as [Description],Payment_Type as [Payment Type],Created_By as [Creatd By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code] from TSPL_PAYMENT_CODE  "
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function
    Public Shared Function ShowCityMasterFinder(Optional ByVal ReportId As String = "RPTCITYFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select City_Code as [Code],City_Name as [City Name],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],STATE_CODE  as [State Code] from TSPL_CITY_MASTER  "
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function

    Public Shared Function ShowCompanyMasterFinder(Optional ByVal ReportId As String = "RPTCPMSTFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select Comp_Code as [Code],Comp_Name as [Company Name],Add1,Add2,Add3,City_Code as [City Code],Phone1,Phone2,Fax,Email,Pincode,State,Tin_No as [Tin No],CST_LST as [CST LST],Regn_No as [Registration No],Cform as [C Form],Mode_of_Trans as [Mode Of Trans],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code1 as [Company Code1],DataBase_Name as [Database Name],Vat_Reg_No as [VAT Registration No],ServiceTax_Reg_No as [Service Tax Registration No],Ecc_No as [ECC No],CE_Range as [CE Range],CE_Commissionerate as [CE Commission Rate],CE_Division as [CE Division],Pan_No as [PAN No],Tan_No as [TAN NO],Tcan_No as [TCAN No],Circle_No as [Circle No],Ward_No as [Ward No],Access_Officer as [Access Officer],NameInTally as [Name In Tally],IntegrateWithTally as [Integrate With Tally],BaseCurrencyCode as [Base Currency Code],ApplyMultiCurrency  as [Apply Multi Currency] from TSPL_COMPANY_MASTER   "
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function

    Public Shared Function ShowAdditionalChargesFinder(Optional ByVal ReportId As String = "RPTADCHGFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select Code as [Code],Description,Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modify By],Modified_Date as [Modify Date],Comp_Code as [Company Code],Account_Code as [Account Code],Account_Description as [Account Description],FreightCharges as [Freight Charges] from TSPL_Additional_Charges   "
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function

    Public Shared Function ShowFormMasterFinder(Optional ByVal ReportId As String = "RPTFRMMSTFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select Form_code as [Code],Form_name as [Form Name],Remarks,Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modify By],Modified_Date as [Modify Date],Comp_Code as [Company Code],Form_Type as [Form Type]  from TSPL_Form_Master    "
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function

    Public Shared Function ShowBankMasterFinder(Optional ByVal ReportId As String = "RPTBNKMSTFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select BANK_CODE as [Code],DESCRIPTION as [Description],ADD1 as [Add1],ADD2 as [Add2],ADD3 as [Add3],ADD4 as [Add4],CITY as [City],STATE as [State],POSTAL as [Postal],COUNTRY as [Country],CONTACT as [Contact],PHONE as [Phone],FAX as [Fax],INACTIVE as [Inactive],BANKACCNUMBER as [Bank Account Number],BANKACC as [Bank Account],WRITEOFFACC as [Write Off Account],CREDITACC as [Credit Account],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Bank_type as [Bank Type],Cheque_Validity_In_Days as [Cheque Validity In Days]   from TSPL_Bank_Master    "
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function
    Public Shared Function ShowCurrencyConversionRateFinder(Optional ByVal ReportId As String = "RPTCURCNVFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select Code as [Code],FROM_CURRENCY as [From Currency],TO_CURRENCY as [To Currency],FROM_DATE as [From Date],TO_DATE as [To Date],Rate as [Rate],DESCRIPTION as [Description]  from TSPL_CURRENCY_CONVERSION_RATE    "
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function
    Public Shared Function ShowPaymentTermsFinder(Optional ByVal ReportId As String = "RPTTRMFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select Terms_Code as [Code],Terms_Desc as [Description],No_Days as [No of Days],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code]  from TSPL_TERMS_MASTER    "
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function
    Public Shared Function ShowStateMasterFinder(Optional ByVal ReportId As String = "STMSTRFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select STATE_CODE as [Code],STATE_NAME as [State Name],COUNTRY_CODE as [Country Code] ,Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modify By],Modified_Date as [Modify Date]  from TSPL_State_MASTER     "
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function
    Public Shared Function ShowLocationMasterFinder(Optional ByVal ReportId As String = "LOCMSTRFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER     "
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function
    Public Shared Function ShowAbatementMasterFinder(Optional ByVal ReportId As String = "ABTMSTRFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select Abatement_Code as [Code],Abatement_Desc as [Description],Start_Date as [Start Date],End_Date as [End Date],Abatement_Percent as [Abatement Percent],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code]   from TSPL_ABATEMENT_MASTER     "
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function
    Public Shared Function ShowCurrencyMasterFinder(Optional ByVal ReportId As String = "CURMSTRFND", Optional ByVal whrcls As String = "", Optional ByVal curcode As String = "") As String
        Dim str As String = ""
        Dim qry As String = " select  CURRENCY_CODE as [Code],CURRENCY_NAME as [Currency Name],DESCRIPTION as [Description],Created_By as [Created By],Created_Date as [Created Date] ,Modified_By as [Modified By],Modified_Date as [Modified Date],CURRENCY_SIGN as [Currency Sign]  from TSPL_CURRENCY_MASTER     "
        str = clsCommon.ShowSelectForm(ReportId, qry, "Code", whrcls, curcode, "", True)
        Return str
    End Function

End Class
