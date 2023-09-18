'update by pradeep @ BM00000000706
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports common
Imports System
Imports System.Data
Imports System.IO
Imports System.Configuration
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports XpertERPEngine
Public Class frmEmployee_Master
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoad As Boolean = False


#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private isCellValueChanged As Boolean = False
    Dim Qry As String
    Dim CreateEmpCodeAsPerEmployeeBasisType As Boolean = False
    Dim AadharNoMandatoryOnEmpMaster As Boolean = False
    Public ObjListEmpFamilieDetails As List(Of clsEmpFamilieDetails) = Nothing
    Dim objEmpFamilieDetails As New clsEmpFamilieDetails()

    Public ObjListEmpLanguageDetails As List(Of clsEmpLanguageDetails) = Nothing
    Dim objEmpLanguageDetails As New clsEmpLanguageDetails()

    Public ObjListEmpQualiDetails As List(Of clsEmpQualiDetails) = Nothing
    Dim objEmpQualiDetails As New clsEmpQualiDetails()

    Dim ObjListEmpDocuments As List(Of clsEmpDocuments) = Nothing
    Dim objEmpDocuments As New clsEmpDocuments()

    Public ObjListEmpExpDetails As List(Of clsEmpExpDetails) = Nothing
    Dim objEmpExpDetails As New clsEmpExpDetails()

    Public ObjListEmpAssetDetails As List(Of clsEmpAssets) = Nothing
    Dim objEmpAssetDetails As New clsEmpAssets()


#Region "Emp Exp"
    Const colLineNo As String = "LineNo"
    Const colEmployerName As String = "EmployerName"
    Const colEmployerAddress As String = "EmployerAddress"
    Const colJoiningDate As String = "JoiningDate"
    Const colLeavingDate As String = "LeavingDate"
    Const colJoinDesi As String = "JoinDesi"
    Const colLeavDesi As String = "LeavDesi"
    Const colJoinSalary As String = "JoinSalary"
    Const colLeavingSalary As String = "LeavingSalary"
    Const colAchievements As String = "Achievements"
    Const colDescription As String = "Description"
    Const colExpVerification_Done As String = "colExpVerification_Done"
    Const colReporting_Person_Name As String = "colReporting_Person_Name"
    Const colReporting_Person_Designation As String = "colReporting_Person_Designation"
    Const colReporting_Person_Phone As String = "colReporting_Person_Phone"
    Const colReporting_Person_Email As String = "colReporting_Person_Email"
    Const colReporting_Person_Mobile As String = "colReporting_Person_Mobile"

    Const colVERIFICATION_STATUS As String = "colVERIFICATION_STATUS"
    Const colVERIFICATION_MODE As String = "colVERIFICATION_MODE"
    Const colVERIFICATION_REMARKS As String = "colVERIFICATION_REMARKS"

#End Region

#Region "Emp Quli"
    Const colQuliLineNo As String = "QuliLineNo"
    Const colQuliCourseCode As String = "QuliCourseCode"
    Const colQuliJoiningDate As String = "QuliJoiningDate"
    Const colQuliCompletionDate As String = "QuliCompletionDate"
    Const colQuliGradePercentage As String = "QuliGradePercentage"
    Const colQuliCollegeUniversity As String = "QuliCollegeUniversity"
    Const colQuliDescription As String = "QuliDescription"
    Const colQualVerification_Done As String = "colQualVerification_Done"
    Const colUniversity_Address As String = "colUniversity_Address"
    Const colUniversity_Website As String = "colUniversity_Website"

#End Region

#Region "Emp Doc"
    Const colDocLineNo As String = "DocLineNo"
    Const colDocCode As String = "DocCode"
    Const colDocSubmitDate As String = "DocSubmitDate"
    Const colDocFileName As String = "DocFileName"
    Const colDocDescription As String = "DocDescription"
    Const colDocBrowse As String = "colDocBrowse"
    Const colDocOpen As String = "colDocOpen"
    Const colDocPath As String = "colDocPath"
#End Region

#Region "Emp Language"
    Const colLangLineNo As String = "LangLineNo"
    Const colLangCode As String = "LangCode"
    Const colLangReadingLevel As String = "LangReadingLevel"
    Const colLangSpeakingLevel As String = "LangSpeakingLevel"
    Const colLangWrittingLevel As String = "LangWrittingLevel"
    Const colLangDescription As String = "LangDescription"
#End Region

#Region "Emp Family"
    Const colFamilyLineNo As String = "FamilyLineNo"
    Const colFamilyMemberName As String = "FamilyMemberName"
    Const colFamilyRelation As String = "FamilyRelation"
    Const colFamilyAge As String = "FamilyAge"
    Const colFamilySex As String = "FamilySex"
    Const colFamilyDescription As String = "FamilyDescription"
    Const colIs_Dependent As String = "colIs_Dependent"
    Const colMember_DOB As String = "colMember_DOB"
    Const colMember_Occupation As String = "colMember_Occupation"
    Const colDependent_Living_With_Emp As String = "colDependent_Living_With_Emp"
    Const colFDContactNo As String = "colFDContactNo"
#End Region
#Region "Allocated Assets"
    Const colEMP_CODE As String = "colEMP_CODE"
    Const colLINE_NO As String = "colLINE_NO"
    Const colASSET_CODE As String = "colASSET_CODE"
    Const colASSET_NAME As String = "colASSET_NAME"
    Const colALLOCATE_DATE As String = "colALLOCATE_DATE"
    Const colAssetDESCRIPTION As String = "colAssetDESCRIPTION"
    Const colRETURNED As String = "colRETURNED"
