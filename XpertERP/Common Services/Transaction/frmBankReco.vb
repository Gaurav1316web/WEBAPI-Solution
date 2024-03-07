Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Collections
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Threading
Imports common
Imports System.IO

Public Class FrmBankReco
    Inherits FrmMainTranScreen

#Region "Variable"
    Public IsFillDetails As Boolean = False
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public clicked As String = False
    Public Shared ArrBankReco_Arr As New ArrayList()
    Public AllowTreatChequeclearDateAsRecoDate As Boolean = False
    Dim arrHide As List(Of clsBankRecoDetails)
    Const colSelect As String = "colSelect"
    Const colChequeDate As String = "colChequeDate"
    Const colChequeNumber As String = "colChequeNumber"
    Const colPaymentMode As String = "colPaymentMode"
    Const colDocNo As String = "colDocNo"
    Const colDocDate As String = "colDocDate"
    Const colEntryDescription As String = "colEntryDescription"
    Const colWithdrawal As String = "colWithdrawal"
    Const colDeposit As String = "colDeposit"
    Const colClearedAmount As String = "colClearedAmount"
    Const colReconcileStatus As String = "colReconcileStatus"
    Const colReconcileDate As String = "colReconcileDate"
    Const colReconcileDescription As String = "colReconcileDescription"
    Const colDocType As String = "colDocType"
    Const colSource As String = "colSource"
    Const colEntryType As String = "colEntryType"
    Const colCustomerName As String = "colCustomerName"

    Const ReconciliationStatusOutstanding As String = "OutStanding"
    Const ReconciliationStatusCleared As String = "Cleared"

    Const TransactionTypeAll As String = "All"
    Const TransactionTypeWithdrawal As String = "Withdrawal"
    Const TransactionTypeDeposit As String = "Deposit"

    Const colRefDocNo As String = "colRefDocNo"
    Const colRemarks As String = "colRemarks"
    Const colRecoDoneDate As String = "colRecoDoneDate"

    Private BookBal As Decimal = 0
    Private isNewEntry As Boolean = False
    Private StrQuery As String = Nothing
    Private Dt As DataTable = Nothing
    Dim isValueChangingOccored As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim settBankRecoCheckFutureDocuments As Boolean = True
    Dim ArrDocs As List(Of String)
