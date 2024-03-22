' ----------------- Created By Anubhooti On 07-Aug-2014 Against -------------------- '
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports XpertERPEngine
Imports common
Imports System.IO

Public Class FrmApplicantEntry
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Dim File_Name As String = ""
    Dim SalAccToM As Boolean = False
    Dim SalAccToF As Boolean = False
    Dim ComboLoad As Boolean = False
    Dim ShortListed As Integer = 0
    Dim Rejected As Integer = 0
#Region "Qualification Details"
    Public Const FullType As String = "Full Type"
    Public Const PartType As String = "Part Type"
    Const ColUnvClg As String = "Unv/College"
    Const ColCourse As String = "Course Code"
    Const ColCourseName As String = "Course Name"
    Const ColYrOfPassing As String = "Year Of Passing"
    Const ColGradePercentage As String = "Grade%tage"
    Const ColCourseType As String = "Course Type"
    Const ColHighQual As String = "Higher Qualification"
#End Region
#Region "Chek List"
    Const ColCheckListCode As String = "Check List Code"
    Const ColCheckListName As String = "Check List Name"
    Const ColIsManadatory As String = "Manadatory"
    Const ColIsReceived As String = "Received"
#End Region
#Region "Emp History"
    Const ColOrgName As String = "Organisation Name"
    Const ColFromPeriod As String = "From Period"
    Const ColToPeriod As String = "To Period"
    Const ColTillDate As String = "Till Date"
    Const ColDesignation As String = "Designation"
    Const ColDesignationName As String = "Designation Name"
    Const ColRolesRes As String = "Roles And Responsibilities"
    Const ColReasSep As String = "Reason for Seperation"
#End Region
#Region "Family Background"
    Const ColRelation As String = "Relation"
    Const ColRelationName As String = "Relation Name"
    Const ColQualification As String = "Qualification"
    Const ColQualificationName As String = "Qualification Name"
    Const ColOccupation As String = "Occupation"
    Const ColOccupationName As String = "Occupation Name"
    Const ColDateofBirth As String = "Date of Birth"
