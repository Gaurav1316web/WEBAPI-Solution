' ----------------- Created By Anubhooti On 20-Aug-2014 Against BM00000003527-------------------- '
'Ticket No- BHA/24/09/18-000565 ,all payheads in Grid
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

Public Class FrmSalaryFitment
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False
    Const colSelect As String = "SELECT"
    Const colPHCode As String = "PHCODE"
    Const colPHName As String = "PHNAME"

#Region "FUNCTIONS"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSalaryFitment)
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

        Dim obj As ClsSalaryFitment = ClsSalaryFitment.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then

            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            btnDelete.Enabled = True
            btnpost.Enabled = True

            txtAppcode.Value = obj.Applicant_Code
            txtCTC.Value = obj.CTC_Rs_Month
            txtFixedCTC.Value = obj.Fixed_CTC_Rs_Month
            txtVariableAmt.Value = obj.Variable_Amount
            txtVariablePay.Value = obj.Variable_Pay_Percentage

            For i As Integer = 0 To obj.arrPayHead.Count - 1
                For Each grow As GridViewRowInfo In dgv.Rows
                    If (clsCommon.myCstr(grow.Cells(colPHCode).Value) = clsCommon.myCstr(obj.arrPayHead(i))) Then
                        grow.Cells(colSelect).Value = True
                    End If
                Next
            Next

            If obj.Posted = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btnpost.Enabled = False
                btnDelete.Enabled = False
            End If
            UsLock1.Status = obj.Posted

            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()

        End If
    End Sub
    '' -------------------------------------------------- Nav. Query ------------------------------------------------------------------
    Sub LoadDataForNav(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtAppcode.MyReadOnly = True
        'btnsave.Enabled = True
        'btnDelete.Enabled = True
        'btnpost.Enabled = True
        isNewEntry = False

        Dim obj As ClsSalaryFitment = ClsSalaryFitment.GetDataForNav(strCode, NavTyep)
        If obj IsNot Nothing Then

            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            btnDelete.Enabled = True
            btnpost.Enabled = True

            txtAppcode.Value = obj.Applicant_Code
            txtCTC.Value = obj.CTC_Rs_Month
            txtFixedCTC.Value = obj.Fixed_CTC_Rs_Month
            txtVariableAmt.Value = obj.Variable_Amount
            txtVariablePay.Value = obj.Variable_Pay_Percentage

            For i As Integer = 0 To obj.arrPayHead.Count - 1
                For Each grow As GridViewRowInfo In dgv.Rows
                    If (clsCommon.myCstr(grow.Cells(colPHCode).Value) = clsCommon.myCstr(obj.arrPayHead(i))) Then
                        grow.Cells(colSelect).Value = True
                    End If
                Next
            Next

            If obj.Posted = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btnpost.Enabled = False
                btnDelete.Enabled = False
            End If
            UsLock1.Status = obj.Posted

            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()

        End If
    End Sub
    '' --------------------------------------------------------------------------------------------------------------------------------
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtAppcode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Are you sure? do you want to delete this Code ('" + txtAppcode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry1 As String = "delete from TSPL_FITMENT_PAYHEAD_MAPPING where APPLICANT_CODE='" + txtAppcode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry1)

                Dim qry As String = "DELETE FROM TSPL_HR_SALARY_FITMENT WHERE Applicant_Code='" + txtAppcode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow(Me, "Deleted Successfully", Me.Text)
                funReset()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "Current Code is in use")
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

        txtCTC.Value = 0
        txtFixedCTC.Value = 0
        txtVariableAmt.Value = 0
        txtVariablePay.Value = 0
        UsLock1.Status = ERPTransactionStatus.Pending
        UcRequisitionDetail1.AppCode = ""
        UcRequisitionDetail1.RefreshData()

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnpost.Enabled = False
        btnDelete.Enabled = False
        For i As Integer = 0 To dgv.Rows.Count - 1
            If dgv.Rows(i).Cells(colSelect).Value = True Then
                dgv.Rows(i).Cells(colSelect).Value = False
            End If
        Next
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtAppcode.Value)) <= 0 Then
                txtAppcode.Focus()
                Throw New Exception("Code can not be left blank")
            ElseIf clsCommon.myLen(txtCTC.Value) <= 0 Or clsCommon.myCdbl(txtCTC.Value) <= 0 Then
                txtCTC.Focus()
                Throw New Exception("CTC can not be left blank or incorrect")
            ElseIf clsCommon.myLen(txtVariablePay.Value) <= 0 Or clsCommon.myCdbl(txtVariablePay.Value) <= 0 Then
                txtVariablePay.Focus()
                Throw New Exception("Variable pay percentage can not be left blank or incorrect")
            ElseIf clsCommon.myCdbl(txtVariablePay.Value) > 100 Then
                txtVariablePay.Focus()
                Throw New Exception("Variable pay percentage can not be more than 100")
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New ClsSalaryFitment()
                obj.Applicant_Code = clsCommon.myCstr(txtAppcode.Value)
                obj.CTC_Rs_Month = clsCommon.myCdbl(txtCTC.Value)
                obj.Fixed_CTC_Rs_Month = clsCommon.myCdbl(txtFixedCTC.Value)
                obj.Variable_Amount = clsCommon.myCdbl(txtVariableAmt.Value)
                obj.Variable_Pay_Percentage = clsCommon.myCdbl(txtVariablePay.Value)

                Dim templist As New ArrayList
                For Each grow As GridViewRowInfo In dgv.Rows
                    If (clsCommon.myCBool(grow.Cells(colSelect).Value) = True) Then
                        templist.Add(clsCommon.myCstr(grow.Cells(colPHCode).Value))
                    End If
                Next
                obj.arrPayHead = templist

                If (ClsSalaryFitment.SaveData(obj, isNewEntry)) Then
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
            Dim Applicant_Code As Double = 0
            isFlag = True
            If clsCommon.myLen(txtAppcode.Value) > 0 Then
                Applicant_Code = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(Applicant_Code) As Applicant_Code  from TSPL_HR_SALARY_FITMENT where Applicant_Code='" + txtAppcode.Value + "'"))
                If Applicant_Code > 0 Then
                    If (myMessages.postConfirm()) Then
                        SaveData()
                        If (ClsSalaryFitment.PostData(MyBase.Form_ID, txtAppcode.Value)) Then
                            msg = "Successfully Posted"
                            common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                            LoadData(txtAppcode.Value, NavigatorType.Current)
                        End If
                    End If
                Else
                    Throw New Exception("You cannot post this entry before entering applicant code")
                End If

            Else
                Throw New Exception("Applicant code not found to post")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
    End Sub
    Sub CalculateVarAmt()
        Dim CTC As Double = 0
        Dim VarPayPercentage As Double = 0
        Dim VarAmt As Double = 0
        Dim FixedCTC As Double = 0

        CTC = clsCommon.myCdbl(txtCTC.Value)
        VarPayPercentage = clsCommon.myCdbl(txtVariablePay.Value)
        VarAmt = clsCommon.myCdbl((CTC * VarPayPercentage) / 100)
        If VarAmt > 0 Then
            txtVariableAmt.Value = VarAmt
            FixedCTC = CTC - VarAmt
            If FixedCTC > 0 Then
                txtFixedCTC.Value = FixedCTC
            End If

        End If
    End Sub
