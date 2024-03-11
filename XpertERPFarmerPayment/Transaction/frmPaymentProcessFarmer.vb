'===================BM00000007864,BM00000007337,BM00000007744===================
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls
''Checkin Richa 22/06/2020
Public Class frmPaymentProcessFarmer
    Inherits FrmMainTranScreen

#Region "Variables"
    Public isEmpOnAmtOnly As Boolean = False
    Public Const colSlno As String = "colSlno"
    Public Const colPurchaseInvoiceNo As String = "colPurchaseInvoiceNo"
    Public Const colAPInvoiceNo As String = "colAPInvoiceNo"
    Public Const colARInvoiceNo As String = "colARInvoiceNo"
    Public Const colPurchaseInvoiceDate As String = "colPurchaseInvoiceDate"
    Public Const colAPInvoiceDate As String = "colAPInvoiceDate"
    Public Const colARInvoiceDate As String = "colARInvoiceDate"
    Public Const colInvAmt As String = "colInvAmt"
    Public Const colItemCode As String = "colItemCode"
    Public Const colItemDesc As String = "colItemDesc"
    Public Const colLocCode As String = "colLocCode"
    Public Const colLocDesc As String = "colLocDesc"
    Public Const colTotalEmp As String = "colTotalEmp"
    Public Const colDeductionCode As String = "colDeductionCode"
    Public Const colDeductionDesc As String = "colDeductionDesc"
    Public Const colIsFromPrevPPCycle As String = "colIsFromPrevPPCycle"
    Public Const colVLCCode As String = "colVLCCode"
    Public Const colVLCName As String = "colVLCName"
    Public Const colVLCUploaderCode As String = "colVLCUploaderCode"
    Public Const colVendorCode As String = "colVendorCode"
    Public Const colVendorDesc As String = "colVendorDesc"
    Public Const colCustomerCode As String = "colCustomerCode"
    Public Const colCustomerName As String = "colCustomerName"
    Public Const colPayeeJointName As String = "colPayeeJointName"
    Public Const colPayeeJointAcNo As String = "colPayeeJointAcNo"
    Public Const colPayeeJointIFSC As String = "colPayeeJointIFSC"
    Public Const colPayeeJointBankCode As String = "colPayeeJointBankCode"
    Public Const colPayeeJointBankDesc As String = "colPayeeJointBankDesc"
    Public Const colPayeeJointBranchCode As String = "colPayeeJointBranchCode"
    Public Const colPayeeJointBranchDesc As String = "colPayeeJointBranchDesc"
    Public Const colMilkQty As String = "colMilkQty"
    Public Const colPrevCycleBalance As String = "colPrevCycleBalance"
    Public Const colItemAmt As String = "colItemAmt"
    Public Const colEmpAmt As String = "colEmpAmt"
    Public Const colInvAndEmpAmt As String = "colInvAndEmpAmt"
    Public Const colIncenAmt As String = "colIncenAmt"
    Public Const colIncenEmpAmt As String = "colIncenEmpAmt"
    Public Const colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt As String = "colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt"
    Public Const colVSPOwnSystemAmt As String = "colVSPOwnSystemAmt"
    Public Const colHeadLoadAmt As String = "colHeadLoadAmt"
    Public Const colInvDeduc As String = "colInvDeduc"
    Public Const colReduceDeduc As String = "colReduceDeduc"

    Public Const colIncentiveAmt As String = "colIncentiveAmt"
    Public Const colDeductionAmt As String = "colDeductionAmt"



    Public Const colPaybleAmt As String = "colPaybleAmt"
    Public Const colIsPaymentProcessHold As String = "colIsPaymentProcessHold"
    Public Const colShipmentNo As String = "colShipmentNo"
    Public Const colShipmentDate As String = "colShipmentDate"
    Public Const colSaleInvNo As String = "colSaleInvNo"
    Public Const colSaleInvDate As String = "colSaleInvDate"
    Public Const colVspItemIssueNo As String = "colVspItemIssueNo"
    Public Const colVspItemIssueReturnNo As String = "colVspItemIssueReturnNo"
    Public Const colVspItemIssueDate As String = "colVspItemIssueDate"
    Public Const colSelect As String = "colSelect"
    Public Const colReturnDocNo As String = "colReturnDocNo"
    Public Const colReturnDocDate As String = "colReturnDocDate"
    Public Const colReturnDocType As String = "colReturnDocType"


    Public Const colFarmerMilkQty As String = "colFarmerMilkQty"
    Public Const colFarmerTotalSale As String = "colFarmerTotalSale"
    Public Const colFarmerTotalSaleReturn As String = "colFarmerTotalSaleReturn"
    Public Const colFarmerAdjustmentAmountTotal As String = "colFarmerAdjustmentAmountTotal"
    Public Const colMPAmountPayable As String = "colMPAmountPayable"

    Public Const colVSPAmount As String = "colVSPAmount"
    Public Const colMPAmount As String = "colMPAmount"

    Public Const colMPNetAmount As String = "colMPNetAmount"
    Public Const colMPEMPAmount As String = "colMPEMPAmount"
    Public Const colMPIncentiveAmount As String = "colMPIncentiveAmount"
    Public Const colMPEMPIncentiveAmount As String = "colMPEMPIncentiveAmount"

    Public Const colMPVSPDiffAmount As String = "colMPVSPDiffAmount"
    Public Const colServiceChargeAmt As String = "colServiceChargeAmt"

    Public Const colAPSelect As String = "colAPSelect"
    Public Const colAPSNo As String = "colAPSNo"
    Public Const colAPVendorCode As String = "colAPVendorCode"
    Public Const colAPVendorName As String = "colAPVendorName"
    Public Const colAPPaymentCode As String = "colAPPaymentCode"
    Public Const colAPPaymentDate As String = "colAPPaymentDate"
    Public Const colAPPaymentAmt As String = "colAPPaymentAmt"
    Public Const colAPPaymentAmtBalance As String = "colAPPaymentAmtBalance"
    Public Const colFarmerCode As String = "colFarmerCode"
    Public Const colFarmerName As String = "colFarmerName"
    Public Const colMPUploaderCode As String = "colMPUploaderCode"
    Public Const colMPAccountType As String = "colMPAccountType"

    '' mp adjsutment columns
    Public Const colAdjNo As String = "colAdjNo"
    Public Const colMPAdjDate As String = "colMPAdjDate"
    Public Const colMPAdjType As String = "colMPAdjType"
    Public Const colMPADjDesc As String = "colMPADjDesc"
    Public Const colMPAdjRemarks As String = "colMPAdjRemarks"
    Public Const colMPAdjustAmount As String = "colMPAdjustAmount"

    Public Const colFAFarmerCode As String = "colFAFarmerCode"
    Public Const colFAFarmerName As String = "colFAFarmerName"
    Public Const colFAPaymentCode As String = "colFAPaymentCode"
    Public Const colFAPaymentDate As String = "colFAPaymentDate"
    Public Const colFALoan As String = "colFALoan"
    Public Const colFAPaymentAmt As String = "colFAPaymentAmt"
    Public Const colFAKnockOff As String = "colFAKnockOff"

    Public Const colFATotalAdvance As String = "colFATotalAdvance"
    Public Const colFATotalAdvanceRecovery As String = "colFATotalAdvanceRecovery"

    Public Const colFALoanPayment As String = "colFALoanPayment"
    Public Const colFALoanPaymentRecovery As String = "colFALoanPaymentRecovery"



    Dim colChkBox As GridViewCheckBoxColumn = Nothing
    Dim colTextBox As GridViewTextBoxColumn = Nothing
    Dim colDate As GridViewDateTimeColumn = Nothing
    Dim colDecimal As GridViewDecimalColumn = Nothing
    Dim arrStrMccSaleItemCode As List(Of String) = Nothing
    Dim arrStrMccSaleItemDesc As List(Of String) = Nothing
    Dim arrStrIssueItemCode As List(Of String) = Nothing
    Dim arrStrIssueItemDesc As List(Of String) = Nothing
    Dim arrStrDedCode As List(Of String) = Nothing
    Dim arrStrDedDesc As List(Of String) = Nothing
    Dim arrAgainstMilkPurchaseInvoiceNo As String = ""
    Dim arrVendorInvoiceNo As String = ""
    Dim strVendorCode As String = ""
    Dim strCustomerCode As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isLoad As Boolean = True
    Dim isNewEntry As Boolean = False
    Dim frm As FrmPaymentDetail = New FrmPaymentDetail()
    Public Const colMccSaleTotalAmount As String = "colMccSaleTotalAmount"
    Public Const colItemIssueTotalAmount As String = "colItemIssueTotalAmount"
    Public Const colItemIssueReturnTotalAmount As String = "colItemIssueReturnTotalAmount"
    Public Const colDeductionTotalAmount As String = "colDeductionTotalAmount"
    Public Const colTotalCreditNoteAmount As String = "colTotalCreditNoteAmount"
    Public Const colAdvanceAmount As String = "colAdvanceTotal"
    Public Const colAdvanceKnockOffAmount As String = "colAdvanceKnockOffAmount"

    Public Const colBankCode As String = "colBankCode"
    Public colBankDesc As String = "colBankDesc"
    Public Const colPayMode As String = "colPayMode"
    Public colChequeNo As String = "colChequeNo"
    Public colChequeDate As String = "colChequeDate"
    Public isCellValueChanged = False
    Public PayProcessDocNo As String = ""
    Public colActualVSPCode As String = "colActualVSPCode"
    Public colActualVSPName As String = "colActualVSPName"
    Public Const colPayToFarmer As String = "colPayToFarmer"
    Public Const colFarmerPayment As String = "colFarmerPayment"
    Public Const colAPAdjustmentNo As String = "colAPAdjustmentNo"
    Public Const colAPAdjustmentDate As String = "colAPAdjustmentDate"
    '============Added By Rohit,========================
    Public Const colMccSaleReturnTotalAmount As String = "colMccSaleReturnTotalAmount"


    Public Const colNextCycleDebitNoteFarmer As String = "colNextCycleDebitNoteFarmer"
    'Public Const colNextCycleDebitNoteFarmer As String = "colNextCycleDebitNoteFarmer"

    Public Const colgvVSPExcessAmount As String = "colgvVSPExcessAmount"
    Public Const colgvTotMPDeductionAmount As String = "colgvTotMPDeductionAmount"
    Public Const colNextCycleDebitNote As String = "colNextCycleDebitNote"

    Private isConsiderAdvancePayment As Boolean = False
    Private PayableAmountZeroForMCCSale As Boolean = False
    Dim isPickPendingMilkSRNinNextPaymentCycle As Boolean = False
#End Region


    Private Sub FrmProvisionEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim isUpdateTSPL_MP_PAY_PROCESS_DETAIL As Boolean = False
        'Dim qry As String = "select * from INFORMATION_SCHEMA.COLUMNS where table_Name='TSPL_MP_PAY_PROCESS_DETAIL' and COLUMN_NAME='Total_Advance_Amount_Recovery'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '    isUpdateTSPL_MP_PAY_PROCESS_DETAIL = True
        'End If


        'Dim coll = New Dictionary(Of String, String)()
        'coll.Add("Total_Advance_Amount", "decimal(18,2) null")
        'coll.Add("Total_Advance_Amount_Recovery", "decimal(18,2) null")
        'coll.Add("Total_Loan_Payment", "decimal(18,2) null")
        'coll.Add("Total_Loan_Payment_Recovery", "decimal(18,2) null")
        'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MP_PAY_PROCESS_DETAIL", coll, Nothing, False, False)
        'If isUpdateTSPL_MP_PAY_PROCESS_DETAIL Then
        '    qry = "update TSPL_MP_PAY_PROCESS_DETAIL set Total_Advance_Amount_Recovery=Total_Advance_Amount"
        '    clsDBFuncationality.ExecuteNonQuery(qry)
        'End If




        'Dim isUpdateTSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE As Boolean = False
        'qry = "select * from INFORMATION_SCHEMA.COLUMNS where table_Name='TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE' and COLUMN_NAME='Knock_Off_Amt'"
        'dt = clsDBFuncationality.GetDataTable(qry)
        'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '    isUpdateTSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE = True
        'End If
        'coll = New Dictionary(Of String, String)()
        'coll.Add("Is_Loan", "integer null")
        'coll.Add("Knock_Off_Amt", "decimal(18,2) null")
        'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE", coll, Nothing, False, False)
        'If isUpdateTSPL_MP_PAY_PROCESS_DETAIL Then
        '    qry = "update TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE set Knock_Off_Amt=Payment_Amount"
        '    clsDBFuncationality.ExecuteNonQuery(qry)
        'End If

        SetUserMgmtNew()
        isConsiderAdvancePayment = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsiderAdvancePayment, clsFixedParameterType.ConsiderAdvancePayment, Nothing)) = 1, True, False)
        PayableAmountZeroForMCCSale = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PayableAmountZeroForFarmerPayment, clsFixedParameterType.PayableAmountZeroForFarmerPayment, Nothing)) = 1, True, False)
        RadPageView1.Pages("RadPageViewPage7").Item.Visibility = IIf(isConsiderAdvancePayment, Telerik.WinControls.ElementVisibility.Visible, Telerik.WinControls.ElementVisibility.Collapsed)
        chkSkipPreviousDocumentOfAdvancePayment.Visible = isConsiderAdvancePayment
        RadPageView1.SelectedPage = RadPageViewPage1
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        'ButtonToolTip.SetToolTip(btnProcess, "Press Alt+P to Post the Transaction")
        isPickPendingMilkSRNinNextPaymentCycle = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickPendingMilkSRNinNextPaymentCycle, clsFixedParameterCode.PickPendingMilkSRNinNextPaymentCycle, Nothing)) = 1
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Public Sub LoadDataFromOtherForm()
        Try
            If clsCommon.myLen(PayProcessDocNo) > 0 Then
                LoadData(PayProcessDocNo, NavigatorType.Current)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Sub LoadBlankGridInvoice()
        gvInvoice.Rows.Clear()
        gvInvoice.Columns.Clear()

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.ReadOnly = True
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvInvoice.MasterTemplate.Columns.Add(colChkBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Purchase Invoice No"
        colTextBox.Name = colPurchaseInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Purchase Invoice Date"
        colTextBox.Name = colPurchaseInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = False
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Name"
        colTextBox.Name = colVLCName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VSP Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Pay To farmer"
        colTextBox.Name = colPayToFarmer
        colTextBox.Width = 50
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VSP Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Main VSP Code"
        colTextBox.Name = colActualVSPCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Main VSP Name"
        colTextBox.Name = colActualVSPName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Name"
        colTextBox.Name = colPayeeJointName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Bank Code"
        colTextBox.Name = colPayeeJointBankCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Bank Name"
        colTextBox.Name = colPayeeJointBankDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Branch Code"
        colTextBox.Name = colPayeeJointBranchCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = False
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Branch Name"
        colTextBox.Name = colPayeeJointBranchDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Account No"
        colTextBox.Name = colPayeeJointAcNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint IFSC Code"
        colTextBox.Name = colPayeeJointIFSC
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Qty"
        colDecimal.Name = colMilkQty
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colInvAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "EMP"
        colDecimal.Name = colEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Total"
        colDecimal.Name = colInvAndEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Incentive"
        colDecimal.Name = colIncenAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Incentive EMP"
        colDecimal.Name = colIncenEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Service Charge Amt"
        colDecimal.Name = colServiceChargeAmt
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Total Amount"
        colDecimal.Name = colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "VSP Own System"
        colDecimal.Name = colVSPOwnSystemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Head Load"
        colDecimal.Name = colHeadLoadAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Deduction"
        colDecimal.Name = colInvDeduc
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Reduce Deduction"
        colDecimal.Name = colReduceDeduc
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = False
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Bank code"
        colTextBox.Name = colBankCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Bank Desc"
        colTextBox.Name = colBankDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payment Mode"
        colTextBox.Name = colPayMode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Cheque No"
        colTextBox.Name = colChequeNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Amount(Farmer)"
        colDecimal.Name = colMPAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "EMP Amount(Farmer)"
        colDecimal.Name = colMPEMPAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Incentive Amount(Farmer)"
        colDecimal.Name = colMPIncentiveAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "EMP Incentive Amount(Farmer)"
        colDecimal.Name = colMPEMPIncentiveAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Amount(Farmer)"
        colDecimal.Name = colMPNetAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        gvInvoice.AllowAddNewRow = False
        gvInvoice.AllowDeleteRow = False
        gvInvoice.ShowGroupPanel = False
        gvInvoice.AllowColumnReorder = True
        gvInvoice.AllowRowReorder = False
        gvInvoice.EnableSorting = True
        gvInvoice.EnableFiltering = True
        gvInvoice.TableElement.TableHeaderHeight = 40
        gvInvoice.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub getVendors()
        strVendorCode = ""
        If gvInvoice.Rows.Count > 0 Then
            For i As Integer = 0 To gvInvoice.Rows.Count - 1
                If gvInvoice.Rows(i).Cells(colSelect).Value = True Then
                    strVendorCode = strVendorCode & "'" & gvInvoice.Rows(i).Cells(colVendorCode).Value & "',"
                End If
            Next
            If clsCommon.myLen(strVendorCode) > 0 Then
                strVendorCode = Microsoft.VisualBasic.Left(strVendorCode, Microsoft.VisualBasic.Len(strVendorCode) - 1)
            End If
        End If
    End Sub

    Function getTotalDeductionReduceDeduSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvDeduction IsNot Nothing AndAlso gvDeduction.Rows.Count > 0 Then
                For i As Integer = 0 To gvDeduction.Rows.Count - 1
                    If gvDeduction.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvDeduction.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colReduceDeduc).Value) > 0 Then
                        rValue = rValue + clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colReduceDeduc).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalDeductionSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvDeduction IsNot Nothing AndAlso gvDeduction.Rows.Count > 0 Then
                For i As Integer = 0 To gvDeduction.Rows.Count - 1
                    If gvDeduction.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvDeduction.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then

                        rValue = rValue + clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalCreditNoteSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvCreditNote IsNot Nothing AndAlso gvCreditNote.Rows.Count > 0 Then
                For i As Integer = 0 To gvCreditNote.Rows.Count - 1
                    If gvCreditNote.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvCreditNote.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then

                        rValue = rValue + clsCommon.myCdbl(gvCreditNote.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalAdvancePayment(ByVal strVendorCode As String) As Double
        Dim rValue As Double = 0
        Try
            If isConsiderAdvancePayment Then
                If clsCommon.myLen(strVendorCode) > 0 AndAlso gvAdvancePayment IsNot Nothing AndAlso gvAdvancePayment.Rows.Count > 0 Then
                    For i As Integer = 0 To gvAdvancePayment.Rows.Count - 1
                        If gvAdvancePayment.Rows(i).Cells(colAPSelect).Value = True AndAlso clsCommon.CompairString(gvAdvancePayment.Rows(i).Cells(colAPVendorCode).Value, strVendorCode) = CompairStringResult.Equal Then
                            rValue = rValue + clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPPaymentAmtBalance).Value)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Math.Round(rValue, 2, MidpointRounding.ToEven)
    End Function

    Function getTotalItemIssueSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvItemIssue IsNot Nothing AndAlso gvItemIssue.Rows.Count > 0 Then
                For i As Integer = 0 To gvItemIssue.Rows.Count - 1
                    If gvItemIssue.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvItemIssue.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then

                        rValue = rValue + clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalItemIssueReturnSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvItemIssueReturn IsNot Nothing AndAlso gvItemIssueReturn.Rows.Count > 0 Then
                For i As Integer = 0 To gvItemIssueReturn.Rows.Count - 1
                    If gvItemIssueReturn.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvItemIssueReturn.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then
                        rValue = rValue + clsCommon.myCdbl(gvItemIssueReturn.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalItemIssueReduceDeduSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvItemIssue IsNot Nothing AndAlso gvItemIssue.Rows.Count > 0 Then
                For i As Integer = 0 To gvItemIssue.Rows.Count - 1
                    If gvItemIssue.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvItemIssue.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colReduceDeduc).Value) > 0 Then
                        rValue = rValue + clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colReduceDeduc).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    'Function getTotalItemIssueReturnReduceDeduSum(ByVal vsp As String) As Double
    '    Dim rValue As Double = 0
    '    Try
    '        If clsCommon.myLen(vsp) > 0 AndAlso gvItemIssueReturn IsNot Nothing AndAlso gvItemIssueReturn.Rows.Count > 0 Then
    '            For i As Integer = 0 To gvItemIssueReturn.Rows.Count - 1
    '                If gvItemIssueReturn.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvItemIssueReturn.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gvItemIssueReturn.Rows(i).Cells(colReduceDeduc).Value) > 0 Then
    '                    rValue = rValue + clsCommon.myCdbl(gvItemIssueReturn.Rows(i).Cells(colReduceDeduc).Value)
    '                End If
    '            Next
    '        End If

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return rValue
    'End Function
    Function getTotalMccSaleSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvMccSale IsNot Nothing AndAlso gvMccSale.Rows.Count > 0 Then
                For i As Integer = 0 To gvMccSale.Rows.Count - 1
                    If gvMccSale.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvMccSale.Rows(i).Cells(colCustomerCode).Value, vsp) = CompairStringResult.Equal Then

                        rValue = rValue + clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Function getTotalMccSaleFarmerSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvMccSaleFarmer IsNot Nothing AndAlso gvMccSaleFarmer.Rows.Count > 0 Then
                For i As Integer = 0 To gvMccSaleFarmer.Rows.Count - 1
                    If gvMccSaleFarmer.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvMccSaleFarmer.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then

                        rValue = rValue + clsCommon.myCdbl(gvMccSaleFarmer.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Function getTotalMccSaleReturnSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso GvMccSaleReturn IsNot Nothing AndAlso GvMccSaleReturn.Rows.Count > 0 Then
                For i As Integer = 0 To GvMccSaleReturn.Rows.Count - 1
                    If GvMccSaleReturn.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(GvMccSaleReturn.Rows(i).Cells(colCustomerCode).Value, vsp) = CompairStringResult.Equal Then

                        rValue = rValue + clsCommon.myCdbl(GvMccSaleReturn.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalMccSaleFarmerSum(ByVal vsp As String, ByVal Farmer_Code As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvMccSaleFarmer IsNot Nothing AndAlso gvMccSaleFarmer.Rows.Count > 0 Then
                For i As Integer = 0 To gvMccSaleFarmer.Rows.Count - 1
                    If gvMccSaleFarmer.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvMccSaleFarmer.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gvMccSaleFarmer.Rows(i).Cells(colFarmerCode).Value, Farmer_Code) = CompairStringResult.Equal Then
                        rValue = rValue + clsCommon.myCdbl(gvMccSaleFarmer.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalMccSaleReturnFarmerSum(ByVal vsp As String, ByVal Farmer_Code As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvMccSaleReturnFarmer IsNot Nothing AndAlso gvMccSaleReturnFarmer.Rows.Count > 0 Then
                For i As Integer = 0 To gvMccSaleReturnFarmer.Rows.Count - 1
                    If gvMccSaleReturnFarmer.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvMccSaleReturnFarmer.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gvMccSaleReturnFarmer.Rows(i).Cells(colFarmerCode).Value, Farmer_Code) = CompairStringResult.Equal Then
                        rValue = rValue + clsCommon.myCdbl(gvMccSaleReturnFarmer.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Function getTotalFarmerAdjustmentSum(ByVal vsp As String, ByVal Farmer_Code As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvMPAdj IsNot Nothing AndAlso gvMPAdj.Rows.Count > 0 Then
                For i As Integer = 0 To gvMPAdj.Rows.Count - 1
                    If gvMPAdj.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvMPAdj.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gvMPAdj.Rows(i).Cells(colFarmerCode).Value, Farmer_Code) = CompairStringResult.Equal Then
                        rValue = rValue + clsCommon.myCdbl(gvMPAdj.Rows(i).Cells(colMPAdjustAmount).Value) * IIf(clsCommon.myCstr(gvMPAdj.Rows(i).Cells(colMPAdjType).Value) = "Payment", -1, 1)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Private Function GetDataTable(ByVal dtg As RadGridView, ByVal columnName As String, ByVal ConditionValue As String) As DataTable
        Dim dt As New DataTable()
        ' add the columns to the datatable            
        If dtg IsNot Nothing Then
            For i As Integer = 0 To dtg.Columns.Count - 1
                dt.Columns.Add(dtg.Columns(i).HeaderText)
            Next
        End If
        '  add each of the data rows to the table
        For j As Integer = 0 To dtg.Rows.Count - 1
            If clsCommon.CompairString(dtg.Rows(j).Cells(columnName).Value, ConditionValue) = CompairStringResult.Equal Then
                Dim dr As DataRow
                dr = dt.NewRow()
                For k As Integer = 0 To dtg.Columns.Count - 1
                    dr(k) = dtg.Rows(j).Cells(k).Value
                Next
                dt.Rows.Add(dr)
            End If
        Next
        Return dt
    End Function
    Function getMccSaleList(ByVal vsp As String) As DataTable
        Dim rValue As DataTable = Nothing
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvMccSale IsNot Nothing AndAlso gvMccSale.Rows.Count > 0 Then
                rValue = GetDataTable(gvMccSale, colCustomerCode, vsp)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Function getMccSaleReturnList(ByVal vsp As String) As DataTable
        Dim rValue As DataTable = Nothing
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso GvMccSaleReturn IsNot Nothing AndAlso GvMccSaleReturn.Rows.Count > 0 Then
                rValue = GetDataTable(GvMccSaleReturn, colCustomerCode, vsp)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Function getCreditNoteList(ByVal vsp As String) As DataTable
        Dim rValue As DataTable = Nothing
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvCreditNote IsNot Nothing AndAlso gvCreditNote.Rows.Count > 0 Then
                rValue = GetDataTable(gvCreditNote, colVendorCode, vsp)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Function getItemIssueList(ByVal vsp As String) As DataTable
        Dim rValue As DataTable = Nothing
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvItemIssue IsNot Nothing AndAlso gvItemIssue.Rows.Count > 0 Then
                rValue = GetDataTable(gvItemIssue, colVendorCode, vsp)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Function getDeductionList(ByVal vsp As String) As DataTable
        Dim rValue As DataTable = Nothing
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvDeduction IsNot Nothing AndAlso gvDeduction.Rows.Count > 0 Then
                rValue = GetDataTable(gvDeduction, colVendorCode, vsp)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Function getTotalMccSaleReduceDeduSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvMccSale IsNot Nothing AndAlso gvMccSale.Rows.Count > 0 Then
                For i As Integer = 0 To gvMccSale.Rows.Count - 1
                    If gvMccSale.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvMccSale.Rows(i).Cells(colCustomerCode).Value, vsp) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colReduceDeduc).Value) > 0 Then

                        rValue = rValue + clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colReduceDeduc).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Sub LoadBlankGridGV()
        gv.Rows.Clear()
        gv.Columns.Clear()

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(colChkBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 80
        colTextBox.ReadOnly = False
        gv.MasterTemplate.Columns.Add(colTextBox)

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "On Hold"
        colChkBox.Name = colIsPaymentProcessHold
        colChkBox.ReadOnly = True
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(colChkBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Purchase Invoice No"
        colTextBox.Name = colPurchaseInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Purchase Invoice Date"
        colTextBox.Name = colPurchaseInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Code"
        colTextBox.Name = colVLCCode
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Name"
        colTextBox.Name = colVLCName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VSP Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VSP Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Pay To farmer"
        colTextBox.Name = colPayToFarmer
        colTextBox.Width = 50
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Main VSP Code"
        colTextBox.Name = colActualVSPCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Main VSP Name"
        colTextBox.Name = colActualVSPName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Name"
        colTextBox.Name = colPayeeJointName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Bank Code"
        colTextBox.Name = colPayeeJointBankCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Bank Name"
        colTextBox.Name = colPayeeJointBankDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Branch Code"
        colTextBox.Name = colPayeeJointBranchCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = False
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Branch Name"
        colTextBox.Name = colPayeeJointBranchDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Account No"
        colTextBox.Name = colPayeeJointAcNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint IFSC Code"
        colTextBox.Name = colPayeeJointIFSC
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        Dim colTextBox1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        colTextBox1.FormatString = ""
        colTextBox1.HeaderText = "Bank code"
        colTextBox1.Name = colBankCode
        colTextBox1.Width = 200
        colTextBox1.ReadOnly = False

        'colTextBox.IsVisible = False
        gv.MasterTemplate.Columns.Add(colTextBox1)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Bank Desc"
        colTextBox.Name = colBankDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        'colTextBox.IsVisible = False
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payment Mode"
        colTextBox.Name = colPayMode
        colTextBox.Width = 200
        colTextBox.ReadOnly = False
        'colTextBox.IsVisible = False
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Cheque No"
        colTextBox.Name = colChequeNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = False
        colTextBox.IsVisible = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colDate = New GridViewDateTimeColumn
        colDate.FormatString = ""
        colDate.HeaderText = "Cheque Date"
        colDate.Name = colChequeDate

        colDate.CustomFormat = "dd/MM/yyyy"
        colDate.FormatString = "{0:dd/MM/yyyy}"

        colDate.Width = 100
        colDate.ReadOnly = False
        colDate.IsVisible = True
        gv.MasterTemplate.Columns.Add(colDate)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Previous Cycle Balance"
        colDecimal.Name = colPrevCycleBalance
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Qty"
        colDecimal.Name = colMilkQty
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Farmer Milk Qty"
        colDecimal.Name = colFarmerMilkQty
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Amount(VSP)"
        colDecimal.Name = colVSPAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Amount(Farmer)"
        colDecimal.Name = colMPAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)



        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "EMP Amount(Farmer)"
        colDecimal.Name = colMPEMPAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Incentive Amount(Farmer)"
        colDecimal.Name = colMPIncentiveAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "EMP Incentive Amount(Farmer)"
        colDecimal.Name = colMPEMPIncentiveAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Net Milk Amount(Farmer)"
        colDecimal.Name = colMPNetAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Diff. Amount(Farmer-VSP)"
        colDecimal.Name = colMPVSPDiffAmount
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = False
        gv.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Amount"
        colDecimal.Name = colInvAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Incentive"
        colDecimal.Name = colIncenAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "EMP"
        colDecimal.Name = colEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = False
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Incentive EMP"
        colDecimal.Name = colIncenEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = False
        gv.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Total EMP"
        colDecimal.Name = colTotalEmp
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Total"
        colDecimal.Name = colInvAndEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = False
        gv.MasterTemplate.Columns.Add(colDecimal)



        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Total Invoice Amount"
        colDecimal.Name = colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "VSP Own System"
        colDecimal.Name = colVSPOwnSystemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Head Load"
        colDecimal.Name = colHeadLoadAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Inv Deduction"
        colDecimal.Name = colInvDeduc
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Reduce Deduction"
        colDecimal.Name = colReduceDeduc
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Service Charge Amt"
        colDecimal.Name = colServiceChargeAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "MCC Sale Amount"
        colDecimal.Name = colMccSaleTotalAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "MCC Sale Return Amount"
        colDecimal.Name = colMccSaleReturnTotalAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Item Issue Amount"
        colDecimal.Name = colItemIssueTotalAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Item Issue Return Amount"
        colDecimal.Name = colItemIssueReturnTotalAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Deduction Amount"
        colDecimal.Name = colDeductionTotalAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Advance Payment"
        colDecimal.Name = colAdvanceAmount
        colDecimal.Width = 150
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = isConsiderAdvancePayment
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Advance Knock Off"
        colDecimal.Name = colAdvanceKnockOffAmount
        colDecimal.Width = 150
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = isConsiderAdvancePayment
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Credit Note Amount"
        colDecimal.Name = colTotalCreditNoteAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Farmer Payment Amount"
        colDecimal.Name = colFarmerPayment
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Farmer Sale Amount"
        colDecimal.Name = colFarmerTotalSale
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Farmer Sale Return Amount"
        colDecimal.Name = colFarmerTotalSaleReturn
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Farmer Adjustment Amount"
        colDecimal.Name = colFarmerAdjustmentAmountTotal
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Payable Amount(Farmer)"
        colDecimal.Name = colMPAmountPayable
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Next Cycle Debit Note(Farmer)"
        ''richa agarwal
        colDecimal.FormatString = "{0:n2}"
        ''----------------
        colDecimal.Name = colNextCycleDebitNoteFarmer
        colDecimal.Width = 200
        colDecimal.IsVisible = True
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Payable Amount(VSP)"
        ''richa agarwal
        colDecimal.FormatString = "{0:n2}"
        ''----------------
        colDecimal.Name = colPaybleAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        'colDecimal = New GridViewDecimalColumn()
        'colDecimal.FormatString = ""
        'colDecimal.HeaderText = "Next Cycle Debit Note(Farmer)"        
        'colDecimal.FormatString = "{0:n2}"
        ' ''----------------
        'colDecimal.Name = colNextCycleDebitNoteFarmer
        'colDecimal.Width = 200
        'colDecimal.IsVisible = True
        'colDecimal.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "VSP Excess Amount"
        colDecimal.FormatString = "{0:n2}"
        colDecimal.Name = colgvVSPExcessAmount
        colDecimal.Width = 200
        colDecimal.IsVisible = True
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Total Deduction (MP)"
        colDecimal.FormatString = "{0:n2}"
        colDecimal.Name = colgvTotMPDeductionAmount
        colDecimal.Width = 200
        colDecimal.IsVisible = True
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Next Cycle Debit Note(VSP)"
        colDecimal.FormatString = "{0:n2}"
        colDecimal.Name = colNextCycleDebitNote
        colDecimal.Width = 200
        colDecimal.IsVisible = True
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        gv.Columns(colBankCode).HeaderImage = My.Resources.search4
        gv.Columns(colBankCode).TextImageRelation = TextImageRelation.TextBeforeImage

        gv.Columns(colPayMode).HeaderImage = My.Resources.search4
        gv.Columns(colPayMode).TextImageRelation = TextImageRelation.TextBeforeImage

        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = True
        gv.EnableFiltering = True
        gv.TableElement.TableHeaderHeight = 40
        gv.BestFitColumns(BestFitColumnMode.AllCells)
        gv.MasterTemplate.AllowEditRow = True

    End Sub
    Sub LoadBlankGridGVFarmer()
        gvPaymentToFarmer.Rows.Clear()
        gvPaymentToFarmer.Columns.Clear()

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colChkBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 80
        colTextBox.ReadOnly = False
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "On Hold"
        colChkBox.Name = colIsPaymentProcessHold
        colChkBox.ReadOnly = True
        colChkBox.IsVisible = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colChkBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Farmer Invoice No"
        colTextBox.Name = colPurchaseInvoiceNo
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Farmer Invoice Date"
        colTextBox.Name = colPurchaseInvoiceDate
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Adjustment No"
        colTextBox.Name = colAPAdjustmentNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Adjustment Date"
        colTextBox.Name = colAPAdjustmentDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Code"
        colTextBox.Name = colVLCCode
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Name"
        colTextBox.Name = colVLCName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Farmer Code"
        colTextBox.Name = colFarmerCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Farmer Name"
        colTextBox.Name = colFarmerName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Account Type" '' name change from Farmer Uploader Code to Account type as pr required to Bipin for client
        colTextBox.Name = colMPUploaderCode
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Farmer Uploader Code"
        colTextBox.Name = colMPAccountType
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VSP Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VSP Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Name"
        colTextBox.Name = colPayeeJointName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Bank Code"
        colTextBox.Name = colPayeeJointBankCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Bank Name"
        colTextBox.Name = colPayeeJointBankDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Branch Code"
        colTextBox.Name = colPayeeJointBranchCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = False
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Branch Name"
        colTextBox.Name = colPayeeJointBranchDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Account No"
        colTextBox.Name = colPayeeJointAcNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint IFSC Code"
        colTextBox.Name = colPayeeJointIFSC
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)


        Dim colTextBox1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        colTextBox1.FormatString = ""
        colTextBox1.HeaderText = "Bank code"
        colTextBox1.Name = colBankCode
        colTextBox1.Width = 200
        colTextBox1.ReadOnly = False

        'colTextBox.IsVisible = False
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox1)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Bank Desc"
        colTextBox.Name = colBankDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        'colTextBox.IsVisible = False
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payment Mode"
        colTextBox.Name = colPayMode
        colTextBox.Width = 200
        colTextBox.ReadOnly = False
        'colTextBox.IsVisible = False
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Cheque No"
        colTextBox.Name = colChequeNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = False
        colTextBox.IsVisible = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colTextBox)

        colDate = New GridViewDateTimeColumn
        colDate.FormatString = ""
        colDate.HeaderText = "Cheque Date"
        colDate.Name = colChequeDate

        colDate.CustomFormat = "dd/MM/yyyy"
        colDate.FormatString = "{0:dd/MM/yyyy}"

        colDate.Width = 100
        colDate.ReadOnly = False
        colDate.IsVisible = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colDate)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Qty"
        colDecimal.Name = colMilkQty
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colDecimal)

        'colDecimal = New GridViewDecimalColumn()
        'colDecimal.FormatString = ""
        'colDecimal.HeaderText = "Milk Amount(VSP)"
        'colDecimal.Name = colVSPAmount
        'colDecimal.Width = 200
        'colDecimal.ReadOnly = True
        'gvPaymentToFarmer.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Amount(Farmer)"
        colDecimal.Name = colMPAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "MCC Sale Amount"
        colDecimal.Name = colMccSaleTotalAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "MCC Sale Return Amount"
        colDecimal.Name = colMccSaleReturnTotalAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Adjustment Amount"
        colDecimal.Name = colMPAdjustAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Incentive Amount"
        colDecimal.Name = colIncentiveAmt
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Advance Amount"
        colDecimal.Name = colFATotalAdvance
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Advance Amount Recovery"
        colDecimal.Name = colFATotalAdvanceRecovery
        colDecimal.Width = 100
        colDecimal.ReadOnly = False
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Loan Payment"
        colDecimal.Name = colFALoanPayment
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Loan Payment Recovery"
        colDecimal.Name = colFALoanPaymentRecovery
        colDecimal.Width = 100
        colDecimal.ReadOnly = False
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Deduction Amount"
        colDecimal.Name = colDeductionAmt
        colDecimal.Width = 100
        colDecimal.ReadOnly = False
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Payable Amount"
        ''richa agarwal
        'colDecimal.FormatString = "{0:n2}"
        ''----------------
        colDecimal.Name = colPaybleAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Next Cycle Debit Note(Farmer)"
        colDecimal.FormatString = "{0:n2}"
        colDecimal.Name = colNextCycleDebitNoteFarmer
        colDecimal.Width = 200
        colDecimal.IsVisible = True
        colDecimal.ReadOnly = True
        gvPaymentToFarmer.MasterTemplate.Columns.Add(colDecimal)

        gvPaymentToFarmer.Columns(colBankCode).HeaderImage = My.Resources.search4
        gvPaymentToFarmer.Columns(colBankCode).TextImageRelation = TextImageRelation.TextBeforeImage

        gvPaymentToFarmer.Columns(colPayMode).HeaderImage = My.Resources.search4
        gvPaymentToFarmer.Columns(colPayMode).TextImageRelation = TextImageRelation.TextBeforeImage

        gvPaymentToFarmer.AllowAddNewRow = False
        gvPaymentToFarmer.AllowDeleteRow = False
        gvPaymentToFarmer.ShowGroupPanel = False
        gvPaymentToFarmer.AllowColumnReorder = True
        gvPaymentToFarmer.AllowRowReorder = False
        gvPaymentToFarmer.EnableSorting = True
        gvPaymentToFarmer.EnableFiltering = True
        gvPaymentToFarmer.TableElement.TableHeaderHeight = 40
        gvPaymentToFarmer.BestFitColumns(BestFitColumnMode.AllCells)
        gvPaymentToFarmer.MasterTemplate.AllowEditRow = True

    End Sub
    Function isMultipleDocumentForSameVendor() As Boolean
        For i As Integer = 0 To gvInvoice.Rows.Count - 1
            For j As Integer = 0 To gvInvoice.Rows.Count - 1
                If i <> j AndAlso clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, gvInvoice.Rows(j).Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function
    Function getAllDeductionSum(ByVal RowNo) As Double
        Dim rValue As Double = 0
        If arrStrDedCode IsNot Nothing AndAlso arrStrDedCode.Count > 0 AndAlso RowNo <= gv.Rows.Count - 1 Then
            For i As Integer = 0 To arrStrDedCode.Count - 1
                rValue = rValue + clsCommon.myCdbl(gv.Rows(RowNo).Cells(arrStrDedCode.Item(i) & "D").Value)
            Next
        End If
        Return rValue
    End Function
    Function getAllItemIssueSum(ByVal RowNo) As Double
        Dim rValue As Double = 0
        If arrStrIssueItemCode IsNot Nothing AndAlso arrStrIssueItemCode.Count > 0 AndAlso RowNo <= gv.Rows.Count - 1 Then
            For i As Integer = 0 To arrStrIssueItemCode.Count - 1
                rValue = rValue + clsCommon.myCdbl(gv.Rows(RowNo).Cells(arrStrIssueItemCode.Item(i) & "I").Value)
            Next
        End If
        Return rValue
    End Function
    Function getAllMccSaleItemSum(ByVal RowNo) As Double
        Dim rValue As Double = 0
        If arrStrMccSaleItemCode IsNot Nothing AndAlso arrStrMccSaleItemCode.Count > 0 AndAlso RowNo <= gv.Rows.Count - 1 Then
            For i As Integer = 0 To arrStrMccSaleItemCode.Count - 1
                rValue = rValue + clsCommon.myCdbl(gv.Rows(RowNo).Cells(arrStrMccSaleItemCode.Item(i) & "S").Value)
            Next
        End If
        Return rValue
    End Function
    Function getMultipleDocumentForSameVendor() As String
        Dim rValue As String = ""
        For i As Integer = 0 To gvInvoice.Rows.Count - 1
            For j As Integer = 0 To gvInvoice.Rows.Count - 1
                If i <> j AndAlso clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, gvInvoice.Rows(j).Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                    rValue = rValue & "Vendor " & gvInvoice.Rows(i).Cells(colVendorCode).Value & " ( " & gvInvoice.Rows(i).Cells(colVendorDesc).Value & " ) " & " Invoice No. " & gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value & " Invoice Date " & gvInvoice.Rows(i).Cells(colAPInvoiceDate).Value & Environment.NewLine
                End If
            Next
        Next
        Return rValue
    End Function
    Sub LoadInvoiceGridData()
        Try
            LoadBlankGridInvoice()
            Dim qry As String = "    select   MAX( xxx.[AP Invoice Doc No]) as [AP Invoice Doc No] ,max(xxx.[Ap Invoice Doc Date]) as [Ap Invoice Doc Date] ,xxx.[Milk Purchase Invoice Doc No] as [Milk Purchase Invoice Doc No],max(xxx.[Milk Purchase Invoice Doc Date]) as [Milk Purchase Invoice Doc Date],max(VLC_Code) as VLC_Code,max(xxx.VLC_Name) as VLC_Name,max(xxx.Vendor_Code)  as Vendor_Code,max(xxx.Vendor_Name) as Vendor_Name,max(xxx.[Payee/Joint Name]) as [Payee/Joint Name],max(xxx.[Bank Code]) as [Bank Code],max(xxx.[Bank Name]) as  [Bank Name] , max(xxx.[Branch Code]) as [Branch Code],max(xxx.[Branch Name]) as [Branch Name],max(xxx.[IFSC Code]) as  [IFSC Code],SUM(xxx.qty) as [Total Qty]   ,max(xxx.TOTAL_basic_amount) as TOTAL_basic_amount,max(xxx.TOTAL_AMOUNT ) as TOTAL_AMOUNT,MAX(xxx.TOTAL_PaymentCOMMISSION) as TOTAL_PaymentCOMMISSION,MAX(xxx.Incentive_Head ) as Incentive_Head, MAX(xxx .IncentiveEMP_Head ) as  IncentiveEMP_Head,sum(Service_Charge_Amount) as Service_Charge_Amount,max(xxx.TOTAL_AMOUNT_Acc  ) as TOTAL_AMOUNT_Acc,max(xxx.MCC_CODE) as  MCC_CODE,max(xxx.AccountNo) as AccountNo,max(MP_Amount) as MP_Amount,max(MP_EMP) as MP_EMP,max(MP_Incentive) as MP_Incentive,max(MP_IncentiveEMP) as MP_IncentiveEMP,max(VSP_Farmer_Billing) as VSP_Farmer_Billing " + Environment.NewLine +
                " from ( 	   select  TSPL_VENDOR_INVOICE_HEAD.Document_No as [AP Invoice Doc No], TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date as [Ap Invoice Doc Date], TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as [Milk Purchase Invoice Doc No],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE as [Milk Purchase Invoice Doc Date],coalesce(TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader,mp_vlc.vlc_code_vlc_uploader) as VLC_Code ,coalesce(TSPL_VLC_MASTER_HEAD.VLC_Name ,mp_vlc.vlc_name) as vlc_name,coalesce(TSPL_VENDOR_MASTER.Vendor_Code,mp_v.vendor_Code) as vendor_Code,coalesce(TSPL_VENDOR_MASTER.Vendor_Name,mp_v.Vendor_name) as Vendor_name , coalesce(TSPL_VENDOR_MASTER.VSP_Payee_Name,Mp_V.VSP_Payee_Name) as [Payee/Joint Name], case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then ''  else ''   end as [Branch Code],case when isnull (coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(SelfBank .Bank_Name,selfBank_mp.bank_name)  else coalesce(jointBank .Bank_Name,jointBank_MP .Bank_Code)   end as [Bank Name],case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(SelfBank .Bank_Code,SelfBank_MP .Bank_Code)   else coalesce(jointBank .Bank_Code,jointBank_Mp .Bank_Code)    end as [Bank Code],case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(TSPL_VENDOR_MASTER .Branch_Name,MP_V .Branch_Name )   else coalesce(TSPL_VENDOR_MASTER .Joint_Branch_Name,Mp_V.Joint_Branch_Name)   end as [Branch Name],case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(TSPL_VENDOR_MASTER .IFSC_Code,mp_V.IFSC_Code)   else coalesce(TSPL_VENDOR_MASTER .Joint_IFSC_Code,mp_v.Joint_IFSC_Code)   end as [IFSC Code],case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(TSPL_VENDOR_MASTER .Account_No,mp_V .Account_No)    else coalesce(TSPL_VENDOR_MASTER.Joint_Account_No,mp_V.Joint_Account_No)    end as [AccountNo],TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty,TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_basic_amount,TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_AMOUNT  , TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION, TSPL_MILK_PURCHASE_INVOICE_HEAD.Incentive_Head,TSPL_MILK_PURCHASE_INVOICE_HEAD.IncentiveEMP_Head, TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_AMOUNT_Acc ,  TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Service_Charge_Amount,TSPL_MILK_PURCHASE_INVOICE_HEAD.MP_Amount,TSPL_MILK_PURCHASE_INVOICE_HEAD.MP_EMP,TSPL_MILK_PURCHASE_INVOICE_HEAD.MP_Incentive,TSPL_MILK_PURCHASE_INVOICE_HEAD.MP_IncentiveEMP,TSPL_VENDOR_MASTER.VSP_Farmer_Billing     " + Environment.NewLine +
                " from TSPL_VENDOR_INVOICE_HEAD  " + Environment.NewLine +
                " left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE= TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No " + Environment.NewLine +
                " left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE  " + Environment.NewLine +
                " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE" + Environment.NewLine +
                " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE " + Environment.NewLine +
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  " + Environment.NewLine +
                " left outer join TSPL_Vendor_Bank_MASTER as jointBank on jointBank.Bank_Code =TSPL_VENDOR_MASTER .Joint_Bank_Code  " + Environment.NewLine +
                " left outer join TSPL_Vendor_Bank_MASTER as SelfBank on SelfBank .Bank_Code =TSPL_VENDOR_MASTER.Bank_Name " _
                & " left outer join TSPL_MP_MASTER mp on mp.MP_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_Code left outer join TSPL_VLC_MASTER_HEAD mp_vlc on mp_vlc.Vlc_Code=mp.VLC_Code Left join tspl_vendor_master Mp_V on mp_V.Vendor_Code=mp.MP_Code  left outer join TSPL_Vendor_Bank_MASTER as jointBank_Mp on jointBank_Mp.Bank_Code =Mp_V .Joint_Bank_Code   left outer join TSPL_Vendor_Bank_MASTER as SelfBank_Mp on SelfBank .Bank_Code =Mp_V.Bank_Name   " _
                & "  where TSPL_VENDOR_INVOICE_HEAD.Balance_Amt>0 and TSPL_VENDOR_INVOICE_HEAD.document_type='I' and TSPL_VENDOR_INVOICE_HEAD.Invoice_Type='AP' and TSPL_VENDOR_INVOICE_HEAD.REFDocType='MI-PI' and ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No,'')<>''    ) xxx where  1=1 "
            Dim whrCls As String = ""
            If clsCommon.myLen(fndLoc.Value) > 0 Then
                whrCls = " And MCC_Code in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "')"
            End If

            qry = qry & whrCls

            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                fndLoc.Focus()
                Throw New Exception("Please select Location")
            End If

            If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
                txtVSP.Focus()
                Throw New Exception("Please select at lease one VSP ")
            End If
            whrCls = " And vendor_code in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"

            qry = qry & whrCls

            whrCls = " and [Milk Purchase Invoice Doc Date] between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'   group by xxx.[Milk Purchase Invoice Doc No]"
            qry = qry & whrCls

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvInvoice.Rows.AddNew()
                    gvInvoice.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvInvoice.Rows(i).Cells(colSelect).Value = True
                    gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value = dt.Rows(i)("AP Invoice Doc No")
                    gvInvoice.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Ap Invoice Doc Date"), "dd/MMM/yyyy")
                    gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value = dt.Rows(i)("Milk Purchase Invoice Doc No")
                    gvInvoice.Rows(i).Cells(colPurchaseInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Milk Purchase Invoice Doc Date"), "dd/MMM/yyyy")
                    gvInvoice.Rows(i).Cells(colVLCCode).Value = dt.Rows(i)("VLC_Code")
                    gvInvoice.Rows(i).Cells(colVLCName).Value = dt.Rows(i)("VLC_Name")
                    gvInvoice.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("Vendor_Code")
                    gvInvoice.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("Vendor_Name")
                    gvInvoice.Rows(i).Cells(colPayToFarmer).Value = clsCommon.myCdbl(dt.Rows(i)("VSP_Farmer_Billing"))
                    gvInvoice.Rows(i).Cells(colPayeeJointName).Value = dt.Rows(i)("Payee/Joint Name")
                    gvInvoice.Rows(i).Cells(colPayeeJointBankCode).Value = dt.Rows(i)("Bank Code")
                    gvInvoice.Rows(i).Cells(colPayeeJointBankDesc).Value = dt.Rows(i)("Bank Name")
                    gvInvoice.Rows(i).Cells(colPayeeJointBranchCode).Value = dt.Rows(i)("Branch Code")
                    gvInvoice.Rows(i).Cells(colPayeeJointBranchDesc).Value = dt.Rows(i)("Branch Name")
                    gvInvoice.Rows(i).Cells(colPayeeJointIFSC).Value = dt.Rows(i)("IFSC Code")
                    gvInvoice.Rows(i).Cells(colPayeeJointAcNo).Value = dt.Rows(i)("AccountNo")
                    gvInvoice.Rows(i).Cells(colMilkQty).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Total Qty")))
                    gvInvoice.Rows(i).Cells(colInvAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("TOTAL_basic_amount")))
                    gvInvoice.Rows(i).Cells(colEmpAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("TOTAL_PaymentCOMMISSION")))
                    gvInvoice.Rows(i).Cells(colInvAndEmpAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("TOTAL_AMOUNT"))) '+ clsCommon.myCdbl(dt.Rows(i)("TOTAL_PaymentCOMMISSION"))
                    gvInvoice.Rows(i).Cells(colIncenAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Incentive_Head")))
                    gvInvoice.Rows(i).Cells(colIncenEmpAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("IncentiveEMP_Head")))

                    '' added by Panch Raj for Debit note of MP and MCC collection
                    gvInvoice.Rows(i).Cells(colMPAmount).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("MP_Amount")))
                    gvInvoice.Rows(i).Cells(colMPEMPAmount).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("MP_EMP")))
                    gvInvoice.Rows(i).Cells(colMPIncentiveAmount).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("MP_Incentive")))
                    gvInvoice.Rows(i).Cells(colMPEMPIncentiveAmount).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("MP_IncentiveEMP")))
                    gvInvoice.Rows(i).Cells(colMPNetAmount).Value = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMPAmount).Value) + clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMPEMPAmount).Value) + clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMPIncentiveAmount).Value) + clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMPEMPIncentiveAmount).Value)

                    gvInvoice.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("TOTAL_AMOUNT_Acc")))
                    gvInvoice.Rows(i).Cells(colVSPOwnSystemAmt).Value = clsCommon.myFormat(getVspOwnSystemAmount(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value))
                    gvInvoice.Rows(i).Cells(colVSPOwnSystemAmt).Tag = getVspOwnSystemINV(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value)
                    gvInvoice.Rows(i).Cells(colHeadLoadAmt).Value = clsCommon.myFormat(clsCommon.myFormat(getHeadLoadAmount(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value)))
                    gvInvoice.Rows(i).Cells(colHeadLoadAmt).Tag = getHeadLoadINV(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value)
                    gvInvoice.Rows(i).Cells(colInvDeduc).Value = clsCommon.myFormat(getDeductionAmount(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value))
                    gvInvoice.Rows(i).Cells(colInvDeduc).Tag = getDeductionINV(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value)
                    gvInvoice.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myFormat(0)
                    gvInvoice.Rows(i).Cells(colBankCode).Value = ""
                    gvInvoice.Rows(i).Cells(colBankDesc).Value = ""
                    gvInvoice.Rows(i).Cells(colPayMode).Value = ""
                    gvInvoice.Rows(i).Cells(colChequeNo).Value = ""
                    gvInvoice.Rows(i).Cells(colServiceChargeAmt).Value = clsCommon.myCdbl(dt.Rows(i)("Service_Charge_Amount"))
                Next
            End If


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            gvInvoice.SummaryRowsBottom.Clear()

            For iii As Integer = 0 To gvInvoice.Columns.Count - 1
                If TypeOf (gvInvoice.Columns(iii)) Is GridViewDecimalColumn Then
                    summaryRowItem.Add(New GridViewSummaryItem(gvInvoice.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                End If
            Next
            gvInvoice.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Function getVspOwnSystemAmount(ByVal strMilkPurchaseInvoiceNo As String) As Double
        Dim rValue As Double = 0
        Dim qry As String = " select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD where Document_Type='C' and RefDocType='Milk_OW' and Against_MillkPurchaseInvoice_No='" & strMilkPurchaseInvoiceNo & "' and Balance_Amt<>0 "
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        Return rValue
    End Function
    Function getVspOwnSystemINV(ByVal strMilkPurchaseInvoiceNo As String) As String
        Dim rValue As String = ""
        Dim qry As String = " select Document_No from TSPL_VENDOR_INVOICE_HEAD where Document_Type='C' and RefDocType='Milk_OW' and Against_MillkPurchaseInvoice_No='" & strMilkPurchaseInvoiceNo & "' and Balance_Amt<>0 "
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return rValue
    End Function

    Function getHeadLoadINV(ByVal strMilkPurchaseInvoiceNo As String) As String
        Dim rValue As String = ""
        Dim qry As String = " select Document_No from TSPL_VENDOR_INVOICE_HEAD where Document_Type='C' and RefDocType='Milk_HE' and Against_MillkPurchaseInvoice_No='" & strMilkPurchaseInvoiceNo & "' and Balance_Amt<>0 "
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return rValue
    End Function

    Function getDeductionINV(ByVal strMilkPurchaseInvoiceNo As String) As String
        Dim rValue As String = ""
        Dim qry As String = " select Document_No from TSPL_VENDOR_INVOICE_HEAD where Document_Type='D' and RefDocType='Milk_DE' and Against_MillkPurchaseInvoice_No='" & strMilkPurchaseInvoiceNo & "' and Balance_Amt<>0 "
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return rValue
    End Function

    Function getHeadLoadAmount(ByVal strMilkPurchaseInvoiceNo As String) As Double
        Dim rValue As Double = 0
        Dim qry As String = " select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD where Document_Type='C' and RefDocType='Milk_HE' and Against_MillkPurchaseInvoice_No='" & strMilkPurchaseInvoiceNo & "' and Balance_Amt<>0 "
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        Return rValue
    End Function

    Function getDeductionAmount(ByVal strMilkPurchaseInvoiceNo As String) As Double
        Dim rValue As Double = 0
        Dim qry As String = " select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD where Document_Type='D' and RefDocType='Milk_DE' and Against_MillkPurchaseInvoice_No='" & strMilkPurchaseInvoiceNo & "' and Balance_Amt<>0 "
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        Return rValue
    End Function

    Sub LoadBlankGridMccSale()
        gvMccSale.Rows.Clear()
        gvMccSale.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = True
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvMccSale.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Shipment Doc No"
        colTextBox.Name = colShipmentNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Shipment Doc Date"
        colTextBox.Name = colShipmentDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Sale Doc No"
        colTextBox.Name = colSaleInvNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Sale Doc Date"
        colTextBox.Name = colSaleInvDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AR Invoice No"
        colTextBox.Name = colARInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AR Invoice Date"
        colTextBox.Name = colARInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colCustomerCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colCustomerName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)



        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Item Code"
        'colTextBox.Name = colItemCode
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvMccSale.MasterTemplate.Columns.Add(colTextBox)



        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Item Desc"
        'colTextBox.Name = colItemDesc
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Reduce" & Environment.NewLine & "Deduction"
        colDecimal.Name = colReduceDeduc
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = False
        colDecimal.WrapText = True
        gvMccSale.MasterTemplate.Columns.Add(colDecimal)


        gvMccSale.AllowAddNewRow = False
        gvMccSale.AllowDeleteRow = False
        gvMccSale.ShowGroupPanel = False
        gvMccSale.AllowColumnReorder = True
        gvMccSale.AllowRowReorder = False
        gvMccSale.EnableSorting = True
        gvMccSale.EnableFiltering = True
        gvMccSale.TableElement.TableHeaderHeight = 40
        gvMccSale.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub
    Sub LoadBlankGridMccSaleFarmer()
        gvMccSaleFarmer.Rows.Clear()
        gvMccSaleFarmer.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = True
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvMccSaleFarmer.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSaleFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Shipment Doc No"
        colTextBox.Name = colShipmentNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSaleFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Shipment Doc Date"
        colTextBox.Name = colShipmentDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSaleFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Sale Doc No"
        colTextBox.Name = colSaleInvNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSaleFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Sale Doc Date"
        colTextBox.Name = colSaleInvDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSaleFarmer.MasterTemplate.Columns.Add(colTextBox)

        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "AR Invoice No"
        'colTextBox.Name = colARInvoiceNo
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvMccSaleFarmer.MasterTemplate.Columns.Add(colTextBox)

        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "AR Invoice Date"
        'colTextBox.Name = colARInvoiceDate
        'colTextBox.Width = 150
        'colTextBox.ReadOnly = True
        'gvMccSaleFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Farmer Code"
        colTextBox.Name = colFarmerCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSaleFarmer.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Farmer Name"
        colTextBox.Name = colFarmerName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSaleFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSaleFarmer.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSaleFarmer.MasterTemplate.Columns.Add(colTextBox)





        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Item Code"
        'colTextBox.Name = colItemCode
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvMccSaleFarmer.MasterTemplate.Columns.Add(colTextBox)



        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Item Desc"
        'colTextBox.Name = colItemDesc
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvMccSaleFarmer.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvMccSaleFarmer.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Reduce" & Environment.NewLine & "Deduction"
        colDecimal.Name = colReduceDeduc
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        colDecimal.WrapText = True
        colDecimal.IsVisible = False
        gvMccSaleFarmer.MasterTemplate.Columns.Add(colDecimal)


        gvMccSaleFarmer.AllowAddNewRow = False
        gvMccSaleFarmer.AllowDeleteRow = False
        gvMccSaleFarmer.ShowGroupPanel = False
        gvMccSaleFarmer.AllowColumnReorder = True
        gvMccSaleFarmer.AllowRowReorder = False
        gvMccSaleFarmer.EnableSorting = True
        gvMccSaleFarmer.EnableFiltering = True
        gvMccSaleFarmer.TableElement.TableHeaderHeight = 40
        gvMccSaleFarmer.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub LoadBlankGridMccSaleReturn()
        GvMccSaleReturn.Rows.Clear()
        GvMccSaleReturn.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = True
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        GvMccSaleReturn.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Return Doc No"
        colTextBox.Name = colReturnDocNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Return Doc Date"
        colTextBox.Name = colReturnDocDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Return Doc Type"
        colTextBox.Name = colReturnDocType
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AR Invoice No"
        colTextBox.Name = colARInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AR Invoice Date"
        colTextBox.Name = colARInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Shipment Doc No"
        colTextBox.Name = colShipmentNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Shipment Doc Date"
        colTextBox.Name = colShipmentDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Sale Doc No"
        colTextBox.Name = colSaleInvNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Sale Doc Date"
        colTextBox.Name = colSaleInvDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colCustomerCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colCustomerName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)



        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Item Code"
        'colTextBox.Name = colItemCode
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvMccsaleReturn.MasterTemplate.Columns.Add(colTextBox)



        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Item Desc"
        'colTextBox.Name = colItemDesc
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvMccsaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colDecimal)

        'colDecimal = New GridViewDecimalColumn()
        'colDecimal.FormatString = ""
        'colDecimal.HeaderText = "Reduce" & Environment.NewLine & "Deduction"
        'colDecimal.Name = colReduceDeduc
        'colDecimal.Width = 100
        'colDecimal.ReadOnly = False
        'colDecimal.WrapText = True
        'GvMccSaleReturn.MasterTemplate.Columns.Add(colDecimal)


        GvMccSaleReturn.AllowAddNewRow = False
        GvMccSaleReturn.AllowDeleteRow = False
        GvMccSaleReturn.ShowGroupPanel = False
        GvMccSaleReturn.AllowColumnReorder = True
        GvMccSaleReturn.AllowRowReorder = False
        GvMccSaleReturn.EnableSorting = True
        GvMccSaleReturn.EnableFiltering = True
        GvMccSaleReturn.TableElement.TableHeaderHeight = 40
        GvMccSaleReturn.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub
    Sub LoadBlankGridMccSaleReturnFarmer()
        gvMccSaleReturnFarmer.Rows.Clear()
        gvMccSaleReturnFarmer.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = True
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Return Doc No"
        colTextBox.Name = colReturnDocNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Return Doc Date"
        colTextBox.Name = colReturnDocDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Return Doc Type"
        colTextBox.Name = colReturnDocType
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colTextBox)

        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "AR Invoice No"
        'colTextBox.Name = colARInvoiceNo
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colTextBox)

        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "AR Invoice Date"
        'colTextBox.Name = colARInvoiceDate
        'colTextBox.Width = 150
        'colTextBox.ReadOnly = True
        'gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Shipment Doc No"
        colTextBox.Name = colShipmentNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Shipment Doc Date"
        colTextBox.Name = colShipmentDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Sale Doc No"
        colTextBox.Name = colSaleInvNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Sale Doc Date"
        colTextBox.Name = colSaleInvDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Farmer Code"
        colTextBox.Name = colFarmerCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Farmer Name"
        colTextBox.Name = colFarmerName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvMccSaleReturnFarmer.MasterTemplate.Columns.Add(colDecimal)

        gvMccSaleReturnFarmer.AllowAddNewRow = False
        gvMccSaleReturnFarmer.AllowDeleteRow = False
        gvMccSaleReturnFarmer.ShowGroupPanel = False
        gvMccSaleReturnFarmer.AllowColumnReorder = True
        gvMccSaleReturnFarmer.AllowRowReorder = False
        gvMccSaleReturnFarmer.EnableSorting = True
        gvMccSaleReturnFarmer.EnableFiltering = True
        gvMccSaleReturnFarmer.TableElement.TableHeaderHeight = 40
        gvMccSaleReturnFarmer.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub
    Sub LoadBlankGridFarmerAdjustment()
        gvMPAdj.Rows.Clear()
        gvMPAdj.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvMPAdj.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 50
        colTextBox.ReadOnly = True
        gvMPAdj.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Adjustment No"
        colTextBox.Name = colAdjNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMPAdj.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Adjustment Date"
        colTextBox.Name = colMPAdjDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMPAdj.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Adjustment Type"
        colTextBox.Name = colMPAdjType
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvMPAdj.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Description"
        colTextBox.Name = colMPADjDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMPAdj.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Remarks"
        colTextBox.Name = colMPAdjRemarks
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMPAdj.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Farmer Code"
        colTextBox.Name = colFarmerCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMPAdj.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Farmer Name"
        colTextBox.Name = colFarmerName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMPAdj.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMPAdj.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMPAdj.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Adjustment Amount"
        colDecimal.Name = colMPAdjustAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvMPAdj.MasterTemplate.Columns.Add(colDecimal)


        gvMPAdj.AllowAddNewRow = False
        gvMPAdj.AllowDeleteRow = False
        gvMPAdj.ShowGroupPanel = False
        gvMPAdj.AllowColumnReorder = True
        gvMPAdj.AllowRowReorder = False
        gvMPAdj.EnableSorting = True
        gvMPAdj.EnableFiltering = True
        gvMPAdj.TableElement.TableHeaderHeight = 40
        gvMPAdj.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub LoadBlankGridDeduction()
        gvDeduction.Rows.Clear()
        gvDeduction.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = True
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvDeduction.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Deduction Code"
        colTextBox.Name = colDeductionCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Deduction Desc"
        colTextBox.Name = colDeductionDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Reduce Deduction"
        colDecimal.Name = colReduceDeduc
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = False
        gvDeduction.MasterTemplate.Columns.Add(colDecimal)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Is Previous Balance"
        colTextBox.Name = colIsFromPrevPPCycle
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)


        gvDeduction.AllowAddNewRow = False
        gvDeduction.AllowDeleteRow = False
        gvDeduction.ShowGroupPanel = False
        gvDeduction.AllowColumnReorder = True
        gvDeduction.AllowRowReorder = False
        gvDeduction.EnableSorting = True
        gvDeduction.EnableFiltering = True
        gvDeduction.TableElement.TableHeaderHeight = 40
        gvDeduction.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub LoadBlankGridAdvancePayment()
        gvAdvancePayment.Rows.Clear()
        gvAdvancePayment.Columns.Clear()

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = " "
        colChkBox.Name = colAPSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 30
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvAdvancePayment.MasterTemplate.Columns.Add(colChkBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SNo"
        colTextBox.Name = colAPSNo
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gvAdvancePayment.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colAPVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvAdvancePayment.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colAPVendorName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvAdvancePayment.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payment No"
        colTextBox.Name = colAPPaymentCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvAdvancePayment.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payment Date"
        colTextBox.Name = colAPPaymentDate
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvAdvancePayment.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colAPPaymentAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvAdvancePayment.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Balance Amount"
        colDecimal.Name = colAPPaymentAmtBalance
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvAdvancePayment.MasterTemplate.Columns.Add(colDecimal)

        gvAdvancePayment.AllowAddNewRow = False
        gvAdvancePayment.AllowDeleteRow = False
        gvAdvancePayment.ShowGroupPanel = False
        gvAdvancePayment.AllowColumnReorder = True
        gvAdvancePayment.AllowRowReorder = False
        gvAdvancePayment.EnableSorting = True
        gvAdvancePayment.EnableFiltering = True
        gvAdvancePayment.TableElement.TableHeaderHeight = 40
        gvAdvancePayment.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub LoadBlankGridFAAdvancePayment()
        gvFA.Rows.Clear()
        gvFA.Columns.Clear()

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "MP Code"
        colTextBox.Name = colFAFarmerCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvFA.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "MP Name"
        colTextBox.Name = colFAFarmerName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvFA.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payment No"
        colTextBox.Name = colFAPaymentCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvFA.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payment Date"
        colTextBox.Name = colFAPaymentDate
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvFA.MasterTemplate.Columns.Add(colTextBox)

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Loan"
        colChkBox.Name = colFALoan
        colChkBox.ReadOnly = True
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvFA.MasterTemplate.Columns.Add(colChkBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colFAPaymentAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvFA.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Knock Off"
        colDecimal.Name = colFAKnockOff
        colDecimal.IsVisible = False
        colDecimal.ReadOnly = True
        gvFA.MasterTemplate.Columns.Add(colDecimal)

        gvFA.AllowAddNewRow = False
        gvFA.AllowDeleteRow = False
        gvFA.ShowGroupPanel = False
        gvFA.AllowColumnReorder = True
        gvFA.AllowRowReorder = False
        gvFA.EnableSorting = True
        gvFA.EnableFiltering = True
        gvFA.TableElement.TableHeaderHeight = 40
        gvFA.BestFitColumns(BestFitColumnMode.AllCells)

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gvFA.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To gvFA.Columns.Count - 1
            If TypeOf (gvFA.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gvFA.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvFA.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Sub LoadBlankGridCreditNote()
        gvCreditNote.Rows.Clear()
        gvCreditNote.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvCreditNote.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvCreditNote.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvCreditNote.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvCreditNote.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvCreditNote.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvCreditNote.MasterTemplate.Columns.Add(colTextBox)





        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvCreditNote.MasterTemplate.Columns.Add(colDecimal)




        gvCreditNote.AllowAddNewRow = False
        gvCreditNote.AllowDeleteRow = False
        gvCreditNote.ShowGroupPanel = False
        gvCreditNote.AllowColumnReorder = True
        gvCreditNote.AllowRowReorder = False
        gvCreditNote.EnableSorting = True
        gvCreditNote.EnableFiltering = True
        gvCreditNote.TableElement.TableHeaderHeight = 40
        gvCreditNote.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub LoadDeductionGridData()
        '' Updation by pankaj jha in query  Against Ticket No : BM00000008003
        'gvDeduction.Rows.Clear()
        LoadBlankGridDeduction()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "   select coalesce((select 1 from  TSPL_PAYMENT_PROCESS_DETAIL where PP_Detail_No= TSPL_VENDOR_INVOICE_HEAD.RefDocNo " &
                " and CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.POSTING_DATE,103)> CONVERT(DATE,TSPL_PAYMENT_PROCESS_DETAIL.AP_INVOICE_DATE,103)),0) as IsFromPrevPPCycle,TSPL_VENDOR_INVOICE_DETAIL.Document_No,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code, " &
                " TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc,TSPL_VENDOR_INVOICE_DETAIL.Total_Amount " &
                " from TSPL_VENDOR_INVOICE_DETAIL left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No " &
                " where  Document_Type='D' " &
                " and ((TSPL_VENDOR_INVOICE_HEAD.isDeduction='1' " &
                " and ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,'')<>'') or  len(coalesce(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,''))>0)  " &
                " and TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 "
            Dim whrCls As String = ""


            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  in ( " & strVendorCode & ")  and  coalesce(Posting_Date,'')<>'' "
            End If

            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                whrCls += "and 2=(case when TSPL_VENDOR_INVOICE_HEAD.isDeduction='1' and TSPL_VENDOR_INVOICE_DETAIL.DeductionCode='SECURITY DED' then" + Environment.NewLine +
                  " case when convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >= '" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") + "' then 2 " + Environment.NewLine +
                  " else 0 end else 2 end )"
            End If

            qry = qry & whrCls & "  and TSPL_VENDOR_INVOICE_HEAD.Loc_Code ='" & fndLoc.Value & "' and " & IIf(chkSkipPrevDeduction.Checked, " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' ")
            'in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code



            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvDeduction.Rows.AddNew()
                    gvDeduction.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvDeduction.Rows(i).Cells(colSelect).Value = True
                    gvDeduction.Rows(i).Cells(colAPInvoiceNo).Value = dt.Rows(i)("Document_No")
                    gvDeduction.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Invoice_Entry_Date"), "dd/MMM/yyyy")
                    gvDeduction.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("Vendor_Code")
                    gvDeduction.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("Vendor_Name")
                    gvDeduction.Rows(i).Cells(colDeductionCode).Value = dt.Rows(i)("DeductionCode")
                    gvDeduction.Rows(i).Cells(colDeductionDesc).Value = dt.Rows(i)("Deduction_Desc")
                    gvDeduction.Rows(i).Cells(colItemAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Total_Amount")))
                    gvDeduction.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myFormat(0)
                    gvDeduction.Rows(i).Cells(colIsFromPrevPPCycle).Value = clsCommon.myCdbl(dt.Rows(i)("IsFromPrevPPCycle"))
                Next
            End If
        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gvDeduction.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To gvDeduction.Columns.Count - 1
            If TypeOf (gvDeduction.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gvDeduction.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvDeduction.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Sub LoadCreditNoteGridData()
        'gvDeduction.Rows.Clear()
        LoadBlankGridCreditNote()
        If clsCommon.myLen(strVendorCode) > 0 Then
            '' Update By pankaj jha For picking up amount from Head in place of details 
            Dim qry As String = "   select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_HEAD.document_total   as Total_Amount   from TSPL_VENDOR_INVOICE_head   where   TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 and coalesce(refDocType,'') not in ('Milk_HE','Milk_OW','V_I_Issue_Return')  "
            Dim whrCls As String = ""


            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  in ( " & strVendorCode & ")   and  coalesce(Posting_Date,'')<>''"
            End If
            qry = qry & whrCls & "  and TSPL_VENDOR_INVOICE_HEAD.Loc_Code ='" & fndLoc.Value & "' and " & IIf(chkSkipPrevCreditNote.Checked, " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' ")
            'in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvCreditNote.Rows.AddNew()
                    gvCreditNote.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvCreditNote.Rows(i).Cells(colSelect).Value = True
                    gvCreditNote.Rows(i).Cells(colAPInvoiceNo).Value = dt.Rows(i)("Document_No")
                    gvCreditNote.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Invoice_Entry_Date"), "dd/MMM/yyyy")
                    gvCreditNote.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("Vendor_Code")
                    gvCreditNote.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("Vendor_Name")
                    gvCreditNote.Rows(i).Cells(colItemAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Total_Amount")))
                Next
            End If
        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gvCreditNote.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To gvCreditNote.Columns.Count - 1
            If TypeOf (gvCreditNote.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gvCreditNote.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvCreditNote.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Sub LoadAdvancePaymentGridData()
        If isConsiderAdvancePayment Then
            LoadBlankGridAdvancePayment()
            If clsCommon.myLen(strVendorCode) > 0 Then
                Dim dtToDateForQry As DateTime = dtpToDate.Value
                If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotConsiderTheFutureDateOfAdvancePayment, clsFixedParameterCode.DoNotConsiderTheFutureDateOfAdvancePayment, Nothing)) = 0) Then
                    dtToDateForQry = clsCommon.GETSERVERDATE()
                End If

                Dim qry As String = "Select * from (" &
               " Select TSPL_PAYMENT_HEADER.Vendor_Code,TSPL_PAYMENT_HEADER.Vendor_Name,TSPL_PAYMENT_HEADER.Payment_No,TSPL_PAYMENT_HEADER.Payment_Date,TSPL_PAYMENT_HEADER.Payment_Amount+isnull(TDS_Amount ,0) as Payment_Amount," &
               " Balance_Amt+isnull(TDS_Amount ,0)-ISNULL((Select SUM(Payment_Amount)+sum(isnull(TDS_Amount ,0)) from TSPL_PAYMENT_HEADER PH WHERE PH.Posted<>'1' AND PH.Payment_Type='AD' AND PH.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code AND PH.Applied_Payment=TSPL_PAYMENT_HEADER.Payment_No),0) as Balance_Amt  from TSPL_PAYMENT_HEADER WHERE Posted='1' "
                If clsCommon.myLen(strVendorCode) <= 0 Then
                Else
                    qry += " AND Vendor_Code in  (" + strVendorCode + ") "
                End If
                qry += " AND   IsChkReverse='N' and Payment_Type IN ('AV','OA') "
                If chkSkipPreviousDocumentOfAdvancePayment.Checked Then
                    qry += " and TSPL_PAYMENT_HEADER.Payment_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "
                End If
                qry += " and TSPL_PAYMENT_HEADER.Payment_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDateForQry), "dd/MMM/yyyy hh:mm tt") + "' " &
               " ) Final where Balance_Amt>0 order by Payment_Date"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        gvAdvancePayment.Rows.AddNew()
                        gvAdvancePayment.Rows(i).Cells(colAPSelect).Value = True
                        gvAdvancePayment.Rows(i).Cells(colAPSNo).Value = (i + 1)
                        gvAdvancePayment.Rows(i).Cells(colAPVendorCode).Value = clsCommon.myCstr(dt.Rows(i)("Vendor_Code"))
                        gvAdvancePayment.Rows(i).Cells(colAPVendorName).Value = clsCommon.myCstr(dt.Rows(i)("Vendor_Name"))
                        gvAdvancePayment.Rows(i).Cells(colAPPaymentCode).Value = clsCommon.myCstr(dt.Rows(i)("Payment_No"))
                        gvAdvancePayment.Rows(i).Cells(colAPPaymentDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Payment_Date"), "dd/MM/yyyy")
                        gvAdvancePayment.Rows(i).Cells(colAPPaymentAmt).Value = clsCommon.myCdbl(dt.Rows(i)("Payment_Amount"))
                        gvAdvancePayment.Rows(i).Cells(colAPPaymentAmtBalance).Value = clsCommon.myCdbl(dt.Rows(i)("Balance_Amt"))
                    Next
                End If
            End If
        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gvAdvancePayment.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To gvAdvancePayment.Columns.Count - 1
            If TypeOf (gvAdvancePayment.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gvAdvancePayment.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvAdvancePayment.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub



    Sub LoadMccSaleGridData()
        'gvMccSale.Rows.Clear()
        LoadBlankGridMccSale()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "   select TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE as [Shipment_No],TSPL_SD_SHIPMENT_HEAD.Document_Date as [Shipment_Date] ,TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code [Vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name as [Vendor_Name] , TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Sale_Inoivce_Date] ,TSPL_Customer_Invoice_Head.Document_No as [AR_Invoice_No] ,   TSPL_Customer_Invoice_Head.Document_Date as [AR_Invoice_Date], TSPL_Customer_Invoice_Head.Balance_Amt   from TSPL_SD_SHIPMENT_HEAD  left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code     inner join TSPL_Customer_Invoice_Head on  TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No  and coalesce(TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>''   left outer join TSPL_VENDOR_MASTER   on  TSPL_VENDOR_MASTER .Vendor_code  =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code   left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No   where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC' and " & IIf(chkSkipPrevMccSale.Checked, " convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") & " and TSPL_Customer_Invoice_Head.Loc_Code  ='" & fndLoc.Value & "'  and tspl_customer_invoice_head.Balance_Amt<>0 "
            Dim whrCls As String = ""


            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code  in (  " & strVendorCode & " )"
            End If
            qry = qry & whrCls

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvMccSale.Rows.AddNew()
                    gvMccSale.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvMccSale.Rows(i).Cells(colSelect).Value = True
                    gvMccSale.Rows(i).Cells(colShipmentNo).Value = dt.Rows(i)("Shipment_No")
                    gvMccSale.Rows(i).Cells(colShipmentDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Shipment_Date"), "dd/MMM/yyyy")
                    gvMccSale.Rows(i).Cells(colSaleInvNo).Value = dt.Rows(i)("Sale_Invoice_No")
                    gvMccSale.Rows(i).Cells(colSaleInvDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Sale_Inoivce_Date"), "dd/MMM/yyyy")
                    gvMccSale.Rows(i).Cells(colARInvoiceNo).Value = dt.Rows(i)("AR_Invoice_No")
                    gvMccSale.Rows(i).Cells(colARInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("AR_Invoice_Date"), "dd/MMM/yyyy")
                    gvMccSale.Rows(i).Cells(colCustomerCode).Value = dt.Rows(i)("Vendor_Code")
                    gvMccSale.Rows(i).Cells(colCustomerName).Value = dt.Rows(i)("Vendor_Name")
                    gvMccSale.Rows(i).Cells(colItemAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Balance_Amt")))
                    'gvMccSale.Rows(i).Cells(colReduceDeduc).Value = 0
                Next
            End If
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gvMccSale.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To gvMccSale.Columns.Count - 1
            If TypeOf (gvMccSale.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gvMccSale.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvMccSale.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Sub LoadMccSaleGridDataFarmer()
        LoadBlankGridMccSaleFarmer()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = ""
            qry = "select max([Shipment_No]) as [Shipment_No],max([Shipment_Date]) as [Shipment_Date], max([Farmer Code]) as [Farmer Code],max([Farmer Name]) as [Farmer Name] ,max([Vendor_Code]) as [Vendor_Code] ,max([Vendor_Name]) as [Vendor_Name] ,  Sale_Invoice_No,max([Sale_Inoivce_Date]) as [Sale_Inoivce_Date] ,sum(total_amt*RI) as Balance_Amt from (" + Environment.NewLine +
                  " select TSPL_MCC_Sale_Farmer_Head.DOCUMENT_CODE as [Shipment_No],TSPL_MCC_Sale_Farmer_Head.Document_Date as [Shipment_Date]," &
                  " TSPL_MCC_Sale_Farmer_Head.Farmer_Code AS [Farmer Code],MP.MP_Name as [Farmer Name] ,VLC.VSP_Code as [Vendor_Code] ,VSP.Vendor_Name as [Vendor_Name] , " &
                  " TSPL_MCC_Sale_Farmer_Head.Sale_Invoice_No,TSPL_MCC_Sale_Farmer_Head.Sale_Invoice_Date as [Sale_Inoivce_Date] , TSPL_MCC_Sale_Farmer_Head.total_amt,1 as RI,1 as Chk " &
                  " from TSPL_MCC_Sale_Farmer_Head left join TSPL_MP_MASTER MP on MP.MP_Code= TSPL_MCC_Sale_Farmer_Head.Farmer_Code " &
                  " LEFT JOIN TSPL_VLC_MASTER_HEAD VLC ON VLC.VLC_Code=MP.VLC_Code " &
                  " left join TSPL_VENDOR_MASTER VSP   on  VSP.Vendor_code=VLC.VSP_Code " &
                  " where 2=2 and TSPL_MCC_Sale_Farmer_Head.Trans_Type='MCC' " &
                  " and " & IIf(chkSkipPrevMccSale.Checked, " convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") & " and TSPL_MCC_Sale_Farmer_Head.Bill_To_Location in ( select Location_Code from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "')    " + Environment.NewLine
            If clsCommon.myLen(strVendorCode) > 0 Then
                qry += " and VLC.VSP_Code  in (  " & strVendorCode & " )"
            End If
            qry += " union all" + Environment.NewLine +
                  " select null as [Shipment_No],null as [Shipment_Date], null as [Farmer Code],null as [Farmer Name] ,null as [Vendor_Code] ,null as [Vendor_Name] ,  TSPL_MP_PAY_PROCESS_MCC_SALE.Sale_Doc_No as Sale_Invoice_No,null as [Sale_Inoivce_Date] ,TSPL_MP_PAY_PROCESS_MCC_SALE.Amount as total_amt,-1 as RI,0 as chk from TSPL_MP_PAY_PROCESS_MCC_SALE where Doc_No not in ('" + fndDocNo.Value + "')" + Environment.NewLine +
                  " )xx group by Sale_Invoice_No having sum(chk)>0 and sum(total_amt*RI)>0 "


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvMccSaleFarmer.Rows.AddNew()
                    gvMccSaleFarmer.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvMccSaleFarmer.Rows(i).Cells(colSelect).Value = True
                    gvMccSaleFarmer.Rows(i).Cells(colShipmentNo).Value = dt.Rows(i)("Shipment_No")
                    gvMccSaleFarmer.Rows(i).Cells(colShipmentDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Shipment_Date"), "dd/MMM/yyyy")
                    gvMccSaleFarmer.Rows(i).Cells(colSaleInvNo).Value = dt.Rows(i)("Sale_Invoice_No")
                    gvMccSaleFarmer.Rows(i).Cells(colSaleInvDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Sale_Inoivce_Date"), "dd/MMM/yyyy")
                    gvMccSaleFarmer.Rows(i).Cells(colFarmerCode).Value = clsCommon.myCstr(dt.Rows(i)("Farmer Code"))
                    gvMccSaleFarmer.Rows(i).Cells(colFarmerName).Value = clsCommon.myCstr(dt.Rows(i)("Farmer Name"))
                    gvMccSaleFarmer.Rows(i).Cells(colVendorCode).Value = clsCommon.myCstr(dt.Rows(i)("Vendor_Code"))
                    gvMccSaleFarmer.Rows(i).Cells(colVendorDesc).Value = clsCommon.myCstr(dt.Rows(i)("Vendor_Name"))
                    gvMccSaleFarmer.Rows(i).Cells(colItemAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Balance_Amt")))
                Next
            End If
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gvMccSaleFarmer.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To gvMccSaleFarmer.Columns.Count - 1
            If TypeOf (gvMccSaleFarmer.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gvMccSaleFarmer.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvMccSaleFarmer.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        isCellValueChanged = False
    End Sub

    Sub LoadMccSaleReturnGridData()
        'GvMccSaleReturn.Rows.Clear()
        LoadBlankGridMccSaleReturn()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "   select TSPL_SD_SALE_RETURN_HEAD.Document_Code,case when coalesce(TSPL_SD_SALE_RETURN_HEAD.Return_Type,'')='D' then 'Damaged Goods' when coalesce(TSPL_SD_SALE_RETURN_HEAD.Return_Type,'')='P' then 'Price Only' when coalesce(TSPL_SD_SALE_RETURN_HEAD.Return_Type,'')='I' then 'Inventory Type' end as Document_Type,TSPL_SD_SALE_RETURN_HEAD.Document_Date,TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE as [Shipment_No]" _
                & " ,TSPL_SD_SHIPMENT_HEAD.Document_Date as [Shipment_Date] ,TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code [Vendor_Code] ," _
                & " TSPL_VENDOR_MASTER.Vendor_Name as [Vendor_Name] , TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as " _
                & " [Sale_Inoivce_Date] ,TSPL_Customer_Invoice_Head.Document_No as [AR_Invoice_No] ,   TSPL_Customer_Invoice_Head.Document_Date as " _
                & " [AR_Invoice_Date], TSPL_Customer_Invoice_Head.Balance_Amt   from TSPL_SD_SALE_RETURN_HEAD left join TSPL_SD_SHIPMENT_HEAD on " _
                & " TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No  left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on " _
                & " TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code     inner join TSPL_Customer_Invoice_Head on  " _
                & " TSPL_Customer_Invoice_Head.against_Mcc_Material_Sale_return=TSPL_SD_SALE_RETURN_HEAD.Document_Code  and coalesce(TSPL_Customer_Invoice_Head.against_Mcc_Material_Sale_return,'')<>''  " _
                & " left outer join TSPL_VENDOR_MASTER   on  TSPL_VENDOR_MASTER .Vendor_code  =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code   left outer join " _
                & " TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No   where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC' " _
                & " and " & IIf(ChkSkipMccSaleReturn.Checked, " convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and " _
                & " '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= " _
                & " '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") & " and TSPL_Customer_Invoice_Head.Loc_Code  ='" & fndLoc.Value & "'  and " _
                & " tspl_customer_invoice_head.Balance_Amt<>0 "
            Dim whrCls As String = ""


            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and TSPL_SD_SALE_RETURN_HEAD.Customer_code  in (  " & strVendorCode & " )"
            End If
            qry = qry & whrCls

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    GvMccSaleReturn.Rows.AddNew()
                    GvMccSaleReturn.Rows(i).Cells(colSlno).Value = (i + 1)
                    GvMccSaleReturn.Rows(i).Cells(colSelect).Value = True
                    GvMccSaleReturn.Rows(i).Cells(colReturnDocNo).Value = dt.Rows(i)("Document_Code")
                    GvMccSaleReturn.Rows(i).Cells(colReturnDocDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Document_Date"), "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colReturnDocType).Value = dt.Rows(i)("Document_Type")
                    GvMccSaleReturn.Rows(i).Cells(colShipmentNo).Value = dt.Rows(i)("Shipment_No")
                    GvMccSaleReturn.Rows(i).Cells(colShipmentDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Shipment_Date"), "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colSaleInvNo).Value = dt.Rows(i)("Sale_Invoice_No")
                    GvMccSaleReturn.Rows(i).Cells(colSaleInvDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Sale_Inoivce_Date"), "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colARInvoiceNo).Value = dt.Rows(i)("AR_Invoice_No")
                    GvMccSaleReturn.Rows(i).Cells(colARInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("AR_Invoice_Date"), "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colCustomerCode).Value = dt.Rows(i)("Vendor_Code")
                    GvMccSaleReturn.Rows(i).Cells(colCustomerName).Value = dt.Rows(i)("Vendor_Name")
                    GvMccSaleReturn.Rows(i).Cells(colItemAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Balance_Amt")))
                    'gvMccsaleReturn.Rows(i).Cells(colReduceDeduc).Value = 0
                Next
            End If
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        GvMccSaleReturn.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To GvMccSaleReturn.Columns.Count - 1
            If TypeOf (GvMccSaleReturn.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(GvMccSaleReturn.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        GvMccSaleReturn.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Sub LoadMccSaleReturnGridDataFarmer()
        LoadBlankGridMccSaleReturnFarmer()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "select Document_Code,max(Document_Type) as Document_Type,max(Document_Date) as Document_Date,max([Shipment_No]) as [Shipment_No]  ,max([Shipment_Date]) as [Shipment_Date] ,max([Vendor_Code]) as [Vendor_Code] , max([Vendor_Name]) as [Vendor_Name],max(Farmer_Code) as Farmer_Code,max(Farmer_Name) AS Farmer_Name,  max(Sale_Invoice_No) as Sale_Invoice_No,max([Sale_Inoivce_Date]) as [Sale_Inoivce_Date],sum(Total_Amt*RI) as Balance_Amt from (" + Environment.NewLine +
                 "select TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code,case when coalesce(TSPL_MCC_SALE_RETURN_HEAD_FARMER.Return_Type,'')='D' then 'Damaged Goods' " &
                    " when coalesce(TSPL_MCC_SALE_RETURN_HEAD_FARMER.Return_Type,'')='P' then 'Price Only' " &
                    " when coalesce(TSPL_MCC_SALE_RETURN_HEAD_FARMER.Return_Type,'')='I' " &
                    " then 'Inventory Type' end as Document_Type,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Date,TSPL_MCC_Sale_Farmer_Head.DOCUMENT_CODE as [Shipment_No] " &
                    " ,TSPL_MCC_Sale_Farmer_Head.Document_Date as [Shipment_Date] ,VLC.VSP_Code [Vendor_Code] ," &
                    " VSP.Vendor_Name as [Vendor_Name],TSPL_MCC_SALE_RETURN_HEAD_FARMER.Farmer_Code,MP.MP_Name AS Farmer_Name, " &
                    " TSPL_MCC_Sale_Farmer_Head.Sale_Invoice_No,TSPL_MCC_Sale_Farmer_Head.Document_Date as [Sale_Inoivce_Date],TSPL_MCC_SALE_RETURN_HEAD_FARMER.Total_Amt,1 as RI,1 as Chk " &
                    " from TSPL_MCC_SALE_RETURN_HEAD_FARMER " &
                    " left join TSPL_MCC_Sale_Farmer_Head on TSPL_MCC_Sale_Farmer_Head.Sale_Invoice_No = TSPL_MCC_SALE_RETURN_HEAD_FARMER.Against_Invoice_No " &
                    " left join TSPL_MP_MASTER MP on MP.MP_Code= TSPL_MCC_SALE_RETURN_HEAD_FARMER.Farmer_Code " &
                    " LEFT JOIN TSPL_VLC_MASTER_HEAD VLC ON VLC.VLC_Code=MP.VLC_Code " &
                    " left join TSPL_VENDOR_MASTER VSP   on  VSP.Vendor_code=VLC.VSP_Code " &
                    " where TSPL_MCC_SALE_RETURN_HEAD_FARMER.Trans_Type='MCC' " &
                    " and " & IIf(chkSkipPrevMccSale.Checked, " convert(date,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") & " and TSPL_MCC_SALE_RETURN_HEAD_FARMER.Bill_To_Location  in ( select Location_Code from TSPL_LOCATION_MASTER where Loc_Segment_Code=  '" & fndLoc.Value & "' ) "
            If clsCommon.myLen(strVendorCode) > 0 Then
                qry += " and VLC.VSP_CODE  in (  " & strVendorCode & " )"
            End If
            qry += "union all" + Environment.NewLine +
            " select  Return_Doc_No as Document_Code,null as Document_Type,null as Document_Date,null as [Shipment_No]  ,null as [Shipment_Date] ,null as [Vendor_Code] , null as [Vendor_Name],null as Farmer_Code,null AS Farmer_Name,  null as Sale_Invoice_No,null as [Sale_Inoivce_Date],TSPL_MP_PAY_PROCESS_MCC_SALE_RETURN.Amount as Total_Amt,-1 as RI,0 as Chk" + Environment.NewLine +
            " from TSPL_MP_PAY_PROCESS_MCC_SALE_RETURN where Doc_No not in ('" + fndDocNo.Value + "')" + Environment.NewLine +
            " )xx group by Document_Code having sum(chk)>0 and sum(Total_Amt*RI)>0"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvMccSaleReturnFarmer.Rows.AddNew()
                    gvMccSaleReturnFarmer.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvMccSaleReturnFarmer.Rows(i).Cells(colSelect).Value = True
                    gvMccSaleReturnFarmer.Rows(i).Cells(colReturnDocNo).Value = dt.Rows(i)("Document_Code")
                    gvMccSaleReturnFarmer.Rows(i).Cells(colReturnDocDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Document_Date"), "dd/MMM/yyyy")
                    gvMccSaleReturnFarmer.Rows(i).Cells(colReturnDocType).Value = dt.Rows(i)("Document_Type")
                    gvMccSaleReturnFarmer.Rows(i).Cells(colShipmentNo).Value = dt.Rows(i)("Shipment_No")
                    gvMccSaleReturnFarmer.Rows(i).Cells(colShipmentDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Shipment_Date"), "dd/MMM/yyyy")
                    gvMccSaleReturnFarmer.Rows(i).Cells(colSaleInvNo).Value = dt.Rows(i)("Sale_Invoice_No")
                    gvMccSaleReturnFarmer.Rows(i).Cells(colSaleInvDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Sale_Inoivce_Date"), "dd/MMM/yyyy")
                    gvMccSaleReturnFarmer.Rows(i).Cells(colFarmerCode).Value = clsCommon.myCstr(dt.Rows(i)("Farmer_Code"))
                    gvMccSaleReturnFarmer.Rows(i).Cells(colFarmerName).Value = clsCommon.myCstr(dt.Rows(i)("Farmer_Name"))
                    gvMccSaleReturnFarmer.Rows(i).Cells(colVendorCode).Value = clsCommon.myCstr(dt.Rows(i)("Vendor_Code"))
                    gvMccSaleReturnFarmer.Rows(i).Cells(colVendorDesc).Value = clsCommon.myCstr(dt.Rows(i)("Vendor_Name"))
                    gvMccSaleReturnFarmer.Rows(i).Cells(colItemAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Balance_Amt")))

                Next
            End If
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gvMccSaleReturnFarmer.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To gvMccSaleReturnFarmer.Columns.Count - 1
            If TypeOf (gvMccSaleReturnFarmer.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gvMccSaleReturnFarmer.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvMccSaleReturnFarmer.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Sub LoadAdjustmentGridDataFarmer()
        'GvMccSaleReturn.Rows.Clear()
        LoadBlankGridFarmerAdjustment()
        If clsCommon.myLen(strVendorCode) > 0 Then
            ''change by Panch Raj on 01-may-2018 against ticket : KDI/30/04/18-000281
            Dim qry As String = ""
            qry = "select Adjustment_No,Description,Remarks,Adjustment_Type,Adjustment_Date,Farmer_Code,Farmer_Name,VSP.Vendor_Code,VSP.Vendor_Name,Adjustment_Amount from TSPL_MP_PAY_ADJ_HEAD " &
                    " left join TSPL_MP_MASTER MP on MP.MP_Code= TSPL_MP_PAY_ADJ_HEAD.Farmer_Code " &
                    " LEFT JOIN TSPL_VLC_MASTER_HEAD VLC ON VLC.VLC_Code=MP.VLC_Code " &
                    " left join TSPL_VENDOR_MASTER VSP   on  VSP.Vendor_code=VLC.VSP_Code " &
                    " where TSPL_MP_PAY_ADJ_HEAD.Doc_No is null and not exists (select 1 from TSPL_PAYMENT_PROCESS_ADJ_DETAIL where TSPL_PAYMENT_PROCESS_ADJ_DETAIL.Doc_No<>'" & fndDocNo.Value & "' and TSPL_MP_PAY_ADJ_HEAD.Adjustment_No=TSPL_PAYMENT_PROCESS_ADJ_DETAIL.Adjustment_No)   " &
                    " and " & IIf(chkPrevMPAdj.Checked, " convert(date,TSPL_MP_PAY_ADJ_HEAD.Adjustment_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_MP_PAY_ADJ_HEAD.Adjustment_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") & " "
            Dim whrCls As String = ""


            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and VLC.VSP_CODE  in (  " & strVendorCode & " )"
            End If
            qry = qry & whrCls

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvMPAdj.Rows.AddNew()
                    gvMPAdj.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvMPAdj.Rows(i).Cells(colSelect).Value = True
                    gvMPAdj.Rows(i).Cells(colAdjNo).Value = clsCommon.myCstr(dt.Rows(i)("Adjustment_No"))
                    gvMPAdj.Rows(i).Cells(colMPAdjDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Adjustment_Date"), "dd/MMM/yyyy")
                    gvMPAdj.Rows(i).Cells(colMPAdjType).Value = clsCommon.myCstr(dt.Rows(i)("Adjustment_Type"))
                    gvMPAdj.Rows(i).Cells(colMPADjDesc).Value = clsCommon.myCstr(dt.Rows(i)("Description"))
                    gvMPAdj.Rows(i).Cells(colMPAdjRemarks).Value = clsCommon.myCstr(dt.Rows(i)("Remarks"))

                    gvMPAdj.Rows(i).Cells(colFarmerCode).Value = clsCommon.myCstr(dt.Rows(i)("Farmer_Code"))
                    gvMPAdj.Rows(i).Cells(colFarmerName).Value = clsCommon.myCstr(dt.Rows(i)("Farmer_Name"))
                    gvMPAdj.Rows(i).Cells(colVendorCode).Value = clsCommon.myCstr(dt.Rows(i)("Vendor_Code"))
                    gvMPAdj.Rows(i).Cells(colVendorDesc).Value = clsCommon.myCstr(dt.Rows(i)("Vendor_Name"))
                    gvMPAdj.Rows(i).Cells(colMPAdjustAmount).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Adjustment_Amount")))

                Next
            End If
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gvMPAdj.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To gvMPAdj.Columns.Count - 1
            If TypeOf (gvMPAdj.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gvMPAdj.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvMPAdj.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Sub LoadBlankGridItemIssue()
        gvItemIssue.Rows.Clear()
        gvItemIssue.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select"
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = True
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItemIssue.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Item Issue Doc No"
        colTextBox.Name = colVspItemIssueNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Item Issue Doc Date"
        colTextBox.Name = colVspItemIssueDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colTextBox)




        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Item Code"
        'colTextBox.Name = colItemCode
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvItemIssue.MasterTemplate.Columns.Add(colTextBox)
        'gvItemIssue.BestFitColumns(BestFitColumnMode.AllCells)


        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Item Desc"
        'colTextBox.Name = colItemDesc
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvItemIssue.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Reduce " & Environment.NewLine & " Deduction"
        colDecimal.Name = colReduceDeduc
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = False
        colDecimal.WrapText = True
        colDecimal.ShowUpDownButtons = False
        colDecimal.Minimum = 0
        gvItemIssue.MasterTemplate.Columns.Add(colDecimal)

        gvItemIssue.AllowAddNewRow = False
        gvItemIssue.AllowDeleteRow = False
        gvItemIssue.ShowGroupPanel = False
        gvItemIssue.AllowColumnReorder = True
        gvItemIssue.AllowRowReorder = False
        gvItemIssue.EnableSorting = True
        gvItemIssue.EnableFiltering = True
        gvItemIssue.TableElement.TableHeaderHeight = 40
    End Sub

    Sub LoadBlankGridItemIssueReturn()
        gvItemIssueReturn.Rows.Clear()
        gvItemIssueReturn.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select"
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItemIssueReturn.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Item Issue Return No"
        colTextBox.Name = colVspItemIssueReturnNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Item Issue Doc No"
        colTextBox.Name = colVspItemIssueNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Item Issue Return Date"
        colTextBox.Name = colVspItemIssueDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colDecimal)

        'colDecimal = New GridViewDecimalColumn()
        'colDecimal.FormatString = ""
        'colDecimal.HeaderText = "Reduce " & Environment.NewLine & " Deduction"
        'colDecimal.Name = colReduceDeduc
        'colDecimal.Width = 100
        'colDecimal.ReadOnly = False
        'colDecimal.WrapText = True
        'colDecimal.ShowUpDownButtons = False
        'colDecimal.Minimum = 0
        'gvItemIssueReturn.MasterTemplate.Columns.Add(colDecimal)

        gvItemIssueReturn.AllowAddNewRow = False
        gvItemIssueReturn.AllowDeleteRow = False
        gvItemIssueReturn.ShowGroupPanel = False
        gvItemIssueReturn.AllowColumnReorder = True
        gvItemIssueReturn.AllowRowReorder = False
        gvItemIssueReturn.EnableSorting = True
        gvItemIssueReturn.EnableFiltering = True
        gvItemIssueReturn.TableElement.TableHeaderHeight = 40
    End Sub

    Sub LoadItemIssueGridData()
        'gvItemIssue.Rows.Clear()
        LoadBlankGridItemIssue()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "   select TSPL_VSPItem_HEAD.Doc_No  as [Item_Issue_Doc_No],TSPL_VSPItem_HEAD.Doc_Date  as [Item_Issue_Doc_Date] ,TSPL_VSPItem_HEAD.From_Location , " &
                " TSPL_VSPItem_HEAD.Issue_To as [vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_INVOICE_HEAD.Document_No as [AP_Invoice_No] , " &
                " TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  as [AP_Invoice_Date],TSPL_VENDOR_INVOICE_HEAD.Balance_Amt   from TSPL_VENDOR_INVOICE_HEAD  " &
                " inner join TSPL_VSPItem_HEAD on TSPL_VSPItem_HEAD .Doc_No=TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No    " &
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSPItem_HEAD.Issue_To  " &
                " where TSPL_VSPItem_HEAD.Doc_Type='Issue' and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 and " & IIf(chkSkipPrevItemIssue.Checked, " convert(date,TSPL_VSPItem_HEAD.Doc_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_VSPItem_HEAD.Doc_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") & " " &
                " and TSPL_VSPItem_HEAD.From_Location in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code   ='" & fndLoc.Value & "')  "
            Dim whrCls As String = ""
            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and TSPL_VSPItem_HEAD.Issue_To  in ( " & strVendorCode & " )"
            End If
            qry = qry & whrCls
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvItemIssue.Rows.AddNew()
                    gvItemIssue.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvItemIssue.Rows(i).Cells(colSelect).Value = True
                    gvItemIssue.Rows(i).Cells(colVspItemIssueNo).Value = dt.Rows(i)("Item_Issue_Doc_No")
                    gvItemIssue.Rows(i).Cells(colVspItemIssueDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Item_Issue_Doc_Date"), "dd/MMM/yyyy")
                    gvItemIssue.Rows(i).Cells(colAPInvoiceNo).Value = dt.Rows(i)("AP_Invoice_No")
                    gvItemIssue.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("AP_Invoice_Date"), "dd/MMM/yyyy")
                    gvItemIssue.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("vendor_Code")
                    gvItemIssue.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("Vendor_Name")
                    gvItemIssue.Rows(i).Cells(colItemAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Balance_Amt")))
                    gvItemIssue.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myFormat(0)
                Next
            End If
        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gvItemIssue.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To gvItemIssue.Columns.Count - 1
            If TypeOf (gvItemIssue.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gvItemIssue.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvItemIssue.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Sub LoadItemIssueReturnGridData()
        'gvItemIssue.Rows.Clear()
        LoadBlankGridItemIssueReturn()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "   select TSPL_VSPItem_HEAD.Doc_No  as [Item_Issue_Return_No],Issue_No as [Item_Issue_Doc_No],TSPL_VSPItem_HEAD.Doc_Date  as [Item_Issue_Return_Date] , " &
                " TSPL_VSPItem_HEAD.From_Location ,TSPL_VSPItem_HEAD.Issue_To as [vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_INVOICE_HEAD.Document_No as [AP_Invoice_No] , " &
                " TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  as [AP_Invoice_Date],TSPL_VENDOR_INVOICE_HEAD.Balance_Amt   from TSPL_VENDOR_INVOICE_HEAD  " &
                " inner join TSPL_VSPItem_HEAD on TSPL_VSPItem_HEAD .Doc_No=TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No    " &
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSPItem_HEAD.Issue_To  " &
                " where TSPL_VSPItem_HEAD.Doc_Type='Return' and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 and " & IIf(chkSkipPrevItemIssueReturn.Checked, " convert(date,TSPL_VSPItem_HEAD.Doc_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_VSPItem_HEAD.Doc_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") & " " &
                " and TSPL_VSPItem_HEAD.From_Location in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code   ='" & fndLoc.Value & "')  "
            Dim whrCls As String = ""
            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and TSPL_VSPItem_HEAD.Issue_To  in ( " & strVendorCode & " )"
            End If
            qry = qry & whrCls
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvItemIssueReturn.Rows.AddNew()
                    gvItemIssueReturn.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvItemIssueReturn.Rows(i).Cells(colSelect).Value = True
                    gvItemIssueReturn.Rows(i).Cells(colVspItemIssueReturnNo).Value = clsCommon.myCstr(dt.Rows(i)("Item_Issue_Return_No"))
                    gvItemIssueReturn.Rows(i).Cells(colVspItemIssueNo).Value = clsCommon.myCstr(dt.Rows(i)("Item_Issue_Doc_No"))
                    gvItemIssueReturn.Rows(i).Cells(colVspItemIssueDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Item_Issue_Return_Date"), "dd/MMM/yyyy")
                    gvItemIssueReturn.Rows(i).Cells(colAPInvoiceNo).Value = dt.Rows(i)("AP_Invoice_No")
                    gvItemIssueReturn.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("AP_Invoice_Date"), "dd/MMM/yyyy")
                    gvItemIssueReturn.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("vendor_Code")
                    gvItemIssueReturn.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("Vendor_Name")
                    gvItemIssueReturn.Rows(i).Cells(colItemAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Balance_Amt")))
                    'gvItemIssueReturn.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myFormat(0)
                Next
            End If
        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gvItemIssueReturn.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To gvItemIssueReturn.Columns.Count - 1
            If TypeOf (gvItemIssueReturn.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gvItemIssueReturn.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvItemIssueReturn.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub Reset()
        isLoad = True
        isNewEntry = True
        Dim dt As Date = clsCommon.GETSERVERDATE()
        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        dtpDate.Value = dt
        fndDocNo.Value = ""
        txtNEFTUploaderREFNo.Text = ""
        fndDocNo.MyReadOnly = False
        arrStrIssueItemCode = Nothing
        arrStrIssueItemDesc = Nothing
        arrStrMccSaleItemCode = Nothing
        arrStrMccSaleItemDesc = Nothing
        arrStrDedCode = Nothing
        arrStrDedDesc = Nothing
        strVendorCode = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        fndLoc.Value = ""
        txtLocName.Text = ""
        btnProcess.Enabled = False
        btnDelete.Enabled = False
        lblPending.Status = ERPTransactionStatus.Pending
        'btnSave.Visible = False
        'btnProcess.Enabled = True
        'btnDelete.Visible = False

        LoadBlankGridInvoice()
        LoadBlankGridItemIssue()
        LoadBlankGridItemIssueReturn()
        LoadBlankGridMccSale()
        LoadBlankGridMccSaleFarmer()
        LoadBlankGridMccSaleReturn()
        LoadBlankGridMccSaleReturnFarmer()
        LoadBlankGridFarmerAdjustment()
        LoadBlankGridDeduction()
        LoadBlankGridCreditNote()
        LoadBlankGridAdvancePayment()
        LoadBlankGridFAAdvancePayment()
        LoadBlankGridGV()
        frm.desc = "Against Bulk Payment Process. "
        fndLoc.Enabled = True
        txtLocName.Enabled = True
        dtpToDate.Enabled = True
        dtpToDate.ReadOnly = True
        dtpFromDate.Enabled = True
        isLoad = False
        GroupBox2.Visible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowSkippingPrevDocumentsOnPaymentProcess, Nothing)) = 1, True, False)
    End Sub

    Private Sub FrmProvisionEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose.PerformClick()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmPaymentProcessFarmer)
        If Not (MyBase.isReadFlag) Then
            'If MDI.blnShowAllMenu = False Then
            Throw New Exception("Permission Denied")
            'Else
            '    Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")

            'End If
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Function AllowToSave() As Boolean
        If dtpFromDate.Value > dtpToDate.Value Then
            Throw New Exception(" 'From Date' can't be larger than 'To Date'")
        End If
        If clsCommon.GetDateWithEndTime(dtpDate.Value) < clsCommon.GetDateWithStartTime(dtpToDate.Value) Then
            Throw New Exception("Payment process date can't be less than to date")
        End If
        If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
            Throw New Exception(" Please select atleast one Vendor")
        End If

        If clsCommon.myLen(fndLoc.Value) <= 0 Then
            Throw New Exception("Please select Location segment")
            fndLoc.Focus()
        End If

        If gv.Rows.Count <= 0 OrElse gv Is Nothing Then
            Throw New Exception("Please select atleast one document")
        End If
        '' done by Panch Raj against ticket no:BM00000008937
        '' unselect mcc sale trans for unseleceted vendor 
        Dim IsInvalidVendor As Boolean

        For Each grow As GridViewRowInfo In gvMccSale.Rows
            IsInvalidVendor = True
            If grow.Cells(colSelect).Value = True Then
                For i As Integer = 0 To gv.Rows.Count - 1
                    If gv.Rows(i).Cells(colSelect).Value = True Then
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
                            IsInvalidVendor = False
                        End If
                    End If
                Next
                If IsInvalidVendor = True Then
                    Throw New Exception("MCC Sale : Invalid Vendor No-" & grow.Cells(colCustomerCode).Value & " at Line No-" & (grow.Index + 1) & ". Please unselect it")
                End If
            End If
        Next

        '' unselect mcc sale return trans for unseleceted vendor 
        For Each grow As GridViewRowInfo In GvMccSaleReturn.Rows
            IsInvalidVendor = True
            If grow.Cells(colSelect).Value = True Then
                For i As Integer = 0 To gv.Rows.Count - 1
                    If gv.Rows(i).Cells(colSelect).Value = True Then
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
                            IsInvalidVendor = False
                        End If
                    End If
                Next
                If IsInvalidVendor = True Then
                    Throw New Exception("MCC Sale Return : Invalid Vendor No-" & grow.Cells(colCustomerCode).Value & " at Line No-" & (grow.Index + 1) & ". Please unselect it")
                End If
            End If
        Next

        '' unselect mcc Item Issue trans for unseleceted vendor 
        For Each grow As GridViewRowInfo In gvItemIssue.Rows
            IsInvalidVendor = True
            If grow.Cells(colSelect).Value = True Then
                For i As Integer = 0 To gv.Rows.Count - 1
                    If gv.Rows(i).Cells(colSelect).Value = True Then
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            IsInvalidVendor = False
                        End If
                    End If
                Next
                If IsInvalidVendor = True Then
                    Throw New Exception("Item Issue : Invalid Vendor No-" & grow.Cells(colVendorCode).Value & " at Line No-" & (grow.Index + 1) & ". Please unselect it")
                End If
            End If
        Next

        '' unselect mcc Item Issue trans for unseleceted vendor 
        For Each grow As GridViewRowInfo In gvItemIssueReturn.Rows
            IsInvalidVendor = True
            If grow.Cells(colSelect).Value = True Then
                For i As Integer = 0 To gv.Rows.Count - 1
                    If gv.Rows(i).Cells(colSelect).Value = True Then
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            IsInvalidVendor = False
                        End If
                    End If
                Next
                If IsInvalidVendor = True Then
                    Throw New Exception("Item Issue Return : Invalid Vendor No-" & grow.Cells(colVendorCode).Value & " at Line No-" & (grow.Index + 1) & ". Please unselect it")
                End If
            End If
        Next

        '' unselect mcc Deduction trans for unseleceted vendor 
        For Each grow As GridViewRowInfo In gvDeduction.Rows
            IsInvalidVendor = True
            If grow.Cells(colSelect).Value = True Then
                For i As Integer = 0 To gv.Rows.Count - 1
                    If gv.Rows(i).Cells(colSelect).Value = True Then
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            IsInvalidVendor = False
                        End If
                    End If
                Next
                If IsInvalidVendor = True Then
                    Throw New Exception("Deduction : Invalid Vendor No-" & grow.Cells(colVendorCode).Value & " at Line No-" & (grow.Index + 1) & ". Please unselect it")
                End If
            End If
        Next
        '' unselect mcc Credit trans for unseleceted vendor 
        For Each grow As GridViewRowInfo In gvCreditNote.Rows
            IsInvalidVendor = True
            If grow.Cells(colSelect).Value = True Then
                For i As Integer = 0 To gv.Rows.Count - 1
                    If gv.Rows(i).Cells(colSelect).Value = True Then
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            IsInvalidVendor = False
                        End If
                    End If
                Next
                If IsInvalidVendor = True Then
                    Throw New Exception("Credit Note : Invalid Vendor No-" & grow.Cells(colVendorCode).Value & " at Line No-" & (grow.Index + 1) & ". Please unselect it")
                End If
            End If
        Next


        ''By Balwinder on 27/04/2020 for SPMMD Problem Take time while update.
        gvPaymentToFarmer.FilterDescriptors.Clear()
        Application.DoEvents()
        gvPaymentToFarmer.CurrentColumn = gvPaymentToFarmer.Columns(colPaybleAmt)
        For ii As Integer = 0 To gvPaymentToFarmer.Rows.Count - 1
            CalculatePayableAmt(ii)
        Next



        'Try
        '    gvPaymentToFarmer.FilterDescriptors.Clear()
        '    gvPaymentToFarmer.Refresh()
        '    clsCommon.ProgressBarPercentShow()
        '    gvPaymentToFarmer.CurrentColumn = gvPaymentToFarmer.Columns(colPaybleAmt)
        '    For ii As Integer = 0 To gvPaymentToFarmer.Rows.Count - 1
        '        clsCommon.ProgressBarPercentUpdate(ii * 100 / gvPaymentToFarmer.Rows.Count, "Update Row" & (ii + 1) & " Of " & gvPaymentToFarmer.Rows.Count)
        '        CalculatePayableAmt(ii)
        '    Next
        '    clsCommon.ProgressBarPercentHide()
        'Catch ex As Exception
        '    clsCommon.ProgressBarPercentHide()
        'End Try

        'gvPaymentToFarmer.FilterDescriptors.Clear()
        'gvPaymentToFarmer.Refresh()
        'Try
        '    isLoad = True
        '    clsCommon.ProgressBarPercentShow()
        '    gvPaymentToFarmer.CurrentColumn = gvPaymentToFarmer.Columns(colPaybleAmt)
        '    For ii As Integer = 0 To gvPaymentToFarmer.Rows.Count - 1
        '        clsCommon.ProgressBarPercentUpdate(ii * 100 / gvPaymentToFarmer.Rows.Count, " Creating Payment Entry  Record " & (ii + 1) & " Of " & gvPaymentToFarmer.Rows.Count)
        '        CalculatePayableAmt(ii)
        '    Next
        '    clsCommon.ProgressBarPercentHide()
        'Catch ex As Exception
        '    clsCommon.ProgressBarPercentHide()
        'Finally
        '    isLoad = False
        'End Try


        'For ii As Integer = 0 To gvPaymentToFarmer.Rows.Count - 1
        '    gvPaymentToFarmer.CurrentColumn = gvPaymentToFarmer.Columns(colPaybleAmt)
        '    CalculatePayableAmt(ii)
        'Next
        'Throw New Exception("Balwinder")
        For i As Integer = 0 To gv.Rows.Count - 1
            If gv.Rows(i).Cells(colSelect).Value = True Then
                gvSetVSPDedAmount("", i)
                ''Balwinder Comment on 25/05/2020 for   = 
                'If clsCommon.myCdbl(gv.Rows(i).Cells(colNextCycleDebitNote).Value) < 0 Then
                '    Throw New Exception(gv.Columns(colNextCycleDebitNote).HeaderText + "  Amount cannot be negative At line no " & (i + 1))
                'End If
                gv.Rows(i).Cells(colPaybleAmt).Value = Math.Round(clsCommon.myCdbl(gv.Rows(i).Cells(colPaybleAmt).Value), 2)
                If clsCommon.myCdbl(gv.Rows(i).Cells(colPaybleAmt).Value) < 0 Then
                    Throw New Exception("Payable Amount cannot be in negative At line no " & (i + 1))
                End If

                If clsCommon.myCdbl(gv.Rows(i).Cells(colPaybleAmt).Value) = 0 Then
                    If Not (isConsiderAdvancePayment OrElse PayableAmountZeroForMCCSale) Then
                        Throw New Exception("Payable Amount cannot be zero At line no " & (i + 1))
                    End If
                End If
            End If
        Next
        Return True
    End Function

    Sub deleteData()
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Want To Delete The Doc No : " & fndDocNo.Value & " ?", "Confirm", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    If clsPaymentProcessFarmerHead.deleteData(fndDocNo.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Deleted successFully", Me.Text)
                        Reset()
                    End If
                End If
            Else
                Throw New Exception("Doc No not Found to delete")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deleteData()
    End Sub

    Sub SaveData(Optional ByVal isPostbtnClick As Boolean = False)
        Try
            If chkSelected.Checked Then
                AllowToSave()
                Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    For i = 0 To gv.Rows.Count - 1
                        Dim strTRNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + fndDocNo.Value + "' and Milk_Purchase_Invoice_No='" + clsCommon.myCstr(gv.Rows(i).Cells(colPurchaseInvoiceNo).Value) + "' ", tran))
                        If clsCommon.myLen(strTRNo) <= 0 Then
                            Throw New Exception("TR No Not found for " + clsCommon.myCstr(gv.Rows(i).Cells(colPurchaseInvoiceNo).Value) + "]")
                        End If
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Payable_Amount", clsCommon.myCdbl(gv.Rows(i).Cells(colPaybleAmt).Value))
                        clsCommon.AddColumnsForChange(coll, "NextCycleDebitNote", clsCommon.myCdbl(gv.Rows(i).Cells(colNextCycleDebitNote).Value))
                        clsCommon.AddColumnsForChange(coll, "VSP_Excess_Amount", clsCommon.myCdbl(gv.Rows(i).Cells(colgvVSPExcessAmount).Value))
                        clsCommon.AddColumnsForChange(coll, "PrevCycleDebitNote", clsCommon.myCdbl(gv.Rows(i).Cells(colPrevCycleBalance).Value))
                        clsCommon.AddColumnsForChange(coll, "Deduction_Amount", clsCommon.myCdbl(gv.Rows(i).Cells(colDeductionTotalAmount).Value))
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_PROCESS_DETAIL", OMInsertOrUpdate.Update, "PP_Detail_No='" + strTRNo + "'", tran)
                    Next
                    tran.Commit()
                    If Not isPostbtnClick Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                Catch ex As Exception
                    tran.Rollback()
                    Throw New Exception(ex.Message)
                End Try
            Else
                Dim i As Integer = 0
                AllowToSave()
                If isPostbtnClick Then
                    For i = 0 To gv.Rows.Count - 1
                        If clsCommon.myLen(gv.Rows(i).Cells(colBankCode).Value) <= 0 Then
                            frm.IsFarmerPayment = True
                            frm.StartPosition = FormStartPosition.CenterScreen
                            frm.desc = "Against Payment Process "
                            frm.ShowDialog()
                            If frm.btnOkClicked Then
                                Exit For
                            Else
                                Exit Sub
                            End If
                        End If
                    Next
                    isCellValueChanged = True
                    For i = 0 To gvInvoice.Rows.Count - 1
                        If clsCommon.myLen(gvInvoice.Rows(i).Cells(colBankCode).Value) <= 0 AndAlso clsCommon.myLen(frm.bankCode) > 0 Then
                            gvInvoice.Rows(i).Cells(colBankCode).Value = frm.MainbankCode
                            gvInvoice.Rows(i).Cells(colBankDesc).Value = frm.MainbankDesc
                            gvInvoice.Rows(i).Cells(colPayMode).Value = frm.paymentMode
                            gvInvoice.Rows(i).Cells(colChequeNo).Value = ""
                        End If
                    Next
                    For i = 0 To gv.Rows.Count - 1
                        If clsCommon.myLen(gv.Rows(i).Cells(colBankCode).Value) <= 0 AndAlso clsCommon.myLen(frm.bankCode) > 0 Then
                            gv.Rows(i).Cells(colBankCode).Value = frm.MainbankCode
                            gv.Rows(i).Cells(colBankDesc).Value = frm.MainbankDesc
                            gv.Rows(i).Cells(colPayMode).Value = frm.paymentMode
                            If clsCommon.myCBool(gv.Rows(i).Cells(colSelect).Value) = True Then
                                If clsCommon.CompairString(gv.Rows(i).Cells(colPayMode).Value, "Cheque") = CompairStringResult.Equal Then
                                    gv.Rows(i).Cells(colChequeNo).ReadOnly = False
                                    gv.Rows(i).Cells(colChequeDate).ReadOnly = False

                                    For j As Integer = 0 To gv.Rows.Count - 1
                                        gv.Rows(j).Cells(colBankCode).Value = frm.MainbankCode
                                        gv.Rows(j).Cells(colBankDesc).Value = frm.MainbankDesc
                                        gv.Rows(j).Cells(colPayMode).Value = frm.paymentMode
                                    Next
                                    If clsCommon.myLen(gv.Rows(i).Cells(colChequeNo).Value) <= 0 Then
                                        Throw New Exception("Please Enter Cheque No at line no: " & i + 1 & ". Cheque No is mandatory in case of Payment Mode:Cheque.")
                                    End If
                                    If clsCommon.myLen(gv.Rows(i).Cells(colChequeDate).Value) <= 0 Then
                                        Throw New Exception("Please Enter Cheque Date at line no: " & i + 1 & ". Cheque Date is mandatory in case of Payment Mode:Cheque.")
                                    End If
                                Else
                                    gv.Rows(i).Cells(colChequeNo).ReadOnly = True
                                    gv.Rows(i).Cells(colChequeNo).Value = ""

                                    gv.Rows(i).Cells(colChequeDate).ReadOnly = True
                                    gv.Rows(i).Cells(colChequeDate).Value = Nothing
                                End If
                            End If

                        Else
                            If clsCommon.myCBool(gv.Rows(i).Cells(colSelect).Value) = True Then
                                gv.Rows(i).Cells(colBankCode).Value = frm.MainbankCode
                                gv.Rows(i).Cells(colBankDesc).Value = frm.MainbankDesc
                                gv.Rows(i).Cells(colPayMode).Value = frm.paymentMode
                                If clsCommon.CompairString(gv.Rows(i).Cells(colPayMode).Value, "Cheque") = CompairStringResult.Equal Then
                                    gv.Rows(i).Cells(colChequeNo).ReadOnly = False
                                    gv.Rows(i).Cells(colChequeDate).ReadOnly = False
                                    For j As Integer = 0 To gv.Rows.Count - 1
                                        gv.Rows(j).Cells(colBankCode).Value = frm.MainbankCode
                                        gv.Rows(j).Cells(colBankDesc).Value = frm.MainbankDesc
                                        gv.Rows(j).Cells(colPayMode).Value = frm.paymentMode
                                    Next
                                    If clsCommon.myLen(gv.Rows(i).Cells(colChequeNo).Value) <= 0 Then
                                        Throw New Exception("Please Enter Cheque No at line no: " & i + 1 & ". Cheque No is mandatory in case of Payment Mode:Cheque.")
                                    End If
                                    If clsCommon.myLen(gv.Rows(i).Cells(colChequeDate).Value) <= 0 Then
                                        Throw New Exception("Please Enter Cheque Date at line no: " & i + 1 & ". Cheque Date is mandatory in case of Payment Mode:Cheque.")
                                    End If
                                Else
                                    gv.Rows(i).Cells(colChequeNo).ReadOnly = True
                                    gv.Rows(i).Cells(colChequeNo).Value = ""

                                    gv.Rows(i).Cells(colChequeDate).ReadOnly = True
                                    gv.Rows(i).Cells(colChequeDate).Value = Nothing
                                End If
                            End If

                        End If
                    Next
                    For i = 0 To gvPaymentToFarmer.Rows.Count - 1
                        If clsCommon.myLen(gvPaymentToFarmer.Rows(i).Cells(colBankCode).Value) <= 0 AndAlso clsCommon.myLen(frm.bankCode) > 0 Then
                            gvPaymentToFarmer.Rows(i).Cells(colBankCode).Value = frm.bankCode
                            gvPaymentToFarmer.Rows(i).Cells(colBankDesc).Value = frm.bankDesc
                            gvPaymentToFarmer.Rows(i).Cells(colPayMode).Value = frm.paymentMode
                            If clsCommon.myCBool(gvPaymentToFarmer.Rows(i).Cells(colSelect).Value) = True Then
                                If clsCommon.CompairString(gvPaymentToFarmer.Rows(i).Cells(colPayMode).Value, "Cheque") = CompairStringResult.Equal Then
                                    gvPaymentToFarmer.Rows(i).Cells(colChequeNo).ReadOnly = False
                                    gvPaymentToFarmer.Rows(i).Cells(colChequeDate).ReadOnly = False

                                    For j As Integer = 0 To gvPaymentToFarmer.Rows.Count - 1
                                        gvPaymentToFarmer.Rows(j).Cells(colBankCode).Value = frm.bankCode
                                        gvPaymentToFarmer.Rows(j).Cells(colBankDesc).Value = frm.bankDesc
                                        gvPaymentToFarmer.Rows(j).Cells(colPayMode).Value = frm.paymentMode
                                    Next
                                    If clsCommon.myLen(gvPaymentToFarmer.Rows(i).Cells(colChequeNo).Value) <= 0 Then
                                        Throw New Exception("Tab Payment To Farmer :Please Enter Cheque No at line no: " & i + 1 & ". Cheque No is mandatory in case of Payment Mode:Cheque.")
                                    End If
                                    If clsCommon.myLen(gvPaymentToFarmer.Rows(i).Cells(colChequeDate).Value) <= 0 Then
                                        Throw New Exception("Tab Payment To Farmer :Please Enter Cheque Date at line no: " & i + 1 & ". Cheque Date is mandatory in case of Payment Mode:Cheque.")
                                    End If
                                Else
                                    gvPaymentToFarmer.Rows(i).Cells(colChequeNo).ReadOnly = True
                                    gvPaymentToFarmer.Rows(i).Cells(colChequeNo).Value = ""

                                    gvPaymentToFarmer.Rows(i).Cells(colChequeDate).ReadOnly = True
                                    gvPaymentToFarmer.Rows(i).Cells(colChequeDate).Value = Nothing
                                End If
                            End If

                        Else
                            If clsCommon.myCBool(gvPaymentToFarmer.Rows(i).Cells(colSelect).Value) = True Then
                                gvPaymentToFarmer.Rows(i).Cells(colBankCode).Value = frm.bankCode
                                gvPaymentToFarmer.Rows(i).Cells(colBankDesc).Value = frm.bankDesc
                                gvPaymentToFarmer.Rows(i).Cells(colPayMode).Value = frm.paymentMode
                                If clsCommon.CompairString(gvPaymentToFarmer.Rows(i).Cells(colPayMode).Value, "Cheque") = CompairStringResult.Equal Then
                                    gvPaymentToFarmer.Rows(i).Cells(colChequeNo).ReadOnly = False
                                    gvPaymentToFarmer.Rows(i).Cells(colChequeDate).ReadOnly = False
                                    For j As Integer = 0 To gvPaymentToFarmer.Rows.Count - 1
                                        gvPaymentToFarmer.Rows(j).Cells(colBankCode).Value = frm.bankCode
                                        gvPaymentToFarmer.Rows(j).Cells(colBankDesc).Value = frm.bankDesc
                                        gvPaymentToFarmer.Rows(j).Cells(colPayMode).Value = frm.paymentMode
                                    Next
                                    If clsCommon.myLen(gvPaymentToFarmer.Rows(i).Cells(colChequeNo).Value) <= 0 Then
                                        Throw New Exception("Tab Payment To Farmer :Please Enter Cheque No at line no: " & i + 1 & ". Cheque No is mandatory in case of Payment Mode:Cheque.")
                                    End If
                                    If clsCommon.myLen(gvPaymentToFarmer.Rows(i).Cells(colChequeDate).Value) <= 0 Then
                                        Throw New Exception("Tab Payment To Farmer :Please Enter Cheque Date at line no: " & i + 1 & ". Cheque Date is mandatory in case of Payment Mode:Cheque.")
                                    End If
                                Else
                                    gvPaymentToFarmer.Rows(i).Cells(colChequeNo).ReadOnly = True
                                    gvPaymentToFarmer.Rows(i).Cells(colChequeNo).Value = ""

                                    gvPaymentToFarmer.Rows(i).Cells(colChequeDate).ReadOnly = True
                                    gvPaymentToFarmer.Rows(i).Cells(colChequeDate).Value = Nothing
                                End If
                            End If

                        End If
                    Next
                    isCellValueChanged = False
                End If


                Dim obj As clsPaymentProcessFarmerHead = New clsPaymentProcessFarmerHead()
                obj.Doc_No = fndDocNo.Value
                obj.DocRefNoForUploader = clsCommon.myCstr(txtNEFTUploaderREFNo.Text)
                obj.Doc_Date = clsCommon.GetPrintDate(dtpDate.Value, "dd/MMM/yyyy")
                obj.From_Date = clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy")
                obj.To_Date = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy")
                obj.Loc_Seg_Code = clsCommon.myCstr(fndLoc.Value)
                ''richa agarwal 07-jan-2016
                If btnSave.Text = "Update" Then
                    frm.desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(paymentDesc,'') as paymentDesc from tspl_payment_process_head where doc_no='" & clsCommon.myCstr(fndDocNo.Value) & "'"))
                End If
                ''-------------------
                obj.PaymentDesc = frm.desc
                If gvInvoice IsNot Nothing AndAlso gvInvoice.Rows.Count > 0 Then
                    obj.arrclsPaymentProcessFarmerInvoices = New List(Of clsPaymentProcessFarmerInvoices)
                    Dim objPayProInv As clsPaymentProcessFarmerInvoices = Nothing
                    For i = 0 To gvInvoice.Rows.Count - 1
                        If gvInvoice.Rows(i).Cells(colSelect).Value = True Then
                            objPayProInv = New clsPaymentProcessFarmerInvoices
                            objPayProInv.SLNO = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colSlno).Value)
                            objPayProInv.Milk_Purchase_Invoice_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value)
                            objPayProInv.Milk_Purchase_Invoice_Date = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPurchaseInvoiceDate).Value)
                            objPayProInv.AP_Invoice_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value)
                            objPayProInv.AP_Invoice_Date = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colAPInvoiceDate).Value)
                            objPayProInv.VLC_CODE = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colVLCName).Value)
                            objPayProInv.VSP_CODE = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colVendorCode).Value)
                            objPayProInv.VSP_NAME = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colVendorDesc).Value)
                            objPayProInv.Payee_Joint_Name = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayeeJointName).Value)
                            objPayProInv.Payee_Joint_Bank_Code = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayeeJointBankCode).Value)
                            objPayProInv.Payee_Joint_Bank_Name = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayeeJointBankDesc).Value)
                            objPayProInv.Payee_Joint_Branch_Code = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayeeJointBranchCode).Value)
                            objPayProInv.Payee_Joint_Branch_Name = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayeeJointBranchDesc).Value)
                            objPayProInv.Payee_Joint_IFSC_Code = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayeeJointIFSC).Value)
                            objPayProInv.Payee_Joint_Ac_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayeeJointAcNo).Value)
                            objPayProInv.Milk_Qty = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMilkQty).Value)
                            objPayProInv.Inv_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colInvAmt).Value)
                            objPayProInv.Inv_EMP_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colEmpAmt).Value)
                            objPayProInv.Inv_Amt_EMP_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colInvAndEmpAmt).Value)
                            objPayProInv.Inv_Incentive_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colIncenAmt).Value)
                            objPayProInv.Inv_Incentive_EMP_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colIncenEmpAmt).Value)
                            objPayProInv.Gross_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value)
                            objPayProInv.Vsp_Own_System_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colVSPOwnSystemAmt).Value)
                            objPayProInv.Head_Load_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colHeadLoadAmt).Value)
                            objPayProInv.Deduction_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colInvDeduc).Value)
                            objPayProInv.Reduce_Deduc_Amt = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colReduceDeduc).Value)
                            objPayProInv.Vsp_Own_System_Doc_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colVSPOwnSystemAmt).Tag)
                            objPayProInv.Head_Load_Doc_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colHeadLoadAmt).Tag)
                            objPayProInv.Deduction_Doc_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colInvDeduc).Tag)
                            objPayProInv.Bank_Code = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colBankCode).Value)
                            objPayProInv.Bank_Desc = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colBankDesc).Value)
                            objPayProInv.Payment_Mode = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayMode).Value)
                            objPayProInv.Cheque_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colChequeNo).Value)
                            objPayProInv.Service_Charge_Amt = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colServiceChargeAmt).Value)

                            objPayProInv.MP_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMPAmount).Value)
                            objPayProInv.MP_EMP = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMPEMPAmount).Value)
                            objPayProInv.MP_Incentive = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMPIncentiveAmount).Value)
                            objPayProInv.MP_IncentiveEMP = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMPEMPIncentiveAmount).Value)
                            objPayProInv.MP_Net_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMPNetAmount).Value)
                            obj.arrclsPaymentProcessFarmerInvoices.Add(objPayProInv)
                        End If
                    Next
                End If

                If gvMccSale IsNot Nothing AndAlso gvMccSale.Rows.Count > 0 Then
                    obj.arrClsPaymentProcessMccSale = New List(Of clsPaymentProcessMCCSale)
                    Dim objPayProMccSale As clsPaymentProcessMCCSale = Nothing
                    For i = 0 To gvMccSale.Rows.Count - 1
                        If gvMccSale.Rows(i).Cells(colSelect).Value = True Then
                            objPayProMccSale = New clsPaymentProcessMCCSale
                            objPayProMccSale.SLNO = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colSlno).Value)
                            objPayProMccSale.Shipment_Doc_No = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colShipmentNo).Value)
                            objPayProMccSale.Shipment_Doc_Date = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colShipmentDate).Value)
                            objPayProMccSale.Sale_Doc_No = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colSaleInvNo).Value)
                            objPayProMccSale.Sale_Doc_Date = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colSaleInvDate).Value)
                            objPayProMccSale.AR_Invoice_No = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colARInvoiceNo).Value)
                            objPayProMccSale.AR_Invoice_Date = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colARInvoiceDate).Value)
                            objPayProMccSale.Customer_CODE = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colCustomerCode).Value)
                            objPayProMccSale.Customer_NAME = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colCustomerName).Value)
                            'objPayProMccSale.Item_Code = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colItemCode).Value)
                            'objPayProMccSale.Item_Desc = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colItemDesc).Value)
                            objPayProMccSale.Amount = clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colItemAmt).Value)
                            objPayProMccSale.Reduce_Deduc_Amt = clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colReduceDeduc).Value)
                            obj.arrClsPaymentProcessMccSale.Add(objPayProMccSale)
                        End If
                    Next
                End If

                If gvMccSaleFarmer IsNot Nothing AndAlso gvMccSaleFarmer.Rows.Count > 0 Then
                    obj.arrclsPaymentProcessFarmerMccSale = New List(Of clsPaymentProcessFarmerMCCSale)
                    Dim objPayProMccSale As clsPaymentProcessFarmerMCCSale = Nothing
                    For i = 0 To gvMccSaleFarmer.Rows.Count - 1
                        If gvMccSaleFarmer.Rows(i).Cells(colSelect).Value = True Then
                            objPayProMccSale = New clsPaymentProcessFarmerMCCSale
                            objPayProMccSale.SLNO = clsCommon.myCstr(gvMccSaleFarmer.Rows(i).Cells(colSlno).Value)
                            objPayProMccSale.Shipment_Doc_No = clsCommon.myCstr(gvMccSaleFarmer.Rows(i).Cells(colShipmentNo).Value)
                            objPayProMccSale.Shipment_Doc_Date = clsCommon.myCstr(gvMccSaleFarmer.Rows(i).Cells(colShipmentDate).Value)
                            objPayProMccSale.Sale_Doc_No = clsCommon.myCstr(gvMccSaleFarmer.Rows(i).Cells(colSaleInvNo).Value)
                            objPayProMccSale.Sale_Doc_Date = clsCommon.myCstr(gvMccSaleFarmer.Rows(i).Cells(colSaleInvDate).Value)
                            objPayProMccSale.Farmer_Code = clsCommon.myCstr(gvMccSaleFarmer.Rows(i).Cells(colFarmerCode).Value)
                            objPayProMccSale.Farmer_Name = clsCommon.myCstr(gvMccSaleFarmer.Rows(i).Cells(colFarmerName).Value)
                            objPayProMccSale.VSP_CODE = clsCommon.myCstr(gvMccSaleFarmer.Rows(i).Cells(colVendorCode).Value)
                            objPayProMccSale.VSP_NAME = clsCommon.myCstr(gvMccSaleFarmer.Rows(i).Cells(colVendorDesc).Value)
                            'objPayProMccSale.Item_Code = clsCommon.myCstr(gvMccSaleFarmer.Rows(i).Cells(colItemCode).Value)
                            'objPayProMccSale.Item_Desc = clsCommon.myCstr(gvMccSaleFarmer.Rows(i).Cells(colItemDesc).Value)
                            objPayProMccSale.Amount = clsCommon.myCdbl(gvMccSaleFarmer.Rows(i).Cells(colItemAmt).Value)
                            objPayProMccSale.Reduce_Deduc_Amt = clsCommon.myCdbl(gvMccSaleFarmer.Rows(i).Cells(colReduceDeduc).Value)
                            obj.arrclsPaymentProcessFarmerMccSale.Add(objPayProMccSale)
                        End If
                    Next
                End If

                If GvMccSaleReturn IsNot Nothing AndAlso GvMccSaleReturn.Rows.Count > 0 Then
                    obj.arrClsPaymentProcessMccSaleReturn = New List(Of clsPaymentProcessMCCSaleReturn)
                    Dim objPayProMccSale As clsPaymentProcessMCCSaleReturn = Nothing
                    For i = 0 To GvMccSaleReturn.Rows.Count - 1
                        If GvMccSaleReturn.Rows(i).Cells(colSelect).Value = True Then
                            objPayProMccSale = New clsPaymentProcessMCCSaleReturn
                            objPayProMccSale.SLNO = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colSlno).Value)
                            objPayProMccSale.Shipment_Doc_No = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colShipmentNo).Value)
                            objPayProMccSale.Shipment_Doc_Date = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colShipmentDate).Value)
                            objPayProMccSale.Sale_Doc_No = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colSaleInvNo).Value)
                            objPayProMccSale.Sale_Doc_Date = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colSaleInvDate).Value)
                            objPayProMccSale.AR_Invoice_No = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colARInvoiceNo).Value)
                            objPayProMccSale.AR_Invoice_Date = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colARInvoiceDate).Value)
                            objPayProMccSale.Customer_CODE = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colCustomerCode).Value)
                            objPayProMccSale.Customer_NAME = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colCustomerName).Value)
                            objPayProMccSale.Amount = clsCommon.myCdbl(GvMccSaleReturn.Rows(i).Cells(colItemAmt).Value)
                            objPayProMccSale.Return_Doc_No = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colReturnDocNo).Value)
                            objPayProMccSale.Return_Doc_Date = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colReturnDocDate).Value)
                            objPayProMccSale.Return_Doc_Type = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colReturnDocType).Value)
                            obj.arrClsPaymentProcessMccSaleReturn.Add(objPayProMccSale)
                        End If
                    Next
                End If

                If gvMccSaleReturnFarmer IsNot Nothing AndAlso gvMccSaleReturnFarmer.Rows.Count > 0 Then
                    obj.arrclsPaymentProcessFarmerMccSaleReturn = New List(Of clsPaymentProcessFarmerMCCSaleReturn)
                    Dim objPayProMccSale As clsPaymentProcessFarmerMCCSaleReturn = Nothing
                    For i = 0 To gvMccSaleReturnFarmer.Rows.Count - 1
                        If gvMccSaleReturnFarmer.Rows(i).Cells(colSelect).Value = True Then
                            objPayProMccSale = New clsPaymentProcessFarmerMCCSaleReturn
                            objPayProMccSale.SLNO = clsCommon.myCstr(gvMccSaleReturnFarmer.Rows(i).Cells(colSlno).Value)
                            objPayProMccSale.Shipment_Doc_No = clsCommon.myCstr(gvMccSaleReturnFarmer.Rows(i).Cells(colShipmentNo).Value)
                            objPayProMccSale.Shipment_Doc_Date = clsCommon.myCstr(gvMccSaleReturnFarmer.Rows(i).Cells(colShipmentDate).Value)
                            objPayProMccSale.Sale_Doc_No = clsCommon.myCstr(gvMccSaleReturnFarmer.Rows(i).Cells(colSaleInvNo).Value)
                            objPayProMccSale.Sale_Doc_Date = clsCommon.myCstr(gvMccSaleReturnFarmer.Rows(i).Cells(colSaleInvDate).Value)
                            objPayProMccSale.Farmer_Code = clsCommon.myCstr(gvMccSaleReturnFarmer.Rows(i).Cells(colFarmerCode).Value)
                            objPayProMccSale.Farmer_Name = clsCommon.myCstr(gvMccSaleReturnFarmer.Rows(i).Cells(colFarmerName).Value)
                            objPayProMccSale.VSP_CODE = clsCommon.myCstr(gvMccSaleReturnFarmer.Rows(i).Cells(colVendorCode).Value)
                            objPayProMccSale.VSP_NAME = clsCommon.myCstr(gvMccSaleReturnFarmer.Rows(i).Cells(colVendorDesc).Value)
                            objPayProMccSale.Amount = clsCommon.myCdbl(gvMccSaleReturnFarmer.Rows(i).Cells(colItemAmt).Value)
                            objPayProMccSale.Return_Doc_No = clsCommon.myCstr(gvMccSaleReturnFarmer.Rows(i).Cells(colReturnDocNo).Value)
                            objPayProMccSale.Return_Doc_Date = clsCommon.myCstr(gvMccSaleReturnFarmer.Rows(i).Cells(colReturnDocDate).Value)
                            objPayProMccSale.Return_Doc_Type = clsCommon.myCstr(gvMccSaleReturnFarmer.Rows(i).Cells(colReturnDocType).Value)
                            obj.arrclsPaymentProcessFarmerMccSaleReturn.Add(objPayProMccSale)
                        End If
                    Next
                End If

                If gvItemIssue IsNot Nothing AndAlso gvItemIssue.Rows.Count > 0 Then
                    obj.arrclsPaymentProcessFarmerItemIssue = New List(Of clsPaymentProcessFarmerItemIssue)
                    Dim objPayProItemIssue As clsPaymentProcessFarmerItemIssue = Nothing
                    For i = 0 To gvItemIssue.Rows.Count - 1
                        If gvItemIssue.Rows(i).Cells(colSelect).Value = True Then
                            objPayProItemIssue = New clsPaymentProcessFarmerItemIssue
                            objPayProItemIssue.SLNO = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colSlno).Value)
                            objPayProItemIssue.Item_Issue_Doc_No = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colVspItemIssueNo).Value)
                            objPayProItemIssue.Item_Issue_Doc_Date = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colVspItemIssueDate).Value)
                            objPayProItemIssue.AP_Invoice_No = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colAPInvoiceNo).Value)
                            objPayProItemIssue.AP_Invoice_Date = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colAPInvoiceDate).Value)
                            objPayProItemIssue.Vendor_CODE = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colVendorCode).Value)
                            objPayProItemIssue.Vendor_NAME = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colVendorDesc).Value)
                            objPayProItemIssue.Amount = clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colItemAmt).Value)
                            objPayProItemIssue.Reduce_Deduc_Amt = clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colReduceDeduc).Value)
                            obj.arrclsPaymentProcessFarmerItemIssue.Add(objPayProItemIssue)
                        End If
                    Next
                End If

                If gvItemIssueReturn IsNot Nothing AndAlso gvItemIssueReturn.Rows.Count > 0 Then
                    obj.arrclsPaymentProcessFarmerItemIssueReturn = New List(Of clsPaymentProcessFarmerItemIssueReturn)
                    Dim objPayProItemIssueReturn As clsPaymentProcessFarmerItemIssueReturn = Nothing
                    For i = 0 To gvItemIssueReturn.Rows.Count - 1
                        If gvItemIssueReturn.Rows(i).Cells(colSelect).Value = True Then
                            objPayProItemIssueReturn = New clsPaymentProcessFarmerItemIssueReturn
                            objPayProItemIssueReturn.SLNO = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colSlno).Value)
                            objPayProItemIssueReturn.Item_Issue_Return_No = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colVspItemIssueReturnNo).Value)
                            objPayProItemIssueReturn.Item_Issue_Doc_No = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colVspItemIssueNo).Value)
                            objPayProItemIssueReturn.Item_Issue_Return_Date = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colVspItemIssueDate).Value)
                            objPayProItemIssueReturn.AP_Invoice_No = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colAPInvoiceNo).Value)
                            objPayProItemIssueReturn.AP_Invoice_Date = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colAPInvoiceDate).Value)
                            objPayProItemIssueReturn.Vendor_CODE = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colVendorCode).Value)
                            objPayProItemIssueReturn.Vendor_NAME = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colVendorDesc).Value)
                            objPayProItemIssueReturn.Amount = clsCommon.myCdbl(gvItemIssueReturn.Rows(i).Cells(colItemAmt).Value)
                            'objPayProItemIssueReturn.Reduce_Deduc_Amt = clsCommon.myCdbl(gvItemIssueReturn.Rows(i).Cells(colReduceDeduc).Value)
                            obj.arrclsPaymentProcessFarmerItemIssueReturn.Add(objPayProItemIssueReturn)
                        End If
                    Next
                End If

                If gvDeduction IsNot Nothing AndAlso gvDeduction.Rows.Count > 0 Then
                    obj.arrclsPaymentProcessFarmerDeductions = New List(Of clsPaymentProcessFarmerDeduction)
                    Dim objPayProDeduction As clsPaymentProcessFarmerDeduction = Nothing
                    For i = 0 To gvDeduction.Rows.Count - 1
                        If gvDeduction.Rows(i).Cells(colSelect).Value = True Then
                            objPayProDeduction = New clsPaymentProcessFarmerDeduction
                            objPayProDeduction.SLNO = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colSlno).Value)
                            objPayProDeduction.AP_Invoice_No = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colAPInvoiceNo).Value)
                            objPayProDeduction.AP_Invoice_Date = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colAPInvoiceDate).Value)
                            objPayProDeduction.Vendor_CODE = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colVendorCode).Value)
                            objPayProDeduction.Vendor_NAME = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colVendorDesc).Value)
                            objPayProDeduction.Ded_Code = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colDeductionCode).Value)
                            objPayProDeduction.Ded_Desc = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colDeductionDesc).Value)
                            objPayProDeduction.Amount = clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colItemAmt).Value)
                            objPayProDeduction.Reduce_Deduc_Amt = clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colReduceDeduc).Value)
                            objPayProDeduction.IsFromPrevPPCycle = clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colIsFromPrevPPCycle).Value)
                            obj.arrclsPaymentProcessFarmerDeductions.Add(objPayProDeduction)
                        End If
                    Next
                End If

                If gvCreditNote IsNot Nothing AndAlso gvCreditNote.Rows.Count > 0 Then
                    obj.arrclsPaymentProcessFarmerCreditNote = New List(Of clsPaymentProcessFarmerCreditNote)
                    Dim objPayProCreditNote As clsPaymentProcessFarmerCreditNote = Nothing
                    For i = 0 To gvCreditNote.Rows.Count - 1
                        If gvCreditNote.Rows(i).Cells(colSelect).Value = True Then
                            objPayProCreditNote = New clsPaymentProcessFarmerCreditNote
                            objPayProCreditNote.SLNO = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colSlno).Value)
                            objPayProCreditNote.AP_Invoice_No = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colAPInvoiceNo).Value)
                            objPayProCreditNote.AP_Invoice_Date = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colAPInvoiceDate).Value)
                            objPayProCreditNote.Vendor_CODE = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colVendorCode).Value)
                            objPayProCreditNote.Vendor_NAME = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colVendorDesc).Value)
                            objPayProCreditNote.Amount = clsCommon.myCdbl(gvCreditNote.Rows(i).Cells(colItemAmt).Value)
                            obj.arrclsPaymentProcessFarmerCreditNote.Add(objPayProCreditNote)
                        End If
                    Next
                End If

                If gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
                    obj.ArrVSPPPDetail = New List(Of clsPaymentProcessDetail)
                    Dim objPPDetail As clsPaymentProcessDetail = Nothing
                    For i = 0 To gv.Rows.Count - 1
                        If clsCommon.myCBool(gv.Rows(i).Cells(colSelect).Value) Then
                            objPPDetail = New clsPaymentProcessDetail()
                            objPPDetail.Is_select = clsCommon.myCBool(gv.Rows(i).Cells(colSelect).Value)
                            objPPDetail.is_Hold_Payment_Process = clsCommon.myCBool(gv.Rows(i).Cells(colIsPaymentProcessHold).Value)
                            objPPDetail.SNo = clsCommon.myCstr(gv.Rows(i).Cells(colSlno).Value)
                            objPPDetail.Milk_Purchase_Invoice_No = clsCommon.myCstr(gv.Rows(i).Cells(colPurchaseInvoiceNo).Value)
                            objPPDetail.Milk_Purchase_Invoice_Date = clsCommon.myCDate(gv.Rows(i).Cells(colPurchaseInvoiceDate).Value)
                            objPPDetail.AP_Invoice_No = clsCommon.myCstr(gv.Rows(i).Cells(colAPInvoiceNo).Value)
                            objPPDetail.AP_Invoice_Date = clsCommon.myCDate(gv.Rows(i).Cells(colAPInvoiceDate).Value)
                            objPPDetail.VLC_CODE_Uploader = clsCommon.myCstr(gv.Rows(i).Cells(colVLCUploaderCode).Value)
                            objPPDetail.VLC_Name = clsCommon.myCstr(gv.Rows(i).Cells(colVLCName).Value)
                            objPPDetail.VSP_CODE = clsCommon.myCstr(gv.Rows(i).Cells(colVendorCode).Value)
                            objPPDetail.VSP_NAME = clsCommon.myCstr(gv.Rows(i).Cells(colVendorDesc).Value)
                            objPPDetail.Main_VSP_CODE = clsCommon.myCstr(gv.Rows(i).Cells(colActualVSPCode).Value)
                            objPPDetail.Main_VSP_NAME = clsCommon.myCstr(gv.Rows(i).Cells(colActualVSPName).Value)
                            objPPDetail.Payee_Joint_Name = clsCommon.myCstr(gv.Rows(i).Cells(colPayeeJointName).Value)
                            objPPDetail.Payee_Joint_Bank_Code = clsCommon.myCstr(gv.Rows(i).Cells(colPayeeJointBankCode).Value)
                            objPPDetail.Payee_Joint_Bank_Name = clsCommon.myCstr(gv.Rows(i).Cells(colPayeeJointBankDesc).Value)
                            objPPDetail.Payee_Joint_Branch_Code = clsCommon.myCstr(gv.Rows(i).Cells(colPayeeJointBranchCode).Value)
                            objPPDetail.Payee_Joint_Branch_Name = clsCommon.myCstr(gv.Rows(i).Cells(colPayeeJointBranchDesc).Value)
                            objPPDetail.Payee_Joint_Account_No = clsCommon.myCstr(gv.Rows(i).Cells(colPayeeJointAcNo).Value)
                            objPPDetail.Payee_Joint_IFSC_Code = clsCommon.myCstr(gv.Rows(i).Cells(colPayeeJointIFSC).Value)
                            objPPDetail.Bank_Code = clsCommon.myCstr(gv.Rows(i).Cells(colBankCode).Value)
                            objPPDetail.Bank_Desc = clsCommon.myCstr(gv.Rows(i).Cells(colBankDesc).Value)
                            objPPDetail.Payment_Mode = clsCommon.myCstr(gv.Rows(i).Cells(colPayMode).Value)
                            objPPDetail.Cheque_No = clsCommon.myCstr(gv.Rows(i).Cells(colChequeNo).Value)
                            If clsCommon.CompairString(objPPDetail.Payment_Mode, "Cheque") = CompairStringResult.Equal Then
                                objPPDetail.Cheque_Dated = clsCommon.myCstr(gv.Rows(i).Cells(colChequeDate).Value)
                            Else
                                objPPDetail.Cheque_Dated = Nothing
                            End If

                            objPPDetail.Milk_Qty = clsCommon.myCdbl(gv.Rows(i).Cells(colMilkQty).Value)
                            objPPDetail.VSP_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colVSPAmount).Value)
                            objPPDetail.MP_Net_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colMPNetAmount).Value)
                            objPPDetail.MP_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colMPAmount).Value)
                            objPPDetail.MP_EMP = clsCommon.myCdbl(gv.Rows(i).Cells(colMPEMPAmount).Value)
                            objPPDetail.MP_Incentive = clsCommon.myCdbl(gv.Rows(i).Cells(colMPIncentiveAmount).Value)
                            objPPDetail.Incentive_EMP_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colMPEMPIncentiveAmount).Value)

                            objPPDetail.MP_VSP_Diff_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colMPVSPDiffAmount).Value)
                            objPPDetail.Milk_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colInvAmt).Value)
                            objPPDetail.Incentive_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colIncenAmt).Value)
                            objPPDetail.EMP_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colEmpAmt).Value)
                            objPPDetail.Incentive_EMP_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colIncenEmpAmt).Value)
                            objPPDetail.Total_EMP_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colTotalEmp).Value)
                            objPPDetail.Total = clsCommon.myCdbl(gv.Rows(i).Cells(colInvAndEmpAmt).Value)
                            objPPDetail.Total_Invoice_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value)
                            objPPDetail.Vsp_Own_System_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colVSPOwnSystemAmt).Value)
                            objPPDetail.Head_Load_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colHeadLoadAmt).Value)
                            objPPDetail.Invoice_Deduction_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colInvDeduc).Value)
                            objPPDetail.Reduce_Deduc_Amt = clsCommon.myCdbl(gv.Rows(i).Cells(colReduceDeduc).Value)
                            objPPDetail.MCC_Sale_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colMccSaleTotalAmount).Value)
                            objPPDetail.MCC_Sale_Return_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colMccSaleReturnTotalAmount).Value)
                            objPPDetail.Item_Issue_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colItemIssueTotalAmount).Value)
                            objPPDetail.Item_Issue_Return_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colItemIssueReturnTotalAmount).Value)
                            objPPDetail.Deduction_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colDeductionTotalAmount).Value)
                            objPPDetail.Credit_Note_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colTotalCreditNoteAmount).Value)
                            objPPDetail.Payable_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colPaybleAmt).Value)
                            objPPDetail.Service_Charge_Amt = clsCommon.myCdbl(gv.Rows(i).Cells(colServiceChargeAmt).Value)

                            objPPDetail.Advance_Payment_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colAdvanceAmount).Value)
                            objPPDetail.Advance_Payment_Amount_Knock_Off = clsCommon.myCdbl(gv.Rows(i).Cells(colAdvanceKnockOffAmount).Value)

                            objPPDetail.VSP_Excess_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colgvVSPExcessAmount).Value)
                            objPPDetail.MP_Total_Deduction = clsCommon.myCdbl(gv.Rows(i).Cells(colgvTotMPDeductionAmount).Value)
                            objPPDetail.NextCycleDebitNote = clsCommon.myCdbl(gv.Rows(i).Cells(colNextCycleDebitNote).Value)

                            objPPDetail.FarmerMilkQty = clsCommon.myCdbl(gv.Rows(i).Cells(colFarmerMilkQty).Value)

                            objPPDetail.NextCycleDebitNoteMP = clsCommon.myCdbl(gv.Rows(i).Cells(colNextCycleDebitNoteFarmer).Value)
                            objPPDetail.FarmerPayment = clsCommon.myCdbl(gv.Rows(i).Cells(colFarmerPayment).Value)

                            '' new cols
                            objPPDetail.FarmerSaleAmount = clsCommon.myCdbl(gv.Rows(i).Cells(colFarmerTotalSale).Value)
                            objPPDetail.FarmerSaleReturnAmount = clsCommon.myCdbl(gv.Rows(i).Cells(colFarmerTotalSaleReturn).Value)
                            objPPDetail.FarmerAdjustmentAmount = clsCommon.myCdbl(gv.Rows(i).Cells(colFarmerAdjustmentAmountTotal).Value)

                            objPPDetail.FarmerPayableAmount = clsCommon.myCdbl(gv.Rows(i).Cells(colMPAmountPayable).Value)
                            objPPDetail.PrevCycleDebitNote = clsCommon.myCdbl(gv.Rows(i).Cells(colPrevCycleBalance).Value)
                            objPPDetail.NextCycleDebitNote = clsCommon.myCdbl(gv.Rows(i).Cells(colNextCycleDebitNote).Value)
                            obj.ArrVSPPPDetail.Add(objPPDetail)
                        End If
                    Next
                End If
                ''Balwinder
                If gvPaymentToFarmer IsNot Nothing AndAlso gvPaymentToFarmer.Rows.Count > 0 Then
                    obj.ArrPPDetail = New List(Of clsPaymentProcessFarmerPaymentDetail)
                    Dim objPPDetail As clsPaymentProcessFarmerPaymentDetail = Nothing
                    For i = 0 To gvPaymentToFarmer.Rows.Count - 1
                        If clsCommon.myCBool(gvPaymentToFarmer.Rows(i).Cells(colSelect).Value) Then
                            objPPDetail = New clsPaymentProcessFarmerPaymentDetail()
                            objPPDetail.Is_select = clsCommon.myCBool(gvPaymentToFarmer.Rows(i).Cells(colSelect).Value)
                            objPPDetail.is_Hold_Payment_Process = clsCommon.myCBool(gvPaymentToFarmer.Rows(i).Cells(colIsPaymentProcessHold).Value)
                            objPPDetail.SNo = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colSlno).Value)
                            objPPDetail.Farmer_Invoice_No = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colPurchaseInvoiceNo).Value)
                            objPPDetail.Farmer_Invoice_Date = clsCommon.myCDate(gvPaymentToFarmer.Rows(i).Cells(colPurchaseInvoiceDate).Value)
                            objPPDetail.AP_Adjustment_No = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colAPAdjustmentNo).Value)
                            objPPDetail.AP_Invoice_Date = clsCommon.myCDate(gvPaymentToFarmer.Rows(i).Cells(colAPAdjustmentDate).Value)
                            objPPDetail.AP_Invoice_No = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colAPInvoiceNo).Value)
                            objPPDetail.AP_Invoice_Date = clsCommon.myCDate(gvPaymentToFarmer.Rows(i).Cells(colAPInvoiceDate).Value)
                            objPPDetail.VLC_CODE_Uploader = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colVLCUploaderCode).Value)
                            objPPDetail.VLC_Name = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colVLCName).Value)
                            objPPDetail.VSP_CODE = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colVendorCode).Value)
                            objPPDetail.VSP_NAME = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colVendorDesc).Value)
                            objPPDetail.Farmer_Code = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colFarmerCode).Value)
                            objPPDetail.Farmer_Name = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colFarmerName).Value)
                            objPPDetail.Payee_Joint_Name = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colPayeeJointName).Value)
                            objPPDetail.Payee_Joint_Bank_Code = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colPayeeJointBankCode).Value)
                            objPPDetail.Payee_Joint_Bank_Name = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colPayeeJointBankDesc).Value)
                            objPPDetail.Payee_Joint_Branch_Code = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colPayeeJointBranchCode).Value)
                            objPPDetail.Payee_Joint_Branch_Name = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colPayeeJointBranchDesc).Value)
                            objPPDetail.Payee_Joint_Account_No = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colPayeeJointAcNo).Value)
                            objPPDetail.Payee_Joint_IFSC_Code = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colPayeeJointIFSC).Value)
                            objPPDetail.Bank_Code = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colBankCode).Value)
                            objPPDetail.Bank_Desc = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colBankDesc).Value)
                            objPPDetail.Payment_Mode = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colPayMode).Value)
                            objPPDetail.Cheque_No = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colChequeNo).Value)
                            If clsCommon.CompairString(objPPDetail.Payment_Mode, "Cheque") = CompairStringResult.Equal Then
                                objPPDetail.Cheque_Dated = clsCommon.myCstr(gvPaymentToFarmer.Rows(i).Cells(colChequeDate).Value)
                            Else
                                objPPDetail.Cheque_Dated = Nothing
                            End If
                            objPPDetail.Milk_Qty = clsCommon.myCdbl(gvPaymentToFarmer.Rows(i).Cells(colMilkQty).Value)
                            objPPDetail.Milk_Amount = clsCommon.myCdbl(gvPaymentToFarmer.Rows(i).Cells(colMPAmount).Value)

                            objPPDetail.MCC_Sale_Amount = clsCommon.myCdbl(gvPaymentToFarmer.Rows(i).Cells(colMccSaleTotalAmount).Value)
                            objPPDetail.MCC_Sale_Return_Amount = clsCommon.myCdbl(gvPaymentToFarmer.Rows(i).Cells(colMccSaleReturnTotalAmount).Value)
                            '' adjustment column
                            objPPDetail.MP_Adjust_Amount = clsCommon.myCdbl(gvPaymentToFarmer.Rows(i).Cells(colMPAdjustAmount).Value)

                            objPPDetail.Incentive_Amount = clsCommon.myCdbl(gvPaymentToFarmer.Rows(i).Cells(colIncentiveAmt).Value)
                            objPPDetail.Deduction_Amount = clsCommon.myCdbl(gvPaymentToFarmer.Rows(i).Cells(colDeductionAmt).Value)

                            objPPDetail.Payable_Amount = clsCommon.myCdbl(gvPaymentToFarmer.Rows(i).Cells(colPaybleAmt).Value)
                            objPPDetail.NextCycleDebitNoteMP = clsCommon.myCdbl(gvPaymentToFarmer.Rows(i).Cells(colNextCycleDebitNoteFarmer).Value)

                            objPPDetail.Total_Advance_Amount = clsCommon.myCdbl(gvPaymentToFarmer.Rows(i).Cells(colFATotalAdvance).Value)
                            objPPDetail.Total_Advance_Amount_Recovery = clsCommon.myCdbl(gvPaymentToFarmer.Rows(i).Cells(colFATotalAdvanceRecovery).Value)

                            objPPDetail.Total_Loan_Payment = clsCommon.myCdbl(gvPaymentToFarmer.Rows(i).Cells(colFALoanPayment).Value)
                            objPPDetail.Total_Loan_Payment_Recovery = clsCommon.myCdbl(gvPaymentToFarmer.Rows(i).Cells(colFALoanPaymentRecovery).Value)

                            obj.ArrPPDetail.Add(objPPDetail)
                            Dim Total_Advance_Amount_Recovery As Decimal = objPPDetail.Total_Advance_Amount_Recovery
                            Dim Total_Loan_Payment_Recovery As Decimal = objPPDetail.Total_Loan_Payment_Recovery
                            For idx = 0 To gvFA.Rows.Count - 1
                                If clsCommon.CompairString(objPPDetail.Farmer_Code, clsCommon.myCstr(gvFA.Rows(idx).Cells(colFAFarmerCode).Value)) = CompairStringResult.Equal Then
                                    If clsCommon.myCBool(gvFA.Rows(idx).Cells(colFALoan).Value) Then
                                        If Total_Loan_Payment_Recovery < clsCommon.myCdbl(gvFA.Rows(idx).Cells(colFAPaymentAmt).Value) Then
                                            gvFA.Rows(idx).Cells(colFAKnockOff).Value = Total_Loan_Payment_Recovery
                                            Total_Loan_Payment_Recovery = 0
                                        Else
                                            gvFA.Rows(idx).Cells(colFAKnockOff).Value = gvFA.Rows(idx).Cells(colFAPaymentAmt).Value
                                            Total_Loan_Payment_Recovery -= clsCommon.myCdbl(gvFA.Rows(idx).Cells(colFAPaymentAmt).Value)
                                        End If
                                    Else
                                        If Total_Advance_Amount_Recovery < clsCommon.myCdbl(gvFA.Rows(idx).Cells(colFAPaymentAmt).Value) Then
                                            gvFA.Rows(idx).Cells(colFAKnockOff).Value = Total_Advance_Amount_Recovery
                                            Total_Advance_Amount_Recovery = 0
                                        Else
                                            gvFA.Rows(idx).Cells(colFAKnockOff).Value = gvFA.Rows(idx).Cells(colFAPaymentAmt).Value
                                            Total_Advance_Amount_Recovery -= clsCommon.myCdbl(gvFA.Rows(idx).Cells(colFAPaymentAmt).Value)
                                        End If
                                    End If
                                    If Total_Advance_Amount_Recovery <= 0 AndAlso Total_Loan_Payment_Recovery <= 0 Then
                                        Exit For
                                    End If
                                End If
                            Next
                        End If
                    Next
                End If

                If gvAdvancePayment IsNot Nothing AndAlso gvAdvancePayment.Rows.Count > 0 Then
                    obj.ArrPPAdvancePayment = New List(Of clsPaymentProcessFarmerAdvancePayment)
                    Dim objPPAdvancePayment As clsPaymentProcessFarmerAdvancePayment = Nothing
                    For i = 0 To gvAdvancePayment.Rows.Count - 1
                        If clsCommon.myCBool(gvAdvancePayment.Rows(i).Cells(colAPSelect).Value) Then
                            objPPAdvancePayment = New clsPaymentProcessFarmerAdvancePayment
                            objPPAdvancePayment.SNo = clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPSNo).Value)
                            objPPAdvancePayment.Vendor_Code = clsCommon.myCstr(gvAdvancePayment.Rows(i).Cells(colAPVendorCode).Value)
                            objPPAdvancePayment.Payment_No = clsCommon.myCstr(gvAdvancePayment.Rows(i).Cells(colAPPaymentCode).Value)
                            objPPAdvancePayment.Payment_Amount = clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPPaymentAmt).Value)
                            objPPAdvancePayment.Payment_Balance = clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPPaymentAmtBalance).Value)
                            obj.ArrPPAdvancePayment.Add(objPPAdvancePayment)
                        End If
                    Next
                End If

                If gvMPAdj IsNot Nothing AndAlso gvMPAdj.Rows.Count > 0 Then
                    obj.ArrPPAdjustment = New List(Of clsPaymentProcessAdjustment)
                    Dim objPPAdj As clsPaymentProcessAdjustment = Nothing
                    For i = 0 To gvMPAdj.Rows.Count - 1
                        If clsCommon.myCBool(gvMPAdj.Rows(i).Cells(colSelect).Value) Then
                            objPPAdj = New clsPaymentProcessAdjustment
                            objPPAdj.Doc_No = clsCommon.myCstr(fndDocNo.Value)
                            objPPAdj.Adjustment_No = clsCommon.myCstr(gvMPAdj.Rows(i).Cells(colAdjNo).Value)
                            objPPAdj.SNo = clsCommon.myCdbl(gvMPAdj.Rows(i).Cells(colSlno).Value)
                            objPPAdj.Adjustment_Amount = clsCommon.myCdbl(gvMPAdj.Rows(i).Cells(colMPAdjustAmount).Value)
                            objPPAdj.Adjustment_Date = clsCommon.myCDate(gvMPAdj.Rows(i).Cells(colMPAdjDate).Value)
                            objPPAdj.Adjustment_Type = clsCommon.myCstr(gvMPAdj.Rows(i).Cells(colMPAdjType).Value)
                            objPPAdj.Desciption = clsCommon.myCstr(gvMPAdj.Rows(i).Cells(colMPADjDesc).Value)
                            objPPAdj.Remarks = clsCommon.myCstr(gvMPAdj.Rows(i).Cells(colMPAdjRemarks).Value)
                            objPPAdj.Is_select = gvMPAdj.Rows(i).Cells(colSelect).Value
                            objPPAdj.Farmer_Code = clsCommon.myCstr(gvMPAdj.Rows(i).Cells(colFarmerCode).Value)
                            objPPAdj.Farmer_Name = clsCommon.myCstr(gvMPAdj.Rows(i).Cells(colFarmerName).Value)
                            objPPAdj.VSP_CODE = clsCommon.myCstr(gvMPAdj.Rows(i).Cells(colVendorCode).Value)
                            objPPAdj.VSP_NAME = clsCommon.myCstr(gvMPAdj.Rows(i).Cells(colVendorDesc).Value)
                            obj.ArrPPAdjustment.Add(objPPAdj)
                        End If
                    Next
                End If

                If gvFA IsNot Nothing AndAlso gvFA.Rows.Count > 0 Then
                    obj.arrMPAdvance = New List(Of clsPaymentProcessFarmerMPAdvance)
                    Dim objMPAdv As clsPaymentProcessFarmerMPAdvance = Nothing
                    For i = 0 To gvFA.Rows.Count - 1
                        objMPAdv = New clsPaymentProcessFarmerMPAdvance
                        objMPAdv.MP_Code = clsCommon.myCstr(gvFA.Rows(i).Cells(colFAFarmerCode).Value)
                        objMPAdv.MP_Name = clsCommon.myCstr(gvFA.Rows(i).Cells(colFAFarmerName).Value)
                        objMPAdv.Payment_No = clsCommon.myCstr(gvFA.Rows(i).Cells(colFAPaymentCode).Value)
                        objMPAdv.Payment_Date = clsCommon.myCDate(gvFA.Rows(i).Cells(colFAPaymentDate).Value)
                        objMPAdv.Is_Loan = clsCommon.myCBool(gvFA.Rows(i).Cells(colFALoan).Value)
                        objMPAdv.Payment_Amount = clsCommon.myCdbl(gvFA.Rows(i).Cells(colFAPaymentAmt).Value)
                        objMPAdv.Knock_Off_Amt = clsCommon.myCdbl(gvFA.Rows(i).Cells(colFAKnockOff).Value)
                        obj.arrMPAdvance.Add(objMPAdv)
                    Next
                End If

                If clsPaymentProcessFarmerHead.SaveData(obj, isNewEntry) Then
                    fndDocNo.Value = obj.Doc_No
                    If Not isPostbtnClick Then
                        If isNewEntry Then
                            myMessages.insert()
                        Else
                            myMessages.update()
                        End If
                    End If

                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    btnProcess.Enabled = True
                    fndDocNo.MyReadOnly = True
                    lblPending.Status = ERPTransactionStatus.Pending
                    'LoadData(obj.Doc_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            If isPostbtnClick Then
                Throw New Exception(ex.Message)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal navType As NavigatorType)
        Dim obj As clsPaymentProcessFarmerHead = clsPaymentProcessFarmerHead.getData(strCode, navType)
        If obj IsNot Nothing Then
            Reset()
            fndLoc.Enabled = False
            isNewEntry = False
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            btnProcess.Enabled = True
            fndDocNo.MyReadOnly = True
            fndDocNo.Value = obj.Doc_No
            fndLoc.Value = obj.Loc_Seg_Code
            txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndLoc.Value & "' "))
            dtpFromDate.Value = obj.From_Date
            dtpToDate.Value = obj.To_Date
            dtpDate.Value = obj.Doc_Date
            txtNEFTUploaderREFNo.Text = obj.DocRefNoForUploader
            txtNEFTUploaderREFNo.Tag = obj.PaymentDesc
            If obj.isPosted = 0 Then
                lblPending.Status = ERPTransactionStatus.Pending
            Else
                btnSave.Enabled = False
                btnProcess.Enabled = False
                btnDelete.Enabled = False
                lblPending.Status = ERPTransactionStatus.Approved
            End If
            isLoad = True
            Dim i As Integer = 0
            If obj.arrclsPaymentProcessFarmerInvoices IsNot Nothing AndAlso obj.arrclsPaymentProcessFarmerInvoices.Count > 0 Then
                gvInvoice.Rows.Clear()
                For i = 0 To obj.arrclsPaymentProcessFarmerInvoices.Count - 1
                    gvInvoice.Rows.AddNew()
                    gvInvoice.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvInvoice.Rows(i).Cells(colSelect).Value = True
                    gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).AP_Invoice_No
                    gvInvoice.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrclsPaymentProcessFarmerInvoices.Item(i).AP_Invoice_Date, "dd/MMM/yyyy")
                    gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Milk_Purchase_Invoice_No
                    gvInvoice.Rows(i).Cells(colPurchaseInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrclsPaymentProcessFarmerInvoices.Item(i).Milk_Purchase_Invoice_Date, "dd/MMM/yyyy")
                    gvInvoice.Rows(i).Cells(colVLCCode).Value = getVLCUploaderCode(obj.arrclsPaymentProcessFarmerInvoices.Item(i).VSP_CODE)
                    gvInvoice.Rows(i).Cells(colVLCName).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).VLC_CODE
                    gvInvoice.Rows(i).Cells(colVendorCode).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).VSP_CODE
                    gvInvoice.Rows(i).Cells(colVendorDesc).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).VSP_NAME
                    gvInvoice.Rows(i).Cells(colPayeeJointName).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Payee_Joint_Name
                    gvInvoice.Rows(i).Cells(colPayeeJointBankCode).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Payee_Joint_Bank_Code
                    gvInvoice.Rows(i).Cells(colPayeeJointBankDesc).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Payee_Joint_Bank_Name
                    gvInvoice.Rows(i).Cells(colPayeeJointBranchCode).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Payee_Joint_Branch_Code
                    gvInvoice.Rows(i).Cells(colPayeeJointBranchDesc).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Payee_Joint_Branch_Name
                    gvInvoice.Rows(i).Cells(colPayeeJointIFSC).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Payee_Joint_IFSC_Code
                    gvInvoice.Rows(i).Cells(colPayeeJointAcNo).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Payee_Joint_Ac_No
                    gvInvoice.Rows(i).Cells(colMilkQty).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerInvoices.Item(i).Milk_Qty)
                    gvInvoice.Rows(i).Cells(colInvAmt).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerInvoices.Item(i).Inv_Amount)
                    gvInvoice.Rows(i).Cells(colEmpAmt).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerInvoices.Item(i).Inv_EMP_Amount)
                    gvInvoice.Rows(i).Cells(colInvAndEmpAmt).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerInvoices.Item(i).Inv_Amt_EMP_Amount)
                    gvInvoice.Rows(i).Cells(colIncenAmt).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerInvoices.Item(i).Inv_Incentive_Amount)
                    gvInvoice.Rows(i).Cells(colIncenEmpAmt).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerInvoices.Item(i).Inv_Incentive_EMP_Amount)
                    gvInvoice.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerInvoices.Item(i).Gross_Amount)
                    gvInvoice.Rows(i).Cells(colVSPOwnSystemAmt).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Vsp_Own_System_Amount
                    gvInvoice.Rows(i).Cells(colVSPOwnSystemAmt).Tag = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Vsp_Own_System_Doc_No
                    gvInvoice.Rows(i).Cells(colHeadLoadAmt).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Head_Load_Amount
                    gvInvoice.Rows(i).Cells(colHeadLoadAmt).Tag = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Head_Load_Doc_No
                    gvInvoice.Rows(i).Cells(colInvDeduc).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Deduction_Amount
                    gvInvoice.Rows(i).Cells(colInvDeduc).Tag = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Deduction_Doc_No
                    gvInvoice.Rows(i).Cells(colReduceDeduc).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Reduce_Deduc_Amt
                    gvInvoice.Rows(i).Cells(colReduceDeduc).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Reduce_Deduc_Amt
                    gvInvoice.Rows(i).Cells(colBankCode).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Bank_Code
                    gvInvoice.Rows(i).Cells(colBankDesc).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Bank_Desc
                    gvInvoice.Rows(i).Cells(colPayMode).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Payment_Mode
                    gvInvoice.Rows(i).Cells(colChequeNo).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Cheque_No
                    gvInvoice.Rows(i).Cells(colServiceChargeAmt).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).Service_Charge_Amt
                    gvInvoice.Rows(i).Cells(colActualVSPCode).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).ActualVSPCode
                    gvInvoice.Rows(i).Cells(colActualVSPName).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).ActualVSPName
                    gvInvoice.Rows(i).Cells(colPayToFarmer).Value = "1"

                    gvInvoice.Rows(i).Cells(colMPAmount).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).MP_Amount
                    gvInvoice.Rows(i).Cells(colMPEMPAmount).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).MP_EMP
                    gvInvoice.Rows(i).Cells(colMPIncentiveAmount).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).MP_Incentive
                    gvInvoice.Rows(i).Cells(colMPEMPIncentiveAmount).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).MP_IncentiveEMP
                    gvInvoice.Rows(i).Cells(colMPNetAmount).Value = obj.arrclsPaymentProcessFarmerInvoices.Item(i).MP_Net_Amount
                Next
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                gvInvoice.SummaryRowsBottom.Clear()

                For iii As Integer = 0 To gvInvoice.Columns.Count - 1
                    If TypeOf (gvInvoice.Columns(iii)) Is GridViewDecimalColumn Then
                        summaryRowItem.Add(New GridViewSummaryItem(gvInvoice.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                    End If
                Next

                gvInvoice.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
            If obj.arrclsPaymentProcessFarmerMccSale IsNot Nothing AndAlso obj.arrclsPaymentProcessFarmerMccSale.Count > 0 Then
                gvMccSaleFarmer.Rows.Clear()
                For i = 0 To obj.arrclsPaymentProcessFarmerMccSale.Count - 1
                    gvMccSaleFarmer.Rows.AddNew()
                    gvMccSaleFarmer.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvMccSaleFarmer.Rows(i).Cells(colSelect).Value = True
                    gvMccSaleFarmer.Rows(i).Cells(colShipmentNo).Value = obj.arrclsPaymentProcessFarmerMccSale.Item(i).Shipment_Doc_No
                    gvMccSaleFarmer.Rows(i).Cells(colShipmentDate).Value = clsCommon.GetPrintDate(obj.arrclsPaymentProcessFarmerMccSale.Item(i).Shipment_Doc_Date, "dd/MMM/yyyy")
                    gvMccSaleFarmer.Rows(i).Cells(colSaleInvNo).Value = obj.arrclsPaymentProcessFarmerMccSale.Item(i).Sale_Doc_No
                    gvMccSaleFarmer.Rows(i).Cells(colSaleInvDate).Value = clsCommon.GetPrintDate(obj.arrclsPaymentProcessFarmerMccSale.Item(i).Sale_Doc_Date, "dd/MMM/yyyy")
                    'gvMccSaleFarmer.Rows(i).Cells(colARInvoiceNo).Value = obj.arrclsPaymentProcessFarmerMccSale.Item(i).AR_Invoice_No
                    'gvMccSaleFarmer.Rows(i).Cells(colARInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSale.Item(i).AR_Invoice_Date, "dd/MMM/yyyy")
                    gvMccSaleFarmer.Rows(i).Cells(colVendorCode).Value = obj.arrclsPaymentProcessFarmerMccSale.Item(i).VSP_CODE
                    gvMccSaleFarmer.Rows(i).Cells(colVendorDesc).Value = obj.arrclsPaymentProcessFarmerMccSale.Item(i).VSP_NAME

                    gvMccSaleFarmer.Rows(i).Cells(colFarmerCode).Value = obj.arrclsPaymentProcessFarmerMccSale.Item(i).Farmer_Code

                    gvMccSaleFarmer.Rows(i).Cells(colFarmerName).Value = obj.arrclsPaymentProcessFarmerMccSale.Item(i).Farmer_Name

                    gvMccSaleFarmer.Rows(i).Cells(colItemAmt).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerMccSale.Item(i).Amount)
                    gvMccSaleFarmer.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerMccSale.Item(i).Reduce_Deduc_Amt)
                Next

                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                gvMccSaleFarmer.SummaryRowsBottom.Clear()

                For iii As Integer = 0 To gvMccSaleFarmer.Columns.Count - 1
                    If TypeOf (gvMccSaleFarmer.Columns(iii)) Is GridViewDecimalColumn Then
                        summaryRowItem.Add(New GridViewSummaryItem(gvMccSaleFarmer.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                    End If
                Next

                gvMccSaleFarmer.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
            If obj.arrClsPaymentProcessMccSale IsNot Nothing AndAlso obj.arrClsPaymentProcessMccSale.Count > 0 Then
                gvMccSale.Rows.Clear()
                For i = 0 To obj.arrClsPaymentProcessMccSale.Count - 1
                    gvMccSale.Rows.AddNew()
                    gvMccSale.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvMccSale.Rows(i).Cells(colSelect).Value = True
                    gvMccSale.Rows(i).Cells(colShipmentNo).Value = obj.arrClsPaymentProcessMccSale.Item(i).Shipment_Doc_No
                    gvMccSale.Rows(i).Cells(colShipmentDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSale.Item(i).Shipment_Doc_Date, "dd/MMM/yyyy")
                    gvMccSale.Rows(i).Cells(colSaleInvNo).Value = obj.arrClsPaymentProcessMccSale.Item(i).Sale_Doc_No
                    gvMccSale.Rows(i).Cells(colSaleInvDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSale.Item(i).Sale_Doc_Date, "dd/MMM/yyyy")
                    gvMccSale.Rows(i).Cells(colARInvoiceNo).Value = obj.arrClsPaymentProcessMccSale.Item(i).AR_Invoice_No
                    gvMccSale.Rows(i).Cells(colARInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSale.Item(i).AR_Invoice_Date, "dd/MMM/yyyy")
                    gvMccSale.Rows(i).Cells(colCustomerCode).Value = obj.arrClsPaymentProcessMccSale.Item(i).Customer_CODE
                    gvMccSale.Rows(i).Cells(colCustomerName).Value = obj.arrClsPaymentProcessMccSale.Item(i).Customer_NAME
                    'gvMccSale.Rows(i).Cells(colItemCode).Value = obj.arrClsPaymentProcessMccSale.Item(i).Item_Code
                    'gvMccSale.Rows(i).Cells(colItemDesc).Value = obj.arrClsPaymentProcessMccSale.Item(i).Item_Desc
                    gvMccSale.Rows(i).Cells(colItemAmt).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessMccSale.Item(i).Amount)
                    gvMccSale.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessMccSale.Item(i).Reduce_Deduc_Amt)
                Next
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                gvMccSale.SummaryRowsBottom.Clear()

                For iii As Integer = 0 To gvMccSale.Columns.Count - 1
                    If TypeOf (gvMccSale.Columns(iii)) Is GridViewDecimalColumn Then
                        summaryRowItem.Add(New GridViewSummaryItem(gvMccSale.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                    End If
                Next

                gvMccSale.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
            If obj.arrclsPaymentProcessFarmerItemIssue IsNot Nothing AndAlso obj.arrclsPaymentProcessFarmerItemIssue.Count > 0 Then
                gvItemIssue.Rows.Clear()
                For i = 0 To obj.arrclsPaymentProcessFarmerItemIssue.Count - 1
                    gvItemIssue.Rows.AddNew()
                    gvItemIssue.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvItemIssue.Rows(i).Cells(colSelect).Value = True
                    gvItemIssue.Rows(i).Cells(colVspItemIssueNo).Value = obj.arrclsPaymentProcessFarmerItemIssue.Item(i).Item_Issue_Doc_No
                    gvItemIssue.Rows(i).Cells(colVspItemIssueDate).Value = clsCommon.GetPrintDate(obj.arrclsPaymentProcessFarmerItemIssue.Item(i).Item_Issue_Doc_Date, "dd/MMM/yyyy")
                    gvItemIssue.Rows(i).Cells(colAPInvoiceNo).Value = obj.arrclsPaymentProcessFarmerItemIssue.Item(i).AP_Invoice_No
                    gvItemIssue.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrclsPaymentProcessFarmerItemIssue.Item(i).AP_Invoice_Date, "dd/MMM/yyyy")
                    gvItemIssue.Rows(i).Cells(colVendorCode).Value = obj.arrclsPaymentProcessFarmerItemIssue.Item(i).Vendor_CODE
                    gvItemIssue.Rows(i).Cells(colVendorDesc).Value = obj.arrclsPaymentProcessFarmerItemIssue.Item(i).Vendor_NAME
                    'gvItemIssue.Rows(i).Cells(colItemCode).Value = obj.arrClsPaymentProcessFarmerItemIssue.Item(i).Item_Code
                    'gvItemIssue.Rows(i).Cells(colItemDesc).Value = obj.arrClsPaymentProcessFarmerItemIssue.Item(i).Item_Desc
                    gvItemIssue.Rows(i).Cells(colItemAmt).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerItemIssue.Item(i).Amount)
                    gvItemIssue.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerItemIssue.Item(i).Reduce_Deduc_Amt)
                Next
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                gvItemIssue.SummaryRowsBottom.Clear()

                For iii As Integer = 0 To gvItemIssue.Columns.Count - 1
                    If TypeOf (gvItemIssue.Columns(iii)) Is GridViewDecimalColumn Then
                        summaryRowItem.Add(New GridViewSummaryItem(gvItemIssue.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                    End If
                Next

                gvItemIssue.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If

            If obj.arrclsPaymentProcessFarmerItemIssueReturn IsNot Nothing AndAlso obj.arrclsPaymentProcessFarmerItemIssueReturn.Count > 0 Then
                gvItemIssueReturn.Rows.Clear()
                For i = 0 To obj.arrclsPaymentProcessFarmerItemIssueReturn.Count - 1
                    gvItemIssueReturn.Rows.AddNew()
                    gvItemIssueReturn.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvItemIssueReturn.Rows(i).Cells(colSelect).Value = True
                    gvItemIssueReturn.Rows(i).Cells(colVspItemIssueReturnNo).Value = obj.arrclsPaymentProcessFarmerItemIssueReturn.Item(i).Item_Issue_Return_No
                    gvItemIssueReturn.Rows(i).Cells(colVspItemIssueNo).Value = obj.arrclsPaymentProcessFarmerItemIssueReturn.Item(i).Item_Issue_Doc_No
                    gvItemIssueReturn.Rows(i).Cells(colVspItemIssueDate).Value = clsCommon.GetPrintDate(obj.arrclsPaymentProcessFarmerItemIssueReturn.Item(i).Item_Issue_Return_Date, "dd/MMM/yyyy")
                    gvItemIssueReturn.Rows(i).Cells(colAPInvoiceNo).Value = obj.arrclsPaymentProcessFarmerItemIssueReturn.Item(i).AP_Invoice_No
                    gvItemIssueReturn.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrclsPaymentProcessFarmerItemIssueReturn.Item(i).AP_Invoice_Date, "dd/MMM/yyyy")
                    gvItemIssueReturn.Rows(i).Cells(colVendorCode).Value = obj.arrclsPaymentProcessFarmerItemIssueReturn.Item(i).Vendor_CODE
                    gvItemIssueReturn.Rows(i).Cells(colVendorDesc).Value = obj.arrclsPaymentProcessFarmerItemIssueReturn.Item(i).Vendor_NAME
                    gvItemIssueReturn.Rows(i).Cells(colItemAmt).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerItemIssueReturn.Item(i).Amount)
                    'gvItemIssueReturn.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessFarmerItemIssueReturn.Item(i).Reduce_Deduc_Amt)
                Next

                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                gvItemIssueReturn.SummaryRowsBottom.Clear()

                For iii As Integer = 0 To gvItemIssueReturn.Columns.Count - 1
                    If TypeOf (gvItemIssueReturn.Columns(iii)) Is GridViewDecimalColumn Then
                        summaryRowItem.Add(New GridViewSummaryItem(gvItemIssueReturn.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                    End If
                Next

                gvItemIssueReturn.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If

            If obj.arrclsPaymentProcessFarmerDeductions IsNot Nothing AndAlso obj.arrclsPaymentProcessFarmerDeductions.Count > 0 Then
                gvDeduction.Rows.Clear()
                For i = 0 To obj.arrclsPaymentProcessFarmerDeductions.Count - 1
                    gvDeduction.Rows.AddNew()
                    gvDeduction.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvDeduction.Rows(i).Cells(colSelect).Value = True
                    gvDeduction.Rows(i).Cells(colAPInvoiceNo).Value = obj.arrclsPaymentProcessFarmerDeductions.Item(i).AP_Invoice_No
                    gvDeduction.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrclsPaymentProcessFarmerDeductions.Item(i).AP_Invoice_Date, "dd/MMM/yyyy")
                    gvDeduction.Rows(i).Cells(colVendorCode).Value = obj.arrclsPaymentProcessFarmerDeductions.Item(i).Vendor_CODE
                    gvDeduction.Rows(i).Cells(colVendorDesc).Value = obj.arrclsPaymentProcessFarmerDeductions.Item(i).Vendor_NAME
                    gvDeduction.Rows(i).Cells(colDeductionCode).Value = obj.arrclsPaymentProcessFarmerDeductions.Item(i).Ded_Code
                    gvDeduction.Rows(i).Cells(colDeductionDesc).Value = obj.arrclsPaymentProcessFarmerDeductions.Item(i).Ded_Desc
                    gvDeduction.Rows(i).Cells(colItemAmt).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerDeductions.Item(i).Amount)
                    gvDeduction.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerDeductions.Item(i).Reduce_Deduc_Amt)
                    gvDeduction.Rows(i).Cells(colIsFromPrevPPCycle).Value = obj.arrclsPaymentProcessFarmerDeductions.Item(i).IsFromPrevPPCycle
                Next
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                gvDeduction.SummaryRowsBottom.Clear()

                For iii As Integer = 0 To gvDeduction.Columns.Count - 1
                    If TypeOf (gvDeduction.Columns(iii)) Is GridViewDecimalColumn Then
                        summaryRowItem.Add(New GridViewSummaryItem(gvDeduction.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                    End If
                Next

                gvDeduction.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If

            If obj.arrclsPaymentProcessFarmerCreditNote IsNot Nothing AndAlso obj.arrclsPaymentProcessFarmerCreditNote.Count > 0 Then
                gvCreditNote.Rows.Clear()
                For i = 0 To obj.arrclsPaymentProcessFarmerCreditNote.Count - 1
                    gvCreditNote.Rows.AddNew()
                    gvCreditNote.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvCreditNote.Rows(i).Cells(colSelect).Value = True
                    gvCreditNote.Rows(i).Cells(colAPInvoiceNo).Value = obj.arrclsPaymentProcessFarmerCreditNote.Item(i).AP_Invoice_No
                    gvCreditNote.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrclsPaymentProcessFarmerCreditNote.Item(i).AP_Invoice_Date, "dd/MMM/yyyy")
                    gvCreditNote.Rows(i).Cells(colVendorCode).Value = obj.arrclsPaymentProcessFarmerCreditNote.Item(i).Vendor_CODE
                    gvCreditNote.Rows(i).Cells(colVendorDesc).Value = obj.arrclsPaymentProcessFarmerCreditNote.Item(i).Vendor_NAME
                    gvCreditNote.Rows(i).Cells(colItemAmt).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerCreditNote.Item(i).Amount)

                Next
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                gvCreditNote.SummaryRowsBottom.Clear()

                For iii As Integer = 0 To gvCreditNote.Columns.Count - 1
                    If TypeOf (gvCreditNote.Columns(iii)) Is GridViewDecimalColumn Then
                        summaryRowItem.Add(New GridViewSummaryItem(gvCreditNote.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                    End If
                Next

                gvCreditNote.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
            If obj.arrClsPaymentProcessMccSaleReturn IsNot Nothing AndAlso obj.arrClsPaymentProcessMccSaleReturn.Count > 0 Then
                GvMccSaleReturn.Rows.Clear()
                For i = 0 To obj.arrClsPaymentProcessMccSaleReturn.Count - 1
                    GvMccSaleReturn.Rows.AddNew()
                    GvMccSaleReturn.Rows(i).Cells(colSlno).Value = (i + 1)
                    GvMccSaleReturn.Rows(i).Cells(colSelect).Value = True
                    GvMccSaleReturn.Rows(i).Cells(colReturnDocNo).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).Return_Doc_No
                    GvMccSaleReturn.Rows(i).Cells(colReturnDocType).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).Return_Doc_Type
                    GvMccSaleReturn.Rows(i).Cells(colReturnDocDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSaleReturn.Item(i).Return_Doc_Date, "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colShipmentNo).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).Shipment_Doc_No
                    GvMccSaleReturn.Rows(i).Cells(colShipmentDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSaleReturn.Item(i).Shipment_Doc_Date, "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colSaleInvNo).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).Sale_Doc_No
                    GvMccSaleReturn.Rows(i).Cells(colSaleInvDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSaleReturn.Item(i).Sale_Doc_Date, "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colARInvoiceNo).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).AR_Invoice_No
                    GvMccSaleReturn.Rows(i).Cells(colARInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSaleReturn.Item(i).AR_Invoice_Date, "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colCustomerCode).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).Customer_CODE
                    GvMccSaleReturn.Rows(i).Cells(colCustomerName).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).Customer_NAME
                    GvMccSaleReturn.Rows(i).Cells(colItemAmt).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessMccSaleReturn.Item(i).Amount)
                Next
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                GvMccSaleReturn.SummaryRowsBottom.Clear()

                For iii As Integer = 0 To GvMccSaleReturn.Columns.Count - 1
                    If TypeOf (GvMccSaleReturn.Columns(iii)) Is GridViewDecimalColumn Then
                        summaryRowItem.Add(New GridViewSummaryItem(GvMccSaleReturn.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                    End If
                Next

                GvMccSaleReturn.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If

            If obj.arrclsPaymentProcessFarmerMccSaleReturn IsNot Nothing AndAlso obj.arrclsPaymentProcessFarmerMccSaleReturn.Count > 0 Then
                gvMccSaleReturnFarmer.Rows.Clear()
                For i = 0 To obj.arrclsPaymentProcessFarmerMccSaleReturn.Count - 1
                    gvMccSaleReturnFarmer.Rows.AddNew()
                    gvMccSaleReturnFarmer.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvMccSaleReturnFarmer.Rows(i).Cells(colSelect).Value = True
                    gvMccSaleReturnFarmer.Rows(i).Cells(colReturnDocNo).Value = obj.arrclsPaymentProcessFarmerMccSaleReturn.Item(i).Return_Doc_No
                    gvMccSaleReturnFarmer.Rows(i).Cells(colReturnDocType).Value = obj.arrclsPaymentProcessFarmerMccSaleReturn.Item(i).Return_Doc_Type
                    gvMccSaleReturnFarmer.Rows(i).Cells(colReturnDocDate).Value = clsCommon.GetPrintDate(obj.arrclsPaymentProcessFarmerMccSaleReturn.Item(i).Return_Doc_Date, "dd/MMM/yyyy")
                    gvMccSaleReturnFarmer.Rows(i).Cells(colShipmentNo).Value = obj.arrclsPaymentProcessFarmerMccSaleReturn.Item(i).Shipment_Doc_No
                    gvMccSaleReturnFarmer.Rows(i).Cells(colShipmentDate).Value = clsCommon.GetPrintDate(obj.arrclsPaymentProcessFarmerMccSaleReturn.Item(i).Shipment_Doc_Date, "dd/MMM/yyyy")
                    gvMccSaleReturnFarmer.Rows(i).Cells(colSaleInvNo).Value = obj.arrclsPaymentProcessFarmerMccSaleReturn.Item(i).Sale_Doc_No
                    gvMccSaleReturnFarmer.Rows(i).Cells(colSaleInvDate).Value = clsCommon.GetPrintDate(obj.arrclsPaymentProcessFarmerMccSaleReturn.Item(i).Sale_Doc_Date, "dd/MMM/yyyy")
                    gvMccSaleReturnFarmer.Rows(i).Cells(colFarmerCode).Value = obj.arrclsPaymentProcessFarmerMccSaleReturn.Item(i).Farmer_Code
                    gvMccSaleReturnFarmer.Rows(i).Cells(colFarmerName).Value = obj.arrclsPaymentProcessFarmerMccSaleReturn.Item(i).Farmer_Name
                    gvMccSaleReturnFarmer.Rows(i).Cells(colVendorCode).Value = obj.arrclsPaymentProcessFarmerMccSaleReturn.Item(i).VSP_CODE
                    gvMccSaleReturnFarmer.Rows(i).Cells(colVendorDesc).Value = obj.arrclsPaymentProcessFarmerMccSaleReturn.Item(i).VSP_NAME
                    gvMccSaleReturnFarmer.Rows(i).Cells(colItemAmt).Value = clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerMccSaleReturn.Item(i).Amount)
                Next
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                gvMccSaleReturnFarmer.SummaryRowsBottom.Clear()

                For iii As Integer = 0 To gvMccSaleReturnFarmer.Columns.Count - 1
                    If TypeOf (gvMccSaleReturnFarmer.Columns(iii)) Is GridViewDecimalColumn Then
                        summaryRowItem.Add(New GridViewSummaryItem(gvMccSaleReturnFarmer.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                    End If
                Next

                gvMccSaleReturnFarmer.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
            LoadBlankGridGV()


            If obj.ArrPPAdvancePayment IsNot Nothing AndAlso obj.ArrPPAdvancePayment.Count > 0 Then
                gvAdvancePayment.Rows.Clear()
                For i = 0 To obj.ArrPPAdvancePayment.Count - 1
                    gvAdvancePayment.Rows.AddNew()
                    gvAdvancePayment.Rows(i).Cells(colAPSNo).Value = (i + 1)
                    gvAdvancePayment.Rows(i).Cells(colAPSelect).Value = True
                    gvAdvancePayment.Rows(i).Cells(colAPVendorCode).Value = obj.ArrPPAdvancePayment.Item(i).Vendor_Code
                    gvAdvancePayment.Rows(i).Cells(colAPVendorName).Value = obj.ArrPPAdvancePayment.Item(i).Vendor_Name
                    gvAdvancePayment.Rows(i).Cells(colAPPaymentCode).Value = obj.ArrPPAdvancePayment.Item(i).Payment_No
                    gvAdvancePayment.Rows(i).Cells(colAPPaymentDate).Value = obj.ArrPPAdvancePayment.Item(i).Payment_Date
                    gvAdvancePayment.Rows(i).Cells(colAPPaymentAmt).Value = obj.ArrPPAdvancePayment.Item(i).Payment_Amount
                    gvAdvancePayment.Rows(i).Cells(colAPPaymentAmtBalance).Value = obj.ArrPPAdvancePayment.Item(i).Payment_Balance
                Next
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                gvAdvancePayment.SummaryRowsBottom.Clear()

                For iii As Integer = 0 To gvAdvancePayment.Columns.Count - 1
                    If TypeOf (gvAdvancePayment.Columns(iii)) Is GridViewDecimalColumn Then
                        summaryRowItem.Add(New GridViewSummaryItem(gvAdvancePayment.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                    End If
                Next

                gvAdvancePayment.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
            '' farmer payment grid   
            LoadBlankGridGVFarmer()
            If obj.ArrPPDetail IsNot Nothing AndAlso obj.ArrPPDetail.Count > 0 Then
                'arr = New ArrayList
                For i = 0 To obj.ArrPPDetail.Count - 1

                    gvPaymentToFarmer.Rows.AddNew()
                    gvPaymentToFarmer.Rows(i).Cells(colSlno).Value = obj.ArrPPDetail.Item(i).SNo
                    gvPaymentToFarmer.Rows(i).Cells(colSelect).Value = obj.ArrPPDetail.Item(i).Is_select
                    gvPaymentToFarmer.Rows(i).Cells(colIsPaymentProcessHold).Value = obj.ArrPPDetail.Item(i).is_Hold_Payment_Process
                    gvPaymentToFarmer.Rows(i).Cells(colPurchaseInvoiceNo).Value = obj.ArrPPDetail.Item(i).Farmer_Invoice_No
                    gvPaymentToFarmer.Rows(i).Cells(colPurchaseInvoiceDate).Value = obj.ArrPPDetail.Item(i).Farmer_Invoice_Date
                    gvPaymentToFarmer.Rows(i).Cells(colAPInvoiceNo).Value = obj.ArrPPDetail.Item(i).AP_Invoice_No
                    gvPaymentToFarmer.Rows(i).Cells(colAPInvoiceDate).Value = obj.ArrPPDetail.Item(i).AP_Invoice_Date

                    gvPaymentToFarmer.Rows(i).Cells(colAPAdjustmentNo).Value = obj.ArrPPDetail.Item(i).AP_Adjustment_No
                    gvPaymentToFarmer.Rows(i).Cells(colAPAdjustmentDate).Value = obj.ArrPPDetail.Item(i).AP_Adjustment_Date

                    gvPaymentToFarmer.Rows(i).Cells(colVLCUploaderCode).Value = obj.ArrPPDetail.Item(i).VLC_CODE_Uploader
                    gvPaymentToFarmer.Rows(i).Cells(colVLCCode).Value = clsDBFuncationality.getSingleValue("select VLC_Code from TSPL_VLC_MASTER_HEAD where VSP_Code='" + obj.ArrPPDetail.Item(i).VSP_CODE + "' ")
                    gvPaymentToFarmer.Rows(i).Cells(colVLCName).Value = obj.ArrPPDetail.Item(i).VLC_Name
                    gvPaymentToFarmer.Rows(i).Cells(colVendorCode).Value = obj.ArrPPDetail.Item(i).VSP_CODE
                    gvPaymentToFarmer.Rows(i).Cells(colVendorDesc).Value = obj.ArrPPDetail.Item(i).VSP_NAME
                    gvPaymentToFarmer.Rows(i).Cells(colFarmerCode).Value = obj.ArrPPDetail.Item(i).Farmer_Code
                    SetMPUploaderCode(i)
                    gvPaymentToFarmer.Rows(i).Cells(colFarmerName).Value = obj.ArrPPDetail.Item(i).Farmer_Name
                    gvPaymentToFarmer.Rows(i).Cells(colPayeeJointName).Value = obj.ArrPPDetail.Item(i).Payee_Joint_Name
                    gvPaymentToFarmer.Rows(i).Cells(colPayeeJointBankCode).Value = obj.ArrPPDetail.Item(i).Payee_Joint_Bank_Code
                    gvPaymentToFarmer.Rows(i).Cells(colPayeeJointBankDesc).Value = obj.ArrPPDetail.Item(i).Payee_Joint_Bank_Name
                    gvPaymentToFarmer.Rows(i).Cells(colPayeeJointBranchCode).Value = obj.ArrPPDetail.Item(i).Payee_Joint_Branch_Code
                    gvPaymentToFarmer.Rows(i).Cells(colPayeeJointBranchDesc).Value = obj.ArrPPDetail.Item(i).Payee_Joint_Branch_Name
                    gvPaymentToFarmer.Rows(i).Cells(colPayeeJointAcNo).Value = obj.ArrPPDetail.Item(i).Payee_Joint_Account_No
                    gvPaymentToFarmer.Rows(i).Cells(colPayeeJointIFSC).Value = obj.ArrPPDetail.Item(i).Payee_Joint_IFSC_Code
                    gvPaymentToFarmer.Rows(i).Cells(colBankCode).Value = obj.ArrPPDetail.Item(i).Bank_Code
                    gvPaymentToFarmer.Rows(i).Cells(colBankDesc).Value = obj.ArrPPDetail.Item(i).Bank_Desc
                    gvPaymentToFarmer.Rows(i).Cells(colPayMode).Value = obj.ArrPPDetail.Item(i).Payment_Mode
                    gvPaymentToFarmer.Rows(i).Cells(colChequeNo).Value = obj.ArrPPDetail.Item(i).Cheque_No
                    If clsCommon.CompairString(obj.ArrPPDetail.Item(i).Payment_Mode, "Cheque") = CompairStringResult.Equal Then
                        gvPaymentToFarmer.Rows(i).Cells(colChequeDate).Value = obj.ArrPPDetail.Item(i).Cheque_Dated
                    Else
                        gvPaymentToFarmer.Rows(i).Cells(colChequeDate).Value = Nothing
                    End If

                    gvPaymentToFarmer.Rows(i).Cells(colMilkQty).Value = obj.ArrPPDetail.Item(i).Milk_Qty

                    gvPaymentToFarmer.Rows(i).Cells(colMPAmount).Value = obj.ArrPPDetail.Item(i).Milk_Amount

                    gvPaymentToFarmer.Rows(i).Cells(colMccSaleTotalAmount).Value = obj.ArrPPDetail.Item(i).MCC_Sale_Amount
                    gvPaymentToFarmer.Rows(i).Cells(colMccSaleReturnTotalAmount).Value = obj.ArrPPDetail.Item(i).MCC_Sale_Return_Amount
                    gvPaymentToFarmer.Rows(i).Cells(colMPAdjustAmount).Value = obj.ArrPPDetail.Item(i).MP_Adjust_Amount

                    gvPaymentToFarmer.Rows(i).Cells(colIncentiveAmt).Value = obj.ArrPPDetail.Item(i).Incentive_Amount
                    gvPaymentToFarmer.Rows(i).Cells(colDeductionAmt).Value = obj.ArrPPDetail.Item(i).Deduction_Amount

                    gvPaymentToFarmer.Rows(i).Cells(colPaybleAmt).Value = obj.ArrPPDetail.Item(i).Payable_Amount
                    gvPaymentToFarmer.Rows(i).Cells(colNextCycleDebitNoteFarmer).Value = obj.ArrPPDetail.Item(i).NextCycleDebitNoteMP

                    gvPaymentToFarmer.Rows(i).Cells(colFATotalAdvance).Value = obj.ArrPPDetail.Item(i).Total_Advance_Amount
                    gvPaymentToFarmer.Rows(i).Cells(colFATotalAdvanceRecovery).Value = obj.ArrPPDetail.Item(i).Total_Advance_Amount_Recovery

                    gvPaymentToFarmer.Rows(i).Cells(colFALoanPayment).Value = obj.ArrPPDetail.Item(i).Total_Loan_Payment
                    gvPaymentToFarmer.Rows(i).Cells(colFALoanPaymentRecovery).Value = obj.ArrPPDetail.Item(i).Total_Loan_Payment_Recovery

                Next
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                gvPaymentToFarmer.SummaryRowsBottom.Clear()

                For iii As Integer = 0 To gvPaymentToFarmer.Columns.Count - 1
                    If TypeOf (gvPaymentToFarmer.Columns(iii)) Is GridViewDecimalColumn Then
                        summaryRowItem.Add(New GridViewSummaryItem(gvPaymentToFarmer.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                    End If
                Next

                gvPaymentToFarmer.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
            '' adjustment
            If obj.ArrPPAdjustment IsNot Nothing AndAlso obj.ArrPPAdjustment.Count > 0 Then
                gvMPAdj.Rows.Clear()
                For i = 0 To obj.ArrPPAdjustment.Count - 1
                    gvMPAdj.Rows.AddNew()
                    gvMPAdj.Rows(i).Cells(colSlno).Value = obj.ArrPPAdjustment.Item(i).SNo
                    gvMPAdj.Rows(i).Cells(colSelect).Value = obj.ArrPPAdjustment.Item(i).Is_select
                    gvMPAdj.Rows(i).Cells(colVendorCode).Value = obj.ArrPPAdjustment.Item(i).VSP_CODE
                    gvMPAdj.Rows(i).Cells(colVendorDesc).Value = obj.ArrPPAdjustment.Item(i).VSP_NAME
                    gvMPAdj.Rows(i).Cells(colFarmerCode).Value = obj.ArrPPAdjustment.Item(i).Farmer_Code
                    gvMPAdj.Rows(i).Cells(colFarmerName).Value = obj.ArrPPAdjustment.Item(i).Farmer_Name
                    gvMPAdj.Rows(i).Cells(colAdjNo).Value = obj.ArrPPAdjustment.Item(i).Adjustment_No
                    gvMPAdj.Rows(i).Cells(colMPAdjDate).Value = clsCommon.GetPrintDate(obj.ArrPPAdjustment.Item(i).Adjustment_Date, "dd/MMM/yyyy")
                    gvMPAdj.Rows(i).Cells(colMPAdjType).Value = obj.ArrPPAdjustment.Item(i).Adjustment_Type
                    gvMPAdj.Rows(i).Cells(colMPADjDesc).Value = obj.ArrPPAdjustment.Item(i).Desciption
                    gvMPAdj.Rows(i).Cells(colMPAdjRemarks).Value = obj.ArrPPAdjustment.Item(i).Remarks
                    gvMPAdj.Rows(i).Cells(colMPAdjustAmount).Value = obj.ArrPPAdjustment.Item(i).Adjustment_Amount

                Next
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                gvMPAdj.SummaryRowsBottom.Clear()

                For iii As Integer = 0 To gvMPAdj.Columns.Count - 1
                    If TypeOf (gvMPAdj.Columns(iii)) Is GridViewDecimalColumn Then
                        summaryRowItem.Add(New GridViewSummaryItem(gvMPAdj.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                    End If
                Next

                gvMPAdj.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If


            If obj.arrMPAdvance IsNot Nothing AndAlso obj.arrMPAdvance.Count > 0 Then
                gvFA.Rows.Clear()
                For i = 0 To obj.arrMPAdvance.Count - 1
                    gvFA.Rows.AddNew()
                    gvFA.Rows(i).Cells(colFAFarmerCode).Value = obj.arrMPAdvance.Item(i).MP_Code
                    gvFA.Rows(i).Cells(colFAFarmerName).Value = obj.arrMPAdvance.Item(i).MP_Name
                    gvFA.Rows(i).Cells(colFAPaymentCode).Value = obj.arrMPAdvance.Item(i).Payment_No
                    gvFA.Rows(i).Cells(colFAPaymentDate).Value = clsCommon.GetPrintDate(obj.arrMPAdvance.Item(i).Payment_Date, "dd/MMM/yyyy")
                    gvFA.Rows(i).Cells(colFALoan).Value = obj.arrMPAdvance.Item(i).Is_Loan
                    gvFA.Rows(i).Cells(colFAPaymentAmt).Value = obj.arrMPAdvance.Item(i).Payment_Amount
                    gvFA.Rows(i).Cells(colFAKnockOff).Value = obj.arrMPAdvance.Item(i).Knock_Off_Amt
                Next

                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                gvFA.SummaryRowsBottom.Clear()
                For iii As Integer = 0 To gvFA.Columns.Count - 1
                    If TypeOf (gvFA.Columns(iii)) Is GridViewDecimalColumn Then
                        summaryRowItem.Add(New GridViewSummaryItem(gvFA.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                    End If
                Next
                gvFA.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If

            If obj.ArrVSPPPDetail IsNot Nothing AndAlso obj.ArrVSPPPDetail.Count > 0 Then
                Dim arr As New ArrayList()
                For i = 0 To obj.ArrVSPPPDetail.Count - 1
                    If Not arr.Contains(obj.ArrVSPPPDetail.Item(i).VSP_CODE) Then
                        arr.Add(obj.ArrVSPPPDetail.Item(i).VSP_CODE)
                    End If

                    gv.Rows.AddNew()
                    gv.Rows(i).Cells(colSlno).Value = obj.ArrVSPPPDetail.Item(i).SNo
                    gv.Rows(i).Cells(colSelect).Value = obj.ArrVSPPPDetail.Item(i).Is_select
                    gv.Rows(i).Cells(colIsPaymentProcessHold).Value = obj.ArrVSPPPDetail.Item(i).is_Hold_Payment_Process
                    gv.Rows(i).Cells(colPurchaseInvoiceNo).Value = obj.ArrVSPPPDetail.Item(i).Milk_Purchase_Invoice_No
                    gv.Rows(i).Cells(colPurchaseInvoiceDate).Value = obj.ArrVSPPPDetail.Item(i).Milk_Purchase_Invoice_Date
                    gv.Rows(i).Cells(colAPInvoiceNo).Value = obj.ArrVSPPPDetail.Item(i).AP_Invoice_No
                    gv.Rows(i).Cells(colAPInvoiceDate).Value = obj.ArrVSPPPDetail.Item(i).AP_Invoice_Date
                    gv.Rows(i).Cells(colVLCUploaderCode).Value = obj.ArrVSPPPDetail.Item(i).VLC_CODE_Uploader
                    gv.Rows(i).Cells(colVLCCode).Value = clsDBFuncationality.getSingleValue("select VLC_Code from TSPL_VLC_MASTER_HEAD where VSP_Code='" + obj.ArrVSPPPDetail.Item(i).VSP_CODE + "' ")
                    gv.Rows(i).Cells(colVLCName).Value = obj.ArrVSPPPDetail.Item(i).VLC_Name
                    gv.Rows(i).Cells(colVendorCode).Value = obj.ArrVSPPPDetail.Item(i).VSP_CODE
                    gv.Rows(i).Cells(colVendorDesc).Value = obj.ArrVSPPPDetail.Item(i).VSP_NAME
                    gv.Rows(i).Cells(colActualVSPCode).Value = obj.ArrVSPPPDetail.Item(i).Main_VSP_CODE
                    gv.Rows(i).Cells(colActualVSPName).Value = obj.ArrVSPPPDetail.Item(i).Main_VSP_NAME
                    gv.Rows(i).Cells(colPayeeJointName).Value = obj.ArrVSPPPDetail.Item(i).Payee_Joint_Name
                    gv.Rows(i).Cells(colPayeeJointBankCode).Value = obj.ArrVSPPPDetail.Item(i).Payee_Joint_Bank_Code
                    gv.Rows(i).Cells(colPayeeJointBankDesc).Value = obj.ArrVSPPPDetail.Item(i).Payee_Joint_Bank_Name
                    gv.Rows(i).Cells(colPayeeJointBranchCode).Value = obj.ArrVSPPPDetail.Item(i).Payee_Joint_Branch_Code
                    gv.Rows(i).Cells(colPayeeJointBranchDesc).Value = obj.ArrVSPPPDetail.Item(i).Payee_Joint_Branch_Name
                    gv.Rows(i).Cells(colPayeeJointAcNo).Value = obj.ArrVSPPPDetail.Item(i).Payee_Joint_Account_No
                    gv.Rows(i).Cells(colPayeeJointIFSC).Value = obj.ArrVSPPPDetail.Item(i).Payee_Joint_IFSC_Code
                    gv.Rows(i).Cells(colBankCode).Value = obj.ArrVSPPPDetail.Item(i).Bank_Code
                    gv.Rows(i).Cells(colBankDesc).Value = obj.ArrVSPPPDetail.Item(i).Bank_Desc
                    gv.Rows(i).Cells(colPayMode).Value = obj.ArrVSPPPDetail.Item(i).Payment_Mode
                    gv.Rows(i).Cells(colChequeNo).Value = obj.ArrVSPPPDetail.Item(i).Cheque_No
                    If clsCommon.CompairString(obj.ArrVSPPPDetail.Item(i).Payment_Mode, "Cheque") = CompairStringResult.Equal Then
                        gv.Rows(i).Cells(colChequeDate).Value = obj.ArrVSPPPDetail.Item(i).Cheque_Dated
                    Else
                        gv.Rows(i).Cells(colChequeDate).Value = Nothing
                    End If
                    gv.Rows(i).Cells(colPayToFarmer).Value = "1"
                    gv.Rows(i).Cells(colMilkQty).Value = obj.ArrVSPPPDetail.Item(i).Milk_Qty
                    gv.Rows(i).Cells(colVSPAmount).Value = obj.ArrVSPPPDetail.Item(i).VSP_Amount

                    gv.Rows(i).Cells(colMPAmount).Value = obj.ArrVSPPPDetail.Item(i).MP_Amount
                    gv.Rows(i).Cells(colMPEMPAmount).Value = obj.ArrVSPPPDetail.Item(i).MP_EMP
                    gv.Rows(i).Cells(colMPIncentiveAmount).Value = obj.ArrVSPPPDetail.Item(i).MP_Incentive
                    gv.Rows(i).Cells(colMPEMPIncentiveAmount).Value = obj.ArrVSPPPDetail.Item(i).MP_IncentiveEMP
                    gv.Rows(i).Cells(colMPNetAmount).Value = obj.ArrVSPPPDetail.Item(i).MP_Net_Amount

                    gv.Rows(i).Cells(colMPVSPDiffAmount).Value = obj.ArrVSPPPDetail.Item(i).MP_VSP_Diff_Amount
                    gv.Rows(i).Cells(colIncenAmt).Value = obj.ArrVSPPPDetail.Item(i).Incentive_Amount
                    gv.Rows(i).Cells(colEmpAmt).Value = obj.ArrVSPPPDetail.Item(i).EMP_Amount
                    gv.Rows(i).Cells(colIncenEmpAmt).Value = obj.ArrVSPPPDetail.Item(i).Incentive_EMP_Amount
                    gv.Rows(i).Cells(colTotalEmp).Value = obj.ArrVSPPPDetail.Item(i).Total_EMP_Amount
                    gv.Rows(i).Cells(colInvAmt).Value = obj.ArrVSPPPDetail.Item(i).Milk_Amount
                    gv.Rows(i).Cells(colInvAndEmpAmt).Value = obj.ArrVSPPPDetail.Item(i).Incentive_EMP_Amount
                    gv.Rows(i).Cells(colInvAndEmpAmt).Value = obj.ArrVSPPPDetail.Item(i).Total
                    gv.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value = obj.ArrVSPPPDetail.Item(i).Total_Invoice_Amount
                    gv.Rows(i).Cells(colVSPOwnSystemAmt).Value = obj.ArrVSPPPDetail.Item(i).Vsp_Own_System_Amount
                    gv.Rows(i).Cells(colHeadLoadAmt).Value = obj.ArrVSPPPDetail.Item(i).Head_Load_Amount
                    gv.Rows(i).Cells(colInvDeduc).Value = obj.ArrVSPPPDetail.Item(i).Invoice_Deduction_Amount
                    gv.Rows(i).Cells(colReduceDeduc).Value = obj.ArrVSPPPDetail.Item(i).Reduce_Deduc_Amt
                    gv.Rows(i).Cells(colMccSaleTotalAmount).Value = obj.ArrVSPPPDetail.Item(i).MCC_Sale_Amount
                    gv.Rows(i).Cells(colMccSaleReturnTotalAmount).Value = obj.ArrVSPPPDetail.Item(i).MCC_Sale_Return_Amount
                    gv.Rows(i).Cells(colItemIssueTotalAmount).Value = obj.ArrVSPPPDetail.Item(i).Item_Issue_Amount
                    gv.Rows(i).Cells(colItemIssueReturnTotalAmount).Value = obj.ArrVSPPPDetail.Item(i).Item_Issue_Return_Amount
                    gv.Rows(i).Cells(colDeductionTotalAmount).Value = obj.ArrVSPPPDetail.Item(i).Deduction_Amount
                    gv.Rows(i).Cells(colTotalCreditNoteAmount).Value = obj.ArrVSPPPDetail.Item(i).Credit_Note_Amount
                    gv.Rows(i).Cells(colFarmerPayment).Value = CalculateFarmerPayment(gv.Rows(i).Cells(colVendorCode).Value)
                    gv.Rows(i).Cells(colPaybleAmt).Value = obj.ArrVSPPPDetail.Item(i).Payable_Amount
                    gv.Rows(i).Cells(colServiceChargeAmt).Value = obj.ArrVSPPPDetail.Item(i).Service_Charge_Amt

                    gv.Rows(i).Cells(colAdvanceAmount).Value = obj.ArrVSPPPDetail.Item(i).Advance_Payment_Amount
                    gv.Rows(i).Cells(colAdvanceKnockOffAmount).Value = obj.ArrVSPPPDetail.Item(i).Advance_Payment_Amount_Knock_Off

                    gv.Rows(i).Cells(colgvVSPExcessAmount).Value = obj.ArrVSPPPDetail.Item(i).VSP_Excess_Amount
                    gv.Rows(i).Cells(colgvTotMPDeductionAmount).Value = obj.ArrVSPPPDetail.Item(i).MP_Total_Deduction
                    gv.Rows(i).Cells(colNextCycleDebitNote).Value = obj.ArrVSPPPDetail.Item(i).NextCycleDebitNote

                    gv.Rows(i).Cells(colFarmerMilkQty).Value = obj.ArrVSPPPDetail.Item(i).FarmerMilkQty

                    '' new cols
                    gv.Rows(i).Cells(colFarmerTotalSale).Value = obj.ArrVSPPPDetail.Item(i).FarmerSaleAmount
                    gv.Rows(i).Cells(colFarmerTotalSaleReturn).Value = obj.ArrVSPPPDetail.Item(i).FarmerSaleReturnAmount
                    gv.Rows(i).Cells(colFarmerAdjustmentAmountTotal).Value = obj.ArrVSPPPDetail.Item(i).FarmerAdjustmentAmount

                    gv.Rows(i).Cells(colMPAmountPayable).Value = obj.ArrVSPPPDetail.Item(i).FarmerPayableAmount
                    gv.Rows(i).Cells(colPrevCycleBalance).Value = obj.ArrVSPPPDetail.Item(i).PrevCycleDebitNote

                    gv.Rows(i).Cells(colNextCycleDebitNoteFarmer).Value = obj.ArrVSPPPDetail.Item(i).NextCycleDebitNoteMP
                Next
                txtVSP.arrValueMember = arr
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                gv.SummaryRowsBottom.Clear()

                For iii As Integer = 0 To gv.Columns.Count - 1
                    If TypeOf (gv.Columns(iii)) Is GridViewDecimalColumn Then
                        summaryRowItem.Add(New GridViewSummaryItem(gv.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                    End If
                Next

                gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                ReStoreGridLayout()
            Else
                loadGvData()
            End If


            isLoad = False
        Else
            Reset()
        End If
    End Sub

    Private Sub fndDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocNo._MYNavigator
        LoadData(fndDocNo.Value, NavType)
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Try
        '    If AllowToSave() Then
        SaveData(False)
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    Sub PaymentProcess()

        Try
            'frm.StartPosition = FormStartPosition.CenterScreen
            'frm.desc = "Against Payment Process "
            'frm.ShowDialog()
            ' If frm.btnOkClicked Then
            If Not AllowToSave() Then
                Exit Sub
            End If
            GC.Collect()
            GC.WaitForPendingFinalizers()
            SaveData(True)
            If clsCommon.MyMessageBoxShow(Me, "Continue to Process the payment ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                If chkSelected.Checked Then
                    clsPaymentProcessFarmerHead.ProcessDataSelected(fndDocNo.Value, IIf(clsCommon.myLen(txtNEFTUploaderREFNo.Tag) > 0, txtNEFTUploaderREFNo.Tag, frm.desc))
                Else
                    clsPaymentProcessFarmerHead.ProcessData(fndDocNo.Value, IIf(clsCommon.myLen(txtNEFTUploaderREFNo.Tag) > 0, txtNEFTUploaderREFNo.Tag, frm.desc))
                End If


                clsCommon.MyMessageBoxShow(Me, "Payment Processed", Me.Text)

                'LoadData(fndDocNo.Value, NavigatorType.Current)
                btnSave.Enabled = False
                btnProcess.Enabled = False
                btnDelete.Enabled = False
                fndDocNo.MyReadOnly = True
                lblPending.Status = ERPTransactionStatus.Approved

            End If
            ' End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function isItemFoundInList(ByVal arrStr As List(Of String), ByVal str As String) As Boolean

        If arrStr Is Nothing OrElse arrStr.Count <= 0 Then
            Return False
        Else
            For i As Integer = 0 To arrStr.Count - 1
                If clsCommon.CompairString(arrStr.Item(i), str) = CompairStringResult.Equal Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function

    Function isDedFoundInList(ByVal arrStr As List(Of String), ByVal str As String) As Boolean

        If arrStr Is Nothing OrElse arrStr.Count <= 0 Then
            Return False
        Else
            For i As Integer = 0 To arrStr.Count - 1
                If clsCommon.CompairString(arrStr.Item(i), str) = CompairStringResult.Equal Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function

    Sub getDistinctMccSaleItem()
        arrStrMccSaleItemCode = New List(Of String)
        arrStrMccSaleItemDesc = New List(Of String)
        Dim itemCode As String = ""
        Dim itemDes As String = ""
        For i As Integer = 0 To gvMccSale.Rows.Count - 1
            itemCode = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colItemCode).Value)
            itemDes = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colItemDesc).Value)
            If isItemFoundInList(arrStrMccSaleItemCode, itemCode) = False Then
                arrStrMccSaleItemCode.Add(itemCode)
                arrStrMccSaleItemDesc.Add(itemDes)
            End If
        Next

    End Sub

    Sub getDistinctDeduction()
        arrStrDedCode = New List(Of String)
        arrStrDedDesc = New List(Of String)
        Dim dedCode As String = ""
        Dim dedDes As String = ""
        For i As Integer = 0 To gvDeduction.Rows.Count - 1
            dedCode = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colDeductionCode).Value)
            dedDes = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colDeductionDesc).Value)
            If isItemFoundInList(arrStrDedCode, dedCode) = False Then
                arrStrDedCode.Add(dedCode)
                arrStrDedDesc.Add(dedDes)
            End If
        Next

    End Sub

    Sub getDistinctVSPIssueItem()
        arrStrIssueItemCode = New List(Of String)
        arrStrIssueItemDesc = New List(Of String)
        Dim itemCode As String = ""
        Dim itemDes As String = ""
        For i As Integer = 0 To gvItemIssue.Rows.Count - 1
            itemCode = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colItemCode).Value)
            itemDes = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colItemDesc).Value)
            If isItemFoundInList(arrStrIssueItemCode, itemCode) = False Then
                arrStrIssueItemCode.Add(itemCode)
                arrStrIssueItemDesc.Add(itemDes)
            End If
        Next

    End Sub

    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        isLoad = True
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Location segment", Me.Text)
                Exit Sub
            End If
            If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
                txtVSP.Focus()
                Throw New Exception("Please select at lease one VSP ")
            End If
            If chkSelected.Checked Then
                getVendors()
                LoadDeductionGridData()
                loadGvData()
                setVSPExcssAmt()
            Else
                LoadInvoiceGridData()
                If isMultipleDocumentForSameVendor() Then
                    gvInvoice.Rows.Clear()
                    clsCommon.MyMessageBoxShow(Me, "Multiple Invoices For Same vendor Found in selected date range" & Environment.NewLine & "Please select another Date range and continue " & Environment.NewLine & getMultipleDocumentForSameVendor())
                    Exit Sub
                End If
                getVendors()
                LoadMccSaleGridData()
                LoadMccSaleGridDataFarmer()
                LoadMccSaleReturnGridData()
                LoadMccSaleReturnGridDataFarmer()
                LoadAdjustmentGridDataFarmer()
                LoadItemIssueGridData()
                LoadItemIssueReturnGridData()
                LoadDeductionGridData()
                LoadCreditNoteGridData()
                LoadAdvancePaymentGridData()
                LoadBlankGridGV()
                LoadBlankGridGVFarmer()
                loadGvData()
                setVSPExcssAmt()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isLoad = False
        End Try
    End Sub

    Sub setVSPExcssAmt()
        If PayableAmountZeroForMCCSale Then
            For ii As Integer = 0 To gv.Rows.Count - 1
                Dim dblAmt As Double = Math.Round((clsCommon.myCdbl(gv.Rows(ii).Cells(colPaybleAmt).Value)), 2, MidpointRounding.AwayFromZero)
                If dblAmt < 0 Then ''IF amount negative
                    dblAmt = Math.Abs(dblAmt)
                    If dblAmt > 0 Then
                        gv.Rows(ii).Cells(colgvVSPExcessAmount).Value = dblAmt
                        gv.Rows(ii).Cells(colPaybleAmt).Value = (((gv.Rows(ii).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.Rows(ii).Cells(colTotalCreditNoteAmount).Value + gv.Rows(ii).Cells(colVSPOwnSystemAmt).Value + gv.Rows(ii).Cells(colHeadLoadAmt).Value) - gv.Rows(ii).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(ii).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(ii).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(ii).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(ii).Cells(colReduceDeduc).Value) + getTotalMccSaleReturnSum(gv.Rows(ii).Cells(colVendorCode).Value) + getTotalItemIssueReturnSum(gv.Rows(ii).Cells(colVendorCode).Value) - clsCommon.myCdbl(gv.Rows(ii).Cells(colFarmerPayment).Value) + clsCommon.myCdbl(gv.Rows(ii).Cells(colgvVSPExcessAmount).Value + clsCommon.myCdbl(gv.Rows(ii).Cells(colPrevCycleBalance).Value))
                    End If
                End If
            Next
        End If
    End Sub

    Sub loadSaleData(ByVal qry As String, ByVal arrStr As List(Of String))
        Dim qry1 As String = " select amount    from(  " & qry & " ) yyy where 1=1 "
        Dim dblAmt As Double = 0
        Dim whrCls As String = ""
        If arrStr IsNot Nothing AndAlso arrStr.Count > 0 Then
            For i As Integer = 0 To gv.Rows.Count - 1
                For j As Integer = 0 To arrStr.Count - 1
                    whrCls = " and [Item Code] ='" & arrStr.Item(j).ToString & "' and Location='" & gv.Rows(i).Cells("MCC_CODE").Value & "' and vendor_code='" & gv.Rows(i).Cells("Vendor_Code").Value & "' "
                    dblAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry1 & whrCls))
                    gv.Rows(i).Cells(arrStr.Item(j).ToString).Value = dblAmt
                Next
            Next
        End If
    End Sub

    Function getItemSumMccSale(ByVal itemCode As String, ByVal vendorCode As String) As Double
        Dim rValue As Double = 0
        If gvMccSale.Rows.Count > 0 Then
            For i As Integer = 0 To gvMccSale.Rows.Count - 1
                If clsCommon.CompairString(gvMccSale.Rows(i).Cells(colCustomerCode).Value, vendorCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gvMccSale.Rows(i).Cells(colItemCode).Value, itemCode) = CompairStringResult.Equal Then
                    rValue = rValue + clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colItemAmt).Value)
                End If
            Next
        End If
        Return rValue
    End Function

    Function getItemSumItemIssue(ByVal itemCode As String, ByVal vendorCode As String) As Double
        Dim rValue As Double = 0
        If gvItemIssue.Rows.Count > 0 Then
            For i As Integer = 0 To gvItemIssue.Rows.Count - 1
                If clsCommon.CompairString(gvItemIssue.Rows(i).Cells(colVendorCode).Value, vendorCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gvItemIssue.Rows(i).Cells(colItemCode).Value, itemCode) = CompairStringResult.Equal Then
                    rValue = rValue + clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colItemAmt).Value)
                End If
            Next
        End If
        Return rValue
    End Function

    Function getDedSum(ByVal DedCode As String, ByVal vendorCode As String) As Double
        Dim rValue As Double = 0
        If gvDeduction.Rows.Count > 0 Then
            For i As Integer = 0 To gvDeduction.Rows.Count - 1
                If clsCommon.CompairString(gvDeduction.Rows(i).Cells(colVendorCode).Value, vendorCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gvDeduction.Rows(i).Cells(colDeductionCode).Value, DedCode) = CompairStringResult.Equal Then
                    rValue = rValue + clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colItemAmt).Value)
                End If
            Next
        End If
        Return rValue
    End Function

    Sub loadGvData(Optional ByVal LoadGVOnly As Boolean = False)
        LoadBlankGridGV()
        If LoadGVOnly = False Then
            LoadBlankGridGVFarmer()
            loadGvDataFarmer()
        End If

        Dim k As Integer = -1
        Dim VendCustCode As String = ""
        getVendors()
        isEmpOnAmtOnly = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select isnull(TSPL_MCC_MASTER.empOnAmountOnly,0) as empOnAmountOnly  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code =(select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")) = 0, False, True)
        If gvInvoice.Rows.Count > 0 Then
            For i As Integer = 0 To gvInvoice.Rows.Count - 1
                gv.Rows.AddNew()
                k = k + 1
                gv.Rows(k).Cells(colSelect).Value = gvInvoice.Rows(i).Cells(colSelect).Value
                gv.Rows(k).Cells(colSlno).Value = gvInvoice.Rows(i).Cells(colSlno).Value
                gv.Rows(k).Cells(colPurchaseInvoiceNo).Value = gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value
                gv.Rows(k).Cells(colPurchaseInvoiceDate).Value = gvInvoice.Rows(i).Cells(colPurchaseInvoiceDate).Value
                gv.Rows(k).Cells(colAPInvoiceNo).Value = gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value
                gv.Rows(k).Cells(colAPInvoiceDate).Value = gvInvoice.Rows(i).Cells(colAPInvoiceDate).Value
                gv.Rows(k).Cells(colVLCCode).Value = clsDBFuncationality.getSingleValue("select VLC_Code from TSPL_VLC_MASTER_HEAD where VSP_Code='" + clsCommon.myCstr(gvInvoice.Rows(i).Cells(colVendorCode).Value) + "' ")
                gv.Rows(k).Cells(colVLCName).Value = gvInvoice.Rows(i).Cells(colVLCName).Value
                gv.Rows(k).Cells(colVLCUploaderCode).Value = gvInvoice.Rows(i).Cells(colVLCCode).Value
                gv.Rows(k).Cells(colVendorCode).Value = gvInvoice.Rows(i).Cells(colVendorCode).Value
                gv.Rows(k).Cells(colVendorDesc).Value = gvInvoice.Rows(i).Cells(colVendorDesc).Value
                gv.Rows(k).Cells(colPayToFarmer).Value = gvInvoice.Rows(i).Cells(colPayToFarmer).Value
                gv.Rows(k).Cells(colIsPaymentProcessHold).Value = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select is_Hold_Payment_Process  from TSPL_VENDOR_MASTER where Vendor_Code='" + clsCommon.myCstr(gvInvoice.Rows(i).Cells(colVendorCode).Value) + "'")) = 1, True, False)
                gv.Rows(k).Cells(colPayeeJointName).Value = gvInvoice.Rows(i).Cells(colPayeeJointName).Value
                gv.Rows(k).Cells(colPayeeJointBankCode).Value = gvInvoice.Rows(i).Cells(colPayeeJointBankCode).Value
                gv.Rows(k).Cells(colPayeeJointBankDesc).Value = gvInvoice.Rows(i).Cells(colPayeeJointBankDesc).Value
                gv.Rows(k).Cells(colPayeeJointBranchCode).Value = gvInvoice.Rows(i).Cells(colPayeeJointBranchCode).Value
                gv.Rows(k).Cells(colPayeeJointBranchDesc).Value = gvInvoice.Rows(i).Cells(colPayeeJointBranchDesc).Value
                gv.Rows(k).Cells(colPayeeJointAcNo).Value = gvInvoice.Rows(i).Cells(colPayeeJointAcNo).Value
                gv.Rows(k).Cells(colPayeeJointIFSC).Value = gvInvoice.Rows(i).Cells(colPayeeJointIFSC).Value
                gv.Rows(k).Cells(colBankCode).Value = gvInvoice.Rows(i).Cells(colBankCode).Value
                gv.Rows(k).Cells(colBankDesc).Value = gvInvoice.Rows(i).Cells(colBankDesc).Value
                gv.Rows(k).Cells(colPayMode).Value = gvInvoice.Rows(i).Cells(colPayMode).Value
                gv.Rows(k).Cells(colChequeNo).Value = gvInvoice.Rows(i).Cells(colChequeNo).Value

                '' show previous cycle extra amount paid that is to be recovered
                gv.Rows(k).Cells(colPrevCycleBalance).Value = CalculatePrevBalance(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colMilkQty).Value = gvInvoice.Rows(i).Cells(colMilkQty).Value
                gv.Rows(k).Cells(colVSPAmount).Value = gvInvoice.Rows(i).Cells(colInvAmt).Value

                gv.Rows(k).Cells(colMPAmount).Value = gvInvoice.Rows(i).Cells(colMPAmount).Value
                gv.Rows(k).Cells(colMPEMPAmount).Value = gvInvoice.Rows(i).Cells(colMPEMPAmount).Value
                gv.Rows(k).Cells(colMPIncentiveAmount).Value = gvInvoice.Rows(i).Cells(colMPIncentiveAmount).Value
                gv.Rows(k).Cells(colMPEMPIncentiveAmount).Value = gvInvoice.Rows(i).Cells(colMPEMPIncentiveAmount).Value

                gv.Rows(k).Cells(colMPNetAmount).Value = gvInvoice.Rows(i).Cells(colMPAmount).Value + gvInvoice.Rows(i).Cells(colMPEMPAmount).Value + gvInvoice.Rows(i).Cells(colMPIncentiveAmount).Value + gvInvoice.Rows(i).Cells(colMPEMPIncentiveAmount).Value ''calculateMPAmount(clsCommon.myCstr(gv.Rows(k).Cells(colVendorCode).Value), dtpFromDate.Value, dtpToDate.Value, fndLoc.Value)
                gv.Rows(k).Cells(colIncenAmt).Value = gvInvoice.Rows(i).Cells(colIncenAmt).Value
                gv.Rows(k).Cells(colIncenEmpAmt).Value = gvInvoice.Rows(i).Cells(colIncenEmpAmt).Value
                gv.Rows(k).Cells(colInvAmt).Value = gvInvoice.Rows(i).Cells(colInvAmt).Value
                gv.Rows(k).Cells(colEmpAmt).Value = gvInvoice.Rows(i).Cells(colEmpAmt).Value
                If Not isEmpOnAmtOnly Then
                    gv.Rows(k).Cells(colTotalEmp).Value = clsCommon.myCdbl(gv.Rows(k).Cells(colIncenEmpAmt).Value) + clsCommon.myCdbl(gv.Rows(k).Cells(colEmpAmt).Value)
                Else
                    gv.Rows(k).Cells(colTotalEmp).Value = clsCommon.myCdbl(gv.Rows(k).Cells(colEmpAmt).Value)
                End If
                gv.Rows(k).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value = gvInvoice.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value
                gv.Rows(k).Cells(colActualVSPCode).Value = gvInvoice.Rows(i).Cells(colActualVSPCode).Value
                gv.Rows(k).Cells(colActualVSPName).Value = gvInvoice.Rows(i).Cells(colActualVSPName).Value
                gv.Rows(k).Cells(colVSPOwnSystemAmt).Value = gvInvoice.Rows(i).Cells(colVSPOwnSystemAmt).Value
                gv.Rows(k).Cells(colHeadLoadAmt).Value = gvInvoice.Rows(i).Cells(colHeadLoadAmt).Value
                gv.Rows(k).Cells(colInvDeduc).Value = gvInvoice.Rows(i).Cells(colInvDeduc).Value
                gvInvoice.Rows(i).Cells(colReduceDeduc).Value = getTotalDeductionReduceDeduSum(gvInvoice.Rows(i).Cells(colVendorCode).Value) + getTotalMccSaleReduceDeduSum(gvInvoice.Rows(i).Cells(colVendorCode).Value) + getTotalItemIssueReduceDeduSum(gvInvoice.Rows(i).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colReduceDeduc).Value = gvInvoice.Rows(i).Cells(colReduceDeduc).Value
                gv.Rows(k).Cells(colMccSaleTotalAmount).Value = getTotalMccSaleSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colMccSaleReturnTotalAmount).Value = getTotalMccSaleReturnSum(gv.Rows(k).Cells(colVendorCode).Value)

                gv.Rows(k).Cells(colTotalCreditNoteAmount).Value = getTotalCreditNoteSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colItemIssueTotalAmount).Value = getTotalItemIssueSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colItemIssueReturnTotalAmount).Value = getTotalItemIssueReturnSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colDeductionTotalAmount).Value = getTotalDeductionSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colServiceChargeAmt).Value = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colServiceChargeAmt).Value)

                If clsCommon.CompairString(gv.Rows(k).Cells(colPayToFarmer).Value, "1") = CompairStringResult.Equal Then
                    gv.Rows(k).Cells(colFarmerPayment).Value = CalculateFarmerPayment(gv.Rows(k).Cells(colVendorCode).Value) 'gv.Rows(k).Cells(colMPAmount).Value
                Else
                    gv.Rows(k).Cells(colFarmerPayment).Value = 0
                End If

                '' new cplumns
                If clsCommon.CompairString(gv.Rows(k).Cells(colPayToFarmer).Value, "1") = CompairStringResult.Equal Then
                    gv.Rows(k).Cells(colFarmerTotalSale).Value = CalculateFarmerSaleAmount(gv.Rows(k).Cells(colVendorCode).Value)
                Else
                    gv.Rows(k).Cells(colFarmerTotalSale).Value = 0
                End If
                If clsCommon.CompairString(gv.Rows(k).Cells(colPayToFarmer).Value, "1") = CompairStringResult.Equal Then
                    gv.Rows(k).Cells(colFarmerTotalSaleReturn).Value = CalculateFarmerSaleReturnAmount(gv.Rows(k).Cells(colVendorCode).Value)
                Else
                    gv.Rows(k).Cells(colFarmerTotalSaleReturn).Value = 0
                End If
                If clsCommon.CompairString(gv.Rows(k).Cells(colPayToFarmer).Value, "1") = CompairStringResult.Equal Then
                    gv.Rows(k).Cells(colFarmerAdjustmentAmountTotal).Value = CalculateFarmerAdjustmentAmount(gv.Rows(k).Cells(colVendorCode).Value)
                Else
                    gv.Rows(k).Cells(colFarmerAdjustmentAmountTotal).Value = 0
                End If

                If clsCommon.CompairString(gv.Rows(k).Cells(colPayToFarmer).Value, "1") = CompairStringResult.Equal Then
                    gv.Rows(k).Cells(colMPAmountPayable).Value = CalculateFarmerPayableAmount(gv.Rows(k).Cells(colVendorCode).Value) 'gv.Rows(k).Cells(colMPAmount).Value
                    gv.Rows(k).Cells(colNextCycleDebitNoteFarmer).Value = CalculateFarmerNextCycleDebitNote(gv.Rows(k).Cells(colVendorCode).Value)
                Else
                    gv.Rows(k).Cells(colMPAmountPayable).Value = 0
                    gv.Rows(k).Cells(colNextCycleDebitNoteFarmer).Value = 0
                End If

                If clsCommon.CompairString(gv.Rows(k).Cells(colPayToFarmer).Value, "1") = CompairStringResult.Equal Then
                    gv.Rows(k).Cells(colFarmerMilkQty).Value = CalculateFarmerMilkQty(gv.Rows(k).Cells(colVendorCode).Value) 'gv.Rows(k).Cells(colMPAmount).Value
                Else
                    gv.Rows(k).Cells(colFarmerMilkQty).Value = 0
                End If

                gv.Rows(k).Cells(colPaybleAmt).Value = (((gv.Rows(k).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.Rows(k).Cells(colTotalCreditNoteAmount).Value + gv.Rows(k).Cells(colVSPOwnSystemAmt).Value + gv.Rows(k).Cells(colHeadLoadAmt).Value) - gv.Rows(k).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(k).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(k).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(k).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(k).Cells(colReduceDeduc).Value) + getTotalMccSaleReturnSum(gv.Rows(k).Cells(colVendorCode).Value) + getTotalItemIssueReturnSum(gv.Rows(k).Cells(colVendorCode).Value) - clsCommon.myCdbl(gv.Rows(k).Cells(colFarmerPayment).Value) + clsCommon.myCdbl(gv.Rows(k).Cells(colPrevCycleBalance).Value)
                If isConsiderAdvancePayment Then
                    Dim totalPayableAmt As Double = getTotalAdvancePayment(clsCommon.myCstr(gv.Rows(k).Cells(colVendorCode).Value))
                    If totalPayableAmt > 0 Then
                        gv.Rows(k).Cells(colAdvanceAmount).Value = totalPayableAmt
                        If clsCommon.myCdbl(gv.Rows(k).Cells(colAdvanceAmount).Value) > clsCommon.myCdbl(gv.Rows(k).Cells(colPaybleAmt).Value) Then
                            gv.Rows(k).Cells(colAdvanceKnockOffAmount).Value = Math.Round(clsCommon.myCdbl(gv.Rows(k).Cells(colPaybleAmt).Value), 2)
                            gv.Rows(k).Cells(colPaybleAmt).Value = 0
                        ElseIf clsCommon.myCdbl(gv.Rows(k).Cells(colAdvanceAmount).Value) < clsCommon.myCdbl(gv.Rows(k).Cells(colPaybleAmt).Value) Then
                            gv.Rows(k).Cells(colPaybleAmt).Value = Math.Round(clsCommon.myCdbl(gv.Rows(k).Cells(colPaybleAmt).Value) - clsCommon.myCdbl(gv.Rows(k).Cells(colAdvanceAmount).Value), 2, MidpointRounding.ToEven)
                            gv.Rows(k).Cells(colAdvanceKnockOffAmount).Value = Math.Round(clsCommon.myCdbl(gv.Rows(k).Cells(colAdvanceAmount).Value), MidpointRounding.ToEven)
                        ElseIf clsCommon.myCdbl(gv.Rows(k).Cells(colAdvanceAmount).Value) = clsCommon.myCdbl(gv.Rows(k).Cells(colPaybleAmt).Value) Then
                            gv.Rows(k).Cells(colPaybleAmt).Value = 0
                            gv.Rows(k).Cells(colAdvanceKnockOffAmount).Value = Math.Round(clsCommon.myCdbl(gv.Rows(k).Cells(colAdvanceAmount).Value), 2, MidpointRounding.ToEven)
                        End If
                    Else
                        gv.Rows(k).Cells(colAdvanceKnockOffAmount).Value = 0
                        gv.Rows(k).Cells(colAdvanceAmount).Value = 0
                    End If
                End If
            Next
        End If
        ReStoreGridLayout()
        gv.BestFitColumns(BestFitColumnMode.AllCells)
        gv.AllowDeleteRow = False
        gv.AllowRowResize = False
        gv.AllowEditRow = True
        gv.AllowAddNewRow = False
        gv.ShowFilteringRow = True
        gv.ShowGroupPanel = False


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gv.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To gv.Columns.Count - 1
            If TypeOf (gv.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gv.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        isCellValueChanged = False
    End Sub

    Sub SetMPUploaderCode(ByVal k As Integer)
        Dim qry As String = "select Education,case when len(MP_Code_VLC_Uploader)>4 then (select SUBSTRING(MP_Code_VLC_Uploader,len(MP_Code_VLC_Uploader)-3,4)) else MP_Code_VLC_Uploader end as MP_Code_VLC_Uploader  from TSPL_MP_MASTER where mp_code='" + clsCommon.myCstr(gvPaymentToFarmer.Rows(k).Cells(colFarmerCode).Value) + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gvPaymentToFarmer.Rows(k).Cells(colMPUploaderCode).Value = clsCommon.myCstr(dt.Rows(0)("Education"))
            gvPaymentToFarmer.Rows(k).Cells(colMPAccountType).Value = clsCommon.myCstr(dt.Rows(0)("MP_Code_VLC_Uploader"))
        End If
    End Sub

    Sub loadGvDataFarmer()
        LoadBlankGridGVFarmer()
        LoadBlankGridFAAdvancePayment()
        Dim k As Integer = -1
        Dim VendCustCode As String = ""
        'getVendors()
        isEmpOnAmtOnly = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select isnull(TSPL_MCC_MASTER.empOnAmountOnly,0) as empOnAmountOnly  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code =(select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")) = 0, False, True)
        If gvInvoice.Rows.Count > 0 Then
            Dim qry As String = "SELECT top 1 TSPL_MCC_MASTER.MCC_Code FROM TSPL_LOCATION_MASTER INNER JOIN TSPL_MCC_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.MCC_Code " &
                               " WHERE TSPL_LOCATION_MASTER.Loc_Segment_Code='" & fndLoc.Value & "'"
            Dim MCC_CODE As String = clsDBFuncationality.getSingleValue(qry)
            Dim qryMP As String = clsMilkPurchaseInvoiceMCC.GetBaseQueryWithMP(dtpFromDate.Value, dtpToDate.Value, MCC_CODE, txtVSP.arrValueMember)
            ''Type,Doc_No,Doc_Date,File_Date,MCC_Code,Milk_Type,VLC_Fat,VLC_SNF,VLC_Water,Rate,
            qryMP = " SELECT MPData.MP_CODE,MP.MP_Farmer_Billing,MP.VLC_CODE_NEW,MPData.MP_Name,MPData.VLC_CODE,MPData.VLC_Code_VLC_Uploader,Mcc_Code_VLC_Uploader,MP_Code_Uploader,MPData.VSP_CODE," &
                    " TSPL_VENDOR_MASTER.Vendor_Name,Uom_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VENDOR_MASTER.is_Hold_Payment_Process, " &
                    " coalesce(MP.PayeeName,'') as [Payee/Joint Name], MP.BankBranch as [Branch Code], " &
                    " COALESCE(SelfBank.Bank_Name,'') as [Bank Name],COALESCE(MP.BankName,'') as [Bank Code], " &
                    " MP.BankBranch as [Branch Name], MP.IFCICode as [IFSC Code],MP.AccountNO as [AccountNo],Milk_Qty,Milk_Amount,Fat_KG,SNF_KG " &
                    " FROM (select MP_CODE,MP_Name,max(VLC_CODE) as VLC_CODE,max(VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,max(Mcc_Code_VLC_Uploader) as Mcc_Code_VLC_Uploader," &
                    " max(MP_Code_Uploader) as MP_Code_Uploader,max(VSP_CODE) as VSP_CODE,max(Uom_Code) as Uom_Code,sum(VLC_Qty) as Milk_Qty,sum(Fat_KG) as Fat_KG,sum(SNF_KG) as SNF_KG,sum(Amount) as Milk_Amount " &
                    " from (" & qryMP & ") as MPData1 group by MP_CODE,MP_Name) AS MPData " &
                    " left join TSPL_VENDOR_MASTER on MPData.VSP_CODE=TSPL_VENDOR_MASTER.Vendor_Code " &
                    " left join TSPL_VLC_MASTER_HEAD on MPData.VLC_CODE=TSPL_VLC_MASTER_HEAD.VLC_CODE " &
                    " left join (select MP.MP_Code,VLC.VLC_Code AS VLC_CODE_NEW,MP.MP_Farmer_Billing,MP.BankName,MP.PayeeName,MP.BankBranch,MP.IFCICode,MP.AccountNO from TSPL_MP_MASTER MP left join TSPL_VLC_MASTER_HEAD VLC ON MP.VLC_Code=VLC.VLC_CODE) MP on MPData.MP_CODE=MP.MP_CODE " &
                    " left outer join TSPL_Vendor_Bank_MASTER as SelfBank on SelfBank .Bank_Code =MP.BankName where (MP.MP_Farmer_Billing=1 or MP.VLC_CODE_NEW<>MPData.VLC_CODE )  order by MPData.MP_CODE "
            ''MP.MP_Farmer_Billing=1
            ''where MP.MP_Farmer_Billing=1
            Dim dtMP As DataTable = clsDBFuncationality.GetDataTable(qryMP)
            For i As Integer = 0 To gvInvoice.Rows.Count - 1
                For Each drMP As DataRow In dtMP.Select("VSP_Code='" & gvInvoice.Rows(i).Cells(colVendorCode).Value & "'")
                    gvPaymentToFarmer.Rows.AddNew()
                    k = k + 1
                    gvPaymentToFarmer.Rows(k).Cells(colSelect).Value = IIf(clsCommon.myCdbl(drMP.Item("MP_Farmer_Billing")) = 1, True, False) 'gvInvoice.Rows(i).Cells(colSelect).Value
                    If clsCommon.myCBool(gvPaymentToFarmer.Rows(k).Cells(colSelect).Value) = False Then
                        If clsCommon.CompairString(clsCommon.myCstr(drMP.Item("VLC_CODE_NEW")), clsCommon.myCstr(drMP.Item("VLC_CODE"))) = CompairStringResult.Equal Then
                            gvPaymentToFarmer.Rows(k).Cells(colSelect).ReadOnly = True
                        Else
                            gvPaymentToFarmer.Rows(k).Cells(colSelect).ReadOnly = False
                        End If
                    Else
                        gvPaymentToFarmer.Rows(k).Cells(colSelect).ReadOnly = True
                    End If
                    gvPaymentToFarmer.Rows(k).Cells(colSlno).Value = gvPaymentToFarmer.Rows.Count
                    gvPaymentToFarmer.Rows(k).Cells(colPurchaseInvoiceNo).Value = gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value & "/" & clsCommon.myCstr(drMP.Item("MP_CODE"))
                    gvPaymentToFarmer.Rows(k).Cells(colPurchaseInvoiceDate).Value = clsCommon.GetPrintDate(gvInvoice.Rows(i).Cells(colAPInvoiceDate).Value, "dd/MMM/yyyy")

                    gvPaymentToFarmer.Rows(k).Cells(colAPInvoiceNo).Value = gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value
                    gvPaymentToFarmer.Rows(k).Cells(colAPInvoiceDate).Value = gvInvoice.Rows(i).Cells(colAPInvoiceDate).Value

                    gvPaymentToFarmer.Rows(k).Cells(colVLCUploaderCode).Value = clsCommon.myCstr(drMP.Item("VLC_Code_VLC_Uploader"))
                    gvPaymentToFarmer.Rows(k).Cells(colVLCCode).Value = clsCommon.myCstr(drMP.Item("VLC_Code"))
                    gvPaymentToFarmer.Rows(k).Cells(colVLCName).Value = clsCommon.myCstr(drMP.Item("VLC_Name"))
                    gvPaymentToFarmer.Rows(k).Cells(colVendorCode).Value = clsCommon.myCstr(drMP.Item("VSP_Code"))
                    gvPaymentToFarmer.Rows(k).Cells(colVendorDesc).Value = clsCommon.myCstr(drMP.Item("Vendor_Name"))
                    gvPaymentToFarmer.Rows(k).Cells(colFarmerCode).Value = clsCommon.myCstr(drMP.Item("MP_CODE"))
                    SetMPUploaderCode(k)
                    gvPaymentToFarmer.Rows(k).Cells(colFarmerName).Value = clsCommon.myCstr(drMP.Item("MP_Name"))





                    gvPaymentToFarmer.Rows(k).Cells(colIsPaymentProcessHold).Value = IIf(clsCommon.myCdbl(clsCommon.myCstr(drMP.Item("MP_Name"))) = 1, True, False)

                    gvPaymentToFarmer.Rows(k).Cells(colPayeeJointName).Value = clsCommon.myCstr(drMP.Item("Payee/Joint Name"))

                    gvPaymentToFarmer.Rows(k).Cells(colPayeeJointBranchCode).Value = clsCommon.myCstr(drMP.Item("Branch Code"))
                    gvPaymentToFarmer.Rows(k).Cells(colPayeeJointBranchDesc).Value = clsCommon.myCstr(drMP.Item("Branch Name"))
                    gvPaymentToFarmer.Rows(k).Cells(colPayeeJointAcNo).Value = clsCommon.myCstr(drMP.Item("AccountNo"))
                    gvPaymentToFarmer.Rows(k).Cells(colPayeeJointIFSC).Value = clsCommon.myCstr(drMP.Item("IFSC Code"))
                    gvPaymentToFarmer.Rows(k).Cells(colBankCode).Value = clsCommon.myCstr(drMP.Item("Bank Code"))
                    gvPaymentToFarmer.Rows(k).Cells(colBankDesc).Value = clsCommon.myCstr(drMP.Item("Bank Name"))
                    gvPaymentToFarmer.Rows(k).Cells(colPayMode).Value = ""
                    gvPaymentToFarmer.Rows(k).Cells(colChequeNo).Value = ""
                    gvPaymentToFarmer.Rows(k).Cells(colMilkQty).Value = clsCommon.myCdbl(drMP.Item("Milk_Qty"))

                    gvPaymentToFarmer.Rows(k).Cells(colMPAmount).Value = clsCommon.myCdbl(drMP.Item("Milk_Amount"))

                    gvPaymentToFarmer.Rows(k).Cells(colMccSaleTotalAmount).Value = getTotalMccSaleFarmerSum(clsCommon.myCstr(drMP.Item("VSP_Code")), gvPaymentToFarmer.Rows(k).Cells(colFarmerCode).Value)
                    gvPaymentToFarmer.Rows(k).Cells(colMccSaleReturnTotalAmount).Value = getTotalMccSaleReturnFarmerSum(clsCommon.myCstr(drMP.Item("VSP_Code")), gvPaymentToFarmer.Rows(k).Cells(colFarmerCode).Value)
                    gvPaymentToFarmer.Rows(k).Cells(colMPAdjustAmount).Value = getTotalFarmerAdjustmentSum(clsCommon.myCstr(drMP.Item("VSP_Code")), gvPaymentToFarmer.Rows(k).Cells(colFarmerCode).Value)

                    Dim incentive As ArrayList = CalculateIncetiveAmount(MCC_CODE, clsCommon.myCstr(drMP.Item("MP_CODE")), dtpFromDate.Value, dtpToDate.Value, Nothing, "", 0, "", 0)
                    Dim TotIncAmt As Decimal = 0
                    If incentive.Count > 0 Then
                        If incentive(1) > 0 Then
                            TotIncAmt = Math.Round(clsCommon.myCdbl(incentive(1)), 2, MidpointRounding.ToEven)
                        End If
                    End If
                    gvPaymentToFarmer.Rows(k).Cells(colIncentiveAmt).Value = TotIncAmt

                    LoadMPAdvancePayment(clsCommon.myCstr(drMP.Item("MP_CODE")), k)
                    CalculatePayableAmt(k)
                Next
            Next
        End If
        ReStoreGridLayout()
        gvPaymentToFarmer.BestFitColumns(BestFitColumnMode.AllCells)
        gvPaymentToFarmer.AllowDeleteRow = False
        gvPaymentToFarmer.AllowRowResize = False
        gvPaymentToFarmer.AllowEditRow = True
        gvPaymentToFarmer.AllowAddNewRow = False
        gvPaymentToFarmer.ShowFilteringRow = True
        gvPaymentToFarmer.ShowGroupPanel = False


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gvPaymentToFarmer.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To gvPaymentToFarmer.Columns.Count - 1
            If TypeOf (gvPaymentToFarmer.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gvPaymentToFarmer.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next

        gvPaymentToFarmer.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        isCellValueChanged = False
    End Sub

    Sub LoadMPAdvancePayment(ByVal strMPCode As String, ByVal PaymentToFarmerIndex As Integer)
        Dim TotAdvance As Decimal = 0
        Dim TotLoanPayment As Decimal = 0
        If clsCommon.myLen(strMPCode) > 0 Then
            Dim dtToDateForQry As DateTime = dtpToDate.Value
            If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotConsiderTheFutureDateOfAdvancePayment, clsFixedParameterCode.DoNotConsiderTheFutureDateOfAdvancePayment, Nothing)) = 0) Then
                dtToDateForQry = clsCommon.GETSERVERDATE()
            End If

            Dim qry As String = "  select max(MP_Code_For_Advance) as MP_Code_For_Advance,max(MP_Name) as MP_Name,Payment_No as Payment_No,max(Payment_Date) as Payment_Date,sum(Payment_Amount*RI)*max(case when isReceipt=1 then -1 else 1 end) as Payment_Amount,max(isFarmerLoanPayment) as isFarmerLoanPayment" + Environment.NewLine +
            " from ( Select TSPL_PAYMENT_HEADER.MP_Code_For_Advance,TSPL_MP_MASTER.MP_Name,TSPL_PAYMENT_HEADER.Payment_No,TSPL_PAYMENT_HEADER.Payment_Date," + Environment.NewLine +
            "TSPL_PAYMENT_HEADER.Payment_Amount+isnull(TDS_Amount ,0) as Payment_Amount ,TSPL_PAYMENT_HEADER.isFarmerLoanPayment,1 as RI,1 as Chk,TSPL_PAYMENT_HEADER.isReceipt  " + Environment.NewLine +
            "from TSPL_PAYMENT_HEADER" + Environment.NewLine +
            "inner join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code=TSPL_PAYMENT_HEADER.MP_Code_For_Advance  WHERE   MP_Code_For_Advance is not null  and Posted='1' " + Environment.NewLine +
            " AND MP_Code_For_Advance in ('" + strMPCode + "') "
            qry += " AND   IsChkReverse='N' and Payment_Type IN ('MI') "
            If chkSkipPreviousDocumentOfAdvancePayment.Checked Then
                qry += " and TSPL_PAYMENT_HEADER.Payment_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "
            End If
            qry += " and TSPL_PAYMENT_HEADER.Payment_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDateForQry), "dd/MMM/yyyy hh:mm tt") + "' " &
            "  union all " &
            " select null as MP_CODE,'' as MP_Name, TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE.Payment_No,null as Payment_Date,Knock_Off_Amt as Payment_Amount,0 as isFarmerLoanPayment ,-1 as RI,0 as Chk,0 as isReceipt from TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE where  TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE.Doc_No not in ('" + fndDocNo.Value + "') " &
            " )xx group by Payment_No having sum(RI*Payment_Amount)>0 and sum(Chk)>0 "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvFA.Rows.AddNew()
                    gvFA.Rows(gvFA.Rows.Count - 1).Cells(colFAFarmerCode).Value = clsCommon.myCstr(dt.Rows(i)("MP_Code_For_Advance"))
                    gvFA.Rows(gvFA.Rows.Count - 1).Cells(colFAFarmerName).Value = clsCommon.myCstr(dt.Rows(i)("MP_Name"))
                    gvFA.Rows(gvFA.Rows.Count - 1).Cells(colFAPaymentCode).Value = clsCommon.myCstr(dt.Rows(i)("Payment_No"))
                    gvFA.Rows(gvFA.Rows.Count - 1).Cells(colFAPaymentDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Payment_Date"), "dd/MM/yyyy")
                    gvFA.Rows(gvFA.Rows.Count - 1).Cells(colFAPaymentAmt).Value = clsCommon.myCdbl(dt.Rows(i)("Payment_Amount"))
                    If clsCommon.myCdbl(dt.Rows(i)("isFarmerLoanPayment")) = 1 Then
                        gvFA.Rows(gvFA.Rows.Count - 1).Cells(colFALoan).Value = True
                        TotLoanPayment += clsCommon.myCdbl(dt.Rows(i)("Payment_Amount"))
                    Else
                        gvFA.Rows(gvFA.Rows.Count - 1).Cells(colFALoan).Value = False
                        TotAdvance += clsCommon.myCdbl(dt.Rows(i)("Payment_Amount"))
                    End If
                Next
            End If
        End If

        gvPaymentToFarmer.Rows(PaymentToFarmerIndex).Cells(colFATotalAdvance).Value = TotAdvance
        gvPaymentToFarmer.Rows(PaymentToFarmerIndex).Cells(colFATotalAdvanceRecovery).Value = TotAdvance
        gvPaymentToFarmer.Rows(PaymentToFarmerIndex).Cells(colFALoanPayment).Value = TotLoanPayment
    End Sub

    Public Shared Function CalculateIncetiveAmount(ByVal MCC_Code As String, ByVal MPCode As String, ByVal dtFromDate As Date, ByVal dtToDate As Date, ByVal trans As SqlTransaction, ByRef strIncentiveCode As String, ByRef dclIncentiveQty As Decimal, ByRef strIncentiveUOM As String, ByRef dclIncentiveRate As Decimal) As ArrayList
        Dim days_count As Integer = DateTime.DaysInMonth(dtFromDate.Year, dtFromDate.Month)
        Dim ArrReturn As New ArrayList
        Dim Qty As Double = 0
        Dim qry As String = " select TSPL_MP_INCENTIVE.TR_Code, TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE,TSPL_INCENTIVE_MASTER_HEAD.START_DATE,TSPL_INCENTIVE_MASTER_HEAD.End_Date,SCHEME_FOR,Calc_Type,rate_type,TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_TYPE  ,Starting_Shift,ending_shift,Qty_Type  from TSPL_MP_INCENTIVE " + Environment.NewLine +
         "inner join TSPL_INCENTIVE_MASTER_HEAD on TSPL_MP_INCENTIVE.INCENTIVE_CODE=TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE" + Environment.NewLine +
         "where MP_code='" + MPCode + "'"
        Dim DtIncentiveMaster As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If DtIncentiveMaster.Rows.Count <= 0 Then
            Return ArrReturn
            Exit Function
        End If

        If DtIncentiveMaster.Rows(0).Item("Scheme_For") = "PC" Then
            For Each Incrow As DataRow In DtIncentiveMaster.Rows()
                qry = "select distinct CAST(0 as bit) as Sel,code,convert(date,Final.DOC_DATE,103) AS DOC_DATE,DENSE_RANK() over (order by convert(date,Final.DOC_DATE,103)) as Date_Day,MONTH(convert(date,Final.DOC_DATE,103)) as Date_Month,Year(convert(date,Final.DOC_DATE,103)) as Date_Year,Final.MCC_code,Final.MP_Code,max(MP_Name) as MP_Name,Unit ,sum(Qty) as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end)  as GRNQty,SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,0 as Assessable,max(Amount) as Amount,FAT_PER,SNF_PER,CLR,FAT_KG,SNF_KG,cans,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Correction_factor,Final.shift,0 as Commission,0 as Payment_Commission,0.00 as Incentive_value from (" + Environment.NewLine +
                "select doc_no as Code,doc_date as DOC_DATE,x.MCC_code,x.VLC_Code,x.MP_CODE,x.MP_Name,x.VLC_Qty as Qty" + Environment.NewLine +
                ",0 as Unapproved,x.UOM_Code as Unit,1 as RI,x.Rate ,1 as Chk  ,x.Amount,x.VLC_Fat as FAT_PER,x.VLC_SNF as SNF_PER,0 as cans,0  as CLR,x.Route_No as   Route_Code  ,'' as route_name,'' as  VEHICLE_CODE,'' as Vehicle_Name,0 as Correction_factor,case when x.SHIFT='M' then 'Morning' else 'Evening' end as shift  ,TSPL_INCENTIVE_MASTER_HEAD.Qty_Type,x.FAT_KG,x.SNF_KG " + Environment.NewLine +
                "from (" + Environment.NewLine
                qry += clsMilkPurchaseInvoiceMCC.GetBaseQueryWithMP(dtFromDate, dtToDate, MCC_Code, Nothing)
                qry += Environment.NewLine + ")x" + Environment.NewLine +
              "inner join TSPL_MP_INCENTIVE on TSPL_MP_INCENTIVE.TR_Code='" + clsCommon.myCstr(Incrow("TR_Code")) + "' " + Environment.NewLine +
              "Inner join TSPL_INCENTIVE_MASTER_HEAD on TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE= TSPL_MP_INCENTIVE.INCENTIVE_CODE and TSPL_INCENTIVE_MASTER_HEAD.start_date<=convert(date,x.Doc_Date,103) and TSPL_INCENTIVE_MASTER_HEAD.End_date>=convert(date,x.Doc_Date,103) " + Environment.NewLine +
              "where x.mp_code='" + MPCode + "'" + Environment.NewLine +
              ")Final " + Environment.NewLine +
              "group by Code,Final.DOC_DATE,Final.MCC_code,Unit,Final.mp_code,FAT_PER,SNF_PER,CLR,FAT_KG,SNF_KG,cans,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Correction_factor,Final.shift  " + Environment.NewLine +
              "having SUM(Chk)>0 and   (SUM(Qty*RI) <>0 or (SUM(Qty*RI)=0  and (SUM((Qty *RI)- Unapproved)<>0 )))             " + Environment.NewLine +
              "order by Code "

                Dim dtAllData As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtAllData.Rows.Count <= 0 Then
                    Continue For
                End If
                If clsCommon.CompairString(Incrow.Item("Starting_Shift"), "E") = CompairStringResult.Equal Or dtAllData.Select("Doc_Date<'" & Incrow.Item("Start_date") & "'").Count > 0 Then
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date='" & Incrow.Item("Start_date") & "' and shift='Morning'")
                        dtAllData.Rows.Remove(row1)
                    Next
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date<'" & Incrow.Item("Start_date") & "'")
                        dtAllData.Rows.Remove(row1)
                    Next
                End If
                If clsCommon.CompairString(Incrow.Item("Ending_Shift"), "M") = CompairStringResult.Equal Or dtAllData.Select("Doc_Date>'" & Incrow.Item("End_date") & "'").Count > 0 Then
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date='" & Incrow.Item("End_date") & "' and shift='Evening'")
                        dtAllData.Rows.Remove(row1)
                    Next
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date>'" & Incrow.Item("End_date") & "'")
                        dtAllData.Rows.Remove(row1)
                    Next
                End If
                Dim Dtincentive As DataTable = clsMilkPurchaseInvoiceMCC.GetMPIncentive(MCC_Code, MPCode, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                '' calculate quality incentive
                If clsCommon.CompairString(Incrow.Item("INCENTIVE_TYPE"), "QLTY") = CompairStringResult.Equal Then
                    ArrReturn.Add(0)
                    clsMilkPurchaseInvoiceMCC.Calculate_Quality_Incentive(dtAllData, Nothing, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                    Continue For
                End If
                Dim DaysSetting As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.NoOfDaysForMultiInceForSameVSPForSamePayCycle, clsFixedParameterCode.NoOfDaysForMultiInceForSameVSPForSamePayCycle, trans))
                If DaysSetting = 1 Then
                    days_count = clsCommon.myCdbl(dtAllData.Compute("Max(Date_Day)", ""))
                End If
                For Each row As DataRow In Dtincentive.Rows()
                    strIncentiveCode = clsCommon.myCstr(row("INCENTIVE_CODE"))
                    dclIncentiveRate = clsCommon.myCdbl(row("Rate"))
                    strIncentiveUOM = clsCommon.myCdbl(row("RATE_UOM"))
                    dclIncentiveQty = clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                    If clsCommon.CompairString(clsCommon.myCstr(row.Item("Calc_TYPE")), "A") = CompairStringResult.Equal Then '================Avg. Calculation============Payment Cycle================
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "F") = CompairStringResult.Equal Then '================FAT Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "Q") = CompairStringResult.Equal Then '================Quantitative Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans)
                                    Exit For
                                End If
                            End If
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Calc_TYPE")), "F") = CompairStringResult.Equal Then '================Flat Calculation============Payment Cycle================
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "F") = CompairStringResult.Equal Then '================FAT Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "Q") = CompairStringResult.Equal Then '================Quantitative Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)

                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans)
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans)
                                    Exit For
                                End If
                            End If
                        End If
                    End If
                Next
            Next
        End If
        If ArrReturn.Count > 2 Then
            Dim counter As Integer = 1
            Dim Incentive_Value As Double = 0
            For Each row As String In ArrReturn
                If counter > 2 And counter Mod 2 = 0 Then
                    Incentive_Value += clsCommon.myCdbl(row)
                End If
                counter += 1
            Next
            ArrReturn(1) += Incentive_Value
        End If
        Return ArrReturn

    End Function

    Sub CalculatePayableAmt(ByVal RowID As Integer)
        gvPaymentToFarmer.Rows(RowID).Cells(colNextCycleDebitNoteFarmer).Value = 0
        gvPaymentToFarmer.Rows(RowID).Cells(colPaybleAmt).Value = gvPaymentToFarmer.Rows(RowID).Cells(colIncentiveAmt).Value - gvPaymentToFarmer.Rows(RowID).Cells(colDeductionAmt).Value + gvPaymentToFarmer.Rows(RowID).Cells(colMPAmount).Value - gvPaymentToFarmer.Rows(RowID).Cells(colMccSaleTotalAmount).Value + gvPaymentToFarmer.Rows(RowID).Cells(colMccSaleReturnTotalAmount).Value + gvPaymentToFarmer.Rows(RowID).Cells(colMPAdjustAmount).Value - clsCommon.myCdbl(gvPaymentToFarmer.Rows(RowID).Cells(colFATotalAdvanceRecovery).Value) - clsCommon.myCdbl(gvPaymentToFarmer.Rows(RowID).Cells(colFALoanPaymentRecovery).Value)
        If clsCommon.myCdbl(gvPaymentToFarmer.Rows(RowID).Cells(colPaybleAmt).Value) < 0 Then
            gvPaymentToFarmer.Rows(RowID).Cells(colNextCycleDebitNoteFarmer).Value = Math.Abs(gvPaymentToFarmer.Rows(RowID).Cells(colPaybleAmt).Value)
            gvPaymentToFarmer.Rows(RowID).Cells(colPaybleAmt).Value = 0
        End If
    End Sub


    Private Function calculateMPAmount(ByVal strVSPCode As String, ByVal dtFrom As DateTime, ByVal dtTo As DateTime, ByVal strMCC As String) As Double
        Dim retVAL As Double = 0
        Dim qry As String = "select 1 from TSPL_VLC_MAPPING_FOR_MP_MILK_AMOUNT left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_VLC_MAPPING_FOR_MP_MILK_AMOUNT.VLC_Code where TSPL_VLC_MASTER_HEAD.VSP_Code='" + strVSPCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            qry = "select sum(Amount) as Amount from (" + Environment.NewLine + _
            " Select TSPL_VLC_DATA_UPLOADER.Amount " + Environment.NewLine + _
            " from TSPL_VLC_DATA_UPLOADER " + Environment.NewLine + _
            " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.VLC_CODE and TSPL_VLC_MASTER_HEAD.MCC=TSPL_VLC_DATA_UPLOADER.MCC_Code" + Environment.NewLine + _
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VLC_MASTER_HEAD.MCC " + Environment.NewLine + _
            " where File_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm tt") + "' and File_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_LOCATION_MASTER.Loc_Segment_Code='" + strMCC + "' and TSPL_VLC_MASTER_HEAD.VSP_Code='" + strVSPCode + "' " + Environment.NewLine + _
            " union all" + Environment.NewLine + _
            " select  TSPL_VLC_DATA_UPLOADER_DETAIL.Amount from TSPL_VLC_DATA_UPLOADER_DETAIL" + Environment.NewLine + _
            " left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code=TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code" + Environment.NewLine + _
            " left Outer Join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code= TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code" + Environment.NewLine + _
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VLC_MASTER_HEAD.MCC" + Environment.NewLine + _
            " where TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_LOCATION_MASTER.Loc_Segment_Code='" + strMCC + "'" + Environment.NewLine + _
            " and TSPL_VLC_MASTER_HEAD.VSP_Code='" + strVSPCode + "'" + Environment.NewLine + _
            " )xxx"
            retVAL = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)), 2, MidpointRounding.ToEven)
        End If
        Return retVAL
    End Function


    Private Sub fndLoc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLoc._MYValidating
        Dim whrCls As String = " 1=1 "
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
            End If
        End If

        whrCls = whrCls & " and   Rejected_Type='N' and Location_Category='MCC'"
        fndLoc.Value = clsLocation.getLocSegFinder(whrCls, fndLoc.Value, isButtonClicked)
        txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndLoc.Value & "' "))
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            ' fndLoc.Enabled = False
            ' txtLocName.Enabled = False

            If Not isLoad Then
                Dim PaymentCycleType As String = ""
                Dim PaymentCycleValue As Integer = 0
                ' If Not isLoad Then
                If clsCommon.myLen(fndLoc.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please select the Location first", Me.Text)
                    Exit Sub
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select isnull(TSPL_MCC_MASTER.empOnAmountOnly,0) as empOnAmountOnly,TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code =(select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
                    Exit Sub
                End If
                PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
                PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
                isEmpOnAmtOnly = IIf(clsCommon.myCdbl(dt.Rows(0)("empOnAmountOnly")) = 0, False, True)
                If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then

                    If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                        clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        Exit Sub
                    End If
                    dtpToDate.Value = DateAdd(DateInterval.Day, PaymentCycleValue - 1, dtpFromDate.Value)
                    If DatePart(DateInterval.Month, dtpFromDate.Value) <> DatePart(DateInterval.Month, dtpToDate.Value) Then
                        dtpToDate.Value = DateAdd(DateInterval.Month, 1, clsCommon.myCDate("01/" & DatePart(DateInterval.Month, dtpFromDate.Value) & "/" & DatePart(DateInterval.Year, dtpFromDate.Value)))
                        dtpToDate.Value = DateAdd(DateInterval.Day, -1, dtpToDate.Value)

                    End If
                ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                        clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        Exit Sub
                    End If
                    dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
                ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                        clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        Exit Sub
                    End If
                    dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDate.Value)
                ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                    ''ERO/19/06/18-000351 By balwinder.
                    Dim today As Date = dtpFromDate.Value
                    Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                    dtpFromDate.Value = today.AddDays(-dayDiff)
                    dtpToDate.Value = dtpFromDate.Value.AddDays(6)
                End If
                ' End If
            End If
        End If
    End Sub

    Private Sub gvInvoice_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvInvoice.CellValueChanged
        If Not isLoad AndAlso e.Column Is gvInvoice.Columns(colSelect) Then
            isLoad = True
            getVendors()
            LoadDeductionGridData()
            LoadMccSaleGridData()
            LoadMccSaleGridDataFarmer()
            LoadMccSaleReturnGridData()
            LoadMccSaleReturnGridDataFarmer()
            LoadAdjustmentGridDataFarmer()
            LoadItemIssueGridData()
            LoadCreditNoteGridData()
            'LoadBlankGridGV()
            loadGvData()
            isLoad = False
        End If
    End Sub

    Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        PaymentProcess()
    End Sub

    Private Sub fndDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocNo._MYValidating

        fndDocNo.Value = clsPaymentProcessFarmerHead.getFinder("FarmType='PPF'", fndDocNo.Value, isButtonClicked)
        If clsCommon.myLen(fndDocNo.Value) > 0 Then
            LoadData(fndDocNo.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub dtpFromDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFromDate.ValueChanged
    End Sub

    Function getInvoiceRowNo(ByVal vsp As String) As Integer
        Dim rvalue As Integer = 0
        Try
            If gvInvoice IsNot Nothing AndAlso gvInvoice.Rows.Count > 0 And clsCommon.myLen(vsp) > 0 Then
                For i As Integer = 0 To gvInvoice.Rows.Count - 1
                    If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then
                        rvalue = i
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            rvalue = -1
        End Try
        Return rvalue
    End Function

    Function getMainRowNo(ByVal vsp As String) As Integer
        Dim rvalue As Integer = 0
        Try
            If gv IsNot Nothing AndAlso gv.Rows.Count > 0 And clsCommon.myLen(vsp) > 0 Then
                For i As Integer = 0 To gv.Rows.Count - 1
                    If clsCommon.CompairString(gv.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then
                        rvalue = i
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            rvalue = -1
        End Try
        Return rvalue
    End Function

    Private Sub gvDeduction_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvDeduction.CellValueChanged
        If e.Column Is gvDeduction.Columns(colReduceDeduc) Then
            Dim Rownum As Integer = 0
            Dim rownummain As Integer = 0
            Try
                If gvDeduction.CurrentRow.Cells(colReduceDeduc).Value > gvDeduction.CurrentRow.Cells(colItemAmt).Value Then
                    clsCommon.MyMessageBoxShow(Me, "Reduce Deduction can not be more than invoice amount ", Me.Text)
                    gvDeduction.CurrentRow.Cells(colReduceDeduc).Value = 0
                    Exit Sub
                End If
                Rownum = getInvoiceRowNo(gvDeduction.CurrentRow.Cells(colVendorCode).Value)
                rownummain = getMainRowNo(gvDeduction.CurrentRow.Cells(colVendorCode).Value)
                If Rownum <> -1 Then
                    gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value = getTotalDeductionReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalMccSaleReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalItemIssueReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value)

                    If rownummain <> -1 Then
                        gv.Rows(rownummain).Cells(colReduceDeduc).Value = gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value
                        gv.Rows(rownummain).Cells(colPaybleAmt).Value = (((gv.Rows(rownummain).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.Rows(rownummain).Cells(colTotalCreditNoteAmount).Value + gv.Rows(rownummain).Cells(colVSPOwnSystemAmt).Value + gv.Rows(rownummain).Cells(colHeadLoadAmt).Value) - gv.Rows(rownummain).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(rownummain).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.Rows(rownummain).Cells(colVendorCode).Value) - clsCommon.myCdbl(gv.Rows(rownummain).Cells(colFarmerPayment).Value) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colgvVSPExcessAmount).Value)
                    End If
                End If
            Catch ex As Exception
            End Try
        ElseIf e.Column Is gvDeduction.Columns(colSelect) Then
            If Not isLoad Then
                isLoad = True
                loadGvData()
                isLoad = False
            End If
        End If
    End Sub

    Private Sub gvCreditNote_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvCreditNote.CellValueChanged
        If e.Column Is gvCreditNote.Columns(colSelect) Then
            If Not isLoad Then
                isLoad = True
                loadGvData()
                isLoad = False
            End If
        End If
    End Sub

    Private Sub gvMCCSale_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvMccSale.CellValueChanged
        If e.Column Is gvMccSale.Columns(colReduceDeduc) Then
            Dim Rownum As Integer = 0
            Dim rownummain As Integer = 0
            Try
                If gvMccSale.CurrentRow.Cells(colReduceDeduc).Value > gvMccSale.CurrentRow.Cells(colItemAmt).Value Then
                    clsCommon.MyMessageBoxShow(Me, "Reduce Deduction can not be more than invoice amount ", Me.Text)
                    gvMccSale.CurrentRow.Cells(colReduceDeduc).Value = 0
                    Exit Sub
                End If
                Rownum = getInvoiceRowNo(gvMccSale.CurrentRow.Cells(colCustomerCode).Value)
                rownummain = getMainRowNo(gvMccSale.CurrentRow.Cells(colCustomerCode).Value)
                If Rownum <> -1 Then
                    gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value = getTotalDeductionReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalMccSaleReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalItemIssueReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value)
                    If rownummain <> -1 Then
                        gv.Rows(rownummain).Cells(colReduceDeduc).Value = gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value
                        gv.Rows(rownummain).Cells(colPaybleAmt).Value = (((gv.Rows(rownummain).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.Rows(rownummain).Cells(colTotalCreditNoteAmount).Value + gv.Rows(rownummain).Cells(colVSPOwnSystemAmt).Value + gv.Rows(rownummain).Cells(colHeadLoadAmt).Value) - gv.Rows(rownummain).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(rownummain).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.Rows(rownummain).Cells(colVendorCode).Value) - gv.Rows(rownummain).Cells(colFarmerPayment).Value + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colgvVSPExcessAmount).Value)
                    End If
                End If
            Catch ex As Exception
            End Try
        ElseIf e.Column Is gvMccSale.Columns(colSelect) Then
            If Not isLoad Then
                isLoad = True
                loadGvData()
                isLoad = False
            End If
        End If
    End Sub

    Private Sub gvItemIssue_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItemIssue.CellValueChanged
        If e.Column Is gvItemIssue.Columns(colReduceDeduc) Then
            Dim Rownum As Integer = 0
            Dim rownummain As Integer = 0
            Try
                If Not isCellValueChanged Then
                    isCellValueChanged = True
                    If gvItemIssue.CurrentRow.Cells(colReduceDeduc).Value > gvItemIssue.CurrentRow.Cells(colItemAmt).Value Then
                        clsCommon.MyMessageBoxShow("Item Issue : Reduce Deduction can not be more than invoice amount at line no: " & (e.RowIndex + 1) & "")
                        gvItemIssue.CurrentRow.Cells(colReduceDeduc).Value = 0
                        isCellValueChanged = False
                        Exit Sub
                    End If
                    Rownum = getInvoiceRowNo(gvItemIssue.CurrentRow.Cells(colVendorCode).Value)
                    rownummain = getMainRowNo(gvItemIssue.CurrentRow.Cells(colVendorCode).Value)
                    If Rownum <> -1 Then
                        gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value = getTotalDeductionReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalMccSaleReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalItemIssueReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value)
                        If rownummain <> -1 Then
                            gv.Rows(rownummain).Cells(colReduceDeduc).Value = gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value
                            gv.Rows(rownummain).Cells(colPaybleAmt).Value = (((gv.Rows(rownummain).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.Rows(rownummain).Cells(colTotalCreditNoteAmount).Value + gv.Rows(rownummain).Cells(colVSPOwnSystemAmt).Value + gv.Rows(rownummain).Cells(colHeadLoadAmt).Value) - gv.Rows(rownummain).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(rownummain).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.Rows(rownummain).Cells(colVendorCode).Value) - clsCommon.myCdbl(gv.Rows(rownummain).Cells(colFarmerPayment).Value) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colgvVSPExcessAmount).Value)
                        End If
                    End If
                    isCellValueChanged = False
                End If

            Catch ex As Exception
            End Try
        ElseIf e.Column Is gvItemIssue.Columns(colSelect) Then
            If Not isLoad Then
                isLoad = True
                loadGvData()
                isLoad = False
            End If
        End If
        isCellValueChanged = False
    End Sub

    Private Sub gvMccsaleReturn_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles GvMccSaleReturn.CellValueChanged
        If e.Column Is GvMccSaleReturn.Columns(colReduceDeduc) Then
            Dim Rownum As Integer = 0
            Dim rownummain As Integer = 0
            Try
                'If GvMccSaleReturn.CurrentRow.Cells(colReduceDeduc).Value > GvMccSaleReturn.CurrentRow.Cells(colItemAmt).Value Then
                '    clsCommon.MyMessageBoxShow("Reduce Deduction can not be more than invoice amount ")
                '    GvMccSaleReturn.CurrentRow.Cells(colReduceDeduc).Value = 0
                '    Exit Sub
                'End If
                Rownum = getInvoiceRowNo(GvMccSaleReturn.CurrentRow.Cells(colCustomerCode).Value)
                rownummain = getMainRowNo(GvMccSaleReturn.CurrentRow.Cells(colCustomerCode).Value)
                If Rownum <> -1 Then
                    gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value = getTotalDeductionReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalMccSaleReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalItemIssueReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value)
                    If rownummain <> -1 Then
                        gv.Rows(rownummain).Cells(colReduceDeduc).Value = gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value
                        gv.Rows(rownummain).Cells(colPaybleAmt).Value = (((gv.Rows(rownummain).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.Rows(rownummain).Cells(colTotalCreditNoteAmount).Value + gv.Rows(rownummain).Cells(colVSPOwnSystemAmt).Value + gv.Rows(rownummain).Cells(colHeadLoadAmt).Value) - gv.Rows(rownummain).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(rownummain).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.Rows(rownummain).Cells(colVendorCode).Value) - clsCommon.myCdbl(gv.Rows(rownummain).Cells(colFarmerPayment).Value) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colgvVSPExcessAmount).Value)
                    End If
                End If
            Catch ex As Exception
            End Try
        ElseIf e.Column Is GvMccSaleReturn.Columns(colSelect) Then
            If Not isLoad Then
                isLoad = True
                loadGvData()
                isLoad = False
            End If
        End If
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        If gv IsNot Nothing AndAlso gv.Rows.Count > 0 AndAlso gv.CurrentRow.Index >= 0 Then
            If e.Column Is gv.Columns(colMccSaleTotalAmount) Then
                Dim frm As New FrmFreeGrid
                frm.dt = getMccSaleList(gv.CurrentRow.Cells(colVendorCode).Value)
                If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No data Found", Me.Text)
                    Exit Sub
                End If
                frm.strFormName = "MCC Sale List For VSP: " & gv.CurrentRow.Cells(colVendorCode).Value
                frm.ReportID = "MCCSaleDetail"
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If

            If e.Column Is gv.Columns(colMccSaleReturnTotalAmount) Then
                Dim frm As New FrmFreeGrid
                frm.dt = getMccSaleReturnList(gv.CurrentRow.Cells(colVendorCode).Value)
                If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No data Found", Me.Text)
                    Exit Sub
                End If
                frm.strFormName = "MCC Sale Return List For VSP: " & gv.CurrentRow.Cells(colVendorCode).Value
                frm.ReportID = "MCCSaleReturnDetail"
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If

            If e.Column Is gv.Columns(colItemIssueTotalAmount) Then
                Dim frm As New FrmFreeGrid
                frm.dt = getItemIssueList(gv.CurrentRow.Cells(colVendorCode).Value)
                If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No data Found", Me.Text)
                    Exit Sub
                End If
                frm.strFormName = "Item Issue List For VSP: " & gv.CurrentRow.Cells(colVendorCode).Value
                frm.WindowState = FormWindowState.Maximized
                frm.ReportID = "ItemIssueDetail"
                frm.Show()
            End If
            If e.Column Is gv.Columns(colItemIssueReturnTotalAmount) Then
                Dim frm As New FrmFreeGrid
                frm.dt = getItemIssueList(gv.CurrentRow.Cells(colVendorCode).Value)
                If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No data Found", Me.Text)
                    Exit Sub
                End If
                frm.strFormName = "Item Issue Return List For VSP: " & gv.CurrentRow.Cells(colVendorCode).Value
                frm.WindowState = FormWindowState.Maximized
                frm.ReportID = "ItemIssueDetail"
                frm.Show()
            End If

            If e.Column Is gv.Columns(colDeductionTotalAmount) Then
                Dim frm As New FrmFreeGrid
                frm.dt = getDeductionList(gv.CurrentRow.Cells(colVendorCode).Value)
                If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No data Found", Me.Text)
                    Exit Sub
                End If
                frm.strFormName = "Deduction List For VSP: " & gv.CurrentRow.Cells(colVendorCode).Value
                frm.WindowState = FormWindowState.Maximized
                frm.ReportID = "DeductionDetail"
                frm.Show()
            End If

            If e.Column Is gv.Columns(colTotalCreditNoteAmount) Then
                Dim frm As New FrmFreeGrid
                frm.dt = getCreditNoteList(gv.CurrentRow.Cells(colVendorCode).Value)
                If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No data Found", Me.Text)
                    Exit Sub
                End If
                frm.strFormName = "Credit Note List For VSP: " & gv.CurrentRow.Cells(colVendorCode).Value
                frm.WindowState = FormWindowState.Maximized
                frm.ReportID = "CreditNoteDetail"
                frm.Show()
            End If

            If e.Column Is gv.Columns(colAPInvoiceNo) Then
                If clsCommon.myLen(gv.CurrentRow.Cells(colAPInvoiceNo).Value) > 0 Then
                    'Dim frm As New FrmAPInvoiceEntry
                    'frm.strAPInvoice = gv.CurrentRow.Cells(colAPInvoiceNo).Value
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    Dim docno = clsCommon.myCstr(gv.CurrentRow.Cells(colAPInvoiceNo).Value)
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, docno)
                End If
            End If
            If e.Column Is gv.Columns(colPurchaseInvoiceNo) Then
                If clsCommon.myLen(gv.CurrentRow.Cells(colPurchaseInvoiceNo).Value) > 0 Then
                    'Dim frm As New frmMilkPurchaseInvoiceMCC
                    'frmMilkPurchaseInvoiceMCC.strDocumentNo = gv.CurrentRow.Cells(colPurchaseInvoiceNo).Value
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    Dim docno = clsCommon.myCstr(gv.CurrentRow.Cells(colPurchaseInvoiceNo).Value)
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkPurchaseInvoice, docno)
                End If
            End If

        End If
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isLoad Then
            isLoad = True
            If (e.Column Is gv.Columns(colReduceDeduc) OrElse e.Column Is gv.Columns(colMccSaleTotalAmount) OrElse e.Column Is gv.Columns(colMccSaleReturnTotalAmount) OrElse e.Column Is gv.Columns(colItemIssueTotalAmount) OrElse e.Column Is gv.Columns(colItemIssueReturnTotalAmount) OrElse e.Column Is gv.Columns(colDeductionTotalAmount)) AndAlso gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
                gv.CurrentRow.Cells(colPaybleAmt).Value = (((gv.CurrentRow.Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.CurrentRow.Cells(colTotalCreditNoteAmount).Value + gv.CurrentRow.Cells(colVSPOwnSystemAmt).Value + gv.CurrentRow.Cells(colHeadLoadAmt).Value) - gv.CurrentRow.Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.CurrentRow.Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.CurrentRow.Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.CurrentRow.Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.CurrentRow.Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.CurrentRow.Cells(colVendorCode).Value) - clsCommon.myCdbl(gv.CurrentRow.Cells(colFarmerPayment).Value) + clsCommon.myCdbl(gv.CurrentRow.Cells(colgvVSPExcessAmount).Value)
            End If
            isLoad = False
            isLoad = True
            If e.Column Is gv.Columns(colSelect) Then
                If gv.CurrentRow.Cells(colSelect).Value = True Then
                    CheckInvoice(gv.CurrentRow.Cells(colAPInvoiceNo).Value)
                Else
                    UncheckInvoice(gv.CurrentRow.Cells(colAPInvoiceNo).Value)
                End If
            End If
            isLoad = False
        End If
        If Not isLoad Then
            ' isCellValueChanged = False
            If Not isCellValueChanged Then
                isCellValueChanged = True
                If e.Column Is gv.Columns(colBankCode) And e.RowIndex >= 0 Then
                    isCellValueChanged = True
                    openBankcode()
                    isCellValueChanged = False
                End If
                If e.Column Is gv.Columns(colPayMode) And e.RowIndex >= 0 Then
                    isCellValueChanged = True
                    openPaymentMode()
                    isCellValueChanged = False
                End If
            End If
            'isCellValueChanged = False
            Dim Rownum As Integer = getInvoiceRowNo(gv.CurrentRow.Cells(colVendorCode).Value)
            If Rownum <> -1 Then
                If e.Column Is gv.Columns(colBankCode) Then
                    gvInvoice.Rows(Rownum).Cells(colBankCode).Value = gv.CurrentRow.Cells(colBankCode).Value
                End If
                If e.Column Is gv.Columns(colBankDesc) Then
                    gvInvoice.Rows(Rownum).Cells(colBankDesc).Value = gv.CurrentRow.Cells(colBankDesc).Value
                End If
                If e.Column Is gv.Columns(colPayMode) Then
                    gvInvoice.Rows(Rownum).Cells(colPayMode).Value = gv.CurrentRow.Cells(colPayMode).Value
                End If
                If e.Column Is gv.Columns(colChequeNo) Then
                    gvInvoice.Rows(Rownum).Cells(colChequeNo).Value = gv.CurrentRow.Cells(colChequeNo).Value
                End If
            End If
        End If

    End Sub

    Sub openBankcode()
        Dim strWhrclas As String = ""
        Dim Qry As String = clsERPFuncationality.glbankqueryNew(strWhrclas)
        gv.CurrentRow.Cells(colBankCode).Value = clsCommon.ShowSelectForm("BankSlctr@Payment", Qry, "Code", strWhrclas, gv.CurrentRow.Cells(colBankCode).Value, "Code", True)
        gv.CurrentRow.Cells(colBankDesc).Value = connectSql.RunScalar("select description from TSPL_BANK_MASTER where bank_code = '" + gv.CurrentRow.Cells(colBankCode).Value + "'")
        gv.CurrentRow.Cells(colPayMode).Value = ""
        gv.CurrentRow.Cells(colChequeNo).Value = ""
        gv.CurrentRow.Cells(colChequeDate).Value = Nothing


    End Sub

    Sub openPaymentMode()
        Dim strbankcode As String
        If Not String.IsNullOrEmpty(connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + gv.CurrentRow.Cells(colBankCode).Value + "'")) Then
            strbankcode = connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + gv.CurrentRow.Cells(colBankCode).Value + "'")
            If strbankcode.Trim() = "C" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                gv.CurrentRow.Cells(colPayMode).Value = clsCommon.ShowSelectForm("PaymentCode Selector1", Qry1, "PaymentMode", "PAYMENT_TYPE = 'CASH'", gv.CurrentRow.Cells(colPayMode).Value, "PaymentMode", True)
            ElseIf strbankcode.Trim() = "P" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                gv.CurrentRow.Cells(colPayMode).Value = clsCommon.ShowSelectForm("PaymentCode Selector2", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Petty Cash'", gv.CurrentRow.Cells(colPayMode).Value, "PaymentMode", True)
            ElseIf strbankcode = "B" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                gv.CurrentRow.Cells(colPayMode).Value = clsCommon.ShowSelectForm("PaymentCode Selector3", Qry1, "PaymentMode", "PAYMENT_TYPE IN ('Cheque', 'Other','NEFT','RTGS')", gv.CurrentRow.Cells(colPayMode).Value, "PaymentMode", True)
            Else
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                gv.CurrentRow.Cells(colPayMode).Value = clsCommon.ShowSelectForm("PaymentCode Selector4", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Other'", gv.CurrentRow.Cells(colPayMode).Value, "PaymentMode", True)
            End If
            If clsCommon.myLen(gv.CurrentRow.Cells(colPayMode).Value) > 0 Then
                If clsCommon.CompairString(gv.CurrentRow.Cells(colPayMode).Value, "Cheque") = CompairStringResult.Equal Then
                    gv.CurrentRow.Cells(colChequeNo).ReadOnly = False
                Else
                    gv.CurrentRow.Cells(colChequeNo).ReadOnly = True
                End If
                gv.CurrentRow.Cells(colChequeNo).Value = ""
                gv.CurrentRow.Cells(colChequeDate).Value = Nothing
            End If
        End If
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        CheckAll()
    End Sub

    Sub UnCheckAll()
        If gv IsNot Nothing AndAlso gv.ChildRows.Count > 0 Then
            For i As Integer = 0 To gv.ChildRows.Count - 1
                gv.ChildRows(i).Cells(colSelect).Value = False
                UncheckInvoice(gv.ChildRows(i).Cells(colAPInvoiceNo).Value)
            Next
        End If
    End Sub

    Sub CheckAll()
        If gv IsNot Nothing AndAlso gv.ChildRows.Count > 0 Then
            For i As Integer = 0 To gv.ChildRows.Count - 1
                gv.ChildRows(i).Cells(colSelect).Value = True
                CheckInvoice(gv.ChildRows(i).Cells(colAPInvoiceNo).Value)
            Next
        End If
    End Sub

    Private Sub btnUnselectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnselectAll.Click
        UnCheckAll()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
            gv.Columns(colSelect).IsVisible = False
            Dim arr As List(Of String) = New List(Of String)
            arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
            arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
            arr.Add("VSP :" & clsCommon.GetMulcallStringWithComma(txtVSP.arrValueMember))
            clsCommon.MyExportToExcelGrid("Payment Process Details", gv, arr, "Payment Process")
            gv.Columns(colSelect).IsVisible = True
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found to export", Me.Text)
        End If
    End Sub

    Function getVLCUploaderCode(vsp_code As String, Optional trans As SqlTransaction = Nothing) As String
        Dim rValue As String = String.Empty
        Dim qry As String = String.Empty
        Try
            qry = "select VLC_Code_VLC_Uploader  from TSPL_VLC_MASTER_HEAD  where VSP_Code ='" & vsp_code & "'"
            rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Sub UncheckInvoice(ByVal strApInvoiceNo As String)
        If gvInvoice IsNot Nothing AndAlso gvInvoice.Rows.Count > 0 AndAlso clsCommon.myLen(strApInvoiceNo) > 0 Then
            For i As Integer = 0 To gvInvoice.Rows.Count - 1
                If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value, strApInvoiceNo) = CompairStringResult.Equal Then
                    gvInvoice.Rows(i).Cells(colSelect).Value = False

                    For Each grow As GridViewRowInfo In gvPaymentToFarmer.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next

                    '' done by Panch Raj against ticket no:BM00000008937
                    '' unselect mcc sale trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvMccSale.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next

                    For Each grow As GridViewRowInfo In gvMccSaleFarmer.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next
                    '' unselect mcc sale return trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In GvMccSaleReturn.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next

                    For Each grow As GridViewRowInfo In gvMccSaleReturnFarmer.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next

                    '' unselect mcc Item Issue trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvItemIssue.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next

                    '' unselect mcc Item Issue trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvItemIssueReturn.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next

                    '' unselect mcc Deduction trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvDeduction.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next
                    '' unselect mcc Credit trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvCreditNote.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next

                End If
            Next
        End If
    End Sub

    Sub CheckInvoice(ByVal strApInvoiceNo As String)
        If gvInvoice IsNot Nothing AndAlso gvInvoice.Rows.Count > 0 AndAlso clsCommon.myLen(strApInvoiceNo) > 0 Then
            For i As Integer = 0 To gvInvoice.Rows.Count - 1
                If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value, strApInvoiceNo) = CompairStringResult.Equal Then
                    gvInvoice.Rows(i).Cells(colSelect).Value = True

                    For Each grow As GridViewRowInfo In gvPaymentToFarmer.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next

                    '' done by Panch Raj against ticket no:BM00000008937
                    '' unselect mcc sale trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvMccSale.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next

                    For Each grow As GridViewRowInfo In gvMccSaleFarmer.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next

                    '' unselect mcc sale return trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In GvMccSaleReturn.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next

                    For Each grow As GridViewRowInfo In gvMccSaleReturnFarmer.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next

                    '' unselect mcc Item Issue trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvItemIssue.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next
                    '' unselect mcc Item Issue trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvItemIssueReturn.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next
                    '' unselect mcc Deduction trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvDeduction.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next
                    '' unselect mcc Credit trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvCreditNote.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub gvItemIssueReturnReturn_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvItemIssueReturn.CellValueChanged
        If e.Column Is gvItemIssueReturn.Columns(colSelect) Then
            If Not isLoad Then
                isLoad = True
                loadGvData()
                isLoad = False
            End If
        End If
        isCellValueChanged = False
    End Sub

    Sub SetToDate()
        If Not isLoad Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select the Location first")
                Exit Sub
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code =(select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If dtpFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                    dtpFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    dtpToDate.Value = dtpFromDate.Value
                    Exit Sub
                End If
                dtpToDate.Value = dtpFromDate.Value.AddDays(PaymentCycleValue - 1)

                If dtpFromDate.Value.Month <> dtpToDate.Value.Month Then
                    dtpToDate.Value = New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = dtpToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If dtpFromDate.Value.Month <> dtNxtPay.Month Then
                    dtpToDate.Value = New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As Date = dtpFromDate.Value
                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                dtpFromDate.Value = today.AddDays(-dayDiff)
                dtpToDate.Value = dtpFromDate.Value.AddDays(6)
            End If
            ' End If
        End If
    End Sub

    Private Sub dtpFromDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpFromDate.Validating
        SetToDate()
    End Sub

    Private Sub dtpFromDate_Leave(sender As Object, e As EventArgs) Handles dtpFromDate.Leave
        SetToDate()
    End Sub

    Private Sub txtVSP__My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                fndLoc.Focus()
                Throw New Exception("Please select MCC")
            End If

            Dim qry As String = "select xx.VSP_CODE as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,xx.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name from (" + Environment.NewLine +
            " select VSP_CODE,max(VLC_CODE)as VLC_CODE from (" + Environment.NewLine + _
            " select VSP_CODE,VLC_CODE from TSPL_MILK_SRN_Head left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MILK_SRN_Head.MCC_CODE where TSPL_LOCATION_MASTER.Loc_Segment_Code ='" + fndLoc.Value + "'"
            If Not isPickPendingMilkSRNinNextPaymentCycle Then
                qry += " and DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "
            End If
            qry += " and DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine + _
            " union all " + Environment.NewLine + _
            " select VSP_CODE,VLC_CODE as VLC_CODE from TSPL_MILK_REJECT_DETAIL left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MILK_REJECT_HEAD.MCC_CODE where TSPL_MILK_REJECT_HEAD.Posted=1 and TSPL_LOCATION_MASTER.Loc_Segment_Code ='" + fndLoc.Value + "'"
            If Not isPickPendingMilkSRNinNextPaymentCycle Then
                qry += " and TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
            End If
            qry += " and TSPL_MILK_REJECT_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine + _
            " )xxx group by VSP_CODE " + Environment.NewLine + _
            " )xx " + Environment.NewLine +
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.VSP_CODE " + Environment.NewLine +
            " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=xx.VLC_CODE " + Environment.NewLine +
            " where TSPL_VENDOR_MASTER.VSP_Farmer_Billing=1 order by xx.VSP_CODE"
            '" where TSPL_VENDOR_MASTER.is_Hold_Payment_Process=0 " + Environment.NewLine +


            txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm("PPfPVLF", qry, "Code", "", txtVSP.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function Load_Report(ByVal Location As String, ByVal dFromDate As Date, ByVal dToDate As Date, ByVal ListOfVSPs As ArrayList, ByVal otherCls As Boolean, ByVal PrintOpen As Boolean) As DataTable
        '======================preeti gupta ticket no []
        Dim sQuery As String
        Dim dtgv As New DataTable
        Dim companyADD, CompName, CompCode As String

        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        companyADD = dt1.Rows(0).Item("comp_address")

        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompName = dt2.Rows(0).Item("Comp_Name")


        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompCode = dt5.Rows(0).Item("Comp_Code")

        Dim fromDate As String = dtpFromDate.Value
        Dim Todate As String = dtpToDate.Value


        Dim whrcls As String = " where 2=2 "
        Dim whrcls1 As String = " where 2=2 "
        Dim whrclsItemWise As String = " where 2=2 "
        If otherCls Then

            whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dFromDate + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dToDate + "'),103) "
            whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(ListOfVSPs) & ")"
            If clsCommon.myLen(Location) > 0 Then
                whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN (" + Location + ") "
            End If

            whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dFromDate + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dToDate + "'),103) "
            whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(ListOfVSPs) & ")"
            If clsCommon.myLen(Location) > 0 Then
                whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN (" + Location + ") "
            End If
            whrclsItemWise += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dFromDate + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dToDate + "'),103) "

            whrclsItemWise += " and final.Customer_CODE in ( " & clsCommon.GetMulcallString(ListOfVSPs) & ")"
            whrclsItemWise += " and TSPL_PAYMENT_PROCESS_head.Loc_Seg_Code IN (" + Location + ")"


        Else
            whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
            whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
            If clsCommon.myLen(fndLoc.Value) > 0 Then
                whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN (" + fndLoc.Value + ") "
            End If

            whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
            whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
            whrcls1 += "  and TSPL_PAYMENT_PROCESS_Head.doc_no='" + fndDocNo.Value + "'"
            If clsCommon.myLen(fndLoc.Value) > 0 Then
                whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN (" + fndLoc.Value + ") "
            End If
            whrclsItemWise += " and final.doc_no in ('" + fndDocNo.Value + "')"
            whrclsItemWise += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
            'whrclsItemWise += " and final.Customer_CODE in  ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
            'whrclsItemWise += " and TSPL_PAYMENT_PROCESS_head.Loc_Seg_Code IN (" + fndLoc.Value + ")"
        End If
        sQuery = ""

        'sQuery += " select  '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate ,''  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,convert(varchar,DOC_DATE,103) as DOC_DATE,SHIFT,TYPE,SAMPLE_NO,convert(decimal(18,2),NewQty)as NewQty ,convert(decimal(18,1),FAT_PER) as FAT_PER ,convert(decimal(18,1),SNF_PER) as SNF_PER  ,convert(decimal(18,1),CLR)as CLR ,convert(decimal(18,2),(FAT_PER * NewQty)/100 )as TFAT,convert(decimal(18,2),(SNF_PER * NewQty)/100) as TSNF,convert(decimal(18,2),Amount/NewQty) as RATE  ,convert(decimal(18,2),Amount)as Amount ,MCC_CODE,VLC_Code ,VLC_Code_VLC_Uploader,emp,incentive,HEDAmt,AstAMT,DedAmt,coalesce(sale_AMt,0) as sale_AMt,VSP_CODE, ISNULL(Deduction_Debit_Amount,0) as Deduction_Debit_Amount, ISNULL(Deduction_MCC_Sale_Amount,0) as Deduction_MCC_Sale_Amount, ISNULL(Deduction_MCC_Sale_Return_Amount,0) as Deduction_MCC_Sale_Return_Amount, ISNULL(Deduction_Item_Issue_Amount,0) as Deduction_Item_Issue_Amount, ISNULL(Deduction_CREDIT_Amount,0) as Deduction_CREDIT_Amount,ISNULL(Issue_Return_Amount,0) as Issue_Return_Amount,Total_Basic_AMOUNT from( "
        'sQuery += " select addd,DOC_DATE,UOM_Code,Qty as NewQty, Qty,RATE,Net_AMOUNT as Amount ,MCC_CODE+' -'+MCC_NAME+'  QtyMode :'+UOM_Code    as MCC_CODE ,VSP_CODE ,SHIFT,ROUTE_CODE+' -'+Route_Name as ROUTE_CODE  ,Vendor_Name ,MCC_NAME,SAMPLE_NO ,TYPE ,CLR,FAT_PER ,SNF_PER ,VLC_Code_VLC_Uploader+' - '+VLC_Name as VLC_Code_VLC_Uploader, VLC_Code,emp,incentive,HEDAmt,AstAMT,DedAmt,sale_AMt,Deduction_Debit_Amount, Deduction_MCC_Sale_Amount, Deduction_MCC_Sale_Return_Amount, Deduction_Item_Issue_Amount, Deduction_CREDIT_Amount,Issue_Return_Amount,Total_Basic_AMOUNT from "
        sQuery += "select TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, "
        If otherCls Then
            sQuery += "    '" & dFromDate & "'  as fromDate ,'" & dToDate & "'  as Todate"
        Else
            sQuery += "    '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate"
        End If
        sQuery += " ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,coalesce(PaymentProcess.Total_EMP_Amount,0) as Total_EMP_Amount,coalesce(PaymentProcess.Incentive_Amount,0) as Incentive_Amount ,coalesce(PaymentProcess.Incentive_EMP_Amount,0) as Incentive_EMP_Amount ,coalesce(PaymentProcess.EMP_Amount,0) as EMP_Amount ,coalesce(PaymentProcess.Vsp_Own_System_Amount,0) as Vsp_Own_System_Amount ,coalesce(PaymentProcess.Head_Load_Amount,0) as Head_Load_Amount ,coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0)*(-1) as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_RECEIPT_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty  ,FAT_PER ,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as FATQTY,SNF_PER,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as SNFQTY "
        sQuery += " ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.RATE as RATE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT, TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,TSPL_MILK_SAMPLE_HEAD.SHIFT,"
        sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,TSPL_MILK_SAMPLE_DETAIL.TYPE ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
        sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt ,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT"
        sQuery += "  from TSPL_MILK_PURCHASE_INVOICE_DETAIL  Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE"
        sQuery += "   Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE    Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  "
        sQuery += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE   left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE Left Outer Join TSPL_VENDOR_MASTER On"
        sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'   Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_RECEIPT_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MCC_MASTER.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join TSPL_VLC_MASTER_HEAD on"
        sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  "
        sQuery += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code "
        sQuery += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code "
        sQuery += "  left join  (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL"
        sQuery += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
        sQuery += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
        sQuery += " ) as pp group by VSP_CODE,VLC_Code"
        sQuery += " ) as PaymentProcess on "
        sQuery += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_Code"
        sQuery += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code"

        sQuery += "  " & whrcls & " "
        sQuery += "order by vsp_code,convert(datetime,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)"

        '================================================= END ALL TYPE DEDUCTION=================================================
        'sQuery += " ) as yy left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_name='" & CompName & "'"
        'sQuery += "  order by convert(date,DOC_DATE,103)"
        Dim dt As New DataTable

        dt = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "  select Customer_CODE as VSP_Code,trans_type,Item_Code as VLC_Code_VLC_Uploader ,Item_Desc ,coalesce(Amount,0) as Amount  from  (select 'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Amount*(-1) as Amount  from TSPL_PAYMENT_PROCESS_MCC_SALE"
        sQuery += " left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no"
        sQuery += " left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code  "
        sQuery += "  union all"
        sQuery += " select 'MCC Sale Return' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Doc_No ,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no,TSPL_SD_SALE_RETURN_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.amount as Paymnet_Amount ,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt"
        sQuery += " from TSPL_PAYMENT_PROCESS_MCC_SALE_Return"
        sQuery += "  left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no"
        sQuery += " left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code "

        sQuery += "  union all"
        sQuery += " select 'Item Issue' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt*(-1) as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE "
        sQuery += " left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no"
        sQuery += " left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code "
        sQuery += "   union all"

        sQuery += " select 'Item Issue Return' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN "
        sQuery += " left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no"
        sQuery += " left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code "

        sQuery += "   union all"

        sQuery += " select 'Deduction' as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE ,0 as Paymnet_Amount ,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Item_Net_AMount  from TSPL_PAYMENT_PROCESS_DEDUCTION  "

        sQuery += " union all"

        sQuery += "  select 'Credit' as trans_type,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No   ,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE ,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_NAME,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,0 as Paymnet_Amount ,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Item_Net_AMount  from TSPL_PAYMENT_PROCESS_CREDIT_NOTE  "

        sQuery += " ) as final "
        sQuery += " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no"
        sQuery += " " & whrclsItemWise & " "


        dtgv = clsDBFuncationality.GetDataTable(sQuery)



        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            If PrintOpen = True Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dtgv, "crptMilkPurchaseBillPaymentProcess", "SubMilkPurchaseBill.rpt", "SubMilkPurchaseBill.rpt", "Address.rpt")
                frmCRV = Nothing
            End If
            Return dt
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Return Nothing
        End If

    End Function

    Public Sub Load_Report_Paymnet_UDL()
        '======================preeti gupta ticket no [] update 29/11/2016
        Dim sQuery As String
        Dim dtgv As New DataTable
        Dim dt As New DataTable
        Dim companyADD, CompName, CompCode As String

        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        companyADD = dt1.Rows(0).Item("comp_address")

        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompName = dt2.Rows(0).Item("Comp_Name")


        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompCode = dt5.Rows(0).Item("Comp_Code")

        Dim whrcls As String = " where 2=2 "
        whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN (" + fndLoc.Value + ") "
        End If
        whrcls += " and  PaymentProcess.doc_no='" + fndDocNo.Value + "'"
        Dim whrcls1 As String = " where 2=2 "
        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN (" + fndLoc.Value + ") "
        End If

        Dim whrclsDeduction As String = " where 2=2 "
        whrclsDeduction += "  and convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and  convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrclsDeduction += "  and TSPL_MILK_REJECT_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")   "
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrclsDeduction += " and TSPL_LOCATION_MASTER.Loc_Segment_Code    IN (" + fndLoc.Value + ") "
        End If
        sQuery = ""


        sQuery += "select *,Net_AMOUNT-Reject_Deduction_Amount as GrossAmt from (select  TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE,'' as Reject_Type,0 as Reject_Deduction_Amount ,TSPL_MILK_SRN_DETAIL .FAT_KG,TSPL_MILK_SRN_DETAIL .SNF_KG , (-1)*coalesce(TSPL_VENDOR_MASTER.Security_Deduction_Amount,0) as Security_Deduction_Amount,"
        sQuery += " PaymentProcess.doc_no as paymnet_DOC_No,PaymentProcess.Advance_Payment_Amount,PaymentProcess.Advance_Payment_Amount_Knock_Off,PaymentProcess.doc_date as paymnet_doc_date,convert(varchar,PaymentProcess.from_date,103) as Paymnet_From_Date,convert(varchar,PaymentProcess.to_date,103) as Payment_To_Date, (-1)*PaymentProcess.Service_Charge_Amt as Service_Charge_Amt  ,  TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, '" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,PaymentProcess.Total_EMP_Amount,PaymentProcess.Incentive_Amount ,PaymentProcess.Incentive_EMP_Amount ,PaymentProcess.EMP_Amount ,PaymentProcess.Vsp_Own_System_Amount ,PaymentProcess.Head_Load_Amount ,(PaymentProcess.Payable_Amount) as Payable_Amount,(PaymentProcess.Credit_Note_Amount)as Credit_Note_Amount,(PaymentProcess.Deduction_Amount)*(-1) as Deduction_Amount,(PaymentProcess.Item_Issue_Amount)*(-1) as Item_Issue_Amount,(PaymentProcess.Item_Issue_Return_Amount) as Item_Issue_Return_Amount,(PaymentProcess.MCC_Sale_Amount)*(-1) as MCC_Sale_Amount,(PaymentProcess.Reduce_Deduc_Amt) as Reduce_Deduc_Amt ,(PaymentProcess.MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_RECEIPT_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty  ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER ,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as FATQTY,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as SNFQTY "
        sQuery += " ,case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  Price_Chart.milk_rate end as RATE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT, TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,TSPL_MILK_SAMPLE_HEAD.SHIFT,"
        sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,TSPL_MILK_SAMPLE_DETAIL.TYPE ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
        sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt ,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT,isnull(PaymentProcess.Deduction_Amount,0)*(-1)  as Total_Deduction "
        sQuery += " from TSPL_MILK_PURCHASE_INVOICE_DETAIL  Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE  "
        sQuery += " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE    Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  "
        sQuery += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE   left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE Left Outer Join TSPL_VENDOR_MASTER On"
        sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'   Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_RECEIPT_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MCC_MASTER.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join TSPL_VLC_MASTER_HEAD on"
        sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  "
        sQuery += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code "
        sQuery += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code "
        sQuery += " left join  (select sum(Advance_Payment_Amount) as Advance_Payment_Amount,sum(Advance_Payment_Amount_Knock_Off)as Advance_Payment_Amount_Knock_Off, max(doc_no) as doc_no,max(convert(varchar,doc_date,103)) as doc_date,max(from_date) as from_date,max(to_date) as to_date,sum(Service_Charge_Amt ) as Service_Charge_Amt, VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount,sum(Reduce_Deduc_Amt) as Reduce_Deduc_Amt from (select TSPL_PAYMENT_PROCESS_DETAIL.Advance_Payment_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Advance_Payment_Amount_Knock_Off, TSPL_PAYMENT_PROCESS_HEAD.doc_no,TSPL_PAYMENT_PROCESS_HEAD.doc_date,TSPL_PAYMENT_PROCESS_HEAD.from_date,TSPL_PAYMENT_PROCESS_HEAD.to_date,tspl_payment_process_detail.milk_purchase_invoice_no as Doc_Code,TSPL_PAYMENT_PROCESS_DETAIL.Service_Charge_Amt ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Reduce_Deduc_Amt  from TSPL_PAYMENT_PROCESS_DETAIL"
        sQuery += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
        sQuery += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
        sQuery += " ) as pp group by Doc_No,VSP_CODE,VLC_Code"
        sQuery += " ) as PaymentProcess on "

        sQuery += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_Code"
        sQuery += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code"
        sQuery += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code "
        sQuery += "   from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart "
        sQuery += "   on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code "

        sQuery += "  " & whrcls & " "


        sQuery += "  union all"

        sQuery += " select '' as DOC_CODE,case when  TSPL_MILK_REJECT_DETAIL.Defaulter='VSP' then  TSPL_MILK_REJECT_DETAIL .Reject_Type else '' end Reject_Type, case when  TSPL_MILK_REJECT_DETAIL.Defaulter='VSP' then  isnull(TSPL_MILK_REJECT_DETAIL.snf_deduction_amount,0)+isnull(TSPL_MILK_REJECT_DETAIL.Fat_deduction_amount,0) else 0 end as Reject_Deduction_Amount, isnull(TSPL_MILK_REJECT_DETAIL.FAT,0) * isnull(TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,0) /100 as FAT_KG,isnull(TSPL_MILK_REJECT_DETAIL.SNF,0)  * isnull(TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,0) /100 as SNF_KG ,"
        sQuery += " 0 as Security_Deduction_Amount,'' as paymnet_DOC_No,0 as Advance_Payment_Amount,0 as Advance_Payment_Amount_Knock_Off,"
        sQuery += " NULL as paymnet_doc_date,NULL as Paymnet_From_Date,NULL as Payment_To_Date,"
        sQuery += " 0 as Service_Charge_Amt , ''  as MCC_address,'' as companyADD, ''  as CompName,''  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2"
        sQuery += ",0 as Total_EMP_Amount,0 as Incentive_Amount ,0 as Incentive_EMP_Amount ,0 as EMP_Amount ,0 as Vsp_Own_System_Amount ,0 as Head_Load_Amount ,0  as Payable_Amount,0 as Credit_Note_Amount,0 as Deduction_Amount,0 as Item_Issue_Amount,0 as Item_Issue_Return_Amount,0 as MCC_Sale_Amount ,0 as Reduce_Deduc_Amt,0 as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_REJECT_DETAIL.UOM_Code as UOM_Code,TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT  as Qty  ,TSPL_MILK_REJECT_DETAIL.FAT  as FAT_PER ,0 as FATQTY,TSPL_MILK_REJECT_DETAIL.SNF  as SNF_PER,0 as SNFQTY  ,Price_Chart.milk_rate   as RATE,case when TSPL_MILK_REJECT_DETAIL.Is_Return in ('1','2') then 0 else TSPL_MILK_REJECT_DETAIL.Amount end as Net_AMOUNT, TSPL_MILK_REJECT_HEAD.MCC_CODE  as MCC_CODE , "
        sQuery += " convert(varchar,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE,"
        sQuery += "TSPL_MILK_REJECT_DETAIL.VSP_CODE  as VSP_CODE ,TSPL_MILK_REJECT_HEAD.SHIFT  as SHIFT, TSPL_MILK_REJECT_DETAIL.ROUTE_CODE  as ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,"
        sQuery += " '' as TYPE ,0 as CLR,'' as SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_VLC_MASTER_HEAD.VLC_Name "
        sQuery += " ,0 as [EMP],0 as Incentive,0 as HEDAmt,0 as AstAMT,0 as DedAmt ,0 as Total_Basic_AMOUNT,0 as Total_Deduction "
        sQuery += " from TSPL_MILK_REJECT_HEAD left join TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE =TSPL_MILK_REJECT_HEAD.DOC_CODE  left join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code  =TSPL_MILK_REJECT_HEAD.MCC_CODE "
        sQuery += " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_REJECT_HEAD.MCC_CODE"
        sQuery += " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =TSPL_MILK_REJECT_HEAD.Comp_Code "
        sQuery += " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_MILK_REJECT_DETAIL .VSP_CODE "
        sQuery += " left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code =TSPL_MILK_REJECT_DETAIL.ROUTE_CODE "
        sQuery += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_REJECT_DETAIL.VLC_CODE "
        sQuery += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_REJECT_DETAIL.Price_Code=Price_Chart.Code  "
        sQuery += " " & whrclsDeduction & ") as final "
        sQuery += " order by vsp_code,convert(date,doc_date,103) ,Reject_Type "



        dt = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select Customer_CODE,Item_Code,max(Item_Desc)  as Item_Desc,sum(Qty) as Qty from ( select  TSPL_SD_SHIPMENT_detail.qty,'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Amount*(-1) as Amount  from TSPL_PAYMENT_PROCESS_MCC_SALE"
        sQuery += " left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no"
        sQuery += " left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no='" + fndDocNo.Value + "' )xx group by Customer_CODE,item_code "

        dtgv = clsDBFuncationality.GetDataTable(sQuery)

        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dtgv, "crptMilkPurchaseBillPaymentProcess", "SubMilkPurchaseBill.rpt", "SubMilkPurchaseBill.rpt", "Address.rpt")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            Load_Report_Paymnet_UDL()
        ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then ' Ticket No : ERO/23/07/19-000961 By Prabhakar
            PrintData(False)
        ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "RCDF") = CompairStringResult.Equal Then
            PrintDataRCDF(False)
        Else
            Load_Report(Nothing, Nothing, Nothing, Nothing, False, True)
        End If
    End Sub

    Sub PrintDataRCDF(ByVal isPreFormatePrint)
        Try
            Dim qry As String = "Select SSSS.* , TBL_IncentiveDeduction.Incentive_Amount , TBL_IncentiveDeduction.Deduction_Amount from (" &
                               " select Final.*, isnull (TBL_FOR_DEDUCTION.Amount,0) as DedAddAmount,TSPL_MP_MASTER.BankName as Bank_Code, tspl_vendor_bank_master.Bank_Name,TSPL_MP_MASTER.AccountNo,TSPL_MP_MASTER.IFCICode,TSPL_MP_MASTER.BankBranch, RIGHT( MP_VLC_Uploader_Code,3) as MP_VLC_Uploader_Code_Last3Degit from ( select TSPL_COMPANY_MASTER.Comp_Code," + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img, TSPL_COMPANY_MASTER.Comp_Name as CompName,'" + clsCommon.myCDate(dtpFromDate.Value) + "' as FromDate ,'" + clsCommon.myCDate(dtpToDate.Value) + "' as ToDate ,Convert (varchar,TSPL_VLC_DATA_UPLOADER.Doc_Date,103) as Doc_Date ,TSPL_VLC_DATA_UPLOADER.Milk_Type, TSPL_VLC_DATA_UPLOADER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Add1 as MCC_Add1 , TSPL_MCC_MASTER.Add2 as MCC_Add2 , TSPL_MCC_MASTER.City_code as MCC_City_Code, TSPL_CITY_MASTER_MCC.City_Name as MCC_City_Name,TSPL_MCC_MASTER.State_Code as MCC_State_Code,TSPL_STATE_MASTER_MCC.STATE_NAME as MCC_STATE_NAME,TSPL_MCC_MASTER.Pin_code as MCC_Pin_Code , TSPL_VLC_DATA_UPLOADER.shift , TSPL_VLC_DATA_UPLOADER.VLC_CODE as VLC_Code_VLC_Uploader , " &
                               " TSPL_MP_MASTER.MP_Code as VSP_Code, TSPL_MP_MASTER.MP_Name as VSP_Name , TSPL_MP_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name " &
                               " , TSPL_VLC_DATA_UPLOADER.Route_No , TSPL_VLC_DATA_UPLOADER.MP_CODE as MP_VLC_Uploader_Code ,TSPL_VLC_DATA_UPLOADER.Uom_Code, TSPL_VLC_DATA_UPLOADER.qty , TSPL_VLC_DATA_UPLOADER.fat as FAT_PER, TSPL_VLC_DATA_UPLOADER.snf as SNF_PER,TSPL_VLC_DATA_UPLOADER.Rate , TSPL_VLC_DATA_UPLOADER.Amount as Net_Amount from TSPL_VLC_DATA_UPLOADER " &
                               " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_DATA_UPLOADER.MCC_Code " &
                               " left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_MCC on TSPL_CITY_MASTER_MCC.City_Code =  TSPL_MCC_MASTER.City_code " &
                               " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_MCC on TSPL_STATE_MASTER_MCC.State_Code = TSPL_MCC_MASTER.State_Code " &
                               " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_UPLOADER = TSPL_VLC_DATA_UPLOADER.VLC_CODE " &
                               " left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_VLC_DATA_UPLOADER.Comp_Code " &
                               " left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code_VLC_Uploader = TSPL_VLC_DATA_UPLOADER.MP_CODE " &
                               " where convert (date, TSPL_VLC_DATA_UPLOADER.Doc_Date,103) > = (select TSPL_PAYMENT_PROCESS_HEAD.From_Date  from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "')  " &
                               " and convert (date, TSPL_VLC_DATA_UPLOADER.Doc_Date,103) < = (select  TSPL_PAYMENT_PROCESS_HEAD.To_Date from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "') " &
                               " and TSPL_VLC_DATA_UPLOADER.MP_CODE in (select distinct TSPL_MP_MASTER.MP_Code_VLC_Uploader from TSPL_MP_PAY_PROCESS_DETAIL inner join TSPL_MP_MASTER on TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code = TSPL_MP_MASTER.MP_Code  where Doc_No = '" + fndDocNo.Value + "') " &
                               " ) Final  " &
                               " left outer join  (select Final.VSP_Code  ,sum (Final.Item_Net_Amt) as Amount from ( " &
                               " select TSPL_MP_PAY_PROCESS_MCC_SALE.FARMER_CODE as VSP_Code,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_SALE_FARMER_DETAIL.Item_Net_Amt * -1 as Item_Net_Amt from TSPL_MP_PAY_PROCESS_MCC_SALE inner join TSPL_MCC_SALE_FARMER_DETAIL on TSPL_MP_PAY_PROCESS_MCC_SALE.Shipment_Doc_No = TSPL_MCC_SALE_FARMER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_FARMER_DETAIL.Item_Code where TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocNo.Value + "' " &
                               " Union All " &
                               " select TSPL_MCC_SALE_RETURN_HEAD_FARMER.FARMER_CODE as  VSP_Code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount  as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN   inner join TSPL_MCC_SALE_RETURN_DETAIL_FARMER on TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Item_Code   left outer join TSPL_MCC_SALE_RETURN_HEAD_FARMER on TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Document_Code   where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" + fndDocNo.Value + "' " &
                               " ) Final group by Final.VSP_Code ) As TBL_FOR_DEDUCTION on TBL_FOR_DEDUCTION.VSP_Code = Final.VSP_Code " &
                               " Left Outer Join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = Final.VSP_Code " &
                               " left Outer Join tspl_vendor_bank_master on tspl_vendor_bank_master.Bank_Code = TSPL_MP_MASTER.BankName " &
                               "  " &
                               " Union All" &
                               " select Final.*, isnull (TBL_FOR_DEDUCTION.Amount,0) as DedAddAmount,TSPL_MP_MASTER.BankName as Bank_Code, tspl_vendor_bank_master.Bank_Name,TSPL_MP_MASTER.AccountNo,TSPL_MP_MASTER.IFCICode,TSPL_MP_MASTER.BankBranch, RIGHT( MP_VLC_Uploader_Code,3) as MP_VLC_Uploader_Code_Last3Degit from ( select TSPL_COMPANY_MASTER.Comp_Code, " + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img, TSPL_COMPANY_MASTER.Comp_Name as CompName,'" + clsCommon.myCDate(dtpFromDate.Value) + "' as FromDate ,'" + clsCommon.myCDate(dtpToDate.Value) + "' as ToDate ,Convert (varchar,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) as Doc_Date ,'C' as Milk_Type,TSPL_VLC_MASTER_HEAD_ForMCC.MCC " &
                               " as MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Add1 as MCC_Add1 , TSPL_MCC_MASTER.Add2 as MCC_Add2 , TSPL_MCC_MASTER.City_code as MCC_City_Code, TSPL_CITY_MASTER_MCC.City_Name as MCC_City_Name,TSPL_MCC_MASTER.State_Code as MCC_State_Code,TSPL_STATE_MASTER_MCC.STATE_NAME as MCC_STATE_NAME,TSPL_MCC_MASTER.Pin_code as MCC_Pin_Code , case when  TSPL_VLC_DATA_UPLOADER_MASTER.Shift = 'MORNING' then 'M' else 'E' end as Shift , TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader  as VLC_Code_VLC_Uploader ,  TSPL_MP_MASTER.MP_Code as VSP_Code, TSPL_MP_MASTER.MP_Name as VSP_Name , TSPL_MP_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name  , TSPL_VLC_DATA_UPLOADER_MASTER.Route_Code as Route_No , TSPL_MP_MASTER.MP_Code_VLC_Uploader as MP_VLC_Uploader_Code ,TSPL_VLC_DATA_UPLOADER_DETAIL.Unit_Code as Uom_Code, TSPL_VLC_DATA_UPLOADER_DETAIL.Qty , TSPL_VLC_DATA_UPLOADER_DETAIL.fatPer as FAT_PER, TSPL_VLC_DATA_UPLOADER_DETAIL.SNFPer as SNF_PER,TSPL_VLC_DATA_UPLOADER_DETAIL.Rate , TSPL_VLC_DATA_UPLOADER_DETAIL.Amount as Net_Amount from  TSPL_VLC_DATA_UPLOADER_DETAIL left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code = TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code " &
                               " Left Outer Join TSPL_VLC_MASTER_HEAD as TSPL_VLC_MASTER_HEAD_ForMCC on TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code=TSPL_VLC_MASTER_HEAD_ForMCC.VLC_Code  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code" &
                               " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD_ForMCC.MCC  left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_MCC on TSPL_CITY_MASTER_MCC.City_Code =  TSPL_MCC_MASTER.City_code  left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_MCC on TSPL_STATE_MASTER_MCC.State_Code = TSPL_MCC_MASTER.State_Code    left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_VLC_DATA_UPLOADER_MASTER.Comp_Code  left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code  where convert (date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) > = (select TSPL_PAYMENT_PROCESS_HEAD.From_Date  from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "')   and convert (date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) < = (select  TSPL_PAYMENT_PROCESS_HEAD.To_Date from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "')  and TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code in (select distinct TSPL_MP_MASTER.MP_Code from TSPL_MP_PAY_PROCESS_DETAIL inner join TSPL_MP_MASTER on TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code = TSPL_MP_MASTER.MP_Code  where Doc_No = '" + fndDocNo.Value + "')  ) Final   left outer join  (select Final.VSP_Code  ,sum (Final.Item_Net_Amt) as Amount from (  select TSPL_MP_PAY_PROCESS_MCC_SALE.FARMER_CODE as VSP_Code,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_SALE_FARMER_DETAIL.Item_Net_Amt * -1 as Item_Net_Amt from TSPL_MP_PAY_PROCESS_MCC_SALE inner join TSPL_MCC_SALE_FARMER_DETAIL on TSPL_MP_PAY_PROCESS_MCC_SALE.Shipment_Doc_No = TSPL_MCC_SALE_FARMER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_FARMER_DETAIL.Item_Code where TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocNo.Value + "'  Union All  select TSPL_MCC_SALE_RETURN_HEAD_FARMER.FARMER_CODE as  VSP_Code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount  as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN   inner join TSPL_MCC_SALE_RETURN_DETAIL_FARMER on TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Item_Code   left outer join TSPL_MCC_SALE_RETURN_HEAD_FARMER on TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Document_Code   where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" + fndDocNo.Value + "'  ) Final group by Final.VSP_Code ) As TBL_FOR_DEDUCTION on TBL_FOR_DEDUCTION.VSP_Code = Final.VSP_Code  Left Outer Join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = Final.VSP_Code  left Outer Join tspl_vendor_bank_master on tspl_vendor_bank_master.Bank_Code = TSPL_MP_MASTER.BankName " &
                               " ) SSSS  left outer join  ( select VLC_CODE_Uploader,Farmer_Code, Sum ( isNull (Incentive_Amount,0)) as Incentive_Amount , Sum ( isnull(Deduction_Amount,0)) as Deduction_Amount from TSPL_MP_PAY_PROCESS_DETAIL where Doc_No = '" + fndDocNo.Value + "' group by VLC_CODE_Uploader,Farmer_Code ) TBL_IncentiveDeduction on TBL_IncentiveDeduction.VLC_CODE_Uploader = SSSS.VLC_Code_VLC_Uploader and TBL_IncentiveDeduction.Farmer_Code = SSSS.VSP_Code    "
            If isPreFormatePrint = True Then
                qry = " select TBL_Farmer_Invoice.Farmer_Invoice_No,VLC_Code_VLC_Uploader,TBL_Farmer_Invoice.MP_Adjust_Amount as OutStanding_Amt ,RIGHT ( FinalXXX.VLC_Code_VLC_Uploader,3) as VLC_Code_VLC_Uploader_3Digit, FinalXXX.Comp_Code," + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img ,FinalXXX.CompName as CompName,FinalXXX.FromDate as FromDate ,FinalXXX.ToDate  as ToDate,FinalXXX.Doc_Date,FinalXXX.Milk_Type as Milk_Type,FinalXXX.MCC_Code as MCC_Code,FinalXXX.MCC_NAME as MCC_NAME,FinalXXX.MCC_ADD1 as MCC_ADD1,FinalXXX.MCC_Add2 as MCC_Add2,FinalXXX.MCC_City_Code as MCC_City_Code,FinalXXX.MCC_City_Name as MCC_City_Name,FinalXXX.MCC_State_Code as MCC_State_Code,FinalXXX.MCC_STATE_NAME as MCC_STATE_NAME,FinalXXX.MCC_Pin_Code as MCC_Pin_Code,FinalXXX.shift,FinalXXX.VLC_Code_VLC_Uploader as VLC_Code_VLC_Uploader,FinalXXX.VSP_Code as VSP_Code,FinalXXX.VSP_Name as VSP_Name,FinalXXX.VLC_Code as VLC_Code,FinalXXX.VLC_Name as VLC_Name,FinalXXX.Route_No as Route_No ,FinalXXX.MP_VLC_Uploader_Code as MP_VLC_Uploader_Code,FinalXXX.Uom_Code as Uom_Code,FinalXXX.qty as qty , " &
                      " convert ( decimal(18,2), ((FinalXXX.Fat_KG * 100) / nullif (FinalXXX.Qty_In_KG,0) ))  as FAT_PER  ,convert (decimal(18,2), ((FinalXXX.SNF_KG * 100 )/ nullif (FinalXXX.Qty_In_KG,0)))   as SNF_PER,Rate as Rate,FinalXXX.Net_Amount as Net_Amount,FinalXXX.DedAddAmount as DedAddAmount,FinalXXX.Bank_Code as Bank_Code,FinalXXX.Bank_Name as Bank_Name ,FinalXXX.AccountNO as AccountNO,FinalXXX.IFCICode as IFCICode ,FinalXXX.BankBranch as BankBranch,FinalXXX.MP_VLC_Uploader_Code_Last3Degit as MP_VLC_Uploader_Code_Last3Degit, FinalXXX.Incentive_Amount as Incentive_Amount ,FinalXXX.Deduction_Amount as Deduction_Amount  " &
" ,TBL_Farmer_Invoice.FeedAmount ,TBL_Farmer_Invoice .LoanAmount ,TBL_Farmer_Invoice .OtherAmount,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_EMPLOYEE_MASTER.Emp_Name AS SuperVisorName ,TBL_Farmer_Invoice.Farmer_Name ,isnull (TBL_Farmer_Invoice.IncentivePerLitre,0) as  IncentivePerLitre " &
 " from ( " &
                      " select " &
                      " max(XXXXX.Comp_Code) as Comp_Code, " &
                      " max(XXXXX.CompName) as CompName,max(XXXXX.FromDate) as FromDate ,max(XXXXX.ToDate)  as ToDate,XXXXX.Doc_Date,max(XXXXX.Milk_Type) as Milk_Type,max(XXXXX.MCC_Code) as MCC_Code,max(XXXXX.MCC_NAME) as MCC_NAME,max(XXXXX.MCC_ADD1) as MCC_ADD1,max(XXXXX.MCC_Add2) as MCC_Add2,max(XXXXX.MCC_City_Code) as MCC_City_Code,max(XXXXX.MCC_City_Name) as MCC_City_Name,max(XXXXX.MCC_State_Code) as MCC_State_Code,max(XXXXX.MCC_STATE_NAME) as MCC_STATE_NAME,max(XXXXX.MCC_Pin_Code) as MCC_Pin_Code,XXXXX.shift,max(XXXXX.VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,XXXXX.VSP_Code as VSP_Code,max(XXXXX.VSP_Name) as VSP_Name,max(XXXXX.VLC_Code) as VLC_Code,max(XXXXX.VLC_Name) as VLC_Name,max(XXXXX.Route_No) as Route_No ,max(XXXXX.MP_VLC_Uploader_Code) as MP_VLC_Uploader_Code,max(XXXXX.Uom_Code) as Uom_Code,sum(XXXXX.qty) as qty ," &
                      " Sum (XXXXX.FAT_KG) as FAT_KG , Sum(SNF_KG) as SNF_KG , sum (Qty_In_KG) as Qty_In_KG,convert(decimal(18,2), sum (XXXXX.Net_Amount) / nullif ( sum(XXXXX.qty),0)) as Rate,sum (XXXXX.Net_Amount) as Net_Amount,sum (XXXXX.DedAddAmount) as DedAddAmount,max(XXXXX.Bank_Code) as Bank_Code,max(XXXXX.Bank_Name) as Bank_Name ,max(XXXXX.AccountNO) as AccountNO,max(XXXXX.IFCICode) as IFCICode ,max(XXXXX.BankBranch) as BankBranch,max(XXXXX.MP_VLC_Uploader_Code_Last3Degit) as MP_VLC_Uploader_Code_Last3Degit, max(XXXXX.Incentive_Amount) as Incentive_Amount ,max( XXXXX.Deduction_Amount) as Deduction_Amount from ( " &
                      " Select  SSSSFinal.Comp_Code, " &
                      " SSSSFinal.Logo_Img,SSSSFinal.CompName,SSSSFinal.FromDate,SSSSFinal.ToDate,SSSSFinal.Doc_Date,SSSSFinal.Milk_Type,SSSSFinal.MCC_Code,SSSSFinal.MCC_NAME,SSSSFinal.MCC_ADD1,SSSSFinal.MCC_Add2,SSSSFinal.MCC_City_Code,SSSSFinal.MCC_City_Name,SSSSFinal.MCC_State_Code,SSSSFinal.MCC_STATE_NAME,SSSSFinal.MCC_Pin_Code,SSSSFinal.shift,SSSSFinal.VLC_Code_VLC_Uploader,SSSSFinal.VSP_Code,SSSSFinal.VSP_Name,SSSSFinal.VLC_Code,SSSSFinal.VLC_Name,SSSSFinal.Route_No,SSSSFinal.MP_VLC_Uploader_Code,SSSSFinal.Uom_Code,SSSSFinal.qty,SSSSFinal.FAT_PER,SSSSFinal.SNF_PER,SSSSFinal.Rate,SSSSFinal.Net_Amount,SSSSFinal.DedAddAmount,SSSSFinal.Bank_Code,SSSSFinal.Bank_Name,SSSSFinal.AccountNO,SSSSFinal.IFCICode,SSSSFinal.BankBranch,SSSSFinal.MP_VLC_Uploader_Code_Last3Degit, SSSSFinal.Incentive_Amount , SSSSFinal.Deduction_Amount ,(SSSSFinal.FAT_PER *  (zzz.CF*SSSSFinal.Qty) ) /100  as FAT_KG,   (SSSSFinal.SNF_PER * (zzz.CF*SSSSFinal.Qty) ) /100  as  SNF_KG,zzz.CF*SSSSFinal.Qty as Qty_In_KG " &
                      " from ( " &
                      " " + qry + "  " &
                      " ) SSSSFinal left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/  nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =SSSSFinal.UOM_CODE and lower(zzz.TOUOM)='KG' " &
                      " )   XXXXX " &
                      " Group by XXXXX.VSP_Code,XXXXX.Doc_Date,XXXXX.shift " &
                      " ) FinalXXX left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = FinalXXX.Comp_Code " &
                      "  left outer join (select   Farmer_Code ,left ( Farmer_Invoice_No,(len(Farmer_Invoice_No)- ( len(Farmer_Code)+1 ))) as Farmer_Invoice_No ,MP_Adjust_Amount,Deduction_Amount as OtherAmount,MCC_Sale_Amount-MCC_Sale_Return_Amount as FeedAmount,Total_Advance_Amount as LoanAmount,Farmer_Name,case when isnull(Milk_Qty,0)>0 then Incentive_Amount/Milk_Qty  end IncentivePerLitre  from TSPL_MP_PAY_PROCESS_DETAIL  where  TSPL_MP_PAY_PROCESS_DETAIL.Doc_No = '" + fndDocNo.Value + "') as  TBL_Farmer_Invoice on FinalXXX.VSP_CODE = TBL_Farmer_Invoice.Farmer_Code left outer join TSPL_MCC_ROUTE_MASTER  on TSPL_MCC_ROUTE_MASTER .route_code=finalxxx.Route_No LEFT OUTER jOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_MCC_ROUTE_MASTER .Supervisor_Name  " &
                      " order by convert (date,FinalXXX.Doc_Date,103) asc ,FinalXXX.shift desc "
            Else
                qry = qry + " order by convert (date,SSSS.Doc_Date,103) asc ,SSSS.shift desc "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            qry = "  select Final.VSP_Code ,Final.Item_Code,max(Final.Item_Desc) as Item_Desc ,sum (Final.Item_Net_Amt) as Amount from ( " &
                  "  select TSPL_MP_PAY_PROCESS_MCC_SALE.FARMER_CODE as VSP_Code,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_SALE_FARMER_DETAIL.Item_Net_Amt * -1 as Item_Net_Amt from TSPL_MP_PAY_PROCESS_MCC_SALE inner join TSPL_MCC_SALE_FARMER_DETAIL on TSPL_MP_PAY_PROCESS_MCC_SALE.Shipment_Doc_No = TSPL_MCC_SALE_FARMER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_FARMER_DETAIL.Item_Code where TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocNo.Value + "' " &
                  "  Union All " &
                  "  select TSPL_MCC_SALE_RETURN_HEAD_FARMER.FARMER_CODE as  VSP_Code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount  as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN   inner join TSPL_MCC_SALE_RETURN_DETAIL_FARMER on TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Item_Code   left outer join TSPL_MCC_SALE_RETURN_HEAD_FARMER on TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Document_Code   where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" + fndDocNo.Value + "' " &
                  " ) Final group by Final.VSP_Code ,Final.Item_Code "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If isPreFormatePrint = True Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dt2, "crptPaymentProcessFarmerPredefineFormate", "SubMilkPurchaseBill.rpt", "SubMilkPurchaseBill.rpt", "Address.rpt")
                    frmCRV = Nothing
                Else
                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dt2, "crptPaymentProcessFarmer", "SubMilkPurchaseBill.rpt", "SubMilkPurchaseBill.rpt", "Address.rpt")
                    frmCRV = Nothing
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDocPrint_Click(sender As Object, e As EventArgs) Handles btnDocPrint.Click
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            Doc_Print()
        End If
    End Sub

    Public Sub Doc_Print()

        Dim Query As String = Nothing

        Dim sQuery As String = Nothing

        Dim companyADD, CompName, CompCode As String

        sQuery = ""
        sQuery = " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address " & _
            " ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.comp_code from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        companyADD = dt1.Rows(0).Item("comp_address")

        CompName = dt1.Rows(0).Item("Comp_Name")

        CompCode = dt1.Rows(0).Item("Comp_Code")

        Dim whrcls As String = " where 2=2 "
        whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN (" + fndLoc.Value + ") "
        End If
        whrcls += " and  PaymentProcess.doc_no='" + fndDocNo.Value + "'"
        Dim whrcls1 As String = " where 2=2 "
        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN (" + fndLoc.Value + ") "
        End If

        Dim whrclsDeduction As String = " where 2=2 "
        whrclsDeduction += "  and convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and  convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrclsDeduction += "  and TSPL_MILK_REJECT_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrclsDeduction += " and TSPL_LOCATION_MASTER.Loc_Segment_Code    IN (" + fndLoc.Value + ") "
        End If

        Query = "select final.fdate, tdate, FAT_KG, SNF_KG, Advance_Payment_Amount, companyADD, CompName, CompCode, Payable_Amount, Qty, Rate, Net_AMOUNT, ROUTE_CODE, Route_Name, final.VLC_Code, final.VSP_CODE, final.VLC_Name " & _
         ", GHEE, CATTLEFEED , OTHERS " & _
        "from (select      " & _
        " convert(varchar,('" + dtpFromDate.Value + "'),103) as fdate,convert(varchar,('" + dtpDate.Value + "'),103) as tdate,sum(TSPL_MILK_SRN_DETAIL .FAT_KG) as FAT_KG, " & _
        " sum(TSPL_MILK_SRN_DETAIL .SNF_KG) as SNF_KG,sum(ISNULL(MCCSALE.GHEE,0)) AS GHEE,sum(ISNULL(MCCSALE.CATTLEFEED,0)) AS CATTLEFEED ,sum(ISNULL(MCCSALE.OTHERS,0)) AS OTHERS, " & _
         "sum(PaymentProcess.Advance_Payment_Amount) as Advance_Payment_Amount,   max(TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE) as VSP_CODE, " & _
        "'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode," & _
        "sum(PaymentProcess.Payable_Amount) as Payable_Amount,sum(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty)as Qty ,sum( Price_Chart.milk_rate) as RATE, sum(TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT) as Net_AMOUNT, max(TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE) as ROUTE_CODE , max(TSPL_MCC_ROUTE_MASTER .Route_Name) as Route_Name , max(TSPL_VLC_MASTER_HEAD.VLC_Code) as VLC_Code,max(TSPL_VLC_MASTER_HEAD.VLC_Name) as VLC_Name " & _
        "from TSPL_MILK_PURCHASE_INVOICE_DETAIL Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD  On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " & _
        " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD .DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE " & _
        " left outer join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL .DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " & _
        "Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE = TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE " & _
         "Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE " & _
        "left outer join TSPL_MILK_RECEIPT_HEAD  on TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE " & _
         "left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE and TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE " & _
         "Left Outer Join TSPL_VENDOR_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE = TSPL_VENDOR_MASTER.Vendor_Code  And TSPL_VENDOR_MASTER.Form_Type = 'VSP' " & _
         "Left Outer Join TSPL_MCC_MASTER  On TSPL_MILK_RECEIPT_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code " & _
         "left join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code = TSPL_MCC_MASTER.MCC_Code " & _
         "Left Outer Join TSPL_MCC_ROUTE_MASTER  On TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code " & _
         "left outer join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO " & _
         "left join TSPL_CITY_MASTER as MCC_City  on MCC_City.city_code = TSPL_MCC_MASTER.City_code " & _
         "left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE = TSPL_MCC_MASTER.State_Code " & _
         "left join ( select sum(Advance_Payment_Amount) as Advance_Payment_Amount,sum(Advance_Payment_Amount_Knock_Off)as Advance_Payment_Amount_Knock_Off, max(doc_no) as doc_no," & _
          "max(convert(varchar, doc_date, 103)) as doc_date, max(from_date) as from_date, max(to_date) as to_date, sum(Service_Charge_Amt ) as Service_Charge_Amt, VLC_Code, VSP_CODE," & _
            "sum(Total_EMP_Amount) as Total_EMP_Amount, sum(Incentive_Amount) as Incentive_Amount, sum(Incentive_EMP_Amount) as Incentive_EMP_Amount, sum(EMP_Amount) as EMP_Amount, sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount, sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount," & _
            "sum(Deduction_Amount) as Deduction_Amount, sum(Item_Issue_Amount) as Item_Issue_Amount, sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount, sum(MCC_Sale_Amount) as MCC_Sale_Amount,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount " & _
               "from (select TSPL_PAYMENT_PROCESS_DETAIL.Advance_Payment_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Advance_Payment_Amount_Knock_Off, TSPL_PAYMENT_PROCESS_HEAD.doc_no," & _
          "TSPL_PAYMENT_PROCESS_HEAD.doc_date,TSPL_PAYMENT_PROCESS_HEAD.from_date, TSPL_PAYMENT_PROCESS_HEAD.to_date,tspl_payment_process_detail.milk_purchase_invoice_no as Doc_Code," & _
                        "TSPL_PAYMENT_PROCESS_DETAIL.Service_Charge_Amt,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount, TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount," & _
               "TSPL_VLC_MASTER_HEAD.VLC_Code, TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount," & _
              "TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from  TSPL_PAYMENT_PROCESS_DETAIL " & _
                       "left join TSPL_PAYMENT_PROCESS_HEAD  on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No " & _
                        "left join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader = TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & _
        "" & whrcls1 & "" & _
                        " )  as pp group by Doc_No,VSP_CODE,VLC_Code ) as PaymentProcess  on PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code  And PaymentProcess.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_Code " & _
         "left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code " & _
         "left join  ( select distinct FAT_Pers, SNF_Pers, Ratio as Fat_ratio, SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code, TSPL_FAT_SNF_UPLOADER_MASTER.code " & _
              " from TSPL_FAT_SNF_UPLOADER_MASTER  inner join TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code = TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code)" & _
            "as Price_Chart  on TSPL_MILK_SRN_DETAIL.Price_Code = Price_Chart.Code " & _
         " left join(" & _
       " select 'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Amount*(-1) as Amount,(CASE WHEN TSPL_ITEM_MASTER.ITEM_DESC LIKE '%GHEE%' THEN TSPL_SD_SHIPMENT_detail.Amount ELSE 0 END ) AS [GHEE],(CASE when TSPL_ITEM_MASTER.ITEM_DESC LIKE '%CATTLE%' THEN  TSPL_SD_SHIPMENT_detail.Amount ELSE 0 END) AS [CATTLEFEED],(CASE when TSPL_ITEM_MASTER.ITEM_DESC NOT LIKE '%CATTLE%' AND TSPL_ITEM_MASTER.ITEM_DESC NOT LIKE '%GHEE%'  THEN  TSPL_SD_SHIPMENT_detail.Amount ELSE 0 END) AS [OTHERS]   from TSPL_PAYMENT_PROCESS_MCC_SALE " & _
 " LEFT JOIN TSPL_PAYMENT_PROCESS_HEAD ON TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No " & _
   "left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " & _
 "left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code   " & _
 "WHERE convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) AND convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103)) AS MCCSALE " & _
" on MCCSALE.customer_code=TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE " & _
 "" & whrcls & " " & _
        " group by TSPL_VLC_MASTER_HEAD.VLC_Code )  as final "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Query)
        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptMilkProBulkPmtProcess_VLCWise", "VLC WISE DOC REPORT")
            frmCRV = Nothing
        End If

    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID + "M"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New System.IO.MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(MyBase.Form_ID + "M", objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID + "M", "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub gvAdvancePayment_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvAdvancePayment.CellValueChanged
        If e.Column Is gvAdvancePayment.Columns(colAPSelect) Then
            If Not isLoad Then
                isLoad = True
                loadGvData()
                isLoad = False
            End If
        End If
    End Sub
    Function CalculateFarmerPayment(ByVal VSP_Code As String) As Decimal
        Dim FarmerPayment As Decimal = 0
        For Each grow As GridViewRowInfo In gvPaymentToFarmer.Rows
            If clsCommon.CompairString(VSP_Code, clsCommon.myCstr(grow.Cells(colVendorCode).Value)) = CompairStringResult.Equal Then
                If clsCommon.myCBool(grow.Cells(colSelect).Value) = True Then
                    FarmerPayment = FarmerPayment + clsCommon.myCdbl(grow.Cells(colMPAmount).Value)
                End If
            End If

        Next
        Return FarmerPayment
    End Function
    '' new cols 
    Function CalculateFarmerSaleAmount(ByVal VSP_Code As String) As Decimal
        Dim SaleAmount As Decimal = 0
        For Each grow As GridViewRowInfo In gvPaymentToFarmer.Rows
            If clsCommon.CompairString(VSP_Code, clsCommon.myCstr(grow.Cells(colVendorCode).Value)) = CompairStringResult.Equal Then
                If clsCommon.myCBool(grow.Cells(colSelect).Value) = True Then
                    SaleAmount = SaleAmount + clsCommon.myCdbl(grow.Cells(colMccSaleTotalAmount).Value)
                End If
            End If

        Next
        Return SaleAmount
    End Function

    Function CalculateFarmerSaleReturnAmount(ByVal VSP_Code As String) As Decimal
        Dim ReturnAmount As Decimal = 0
        For Each grow As GridViewRowInfo In gvPaymentToFarmer.Rows
            If clsCommon.CompairString(VSP_Code, clsCommon.myCstr(grow.Cells(colVendorCode).Value)) = CompairStringResult.Equal Then
                If clsCommon.myCBool(grow.Cells(colSelect).Value) = True Then
                    ReturnAmount = ReturnAmount + clsCommon.myCdbl(grow.Cells(colMccSaleReturnTotalAmount).Value)
                End If
            End If

        Next
        Return ReturnAmount
    End Function

    Function CalculateFarmerAdjustmentAmount(ByVal VSP_Code As String) As Decimal
        Dim AdjustAmount As Decimal = 0
        For Each grow As GridViewRowInfo In gvPaymentToFarmer.Rows
            If clsCommon.CompairString(VSP_Code, clsCommon.myCstr(grow.Cells(colVendorCode).Value)) = CompairStringResult.Equal Then
                If clsCommon.myCBool(grow.Cells(colSelect).Value) = True Then
                    AdjustAmount = AdjustAmount + clsCommon.myCdbl(grow.Cells(colMPAdjustAmount).Value)
                End If
            End If

        Next
        Return AdjustAmount
    End Function

    Function CalculateFarmerPayableAmount(ByVal VSP_Code As String) As Decimal
        Dim PayableAmount As Decimal = 0
        For Each grow As GridViewRowInfo In gvPaymentToFarmer.Rows
            If clsCommon.CompairString(VSP_Code, clsCommon.myCstr(grow.Cells(colVendorCode).Value)) = CompairStringResult.Equal Then
                If clsCommon.myCBool(grow.Cells(colSelect).Value) = True Then
                    PayableAmount = PayableAmount + clsCommon.myCdbl(grow.Cells(colPaybleAmt).Value)
                End If
            End If
        Next
        Return PayableAmount
    End Function
    Function CalculateFarmerNextCycleDebitNote(ByVal VSP_Code As String) As Decimal
        Dim DebitNoteAmount As Decimal = 0
        For Each grow As GridViewRowInfo In gvPaymentToFarmer.Rows
            If clsCommon.CompairString(VSP_Code, clsCommon.myCstr(grow.Cells(colVendorCode).Value)) = CompairStringResult.Equal Then
                If clsCommon.myCBool(grow.Cells(colSelect).Value) = True Then
                    DebitNoteAmount = DebitNoteAmount + clsCommon.myCdbl(grow.Cells(colNextCycleDebitNoteFarmer).Value)
                End If
            End If

        Next
        Return DebitNoteAmount
    End Function
    Function CalculateFarmerMilkQty(ByVal VSP_Code As String) As Decimal
        Dim FarmerMilkQty As Decimal = 0
        For Each grow As GridViewRowInfo In gvPaymentToFarmer.Rows
            If clsCommon.CompairString(VSP_Code, clsCommon.myCstr(grow.Cells(colVendorCode).Value)) = CompairStringResult.Equal Then
                If clsCommon.myCBool(grow.Cells(colSelect).Value) = True Then
                    FarmerMilkQty = FarmerMilkQty + clsCommon.myCdbl(grow.Cells(colMilkQty).Value)
                End If
            End If

        Next
        Return FarmerMilkQty
    End Function
    Function CalculatePrevBalance(ByVal VSP_Code As String) As Decimal
        Dim prevBal As Decimal = 0
        For Each grow As GridViewRowInfo In gvDeduction.Rows
            If clsCommon.CompairString(VSP_Code, clsCommon.myCstr(grow.Cells(colVendorCode).Value)) = CompairStringResult.Equal Then
                If clsCommon.myCBool(grow.Cells(colSelect).Value) = True AndAlso clsCommon.myCdbl(grow.Cells(colIsFromPrevPPCycle).Value) = 1 Then
                    prevBal = prevBal + clsCommon.myCdbl(grow.Cells(colItemAmt).Value) - clsCommon.myCdbl(grow.Cells(colReduceDeduc).Value)
                End If
            End If
        Next
        Return prevBal
    End Function

    Private Sub gvPaymentToFarmer_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvPaymentToFarmer.CellValueChanged
        If Not isLoad Then
            isLoad = True
            If e.Column Is gvPaymentToFarmer.Columns(colSelect) Then
                loadGvData(True)
            ElseIf e.Column Is gvPaymentToFarmer.Columns(colDeductionAmt) Then
                CalculatePayableAmt(gvPaymentToFarmer.CurrentRow.Index)
                gvSetVSPDedAmount(clsCommon.myCstr(gvPaymentToFarmer.CurrentRow.Cells(colVendorCode).Value), -1)
            ElseIf e.Column Is gvPaymentToFarmer.Columns(colFATotalAdvanceRecovery) Then
                If clsCommon.myCdbl(gvPaymentToFarmer.CurrentRow.Cells(colFATotalAdvanceRecovery).Value) > clsCommon.myCdbl(gvPaymentToFarmer.CurrentRow.Cells(colFATotalAdvance).Value) Then
                    gvPaymentToFarmer.CurrentRow.Cells(colFATotalAdvanceRecovery).Value = 0
                End If
                CalculatePayableAmt(gvPaymentToFarmer.CurrentRow.Index)
            ElseIf e.Column Is gvPaymentToFarmer.Columns(colFALoanPaymentRecovery) Then
                If clsCommon.myCdbl(gvPaymentToFarmer.CurrentRow.Cells(colFALoanPaymentRecovery).Value) > clsCommon.myCdbl(gvPaymentToFarmer.CurrentRow.Cells(colFALoanPayment).Value) Then
                    gvPaymentToFarmer.CurrentRow.Cells(colFALoanPaymentRecovery).Value = 0
                End If
                CalculatePayableAmt(gvPaymentToFarmer.CurrentRow.Index)
            End If
            isLoad = False
        End If
    End Sub

    Sub gvSetVSPDedAmount(ByVal VSP_Code As String, ByVal Rowindex As Integer)
        If Rowindex < 0 Then
            For ii As Integer = 0 To gv.Rows.Count - 1
                If clsCommon.CompairString(VSP_Code, clsCommon.myCstr(gv.Rows(ii).Cells(colVendorCode).Value)) = CompairStringResult.Equal Then
                    Rowindex = ii
                    Exit For
                End If
            Next
        End If
        gv.Rows(Rowindex).Cells(colgvTotMPDeductionAmount).Value = gvCalculateTotDeduction(clsCommon.myCstr(gv.Rows(Rowindex).Cells(colVendorCode).Value))
        ''richa agarwal 01 July 2020
        ''gv.Rows(Rowindex).Cells(colNextCycleDebitNote).Value = clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colgvVSPExcessAmount).Value) - clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colgvTotMPDeductionAmount).Value)
        ''Balwinder on 16/01/2021
        'If clsCommon.myCdbl(clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colVSPAmount).Value) - clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colFarmerPayment).Value) - clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colgvVSPExcessAmount).Value) + clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colgvTotMPDeductionAmount).Value)) < 0 Then
        '    gv.Rows(Rowindex).Cells(colNextCycleDebitNote).Value = (clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colVSPAmount).Value) - clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colFarmerPayment).Value) - clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colgvVSPExcessAmount).Value) + clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colgvTotMPDeductionAmount).Value)) * -1
        'End If


        'If clsCommon.myCdbl(clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colVSPAmount).Value) - clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colFarmerPayment).Value) - clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colPrevCycleBalance).Value)) < 0 Then
        '    gv.Rows(Rowindex).Cells(colNextCycleDebitNote).Value = (clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colVSPAmount).Value) - clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colFarmerPayment).Value) - clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colPrevCycleBalance).Value)) * -1
        'End If
        'If clsCommon.myCdbl(clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colVSPAmount).Value) - clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colFarmerPayment).Value)) < 0 Then
        '    gv.Rows(Rowindex).Cells(colNextCycleDebitNote).Value = (clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colVSPAmount).Value) - clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colFarmerPayment).Value)) * -1
        'End If

        ''Currnet
        If clsCommon.myCdbl(clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value) - clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colFarmerPayment).Value)) < 0 Then
            gv.Rows(Rowindex).Cells(colNextCycleDebitNote).Value = (clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value) - clsCommon.myCdbl(gv.Rows(Rowindex).Cells(colFarmerPayment).Value)) * -1
        End If

    End Sub

    Function gvCalculateTotDeduction(ByVal VSP_Code As String) As Decimal
        Dim AdjustAmount As Decimal = 0
        For Each grow As GridViewRowInfo In gvPaymentToFarmer.Rows
            If clsCommon.CompairString(VSP_Code, clsCommon.myCstr(grow.Cells(colVendorCode).Value)) = CompairStringResult.Equal Then
                If clsCommon.myCBool(grow.Cells(colSelect).Value) = True Then
                    AdjustAmount = AdjustAmount + clsCommon.myCdbl(grow.Cells(colDeductionAmt).Value)
                End If
            End If
        Next
        Return AdjustAmount
    End Function

    Private Sub FrmPaymentProcess_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.J Then
            'clsCommon.MyMessageBoxShow("Showing Journal ")
            e.Handled = True
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
                chkSelected.Visible = True
            End If
        End If
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Reverse and unpost The payment Process " + Environment.NewLine + "Are You sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    If chkSelected.Checked Then
                        clsPaymentProcessFarmerHead.ReverseAndUnpostSelected(fndDocNo.Value)
                    Else
                        clsPaymentProcessFarmerHead.ReverseAndUnpost(fndDocNo.Value)
                    End If
                    clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text)
                    LoadData(fndDocNo.Value, NavigatorType.Current)
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPreFormatePrint_Click(sender As Object, e As EventArgs) Handles btnPreFormatePrint.Click
        PrintData(True)
    End Sub
    ' Ticet No : ERO/17/10/19-001064 By Prabhakar Pre -Formate print
    Sub PrintData(ByVal isPreFormatePrint)
        '        Try
        '            Dim qry As String = "Select SSSS.* , TBL_IncentiveDeduction.Incentive_Amount , TBL_IncentiveDeduction.Deduction_Amount from (" & _
        '                               " select Final.*, isnull (TBL_FOR_DEDUCTION.Amount,0) as DedAddAmount,TSPL_MP_MASTER.BankName as Bank_Code, tspl_vendor_bank_master.Bank_Name,TSPL_MP_MASTER.AccountNo,TSPL_MP_MASTER.IFCICode,TSPL_MP_MASTER.BankBranch, RIGHT( MP_VLC_Uploader_Code,3) as MP_VLC_Uploader_Code_Last3Degit from ( select TSPL_COMPANY_MASTER.Comp_Code," + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img, TSPL_COMPANY_MASTER.Comp_Name as CompName,'" + clsCommon.myCDate(dtpFromDate.Value) + "' as FromDate ,'" + clsCommon.myCDate(dtpToDate.Value) + "' as ToDate ,Convert (varchar,TSPL_VLC_DATA_UPLOADER.Doc_Date,103) as Doc_Date ,TSPL_VLC_DATA_UPLOADER.Milk_Type, TSPL_VLC_DATA_UPLOADER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Add1 as MCC_Add1 , TSPL_MCC_MASTER.Add2 as MCC_Add2 , TSPL_MCC_MASTER.City_code as MCC_City_Code, TSPL_CITY_MASTER_MCC.City_Name as MCC_City_Name,TSPL_MCC_MASTER.State_Code as MCC_State_Code,TSPL_STATE_MASTER_MCC.STATE_NAME as MCC_STATE_NAME,TSPL_MCC_MASTER.Pin_code as MCC_Pin_Code , TSPL_VLC_DATA_UPLOADER.shift , TSPL_VLC_DATA_UPLOADER.VLC_CODE as VLC_Code_VLC_Uploader , " & _
        '                               " TSPL_MP_MASTER.MP_Code as VSP_Code, TSPL_MP_MASTER.MP_Name as VSP_Name , TSPL_MP_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name " & _
        '                               " , TSPL_VLC_DATA_UPLOADER.Route_No , TSPL_VLC_DATA_UPLOADER.MP_CODE as MP_VLC_Uploader_Code ,TSPL_VLC_DATA_UPLOADER.Uom_Code, TSPL_VLC_DATA_UPLOADER.qty , TSPL_VLC_DATA_UPLOADER.fat as FAT_PER, TSPL_VLC_DATA_UPLOADER.snf as SNF_PER,TSPL_VLC_DATA_UPLOADER.Rate , TSPL_VLC_DATA_UPLOADER.Amount as Net_Amount from TSPL_VLC_DATA_UPLOADER " & _
        '                               " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_DATA_UPLOADER.MCC_Code " & _
        '                               " left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_MCC on TSPL_CITY_MASTER_MCC.City_Code =  TSPL_MCC_MASTER.City_code " & _
        '                               " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_MCC on TSPL_STATE_MASTER_MCC.State_Code = TSPL_MCC_MASTER.State_Code " & _
        '                               " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_UPLOADER = TSPL_VLC_DATA_UPLOADER.VLC_CODE " & _
        '                               " left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_VLC_DATA_UPLOADER.Comp_Code " & _
        '                               " left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code_VLC_Uploader = TSPL_VLC_DATA_UPLOADER.MP_CODE " & _
        '                               " where convert (date, TSPL_VLC_DATA_UPLOADER.Doc_Date,103) > = (select TSPL_PAYMENT_PROCESS_HEAD.From_Date  from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "')  " & _
        '                               " and convert (date, TSPL_VLC_DATA_UPLOADER.Doc_Date,103) < = (select  TSPL_PAYMENT_PROCESS_HEAD.To_Date from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "') " & _
        '                               " and TSPL_VLC_DATA_UPLOADER.MP_CODE in (select distinct TSPL_MP_MASTER.MP_Code_VLC_Uploader from TSPL_MP_PAY_PROCESS_DETAIL inner join TSPL_MP_MASTER on TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code = TSPL_MP_MASTER.MP_Code  where Doc_No = '" + fndDocNo.Value + "') " & _
        '                               " ) Final  " & _
        '                               " left outer join  (select Final.VSP_Code  ,sum (Final.Item_Net_Amt) as Amount from ( " & _
        '                               " select TSPL_MP_PAY_PROCESS_MCC_SALE.FARMER_CODE as VSP_Code,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_SALE_FARMER_DETAIL.Item_Net_Amt * -1 as Item_Net_Amt from TSPL_MP_PAY_PROCESS_MCC_SALE inner join TSPL_MCC_SALE_FARMER_DETAIL on TSPL_MP_PAY_PROCESS_MCC_SALE.Shipment_Doc_No = TSPL_MCC_SALE_FARMER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_FARMER_DETAIL.Item_Code where TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocNo.Value + "' " & _
        '                               " Union All " & _
        '                               " select TSPL_MCC_SALE_RETURN_HEAD_FARMER.FARMER_CODE as  VSP_Code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount  as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN   inner join TSPL_MCC_SALE_RETURN_DETAIL_FARMER on TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Item_Code   left outer join TSPL_MCC_SALE_RETURN_HEAD_FARMER on TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Document_Code   where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" + fndDocNo.Value + "' " & _
        '                               " ) Final group by Final.VSP_Code ) As TBL_FOR_DEDUCTION on TBL_FOR_DEDUCTION.VSP_Code = Final.VSP_Code " & _
        '                               " Left Outer Join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = Final.VSP_Code " & _
        '                               " left Outer Join tspl_vendor_bank_master on tspl_vendor_bank_master.Bank_Code = TSPL_MP_MASTER.BankName " & _
        '                               "  " & _
        '                               " Union All" & _
        '                               " select Final.*, isnull (TBL_FOR_DEDUCTION.Amount,0) as DedAddAmount,TSPL_MP_MASTER.BankName as Bank_Code, tspl_vendor_bank_master.Bank_Name,TSPL_MP_MASTER.AccountNo,TSPL_MP_MASTER.IFCICode,TSPL_MP_MASTER.BankBranch, RIGHT( MP_VLC_Uploader_Code,3) as MP_VLC_Uploader_Code_Last3Degit from ( select TSPL_COMPANY_MASTER.Comp_Code, " + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img, TSPL_COMPANY_MASTER.Comp_Name as CompName,'" + clsCommon.myCDate(dtpToDate.Value) + "' as FromDate ,'" + clsCommon.myCDate(dtpToDate.Value) + "' as ToDate ,Convert (varchar,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) as Doc_Date ,'C' as Milk_Type,TSPL_VLC_MASTER_HEAD_ForMCC.MCC " & _
        '                               " as MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Add1 as MCC_Add1 , TSPL_MCC_MASTER.Add2 as MCC_Add2 , TSPL_MCC_MASTER.City_code as MCC_City_Code, TSPL_CITY_MASTER_MCC.City_Name as MCC_City_Name,TSPL_MCC_MASTER.State_Code as MCC_State_Code,TSPL_STATE_MASTER_MCC.STATE_NAME as MCC_STATE_NAME,TSPL_MCC_MASTER.Pin_code as MCC_Pin_Code , case when  TSPL_VLC_DATA_UPLOADER_MASTER.Shift = 'MORNING' then 'M' else 'E' end as Shift , TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader  as VLC_Code_VLC_Uploader ,  TSPL_MP_MASTER.MP_Code as VSP_Code, TSPL_MP_MASTER.MP_Name as VSP_Name , TSPL_MP_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name  , TSPL_VLC_DATA_UPLOADER_MASTER.Route_Code as Route_No , TSPL_MP_MASTER.MP_Code_VLC_Uploader as MP_VLC_Uploader_Code ,TSPL_VLC_DATA_UPLOADER_DETAIL.Unit_Code as Uom_Code, TSPL_VLC_DATA_UPLOADER_DETAIL.Qty , TSPL_VLC_DATA_UPLOADER_DETAIL.fatPer as FAT_PER, TSPL_VLC_DATA_UPLOADER_DETAIL.SNFPer as SNF_PER,TSPL_VLC_DATA_UPLOADER_DETAIL.Rate , TSPL_VLC_DATA_UPLOADER_DETAIL.Amount as Net_Amount from  TSPL_VLC_DATA_UPLOADER_DETAIL left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code = TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code " & _
        '                               " Left Outer Join TSPL_VLC_MASTER_HEAD as TSPL_VLC_MASTER_HEAD_ForMCC on TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code=TSPL_VLC_MASTER_HEAD_ForMCC.VLC_Code  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code" & _
        '                               " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD_ForMCC.MCC  left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_MCC on TSPL_CITY_MASTER_MCC.City_Code =  TSPL_MCC_MASTER.City_code  left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_MCC on TSPL_STATE_MASTER_MCC.State_Code = TSPL_MCC_MASTER.State_Code    left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_VLC_DATA_UPLOADER_MASTER.Comp_Code  left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code  where convert (date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) > = (select TSPL_PAYMENT_PROCESS_HEAD.From_Date  from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "')   and convert (date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) < = (select  TSPL_PAYMENT_PROCESS_HEAD.To_Date from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "')  and TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code in (select distinct TSPL_MP_MASTER.MP_Code from TSPL_MP_PAY_PROCESS_DETAIL inner join TSPL_MP_MASTER on TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code = TSPL_MP_MASTER.MP_Code  where Doc_No = '" + fndDocNo.Value + "')  ) Final   left outer join  (select Final.VSP_Code  ,sum (Final.Item_Net_Amt) as Amount from (  select TSPL_MP_PAY_PROCESS_MCC_SALE.FARMER_CODE as VSP_Code,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_SALE_FARMER_DETAIL.Item_Net_Amt * -1 as Item_Net_Amt from TSPL_MP_PAY_PROCESS_MCC_SALE inner join TSPL_MCC_SALE_FARMER_DETAIL on TSPL_MP_PAY_PROCESS_MCC_SALE.Shipment_Doc_No = TSPL_MCC_SALE_FARMER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_FARMER_DETAIL.Item_Code where TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocNo.Value + "'  Union All  select TSPL_MCC_SALE_RETURN_HEAD_FARMER.FARMER_CODE as  VSP_Code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount  as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN   inner join TSPL_MCC_SALE_RETURN_DETAIL_FARMER on TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Item_Code   left outer join TSPL_MCC_SALE_RETURN_HEAD_FARMER on TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Document_Code   where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" + fndDocNo.Value + "'  ) Final group by Final.VSP_Code ) As TBL_FOR_DEDUCTION on TBL_FOR_DEDUCTION.VSP_Code = Final.VSP_Code  Left Outer Join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = Final.VSP_Code  left Outer Join tspl_vendor_bank_master on tspl_vendor_bank_master.Bank_Code = TSPL_MP_MASTER.BankName " & _
        '                               " ) SSSS  left outer join  ( select VLC_CODE_Uploader,Farmer_Code, Sum ( isNull (Incentive_Amount,0)) as Incentive_Amount , Sum ( isnull(Deduction_Amount,0)) as Deduction_Amount from TSPL_MP_PAY_PROCESS_DETAIL where Doc_No = '" + fndDocNo.Value + "' group by VLC_CODE_Uploader,Farmer_Code ) TBL_IncentiveDeduction on TBL_IncentiveDeduction.VLC_CODE_Uploader = SSSS.VLC_Code_VLC_Uploader and TBL_IncentiveDeduction.Farmer_Code = SSSS.VSP_Code    "
        '            If isPreFormatePrint = True Then
        '                qry = " select TBL_Farmer_Invoice.Farmer_Invoice_No,VLC_Code_VLC_Uploader,TBL_Farmer_Invoice.MP_Adjust_Amount as OutStanding_Amt ,RIGHT ( FinalXXX.VLC_Code_VLC_Uploader,3) as VLC_Code_VLC_Uploader_3Digit, FinalXXX.Comp_Code," + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img ,FinalXXX.CompName as CompName,FinalXXX.FromDate as FromDate ,FinalXXX.ToDate  as ToDate,FinalXXX.Doc_Date,FinalXXX.Milk_Type as Milk_Type,FinalXXX.MCC_Code as MCC_Code,FinalXXX.MCC_NAME as MCC_NAME,FinalXXX.MCC_ADD1 as MCC_ADD1,FinalXXX.MCC_Add2 as MCC_Add2,FinalXXX.MCC_City_Code as MCC_City_Code,FinalXXX.MCC_City_Name as MCC_City_Name,FinalXXX.MCC_State_Code as MCC_State_Code,FinalXXX.MCC_STATE_NAME as MCC_STATE_NAME,FinalXXX.MCC_Pin_Code as MCC_Pin_Code,FinalXXX.shift,FinalXXX.VLC_Code_VLC_Uploader as VLC_Code_VLC_Uploader,FinalXXX.VSP_Code as VSP_Code,FinalXXX.VSP_Name as VSP_Name,FinalXXX.VLC_Code as VLC_Code,FinalXXX.VLC_Name as VLC_Name,FinalXXX.Route_No as Route_No ,FinalXXX.MP_VLC_Uploader_Code as MP_VLC_Uploader_Code,FinalXXX.Uom_Code as Uom_Code,FinalXXX.qty as qty , " & _
        '                      " convert ( decimal(18,2), ((FinalXXX.Fat_KG * 100) / nullif (FinalXXX.Qty_In_KG,0) ))  as FAT_PER  ,convert (decimal(18,2), ((FinalXXX.SNF_KG * 100 )/ nullif (FinalXXX.Qty_In_KG,0)))   as SNF_PER,Rate as Rate,FinalXXX.Net_Amount as Net_Amount,FinalXXX.DedAddAmount as DedAddAmount,FinalXXX.Bank_Code as Bank_Code,FinalXXX.Bank_Name as Bank_Name ,FinalXXX.AccountNO as AccountNO,FinalXXX.IFCICode as IFCICode ,FinalXXX.BankBranch as BankBranch,FinalXXX.MP_VLC_Uploader_Code_Last3Degit as MP_VLC_Uploader_Code_Last3Degit, FinalXXX.Incentive_Amount as Incentive_Amount ,FinalXXX.Deduction_Amount as Deduction_Amount  " & _
        '" ,TBL_Farmer_Invoice.FeedAmount ,TBL_Farmer_Invoice .LoanAmount ,TBL_Farmer_Invoice .OtherAmount,TSPL_MCC_ROUTE_MASTER.Route_Name   " & _
        ' " from ( " & _
        '                      " select " & _
        '                      " max(XXXXX.Comp_Code) as Comp_Code, " & _
        '                      " max(XXXXX.CompName) as CompName,max(XXXXX.FromDate) as FromDate ,max(XXXXX.ToDate)  as ToDate,XXXXX.Doc_Date,max(XXXXX.Milk_Type) as Milk_Type,max(XXXXX.MCC_Code) as MCC_Code,max(XXXXX.MCC_NAME) as MCC_NAME,max(XXXXX.MCC_ADD1) as MCC_ADD1,max(XXXXX.MCC_Add2) as MCC_Add2,max(XXXXX.MCC_City_Code) as MCC_City_Code,max(XXXXX.MCC_City_Name) as MCC_City_Name,max(XXXXX.MCC_State_Code) as MCC_State_Code,max(XXXXX.MCC_STATE_NAME) as MCC_STATE_NAME,max(XXXXX.MCC_Pin_Code) as MCC_Pin_Code,XXXXX.shift,max(XXXXX.VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,XXXXX.VSP_Code as VSP_Code,max(XXXXX.VSP_Name) as VSP_Name,max(XXXXX.VLC_Code) as VLC_Code,max(XXXXX.VLC_Name) as VLC_Name,max(XXXXX.Route_No) as Route_No ,max(XXXXX.MP_VLC_Uploader_Code) as MP_VLC_Uploader_Code,max(XXXXX.Uom_Code) as Uom_Code,sum(XXXXX.qty) as qty ," & _
        '                      " Sum (XXXXX.FAT_KG) as FAT_KG , Sum(SNF_KG) as SNF_KG , sum (Qty_In_KG) as Qty_In_KG,convert(decimal(18,2), sum (XXXXX.Net_Amount) / nullif ( sum(XXXXX.qty),0)) as Rate,sum (XXXXX.Net_Amount) as Net_Amount,sum (XXXXX.DedAddAmount) as DedAddAmount,max(XXXXX.Bank_Code) as Bank_Code,max(XXXXX.Bank_Name) as Bank_Name ,max(XXXXX.AccountNO) as AccountNO,max(XXXXX.IFCICode) as IFCICode ,max(XXXXX.BankBranch) as BankBranch,max(XXXXX.MP_VLC_Uploader_Code_Last3Degit) as MP_VLC_Uploader_Code_Last3Degit, max(XXXXX.Incentive_Amount) as Incentive_Amount ,max( XXXXX.Deduction_Amount) as Deduction_Amount from ( " & _
        '                      " Select  SSSSFinal.Comp_Code, " & _
        '                      " SSSSFinal.Logo_Img,SSSSFinal.CompName,SSSSFinal.FromDate,SSSSFinal.ToDate,SSSSFinal.Doc_Date,SSSSFinal.Milk_Type,SSSSFinal.MCC_Code,SSSSFinal.MCC_NAME,SSSSFinal.MCC_ADD1,SSSSFinal.MCC_Add2,SSSSFinal.MCC_City_Code,SSSSFinal.MCC_City_Name,SSSSFinal.MCC_State_Code,SSSSFinal.MCC_STATE_NAME,SSSSFinal.MCC_Pin_Code,SSSSFinal.shift,SSSSFinal.VLC_Code_VLC_Uploader,SSSSFinal.VSP_Code,SSSSFinal.VSP_Name,SSSSFinal.VLC_Code,SSSSFinal.VLC_Name,SSSSFinal.Route_No,SSSSFinal.MP_VLC_Uploader_Code,SSSSFinal.Uom_Code,SSSSFinal.qty,SSSSFinal.FAT_PER,SSSSFinal.SNF_PER,SSSSFinal.Rate,SSSSFinal.Net_Amount,SSSSFinal.DedAddAmount,SSSSFinal.Bank_Code,SSSSFinal.Bank_Name,SSSSFinal.AccountNO,SSSSFinal.IFCICode,SSSSFinal.BankBranch,SSSSFinal.MP_VLC_Uploader_Code_Last3Degit, SSSSFinal.Incentive_Amount , SSSSFinal.Deduction_Amount ,(SSSSFinal.FAT_PER *  (zzz.CF*SSSSFinal.Qty) ) /100  as FAT_KG,   (SSSSFinal.SNF_PER * (zzz.CF*SSSSFinal.Qty) ) /100  as  SNF_KG,zzz.CF*SSSSFinal.Qty as Qty_In_KG " & _
        '                      " from ( " & _
        '                      " " + qry + "  " & _
        '                      " ) SSSSFinal left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/  nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =SSSSFinal.UOM_CODE and lower(zzz.TOUOM)='KG' " & _
        '                      " )   XXXXX " & _
        '                      " Group by XXXXX.VSP_Code,XXXXX.Doc_Date,XXXXX.shift " & _
        '                      " ) FinalXXX left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = FinalXXX.Comp_Code " & _
        '                      "  left outer join (select   Farmer_Code ,left ( Farmer_Invoice_No,(len(Farmer_Invoice_No)- ( len(Farmer_Code)+1 ))) as Farmer_Invoice_No ,MP_Adjust_Amount,Deduction_Amount as OtherAmount,MCC_Sale_Amount-MCC_Sale_Return_Amount as FeedAmount,Total_Advance_Amount as LoanAmount  from TSPL_MP_PAY_PROCESS_DETAIL  where  TSPL_MP_PAY_PROCESS_DETAIL.Doc_No = '" + fndDocNo.Value + "') as  TBL_Farmer_Invoice on FinalXXX.VSP_CODE = TBL_Farmer_Invoice.Farmer_Code left outer join TSPL_MCC_ROUTE_MASTER  on TSPL_MCC_ROUTE_MASTER .route_code=finalxxx.Route_No " & _
        '                      " order by convert (date,FinalXXX.Doc_Date,103) asc ,FinalXXX.shift desc "
        '            Else
        '                qry = qry + " order by convert (date,SSSS.Doc_Date,103) asc ,SSSS.shift desc "
        '            End If

        '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '            qry = "  select Final.VSP_Code ,Final.Item_Code,max(Final.Item_Desc) as Item_Desc ,sum (Final.Item_Net_Amt) as Amount from ( " & _
        '                  "  select TSPL_MP_PAY_PROCESS_MCC_SALE.FARMER_CODE as VSP_Code,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_SALE_FARMER_DETAIL.Item_Net_Amt * -1 as Item_Net_Amt from TSPL_MP_PAY_PROCESS_MCC_SALE inner join TSPL_MCC_SALE_FARMER_DETAIL on TSPL_MP_PAY_PROCESS_MCC_SALE.Shipment_Doc_No = TSPL_MCC_SALE_FARMER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_FARMER_DETAIL.Item_Code where TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocNo.Value + "' " & _
        '                  "  Union All " & _
        '                  "  select TSPL_MCC_SALE_RETURN_HEAD_FARMER.FARMER_CODE as  VSP_Code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount  as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN   inner join TSPL_MCC_SALE_RETURN_DETAIL_FARMER on TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Item_Code   left outer join TSPL_MCC_SALE_RETURN_HEAD_FARMER on TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Document_Code   where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" + fndDocNo.Value + "' " & _
        '                  " ) Final group by Final.VSP_Code ,Final.Item_Code "
        '            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
        '            If dt IsNot Nothing And dt.Rows.Count > 0 Then
        '                Dim frmCRV As New frmCrystalReportViewer()
        '                If isPreFormatePrint = True Then
        '                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dt2, "crptPaymentProcessFarmerPredefineFormate", "SubMilkPurchaseBill.rpt", "SubMilkPurchaseBill.rpt", "Address.rpt")
        '                    frmCRV = Nothing
        '                Else
        '                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dt2, "crptPaymentProcessFarmer", "SubMilkPurchaseBill.rpt", "SubMilkPurchaseBill.rpt", "Address.rpt")
        '                    frmCRV = Nothing
        '                End If
        '            Else
        '                clsCommon.MyMessageBoxShow("No Data Found")
        '            End If
        '        Catch ex As Exception
        '            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        '        End Try
        Try
            Dim qry As String = "Select SSSS.* , TBL_IncentiveDeduction.Incentive_Amount , TBL_IncentiveDeduction.Deduction_Amount from (" & _
                               " select Final.*, isnull (TBL_FOR_DEDUCTION.Amount,0) as DedAddAmount,TSPL_MP_MASTER.BankName as Bank_Code, tspl_vendor_bank_master.Bank_Name,TSPL_MP_MASTER.AccountNo,TSPL_MP_MASTER.IFCICode,TSPL_MP_MASTER.BankBranch, RIGHT( MP_VLC_Uploader_Code,3) as MP_VLC_Uploader_Code_Last3Degit from ( select TSPL_COMPANY_MASTER.Comp_Code," + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img, TSPL_COMPANY_MASTER.Comp_Name as CompName,'" + clsCommon.myCDate(dtpFromDate.Value) + "' as FromDate ,'" + clsCommon.myCDate(dtpToDate.Value) + "' as ToDate ,Convert (varchar,TSPL_VLC_DATA_UPLOADER.Doc_Date,103) as Doc_Date ,TSPL_VLC_DATA_UPLOADER.Milk_Type, TSPL_VLC_DATA_UPLOADER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Add1 as MCC_Add1 , TSPL_MCC_MASTER.Add2 as MCC_Add2 , TSPL_MCC_MASTER.City_code as MCC_City_Code, TSPL_CITY_MASTER_MCC.City_Name as MCC_City_Name,TSPL_MCC_MASTER.State_Code as MCC_State_Code,TSPL_STATE_MASTER_MCC.STATE_NAME as MCC_STATE_NAME,TSPL_MCC_MASTER.Pin_code as MCC_Pin_Code , TSPL_VLC_DATA_UPLOADER.shift , TSPL_VLC_DATA_UPLOADER.VLC_CODE as VLC_Code_VLC_Uploader , " & _
                               " TSPL_MP_MASTER.MP_Code as VSP_Code, TSPL_MP_MASTER.MP_Name as VSP_Name , TSPL_MP_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name " & _
                               " , TSPL_VLC_DATA_UPLOADER.Route_No , TSPL_VLC_DATA_UPLOADER.MP_CODE as MP_VLC_Uploader_Code ,TSPL_VLC_DATA_UPLOADER.Uom_Code, TSPL_VLC_DATA_UPLOADER.qty , TSPL_VLC_DATA_UPLOADER.fat as FAT_PER, TSPL_VLC_DATA_UPLOADER.snf as SNF_PER,TSPL_VLC_DATA_UPLOADER.Rate , TSPL_VLC_DATA_UPLOADER.Amount as Net_Amount from TSPL_VLC_DATA_UPLOADER " & _
                               " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_DATA_UPLOADER.MCC_Code " & _
                               " left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_MCC on TSPL_CITY_MASTER_MCC.City_Code =  TSPL_MCC_MASTER.City_code " & _
                               " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_MCC on TSPL_STATE_MASTER_MCC.State_Code = TSPL_MCC_MASTER.State_Code " & _
                               " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_UPLOADER = TSPL_VLC_DATA_UPLOADER.VLC_CODE " & _
                               " left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_VLC_DATA_UPLOADER.Comp_Code " & _
                               " left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code_VLC_Uploader = TSPL_VLC_DATA_UPLOADER.MP_CODE " & _
                               " where convert (date, TSPL_VLC_DATA_UPLOADER.Doc_Date,103) > = (select TSPL_PAYMENT_PROCESS_HEAD.From_Date  from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "')  " & _
                               " and convert (date, TSPL_VLC_DATA_UPLOADER.Doc_Date,103) < = (select  TSPL_PAYMENT_PROCESS_HEAD.To_Date from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "') " & _
                               " and TSPL_VLC_DATA_UPLOADER.MP_CODE in (select distinct TSPL_MP_MASTER.MP_Code_VLC_Uploader from TSPL_MP_PAY_PROCESS_DETAIL inner join TSPL_MP_MASTER on TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code = TSPL_MP_MASTER.MP_Code  where Doc_No = '" + fndDocNo.Value + "') " & _
                               " ) Final  " & _
                               " left outer join  (select Final.VSP_Code  ,sum (Final.Item_Net_Amt) as Amount from ( " & _
                               " select TSPL_MP_PAY_PROCESS_MCC_SALE.FARMER_CODE as VSP_Code,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_SALE_FARMER_DETAIL.Item_Net_Amt * -1 as Item_Net_Amt from TSPL_MP_PAY_PROCESS_MCC_SALE inner join TSPL_MCC_SALE_FARMER_DETAIL on TSPL_MP_PAY_PROCESS_MCC_SALE.Shipment_Doc_No = TSPL_MCC_SALE_FARMER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_FARMER_DETAIL.Item_Code where TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocNo.Value + "' " & _
                               " Union All " & _
                               " select TSPL_MCC_SALE_RETURN_HEAD_FARMER.FARMER_CODE as  VSP_Code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount  as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN   inner join TSPL_MCC_SALE_RETURN_DETAIL_FARMER on TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Item_Code   left outer join TSPL_MCC_SALE_RETURN_HEAD_FARMER on TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Document_Code   where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" + fndDocNo.Value + "' " & _
                               " ) Final group by Final.VSP_Code ) As TBL_FOR_DEDUCTION on TBL_FOR_DEDUCTION.VSP_Code = Final.VSP_Code " & _
                               " Left Outer Join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = Final.VSP_Code " & _
                               " left Outer Join tspl_vendor_bank_master on tspl_vendor_bank_master.Bank_Code = TSPL_MP_MASTER.BankName " & _
                               "  " & _
                               " Union All" & _
                               " select Final.*, isnull (TBL_FOR_DEDUCTION.Amount,0) as DedAddAmount,TSPL_MP_MASTER.BankName as Bank_Code, tspl_vendor_bank_master.Bank_Name,TSPL_MP_MASTER.AccountNo,TSPL_MP_MASTER.IFCICode,TSPL_MP_MASTER.BankBranch, RIGHT( MP_VLC_Uploader_Code,3) as MP_VLC_Uploader_Code_Last3Degit from ( select TSPL_COMPANY_MASTER.Comp_Code, " + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img, TSPL_COMPANY_MASTER.Comp_Name as CompName,'" + clsCommon.myCDate(dtpFromDate.Value) + "' as FromDate ,'" + clsCommon.myCDate(dtpToDate.Value) + "' as ToDate ,Convert (varchar,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) as Doc_Date ,'C' as Milk_Type,TSPL_VLC_MASTER_HEAD_ForMCC.MCC " & _
                               " as MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Add1 as MCC_Add1 , TSPL_MCC_MASTER.Add2 as MCC_Add2 , TSPL_MCC_MASTER.City_code as MCC_City_Code, TSPL_CITY_MASTER_MCC.City_Name as MCC_City_Name,TSPL_MCC_MASTER.State_Code as MCC_State_Code,TSPL_STATE_MASTER_MCC.STATE_NAME as MCC_STATE_NAME,TSPL_MCC_MASTER.Pin_code as MCC_Pin_Code , case when  TSPL_VLC_DATA_UPLOADER_MASTER.Shift = 'MORNING' then 'M' else 'E' end as Shift , TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader  as VLC_Code_VLC_Uploader ,  TSPL_MP_MASTER.MP_Code as VSP_Code, TSPL_MP_MASTER.MP_Name as VSP_Name , TSPL_MP_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name  , TSPL_VLC_DATA_UPLOADER_MASTER.Route_Code as Route_No , TSPL_MP_MASTER.MP_Code_VLC_Uploader as MP_VLC_Uploader_Code ,TSPL_VLC_DATA_UPLOADER_DETAIL.Unit_Code as Uom_Code, TSPL_VLC_DATA_UPLOADER_DETAIL.Qty , TSPL_VLC_DATA_UPLOADER_DETAIL.fatPer as FAT_PER, TSPL_VLC_DATA_UPLOADER_DETAIL.SNFPer as SNF_PER,TSPL_VLC_DATA_UPLOADER_DETAIL.Rate , TSPL_VLC_DATA_UPLOADER_DETAIL.Amount as Net_Amount from  TSPL_VLC_DATA_UPLOADER_DETAIL left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code = TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code " & _
                               " Left Outer Join TSPL_VLC_MASTER_HEAD as TSPL_VLC_MASTER_HEAD_ForMCC on TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code=TSPL_VLC_MASTER_HEAD_ForMCC.VLC_Code  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code" & _
                               " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD_ForMCC.MCC  left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_MCC on TSPL_CITY_MASTER_MCC.City_Code =  TSPL_MCC_MASTER.City_code  left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_MCC on TSPL_STATE_MASTER_MCC.State_Code = TSPL_MCC_MASTER.State_Code    left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_VLC_DATA_UPLOADER_MASTER.Comp_Code  left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code  where convert (date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) > = (select TSPL_PAYMENT_PROCESS_HEAD.From_Date  from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "')   and convert (date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) < = (select  TSPL_PAYMENT_PROCESS_HEAD.To_Date from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "')  and TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code in (select distinct TSPL_MP_MASTER.MP_Code from TSPL_MP_PAY_PROCESS_DETAIL inner join TSPL_MP_MASTER on TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code = TSPL_MP_MASTER.MP_Code  where Doc_No = '" + fndDocNo.Value + "')  ) Final   left outer join  (select Final.VSP_Code  ,sum (Final.Item_Net_Amt) as Amount from (  select TSPL_MP_PAY_PROCESS_MCC_SALE.FARMER_CODE as VSP_Code,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_SALE_FARMER_DETAIL.Item_Net_Amt * -1 as Item_Net_Amt from TSPL_MP_PAY_PROCESS_MCC_SALE inner join TSPL_MCC_SALE_FARMER_DETAIL on TSPL_MP_PAY_PROCESS_MCC_SALE.Shipment_Doc_No = TSPL_MCC_SALE_FARMER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_FARMER_DETAIL.Item_Code where TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocNo.Value + "'  Union All  select TSPL_MCC_SALE_RETURN_HEAD_FARMER.FARMER_CODE as  VSP_Code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount  as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN   inner join TSPL_MCC_SALE_RETURN_DETAIL_FARMER on TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Item_Code   left outer join TSPL_MCC_SALE_RETURN_HEAD_FARMER on TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Document_Code   where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" + fndDocNo.Value + "'  ) Final group by Final.VSP_Code ) As TBL_FOR_DEDUCTION on TBL_FOR_DEDUCTION.VSP_Code = Final.VSP_Code  Left Outer Join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = Final.VSP_Code  left Outer Join tspl_vendor_bank_master on tspl_vendor_bank_master.Bank_Code = TSPL_MP_MASTER.BankName " & _
                               " ) SSSS  left outer join  ( select VLC_CODE_Uploader,Farmer_Code, Sum ( isNull (Incentive_Amount,0)) as Incentive_Amount , Sum ( isnull(Deduction_Amount,0)) as Deduction_Amount from TSPL_MP_PAY_PROCESS_DETAIL where Doc_No = '" + fndDocNo.Value + "' group by VLC_CODE_Uploader,Farmer_Code ) TBL_IncentiveDeduction on TBL_IncentiveDeduction.VLC_CODE_Uploader = SSSS.VLC_Code_VLC_Uploader and TBL_IncentiveDeduction.Farmer_Code = SSSS.VSP_Code    "
            If isPreFormatePrint = True Then
                qry = " select TBL_Farmer_Invoice.Farmer_Invoice_No,VLC_Code_VLC_Uploader,TBL_Farmer_Invoice.MP_Adjust_Amount as OutStanding_Amt ,RIGHT ( FinalXXX.VLC_Code_VLC_Uploader,3) as VLC_Code_VLC_Uploader_3Digit, FinalXXX.Comp_Code," + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img ,FinalXXX.CompName as CompName,FinalXXX.FromDate as FromDate ,FinalXXX.ToDate  as ToDate,FinalXXX.Doc_Date,FinalXXX.Milk_Type as Milk_Type,FinalXXX.MCC_Code as MCC_Code,FinalXXX.MCC_NAME as MCC_NAME,FinalXXX.MCC_ADD1 as MCC_ADD1,FinalXXX.MCC_Add2 as MCC_Add2,FinalXXX.MCC_City_Code as MCC_City_Code,FinalXXX.MCC_City_Name as MCC_City_Name,FinalXXX.MCC_State_Code as MCC_State_Code,FinalXXX.MCC_STATE_NAME as MCC_STATE_NAME,FinalXXX.MCC_Pin_Code as MCC_Pin_Code,FinalXXX.shift,FinalXXX.VLC_Code_VLC_Uploader as VLC_Code_VLC_Uploader,FinalXXX.VSP_Code as VSP_Code,FinalXXX.VSP_Name as VSP_Name,FinalXXX.VLC_Code as VLC_Code,FinalXXX.VLC_Name as VLC_Name,FinalXXX.Route_No as Route_No ,FinalXXX.MP_VLC_Uploader_Code as MP_VLC_Uploader_Code,FinalXXX.Uom_Code as Uom_Code,FinalXXX.qty as qty , " & _
                      " convert ( decimal(18,2), ((FinalXXX.Fat_KG * 100) / nullif (FinalXXX.Qty_In_KG,0) ))  as FAT_PER  ,convert (decimal(18,2), ((FinalXXX.SNF_KG * 100 )/ nullif (FinalXXX.Qty_In_KG,0)))   as SNF_PER,Rate as Rate,FinalXXX.Net_Amount as Net_Amount,FinalXXX.DedAddAmount as DedAddAmount,FinalXXX.Bank_Code as Bank_Code,FinalXXX.Bank_Name as Bank_Name ,FinalXXX.AccountNO as AccountNO,FinalXXX.IFCICode as IFCICode ,FinalXXX.BankBranch as BankBranch,FinalXXX.MP_VLC_Uploader_Code_Last3Degit as MP_VLC_Uploader_Code_Last3Degit, FinalXXX.Incentive_Amount as Incentive_Amount ,FinalXXX.Deduction_Amount as Deduction_Amount  " & _
" ,TBL_Farmer_Invoice.FeedAmount ,TBL_Farmer_Invoice .LoanAmount ,TBL_Farmer_Invoice .OtherAmount,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_EMPLOYEE_MASTER.Emp_Name AS SuperVisorName ,TBL_Farmer_Invoice.Farmer_Name ,isnull (TBL_Farmer_Invoice.IncentivePerLitre,0) as  IncentivePerLitre " & _
 " from ( " & _
                      " select " & _
                      " max(XXXXX.Comp_Code) as Comp_Code, " & _
                      " max(XXXXX.CompName) as CompName,max(XXXXX.FromDate) as FromDate ,max(XXXXX.ToDate)  as ToDate,XXXXX.Doc_Date,max(XXXXX.Milk_Type) as Milk_Type,max(XXXXX.MCC_Code) as MCC_Code,max(XXXXX.MCC_NAME) as MCC_NAME,max(XXXXX.MCC_ADD1) as MCC_ADD1,max(XXXXX.MCC_Add2) as MCC_Add2,max(XXXXX.MCC_City_Code) as MCC_City_Code,max(XXXXX.MCC_City_Name) as MCC_City_Name,max(XXXXX.MCC_State_Code) as MCC_State_Code,max(XXXXX.MCC_STATE_NAME) as MCC_STATE_NAME,max(XXXXX.MCC_Pin_Code) as MCC_Pin_Code,XXXXX.shift,max(XXXXX.VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,XXXXX.VSP_Code as VSP_Code,max(XXXXX.VSP_Name) as VSP_Name,max(XXXXX.VLC_Code) as VLC_Code,max(XXXXX.VLC_Name) as VLC_Name,max(XXXXX.Route_No) as Route_No ,max(XXXXX.MP_VLC_Uploader_Code) as MP_VLC_Uploader_Code,max(XXXXX.Uom_Code) as Uom_Code,sum(XXXXX.qty) as qty ," & _
                      " Sum (XXXXX.FAT_KG) as FAT_KG , Sum(SNF_KG) as SNF_KG , sum (Qty_In_KG) as Qty_In_KG,convert(decimal(18,2), sum (XXXXX.Net_Amount) / nullif ( sum(XXXXX.qty),0)) as Rate,sum (XXXXX.Net_Amount) as Net_Amount,sum (XXXXX.DedAddAmount) as DedAddAmount,max(XXXXX.Bank_Code) as Bank_Code,max(XXXXX.Bank_Name) as Bank_Name ,max(XXXXX.AccountNO) as AccountNO,max(XXXXX.IFCICode) as IFCICode ,max(XXXXX.BankBranch) as BankBranch,max(XXXXX.MP_VLC_Uploader_Code_Last3Degit) as MP_VLC_Uploader_Code_Last3Degit, max(XXXXX.Incentive_Amount) as Incentive_Amount ,max( XXXXX.Deduction_Amount) as Deduction_Amount from ( " & _
                      " Select  SSSSFinal.Comp_Code, " & _
                      " SSSSFinal.Logo_Img,SSSSFinal.CompName,SSSSFinal.FromDate,SSSSFinal.ToDate,SSSSFinal.Doc_Date,SSSSFinal.Milk_Type,SSSSFinal.MCC_Code,SSSSFinal.MCC_NAME,SSSSFinal.MCC_ADD1,SSSSFinal.MCC_Add2,SSSSFinal.MCC_City_Code,SSSSFinal.MCC_City_Name,SSSSFinal.MCC_State_Code,SSSSFinal.MCC_STATE_NAME,SSSSFinal.MCC_Pin_Code,SSSSFinal.shift,SSSSFinal.VLC_Code_VLC_Uploader,SSSSFinal.VSP_Code,SSSSFinal.VSP_Name,SSSSFinal.VLC_Code,SSSSFinal.VLC_Name,SSSSFinal.Route_No,SSSSFinal.MP_VLC_Uploader_Code,SSSSFinal.Uom_Code,SSSSFinal.qty,SSSSFinal.FAT_PER,SSSSFinal.SNF_PER,SSSSFinal.Rate,SSSSFinal.Net_Amount,SSSSFinal.DedAddAmount,SSSSFinal.Bank_Code,SSSSFinal.Bank_Name,SSSSFinal.AccountNO,SSSSFinal.IFCICode,SSSSFinal.BankBranch,SSSSFinal.MP_VLC_Uploader_Code_Last3Degit, SSSSFinal.Incentive_Amount , SSSSFinal.Deduction_Amount ,(SSSSFinal.FAT_PER *  (zzz.CF*SSSSFinal.Qty) ) /100  as FAT_KG,   (SSSSFinal.SNF_PER * (zzz.CF*SSSSFinal.Qty) ) /100  as  SNF_KG,zzz.CF*SSSSFinal.Qty as Qty_In_KG " & _
                      " from ( " & _
                      " " + qry + "  " & _
                      " ) SSSSFinal left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/  nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =SSSSFinal.UOM_CODE and lower(zzz.TOUOM)='KG' " & _
                      " )   XXXXX " & _
                      " Group by XXXXX.VSP_Code,XXXXX.Doc_Date,XXXXX.shift " & _
                      " ) FinalXXX left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = FinalXXX.Comp_Code " & _
                      "  left outer join (select   Farmer_Code ,left ( Farmer_Invoice_No,(len(Farmer_Invoice_No)- ( len(Farmer_Code)+1 ))) as Farmer_Invoice_No ,MP_Adjust_Amount,Deduction_Amount as OtherAmount,MCC_Sale_Amount-MCC_Sale_Return_Amount as FeedAmount,Total_Advance_Amount as LoanAmount,Farmer_Name,case when isnull(Milk_Qty,0)>0 then Incentive_Amount/Milk_Qty  end IncentivePerLitre  from TSPL_MP_PAY_PROCESS_DETAIL  where  TSPL_MP_PAY_PROCESS_DETAIL.Doc_No = '" + fndDocNo.Value + "') as  TBL_Farmer_Invoice on FinalXXX.VSP_CODE = TBL_Farmer_Invoice.Farmer_Code left outer join TSPL_MCC_ROUTE_MASTER  on TSPL_MCC_ROUTE_MASTER .route_code=finalxxx.Route_No LEFT OUTER jOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_MCC_ROUTE_MASTER .Supervisor_Name  " & _
                      " order by convert (date,FinalXXX.Doc_Date,103) asc ,FinalXXX.shift desc "
            Else
                qry = qry + " order by convert (date,SSSS.Doc_Date,103) asc ,SSSS.shift desc "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            qry = "  select Final.VSP_Code ,Final.Item_Code,max(Final.Item_Desc) as Item_Desc ,sum (Final.Item_Net_Amt) as Amount from ( " & _
                  "  select TSPL_MP_PAY_PROCESS_MCC_SALE.FARMER_CODE as VSP_Code,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_SALE_FARMER_DETAIL.Item_Net_Amt * -1 as Item_Net_Amt from TSPL_MP_PAY_PROCESS_MCC_SALE inner join TSPL_MCC_SALE_FARMER_DETAIL on TSPL_MP_PAY_PROCESS_MCC_SALE.Shipment_Doc_No = TSPL_MCC_SALE_FARMER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_FARMER_DETAIL.Item_Code where TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocNo.Value + "' " & _
                  "  Union All " & _
                  "  select TSPL_MCC_SALE_RETURN_HEAD_FARMER.FARMER_CODE as  VSP_Code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount  as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN   inner join TSPL_MCC_SALE_RETURN_DETAIL_FARMER on TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Item_Code   left outer join TSPL_MCC_SALE_RETURN_HEAD_FARMER on TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Document_Code   where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" + fndDocNo.Value + "' " & _
                  " ) Final group by Final.VSP_Code ,Final.Item_Code "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If isPreFormatePrint = True Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dt2, "crptPaymentProcessFarmerPredefineFormate", "SubMilkPurchaseBill.rpt", "SubMilkPurchaseBill.rpt", "Address.rpt")
                    frmCRV = Nothing
                Else
                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dt2, "crptPaymentProcessFarmer", "SubMilkPurchaseBill.rpt", "SubMilkPurchaseBill.rpt", "Address.rpt")
                    frmCRV = Nothing
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExportToExcelPaymentToFarmer_Click(sender As Object, e As EventArgs) Handles btnExportToExcelPaymentToFarmer.Click
        If gvPaymentToFarmer IsNot Nothing AndAlso gvPaymentToFarmer.Rows.Count > 0 Then
            gv.Columns(colSelect).IsVisible = False
            Dim arr As List(Of String) = New List(Of String)
            arr.Add("Document No  : " & fndDocNo.Value)
            arr.Add("Payment Process Date  : " & dtpDate.Value)
            arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
            arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
            arr.Add("VSP :" & clsCommon.GetMulcallStringWithComma(txtVSP.arrValueMember))
            arr.Add("NEFT Uploder REF. No : " & txtNEFTUploaderREFNo.Text)

            clsCommon.MyExportToExcelGrid("Payment To Farmer Details", gvPaymentToFarmer, arr, "Payment Process")
            gv.Columns(colSelect).IsVisible = True
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found to export", Me.Text)
        End If
    End Sub

    Private Sub btnPreFormat2Print_Click(sender As Object, e As EventArgs) Handles btnPreFormat2Print.Click
        Try
            Dim isPreFormatePrint As Boolean = True
            Dim qry As String = "Select SSSS.* , TBL_IncentiveDeduction.Incentive_Amount , TBL_IncentiveDeduction.Deduction_Amount from (" &
                               " select Final.*, isnull (TBL_FOR_DEDUCTION.Amount,0) as DedAddAmount,TSPL_MP_MASTER.BankName as Bank_Code, tspl_vendor_bank_master.Bank_Name,TSPL_MP_MASTER.AccountNo,TSPL_MP_MASTER.PayeeName,TSPL_MP_MASTER.IFCICode,TSPL_MP_MASTER.BankBranch, RIGHT( MP_VLC_Uploader_Code,3) as MP_VLC_Uploader_Code_Last3Degit from ( select TSPL_COMPANY_MASTER.Comp_Code," + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img, TSPL_COMPANY_MASTER.Comp_Name as CompName,'" + clsCommon.myCDate(dtpFromDate.Value) + "' as FromDate ,'" + clsCommon.myCDate(dtpToDate.Value) + "' as ToDate ,Convert (varchar,TSPL_VLC_DATA_UPLOADER.Doc_Date,103) as Doc_Date ,TSPL_VLC_DATA_UPLOADER.Milk_Type, TSPL_VLC_DATA_UPLOADER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Add1 as MCC_Add1 , TSPL_MCC_MASTER.Add2 as MCC_Add2 , TSPL_MCC_MASTER.City_code as MCC_City_Code, TSPL_CITY_MASTER_MCC.City_Name as MCC_City_Name,TSPL_MCC_MASTER.State_Code as MCC_State_Code,TSPL_STATE_MASTER_MCC.STATE_NAME as MCC_STATE_NAME,TSPL_MCC_MASTER.Pin_code as MCC_Pin_Code , TSPL_VLC_DATA_UPLOADER.shift , TSPL_VLC_DATA_UPLOADER.VLC_CODE as VLC_Code_VLC_Uploader , " &
                               " TSPL_MP_MASTER.MP_Code as VSP_Code, TSPL_MP_MASTER.MP_Name as VSP_Name , TSPL_MP_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name " &
                               " , TSPL_VLC_DATA_UPLOADER.Route_No , TSPL_VLC_DATA_UPLOADER.MP_CODE as MP_VLC_Uploader_Code ,TSPL_VLC_DATA_UPLOADER.Uom_Code, TSPL_VLC_DATA_UPLOADER.qty , TSPL_VLC_DATA_UPLOADER.fat as FAT_PER, TSPL_VLC_DATA_UPLOADER.snf as SNF_PER,TSPL_VLC_DATA_UPLOADER.Rate , TSPL_VLC_DATA_UPLOADER.Amount as Net_Amount from TSPL_VLC_DATA_UPLOADER " &
                               " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_DATA_UPLOADER.MCC_Code " &
                               " left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_MCC on TSPL_CITY_MASTER_MCC.City_Code =  TSPL_MCC_MASTER.City_code " &
                               " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_MCC on TSPL_STATE_MASTER_MCC.State_Code = TSPL_MCC_MASTER.State_Code " &
                               " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_UPLOADER = TSPL_VLC_DATA_UPLOADER.VLC_CODE " &
                               " left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_VLC_DATA_UPLOADER.Comp_Code " &
                               " left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code_VLC_Uploader = TSPL_VLC_DATA_UPLOADER.MP_CODE " &
                               " where convert (date, TSPL_VLC_DATA_UPLOADER.Doc_Date,103) > = (select TSPL_PAYMENT_PROCESS_HEAD.From_Date  from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "')  " &
                               " and convert (date, TSPL_VLC_DATA_UPLOADER.Doc_Date,103) < = (select  TSPL_PAYMENT_PROCESS_HEAD.To_Date from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "') " &
                               " and TSPL_VLC_DATA_UPLOADER.MP_CODE in (select distinct TSPL_MP_MASTER.MP_Code_VLC_Uploader from TSPL_MP_PAY_PROCESS_DETAIL inner join TSPL_MP_MASTER on TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code = TSPL_MP_MASTER.MP_Code  where Doc_No = '" + fndDocNo.Value + "') " &
                               " ) Final  " &
                               " left outer join  (select Final.VSP_Code  ,sum (Final.Item_Net_Amt) as Amount from ( " &
                               " select TSPL_MP_PAY_PROCESS_MCC_SALE.FARMER_CODE as VSP_Code,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_SALE_FARMER_DETAIL.Item_Net_Amt * -1 as Item_Net_Amt from TSPL_MP_PAY_PROCESS_MCC_SALE inner join TSPL_MCC_SALE_FARMER_DETAIL on TSPL_MP_PAY_PROCESS_MCC_SALE.Shipment_Doc_No = TSPL_MCC_SALE_FARMER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_FARMER_DETAIL.Item_Code where TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocNo.Value + "' " &
                               " Union All " &
                               " select TSPL_MCC_SALE_RETURN_HEAD_FARMER.FARMER_CODE as  VSP_Code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount  as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN   inner join TSPL_MCC_SALE_RETURN_DETAIL_FARMER on TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Item_Code   left outer join TSPL_MCC_SALE_RETURN_HEAD_FARMER on TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Document_Code   where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" + fndDocNo.Value + "' " &
                               " ) Final group by Final.VSP_Code ) As TBL_FOR_DEDUCTION on TBL_FOR_DEDUCTION.VSP_Code = Final.VSP_Code " &
                               " Left Outer Join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = Final.VSP_Code " &
                               " left Outer Join tspl_vendor_bank_master on tspl_vendor_bank_master.Bank_Code = TSPL_MP_MASTER.BankName " &
                               "  " &
                               " Union All" &
                               " select Final.*, isnull (TBL_FOR_DEDUCTION.Amount,0) as DedAddAmount,TSPL_MP_MASTER.BankName as Bank_Code, tspl_vendor_bank_master.Bank_Name,TSPL_MP_MASTER.AccountNo,TSPL_MP_MASTER.PayeeName,TSPL_MP_MASTER.IFCICode,TSPL_MP_MASTER.BankBranch, RIGHT( MP_VLC_Uploader_Code,3) as MP_VLC_Uploader_Code_Last3Degit from ( select TSPL_COMPANY_MASTER.Comp_Code, " + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img, TSPL_COMPANY_MASTER.Comp_Name as CompName,'" + clsCommon.myCDate(dtpFromDate.Value) + "' as FromDate ,'" + clsCommon.myCDate(dtpToDate.Value) + "' as ToDate ,Convert (varchar,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) as Doc_Date ,'C' as Milk_Type,TSPL_VLC_MASTER_HEAD_ForMCC.MCC " &
                               " as MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Add1 as MCC_Add1 , TSPL_MCC_MASTER.Add2 as MCC_Add2 , TSPL_MCC_MASTER.City_code as MCC_City_Code, TSPL_CITY_MASTER_MCC.City_Name as MCC_City_Name,TSPL_MCC_MASTER.State_Code as MCC_State_Code,TSPL_STATE_MASTER_MCC.STATE_NAME as MCC_STATE_NAME,TSPL_MCC_MASTER.Pin_code as MCC_Pin_Code , case when  TSPL_VLC_DATA_UPLOADER_MASTER.Shift = 'MORNING' then 'M' else 'E' end as Shift , TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader  as VLC_Code_VLC_Uploader ,  TSPL_MP_MASTER.MP_Code as VSP_Code, TSPL_MP_MASTER.MP_Name as VSP_Name , TSPL_MP_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name  , TSPL_VLC_DATA_UPLOADER_MASTER.Route_Code as Route_No , TSPL_MP_MASTER.MP_Code_VLC_Uploader as MP_VLC_Uploader_Code ,TSPL_VLC_DATA_UPLOADER_DETAIL.Unit_Code as Uom_Code, TSPL_VLC_DATA_UPLOADER_DETAIL.Qty , TSPL_VLC_DATA_UPLOADER_DETAIL.fatPer as FAT_PER, TSPL_VLC_DATA_UPLOADER_DETAIL.SNFPer as SNF_PER,TSPL_VLC_DATA_UPLOADER_DETAIL.Rate , TSPL_VLC_DATA_UPLOADER_DETAIL.Amount as Net_Amount from  TSPL_VLC_DATA_UPLOADER_DETAIL left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code = TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code " &
                               " Left Outer Join TSPL_VLC_MASTER_HEAD as TSPL_VLC_MASTER_HEAD_ForMCC on TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code=TSPL_VLC_MASTER_HEAD_ForMCC.VLC_Code  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code" &
                               " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD_ForMCC.MCC  left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_MCC on TSPL_CITY_MASTER_MCC.City_Code =  TSPL_MCC_MASTER.City_code  left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_MCC on TSPL_STATE_MASTER_MCC.State_Code = TSPL_MCC_MASTER.State_Code    left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_VLC_DATA_UPLOADER_MASTER.Comp_Code  left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code  where convert (date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) > = (select TSPL_PAYMENT_PROCESS_HEAD.From_Date  from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "')   and convert (date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) < = (select  TSPL_PAYMENT_PROCESS_HEAD.To_Date from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocNo.Value + "')  and TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code in (select distinct TSPL_MP_MASTER.MP_Code from TSPL_MP_PAY_PROCESS_DETAIL inner join TSPL_MP_MASTER on TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code = TSPL_MP_MASTER.MP_Code  where Doc_No = '" + fndDocNo.Value + "')  ) Final   left outer join  (select Final.VSP_Code  ,sum (Final.Item_Net_Amt) as Amount from (  select TSPL_MP_PAY_PROCESS_MCC_SALE.FARMER_CODE as VSP_Code,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_SALE_FARMER_DETAIL.Item_Net_Amt * -1 as Item_Net_Amt from TSPL_MP_PAY_PROCESS_MCC_SALE inner join TSPL_MCC_SALE_FARMER_DETAIL on TSPL_MP_PAY_PROCESS_MCC_SALE.Shipment_Doc_No = TSPL_MCC_SALE_FARMER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_FARMER_DETAIL.Item_Code where TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocNo.Value + "'  Union All  select TSPL_MCC_SALE_RETURN_HEAD_FARMER.FARMER_CODE as  VSP_Code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount  as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN   inner join TSPL_MCC_SALE_RETURN_DETAIL_FARMER on TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Item_Code   left outer join TSPL_MCC_SALE_RETURN_HEAD_FARMER on TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Document_Code   where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" + fndDocNo.Value + "'  ) Final group by Final.VSP_Code ) As TBL_FOR_DEDUCTION on TBL_FOR_DEDUCTION.VSP_Code = Final.VSP_Code  Left Outer Join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = Final.VSP_Code  left Outer Join tspl_vendor_bank_master on tspl_vendor_bank_master.Bank_Code = TSPL_MP_MASTER.BankName " &
                               " ) SSSS  left outer join  ( select VLC_CODE_Uploader,Farmer_Code, Sum ( isNull (Incentive_Amount,0)) as Incentive_Amount , Sum ( isnull(Deduction_Amount,0)) as Deduction_Amount from TSPL_MP_PAY_PROCESS_DETAIL where Doc_No = '" + fndDocNo.Value + "' group by VLC_CODE_Uploader,Farmer_Code ) TBL_IncentiveDeduction on TBL_IncentiveDeduction.VLC_CODE_Uploader = SSSS.VLC_Code_VLC_Uploader and TBL_IncentiveDeduction.Farmer_Code = SSSS.VSP_Code    "
            If isPreFormatePrint = True Then
                '                qry = " select XFinal.*, isnull (TBL_EVENING.qty,0) as EQty ,isnull(TBL_EVENING.FAT_PER,0) as EFAT_PER, isnull(TBL_EVENING.SNF_PER,0) as ESNF_PER,isnull (TBL_EVENING.rate,0) as ERate, isnull (TBL_EVENING.Net_Amount,0) as ENetAmount , isnull(TBL_EVENING.DedAddAmount,0) as EDedAddAmoun,isnull(TBL_EVENING.Incentive_amount,0) as EIncentive_amount ,isnull(TBL_EVENING.deduction_amount,0) as Ededuction_amount, isnull (TBL_EVENING.feedAmount,0 ) as EfeedAmount, isnull ( TBL_EVENING.LoanAmount,0) as ELoanAmount, isnull (TBL_EVENING.OtherAmount,0) as EOtherAmount, isnull (TBL_EVENING.IncentivePErLitre,0) as EIncentivePErLitre  from( " &
                '                      " select TBL_Farmer_Invoice.Farmer_Invoice_No,VLC_Code_VLC_Uploader,TBL_Farmer_Invoice.MP_Adjust_Amount as OutStanding_Amt ,RIGHT ( FinalXXX.VLC_Code_VLC_Uploader,3) as VLC_Code_VLC_Uploader_3Digit, FinalXXX.Comp_Code," + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img ,FinalXXX.CompName as CompName,FinalXXX.FromDate as FromDate ,FinalXXX.ToDate  as ToDate,FinalXXX.Doc_Date,FinalXXX.Milk_Type as Milk_Type,FinalXXX.MCC_Code as MCC_Code,FinalXXX.MCC_NAME as MCC_NAME,FinalXXX.MCC_ADD1 as MCC_ADD1,FinalXXX.MCC_Add2 as MCC_Add2,FinalXXX.MCC_City_Code as MCC_City_Code,FinalXXX.MCC_City_Name as MCC_City_Name,FinalXXX.MCC_State_Code as MCC_State_Code,FinalXXX.MCC_STATE_NAME as MCC_STATE_NAME,FinalXXX.MCC_Pin_Code as MCC_Pin_Code,FinalXXX.shift,FinalXXX.VSP_Code as VSP_Code,FinalXXX.VSP_Name as VSP_Name,FinalXXX.VLC_Code as VLC_Code,FinalXXX.VLC_Name as VLC_Name,FinalXXX.Route_No as Route_No ,FinalXXX.MP_VLC_Uploader_Code as MP_VLC_Uploader_Code,FinalXXX.Uom_Code as Uom_Code,FinalXXX.qty as qty , " &
                '                      " convert ( decimal(18,2), ((FinalXXX.Fat_KG * 100) / nullif (FinalXXX.Qty_In_KG,0) ))  as FAT_PER  ,convert (decimal(18,2), ((FinalXXX.SNF_KG * 100 )/ nullif (FinalXXX.Qty_In_KG,0)))   as SNF_PER,Rate as Rate,FinalXXX.Net_Amount as Net_Amount,FinalXXX.DedAddAmount as DedAddAmount,FinalXXX.Bank_Code as Bank_Code,FinalXXX.Bank_Name as Bank_Name ,FinalXXX.AccountNO as AccountNO,FinalXXX.PayeeName as PayeeName,FinalXXX.IFCICode as IFCICode ,FinalXXX.BankBranch as BankBranch,FinalXXX.MP_VLC_Uploader_Code_Last3Degit as MP_VLC_Uploader_Code_Last3Degit, FinalXXX.Incentive_Amount as Incentive_Amount ,FinalXXX.Deduction_Amount as Deduction_Amount  " &
                '" ,TBL_Farmer_Invoice.FeedAmount ,TBL_Farmer_Invoice .LoanAmount ,TBL_Farmer_Invoice .OtherAmount,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_EMPLOYEE_MASTER.Emp_Name AS SuperVisorName ,TBL_Farmer_Invoice.Farmer_Name ,isnull (TBL_Farmer_Invoice.IncentivePerLitre,0) as  IncentivePerLitre " &
                ' " from ( " &
                '                      " select " &
                '                      " max(XXXXX.Comp_Code) as Comp_Code, " &
                '                      " max(XXXXX.CompName) as CompName,max(XXXXX.FromDate) as FromDate ,max(XXXXX.ToDate)  as ToDate,XXXXX.Doc_Date,max(XXXXX.Milk_Type) as Milk_Type,max(XXXXX.MCC_Code) as MCC_Code,max(XXXXX.MCC_NAME) as MCC_NAME,max(XXXXX.MCC_ADD1) as MCC_ADD1,max(XXXXX.MCC_Add2) as MCC_Add2,max(XXXXX.MCC_City_Code) as MCC_City_Code,max(XXXXX.MCC_City_Name) as MCC_City_Name,max(XXXXX.MCC_State_Code) as MCC_State_Code,max(XXXXX.MCC_STATE_NAME) as MCC_STATE_NAME,max(XXXXX.MCC_Pin_Code) as MCC_Pin_Code,XXXXX.shift,max(XXXXX.VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,XXXXX.VSP_Code as VSP_Code,max(XXXXX.VSP_Name) as VSP_Name,max(XXXXX.VLC_Code) as VLC_Code,max(XXXXX.VLC_Name) as VLC_Name,max(XXXXX.Route_No) as Route_No ,max(XXXXX.MP_VLC_Uploader_Code) as MP_VLC_Uploader_Code,max(XXXXX.Uom_Code) as Uom_Code,sum(XXXXX.qty) as qty ," &
                '                      " Sum (XXXXX.FAT_KG) as FAT_KG , Sum(SNF_KG) as SNF_KG , sum (Qty_In_KG) as Qty_In_KG,convert(decimal(18,2), sum (XXXXX.Net_Amount) / nullif ( sum(XXXXX.qty),0)) as Rate,sum (XXXXX.Net_Amount) as Net_Amount,sum (XXXXX.DedAddAmount) as DedAddAmount,max(XXXXX.Bank_Code) as Bank_Code,max(XXXXX.Bank_Name) as Bank_Name ,max(XXXXX.AccountNO) as AccountNO,max(XXXXX.PayeeName) as PayeeName ,max(XXXXX.IFCICode) as IFCICode ,max(XXXXX.BankBranch) as BankBranch,max(XXXXX.MP_VLC_Uploader_Code_Last3Degit) as MP_VLC_Uploader_Code_Last3Degit, max(XXXXX.Incentive_Amount) as Incentive_Amount ,max( XXXXX.Deduction_Amount) as Deduction_Amount from ( " &
                '                      " Select  SSSSFinal.Comp_Code, " &
                '                      " SSSSFinal.Logo_Img,SSSSFinal.CompName,SSSSFinal.FromDate,SSSSFinal.ToDate,SSSSFinal.Doc_Date,SSSSFinal.Milk_Type,SSSSFinal.MCC_Code,SSSSFinal.MCC_NAME,SSSSFinal.MCC_ADD1,SSSSFinal.MCC_Add2,SSSSFinal.MCC_City_Code,SSSSFinal.MCC_City_Name,SSSSFinal.MCC_State_Code,SSSSFinal.MCC_STATE_NAME,SSSSFinal.MCC_Pin_Code,SSSSFinal.shift,SSSSFinal.VLC_Code_VLC_Uploader,SSSSFinal.VSP_Code,SSSSFinal.VSP_Name,SSSSFinal.VLC_Code,SSSSFinal.VLC_Name,SSSSFinal.Route_No,SSSSFinal.MP_VLC_Uploader_Code,SSSSFinal.Uom_Code,SSSSFinal.qty,SSSSFinal.FAT_PER,SSSSFinal.SNF_PER,SSSSFinal.Rate,SSSSFinal.Net_Amount,SSSSFinal.DedAddAmount,SSSSFinal.Bank_Code,SSSSFinal.Bank_Name,SSSSFinal.AccountNO,SSSSFinal.PayeeName,SSSSFinal.IFCICode,SSSSFinal.BankBranch,SSSSFinal.MP_VLC_Uploader_Code_Last3Degit, SSSSFinal.Incentive_Amount , SSSSFinal.Deduction_Amount ,(SSSSFinal.FAT_PER *  (zzz.CF*SSSSFinal.Qty) ) /100  as FAT_KG,   (SSSSFinal.SNF_PER * (zzz.CF*SSSSFinal.Qty) ) /100  as  SNF_KG,zzz.CF*SSSSFinal.Qty as Qty_In_KG " &
                '                      " from ( " &
                '                      " " + qry + "  " &
                '                      " ) SSSSFinal left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/  nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =SSSSFinal.UOM_CODE and lower(zzz.TOUOM)='KG' " &
                '                      " )   XXXXX " &
                '                      " Group by XXXXX.VSP_Code,XXXXX.Doc_Date,XXXXX.shift " &
                '                      " ) FinalXXX left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = FinalXXX.Comp_Code " &
                '                      "  left outer join (select   Farmer_Code ,left ( Farmer_Invoice_No,(len(Farmer_Invoice_No)- ( len(Farmer_Code)+1 ))) as Farmer_Invoice_No ,MP_Adjust_Amount,Deduction_Amount as OtherAmount,MCC_Sale_Amount-MCC_Sale_Return_Amount as FeedAmount,Total_Advance_Amount as LoanAmount,Farmer_Name,case when isnull(Milk_Qty,0)>0 then Incentive_Amount/Milk_Qty  end IncentivePerLitre  from TSPL_MP_PAY_PROCESS_DETAIL  where  TSPL_MP_PAY_PROCESS_DETAIL.Doc_No = '" + fndDocNo.Value + "') as  TBL_Farmer_Invoice on FinalXXX.VSP_CODE = TBL_Farmer_Invoice.Farmer_Code left outer join TSPL_MCC_ROUTE_MASTER  on TSPL_MCC_ROUTE_MASTER .route_code=finalxxx.Route_No LEFT OUTER jOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_MCC_ROUTE_MASTER .Supervisor_Name  " &
                '                      " where 2=2  and Shift = 'M' " &
                '                      " ) XFinal " &
                '                      " left outer join " &
                '                      "  ( " &
                '                       " select TBL_Farmer_Invoice.Farmer_Invoice_No,VLC_Code_VLC_Uploader,TBL_Farmer_Invoice.MP_Adjust_Amount as OutStanding_Amt ,RIGHT ( FinalXXX.VLC_Code_VLC_Uploader,3) as VLC_Code_VLC_Uploader_3Digit, FinalXXX.Comp_Code," + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img ,FinalXXX.CompName as CompName,FinalXXX.FromDate as FromDate ,FinalXXX.ToDate  as ToDate,FinalXXX.Doc_Date,FinalXXX.Milk_Type as Milk_Type,FinalXXX.MCC_Code as MCC_Code,FinalXXX.MCC_NAME as MCC_NAME,FinalXXX.MCC_ADD1 as MCC_ADD1,FinalXXX.MCC_Add2 as MCC_Add2,FinalXXX.MCC_City_Code as MCC_City_Code,FinalXXX.MCC_City_Name as MCC_City_Name,FinalXXX.MCC_State_Code as MCC_State_Code,FinalXXX.MCC_STATE_NAME as MCC_STATE_NAME,FinalXXX.MCC_Pin_Code as MCC_Pin_Code,FinalXXX.shift,FinalXXX.VSP_Code as VSP_Code,FinalXXX.VSP_Name as VSP_Name,FinalXXX.VLC_Code as VLC_Code,FinalXXX.VLC_Name as VLC_Name,FinalXXX.Route_No as Route_No ,FinalXXX.MP_VLC_Uploader_Code as MP_VLC_Uploader_Code,FinalXXX.Uom_Code as Uom_Code,FinalXXX.qty as qty , " &
                '                      " convert ( decimal(18,2), ((FinalXXX.Fat_KG * 100) / nullif (FinalXXX.Qty_In_KG,0) ))  as FAT_PER  ,convert (decimal(18,2), ((FinalXXX.SNF_KG * 100 )/ nullif (FinalXXX.Qty_In_KG,0)))   as SNF_PER,Rate as Rate,FinalXXX.Net_Amount as Net_Amount,FinalXXX.DedAddAmount as DedAddAmount,FinalXXX.Bank_Code as Bank_Code,FinalXXX.Bank_Name as Bank_Name ,FinalXXX.AccountNO as AccountNO,FinalXXX.PayeeName as PayeeName,FinalXXX.IFCICode as IFCICode ,FinalXXX.BankBranch as BankBranch,FinalXXX.MP_VLC_Uploader_Code_Last3Degit as MP_VLC_Uploader_Code_Last3Degit, FinalXXX.Incentive_Amount as Incentive_Amount ,FinalXXX.Deduction_Amount as Deduction_Amount  " &
                '" ,TBL_Farmer_Invoice.FeedAmount ,TBL_Farmer_Invoice .LoanAmount ,TBL_Farmer_Invoice .OtherAmount,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_EMPLOYEE_MASTER.Emp_Name AS SuperVisorName ,TBL_Farmer_Invoice.Farmer_Name ,isnull (TBL_Farmer_Invoice.IncentivePerLitre,0) as  IncentivePerLitre " &
                ' " from ( " &
                '                      " select " &
                '                      " max(XXXXX.Comp_Code) as Comp_Code, " &
                '                      " max(XXXXX.CompName) as CompName,max(XXXXX.FromDate) as FromDate ,max(XXXXX.ToDate)  as ToDate,XXXXX.Doc_Date,max(XXXXX.Milk_Type) as Milk_Type,max(XXXXX.MCC_Code) as MCC_Code,max(XXXXX.MCC_NAME) as MCC_NAME,max(XXXXX.MCC_ADD1) as MCC_ADD1,max(XXXXX.MCC_Add2) as MCC_Add2,max(XXXXX.MCC_City_Code) as MCC_City_Code,max(XXXXX.MCC_City_Name) as MCC_City_Name,max(XXXXX.MCC_State_Code) as MCC_State_Code,max(XXXXX.MCC_STATE_NAME) as MCC_STATE_NAME,max(XXXXX.MCC_Pin_Code) as MCC_Pin_Code,XXXXX.shift,max(XXXXX.VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,XXXXX.VSP_Code as VSP_Code,max(XXXXX.VSP_Name) as VSP_Name,max(XXXXX.VLC_Code) as VLC_Code,max(XXXXX.VLC_Name) as VLC_Name,max(XXXXX.Route_No) as Route_No ,max(XXXXX.MP_VLC_Uploader_Code) as MP_VLC_Uploader_Code,max(XXXXX.Uom_Code) as Uom_Code,sum(XXXXX.qty) as qty ," &
                '                      " Sum (XXXXX.FAT_KG) as FAT_KG , Sum(SNF_KG) as SNF_KG , sum (Qty_In_KG) as Qty_In_KG,convert(decimal(18,2), sum (XXXXX.Net_Amount) / nullif ( sum(XXXXX.qty),0)) as Rate,sum (XXXXX.Net_Amount) as Net_Amount,sum (XXXXX.DedAddAmount) as DedAddAmount,max(XXXXX.Bank_Code) as Bank_Code,max(XXXXX.Bank_Name) as Bank_Name ,max(XXXXX.AccountNO) as AccountNO,max(XXXXX.PayeeName) as PayeeName,max(XXXXX.IFCICode) as IFCICode ,max(XXXXX.BankBranch) as BankBranch,max(XXXXX.MP_VLC_Uploader_Code_Last3Degit) as MP_VLC_Uploader_Code_Last3Degit, max(XXXXX.Incentive_Amount) as Incentive_Amount ,max( XXXXX.Deduction_Amount) as Deduction_Amount from ( " &
                '                      " Select  SSSSFinal.Comp_Code, " &
                '                      " SSSSFinal.Logo_Img,SSSSFinal.CompName,SSSSFinal.FromDate,SSSSFinal.ToDate,SSSSFinal.Doc_Date,SSSSFinal.Milk_Type,SSSSFinal.MCC_Code,SSSSFinal.MCC_NAME,SSSSFinal.MCC_ADD1,SSSSFinal.MCC_Add2,SSSSFinal.MCC_City_Code,SSSSFinal.MCC_City_Name,SSSSFinal.MCC_State_Code,SSSSFinal.MCC_STATE_NAME,SSSSFinal.MCC_Pin_Code,SSSSFinal.shift,SSSSFinal.VLC_Code_VLC_Uploader,SSSSFinal.VSP_Code,SSSSFinal.VSP_Name,SSSSFinal.VLC_Code,SSSSFinal.VLC_Name,SSSSFinal.Route_No,SSSSFinal.MP_VLC_Uploader_Code,SSSSFinal.Uom_Code,SSSSFinal.qty,SSSSFinal.FAT_PER,SSSSFinal.SNF_PER,SSSSFinal.Rate,SSSSFinal.Net_Amount,SSSSFinal.DedAddAmount,SSSSFinal.Bank_Code,SSSSFinal.Bank_Name,SSSSFinal.AccountNO,SSSSFinal.PayeeName,SSSSFinal.IFCICode,SSSSFinal.BankBranch,SSSSFinal.MP_VLC_Uploader_Code_Last3Degit, SSSSFinal.Incentive_Amount , SSSSFinal.Deduction_Amount ,(SSSSFinal.FAT_PER *  (zzz.CF*SSSSFinal.Qty) ) /100  as FAT_KG,   (SSSSFinal.SNF_PER * (zzz.CF*SSSSFinal.Qty) ) /100  as  SNF_KG,zzz.CF*SSSSFinal.Qty as Qty_In_KG " &
                '                      " from ( " &
                '                      " " + qry + "  " &
                '                      " ) SSSSFinal left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/  nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =SSSSFinal.UOM_CODE and lower(zzz.TOUOM)='KG' " &
                '                      " )   XXXXX " &
                '                      " Group by XXXXX.VSP_Code,XXXXX.Doc_Date,XXXXX.shift " &
                '                      " ) FinalXXX left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = FinalXXX.Comp_Code " &
                '                      "  left outer join (select   Farmer_Code ,left ( Farmer_Invoice_No,(len(Farmer_Invoice_No)- ( len(Farmer_Code)+1 ))) as Farmer_Invoice_No ,MP_Adjust_Amount,Deduction_Amount as OtherAmount,MCC_Sale_Amount-MCC_Sale_Return_Amount as FeedAmount,Total_Advance_Amount as LoanAmount,Farmer_Name,case when isnull(Milk_Qty,0)>0 then Incentive_Amount/Milk_Qty  end IncentivePerLitre  from TSPL_MP_PAY_PROCESS_DETAIL  where  TSPL_MP_PAY_PROCESS_DETAIL.Doc_No = '" + fndDocNo.Value + "') as  TBL_Farmer_Invoice on FinalXXX.VSP_CODE = TBL_Farmer_Invoice.Farmer_Code left outer join TSPL_MCC_ROUTE_MASTER  on TSPL_MCC_ROUTE_MASTER .route_code=finalxxx.Route_No LEFT OUTER jOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_MCC_ROUTE_MASTER .Supervisor_Name  " &
                '                      " where 2=2  and Shift = 'E' " &
                '                      "  ) TBL_EVENING on TBL_EVENING .Farmer_invoice_no = XFinal.Farmer_invoice_no and TBL_EVENING.vlc_code_vlc_uploader =XFinal.VLC_Code_VLC_Uploader and TBL_EVENING.VLC_Code_VLC_Uploader =XFinal.VLC_Code_VLC_Uploader and TBL_EVENING.vsp_code = XFinal.vsp_code and TBL_EVENING.VLC_Code = XFinal.VLC_CODE  and TBL_EVENING.MP_VLc_Uploader_code = XFinal.MP_VLc_Uploader_code and TBL_EVENING.Doc_Date = XFinal.Doc_Date " &
                '                      " order by convert (date,XFinal.Doc_Date,103) asc "

                qry = " select Farmer_Invoice_No,	VLC_Code_VLC_Uploader,	sum(OutStanding_Amt) as OutStanding_Amt,	max(VLC_Code_VLC_Uploader_3Digit) as VLC_Code_VLC_Uploader_3Digit,	Comp_Code " &
                " ,	max(isnull(Logo_Img,'')) as Logo_Img,max(CompName) as CompName,	FromDate,	ToDate,	Doc_Date,	Milk_Type,	MCC_Code " &
                " ,	max(MCC_NAME) as MCC_NAME,	max(MCC_ADD1) as MCC_ADD1,	max(MCC_Add2) as MCC_Add2,	max(MCC_City_Code) as MCC_City_Code,	max(MCC_City_Name) as MCC_City_Name " &
                " ,max(MCC_State_Code) as MCC_State_Code,	max(MCC_STATE_NAME) as MCC_STATE_NAME,	max(MCC_Pin_Code) as MCC_Pin_Code " &
                " ,	VSP_Code,	max(VSP_Name) as VSP_Name,	VLC_Code " &
                " ,	max(VLC_Name) as VLC_Name,	Route_No,	MP_VLC_Uploader_Code,	max(Uom_Code) Uom_Code,	sum(qty) as qty,	max(FAT_PER) as FAT_PER,	max(SNF_PER) as SNF_PER " &
                " ,	max(Rate) as Rate,	sum(Net_Amount) as Net_Amount,	sum(DedAddAmount) as DedAddAmount,	max(Bank_Code) as Bank_Code,	max(Bank_Name) as Bank_Name " &
                " ,	max(AccountNO) as AccountNO,	max(PayeeName) as PayeeName,	max(IFCICode) as IFCICode,	max(BankBranch) as BankBranch " &
                    " ,	max(MP_VLC_Uploader_Code_Last3Degit) as MP_VLC_Uploader_Code_Last3Degit,	 " &
                    " sum(Incentive_Amount) As Incentive_Amount,	sum(Deduction_Amount) As Deduction_Amount,	sum(FeedAmount) As FeedAmount,	sum(LoanAmount) As LoanAmount " &
                    " ,	sum(OtherAmount) as OtherAmount,	max(Route_Name) as Route_Name,	max(SuperVisorName) as SuperVisorName " &
                    " ,	max(Farmer_Name) as Farmer_Name,max(IncentivePerLitre) as IncentivePerLitre " &
                " ,sum(EQty) AS EQty,max(EFAT_PER) AS EFAT_PER,max(ESNF_PER) AS ESNF_PER,max(ERate) AS ERate,sum(ENetAmount) AS ENetAmount,sum(EDedAddAmoun) AS EDedAddAmoun " &
                " ,sum(EIncentive_amount) AS EIncentive_amount,sum(Ededuction_amount) AS Ededuction_amount,sum(EfeedAmount) AS EfeedAmount " &
                " ,sum(ELoanAmount) AS ELoanAmount,sum(EOtherAmount) AS EOtherAmount,max(EIncentivePErLitre) AS EIncentivePErLitre " &
                    "  from( " &
                   " select TBL_Farmer_Invoice.Farmer_Invoice_No,VLC_Code_VLC_Uploader,TBL_Farmer_Invoice.MP_Adjust_Amount as OutStanding_Amt ,RIGHT ( FinalXXX.VLC_Code_VLC_Uploader,3) as VLC_Code_VLC_Uploader_3Digit, FinalXXX.Comp_Code," + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img ,FinalXXX.CompName as CompName,FinalXXX.FromDate as FromDate ,FinalXXX.ToDate  as ToDate,FinalXXX.Doc_Date,FinalXXX.Milk_Type as Milk_Type,FinalXXX.MCC_Code as MCC_Code,FinalXXX.MCC_NAME as MCC_NAME,FinalXXX.MCC_ADD1 as MCC_ADD1,FinalXXX.MCC_Add2 as MCC_Add2,FinalXXX.MCC_City_Code as MCC_City_Code,FinalXXX.MCC_City_Name as MCC_City_Name,FinalXXX.MCC_State_Code as MCC_State_Code,FinalXXX.MCC_STATE_NAME as MCC_STATE_NAME,FinalXXX.MCC_Pin_Code as MCC_Pin_Code,FinalXXX.shift,FinalXXX.VSP_Code as VSP_Code,FinalXXX.VSP_Name as VSP_Name,FinalXXX.VLC_Code as VLC_Code,FinalXXX.VLC_Name as VLC_Name,FinalXXX.Route_No as Route_No ,FinalXXX.MP_VLC_Uploader_Code as MP_VLC_Uploader_Code,FinalXXX.Uom_Code as Uom_Code,FinalXXX.qty as qty , " &
                   " convert ( decimal(18,2), ((FinalXXX.Fat_KG * 100) / nullif (FinalXXX.Qty_In_KG,0) ))  as FAT_PER  ,convert (decimal(18,2), ((FinalXXX.SNF_KG * 100 )/ nullif (FinalXXX.Qty_In_KG,0)))   as SNF_PER,Rate as Rate,FinalXXX.Net_Amount as Net_Amount,FinalXXX.DedAddAmount as DedAddAmount,FinalXXX.Bank_Code as Bank_Code,FinalXXX.Bank_Name as Bank_Name ,FinalXXX.AccountNO as AccountNO,FinalXXX.PayeeName as PayeeName,FinalXXX.IFCICode as IFCICode ,FinalXXX.BankBranch as BankBranch,FinalXXX.MP_VLC_Uploader_Code_Last3Degit as MP_VLC_Uploader_Code_Last3Degit, FinalXXX.Incentive_Amount as Incentive_Amount ,FinalXXX.Deduction_Amount as Deduction_Amount  " &
                " ,TBL_Farmer_Invoice.FeedAmount ,TBL_Farmer_Invoice .LoanAmount ,TBL_Farmer_Invoice .OtherAmount,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_EMPLOYEE_MASTER.Emp_Name AS SuperVisorName ,TBL_Farmer_Invoice.Farmer_Name ,isnull (TBL_Farmer_Invoice.IncentivePerLitre,0) as  IncentivePerLitre " &
                " ,0 AS EQty,0 AS EFAT_PER,0 AS ESNF_PER,0 AS ERate,0 AS ENetAmount,0 AS EDedAddAmoun,0 AS EIncentive_amount,0 AS Ededuction_amount,0 AS EfeedAmount,0 AS ELoanAmount,0 AS EOtherAmount,0 AS EIncentivePErLitre  from ( " &
                   " select " &
                   " max(XXXXX.Comp_Code) as Comp_Code, " &
                   " max(XXXXX.CompName) as CompName,max(XXXXX.FromDate) as FromDate ,max(XXXXX.ToDate)  as ToDate,XXXXX.Doc_Date,max(XXXXX.Milk_Type) as Milk_Type,max(XXXXX.MCC_Code) as MCC_Code,max(XXXXX.MCC_NAME) as MCC_NAME,max(XXXXX.MCC_ADD1) as MCC_ADD1,max(XXXXX.MCC_Add2) as MCC_Add2,max(XXXXX.MCC_City_Code) as MCC_City_Code,max(XXXXX.MCC_City_Name) as MCC_City_Name,max(XXXXX.MCC_State_Code) as MCC_State_Code,max(XXXXX.MCC_STATE_NAME) as MCC_STATE_NAME,max(XXXXX.MCC_Pin_Code) as MCC_Pin_Code,XXXXX.shift,max(XXXXX.VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,XXXXX.VSP_Code as VSP_Code,max(XXXXX.VSP_Name) as VSP_Name,max(XXXXX.VLC_Code) as VLC_Code,max(XXXXX.VLC_Name) as VLC_Name,max(XXXXX.Route_No) as Route_No ,max(XXXXX.MP_VLC_Uploader_Code) as MP_VLC_Uploader_Code,max(XXXXX.Uom_Code) as Uom_Code,sum(XXXXX.qty) as qty ," &
                   " Sum (XXXXX.FAT_KG) as FAT_KG , Sum(SNF_KG) as SNF_KG , sum (Qty_In_KG) as Qty_In_KG,convert(decimal(18,2), sum (XXXXX.Net_Amount) / nullif ( sum(XXXXX.qty),0)) as Rate,sum (XXXXX.Net_Amount) as Net_Amount,sum (XXXXX.DedAddAmount) as DedAddAmount,max(XXXXX.Bank_Code) as Bank_Code,max(XXXXX.Bank_Name) as Bank_Name ,max(XXXXX.AccountNO) as AccountNO,max(XXXXX.PayeeName) as PayeeName ,max(XXXXX.IFCICode) as IFCICode ,max(XXXXX.BankBranch) as BankBranch,max(XXXXX.MP_VLC_Uploader_Code_Last3Degit) as MP_VLC_Uploader_Code_Last3Degit, max(XXXXX.Incentive_Amount) as Incentive_Amount ,max( XXXXX.Deduction_Amount) as Deduction_Amount from ( " &
                   " Select  SSSSFinal.Comp_Code, " &
                   " SSSSFinal.Logo_Img,SSSSFinal.CompName,SSSSFinal.FromDate,SSSSFinal.ToDate,SSSSFinal.Doc_Date,SSSSFinal.Milk_Type,SSSSFinal.MCC_Code,SSSSFinal.MCC_NAME,SSSSFinal.MCC_ADD1,SSSSFinal.MCC_Add2,SSSSFinal.MCC_City_Code,SSSSFinal.MCC_City_Name,SSSSFinal.MCC_State_Code,SSSSFinal.MCC_STATE_NAME,SSSSFinal.MCC_Pin_Code,SSSSFinal.shift,SSSSFinal.VLC_Code_VLC_Uploader,SSSSFinal.VSP_Code,SSSSFinal.VSP_Name,SSSSFinal.VLC_Code,SSSSFinal.VLC_Name,SSSSFinal.Route_No,SSSSFinal.MP_VLC_Uploader_Code,SSSSFinal.Uom_Code,SSSSFinal.qty,SSSSFinal.FAT_PER,SSSSFinal.SNF_PER,SSSSFinal.Rate,SSSSFinal.Net_Amount,SSSSFinal.DedAddAmount,SSSSFinal.Bank_Code,SSSSFinal.Bank_Name,SSSSFinal.AccountNO,SSSSFinal.PayeeName,SSSSFinal.IFCICode,SSSSFinal.BankBranch,SSSSFinal.MP_VLC_Uploader_Code_Last3Degit, SSSSFinal.Incentive_Amount , SSSSFinal.Deduction_Amount ,(SSSSFinal.FAT_PER *  (zzz.CF*SSSSFinal.Qty) ) /100  as FAT_KG,   (SSSSFinal.SNF_PER * (zzz.CF*SSSSFinal.Qty) ) /100  as  SNF_KG,zzz.CF*SSSSFinal.Qty as Qty_In_KG " &
                   " from ( " &
                   " " + qry + "  " &
                   " ) SSSSFinal left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/  nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =SSSSFinal.UOM_CODE and lower(zzz.TOUOM)='KG' " &
                   " )   XXXXX " &
                   " Group by XXXXX.VSP_Code,XXXXX.Doc_Date,XXXXX.shift " &
                   " ) FinalXXX left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = FinalXXX.Comp_Code " &
                   "  left outer join (select   Farmer_Code ,left ( Farmer_Invoice_No,(len(Farmer_Invoice_No)- ( len(Farmer_Code)+1 ))) as Farmer_Invoice_No ,MP_Adjust_Amount,Deduction_Amount as OtherAmount,MCC_Sale_Amount-MCC_Sale_Return_Amount as FeedAmount,Total_Advance_Amount as LoanAmount,Farmer_Name,case when isnull(Milk_Qty,0)>0 then Incentive_Amount/Milk_Qty  end IncentivePerLitre  from TSPL_MP_PAY_PROCESS_DETAIL  where  TSPL_MP_PAY_PROCESS_DETAIL.Doc_No = '" + fndDocNo.Value + "') as  TBL_Farmer_Invoice on FinalXXX.VSP_CODE = TBL_Farmer_Invoice.Farmer_Code left outer join TSPL_MCC_ROUTE_MASTER  on TSPL_MCC_ROUTE_MASTER .route_code=finalxxx.Route_No LEFT OUTER jOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_MCC_ROUTE_MASTER .Supervisor_Name  " &
                   " where 2=2  and Shift = 'M' " &
                   "  " &
                   "  UNION ALL " &
                   "  " &
                    " select TBL_Farmer_Invoice.Farmer_Invoice_No,VLC_Code_VLC_Uploader,TBL_Farmer_Invoice.MP_Adjust_Amount as OutStanding_Amt ,RIGHT ( FinalXXX.VLC_Code_VLC_Uploader,3) as VLC_Code_VLC_Uploader_3Digit, FinalXXX.Comp_Code," + IIf(isPreFormatePrint = True, "null", "TSPL_COMPANY_MASTER.Logo_Img") + " as Logo_Img ,FinalXXX.CompName as CompName,FinalXXX.FromDate as FromDate ,FinalXXX.ToDate  as ToDate,FinalXXX.Doc_Date,FinalXXX.Milk_Type as Milk_Type,FinalXXX.MCC_Code as MCC_Code,FinalXXX.MCC_NAME as MCC_NAME,FinalXXX.MCC_ADD1 as MCC_ADD1,FinalXXX.MCC_Add2 as MCC_Add2,FinalXXX.MCC_City_Code as MCC_City_Code,FinalXXX.MCC_City_Name as MCC_City_Name,FinalXXX.MCC_State_Code as MCC_State_Code,FinalXXX.MCC_STATE_NAME as MCC_STATE_NAME,FinalXXX.MCC_Pin_Code as MCC_Pin_Code,FinalXXX.shift,FinalXXX.VSP_Code as VSP_Code,FinalXXX.VSP_Name as VSP_Name,FinalXXX.VLC_Code as VLC_Code,FinalXXX.VLC_Name as VLC_Name,FinalXXX.Route_No as Route_No ,FinalXXX.MP_VLC_Uploader_Code as MP_VLC_Uploader_Code,FinalXXX.Uom_Code as Uom_Code " &
                   "  ,0 as qty ,  0  as FAT_PER ,0 as SNF_PER,0 as Rate,0 as Net_Amount,0 as DedAddAmount " &
                    " ,FinalXXX.Bank_Code as Bank_Code,FinalXXX.Bank_Name as Bank_Name ,FinalXXX.AccountNO as AccountNO,FinalXXX.PayeeName as PayeeName " &
                        " ,FinalXXX.IFCICode as IFCICode ,FinalXXX.BankBranch as BankBranch,FinalXXX.MP_VLC_Uploader_Code_Last3Degit as MP_VLC_Uploader_Code_Last3Degit " &
                        " , 0 as Incentive_Amount ,0 as Deduction_Amount   ,0 as FeedAmount ,0 as LoanAmount ,0 as OtherAmount " &
                        " ,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_EMPLOYEE_MASTER.Emp_Name AS SuperVisorName ,TBL_Farmer_Invoice.Farmer_Name ,0 as  IncentivePerLitre ,FinalXXX.qty AS EQty " &
                       " ,convert ( decimal(18,2), ((FinalXXX.Fat_KG * 100) / nullif (FinalXXX.Qty_In_KG,0) )) AS EFAT_PER " &
                       " ,convert (decimal(18,2), ((FinalXXX.SNF_KG * 100 )/ nullif (FinalXXX.Qty_In_KG,0))) AS ESNF_PER " &
                       " ,Rate AS ERate,FinalXXX.Net_Amount AS ENetAmount,FinalXXX.DedAddAmount AS EDedAddAmoun,FinalXXX.Incentive_Amount AS EIncentive_amount " &
                       " ,FinalXXX.Deduction_Amount AS Ededuction_amount,TBL_Farmer_Invoice.FeedAmount AS EfeedAmount " &
                    " ,TBL_Farmer_Invoice .LoanAmount AS ELoanAmount,TBL_Farmer_Invoice .OtherAmount AS EOtherAmount,isnull (TBL_Farmer_Invoice.IncentivePerLitre,0) AS EIncentivePErLitre " &
                    " from ( " &
                   " select " &
                   " max(XXXXX.Comp_Code) as Comp_Code, " &
                   " max(XXXXX.CompName) as CompName,max(XXXXX.FromDate) as FromDate ,max(XXXXX.ToDate)  as ToDate,XXXXX.Doc_Date,max(XXXXX.Milk_Type) as Milk_Type,max(XXXXX.MCC_Code) as MCC_Code,max(XXXXX.MCC_NAME) as MCC_NAME,max(XXXXX.MCC_ADD1) as MCC_ADD1,max(XXXXX.MCC_Add2) as MCC_Add2,max(XXXXX.MCC_City_Code) as MCC_City_Code,max(XXXXX.MCC_City_Name) as MCC_City_Name,max(XXXXX.MCC_State_Code) as MCC_State_Code,max(XXXXX.MCC_STATE_NAME) as MCC_STATE_NAME,max(XXXXX.MCC_Pin_Code) as MCC_Pin_Code,XXXXX.shift,max(XXXXX.VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,XXXXX.VSP_Code as VSP_Code,max(XXXXX.VSP_Name) as VSP_Name,max(XXXXX.VLC_Code) as VLC_Code,max(XXXXX.VLC_Name) as VLC_Name,max(XXXXX.Route_No) as Route_No ,max(XXXXX.MP_VLC_Uploader_Code) as MP_VLC_Uploader_Code,max(XXXXX.Uom_Code) as Uom_Code,sum(XXXXX.qty) as qty ," &
                   " Sum (XXXXX.FAT_KG) as FAT_KG , Sum(SNF_KG) as SNF_KG , sum (Qty_In_KG) as Qty_In_KG,convert(decimal(18,2), sum (XXXXX.Net_Amount) / nullif ( sum(XXXXX.qty),0)) as Rate,sum (XXXXX.Net_Amount) as Net_Amount,sum (XXXXX.DedAddAmount) as DedAddAmount,max(XXXXX.Bank_Code) as Bank_Code,max(XXXXX.Bank_Name) as Bank_Name ,max(XXXXX.AccountNO) as AccountNO,max(XXXXX.PayeeName) as PayeeName,max(XXXXX.IFCICode) as IFCICode ,max(XXXXX.BankBranch) as BankBranch,max(XXXXX.MP_VLC_Uploader_Code_Last3Degit) as MP_VLC_Uploader_Code_Last3Degit, max(XXXXX.Incentive_Amount) as Incentive_Amount ,max( XXXXX.Deduction_Amount) as Deduction_Amount from ( " &
                   " Select  SSSSFinal.Comp_Code, " &
                   " SSSSFinal.Logo_Img,SSSSFinal.CompName,SSSSFinal.FromDate,SSSSFinal.ToDate,SSSSFinal.Doc_Date,SSSSFinal.Milk_Type,SSSSFinal.MCC_Code,SSSSFinal.MCC_NAME,SSSSFinal.MCC_ADD1,SSSSFinal.MCC_Add2,SSSSFinal.MCC_City_Code,SSSSFinal.MCC_City_Name,SSSSFinal.MCC_State_Code,SSSSFinal.MCC_STATE_NAME,SSSSFinal.MCC_Pin_Code,SSSSFinal.shift,SSSSFinal.VLC_Code_VLC_Uploader,SSSSFinal.VSP_Code,SSSSFinal.VSP_Name,SSSSFinal.VLC_Code,SSSSFinal.VLC_Name,SSSSFinal.Route_No,SSSSFinal.MP_VLC_Uploader_Code,SSSSFinal.Uom_Code,SSSSFinal.qty,SSSSFinal.FAT_PER,SSSSFinal.SNF_PER,SSSSFinal.Rate,SSSSFinal.Net_Amount,SSSSFinal.DedAddAmount,SSSSFinal.Bank_Code,SSSSFinal.Bank_Name,SSSSFinal.AccountNO,SSSSFinal.PayeeName,SSSSFinal.IFCICode,SSSSFinal.BankBranch,SSSSFinal.MP_VLC_Uploader_Code_Last3Degit, SSSSFinal.Incentive_Amount , SSSSFinal.Deduction_Amount ,(SSSSFinal.FAT_PER *  (zzz.CF*SSSSFinal.Qty) ) /100  as FAT_KG,   (SSSSFinal.SNF_PER * (zzz.CF*SSSSFinal.Qty) ) /100  as  SNF_KG,zzz.CF*SSSSFinal.Qty as Qty_In_KG " &
                   " from ( " &
                   " " + qry + "  " &
                   " ) SSSSFinal left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/  nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =SSSSFinal.UOM_CODE and lower(zzz.TOUOM)='KG' " &
                   " )   XXXXX " &
                   " Group by XXXXX.VSP_Code,XXXXX.Doc_Date,XXXXX.shift " &
                   " ) FinalXXX left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = FinalXXX.Comp_Code " &
                   "  left outer join (select   Farmer_Code ,left ( Farmer_Invoice_No,(len(Farmer_Invoice_No)- ( len(Farmer_Code)+1 ))) as Farmer_Invoice_No ,MP_Adjust_Amount,Deduction_Amount as OtherAmount,MCC_Sale_Amount-MCC_Sale_Return_Amount as FeedAmount,Total_Advance_Amount as LoanAmount,Farmer_Name,case when isnull(Milk_Qty,0)>0 then Incentive_Amount/Milk_Qty  end IncentivePerLitre  from TSPL_MP_PAY_PROCESS_DETAIL  where  TSPL_MP_PAY_PROCESS_DETAIL.Doc_No = '" + fndDocNo.Value + "') as  TBL_Farmer_Invoice on FinalXXX.VSP_CODE = TBL_Farmer_Invoice.Farmer_Code left outer join TSPL_MCC_ROUTE_MASTER  on TSPL_MCC_ROUTE_MASTER .route_code=finalxxx.Route_No LEFT OUTER jOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_MCC_ROUTE_MASTER .Supervisor_Name  " &
                   " where 2=2  and Shift = 'E' " &
                   "  ) TBL_EVENING  group by Farmer_Invoice_No,	VLC_Code_VLC_Uploader,Comp_Code,	FromDate,	ToDate,	Doc_Date,	Milk_Type,	MCC_Code,	VSP_Code,	VLC_Code,	Route_No,	MP_VLC_Uploader_Code" &
                   " order by convert (date,Doc_Date,103) asc "
            Else
                qry = qry + " order by convert (Date,Doc_Date,103) asc ,SSSS.shift desc "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            qry = "  Select Final.VSP_Code ,Final.Item_Code,max(Final.Item_Desc) As Item_Desc ,sum (Final.Item_Net_Amt) As Amount from ( " &
                  "  Select TSPL_MP_PAY_PROCESS_MCC_SALE.FARMER_CODE As VSP_Code,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_SALE_FARMER_DETAIL.Item_Net_Amt * -1 As Item_Net_Amt from TSPL_MP_PAY_PROCESS_MCC_SALE inner join TSPL_MCC_SALE_FARMER_DETAIL On TSPL_MP_PAY_PROCESS_MCC_SALE.Shipment_Doc_No = TSPL_MCC_SALE_FARMER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_FARMER_DETAIL.Item_Code where TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocNo.Value + "' " &
                  "  Union All " &
                  "  select TSPL_MCC_SALE_RETURN_HEAD_FARMER.FARMER_CODE as  VSP_Code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount  as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN   inner join TSPL_MCC_SALE_RETURN_DETAIL_FARMER on TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Item_Code   left outer join TSPL_MCC_SALE_RETURN_HEAD_FARMER on TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Document_Code   where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" + fndDocNo.Value + "' " &
                  " ) Final group by Final.VSP_Code ,Final.Item_Code "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If isPreFormatePrint = True Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dt2, "crptPaymentProcessFarmerPredefineFormate2", "SubMilkPurchaseBill.rpt", "SubMilkPurchaseBill.rpt", "Address.rpt")
                    frmCRV = Nothing
                Else
                    'frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dt2, "crptPaymentProcessFarmer", "SubMilkPurchaseBill.rpt", "SubMilkPurchaseBill.rpt", "Address.rpt")
                    'frmCRV = Nothing
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtNEFTUploaderREFNo_TextChanged(sender As Object, e As EventArgs) Handles txtNEFTUploaderREFNo.TextChanged

    End Sub
End Class
