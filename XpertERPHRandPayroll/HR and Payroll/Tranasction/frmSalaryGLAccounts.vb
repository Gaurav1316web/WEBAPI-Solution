Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmSalaryGLAccounts
    Inherits FrmMainTranScreen

    Public Const colDescription As String = "Description"
    Public Const colAccountCode As String = "AccountCode"
    Public Const colDebit As String = "Debit"
    Public Const colCredit As String = "Credit"
    Public Const colEmployer As String = "colEmployer"

    Public Sal_Gen_Code As String
    Public SalaryPayableAccount As String
    Public SalaryPayableAccountDesc As String
    Public GL_Employer_PF_PAYABLE As String
    Public GL_Employer_PF_PAYABLE_Desc As String
    Public GL_Employer_ESI_PAYABLE As String
    Public GL_Employer_ESI_PAYABLE_Desc As String
    Public GL_EMPLOYER_OTHERS_PAYABLE As String
    Public GL_EMPLOYER_OTHERS_PAYABLE_Desc As String
    Public PAY_PERIOD_CODE As String
    Public Proceed As Boolean = False
    Public SalaryPayableAmt As Decimal
    Public PFPayableAmt As Decimal
    Public ESIPayableAmt As Decimal
    Public OthrPayableAmt As Decimal

    Public Generate_Date As Date
    Public Remarks As String
    Public ChequeNo As String
    Public ChequeDated As Date
    Public Location_Code As String
    Public Division_Code As String = ""
    Public arrAccGL As New List(Of clsSalaryFEAccounts)
    'Public Trans As SqlTransaction = Nothing




    Private Sub frmSalaryGLAccounts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'gv1.DataSource = LoadSalaryGLAccounts()
        LoadGridColumns()
        loadSalaryGLAccounts()


    End Sub
    Sub LoadGridColumns()
        Dim Description As New GridViewTextBoxColumn
        Dim AccountCode As New GridViewTextBoxColumn
        Dim Debit As New GridViewDecimalColumn
        Dim Credit As New GridViewDecimalColumn
        Dim Employer As New GridViewTextBoxColumn

        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Description.FormatString = ""
        Description.HeaderText = "Description"
        Description.Name = colDescription
        Description.Width = 100
        Description.ReadOnly = True
        Description.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(Description)

        AccountCode.FormatString = ""
        AccountCode.HeaderText = "AccountCode"
        AccountCode.Name = colAccountCode
        AccountCode.Width = 100
        AccountCode.ReadOnly = True
        AccountCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(AccountCode)

        Debit.FormatString = ""
        Debit.HeaderText = "Debit"
        Debit.Name = colDebit
        Debit.Width = 100
        Debit.ReadOnly = True
        Debit.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Debit)

        Credit.FormatString = ""
        Credit.HeaderText = "Credit"
        Credit.Name = colCredit
        Credit.Width = 100
        Credit.ReadOnly = True
        Credit.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Credit)

        Employer.FormatString = ""
        Employer.HeaderText = "Employer"
        Employer.Name = colEmployer
        Employer.Width = 100
        Employer.ReadOnly = True
        Employer.IsVisible = False
        Employer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Employer)



    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Proceed = False
        Me.Close()
    End Sub
    Function GetSalaryGLAccountsDT() As DataTable
        Dim strq As String
        strq = "select (case when ISEARNING=1 or Is_Emplr in (1,2,3) then TSPL_GL_ACCOUNTS.Description  else 'To ' " & _
               " + TSPL_GL_ACCOUNTS.Description end) as 'Description',salary.Account_Code, " & _
               " salary.Debit,salary.Credit,Is_Emplr  from ( " & _
               " select isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0) as ISEARNING,TSPL_GENERATE_SALARY_PAYHEADS.Account_Code, " & _
               " SUM((case when isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0)=1 then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else 0 end)) as Debit," & _
               " SUM((case when isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0)<>1 then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else 0 end)) as Credit,0 as Is_Emplr  " & _
               " from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY " & _
               " on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE " & _
               " left join TSPL_PAYHEAD_MASTER on TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE " & _
               " left join TSPL_GL_ACCOUNTS on TSPL_PAYHEAD_MASTER.Account_Code=TSPL_GL_ACCOUNTS.Account_Code " & _
               " where TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE='" & Sal_Gen_Code & "' group by TSPL_GENERATE_SALARY_PAYHEADS.Account_Code,TSPL_PAYHEAD_MASTER.ISEARNING " & _
               " Union All " & _
               " select isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0) as ISEARNING,TSPL_GENERATE_SALARY_PAYHEADS.Employer_Account, " & _
               " Ceiling(SUM((case when TSPL_GENERATE_SALARY_PAYHEADS.Sub_Head_Type ='EPF' then (TSPL_GENERATE_SALARY_PAYHEADS.CoEPF_AMT_AC01+TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10) when TSPL_GENERATE_SALARY_PAYHEADS.Sub_Head_Type ='EMPESI' THEN TSPL_GENERATE_SALARY_PAYHEADS.Co_ESI_AMT when TSPL_GENERATE_SALARY_PAYHEADS.Sub_Head_Type ='LWF' then TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount*2 else 0 end))) as Debit," & _
               " 0 as Credit ,(case when TSPL_GENERATE_SALARY_PAYHEADS.Sub_Head_Type ='EPF' then 1 when TSPL_GENERATE_SALARY_PAYHEADS.Sub_Head_Type ='EMPESI' then 2  when TSPL_GENERATE_SALARY_PAYHEADS.Sub_Head_Type ='LWF' then 3 else 0 end) as Is_Emplr " & _
               " from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY " & _
               " on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE " & _
               " left join TSPL_PAYHEAD_MASTER on TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE " & _
               " where TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE='" & Sal_Gen_Code & "' and TSPL_GENERATE_SALARY_PAYHEADS.Sub_Head_Type in ('EPF','EMPESI','LWF') group by TSPL_GENERATE_SALARY_PAYHEADS.Employer_Account,TSPL_PAYHEAD_MASTER.ISEARNING,TSPL_GENERATE_SALARY_PAYHEADS.Sub_Head_Type" & _
               " ) as salary " & _
               " left join TSPL_GL_ACCOUNTS on salary.Account_Code=TSPL_GL_ACCOUNTS.Account_Code where  salary.Account_Code is not null and (salary.Debit>0 or salary.Credit>0) order by is_emplr "
        Dim dtGL As DataTable
        'Dim frmtr As frmSalaryGeneration
        dtGL = clsDBFuncationality.GetDataTable(strq, Nothing)
        Return dtGL

    End Function
    Sub loadSalaryGLAccounts()
        '' changes done by Panch Raj agaist Ticket No - BM00000007674,BM00000008112 (23/10/2015) 
        Dim dt As DataTable

        Dim totalDebit As Decimal = 0
        Dim totalCredit As Decimal = 0

        Dim totalDebitSalary As Decimal = 0
        Dim totalCreditSalary As Decimal = 0

        Dim totalDebitPF As Decimal = 0
        Dim totalCreditPF As Decimal = 0

        Dim totalDebitEsi As Decimal = 0
        Dim totalCreditEsi As Decimal = 0

        Dim totalDebitOthr As Decimal = 0
        Dim totalCreditOthr As Decimal = 0

        dt = GetSalaryGLAccountsDT()
        Dim DivCond As String = ""
        If clsCommon.myLen(Division_Code) > 0 Then
            DivCond = " and EMP.DEVISION_CODE in ('" & Division_Code & "' )"
        End If
        '" & FndLocationCode.Value & "'
        Dim Loc As String = "('" & Location_Code & "')"

        Dim dtEmplr As DataTable = clsDBFuncationality.GetDataTable(clsSalaryGeneration.GetPFESIQuery(PAY_PERIOD_CODE, Loc, DivCond, "", "", "", ""))
        For Each dr As DataRow In dt.Rows
            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDescription).Value = clsCommon.myCstr(dr.Item("Description"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountCode).Value = clsCommon.myCstr(dr.Item("Account_Code"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value = clsCommon.myCdbl(dr.Item("debit"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value = clsCommon.myCdbl(dr.Item("Credit"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colEmployer).Value = clsCommon.myCdbl(dr.Item("Is_Emplr"))
            If clsCommon.myCdbl(dr.Item("Is_Emplr")) = 0 Then
                totalDebitSalary = totalDebitSalary + gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value
                totalCreditSalary = totalCreditSalary + gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value

                totalDebit = totalDebit + gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value
                totalCredit = totalCredit + gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value
            ElseIf clsCommon.myCdbl(dr.Item("Is_Emplr")) = 1 Then
                Dim Penson_10 As Decimal = 0
                Dim Diff_01 As Decimal = 0
                Dim AdminAmtAc02 As Decimal = 0
                Dim EDLIAmtAc21 As Decimal = 0
                Dim AdminEDLIAmtAc22 As Decimal = 0

                If dtEmplr.Rows.Count > 0 Then
                    Penson_10 = Math.Round(clsCommon.myCdbl(dtEmplr.Rows(0).Item("PensionAmtAc10")), 0, MidpointRounding.AwayFromZero)
                    Diff_01 = Math.Round(clsCommon.myCdbl(dtEmplr.Rows(0).Item("DifferenceAmtAc01")), 0, MidpointRounding.AwayFromZero)
                    AdminAmtAc02 = Math.Round(clsCommon.myCdbl(dtEmplr.Rows(0).Item("AdminAmtAc02")), 0, MidpointRounding.AwayFromZero)
                    EDLIAmtAc21 = Math.Round(clsCommon.myCdbl(dtEmplr.Rows(0).Item("EDLIAmtAc21")), 0, MidpointRounding.AwayFromZero)
                    AdminEDLIAmtAc22 = Math.Round(clsCommon.myCdbl(dtEmplr.Rows(0).Item("AdminEDLIAmtAc22")), 0, MidpointRounding.AwayFromZero)
                End If
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value = Penson_10 + Diff_01 + AdminAmtAc02 + EDLIAmtAc21 + AdminEDLIAmtAc22
                totalDebitPF = totalDebitPF + gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value
                totalCreditPF = totalCreditPF + gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value

                totalDebit = totalDebit + gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value
                totalCredit = totalCredit + gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value

                PFPayableAmt = (totalDebitPF - totalCreditPF)
                If PFPayableAmt > 0 Then
                    gv1.Rows.AddNew()
                    GL_Employer_PF_PAYABLE = clsERPFuncationality.ChangeGLAccountLocationSegment(GL_Employer_PF_PAYABLE, Location_Code, False, Nothing)
                    GL_Employer_PF_PAYABLE_Desc = clsGLAccount.GetName(GL_Employer_PF_PAYABLE, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDescription).Value = "To " & GL_Employer_PF_PAYABLE_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountCode).Value = GL_Employer_PF_PAYABLE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value = PFPayableAmt
                    totalDebit = totalDebit + gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value
                    totalCredit = totalCredit + gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value
                End If

            ElseIf clsCommon.myCdbl(dr.Item("Is_Emplr")) = 2 Then
                Dim EmployerESIAMT As Decimal = 0
                If dtEmplr.Rows.Count > 0 Then
                    EmployerESIAMT = Math.Round(clsCommon.myCdbl(dtEmplr.Rows(0).Item("EmployerESIAMT")), 0, MidpointRounding.AwayFromZero)
                End If
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value = EmployerESIAMT

                totalDebitEsi = totalDebitEsi + gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value
                totalCreditEsi = totalCreditEsi + gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value

                totalDebit = totalDebit + gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value
                totalCredit = totalCredit + gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value

                ESIPayableAmt = (totalDebitEsi - totalCreditEsi)
                If ESIPayableAmt > 0 Then
                    gv1.Rows.AddNew()
                    GL_Employer_ESI_PAYABLE = clsERPFuncationality.ChangeGLAccountLocationSegment(GL_Employer_ESI_PAYABLE, Location_Code, False, Nothing)
                    GL_Employer_ESI_PAYABLE_Desc = clsGLAccount.GetName(GL_Employer_ESI_PAYABLE, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDescription).Value = "To " & GL_Employer_ESI_PAYABLE_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountCode).Value = GL_Employer_ESI_PAYABLE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value = ESIPayableAmt
                    totalDebit = totalDebit + gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value
                    totalCredit = totalCredit + gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value
                End If

            ElseIf clsCommon.myCdbl(dr.Item("Is_Emplr")) = 3 Then
                totalDebitOthr = totalDebitOthr + gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value
                totalCreditOthr = totalCreditOthr + gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value

                totalDebit = totalDebit + gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value
                totalCredit = totalCredit + gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value
                OthrPayableAmt = (totalDebitOthr - totalCreditOthr)
                If OthrPayableAmt > 0 Then
                    gv1.Rows.AddNew()
                    GL_EMPLOYER_OTHERS_PAYABLE = clsERPFuncationality.ChangeGLAccountLocationSegment(GL_EMPLOYER_OTHERS_PAYABLE, Location_Code, False, Nothing)
                    GL_EMPLOYER_OTHERS_PAYABLE_Desc = clsGLAccount.GetName(GL_EMPLOYER_OTHERS_PAYABLE, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDescription).Value = "To " & GL_EMPLOYER_OTHERS_PAYABLE_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountCode).Value = GL_EMPLOYER_OTHERS_PAYABLE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value = OthrPayableAmt
                    totalDebit = totalDebit + gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value
                    totalCredit = totalCredit + gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value
                End If
            End If
        Next
        SalaryPayableAmt = (totalDebitSalary - totalCreditSalary)
        gv1.Rows.AddNew()
        SalaryPayableAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(SalaryPayableAccount, Location_Code, False, Nothing)
        SalaryPayableAccountDesc = clsGLAccount.GetName(SalaryPayableAccount, Nothing)
        gv1.Rows(gv1.Rows.Count - 1).Cells(colDescription).Value = "To " & SalaryPayableAccountDesc
        gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountCode).Value = SalaryPayableAccount
        gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value = 0
        gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value = SalaryPayableAmt
        totalDebit = totalDebit + gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value
        totalCredit = totalCredit + gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value

        gv1.Rows.AddNew()
        gv1.Rows(gv1.Rows.Count - 1).Cells(colDescription).Value = "Total "
        gv1.Rows(gv1.Rows.Count - 1).Cells(colDebit).Value = totalDebit
        gv1.Rows(gv1.Rows.Count - 1).Cells(colCredit).Value = totalCredit
        Me.txtSalariesPayableAmt.Text = SalaryPayableAmt
        Me.txtToBanksAmt.Text = SalaryPayableAmt

    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Dim salgen As New frmSalaryGeneration
        salgen.SetUserMgmt(clsUserMgtCode.frmSalaryGeneration)
        'If funInsertSalaryGL(True, Nothing) = True Then
        '    Proceed = True
        '    frmSalaryGeneration.Proceed = True
        '    frmSalaryGeneration.SalaryPayableAmt = clsCommon.myCdbl(Me.txtSalariesPayableAmt.Text)
        '    Me.Close()
        'End If
        Try
            'If Trans Is Nothing OrElse Trans.Connection Is Nothing Then
            '    Trans = clsDBFuncationality.GetTransactin()
            'End If

            'If funInsertSalaryPayment(True, Trans) = True Then
            '    Trans.Commit()
            '    Proceed = True
            '    frmSalaryGeneration.Proceed = True
            '    frmSalaryGeneration.SalaryPayableAmt = clsCommon.myCdbl(Me.txtSalariesPayableAmt.Text)
            '    Me.Close()
            'Else
            '    Proceed = False
            '    frmSalaryGeneration.Proceed = False
            '    Me.Close()
            'End If
            If FunCreateJournalEntryForSalary(True, "") = True Then
                'Trans.Commit()
                Proceed = True
                salgen.Proceed = True
                salgen.SalaryPayableAmt = clsCommon.myCdbl(Me.txtSalariesPayableAmt.Text)
                Me.Close()
            Else
                Proceed = False
                salgen.Proceed = False
                Me.Close()
            End If
        Catch ex As Exception
            'Trans.Rollback()
            Proceed = False
            salgen.Proceed = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

            Me.Close()
        End Try



    End Sub
    Private Function FunCreateJournalEntryForSalary(ByVal ChekPostBtn As Boolean, ByVal strVoucherNoifExists As String) As Boolean
        'Dim arrmis As New ArrayList()
        Dim sourceType As String = "PL-JE"
        Dim sourceDesc As String = "Payroll Journal Entry"
        Dim obj As New clsSalaryFEAccounts
        For Each grow As GridViewRowInfo In gv1.Rows
            If (grow.Index + 1) > (gv1.Rows.Count - 1) Then
                Exit For
            End If
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colAccountCode).Value)) <= 0 Then
                Continue For
            End If
            'Dim strAccountLocation As String = Location_Code
            'Dim dblAmount As Double = clsCommon.myCdbl(grow.Cells(colDebit).Value) - clsCommon.myCdbl(grow.Cells(colCredit).Value)
            'If clsCommon.myCdbl(grow.Cells(colDebit).Value) > 0 Then
            '    Dim acc3() As String = {clsCommon.myCstr(grow.Cells(colAccountCode).Value), clsCommon.myCdbl(grow.Cells(colDebit).Value), clsCommon.myCstr(grow.Cells(colDescription).Value)}
            '    arrAccGL.Add(acc3)
            'Else
            '    Dim Acc4() As String = {clsCommon.myCstr(grow.Cells(colAccountCode).Value), -1 * clsCommon.myCdbl(grow.Cells(colCredit).Value), clsCommon.myCstr(grow.Cells(colDescription).Value)}
            '    arrAccGL.Add(Acc4)
            'End If
            obj = New clsSalaryFEAccounts
            obj.SALARY_GENERATION_CODE = Sal_Gen_Code
            obj.AccountCode = clsCommon.myCstr(grow.Cells(colAccountCode).Value)
            obj.Description = clsCommon.myCstr(grow.Cells(colDescription).Value)
            obj.DEBIT = clsCommon.myCdbl(grow.Cells(colDebit).Value)
            obj.CREDIT = clsCommon.myCdbl(grow.Cells(colCredit).Value)
            obj.IS_EMPLOYER = clsCommon.myCdbl(grow.Cells(colEmployer).Value)
            arrAccGL.Add(obj)
        Next
        Return clsSalaryFEAccounts.SaveData(Sal_Gen_Code, arrAccGL)
        'Dim SalaryPayment_Date As Date = clsPayPeriodMaster.GetToDate(PAY_PERIOD_CODE, trans)
        'Dim paymentDesc As String = "Payment of salary for the month of " & PAY_PERIOD_CODE & ""
        'Return clsJournalMaster.FunGrnlEntryWithTrans(Location_Code, True, strVoucherNoifExists, trans, SalaryPayment_Date, paymentDesc, sourceType, sourceDesc, Sal_Gen_Code, Remarks, "", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrmis, , Remarks, Remarks)

    End Function
    'Private Function funInsertSalaryGL(ByVal ChekPostBtn As Boolean, ByVal trans As SqlTransaction) As Boolean
    '    Dim i As Integer = 0
    '    Dim myreader As DataTable
    '    'Dim SourceCode As String
    '    'Dim SourceCodeDesc As String
    '    Dim Source_Doc_No As String = "0"

    '    'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        Dim strFirstAccCode As String = clsCommon.myCstr(SalaryPayableAccount)
    '        strFirstAccCode = strFirstAccCode.Substring(clsCommon.myLen(strFirstAccCode) - 3, 3)
    '        Dim frm As frmJournalEntry
    '        frm = New frmJournalEntry(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)

    '        Dim StrVoucher As String = "" '' frm.fnAutoGenerateNo(trans, clsCommon.myCDate(GENERATE_DATE, "dd/MM/yyyy"), True, strFirstAccCode)
    '        StrVoucher = clsERPFuncationality.GetNextCode(Nothing, clsCommon.myCDate(clsCommon.GETSERVERDATE()), clsDocType.JournalEntry, "", "")
    '        Dim strSrcType As String
    '        strSrcType = "O"                 '****** for: Others

    '        Dim strEntryType As String
    '        strEntryType = "N"               '****** for: Normal Entry

    '        Dim strRvrs As String
    '        strRvrs = "N"                    '****** For: Auto-Reverse is False

    '        Dim strJrnl As String = "select (case when max(journal_no) is not null then max(journal_no) else 0 end) from TSPL_JOURNAL_MASTER "
    '        Dim Jrnl As String = CInt(connectSql.RunScalar(trans, strJrnl)) + 1

    '        Dim ReverseDate As String = ""
    '        'If chkReverse.Checked = True Then
    '        '    ReverseDate = clsCommon.GetPrintDate(dtRevese.Value, "dd/MM/yyyy")
    '        'End If

    '        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", clsCommon.GetPrintDate(Generate_Date, "dd/MMM/yyyy")), New SqlParameter("@Source_Code", SourceCode), New SqlParameter("@Source_Desc", SourceCodeDesc), New SqlParameter("@Source_Doc_No", Source_Doc_No), New SqlParameter("@Source_Doc_Date", clsCommon.GetPrintDate(Generate_Date, "dd/MMM/yyyy")), New SqlParameter("@Posting_Date", clsCommon.GetPrintDate(Generate_Date, "dd/MMM/yyyy")), New SqlParameter("@Voucher_Desc", "Salary for month '" & PAY_PERIOD_CODE & "'"), New SqlParameter("@Source_Narration", SourceCodeDesc), New SqlParameter("@Remarks", Remarks), New SqlParameter("@Comments", Remarks), New SqlParameter("@Auto_Reverse", strRvrs), New SqlParameter("@Reverse_Date", ReverseDate), New SqlParameter("@Source_Type", strSrcType), New SqlParameter("@CustVend_Code", ""), New SqlParameter("@CustVend_Name", ""), New SqlParameter("@Transaction_Type", strEntryType), New SqlParameter("@Total_Debit_Amt", SalaryPayableAmt), New SqlParameter("@Total_Credit_Amt", SalaryPayableAmt), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))

    '        Dim strJrnl1 As String = "select journal_no from TSPL_JOURNAL_MASTER where Voucher_No='" + StrVoucher + "'"
    '        Dim Jrnl1 As String
    '        Jrnl1 = connectSql.RunScalar(trans, strJrnl1)


    '        For i = 0 To 1
    '            Dim strAccCode As String

    '            strAccCode = IIf(i = 0, SalaryPayableAccount, BankGLAccount) ''gv1.Rows(i).Cells(colAccountCode).Value
    '            If clsCommon.myLen(strAccCode) > 0 Then
    '                'Dim LineNo As Integer = i + 1
    '                'Dim strAccDesc As String = gv1.Rows(i).Cells(colDescription).Value

    '                'Dim Amt As Decimal
    '                'Dim strAmtDr As Decimal = gv1.Rows(i).Cells(colDebit).Value
    '                'Dim strAmtCr As Decimal = gv1.Rows(i).Cells(colCredit).Value
    '                'If strAmtDr = 0 Then
    '                '    Amt = strAmtCr * -1
    '                'ElseIf strAmtCr = 0 Then
    '                '    Amt = strAmtDr
    '                'End If

    '                Dim LineNo As Integer = IIf(i = 0, 1, 2)
    '                Dim strAccDesc As String = IIf(i = 0, SalaryPayableAccountDesc, BankGLAccountDesc)

    '                Dim Amt As Decimal
    '                Dim strAmtDr As Decimal = IIf(i = 0, SalaryPayableAmt, 0)
    '                Dim strAmtCr As Decimal = IIf(i = 0, 0, SalaryPayableAmt)
    '                If strAmtDr = 0 Then
    '                    Amt = strAmtCr * -1
    '                ElseIf strAmtCr = 0 Then
    '                    Amt = strAmtDr
    '                End If

    '                Dim strDesc As String = Remarks
    '                Dim strRef As String = "Salary"
    '                Dim PostDate As String = Generate_Date

    '                Dim strQ1 As String = " SELECT     Account_Type, Account_Group_Code, Account_Seg_Code1, Account_Seg_Desc1, Account_Seg_Code2, Account_Seg_Desc2, Account_Seg_Code3, " & _
    '                     " Account_Seg_Desc3, Account_Seg_Code4, Account_Seg_Desc4, Account_Seg_Code5, Account_Seg_Desc5, Account_Seg_Code6, Account_Seg_Desc6," & _
    '                     " Account_Seg_Code7, Account_Seg_Desc7, Account_Seg_Code8, Account_Seg_Desc8, Account_Seg_Code9, Account_Seg_Desc9, " & _
    '                     " Account_Seg_Code10, Account_Seg_Desc10 FROM  TSPL_GL_ACCOUNTS where Account_Code='" + strAccCode + "'"
    '                myreader = clsDBFuncationality.GetDataTable(strQ1, trans)

    '                If (myreader IsNot Nothing AndAlso myreader.Rows.Count > 0) Then

    '                    For Each dr As DataRow In myreader.Rows

    '                        Dim AccType As String = dr(0).ToString()
    '                        Dim AccGrp As String = dr(1).ToString()

    '                        Dim SegC1 As String = dr(2).ToString()
    '                        Dim SegDesc1 As String = dr(3).ToString()

    '                        Dim SegC2 As String = dr(4).ToString()
    '                        Dim SegDesc2 As String = dr(5).ToString()

    '                        Dim SegC3 As String = dr(6).ToString()
    '                        Dim SegDesc3 As String = dr(7).ToString()

    '                        Dim SegC4 As String = dr(8).ToString()
    '                        Dim SegDesc4 As String = dr(9).ToString()

    '                        Dim SegC5 As String = dr(10).ToString()
    '                        Dim SegDesc5 As String = dr(11).ToString()

    '                        Dim SegC6 As String = dr(12).ToString()
    '                        Dim SegDesc6 As String = dr(13).ToString()

    '                        Dim SegC7 As String = dr(14).ToString()
    '                        Dim SegDesc7 As String = dr(15).ToString()

    '                        Dim SegC8 As String = dr(16).ToString()
    '                        Dim SegDesc8 As String = dr(17).ToString()

    '                        Dim SegC9 As String = dr(18).ToString()
    '                        Dim SegDesc9 As String = dr(19).ToString()

    '                        Dim SegC10 As String = dr(20).ToString()
    '                        Dim SegDesc10 As String = dr(21).ToString()

    '                        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl1), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", clsCommon.GetPrintDate(Generate_Date, "dd/MMM/yyyy")), New SqlParameter("@Detail_Line_No", LineNo), New SqlParameter("@Account_code", strAccCode), New SqlParameter("@Account_Desc", strAccDesc), New SqlParameter("@Amount", Amt), New SqlParameter("@Description", strDesc), New SqlParameter("@Reference", strRef), New SqlParameter("@Posting_Date", clsCommon.GetPrintDate(PostDate, "dd/MMM/yyyy")), New SqlParameter("@Account_Type", AccType), New SqlParameter("@Account_Group_Code", AccGrp), New SqlParameter("@Account_Seg_Code1", SegC1), New SqlParameter("@Account_Seg_Desc1", SegDesc1), New SqlParameter("@Account_Seg_Code2", SegC2), New SqlParameter("@Account_Seg_Desc2", SegDesc2), New SqlParameter("@Account_Seg_Code3", SegC3), New SqlParameter("@Account_Seg_Desc3", SegDesc3), New SqlParameter("@Account_Seg_Code4", SegC4), New SqlParameter("@Account_Seg_Desc4", SegDesc4), New SqlParameter("@Account_Seg_Code5", SegC5), New SqlParameter("@Account_Seg_Desc5", SegDesc5), New SqlParameter("@Account_Seg_Code6", SegC6), New SqlParameter("@Account_Seg_Desc6", SegDesc6), New SqlParameter("@Account_Seg_Code7", SegC7), New SqlParameter("@Account_Seg_Desc7", SegDesc7), New SqlParameter("@Account_Seg_Code8", SegC8), New SqlParameter("@Account_Seg_Desc8", SegDesc8), New SqlParameter("@Account_Seg_Code9", SegC9), New SqlParameter("@Account_Seg_Desc9", SegDesc9), New SqlParameter("@Account_Seg_Code10", SegC10), New SqlParameter("@Account_Seg_Desc10", SegDesc10))
    '                    Next
    '                End If
    '            End If
    '        Next
    '        'trans.Commit()
    '        Return True
    '        If ChekPostBtn = True Then
    '        Else
    '            myMessages.insert()
    '        End If

    '    Catch ex As Exception
    '        'trans.Rollback()
    '        common.clsCommon.MyMessageBoxShow(ex.Message, "Journal-Entry", MessageBoxButtons.OK)
    '        Return False
    '    End Try
    'End Function
    'Private Function funInsertSalaryPayment(ByVal ChekPostBtn As Boolean, ByVal trans As SqlTransaction) As Boolean
    '    'Try
    '    'If (AllowToSave()) Then
    '    Dim obj As New clsPaymentHeader()
    '    obj.Payment_No = ""
    '    obj.Entry_Desc = "Payment of salary for the month of " & PAY_PERIOD_CODE & ""
    '    obj.Payment_Date = clsCommon.GETSERVERDATE(trans)
    '    obj.Payment_Post_Date = obj.Payment_Date
    '    obj.Bank_Code = BankCode
    '    obj.Payment_Type = "MI"
    '    obj.Vendor_Code = ""
    '    obj.Vendor_Name = ""
    '    obj.Payment_Code = "Cheque"
    '    'obj.Location_Code = Location_Code
    '    If clsCommon.CompairString(obj.Payment_Code, "Cheque") = CompairStringResult.Equal Then
    '        obj.Cheque_No = ChequeNo
    '        obj.Cheque_Date = clsCommon.myCDate(ChequeDated)

    '        'If chkPDC.Checked Then
    '        '    obj.PDC_Cheque = "Y"
    '        'End If
    '    Else
    '        obj.Cheque_No = ""
    '        obj.Cheque_Date = Nothing
    '    End If


    '    obj.Payment_Amount = clsCommon.myCdbl(txtSalariesPayableAmt.Text)
    '    obj.Total_Applied_Amount = clsCommon.myCdbl(txtSalariesPayableAmt.Text)
    '    obj.Remit_To = ""
    '    obj.Loadout_No = ""

    '    obj.IsChkReverse = "N"
    '    obj.Bank_Charges = 0 'clsCommon.myCdbl(txtBankCharges.Text)
    '    obj.objRemittance = Nothing

    '    'If chkCForm.Checked = True Then
    '    '    obj.CFormRecd = "Y"
    '    'Else
    '    '    obj.CFormRecd = "N"
    '    'End If
    '    obj.CFormRecd = "N"
    '    obj.CForm_InvoiceNo = ""

    '    obj.ArrTr = New List(Of clsPaymentDetail)

    '    ''For Custom Fields
    '    obj.Form_ID = MyBase.Form_ID
    '    obj.arrCustomFields = New List(Of clsCustomFieldValues)
    '    'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
    '    '    UcCustomFields1.GetData(obj.arrCustomFields)
    '    'End If
    '    ''End of For Custom Fields

    '    '============================Detail Section==============================
    '    If clsCommon.CompairString(obj.Payment_Type, "PY") = CompairStringResult.Equal Then

    '    ElseIf clsCommon.CompairString(obj.Payment_Type, "MI") = CompairStringResult.Equal Then
    '        Dim ESiAmt As Decimal = 0.0
    '        Dim MiscAmt As Decimal = 0.0
    '        Dim ESI_Percent As Decimal = 0.0
    '        For i As Integer = 0 To gv1.Rows.Count - 1
    '            If Not IsDBNull(gv1.Rows(i).Cells(colAccountCode).Value) Then
    '                If clsCommon.myLen(gv1.Rows(i).Cells(colAccountCode).Value) > 0 Then
    '                    If clsCommon.myCdbl(gv1.Rows(i).Cells(3).Value) > 0 Then
    '                        MiscAmt = MiscAmt + (clsCommon.myCdbl(gv1.Rows(i).Cells(colDebit).Value) + clsCommon.myCdbl(gv1.Rows(i).Cells(colCredit).Value))
    '                        ESiAmt = ESiAmt + (clsCommon.myCdbl(gv1.Rows(i).Cells(colDebit).Value) + clsCommon.myCdbl(gv1.Rows(i).Cells(colCredit).Value)) * -1
    '                    End If
    '                End If
    '            End If
    '        Next
    '        If MiscAmt = 0 Then
    '            ESI_Percent = 0
    '        Else
    '            ESI_Percent = (ESiAmt / MiscAmt) * 100
    '        End If

    '        For Each grow As GridViewRowInfo In gv1.Rows
    '            If (grow.Index + 1) > (gv1.Rows.Count - 2) Then
    '                Exit For
    '            End If
    '            If clsCommon.myLen(grow.Cells(colAccountCode).Value) > 0 Then
    '                Dim objTr As New clsPaymentDetail()
    '                objTr.Payment_Type = obj.Payment_Type
    '                objTr.Account_Code = clsCommon.myCstr(grow.Cells(colAccountCode).Value)
    '                objTr.Description = clsCommon.myCstr(grow.Cells(colDescription).Value)
    '                'objTr.Applied_Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
    '                objTr.Net_Balance = clsCommon.myCdbl(grow.Cells(colDebit).Value) - clsCommon.myCdbl(grow.Cells(colCredit).Value)
    '                objTr.Remarks = clsCommon.myCstr(grow.Cells(colDescription).Value)
    '                objTr.ESI_WCT_Percentage = ESI_Percent
    '                obj.ArrTr.Add(objTr)
    '            End If
    '        Next
    '        'If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
    '        '    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colGLAccount)
    '        'End If
    '    End If
    '    '==================Detail Section Ends Here=======================

    '    '' CurrencyConversion

    '    obj.CURRENCY_CODE = Nothing
    '    obj.ConvRate = 1
    '    obj.ConvRateOld = 1
    '    obj.ApplicableFrom = Nothing
    '    obj.BASE_CURRENCY_CODE = Nothing
    '    obj.PAYMENT_AMOUNT_BASE_CURRENCY = clsCommon.myCdbl(Me.txtSalariesPayableAmt.Text)
    '    obj.EXCHANGE_LOSS_AMT = 0
    '    obj.EXCHANGE_GAIN_AMT = 0
    '    obj.EXCHANGE_GAIN_ACCOUNT = Nothing
    '    obj.EXCHANGE_LOSS_ACCOUNT = Nothing
    '    '' end CurrencyConversion
    '    Try
    '        Return clsPaymentHeader.PostData(obj.SaveDataWithPaymentNo(obj, True, trans).Payment_No, Me.Module_Code, trans)
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '        Return False
    '    End Try


    '    'UcAttachment1.SaveData(obj.Payment_No)
    '    'LoadData(obj.Payment_No, NavigatorType.Current)
    '    'End If
    '    'Catch ex As Exception
    '    '    Throw New Exception(ex.Message)
    '    'End Try

    'End Function
End Class