#End Region
#Region "Events"
    Private Sub txtAppcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtAppcode._MYNavigator
        Try
            'LoadData(txtAppcode.Value, NavType)
            Dim obj As New ClsReferenceCheck()

            Dim qst As String = "select count(*) from TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE='" + txtAppcode.Value + "' AND Posted =1"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtAppcode.MyReadOnly = False
            Else
                txtAppcode.MyReadOnly = True
            End If
            Dim ISAppCode As Integer
            Dim AppCode As String = clsCommon.myCstr(txtAppcode.Value)
            obj = ClsReferenceCheck.GetPostedData(AppCode, NavType)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.APPLICANT_CODE) > 0) Then
                ISAppCode = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) From TSPL_HR_SALARY_FITMENT WHERE APPLICANT_CODE='" + obj.APPLICANT_CODE + "'"))
                txtAppcode.Value = clsCommon.myCstr(obj.APPLICANT_CODE)
                UcRequisitionDetail1.AppCode = obj.APPLICANT_CODE
                UcRequisitionDetail1.RefreshData()
                If ISAppCode > 0 Then
                    LoadDataForNav(txtAppcode.Value, NavType)
                Else
                    isNewEntry = True
                    txtAppcode.MyReadOnly = False
                    txtAppcode.Focus()

                    txtCTC.Value = 0
                    txtFixedCTC.Value = 0
                    txtVariableAmt.Value = 0
                    txtVariablePay.Value = 0

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
        'Dim str As String = "select count(*) from TSPL_HR_SALARY_FITMENT where APPLICANT_CODE ='" + txtAppcode.Value + "' "
        'Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'If no = 0 AndAlso isButtonClicked = False Then
        '    txtAppcode.MyReadOnly = False
        'Else
        '    txtAppcode.MyReadOnly = True
        'End If

        'If txtAppcode.MyReadOnly OrElse isButtonClicked Then
        '    txtAppcode.Value = ClsSalaryFitment.GetFinder("", txtAppcode.Value, isButtonClicked)
        '    LoadData(txtAppcode.Value, NavigatorType.Current)
        'End If

        'If txtAppcode.MyReadOnly OrElse isButtonClicked Then
        '    Dim qry As String = "SELECT APPLICANT_CODE AS Code  FROM TSPL_HR_REFERENCE_CHECK  "
        '    txtAppcode.Value = clsCommon.ShowSelectForm("App_Code", qry, "Code", "Posted = 1 AND Final_feedback='P'", txtAppcode.Value, "", isButtonClicked)
        '    If clsCommon.myLen(txtAppcode.Value) > 0 Then
        '        UcRequisitionDetail1.AppCode = txtAppcode.Value
        '        UcRequisitionDetail1.RefreshData()
        '        LoadData(txtAppcode.Value, NavigatorType.Current)

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

        'Dim qry As String = "SELECT APPLICANT_CODE AS Code  FROM TSPL_HR_REFERENCE_CHECK  "
        'txtAppcode.Value = clsCommon.ShowSelectForm("App_Code", qry, "Code", "Posted = 1 AND Final_feedback='P'", txtAppcode.Value, "", isButtonClicked)
        txtAppcode.Value = ClsReferenceCheck.GetFinder(" ", txtAppcode.Value, isButtonClicked)
        If clsCommon.myLen(txtAppcode.Value) > 0 Then
            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()
            LoadData(txtAppcode.Value, NavigatorType.Current)
        Else
            UcRequisitionDetail1.AppCode = ""
            UcRequisitionDetail1.RefreshData()
            funReset()
        End If
    End Sub

    Private Sub FrmSalaryFitment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub FrmSalaryFitment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        SetDataBaseGrid()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnpost, "Press Alt+P Post Trasnaction")
        funReset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SetDataBaseGrid()
        dgv.Rows.Clear()
        dgv.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgv.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCostCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCostCode.FormatString = ""
        repoCostCode.HeaderText = "Pay Head Code"
        repoCostCode.Name = colPHCode
        repoCostCode.Width = 150
        repoCostCode.ReadOnly = True
        dgv.MasterTemplate.Columns.Add(repoCostCode)

        Dim repoCostName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCostName.FormatString = ""
        repoCostName.HeaderText = "Pay Head Name"
        repoCostName.Name = colPHName
        repoCostName.Width = 257
        repoCostName.ReadOnly = True
        dgv.MasterTemplate.Columns.Add(repoCostName)


        Dim qry As String = "SELECT PAY_HEAD_CODE,PAY_HEAD_NAME from TSPL_PAYHEAD_MASTER "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                dgv.Rows.AddNew()
                dgv.Rows(dgv.Rows.Count - 1).Cells(colSelect).Value = False
                dgv.Rows(dgv.Rows.Count - 1).Cells(colPHCode).Value = clsCommon.myCstr(dr("PAY_HEAD_CODE"))
                dgv.Rows(dgv.Rows.Count - 1).Cells(colPHName).Value = clsCommon.myCstr(dr("PAY_HEAD_NAME"))
            Next
        End If
    End Sub


    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
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

    Private Sub txtCTC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCTC.TextChanged
        If clsCommon.myCdbl(txtCTC.Value) > 0 Then
            CalculateVarAmt()
        Else
            txtVariableAmt.Value = 0
            txtVariablePay.Value = 0
            'txtFixedCTC.Value = 0
        End If
    End Sub

    Private Sub txtVariablePay_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVariablePay.TextChanged
        If clsCommon.myCdbl(txtVariablePay.Value) > 0 Then
            CalculateVarAmt()
        Else
            txtVariableAmt.Value = 0
            ' txtCTC.Value = 0
            txtFixedCTC.Value = 0
        End If

    End Sub
#End Region
End Class
