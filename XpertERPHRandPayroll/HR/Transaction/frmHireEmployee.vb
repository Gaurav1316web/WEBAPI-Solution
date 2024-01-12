' ----------------- Created By Anubhooti On 20-Aug-2014 Against BM00000003528-------------------- '
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPEngine
Imports System.IO

Public Class FrmHireEmployee
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False

#Region "Functions"
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmHireEmployee)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtAppcode.MyReadOnly = True
        'btnsave.Enabled = True
        'btnDelete.Enabled = True
        'btnpost.Enabled = True
        isNewEntry = False

        Dim obj As ClsHireEmployee = ClsHireEmployee.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then

            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            btnpost.Enabled = True

            txtAppcode.Value = obj.Applicant_Code
            txtBank.Value = obj.Bank_Code
            TxtIFSCCode.Text = obj.IFSC_Code
            txtMonths.Value = obj.Probation_Month
            txtDays.Value = obj.Probation_Days
            txtAccNo.Value = obj.Bank_Account_No
            txtRemarks.Text = obj.Remarks
            txtPanNo.Text = obj.PAN_No
            If obj.Posted = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btnpost.Enabled = False
            End If
            UsLock1.Status = obj.Posted

            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()
        End If
    End Sub
    '' ------------------------------------ Nav. Query (=) --------------------------------------------------------
    Sub LoadDataForNav(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtAppcode.MyReadOnly = True

        isNewEntry = False

        Dim obj As ClsHireEmployee = ClsHireEmployee.GetDataForNav(strCode, NavTyep)
        If obj IsNot Nothing Then

            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            btnpost.Enabled = True

            txtAppcode.Value = obj.Applicant_Code
            txtBank.Value = obj.Bank_Code
            TxtIFSCCode.Text = obj.IFSC_Code
            txtMonths.Value = obj.Probation_Month
            txtDays.Value = obj.Probation_Days
            txtAccNo.Value = obj.Bank_Account_No
            txtRemarks.Text = obj.Remarks

            txtPanNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Pan_No,'') AS PanNo From TSPL_HR_APPLICANT_ENTRY Where APPLICANT_CODE ='" + obj.Applicant_Code + "'"))
            If obj.Posted = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btnpost.Enabled = False
            End If
            UsLock1.Status = obj.Posted

            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()
        End If
    End Sub
    '' ------------------------------------------------------------------------------------------------------------
    Sub funReset()
        isNewEntry = True
        txtAppcode.MyReadOnly = False
        txtAppcode.Value = Nothing
        txtAppcode.Focus()

        txtAppcode.Value = ""
        txtBank.Value = ""
        TxtIFSCCode.Text = 0
        txtMonths.Value = 0
        txtDays.Value = 0
        txtAccNo.Value = 0
        txtRemarks.Text = ""
        txtPanNo.Text = ""
        txtPanNo.ReadOnly = True
        UsLock1.Status = ERPTransactionStatus.Pending
        UcRequisitionDetail1.AppCode = ""
        UcRequisitionDetail1.RefreshData()

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnpost.Enabled = False
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            btnsave.Focus()
            If clsCommon.myLen(clsCommon.myCstr(txtAppcode.Value)) <= 0 Then
                txtAppcode.Focus()
                Throw New Exception("Applicant Code can not be left blank")
            ElseIf clsCommon.myLen(txtBank.Value) <= 0 Then
                txtBank.Focus()
                Throw New Exception("Bank Code can not be left blank")
            ElseIf clsCommon.myLen(txtAccNo.Value) <= 0 Or clsCommon.myCdbl(txtAccNo.Value) <= 0 Then
                txtAccNo.Focus()
                Throw New Exception("Account No can not be left blank or incorrect")
            ElseIf clsCommon.myLen(TxtIFSCCode.Text) <= 0 Then
                TxtIFSCCode.Focus()
                Throw New Exception("IFSC Code can not be left blank or incorrect")
                'ElseIf clsCommon.myLen(txtPanNo.Text) <= 0 Then
                '    txtPanNo.Focus()
                '    Throw New Exception("PAN No can not be left blank")
            ElseIf clsCommon.myCdbl(txtMonths.Value) <= 0 Or clsCommon.myCdbl(txtMonths.Value) > 12 Then
                txtMonths.Focus()
                Throw New Exception("Probation month can not be incorrect")
                'ElseIf clsCommon.myCdbl(txtDays.Value) <= 0 Then
                '    txtDays.Focus()
                '    Throw New Exception("Probation days can not be incorrect")
            End If
            If clsCommon.myLen(txtBank.Value) > 0 Then
                Dim BankCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) As Row from TSPL_Bank_MASTER where Bank_Code='" + clsCommon.myCstr(txtBank.Value) + "'"))
                If BankCode = 0 Then
                    Throw New Exception("Please check ! bank code does not exist")
                End If
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New ClsHireEmployee()
                obj.Applicant_Code = clsCommon.myCstr(txtAppcode.Value)
                obj.Bank_Code = clsCommon.myCstr(txtBank.Value)
                obj.IFSC_Code = clsCommon.myCstr(TxtIFSCCode.Text)
                obj.Probation_Month = clsCommon.myCdbl(txtMonths.Value)
                obj.Probation_Days = clsCommon.myCdbl(txtDays.Value)
                obj.PAN_No = clsCommon.myCstr(txtPanNo.Text)
                obj.Bank_Account_No = clsCommon.myCstr(txtAccNo.Value)
                obj.Remarks = clsCommon.myCstr(txtRemarks.Text)

                If (ClsHireEmployee.SaveData(obj, isNewEntry)) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                        LoadData(obj.Applicant_Code, NavigatorType.Current)
                        btnsave.Text = "Update"
                        btnpost.Enabled = True
                    End If
                Else
                    btnsave.Text = "Save"
                    btnpost.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim Applicant_Code As Integer = 0
            isFlag = True
            If clsCommon.myLen(txtAppcode.Value) > 0 Then
                Applicant_Code = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_HR_HIRE_EMPLOYEE where Applicant_Code='" + txtAppcode.Value + "'"))
                If Applicant_Code > 0 Then
                    If (myMessages.postConfirm()) Then
                        SaveData()
                        If (ClsHireEmployee.PostData(MyBase.Form_ID, txtAppcode.Value, clsCommon.myCstr(UcRequisitionDetail1.AppName))) Then
                            'If (ClsHireEmployee.EmpSaveAfterPost(clsCommon.myCstr(UcRequisitionDetail1.AppName), txtAppcode.Value)) Then
                            msg = "Successfully Posted"
                            common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                            LoadData(txtAppcode.Value, NavigatorType.Current)
                            'End If
                        End If
                    End If
                Else
                    Throw New Exception("You cannot post this entry before entering applicant code")
                End If
            Else
                Throw New Exception("Applicant code not found to Post")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
    End Sub
    'Public Sub EmpSaveAfterPost()
    '    If AllowToSave() Then
    '        Dim obj As New clsEmployeeMaster()
    '        obj.EMP_CODE = clsCommon.myCstr(txtAppcode.Value)
    '        obj.Emp_Name = clsCommon.myCstr(UcRequisitionDetail1.AppName)

    '        Dim objApp As New ClsApplicantEntry()
    '        objApp = ClsApplicantEntry.GetData(obj.EMP_CODE, Nothing)
    '        If (objApp IsNot Nothing AndAlso clsCommon.myLen(objApp.APPLICANT_CODE) > 0) Then

    '            Dim DeptCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(TSPL_HR_REQUISITION.DEPARTMENT_CODE,'') AS [Department Code] From  TSPL_HR_REQUISITION LEFT OUTER JOIN TSPL_HR_APPLICANT_ENTRY ON TSPL_HR_APPLICANT_ENTRY.Requisition_Code = TSPL_HR_REQUISITION.Requisition_Code WHERE TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE ='" + clsCommon.myCstr(objApp.APPLICANT_CODE) + "'"))
    '            obj.DEPARTMENT_CODE = clsCommon.myCstr(DeptCode)
    '            obj.Birth_date = clsCommon.GetPrintDate(objApp.Applicant_Date_Of_Birth, "dd/MM/yyyy")
    '            'obj.Joining_date = clsCommon.GetPrintDate(objApp, "dd/MM/yyyy")
    '            If clsCommon.CompairString(objApp.Gender, "M") = CompairStringResult.Equal Then
    '                obj.SEX = "Male"
    '            ElseIf clsCommon.CompairString(objApp.Gender, "F") = CompairStringResult.Equal Then
    '                obj.SEX = "Female"
    '            End If
    '            If clsCommon.CompairString(objApp.Maritial_Status, "M") = CompairStringResult.Equal Then
    '                obj.MARITAL_STATUS = "Married"
    '            ElseIf clsCommon.CompairString(objApp.Maritial_Status, "U") = CompairStringResult.Equal Then
    '                obj.MARITAL_STATUS = "Single"
    '            End If
    '            obj.LOCATION_CODE = clsCommon.myCstr(objApp.Location_Code)
    '            obj.BANK_ACC_NO = clsCommon.myCstr(objApp.Account_No)
    '            obj.BANK_CODE = clsCommon.myCstr(objApp.Bank_Code)
    '            obj.Add1 = clsCommon.myCstr(objApp.Add1)
    '            obj.Add2 = clsCommon.myCstr(objApp.Add2)
    '            obj.PRESENT_COUNTRY_CODE = clsCommon.myCstr(objApp.COUNTRY_CODE)
    '            obj.PRESENT_STATE_CODE = clsCommon.myCstr(objApp.State_Code)
    '            obj.PRESENT_CITY_CODE = clsCommon.myCstr(objApp.City_code)
    '            obj.Phone = clsCommon.myCstr(objApp.TELEPHONE_NO)
    '            obj.Pin_Code = clsCommon.myCstr(objApp.Pin_Code)
    '            obj.PAN_NO = clsCommon.myCstr(objApp.Pan_No)
    '            obj.DESCRIPTION = clsCommon.myCstr(objApp.Applicant_Description)
    '            obj.EMail_ID = clsCommon.myCstr(objApp.Email)

    '            Dim OfferDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Offer_Date From TSPL_HR_OFFER_LETTER Where APPLICANT_CODE ='" + clsCommon.myCstr(objApp.APPLICANT_CODE) + "'"))
    '            If clsCommon.myLen(OfferDate) > 0 Then
    '                obj.CONFIRMATION_DATE = OfferDate
    '            Else
    '                obj.CONFIRMATION_DATE = Nothing
    '            End If

    '            'obj.DEPARTMENT_CODE = clsCommon.myCstr(txtDepartment.Value)
    '            'obj.OCCUPATION_CODE = clsCommon.myCstr(txtOccupation.Value)
    '            '' Anubhooti 14-July-2014 
    '            'obj.PRESENT_MOBILE_NO = clsCommon.myCstr(txtPresentMobileNo.Text)

    '            'obj.FATHERS_NAME = clsCommon.myCstr(TxtFathersName.Text)
    '            'obj.MOTHERS_NAME = clsCommon.myCstr(txtMothersName.Text)
    '            'obj.SPOUSE_NAME = clsCommon.myCstr(txtSpouseName.Text)

    '            If (obj.SaveData(obj, True)) Then
    '                ' common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
    '                'LoadData(obj.EMP_CODE, NavigatorType.Current)
    '            End If
    '        End If
    '    End If
    'End Sub
