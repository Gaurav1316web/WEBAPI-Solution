' ----------------- Created By Anubhooti On 20-Aug-2014 Against BM00000003526-------------------- '
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
Imports System.IO
Imports XpertERPEngine


Public Class FrmReferenceCheck
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False
    Dim IsComboLoad As Boolean = False

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmReferenceCheck)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnpost.Visible = MyBase.isPostFlag
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtAppcode.MyReadOnly = True
        'btnsave.Enabled = True
        'btnDelete.Enabled = True
        'btnpost.Enabled = True
        isNewEntry = False
        'funReset()
        Dim obj As ClsReferenceCheck = ClsReferenceCheck.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            btnDelete.Enabled = True
            btnpost.Enabled = True

            txtAppcode.Value = obj.Applicant_Code
            txtremarks.Text = obj.Remarks
            TxtInitiateBy.Value = obj.Initiate_By
            If clsCommon.myLen(TxtInitiateBy.Value) > 0 Then
                LblInitiateBy.Text = clsDBFuncationality.getSingleValue("Select isnull(Emp_Name,'') As Emp_Name From TSPL_Employee_Master Where EMP_CODE ='" + TxtInitiateBy.Value + "'")
            Else
                LblInitiateBy.Text = ""
            End If
            cmbMOCPast.SelectedValue = obj.Past_Mode_Of_Check
            cmbFBPast.SelectedValue = obj.Past_Category_Feedback
            txtRemarksPast.Text = obj.Past_Feedback_Remarks
            cmbMOCCand.SelectedValue = obj.Cand_Mode_Of_Check
            cmbFBCand.SelectedValue = obj.Cand_Category_Feedback
            txtRemarksCand.Text = obj.Cand_Feedback_Remarks
            cmbFinalFeedback.SelectedValue = obj.Final_Feedback
            txtremarks.Text = obj.Remarks
            dtpDate.Value = obj.Ref_Date
            If clsCommon.CompairString(cmbFinalFeedback.SelectedValue, "N") = CompairStringResult.Equal Then
                btnpost.Enabled = False
                UsLock1.Visible = False
                LblStatus.Visible = True
            Else
                btnpost.Enabled = True
                UsLock1.Visible = True
                LblStatus.Visible = False
            End If
            If obj.Posted = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btnpost.Enabled = False
                btnDelete.Enabled = False
            End If
            UsLock1.Status = obj.Posted
            ReferenceDetails()
            If clsCommon.myCdbl(obj.Is_Override) = 0 Then
                ChkOverride.Checked = False
                grpmain.Enabled = True
                grpOvride.Enabled = False
            Else
                ChkOverride.Checked = True
                grpOvride.Enabled = True
                grpmain.Enabled = False
            End If
            If clsCommon.myCdbl(obj.Is_PastDetail) = 0 Then
                ChkPastEmp.Checked = False
                grpPastEmp.Enabled = False
            Else
                ChkPastEmp.Checked = True
                grpPastEmp.Enabled = True
            End If
            If clsCommon.myCdbl(obj.Is_CandidateDetail) = 0 Then
                chkDetailsCand.Checked = False
                grpCand.Enabled = False
            Else
                chkDetailsCand.Checked = True
                grpCand.Enabled = True
            End If
            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()
            Dim BothSaved As Integer = clsDBFuncationality.getSingleValue("Select COUNT (*) As Row From TSPL_HR_REFERENCE_CHECK Where Is_PastDetail =1 AND Is_CandidateDetail =1 AND APPLICANT_CODE ='" + txtAppcode.Value + "'")
            If BothSaved > 0 Then
                cmbFinalFeedback.Enabled = True
            Else
                cmbFinalFeedback.Enabled = False
            End If
            'txtAppcode.MyReadOnly = True
            'btnsave.Text = "Update"
            'btnpost.Enabled = True
            'btnDelete.Enabled = True
        End If
    End Sub
    '' ------------------------------------- Nav. Query(=) ---------------------------------------------------------------
    Sub LoadDataForNav(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtAppcode.MyReadOnly = True
        isNewEntry = False
        Dim obj As ClsReferenceCheck = ClsReferenceCheck.GetDataForNav(strCode, NavTyep)
        If obj IsNot Nothing Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            btnDelete.Enabled = True
            btnpost.Enabled = True

            txtAppcode.Value = obj.Applicant_Code
            txtremarks.Text = obj.Remarks
            TxtInitiateBy.Value = obj.Initiate_By
            If clsCommon.myLen(TxtInitiateBy.Value) > 0 Then
                LblInitiateBy.Text = clsDBFuncationality.getSingleValue("Select isnull(Emp_Name,'') As Emp_Name From TSPL_Employee_Master Where EMP_CODE ='" + TxtInitiateBy.Value + "'")
            Else
                LblInitiateBy.Text = ""
            End If
            cmbMOCPast.SelectedValue = obj.Past_Mode_Of_Check
            cmbFBPast.SelectedValue = obj.Past_Category_Feedback
            txtRemarksPast.Text = obj.Past_Feedback_Remarks
            cmbMOCCand.SelectedValue = obj.Cand_Mode_Of_Check
            cmbFBCand.SelectedValue = obj.Cand_Category_Feedback
            txtRemarksCand.Text = obj.Cand_Feedback_Remarks
            cmbFinalFeedback.SelectedValue = obj.Final_Feedback
            txtremarks.Text = obj.Remarks
            dtpDate.Value = obj.Ref_Date
            If clsCommon.CompairString(cmbFinalFeedback.SelectedValue, "N") = CompairStringResult.Equal Then
                btnpost.Enabled = False
                UsLock1.Visible = False
                LblStatus.Visible = True
            Else
                btnpost.Enabled = True
                UsLock1.Visible = True
                LblStatus.Visible = False
            End If
            If obj.Posted = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btnpost.Enabled = False
                btnDelete.Enabled = False
            End If
            UsLock1.Status = obj.Posted
            
            ReferenceDetails()
            If clsCommon.myCdbl(obj.Is_Override) = 0 Then
                ChkOverride.Checked = False
                grpmain.Enabled = True
                grpOvride.Enabled = False
            Else
                ChkOverride.Checked = True
                grpOvride.Enabled = True
                grpmain.Enabled = False
            End If
            If clsCommon.myCdbl(obj.Is_PastDetail) = 0 Then
                ChkPastEmp.Checked = False
                grpPastEmp.Enabled = False
            Else
                ChkPastEmp.Checked = True
                grpPastEmp.Enabled = True
            End If
            If clsCommon.myCdbl(obj.Is_CandidateDetail) = 0 Then
                chkDetailsCand.Checked = False
                grpCand.Enabled = False
            Else
                chkDetailsCand.Checked = True
                grpCand.Enabled = True
            End If
            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()
            Dim BothSaved As Integer = clsDBFuncationality.getSingleValue("Select COUNT (*) As Row From TSPL_HR_REFERENCE_CHECK Where Is_PastDetail =1 AND Is_CandidateDetail =1 AND APPLICANT_CODE ='" + txtAppcode.Value + "'")
            If BothSaved > 0 Then
                cmbFinalFeedback.Enabled = True
            Else
                cmbFinalFeedback.Enabled = False
            End If
           
        End If
    End Sub
    '' ------------------------------------- Nav. Query(=) ---------------------------------------------------------------
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtAppcode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow(Me, "are you sure? do you want to delete this Code ('" + txtAppcode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_HR_REFERENCE_CHECK WHERE Applicant_Code='" + txtAppcode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow(Me, "Deleted Successfully", Me.Text)
                funReset()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "Current Code is in use", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub
    Sub funReset()
        isNewEntry = True
        txtAppcode.MyReadOnly = False
        txtAppcode.Value = Nothing
        txtAppcode.Focus()
        UsLock1.Status = ERPTransactionStatus.Pending
        rbnrefbyAge.ReadOnly = True
        rbnRefbyEmp.ReadOnly = True
        rbnrefbyAge.IsChecked = False
        rbnRefbyEmp.IsChecked = False
        txtAppcode.Value = ""
        txtEmpCode.Value = ""
        txtRelation.Value = ""
        lblEmpName.Text = ""
        lblRelation.Text = ""
        txtremarks.Text = ""
        TxtInitiateBy.Value = ""
        LblInitiateBy.Text = ""
        txtRemarksPast.Text = ""
        txtRemarksCand.Text = ""
        ChkOverride.Checked = False
        grpOvride.Enabled = False
        chkDetailsCand.Checked = False
        grpCand.Enabled = False
        ChkPastEmp.Checked = False
        grpPastEmp.Enabled = False
        cmbFinalFeedback.Enabled = False

        Me.cmbMOCPast.DataSource = ClsReferenceCheck.GetMOC
        Me.cmbMOCPast.DisplayMember = "Name"
        Me.cmbMOCPast.ValueMember = "Code"

        Me.cmbMOCCand.DataSource = ClsReferenceCheck.GetMOC
        Me.cmbMOCCand.DisplayMember = "Name"
        Me.cmbMOCCand.ValueMember = "Code"

        Me.cmbFBCand.DataSource = ClsReferenceCheck.GetFeedback
        Me.cmbFBCand.DisplayMember = "Name"
        Me.cmbFBCand.ValueMember = "Code"

        Me.cmbFBPast.DataSource = ClsReferenceCheck.GetFeedback
        Me.cmbFBPast.DisplayMember = "Name"
        Me.cmbFBPast.ValueMember = "Code"

        Me.cmbFinalFeedback.DataSource = ClsReferenceCheck.GetFinalFeedback
        Me.cmbFinalFeedback.DisplayMember = "Name"
        Me.cmbFinalFeedback.ValueMember = "Code"

        UcRequisitionDetail1.AppCode = ""
        UcRequisitionDetail1.RefreshData()

        dtpDate.Value = clsCommon.GETSERVERDATE()
        UsLock1.Visible = True
        LblStatus.Visible = False
        IsComboLoad = True
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnpost.Enabled = False
        btnDelete.Enabled = False
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtAppcode.Value)) <= 0 Then
                txtAppcode.Focus()
                Throw New Exception("Code can not be left blank or incorrect")
            End If
            If ChkOverride.Checked = True Then
                If clsCommon.myLen(txtremarks.Text) <= 0 Then
                    txtremarks.Focus()
                    Throw New Exception("Remarks can not be left blank or incorrect")
                End If
            ElseIf ChkOverride.Checked = False Then
                If clsCommon.myLen(TxtInitiateBy.Value) <= 0 Then
                    TxtInitiateBy.Focus()
                    Throw New Exception("Initiate By can not be left blank or incorrect")
                End If
                If ChkPastEmp.Checked = False Then
                    ChkPastEmp.Focus()
                    Throw New Exception("Please fill past employement details.")
                End If
                If chkDetailsCand.Checked = False Then
                    chkDetailsCand.Focus()
                    Throw New Exception("Please fill candidate details.")
                End If
            End If
            '' Past Employement
            If ChkPastEmp.Checked = True Then
                If clsCommon.myLen(cmbMOCPast.Text) <= 0 Then
                    cmbMOCPast.Focus()
                    Throw New Exception("Mode Of Check Of Past Employement can not be left blank")
                End If
                If clsCommon.myLen(cmbFBPast.Text) <= 0 Then
                    cmbFBPast.Focus()
                    Throw New Exception("Category Feedback Of Past Employement can not be left blank")
                End If
                If clsCommon.myLen(txtRemarksPast.Text) <= 0 Then
                    txtRemarksPast.Focus()
                    Throw New Exception("Feedback Remarks Of Past Employement can not be left blank")
                End If
            End If

            '' Candidate Details
            If chkDetailsCand.Checked = True Then
                If clsCommon.myLen(cmbMOCCand.Text) <= 0 Then
                    cmbMOCCand.Focus()
                    Throw New Exception("Mode Of Check Of Candidate Details can not be left blank")
                End If
                If clsCommon.myLen(cmbFBCand.Text) <= 0 Then
                    cmbFBCand.Focus()
                    Throw New Exception("Category Feedback Of Candidate Details can not be left blank")
                End If
                If clsCommon.myLen(txtRemarksCand.Text) <= 0 Then
                    txtRemarksCand.Focus()
                    Throw New Exception("Feedback Remarks Of Candidate Details can not be left blank")
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
                Dim obj As New ClsReferenceCheck()
                obj.Applicant_Code = clsCommon.myCstr(txtAppcode.Value)
                obj.Remarks = clsCommon.myCstr(txtremarks.Text)
                obj.Past_Feedback_Remarks = clsCommon.myCstr(txtRemarksPast.Text)
                obj.Past_Mode_Of_Check = clsCommon.myCstr(cmbMOCPast.SelectedValue)
                obj.Past_Category_Feedback = clsCommon.myCstr(cmbFBPast.SelectedValue)
                obj.Cand_Mode_Of_Check = clsCommon.myCstr(cmbMOCCand.SelectedValue)
                obj.Cand_Feedback_Remarks = clsCommon.myCstr(txtRemarksCand.Text)
                obj.Cand_Category_Feedback = clsCommon.myCstr(cmbFBCand.SelectedValue)
                obj.Final_Feedback = clsCommon.myCstr(cmbFinalFeedback.SelectedValue)
                obj.Initiate_By = clsCommon.myCstr(TxtInitiateBy.Value)
                obj.Ref_Date = dtpDate.Value
                If ChkOverride.Checked = True Then
                    obj.Is_Override = 1
                Else
                    obj.Is_Override = 0
                End If
                If ChkPastEmp.Checked = True Then
                    obj.Is_PastDetail = 1
                Else
                    obj.Is_PastDetail = 0
                End If
                If chkDetailsCand.Checked = True Then
                    obj.Is_CandidateDetail = 1
                Else
                    obj.Is_CandidateDetail = 0
                End If
                'Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Relation_Code) from TSPL_HR_RELATION_MASTER where Relation_Code='" + obj.Relation_Code + "'")
                'If (qry = 0) Then
                '    isNewEntry = True
                'Else
                '    isNewEntry = False
                'End If
                If (ClsReferenceCheck.SaveData(obj, isNewEntry)) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                        LoadData(obj.Applicant_Code, NavigatorType.Current)
                        btnsave.Text = "Update"
                        ' btnpost.Enabled = True
                    End If
                Else
                    btnsave.Text = "Save"
                    '  btnpost.Enabled = False
                End If
            End If
        Catch ex As Exception
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim Applicant_Code As Double = 0
            isFlag = True
            If clsCommon.myLen(txtAppcode.Value) > 0 Then
                Applicant_Code = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(Applicant_Code) As Applicant_Code  from TSPL_HR_REFERENCE_CHECK where Applicant_Code='" + txtAppcode.Value + "'"))
                If Applicant_Code > 0 Then
                    If (myMessages.postConfirm()) Then
                        SaveData()
                        If (ClsReferenceCheck.PostData(MyBase.Form_ID, txtAppcode.Value)) Then
                            msg = "Successfully Posted"
                            common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                            LoadData(txtAppcode.Value, NavigatorType.Current)
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
    Private Sub ChkOverride_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ChkOverride.Checked = True Then
            grpOvride.Enabled = True
            grpPastEmp.Enabled = False
            grpCand.Enabled = False
        Else
            grpOvride.Enabled = False
            grpPastEmp.Enabled = True
            grpCand.Enabled = True
        End If
    End Sub

    Private Sub txtAppcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtAppcode._MYNavigator
        Try
            'LoadData(txtAppcode.Value, NavType)
            Dim obj As New clsOfferChkListHead()
            Dim ISAppCode As Integer
            Dim AppCode As String = clsCommon.myCstr(txtAppcode.Value)
            obj = clsOfferChkListHead.GetPostedData(AppCode, NavType)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.ApplicantCode) > 0) Then
                ISAppCode = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) From TSPL_HR_REFERENCE_CHECK WHERE APPLICANT_CODE='" + obj.ApplicantCode + "'"))
                txtAppcode.Value = clsCommon.myCstr(obj.ApplicantCode)
                UcRequisitionDetail1.AppCode = obj.ApplicantCode
                UcRequisitionDetail1.RefreshData()
                If ISAppCode > 0 Then
                    LoadDataForNav(txtAppcode.Value, NavType)
                Else
                    isNewEntry = True
                    txtAppcode.MyReadOnly = False
                    txtAppcode.Focus()
                    rbnrefbyAge.ReadOnly = True
                    rbnRefbyEmp.ReadOnly = True
                    rbnrefbyAge.IsChecked = False
                    rbnRefbyEmp.IsChecked = False
                    txtEmpCode.Value = ""
                    txtRelation.Value = ""
                    lblEmpName.Text = ""
                    lblRelation.Text = ""
                    txtremarks.Text = ""
                    TxtInitiateBy.Value = ""
                    LblInitiateBy.Text = ""
                    txtRemarksPast.Text = ""
                    txtRemarksCand.Text = ""
                    ChkOverride.Checked = False
                    grpOvride.Enabled = False
                    chkDetailsCand.Checked = False
                    grpCand.Enabled = False
                    ChkPastEmp.Checked = False
                    grpPastEmp.Enabled = False
                    cmbFinalFeedback.Enabled = False

                    Me.cmbMOCPast.DataSource = ClsReferenceCheck.GetMOC
                    Me.cmbMOCPast.DisplayMember = "Name"
                    Me.cmbMOCPast.ValueMember = "Code"

                    Me.cmbMOCCand.DataSource = ClsReferenceCheck.GetMOC
                    Me.cmbMOCCand.DisplayMember = "Name"
                    Me.cmbMOCCand.ValueMember = "Code"

                    Me.cmbFBCand.DataSource = ClsReferenceCheck.GetFeedback
                    Me.cmbFBCand.DisplayMember = "Name"
                    Me.cmbFBCand.ValueMember = "Code"

                    Me.cmbFBPast.DataSource = ClsReferenceCheck.GetFeedback
                    Me.cmbFBPast.DisplayMember = "Name"
                    Me.cmbFBPast.ValueMember = "Code"

                    Me.cmbFinalFeedback.DataSource = ClsReferenceCheck.GetFinalFeedback
                    Me.cmbFinalFeedback.DisplayMember = "Name"
                    Me.cmbFinalFeedback.ValueMember = "Code"

                    dtpDate.Value = clsCommon.GETSERVERDATE()
                    UsLock1.Visible = True
                    LblStatus.Visible = False
                    IsComboLoad = True
                    btnsave.Text = "Save"
                    btnsave.Enabled = True
                    btnpost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtAppcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtAppcode._MYValidating
        'Dim str As String = "select count(*) from TSPL_HR_REFERENCE_CHECK where APPLICANT_CODE ='" + txtAppcode.Value + "' "
        'Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'If no = 0 AndAlso isButtonClicked = False Then
        '    txtAppcode.MyReadOnly = False
        'Else
        '    txtAppcode.MyReadOnly = True
        'End If

        'If txtAppcode.MyReadOnly OrElse isButtonClicked Then
        '    Dim qry As String = "SELECT APPLICANT_CODE AS Code, Applicant_Date  FROM TSPL_HR_OFFER_CHECK_LIST_HEAD  "
        '    txtAppcode.Value = clsCommon.ShowSelectForm("App_Code", qry, "Code", "Posted = 1", txtAppcode.Value, "", isButtonClicked)
        '    If clsCommon.myLen(txtAppcode.Value) > 0 Then
        '        ReferenceDetails()
        '        LoadData(txtAppcode.Value, NavigatorType.Current)
        '        UcRequisitionDetail1.AppCode = txtAppcode.Value
        '        UcRequisitionDetail1.RefreshData()
        '    Else
        '        UcRequisitionDetail1.AppCode = ""
        '        UcRequisitionDetail1.RefreshData()
        '        funReset()
        '    End If
        'Else
        '    UcRequisitionDetail1.AppCode = ""
        '    UcRequisitionDetail1.RefreshData()
        '    funReset()
        'End If
        'Dim qry As String = "SELECT APPLICANT_CODE AS Code, Applicant_Date  FROM TSPL_HR_OFFER_CHECK_LIST_HEAD  "
        'txtAppcode.Value = clsCommon.ShowSelectForm("App_Code", qry, "Code", "Posted = 1", txtAppcode.Value, "", isButtonClicked)
        txtAppcode.Value = clsOfferChkListHead.GetFinder(" ", txtAppcode.Value, isButtonClicked)
        If clsCommon.myLen(txtAppcode.Value) > 0 Then
            ReferenceDetails()
            LoadData(txtAppcode.Value, NavigatorType.Current)
            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()
        Else
            UcRequisitionDetail1.AppCode = ""
            UcRequisitionDetail1.RefreshData()
            funReset()
        End If
    End Sub

    Sub ReferenceDetails()
        Dim dt As DataTable

        dt = clsDBFuncationality.GetDataTable("Select Emp_Refrence,Agency_Refrence,Emp_Code,Relation_Code,Agency_Code From TSPL_HR_APPLICANT_ENTRY  Where APPLICANT_CODE='" + txtAppcode.Value + "'")
        If dt.Rows.Count > 0 Then
            If clsCommon.myCdbl(dt.Rows(0).Item("Emp_Refrence")) = 1 Then
                rbnRefbyEmp.IsChecked = True
                LblRefBy.Text = "Emp Code"
                txtEmpCode.Value = clsCommon.myCstr(dt.Rows(0).Item("Emp_Code"))
                Dim Emp_Name As String = clsDBFuncationality.getSingleValue("Select ISnull(Emp_Name,'') As Emp_Name From TSPL_EMPLOYEE_MASTER Where EMP_CODE='" + txtEmpCode.Value + "'")
                If clsCommon.myLen(Emp_Name) > 0 Then
                    lblEmpName.Text = Emp_Name
                End If
                txtRelation.Value = clsCommon.myCstr(dt.Rows(0).Item("Relation_Code"))
                Dim Relation_Name As String = clsDBFuncationality.getSingleValue("Select ISnull(Relation_Name,'') As Relation_Name From TSPL_HR_RELATION_MASTER Where Relation_Code='" + txtRelation.Value + "'")
                If clsCommon.myLen(Relation_Name) > 0 Then
                    lblRelation.Text = Relation_Name
                End If
            ElseIf clsCommon.myCdbl(dt.Rows(0).Item("Agency_Refrence")) = 1 Then
                rbnrefbyAge.IsChecked = True
                LblRefBy.Text = "Agency Code"
                txtEmpCode.Value = clsCommon.myCstr(dt.Rows(0).Item("Agency_Code"))
                Dim Agency_Name As String = clsDBFuncationality.getSingleValue("Select ISnull(Name,'') As Name From Tspl_Agency_Master Where Code='" + txtEmpCode.Value + "'")
                If clsCommon.myLen(Agency_Name) > 0 Then
                    lblEmpName.Text = Agency_Name
                End If
                txtRelation.Enabled = False
            End If
            
        End If
    End Sub
    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnpost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpost.Click
        PostData()
    End Sub

    Private Sub ChkPastEmp_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPastEmp.CheckStateChanged
        If ChkPastEmp.Checked = True Then
            grpPastEmp.Enabled = True
            txtRemarksPast.MendatroryField = True
            If chkDetailsCand.Checked = True Then
                cmbFinalFeedback.Enabled = True
                If clsCommon.CompairString(UsLock1.Status, "1") <> CompairStringResult.Equal Then
                    If clsCommon.CompairString(cmbFinalFeedback.SelectedValue, "N") = CompairStringResult.Equal Then
                        btnpost.Enabled = False
                    Else
                        btnpost.Enabled = True
                    End If
                End If
            Else
                cmbFinalFeedback.Enabled = False
            End If
        Else
            grpPastEmp.Enabled = False
            cmbFinalFeedback.Enabled = False
            txtRemarksPast.MendatroryField = False
            txtRemarksPast.Text = ""
        End If
    End Sub

    Private Sub chkDetailsCand_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDetailsCand.CheckStateChanged
        If chkDetailsCand.Checked = True Then
            grpCand.Enabled = True
            txtRemarksCand.MendatroryField = True
            If ChkPastEmp.Checked = True Then
                cmbFinalFeedback.Enabled = True
                If clsCommon.CompairString(UsLock1.Status, "1") <> CompairStringResult.Equal Then
                    If clsCommon.CompairString(cmbFinalFeedback.SelectedValue, "N") = CompairStringResult.Equal Then
                        btnpost.Enabled = False
                    Else
                        btnpost.Enabled = True
                    End If
                End If
            Else
                cmbFinalFeedback.Enabled = False
            End If
        Else
            grpCand.Enabled = False
            cmbFinalFeedback.Enabled = False
            txtRemarksCand.MendatroryField = False
            txtRemarksCand.Text = ""
        End If
    End Sub

    Private Sub FrmReferenceCheck_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnpost.Enabled Then
            PostData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmReferenceCheck_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnpost, "Press Alt+P Post Transaction")
        funReset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub ChkOverride_CheckStateChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkOverride.CheckStateChanged
        If ChkOverride.Checked = True Then
            grpOvride.Enabled = True
            grpmain.Enabled = False
            txtEmpCode.Value = ""
            txtRelation.Value = ""
            TxtInitiateBy.Value = ""
            txtRemarksCand.Text = ""
            txtRemarksPast.Text = ""
            lblEmpName.Text = ""
            LblInitiateBy.Text = ""
            lblEmpName.Text = ""
            lblRelation.Text = ""
            chkDetailsCand.Checked = False
            ChkPastEmp.Checked = False
            txtremarks.MendatroryField = True
            btnpost.Enabled = True
        Else
            txtremarks.MendatroryField = False
            grpmain.Enabled = True
            grpOvride.Enabled = False
            btnpost.Enabled = False
        End If
    End Sub

    Private Sub TxtInitiateBy__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles TxtInitiateBy._MYValidating
        Dim qry As String = ""
        qry = "select EMP_CODE as [Code],Emp_Name as [Employee Name],FATHERS_NAME as [Fathers Name],MOTHERS_NAME as [Mothers Name],convert(varchar(12),Birth_date,103) as [Date of Birth],SEX as [Sex],MARITAL_STATUS as [Marital Status],SPOUSE_NAME as [Spouse Name]," & _
           "Designation as [Designation],OCCUPATION_CODE as [Occupation],DEPARTMENT_CODE as [Department] From TSPL_EMPLOYEE_MASTER "
        TxtInitiateBy.Value = clsCommon.ShowSelectForm("EMPFND", qry, "Code", "", TxtInitiateBy.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtEmpCode.Value) > 0 Then
            LblInitiateBy.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Emp_Name as [Employee Name] FROM TSPL_EMPLOYEE_MASTER Where EMP_CODE='" + TxtInitiateBy.Value + "'"))
        Else
            LblInitiateBy.Text = ""
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub cmbFinalFeedback_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbFinalFeedback.SelectedIndexChanged
        If IsComboLoad = True Then
            If clsCommon.CompairString(UsLock1.Status, "1") <> CompairStringResult.Equal Then
                If clsCommon.CompairString(cmbFinalFeedback.SelectedValue, "N") = CompairStringResult.Equal Then

                    btnpost.Enabled = False
                Else
                    btnpost.Enabled = True
                End If
            End If
        End If
    End Sub
End Class