#End Region

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub fndBank__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndBank._MYValidating
        StrQuery = " select bank_code As Code,description  as [Description],BANKACCNUMBER as [BankAccNo]  from TSPL_Bank_MASTER "
        Dim Bank_Code As String = FrmMainTranScreen.bankPermission(Nothing)
        Dim strWhrclas As String = "1=1"
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strWhrclas += " AND RIGHT(TSPL_BANK_MASTER.BANKACC,3) in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
        ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(Bank_Code) > 0 Then
                strWhrclas += " AND TSPL_BANK_TRANSFER.Bank_Code in ( " + Bank_Code + " )"
            End If
        End If
        fndBank.Value = clsCommon.ShowSelectForm("Bank Master", StrQuery, "Code", strWhrclas, fndBank.Value, "bank_code", isButtonClicked)

        '----------------Done By Monika---------because no where condition pass after selecting bank code-------------
        StrQuery = StrQuery + " where bank_code='" + fndBank.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuery)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            StrQuery = "select Top(1) Reconciliation_Id from tspl_BankReco_Head Where Post<>'Y' AND Bank_Code='" & fndBank.Value & "'"
            StrQuery = clsDBFuncationality.getSingleValue(StrQuery)
            If clsCommon.myLen(StrQuery) > 0 Then
                clsCommon.MyMessageBoxShow("There is a pending reco '" & StrQuery & "' of Bank '" & clsCommon.myCstr(dt.Rows(0)("description")) & "'" + Environment.NewLine + "Please post it first.")
                fndBank.Value = ""
                txtBankName.Text = ""
                txtBankAccntNo.Text = ""
                gv1.Rows.Clear()
                fndBank.Focus()
            Else
                txtBankName.Text = clsCommon.myCstr(dt.Rows(0)("description"))
                txtBankAccntNo.Text = clsCommon.myCstr(dt.Rows(0)("BankAccNo"))
                LoadBookBalalnce()
                LoadStatementBalance()
            End If
        Else
            txtBankName.Text = ""
            txtBankAccntNo.Text = ""
            gv1.Rows.Clear()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmBankReco)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnReverse.Visible = False
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If MyBase.isExport = True Then
            btnExcel.Enabled = True
            BtnQuickExport.Enabled = True
        Else
            btnExcel.Enabled = False
            BtnQuickExport.Enabled = False
        End If
    End Sub

    Private Sub FrmBankReco_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S Then
            savedata(clicked)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D Then
            funDelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.H Then
            If RadButton6.Visible Then
                RadButton6.Visible = False
            Else
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.BankRecoHidePWD
                frm.strCode = clsFixedParameterCode.BankRecoHidePWD
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    RadButton6.Visible = True
                End If
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                If btnReverse.Visible Then
                    btnReverse.Visible = False
                Else
                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = "SIRC"
                    frm.strCode = "SIReversAndCreate"
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        btnReverse.Visible = True
                    End If

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub FrmBankReco_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '--=================added by preeti gupta[29/12/2016]=======================
        AllowTreatChequeclearDateAsRecoDate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TreatChequeClearDateAsRecoDate, clsFixedParameterCode.TreatChequeClearDateAsRecoDate, Nothing)) = 1, True, False)
        '==============================
        settBankRecoCheckFutureDocuments = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BankRecoCheckFutureDocuments, clsFixedParameterCode.BankRecoCheckFutureDocuments, Nothing)) = 1)
        funReset()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D for Delete")
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")
        ValidateLength()
        ApplyReadOption()
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndRecoId.Value = clsCommon.myCstr(Me.Tag)
            funFill(clsCommon.myCstr(Me.Tag))
        End If
        SplitContainer2.Panel2Collapsed = True
    End Sub

    Private Sub ValidateLength()
        fndRecoId.MyMaxLength = 30
        txtDescription.MaxLength = 150
        txtBankAccntNo.MaxLength = 50
    End Sub

    Private Sub ApplyReadOption()
        txtBankName.ReadOnly = True
    End Sub

    Private Sub LoadBlankGrid()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect = New GridViewCheckBoxColumn()
        'repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 40
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoSelect)


        Dim repoPaymentMode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPaymentMode = New GridViewTextBoxColumn()
        repoPaymentMode.FormatString = ""
        repoPaymentMode.HeaderText = "Payment Mode"
        repoPaymentMode.Name = colPaymentMode
        repoPaymentMode.Width = 120
        repoPaymentMode.ReadOnly = True
        repoPaymentMode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoPaymentMode)


        Dim repoRecoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoRecoDate = New GridViewDateTimeColumn()
        repoRecoDate.CustomFormat = "dd/MM/yyyy"
        repoRecoDate.FormatString = "{0:dd/MM/yyyy}"
        repoRecoDate.HeaderText = "Reconciliation Date"
        repoRecoDate.Name = colReconcileDate
        repoRecoDate.Width = 120
        repoRecoDate.ReadOnly = False
        repoRecoDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoRecoDate)


        Dim repoChequedate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoChequedate = New GridViewDateTimeColumn()
        repoChequedate.CustomFormat = "dd/MMM/yyyy"
        repoChequedate.FormatString = "{0:dd/MMM/yyyy}"
        repoChequedate.HeaderText = "Cheque/DD Date"
        repoChequedate.Name = colChequeDate
        repoChequedate.Width = 100
        repoChequedate.ReadOnly = True
        repoChequedate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoChequedate)

        Dim repoChequeNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoChequeNo = New GridViewTextBoxColumn()
        repoChequeNo.FormatString = ""
        repoChequeNo.HeaderText = "Cheque/DD Number"
        repoChequeNo.Name = colChequeNumber
        repoChequeNo.Width = 120
        repoChequeNo.ReadOnly = True
        repoChequeNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoChequeNo)

        Dim repoDocNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocNo = New GridViewTextBoxColumn()
        repoDocNo.FormatString = ""
        repoDocNo.HeaderText = "Document No."
        repoDocNo.Name = colDocNo
        repoDocNo.Width = 120
        repoDocNo.ReadOnly = True
        repoDocNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoDocNo)

        Dim repoDocdate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDocdate = New GridViewDateTimeColumn()
        repoDocdate.CustomFormat = "dd/MM/yyyy"
        repoDocdate.FormatString = "{0:d}"
        repoDocdate.HeaderText = "Doc Date"
        repoDocdate.Name = colDocDate
        repoDocdate.Width = 100
        repoDocdate.ReadOnly = True
        repoDocdate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoDocdate)


        Dim repoSource As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSource = New GridViewTextBoxColumn()
        repoSource.FormatString = ""
        repoSource.HeaderText = "Customer/Vendor"
        repoSource.Name = colSource
        repoSource.Width = 120
        repoSource.ReadOnly = True
        repoSource.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoSource)


        Dim repoEntryDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEntryDesc = New GridViewTextBoxColumn()
        repoEntryDesc.FormatString = ""
        repoEntryDesc.HeaderText = "Description"
        repoEntryDesc.Name = colEntryDescription
        repoEntryDesc.Width = 150
        repoEntryDesc.ReadOnly = True
        repoEntryDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoEntryDesc)


        Dim repoWithdrawal As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoWithdrawal = New GridViewDecimalColumn()
        repoWithdrawal.FormatString = ""
        repoWithdrawal.HeaderText = "Withdrawal"
        repoWithdrawal.Name = colWithdrawal
        repoWithdrawal.Width = 100
        repoWithdrawal.ReadOnly = True
        repoWithdrawal.FormatString = "{0:F2}"
        repoWithdrawal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoWithdrawal)

        Dim repoDeposit As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDeposit = New GridViewDecimalColumn()
        repoDeposit.FormatString = ""
        repoDeposit.HeaderText = "Deposit"
        repoDeposit.Name = colDeposit
        repoDeposit.Width = 100
        repoDeposit.ReadOnly = True
        repoDeposit.FormatString = "{0:F2}"
        repoDeposit.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDeposit)

        Dim repoClearedAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoClearedAmount = New GridViewDecimalColumn()
        repoClearedAmount.FormatString = ""
        repoClearedAmount.HeaderText = "Cleared Amount"
        repoClearedAmount.Name = colClearedAmount
        repoClearedAmount.Width = 100
        repoClearedAmount.ReadOnly = True
        repoClearedAmount.FormatString = "{0:F2}"
        repoClearedAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoClearedAmount)


        Dim repoRecoStatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRecoStatus = New GridViewComboBoxColumn()
        repoRecoStatus.FormatString = ""
        repoRecoStatus.HeaderText = "Reconciliation Status"
        repoRecoStatus.Name = colReconcileStatus
        repoRecoStatus.Width = 120
        repoRecoStatus.ReadOnly = True
        repoRecoStatus.DataSource = GetRecoStatus()
        repoRecoStatus.ValueMember = "Type"
        repoRecoStatus.DisplayMember = "Type"
        gv1.MasterTemplate.Columns.Add(repoRecoStatus)

       

        Dim repoRecoDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRecoDesc = New GridViewTextBoxColumn()
        repoRecoDesc.FormatString = ""
        repoRecoDesc.HeaderText = "Reconciliation Description"
        repoRecoDesc.Name = colReconcileDescription
        repoRecoDesc.Width = 200
        repoRecoDesc.ReadOnly = False
        repoRecoDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoRecoDesc)

        Dim repoDocType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocType = New GridViewTextBoxColumn()
        repoDocType.FormatString = ""
        repoDocType.HeaderText = "Document Type"
        repoDocType.Name = colDocType
        repoDocType.Width = 50
        repoDocType.ReadOnly = True
        'repoDocType.IsVisible = False
        repoDocType.VisibleInColumnChooser = True
        repoDocType.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        gv1.MasterTemplate.Columns.Add(repoDocType)

        '' Anubhooti 04-Sep-2014 BM00000003437 (Add EntryType Col To Distinguish Document No)
        Dim repoEntryType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEntryType = New GridViewTextBoxColumn()
        repoEntryType.FormatString = ""
        repoEntryType.HeaderText = "Entry Type"
        repoEntryType.Name = colEntryType
        repoEntryType.Width = 75
        repoEntryType.ReadOnly = True
        'repoDocType.IsVisible = False
        ' repoEntryType.VisibleInColumnChooser = True
        repoEntryType.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        gv1.MasterTemplate.Columns.Add(repoEntryType)
        ''
       
        '' richa against ticket no BHA/18/08/18-000460 on 29 Aug,2018
        Dim repoRefDocNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRefDocNo = New GridViewTextBoxColumn()
        repoRefDocNo.FormatString = ""
        repoRefDocNo.HeaderText = "Reference Doc No"
        repoRefDocNo.Name = colRefDocNo
        repoRefDocNo.Width = 75
        repoRefDocNo.ReadOnly = False
        repoRefDocNo.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        gv1.MasterTemplate.Columns.Add(repoRefDocNo)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 75
        repoRemarks.ReadOnly = False
        repoRemarks.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        Dim repoRecoDoneDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoRecoDoneDate = New GridViewDateTimeColumn()
        repoRecoDoneDate.CustomFormat = "dd/MM/yyyy"
        repoRecoDoneDate.FormatString = "{0:dd/MM/yyyy}"
        repoRecoDoneDate.HeaderText = "Reconciliation Done Date"
        repoRecoDoneDate.Name = colRecoDoneDate
        repoRecoDoneDate.Width = 120
        repoRecoDoneDate.ReadOnly = True
        repoRecoDoneDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoRecoDoneDate)


        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.Show()
        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim item1 As New GridViewSummaryItem(colWithdrawal, "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem(colDeposit, "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        'Dim item3 As New GridViewSummaryItem(colClearedAmount, "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)
        'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        'gv1.MasterTemplate.ShowTotals = True
        'gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Public Shared Function GetRecoStatus() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Type", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Type") = ReconciliationStatusOutstanding
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Type") = ReconciliationStatusCleared
        dt.Rows.Add(dr)

        Return dt
    End Function

    Public Shared Function GetTransType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("TransType", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("TransType") = TransactionTypeAll
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("TransType") = TransactionTypeWithdrawal
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("TransType") = TransactionTypeDeposit
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Function GetQueryForUnReconciledDocs(ByVal TransType As Char) As String
        Try
            StrQuery = clsBankReco.GetBankBookQuery("", dtStatementDate.Value, "'" + fndBank.Value + "'", "", "''", "Y", "", "", "", True)

            StrQuery = "Select CAST(0 as bit ) as [Select], MAX(CHEQUE_DATE) as [Date], MAX(CHEQUE_NO) as Number, DocNo, DocDate as Document_Date, MAX(Source_Code) as SourceCode, MAX(Source_Name) as [Description], SUM(Credit_Amount) as WithDrawal, SUM(Debit_Amount) as Deposit, 0.00 as [Cleared Amount], MAX(BANK_CODE) as Bank_Code, MAX(DocDate) as PostDate, TransType as DocType, Case When SUM(Debit_Amount)>0 Then 'D' Else 'W' End as EntryType, '" + ReconciliationStatusOutstanding + "' as RecoStatus, '" + clsCommon.GetPrintDate(dtStatementDate.Value, "dd/MM/yyyy") + "' as RecoDate, '' as RecoDesc,max([Payment Code]) as [Payment Code] from (" + StrQuery + ") XXX"

            StrQuery += " LEFT OUTER JOIN (SELECT tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Document_Type FROM tspl_BankReco_Head LEFT OUTER JOIN tspl_BankReco_Detail ON tspl_BankReco_Detail.Reconciliation_Id  =tspl_BankReco_Head.Reconciliation_Id WHERE Reconciliation_Status='C' and  tspl_BankReco_Head.BANK_CODE ='" & fndBank.Value & "' and tspl_BankReco_Detail.Reconciliation_Id not in ('" + fndRecoId.Value + "')) YYY ON YYY.Document_No=XXX.DocNo AND YYY.Document_Type=XXX.TransType" & _
                        " WHERE ISNULL(YYY.Document_No,'')=''"
            Select Case TransType
                Case "Withdrawal"
                    StrQuery += " and  XXX.Credit_Amount>0"
                Case "Deposit"
                    StrQuery += " and  XXX.Debit_Amount>0"
            End Select
            StrQuery += " GROUP BY XXX.DocNo, XXX.DocDate, XXX.TransType "
            Return StrQuery
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub LoadBookBalalnce()
        Try
            If clsCommon.myLen(fndBank.Value) > 0 Then
                StrQuery = clsBankReco.GetBankBookQuery("", dtRecoDate.Value, "'" + fndBank.Value + "'", "", "''", "Y", "", "", "", False)
                StrQuery = "Select CONVERT(Decimal(18,2), SUM(Debit_Amount)-SUM(Credit_Amount)) from (" & StrQuery & ") ZZZ"
                lblBookBalanceAmt.Text = clsCommon.myFormat(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(StrQuery)))
                lblAdjustedBookBalance.Text = lblBookBalanceAmt.Text
            Else
                lblBookBalanceAmt.Text = "0.00"
                lblAdjustedBookBalance.Text = "0.00"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub LoadStatementBalance()
        Try
            StrQuery = "select Top(1) Statement_Balance from tspl_BankReco_Head Where Bank_Code='" + fndBank.Value + "' ORDER BY Reconciliation_Date Desc"
            txtStatementBalance.Text = clsCommon.myFormat(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(StrQuery)))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub funCollectGridInfo(ByVal TransType As Char)
        StrQuery = GetQueryForUnReconciledDocs(TransType) + " order by XXX.DocDate, xxx.DocNo "
        Dt = clsDBFuncationality.GetDataTable(StrQuery)
        gv1.AutoGenerateColumns = False
        If (Dt IsNot Nothing AndAlso Dt.Rows.Count > 0) Then
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.DataSource = Dt
            gv1.Columns(colSelect).FieldName = "Select"
            gv1.Columns(colChequeDate).FieldName = "Date"
            gv1.Columns(colChequeDate).FormatString = "{0:dd/MMM/yyyy}"
            gv1.Columns(colChequeNumber).FieldName = "Number"
            gv1.Columns(colDocNo).FieldName = "DocNo"
            gv1.Columns(colDocDate).FieldName = "Document_Date"
            gv1.Columns(colSource).FieldName = "SourceCode"
            gv1.Columns(colEntryDescription).FieldName = "Description"
            gv1.Columns(colWithdrawal).FieldName = "withdrawal"
            gv1.Columns(colDeposit).FieldName = "Deposit"
            gv1.Columns(colClearedAmount).FieldName = "Cleared Amount"
            gv1.Columns(colReconcileStatus).FieldName = "RecoStatus"
            gv1.Columns(colReconcileDate).FieldName = "RecoDate"
            gv1.Columns(colPaymentMode).FieldName = "Payment Code"
            gv1.Columns(colReconcileDescription).FieldName = "RecoDesc"
            gv1.Columns(colDocType).FieldName = "DocType"
            gv1.Columns(colEntryType).FieldName = "EntryType"

            arrHide = New List(Of clsBankRecoDetails)
            For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                If clsBankRecoDetails.isHide(clsCommon.myCstr(gv1.Rows(ii).Cells(colDocNo).Value), clsCommon.myCstr(gv1.Rows(ii).Cells(colDocType).Value), Nothing) Then
                    Dim objtr As New clsBankRecoDetails
                    objtr.Reconciliation_Id = fndRecoId.Value
                    objtr.Bank_Code = fndBank.Value
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colChequeNumber).Value) > 0 Then
                        objtr.Cheque_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colChequeDate).Value, "dd/MM/yyyy")
                    Else
                        objtr.Cheque_Date = Nothing
                    End If
                    objtr.Cheque_No = gv1.Rows(ii).Cells(colChequeNumber).Value
                    objtr.Document_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDocNo).Value)
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colDocDate).Value) > 0 Then
                        objtr.Document_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colDocDate).Value, "dd/MM/yyyy")
                    Else
                        objtr.Document_Date = Nothing
                    End If
                    objtr.Description = clsCommon.myCstr(gv1.Rows(ii).Cells(colEntryDescription).Value)
                    objtr.Withdrawal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colWithdrawal).Value)
                    objtr.Deposit = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDeposit).Value)
                    objtr.Cleared_Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(colClearedAmount).Value)
                    Dim Status As String = ""
                    If gv1.Rows(ii).Cells(colReconcileStatus).Value = ReconciliationStatusCleared Then
                        Status = "C"
                    ElseIf gv1.Rows(ii).Cells(colReconcileStatus).Value = ReconciliationStatusOutstanding Then
                        Status = "O"
                    End If
                    objtr.Reconciliation_Status = Status
                    objtr.Reconciliation_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colReconcileDate).Value, "dd/MM/yyyy")
                    objtr.Reconciliation_Description = gv1.Rows(ii).Cells(colReconcileDescription).Value
                    objtr.Document_Type = gv1.Rows(ii).Cells(colDocType).Value
                    objtr.Entry_Type = gv1.Rows(ii).Cells(colEntryType).Value
                    objtr.Customer_Name = clsCommon.myCstr(gv1.Rows(ii).Cells(colSource).Value)
                    objtr.Payment_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colPaymentMode).Value)
                    arrHide.Add(objtr)

                    gv1.Rows(ii).Delete()
                End If
            Next

        End If
       
    End Sub

    Private Sub btnCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculate.Click
        Try
            If clsCommon.myCdbl(txtStatementBalance.Text) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please enter statement balance", Me.Text)
                txtStatementBalance.Focus()
                Exit Sub
            End If

            If clsCommon.myLen(fndBank.Value) > 0 Then
                gv1.MasterTemplate.FilterDescriptors.Clear()
                If gv1.Rows.Count <= 0 Then
                    If clsCommon.myCdbl(txtStatementBalance.Text) <> 0 Then
                        ddlTransType.SelectedValue = TransactionTypeAll
                        funCollectGridInfo("All")
                    End If
                Else
                    'If clsCommon.MyMessageBoxShow("Do you want to auto fill the selected documents", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Try
                        clsCommon.ProgressBarPercentShow()
                        Dim Arrdic As New Dictionary(Of String, DateTime?)
                        'ArrDocs = New List(Of String)
                        For ii As Integer = 0 To gv1.RowCount - 1
                            clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / gv1.RowCount, "Fatching selected Documents " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(gv1.RowCount))
                            If clsCommon.myCBool(gv1.Rows(ii).Cells(colSelect).Value) Then
                                'ArrDocs.Add(clsCommon.myCstr(gv1.Rows(ii).Cells(colDocNo).Value))
                                Arrdic.Add(clsCommon.myCstr(gv1.Rows(ii).Cells(colDocNo).Value), IIf(clsCommon.myLen(gv1.Rows(ii).Cells(colReconcileDate).Value) > 0, clsCommon.myCDate(gv1.Rows(ii).Cells(colReconcileDate).Value), Nothing))
                            End If
                        Next
                        clsCommon.ProgressBarPercentUpdate(0, "Loading All Document")
                        funCollectGridInfo("All")
                        If Arrdic IsNot Nothing AndAlso Arrdic.Count > 0 Then
                            For ii As Integer = 0 To gv1.RowCount - 1
                                clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / gv1.RowCount, "Filling selected Documents " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(gv1.RowCount))
                                If Arrdic.ContainsKey(clsCommon.myCstr(gv1.Rows(ii).Cells(colDocNo).Value)) Then
                                    gv1.CurrentColumn = gv1.Columns(colSelect)
                                    gv1.CurrentRow = gv1.Rows(ii)
                                    gv1.Rows(ii).Cells(colSelect).Value = True
                                    If Arrdic(clsCommon.myCstr(gv1.Rows(ii).Cells(colDocNo).Value)) IsNot Nothing Then
                                        gv1.CurrentColumn = gv1.Columns(colReconcileDate)
                                        gv1.Rows(ii).Cells(colReconcileDate).Value = Arrdic(clsCommon.myCstr(gv1.Rows(ii).Cells(colDocNo).Value))
                                    End If
                                End If
                            Next
                        End If
                        Arrdic = Nothing
                        clsCommon.ProgressBarPercentHide()
                    Catch ex As Exception
                        clsCommon.ProgressBarPercentHide()
                        Throw New Exception(ex.Message)
                    End Try
                    'Else
                    '    funCollectGridInfo("All")
                    'End If

                End If
                funCalculation()
                LoadBookBalalnce()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub funCalculation()
        If Not IsFillDetails Then
            Dim DepositAmt As Decimal = 0
            Dim WithdrawalAmt As Decimal = 0
            Dim dblTotalDebositSelected As Decimal = 0
            Dim dblTotalWithdrawSelected As Decimal = 0
            For Each row As GridViewRowInfo In gv1.Rows
                If clsCommon.CompairString(row.Cells(colEntryType).Value, "W") = CompairStringResult.Equal Then
                    dblTotalWithdrawSelected += clsCommon.myCdbl(row.Cells(colClearedAmount).Value)
                Else
                    dblTotalDebositSelected += clsCommon.myCdbl(row.Cells(colClearedAmount).Value)
                End If
                DepositAmt = DepositAmt + clsCommon.myCdbl(row.Cells(colDeposit).Value)
                WithdrawalAmt = WithdrawalAmt + clsCommon.myCdbl(row.Cells(colWithdrawal).Value)
            Next
            lblDepositOutStandingAmt.Text = clsCommon.myFormat(DepositAmt)
            lblWithdrawalAmtOutstanding.Text = clsCommon.myFormat(WithdrawalAmt)

            txtDepositTotal.Text = clsCommon.myFormat(dblTotalDebositSelected)
            txtWithdrawalTotal.Text = clsCommon.myFormat(dblTotalWithdrawSelected)

            funCalculationNext()
        End If
    End Sub

    Private Sub funCalculationNext()
        lblAdjustedStatementBal.Text = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(txtStatementBalance.Text) - clsCommon.myCdbl(lblWithdrawalAmtOutstanding.Text) + clsCommon.myCdbl(lblDepositOutStandingAmt.Text), 2))
        'lblAdjustedBookBalance.Text = Math.Round(clsCommon.myCdbl(lblBookBalanceAmt.Text), 2)
        txtOutOfBalanceAmt.Text = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(lblAdjustedBookBalance.Text) - clsCommon.myCdbl(lblAdjustedStatementBal.Text), 2))
        txtOutOfBalanceGrid.Text = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(lblBookBalanceAmt.Text), 2))
    End Sub

    Private Sub funReset()

        IsFillDetails = False
        BookBal = 0.0
        fndRecoId.Value = Nothing
        txtDescription.Text = Nothing
        fndBank.Value = Nothing
        txtBankName.Text = Nothing
        txtBankAccntNo.Text = Nothing
        dtRecoDate.Value = clsCommon.GETSERVERDATE()
        dtStatementDate.Value = clsCommon.GETSERVERDATE()
        LoadBlankGrid()
        isNewEntry = True
        txtWithdrawalTotal.Text = "0.00"
        txtDepositTotal.Text = "0.00"
        txtOutOfBalanceGrid.Text = "0.00"
        txtStatementBalance.Text = "0.00"
        lblWithdrawalAmtOutstanding.Text = "0.00"
        lblDepositOutStandingAmt.Text = "0.00"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = True
        isValueChangingOccored = False
        lblAdjustedBookBalance.Text = "0.00"
        lblAdjustedStatementBal.Text = "0.00"
        lblBookBalanceAmt.Text = "0.00"
        txtOutOfBalanceAmt.Text = "0.00"
        ddlTransType.ValueMember = "TransType"
        ddlTransType.DataSource = GetTransType()
        ArrDocs = New List(Of String)
        'ddlTransType.DisplayMember = "TransType"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnCalculate.Enabled = True
        arrHide = New List(Of clsBankRecoDetails)
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        savedata(clicked)
    End Sub

    Public Function savedata(ByVal clicked As Boolean) As Boolean
        Try
            funSave()
            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub funSave()

        If btnSave.Text = "Update" Then
            Dim strchk As String = "select Post from tspl_BankReco_Head where Reconciliation_Id='" + fndRecoId.Value + "'"
            Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
            If chkpost = "Y" Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Exit Sub
            End If
        End If
        ''BM00000008166 By Balwinder on 18/10/2016

        If clsCommon.GetDateWithStartTime(dtStatementDate.Value) > clsCommon.GetDateWithStartTime(dtRecoDate.Value) Then
            clsCommon.MyMessageBoxShow(Me, "Statement Date Should be Less than or equla to Reco Date", Me.Text)
            Exit Sub
        End If
        'ERO/07/05/18-000299 system stoping due to above setting.
        '=================Added by preeti gupta[29/12/2016====================[setting based "TreatChequeClearDateAsRecoDate"]]
        If Not AllowTreatChequeclearDateAsRecoDate Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCBool(gv1.Rows(ii).Cells(colSelect).Value) Then
                    If clsCommon.GetDateWithStartTime(dtStatementDate.Value) > clsCommon.GetDateWithStartTime(clsCommon.myCDate(gv1.Rows(ii).Cells(colReconcileDate).Value)) Then
                        clsCommon.MyMessageBoxShow(Me, "Statement Date Should be Less than or equla to Reco Date.At Row No " + clsCommon.myCstr(ii + 1))
                        Exit Sub
                    End If
                End If
            Next
        End If

        Dim Status As Char = Nothing
        Dim obj As New clsBankReco()
        obj.Reconciliation_Id = fndRecoId.Value
        obj.Bank_Code = fndBank.Value
        obj.Bank_Name = txtBankName.Text
        obj.Description = txtDescription.Text
        obj.Bank_AccountNo = txtBankAccntNo.Text
        obj.Statement_Date = clsCommon.myCDate(dtStatementDate.Value, "dd/MM/yyyy")
        obj.Post = False
        obj.Statement_Balance = clsCommon.myCdbl(txtStatementBalance.Text)
        obj.Reconciliation_Date = clsCommon.myCDate(dtRecoDate.Value, "dd/MM/yyyy")
        obj.Deposit_OutstandingAmt = clsCommon.myCdbl(lblDepositOutStandingAmt.Text)
        obj.Withdrawal_OutstandingAmt = clsCommon.myCdbl(lblWithdrawalAmtOutstanding.Text)


        obj.AdjustmentStatement_Balance = clsCommon.myCdbl(lblAdjustedStatementBal.Text)
        obj.AdjustmentBook_Balance = clsCommon.myCdbl(lblAdjustedBookBalance.Text)
        obj.Book_Balance = clsCommon.myCdbl(lblBookBalanceAmt.Text)
        obj.OutOf_Balance = clsCommon.myCdbl(txtOutOfBalanceAmt.Text)
        obj.TotalWithdrawal = clsCommon.myCdbl(txtWithdrawalTotal.Text)
        obj.TotalDeposit = clsCommon.myCdbl(txtDepositTotal.Text)

        obj.Arr = New List(Of clsBankRecoDetails)
        For Each grow As GridViewRowInfo In gv1.Rows
            Dim objTr As New clsBankRecoDetails()
            objTr.Reconciliation_Id = fndRecoId.Value
            objTr.Bank_Code = fndBank.Value
            If clsCommon.myLen(grow.Cells(colChequeNumber).Value) > 0 Then
                objTr.Cheque_Date = clsCommon.myCDate(grow.Cells(colChequeDate).Value, "dd/MM/yyyy")
            Else
                objTr.Cheque_Date = Nothing
            End If
            objTr.Cheque_No = clsCommon.myCstr(grow.Cells(colChequeNumber).Value)
            objTr.Document_No = clsCommon.myCstr(grow.Cells(colDocNo).Value)
            If clsCommon.myLen(grow.Cells(colDocDate).Value) > 0 Then
                objTr.Document_Date = clsCommon.myCDate(grow.Cells(colDocDate).Value, "dd/MM/yyyy")
            Else
                objTr.Document_Date = Nothing
            End If
            ''richa agarwal BM00000007487
            objTr.Description = clsCommon.myCstr(grow.Cells(colEntryDescription).Value)
            ''-----------------
            objTr.Withdrawal = clsCommon.myCdbl(grow.Cells(colWithdrawal).Value)
            objTr.Deposit = clsCommon.myCdbl(grow.Cells(colDeposit).Value)
            objTr.Cleared_Amount = clsCommon.myCdbl(grow.Cells(colClearedAmount).Value)
            If grow.Cells(colReconcileStatus).Value = ReconciliationStatusCleared Then
                Status = "C"
            ElseIf grow.Cells(colReconcileStatus).Value = ReconciliationStatusOutstanding Then
                Status = "O"
            End If
            objTr.Reconciliation_Status = Status
            objTr.Reconciliation_Date = clsCommon.myCDate(grow.Cells(colReconcileDate).Value, "dd/MM/yyyy") ' dtStatementDate.Value 
            objTr.Reconciliation_Description = grow.Cells(colReconcileDescription).Value
            objTr.Document_Type = grow.Cells(colDocType).Value
            '' Anubhooti 04-Sep-2014 (Save EntryType)
            objTr.Entry_Type = grow.Cells(colEntryType).Value
            objTr.Customer_Name = clsCommon.myCstr(grow.Cells(colSource).Value)
            objTr.Payment_Code = clsCommon.myCstr(grow.Cells(colPaymentMode).Value)
            ''
            '' richa against ticket no BHA/18/08/18-000460 on 29 Aug,2018
            objTr.ReferenceDocNo = grow.Cells(colRefDocNo).Value
            objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
            ''-----------------
            obj.Arr.Add(objTr)
        Next

        If arrHide IsNot Nothing AndAlso arrHide.Count > 0 Then
            For Each objtr As clsBankRecoDetails In arrHide
                objtr.is_Hide = True
                objtr.Bank_Code = fndBank.Value
                obj.Arr.Add(objtr)
            Next
        End If


        If (clsBankReco.funSave(obj, isNewEntry)) Then
            If clicked = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If

            funFill(obj.Reconciliation_Id)
        End If
    End Sub

    Private Sub funFill(ByVal StrDocNo As String)
        Try
            Dim obj As New clsBankReco()
            obj = clsBankReco.GetData(StrDocNo, dtStatementDate.Value)
            isValueChangingOccored = True
            ArrDocs = New List(Of String)
            IsFillDetails = True
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Reconciliation_Id) > 0) Then
                ArrDocs = New List(Of String)
                LoadBlankGrid()
                fndRecoId.Value = obj.Reconciliation_Id
                fndBank.Value = obj.Bank_Code
                txtBankName.Text = obj.Bank_Name
                txtBankAccntNo.Text = obj.Bank_AccountNo
                txtDescription.Text = obj.Description
                dtRecoDate.Value = obj.Reconciliation_Date
                dtStatementDate.Value = obj.Statement_Date

                txtStatementBalance.Text = clsCommon.myFormat(obj.Statement_Balance)
                lblDepositOutStandingAmt.Text = clsCommon.myFormat(obj.Deposit_OutstandingAmt)
                lblWithdrawalAmtOutstanding.Text = clsCommon.myFormat(obj.Withdrawal_OutstandingAmt)
                lblAdjustedStatementBal.Text = clsCommon.myFormat(obj.AdjustmentStatement_Balance)
                lblAdjustedBookBalance.Text = clsCommon.myFormat(obj.AdjustmentBook_Balance)
                lblBookBalanceAmt.Text = clsCommon.myFormat(obj.Book_Balance)
                txtOutOfBalanceAmt.Text = clsCommon.myFormat(obj.OutOf_Balance)
                txtOutOfBalanceGrid.Text = clsCommon.myFormat(obj.Book_Balance)
                txtWithdrawalTotal.Text = clsCommon.myFormat(obj.TotalWithdrawal)
                txtDepositTotal.Text = clsCommon.myFormat(obj.TotalDeposit)

                isNewEntry = False
                If obj.Post Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btnCalculate.EnableCodedUITests = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnCalculate.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    btnCalculate.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnCalculate.Enabled = True
                End If
            End If
            clsCommon.ProgressBarPercentShow()
            arrHide = New List(Of clsBankRecoDetails)
            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Rows.Clear()

                For Each objTr As clsBankRecoDetails In obj.Arr
                    If objTr.is_Hide Then
                        arrHide.Add(objTr)
                        Continue For
                    End If

                    gv1.Rows.AddNew()
                    clsCommon.ProgressBarPercentUpdate(gv1.Rows.Count * 100 / obj.Arr.Count, "Loading selected Data " + clsCommon.myCstr(gv1.Rows.Count) + "/" + clsCommon.myCstr(obj.Arr.Count))

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colChequeDate).Value = objTr.Cheque_Date
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colChequeNumber).Value = objTr.Cheque_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocNo).Value = objTr.Document_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocDate).Value = objTr.Document_Date
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEntryDescription).Value = objTr.Description
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colWithdrawal).Value = objTr.Withdrawal
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDeposit).Value = objTr.Deposit
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colClearedAmount).Value = objTr.Cleared_Amount
                    If objTr.Reconciliation_Status = "C" Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReconcileStatus).Value = ReconciliationStatusCleared
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = True
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReconcileStatus).Value = ReconciliationStatusOutstanding
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReconcileDate).Value = clsCommon.GetPrintDate(objTr.Reconciliation_Date, "dd/MM/yyyy") 'dtStatementDate.Value
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReconcileDescription).Value = objTr.Reconciliation_Description
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocType).Value = objTr.Document_Type
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEntryType).Value = objTr.Entry_Type
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSource).Value = objTr.Customer_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentMode).Value = objTr.Payment_Code
                    If clsCommon.myLen(objTr.ReconciliationDone_Date) > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRecoDoneDate).Value = clsCommon.GetPrintDate(objTr.ReconciliationDone_Date, "dd/MM/yyyy")
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRefDocNo).Value = objTr.ReferenceDocNo
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                    gv1.Refresh()
                Next
            End If

            clsCommon.ProgressBarPercentHide()
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isValueChangingOccored = False
            IsFillDetails = False
        End Try
    End Sub

    Private Sub fndRecoId__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndRecoId._MYNavigator
        Dim qst As String = " SELECT  tspl_BankReco_Head.Reconciliation_Id, tspl_BankReco_Head.Bank_Code, tspl_BankReco_Head.Bank_Name, tspl_BankReco_Head.Description   FROM tspl_BankReco_Head " & _
        " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=tspl_BankReco_Head.Bank_Code  where 1=1 "
        Dim Bank_Code As String = FrmMainTranScreen.bankPermission(Nothing)
        Dim strWhrclas As String = ""
        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing), "0") = CompairStringResult.Equal Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strWhrclas += " AND RIGHT(TSPL_BANK_MASTER.BANKACC,3) in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
        Else
            If clsCommon.myLen(fndBank.Value) > 0 Then
                strWhrclas += " AND tspl_BankReco_Head.Bank_Code in ( '" + fndBank.Value + "' )"
            End If
        End If
        qst += " " + strWhrclas + ""
        Select Case NavType
            Case NavigatorType.Current
                qst += " and tspl_BankReco_Head .Reconciliation_Id in ('" + fndRecoId.Value + "')"
            Case NavigatorType.Next
                qst += " and tspl_BankReco_Head .Reconciliation_Id in (select min(Reconciliation_Id ) from tspl_BankReco_Head LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=tspl_BankReco_Head.Bank_Code where Reconciliation_Id  >'" + fndRecoId.Value + "' " + strWhrclas + ")"
            Case NavigatorType.First
                qst += " and tspl_BankReco_Head .Reconciliation_Id in (select MIN(Reconciliation_Id ) from tspl_BankReco_Head LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=tspl_BankReco_Head.Bank_Code WHere 1=1 " + strWhrclas + ")"
            Case NavigatorType.Last
                qst += " and tspl_BankReco_Head .Reconciliation_Id in (select Max(Reconciliation_Id ) from tspl_BankReco_Head LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=tspl_BankReco_Head.Bank_Code Where 1=1 " + strWhrclas + ")"
            Case NavigatorType.Previous
                qst += " and tspl_BankReco_Head .Reconciliation_Id in (select Max(Reconciliation_Id ) from tspl_BankReco_Head LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=tspl_BankReco_Head.Bank_Code where Reconciliation_Id  <'" + fndRecoId.Value + "' " + strWhrclas + ")"
        End Select
        Dim RecoId As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qst))
        If Not (RecoId Is Nothing OrElse RecoId = "") Then
            fndRecoId.Value = RecoId
            funFill(fndRecoId.Value)
        End If
    End Sub

    Private Sub fndRecoId__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndRecoId._MYValidating
        Dim qry As String = "SELECT tspl_BankReco_Head.Reconciliation_Id, tspl_BankReco_Head.Bank_Code, tspl_BankReco_Head.Bank_Name, tspl_BankReco_Head.Description,CONVERT (VARCHAR,statement_Date,103) As [Statement Date], CONVERT (VARCHAR,Reconciliation_Date ,103) AS [Reconcillation Date],case when isnull(tspl_BankReco_Head.Post,'N')='Y' then 'Approved' else 'Pending' end as Status FROM tspl_BankReco_Head " & _
        " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=tspl_BankReco_Head.Bank_Code"
        Dim Bank_Code As String = FrmMainTranScreen.bankPermission(Nothing)
        Dim strWhrclas As String = "1=1"
        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing), "0") = CompairStringResult.Equal Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strWhrclas += " AND RIGHT(TSPL_BANK_MASTER.BANKACC,3) in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
        Else
            If clsCommon.myLen(fndBank.Value) > 0 Then
                strWhrclas += " AND tspl_BankReco_Head.Bank_Code in ( '" + fndBank.Value + "' )"
            End If
        End If
        fndRecoId.Value = clsCommon.ShowSelectForm("Bank Reconciliation", qry, "Reconciliation_Id", strWhrclas, fndRecoId.Value, "Reconciliation_Id", isButtonClicked, "tspl_BankReco_Head.Reconciliation_Date")
        If clsCommon.myLen(fndRecoId.Value) > 0 Then
            funFill(fndRecoId.Value)
        End If
    End Sub

    Private Sub SelectAll(ByVal val As Boolean)
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                gv1.CurrentColumn = gv1.Columns(colSelect)
                gv1.CurrentRow = gv1.Rows(ii)
                gv1.Rows(ii).Cells(colSelect).Value = val
            Next
            gv1.CurrentRow = gv1.Rows(0)
        End If
        funCalculation()
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        gv1.CurrentColumn = gv1.Columns(3)
        SelectAll(True)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        If gv1.Rows.Count > 0 Then
            For Each row As GridViewRowInfo In gv1.Rows
                If row.Cells(colSelect).Value = True Then
                    row.Cells(colReconcileStatus).Value = ReconciliationStatusCleared
                End If
            Next
        End If
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Dim strCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value)
        If clsCommon.myLen(strCode) > 0 Then
            If clsCommon.CompairString(gv1.CurrentRow.Cells(colDocType).Value, "Opening Bank Reco") = CompairStringResult.Equal Then
                MDI.ShowForm(clsUserMgtCode.BankOpeningReco, "", True, strCode)
            ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colDocType).Value, "Payment") = CompairStringResult.Equal Then
                MDI.ShowForm(clsUserMgtCode.PaymentEntryNew, "", True, strCode)
            ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colDocType).Value, "Receipt") = CompairStringResult.Equal Then
                MDI.ShowForm(clsUserMgtCode.ReceiptEntry, "", True, strCode)
            ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colDocType).Value, "BankTransfer") = CompairStringResult.Equal Then
                MDI.ShowForm(clsUserMgtCode.bankTransfer, "", True, strCode)
            ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colDocType).Value, "Reverse") = CompairStringResult.Equal Then
                MDI.ShowForm(clsUserMgtCode.reverseTransaction, "", True, strCode)
            End If
        End If
    End Sub

    Private Sub gv1_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gv1.ValueChanging
        If Not isInsideLoadData AndAlso gv1.CurrentRow.Index >= 0 Then
            If Not isValueChangingOccored Then
                isValueChangingOccored = True
                Try
                    If gv1.CurrentColumn Is gv1.Columns(colSelect) Then
                        Dim selVal As Boolean = clsCommon.myCBool(e.NewValue)
                        gv1.CurrentRow.Cells(colReconcileStatus).Value = IIf(clsCommon.myCBool(e.NewValue), ReconciliationStatusCleared, ReconciliationStatusOutstanding)
                        If gv1.CurrentRow.Cells(colReconcileStatus).Value = ReconciliationStatusCleared Then
                            If clsCommon.CompairString(gv1.CurrentRow.Cells(colEntryType).Value, "D") = CompairStringResult.Equal Then
                                gv1.CurrentRow.Cells(colClearedAmount).Value = gv1.CurrentRow.Cells(colDeposit).Value
                                gv1.CurrentRow.Cells(colDeposit).Value = "0.00"
                                lblDepositOutStandingAmt.Text = clsCommon.myFormat(clsCommon.myCdbl(lblDepositOutStandingAmt.Text) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colClearedAmount).Value))
                            End If
                            If clsCommon.CompairString(gv1.CurrentRow.Cells(colEntryType).Value, "W") = CompairStringResult.Equal Then
                                gv1.CurrentRow.Cells(colClearedAmount).Value = gv1.CurrentRow.Cells(colWithdrawal).Value
                                gv1.CurrentRow.Cells(colWithdrawal).Value = "0.00"
                                lblWithdrawalAmtOutstanding.Text = clsCommon.myFormat(clsCommon.myCdbl(lblWithdrawalAmtOutstanding.Text) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colClearedAmount).Value))
                            End If
                        ElseIf gv1.CurrentRow.Cells(colReconcileStatus).Value = ReconciliationStatusOutstanding Then
                            If clsCommon.CompairString(gv1.CurrentRow.Cells(colEntryType).Value, "W") = CompairStringResult.Equal Then
                                gv1.CurrentRow.Cells(colWithdrawal).Value = gv1.CurrentRow.Cells(colClearedAmount).Value
                                gv1.CurrentRow.Cells(colClearedAmount).Value = 0.0
                                lblWithdrawalAmtOutstanding.Text = clsCommon.myFormat(clsCommon.myCdbl(lblWithdrawalAmtOutstanding.Text) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colWithdrawal).Value))
                            End If
                            If clsCommon.CompairString(gv1.CurrentRow.Cells(colEntryType).Value, "D") = CompairStringResult.Equal Then
                                gv1.CurrentRow.Cells(colDeposit).Value = gv1.CurrentRow.Cells(colClearedAmount).Value
                                gv1.CurrentRow.Cells(colClearedAmount).Value = 0.0
                                lblDepositOutStandingAmt.Text = clsCommon.myFormat(clsCommon.myCdbl(lblDepositOutStandingAmt.Text) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colDeposit).Value))
                            End If
                        End If
                        funCalculationNext()
                        funCalculation()
                    End If
                Catch ex As Exception
                    e.Cancel = True
                Finally
                    isValueChangingOccored = False
                End Try
            End If
        End If
    End Sub

    Private Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsBankReco.DeleteData(fndRecoId.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub funPost()
        Try
            If (myMessages.postConfirm()) Then
                If savedata(clicked) Then
                    If (clsBankReco.PostData(fndRecoId.Value)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully", Me.Text)
                        funFill(fndRecoId.Value)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        clicked = False
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        clicked = True
        funPost()
    End Sub

    Private Sub txtStatementBalance_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStatementBalance.TextChanged
        funCalculationNext()
    End Sub

    Private Sub btnWithdrawal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWithdrawal.Click
        Try
            Dim frm As New FrmPaymentNew()
            frm.txtBankCode.Value = fndBank.Value
            frm.ShowDialog()
            RefreshAfter_Withdraw_Deposit()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeposit.Click
        Try
            Dim frm As New FrmReceipttNew()
            frm.fndBankCode.Value = fndBank.Value
            frm.ShowDialog()
            RefreshAfter_Withdraw_Deposit()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RefreshAfter_Withdraw_Deposit()
        Try
            StrQuery = GetQueryForUnReconciledDocs("All")
            StrQuery = "Select * from (" + StrQuery + ") ZZZ Where DocNo not in (" + clsCommon.GetMulcallString(ArrDocs) + ")"
            Dt = clsDBFuncationality.GetDataTable(StrQuery)
            For Each dr As DataRow In Dt.Rows
                gv1.Rows.AddNew()
                gv1.CurrentRow.Cells(colChequeNumber).Value = clsCommon.myCstr(dr("Number"))
                If clsCommon.myLen(dr("Number")) > 0 Then
                    gv1.CurrentRow.Cells(colChequeDate).Value = dr("Date")
                End If
                gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.myCstr(dr("DocNo"))
                gv1.CurrentRow.Cells(colDocDate).Value = dr("Document_Date")
                gv1.CurrentRow.Cells(colEntryDescription).Value = clsCommon.myCstr(dr("Description"))
                gv1.CurrentRow.Cells(colWithdrawal).Value = clsCommon.myCstr(dr("withdrawal"))
                gv1.CurrentRow.Cells(colDeposit).Value = clsCommon.myCstr(dr("Deposit"))
                gv1.CurrentRow.Cells(colClearedAmount).Value = clsCommon.myCstr(dr("Cleared Amount"))
                gv1.CurrentRow.Cells(colReconcileStatus).Value = clsCommon.myCstr(dr("RecoStatus"))
                gv1.CurrentRow.Cells(colReconcileDate).Value = dr("RecoDate")
                gv1.CurrentRow.Cells(colReconcileDescription).Value = clsCommon.myCstr(dr("RecoDesc"))
                gv1.CurrentRow.Cells(colDocType).Value = clsCommon.myCstr(dr("DocType"))
                gv1.CurrentRow.Cells(colEntryType).Value = clsCommon.myCstr(dr("EntryType"))
                gv1.CurrentRow.Cells(colSource).Value = clsCommon.myCstr(dr("SourceCode"))
                gv1.CurrentRow.Cells(colPaymentMode).Value = clsCommon.myCstr(dr("Payment Code"))
                ArrDocs.Add(gv1.CurrentRow.Cells(colDocNo).Value)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub ddlTransType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTransType.SelectedValueChanged
        'IsLoadGridAgn = True
        'If ddlTransType.SelectedValue = TransactionTypeAll Then
        '    funCollectGridInfo("All")
        'ElseIf ddlTransType.SelectedValue = TransactionTypeDeposit Then
        '    funCollectGridInfo("Deposit")
        'ElseIf ddlTransType.SelectedValue = TransactionTypeWithdrawal Then
        '    funCollectGridInfo("Withdrawal")
        'End If
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub dtRecoDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtRecoDate.ValueChanged
        Try
            LoadBookBalalnce()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub dtStatementDate_ValueChanged(sender As Object, e As EventArgs) Handles dtStatementDate.ValueChanged
        For Each grow As GridViewRowInfo In gv1.Rows
            If grow.Cells(colSelect).Value = False Then
                grow.Cells(colReconcileDate).Value = dtStatementDate.Value
            End If

        Next
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Bank Name: " + txtBankName.Text)
            arrHeader.Add("Bank Account No.: " + txtBankAccntNo.Text)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Bank Reconcillation", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Bank Reconcillation", gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub BtnQuickExport_Click(sender As Object, e As EventArgs) Handles BtnQuickExport.Click
        Dim arrHeader As List(Of String) = New List(Of String)()
        arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
        arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmBankReco & "'"))
        arrHeader.Add("Bank Name: " + txtBankName.Text)
        arrHeader.Add("Bank Account No.: " + txtBankAccntNo.Text)
        'Dim sfd As SaveFileDialog = New SaveFileDialog()
        'Dim filePath As String
        'sfd.FileName = Me.Text
        'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
        'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '    filePath = sfd.FileName
        'Else
        '    Exit Sub
        'End If
        'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
        'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
        'Process.Start(filePath)
        transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        gv1.CurrentColumn = gv1.Columns(3)
        SelectAll(False)
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnReset.Click, btnReset.Click
        funReset()
    End Sub

    Private Sub txtStatementBalance_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtStatementBalance.Validating
        txtStatementBalance.Text = clsCommon.myFormat(clsCommon.myCdbl(txtStatementBalance.Text))
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        SplitContainer2.Panel2Collapsed = Not SplitContainer2.Panel2Collapsed
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            gvImport.DataSource = Nothing
            gvImport.Rows.Clear()
            gvImport.Columns.Clear()

            transportSql.LoadDocument(gvImport, "BankReco")

            Dim repoIsOK As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoIsOK.FormatString = ""
            repoIsOK.DecimalPlaces = 0
            repoIsOK.HeaderText = "IsOK"
            repoIsOK.Name = "IsOK"
            repoIsOK.Minimum = 0
            repoIsOK.Maximum = 2
            repoIsOK.ReadOnly = True
            repoIsOK.IsVisible = False
            gvImport.MasterTemplate.Columns.Add(repoIsOK)


            gvImport.BestFitColumns()
            For ii As Integer = 0 To gvImport.Columns.Count - 1
                gvImport.Columns(ii).ReadOnly = True
            Next
            gvImport.AllowAddNewRow = False
            gvImport.ShowGroupPanel = False
            gvImport.ShowFilteringRow = True
            gvImport.EnableFiltering = True
            gvImport.AllowColumnReorder = False
            gvImport.AllowRowReorder = False
            gvImport.EnableSorting = False
            gvImport.MasterTemplate.ShowRowHeaderColumn = False


            LoadCombo()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadCombo()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)
        For ii As Integer = 0 To gvImport.Columns.Count - 1
            dr = dt.NewRow()
            dr("Code") = clsCommon.myCstr(gvImport.Columns(ii).HeaderText)
            dr("Name") = clsCommon.myCstr(gvImport.Columns(ii).HeaderText)
            dt.Rows.Add(dr)
        Next


        cboColumnChequeNo.DataSource = dt.Copy()
        cboColumnChequeNo.ValueMember = "Code"
        cboColumnChequeNo.DisplayMember = "Name"

        cboAmount.DataSource = dt.Copy()
        cboAmount.ValueMember = "Code"
        cboAmount.DisplayMember = "Name"

        cboType.DataSource = dt.Copy()
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"

        cboDate.DataSource = dt.Copy()
        cboDate.ValueMember = "Code"
        cboDate.DisplayMember = "Name"

        cboDocNo.DataSource = dt.Copy()
        cboDocNo.ValueMember = "Code"
        cboDocNo.DisplayMember = "Name"


    End Sub

    Private Sub gvImport_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvImport.RowFormatting
        Try
            If clsCommon.myCdbl(e.RowElement.RowInfo.Cells("IsOK").Value) = 1 Then
                e.RowElement.DrawFill = True
                e.RowElement.GradientStyle = GradientStyles.Solid
                e.RowElement.ForeColor = Color.Black
                e.RowElement.BackColor = Color.LightGreen
            ElseIf clsCommon.myCdbl(e.RowElement.RowInfo.Cells("IsOK").Value) = 2 Then
                e.RowElement.DrawFill = True
                e.RowElement.GradientStyle = GradientStyles.Solid
                e.RowElement.ForeColor = Color.Black
                e.RowElement.BackColor = Color.MistyRose
            Else
                e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
                e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local)
                e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
                e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        Try
            If gvImport.RowCount <= 0 Then
                Throw New Exception("Please First browse excel sheet of bank statement")
            End If
            If gv1.RowCount <= 0 Then
                Throw New Exception("No doucment in grid")
            End If
            Dim intRecoDocs As Integer = 0
            clsCommon.ProgressBarPercentShow()
            If rbtnDocumentNo.IsChecked Then
                If clsCommon.myLen(cboDocNo.SelectedValue) <= 0 Then
                    cboDocNo.Focus()
                    Throw New Exception("Please select Document No column")
                End If
                If clsCommon.myLen(cboDate.SelectedValue) <= 0 Then
                    cboDate.Focus()
                    Throw New Exception("Please select Date column")
                End If

                For ii As Integer = 0 To gvImport.Rows.Count - 1
                    clsCommon.ProgressBarPercentUpdate(ii * 100 / gvImport.Rows.Count, "")
                    If clsCommon.myCdbl(gvImport.Rows(ii).Cells("IsOK").Value) = 0 Then
                        Dim strOuterDocNo As String = clsCommon.myCstr(gvImport.Rows(ii).Cells(clsCommon.myCstr(cboDocNo.SelectedValue)).Value)
                        Dim dtOuterDate As Date? = Nothing
                        Try
                            dtOuterDate = clsCommon.myCDate(gvImport.Rows(ii).Cells(clsCommon.myCstr(cboDate.SelectedValue)).Value, "dd/MMM/yyyy")
                        Catch ex As Exception
                        End Try

                        Dim isFound As Boolean = False
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            If Not clsCommon.myCBool(gv1.Rows(jj).Cells(colSelect).Value) Then
                                Dim strInnerDocNo As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colDocNo).Value)
                                If clsCommon.CompairString(strOuterDocNo, strInnerDocNo) = CompairStringResult.Equal Then
                                    gv1.CurrentColumn = gv1.Columns(colSelect)
                                    gv1.CurrentRow = gv1.Rows(jj)
                                    gv1.Rows(jj).Cells(colSelect).Value = True
                                    gv1.CurrentColumn = gv1.Columns(colReconcileDate)
                                    gv1.Rows(jj).Cells(colReconcileDate).Value = dtOuterDate
                                    If dtOuterDate IsNot Nothing Then
                                        gv1.CurrentColumn = gv1.Columns(colReconcileDate)
                                        gv1.Rows(jj).Cells(colReconcileDate).Value = dtOuterDate
                                    End If

                                    isFound = True
                                    Exit For
                                End If
                            End If
                        Next

                        If isFound Then
                            intRecoDocs += 1
                            gvImport.Rows(ii).Cells("IsOK").Value = 1
                        End If
                    End If
                Next

            Else
                If rbtnChequeNoAmtAntType.IsChecked AndAlso clsCommon.myLen(cboColumnChequeNo.SelectedValue) <= 0 Then
                    cboColumnChequeNo.Focus()
                    Throw New Exception("Please select Cheque no column")
                End If
                If clsCommon.myLen(cboAmount.SelectedValue) <= 0 Then
                    cboAmount.Focus()
                    Throw New Exception("Please select Amount column")
                End If
                If clsCommon.myLen(cboType.SelectedValue) <= 0 Then
                    cboType.Focus()
                    Throw New Exception("Please select Type column")
                End If
                If clsCommon.myLen(cboDate.SelectedValue) <= 0 Then
                    cboDate.Focus()
                    Throw New Exception("Please select Date column")
                End If

                For ii As Integer = 0 To gvImport.Rows.Count - 1
                    clsCommon.ProgressBarPercentUpdate(ii * 100 / gvImport.Rows.Count, "")
                    If clsCommon.myCdbl(gvImport.Rows(ii).Cells("IsOK").Value) = 0 Then
                        Dim dblOuterChequeNo As Double = 0
                        If rbtnChequeNoAmtAntType.IsChecked Then
                            dblOuterChequeNo = clsCommon.myCdbl(gvImport.Rows(ii).Cells(clsCommon.myCstr(cboColumnChequeNo.SelectedValue)).Value)
                            If dblOuterChequeNo <= 0 Then
                                Continue For
                            End If
                        End If
                        Dim dblOuterAmount As Double = clsCommon.myCdbl(gvImport.Rows(ii).Cells(clsCommon.myCstr(cboAmount.SelectedValue)).Value)
                        Dim strOuterType As String = clsCommon.myCstr(gvImport.Rows(ii).Cells(clsCommon.myCstr(cboType.SelectedValue)).Value)
                        Dim dblOuterDate As Date? = Nothing
                        Try
                            dblOuterDate = clsCommon.myCDate(gvImport.Rows(ii).Cells(clsCommon.myCstr(cboDate.SelectedValue)).Value, "dd/MMM/yyyy")
                        Catch ex As Exception
                        End Try

                        Dim isFound As Boolean = False
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            If Not clsCommon.myCBool(gv1.Rows(jj).Cells(colSelect).Value) Then
                                Dim dblInnerChequeNo As Double = 0
                                If rbtnChequeNoAmtAntType.IsChecked Then
                                    dblInnerChequeNo = clsCommon.myCdbl(gv1.Rows(jj).Cells(colChequeNumber).Value)
                                End If
                                Dim dblInnerAmount As Double = 0
                                If clsCommon.CompairString(strOuterType, "CR") = CompairStringResult.Equal Then
                                    dblInnerAmount = clsCommon.myCdbl(gv1.Rows(jj).Cells(colDeposit).Value)
                                Else
                                    dblInnerAmount = clsCommon.myCdbl(gv1.Rows(jj).Cells(colWithdrawal).Value)
                                End If
                                If rbtnChequeNoAmtAntType.IsChecked Then
                                    If dblInnerChequeNo <= 0 Then
                                        Continue For
                                    End If
                                    If dblInnerChequeNo = dblOuterChequeNo AndAlso dblInnerAmount = dblOuterAmount Then
                                        gv1.CurrentColumn = gv1.Columns(colSelect)
                                        gv1.CurrentRow = gv1.Rows(jj)
                                        gv1.Rows(jj).Cells(colSelect).Value = True
                                        If dblOuterDate IsNot Nothing Then
                                            gv1.CurrentColumn = gv1.Columns(colReconcileDate)
                                            gv1.Rows(jj).Cells(colReconcileDate).Value = dblOuterDate
                                        End If
                                        isFound = True
                                        Exit For
                                    End If
                                ElseIf rbtnAmtAntType.IsChecked Then
                                    If dblInnerAmount = dblOuterAmount Then
                                        isFound = True
                                        For kk As Integer = 0 To gv1.Rows.Count - 1
                                            If Not clsCommon.myCBool(gv1.Rows(kk).Cells(colSelect).Value) Then
                                                If kk = jj Then
                                                    Continue For
                                                End If
                                                Dim dblInnerInnerAmount As Double = 0
                                                If clsCommon.CompairString(strOuterType, "CR") = CompairStringResult.Equal Then
                                                    dblInnerInnerAmount = clsCommon.myCdbl(gv1.Rows(kk).Cells(colDeposit).Value)
                                                Else
                                                    dblInnerInnerAmount = clsCommon.myCdbl(gv1.Rows(kk).Cells(colWithdrawal).Value)
                                                End If
                                                If dblInnerAmount = dblInnerInnerAmount Then
                                                    isFound = False
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                        If isFound Then
                                            gv1.CurrentColumn = gv1.Columns(colSelect)
                                            gv1.CurrentRow = gv1.Rows(jj)
                                            gv1.Rows(jj).Cells(colSelect).Value = True
                                            If dblOuterDate IsNot Nothing Then
                                                gv1.CurrentColumn = gv1.Columns(colReconcileDate)
                                                gv1.Rows(jj).Cells(colReconcileDate).Value = dblOuterDate
                                            End If
                                        End If

                                    End If
                                End If
                            End If
                        Next
                        If isFound Then
                            intRecoDocs += 1
                            gvImport.Rows(ii).Cells("IsOK").Value = 1
                        End If
                    End If
                Next
            End If
            clsCommon.ProgressBarPercentHide()
            If intRecoDocs > 0 Then
                clsCommon.MyMessageBoxShow(clsCommon.myCstr(intRecoDocs) + " Document(s) reconciled", Me.Text)
            Else
                clsCommon.MyMessageBoxShow("No Document reconciled", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Try
            If gvImport.Rows.Count <= 0 Then
                Throw New Exception("No Data found to export")
            End If
            gvImport.Columns("IsOK").IsVisible = True
            clsCommon.MyExportToExcelGrid("BankReco", gvImport, Nothing, Me.Text, True)
            gvImport.Columns("IsOK").IsVisible = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub PrintData()
        ArrBankReco_Arr = New ArrayList
        Dim Doc_No As String = ""
        Dim RecoId As String = ""
        RecoId = clsCommon.myCstr(fndRecoId.Value)
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), False) = CompairStringResult.Equal Then
                Doc_No = Doc_No + "','" + clsCommon.myCstr(grow.Cells(colDocNo).Value)

            End If
        Next
        If clsCommon.myLen(Doc_No) > 0 AndAlso clsCommon.myCstr(Doc_No).Substring(0, 3) = "','" Then
            Doc_No = Doc_No.Substring(3, Doc_No.Length - 3)
        End If
        Dim Qry As String = LoadPrintQuery(Doc_No, RecoId)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.Purchase, dt, "crptBankOutstanding", "Bank Outstanding Document Report")
            frmCRV = Nothing
        End If
    End Sub

    Public Function LoadPrintQuery(ByVal strDocNo, ByVal strBankRecoId) As String
        Dim Qry As String = " select tspl_BankReco_Head.Bank_Code ,tspl_bank_master.description as bank_Name,tspl_bank_master.add1 as Bank_Add1,tspl_bank_master.add2 as Bank_Add2,tspl_bank_master.add3 as Bank_add3, tspl_BankReco_Head.Reconciliation_Id ,convert(varchar,tspl_BankReco_Head.Reconciliation_Date,103) as Reconciliation_Date,tspl_BankReco_Head.Description as Bank_Desc,Document_No ,convert(varchar,Document_Date,103) as Document_Date ,Cheque_No ,convert(varchar,Cheque_Date,103) as Cheque_Date ,Deposit ,Withdrawal ,tspl_BankReco_Detail.Description as Particulars,convert(varchar,Statement_Date,103) as Statement_Date  ,Book_Balance ,Statement_Balance ,Withdrawal_OutstandingAmt ,Deposit_OutstandingAmt ,AdjustmentBook_Balance ,OutOf_Balance     ,Comp_name ,(case when ISNULL(TSPL_COMPANY_MASTER.ADD1,'')<> '' then TSPL_COMPANY_MASTER.ADD1 else '' end + case when ISNULL(TSPL_COMPANY_MASTER.ADD2,'')<> '' then ', ' +TSPL_COMPANY_MASTER.ADD2 else '' end +  case when ISNULL(TSPL_COMPANY_MASTER.ADD3,'')<> '' then ', ' +TSPL_COMPANY_MASTER.ADD3 else '' end ) as CompAddress " & _
                            " from tspl_BankReco_Detail left join tspl_BankReco_Head on tspl_BankReco_Head.Reconciliation_Id =tspl_BankReco_Detail.Reconciliation_Id " & _
                            " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = tspl_BankReco_Head.comp_code left join tspl_bank_master on tspl_bank_master.BANK_CODE =tspl_BankReco_Head.Bank_Code  where tspl_BankReco_Detail.Document_No in ('" + strDocNo + "')and tspl_BankReco_Head.Reconciliation_Id in ('" + strBankRecoId + "') order by convert(date,Document_Date,103) "
        Return Qry
    End Function

    Private Sub btnPrintOutstandingDoc_Click(sender As Object, e As EventArgs) Handles btnPrintOutstandingDoc.Click
        PrintData()
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            Dim qry As String = "select Post,Bank_Code,Reconciliation_Date  from tspl_BankReco_Head where Reconciliation_Id='" + fndRecoId.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Please select reconciliation.")
            Else
                If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Post")), "Y") = CompairStringResult.Equal Then
                    Throw New Exception("Status should be post to perform this operation")
                End If

                qry = "select Reconciliation_Id  from tspl_BankReco_Head where Post<>'Y' and Bank_Code='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'"
                Dim dtCheck As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtCheck IsNot Nothing AndAlso dtCheck.Rows.Count > 0 Then
                    Throw New Exception("Bank -'" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "' having unposted bank reconciliation i.e. " + clsCommon.myCstr(dtCheck.Rows(0)("Reconciliation_Id")))
                End If

                If settBankRecoCheckFutureDocuments Then
                    qry = "select Reconciliation_Id from tspl_BankReco_Head where  tspl_BankReco_Head.Reconciliation_Date>='" + clsCommon.GetPrintDate(clsCommon.myCDate((dt.Rows(0)("Reconciliation_Date")), "dd/MMM/yyyy")) + "' and Bank_Code='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "' and Reconciliation_Id not in ('" + fndRecoId.Value + "') order by Reconciliation_Date desc"
                    dtCheck = clsDBFuncationality.GetDataTable(qry)
                    If dtCheck IsNot Nothing AndAlso dtCheck.Rows.Count > 0 Then
                        Throw New Exception("Cannot reverse Bank Reco.Future Bank Reco found " + clsCommon.myCstr(dtCheck.Rows(0)("Reconciliation_Id")))
                    End If
                End If



                If clsCommon.MyMessageBoxShow(Me, "Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    '' REASON FOR Reverse 
                    Dim Reason As String = ""
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Reverse"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                    ''End of REASON FOR DELETE 
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        ''Delete AP Journal Entry 
                        qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + fndRecoId.Value + "' and Source_Code='BK-RE')"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No='" + fndRecoId.Value + "' and Source_Code='BK-RE'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        clsDBFuncationality.ExecuteNonQuery("update tspl_BankReco_Head set Post ='N' where Reconciliation_Id ='" + fndRecoId.Value + "'", trans)

                        saveCancelLog(Reason, "Reverse And Recreate", trans)
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndRecoId.Value, "tspl_BankReco_Head", "Reconciliation_Id", trans)
                        trans.Commit()
                        clsCommon.MyMessageBoxShow(Me, "Task done Successfully", Me.Text)
                        funFill(fndRecoId.Value)
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndRecoId.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        Try
            If Not UsLock1.Status = ERPTransactionStatus.Approved Then
                Throw New Exception("Transaction should be approved")
            End If
            Dim frm As New FrmBankRecoHide
            frm.strRecoID = fndRecoId.Value
            frm.strBankCode = fndBank.Value
            frm.strBankName = txtBankName.Text
            frm.ShowDialog()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