#End Region
    Private Sub txtBank__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBank._MYValidating
        txtBank.Value = clsCommon.myCstr(clsBankMaster.getFinder("", txtBank.Value, isButtonClicked))
        If clsCommon.myLen(txtBank.Value) > 0 Then
            lblBankName.Text = clsDBFuncationality.getSingleValue("Select description from TSPL_Bank_MASTER where Bank_Code='" + txtBank.Value + "'")
        Else
            lblBankName.Text = ""
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub


    Private Sub btnpost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpost.Click
        PostData()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

   
    Private Sub txtAppcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtAppcode._MYNavigator
        Try
            'LoadData(txtAppcode.Value, NavType)
            Dim obj As New clsJoiningCheckListHead()

            Dim qst As String = "select count(*) from TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE='" + txtAppcode.Value + "' AND Posted =1"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtAppcode.MyReadOnly = False
            Else
                txtAppcode.MyReadOnly = True
            End If
            Dim ISAppCode As Integer
            Dim AppCode As String = clsCommon.myCstr(txtAppcode.Value)
            obj = clsJoiningCheckListHead.GetPostedData(AppCode, NavType)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.ApplicantCode) > 0) Then
                ISAppCode = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) From TSPL_HR_HIRE_EMPLOYEE WHERE APPLICANT_CODE='" + obj.ApplicantCode + "'"))
                txtAppcode.Value = clsCommon.myCstr(obj.ApplicantCode)
                UcRequisitionDetail1.AppCode = obj.ApplicantCode
                txtPanNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Pan_No,'') AS PanNo From TSPL_HR_APPLICANT_ENTRY Where APPLICANT_CODE ='" + obj.ApplicantCode + "'"))
                UcRequisitionDetail1.RefreshData()
                If ISAppCode > 0 Then
                    LoadDataForNav(txtAppcode.Value, NavType)
                Else
                    isNewEntry = True
                    txtAppcode.MyReadOnly = False
                    txtAppcode.Focus()
                    txtBank.Value = ""
                    TxtIFSCCode.Text = 0
                    txtMonths.Value = 0
                    txtDays.Value = 0
                    txtAccNo.Value = 0
                    txtRemarks.Text = ""
                    'txtPanNo.Text = ""

                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnsave.Text = "Save"
                    btnsave.Enabled = True
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtAppcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtAppcode._MYValidating
        'Dim str As String = "select count(*) from TSPL_HR_HIRE_EMPLOYEE where APPLICANT_CODE ='" + txtAppcode.Value + "' "
        'Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'If no = 0 AndAlso isButtonClicked = False Then
        '    txtAppcode.MyReadOnly = False
        'Else
        '    txtAppcode.MyReadOnly = True
        'End If

        ' If txtAppcode.MyReadOnly OrElse isButtonClicked Then
        'Dim qry As String = "SELECT APPLICANT_CODE AS Code  FROM TSPL_HR_JOINING_HEAD  "
        'txtAppcode.Value = clsCommon.ShowSelectForm("App_Code", qry, "Code", " Posted = 1 ", txtAppcode.Value, "", isButtonClicked)
        txtAppcode.Value = clsJoiningCheckListHead.GetFinder(" ", txtAppcode.Value, isButtonClicked)
        If clsCommon.myLen(txtAppcode.Value) > 0 Then
            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()
            LoadData(txtAppcode.Value, NavigatorType.Current)
        Else
            UcRequisitionDetail1.AppCode = ""
            UcRequisitionDetail1.RefreshData()
            funReset()
        End If
        'Else
        'UcRequisitionDetail1.AppCode = ""
        'UcRequisitionDetail1.RefreshData()
        'funReset()
        'End If
    End Sub

    Private Sub FrmHireEmployee_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            SaveData()
            ' ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnpost.Enabled Then
            PostData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmHireEmployee_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnpost, "Press Alt+P Post Trasnaction")
        funReset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
End Class

