Imports common
Public Class frmLoanEntry
    Inherits FrmMainTranScreen
    ''BHA/21/06/18-000079 by balwinder on 09/07/2018
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                      "TSPL_LOAN_ENTRY" + Environment.NewLine)
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
            cboLoanType.Enabled = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                Dim obj As New clsLoanEntry()
                obj.Loan_Code = txtCode.Value
                obj.Loan_Date = txtDate.Value
                obj.Loan_Desc = txtDescription.Text
                obj.Loan_On_Name = txtName.Text
                obj.Loan_Type = clsCommon.myCstr(cboLoanType.SelectedValue)
                obj.Transaction_Type = clsCommon.myCstr(cboTransactionType.SelectedValue)
                If clsCommon.CompairString(obj.Transaction_Type, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Transaction_Type, "U") = CompairStringResult.Equal Then
                    obj.Account_Code = txtAccount.Value
                End If
                obj.Loan_Amount = txtLoanAmount.Value
                obj.Interest_Rate = txtInterestRate.Value
                obj.Tenaure = txtTenure.Value
                obj.Installment_Amount = clsCommon.myCdbl(lblInsatallmentAmount.Text)
                obj.Loan_Start_Date = txtLoanGivenOn.Value
                obj.Installment_Date_Of_Month = txtInstallmentDate.Value
                obj.Remarks = txtRemarks.Text
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Loan_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            Dim obj As clsLoanEntry = clsLoanEntry.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Loan_Code) > 0) Then
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    BtnPost.Enabled = False
                    btndelete.Enabled = False
                End If
                cboLoanType.Enabled = False
                UsLock1.Status = obj.Status
                txtCode.Value = obj.Loan_Code
                txtDate.Value = obj.Loan_Date
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                clsLoanEntry.PostData(txtCode.Value)
                clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                clsLoanEntry.DeleteData(txtCode.Value)
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
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
        Dim qry As String = "select Loan_Code,Loan_Date,Loan_Desc,Loan_On_Name,Loan_Type,Transaction_Type,Account_Code,Loan_Amount,Interest_Rate,Tenaure,Installment_Amount,Loan_Start_Date" + Environment.NewLine + _
