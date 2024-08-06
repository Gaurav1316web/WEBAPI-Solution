'===================BM00000007864,BM00000007337,BM00000007744===================
Imports System.Data.SqlClient
Imports System.IO
Imports common
Imports Newtonsoft.Json.Linq


Public Class FrmPaymentProcess
    Inherits FrmMainTranScreen
    'Check out prabhakar 22/06/2020
#Region "Variables"
    Public isEmpOnAmtOnly As Boolean = False
    Public Const colSlno As String = "colSlno"
    Public Const colPurchaseInvoiceNo As String = "colPurchaseInvoiceNo"
    Public Const colAPInvoiceNo As String = "colAPInvoiceNo"
    Public Const colAPInvoiceType As String = "colAPInvoiceType"
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
    Public Const colVLCCode As String = "colVLCCode"
    Public Const colVLCName As String = "colVLCName"
    Public Const colVLCUploaderCode As String = "colVLCUploaderCode"
    Public Const colMCCCode As String = "colMCCCode"
    Public Const colRouteCode As String = "colRouteCode"
    Public Const colRouteName As String = "colRouteName"
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

    Public Const colFATPer As String = "colFATPer"
    Public Const colFATKG As String = "colFATKG"
    Public Const colSNFPer As String = "colSNFPer"
    Public Const colSNFKG As String = "colSNFKG"

    Public Const colItemAmt As String = "colItemAmt"
    Public Const colEmpAmt As String = "colEmpAmt"
    Public Const colHandlingCharges As String = "colHandlingCharges"
    Public Const colSRNNetAmount As String = "colSRNNetAmount"
    Public Const colSRNROAmt As String = "colSRNROAmt"

    Public Const colInvAndEmpAmt As String = "colInvAndEmpAmt"
    Public Const colIncenAmt As String = "colIncenAmt"
    Public Const colIncenEmpAmt As String = "colIncenEmpAmt"
    Public Const colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt As String = "colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt"
    Public Const colTDSAmt As String = "colTDSAmt"
    Public Const colVSPOwnSystemAmt As String = "colVSPOwnSystemAmt"
    Public Const colHeadLoadAmt As String = "colHeadLoadAmt"
    Public Const colInvDeduc As String = "colInvDeduc"
    Public Const colReduceDeduc As String = "colReduceDeduc"
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
    Public Const colAPInstallmentAmt As String = "colAPInstallmentAmt"
    Public Const colAPPaymentAmtBalance As String = "colAPPaymentAmtBalance"
    Public Const colAPNoOfInstallment As String = "colAPNoOfInstallment"
    Public Const colInstallmentAmt As String = "colInstallmentAmt"
    Public Const colOrgBalanceAmt As String = "colOrgBalanceAmt"


    Public Const colALSelect As String = "colALSelect"
    Public Const colALSNo As String = "colALSNo"
    Public Const colALVendorCode As String = "colALVendorCode"
    Public Const colALVendorName As String = "colALVendorName"
    Public Const colALPaymentCode As String = "colALPaymentCode"
    Public Const colALPaymentDate As String = "colALPaymentDate"
    Public Const colALPaymentAmt As String = "colALPaymentAmt"

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
    Dim strMccCodes As String = ""
    Dim strCustomerCode As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isLoad As Boolean = True
    Dim isNewEntry As Boolean = False
    Dim frm As FrmPaymentDetail = New FrmPaymentDetail()
    'Dim chkArea As Boolean = False



    Public Const colMccSaleTotalAmount As String = "colMccSaleTotalAmount"
    Public Const colItemIssueTotalAmount As String = "colItemIssueTotalAmount"
    Public Const colItemIssueReturnTotalAmount As String = "colItemIssueReturnTotalAmount"
    Public Const colDeductionTotalAmount As String = "colDeductionTotalAmount"
    Public Const colAssetLostAmount As String = "colAssetLostAmount"
    Public Const colTotalCreditNoteAmount As String = "colTotalCreditNoteAmount"
    Public Const colTotalCompulsoryAmount As String = "colTotalCompulsoryAmount"
    Public Const colTotalSavingAmount As String = "colTotalSavingAmount"
    Public Const colAdvanceAmount As String = "colAdvanceTotal"
    Public Const colAdvanceKnockOffAmount As String = "colAdvanceKnockOffAmount"

    Public colBankCode As String = "colBankCode"
    Public colBankDesc As String = "colBankDesc"
    Public colPayMode As String = "colPayMode"
    Public colChequeNo As String = "colChequeNo"
    Public colChequeDate As String = "colChequeDate"
    Public isCellValueChanged = False
    Public PayProcessDocNo As String = ""
    Public colActualVSPCode As String = "colActualVSPCode"
    Public colActualVSPName As String = "colActualVSPName"



    Public Const colBankCodeSaving As String = "colBankCodeSaving"
    Public Const colBankDescSaving As String = "colBankDescSaving"
    Public Const colPayModeSaving As String = "colPayModeSaving"

    Public Const colIsPaymentProcessHoldSaving As String = "colIsPaymentProcessHoldSaving"
    Public Const colIsPaymentProcessHoldSavingAuto As String = "colIsPaymentProcessHoldSavingAuto"
    Public Const colIsPaymentProcessHoldSavingManual As String = "colIsPaymentProcessHoldSavingManual"

    '============Added By Rohit,========================
    Public Const colMccSaleReturnTotalAmount As String = "colMccSaleReturnTotalAmount"

    Private isConsiderAdvancePayment As Boolean = False
    Private PayableAmountZeroForMCCSale As Boolean = False
    Dim isPickPendingMilkSRNinNextPaymentCycle As Boolean = False
    Dim AreaWiseBilling As Boolean = False
    Dim PaymentProcessInHindi As Boolean = False
    Dim IsRoundOffPaiseAmount As Boolean
    Dim settingShowFATSNF As Boolean = False
    Dim SettShowMCCFinder As Boolean = False
    Dim ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster As Boolean = False
    Dim MultipleFinderFillAuto As Boolean = False
    Dim SettVSPHoldPaymentNotCompanyBank As Boolean = False
    Dim SetCowFatPer As Decimal = 0
    'Dim AreaWiseBilling As Boolean = False
    Dim Is_gv_Rows_Clear As Boolean = False
    Dim PrintHindi As Boolean = False
    Public fontInstalled As Boolean = False
#End Region

    Private Sub FrmProvisionEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SetCowFatPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CowFATPer, clsFixedParameterCode.CowFATPer, Nothing))
        SettVSPHoldPaymentNotCompanyBank = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VSPHoldPaymentNotCompanyBank, clsFixedParameterCode.VSPHoldPaymentNotCompanyBank, Nothing)) = 1)
        IsRoundOffPaiseAmount = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RoundOffPaiseAmount, clsFixedParameterCode.RoundOffPaiseAmount, Nothing)) = 1)
        isConsiderAdvancePayment = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsiderAdvancePayment, clsFixedParameterType.ConsiderAdvancePayment, Nothing)) = 1, True, False)
        PayableAmountZeroForMCCSale = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PayableAmountZeroForMCCSale, clsFixedParameterType.PayableAmountZeroForMCCSale, Nothing)) = 1, True, False)
        settingShowFATSNF = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowFATSNFinPaymentProcess, clsFixedParameterType.ShowFATSNFinPaymentProcess, Nothing)) = 1, True, False)
        ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) > 0, True, False)
        RadPageView1.Pages("RadPageViewPage7").Item.Visibility = IIf(isConsiderAdvancePayment, Telerik.WinControls.ElementVisibility.Visible, Telerik.WinControls.ElementVisibility.Collapsed)
        chkSkipPreviousDocumentOfAdvancePayment.Visible = isConsiderAdvancePayment
        RadPageView1.SelectedPage = RadPageViewPage1
        'AreaWiseBilling = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)
        AreaWiseBilling = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)
        Label1.Visible = AreaWiseBilling
        fndArea.Visible = AreaWiseBilling
        PaymentProcessInHindi = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PaymentProcessPrintInHindi, clsFixedParameterCode.PaymentProcessPrintInHindi, Nothing)) = 1)

        If PaymentProcessInHindi = True Then
            btnPrintHindi.Visible = PaymentProcessInHindi
            ' btnPrint.Enabled = False
        End If


        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        isPickPendingMilkSRNinNextPaymentCycle = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickPendingMilkSRNinNextPaymentCycle, clsFixedParameterCode.PickPendingMilkSRNinNextPaymentCycle, Nothing)) = 1
        SettShowMCCFinder = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMCCFinderInPaymentProcess, clsFixedParameterCode.ShowMCCFinderInPaymentProcess, Nothing)) = 1)
        txtMCC.Visible = SettShowMCCFinder
        lblMCC.Visible = SettShowMCCFinder
        lblMCC2.Visible = SettShowMCCFinder



        MultipleFinderFillAuto = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MultipleFinderFillAuto, clsFixedParameterCode.MultipleFinderFillAuto, Nothing)) = 1)
        mfndMcc.Visible = False
        If MultipleFinderFillAuto Then
            fndLoc.Enabled = False
            txtLocName.Enabled = False
        Else
            fndLoc.Enabled = True
            txtLocName.Enabled = True
        End If

        If MultipleFinderFillAuto Then
            FillAllMCCDefault()
            AutoFillAllVSP()
        End If

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
        'gvInvoice.Rows.Clear()
        gvInvoice.Columns.Clear()

        gvInvoice.DataSource = Nothing

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
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
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
        colTextBox.HeaderText = "MCC Code"
        colTextBox.Name = colMCCCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Route Code"
        colTextBox.Name = colRouteCode
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Route"
        colTextBox.Name = colRouteName
        colTextBox.Width = 150
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
        colDecimal.HeaderText = "FAT %"
        colDecimal.Name = colFATPer
        colDecimal.Width = 80
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = settingShowFATSNF
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "FAT Kg"
        colDecimal.Name = colFATKG
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = settingShowFATSNF
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "SNF %"
        colDecimal.Name = colSNFPer
        colDecimal.Width = 80
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = settingShowFATSNF
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "SNF Kg"
        colDecimal.Name = colSNFKG
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = settingShowFATSNF
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
        colDecimal.HeaderText = "SRN RO Amt"
        colDecimal.Name = colSRNROAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "SRN Net Amount"
        colDecimal.Name = colSRNNetAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Handling Charges"
        colDecimal.Name = colHandlingCharges
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
        colDecimal.HeaderText = "TDS Amount"
        colDecimal.Name = colTDSAmt
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
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


    Sub getMCCs()
        strMccCodes = ""
        If gvInvoice.Rows.Count > 0 Then
            For i As Integer = 0 To gvInvoice.Rows.Count - 1
                If gvInvoice.Rows(i).Cells(colSelect).Value = True Then
                    strMccCodes = strMccCodes & "'" & gvInvoice.Rows(i).Cells(colMCCCode).Value & "',"
                End If
            Next
            If clsCommon.myLen(strMccCodes) > 0 Then
                strMccCodes = Microsoft.VisualBasic.Left(strMccCodes, Microsoft.VisualBasic.Len(strMccCodes) - 1)
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

    Function getTDSTotalCreditNoteSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvCreditNote IsNot Nothing AndAlso gvCreditNote.Rows.Count > 0 Then
                For i As Integer = 0 To gvCreditNote.Rows.Count - 1
                    If gvCreditNote.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvCreditNote.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then
                        rValue = rValue + clsCommon.myCdbl(gvCreditNote.Rows(i).Cells(colTDSAmt).Value)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Function getTotalSavingSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvSaving IsNot Nothing AndAlso gvSaving.Rows.Count > 0 Then
                For i As Integer = 0 To gvSaving.Rows.Count - 1
                    If gvSaving.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvSaving.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then
                        rValue = rValue + clsCommon.myCdbl(gvSaving.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalCompulsorySum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvCompulsory IsNot Nothing AndAlso gvCompulsory.Rows.Count > 0 Then
                For i As Integer = 0 To gvCompulsory.Rows.Count - 1
                    If gvCompulsory.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvCompulsory.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then
                        rValue = rValue + clsCommon.myCdbl(gvCompulsory.Rows(i).Cells(colItemAmt).Value)
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
                        If clsCommon.CompairString(gvAdvancePayment.Rows(i).Cells(colAPVendorCode).Value, strVendorCode) = CompairStringResult.Equal Then
                            If clsCommon.myCBool(gvAdvancePayment.Rows(i).Cells(colAPSelect).Value) Then
                                If clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPNoOfInstallment).Value) > 0 Then
                                    rValue = rValue + clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPInstallmentAmt).Value)
                                Else
                                    rValue = rValue + clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPPaymentAmtBalance).Value)
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Math.Round(rValue, 2, MidpointRounding.ToEven)
    End Function


    Function getTotalAssetLost(ByVal strVendorCode As String) As Double
        Dim rValue As Double = 0
        Try
            If True Then
                If clsCommon.myLen(strVendorCode) > 0 AndAlso gvAssetLost IsNot Nothing AndAlso gvAssetLost.Rows.Count > 0 Then
                    For i As Integer = 0 To gvAssetLost.Rows.Count - 1
                        If clsCommon.CompairString(gvAssetLost.Rows(i).Cells(colALVendorCode).Value, strVendorCode) = CompairStringResult.Equal Then
                            If clsCommon.myCBool(gvAssetLost.Rows(i).Cells(colALSelect).Value) Then
                                rValue = rValue + clsCommon.myCdbl(gvAssetLost.Rows(i).Cells(colALPaymentAmt).Value)
                            End If
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

    Function getAssetLostList(ByVal vsp As String) As DataTable
        Dim rValue As DataTable = Nothing
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvAssetLost IsNot Nothing AndAlso gvAssetLost.Rows.Count > 0 Then
                rValue = GetDataTable(gvAssetLost, colVendorCode, vsp)
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
        If Is_gv_Rows_Clear Then
            gv.Rows.Clear()
        End If
        gv.Columns.Clear()
        gv.DataSource = Nothing
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

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "On Hold Saving"
        colChkBox.Name = colIsPaymentProcessHoldSaving
        colChkBox.ReadOnly = True
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(colChkBox)

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "On Hold Saving Auto"
        colChkBox.Name = colIsPaymentProcessHoldSavingAuto
        colChkBox.ReadOnly = True
        colChkBox.IsVisible = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(colChkBox)

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "On Hold Saving Manual"
        colChkBox.Name = colIsPaymentProcessHoldSavingManual
        colChkBox.ReadOnly = True
        colChkBox.Width = 50
        colChkBox.IsVisible = False
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(colChkBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Purchase Invoice No"
        colTextBox.Name = colPurchaseInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colDate = New GridViewDateTimeColumn
        colDate.HeaderText = "Purchase Invoice Date"
        colDate.Name = colPurchaseInvoiceDate
        colDate.Format = DateTimePickerFormat.Custom
        colDate.CustomFormat = "dd/MM/yyyy"
        colDate.FormatString = "{0:dd/MM/yyyy}"
        colDate.Width = 150
        colDate.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDate)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)



        colDate = New GridViewDateTimeColumn
        colDate.HeaderText = "AP Invoice Date"
        colDate.Name = colAPInvoiceDate
        colDate.Format = DateTimePickerFormat.Custom
        colDate.CustomFormat = "dd/MM/yyyy"
        colDate.FormatString = "{0:dd/MM/yyyy}"
        colDate.Width = 150
        colDate.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDate)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
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
        colTextBox.HeaderText = "MCC Code"
        colTextBox.Name = colMCCCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Route Code"
        colTextBox.Name = colRouteCode
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Route"
        colTextBox.Name = colRouteName
        colTextBox.Width = 150
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
        colTextBox1.HeaderText = "Current Bank code"
        colTextBox1.Name = colBankCode
        colTextBox1.Width = 200
        colTextBox1.ReadOnly = True

        'colTextBox.IsVisible = False
        gv.MasterTemplate.Columns.Add(colTextBox1)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Current Bank Desc"
        colTextBox.Name = colBankDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        'colTextBox.IsVisible = False
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Current Payment Mode"
        colTextBox.Name = colPayMode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        'colTextBox.IsVisible = False
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Current Cheque No"
        colTextBox.Name = colChequeNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = False
        colTextBox.IsVisible = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colDate = New GridViewDateTimeColumn
        colDate.FormatString = ""
        colDate.HeaderText = "Current Cheque Date"
        colDate.Name = colChequeDate
        colDate.CustomFormat = "dd/MM/yyyy"
        colDate.FormatString = "{0:dd/MM/yyyy}"
        colDate.Width = 100
        colDate.ReadOnly = False
        colDate.IsVisible = True
        gv.MasterTemplate.Columns.Add(colDate)


        colTextBox1 = New GridViewTextBoxColumn()
        colTextBox1.FormatString = ""
        colTextBox1.HeaderText = "Saving Bank code"
        colTextBox1.Name = colBankCodeSaving
        colTextBox1.Width = 200
        colTextBox1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox1)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Saving Bank Desc"
        colTextBox.Name = colBankDescSaving
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Saving Payment Mode"
        colTextBox.Name = colPayModeSaving
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Qty"
        colDecimal.Name = colMilkQty
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "FAT %"
        colDecimal.Name = colFATPer
        colDecimal.Width = 80
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = settingShowFATSNF
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "FAT Kg"
        colDecimal.Name = colFATKG
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = settingShowFATSNF
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "SNF %"
        colDecimal.Name = colSNFPer
        colDecimal.Width = 80
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = settingShowFATSNF
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "SNF Kg"
        colDecimal.Name = colSNFKG
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = settingShowFATSNF
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
        colDecimal.HeaderText = "SRN RO Amt"
        colDecimal.Name = colSRNROAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "SRN Net Amount"
        colDecimal.Name = colSRNNetAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Handling Charges"
        colDecimal.Name = colHandlingCharges
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "TDS Amount"
        colDecimal.Name = colTDSAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
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
        colDecimal.HeaderText = "Asset Lost Amount"
        colDecimal.Name = colAssetLostAmount
        colDecimal.Width = 100
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
        colDecimal.HeaderText = "Saving Amount"
        colDecimal.Name = colTotalSavingAmount
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Compulsory Amount"
        colDecimal.Name = colTotalCompulsoryAmount
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Payble Amount"
        ''richa agarwal
        colDecimal.FormatString = "{0:n2}"
        ''----------------
        colDecimal.Name = colPaybleAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        gv.Columns(colBankCode).HeaderImage = Global.ERP.My.Resources.Resources.search4
        gv.Columns(colBankCode).TextImageRelation = TextImageRelation.TextBeforeImage

        gv.Columns(colPayMode).HeaderImage = Global.ERP.My.Resources.Resources.search4
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
            Dim qry As String = "select cast(1 as bit) as Sel,ROW_NUMBER() over(order by x.[Milk Purchase Invoice Doc No]) as SNo ,x.*,Extra.Milk_OW_Amt_Document_No,Extra.HeadLoadAmt_Document_No,Extra.MilkDedAmt_Document_No,Extra.Milk_OW_Amt,Extra.HeadLoadAmt,Extra.MilkDedAmt from (select   MAX( xxx.[AP Invoice Doc No]) as [AP Invoice Doc No] ,max(xxx.[Ap Invoice Doc Date]) as [Ap Invoice Doc Date] ,xxx.[Milk Purchase Invoice Doc No] as [Milk Purchase Invoice Doc No],max(xxx.[Milk Purchase Invoice Doc Date]) as [Milk Purchase Invoice Doc Date],max(VLC_Code) as VLC_Code,max(xxx.VLC_Name) as VLC_Name,max(xxx.Vendor_Code)  as Vendor_Code,max(xxx.Vendor_Name) as Vendor_Name,max(xxx.[Payee/Joint Name]) as [Payee/Joint Name],max(xxx.[Bank Code]) as [Bank Code],max(xxx.[Bank Name]) as  [Bank Name] , max(xxx.[Branch Code]) as [Branch Code],max(xxx.[Branch Name]) as [Branch Name],max(xxx.[IFSC Code]) as  [IFSC Code],SUM(xxx.qty) as [Total Qty]   ,max(xxx.TOTAL_basic_amount) as TOTAL_basic_amount,max(xxx.TOTAL_AMOUNT ) as TOTAL_AMOUNT,MAX(xxx.TOTAL_PaymentCOMMISSION) as TOTAL_PaymentCOMMISSION,MAX(xxx.Incentive_Head ) as Incentive_Head, MAX(xxx .IncentiveEMP_Head ) as  IncentiveEMP_Head,sum(Service_Charge_Amount) as Service_Charge_Amount,max(xxx.TOTAL_AMOUNT_Acc  ) as TOTAL_AMOUNT_Acc,max(xxx.MCC_CODE) as  MCC_CODE,max(xxx.AccountNo) as AccountNo,max(MP_Amount) as MP_Amount,max(MP_EMP) as MP_EMP,max(MP_Incentive) as MP_Incentive,max(MP_IncentiveEMP) as MP_IncentiveEMP,max(Handling_Charges_Amount) as Handling_Charges_Amount,max(SRN_Net_Amount) as SRN_Net_Amount,max(SRN_RO_Amount) as SRN_RO_Amount,cast( sum(FATKg) as decimal(18,3)) as FATKg,cast(case when sum(ACC_Qty)=0 then 0 else sum(FATKg)*100/sum(ACC_Qty) end as decimal(18,2) ) as FATPer  ,cast( sum(SNFKg) as decimal(18,3)) as SNFKg,cast(case when sum(ACC_Qty)=0 then 0 else sum(SNFKg)*100/sum(ACC_Qty) end as decimal(18,2) ) as SNFPer,max(Calculated_TDS) as Calculated_TDS ,max(ROUTE_CODE) as ROUTE_CODE ,max(ROUTE_NAME) as ROUTE_NAME
from ( 	   select  TSPL_VENDOR_INVOICE_HEAD.Document_No as [AP Invoice Doc No], TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date as [Ap Invoice Doc Date], TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as [Milk Purchase Invoice Doc No],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE  as [Milk Purchase Invoice Doc Date],coalesce(TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader,mp_vlc.vlc_code_vlc_uploader) as VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ,coalesce(TSPL_VLC_MASTER_HEAD.VLC_Name ,mp_vlc.vlc_name) as vlc_name,coalesce(TSPL_VENDOR_MASTER.Vendor_Code,mp_v.vendor_Code) as vendor_Code,coalesce(TSPL_VENDOR_MASTER.Vendor_Name,mp_v.Vendor_name) as Vendor_name , coalesce(TSPL_VENDOR_MASTER.VSP_Payee_Name,Mp_V.VSP_Payee_Name) as [Payee/Joint Name], case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then ''  else ''   end as [Branch Code],case when isnull (coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(SelfBank .Bank_Name,selfBank_mp.bank_name)  else coalesce(jointBank .Bank_Name,jointBank_MP .Bank_Code)   end as [Bank Name],case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(SelfBank .Bank_Code,SelfBank_MP .Bank_Code)   else coalesce(jointBank .Bank_Code,jointBank_Mp .Bank_Code)    end as [Bank Code],case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(TSPL_VENDOR_MASTER .Branch_Name,MP_V .Branch_Name )   else coalesce(TSPL_VENDOR_MASTER .Joint_Branch_Name,Mp_V.Joint_Branch_Name)   end as [Branch Name],case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(TSPL_VENDOR_MASTER .IFSC_Code,mp_V.IFSC_Code)   else coalesce(TSPL_VENDOR_MASTER .Joint_IFSC_Code,mp_v.Joint_IFSC_Code)   end as [IFSC Code],case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(TSPL_VENDOR_MASTER .Account_No,mp_V .Account_No)    else coalesce(TSPL_VENDOR_MASTER.Joint_Account_No,mp_V.Joint_Account_No)    end as [AccountNo],TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty,TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_basic_amount,TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_AMOUNT  , TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION, TSPL_MILK_PURCHASE_INVOICE_HEAD.Incentive_Head,TSPL_MILK_PURCHASE_INVOICE_HEAD.IncentiveEMP_Head, TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_AMOUNT_Acc ,  TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Service_Charge_Amount,TSPL_MILK_PURCHASE_INVOICE_HEAD.MP_Amount,TSPL_MILK_PURCHASE_INVOICE_HEAD.MP_EMP,TSPL_MILK_PURCHASE_INVOICE_HEAD.MP_Incentive,TSPL_MILK_PURCHASE_INVOICE_HEAD.MP_IncentiveEMP,TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount,TSPL_MILK_PURCHASE_INVOICE_HEAD.SRN_Net_Amount,TSPL_MILK_PURCHASE_INVOICE_HEAD.SRN_RO_Amount,TSPL_MILK_PURCHASE_INVOICE_DETAIL.ACC_Qty,cast((TSPL_MILK_PURCHASE_INVOICE_DETAIL.ACC_Qty*TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER/100) as decimal(18,2)) as FATKg,cast((TSPL_MILK_PURCHASE_INVOICE_DETAIL.ACC_Qty*TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER/100)as decimal(18,2)) as SNFKg,TSPL_REMITTANCE.Calculated_TDS,TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME 
from TSPL_VENDOR_INVOICE_HEAD   
left outer join TSPL_REMITTANCE on TSPL_REMITTANCE.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE= TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No 
left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE
left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE  
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
left outer join TSPL_Vendor_Bank_MASTER as jointBank on jointBank.Bank_Code =TSPL_VENDOR_MASTER .Joint_Bank_Code
left outer join TSPL_Vendor_Bank_MASTER as SelfBank on SelfBank .Bank_Code =TSPL_VENDOR_MASTER.Bank_Name 
left outer join TSPL_MP_MASTER mp on mp.MP_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_Code left outer join TSPL_VLC_MASTER_HEAD mp_vlc on mp_vlc.Vlc_Code=mp.VLC_Code Left join tspl_vendor_master Mp_V on mp_V.Vendor_Code=mp.MP_Code  left outer join TSPL_Vendor_Bank_MASTER as jointBank_Mp on jointBank_Mp.Bank_Code =Mp_V .Joint_Bank_Code   left outer join TSPL_Vendor_Bank_MASTER as SelfBank_Mp on SelfBank .Bank_Code =Mp_V.Bank_Name   
where ((TSPL_VENDOR_INVOICE_HEAD.Balance_Amt>0) or (TSPL_VENDOR_INVOICE_HEAD.Balance_Amt=0 and TSPL_VENDOR_INVOICE_HEAD.Posting_Date='" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "' ) ) and TSPL_VENDOR_INVOICE_HEAD.document_type='I' and TSPL_VENDOR_INVOICE_HEAD.Invoice_Type='AP' and TSPL_VENDOR_INVOICE_HEAD.REFDocType='MI-PI' and ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No,'')<>''   
and not exists(select 1 from TSPL_PAYMENT_PROCESS_INVOICE  where TSPL_PAYMENT_PROCESS_INVOICE.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_PAYMENT_PROCESS_INVOICE.Doc_No not in ('" + fndDocNo.Value + "')) 
) xxx where  1=1 "

            If clsCommon.myLen(fndLoc.Value) > 0 AndAlso MultipleFinderFillAuto = False Then
                qry += " And MCC_Code in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "')"
            End If
            If MultipleFinderFillAuto Then
                qry += " And MCC_Code in (" + clsCommon.GetMulcallString(mfndMcc.arrValueMember) + ") "
            End If
            If clsCommon.myLen(fndLoc.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                fndLoc.Focus()
                Throw New Exception("Please select Location")
            End If
            If MultipleFinderFillAuto Then
                If mfndMcc.arrValueMember Is Nothing OrElse mfndMcc.arrValueMember.Count <= 0 Then
                    Throw New Exception("Please select Location")
                End If
            End If
            If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
                txtVSP.Focus()
                Throw New Exception("Please select at lease one VSP ")
            End If
            qry += " And vendor_code in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
            qry += " and [Milk Purchase Invoice Doc Date] between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'   group by xxx.[Milk Purchase Invoice Doc No]
)x
left outer join (select Against_MillkPurchaseInvoice_No,max(Milk_OW_Amt_Document_No) as Milk_OW_Amt_Document_No,max(HeadLoadAmt_Document_No) as HeadLoadAmt_Document_No,max(MilkDedAmt_Document_No) as MilkDedAmt_Document_No,sum(Milk_OW_Amt) as Milk_OW_Amt,sum(HeadLoadAmt) as HeadLoadAmt,sum(MilkDedAmt) as MilkDedAmt from ( select  Against_MillkPurchaseInvoice_No
,case when Document_Type='C' and RefDocType='Milk_OW' then Document_No else '' end as Milk_OW_Amt_Document_No 
,case when Document_Type='C' and RefDocType='Milk_HE' then Document_No else '' end as HeadLoadAmt_Document_No
,case when Document_Type='D' and RefDocType='Milk_DE' then Document_No else '' end as MilkDedAmt_Document_No
,case when Document_Type='C' and RefDocType='Milk_OW' then Balance_Amt else 0 end as Milk_OW_Amt 
,case when Document_Type='C' and RefDocType='Milk_HE' then Balance_Amt else 0 end as HeadLoadAmt 
,case when Document_Type='D' and RefDocType='Milk_DE' then Balance_Amt else 0 end as MilkDedAmt  
 from TSPL_VENDOR_INVOICE_HEAD 
)x where ( Milk_OW_Amt>0 or HeadLoadAmt>0 or MilkDedAmt>0) 
group by Against_MillkPurchaseInvoice_No) as Extra on Extra.Against_MillkPurchaseInvoice_No=x.[Milk Purchase Invoice Doc No]"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvInvoice.DataSource = Nothing
                gvInvoice.AutoGenerateColumns = False
                gvInvoice.DataSource = dt
                gvInvoice.Columns(colSelect).FieldName = "Sel"
                gvInvoice.Columns(colSlno).FieldName = "SNo"
                gvInvoice.Columns(colSelect).FieldName = "Sel"
                gvInvoice.Columns(colAPInvoiceNo).FieldName = "AP Invoice Doc No"
                gvInvoice.Columns(colAPInvoiceDate).FieldName = "Ap Invoice Doc Date"
                gvInvoice.Columns(colPurchaseInvoiceNo).FieldName = "Milk Purchase Invoice Doc No"
                gvInvoice.Columns(colPurchaseInvoiceDate).FieldName = "Milk Purchase Invoice Doc Date"
                gvInvoice.Columns(colVLCCode).FieldName = "VLC_Code"
                gvInvoice.Columns(colVLCName).FieldName = "VLC_Name"
                gvInvoice.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                gvInvoice.Columns(colMCCCode).FieldName = "MCC_Code"
                gvInvoice.Columns(colRouteCode).FieldName = "ROUTE_CODE"
                gvInvoice.Columns(colRouteName).FieldName = "ROUTE_NAME"
                gvInvoice.Columns(colVendorCode).FieldName = "Vendor_Code"
                gvInvoice.Columns(colVendorDesc).FieldName = "Vendor_Name"
                gvInvoice.Columns(colPayeeJointName).FieldName = "Payee/Joint Name"
                gvInvoice.Columns(colPayeeJointBankCode).FieldName = "Bank Code"
                gvInvoice.Columns(colPayeeJointBankDesc).FieldName = "Bank Name"
                gvInvoice.Columns(colPayeeJointBranchCode).FieldName = "Branch Code"
                gvInvoice.Columns(colPayeeJointBranchDesc).FieldName = "Branch Name"
                gvInvoice.Columns(colPayeeJointIFSC).FieldName = "IFSC Code"
                gvInvoice.Columns(colPayeeJointAcNo).FieldName = "AccountNo"
                gvInvoice.Columns(colMilkQty).FieldName = "Total Qty"
                gvInvoice.Columns(colInvAmt).FieldName = "TOTAL_basic_amount"
                gvInvoice.Columns(colEmpAmt).FieldName = "TOTAL_PaymentCOMMISSION"
                gvInvoice.Columns(colHandlingCharges).FieldName = "Handling_Charges_Amount"
                gvInvoice.Columns(colSRNROAmt).FieldName = "SRN_RO_Amount"
                gvInvoice.Columns(colSRNNetAmount).FieldName = "SRN_Net_Amount"
                gvInvoice.Columns(colInvAndEmpAmt).FieldName = "TOTAL_AMOUNT"
                gvInvoice.Columns(colIncenAmt).FieldName = "Incentive_Head"
                gvInvoice.Columns(colIncenEmpAmt).FieldName = "IncentiveEMP_Head"
                gvInvoice.Columns(colFATKG).FieldName = "FATKg"
                gvInvoice.Columns(colFATPer).FieldName = "FATPer"
                gvInvoice.Columns(colSNFKG).FieldName = "SNFKg"
                gvInvoice.Columns(colSNFPer).FieldName = "SNFPer"
                gvInvoice.Columns(colMPAmount).FieldName = "MP_Amount"
                gvInvoice.Columns(colMPEMPAmount).FieldName = "MP_EMP"
                gvInvoice.Columns(colMPIncentiveAmount).FieldName = "MP_Incentive"
                gvInvoice.Columns(colMPEMPIncentiveAmount).FieldName = "MP_IncentiveEMP"
                gvInvoice.Columns(colMPNetAmount).FieldName = "MP_NetAmount"
                gvInvoice.Columns(colTDSAmt).FieldName = "Calculated_TDS"
                gvInvoice.Columns(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).FieldName = "TOTAL_AMOUNT_Acc"
                gvInvoice.Columns(colVSPOwnSystemAmt).FieldName = "Milk_OW_Amt"
                'gvInvoice.Columns(colVSPOwnSystemAmt).Tag = getVspOwnSystemINV(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value) ''BSP Pending
                gvInvoice.Columns(colHeadLoadAmt).FieldName = "HeadLoadAmt"
                'gvInvoice.Columns(colHeadLoadAmt).Tag = getHeadLoadINV(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value)''BSP Pending
                gvInvoice.Columns(colInvDeduc).FieldName = "MilkDedAmt"
                'gvInvoice.Columns(colInvDeduc).Tag = getDeductionINV(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value)''BSP Pending
                'gvInvoice.Columns(colReduceDeduc).FieldName = clsCommon.myFormat(0)
                'gvInvoice.Columns(colBankCode).FieldName = ""
                'gvInvoice.Columns(colBankDesc).FieldName = ""
                'gvInvoice.Columns(colPayMode).FieldName = ""
                'gvInvoice.Columns(colChequeNo).FieldName = ""
                gvInvoice.Columns(colServiceChargeAmt).FieldName = "Service_Charge_Amount"
            End If
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
        'gvMccSale.Rows.Clear()
        gvMccSale.Columns.Clear()
        gvMccSale.DataSource = Nothing

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
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
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
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
        colDecimal.HeaderText = "Original Balance Amount"
        colDecimal.Name = colOrgBalanceAmt
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Installment Amount"
        colDecimal.Name = colInstallmentAmt
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colDecimal)


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
        colDecimal.ReadOnly = False
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

    Sub LoadBlankGridMccSaleReturn()
        'GvMccSaleReturn.Rows.Clear()
        GvMccSaleReturn.Columns.Clear()

        GvMccSaleReturn.DataSource = Nothing

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
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
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
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

    Sub LoadBlankGridDeduction()
        'gvDeduction.Rows.Clear()
        gvDeduction.Columns.Clear()

        gvDeduction.DataSource = Nothing

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
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
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
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
        colDecimal.ReadOnly = False
        gvDeduction.MasterTemplate.Columns.Add(colDecimal)

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
        'gvAdvancePayment.Rows.Clear()
        gvAdvancePayment.Columns.Clear()
        gvAdvancePayment.DataSource = Nothing

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
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
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

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Installment Amount"
        colDecimal.Name = colAPInstallmentAmt
        colDecimal.Width = 200
        ''richa agarwal make Installment Amount column editable as per disussion with Ranjana Mam
        colDecimal.ReadOnly = False
        gvAdvancePayment.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "No Of Installment"
        colDecimal.Name = colAPNoOfInstallment
        colDecimal.IsVisible = False
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


    Sub LoadBlankGridAssetLost()
        'gvAssetLost.Rows.Clear()
        gvAssetLost.Columns.Clear()
        gvAssetLost.DataSource = Nothing
        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = " "
        colChkBox.Name = colALSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 30
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvAssetLost.MasterTemplate.Columns.Add(colChkBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SNo"
        colTextBox.Name = colALSNo
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gvAssetLost.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvAssetLost.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colALVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvAssetLost.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colALVendorName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvAssetLost.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payment No"
        colTextBox.Name = colALPaymentCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvAssetLost.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payment Date"
        colTextBox.Name = colALPaymentDate
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvAssetLost.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colALPaymentAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvAssetLost.MasterTemplate.Columns.Add(colDecimal)

        gvAssetLost.AllowAddNewRow = False
        gvAssetLost.AllowDeleteRow = False
        gvAssetLost.ShowGroupPanel = False
        gvAssetLost.AllowColumnReorder = True
        gvAssetLost.AllowRowReorder = False
        gvAssetLost.EnableSorting = True
        gvAssetLost.EnableFiltering = True
        gvAssetLost.TableElement.TableHeaderHeight = 40
        gvAssetLost.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub LoadBlankGridCreditNote()
        'gvCreditNote.Rows.Clear()
        gvCreditNote.Columns.Clear()

        gvCreditNote.DataSource = Nothing

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
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
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
        colDecimal.HeaderText = "TDS Amount"
        colDecimal.Name = colTDSAmt
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        gvCreditNote.MasterTemplate.Columns.Add(colDecimal)

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

        LoadBlankGridDeduction()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = " select cast(1 as bit) as Sel,ROW_NUMBER() over(order by "
            If PayableAmountZeroForMCCSale Then
                qry += " max(Vendor_Code),max(Sequence_No),max(Posting_Date),max(Sequence_No2) "
            Else
                qry += " Document_No "
            End If


            qry += ") as SNo,Document_No,max(Invoice_Entry_Date) as Invoice_Entry_Date,max(Vendor_Code) as Vendor_Code,max(VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,max(Vendor_Name) as Vendor_Name,(case when min(DeductionCode)<>max(DeductionCode) then '*' else '' end)+max(DeductionCode) as DeductionCode,(case when min(DeductionCode)<>max(DeductionCode) then '*' else '' end)+max(Deduction_Desc) as Deduction_Desc,Max(Total_Amount) as Total_Amount,max(Sequence_No) as Sequence_No,max(Sequence_No2) as Sequence_No2,max(Posting_Date) as Posting_Date from (  
select TSPL_VENDOR_INVOICE_DETAIL.Document_No,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,
TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.DeductionCode else TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction end as DeductionCode , case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc else TSPL_DCS_ADDITION_DEDUCTION.Description end as Deduction_Desc,
TSPL_VENDOR_INVOICE_HEAD.Balance_Amt as Total_Amount ,(case when TSPL_VENDOR_INVOICE_HEAD.RefDocType='VSP-NGT' then -2 else (case when  TSPL_DEDUCTION_MASTER.Sequence_No is null then -1 else 0 end) end)  as Sequence_No 
,TSPL_VENDOR_INVOICE_HEAD.Posting_Date 
,TSPL_DEDUCTION_MASTER.Sequence_No as Sequence_No2 
from TSPL_VENDOR_INVOICE_DETAIL 
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No 
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where  Document_Type='D' and ((TSPL_VENDOR_INVOICE_HEAD.isDeduction='1' 
and (ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,'')<>'' or ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')<>'')) or  len(coalesce(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,''))>0)  
and TSPL_VENDOR_INVOICE_HEAD.Balance_Amt>0 "

            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "ALW") = CompairStringResult.Equal Then
                qry += " and isnull(TSPL_VENDOR_INVOICE_HEAD.Saving,0)=0 and isnull(TSPL_DEDUCTION_MASTER.Is_Transfer_To_Saving,0)=0 "
            Else
                qry += " and isnull(TSPL_VENDOR_INVOICE_HEAD.Saving,0)=0 "
            End If

            Dim whrCls As String = ""
            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  in ( " & strVendorCode & ")  and  coalesce(Posting_Date,'')<>'' "
            End If

            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                whrCls += "and 2=(case when TSPL_VENDOR_INVOICE_HEAD.isDeduction='1' and TSPL_VENDOR_INVOICE_DETAIL.DeductionCode='SECURITY DED' then" + Environment.NewLine +
                  " case when convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >= '" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") + "' then 2 " + Environment.NewLine +
                  " else 0 end else 2 end ) "
            End If

            If MultipleFinderFillAuto Then
                Dim dtSeg As DataTable = clsDBFuncationality.GetDataTable("select distinct Loc_Segment_Code from TSPL_LOCATION_MASTER where location_code in (" + clsCommon.GetMulcallString(mfndMcc.arrValueMember) + ")")
                If dtSeg IsNot Nothing AndAlso dtSeg.Rows.Count > 0 Then
                    Dim ArrSeg As New ArrayList
                    For Each drSeg As DataRow In dtSeg.Rows
                        ArrSeg.Add(drSeg("Loc_Segment_Code"))
                    Next
                    whrCls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in ( " + clsCommon.GetMulcallString(ArrSeg) + ") "
                Else
                    whrCls += " and 2=3 "
                End If
            Else
                whrCls += "  and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in ( '" & fndLoc.Value & "' ) "
            End If
            qry = qry & whrCls & "  and   " & IIf(chkSkipPrevDeduction.Checked, " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' ")
            qry += " )xxx group by Document_No "
            If PayableAmountZeroForMCCSale Then
                qry += " order by Vendor_Code,Sequence_No,Posting_Date,Sequence_No2  "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvDeduction.DataSource = Nothing
                gvDeduction.AutoGenerateColumns = False
                gvDeduction.DataSource = dt

                gvDeduction.Columns(colSlno).FieldName = "SNo"
                gvDeduction.Columns(colSelect).FieldName = "Sel"
                gvDeduction.Columns(colAPInvoiceNo).FieldName = "Document_No"
                gvDeduction.Columns(colAPInvoiceDate).FieldName = "Invoice_Entry_Date"
                gvDeduction.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                gvDeduction.Columns(colVendorCode).FieldName = "Vendor_Code"
                gvDeduction.Columns(colVendorDesc).FieldName = "Vendor_Name"
                gvDeduction.Columns(colDeductionCode).FieldName = "DeductionCode"
                gvDeduction.Columns(colDeductionDesc).FieldName = "Deduction_Desc"
                gvDeduction.Columns(colItemAmt).FieldName = "Total_Amount"
            End If
        End If
    End Sub

    Sub LoadCreditNoteGridData()
        LoadBlankGridCreditNote()
        If clsCommon.myLen(strVendorCode) > 0 Then
            '' Update By pankaj jha For picking up amount from Head in place of details 
            Dim qry As String = "   select cast(1 as bit) as Sel,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,ROW_NUMBER() over(order by TSPL_VENDOR_INVOICE_HEAD.Document_No) as SNo, TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_REMITTANCE.Calculated_TDS,TSPL_VENDOR_INVOICE_HEAD.document_total   as Total_Amount   
from TSPL_VENDOR_INVOICE_head   
left outer join TSPL_REMITTANCE on TSPL_REMITTANCE.Document_No=TSPL_VENDOR_INVOICE_head.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where   TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt>0 and coalesce(refDocType,'') not in ('Milk_HE','Milk_OW','V_I_Issue_Return','COM-INC')  " ''UDL/03/07/18-000201 by balwinder on 09/07/2018  change TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 to TSPL_VENDOR_INVOICE_HEAD.Balance_Amt>0;ERO/14/08/19-000992 by balwinder on 14/08/2019
            Dim whrCls As String = " and not exists(select 1 from TSPL_PAYMENT_PROCESS_CREDIT_NOTE  where TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no not in ('" + fndDocNo.Value + "')) "

            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  in ( " & strVendorCode & ")   and  coalesce(Posting_Date,'')<>''"
            End If

            If MultipleFinderFillAuto Then
                Dim dtSeg As DataTable = clsDBFuncationality.GetDataTable("select distinct Loc_Segment_Code from TSPL_LOCATION_MASTER where location_code in (" + clsCommon.GetMulcallString(mfndMcc.arrValueMember) + ")")
                If dtSeg IsNot Nothing AndAlso dtSeg.Rows.Count > 0 Then
                    Dim ArrSeg As New ArrayList
                    For Each drSeg As DataRow In dtSeg.Rows
                        ArrSeg.Add(drSeg("Loc_Segment_Code"))
                    Next
                    whrCls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in ( " + clsCommon.GetMulcallString(ArrSeg) + ") "
                Else
                    whrCls += " and 2=3 "
                End If
            Else
                whrCls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code  in ( '" & fndLoc.Value & "' ) "
            End If

            qry = qry & whrCls & " and isnull(TSPL_VENDOR_INVOICE_HEAD.Saving,0)=0  and " & IIf(chkSkipPrevCreditNote.Checked, " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' ")
            qry = "select * from (" + qry + ")xx"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvCreditNote.DataSource = Nothing
                gvCreditNote.AutoGenerateColumns = False
                gvCreditNote.DataSource = dt

                gvCreditNote.Columns(colSlno).FieldName = "SNo"
                gvCreditNote.Columns(colSelect).FieldName = "Sel"
                gvCreditNote.Columns(colAPInvoiceNo).FieldName = "Document_No"
                gvCreditNote.Columns(colAPInvoiceDate).FieldName = "Invoice_Entry_Date"
                gvCreditNote.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                gvCreditNote.Columns(colVendorCode).FieldName = "Vendor_Code"
                gvCreditNote.Columns(colVendorDesc).FieldName = "Vendor_Name"
                gvCreditNote.Columns(colTDSAmt).FieldName = "Calculated_TDS"
                gvCreditNote.Columns(colItemAmt).FieldName = "Total_Amount"
            End If
        End If
    End Sub

    Sub LoadBlankGridSaving()
        'gvSaving.Rows.Clear()
        gvSaving.Columns.Clear()
        gvSaving.DataSource = Nothing

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvSaving.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvSaving.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvSaving.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvSaving.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Type"
        colTextBox.Name = colAPInvoiceType
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvSaving.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvSaving.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvSaving.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvSaving.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvSaving.MasterTemplate.Columns.Add(colDecimal)

        gvSaving.AllowAddNewRow = False
        gvSaving.AllowDeleteRow = False
        gvSaving.ShowGroupPanel = False
        gvSaving.AllowColumnReorder = True
        gvSaving.AllowRowReorder = False
        gvSaving.EnableSorting = True
        gvSaving.EnableFiltering = True
        gvSaving.TableElement.TableHeaderHeight = 40
        gvSaving.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub LoadBlankGridCompulsory()
        'gvCompulsory.Rows.Clear()
        gvCompulsory.Columns.Clear()

        gvCompulsory.DataSource = Nothing

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvCompulsory.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvCompulsory.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvCompulsory.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvCompulsory.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Type"
        colTextBox.Name = colAPInvoiceType
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvCompulsory.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvCompulsory.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvCompulsory.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvCompulsory.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvCompulsory.MasterTemplate.Columns.Add(colDecimal)

        gvCompulsory.AllowAddNewRow = False
        gvCompulsory.AllowDeleteRow = False
        gvCompulsory.ShowGroupPanel = False
        gvCompulsory.AllowColumnReorder = True
        gvCompulsory.AllowRowReorder = False
        gvCompulsory.EnableSorting = True
        gvCompulsory.EnableFiltering = True
        gvCompulsory.TableElement.TableHeaderHeight = 40
        gvCompulsory.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub LoadSavingGridData()
        LoadBlankGridSaving()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "   select cast(1 as bit) as Sel,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,ROW_NUMBER() over(order by TSPL_VENDOR_INVOICE_HEAD.Document_No) as SNo ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_HEAD.document_total   as Total_Amount   
from TSPL_VENDOR_INVOICE_head   
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where   TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt>0 and coalesce(refDocType,'') not in ('Milk_HE','Milk_OW','V_I_Issue_Return','COM-INC')  "
            Dim whrCls As String = " and not exists(select 1 from TSPL_PAYMENT_PROCESS_SAVING  where TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_PAYMENT_PROCESS_SAVING.doc_no not in ('" + fndDocNo.Value + "')) "
            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  in ( " & strVendorCode & ")   and  coalesce(Posting_Date,'')<>''"
            End If
            If MultipleFinderFillAuto Then
                Dim dtSeg As DataTable = clsDBFuncationality.GetDataTable(" select distinct Loc_Segment_Code from TSPL_LOCATION_MASTER where location_code in (" + clsCommon.GetMulcallString(mfndMcc.arrValueMember) + ") ")
                If dtSeg IsNot Nothing AndAlso dtSeg.Rows.Count > 0 Then
                    Dim ArrSeg As New ArrayList
                    For Each drSeg As DataRow In dtSeg.Rows
                        ArrSeg.Add(drSeg("Loc_Segment_Code"))
                    Next
                    whrCls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in ( " + clsCommon.GetMulcallString(ArrSeg) + ") "
                Else
                    whrCls += " and 2=3 "
                End If
            Else
                whrCls += " and  TSPL_VENDOR_INVOICE_HEAD.Loc_Code  in ( '" + fndLoc.Value + "' ) "
            End If
            qry = qry & whrCls & " and isnull(TSPL_VENDOR_INVOICE_HEAD.Saving,0)=1 and " & IIf(chkSkipPrevCreditNote.Checked, " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' ")
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvSaving.DataSource = Nothing
                gvSaving.AutoGenerateColumns = False
                gvSaving.DataSource = dt
                gvSaving.Columns(colSlno).FieldName = "SNo"
                gvSaving.Columns(colSelect).FieldName = "Sel"
                gvSaving.Columns(colAPInvoiceNo).FieldName = "Document_No"
                gvSaving.Columns(colAPInvoiceDate).FieldName = "Invoice_Entry_Date"
                gvSaving.Columns(colAPInvoiceType).FieldName = "Document_Type"
                gvSaving.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                gvSaving.Columns(colVendorCode).FieldName = "Vendor_Code"
                gvSaving.Columns(colVendorDesc).FieldName = "Vendor_Name"
                gvSaving.Columns(colItemAmt).FieldName = "Total_Amount"
            End If
        End If
    End Sub

    Sub LoadCompulsoryGridData()
        LoadBlankGridCompulsory()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "   select cast(1 as bit) as Sel,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,ROW_NUMBER() over(order by TSPL_VENDOR_INVOICE_HEAD.Document_No) as SNo ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_HEAD.document_total   as Total_Amount   
from TSPL_VENDOR_INVOICE_head   
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt>0 and coalesce(refDocType,'') not in ('Milk_HE','Milk_OW','V_I_Issue_Return','COM-INC')  "
            Dim whrCls As String = " and not exists(select 1 from TSPL_PAYMENT_PROCESS_COMPULSORY  where TSPL_PAYMENT_PROCESS_COMPULSORY.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_PAYMENT_PROCESS_COMPULSORY.doc_no not in ('" + fndDocNo.Value + "')) "
            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  in ( " & strVendorCode & ")   and  coalesce(Posting_Date,'')<>''"
            End If
            If MultipleFinderFillAuto Then
                Dim dtSeg As DataTable = clsDBFuncationality.GetDataTable("select distinct Loc_Segment_Code from TSPL_LOCATION_MASTER where location_code in (" + clsCommon.GetMulcallString(mfndMcc.arrValueMember) + ") ")
                If dtSeg IsNot Nothing AndAlso dtSeg.Rows.Count > 0 Then
                    Dim ArrSeg As New ArrayList
                    For Each drSeg As DataRow In dtSeg.Rows
                        ArrSeg.Add(drSeg("Loc_Segment_Code"))
                    Next
                    whrCls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in ( " + clsCommon.GetMulcallString(ArrSeg) + ") "
                Else
                    whrCls += " and 2=3 "
                End If
            Else
                whrCls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code  in ( '" + fndLoc.Value + "' ) "
            End If
            qry = qry & whrCls & " and isnull(TSPL_VENDOR_INVOICE_HEAD.Saving,0)=2  and " & IIf(chkSkipPrevCreditNote.Checked, " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' ")

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvCompulsory.DataSource = Nothing
                gvCompulsory.AutoGenerateColumns = False
                gvCompulsory.DataSource = dt
                gvCompulsory.Columns(colSlno).FieldName = "SNo"
                gvCompulsory.Columns(colSelect).FieldName = "Sel"
                gvCompulsory.Columns(colAPInvoiceNo).FieldName = "Document_No"
                gvCompulsory.Columns(colAPInvoiceDate).FieldName = "Invoice_Entry_Date"
                gvCompulsory.Columns(colAPInvoiceType).FieldName = "Document_Type"
                gvCompulsory.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                gvCompulsory.Columns(colVendorCode).FieldName = "Vendor_Code"
                gvCompulsory.Columns(colVendorDesc).FieldName = "Vendor_Name"
                gvCompulsory.Columns(colItemAmt).FieldName = "Total_Amount"
            End If
        End If
    End Sub
    Sub LoadAdvancePaymentGridData()
        If isConsiderAdvancePayment Then
            LoadBlankGridAdvancePayment()
            If clsCommon.myLen(strVendorCode) > 0 Then
                Dim qry As String = "select cast(1 as bit) as Sel,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,ROW_NUMBER() over(order by x.Payment_No) as SNo ,X.*,(case when No_Of_EMI=0 then 0 else (case when Balance_Amt<(Payment_Amount/No_Of_EMI) then Balance_Amt else (Payment_Amount/No_Of_EMI) end  ) end) as InstallmentAmt  from ( " +
                clsAPInvoiceAdvanceInterest.GetAdvancePaymentQry(strVendorCode, dtpFromDate.Value, dtpToDate.Value, chkSkipPreviousDocumentOfAdvancePayment.Checked, Nothing, False) +
                " ) X 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=X.Vendor_Code
order by Payment_Date "

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gvAdvancePayment.DataSource = Nothing
                    gvAdvancePayment.AutoGenerateColumns = False
                    gvAdvancePayment.DataSource = dt

                    gvAdvancePayment.Columns(colAPSelect).FieldName = "Sel"
                    gvAdvancePayment.Columns(colAPSNo).FieldName = "SNo"
                    gvAdvancePayment.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                    gvAdvancePayment.Columns(colAPVendorCode).FieldName = "Vendor_Code"
                    gvAdvancePayment.Columns(colAPVendorName).FieldName = "Vendor_Name"
                    gvAdvancePayment.Columns(colAPPaymentCode).FieldName = "Payment_No"
                    gvAdvancePayment.Columns(colAPPaymentDate).FieldName = "Payment_Date"
                    gvAdvancePayment.Columns(colAPPaymentAmt).FieldName = "Payment_Amount"
                    gvAdvancePayment.Columns(colAPPaymentAmtBalance).FieldName = "Balance_Amt"
                    gvAdvancePayment.Columns(colAPNoOfInstallment).FieldName = "No_Of_EMI"
                    gvAdvancePayment.Columns(colAPInstallmentAmt).FieldName = "InstallmentAmt"
                End If
            End If
        End If
    End Sub


    Sub LoadAssetLostGridData()
        If True Then
            LoadBlankGridAssetLost()
            If clsCommon.myLen(strVendorCode) > 0 Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(clsPaymentHeader.GetAssetLostPaymentQry(strVendorCode, dtpFromDate.Value, dtpToDate.Value, chkSkipPreviousDocumentOfAssetLost.Checked, Nothing))
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gvAssetLost.DataSource = Nothing
                    gvAssetLost.AutoGenerateColumns = False
                    gvAssetLost.DataSource = dt
                    gvAssetLost.Columns(colALSelect).FieldName = "Sel"
                    gvAssetLost.Columns(colALSNo).FieldName = "SNo"
                    gvAssetLost.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                    gvAssetLost.Columns(colALVendorCode).FieldName = "Vendor_Code"
                    gvAssetLost.Columns(colALVendorName).FieldName = "Vendor_Name"
                    gvAssetLost.Columns(colALPaymentCode).FieldName = "Payment_No"
                    gvAssetLost.Columns(colALPaymentDate).FieldName = "Payment_Date"
                    gvAssetLost.Columns(colALPaymentAmt).FieldName = "Payment_Amount"
                End If
            End If
        End If
    End Sub

    Sub LoadMccSaleGridData()
        LoadBlankGridMccSale()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "select cast(1 as bit) as Sel,ROW_NUMBER() over(order by xx.[Shipment_No]) as SNo,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, Loc_Code,[Shipment_No],[Shipment_Date] ,[Vendor_Code] ,[Vendor_Name] ,Sale_Invoice_No,[Sale_Inoivce_Date] ,[AR_Invoice_No],[AR_Invoice_Date],Balance_Amt as OriginalBalanceAmt,InstallmentAmount,case when InstallmentAmount>0 then case when Balance_Amt>(InstallmentAmount+1.00) then InstallmentAmount else Balance_Amt end else Balance_Amt end as Balance_Amt from (  
select TSPL_Customer_Invoice_Head.Loc_Code, TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE as [Shipment_No],TSPL_SD_SHIPMENT_HEAD.Document_Date as [Shipment_Date] ,TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code [Vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name as [Vendor_Name] , TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Sale_Inoivce_Date] ,TSPL_Customer_Invoice_Head.Document_No as [AR_Invoice_No] ,   TSPL_Customer_Invoice_Head.Document_Date as [AR_Invoice_Date], TSPL_Customer_Invoice_Head.Balance_Amt , 
convert(decimal(18,2), case when isnull(TSPL_SD_SALE_INVOICE_HEAD.No_Of_Instalment,0)=0 then 0 else TSPL_SD_SALE_INVOICE_HEAD.Total_Amt/TSPL_SD_SALE_INVOICE_HEAD.No_Of_Instalment " + IIf(IsRoundOffPaiseAmount, "-((TSPL_SD_SALE_INVOICE_HEAD.Total_Amt/TSPL_SD_SALE_INVOICE_HEAD.No_Of_Instalment)%1)", "") + "  end) as InstallmentAmount  
from TSPL_SD_SHIPMENT_HEAD  
left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code     
inner join TSPL_Customer_Invoice_Head on  TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No  and coalesce(TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>''   
left outer join TSPL_VENDOR_MASTER   on  TSPL_VENDOR_MASTER .Vendor_code  =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code   
left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No    
where  isnull(TSPL_SD_SHIPMENT_HEAD.Is_CashSale,'N')='N' and TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC' and " & IIf(chkSkipPrevMccSale.Checked, " convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") + " and tspl_customer_invoice_head.Balance_Amt<>0 "
            If clsCommon.myLen(strVendorCode) > 0 Then
                qry += " and TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code  in (  " & strVendorCode & " )"
            End If
            qry += " )xx left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=xx.Vendor_Code "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvMccSale.DataSource = Nothing
                gvMccSale.AutoGenerateColumns = False
                gvMccSale.DataSource = dt
                gvMccSale.Columns(colSlno).FieldName = "SNo"
                gvMccSale.Columns(colSelect).FieldName = "Sel"
                gvMccSale.Columns(colShipmentNo).FieldName = "Shipment_No"
                gvMccSale.Columns(colShipmentDate).FieldName = "Shipment_Date"
                gvMccSale.Columns(colSaleInvNo).FieldName = "Sale_Invoice_No"
                gvMccSale.Columns(colSaleInvDate).FieldName = "Sale_Inoivce_Date"
                gvMccSale.Columns(colARInvoiceNo).FieldName = "AR_Invoice_No"
                gvMccSale.Columns(colARInvoiceDate).FieldName = "AR_Invoice_Date"
                gvMccSale.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                gvMccSale.Columns(colCustomerCode).FieldName = "Vendor_Code"
                gvMccSale.Columns(colCustomerName).FieldName = "Vendor_Name"
                gvMccSale.Columns(colItemAmt).FieldName = "Balance_Amt"
                gvMccSale.Columns(colOrgBalanceAmt).FieldName = "OriginalBalanceAmt"
                gvMccSale.Columns(colInstallmentAmt).FieldName = "InstallmentAmount"
            End If
        End If
    End Sub

    Sub LoadMccSaleReturnGridData()
        LoadBlankGridMccSaleReturn()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "   select  cast(1 as bit) as Sel,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,ROW_NUMBER() over(order by TSPL_SD_SALE_RETURN_HEAD.Document_Code) as SNo,TSPL_SD_SALE_RETURN_HEAD.Document_Code,case when coalesce(TSPL_SD_SALE_RETURN_HEAD.Return_Type,'')='D' then 'Damaged Goods' when coalesce(TSPL_SD_SALE_RETURN_HEAD.Return_Type,'')='P' then 'Price Only' when coalesce(TSPL_SD_SALE_RETURN_HEAD.Return_Type,'')='I' then 'Inventory Type' end as Document_Type,TSPL_SD_SALE_RETURN_HEAD.Document_Date,TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE as [Shipment_No]
,TSPL_SD_SHIPMENT_HEAD.Document_Date as [Shipment_Date] ,TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code [Vendor_Code] ,
TSPL_VENDOR_MASTER.Vendor_Name as [Vendor_Name] , TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as 
[Sale_Inoivce_Date] ,TSPL_Customer_Invoice_Head.Document_No as [AR_Invoice_No] ,   TSPL_Customer_Invoice_Head.Document_Date as 
[AR_Invoice_Date], TSPL_Customer_Invoice_Head.Balance_Amt   
from TSPL_SD_SALE_RETURN_HEAD 
left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No  
left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on  TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code     
inner join TSPL_Customer_Invoice_Head on   TSPL_Customer_Invoice_Head.against_Mcc_Material_Sale_return=TSPL_SD_SALE_RETURN_HEAD.Document_Code  and coalesce(TSPL_Customer_Invoice_Head.against_Mcc_Material_Sale_return,'')<>''  
left outer join TSPL_VENDOR_MASTER   on  TSPL_VENDOR_MASTER .Vendor_code  =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code   
left outer join  TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No   
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code
where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC' 
and " & IIf(ChkSkipMccSaleReturn.Checked, " convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and 
'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <= 
'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") & "   and 
tspl_customer_invoice_head.Balance_Amt<>0 "
            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                qry += " and TSPL_SD_SALE_RETURN_HEAD.Customer_code  in (  " & strVendorCode & " )"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                GvMccSaleReturn.DataSource = Nothing
                GvMccSaleReturn.AutoGenerateColumns = False
                GvMccSaleReturn.DataSource = dt

                GvMccSaleReturn.Columns(colSlno).FieldName = "SNo"
                GvMccSaleReturn.Columns(colSelect).FieldName = "Sel"
                GvMccSaleReturn.Columns(colReturnDocNo).FieldName = "Document_Code"
                GvMccSaleReturn.Columns(colReturnDocDate).FieldName = "Document_Date"
                GvMccSaleReturn.Columns(colReturnDocType).FieldName = "Document_Type"
                GvMccSaleReturn.Columns(colShipmentNo).FieldName = "Shipment_No"
                GvMccSaleReturn.Columns(colShipmentDate).FieldName = "Shipment_Date"
                GvMccSaleReturn.Columns(colSaleInvNo).FieldName = "Sale_Invoice_No"
                GvMccSaleReturn.Columns(colSaleInvDate).FieldName = "Sale_Inoivce_Date"
                GvMccSaleReturn.Columns(colARInvoiceNo).FieldName = "AR_Invoice_No"
                GvMccSaleReturn.Columns(colARInvoiceDate).FieldName = "AR_Invoice_Date"
                GvMccSaleReturn.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                GvMccSaleReturn.Columns(colCustomerCode).FieldName = "Vendor_Code"
                GvMccSaleReturn.Columns(colCustomerName).FieldName = "Vendor_Name"
                GvMccSaleReturn.Columns(colItemAmt).FieldName = "Balance_Amt"
            End If
        End If
    End Sub

    Sub LoadBlankGridItemIssue()
        'gvItemIssue.Rows.Clear()
        gvItemIssue.Columns.Clear()

        gvItemIssue.DataSource = Nothing

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select"
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
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
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
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
        colDecimal.ReadOnly = False
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
        'gvItemIssueReturn.Rows.Clear()
        gvItemIssueReturn.Columns.Clear()

        gvItemIssueReturn.DataSource = Nothing

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
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
        gvItemIssueReturn.ReadOnly = True
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
        LoadBlankGridItemIssue()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim strMCCcode = ""
            If MultipleFinderFillAuto Then
                strMCCcode = " " + clsCommon.GetMulcallString(mfndMcc.arrValueMember) + "  "
            Else
                strMCCcode = " select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code   ='" & fndLoc.Value & "'"
            End If
            Dim qry As String = "Select  cast(1 as bit) as Sel,ROW_NUMBER() over(order by TSPL_VSPItem_HEAD.Doc_No) as SNo,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ,TSPL_VSPItem_HEAD.Doc_No  as [Item_Issue_Doc_No],TSPL_VSPItem_HEAD.Doc_Date  as [Item_Issue_Doc_Date] ,TSPL_VSPItem_HEAD.From_Location , 
TSPL_VSPItem_HEAD.Issue_To as [vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_INVOICE_HEAD.Document_No as [AP_Invoice_No] , 
TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  as [AP_Invoice_Date],TSPL_VENDOR_INVOICE_HEAD.Balance_Amt   
from TSPL_VENDOR_INVOICE_HEAD  
inner join TSPL_VSPItem_HEAD on TSPL_VSPItem_HEAD .Doc_No=TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No    
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSPItem_HEAD.Issue_To  
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VSPItem_HEAD.Issue_To
where TSPL_VSPItem_HEAD.Doc_Type='Issue' and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 and " & IIf(chkSkipPrevItemIssue.Checked, " convert(date,TSPL_VSPItem_HEAD.Doc_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_VSPItem_HEAD.Doc_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") & " 
and TSPL_VSPItem_HEAD.From_Location in  ( " + strMCCcode + " )  "
            Dim whrCls As String = ""
            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and TSPL_VSPItem_HEAD.Issue_To  in ( " & strVendorCode & " )"
            End If
            qry = qry & whrCls
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvItemIssue.DataSource = Nothing
                gvItemIssue.AutoGenerateColumns = False
                gvItemIssue.DataSource = dt

                gvItemIssue.Columns(colSlno).FieldName = "SNo"
                gvItemIssue.Columns(colSelect).FieldName = "Sel"
                gvItemIssue.Columns(colVspItemIssueNo).FieldName = "Item_Issue_Doc_No"
                gvItemIssue.Columns(colVspItemIssueDate).FieldName = "Item_Issue_Doc_Date"
                gvItemIssue.Columns(colAPInvoiceNo).FieldName = "AP_Invoice_No"
                gvItemIssue.Columns(colAPInvoiceDate).FieldName = "AP_Invoice_Date"
                gvItemIssue.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                gvItemIssue.Columns(colVendorCode).FieldName = "vendor_Code"
                gvItemIssue.Columns(colVendorDesc).FieldName = "Vendor_Name"
                gvItemIssue.Columns(colItemAmt).FieldName = "Balance_Amt"
            End If
        End If
    End Sub

    Sub LoadItemIssueReturnGridData()
        LoadBlankGridItemIssueReturn()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim strMCCcode = ""
            If MultipleFinderFillAuto Then
                strMCCcode = " " + clsCommon.GetMulcallString(mfndMcc.arrValueMember) + "  "
            Else
                strMCCcode = " select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code   ='" & fndLoc.Value & "' "
            End If
            Dim qry As String = " select cast(1 as bit) as Sel,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,ROW_NUMBER() over(order by TSPL_VSPItem_HEAD.Doc_No) as SNo, TSPL_VSPItem_HEAD.Doc_No  as [Item_Issue_Return_No],Issue_No as [Item_Issue_Doc_No],TSPL_VSPItem_HEAD.Doc_Date  as [Item_Issue_Return_Date] , 
TSPL_VSPItem_HEAD.From_Location ,TSPL_VSPItem_HEAD.Issue_To as [vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_INVOICE_HEAD.Document_No as [AP_Invoice_No] , 
TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  as [AP_Invoice_Date],TSPL_VENDOR_INVOICE_HEAD.Balance_Amt   from TSPL_VENDOR_INVOICE_HEAD  
inner join TSPL_VSPItem_HEAD on TSPL_VSPItem_HEAD .Doc_No=TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No    
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSPItem_HEAD.Issue_To  
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VSPItem_HEAD.Issue_To
where TSPL_VSPItem_HEAD.Doc_Type='Return' and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 and " & IIf(chkSkipPrevItemIssueReturn.Checked, " convert(date,TSPL_VSPItem_HEAD.Doc_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_VSPItem_HEAD.Doc_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") & " 
and TSPL_VSPItem_HEAD.From_Location in  ( " + strMCCcode + " )  "
            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                qry += " and TSPL_VSPItem_HEAD.Issue_To  in ( " & strVendorCode & " )"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvItemIssueReturn.DataSource = Nothing
                gvItemIssueReturn.AutoGenerateColumns = False
                gvItemIssueReturn.DataSource = dt

                gvItemIssueReturn.Columns(colSlno).FieldName = "SNo"
                gvItemIssueReturn.Columns(colSelect).FieldName = "Sel"
                gvItemIssueReturn.Columns(colVspItemIssueReturnNo).FieldName = "Item_Issue_Return_No"
                gvItemIssueReturn.Columns(colVspItemIssueNo).FieldName = "Item_Issue_Doc_No"
                gvItemIssueReturn.Columns(colVspItemIssueDate).FieldName = "Item_Issue_Return_Date"
                gvItemIssueReturn.Columns(colAPInvoiceNo).FieldName = "AP_Invoice_No"
                gvItemIssueReturn.Columns(colAPInvoiceDate).FieldName = "AP_Invoice_Date"
                gvItemIssueReturn.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                gvItemIssueReturn.Columns(colVendorCode).FieldName = "vendor_Code"
                gvItemIssueReturn.Columns(colVendorDesc).FieldName = "Vendor_Name"
                gvItemIssueReturn.Columns(colItemAmt).FieldName = "Balance_Amt"
            End If
        End If
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
        mfndMcc.arrValueMember = Nothing
        txtLocName.Text = ""
        btnProcess.Enabled = False
        btnPost.Enabled = False
        btnDelete.Enabled = False
        lblPending.Status = ERPTransactionStatus.Pending
        lblPrePending.Status = ERPTransactionStatus.Pending
        'btnSave.Visible = False
        'btnProcess.Enabled = True
        'btnDelete.Visible = False
        txtMCC.Text = ""
        lblMCC.Text = ""
        fndArea.Value = ""
        txtPaymentCycleNo.Text = ""
        txtFiscalYear.Text = ""
        LoadBlankGridInvoice()
        LoadBlankGridItemIssue()
        LoadBlankGridItemIssueReturn()
        LoadBlankGridMccSale()
        LoadBlankGridMccSaleReturn()
        LoadBlankGridDeduction()
        LoadBlankGridCreditNote()
        LoadBlankGridSaving()
        LoadBlankGridCompulsory()
        LoadBlankGridAdvancePayment()
        LoadBlankGridAssetLost()
        LoadBlankGridGV()
        frm.desc = "Against Bulk Payment Process. "
        If MultipleFinderFillAuto Then
            fndLoc.Enabled = False
            txtLocName.Enabled = False
        Else
            fndLoc.Enabled = True
            txtLocName.Enabled = True
        End If

        dtpToDate.Enabled = True
        dtpToDate.ReadOnly = True
        dtpFromDate.Enabled = True
        isLoad = False
        GroupBox2.Visible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowSkippingPrevDocumentsOnPaymentProcess, Nothing)) = 1, True, False)

        txtVSP.arrValueMember = Nothing
        chkSkipPrevItemIssue.Checked = False
        chkSkipPrevItemIssueReturn.Checked = False
        chkSkipPrevMccSale.Checked = False
        ChkSkipMccSaleReturn.Checked = False
        chkSkipPrevCreditNote.Checked = False
        chkSkipPrevDeduction.Checked = False
        chkSkipPreviousDocumentOfAdvancePayment.Checked = False
        chkAll.Checked = False
        If MultipleFinderFillAuto Then
            FillAllMCCDefault()
            AutoFillAllVSP()
        End If
        SetTagOFCheckBox()
    End Sub

    Sub SetTagOFCheckBox()
        chkSkipPrevItemIssue.Tag = chkSkipPrevItemIssue.Checked
        chkSkipPrevItemIssueReturn.Tag = chkSkipPrevItemIssueReturn.Checked
        chkSkipPrevMccSale.Tag = chkSkipPrevMccSale.Checked
        ChkSkipMccSaleReturn.Tag = ChkSkipMccSaleReturn.Checked
        chkSkipPrevCreditNote.Tag = chkSkipPrevCreditNote.Checked
        chkSkipPrevDeduction.Tag = chkSkipPrevDeduction.Checked
        chkSkipPreviousDocumentOfAdvancePayment.Tag = chkSkipPreviousDocumentOfAdvancePayment.Checked
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmPaymentProcess)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")

            End If
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnExport.Visible = MyBase.isExport
        'RadMenu1.Visible = MyBase.isExport
        If MyBase.isPrintFlag = True Then
            btnDocPrint.Enabled = True
            btnPrint.Enabled = True
            btnDCPrint.Enabled = True
        Else
            btnDocPrint.Enabled = False
            btnPrint.Enabled = False
            btnDCPrint.Enabled = False
        End If
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        btnReverse.Visible = False
        btnUnpost.Visible = False
    End Sub

    Function AllowToSave() As Boolean

        Try
            ' KUNAL > TICKET : BM00000009575 =======
            'If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
            '    dtpDate.Focus()
            '    Return False
            'End If

            If dtpFromDate.Value > dtpToDate.Value Then
                Throw New Exception(" 'From Date' can't be larger than 'To Date'")
            End If
            If clsCommon.GetDateWithEndTime(dtpDate.Value) < clsCommon.GetDateWithStartTime(dtpToDate.Value) Then
                Throw New Exception("Payment process date can't be less than to date")
            End If
            If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
                Throw New Exception(" Please select atleast one Vendor")
            End If

            If clsCommon.myLen(fndLoc.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                Throw New Exception("Please select Location segment")
                fndLoc.Focus()
            End If
            If MultipleFinderFillAuto = True Then
                If mfndMcc.arrValueMember Is Nothing OrElse mfndMcc.arrValueMember.Count <= 0 Then
                    Throw New Exception("Please select Location")
                End If
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
                            If clsCommon.CompairString(gv.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
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
                            If clsCommon.CompairString(gv.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
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
                            If clsCommon.CompairString(gv.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
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
                            If clsCommon.CompairString(gv.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
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
                            If clsCommon.CompairString(gv.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
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
                            If clsCommon.CompairString(gv.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                                IsInvalidVendor = False
                            End If
                        End If
                    Next
                    If IsInvalidVendor = True Then
                        Throw New Exception("Credit Note : Invalid Vendor No-" & grow.Cells(colVendorCode).Value & " at Line No-" & (grow.Index + 1) & ". Please unselect it")
                    End If
                End If
            Next

            ''richa agarwal changes in 08/08/2016
            'For i As Integer = 0 To gv.Rows.Count - 1
            '    If gv.Rows(i).Cells(colSelect).Value = True Then
            '        If gv.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value < (getTotalMccSaleSum(gv.Rows(i).Cells(colVendorCode).Value) - getTotalMccSaleReduceDeduSum(gv.Rows(i).Cells(colVendorCode).Value)) Then
            '            Throw New Exception(" Please Unselect some MCC Sale documents  " & Environment.NewLine & " Sale Amount can not be more than Milk Purchase Invoice amount At line no " & (i + 1))
            '        End If
            '    End If
            'Next

            'For i As Integer = 0 To gv.Rows.Count - 1
            '    If gv.Rows(i).Cells(colSelect).Value = True Then
            '        If gv.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value < (getTotalMccSaleSum(gv.Rows(i).Cells(colVendorCode).Value) - getTotalMccSaleReturnSum(gv.Rows(i).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(i).Cells(colVendorCode).Value) - getTotalItemIssueReturnSum(gv.Rows(i).Cells(colVendorCode).Value) + getTotalDeductionReduceDeduSum(gv.Rows(i).Cells(colVendorCode).Value) - getTotalCreditNoteSum(gv.Rows(i).Cells(colVendorCode).Value) - getTotalMccSaleReduceDeduSum(gv.Rows(i).Cells(colVendorCode).Value)) Then
            '            Throw New Exception(" Please Unselect some MCC Sale documents  " & Environment.NewLine & " Sale Amount can not be more than Milk Purchase Invoice amount At line no " & (i + 1))
            '        End If
            '    End If
            'Next

            For i As Integer = 0 To gv.Rows.Count - 1
                If gv.Rows(i).Cells(colSelect).Value = True Then
                    'If gv.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value < clsCommon.myCdbl(gv.Rows(i).Cells(colPaybleAmt).Value) Then
                    '    Throw New Exception("Payable Amount cannot be more than invoice Amount At line no " & (i + 1))
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
                    ''Add Credit Note total Amount on 19/Jan/2017 By Balwinder singh Premi
                    If gv.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value < (getTotalMccSaleSum(gv.Rows(i).Cells(colVendorCode).Value) - (getTotalCreditNoteSum(gv.Rows(i).Cells(colVendorCode).Value)) - getTotalMccSaleReturnSum(gv.Rows(i).Cells(colVendorCode).Value) - getTotalMccSaleReduceDeduSum(gv.Rows(i).Cells(colVendorCode).Value)) Then
                        Throw New Exception(" Please Unselect some MCC Sale documents  " & Environment.NewLine & " Sale Amount can not be more than Milk Purchase Invoice amount At line no " & (i + 1))
                    End If
                End If
            Next

            'richa agarwal changes in 07/12/2018 BHA/06/12/18-000743
            For Each grow As GridViewRowInfo In gvAdvancePayment.Rows
                If grow.Cells(colAPSelect).Value = True Then
                    If clsCommon.myCdbl(grow.Cells(colAPInstallmentAmt).Value) > clsCommon.myCdbl(grow.Cells(colAPPaymentAmtBalance).Value) Then
                        Throw New Exception("Installment Amount should be less or equal to Balance Amount-" & grow.Cells(colAPInstallmentAmt).Value & " at Line No-" & (grow.Index + 1) & ".")
                    End If
                End If
            Next
            ''-------------------------
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        ''----------------------
        Return True
    End Function

    Sub deleteData()
        Try
            clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(fndDocNo.Value))
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Want To Delete The Doc No : " & fndDocNo.Value & " ?", "Confirm", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    If clsPaymentProcessHead.deleteData(fndDocNo.Value) Then
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
        Dim i As Integer = 0
        clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(fndDocNo.Value))
        Try
            AllowToSave()
            If isPostbtnClick AndAlso SettVSPHoldPaymentNotCompanyBank = False Then
                For i = 0 To gv.Rows.Count - 1
                    If clsCommon.myLen(gv.Rows(i).Cells(colBankCode).Value) <= 0 Then
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
                        gvInvoice.Rows(i).Cells(colBankCode).Value = frm.bankCode
                        gvInvoice.Rows(i).Cells(colBankDesc).Value = frm.bankDesc
                        gvInvoice.Rows(i).Cells(colPayMode).Value = frm.paymentMode
                        gvInvoice.Rows(i).Cells(colChequeNo).Value = ""
                    End If
                Next
                For i = 0 To gv.Rows.Count - 1
                    If clsCommon.myLen(gv.Rows(i).Cells(colBankCode).Value) <= 0 AndAlso clsCommon.myLen(frm.bankCode) > 0 Then
                        gv.Rows(i).Cells(colBankCode).Value = frm.bankCode
                        gv.Rows(i).Cells(colBankDesc).Value = frm.bankDesc
                        gv.Rows(i).Cells(colPayMode).Value = frm.paymentMode
                        If clsCommon.myCBool(gv.Rows(i).Cells(colSelect).Value) = True Then
                            If clsCommon.CompairString(gv.Rows(i).Cells(colPayMode).Value, "Cheque") = CompairStringResult.Equal Then
                                gv.Rows(i).Cells(colChequeNo).ReadOnly = False
                                gv.Rows(i).Cells(colChequeDate).ReadOnly = False

                                For j As Integer = 0 To gv.Rows.Count - 1
                                    gv.Rows(j).Cells(colBankCode).Value = frm.bankCode
                                    gv.Rows(j).Cells(colBankDesc).Value = frm.bankDesc
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
                            gv.Rows(i).Cells(colBankCode).Value = frm.bankCode
                            gv.Rows(i).Cells(colBankDesc).Value = frm.bankDesc
                            gv.Rows(i).Cells(colPayMode).Value = frm.paymentMode
                            If clsCommon.CompairString(gv.Rows(i).Cells(colPayMode).Value, "Cheque") = CompairStringResult.Equal Then
                                gv.Rows(i).Cells(colChequeNo).ReadOnly = False
                                gv.Rows(i).Cells(colChequeDate).ReadOnly = False
                                For j As Integer = 0 To gv.Rows.Count - 1
                                    gv.Rows(j).Cells(colBankCode).Value = frm.bankCode
                                    gv.Rows(j).Cells(colBankDesc).Value = frm.bankDesc
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
                isCellValueChanged = False
            End If


            Dim obj As clsPaymentProcessHead = New clsPaymentProcessHead()
            obj.Doc_No = fndDocNo.Value
            obj.DocRefNoForUploader = clsCommon.myCstr(txtNEFTUploaderREFNo.Text)
            obj.Doc_Date = clsCommon.GetPrintDate(dtpDate.Value, "dd/MMM/yyyy")
            obj.From_Date = clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy")
            obj.To_Date = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy")
            obj.Loc_Seg_Code = clsCommon.myCstr(fndLoc.Value)
            obj.Area_Location_Code = clsCommon.myCstr(fndArea.Value)

            obj.MCC_Code_Selected = txtMCC.Text
            ''richa agarwal 07-jan-2016
            If btnSave.Text = "Update" Then
                frm.desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(paymentDesc,'') as paymentDesc from tspl_payment_process_head where doc_no='" & clsCommon.myCstr(fndDocNo.Value) & "'"))
            End If
            ''-------------------
            obj.PaymentDesc = frm.desc



            obj.Is_Skip_Previous_Item_Issue = clsCommon.myCBool(chkSkipPrevItemIssue.Tag)
            obj.Is_Skip_Previous_Item_Issue_Return = clsCommon.myCBool(chkSkipPrevItemIssueReturn.Tag)
            obj.Is_Skip_Previous_MCC_Sale = clsCommon.myCBool(chkSkipPrevMccSale.Tag)
            obj.Is_Skip_Previous_MCC_Sale_Return = clsCommon.myCBool(ChkSkipMccSaleReturn.Tag)
            obj.Is_Skip_Previous_Credit_Note = clsCommon.myCBool(chkSkipPrevCreditNote.Tag)
            obj.Is_Skip_Previous_Debit_Note = clsCommon.myCBool(chkSkipPrevDeduction.Tag)
            obj.Is_Skip_Previous_Advacee_Payment = clsCommon.myCBool(chkSkipPreviousDocumentOfAdvancePayment.Tag)



            obj.ArrPPSkipDoc = New List(Of clsPaymentProcessSkipDoc)
            Dim objSkipDoc As clsPaymentProcessSkipDoc = Nothing

            If gvInvoice IsNot Nothing AndAlso gvInvoice.Rows.Count > 0 Then
                obj.arrClsPaymentProcessInvoices = New List(Of clsPaymentProcessInvoices)
                Dim objPayProInv As clsPaymentProcessInvoices = Nothing
                For i = 0 To gvInvoice.Rows.Count - 1
                    If gvInvoice.Rows(i).Cells(colSelect).Value = True Then
                        objPayProInv = New clsPaymentProcessInvoices
                        objPayProInv.SLNO = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colSlno).Value)
                        objPayProInv.Milk_Purchase_Invoice_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value)
                        objPayProInv.Milk_Purchase_Invoice_Date = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPurchaseInvoiceDate).Value)
                        objPayProInv.AP_Invoice_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value)
                        objPayProInv.AP_Invoice_Date = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colAPInvoiceDate).Value)
                        objPayProInv.VLC_CODE = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colVLCCode).Value)
                        objPayProInv.MCC_Code = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colMCCCode).Value)
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
                        objPayProInv.TDS_Amount = clsCommon.myCDecimal(gvInvoice.Rows(i).Cells(colTDSAmt).Value)
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
                        objPayProInv.Handling_Charges_Amount = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colHandlingCharges).Value)
                        objPayProInv.SRN_Net_Amount = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colSRNNetAmount).Value)
                        objPayProInv.SRN_RO_Amount = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colSRNROAmt).Value)

                        objPayProInv.MP_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMPAmount).Value)
                        objPayProInv.MP_EMP = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMPEMPAmount).Value)
                        objPayProInv.MP_Incentive = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMPIncentiveAmount).Value)
                        objPayProInv.MP_IncentiveEMP = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMPEMPIncentiveAmount).Value)
                        objPayProInv.MP_Net_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMPNetAmount).Value)
                        obj.arrClsPaymentProcessInvoices.Add(objPayProInv)
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
                        objPayProMccSale.Original_Balance_Amount = clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colOrgBalanceAmt).Value)
                        objPayProMccSale.Instalment_Amt = clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colInstallmentAmt).Value)
                        obj.arrClsPaymentProcessMccSale.Add(objPayProMccSale)
                    Else
                        objSkipDoc = New clsPaymentProcessSkipDoc()
                        objSkipDoc.Source_Doc_Type = "MCC-SALE"
                        objSkipDoc.Vendor_Code = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colCustomerCode).Value)
                        objSkipDoc.Source_Doc_No = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colSaleInvNo).Value)
                        objSkipDoc.Balance_Amount = clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colItemAmt).Value)
                        obj.ArrPPSkipDoc.Add(objSkipDoc)
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
                    Else
                        objSkipDoc = New clsPaymentProcessSkipDoc()
                        objSkipDoc.Source_Doc_Type = "MCC-SALE-RET"
                        objSkipDoc.Vendor_Code = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colCustomerCode).Value)
                        objSkipDoc.Source_Doc_No = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colReturnDocNo).Value)
                        objSkipDoc.Balance_Amount = clsCommon.myCdbl(GvMccSaleReturn.Rows(i).Cells(colItemAmt).Value)
                        obj.ArrPPSkipDoc.Add(objSkipDoc)
                    End If
                Next
            End If

            If gvItemIssue IsNot Nothing AndAlso gvItemIssue.Rows.Count > 0 Then
                obj.arrClsPaymentProcessItemIssue = New List(Of clsPaymentProcessItemIssue)
                Dim objPayProItemIssue As clsPaymentProcessItemIssue = Nothing
                For i = 0 To gvItemIssue.Rows.Count - 1
                    If gvItemIssue.Rows(i).Cells(colSelect).Value = True Then
                        objPayProItemIssue = New clsPaymentProcessItemIssue
                        objPayProItemIssue.SLNO = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colSlno).Value)
                        objPayProItemIssue.Item_Issue_Doc_No = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colVspItemIssueNo).Value)
                        objPayProItemIssue.Item_Issue_Doc_Date = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colVspItemIssueDate).Value)
                        objPayProItemIssue.AP_Invoice_No = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colAPInvoiceNo).Value)
                        objPayProItemIssue.AP_Invoice_Date = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colAPInvoiceDate).Value)
                        objPayProItemIssue.Vendor_CODE = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colVendorCode).Value)
                        objPayProItemIssue.Vendor_NAME = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colVendorDesc).Value)
                        objPayProItemIssue.Amount = clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colItemAmt).Value)
                        objPayProItemIssue.Reduce_Deduc_Amt = clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colReduceDeduc).Value)
                        obj.arrClsPaymentProcessItemIssue.Add(objPayProItemIssue)
                    Else
                        objSkipDoc = New clsPaymentProcessSkipDoc()
                        objSkipDoc.Source_Doc_Type = "VSP-ITEM-ISSUE"
                        objSkipDoc.Vendor_Code = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colVendorCode).Value)
                        objSkipDoc.Source_Doc_No = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colVspItemIssueNo).Value)
                        objSkipDoc.Balance_Amount = clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colItemAmt).Value)
                        obj.ArrPPSkipDoc.Add(objSkipDoc)
                    End If
                Next
            End If

            If gvItemIssueReturn IsNot Nothing AndAlso gvItemIssueReturn.Rows.Count > 0 Then
                obj.arrClsPaymentProcessItemIssueReturn = New List(Of clsPaymentProcessItemIssueReturn)
                Dim objPayProItemIssueReturn As clsPaymentProcessItemIssueReturn = Nothing
                For i = 0 To gvItemIssueReturn.Rows.Count - 1
                    If gvItemIssueReturn.Rows(i).Cells(colSelect).Value = True Then
                        objPayProItemIssueReturn = New clsPaymentProcessItemIssueReturn
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
                        obj.arrClsPaymentProcessItemIssueReturn.Add(objPayProItemIssueReturn)
                    Else
                        objSkipDoc = New clsPaymentProcessSkipDoc()
                        objSkipDoc.Source_Doc_Type = "VSP-ITEM-ISSUE-RETURN"
                        objSkipDoc.Vendor_Code = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colVendorCode).Value)
                        objSkipDoc.Source_Doc_No = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colVspItemIssueNo).Value)
                        objSkipDoc.Balance_Amount = clsCommon.myCdbl(gvItemIssueReturn.Rows(i).Cells(colItemAmt).Value)
                        obj.ArrPPSkipDoc.Add(objSkipDoc)
                    End If
                Next
            End If

            If gvDeduction IsNot Nothing AndAlso gvDeduction.Rows.Count > 0 Then
                obj.arrClsPaymentProcessDeductions = New List(Of clsPaymentProcessDeduction)
                Dim objPayProDeduction As clsPaymentProcessDeduction = Nothing
                For i = 0 To gvDeduction.Rows.Count - 1
                    If gvDeduction.Rows(i).Cells(colSelect).Value = True Then
                        objPayProDeduction = New clsPaymentProcessDeduction
                        objPayProDeduction.SLNO = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colSlno).Value)
                        objPayProDeduction.AP_Invoice_No = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colAPInvoiceNo).Value)
                        objPayProDeduction.AP_Invoice_Date = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colAPInvoiceDate).Value)
                        objPayProDeduction.Vendor_CODE = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colVendorCode).Value)
                        objPayProDeduction.Vendor_NAME = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colVendorDesc).Value)
                        objPayProDeduction.Ded_Code = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colDeductionCode).Value)
                        objPayProDeduction.Ded_Desc = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colDeductionDesc).Value)
                        objPayProDeduction.Amount = clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colItemAmt).Value)
                        objPayProDeduction.Reduce_Deduc_Amt = clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colReduceDeduc).Value)
                        obj.arrClsPaymentProcessDeductions.Add(objPayProDeduction)
                    Else
                        objSkipDoc = New clsPaymentProcessSkipDoc()
                        objSkipDoc.Source_Doc_Type = "DEBIT-NOTE"
                        objSkipDoc.Vendor_Code = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colVendorCode).Value)
                        objSkipDoc.Source_Doc_No = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colAPInvoiceNo).Value)
                        objSkipDoc.Balance_Amount = clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colItemAmt).Value)
                        obj.ArrPPSkipDoc.Add(objSkipDoc)
                    End If
                Next
            End If

            If gvCreditNote IsNot Nothing AndAlso gvCreditNote.Rows.Count > 0 Then
                obj.arrClsPaymentProcessCreditNote = New List(Of clsPaymentProcessCreditNote)
                Dim objPayProCreditNote As clsPaymentProcessCreditNote = Nothing
                For i = 0 To gvCreditNote.Rows.Count - 1
                    If gvCreditNote.Rows(i).Cells(colSelect).Value = True Then
                        objPayProCreditNote = New clsPaymentProcessCreditNote
                        objPayProCreditNote.SLNO = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colSlno).Value)
                        objPayProCreditNote.AP_Invoice_No = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colAPInvoiceNo).Value)
                        objPayProCreditNote.AP_Invoice_Date = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colAPInvoiceDate).Value)
                        objPayProCreditNote.Vendor_CODE = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colVendorCode).Value)
                        objPayProCreditNote.Vendor_NAME = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colVendorDesc).Value)
                        objPayProCreditNote.TDS_Amount = clsCommon.myCDecimal(gvCreditNote.Rows(i).Cells(colTDSAmt).Value)
                        objPayProCreditNote.Amount = clsCommon.myCDecimal(gvCreditNote.Rows(i).Cells(colItemAmt).Value)
                        obj.arrClsPaymentProcessCreditNote.Add(objPayProCreditNote)
                    Else
                        objSkipDoc = New clsPaymentProcessSkipDoc()
                        objSkipDoc.Source_Doc_Type = "CREDIT-NOTE"
                        objSkipDoc.Vendor_Code = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colVendorCode).Value)
                        objSkipDoc.Source_Doc_No = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colAPInvoiceNo).Value)
                        objSkipDoc.Balance_Amount = clsCommon.myCdbl(gvCreditNote.Rows(i).Cells(colItemAmt).Value)
                        obj.ArrPPSkipDoc.Add(objSkipDoc)
                    End If
                Next
            End If

            If gvSaving IsNot Nothing AndAlso gvSaving.Rows.Count > 0 Then
                obj.arrclsPaymentProcessSaving = New List(Of clsPaymentProcessSaving)
                Dim objPayProCreditNote As clsPaymentProcessSaving = Nothing
                For i = 0 To gvSaving.Rows.Count - 1
                    If gvSaving.Rows(i).Cells(colSelect).Value = True Then
                        objPayProCreditNote = New clsPaymentProcessSaving
                        objPayProCreditNote.SLNO = clsCommon.myCstr(gvSaving.Rows(i).Cells(colSlno).Value)
                        objPayProCreditNote.AP_Invoice_No = clsCommon.myCstr(gvSaving.Rows(i).Cells(colAPInvoiceNo).Value)
                        obj.arrclsPaymentProcessSaving.Add(objPayProCreditNote)
                    Else
                        objSkipDoc = New clsPaymentProcessSkipDoc()
                        objSkipDoc.Source_Doc_Type = "SAVING"
                        objSkipDoc.Vendor_Code = clsCommon.myCstr(gvSaving.Rows(i).Cells(colVendorCode).Value)
                        objSkipDoc.Source_Doc_No = clsCommon.myCstr(gvSaving.Rows(i).Cells(colAPInvoiceNo).Value)
                        objSkipDoc.Balance_Amount = clsCommon.myCdbl(gvSaving.Rows(i).Cells(colItemAmt).Value)
                        obj.ArrPPSkipDoc.Add(objSkipDoc)
                    End If
                Next
            End If

            If gvCompulsory IsNot Nothing AndAlso gvCompulsory.Rows.Count > 0 Then
                obj.arrclsPaymentProcessCompulsory = New List(Of clsPaymentProcessCompulsory)
                Dim objPayProCreditNote As clsPaymentProcessCompulsory = Nothing
                For i = 0 To gvCompulsory.Rows.Count - 1
                    If gvCompulsory.Rows(i).Cells(colSelect).Value = True Then
                        objPayProCreditNote = New clsPaymentProcessCompulsory
                        objPayProCreditNote.SLNO = clsCommon.myCstr(gvCompulsory.Rows(i).Cells(colSlno).Value)
                        objPayProCreditNote.AP_Invoice_No = clsCommon.myCstr(gvCompulsory.Rows(i).Cells(colAPInvoiceNo).Value)
                        obj.arrclsPaymentProcessCompulsory.Add(objPayProCreditNote)
                    Else
                        objSkipDoc = New clsPaymentProcessSkipDoc()
                        objSkipDoc.Source_Doc_Type = "SAVING"
                        objSkipDoc.Vendor_Code = clsCommon.myCstr(gvCompulsory.Rows(i).Cells(colVendorCode).Value)
                        objSkipDoc.Source_Doc_No = clsCommon.myCstr(gvCompulsory.Rows(i).Cells(colAPInvoiceNo).Value)
                        objSkipDoc.Balance_Amount = clsCommon.myCdbl(gvCompulsory.Rows(i).Cells(colItemAmt).Value)
                        obj.ArrPPSkipDoc.Add(objSkipDoc)
                    End If
                Next
            End If

            If gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
                obj.ArrPPDetail = New List(Of clsPaymentProcessDetail)
                Dim objPPDetail As clsPaymentProcessDetail = Nothing
                For i = 0 To gv.Rows.Count - 1
                    If clsCommon.myCBool(gv.Rows(i).Cells(colSelect).Value) Then
                        objPPDetail = New clsPaymentProcessDetail()
                        objPPDetail.Is_select = clsCommon.myCBool(gv.Rows(i).Cells(colSelect).Value)
                        objPPDetail.is_Hold_Payment_Process = clsCommon.myCBool(gv.Rows(i).Cells(colIsPaymentProcessHold).Value)

                        objPPDetail.is_Hold_Payment_Process_Saving = clsCommon.myCBool(gv.Rows(i).Cells(colIsPaymentProcessHoldSaving).Value)
                        objPPDetail.is_Hold_Payment_Process_Saving_Auto = clsCommon.myCBool(gv.Rows(i).Cells(colIsPaymentProcessHoldSavingAuto).Value)
                        objPPDetail.is_Hold_Payment_Process_Saving_Manual = clsCommon.myCBool(gv.Rows(i).Cells(colIsPaymentProcessHoldSavingManual).Value)

                        objPPDetail.SNo = clsCommon.myCstr(gv.Rows(i).Cells(colSlno).Value)
                        objPPDetail.Milk_Purchase_Invoice_No = clsCommon.myCstr(gv.Rows(i).Cells(colPurchaseInvoiceNo).Value)
                        objPPDetail.Milk_Purchase_Invoice_Date = clsCommon.myCDate(gv.Rows(i).Cells(colPurchaseInvoiceDate).Value)
                        objPPDetail.AP_Invoice_No = clsCommon.myCstr(gv.Rows(i).Cells(colAPInvoiceNo).Value)
                        objPPDetail.AP_Invoice_Date = clsCommon.myCDate(gv.Rows(i).Cells(colAPInvoiceDate).Value)
                        objPPDetail.VLC_CODE_Uploader = clsCommon.myCstr(gv.Rows(i).Cells(colVLCUploaderCode).Value)
                        objPPDetail.VLC_Name = clsCommon.myCstr(gv.Rows(i).Cells(colVLCName).Value)
                        objPPDetail.MCC_Code = clsCommon.myCstr(gv.Rows(i).Cells(colMCCCode).Value)
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


                        objPPDetail.Bank_Code_Saving = clsCommon.myCstr(gv.Rows(i).Cells(colBankCodeSaving).Value)
                        objPPDetail.Bank_Desc_Saving = clsCommon.myCstr(gv.Rows(i).Cells(colBankDescSaving).Value)
                        objPPDetail.Payment_Mode_Saving = clsCommon.myCstr(gv.Rows(i).Cells(colPayModeSaving).Value)

                        If clsCommon.CompairString(objPPDetail.Payment_Mode, "Cheque") = CompairStringResult.Equal Then
                            objPPDetail.Cheque_Dated = clsCommon.myCstr(gv.Rows(i).Cells(colChequeDate).Value)
                        Else
                            objPPDetail.Cheque_Dated = Nothing
                        End If
                        objPPDetail.Handling_Charges_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colHandlingCharges).Value)
                        objPPDetail.SRN_RO_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colSRNROAmt).Value)
                        objPPDetail.SRN_Net_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colSRNNetAmount).Value)
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
                        objPPDetail.TDS_Amount = clsCommon.myCDecimal(gv.Rows(i).Cells(colTDSAmt).Value)
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
                        objPPDetail.Asset_Lost_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colAssetLostAmount).Value)
                        objPPDetail.Credit_Note_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colTotalCreditNoteAmount).Value)
                        objPPDetail.Saving_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colTotalSavingAmount).Value)
                        objPPDetail.Compulsory_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colTotalCompulsoryAmount).Value)

                        objPPDetail.Payable_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colPaybleAmt).Value)
                        objPPDetail.Service_Charge_Amt = clsCommon.myCdbl(gv.Rows(i).Cells(colServiceChargeAmt).Value)

                        objPPDetail.Advance_Payment_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colAdvanceAmount).Value)
                        objPPDetail.Advance_Payment_Amount_Knock_Off = clsCommon.myCdbl(gv.Rows(i).Cells(colAdvanceKnockOffAmount).Value)
                        obj.ArrPPDetail.Add(objPPDetail)
                    Else
                        objSkipDoc = New clsPaymentProcessSkipDoc()
                        objSkipDoc.Source_Doc_Type = "MILK-PUR-INVOICE"
                        objSkipDoc.Vendor_Code = clsCommon.myCstr(gv.Rows(i).Cells(colVendorCode).Value)
                        objSkipDoc.Source_Doc_No = clsCommon.myCstr(gv.Rows(i).Cells(colPurchaseInvoiceNo).Value)
                        objSkipDoc.Balance_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colPaybleAmt).Value)
                        obj.ArrPPSkipDoc.Add(objSkipDoc)
                    End If
                Next
            End If

            If gvAdvancePayment IsNot Nothing AndAlso gvAdvancePayment.Rows.Count > 0 Then
                obj.ArrPPAdvancePayment = New List(Of clsPaymentProcessAdvancePayment)
                Dim objPPAdvancePayment As clsPaymentProcessAdvancePayment = Nothing
                For i = 0 To gvAdvancePayment.Rows.Count - 1
                    If clsCommon.myCBool(gvAdvancePayment.Rows(i).Cells(colAPSelect).Value) Then
                        objPPAdvancePayment = New clsPaymentProcessAdvancePayment
                        objPPAdvancePayment.SNo = clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPSNo).Value)
                        objPPAdvancePayment.Vendor_Code = clsCommon.myCstr(gvAdvancePayment.Rows(i).Cells(colAPVendorCode).Value)
                        objPPAdvancePayment.Payment_No = clsCommon.myCstr(gvAdvancePayment.Rows(i).Cells(colAPPaymentCode).Value)
                        objPPAdvancePayment.Payment_Amount = clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPPaymentAmt).Value)
                        objPPAdvancePayment.Installment_Amount = clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPInstallmentAmt).Value)
                        objPPAdvancePayment.Payment_Balance = clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPPaymentAmtBalance).Value)
                        obj.ArrPPAdvancePayment.Add(objPPAdvancePayment)
                    Else
                        objSkipDoc = New clsPaymentProcessSkipDoc()
                        objSkipDoc.Source_Doc_Type = "ADVANCE"
                        objSkipDoc.Vendor_Code = clsCommon.myCstr(gvAdvancePayment.Rows(i).Cells(colAPVendorCode).Value)
                        objSkipDoc.Source_Doc_No = clsCommon.myCstr(gvAdvancePayment.Rows(i).Cells(colAPPaymentCode).Value)
                        objSkipDoc.Balance_Amount = clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPPaymentAmtBalance).Value)
                        obj.ArrPPSkipDoc.Add(objSkipDoc)
                    End If
                Next
            End If


            If gvAssetLost IsNot Nothing AndAlso gvAssetLost.Rows.Count > 0 Then
                obj.ArrPPAssetLost = New List(Of clsPaymentProcessAssetLost)
                Dim objPPAssetLost As clsPaymentProcessAssetLost = Nothing
                For i = 0 To gvAssetLost.Rows.Count - 1
                    If clsCommon.myCBool(gvAssetLost.Rows(i).Cells(colALSelect).Value) Then
                        objPPAssetLost = New clsPaymentProcessAssetLost
                        objPPAssetLost.SNo = clsCommon.myCdbl(gvAssetLost.Rows(i).Cells(colALSNo).Value)
                        objPPAssetLost.Vendor_Code = clsCommon.myCstr(gvAssetLost.Rows(i).Cells(colALVendorCode).Value)
                        objPPAssetLost.Payment_No = clsCommon.myCstr(gvAssetLost.Rows(i).Cells(colALPaymentCode).Value)
                        objPPAssetLost.Payment_Amount = clsCommon.myCdbl(gvAssetLost.Rows(i).Cells(colALPaymentAmt).Value)
                        obj.ArrPPAssetLost.Add(objPPAssetLost)
                    Else
                        objSkipDoc = New clsPaymentProcessSkipDoc()
                        objSkipDoc.Source_Doc_Type = "Asset Lost"
                        objSkipDoc.Vendor_Code = clsCommon.myCstr(gvAssetLost.Rows(i).Cells(colALVendorCode).Value)
                        objSkipDoc.Source_Doc_No = clsCommon.myCstr(gvAssetLost.Rows(i).Cells(colALPaymentCode).Value)
                        objSkipDoc.Balance_Amount = clsCommon.myCdbl(gvAssetLost.Rows(i).Cells(colALPaymentAmt).Value)
                        obj.ArrPPSkipDoc.Add(objSkipDoc)
                    End If
                Next
            End If
            Dim objApproval As New clsApply_Approval()
            If clsPaymentProcessHead.SaveData(obj, isNewEntry) Then
                Dim qry As String = "select sum(Payable_Amount) from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + fndDocNo.Value + "' "
                Dim PaybleAmt As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
                clsApply_Approval.CheckApprovalRequired(MyBase.Form_ID, obj.Doc_No, obj.Doc_Date, "", "", clsCommon.myCdbl(PaybleAmt), 0, "", objApproval)
                If Not isPostbtnClick Then
                    If isNewEntry Then
                        myMessages.insert()
                    Else
                        myMessages.update()
                    End If
                End If
                LoadData(obj.Doc_No, NavigatorType.Current)
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
        Try
            Dim obj As clsPaymentProcessHead = clsPaymentProcessHead.getData(strCode, navType, Nothing, "", True)
            If obj IsNot Nothing Then
                Reset()
                fndLoc.Enabled = False
                isNewEntry = False
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                btnProcess.Enabled = True
                btnPost.Enabled = True
                fndDocNo.MyReadOnly = True
                fndDocNo.Value = obj.Doc_No
                fndLoc.Value = obj.Loc_Seg_Code
                If clsCommon.myLen(clsCommon.myCstr(obj.Loc_Seg_Code)) > 0 Then
                    txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndLoc.Value & "' "))
                Else
                    txtLocName.Text = ""
                End If

                dtpFromDate.Value = obj.From_Date
                dtpToDate.Value = obj.To_Date
                dtpDate.Value = obj.Doc_Date
                txtNEFTUploaderREFNo.Text = obj.DocRefNoForUploader
                txtNEFTUploaderREFNo.Tag = obj.PaymentDesc
                fndArea.Value = obj.Area_Location_Code
                txtMCC.Text = obj.MCC_Code_Selected
                lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select MCC_Name from tspl_MCC_Master where mcc_code='" + obj.MCC_Code_Selected + "' "))

                chkSkipPrevItemIssue.Checked = obj.Is_Skip_Previous_Item_Issue
                chkSkipPrevItemIssueReturn.Checked = obj.Is_Skip_Previous_Item_Issue_Return
                chkSkipPrevMccSale.Checked = obj.Is_Skip_Previous_MCC_Sale
                ChkSkipMccSaleReturn.Checked = obj.Is_Skip_Previous_MCC_Sale_Return
                chkSkipPrevCreditNote.Checked = obj.Is_Skip_Previous_Credit_Note
                chkSkipPrevDeduction.Checked = obj.Is_Skip_Previous_Debit_Note
                chkSkipPreviousDocumentOfAdvancePayment.Checked = obj.Is_Skip_Previous_Advacee_Payment
                SetTagOFCheckBox()

                If obj.isPrePosted = 0 Then
                    lblPrePending.Status = ERPTransactionStatus.Pending
                    btnPrintBillMobUser.Enabled = False
                Else
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    lblPrePending.Status = ERPTransactionStatus.Approved
                    btnPrintBillMobUser.Enabled = True
                End If

                If obj.isPosted = 0 Then
                    lblPending.Status = ERPTransactionStatus.Pending
                Else
                    lblPending.Status = ERPTransactionStatus.Approved
                    btnProcess.Enabled = False
                End If
                isLoad = True
                Dim i As Integer = 0

                If obj.dtClsPaymentProcessInvoices IsNot Nothing AndAlso obj.dtClsPaymentProcessInvoices.Rows.Count > 0 Then
                    gvInvoice.DataSource = Nothing
                    gvInvoice.AutoGenerateColumns = False
                    gvInvoice.DataSource = obj.dtClsPaymentProcessInvoices
                    'gvInvoice.Columns(colSlno).FieldName = "Item_Code"
                    gvInvoice.Columns(colSelect).FieldName = "Sel"
                    gvInvoice.Columns(colSlno).FieldName = "SLNO"
                    gvInvoice.Columns(colAPInvoiceNo).FieldName = "AP_Invoice_No"
                    gvInvoice.Columns(colAPInvoiceDate).FieldName = "AP_Invoice_Date"
                    gvInvoice.Columns(colPurchaseInvoiceNo).FieldName = "Milk_Purchase_Invoice_No"
                    gvInvoice.Columns(colPurchaseInvoiceDate).FieldName = "Milk_Purchase_Invoice_Date"
                    gvInvoice.Columns(colVLCCode).FieldName = "VLC_CODE"
                    gvInvoice.Columns(colVLCName).FieldName = "VLC_Name"
                    gvInvoice.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                    gvInvoice.Columns(colMCCCode).FieldName = "MCC_Code"
                    gvInvoice.Columns(colRouteCode).FieldName = "ROUTE_CODE"
                    gvInvoice.Columns(colRouteName).FieldName = "ROUTE_NAME"
                    gvInvoice.Columns(colVendorCode).FieldName = "VSP_CODE"
                    gvInvoice.Columns(colVendorDesc).FieldName = "VSP_NAME"
                    gvInvoice.Columns(colPayeeJointName).FieldName = "Payee_Joint_Name"
                    gvInvoice.Columns(colPayeeJointBankCode).FieldName = "Payee_Joint_Bank_Code"
                    gvInvoice.Columns(colPayeeJointBankDesc).FieldName = "Payee_Joint_Bank_Name"
                    gvInvoice.Columns(colPayeeJointBranchCode).FieldName = "Payee_Joint_Branch_Code"
                    gvInvoice.Columns(colPayeeJointBranchDesc).FieldName = "Payee_Joint_Branch_Name"
                    gvInvoice.Columns(colPayeeJointIFSC).FieldName = "Payee_Joint_IFSC_Code"
                    gvInvoice.Columns(colPayeeJointAcNo).FieldName = "Payee_Joint_Ac_No"
                    gvInvoice.Columns(colMilkQty).FieldName = "Milk_Qty"
                    gvInvoice.Columns(colInvAmt).FieldName = "Inv_Amount"
                    gvInvoice.Columns(colEmpAmt).FieldName = "Inv_EMP_Amount"
                    gvInvoice.Columns(colInvAndEmpAmt).FieldName = "Inv_Amt_EMP_Amount"
                    gvInvoice.Columns(colIncenAmt).FieldName = "Inv_Incentive_Amount"
                    gvInvoice.Columns(colIncenEmpAmt).FieldName = "Inv_Incentive_EMP_Amount"
                    gvInvoice.Columns(colTDSAmt).FieldName = "TDS_Amount"
                    gvInvoice.Columns(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).FieldName = "Gross_Amount"
                    gvInvoice.Columns(colVSPOwnSystemAmt).FieldName = "Vsp_Own_System_Amount"
                    'gvInvoice.Columns(colVSPOwnSystemAmt).Tag = "Vsp_Own_System_Doc_No" ''BSP Pending
                    gvInvoice.Columns(colHeadLoadAmt).FieldName = "Head_Load_Amount"
                    gvInvoice.Columns(colHeadLoadAmt).Tag = "Head_Load_Doc_No"
                    gvInvoice.Columns(colInvDeduc).FieldName = "Deduction_Amount"
                    'gvInvoice.Columns(colInvDeduc).Tag = "Deduction_Doc_No"  ''BSP Pending
                    gvInvoice.Columns(colReduceDeduc).FieldName = "Reduce_Deduc_Amt"
                    gvInvoice.Columns(colBankCode).FieldName = "Bank_Code"
                    gvInvoice.Columns(colBankDesc).FieldName = "Bank_Desc"
                    gvInvoice.Columns(colPayMode).FieldName = "Payment_Mode"
                    gvInvoice.Columns(colChequeNo).FieldName = "Cheque_No"
                    gvInvoice.Columns(colServiceChargeAmt).FieldName = "Service_Charge_Amt"
                    gvInvoice.Columns(colActualVSPCode).FieldName = "ActualVSPCode"
                    gvInvoice.Columns(colActualVSPName).FieldName = "ActualVSPName"
                    gvInvoice.Columns(colHandlingCharges).FieldName = "Handling_Charges_Amount"
                    gvInvoice.Columns(colSRNROAmt).FieldName = "SRN_RO_Amount"
                    gvInvoice.Columns(colSRNNetAmount).FieldName = "SRN_Net_Amount"
                    gvInvoice.Columns(colMPAmount).FieldName = "MP_Amount"
                    gvInvoice.Columns(colMPEMPAmount).FieldName = "MP_EMP"
                    gvInvoice.Columns(colMPIncentiveAmount).FieldName = "MP_Incentive"
                    gvInvoice.Columns(colMPEMPIncentiveAmount).FieldName = "MP_IncentiveEMP"
                    gvInvoice.Columns(colMPNetAmount).FieldName = "MP_Net_Amount"
                    gvInvoice.Columns(colFATKG).FieldName = "FATKg"
                    gvInvoice.Columns(colFATPer).FieldName = "FATPer"
                    gvInvoice.Columns(colSNFKG).FieldName = "SNFKg"
                    gvInvoice.Columns(colSNFPer).FieldName = "SNFPer"
                End If
                If obj.dtClsPaymentProcessMccSale IsNot Nothing AndAlso obj.dtClsPaymentProcessMccSale.Rows.Count > 0 Then
                    gvMccSale.DataSource = Nothing
                    gvMccSale.AutoGenerateColumns = False
                    gvMccSale.DataSource = obj.dtClsPaymentProcessMccSale

                    gvMccSale.Columns(colSlno).FieldName = "SLNO"
                    gvMccSale.Columns(colSelect).FieldName = "Sel"
                    gvMccSale.Columns(colShipmentNo).FieldName = "Shipment_Doc_No"
                    gvMccSale.Columns(colShipmentDate).FieldName = "Shipment_Doc_Date"
                    gvMccSale.Columns(colSaleInvNo).FieldName = "Sale_Doc_No"
                    gvMccSale.Columns(colSaleInvDate).FieldName = "Sale_Doc_Date"
                    gvMccSale.Columns(colARInvoiceNo).FieldName = "AR_Invoice_No"
                    gvMccSale.Columns(colARInvoiceDate).FieldName = "AR_Invoice_Date"
                    gvMccSale.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                    gvMccSale.Columns(colCustomerCode).FieldName = "Customer_CODE"
                    gvMccSale.Columns(colCustomerName).FieldName = "Customer_NAME"
                    gvMccSale.Columns(colItemAmt).FieldName = "Amount"
                    gvMccSale.Columns(colReduceDeduc).FieldName = "Reduce_Deduc_Amt"
                    gvMccSale.Columns(colOrgBalanceAmt).FieldName = "Original_Balance_Amount"
                    gvMccSale.Columns(colInstallmentAmt).FieldName = "Instalment_Amt"
                End If
                If obj.dtClsPaymentProcessItemIssue IsNot Nothing AndAlso obj.dtClsPaymentProcessItemIssue.Rows.Count > 0 Then
                    gvItemIssue.DataSource = Nothing
                    gvItemIssue.AutoGenerateColumns = False
                    gvItemIssue.DataSource = obj.dtClsPaymentProcessItemIssue

                    gvItemIssue.Columns(colSlno).FieldName = "SLNO"
                    gvItemIssue.Columns(colSelect).FieldName = "Sel"
                    gvItemIssue.Columns(colVspItemIssueNo).FieldName = "Item_Issue_Doc_No"
                    gvItemIssue.Columns(colVspItemIssueDate).FieldName = "Item_Issue_Doc_Date"
                    gvItemIssue.Columns(colAPInvoiceNo).FieldName = "AP_Invoice_No"
                    gvItemIssue.Columns(colAPInvoiceDate).FieldName = "AP_Invoice_Date"
                    gvItemIssue.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                    gvItemIssue.Columns(colVendorCode).FieldName = "Vendor_CODE"
                    gvItemIssue.Columns(colVendorDesc).FieldName = "Vendor_NAME"
                    gvItemIssue.Columns(colItemAmt).FieldName = "Amount"
                    gvItemIssue.Columns(colReduceDeduc).FieldName = "Reduce_Deduc_Amt"
                End If
                If obj.dtClsPaymentProcessItemIssueReturn IsNot Nothing AndAlso obj.dtClsPaymentProcessItemIssueReturn.Rows.Count > 0 Then
                    gvItemIssueReturn.DataSource = Nothing
                    gvItemIssueReturn.AutoGenerateColumns = False
                    gvItemIssueReturn.DataSource = obj.dtClsPaymentProcessItemIssueReturn

                    gvItemIssueReturn.Columns(colSlno).FieldName = "SLNO"
                    gvItemIssueReturn.Columns(colSelect).FieldName = "Sel"
                    gvItemIssueReturn.Columns(colVspItemIssueReturnNo).FieldName = "Item_Issue_Return_No"
                    gvItemIssueReturn.Columns(colVspItemIssueNo).FieldName = "Item_Issue_Doc_No"
                    gvItemIssueReturn.Columns(colVspItemIssueDate).FieldName = "Item_Issue_Return_Date"
                    gvItemIssueReturn.Columns(colAPInvoiceNo).FieldName = "AP_Invoice_No"
                    gvItemIssueReturn.Columns(colAPInvoiceDate).FieldName = "AP_Invoice_Date"
                    gvItemIssueReturn.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                    gvItemIssueReturn.Columns(colVendorCode).FieldName = "Vendor_CODE"
                    gvItemIssueReturn.Columns(colVendorDesc).FieldName = "Vendor_NAME"
                    gvItemIssueReturn.Columns(colItemAmt).FieldName = "Amount"
                End If
                If obj.dtClsPaymentProcessDeductions IsNot Nothing AndAlso obj.dtClsPaymentProcessDeductions.Rows.Count > 0 Then
                    gvDeduction.DataSource = Nothing
                    gvDeduction.AutoGenerateColumns = False
                    gvDeduction.DataSource = obj.dtClsPaymentProcessDeductions


                    gvDeduction.Columns(colSlno).FieldName = "SLNO"
                    gvDeduction.Columns(colSelect).FieldName = "Sel"
                    gvDeduction.Columns(colAPInvoiceNo).FieldName = "AP_Invoice_No"
                    gvDeduction.Columns(colAPInvoiceDate).FieldName = "AP_Invoice_Date"
                    gvDeduction.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                    gvDeduction.Columns(colVendorCode).FieldName = "Vendor_CODE"
                    gvDeduction.Columns(colVendorDesc).FieldName = "Vendor_NAME"
                    gvDeduction.Columns(colDeductionCode).FieldName = "Ded_Code"
                    gvDeduction.Columns(colDeductionDesc).FieldName = "Ded_Desc"
                    gvDeduction.Columns(colItemAmt).FieldName = "Amount"
                    gvDeduction.Columns(colReduceDeduc).FieldName = "Reduce_Deduc_Amt"

                End If
                If obj.dtClsPaymentProcessCreditNote IsNot Nothing AndAlso obj.dtClsPaymentProcessCreditNote.Rows.Count > 0 Then
                    gvCreditNote.DataSource = Nothing
                    gvCreditNote.AutoGenerateColumns = False
                    gvCreditNote.DataSource = obj.dtClsPaymentProcessCreditNote

                    gvCreditNote.Columns(colSlno).FieldName = "SLNO"
                    gvCreditNote.Columns(colSelect).FieldName = "Sel"
                    gvCreditNote.Columns(colAPInvoiceNo).FieldName = "AP_Invoice_No"
                    gvCreditNote.Columns(colAPInvoiceDate).FieldName = "AP_Invoice_Date"
                    gvCreditNote.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                    gvCreditNote.Columns(colVendorCode).FieldName = "Vendor_CODE"
                    gvCreditNote.Columns(colVendorDesc).FieldName = "Vendor_NAME"
                    gvCreditNote.Columns(colTDSAmt).FieldName = "TDS_Amount"
                    gvCreditNote.Columns(colItemAmt).FieldName = "Amount"

                End If
                If obj.dtclsPaymentProcessSaving IsNot Nothing AndAlso obj.dtclsPaymentProcessSaving.Rows.Count > 0 Then
                    gvSaving.DataSource = Nothing
                    gvSaving.AutoGenerateColumns = False
                    gvSaving.DataSource = obj.dtclsPaymentProcessSaving

                    gvSaving.Columns(colSlno).FieldName = "SLNO"
                    gvSaving.Columns(colSelect).FieldName = "Sel"
                    gvSaving.Columns(colAPInvoiceNo).FieldName = "AP_Invoice_No"
                    gvSaving.Columns(colAPInvoiceDate).FieldName = "Posting_Date"
                    gvSaving.Columns(colAPInvoiceType).FieldName = "Document_Type"
                    gvSaving.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                    gvSaving.Columns(colVendorCode).FieldName = "Vendor_Code"
                    gvSaving.Columns(colVendorDesc).FieldName = "Vendor_Name"
                    gvSaving.Columns(colItemAmt).FieldName = "Document_Total"

                End If
                If obj.dtclsPaymentProcessCompulsory IsNot Nothing AndAlso obj.dtclsPaymentProcessCompulsory.Rows.Count > 0 Then
                    gvCompulsory.DataSource = Nothing
                    gvCompulsory.AutoGenerateColumns = False
                    gvCompulsory.DataSource = obj.dtclsPaymentProcessCompulsory

                    gvCompulsory.Columns(colSlno).FieldName = "SLNO"
                    gvCompulsory.Columns(colSelect).FieldName = "Sel"
                    gvCompulsory.Columns(colAPInvoiceNo).FieldName = "AP_Invoice_No"
                    gvCompulsory.Columns(colAPInvoiceDate).FieldName = "Posting_Date"
                    gvCompulsory.Columns(colAPInvoiceType).FieldName = "Document_Type"
                    gvCompulsory.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                    gvCompulsory.Columns(colVendorCode).FieldName = "Vendor_Code"
                    gvCompulsory.Columns(colVendorDesc).FieldName = "Vendor_Name"
                    gvCompulsory.Columns(colItemAmt).FieldName = "Document_Total"

                End If
                If obj.dtClsPaymentProcessMccSaleReturn IsNot Nothing AndAlso obj.dtClsPaymentProcessMccSaleReturn.Rows.Count > 0 Then
                    GvMccSaleReturn.DataSource = Nothing
                    GvMccSaleReturn.AutoGenerateColumns = False
                    GvMccSaleReturn.DataSource = obj.dtClsPaymentProcessMccSaleReturn

                    GvMccSaleReturn.Columns(colSlno).FieldName = "SLNO"
                    GvMccSaleReturn.Columns(colSelect).FieldName = "Sel"
                    GvMccSaleReturn.Columns(colReturnDocNo).FieldName = "Return_Doc_No"
                    GvMccSaleReturn.Columns(colReturnDocType).FieldName = "Return_Doc_Type"
                    GvMccSaleReturn.Columns(colReturnDocDate).FieldName = "Return_Doc_Date"
                    GvMccSaleReturn.Columns(colShipmentNo).FieldName = "Shipment_Doc_No"
                    GvMccSaleReturn.Columns(colShipmentDate).FieldName = "Shipment_Doc_Date"
                    GvMccSaleReturn.Columns(colSaleInvNo).FieldName = "Sale_Doc_No"
                    GvMccSaleReturn.Columns(colSaleInvDate).FieldName = "Sale_Doc_Date"
                    GvMccSaleReturn.Columns(colARInvoiceNo).FieldName = "AR_Invoice_No"
                    GvMccSaleReturn.Columns(colARInvoiceDate).FieldName = "AR_Invoice_Date"
                    GvMccSaleReturn.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                    GvMccSaleReturn.Columns(colCustomerCode).FieldName = "Customer_CODE"
                    GvMccSaleReturn.Columns(colCustomerName).FieldName = "Customer_NAME"
                    GvMccSaleReturn.Columns(colItemAmt).FieldName = "Amount"
                End If
                If obj.dtPPAdvancePayment IsNot Nothing AndAlso obj.dtPPAdvancePayment.Rows.Count > 0 Then
                    gvAdvancePayment.DataSource = Nothing
                    gvAdvancePayment.AutoGenerateColumns = False
                    gvAdvancePayment.DataSource = obj.dtPPAdvancePayment
                    gvAdvancePayment.Columns(colAPSNo).FieldName = "SNo"
                    gvAdvancePayment.Columns(colAPSelect).FieldName = "Sel"
                    gvAdvancePayment.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                    gvAdvancePayment.Columns(colAPVendorCode).FieldName = "Vendor_Code"
                    gvAdvancePayment.Columns(colAPVendorName).FieldName = "Vendor_Name"
                    gvAdvancePayment.Columns(colAPPaymentCode).FieldName = "Payment_No"
                    gvAdvancePayment.Columns(colAPPaymentDate).FieldName = "Payment_Date"
                    gvAdvancePayment.Columns(colAPPaymentAmt).FieldName = "Payment_Amount"
                    gvAdvancePayment.Columns(colAPInstallmentAmt).FieldName = "Installment_Amount"
                    gvAdvancePayment.Columns(colAPPaymentAmtBalance).FieldName = "Payment_Balance"
                    gvAdvancePayment.Columns(colAPNoOfInstallment).FieldName = "No_Of_EMI"
                End If
                If obj.dtPPAssetLost IsNot Nothing AndAlso obj.dtPPAssetLost.Rows.Count > 0 Then
                    gvAssetLost.DataSource = Nothing
                    gvAssetLost.AutoGenerateColumns = False
                    gvAssetLost.DataSource = obj.dtPPAssetLost

                    gvAssetLost.Columns(colALSNo).FieldName = "SNo"
                    gvAssetLost.Columns(colALSelect).FieldName = "Sel"
                    gvAssetLost.Columns(colVLCUploaderCode).FieldName = "VLC_Code_VLC_Uploader"
                    gvAssetLost.Columns(colALVendorCode).FieldName = "Vendor_Code"
                    gvAssetLost.Columns(colALVendorName).FieldName = "Vendor_Name"
                    gvAssetLost.Columns(colALPaymentCode).FieldName = "Payment_No"
                    gvAssetLost.Columns(colALPaymentDate).FieldName = "Payment_Date"
                    gvAssetLost.Columns(colALPaymentAmt).FieldName = "Payment_Amount"

                End If

                LoadBlankGridGV()
                If obj.dtPPDetail IsNot Nothing AndAlso obj.dtPPDetail.Rows.Count > 0 Then
                    Is_gv_Rows_Clear = False
                    gv.DataSource = Nothing
                    gv.AutoGenerateColumns = False
                    gv.DataSource = obj.dtPPDetail
                    gv.Columns(colSelect).FieldName = "Is_select"
                    gv.Columns(colSlno).FieldName = "SNo"
                    gv.Columns(colIsPaymentProcessHold).FieldName = "is_Hold_Payment_Process"

                    gv.Columns(colIsPaymentProcessHoldSaving).FieldName = "is_Hold_Payment_Process_Saving"
                    gv.Columns(colIsPaymentProcessHoldSavingAuto).FieldName = "is_Hold_Payment_Process_Saving_Auto"
                    gv.Columns(colIsPaymentProcessHoldSavingManual).FieldName = "is_Hold_Payment_Process_Saving_Manual"

                    gv.Columns(colPurchaseInvoiceNo).FieldName = "Milk_Purchase_Invoice_No"
                    gv.Columns(colPurchaseInvoiceDate).FieldName = "Milk_Purchase_Invoice_Date"
                    gv.Columns(colAPInvoiceNo).FieldName = "AP_Invoice_No"
                    gv.Columns(colAPInvoiceDate).FieldName = "AP_Invoice_Date"
                    gv.Columns(colVLCUploaderCode).FieldName = "VLC_CODE_Uploader"
                    gv.Columns(colVLCName).FieldName = "VLC_Name"
                    gv.Columns(colMCCCode).FieldName = "MCC_Code"
                    gv.Columns(colRouteCode).FieldName = "ROUTE_CODE"
                    gv.Columns(colRouteName).FieldName = "ROUTE_NAME"
                    gv.Columns(colVendorCode).FieldName = "VSP_CODE"
                    gv.Columns(colVendorDesc).FieldName = "VSP_NAME"
                    gv.Columns(colActualVSPCode).FieldName = "Main_VSP_CODE"
                    gv.Columns(colActualVSPName).FieldName = "Main_VSP_NAME"
                    gv.Columns(colPayeeJointName).FieldName = "Payee_Joint_Name"
                    gv.Columns(colPayeeJointBankCode).FieldName = "Payee_Joint_Bank_Code"
                    gv.Columns(colPayeeJointBankDesc).FieldName = "Payee_Joint_Bank_Name"
                    gv.Columns(colPayeeJointBranchCode).FieldName = "Payee_Joint_Branch_Code"
                    gv.Columns(colPayeeJointBranchDesc).FieldName = "Payee_Joint_Branch_Name"
                    gv.Columns(colPayeeJointAcNo).FieldName = "Payee_Joint_Account_No"
                    gv.Columns(colPayeeJointIFSC).FieldName = "Payee_Joint_IFSC_Code"
                    gv.Columns(colBankCode).FieldName = "Bank_Code"
                    gv.Columns(colBankDesc).FieldName = "Bank_Desc"
                    gv.Columns(colPayMode).FieldName = "Payment_Mode"
                    gv.Columns(colChequeNo).FieldName = "Cheque_No"
                    gv.Columns(colChequeDate).FieldName = "Cheque_Dated"
                    gv.Columns(colBankCodeSaving).FieldName = "Bank_Code_Saving"
                    gv.Columns(colBankDescSaving).FieldName = "Bank_Desc_Saving"
                    gv.Columns(colPayModeSaving).FieldName = "Payment_Mode_Saving"
                    gv.Columns(colMilkQty).FieldName = "Milk_Qty"
                    gv.Columns(colVSPAmount).FieldName = "VSP_Amount"
                    gv.Columns(colHandlingCharges).FieldName = "Handling_Charges_Amount"
                    gv.Columns(colSRNROAmt).FieldName = "SRN_RO_Amount"
                    gv.Columns(colSRNNetAmount).FieldName = "SRN_Net_Amount"
                    gv.Columns(colMPAmount).FieldName = "MP_Amount"
                    gv.Columns(colMPEMPAmount).FieldName = "MP_EMP"
                    gv.Columns(colMPIncentiveAmount).FieldName = "MP_Incentive"
                    gv.Columns(colMPEMPIncentiveAmount).FieldName = "MP_IncentiveEMP"
                    gv.Columns(colMPNetAmount).FieldName = "MP_Net_Amount"
                    gv.Columns(colMPVSPDiffAmount).FieldName = "MP_VSP_Diff_Amount"
                    gv.Columns(colIncenAmt).FieldName = "Incentive_Amount"
                    gv.Columns(colEmpAmt).FieldName = "EMP_Amount"
                    gv.Columns(colIncenEmpAmt).FieldName = "Incentive_EMP_Amount"
                    gv.Columns(colTotalEmp).FieldName = "Total_EMP_Amount"
                    gv.Columns(colInvAmt).FieldName = "Milk_Amount"
                    gv.Columns(colInvAndEmpAmt).FieldName = "Incentive_EMP_Amount"
                    gv.Columns(colInvAndEmpAmt).FieldName = "Total"
                    gv.Columns(colTDSAmt).FieldName = "TDS_Amount"
                    gv.Columns(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).FieldName = "Total_Invoice_Amount"
                    gv.Columns(colVSPOwnSystemAmt).FieldName = "Vsp_Own_System_Amount"
                    gv.Columns(colHeadLoadAmt).FieldName = "Head_Load_Amount"
                    gv.Columns(colInvDeduc).FieldName = "Invoice_Deduction_Amount"
                    gv.Columns(colReduceDeduc).FieldName = "Reduce_Deduc_Amt"
                    gv.Columns(colMccSaleTotalAmount).FieldName = "MCC_Sale_Amount"
                    gv.Columns(colMccSaleReturnTotalAmount).FieldName = "MCC_Sale_Return_Amount"
                    gv.Columns(colItemIssueTotalAmount).FieldName = "Item_Issue_Amount"
                    gv.Columns(colItemIssueReturnTotalAmount).FieldName = "Item_Issue_Return_Amount"
                    gv.Columns(colDeductionTotalAmount).FieldName = "Deduction_Amount"
                    gv.Columns(colAssetLostAmount).FieldName = "Asset_Lost_Amount"
                    gv.Columns(colTotalCreditNoteAmount).FieldName = "Credit_Note_Amount"
                    gv.Columns(colTotalSavingAmount).FieldName = "Saving_Amount"
                    gv.Columns(colTotalCompulsoryAmount).FieldName = "Compulsory_Amount"
                    gv.Columns(colPaybleAmt).FieldName = "Payable_Amount"
                    gv.Columns(colServiceChargeAmt).FieldName = "Service_Charge_Amt"
                    gv.Columns(colAdvanceAmount).FieldName = "Advance_Payment_Amount"
                    gv.Columns(colAdvanceKnockOffAmount).FieldName = "Advance_Payment_Amount_Knock_Off"
                    gv.Columns(colFATPer).FieldName = "FATPer"
                    gv.Columns(colFATKG).FieldName = "FATKg"
                    gv.Columns(colSNFPer).FieldName = "SNFPer"
                    gv.Columns(colSNFKG).FieldName = "SNFKg"

                    Dim arr As New ArrayList()
                    Dim arrMcc As New ArrayList()
                    For i = 0 To gv.Rows.Count - 1
                        If Not arr.Contains(clsCommon.myCstr(gv.Rows(i).Cells(colVendorCode).Value)) Then
                            arr.Add(clsCommon.myCstr(gv.Rows(i).Cells(colVendorCode).Value))
                        End If
                        If Not arrMcc.Contains(clsCommon.myCstr(gv.Rows(i).Cells(colMCCCode).Value)) Then
                            arrMcc.Add(clsCommon.myCstr(gv.Rows(i).Cells(colMCCCode).Value))
                        End If
                    Next
                    txtVSP.arrValueMember = arr
                    mfndMcc.arrValueMember = arrMcc
                    AddSummary()
                    ReStoreGridLayout()
                End If




                If clsCommon.myLen(txtMCC.Text) > 0 Then
                    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Text, dtpToDate.Value)
                    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Text, dtpToDate.Value)
                Else
                    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(fndLoc.Value, dtpToDate.Value)
                    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(fndLoc.Value, dtpToDate.Value)
                End If
                '=====================if document go for approval then no post button visible or if document contain related setting
                btnPost.Visible = MyBase.isPostFlag
                If Not clsApply_Approval.Visibility_PostButtonForApproval(fndLoc.Value, "", MyBase.Form_ID, clsCommon.myCstr(fndDocNo.Value), 0, 0, "") Then
                    btnPost.Visible = True
                    If lblPrePending.Status = ERPTransactionStatus.Pending Then
                        lblPrePending.Status = clsApply_Approval.ApprovalCondCheck_Doc(MyBase.Form_ID, clsCommon.myCstr(fndDocNo.Value), Nothing)
                    End If
                End If

                isLoad = False
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub AddSummary()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gv.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To gv.Columns.Count - 1
            If TypeOf (gv.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gv.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next


        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Dim summaryRowItemInvoice As New GridViewSummaryRowItem()
        gvInvoice.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To gvInvoice.Columns.Count - 1
            If TypeOf (gvInvoice.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItemInvoice.Add(New GridViewSummaryItem(gvInvoice.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvInvoice.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemInvoice)
        gvInvoice.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Dim summaryRowItemMCCSale As New GridViewSummaryRowItem()
        gvMccSale.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To gvMccSale.Columns.Count - 1
            If TypeOf (gvMccSale.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItemMCCSale.Add(New GridViewSummaryItem(gvMccSale.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvMccSale.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemMCCSale)
        gvMccSale.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Dim summaryRowItemMCCSaleReturn As New GridViewSummaryRowItem()
        GvMccSaleReturn.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To GvMccSaleReturn.Columns.Count - 1
            If TypeOf (GvMccSaleReturn.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItemMCCSaleReturn.Add(New GridViewSummaryItem(GvMccSaleReturn.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        GvMccSaleReturn.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemMCCSaleReturn)
        GvMccSaleReturn.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Dim summaryRowItemIssue As New GridViewSummaryRowItem()
        gvItemIssue.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To gvItemIssue.Columns.Count - 1
            If TypeOf (gvItemIssue.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItemIssue.Add(New GridViewSummaryItem(gvItemIssue.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvItemIssue.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemIssue)
        gvItemIssue.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Dim summaryRowItemIssueReturn As New GridViewSummaryRowItem()
        gvItemIssueReturn.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To gvItemIssueReturn.Columns.Count - 1
            If TypeOf (gvItemIssueReturn.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItemIssueReturn.Add(New GridViewSummaryItem(gvItemIssueReturn.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvItemIssueReturn.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemIssueReturn)
        gvItemIssueReturn.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Dim summaryRowDeduction As New GridViewSummaryRowItem()
        gvDeduction.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To gvDeduction.Columns.Count - 1
            If TypeOf (gvDeduction.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowDeduction.Add(New GridViewSummaryItem(gvDeduction.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvDeduction.MasterTemplate.SummaryRowsBottom.Add(summaryRowDeduction)
        gvDeduction.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Dim summaryRowCreditNote As New GridViewSummaryRowItem()
        gvCreditNote.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To gvCreditNote.Columns.Count - 1
            If TypeOf (gvCreditNote.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowCreditNote.Add(New GridViewSummaryItem(gvCreditNote.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvCreditNote.MasterTemplate.SummaryRowsBottom.Add(summaryRowCreditNote)
        gvCreditNote.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Dim summaryRowAdvance As New GridViewSummaryRowItem()
        gvAdvancePayment.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To gvAdvancePayment.Columns.Count - 1
            If TypeOf (gvAdvancePayment.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowAdvance.Add(New GridViewSummaryItem(gvAdvancePayment.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvAdvancePayment.MasterTemplate.SummaryRowsBottom.Add(summaryRowAdvance)
        gvAdvancePayment.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        Dim summaryRowAssetLost As New GridViewSummaryRowItem()
        gvAssetLost.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To gvAssetLost.Columns.Count - 1
            If TypeOf (gvAssetLost.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowAdvance.Add(New GridViewSummaryItem(gvAssetLost.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvAssetLost.MasterTemplate.SummaryRowsBottom.Add(summaryRowAdvance)
        gvAssetLost.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        Dim summaryRowSaving As New GridViewSummaryRowItem()
        gvSaving.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To gvSaving.Columns.Count - 1
            If TypeOf (gvSaving.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowSaving.Add(New GridViewSummaryItem(gvSaving.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvSaving.MasterTemplate.SummaryRowsBottom.Add(summaryRowSaving)
        gvSaving.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Dim summaryRowCompulsory As New GridViewSummaryRowItem()
        gvCompulsory.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To gvCompulsory.Columns.Count - 1
            If TypeOf (gvCompulsory.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowCompulsory.Add(New GridViewSummaryItem(gvCompulsory.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvCompulsory.MasterTemplate.SummaryRowsBottom.Add(summaryRowCompulsory)
        gvCompulsory.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub fndDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocNo._MYNavigator
        LoadData(fndDocNo.Value, NavType)
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Sub PaymentProcess()
        Dim qry As String = ""
        Dim msg As String = ""
        Dim dt As DataTable = Nothing
        clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(fndDocNo.Value))
        Try

            If SettVSPHoldPaymentNotCompanyBank = False Then
                If Not AllowToSave() Then
                    Exit Sub
                End If
                GC.Collect()
                GC.WaitForPendingFinalizers()
                SaveData(True)
            End If
            'AutoFillAllVSP()
            If clsCommon.MyMessageBoxShow("Continue to Process the payment ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                clsPaymentProcessHead.ProcessData(fndDocNo.Value, "Payment Process")
                clsCommon.MyMessageBoxShow(Me, "Payment Process", Me.Text)
                LoadData(fndDocNo.Value, NavigatorType.Current)
            End If
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
        clsCommon.ProgressBarPercentShow()
        Try
            Dim TaskTotal As Integer = 12
            Dim TaskCurrent As Integer = 0
            If clsCommon.myLen(fndLoc.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                Throw New Exception("Please select Location segment")
            End If
            If SettShowMCCFinder AndAlso MultipleFinderFillAuto = False Then
                If clsCommon.myLen(txtMCC.Text) <= 0 Then
                    Throw New Exception("Please select MCC")
                End If
            End If
            If MultipleFinderFillAuto = True Then
                If mfndMcc.arrValueMember Is Nothing OrElse mfndMcc.arrValueMember.Count <= 0 Then
                    mfndMcc.Focus()
                    Throw New Exception("Please select Location ")
                End If
            End If
            If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
                txtVSP.Focus()
                Throw New Exception("Please select at lease one VSP ")
            End If

            clsCommon.ProgressBarPercentUpdate(TaskCurrent, TaskTotal, "Load Invoice Data")
            LoadInvoiceGridData()

            If isMultipleDocumentForSameVendor() Then
                gvInvoice.Rows.Clear()
                Throw New Exception("Multiple Invoices For Same vendor Found in selected date range" & Environment.NewLine & "Please select another Date range and continue " & Environment.NewLine & getMultipleDocumentForSameVendor())
            End If
            getMCCs()
            getVendors()

            TaskCurrent += 1
            clsCommon.ProgressBarPercentUpdate(TaskCurrent, TaskTotal, "Load MCC Sale Data")
            LoadMccSaleGridData()

            TaskCurrent += 1
            clsCommon.ProgressBarPercentUpdate(TaskCurrent, TaskTotal, "Load MCC Sale Return Data")
            LoadMccSaleReturnGridData()

            TaskCurrent += 1
            clsCommon.ProgressBarPercentUpdate(TaskCurrent, TaskTotal, "Load Item Issue Data")
            LoadItemIssueGridData()

            TaskCurrent += 1
            clsCommon.ProgressBarPercentUpdate(TaskCurrent, TaskTotal, "Load Item Issue Return Data")
            LoadItemIssueReturnGridData()

            TaskCurrent += 1
            clsCommon.ProgressBarPercentUpdate(TaskCurrent, TaskTotal, "Load Deduction Data")
            LoadDeductionGridData()

            TaskCurrent += 1
            clsCommon.ProgressBarPercentUpdate(TaskCurrent, TaskTotal, "Load Addition Data")
            LoadCreditNoteGridData()

            TaskCurrent += 1
            clsCommon.ProgressBarPercentUpdate(TaskCurrent, TaskTotal, "Load Saving Data")
            LoadSavingGridData()

            TaskCurrent += 1
            clsCommon.ProgressBarPercentUpdate(TaskCurrent, TaskTotal, "Load Compulsory Data")
            LoadCompulsoryGridData()

            TaskCurrent += 1
            clsCommon.ProgressBarPercentUpdate(TaskCurrent, TaskTotal, "Load Advance Payment Data")
            LoadAdvancePaymentGridData()

            TaskCurrent += 1
            clsCommon.ProgressBarPercentUpdate(TaskCurrent, TaskTotal, "Load Asset Lost Data")
            LoadAssetLostGridData()

            TaskCurrent += 1
            clsCommon.ProgressBarPercentUpdate(TaskCurrent, TaskTotal, "Calculate Payable Amount")
            LoadBlankGridGV()
            loadGvData()
            SetTagOFCheckBox()
            clsCommon.ProgressBarPercentHide()
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isLoad = False
        End Try
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

    Sub loadGvData()
        LoadBlankGridGV()
        If PayableAmountZeroForMCCSale Then
            For jj As Integer = gvMccSale.Rows.Count - 1 To 0 Step -1
                gvMccSale.CurrentColumn = gvMccSale.Columns(colReduceDeduc)
                gvMccSale.CurrentRow = gvMccSale.Rows(jj)
                gvMccSale.Rows(jj).Cells(colReduceDeduc).Value = 0
            Next
        End If


        Dim k As Integer = -1
        Dim VendCustCode As String = ""
        getMCCs()
        getVendors()

        Dim strWhr As String = ""
        If MultipleFinderFillAuto Then
            Dim dtSeg As DataTable = clsDBFuncationality.GetDataTable("select distinct Loc_Segment_Code from TSPL_LOCATION_MASTER where location_code in (" + clsCommon.GetMulcallString(mfndMcc.arrValueMember) + ") ")
            If dtSeg IsNot Nothing AndAlso dtSeg.Rows.Count > 0 Then
                Dim ArrSeg As New ArrayList
                For Each drSeg As DataRow In dtSeg.Rows
                    ArrSeg.Add(drSeg("Loc_Segment_Code"))
                Next
                strWhr = " and Loc_Segment_Code in ( " + clsCommon.GetMulcallString(ArrSeg) + ") "
            Else
                strWhr = " and 2=3 "
            End If
        Else
            strWhr = " and Loc_Segment_Code = '" & fndLoc.Value & "' "
        End If

        isEmpOnAmtOnly = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select isnull(TSPL_MCC_MASTER.empOnAmountOnly,0) as empOnAmountOnly  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code in (select Location_Code  from TSPL_LOCATION_MASTER where  2=2  " + strWhr + " and Location_Category='MCC' and Rejected_Type='N') ")) = 0, False, True)
        If gvInvoice.Rows.Count > 0 Then
            Is_gv_Rows_Clear = True
            For i As Integer = 0 To gvInvoice.Rows.Count - 1
                gv.Rows.AddNew()
                k = k + 1
                gv.Rows(k).Cells(colSelect).Value = gvInvoice.Rows(i).Cells(colSelect).Value
                gv.Rows(k).Cells(colSlno).Value = gvInvoice.Rows(i).Cells(colSlno).Value
                gv.Rows(k).Cells(colPurchaseInvoiceNo).Value = gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value
                gv.Rows(k).Cells(colPurchaseInvoiceDate).Value = gvInvoice.Rows(i).Cells(colPurchaseInvoiceDate).Value
                gv.Rows(k).Cells(colAPInvoiceNo).Value = gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value
                gv.Rows(k).Cells(colAPInvoiceDate).Value = gvInvoice.Rows(i).Cells(colAPInvoiceDate).Value
                gv.Rows(k).Cells(colVLCUploaderCode).Value = gvInvoice.Rows(i).Cells(colVLCCode).Value
                gv.Rows(k).Cells(colVLCName).Value = gvInvoice.Rows(i).Cells(colVLCName).Value
                gv.Rows(k).Cells(colVLCUploaderCode).Value = gvInvoice.Rows(i).Cells(colVLCCode).Value
                gv.Rows(k).Cells(colMCCCode).Value = gvInvoice.Rows(i).Cells(colMCCCode).Value
                gv.Rows(k).Cells(colRouteCode).Value = gvInvoice.Rows(i).Cells(colRouteCode).Value
                gv.Rows(k).Cells(colRouteName).Value = gvInvoice.Rows(i).Cells(colRouteName).Value
                gv.Rows(k).Cells(colVendorCode).Value = gvInvoice.Rows(i).Cells(colVendorCode).Value
                gv.Rows(k).Cells(colVendorDesc).Value = gvInvoice.Rows(i).Cells(colVendorDesc).Value
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
                gv.Rows(k).Cells(colMilkQty).Value = gvInvoice.Rows(i).Cells(colMilkQty).Value
                gv.Rows(k).Cells(colVSPAmount).Value = gvInvoice.Rows(i).Cells(colInvAmt).Value

                gv.Rows(k).Cells(colMPAmount).Value = gvInvoice.Rows(i).Cells(colMPAmount).Value
                gv.Rows(k).Cells(colMPEMPAmount).Value = gvInvoice.Rows(i).Cells(colMPEMPAmount).Value
                gv.Rows(k).Cells(colMPIncentiveAmount).Value = gvInvoice.Rows(i).Cells(colMPIncentiveAmount).Value
                gv.Rows(k).Cells(colMPEMPIncentiveAmount).Value = gvInvoice.Rows(i).Cells(colMPEMPIncentiveAmount).Value

                gv.Rows(k).Cells(colMPNetAmount).Value = clsCommon.myCDecimal(gvInvoice.Rows(i).Cells(colMPAmount).Value) + clsCommon.myCDecimal(gvInvoice.Rows(i).Cells(colMPEMPAmount).Value) + clsCommon.myCDecimal(gvInvoice.Rows(i).Cells(colMPIncentiveAmount).Value) + clsCommon.myCDecimal(gvInvoice.Rows(i).Cells(colMPEMPIncentiveAmount).Value) ''calculateMPAmount(clsCommon.myCstr(gv.Rows(k).Cells(colVendorCode).Value), dtpFromDate.Value, dtpToDate.Value, fndLoc.Value)
                gv.Rows(k).Cells(colIncenAmt).Value = clsCommon.myCDecimal(gvInvoice.Rows(i).Cells(colIncenAmt).Value)
                gv.Rows(k).Cells(colIncenEmpAmt).Value = clsCommon.myCDecimal(gvInvoice.Rows(i).Cells(colIncenEmpAmt).Value)
                gv.Rows(k).Cells(colInvAmt).Value = clsCommon.myCDecimal(gvInvoice.Rows(i).Cells(colInvAmt).Value)
                gv.Rows(k).Cells(colEmpAmt).Value = clsCommon.myCDecimal(gvInvoice.Rows(i).Cells(colEmpAmt).Value)
                If Not isEmpOnAmtOnly Then
                    gv.Rows(k).Cells(colTotalEmp).Value = clsCommon.myCdbl(gv.Rows(k).Cells(colIncenEmpAmt).Value) + clsCommon.myCdbl(gv.Rows(k).Cells(colEmpAmt).Value)
                Else
                    gv.Rows(k).Cells(colTotalEmp).Value = clsCommon.myCdbl(gv.Rows(k).Cells(colEmpAmt).Value)
                End If
                gv.Rows(k).Cells(colHandlingCharges).Value = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colHandlingCharges).Value)


                gv.Rows(k).Cells(colFATKG).Value = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colFATKG).Value)
                gv.Rows(k).Cells(colFATPer).Value = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colFATPer).Value)
                gv.Rows(k).Cells(colSNFKG).Value = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colSNFKG).Value)
                gv.Rows(k).Cells(colSNFPer).Value = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colSNFPer).Value)


                gv.Rows(k).Cells(colSRNROAmt).Value = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colSRNROAmt).Value)
                gv.Rows(k).Cells(colSRNNetAmount).Value = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colSRNNetAmount).Value)
                gv.Rows(k).Cells(colTDSAmt).Value = clsCommon.myCDecimal(gvInvoice.Rows(i).Cells(colTDSAmt).Value) + getTDSTotalCreditNoteSum(gv.Rows(k).Cells(colVendorCode).Value)

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
                gv.Rows(k).Cells(colTotalSavingAmount).Value = getTotalSavingSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colTotalCompulsoryAmount).Value = getTotalCompulsorySum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colItemIssueTotalAmount).Value = getTotalItemIssueSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colItemIssueReturnTotalAmount).Value = getTotalItemIssueReturnSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colDeductionTotalAmount).Value = getTotalDeductionSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colAssetLostAmount).Value = getTotalAssetLost(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colServiceChargeAmt).Value = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colServiceChargeAmt).Value)

                gv.Rows(k).Cells(colPaybleAmt).Value = (((gv.Rows(k).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value - gv.Rows(k).Cells(colTDSAmt).Value + gv.Rows(k).Cells(colTotalCreditNoteAmount).Value + gv.Rows(k).Cells(colVSPOwnSystemAmt).Value + gv.Rows(k).Cells(colHeadLoadAmt).Value) - gv.Rows(k).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(k).Cells(colVendorCode).Value) + getTotalAssetLost(gv.Rows(k).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(k).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(k).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(k).Cells(colReduceDeduc).Value) + getTotalMccSaleReturnSum(gv.Rows(k).Cells(colVendorCode).Value) + getTotalItemIssueReturnSum(gv.Rows(k).Cells(colVendorCode).Value)
                CalculateAdvanceKnockOff(k)

                If SettVSPHoldPaymentNotCompanyBank Then
                    Dim qry As String = "select TSPL_VENDOR_MASTER.Company_Bank_Current,TSPL_BANK_MASTER_CURRENT.DESCRIPTION as DESCRIPTION_Current,TSPL_VENDOR_MASTER.Company_Bank,TSPL_BANK_MASTER_SAVING.DESCRIPTION as DESCRIPTION_Saving 
from TSPL_VENDOR_MASTER 
left outer join TSPL_BANK_MASTER as TSPL_BANK_MASTER_CURRENT on TSPL_BANK_MASTER_CURRENT.BANK_CODE=TSPL_VENDOR_MASTER.Company_Bank_Current
left outer join TSPL_BANK_MASTER as TSPL_BANK_MASTER_SAVING on TSPL_BANK_MASTER_SAVING.BANK_CODE=TSPL_VENDOR_MASTER.Company_Bank 
where TSPL_VENDOR_MASTER.Vendor_Code='" + gv.Rows(k).Cells(colVendorCode).Value + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        If clsCommon.myLen(dt.Rows(0)("Company_Bank_Current")) > 0 Then
                            gv.Rows(i).Cells(colBankCode).Value = clsCommon.myCstr(dt.Rows(0)("Company_Bank_Current"))
                            gv.Rows(i).Cells(colBankDesc).Value = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION_Current"))
                            gv.Rows(i).Cells(colPayMode).Value = "NEFT"
                        Else
                            gv.Rows(i).Cells(colIsPaymentProcessHold).Value = True
                        End If

                        If clsCommon.myLen(dt.Rows(0)("Company_Bank")) > 0 Then
                            gv.Rows(i).Cells(colBankCodeSaving).Value = clsCommon.myCstr(dt.Rows(0)("Company_Bank"))
                            gv.Rows(i).Cells(colBankDescSaving).Value = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION_Saving"))
                            gv.Rows(i).Cells(colPayModeSaving).Value = "NEFT"
                        Else
                            gv.Rows(i).Cells(colIsPaymentProcessHoldSavingAuto).Value = True
                        End If
                        SetPaymentProcessHoldSaving(i)
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

        AddSummary()

        isCellValueChanged = False
        If PayableAmountZeroForMCCSale Then
            For ii As Integer = 0 To gv.Rows.Count - 1
                Dim strVSPCode As String = clsCommon.myCstr(gv.Rows(ii).Cells(colVendorCode).Value)
                Dim dblAmt As Decimal = Math.Round(clsCommon.myCdbl(gv.Rows(ii).Cells(colPaybleAmt).Value), 2, MidpointRounding.AwayFromZero)
                ''1. Advance
                ''Handled on above
                ''2 MCC Sale
                If dblAmt < 0 Then ''IF amount negative
                    dblAmt = Math.Abs(dblAmt)
                    For jj As Integer = gvMccSale.Rows.Count - 1 To 0 Step -1
                        If clsCommon.CompairString(strVSPCode, clsCommon.myCstr(gvMccSale.Rows(jj).Cells(colCustomerCode).Value)) = CompairStringResult.Equal Then
                            If clsCommon.myCBool(gvMccSale.Rows(jj).Cells(colSelect).Value) Then
                                gvMccSale.CurrentColumn = gvMccSale.Columns(colReduceDeduc)
                                gvMccSale.CurrentRow = gvMccSale.Rows(jj)
                                If dblAmt > clsCommon.myCdbl(gvMccSale.Rows(jj).Cells(colItemAmt).Value) Then
                                    gvMccSale.Rows(jj).Cells(colReduceDeduc).Value = Math.Round(clsCommon.myCdbl(gvMccSale.Rows(jj).Cells(colItemAmt).Value), 2, MidpointRounding.AwayFromZero)
                                    dblAmt -= Math.Round(clsCommon.myCdbl(gvMccSale.Rows(jj).Cells(colItemAmt).Value), 2, MidpointRounding.AwayFromZero)
                                Else
                                    gvMccSale.Rows(jj).Cells(colReduceDeduc).Value = dblAmt
                                    dblAmt = 0
                                    Exit For
                                End If
                            Else
                                gvMccSale.Rows(jj).Cells(colReduceDeduc).Value = 0
                            End If
                        End If
                    Next
                    ''3 Debit Note
                    If dblAmt <> 0 Then ''IF amount negative
                        For jj As Integer = gvDeduction.Rows.Count - 1 To 0 Step -1
                            If clsCommon.CompairString(strVSPCode, clsCommon.myCstr(gvDeduction.Rows(jj).Cells(colVendorCode).Value)) = CompairStringResult.Equal Then
                                If clsCommon.myCBool(gvDeduction.Rows(jj).Cells(colSelect).Value) Then
                                    gvDeduction.CurrentColumn = gvDeduction.Columns(colReduceDeduc)
                                    gvDeduction.CurrentRow = gvDeduction.Rows(jj)
                                    If dblAmt > clsCommon.myCdbl(gvDeduction.Rows(jj).Cells(colItemAmt).Value) Then
                                        gvDeduction.Rows(jj).Cells(colReduceDeduc).Value = Math.Round(clsCommon.myCdbl(gvDeduction.Rows(jj).Cells(colItemAmt).Value), 2, MidpointRounding.AwayFromZero)
                                        dblAmt -= Math.Round(clsCommon.myCdbl(gvDeduction.Rows(jj).Cells(colItemAmt).Value), 2, MidpointRounding.AwayFromZero)
                                    Else
                                        gvDeduction.Rows(jj).Cells(colReduceDeduc).Value = dblAmt
                                        dblAmt = 0
                                        Exit For
                                    End If
                                Else
                                    gvDeduction.Rows(jj).Cells(colReduceDeduc).Value = 0
                                End If
                            End If
                        Next
                    End If
                    ''End of Debit Note
                End If
                ''End of MCC Sale
            Next
        End If
        AddCompularyAmtInPaybleAmount(-1)
    End Sub
    Sub AddCompularyAmtInPaybleAmount(ByVal indx As Integer)
        If indx < 0 Then
            For ii As Integer = 0 To gv.Rows.Count - 1
                gv.Rows(ii).Cells(colPaybleAmt).Value += clsCommon.myCDecimal(gv.Rows(ii).Cells(colTotalCompulsoryAmount).Value)
            Next
        End If
    End Sub

    Sub CalculateAdvanceKnockOff(ByVal k As Integer)
        If isConsiderAdvancePayment Then
            gv.Rows(k).Cells(colAdvanceKnockOffAmount).Value = 0
            gv.Rows(k).Cells(colAdvanceAmount).Value = 0
            Dim TotAdvanceAmount As Double = getTotalAdvancePayment(clsCommon.myCstr(gv.Rows(k).Cells(colVendorCode).Value))
            Dim PayableAmount As Double = clsCommon.myCdbl(gv.Rows(k).Cells(colPaybleAmt).Value)
            If TotAdvanceAmount > 0 Then
                gv.Rows(k).Cells(colAdvanceAmount).Value = TotAdvanceAmount
                If PayableAmount > 0 Then
                    If TotAdvanceAmount > PayableAmount Then
                        gv.Rows(k).Cells(colAdvanceKnockOffAmount).Value = Math.Round(PayableAmount, 2)
                        gv.Rows(k).Cells(colPaybleAmt).Value = 0
                    ElseIf TotAdvanceAmount < PayableAmount Then
                        gv.Rows(k).Cells(colPaybleAmt).Value = Math.Round(PayableAmount - TotAdvanceAmount, 2, MidpointRounding.ToEven)
                        gv.Rows(k).Cells(colAdvanceKnockOffAmount).Value = Math.Round(TotAdvanceAmount, 2, MidpointRounding.ToEven)
                    ElseIf TotAdvanceAmount = PayableAmount Then
                        gv.Rows(k).Cells(colPaybleAmt).Value = 0
                        gv.Rows(k).Cells(colAdvanceKnockOffAmount).Value = Math.Round(TotAdvanceAmount, 2, MidpointRounding.ToEven)
                    End If
                    ''Start comment by balwinder on 30/01/2020 becuase it Payment with -ve amount PPNo-PYPR304/1920/000014,VSP-VSP/002519
                    'ElseIf PayableAmount < 0 Then 
                    'PayableAmount = Math.Abs(PayableAmount)
                    'If TotAdvanceAmount >= PayableAmount Then
                    '    gv.Rows(k).Cells(colAdvanceKnockOffAmount).Value = Math.Round(PayableAmount, 2)
                    '    gv.Rows(k).Cells(colPaybleAmt).Value = 0
                    'End If
                    ''End
                End If
            End If
        End If
    End Sub

    Private Function calculateMPAmount(ByVal strVSPCode As String, ByVal dtFrom As DateTime, ByVal dtTo As DateTime, ByVal strMCC As String) As Double
        Dim retVAL As Double = 0
        Dim qry As String = "select 1 from TSPL_VLC_MAPPING_FOR_MP_MILK_AMOUNT left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_VLC_MAPPING_FOR_MP_MILK_AMOUNT.VLC_Code where TSPL_VLC_MASTER_HEAD.VSP_Code='" + strVSPCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            qry = "select sum(Amount) as Amount from (" + Environment.NewLine +
            " Select TSPL_VLC_DATA_UPLOADER.Amount " + Environment.NewLine +
            " from TSPL_VLC_DATA_UPLOADER " + Environment.NewLine +
            " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.VLC_CODE and TSPL_VLC_MASTER_HEAD.MCC=TSPL_VLC_DATA_UPLOADER.MCC_Code" + Environment.NewLine +
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VLC_MASTER_HEAD.MCC " + Environment.NewLine +
            " where File_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm tt") + "' and File_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_LOCATION_MASTER.Loc_Segment_Code='" + strMCC + "' and TSPL_VLC_MASTER_HEAD.VSP_Code='" + strVSPCode + "' " + Environment.NewLine +
            " union all" + Environment.NewLine +
            " select  TSPL_VLC_DATA_UPLOADER_DETAIL.Amount from TSPL_VLC_DATA_UPLOADER_DETAIL" + Environment.NewLine +
            " left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code=TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code" + Environment.NewLine +
            " left Outer Join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code= TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code" + Environment.NewLine +
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VLC_MASTER_HEAD.MCC" + Environment.NewLine +
            " where TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_LOCATION_MASTER.Loc_Segment_Code='" + strMCC + "'" + Environment.NewLine +
            " and TSPL_VLC_MASTER_HEAD.VSP_Code='" + strVSPCode + "'" + Environment.NewLine +
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
        Dim dr As DataRow = clsLocation.getLocSegFinderFullRow(whrCls)
        If dr Is Nothing OrElse dr.ItemArray.Count <= 0 Then
            fndLoc.Value = ""
            txtMCC.Text = ""
            lblMCC.Text = ""
            txtLocName.Text = ""
            Exit Sub
        End If

        fndLoc.Value = clsCommon.myCstr(dr("LocationSegmentCode"))
        txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndLoc.Value & "' "))
        If SettShowMCCFinder Then
            txtMCC.Text = clsCommon.myCstr(dr("Code"))
            lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where mcc_Code='" + txtMCC.Text + "'"))
        End If



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
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select isnull(TSPL_MCC_MASTER.empOnAmountOnly,0) as empOnAmountOnly,TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code in (select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")
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
                        clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        Exit Sub
                    End If
                    dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
                ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                        clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
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
            If clsCommon.myLen(txtMCC.Text) > 0 Then
                txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Text, dtpToDate.Value)
                txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Text, dtpToDate.Value)
            Else
                txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(fndLoc.Value, dtpToDate.Value)
                txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(fndLoc.Value, dtpToDate.Value)
            End If
        End If
    End Sub

    Private Sub gvInvoice_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvInvoice.CellValueChanged
        If Not isLoad AndAlso e.Column Is gvInvoice.Columns(colSelect) Then
            isLoad = True
            getMCCs()
            getVendors()
            LoadDeductionGridData()
            LoadMccSaleGridData()
            LoadMccSaleReturnGridData()
            LoadItemIssueGridData()
            LoadCreditNoteGridData()
            'LoadBlankGridGV()
            'loadGvData()
            isLoad = False
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(fndDocNo.Value))
        Try
            If SettVSPHoldPaymentNotCompanyBank = False Then
                If Not AllowToSave() Then
                    Exit Sub
                End If
                GC.Collect()
                GC.WaitForPendingFinalizers()
                SaveData(True)
            End If
            If clsCommon.MyMessageBoxShow("Continue to Post the payment ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                clsPaymentProcessHead.PrePostData(fndDocNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Payment Processed", Me.Text)
                LoadData(fndDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        PaymentProcess()
    End Sub

    Private Sub fndDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocNo._MYValidating

        fndDocNo.Value = clsPaymentProcessHead.getFinder("FarmType='PP'", fndDocNo.Value, isButtonClicked)
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
                        gv.Rows(rownummain).Cells(colPaybleAmt).Value = (((gv.Rows(rownummain).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value - gv.Rows(rownummain).Cells(colTDSAmt).Value + gv.Rows(rownummain).Cells(colTotalCreditNoteAmount).Value + gv.Rows(rownummain).Cells(colVSPOwnSystemAmt).Value + gv.Rows(rownummain).Cells(colHeadLoadAmt).Value) - gv.Rows(rownummain).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(rownummain).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.Rows(rownummain).Cells(colVendorCode).Value)
                        CalculateAdvanceKnockOff(rownummain)
                        AddCompularyAmtInPaybleAmount(rownummain)
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
                        gv.Rows(rownummain).Cells(colPaybleAmt).Value = (((gv.Rows(rownummain).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value - gv.Rows(rownummain).Cells(colTDSAmt).Value + gv.Rows(rownummain).Cells(colTotalCreditNoteAmount).Value + gv.Rows(rownummain).Cells(colVSPOwnSystemAmt).Value + gv.Rows(rownummain).Cells(colHeadLoadAmt).Value) - gv.Rows(rownummain).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(rownummain).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.Rows(rownummain).Cells(colVendorCode).Value)
                        CalculateAdvanceKnockOff(rownummain)
                        AddCompularyAmtInPaybleAmount(rownummain)
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
                            gv.Rows(rownummain).Cells(colPaybleAmt).Value = (((gv.Rows(rownummain).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value - gv.Rows(rownummain).Cells(colTDSAmt).Value + gv.Rows(rownummain).Cells(colTotalCreditNoteAmount).Value + gv.Rows(rownummain).Cells(colVSPOwnSystemAmt).Value + gv.Rows(rownummain).Cells(colHeadLoadAmt).Value) - gv.Rows(rownummain).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(rownummain).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.Rows(rownummain).Cells(colVendorCode).Value)
                            CalculateAdvanceKnockOff(rownummain)
                            AddCompularyAmtInPaybleAmount(rownummain)
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
                        gv.Rows(rownummain).Cells(colPaybleAmt).Value = (((gv.Rows(rownummain).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value - gv.Rows(rownummain).Cells(colTDSAmt).Value + gv.Rows(rownummain).Cells(colTotalCreditNoteAmount).Value + gv.Rows(rownummain).Cells(colVSPOwnSystemAmt).Value + gv.Rows(rownummain).Cells(colHeadLoadAmt).Value) - gv.Rows(rownummain).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(rownummain).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.Rows(rownummain).Cells(colVendorCode).Value)
                        CalculateAdvanceKnockOff(rownummain)
                        AddCompularyAmtInPaybleAmount(rownummain)
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

            If e.Column Is gv.Columns(colAssetLostAmount) Then
                Dim frm As New FrmFreeGrid
                frm.dt = getAssetLostList(gv.CurrentRow.Cells(colVendorCode).Value)
                If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No data Found", Me.Text)
                    Exit Sub
                End If
                frm.strFormName = "Asset Lost List For VSP: " & gv.CurrentRow.Cells(colVendorCode).Value
                frm.WindowState = FormWindowState.Maximized
                frm.ReportID = "AssetLostDe"
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
                    Dim frm As New FrmAPInvoiceEntry
                    frm.SetUserMgmt(clsUserMgtCode.mbtnAPInvoiceEntry)
                    frm.strAPInvoice = gv.CurrentRow.Cells(colAPInvoiceNo).Value
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()
                End If
            End If
            If e.Column Is gv.Columns(colPurchaseInvoiceNo) Then
                If clsCommon.myLen(gv.CurrentRow.Cells(colPurchaseInvoiceNo).Value) > 0 Then
                    Dim frm As New frmMilkPurchaseInvoiceMCC
                    frm.SetUserMgmt(clsUserMgtCode.frmMilkPurchaseInvoice)
                    frmMilkPurchaseInvoiceMCC.strDocumentNo = gv.CurrentRow.Cells(colPurchaseInvoiceNo).Value
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()
                End If
            End If

        End If
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isLoad Then
            isLoad = True
            If (e.Column Is gv.Columns(colReduceDeduc) OrElse e.Column Is gv.Columns(colMccSaleTotalAmount) OrElse e.Column Is gv.Columns(colMccSaleReturnTotalAmount) OrElse e.Column Is gv.Columns(colItemIssueTotalAmount) OrElse e.Column Is gv.Columns(colItemIssueReturnTotalAmount) OrElse e.Column Is gv.Columns(colDeductionTotalAmount) OrElse e.Column Is gv.Columns(colAssetLostAmount)) AndAlso gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
                gv.CurrentRow.Cells(colPaybleAmt).Value = (((gv.CurrentRow.Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value - gv.CurrentRow.Cells(colTDSAmt).Value + gv.CurrentRow.Cells(colTotalCreditNoteAmount).Value + gv.CurrentRow.Cells(colVSPOwnSystemAmt).Value + gv.CurrentRow.Cells(colHeadLoadAmt).Value) - gv.CurrentRow.Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.CurrentRow.Cells(colVendorCode).Value) + getTotalAssetLost(gv.CurrentRow.Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.CurrentRow.Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.CurrentRow.Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.CurrentRow.Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.CurrentRow.Cells(colVendorCode).Value)
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

        If RadPageView1.SelectedPage.Text = "Payment Process" Then
            If gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
                gv.Columns(colSelect).IsVisible = False
                Dim arr As List(Of String) = New List(Of String)
                arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
                arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
                clsCommon.MyExportToExcelGrid("Payment Process Details", gv, arr, "Payment Process")
                'clsCommon.MyExportToExcelGrid(Nothing, gv, arr, Nothing)
                'clsCommon.MyExportToExcelGrid("", gv, arr, "")
                gv.Columns(colSelect).IsVisible = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to export", Me.Text)
            End If
        End If

        If RadPageView1.SelectedPage.Text = "Invoices" Then
            If gvInvoice IsNot Nothing AndAlso gvInvoice.Rows.Count > 0 Then
                gvInvoice.Columns(colSelect).IsVisible = False
                Dim arr As List(Of String) = New List(Of String)
                arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
                arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
                clsCommon.MyExportToExcelGrid("Invoices Details", gvInvoice, arr, "Invoices")
                gvInvoice.Columns(colSelect).IsVisible = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to export", Me.Text)
            End If
        End If

        If RadPageView1.SelectedPage.Text = "MCC Sale" Then
            If gvMccSale IsNot Nothing AndAlso gvMccSale.Rows.Count > 0 Then
                gvMccSale.Columns(colSelect).IsVisible = False
                Dim arr As List(Of String) = New List(Of String)
                arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
                arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
                clsCommon.MyExportToExcelGrid("MCC Sale Details", gvMccSale, arr, "MCC Sale")
                gvMccSale.Columns(colSelect).IsVisible = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to export", Me.Text)
            End If
        End If

        If RadPageView1.SelectedPage.Text = "MCC Sale Return" Then
            If GvMccSaleReturn IsNot Nothing AndAlso GvMccSaleReturn.Rows.Count > 0 Then
                GvMccSaleReturn.Columns(colSelect).IsVisible = False
                Dim arr As List(Of String) = New List(Of String)
                arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
                arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
                clsCommon.MyExportToExcelGrid("MCC Sale Return Details", GvMccSaleReturn, arr, "MCC Sale Return")
                GvMccSaleReturn.Columns(colSelect).IsVisible = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to export", Me.Text)
            End If
        End If

        If RadPageView1.SelectedPage.Text = "Item Issue" Then
            If gvItemIssue IsNot Nothing AndAlso gvItemIssue.Rows.Count > 0 Then
                gvItemIssue.Columns(colSelect).IsVisible = False
                Dim arr As List(Of String) = New List(Of String)
                arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
                arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
                clsCommon.MyExportToExcelGrid("Item Issue Details", gvItemIssue, arr, "Item Issue")
                gvItemIssue.Columns(colSelect).IsVisible = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to export", Me.Text)
            End If
        End If

        If RadPageView1.SelectedPage.Text = "Item Issue Return" Then
            If gvItemIssueReturn IsNot Nothing AndAlso gvItemIssueReturn.Rows.Count > 0 Then
                gvItemIssueReturn.Columns(colSelect).IsVisible = False
                Dim arr As List(Of String) = New List(Of String)
                arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
                arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
                clsCommon.MyExportToExcelGrid("Item Issue Return Details", gvItemIssueReturn, arr, "Item Issue Return")
                gvItemIssueReturn.Columns(colSelect).IsVisible = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to export", Me.Text)
            End If
        End If

        If RadPageView1.SelectedPage.Text = "Deductions" Then
            If gvDeduction IsNot Nothing AndAlso gvDeduction.Rows.Count > 0 Then
                gvDeduction.Columns(colSelect).IsVisible = False
                Dim arr As List(Of String) = New List(Of String)
                arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
                arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
                clsCommon.MyExportToExcelGrid("Deduction Details", gvDeduction, arr, "Deduction")
                gvDeduction.Columns(colSelect).IsVisible = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found To Export", Me.Text)
            End If
        End If

        If RadPageView1.SelectedPage.Text = "Credit Note" Then
            If gvCreditNote IsNot Nothing AndAlso gvCreditNote.Rows.Count > 0 Then
                gvCreditNote.Columns(colSelect).IsVisible = False
                Dim arr As List(Of String) = New List(Of String)
                arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
                arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
                clsCommon.MyExportToExcelGrid("Credit Note Details", gvCreditNote, arr, "Credit Note")
                gvCreditNote.Columns(colSelect).IsVisible = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found To Export", Me.Text)
            End If
        End If

        If RadPageView1.SelectedPage.Text = "Advance Payment" Then
            If gvAdvancePayment IsNot Nothing AndAlso gvAdvancePayment.Rows.Count > 0 Then
                gvAdvancePayment.Columns(colSelect).IsVisible = False
                Dim arr As List(Of String) = New List(Of String)
                arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
                arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
                clsCommon.MyExportToExcelGrid("Advance Payment Details", gvAdvancePayment, arr, "Advance Payment")
                gvAdvancePayment.Columns(colSelect).IsVisible = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to export", Me.Text)
            End If
        End If

        If RadPageView1.SelectedPage.Text = "Asset Lost" Then
            If gvAssetLost IsNot Nothing AndAlso gvAssetLost.Rows.Count > 0 Then
                gvAssetLost.Columns(colSelect).IsVisible = False
                Dim arr As List(Of String) = New List(Of String)
                arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
                arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
                clsCommon.MyExportToExcelGrid("Asset Lost Details", gvAssetLost, arr, "Asset Lost")
                gvAssetLost.Columns(colSelect).IsVisible = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to export", Me.Text)
            End If
        End If

        If RadPageView1.SelectedPage.Text = "Saving" Then
            If gvSaving IsNot Nothing AndAlso gvSaving.Rows.Count > 0 Then
                gvSaving.Columns(colSelect).IsVisible = False
                Dim arr As List(Of String) = New List(Of String)
                arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
                arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
                clsCommon.MyExportToExcelGrid("Saving Details", gvSaving, arr, "Saving")
                gvSaving.Columns(colSelect).IsVisible = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found To Export", Me.Text)
            End If
        End If

        If RadPageView1.SelectedPage.Text = "Compulsory" Then
            If gvCompulsory IsNot Nothing AndAlso gvCompulsory.Rows.Count > 0 Then
                gvCompulsory.Columns(colSelect).IsVisible = False
                Dim arr As List(Of String) = New List(Of String)
                arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
                arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
                'clsCommon.MyExportToExcelGrid("Compulsory Details", gvCompulsory, arr, "Compulsory")
                clsCommon.MyExportToExcelGrid(Nothing, gvCompulsory, arr, Nothing)
                gvCompulsory.Columns(colSelect).IsVisible = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to export", Me.Text)
            End If
        End If

    End Sub

    Function getVLCNameByVSPCode(ByVal vsp_code As String, Optional trans As SqlTransaction = Nothing) As String
        Dim rValue As String = String.Empty
        Dim qry As String = String.Empty
        Try
            qry = "select VLC_Name  from TSPL_VLC_MASTER_HEAD  where VSP_Code ='" & vsp_code & "'"
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
                    '' done by Panch Raj against ticket no:BM00000008937
                    '' unselect mcc sale trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvMccSale.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next
                    '' unselect mcc sale return trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In GvMccSaleReturn.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
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
                    '' done by Panch Raj against ticket no:BM00000008937
                    '' unselect mcc sale trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvMccSale.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next
                    '' unselect mcc sale return trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In GvMccSaleReturn.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
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
        'If e.Column Is gvItemIssueReturn.Columns(colReduceDeduc) Then
        '    Dim Rownum As Integer = 0
        '    Dim rownummain As Integer = 0
        '    Try
        '        If Not isCellValueChanged Then
        '            isCellValueChanged = True
        '            If gvItemIssueReturn.CurrentRow.Cells(colReduceDeduc).Value > gvItemIssueReturn.CurrentRow.Cells(colItemAmt).Value Then
        '                clsCommon.MyMessageBoxShow("Item Issue Return : Reduce Deduction can not be more than invoice amount at line no: " & (e.RowIndex + 1) & "")
        '                gvItemIssueReturn.CurrentRow.Cells(colReduceDeduc).Value = 0
        '                isCellValueChanged = False
        '                Exit Sub
        '            End If
        '            Rownum = getInvoiceRowNo(gvItemIssueReturn.CurrentRow.Cells(colVendorCode).Value)
        '            rownummain = getMainRowNo(gvItemIssueReturn.CurrentRow.Cells(colVendorCode).Value)
        '            If Rownum <> -1 Then
        '                gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value = getTotalDeductionReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalMccSaleReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalItemIssueReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value)
        '                If rownummain <> -1 Then
        '                    gv.Rows(rownummain).Cells(colReduceDeduc).Value = gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value
        '                    gv.Rows(rownummain).Cells(colPaybleAmt).Value = (((gv.Rows(rownummain).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.Rows(rownummain).Cells(colTotalCreditNoteAmount).Value + gv.Rows(rownummain).Cells(colVSPOwnSystemAmt).Value + gv.Rows(rownummain).Cells(colHeadLoadAmt).Value) - gv.Rows(rownummain).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(rownummain).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.Rows(rownummain).Cells(colVendorCode).Value)
        '                End If
        '            End If
        '            isCellValueChanged = False
        '        End If

        '    Catch ex As Exception
        '    End Try
        'Else
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
            If MultipleFinderFillAuto Then
                AutoFillAllVSP()
            End If
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then
            If clsCommon.myLen(fndLoc.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                clsCommon.MyMessageBoxShow(Me, "Please select the Location first", Me.Text)
                Exit Sub
            End If
            If MultipleFinderFillAuto = True Then
                If mfndMcc.arrValueMember Is Nothing OrElse mfndMcc.arrValueMember.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please select the Location first", Me.Text)
                    Exit Sub
                End If
            End If
            Dim strMCCcode = ""
            If MultipleFinderFillAuto Then
                strMCCcode = " location_Code in ( " + clsCommon.GetMulcallString(mfndMcc.arrValueMember) + ")  "
            Else
                strMCCcode = " Loc_Segment_Code = '" & fndLoc.Value & "'  "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in (select Location_Code  from TSPL_LOCATION_MASTER where " + strMCCcode + " and Location_Category='MCC' and Rejected_Type='N') ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If dtpFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
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
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type")
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
            If clsCommon.myLen(txtMCC.Text) > 0 Then
                txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Text, dtpToDate.Value)
                txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Text, dtpToDate.Value)
            Else
                txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(fndLoc.Value, dtpToDate.Value)
                txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(fndLoc.Value, dtpToDate.Value)
            End If
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
            Dim strWhr As String = ""
            If clsCommon.myLen(fndLoc.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                fndLoc.Focus()
                Throw New Exception("Please select Location")
            End If
            If MultipleFinderFillAuto Then
                Dim dtSeg As DataTable = clsDBFuncationality.GetDataTable("select distinct Loc_Segment_Code from TSPL_LOCATION_MASTER where location_code in (" + clsCommon.GetMulcallString(mfndMcc.arrValueMember) + ")")
                If dtSeg IsNot Nothing AndAlso dtSeg.Rows.Count > 0 Then
                    Dim ArrSeg As New ArrayList
                    For Each drSeg As DataRow In dtSeg.Rows
                        ArrSeg.Add(drSeg("Loc_Segment_Code"))
                    Next
                    strWhr = " and TSPL_LOCATION_MASTER.Loc_Segment_Code in ( " + clsCommon.GetMulcallString(ArrSeg) + ") "
                Else
                    strWhr = "  and 2=3 "
                End If
            Else
                strWhr = " and TSPL_LOCATION_MASTER.Loc_Segment_Code ='" + fndLoc.Value + "' "
            End If

            Dim qry As String = "select xx.VSP_CODE as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,xx.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name from (" + Environment.NewLine +
            " select VSP_CODE,max(VLC_CODE)as VLC_CODE from (" + Environment.NewLine +
            " select VSP_CODE,VLC_CODE from TSPL_MILK_SRN_Head left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MILK_SRN_Head.MCC_CODE where 2=2 " + strWhr + " "
            If Not isPickPendingMilkSRNinNextPaymentCycle Then
                qry += " and DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "
            End If
            qry += " and DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine
            If SettShowMCCFinder AndAlso MultipleFinderFillAuto Then
                If clsCommon.myLen(txtMCC.Text) <= 0 Then
                    fndLoc.Focus()
                    Throw New Exception("Please select MCC")
                End If
                qry += " and TSPL_MILK_SRN_Head.MCC_Code='" + txtMCC.Text + "'"
            End If
            qry += " )xxx group by VSP_CODE " + Environment.NewLine +
            " )xx " + Environment.NewLine +
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.VSP_CODE " + Environment.NewLine +
            " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=xx.VLC_CODE " + Environment.NewLine +
            " where TSPL_VENDOR_MASTER.VSP_Farmer_Billing=0 and isnull(TSPL_VENDOR_MASTER.is_Drip_Saver,'')<>'Y' order by xx.VSP_CODE"
            '" where TSPL_VENDOR_MASTER.is_Hold_Payment_Process=0 " + Environment.NewLine +


            txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "PPfPVLF", qry, "Code", "", txtVSP.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Public Function Load_Report(ByVal Location As String, ByVal dFromDate As Date, ByVal dToDate As Date, ByVal ListOfVSPs As ArrayList, ByVal otherCls As Boolean, ByVal PrintOpen As Boolean) As DataTable

        '======================update by preeti gupta ticket no [ERO/08/05/19-000593]
        '' work done on print for Milan Dairy against ticket No. MIL/04/05/18-000018
        Try

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
                    whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN ('" + Location + "') "
                End If

                whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dFromDate + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dToDate + "'),103) "
                whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(ListOfVSPs) & ")"
                If clsCommon.myLen(Location) > 0 Then
                    whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN ('" + Location + "') "
                End If
                whrclsItemWise += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dFromDate + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dToDate + "'),103) "

                whrclsItemWise += " and final.Customer_CODE in ( " & clsCommon.GetMulcallString(ListOfVSPs) & ")"
                whrclsItemWise += " and TSPL_PAYMENT_PROCESS_head.Loc_Seg_Code IN ('" + Location + "')"


            Else
                whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
                whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
                If clsCommon.myLen(fndLoc.Value) > 0 Then
                    whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN ('" + fndLoc.Value + "') "
                End If

                whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
                whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
                whrcls1 += "  and TSPL_PAYMENT_PROCESS_Head.doc_no='" + fndDocNo.Value + "'"
                If clsCommon.myLen(fndLoc.Value) > 0 Then
                    whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN ('" + fndLoc.Value + "') "
                End If
                whrclsItemWise += " and final.doc_no in ('" + fndDocNo.Value + "')"
                whrclsItemWise += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
                'whrclsItemWise += " and final.Customer_CODE in  ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
                'whrclsItemWise += " and TSPL_PAYMENT_PROCESS_head.Loc_Seg_Code IN (" + fndLoc.Value + ")"
            End If
            sQuery = "select "
            '' add column fat kg and snf kg for Swadesh 07/12/2017
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "001") = CompairStringResult.Equal Then
                sQuery += "   TSPL_MILK_SRN_DETAIL.FAT_KG,TSPL_MILK_SRN_DETAIL.SNF_KG, "
            End If
            ' Ticket No : MIL/16/01/19-000030 by prabhakar add Rate_Head_Load column for print
            ' Ticket No : MIL/15/01/19-000029 by prabhakar for print work
            '' End
            'sQuery += " select  '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate ,''  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,convert(varchar,DOC_DATE,103) as DOC_DATE,SHIFT,TYPE,SAMPLE_NO,convert(decimal(18,2),NewQty)as NewQty ,convert(decimal(18,1),FAT_PER) as FAT_PER ,convert(decimal(18,1),SNF_PER) as SNF_PER  ,convert(decimal(18,1),CLR)as CLR ,convert(decimal(18,2),(FAT_PER * NewQty)/100 )as TFAT,convert(decimal(18,2),(SNF_PER * NewQty)/100) as TSNF,convert(decimal(18,2),Amount/NewQty) as RATE  ,convert(decimal(18,2),Amount)as Amount ,MCC_CODE,VLC_Code ,VLC_Code_VLC_Uploader,emp,incentive,HEDAmt,AstAMT,DedAmt,coalesce(sale_AMt,0) as sale_AMt,VSP_CODE, ISNULL(Deduction_Debit_Amount,0) as Deduction_Debit_Amount, ISNULL(Deduction_MCC_Sale_Amount,0) as Deduction_MCC_Sale_Amount, ISNULL(Deduction_MCC_Sale_Return_Amount,0) as Deduction_MCC_Sale_Return_Amount, ISNULL(Deduction_Item_Issue_Amount,0) as Deduction_Item_Issue_Amount, ISNULL(Deduction_CREDIT_Amount,0) as Deduction_CREDIT_Amount,ISNULL(Issue_Return_Amount,0) as Issue_Return_Amount,Total_Basic_AMOUNT from( "
            'sQuery += " select addd,DOC_DATE,UOM_Code,Qty as NewQty, Qty,RATE,Net_AMOUNT as Amount ,MCC_CODE+' -'+MCC_NAME+'  QtyMode :'+UOM_Code    as MCC_CODE ,VSP_CODE ,SHIFT,ROUTE_CODE+' -'+Route_Name as ROUTE_CODE  ,Vendor_Name ,MCC_NAME,SAMPLE_NO ,TYPE ,CLR,FAT_PER ,SNF_PER ,VLC_Code_VLC_Uploader+' - '+VLC_Name as VLC_Code_VLC_Uploader, VLC_Code,emp,incentive,HEDAmt,AstAMT,DedAmt,sale_AMt,Deduction_Debit_Amount, Deduction_MCC_Sale_Amount, Deduction_MCC_Sale_Return_Amount, Deduction_Item_Issue_Amount, Deduction_CREDIT_Amount,Issue_Return_Amount,Total_Basic_AMOUNT from "
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GMD") = CompairStringResult.Equal Then
                sQuery += " isnull(TSPL_VENDOR_MASTER.Actual_charges,0) as Actual_charges,isnull (TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load ,isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount,0) as Handling_Charges_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(MCC_City.city_code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then ', '+ MCC_State.STATE_NAME else '' end  as MCC_address, "
            Else
                sQuery += " isnull(TSPL_VENDOR_MASTER.Actual_charges,0) as Actual_charges,isnull (TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load ,isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount,0) as Handling_Charges_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, "
            End If
            If otherCls Then
                sQuery += "    '" & dFromDate & "'  as fromDate ,'" & dToDate & "'  as Todate"
            Else
                sQuery += "    '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate"
            End If
            sQuery += " ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,coalesce(PaymentProcess.Total_EMP_Amount,0) as Total_EMP_Amount,coalesce(PaymentProcess.Incentive_Amount,0) as Incentive_Amount ,coalesce(PaymentProcess.Incentive_EMP_Amount,0) as Incentive_EMP_Amount ,coalesce(PaymentProcess.EMP_Amount,0) as EMP_Amount ,coalesce(PaymentProcess.Vsp_Own_System_Amount,0) as Vsp_Own_System_Amount ,coalesce(PaymentProcess.Head_Load_Amount,0) as Head_Load_Amount ,coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0)*(-1) as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty  ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER ,"
            If objCommonVar.PricePlan = 4 Then
                sQuery += "round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as FATQTY," +
            "round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as SNFQTY"
            Else
                sQuery += "(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as FATQTY," +
            "(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as SNFQTY "
            End If

            sQuery += " ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER, case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  Price_Chart.milk_rate end as Standard_Rate,TSPL_MILK_PURCHASE_INVOICE_DETAIL.RATE as RATE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_RO_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_SRN_head.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,case when isnull(TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'')='' then TSPL_MILK_SRN_HEAD.shift else TSPL_MILK_SRN_HEAD.shift end as SHIFT,"
            sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,case when isnull(TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type,'')='' then 'Mix' else TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type end as Type ,TSPL_MILK_SRN_DETAIL.CLR,TSPL_MILK_SRN_HEAD.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
            sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt ,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT"
            sQuery += " , TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, case when TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type from TSPL_MILK_PURCHASE_INVOICE_DETAIL  Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE"
            sQuery += " left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE "
            sQuery += "  Left Outer Join TSPL_VENDOR_MASTER On"
            sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'   Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join TSPL_VLC_MASTER_HEAD on"
            sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  "
            sQuery += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code "
            sQuery += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code "
            sQuery += "  left join  (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL"
            sQuery += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
            sQuery += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
            sQuery += " ) as pp group by VSP_CODE,VLC_Code"
            sQuery += " ) as PaymentProcess on "
            sQuery += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_Code"
            sQuery += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code "
            sQuery += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code "
            sQuery += " left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code " &
            " left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
            left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No"
            sQuery += "  " & whrcls & " "
            sQuery += "order by vsp_code,convert(datetime,TSPL_MILK_SRN_HEAD.DOC_DATE,103)"

            ' Ticket No : BHA/07/06/18-000044 By Prabhakar ,  ( Ticket No : UDL/11/06/18-000184 For Print Transfer Screen)
            '================================================= END ALL TYPE DEDUCTION=================================================
            'sQuery += " ) as yy left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_name='" & CompName & "'"
            'sQuery += "  order by convert(date,DOC_DATE,103)"
            Dim dt As New DataTable

            dt = clsDBFuncationality.GetDataTable(sQuery)

            sQuery = "  select Customer_CODE as VSP_Code,trans_type,Item_Code as VLC_Code_VLC_Uploader ,Item_Desc ,coalesce(Amount,0) as Amount  from  (select 'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount"
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
                sQuery += ", case when isnull(TSPL_PAYMENT_PROCESS_MCC_SALE.Instalment_Amount,0) =0 then isnull(TSPL_PAYMENT_PROCESS_MCC_SALE.Amount ,0)*(-1) else isnull(TSPL_PAYMENT_PROCESS_MCC_SALE.Instalment_Amount,0)*(-1) end  as Amount "
            Else
                sQuery += ",TSPL_SD_SHIPMENT_detail.Item_net_amt*(-1) as Amount "
            End If


            sQuery += "from TSPL_PAYMENT_PROCESS_MCC_SALE"
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
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
            whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN ('" + fndLoc.Value + "') "
        End If
        whrcls += " and  PaymentProcess.doc_no='" + fndDocNo.Value + "'"
        Dim whrcls1 As String = " where 2=2 "
        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN ('" + fndLoc.Value + "') "
        End If

        Dim whrclsDeduction As String = " where 2=2 "
        whrclsDeduction += " and TSPL_MILK_REJECT_HEAD.posted=1 and convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and  convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrclsDeduction += "  and TSPL_MILK_REJECT_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")   "
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrclsDeduction += " and TSPL_LOCATION_MASTER.Loc_Segment_Code    IN ('" + fndLoc.Value + "') "
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

        sQuery += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_Code"
        sQuery += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code"
        sQuery += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code "
        sQuery += "   from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart "
        sQuery += "   on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code "

        sQuery += "  " & whrcls & " "


        sQuery += "  union all"

        'sQuery += " select '' as DOC_CODE,case when  TSPL_MILK_REJECT_DETAIL.Defaulter='VSP' then  TSPL_MILK_REJECT_DETAIL .Reject_Type else '' end Reject_Type, case when  TSPL_MILK_REJECT_DETAIL.Defaulter='VSP' then  isnull(TSPL_MILK_REJECT_DETAIL.snf_deduction_amount,0)+isnull(TSPL_MILK_REJECT_DETAIL.Fat_deduction_amount,0) else 0 end as Reject_Deduction_Amount, isnull(TSPL_MILK_REJECT_DETAIL.FAT,0) * isnull(TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,0) /100 as FAT_KG,isnull(TSPL_MILK_REJECT_DETAIL.SNF,0)  * isnull(TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,0) /100 as SNF_KG ,"
        'sQuery += " 0 as Security_Deduction_Amount,'' as paymnet_DOC_No,0 as Advance_Payment_Amount,0 as Advance_Payment_Amount_Knock_Off,"
        'sQuery += " NULL as paymnet_doc_date,NULL as Paymnet_From_Date,NULL as Payment_To_Date,"
        'sQuery += " 0 as Service_Charge_Amt , ''  as MCC_address,'' as companyADD, ''  as CompName,''  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2"
        'sQuery += ",0 as Total_EMP_Amount,0 as Incentive_Amount ,0 as Incentive_EMP_Amount ,0 as EMP_Amount ,0 as Vsp_Own_System_Amount ,0 as Head_Load_Amount ,0  as Payable_Amount,0 as Credit_Note_Amount,0 as Deduction_Amount,0 as Item_Issue_Amount,0 as Item_Issue_Return_Amount,0 as MCC_Sale_Amount ,0 as Reduce_Deduc_Amt,0 as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_REJECT_DETAIL.UOM_Code as UOM_Code,TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT  as Qty  ,TSPL_MILK_REJECT_DETAIL.FAT  as FAT_PER ,0 as FATQTY,TSPL_MILK_REJECT_DETAIL.SNF  as SNF_PER,0 as SNFQTY  ,Price_Chart.milk_rate   as RATE,case when TSPL_MILK_REJECT_DETAIL.Is_Return in ('1','2') then 0 else TSPL_MILK_REJECT_DETAIL.Amount end as Net_AMOUNT, TSPL_MILK_REJECT_HEAD.MCC_CODE  as MCC_CODE , "
        'sQuery += " convert(varchar,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE,"
        'sQuery += "TSPL_MILK_REJECT_DETAIL.VSP_CODE  as VSP_CODE ,TSPL_MILK_REJECT_HEAD.SHIFT  as SHIFT, TSPL_MILK_REJECT_DETAIL.ROUTE_CODE  as ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,"
        'sQuery += " '' as TYPE ,0 as CLR,'' as SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_VLC_MASTER_HEAD.VLC_Name "
        'sQuery += " ,0 as [EMP],0 as Incentive,0 as HEDAmt,0 as AstAMT,0 as DedAmt ,0 as Total_Basic_AMOUNT,0 as Total_Deduction "

        sQuery += " select pp.Milk_Purchase_Invoice_No as DOC_CODE,case when  TSPL_MILK_REJECT_DETAIL.Defaulter='VSP' then  TSPL_MILK_REJECT_DETAIL .Reject_Type else '' end Reject_Type, case when  TSPL_MILK_REJECT_DETAIL.Defaulter='VSP' then  isnull(TSPL_MILK_REJECT_DETAIL.snf_deduction_amount,0)+isnull(TSPL_MILK_REJECT_DETAIL.Fat_deduction_amount,0) else 0 end as Reject_Deduction_Amount, isnull(TSPL_MILK_REJECT_DETAIL.FAT,0) * isnull(TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,0) /100 as FAT_KG,isnull(TSPL_MILK_REJECT_DETAIL.SNF,0)  * isnull(TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,0) /100 as SNF_KG , (-1)*coalesce(TSPL_VENDOR_MASTER.Security_Deduction_Amount,0) as Security_Deduction_Amount,pp.Doc_No as paymnet_DOC_No,PP.Advance_Payment_Amount,PP.Advance_Payment_Amount_Knock_Off, convert(varchar,ppH.doc_date,103)  as paymnet_doc_date,convert(varchar,ppH.from_date,103) as Paymnet_From_Date,convert(varchar,ppH.to_date,103) as Payment_To_Date, (-1)*pp.Service_Charge_Amt as Service_Charge_Amt , ''  as MCC_address,'' as companyADD, ''  as CompName,''  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,pp.Total_EMP_Amount as Total_EMP_Amount,pp.Incentive_Amount as Incentive_Amount ,pp.Incentive_EMP_Amount as Incentive_EMP_Amount ,pp.EMP_Amount as EMP_Amount ,pp.Vsp_Own_System_Amount  as Vsp_Own_System_Amount ,pp.Head_Load_Amount as Head_Load_Amount ,pp.Payable_Amount  as Payable_Amount,pp.Credit_Note_Amount as Credit_Note_Amount,pp.Deduction_Amount*(-1) as Deduction_Amount,pp.Item_Issue_Amount*(-1) as Item_Issue_Amount,pp.Item_Issue_Return_Amount as Item_Issue_Return_Amount,pp.MCC_Sale_Amount*(-1) as MCC_Sale_Amount ,pp.Reduce_Deduc_Amt as Reduce_Deduc_Amt,pp.MCC_Sale_Return_Amount as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_REJECT_DETAIL.UOM_Code as UOM_Code,TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT  as Qty  ,TSPL_MILK_REJECT_DETAIL.FAT  as FAT_PER ,0 as FATQTY,TSPL_MILK_REJECT_DETAIL.SNF  as SNF_PER,0 as SNFQTY  ,Price_Chart.milk_rate   as RATE,case when TSPL_MILK_REJECT_DETAIL.Is_Return in ('1','2') then 0 else TSPL_MILK_REJECT_DETAIL.Amount end as Net_AMOUNT, TSPL_MILK_REJECT_HEAD.MCC_CODE  as MCC_CODE ,  convert(varchar,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_REJECT_DETAIL.VSP_CODE  as VSP_CODE ,TSPL_MILK_REJECT_HEAD.SHIFT  as SHIFT, TSPL_MILK_REJECT_DETAIL.ROUTE_CODE  as ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME , '' as TYPE ,0 as CLR,'' as SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_VLC_MASTER_HEAD.VLC_Name  ,0 as [EMP],0 as Incentive,0 as HEDAmt,0 as AstAMT,0 as DedAmt ,0 as Total_Basic_AMOUNT,isnull(pp.Deduction_Amount,0)*(-1) as Total_Deduction "
        sQuery += " from TSPL_MILK_REJECT_HEAD left join TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE =TSPL_MILK_REJECT_HEAD.DOC_CODE  left join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code  =TSPL_MILK_REJECT_HEAD.MCC_CODE "
        sQuery += " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_REJECT_HEAD.MCC_CODE"
        sQuery += " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =TSPL_MILK_REJECT_HEAD.Comp_Code "
        sQuery += " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_MILK_REJECT_DETAIL .VSP_CODE "
        sQuery += " left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code =TSPL_MILK_REJECT_DETAIL.ROUTE_CODE "
        sQuery += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_REJECT_DETAIL.VLC_CODE "
        sQuery += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_REJECT_DETAIL.Price_Code=Price_Chart.Code  "
        sQuery += " left outer join  TSPL_PAYMENT_PROCESS_DETAIL  as pp  on pp.VSP_CODE=TSPL_MILK_REJECT_DETAIL.VSP_CODE and PP.Doc_No='" + fndDocNo.Value + "' " + Environment.NewLine +
            " left outer join  TSPL_PAYMENT_PROCESS_HEAD  as ppH  on ppH.Doc_No=pp.Doc_No  " ''UDL/12/07/18-000204 by balwinder if first row is of reject type then bill no and date is not coming on print.

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

    Public Sub Load_Report_Paymnet_BHAD()
        '======================preeti gupta ticket no [] update 29/11/2016
        ' Ticket No : BHA/06/09/18-000519 By prabhakar - work on rpt 
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
            whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN ('" + fndLoc.Value + "') "
        End If
        whrcls += " and  PaymentProcess.doc_no='" + fndDocNo.Value + "'"
        Dim whrcls1 As String = " where 2=2 "
        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN ('" + fndLoc.Value + "') "
        End If

        Dim whrclsDeduction As String = " where 2=2 "
        whrclsDeduction += "  and convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and  convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrclsDeduction += "  and TSPL_MILK_REJECT_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")   "
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrclsDeduction += " and TSPL_LOCATION_MASTER.Loc_Segment_Code    IN (" + fndLoc.Value + ") "
        End If
        sQuery = ""


        sQuery += "select *,Net_AMOUNT-Reject_Deduction_Amount as GrossAmt from (select  TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE,'' as Reject_Type,0 as Reject_Deduction_Amount ,TSPL_MILK_SRN_DETAIL .FAT_KG, cast( ((isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty,0) * isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER,0) )/100) as Decimal(18,2)) as FAT_LTR ,Cast( ( (isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty,0) * isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER,0) )/100) as Decimal(18,2)) as SNF_LTR ,TSPL_MILK_SRN_DETAIL .SNF_KG , (-1)*coalesce(TSPL_VENDOR_MASTER.Security_Deduction_Amount,0) as Security_Deduction_Amount,"
        sQuery += " PaymentProcess.doc_no as paymnet_DOC_No,PaymentProcess.Advance_Payment_Amount,PaymentProcess.Advance_Payment_Amount_Knock_Off,PaymentProcess.doc_date as paymnet_doc_date,convert(varchar,PaymentProcess.from_date,103) as Paymnet_From_Date,convert(varchar,PaymentProcess.to_date,103) as Payment_To_Date, (-1)*PaymentProcess.Service_Charge_Amt as Service_Charge_Amt  ,  TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, '" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,PaymentProcess.Total_EMP_Amount,PaymentProcess.Incentive_Amount ,PaymentProcess.Incentive_EMP_Amount ,PaymentProcess.EMP_Amount ,PaymentProcess.Vsp_Own_System_Amount ,PaymentProcess.Head_Load_Amount ,(PaymentProcess.Payable_Amount) as Payable_Amount,(PaymentProcess.Credit_Note_Amount)as Credit_Note_Amount,(PaymentProcess.Deduction_Amount)*(-1) as Deduction_Amount,(PaymentProcess.Item_Issue_Amount)*(-1) as Item_Issue_Amount,(PaymentProcess.Item_Issue_Return_Amount) as Item_Issue_Return_Amount,(PaymentProcess.MCC_Sale_Amount)*(-1) as MCC_Sale_Amount,(PaymentProcess.Reduce_Deduc_Amt) as Reduce_Deduc_Amt ,(PaymentProcess.MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_RECEIPT_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty  ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER ,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as FATQTY,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as SNFQTY "
        sQuery += " ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Rate as RATE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT, TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,TSPL_MILK_SAMPLE_HEAD.SHIFT,"
        sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,TSPL_MILK_SAMPLE_DETAIL.TYPE ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
        sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt ,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT,isnull(PaymentProcess.Deduction_Amount,0)*(-1)  as Total_Deduction ,TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, case when TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type "
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

        sQuery += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_Code"
        sQuery += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code"
        sQuery += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code "
        sQuery += "   from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart "
        sQuery += "   on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code  left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code "

        sQuery += "  " & whrcls & " "


        sQuery += "  union all"

        'sQuery += " select '' as DOC_CODE,case when  TSPL_MILK_REJECT_DETAIL.Defaulter='VSP' then  TSPL_MILK_REJECT_DETAIL .Reject_Type else '' end Reject_Type, case when  TSPL_MILK_REJECT_DETAIL.Defaulter='VSP' then  isnull(TSPL_MILK_REJECT_DETAIL.snf_deduction_amount,0)+isnull(TSPL_MILK_REJECT_DETAIL.Fat_deduction_amount,0) else 0 end as Reject_Deduction_Amount, isnull(TSPL_MILK_REJECT_DETAIL.FAT,0) * isnull(TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,0) /100 as FAT_KG,isnull(TSPL_MILK_REJECT_DETAIL.SNF,0)  * isnull(TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,0) /100 as SNF_KG ,"
        'sQuery += " 0 as Security_Deduction_Amount,'' as paymnet_DOC_No,0 as Advance_Payment_Amount,0 as Advance_Payment_Amount_Knock_Off,"
        'sQuery += " NULL as paymnet_doc_date,NULL as Paymnet_From_Date,NULL as Payment_To_Date,"
        'sQuery += " 0 as Service_Charge_Amt , ''  as MCC_address,'' as companyADD, ''  as CompName,''  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2"
        'sQuery += ",0 as Total_EMP_Amount,0 as Incentive_Amount ,0 as Incentive_EMP_Amount ,0 as EMP_Amount ,0 as Vsp_Own_System_Amount ,0 as Head_Load_Amount ,0  as Payable_Amount,0 as Credit_Note_Amount,0 as Deduction_Amount,0 as Item_Issue_Amount,0 as Item_Issue_Return_Amount,0 as MCC_Sale_Amount ,0 as Reduce_Deduc_Amt,0 as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_REJECT_DETAIL.UOM_Code as UOM_Code,TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT  as Qty  ,TSPL_MILK_REJECT_DETAIL.FAT  as FAT_PER ,0 as FATQTY,TSPL_MILK_REJECT_DETAIL.SNF  as SNF_PER,0 as SNFQTY  ,Price_Chart.milk_rate   as RATE,case when TSPL_MILK_REJECT_DETAIL.Is_Return in ('1','2') then 0 else TSPL_MILK_REJECT_DETAIL.Amount end as Net_AMOUNT, TSPL_MILK_REJECT_HEAD.MCC_CODE  as MCC_CODE , "
        'sQuery += " convert(varchar,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE,"
        'sQuery += "TSPL_MILK_REJECT_DETAIL.VSP_CODE  as VSP_CODE ,TSPL_MILK_REJECT_HEAD.SHIFT  as SHIFT, TSPL_MILK_REJECT_DETAIL.ROUTE_CODE  as ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,"
        'sQuery += " '' as TYPE ,0 as CLR,'' as SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_VLC_MASTER_HEAD.VLC_Name "
        'sQuery += " ,0 as [EMP],0 as Incentive,0 as HEDAmt,0 as AstAMT,0 as DedAmt ,0 as Total_Basic_AMOUNT,0 as Total_Deduction "

        sQuery += " select '' as DOC_CODE,case when  TSPL_MILK_REJECT_DETAIL.Defaulter='VSP' then  TSPL_MILK_REJECT_DETAIL .Reject_Type else '' end Reject_Type, case when  TSPL_MILK_REJECT_DETAIL.Defaulter='VSP' then  isnull(TSPL_MILK_REJECT_DETAIL.snf_deduction_amount,0)+isnull(TSPL_MILK_REJECT_DETAIL.Fat_deduction_amount,0) else 0 end as Reject_Deduction_Amount, isnull(TSPL_MILK_REJECT_DETAIL.FAT,0) * isnull(TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,0) /100 as FAT_KG, Cast(( isnull(TSPL_MILK_REJECT_DETAIL.FAT,0) * isnull(TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,0) /100 ) as Decimal(18,2)) as FAT_LTR  ,Cast( (isnull(TSPL_MILK_REJECT_DETAIL.SNF,0)  * isnull(TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,0) /100) as Decimal(18,2) )as SNF_LTR  , isnull(TSPL_MILK_REJECT_DETAIL.SNF,0)  * isnull(TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,0) /100 as SNF_KG , (-1)*coalesce(TSPL_VENDOR_MASTER.Security_Deduction_Amount,0) as Security_Deduction_Amount,pp.Doc_No as paymnet_DOC_No,PP.Advance_Payment_Amount,PP.Advance_Payment_Amount_Knock_Off, null as paymnet_doc_date,NULL as Paymnet_From_Date,NULL as Payment_To_Date, (-1)*pp.Service_Charge_Amt as Service_Charge_Amt , ''  as MCC_address,'' as companyADD, ''  as CompName,''  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,pp.Total_EMP_Amount as Total_EMP_Amount,pp.Incentive_Amount as Incentive_Amount ,pp.Incentive_EMP_Amount as Incentive_EMP_Amount ,pp.EMP_Amount as EMP_Amount ,pp.Vsp_Own_System_Amount  as Vsp_Own_System_Amount ,pp.Head_Load_Amount as Head_Load_Amount ,pp.Payable_Amount  as Payable_Amount,pp.Credit_Note_Amount as Credit_Note_Amount,pp.Deduction_Amount*(-1) as Deduction_Amount,pp.Item_Issue_Amount*(-1) as Item_Issue_Amount,pp.Item_Issue_Return_Amount as Item_Issue_Return_Amount,pp.MCC_Sale_Amount*(-1) as MCC_Sale_Amount ,pp.Reduce_Deduc_Amt as Reduce_Deduc_Amt,pp.MCC_Sale_Return_Amount as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_REJECT_DETAIL.UOM_Code as UOM_Code,TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT  as Qty  ,TSPL_MILK_REJECT_DETAIL.FAT  as FAT_PER ,0 as FATQTY,TSPL_MILK_REJECT_DETAIL.SNF  as SNF_PER,0 as SNFQTY  ,Price_Chart.milk_rate   as RATE,case when TSPL_MILK_REJECT_DETAIL.Is_Return in ('1','2') then 0 else TSPL_MILK_REJECT_DETAIL.Amount end as Net_AMOUNT, TSPL_MILK_REJECT_HEAD.MCC_CODE  as MCC_CODE ,  convert(varchar,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_REJECT_DETAIL.VSP_CODE  as VSP_CODE ,TSPL_MILK_REJECT_HEAD.SHIFT  as SHIFT, TSPL_MILK_REJECT_DETAIL.ROUTE_CODE  as ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME , '' as TYPE ,0 as CLR,'' as SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_VLC_MASTER_HEAD.VLC_Name  ,0 as [EMP],0 as Incentive,0 as HEDAmt,0 as AstAMT,0 as DedAmt ,0 as Total_Basic_AMOUNT,isnull(pp.Deduction_Amount,0)*(-1) as Total_Deduction ,TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, case when TSPL_MILK_REJECT_DETAIL.FAT >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type  "
        sQuery += " from TSPL_MILK_REJECT_HEAD left join TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE =TSPL_MILK_REJECT_HEAD.DOC_CODE  left join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code  =TSPL_MILK_REJECT_HEAD.MCC_CODE "
        sQuery += " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_REJECT_HEAD.MCC_CODE"
        sQuery += " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =TSPL_MILK_REJECT_HEAD.Comp_Code "
        sQuery += " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_MILK_REJECT_DETAIL .VSP_CODE "
        sQuery += " left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code =TSPL_MILK_REJECT_DETAIL.ROUTE_CODE "
        sQuery += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_REJECT_DETAIL.VLC_CODE "
        sQuery += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_REJECT_DETAIL.Price_Code=Price_Chart.Code  "
        sQuery += " left outer join  TSPL_PAYMENT_PROCESS_DETAIL  as pp  on pp.VSP_CODE=TSPL_MILK_REJECT_DETAIL.VSP_CODE and PP.Doc_No='" + fndDocNo.Value + "' left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code  "
        sQuery += " " & whrclsDeduction & ") as final "

        sQuery += " order by vsp_code,convert(date,doc_date,103) ,Reject_Type "



        dt = clsDBFuncationality.GetDataTable(sQuery)
        ' Ticket No : BHA/28/06/18-000108 Work On Print Formate By Prabhakar
        sQuery = "select Customer_CODE,Item_Code,max(Item_Desc)  as Item_Desc,sum(Qty) as Qty,sum(Paymnet_Amount) as Paymnet_Amount from ( select  TSPL_SD_SHIPMENT_detail.qty,'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Amount*(-1) as Amount  from TSPL_PAYMENT_PROCESS_MCC_SALE"
        sQuery += " left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no"
        sQuery += " left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no='" + fndDocNo.Value + "' )xx group by Customer_CODE,item_code "

        dtgv = clsDBFuncationality.GetDataTable(sQuery)

        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dtgv, "crptMilkPurchaseBillPaymentProcess_Type_Wise", "SubMilkPurchaseBill.rpt", "SubMilkPurchaseBill.rpt", "Address.rpt")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                Load_Report_Paymnet_UDL()
            ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal Then
                Load_Report_Paymnet_BHAD()
            ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHBA") = CompairStringResult.Equal Then
                Load_Report_Paymnet_BHBA()
            ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UCDF") = CompairStringResult.Equal Then
                Load_Report_Paymnet_UCDF()
            ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "RCDF") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDP") = CompairStringResult.Equal Then
                'Load_Report_Paymnet_RCDF()
                clsPaymentProcessHead.Load_Report_Paymnet_RCDF("'" + fndDocNo.Value + "'", dtpFromDate.Text, dtpToDate.Text, "", clsCommon.GetMulcallString(txtVSP.arrValueMember), "", "", "", False)
            Else
                Load_Report(Nothing, Nothing, Nothing, Nothing, False, True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        End Try
    End Sub

    Private Sub Load_Report_Paymnet_RCDF()
        Dim companyADD, CompName, CompCode As String

        Dim sQuery As String = ""
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

        whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN ('" + fndLoc.Value + "') "
        End If
        whrcls += " and not exists(select 1 from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS where TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE) "

        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_Head.doc_no='" + fndDocNo.Value + "'"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN ('" + fndLoc.Value + "') "
        End If
        whrclsItemWise += " and final.doc_no in ('" + fndDocNo.Value + "')"
        whrclsItemWise += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        Dim strPC_FATValue As String = 0
        Dim strPC_SNFValue As String = 0
        Dim dtPC_FAT_SNF As DataTable = clsDBFuncationality.GetDataTable("select  top 1 round ( Price_Chart_FAT_Ratio * Price_Chart_Rate / nullif (Price_Chart_FAT_Per,0) , 2,1 ) as FAT_PCValue , round (  Price_Chart_SNF_Ratio * Price_Chart_Rate / nullif (Price_Chart_SNF_Per,0),2,1)  as SNF_PCValue  from TSPL_PRICE_CHART_PLANNING order by convert (date, Planning_Date,103)")
        If dtPC_FAT_SNF IsNot Nothing AndAlso dtPC_FAT_SNF.Rows.Count > 0 Then
            strPC_FATValue = clsCommon.myCdbl(dtPC_FAT_SNF.Rows(0)("FAT_PCValue"))
            strPC_SNFValue = clsCommon.myCdbl(dtPC_FAT_SNF.Rows(0)("SNF_PCValue"))
        End If
        Dim CycleNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select max(Name) as CycleNo from TSPL_PAYMENT_CYCLE_GENERATED where convert(varchar, From_Date,103) = convert(varchar, '" + dtpFromDate.Value + "',103) "))
        Dim BaseQry As String = ""
        BaseQry = "select '" + fndDocNo.Value + "' as PPDoc_No,'" + clsCommon.myCstr(clsCommon.GetPrintDate(dtpDate.Value, "dd/MM/yyyy")) + "' as PPDoc_Date,'" + CycleNo + "' as CycleNo, TBL_BILL_DETAILS.BillNo, TBL_BILL_DETAILS.BillDate, '" + strPC_FATValue + "' as PC_FATValue, '" + strPC_SNFValue + "' as PC_SNFValue, "
        BaseQry += " isnull(TSPL_VENDOR_MASTER.Actual_charges,0) as Actual_charges,isnull (TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load ,isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount,0) as Handling_Charges_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, "
        BaseQry += "    '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate"
        BaseQry += " ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,TSPL_COMPANY_MASTER.Vat_Reg_No,TSPL_COMPANY_MASTER.Phone1 as Comp_Phone1,TSPL_COMPANY_MASTER.Phone2 as Comp_Phone2,TSPL_COMPANY_MASTER.Regn_No,coalesce(PaymentProcess.Total_EMP_Amount,0) as Total_EMP_Amount,coalesce(PaymentProcess.Incentive_Amount,0) as Incentive_Amount ,coalesce(PaymentProcess.Incentive_EMP_Amount,0) as Incentive_EMP_Amount ,coalesce(PaymentProcess.EMP_Amount,0) as EMP_Amount ,coalesce(PaymentProcess.Vsp_Own_System_Amount,0) as Vsp_Own_System_Amount ,coalesce(PaymentProcess.Head_Load_Amount,0) as Head_Load_Amount ,coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0)*(-1) as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty "
        BaseQry += " ,case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  (Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0)) end as Standard_Rate" + Environment.NewLine +
        ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.Fat_ratio)/Price_Chart.FAT_Pers) as decimal(18,2)) end as Standard_FAT_Rate" + Environment.NewLine +
        ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.SNF_Ratio)/Price_Chart.SNF_Pers) as decimal(18,2)) end as Standard_SNF_Rate" + Environment.NewLine +
        ",TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_RO_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_SRN_head.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,case when isnull(TSPL_MILK_SRN_HEAD.Against_reject_no,'')='' then TSPL_MILK_RECEIPT_HEAD.shift else TSPL_MILK_REJECT_head.shift end as SHIFT,"
        BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Bank_Code as Vendor_Bank_Code, TSPL_VENDOR_MASTER.Bank_Name as Vendor_Bank_Name, TSPL_VENDOR_MASTER.Account_Type as Vendor_Bank_Account_Type1 , TSPL_VENDOR_MASTER.AccountType2 as Vendor_Bank_Account_Type2 , TSPL_VENDOR_MASTER.Account_No as Vendor_Account_No1, TSPL_VENDOR_MASTER.AccNo2 as Vendor_Account_No2,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,case when isnull(TSPL_MILK_SAMPLE_DETAIL.TYPE,'')='' then 'Mix' else TSPL_MILK_SAMPLE_DETAIL.TYPE end as Type ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
        BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt"
        BaseQry += " ,TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, case when TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type " + Environment.NewLine +
            ",(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)) as SRN_Net_Amount
,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT "
        If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
            BaseQry += " ,coalesce(TSPL_Primary_Vehicle_Master.Vehicle,TSPL_MILK_SRN_HEAD.VEHICLE_CODE) as VEHICLE_CODE "
        Else
            BaseQry += " ,TSPL_MILK_SRN_HEAD.VEHICLE_CODE "
        End If
        BaseQry += ",cast( case when  TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty=0 then 0 else (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))/TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty end as decimal(18,2)) as RATE
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER 
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as FATQTY
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else ( cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) ) end as decimal(18,2)) as FAT_Rate
,cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as SNFQTY 
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else (cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1)) end as decimal(18,2)) as SNF_Rate
,cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount
, " + IIf(objCommonVar.CurrentCompanyCode = "RCDF", "TSPL_MILK_REJECT_DETAIL.Reject_Type", "case when isnull (TSPL_MILK_REJECT_DETAIL.Reject_Type,'') = '' then  'SWEET' else upper (TSPL_MILK_REJECT_DETAIL.Reject_Type) end") + " as QBD, '1' as QAP,TBL_BILL_DETAILS.BILLSRL  
,TabSaving.Item_Desc as SavingDesc, TabSaving.[Amount] as [SavingAmount]
         from TSPL_MILK_PURCHASE_INVOICE_DETAIL  " + Environment.NewLine + " Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " + Environment.NewLine + " left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE " + Environment.NewLine
        BaseQry += " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  " + Environment.NewLine + "  Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  " + Environment.NewLine
        BaseQry += " left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine
        BaseQry += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE " + Environment.NewLine + "  left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE " + Environment.NewLine + " Left Outer Join TSPL_VENDOR_MASTER On"
        BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  " + Environment.NewLine + " Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  " + Environment.NewLine + " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code " + Environment.NewLine + " left outer join TSPL_VLC_MASTER_HEAD on"
        BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  " + Environment.NewLine
        BaseQry += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code " + Environment.NewLine
        BaseQry += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code " + Environment.NewLine
        BaseQry += " left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.VEHICLE_CODE = TSPL_MILK_SRN_HEAD.VEHICLE_CODE " + Environment.NewLine
        BaseQry += " left join  (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL"
        BaseQry += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
        BaseQry += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
        BaseQry += " ) as pp group by VSP_CODE,VLC_Code"
        BaseQry += " ) as PaymentProcess on "
        BaseQry += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_Code " + Environment.NewLine
        BaseQry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code " + Environment.NewLine
        BaseQry += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code "
        BaseQry += " left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code " + Environment.NewLine +
        " left join TSPL_MILK_REJECT_head on TSPL_MILK_REJECT_head.doc_code=TSPL_MILK_SRN_HEAD.Against_reject_no
          left outer join  TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE = TSPL_MILK_REJECT_head.DOC_CODE and TSPL_VLC_MASTER_HEAD.VLC_Code  = TSPL_MILK_REJECT_DETAIL.VLC_CODE  and TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT = TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty   
          left outer join (select TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE , TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No as BillNo , convert(varchar,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_Date,103) as BillDate, TSPL_PAYMENT_PROCESS_DETAIL.SNo as BILLSRL from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No = '" + fndDocNo.Value + "') as TBL_BILL_DETAILS on TBL_BILL_DETAILS.VSP_CODE =
          TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code  
          left outer join (select VSP_Code,max(Item_Desc) as Item_Desc, sum([Amount]) as [Amount] from (
			 select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,'' as Vendor_NAME,TSPL_DCS_ADDITION_DEDUCTION.Description as Item_Desc,(TSPL_VENDOR_INVOICE_HEAD.Document_Total) as [Amount]  from TSPL_PAYMENT_PROCESS_SAVING 
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No
left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_CODE
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
where  TSPL_PAYMENT_PROCESS_SAVING.Doc_No = '" + fndDocNo.Value + "' )x group by VSP_Code)TabSaving on TabSaving.VSP_Code=TBL_BILL_DETAILS.VSP_CODE"
        BaseQry += "  " & whrcls & " "
        Dim dt As New DataTable
        sQuery = BaseQry + " order by " + IIf(objCommonVar.CurrentCompanyCode = "RCDF", " ", "BILLSRL asc,") + " vsp_code,convert(datetime,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103),shift desc"
        dt = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select Vendor_CODE as VSP_Code,trans_type,round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as  FAT_Amount,Amount - round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as SNF_Amount,coalesce(Amount,0) as Amount from (" + Environment.NewLine +
"select doc_no,Vendor_CODE,SNo,trans_type,sum(Amount) as Amount from( " + Environment.NewLine +
"select case when RefDocType ='VSP-COM' then 'EMP' else (case when RefDocType ='PRO-VFC' then 'PRO ADD.' else 'OTHER ADD.' end ) end as trans_type,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,1 as   Show_FAT_SNF , case when RefDocType ='VSP-COM' then 1 else (case when RefDocType ='PRO-VFC' then 3 else 2 end ) end as SNo" + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   " + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No" + Environment.NewLine +
"where TSPL_VENDOR_INVOICE_HEAD.RefDocType not in ('VSP-DIT') and TSPL_VENDOR_INVOICE_HEAD.Description = 'AP Credit Note For VSP Commission' and TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no  = ('" + fndDocNo.Value + "')" + Environment.NewLine +
"union all " + Environment.NewLine +
"select case when RefDocType='VSP-QLT' then 'CBD' else case when RefDocType='MILK-REJ' then 'Rejection Ch.' else (case when RefDocType ='PRO-VFD' then 'PRO DED.' else 'OTHER DED.' end ) end end as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
" TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Amount ,case when RefDocType='MILK-REJ' then 1 else TSPL_DEDUCTION_MASTER.Show_FAT_SNF  end Show_FAT_SNF, case when RefDocType='VSP-QLT' then 4 else case when RefDocType='MILK-REJ' then 5 else (case when RefDocType ='PRO-VFD' then 7 else 6 end ) end end as SNo" + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
"left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine +
" where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and (TSPL_DEDUCTION_MASTER.Show_FAT_SNF=1 or RefDocType='MILK-REJ')" + Environment.NewLine +
")xxx group by doc_no, Vendor_CODE,SNo,trans_type" + Environment.NewLine +
        " ) as final " + Environment.NewLine +
        " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no" + Environment.NewLine +
        "left outer join (select vsp_code,sum(FAT_Amount) as FAT_Amount,sum(SNF_Amount) as SNF_Amount from (" + BaseQry + ")x group by vsp_code)Tab on tab.VSP_CODE=final.Vendor_CODE"
        sQuery += " " & whrclsItemWise & " "
        Dim dtFATNSFDCNote As DataTable = clsDBFuncationality.GetDataTable(sQuery)





        sQuery = " select * from ( select Customer_CODE as VSP_Code,trans_type,Item_Code as VLC_Code_VLC_Uploader ,Item_Desc,0 as FAT_Amount,0 as SNF_Amount,coalesce(Amount,0) as Amount , AP_Invoice_Date,  isnull(TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos,0) as Is_Default_Pashu_Vikash_Kos, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code from (" + Environment.NewLine +
        "select max(trans_type) as trans_type,max(doc_no) as doc_no,max(shipment_doc_no) as AP_Invoice_No,(item_code) as	item_code	, max(Item_Desc) as	Item_Desc,customer_code	,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF , max( AP_Invoice_Date) as AP_Invoice_Date from(" + Environment.NewLine +
        "select 'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Item_net_amt*(-1) as Amount ,0 as Show_FAT_SNF  , case when len (TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_Date) > 0 then convert (varchar,TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_Date,103) else '' end as AP_Invoice_Date  " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code    where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  = ('" + fndDocNo.Value + "')" + Environment.NewLine +
")xxx group by customer_code,item_code" + Environment.NewLine +
"union all " + Environment.NewLine +
"select 'MCC Sale Return' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Doc_No ,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no,TSPL_SD_SALE_RETURN_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.amount as Paymnet_Amount ,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF , case when len (TSPL_PAYMENT_PROCESS_MCC_SALE_Return.AR_Invoice_Date) > 0 then convert (varchar,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.AR_Invoice_Date,103) else '' end as AP_Invoice_Date " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE_Return  " + Environment.NewLine +
"left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code   " + Environment.NewLine +
"union all
select max(trans_type) as trans_type,max(doc_no) as doc_no,max(Payment_No) as AP_Invoice_No,'' as	item_code	,'Asset Lost' as Item_Desc,Vendor_Code as customer_code,0 as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF,  '' as AP_Invoice_Date from(
select 'Asset Lost' as trans_type,TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Vendor_Code,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_Amount*(-1) as Amount ,0 as Show_FAT_SNF 
from TSPL_PAYMENT_PROCESS_ASSET_LOST 
 where TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No  = ('" + fndDocNo.Value + "')
)xxx group by Vendor_Code" + Environment.NewLine +
"union all " + Environment.NewLine +
"select 'Item Issue' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt*(-1) as Item_Net_Amt ,0 as Show_FAT_SNF , '' as AP_Invoice_Date " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE  " + Environment.NewLine +
"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine +
"union all " + Environment.NewLine +
"select 'Item Issue Return' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF  , '' as AP_Invoice_Date " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN  " + Environment.NewLine +
"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine +
"union all " + Environment.NewLine +
"select max(trans_type) as trans_type,max(doc_no) as doc_no,max(AP_Invoice_No) as AP_Invoice_No,max(Vendor_CODE) as	Vendor_CODE	,Item_Desc,Vendor_CODE	,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Item_Net_AMount) as	Item_Net_AMount	,max(Show_FAT_SNF) as	Show_FAT_SNF, AP_Invoice_Date  from( " + Environment.NewLine +
"select RefDocType,'Deduction' as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
"case when TSPL_DEDUCTION_MASTER.Code is not null then TSPL_DEDUCTION_MASTER.Description else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc end as Item_Desc ,0 as Paymnet_Amount ,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Item_Net_AMount ,TSPL_DEDUCTION_MASTER.Show_FAT_SNF, convert (varchar,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_Date,103) as AP_Invoice_Date   " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
"left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine + " where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and not ( RefDocType='MILK-REJ')" + Environment.NewLine +     ' TSPL_DEDUCTION_MASTER.Show_FAT_SNF=1 or
")xxx group by RefDocType,Vendor_CODE,Item_Desc,AP_Invoice_Date " + Environment.NewLine
        sQuery += " ) as final "
        sQuery += " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no" + Environment.NewLine +
        "left outer join (select vsp_code,sum(FAT_Amount) as FAT_Amount,sum(SNF_Amount) as SNF_Amount from (" + BaseQry + ")x group by vsp_code)Tab on tab.VSP_CODE=final.Customer_CODE
         
         left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Description=final.Item_Desc
         left outer join (select distinct VSP_Code ,VLC_Code_VLC_Uploader from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = Item_Code
          
         "
        sQuery += " " & whrclsItemWise & "  Union All  "
        sQuery += " select TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VSP_Code ,TSPL_MULTIPLE_DEDUCTION_head.trans_type   , TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VLC_Code_VLC_Uploader, TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc as Item_Desc , 0 as FAT_Amount,0 as SNF_Amount , TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Amount ,Convert (varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as  AP_Invoice_Date,  0 as Is_Default_Pashu_Vikas_Kos, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code
                    from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
                    left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_MULTIPLE_DEDUCTION_head on TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
                    where  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no in ('" + fndDocNo.Value + "')
                    and TSPL_VENDOR_INVOICE_HEAD.Description <> 'AP Credit Note For VSP Commission'
                    and TSPL_MULTIPLE_DEDUCTION_head.trans_type = 'Addition' 
                    ) Final order by Is_Default_Pashu_Vikash_Kos desc , trans_type desc "
        Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,convert(varchar, TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_REJECT_HEAD.SHIFT,TSPL_MILK_REJECT_DETAIL.Reject_Type,TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,UOM_Code,TSPL_MILK_REJECT_DETAIL.RATE, TSPL_PAYMENT_PROCESS_DEDUCTION.Amount   " + Environment.NewLine +
        "from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
        "left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
        "left outer join TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE=TSPL_VENDOR_INVOICE_HEAD.RefDocNo and TSPL_MILK_REJECT_DETAIL.SAMPLE_NO=TSPL_VENDOR_INVOICE_HEAD.Ref_SNo " + Environment.NewLine +
        "left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE" + Environment.NewLine +
        "where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and RefDocType='MILK-REJ'"
        Dim dtRej As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,  TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc" + Environment.NewLine +
",TSPL_SD_SHIPMENT_detail.Qty as MILK_WEIGHT,case when isnull(TSPL_SD_SHIPMENT_detail.Qty,0)<=0 then 0 else cast(TSPL_PAYMENT_PROCESS_MCC_SALE.Amount/TSPL_SD_SHIPMENT_detail.Qty as decimal(18,2)) end as RATE,TSPL_PAYMENT_PROCESS_MCC_SALE.Amount  " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code    " + Environment.NewLine +
"where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  = ('" + fndDocNo.Value + "')"
        Dim dtSale As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        '        sQuery = " select  Final.VSP_Uploader_Code, Final.VSP_Code ,'' as Vendor_NAME, Final.Item_Desc as Addition , sum(Amount) as [Amount]  from (
        'select TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VSP_Code ,TSPL_MULTIPLE_DEDUCTION_head.trans_type   , TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VLC_Code_VLC_Uploader, TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc as Item_Desc , 0 as FAT_Amount,0 as SNF_Amount , TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Amount ,Convert (varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as  AP_Invoice_Date,  0 as Is_Default_Pashu_Vikas_Kos, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code
        '                    from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
        '                    left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
        '                    left outer join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
        '                    left outer join TSPL_MULTIPLE_DEDUCTION_head on TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
        '                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
        '					inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
        '                    where  TSPL_PAYMENT_PROCESS_HEAD.Doc_No = '" + fndDocNo.Value + "'                      
        '                    and TSPL_MULTIPLE_DEDUCTION_head.trans_type = 'Addition' ) Final group by Final.VSP_Uploader_Code, Final.VSP_Code , Final.Item_Desc "

        sQuery = "select  Final.VSP_Uploader_Code, Final.VSP_Code ,'' as Vendor_NAME,Final.Item_Desc as Addition, sum(Amount) as [Amount]  from (
select TSPL_VENDOR_INVOICE_HEAD.Document_No, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VSP_Code ,TSPL_MULTIPLE_DEDUCTION_head.trans_type,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VLC_Code_VLC_Uploader, 
case when isnull(TSPL_MULTIPLE_DEDUCTION_head.trans_type,'')='Addition' then TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc else TSPL_DCS_ADDITION_DEDUCTION.Description  end as Item_Desc , 0 as FAT_Amount,0 as SNF_Amount , TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Amount ,Convert (varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as  AP_Invoice_Date,  0 as Is_Default_Pashu_Vikas_Kos, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code
,TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
left outer join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
left outer join TSPL_MULTIPLE_DEDUCTION_head on TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
where  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = '" + fndDocNo.Value + "' 
) Final group by Final.VSP_Uploader_Code, Final.VSP_Code , Final.Item_Desc 
union all
select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,'' as Vendor_NAME,TSPL_DCS_ADDITION_DEDUCTION.Description as Addition,(TSPL_VENDOR_INVOICE_HEAD.Document_Total) as [Amount]  from TSPL_PAYMENT_PROCESS_SAVING 
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No
left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_CODE
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
where  TSPL_PAYMENT_PROCESS_SAVING.Doc_No = '" + fndDocNo.Value + "' "
        Dim dtAddition As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select Final.VSP_Uploader_Code, Final.Vendor_CODE, Vendor_NAME, Max(Ded_Desc) as Ded_Code, sum(Amount) as [Amount] from (
select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount 
from TSPL_PAYMENT_PROCESS_DEDUCTION 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = '" + fndDocNo.Value + "' 
) Final group by  final.VSP_Uploader_Code, Final.Vendor_CODE, Vendor_NAME, Final.Ded_Code "
        Dim dtDeduction As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "RCDF") = CompairStringResult.Equal Then
                frmCRV.funsubreportWithdt(False, CrystalReportFolder.MilkProcurement, dt, dtgv, "crptMilkPurchaseBillPaymentProcess", "", Nothing, "SubMilkPurchaseBill.rpt", "Address.rpt", Nothing, "SubMilkPurchaseBillRejection.rpt", dtRej, "SubMilkPurchaseBillMCCSale.rpt", dtSale, "SubMilkPurchaseBillFATSNFDebitCreditNote.rpt", dtFATNSFDCNote)
            Else
                frmCRV.funsubreportWithdt(False, CrystalReportFolder.MilkProcurement, dt, dtAddition, "crptMilkPurchaseBillPaymentProcessNew", "", Nothing, "subAddition.rpt", "subDeduction.rpt", dtDeduction)
            End If


            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub

    Public Sub Load_Report_Paymnet_UCDF()
        Dim companyADD, CompName, CompCode As String

        Dim sQuery As String = ""
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

        whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN ('" + fndLoc.Value + "') "
        End If
        whrcls += " and not exists(select 1 from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS where TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE) "

        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_Head.doc_no='" + fndDocNo.Value + "'"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN ('" + fndLoc.Value + "') "
        End If
        whrclsItemWise += " and final.doc_no in ('" + fndDocNo.Value + "')"
        whrclsItemWise += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        Dim strPC_FATValue As String = 0
        Dim strPC_SNFValue As String = 0
        Dim dtPC_FAT_SNF As DataTable = clsDBFuncationality.GetDataTable("select  top 1 round ( Price_Chart_FAT_Ratio * Price_Chart_Rate / nullif (Price_Chart_FAT_Per,0) , 2,1 ) as FAT_PCValue , round (  Price_Chart_SNF_Ratio * Price_Chart_Rate / nullif (Price_Chart_SNF_Per,0),2,1)  as SNF_PCValue  from TSPL_PRICE_CHART_PLANNING order by convert (date, Planning_Date,103)")
        If dtPC_FAT_SNF IsNot Nothing AndAlso dtPC_FAT_SNF.Rows.Count > 0 Then
            strPC_FATValue = clsCommon.myCdbl(dtPC_FAT_SNF.Rows(0)("FAT_PCValue"))
            strPC_SNFValue = clsCommon.myCdbl(dtPC_FAT_SNF.Rows(0)("SNF_PCValue"))
        End If
        Dim CycleNo As String = clsDBFuncationality.getSingleValue(" select max(Name) as CycleNo from TSPL_PAYMENT_CYCLE_GENERATED where convert(varchar, From_Date,103) = convert(varchar, '" + dtpFromDate.Value + "',103) ")
        Dim BaseQry As String = ""
        BaseQry = "select '" + CycleNo + "' as CycleNo, TBL_BILL_DETAILS.BillNo, TBL_BILL_DETAILS.BillDate, '" + strPC_FATValue + "' as PC_FATValue, '" + strPC_SNFValue + "' as PC_SNFValue, "
        BaseQry += " isnull(TSPL_VENDOR_MASTER.Actual_charges,0) as Actual_charges,isnull (TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load ,isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount,0) as Handling_Charges_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, "
        BaseQry += "    '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate"
        BaseQry += " ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,coalesce(PaymentProcess.Total_EMP_Amount,0) as Total_EMP_Amount,coalesce(PaymentProcess.Incentive_Amount,0) as Incentive_Amount ,coalesce(PaymentProcess.Incentive_EMP_Amount,0) as Incentive_EMP_Amount ,coalesce(PaymentProcess.EMP_Amount,0) as EMP_Amount ,coalesce(PaymentProcess.Vsp_Own_System_Amount,0) as Vsp_Own_System_Amount ,coalesce(PaymentProcess.Head_Load_Amount,0) as Head_Load_Amount ,coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0)*(-1) as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty "
        BaseQry += " ,case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  (Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0)) end as Standard_Rate" + Environment.NewLine +
        ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.Fat_ratio)/Price_Chart.FAT_Pers) as decimal(18,2)) end as Standard_FAT_Rate" + Environment.NewLine +
        ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.SNF_Ratio)/Price_Chart.SNF_Pers) as decimal(18,2)) end as Standard_SNF_Rate" + Environment.NewLine +
        ",TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_RO_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_SRN_head.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,case when isnull(TSPL_MILK_SRN_HEAD.Against_reject_no,'')='' then TSPL_MILK_RECEIPT_HEAD.shift else TSPL_MILK_REJECT_head.shift end as SHIFT,"
        BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,case when isnull(TSPL_MILK_SAMPLE_DETAIL.TYPE,'')='' then 'Mix' else TSPL_MILK_SAMPLE_DETAIL.TYPE end as Type ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
        BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt"
        BaseQry += " ,TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, case when TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type " + Environment.NewLine +
            ",(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)) as SRN_Net_Amount
,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT "
        If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
            BaseQry += " ,coalesce(TSPL_Primary_Vehicle_Master.Vehicle,TSPL_MILK_SRN_HEAD.VEHICLE_CODE) as VEHICLE_CODE "
        Else
            BaseQry += " ,TSPL_MILK_SRN_HEAD.VEHICLE_CODE "
        End If
        BaseQry += ",cast( case when  TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty=0 then 0 else (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))/TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty end as decimal(18,2)) as RATE
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER 
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as FATQTY
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else ( cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) ) end as decimal(18,2)) as FAT_Rate
,cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as SNFQTY 
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else (cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1)) end as decimal(18,2)) as SNF_Rate
,cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount
,TSPL_MILK_REJECT_DETAIL.Reject_Type  as QBD" + Environment.NewLine +
        " from TSPL_MILK_PURCHASE_INVOICE_DETAIL  " + Environment.NewLine + " Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " + Environment.NewLine + " left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE " + Environment.NewLine
        BaseQry += " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  " + Environment.NewLine + "  Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  " + Environment.NewLine
        BaseQry += " left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine
        BaseQry += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE " + Environment.NewLine + "  left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE " + Environment.NewLine + " Left Outer Join TSPL_VENDOR_MASTER On"
        BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  " + Environment.NewLine + " Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  " + Environment.NewLine + " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code " + Environment.NewLine + " left outer join TSPL_VLC_MASTER_HEAD on"
        BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  " + Environment.NewLine
        BaseQry += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code " + Environment.NewLine
        BaseQry += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code " + Environment.NewLine
        BaseQry += " left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.VEHICLE_CODE = TSPL_MILK_SRN_HEAD.VEHICLE_CODE " + Environment.NewLine
        BaseQry += " left join  (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL"
        BaseQry += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
        BaseQry += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
        BaseQry += " ) as pp group by VSP_CODE,VLC_Code"
        BaseQry += " ) as PaymentProcess on "
        BaseQry += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_Code " + Environment.NewLine
        BaseQry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code " + Environment.NewLine
        BaseQry += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code "
        BaseQry += " left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code " + Environment.NewLine +
        " left join TSPL_MILK_REJECT_head on TSPL_MILK_REJECT_head.doc_code=TSPL_MILK_SRN_HEAD.Against_reject_no
          left outer join  TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE = TSPL_MILK_REJECT_head.DOC_CODE and TSPL_VLC_MASTER_HEAD.VLC_Code  = TSPL_MILK_REJECT_DETAIL.VLC_CODE  and TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT = TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty   
          left outer join (select TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE , TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No as BillNo , convert(varchar,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_Date,103) as BillDate from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No = '" + fndDocNo.Value + "') as TBL_BILL_DETAILS on TBL_BILL_DETAILS.VSP_CODE =
          TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code  
          "
        BaseQry += "  " & whrcls & " "
        Dim dt As New DataTable
        sQuery = BaseQry + " order by vsp_code,convert(datetime,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103),shift desc"

        'sQuery = " select * from (
        '           select 1 as RowRank, XXX1.compLogo1,XXX1.CompName,XXX1.MCC_address,XXX1.fromDate,XXX1.Todate ,XXX1.MCC_CODE , XXX1.MCC_NAME,XXX1.VLC_Code_VLC_Uploader,XXX1.Vendor_Name,XXX1.VSP_CODE,XXX1.Village_Code,XXX1.Village_Name,XXX1.CycleNo,XXX1.BillNo, XXX1.BillDate, XXX1.PC_FATValue, XXX1.PC_SNFValue, XXX1.DOC_DATE, XXX1.SHIFT, XXX1.CowBuffalo_Type,XXX1.Qty , XXX1.FAT_PER , XXX1.SNF_PER , XXX1.FATQTY, XXX1.SNFQTY, XXX1.SRN_Net_Amount, '' as QBD  from ( " + BaseQry + "   and len (isnull (Against_reject_no,'') ) <= 0 )XXX1 
        '           Union All
        '           select RowRank, XXX1.compLogo1,XXX1.CompName,XXX1.MCC_address,XXX1.fromDate,XXX1.Todate ,XXX1.MCC_CODE , XXX1.MCC_NAME,XXX1.VLC_Code_VLC_Uploader,XXX1.Vendor_Name,XXX1.VSP_CODE,XXX1.Village_Code,XXX1.Village_Name,XXX1.CycleNo,XXX1.BillNo, XXX1.BillDate, XXX1.PC_FATValue, XXX1.PC_SNFValue, XXX1.DOC_DATE, XXX1.SHIFT, XXX1.CowBuffalo_Type,XXX1.Qty , XXX1.FAT_PER , XXX1.SNF_PER , XXX1.FATQTY, XXX1.SNFQTY, XXX1.SRN_Net_Amount, XXX1.Reject_Type as QBD  from (
        '           select  2 as RowRank,'" & fromDate & "' as fromDate,'" & Todate & "' as Todate,TSPL_COMPANY_MASTER.Comp_Name as CompName,TSPL_MILK_REJECT_DETAIL.VSP_CODE,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1, '" + CycleNo + "' as CycleNo, TBL_BILL_DETAILS.BillNo, TBL_BILL_DETAILS.BillDate, '" + strPC_FATValue + "' as PC_FATValue, '" + strPC_SNFValue + "' as PC_SNFValue,  TSPL_MCC_MASTER.MCC_Code, TSPL_MCC_MASTER.MCC_NAME, TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_VLC_MASTER_HEAD.VLC_Name ,TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, convert (varchar,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE ,TSPL_MILK_REJECT_HEAD.SHIFT , case when TSPL_MILK_REJECT_DETAIL.FAT >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type, MILK_WEIGHT as Qty, TSPL_MILK_REJECT_DETAIL.FAT as FAT_PER, TSPL_MILK_REJECT_DETAIL.snf as SNF_PER, round(TSPL_MILK_REJECT_DETAIL.FAT*TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT/100,2,1 ) as FATQTY ,round(TSPL_MILK_REJECT_DETAIL.SNF *TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT/100,2,1 ) as SNFQTY, TSPL_MILK_REJECT_DETAIL.Amount as SRN_Net_Amount , TSPL_MILK_REJECT_DETAIL.Reject_Type from TSPL_MILK_REJECT_DETAIL left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_DETAIL.DOC_CODE = TSPL_MILK_REJECT_HEAD.DOC_CODE
        '           left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_REJECT_HEAD.MCC_CODE
        '           left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_MILK_REJECT_DETAIL.VSP_CODE 
        '           left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code 
        '           left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =TSPL_MILK_REJECT_HEAD.Comp_Code 
        '           left outer join (select TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE , TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No as BillNo , convert(varchar,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_Date,103) as BillDate from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No = '" + fndDocNo.Value + "') as TBL_BILL_DETAILS on TBL_BILL_DETAILS.VSP_CODE = TSPL_MILK_REJECT_DETAIL.VSP_CODE
        '           left outer join  TSPL_PAYMENT_PROCESS_DETAIL  as pp  on pp.VSP_CODE=TSPL_MILK_REJECT_DETAIL.VSP_CODE and PP.Doc_No='" + fndDocNo.Value + "'
        '           left outer join  TSPL_PAYMENT_PROCESS_HEAD  as ppH  on ppH.Doc_No=pp.Doc_No
        '           left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code 
        '           left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code 
        '           Left Outer Join TSPL_VENDOR_MASTER On TSPL_MILK_REJECT_DETAIL.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'
        '           where 2=2 and convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) 
        '           and TSPL_MILK_REJECT_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")  
        '           ) XXX1 )XXXFinal order by RowRank, vsp_code,convert(datetime,DOC_DATE,103),shift desc
        '          "
        dt = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select Vendor_CODE as VSP_Code,trans_type,round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as  FAT_Amount,Amount - round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as SNF_Amount,coalesce(Amount,0) as Amount from (" + Environment.NewLine +
"select doc_no,Vendor_CODE,SNo,trans_type,sum(Amount) as Amount from( " + Environment.NewLine +
"select case when RefDocType ='VSP-COM' then 'EMP' else (case when RefDocType ='PRO-VFC' then 'PRO ADD.' else 'OTHER ADD.' end ) end as trans_type,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,1 as   Show_FAT_SNF , case when RefDocType ='VSP-COM' then 1 else (case when RefDocType ='PRO-VFC' then 3 else 2 end ) end as SNo" + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   " + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No" + Environment.NewLine +
"where TSPL_VENDOR_INVOICE_HEAD.RefDocType not in ('VSP-DIT') and TSPL_VENDOR_INVOICE_HEAD.Description = 'AP Credit Note For VSP Commission' and TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no  = ('" + fndDocNo.Value + "')" + Environment.NewLine +
"union all " + Environment.NewLine +
"select case when RefDocType='VSP-QLT' then 'CBD' else case when RefDocType='MILK-REJ' then 'Rejection Ch.' else (case when RefDocType ='PRO-VFD' then 'PRO DED.' else 'OTHER DED.' end ) end end as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
" TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Amount ,case when RefDocType='MILK-REJ' then 1 else TSPL_DEDUCTION_MASTER.Show_FAT_SNF  end Show_FAT_SNF, case when RefDocType='VSP-QLT' then 4 else case when RefDocType='MILK-REJ' then 5 else (case when RefDocType ='PRO-VFD' then 7 else 6 end ) end end as SNo" + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
"left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine +
" where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and (TSPL_DEDUCTION_MASTER.Show_FAT_SNF=1 or RefDocType='MILK-REJ')" + Environment.NewLine +
")xxx group by doc_no, Vendor_CODE,SNo,trans_type" + Environment.NewLine +
        " ) as final " + Environment.NewLine +
        " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no" + Environment.NewLine +
        "left outer join (select vsp_code,sum(FAT_Amount) as FAT_Amount,sum(SNF_Amount) as SNF_Amount from (" + BaseQry + ")x group by vsp_code)Tab on tab.VSP_CODE=final.Vendor_CODE"
        sQuery += " " & whrclsItemWise & " "
        Dim dtFATNSFDCNote As DataTable = clsDBFuncationality.GetDataTable(sQuery)





        sQuery = " select * from ( select Customer_CODE as VSP_Code,trans_type,Item_Code as VLC_Code_VLC_Uploader ,Item_Desc,0 as FAT_Amount,0 as SNF_Amount,coalesce(Amount,0) as Amount , AP_Invoice_Date,  isnull(TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos,0) as Is_Default_Pashu_Vikash_Kos, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code from (" + Environment.NewLine +
        "select max(trans_type) as trans_type,max(doc_no) as doc_no,max(shipment_doc_no) as AP_Invoice_No,(item_code) as	item_code	, max(Item_Desc) as	Item_Desc,customer_code	,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF , max( AP_Invoice_Date) as AP_Invoice_Date from(" + Environment.NewLine +
        "select 'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Item_net_amt*(-1) as Amount ,0 as Show_FAT_SNF  , case when len (TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_Date) > 0 then convert (varchar,TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_Date,103) else '' end as AP_Invoice_Date  " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code    where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  = ('" + fndDocNo.Value + "')" + Environment.NewLine +
")xxx group by customer_code,item_code" + Environment.NewLine +
"union all " + Environment.NewLine +
"select 'MCC Sale Return' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Doc_No ,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no,TSPL_SD_SALE_RETURN_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.amount as Paymnet_Amount ,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF , case when len (TSPL_PAYMENT_PROCESS_MCC_SALE_Return.AR_Invoice_Date) > 0 then convert (varchar,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.AR_Invoice_Date,103) else '' end as AP_Invoice_Date " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE_Return  " + Environment.NewLine +
"left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code   " + Environment.NewLine +
"union all
select max(trans_type) as trans_type,max(doc_no) as doc_no,max(Payment_No) as AP_Invoice_No,'' as	item_code	,'Asset Lost' as Item_Desc,Vendor_Code as customer_code,0 as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF,  '' as AP_Invoice_Date from(
select 'Asset Lost' as trans_type,TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Vendor_Code,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_Amount*(-1) as Amount ,0 as Show_FAT_SNF 
from TSPL_PAYMENT_PROCESS_ASSET_LOST 
 where TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No  = ('" + fndDocNo.Value + "')
)xxx group by Vendor_Code" + Environment.NewLine +
"union all " + Environment.NewLine +
"select 'Item Issue' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt*(-1) as Item_Net_Amt ,0 as Show_FAT_SNF , '' as AP_Invoice_Date " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE  " + Environment.NewLine +
"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine +
"union all " + Environment.NewLine +
"select 'Item Issue Return' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF  , '' as AP_Invoice_Date " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN  " + Environment.NewLine +
"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine +
"union all " + Environment.NewLine +
"select max(trans_type) as trans_type,max(doc_no) as doc_no,max(AP_Invoice_No) as AP_Invoice_No,max(Vendor_CODE) as	Vendor_CODE	,Item_Desc,Vendor_CODE	,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Item_Net_AMount) as	Item_Net_AMount	,max(Show_FAT_SNF) as	Show_FAT_SNF, AP_Invoice_Date  from( " + Environment.NewLine +
"select RefDocType,'Deduction' as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
"case when TSPL_DEDUCTION_MASTER.Code is not null then TSPL_DEDUCTION_MASTER.Description else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc end as Item_Desc ,0 as Paymnet_Amount ,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Item_Net_AMount ,TSPL_DEDUCTION_MASTER.Show_FAT_SNF, convert (varchar,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_Date,103) as AP_Invoice_Date   " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
"left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine + " where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and not ( RefDocType='MILK-REJ')" + Environment.NewLine +     ' TSPL_DEDUCTION_MASTER.Show_FAT_SNF=1 or
")xxx group by RefDocType,Vendor_CODE,Item_Desc,AP_Invoice_Date " + Environment.NewLine
        sQuery += " ) as final "
        sQuery += " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no" + Environment.NewLine +
        "left outer join (select vsp_code,sum(FAT_Amount) as FAT_Amount,sum(SNF_Amount) as SNF_Amount from (" + BaseQry + ")x group by vsp_code)Tab on tab.VSP_CODE=final.Customer_CODE
         
         left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Description=final.Item_Desc
         left outer join (select distinct VSP_Code ,VLC_Code_VLC_Uploader from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = Item_Code
          
         "
        sQuery += " " & whrclsItemWise & "  Union All  "
        sQuery += " select TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VSP_Code ,TSPL_MULTIPLE_DEDUCTION_head.trans_type   , TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VLC_Code_VLC_Uploader, TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc as Item_Desc , 0 as FAT_Amount,0 as SNF_Amount , TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Amount ,Convert (varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as  AP_Invoice_Date,  0 as Is_Default_Pashu_Vikas_Kos, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code
                    from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
                    left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_MULTIPLE_DEDUCTION_head on TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
                    where  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no in ('" + fndDocNo.Value + "')
                    and TSPL_VENDOR_INVOICE_HEAD.Description <> 'AP Credit Note For VSP Commission'
                    and TSPL_MULTIPLE_DEDUCTION_head.trans_type = 'Addition' 
                    ) Final order by Is_Default_Pashu_Vikash_Kos desc , trans_type desc "
        Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,convert(varchar, TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_REJECT_HEAD.SHIFT,TSPL_MILK_REJECT_DETAIL.Reject_Type,TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,UOM_Code,TSPL_MILK_REJECT_DETAIL.RATE, TSPL_PAYMENT_PROCESS_DEDUCTION.Amount   " + Environment.NewLine +
        "from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
        "left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
        "left outer join TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE=TSPL_VENDOR_INVOICE_HEAD.RefDocNo and TSPL_MILK_REJECT_DETAIL.SAMPLE_NO=TSPL_VENDOR_INVOICE_HEAD.Ref_SNo " + Environment.NewLine +
        "left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE" + Environment.NewLine +
        "where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and RefDocType='MILK-REJ'"
        Dim dtRej As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,  TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc" + Environment.NewLine +
",TSPL_SD_SHIPMENT_detail.Qty as MILK_WEIGHT,case when isnull(TSPL_SD_SHIPMENT_detail.Qty,0)<=0 then 0 else cast(TSPL_PAYMENT_PROCESS_MCC_SALE.Amount/TSPL_SD_SHIPMENT_detail.Qty as decimal(18,2)) end as RATE,TSPL_PAYMENT_PROCESS_MCC_SALE.Amount  " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code    " + Environment.NewLine +
"where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  = ('" + fndDocNo.Value + "')"
        Dim dtSale As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(False, CrystalReportFolder.MilkProcurement, dt, dtgv, "crptMilkPurchaseBillPaymentProcess", "", Nothing, "SubMilkPurchaseBill.rpt", "Address.rpt", Nothing, "SubMilkPurchaseBillRejection.rpt", dtRej, "SubMilkPurchaseBillMCCSale.rpt", dtSale, "SubMilkPurchaseBillFATSNFDebitCreditNote.rpt", dtFATNSFDCNote)
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub

    Public Sub Load_Report_Paymnet_BHBA()
        Dim isPrintProData As Boolean = False
        Dim sQuery As String = "select sum(case when InvoiceNo is null then 0 else 1 end) as Pro,sum(case when Milk_Purchase_Invoice_No is null then 0 else 1 end) as Inv from ( " + Environment.NewLine +
"select TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No  from TSPL_PAYMENT_PROCESS_DETAIL " + Environment.NewLine +
"left join TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS on TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No" + Environment.NewLine +
"where doc_no='" + fndDocNo.Value + "' " + Environment.NewLine +
"group by TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No" + Environment.NewLine +
")xx"
        Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing AndAlso dtgv.Rows.Count > 0 Then
            If clsCommon.myCdbl(dtgv.Rows(0)("Pro")) > 0 Then
                If clsCommon.myCdbl(dtgv.Rows(0)("Pro")) = clsCommon.myCdbl(dtgv.Rows(0)("Inv")) Then
                    isPrintProData = True
                ElseIf clsCommon.MyMessageBoxShow(Me, "Do you want to print Pro Bill", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    isPrintProData = True
                End If
            End If
        End If
        If isPrintProData Then
            Load_Report_Paymnet_BHBAPro()
        Else
            Load_Report_Paymnet_BHBANormal()
        End If
    End Sub
    Public Sub Load_Report_Paymnet_BHBAPro()
        Dim settProStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.BholeBabaPaymentProcessProPrintStartDate, clsFixedParameterCode.BholeBabaPaymentProcessProPrintStartDate, Nothing)
        Dim IsNewFormatApplicable As Boolean = False
        If clsCommon.myLen(settProStartDate) > 0 Then
            If clsCommon.GetDateWithStartTime(clsCommon.myCDate(settProStartDate)) <= clsCommon.GetDateWithStartTime(dtpFromDate.Value) Then
                IsNewFormatApplicable = True
            End If
        End If

        If IsNewFormatApplicable Then
            Load_Report_Paymnet_BHBAProNewFormat()
        Else
            Load_Report_Paymnet_BHBAProOLD()
        End If
    End Sub

    Public Sub Load_Report_Paymnet_BHBAProNewFormat()
        Dim companyADD, CompName, CompCode As String

        Dim sQuery As String = ""
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

        whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN ('" + fndLoc.Value + "') "
        End If
        whrcls += " and exists(select 1 from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS where TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE) "


        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_Head.doc_no='" + fndDocNo.Value + "'"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN ('" + fndLoc.Value + "') "
        End If


        whrclsItemWise += " and final.doc_no in ('" + fndDocNo.Value + "')"
        whrclsItemWise += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "

        Dim BaseQry As String = ""
        BaseQry = "select "
        BaseQry += " isnull(TSPL_VENDOR_MASTER.Actual_charges,0) as Actual_charges,isnull (TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load ,isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount,0) as Handling_Charges_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPI_Code,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, "
        BaseQry += "    '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate"
        BaseQry += " ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,coalesce(PaymentProcess.Total_EMP_Amount,0) as Total_EMP_Amount,coalesce(PaymentProcess.Incentive_Amount,0) as Incentive_Amount ,coalesce(PaymentProcess.Incentive_EMP_Amount,0) as Incentive_EMP_Amount ,coalesce(PaymentProcess.EMP_Amount,0) as EMP_Amount ,coalesce(PaymentProcess.Vsp_Own_System_Amount,0) as Vsp_Own_System_Amount ,coalesce(PaymentProcess.Head_Load_Amount,0) as Head_Load_Amount ,coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0)*(-1) as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty "
        BaseQry += " ,case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  (Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0)) end as Standard_Rate" + Environment.NewLine +
        ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.Fat_ratio)/Price_Chart.FAT_Pers) as decimal(18,2)) end as Standard_FAT_Rate" + Environment.NewLine +
        ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.SNF_Ratio)/Price_Chart.SNF_Pers) as decimal(18,2)) end as Standard_SNF_Rate" + Environment.NewLine +
        ",TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_RO_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_SRN_head.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,case when isnull(TSPL_MILK_SRN_HEAD.Against_reject_no,'')='' then TSPL_MILK_RECEIPT_HEAD.shift else TSPL_MILK_REJECT_head.shift end as SHIFT,"
        BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,case when isnull(TSPL_MILK_SAMPLE_DETAIL.TYPE,'')='' then 'Mix' else TSPL_MILK_SAMPLE_DETAIL.TYPE end as Type ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
        BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt"
        BaseQry += " ,TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, case when TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type " + Environment.NewLine +
            ",(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)) as SRN_Net_Amount
,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT"
        If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
            BaseQry += " ,coalesce(TSPL_Primary_Vehicle_Master.Vehicle,TSPL_MILK_SRN_HEAD.VEHICLE_CODE) as VEHICLE_CODE "
        Else
            BaseQry += " ,TSPL_MILK_SRN_HEAD.VEHICLE_CODE "
        End If

        BaseQry += " ,cast( case when  TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty=0 then 0 else (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))/TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty end as decimal(18,2)) as RATE
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER 
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as FATQTY
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else ( cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) ) end as decimal(18,2)) as FAT_Rate
,cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as SNFQTY 
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else (cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1)) end as decimal(18,2)) as SNF_Rate
,cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount
,(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount * TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply )  as QBD" + Environment.NewLine +
        " from TSPL_MILK_PURCHASE_INVOICE_DETAIL  " + Environment.NewLine + " Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " + Environment.NewLine + " left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE " + Environment.NewLine
        BaseQry += " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  " + Environment.NewLine + "  Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  " + Environment.NewLine
        BaseQry += " left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine
        BaseQry += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE " + Environment.NewLine + "  left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE " + Environment.NewLine + " Left Outer Join TSPL_VENDOR_MASTER On"
        BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  " + Environment.NewLine + " Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  " + Environment.NewLine + " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code " + Environment.NewLine + " left outer join TSPL_VLC_MASTER_HEAD on"
        BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  " + Environment.NewLine
        BaseQry += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code " + Environment.NewLine
        BaseQry += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code " + Environment.NewLine
        BaseQry += " left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.VEHICLE_CODE = TSPL_MILK_SRN_HEAD.VEHICLE_CODE "
        BaseQry += " left join  (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL"
        BaseQry += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
        BaseQry += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
        BaseQry += " ) as pp group by VSP_CODE,VLC_Code"
        BaseQry += " ) as PaymentProcess on "
        BaseQry += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_Code " + Environment.NewLine
        BaseQry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code " + Environment.NewLine
        BaseQry += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code "
        BaseQry += " left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code " + Environment.NewLine +
        " left join TSPL_MILK_REJECT_head on TSPL_MILK_REJECT_head.doc_code=TSPL_MILK_SRN_HEAD.Against_reject_no"
        BaseQry += "  " & whrcls & " "
        Dim dt As New DataTable


        sQuery = "select xxx.*" + Environment.NewLine +
",case when isnull(TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_Qty,0)=0 then null else cast(((TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_FAT_KG*100/TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_Qty)) as decimal(18,2)) end as Farmer_FAT_Per  " + Environment.NewLine +
",case when isnull(TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_Qty,0)=0 then null else cast(((TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_SNF_KG*100/TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_Qty)) as decimal(18,2)) end as Farmer_SNF_Per " + Environment.NewLine +
", TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.FarmerAmt,(TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.FATLossAmt+TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.SNFLossAmt) as FarmerLoss"

        sQuery += ",case when isnull( Pro_Loss_Check.Farmer_Qty,0) =0 then xxx.Qty else TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_Qty end as Farmer_Qty
,case when isnull( Pro_Loss_Check.Farmer_Qty,0) =0 then xxx.FATQTY else TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_FAT_KG end as Farmer_FAT_KG
,case when isnull( Pro_Loss_Check.Farmer_Qty,0) =0 then xxx.SNFQTY else TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_SNF_KG end as Farmer_SNF_KG
,case when isnull( Pro_Loss_Check.Farmer_Qty,0) =0 then SRN_Net_Amount else TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.NetMilkAmt end as NetMilkAmt "

        sQuery += " from (" + Environment.NewLine +
"select ROW_NUMBER() over (Partition by VSP_CODE,DOC_DATE,SHIFT order by VSP_CODE,DOC_DATE,SHIFT) as SNO, * from (" + Environment.NewLine + BaseQry + Environment.NewLine +
" )xx" + Environment.NewLine +
 ")xxx left outer join TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS on TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=xxx.MPI_Code and xxx.sno=1 and xxx.DOC_DATE=convert(varchar,TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Doc_Date,103) and xxx.SHIFT=TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Shift" + Environment.NewLine +
 "left outer join TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS as Pro_Loss_Check on Pro_Loss_Check.InvoiceNo=xxx.MPI_Code and  xxx.DOC_DATE=convert(varchar,Pro_Loss_Check.Doc_Date,103) and xxx.SHIFT=Pro_Loss_Check.Shift " + Environment.NewLine +
 "order by vsp_code,convert(datetime,xxx.DOC_DATE,103),shift desc"
        dt = clsDBFuncationality.GetDataTable(sQuery)








        '        sQuery = "select Customer_CODE as VSP_Code,trans_type,Item_Code as VLC_Code_VLC_Uploader ,Item_Desc,0 as FAT_Amount,0 as SNF_Amount,coalesce(Amount,0) as Amount from (" + Environment.NewLine +
        '           "select max(trans_type) as trans_type,max(doc_no) as doc_no,max(AP_Invoice_No) as AP_Invoice_No,max(Vendor_CODE) as	Item_CODE	,Item_Desc,Vendor_CODE as customer_code,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Item_Net_AMount) as	Amount,max(Show_FAT_SNF) as	Show_FAT_SNF,max(SNO) as SNO from( " + Environment.NewLine +
        '"select RefDocType,'Deduction' as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
        '"case when TSPL_DEDUCTION_MASTER.Code is not null then TSPL_DEDUCTION_MASTER.Description else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc end as Item_Desc ,0 as Paymnet_Amount ,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Item_Net_AMount ,TSPL_DEDUCTION_MASTER.Show_FAT_SNF, case when RefDocType='PRO-LCS' then 1 else case when RefDocType='PRO-STD' then 2 else case when RefDocType='MILK-REJ' then 3 else 10 end end end as SNO " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
        '"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
        '"left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
        '"left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine + " where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "')  and RefDocType not in ('PRO-VFD','PRO-VFC' ) " + Environment.NewLine +
        '")xxx group by RefDocType,Vendor_CODE,Item_Desc" + Environment.NewLine +
        '"union all " + Environment.NewLine +
        '       "select max(trans_type) as trans_type,max(doc_no) as doc_no,max(AP_Invoice_No) as AP_Invoice_No,max(Vendor_CODE) as	Item_CODE	,Item_Desc,Vendor_CODE as customer_code,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Item_Net_AMount) as	Amount,max(Show_FAT_SNF) as	Show_FAT_SNF,max(SNO) as SNO from( " + Environment.NewLine +
        '"select RefDocType,'Addition' as trans_type,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
        ',case when RefDocType ='VSP-COM' then 'EMP' else (case when RefDocType ='PRO-VFC' then 'PRO ADD.' else 'OTHER ADD.' end ) end  as Item_Desc,0 as Paymnet_Amount,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Item_Net_AMount,0 as Show_FAT_SNF,case when  RefDocType ='VSP-COM' then 4 else 10 end as SNO    
        'from TSPL_PAYMENT_PROCESS_CREDIT_NOTE 
        'left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
        'left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1
        'where TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no = ('" + fndDocNo.Value + "') and RefDocType not in ('VSP-DIT','PRO-VFC') 
        ')xxx group by RefDocType,Vendor_CODE,Item_Desc" + Environment.NewLine +
        '         "union all " + Environment.NewLine +
        '        "select max(trans_type) as trans_type,max(doc_no) as doc_no,max(shipment_doc_no) as AP_Invoice_No,max(item_code) as	item_code	,'Sales' as	Item_Desc,customer_code	,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF,10 as SNO from(" + Environment.NewLine +
        '        "select 'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Item_net_amt*(-1) as Amount ,0 as Show_FAT_SNF " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
        '"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
        '"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code    where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  = ('" + fndDocNo.Value + "')" + Environment.NewLine +
        '")xxx group by customer_code" + Environment.NewLine +
        '"union all " + Environment.NewLine +
        '"select 'MCC Sale Return' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Doc_No ,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no,TSPL_SD_SALE_RETURN_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.amount as Paymnet_Amount ,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF,10 as SNO " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_MCC_SALE_Return  " + Environment.NewLine +
        '"left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no " + Environment.NewLine +
        '"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code   " + Environment.NewLine +
        '"union all
        'select max(trans_type) as trans_type,max(doc_no) as doc_no,max(Payment_No) as AP_Invoice_No,'' as	item_code	,'Asset Lost' as Item_Desc,Vendor_Code as customer_code,0 as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF,10 as SNO from(
        'select 'Asset Lost' as trans_type,TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Vendor_Code,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_Amount*(-1) as Amount ,0 as Show_FAT_SNF 
        'from TSPL_PAYMENT_PROCESS_ASSET_LOST 
        ' where TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No  = ('" + fndDocNo.Value + "')
        ')xxx group by Vendor_Code" + Environment.NewLine +
        '"union all
        'select max(trans_type) as trans_type,max(doc_no) as doc_no,max(Payment_No) as AP_Invoice_No,'' as	item_code	,'Advnace' as Item_Desc,Vendor_Code as customer_code,0 as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF,10 as SNO from(
        'select 'Advnace' as trans_type,TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.doc_no,TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Payment_No ,TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Vendor_Code,TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Amount_Knock_Off*(-1) as Amount ,0 as Show_FAT_SNF 
        'from TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT
        ' where TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Doc_No  = ('" + fndDocNo.Value + "')
        ')xxx group by Vendor_Code" + Environment.NewLine +
        '"union all " + Environment.NewLine +
        '"select 'Item Issue' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt*(-1) as Item_Net_Amt ,0 as Show_FAT_SNF,10 as SNO" + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE  " + Environment.NewLine +
        '"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no " + Environment.NewLine +
        '"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine +
        '"union all " + Environment.NewLine +
        '"select 'Item Issue Return' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF ,10 as SNO" + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN  " + Environment.NewLine +
        '"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no " + Environment.NewLine +
        '"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine
        '        sQuery += " ) as final "
        '        sQuery += " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no" + Environment.NewLine
        '        sQuery += " " & whrclsItemWise & " order by Customer_CODE,sno "


        sQuery = "select Is_MILK,Customer_CODE as VSP_Code,trans_type,Item_Code as VLC_Code_VLC_Uploader ,Item_Desc,0 as FAT_Amount,0 as SNF_Amount,coalesce(Amount,0) as Amount from (" + Environment.NewLine +
           " select Is_MILK,max(trans_type) as trans_type,max(doc_no) as doc_no,max(AP_Invoice_No) as AP_Invoice_No,max(Vendor_CODE) as	Item_CODE	,Item_Desc,Vendor_CODE as customer_code,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Item_Net_AMount) as	Amount,max(Show_FAT_SNF) as	Show_FAT_SNF,max(SNO) as SNO from( " + Environment.NewLine +
"select isnull(TSPL_DEDUCTION_MASTER.Is_MILK,0) as Is_MILK,RefDocType,'Deduction' as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
"case when TSPL_DEDUCTION_MASTER.Code is not null then TSPL_DEDUCTION_MASTER.Description else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc end as Item_Desc ,0 as Paymnet_Amount ,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Item_Net_AMount ,TSPL_DEDUCTION_MASTER.Show_FAT_SNF, case when RefDocType='PRO-LCS' then 1 else case when RefDocType='PRO-STD' then 2 else case when RefDocType='MILK-REJ' then 3 else 10 end end end as SNO " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
"left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine + " where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "')  and RefDocType not in ('PRO-VFD','PRO-VFC' )  and  case when TSPL_DEDUCTION_MASTER.Code is not null then TSPL_DEDUCTION_MASTER.Description else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc end <> 'R/M MACHINERY (-)' " + Environment.NewLine +
" Union all " + Environment.NewLine +
" select 1 as Is_MILK,'PRO-STD' as RefDocType,'Deduction' as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
" 'Std Deduction (short received)' as Item_Desc ,0 as Paymnet_Amount ,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Item_Net_AMount ,TSPL_DEDUCTION_MASTER.Show_FAT_SNF, case when RefDocType='PRO-LCS' then 1 else case when RefDocType='PRO-STD' then 2 else case when RefDocType='MILK-REJ' then 3 else 10 end end end as SNO " + Environment.NewLine +
" from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
" left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
" left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
" left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine + " where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "')  and RefDocType not in ('PRO-VFD','PRO-VFC' )  and  case when TSPL_DEDUCTION_MASTER.Code is not null then TSPL_DEDUCTION_MASTER.Description else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc end = 'R/M MACHINERY (-)' " + Environment.NewLine +
" )xxx group by RefDocType,Vendor_CODE,Item_Desc,Is_MILK  " + Environment.NewLine +
" union all " + Environment.NewLine +
       "select 0 as Is_MILK,max(trans_type) as trans_type,max(doc_no) as doc_no,max(AP_Invoice_No) as AP_Invoice_No,max(Vendor_CODE) as	Item_CODE	,Item_Desc,Vendor_CODE as customer_code,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Item_Net_AMount) as	Amount,max(Show_FAT_SNF) as	Show_FAT_SNF,max(SNO) as SNO from( " + Environment.NewLine +
"select RefDocType,'Addition' as trans_type,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
,case when RefDocType ='VSP-COM' then 'EMP' else (case when RefDocType ='PRO-VFC' then 'PRO ADD.' else 'OTHER ADD.' end ) end  as Item_Desc,0 as Paymnet_Amount,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Item_Net_AMount,0 as Show_FAT_SNF,case when  RefDocType ='VSP-COM' then 4 else 10 end as SNO    
from TSPL_PAYMENT_PROCESS_CREDIT_NOTE 
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1
where TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no = ('" + fndDocNo.Value + "') and RefDocType not in ('VSP-DIT','PRO-VFC') 
)xxx group by RefDocType,Vendor_CODE,Item_Desc" + Environment.NewLine +
         "union all " + Environment.NewLine +
        "select 0 as Is_MILK,max(trans_type) as trans_type,max(doc_no) as doc_no,max(shipment_doc_no) as AP_Invoice_No,max(item_code) as	item_code	,'Sales' as	Item_Desc,customer_code	,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF,10 as SNO from(" + Environment.NewLine +
        "select 'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Item_net_amt*(-1) as Amount ,0 as Show_FAT_SNF " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code    where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  = ('" + fndDocNo.Value + "')" + Environment.NewLine +
")xxx group by customer_code" + Environment.NewLine +
"union all " + Environment.NewLine +
"select 0 as Is_MILK,'MCC Sale Return' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Doc_No ,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no,TSPL_SD_SALE_RETURN_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.amount as Paymnet_Amount ,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF,10 as SNO " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE_Return  " + Environment.NewLine +
"left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code   " + Environment.NewLine +
"union all
select 0 as Is_MILK,max(trans_type) as trans_type,max(doc_no) as doc_no,max(Payment_No) as AP_Invoice_No,'' as	item_code	,'Asset Lost' as Item_Desc,Vendor_Code as customer_code,0 as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF,10 as SNO from(
select 'Asset Lost' as trans_type,TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Vendor_Code,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_Amount*(-1) as Amount ,0 as Show_FAT_SNF 
from TSPL_PAYMENT_PROCESS_ASSET_LOST 
 where TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No  = ('" + fndDocNo.Value + "')
)xxx group by Vendor_Code" + Environment.NewLine +
"union all
select 0 as Is_MILK,max(trans_type) as trans_type,max(doc_no) as doc_no,max(Payment_No) as AP_Invoice_No,'' as	item_code	,'Advnace' as Item_Desc,Vendor_Code as customer_code,0 as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF,10 as SNO from(
select 'Advnace' as trans_type,TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.doc_no,TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Payment_No ,TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Vendor_Code,TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Amount_Knock_Off*(-1) as Amount ,0 as Show_FAT_SNF 
from TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT
 where TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Doc_No  = ('" + fndDocNo.Value + "')
)xxx group by Vendor_Code" + Environment.NewLine +
"union all " + Environment.NewLine +
"select 0 as Is_MILK,'Item Issue' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt*(-1) as Item_Net_Amt ,0 as Show_FAT_SNF,10 as SNO" + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE  " + Environment.NewLine +
"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine +
"union all " + Environment.NewLine +
"select 0 as Is_MILK,'Item Issue Return' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF ,10 as SNO" + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN  " + Environment.NewLine +
"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine
        sQuery += " ) as final "
        sQuery += " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no" + Environment.NewLine
        sQuery += " " & whrclsItemWise & " order by Customer_CODE,sno "

        Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(sQuery)


        sQuery = "select  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,convert(varchar, TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_REJECT_HEAD.SHIFT,TSPL_MILK_REJECT_DETAIL.Reject_Type,TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,UOM_Code,TSPL_MILK_REJECT_DETAIL.RATE, TSPL_PAYMENT_PROCESS_DEDUCTION.Amount   " + Environment.NewLine +
        "from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
        "left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
        "left outer join TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE=TSPL_VENDOR_INVOICE_HEAD.RefDocNo and TSPL_MILK_REJECT_DETAIL.SAMPLE_NO=TSPL_VENDOR_INVOICE_HEAD.Ref_SNo " + Environment.NewLine +
        "left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE" + Environment.NewLine +
        "where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and RefDocType='MILK-REJ'"
        Dim dtRej As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,  TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc" + Environment.NewLine +
",TSPL_SD_SHIPMENT_detail.Qty as MILK_WEIGHT,case when isnull( TSPL_SD_SHIPMENT_detail.Qty,0)<=0 then 0 else cast(TSPL_PAYMENT_PROCESS_MCC_SALE.Amount/TSPL_SD_SHIPMENT_detail.Qty as decimal(18,2)) end as RATE,TSPL_PAYMENT_PROCESS_MCC_SALE.Amount  " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code    " + Environment.NewLine +
"where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  = ('" + fndDocNo.Value + "')"
        Dim dtSale As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(False, CrystalReportFolder.MilkProcurement, dt, dtgv, "crptMilkPurchaseBillPaymentProcessPRO2", "", Nothing, "SubMilkPurchaseBill.rpt", "Address.rpt", Nothing, "SubNonMilkPurchaseBill.rpt", dtgv, "SubMilkPurchaseBillRejection.rpt", dtRej, "SubMilkPurchaseBillMCCSale.rpt", dtSale)
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub

    Public Sub Load_Report_Paymnet_BHBAProOLD()
        Dim companyADD, CompName, CompCode As String

        Dim sQuery As String = ""
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

        whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN ('" + fndLoc.Value + "') "
        End If
        whrcls += " and exists(select 1 from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS where TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE) "


        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_Head.doc_no='" + fndDocNo.Value + "'"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN ('" + fndLoc.Value + "') "
        End If


        whrclsItemWise += " and final.doc_no in ('" + fndDocNo.Value + "')"
        whrclsItemWise += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "

        Dim BaseQry As String = ""
        BaseQry = "select "
        BaseQry += " isnull(TSPL_VENDOR_MASTER.Actual_charges,0) as Actual_charges,isnull (TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load ,isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount,0) as Handling_Charges_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPI_Code,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, "
        BaseQry += "    '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate"
        BaseQry += " ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,coalesce(PaymentProcess.Total_EMP_Amount,0) as Total_EMP_Amount,coalesce(PaymentProcess.Incentive_Amount,0) as Incentive_Amount ,coalesce(PaymentProcess.Incentive_EMP_Amount,0) as Incentive_EMP_Amount ,coalesce(PaymentProcess.EMP_Amount,0) as EMP_Amount ,coalesce(PaymentProcess.Vsp_Own_System_Amount,0) as Vsp_Own_System_Amount ,coalesce(PaymentProcess.Head_Load_Amount,0) as Head_Load_Amount ,coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0)*(-1) as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty "
        BaseQry += " ,case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  (Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0)) end as Standard_Rate" + Environment.NewLine +
        ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.Fat_ratio)/Price_Chart.FAT_Pers) as decimal(18,2)) end as Standard_FAT_Rate" + Environment.NewLine +
        ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.SNF_Ratio)/Price_Chart.SNF_Pers) as decimal(18,2)) end as Standard_SNF_Rate" + Environment.NewLine +
        ",TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_RO_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_SRN_head.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,case when isnull(TSPL_MILK_SRN_HEAD.Against_reject_no,'')='' then TSPL_MILK_RECEIPT_HEAD.shift else TSPL_MILK_REJECT_head.shift end as SHIFT,"
        BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,case when isnull(TSPL_MILK_SAMPLE_DETAIL.TYPE,'')='' then 'Mix' else TSPL_MILK_SAMPLE_DETAIL.TYPE end as Type ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
        BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt"
        BaseQry += " ,TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, case when TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type " + Environment.NewLine +
            ",(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)) as SRN_Net_Amount
,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT"
        If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
            BaseQry += " ,coalesce(TSPL_Primary_Vehicle_Master.Vehicle,TSPL_MILK_SRN_HEAD.VEHICLE_CODE) as VEHICLE_CODE "
        Else
            BaseQry += " ,TSPL_MILK_SRN_HEAD.VEHICLE_CODE "
        End If

        BaseQry += " ,cast( case when  TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty=0 then 0 else (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))/TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty end as decimal(18,2)) as RATE
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER 
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as FATQTY
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else ( cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) ) end as decimal(18,2)) as FAT_Rate
,cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as SNFQTY 
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else (cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1)) end as decimal(18,2)) as SNF_Rate
,cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount
,(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount * TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply )  as QBD" + Environment.NewLine +
        " from TSPL_MILK_PURCHASE_INVOICE_DETAIL  " + Environment.NewLine + " Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " + Environment.NewLine + " left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE " + Environment.NewLine
        BaseQry += " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  " + Environment.NewLine + "  Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  " + Environment.NewLine
        BaseQry += " left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine
        BaseQry += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE " + Environment.NewLine + "  left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE " + Environment.NewLine + " Left Outer Join TSPL_VENDOR_MASTER On"
        BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  " + Environment.NewLine + " Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  " + Environment.NewLine + " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code " + Environment.NewLine + " left outer join TSPL_VLC_MASTER_HEAD on"
        BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  " + Environment.NewLine
        BaseQry += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code " + Environment.NewLine
        BaseQry += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code " + Environment.NewLine
        BaseQry += " left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.VEHICLE_CODE = TSPL_MILK_SRN_HEAD.VEHICLE_CODE "
        BaseQry += " left join  (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL"
        BaseQry += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
        BaseQry += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
        BaseQry += " ) as pp group by VSP_CODE,VLC_Code"
        BaseQry += " ) as PaymentProcess on "
        BaseQry += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_Code " + Environment.NewLine
        BaseQry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code " + Environment.NewLine
        BaseQry += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code "
        BaseQry += " left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code " + Environment.NewLine +
        " left join TSPL_MILK_REJECT_head on TSPL_MILK_REJECT_head.doc_code=TSPL_MILK_SRN_HEAD.Against_reject_no"
        BaseQry += "  " & whrcls & " "
        Dim dt As New DataTable


        sQuery = "select xxx.*" + Environment.NewLine +
",case when isnull(TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_Qty,0)=0 then null else cast(((TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_FAT_KG*100/TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_Qty)) as decimal(18,2)) end as Farmer_FAT_Per  " + Environment.NewLine +
",case when isnull(TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_Qty,0)=0 then null else cast(((TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_SNF_KG*100/TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_Qty)) as decimal(18,2)) end as Farmer_SNF_Per " + Environment.NewLine +
", TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.FarmerAmt,(TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.FATLossAmt+TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.SNFLossAmt) as FarmerLoss"

        sQuery += ",TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_Qty,TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_FAT_KG,TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Farmer_SNF_KG,TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.NetMilkAmt"

        sQuery += " from (" + Environment.NewLine +
"select ROW_NUMBER() over (Partition by VSP_CODE,DOC_DATE,SHIFT order by VSP_CODE,DOC_DATE,SHIFT) as SNO, * from (" + Environment.NewLine + BaseQry + Environment.NewLine +
" )xx" + Environment.NewLine +
 ")xxx left outer join TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS on TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=xxx.MPI_Code and xxx.sno=1 and xxx.DOC_DATE=convert(varchar,TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Doc_Date,103) and xxx.SHIFT=TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Shift" + Environment.NewLine +
 "left outer join TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS as Pro_Loss_Check on Pro_Loss_Check.InvoiceNo=xxx.MPI_Code and  xxx.DOC_DATE=convert(varchar,Pro_Loss_Check.Doc_Date,103) and xxx.SHIFT=Pro_Loss_Check.Shift " + Environment.NewLine +
 "order by vsp_code,convert(datetime,xxx.DOC_DATE,103),shift desc"
        dt = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select Vendor_CODE as VSP_Code,trans_type,round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as  FAT_Amount,Amount - round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as SNF_Amount,coalesce(Amount,0) as Amount from (" + Environment.NewLine +
"select doc_no,Vendor_CODE,SNo,trans_type,sum(Amount) as Amount from( " + Environment.NewLine +
"select case when RefDocType ='VSP-COM' then 'EMP' else (case when RefDocType ='PRO-VFC' then 'PRO ADD.' else 'OTHER ADD.' end ) end as trans_type,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,1 as   Show_FAT_SNF , case when RefDocType ='VSP-COM' then 1 else (case when RefDocType ='PRO-VFC' then 3 else 2 end ) end as SNo" + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   " + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No" + Environment.NewLine +
"where TSPL_VENDOR_INVOICE_HEAD.RefDocType not in ('VSP-DIT'"

        sQuery += " ) and TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no  = ('" + fndDocNo.Value + "')" + Environment.NewLine +
"union all " + Environment.NewLine +
"select 'Deduction' as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
" TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Amount ,case when RefDocType='MILK-REJ' then 1 else TSPL_DEDUCTION_MASTER.Show_FAT_SNF  end Show_FAT_SNF, case when RefDocType='VSP-QLT' then 4 else case when RefDocType='MILK-REJ' then 5 else (case when RefDocType ='PRO-VFD' then 7 else 6 end ) end end as SNo" + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
"left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine +
" where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and (TSPL_DEDUCTION_MASTER.Show_FAT_SNF=1 or RefDocType in ('MILK-REJ','PRO-VFD'))"

        sQuery += ")xxx group by doc_no, Vendor_CODE,SNo,trans_type" + Environment.NewLine +
        " ) as final " + Environment.NewLine +
        " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no" + Environment.NewLine +
        "left outer join (select vsp_code,sum(FAT_Amount) as FAT_Amount,sum(SNF_Amount) as SNF_Amount from (" + BaseQry + ")x group by vsp_code)Tab on tab.VSP_CODE=final.Vendor_CODE"
        sQuery += " " & whrclsItemWise & " "
        Dim dtFATNSFDCNote As DataTable = clsDBFuncationality.GetDataTable(sQuery)






        sQuery = "select Customer_CODE as VSP_Code,trans_type,Item_Code as VLC_Code_VLC_Uploader ,Item_Desc,0 as FAT_Amount,0 as SNF_Amount,coalesce(Amount,0) as Amount from (" + Environment.NewLine +
        "select max(trans_type) as trans_type,max(doc_no) as doc_no,max(shipment_doc_no) as AP_Invoice_No,max(item_code) as	item_code	,'Sales' as	Item_Desc,customer_code	,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF from(" + Environment.NewLine +
        "select 'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Item_net_amt*(-1) as Amount ,0 as Show_FAT_SNF " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code    where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  = ('" + fndDocNo.Value + "')" + Environment.NewLine +
")xxx group by customer_code" + Environment.NewLine +
"union all " + Environment.NewLine +
"select 'MCC Sale Return' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Doc_No ,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no,TSPL_SD_SALE_RETURN_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.amount as Paymnet_Amount ,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE_Return  " + Environment.NewLine +
"left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code   " + Environment.NewLine +
"union all
select max(trans_type) as trans_type,max(doc_no) as doc_no,max(Payment_No) as AP_Invoice_No,'' as	item_code	,'Asset Lost' as Item_Desc,Vendor_Code as customer_code,0 as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF from(
select 'Asset Lost' as trans_type,TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Vendor_Code,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_Amount*(-1) as Amount ,0 as Show_FAT_SNF 
from TSPL_PAYMENT_PROCESS_ASSET_LOST 
 where TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No  = ('" + fndDocNo.Value + "')
)xxx group by Vendor_Code" + Environment.NewLine +
"union all " + Environment.NewLine +
"select 'Item Issue' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt*(-1) as Item_Net_Amt ,0 as Show_FAT_SNF" + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE  " + Environment.NewLine +
"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine +
"union all " + Environment.NewLine +
"select 'Item Issue Return' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN  " + Environment.NewLine +
"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine +
"union all " + Environment.NewLine +
"select max(trans_type) as trans_type,max(doc_no) as doc_no,max(AP_Invoice_No) as AP_Invoice_No,max(Vendor_CODE) as	Vendor_CODE	,Item_Desc,Vendor_CODE	,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Item_Net_AMount) as	Item_Net_AMount	,max(Show_FAT_SNF) as	Show_FAT_SNF from( " + Environment.NewLine +
"select RefDocType,'Deduction' as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
"case when TSPL_DEDUCTION_MASTER.Code is not null then TSPL_DEDUCTION_MASTER.Description else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc end as Item_Desc ,0 as Paymnet_Amount ,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Item_Net_AMount ,TSPL_DEDUCTION_MASTER.Show_FAT_SNF " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
"left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine + " where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and not (TSPL_DEDUCTION_MASTER.Show_FAT_SNF=1 or RefDocType='MILK-REJ')" + Environment.NewLine +
")xxx group by RefDocType,Vendor_CODE,Item_Desc" + Environment.NewLine
        sQuery += " ) as final "
        sQuery += " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no" + Environment.NewLine +
        "left outer join (select vsp_code,sum(FAT_Amount) as FAT_Amount,sum(SNF_Amount) as SNF_Amount from (" + BaseQry + ")x group by vsp_code)Tab on tab.VSP_CODE=final.Customer_CODE"
        sQuery += " " & whrclsItemWise & " "
        Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(sQuery)


        sQuery = "select  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,convert(varchar, TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_REJECT_HEAD.SHIFT,TSPL_MILK_REJECT_DETAIL.Reject_Type,TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,UOM_Code,TSPL_MILK_REJECT_DETAIL.RATE, TSPL_PAYMENT_PROCESS_DEDUCTION.Amount   " + Environment.NewLine +
        "from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
        "left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
        "left outer join TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE=TSPL_VENDOR_INVOICE_HEAD.RefDocNo and TSPL_MILK_REJECT_DETAIL.SAMPLE_NO=TSPL_VENDOR_INVOICE_HEAD.Ref_SNo " + Environment.NewLine +
        "left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE" + Environment.NewLine +
        "where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and RefDocType='MILK-REJ'"
        Dim dtRej As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,  TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc" + Environment.NewLine +
",TSPL_SD_SHIPMENT_detail.Qty as MILK_WEIGHT,case when isnull( TSPL_SD_SHIPMENT_detail.Qty,0)<=0 then 0 else cast(TSPL_PAYMENT_PROCESS_MCC_SALE.Amount/TSPL_SD_SHIPMENT_detail.Qty as decimal(18,2)) end as RATE,TSPL_PAYMENT_PROCESS_MCC_SALE.Amount  " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code    " + Environment.NewLine +
"where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  = ('" + fndDocNo.Value + "')"
        Dim dtSale As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(False, CrystalReportFolder.MilkProcurement, dt, dtgv, "crptMilkPurchaseBillPaymentProcessPRO", "", Nothing, "SubMilkPurchaseBill.rpt", "Address.rpt", Nothing, "SubMilkPurchaseBillRejection.rpt", dtRej, "SubMilkPurchaseBillMCCSale.rpt", dtSale, "SubMilkPurchaseBillFATSNFDebitCreditNote.rpt", dtFATNSFDCNote)
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub


    Public Sub Load_Report_Paymnet_BHBANormal()
        Dim companyADD, CompName, CompCode As String

        Dim sQuery As String = ""
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

        whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN ('" + fndLoc.Value + "') "
        End If
        whrcls += " and not exists(select 1 from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS where TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE) "

        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_Head.doc_no='" + fndDocNo.Value + "'"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN ('" + fndLoc.Value + "') "
        End If
        whrclsItemWise += " and final.doc_no in ('" + fndDocNo.Value + "')"
        whrclsItemWise += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "

        Dim BaseQry As String = ""
        BaseQry = "select "
        BaseQry += " isnull(TSPL_VENDOR_MASTER.Actual_charges,0) as Actual_charges,isnull (TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load ,isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount,0) as Handling_Charges_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, "
        BaseQry += "    '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate"
        BaseQry += " ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,coalesce(PaymentProcess.Total_EMP_Amount,0) as Total_EMP_Amount,coalesce(PaymentProcess.Incentive_Amount,0) as Incentive_Amount ,coalesce(PaymentProcess.Incentive_EMP_Amount,0) as Incentive_EMP_Amount ,coalesce(PaymentProcess.EMP_Amount,0) as EMP_Amount ,coalesce(PaymentProcess.Vsp_Own_System_Amount,0) as Vsp_Own_System_Amount ,coalesce(PaymentProcess.Head_Load_Amount,0) as Head_Load_Amount ,coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0)*(-1) as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty "
        BaseQry += " ,case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  (Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0)) end as Standard_Rate" + Environment.NewLine +
        ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.Fat_ratio)/Price_Chart.FAT_Pers) as decimal(18,2)) end as Standard_FAT_Rate" + Environment.NewLine +
        ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.SNF_Ratio)/Price_Chart.SNF_Pers) as decimal(18,2)) end as Standard_SNF_Rate" + Environment.NewLine +
        ",TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_RO_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_SRN_head.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,case when isnull(TSPL_MILK_SRN_HEAD.Against_reject_no,'')='' then TSPL_MILK_RECEIPT_HEAD.shift else TSPL_MILK_REJECT_head.shift end as SHIFT,"
        BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,case when isnull(TSPL_MILK_SAMPLE_DETAIL.TYPE,'')='' then 'Mix' else TSPL_MILK_SAMPLE_DETAIL.TYPE end as Type ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
        BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt"
        BaseQry += " ,TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, case when TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type " + Environment.NewLine +
            ",(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)) as SRN_Net_Amount
,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT "
        If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
            BaseQry += " ,coalesce(TSPL_Primary_Vehicle_Master.Vehicle,TSPL_MILK_SRN_HEAD.VEHICLE_CODE) as VEHICLE_CODE "
        Else
            BaseQry += " ,TSPL_MILK_SRN_HEAD.VEHICLE_CODE "
        End If
        BaseQry += ",cast( case when  TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty=0 then 0 else (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))/TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty end as decimal(18,2)) as RATE
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER 
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as FATQTY
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else ( cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) ) end as decimal(18,2)) as FAT_Rate
,cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as SNFQTY 
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else (cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1)) end as decimal(18,2)) as SNF_Rate
,cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount
,(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount * TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply )  as QBD" + Environment.NewLine +
        " from TSPL_MILK_PURCHASE_INVOICE_DETAIL  " + Environment.NewLine + " Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " + Environment.NewLine + " left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE " + Environment.NewLine
        BaseQry += " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  " + Environment.NewLine + "  Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  " + Environment.NewLine
        BaseQry += " left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine
        BaseQry += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE " + Environment.NewLine + "  left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE " + Environment.NewLine + " Left Outer Join TSPL_VENDOR_MASTER On"
        BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  " + Environment.NewLine + " Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  " + Environment.NewLine + " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code " + Environment.NewLine + " left outer join TSPL_VLC_MASTER_HEAD on"
        BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  " + Environment.NewLine
        BaseQry += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code " + Environment.NewLine
        BaseQry += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code " + Environment.NewLine
        BaseQry += " left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.VEHICLE_CODE = TSPL_MILK_SRN_HEAD.VEHICLE_CODE " + Environment.NewLine
        BaseQry += " left join  (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL"
        BaseQry += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
        BaseQry += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
        BaseQry += " ) as pp group by VSP_CODE,VLC_Code"
        BaseQry += " ) as PaymentProcess on "
        BaseQry += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_Code " + Environment.NewLine
        BaseQry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code " + Environment.NewLine
        BaseQry += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code "
        BaseQry += " left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code " + Environment.NewLine +
        " left join TSPL_MILK_REJECT_head on TSPL_MILK_REJECT_head.doc_code=TSPL_MILK_SRN_HEAD.Against_reject_no"
        BaseQry += "  " & whrcls & " "
        Dim dt As New DataTable
        sQuery = BaseQry + " order by vsp_code,convert(datetime,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103),shift desc"
        dt = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select Vendor_CODE as VSP_Code,trans_type,round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as  FAT_Amount,Amount - round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as SNF_Amount,coalesce(Amount,0) as Amount from (" + Environment.NewLine +
"select doc_no,Vendor_CODE,SNo,trans_type,sum(Amount) as Amount from( " + Environment.NewLine +
"select case when RefDocType ='VSP-COM' then 'EMP' else (case when RefDocType ='PRO-VFC' then 'PRO ADD.' else 'OTHER ADD.' end ) end as trans_type,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,1 as   Show_FAT_SNF , case when RefDocType ='VSP-COM' then 1 else (case when RefDocType ='PRO-VFC' then 3 else 2 end ) end as SNo" + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   " + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No" + Environment.NewLine +
"where TSPL_VENDOR_INVOICE_HEAD.RefDocType not in ('VSP-DIT') and TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no  = ('" + fndDocNo.Value + "')" + Environment.NewLine +
"union all " + Environment.NewLine +
"select case when RefDocType='VSP-QLT' then 'CBD' else case when RefDocType='MILK-REJ' then 'Rejection Ch.' else (case when RefDocType ='PRO-VFD' then 'PRO DED.' else 'OTHER DED.' end ) end end as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
" TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Amount ,case when RefDocType='MILK-REJ' then 1 else TSPL_DEDUCTION_MASTER.Show_FAT_SNF  end Show_FAT_SNF, case when RefDocType='VSP-QLT' then 4 else case when RefDocType='MILK-REJ' then 5 else (case when RefDocType ='PRO-VFD' then 7 else 6 end ) end end as SNo" + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
"left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine +
" where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and (TSPL_DEDUCTION_MASTER.Show_FAT_SNF=1 or RefDocType='MILK-REJ')" + Environment.NewLine +
")xxx group by doc_no, Vendor_CODE,SNo,trans_type" + Environment.NewLine +
        " ) as final " + Environment.NewLine +
        " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no" + Environment.NewLine +
        "left outer join (select vsp_code,sum(FAT_Amount) as FAT_Amount,sum(SNF_Amount) as SNF_Amount from (" + BaseQry + ")x group by vsp_code)Tab on tab.VSP_CODE=final.Vendor_CODE"
        sQuery += " " & whrclsItemWise & " "
        Dim dtFATNSFDCNote As DataTable = clsDBFuncationality.GetDataTable(sQuery)





        sQuery = "select Customer_CODE as VSP_Code,trans_type,Item_Code as VLC_Code_VLC_Uploader ,Item_Desc,0 as FAT_Amount,0 as SNF_Amount,coalesce(Amount,0) as Amount from (" + Environment.NewLine +
        "select max(trans_type) as trans_type,max(doc_no) as doc_no,max(shipment_doc_no) as AP_Invoice_No,max(item_code) as	item_code	,'Sales' as	Item_Desc,customer_code	,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF from(" + Environment.NewLine +
        "select 'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Item_net_amt*(-1) as Amount ,0 as Show_FAT_SNF " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code    where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  = ('" + fndDocNo.Value + "')" + Environment.NewLine +
")xxx group by customer_code" + Environment.NewLine +
"union all " + Environment.NewLine +
"select 'MCC Sale Return' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Doc_No ,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no,TSPL_SD_SALE_RETURN_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.amount as Paymnet_Amount ,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE_Return  " + Environment.NewLine +
"left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code   " + Environment.NewLine +
"union all
select max(trans_type) as trans_type,max(doc_no) as doc_no,max(Payment_No) as AP_Invoice_No,'' as	item_code	,'Asset Lost' as Item_Desc,Vendor_Code as customer_code,0 as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF from(
select 'Asset Lost' as trans_type,TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Vendor_Code,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_Amount*(-1) as Amount ,0 as Show_FAT_SNF 
from TSPL_PAYMENT_PROCESS_ASSET_LOST 
 where TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No  = ('" + fndDocNo.Value + "')
)xxx group by Vendor_Code" + Environment.NewLine +
"union all 
Select  max(trans_type) As trans_type,max(doc_no) As doc_no,max(Payment_No) As AP_Invoice_No,'' as	item_code	,'Advnace' as Item_Desc,Vendor_Code as customer_code,0 as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF from(
Select  'Advnace' as trans_type,TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.doc_no,TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Payment_No ,TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Vendor_Code,TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Amount_Knock_Off*(-1) as Amount ,0 as Show_FAT_SNF 
From TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT
 Where TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Doc_No = ('" + fndDocNo.Value + "')
)xxx Group By Vendor_Code" + Environment.NewLine +
"union all " + Environment.NewLine +
"select 'Item Issue' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt*(-1) as Item_Net_Amt ,0 as Show_FAT_SNF" + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE  " + Environment.NewLine +
"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine +
"union all " + Environment.NewLine +
"select 'Item Issue Return' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN  " + Environment.NewLine +
"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine +
"union all " + Environment.NewLine +
"select max(trans_type) as trans_type,max(doc_no) as doc_no,max(AP_Invoice_No) as AP_Invoice_No,max(Vendor_CODE) as	Vendor_CODE	,Item_Desc,Vendor_CODE	,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Item_Net_AMount) as	Item_Net_AMount	,max(Show_FAT_SNF) as	Show_FAT_SNF from( " + Environment.NewLine +
"select RefDocType,'Deduction' as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
"case when TSPL_DEDUCTION_MASTER.Code is not null then TSPL_DEDUCTION_MASTER.Description else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc end as Item_Desc ,0 as Paymnet_Amount ,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Item_Net_AMount ,TSPL_DEDUCTION_MASTER.Show_FAT_SNF " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
"left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
"left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine + " where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and not (TSPL_DEDUCTION_MASTER.Show_FAT_SNF=1 or RefDocType='MILK-REJ')" + Environment.NewLine +
")xxx group by RefDocType,Vendor_CODE,Item_Desc" + Environment.NewLine
        sQuery += " ) as final "
        sQuery += " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no" + Environment.NewLine +
        "left outer join (select vsp_code,sum(FAT_Amount) as FAT_Amount,sum(SNF_Amount) as SNF_Amount from (" + BaseQry + ")x group by vsp_code)Tab on tab.VSP_CODE=final.Customer_CODE"
        sQuery += " " & whrclsItemWise & " "
        Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,convert(varchar, TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_REJECT_HEAD.SHIFT,TSPL_MILK_REJECT_DETAIL.Reject_Type,TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,UOM_Code,TSPL_MILK_REJECT_DETAIL.RATE, TSPL_PAYMENT_PROCESS_DEDUCTION.Amount   " + Environment.NewLine +
        "from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
        "left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
        "left outer join TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE=TSPL_VENDOR_INVOICE_HEAD.RefDocNo and TSPL_MILK_REJECT_DETAIL.SAMPLE_NO=TSPL_VENDOR_INVOICE_HEAD.Ref_SNo " + Environment.NewLine +
        "left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE" + Environment.NewLine +
        "where TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and RefDocType='MILK-REJ'"
        Dim dtRej As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,  TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc" + Environment.NewLine +
",TSPL_SD_SHIPMENT_detail.Qty as MILK_WEIGHT,case when isnull(TSPL_SD_SHIPMENT_detail.Qty,0)<=0 then 0 else cast(TSPL_PAYMENT_PROCESS_MCC_SALE.Amount/TSPL_SD_SHIPMENT_detail.Qty as decimal(18,2)) end as RATE,TSPL_PAYMENT_PROCESS_MCC_SALE.Amount  " + Environment.NewLine +
"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code    " + Environment.NewLine +
"where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  = ('" + fndDocNo.Value + "')"
        Dim dtSale As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(False, CrystalReportFolder.MilkProcurement, dt, dtgv, "crptMilkPurchaseBillPaymentProcess", "", Nothing, "SubMilkPurchaseBill.rpt", "Address.rpt", Nothing, "SubMilkPurchaseBillRejection.rpt", dtRej, "SubMilkPurchaseBillMCCSale.rpt", dtSale, "SubMilkPurchaseBillFATSNFDebitCreditNote.rpt", dtFATNSFDCNote)
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
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
        sQuery = " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address " &
            " ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.comp_code from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        companyADD = dt1.Rows(0).Item("comp_address")

        CompName = dt1.Rows(0).Item("Comp_Name")

        CompCode = dt1.Rows(0).Item("Comp_Code")

        Dim whrcls As String = " where 2=2 "
        whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN ('" + fndLoc.Value + "') "
        End If
        whrcls += " and  PaymentProcess.doc_no='" + fndDocNo.Value + "'"
        Dim whrcls1 As String = " where 2=2 "
        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN ('" + fndLoc.Value + "') "
        End If

        Dim whrclsDeduction As String = " where 2=2 "
        whrclsDeduction += "  and convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and  convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrclsDeduction += "  and TSPL_MILK_REJECT_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrclsDeduction += " and TSPL_LOCATION_MASTER.Loc_Segment_Code    IN (" + fndLoc.Value + ") "
        End If

        Query = "select final.fdate, tdate, FAT_KG, SNF_KG, Advance_Payment_Amount,Advance_Payment_Amount_Knock_Off, companyADD, CompName, CompCode, Payable_Amount, Qty, Rate, Net_AMOUNT, ROUTE_CODE, Route_Name, final.VLC_Code, final.VSP_CODE, final.VLC_Name " &
         ", GHEE, CATTLEFEED , OTHERS " &
        "from (select      " &
        " convert(varchar,('" + dtpFromDate.Value + "'),103) as fdate,convert(varchar,('" + dtpDate.Value + "'),103) as tdate,sum(TSPL_MILK_SRN_DETAIL .FAT_KG) as FAT_KG, " &
        " sum(TSPL_MILK_SRN_DETAIL .SNF_KG) as SNF_KG,max(ISNULL(MCCSALE.GHEE,0)) AS GHEE,max(ISNULL(MCCSALE.CATTLEFEED,0)) AS CATTLEFEED ,max(ISNULL(MCCSALE.OTHERS,0)) AS OTHERS, " &
         " max(PaymentProcess.Advance_Payment_Amount) as Advance_Payment_Amount,max( Advance_Payment_Amount_Knock_Off) as Advance_Payment_Amount_Knock_Off , max(TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE) as VSP_CODE, " &
        "'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode," &
        "max(PaymentProcess.Payable_Amount) as Payable_Amount,sum(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty)as Qty ,sum( Price_Chart.milk_rate) as RATE, sum(TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT) as Net_AMOUNT, max(TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE) as ROUTE_CODE , max(TSPL_MCC_ROUTE_MASTER .Route_Name) as Route_Name , max(TSPL_VLC_MASTER_HEAD.VLC_Code) as VLC_Code,max(TSPL_VLC_MASTER_HEAD.VLC_Name) as VLC_Name " &
        "from TSPL_MILK_PURCHASE_INVOICE_DETAIL Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD  On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " &
        " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD .DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE " &
        " left outer join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL .DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " &
        "Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE = TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE " &
         "Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE " &
        "left outer join TSPL_MILK_RECEIPT_HEAD  on TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE " &
         "left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE and TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE " &
         "Left Outer Join TSPL_VENDOR_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE = TSPL_VENDOR_MASTER.Vendor_Code  And TSPL_VENDOR_MASTER.Form_Type = 'VSP' " &
         "Left Outer Join TSPL_MCC_MASTER  On TSPL_MILK_RECEIPT_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code " &
         "left join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code = TSPL_MCC_MASTER.MCC_Code " &
         "Left Outer Join TSPL_MCC_ROUTE_MASTER  On TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code " &
         "left outer join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO " &
         "left join TSPL_CITY_MASTER as MCC_City  on MCC_City.city_code = TSPL_MCC_MASTER.City_code " &
         "left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE = TSPL_MCC_MASTER.State_Code " &
         "left join ( select sum(Advance_Payment_Amount) as Advance_Payment_Amount,sum(Advance_Payment_Amount_Knock_Off)as Advance_Payment_Amount_Knock_Off, max(doc_no) as doc_no," &
          "max(convert(varchar, doc_date, 103)) as doc_date, max(from_date) as from_date, max(to_date) as to_date, sum(Service_Charge_Amt ) as Service_Charge_Amt, VLC_Code, VSP_CODE," &
            "sum(Total_EMP_Amount) as Total_EMP_Amount, sum(Incentive_Amount) as Incentive_Amount, sum(Incentive_EMP_Amount) as Incentive_EMP_Amount, sum(EMP_Amount) as EMP_Amount, sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount, sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount," &
            "sum(Deduction_Amount) as Deduction_Amount, sum(Item_Issue_Amount) as Item_Issue_Amount, sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount, sum(MCC_Sale_Amount) as MCC_Sale_Amount,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount " &
               "from (select TSPL_PAYMENT_PROCESS_DETAIL.Advance_Payment_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Advance_Payment_Amount_Knock_Off, TSPL_PAYMENT_PROCESS_HEAD.doc_no," &
          "TSPL_PAYMENT_PROCESS_HEAD.doc_date,TSPL_PAYMENT_PROCESS_HEAD.from_date, TSPL_PAYMENT_PROCESS_HEAD.to_date,tspl_payment_process_detail.milk_purchase_invoice_no as Doc_Code," &
                        "TSPL_PAYMENT_PROCESS_DETAIL.Service_Charge_Amt,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount, TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount," &
               "TSPL_VLC_MASTER_HEAD.VLC_Code, TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount," &
              "TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from  TSPL_PAYMENT_PROCESS_DETAIL " &
                       "left join TSPL_PAYMENT_PROCESS_HEAD  on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No " &
                        "left join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader = TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " &
        "" & whrcls1 & "" &
                        " )  as pp group by Doc_No,VSP_CODE,VLC_Code ) as PaymentProcess  on PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code  And PaymentProcess.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_Code " &
         "left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code " &
         "left join  ( select distinct FAT_Pers, SNF_Pers, Ratio as Fat_ratio, SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code, TSPL_FAT_SNF_UPLOADER_MASTER.code " &
              " from TSPL_FAT_SNF_UPLOADER_MASTER  inner join TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code = TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code)" &
            "as Price_Chart  on TSPL_MILK_SRN_DETAIL.Price_Code = Price_Chart.Code " &
         " left join(" &
       " select 'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Amount*(-1) as Amount,(CASE WHEN TSPL_ITEM_MASTER.ITEM_DESC LIKE '%GHEE%' THEN TSPL_SD_SHIPMENT_detail.Amount ELSE 0 END ) AS [GHEE],(CASE when TSPL_ITEM_MASTER.ITEM_DESC LIKE '%CATTLE%' THEN  TSPL_SD_SHIPMENT_detail.Amount ELSE 0 END) AS [CATTLEFEED],(CASE when TSPL_ITEM_MASTER.ITEM_DESC NOT LIKE '%CATTLE%' AND TSPL_ITEM_MASTER.ITEM_DESC NOT LIKE '%GHEE%'  THEN  TSPL_SD_SHIPMENT_detail.Amount ELSE 0 END) AS [OTHERS]   from TSPL_PAYMENT_PROCESS_MCC_SALE " &
 " LEFT JOIN TSPL_PAYMENT_PROCESS_HEAD ON TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No " &
   "left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " &
 "left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code   " &
 "WHERE convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) AND convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103)) AS MCCSALE " &
" on MCCSALE.customer_code=TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE " &
 "" & whrcls & " " &
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(MyBase.Form_ID + "M", objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
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
        Try
            If Not isLoad Then
                isLoad = True
                If e.Column Is gvAdvancePayment.Columns(colAPSelect) Then
                    loadGvData()
                ElseIf e.Column Is gvAdvancePayment.Columns(colAPInstallmentAmt) Then
                    loadGvData()
                End If
                isLoad = False
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub gvAssetLost_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvAssetLost.CellValueChanged
        Try
            If Not isLoad Then
                isLoad = True
                If e.Column Is gvAssetLost.Columns(colALSelect) Then
                    loadGvData()
                End If
                isLoad = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FrmPaymentProcess_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    lblPending.Visible = True
                    btnReverse.Visible = True
                    btnUnpost.Visible = True
                    btnDeleteVSPBill.Visible = True
                    btnProcess.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Reverse and unpost The payment Process " + Environment.NewLine + "Are You sure", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsPaymentProcessHead.ReverseAndUnpost(fndDocNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text)
                    LoadData(fndDocNo.Value, NavigatorType.Current)
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles btnUnpost.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Unpost The payment Process " + Environment.NewLine + "Are You sure", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsPaymentProcessHead.Unpost(fndDocNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text)
                    LoadData(fndDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnDeleteVSPBill_Click(sender As Object, e As EventArgs) Handles btnDeleteVSPBill.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Reverse and unpost The payment Process " + Environment.NewLine + "Are You sure", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsPaymentProcessHead.DeleteWithVSPBill(fndDocNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        If clsCommon.myLen(fndLoc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please first select Location ", Me.Text)
            fndLoc.Focus()
            Exit Sub
        End If


        Dim qry As String = ""
        Dim arrLoc As String = ""
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            arrLoc = obj.arrLocCodes
        End If

        qry = "select * from ( select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
        & " and (tspl_location_master.loc_segment_Code in (" & arrLoc & ") or tspl_mcc_master.mcc_Code in (" & arrLoc & ")) where tspl_location_master.Loc_Segment_Code='" + fndLoc.Value + "' )xx "

        txtMCC.Text = clsCommon.ShowSelectForm("VSPPMCCa", qry, "Code", "", txtMCC.Text, "", isButtonClicked)
        lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where mcc_Code='" + txtMCC.Text + "'"))
        txtVSP.arrValueMember = Nothing
    End Sub



    Public Function GetVLCUploderName(ByVal VSPCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Code_VLC_Uploader from TSPL_VLC_MASTER_HEAD  where VSP_Code = '" + VSPCode + "'"))
    End Function

    Private Sub btnJE_Click(sender As Object, e As EventArgs) Handles btnJE.Click
        'MyBase.ShowJE(MyBase.Form_ID, fndDocNo.Value)
        Dim qry As String = Nothing
        If clsCommon.myLen(fndDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Document No.", Me.Text)
            Exit Sub
        End If
        qry = "----Invoices " & Environment.NewLine & "select  (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_details.Account_code AS GL_Account_Code ,TSPL_JOURNAL_details.Account_Desc AS GL_Account_Desc,(TSPL_JOURNAL_DETAILS.Amount) AS Expr1, TSPL_JOURNAL_details.Amount,TSPL_JOURNAL_details.Detail_Line_No as Line_No from  TSPL_PAYMENT_PROCESS_INVOICE 
        left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_INVOICE.Doc_No left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No = TSPL_PAYMENT_PROCESS_INVOICE.AP_Invoice_No  left outer join TSPL_JOURNAL_details on TSPL_JOURNAL_details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By 
        on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Modified_By 
        where TSPL_PAYMENT_PROCESS_INVOICE.Doc_No = '" & fndDocNo.Value & "' AND Source_Code = 'AP-IN'
        " & Environment.NewLine & " -----MCC SALE " & Environment.NewLine & " union all   SELECT (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_details.Account_code AS GL_Account_Code ,TSPL_JOURNAL_details.Account_Desc AS GL_Account_Desc,(TSPL_JOURNAL_DETAILS.Amount) AS Expr1, TSPL_JOURNAL_details.Amount,TSPL_JOURNAL_details.Detail_Line_No as Line_No
        from TSPL_PAYMENT_PROCESS_MCC_SALE left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No = TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No  left outer join TSPL_JOURNAL_details on TSPL_JOURNAL_details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No    left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By on
        TSPL_USER_MASTER_Created_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Modified_By     where TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No = '" & fndDocNo.Value & "' 
        " & Environment.NewLine & " -----MCC SALE RETURN " & Environment.NewLine & "union all  SELECT (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_details.Account_code AS GL_Account_Code ,TSPL_JOURNAL_details.Account_Desc AS GL_Account_Desc,(TSPL_JOURNAL_DETAILS.Amount) AS Expr1, TSPL_JOURNAL_details.Amount,TSPL_JOURNAL_details.Detail_Line_No as Line_No
        from TSPL_PAYMENT_PROCESS_MCC_SALE_Return left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Doc_No left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No = TSPL_PAYMENT_PROCESS_MCC_SALE_Return.AR_Invoice_No  left outer join TSPL_JOURNAL_details on TSPL_JOURNAL_details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By 
        on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Modified_By 
        where TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Doc_No = '" & fndDocNo.Value & "'
        " & Environment.NewLine & "---ITEM ISSUE " & Environment.NewLine & "union all select  (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_details.Account_code AS GL_Account_Code ,TSPL_JOURNAL_details.Account_Desc AS GL_Account_Desc,(TSPL_JOURNAL_DETAILS.Amount) AS Expr1, TSPL_JOURNAL_details.Amount,TSPL_JOURNAL_details.Detail_Line_No as Line_No
        from  TSPL_PAYMENT_PROCESS_ITEM_ISSUE left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Doc_No left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No = TSPL_PAYMENT_PROCESS_ITEM_ISSUE.AP_Invoice_No  left outer join TSPL_JOURNAL_details on TSPL_JOURNAL_details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By 
        on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Modified_By 
        where TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Doc_No = '" & fndDocNo.Value & "'
        " & Environment.NewLine & " ---ITEM ISSUE RETURN" & Environment.NewLine & "union all select (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_details.Account_code AS GL_Account_Code ,TSPL_JOURNAL_details.Account_Desc AS GL_Account_Desc,(TSPL_JOURNAL_DETAILS.Amount) AS Expr1, TSPL_JOURNAL_details.Amount,TSPL_JOURNAL_details.Detail_Line_No as Line_No
        from  TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No = TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.AP_Invoice_No  left outer join TSPL_JOURNAL_details on TSPL_JOURNAL_details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By 
        on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Modified_By 
        where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" & fndDocNo.Value & "'
        " & Environment.NewLine & " -----DEDDUCTIONS   " & Environment.NewLine & "union all select (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_details.Account_code AS GL_Account_Code ,TSPL_JOURNAL_details.Account_Desc AS GL_Account_Desc,(TSPL_JOURNAL_DETAILS.Amount) AS Expr1, TSPL_JOURNAL_details.Amount,TSPL_JOURNAL_details.Detail_Line_No as Line_No
        from  TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No left outer join TSPL_JOURNAL_details on TSPL_JOURNAL_details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By 
        on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Modified_By 
        where TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = '" & fndDocNo.Value & "' AND Source_Code = 'AP-DN'
        " & Environment.NewLine & " ---CREDIT NOTE" & Environment.NewLine & "union all select (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_details.Account_code AS GL_Account_Code ,TSPL_JOURNAL_details.Account_Desc AS GL_Account_Desc,(TSPL_JOURNAL_DETAILS.Amount) AS Expr1, TSPL_JOURNAL_details.Amount,TSPL_JOURNAL_details.Detail_Line_No as Line_No
        from  TSPL_PAYMENT_PROCESS_CREDIT_NOTE  left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE .Doc_No left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No left outer join TSPL_JOURNAL_details on TSPL_JOURNAL_details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By 
        on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Modified_By 
        where TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = '" & fndDocNo.Value & "' AND Source_Code = 'AP-CN'
        " & Environment.NewLine & " -----ASSET LOST " & Environment.NewLine & "union all select (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_details.Account_code AS GL_Account_Code ,TSPL_JOURNAL_details.Account_Desc AS GL_Account_Desc,(TSPL_JOURNAL_DETAILS.Amount) AS Expr1, TSPL_JOURNAL_details.Amount,TSPL_JOURNAL_details.Detail_Line_No as Line_No
        from TSPL_PAYMENT_PROCESS_ASSET_LOST left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_No  left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No = TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_JOURNAL_details on TSPL_JOURNAL_details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No
        left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Modified_By 
        where TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No = '" & fndDocNo.Value & "' 
        " & Environment.NewLine & " ----SAVING" & Environment.NewLine & "union all select (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_details.Account_code AS GL_Account_Code ,TSPL_JOURNAL_details.Account_Desc AS GL_Account_Desc,(TSPL_JOURNAL_DETAILS.Amount) AS Expr1, TSPL_JOURNAL_details.Amount,TSPL_JOURNAL_details.Detail_Line_No as Line_No
        from  TSPL_PAYMENT_PROCESS_SAVING left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_SAVING.Doc_No left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No = TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_JOURNAL_details on TSPL_JOURNAL_details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No
        left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By 
        on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Modified_By 
        where TSPL_PAYMENT_PROCESS_SAVING.Doc_No = '" & fndDocNo.Value & "'  AND Source_Code = 'AP-CN'
        " & Environment.NewLine & " ---COMPULSORY " & Environment.NewLine & "union all select (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_details.Account_code AS GL_Account_Code ,TSPL_JOURNAL_details.Account_Desc AS GL_Account_Desc,(TSPL_JOURNAL_DETAILS.Amount) AS Expr1, TSPL_JOURNAL_details.Amount,TSPL_JOURNAL_details.Detail_Line_No as Line_No
        from TSPL_PAYMENT_PROCESS_COMPULSORY left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_COMPULSORY.Doc_No  left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_PROCESS_COMPULSORY.AP_Invoice_No  left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No = TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_JOURNAL_details on TSPL_JOURNAL_details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No
        left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Modified_By 
        where TSPL_PAYMENT_PROCESS_COMPULSORY.Doc_No = '" & fndDocNo.Value & "'  AND Source_Code = 'AP-CN' 
        " & Environment.NewLine & "------Milk Purchase Invoice SRN" & Environment.NewLine & " UNION ALL select (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_details.Account_code AS GL_Account_Code ,TSPL_JOURNAL_details.Account_Desc AS GL_Account_Desc,(TSPL_JOURNAL_DETAILS.Amount) AS Expr1, TSPL_JOURNAL_details.Amount,TSPL_JOURNAL_details.Detail_Line_No as Line_No from 
        tspl_milk_purchase_invoice_detail left outer join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No = tspl_milk_purchase_invoice_detail.DOC_CODE left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No = tspl_milk_purchase_invoice_detail.SRN_CODE 
        left outer join TSPL_JOURNAL_details on TSPL_JOURNAL_details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By  on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Modified_By 
        where TSPL_PAYMENT_PROCESS_HEAD.Doc_No = '" & fndDocNo.Value & "'  AND Source_Code = 'MI-SR'
        " & Environment.NewLine & "---Payable adjustment" & Environment.NewLine & " UNION ALL select (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_details.Account_code AS GL_Account_Code ,TSPL_JOURNAL_details.Account_Desc AS GL_Account_Desc,(TSPL_JOURNAL_DETAILS.Amount) AS Expr1, TSPL_JOURNAL_details.Amount,TSPL_JOURNAL_details.Detail_Line_No as Line_No from TSPL_Payment_Adjustment_Header 
        left outer join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_No = TSPL_Payment_Adjustment_Header.Doc_No left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No = TSPL_Payment_Adjustment_Header.Adjustment_No 
        left outer join TSPL_JOURNAL_details on TSPL_JOURNAL_details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Modified_By 
        where TSPL_PAYMENT_PROCESS_HEAD.Doc_No = '" & fndDocNo.Value & "'  AND Source_Code = 'AP-AD'
        " & Environment.NewLine & "----Advance Payment" & Environment.NewLine & " UNION ALL select (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_details.Account_code AS GL_Account_Code ,TSPL_JOURNAL_details.Account_Desc AS GL_Account_Desc,(TSPL_JOURNAL_DETAILS.Amount) AS Expr1, TSPL_JOURNAL_details.Amount,TSPL_JOURNAL_details.Detail_Line_No as Line_No from  TSPL_PAYMENT_HEADER 
        left outer join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.PP_Detail_No = TSPL_PAYMENT_HEADER.Against_PP_Detail_No left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No = TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_JOURNAL_details on TSPL_JOURNAL_details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No
        left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Modified_By 
        where TSPL_PAYMENT_PROCESS_HEAD.Doc_No = '" & fndDocNo.Value & "'  AND Source_Code = 'AP-PY'
        " & Environment.NewLine & "------Receipt Adjustment " & Environment.NewLine & " union all SELECT (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_details.Account_code AS GL_Account_Code ,TSPL_JOURNAL_details.Account_Desc AS GL_Account_Desc,(TSPL_JOURNAL_DETAILS.Amount) AS Expr1, TSPL_JOURNAL_details.Amount,TSPL_JOURNAL_details.Detail_Line_No as Line_No from TSPL_Receipt_Adjustment_Header
        left outer join TSPL_PAYMENT_PROCESS_MCC_SALE on TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No = TSPL_Receipt_Adjustment_Header.ARInvoiceNo and TSPL_PAYMENT_PROCESS_MCC_SALE.Sale_Doc_No = TSPL_Receipt_Adjustment_Header.Doc_No left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No = TSPL_Receipt_Adjustment_Header.Adjustment_No 
        left outer join TSPL_JOURNAL_details on TSPL_JOURNAL_details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By  on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PAYMENT_PROCESS_HEAD.Modified_By 
        where TSPL_PAYMENT_PROCESS_HEAD.Doc_No = '" & fndDocNo.Value & "'
        ) XX  )  xxx group by xxx.GL_Account_Code having sum(Amount) <> 0 )xxxx"

        qry = "select 'PaymentProcess' as PJVGroup1,ROW_NUMBER() over(order by (Voucher_No)) as Rownum,Invoice_Created_By_Name,Invoice_Modifiy_By_Name,Voucher_No , GL_Account_Code,GL_Account_Desc,Expr1,Amount,Line_No,case when (Amount)<0 then -1 * (Amount) else 0 end as Credit,case when (Amount)>=0 then (Amount) else 0 end as Debit ,(select ISNULL(SUM(TDS_Amount),0) from  TSPL_PAYMENT_PROCESS_DETAIL where Doc_no = 'PP/2324/0230' )AS TDS_Amount from (" & Environment.NewLine & "
         select max(Invoice_Created_By_Name) as Invoice_Created_By_Name,max(Invoice_Modifiy_By_Name) as Invoice_Modifiy_By_Name ,max(Voucher_No)Voucher_No, GL_Account_Code , max(GL_Account_Desc)GL_Account_Desc,sum(Expr1)Expr1, sum(Amount)Amount , max(Line_No)Line_No from " & Environment.NewLine & " ( SELECT * FROM ( " & Environment.NewLine & "" + qry + ""
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim frmCRV As New frmCrystalReportViewer()

        frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinHeaderPartForERODE(), "rptPaymentProcessJ-V", "Payment Process JournalEntry Report", clsCommon.myCDate(clsCommon.GETSERVERDATE()), "SubRptCmpnyMasterForERODE.rpt")
        frmCRV = Nothing
    End Sub

    Private Sub mfndMcc__My_Click(sender As Object, e As EventArgs) Handles mfndMcc._My_Click
        Try
            Dim whrCls As String = " 1=1 "
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
                End If
            End If

            whrCls = whrCls & " and   Rejected_Type='N' and Location_Category='MCC'"
            Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Segment_Code as [LocationSegmentCode],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1,hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C]  from TSPL_Location_MASTER "
            If clsCommon.myLen(whrCls) > 0 Then
                qry += " where " + whrCls
            End If
            mfndMcc.arrValueMember = clsCommon.ShowMultipleSelectForm("MULMCC@PaymentProcess", qry, "Code", "", mfndMcc.arrValueMember, Nothing)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Public Sub FillAllMCCDefault()
        Try
            Dim arr As ArrayList = Nothing
            Dim qry As String = ""
            Dim whrCls As String = " 1=1 "
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
                End If
            End If

            whrCls = whrCls & " and   Rejected_Type='N' and Location_Category='MCC'"
            qry = " select Location_Code as [Code],Location_Desc as [Description],Loc_Segment_Code as [LocationSegmentCode],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1,hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C]  from TSPL_Location_MASTER 
inner join (select MCC_Code from TSPL_MCC_MASTER ) as TabTSPL_MCC_MASTER on TabTSPL_MCC_MASTER.MCC_Code=TSPL_Location_MASTER.Location_Code"
            If clsCommon.myLen(whrCls) > 0 Then
                qry += " where " + whrCls
            End If
            Dim dtMCC As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtMCC IsNot Nothing AndAlso dtMCC.Rows.Count > 0 Then
                arr = New ArrayList()
                For Each dr As DataRow In dtMCC.Rows
                    arr.Add(clsCommon.myCstr(dr("Code")))
                Next
                mfndMcc.arrValueMember = arr
            End If
            txtVSP.arrValueMember = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub AutoFillAllVSP()
        Try
            Dim arr As ArrayList = Nothing
            If mfndMcc.arrValueMember IsNot Nothing AndAlso mfndMcc.arrValueMember.Count <= 0 Then
                Return
            End If
            If fndArea.Value IsNot Nothing AndAlso fndArea.Value.Count <= 0 Then
                Return
            End If

            Dim qry As String = "select xx.VSP_CODE as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,xx.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name from (" + Environment.NewLine +
            " select VSP_CODE,max(VLC_CODE)as VLC_CODE from (" + Environment.NewLine +
            " select VSP_CODE,VLC_CODE from TSPL_MILK_SRN_Head  left outer join tspl_mcc_master on tspl_mcc_master.MCC_Code=TSPL_MILK_SRN_Head.MCC_CODE
              left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MILK_SRN_Head.MCC_CODE where TSPL_LOCATION_MASTER.Location_code in (" & clsCommon.GetMulcallString(mfndMcc.arrValueMember) & ") AND tspl_mcc_master.Area_Location_Code='" & clsCommon.myCstr(fndArea.Value) & "' "
            If Not isPickPendingMilkSRNinNextPaymentCycle Then
                qry += " and DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "
            End If
            qry += " and DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine
            qry += " )xxx group by VSP_CODE 
             )xx 
            left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.VSP_CODE 
            left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=xx.VLC_CODE 
            where TSPL_VENDOR_MASTER.VSP_Farmer_Billing=0 and len(xx.VSP_CODE) > 0 and isnull(TSPL_VENDOR_MASTER.is_Drip_Saver,'')<>'Y' order by xx.VSP_CODE"

            Dim dtVSP As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtVSP IsNot Nothing AndAlso dtVSP.Rows.Count > 0 Then
                arr = New ArrayList()
                For Each dr As DataRow In dtVSP.Rows
                    arr.Add(clsCommon.myCstr(dr("Code")))
                Next
                txtVSP.arrValueMember = arr
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDCPrint_Click(sender As Object, e As EventArgs) Handles btnDCPrint.Click

        Try
            If clsCommon.myLen(fndDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select document", Me.Text)
                fndDocNo.Focus()
                Exit Sub
            End If

            'clsPaymentProcessHead.PaymentProcessDrCrPrint("'" + fndDocNo.Value + "'", dtpFromDate.Text, dtpToDate.Text, "'" + fndLoc.Value + "'", clsCommon.GetMulcallString(txtVSP.arrValueMember), "", "", "", txtMCC.Text)
            clsPaymentProcessHead.PaymentProcessDrCrPrint("'" + fndDocNo.Value + "'", dtpFromDate.Text, dtpToDate.Text, "'" + fndLoc.Value + "'", "", "", "", txtMCC.Text)
            '

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrintBillMobUser_Click(sender As Object, e As EventArgs) Handles btnPrintBillMobUser.Click
        Try
            Dim qry As String = "select TSPL_PAYMENT_PROCESS_DETAIL.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No from TSPL_PAYMENT_PROCESS_DETAIL 
left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + fndDocNo.Value + "' and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim PDFPath As String = clsPaymentProcessHead.Load_Report_Paymnet_RCDF("'" + clsCommon.myCstr(dr("Doc_No")) + "'", clsCommon.myCDate(dr("From_Date")), clsCommon.myCDate(dr("To_Date")), "", "'" + clsCommon.myCstr(dr("VSP_CODE")) + "'", "", "", "", False, True)
                    Dim FileNo As Integer = clsAttachDocument.UploadWithHttpRequest(PDFPath, Path.GetFileName(PDFPath), clsUserMgtCode.frmMilkPurchaseInvoice, clsCommon.myCstr(dr("Milk_Purchase_Invoice_No")))
                    If FileNo > 0 Then
                        qry = " UPDATE TSPL_MILK_PURCHASE_INVOICE_HEAD set FILE_INFO=" + clsCommon.myCstr(FileNo) + " where DOC_CODE='" + clsCommon.myCstr(dr("Milk_Purchase_Invoice_No")) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    End If
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub MyCheckBox1_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAll.ToggleStateChanged
        chkSkipPrevItemIssue.Checked = chkAll.Checked
        chkSkipPrevItemIssueReturn.Checked = chkAll.Checked
        chkSkipPrevMccSale.Checked = chkAll.Checked
        ChkSkipMccSaleReturn.Checked = chkAll.Checked
        chkSkipPrevCreditNote.Checked = chkAll.Checked
        chkSkipPrevDeduction.Checked = chkAll.Checked
        chkSkipPreviousDocumentOfAdvancePayment.Checked = chkAll.Checked
        chkSkipPreviousDocumentOfAssetLost.Checked = chkAll.Checked
    End Sub
    'Private Sub chkArea_CheckedChanged(sender As Object, e As EventArgs) Handles chkArea.CheckedChanged
    '    If chkArea.Checked Then
    '        fndArea.Visible = True
    '    End If
    'End Sub

    Private Sub fndArea__MYValidating_1(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndArea._MYValidating
        Try
            Dim sQuery As String = " Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc, Type from TSPL_LOCATION_MASTER"
            fndArea.Value = clsCommon.ShowSelectForm("Location@Plant@Master", sQuery, "Code", "TSPL_LOCATION_MASTER.Type <> 'PLANT' OR TSPL_LOCATION_MASTER.Location_Category <> 'Mcc'", fndArea.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Private Sub chkHoldSavingPayment_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkHoldSavingPayment.ToggleStateChanged
        Try
            For ii As Integer = 0 To gv.Rows.Count - 1
                gv.Rows(ii).Cells(colIsPaymentProcessHoldSavingManual).Value = chkHoldSavingPayment.Checked
                SetPaymentProcessHoldSaving(ii)
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetPaymentProcessHoldSaving(ii As Integer)
        gv.Rows(ii).Cells(colIsPaymentProcessHoldSaving).Value = (clsCommon.myCBool(gv.Rows(ii).Cells(colIsPaymentProcessHoldSavingAuto).Value) OrElse clsCommon.myCBool(gv.Rows(ii).Cells(colIsPaymentProcessHoldSavingManual).Value))
    End Sub

    Private Sub btnPrintHindi_Click(sender As Object, e As EventArgs) Handles btnPrintHindi.Click
        Try
            Dim fontInstalled As Boolean = False
            For Each fontFamily As FontFamily In FontFamily.Families
                If fontFamily.Name = "Mangal" Then
                    fontInstalled = True
                    Exit For
                End If
            Next
            If fontInstalled Then
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                    Load_Report_Paymnet_UDL()
                ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal Then
                    Load_Report_Paymnet_BHAD()
                ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHBA") = CompairStringResult.Equal Then
                    Load_Report_Paymnet_BHBA()
                ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UCDF") = CompairStringResult.Equal Then
                    Load_Report_Paymnet_UCDF()
                ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "RCDF") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDP") = CompairStringResult.Equal Then
                    'Load_Report_Paymnet_RCDF()
                    clsPaymentProcessHead.Load_Report_Paymnet_RCDF("'" + fndDocNo.Value + "'", dtpFromDate.Text, dtpToDate.Text, "", clsCommon.GetMulcallString(txtVSP.arrValueMember), "", "", "", False, "", True)
                Else
                    Load_Report(Nothing, Nothing, Nothing, Nothing, False, True)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "The font Mangal is not installed on the system.", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class
