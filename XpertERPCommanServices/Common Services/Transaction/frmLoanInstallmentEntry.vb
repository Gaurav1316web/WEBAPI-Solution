Imports common
Public Class frmLoanInstallmentEntry
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Public errorControl As clsErrorControl = New clsErrorControl()
#End Region

    Private Sub frmMilkGateEntryIn_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
            ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(BtnPost, "Press Alt+P Post Trasnaction")

            LoadLoanType()
            LoadTansactionType()
            SetUserMgmtNew()
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        BtnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub LoadLoanType()
        cboLoanType.DataSource = clsLoanEntry.GetLoanType()
        cboLoanType.ValueMember = "Code"
        cboLoanType.DisplayMember = "Name"
    End Sub

    Private Sub LoadTansactionType()
        cboTransactionType.DataSource = clsLoanEntry.GetTansactionType()
        cboTransactionType.ValueMember = "Code"
        cboTransactionType.DisplayMember = "Name"
    End Sub

    Private Sub txtAccount__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtAccount._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        txtAccount.Value = clsCommon.ShowSelectForm("LoacAcctFnd", qry, "Account_Code", " ControlAccount ='Y' ", txtAccount.Value, "Account_Code", isButtonClicked)
        qry = "select description from tspl_gl_accounts where account_code='" & txtAccount.Value & "'"
        lblAccountName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub

    Private Sub frmMilkGateEntryIn_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso BtnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                      "========Table Name=========" + Environment.NewLine + _
                      "TSPL_LOAN_INSTALLMENT_ENTRY" + Environment.NewLine)
        End If
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Sub AddNew()
        Try
            btnSave.Enabled = False
            BlankAllControls()
            isNewEntry = True
            btnSave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
            txtloanDocNo.Enabled = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtDescription.Text = ""
        txtName.Text = ""
        cboLoanType.SelectedValue = ""
        cboTransactionType.SelectedValue = ""
        txtAccount.Value = ""
        lblAccountName.Text = ""
        txtLoanAmount.Value = 0
        txtInterestRate.Value = 0
        txtTenure.Value = 0
        lblInsatallmentAmount.Text = ""
        txtLoanGivenOn.Value = txtDate.Value
        txtInstallmentDate.Value = 0
        txtRemarks.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtEnteredInstallmentAmount.Value = 0
        txtEnteredInstalmentRemarks.Text = ""
        txtloanDocNo.Value = ""
    End Sub

    Private Function AllowToSave() As Boolean
        '===============Preeti Gupta==================================
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Select()
            Return False
        End If
        '=======================================================
        If clsCommon.myLen(cboLoanType.SelectedValue) <= 0 Then
            cboLoanType.Focus()
            errorControl.SetError(cboLoanType, "Please select Loan Type")
            Throw New Exception("Please select Loan Type")
        End If
        If clsCommon.myLen(cboTransactionType.SelectedValue) <= 0 Then
            cboTransactionType.Focus()
            errorControl.SetError(cboTransactionType, "Please select Transaction Type")
            Throw New Exception("Please select Transaction Type")
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "U") = CompairStringResult.Equal Then
            If clsCommon.myLen(txtAccount.Value) <= 0 Then
                txtAccount.Focus()
                errorControl.SetError(txtAccount, "Please enter Account")
                Throw New Exception("Please enter Account")
            End If
        End If

        If txtLoanAmount.Value <= 0 Then
            txtLoanAmount.Focus()
            errorControl.SetError(txtLoanAmount, "Please enter loan amount")
            Throw New Exception("Please enter loan amount")
        End If

        If txtInterestRate.Value <= 0 Then
            txtInterestRate.Focus()
            errorControl.SetError(txtInterestRate, "Please enter Interest Rate")
            Throw New Exception("Please enter Interest Rate")
        End If

        If txtTenure.Value <= 0 Then
            txtTenure.Focus()
            errorControl.SetError(txtTenure, "Please enter loan Tenure")
            Throw New Exception("Please enter loan Tenure")
        End If
        If txtInstallmentDate.Value <= 0 Then
            txtInstallmentDate.Focus()
            errorControl.SetError(txtInstallmentDate, "Please enter Installment Date")
            Throw New Exception("Please enter Installment Date")
        ElseIf txtInstallmentDate.Value > 28 Then
            txtInstallmentDate.Focus()
            errorControl.SetError(txtInstallmentDate, "Installment Date cant be more than 28")
            Throw New Exception("Installment Date cant be more than 28")
        End If
        CalculateEMI()
        Return True
    End Function

    Private Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsLoanInstallmentEntry()
                obj.Installment_Code = txtCode.Value
                obj.Installment_Date = txtDate.Value
                obj.Against_Loan_Code = txtloanDocNo.Value
                obj.Installment_Amount = txtEnteredInstallmentAmount.Value
                obj.Remarks = txtEnteredInstalmentRemarks.Text


                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow("Data saved successfully")
                LoadData(obj.Installment_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            Dim obj As clsLoanInstallmentEntry = clsLoanInstallmentEntry.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Installment_Code) > 0) Then
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    BtnPost.Enabled = False
                    btndelete.Enabled = False
                End If
                txtloanDocNo.Enabled = False
                UsLock1.Status = obj.Status
                txtCode.Value = obj.Installment_Code
                txtDate.Value = obj.Installment_Date
                txtloanDocNo.Value = obj.Against_Loan_Code
                txtEnteredInstallmentAmount.Value = obj.Installment_Amount
                txtEnteredInstalmentRemarks.Text = obj.Remarks
                LoadLoanData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                clsLoanInstallmentEntry.PostData(txtCode.Value)
                clsCommon.MyMessageBoxShow("Successfully Posted", Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                clsLoanInstallmentEntry.DeleteData(txtCode.Value)
                clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub BtnPost_Click(sender As Object, e As EventArgs) Handles BtnPost.Click
        PostData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select  Installment_Code,Installment_Date,Against_Loan_Code,Installment_Amount,Remarks, case when Status=1 then 'Approved' else 'Pending' end as Status  " + Environment.NewLine + _
" from TSPL_LOAN_INSTALLMENT_ENTRY "
        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("LoanIEntryfind", qry, "Installment_Code", whrClas, txtCode.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_LOAN_INSTALLMENT_ENTRY where Installment_Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub cboTransactionType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboTransactionType.SelectedIndexChanged
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "U") = CompairStringResult.Equal Then
                lblAccount.Visible = True
                txtAccount.Visible = True
                lblAccountName.Visible = True
            Else
                lblAccount.Visible = False
                txtAccount.Visible = False
                lblAccountName.Visible = False
            End If
        Catch ex As Exception
            lblAccount.Visible = False
            txtAccount.Visible = False
            lblAccountName.Visible = False
        End Try
    End Sub

    Private Sub txtLoanAmount_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtLoanAmount.Validating, txtInterestRate.Validating, txtTenure.Validating
        CalculateEMI()
    End Sub

    Sub CalculateEMI()
        Dim dclIntRatePerMonth As Decimal = txtInterestRate.Value / (12 * 100)
        Dim dclInsAmt As Decimal = txtLoanAmount.Value * dclIntRatePerMonth * (Math.Pow(1 + dclIntRatePerMonth, txtTenure.Value) / (Math.Pow(1 + dclIntRatePerMonth, txtTenure.Value) - 1))
        dclInsAmt = Math.Round(dclInsAmt, 2, MidpointRounding.AwayFromZero)
        lblInsatallmentAmount.Text = clsCommon.myFormat(dclInsAmt)
    End Sub

    Private Sub txtloanDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtloanDocNo._MYValidating
        Dim qry As String = "select Loan_Code,Loan_Date,Loan_Desc,Loan_On_Name,Loan_Type,Transaction_Type,Account_Code,Loan_Amount,Interest_Rate,Tenaure,Installment_Amount,Loan_Start_Date" + Environment.NewLine + _
"Installment_Date_Of_Month,Remarks, case when TSPL_LOAN_ENTRY.Status=1 then 'Approved' else 'Pending' end as Status  " + Environment.NewLine + _
" from TSPL_LOAN_ENTRY  "
        Dim whrClas As String = " Status=1 "
        txtloanDocNo.Value = clsCommon.ShowSelectForm("LoaanEntryfind", qry, "Loan_Code", whrClas, txtloanDocNo.Value, "", isButtonClicked)
        LoadLoanData()
        txtEnteredInstallmentAmount.Value = clsCommon.myCdbl(lblInsatallmentAmount.Text)
    End Sub

    Sub LoadLoanData()
        Try
            Dim obj As clsLoanEntry = clsLoanEntry.GetData(txtloanDocNo.Value, common.NavigatorType.Current)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Loan_Code) > 0) Then
                txtDescription.Text = obj.Loan_Desc
                txtName.Text = obj.Loan_On_Name
                cboLoanType.SelectedValue = obj.Loan_Type
                cboTransactionType.SelectedValue = obj.Transaction_Type
                txtAccount.Value = obj.Account_Code
                lblAccountName.Text = obj.Account_Name
                txtLoanAmount.Value = obj.Loan_Amount
                txtInterestRate.Value = obj.Interest_Rate
                txtTenure.Value = obj.Tenaure
                lblInsatallmentAmount.Text = clsCommon.myFormat(obj.Installment_Amount)
                txtLoanGivenOn.Value = obj.Loan_Start_Date
                txtInstallmentDate.Value = obj.Installment_Date_Of_Month
                txtRemarks.Text = obj.Remarks
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class