"Installment_Date_Of_Month,Remarks, case when TSPL_LOAN_ENTRY.Status=1 then 'Approved' else 'Pending' end as Status  " + Environment.NewLine + _
" from TSPL_LOAN_ENTRY  "
        Dim whrClas As String = ""

        LoadData(clsCommon.ShowSelectForm("LoanEntryfind", qry, "Loan_Code", whrClas, txtCode.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_LOAN_ENTRY where Loan_Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        Try
            Dim dclIntRatePerMonth As Decimal = txtInterestRate.Value / (12 * 100)
            Dim dclInsAmt As Decimal = txtLoanAmount.Value * dclIntRatePerMonth * (Math.Pow(1 + dclIntRatePerMonth, txtTenure.Value) / (Math.Pow(1 + dclIntRatePerMonth, txtTenure.Value) - 1))
            dclInsAmt = Math.Round(dclInsAmt, 2, MidpointRounding.AwayFromZero)
            lblInsatallmentAmount.Text = clsCommon.myFormat(dclInsAmt)
        Catch ex As Exception
            lblInsatallmentAmount.Text = "0"
        End Try

    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Loan entry No no found..")
            End If
            Dim qry As String = "select * from ( select convert(varchar,max(InstallmentDate),103) as InstallmentDate,max(interest_Rate) as interest_Rate,max(Installment_Amount) as Installment_Amount,convert(varchar, max(ActualInstallment_Date),103) as ActualInstallment_Date,sum(ActualInstallment_Amount) as ActualInstallment_Amount,max(ActualInstallmentRemarks) as ActualInstallmentRemarks,max(InstallmentDate) as ViewInstallmentDate from (" + Environment.NewLine + _
             "select TSPL_LOAN_ENTRY.loan_code,DateQry.thedate as InstallmentDate,TSPL_LOAN_ENTRY.Interest_Rate, Installment_Amount,null as ActualInstallment_Date,0 as ActualInstallment_Amount,null as ActualInstallmentRemarks,CYear,CMonth" + Environment.NewLine + _
             "from TSPL_LOAN_ENTRY" + Environment.NewLine + _
             "left outer join (" + Environment.NewLine + _
             "select '" + txtCode.Value + "'  as Loan_Code, convert(date, thedate,103) as thedate,DATEPART(yy, thedate) as CYear,DATEPART(MM, thedate) as CMonth,DATEPART(DD, thedate) as CDATE " + Environment.NewLine + _
             "from  ExplodeDates ('" + clsCommon.GetPrintDate(txtLoanGivenOn.Value.AddMonths(1), "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtLoanGivenOn.Value.AddMonths(txtTenure.Value + 1), "dd/MMM/yyyy") + "') " + Environment.NewLine + _
             "where DATEPART(DD, ExplodeDates.thedate)=" + clsCommon.myCstr(txtInstallmentDate.Value) + "" + Environment.NewLine + _
             ")DateQry on DateQry.Loan_Code=TSPL_LOAN_ENTRY.loan_Code" + Environment.NewLine + _
             "where TSPL_LOAN_ENTRY.Loan_Code='" + txtCode.Value + "' " + Environment.NewLine + _
             "union all" + Environment.NewLine + _
             "select TSPL_LOAN_INSTALLMENT_ENTRY.Against_Loan_Code,null as InstallmentDate,0 as Interest_Rate,0 as Installment_Amount,TSPL_LOAN_INSTALLMENT_ENTRY.Installment_Date as ActualInstallment_Date ,Installment_Amount as ActualInstallment_Amount,TSPL_LOAN_INSTALLMENT_ENTRY.Remarks ActualInstallmentRemarks,DATEPART(yy, Installment_Date) as CYear,DATEPART(MM, Installment_Date) as CMonth from TSPL_LOAN_INSTALLMENT_ENTRY where Against_Loan_Code='" + txtCode.Value + "' and Status=1" + Environment.NewLine + _
             " )xx group by Loan_Code,CYear,CMonth)xxx order by  ViewInstallmentDate"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv.DataSource = dt
            'gv.Columns.Clear()
            'gv.Rows.Clear()
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            gv.EnableFiltering = True
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")
            Else
                gv.DataSource = dt
                SetGridFormationOFGV1()
            End If
            FindAndRestoreGridLayout(Me)
            gv.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
            gv.Columns(ii).BestFit()
        Next

        gv.Columns("InstallmentDate").IsVisible = True
        gv.Columns("InstallmentDate").Width = 120
        gv.Columns("InstallmentDate").HeaderText = "Installment Date"

        gv.Columns("interest_Rate").IsVisible = True
        gv.Columns("interest_Rate").Width = 120
        gv.Columns("interest_Rate").HeaderText = "interest Rate"

        gv.Columns("Installment_Amount").IsVisible = True
        gv.Columns("Installment_Amount").Width = 120
        gv.Columns("Installment_Amount").HeaderText = "Insatallment Amount"

        gv.Columns("ActualInstallment_Date").IsVisible = True
        gv.Columns("ActualInstallment_Date").Width = 120
        gv.Columns("ActualInstallment_Date").HeaderText = "Actual Installment Date"

        gv.Columns("ActualInstallment_Amount").IsVisible = True
        gv.Columns("ActualInstallment_Amount").Width = 120
        gv.Columns("ActualInstallment_Amount").HeaderText = "Actual Installment Amount"

        gv.Columns("ActualInstallmentRemarks").IsVisible = True
        gv.Columns("ActualInstallmentRemarks").Width = 120
        gv.Columns("ActualInstallmentRemarks").HeaderText = "Actual Installment Remarks"

        gv.Columns("ViewInstallmentDate").IsVisible = False
        gv.Columns("ViewInstallmentDate").HeaderText = "View Installment Date"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item As New GridViewSummaryItem("Installment_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item)
        item = New GridViewSummaryItem("ActualInstallment_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item)

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        RadPageView1.SelectedPage = RadPageViewPage2
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False

        gv.BestFitColumns()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If gv Is Nothing OrElse gv.Rows.Count <= 0 Then
                Throw New Exception("No data found to export")
            End If

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Amortization Vs Acutal Details")
            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class