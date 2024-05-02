Imports common
Imports System.Data.SqlClient
Imports System.IO
'' work done agaist ticket no. BHA/04/10/18-000597 

Public Class RptCustomerAccount
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As New ToolTip()
    Const colSno As String = "colSno"
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colCustGroup As String = "colCustGroup"
    Const colAccountCode As String = "colAccountCode"
    Const colAccountDesc As String = "colAccountCodeDesc"
    Const colAccount1 As String = "colAccount1"
    Const colAccount1Desc As String = "colAccount1Desc"
    Const colAccount2 As String = "colAccount2"
    Const colAccount2Desc As String = "colAccount2Desc"

    Const colAdvance As String = "colAdvance"
    Const colAdvanceDesc As String = "colAdvanceDesc"
    Const colBankOtherCharges As String = "colBankOtherCharges"
    Const colBankOtherChargesDesc As String = "colBankOtherChargesDesc"
    Const colBankGuarantee As String = "colBankGuarantee"
    Const colBankGuaranteeDesc As String = "colBankGuaranteeDesc"

    Const colConsignmentAcct As String = "colConsignmentAcct"
    Const colContainerDeposit As String = "colContainerDeposit"
    Const colContainerDepositDesc As String = "colContainerDepositDesc"
    Const colCrateSecurity As String = "colCrateSecurity"
    Const colCrateSecurityDesc As String = "colCrateSecurityDesc"
    Const colExchangeGain As String = "colExchangeGain"
    Const colExchangeGainDesc As String = "colExchangeGainDesc"
    Const colExchangeLoss As String = "colExchangeLoss"
    Const colExchangeLossDesc As String = "colExchangeLossDesc"
    Const colForeignBankCharges As String = "colForeignBankCharges"
    Const colForeignBankChargesDesc As String = "colForeignBankChargesDesc"

    Const colGainAcct As String = "colGainAcct"
    Const colGainAcctDesc As String = "colGainAcctDesc"
    Const colGOSC As String = "colGOSC"
    Const colGOSCDesc As String = "colGOSCDesc"
    Const colLeakageDeduction As String = "colLeakageDeduction"
    Const colLeakageDeductionDesc As String = "colLeakageDeductionDesc"
    Const colPenaltyCharges As String = "colPenaltyCharges"
    Const colPenaltyChargesDesc As String = "colPenaltyChargesDesc"
    Const colReceipt As String = "colReceipt"
    Const colReceiptDesc As String = "colReceiptDesc"
    Const colDebtorControl As String = "colDebtorControl"
    Const colDebtorControlDesc As String = "colDebtorControlDesc"
    Const colSecurity As String = "colSecurity"
    Const colSecurityDesc As String = "colSecurityDesc"
    Const colSubsidy As String = "colSubsidy"
    Const colSubsidyDesc As String = "colSubsidyDesc"
    Const colWriteoffs As String = "colWriteoffs"
    Const colWriteoffsDesc As String = "colWriteoffsDesc"
    Public isInsideLoadData As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnexport.Visible = MyBase.isExport
        btnprint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub FrmItemListRpt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            btnprint.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmItemListRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FunReset()

        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R for reset window")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for view report")
    End Sub



    Private Sub FunReset()
        gv.Columns.Clear()
        gv.DataSource = Nothing

        txtItemType.arrValueMember = Nothing
        txtPurchaseSet.arrValueMember = Nothing
        txtCustomerGroup.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        isInsideLoadData = False
        gv.EnableFiltering = False
        chkOnlyview.Checked = False
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Print(Exporter.Refresh)
    End Sub
    Private Sub Printbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Printbtn.Click
        Print(Exporter.Print)
    End Sub
    Enum Exporter
        Print = 2
        Refresh = 1
        PDF = 3
        Export = 4
    End Enum
    Sub Print(ByVal IsPrint As Exporter)
        Try
            isInsideLoadData = False
            If IsPrint = Exporter.Print OrElse IsPrint = Exporter.Refresh Then


                Dim qry As String = "select TSPL_CUSTOMER_Master.Cust_Code as [Customer Code],TSPL_CUSTOMER_Master.Customer_Name as [Customer Name],TSPL_CUSTOMER_Master.cust_group_code as [Customer Group],TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account as [Account Code],TSPL_CUSTOMER_ACCOUNT_SET.Cust_Acct_Desc as [Account Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.ACCOUNT1"
                qry += " ,GL_ACCOUNT1.Description as [Account1 Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.ACCOUNT2"
                qry += " ,GL_ACCOUNT2.Description as [Account2 Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct as [Advance]"
                qry += " ,GL_Advance_acct.Description as [Advance Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.Bank_Charges_Other_Account as [Bank Other Charges]"
                qry += " ,GL_Bank_Charges_Other_Account.Description as [Bank Other Charges Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.BANK_GUARANTEE as [Bank Guarantee]"
                qry += " ,GL_BANK_GUARANTEE.Description as [Bank Guarantee Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.Consignment_Acct"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.Container_Deposit as [Container Deposit]"
                qry += " ,GL_Container_Deposit.Description as [Container Deposit Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.CREATE_SECURITY_ACCOUNT as [Create Security]"
                qry += " ,GL_CREATE_SECURITY_ACCOUNT.Description as [Create Security Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT as [Exchange Gain]"
                qry += " ,GL_EXCHANGE_GAIN_ACCOUNT.Description as [Exchange Gain Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT as [Exchange Loss]"
                qry += " ,GL_EXCHANGE_LOSS_ACCOUNT.Description as [Exchange Loss Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.Foreign_Bank_Charges_Account as [Foreign Bank Charges]"
                qry += " ,GL_Foreign_Bank_Charges_Account.Description  as [Foreign Bank Charges Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.Gain_Acct as [Gain Acct]"
                qry += " ,GL_Gain_Acct.Description as [Gain Acct Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.GSOC_Acct as [GSOC Acct]"
                qry += " ,GL_GSOC_Acct.Description as [GSOC Acct Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.Leakage_Deduction as [Leakage Deduction]"
                qry += " ,GL_Leakage_Deduction.Description as [Leakage Deduction Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.Penalty_Charges_Account as [Penalty Charges]"
                qry += " ,GL_Penalty_Charges_Account.Description as [Penalty Charges Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.Receipts_Discount_acct as [Receipts Discount Account]"
                qry += " ,GL_Receipts_Discount_acct.Description as [Receipts Discount Account Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct as [Debtor Control]"
                qry += " ,GL_Receivable_Control_acct.Description as [Debtor Control Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.SECURITY_ACCOUNT as [Security Account]"
                qry += " ,GL_SECURITY_ACCOUNT.Description as [Security Account Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.SubSidy_Account as [SubSidy Account]"
                qry += " ,GL_SubSidy_Account.Description as [SubSidy Account Desc]"
                qry += " ,TSPL_CUSTOMER_ACCOUNT_SET.Write_Offs as [Write offs]"
                qry += " ,GL_Write_Offs.Description as [Write Offs Desc]"
                qry += "          from TSPL_CUSTOMER_Master"
                qry += " left outer join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_Master.Cust_Account"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_ACCOUNT1 on GL_ACCOUNT1.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.ACCOUNT1"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_ACCOUNT2 on GL_ACCOUNT2.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.ACCOUNT2"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Advance_acct on GL_Advance_acct.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Bank_Charges_Other_Account on GL_Bank_Charges_Other_Account.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.Bank_Charges_Other_Account"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_BANK_GUARANTEE on GL_BANK_GUARANTEE.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.BANK_GUARANTEE"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Consignment_Acct on GL_Consignment_Acct.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.Consignment_Acct"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Container_Deposit on GL_Container_Deposit.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.Container_Deposit"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_CREATE_SECURITY_ACCOUNT on GL_CREATE_SECURITY_ACCOUNT.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.CREATE_SECURITY_ACCOUNT"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_EXCHANGE_GAIN_ACCOUNT on GL_EXCHANGE_GAIN_ACCOUNT.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_EXCHANGE_LOSS_ACCOUNT on GL_EXCHANGE_LOSS_ACCOUNT.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Foreign_Bank_Charges_Account on GL_Foreign_Bank_Charges_Account.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.Foreign_Bank_Charges_Account"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Gain_Acct on GL_Gain_Acct.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.Gain_Acct"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_GSOC_Acct on GL_GSOC_Acct.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.GSOC_Acct"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Leakage_Deduction on GL_Leakage_Deduction.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.Leakage_Deduction"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Loss_Acct on GL_Loss_Acct.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.Loss_Acct	"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Penalty_Charges_Account on GL_Penalty_Charges_Account.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.Penalty_Charges_Account"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Receipts_Discount_acct on GL_Receipts_Discount_acct.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.Receipts_Discount_acct"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Receivable_Control_acct on GL_Receivable_Control_acct.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_SECURITY_ACCOUNT on GL_SECURITY_ACCOUNT.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.SECURITY_ACCOUNT"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_SubSidy_Account on GL_SubSidy_Account.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.SubSidy_Account	"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Write_Offs on GL_Write_Offs.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.Write_Offs"
                qry += " where 2=2 "

                If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CUSTOMER_Master.Cust_Code in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ")"
                End If
                If txtPurchaseSet.arrValueMember IsNot Nothing AndAlso txtPurchaseSet.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account in (" + clsCommon.GetMulcallString(txtPurchaseSet.arrValueMember) + ")"
                End If
                If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CUSTOMER_Master.cust_group_code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")"
                End If


                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()

                If dt IsNot Nothing AndAlso dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                ElseIf IsPrint = Exporter.Print Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptCustomerAccount", "Customer Account Set Report", clsCommon.GETSERVERDATE())
                    frmCRV = Nothing
                Else
                    ' gv.DataSource = dt
                    FormatGridUOM()
                    For Each row As DataRow In dt.Rows
                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colCustCode).Value = clsCommon.myCstr(row("Customer Code").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colCustName).Value = clsCommon.myCstr(row("Customer Name").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colCustGroup).Value = clsCommon.myCstr(row("Customer Group").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colAccountCode).Value = clsCommon.myCstr(row("Account Code").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colAccountDesc).Value = clsCommon.myCstr(row("Account desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colAccount1).Value = clsCommon.myCstr(row("Account1").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colAccount1Desc).Value = clsCommon.myCstr(row("Account1 Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colAccount2).Value = clsCommon.myCstr(row("Account2").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colAccount2Desc).Value = clsCommon.myCstr(row("Account2 Desc").ToString().Trim())

                        gv.Rows(gv.Rows.Count - 1).Cells(colAdvance).Value = clsCommon.myCstr(row("Advance").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colAdvanceDesc).Value = clsCommon.myCstr(row("Advance Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colBankOtherCharges).Value = clsCommon.myCstr(row("Bank other charges").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colBankOtherChargesDesc).Value = clsCommon.myCstr(row("Bank other charges Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colBankGuarantee).Value = clsCommon.myCstr(row("Bank Guarantee").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colBankGuaranteeDesc).Value = clsCommon.myCstr(row("Bank Guarantee Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colConsignmentAcct).Value = clsCommon.myCstr(row("Consignment_Acct").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colContainerDeposit).Value = clsCommon.myCstr(row("Container Deposit").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colContainerDepositDesc).Value = clsCommon.myCstr(row("Container Deposit Desc").ToString().Trim())

                        gv.Rows(gv.Rows.Count - 1).Cells(colCrateSecurity).Value = clsCommon.myCstr(row("Create Security").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colCrateSecurityDesc).Value = clsCommon.myCstr(row("Create Security Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colExchangeGain).Value = clsCommon.myCstr(row("Exchange Gain").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colExchangeGainDesc).Value = clsCommon.myCstr(row("Exchange Gain Desc").ToString().Trim())

                        gv.Rows(gv.Rows.Count - 1).Cells(colExchangeLoss).Value = clsCommon.myCstr(row("Exchange Loss").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colExchangeLossDesc).Value = clsCommon.myCstr(row("Exchange Loss Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colForeignBankCharges).Value = clsCommon.myCstr(row("Foreign Bank Charges").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colForeignBankChargesDesc).Value = clsCommon.myCstr(row("Foreign Bank Charges Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colGainAcct).Value = clsCommon.myCstr(row("Gain Acct").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colGainAcctDesc).Value = clsCommon.myCstr(row("Gain Acct Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colGOSC).Value = clsCommon.myCstr(row("GSOC Acct").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colGOSCDesc).Value = clsCommon.myCstr(row("GSOC Acct Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colLeakageDeduction).Value = clsCommon.myCstr(row("Leakage Deduction").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colLeakageDeductionDesc).Value = clsCommon.myCstr(row("Leakage Deduction Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colPenaltyCharges).Value = clsCommon.myCstr(row("Penalty Charges").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colPenaltyChargesDesc).Value = clsCommon.myCstr(row("Penalty Charges Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colReceipt).Value = clsCommon.myCstr(row("Receipts Discount Account").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colReceiptDesc).Value = clsCommon.myCstr(row("Receipts Discount Account Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colDebtorControl).Value = clsCommon.myCstr(row("Debtor Control").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colDebtorControlDesc).Value = clsCommon.myCstr(row("Debtor Control Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colSecurity).Value = clsCommon.myCstr(row("Security Account").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colSecurityDesc).Value = clsCommon.myCstr(row("Security Account Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colSubsidy).Value = clsCommon.myCstr(row("Subsidy Account").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colSubsidyDesc).Value = clsCommon.myCstr(row("Subsidy Account Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colWriteoffs).Value = clsCommon.myCstr(row("Write offs").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colWriteoffsDesc).Value = clsCommon.myCstr(row("Write offs Desc").ToString().Trim())

                    Next
                    'For ii As Integer = 0 To gv.Columns.Count - 1
                    '    gv.Columns(ii).ReadOnly = True
                    'Next

                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv.BestFitColumns()
                    isInsideLoadData = True

                End If
            End If
            'ChangeColumnValue()
            ReStoreGridLayout()
            If chkOnlyview.Checked = True Then
                btnUpdate.Enabled = False
            Else
                btnUpdate.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub FormatGridUOM()

        Dim repoString As New GridViewTextBoxColumn()

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Customer Code"
        repoString.Name = colCustCode
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Customer Name"
        repoString.Name = colCustName
        repoString.Width = 250
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Customer Group"
        repoString.Name = colCustGroup
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Account Code"
        repoString.Name = colAccountCode
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)


        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Account Desc"
        repoString.Name = colAccountDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)


        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Account1"
        repoString.Name = colAccount1
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)



        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Account1 Desc"
        repoString.Name = colAccount1Desc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Account2"
        repoString.Name = colAccount2
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)



        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Account2 Desc"
        repoString.Name = colAccount2Desc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Advance"
        repoString.Name = colAdvance
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Advance Desc"
        repoString.Name = colAdvanceDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Bank Other Charges"
        repoString.Name = colBankOtherCharges
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Bank Other Charges Desc"
        repoString.Name = colBankOtherChargesDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Bank Guarantee"
        repoString.Name = colBankGuarantee
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Bank Guarantee Desc"
        repoString.Name = colBankGuaranteeDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Consignment Acct"
        repoString.Name = colConsignmentAcct
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Container Deposit"
        repoString.Name = colContainerDeposit
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Container Deposit Desc"
        repoString.Name = colContainerDepositDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

       
        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Crate Security"
        repoString.Name = colCrateSecurity
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Crate Security Desc"
        repoString.Name = colCrateSecurityDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Exchange Gain"
        repoString.Name = colExchangeGain
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Exchange Gain Desc"
        repoString.Name = colExchangeGainDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Exchange Loss"
        repoString.Name = colExchangeLoss
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Exchange Loss Desc"
        repoString.Name = colExchangeLossDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Foreign Bank Charges"
        repoString.Name = colForeignBankCharges
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Foreign Bank Charges Desc"
        repoString.Name = colForeignBankChargesDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Gain Acct"
        repoString.Name = colGainAcct
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Gain Acct Desc"
        repoString.Name = colGainAcctDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "GSOC Acct"
        repoString.Name = colGOSC
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "GSOC Acct Desc"
        repoString.Name = colGOSCDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Leakage Deduction"
        repoString.Name = colLeakageDeduction
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Leakage Deduction Desc"
        repoString.Name = colLeakageDeductionDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Penalty Charges"
        repoString.Name = colPenaltyCharges
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Penalty Charges Desc"
        repoString.Name = colPenaltyChargesDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Receipts Discount Account"
        repoString.Name = colReceipt
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Receipts Discount Account Desc"
        repoString.Name = colReceiptDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Debtor Control"
        repoString.Name = colDebtorControl
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Debtor Control Desc"
        repoString.Name = colDebtorControlDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Security Account"
        repoString.Name = colSecurity
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Security Account Desc"
        repoString.Name = colSecurityDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "SubSidy Account"
        repoString.Name = colSubsidy
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "SubSidy Account Desc"
        repoString.Name = colSubsidyDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Write offs"
        repoString.Name = colWriteoffs
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Write offs Desc"
        repoString.Name = colWriteoffsDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)


    End Sub
    Sub ChangeColumnValue()
        Dim qry As String = " select LABEL_ID,NEW_LABEL_NAME,CURRENT_LABEL_NAME from TSPL_CLIENT_FORM_LABEL_SETTING left outer join(select 'rdlblrecievablescontrol' as LabelName union all select 'rdlblrecieptdiscount' as LabelName"
        qry += " union all select 'rdlblAdvance' as LabelName"
        qry += " union all select 'rdlblWriteoffs' as LabelName"
        qry += " union all select 'lblcontainer' as LabelName"
        qry += " union all select 'lblBaseCurrency' as LabelName"
        qry += " union all select 'lblExchangeLoss' as LabelName"
        qry += " union all select 'lblExchangeGain' as LabelName"
        qry += " union all select 'MyLabel1' as LabelName"
        qry += " union all select 'MyLabel2' as LabelName"
        qry += " union all select 'MyLabel3' as LabelName"
        qry += " union all select 'MyLabel4' as LabelName"
        qry += " union all select 'MyLabel5' as LabelName"
        qry += " union all select 'MyLabel11' as LabelName"
        qry += " union all select 'MyLabel12' as LabelName"
        qry += " union all select 'lblSubsidyAccount' as LabelName"
        qry += " union all select 'MyLabel13' as LabelName"
        qry += " union all select 'MyLabel9' as LabelName)Label_Name"
        qry += " on TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID=Label_Name.LabelName"
        qry += " where form_name='CUST-ACC-SET'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                If clsCommon.myCstr(dr("LABEL_ID")) = "rdlblrecievablescontrol" Then
                    gv.Columns("Debtor Control").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Debtor Control Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "rdlblrecieptdiscount" Then
                    gv.Columns("Receipts Disount Account").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Receipts Disount Account Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "rdlblAdvance" Then
                    gv.Columns("Advance").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Advance Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "rdlblWriteoffs" Then
                    gv.Columns("Write offs").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Write offs Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "lblcontainer" Then
                    gv.Columns("Container Deposit").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Container Deposit Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "lblBaseCurrency" Then
                    gv.Columns("Currency").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Currency Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "lblExchangeLoss" Then
                    gv.Columns("Exchange Loss").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Exchange Loss Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "lblExchangeGain" Then
                    gv.Columns("Exchange Gain").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Exchange Gain Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "MyLabel1" Then
                    gv.Columns("Security").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Security Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "MyLabel2" Then
                    gv.Columns("Create Security").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Create Security Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "MyLabel3" Then
                    gv.Columns("Bank Guarantee").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Bank Guarantee Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "MyLabel4" Then
                    gv.Columns("Account1").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Account1 Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "MyLabel5" Then
                    gv.Columns("Account2").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Account2 Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "MyLabel11" Then
                    gv.Columns("Foreign Bank Charges").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Foreign Bank Charges Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "MyLabel12" Then
                    gv.Columns("Penalty Charges").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Penalty Charges Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "lblSubsidyAccount" Then
                    gv.Columns("Subsidy Account").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Subsidy Account Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "MyLabel13" Then
                    gv.Columns("Leakage Deduction").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Leakage Deduction Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                ElseIf clsCommon.myCstr(dr("LABEL_ID")) = "MyLabel9" Then
                    gv.Columns("Loss").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                    gv.Columns("Loss Desc").HeaderText = clsCommon.myCstr(dr("NEW_LABEL_NAME")) + " Desc"
                End If
            Next
        End If
    End Sub
    Private Sub TxtMultiSelectFinder1__My_Click(sender As Object, e As EventArgs) Handles txtPurchaseSet._My_Click
        Dim qry As String
        qry = " select Cust_Account as Code,Cust_Acct_desc as [Description] from TSPL_CUSTOMER_ACCOUNT_SET "

        txtPurchaseSet.arrValueMember = clsCommon.ShowMultipleSelectForm("PurMulSel", qry, "Code", "Description", txtPurchaseSet.arrValueMember, txtPurchaseSet.arrDispalyMember)

    End Sub
    Private Sub txtItemStructure__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        Dim qry As String
        qry = " select Cust_Code as Code,Customer_Name as [Description] from TSPL_Customer_MASTER "

        txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("PurMulSel", qry, "Code", "Description", txtItemType.arrValueMember, txtItemType.arrDispalyMember)

    End Sub
    Private Sub txtCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustomerGroup._My_Click
        Dim qry As String
        qry = " select Cust_Group_Code as Code,Cust_Group_Desc as [Description] from tspl_customer_group_master "

        txtCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CusMulSel", qry, "Code", "Description", txtCustomerGroup.arrValueMember, txtCustomerGroup.arrDispalyMember)

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            Dim TempFormId As String = ""

            If clsCommon.myLen(TempFormId) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(TempFormId, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub FormatGrid()
        ' Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
            gv.Columns(ii).Width = 100
        Next
    
        gv.Columns(1).Width = 250
        'End If

    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        FunReset()
    End Sub

    Private Sub btnexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexcel.Click
        Print(Exporter.Export)

        'If gv.Rows.Count > 0 Then
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    arrHeader.Add("Customer Account Set Report")
        '    clsCommon.MyExportToExcelGrid("Item List", gv, arrHeader, "Customer Account Set Report")
        'End If
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub btnpdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        Print(Exporter.PDF)
        'If gv.Rows.Count > 0 Then
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    arrHeader.Add("Customer Account Set Report")
        '    clsCommon.MyExportToPDF("Customer List", gv, arrHeader, "Customer Account Set Report")
        'End If
        Export(EnumExportTo.PDF)
    End Sub

    Sub Export(ByVal exporter As EnumExportTo)
        If gv.Rows.Count > 0 Then
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Customer Account Set Report")
            If txtPurchaseSet.arrValueMember IsNot Nothing AndAlso txtPurchaseSet.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Account Set : " + clsCommon.GetMulcallStringWithComma(txtPurchaseSet.arrDispalyMember))
            End If
            If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtItemType.arrDispalyMember))
            End If
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Customer Account Set Report", gv, arrHeader, "Customer Account Set Report")
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Customer Account Set Report", gv, arrHeader, "Customer Account Set Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID

        If clsCommon.myLen(ReportID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub gv_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If (isInsideLoadData) Then

                If e.Column Is gv.Columns(colAccount1) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colAccount1).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colAccount1).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colAccount1Desc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colAccount1).Value + "' ")
                ElseIf e.Column Is gv.Columns(colAccount2) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colAccount2).Value = clsCommon.ShowSelectForm("fndPayable", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colAccount2).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colAccount2Desc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colAccount2).Value + "' ")
                ElseIf e.Column Is gv.Columns(colAdvance) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colAdvance).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colAdvance).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colAdvanceDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colAdvance).Value + "' ")
                ElseIf e.Column Is gv.Columns(colBankGuarantee) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colBankGuarantee).Value = clsCommon.ShowSelectForm("fndWIP", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colBankGuarantee).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colBankGuaranteeDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colBankGuarantee).Value + "' ")
                ElseIf e.Column Is gv.Columns(colBankOtherCharges) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colBankOtherCharges).Value = clsCommon.ShowSelectForm("fndRM", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colBankOtherCharges).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colBankOtherChargesDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colBankOtherCharges).Value + "' ")
        
                ElseIf e.Column Is gv.Columns(colContainerDeposit) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colContainerDeposit).Value = clsCommon.ShowSelectForm("fndISaleAccControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colContainerDeposit).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colContainerDeposit).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colContainerDeposit).Value + "' ")
                ElseIf e.Column Is gv.Columns(colCrateSecurity) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colCrateSecurity).Value = clsCommon.ShowSelectForm("fndSaleRControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colCrateSecurity).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colCrateSecurityDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colCrateSecurity).Value + "' ")
                ElseIf e.Column Is gv.Columns(colDebtorControl) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colDebtorControl).Value = clsCommon.ShowSelectForm("fndCOGSControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colDebtorControl).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colDebtorControlDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colDebtorControl).Value + "' ")
                
                ElseIf e.Column Is gv.Columns(colExchangeGain) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colExchangeGain).Value = clsCommon.ShowSelectForm("fndshipControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colExchangeGain).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colExchangeGainDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colExchangeGain).Value + "' ")
                ElseIf e.Column Is gv.Columns(colExchangeLoss) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colExchangeLoss).Value = clsCommon.ShowSelectForm("fndshipControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colExchangeLoss).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colExchangeLossDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colExchangeLoss).Value + "' ")
                ElseIf e.Column Is gv.Columns(colExchangeLoss) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colExchangeLoss).Value = clsCommon.ShowSelectForm("fndshipControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colExchangeLoss).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colExchangeLossDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colExchangeLoss).Value + "' ")
                ElseIf e.Column Is gv.Columns(colForeignBankCharges) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colForeignBankCharges).Value = clsCommon.ShowSelectForm("fndshipControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colForeignBankCharges).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colForeignBankChargesDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colForeignBankCharges).Value + "' ")
                ElseIf e.Column Is gv.Columns(colLeakageDeduction) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colLeakageDeduction).Value = clsCommon.ShowSelectForm("fndshipControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colLeakageDeduction).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colLeakageDeductionDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colLeakageDeduction).Value + "' ")
                ElseIf e.Column Is gv.Columns(colSubsidy) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colSubsidy).Value = clsCommon.ShowSelectForm("fndshipControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colSubsidy).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colSubsidyDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colSubsidy).Value + "' ")
                ElseIf e.Column Is gv.Columns(colWriteoffs) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colWriteoffs).Value = clsCommon.ShowSelectForm("fndshipControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colWriteoffs).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colWriteoffsDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colWriteoffs).Value + "' ")


                End If
            End If
            'OpenICodeList(False)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Dim TempFormId As String = ""
        TempFormId = Form_ID
        clsGridLayout.DeleteData(TempFormId, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Try
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = False
                gv.Columns(ii).IsVisible = True
                gv.Columns(ii).Width = 100
            Next
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()


            If transportSql.importExcel(gv, "Customer Code", "Customer Name", "Customer Group", "Account Code", "Account desc", "Account1", "Account1 Desc", "Account2", "Account2 Desc", "Advance", "Advance Desc", "Bank other charges", "Bank other charges Desc", "Bank Guarantee", "Bank Guarantee Desc", "Consignment Acct", "Container Deposit", "Container Deposit Desc", "Crate Security", "Crate Security Desc", "Exchange Gain", "Exchange Gain Desc", "Exchange Loss", "Exchange Loss Desc", "Foreign Bank Charges", "Foreign Bank Charges Desc", "Gain Acct", "Gain Acct Desc", "GSOC Acct", "GSOC Acct Desc", "Leakage Deduction", "Leakage Deduction Desc", "Penalty Charges", "Penalty Charges Desc", "Receipts Discount Account", "Receipts Discount Account Desc", "Debtor Control", "Debtor Control Desc", "Security Account", "Security Account Desc", "Subsidy Account", "Subsidy Account Desc", "Write offs", "Write offs Desc") Then
                clsCommon.ProgressBarPercentShow()

                Dim dt As New DataTable()
                dt = gv.DataSource()
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                FormatGridUOM()
                For Each row As DataRow In dt.Rows
                    gv.Rows.AddNew()
                    gv.Rows(gv.Rows.Count - 1).Cells(colCustCode).Value = clsCommon.myCstr(row("Customer Code").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colCustName).Value = clsCommon.myCstr(row("Customer Name").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colCustGroup).Value = clsCommon.myCstr(row("Customer Group").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAccountCode).Value = clsCommon.myCstr(row("Account Code").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAccountDesc).Value = clsCommon.myCstr(row("Account desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAccount1).Value = clsCommon.myCstr(row("Account1").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAccount1Desc).Value = clsCommon.myCstr(row("Account1 Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAccount2).Value = clsCommon.myCstr(row("Account2").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAccount2Desc).Value = clsCommon.myCstr(row("Account2 Desc").ToString().Trim())

                    gv.Rows(gv.Rows.Count - 1).Cells(colAdvance).Value = clsCommon.myCstr(row("Advance").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAdvanceDesc).Value = clsCommon.myCstr(row("Advance Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colBankOtherCharges).Value = clsCommon.myCstr(row("Bank other charges").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colBankOtherChargesDesc).Value = clsCommon.myCstr(row("Bank other charges Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colBankGuarantee).Value = clsCommon.myCstr(row("Bank Guarantee").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colBankGuaranteeDesc).Value = clsCommon.myCstr(row("Bank Guarantee Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colConsignmentAcct).Value = clsCommon.myCstr(row("Consignment Acct").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colContainerDeposit).Value = clsCommon.myCstr(row("Container Deposit").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colContainerDepositDesc).Value = clsCommon.myCstr(row("Container Deposit Desc").ToString().Trim())

                    gv.Rows(gv.Rows.Count - 1).Cells(colCrateSecurity).Value = clsCommon.myCstr(row("Crate Security").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colCrateSecurityDesc).Value = clsCommon.myCstr(row("Crate Security Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colExchangeGain).Value = clsCommon.myCstr(row("Exchange Gain").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colExchangeGainDesc).Value = clsCommon.myCstr(row("Exchange Gain Desc").ToString().Trim())

                    gv.Rows(gv.Rows.Count - 1).Cells(colExchangeLoss).Value = clsCommon.myCstr(row("Exchange Loss").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colExchangeLossDesc).Value = clsCommon.myCstr(row("Exchange Loss Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colForeignBankCharges).Value = clsCommon.myCstr(row("Foreign Bank Charges").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colForeignBankChargesDesc).Value = clsCommon.myCstr(row("Foreign Bank Charges Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colGainAcct).Value = clsCommon.myCstr(row("Gain Acct").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colGainAcctDesc).Value = clsCommon.myCstr(row("Gain Acct Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colGOSC).Value = clsCommon.myCstr(row("GSOC Acct").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colGOSCDesc).Value = clsCommon.myCstr(row("GSOC Acct Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colLeakageDeduction).Value = clsCommon.myCstr(row("Leakage Deduction").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colLeakageDeductionDesc).Value = clsCommon.myCstr(row("Leakage Deduction Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPenaltyCharges).Value = clsCommon.myCstr(row("Penalty Charges").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPenaltyChargesDesc).Value = clsCommon.myCstr(row("Penalty Charges Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colReceipt).Value = clsCommon.myCstr(row("Receipts Discount Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colReceiptDesc).Value = clsCommon.myCstr(row("Receipts Discount Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colDebtorControl).Value = clsCommon.myCstr(row("Debtor Control").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colDebtorControlDesc).Value = clsCommon.myCstr(row("Debtor Control Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colSecurity).Value = clsCommon.myCstr(row("Security Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colSecurityDesc).Value = clsCommon.myCstr(row("Security Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colSubsidy).Value = clsCommon.myCstr(row("Subsidy Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colSubsidyDesc).Value = clsCommon.myCstr(row("Subsidy Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colWriteoffs).Value = clsCommon.myCstr(row("Write offs").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colWriteoffsDesc).Value = clsCommon.myCstr(row("Write offs Desc").ToString().Trim())

                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow("Data Transfered Successfully.")

            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub
    Function AllowToSave() As Boolean
        Try
            Dim LineNo As String

            For Each grow As GridViewRowInfo In gv.Rows
                LineNo = clsCommon.myCstr(grow.Index + 1)
                Dim Account1 As String = clsCommon.myCstr(grow.Cells(colAccount1).Value)
                Dim Account2 As String = clsCommon.myCstr(grow.Cells(colAccount2).Value)
                Dim AccountCode As String = clsCommon.myCstr(grow.Cells(colAccountCode).Value)
                Dim Advance As String = clsCommon.myCstr(grow.Cells(colAdvance).Value)
                Dim BankGuarantee As String = clsCommon.myCstr(grow.Cells(colBankGuarantee).Value)
                Dim BankOtherCharges As String = clsCommon.myCstr(grow.Cells(colBankOtherCharges).Value)
                Dim ConsignmentAcct As String = clsCommon.myCstr(grow.Cells(colConsignmentAcct).Value)
                Dim ContainerDeposit As String = clsCommon.myCstr(grow.Cells(colContainerDeposit).Value)
                Dim CrateSecurity As String = clsCommon.myCstr(grow.Cells(colCrateSecurity).Value)
                Dim CustCode As String = clsCommon.myCstr(grow.Cells(colCustCode).Value)
                Dim DebtorControl As String = clsCommon.myCstr(grow.Cells(colDebtorControl).Value)
                Dim ExchangeGain As String = clsCommon.myCstr(grow.Cells(colExchangeGain).Value)
                Dim ExchangeLoss As String = clsCommon.myCstr(grow.Cells(colExchangeLoss).Value)
                Dim ForeignBankCharges As String = clsCommon.myCstr(grow.Cells(colForeignBankCharges).Value)
                Dim GainAcct As String = clsCommon.myCstr(grow.Cells(colGainAcct).Value)
                Dim GOSC As String = clsCommon.myCstr(grow.Cells(colGOSC).Value)
                Dim LeakageDeduction As String = clsCommon.myCstr(grow.Cells(colLeakageDeduction).Value)
                Dim PenaltyCharges As String = clsCommon.myCstr(grow.Cells(colPenaltyCharges).Value)
                Dim Receipt As String = clsCommon.myCstr(grow.Cells(colReceipt).Value)
                Dim Security As String = clsCommon.myCstr(grow.Cells(colSecurity).Value)
                Dim Subsidy As String = clsCommon.myCstr(grow.Cells(colSubsidy).Value)
                Dim Writeoffs As String = clsCommon.myCstr(grow.Cells(colWriteoffs).Value)

                
                If clsCommon.myLen(Account1) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Account1 + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & Account1 & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(Account1) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Account2 + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & Account2 & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(Advance) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Advance + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & Advance & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(BankGuarantee) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + BankGuarantee + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & BankGuarantee & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(BankOtherCharges) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + BankOtherCharges + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & BankOtherCharges & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(ConsignmentAcct) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + ConsignmentAcct + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & ConsignmentAcct & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(ContainerDeposit) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + ContainerDeposit + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & ContainerDeposit & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(CrateSecurity) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + CrateSecurity + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & CrateSecurity & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(DebtorControl) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + DebtorControl + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & DebtorControl & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(ExchangeGain) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + ExchangeGain + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & ExchangeGain & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(ForeignBankCharges) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + ForeignBankCharges + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & ForeignBankCharges & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(GainAcct) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + GainAcct + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & GainAcct & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(GOSC) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + GOSC + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & GOSC & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(LeakageDeduction) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + LeakageDeduction + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & LeakageDeduction & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(PenaltyCharges) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + PenaltyCharges + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & PenaltyCharges & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(Receipt) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Receipt + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & Receipt & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(Security) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Security + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & Security & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(Subsidy) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Subsidy + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & Subsidy & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(Writeoffs) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Writeoffs + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & Writeoffs & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If

            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Private Sub Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            Dim obj As New clsGLSourceCode()
            obj.CustomerArrTr = New List(Of clsCustomerAccountSet)

            If (AllowToSave()) Then
                For Each grow As GridViewRowInfo In gv.Rows
                    If (clsCommon.myLen(grow.Cells(colCustCode).Value) > 0) Then

                        Dim objTr As New clsCustomerAccountSet()

                        objTr.Account1 = clsCommon.myCstr(grow.Cells(colAccount1).Value)
                        objTr.Account2 = clsCommon.myCstr(grow.Cells(colAccount2).Value)
                        objTr.AccountCode = clsCommon.myCstr(grow.Cells(colAccountCode).Value)
                        objTr.Advance = clsCommon.myCstr(grow.Cells(colAdvance).Value)
                        objTr.BankGuarantee = clsCommon.myCstr(grow.Cells(colBankGuarantee).Value)
                        objTr.BankOtherCharges = clsCommon.myCstr(grow.Cells(colBankOtherCharges).Value)
                        objTr.ConsignmentAcct = clsCommon.myCstr(grow.Cells(colConsignmentAcct).Value)
                        objTr.ContainerDeposit = clsCommon.myCstr(grow.Cells(colContainerDeposit).Value)
                        objTr.CrateSecurity = clsCommon.myCstr(grow.Cells(colCrateSecurity).Value)
                        objTr.CustCode = clsCommon.myCstr(grow.Cells(colCustCode).Value)
                        objTr.DebtorControl = clsCommon.myCstr(grow.Cells(colDebtorControl).Value)
                        objTr.ExchangeGain = clsCommon.myCstr(grow.Cells(colExchangeGain).Value)
                        objTr.ExchangeLoss = clsCommon.myCstr(grow.Cells(colExchangeLoss).Value)
                        objTr.ForeignBankCharges = clsCommon.myCstr(grow.Cells(colForeignBankCharges).Value)
                        objTr.GainAcct = clsCommon.myCstr(grow.Cells(colGainAcct).Value)
                        objTr.GOSC = clsCommon.myCstr(grow.Cells(colGOSC).Value)
                        objTr.LeakageDeduction = clsCommon.myCstr(grow.Cells(colLeakageDeduction).Value)
                        objTr.PenaltyCharges = clsCommon.myCstr(grow.Cells(colPenaltyCharges).Value)
                        objTr.Receipt = clsCommon.myCstr(grow.Cells(colReceipt).Value)
                        objTr.Security = clsCommon.myCstr(grow.Cells(colSecurity).Value)
                        objTr.Subsidy = clsCommon.myCstr(grow.Cells(colSubsidy).Value)
                        objTr.Writeoffs = clsCommon.myCstr(grow.Cells(colWriteoffs).Value)
                        

                        If (clsCommon.myLen(objTr.CustCode) > 0) Then
                            obj.CustomerArrTr.Add(objTr)
                        End If

                    End If
                Next
                If (obj.CustomerArrTr Is Nothing OrElse obj.CustomerArrTr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                    Return
                End If
                If (obj.CustomerAccountUpdate(obj)) Then
                    common.clsCommon.MyMessageBoxShow("Data Update Successfully")
                    FunReset()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    ' Ticket No : TEC/02/05/19-000470 by prabhakar
    Private Sub chkOnlyview_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnlyview.CheckedChanged
        If chkOnlyview.Checked = True Then
            gv.EnableFiltering = True
        Else
            gv.EnableFiltering = False
        End If
    End Sub
End Class