#End Region
#End Region

    Private Sub frmEmployee_Master_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        CboGender.DataSource = GetGender()
        CboGender.ValueMember = "Code"
        CboGender.DisplayMember = "Code"

        LoadMaritalStatus()
        LoadEmpStatus()
        LoadAddressType()
        isNewEntry = True
        BlankAllControl()
        LoadPFCalculationType()
        LoadConvenceType()
        loademptype()
        LoadEmpNature()
        LoadEmpBasisType()
        CreateEmpCodeAsPerEmployeeBasisType = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateEmpCodeAsPerEmployeeBasisType, clsFixedParameterCode.CreateEmpCodeAsPerEmployeeBasisType, Nothing)) = "1", True, False)
        AadharNoMandatoryOnEmpMaster = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AadharNoMandatoryOnEmpMaster, clsFixedParameterCode.AadharNoMandatoryOnEmpMaster, Nothing)) = "1", True, False)
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        AddNew()
    End Sub

    Public Function GetGender() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Male"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Female"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Public Sub LoadMaritalStatus()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Single"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Married"
        dt.Rows.Add(dr)

        CboMaritalStatus.DataSource = dt
        CboMaritalStatus.ValueMember = "Code"
        CboMaritalStatus.DisplayMember = "Code"
    End Sub

    Public Sub LoadEmpStatus()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Active"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Inactive"
        dt.Rows.Add(dr)

        CboEmployeeStatus.DataSource = dt
        CboEmployeeStatus.ValueMember = "Code"
        CboEmployeeStatus.DisplayMember = "Code"
    End Sub

    Public Sub LoadEmpBasisType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "PB"
        dr("Name") = "Permanent Basis"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CB"
        dr("Name") = "Contract Basis "
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "DB"
        dr("Name") = "Daily Basis"
        dt.Rows.Add(dr)

        cboemployeebasistype.DataSource = dt
        cboemployeebasistype.ValueMember = "Code"
        cboemployeebasistype.DisplayMember = "Name"
    End Sub

    Public Sub LoadAddressType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Owned"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Rented"
        dt.Rows.Add(dr)

        cboAdd1_Type.DataSource = dt.Copy()
        cboAdd1_Type.ValueMember = "Code"
        cboAdd1_Type.DisplayMember = "Code"


        cboAdd2_Type.DataSource = dt.Copy()
        cboAdd2_Type.ValueMember = "Code"
        cboAdd2_Type.DisplayMember = "Code"
    End Sub


    Public Sub LoadConvenceType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Other"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Two Wheeler"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "Four Wheeler"
        dt.Rows.Add(dr)

        cboConveyanceType.DataSource = dt
        cboConveyanceType.ValueMember = "Code"
        cboConveyanceType.DisplayMember = "Code"
    End Sub

    Public Sub loademptype()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SalesMan"
        dr("Name") = "SalesMan"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Halper"
        dr("Name") = "Halper"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "Other"
        dr("Name") = "Other"
        dt.Rows.Add(dr)

        CboEmployeeType.DataSource = dt
        CboEmployeeType.ValueMember = "Code"
        CboEmployeeType.DisplayMember = "Name"
    End Sub

    Public Sub LoadEmpNature()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Other"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Permanent"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Contractual"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Casual"
        dt.Rows.Add(dr)

        cboEmpNature.DataSource = dt
        cboEmpNature.ValueMember = "Code"
        cboEmpNature.DisplayMember = "Code"
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Public Sub Save()
        Try
            If AllowToSave() Then
                Dim obj As New clsEmployeeMaster()
                obj.EMP_Band_Code = clsCommon.myCstr(txtEmployeeBand.Value)
                obj.Working_City_Code = clsCommon.myCstr(fndcity.Value)
                obj.EMP_CODE = txtCode.Value
                obj.Emp_Name = txtname.Text
                obj.SEX = clsCommon.myCstr(CboGender.SelectedValue)
                obj.FATHERS_NAME = TxtFathersName.Text
                obj.MOTHERS_NAME = txtMothersName.Text
                obj.MARITAL_STATUS = clsCommon.myCstr(CboMaritalStatus.SelectedValue)
                obj.SPOUSE_NAME = txtSpouseName.Text
                obj.SHIFT_CODE = txtShift.Value
                obj.Designation = TxtDesignation.Value
                obj.SUB_DEPARTMENT_CODE = txtSubDepartment.Value
                obj.DEPARTMENT_CODE = txtDepartment.Value
                obj.OCCUPATION_CODE = txtOccupation.Value
                obj.DEVISION_CODE = txtDivision.Value
                obj.GRADE_CODE = txtGrade.Value
                obj.Comp_Code = txtCompanyCode.Value
                obj.LOCATION_CODE = txtBranch.Value
                obj.ATTENDANCE_CODE = txtAttendance.Value

                If txtConfirmationDate.Checked Then
                    obj.CONFIRMATION_DATE = txtConfirmationDate.Value
                End If
                If txtAnniversaryDate.Checked Then
                    obj.ANNIVERSARY_DATE = txtAnniversaryDate.Value
                End If

                If txtProbationEndDate.Checked Then
                    obj.PROBATION_END_DATE = txtProbationEndDate.Value
                End If
                obj.Birth_date = clsCommon.myCstr(clsCommon.GetPrintDate(txtDOB.Value, "dd/MM/yyyy"))
                obj.Joining_date = clsCommon.myCstr(clsCommon.GetPrintDate(txtJoiningDate.Value, "dd/MM/yyyy"))
                obj.Hold_Slary = chkHoldsalary.Checked
                obj.BLOOD_GROUP = txtBloodGroup.Text
                obj.Emp_Status = clsCommon.myCstr(CboEmployeeStatus.SelectedValue)
                obj.Payroll_Code = txtPayRollCode.Text
                obj.GL_Account = TxtGLAccount.Value
                obj.CAST_CATEGORY_CODE = txtCastCategory.Value
                obj.RELIGION_CODE = txtReligion.Value
                obj.Empty_Ex = txtEmptyEx.Value
                obj.WORKING_LOCATION_CODE = txtWorkingLocation.Value
                obj.UIN_NO = txtUINNo.Text
                obj.WARD_CIRCLE = txtWardCircle.Text
                obj.SALARY_ACCOUNT_CODE = txtSalaryAccount.Value
                obj.ADVANCE_TO_STAFF = txtAdvToStaff.Value
                obj.Card_No = txtcardno.Text
                obj.Add2 = txtPermAddress.Text
                obj.PERMA_COUNTRY_CODE = txtPermCountry.Value
                obj.PERMA_STATE_CODE = txtPermState.Value
                obj.PERMA_CITY_CODE = txtPermCity.Value
                obj.PERMA_PHONE_NO = txtPermPhoneNo.Text
                obj.PERMA_MOBILE_NO = txtPermMobileNo.Text
                obj.PERMA_PIN_CODE = txtPermPostalCode.Text
                obj.ADD1_TEHSIL = txtAdd1_Tehsil.Text
                obj.ADD1_VILLAGE = txtAdd1_Village.Text
                obj.ADD1_POST_OFFICE = txtAdd1_PostOffice.Text
                obj.ADD1_POLICE_STATION = txtAdd1_PoliceStation.Text
                obj.ADD1_TYPE = clsCommon.myCstr(cboAdd1_Type.SelectedValue)
                obj.ADD1_VERIFIED = chkAdd1_Verified.Checked
                obj.ADD1_VERIFIED_REMARKS = txtAdd1_Verfi_Remarks.Text
                obj.Add1 = txtPresentAddress.Text
                obj.PRESENT_COUNTRY_CODE = txtPresentCountry.Value
                obj.PRESENT_STATE_CODE = txtPresentState.Value
                obj.PRESENT_CITY_CODE = txtPresentCity.Value
                obj.Phone = txtPresentPhoneNo.Text
                obj.PRESENT_MOBILE_NO = txtPresentMobileNo.Text
                obj.Pin_Code = txtPresentPostalCode.Text
                obj.ADD2_TEHSIL = txtAdd2_Tehsil.Text
                obj.ADD2_VILLAGE = txtAdd2_Village.Text
                obj.ADD2_POST_OFFICE = txtAdd2_PostOffice.Text
                obj.ADD2_POLICE_STATION = txtAdd2_PoliceStation.Text
                obj.ADD2_TYPE = clsCommon.myCstr(cboAdd2_Type.SelectedValue)
                obj.ADD2_VERIFIED = chkAdd2_Verified.Checked
                obj.ADD2_VERIFIED_REMARKS = txtAdd2_Verifi_Remarks.Text
                obj.PAN_NO = txtPanNo.Text
                obj.EMail_ID = txtEmail.Text
                obj.Adhar_No = txtAadharCard.Text
                obj.Votercard_No = txtVoterCard.Text
                obj.DESCRIPTION = txtRemarks.Text
                obj.PASPORT_NO = txtPassportNo.Text
                obj.ALTERNATE_EMAIL_ID = txtAlternateEmail.Text
                obj.Rationcard_No = txtRationCard.Text
                obj.DL_No = txtDLNo.Text
                obj.SecChequeNoLac1 = txtSecChequeLac1.Text
                obj.SecChequeNoRs100 = txtSecChequeRs100.Text
                If txtResigSubDate.Checked Then
                    obj.RESINATION_SUBMIT_DATE = txtResigSubDate.Value
                End If
                obj.NOTICE_IN_DAYS = txtNoticeInDays.Value
                If txtRelevingDate.Checked Then
                    obj.RELIEVING_DATE = txtRelevingDate.Value
                End If
                obj.NO_DUES = chkNoDues.Checked
                obj.LEAVING_REASON = txtReasonOfLeaving.Text
                obj.ISESI = chkApplyESI.Checked
                If chkApplyESI.Checked Then
                    obj.ESI_NO = txtEsiNo.Text
                    obj.ESI_DISPENSARY = txtESIDispensary.Text
                End If
                obj.ISPF = chkPFApplicable.Checked
                If chkPFApplicable.Checked Then
                    obj.PF_NO = txtPFNo.Text
                    obj.EPF_Rate = txtEPFRate.Value
                    obj.Max_Amount_EPF = txtEPFMaxLimit.Value
                    obj.Pf_Calculation_Type = clsCommon.myCstr(cboPFCalculatnType.SelectedValue)
                    obj.PF_NO_DEPT_FILE = txtPFNoforDeptFile.Text
                    obj.UANNo = clsCommon.myCstr(txtUANNo.Text)
                End If
                obj.EMPLOYMENT_NATURE = clsCommon.myCstr(cboEmpNature.SelectedValue)
                obj.CONV_TYPE = clsCommon.myCstr(cboConveyanceType.SelectedValue)
                obj.MINIMUM_BASIC_SALARY = txtMinBasicSalary.Value
                obj.VENDOR_CODE = Me.fndVendor.Value
                obj.AGENCY_CODE = fndAgent.Value
                obj.AgeForPension = clsCommon.myCdbl(TxtAgeFPen.Value)
                obj.IS_OT_APPL = chkOTApplicable.Checked
                obj.IS_OD_APPL = chkODApplicable.Checked
                obj.DISPLAY_IN_STATUTORY = chkShowInStatory.Checked
                obj.USER_CODE = fndUser.Value
                obj.Emp_type = clsCommon.myCstr(CboEmployeeType.SelectedValue)
                obj.Bank_Branch = txtBankBranch.Text
                obj.PAYMENT_MODE = fndPaymentMode.Value
                obj.Bank_Branch_Name = TxtBankBranchName.Text
                obj.BANK_ACC_NO = txtaccno.Text
                obj.Bank_Name = txtbankname.Text
                obj.BANK_CODE = txtBank.Value
                '' addded by Parteek on 20/09/2018
                obj.BioMetricEmpID = txtBiometricEmpID.Text
                obj.EmpBasisType = clsCommon.myCstr(cboemployeebasistype.SelectedValue)
                ''Family Details
                Try
                    If gvEmpFamily IsNot Nothing AndAlso gvEmpFamily.Rows.Count > 0 Then
                        obj.ObjListEmpFamilieDetails = New List(Of clsEmpFamilieDetails)
                        For Each grow As GridViewRowInfo In gvEmpFamily.Rows
                            If clsCommon.myLen(grow.Cells(colFamilyMemberName).Value) > 0 Then
                                Dim objFamilyTr As New clsEmpFamilieDetails()
                                objFamilyTr.EMP_CODE = clsCommon.myCstr(txtCode.Value)
                                objFamilyTr.LINE_NO = clsCommon.myCdbl(grow.Cells(colFamilyLineNo).Value)
                                objFamilyTr.MEMBER_NAME = clsCommon.myCstr(grow.Cells(colFamilyMemberName).Value)
                                objFamilyTr.RELATION_WITH_EMP = clsCommon.myCstr(grow.Cells(colFamilyRelation).Value)
                                objFamilyTr.MEMBER_AGE = clsCommon.myCdbl(grow.Cells(colFamilyAge).Value)
                                objFamilyTr.MEMBER_SEX = clsCommon.myCstr(grow.Cells(colFamilySex).Value)
                                objFamilyTr.DESCRIPTION = clsCommon.myCstr(grow.Cells(colFamilyDescription).Value)
                                objFamilyTr.IS_DEPENDENT = IIf(clsCommon.myCBool(grow.Cells(colIs_Dependent).Value), "1", "0")
                                If clsCommon.myLen(grow.Cells(colMember_DOB).Value) > 0 Then
                                    objFamilyTr.Member_DOB = clsCommon.myCDate(grow.Cells(colMember_DOB).Value)
                                Else
                                    objFamilyTr.Member_DOB = Nothing
                                End If
                                objFamilyTr.Member_Occupation = clsCommon.myCstr(grow.Cells(colMember_Occupation).Value)
                                objFamilyTr.Dependent_Living_With_Emp = IIf(clsCommon.myCBool(grow.Cells(colDependent_Living_With_Emp).Value), "1", "0")
                                objFamilyTr.FDContactNo = clsCommon.myCstr(grow.Cells(colFDContactNo).Value)
                                obj.ObjListEmpFamilieDetails.Add(objFamilyTr)
                            End If
                        Next
                    End If
                Catch ex As Exception
                    RadPageView1.SelectedPage = txtFamilyAge
                    Throw New Exception(ex.Message)
                End Try




                ''Language Details
                Try
                    If gvEmpLanguage IsNot Nothing AndAlso gvEmpLanguage.Rows.Count > 0 Then
                        obj.ObjListEmpLangDetails = New List(Of clsEmpLanguageDetails)
                        For Each grow As GridViewRowInfo In gvEmpLanguage.Rows
                            If clsCommon.myLen(grow.Cells(colLangCode).Value) > 0 Then
                                Dim objLangTr As New clsEmpLanguageDetails()
                                objLangTr.EMP_CODE = clsCommon.myCstr(txtCode.Value)
                                objLangTr.LINE_NO = clsCommon.myCstr(grow.Cells(colLangLineNo).Value)
                                objLangTr.LANGUAGE_CODE = clsCommon.myCstr(grow.Cells(colLangCode).Value)
                                objLangTr.READING_LEVEL = clsCommon.myCstr(grow.Cells(colLangReadingLevel).Value)
                                objLangTr.WRITTING_LEVEL = clsCommon.myCstr(grow.Cells(colLangWrittingLevel).Value)
                                objLangTr.SPEAKING_LEVEL = clsCommon.myCstr(grow.Cells(colLangSpeakingLevel).Value)
                                objLangTr.DESCRIPTION = clsCommon.myCstr(grow.Cells(colLangDescription).Value)
                                obj.ObjListEmpLangDetails.Add(objLangTr)
                            End If
                        Next
                    End If
                Catch ex As Exception
                    RadPageView1.SelectedPage = Languages
                    Throw New Exception(ex.Message)
                End Try





                ''Qulification Details
                Try
                    If gvEmpQuli IsNot Nothing AndAlso gvEmpQuli.Rows.Count > 0 Then
                        obj.ObjListEmpQualiDetails = New List(Of clsEmpQualiDetails)
                        For Each grow As GridViewRowInfo In gvEmpQuli.Rows
                            If clsCommon.myLen(grow.Cells(colQuliCourseCode).Value) > 0 Then
                                Dim objExTr As New clsEmpQualiDetails()
                                objExTr.EMP_CODE = clsCommon.myCstr(txtCode.Value)
                                objExTr.LINE_NO = clsCommon.myCstr(grow.Cells(colQuliLineNo).Value)
                                objExTr.COURSE_CODE = clsCommon.myCstr(grow.Cells(colQuliCourseCode).Value)
                                If clsCommon.myLen(grow.Cells(colQuliJoiningDate).Value) <= 0 Then
                                    Throw New Exception("Please provide Qualifiaction Join date of " + objExTr.COURSE_CODE)
                                End If
                                objExTr.JOINING_DATE = clsCommon.myCDate(grow.Cells(colQuliJoiningDate).Value)
                                If clsCommon.myLen(grow.Cells(colQuliCompletionDate).Value) <= 0 Then
                                    Throw New Exception("Please provide Qualifiaction Complete date of " + objExTr.COURSE_CODE)
                                End If
                                objExTr.COMPLETION_DATE = clsCommon.myCDate(grow.Cells(colQuliCompletionDate).Value)
                                objExTr.COLLEGE_UNIVERSITY = clsCommon.myCstr(grow.Cells(colQuliCollegeUniversity).Value)
                                objExTr.GRADE_PERCENTAGE = clsCommon.myCstr(grow.Cells(colQuliGradePercentage).Value)
                                objExTr.DESCRIPTION = clsCommon.myCstr(grow.Cells(colQuliDescription).Value)
                                objExTr.VERIFICATION_DONE = IIf(clsCommon.myCBool(grow.Cells(colQualVerification_Done).Value), "Y", "N")
                                objExTr.University_Address = clsCommon.myCstr(grow.Cells(colUniversity_Address).Value)
                                objExTr.University_Website = clsCommon.myCstr(grow.Cells(colUniversity_Website).Value)
                                obj.ObjListEmpQualiDetails.Add(objExTr)
                            End If
                        Next
                    End If
                Catch ex As Exception
                    RadPageView1.SelectedPage = Qualification
                    Throw New Exception(ex.Message)
                End Try





                ''Exp Details
                Try
                    If gvEmpEx IsNot Nothing AndAlso gvEmpEx.Rows.Count > 0 Then
                        obj.ObjListEmpExpDetails = New List(Of clsEmpExpDetails)
                        For Each grow As GridViewRowInfo In gvEmpEx.Rows
                            If clsCommon.myLen(grow.Cells(colEmployerName).Value) > 0 Then
                                Dim objExTr As New clsEmpExpDetails()
                                objExTr.EMP_CODE = clsCommon.myCstr(txtCode.Value)

                                objExTr.LINE_NO = clsCommon.myCstr(grow.Cells(colLineNo).Value)
                                objExTr.EMPLOYER_NAME = clsCommon.myCstr(grow.Cells(colEmployerName).Value)
                                objExTr.EMPLOYER_ADDRESS = clsCommon.myCstr(grow.Cells(colEmployerAddress).Value)
                                If clsCommon.myLen(grow.Cells(colJoiningDate).Value) <= 0 Then
                                    Throw New Exception("Please provide experience Joining date of employer " + objExTr.EMPLOYER_NAME)
                                End If

                                objExTr.JOINING_DATE = clsCommon.myCDate(grow.Cells(colJoiningDate).Value)
                                If clsCommon.myLen(grow.Cells(colJoinDesi).Value) <= 0 Then
                                    Throw New Exception("Please provide experience Joining Designation of employer " + objExTr.EMPLOYER_NAME)
                                End If
                                objExTr.JOINING_DESIGNATION_ID = clsCommon.myCstr(grow.Cells(colJoinDesi).Value)
                                objExTr.JOINING_SALARY = clsCommon.myCdbl(grow.Cells(colJoinSalary).Value)
                                If clsCommon.myLen(grow.Cells(colLeavingDate).Value) <= 0 Then
                                    Throw New Exception("Please provide experience leaving date of employer " + objExTr.EMPLOYER_NAME)
                                End If
                                objExTr.LEAVING_DATE = clsCommon.myCDate(grow.Cells(colLeavingDate).Value)
                                objExTr.LEAVING_SALARY = clsCommon.myCdbl(grow.Cells(colLeavingSalary).Value)
                                If clsCommon.myLen(grow.Cells(colLeavDesi).Value) <= 0 Then
                                    Throw New Exception("Please provide experience Leaving Designation of employer " + objExTr.EMPLOYER_NAME)
                                End If
                                objExTr.LEAVING_DESIGNATION_ID = clsCommon.myCstr(grow.Cells(colLeavDesi).Value)
                                objExTr.ACHIEVEMENTS = clsCommon.myCstr(grow.Cells(colAchievements).Value)
                                objExTr.DESCRIPTION = clsCommon.myCstr(grow.Cells(colDescription).Value)
                                objExTr.VERIFICATION_DONE = IIf(clsCommon.myCBool(grow.Cells(colExpVerification_Done).Value), "Y", "N")
                                objExTr.Reporting_Person_Name = clsCommon.myCstr(grow.Cells(colReporting_Person_Name).Value)
                                objExTr.Reporting_Person_Designation = clsCommon.myCstr(grow.Cells(colReporting_Person_Designation).Value)
                                objExTr.Reporting_Person_Phone = clsCommon.myCstr(grow.Cells(colReporting_Person_Phone).Value)
                                objExTr.Reporting_Person_Mobile = clsCommon.myCstr(grow.Cells(colReporting_Person_Mobile).Value)
                                objExTr.Reporting_Person_Email = clsCommon.myCstr(grow.Cells(colReporting_Person_Email).Value)
                                If clsCommon.myLen(grow.Cells(colVERIFICATION_STATUS).Value) <= 0 Then
                                    Throw New Exception("Please provide Verification status of employer " + objExTr.EMPLOYER_NAME)
                                End If
                                objExTr.VERIFICATION_STATUS = clsCommon.myCstr(grow.Cells(colVERIFICATION_STATUS).Value)

                                If clsCommon.myLen(grow.Cells(colVERIFICATION_MODE).Value) <= 0 Then
                                    Throw New Exception("Please provide experience varifacation mode of employer " + objExTr.EMPLOYER_NAME)
                                End If
                                objExTr.VERIFICATION_MODE = clsCommon.myCstr(grow.Cells(colVERIFICATION_MODE).Value)
                                objExTr.VERIFICATION_REMARKS = clsCommon.myCstr(grow.Cells(colVERIFICATION_REMARKS).Value)
                                obj.ObjListEmpExpDetails.Add(objExTr)
                            End If
                        Next
                    End If
                Catch ex As Exception
                    RadPageView1.SelectedPage = Experience
                    Throw New Exception(ex.Message)
                End Try




                '' document details
                Try
                    If gvEmpDoc IsNot Nothing AndAlso gvEmpDoc.Rows.Count > 0 Then
                        Dim objDocTr As clsEmpDocuments
                        obj.ObjListEmpDocuments = New List(Of clsEmpDocuments)
                        For Each row As GridViewRowInfo In gvEmpDoc.Rows
                            If clsCommon.myLen(row.Cells(colDocCode).Value) > 0 Then
                                If clsCommon.myLen(row.Cells(colDocCode).Value) > 0 Then
                                    objDocTr = New clsEmpDocuments
                                    objDocTr.EMP_CODE = txtCode.Value
                                    objDocTr.LINE_NO = row.Cells(colDocLineNo).Value
                                    objDocTr.DOCUMENT_CODE = row.Cells(colDocCode).Value
                                    objDocTr.DocName = row.Cells(colDocDescription).Value
                                    objDocTr.DESCRIPTION = row.Cells(colDocDescription).Value
                                    objDocTr.SUBMIT_DATE = row.Cells(colDocSubmitDate).Value
                                    objDocTr.DocName = System.IO.Path.GetFileName(row.Cells(colDocBrowse).Value)
                                    If clsCommon.myLen(row.Cells(colDocBrowse).Value) > 0 Then
                                        Dim bData As Byte()
                                        Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(row.Cells(colDocBrowse).Value))
                                        bData = br.ReadBytes(br.BaseStream.Length)
                                        objDocTr.DOCUMENT_FILE = bData
                                        br.Close() ' done by stuti reagrding memory leakage
                                    Else
                                        objDocTr.DOCUMENT_FILE = row.Cells(colDocOpen).Tag
                                    End If
                                    obj.ObjListEmpDocuments.Add(objDocTr)
                                End If
                            End If
                        Next
                    End If
                Catch ex As Exception
                    RadPageView1.SelectedPage = Documents
                    Throw New Exception(ex.Message)
                End Try


                '' asset details: KDIL new requirement
                Try
                    If gvAssets IsNot Nothing AndAlso gvAssets.Rows.Count > 0 Then
                        Dim objAssetTr As clsEmpAssets
                        obj.ObjListEmpAssets = New List(Of clsEmpAssets)
                        For Each row As GridViewRowInfo In gvAssets.Rows
                            If clsCommon.myLen(row.Cells(colASSET_CODE).Value) > 0 Then
                                If clsCommon.myLen(row.Cells(colASSET_CODE).Value) > 0 Then
                                    objAssetTr = New clsEmpAssets
                                    objAssetTr.LINE_NO = row.Cells(colLINE_NO).Value
                                    objAssetTr.ASSET_CODE = row.Cells(colASSET_CODE).Value
                                    objAssetTr.ASSET_NAME = row.Cells(colASSET_NAME).Value
                                    If clsCommon.myLen(row.Cells(colALLOCATE_DATE).Value) > 0 Then
                                        objAssetTr.ALLOCATE_DATE = row.Cells(colALLOCATE_DATE).Value
                                    Else
                                        objAssetTr.ALLOCATE_DATE = Nothing
                                    End If
                                    objAssetTr.DESCRIPTION = row.Cells(colAssetDESCRIPTION).Value
                                    objAssetTr.EMP_CODE = txtCode.Value
                                    objAssetTr.RETURNED = row.Cells(colRETURNED).Value
                                    obj.ObjListEmpAssets.Add(objAssetTr)
                                End If
                            End If
                        Next
                    End If
                Catch ex As Exception
                    RadPageView1.SelectedPage = RadPageViewPage1
                    Throw New Exception(ex.Message)
                End Try



                If grpFranchise.Visible Then
                    obj.strFranchiseCode = txtFranchiseCode.Value.ToString.Trim
                Else
                    obj.strFranchiseCode = ""
                End If
                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.EMP_CODE, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        BlankAllControl()

        Dim obj As New clsEmployeeMaster()
        obj = clsEmployeeMaster.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0) Then
            txtCode.MyReadOnly = True
            btnsave.Enabled = True
            btndelete.Enabled = True
            isNewEntry = False
            btnsave.Text = "Update"

            txtEmployeeBand.Value = obj.EMP_Band_Code
            fndcity.Value = obj.Working_City_Code
            txtCode.Value = obj.EMP_CODE
            txtname.Text = obj.Emp_Name
            CboGender.SelectedValue = obj.SEX
            TxtFathersName.Text = obj.FATHERS_NAME
            txtMothersName.Text = obj.MOTHERS_NAME
            CboMaritalStatus.SelectedValue = obj.MARITAL_STATUS
            txtSpouseName.Text = obj.SPOUSE_NAME
            txtShift.Value = obj.SHIFT_CODE
            TxtDesignation.Value = obj.Designation
            txtSubDepartment.Value = obj.SUB_DEPARTMENT_CODE
            txtDepartment.Value = obj.DEPARTMENT_CODE
            txtOccupation.Value = obj.OCCUPATION_CODE
            txtDivision.Value = obj.DEVISION_CODE
            txtGrade.Value = obj.GRADE_CODE
            txtCompanyCode.Value = obj.Comp_Code
            txtBranch.Value = obj.LOCATION_CODE
            txtAttendance.Value = obj.ATTENDANCE_CODE
            If obj.CONFIRMATION_DATE IsNot Nothing Then
                txtConfirmationDate.Checked = True
                txtConfirmationDate.Value = obj.CONFIRMATION_DATE
            End If
            If obj.ANNIVERSARY_DATE IsNot Nothing Then
                txtAnniversaryDate.Checked = True
                txtAnniversaryDate.Value = obj.ANNIVERSARY_DATE
            End If

            If obj.PROBATION_END_DATE IsNot Nothing Then
                txtProbationEndDate.Checked = True
                txtProbationEndDate.Value = obj.PROBATION_END_DATE
            End If

            txtDOB.Value = obj.Birth_date
            txtJoiningDate.Value = obj.Joining_date
            chkHoldsalary.Checked = obj.Hold_Slary
            txtBloodGroup.Text = obj.BLOOD_GROUP
            CboEmployeeStatus.SelectedValue = obj.Emp_Status
            txtPayRollCode.Text = obj.Payroll_Code
            TxtGLAccount.Value = obj.GL_Account
            txtCastCategory.Value = obj.CAST_CATEGORY_CODE
            txtReligion.Value = obj.RELIGION_CODE
            txtEmptyEx.Value = obj.Empty_Ex
            txtWorkingLocation.Value = obj.WORKING_LOCATION_CODE
            txtUINNo.Text = obj.UIN_NO
            txtWardCircle.Text = obj.WARD_CIRCLE
            txtSalaryAccount.Value = obj.SALARY_ACCOUNT_CODE
            txtAdvToStaff.Value = obj.ADVANCE_TO_STAFF
            txtcardno.Text = obj.Card_No
            txtPermAddress.Text = obj.Add2
            txtPermCountry.Value = obj.PERMA_COUNTRY_CODE
            txtPermState.Value = obj.PERMA_STATE_CODE
            txtPermCity.Value = obj.PERMA_CITY_CODE
            txtPermPhoneNo.Text = obj.PERMA_PHONE_NO
            txtPermMobileNo.Text = obj.PERMA_MOBILE_NO
            txtPermPostalCode.Text = obj.PERMA_PIN_CODE
            txtAdd1_Tehsil.Text = obj.ADD1_TEHSIL
            txtAdd1_Village.Text = obj.ADD1_VILLAGE
            txtAdd1_PostOffice.Text = obj.ADD1_POST_OFFICE
            txtAdd1_PoliceStation.Text = obj.ADD1_POLICE_STATION
            cboAdd1_Type.SelectedValue = obj.ADD1_TYPE
            chkAdd1_Verified.Checked = obj.ADD1_VERIFIED
            txtAdd1_Verfi_Remarks.Text = obj.ADD1_VERIFIED_REMARKS
            txtPresentAddress.Text = obj.Add1
            txtPresentCountry.Value = obj.PRESENT_COUNTRY_CODE
            txtPresentState.Value = obj.PRESENT_STATE_CODE
            txtPresentCity.Value = obj.PRESENT_CITY_CODE
            txtPresentPhoneNo.Text = obj.Phone
            txtPresentMobileNo.Text = obj.PRESENT_MOBILE_NO
            txtPresentPostalCode.Text = obj.Pin_Code
            txtAdd2_Tehsil.Text = obj.ADD2_TEHSIL
            txtAdd2_Village.Text = obj.ADD2_VILLAGE
            txtAdd2_PostOffice.Text = obj.ADD2_POST_OFFICE
            txtAdd2_PoliceStation.Text = obj.ADD2_POLICE_STATION
            cboAdd2_Type.SelectedValue = obj.ADD2_TYPE
            chkAdd2_Verified.Checked = obj.ADD2_VERIFIED
            txtAdd2_Verifi_Remarks.Text = obj.ADD2_VERIFIED_REMARKS
            txtPanNo.Text = obj.PAN_NO
            txtEmail.Text = obj.EMail_ID
            txtAadharCard.Text = obj.Adhar_No
            txtVoterCard.Text = obj.Votercard_No
            txtRemarks.Text = obj.DESCRIPTION
            txtPassportNo.Text = obj.PASPORT_NO
            txtAlternateEmail.Text = obj.ALTERNATE_EMAIL_ID
            txtRationCard.Text = obj.Rationcard_No
            txtDLNo.Text = obj.DL_No
            txtSecChequeLac1.Text = obj.SecChequeNoLac1
            txtSecChequeRs100.Text = obj.SecChequeNoRs100
            If obj.RESINATION_SUBMIT_DATE IsNot Nothing Then
                txtResigSubDate.Checked = True
                txtResigSubDate.Value = obj.RESINATION_SUBMIT_DATE
            End If
            txtNoticeInDays.Value = obj.NOTICE_IN_DAYS
            If obj.RELIEVING_DATE IsNot Nothing Then
                txtRelevingDate.Checked = True
                txtRelevingDate.Value = obj.RELIEVING_DATE
            End If
            chkNoDues.Checked = obj.NO_DUES
            txtReasonOfLeaving.Text = obj.LEAVING_REASON
            chkApplyESI.Checked = obj.ISESI
            If obj.ISESI Then
                txtEsiNo.Text = obj.ESI_NO
                txtESIDispensary.Text = obj.ESI_DISPENSARY
            End If
            chkPFApplicable.Checked = obj.ISPF
            If obj.ISPF Then
                txtPFNo.Text = obj.PF_NO
                txtEPFRate.Value = obj.EPF_Rate
                txtEPFMaxLimit.Value = obj.Max_Amount_EPF
                cboPFCalculatnType.SelectedValue = obj.Pf_Calculation_Type
                txtPFNoforDeptFile.Text = obj.PF_NO_DEPT_FILE
                txtUANNo.Text = obj.UANNo
            End If
            cboEmpNature.SelectedValue = obj.EMPLOYMENT_NATURE
            cboConveyanceType.SelectedValue = obj.CONV_TYPE
            txtMinBasicSalary.Value = obj.MINIMUM_BASIC_SALARY
            fndVendor.Value = obj.VENDOR_CODE
            fndAgent.Value = obj.AGENCY_CODE
            TxtAgeFPen.Value = obj.AgeForPension
            chkOTApplicable.Checked = obj.IS_OT_APPL
            chkODApplicable.Checked = obj.IS_OD_APPL
            chkShowInStatory.Checked = obj.DISPLAY_IN_STATUTORY
            fndUser.Value = obj.USER_CODE
            CboEmployeeType.SelectedValue = IIf(obj.Emp_type = "Salesman", "SalesMan", obj.Emp_type)
            txtBankBranch.Text = obj.Bank_Branch
            fndPaymentMode.Value = obj.PAYMENT_MODE
            TxtBankBranchName.Text = obj.Bank_Branch_Name
            txtaccno.Text = obj.BANK_ACC_NO
            txtbankname.Text = obj.Bank_Name
            txtBank.Value = obj.BANK_CODE
            txtBiometricEmpID.Text = obj.BioMetricEmpID
            cboemployeebasistype.SelectedValue = obj.EmpBasisType
            ''Family Details
            If obj.ObjListEmpFamilieDetails IsNot Nothing AndAlso obj.ObjListEmpFamilieDetails.Count > 0 Then
                LoadEmpFamilyGridColumns()
                For Each objFamilyTr As clsEmpFamilieDetails In obj.ObjListEmpFamilieDetails
                    gvEmpFamily.Rows.AddNew()
                    gvEmpFamily.Rows(gvEmpFamily.Rows.Count - 1).Cells(colFamilyLineNo).Value = objFamilyTr.LINE_NO
                    gvEmpFamily.Rows(gvEmpFamily.Rows.Count - 1).Cells(colFamilyAge).Value = objFamilyTr.MEMBER_AGE
                    gvEmpFamily.Rows(gvEmpFamily.Rows.Count - 1).Cells(colFamilyRelation).Value = objFamilyTr.RELATION_WITH_EMP
                    gvEmpFamily.Rows(gvEmpFamily.Rows.Count - 1).Cells(colFamilyMemberName).Value = objFamilyTr.MEMBER_NAME
                    gvEmpFamily.Rows(gvEmpFamily.Rows.Count - 1).Cells(colFamilySex).Value = objFamilyTr.MEMBER_SEX
                    gvEmpFamily.Rows(gvEmpFamily.Rows.Count - 1).Cells(colFamilyDescription).Value = objFamilyTr.DESCRIPTION
                    gvEmpFamily.Rows(gvEmpFamily.Rows.Count - 1).Cells(colIs_Dependent).Value = IIf(clsCommon.CompairString(objFamilyTr.IS_DEPENDENT, "1") = CompairStringResult.Equal, True, False)

                    If Not objFamilyTr.Member_DOB Is Nothing Then
                        gvEmpFamily.Rows(gvEmpFamily.Rows.Count - 1).Cells(colMember_DOB).Value = objFamilyTr.Member_DOB
                    Else
                        gvEmpFamily.Rows(gvEmpFamily.Rows.Count - 1).Cells(colMember_DOB).Value = Nothing
                    End If

                    gvEmpFamily.Rows(gvEmpFamily.Rows.Count - 1).Cells(colMember_Occupation).Value = objFamilyTr.Member_Occupation

                    gvEmpFamily.Rows(gvEmpFamily.Rows.Count - 1).Cells(colDependent_Living_With_Emp).Value = IIf(clsCommon.CompairString(objFamilyTr.Dependent_Living_With_Emp, "1") = CompairStringResult.Equal, True, False)


                    gvEmpFamily.Rows(gvEmpFamily.Rows.Count - 1).Cells(colFDContactNo).Value = objFamilyTr.FDContactNo
                Next
            End If

            ''Language Details
            If obj.ObjListEmpLangDetails IsNot Nothing AndAlso obj.ObjListEmpLangDetails.Count > 0 Then
                LoadEmpLangGridColumns()
                For Each objLangTr As clsEmpLanguageDetails In obj.ObjListEmpLangDetails
                    gvEmpLanguage.Rows.AddNew()
                    gvEmpLanguage.Rows(gvEmpLanguage.Rows.Count - 1).Cells(colLangLineNo).Value = objLangTr.LINE_NO
                    gvEmpLanguage.Rows(gvEmpLanguage.Rows.Count - 1).Cells(colLangCode).Value = objLangTr.LANGUAGE_CODE
                    gvEmpLanguage.Rows(gvEmpLanguage.Rows.Count - 1).Cells(colLangReadingLevel).Value = objLangTr.READING_LEVEL
                    gvEmpLanguage.Rows(gvEmpLanguage.Rows.Count - 1).Cells(colLangWrittingLevel).Value = objLangTr.WRITTING_LEVEL
                    gvEmpLanguage.Rows(gvEmpLanguage.Rows.Count - 1).Cells(colLangSpeakingLevel).Value = objLangTr.SPEAKING_LEVEL
                    gvEmpLanguage.Rows(gvEmpLanguage.Rows.Count - 1).Cells(colLangDescription).Value = objLangTr.DESCRIPTION
                Next
            End If

            ''Qulification Details
            If obj.ObjListEmpQualiDetails IsNot Nothing AndAlso obj.ObjListEmpQualiDetails.Count > 0 Then
                LoadEmpQuliGridColumns()
                For Each objQualiTr As clsEmpQualiDetails In obj.ObjListEmpQualiDetails
                    gvEmpQuli.Rows.AddNew()
                    gvEmpQuli.Rows(gvEmpQuli.Rows.Count - 1).Cells(colQuliLineNo).Value = objQualiTr.LINE_NO
                    gvEmpQuli.Rows(gvEmpQuli.Rows.Count - 1).Cells(colQuliCourseCode).Value = objQualiTr.COURSE_CODE
                    gvEmpQuli.Rows(gvEmpQuli.Rows.Count - 1).Cells(colQuliCollegeUniversity).Value = objQualiTr.COLLEGE_UNIVERSITY
                    gvEmpQuli.Rows(gvEmpQuli.Rows.Count - 1).Cells(colQuliGradePercentage).Value = objQualiTr.GRADE_PERCENTAGE
                    gvEmpQuli.Rows(gvEmpQuli.Rows.Count - 1).Cells(colQuliCompletionDate).Value = objQualiTr.COMPLETION_DATE
                    gvEmpQuli.Rows(gvEmpQuli.Rows.Count - 1).Cells(colQuliJoiningDate).Value = objQualiTr.JOINING_DATE
                    gvEmpQuli.Rows(gvEmpQuli.Rows.Count - 1).Cells(colQuliDescription).Value = objQualiTr.DESCRIPTION
                    gvEmpQuli.Rows(gvEmpQuli.Rows.Count - 1).Cells(colQualVerification_Done).Value = IIf(clsCommon.CompairString(objQualiTr.VERIFICATION_DONE, "Y") = CompairStringResult.Equal, True, False)

                    gvEmpQuli.Rows(gvEmpQuli.Rows.Count - 1).Cells(colUniversity_Address).Value = objQualiTr.University_Address
                    gvEmpQuli.Rows(gvEmpQuli.Rows.Count - 1).Cells(colUniversity_Website).Value = objQualiTr.University_Website
                Next
            End If

            ''Exp Details
            If obj.ObjListEmpExpDetails IsNot Nothing AndAlso obj.ObjListEmpExpDetails.Count > 0 Then
                LoadEmpExGridColumns()
                For Each objExTr As clsEmpExpDetails In obj.ObjListEmpExpDetails
                    gvEmpEx.Rows.AddNew()
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colLineNo).Value = objExTr.LINE_NO
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colEmployerName).Value = objExTr.EMPLOYER_NAME
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colEmployerAddress).Value = objExTr.EMPLOYER_ADDRESS
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colJoinDesi).Value = objExTr.JOINING_DESIGNATION_ID
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colJoiningDate).Value = objExTr.JOINING_DATE
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colJoinSalary).Value = objExTr.JOINING_SALARY
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colLeavDesi).Value = objExTr.LEAVING_DESIGNATION_ID
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colLeavingDate).Value = objExTr.LEAVING_DATE
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colLeavingSalary).Value = objExTr.JOINING_SALARY
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colAchievements).Value = objExTr.ACHIEVEMENTS
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colDescription).Value = objExTr.DESCRIPTION
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colExpVerification_Done).Value = IIf(clsCommon.CompairString(objExTr.VERIFICATION_DONE, "Y") = CompairStringResult.Equal, True, False)

                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colReporting_Person_Name).Value = objExTr.Reporting_Person_Name
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colReporting_Person_Designation).Value = objExTr.Reporting_Person_Designation
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colReporting_Person_Phone).Value = objExTr.Reporting_Person_Phone
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colReporting_Person_Mobile).Value = objExTr.Reporting_Person_Mobile
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colReporting_Person_Email).Value = objExTr.Reporting_Person_Email

                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colVERIFICATION_STATUS).Value = objExTr.VERIFICATION_STATUS
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colVERIFICATION_MODE).Value = objExTr.VERIFICATION_MODE
                    gvEmpEx.Rows(gvEmpEx.Rows.Count - 1).Cells(colVERIFICATION_REMARKS).Value = objExTr.VERIFICATION_REMARKS
                Next
            End If

            ''Document Details
            If obj.ObjListEmpDocuments IsNot Nothing AndAlso obj.ObjListEmpDocuments.Count > 0 Then
                LoadEmpDocGridColumns()
                For Each objDocTr As clsEmpDocuments In obj.ObjListEmpDocuments
                    gvEmpDoc.Rows.AddNew()
                    'BtnDocReset_Click(Nothing, Nothing)
                    gvEmpDoc.Rows(gvEmpDoc.Rows.Count - 1).Cells(colDocLineNo).Value = objDocTr.LINE_NO
                    gvEmpDoc.Rows(gvEmpDoc.Rows.Count - 1).Cells(colDocCode).Value = objDocTr.DOCUMENT_CODE
                    gvEmpDoc.Rows(gvEmpDoc.Rows.Count - 1).Cells(colDocFileName).Value = objDocTr.DocName
                    gvEmpDoc.Rows(gvEmpDoc.Rows.Count - 1).Cells(colDocSubmitDate).Value = objDocTr.SUBMIT_DATE
                    gvEmpDoc.Rows(gvEmpDoc.Rows.Count - 1).Cells(colDocDescription).Value = objDocTr.DESCRIPTION
                    gvEmpDoc.Rows(gvEmpDoc.Rows.Count - 1).Cells(colDocOpen).Tag = objDocTr.DOCUMENT_FILE
                Next
                'gvEmpDoc_SelectionChanged(Nothing, Nothing)
            End If

            ''asset Details
            If obj.ObjListEmpAssets IsNot Nothing AndAlso obj.ObjListEmpAssets.Count > 0 Then
                LoadEmpAssetsGridColumns()
                For Each objAssetTr As clsEmpAssets In obj.ObjListEmpAssets
                    gvAssets.Rows.AddNew()

                    gvAssets.Rows(gvAssets.Rows.Count - 1).Cells(colLINE_NO).Value = objAssetTr.LINE_NO
                    gvAssets.Rows(gvAssets.Rows.Count - 1).Cells(colASSET_CODE).Value = objAssetTr.ASSET_CODE
                    gvAssets.Rows(gvAssets.Rows.Count - 1).Cells(colASSET_NAME).Value = objAssetTr.ASSET_NAME
                    gvAssets.Rows(gvAssets.Rows.Count - 1).Cells(colALLOCATE_DATE).Value = objAssetTr.ALLOCATE_DATE
                    gvAssets.Rows(gvAssets.Rows.Count - 1).Cells(colAssetDESCRIPTION).Value = objAssetTr.DESCRIPTION
                    gvAssets.Rows(gvAssets.Rows.Count - 1).Cells(colRETURNED).Value = objAssetTr.RETURNED
                Next

            End If
            gvEmpFamily.Rows.AddNew()
            gvEmpLanguage.Rows.AddNew()
            gvEmpQuli.Rows.AddNew()
            gvAssets.Rows.AddNew()
            gvEmpDoc.Rows.AddNew()
            gvEmpEx.Rows.AddNew()
            txtCode.MyReadOnly = True

            btnsave.Enabled = True
            btndelete.Enabled = True
            isNewEntry = False
        End If

    End Sub

    Function AllowToSave() As Boolean
        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("Code")
        '    txtCode.Focus()
        '    Return False
        'Else
        '========================shivani[BM00000005685]

        Dim Year As String = clsDBFuncationality.getSingleValue(clsCommon.myCstr("select DATEDIFF(yy,convert(date,'" & txtDOB.Text & "',103),getdate()) "))
        If clsCommon.myLen(txtDOB.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(" Enter Date of birth ")
            txtDOB.Focus()
            Return False
        End If
        If (clsCommon.myCdbl(Year)) <= 17 Then
            common.clsCommon.MyMessageBoxShow(" DOB should be greater than equal to 18")
            txtDOB.Focus()
            Return False
        End If
        If clsCommon.myLen(txtJoiningDate.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(" Enter Date of Joining. ")
            txtJoiningDate.Focus()
            Return False
        End If

        If clsCommon.myLen(txtConfirmationDate.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(" Enter Date of Confirmation ")
            txtConfirmationDate.Focus()
            Return False
        End If
        '==========update by preeti gupta against ticket no[BHA/09/05/19-000885]
        If clsCommon.myLen(txtRelevingDate.Text) > 0 AndAlso txtRelevingDate.Checked Then
            If clsCommon.myCDate(txtRelevingDate.Text) < clsCommon.myCDate(txtJoiningDate.Text) Then
                common.clsCommon.MyMessageBoxShow(" Releaving date should not less than joining date ")
                txtRelevingDate.Focus()
                Return False
            End If
        End If
        If clsCommon.myLen(txtRelevingDate.Text) > 0 AndAlso txtRelevingDate.Checked Then
            If clsCommon.myCDate(txtRelevingDate.Text) < clsCommon.myCDate(txtDOB.Text) Then
                common.clsCommon.MyMessageBoxShow(" Releaving date should not less than DOB ")
                txtRelevingDate.Focus()
                Return False
            End If
        End If

        If clsCommon.myLen(txtJoiningDate.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(" Enter Date of joining ")
            txtJoiningDate.Focus()
            Return False
        End If
        If clsCommon.myCDate(txtDOB.Text) > clsCommon.myCDate(txtJoiningDate.Text) Then
            common.clsCommon.MyMessageBoxShow(" DOB should not Equal or greater than Joining Date")
            txtDOB.Focus()
            Return False
        End If


        If clsCommon.myLen(txtConfirmationDate.Text) > 0 Then
            If clsCommon.myCDate(txtDOB.Text) > clsCommon.myCDate(txtConfirmationDate.Text) Then
                common.clsCommon.MyMessageBoxShow(" DOB should not Equal or greater than Confirmation Date")
                txtDOB.Focus()
                Return False
            End If
        End If
        If clsCommon.myLen(txtLeavingDate.Text) > 0 Then
            If clsCommon.myLen(dtpJoining.Text) > clsCommon.myLen(txtLeavingDate.Text) Then
                common.clsCommon.MyMessageBoxShow(" Employer joining date should not greater than leaving date.")
                dtpJoining.Focus()
                Return False
            End If
        End If


        If clsCommon.myLen(dtpJoining.Text) > clsCommon.myLen(txtJoiningDate.Text) Then
            common.clsCommon.MyMessageBoxShow(" previous employer joining  should not greater than current employer joining date")
            dtpJoining.Focus()
            Return False
        End If

        If clsCommon.myLen(txtLeavingDate.Text) > clsCommon.myLen(txtJoiningDate.Text) Then
            common.clsCommon.MyMessageBoxShow(" previous employer  leaving date should not greater than current employer joining date")
            txtLeavingDate.Focus()
            Return False
        End If

        If clsCommon.myLen(txtDOB.Text) <= 0 Then
            myMessages.blankValue(" Date Of birth")
            txtDOB.Focus()
            Return False
        ElseIf clsCommon.myLen(txtJoiningDate.Text) <= 0 Then
            myMessages.blankValue(" Date Of Join")
            txtJoiningDate.Focus()
            Return False
            '====================
        ElseIf clsCommon.myLen(txtname.Text) <= 0 Then
            myMessages.blankValue(" Name")
            txtname.Focus()
            Return False
        ElseIf clsCommon.myLen(txtAttendance.Value) <= 0 Then
            myMessages.blankValue(" Attendance")
            txtAttendance.Focus()
            Return False
        ElseIf clsCommon.myLen(txtAadharCard.Text) > 12 Then
            clsCommon.MyMessageBoxShow("Length of Aadhar Card No. Should not be Greater than 12")
            txtAadharCard.Focus()
            Return False
        ElseIf clsCommon.myLen(txtDepartment.Value) <= 0 Then
            myMessages.blankValue(" Department ")
            txtDepartment.Focus()
            Return False
        ElseIf CboGender.SelectedIndex <= -1 AndAlso clsCommon.myLen(CboGender.Text) <= 0 Then
            myMessages.blankValue(" Gender ")
            CboGender.Focus()
            Return False
        ElseIf CboMaritalStatus.SelectedIndex <= -1 AndAlso clsCommon.myLen(CboMaritalStatus.Text) <= 0 Then
            myMessages.blankValue(" MaritalStatus")
            CboMaritalStatus.Focus()
            Return False
        ElseIf grpFranchise.Visible Then
            If txtFranchiseCode.Value = "" Then
                clsCommon.MyMessageBoxShow("Please Select Franchise. When Employee Type is Service Dealer it is manadatory to select Franchise")
                txtFranchiseCode.Focus()
                Return False
            End If
        Else
            If isNewEntry = True Then
                If clsCommon.myLen(clsEmployeeMaster.CheckRejoinEmployee(TxtFathersName.Text, txtDOB.Value, txtPresentMobileNo.Text, txtEmail.Text)) > 0 Then
                    If clsCommon.MyMessageBoxShow("An employee with same father name, DOB,Present Mobile No and Email exists in database. Still you want to proceed ?") = Windows.Forms.DialogResult.No Then
                        Return False
                    End If
                End If
            End If

        End If
        'sanjay Ticket No- BHA/24/09/18-000564 Emp code as per employee type
        If CreateEmpCodeAsPerEmployeeBasisType = True Then
            If clsCommon.CompairString(cboemployeebasistype.Text, "Select") = CompairStringResult.Equal Then
                myMessages.blankValue(" Employee basis type ")
                cboemployeebasistype.Focus()
                Return False
            End If
        End If

        If AadharNoMandatoryOnEmpMaster = True Then
            If clsCommon.myLen(txtAadharCard.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow(" Aadhar Card No ")
                txtAadharCard.Focus()
                Return False
            End If
        End If
        If clsCommon.myLen(txtUANNo.Text) > 0 AndAlso clsCommon.myLen(txtUANNo.Text) < 12 Then
            common.clsCommon.MyMessageBoxShow("Length of UAN No. Should be 12")
            txtUANNo.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        'Dim discCode As String
        'discCode = clsDBFuncationality.getSingleValue("select EMP_CODE  from TSPL_SHIPMENT_DETAILS  where EMP_CODE ='" & txtCode.Value & "'")
        'If clsCommon.myLen(discCode) > 0 Then
        '    common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
        '    Exit Sub
        'End If
        '' Code Ends 
        funDelete()
    End Sub

    Sub funDelete()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete(trans) Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If


                If (clsEmployeeMaster.DeleteData(txtCode.Value, trans)) Then
                    saveCancelLog(Reason, "Delete", trans)
                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    BlankAllControl()
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub frmEmployee_Master_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave

    End Sub



    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmEmployee_Master)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        '' Anubhooti 24-July-2014 BM00000003181
        RadMenuItem3.Enabled = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Sub AddNew()
        isNewEntry = True
        btnsave.Text = "Save"
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        BlankAllControl()
        gvEmpFamily.Rows.AddNew()
        gvEmpLanguage.Rows.AddNew()
        gvEmpQuli.Rows.AddNew()
        gvAssets.Rows.AddNew()
        gvEmpDoc.Rows.AddNew()
        gvEmpEx.Rows.AddNew()
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            txtBranch.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
        Else
            txtBranch.Value = ""
        End If
    End Sub

    Sub BlankAllControl()
        RadPageView1.SelectedPage = General
        txtname.Text = ""
        CboGender.SelectedIndex = 0
        TxtFathersName.Text = ""
        txtMothersName.Text = ""
        CboMaritalStatus.SelectedIndex = 0
        txtSpouseName.Text = ""
        txtShift.Value = ""
        TxtDesignation.Value = ""
        txtSubDepartment.Value = ""
        txtDepartment.Value = ""
        txtOccupation.Value = ""
        txtDivision.Value = ""
        txtGrade.Value = ""
        txtEmployeeBand.Value = ""
        fndcity.Value = ""
        txtCompanyCode.Value = ""
        txtBranch.Value = ""
        txtAttendance.Value = ""
        Dim currDate As DateTime = clsCommon.GETSERVERDATE()
        txtConfirmationDate.Checked = False
        txtConfirmationDate.Value = currDate
        txtAnniversaryDate.Checked = False
        txtAnniversaryDate.Value = currDate
        txtProbationEndDate.Checked = False
        txtProbationEndDate.Value = currDate
        txtDOB.Value = currDate
        txtJoiningDate.Value = currDate
        chkHoldsalary.Checked = False
        txtBloodGroup.Text = ""
        CboEmployeeStatus.SelectedIndex = 0
        txtPayRollCode.Text = ""
        TxtGLAccount.Value = ""
        txtCastCategory.Value = ""
        txtReligion.Value = ""
        txtEmptyEx.Value = 0
        txtWorkingLocation.Value = ""
        txtUINNo.Text = ""
        txtWardCircle.Text = ""
        txtSalaryAccount.Value = ""
        txtAdvToStaff.Value = ""
        txtcardno.Text = ""
        txtPermAddress.Text = ""
        txtPermCountry.Value = ""
        txtPermState.Value = ""
        txtPermCity.Value = ""
        txtPermPhoneNo.Text = ""
        txtPermMobileNo.Text = ""
        txtPermPostalCode.Text = ""
        txtAdd1_Tehsil.Text = ""
        txtAdd1_Village.Text = ""
        txtAdd1_PostOffice.Text = ""
        txtAdd1_PoliceStation.Text = ""
        cboAdd1_Type.SelectedIndex = 0
        chkAdd1_Verified.Checked = False
        txtAdd1_Verfi_Remarks.Text = ""
        txtPresentAddress.Text = ""
        txtPresentCountry.Value = ""
        txtPresentState.Value = ""
        txtPresentCity.Value = ""
        txtPresentPhoneNo.Text = ""
        txtPresentMobileNo.Text = ""
        txtPresentPostalCode.Text = ""
        txtAdd2_Tehsil.Text = ""
        txtAdd2_Village.Text = ""
        txtAdd2_PostOffice.Text = ""
        txtAdd2_PoliceStation.Text = ""
        cboAdd2_Type.SelectedIndex = 0
        chkAdd2_Verified.Checked = False
        txtAdd2_Verifi_Remarks.Text = ""
        txtPanNo.Text = ""
        txtEmail.Text = ""
        txtAadharCard.Text = ""
        txtVoterCard.Text = ""
        txtRemarks.Text = ""
        txtPassportNo.Text = ""
        txtAlternateEmail.Text = ""
        txtRationCard.Text = ""
        txtDLNo.Text = ""
        txtResigSubDate.Checked = False
        txtResigSubDate.Value = currDate
        txtNoticeInDays.Value = 0
        txtRelevingDate.Checked = False
        txtRelevingDate.Value = currDate
        chkNoDues.Checked = False
        txtReasonOfLeaving.Text = ""
        chkApplyESI.Checked = False
        txtEsiNo.Text = ""
        txtESIDispensary.Text = ""
        chkPFApplicable.Checked = False
        txtPFNo.Text = ""
        txtEPFRate.Value = 0
        txtEPFMaxLimit.Value = 0
        cboPFCalculatnType.SelectedIndex = 0
        txtPFNoforDeptFile.Text = ""
        cboEmpNature.SelectedIndex = 0
        cboConveyanceType.SelectedIndex = 0
        txtMinBasicSalary.Value = 0
        fndVendor.Value = ""
        fndAgent.Value = ""
        TxtAgeFPen.Value = 0
        chkOTApplicable.Checked = False
        chkODApplicable.Checked = False
        chkShowInStatory.Checked = False
        fndUser.Value = ""
        CboEmployeeType.SelectedIndex = 0
        txtBankBranch.Text = ""
        fndPaymentMode.Value = ""
        TxtBankBranchName.Text = ""
        txtaccno.Text = ""
        txtbankname.Text = ""
        txtBank.Value = ""
        txtBiometricEmpID.Text = ""
        txtSecChequeLac1.Text = ""
        txtSecChequeRs100.Text = ""
        txtUANNo.Text = ""
        isCellValueChangedOpen = False
        cboemployeebasistype.SelectedIndex = 0
        LoadEmpDocGridColumns()
        LoadEmpFamilyGridColumns()
        LoadEmpLangGridColumns()
        LoadEmpQuliGridColumns()
        LoadEmpExGridColumns()
        LoadEmpAssetsGridColumns()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        Dim whrQry As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " LOCATION_CODE='" + LocCode + "'"
                whrQry = " and LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        Dim str As String = "select count(*) from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" + txtCode.Value + "' " + whrQry
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            'Dim qry As String = "select EMP_CODE as Code, Emp_Name as Name, Designation from TSPL_EMPLOYEE_MASTER"
            'txtCode.Value = clsCommon.ShowSelectForm("EMP_MASTER", qry, "Code", "", txtCode.Value, "EMP_CODE", isButtonClicked)
            'txtCode.Value = clsFinder.ShowEmployeeFinder(, , txtCode.Value)
            txtCode.Value = clsEmployeeMaster.getFinder(whrcls, txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                BlankAllControl()
            End If
        End If
    End Sub

    Sub funFill()

    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmEmployee_Master_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

#Region "Employee Exp"



    Private Function GetVerificationStatus() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Positive"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Negative"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Function GetVerificationMode() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Self"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Third Party"
        dt.Rows.Add(dr)

        Return dt
    End Function


    Private Sub gvEmpEx_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvEmpEx.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "Finders"


    Private Sub txtPresentCity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPresentCity._MYValidating
        Dim qry As String = " select City_Code As Code,City_Name  as [City Name]from TSPL_City_MASTER "
        txtPresentCity.Value = clsCommon.ShowSelectForm("frmCity", qry, "Code", "", txtPresentCity.Value, "", isButtonClicked)
        'lblCityName.Text = clsCityMaster.GetName(txtCityCode.Value)
    End Sub
    Private Sub txtPresentCountry__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPresentCountry._MYValidating
        Dim qry As String = " select COUNTRY_CODE as Code, COUNTRY_NAME as Name from TSPL_COUNTRY_MASTER "
        txtPresentCountry.Value = clsCommon.ShowSelectForm("COUNTRY_MASTER", qry, "Code", "", txtPresentCountry.Value, "COUNTRY_CODE", isButtonClicked)
        'lblCountry.Text = clsCountryMaster.GetName(TxtCountry.Value, Nothing)
    End Sub
    Private Sub txtPresentState__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPresentState._MYValidating
        Dim qry As String = "select STATE_CODE AS Code, STATE_NAME as Name from TSPL_STATE_MASTER"
        txtPresentState.Value = clsCommon.ShowSelectForm("STATE_MASTER", qry, "Code", "", txtPresentState.Value, "STATE_CODE", isButtonClicked)
        'lblState.Text = clsStateMaster.GetName(TxtState.Value)
    End Sub

    Private Sub txtPermCity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPermCity._MYValidating
        Dim qry As String = " select City_Code As Code,City_Name  as [City Name]from TSPL_City_MASTER "
        txtPermCity.Value = clsCommon.ShowSelectForm("frmCity", qry, "Code", "", txtPermCity.Value, "", isButtonClicked)
        'lblCityName.Text = clsCityMaster.GetName(txtCityCode.Value)
    End Sub
    Private Sub txtPermCountry__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPermCountry._MYValidating
        Dim qry As String = " select COUNTRY_CODE as Code, COUNTRY_NAME as Name from TSPL_COUNTRY_MASTER "
        txtPermCountry.Value = clsCommon.ShowSelectForm("COUNTRY_MASTER", qry, "Code", "", txtPermCountry.Value, "COUNTRY_CODE", isButtonClicked)
        'lblCountry.Text = clsCountryMaster.GetName(TxtCountry.Value, Nothing)
    End Sub
    Private Sub txtPermState__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPermState._MYValidating
        Dim qry As String = "select STATE_CODE AS Code, STATE_NAME as Name from TSPL_STATE_MASTER"
        txtPermState.Value = clsCommon.ShowSelectForm("STATE_MASTER", qry, "Code", "", txtPermState.Value, "STATE_CODE", isButtonClicked)
        'lblState.Text = clsStateMaster.GetName(TxtState.Value)
    End Sub


    Private Sub txtDivision__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDivision._MYValidating
        Dim qry As String = "select DEVISION_CODE as Code, DEVISION_NAME as Name, DESCRIPTION as Description from TSPL_DEVISION_MASTER"
        txtDivision.Value = clsCommon.ShowSelectForm("DEVISION_MASTER", qry, "Code", "", txtDivision.Value, "DEVISION_CODE", isButtonClicked)
    End Sub

    Private Sub TxtDesignation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtDesignation._MYValidating
        Dim qry As String = " select designation_id As Code,designation_desc  as [Description]from TSPL_Designation_MASTER "
        TxtDesignation.Value = clsCommon.ShowSelectForm("fmdesignation", qry, "Code", "", TxtDesignation.Value, "", isButtonClicked)
    End Sub

    Private Sub txtDepartment__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepartment._MYValidating
        Dim qry As String = "select DEPARTMENT_CODE AS Code, DEPARTMENT_NAME AS Name, DESCRIPTION AS Description from TSPL_DEPARTMENT_MASTER"
        txtDepartment.Value = clsCommon.ShowSelectForm("Dep_Master", qry, "Code", "", txtDepartment.Value, "DEPARTMENT_CODE", isButtonClicked)
    End Sub

    Private Sub txtOccupation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtOccupation._MYValidating
        Dim qry As String = "select OCCUPATION_CODE as Code, OCCUPATION_NAME as Name, DESCRIPTION as Description from TSPL_OCCUPATION_MASTER"
        txtOccupation.Value = clsCommon.ShowSelectForm("OCCUPATION_MASTER", qry, "Code", "", txtOccupation.Value, "OCCUPATION_CODE", isButtonClicked)
    End Sub

    Private Sub txtGrade__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtGrade._MYValidating

        Dim qry As String = "select GRADE_CODE as Code, GRADE_NAME as Name, DESCRIPTION from TSPL_GRADE_MASTER"
        txtGrade.Value = clsCommon.ShowSelectForm("GRADE_MASTER", qry, "Code", "", txtGrade.Value, "GRADE_CODE", isButtonClicked)
    End Sub

    Private Sub txtBranch__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBranch._MYValidating
        'Dim qry As String = "select BRANCH_CODE as Code, BRANCH_NAME as Name,RESPONSIBLE_PERSION_NAME as 'Responsible Persion', BRANCH_ADDRESS as 'Branch Address', CITY_CODE as 'City Code', STATE_CODE as 'State Code' , COUNTRY_CODE as 'Country Code', PHONE_NO as 'Phone No',FAX_NO as 'Fax No', EMAIL_ID as 'Email Id'  from TSPL_BRANCH_MASTER"
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " Location_Type='Physical' And LOCATION_CODE='" + LocCode + "'"
            Else
                whrcls = " Location_Type='Physical'"
            End If
        End If
        txtBranch.Value = clsLocation.getFinder(whrcls, Me.txtBranch.Value, isButtonClicked)
        'txtBranch.Value = clsCommon.ShowSelectForm("BRANCH_MASTER", qry, "Code", "", txtBranch.Value, "BRANCH_CODE", isButtonClicked)
    End Sub

    Private Sub txtCompanyCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCompanyCode._MYValidating

    End Sub

    Private Sub txtAttendance__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtAttendance._MYValidating
        Dim qry As String = "select ATTENDANCE_CODE as Code, ATTENDANCE_NAME as Name, DESCRIPTION as Description, SALARY_DEPENDENT_ON_ATTEN as 'Salary Dependency', OT_CODE as 'OT Code' , CALC_SAL_ON as 'Salary Calculation on Days', ATTN_REGISTER_TYPE as 'Attendance Register Type'  from TSPL_ATTENDANCE_MASTER"
        txtAttendance.Value = clsCommon.ShowSelectForm("ATTENDANCE_MASTER", qry, "Code", "", txtAttendance.Value, "ATTENDANCE_CODE", isButtonClicked)
    End Sub

    Private Sub txtBank__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBank._MYValidating
        Dim qry As String = " select bank_code As Code,description  as [Description]from TSPL_Bank_MASTER "
        txtBank.Value = clsCommon.ShowSelectForm("fmBankMaster", qry, "Code", "", txtBank.Value, "bank_code", isButtonClicked)

    End Sub

    Private Sub txtShift__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShift._MYValidating
        Dim qry As String = "select SHIFT_CODE as Code, SHIFT_NAME AS Name, FROM_Time AS 'From Time', TO_Time AS 'To Time'  from TSPL_SHIFT_MASTER"
        txtShift.Value = clsCommon.ShowSelectForm("SHIFT_Master", qry, "Code", "", txtShift.Value, "SHIFT_CODE", isButtonClicked)
    End Sub

    Private Sub TxtGLAccount__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtGLAccount._MYValidating
        Dim qry As String = " SELECT Account_Code AS [Code], Description FROM TSPL_GL_ACCOUNTS "
        TxtGLAccount.Value = clsCommon.ShowSelectForm("GL Accounts", qry, "Code", "", TxtGLAccount.Value, "", isButtonClicked)
    End Sub

    Private Sub txtCastCategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCastCategory._MYValidating
        Dim qry As String = "select CAST_CATEGORY_CODE as Code, CAST_CATEGORY_NAME AS Name, DESCRIPTION AS Description  from TSPL_CAST_CATEGORY_MASTER"
        txtCastCategory.Value = clsCommon.ShowSelectForm("Cast_Master", qry, "Code", "", txtCastCategory.Value, "CAST_CATEGORY_CODE", isButtonClicked)
    End Sub

    Private Sub txtReligion__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReligion._MYValidating
        Dim qry As String = "select RELIGION_CODE as Code, RELIGION_NAME as Name from TSPL_RELIGION_MASTER"
        txtReligion.Value = clsCommon.ShowSelectForm("RELIGION_MASTER", qry, "Code", "", txtReligion.Value, "RELIGION_CODE", isButtonClicked)
    End Sub

#End Region

#Region "Employee Quli"




    Private Sub gvEmpquli_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvEmpQuli.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "Employee doc"

    Sub LoadEmpDocGridColumns()
        'gvEmpDoc.DataSource = Nothing
        gvEmpDoc.Rows.Clear()
        gvEmpDoc.Columns.Clear()
        gvEmpDoc.ReadOnly = False

        Dim DoclineNo As New GridViewTextBoxColumn()
        DoclineNo.FormatString = ""
        DoclineNo.HeaderText = "Line No"
        DoclineNo.Name = colDocLineNo
        DoclineNo.Width = 30
        DoclineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEmpDoc.Columns.Add(DoclineNo)

        Dim DocCode As New GridViewTextBoxColumn()
        DocCode.FormatString = ""
        DocCode.HeaderText = "Document Code"
        DocCode.Name = colDocCode
        DocCode.Width = 100
        DocCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpDoc.Columns.Add(DocCode)

        Dim DocFileName As New GridViewTextBoxColumn()
        DocFileName.FormatString = ""
        DocFileName.HeaderText = "Document Name"
        DocFileName.Name = colDocFileName
        DocFileName.Width = 150
        DocFileName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DocFileName.ReadOnly = True
        gvEmpDoc.Columns.Add(DocFileName)

        Dim DocSubmitDate As New GridViewDateTimeColumn()
        DocSubmitDate.CustomFormat = "dd/MM/yyyy"
        DocSubmitDate.FormatString = "{0:d}"
        DocSubmitDate.HeaderText = "Submit Date"
        DocSubmitDate.Name = colDocSubmitDate
        DocSubmitDate.Width = 80
        DocSubmitDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEmpDoc.Columns.Add(DocSubmitDate)


        Dim DocBrowse As New GridViewBrowseColumn
        DocBrowse.FormatString = ""
        DocBrowse.HeaderText = "Browse"
        DocBrowse.Name = colDocBrowse
        DocBrowse.Width = 80
        DocBrowse.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpDoc.Columns.Add(DocBrowse)

        Dim docOpen As New GridViewCommandColumn
        docOpen.FormatString = ""
        docOpen.HeaderText = "Open"

        docOpen.Name = colDocOpen
        docOpen.Width = 100
        docOpen.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpDoc.Columns.Add(docOpen)

        Dim DocPath As New GridViewTextBoxColumn()
        DocPath.FormatString = ""
        DocPath.HeaderText = "Path"
        DocPath.Name = colDocPath
        DocPath.Width = 10
        DocPath.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DocPath.IsVisible = False
        gvEmpDoc.Columns.Add(DocPath)

        Dim DocDescription As New GridViewTextBoxColumn()
        DocDescription.FormatString = ""
        DocDescription.HeaderText = "Remark"
        DocDescription.Name = colDocDescription
        DocDescription.Width = 100
        DocDescription.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpDoc.Columns.Add(DocDescription)
        'gvEmpDoc.Rows.AddNew()
    End Sub

    Public Sub LoadDocumentGridData()
        LoadEmpDocGridColumns()
        If clsCommon.myLen(txtCode.Value) > 0 Then
            ObjListEmpDocuments = clsEmpDocuments.GetDataForGrid(txtCode.Value, Nothing)
            If ObjListEmpDocuments IsNot Nothing AndAlso ObjListEmpDocuments.Count > 0 Then

                For Each objDocTr As clsEmpDocuments In ObjListEmpDocuments
                    gvEmpDoc.Rows.AddNew()
                    gvEmpDoc.Rows(gvEmpDoc.Rows.Count - 1).Cells(colDocLineNo).Value = objDocTr.LINE_NO
                    gvEmpDoc.Rows(gvEmpDoc.Rows.Count - 1).Cells(colDocCode).Value = objDocTr.DOCUMENT_CODE
                    gvEmpDoc.Rows(gvEmpDoc.Rows.Count - 1).Cells(colDocFileName).Value = objDocTr.DocName
                    gvEmpDoc.Rows(gvEmpDoc.Rows.Count - 1).Cells(colDocSubmitDate).Value = objDocTr.SUBMIT_DATE
                    gvEmpDoc.Rows(gvEmpDoc.Rows.Count - 1).Cells(colDocDescription).Value = objDocTr.DESCRIPTION
                Next
            End If
        End If
        'gvEmpDoc_SelectionChanged(Nothing, Nothing)
    End Sub


    Private Sub gvEmpDoc_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvEmpDoc.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                If e.Column Is gvEmpDoc.Columns(colDocBrowse) Then
                    isCellValueChanged = True
                    'OpenFileDialog1.ShowDialog()
                    'gvEmpDoc.CurrentRow.Cells(colDocPath).Value =

                    isCellValueChanged = False
                End If
                If e.Column Is gvEmpDoc.Columns(colDocCode) Then
                    gvEmpDoc.CurrentRow.Cells(colDocCode).Value = clsDocumentMaster.getFinder("", gvEmpDoc.CurrentRow.Cells(colDocCode).Value, False)
                    If clsCommon.myLen(gvEmpDoc.CurrentRow.Cells(colDocCode).Value) > 0 Then
                        gvEmpDoc.CurrentRow.Cells(colDocFileName).Value = clsDocumentMaster.GetData(gvEmpDoc.CurrentRow.Cells(colDocCode).Value, NavigatorType.Current).Name
                    End If

                End If
                If e.Column Is gvEmpDoc.Columns(colDocOpen) Then
                    btnShowDoc_Click(gvEmpDoc.CurrentRow.Cells(colDocCode).Value, gvEmpDoc.CurrentRow.Cells(colDocBrowse).Value)
                End If
            End If
        End If
    End Sub
    Private Sub gvEmpDoc_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvEmpDoc.CurrentColumnChanged
        If gvEmpDoc.RowCount > 0 Then
            Dim intCurrRow As Integer = gvEmpDoc.CurrentRow.Index
            gvEmpDoc.CurrentRow.Cells(colDocLineNo).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gvEmpDoc.Rows.Count - 1 Then
                gvEmpDoc.Rows.AddNew()
                gvEmpDoc.CurrentRow = gvEmpDoc.Rows(intCurrRow)

            End If
        End If
    End Sub

    Private Sub btnShowDoc_Click(ByVal Doc_Code As String, ByVal DocPath As String)
        If clsCommon.CompairString(Doc_Code, "") = CompairStringResult.Equal And clsCommon.CompairString(DocPath, "") = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow("No document attached.")
            Exit Sub
        End If
        Dim ds_attachment As DataTable
        Dim filename As String = ""
        Dim file_path As String = ""
        Dim file_extn As String = ""

        Try
            If clsCommon.CompairString(DocPath, "") = CompairStringResult.Equal Then
                ds_attachment = New DataTable
                ds_attachment = clsEmpDocuments.GetDocument(txtCode.Value, Doc_Code)
                filename = clsCommon.myCstr(ds_attachment.Rows(0)("DocName"))
                Dim blob As Byte() = ds_attachment.Rows(0)("DOCUMENT_FILE")
                file_path = Application.StartupPath '"C:\ERPTempFolder"
                Dim dir As DirectoryInfo = New DirectoryInfo(file_path)

                If dir.Exists = False Then
                    dir.Create()
                End If
                Dim index As Int16 = filename.LastIndexOf(".")
                file_extn = filename.Substring(index)
                filename = filename.Remove(index)
                filename += (clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yy hh:mm:ss")).ToString()
                filename = filename.Replace(" ", "")
                filename = filename.Replace("/", "_")
                filename = filename.Replace(":", "_")
                Dim fs As FileStream = File.Create(file_path + "\\" + filename + file_extn)
                fs.Write(blob, 0, blob.Length)
                fs.Close()
                System.Diagnostics.Process.Start(file_path + "\\" + filename + file_extn)
            Else
                System.Diagnostics.Process.Start(DocPath)
            End If

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub gvEmpDoc_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gvEmpDoc.DoubleClick
    '    btnShowDoc_Click(gvEmpDoc.CurrentRow.Cells(colDocCode).Value, gvEmpDoc.CurrentRow.Cells(colDocBrowse).Value)
    'End Sub

    Private Sub gvEmpDoc_CommandCellClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvEmpDoc.CommandCellClick
        btnShowDoc_Click(gvEmpDoc.CurrentRow.Cells(colDocCode).Value, gvEmpDoc.CurrentRow.Cells(colDocBrowse).Value)
    End Sub

#End Region

#Region "Employee Language"

    Private Sub gvEmpLang_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs)
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "Employee Family"

    Private Sub gvEmpFamily_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs)
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

#End Region

    Private Sub CboRelation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs)

    End Sub
    Sub demoImport()
        Dim gv As New RadGridView()
        Dim Counter As Int16 = 0
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        'If transportSql.importExcel(gv, "Emp ID", "Employee Name", "Fathers Name", "Mothers Name", "Date of Birth", "Sex", "Marital Status", "Spouse Name", "Date of Joining", "Salary calculate from", "Date of leaving", "Reason for leaving") Then

        If transportSql.importExcel(gv, "Emp ID", "Employee Name", "Fathers Name", "Mothers Name", "Date of Birth", "Sex", "Marital Status", "Spouse Name", "Designation", "Designation Name", "Occupation", "Department", "Department Name", "Sub Department", "Sub Department Name", "Grade", "Branch", "Working City", "Division", "Bank Account No", "Bank Name", "Bank Branch Name", "Bank Branch Description", "Bank Description", "Sal Structure", "Attendance", "Res No", "Res Name", "Road/Street 1", "Locality/Area", "Present City/District", "State", "Present Pincode", "Road/Street 2", "Permanent City/District", "Permanent State", "Permanent Pincode", "E - Mail ID", "STD Code", "Phone", "Mobile", "Date of Joining", "Salary calculate frm", "Date of leaving", "Reason for leaving", "ESI Applicable", "ESI No", "ESI Dispensary", "PF Applicable", "PF No", "PF No for Dept File", "Restrict PF", "Zero Pension", "Zero PT", "PAN", "Ward/Circle", "Director", "Resignation Submit Date", "Notice Period In Days", "Salary Account", "Advance To Staff", "Conveyance Type", "Employment Nature", "Is OT Applicable", "Is OD Applicable", "Show in Statutory", "Minimum Basic Salary", "Vendor Code", "Agency Code", "Age For Pension") Then
            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                'Dim obj As New clsEmployeeMaster()
                For Each grow As GridViewRowInfo In gv.Rows
                    clsCommon.ProgressBarUpdate((grow.Index + 1) & "/" & gv.Rows.Count)
                    If clsCommon.myLen(grow.Cells("Emp ID").Value) > 0 AndAlso clsCommon.myLen(grow.Cells("Employee Name").Value) > 0 Then


                        Dim obj As New clsEmployeeMaster()
                        Counter += 1
                        'Dim Qry As String
                        'Dim check As Integer
                        Dim strCode As String = clsCommon.myCstr(grow.Cells("Emp ID").Value)
                        If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                            'Throw New Exception("Employee id can not be blank or incorrect.")
                            Continue For
                        End If
                        obj.EMP_CODE = strCode

                        Dim strName As String = ""
                        Dim strBBDesp As String = ""
                        Dim strBBName As String = ""

                        strName = clsCommon.myCstr(grow.Cells("Employee Name").Value)
                        If strName.Length > 100 Or (String.IsNullOrEmpty(strName)) Then
                            Throw New Exception("Name can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.Emp_Name = strName

                        strName = clsCommon.myCstr(grow.Cells("Fathers Name").Value)
                        'If strName.Length > 100 Or (String.IsNullOrEmpty(strName)) Then
                        '    Throw New Exception("Fathers Name can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        'End If
                        obj.FATHERS_NAME = strName

                        strName = clsCommon.myCstr(grow.Cells("Mothers Name").Value)
                        'If strName.Length > 100 Then
                        '    Throw New Exception("Mothers Name can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        'End If
                        obj.MOTHERS_NAME = strName

                        Dim Date_date As Date = Nothing
                        If clsCommon.myLen(grow.Cells("Date of Birth")) > 0 Then
                            Date_date = clsCommon.myCDate(grow.Cells("Date of Birth").Value)
                            If Date_date.Year < 1900 Or (String.IsNullOrEmpty(Date_date)) Then
                                Throw New Exception("Date of Birth can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                            End If
                            obj.Birth_date = clsCommon.GetPrintDate(Date_date, "dd/MM/yyyy")
                        Else
                            Throw New Exception("Date of Birth can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Sex").Value)
                        If strName.Length > 10 Or (String.IsNullOrEmpty(strName)) Then
                            Throw New Exception("Sex can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.SEX = strName

                        strName = clsCommon.myCstr(grow.Cells("Marital Status").Value)
                        If strName.Length > 0 Then
                            obj.MARITAL_STATUS = strName
                        Else
                            obj.MARITAL_STATUS = ""
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Spouse Name").Value)
                        If strName.Length > 100 Then
                            Throw New Exception("Spouse Name can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.SPOUSE_NAME = strName

                        strName = clsCommon.myCstr(grow.Cells("Designation").Value)
                        If strName.Length > 0 Then
                            obj.Designation = strName
                        Else
                            obj.Designation = ""
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Occupation").Value)

                        If strName.Length > 0 Then
                            'Qry = "select OCCUPATION_CODE from TSPL_EMPLOYEE_MASTER where OCCUPATION_CODE ='" & strName & "'"
                            'check = clsDBFuncationality.getSingleValue(Qry, trans)
                            'If check <= 0 Then
                            '    Throw New Exception("'" & clsCommon.myCstr(strName) & "' code does not exists at line no. " + clsCommon.myCstr(Counter) + ".Please Check Occupation Code in  Occupation Master")
                            'End If


                            obj.OCCUPATION_CODE = strName
                        Else
                            obj.OCCUPATION_CODE = ""
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Department").Value)

                        'Qry = "select DEPARTMENT_CODE from TSPL_EMPLOYEE_MASTER where DEPARTMENT_CODE ='" & strName & "'"
                        'check = clsDBFuncationality.getSingleValue(Qry, trans)
                        'If check <= 0 Then
                        '    Throw New Exception("'" & clsCommon.myCstr(strName) & "' code does not exists at line no. " + clsCommon.myCstr(Counter) + ".Please Check Department Code in  Department Master")
                        'End If
                        If strName.Length <= 0 Then
                            Throw New Exception("Department can not be left blank")
                        Else
                            obj.DEPARTMENT_CODE = strName
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Sub Department").Value)
                        If strName.Length > 0 Then
                            '    Qry = "select SUB_DEPARTMENT_CODE from TSPL_EMPLOYEE_MASTER where SUB_DEPARTMENT_CODE ='" & strName & "'"
                            '    check = clsDBFuncationality.getSingleValue(Qry, trans)
                            '    If check <= 0 Then
                            '        Throw New Exception("'" & clsCommon.myCstr(strName) & "' code does not exists at line no. " + clsCommon.myCstr(Counter) + ".Please Check Sub Department Code in  Sub Department Master")
                            '    End If

                            obj.SUB_DEPARTMENT_CODE = strName
                        Else
                            obj.SUB_DEPARTMENT_CODE = ""
                        End If


                        strName = clsCommon.myCstr(grow.Cells("Grade").Value)

                        If strName.Length > 0 Then
                            'Qry = "select GRADE_CODE from TSPL_EMPLOYEE_MASTER where GRADE_CODE ='" & strName & "'"
                            'check = clsDBFuncationality.getSingleValue(Qry, trans)
                            'If check <= 0 Then
                            '    Throw New Exception("'" & clsCommon.myCstr(strName) & "' code does not exists at line no. " + clsCommon.myCstr(Counter) + ".Please Check Grade Code in  Grade Master")
                            'End If

                            obj.GRADE_CODE = strName
                        Else
                            obj.GRADE_CODE = ""
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Branch").Value)
                        If strName.Length > 0 Then
                            'Qry = "select LOCATION_CODE from TSPL_EMPLOYEE_MASTER where LOCATION_CODE ='" & strName & "'"
                            'check = clsDBFuncationality.getSingleValue(Qry, trans)
                            'If check <= 0 Then
                            '    Throw New Exception("'" & clsCommon.myCstr(strName) & "' code does not exists at line no. " + clsCommon.myCstr(Counter) + ".Please Check Location Code in  Location Master")
                            'End If

                            obj.LOCATION_CODE = strName
                        Else
                            obj.LOCATION_CODE = ""
                        End If
                        strName = clsCommon.myCstr(grow.Cells("Working City").Value)
                        If strName.Length > 0 Then

                            obj.Working_City_Code = strName
                        Else
                            obj.Working_City_Code = ""
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Division").Value)
                        If strName.Length > 0 Then
                            ''Qry = "select Division from TSPL_EMPLOYEE_MASTER where Division ='" & strName & "'"
                            ''check = clsDBFuncationality.getSingleValue(Qry, trans)
                            ''If check <= 0 Then
                            ''    Throw New Exception("'" & clsCommon.myCstr(strName) & "' code does not exists at line no. " + clsCommon.myCstr(Counter) + ".Please Division Code in  Division Master")
                            ''End If

                            obj.DEVISION_CODE = strName
                        Else
                            obj.DEVISION_CODE = ""
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Bank Account No").Value)
                        If strName.Length > 50 Then
                            Throw New Exception("Bank Account No can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.BANK_ACC_NO = strName

                        strName = clsCommon.myCstr(grow.Cells("Bank Name").Value)
                        If strName.Length > 0 Then
                            'Qry = "select BANK_CODE from TSPL_EMPLOYEE_MASTER where BANK_CODE ='" & strName & "'"
                            'check = clsDBFuncationality.getSingleValue(Qry, trans)
                            'If check <= 0 Then
                            '    Throw New Exception("'" & clsCommon.myCstr(strName) & "' code does not exists at line no. " + clsCommon.myCstr(Counter) + ".Please Bank Code in  Bank Master")
                            'End If

                            obj.BANK_CODE = strName
                        Else
                            obj.BANK_CODE = ""
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Bank Branch Name").Value)
                        If strName.Length > 0 Then
                            obj.Bank_Branch = strName
                        Else
                            obj.Bank_Branch = ""
                        End If
                        '' Bank Branch Description
                        strBBDesp = clsCommon.myCstr(grow.Cells("Bank Branch Description").Value)
                        If strBBDesp.Length > 0 Then
                            obj.Bank_Branch_Name = strBBDesp
                        ElseIf strBBDesp.Length > 100 Then
                            Throw New Exception("Bank branch description can not be more than 100 characters for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        Else
                            obj.Bank_Branch_Name = ""
                        End If
                        '' Bank Name
                        strBBName = clsCommon.myCstr(grow.Cells("Bank Description").Value)
                        If strBBName.Length > 0 Then
                            obj.Bank_Name = strBBName
                        ElseIf strBBName.Length > 100 Then
                            Throw New Exception("Bank description can not be more than 100 characters for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        Else
                            obj.Bank_Name = ""
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Attendance").Value)
                        If strName.Length > 0 Then
                            'Qry = "select ATTENDANCE_CODE from TSPL_EMPLOYEE_MASTER where ATTENDANCE_CODE ='" & strName & "'"
                            'check = clsDBFuncationality.getSingleValue(Qry, trans)
                            'If check <= 0 Then
                            '    Throw New Exception("'" & clsCommon.myCstr(strName) & "' code does not exists at line no. " + clsCommon.myCstr(Counter) + ".Please Attendance Code in  Attendance Master")
                            'End If

                            obj.ATTENDANCE_CODE = strName
                        Else
                            obj.ATTENDANCE_CODE = ""
                        End If

                        Dim add As String = ""
                        strName = clsCommon.myCstr(grow.Cells("Res No").Value)
                        If clsCommon.myCstr(strName).Length > 0 Then
                            add += strName
                        End If
                        strName = clsCommon.myCstr(grow.Cells("Res Name").Value)
                        If clsCommon.myCstr(strName).Length > 0 Then
                            add += strName
                        End If
                        strName = clsCommon.myCstr(grow.Cells("Road/Street 1").Value)
                        If clsCommon.myCstr(strName).Length > 0 Then
                            add += strName
                        End If
                        strName = clsCommon.myCstr(grow.Cells("Locality/Area").Value)
                        If clsCommon.myCstr(strName).Length > 0 Then
                            add += strName
                        End If
                        obj.Add1 = add

                        strName = clsCommon.myCstr(grow.Cells("Present City/District").Value)
                        If strName.Length > 0 Then
                            'Qry = "select PRESENT_CITY_CODE from TSPL_EMPLOYEE_MASTER where PRESENT_CITY_CODE ='" & strName & "'"
                            'check = clsDBFuncationality.getSingleValue(Qry, trans)
                            'If check <= 0 Then
                            '    Throw New Exception("'" & clsCommon.myCstr(strName) & "' code does not exists at line no. " + clsCommon.myCstr(Counter) + ".Please Check City Code in  City Master")
                            'End If


                            obj.PRESENT_CITY_CODE = strName
                        Else
                            obj.PRESENT_CITY_CODE = ""
                        End If

                        strName = clsCommon.myCstr(grow.Cells("State").Value)
                        If strName.Length > 0 Or (String.IsNullOrEmpty(strName)) Then
                            'Qry = "select PRESENT_STATE_CODE from TSPL_EMPLOYEE_MASTER where PRESENT_STATE_CODE ='" & strName & "'"
                            'check = clsDBFuncationality.getSingleValue(Qry, trans)
                            'If check <= 0 Then
                            '    Throw New Exception("'" & clsCommon.myCstr(strName) & "' code does not exists at line no. " + clsCommon.myCstr(Counter) + ".Please Check State Code in  State Master")
                            'End If

                            obj.PRESENT_STATE_CODE = strName
                        Else
                            obj.PRESENT_STATE_CODE = ""
                        End If

                        Dim Phone_no As String = ""
                        strName = clsCommon.myCstr(grow.Cells("STD Code").Value)
                        If strName.Length > 0 Then
                            Phone_no += strName
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Phone").Value)
                        If strName.Length > 0 Then
                            Phone_no += strName
                        End If
                        obj.Phone = Phone_no

                        strName = clsCommon.myCstr(grow.Cells("Mobile").Value)
                        If strName.Length > 14 Then
                            Throw New Exception("Mobile can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.PRESENT_MOBILE_NO = strName

                        strName = clsCommon.myCstr(grow.Cells("Present Pincode").Value)
                        If strName.Length > 6 Then
                            Throw New Exception("Pincode can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.Pin_Code = strName

                        add = ""
                        'strName = clsCommon.myCstr(grow.Cells("Res No1").Value)
                        'If clsCommon.myCstr(strName).Length > 0 Then
                        '    add += strName
                        'End If
                        'strName = clsCommon.myCstr(grow.Cells("Res Name1").Value)
                        'If clsCommon.myCstr(strName).Length > 0 Then
                        '    add += strName
                        'End If
                        strName = clsCommon.myCstr(grow.Cells("Road/Street 2").Value)
                        If clsCommon.myCstr(strName).Length > 0 Then
                            add += strName
                        End If
                        'strName = clsCommon.myCstr(grow.Cells("Locality/Area1").Value)
                        'If clsCommon.myCstr(strName).Length > 0 Then
                        '    add += strName
                        'End If
                        obj.Add2 = add

                        strName = clsCommon.myCstr(grow.Cells("Permanent City/District").Value)
                        'Qry = "select PERMA_CITY_CODE from TSPL_EMPLOYEE_MASTER where PERMA_CITY_CODE ='" & strName & "'"
                        'check = clsDBFuncationality.getSingleValue(Qry, trans)
                        'If check <= 0 Then
                        '    Throw New Exception("'" & clsCommon.myCstr(strName) & "' code does not exists at line no. " + clsCommon.myCstr(Counter) + ".Please Check City Code in  City Master")
                        'End If

                        If strName.Length > 0 Then
                            obj.PERMA_CITY_CODE = strName
                        Else
                            obj.PERMA_CITY_CODE = ""
                        End If


                        strName = clsCommon.myCstr(grow.Cells("Permanent State").Value)
                        If strName.Length > 0 Then
                            'Qry = "select PERMA_STATE_CODE from TSPL_EMPLOYEE_MASTER where PERMA_STATE_CODE ='" & strName & "'"
                            'check = clsDBFuncationality.getSingleValue(Qry, trans)
                            'If check <= 0 Then
                            '    Throw New Exception("'" & clsCommon.myCstr(strName) & "' code does not exists at line no. " + clsCommon.myCstr(Counter) + ".Please Check State Code in  State Master")
                            'End If

                            obj.PERMA_STATE_CODE = strName
                        Else
                            obj.PERMA_STATE_CODE = ""
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Resignation Submit Date").Value)
                        If strName.Length > 0 Then
                            obj.RESINATION_SUBMIT_DATE = clsCommon.GetPrintDate(clsCommon.myCDate(strName), "dd/MMM/yyyy")
                        Else
                            obj.RESINATION_SUBMIT_DATE = Nothing
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Notice Period In Days").Value)
                        If strName.Length > 0 Then
                            obj.NOTICE_IN_DAYS = clsCommon.myCdbl(strName)
                        Else
                            obj.NOTICE_IN_DAYS = 0
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Permanent Pincode").Value)
                        If strName.Length > 6 Then
                            Throw New Exception("Pincode can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.PERMA_PIN_CODE = strName

                        strName = clsCommon.myCstr(grow.Cells("E - Mail ID").Value)
                        If strName.Length > 100 Then
                            Throw New Exception("E - Mail ID can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.EMail_ID = strName

                        strName = clsCommon.myCstr(grow.Cells("Date of Joining").Value)
                        If clsCommon.myLen(strName) <= 0 Then
                            Throw New Exception("Date of Joining can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        Date_date = clsCommon.myCstr(grow.Cells("Date of Joining").Value)
                        If Date_date.Year < 1900 Or (String.IsNullOrEmpty(Date_date)) Then
                            Throw New Exception("Date of Joining can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.Joining_date = clsCommon.GetPrintDate(Date_date, "dd/MM/yyyy")


                        If IsDBNull(grow.Cells("Salary calculate frm").Value) Then
                            Date_date = clsCommon.myCDate(grow.Cells("Date of Joining").Value)
                        Else
                            Date_date = clsCommon.myCDate(grow.Cells("Salary calculate frm").Value)
                        End If
                        If Date_date.Year < 1900 Or (String.IsNullOrEmpty(Date_date)) Then
                            Throw New Exception("Salary calculate frm can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.CONFIRMATION_DATE = clsCommon.GetPrintDate(Date_date, "dd/MM/yyyy")

                        If clsCommon.myCstr(grow.Cells("Date of leaving").Value).Length > 0 Then
                            Date_date = clsCommon.myCDate(grow.Cells("Date of leaving").Value)
                            If Date_date.Year < 1900 Then
                                Throw New Exception("Date of leaving can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                            End If
                            obj.RELIEVING_DATE = clsCommon.GetPrintDate(Date_date, "dd/MM/yyyy")
                        End If

                        If clsCommon.myCstr(grow.Cells("Date of leaving").Value).Length > 0 Then
                            Date_date = clsCommon.myCDate(grow.Cells("Date of leaving").Value)
                            If Date_date.Year < 1900 Then
                                Throw New Exception("Date of leaving can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                            End If
                            obj.RELIEVING_DATE = clsCommon.GetPrintDate(Date_date, "dd/MMM/yyyy")
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Reason for leaving").Value)
                        If strName.Length > 50 Then
                            Throw New Exception("Reason for leaving can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.LEAVING_REASON = strName

                        strName = clsCommon.myCstr(grow.Cells("ESI Applicable").Value)
                        If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                            obj.ISESI = True
                        Else
                            obj.ISESI = False
                        End If

                        strName = clsCommon.myCstr(grow.Cells("ESI No").Value)
                        If strName.Length > 50 Then
                            Throw New Exception("ESI No can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.ESI_NO = strName

                        strName = clsCommon.myCstr(grow.Cells("ESI Dispensary").Value)
                        If strName.Length > 100 Then
                            Throw New Exception("ESI Dispensary can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.ESI_DISPENSARY = strName

                        strName = clsCommon.myCstr(grow.Cells("PF Applicable").Value)
                        If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                            obj.ISPF = True
                        Else
                            obj.ISPF = False
                        End If

                        strName = clsCommon.myCstr(grow.Cells("PF No").Value)
                        If strName.Length > 50 Then
                            Throw New Exception("PF No can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.PF_NO = strName

                        strName = clsCommon.myCstr(grow.Cells("PF No for Dept File").Value)
                        If strName.Length > 50 Then
                            Throw New Exception("PF No for Dept File can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.PF_NO_DEPT_FILE = strName

                        strName = clsCommon.myCstr(grow.Cells("Restrict PF").Value)
                        If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                            obj.ISRESTRICT_PF = True
                        Else
                            obj.ISRESTRICT_PF = False
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Zero Pension").Value)
                        If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                            obj.ISZERO_PENSION = True
                        Else
                            obj.ISZERO_PENSION = False
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Zero PT").Value)
                        If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                            obj.ISZERO_PT = True
                        Else
                            obj.ISZERO_PT = False
                        End If

                        'strName = clsCommon.myCstr(grow.Cells("PAN").Value)
                        'If strName.Length > 50 Then
                        '    Throw New Exception("PAN No can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        'End If
                        'obj.PAN_NO = strName

                        strName = clsCommon.myCstr(grow.Cells("Ward/Circle").Value)
                        If strName.Length > 50 Then
                            Throw New Exception("Ward/Circle can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.WARD_CIRCLE = strName

                        strName = clsCommon.myCstr(grow.Cells("Director").Value)
                        If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                            obj.ISDIRECTOR = True
                        Else
                            obj.ISDIRECTOR = False
                        End If

                        '' panch raj 14-oct-2014 
                        Dim Salary_Acc As String = ""
                        Dim Adv_To As String = ""

                        Dim Salary_Acc_Code As String
                        Dim Account_To_Staff As String

                        Salary_Acc = clsCommon.myCstr(grow.Cells("Salary Account").Value)
                        If Salary_Acc.Length > 30 Then
                            'obj.SALARY_ACCOUNT_CODE = strName
                            Throw New Exception("Salary Account Code length can not be greater than 30")
                        Else
                            If Salary_Acc.Length > 0 Then
                                Salary_Acc_Code = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_PAYROLL_ACCOUNTSETS Where ACCOUNT_SET_CODE ='" & Salary_Acc & "'", trans)
                                If Salary_Acc_Code <= 0 Then
                                    Throw New Exception("Salary Account Code(" & Salary_Acc & ") does not exist . Please make it entry first.")
                                End If
                            End If
                        End If
                        obj.SALARY_ACCOUNT_CODE = Salary_Acc

                        Adv_To = clsCommon.myCstr(grow.Cells("Advance To Staff").Value)
                        If Adv_To.Length > 50 Then
                            Throw New Exception("Advance To Staff length can not be greater than 50")
                        Else
                            If Adv_To.Length > 0 Then
                                Account_To_Staff = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_GL_ACCOUNTS Where Account_Code ='" & Adv_To & "'", trans)
                                If Account_To_Staff <= 0 Then
                                    Throw New Exception("Advance To Staff(" & Adv_To & ") does not exist . Please make it entry first.")
                                End If
                            End If
                        End If
                        obj.ADVANCE_TO_STAFF = Adv_To
                        '' for kdil and viney

                        strName = clsCommon.myCstr(grow.Cells("Conveyance Type").Value)
                        If clsCommon.CompairString(strName, "TW") = CompairStringResult.Equal Then
                            obj.CONV_TYPE = strName
                        ElseIf clsCommon.CompairString(strName, "FW") = CompairStringResult.Equal Then
                            obj.CONV_TYPE = strName
                        ElseIf clsCommon.CompairString(strName, "None") = CompairStringResult.Equal Then
                            obj.CONV_TYPE = strName
                        Else
                            obj.CONV_TYPE = "None"
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Employment Nature").Value)
                        If clsCommon.CompairString(strName, "Permanent") = CompairStringResult.Equal Then
                            obj.CONV_TYPE = strName
                        ElseIf clsCommon.CompairString(strName, "Contractual") = CompairStringResult.Equal Then
                            obj.CONV_TYPE = strName
                        ElseIf clsCommon.CompairString(strName, "Other") = CompairStringResult.Equal Then
                            obj.CONV_TYPE = strName
                        Else
                            obj.CONV_TYPE = "Other"
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Is OT Applicable").Value)
                        If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                            obj.IS_OT_APPL = True
                        Else
                            obj.IS_OT_APPL = False
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Is OD Applicable").Value)
                        If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                            obj.IS_OD_APPL = True
                        Else
                            obj.IS_OD_APPL = False
                        End If

                        strName = clsCommon.myCstr(grow.Cells("Show in Statutory").Value)
                        If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                            obj.DISPLAY_IN_STATUTORY = True
                        Else
                            obj.DISPLAY_IN_STATUTORY = False
                        End If

                        strName = clsCommon.myCdbl(grow.Cells("Minimum Basic Salary").Value)
                        obj.MINIMUM_BASIC_SALARY = strName

                        strCode = clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                        If strCode.Length > 30 Then
                            Throw New Exception("Vendor Code length can not be greater than 30")
                        Else
                            If strCode.Length > 0 Then
                                strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM tspl_vendor_master Where vendor_code ='" & strCode & "'", trans)
                                If strName <= 0 Then
                                    Throw New Exception("Vendor Code (" & strCode & ") does not exist . Please make it entry first.")
                                End If
                            End If
                        End If
                        obj.VENDOR_CODE = strCode

                        strCode = clsCommon.myCstr(grow.Cells("Agency Code").Value)
                        If strCode.Length > 30 Then
                            Throw New Exception("Agency Code length can not be greater than 30")
                        Else
                            If strCode.Length > 0 Then
                                strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM Tspl_HR_Agency_Master Where code ='" & strCode & "'", trans)
                                If strName <= 0 Then
                                    Throw New Exception("Agency Code (" & strCode & ") does not exist . Please make it entry first.")
                                End If
                            End If
                        End If
                        obj.AGENCY_CODE = strCode

                        '' user code
                        strCode = clsCommon.myCstr(grow.Cells("User Code").Value)
                        If strCode.Length > 30 Then
                            Throw New Exception("User Code length can not be greater than 30")
                        Else
                            If strCode.Length > 0 Then
                                strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_USER_MASTER Where USER_CODE ='" & strCode & "'", trans)
                                If strName <= 0 Then
                                    Throw New Exception("User Code (" & strCode & ") does not exist . Please make it entry first.")
                                End If
                            End If
                        End If
                        obj.USER_CODE = strCode

                        Dim AgeFPen As Double = clsCommon.myCdbl(grow.Cells("Age For Pension").Value)
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Age For Pension").Value)) > 0 Then
                            If clsCommon.myLen(AgeFPen) > 0 Then
                                If clsCommon.myCdbl(AgeFPen) < 0 Then
                                    Throw New Exception("Age for pension should be numeric")
                                End If
                                If clsCommon.myLen(AgeFPen) <> 2 Then
                                    Throw New Exception("Age for pension should be in 2 digits")
                                End If
                            End If
                        End If

                        obj.AgeForPension = AgeFPen
                        obj.Emp_Status = "Active"
                        '' end kdil and viney
                        obj.SaveDataFromExcelSheet(obj, True)
                        grow = Nothing
                        obj = Nothing
                        'strName = String.Empty
                        'strCode = String.Empty
                    End If
                Next
                'Dim ds As DataSet = gv.DataSource
                ''Dim bulkcopy As SqlBulkCopy
                'Using bulkcopy = New SqlBulkCopy(clsDBFuncationality.GetConnnection)

                '    Try
                '        bulkcopy.DestinationTableName = "dbo.TestTable"
                '        bulkcopy.WriteToServer(ds.Tables(0))
                '    Catch ex As Exception
                '        Console.WriteLine(ex.Message)
                '    End Try
                'End Using

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(ex.Message.ToString)
                clsCommon.ProgressBarHide()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemImport.Click

        Import()


    End Sub
    '' changes by shivani against[8264]
    '==update by preeti gupta Against ticket no[GKD/06/03/19-000178]
    Sub Import()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim i As Integer = 0
        connectSql.OpenConnection()
        Dim strdate As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        If transportSql.importExcel(gv, "Emp ID", "Employee Name", "Fathers Name", "Mothers Name", "Date of Birth", "Sex", "Marital Status", "Spouse Name", "Designation", "Designation Name", "Occupation", "Department", "Department Name", "Sub Department", "Sub Department Name", "Grade", "Location", "Working Location", "Division", "Bank Account No", "Bank Branch IFC", "Bank Branch Description", "Emp Bank Name", "Sal Structure", "Attendance", "Res No", "Res Name", "Payment Mode", "Current Address", "Current Country Code", "Current Country Name", "Current State", "Current State Name", "Current City", "Current City Name", "Current Phone No", "Current Mobile No", "Current Postal Code", "Current Tehsil", "Current Village", "Current Post office", "Current Police Station", "Current Type", "Current Address Verified", "Current Verification Remarks", "Permanent Address", "Permanent Country", "Permanent Country Name", "Permanent State", "Permanent State Name", "Permanent City", "Permanent City Name", "Permanent Phone No", "Permanent Mobile No", "Permanent Postal", "Permanent Tehsil", "Permanent Village", "Permanent Post Office", "Permanent Police Station", "Permanent Type", "Permanent Address Verified", "Permanent Verification remarks", "E - Mail ID", "STD Code", "Date of Joining", "Salary calculate frm", "Date of leaving", "Reason for leaving", "ESI Applicable", "ESI No", "ESI Dispensary", "PF Applicable", "PF No", "PF No for Dept File", "Restrict PF", "Zero Pension", "Zero PT", "PAN", "Ward/Circle", "Director", "Resignation Submit Date", "Notice Period In Days", "Salary Account", "Advance To Staff", "Conveyance Type", "Employment Nature", "Is OT Applicable", "Is OD Applicable", "Show in Statutory", "Minimum Basic Salary", "Vendor Code", "Agency Code", "User Code", "Age For Pension", "Aadhar No", "PF Calculation Type", "EPF Rate", "Max Amount EPF", "Employee Band Code", "BioMetric Employee Code", "Employee Status(Active/Inactive)", "Card_No", "UIN_NO", "SecChequeNoLac1", "SecChequeNoRs100", "UANNo") Then
            Dim trans As SqlTransaction = Nothing
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsEmployeeMaster()
                    i = i + 1
                    Dim strName As String = ""
                    Dim strBBDesp As String = ""
                    Dim strBBName As String = ""
                    Dim strCode As String = ""
                    Dim strEmpBandCode As String = ""

                    strCode = clsCommon.myCstr(grow.Cells("Emp ID").Value)
                    If strCode.Length > 12 Then
                        Throw New Exception(" Length of Employee id can not be greater than 30")
                    End If
                    obj.EMP_CODE = strCode



                    strName = clsCommon.myCstr(grow.Cells("Employee Name").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of Employee Name can not be greater than 50 ")
                    ElseIf (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Employee Name can not be blank ")
                    End If
                    obj.Emp_Name = strName

                    strName = clsCommon.myCstr(grow.Cells("Fathers Name").Value)
                    If strName.Length > 100 Then
                        Throw New Exception("Length of Father Name can not be greater than 100")
                    End If
                    obj.FATHERS_NAME = strName

                    strName = clsCommon.myCstr(grow.Cells("Mothers Name").Value)
                    If strName.Length > 100 Then
                        Throw New Exception("Length of Mother Name can not be greater than 100")
                    End If
                    obj.MOTHERS_NAME = strName

                    Dim Date_date As Date = Nothing
                    'Date_date = clsCommon.myCDate(grow.Cells("Date of Birth").Value)
                    'If clsCommon.myLen(grow.Cells("Date of Birth")) > 0 Then
                    '    If Date_date.Year < 1900 Or (String.IsNullOrEmpty(Date_date)) Then
                    '        Throw New Exception("Date of Birth can not be blank or incorrect ")
                    '    End If
                    '    obj.Birth_date = clsCommon.GetPrintDate(Date_date, "dd/MM/yyyy")
                    'Else
                    '    Throw New Exception("Date of Birth can not be blank or incorrect")
                    'End If
                    strName = clsCommon.myCstr(grow.Cells("Date of Birth").Value)
                    If clsCommon.myLen(Date_date) <= 0 Then
                        Throw New Exception("Date of Birth can not be blank ")
                    ElseIf IsDate(clsCommon.myCstr(grow.Cells("Date of Birth").Value)) = False Then
                        Throw New Exception("Format of Date of Birth is incorrect")
                    End If
                    obj.Birth_date = clsCommon.GetPrintDate(strName, "dd/MM/yyyy")

                    strName = clsCommon.myCstr(grow.Cells("Sex").Value)
                    If strName.Length > 10 Then
                        Throw New Exception("Length of Sex can not be greater than 10")
                    ElseIf (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Sex can not be blank ")
                    End If
                    obj.SEX = strName

                    strName = clsCommon.myCstr(grow.Cells("Marital Status").Value)
                    If strName.Length > 15 Then
                        Throw New Exception("Length of Marital Status can not be greater than 15")
                    ElseIf (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Marital Status can not be blank ")
                    End If
                    obj.MARITAL_STATUS = strName


                    strName = clsCommon.myCstr(grow.Cells("Spouse Name").Value)
                    If strName.Length > 100 Then
                        Throw New Exception("Length of Spouse Name can not be greater than 100")
                    End If
                    obj.SPOUSE_NAME = strName

                    strCode = clsCommon.myCstr(grow.Cells("Designation").Value)
                    If strCode.Length > 50 Then
                        Throw New Exception("length of Designation can not be greater than 50")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_Designation_MASTER Where Designation_id ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Designation (" & strCode & ") does not exist in Designation Master . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.Designation = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Occupation").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("length of Occupation can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_OCCUPATION_MASTER Where OCCUPATION_CODE ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Occupation (" & strCode & ") does not exist in Occupation Master . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.OCCUPATION_CODE = strCode


                    strCode = clsCommon.myCstr(grow.Cells("Department").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("length of Department can not be greater than 30")
                    ElseIf strCode.Length <= 0 Then
                        Throw New Exception("Department can not be left blank")
                    ElseIf strCode.Length > 0 Then
                        strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_DEPARTMENT_MASTER Where DEPARTMENT_CODE ='" & strCode & "'", trans)
                        If strName <= 0 Then
                            Throw New Exception("Department (" & strCode & ") does not exist in Department Master . Please make it entry first.")
                        End If
                    End If
                    obj.DEPARTMENT_CODE = strCode


                    strCode = clsCommon.myCstr(grow.Cells("Sub Department").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("length of Sub Department can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then

                            strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_SUB_DEPARTMENT_MASTER Where TSPL_SUB_DEPARTMENT_MASTER.SUB_DEPARTMENT_CODE   ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Sub Department (" & strCode & ") does not exist in Sub Department Master . Please make it entry first.")
                            End If
                        End If
                    End If

                    obj.SUB_DEPARTMENT_CODE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Grade").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("length of Grade can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_GRADE_MASTER Where GRADE_CODE ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Grade (" & strCode & ") does not exist in Grade Master . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.GRADE_CODE = strCode


                    strCode = clsCommon.myCstr(grow.Cells("Location").Value)
                    If strCode.Length > 12 Then
                        Throw New Exception("length of Location can not be greater than 12")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*)from  TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code  where Location_Type='Physical' and Location_Code ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Location (" & strCode & ") does not exist in Location Master . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.LOCATION_CODE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Working Location").Value)
                    If strCode.Length > 50 Then
                        Throw New Exception("length of Working Location can not be greater than 50")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_City_MASTER  where city_Code ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Working Location (" & strCode & ") does not exist in City Master . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.Working_City_Code = strCode


                    strCode = clsCommon.myCstr(grow.Cells("Division").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("length of Division can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_DEVISION_MASTER  where DEVISION_CODE ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Division (" & strCode & ") does not exist in Division Master . Please make it entry first.")
                            End If
                        End If
                    End If

                    obj.DEVISION_CODE = strCode


                    strCode = clsCommon.myCstr(grow.Cells("Bank Account No").Value)
                    If strCode.Length > 0 Then
                        If (Not IsNumeric(clsCommon.myCstr(grow.Cells("Bank Account No").Value))) Then
                            Throw New Exception("Please enter Numeric only in Bank Account No.")
                        ElseIf strCode.Length > 50 Then
                            Throw New Exception("Length of Bank Account No can not be greater than 50")
                        End If
                    End If
                    obj.BANK_ACC_NO = strCode

                    'strCode = clsCommon.myCstr(grow.Cells("B Name").Value)

                    'If strCode.Length > 30 Then
                    '    Throw New Exception("length of B Name can not be greater than 30")
                    'Else
                    '    If strCode.Length > 0 Then
                    '        strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_BANK_MASTER  where BANK_CODE ='" & strCode & "'", trans)
                    '        If strName <= 0 Then
                    '            Throw New Exception("B Name (" & strCode & ") does not exist in Bank Master . Please make it entry first.")
                    '        End If
                    '    End If
                    'End If

                    'obj.BANK_CODE = strCode


                    strName = clsCommon.myCstr(grow.Cells("Bank Branch IFC").Value)
                    'If (Not IsNumeric(clsCommon.myCstr(grow.Cells("Bank Branch IFC").Value))) Then
                    '    Throw New Exception("Please enter Numeric only in Bank Branch IFC")
                    'End If
                    If strName.Length > 0 Then
                        obj.Bank_Branch = strName
                    Else
                        obj.Bank_Branch = ""
                    End If

                    strBBDesp = clsCommon.myCstr(grow.Cells("Bank Branch Description").Value)
                    If strBBDesp.Length > 0 Then
                        obj.Bank_Branch_Name = strBBDesp
                    ElseIf strBBDesp.Length > 100 Then
                        Throw New Exception("Bank branch description can not be more than 100 characters ")
                    Else
                        obj.Bank_Branch_Name = ""
                    End If
                    '' Bank Name
                    strBBName = clsCommon.myCstr(grow.Cells("Emp Bank Name").Value)
                    If strBBName.Length > 0 Then
                        obj.Bank_Name = strBBName
                    ElseIf strBBName.Length > 100 Then
                        Throw New Exception("Bank description can not be more than 100 characters ")
                    Else
                        obj.Bank_Name = ""
                    End If

                    strCode = clsCommon.myCstr(grow.Cells("Attendance").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("length of Attendance can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_ATTENDANCE_MASTER  where ATTENDANCE_CODE ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Attendance (" & strCode & ") does not exist in Attendance Master . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.ATTENDANCE_CODE = strCode


                    Dim add As String = ""
                    strName = clsCommon.myCstr(grow.Cells("Res No").Value)
                    If clsCommon.myCstr(strName).Length > 0 Then
                        add += strName
                    End If
                    strName = clsCommon.myCstr(grow.Cells("Res Name").Value)
                    If clsCommon.myCstr(strName).Length > 0 Then
                        add += strName
                    End If

                    strCode = clsCommon.myCstr(grow.Cells("Payment Mode").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("length of Payment Mode can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from Tspl_Payment_mode  where Code ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Payment Mode (" & strCode & ") does not exist in Payment Mode Master . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.PAYMENT_MODE = strCode



                    strName = clsCommon.myCstr(grow.Cells("Current Address").Value)
                    If strName.Length > 250 Then
                        Throw New Exception("Length of Current Address can not be greater than 250")
                    End If
                    obj.Add1 = strName


                    strCode = clsCommon.myCstr(grow.Cells("Current Country Code").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("length of Current Country can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_COUNTRY_MASTER  where COUNTRY_CODE ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Current Country (" & strCode & ") does not exist in Country Master  . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.PRESENT_COUNTRY_CODE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Current State").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("length of Current State can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_STATE_MASTER  where STATE_CODE ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Current State (" & strCode & ") does not exist in State Master  . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.PRESENT_STATE_CODE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Current City").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("length of Current City can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_City_MASTER  where City_Code ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Current City (" & strCode & ") does not exist in State Master  . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.PRESENT_CITY_CODE = strCode

                    strName = clsCommon.myCstr(grow.Cells("Current Phone No").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of Current Phone No. can not be greater than 50")
                    End If
                    obj.Phone = strName

                    strName = clsCommon.myCstr(grow.Cells("Current Mobile No").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of Current Mobile No can not be greater than 14 ")
                    End If
                    obj.PRESENT_MOBILE_NO = strName

                    strName = clsCommon.myCstr(grow.Cells("Current Postal Code").Value)
                    If strName.Length > 6 Then
                        Throw New Exception("Length of Current Postal Code can not be greater than 6")
                    End If
                    obj.Pin_Code = strName

                    strName = clsCommon.myCstr(grow.Cells("Current Tehsil").Value)
                    If strName.Length > 200 Then
                        Throw New Exception("Length of Current Tehsil can not be greater than 200")
                    End If
                    obj.ADD2_TEHSIL = strName

                    strName = clsCommon.myCstr(grow.Cells("Current Village").Value)
                    If strName.Length > 200 Then
                        Throw New Exception("Length of Current Village can not be greater than 200")
                    End If
                    obj.ADD2_VILLAGE = strName

                    strName = clsCommon.myCstr(grow.Cells("Current Post office").Value)
                    If strName.Length > 200 Then
                        Throw New Exception("Length of Current Post office can not be greater than 200")
                    End If
                    obj.ADD2_POST_OFFICE = strName

                    strName = clsCommon.myCstr(grow.Cells("Current Police Station").Value)
                    If strName.Length > 200 Then
                        Throw New Exception("Length of Current police Station can not be greater than 200")
                    End If
                    obj.ADD2_POLICE_STATION = strName


                    strName = clsCommon.myCstr(grow.Cells("Current Type").Value)
                    If strName.Length > 30 Then
                        Throw New Exception("Length of Current Type can not be greater than 30")
                    End If
                    If strName.Length <= 0 Then
                        obj.ADD2_TYPE = ""
                    ElseIf clsCommon.CompairString(strName, "Owned") <> CompairStringResult.Equal And clsCommon.CompairString(strName, "Rented") <> CompairStringResult.Equal Then
                        Throw New Exception("Current Type should be amoung 'Owned','Rented' ")
                    End If
                    obj.ADD2_TYPE = strName


                    strName = clsCommon.myCstr(grow.Cells("Current Address Verified").Value)
                    If strName.Length <= 0 Then
                        obj.ADD2_VERIFIED = ""
                    ElseIf clsCommon.CompairString(strName, "Y") <> CompairStringResult.Equal And clsCommon.CompairString(strName, "N") <> CompairStringResult.Equal Then
                        Throw New Exception("Please enter only Y or N in Current Address Verified")
                    End If
                    obj.ADD2_VERIFIED = IIf(clsCommon.CompairString(strName, "Y") = CompairStringResult.Equal, 1, 0)


                    strName = clsCommon.myCstr(grow.Cells("Current Verification Remarks").Value)
                    If strName.Length > 200 Then
                        Throw New Exception("Length of Current Verification remarks can not be greater than 200")
                    End If
                    obj.ADD2_VERIFIED_REMARKS = strName

                    strName = clsCommon.myCstr(grow.Cells("Permanent Address").Value)
                    If strName.Length > 250 Then
                        Throw New Exception("Length of Permanent Address can not be greater than 250")
                    End If
                    obj.Add2 = strName

                    strCode = clsCommon.myCstr(grow.Cells("Permanent Country").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("length of Permanent Country can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_COUNTRY_MASTER  where COUNTRY_CODE ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Permanent Country (" & strCode & ") does not exist in Country Master  . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.PERMA_COUNTRY_CODE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Permanent State").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("length of Permanent State can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_STATE_MASTER  where STATE_CODE ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Permanent State (" & strCode & ") does not exist in State Master  . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.PERMA_STATE_CODE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Permanent City").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("length of Permanent City can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_City_MASTER  where City_Code ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Permanent City (" & strCode & ") does not exist in State Master  . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.PERMA_CITY_CODE = strCode

                    strName = clsCommon.myCstr(grow.Cells("Permanent Phone No").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of Permanent Phone No. can not be greater than 50")
                    End If
                    obj.PERMA_PHONE_NO = strName

                    strName = clsCommon.myCstr(grow.Cells("Permanent Mobile No").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of Permanent Mobile No can not be greater than 14 ")
                    End If
                    obj.PERMA_MOBILE_NO = strName

                    strName = clsCommon.myCstr(grow.Cells("Permanent Postal").Value)
                    If strName.Length > 6 Then
                        Throw New Exception("Length of Permanent Postal Code can not be greater than 6")
                    End If
                    obj.PERMA_PIN_CODE = strName

                    strName = clsCommon.myCstr(grow.Cells("Permanent Tehsil").Value)
                    If strName.Length > 200 Then
                        Throw New Exception("Length of Permanent Tehsil can not be greater than 200")
                    End If
                    obj.ADD1_TEHSIL = strName

                    strName = clsCommon.myCstr(grow.Cells("Permanent Village").Value)
                    If strName.Length > 200 Then
                        Throw New Exception("Length of Permanent Village can not be greater than 200")
                    End If
                    obj.ADD1_VILLAGE = strName

                    strName = clsCommon.myCstr(grow.Cells("Permanent Post Office").Value)
                    If strName.Length > 200 Then
                        Throw New Exception("Length of Permanent Post office can not be greater than 200")
                    End If
                    obj.ADD1_POST_OFFICE = strName

                    strName = clsCommon.myCstr(grow.Cells("Permanent Police Station").Value)
                    If strName.Length > 200 Then
                        Throw New Exception("Length of Permanent police Station can not be greater than 200")
                    End If
                    obj.ADD1_POLICE_STATION = strName

                    strName = clsCommon.myCstr(grow.Cells("Permanent Type").Value)

                    If strName.Length > 30 Then
                        Throw New Exception("Length of Permanent Type can not be greater than 30")
                    End If
                    If strName.Length <= 0 Then
                        obj.ADD1_TYPE = ""
                    ElseIf clsCommon.CompairString(strName, "Owned") <> CompairStringResult.Equal And clsCommon.CompairString(strName, "Rented") <> CompairStringResult.Equal Then
                        Throw New Exception("Current Type should be amoung 'Owned','Rented' ")
                    End If
                    obj.ADD1_TYPE = strName

                    strName = clsCommon.myCstr(grow.Cells("Permanent Address Verified").Value)
                    If strName.Length <= 0 Then
                        obj.ADD1_VERIFIED_REMARKS = ""
                    ElseIf clsCommon.CompairString(strName, "Y") <> CompairStringResult.Equal And clsCommon.CompairString(strName, "N") <> CompairStringResult.Equal Then
                        Throw New Exception("Please enter only Y or N in Permanent Address Verified")
                    End If
                    obj.ADD1_VERIFIED = IIf(clsCommon.CompairString(strName, "Y") = CompairStringResult.Equal, 1, 0)


                    strName = clsCommon.myCstr(grow.Cells("Permanent Verification remarks").Value)
                    If strName.Length > 200 Then
                        Throw New Exception("Length of Permanent Verification remarks can not be greater than 200")
                    End If

                    obj.ADD1_VERIFIED_REMARKS = strName


                    strName = clsCommon.myCstr(grow.Cells("E - Mail ID").Value)
                    If strName.Length > 100 Then
                        Throw New Exception("Length of E-Mail ID can not be greater than 100 ")
                    End If
                    obj.EMail_ID = strName

                    strName = clsCommon.myCstr(grow.Cells("Date of Joining").Value)
                    If clsCommon.myLen(strName) <= 0 Then
                        Throw New Exception("Date of Joining can not be blank ")
                    ElseIf IsDate(clsCommon.myCstr(grow.Cells("Date of Joining").Value)) = False Then
                        Throw New Exception("Format of Date of Joining incorrect ")
                    End If
                    Date_date = clsCommon.myCstr(grow.Cells("Date of Joining").Value)
                    'If Date_date.Year < 1900 Or (String.IsNullOrEmpty(Date_date)) Then
                    '    Throw New Exception("Date of Joining can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                    'End If
                    obj.Joining_date = clsCommon.GetPrintDate(Date_date, "dd/MM/yyyy")


                    If IsDBNull(grow.Cells("Salary calculate frm").Value) Then
                        Date_date = clsCommon.myCDate(grow.Cells("Date of Joining").Value)
                    Else
                        If clsCommon.myLen(grow.Cells("Salary calculate frm").Value) > 0 Then
                            Date_date = clsCommon.myCDate(grow.Cells("Salary calculate frm").Value)
                        End If

                    End If
                    'If Date_date.Year < 1900 Or (String.IsNullOrEmpty(Date_date)) Then
                    '    Throw New Exception("Salary calculate frm can not be blank or incorrect ")
                    'End If
                    obj.CONFIRMATION_DATE = clsCommon.GetPrintDate(Date_date, "dd/MM/yyyy")

                    'If clsCommon.myCstr(grow.Cells("Date of leaving").Value).Length > 0 Then
                    '    Date_date = clsCommon.myCDate(grow.Cells("Date of leaving").Value)
                    '    'If Date_date.Year < 1900 Then
                    '    '    Throw New Exception("Date of leaving can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                    '    'End If
                    '    obj.rel_date = clsCommon.GetPrintDate(Date_date, "dd/MM/yyyy")
                    'End If


                    strName = clsCommon.myCstr(grow.Cells("Date of leaving").Value)
                    If clsCommon.myLen(strName) > 0 Then
                        If IsDate(clsCommon.myCstr(grow.Cells("Date of leaving").Value)) = False Then
                            Throw New Exception("Format of Date of leaving is incorrect")
                        End If
                        obj.RELIEVING_DATE = clsCommon.GetPrintDate(strName, "dd/MMM/yyyy")
                    Else
                        obj.RELIEVING_DATE = Nothing
                    End If


                    strName = clsCommon.myCstr(grow.Cells("Reason for leaving").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of Reason for leaving can not be greater then 50")
                    End If
                    obj.LEAVING_REASON = strName

                    strName = clsCommon.myCstr(grow.Cells("ESI Applicable").Value)
                    If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                        obj.ISESI = True
                    Else
                        obj.ISESI = False
                    End If

                    strName = clsCommon.myCstr(grow.Cells("ESI No").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of ESI No can not be greater then 50 ")
                    End If
                    obj.ESI_NO = strName

                    strName = clsCommon.myCstr(grow.Cells("ESI Dispensary").Value)
                    If strName.Length > 100 Then
                        Throw New Exception("Length of ESI Dispensary can not be greater then 100")
                    End If
                    obj.ESI_DISPENSARY = strName

                    strName = clsCommon.myCstr(grow.Cells("PF Applicable").Value)
                    If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                        obj.ISPF = True
                    Else
                        obj.ISPF = False
                    End If

                    strName = clsCommon.myCstr(grow.Cells("PF No").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of PF No can not greater then 50")
                    End If
                    obj.PF_NO = strName

                    strName = clsCommon.myCstr(grow.Cells("PF No for Dept File").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of PF No for Dept File can not greater then 50")
                    End If
                    obj.PF_NO_DEPT_FILE = strName

                    strName = clsCommon.myCstr(grow.Cells("Restrict PF").Value)
                    If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                        obj.ISRESTRICT_PF = True
                    Else
                        obj.ISRESTRICT_PF = False
                    End If

                    strName = clsCommon.myCstr(grow.Cells("Zero Pension").Value)
                    If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                        obj.ISZERO_PENSION = True
                    Else
                        obj.ISZERO_PENSION = False
                    End If

                    strName = clsCommon.myCstr(grow.Cells("Zero PT").Value)
                    If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                        obj.ISZERO_PT = True
                    Else
                        obj.ISZERO_PT = False
                    End If

                    strName = clsCommon.myCstr(grow.Cells("PAN").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("PAN No can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                    End If
                    obj.PAN_NO = strName

                    strName = clsCommon.myCstr(grow.Cells("Ward/Circle").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of Ward/Circle can not greater then 50")
                    End If
                    obj.WARD_CIRCLE = strName

                    strName = clsCommon.myCstr(grow.Cells("Director").Value)
                    If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                        obj.ISDIRECTOR = True
                    Else
                        obj.ISDIRECTOR = False
                    End If

                    '' panch raj 14-oct-2014 
                    Dim Salary_Acc As String = ""
                    Dim Adv_To As String = ""

                    Dim Salary_Acc_Code As String
                    Dim Account_To_Staff As String

                    Salary_Acc = clsCommon.myCstr(grow.Cells("Salary Account").Value)
                    If Salary_Acc.Length > 30 Then
                        'obj.SALARY_ACCOUNT_CODE = strName
                        Throw New Exception("Salary Account Code length can not be greater than 30")
                    Else
                        If Salary_Acc.Length > 0 Then
                            Salary_Acc_Code = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_PAYROLL_ACCOUNTSETS Where ACCOUNT_SET_CODE ='" & Salary_Acc & "'", trans)
                            If Salary_Acc_Code <= 0 Then
                                Throw New Exception("Salary Account Code(" & Salary_Acc & ") does not exist . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.SALARY_ACCOUNT_CODE = Salary_Acc

                    Adv_To = clsCommon.myCstr(grow.Cells("Advance To Staff").Value)
                    If Adv_To.Length > 50 Then
                        Throw New Exception("Advance To Staff length can not be greater than 50")
                    Else
                        If Adv_To.Length > 0 Then
                            Account_To_Staff = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_GL_ACCOUNTS Where Account_Code ='" & Adv_To & "'", trans)
                            If Account_To_Staff <= 0 Then
                                Throw New Exception("Advance To Staff(" & Adv_To & ") does not exist . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.ADVANCE_TO_STAFF = Adv_To
                    '' for kdil and viney

                    strName = clsCommon.myCstr(grow.Cells("Conveyance Type").Value)
                    If clsCommon.CompairString(strName, "TW") = CompairStringResult.Equal Then
                        obj.CONV_TYPE = strName
                    ElseIf clsCommon.CompairString(strName, "FW") = CompairStringResult.Equal Then
                        obj.CONV_TYPE = strName
                    ElseIf clsCommon.CompairString(strName, "None") = CompairStringResult.Equal Then
                        obj.CONV_TYPE = strName
                    Else
                        obj.CONV_TYPE = "None"
                    End If

                    strName = clsCommon.myCstr(grow.Cells("Employment Nature").Value)
                    If clsCommon.CompairString(strName, "Permanent") = CompairStringResult.Equal Then
                        obj.CONV_TYPE = strName
                    ElseIf clsCommon.CompairString(strName, "Contractual") = CompairStringResult.Equal Then
                        obj.CONV_TYPE = strName
                    ElseIf clsCommon.CompairString(strName, "Other") = CompairStringResult.Equal Then
                        obj.CONV_TYPE = strName
                    Else
                        obj.CONV_TYPE = "Other"
                    End If

                    strName = clsCommon.myCstr(grow.Cells("Is OT Applicable").Value)
                    If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                        obj.IS_OT_APPL = True
                    Else
                        obj.IS_OT_APPL = False
                    End If

                    strName = clsCommon.myCstr(grow.Cells("Aadhar No").Value)
                    If strName.Length > 12 Then
                        Throw New Exception("Length of Aadhar No can not be greater than 12")
                    End If
                    obj.Adhar_No = strName

                    '=================
                    Dim Limit As Double = clsCommon.myCdbl(grow.Cells("Max Amount EPF").Value)
                    Dim EPF As Double = clsCommon.myCdbl(grow.Cells("EPF Rate").Value)
                    strName = clsCommon.myCstr(grow.Cells("PF Calculation Type").Value)
                    If clsCommon.CompairString(strName, "PR") = CompairStringResult.Equal Then
                        obj.Pf_Calculation_Type = strName
                        obj.EPF_Rate = 0
                        obj.Max_Amount_EPF = 0
                    ElseIf clsCommon.CompairString(strName, "FA") = CompairStringResult.Equal Then
                        obj.Pf_Calculation_Type = strName
                        obj.EPF_Rate = 0
                        obj.Max_Amount_EPF = 0
                    ElseIf clsCommon.CompairString(strName, "None") = CompairStringResult.Equal Then
                        obj.Pf_Calculation_Type = strName
                        obj.EPF_Rate = 0
                        obj.Max_Amount_EPF = 0
                    ElseIf clsCommon.CompairString(strName, "C") = CompairStringResult.Equal Then
                        obj.Pf_Calculation_Type = strName
                        obj.Max_Amount_EPF = Limit
                        obj.EPF_Rate = EPF
                    Else
                        obj.Pf_Calculation_Type = "None"
                        obj.EPF_Rate = 0
                        obj.Max_Amount_EPF = 0
                    End If

                    '==========










                    strName = clsCommon.myCstr(grow.Cells("Is OD Applicable").Value)
                    If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                        obj.IS_OD_APPL = True
                    Else
                        obj.IS_OD_APPL = False
                    End If

                    strName = clsCommon.myCstr(grow.Cells("Show in Statutory").Value)
                    If strName.Length > 0 AndAlso clsCommon.CompairString(strName, "Yes") = CompairStringResult.Equal Then
                        obj.DISPLAY_IN_STATUTORY = True
                    Else
                        obj.DISPLAY_IN_STATUTORY = False
                    End If

                    strName = clsCommon.myCdbl(grow.Cells("Minimum Basic Salary").Value)
                    obj.MINIMUM_BASIC_SALARY = strName

                    strCode = clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("Vendor Code length can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM tspl_vendor_master Where vendor_code ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Vendor Code (" & strCode & ") does not exist . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.VENDOR_CODE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Agency Code").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("Agency Code length can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM Tspl_HR_Agency_Master Where code ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Agency Code (" & strCode & ") does not exist . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.AGENCY_CODE = strCode

                    '' user code
                    strCode = clsCommon.myCstr(grow.Cells("User Code").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("User Code length can not be greater than 30")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_USER_MASTER Where USER_CODE ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("User Code (" & strCode & ") does not exist . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.USER_CODE = strCode

                    Dim AgeFPen As Double = clsCommon.myCdbl(grow.Cells("Age For Pension").Value)
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Age For Pension").Value)) > 0 Then
                        If clsCommon.myLen(AgeFPen) > 0 Then

                            If clsCommon.myCdbl(AgeFPen) < 0 Then
                                Throw New Exception("Age for pension should be numeric")
                            End If
                            If clsCommon.myLen(AgeFPen) <> 2 AndAlso clsCommon.myCdbl(AgeFPen) > 0 Then
                                Throw New Exception("Age for pension should be in 2 digits")
                            End If
                        End If
                    End If

                    obj.AgeForPension = AgeFPen

                    strEmpBandCode = clsCommon.myCstr(grow.Cells("Employee Band Code").Value)
                    If clsCommon.CompairString(clsCommon.myCstr(strEmpBandCode), "") <> CompairStringResult.Equal Then
                        Dim dt As DataTable
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_EMPLOYEE_BAND_MASTER where Code='" + clsCommon.myCstr(strEmpBandCode) + "'")
                        If dt.Rows.Count <= 0 Then
                            Throw New Exception("Employee Band Code not exists in employee band master.")
                        End If
                    End If
                    obj.EMP_Band_Code = strEmpBandCode

                    Dim BiometricEmpCode = clsCommon.myCstr(grow.Cells("BioMetric Employee Code").Value)
                    obj.BioMetricEmpID = BiometricEmpCode
                    '==========================
                    ' Ticket No : TEC/13/02/19-000422 By Prabhakar
                    strName = clsCommon.myCstr(grow.Cells("Employee Status(Active/Inactive)").Value)
                    If clsCommon.CompairString(strName, "Active") <> CompairStringResult.Equal And clsCommon.CompairString(strName, "Inactive") <> CompairStringResult.Equal Then
                        Throw New Exception("[Employee Status] should be Active or Inactive.")
                    End If
                    obj.Emp_Status = strName '"Active"

                    'sanjay Ticket No  BHA/14/03/19-000846 
                    obj.Card_No = clsCommon.myCstr(grow.Cells("Card_No").Value)
                    obj.UIN_NO = clsCommon.myCstr(grow.Cells("UIN_NO").Value)
                    'sanjay
                    obj.SecChequeNoLac1 = clsCommon.myCstr(grow.Cells("SecChequeNoLac1").Value)
                    obj.SecChequeNoRs100 = clsCommon.myCstr(grow.Cells("SecChequeNoRs100").Value)

                    If clsCommon.myLen(grow.Cells("UANNo").Value) > 0 AndAlso clsCommon.myLen(grow.Cells("UANNo").Value) < 12 Then
                        Throw New Exception("Length of UAN No. Should be 12")
                    End If

                    obj.UANNo = clsCommon.myCstr(grow.Cells("UANNo").Value)
                    '' end kdil and viney
                    obj.SaveDataFromExcelSheet(obj, True)
                    grow = Nothing
                    obj = Nothing


                Next

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message & " At Line No : " & i)
            End Try
        End If


    End Sub

    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExport.Click
        Dim str As String = "select EMP_CODE as 'Emp ID',[Emp_Name] as 'Employee Name',[FATHERS_NAME] as 'Fathers Name',[MOTHERS_NAME] as 'Mothers Name', " &
"convert(varchar(12),Birth_date,103) as 'Date of Birth',[SEX] as 'Sex',[MARITAL_STATUS] as 'Marital Status',[SPOUSE_NAME] as 'Spouse Name', " &
"[Designation] as 'Designation',TSPL_DESIGNATION_MASTER.Designation_Desc as [Designation Name],[OCCUPATION_CODE] as 'Occupation',TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE as 'Department',TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME as [Department Name],TSPL_EMPLOYEE_MASTER.SUB_DEPARTMENT_CODE as 'Sub Department',TSPL_SUB_DEPARTMENT_MASTER.Description as [Sub Department Name],[GRADE_CODE] as 'Grade', " &
"[LOCATION_CODE] as 'Location',[WORKING_City_CODE] as 'Working Location',[DEVISION_CODE] as 'Division',[BANK_ACC_NO] as 'Bank Account No', [Bank_Branch] as 'Bank Branch IFC',Bank_Branch_Name As [Bank Branch Description],Bank_Name AS [Emp Bank Name], '' as 'Sal Structure', " &
"[ATTENDANCE_CODE] as 'Attendance', " &
       " '' as 'Res No','' as 'Res Name',PAYMENT_MODE_New as 'Payment Mode', [Add1] as 'Current Address',PRESENT_COUNTRY_CODE as 'Current Country Code',Present_Country.COUNTRY_NAME as 'Current Country Name',PRESENT_STATE_CODE as 'Current State',Present_State.STATE_NAME as 'Current State Name',PRESENT_CITY_CODE as 'Current City',Present_City.City_Name as 'Current City Name',[Phone] as 'Current Phone No',[PRESENT_MOBILE_NO] as 'Current Mobile No',[Pin_Code] as 'Current Postal Code',ADD2_TEHSIL as 'Current Tehsil',ADD2_VILLAGE as 'Current Village',ADD2_POST_OFFICE as 'Current Post office',ADD2_POLICE_STATION as 'Current Police Station',ADD2_TYPE as 'Current Type',case when isnull(ADD2_VERIFIED,0) = 0 then 'N' else 'Y' end as 'Current Address Verified',ADD2_VERIFIED_REMARKS as 'Current Verification Remarks',[Add2] as 'Permanent Address',PERMA_COUNTRY_CODE as 'Permanent Country',Permanent_Country.COUNTRY_NAME as 'Permanent Country Name',PERMA_STATE_CODE as 'Permanent State',Permanent_State.STATE_NAME as 'Permanent State Name',PERMA_CITY_CODE as 'Permanent City',Permanent_City.City_Name as 'Permanent City Name',PERMA_PHONE_NO as 'Permanent Phone No',PERMA_MOBILE_NO as 'Permanent Mobile No',PERMA_PIN_CODE as 'Permanent Postal',ADD1_TEHSIL as 'Permanent Tehsil',ADD1_VILLAGE  as 'Permanent Village',ADD1_POST_OFFICE as 'Permanent Post Office',ADD1_POLICE_STATION as 'Permanent Police Station',ADD1_TYPE as 'Permanent Type',case when isnull(ADD1_VERIFIED,0)=0 then 'N' else 'Y' end as 'Permanent Address Verified',ADD1_VERIFIED_REMARKS as 'Permanent Verification remarks'," &
"[EMail_ID] as 'E - Mail ID','' as 'STD Code' ,    " &
"convert(varchar(12),Joining_date,103) as 'Date of Joining',convert(varchar(12),CONFIRMATION_DATE,103) as 'Salary calculate frm', " &
"convert(varchar(12),RELIEVING_DATE,103) as 'Date of leaving',[LEAVING_REASON] as 'Reason for leaving', (case when ISESI=1 then 'Yes' else 'No' end) as 'ESI Applicable',[ESI_NO] as 'ESI No', " &
"[ESI_DISPENSARY] as 'ESI Dispensary',(case when ISPF=1 then 'Yes' else 'No' end) as 'PF Applicable',[PF_NO] as 'PF No',[PF_NO_DEPT_FILE] as 'PF No for Dept File',   " &
"(case when ISRESTRICT_PF=1 then 'Yes' else 'No' end) as 'Restrict PF', (case when ISZERO_PENSION=1 then 'Yes' else 'No' end) as 'Zero Pension', (case when ISZERO_PT=1 then 'Yes' else 'No' end) as 'Zero PT',[PAN_NO] as 'PAN', " &
"[WARD_CIRCLE] as 'Ward/Circle',[ISDIRECTOR] as 'Director' ,[RESIGNATION_SUBMIT_DATE] as 'Resignation Submit Date',  " &
"[NOTICE_PERIOD_IN_DAYS] as 'Notice Period In Days',[SALARY_ACCOUNT_CODE] As 'Salary Account',[ADVANCE_TO_STAFF] As 'Advance To Staff',  " &
" CONV_TYPE as [Conveyance Type],EMPLOYMENT_NATURE as [Employment Nature],(case when IS_OT_APPL=1 then 'Yes' else 'No' end) as [Is OT Applicable],(case when IS_OD_APPL=1 then 'Yes' else 'No' end) as [Is OD Applicable],  " &
 "(case when DISPLAY_IN_STATUTORY=1 then 'Yes' else 'No'  end) as [Show in Statutory],MINIMUM_BASIC_SALARY as [Minimum Basic Salary],VENDOR_CODE as [Vendor Code],AGENCY_CODE as [Agency Code],User_Code as [User Code],Age_For_Pension AS [Age For Pension], Adhar_No as [Aadhar No],PF_Calculation_Type as [PF Calculation Type],EPF_Rate as [EPF Rate] ,Max_Amount_EPF as [Max Amount EPF],Employee_BandCode as [Employee Band Code],BioMetricEmpID as [BioMetric Employee Code],TSPL_EMPLOYEE_MASTER.Emp_Status  as [Employee Status(Active/Inactive)],TSPL_EMPLOYEE_MASTER.Card_No,TSPL_EMPLOYEE_MASTER.UIN_NO,TSPL_EMPLOYEE_MASTER.SecChequeNoLac1,TSPL_EMPLOYEE_MASTER.SecChequeNoRs100,TSPL_EMPLOYEE_MASTER.UANNo From TSPL_EMPLOYEE_MASTER  " &
" left join TSPL_DESIGNATION_MASTER on TSPL_EMPLOYEE_MASTER.Designation=TSPL_DESIGNATION_MASTER.Designation_id  " &
" left join TSPL_DEPARTMENT_MASTER on TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE=TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE left join TSPL_SUB_DEPARTMENT_MASTER on TSPL_EMPLOYEE_MASTER.SUB_DEPARTMENT_CODE=TSPL_SUB_DEPARTMENT_MASTER.SUB_DEPARTMENT_CODE " &
" left join TSPL_COUNTRY_MASTER as Permanent_Country on Permanent_Country.COUNTRY_CODE =TSPL_EMPLOYEE_MASTER .PERMA_COUNTRY_CODE" &
 " left join TSPL_COUNTRY_MASTER as Present_Country on Present_Country.COUNTRY_CODE =TSPL_EMPLOYEE_MASTER.PRESENT_COUNTRY_CODE" &
" left join TSPL_STATE_MASTER as Permanent_State on Permanent_State.STATE_CODE =TSPL_EMPLOYEE_MASTER.PERMA_STATE_CODE " &
" left join TSPL_STATE_MASTER as Present_State on Present_State.STATE_CODE =TSPL_EMPLOYEE_MASTER.PRESENT_STATE_CODE " &
" left join TSPL_CITY_MASTER as Permanent_City on Permanent_City.City_Code  = TSPL_EMPLOYEE_MASTER.PERMA_CITY_CODE " &
 " left join TSPL_CITY_MASTER as Present_City on Present_City.City_Code =TSPL_EMPLOYEE_MASTER.PRESENT_CITY_CODE "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmEmployee_Master
        frm.ShowDialog()
    End Sub

    Private Sub txtFranchiseCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFranchiseCode._MYValidating

        'Dim qry As String = "select Vendor_CODE as Code, Vendor_NAME as Name, add1 + ',' + add2 + ',' + add3 as Address  from TSPL_Vendor_MASTER "
        'txtFranchiseCode.Value = clsCommon.ShowSelectForm("FRNCHICE", qry, "Code", "franchise_yn='Y'", txtFranchiseCode.Value, "Vendor_CODE", isButtonClicked)
        txtFranchiseCode.Value = clsVendorMaster.getFinder("franchise_yn='Y'", txtFranchiseCode.Value, isButtonClicked)
        lblFranchiseName.Text = "Franchise Name : " & clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" & txtFranchiseCode.Value & "'")
    End Sub

    Private Sub txtSalaryAccount__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtSalaryAccount._MYValidating
        Dim qry As String = " Select ACCOUNT_SET_CODE AS Code ,DESCRIPTION  From TSPL_PAYROLL_ACCOUNTSETS "
        txtSalaryAccount.Value = clsCommon.ShowSelectForm("fmSalAcc", qry, "Code", "", txtSalaryAccount.Value, "Code", isButtonClicked)
    End Sub

    Private Sub txtAdvToStaff__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtAdvToStaff._MYValidating
        OpenGLAccount(isButtonClicked)
    End Sub
    Private Sub OpenGLAccount(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String
            Dim whrcls As String
            Dim arr As New ArrayList()
            '  Dim isEarningCond As String

            arr = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arr.Item(0)
            whrcls = arr.Item(1)

            txtAdvToStaff.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("GLACJournalEntry", qry, "Account_Code", whrcls, clsCommon.myCstr(txtAdvToStaff.Value), "Account_Code", isButtonClick))
            'txtSalaryPayableAccountDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(fndSalaryPayableAccount.Value) + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndVendor__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndVendor._MYValidating
        Me.fndVendor.Value = clsVendorMaster.getFinder("", Me.fndVendor.Value, isButtonClicked)
    End Sub

    Private Sub fndAgent__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndAgent._MYValidating

        Dim qry As String = "select Code,Name from Tspl_HR_Agency_Master  "
        Me.fndAgent.Value = clsCommon.ShowSelectForm("Agency_M", qry, "Code", "", fndAgent.Value, "", isButtonClicked)
    End Sub

    Private Sub txtUser__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndUser._MYValidating
        Me.fndUser.Value = clsUserMaster.getFinder("", Me.fndUser.Value, isButtonClicked)
    End Sub


#Region "Employee Assets"
    Private Sub gvAssets_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvAssets.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                If e.Column Is gvAssets.Columns(colASSET_CODE) Then
                    isCellValueChanged = True
                    gvAssets.CurrentRow.Cells(colASSET_CODE).Value = clsAcquisitionDetail.GetFinder("", gvAssets.CurrentRow.Cells(colASSET_CODE).Value, False)
                    If clsCommon.myLen(gvAssets.CurrentRow.Cells(colASSET_CODE).Value) > 0 Then
                        gvAssets.CurrentRow.Cells(colASSET_NAME).Value = clsAcquisitionDetail.GetName(gvAssets.CurrentRow.Cells(colASSET_CODE).Value)
                    End If
                    isCellValueChanged = False
                End If
            End If
        End If

    End Sub
    Sub LoadEmpAssetsGridColumns()
        'gvAssets.DataSource = Nothing
        gvAssets.Rows.Clear()
        gvAssets.Columns.Clear()
        gvAssets.ReadOnly = False

        Dim DoclineNo As New GridViewTextBoxColumn()
        DoclineNo.FormatString = ""
        DoclineNo.HeaderText = "Line No"
        DoclineNo.Name = colLINE_NO
        DoclineNo.Width = 30
        DoclineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvAssets.Columns.Add(DoclineNo)

        Dim DocCode As New GridViewTextBoxColumn()
        DocCode.FormatString = ""
        DocCode.HeaderText = "Asset Code"
        DocCode.Name = colASSET_CODE
        DocCode.Width = 100
        DocCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvAssets.Columns.Add(DocCode)

        Dim DocFileName As New GridViewTextBoxColumn()
        DocFileName.FormatString = ""
        DocFileName.HeaderText = "Asset Name"
        DocFileName.Name = colASSET_NAME
        DocFileName.Width = 150
        DocFileName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DocFileName.ReadOnly = True
        gvAssets.Columns.Add(DocFileName)

        Dim DocSubmitDate As New GridViewDateTimeColumn()
        DocSubmitDate.CustomFormat = "dd/MM/yyyy"
        DocSubmitDate.FormatString = "{0:d}"
        DocSubmitDate.HeaderText = "Allocation Date"
        DocSubmitDate.Name = colALLOCATE_DATE
        DocSubmitDate.Width = 80
        DocSubmitDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvAssets.Columns.Add(DocSubmitDate)

        Dim DocDescription As New GridViewTextBoxColumn()
        DocDescription.FormatString = ""
        DocDescription.HeaderText = "Remark"
        DocDescription.Name = colAssetDESCRIPTION
        DocDescription.Width = 100
        DocDescription.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvAssets.Columns.Add(DocDescription)

        Dim AssetReturned As New GridViewComboBoxColumn
        Dim arr As New ArrayList
        arr.Add("Y")
        arr.Add("N")
        AssetReturned.DataSource = arr

        AssetReturned.FormatString = ""
        AssetReturned.HeaderText = "Returned"
        AssetReturned.Name = colRETURNED
        AssetReturned.Width = 80
        AssetReturned.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        AssetReturned.IsVisible = True
        gvAssets.Columns.Add(AssetReturned)


        'gvAssets.Rows.AddNew()
    End Sub

    Public Sub LoadAssetGridData()
        LoadEmpAssetsGridColumns()
        If clsCommon.myLen(txtCode.Value) > 0 Then
            ObjListEmpAssetDetails = clsEmpAssets.GetDataForGrid(txtCode.Value, Nothing)
            If ObjListEmpAssetDetails IsNot Nothing AndAlso ObjListEmpAssetDetails.Count > 0 Then

                For Each objDocTr As clsEmpAssets In ObjListEmpAssetDetails
                    gvAssets.Rows.AddNew()
                    gvAssets.Rows(gvAssets.Rows.Count - 1).Cells(colLINE_NO).Value = objDocTr.LINE_NO
                    gvAssets.Rows(gvAssets.Rows.Count - 1).Cells(colASSET_CODE).Value = objDocTr.ASSET_CODE
                    gvAssets.Rows(gvAssets.Rows.Count - 1).Cells(colASSET_NAME).Value = objDocTr.ASSET_NAME
                    gvAssets.Rows(gvAssets.Rows.Count - 1).Cells(colALLOCATE_DATE).Value = objDocTr.ALLOCATE_DATE
                    gvAssets.Rows(gvAssets.Rows.Count - 1).Cells(colAssetDESCRIPTION).Value = objDocTr.DESCRIPTION
                    gvAssets.Rows(gvAssets.Rows.Count - 1).Cells(colRETURNED).Value = objDocTr.RETURNED
                Next
            End If
        End If
        'gvEmpDoc_SelectionChanged(Nothing, Nothing)
    End Sub



    Private Sub gvAssets_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvAssets.CurrentColumnChanged
        If gvEmpDoc.RowCount > 0 Then
            Dim intCurrRow As Integer = gvAssets.CurrentRow.Index
            gvAssets.CurrentRow.Cells(colLINE_NO).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gvAssets.Rows.Count - 1 Then
                gvAssets.Rows.AddNew()
                gvAssets.CurrentRow = gvAssets.Rows(intCurrRow)

            End If
        End If
    End Sub


#End Region
    '==========Shivani Tyagi
    Private Sub txtSubDepartment__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSubDepartment._MYValidating
        Try
            If clsCommon.myLen(txtDepartment.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Department First..")
                txtDepartment.Focus()
                Exit Sub
            End If
            Dim whrcls As String = "DEPARTMENT_CODE='" & clsCommon.myCstr(txtDepartment.Value) & "'"

            txtSubDepartment.Value = clsSubDepartmentMaster.getFinder(whrcls, txtSubDepartment.Value, isButtonClicked)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndLocation2__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtWorkingLocation._MYValidating, txtWorkingLocation._MYValidating

        txtWorkingLocation.Value = clsLocation.getFinder("Location_Type='Physical'", Me.txtBranch.Value, isButtonClicked)


    End Sub

    Private Sub CboEmployeeType_SelectedIndexChanged_1(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles CboEmployeeType.SelectedIndexChanged
        If CboEmployeeType.Text.Trim = "Service Dealer" Then
            grpFranchise.Visible = True
        Else
            grpFranchise.Visible = False
        End If
    End Sub

    Private Sub fndPaymentMode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPaymentMode._MYValidating
        Dim qry As String = "select CODE as Code, NAME as Name  from TSPL_Payment_MODE"
        fndPaymentMode.Value = clsCommon.ShowSelectForm("PaymentMode", qry, "Code", "", fndPaymentMode.Value, "CODE", isButtonClicked)
    End Sub
    Sub LoadPFCalculationType()
        Try
            isInsideLoad = True
            Dim dt As DataTable = New DataTable
            dt.Columns.Add("Code")
            dt.Columns.Add("Name")

            Dim dr As DataRow = dt.NewRow
            dr("Code") = "PR"
            dr("Name") = "PF Rule"
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Code") = "FA"
            dr("Name") = "Formula Amount"
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Code") = "C"
            dr("Name") = "Custom"
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Code") = "N"
            dr("Name") = "None"
            dt.Rows.Add(dr)
            cboPFCalculatnType.DataSource = dt
            cboPFCalculatnType.ValueMember = "Code"
            cboPFCalculatnType.DisplayMember = "Name"
            isInsideLoad = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



    Private Sub chkPFApplicable_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkPFApplicable.ToggleStateChanged
        If chkPFApplicable.Checked = True Then
            cboPFCalculatnType.Visible = True
            txtPFNo.Enabled = True
        Else
            cboPFCalculatnType.Visible = False
            txtPFNo.Enabled = False
            txtEPFRate.Enabled = False
            txtEPFMaxLimit.Enabled = False
            txtEPFRate.Text = 0
            txtEPFMaxLimit.Text = 0
        End If
    End Sub

    Private Sub cboPFCalculatnType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboPFCalculatnType.SelectedValueChanged
        If isInsideLoad Then
            Exit Sub
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboPFCalculatnType.SelectedValue), "C") = CompairStringResult.Equal Then
            txtEPFRate.Enabled = True
            txtEPFRate.ReadOnly = False
            txtEPFMaxLimit.Enabled = True
            txtEPFMaxLimit.ReadOnly = False
        Else
            txtEPFRate.Enabled = False
            txtEPFMaxLimit.Enabled = False
            txtEPFRate.Text = 0
            txtEPFMaxLimit.Text = 0

        End If
    End Sub




    Dim isCellValueChangedOpen As Boolean = False
    Private Sub gvEmpEx_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvEmpEx.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvEmpEx.Columns(colJoinDesi) Then
                        OpenJoiningDesignation(False)
                    ElseIf e.Column Is gvEmpEx.Columns(colLeavDesi) Then
                        OpenLivingDesignation(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenJoiningDesignation(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gvEmpEx.CurrentRow.Cells(colEmployerName).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = " select designation_id As Code,designation_desc  as [Description]from TSPL_Designation_MASTER "
            gvEmpEx.CurrentRow.Cells(colJoinDesi).Value = clsCommon.ShowSelectForm("EMEexJoin", qry, "Code", "", clsCommon.myCstr(gvEmpEx.CurrentRow.Cells(colJoinDesi).Value), "", isButtonClick)
        End If
    End Sub

    Sub OpenLivingDesignation(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gvEmpEx.CurrentRow.Cells(colEmployerName).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = " select designation_id As Code,designation_desc  as [Description]from TSPL_Designation_MASTER "
            gvEmpEx.CurrentRow.Cells(colLeavDesi).Value = clsCommon.ShowSelectForm("EMEexJoin", qry, "Code", "", clsCommon.myCstr(gvEmpEx.CurrentRow.Cells(colLeavDesi).Value), "", isButtonClick)
        End If
    End Sub

    Sub LoadEmpExGridColumns()
        gvEmpEx.DataSource = Nothing
        gvEmpEx.Rows.Clear()
        gvEmpEx.Columns.Clear()

        gvEmpEx.ReadOnly = False
        Dim lineNo As New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "Line No"
        lineNo.Name = colLineNo
        lineNo.Width = 30
        lineNo.ReadOnly = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEmpEx.Columns.Add(lineNo)

        Dim EmployerName As New GridViewTextBoxColumn()
        EmployerName.FormatString = ""
        EmployerName.HeaderText = "Employer Name"
        EmployerName.Name = colEmployerName
        EmployerName.Width = 150
        EmployerName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        EmployerName.ReadOnly = False
        gvEmpEx.Columns.Add(EmployerName)

        Dim EmployerAddress As New GridViewTextBoxColumn()
        EmployerAddress.FormatString = ""
        EmployerAddress.HeaderText = "Employer Address"
        EmployerAddress.Name = colEmployerAddress
        EmployerAddress.Width = 200
        EmployerAddress.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        EmployerAddress.ReadOnly = False
        gvEmpEx.Columns.Add(EmployerAddress)

        Dim JoiningDate As New GridViewDateTimeColumn()
        JoiningDate.CustomFormat = "dd/MM/yyyy"
        JoiningDate.FormatString = "{0:d}"
        JoiningDate.HeaderText = "Joining Date"
        JoiningDate.Name = colJoiningDate
        JoiningDate.Width = 80
        JoiningDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        JoiningDate.ReadOnly = False
        gvEmpEx.Columns.Add(JoiningDate)

        Dim JoinDesi As New GridViewTextBoxColumn()
        JoinDesi.FormatString = ""
        JoinDesi.HeaderText = "Joining Designation"
        JoinDesi.Name = colJoinDesi
        JoinDesi.Width = 50
        JoinDesi.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        JoinDesi.TextImageRelation = TextImageRelation.TextBeforeImage
        JoinDesi.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        JoinDesi.ReadOnly = False
        gvEmpEx.Columns.Add(JoinDesi)

        Dim JoinSalary As New GridViewDecimalColumn()
        JoinSalary.FormatString = ""
        JoinSalary.HeaderText = "Joining Salary"
        JoinSalary.Name = colJoinSalary
        JoinSalary.Width = 100
        JoinSalary.Maximum = 366
        JoinSalary.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        JoinSalary.ReadOnly = False
        gvEmpEx.Columns.Add(JoinSalary)

        Dim LeavingDate As New GridViewDateTimeColumn()
        LeavingDate.CustomFormat = "dd/MM/yyyy"
        LeavingDate.FormatString = "{0:d}"
        LeavingDate.HeaderText = "Leaving Date"
        LeavingDate.Name = colLeavingDate
        LeavingDate.Width = 80
        LeavingDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        LeavingDate.ReadOnly = False
        gvEmpEx.Columns.Add(LeavingDate)

        Dim LeavingSalary As New GridViewDecimalColumn()
        LeavingSalary.FormatString = ""
        LeavingSalary.HeaderText = "Leaving Salary"
        LeavingSalary.Name = colLeavingSalary
        LeavingSalary.Width = 50
        LeavingSalary.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        LeavingSalary.ReadOnly = False
        gvEmpEx.Columns.Add(LeavingSalary)

        Dim LeavDesi As New GridViewTextBoxColumn()
        LeavDesi.FormatString = ""
        LeavDesi.HeaderText = "Leaving Designation"
        LeavDesi.Name = colLeavDesi
        LeavDesi.Width = 200
        LeavDesi.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        LeavDesi.TextImageRelation = TextImageRelation.TextBeforeImage
        LeavDesi.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        LeavDesi.ReadOnly = False
        gvEmpEx.Columns.Add(LeavDesi)

        Dim Achievements As New GridViewTextBoxColumn()
        Achievements.FormatString = ""
        Achievements.HeaderText = "Achievements"
        Achievements.Name = colAchievements
        Achievements.Width = 200
        Achievements.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Achievements.ReadOnly = False
        gvEmpEx.Columns.Add(Achievements)

        Dim Description As New GridViewTextBoxColumn()
        Description.FormatString = ""
        Description.HeaderText = "Description"
        Description.Name = colDescription
        Description.Width = 200
        Description.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Description.ReadOnly = False
        gvEmpEx.Columns.Add(Description)

        Dim Verification_Done As New GridViewCheckBoxColumn()
        Verification_Done.FormatString = ""
        Verification_Done.HeaderText = "Verification Done"
        Verification_Done.Name = colExpVerification_Done
        Verification_Done.Width = 100
        Verification_Done.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Verification_Done.ReadOnly = False
        gvEmpEx.Columns.Add(Verification_Done)

        Dim Reporting_Person_Name As New GridViewTextBoxColumn()
        Reporting_Person_Name.FormatString = ""
        Reporting_Person_Name.HeaderText = "Reporting Person Name"
        Reporting_Person_Name.Name = colReporting_Person_Name
        Reporting_Person_Name.Width = 100
        Reporting_Person_Name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Reporting_Person_Name.ReadOnly = False
        gvEmpEx.Columns.Add(Reporting_Person_Name)

        Dim Reporting_Person_Designation As New GridViewTextBoxColumn()
        Reporting_Person_Designation.FormatString = ""
        Reporting_Person_Designation.HeaderText = "Reporting Person Designation"
        Reporting_Person_Designation.Name = colReporting_Person_Designation
        Reporting_Person_Designation.Width = 100
        Reporting_Person_Designation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Reporting_Person_Designation.ReadOnly = False
        gvEmpEx.Columns.Add(Reporting_Person_Designation)

        Dim Reporting_Person_Phone As New GridViewTextBoxColumn()
        Reporting_Person_Phone.FormatString = ""
        Reporting_Person_Phone.HeaderText = "Reporting Person Phone"
        Reporting_Person_Phone.Name = colReporting_Person_Phone
        Reporting_Person_Phone.Width = 100
        Reporting_Person_Phone.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Reporting_Person_Phone.ReadOnly = False
        gvEmpEx.Columns.Add(Reporting_Person_Phone)

        Dim Reporting_Person_Mobile As New GridViewTextBoxColumn()
        Reporting_Person_Mobile.FormatString = ""
        Reporting_Person_Mobile.HeaderText = "Reporting Person Mobile"
        Reporting_Person_Mobile.Name = colReporting_Person_Mobile
        Reporting_Person_Mobile.Width = 100
        Reporting_Person_Mobile.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Reporting_Person_Mobile.ReadOnly = False
        gvEmpEx.Columns.Add(Reporting_Person_Mobile)

        Dim Reporting_Person_Email As New GridViewTextBoxColumn()
        Reporting_Person_Email.FormatString = ""
        Reporting_Person_Email.HeaderText = "Reporting Person Email"
        Reporting_Person_Email.Name = colReporting_Person_Email
        Reporting_Person_Email.Width = 100
        Reporting_Person_Email.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Reporting_Person_Email.ReadOnly = False
        gvEmpEx.Columns.Add(Reporting_Person_Email)


        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Verification Status"
        repoRowType.Name = colVERIFICATION_STATUS
        repoRowType.Width = 50
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetVerificationStatus()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
        gvEmpEx.MasterTemplate.Columns.Add(repoRowType)

        repoRowType = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Verification Mode"
        repoRowType.Name = colVERIFICATION_MODE
        repoRowType.Width = 50
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetVerificationMode()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
        gvEmpEx.MasterTemplate.Columns.Add(repoRowType)

        Dim VERIFICATION_REMARKS As New GridViewTextBoxColumn()
        VERIFICATION_REMARKS.FormatString = ""
        VERIFICATION_REMARKS.HeaderText = "Verification Remarks"
        VERIFICATION_REMARKS.Name = colVERIFICATION_REMARKS
        VERIFICATION_REMARKS.Width = 100
        VERIFICATION_REMARKS.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        VERIFICATION_REMARKS.ReadOnly = False
        gvEmpEx.Columns.Add(VERIFICATION_REMARKS)

    End Sub


    Sub LoadEmpQuliGridColumns()
        gvEmpQuli.DataSource = Nothing
        gvEmpQuli.Rows.Clear()
        gvEmpQuli.Columns.Clear()

        gvEmpQuli.ReadOnly = False
        Dim lineNo As New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "Line No"
        lineNo.Name = colQuliLineNo
        lineNo.Width = 30
        lineNo.ReadOnly = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEmpQuli.Columns.Add(lineNo)

        Dim QuliCourseCode As New GridViewTextBoxColumn()
        QuliCourseCode.FormatString = ""
        QuliCourseCode.HeaderText = "Course Code"
        QuliCourseCode.Name = colQuliCourseCode
        QuliCourseCode.Width = 100
        QuliCourseCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpQuli.Columns.Add(QuliCourseCode)

        Dim QuliJoiningDate As New GridViewDateTimeColumn()
        QuliJoiningDate.CustomFormat = "dd/MM/yyyy"
        QuliJoiningDate.FormatString = "{0:d}"
        QuliJoiningDate.HeaderText = "Joining Date"
        QuliJoiningDate.Name = colQuliJoiningDate
        QuliJoiningDate.Width = 80
        QuliJoiningDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEmpQuli.Columns.Add(QuliJoiningDate)

        Dim QuliCompletionDate As New GridViewDateTimeColumn()
        QuliCompletionDate.CustomFormat = "dd/MM/yyyy"
        QuliCompletionDate.FormatString = "{0:d}"
        QuliCompletionDate.HeaderText = "Completion Date"
        QuliCompletionDate.Name = colQuliCompletionDate
        QuliCompletionDate.Width = 80
        QuliCompletionDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEmpQuli.Columns.Add(QuliCompletionDate)

        Dim QuliCollegeUniversity As New GridViewTextBoxColumn()
        QuliCollegeUniversity.FormatString = ""
        QuliCollegeUniversity.HeaderText = "College / University"
        QuliCollegeUniversity.Name = colQuliCollegeUniversity
        QuliCollegeUniversity.Width = 150
        QuliCollegeUniversity.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpQuli.Columns.Add(QuliCollegeUniversity)

        Dim QuliGradePercentage As New GridViewTextBoxColumn()
        QuliGradePercentage.FormatString = ""
        QuliGradePercentage.HeaderText = "Grade Percentage"
        QuliGradePercentage.Name = colQuliGradePercentage
        QuliGradePercentage.Width = 100
        QuliGradePercentage.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpQuli.Columns.Add(QuliGradePercentage)

        Dim QuliDescription As New GridViewTextBoxColumn()
        QuliDescription.FormatString = ""
        QuliDescription.HeaderText = "Other Details"
        QuliDescription.Name = colQuliDescription
        QuliDescription.Width = 200
        QuliDescription.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpQuli.Columns.Add(QuliDescription)

        Dim Verification_Done As New GridViewCheckBoxColumn()
        Verification_Done.FormatString = ""
        Verification_Done.HeaderText = "Verification Done"
        Verification_Done.Name = colQualVerification_Done
        Verification_Done.Width = 100
        Verification_Done.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpQuli.Columns.Add(Verification_Done)

        Dim University_Address As New GridViewTextBoxColumn()
        University_Address.FormatString = ""
        University_Address.HeaderText = "University Address"
        University_Address.Name = colUniversity_Address
        University_Address.Width = 100
        University_Address.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpQuli.Columns.Add(University_Address)

        Dim University_Website As New GridViewTextBoxColumn()
        University_Website.FormatString = ""
        University_Website.HeaderText = "University Website"
        University_Website.Name = colUniversity_Website
        University_Website.Width = 100
        University_Website.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpQuli.Columns.Add(University_Website)

    End Sub

    Private Sub gvEmpQuli_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvEmpQuli.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvEmpQuli.Columns(colQuliCourseCode) Then
                        OpenCource(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenCource(ByVal isButtonClick As Boolean)
        Dim qry As String = " select COURSE_CODE as Code, COURSE_NAME as Name, DESCRIPTION AS Description from TSPL_COURSE_MASTER"
        gvEmpQuli.CurrentRow.Cells(colQuliCourseCode).Value = clsCommon.ShowSelectForm("Course_Master", qry, "Code", "", clsCommon.myCstr(gvEmpQuli.CurrentRow.Cells(colQuliCourseCode).Value), "", isButtonClick)
    End Sub

    Private Sub gvEmpQuli_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvEmpQuli.CurrentColumnChanged
        If gvEmpQuli.RowCount > 0 Then
            Dim intCurrRow As Integer = gvEmpQuli.CurrentRow.Index
            gvEmpQuli.CurrentRow.Cells(colQuliLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvEmpQuli.Rows.Count - 1 Then
                gvEmpQuli.Rows.AddNew()
                gvEmpQuli.CurrentRow = gvEmpQuli.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvEmpEx_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvEmpEx.CurrentColumnChanged
        If gvEmpEx.RowCount > 0 Then
            Dim intCurrRow As Integer = gvEmpEx.CurrentRow.Index
            gvEmpEx.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvEmpEx.Rows.Count - 1 Then
                gvEmpEx.Rows.AddNew()
                gvEmpEx.CurrentRow = gvEmpEx.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvEmpLanguage_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvEmpLanguage.CurrentColumnChanged
        If gvEmpLanguage.RowCount > 0 Then
            Dim intCurrRow As Integer = gvEmpLanguage.CurrentRow.Index
            gvEmpLanguage.CurrentRow.Cells(colLangLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvEmpLanguage.Rows.Count - 1 Then
                gvEmpLanguage.Rows.AddNew()
                gvEmpLanguage.CurrentRow = gvEmpLanguage.Rows(intCurrRow)
            End If
        End If
    End Sub

    Sub LoadEmpLangGridColumns()
        gvEmpLanguage.DataSource = Nothing
        gvEmpLanguage.Rows.Clear()
        gvEmpLanguage.Columns.Clear()
        gvEmpLanguage.ReadOnly = False

        Dim LangLineNo As New GridViewTextBoxColumn()
        LangLineNo.FormatString = ""
        LangLineNo.HeaderText = "Line No"
        LangLineNo.Name = colLangLineNo
        LangLineNo.Width = 30
        LangLineNo.ReadOnly = True
        LangLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEmpLanguage.Columns.Add(LangLineNo)

        Dim LangCode As New GridViewTextBoxColumn()
        LangCode.FormatString = ""
        LangCode.HeaderText = "Language Code"
        LangCode.Name = colLangCode
        LangCode.Width = 100
        LangCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        LangCode.ReadOnly = False
        gvEmpLanguage.Columns.Add(LangCode)

        Dim LangReadingLevel As New GridViewComboBoxColumn()
        LangReadingLevel.FormatString = ""
        LangReadingLevel.HeaderText = "Reading Level"
        LangReadingLevel.Name = colLangReadingLevel
        LangReadingLevel.Width = 150
        LangReadingLevel.DataSource = GetLevel()
        LangReadingLevel.ValueMember = "Code"
        LangReadingLevel.DisplayMember = "Description"
        LangReadingLevel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft

        gvEmpLanguage.Columns.Add(LangReadingLevel)

        Dim LangWrittingLevel As New GridViewComboBoxColumn()
        LangWrittingLevel.FormatString = ""
        LangWrittingLevel.HeaderText = "Writing Level"
        LangWrittingLevel.Name = colLangWrittingLevel
        LangWrittingLevel.Width = 100
        LangWrittingLevel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        LangWrittingLevel.DataSource = GetLevel()
        LangWrittingLevel.ValueMember = "Code"
        LangWrittingLevel.DisplayMember = "Description"
        gvEmpLanguage.Columns.Add(LangWrittingLevel)

        Dim LangSpeakingLevel As New GridViewComboBoxColumn()
        LangSpeakingLevel.FormatString = ""
        LangSpeakingLevel.HeaderText = "Speaking Level"
        LangSpeakingLevel.Name = colLangSpeakingLevel
        LangSpeakingLevel.Width = 100
        LangSpeakingLevel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        LangSpeakingLevel.DataSource = GetLevel()
        LangSpeakingLevel.ValueMember = "Code"
        LangSpeakingLevel.DisplayMember = "Description"
        gvEmpLanguage.Columns.Add(LangSpeakingLevel)

        Dim LangDescription As New GridViewTextBoxColumn()
        LangDescription.FormatString = ""
        LangDescription.HeaderText = "Other Details"
        LangDescription.Name = colLangDescription
        LangDescription.Width = 200
        LangDescription.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpLanguage.Columns.Add(LangDescription)
    End Sub

    Private Function GetLevel() As DataTable
        Return clsDBFuncationality.GetDataTable("select Code,Description from tspl_fixed_parameter where type='Language'")
    End Function

    Private Function GetRelation() As DataTable
        Return clsDBFuncationality.GetDataTable("select Code,Description from tspl_fixed_parameter where type='Relation'")
    End Function

    Private Sub gvEmpLanguage_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvEmpLanguage.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvEmpLanguage.Columns(colLangCode) Then
                        OpenEmpLang(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenEmpLang(ByVal isButtonClick As Boolean)
        Dim qry As String = "select LANGUAGE_CODE as Code, LANGUAGE_NAME as Name, DESCRIPTION as Description from TSPL_LANGUAGE_MASTER"
        gvEmpLanguage.CurrentRow.Cells(colLangCode).Value = clsCommon.ShowSelectForm("LANGUAGE_MASTER", qry, "Code", "", clsCommon.myCstr(gvEmpLanguage.CurrentRow.Cells(colLangCode).Value), "", isButtonClick)
    End Sub


    Sub LoadEmpFamilyGridColumns()
        gvEmpFamily.DataSource = Nothing
        gvEmpFamily.Rows.Clear()
        gvEmpFamily.Columns.Clear()
        gvEmpFamily.ReadOnly = False

        Dim FamilyLineNo As New GridViewTextBoxColumn()
        FamilyLineNo.FormatString = ""
        FamilyLineNo.HeaderText = "Line No"
        FamilyLineNo.Name = colFamilyLineNo
        FamilyLineNo.Width = 30
        FamilyLineNo.ReadOnly = True
        FamilyLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEmpFamily.Columns.Add(FamilyLineNo)

        Dim FamilyMemberName As New GridViewTextBoxColumn()
        FamilyMemberName.FormatString = ""
        FamilyMemberName.HeaderText = "Name"
        FamilyMemberName.Name = colFamilyMemberName
        FamilyMemberName.Width = 100
        FamilyMemberName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpFamily.Columns.Add(FamilyMemberName)

        'Ticket No-ERO/21/08/19-000998,Sanjay ,Manual Entry
        'Dim FamilyRelation As New GridViewComboBoxColumn(
        Dim FamilyRelation As New GridViewTextBoxColumn()
        FamilyRelation.FormatString = ""
        FamilyRelation.HeaderText = "Relation"
        FamilyRelation.Name = colFamilyRelation
        FamilyRelation.Width = 150
        'FamilyRelation.DataSource = GetRelation()
        'FamilyRelation.ValueMember = "Code"
        'FamilyRelation.DisplayMember = "Description"
        FamilyRelation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpFamily.Columns.Add(FamilyRelation)

        Dim FamilyAge As New GridViewTextBoxColumn()
        FamilyAge.FormatString = ""
        FamilyAge.HeaderText = "Age"
        FamilyAge.Name = colFamilyAge
        FamilyAge.Width = 100
        FamilyAge.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpFamily.Columns.Add(FamilyAge)

        Dim FamilySex As New GridViewComboBoxColumn()
        FamilySex.FormatString = ""
        FamilySex.HeaderText = "Gender"
        FamilySex.Name = colFamilySex
        FamilySex.Width = 100
        FamilySex.DataSource = GetGender()
        FamilySex.ValueMember = "Code"
        FamilySex.DisplayMember = "Code"
        FamilySex.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpFamily.Columns.Add(FamilySex)

        Dim FamilyDescription As New GridViewTextBoxColumn()
        FamilyDescription.FormatString = ""
        FamilyDescription.HeaderText = "Other Details"
        FamilyDescription.Name = colFamilyDescription
        FamilyDescription.Width = 200
        FamilyDescription.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpFamily.Columns.Add(FamilyDescription)

        Dim Is_Dependent As New GridViewCheckBoxColumn()
        Is_Dependent.FormatString = ""
        Is_Dependent.HeaderText = "Is Dependent"
        Is_Dependent.Name = colIs_Dependent
        Is_Dependent.Width = 100
        Is_Dependent.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpFamily.Columns.Add(Is_Dependent)

        Dim Member_DOB As New GridViewDateTimeColumn()
        Member_DOB.CustomFormat = "dd/MM/yyyy"
        Member_DOB.FormatString = "{0:d}"
        Member_DOB.HeaderText = "DOB"
        Member_DOB.Name = colMember_DOB
        Member_DOB.Width = 80
        Member_DOB.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEmpFamily.Columns.Add(Member_DOB)


        Dim Member_Occupation As New GridViewTextBoxColumn()
        Member_Occupation.FormatString = ""
        Member_Occupation.HeaderText = "Occupation"
        Member_Occupation.Name = colMember_Occupation
        Member_Occupation.Width = 100
        Member_Occupation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpFamily.Columns.Add(Member_Occupation)

        Dim Dependent_Living_With_Emp As New GridViewCheckBoxColumn()
        Dependent_Living_With_Emp.FormatString = ""
        Dependent_Living_With_Emp.HeaderText = "Living with employee"
        Dependent_Living_With_Emp.Name = colDependent_Living_With_Emp
        Dependent_Living_With_Emp.Width = 100
        Dependent_Living_With_Emp.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpFamily.Columns.Add(Dependent_Living_With_Emp)


        Member_Occupation = New GridViewTextBoxColumn()
        Member_Occupation.FormatString = ""
        Member_Occupation.HeaderText = "Contact No"
        Member_Occupation.Name = colFDContactNo
        Member_Occupation.Width = 100
        Member_Occupation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpFamily.Columns.Add(Member_Occupation)

    End Sub


    Private Sub gvEmpFamily_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvEmpFamily.CurrentColumnChanged
        If gvEmpFamily.RowCount > 0 Then
            Dim intCurrRow As Integer = gvEmpFamily.CurrentRow.Index
            gvEmpFamily.CurrentRow.Cells(colFamilyLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvEmpFamily.Rows.Count - 1 Then
                gvEmpFamily.Rows.AddNew()
                gvEmpFamily.CurrentRow = gvEmpFamily.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub txtEmployeeBand__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtEmployeeBand._MYValidating
        Try
            Me.txtEmployeeBand.Value = clsEmployeeBandMaster.getFinder("", txtEmployeeBand.Value, isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndcity__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndcity._MYValidating
        fndcity.Value = clsCityMaster.getFinder("", fndcity.Value, isButtonClicked)
    End Sub

    Private Sub MyLabel42_Click(sender As Object, e As EventArgs) Handles MyLabel42.Click

    End Sub
End Class