#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmApplicantEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    'Function Age(ByVal AppDate As Date, ByVal DOB As Date) As Integer
    '    Dim ApplicantAge As Date
    '    ApplicantAge = DateDiff("yyyy", AppDate, DOB)

    'End Function
    Private Function Y_M_D_Diff(ByVal DateOne As DateTime, ByVal DateTwo As DateTime) As String
        Dim Year, Month, Day As Integer

        ' Function to display difference between two dates in Years, Months and Days, calling routine ensures that DateOne is always earlier than DateTwo
        If DateOne <> DateTwo Then                                          ' If both dates the same then exit with zeros returned otherwise a difference of one year gets returned!!!
            ' Years
            If DateTwo.Year > DateOne.Year Then                       ' If year is the same in both dates, an out of range exception is thrown!!!
                Year = DateTwo.AddYears(-DateOne.Year).Year       ' Subtract DateOne years from DateTwo years to get difference
            End If

            ' Months
            Month = DateTwo.AddMonths(-DateOne.Month).Month         ' Subtract DateOne months from DateTwo months
            If DateTwo.Month <= DateOne.Month Then                        ' Decrement year by one if DateTwo months hasn't exceeded DateOne months, i.e. not full year yet
                If Year > 0 Then Year -= 1
            End If

            ' Days
            Day = DateTwo.AddDays(-DateOne.Day).Day                         ' Subtract DateOne days from DateTwo days
            If DateTwo.Day <= DateOne.Day Then                             ' Decrement month by one if DateTwo days hasn't exceeded DateOne days - not full month yet
                If Month > 0 Then Month -= 1
            End If
            If DateOne.Day = DateTwo.Day Then                        ' Avoid silliness like "1 month 31 days" instead of 2 months
                Day = 0                                                                          ' Reset days
                Month += 1                                                                   ' And increment month
            End If

            ' Corrections
            If Month = 12 Then                                                         ' Months value goes up to 12, and we want a maximum of 11, so:
                Month = 0                                                                     ' Reset months to zero
                Year += 1                                                                       ' And increment year
            End If
            If DateOne.Year = DateTwo.Year AndAlso DateOne.Month = DateOne.Month Then            ' If year and month are the same in DateOne & DateTwo then month = 12 and therefore year has been incremented
                Year = 0                                                                     ' So reset it
            End If

        End If         ' DateOne <> DateTwo

        Return Year & " years, " & Month & " months, " & Day & " days"                  ' Concatenate string and return to calling routine

    End Function       ' Y_M_D_Diff
    Function AllowToSave() As Boolean
        Try
            btnsave.Focus()
            Dim check As Match = Regex.Match(txtEmail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            Dim checkPan As New System.Text.RegularExpressions.Regex("^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$")
            Dim ApplicantAge As Double
            ApplicantAge = DateDiff("yyyy", dtpDateofBirth.Value, dtpDate.Value)
            'If clsCommon.myLen(txtcode.Value) <= 0 Then
            '    Me.RadPageView1.SelectedPage = RadPageViewPage1
            '    txtcode.Focus()
            '    Throw New Exception("Code can not be left blank")
            If clsCommon.myLen(txtrequisitioncode.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                txtrequisitioncode.Focus()
                Throw New Exception("Requisition code can not be left blank")
            End If
            If clsCommon.myLen(txtrequisitioncode.Value) > 0 Then
                Dim RequsitionCode As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_HR_REQUISITION Where Requisition_Code ='" + clsCommon.myCstr(txtrequisitioncode.Value) + "'"))
                If RequsitionCode = 0 Then
                    Throw New Exception("Please check ! requsition code does not exist")
                End If
            End If
            If clsCommon.myLen(txtsourcetype.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                txtsourcetype.Focus()
                Throw New Exception("Source type code can not be left blank")
            End If
            If clsCommon.myLen(txtsourcetype.Value) > 0 Then
                Dim SourceType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_HR_SOURCE_TYPE Where Source_Type_Code  ='" + clsCommon.myCstr(txtsourcetype.Value) + "'"))
                If SourceType = 0 Then
                    Throw New Exception("Please check ! source type does not exist")
                End If
            End If
            If clsCommon.myLen(txtsourcedetail.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                txtsourcedetail.Focus()
                Throw New Exception("Source type detail code can not be left blank")
            End If
            If clsCommon.myLen(txtsourcedetail.Value) > 0 Then
                Dim SourceDetail As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(Source_Type_Detail_Code) As Row  From TSPL_HR_SOURCE_TYPE_DETAIL Where Source_Type_Code  ='" + clsCommon.myCstr(txtsourcetype.Value) + "'"))
                If SourceDetail = 0 Then
                    Throw New Exception("Please check ! source detail does not exist")
                End If
            End If
            If clsCommon.myLen(cmbGender.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                cmbGender.Focus()
                Throw New Exception("Gender not can be left blank")
            End If
            If clsCommon.myLen(cmbSalutation.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                cmbSalutation.Focus()
                Throw New Exception("Salutation can not be left blank")
            End If
            If clsCommon.myLen(txtFirstName.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                txtFirstName.Focus()
                Throw New Exception("First name can not be left blank")
            End If
            If clsCommon.myLen(txtLastName.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                txtLastName.Focus()
                Throw New Exception("Last name can not be left blank")
            End If
            If clsCommon.myLen(dtpDateofBirth.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                dtpDateofBirth.Focus()
                Throw New Exception("Applicant date of birth can not be left blank")
            End If
            If clsCommon.myLen(CmbMarStatus.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                CmbMarStatus.Focus()
                Throw New Exception("Maritial status can not be left blank")
            End If
            If clsCommon.myLen(txtPanNo.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                txtPanNo.Focus()
                Throw New Exception("PAN No. can not be left blank")
            End If
            If clsCommon.myLen(txtPanNo.Text) > 0 Then
                If Not checkPan.IsMatch(txtPanNo.Text) Then
                    Me.RadPageView1.SelectedPage = RadPageViewPage1
                    txtPanNo.Focus()
                    Throw New Exception("Please check ! PAN numbers need to have 5 characters followed by 4 numbers then a final character")
                End If
            End If
            If clsCommon.myLen(txtAdd1.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                txtAdd1.Focus()
                Throw New Exception("Address 1 can not be left blank")
            End If
            If ApplicantAge < 18 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                dtpDateofBirth.Focus()
                Throw New Exception("Please Check ! age should not be less than 18 ")
            End If
            If clsCommon.myLen(txtCountry.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                txtCountry.Focus()
                Throw New Exception("Country code can not be left blank")
            End If
            If clsCommon.myLen(txtCountry.Value) > 0 Then
                Dim CountryCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_COUNTRY_MASTER Where COUNTRY_CODE  ='" + clsCommon.myCstr(txtCountry.Value) + "'"))
                If CountryCode = 0 Then
                    Me.RadPageView1.SelectedPage = RadPageViewPage1
                    Throw New Exception("Please check ! country code does not exist")
                End If
            End If
            If clsCommon.myLen(txtState.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                txtState.Focus()
                Throw New Exception("State code can not be left blank")
            End If
            If clsCommon.myLen(txtState.Value) > 0 Then
                Dim StateCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_STATE_MASTER Where STATE_CODE  ='" + clsCommon.myCstr(txtState.Value) + "'"))
                If StateCode = 0 Then
                    Me.RadPageView1.SelectedPage = RadPageViewPage1
                    Throw New Exception("Please check ! state code does not exist")
                End If
            End If
            If clsCommon.myLen(txtCity.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                txtCity.Focus()
                Throw New Exception("City code can not be left blank")
            End If
            If clsCommon.myLen(txtCity.Value) > 0 Then
                Dim City_Code As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_CITY_MASTER Where City_Code  ='" + clsCommon.myCstr(txtCity.Value) + "'"))
                If City_Code = 0 Then
                    Me.RadPageView1.SelectedPage = RadPageViewPage1
                    Throw New Exception("Please check ! city code does not exist")
                End If
            End If
            If clsCommon.CompairString(txtTelephone.Value, "(+__)__________") = CompairStringResult.Equal Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                txtTelephone.Focus()
                Throw New Exception("Telephone no. can not be left blank.")
            End If
            If clsCommon.myLen(txtEmail.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                txtEmail.Focus()
                Throw New Exception("Applicant email can not be left blank")
            End If
            If check.Success = False AndAlso txtEmail.Text <> "" Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtEmail.Focus()
                Throw New Exception("Please Enter Valid Email, It is in Invalid Format")
            End If
            '' Check For Duplication Of Applicant Entry
            If btnsave.Text = "Save" Then
                Dim dtCheck As DataTable
                dtCheck = clsDBFuncationality.GetDataTable("select SUM(1),MAX(Applicant_Date) as Applicant_Date from TSPL_HR_APPLICANT_ENTRY where First_Name + ' '  + Middle_Name + ' ' + Last_Name ='" + clsCommon.myCstr(txtFirstName.Text) + " " + clsCommon.myCstr(txtMiddleName.Text) + " " + clsCommon.myCstr(txtLastName.Text) + "' And Applicant_Date_Of_Birth='" + clsCommon.GetPrintDate(dtpDateofBirth.Value, "dd/MMM/yyyy") + "' AND TELEPHONE_NO ='" + clsCommon.myCstr(txtTelephone.Text) + "'  AND Email ='" + clsCommon.myCstr(txtEmail.Text) + "' AND Applicant_Code <> '" + clsCommon.myCstr(txtcode.Value) + "' GROUP BY First_Name,Middle_Name,Last_Name,Applicant_Date_Of_Birth,TELEPHONE_NO,Email HAVING SUM(1) >0 ")
                If dtCheck IsNot Nothing AndAlso dtCheck.Rows.Count > 0 Then

                    Dim AppDate As DateTime
                    Dim TodayDate As DateTime
                    TodayDate = dtpDate.Value
                    AppDate = clsCommon.myCstr(dtCheck.Rows(0)("Applicant_Date"))
                    Dim Year As Integer = Math.Abs((TodayDate.Year - AppDate.Year))
                    Dim Months As Integer = ((Year * 12) + Math.Abs((TodayDate.Month - AppDate.Month)))
                    Dim Message As String = Y_M_D_Diff(AppDate, TodayDate)
                    'MessageBox.Show(Message)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    If common.clsCommon.MyMessageBoxShow(Me, "This Applicant has applied " + Message + " back.Do you still want to continue ", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    Else
                        Return False
                    End If

                End If
            End If
            If btnsave.Text = "Update" Then
                If common.clsCommon.MyMessageBoxShow(Me, "Do you want to update this entry (" + clsCommon.myCstr(txtcode.Value) + ")", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Else
                    Return False
                End If
            End If
            ''
            '' Add Info
            If rbnRefbyEmp.IsChecked = True AndAlso (clsCommon.myLen(txtEmpCode.Value) <= 0 Or clsCommon.myLen(txtRelation.Value) <= 0) Then
                Me.RadPageView1.SelectedPage = RadPageViewPage6
                txtEmpCode.Focus()
                Throw New Exception("Emp code and relation can code not be left blank")
            End If
            If clsCommon.myLen(txtRelation.Value) > 0 Then
                Dim Relation_Code As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_HR_RELATION_MASTER Where Relation_Code  ='" + clsCommon.myCstr(txtRelation.Value) + "'"))
                If Relation_Code = 0 Then
                    Me.RadPageView1.SelectedPage = RadPageViewPage6
                    Throw New Exception("Please check ! relation code does not exist")
                End If
            End If
            If clsCommon.myLen(txtEmpCode.Value) > 0 Then
                Dim EMP_CODE As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_EMPLOYEE_MASTER Where EMP_CODE  ='" + clsCommon.myCstr(txtEmpCode.Value) + "'"))
                If EMP_CODE = 0 Then
                    Me.RadPageView1.SelectedPage = RadPageViewPage6
                    Throw New Exception("Please check ! emp code does not exist")
                End If
            End If
            'If rbnrefbyAge.IsChecked = True AndAlso clsCommon.myLen(txtAgency.Value) <= 0 Then
            '    Me.RadPageView1.SelectedPage = RadPageViewPage6
            '    txtAgency.Focus()
            '    Throw New Exception("Agency code can not be left blank")
            'End If
            'If clsCommon.myLen(txtAgency.Value) > 0 Then
            '    Dim CODE As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From Tspl_HR_Agency_Master Where CODE  ='" + clsCommon.myCstr(txtAgency.Value) + "'"))
            '    If CODE = 0 Then
            '        Throw New Exception("Please check ! agency code does not exist")
            '    End If
            'End If
            'If ChkHandicaped.Checked = True AndAlso clsCommon.myLen(txtHandiDetail.Text) <= 0 Then
            '    Me.RadPageView1.SelectedPage = RadPageViewPage5
            '    txtHandiDetail.Focus()
            '    Throw New Exception("Handicaped detail not be left blank")
            'End If
            'If clsCommon.myLen(txtBankCode.Value) <= 0 Then
            '    Me.RadPageView1.SelectedPage = RadPageViewPage5
            '    txtBankCode.Focus()
            '    Throw New Exception("Bank code not be left blank")
            'End If
            'If clsCommon.myLen(txtBranchCode.Value) <= 0 Then
            '    Me.RadPageView1.SelectedPage = RadPageViewPage5
            '    txtBranchCode.Focus()
            '    Throw New Exception("Branch code not be left blank")
            'End If
            'If clsCommon.myLen(txtCurrGrossSal.Value) <= 0 Or clsCommon.myCdbl(txtCurrGrossSal.Value) <= 0 Then
            '    Me.RadPageView1.SelectedPage = RadPageViewPage5
            '    txtCurrGrossSal.Focus()
            '    Throw New Exception("Current gross salary can not be left blank or incorrect")
            'End If
            'If clsCommon.myLen(txtTotalCTC.Value) <= 0 Or clsCommon.myCdbl(txtTotalCTC.Value) <= 0 Then
            '    Me.RadPageView1.SelectedPage = RadPageViewPage5
            '    txtTotalCTC.Focus()
            '    Throw New Exception("Total CTC not be left blank or incorrect")
            'End If
            'If ChkRelocation.Checked = True AndAlso (clsCommon.myLen(txtFromLoc.Value) <= 0 Or clsCommon.myLen(txtToLoc.Value) <= 0) Then
            '    Me.RadPageView1.SelectedPage = RadPageViewPage5
            '    txtFromLoc.Focus()
            '    Throw New Exception("From and to location can not be left blank")
            'End If
            'If clsCommon.myLen(txtFromLoc.Value) > 0 Then
            '    Dim F_Code As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_LOCATION_MASTER Where Location_Code  ='" + clsCommon.myCstr(txtFromLoc.Value) + "'"))
            '    If F_Code = 0 Then
            '        Throw New Exception("Please check ! from location code does not exist")
            '    End If
            'End If
            'If clsCommon.myLen(txtToLoc.Value) > 0 Then
            '    Dim T_Code As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_LOCATION_MASTER Where Location_Code  ='" + clsCommon.myCstr(txtToLoc.Value) + "'"))
            '    If T_Code = 0 Then
            '        Throw New Exception("Please check ! to location code does not exist")
            '    End If
            'End If
            'If clsCommon.myLen(txtLocation.Value) <= 0 Then
            '    Me.RadPageView1.SelectedPage = RadPageViewPage5
            '    txtLocation.Focus()
            '    Throw New Exception("Location code can not be left blank")
            'End If
            'If clsCommon.myLen(txtLocation.Value) > 0 Then
            '    Dim Location_Code As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_LOCATION_MASTER Where Location_Code  ='" + clsCommon.myCstr(txtLocation.Value) + "'"))
            '    If Location_Code = 0 Then
            '        Throw New Exception("Please check ! location code does not exist")
            '    End If
            'End If
            'If clsCommon.myLen(txtPreferedLoc.Value) <= 0 Then
            '    Me.RadPageView1.SelectedPage = RadPageViewPage5
            '    txtPreferedLoc.Focus()
            '    Throw New Exception("Preferred location can not be left blank")
            'End If
            'If clsCommon.myLen(txtPreferedLoc.Value) > 0 Then
            '    Dim PLocation_Code As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_LOCATION_MASTER Where Location_Code  ='" + clsCommon.myCstr(txtPreferedLoc.Value) + "'"))
            '    If PLocation_Code = 0 Then
            '        Throw New Exception("Please check ! preferred location code does not exist")
            '    End If
            'End If

            '' qualification
            Dim GridRowQual As Integer = 0
            Dim HighQual As Integer = 0
            Dim QLineNo As Integer = 1

            For Each grow As GridViewRowInfo In gvQualification.Rows
                'If clsCommon.myLen(grow.Cells(ColUnvClg).Value) <= 0 Then
                '    Continue For
                'End If
                QLineNo += 1
                If clsCommon.myLen(grow.Cells(ColUnvClg).Value) > 0 Then
                    If clsCommon.myLen(grow.Cells(ColCourse).Value) <= 0 Then
                        Me.RadPageView1.SelectedPage = RadPageViewPage3
                        Throw New Exception("Please fill course code at line no. " + clsCommon.myCstr(QLineNo) + "")
                    End If
                    If clsCommon.myLen(grow.Cells(ColYrOfPassing).Value) <= 0 Then
                        Me.RadPageView1.SelectedPage = RadPageViewPage3
                        Throw New Exception("Please fill year of passing at line no. " + clsCommon.myCstr(QLineNo) + "")
                    End If
                    If clsCommon.myLen(grow.Cells(ColGradePercentage).Value) <= 0 Then
                        Me.RadPageView1.SelectedPage = RadPageViewPage3
                        Throw New Exception("Please fill grade or percentage at line no. " + clsCommon.myCstr(QLineNo) + "")
                    End If
                    If IsNumeric(grow.Cells(ColGradePercentage).Value) Then
                        If clsCommon.myCdbl(grow.Cells(ColGradePercentage).Value) <= 0 Or clsCommon.myCdbl(grow.Cells(ColGradePercentage).Value) > 100 Then
                            Me.RadPageView1.SelectedPage = RadPageViewPage3
                            Throw New Exception("Grade can not be negative or more than 100 at line no. " + clsCommon.myCstr(QLineNo) + "")
                        End If
                    End If

                    If clsCommon.myLen(grow.Cells(ColCourseType).Value) <= 0 Then
                        Me.RadPageView1.SelectedPage = RadPageViewPage3
                        Throw New Exception("Please fill course type at line no. " + clsCommon.myCstr(QLineNo) + "")
                    End If
                    If CBool(grow.Cells(ColHighQual).Value) = True Then
                        HighQual = HighQual + 1
                    End If
                    GridRowQual = GridRowQual + 1
                End If
                'If clsCommon.myLen(grow.Cells(ColUnvClg).Value) <= 0 Then
                '    Throw New Exception("please fill university or college code")
                'End If

            Next
            If GridRowQual <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage3
                Throw New Exception("Enter at least one qualification code")
            End If
            If HighQual > 1 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage3
                Throw New Exception("You can not make more than one course as your higher qualification")
            End If
            '' Check List 
            Dim GridRowChk As Integer = 0
            If gvDoc.Rows.Count > 1 Then
                For Each grow As GridViewRowInfo In gvDoc.Rows
                    If clsCommon.myLen(grow.Cells("Check Code").Value) > 0 Then
                        GridRowChk = GridRowChk + 1
                    End If
                Next
            End If

            'If GridRowChk <= 0 Then
            '    Me.RadPageView1.SelectedPage = RadPageViewPage4
            '    Throw New Exception("Enter at least one Check list Code")
            'End If

            '' References 
            If rbnrefbyAge.IsChecked = True AndAlso clsCommon.myLen(txtAgency.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage6
                txtAgency.Focus()
                Throw New Exception("Agency code can not be left blank")
            End If
            If clsCommon.myLen(txtAgency.Value) > 0 Then
                Dim CODE As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From Tspl_HR_Agency_Master Where CODE  ='" + clsCommon.myCstr(txtAgency.Value) + "'"))
                If CODE = 0 Then
                    Throw New Exception("Please check ! agency code does not exist")
                End If
            End If
            ''

            '' Emp History 
            Dim GridRowEmp As Integer = 0
            Dim ELineNo As Integer = 0
            Dim ToPeriod As Date? = Nothing
            Dim FromPeriod As Date? = Nothing

            If ChkFresher.Checked = False Then

                For Each grow As GridViewRowInfo In gvEmpHis.Rows
                    If clsCommon.myLen(grow.Cells(ColOrgName).Value) > 0 Then
                        GridRowEmp = GridRowEmp + 1
                        ELineNo += 1
                        If clsCommon.myLen(grow.Cells(ColFromPeriod).Value) <= 0 Then
                            Me.RadPageView1.SelectedPage = RadPageViewPage2
                            Throw New Exception("From period can not be left blank at line no " + clsCommon.myCstr(ELineNo) + "")
                        End If
                        If clsCommon.myLen(grow.Cells(ColToPeriod).Value) <= 0 Then
                            Me.RadPageView1.SelectedPage = RadPageViewPage2
                            Throw New Exception("To period can not be left blank at line no " + clsCommon.myCstr(ELineNo) + "")
                        End If
                        If clsCommon.myLen(grow.Cells(ColDesignation).Value) <= 0 Then
                            Me.RadPageView1.SelectedPage = RadPageViewPage2
                            Throw New Exception("Please fill designation at line no " + clsCommon.myCstr(ELineNo) + "")
                        End If
                        If clsCommon.myLen(grow.Cells(ColRolesRes).Value) <= 0 Then
                            Me.RadPageView1.SelectedPage = RadPageViewPage2
                            Throw New Exception("Please fill roles and responsibilties at line no " + clsCommon.myCstr(ELineNo) + "")
                        End If
                        If clsCommon.myLen(grow.Cells(ColReasSep).Value) <= 0 Then
                            Me.RadPageView1.SelectedPage = RadPageViewPage2
                            Throw New Exception("Please fill reason for seperation at line no " + clsCommon.myCstr(ELineNo) + "")
                        End If
                        ToPeriod = clsCommon.myCDate(grow.Cells(ColToPeriod).Value)
                        FromPeriod = clsCommon.myCDate(grow.Cells(ColFromPeriod).Value)
                        If FromPeriod > ToPeriod Then
                            Me.RadPageView1.SelectedPage = RadPageViewPage2
                            Throw New Exception("Please check To period should be greater than From period date at line no " + clsCommon.myCstr(ELineNo) + "")
                        End If
                    End If
                Next
                If GridRowEmp <= 0 Then
                    Me.RadPageView1.SelectedPage = RadPageViewPage2
                    Throw New Exception("Enter at least one Employement Detail")
                End If
            End If
            '' Family Background
            Dim GridRowFam As Integer = 0
            Dim FLineNo As Integer = 0
            For Each grow As GridViewRowInfo In gvFamily.Rows
                If clsCommon.myLen(grow.Cells(ColRelation).Value) > 0 Then
                    GridRowFam = GridRowFam + 1
                    FLineNo += 1
                    If clsCommon.myLen(grow.Cells(ColQualification).Value) <= 0 Then
                        Me.RadPageView1.SelectedPage = RadPageViewPage5
                        Throw New Exception("Qualification can not be left blank at line no " + clsCommon.myCstr(FLineNo) + "")
                    End If
                    If clsCommon.myLen(grow.Cells(ColOccupation).Value) <= 0 Then
                        Me.RadPageView1.SelectedPage = RadPageViewPage5
                        Throw New Exception("Please fill occupation at line no " + clsCommon.myCstr(FLineNo) + "")
                    End If
                    If clsCommon.myLen(grow.Cells(ColDateofBirth).Value) <= 0 Then
                        Me.RadPageView1.SelectedPage = RadPageViewPage5
                        Throw New Exception("Please fill date of birth at line no " + clsCommon.myCstr(FLineNo) + "")
                    End If
                End If
            Next
            If GridRowFam <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage5
                Throw New Exception("Enter at least one family background Detail")
            End If
            '' Controls
            If ChkHandicaped.Checked = True AndAlso clsCommon.myLen(txtHandiDetail.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage5
                txtHandiDetail.Focus()
                Throw New Exception("Handicaped detail not be left blank")
            End If
            If clsCommon.myLen(txtBloodGrp.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage5
                txtBloodGrp.Focus()
                Throw New Exception("Blood group can not be left blank")
            End If
            If clsCommon.myLen(txtBankCode.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage5
                txtBankCode.Focus()
                Throw New Exception("Bank code not be left blank")
            End If
            If clsCommon.myLen(txtBranchCode.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage5
                txtBranchCode.Focus()
                Throw New Exception("Branch code not be left blank")
            End If
            If clsCommon.myLen(txtCurrGrossSal.Value) <= 0 Or clsCommon.myCdbl(txtCurrGrossSal.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage5
                txtCurrGrossSal.Focus()
                Throw New Exception("Current gross salary can not be left blank or incorrect")
            End If
            If clsCommon.myLen(txtTotalCTC.Value) <= 0 Or clsCommon.myCdbl(txtTotalCTC.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage5
                txtTotalCTC.Focus()
                Throw New Exception("Total CTC not be left blank or incorrect")
            End If
            If ChkRelocation.Checked = True AndAlso (clsCommon.myLen(txtFromLoc.Value) <= 0 Or clsCommon.myLen(txtToLoc.Value) <= 0) Then
                Me.RadPageView1.SelectedPage = RadPageViewPage5
                txtFromLoc.Focus()
                Throw New Exception("From and to location can not be left blank")
            End If
            If clsCommon.myLen(txtFromLoc.Value) > 0 Then
                Dim F_Code As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_LOCATION_MASTER Where Location_Code  ='" + clsCommon.myCstr(txtFromLoc.Value) + "'"))
                If F_Code = 0 Then
                    Me.RadPageView1.SelectedPage = RadPageViewPage5
                    Throw New Exception("Please check ! from location code does not exist")
                End If
            End If
            If clsCommon.myLen(txtToLoc.Value) > 0 Then
                Dim T_Code As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_LOCATION_MASTER Where Location_Code  ='" + clsCommon.myCstr(txtToLoc.Value) + "'"))
                If T_Code = 0 Then
                    Me.RadPageView1.SelectedPage = RadPageViewPage5
                    Throw New Exception("Please check ! to location code does not exist")
                End If
            End If
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage5
                txtLocation.Focus()
                Throw New Exception("Location code can not be left blank")
            End If
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                Dim Location_Code As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_LOCATION_MASTER Where Location_Code  ='" + clsCommon.myCstr(txtLocation.Value) + "'"))
                If Location_Code = 0 Then
                    Me.RadPageView1.SelectedPage = RadPageViewPage5
                    Throw New Exception("Please check ! location code does not exist")
                End If
            End If
            If clsCommon.myLen(txtPreferedLoc.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage5
                txtPreferedLoc.Focus()
                Throw New Exception("Preferred location can not be left blank")
            End If
            If clsCommon.myLen(txtPreferedLoc.Value) > 0 Then
                Dim PLocation_Code As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_LOCATION_MASTER Where Location_Code  ='" + clsCommon.myCstr(txtPreferedLoc.Value) + "'"))
                If PLocation_Code = 0 Then
                    Me.RadPageView1.SelectedPage = RadPageViewPage5
                    Throw New Exception("Please check ! preferred location code does not exist")
                End If
            End If
            ''
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Private Function GetCourseType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Name") = FullType
        dr("Code") = "F"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Name") = PartType
        dr("Code") = "P"
        dt.Rows.Add(dr)


        Return dt
    End Function
    Sub LoadQualificationGrid()
        gvQualification.Rows.Clear()
        gvQualification.Columns.Clear()

        Dim repoUnvClg As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnvClg = New GridViewTextBoxColumn()
        repoUnvClg.FormatString = ""
        repoUnvClg.HeaderText = "Unv./College"
        repoUnvClg.Name = ColUnvClg
        repoUnvClg.Width = 350
        'repoUnvClg.ReadOnly = True
        repoUnvClg.MaxLength = 200
        repoUnvClg.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvQualification.MasterTemplate.Columns.Add(repoUnvClg) '0

        Dim repoCourseCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCourseCode.FormatString = ""
        repoCourseCode.HeaderText = "Course Code"
        repoCourseCode.Name = ColCourse
        repoCourseCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoCourseCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCourseCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoCourseCode.Width = 100
        repoCourseCode.MaxLength = 50
        gvQualification.MasterTemplate.Columns.Add(repoCourseCode) '1

        Dim repoCourseName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCourseName.FormatString = ""
        repoCourseName.HeaderText = "Course Name"
        repoCourseName.Name = ColCourseName
        repoCourseName.ReadOnly = True
        ' repoCourseName.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        ' repoCourseName.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCourseName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoCourseName.Width = 100
        gvQualification.MasterTemplate.Columns.Add(repoCourseName) '2

        Dim repoYrOfPass As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoYrOfPass.Format = DateTimePickerFormat.Custom
        repoYrOfPass.FormatString = "{0:MMM/yyyy}"
        repoYrOfPass.CustomFormat = "MMM/yyyy"
        repoYrOfPass.HeaderText = "Year of Passing"
        repoYrOfPass.Name = ColYrOfPassing
        'repoYrOfPass.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoYrOfPass.TextImageRelation = TextImageRelation.TextBeforeImage
        repoYrOfPass.Width = 100
        gvQualification.MasterTemplate.Columns.Add(repoYrOfPass) '3

        Dim repoGrade As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGrade.FormatString = ""
        repoGrade.HeaderText = "Grade(%tage)"
        repoGrade.Name = ColGradePercentage
        repoGrade.MaxLength = 10
        'repoGrade.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        ' repoGrade.TextImageRelation = TextImageRelation.TextBeforeImage
        repoGrade.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoGrade.Width = 100
        gvQualification.MasterTemplate.Columns.Add(repoGrade) '4

        Dim repoCourseType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoCourseType.FormatString = ""
        repoCourseType.HeaderText = "Course Type"
        repoCourseType.Name = ColCourseType
        repoCourseType.Width = 100
        'repoCourseType.ReadOnly = True
        repoCourseType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoCourseType.DataSource = GetCourseType()
        repoCourseType.ValueMember = "Code"
        repoCourseType.DisplayMember = "Name"
        gvQualification.MasterTemplate.Columns.Add(repoCourseType) '5



        Dim repoHighQua As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoHighQua.HeaderText = "Higher Qualification"
        repoHighQua.Name = ColHighQual
        repoHighQua.Width = 200
        'repoHighQua.ReadOnly = True
        repoHighQua.IsVisible = True
        repoHighQua.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvQualification.MasterTemplate.Columns.Add(repoHighQua) '6




        clsCustomFieldGrid.LoadBlankGrid(gvQualification, MyBase.ArrDetailFields)

        gvQualification.AllowDeleteRow = True
        gvQualification.AllowAddNewRow = False
        gvQualification.ShowGroupPanel = False
        gvQualification.AllowColumnReorder = False
        gvQualification.AllowRowReorder = False
        gvQualification.EnableSorting = False
        gvQualification.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvQualification.MasterTemplate.ShowRowHeaderColumn = False
        gvQualification.TableElement.TableHeaderHeight = 40
        'ReStoreGridLayout()


    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvQualification.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvQualification.Columns.Count - 1 Step ii + 1
                        gvQualification.Columns(ii).IsVisible = False
                        gvQualification.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvQualification.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub LoadCheckListGrid()
        gvDoc.DataSource = Nothing
        gvDoc.Rows.Clear()
        gvDoc.Columns.Clear()

        Dim repoChkCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoChkCode = New GridViewTextBoxColumn()
        repoChkCode.FormatString = ""
        repoChkCode.HeaderText = "Check List Code"
        repoChkCode.Name = ColCheckListCode
        repoChkCode.Width = 150
        repoChkCode.MaxLength = 30
        repoChkCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoChkCode.TextImageRelation = TextImageRelation.TextBeforeImage
        'repoChkCode.ReadOnly = True
        repoChkCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvDoc.MasterTemplate.Columns.Add(repoChkCode) '0

        Dim repoChkName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoChkName.FormatString = ""
        repoChkName.HeaderText = "Check List Name"
        repoChkName.Name = ColCheckListName
        repoChkName.ReadOnly = True
        repoChkName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoChkName.Width = 150
        gvDoc.MasterTemplate.Columns.Add(repoChkName) '1

        Dim repoMan As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoMan.HeaderText = "Manadatory"
        repoMan.Name = ColIsManadatory
        'repoMan.ReadOnly = True
        'repoMan.IsVisible = True
        repoMan.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvDoc.MasterTemplate.Columns.Add(repoMan) '3

        Dim repoRcvd As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoRcvd.HeaderText = "Received"
        repoRcvd.Name = ColIsReceived
        repoRcvd.ReadOnly = False
        'repoRcvd.IsVisible = True
        repoRcvd.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvDoc.MasterTemplate.Columns.Add(repoRcvd) '3


        clsCustomFieldGrid.LoadBlankGrid(gvDoc, MyBase.ArrDetailFields)

        gvDoc.AllowDeleteRow = True
        gvDoc.AllowAddNewRow = False
        gvDoc.ShowGroupPanel = False
        gvDoc.AllowColumnReorder = False
        gvDoc.AllowRowReorder = False
        gvDoc.EnableSorting = False
        gvDoc.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvDoc.MasterTemplate.ShowRowHeaderColumn = False
        gvDoc.TableElement.TableHeaderHeight = 40
        'ReStoreGridLayout()


    End Sub
    Sub LoadEmpGrid()
        gvEmpHis.Rows.Clear()
        gvEmpHis.Columns.Clear()

        Dim repoOrg As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOrg = New GridViewTextBoxColumn()
        repoOrg.FormatString = ""
        repoOrg.HeaderText = "Organisation Name"
        repoOrg.Name = ColOrgName
        repoOrg.Width = 180
        repoOrg.MaxLength = 200
        'repoOrg.ReadOnly = True
        repoOrg.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpHis.MasterTemplate.Columns.Add(repoOrg) '0

        Dim repoFromPeriod As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoFromPeriod.Format = DateTimePickerFormat.Custom
        repoFromPeriod.FormatString = "{0:dd/MMM/yyyy}"
        repoFromPeriod.CustomFormat = "dd/MMM/yyyy"
        repoFromPeriod.HeaderText = "From Period"
        repoFromPeriod.Name = ColFromPeriod
        'repoFromPeriod.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoFromPeriod.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFromPeriod.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoFromPeriod.Width = 100
        gvEmpHis.MasterTemplate.Columns.Add(repoFromPeriod) '1

        Dim repoToPeriod As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoToPeriod.Format = DateTimePickerFormat.Custom
        repoToPeriod.FormatString = "{0:dd/MMM/yyyy}"
        repoToPeriod.CustomFormat = "dd/MMM/yyyy"
        repoToPeriod.HeaderText = "To Period"
        repoToPeriod.Name = ColToPeriod
        'repoToPeriod.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoToPeriod.TextImageRelation = TextImageRelation.TextBeforeImage
        repoToPeriod.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoToPeriod.Width = 100
        gvEmpHis.MasterTemplate.Columns.Add(repoToPeriod) '2

        Dim repoTillDate As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTillDate.HeaderText = "Till Date"
        repoTillDate.Name = ColTillDate
        'repoTillDate.ReadOnly = True
        repoTillDate.IsVisible = True
        repoTillDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmpHis.MasterTemplate.Columns.Add(repoTillDate) '3

        Dim repoDesig As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDesig.FormatString = ""
        repoDesig.HeaderText = "Designation"
        repoDesig.Name = ColDesignation
        repoDesig.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoDesig.TextImageRelation = TextImageRelation.TextBeforeImage
        repoDesig.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoDesig.Width = 100
        repoDesig.MaxLength = 12
        gvEmpHis.MasterTemplate.Columns.Add(repoDesig) '3

        Dim repoDesigName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDesigName.FormatString = ""
        repoDesigName.HeaderText = "Designation Name"
        repoDesigName.Name = ColDesignationName
        repoDesigName.ReadOnly = True
        'repoDesigName.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoDesigName.TextImageRelation = TextImageRelation.TextBeforeImage
        repoDesigName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoDesigName.Width = 150
        gvEmpHis.MasterTemplate.Columns.Add(repoDesigName) '4

        Dim repoRoles As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRoles.FormatString = ""
        repoRoles.HeaderText = "Roles And Responsibilities"
        repoRoles.Name = ColRolesRes
        'repoRoles.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoRoles.TextImageRelation = TextImageRelation.TextBeforeImage
        repoRoles.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRoles.Width = 150
        repoRoles.MaxLength = 200
        gvEmpHis.MasterTemplate.Columns.Add(repoRoles) '5

        Dim repoResSep As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoResSep.FormatString = ""
        repoResSep.HeaderText = "Reason for Seperation"
        repoResSep.Name = ColReasSep
        'repoResSep.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoResSep.TextImageRelation = TextImageRelation.TextBeforeImage
        repoResSep.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoResSep.Width = 200
        repoResSep.MaxLength = 250
        gvEmpHis.MasterTemplate.Columns.Add(repoResSep) '6

        clsCustomFieldGrid.LoadBlankGrid(gvEmpHis, MyBase.ArrDetailFields)

        gvEmpHis.AllowDeleteRow = True
        gvEmpHis.AllowAddNewRow = False
        gvEmpHis.ShowGroupPanel = False
        gvEmpHis.AllowColumnReorder = False
        gvEmpHis.AllowRowReorder = False
        gvEmpHis.EnableSorting = False
        gvEmpHis.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvEmpHis.MasterTemplate.ShowRowHeaderColumn = False
        gvEmpHis.TableElement.TableHeaderHeight = 40
        'ReStoreGridLayout()


    End Sub

    'Sub LoadFamilyGrid()
    '    gvFamily.Rows.Clear()
    '    gvFamily.Columns.Clear()

    '    Dim repoRel As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoRel = New GridViewTextBoxColumn()
    '    repoRel.FormatString = ""
    '    repoRel.HeaderText = "Relation"
    '    repoRel.Name = ColRelation
    '    repoRel.Width = 100
    '    repoRel.MaxLength = 30
    '    'repoOrg.ReadOnly = True
    '    repoRel.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
    '    repoRel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvFamily.MasterTemplate.Columns.Add(repoRel) '0

    '    Dim repoRelN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoRelN.FormatString = ""
    '    repoRelN.HeaderText = "Relation Name"
    '    repoRelN.Name = ColRelationName
    '    repoRelN.TextImageRelation = TextImageRelation.TextBeforeImage
    '    repoRelN.Width = 100
    '    repoRelN.ReadOnly = True
    '    gvFamily.MasterTemplate.Columns.Add(repoRelN) '1

    '    Dim repoqu As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoqu.FormatString = ""
    '    repoqu.HeaderText = "Qualification"
    '    repoqu.Name = ColQualification
    '    repoqu.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
    '    repoqu.TextImageRelation = TextImageRelation.TextBeforeImage
    '    repoqu.Width = 100
    '    repoqu.MaxLength = 30
    '    gvEmpHis.MasterTemplate.Columns.Add(repoqu) '2

    '    Dim repoQN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoQN.HeaderText = "Qualification Name"
    '    repoQN.Name = ColQualificationName
    '    repoQN.ReadOnly = True
    '    'repoQN.IsVisible = True
    '    repoQN.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
    '    gvFamily.MasterTemplate.Columns.Add(repoQN) '3

    '    Dim repoOcc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoOcc.FormatString = ""
    '    repoOcc.HeaderText = "Occupation"
    '    repoOcc.Name = ColOccupation
    '    repoOcc.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
    '    repoOcc.TextImageRelation = TextImageRelation.TextBeforeImage
    '    repoOcc.Width = 100
    '    repoOcc.MaxLength = 30
    '    gvFamily.MasterTemplate.Columns.Add(repoOcc) '3

    '    Dim repoON As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoON.FormatString = ""
    '    repoON.HeaderText = "Occupation Name"
    '    repoON.Name = ColOccupationName
    '    repoON.ReadOnly = True
    '    'repoON.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
    '    repoON.TextImageRelation = TextImageRelation.TextBeforeImage
    '    repoON.Width = 150
    '    gvFamily.MasterTemplate.Columns.Add(repoON) '4

    '    Dim repoDOB As GridViewDateTimeColumn = New GridViewDateTimeColumn()
    '    repoDOB.FormatString = ""
    '    repoDOB.HeaderText = "Date Of Birth"
    '    repoDOB.Name = ColDateofBirth
    '    'repoDOB.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
    '    repoDOB.TextImageRelation = TextImageRelation.TextBeforeImage
    '    repoDOB.Width = 150
    '    'repoDOB.MaxLength = 200
    '    gvFamily.MasterTemplate.Columns.Add(repoDOB) '5

    '    clsCustomFieldGrid.LoadBlankGrid(gvFamily, MyBase.ArrDetailFields)

    '    gvFamily.AllowDeleteRow = True
    '    gvFamily.AllowAddNewRow = False
    '    gvFamily.ShowGroupPanel = False
    '    gvFamily.AllowColumnReorder = False
    '    gvFamily.AllowRowReorder = False
    '    gvFamily.EnableSorting = False
    '    gvFamily.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
    '    gvFamily.MasterTemplate.ShowRowHeaderColumn = False
    '    gvFamily.TableElement.TableHeaderHeight = 40
    '    'ReStoreGridLayout()


    'End Sub

    Sub LoadFamilyGrid()
        gvFamily.Rows.Clear()
        gvFamily.Columns.Clear()

        Dim repoRelation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRelation = New GridViewTextBoxColumn()
        repoRelation.FormatString = ""
        repoRelation.HeaderText = "Relation"
        repoRelation.Name = ColRelation
        repoRelation.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoRelation.TextImageRelation = TextImageRelation.TextBeforeImage
        repoRelation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRelation.Width = 120
        repoRelation.ReadOnly = False
        gvFamily.MasterTemplate.Columns.Add(repoRelation) '0

        Dim repoRelationN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRelationN = New GridViewTextBoxColumn()
        repoRelationN.FormatString = ""
        repoRelationN.HeaderText = "Relation Name"
        repoRelationN.Name = ColRelationName
        repoRelationN.Width = 200
        repoRelationN.ReadOnly = True
        'repoRelationN.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoRelationN.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvFamily.MasterTemplate.Columns.Add(repoRelationN) '0

        Dim repoQualification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQualification.FormatString = ""
        repoQualification.HeaderText = "Qualification"
        repoQualification.Name = ColQualification
        repoQualification.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoQualification.TextImageRelation = TextImageRelation.TextBeforeImage
        repoQualification.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoQualification.Width = 120
        gvFamily.MasterTemplate.Columns.Add(repoQualification) '1

        Dim repoQualificationName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQualificationName.FormatString = ""
        repoQualificationName.HeaderText = "Qualification Name"
        repoQualificationName.Name = ColQualificationName
        repoQualificationName.ReadOnly = True
        'repoQualificationName.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoQualificationName.TextImageRelation = TextImageRelation.TextBeforeImage
        repoQualificationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoQualificationName.Width = 200
        gvFamily.MasterTemplate.Columns.Add(repoQualificationName) '1

        Dim repoOcc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOcc = New GridViewTextBoxColumn()
        repoOcc.FormatString = ""
        repoOcc.HeaderText = "Occupation"
        repoOcc.Name = ColOccupation
        repoOcc.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoOcc.TextImageRelation = TextImageRelation.TextBeforeImage
        repoOcc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoOcc.Width = 120
        repoOcc.ReadOnly = False
        gvFamily.MasterTemplate.Columns.Add(repoOcc) '3

        Dim repoOccN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOccN = New GridViewTextBoxColumn()
        repoOccN.FormatString = ""
        repoOccN.HeaderText = "Occupation Name"
        repoOccN.Name = ColOccupationName
        repoOccN.Width = 200
        repoOccN.ReadOnly = True
        repoOccN.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvFamily.MasterTemplate.Columns.Add(repoOccN) '3

        Dim repoDOB As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDOB.Format = DateTimePickerFormat.Custom
        repoDOB.FormatString = "{0:dd/MMM/yyyy}"
        repoDOB.CustomFormat = "dd/MMM/yyyy"
        repoDOB.HeaderText = "Date of Birth"
        repoDOB.Name = ColDateofBirth
        ' repoDOB.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoDOB.TextImageRelation = TextImageRelation.TextBeforeImage
        repoDOB.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoDOB.Width = 85
        gvFamily.MasterTemplate.Columns.Add(repoDOB) '4

        clsCustomFieldGrid.LoadBlankGrid(gvFamily, MyBase.ArrDetailFields)

        gvFamily.AllowDeleteRow = True
        gvFamily.AllowAddNewRow = False
        gvFamily.ShowGroupPanel = False
        gvFamily.AllowColumnReorder = False
        gvFamily.AllowRowReorder = False
        gvFamily.EnableSorting = False
        gvFamily.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvFamily.MasterTemplate.ShowRowHeaderColumn = False
        gvFamily.TableElement.TableHeaderHeight = 40
        'ReStoreGridLayout()


    End Sub
    Sub OpenCourseCodeList(ByVal isButtonClick As Boolean)
        gvQualification.CurrentRow.Cells(ColCourseName).Value = ""
        Dim qry As String = "select COURSE_CODE as Code,COURSE_NAME as COURSE_NAME from TSPL_COURSE_MASTER"
        gvQualification.CurrentRow.Cells(ColCourse).Value = clsCommon.ShowSelectForm("FndCrs", qry, "Code", "", clsCommon.myCstr(gvQualification.CurrentRow.Cells(ColCourse).Value), "Code", isButtonClick)
        If clsCommon.myLen(gvQualification.CurrentRow.Cells(ColCourse).Value) > 0 Then
            qry = "select COURSE_CODE,COURSE_NAME from TSPL_COURSE_MASTER  WHERE COURSE_CODE ='" + clsCommon.myCstr(gvQualification.CurrentRow.Cells(ColCourse).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvQualification.CurrentRow.Cells(ColCourse).Value = clsCommon.myCstr(dt.Rows(0)("COURSE_CODE"))
                gvQualification.CurrentRow.Cells(ColCourseName).Value = clsCommon.myCstr(dt.Rows(0)("COURSE_NAME"))
            End If
        End If
    End Sub
    Sub OpenDesgCodeList(ByVal isButtonClick As Boolean)
        gvEmpHis.CurrentRow.Cells(ColDesignationName).Value = ""
        Dim qry As String = "select DESIGNATION_ID as Code,Designation_Desc as Designation_Desc from TSPL_DESIGNATION_MASTER"
        gvEmpHis.CurrentRow.Cells(ColDesignation).Value = clsCommon.ShowSelectForm("FndDes", qry, "Code", "", clsCommon.myCstr(gvEmpHis.CurrentRow.Cells(ColDesignation).Value), "Code", isButtonClick)
        If clsCommon.myLen(gvEmpHis.CurrentRow.Cells(ColDesignation).Value) > 0 Then
            qry = "select DESIGNATION_ID,Designation_Desc from TSPL_DESIGNATION_MASTER  WHERE DESIGNATION_ID ='" + clsCommon.myCstr(gvEmpHis.CurrentRow.Cells(ColDesignation).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvEmpHis.CurrentRow.Cells(ColDesignation).Value = clsCommon.myCstr(dt.Rows(0)("DESIGNATION_ID"))
                gvEmpHis.CurrentRow.Cells(ColDesignationName).Value = clsCommon.myCstr(dt.Rows(0)("Designation_Desc"))
            End If
        End If
    End Sub
    Sub OpenCheckCodeList(ByVal isButtonClick As Boolean)
        gvDoc.CurrentRow.Cells(ColCheckListName).Value = ""
        Dim qry As String = "select Chk_Code as Code,Chk_Description as Chk_Description from TSPL_HR_Check_List"
        gvDoc.CurrentRow.Cells(ColCheckListCode).Value = clsCommon.ShowSelectForm("FndChk", qry, "Code", "", clsCommon.myCstr(gvDoc.CurrentRow.Cells(ColCheckListCode).Value), "Code", isButtonClick)
        If clsCommon.myLen(gvDoc.CurrentRow.Cells(ColCheckListCode).Value) > 0 Then
            qry = "select Chk_Code,Chk_Description from TSPL_HR_Check_List  WHERE Chk_Code ='" + clsCommon.myCstr(gvDoc.CurrentRow.Cells(ColCheckListCode).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvDoc.CurrentRow.Cells(ColCheckListCode).Value = clsCommon.myCstr(dt.Rows(0)("Chk_Code"))
                gvDoc.CurrentRow.Cells(ColCheckListName).Value = clsCommon.myCstr(dt.Rows(0)("Chk_Description"))
            End If
        End If
    End Sub
    Sub OpenOccCodeList(ByVal isButtonClick As Boolean)
        gvFamily.CurrentRow.Cells(ColOccupationName).Value = ""
        Dim qry As String = "select OCCUPATION_CODE as Code,OCCUPATION_NAME as [Occupation Name] from TSPL_OCCUPATION_MASTER"
        gvFamily.CurrentRow.Cells(ColOccupation).Value = clsCommon.ShowSelectForm("FndOcc", qry, "Code", "", clsCommon.myCstr(gvFamily.CurrentRow.Cells(ColOccupation).Value), "Code", isButtonClick)
        If clsCommon.myLen(gvFamily.CurrentRow.Cells(ColOccupation).Value) > 0 Then
            qry = "select OCCUPATION_CODE,OCCUPATION_NAME from TSPL_OCCUPATION_MASTER  WHERE OCCUPATION_CODE ='" + clsCommon.myCstr(gvFamily.CurrentRow.Cells(ColOccupation).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvFamily.CurrentRow.Cells(ColOccupation).Value = clsCommon.myCstr(dt.Rows(0)("OCCUPATION_CODE"))
                gvFamily.CurrentRow.Cells(ColOccupationName).Value = clsCommon.myCstr(dt.Rows(0)("OCCUPATION_NAME"))
            End If
        End If
    End Sub
    Sub OpenQualCodeList(ByVal isButtonClick As Boolean)
        gvFamily.CurrentRow.Cells(ColQualificationName).Value = ""
        Dim qry As String = "select Qualification_Code as Code,Qualification_Name as [Qualification Name] from TSPL_HR_QUALIFICATION_MASTER"
        gvFamily.CurrentRow.Cells(ColQualification).Value = clsCommon.ShowSelectForm("FndQual", qry, "Code", "", clsCommon.myCstr(gvFamily.CurrentRow.Cells(ColQualification).Value), "Code", isButtonClick)
        If clsCommon.myLen(gvFamily.CurrentRow.Cells(ColQualification).Value) > 0 Then
            qry = "select Qualification_Code,Qualification_Name from TSPL_HR_QUALIFICATION_MASTER  WHERE Qualification_Code ='" + clsCommon.myCstr(gvFamily.CurrentRow.Cells(ColQualification).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvFamily.CurrentRow.Cells(ColQualification).Value = clsCommon.myCstr(dt.Rows(0)("Qualification_Code"))
                gvFamily.CurrentRow.Cells(ColQualificationName).Value = clsCommon.myCstr(dt.Rows(0)("Qualification_Name"))
            End If
        End If
    End Sub
    Sub OpenRelCodeList(ByVal isButtonClick As Boolean)
        gvFamily.CurrentRow.Cells(ColRelationName).Value = ""
        Dim qry As String = "select Relation_Code as Code,Relation_Name as [Relation Name] from TSPL_HR_RELATION_MASTER"
        gvFamily.CurrentRow.Cells(ColRelation).Value = clsCommon.ShowSelectForm("FndQual", qry, "Code", "", clsCommon.myCstr(gvFamily.CurrentRow.Cells(ColRelation).Value), "Code", isButtonClick)
        If clsCommon.myLen(gvFamily.CurrentRow.Cells(ColRelation).Value) > 0 Then
            qry = "select Relation_Code,Relation_Name from TSPL_HR_RELATION_MASTER  WHERE Relation_Code ='" + clsCommon.myCstr(gvFamily.CurrentRow.Cells(ColRelation).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvFamily.CurrentRow.Cells(ColRelation).Value = clsCommon.myCstr(dt.Rows(0)("Relation_Code"))
                gvFamily.CurrentRow.Cells(ColRelationName).Value = clsCommon.myCstr(dt.Rows(0)("Relation_Name"))
            End If
        End If
    End Sub
    ' ----------------- Get_Salutation ------------------------
    Public Sub GetSal()
        Dim DT_Sal As DataTable = New DataTable
        DT_Sal.Columns.Add("Code", GetType(String))
        DT_Sal.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Sal.NewRow()
        DR("Name") = "Dr."
        DR("Code") = "Dr"
        DT_Sal.Rows.Add(DR)
        DT_Sal.AcceptChanges()

        If SalAccToM = True Then
            DR = DT_Sal.NewRow()
            DR("Name") = "Mr."
            DR("Code") = "Mr"
            DT_Sal.Rows.Add(DR)
        End If

        If SalAccToF = True Then
            DR = DT_Sal.NewRow()
            DR("Name") = "Ms."
            DR("Code") = "Ms"
            DT_Sal.Rows.Add(DR)

            DR = DT_Sal.NewRow()
            DR("Name") = "Mrs."
            DR("Code") = "Mrs"
            DT_Sal.Rows.Add(DR)
        End If

        cmbSalutation.DataSource = DT_Sal
        cmbSalutation.DisplayMember = "Name"
        cmbSalutation.ValueMember = "Code"
    End Sub
    Sub funReset()
        isNewEntry = True
        txtcode.MyReadOnly = False
        txtcode.Value = Nothing
        txtcode.Focus()
        Me.RadPageView1.SelectedPage = RadPageViewPage1
        '' Personal Detail
        dtpDate.Value = clsCommon.GETSERVERDATE()
        dtpDateofIntr.Value = clsCommon.GETSERVERDATE()
        txtrequisitioncode.Value = ""
        lblrequisition.Text = ""
        txtsourcetype.Value = ""
        lblsourcetype.Text = ""
        txtsourcedetail.Value = ""
        lblsourcedetail.Text = ""
        txtdesp.Text = ""
        Me.cmbGender.DataSource = ClsApplicantEntry.GetGender
        Me.cmbGender.DisplayMember = "Name"
        Me.cmbGender.ValueMember = "Code"
        'Me.cmbSalutation.DataSource = GetSal()
        'Me.cmbSalutation.DisplayMember = "Name"
        'Me.cmbSalutation.ValueMember = "Code"
        SalAccToM = True
        GetSal()
        Me.CmbMarStatus.DataSource = ClsApplicantEntry.GetMS
        Me.CmbMarStatus.DisplayMember = "Name"
        Me.CmbMarStatus.ValueMember = "Code"
        dtpDateofBirth.Value = clsCommon.GETSERVERDATE()
        UsLock1.Status = ERPTransactionStatus.Pending
        ComboLoad = True
        txtFirstName.Text = ""
        txtMiddleName.Text = ""
        txtLastName.Text = ""
        txtPanNo.Text = ""
        PicImage.Image = Nothing
        txtAdd1.Text = ""
        txtAdd2.Text = ""
        txtadd3.Text = ""
        txtadd4.Text = ""
        txtEmail.Text = ""
        txtPinCode.Text = ""
        txtCountry.Value = ""
        lblCountry.Text = ""
        txtState.Value = ""
        lblState.Text = ""
        txtCity.Value = ""
        lblCity.Text = ""
        File_Name = ""
        txtTelephone.Text = "(+__)__________"
        '' EmpHis
        ChkFresher.Checked = False
        '' Add Info
        rbnrefbyAge.IsChecked = False
        rbnRefbyEmp.IsChecked = False
        txtRelation.Enabled = False
        txtEmpCode.Enabled = False
        txtAgency.Enabled = False
        txtEmpCode.MendatroryField = False
        txtRelation.MendatroryField = False
        txtAgency.MendatroryField = False
        txtAgency.Value = ""
        LblAgency.Text = ""
        txtEmpCode.Value = ""
        lblEmpName.Text = ""
        txtRelation.Value = ""
        lblRelation.Text = ""
        '' Family
        ChkHandicaped.Checked = False
        txtHandiDetail.Enabled = False
        ChkRelocation.Checked = False
        grpReLoc.Enabled = False
        txtFromLoc.MendatroryField = False
        txtToLoc.MendatroryField = False
        txtHandiDetail.MendatroryField = False

        txtHandiDetail.Text = ""
        txtBloodGrp.Text = ""
        txtBankCode.Value = ""
        LblBankName.Text = ""
        txtBranchCode.Value = ""
        lblBranchName.Text = ""
        txtAccNo.Text = ""
        txtCurrGrossSal.Text = ""
        txtTotalCTC.Text = ""
        txtLocation.Value = ""
        LblLocation.Text = ""
        txtFromLoc.Value = ""
        LblFromLoc.Text = ""
        txtToLoc.Value = ""
        LblToLoc.Text = ""
        txtPerBy.Text = ""
        txtPreferedLoc.Value = ""
        LblPreLoc.Text = ""
        ShortListed = 0
        Rejected = 0
        '' Blank Grid
        Me.gvQualification.Rows.Clear()
        Me.gvQualification.Rows.AddNew()
        Me.gvQualification.CurrentRow.Cells(ColCourseType).Value = "F"
        Me.gvDoc.DataSource = Nothing
        Me.gvDoc.Rows.Clear()
        Me.gvDoc.Rows.AddNew()
        Me.gvEmpHis.Rows.Clear()
        Me.gvEmpHis.Rows.AddNew()
        Me.gvFamily.Rows.Clear()
        Me.gvFamily.Rows.AddNew()
        'gvQualification.Rows(gvQualification.Rows.Count - 1).Cells(ColCourseType).Value = FullType
        'isInsideLoadData = True
        txtResume.Text = ""
        OpenFileDialog1.Reset()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnShowDoc.Enabled = False
        BtnDeleteDoc.Enabled = False

    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "code not found to delete", Me.Text)
            Exit Sub
        End If

        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsApplicantEntry.DeleteData(txtcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Sub LoadFromAppCheckList(ByVal AppCode As String)
        Dim JobTitleCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Job_Title_Code,'') AS Job_Title_Code From TSPL_HR_REQUISITION  Where Requisition_Code ='" & clsCommon.myCstr(txtrequisitioncode.Value) & "'"))
        'Dim qry As String = "Select TSPL_HR_Offer_ChkList_JOB_TITLE.Chk_Code As [Check Code],TSPL_HR_Check_List.Chk_Description AS [Check Name],CAST(mandatory as bit) AS Manadatory,CAST(0 as bit) as Received From TSPL_HR_Offer_ChkList_JOB_TITLE LEFT OUTER JOIN TSPL_HR_Check_List ON TSPL_HR_Check_List.Chk_Code =TSPL_HR_Offer_ChkList_JOB_TITLE.Chk_Code  where TSPL_HR_Offer_ChkList_JOB_TITLE.Job_Title_Code ='" & JobTitleCode & "'"
        Dim qry As String = "Select TSPL_HR_CHECK_APPLICANT_ENTRY.Chk_Code As [Check Code],TSPL_HR_Check_List.Chk_Description  AS [Check Name],CAST(isnull(TSPL_HR_CHECK_APPLICANT_ENTRY.Is_Manadatory  ,0)  as bit) as Manadatory From TSPL_HR_CHECK_APPLICANT_ENTRY LEFT OUTER JOIN TSPL_HR_Check_List ON TSPL_HR_Check_List.Chk_Code =TSPL_HR_CHECK_APPLICANT_ENTRY.Chk_Code Where TSPL_HR_CHECK_APPLICANT_ENTRY.Applicant_Code ='" + AppCode + "'"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        gvDoc.DataSource = Nothing

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            'dt.Columns.Add("Status", GetType(Boolean))
            gvDoc.DataSource = dt
            gvDoc.Columns("Check Code").Width = 100
            'gvDoc.Columns("Check Code").ReadOnly = True
            gvDoc.Columns("Check Name").Width = 180
            gvDoc.Columns("Check Name").ReadOnly = True
            gvDoc.Columns("Manadatory").Width = 100
            'gvDoc.Columns("Manadatory").ReadOnly = True
            'gvDoc.Columns("Received").Width = 100
            'gvDoc.Columns("Received").ReadOnly = False

            gvDoc.AllowDeleteRow = False
            gvDoc.AllowAddNewRow = False
            gvDoc.ShowGroupPanel = False
            gvDoc.AllowColumnReorder = False
            gvDoc.AllowRowReorder = False
            gvDoc.EnableSorting = False
            gvDoc.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gvDoc.MasterTemplate.ShowRowHeaderColumn = False
            gvDoc.TableElement.TableHeaderHeight = 40
        Else
            Me.gvDoc.Rows.Clear()
            Me.gvDoc.Rows.AddNew()
        End If
    End Sub
    Sub LoadJobTitleCheckList()
        Dim JobTitleCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Job_Title_Code,'') AS Job_Title_Code From TSPL_HR_REQUISITION  Where Requisition_Code ='" & clsCommon.myCstr(txtrequisitioncode.Value) & "'"))
        'Dim qry As String = "Select TSPL_HR_Offer_ChkList_JOB_TITLE.Chk_Code As [Check Code],TSPL_HR_Check_List.Chk_Description AS [Check Name],CAST(mandatory as bit) AS Manadatory,CAST(0 as bit) as Received From TSPL_HR_Offer_ChkList_JOB_TITLE LEFT OUTER JOIN TSPL_HR_Check_List ON TSPL_HR_Check_List.Chk_Code =TSPL_HR_Offer_ChkList_JOB_TITLE.Chk_Code  where TSPL_HR_Offer_ChkList_JOB_TITLE.Job_Title_Code ='" & JobTitleCode & "'"
        Dim qry As String = "Select TSPL_HR_Offer_ChkList_JOB_TITLE.Chk_Code As [Check Code],TSPL_HR_Check_List.Chk_Description AS [Check Name],CAST(mandatory as bit) AS Manadatory From TSPL_HR_Offer_ChkList_JOB_TITLE LEFT OUTER JOIN TSPL_HR_Check_List ON TSPL_HR_Check_List.Chk_Code =TSPL_HR_Offer_ChkList_JOB_TITLE.Chk_Code  where TSPL_HR_Offer_ChkList_JOB_TITLE.Job_Title_Code='" + JobTitleCode + "'"
        'If clsCommon.myLen(AppCode) > 0 Then
        '    qry += "  AND TSPL_HR_CHECK_APPLICANT_ENTRY.Applicant_Code ='" + AppCode + "'"
        'End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        gvDoc.DataSource = Nothing

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            'dt.Columns.Add("Status", GetType(Boolean))
            gvDoc.DataSource = dt
            gvDoc.Columns("Check Code").Width = 100
            gvDoc.Columns("Check Code").ReadOnly = True
            gvDoc.Columns("Check Name").Width = 180
            gvDoc.Columns("Check Name").ReadOnly = True
            gvDoc.Columns("Manadatory").Width = 100
            gvDoc.Columns("Manadatory").ReadOnly = True
            'gvDoc.Columns("Received").Width = 100
            'gvDoc.Columns("Received").ReadOnly = False

            gvDoc.AllowDeleteRow = False
            gvDoc.AllowAddNewRow = False
            gvDoc.ShowGroupPanel = False
            gvDoc.AllowColumnReorder = False
            gvDoc.AllowRowReorder = False
            gvDoc.EnableSorting = False
            gvDoc.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gvDoc.MasterTemplate.ShowRowHeaderColumn = False
            gvDoc.TableElement.TableHeaderHeight = 40
        Else
            Me.gvDoc.Rows.Clear()
            Me.gvDoc.Rows.AddNew()
        End If
    End Sub
    Private Sub SaveImage(ByVal AppCode As String)
        Try
            Dim qry As String = ""
            'If clsCommon.myLen(txtcode.Value) > 0 Then
            If PicImage.Image IsNot Nothing Then
                Dim ms As New MemoryStream()
                PicImage.Image.Save(ms, PicImage.Image.RawFormat)
                Dim data As Byte() = ms.GetBuffer()
                clsDBFuncationality.UpdateImage("Applicant_Photo", data, "TSPL_HR_APPLICANT_ENTRY", "APPLICANT_CODE='" + AppCode + "'")
                ''richa agarwal regarding memory leakage
                ms.Close()
                ms.Dispose()
            Else
                clsDBFuncationality.ExecuteNonQuery("Update TSPL_HR_APPLICANT_ENTRY set Applicant_Photo=null where  APPLICANT_CODE='" + AppCode + "'")
            End If

            'End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
    End Sub
    Private Sub LoadImage()
        Try
            PicImage.Image = Nothing
            Dim qry As String = "select Applicant_Photo from TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE='" + txtcode.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If (clsCommon.myLen(dt.Rows(0)("Applicant_Photo")) > 0) Then
                    Dim data As Byte() = DirectCast(dt.Rows(0)("Applicant_Photo"), Byte())
                    Dim ms As New MemoryStream(data)
                    PicImage.Image = Image.FromStream(ms)
                    ''PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage

                    ''richa agarwal regarding memory leakage
                    ms.Close()
                    ms.Dispose()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Save()

        Try

            If AllowToSave() Then

                Dim arr As New List(Of ClsApplicantEntry)
                Dim obj As New ClsApplicantEntry()
                obj.APPLICANT_CODE = clsCommon.myCstr(txtcode.Value)
                obj.Applicant_Description = clsCommon.myCstr(txtdesp.Text)
                obj.Applicant_Date = dtpDate.Value
                obj.Requisition_Code = clsCommon.myCstr(txtrequisitioncode.Value)
                obj.Date_Of_Interview = dtpDateofIntr.Value
                obj.Gender = clsCommon.myCstr(cmbGender.SelectedValue)
                obj.Salutation = clsCommon.myCstr(cmbSalutation.SelectedValue)
                obj.First_Name = clsCommon.myCstr(txtFirstName.Text)
                obj.Middle_Name = clsCommon.myCstr(txtMiddleName.Text)
                obj.Last_Name = clsCommon.myCstr(txtLastName.Text)
                obj.Applicant_Date_Of_Birth = dtpDateofBirth.Value
                obj.Maritial_Status = clsCommon.myCstr(CmbMarStatus.SelectedValue)
                obj.Pan_No = txtPanNo.Text
                'obj.Applicant_Photo = Applicant_Photo
                obj.Add1 = clsCommon.myCstr(txtAdd1.Text)
                obj.Add2 = clsCommon.myCstr(txtAdd2.Text)
                obj.Add3 = clsCommon.myCstr(txtadd3.Text)
                obj.Add4 = clsCommon.myCstr(txtadd4.Text)
                obj.COUNTRY_CODE = clsCommon.myCstr(txtCountry.Value)
                obj.City_code = clsCommon.myCstr(txtCity.Value)
                obj.State_Code = clsCommon.myCstr(txtState.Value)
                obj.Pin_Code = clsCommon.myCstr(txtPinCode.Text)
                obj.Email = clsCommon.myCstr(txtEmail.Text)
                obj.TELEPHONE_NO = clsCommon.myCstr(txtTelephone.Text)
                obj.Relation_Code = clsCommon.myCstr(txtRelation.Value)
                obj.Source_Type_Code = clsCommon.myCstr(txtsourcetype.Value)
                obj.Source_Type_Detail_Code = clsCommon.myCstr(txtsourcedetail.Value)
                If rbnRefbyEmp.IsChecked = True Then
                    obj.Emp_Refrence = 1
                Else
                    obj.Emp_Refrence = 0
                End If
                If rbnrefbyAge.IsChecked = True Then
                    obj.Agency_Refrence = 1
                Else
                    obj.Agency_Refrence = 0
                End If
                If ChkHandicaped.Checked = True Then
                    obj.Is_Handicaped = 1
                Else
                    obj.Is_Handicaped = 0
                End If
                If ChkRelocation.Checked = True Then
                    obj.Relocation = 1

                Else
                    obj.Relocation = 0

                End If
                If ChkFresher.Checked = True Then
                    obj.Is_Fresher = 1
                Else
                    obj.Is_Fresher = 0
                End If


                'If cnt > 0 AndAlso issaved Then
                '    clsCommon.MyMessageBoxShow("Document Save Successfully.")
                'End If

                'obj.Emp_Refrence = Emp_Refrence
                'obj.Agency_Refrence = Agency_Refrence
                obj.Emp_Code = clsCommon.myCstr(txtEmpCode.Value)
                obj.Agency_Code = clsCommon.myCstr(txtAgency.Value)
                obj.Handicaped_Detail = clsCommon.myCstr(txtHandiDetail.Text)
                obj.Blood_Group = clsCommon.myCstr(txtBloodGrp.Text)
                obj.Bank_Code = clsCommon.myCstr(txtBankCode.Value)
                obj.Branch_Code = clsCommon.myCstr(txtBranchCode.Value)
                obj.Account_No = clsCommon.myCstr(txtAccNo.Text)
                obj.From_Location_Code = clsCommon.myCstr(txtFromLoc.Value)
                obj.To_location_Code = clsCommon.myCstr(txtToLoc.Value)
                obj.Location_Code = clsCommon.myCstr(txtLocation.Value)
                obj.Preferred_Location_Code = clsCommon.myCstr(txtPreferedLoc.Value)
                obj.Current_Gross_Salary = clsCommon.myCdbl(txtCurrGrossSal.Value)
                obj.Total_CTC = clsCommon.myCdbl(txtTotalCTC.Value)
                obj.Performance_By = clsCommon.myCstr(txtPerBy.Text)
                'obj.Is_Fresher = Is_Fresher
                'obj.Is_Handicaped = Is_Handicaped
                'obj.Relocation = Relocation

                'SaveImage()
                '' Qualification 
                obj.ObjList = New List(Of ClsAppQualificationDetail)
                For Each grow As GridViewRowInfo In gvQualification.Rows
                    If clsCommon.myLen(grow.Cells(ColUnvClg).Value) <= 0 Then
                        Continue For
                    End If
                    Dim objTr As New ClsAppQualificationDetail()
                    objTr.APPLICANT_CODE = clsCommon.myCstr(Me.txtcode.Value)
                    objTr.COLLEGE_UNIVERSITY = clsCommon.myCstr(grow.Cells(ColUnvClg).Value)
                    objTr.COURSE_CODE = clsCommon.myCstr(grow.Cells(ColCourse).Value)
                    objTr.Year_Of_Passing = clsCommon.myCstr(grow.Cells(ColYrOfPassing).Value)
                    objTr.GRADE_PERCENTAGE = clsCommon.myCstr(grow.Cells(ColGradePercentage).Value)
                    If CBool(grow.Cells(ColHighQual).Value) = True Then
                        objTr.Higher_Qualification = "1"
                    Else
                        objTr.Higher_Qualification = "0"

                    End If
                    ' objTr.Higher_Qualification = clsCommon.myCstr(grow.Cells(ColHighQual).Value)
                    objTr.Course_Type = clsCommon.myCstr(grow.Cells(ColCourseType).Value)
                    obj.ObjList.Add(objTr)
                Next
                '' Check List
                obj.ObjListChk = New List(Of ClsAppCheckListDetail)
                For Each grow As GridViewRowInfo In gvDoc.Rows
                    If clsCommon.myLen(grow.Cells("Check List Code").Value) <= 0 Then
                        Continue For
                    End If
                    Dim objTr1 As New ClsAppCheckListDetail()
                    objTr1.APPLICANT_CODE = clsCommon.myCstr(Me.txtcode.Value)
                    objTr1.Chk_Code = clsCommon.myCstr(grow.Cells("Check List Code").Value)
                    'If CBool(grow.Cells("Received").Value) = True Then
                    '    objTr1.Is_Received = "1"
                    'Else
                    '    objTr1.Is_Received = "0"
                    'End If
                    If CBool(grow.Cells("Manadatory").Value) = True Then
                        objTr1.Is_Manadatory = "1"
                    Else
                        objTr1.Is_Manadatory = "0"
                    End If
                    obj.ObjListChk.Add(objTr1)
                Next

                '' Emp History 
                obj.ObjListEmp = New List(Of ClsAppEmpHisListDetail)
                For Each grow As GridViewRowInfo In gvEmpHis.Rows
                    If clsCommon.myLen(grow.Cells(ColOrgName).Value) <= 0 Then
                        Continue For
                    End If
                    Dim objTr As New ClsAppEmpHisListDetail()
                    objTr.APPLICANT_CODE = clsCommon.myCstr(Me.txtcode.Value)
                    objTr.Organisation_Name = clsCommon.myCstr(grow.Cells(ColOrgName).Value)
                    objTr.From_Period = clsCommon.myCstr(grow.Cells(ColFromPeriod).Value)
                    objTr.To_Period = clsCommon.myCstr(grow.Cells(ColToPeriod).Value)
                    objTr.DESIGNATION_ID = clsCommon.myCstr(grow.Cells(ColDesignation).Value)
                    'objTr.Till_Date = clsCommon.myCstr(grow.Cells(ColCheckListCode).Value)
                    objTr.Roles_and_Responsibilities = clsCommon.myCstr(grow.Cells(ColRolesRes).Value)
                    objTr.Reason_for_Seperation = clsCommon.myCstr(grow.Cells(ColReasSep).Value)
                    If CBool(grow.Cells(ColTillDate).Value) = True Then
                        objTr.Till_Date = "1"
                    Else
                        objTr.Till_Date = "0"

                    End If
                    ' objTr.Higher_Qualification = clsCommon.myCstr(grow.Cells(ColHighQual).Value)
                    ' objTr.Course_Type = clsCommon.myCstr(grow.Cells(ColCourseType).Value)
                    obj.ObjListEmp.Add(objTr)
                Next

                '' Family Background
                obj.ObjListFamily = New List(Of ClsAppFamilyListDetail)
                For Each grow As GridViewRowInfo In gvFamily.Rows
                    If clsCommon.myLen(grow.Cells(ColRelation).Value) <= 0 Then
                        Continue For
                    End If
                    Dim objTr1 As New ClsAppFamilyListDetail()
                    objTr1.APPLICANT_CODE = clsCommon.myCstr(Me.txtcode.Value)
                    objTr1.Qualification_Code = clsCommon.myCstr(grow.Cells(ColQualification).Value)
                    objTr1.Family_Date_Of_Birth = clsCommon.myCstr(grow.Cells(ColDateofBirth).Value)
                    objTr1.Occupation_Code = clsCommon.myCstr(grow.Cells(ColOccupation).Value)
                    objTr1.Relation_Code = clsCommon.myCstr(grow.Cells(ColRelation).Value)
                    obj.ObjListFamily.Add(objTr1)
                Next
                '' For Document Saving
                obj.DocName = OpenFileDialog1.SafeFileName
                Dim cnt As Int16 = 0
                Dim bDataR As Byte() = Nothing
                If clsCommon.myLen(OpenFileDialog1.FileName) > 0 Then
                    Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(OpenFileDialog1.FileName))
                    bDataR = br.ReadBytes(br.BaseStream.Length)
                    obj.DOCUMENT_FILE = bDataR
                    br.Close() ' done by stuti reagrding memory leakage
                End If


                arr.Add(obj)
                ''''''Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If (ClsApplicantEntry.SaveData(arr)) Then

                    '' For Image Saving
                    'SaveImage(obj.APPLICANT_CODE)

                    If clsCommon.myLen(File_Name) > 0 Then
                        Dim bData As Byte()
                        Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(clsCommon.myCstr(File_Name)))
                        bData = br.ReadBytes(br.BaseStream.Length)

                        Dim Str As String = " UPDATE TSPL_HR_APPLICANT_ENTRY set Applicant_Photo = @BLOBData where APPLICANT_CODE='" + obj.APPLICANT_CODE + "'"
                        Dim cmd As SqlCommand = New SqlCommand(Str, clsDBFuncationality.GetConnnection)
                        Dim prm As New SqlParameter("@BLOBData", bData)
                        cmd.Parameters.Add(prm)
                        cmd.ExecuteNonQuery()
                        br.Close() ' done by stuti reagrding memory leakage
                    End If

                    '' For Document Saving
                    If clsCommon.myLen(OpenFileDialog1.FileName) > 0 Then
                        Dim Str As String
                        Str = "UPDATE TSPL_HR_APPLICANT_ENTRY set DOCUMENT_FILE = @BLOBData,DocName='" & txtResume.Text & "' where APPLICANT_CODE = '" + obj.APPLICANT_CODE + "'"
                        Dim cmd As SqlCommand = New SqlCommand(Str, clsDBFuncationality.GetConnnection)
                        Dim prm As New SqlParameter("@BLOBData", bDataR)
                        cmd.Parameters.Add(prm)
                        cnt = cmd.ExecuteNonQuery()
                    End If

                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.APPLICANT_CODE, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                    btnShowDoc.Enabled = True
                    BtnDeleteDoc.Enabled = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                    btnShowDoc.Enabled = False
                    BtnDeleteDoc.Enabled = False
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Try
            txtcode.MyReadOnly = True
            btnsave.Enabled = True
            btndelete.Enabled = True
            ' btnShowDoc.Enabled = True
            BtnDeleteDoc.Enabled = True
            isNewEntry = False

            Dim Course_Name As String
            Dim Chk_Description As String
            Dim Relation_Name As String
            Dim Qualification_Name As String
            Dim Occupation_Name As String
            Dim Designation_Desc As String

            Dim obj As New ClsApplicantEntry()
            obj = ClsApplicantEntry.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.APPLICANT_CODE) > 0) Then
                funReset()
                isNewEntry = False
                btnsave.Text = "Update"
                btndelete.Enabled = True
                ' btnShowDoc.Enabled = True
                BtnDeleteDoc.Enabled = True
                txtcode.Value = obj.APPLICANT_CODE
                txtdesp.Text = obj.Applicant_Description
                dtpDate.Value = obj.Applicant_Date
                txtrequisitioncode.Value = obj.Requisition_Code
                '' After Posted From ShortList Screen
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                Else
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                End If
                UsLock1.Status = obj.Posted
                ''
                ShortListed = obj.ShortListed
                Rejected = obj.Rejected
                If clsCommon.myLen(txtrequisitioncode.Value) > 0 Then
                    lblrequisition.Text = clsDBFuncationality.getSingleValue("Select isnull(Requisition_Description,'') As Requisition_Description From TSPL_HR_REQUISITION Where Requisition_Code ='" + txtrequisitioncode.Value + "'")
                Else
                    lblrequisition.Text = ""
                End If
                dtpDateofIntr.Value = obj.Date_Of_Interview
                cmbGender.SelectedValue = obj.Gender
                cmbSalutation.SelectedValue = obj.Salutation
                txtFirstName.Text = obj.First_Name
                txtMiddleName.Text = obj.Middle_Name
                txtLastName.Text = obj.Last_Name
                dtpDateofBirth.Value = obj.Applicant_Date_Of_Birth
                CmbMarStatus.SelectedValue = obj.Maritial_Status
                txtPanNo.Text = obj.Pan_No
                'Applicant_Photo = obj.Applicant_Photo
                txtAdd1.Text = obj.Add1
                txtAdd2.Text = obj.Add2
                txtadd3.Text = obj.Add3
                txtadd4.Text = obj.Add4
                txtCountry.Value = obj.COUNTRY_CODE
                If clsCommon.myLen(txtCountry.Value) > 0 Then
                    lblCountry.Text = clsDBFuncationality.getSingleValue("Select isnull(Country_Name,'') As Country_Name From TSPL_Country_Master Where Country_Code ='" + txtCountry.Value + "'")
                Else
                    lblCountry.Text = ""
                End If
                txtState.Value = obj.State_Code
                If clsCommon.myLen(txtState.Value) > 0 Then
                    lblState.Text = clsDBFuncationality.getSingleValue("Select isnull(State_Name,'') As State_Name From TSPL_State_Master Where State_Code ='" + txtState.Value + "'")
                Else
                    lblState.Text = ""
                End If
                txtCity.Value = obj.City_code
                If clsCommon.myLen(txtCity.Value) > 0 Then
                    lblCity.Text = clsDBFuncationality.getSingleValue("Select isnull(City_Name,'') As City_Name From TSPL_City_Master Where City_Code ='" + txtCity.Value + "'")
                Else
                    lblCity.Text = ""
                End If
                If obj.Emp_Refrence = 1 Then
                    rbnRefbyEmp.IsChecked = True
                Else
                    rbnRefbyEmp.IsChecked = False
                End If
                If obj.Agency_Refrence = 1 Then
                    rbnrefbyAge.IsChecked = True
                Else
                    rbnrefbyAge.IsChecked = False
                End If
                txtEmpCode.Value = obj.Emp_Code
                If clsCommon.myLen(txtEmpCode.Value) > 0 Then
                    lblEmpName.Text = clsDBFuncationality.getSingleValue("Select isnull(Emp_Name,'') As Emp_Name From TSPL_Employee_Master Where EMP_CODE ='" + txtEmpCode.Value + "'")
                Else
                    lblEmpName.Text = ""
                End If
                txtAgency.Value = obj.Agency_Code
                If clsCommon.myLen(txtAgency.Value) > 0 Then
                    LblAgency.Text = clsDBFuncationality.getSingleValue("Select isnull(Name,'') As Name From Tspl_HR_Agency_Master Where CODE ='" + txtAgency.Value + "'")
                Else
                    LblAgency.Text = ""
                End If
                txtHandiDetail.Text = obj.Handicaped_Detail
                txtBloodGrp.Text = obj.Blood_Group
                txtBankCode.Value = obj.Bank_Code
                If clsCommon.myLen(txtBankCode.Value) > 0 Then
                    LblBankName.Text = clsDBFuncationality.getSingleValue("Select isnull(DESCRIPTION,'') As DESCRIPTION From TSPL_BANK_MASTER Where BANK_CODE ='" + txtBankCode.Value + "'")
                Else
                    LblBankName.Text = ""
                End If
                txtBranchCode.Value = obj.Branch_Code
                If clsCommon.myLen(txtBranchCode.Value) > 0 Then
                    lblBranchName.Text = clsDBFuncationality.getSingleValue("Select isnull(BRANCH_NAME,'') As BRANCH_NAME From TSPL_BRANCH_MASTER Where BRANCH_CODE ='" + txtBranchCode.Value + "'")
                Else
                    lblBranchName.Text = ""
                End If
                txtAccNo.Text = obj.Account_No
                txtFromLoc.Value = obj.From_Location_Code
                If clsCommon.myLen(txtFromLoc.Value) > 0 Then
                    LblFromLoc.Text = clsDBFuncationality.getSingleValue("Select isnull(Location_Desc,'') As Location_Desc From TSPL_LOCATION_MASTER Where Location_Code ='" + txtFromLoc.Value + "'")
                Else
                    LblFromLoc.Text = ""
                End If
                txtToLoc.Value = obj.To_location_Code
                If clsCommon.myLen(txtToLoc.Value) > 0 Then
                    LblToLoc.Text = clsDBFuncationality.getSingleValue("Select isnull(Location_Desc,'') As Location_Desc From TSPL_LOCATION_MASTER Where Location_Code ='" + txtToLoc.Value + "'")
                Else
                    LblToLoc.Text = ""
                End If
                txtLocation.Value = obj.Location_Code
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    LblLocation.Text = clsDBFuncationality.getSingleValue("Select isnull(Location_Desc,'') As Location_Desc From TSPL_LOCATION_MASTER Where Location_Code ='" + txtLocation.Value + "'")
                Else
                    LblLocation.Text = ""
                End If
                txtPreferedLoc.Value = obj.Preferred_Location_Code
                If clsCommon.myLen(txtPreferedLoc.Value) > 0 Then
                    LblPreLoc.Text = clsDBFuncationality.getSingleValue("Select isnull(Location_Desc,'') As Location_Desc From TSPL_LOCATION_MASTER Where Location_Code ='" + txtPreferedLoc.Value + "'")
                Else
                    LblPreLoc.Text = ""
                End If
                txtCurrGrossSal.Value = obj.Current_Gross_Salary
                txtTotalCTC.Value = obj.Total_CTC
                txtPerBy.Text = obj.Performance_By
                If obj.Is_Fresher = 1 Then
                    ChkFresher.Checked = True
                Else
                    ChkFresher.Checked = False
                End If
                If obj.Is_Handicaped = 1 Then
                    ChkHandicaped.Checked = True
                    txtHandiDetail.Enabled = True
                    txtHandiDetail.MendatroryField = True
                Else
                    ChkHandicaped.Checked = False
                    txtHandiDetail.Enabled = False
                    txtHandiDetail.MendatroryField = False
                End If
                If obj.Relocation = 1 Then
                    ChkRelocation.Checked = True
                    grpReLoc.Enabled = True
                    txtFromLoc.MendatroryField = True
                    txtToLoc.MendatroryField = True
                Else
                    ChkRelocation.Checked = False
                    grpReLoc.Enabled = False
                    txtFromLoc.MendatroryField = False
                    txtToLoc.MendatroryField = False
                End If
                txtPinCode.Text = obj.Pin_Code
                txtEmail.Text = obj.Email
                txtTelephone.Text = obj.TELEPHONE_NO
                txtRelation.Value = obj.Relation_Code
                If clsCommon.myLen(txtRelation.Value) > 0 Then
                    lblRelation.Text = clsDBFuncationality.getSingleValue("Select isnull(Relation_Name,'') As Relation_Name From TSPL_HR_RELATION_MASTER Where Relation_Code ='" + txtRelation.Value + "'")
                Else
                    lblRelation.Text = ""
                End If
                txtsourcedetail.Value = obj.Source_Type_Detail_Code
                If clsCommon.myLen(txtsourcedetail.Value) > 0 Then
                    lblsourcedetail.Text = clsDBFuncationality.getSingleValue("Select isnull(Source_Name,'') As Source_Name From TSPL_HR_SOURCE_TYPE_DETAIL Where Source_Type_Detail_Code ='" + txtsourcedetail.Value + "'")
                Else
                    lblsourcedetail.Text = ""
                End If
                txtsourcetype.Value = obj.Source_Type_Code
                If clsCommon.myLen(txtsourcetype.Value) > 0 Then
                    lblsourcetype.Text = clsDBFuncationality.getSingleValue("Select isnull(Source_Name,'') As Source_Name From TSPL_HR_SOURCE_TYPE Where Source_Type_Code ='" + txtsourcetype.Value + "'")
                Else
                    lblsourcetype.Text = ""
                End If

                txtResume.Text = obj.DocName
                If clsCommon.myLen(txtResume.Text) > 0 Then
                    btnShowDoc.Enabled = True
                    BtnDeleteDoc.Enabled = True
                Else
                    btnShowDoc.Enabled = False
                    BtnDeleteDoc.Enabled = False
                End If
                txtcode.MyReadOnly = True

                '' Qualification Details

                Dim ii As Int16 = 0
                If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                    LoadQualificationGrid()
                    For Each objTr As ClsAppQualificationDetail In obj.ObjList
                        gvQualification.Rows.AddNew()
                        ii = ii + 1
                        gvQualification.Rows(gvQualification.Rows.Count - 1).Cells(ColUnvClg).Value = objTr.COLLEGE_UNIVERSITY
                        gvQualification.Rows(gvQualification.Rows.Count - 1).Cells(ColCourse).Value = objTr.COURSE_CODE
                        gvQualification.Rows(gvQualification.Rows.Count - 1).Cells(ColYrOfPassing).Value = objTr.Year_Of_Passing
                        gvQualification.Rows(gvQualification.Rows.Count - 1).Cells(ColGradePercentage).Value = objTr.GRADE_PERCENTAGE
                        gvQualification.Rows(gvQualification.Rows.Count - 1).Cells(ColCourseType).Value = objTr.Course_Type
                        gvQualification.Rows(gvQualification.Rows.Count - 1).Cells(ColHighQual).Value = objTr.Higher_Qualification
                        'If objTr.Higher_Qualification = "1" Then
                        '    gvQualification.Rows(gvQualification.Rows.Count - 1).Cells(ColHighQual).Value = True
                        'Else
                        '    gvQualification.Rows(gvQualification.Rows.Count - 1).Cells(ColHighQual).Value = False
                        'End If
                        Course_Name = clsDBFuncationality.getSingleValue("Select Course_Name From TSPL_COURSE_MASTER Where COURSE_CODE ='" + objTr.COURSE_CODE + "'")
                        gvQualification.Rows(gvQualification.Rows.Count - 1).Cells(ColCourseName).Value = Course_Name
                    Next
                End If

                '' CHECK LIST
                Dim i As Int16 = 0
                If obj.ObjListChk IsNot Nothing AndAlso obj.ObjListChk.Count > 0 Then
                    'LoadCheckListGrid()
                    For Each objTr As ClsAppCheckListDetail In obj.ObjListChk

                        i = i + 1
                        gvDoc.Rows(gvDoc.Rows.Count - 1).Cells("Check List Code").Value = objTr.Chk_Code
                        Chk_Description = clsDBFuncationality.getSingleValue("Select Chk_Description From TSPL_HR_Check_List Where Chk_Code ='" + objTr.Chk_Code + "'")
                        gvDoc.Rows(gvDoc.Rows.Count - 1).Cells("Check List Name").Value = Chk_Description
                        gvDoc.Rows(gvDoc.Rows.Count - 1).Cells("Received").Value = objTr.Is_Received
                        gvDoc.Rows(gvDoc.Rows.Count - 1).Cells("Manadatory").Value = objTr.Is_Manadatory
                        gvDoc.Rows.AddNew()
                    Next
                End If

                'LoadFromAppCheckList(clsCommon.myCstr(txtcode.Value))

                '' EMP HISTORY
                Dim j As Int16 = 0
                If obj.ObjListEmp IsNot Nothing AndAlso obj.ObjListEmp.Count > 0 Then
                    LoadEmpGrid()
                    For Each objTr As ClsAppEmpHisListDetail In obj.ObjListEmp
                        gvEmpHis.Rows.AddNew()
                        j = j + 1
                        gvEmpHis.Rows(gvEmpHis.Rows.Count - 1).Cells(ColOrgName).Value = objTr.Organisation_Name
                        gvEmpHis.Rows(gvEmpHis.Rows.Count - 1).Cells(ColFromPeriod).Value = objTr.From_Period
                        gvEmpHis.Rows(gvEmpHis.Rows.Count - 1).Cells(ColToPeriod).Value = objTr.To_Period
                        gvEmpHis.Rows(gvEmpHis.Rows.Count - 1).Cells(ColRolesRes).Value = objTr.Roles_and_Responsibilities
                        gvEmpHis.Rows(gvEmpHis.Rows.Count - 1).Cells(ColReasSep).Value = objTr.Reason_for_Seperation
                        gvEmpHis.Rows(gvEmpHis.Rows.Count - 1).Cells(ColDesignation).Value = objTr.DESIGNATION_ID
                        If objTr.Till_Date = "1" Then
                            gvEmpHis.Rows(gvEmpHis.Rows.Count - 1).Cells(ColTillDate).Value = True
                        Else
                            gvEmpHis.Rows(gvEmpHis.Rows.Count - 1).Cells(ColTillDate).Value = False
                        End If
                        Designation_Desc = clsDBFuncationality.getSingleValue("Select Designation_Desc From TSPL_DESIGNATION_MASTER Where Designation_id ='" + objTr.DESIGNATION_ID + "'")
                        gvEmpHis.Rows(gvEmpHis.Rows.Count - 1).Cells(ColDesignationName).Value = Designation_Desc
                    Next
                End If

                '' FAMILY BACKGROUND
                Dim k As Int16 = 0
                If obj.ObjListFamily IsNot Nothing AndAlso obj.ObjListFamily.Count > 0 Then
                    LoadFamilyGrid()
                    For Each objTr As ClsAppFamilyListDetail In obj.ObjListFamily
                        gvFamily.Rows.AddNew()
                        k = k + 1
                        gvFamily.Rows(gvFamily.Rows.Count - 1).Cells(ColDateofBirth).Value = objTr.Family_Date_Of_Birth
                        gvFamily.Rows(gvFamily.Rows.Count - 1).Cells(ColRelation).Value = objTr.Relation_Code
                        gvFamily.Rows(gvFamily.Rows.Count - 1).Cells(ColOccupation).Value = objTr.Occupation_Code
                        gvFamily.Rows(gvFamily.Rows.Count - 1).Cells(ColQualification).Value = objTr.Qualification_Code

                        Qualification_Name = clsDBFuncationality.getSingleValue("Select Qualification_Name From TSPL_HR_QUALIFICATION_MASTER Where Qualification_Code ='" + objTr.Qualification_Code + "'")
                        gvFamily.Rows(gvFamily.Rows.Count - 1).Cells(ColQualificationName).Value = Qualification_Name
                        Occupation_Name = clsDBFuncationality.getSingleValue("Select Occupation_Name From TSPL_OCCUPATION_MASTER Where OCCUPATION_CODE ='" + objTr.Occupation_Code + "'")
                        gvFamily.Rows(gvFamily.Rows.Count - 1).Cells(ColOccupationName).Value = Occupation_Name
                        Relation_Name = clsDBFuncationality.getSingleValue("Select Relation_Name From TSPL_HR_RELATION_MASTER Where Relation_Code ='" + objTr.Relation_Code + "'")
                        gvFamily.Rows(gvFamily.Rows.Count - 1).Cells(ColRelationName).Value = Relation_Name
                    Next
                End If
                ' LoadImage()
                'isInsideLoadData = False
                Try
                    '============ Display Image====================
                    Dim Filename As Byte() = clsDBFuncationality.getSingleValue("select Applicant_Photo from TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE='" & txtcode.Value & "'")
                    Using ms As New IO.MemoryStream(CType(Filename, Byte()))
                        Dim img As Image = Image.FromStream(ms)
                        PicImage.Image = img

                        ''richa agarwal regarding memory leakage
                        ms.Close()
                        ms.Dispose()
                    End Using
                    '=============================================
                Catch ex As Exception
                End Try
            Else
                isNewEntry = True
                funReset()
                'Me.gvQualification.Rows.Clear()
                'Me.gvQualification.Rows.AddNew()
                'gvDoc.DataSource = Nothing
                'Me.gvDoc.Rows.Clear()
                'Me.gvDoc.Rows.AddNew()
                'Me.gvEmpHis.Rows.Clear()
                'Me.gvEmpHis.Rows.AddNew()
                'Me.gvFamily.Rows.Clear()
                'Me.gvFamily.Rows.AddNew()
                UsLock1.Status = ERPTransactionStatus.Pending
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub FrmApplicantEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub FrmApplicantEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")
        isNewEntry = True
        LoadQualificationGrid()
        'LoadCheckListGrid()
        LoadCheckListGrid()
        LoadEmpGrid()
        LoadFamilyGrid()
        funReset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub gvQualification_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvQualification.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gvQualification.Columns(ColCourse) Then
                    OpenCourseCodeList(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtrequisitioncode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtrequisitioncode._MYValidating
        'Dim qry As String = "Select Requisition_Code As Code,Requisition_Description AS [Requistion Description],TSPL_HR_REQUISITION.DEPARTMENT_CODE AS [Department Code],TSPL_HR_REQUISITION.Designation_id As [Designation Id],TSPL_HR_REQUISITION.Job_Title_Code As [Job Title Code] ,TSPL_HR_REQUISITION.Qualification_Code As [Qualification Code] from TSPL_HR_REQUISITION " & _
        '                "LEFT OUTER JOIN TSPL_DEPARTMENT_MASTER ON TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE =TSPL_HR_REQUISITION.DEPARTMENT_CODE " & _
        '                "LEFT OUTER JOIN TSPL_DESIGNATION_MASTER ON TSPL_DESIGNATION_MASTER.Designation_id   =TSPL_HR_REQUISITION.Designation_id " & _
        '                "LEFT OUTER JOIN TSPL_HR_JOB_TITLE ON TSPL_HR_JOB_TITLE.JOB_TITLE_CODE   =TSPL_HR_REQUISITION.Job_Title_Code  " & _
        '                "LEFT OUTER JOIN TSPL_HR_QUALIFICATION_MASTER ON TSPL_HR_QUALIFICATION_MASTER.Qualification_Code  =TSPL_HR_REQUISITION.Qualification_Code  "
        '' Anubhooti 16-Apr-2015 BM00000003975 (Close req. should not be shown only approved req. should be shown)
        Dim qry As String = "SELECT * FROM (Select Requisition_Code As [Code],MAX(Requisition_Description) AS [Requisition Description],MAX([Requisition Date]) As [Requisition Date],MAX(Initiated_By) AS [Initiated By],MAX(DEPARTMENT_CODE) AS [Department Code],MAX(Location_Code) AS [Location Code],MAX(Job_Title_Code) AS [Job Title Code],MAX(Industry_Code) As [Industry Code],MAX(Vertical_Code) AS Vertical_Code,MAX(Emp_Type) AS [Emp Type],MAX(Profile_Code) AS [Profile Code],MAX(Gender) AS Gender, MAX(NoOfPost) as NoOfPost, Count(APPLICANT_CODE) AS TotalCount FROM ( " & _
                            " SELECT TSPL_HR_REQUISITION.Requisition_Code,TSPL_HR_REQUISITION.Requisition_Description,TSPL_HR_REQUISITION.Date As [Requisition Date],TSPL_HR_REQUISITION.Initiated_By ,TSPL_HR_REQUISITION.DEPARTMENT_CODE ,TSPL_HR_REQUISITION.Location_Code,TSPL_HR_REQUISITION.Job_Title_Code,TSPL_HR_REQUISITION.Industry_Code ,TSPL_HR_REQUISITION.Vertical_Code ,TSPL_HR_REQUISITION.Emp_Type ,TSPL_HR_REQUISITION.Profile_Code ,TSPL_HR_REQUISITION.Gender,TSPL_HR_REQUISITION.NoOfPost, TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE FROM TSPL_HR_REQUISITION " & _
                            " LEFT OUTER JOIN TSPL_HR_APPLICANT_ENTRY ON TSPL_HR_REQUISITION.Requisition_Code = TSPL_HR_APPLICANT_ENTRY.Requisition_Code " & _
                            " WHERE Approved_Status=1 AND TSPL_HR_REQUISITION.Closed_Status=0 " & _
                            " ) XXX Group By XXX.Requisition_Code " & _
                            " ) YYY "
        txtrequisitioncode.Value = clsCommon.ShowSelectForm("REQFND", qry, "Code", "  NoOfPost>TotalCount ", txtrequisitioncode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtrequisitioncode.Value) > 0 Then
            lblrequisition.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Requisition_Description As [Requisition Description] FROM TSPL_HR_REQUISITION Where Requisition_Code='" + txtrequisitioncode.Value + "'"))
            LoadJobTitleCheckList()
        Else
            lblrequisition.Text = ""
            gvDoc.DataSource = Nothing
            gvDoc.Rows.Clear()
            End If
    End Sub

    Private Sub txtsourcedetail__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtsourcedetail._MYValidating

        If clsCommon.myLen(txtsourcetype.Value) > 0 Then
            Dim qry As String = " select Source_Type_Detail_Code As Code,Source_Name As [Source Name] FROM TSPL_HR_SOURCE_TYPE_DETAIL "
            Dim Src_Code_qry As String = "select Source_Type_Code As Code FROM TSPL_HR_SOURCE_TYPE where Source_Type_Code ='" + txtsourcetype.Value + "'"
            Dim Source_Code As String = clsDBFuncationality.getSingleValue(Src_Code_qry)
            txtsourcedetail.Value = clsCommon.ShowSelectForm("FNDSRC", qry, "Code", "Source_Type_Code ='" + Source_Code + "'", txtsourcedetail.Value, "Source_Type_Code", isButtonClicked)
            If clsCommon.myLen(txtsourcedetail.Value) > 0 Then
                lblsourcedetail.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Source_Name As Name FROM TSPL_HR_SOURCE_TYPE_DETAIL Where Source_Type_Detail_Code='" + txtsourcedetail.Value + "'"))
            Else
                lblsourcedetail.Text = ""
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "first select source type", Me.Text)
        End If


        'Dim qry As String = " select Source_Type_Detail_Code As Code,Source_Name As [Source_Name] FROM TSPL_HR_SOURCE_TYPE_DETAIL  "
        'txtsourcedetail.Value = clsCommon.ShowSelectForm("FNDSRCD", qry, "Code", "", txtsourcedetail.Value, "Source_Type_Code", isButtonClicked)
        'If clsCommon.myLen(txtsourcedetail.Value) > 0 Then
        '    lblsourcedetail.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Source_Name As Name FROM TSPL_HR_SOURCE_TYPE_DETAIL Where Source_Type_Detail_Code='" + txtsourcedetail.Value + "'"))
        'Else
        '    lblsourcedetail.Text = ""
        'End If
    End Sub

    Private Sub txtsourcetype__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtsourcetype._MYValidating
        'If clsCommon.myLen(txtsourcedetail.Value) > 0 Then
        '    Dim qry As String = " select Source_Type_Code As Code,Source_Name As [Source_Name] FROM TSPL_HR_SOURCE_TYPE "
        '    Dim Src_Code_qry As String = "select Source_Type_Code As Code FROM TSPL_HR_SOURCE_TYPE_DETAIL where Source_Type_Detail_Code ='" + txtsourcedetail.Value + "'"
        '    Dim Source_Code As String = clsDBFuncationality.getSingleValue(Src_Code_qry)
        '    txtsourcetype.Value = clsCommon.ShowSelectForm("FNDSRC", qry, "Code", "Source_Type_Code ='" + Source_Code + "'", txtsourcetype.Value, "Source_Type_Code", isButtonClicked)
        '    If clsCommon.myLen(txtsourcetype.Value) > 0 Then
        '        lblsourcetype.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Source_Name As Name FROM TSPL_HR_SOURCE_TYPE Where Source_Type_Code='" + txtsourcetype.Value + "'"))
        '    Else
        '        lblsourcetype.Text = ""
        '    End If
        'Else
        '    clsCommon.MyMessageBoxShow("first select source type detail")
        'End If

        Dim qry As String = " select Source_Type_Code As Code,Source_Name As [Source Name] FROM TSPL_HR_SOURCE_TYPE  "
        txtsourcetype.Value = clsCommon.ShowSelectForm("FNDSRCD", qry, "Code", "", txtsourcetype.Value, "Source_Type_Code", isButtonClicked)
        If clsCommon.myLen(txtsourcetype.Value) > 0 Then
            lblsourcetype.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Source_Name As Name FROM TSPL_HR_SOURCE_TYPE Where Source_Type_Code='" + txtsourcetype.Value + "'"))
        Else
            lblsourcetype.Text = ""
        End If
    End Sub

    Private Sub txtState__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtState._MYValidating
        Try
            If clsCommon.myLen(txtCountry.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Country First..", Me.Text)
                txtCountry.Focus()
                Exit Sub
            End If
            Dim whrcls As String = "country_code='" & clsCommon.myCstr(txtCountry.Value) & "'"

            txtState.Value = clsStateMaster.getFinder(whrcls, txtState.Value, isButtonClicked)
            If clsCommon.myLen(txtState.Value) > 0 Then
                lblState.Text = clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" & txtState.Value & "'")
                txtCity.Value = ""
                lblCity.Text = ""
            Else
                lblState.Text = ""
                lblCity.Text = ""
                txtState.Value = ""
                txtCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtEmpCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtEmpCode._MYValidating
        Dim qry As String = ""
        qry = "select EMP_CODE as [Code],Emp_Name as [Employee Name],FATHERS_NAME as [Fathers Name],MOTHERS_NAME as [Mothers Name],convert(varchar(12),Birth_date,103) as [Date of Birth],SEX as [Sex],MARITAL_STATUS as [Marital Status],SPOUSE_NAME as [Spouse Name]," & _
                           "Designation as [Designation],OCCUPATION_CODE as [Occupation],DEPARTMENT_CODE as [Department] From TSPL_EMPLOYEE_MASTER "

        txtEmpCode.Value = clsCommon.ShowSelectForm("EMPFND", qry, "Code", "", txtEmpCode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtEmpCode.Value) > 0 Then
            lblEmpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Emp_Name as [Employee Name] FROM TSPL_EMPLOYEE_MASTER Where EMP_CODE='" + txtEmpCode.Value + "'"))
        Else
            lblEmpName.Text = ""
        End If
    End Sub

    Private Sub txtBranchCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtBranchCode._MYValidating
        Dim qry As String = "select BRANCH_CODE as Code, BRANCH_NAME as Name,RESPONSIBLE_PERSION_NAME as 'Responsible Persion', BRANCH_ADDRESS as 'Branch Address', CITY_CODE as 'City Code', STATE_CODE as 'State Code' , COUNTRY_CODE as 'Country Code', PHONE_NO as 'Phone No',FAX_NO as 'Fax No', EMAIL_ID as 'Email Id'  from TSPL_BRANCH_MASTER"
        txtBranchCode.Value = clsCommon.ShowSelectForm("BRANCH_MASTER", qry, "Code", "", txtBranchCode.Value, "BRANCH_CODE", isButtonClicked)
        If clsCommon.myLen(txtBranchCode.Value) > 0 Then
            lblBranchName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT BRANCH_NAME as Name FROM TSPL_BRANCH_MASTER Where BRANCH_CODE='" + txtBranchCode.Value + "'"))
        Else
            lblBranchName.Text = ""
        End If
    End Sub

    Private Sub txtCity__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCity._MYValidating
        Try
            If clsCommon.myLen(txtCountry.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Country First..", Me.Text)
                txtCountry.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtState.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select state First..", Me.Text)
                txtState.Focus()
                Exit Sub
            End If
            Dim whrcls As String = "state_code='" & clsCommon.myCstr(txtState.Value) & "'"
            txtCity.Value = clsCityMaster.getFinder(whrcls, txtCity.Value, isButtonClicked)
            If clsCommon.myLen(txtCity.Value) > 0 Then
                lblCity.Text = clsDBFuncationality.getSingleValue("select  city_name from tspl_city_master where city_code='" & txtCity.Value & "'")
            Else
                lblCity.Text = ""
                txtCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBankCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtBankCode._MYValidating
        Dim qry As String = "select BANK_CODE AS Code,DESCRIPTION as Name,BANKACC from TSPL_BANK_MASTER "
        txtBankCode.Value = clsCommon.ShowSelectForm("Bank", qry, "Code", "", txtBankCode.Value, "", isButtonClicked)
        Dim dt As DataTable
        qry = qry & " where BANK_CODE='" & clsCommon.myCstr(txtBankCode.Value) & "'"

        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            LblBankName.Text = clsCommon.myCstr(dt.Rows(0).Item("Name"))
        Else
            LblBankName.Text = ""
        End If
    End Sub

    Private Sub txtCountry__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCountry._MYValidating
        Try
            txtCountry.Value = clsCountryMaster.getFinder("", txtCountry.Value, isButtonClicked)
            If clsCommon.myLen(txtCountry.Value) > 0 Then
                lblCountry.Text = clsDBFuncationality.getSingleValue("select Country_name from tspl_country_master where country_code='" & txtCountry.Value & "'")
                lblState.Text = ""
                lblCity.Text = ""
                txtState.Value = ""
                txtCity.Value = ""
            Else
                txtCountry.Text = ""
                lblState.Text = ""
                lblCity.Text = ""
                txtState.Value = ""
                txtCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = " select TSPL_LOCATION_MASTER.Location_Code  as Code,TSPL_LOCATION_MASTER.Location_Desc  as [Loc Desc], (TSPL_LOCATION_MASTER.Add1+' '+TSPL_LOCATION_MASTER.Add2) as Address,TSPL_CITY_MASTER.City_Name as [City],TSPL_STATE_MASTER.STATE_NAME as [State],TSPL_LOCATION_MASTER.Pin_code as [Pin Code] from TSPL_LOCATION_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_LOCATION_MASTER.City_Code left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER.State  "
        txtLocation.Value = clsCommon.ShowSelectForm("FNDLOC", qry, "Code", " TSPL_LOCATION_MASTER.Is_Section ='N' AND TSPL_LOCATION_MASTER.Is_Sub_Location ='N' ", txtLocation.Value, "Location_Code", isButtonClicked)
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            LblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc As Name FROM TSPL_LOCATION_MASTER Where Location_Code='" + txtLocation.Value + "'"))
        Else
            LblLocation.Text = ""
        End If
    End Sub

    Private Sub txtRelation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtRelation._MYValidating
        Dim qry As String = " select Relation_Code As Code,Relation_Name As [Relation_Name] FROM TSPL_HR_RELATION_MASTER  "
        txtRelation.Value = clsCommon.ShowSelectForm("FNDREL", qry, "Code", "", txtRelation.Value, "Relation_Code", isButtonClicked)
        If clsCommon.myLen(txtRelation.Value) > 0 Then
            lblRelation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Relation_Name As Name FROM TSPL_HR_RELATION_MASTER Where Relation_Code='" + txtRelation.Value + "'"))
        Else
            lblRelation.Text = ""
        End If
    End Sub

    Private Sub txtPreferedLoc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtPreferedLoc._MYValidating
        Dim qry As String = " select TSPL_LOCATION_MASTER.Location_Code  as Code,TSPL_LOCATION_MASTER.Location_Desc  as [Loc Desc], (TSPL_LOCATION_MASTER.Add1+' '+TSPL_LOCATION_MASTER.Add2) as Address,TSPL_CITY_MASTER.City_Name as [City],TSPL_STATE_MASTER.STATE_NAME as [State],TSPL_LOCATION_MASTER.Pin_code as [Pin Code] from TSPL_LOCATION_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_LOCATION_MASTER.City_Code left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER.State  "
        txtPreferedLoc.Value = clsCommon.ShowSelectForm("FNDLOC", qry, "Code", " TSPL_LOCATION_MASTER.Is_Section ='N' AND TSPL_LOCATION_MASTER.Is_Sub_Location ='N'", txtPreferedLoc.Value, "Location_Code", isButtonClicked)
        If clsCommon.myLen(txtPreferedLoc.Value) > 0 Then
            LblPreLoc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc As Name FROM TSPL_LOCATION_MASTER Where Location_Code='" + txtPreferedLoc.Value + "'"))
        Else
            LblPreLoc.Text = ""
        End If
    End Sub

    Private Sub txtFromLoc__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromLoc._MYValidating
        Dim qry As String = " select TSPL_LOCATION_MASTER.Location_Code  as Code,TSPL_LOCATION_MASTER.Location_Desc  as [Loc Desc], (TSPL_LOCATION_MASTER.Add1+' '+TSPL_LOCATION_MASTER.Add2) as Address,TSPL_CITY_MASTER.City_Name as [City],TSPL_STATE_MASTER.STATE_NAME as [State],TSPL_LOCATION_MASTER.Pin_code as [Pin Code] from TSPL_LOCATION_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_LOCATION_MASTER.City_Code left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER.State  "
        txtFromLoc.Value = clsCommon.ShowSelectForm("FNDLOC", qry, "Code", " TSPL_LOCATION_MASTER.Is_Section ='N' AND TSPL_LOCATION_MASTER.Is_Sub_Location ='N'", txtFromLoc.Value, "Location_Code", isButtonClicked)
        If clsCommon.myLen(txtFromLoc.Value) > 0 Then
            LblFromLoc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc As Name FROM TSPL_LOCATION_MASTER Where Location_Code='" + txtFromLoc.Value + "'"))
        Else
            LblFromLoc.Text = ""
        End If
    End Sub

    Private Sub txtToLoc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtToLoc._MYValidating
        Dim qry As String = " select TSPL_LOCATION_MASTER.Location_Code  as Code,TSPL_LOCATION_MASTER.Location_Desc  as [Loc Desc], (TSPL_LOCATION_MASTER.Add1+' '+TSPL_LOCATION_MASTER.Add2) as Address,TSPL_CITY_MASTER.City_Name as [City],TSPL_STATE_MASTER.STATE_NAME as [State],TSPL_LOCATION_MASTER.Pin_code as [Pin Code] from TSPL_LOCATION_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_LOCATION_MASTER.City_Code left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER.State  "
        txtToLoc.Value = clsCommon.ShowSelectForm("FNDLOC", qry, "Code", " TSPL_LOCATION_MASTER.Is_Section ='N' AND TSPL_LOCATION_MASTER.Is_Sub_Location ='N'", txtToLoc.Value, "Location_Code", isButtonClicked)
        If clsCommon.myLen(txtToLoc.Value) > 0 Then
            LblToLoc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc As Name FROM TSPL_LOCATION_MASTER Where Location_Code='" + txtToLoc.Value + "'"))
        Else
            LblToLoc.Text = ""
        End If
    End Sub

    Private Sub gvDoc_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvDoc.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gvDoc.Columns(ColCheckListCode) Then
                    OpenCheckCodeList(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvEmpHis_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvEmpHis.CellFormatting
        If e.Column Is gvEmpHis.Columns(ColTillDate) Then
            If CBool(gvEmpHis.CurrentRow.Cells(ColTillDate).Value) = True Then
                gvEmpHis.CurrentRow.Cells(ColToPeriod).ReadOnly = True
                gvEmpHis.CurrentRow.Cells(ColToPeriod).Value = clsCommon.GETSERVERDATE()
            Else
                gvEmpHis.CurrentRow.Cells(ColToPeriod).ReadOnly = False
            End If
        End If
    End Sub

    Private Sub gvEmpHis_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvEmpHis.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gvEmpHis.Columns(ColDesignation) Then
                    OpenDesgCodeList(False)
                
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvFamily_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvFamily.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gvFamily.Columns(ColQualification) Then
                    OpenQualCodeList(False)
                ElseIf e.Column Is gvFamily.Columns(ColOccupation) Then
                    OpenOccCodeList(False)
                ElseIf e.Column Is gvFamily.Columns(ColRelation) Then
                    OpenRelCodeList(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub ChkFresher_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkFresher.CheckStateChanged
        If ChkFresher.Checked = True Then
            gvEmpHis.Enabled = False
        Else
            gvEmpHis.Enabled = True
        End If
    End Sub

    Private Sub gvQualification_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvQualification.CurrentColumnChanged
        If gvQualification.RowCount > 0 Then
            Dim intCurrRow As Integer = gvQualification.CurrentRow.Index
            'gvQualification.CurrentRow.Cells(ColUnvClg).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvQualification.Rows.Count - 1 Then
                'If Not (chkAgainst_RGP.Checked = True AndAlso clsCommon.myLen(txtRGPNo.Value) > 0) Then
                gvQualification.Rows.AddNew()
                gvQualification.Rows(gvQualification.Rows.Count - 1).Cells(ColCourseType).Value = "F"
                gvQualification.CurrentRow = gvQualification.Rows(intCurrRow)
                'End If
            End If
        End If
    End Sub

    Private Sub txtcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtcode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE='" + txtcode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtcode.MyReadOnly = False
            Else
                txtcode.MyReadOnly = True
            End If
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtcode._MYValidating
        'Dim str As String = "select count(*) from TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE ='" + txtcode.Value + "' "
        'Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'If no = 0 AndAlso isButtonClicked = False Then
        '    txtcode.MyReadOnly = False
        'Else
        '    txtcode.MyReadOnly = True
        'End If
        ''isInsideLoadData = True
        'If txtcode.MyReadOnly OrElse isButtonClicked Then
        '    Dim qry As String = ""
        '    qry = "Select APPLICANT_CODE As [Code],Applicant_Description As [Applicant Description] from TSPL_HR_APPLICANT_ENTRY"
        '    txtcode.Value = clsCommon.ShowSelectForm("TSPL_HR_APPLICANT_ENTRY", qry, "Code", "", txtcode.Value, "TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE", isButtonClicked)
        '    If clsCommon.myLen(txtcode.Value) > 0 Then
        '        Dim objOT As ClsApplicantEntry
        '        objOT = ClsApplicantEntry.GetData(txtcode.Value, NavigatorType.Current)
        '        If Not objOT Is Nothing Then

        '            LoadData(txtcode.Value, NavigatorType.Current)

        '        End If
        '    Else
        '        funReset()
        '    End If
        'End If


        '''' After Auto Genrated Code

        Dim qry As String = "Select APPLICANT_CODE As [Code],Applicant_Description As [Applicant Description] from TSPL_HR_APPLICANT_ENTRY"
        txtcode.Value = ClsApplicantEntry.GetFinder("", txtcode.Value, isButtonClicked)
        ' txtcode.Value = clsCommon.ShowSelectForm("TSPL_HR_APPLICANT_ENTRY", qry, "Code", "", txtcode.Value, "TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE", isButtonClicked)
        If clsCommon.myLen(txtcode.Value) > 0 Then
            Dim objOT As ClsApplicantEntry
            objOT = ClsApplicantEntry.GetData(txtcode.Value, NavigatorType.Current)
            If Not objOT Is Nothing Then
                LoadData(txtcode.Value, NavigatorType.Current)
            End If
        Else
            funReset()
        End If
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        OpenFileDialog1.ShowDialog()
        txtResume.Text = OpenFileDialog1.SafeFileName
        'Try
        '    If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '        Dim FileName As String = OpenFileDialog1.FileName
        '        If clsCommon.myLen(FileName) > 0 Then
        '            Using ms As New IO.MemoryStream(CType(GetPhoto(FileName), Byte()))
        '                Dim img As Image = Image.FromStream(ms)
        '                PicImage.Image = img
        '            End Using
        '            File_Name = OpenFileDialog1.FileName
        '        End If
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.ToString)
        'End Try
    End Sub

    Private Sub BtnDocReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDocReset.Click
        'txtResume.Text = ""
        'OpenFileDialog1.Reset()
    End Sub

    Private Sub ChkRelocation_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkRelocation.CheckStateChanged
        If ChkRelocation.Checked = True Then
            grpReLoc.Enabled = True
            txtFromLoc.MendatroryField = True
            txtToLoc.MendatroryField = True
        Else
            grpReLoc.Enabled = False
            txtFromLoc.MendatroryField = False
            txtToLoc.MendatroryField = False
        End If
    End Sub

    Private Sub ChkHandicaped_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkHandicaped.CheckStateChanged
        If ChkHandicaped.Checked = True Then
            txtHandiDetail.Enabled = True
            txtHandiDetail.MendatroryField = True
        Else
            txtHandiDetail.Enabled = False
            txtHandiDetail.MendatroryField = False
        End If
    End Sub

    Private Sub btnShowDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowDoc.Click
        Dim ds_attachment As DataTable
        Dim filename As String = ""
        Dim file_path As String = ""
        Dim file_extn As String = ""
        Try

            ds_attachment = New DataTable
            ds_attachment = ClsApplicantEntry.GetDocument(txtcode.Value, txtcode.Value)
           
            If (ds_attachment IsNot Nothing AndAlso ds_attachment.Rows.Count > 0) Then
                If clsCommon.myCstr(ds_attachment.Rows(0)("DocName")) <> "" Then
                    filename = clsCommon.myCstr(ds_attachment.Rows(0)("DocName"))
                    Dim blob As Byte() = ds_attachment.Rows(0)("DOCUMENT_FILE")
                    file_path = "C:\ERPTempFolder"
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
                    clsCommon.MyMessageBoxShow(Me, "No document found", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No document found", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnDeleteDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDeleteDoc.Click
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            myMessages.blankValue(Me, "Applicant ID ", Me.Text)
            txtcode.Focus()
        ElseIf ClsApplicantEntry.DeleteDocData(txtcode.Value, txtcode.Value) Then
            clsCommon.MyMessageBoxShow(Me, "Document Deleted Successfully.", Me.Text)
            'txtcode.Value = Nothing
            txtResume.Text = ""
            OpenFileDialog1.Reset()

        End If
    End Sub

    Private Sub gvFamily_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvFamily.CurrentColumnChanged
        If gvFamily.RowCount > 0 Then
            Dim intCurrRow As Integer = gvFamily.CurrentRow.Index
            If intCurrRow = gvFamily.Rows.Count - 1 Then
                gvFamily.Rows.AddNew()
                ' gvFamily.Rows(gvQualification.Rows.Count - 1).Cells(ColCourseType).Value = FullType
                gvFamily.CurrentRow = gvFamily.Rows(intCurrRow)
                'End If
            End If
        End If
    End Sub

    Private Sub gvEmpHis_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvEmpHis.CurrentColumnChanged
        If gvEmpHis.RowCount > 0 Then
            Dim intCurrRow As Integer = gvEmpHis.CurrentRow.Index
            If intCurrRow = gvEmpHis.Rows.Count - 1 Then
                gvEmpHis.Rows.AddNew()
                ' gvFamily.Rows(gvQualification.Rows.Count - 1).Cells(ColCourseType).Value = FullType
                gvEmpHis.CurrentRow = gvEmpHis.Rows(intCurrRow)
                'End If
            End If
        End If
    End Sub

    Private Sub gvDoc_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvDoc.CurrentColumnChanged
        'If gvDoc.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gvDoc.CurrentRow.Index
        '    If intCurrRow = gvDoc.Rows.Count - 1 Then
        '        gvDoc.Rows.AddNew()
        '        ' gvFamily.Rows(gvQualification.Rows.Count - 1).Cells(ColCourseType).Value = FullType
        '        gvDoc.CurrentRow = gvDoc.Rows(intCurrRow)
        '        'End If
        '    End If
        'End If
    End Sub
    Public Function GetPhoto(ByVal filePath As String) As Byte()
        Dim stream As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
        Dim reader As BinaryReader = New BinaryReader(stream)
        Dim photo() As Byte = reader.ReadBytes(CInt(stream.Length))
        reader.Close()
        stream.Close()
        Return photo
    End Function
    Private Sub btnPhotoBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPhotoBrowse.Click
        'OpenFileDialog1.Filter = " image |*.gif;  *.jpg;  *.bmp;  *.png;"
        'OpenFileDialog1.ShowDialog()
        ''If PictureBox1.Image <> Nothing Then
        'PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
        ''End If

        Try
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim FileName As String = OpenFileDialog1.FileName
                If clsCommon.myLen(FileName) > 0 Then
                    Using ms As New IO.MemoryStream(CType(GetPhoto(FileName), Byte()))
                        Dim img As Image = Image.FromStream(ms)
                        PicImage.Image = img

                        ''richa agarwal regarding memory leakage
                        ms.Close()
                        ms.Dispose()
                    End Using
                    File_Name = OpenFileDialog1.FileName
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub btnclearphoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclearphoto.Click
        PicImage.Image = Nothing
    End Sub

    Private Sub rbnrefbyAge_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnrefbyAge.CheckStateChanged
        If rbnrefbyAge.IsChecked = True Then
            txtRelation.Enabled = False
            txtEmpCode.Enabled = False
            txtAgency.Enabled = True
            txtEmpCode.Value = ""
            lblEmpName.Text = ""
            txtRelation.Value = ""
            lblRelation.Text = ""
        Else
            txtRelation.Enabled = True
            txtEmpCode.Enabled = True
            txtAgency.Enabled = False
        End If
    End Sub

    Private Sub rbnRefbyEmp_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnRefbyEmp.CheckStateChanged
        If rbnRefbyEmp.IsChecked = True Then
            txtRelation.Enabled = True
            txtEmpCode.Enabled = True
            txtAgency.Enabled = False
            txtAgency.Value = ""
            LblAgency.Text = ""
        Else
            txtRelation.Enabled = False
            txtEmpCode.Enabled = False
            txtAgency.Enabled = True
        End If
    End Sub

    Private Sub txtAgency__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtAgency._MYValidating
        Dim qry As String = ""
        qry = "Select Code,Name From Tspl_HR_Agency_Master"

        txtAgency.Value = clsCommon.ShowSelectForm("AGFND", qry, "Code", "", txtAgency.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtAgency.Value) > 0 Then
            LblAgency.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Name From Tspl_HR_Agency_Master Where CODE='" + txtAgency.Value + "'"))
        Else
            LblAgency.Text = ""
        End If
    End Sub


    Private Sub gvEmpHis_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvEmpHis.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvFamily_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvFamily.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvQualification_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvQualification.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    
    'Private Sub txtBloodGrp_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBloodGrp.KeyDown
    '    If Not Char.IsLetter(e.KeyData) And Not e.KeyCode = Chr(Keys.Delete) And Not e.KeyCode = Chr(Keys.Back) Then
    '        e.Handled = True
    '    End If
    'End Sub

    Private Sub txtBloodGrp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBloodGrp.KeyPress
        If Not Char.IsLetter(e.KeyChar) And Not e.KeyChar = Chr(Keys.Delete) And Not e.KeyChar = Chr(Keys.Back) And Not (e.KeyChar = "+") And Not (e.KeyChar = "-") Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbGender_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbGender.SelectedIndexChanged
        If ComboLoad = True Then
            If clsCommon.CompairString(cmbGender.SelectedValue, "M") = CompairStringResult.Equal Then
                SalAccToM = True
                SalAccToF = False
                GetSal()
            ElseIf clsCommon.CompairString(cmbGender.SelectedValue, "F") = CompairStringResult.Equal Then
                SalAccToF = True
                SalAccToM = False
                GetSal()
            End If
        End If

    End Sub
End Class
