Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPHRandPayroll
'created by --> Sanjay

Public Class frmVSP_VLCMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim userCode, companyCode As String
    Dim str As String
    Dim OneTimeCheck As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim checkPan As New System.Text.RegularExpressions.Regex("^([A-Z]){5}([0-9]){4}([A-Z]){1}")
    Dim IsInsieLoadData As Boolean
    Dim Frm_Open As FrmMainTranScreen
    Dim FixVSPEMP As Integer = 0
    Dim AllowVSPMasterAutoPrefix As Integer = 0
    Dim UserPrefix As String = Nothing
    Dim EnableBankFromMaster As Boolean
    Const colEMPSno As String = "colEMPSno"
    Const colEMPSlab As String = "colEMPSlab"
    Const colEMPValue As String = "colEMPValue"
    Dim TIPRateMix As Double = 0
    Dim TIPRateCow As Double = 0
    Dim TIPRateBuffalo As Double = 0
    Dim DefaultCustomerGroupCodeforVSP As String = ""
    Dim DefaultVendorGroupCodeforVSP As String = ""
    Dim isLoadCopy As Boolean = False
    Dim arrLoc As String = Nothing
    Dim Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster As Boolean = False

    'Default Mcc create''''''''''''''''''''''''''''''''''''''''''''''''''''
    Dim arrExistCols As New List(Of String)
    Dim dtDefault As DataTable = Nothing
    ''Public Const colMCCUploaderCode1 As String = "MCC Uploader Code#"
    ''Public Const colMCCCode1 As String = "MCC Code#"
    ''Public Const colMCCType As String = "MCC Type#"
    ''Public Const colMCCName1 As String = "MCC Name#"
    ''Public Const colMCCChillingVendorCode As String = "MCC Chilling Vendor Code#"
    ''Public Const colMCCAddress1 As String = "MCC Address1#"
    ''Public Const colMCCAddress2 As String = "MCC Address2#"
    ''Public Const colMCCTehsil As String = "MCC Tehsil#"
    ''Public Const colMCCCity As String = "MCC City Code#"
    ''Public Const colMCCState As String = "MCC State Code#"
    ''Public Const colMCCCountry As String = "MCC Country Code#"
    ''Public Const colMCCPinCode As String = "MCC Pin Code#"
    ''Public Const colMCCTelphone As String = "MCC Telphone#"
    ''Public Const colMCCEmail As String = "MCC Email#"
    ''Public Const colMCCFax As String = "MCC Fax#"
    ''Public Const colMccSuperArea As String = "MCC Super Area#"
    ''Public Const colMccSuperAreaUOM As String = "MCC Super Area UOM(Sq. Ft./Sq. Mt.)#"
    ''Public Const colMCCAreaOfStore As String = "MCC Area Of Store#"
    ''Public Const colMCCAreaOfOffice As String = "MCC Area Of Office#"
    ''Public Const colMCCAreaOfOfficeUOM As String = "MCC Area of Office UOM(Sq. Ft./Sq. Mt.)#"
    ''Public Const colMCCOpenAreaForTanker As String = "MCC Open Area For Tanker#"
    ''Public Const colMCCOpenAreaForTankerUOM As String = "MCC Open Area For Tanker UOM(Sq. Ft./Sq. Mt.)#"
    ''Public Const colMCCAreaOfLab As String = "MCC Area Of Lab#"
    ''Public Const colMCCAreaOfLabUOM As String = "MCC Area of Lab UOM(Sq. Ft./Sq. Mt.)#"
    ''Public Const colMCCTotalStorageCapacity As String = "MCC Total Storage Capacity#"
    ''Public Const colMCCAreaOfReceivingDock As String = "MCC Area Of Receiving Dock#"
    ''Public Const colMCCAreaOfReceivingDockUOM As String = "MCC Area of Receiving Dock UOM(Sq. Ft./Sq. Mt.)#"
    ''Public Const colMCCDripSaver As String = "MCC Drip Saver (Yes/No)#"
    ''Public Const colMCCCanWasher As String = "MCC Can Washer (Yes/No)#"
    ''Public Const colMCCCanScrubber As String = "MCC Can Scrubber (Yes/No)#"
    ''Public Const colMCCFssaiNo As String = "MCC Fssai No#"
    ''Public Const colMCCETP As String = "MCC ETP (Yes/No)#"
    ''Public Const colMCCEarthing As String = "MCC Earthing (Yes/No)#"
    ''Public Const colMCCCoilLength As String = "MCC Coil Length#"
    ''Public Const colMCCElectricityConnection As String = "MCC Electricity Connection#"
    ''Public Const colMCCBoiler As String = "MCC Boiler (Yes/No)#"
    ''Public Const colMCCIndustryType As String = "MCC Industry Type#"
    ''Public Const colMCCPropName As String = "MCC Prop Name#"
    ''Public Const colMCCPartnerName As String = "MCC Partner Name#"
    ''Public Const colMCCDirectorName As String = "MCC Director Name#"
    ''Public Const colMCCMonthlyProvision As String = "MCC Monthly Provision(Y/N)#"
    ''Public Const colMCCChillingCharges As String = "MCC Chilling Charges#"
    ''Public Const colMCCChillingOn As String = "MCC Chilling On#"
    ''Public Const colMCCChillingMinGuaranteedAvgQty As String = "MCC Chilling Min. Guaranteed Avg. Qty#"
    ''Public Const colMCCChillingOnUOMKGLTR As String = "MCC Chilling On UOM(KG/LTR)#"
    ''Public Const colMCCChillingOnQty As String = "MCC Chilling On Qty#"
    ''Public Const colMCCChillingOnUOMHandledDispatched As String = "MCC Chilling On UOM(Handled/Dispatched)#"
    ''Public Const colMCCChillingMinGuaranteedPeriod As String = "MCC Chilling Min. Guaranteed Period#"
    ''Public Const colMCCChillingMinGuaranteedPeriodUOM As String = "MCC Chilling Min. Guaranteed Period UOM (Day/Month/Year)#"
    ''Public Const colMCCRateofLeaseCharges As String = "MCC Rate of Lease Charges#"
    ''Public Const colMCCRateofLeasedChargesUOM As String = "MCC Rate of Leased Charges UOM(Day/Month/Year)#"
    ''Public Const colMCCAreaofStoreUOM As String = "MCC Area of Store UOM(Sq. Ft./Sq. Mt.)#"
    ''Public Const colMCCAgreement_Status As String = "MCC Agreement_Status#"
    ''Public Const colMCCAgreement_Date As String = "MCC Agreement_Date#"
    ''Public Const colMCCAgreementExpiryDate As String = "MCC Agreement Expiry Date#"
    ''Public Const colMCCSecurity_Status As String = "MCC Security_Status#"
    ''Public Const colMCCCheque_Amt As String = "MCC Cheque_Amt#"
    ''Public Const colMCCCheque_No As String = "MCC Cheque_No#"
    ''Public Const colMCCCheque_Date As String = "MCC Cheque_Date#"
    ''Public Const colMCCChillingStartingDate As String = "MCC Chilling Starting Date#"
    ''Public Const colMCCIsTruckSheetMandatory As String = "MCC Is Truck Sheet Mandatory#"
    ''Public Const colMCCWeighingComPort As String = "MCC Weighing ComPort#"
    ''Public Const colMCCWeighingMachine As String = "MCC Weighing Machine#"
    ''Public Const colMCCSampleComPort As String = "MCC Sample ComPort#"
    ''Public Const colMCCSampleMachine As String = "MCC Sample Machine#"
    ''Public Const colMCCPaymentCycle As String = "MCC Payment Cycle#"
    ''Public Const colMCCIncentiveCode As String = "MCC Incentive Code#"
    ''Public Const colMCCShiftMorningOpeningTime As String = "MCC Shift Morning Opening Time#"
    ''Public Const colMCCShiftMorningClosingTime As String = "MCC Shift Morning Closing Time#"
    ''Public Const colMCCShiftEveningOpeningTime As String = "MCC Shift Evening Opening Time#"
    ''Public Const colMCCShiftEveningClosingTime As String = "MCC Shift Evening Closing Time#"
    ''Public Const colMCCRM As String = "MCC RM#"
    ''Public Const colMCCRequiredGateEntry As String = "MCC Required Gate Entry(Yes/No)#"
    ''Public Const colMCCAllowAutoMilkIn As String = "MCC AllowAutoMilkIn#"
    ''Public Const colMCCAutoIn_Location As String = "MCC AutoIn_Location#"
    ''Public Const colMCCSILOIn_Location As String = "MCC SILOIn_Location#"
    ''Public Const colMCCApplyReceiptWeightTolerance As String = "MCC ApplyReceiptWeightTolerance(Y/N)#"
    ''Public Const colMCCReceiptWeightToleranceValue As String = "MCC ReceiptWeightToleranceValue#"
    ''Public Const colMCCApplyFailedSample As String = "MCC Apply Failed Sample(Y/N)#"
    ''Public Const colMCCFailedSampleFAT As String = "MCC Failed Sample FAT %#"
    ''Public Const colMCCFailedSampleSNF As String = "MCC Failed Sample SNF %#"
    ''Public Const colMCCLocSegmentCode As String = "MCC Loc Segment Code#"
    ''Public Const colMCCBMCC As String = "MCC MCC(1)/BMCC(0)#"
    ''Public Const colMCCCommissionRate As String = "MCC CommissionRate#"
    ''Public Const colMCCCommissionMinimumShiftInPaymentCycle As String = "MCC CommissionMinimumShiftInPaymentCycle#"
    ''Public Const colMCCCommissionMinimumQtyInShift As String = "MCC CommissionMinimumQtyInShift#"
    ''Public Const colMCCCommissionNoOfPaymentCycleForNewVSP As String = "MCC CommissionNoOfPaymentCycleForNewVSP#"
    ''Public Const colMCCDeductionMinimumFATPer As String = "MCC DeductionMinimumFATPer#"
    ''Public Const colMCCDeductionMinimumSNFPer As String = "MCC DeductionMinimumSNFPer#"
    ''Public Const colMCCDeductionNoOfPaymentCycleForNewVSP As String = "MCC DeductionNoOfPaymentCycleForNewVSP#"
    ''Public Const colMCCPlant As String = "MCC Plant#"
    ''Public Const colMCCMorningShiftOpeningTime As String = "MCC Morning Shift Opening Time#"
    ''Public Const colMCCMorningShiftClosingTime As String = "MCC Morning Shift Closing Time#"
    ''Public Const colMCCEveningShiftOpeningTime As String = "MCC Evening Shift Opening Time#"
    ''Public Const colMCCEveningShiftClosingTime As String = "MCC Evening Shift Closing Time#"
    'Default Mcc create''''''''''''''''''''''''''''''''''''''''''''''''''''
#End Region


    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

#Region "Page Load"
    Private Sub frmVSP_VLCMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserPrefix = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PrefixForUserMaster, clsFixedParameterCode.PrefixForUserMaster, Nothing))
        AllowVSPMasterAutoPrefix = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowVSPMasterAutoPrefix, clsFixedParameterCode.AllowVSPMasterAutoPrefix, Nothing))
        FixVSPEMP = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FixVSPEMP, clsFixedParameterCode.FixVSPEMP, Nothing))
        EnableBankFromMaster = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.EnableBankFromMaster & "'")) = 0, False, True)

        TIPRateMix = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TIPRateMix, clsFixedParameterCode.TIPRateMix, Nothing))
        TIPRateCow = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TIPRateCow, clsFixedParameterCode.TIPRateCow, Nothing))
        TIPRateBuffalo = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TIPRateBuffalo, clsFixedParameterCode.TIPRateBuffalo, Nothing))
        DefaultCustomerGroupCodeforVSP = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DefaultCustomerGroupCodeforVSP, clsFixedParameterCode.DefaultCustomerGroupCodeforVSP, Nothing))
        DefaultVendorGroupCodeforVSP = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DefaultVendorGroupCodeforVSP, clsFixedParameterCode.DefaultVendorGroupCodeforVSP, Nothing))
        Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster & "'")) = 0, False, True)
        funreset()
        SetLength()
        pageCus.SelectedPage = RadPageViewPage1
        SetUserMgmtNew()
        ToolTipvendor.SetToolTip(btnnew, "New")
        '' Anubhooti 4-Aug-2014 BM00000003319
        LoadAccountType()
        ''
        fndvendorNo_text_changed()
        fndgroupcode_text_Changed()
        fndcity_text_Changed()

        fndgroupcode_leave()
        fndCity_leave()
        ''LoadEMPType()

        chkInActive.Checked = False
        dtClosing.Enabled = False
        btndelete.Enabled = False
        btnsave.Enabled = True

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclear, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")

        '' check for multi currency
        If objCommonVar.IsDemoERP = True Then
            If CheckMultiCurrency(Nothing) = True Then
                lblBaseCurrency.Visible = True
                Me.fndVendorCurrency.Visible = True
            Else
                lblBaseCurrency.Visible = False
                Me.fndVendorCurrency.Visible = False

            End If
        Else
            lblBaseCurrency.Visible = False
            fndVendorCurrency.Visible = False
            fndVendorCurrency.Value = objCommonVar.BaseCurrencyCode
        End If

        If EnableBankFromMaster = True Then
            findfndbankcode.Visible = True
            findfndbankcode.Value = ""
            findTxtIFSCCode.Visible = True
            findTxtIFSCCode.Value = ""
            findTxtIFSCCode2.Visible = True
            findTxtIFSCCode2.Value = ""
            fndbankcode.Visible = False
            TxtIFSCCode.Visible = False
            txtIFSCCode2.Visible = False
            findfndbankcode2.Visible = True
            findfndbankcode2.Value = ""

        Else
            fndbankcode.Visible = True
            TxtIFSCCode.Visible = True
            txtIFSCCode2.Visible = True
            findfndbankcode.Visible = False
            findfndbankcode.Value = ""
            findTxtIFSCCode.Visible = False
            findTxtIFSCCode.Value = ""
            findTxtIFSCCode2.Visible = False
            findTxtIFSCCode2.Value = ""
            findfndbankcode2.Visible = False
            findfndbankcode2.Value = ""

        End If

        '---------------------------------
        txtvndrtype.Text = "VSP"
        LoadVSPPayment()
        LoadIncentive()
        '---------------------------------------------------------

        txtcountrycode.Value = clsDBFuncationality.getSingleValue("select country_Code from tspl_Country_Master where Country_Code='INDIA'") '""
        txtCountry.Text = clsDBFuncationality.getSingleValue("select country_Name from tspl_Country_Master where Country_Code='INDIA'") '""
        funSetDefaultData()
        txtvspcode.Enabled = False
        GroupBox2.Visible = False

        chkRegistered.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        chkPDCS.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        'chkCLUSTER.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        lblStartDate.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        txtStartDate.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        chkOwnBMC.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        lblOwnMCC.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        txtMCCOwnBMC.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        lblMCCOwnBMC.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        gbBank2Details.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        lblMCCOwnBMC.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        txtSupervisiorRP.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        lblSupervisiorRP.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        lblRegistrationNo.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        txtRegistrationNo.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        lblRegistrationDate.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        txtRegistrationDate.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        txtRegistrationDate.Value = clsCommon.GETSERVERDATE()
        txtCowPriceDate.Value = clsCommon.GETSERVERDATE()
        If chkApplyCowPrice.Checked Then
            txtCowPriceDate.Enabled = True
        Else
            txtCowPriceDate.Enabled = False
        End If

        If ChkHeadLoad.Checked Then
            CmbHeadLoadServiceBasis.Enabled = True
            txtRateHeadLoad.Enabled = True
        Else
            CmbHeadLoadServiceBasis.Enabled = False
            txtRateHeadLoad.Enabled = False
        End If

        If chkOwnBMC.Checked Then
            lblOwnMCC.Visible = True
            txtMCCOwnBMC.Visible = True
            lblMCCOwnBMC.Visible = True
            txtOwnBMCDate.Enabled = True
        Else
            lblOwnMCC.Visible = False
            txtMCCOwnBMC.Visible = False
            lblMCCOwnBMC.Visible = False
            txtOwnBMCDate.Enabled = False
        End If
        chkCLUSTER.Visible = False
    End Sub
    Function CheckMultiCurrency(ByVal trans As SqlTransaction) As Boolean
        Dim strq As String
        strq = "select * from tspl_module_currency_mapping where comp_code='" + objCommonVar.CurrentCompanyCode + "' and module_code='" & Me.Module_Code & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("Apply") = True Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Public Sub SetLength()
        fndvendorNo.MyMaxLength = 12
        txtvendorname.MaxLength = 50
        txtAdd1.MaxLength = 50
        txtAdd2.MaxLength = 50
        txtAdd3.MaxLength = 50
        txtgroupdes.MaxLength = 50
        txtfax.MaxLength = 20
        txtEmail.MaxLength = 50
        txtWeb.MaxLength = 50
        txtvendortypedes.MaxLength = 50
        txtbankcodedes.MaxLength = 50
        txtbankcodedes2.MaxLength = 50
        txtCredit.MaxLength = 9
        txtcollect.MaxLength = 30
        txtpan.MaxLength = 30

    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmVSPMaster)
        If Not (MyBase.isReadFlag) Then

            If MDI.blnShowAllMenu = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If

            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        If Not (MyBase.isReadFlag) Then
            MenuExport.Visibility = ElementVisibility.Collapsed
        End If
        If Not (MyBase.isModifyFlag) Then
            MenuImport.Visibility = ElementVisibility.Collapsed
        End If
    End Sub
#End Region

#Region "Function"

    '' Anubhooti 4-Aug-2014 BM00000003319
    Sub LoadAccountType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        dt.Rows.Add("Cur", "Current")
        dt.Rows.Add("Sav", "Saving")
        dt.Rows.Add("Cas", "Cash")
        dt.Rows.Add("Cre", "Credit")
        dt.Rows.Add("Loa", "Loan")
        dt.Rows.Add("Oth", "Others")

        cmbAccountType.DataSource = dt.Copy()
        cmbAccountType.DisplayMember = "Name"
        cmbAccountType.ValueMember = "Code"
        cmbAccountType.SelectedValue = "Cur"
        cmbAccountType.Enabled = False

        cmbAccountType2.DataSource = dt.Copy()
        cmbAccountType2.DisplayMember = "Name"
        cmbAccountType2.ValueMember = "Code"
        cmbAccountType2.SelectedValue = "Sav"
        cmbAccountType2.Enabled = False
    End Sub
    'It will fill the  controls if value exist in database according to fndgroupcode
    Public Sub funfillfndGroupCode()
        Try
            Dim strquery As String = "select group_desc,tax_Group_Code,Acct_Set_code,Terms_COde,Bank_Code ,payment_code from Tspl_vendor_group where ven_group_code='" + fndgroupcode.Value + "'"
            Dim dr As DataTable
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txtgroupdes.Text = dr.Rows(0)("group_desc").ToString()
                fndbankcode.Text = dr.Rows(0)("Bank_Code").ToString()
                txtbankcodedes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_bank_master where bank_code='" + fndbankcode.Text + "'"))

            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'It will fill the  controls if value exist in database according to fndCity
    Public Sub funfilfndcity()
        Try

            Dim strquery As String = "select City_Code ,city_Name from TSPL_City_MASTER where City_code = '" + fndCity.Value + "'"
            Dim dr As DataTable
            ' Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txtCity.Text = dr.Rows(0)("city_Name").ToString()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    'It will fill the  controls if value exist in database according to fndvendortype
    Public Sub funfillfndventype()
        Try

            Dim strquery As String = "SELECT  [ven_Type_Code] as [Vendor Type Code],[ven_Type_Desc] as [Description]FROM [TSPL_VENDOR_TYPE_MASTER] where ven_type_code='" + fndvendortype.Value + "'"
            Dim dr As DataTable
            'Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txtvendortypedes.Text = dr.Rows(0)("Description").ToString()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'It will fill the  controls if value exist in database according to fndbankcode
    Public Sub funfillbank()
        Try

            Dim strquery As String = "select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER where bank_code='" + fndbankcode.Text + "'"
            'Dim strquery As String = "select bankcode2 As [Bank Code],description  as [Description]from TSPL_Bank_MASTER where BankCode2='" + fndbankcode2.Text + "'"

            Dim dr As DataTable
            'Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txtbankcodedes.Text = dr.Rows(0)("Description").ToString()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub funfillbranch()
        Try
            If clsCommon.myLen(fndbankcode.Text) > 0 Then
                TxtBankBranch.Text = clsDBFuncationality.getSingleValue("Select Branch_Name from TSPL_Vendor_Bank_Branch_Details where Bank_Code ='" & fndbankcode.Text & "' and Bank_IFSC_Code='" & TxtIFSCCode.Text & "' ")
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    'This funtion will fill all the fields on selecting the value from finder
    Public Sub funfill()
        Try

            Dim strCmd As String
            Dim myDs As DataSet
            strCmd = " Select Vendor_Name, Vendor_Group_Code,  Vendor_Group_Code_Desc,  Status ,OnHold  ,Convert(Date,Closing_Date,103) ,Add1 ,	Add2 ,Add3 ," &
                     "City_Code ,City_Code_Desc ,State ,Country ,	Phone1 ,Phone2 ,Fax,Email ,WebSite ,Contact_Person_Name ,Contact_Person_Phone ," &
                     "Contact_Person_Fax ,Contact_Person_Website ,Contact_Person_Email ,Terms_Code ,Terms_Code_Desc ,Vendor_Account ,Vendor_Account_Desc ," &
                     "Payment_Code,Payment_Code_Desc ,Ven_Type_Code ,Ven_Type_Desc ,Bank_Code ,Bank_Code_Desc ,Service_Tax_No ,Lst_No ,Tin_No ,	Credit_Limit ," &
                     "Tax_Group ,Tax_Group_Desc ,TAX1 ,TAX1_Rate ,TAX2,TAX2_Rate ,TAX3 ,TAX3_Rate ,TAX4 ,TAX4_Rate ,TAX5 ,TAX5_Rate ,TAX6 ,TAX6_Rate ," &
                     "TAX7 ,TAX7_Rate ,TAX8 ,TAX8_Rate ,TAX9 ,TAX9_Rate ,TAX10 ,TAX10_Rate ,Remarks1 ,Remarks2 ,Additional1 ,Additional2 ,Additional3,transporter,CST,ECC,Range,Collectorate,PAN,is_Gross_Receipt,Inter_branch,currency_code,franchise_yn,state_code,country_code,vsp_payment,incentive_days,incentive,commision_pers,payment_commision_pers,Service_charges,VSP_Payee_Name,Service_Charge_Type,Joint_Name,Branch_Name,Account_No,Bank_Name,IFSC_Code,Account_Type,Security_Amount,AMCU,Amc_Charge,Billing_date,Nature,Actual_charges,joint_bank_Code,Joint_Account_No,Agreement,Start_Date,End_Date,PC_Code,Is_Head_Load,Rate_Head_Load,Service_Basis_Head_Load,Is_Own_Asset,Rate_Own_Asset,Service_Basis_Own_Asset,joint_bank_code,Standard_security_Amount,MP_code,MP_Name,Cheque_In_Favour_Of,Pin_code,is_drip_saver,isnull(Joint_Branch_Name,'') as Joint_Branch_Name,isnull(Joint_IFSC_Code,'') as Joint_IFSC_Code,EMP_Type,EMP_Fixed_Amount,Actual_charges_Slab,Actual_charges_Slab2,Actual_charges2,Actual_charges_Slab3,Actual_charges3,Actual_charges_Slab4,Actual_charges4,Actual_charges_Slab5,Actual_charges5,Apply_Mult_Incentive,Security_Deduction_Amount,Interest_Per,Minimum_Interest,Is_Blacklist,Service_Charge_Per_Unit,is_Hold_Payment_Process,Is_Inactive_In_Milk_Procurement,GSTRegistered,GSTEntity,GSTLastEntity,GSTFinalNo,CorrectionFat,CorrectionSNF,Handling_Charges_Per,Credit_Limit_On_Milk_Receipt_Per,Monthly_Rent,TIP_Buffalo,TIP_Cow,TIP_Mix,case when Active_Date is null and tspl_vendor_master.Status = 'Y' then ''  when Active_Date is null and tspl_vendor_master.Status = 'N' then convert (varchar, Created_Date,103) else convert(varchar, Active_Date,103) end as Active_Date, isnull (Gender,'')  as Gender , BankCode2,BankName2,Credit2, IFSCCode2 ,AccNo2,AccountType2,BankBranch2,SecurityCharges2,Registered_PDCS_CLUSTER,StartDate,SupervisorOrRP, RegistrationNo,RegistrationDate, Vendor_name_Hindi, DISTRICT_Code, Zone_Code , CAST_CATEGORY_CODE , BLOCK_CODE,Company_Bank,Company_Bank_Current, REVENUE_VILLAGE_CODE,GRAMPANCHAYAT_CODE,PANCHAYAT_SAMITI_CODE,VIDHAN_SABHA_CODE  from tspl_vendor_master where vendor_code='" + fndvendorNo.Value + "' and form_type='VSP'"
            myDs = connectSql.RunSQLReturnDS(strCmd)
            Dim myDr As DataRow
            For Each myDr In myDs.Tables(0).Rows
                chk_isblacklist.Checked = IIf(clsCommon.myCstr(myDr("Is_Blacklist").ToString()) = "1", True, False)
                chkMultIncentive.Checked = IIf(clsCommon.myCdbl(myDr("Apply_Mult_Incentive")) > 0, True, False)
                LoadIncentive(fndvendorNo.Value, Nothing)
                Me.txtvendorname.Text = myDr(0).ToString()
                Me.fndgroupcode.Value = myDr(1).ToString()
                If clsCommon.myLen(fndgroupcode.Value) <= 0 Then
                    Me.txtgroupdes.Text = ""
                Else
                    Me.txtgroupdes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TSPL_VENDOR_GROUP.Group_Desc FROM TSPL_VENDOR_GROUP WHERE Ven_Group_Code='" & fndgroupcode.Value & "'"))
                End If

                TxtCreditLimitBasedOnMilkReceipt.Value = clsCommon.myCdbl(myDr("Credit_Limit_On_Milk_Receipt_Per"))
                If TxtCreditLimitBasedOnMilkReceipt.Value > 0 Then
                    chkCreditLimitBasedOnMilkReceipt.Checked = True
                Else
                    chkCreditLimitBasedOnMilkReceipt.Checked = False
                    TxtCreditLimitBasedOnMilkReceipt.Value = Nothing
                End If

                'ChkHeadLoad.Checked = IIf(clsCommon.myCstr(myDr("Is_Head_Load")) = "T", True, False)
                chkHoldPaymentProcess.Checked = clsCommon.myCdbl(myDr("is_Hold_Payment_Process")) = 1
                chkInactiveInMilkModule.Checked = clsCommon.myCdbl(myDr("Is_Inactive_In_Milk_Procurement")) = 1

                TxtPinCode.Text = clsCommon.myCstr(myDr("Pin_Code"))
                fndpaymentCycle.Value = clsCommon.myCstr(myDr("PC_COde"))
                GetPaymentCycleData(False)

                Dim strStatus As String = myDr(3).ToString()
                If strStatus = "N" Then
                    chkInActive.Checked = False
                ElseIf strStatus = "Y" Then
                    chkInActive.Checked = True
                End If
                lblActiveDate.Text = myDr("Active_Date").ToString()
                cmbGender.Text = myDr("Gender").ToString()
                Dim strHold As String = myDr(4).ToString()
                If strHold = "N" Then
                    chkHold.Checked = False
                ElseIf strHold = "Y" Then
                    chkHold.Checked = True
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr("Transporter")), "Y") = CompairStringResult.Equal Then
                    chktrarns.Checked = True
                Else
                    chktrarns.Checked = False
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr("is_drip_saver")), "Y") = CompairStringResult.Equal Then
                    ChkIsDripSaver.Checked = True
                Else
                    ChkIsDripSaver.Checked = False
                End If

                If (myDr(5).ToString()) = Nothing Then
                    Me.dtClosing.Value = System.DateTime.Now.Date
                Else
                    Me.dtClosing.Value = CDate(myDr(5).ToString())
                End If
                Me.txtAdd1.Text = myDr(6).ToString()
                Me.txtAdd2.Text = myDr(7).ToString()
                Me.txtAdd3.Text = myDr(8).ToString()
                Me.fndCity.Value = myDr(9).ToString()
                Me.txtCity.Text = myDr(10).ToString()
                Me.txtState.Text = myDr(11).ToString()
                Me.txtCountry.Text = myDr(12).ToString()

                '----------------------------------------------------Monika 22/05/2014-----------------------
                txtcountrycode.Value = clsCommon.myCstr(myDr("country_code"))
                txtstatecode.Value = clsCommon.myCstr(myDr("state_code"))
                '---------------------------------for old data getting code'-----------------------------

                If objCommonVar.GSTApplicable Then
                    Dim check As String = clsDBFuncationality.getSingleValue("select case when isnull(GST_State_Code,'')='' then '' else GST_State_Code end as GST_State_Code from tspl_state_master where state_code='" & txtstatecode.Value & "'")
                    If clsCommon.myLen(check) > 0 Then
                        txtGSTStateCode.Text = check
                    End If

                    txtEntity.Text = clsCommon.myCstr(myDr("GSTEntity"))
                    MyTextBox2.Text = clsCommon.myCstr(myDr("GSTLastEntity"))
                    Rchkregistered.Checked = IIf(clsCommon.myCstr(myDr("GSTRegistered").ToString()) = "1", True, False)
                    txtGSTIN_No_final.Text = clsCommon.myCstr(myDr("GSTFinalNo"))
                End If

                numCorrectionFat.Text = clsCommon.myCdbl(myDr("CorrectionFat"))
                numCorrectionSNF.Text = clsCommon.myCdbl(myDr("CorrectionSNF"))

                If clsCommon.myLen(txtState.Text) > 0 AndAlso clsCommon.myLen(txtstatecode.Value) <= 0 Then
                    Dim qry As String = "select state_code from tspl_state_master where state_name like '%" + txtState.Text + "%'"
                    txtstatecode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                End If

                If clsCommon.myLen(txtCountry.Text) > 0 AndAlso clsCommon.myLen(txtcountrycode.Value) <= 0 Then
                    Dim qry As String = "select country_code from tspl_country_master where country_name like '%" + txtCountry.Text + "%'"
                    txtcountrycode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                End If



                'CmbNature.SelectedValue = clsCommon.myCstr(myDr("Nature"))
                txtcommpers.Text = clsCommon.myCstr(myDr("commision_pers"))
                txtpaymnt_cmsn.Text = clsCommon.myCstr(myDr("payment_commision_pers"))
                'cmbincentive.SelectedValue = clsCommon.myCstr(myDr("incentive"))
                FndIncentive.Value = clsCommon.myCstr(myDr("incentive"))
                GetIIncentiveDetails(False)
                txtno_days.Text = clsCommon.myCstr(myDr("incentive_days"))
                cmbvsppayment.SelectedValue = clsCommon.myCstr(myDr("vsp_payment"))
                txtpayeename.Text = clsCommon.myCstr(myDr("vsp_payee_name"))
                ''cmbservc_type.Text = clsCommon.myCstr(myDr("Service_Charge_Type"))
                ''txtChequeInFavour.Text = clsCommon.myCstr(myDr("Cheque_In_Favour_Of"))
                '====
                txtjointname.Text = clsCommon.myCstr(myDr("Joint_Name")) 'fndVendorCOde.Value = clsCommon.myCstr(myDr("Joint_Name"))
                TxtSecurityCharges.Text = clsCommon.myCdbl(myDr("Security_Amount"))
                If clsCommon.myLen(myDr("Billing_Date")) > 0 Then
                    DtpBillingDate.Value = clsCommon.myCDate(myDr("Billing_Date"))
                    DtpEndBillingDate.Value = DtpBillingDate.Value.AddDays(2)
                End If

                TxtAmCU.Text = clsCommon.myCstr(myDr("AMCU"))
                TxtAmc_Charge.Text = clsCommon.myCdbl(myDr("AMC_Charge"))
                '---------------------------------------------------------------------------------

                Me.txtPhone1.Text = myDr(13).ToString()
                Me.txtPhone2.Text = myDr(14).ToString()
                Me.txtfax.Text = myDr(15).ToString()
                Me.txtEmail.Text = myDr(16).ToString()
                Me.txtWeb.Text = myDr(17).ToString()

                Me.fndvendortype.Value = myDr(29).ToString()
                Me.txtvendortypedes.Text = myDr(30).ToString()
                Me.txtbankcodedes.Text = myDr(32).ToString()
                Me.txtCredit.Text = myDr(36).ToString()
                chkIsGrossReceipt.Checked = IIf(clsCommon.myCdbl(myDr("is_Gross_Receipt")) = 1, True, False)

                '' multicurrency
                Me.fndVendorCurrency.Value = myDr("currency_code").ToString()
                '' end multicurrency

                Dim strtran As String = myDr(64).ToString()
                If strtran = "N" Then
                    chktrarns.Checked = False
                ElseIf strtran = "Y" Then
                    chktrarns.Checked = True
                ElseIf strtran = "" Then
                    chktrarns.Checked = False
                End If
                ''Me.txtcst.Text = myDr(65).ToString()
                ''Me.txtecc.Text = myDr(66).ToString()
                ''Me.txtrange.Text = myDr(67).ToString()
                Me.txtcollect.Text = myDr(68).ToString()
                Me.txtpan.Text = myDr(69).ToString()

                If objCommonVar.GSTApplicable Then
                    Me.txtGST_PanCode.Text = myDr(69).ToString()
                End If


                Dim interbranch As String = myDr("Inter_Branch").ToString()
                If interbranch = "Y" Then
                    chkInterBranch.Checked = True
                ElseIf interbranch = "N" Then
                    chkInterBranch.Checked = False
                End If
                Dim strtagasfranchise As String = myDr("franchise_yn").ToString()
                If strtagasfranchise = "Y" Then
                    chkTagAsFranchise.Checked = True
                ElseIf interbranch = "N" Then
                    chkTagAsFranchise.Checked = False
                End If

                '' Anubhooti 1-Aug-2014 BM00000003318
                Me.TxtBankName.Text = myDr("Bank_Name").ToString()
                Me.TxtBankBranch.Text = myDr("Branch_Name").ToString()

                Me.TxtAccNo.Text = myDr("Account_No").ToString()
                'Me.TxtIFSCCode.Text = myDr("IFSC_Code").ToString()

                If EnableBankFromMaster = True Then
                    findfndbankcode.Value = myDr(31).ToString()
                    findTxtIFSCCode.Value = myDr("IFSC_Code").ToString()


                Else
                    Me.fndbankcode.Text = myDr(31).ToString()
                    Me.TxtIFSCCode.Text = myDr("IFSC_Code").ToString()

                End If



                txtTIPBuffalo.Value = clsCommon.myCdbl(myDr("TIP_Buffalo"))
                txtTIPCow.Value = clsCommon.myCdbl(myDr("TIP_Cow"))
                txtTIPMix.Value = clsCommon.myCdbl(myDr("TIP_Mix"))
                'Sanjay
                Dim TempCust_Group_Code As String = clsDBFuncationality.getSingleValue("select isnull(Cust_Group_Code,'') as Cust_Group_Code from tspl_customer_master where CUSTOMER_FORM_TYPE='VSP' AND cust_code='" + fndvendorNo.Value + "'")
                If clsCommon.myLen(TempCust_Group_Code) > 0 Then
                    chkCreateCustomerAlso.Checked = True
                    fndCusgrp.Value = clsCommon.myCstr(TempCust_Group_Code)
                Else
                    chkCreateCustomerAlso.Checked = False
                    fndCusgrp.Value = Nothing
                End If
                '==================================================
                Dim qryPR As Object = clsDBFuncationality.getSingleValue("SELECT Registered_PDCS_CLUSTER FROM TSPL_VLC_MASTER_HEAD WHERE VSP_Code='" + fndvendorNo.Value + " '")
                'Dim strRegistered_PDCS_CLUSTER As String = clsCommon.myCstr(myDr(qryPR))
                Dim strRegistered_PDCS_CLUSTER As String
                If IsDBNull(qryPR) Then
                    strRegistered_PDCS_CLUSTER = String.Empty
                Else
                    strRegistered_PDCS_CLUSTER = clsCommon.myCstr(qryPR)

                End If
                'If clsCommon.myLen(qryPR) > 0 Then
                '    strRegistered_PDCS_CLUSTER = clsCommon.myCstr(myDr(qryPR))
                'End If
                'Dim strRegistered_PDCS_CLUSTER As String = clsCommon.myCstr(myDr("Registered_PDCS_CLUSTER"))
                If clsCommon.CompairString(strRegistered_PDCS_CLUSTER, "Registered") = CompairStringResult.Equal Then
                    chkRegistered.Checked = True
                ElseIf clsCommon.CompairString(strRegistered_PDCS_CLUSTER, "PDCS") = CompairStringResult.Equal Then
                    chkPDCS.Checked = True
                ElseIf clsCommon.CompairString(strRegistered_PDCS_CLUSTER, "CLUSTER") = CompairStringResult.Equal Then
                    chkCLUSTER.Checked = True
                Else
                    chkRegistered.Checked = False
                    chkPDCS.Checked = False
                    chkCLUSTER.Checked = False
                End If
                If clsCommon.myLen(clsCommon.myCstr(myDr("StartDate"))) > 0 Then
                    txtStartDate.Value = clsCommon.myCDate((myDr("StartDate")))
                Else
                    txtStartDate.Value = Nothing
                End If

                If clsCommon.myLen(clsCommon.myCstr(myDr("RegistrationDate"))) > 0 Then
                    txtRegistrationDate.Value = clsCommon.myCDate((myDr("RegistrationDate")))
                Else
                    txtRegistrationDate.Value = Nothing
                End If
                txtRegistrationNo.Text = clsCommon.myCstr((myDr("RegistrationNo")))
                chkOwnBMC.Checked = clsfrmVLCMaster.IsOwnBMCByVSPCode(fndvendorNo.Value, Nothing) 'clsCommon.myCBool(clsCommon.myCdbl(myDr("isOwnBMC")))
                If chkOwnBMC.Checked = True Then
                    txtMCCOwnBMC.Value = clsfrmVLCMaster.OwnBMCCode(fndvendorNo.Value, Nothing) 'clsCommon.myCstr(myDr("MCCOwnBMC"))
                    lblMCCOwnBMC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select MCC_NAME from tspl_MCC_MASTER where MCC_Code = '" + txtMCCOwnBMC.Value + "'"))
                End If
                findfndbankcode2.Value = clsCommon.myCstr(myDr("BankCode2"))
                fndbankcode2.Text = clsCommon.myCstr(myDr("BankCode2"))
                If clsCommon.myLen(findfndbankcode2.Value) > 0 Then
                    txtbankcodedes2.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select BankCode2 from tspl_vendor_master where BankCode2 = '" + clsCommon.myCstr(findfndbankcode2.Value) + "'"))
                Else
                    txtbankcodedes2.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select BankCode2 from tspl_vendor_master where BankCode2 = '" + clsCommon.myCstr(fndbankcode2.Text) + "'"))
                End If
                txtCredit2.Text = clsCommon.myCdbl(myDr("Credit2"))
                txtIFSCCode2.Text = clsCommon.myCstr(myDr("IFSCCode2"))
                TxtAccNo2.Text = clsCommon.myCstr(myDr("AccNo2"))
                TxtBankName2.Text = clsCommon.myCstr(myDr("BankName2"))
                TxtSecurityCharges2.Text = clsCommon.myCstr(myDr("SecurityCharges2"))
                findTxtIFSCCode2.Value = clsCommon.myCstr(myDr("IFSCCode2"))
                txtBankBranch2.Text = clsCommon.myCstr(myDr("BankBranch2"))
                txtSupervisiorRP.Value = clsCommon.myCstr(myDr("SupervisorOrRP"))
                txtvendornameHindi.Text = clsCommon.myCstr(myDr("Vendor_name_Hindi"))
                lblSupervisiorRPName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + txtSupervisiorRP.Value + "' "))
                txtDistrict.Value = clsCommon.myCstr(myDr("DISTRICT_Code"))
                lblDistrict.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Name  from TSPL_DISTRICT_MASTER where Code = '" + clsCommon.myCstr(myDr("DISTRICT_Code")) + "' "))
                txtBlockCode.Value = clsCommon.myCstr(myDr("BLOCK_CODE"))
                lblBlockCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BLOCK_NAME from TSPL_BLOCK_MASTER where BLOCK_CODE = '" + clsCommon.myCstr(myDr("BLOCK_CODE")) + "' "))
                txtZone.Value = clsCommon.myCstr(myDr("Zone_Code"))
                lblZone.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_ZONE_MASTER where Zone_Code  = '" + clsCommon.myCstr(myDr("Zone_Code")) + "' "))
                txtCastCategory.Value = clsCommon.myCstr(myDr("CAST_CATEGORY_CODE"))
                txtSavingCompanyBank.Value = clsCommon.myCstr(myDr("Company_Bank"))
                lblSavingCompanyBank.Text = clsBankMaster.GetName(clsCommon.myCstr(myDr("Company_Bank")))
                txtCurrentCompanyBank.Value = clsCommon.myCstr(myDr("Company_Bank_Current"))
                lblCurrentCompanyBank.Text = clsBankMaster.GetName(clsCommon.myCstr(myDr("Company_Bank_Current")))
                txtVidhanSabha.Value = clsCommon.myCstr(myDr("VIDHAN_SABHA_CODE"))
                lblVidhanSabha.Text = clsVidhanSabhaMaster.GetName(clsCommon.myCstr(myDr("VIDHAN_SABHA_CODE")))
                txtPanchayatSamiti.Value = clsCommon.myCstr(myDr("PANCHAYAT_SAMITI_CODE"))
                lblPanchayatSamiti.Text = clsPanchayatSamitiMaster.GetName(clsCommon.myCstr(myDr("PANCHAYAT_SAMITI_CODE")))
                txtGrampanchayat.Value = clsCommon.myCstr(myDr("GRAMPANCHAYAT_CODE"))
                lblGrampanchayat.Text = clsGrampanchayatMaster.GetName(clsCommon.myCstr(myDr("GRAMPANCHAYAT_CODE")))
                txtRevenueVillage.Value = clsCommon.myCstr(myDr("REVENUE_VILLAGE_CODE"))
                lblRevenueVillage.Text = clsRevenueVillageMaster.GetName(clsCommon.myCstr(myDr("REVENUE_VILLAGE_CODE")))
                '================================================== ,  ,  , 
                UcAttachment1.LoadData(fndvendorNo.Value)
            Next

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
    End Sub

    Sub GenerateVoucherNo(ByVal trans As SqlTransaction)

        Try
            If chkPDCS.Checked = True Then
                fndvendorNo.Value = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, clsDocTransactionType.PDCS, "")
                'ElseIf chkCLUSTER.Checked = True Then
                '    fndvendorNo.Value = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, clsDocTransactionType.CLUSTER, "")
            Else
                fndvendorNo.Value = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, clsDocTransactionType.Registered, "")
            End If

            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub
    'For inserting the data in the database
    Public Sub funinsert()
        If chkCreateCustomerAlso.Checked = True Then
            If fndCusgrp.Value = "" Then
                myMessages.blankValue(Me, "Please Select Customer Group Code", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                fndCusgrp.Focus()
                fndCusgrp.Select()
                Return
            End If
        End If

        'Create Mcc Master
        Try
            If chkOwnBMC.Checked = True AndAlso clsCommon.myLen(txtMCCOwnBMC.Value) <= 0 Then
                Dim qry As String = " select count (*)  from TSPL_MCC_MASTER where Mcc_Code_VLC_Uploader = '" + txtVLCCodeVlcUploader.Text + "'  "
                Dim checkValid As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry))
                If checkValid = False Then

                    dtDefault = New DataTable()
                    Dim newBlankRow1 As DataRow = dtDefault.NewRow
                    dtDefault.Rows.Add(newBlankRow1)
                    Dim objDefaultTemplate As clsExportTemplate = clsExportTemplate.GetDefaultData("MP-IMP-TMP")
                    If (objDefaultTemplate IsNot Nothing AndAlso clsCommon.myLen(objDefaultTemplate.Export_Code) > 0) Then
                        If objDefaultTemplate.Arr IsNot Nothing AndAlso objDefaultTemplate.Arr.Count > 0 Then
                            For Each objTr As clsExportTemplateDetail In objDefaultTemplate.Arr
                                If clsCommon.myLen(objTr.Column_Name) > 0 Then
                                    arrExistCols.Add(objTr.Column_Name)
                                    Dim newColumn As New DataColumn(clsCommon.myCstr(objTr.Column_Name), GetType(System.String))
                                    dtDefault.Columns.Add(newColumn)
                                    dtDefault.Rows(0).Item(clsCommon.myCstr(objTr.Column_Name)) = clsCommon.myCstr(objTr.Column_Header)
                                End If
                            Next
                        End If
                    End If

                    If (dtDefault IsNot Nothing AndAlso clsCommon.myLen(dtDefault.Rows.Count) > 0) Then
                        CreateNewMCC(txtVLCCodeVlcUploader.Text)
                    Else
                        Throw New Exception("Please set Default Templete to create MCC Master")
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "VSP/VLC Master")
            Return
        End Try
        'Create Mcc Master

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowVSPMasterAutoPrefix = 1 Then
                GenerateVoucherNo(trans)
            End If
            Dim Registered As Integer = 0
            If objCommonVar.GSTApplicable Then
                If Rchkregistered.Checked = True Then
                    Registered = 1
                Else
                    Registered = 0
                    txtGST_PanCode.Text = ""
                    txtEntity.Text = ""
                    txtGSTStateCode.Text = ""
                    txtGSTIN_No_final.Text = ""
                End If

            End If


            Dim strStatus As String = ""
            If chkInActive.Checked = True Then
                strStatus = "Y"                    '******* for:In-Active ********
            ElseIf chkInActive.Checked = False Then
                strStatus = "N"                    '******* for:Active ******** 
            End If

            If clsCommon.myLen(fndvendorNo.Value) <= 0 AndAlso strStatus = "N" Then
                lblActiveDate.Text = connectSql.serverDate(trans)
            ElseIf clsCommon.myLen(fndvendorNo.Value) <= 0 AndAlso strStatus = "Y" Then
                lblActiveDate.Text = ""
            ElseIf clsCommon.myLen(fndvendorNo.Value) > 0 AndAlso strStatus = "N" Then

            ElseIf clsCommon.myLen(fndvendorNo.Value) > 0 AndAlso strStatus = "Y" Then
                lblActiveDate.Text = ""
            End If

            Dim strInterBranch As String = ""
            If chkInterBranch.Checked = True Then
                strInterBranch = "Y"
            Else
                strInterBranch = "N"
            End If

            Dim strTagAsFranchise As String
            If chkTagAsFranchise.Checked = True Then
                strTagAsFranchise = "Y"  '''''   for taging vendor as franchise
            Else
                strTagAsFranchise = "N" '''''   for untaging vendor as franchise
            End If

            Dim strHold As String = ""
            If chkHold.Checked = True Then
                strHold = "Y"                      '******* for:Hold ******** 
            ElseIf chkHold.Checked = False Then
                strHold = "N"                      '******* for:Remove Hold ********
            End If


            Dim strtrans As String = ""
            If chktrarns.Checked = True Then
                strtrans = "Y"                      '******* for:Transporter type ******** 
            ElseIf chktrarns.Checked = False Then
                strtrans = "N"                      '******* for:Remove Non Transporter type ********
            End If



            Dim strTax1 As String = ""
            Dim strTax1_Rate As Decimal = 0.0

            Dim strTax2 As String = ""
            Dim strTax2_Rate As Decimal = 0.0

            Dim strTax3 As String = ""
            Dim strTax3_Rate As Decimal = 0.0

            Dim strTax4 As String = ""
            Dim strTax4_Rate As Decimal = 0.0

            Dim strTax5 As String = ""
            Dim strTax5_Rate As Decimal = 0.0

            Dim strTax6 As String = ""
            Dim strTax6_Rate As Decimal = 0.0

            Dim strTax7 As String = ""
            Dim strTax7_Rate As Decimal = 0.0

            Dim strTax8 As String = ""
            Dim strTax8_Rate As Decimal = 0.0

            Dim strTax9 As String = ""
            Dim strTax9_Rate As Decimal = 0.0

            Dim strTax10 As String = ""
            Dim strTax10_Rate As Decimal = 0.0

            ' Dim Bal1 As Decimal
            Dim CrLimit As Decimal
            'Dim OutComm As Decimal

            If txtCredit.Text = "" Then
                CrLimit = Convert.ToDecimal("0.00")
            Else
                CrLimit = Convert.ToDecimal(txtCredit.Text)
            End If

            Dim joint_name As String = clsCommon.myCstr(txtjointname.Text.Replace("'", "`")) 'clsCommon.myCstr(fndVendorCOde.Value)

            Dim IsGrossReceipt As Integer = IIf(chkIsGrossReceipt.Checked, 1, 0)

            Dim TermsCode As String = ""
            Dim TermsCodeDesc As String = ""
            Dim AccountSetCode As String = ""
            Dim AccountSetCodeDesc As String = ""
            Dim TaxGroupCode As String = ""
            Dim TaxGroupDesc As String = ""
            Dim dr As DataTable
            dr = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Acct_Set_Desc,Terms_Code,Terms_Desc,Tax_Group_Code,Tax_Group_Desc from Tspl_vendor_group where ven_group_code='" & fndgroupcode.Value & "'", trans)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                TermsCode = dr.Rows(0)("Terms_Code").ToString()
                TermsCodeDesc = dr.Rows(0)("Terms_Desc").ToString()
                AccountSetCode = dr.Rows(0)("Acct_Set_Code").ToString()
                AccountSetCodeDesc = dr.Rows(0)("Acct_Set_Desc").ToString()
                TaxGroupCode = dr.Rows(0)("Tax_Group_Code").ToString()
                TaxGroupDesc = dr.Rows(0)("Tax_Group_Desc").ToString()
            End If

            connectSql.RunSpTransaction(trans, "sp_TSPL_VENDOR_MASTER_INSERT", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@Vendor_Name", txtvendorname.Text.ToString()), New SqlParameter("@Vendor_Group_Code", fndgroupcode.Value), New SqlParameter("Vendor_Group_Des", txtgroupdes.Text.ToString()), New SqlParameter("@Status", strStatus), New SqlParameter("@OnHold", strHold), New SqlParameter("@transporter", strtrans), New SqlParameter("@Closing_Date", Format(Me.dtClosing.Value, "dd/MM/yyyy")), New SqlParameter("@Add1", txtAdd1.Text.ToString()), New SqlParameter("@Add2", txtAdd2.Text.ToString()), New SqlParameter("@Add3", txtAdd3.Text.ToString()), New SqlParameter("@City_Code", fndCity.Value), New SqlParameter("@City_Des", txtCity.Text.ToString()), New SqlParameter("@State", txtState.Text.ToString()), New SqlParameter("@Country", txtCountry.Text.ToString()), New SqlParameter("@Phone1", txtPhone1.Text.ToString()), New SqlParameter("@Phone2", txtPhone2.Text.ToString()), New SqlParameter("@Fax", txtfax.Text.ToString()), New SqlParameter("@Email", txtEmail.Text.ToString()), New SqlParameter("@WebSite", txtWeb.Text.ToString()), New SqlParameter("@Contact_Person_Name", ""), New SqlParameter("@Contact_Person_Phone", ""), New SqlParameter("@Contact_Person_Fax", ""), New SqlParameter("@Contact_Person_Website", ""), New SqlParameter("@Contact_Person_Email", ""), New SqlParameter("@Terms_Code", TermsCode), New SqlParameter("@Terms_Code_Des", TermsCodeDesc), New SqlParameter("@Vendor_Account", AccountSetCode), New SqlParameter("@Vendor_Account_Set_Des", AccountSetCodeDesc), New SqlParameter("@Payment_Code", ""), New SqlParameter("@Payment_Code_Des", ""), New SqlParameter("@Vendor_Type_Code", fndvendortype.Value), New SqlParameter("@Vendor_Type_Des", txtvendortypedes.Text.ToString()), New SqlParameter("@Bank_Code", IIf(EnableBankFromMaster = True, findfndbankcode.Value, fndbankcode.Text)), New SqlParameter("@Bank_Code_Des", txtbankcodedes.Text.ToString()), New SqlParameter("@Service_Tax_No", ""), New SqlParameter("@Lst_No", ""), New SqlParameter("@Tin_No", ""), New SqlParameter("@Credit_Limit", CrLimit), New SqlParameter("@Tax_Group", TaxGroupCode), New SqlParameter("@Tax_Group_Des", TaxGroupDesc), New SqlParameter("@TAX1", strTax1), New SqlParameter("@TAX1_Rate", strTax1_Rate), New SqlParameter("@TAX2", strTax2), New SqlParameter("@TAX2_Rate", strTax2_Rate), New SqlParameter("@TAX3", strTax3), New SqlParameter("@TAX3_Rate", strTax3_Rate), New SqlParameter("@TAX4", strTax4), New SqlParameter("@TAX4_Rate", strTax4_Rate), New SqlParameter("@TAX5", strTax5), New SqlParameter("@TAX5_Rate", strTax5_Rate), New SqlParameter("@TAX6", strTax6), New SqlParameter("@TAX6_Rate", strTax6_Rate), New SqlParameter("@TAX7", strTax7), New SqlParameter("@TAX7_Rate", strTax7_Rate), New SqlParameter("@TAX8", strTax8), New SqlParameter("@TAX8_Rate", strTax8_Rate), New SqlParameter("@TAX9", strTax9), New SqlParameter("@TAX9_Rate", strTax9_Rate), New SqlParameter("@TAX10", strTax10), New SqlParameter("@TAX10_Rate", strTax10_Rate), New SqlParameter("@Remarks1", ""), New SqlParameter("@Remarks2", ""), New SqlParameter("@Additional1", ""), New SqlParameter("@Additional2", ""), New SqlParameter("@Additional3", ""), New SqlParameter("@cst", ""), New SqlParameter("@ecc", ""), New SqlParameter("@range", ""), New SqlParameter("@collectorate", txtcollect.Text.ToString()), New SqlParameter("@pan", txtpan.Text.ToString()), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@is_Gross_Receipt", IsGrossReceipt), New SqlParameter("@Inter_branch", strInterBranch), New SqlParameter("@Branch_Name ", TxtBankBranch.Text.ToString()), New SqlParameter("@Account_No ", TxtAccNo.Text.ToString()), New SqlParameter("@Bank_Name ", TxtBankName.Text.ToString()), New SqlParameter("@IFSC_Code ", IIf(EnableBankFromMaster = True, findTxtIFSCCode.Value, TxtIFSCCode.Text.ToString())), New SqlParameter("@Account_Type ", clsCommon.myCstr(cmbAccountType.SelectedValue)))
            clsDBFuncationality.ExecuteNonQuery(GetUpdateQry(strTagAsFranchise, joint_name), trans) ', srvc_type

            Dim strCmd11 As String = "Update TSPL_VENDOR_MASTER set Is_Inactive_In_Milk_Procurement='" + IIf(chkInactiveInMilkModule.Checked, "1", "0") + "', is_Hold_Payment_Process='" + IIf(chkHoldPaymentProcess.Checked, "1", "0") + "',Is_Blacklist='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chk_isblacklist.Checked) = True, 1, 0)) + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            connectSql.RunSqlTransaction(trans, strCmd11)

            If clsCommon.myLen(cmbGender.Text) > 0 Then
                connectSql.RunSqlTransaction(trans, "Update TSPL_VENDOR_MASTER set Gender = '" + cmbGender.Text + "'  where Vendor_Code='" + fndvendorNo.Value + "' ")
            End If
            If clsCommon.myLen(lblActiveDate.Text) > 0 Then
                connectSql.RunSqlTransaction(trans, "Update TSPL_VENDOR_MASTER set Active_Date = '" + clsCommon.GetPrintDate(lblActiveDate.Text, "dd/MMM/yyyy") + "'  where Vendor_Code='" + fndvendorNo.Value + "' ")
            End If

            If objCommonVar.GSTApplicable = True Then
                Dim streq As String = "Update TSPL_VENDOR_MASTER set GSTRegistered='" & Registered & "',GSTEntity='" & txtEntity.Text & "',GSTLastEntity='" & MyTextBox2.Text & "',GSTFinalNo='" & txtGSTIN_No_final.Text & "',GSTMiddle='" & txtFixxed.Text & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(streq, trans)
            End If

            '' multicurrency
            Dim strq As String
            If Me.fndVendorCurrency.Visible = True Then
                strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE='" & clsCommon.myCstr(Me.fndVendorCurrency.Value) & "',Form_Type='" + txtvndrtype.Text + "',state_code='" + txtstatecode.Value + "',country_code='" + txtcountrycode.Value + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            Else
                strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE='" & objCommonVar.BaseCurrencyCode & "',Form_Type='" + txtvndrtype.Text + "',state_code='" + txtstatecode.Value + "',country_code='" + txtcountrycode.Value + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            End If
            clsDBFuncationality.ExecuteNonQuery(strq, trans)

            If clsCommon.myCdbl(Me.TxtSecurityCharges.Text) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set Security_Amount='" & clsCommon.myCdbl(Me.TxtSecurityCharges.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myLen(Me.TxtAmCU.Text) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set AMCU='" & clsCommon.myCstr(Me.TxtAmCU.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myCdbl(Me.TxtAmc_Charge.Text) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set AMC_Charge ='" & clsCommon.myCstr(Me.TxtAmc_Charge.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If

            If clsCommon.myLen(Me.fndpaymentCycle.Value) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set PC_CODE ='" & clsCommon.myCstr(Me.fndpaymentCycle.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myLen(txtAliesName.Text) > 0 Then
                Dim AliesQry As String = "Update TSPL_VENDOR_MASTER set Alies_Name='" & clsCommon.myCstr(txtAliesName.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(AliesQry, trans)
            End If

            Dim CorrectionFatSNF As String = "update TSPL_VENDOR_MASTER set TIP_Buffalo='" & clsCommon.myCstr(txtTIPBuffalo.Value) & "',TIP_Cow='" & clsCommon.myCdbl(txtTIPCow.Value) & "',TIP_Mix='" & clsCommon.myCdbl(txtTIPMix.Value) & "', CorrectionFat='" & clsCommon.myCdbl(numCorrectionFat.Text) & "' , CorrectionSNF='" & clsCommon.myCdbl(numCorrectionSNF.Text) & "',Credit_Limit_On_Milk_Receipt_Per='" + clsCommon.myCstr(IIf(chkCreditLimitBasedOnMilkReceipt.Checked, TxtCreditLimitBasedOnMilkReceipt.Value, -1)) + "' where Vendor_Code='" & fndvendorNo.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(CorrectionFatSNF, trans)

            Dim VendorNameHindi As String = " update TSPL_VENDOR_MASTER set Vendor_name_Hindi = N'" + txtvendornameHindi.Text + "' where Vendor_Code='" + fndvendorNo.Value + "' "
            clsDBFuncationality.ExecuteNonQuery(VendorNameHindi, trans)

            If clsCommon.myLen(txtDistrict.Value) > 0 Then
                Dim qryDistrict As String = " Update TSPL_VENDOR_MASTER set DISTRICT_Code = '" & clsCommon.myCstr(txtDistrict.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryDistrict, trans)
            End If
            If clsCommon.myLen(txtZone.Value) > 0 Then
                Dim qryZone As String = " Update TSPL_VENDOR_MASTER set Zone_Code ='" & clsCommon.myCstr(txtZone.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryZone, trans)
            End If

            If clsCommon.myLen(txtCastCategory.Value) > 0 Then
                Dim qryCastCategory As String = " Update TSPL_VENDOR_MASTER set CAST_CATEGORY_CODE ='" & clsCommon.myCstr(txtCastCategory.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryCastCategory, trans)
            End If

            If clsCommon.myLen(txtBlockCode.Value) > 0 Then
                Dim qryBlockCode As String = " Update TSPL_VENDOR_MASTER set BLOCK_CODE ='" & clsCommon.myCstr(txtBlockCode.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryBlockCode, trans)
            End If

            If clsCommon.myLen(txtRevenueVillage.Value) > 0 Then
                Dim qryRevenueVillage As String = " Update TSPL_VENDOR_MASTER set REVENUE_VILLAGE_CODE ='" & clsCommon.myCstr(txtRevenueVillage.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryRevenueVillage, trans)
            End If

            If clsCommon.myLen(txtGrampanchayat.Value) > 0 Then
                Dim qryGrampanchayat As String = " Update TSPL_VENDOR_MASTER set GRAMPANCHAYAT_CODE ='" & clsCommon.myCstr(txtGrampanchayat.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryGrampanchayat, trans)
            End If

            If clsCommon.myLen(txtPanchayatSamiti.Value) > 0 Then
                Dim qryPanchayatSamiti As String = " Update TSPL_VENDOR_MASTER set PANCHAYAT_SAMITI_CODE ='" & clsCommon.myCstr(txtPanchayatSamiti.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryPanchayatSamiti, trans)
            End If

            If clsCommon.myLen(txtVidhanSabha.Value) > 0 Then
                Dim qryVidhanSabha As String = " Update TSPL_VENDOR_MASTER set VIDHAN_SABHA_CODE ='" & clsCommon.myCstr(txtVidhanSabha.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryVidhanSabha, trans)
            End If

            Dim HeadLoadBasis As String = Nothing
            If ChkHeadLoad.Checked Then
                If CmbHeadLoadServiceBasis.SelectedIndex = 1 Then
                    HeadLoadBasis = "P"
                ElseIf CmbHeadLoadServiceBasis.SelectedIndex = 2 Then
                    HeadLoadBasis = "K"
                ElseIf CmbHeadLoadServiceBasis.SelectedIndex = 3 Then
                    HeadLoadBasis = "L"
                End If
            End If
            Dim qryHeadLoad As String = " Update TSPL_VENDOR_MASTER set Is_Head_Load ='" & IIf(ChkHeadLoad.Checked = True, "T", "F") & "',Rate_Head_Load='" & clsCommon.myCDecimal(txtRateHeadLoad.Text) & "',Service_Basis_Head_Load='" & clsCommon.myCstr(HeadLoadBasis) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qryHeadLoad, trans)

            updateMultipleIncentive(fndvendorNo.Value, trans)

            'Sanjay
            If chkCreateCustomerAlso.Checked = True Then
                If CreateCustomer(True, trans) = False Then
                    Throw New Exception("Error while create customer")
                End If
            End If
            'Sanjay
            If objCommonVar.ApplyDefaultsInMaster = True Then
                CreateDefaultMasters(trans)
            End If
            'VLC
            If clsCommon.myLen(txtvlcname.Text) > 0 Then
                VLCSaveData(True, trans)
                If objCommonVar.ApplyDefaultsInMaster = True Then
                    CreateDefaultUserMaster(trans)
                End If
            End If
            UpdateFeild(fndvendorNo.Value, fndvlccode.Text, trans)
            UcAttachment1.SaveData(fndvendorNo.Value, True, trans)
            trans.Commit()

            myMessages.insert()
            btnsave.Text = "Update"
            btndelete.Enabled = True
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Sub CreateDefaultUserMaster(ByVal trans As SqlTransaction)
        Dim qry As String = ""
        Dim check As Integer = 0
        Dim coll As New Hashtable()
        Try
            'Create User
            Dim PrefixUserCode As String = UserPrefix + txtVLCCodeVlcUploader.Text
            qry = "select count(User_Code) from TSPL_USER_MASTER where User_Code='" + PrefixUserCode + "'"
            check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
            If check <= 0 Then
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "User_Code", PrefixUserCode)
                clsCommon.AddColumnsForChange(coll, "User_Name", clsCommon.myCstr(txtvendorname.Text))
                clsCommon.AddColumnsForChange(coll, "Password", clsCommon.EncryptString(txtVLCCodeVlcUploader.Text))
                clsCommon.AddColumnsForChange(coll, "Default_Location", fndMcc.Value, True)
                clsCommon.AddColumnsForChange(coll, "User_APP_Type", "V", True)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", txtvspcode.Value, True)
                clsCommon.AddColumnsForChange(coll, "User_Type", "")
                clsCommon.AddColumnsForChange(coll, "EMP_CODE", "")
                clsCommon.AddColumnsForChange(coll, "Emp_Name", "")
                clsCommon.AddColumnsForChange(coll, "Comp_Code", companyCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", userCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modify_By", userCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_USER_MASTER", OMInsertOrUpdate.Insert, "", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub CreateDefaultMasters(ByVal trans As SqlTransaction)
        Dim qry As String = ""
        Dim check As Integer = 0
        Dim coll As New Hashtable()
        Try
            ''Create User
            'Dim PrefixUserCode As String = UserPrefix + txtVLCCodeVlcUploader.Text
            'qry = "select count(User_Code) from TSPL_USER_MASTER where User_Code='" + PrefixUserCode + "'"
            'check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
            'If check <= 0 Then
            '    coll = New Hashtable()
            '    clsCommon.AddColumnsForChange(coll, "User_Code", PrefixUserCode)
            '    clsCommon.AddColumnsForChange(coll, "User_Name", clsCommon.myCstr(txtvendorname.Text))
            '    clsCommon.AddColumnsForChange(coll, "Password", clsCommon.EncryptString(txtVLCCodeVlcUploader.Text))
            '    clsCommon.AddColumnsForChange(coll, "Default_Location", fndMcc.Value, True)
            '    clsCommon.AddColumnsForChange(coll, "User_APP_Type", "V", True)
            '    clsCommon.AddColumnsForChange(coll, "Vendor_Code", txtvspcode.Value, True)
            '    clsCommon.AddColumnsForChange(coll, "User_Type", "")
            '    clsCommon.AddColumnsForChange(coll, "EMP_CODE", "")
            '    clsCommon.AddColumnsForChange(coll, "Emp_Name", "")
            '    clsCommon.AddColumnsForChange(coll, "Comp_Code", companyCode)
            '    clsCommon.AddColumnsForChange(coll, "Created_By", userCode)
            '    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            '    clsCommon.AddColumnsForChange(coll, "Modify_By", userCode)
            '    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            '    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_USER_MASTER", OMInsertOrUpdate.Insert, "", trans)
            'End If

            '' Start Primary Transporter Master
            Dim StrVdrNo As String = ""
            Dim StrTempVSPName As String = clsCommon.myCstr(txtvendorname.Text).Replace(" ", "")
            StrTempVSPName = StrTempVSPName.Replace("'", "")
            'qry = "select count(*) from TSPL_VENDOR_MASTER Inner Join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code where TSPL_VENDOR_MASTER.Vendor_Name ='" + StrTempVSPName + "' and TSPL_VENDOR_MASTER.form_type='VSP' And TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + clsCommon.myCstr(txtVLCCodeVlcUploader.Text) + "'"
            'check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
            'If check <= 0 Then
            '    coll = New Hashtable()
            '    clsCommon.AddColumnsForChange(coll, "Vendor_Name", StrTempVSPName)
            '    clsCommon.AddColumnsForChange(coll, "Closing_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            '    clsCommon.AddColumnsForChange(coll, "State", txtState.Text)
            '    clsCommon.AddColumnsForChange(coll, "form_type", "VSP")
            '    clsCommon.AddColumnsForChange(coll, "state_code", txtstatecode.Value, True)
            '    clsCommon.AddColumnsForChange(coll, "City_Code", fndCity.Value, True)
            '    clsCommon.AddColumnsForChange(coll, "City_Code_Desc", txtCity.Text, True)
            '    clsCommon.AddColumnsForChange(coll, "PC_CODE", fndpaymentCycle.Value, True)
            '    clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code", fndgroupcode.Value)
            '    clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code_Desc", txtgroupdes.Text)
            '    clsCommon.AddColumnsForChange(coll, "Start_Date", Nothing, True)
            '    clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
            '    clsCommon.AddColumnsForChange(coll, "is_Head_Load", "F")
            '    clsCommon.AddColumnsForChange(coll, "Status", "N")
            '    clsCommon.AddColumnsForChange(coll, "Onhold", "N")
            '    clsCommon.AddColumnsForChange(coll, "Transporter", "Y")
            '    clsCommon.AddColumnsForChange(coll, "Currency_Code", objCommonVar.BaseCurrencyCode)
            '    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            '    clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
            '    clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            '    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            '    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            '    StrVdrNo = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.PTMMASTER, "", "")
            '    clsCommon.AddColumnsForChange(coll, "Vendor_Code", StrVdrNo)
            '    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
            'End If
            ''end of Primary Transporter Master

            ''Primary Transporter Vehiclee Master

            'qry = "select count(*) from TSPL_Primary_Vehicle_Master where vehicle_code='" + StrTempVSPName + "'"
            'check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
            'If check <= 0 Then
            '    Dim obj As clsfrmPrimaryTransporterVehicalMaster
            '    obj = New clsfrmPrimaryTransporterVehicalMaster()
            '    obj.docno = StrTempVSPName
            '    obj.primarycode = StrVdrNo
            '    obj.primaryname = StrTempVSPName
            '    obj.mcccode = clsCommon.myCstr(fndMcc.Value)
            '    obj.mccname = clsMccMaster.GetName(fndMcc.Value, trans)
            '    obj.pricekm = 0
            '    obj.status = "Rate/K.M"
            '    clsfrmPrimaryTransporterVehicalMaster.SaveData(obj.docno, True, obj, trans)
            'End If


            ''' Milk Route Master
            If False Then
                qry = "select count(*) from tspl_mcc_route_master where route_Name='" + StrTempVSPName + "'"
                check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                If check <= 0 Then
                    Dim objMRM As clsfrmMilkRouteMaster
                    objMRM = New clsfrmMilkRouteMaster()
                    objMRM.arr_VLC_Detail = Nothing
                    objMRM = New clsfrmMilkRouteMaster
                    objMRM.code = StrTempVSPName
                    objMRM.desc = StrTempVSPName
                    objMRM.vehiclecode = StrTempVSPName
                    objMRM.Active = 1
                    objMRM.mcccode = clsCommon.myCstr(fndMcc.Value)
                    objMRM.mccname = clsMccMaster.GetName(fndMcc.Value, trans)
                    objMRM.kilometer = 0
                    clsfrmMilkRouteMaster.SaveData(objMRM.code, trans, objMRM, True, True)
                    If clsCommon.myLen(txtroutecode.Value) <= 0 Then
                        txtroutecode.Value = clsCommon.myCstr(objMRM.code)
                        txtroutename.Text = clsfrmMilkRouteMaster.GetName(txtroutecode.Value, trans)
                    End If
                End If
            End If
            '''end of Milk Route master

            '' Village Master
            qry = "select Village_Code,Village_Name from TSPL_VILLAGE_MASTER where Village_Name='" + StrTempVSPName + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtvillcode.Value = clsCommon.myCstr(dt.Rows(0)("Village_Code"))
                txtvillname.Text = clsCommon.myCstr(dt.Rows(0)("Village_Name"))
            Else
                Dim objVillage As New clsfrmVillageMaster
                objVillage.villname = StrTempVSPName
                objVillage.citycode = fndCity.Value
                objVillage.statecode = txtstatecode.Value
                objVillage.countrycode = txtcountrycode.Value
                clsfrmVillageMaster.SaveData(objVillage, True, trans)
                txtvillcode.Value = objVillage.villcode
                txtvillname.Text = objVillage.villname

            End If

            '' End of Village MAster

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub VLCSaveData(ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction)
        Try
            If isNewEntry = True OrElse btnsave.Text = "Update" Then
                txtvspcode.Value = fndvendorNo.Value
                txtvsp.Text = txtvendorname.Text
            End If
            Dim obj As New clsfrmVLCMaster()
            obj.vlcCode = clsCommon.myCstr(fndvlccode.Text)
            obj.VLC_CODE_VLC_UPLOADER = clsCommon.myCstr(txtVLCCodeVlcUploader.Text)
            obj.vlcName = clsCommon.myCstr(txtvlcname.Text.Replace("'", "`"))
            'obj.vehical = clsCommon.myCstr(txtvehicalname.Text.Replace("'", "`"))
            obj.vspCode = clsCommon.myCstr(txtvspcode.Value)
            obj.MCCCOde = clsCommon.myCstr(fndMcc.Value)
            obj.mainvillcode = clsCommon.myCstr(txtvillcode.Value)
            obj.routecode = clsCommon.myCstr(txtroutecode.Value)
            obj.routename = clsCommon.myCstr(txtroutename.Text)
            obj.Active = IIf(Me.chkInActive.Checked, 0, 1)
            obj.Price_Code = clsCommon.myCstr(fndPriceCode.Value)
            obj.Apply_Cow_Price = chkApplyCowPrice.Checked
            obj.ApplyCowPriceDate = txtCowPriceDate.Value
            obj.IsSuspense = chkSuspense.Checked
            obj.Loyalty_Rate = txtLoyaltyPer.Value
            If chkOwnBMC.Checked Then
                obj.TFOwnBMC = True
                obj.OwnBMCDate = txtOwnBMCDate.Value
                obj.OwnBMC = clsCommon.myCstr(txtMCCOwnBMC.Value)
            Else
                obj.TFOwnBMC = False
                obj.OwnBMCDate = Nothing
            End If
            Dim arr As New List(Of clsfrmVLCMaster)
            obj.Form_ID = MyBase.Form_ID
            If clsfrmVLCMaster.SaveData(obj.vlcCode, isNewEntry, obj, arr, trans) Then
                fndvlccode.Text = obj.vlcCode
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub VLCLoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            'gv.Rows.Clear()

            Dim obj As clsfrmVLCMaster = clsfrmVLCMaster.GetData(strCode, arrLoc, NavType)
            'isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.vlcCode) > 0 Then
                'isNewEntry = False
                fndvlccode.Text = obj.vlcCode
                txtVLCCodeVlcUploader.Text = clsCommon.myCstr(obj.VLC_CODE_VLC_UPLOADER)
                txtvlcname.Text = obj.vlcName
                'txtvehicalname.Text = obj.vehical
                txtvspcode.Value = obj.vspCode
                If obj.Apply_Cow_Price = True Then
                    chkApplyCowPrice.Checked = True
                    txtCowPriceDate.Value = clsCommon.myCDate(obj.ApplyCowPriceDate)
                Else
                    chkApplyCowPrice.Checked = False
                End If

                chkSuspense.Checked = obj.IsSuspense
                txtvsp.Text = obj.VspName
                fndMcc.Value = obj.MCCCOde
                If clsCommon.myLen(fndMcc.Value) > 0 Then
                    lblMCCName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select MCC_NAME from tspl_MCC_MASTER where MCC_Code = '" + fndMcc.Value + "' "))
                Else
                    lblMCCName.Text = ""
                End If
                txtvillcode.Value = obj.mainvillcode
                txtvillname.Text = obj.mainvillname
                txtroutecode.Value = obj.routecode
                If clsCommon.myLen(txtroutecode.Value) > 0 Then
                    txtroutename.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ROUTE_NO As Code, ROUTE_NAME as Description from TSPL_BULK_ROUTE_MASTER where ROUTE_NO='" + txtroutecode.Value + "'"))
                Else
                    txtroutename.Text = ""
                End If
                txtroutename.Text = obj.routename
                txtLoyaltyPer.Value = obj.Loyalty_Rate
                If obj.TFOwnBMC = True Then
                    chkOwnBMC.Checked = True
                    If clsCommon.myLen(obj.OwnBMCDate) <= 0 Then
                        Dim BMCDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Created_Date from TSPL_VLC_MASTER_HEAD where VSP_Code='" + obj.vspCode + "'"))
                        If clsCommon.myLen(BMCDate) > 0 Then
                            Dim UpdateBMC As String = "Update TSPL_VLC_MASTER_HEAD set OwnBMCDate='" & clsCommon.GetPrintDate(BMCDate) & "' where VSP_Code='" + obj.vspCode + "' "
                            clsDBFuncationality.ExecuteNonQuery(UpdateBMC)
                        End If
                        txtOwnBMCDate.Value = clsCommon.myCDate(BMCDate)
                    Else
                        txtOwnBMCDate.Value = clsCommon.myCDate(obj.OwnBMCDate)
                    End If
                    txtMCCOwnBMC.Value = obj.OwnBMC
                Else
                    chkOwnBMC.Checked = False
                    txtMCCOwnBMC.Value = Nothing
                End If
                fndPriceCode.Value = obj.Price_Code
                Me.chkInActive.Checked = IIf(obj.Active = 0, True, False)
                'fndvlccode.ReadOnly = True
                If clsCommon.myCBool(obj.HeadLoad) = True Then
                    ChkHeadLoad.Checked = True
                    If clsCommon.myCstr(obj.HeadLoadBasis) = "P" Then
                        CmbHeadLoadServiceBasis.Text = "%(Percentage)"
                    ElseIf clsCommon.myCstr(obj.HeadLoadBasis) = "K" Then
                        CmbHeadLoadServiceBasis.Text = "Rate/Kg"
                    ElseIf clsCommon.myCstr(obj.HeadLoadBasis) = "L" Then
                        CmbHeadLoadServiceBasis.Text = "Rate/Ltr"
                    End If
                    txtRateHeadLoad.Text = obj.HeadLoadRate
                Else
                    ChkHeadLoad.Checked = False
                    CmbHeadLoadServiceBasis.Text = ""
                    txtRateHeadLoad.Text = ""
                End If
            Else
                Reset()
            End If

            'isLoadData = False
        Catch ex As Exception
            'isNewEntry = True
            'isLoadData = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    'Sanjay
    Private Function CreateCustomer(ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As New clsCustomerMaster()
            obj.Cust_Code = clsCommon.myCstr(fndvendorNo.Value)
            obj.Customer_Name = clsCommon.myCstr(txtvendorname.Text)
            obj.Alies_Name = clsCommon.myCstr(txtAliesName.Text)
            obj.Add1 = clsCommon.myCstr(txtAdd1.Text)
            obj.Add2 = clsCommon.myCstr(txtAdd2.Text)
            obj.Add3 = clsCommon.myCstr(txtAdd3.Text)
            obj.City_Code = clsCommon.myCstr(fndCity.Value)
            obj.State = clsCommon.myCstr(txtstatecode.Value)
            obj.Country = clsCommon.myCstr(txtcountrycode.Value)
            obj.Phone1 = clsCommon.myCstr(txtPhone1.Text)
            obj.Phone2 = clsCommon.myCstr(txtPhone2.Text)
            obj.Fax = clsCommon.myCstr(txtfax.Text)
            obj.Email = clsCommon.myCstr(txtEmail.Text)
            obj.WebSite = clsCommon.myCstr(txtWeb.Text)
            obj.CURRENCY_CODE = IIf(Me.fndVendorCurrency.Visible = True, clsCommon.myCstr(Me.fndVendorCurrency.Value), objCommonVar.BaseCurrencyCode)
            obj.CUSTOMER_FORM_TYPE = "VSP"
            Dim strCmd1 As String = " SELECT Cust_Group_Code as [Customer Gruop Code],Cust_Group_Desc as [Description]," &
              " Tax_Group as [Tax Group],Cust_Account as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + fndCusgrp.Value + "' "
            Dim myDs As DataSet = connectSql.RunSQLReturnDS(trans, strCmd1)
            If myDs.Tables(0).Rows.Count > 0 Then
                Dim row As DataRow = myDs.Tables(0).Rows(0)
                obj.Cust_Group_Code = clsCommon.myCstr(row(0).ToString().Trim())
                obj.Tax_Group = clsCommon.myCstr(row(2).ToString().Trim())
                obj.Cust_Account = clsCommon.myCstr(row(3).ToString().Trim())
                obj.Terms_Code = clsCommon.myCstr(row(4).ToString().Trim())
            End If

            obj.PIN_NO = clsCommon.myCstr(TxtPinCode.Text)
            obj.OutLet_Commossion = clsCommon.myCdbl(0)
            obj.Balance_ToDate = 0
            obj.Credit_Limit = clsCommon.myCdbl(txtCredit.Text)
            obj.Collectorate = clsCommon.myCstr(txtcollect.Text)
            obj.PAN = clsCommon.myCstr(txtpan.Text)
            obj.Credit_Customer = "N"

            obj.LastInvoice_No = Nothing
            obj.LastInvoice_Date = Nothing
            obj.Inter_Branch = "N"

            obj.IsDistributor = "N"

            obj.prntcustyn = "N"

            obj.CSA_Type = "N"
            obj.ManualCustomer = "N"
            obj.Status = "N"
            obj.Comp_Code = objCommonVar.CurrentCompanyCode

            Dim arrDBName As New List(Of String)
            arrDBName.Add(clsCommon.myCstr(objCommonVar.CurrDatabase))

            If Rchkregistered.Checked = True Then
                obj.GSTNO = clsCommon.myCstr(txtGSTIN_No_final.Text)
                obj.GSTEntity = clsCommon.myCstr(txtEntity.Text)
                obj.GSTBlank = clsCommon.myCstr(txtFixxed.Text)
                obj.GSTDigit = clsCommon.myCstr(MyTextBox2.Text)
                obj.GST_Registered = 1
            End If

            ' obj.SaveData(obj, obj.ArrVisi, isNewEntry, arrDBName, trans)
            obj.SaveData(obj, obj.ArrVisi, isNewEntry, trans)

            'Customer Vendor mapping
            Dim ii As Integer = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_CUSTOMER_VENDOR_MAPPING WHERE cust_code='" + fndvendorNo.Value + "'", trans)
            If ii = 0 Then
                Dim qry As String = "insert into TSPL_CUSTOMER_VENDOR_MAPPING values('" + fndvendorNo.Value + "','" + fndvendorNo.Value + "') "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

        Catch ex As Exception
            Return False
            myMessages.myExceptions(ex)
        End Try
        Return True
    End Function

    Public Function updateMultipleIncentive(ByVal Vendor_Code As String, ByVal trans As SqlTransaction) As Boolean
        Dim strq As String = ""
        Try
            strq = "delete from TSPL_VSP_INCENTIVE where VENDOR_CODE='" & Vendor_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(strq, trans)
            If chkMultIncentive.Checked Then
                Dim arrList As New ArrayList
                arrList = txtIncentiveMult.arrValueMember
                For Each Incentive As String In arrList
                    strq = "insert into TSPL_VSP_INCENTIVE(VENDOR_CODE,INCENTIVE_CODE) Values('" & Vendor_Code & "','" & Incentive & "')"
                    clsDBFuncationality.ExecuteNonQuery(strq, trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Sub LoadIncentive(ByVal Vendor_Code As String, ByVal trans As SqlTransaction)
        Dim strq As String = ""
        Try
            '' get already selected data
            strq = "select Vendor_Code,INCENTIVE_CODE from TSPL_VSP_INCENTIVE where Vendor_Code='" & fndvendorNo.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strq)
            Dim arr As New ArrayList
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr.Item("INCENTIVE_CODE")))
            Next
            txtIncentiveMult.arrValueMember = arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    'This function for updation
    Public Sub funupdate()
        If chkCreateCustomerAlso.Checked = True Then
            If fndCusgrp.Value = "" Then
                myMessages.blankValue(Me, "Please Select Customer Group Code", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                fndCusgrp.Focus()
                fndCusgrp.Select()
                Return
            End If
        End If

        ''Create Mcc Master
        Try
            If chkOwnBMC.Checked = True AndAlso clsCommon.myLen(txtMCCOwnBMC.Value) <= 0 Then
                Dim qry As String = " select count (*)  from TSPL_MCC_MASTER where Mcc_Code_VLC_Uploader = '" + txtVLCCodeVlcUploader.Text + "'  "
                Dim checkValid As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry))
                If checkValid = False Then

                    dtDefault = New DataTable()
                    Dim newBlankRow1 As DataRow = dtDefault.NewRow
                    dtDefault.Rows.Add(newBlankRow1)
                    Dim objDefaultTemplate As clsExportTemplate = clsExportTemplate.GetDefaultData("MP-IMP-TMP")
                    If (objDefaultTemplate IsNot Nothing AndAlso clsCommon.myLen(objDefaultTemplate.Export_Code) > 0) Then
                        If objDefaultTemplate.Arr IsNot Nothing AndAlso objDefaultTemplate.Arr.Count > 0 Then
                            For Each objTr As clsExportTemplateDetail In objDefaultTemplate.Arr
                                If clsCommon.myLen(objTr.Column_Name) > 0 Then
                                    arrExistCols.Add(objTr.Column_Name)
                                    Dim newColumn As New DataColumn(clsCommon.myCstr(objTr.Column_Name), GetType(System.String))
                                    dtDefault.Columns.Add(newColumn)
                                    dtDefault.Rows(0).Item(clsCommon.myCstr(objTr.Column_Name)) = clsCommon.myCstr(objTr.Column_Header)
                                End If
                            Next
                        End If
                    End If

                    If (dtDefault IsNot Nothing AndAlso clsCommon.myLen(dtDefault.Rows.Count) > 0) Then
                        CreateNewMCC(txtVLCCodeVlcUploader.Text)
                    Else
                        Throw New Exception("Please set Default Templete to create MCC Master")
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "VSP/VLC Master")
            Return
        End Try
        'Create Mcc Master

        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndvendorNo.Value, "TSPL_VENDOR_MASTER", "Vendor_Code", trans)

            Dim Registered As Integer = 0
            If objCommonVar.GSTApplicable Then
                If Rchkregistered.Checked = True Then
                    Registered = 1
                Else
                    Registered = 0
                    txtGST_PanCode.Text = ""
                    txtEntity.Text = ""
                    txtGSTStateCode.Text = ""
                    txtGSTIN_No_final.Text = ""
                End If

            End If


            Dim closingdate As String = Nothing
            Dim strStatus As String = ""
            If chkInActive.Checked = True Then
                strStatus = "Y"                    '******* for:In-Active ********
                closingdate = Format(dtClosing.Value, "dd/MM/yyyy")
            ElseIf chkInActive.Checked = False Then
                strStatus = "N"                    '******* for:Active ******** 
                closingdate = connectSql.serverDate(trans)
            End If


            If clsCommon.myLen(fndvendorNo.Value) <= 0 AndAlso strStatus = "N" Then
                lblActiveDate.Text = connectSql.serverDate(trans)
            ElseIf clsCommon.myLen(fndvendorNo.Value) <= 0 AndAlso strStatus = "Y" Then
                lblActiveDate.Text = ""
            ElseIf clsCommon.myLen(fndvendorNo.Value) > 0 AndAlso strStatus = "N" Then

            ElseIf clsCommon.myLen(fndvendorNo.Value) > 0 AndAlso strStatus = "Y" Then
                lblActiveDate.Text = ""
            End If

            Dim strTagAsFranchise As String
            If chkTagAsFranchise.Checked = True Then
                strTagAsFranchise = "Y"  '''''   for taging vendor as franchise
            Else
                strTagAsFranchise = "N" '''''   for untaging vendor as franchise
            End If

            Dim strHold As String = ""
            If chkHold.Checked = True Then
                strHold = "Y"                      '******* for:Hold ******** 
            ElseIf chkHold.Checked = False Then
                strHold = "N"                      '******* for:Remove Hold ********
            End If

            Dim strInterBranch As String = ""
            If chkInterBranch.Checked = True Then
                strInterBranch = "Y"
            Else
                strInterBranch = "N"
            End If

            Dim strtrans As String = ""
            If chktrarns.Checked = True Then
                strtrans = "Y"                      '******* for:Transporter type ******** 
            ElseIf chktrarns.Checked = False Then
                strtrans = "N"
                Dim ii As Integer = clsDBFuncationality.getSingleValue("Select COUNT(*) from tspl_transport_Master WHERE Transport_Id='" + fndvendorNo.Value + "'", trans)
                If ii > 0 Then
                    If common.clsCommon.MyMessageBoxShow("Do you want to delete this Vendor from Transport Master?", "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                        strtrans = "N"                      '******* for:Remove Non Transporter type ********
                        clsDBFuncationality.ExecuteNonQuery("Delete from tspl_transport_Master Where Transport_Id='" + fndvendorNo.Value + "'", trans)
                    Else
                        strtrans = "Y"
                    End If
                End If

            End If



            Dim strTax1 As String = ""
            Dim strTax1_Rate As Decimal = 0.0

            Dim strTax2 As String = ""
            Dim strTax2_Rate As Decimal = 0.0

            Dim strTax3 As String = ""
            Dim strTax3_Rate As Decimal = 0.0

            Dim strTax4 As String = ""
            Dim strTax4_Rate As Decimal = 0.0

            Dim strTax5 As String = ""
            Dim strTax5_Rate As Decimal = 0.0

            Dim strTax6 As String = ""
            Dim strTax6_Rate As Decimal = 0.0

            Dim strTax7 As String = ""
            Dim strTax7_Rate As Decimal = 0.0

            Dim strTax8 As String = ""
            Dim strTax8_Rate As Decimal = 0.0

            Dim strTax9 As String = ""
            Dim strTax9_Rate As Decimal = 0.0

            Dim strTax10 As String = ""
            Dim strTax10_Rate As Decimal = 0.0


            Dim CrLimit As Decimal
            If txtCredit.Text = "" Then
                CrLimit = Convert.ToDecimal("0.00")
            Else
                CrLimit = Convert.ToDecimal(txtCredit.Text)
            End If

            Dim srvc_type As String = "" 'clsCommon.myCstr(cmbservc_type.Text)
            Dim joint_name As String = clsCommon.myCstr(txtjointname.Text.Replace("'", "`")) 'clsCommon.myCstr(fndVendorCOde.Value)

            Dim IsGrossReceipt As Integer = IIf(chkIsGrossReceipt.Checked, 1, 0)

            Dim TermsCode As String = ""
            Dim TermsCodeDesc As String = ""
            Dim AccountSetCode As String = ""
            Dim AccountSetCodeDesc As String = ""
            Dim TaxGroupCode As String = ""
            Dim TaxGroupDesc As String = ""
            Dim dr As DataTable
            dr = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Acct_Set_Desc,Terms_Code,Terms_Desc,Tax_Group_Code,Tax_Group_Desc from Tspl_vendor_group where ven_group_code='" & fndgroupcode.Value & "'", trans)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                TermsCode = dr.Rows(0)("Terms_Code").ToString()
                TermsCodeDesc = dr.Rows(0)("Terms_Desc").ToString()
                AccountSetCode = dr.Rows(0)("Acct_Set_Code").ToString()
                AccountSetCodeDesc = dr.Rows(0)("Acct_Set_Desc").ToString()
                TaxGroupCode = dr.Rows(0)("Tax_Group_Code").ToString()
                TaxGroupDesc = dr.Rows(0)("Tax_Group_Desc").ToString()
            End If


            clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_VENDOR_MASTER_UPDATE", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@Vendor_Name", txtvendorname.Text.ToString()), New SqlParameter("@Vendor_Group_Code", fndgroupcode.Value), New SqlParameter("Vendor_Group_Des", txtgroupdes.Text.ToString()), New SqlParameter("@Status", strStatus), New SqlParameter("@OnHold", strHold), New SqlParameter("@transporter", strtrans), New SqlParameter("@Closing_Date", closingdate), New SqlParameter("@Add1", txtAdd1.Text.ToString()), New SqlParameter("@Add2", txtAdd2.Text.ToString()), New SqlParameter("@Add3", txtAdd3.Text.ToString()), New SqlParameter("@City_Code", fndCity.Value), New SqlParameter("@City_Des", txtCity.Text.ToString()), New SqlParameter("@State", txtState.Text.ToString()), New SqlParameter("@Country", txtCountry.Text.ToString()), New SqlParameter("@Phone1", txtPhone1.Text.ToString()), New SqlParameter("@Phone2", txtPhone2.Text.ToString()), New SqlParameter("@Fax", txtfax.Text.ToString()), New SqlParameter("@Email", txtEmail.Text.ToString()), New SqlParameter("@WebSite", txtWeb.Text.ToString()), New SqlParameter("@Contact_Person_Name", ""), New SqlParameter("@Contact_Person_Phone", ""), New SqlParameter("@Contact_Person_Fax", ""), New SqlParameter("@Contact_Person_Website", ""), New SqlParameter("@Contact_Person_Email", ""), New SqlParameter("@Terms_Code", TermsCode), New SqlParameter("@Terms_Code_Des", TermsCodeDesc), New SqlParameter("@Vendor_Account", AccountSetCode), New SqlParameter("@Vendor_Account_Set_Des", AccountSetCodeDesc), New SqlParameter("@Payment_Code", ""), New SqlParameter("@Payment_Code_Des", ""), New SqlParameter("@Vendor_Type_Code", fndvendortype.Value), New SqlParameter("@Vendor_Type_Des", txtvendortypedes.Text.ToString()), New SqlParameter("@Bank_Code", IIf(EnableBankFromMaster = True, findfndbankcode.Value, fndbankcode.Text)), New SqlParameter("@Bank_Code_Des", txtbankcodedes.Text.ToString()), New SqlParameter("@Service_Tax_No", ""), New SqlParameter("@Lst_No", ""), New SqlParameter("@Tin_No", ""), New SqlParameter("@Credit_Limit", CrLimit), New SqlParameter("@Tax_Group", TaxGroupCode), New SqlParameter("@Tax_Group_Des", TaxGroupDesc), New SqlParameter("@TAX1", strTax1), New SqlParameter("@TAX1_Rate", strTax1_Rate), New SqlParameter("@TAX2", strTax2), New SqlParameter("@TAX2_Rate", strTax2_Rate), New SqlParameter("@TAX3", strTax3), New SqlParameter("@TAX3_Rate", strTax3_Rate), New SqlParameter("@TAX4", strTax4), New SqlParameter("@TAX4_Rate", strTax4_Rate), New SqlParameter("@TAX5", strTax5), New SqlParameter("@TAX5_Rate", strTax5_Rate), New SqlParameter("@TAX6", strTax6), New SqlParameter("@TAX6_Rate", strTax6_Rate), New SqlParameter("@TAX7", strTax7), New SqlParameter("@TAX7_Rate", strTax7_Rate), New SqlParameter("@TAX8", strTax8), New SqlParameter("@TAX8_Rate", strTax8_Rate), New SqlParameter("@TAX9", strTax9), New SqlParameter("@TAX9_Rate", strTax9_Rate), New SqlParameter("@TAX10", strTax10), New SqlParameter("@TAX10_Rate", strTax10_Rate), New SqlParameter("@Remarks1", ""), New SqlParameter("@Remarks2", ""), New SqlParameter("@Additional1", ""), New SqlParameter("@Additional2", ""), New SqlParameter("@Additional3", ""), New SqlParameter("@cst", ""), New SqlParameter("@ecc", ""), New SqlParameter("@range", ""), New SqlParameter("@collectorate", txtcollect.Text.ToString()), New SqlParameter("@pan", txtpan.Text.ToString()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@is_Gross_Receipt", IsGrossReceipt), New SqlParameter("@InterBranch ", strInterBranch), New SqlParameter("@Branch_Name ", TxtBankBranch.Text.ToString()), New SqlParameter("@Account_No ", TxtAccNo.Text.ToString()), New SqlParameter("@Bank_Name ", TxtBankName.Text.ToString()), New SqlParameter("@IFSC_Code ", IIf(EnableBankFromMaster = True, findTxtIFSCCode.Value, TxtIFSCCode.Text.ToString())), New SqlParameter("@Account_Type ", clsCommon.myCstr(cmbAccountType.SelectedValue)))

            clsDBFuncationality.ExecuteNonQuery(GetUpdateQry(strTagAsFranchise, joint_name), trans) ', srvc_type

            'done by stuti against purchase points on 20/10/2016
            Dim strCmd11 As String = "Update TSPL_VENDOR_MASTER set Is_Inactive_In_Milk_Procurement='" + IIf(chkInactiveInMilkModule.Checked, "1", "0") + "',is_Hold_Payment_Process='" + IIf(chkHoldPaymentProcess.Checked, "1", "0") + "',Is_Blacklist='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chk_isblacklist.Checked) = True, 1, 0)) + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            connectSql.RunSqlTransaction(trans, strCmd11)


            If objCommonVar.GSTApplicable = True Then
                Dim streq1 As String = "Update TSPL_VENDOR_MASTER set GSTRegistered='" & Registered & "',GSTEntity='" & txtEntity.Text & "',GSTLastEntity='" & MyTextBox2.Text & "',GSTFinalNo='" & txtGSTIN_No_final.Text & "',GSTMiddle='" & txtFixxed.Text & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(streq1, trans)
            End If

            ''For Custom Fields
            Dim arrCustomFields As List(Of clsCustomFieldValues) = New List(Of clsCustomFieldValues)


            clsCustomFieldValues.SaveData(MyBase.Form_ID, fndvendorNo.Value, arrCustomFields, trans)
            ''End of For Custom Fields


            '' multicurrency
            Dim strq As String
            If Me.fndVendorCurrency.Visible = True Then
                strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE='" & clsCommon.myCstr(Me.fndVendorCurrency.Value) & "',Form_Type='" + txtvndrtype.Text + "',state_code='" + txtstatecode.Value + "',country_code='" + txtcountrycode.Value + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            Else
                strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE='" & objCommonVar.BaseCurrencyCode & "',Form_Type='" + txtvndrtype.Text + "',state_code='" + txtstatecode.Value + "',country_code='" + txtcountrycode.Value + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            End If
            clsDBFuncationality.ExecuteNonQuery(strq, trans)

            If clsCommon.myCdbl(Me.TxtSecurityCharges.Text) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set Security_Amount='" & clsCommon.myCdbl(Me.TxtSecurityCharges.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If


            If clsCommon.myLen(Me.TxtAmCU.Text) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set AMCU='" & clsCommon.myCstr(Me.TxtAmCU.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myCdbl(Me.TxtAmc_Charge.Text) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set AMC_Charge ='" & clsCommon.myCstr(Me.TxtAmc_Charge.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If

            If clsCommon.myLen(Me.fndpaymentCycle.Value) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set PC_CODE ='" & clsCommon.myCstr(Me.fndpaymentCycle.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If

            Dim streq As String = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of= '" + txtvendorname.Text + "'+'-' + '" + TxtBankName.Text + "' +'-' + '" + TxtAccNo.Text + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(streq, trans)


            Dim CorrectionFatSNF As String = "update TSPL_VENDOR_MASTER set TIP_Buffalo='" & clsCommon.myCstr(txtTIPBuffalo.Value) & "',TIP_Cow='" & clsCommon.myCdbl(txtTIPCow.Value) & "',TIP_Mix='" & clsCommon.myCdbl(txtTIPMix.Value) & "', CorrectionFat='" & clsCommon.myCdbl(numCorrectionFat.Text) & "' , CorrectionSNF='" & clsCommon.myCdbl(numCorrectionSNF.Text) & "',Credit_Limit_On_Milk_Receipt_Per='" + clsCommon.myCstr(IIf(chkCreditLimitBasedOnMilkReceipt.Checked, TxtCreditLimitBasedOnMilkReceipt.Value, -1)) + "' where Vendor_Code='" & fndvendorNo.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(CorrectionFatSNF, trans)

            Dim VendorNameHindi As String = " update TSPL_VENDOR_MASTER set Vendor_name_Hindi = N'" + txtvendornameHindi.Text + "' where Vendor_Code='" + fndvendorNo.Value + "' "
            clsDBFuncationality.ExecuteNonQuery(VendorNameHindi, trans)


            '' end multi currency
            '' update multiple incentive
            updateMultipleIncentive(fndvendorNo.Value, trans)


            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_customer_master where CUSTOMER_FORM_TYPE='VSP' and cust_code='" + fndvendorNo.Value + "'", trans)) > 0 Then
                If CreateCustomer(False, trans) = False Then
                    Exit Sub
                End If
            Else
                If chkCreateCustomerAlso.Checked = True Then
                    If CreateCustomer(True, trans) = False Then
                        Exit Sub
                    End If
                End If
            End If

            If clsCommon.myLen(cmbGender.Text) > 0 Then
                connectSql.RunSqlTransaction(trans, "Update TSPL_VENDOR_MASTER set Gender = '" + cmbGender.Text + "'  where Vendor_Code='" + fndvendorNo.Value + "' ")
            End If
            If clsCommon.myLen(lblActiveDate.Text) > 0 Then
                connectSql.RunSqlTransaction(trans, "Update TSPL_VENDOR_MASTER set Active_Date = '" + clsCommon.GetPrintDate(lblActiveDate.Text, "dd/MMM/yyyy") + "'  where Vendor_Code='" + fndvendorNo.Value + "' ")
            Else
                connectSql.RunSqlTransaction(trans, "Update TSPL_VENDOR_MASTER set Active_Date = Null  where Vendor_Code='" + fndvendorNo.Value + "' ")
            End If

            'Dim qryBlockZone As String = " Update TSPL_VENDOR_MASTER set DISTRICT_Code = '" & clsCommon.myCstr(txtDistrict.Value) & "' , Zone_Code ='" & clsCommon.myCstr(txtZone.Value) & "' , CAST_CATEGORY_CODE ='" & clsCommon.myCstr(txtCastCategory.Value) & "' , BLOCK_CODE ='" & clsCommon.myCstr(txtBlockCode.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            'clsDBFuncationality.ExecuteNonQuery(qryBlockZone, trans)

            If clsCommon.myLen(txtDistrict.Value) > 0 Then
                Dim qryDistrict As String = " Update TSPL_VENDOR_MASTER set DISTRICT_Code = '" & clsCommon.myCstr(txtDistrict.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryDistrict, trans)
            Else
                Dim qryDistrict As String = " Update TSPL_VENDOR_MASTER set DISTRICT_Code = null where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryDistrict, trans)
            End If
            If clsCommon.myLen(txtZone.Value) > 0 Then
                Dim qryZone As String = " Update TSPL_VENDOR_MASTER set Zone_Code ='" & clsCommon.myCstr(txtZone.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryZone, trans)
            Else
                Dim qryZone As String = " Update TSPL_VENDOR_MASTER set Zone_Code =null where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryZone, trans)
            End If

            If clsCommon.myLen(txtCastCategory.Value) > 0 Then
                Dim qryCastCategory As String = " Update TSPL_VENDOR_MASTER set CAST_CATEGORY_CODE ='" & clsCommon.myCstr(txtCastCategory.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryCastCategory, trans)
            Else
                Dim qryCastCategory As String = " Update TSPL_VENDOR_MASTER set CAST_CATEGORY_CODE =null where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryCastCategory, trans)
            End If

            If clsCommon.myLen(txtBlockCode.Value) > 0 Then
                Dim qryBlockCode As String = " Update TSPL_VENDOR_MASTER set BLOCK_CODE ='" & clsCommon.myCstr(txtBlockCode.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryBlockCode, trans)
            Else
                Dim qryBlockCode As String = " Update TSPL_VENDOR_MASTER set BLOCK_CODE =null where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryBlockCode, trans)
            End If

            If clsCommon.myLen(txtRevenueVillage.Value) > 0 Then
                Dim qryRevenueVillage As String = " Update TSPL_VENDOR_MASTER set REVENUE_VILLAGE_CODE ='" & clsCommon.myCstr(txtRevenueVillage.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryRevenueVillage, trans)
            Else
                Dim qryRevenueVillage As String = " Update TSPL_VENDOR_MASTER set REVENUE_VILLAGE_CODE =null where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryRevenueVillage, trans)
            End If

            If clsCommon.myLen(txtGrampanchayat.Value) > 0 Then
                Dim qryGrampanchayat As String = " Update TSPL_VENDOR_MASTER set GRAMPANCHAYAT_CODE ='" & clsCommon.myCstr(txtGrampanchayat.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryGrampanchayat, trans)
            Else
                Dim qryGrampanchayat As String = " Update TSPL_VENDOR_MASTER set GRAMPANCHAYAT_CODE =null where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryGrampanchayat, trans)
            End If

            If clsCommon.myLen(txtPanchayatSamiti.Value) > 0 Then
                Dim qryPanchayatSamiti As String = " Update TSPL_VENDOR_MASTER set PANCHAYAT_SAMITI_CODE ='" & clsCommon.myCstr(txtPanchayatSamiti.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryPanchayatSamiti, trans)
            Else
                Dim qryPanchayatSamiti As String = " Update TSPL_VENDOR_MASTER set PANCHAYAT_SAMITI_CODE =null where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryPanchayatSamiti, trans)
            End If

            If clsCommon.myLen(txtVidhanSabha.Value) > 0 Then
                Dim qryVidhanSabha As String = " Update TSPL_VENDOR_MASTER set VIDHAN_SABHA_CODE ='" & clsCommon.myCstr(txtVidhanSabha.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryVidhanSabha, trans)
            Else
                Dim qryVidhanSabha As String = " Update TSPL_VENDOR_MASTER set VIDHAN_SABHA_CODE =null where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qryVidhanSabha, trans)
            End If

            Dim HeadLoadBasis As String = Nothing
            If ChkHeadLoad.Checked Then
                If CmbHeadLoadServiceBasis.SelectedIndex = 1 Then
                    HeadLoadBasis = "P"
                ElseIf CmbHeadLoadServiceBasis.SelectedIndex = 2 Then
                    HeadLoadBasis = "K"
                ElseIf CmbHeadLoadServiceBasis.SelectedIndex = 3 Then
                    HeadLoadBasis = "L"
                End If
            End If
            'Dim qryHeadLoad As String = " Update TSPL_VENDOR_MASTER set Is_Head_Load ='" & IIf(ChkHeadLoad.Checked, "T", "F") & "',Rate_Head_Load='" & clsCommon.myCDecimal(txtRateHeadLoad.Text) & "',Service_Basis_Head_Load='" & clsCommon.myCstr(HeadLoadBasis) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            'clsDBFuncationality.ExecuteNonQuery(qryHeadLoad, trans)
            'VLC 
            If clsCommon.myLen(fndvlccode.Text) > 0 Then
                VLCSaveData(False, trans)
            ElseIf clsCommon.myLen(txtvlcname.Text) > 0 Then
                VLCSaveData(True, trans)
            End If

            UpdateFeild(fndvendorNo.Value, fndvlccode.Text, trans)

            trans.Commit()

            Dim obj As New clsfrmVLCMaster()
            If clsCommon.myLen(obj.vlcCode) > 0 Then
                VLCLoadData(obj.vlcCode, NavigatorType.Current)
            End If
            'If clsCommon.myLen(fndvlccode.Text) > 0 Then
            '    VLCLoadData(fndvlccode.Text, NavigatorType.Current)
            'End If

            UcAttachment1.SaveData(fndvendorNo.Value)
            myMessages.update()
        Catch ex As Exception
            trans.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
    End Sub


    Private Function CreateNewMCC(ByVal BMCMCCCode As String) As Boolean
        Try
            Dim obj As New clsMccMaster()
            Dim dtDefaultUOM As New DataTable
            Dim qry As String = ""
            If objCommonVar.ApplyDefaultsInMaster = True Then
                qry = "select * from TSPL_UNIT_MASTER  WHERE IsDefault=1"
                dtDefaultUOM = clsDBFuncationality.GetDataTable(qry)
            End If
            Dim strdate As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
            If arrExistCols.Contains(clsMasterDefault.colMCCState) Then
                obj.State_Code = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCState))
            End If
            If clsCommon.myLen(obj.State_Code) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                obj.State_Code = clsStateMaster.GetDefault()
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCBMCC) Then
                obj.Is_MCC = IIf(clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCBMCC)) = 0, 0, 1)
                If obj.Is_MCC = 1 Then
                    obj.MCC_Code = clsERPFuncationality.GetNextCode(Nothing, strdate, clsDocType.MCCMaster, clsDocTransactionType.MCC, obj.State_Code, False, True, True)
                Else
                    obj.MCC_Code = clsERPFuncationality.GetNextCode(Nothing, strdate, clsDocType.MCCMaster, clsDocTransactionType.BMCU, obj.State_Code, False, True, True)
                End If
            Else
                obj.MCC_Code = clsERPFuncationality.GetNextCode(Nothing, strdate, clsDocType.MCCMaster, clsDocTransactionType.MCC, obj.State_Code, False, True, True)
            End If

            If clsCommon.myLen(obj.MCC_Code) <= 0 Then
                Throw New Exception("Error In BMC Code Genertion for Uploader code- " + BMCMCCCode)
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCType) Then
                obj.MCC_Type = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCType))
            End If
            If clsCommon.myLen(obj.MCC_Type) <= 0 Then
                obj.MCC_Type = "Co. Owned"
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCChillingVendorCode) Then
                obj.Chilling_Vendor = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingVendorCode))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCName) Then
                obj.MCC_NAME = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCName))
            End If

            If clsCommon.myLen(obj.MCC_NAME) <= 0 Then
                obj.MCC_NAME = txtvendorname.Text 'BMCMCCCode
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCAddress1) Then
                obj.Add1 = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAddress1))
            End If


            If arrExistCols.Contains(clsMasterDefault.colMCCAddress2) Then
                obj.Add2 = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAddress2))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCTehsil) Then
                obj.Tehsil = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCTehsil))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCPinCode) Then
                obj.Pin_code = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCPinCode))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCCity) Then
                obj.City_code = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCity))
            End If
            If clsCommon.myLen(obj.City_code) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                obj.City_code = clsCityMaster.GetDefault()
            End If
            'obj.Country_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colMCCCountry).Value)

            If arrExistCols.Contains(clsMasterDefault.colMCCCountry) Then
                obj.Country_code = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCountry))
            End If
            If clsCommon.myLen(obj.Country_code) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                obj.Country_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 COUNTRY_CODE from TSPL_COUNTRY_MASTER"))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCTelphone) Then
                obj.Telphone = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCTelphone))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCEmail) Then
                obj.Email = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCEmail))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCFax) Then
                obj.Fax = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCFax))
            End If


            If arrExistCols.Contains(clsMasterDefault.colMccSuperArea) Then
                obj.MCC_Area = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMccSuperArea))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfStore) Then
                obj.Area_Of_Store = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaOfStore))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfOffice) Then
                obj.Area_Of_Office = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaOfOffice))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCOpenAreaForTanker) Then
                obj.Open_Area_For_tanker = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCOpenAreaForTanker))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfLab) Then
                obj.Area_Of_LAB = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaOfLab))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCTotalStorageCapacity) Then
                obj.Total_Storage_capacity = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCTotalStorageCapacity))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfReceivingDock) Then
                obj.Area_Of_Receiving_DOCK = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaOfReceivingDock))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCFssaiNo) Then
                obj.FSSAI_NO = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCFssaiNo))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCDripSaver) Then
                obj.DripSaver = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCDripSaver))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCCanWasher) Then
                obj.CanWasher = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCanWasher))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCCanScrubber) Then
                obj.CanScrubber = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCanScrubber))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCETP) Then
                obj.ETP = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCETP))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCEarthing) Then
                obj.Earthing = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCEarthing))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCBoiler) Then
                obj.Boiler = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCBoiler))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCApplyFailedSample) Then
                obj.Failed_Sample_Apply = (clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCApplyFailedSample)), "Y") = CompairStringResult.Equal)
                If obj.Failed_Sample_Apply Then
                    If arrExistCols.Contains(clsMasterDefault.colMCCFailedSampleFAT) Then
                        obj.Failed_Sample_FAT = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCFailedSampleFAT))
                        If obj.Failed_Sample_FAT <= 0 Then
                            Throw New Exception("Please provide Failed Sample FAT %")
                        End If
                    End If
                    If arrExistCols.Contains(clsMasterDefault.colMCCFailedSampleSNF) Then
                        obj.Failed_Sample_SNF = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCFailedSampleSNF))
                        If obj.Failed_Sample_SNF <= 0 Then
                            Throw New Exception("Please provide Failed Sample SNF %")
                        End If
                    End If
                End If
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCAgreement_Status) Then
                obj.agreemnt = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAgreement_Status))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCAgreement_Date) AndAlso clsCommon.myLen(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAgreement_Date)) > 0 Then
                obj.agrmnt_date = clsCommon.myCDate(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAgreement_Date))
            Else
                obj.agrmnt_date = clsCommon.GETSERVERDATE()
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCAgreementExpiryDate) AndAlso clsCommon.myLen(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAgreementExpiryDate)) > 0 Then
                obj.expired_date = clsCommon.myCDate(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAgreementExpiryDate))
            Else
                obj.expired_date = clsCommon.GETSERVERDATE()
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCSecurity_Status) Then
                obj.secutiy = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCSecurity_Status))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCCheque_Amt) Then
                obj.chq_amt = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCheque_Amt))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCCheque_No) Then
                obj.chq_no = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCheque_No))
            End If
            'If clsCommon.myLen(obj.chq_no) > 0 Then
            '    If arrExistCols.Contains(colMCCCheque_Date) Then

            '        If clsCommon.myLen(dtDefault.Rows(0).Item(colMCCCheque_Date)) > 0 Then
            '            obj.chq_date = clsCommon.myCDate(dtDefault.Rows(0).Item(colMCCCheque_Date))
            '        Else
            '            obj.chq_date = clsCommon.GETSERVERDATE()
            '        End If

            '    End If
            'End If

            If arrExistCols.Contains(clsMasterDefault.colMCCCheque_Date) AndAlso clsCommon.myLen(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCheque_Date)) > 0 Then
                obj.chq_date = clsCommon.myCDate(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCheque_Date))
            Else
                obj.chq_date = clsCommon.GETSERVERDATE()
            End If

            If clsCommon.CompairString(obj.secutiy, "YES") = CompairStringResult.Equal AndAlso (clsCommon.myCdbl(obj.chq_amt) <= 0 Or clsCommon.myLen(obj.chq_no) <= 0) Then
                Throw New Exception("Please Fill Cheque Amount And Cheque No./Date")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCIndustryType) Then
                obj.industry_type = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCIndustryType))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCMonthlyProvision) Then
                If clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCMonthlyProvision)), "Y") = CompairStringResult.Equal Then
                    obj.is_Chilling_Provision_Monthly = True
                Else
                    obj.is_Chilling_Provision_Monthly = False
                End If
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingCharges) Then
                obj.chilling_rate = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingCharges))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingOnQty) Then
                obj.chilling_qty = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingOnQty))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingOn) Then
                obj.chilling_kg_ltr = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingOn))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingMinGuaranteedAvgQty) Then
                obj.chilling_assur_qty = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingMinGuaranteedAvgQty))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingMinGuaranteedPeriod) Then
                obj.chilling_assur_period = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingMinGuaranteedPeriod))
            End If


            If arrExistCols.Contains(clsMasterDefault.colMCCChillingStartingDate) Then
                If clsCommon.myLen(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingStartingDate)) > 0 Then
                    obj.Chilling_Period_Starting_Date = clsCommon.GetPrintDate(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingStartingDate), "dd-MMM-yyyy")
                End If
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCRateofLeaseCharges) Then
                obj.lease_rate = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCRateofLeaseCharges))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingOnUOMHandledDispatched) Then
                obj.Unit_ChillingOnQty = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingOnUOMHandledDispatched)) = "Handled", "H", "D")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingOnUOMKGLTR) Then
                obj.Unit_ChillingOn = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingOnUOMKGLTR)) = "KG", "K", "L")
            End If


            If arrExistCols.Contains(clsMasterDefault.colMCCChillingMinGuaranteedPeriodUOM) Then
                obj.Unit_ChillingMinGuaranteePeriod = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingMinGuaranteedPeriodUOM)) = "Day", "D", IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingMinGuaranteedPeriodUOM)) = "Month", "M", "Y"))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCRateofLeasedChargesUOM) Then
                obj.Unit_RateOfLeasedCharges = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCRateofLeasedChargesUOM)) = "Day", "D", IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCRateofLeasedChargesUOM)) = "Month", "M", "Y"))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaofStoreUOM) Then
                obj.Unit_AreaOfStore = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaofStoreUOM)) = "Sq. Mt.", "M", "F")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfReceivingDockUOM) Then
                obj.Unit_AreaOfReceivingDock = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaOfReceivingDockUOM)) = "Sq. Mt.", "M", "F")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfOfficeUOM) Then
                obj.Unit_AreaOfOffice = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaOfOfficeUOM)) = "Sq. Mt.", "M", "F")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfLabUOM) Then
                obj.Unit_AreaOfLab = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaOfLabUOM)) = "Sq. Mt.", "M", "F")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCOpenAreaForTankerUOM) Then
                obj.Unit_OpenAreaForTankerMovement = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCOpenAreaForTankerUOM)) = "Sq. Mt.", "M", "F")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMccSuperAreaUOM) Then
                obj.Unit_MccSuperArea = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMccSuperAreaUOM)) = "Sq. Mt.", "M", "F")
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCWeighingMachine) Then
                obj.Weighing_Machine = IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCWeighingMachine)), "Prompt") = CompairStringResult.Equal, "P", IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCWeighingMachine)), "Delta") = CompairStringResult.Equal, "D", IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCWeighingMachine)), "Panasonic") = CompairStringResult.Equal, "B", IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCWeighingMachine)), "Supertech") = CompairStringResult.Equal, "S", "C"))))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCSampleMachine) Then
                obj.Sample_Machine = IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCSampleMachine)), "Kanha") = CompairStringResult.Equal, "K", IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCSampleMachine)), "Everest New") = CompairStringResult.Equal, "N", "E"))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCWeighingComPort) Then
                obj.Weighing_Comport = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCWeighingComPort))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCSampleComPort) Then
                obj.Sample_comport = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCSampleComPort))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCPaymentCycle) Then
                If clsCommon.myLen(obj.Payment_Cycle) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                    obj.Payment_Cycle = clsPaymentCycleMaster.GetDefault()
                Else
                    obj.Payment_Cycle = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCPaymentCycle))
                End If
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCShiftMorningOpeningTime) Then
                obj.Shift_Opening_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCShiftMorningOpeningTime))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCShiftMorningClosingTime) Then
                obj.Shift_Closing_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCShiftMorningClosingTime))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCShiftEveningOpeningTime) Then
                obj.Shift_Eve_Opening_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCShiftEveningOpeningTime))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCShiftEveningClosingTime) Then
                obj.Shift_Eve_Closing_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCShiftEveningClosingTime))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCRequiredGateEntry) Then
                obj.is_Reuired_Gate_Entry = IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCRequiredGateEntry)), "Yes") = CompairStringResult.Equal, True, False)
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCRM) Then
                obj.EMP_CODE = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCRM))
            End If
            obj.MCC_Code_VLC_Uploader = BMCMCCCode
            ''obj.Loc_Segment_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colMCCLocSegmentCode).Value)
            If clsCommon.myLen(obj.Loc_Segment_Code) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                obj.Loc_Segment_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Segment_code from TSPL_GL_SEGMENT_CODE where seg_no=7 and len(Segment_code)>0 "))
            End If

            If True Then
                Dim isValidSegmentcode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select Count (*) from TSPL_GL_SEGMENT_CODE where Seg_No='7' and Segment_code = '" + obj.Loc_Segment_Code + "'"))
                If isValidSegmentcode = False AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                    'Create Segment code
                    Dim coll As New Hashtable()
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Seg_No", "7")
                    clsCommon.AddColumnsForChange(coll, "Segment_Code", obj.Loc_Segment_Code)
                    clsCommon.AddColumnsForChange(coll, "Segment_Name", "Location")
                    clsCommon.AddColumnsForChange(coll, "Description", obj.MCC_NAME)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "GIT", "N")
                    clsCommon.AddColumnsForChange(coll, "STATE_CODE", obj.State_Code)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_SEGMENT_CODE", OMInsertOrUpdate.Insert, "")

                ElseIf isValidSegmentcode = False Then
                    Throw New Exception("Invalid Loc Segment Code.")
                End If
            End If

            'Create GL Security
            qry = "select count(User_Code) from TSPL_GL_SEGMENT_PERMISSION where User_Code='" + objCommonVar.CurrentUserCode + "' and GL_Segment='7' and Segment_code = '" + obj.Loc_Segment_Code + "'"
            Dim check1 As Integer = CInt(clsDBFuncationality.getSingleValue(qry))
            If check1 <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                Dim coll As New Hashtable()
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "User_Code", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "GL_Segment", "7")
                clsCommon.AddColumnsForChange(coll, "Segment_Code", obj.Loc_Segment_Code)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Default_Segment", "N")
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_SEGMENT_PERMISSION", OMInsertOrUpdate.Insert, "")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCBMCC) Then
                obj.Is_MCC = IIf(clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCBMCC)) = 0, 0, 1)
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCIsTruckSheetMandatory) Then
                obj.Is_Truck_Sheet = IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCIsTruckSheetMandatory)), "Yes") = CompairStringResult.Equal, 1, 0)
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAllowAutoMilkIn) Then
                obj.AllowAutoMilkIn = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAllowAutoMilkIn))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAutoIn_Location) Then
                obj.AutoIn_Location = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAutoIn_Location))
                qry = "select 1  from TSPL_LOCATION_MASTER where Location_Code='" + obj.AutoIn_Location + "' and Location_Category='MCC'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    If arrExistCols.Contains(clsMasterDefault.colMCCSILOIn_Location) Then
                        obj.SILOIn_Location = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCSILOIn_Location))
                    End If
                Else
                    obj.SILOIn_Location = ""
                End If

                If clsCommon.myCdbl(obj.AllowAutoMilkIn) = 1 Then
                    If clsCommon.myLen(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAutoIn_Location)) <= 0 Then
                        Throw New Exception("Allow auto Milk is true So Auto In Location cannot be blank")
                    End If
                    If arrExistCols.Contains(clsMasterDefault.colMCCSILOIn_Location) Then
                        If clsCommon.myLen(dtDefault.Rows(0).Item(clsMasterDefault.colMCCSILOIn_Location)) <= 0 Then
                            Throw New Exception("Allow auto Milk is true So Silo In Location cannot be blank")
                        End If
                    End If
                End If
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCApplyReceiptWeightTolerance) Then
                obj.Receipt_Weight_tolerance_Apply = IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCApplyReceiptWeightTolerance)), "Y") = CompairStringResult.Equal, True, False)
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCReceiptWeightToleranceValue) Then
                obj.Receipt_Weight_tolerance_Value = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCReceiptWeightToleranceValue))
            End If

            If obj.Receipt_Weight_tolerance_Apply Then
                If obj.Receipt_Weight_tolerance_Value < 0 Then
                    Throw New Exception("Value of ReceiptWeightToleranceValue can't be -ve")
                End If
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCCommissionRate) Then
                obj.Commission_Rate = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCommissionRate))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCCommissionMinimumShiftInPaymentCycle) Then
                obj.Commission_Minimum_Shift_In_Payment_Cycle = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCommissionMinimumShiftInPaymentCycle))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCCommissionMinimumQtyInShift) Then
                obj.Commission_Minimum_Qty_In_Shift = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCommissionMinimumQtyInShift))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCCommissionNoOfPaymentCycleForNewVSP) Then
                obj.Commission_No_Of_Payment_Cycle_For_New_VSP = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCommissionNoOfPaymentCycleForNewVSP))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCDeductionMinimumFATPer) Then
                obj.Deduction_Minimum_FAT_Per = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCDeductionMinimumFATPer))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCDeductionMinimumSNFPer) Then
                obj.Deduction_Minimum_SNF_Per = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCDeductionMinimumSNFPer))

            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCDeductionNoOfPaymentCycleForNewVSP) Then
                obj.Deduction_No_Of_Payment_Cycle_For_New_VSP = clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colMCCDeductionNoOfPaymentCycleForNewVSP))
            End If

            If arrExistCols.Contains(clsMasterDefault.colMCCPlant) Then
                obj.Plant_Code = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCPlant))
                If clsCommon.myLen(obj.Plant_Code) <= 0 Then
                    Throw New Exception("Please define Main Plant in location master")
                End If
                obj.Plant_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_code from tspl_location_master where location_code='" + obj.Plant_Code + "'"))
                If clsCommon.myLen(obj.Plant_Code) <= 0 Then
                    Throw New Exception("Invalid location [" + clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCPlant)) + "]")
                End If
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCMorningShiftOpeningTime) Then
                obj.Shift_Opening_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCMorningShiftOpeningTime))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCMorningShiftClosingTime) Then
                obj.Shift_Closing_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCMorningShiftClosingTime))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCEveningShiftOpeningTime) Then
                obj.Shift_Eve_Opening_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCEveningShiftOpeningTime))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCEveningShiftClosingTime) Then
                obj.Shift_Eve_Closing_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCEveningShiftClosingTime))
            End If

            If True Then
                Dim objgn As clsGenSetDetail
                obj.arrGenSetDetail = New List(Of clsGenSetDetail)
                For j As Integer = 0 To obj.NoOfDG
                    objgn = New clsGenSetDetail()
                    objgn.Prog_Code = clsUserMgtCode.frmMCCMaster
                    objgn.Trans_Code = obj.MCC_Code
                    objgn.Line_No = (j + 1)
                    objgn.Gen_Set_Desc = "N/A"
                    obj.arrGenSetDetail.Add(objgn)
                Next
                Dim objcomp As clsCompressorDetail
                obj.arrCompressorDetail = New List(Of clsCompressorDetail)
                For j As Integer = 0 To obj.NoOfCompressor
                    objcomp = New clsCompressorDetail
                    objcomp.Prog_Code = clsUserMgtCode.frmMCCMaster
                    objcomp.Trans_Code = obj.MCC_Code
                    objcomp.Line_No = (j + 1)
                    objcomp.Compressor_Desc = "N/A"
                    obj.arrCompressorDetail.Add(objcomp)
                Next
                Dim objSilo As clsSiloDetail
                obj.arrSiloDetail = New List(Of clsSiloDetail)
                For j As Integer = 0 To obj.No_Of_SILO
                    objSilo = New clsSiloDetail()
                    objSilo.Prog_Code = clsUserMgtCode.frmMCCMaster
                    objSilo.Trans_Code = obj.MCC_Code
                    objSilo.Line_No = (j + 1)
                    objSilo.Silo_Desc = "N/A"
                    objSilo.Silo_Area = 0
                    objSilo.Silo_Unit = ""
                    obj.arrSiloDetail.Add(objSilo)
                Next
                Dim objmilkpump As clsMilkPumpDetail
                obj.arrMilkPumpDetail = New List(Of clsMilkPumpDetail)
                For j As Integer = 0 To obj.No_Of_MilkPump
                    objmilkpump = New clsMilkPumpDetail()
                    objmilkpump.Prog_Code = clsUserMgtCode.frmMCCMaster
                    objmilkpump.Trans_Code = obj.MCC_Code
                    objmilkpump.Line_No = (j + 1)
                    objmilkpump.Pump_Desc = "N/A"
                    objmilkpump.Pump_Area = 0
                    objmilkpump.Pump_Unit = ""
                    obj.arrMilkPumpDetail.Add(objmilkpump)
                Next

                Dim objChiller As clsChillerDetail
                obj.arrChillerDetail = New List(Of clsChillerDetail)
                For j As Integer = 0 To obj.No_Of_Chiller
                    objChiller = New clsChillerDetail()
                    ' objChiller = New clsMilkPumpDetail()
                    objChiller.Prog_Code = clsUserMgtCode.frmMCCMaster
                    objChiller.Trans_Code = obj.MCC_Code
                    objChiller.Chiller_Desc = "N/A"
                    objChiller.Chiller_Brand = ""
                    objChiller.Chiller_Capacity = 0
                    obj.arrChillerDetail.Add(objChiller)
                Next

                Dim objMccUOM As clsMccUOMDetails
                obj.ArrUomDetails = New List(Of clsMccUOMDetails)




                If objCommonVar.ApplyDefaultsInMaster = True Then
                    If dtDefaultUOM IsNot Nothing AndAlso dtDefaultUOM.Rows.Count > 0 Then
                        objMccUOM = New clsMccUOMDetails()
                        objMccUOM.Mcc_Code = obj.MCC_Code
                        objMccUOM.UOM_Code = clsCommon.myCstr(dtDefaultUOM.Rows(0)("Unit_code"))
                        objMccUOM.UOM_Description = clsCommon.myCstr(dtDefaultUOM.Rows(0)("Unit_Desc"))
                        objMccUOM.Stocking_Unit = "Y"
                        objMccUOM.Conversion_Factor = clsCommon.myCdbl(dtDefaultUOM.Rows(0)("Conv_Factor"))
                        obj.ArrUomDetails.Add(objMccUOM)
                    End If
                ElseIf arrExistCols.Contains(clsMasterDefault.colMCCChillingOnUOMKGLTR) AndAlso clsCommon.myLen(dtDefaultUOM.Rows(0)(clsMasterDefault.colMCCChillingOnUOMKGLTR)) > 0 Then
                    qry = "select * from TSPL_UNIT_MASTER  WHERE unit_code='" + clsCommon.myCstr(dtDefaultUOM.Rows(0)(clsMasterDefault.colMCCChillingOnUOMKGLTR)) + "'"
                    Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry)
                    objMccUOM = New clsMccUOMDetails()
                    objMccUOM.Mcc_Code = obj.MCC_Code
                    objMccUOM.UOM_Code = clsCommon.myCstr(dttemp.Rows(0)("Unit_code"))
                    objMccUOM.UOM_Description = clsCommon.myCstr(dttemp.Rows(0)("Unit_Desc"))
                    objMccUOM.Stocking_Unit = "Y"
                    objMccUOM.Conversion_Factor = clsCommon.myCdbl(dttemp.Rows(0)("Conv_Factor"))
                    obj.ArrUomDetails.Add(objMccUOM)

                End If

                obj.arrChequeDetail = New List(Of clsMCCChequeDetails)


                obj.isNewEntry = True
                obj.Modified_By = objCommonVar.CurrentUserCode
                obj.Modified_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")
                obj.Comp_Code = objCommonVar.CurrentCompanyCode

                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")

                clsMccMaster.SaveData(obj)
                txtMCCOwnBMC.Value = obj.MCC_Code
                Dim MCCName As String = Nothing
                If clsCommon.myLen(obj.MCC_Code) > 0 Then
                    MCCName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select MCC_NAME from tspl_MCC_MASTER where MCC_Code = '" + clsCommon.myCstr(obj.MCC_Code) + "' "))
                Else
                    MCCName = ""
                End If
                lblMCCOwnBMC.Text = MCCName
                If chkOwnBMC.Checked Then
                    fndMcc.Value = obj.MCC_Code
                    If clsCommon.myLen(fndMcc.Value) > 0 Then
                        lblMCCName.Text = MCCName
                    Else
                        lblMCCName.Text = ""
                    End If
                End If

            End If
        Catch ex As Exception
            Return False
            myMessages.myExceptions(ex)
        End Try
        Return True
    End Function

    Sub UpdateFeild(ByVal strVendorCode As String, ByVal strVlcCode As String, ByVal trans As SqlTransaction)
        Dim strRegistered_PDCS_CLUSTER As String = ""
        If chkRegistered.Checked = True Then
            strRegistered_PDCS_CLUSTER = "Registered"
        ElseIf chkPDCS.Checked = True Then
            strRegistered_PDCS_CLUSTER = "PDCS"
        ElseIf chkCLUSTER.Checked = True Then
            strRegistered_PDCS_CLUSTER = "CLUSTER"
        End If
        Dim isOwnBMC As String = 0
        Dim MCCOwnBMC As String = clsCommon.myCstr(txtMCCOwnBMC.Value)
        If chkOwnBMC.Checked = True Then
            isOwnBMC = 1
            MCCOwnBMC = clsCommon.myCstr(txtMCCOwnBMC.Value)
        End If

        Dim strIFSCCode As String = ""
        If EnableBankFromMaster = True Then
            If clsCommon.myLen(findTxtIFSCCode2.Value) > 0 Then
                strIFSCCode = findTxtIFSCCode2.Value
            Else
                strIFSCCode = ""
            End If
            'findfndbankcode.Value = myDr(31).ToString()
            'findTxtIFSCCode.Value = myDr("IFSC_Code").ToString()
        Else
            If clsCommon.myLen(txtIFSCCode2.Text) > 0 Then
                strIFSCCode = txtIFSCCode2.Text
            Else
                strIFSCCode = ""
            End If
        End If
        'Me.fndbankcode.Text = myDr(31).ToString()
        'Me.TxtIFSCCode.Text = myDr("IFSC_Code").ToString()

        'End If
        'If clsCommon.myLen(txtIFSCCode2.Text) > 0 Then
        '    strIFSCCode = txtIFSCCode2.Text
        'ElseIf clsCommon.myLen(findTxtIFSCCode2.Value) > 0 Then
        '    strIFSCCode = findTxtIFSCCode2.Value
        'Else
        '    strIFSCCode = ""
        'End If

        Dim strbank As String = ""
        If EnableBankFromMaster = True Then
            If clsCommon.myLen(findfndbankcode2.Value) > 0 Then
                strbank = findfndbankcode2.Value
            Else
                strbank = ""
            End If
        Else
            If clsCommon.myLen(fndbankcode2.Text) > 0 Then
                strbank = fndbankcode2.Text
            Else
                strbank = ""
            End If
        End If
        'If clsCommon.myLen(fndbankcode2.Text) > 0 Then
        '    strbank = fndbankcode2.Text
        'ElseIf clsCommon.myLen(findfndbankcode2.Value) > 0 Then
        '    strbank = findfndbankcode2.Value
        '    Else
        '        strbank = ""
        'End If


        Dim qry As String = " update TSPL_VENDOR_MASTER set "
        If clsCommon.myLen(txtSavingCompanyBank.Value) > 0 Then
            qry += " Company_Bank ='" + txtSavingCompanyBank.Value + "' ,"
        Else
            qry += " Company_Bank = null , "
        End If

        If clsCommon.myLen(txtCurrentCompanyBank.Value) > 0 Then
            qry += " Company_Bank_Current ='" + txtCurrentCompanyBank.Value + "'"
        Else
            qry += " Company_Bank_Current = null "
        End If
        qry += " , Registered_PDCS_CLUSTER = '" + strRegistered_PDCS_CLUSTER + "' , StartDate = '" + clsCommon.GetPrintDate(txtStartDate.Value, "dd/MMM/yyyy") + "' , BankCode2 = '" + strbank + "' ,  Credit2 =  '" + clsCommon.myCstr(clsCommon.myCdbl(txtCredit2.Text)) + "' , IFSCCode2 = '" + strIFSCCode + "', AccNo2 = '" + TxtAccNo2.Text + "' , AccountType2 = '" + clsCommon.myCstr(cmbAccountType2.SelectedValue) + "', BankBranch2 = '" + txtBankBranch2.Text + "', BankName2='" + TxtBankName2.Text + "',SecurityCharges2 = '" + clsCommon.myCstr(clsCommon.myCdbl(TxtSecurityCharges2.Text)) + "' , SupervisorOrRP = '" + clsCommon.myCstr(txtSupervisiorRP.Value) + "' ,RegistrationNo = '" + txtRegistrationNo.Text + "' , RegistrationDate = '" + clsCommon.GetPrintDate(txtRegistrationDate.Value, "dd/MMM/yyyy") + "'  where Vendor_Code = '" + strVendorCode + "'  "
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        ' 
        If clsCommon.myLen(strVlcCode) > 0 Then
            qry = " update TSPL_VLC_MASTER_HEAD set Registered_PDCS_CLUSTER = '" + strRegistered_PDCS_CLUSTER + "' , StartDate = '" + clsCommon.GetPrintDate(txtStartDate.Value, "dd/MMM/yyyy") + "' , isOwnBMC = '" + isOwnBMC + "', MCCOwnBMC = '" + MCCOwnBMC + "', BankCode2 = '" + strbank + "' ,  Credit2 = '" + txtCredit2.Text + "' , IFSCCode2 = '" + strIFSCCode + "', AccNo2 = '" + TxtAccNo2.Text + "' , AccountType2 = '" + clsCommon.myCstr(cmbAccountType2.SelectedValue) + "', BankBranch2 = '" + txtBankBranch2.Text + "',BankName2='" + TxtBankName2.Text + "', SecurityCharges2 = '" + TxtSecurityCharges2.Text + "', SupervisorOrRP = '" + clsCommon.myCstr(txtSupervisiorRP.Value) + "' where vlc_Code = '" + strVlcCode + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If

    End Sub

    Function GetUpdateQry(ByVal strTagAsFranchise As String, ByVal joint_name As String) As String ', ByVal srvc_type As String
        'Dim strCmd111 As String = "Update TSPL_VENDOR_MASTER set  Monthly_Rent='" + clsCommon.myCstr(txtMonthlyRent.Value) + "', Nature='E',Billing_Date='" & clsCommon.GetPrintDate(DtpBillingDate.Value, "dd-MMM-yyyy") & "', franchise_yn='" + strTagAsFranchise + "',Service_charges='0',commision_pers='" + clsCommon.myCstr(clsCommon.myCdbl(txtcommpers.Text)) + "',incentive='" + clsCommon.myCstr(FndIncentive.Value) + "',incentive_days='" + clsCommon.myCstr(clsCommon.myCdbl(txtno_days.Text)) + "',vsp_payment='" + cmbvsppayment.SelectedValue + "',VSP_Payee_Name='" + txtpayeename.Text + "',Service_Charge_Type='" + srvc_type + "',Joint_Name='" + joint_name + "',payment_commision_pers='" + clsCommon.myCstr(clsCommon.myCdbl("payment_commision_pers")) + "',Is_Head_Load='" & IIf(ChkHeadLoad.Checked = True, "T", "F") & "',Rate_Head_Load='" & clsCommon.myCdbl(txtRateHeadLoad.Text) & "',Standard_Security_Amount='" & clsCommon.myCdbl(TxtStandardSec_Amt.Text) & "',Service_Basis_Head_Load='" & CmbHeadLoadServiceBasis.SelectedValue & "',Is_Own_Asset='" & IIf(ChkOwnAsset.Checked = True, "T", "F") & "',Rate_Own_Asset='" & clsCommon.myCdbl(TxtRateOwnAsset.Text) & "',Service_Basis_Own_Asset='" & CmbOwnAssetServiceBasis.SelectedValue & "',Pin_Code='" & clsCommon.myCstr(TxtPinCode.Text) & "',is_drip_saver='" & IIf(ChkIsDripSaver.Checked, "Y", "") & "',Joint_Branch_Name='" & IIf(EnableBankFromMaster = True, txtBankBranchCode.Value, texttxtBankBranchCode.Text) & "',Joint_IFSC_Code='" & txtIFCICode.Text & "'" + _
        '    ",EMP_Type='" + clsCommon.myCstr(cboEMPType.SelectedValue) + "',Apply_Mult_Incentive=" & IIf(chkMultIncentive.Checked = True, 1, 0) & ""
        Dim strCmd11 As String = "Update TSPL_VENDOR_MASTER set  Nature='E',Billing_Date='" & clsCommon.GetPrintDate(DtpBillingDate.Value, "dd-MMM-yyyy") & "', franchise_yn='" + strTagAsFranchise + "',Service_charges='0',commision_pers='" + clsCommon.myCstr(clsCommon.myCdbl(txtcommpers.Text)) + "',incentive='" + clsCommon.myCstr(FndIncentive.Value) + "',incentive_days='" + clsCommon.myCstr(clsCommon.myCdbl(txtno_days.Text)) + "',vsp_payment='" + cmbvsppayment.SelectedValue + "',VSP_Payee_Name='" + txtpayeename.Text + "',Joint_Name='" + joint_name + "',payment_commision_pers='" + clsCommon.myCstr(clsCommon.myCdbl("payment_commision_pers")) + "',Pin_Code='" & clsCommon.myCstr(TxtPinCode.Text) & "',is_drip_saver='" & IIf(ChkIsDripSaver.Checked, "Y", "") & "'" +
          ",Apply_Mult_Incentive=" & IIf(chkMultIncentive.Checked = True, 1, 0) & ""

        strCmd11 += " where Vendor_Code='" + fndvendorNo.Value + "'"
        Return strCmd11
    End Function
    Public Sub fundelete()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qst As String
            Dim dpt As String
            '------for payment screen---------
            qst = "select Vendor_Code from TSPL_PAYMENT_HEADER where Vendor_Code='" + fndvendorNo.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst, trans)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                Throw New Exception("This Customer Cannot be deleted." + Environment.NewLine + "This is already in Process")
                'Return
            End If
            '-----------for store recevied screen-------
            qst = "select Vendor_Code from TSPL_SRN_HEAD where Vendor_Code='" + fndvendorNo.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst, trans)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                Throw New Exception("This Customer Cannot be deleted." + Environment.NewLine + "This is already in Process")
                Return
            End If
            qst = "select Vendor_Code from TSPL_PR_HEAD where Vendor_Code='" + fndvendorNo.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst, trans)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                Throw New Exception("This Customer Cannot be deleted." + Environment.NewLine + "This is already in Process")
                Return
            End If

            connectSql.RunSpTransaction(trans, "sp_TSPL_VENDOR_MASTER_DELETE", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@form_type", txtvndrtype.Text))
            connectSql.RunSpTransaction(trans, "sp_TSPL_CUSTOMER_MASTER_DELETE", New SqlParameter("@Cust_Code", fndvendorNo.Value), New SqlParameter("@CUSTOMER_FORM_TYPE", "VSP"))
            clsCustomFieldValues.DeleteData(MyBase.Form_ID, fndvendorNo.Value, trans)

            'VLC
            If clsCommon.myLen(fndvlccode.Text) > 0 Then
                Dim qry As String = ""
                qry = "delete from TSPL_VLC_MASTER_DETAIL where VLC_Code='" & fndvlccode.Text & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_FAT_SNF_UPLOADER_VLC where VLC_Code='" & fndvlccode.Text & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_VLC_MASTER_HEAD where VLC_Code='" & fndvlccode.Text & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                'Route Mapping
                Dim sQuery As String = "delete from TSPL_MCC_ROUTE_VLC_MAPPING where vlc_Code='" & fndvlccode.Text & "'"
                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            End If

            myMessages.delete()
            btnsave.Text = "Save"
            btndelete.Enabled = False
            trans.Commit()
            Reset()
            funreset()
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Sub LoadVSPPayment()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "Self"
        dr("Name") = "Self"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Different"
        dr("Name") = "Different"
        dt.Rows.Add(dr)

        'cmbvsppayment.DataSource = Nothing
        cmbvsppayment.DataSource = dt
        cmbvsppayment.DisplayMember = "Name"
        cmbvsppayment.ValueMember = "Code"
        cmbvsppayment.SelectedValue = "Self"
        If clsCommon.CompairString(cmbvsppayment.SelectedValue, "Different") = CompairStringResult.Equal Then
            'txtpayeename.Enabled = True
            txtjointname.Enabled = True

        Else
            txtpayeename.Enabled = False
            txtjointname.Text = ""
            txtjointname.Enabled = False
            TxtIFSCCode.Text = ""
            txtpayeename.Text = txtvendorname.Text
        End If

    End Sub


    Sub LoadIncentive()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Slab"
        dr("Name") = "Slab Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Qty"
        dr("Name") = "Qty Wise"
        dt.Rows.Add(dr)

        'cmbincentive.DataSource = Nothing
        cmbincentive.DataSource = dt
        cmbincentive.DisplayMember = "Name"
        cmbincentive.ValueMember = "Code"
    End Sub


    Sub Load_KG_and_LTr(ByVal Cmb As common.Controls.MyComboBox)
        Load_KG_and_LTr(Cmb, False)
    End Sub
    Sub Load_KG_and_LTr(ByVal Cmb As common.Controls.MyComboBox, ByVal isFATSNFWeight As Boolean)
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = "K"
        dr("Name") = "KG"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "L"
        dr("Name") = "LTR"
        dt.Rows.Add(dr)

        If isFATSNFWeight Then
            dr = dt.NewRow()
            dr("Code") = "W"
            dr("Name") = "FAT/SNF KG"
            dt.Rows.Add(dr)
        End If

        Cmb.DataSource = dt
        Cmb.DisplayMember = "Name"
        Cmb.ValueMember = "Code"
    End Sub


    'this function will reset all the fields for new entry
    Public Sub funreset()
        UcAttachment1.Form_ID = clsUserMgtCode.frmVSPMaster
        UcAttachment1.BlankAllControls()

        chk_isblacklist.Checked = False
        IsInsieLoadData = False
        chkMultIncentive.Checked = False
        txtIncentiveMult.Enabled = False
        txtLoyaltyPer.Value = 0
        txtjointname.Text = ""
        txtno_days.Enabled = False
        txtpayeename.Enabled = False
        ''txtMonthlyRent.Value = 0
        lblActiveDate.Text = ""
        cmbGender.Text = ""
        chkHoldPaymentProcess.Checked = False
        TxtPinCode.Text = Nothing
        fndpaymentCycle.Value = Nothing
        TxtPaymentCycle.Text = Nothing
        ''txtBankCode.Value = Nothing
        ChkIsDripSaver.Checked = False
        txtcommpers.Text = ""
        txtpaymnt_cmsn.Text = ""
        cmbincentive.SelectedValue = ""
        txtno_days.Text = ""
        cmbvsppayment.SelectedValue = ""
        txtpayeename.Text = ""
        ''txtChequeInFavour.Enabled = True
        txtcountrycode.Value = clsDBFuncationality.getSingleValue("select country_Code from tspl_Country_Master where Country_Code='INDIA'") '""
        txtCountry.Text = clsDBFuncationality.getSingleValue("select country_Name from tspl_Country_Master where Country_Code='INDIA'") '""
        txtstatecode.Value = ""
        txtvndrtype.Text = "VSP"
        'fndVendorCOde.Value = ""
        Me.fndvendorNo.Value = ""
        Me.txtvendortypedes.Text = ""
        Me.txtvendorname.Text = ""
        Me.txtvendornameHindi.Text = ""
        Me.fndgroupcode.Value = ""
        Me.txtgroupdes.Text = ""
        chkHold.Checked = False
        chkInActive.Checked = False
        chktrarns.Checked = False
        Me.dtClosing.Value = connectSql.serverDate()
        Me.txtAdd1.Text = ""
        Me.txtAdd2.Text = ""
        Me.txtAdd3.Text = ""
        Me.fndCity.Value = ""
        Me.txtCity.Text = ""
        Me.txtState.Text = ""
        ' Me.txtCountry.Text = ""
        Me.txtPhone1.Text = "(+__)__________"
        Me.txtPhone2.Text = "(+__)__________"
        Me.txtfax.Text = ""
        Me.txtEmail.Text = ""
        Me.txtWeb.Text = ""
        Me.fndvendortype.Value = ""
        Me.txtvendortypedes.Text = ""
        Me.fndbankcode.Text = ""
        Me.txtbankcodedes.Text = ""
        Me.txtbankcodedes2.Text = ""
        Me.txtCredit.Text = "0.00"
        Me.txtcollect.Text = ""
        Me.txtpan.Text = ""
        Me.chkInterBranch.Checked = False
        Me.fndVendorCurrency.Value = Nothing
        '' Anubhooti 4-Aug-2014 BM00000003319

        TxtBankBranch.Text = ""
        TxtBankName.Text = ""
        TxtIFSCCode.Text = ""
        TxtAccNo.Text = ""
        TxtBankName2.Text = ""

        TxtBankName.Text = ""
        chkInActive.Checked = False
        dtClosing.Enabled = False
        findfndbankcode.Value = ""
        findTxtIFSCCode.Value = ""
        btnsave.Text = "Save"
        btndelete.Enabled = False
        chkTagAsFranchise.Checked = False
        txtvendorname.Focus()
        TxtSecurityCharges.Text = Nothing
        DtpBillingDate.Value = clsCommon.GETSERVERDATE()
        DtpEndBillingDate.Value = DtpBillingDate.Value.AddDays(2)
        chktrarns.Visible = False
        TxtAmCU.Text = Nothing
        TxtAmc_Charge.Text = Nothing
        cmbvsppayment.SelectedValue = "Self"
        FndIncentive.Value = Nothing
        LblIncentive.Text = ""

        MyTextBox2.Text = ""
        txtEntity.Text = ""
        txtGSTIN_No_final.Text = ""
        txtGST_PanCode.Text = ""
        txtGSTStateCode.Text = ""
        numCorrectionFat.Text = ""
        numCorrectionSNF.Text = ""
        TxtCreditLimitBasedOnMilkReceipt.Text = Nothing
        chkCreditLimitBasedOnMilkReceipt.Checked = False
        chkCreateCustomerAlso.Checked = False
        fndCusgrp.Value = Nothing

        txtTIPBuffalo.Value = 0
        txtTIPCow.Value = 0
        txtTIPMix.Value = 0
        fndVSPCopy.Value = ""

        chkRegistered.Checked = False
        chkPDCS.Checked = False
        chkCLUSTER.Checked = False
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        txtRegistrationDate.Value = clsCommon.GETSERVERDATE()
        txtRegistrationNo.Text = ""
        chkOwnBMC.Checked = False
        txtMCCOwnBMC.Value = ""
        lblMCCOwnBMC.Text = ""
        findfndbankcode2.Value = ""
        txtbankcodedes2.Text = ""
        txtCredit2.Text = 0
        txtIFSCCode2.Text = ""
        TxtAccNo2.Text = ""
        TxtBankName2.Text = ""
        txtBankBranch2.Text = ""
        TxtSecurityCharges2.Text = ""
        fndbankcode2.Text = ""
        txtIFSCCode2.Text = ""
        findTxtIFSCCode2.Value = ""
        txtSupervisiorRP.Value = ""
        lblSupervisiorRPName.Text = ""
        txtDistrict.Value = ""
        lblDistrict.Text = ""
        txtBlockCode.Value = ""
        lblBlockCode.Text = ""
        txtZone.Value = ""
        lblZone.Text = ""
        txtCastCategory.Value = ""
        txtSavingCompanyBank.Value = ""
        lblSavingCompanyBank.Text = ""
        txtCurrentCompanyBank.Value = ""
        lblCurrentCompanyBank.Text = ""
        txtVidhanSabha.Value = ""
        lblVidhanSabha.Text = ""
        txtPanchayatSamiti.Value = ""
        lblPanchayatSamiti.Text = ""
        txtGrampanchayat.Value = ""
        lblGrampanchayat.Text = ""
        txtRevenueVillage.Value = ""
        lblRevenueVillage.Text = ""
        ChkHeadLoad.Checked = False
        CmbHeadLoadServiceBasis.Text = ""
        txtRateHeadLoad.Text = ""
        funSetDefaultData()
        'VLC
        VLC_reset()
    End Sub

    Private Sub VLC_reset()
        chkInActive.Checked = False
        txtVLCCodeVlcUploader.Text = ""
        txtvillcode.Value = ""
        txtvillname.Text = ""
        txtroutecode.Value = ""
        txtroutename.Text = ""
        'fndvlccode.MyReadOnly = False
        fndvlccode.ReadOnly = True
        fndvlccode.Text = ""
        txtvlcname.Text = ""
        'txtvehicalname.Text = ""
        txtvsp.Text = ""
        txtvspcode.Value = ""
        chkApplyCowPrice.Checked = False
        chkSuspense.Checked = False
        fndMcc.Value = Nothing
        fndPriceCode.Value = Nothing
        fndPriceCode.Enabled = True
        lblMCCName.Text = ""
        'MCCLOCATIONFINDER()
        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AutoUpdateVLCUploaderCodeInVLCMaster, clsFixedParameterCode.AutoUpdateVLCUploaderCodeInVLCMaster, Nothing), "1") = CompairStringResult.Equal Then
            txtVLCCodeVlcUploader.Enabled = False
        Else
            txtVLCCodeVlcUploader.Enabled = True
        End If
    End Sub

    Private Sub funSetDefaultData()
        txtstatecode.Value = clsDBFuncationality.getSingleValue("select state from TSPL_COMPANY_MASTER where comp_code='" + objCommonVar.CurrentCompanyCode + "'")
        fndgroupcode.Value = DefaultVendorGroupCodeforVSP
        If clsCommon.myLen(DefaultVendorGroupCodeforVSP) > 0 Then
            Me.txtgroupdes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Group_Desc,'') as tt from TSPL_VENDOR_GROUP where Ven_Group_Code='" + DefaultVendorGroupCodeforVSP + "'"))
        Else
            Me.txtgroupdes.Text = ""
        End If
        Me.txtState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_STATE_MASTER.STATE_NAME from TSPL_STATE_MASTER left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.State=TSPL_STATE_MASTER.STATE_CODE where TSPL_COMPANY_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
        fndCusgrp.Value = DefaultCustomerGroupCodeforVSP
        txtTIPBuffalo.Value = TIPRateBuffalo
        txtTIPCow.Value = TIPRateCow
        txtTIPMix.Value = TIPRateMix

        If objCommonVar.ApplyDefaultsInMaster = True Then
            chkCreateCustomerAlso.Checked = False
            fndgroupcode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ven_Group_Code from Tspl_vendor_group where Default_VSP=1"))
            If clsCommon.myLen(fndgroupcode.Value) > 0 Then
                txtgroupdes.Text = clsDBFuncationality.getSingleValue("Select group_desc from tspl_vendor_group where ven_group_code='" + fndgroupcode.Value + "'")
            End If

            fndCusgrp.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Group_Code from TSPL_CUSTOMER_GROUP_MASTER where Default_VSP=1"))

            fndCity.Value = clsCommon.myCstr(clsCityMaster.GetDefault())
            txtCity.Text = clsCommon.myCstr(clsCityMaster.GetName(fndCity.Value))
            txtstatecode.Value = clsCommon.myCstr(clsStateMaster.GetDefault())
            txtState.Text = clsCommon.myCstr(clsStateMaster.GetName(txtstatecode.Value))
            fndpaymentCycle.Value = clsCommon.myCstr(clsPaymentCycleMaster.GetDefault())
        End If
    End Sub


#End Region

#Region "Event"
    Sub fndvendorNo_text_changed()
        Try
            Dim str As String = "select vendor_code from TSPL_VENDOR_MASTER where vendor_code = '" + fndvendorNo.Value + "' and form_type='" + txtvndrtype.Text + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(str)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                strvalue = dr.Rows(0)("vendor_code").ToString()
            End If
            If strvalue <> "" Then
                funfill()
                Dim TempVLC_Code As String = clsDBFuncationality.getSingleValue("select isnull(VLC_Code,'') as VLC_Code from TSPL_VLC_MASTER_HEAD where VSP_Code='" + fndvendorNo.Value + "'")
                If clsCommon.myLen(TempVLC_Code) > 0 Then
                    VLCLoadData(TempVLC_Code, NavigatorType.Current)
                Else
                    VLC_reset()
                End If
                If isLoadCopy = True Then
                    btnsave.Text = "Save"
                    btnsave.Enabled = True
                    btndelete.Enabled = False
                Else
                    btnsave.Text = "Update"
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                End If

            Else
                txtno_days.Text = ""
                cmbincentive.SelectedValue = ""
                cmbvsppayment.SelectedValue = ""
                txtpayeename.Text = ""
                txtcommpers.Text = ""
                txtpaymnt_cmsn.Text = ""
                txtcountrycode.Value = ""
                txtstatecode.Value = ""
                Me.txtvendorname.Text = ""
                Me.txtvendornameHindi.Text = ""
                Me.fndgroupcode.Value = ""
                Me.txtgroupdes.Text = ""
                chkHold.Checked = False
                chkInActive.Checked = False
                chktrarns.Checked = False
                chkTagAsFranchise.Checked = False
                chkHoldPaymentProcess.Checked = False
                Me.dtClosing.Value = clsCommon.GETSERVERDATE()
                Me.txtAdd1.Text = ""
                Me.txtAdd2.Text = ""
                Me.txtAdd3.Text = ""
                Me.fndCity.Value = ""
                Me.txtCity.Text = ""
                Me.txtState.Text = ""
                Me.txtCountry.Text = ""
                Me.txtPhone1.Text = ""
                Me.txtPhone2.Text = ""
                Me.txtfax.Text = ""
                Me.txtEmail.Text = ""
                Me.txtWeb.Text = ""
                Me.fndvendortype.Value = ""
                Me.txtvendortypedes.Text = ""
                Me.fndbankcode.Text = ""
                Me.fndbankcode2.Text = ""
                Me.txtbankcodedes.Text = ""
                Me.txtbankcodedes2.Text = ""
                Me.txtCredit.Text = "0.00"
                Me.txtcollect.Text = ""
                Me.txtpan.Text = ""
                If AllowVSPMasterAutoPrefix = 1 Then
                    fndvendorNo.Value = ""
                End If
                VLC_reset()
                btnsave.Text = "Save"
                btndelete.Enabled = False
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
    '    Try
    '        'gv.Rows.Clear()

    '        Dim obj As clsfrmVLCMaster = clsfrmVLCMaster.GetData(strCode, arrLoc, NavType)
    '        'isNewEntry = True
    '        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.vlcCode) > 0 Then
    '            'isNewEntry = False
    '            fndvlccode.Text = obj.vlcCode
    '            txtVLCCodeVlcUploader.Text = clsCommon.myCstr(obj.VLC_CODE_VLC_UPLOADER)
    '            txtvlcname.Text = obj.vlcName
    '            'txtvehicalname.Text = obj.vehical
    '            txtvspcode.Value = obj.vspCode
    '            txtvsp.Text = obj.VspName
    '            fndMcc.Value = obj.MCCCOde
    '            txtvillcode.Value = obj.mainvillcode
    '            txtvillname.Text = obj.mainvillname
    '            txtroutecode.Value = obj.routecode
    '            txtroutename.Text = obj.routename
    '            fndPriceCode.Value = obj.Price_Code
    '            Me.chkInActive.Checked = IIf(obj.Active = 0, True, False)
    '            fndvlccode.ReadOnly = True

    '        Else
    '            Reset()
    '        End If

    '        'isLoadData = False
    '    Catch ex As Exception
    '        'isNewEntry = True
    '        'isLoadData = False
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub


    Public Sub fndvendorNo_text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields
    Sub fndgroupcode_text_Changed()
        Try
            Dim strquery As String = "select ven_Group_code,group_desc  from Tspl_vendor_group where ven_group_code='" + fndgroupcode.Value + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then

                strvalue = dr.Rows(0)("ven_Group_code").ToString()
            End If
            If strvalue <> "" Then
                funfillfndGroupCode()
            Else
                txtgroupdes.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub fndgroupcode_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields
    Sub fndcity_text_Changed()
        Try
            Dim str As String = "select City_Code,city_Name from TSPL_City_MASTER where City_code = '" + fndCity.Value + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(str)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then

                strvalue = dr.Rows(0)("City_Code").ToString()
            End If
            If strvalue <> "" Then
                funfilfndcity()
            Else
                txtCity.Text = ""

            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub fndcity_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields
    Public Sub fndvendortype_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "SELECT  [ven_Type_Code] as [Vendor Type Code],[ven_Type_Desc] as [Description]FROM [TSPL_VENDOR_TYPE_MASTER] where ven_type_code='" + fndvendortype.Value + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(str)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                strvalue = dr.Rows(0)("Vendor Type Code").ToString()
            End If
            If strvalue <> "" Then
                funfillfndventype()
            Else
                txtvendortypedes.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    Sub fndgroupcode_leave()
        If fndgroupcode.Value = "" Then
        Else
            Try
                Dim strquery As String = "select ven_Group_code,group_desc  from Tspl_vendor_group where ven_group_code='" + fndgroupcode.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)("ven_Group_code").ToString()
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    txtgroupdes.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Group Code does not exist in Master Table", Me.Text)
                    fndgroupcode.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub fndgroupcode_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    Sub fndCity_leave()
        If fndCity.Value = "" Then
        Else
            Try
                Dim strquery As String = "select City_Code ,city_Name from TSPL_City_MASTER where City_code = '" + fndCity.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)("City_Code").ToString()
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    txtCity.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This City does not exist in Master Table", Me.Text)
                    fndCity.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub fndCity_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    Public Sub fndvendortype_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndvendortype.Value = "" Then
        Else
            Try
                Dim strquery As String = "SELECT  [ven_Type_Code] as [Vendor Type Code],[ven_Type_Desc] as [Description]FROM [TSPL_VENDOR_TYPE_MASTER] where ven_type_code='" + fndvendortype.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)("Vendor Type Code").ToString()
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    txtvendortypedes.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Vendor Type does not exist in Master Table", Me.Text)
                    fndvendortype.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndvendorNo_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'If (e.KeyChar = Chr(39)) Then
        '    e.Handled = True
        'End If
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndgroupcode_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndCity_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndTermsCode_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndAccntSet_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndPayCode_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndvendortype_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Public Sub fndTxGrp_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    'Validation in credit limit text box
    Private Sub txtCredit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCredit.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48)) Then
        Else
            e.Handled = True
        End If
    End Sub
    'Email Validation
    Private Sub txtEmail_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.Leave
        If txtEmail.Text = "" Then
        Else
            Dim check As Match = Regex.Match(txtEmail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            If check.Success Then
                Errorcontrol.ResetError(txtEmail)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter the proper format of e-mail address", Me.Text)
                txtEmail.Text = ""
                txtEmail.Focus()
                txtEmail.Select()
                Errorcontrol.SetError(txtEmail, "Please Enter the proper format of e-mail address")
            End If
        End If
    End Sub

    'Numerics Validation-                                               --------------------------------------------
    Private Sub txtPhone1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtPhone2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtfax_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfax.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtContactFax_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtContPhone_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub
#End Region

#Region "Finder"

    Private Sub fndvendorNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndvendorNo.Query = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Vendor_Group_Code as [Vendor Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_VENDOR_MASTER  "
        'fndvendorNo.ConnectionString = connectSql.SqlCon()
        'fndvendorNo.Caption = "Vendor"
        'fndvendorNo.ValueToSelect = "Vendor Code"
        'fndvendorNo.ValueToSelect1 = "Vendor Name"
    End Sub

    Private Sub fndgroupcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'fndgroupcode.Query = " SELECT ven_Group_Code as [Vendor Group Code],Group_Desc as [Description],Tax_Group_Code as [Tax Group],Acct_Set_Code as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_VENDOR_GROUP] "
        'fndgroupcode.ConnectionString = connectSql.SqlCon()
        'fndgroupcode.Caption = "Vendor Group"
        'fndgroupcode.ValueToSelect = "Vendor Group Code"
        'fndgroupcode.ValueToSelect1 = "Description"
    End Sub

    Private Sub fndCity_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndCity.Query = "SELECT [City_Code] as [City Code],[City_Name] as [City Name]FROM [TSPL_CITY_MASTER]"
        'fndCity.ConnectionString = connectSql.SqlCon()
        'fndCity.Caption = "City"
        'fndCity.ValueToSelect = "City Code"
        'fndCity.ValueToSelect1 = "City Name"
    End Sub
    'Private Sub fndTrmsCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndTrmsCode.Query = "SELECT [Terms_Code] as [Terms Code],[Terms_Desc] as [Description],[No_Days] as [No. of Days] FROM [TSPL_TERMS_MASTER]"
    '    fndTrmsCode.ConnectionString = connectSql.SqlCon()
    '    fndTrmsCode.Caption = "Payments Terms"
    '    fndTrmsCode.ValueToSelect = "Terms Code"
    '    fndTrmsCode.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndAccntSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndAccntSet.Query = "SELECT  [Acct_Set_Code] as [Account Set Code],[Acct_Set_Desc] as [Description] FROM [TSPL_VENDOR_ACCOUNT_SET]"
    '    fndAccntSet.ConnectionString = connectSql.SqlCon()
    '    fndAccntSet.Caption = "Vendor Account Set"
    '    fndAccntSet.ValueToSelect = "Account Set Code"
    '    fndAccntSet.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndPayCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndPayCode1.Query = "  SELECT [Payment_Code] as [Payment Code],[payment_Desc] as [Description] FROM [TSPL_PAYMENT_CODE]"
    '    fndPayCode1.ConnectionString = connectSql.SqlCon()
    '    fndPayCode1.Caption = "Payment Code"
    '    fndPayCode1.ValueToSelect = "Payment Code"
    '    fndPayCode1.ValueToSelect1 = "Description"
    'End Sub


    'Private Sub fndvendortype_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    fndvendortype1.Query = "SELECT  [ven_Type_Code] as [Vendor Type Code],[ven_Type_Desc] as [Description]FROM [TSPL_VENDOR_TYPE_MASTER]"
    '    fndvendortype1.ConnectionString = connectSql.SqlCon()
    '    fndvendortype1.Caption = "Vendor Type"
    '    fndvendortype1.ValueToSelect = "Vendor Type Code"
    '    fndvendortype1.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndbankcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndbankcode.ConnectionString = connectSql.SqlCon()
    '    'fndbankcode.Query = " select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER "
    '    fndbankcode.Query = clsERPFuncationality.glbankquery
    '    fndbankcode.ValueToSelect = "Bank Code"
    '    fndbankcode.Caption = "Bank Master"
    '    fndbankcode.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndTxGrp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'fndTxGrp.Query = "SELECT [Tax_Group_Code] AS [Tax Group Code],[Tax_Group_Desc] as [Description]," & _
    '    '   " (select case when [Tax_Group_Type]='S' then 'Sale' else 'Purchase' end) as [Type] FROM [TSPL_TAX_GROUP_MASTER] where Tax_Group_Type='S'"
    '    fndTxGrp.Query = clsERPFuncationality.UserAvailableTaxGroup + " and  M.Tax_Group_Type='P'"
    '    fndTxGrp.ConnectionString = connectSql.SqlCon()
    '    fndTxGrp.Caption = "Tax Group"
    '    fndTxGrp.ValueToSelect = "Code"
    '    fndTxGrp.ValueToSelect1 = "Description"
    'End Sub

    Private Sub chkInActive_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkInActive.ToggleStateChanged
        If chkInActive.Checked = True Then
            dtClosing.Enabled = True
            lblActiveDate.Text = ""
        ElseIf chkInActive.Checked = False Then
            dtClosing.Enabled = False
            dtClosing.Value = connectSql.serverDate()
            If clsCommon.myLen(fndvendorNo.Value) > 0 Then
                Dim strVendorStatus As String = clsDBFuncationality.getSingleValue("select Status from tspl_vendor_master where Vendor_Code = '" + fndvendorNo.Value + "'")
                If strVendorStatus = "Y" AndAlso chkInActive.Checked = False Then
                    lblActiveDate.Text = connectSql.serverDate()
                End If
            End If
        End If
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "VENDOR-M"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function





#End Region

#Region "Button Click"
    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If btnsave.Text = "Update" AndAlso OneTimeCheck = False Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.Transactionupdate
                frm.strCode = clsFixedParameterCode.VendorMaster
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    ShowRemarks()
                    OneTimeCheck = True
                End If
            ElseIf btnsave.Text = "Update" AndAlso OneTimeCheck Then
                ShowRemarks()
            Else
                SaveData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        'SaveData()
    End Sub

    Private Sub ShowRemarks()
        Try
            Dim Reason As String = ""
            Dim frm As New FrmFreeTxtBox1
            frm.Text = "Remarks for Update"
            frm.ShowDialog()
            If clsCommon.myLen(frm.strRmks) <= 0 Then
                Exit Sub
            Else
                Reason = frm.strRmks
            End If
            SaveData()
            saveCancelLog(Reason, "Updated", Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndvendorNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Sub SaveData()
        Try
            If AllowVSPMasterAutoPrefix = 0 Then
                If fndvendorNo.Value = "" Then
                    myMessages.blankValue(Me, "Please Fill DCS Code", Me.Text)
                    pageCus.SelectedPage = RadPageViewPage1
                    fndvendorNo.Focus()
                    fndvendorNo.Select()
                    Errorcontrol.SetError(fndvendorNo, "Please Fill DCS Code")
                    Return
                Else
                    Errorcontrol.ResetError(fndvendorNo)
                End If

            End If
            If txtvendorname.Text = "" Then
                myMessages.blankValue(Me, "Please Fill DCS Name", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                txtvendorname.Focus()
                txtvendorname.Select()
                Errorcontrol.SetError(txtvendorname, "Please Fill DCS Name")
                Return
            Else
                Errorcontrol.ResetError(txtvendorname)
            End If

            If fndgroupcode.Value = "" Then
                myMessages.blankValue(Me, "Please Select Group Code", Me.Text)
                pageCus.SelectedPage = RadPageViewPage5
                fndgroupcode.Focus()
                fndgroupcode.Select()
                Errorcontrol.SetError(fndgroupcode, "Please Select Group Code")
                Return
            Else
                Errorcontrol.ResetError(fndgroupcode)
            End If

            'If clsCommon.myLen(txtAdd1.Text) <= 0 AndAlso clsCommon.myLen(txtAdd2.Text) <= 0 AndAlso clsCommon.myLen(txtAdd3.Text) <= 0 Then
            '    myMessages.blankValue("Please Fill Address")
            '    pageCus.SelectedPage = RadPageViewPage5
            '    txtAdd1.Focus()
            '    txtAdd1.Select()
            '    Errorcontrol.SetError(txtAdd1, "Please Fill Address")
            '    Errorcontrol.SetError(txtAdd2, "Please Fill Address")
            '    Errorcontrol.SetError(txtAdd3, "Please Fill Address")
            '    Return
            'Else
            '    Errorcontrol.ResetError(txtAdd1)
            '    Errorcontrol.ResetError(txtAdd2)
            '    Errorcontrol.ResetError(txtAdd3)
            'End If

            If clsCommon.myLen(txtcountrycode) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Country", Me.Text)
                pageCus.SelectedPage = RadPageViewPage5
                txtcountrycode.Select()
                txtcountrycode.Focus()
                Errorcontrol.SetError(txtcountrycode, "Please Select Country")
                Return
            Else
                Errorcontrol.ResetError(txtcountrycode)
            End If

            If clsCommon.myLen(txtstatecode) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select State", Me.Text)
                pageCus.SelectedPage = RadPageViewPage5
                txtstatecode.Select()
                txtstatecode.Focus()
                Errorcontrol.SetError(txtstatecode, "Please Select State")
                Return
            Else
                Errorcontrol.ResetError(txtstatecode)
            End If

            If clsCommon.myLen(txtCity) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select City", Me.Text)
                pageCus.SelectedPage = RadPageViewPage5
                txtCity.Select()
                txtCity.Focus()
                Errorcontrol.SetError(txtcountrycode, "Please Select City")
                Return
            Else
                Errorcontrol.ResetError(txtCity)
            End If
            ' Modify By : Prabhakar Ref Ticket : BM00000010125
            If clsCommon.myLen(TxtPinCode.Text) > 0 Then
                If clsCommon.myLen(TxtPinCode.Text) <> 6 Then
                    pageCus.SelectedPage = RadPageViewPage5
                    clsCommon.MyMessageBoxShow(Me, "Invalid Pin Code.Please Enter Pin Code 6 Digit", Me.Text)
                    Errorcontrol.SetError(TxtPinCode, "Invalid Pin Code.Please Enter Pin Code 6 Digit")
                    Return
                Else
                    Errorcontrol.ResetError(TxtPinCode)
                End If
            Else
                Errorcontrol.ResetError(TxtPinCode)

            End If


            If cmbvsppayment.SelectedValue = "" Then
                clsCommon.MyMessageBoxShow(Me, "Please Select VSP Payment Value", Me.Text)
                pageCus.SelectedPage = RadPageViewPage5
                cmbvsppayment.Select()
                Errorcontrol.SetError(cmbvsppayment, "Please Select VSP Payment Value")
                Return
            Else
                Errorcontrol.ResetError(cmbvsppayment)
            End If

            If EnableBankFromMaster = True Then
                If clsCommon.myLen(findfndbankcode.Value) = 0 Then
                    myMessages.blankValue(Me, "Please Enter Bank Code", Me.Text)
                    pageCus.SelectedPage = RadPageViewPage2
                    findfndbankcode.Focus()
                    findfndbankcode.Select()
                    Errorcontrol.SetError(findfndbankcode, "Please Enter Bank Code")
                    Return
                Else
                    Errorcontrol.ResetError(findfndbankcode)
                End If

                If clsCommon.myLen(findTxtIFSCCode.Value) = 0 Then
                    myMessages.blankValue(Me, "Please Enter IFSC Code", Me.Text)
                    pageCus.SelectedPage = RadPageViewPage2
                    findTxtIFSCCode.Focus()
                    findTxtIFSCCode.Select()
                    Errorcontrol.SetError(findTxtIFSCCode, "Please Enter IFSC Code")
                    Return
                Else
                    Errorcontrol.ResetError(findTxtIFSCCode)
                End If
                'If clsCommon.myLen(findfndbankcode2.Value) = 0 Then
                '    myMessages.blankValue("Please Enter Bank Code")
                '    pageCus.SelectedPage = RadPageViewPage2
                '    fndbankcode2.Focus()
                '    findfndbankcode2.Select()
                '    Errorcontrol.SetError(findfndbankcode2, "Please Enter Bank Code")
                '    Return
                'Else
                '    Errorcontrol.ResetError(findTxtIFSCCode2)
                'End If
                'If clsCommon.myLen(findTxtIFSCCode2.Value) = 0 Then
                '    myMessages.blankValue("Please Enter IFSC Code")
                '    pageCus.SelectedPage = RadPageViewPage2
                '    findTxtIFSCCode2.Focus()
                '    findTxtIFSCCode2.Select()
                '    Errorcontrol.SetError(findTxtIFSCCode2, "Please Enter IFSC Code")
                '    Return
                'Else
                '    Errorcontrol.ResetError(findTxtIFSCCode2)
                'End If
            Else
                If clsCommon.myLen(fndbankcode.Text) = 0 Then
                    myMessages.blankValue(Me, "Please Enter Bank Code", Me.Text)
                    pageCus.SelectedPage = RadPageViewPage2
                    fndbankcode.Focus()
                    fndbankcode.Select()
                    Errorcontrol.SetError(fndbankcode, "Please Enter Bank Code")
                    Return
                Else
                    Errorcontrol.ResetError(fndbankcode)
                End If

                If clsCommon.myLen(TxtIFSCCode.Text) = 0 Then
                    myMessages.blankValue(Me, "Please Enter IFSC Code", Me.Text)
                    pageCus.SelectedPage = RadPageViewPage2
                    TxtIFSCCode.Focus()
                    TxtIFSCCode.Select()
                    Errorcontrol.SetError(TxtIFSCCode, "Please Enter IFSC Code")
                    Return
                Else
                    Errorcontrol.ResetError(TxtIFSCCode)
                End If
                'richa agarwal 27/03/2015
                ''--------------------
            End If

            If clsCommon.CompairString(cmbincentive.SelectedValue, "Qty") = CompairStringResult.Equal AndAlso (clsCommon.myLen(txtno_days.Text) <= 0 Or clsCommon.myCdbl(txtno_days.Text) <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter No.Of Days For Incentive", Me.Text)
                txtno_days.Focus()
                txtno_days.Select()
                Errorcontrol.SetError(txtno_days, "Please Enter No.Of Days For Incentive")
                Return
            Else
                Errorcontrol.ResetError(txtno_days)
            End If

            If clsCommon.CompairString(cmbvsppayment.SelectedValue, "Different") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtpayeename.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter VSP Payment Payee Name", Me.Text)
                txtpayeename.Focus()
                txtpayeename.Select()
                Errorcontrol.SetError(txtpayeename, "Please Enter VSP Payment Payee Name")
                Return
            Else
                Errorcontrol.ResetError(txtpayeename)
            End If

            If clsCommon.myLen(txtpan.Text) > 0 Then
                Dim qry As String = "select count(*) from tspl_vendor_master where PAN='" + clsCommon.myCstr(txtpan.Text) + "' and Form_Type='VSP'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal AndAlso check > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.", Me.Text)
                    pageCus.SelectedPage = RadPageViewPage1
                    txtpan.Focus()
                    txtpan.Select()
                    Errorcontrol.SetError(txtpan, "Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.")
                    Return
                Else
                    Errorcontrol.ResetError(txtpan)
                End If

                If clsCommon.CompairString(btnsave.Text, "Save") <> CompairStringResult.Equal AndAlso check > 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.", Me.Text)
                    pageCus.SelectedPage = RadPageViewPage1
                    txtpan.Focus()
                    txtpan.Select()
                    Errorcontrol.SetError(txtpan, "Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.")
                    Return
                Else
                    Errorcontrol.ResetError(txtpan)
                End If

                If clsCommon.myLen(txtpan.Text) > 0 Then
                    If Not checkPan.IsMatch(txtpan.Text) Then
                        txtpan.Focus()
                        Throw New Exception("Please check ! PAN numbers need to have 5 characters followed by 4 numbers then a final character")
                    End If
                End If
            Else
                Errorcontrol.ResetError(txtpan)
            End If
            If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Country", Me.Text)
                pageCus.SelectedPage = RadPageViewPage5
                txtcountrycode.Focus()
                txtcountrycode.Select()
                Errorcontrol.SetError(txtCountry, "Please Select Country")
                Return
            Else
                Errorcontrol.ResetError(txtCountry)
            End If

            If clsCommon.myLen(txtstatecode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select State", Me.Text)
                pageCus.SelectedPage = RadPageViewPage5
                txtstatecode.Focus()
                txtstatecode.Select()
                Errorcontrol.SetError(txtState, "Please Select State")
                Return
            Else
                Errorcontrol.ResetError(txtState)
            End If


            If clsCommon.myLen(fndCity.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select City", Me.Text)
                pageCus.SelectedPage = RadPageViewPage5
                fndCity.Focus()
                fndCity.Select()
                Errorcontrol.SetError(txtCity, "Please Select City")
                Return
            Else
                Errorcontrol.ResetError(txtCity)
            End If
            Dim totcharge As Decimal = 0
            ''For Each row As GridViewRowInfo In gvCharges.Rows
            ''    totcharge += row.Cells("COLRate").Value
            ''Next
            If txtvendorname.Text = "" Then
                myMessages.blankValue(Me, "Please Fill VSP Name", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                txtvendorname.Focus()
                txtvendorname.Select()
                Errorcontrol.SetError(txtvendorname, "Please Fill VSP Name")
                Return
            Else
                Errorcontrol.ResetError(txtvendorname)
            End If
            If clsCommon.myLen(txtjointname.Text) <= 0 And clsCommon.myCstr(cmbvsppayment.Text) = "Different" Then
                clsCommon.MyMessageBoxShow(Me, "Please Fill Joint Name", Me.Text)
                pageCus.SelectedPage = RadPageViewPage5
                txtjointname.Focus()
                txtjointname.Select()
                Errorcontrol.SetError(txtjointname, "Please Fill Joint Name")
                Return
            Else
                Errorcontrol.ResetError(txtjointname)
            End If

            'If clsCommon.myLen(txtroutecode.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Fill Route Code", Me.Text)
            '    pageCus.SelectedPage = RadPageViewPage1
            '    txtroutecode.Focus()
            '    txtroutecode.Select()
            '    Errorcontrol.SetError(txtroutecode, "Please Fill Route Code")
            '    Return
            'Else
            '    Errorcontrol.ResetError(txtroutecode)
            'End If

            If chkCreditLimitBasedOnMilkReceipt.Checked Then
                If TxtCreditLimitBasedOnMilkReceipt.Value < 0 OrElse TxtCreditLimitBasedOnMilkReceipt.Value > 100 Then
                    TxtCreditLimitBasedOnMilkReceipt.Focus()
                    Throw New Exception("Credit Limit Based% range should be (0-100)")
                End If
            End If


            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmVSPMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            If objCommonVar.GSTApplicable Then
                If clsCommon.myCdbl(Rchkregistered.Checked) > 0 Then
                    Dim GSTFinal As String = clsCommon.myCstr(txtGSTStateCode.Text) + clsCommon.myCstr(txtGST_PanCode.Text) + clsCommon.myCstr(txtEntity.Text) + clsCommon.myCstr(txtFixxed.Text) + clsCommon.myCstr(MyTextBox2.Text)
                    txtGSTIN_No_final.Text = GSTFinal
                    clsERPFuncationality.ValidationGSTNO(txtGSTStateCode.Text, txtpan.Text, GSTFinal, Nothing)
                End If
            End If

            'VLC
            If clsCommon.myLen(txtvlcname.Text) <= 0 Then
                If common.clsCommon.MyMessageBoxShow(Me, "VLC Name is blank." + Environment.NewLine + "Do you want to Save/Update VSP without VSP?", "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.No Then
                    pageCus.SelectedPage = RadPageViewPage5
                    txtvlcname.Focus()
                    txtvlcname.Select()
                    Exit Sub
                End If
            End If

            If clsCommon.myLen(txtvlcname.Text) > 0 Then
                If AllowToSave() = False Then
                    Exit Sub
                End If
            End If

            If btnsave.Text = "Save" Then
                If common.clsCommon.MyMessageBoxShow(Me, "Please confirm MCC/BMC name in VLC Detail !", "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                Else
                    pageCus.SelectedPage = RadPageViewPage3
                    fndMcc.Focus()
                    fndMcc.Select()
                    Exit Sub
                End If
            End If

            If btnsave.Text = "Save" Then
                funinsert()
                ''funInsertCharges()
                isLoadCopy = False
            ElseIf btnsave.Text = "Update" Then
                funupdate()
                ''funInsertCharges()
                isLoadCopy = False
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndvendorNo.Value = "" Then
            myMessages.blankValue(Me, "VSP No.", Me.Text)
        ElseIf myMessages.deleteConfirm() Then
            fundelete()

        End If
    End Sub



    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
        isLoadCopy = False
    End Sub


    Private Sub MenuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuImport.Click
        funImport()
    End Sub
    Public Sub funImport()
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "DCS Code", "DCS Name", "DCS Uploader Code", "PAN No", "MCC", "DCS Route Code", "Active", "Created Date", "Loyalty Rate", "Own BMC", "Own BMC Date", "Apply Cow Price", "Apply Cow Price Date", "Head Load", "Head Load Service Basis", "Head Load Rate", "Registration No", "Registration Date", "Registered/PDCS/CLUSTER", "Gender", "Supervisor", "District Code", "Block Code", "Zone Code", "Revenue Village Code", "Grampanchayat Code", "Panchayat Samiti Code", "Vidhan Sabha Code", "Saving Company Bank", "Current Company Bank", "Bank Code 1", "Bank Name 1", "Branch Name 1", "IFSC Code 1", "Account No 1", "Credit Limit 1", "Account Type 1", "Security Charges 1", "Bank Code 2", "Bank Name 2", "Branch Name 2", "IFSC Code 2", "Account No 2", "Credit Limit 2", "Account Type 2", "Security Charges 2") Then
                Dim linno As Integer = 0
                Dim TempNewRecord As Boolean = False
                clsCommon.ProgressBarShow()
                Dim obj As New clsfrmVLCMaster
                Dim arr As New List(Of clsfrmVLCMaster)
                Dim strCode As String = ""
                Dim strName As String = ""
                Dim strUploader_No As String = ""
                Dim strZone As String = ""
                Dim duplicateUploader As String = Nothing
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        linno += 1
                        'If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("DCS Code").Value))) Then
                        '    Throw New Exception("DCS Code cannot be empty" + clsCommon.myCstr(linno) + ".")
                        'Else
                        obj.vlcCode = clsCommon.myCstr(grow.Cells("DCS Code").Value)
                        obj.vlcName = clsCommon.myCstr(grow.Cells("DCS Name").Value)
                        obj.mainvillname = clsCommon.myCstr(grow.Cells("DCS Name").Value)
                        obj.VLC_CODE_VLC_UPLOADER = clsCommon.myCstr(grow.Cells("DCS Uploader Code").Value)

                        Dim Count As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_VLC_MASTER_HEAD where VLC_Code_vlc_uploader='" + clsCommon.myCstr(obj.VLC_CODE_VLC_UPLOADER) + "'", trans))
                        If Count > 0 Then
                            'clsCommon.MyMessageBoxShow("Duplicate DCS Code for DCS Uploader :" + obj.VLC_CODE_VLC_UPLOADER)
                            If clsCommon.myLen(duplicateUploader) > 0 Then
                                duplicateUploader += ", " + clsCommon.myCstr(obj.VLC_CODE_VLC_UPLOADER)
                            Else
                                duplicateUploader = clsCommon.myCstr(obj.VLC_CODE_VLC_UPLOADER)
                            End If
                            Continue For
                        End If
                        'txtpan.Text = clsCommon.myCstr(grow.Cells("PAN No").Value)
                        obj.Gender = clsCommon.myCstr(grow.Cells("Gender").Value)
                        If clsCommon.myCdbl(grow.Cells("Apply Cow Price").Value) = 1 Then
                            obj.Apply_Cow_Price = True
                            obj.ApplyCowPriceDate = clsCommon.myCDate(grow.Cells("Apply Cow Price Date").Value)
                        Else
                            obj.Apply_Cow_Price = False
                            obj.ApplyCowPriceDate = Nothing
                        End If

                        obj.Loyalty_Rate = clsCommon.myCdbl(grow.Cells("Loyalty Rate").Value)
                        If clsCommon.myCstr(grow.Cells("Registered/PDCS/CLUSTER").Value) = "" Then
                            obj.Registered_PDCS_CLUSTER = "PDCS"
                        Else
                            obj.Registered_PDCS_CLUSTER = clsCommon.myCstr(grow.Cells("Registered/PDCS/CLUSTER").Value)
                        End If
                        obj.RegistrationNo = clsCommon.myCstr(grow.Cells("Registration No").Value)
                        obj.RegistrationDate = clsCommon.myCDate(grow.Cells("Registration Date").Value, "dd/MM/yyyy")
                        obj.MCCCOde = clsCommon.myCstr(grow.Cells("MCC").Value)
                        obj.routecode = clsCommon.myCstr(grow.Cells("DCS Route Code").Value)
                        obj.Supervisor = clsCommon.myCstr(grow.Cells("Supervisor").Value)
                        obj.DistrictCode = clsCommon.myCstr(grow.Cells("District Code").Value)
                        obj.BlockCode = clsCommon.myCstr(grow.Cells("Block Code").Value)
                        obj.ZoneCode = clsCommon.myCstr(grow.Cells("Zone Code").Value)
                        obj.RevenueVillageCode = clsCommon.myCstr(grow.Cells("Revenue Village Code").Value)
                        obj.GrampanchayatCode = clsCommon.myCstr(grow.Cells("Grampanchayat Code").Value)
                        obj.PanchayatSamitiCode = clsCommon.myCstr(grow.Cells("Panchayat Samiti Code").Value)
                        obj.VidhanSabhaCode = clsCommon.myCstr(grow.Cells("Vidhan Sabha Code").Value)
                        'If clsCommon.myLen(obj.routecode) > 0 Then
                        '    OpenRouteAccRouteCode(obj.routecode)
                        'End If
                        'txtroutename.Text = clsCommon.myCstr(grow.Cells("DCS Route Name").Value)
                        If clsCommon.myCdbl(grow.Cells("Own BMC").Value) = 1 Then
                            obj.TFOwnBMC = True
                            obj.OwnBMCDate = clsCommon.myCDate(grow.Cells("Own BMC Date").Value)
                        Else
                            obj.TFOwnBMC = False
                            obj.OwnBMCDate = Nothing
                        End If

                        If clsCommon.myCdbl(grow.Cells("Head Load").Value) = 1 Then
                            obj.HeadLoad = True
                            If clsCommon.myCstr(grow.Cells("Head Load Service Basis").Value) = "P" Then
                                obj.HeadLoadBasis = "%(Percentage)"
                            ElseIf clsCommon.myCstr(grow.Cells("Head Load Service Basis").Value) = "K" Then
                                obj.HeadLoadBasis = "Rate/Kg"
                            ElseIf clsCommon.myCstr(grow.Cells("Head Load Service Basis").Value) = "L" Then
                                obj.HeadLoadBasis = "Rate/Ltr"
                            End If
                            obj.HeadLoadRate = clsCommon.myCdbl(grow.Cells("Head Load Rate").Value)
                        Else
                            obj.HeadLoad = False
                            obj.HeadLoadBasis = ""
                            obj.HeadLoadRate = 0
                        End If

                        If clsCommon.myCdbl(grow.Cells("Active").Value) = 1 Then
                            obj.Active = True
                        Else
                            obj.Active = False
                        End If
                        obj.Bank_Code = clsCommon.myCstr(grow.Cells("Bank Code 1").Value)
                        obj.Bank_Name = clsCommon.myCstr(grow.Cells("Bank Name 1").Value)
                        obj.IFSC_Code = clsCommon.myCstr(grow.Cells("IFSC Code 1").Value)
                        obj.Account_No = clsCommon.myCstr(grow.Cells("Account No 1").Value)
                        obj.Account_Type = clsCommon.myCstr(grow.Cells("Account Type 1").Value)
                        obj.Security_Charges = clsCommon.myCDecimal(grow.Cells("Security Charges 1").Value)
                        obj.Bank_Code2 = clsCommon.myCstr(grow.Cells("Bank Code 2").Value)
                        obj.Bank_Name2 = clsCommon.myCstr(grow.Cells("Bank Name 2").Value)
                        obj.IFSC_Code2 = clsCommon.myCstr(grow.Cells("IFSC Code 2").Value)
                        obj.AccNo2 = clsCommon.myCstr(grow.Cells("Account No 2").Value)
                        obj.Account_Type2 = clsCommon.myCstr(grow.Cells("Account Type 2").Value)
                        obj.Security_Charges2 = clsCommon.myCDecimal(grow.Cells("Security Charges 2").Value)
                        obj.Company_Bank = clsCommon.myCstr(grow.Cells("Company Bank").Value)

                        CreateMasterByImport(obj, trans)
                        Dim objVCode As New clsfrmVillageMaster
                        If clsCommon.myLen(objVCode.villcode) > 0 Then
                            obj.mainvillcode = objVCode.villcode
                        Else
                            obj.mainvillcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Village_Code from TSPL_VILLAGE_MASTER where Village_Name='" + obj.mainvillname + "'", trans))
                        End If
                        'End If
                        clsfrmVLCMaster.SaveData(Nothing, True, obj, arr, trans)
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    If clsCommon.myLen(duplicateUploader) > 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Duplicate DCS Uploader Code !" + Environment.NewLine + duplicateUploader + "", Me.Text, MessageBoxButtons.OK)
                    End If
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            'myMessages.myExceptions(ex)
        End Try
    End Sub

    Sub CreateMasterByImport(ByVal obj As clsfrmVLCMaster, ByVal trans As SqlTransaction)
        Dim qry As String = ""
        Dim check As Integer = 0
        Dim coll As New Hashtable()
        Try
            ' Village Master
            qry = "select count(*) from TSPL_VILLAGE_MASTER where Village_Name='" + obj.mainvillname + "'"
            check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
            If check <= 0 Then
                Dim objVillage As New clsfrmVillageMaster
                objVillage.villname = obj.mainvillname
                'objVillage.citycode = fndCity.Value
                'objVillage.statecode = txtstatecode.Value
                'objVillage.countrycode = txtcountrycode.Value
                clsfrmVillageMaster.SaveData(objVillage, True, trans)
                'txtvillcode.Value = objVillage.villcode
                'txtvillname.Text = objVillage.villname
            End If
            '---------------------

            ' Vendor Master 
            Dim StrVdrNo As String = ""
            Dim StrTempVSPName As String = clsCommon.myCstr(obj.vlcName).Replace(" ", "")
            StrTempVSPName = StrTempVSPName.Replace("'", "")
            qry = "select count(*) from TSPL_VENDOR_MASTER Inner Join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code where TSPL_VENDOR_MASTER.Vendor_Name ='" + StrTempVSPName + "' and TSPL_VENDOR_MASTER.form_type='VSP' And TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + clsCommon.myCstr(obj.VLC_CODE_VLC_UPLOADER) + "'"
            check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
            If check <= 0 Then
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Vendor_Name", StrTempVSPName)
                clsCommon.AddColumnsForChange(coll, "Closing_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                'clsCommon.AddColumnsForChange(coll, "State", txtState.Text)
                clsCommon.AddColumnsForChange(coll, "form_type", "VSP")
                'clsCommon.AddColumnsForChange(coll, "state_code", txtstatecode.Value, True)
                'clsCommon.AddColumnsForChange(coll, "City_Code", fndCity.Value, True)
                'clsCommon.AddColumnsForChange(coll, "City_Code_Desc", txtCity.Text, True)
                'clsCommon.AddColumnsForChange(coll, "PC_CODE", fndpaymentCycle.Value, True)
                'clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code", fndgroupcode.Value)
                'clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code_Desc", txtgroupdes.Text)
                clsCommon.AddColumnsForChange(coll, "RegistrationNo", obj.RegistrationNo)
                clsCommon.AddColumnsForChange(coll, "RegistrationDate", obj.RegistrationDate)
                clsCommon.AddColumnsForChange(coll, "Gender", obj.Gender)
                clsCommon.AddColumnsForChange(coll, "DISTRICT_Code", obj.DistrictCode)
                clsCommon.AddColumnsForChange(coll, "Zone_Code", obj.ZoneCode)
                clsCommon.AddColumnsForChange(coll, "BLOCK_CODE", obj.BlockCode)
                clsCommon.AddColumnsForChange(coll, "REVENUE_VILLAGE_CODE", obj.RevenueVillageCode)
                clsCommon.AddColumnsForChange(coll, "GRAMPANCHAYAT_CODE", obj.GrampanchayatCode)
                clsCommon.AddColumnsForChange(coll, "PANCHAYAT_SAMITI_CODE", obj.PanchayatSamitiCode)
                clsCommon.AddColumnsForChange(coll, "VIDHAN_SABHA_CODE", obj.VidhanSabhaCode)
                clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)
                clsCommon.AddColumnsForChange(coll, "Bank_Name", obj.Bank_Name)
                clsCommon.AddColumnsForChange(coll, "IFSC_Code", obj.IFSC_Code)
                clsCommon.AddColumnsForChange(coll, "Account_No", obj.Account_No)
                clsCommon.AddColumnsForChange(coll, "Account_Type", obj.Account_Type)
                clsCommon.AddColumnsForChange(coll, "BankCode2", obj.Bank_Code2)
                clsCommon.AddColumnsForChange(coll, "BankName2", obj.Bank_Name2)
                clsCommon.AddColumnsForChange(coll, "IFSCCode2", obj.IFSC_Code2)
                clsCommon.AddColumnsForChange(coll, "AccNo2", obj.AccNo2)
                clsCommon.AddColumnsForChange(coll, "AccountType2", obj.Account_Type2)
                clsCommon.AddColumnsForChange(coll, "Company_Bank", obj.Company_Bank)
                clsCommon.AddColumnsForChange(coll, "Start_Date", Nothing, True)
                clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                clsCommon.AddColumnsForChange(coll, "is_Head_Load", "F")
                clsCommon.AddColumnsForChange(coll, "Status", "N")
                clsCommon.AddColumnsForChange(coll, "Onhold", "N")
                clsCommon.AddColumnsForChange(coll, "Transporter", "Y")
                clsCommon.AddColumnsForChange(coll, "Currency_Code", objCommonVar.BaseCurrencyCode)
                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                StrVdrNo = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.PTMMASTER, "", "")
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", StrVdrNo)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                obj.vspCode = StrVdrNo
            Else
                obj.vspCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER Inner Join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code where TSPL_VENDOR_MASTER.Vendor_Name ='" + StrTempVSPName + "' and TSPL_VENDOR_MASTER.form_type='VSP' And TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + clsCommon.myCstr(obj.VLC_CODE_VLC_UPLOADER) + "'", trans))
            End If
            '----------------------------
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        Me.Close()
    End Sub
#End Region


    Private Sub frmVSP_VLCMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.U Then
            Dim pwd As New FrmPWD(Nothing)
            pwd.strCode = clsFixedParameterCode.UploadMultipleMasterPwd
            pwd.strType = clsFixedParameterType.UploadMultipleMasterPwd
            pwd.ShowDialog()
            If pwd.isPasswordCorrect Then
                GroupBox2.Visible = True
            Else
                GroupBox2.Visible = False
            End If
        End If
    End Sub

    Private Sub fndvendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndvendorNo._MYValidating
        If isButtonClicked Then
            'Dim qry As String = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Vendor_Group_Code as [Vendor Group],(select case when Status='N' then 'Active' else 'In Active' end ) as [Status] from TSPL_VENDOR_MASTER "

            'fndvendorNo.Value = clsCommon.ShowSelectForm("fmVedrNofnd", qry, "Vendor Code", "", fndvendorNo.Value, "", isButtonClicked)
            fndvendorNo.Value = clsVendorMaster.getFinder(" form_type='VSP'", fndvendorNo.Value, isButtonClicked)

            'fndvendorNo.Value = clsVendorMaster.("", fndvendorNo.Value, isButtonClicked)
            txtvendorname.Text = clsDBFuncationality.getSingleValue("Select group_desc from tspl_vendor_group where ven_group_code='" + fndvendorNo.Value + "'")
            If fndvendorNo.Value IsNot Nothing Then
                btndelete.Enabled = True
            Else
                btndelete.Enabled = False
            End If
            fndvendorNo_text_changed()
        ElseIf fndvendorNo.MyReadOnly OrElse fndvendorNo.Value IsNot Nothing Then
            Dim qry As String = "Select * from TSPL_VENDOR_MASTER where Vendor_Code ='" + fndvendorNo.Value + "' and form_type='" + txtvndrtype.Text + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count <= 0 Then
                ''txtaccsetdes.Text = ""
                txtvendorname.Text = ""
                txtvendornameHindi.Text = ""
                txtvendortypedes.Text = ""
                txtWeb.Text = ""
                ''txtTxGrp.Text = ""
                ''txtTinNo.Text = ""
                LblIncentive.Text = ""
                FndIncentive.Value = Nothing
                ''txttermcodedes.Text = ""
                ''txtStaxNo.Text = ""
                txtState.Text = ""
                ''txtRemarks2.Text = ""
                ''txtRemarks1.Text = ""
                ''txtrange.Text = ""
                txtPhone2.Text = "(+__)__________"
                txtPhone1.Text = "(+__)__________"
                ''txtpaymentcodedes.Text = ""
                txtpan.Text = ""
                ''txtLstNo.Text = ""
                txtfax.Text = ""
                txtgroupdes.Text = ""
                txtEmail.Text = ""
                ''txtecc.Text = ""
                ''txtcst.Text = ""
                txtCredit.Text = ""
                txtCountry.Text = ""
                ''txtContPhone.Text = "(+__)__________"
                ''txtContactWeb.Text = ""
                ''txtContactName.Text = ""
                ''txtContactFax.Text = ""
                txtAdd1.Text = ""
                txtAdd2.Text = ""
                txtAdd3.Text = ""
                ''txtAddInfo1.Text = ""
                ''txtAddInfo2.Text = ""
                ''txtAddInfo3.Text = ""
                txtbankcodedes.Text = ""
                txtbankcodedes2.Text = ""
                txtCity.Text = ""
                txtcollect.Text = ""
                ''txtContactEmail.Text = ""
                chkHold.Checked = False
                chkInActive.Checked = False
                chkInterBranch.Checked = False
                chkTagAsFranchise.Checked = False
                chkIsGrossReceipt.Checked = False
                chktrarns.Checked = False
                fndgroupcode.Value = Nothing
                fndCity.Value = Nothing
                ''ChkHeadLoad.Checked = False
                ''txtRateHeadLoad.Text = Nothing
                ''TxtStandardSec_Amt.Text = Nothing
                ''CmbHeadLoadServiceBasis.SelectedValue = -1
                ''ChkOwnAsset.Checked = False
                ''TxtRateOwnAsset.Text = Nothing
                ''CmbOwnAssetServiceBasis.SelectedValue = -1
                VLC_reset()
                If AllowVSPMasterAutoPrefix = 1 Then
                    fndvendorNo.Value = ""
                End If
                btnsave.Text = "Save"
            Else
                fndvendorNo_text_changed()
            End If
        End If
    End Sub

    Private Sub fndgroupcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndgroupcode._MYValidating
        fndgroupcode.Value = clsVendorGroupMaster.getFinder("", fndgroupcode.Value, isButtonClicked)
        'txtvendorname.Text = clsDBFuncationality.getSingleValue("Select group_desc from tspl_vendor_group where ven_group_code='" + fndgroupcode.Value + "'")
        fndgroupcode_text_Changed()
        fndgroupcode_leave()
        '   End If
    End Sub

    Private Sub fndCity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCity._MYValidating
        fndCity.Value = clsCityMaster.getFinder("", fndCity.Value, isButtonClicked)
        'txtCity.Text = clsDBFuncationality.getSingleValue("Select City_Name from TSPL_CITY_MASTER where City_Code='" + fndCity.Value + "' and state_code='" + txtstatecode.Value + "'")
        If clsCommon.myLen(fndCity.Value) > 0 Then
            Dim obj As clsCityMaster = clsCityMaster.GetData(fndCity.Value, NavigatorType.Current)
            If Not IsNothing(obj) Then
                txtCity.Text = obj.City_Name
                txtstatecode.Value = obj.STATE_CODE
                txtState.Text = obj.STATE_NAME
                txtcountrycode.Value = clsDBFuncationality.getSingleValue("select country_code from tspl_State_master where state_Code='" & txtstatecode.Value & "'")
                txtCountry.Text = clsDBFuncationality.getSingleValue("select country_name from tspl_Country_master where Country_Code='" & txtcountrycode.Value & "'")
            End If
            'txtCityName.Text = clsDBFuncationality.getSingleValue("select  city_name from tspl_city_master where city_code='" & fndCity.Value & "'")
        Else '------25/06/2014 Monika
            txtCity.Text = ""
            fndCity.Value = ""
        End If
        ''fndcity_text_Changed()
        ''fndCity_leave()
        '  End If
    End Sub

    Private Sub fndvendorNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndvendorNo._MYNavigator
        Dim qst As String = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Vendor_Group_Code as [Vendor Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_VENDOR_MASTER    where  2=2 and form_type='VSP'"
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and Vendor_Code in (select min(Vendor_Code) from TSPL_VENDOR_MASTER where Vendor_Code>'" + fndvendorNo.Value + "' and form_type='" + txtvndrtype.Text + "'  ) "
            Case NavigatorType.First
                qst += "and Vendor_Code in (select MIN(Vendor_Code) from TSPL_VENDOR_MASTER where form_type='" + txtvndrtype.Text + "')"
            Case NavigatorType.Last
                qst += "and Vendor_Code in (select Max(Vendor_Code) from TSPL_VENDOR_MASTER where form_type='" + txtvndrtype.Text + "' )"
            Case NavigatorType.Previous
                qst += "and Vendor_Code in (select max(Vendor_Code) from TSPL_VENDOR_MASTER where Vendor_Code<'" + fndvendorNo.Value + "' and form_type='" + txtvndrtype.Text + "'  )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndvendorNo.Value = clsCommon.myCstr(dt.Rows(0)("Vendor Code"))
            txtvendorname.Text = clsCommon.myCstr(dt.Rows(0)("Vendor Name"))
        End If
        'TextChanged()
        If fndvendorNo.Value IsNot Nothing Then
            btndelete.Enabled = True
        Else
            btndelete.Enabled = False

        End If
        fndvendorNo_text_changed()
    End Sub

    Private Sub fndvendortype__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndvendortype._MYValidating
        Dim qry As String = "SELECT  [ven_Type_Code] as [Vendor Type Code],[ven_Type_Desc] as [Description]FROM [TSPL_VENDOR_TYPE_MASTER]"
        fndvendortype.Value = clsCommon.ShowSelectForm("fndvrn", qry, "Vendor Type Code", "", fndvendortype.Value, "", isButtonClicked)

        txtvendortypedes.Text = clsDBFuncationality.getSingleValue("Select ven_Type_Desc from TSPL_VENDOR_TYPE_MASTER where ven_Type_Code='" + fndvendortype.Value + "'")
    End Sub

    Private Sub fndBaseCurrency__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndVendorCurrency._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        fndVendorCurrency.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", fndVendorCurrency.Value, "CURRENCY_CODE", isButtonClicked)

    End Sub

    Private Sub txtcountrycode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcountrycode._MYValidating
        Try
            Dim qry As String = "select country_code as Code,country_name as Country from tspl_country_master"
            txtcountrycode.Value = clsCommon.ShowSelectForm("CNTFND", qry, "Code", "", txtcountrycode.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtcountrycode.Value) > 0 Then
                txtCountry.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + txtcountrycode.Value + "'"))
                txtState.Text = ""
                txtCity.Text = ""
                fndCity.Value = ""
                txtstatecode.Value = ""
            Else
                txtState.Text = ""
                txtCity.Text = ""
                fndCity.Value = ""
                txtCountry.Text = ""
                txtstatecode.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtstatecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtstatecode._MYValidating
        If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Country First", Me.Text)
            txtcountrycode.Focus()
            txtcountrycode.Select()
            Return
        End If

        Try
            Dim qry As String = "select State_code as Code,state_name as State from tspl_state_master"
            txtstatecode.Value = clsCommon.ShowSelectForm("STAFND", qry, "Code", " country_code='" + txtcountrycode.Value + "'", txtcountrycode.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtstatecode.Value) > 0 Then
                txtState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + txtstatecode.Value + "'"))
                txtGSTStateCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GST_State_Code from tspl_state_master where state_code='" + txtstatecode.Value + "'"))
                txtCity.Text = ""
                fndCity.Value = ""
            Else
                txtState.Text = ""
                txtGSTStateCode.Text = ""
                txtCity.Text = ""
                fndCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub



    Private Sub txtno_days_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtno_days.Validating
        Try
            Convert.ToDecimal(txtno_days.Text)
            Errorcontrol.ResetError(txtno_days)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "No. Of Days Should Be Numeric", Me.Text)
            txtno_days.Text = "0"
            txtno_days.Focus()
            txtno_days.Select()
            Errorcontrol.SetError(txtno_days, "No. Of Days Should Be Numeric")
        End Try
    End Sub



    Private Sub txtpayeename_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtpayeename.Validating
        If clsCommon.myLen(txtpayeename.Text) > 0 Then
            txtpayeename.Text = txtpayeename.Text.Replace("'", "`")
        End If
    End Sub

    Private Sub cmbincentive_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbincentive.SelectedValueChanged
        Try
            txtno_days.Text = "0"
            txtno_days.Enabled = False
            If clsCommon.CompairString(cmbincentive.SelectedValue, "Qty") = CompairStringResult.Equal Then
                txtno_days.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbvsppayment_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbvsppayment.SelectedValueChanged
        Try

            If clsCommon.CompairString(cmbvsppayment.SelectedValue, "Different") = CompairStringResult.Equal Then
                'txtpayeename.Enabled = True
                txtjointname.Enabled = True
                'fndVendorCOde.Enabled = True
                ''Grpjoint.Enabled = True
                ''If EnableBankFromMaster = True Then
                ''    texttxtBankCode.Visible = False
                ''    texttxtBankBranchCode.Visible = False
                ''    txtBankCode.Visible = True
                ''    txtBankBranchCode.Visible = True
                ''Else
                ''    texttxtBankCode.Visible = True
                ''    texttxtBankBranchCode.Visible = True
                ''    txtBankCode.Visible = False
                ''    txtBankBranchCode.Visible = False
                ''End If

            Else
                ''texttxtBankCode.Text = ""
                ''texttxtBankBranchCode.Text = ""
                ''txtBankCode.Value = Nothing
                ''txtBankBranchCode.Value = Nothing
                ' txtpayeename.Text = ""
                txtpayeename.Enabled = False
                txtjointname.Text = ""
                txtjointname.Enabled = False
                ''txtBankBranchName.Text = Nothing
                ''txtBankBranchCode.Text = Nothing
                ''txtBankBranchName.Text = Nothing
                ''fndBankCity.Text = Nothing
                ''txtBankCityName.Text = Nothing
                ''fndBankState.Value = Nothing
                ''txtBankStateName.Text = Nothing
                TxtIFSCCode.Text = ""
                ''Grpjoint.Enabled = False
                txtpayeename.Text = txtvendorname.Text
                'fndVendorCOde.Value = ""
                'fndVendorCOde.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub txtWeb_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWeb.Leave
        Try
            If clsCommon.myLen(txtWeb.Text) > 0 Then
                If Not txtWeb.Text.ToUpper().Contains("WWW.") Then
                    clsCommon.MyMessageBoxShow(Me, "Please Enter Web Site In Proper Format.", Me.Text)
                    txtWeb.Focus()
                    txtWeb.Select()
                    txtWeb.Text = ""
                    Errorcontrol.SetError(txtWeb, "Please Enter Web Site In Proper Format.")
                    Return
                Else
                    Errorcontrol.ResetError(txtWeb)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "Please Enter Web Site In Proper Format.", Me.Text)
            txtWeb.Focus()
            txtWeb.Select()
            txtWeb.Text = ""
            Errorcontrol.SetError(txtWeb, "Please Enter Web Site In Proper Format.")
        End Try
    End Sub


    Private Function GetItemType(Optional ByVal istype As Boolean = False) As DataTable
        Dim dt As New DataTable()
        If istype = True Then
            dt.Columns.Add("Code", GetType(String))

            Dim dr As DataRow = dt.NewRow()
            dr("Code") = "New"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Refurnished"
            dt.Rows.Add(dr)
        Else
            dt.Columns.Add("Code", GetType(String))

            Dim dr As DataRow = dt.NewRow()
            dr("Code") = "Yes"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "No"
            dt.Rows.Add(dr)
        End If

        Return dt
    End Function

    Private Sub DtpBillingDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles DtpBillingDate.Leave
        Try
            DtpEndBillingDate.Value = DtpBillingDate.Value.AddDays(2)
        Catch ex As Exception
        End Try
    End Sub



    Private Sub txtjointname_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtjointname.Leave
        'If Not A Matching Format Entered
        If Not Regex.Match(txtjointname.Text, "^[a-zA-Z ]*$", RegexOptions.IgnorePatternWhitespace).Success Then 'Only Letters
            clsCommon.MyMessageBoxShow(Me, "Please Enter Alphabetic Characters Only!") 'Inform User
            txtjointname.Focus() 'Return Focus
            txtjointname.Clear() 'Clear TextBox
            txtpayeename.Text = txtvendorname.Text
        Else
            'txtpayeename.Text = txtvendorname.Text & " and " & txtjointname.Text
            txtpayeename.Text = txtvendorname.Text & IIf(clsCommon.myLen(txtjointname.Text) > 0, " and " & txtjointname.Text, "")
        End If
    End Sub



    Private Sub txtBankCode__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
        Frm_Open = New FrmVendorBankMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.VendorBankMaster)
        Frm_Open.Show()
    End Sub


    Sub GetBankDetails(ByVal isBUttonclicked As Boolean)
        'If clsCommon.myLen(txtBankCode.Value) > 0 Then
        If isBUttonclicked Then
            findfndbankcode.Value = clsVendorBankMaster.GetFinder("", findfndbankcode.Value, isBUttonclicked)
        End If
        If clsCommon.myLen(findfndbankcode.Value) > 0 Then
            Dim obj As clsVendorBankMaster
            obj = clsVendorBankMaster.GetData(findfndbankcode.Value, NavigatorType.Current)
            If Not IsNothing(obj) Then
                txtbankcodedes.Text = obj.Bank_Name
                TxtBankName.Text = obj.Bank_Name
                ' txtBankBranchCode.Value = obj.Branch_Code
                TxtBankBranch.Text = obj.Branch_Name
                'fndBankCity.Value = obj.city_code
                'txtBankCityName.Text = obj.city_name
                'fndBankState.Value = obj.state_code
                'txtBankStateName.Text = obj.state_name
                'TxtIFSCCode.Value = obj.IFSC_Code

            End If
        Else
            '' txtBankBranchName.Text = ""
        End If
        'Else
        'If isBUttonclicked Then
        '    clsCommon.MyMessageBoxShow("Please Select Bank First")
        'End If
        'txtBankCode.Focus()
        'End If
    End Sub

    Sub GetBankDetails2(ByVal isBUttonclicked As Boolean)

        If isBUttonclicked Then
            findfndbankcode2.Value = clsVendorBankMaster.GetFinder("", findfndbankcode2.Value, isBUttonclicked)
        End If
        If clsCommon.myLen(findfndbankcode2.Value) > 0 Then
            Dim obj As clsVendorBankMaster
            obj = clsVendorBankMaster.GetData(findfndbankcode2.Value, NavigatorType.Current)
            If Not IsNothing(obj) Then
                txtbankcodedes2.Text = obj.Bank_Name
                TxtBankName2.Text = obj.Bank_Name
                txtBankBranch2.Text = obj.Branch_Name
                fndbankcode2.Text = findfndbankcode2.Value

            End If
        Else

        End If

    End Sub


    Private Sub txtvendorname_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtvendorname.Leave
        txtpayeename.Text = txtvendorname.Text & IIf(clsCommon.myLen(txtjointname.Text) > 0, " and " & txtjointname.Text, "")
        txtvlcname.Text = txtpayeename.Text
    End Sub

    Private Sub fndpaymentCycle__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndpaymentCycle._MYOpenMasterForm
        Frm_Open = New frmPaymentCycleMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmPaymentCycleMaster)
        Frm_Open.Show()
    End Sub

    Private Sub fndpaymentCycle__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndpaymentCycle._MYValidating
        Try
            GetPaymentCycleData(isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    Sub GetPaymentCycleData(ByVal isButtonClicked As Boolean)
        If isButtonClicked Then
            fndpaymentCycle.Value = clsPaymentCycleMaster.getFinder(IIf(clsCommon.myLen(fndpaymentCycle.Value) > 0, "TSPL_PAYMENT_CYCLE_MASTER.PC_CODE='" & fndpaymentCycle.Value & "'", ""), fndpaymentCycle.Value, isButtonClicked)
        End If
        If clsCommon.myLen(fndpaymentCycle.Value) > 0 Then
            Dim obj As clsPaymentCycleMaster = clsPaymentCycleMaster.GetData(fndpaymentCycle.Value, NavigatorType.Current)
            TxtPaymentCycle.Text = obj.Description
        End If
    End Sub

    Sub GetIIncentiveDetails(ByVal isBUttonclicked As Boolean)
        'If clsCommon.myLen(txtBankCode.Value) > 0 Then
        If isBUttonclicked Then
            FndIncentive.Value = clsIncentiveMaster.GetFinder("", FndIncentive.Value, isBUttonclicked)
        End If
        If clsCommon.myLen(FndIncentive.Value) > 0 Then
            Dim obj As clsIncentiveMaster
            obj = clsIncentiveMaster.GetData(FndIncentive.Value, NavigatorType.Current)
            If Not IsNothing(obj) Then
                LblIncentive.Text = obj.DESCRIPTION
            End If
        Else
            LblIncentive.Text = ""
            FndIncentive.Value = Nothing
        End If
        'Else
        'If isBUttonclicked Then
        '    clsCommon.MyMessageBoxShow("Please Select Bank First")
        'End If
        'txtBankCode.Focus()
        'End If
    End Sub

    Private Sub FndIncentive__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndIncentive._MYOpenMasterForm
        Frm_Open = New frmIncentiveMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmIncentiveMaster)
        Frm_Open.Show()
    End Sub

    Private Sub FndIncentive__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndIncentive._MYValidating
        GetIIncentiveDetails(isButtonClicked)
    End Sub

    Private Sub fndCity__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCity._MYOpenMasterForm
        Frm_Open = New frmCityMaster(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        Frm_Open.SetUserMgmt(clsUserMgtCode.cityMaster)
        Frm_Open.Show()
    End Sub

    Private Sub txtstatecode__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtstatecode._MYOpenMasterForm
        Frm_Open = New frmStateMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmStateMaster)
        Frm_Open.Show()
    End Sub

    Private Sub txtcountrycode__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcountrycode._MYOpenMasterForm
        Frm_Open = New frmCountryMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmCountryMaster)
        Frm_Open.Show()
    End Sub

    Private Sub rmChargesDetails_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim qry As String
            qry = "Select VSP_CODE AS [VSP Code],Charge_CODE AS [Charge Code],isnull(TSPL_Charge_Category.Description,'') As Description,Rate AS EMP From TSPL_MCC_VSP_ChargeCategory_MAPPING LEFT OUTER JOIN TSPL_Charge_Category  on TSPL_Charge_Category.Charge_Cat_Code=TSPL_MCC_VSP_ChargeCategory_MAPPING.Charge_CODE"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "UOM")
        End Try
    End Sub

    Private Sub rmVSPDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'funImport()
    End Sub

    Private Sub rmChargesDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'ImportChargeDetails()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmVSPDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'funExport()
    End Sub

    Private Sub FndMPCode__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        Frm_Open = New FrmMPMaster(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmMPMaster)
        Frm_Open.Show()
    End Sub

    Public Sub Save_Transport_Data()
        Dim str As String = "select count(*) from tspl_Transport_master where Transport_Id='" & clsCommon.myCstr(fndvendorNo.Value) & "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(str)
        If check > 0 Then
            funupdateTransport()
        Else
            funinsertTransport()
        End If
    End Sub

    'Funtion for insertion of data
    Public Sub funinsertTransport()
        Try

            connectSql.RunSp("sp_transportmaster_insert", New SqlParameter("@transid", fndvendorNo.Value), New SqlParameter("@transname", txtvendorname.Text.ToString()), New SqlParameter("@city", txtCity.Text.ToString()), New SqlParameter("@state", txtState.Text.ToString()), New SqlParameter("@pincode", TxtPinCode.Text.ToString()), New SqlParameter("@panno", txtpan.Text.ToString()), New SqlParameter("@phone", txtPhone1.Text.ToString()), New SqlParameter("@add1", txtAdd1.Text.ToString()), New SqlParameter("@add2", txtAdd2.Text.ToString()), New SqlParameter("@email", txtEmail.Text.ToString()), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            ' myMessages.insert()
            btnsave.Text = "Update"
            btndelete.Enabled = True
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'Funtion for updation  of data
    Public Sub funupdateTransport()
        Try
            Dim currentdate As Date = Date.Today
            connectSql.RunSp("sp_transportmaster_update", New SqlParameter("@transid", fndvendorNo.Value), New SqlParameter("@transname", txtvendorname.Text.ToString()), New SqlParameter("@city", txtCity.Text.ToString()), New SqlParameter("@state", txtState.Text.ToString()), New SqlParameter("@pincode", TxtPinCode.Text.ToString()), New SqlParameter("@panno", txtpan.Text.ToString()), New SqlParameter("@phone", txtPhone1.Text.ToString()), New SqlParameter("@add1", txtAdd1.Text.ToString()), New SqlParameter("@add2", txtAdd2.Text.ToString()), New SqlParameter("@email", txtEmail.Text.ToString()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            ' myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'Private Sub TxtIFSCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    If clsCommon.myLen(fndbankcode.Value) > 0 Then
    '        Dim qry As String = "Select Bank_IFSC_Code as IFSCCode,Branch_Name  from TSPL_Vendor_Bank_Branch_Details "
    '        TxtIFSCCode.Value = clsCommon.ShowSelectForm("FormIFSCCode", qry, "IFSCCode", " Bank_Code ='" & fndbankcode.Value & "' ", TxtIFSCCode.Value, "", isButtonClicked)
    '        TxtBankBranch.Text = clsDBFuncationality.getSingleValue("Select Branch_Name from TSPL_Vendor_Bank_Branch_Details where Bank_Code ='" & fndbankcode.Value & "' and Bank_IFSC_Code='" & TxtIFSCCode.Value & "' ")
    '    Else
    '        clsCommon.MyMessageBoxShow("Please select Bank Code first")
    '    End If
    'End Sub

    Private Sub txtBankBranchCode__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        If clsCommon.myLen(IIf(EnableBankFromMaster = True, findfndbankcode.Value, fndbankcode.Text)) > 0 Then
            Dim frm As New FrmVendorBankMaster '= Nothing
            frm.SetUserMgmt(clsUserMgtCode.VendorBankMaster)
            frm.BankCode = IIf(EnableBankFromMaster = True, findfndbankcode.Value, fndbankcode.Text)
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select bank Code", Me.Text)
        End If

    End Sub

    Private Sub txtBankBranchCode2__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        If clsCommon.myLen(IIf(EnableBankFromMaster = True, findfndbankcode2.Value, fndbankcode2.Text)) > 0 Then
            Dim frm As New FrmVendorBankMaster '= Nothing
            frm.SetUserMgmt(clsUserMgtCode.VendorBankMaster)
            frm.BankCode = IIf(EnableBankFromMaster = True, findfndbankcode2.Value, fndbankcode2.Text)
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select bank Code", Me.Text)
        End If

    End Sub


    Private Sub chkMultIncentive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMultIncentive.ToggleStateChanged
        If chkMultIncentive.Checked Then
            txtIncentiveMult.Enabled = True
            FndIncentive.Enabled = False
            FndIncentive.Value = ""
        Else
            txtIncentiveMult.Enabled = False
            txtIncentiveMult.arrValueMember = Nothing
            FndIncentive.Enabled = True
        End If
    End Sub

    Private Sub txtIncentiveMult__My_Click(sender As Object, e As EventArgs) Handles txtIncentiveMult._My_Click
        Dim qry As String = " select INCENTIVE_CODE as Code,DESCRIPTION as Name,INCENTIVE_DATE as Date,INCENTIVE_TYPE as IncentiveType,SCHEME_FOR as [Scheme For],Calc_Type as [Calculation Type],Rate_Type as [Rate Type],Qty_Type as [Quantity Type] from TSPL_INCENTIVE_MASTER_HEAD "
        '' get already selected data
        Dim qrySel As String = "select Vendor_Code,INCENTIVE_CODE from TSPL_VSP_INCENTIVE where Vendor_Code='" & fndvendorNo.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qrySel)
        Dim arr As New ArrayList
        For Each dr As DataRow In dt.Rows
            arr.Add(clsCommon.myCstr(dr.Item("INCENTIVE_CODE")))
        Next
        If txtIncentiveMult.arrValueMember IsNot Nothing AndAlso txtIncentiveMult.arrValueMember.Count <= 0 Then
            txtIncentiveMult.arrValueMember = arr
        End If
        txtIncentiveMult.arrValueMember = clsCommon.ShowMultipleSelectForm("IncenMulSel", qry, "Code", "Name", txtIncentiveMult.arrValueMember, txtIncentiveMult.arrDispalyMember)

    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs)
        Try
            Dim qry As String
            qry = "select count(*) from TSPL_VSP_INCENTIVE"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
                qry = "SELECT VENDOR_CODE as [VSP Code],INCENTIVE_CODE as [Incentive Code] FROM TSPL_VSP_INCENTIVE where VENDOR_CODE in (select Vendor_Code from TSPL_VENDOR_MASTER where form_type='VSP')"
            Else
                qry = "SELECT '' as [VSP Code],'' as [Incentive Code] FROM TSPL_VSP_INCENTIVE"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "UOM")
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs)
        Try
            ImportIncentiveDetails()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ImportIncentiveDetails()
        Dim gvCharges As New RadGridView()
        Me.Controls.Add(gvCharges)
        Dim countDefaultUOM As Integer = 0
        If transportSql.importExcel(gvCharges, "VSP Code", "Incentive Code") Then
            Dim isSaved As Boolean = True
            Dim currentdate As Date = Date.Today
            Dim trans As SqlTransaction = Nothing
            clsCommon.ProgressBarShow()
            Try
                trans = clsDBFuncationality.GetTransactin()
                For i As Integer = 0 To gvCharges.Rows.Count - 1
                    clsDBFuncationality.ExecuteNonQuery("DELETE FROM TSPL_VSP_INCENTIVE where Vendor_Code = '" & clsCommon.myCstr(gvCharges.Rows(i).Cells("VSP CODE").Value) & "'", trans)
                Next
                For Each grow As GridViewRowInfo In gvCharges.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim coll As New Hashtable()

                    Dim strVSPCode As String
                    Dim VSPCode As String = clsCommon.myCstr(grow.Cells("VSP Code").Value)
                    If clsCommon.myLen(VSPCode) >= 0 Then
                        strVSPCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_Code from TSPL_VENDOR_MASTER Where Vendor_Code ='" + VSPCode + "' And Form_Type ='VSP'", trans))
                        If clsCommon.myLen(strVSPCode) <= 0 Then
                            Throw New Exception("VSP Code '" + VSPCode + "' does not exist .Please make its master first at line no '" + LineNo + "'")
                        End If
                    Else
                        Throw New Exception("Please insert VSP code in at line no '" + LineNo + "' ")
                    End If

                    Dim strChargeC As String
                    Dim ChargeCode As String = clsCommon.myCstr(grow.Cells("Incentive Code").Value)
                    If clsCommon.myLen(ChargeCode) > 0 Then
                        strChargeC = clsDBFuncationality.getSingleValue("Select INCENTIVE_CODE from TSPL_INCENTIVE_MASTER_HEAD Where INCENTIVE_CODE='" + ChargeCode + "'", trans)
                        If clsCommon.CompairString(strChargeC, ChargeCode) = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("INCENTIVE CODE '" + ChargeCode + "' at line no '" + LineNo + "' does  not exist")
                        End If
                    Else
                        Throw New Exception("Please insert INCENTIVE CODE at Line No '" + LineNo + "' ")
                    End If

                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", strVSPCode)
                    clsCommon.AddColumnsForChange(coll, "INCENTIVE_CODE", ChargeCode)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_INCENTIVE", OMInsertOrUpdate.Insert, "", trans)

                Next
                If isSaved Then

                    clsCommon.ProgressBarHide()
                    trans.Commit()
                    RadMessageBox.Show("Data Imported Successfully.")
                Else
                    Throw New Exception("Error in Import")
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                RadMessageBox.Show(ex.Message)
            Finally
                Me.Controls.Remove(gvCharges)
            End Try
        End If
    End Sub

    Private Sub TxtPinCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPinCode.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = Chr(Keys.Back) Then e.Handled = True
    End Sub

    Private Sub txtpan_TextChanged(sender As Object, e As EventArgs) Handles txtpan.TextChanged
        Try
            If clsCommon.myLen(txtpan.Text) <= 0 Then
                Exit Sub
            End If

            Dim msg As String = clsERPFuncationality.CheckPanStructure(txtpan.Text, txtvendorname.Text)
            txtGST_PanCode.Text = txtpan.Text
            If clsCommon.myLen(msg) > 0 Then
                pageCus.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                txtpan.Focus()
                txtpan.Select()
                Return
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Function ValidationMultiCurrencyForImport(ByVal strVendorCurrency As String, ByVal strVendorAccountSet As String, ByVal strTaxGroup As String, ByVal strlineNo As String, ByVal trans As SqlTransaction) As Boolean
        '' validation for multicurrency
        If clsCommon.myLen(clsCommon.myCstr(strVendorCurrency)) > 0 Then
            Dim qry As String
            qry = "select CURRENCY_CODE from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" & clsCommon.myCstr(strVendorAccountSet) & "' "
            Dim accCurrCode As String = clsDBFuncationality.getSingleValue(qry, trans).ToString
            If clsCommon.CompairString(accCurrCode, clsCommon.myCstr(strVendorCurrency)) <> CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "Account Set Currency and Vendor Currency must be same in case of Multicurrency. See At Line No :" + strlineNo) ',See At Line No.
                Return False
            End If
            '' match tax Group currency with vendor currency
            qry = " select TSPL_TAX_GROUP_DETAILS.Tax_Code,coalesce(TSPL_TAX_MASTER.CURRENCY_CODE,'') as CURRENCY_CODE from TSPL_TAX_GROUP_DETAILS " &
                  " inner join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code " &
                  " inner join TSPL_TAX_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Code=TSPL_TAX_MASTER.Tax_Code where TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" & clsCommon.myCstr(strTaxGroup) & "' " &
                  " and coalesce(TSPL_TAX_MASTER.CURRENCY_CODE,'')<>'" & clsCommon.myCstr(strVendorCurrency) & "'"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            Dim taxCode As String = ""
            For Each dr As DataRow In dt.Rows
                If dt.Rows.IndexOf(dr) = 0 Then
                    taxCode = dr.Item("Tax_Code")
                Else
                    taxCode = taxCode & "," & dr.Item("Tax_Code")
                End If
            Next
            If clsCommon.myLen(taxCode) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Tax Code '" & taxCode & "' in Tax Group " & clsCommon.myCstr(strTaxGroup) & " are created for currency other than " & clsCommon.myCstr(strVendorCurrency) & " .See At Line No :" + strlineNo)
                Return False
            End If
            'End If
            Return True
        End If
    End Function

    Private Sub chkCreditLimitBasedOnMilkReceipt_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkCreditLimitBasedOnMilkReceipt.ToggleStateChanged
        TxtCreditLimitBasedOnMilkReceipt.Enabled = chkCreditLimitBasedOnMilkReceipt.Checked
    End Sub

    Private Sub fndCusgrp__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCusgrp._MYValidating
        Dim qry As String = " SELECT Cust_Group_Code as [CustomerGruopCode],Cust_Group_Desc as [Description]," &
                    " Tax_Group as [Tax Group],Cust_Account as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_CUSTOMER_GROUP_MASTER] "
        fndCusgrp.Value = clsCommon.ShowSelectForm("CUSGRP_CODE1", qry, "CustomerGruopCode", "", fndCusgrp.Value, "", isButtonClicked)
    End Sub


    Private Sub findfndbankcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles findfndbankcode._MYValidating
        GetBankDetails(isButtonClicked)
    End Sub


    Private Sub findTxtIFSCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles findTxtIFSCCode._MYValidating
        If clsCommon.myLen(findfndbankcode.Value) > 0 Then
            Dim qry As String = "Select Bank_IFSC_Code as IFSCCode,Branch_Name  from TSPL_Vendor_Bank_Branch_Details "
            findTxtIFSCCode.Value = clsCommon.ShowSelectForm("FormIFSCCode", qry, "IFSCCode", " Bank_Code ='" & findfndbankcode.Value & "' ", findTxtIFSCCode.Value, "", isButtonClicked)
            TxtBankBranch.Text = clsDBFuncationality.getSingleValue("Select Branch_Name from TSPL_Vendor_Bank_Branch_Details where Bank_Code ='" & findfndbankcode.Value & "' and Bank_IFSC_Code='" & findTxtIFSCCode.Value & "' ")
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select Bank Code first", Me.Text)
        End If
    End Sub

    Private Sub findTxtIFSCCode2__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles findTxtIFSCCode2._MYValidating
        If clsCommon.myLen(findfndbankcode2.Value) > 0 Then
            Dim qry As String = "Select Bank_IFSC_Code as IFSCCode,Branch_Name  from TSPL_Vendor_Bank_Branch_Details "
            findTxtIFSCCode2.Value = clsCommon.ShowSelectForm("FormIFSCCode", qry, "IFSCCode", " Bank_Code ='" & findfndbankcode2.Value & "' ", findTxtIFSCCode2.Value, "", isButtonClicked)
            txtBankBranch2.Text = clsDBFuncationality.getSingleValue("Select Branch_Name from TSPL_Vendor_Bank_Branch_Details where Bank_Code ='" & findfndbankcode2.Value & "' and Bank_IFSC_Code='" & findTxtIFSCCode2.Value & "' ")
            txtIFSCCode2.Text = findTxtIFSCCode2.Value
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select Bank Code first", Me.Text)
        End If
    End Sub



    Private Sub fndVSPCopy__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndVSPCopy._MYValidating
        Try
            isLoadCopy = True
            fndVSPCopy.Value = clsVendorMaster.getFinder(" form_type='VSP'", fndVSPCopy.Value, isButtonClicked)
            If clsCommon.myLen(fndVSPCopy.Value) > 0 Then
                fndvendorNo.Value = fndVSPCopy.Value
                fndvendorNo_text_changed()
                fndvendorNo.Value = ""
                txtvendorname.Text = ""
                fndvlccode.Text = ""
                txtvlcname.Text = ""
                txtvspcode.Value = ""
                chkApplyCowPrice.Checked = False
                txtvsp.Text = ""
            Else
                funreset()
                isLoadCopy = False
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    Private Sub MCCLOCATIONFINDER()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                If clsCommon.myLen(fndMcc.Value) <= 0 AndAlso Not obj.Default_HO Then
                    fndMcc.Value = IIf(obj.Default_LocName = "_", "", obj.Default_LocCode)
                End If
                arrLoc = obj.arrLocCodes
            Else
                'cmbmcc.Enabled = False
                fndMcc.Enabled = False
                Throw New Exception("Please Set Default Location Of LogIn User")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtvlcname.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Fill VLC Name", Me.Text)
                pageCus.SelectedPage = RadPageViewPage5
                txtvlcname.Focus()
                txtvlcname.Select()
                Errorcontrol.SetError(txtvlcname, "Please Fill VLC Name")
                Return False
            Else
                Errorcontrol.ResetError(txtvlcname)
            End If
            If txtVLCCodeVlcUploader.Enabled = True Then
                If clsCommon.myLen(txtVLCCodeVlcUploader.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Fill VLC Code For VLC Uploader ", Me.Text)
                    pageCus.SelectedPage = RadPageViewPage1
                    txtVLCCodeVlcUploader.Focus()
                    txtVLCCodeVlcUploader.Select()
                    Errorcontrol.SetError(txtVLCCodeVlcUploader, "Please Fill VLC Code For VLC Uploader")
                    Return False
                Else
                    Errorcontrol.ResetError(txtVLCCodeVlcUploader)
                End If
            End If

            If isDuplicateVLCCode(IIf(clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal, False, True)) Then
                clsCommon.MyMessageBoxShow(Me, "Duplicate DCS Code for DCS Uploader", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                txtVLCCodeVlcUploader.Focus()
                Errorcontrol.SetError(txtVLCCodeVlcUploader, "Duplicate DCS Code for DCS Uploader")
                Return False
            Else
                Errorcontrol.SetError(txtVLCCodeVlcUploader, "")
            End If

            If objCommonVar.ApplyDefaultsInMaster = False OrElse (objCommonVar.ApplyDefaultsInMaster = True AndAlso clsCommon.CompairString(btnsave.Text, "Update") = CompairStringResult.Equal) Then
                If clsCommon.myLen(txtvillcode.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Select Village Code/Name", Me.Text)
                    pageCus.SelectedPage = RadPageViewPage5
                    txtvillcode.Focus()
                    txtvillcode.Select()
                    Errorcontrol.SetError(txtvillcode, "Please Select Village Code/Name")
                    Return False
                Else
                    Errorcontrol.ResetError(txtvillcode)
                End If
            End If

            ''Create Mcc Master
            'If chkOwnBMC.Checked = True Then
            '    Try
            '        If clsCommon.myLen(txtMCCOwnBMC.Value) <= 0 Then
            '            Dim qry As String = " select count (*)  from TSPL_MCC_MASTER where Mcc_Code_VLC_Uploader = '" + txtVLCCodeVlcUploader.Text + "'  "
            '            Dim checkValid As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry))
            '            If checkValid = False Then

            '                dtDefault = New DataTable()
            '                Dim newBlankRow1 As DataRow = dtDefault.NewRow
            '                dtDefault.Rows.Add(newBlankRow1)
            '                Dim objDefaultTemplate As clsExportTemplate = clsExportTemplate.GetDefaultData("MP-IMP-TMP")
            '                If (objDefaultTemplate IsNot Nothing AndAlso clsCommon.myLen(objDefaultTemplate.Export_Code) > 0) Then
            '                    If objDefaultTemplate.Arr IsNot Nothing AndAlso objDefaultTemplate.Arr.Count > 0 Then
            '                        For Each objTr As clsExportTemplateDetail In objDefaultTemplate.Arr
            '                            If clsCommon.myLen(objTr.Column_Name) > 0 Then
            '                                arrExistCols.Add(objTr.Column_Name)
            '                                Dim newColumn As New DataColumn(clsCommon.myCstr(objTr.Column_Name), GetType(System.String))
            '                                dtDefault.Columns.Add(newColumn)
            '                                dtDefault.Rows(0).Item(clsCommon.myCstr(objTr.Column_Name)) = clsCommon.myCstr(objTr.Column_Header)
            '                            End If
            '                        Next
            '                    End If
            '                End If

            '                If (dtDefault IsNot Nothing AndAlso clsCommon.myLen(dtDefault.Rows.Count) > 0) Then
            '                    CreateNewMCC(txtVLCCodeVlcUploader.Text)
            '                Else
            '                    Throw New Exception("Please set Default Templete to create MCC Master")
            '                End If
            '            End If
            '        End If
            '    Catch ex As Exception
            '        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "VSP/VLC Master")
            '        Return False
            '    End Try
            'End If

            If clsCommon.myLen(fndMcc.Value) <= 0 AndAlso chkOwnBMC.Checked = False Then
                clsCommon.MyMessageBoxShow(Me, "Please Select MCC", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                fndMcc.Focus()
                fndMcc.Select()
                Errorcontrol.SetError(fndMcc, "Please Select MCC")
                Return False
            Else
                Errorcontrol.ResetError(fndMcc)
            End If

            'If clsCommon.myLen(txtvspcode.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Select VSP Code/Name", Me.Text)
            '    txtvspcode.Focus()
            '    txtvspcode.Select()
            '    Errorcontrol.SetError(txtvspcode, "Please Select VSP Code/Name")
            '    Return False
            'Else
            '    Errorcontrol.ResetError(txtvspcode)
            'End If

            '-----------check whether the same VSP mapped earlier with the Other VLC----------------------------------------------------------------------------
            Dim check As String = ""
            check = clsDBFuncationality.getSingleValue("select VLC_NAME from TSPL_VLC_MASTER_HEAD where vsp_code='" + txtvspcode.Value + "' and vlc_code<>'" & fndvlccode.Text & "'")

            If clsCommon.myLen(check) > 0 AndAlso clsCommon.CompairString(check, txtvspcode.Value) <> CompairStringResult.Equal Then
                txtvspcode.Value = ""
                txtvspcode.Text = ""
                txtvspcode.Focus()
                txtvspcode.Select()
                Errorcontrol.SetError(txtvspcode, "Selected VSP Already Mapped With VLC " + check + "," + Environment.NewLine + "Please Select The Same VLC As Set Earlier Or" + Environment.NewLine + "Changed Mapping In Old Record First")
                Throw New Exception("Selected VSP Already Mapped With VLC " + check + "," + Environment.NewLine + "Please Select The Same VLC As Set Earlier Or" + Environment.NewLine + "Changed Mapping In Old Record First")
            Else
                Errorcontrol.ResetError(txtvspcode)
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function
    Function isDuplicateVLCCode(ByVal isUpdate As Boolean) As Boolean

        'Dim qry As String = "select COUNT(*) from TSPL_VLC_MASTER_HEAD where VLC_Code_vlc_uploader='" & txtVLCCodeVlcUploader.Text & "' and vlc_code<>'" & fndvlccode.Text & "' and mcc='" & fndMcc.Value & "'"
        Dim qry As String = Nothing
        If btnsave.Text.Contains("Save") Then
            qry = "select COUNT(*) from TSPL_VLC_MASTER_HEAD where VLC_Code_vlc_uploader='" & txtVLCCodeVlcUploader.Text & "'"
        Else
            qry = "select COUNT(*) from TSPL_VLC_MASTER_HEAD where VLC_Code_vlc_uploader='" & txtVLCCodeVlcUploader.Text & "' and vlc_code<>'" & fndvlccode.Text & "' and mcc='" & fndMcc.Value & "'"
        End If

        Dim rvalue As Boolean = False
        Dim cnt As Integer = 0
        cnt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If cnt >= 1 Then
            rvalue = True
            'ElseIf (Not isUpdate) And cnt >= 1 Then
            '    rvalue = True
        Else
            rvalue = False
        End If
        Return rvalue
    End Function

    Private Sub fndMcc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMcc._MYValidating
        If chkOwnBMC.Checked = False Then
            Dim StrWhere As String = ""
            Dim qry As String = "select tspl_mcc_master.mcc_code as Code,tspl_mcc_master.mcc_name as Name,tspl_mcc_master.Plant_Code as [Plant Code],TSPL_LOCATION_MASTER.Location_Desc AS [Plant Name] from tspl_mcc_master left outer join tspl_location_master on tspl_location_master.location_code=tspl_mcc_master.plant_code"
            If clsCommon.myLen(arrLoc) > 0 Then
                StrWhere = " tspl_mcc_master.mcc_code in (" + arrLoc + ")"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Before Doing VLC Master Entry,Make MCC Master", Me.Text)
                Reset()
            End If

            fndMcc.Value = clsCommon.ShowSelectForm("MCCFND", qry, "Code", StrWhere, fndMcc.Value, "Code", isButtonClicked)
            lblMCCName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select MCC_NAME from tspl_MCC_MASTER where MCC_Code = '" + fndMcc.Value + "' "))
        End If
    End Sub

    Private Sub fndPriceCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPriceCode._MYValidating
        fndPriceCode.Value = clsfrmVLCMaster.getPriceCodeforVlc(fndvlccode.Text, fndMcc.Value, True, Nothing)
    End Sub

    Private Sub txtvillcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtvillcode._MYValidating
        Dim qry As String = "select TSPL_VILLAGE_MASTER.Village_Code as Code,TSPL_VILLAGE_MASTER.Village_Name as [Village Name],(TSPL_VILLAGE_MASTER.Add1+' '+TSPL_VILLAGE_MASTER.Add2) as Address,TSPL_CITY_MASTER.City_Name as [City],TSPL_STATE_MASTER.STATE_NAME as [State],TSPL_VILLAGE_MASTER.PIN_NO as [Pin Code] from TSPL_VILLAGE_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_VILLAGE_MASTER.City_Code left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_VILLAGE_MASTER.State_Code"
        txtvillcode.Value = clsCommon.ShowSelectForm("VILFND", qry, "Code", "", txtvillcode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtvillcode.Value) > 0 Then
            txtvillname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select village_name from tspl_village_master where village_code='" + txtvillcode.Value + "'"))
        Else
            txtvillcode.Value = ""
            txtvillname.Text = ""
        End If
    End Sub

    Private Sub txtroutecode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtroutecode._MYValidating
        If clsCommon.myLen(fndMcc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select MCC", Me.Text)
            fndMcc.Focus()
            fndMcc.Select()
            Exit Sub
        End If

        OpenRoute()
    End Sub

    Sub OpenRoute()
        'Dim qry As String = "select TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as [Route Description] from TSPL_MCC_ROUTE_MASTER"
        'If clsCommon.myLen(arrLoc) > 0 Then
        '    qry += " where TSPL_MCC_ROUTE_MASTER.mcc_code in (" + arrLoc + ")"
        'End If
        'qry += "  and coalesce(active,1)=1 and TSPL_MCC_ROUTE_MASTER.mcc_code='" + fndMcc.Value + "'"
        Dim qry As String = "select ROUTE_NO as Code,ROUTE_NAME as Description from TSPL_BULK_ROUTE_MASTER"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ROTFND1", qry)

        If dr IsNot Nothing Then
            txtroutecode.Value = clsCommon.myCstr(dr("Code"))
            txtroutename.Text = clsCommon.myCstr(dr("Description"))
        Else
            txtroutecode.Value = ""
            txtroutename.Text = ""
        End If
    End Sub

    Sub OpenRouteAccRouteCode(ByVal strRouteCode As String)
        Dim qry As String = "select ROUTE_NO as Code,ROUTE_NAME as Description from TSPL_BULK_ROUTE_MASTER where ROUTE_NO='" + strRouteCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt.Rows.Count > 0 Then
            'txtroutecode.Value = clsCommon.myCstr(dr("Code"))
            txtroutename.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            'txtroutecode.Value = ""
            txtroutename.Text = ""
        End If
    End Sub

    Private Sub Rchkregistered_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles Rchkregistered.ToggleStateChanged
        Try
            If Rchkregistered.Checked Then
                txtGSTIN_No_final.Enabled = False
                txtEntity.Enabled = True
                MyTextBox2.Enabled = True
                txtGST_PanCode.Text = txtpan.Text
                If clsCommon.myLen(txtstatecode.Value) > 0 Then
                    txtGSTStateCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GST_State_Code from tspl_state_master where state_code='" + txtstatecode.Value + "'"))
                End If
            Else
                txtEntity.Enabled = False
                MyTextBox2.Enabled = False
                txtGST_PanCode.Text = ""
                txtEntity.Text = ""
                txtGSTStateCode.Text = ""
                txtGSTIN_No_final.Text = ""
                MyTextBox2.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message(), Me.Text)
        End Try
    End Sub

    Private Sub btnExportMultipleMaster_Click(sender As Object, e As EventArgs) Handles btnExportMultipleMaster.Click
        Try
            Dim qry As String = ""
            Dim settApplyEffectiveStartDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyEffectiveStartDate, clsFixedParameterCode.ApplyEffectiveStartDate, Nothing)) > 0, True, False)
            If settApplyEffectiveStartDate = True Then
                qry = "select '' as [MCC Code],'' as [MCC Name],'' as [Route Code],'' as [Route Name],'' as [Route Distance],'' as [Route Effective Start Date],'' as [Vehicle],'' as [Vehicle payment basis],'' as [Payment Per Km],'' as [Vehicle Effective Start Date],'' as [Transporter Code],'' as [Transporter Name],'' as [Transporter group code],'' as [VLC Code],'' as [VLC Name],'' as [VLC Uploader Code],'' as [Village Name],'' as [VSP Code],'' as [VSP Name],'' as [VSP Address],'' as [State],'' as [VSP Group Code],'' as [Create customer],'' as [Customer Group Code],'' as [VSP Payment type],'' as [Bank Code],'' as [Bank Name],'' as [IFSC Code],'' as [Branch Name],'' as [Account No],'' as [Buffalow TIP],'' as [Cow TIP]"
            Else
                qry = "select '' as [MCC Code],'' as [MCC Name],'' as [Route Code],'' as [Route Name],'' as [Route Distance],'' as [Vehicle],'' as [Vehicle payment basis],'' as [Payment Per Km],'' as [Transporter Code],'' as [Transporter Name],'' as [Transporter group code],'' as [VLC Code],'' as [VLC Name],'' as [VLC Uploader Code],'' as [Village Name],'' as [VSP Code],'' as [VSP Name],'' as [VSP Address],'' as [State],'' as [VSP Group Code],'' as [Create customer],'' as [Customer Group Code],'' as [VSP Payment type],'' as [Bank Code],'' as [Bank Name],'' as [IFSC Code],'' as [Branch Name],'' as [Account No],'' as [Buffalow TIP],'' as [Cow TIP]"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Upload Multiple Master", Me.Text)
        End Try
    End Sub

    Private Sub btnImportMultipleMaster_Click(sender As Object, e As EventArgs) Handles btnImportMultipleMaster.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim LineNo As Integer = 0
        Try
            Dim settApplyEffectiveStartDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyEffectiveStartDate, clsFixedParameterCode.ApplyEffectiveStartDate, Nothing)) > 0, True, False)
            Dim Count As String = """"
            ''Dim checkPan As New System.Text.RegularExpressions.Regex("^([A-Z]){5}([0-9]){4}([A-Z]){1}")
            Dim inputs() As String = {}
            If settApplyEffectiveStartDate = True Then
                inputs = {"MCC Code", "MCC Name", "Route Code", "Route Name", "Route Distance", "Route Effective Start Date", "Vehicle", "Vehicle payment basis", "Payment Per Km", "Vehicle Effective Start Date", "Transporter Code", "Transporter Name", "Transporter group code", "VLC Code", "VLC Name", "VLC Uploader Code", "Village Name", "VSP Code", "VSP Name", "VSP Address", "State", "VSP Group Code", "Create customer", "Customer Group Code", "VSP Payment type", "Bank Code", "Bank Name", "IFSC Code", "Branch Name", "Account No", "Buffalow TIP", "Cow TIP"}
            Else
                inputs = {"MCC Code", "MCC Name", "Route Code", "Route Name", "Route Distance", "Vehicle", "Vehicle payment basis", "Payment Per Km", "Transporter Code", "Transporter Name", "Transporter group code", "VLC Code", "VLC Name", "VLC Uploader Code", "Village Name", "VSP Code", "VSP Name", "VSP Address", "State", "VSP Group Code", "Create customer", "Customer Group Code", "VSP Payment type", "Bank Code", "Bank Name", "IFSC Code", "Branch Name", "Account No", "Buffalow TIP", "Cow TIP"}
            End If

            Dim Strs As List(Of String) = New List(Of String)(inputs)
            If transportSql.importExcel(gv, Strs.ToArray()) Then
                Dim trans As SqlTransaction = Nothing
                Try
                    clsCommon.ProgressBarPercentShow()
                    Dim counter As Integer = 1
                    Dim IsBlacklisted As Integer = 0
                    If clsCommon.myLen(objCommonVar.BaseCurrencyCode) <= 0 Then
                        Throw New Exception("Please set currency code in company master")
                    End If
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        LineNo += 1
                        trans = clsDBFuncationality.GetTransactin()
                        ''Primary Transport Master
                        Dim strvendorNo As String = String.Empty
                        Dim strvendorname1 As String = String.Empty
                        Dim strvendorname As String = String.Empty
                        Dim StrVdrNo As String = String.Empty
                        Dim check As Integer = 0
                        Dim i2 As Integer = 0
                        Dim sql1 As String

                        Dim coll As New Hashtable()
                        Dim strBrachName As String = String.Empty
                        Dim strIFSCCode As String = String.Empty
                        Dim strbank As String = String.Empty
                        Dim strBankCode2 As String = String.Empty
                        Dim qry As String = Nothing
                        Dim statecode As String = String.Empty
                        Dim state As String = String.Empty
                        Dim country As String = String.Empty
                        Dim closing_date As String = String.Empty
                        Dim strgroupCode As String = String.Empty
                        Dim strgroupDes As String = String.Empty
                        Dim CityCode As String = String.Empty
                        Dim CityName As String = String.Empty
                        Dim PC_CODE As String = String.Empty
                        Dim StrTempVSPName As String = String.Empty
                        Try
                            StrTempVSPName = clsCommon.myCstr(grow.Cells("VSP Name").Value).Replace(" ", "")
                            StrTempVSPName = StrTempVSPName.Replace("'", "")
                            strvendorNo = clsCommon.myCstr(grow.Cells("Transporter Code").Value)
                            If strvendorNo.Length > 12 Then
                                Throw New Exception("Check the length of Transporter Code,")
                            End If

                            strvendorname1 = clsCommon.myCstr(grow.Cells("Transporter Name").Value)
                            strvendorname = strvendorname1.Replace("'", "''")
                            If strvendorname.Length > 100 Then
                                Throw New Exception("Length of Transporter Name can not be greater than 100.,")
                            End If
                            If String.IsNullOrEmpty(strvendorname) AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                strvendorname = StrTempVSPName
                                grow.Cells("Transporter Code").Value = StrTempVSPName
                                grow.Cells("Transporter Name").Value = StrTempVSPName
                            End If
                            If String.IsNullOrEmpty(strvendorname) Then
                                Throw New Exception("Transporter Name can not be blank")
                            End If
                            closing_date = System.DateTime.Now.Date

                            strgroupCode = clsCommon.myCstr(grow.Cells("Transporter group code").Value)
                            If String.IsNullOrEmpty(strgroupCode) Then
                                Throw New Exception(" Transporter group code can not be blank")
                            End If
                            Dim i As Integer
                            qry = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                            i = connectSql.RunScalar(trans, qry)
                            If i = 0 Then
                                Throw New Exception("Vendor group code does not exist : " + strgroupCode + "")
                            Else
                            End If
                            If strgroupCode.Length > 12 Then
                                Throw New Exception("Check the length of Group Code")
                            End If
                            strgroupDes = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  group_Desc from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'", trans))


                            statecode = clsCommon.myCstr(grow.Cells("State").Value)
                            check = 0

                            If clsCommon.myLen(statecode) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                statecode = clsCommon.myCstr(clsStateMaster.GetDefault(trans))
                            End If
                            If clsCommon.myLen(statecode) > 0 Then
                                qry = "select STATE_CODE,STATE_NAME,COUNTRY_CODE from tspl_state_master where  state_code='" + statecode + "'"
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                    Throw New Exception("State Code Does Not Exist,Please Make Its Master First")
                                End If
                                statecode = clsCommon.myCstr(dt.Rows(0)("STATE_CODE"))
                                state = clsCommon.myCstr(dt.Rows(0)("STATE_NAME"))
                                country = clsCommon.myCstr(dt.Rows(0)("COUNTRY_CODE"))
                            End If
                            grow.Cells("State").Value = statecode

                            strbank = clsCommon.myCstr(grow.Cells("Bank Code").Value)

                            If strbank.Length > 30 Then
                                Throw New Exception("Check the length of bank code")
                            End If

                            strIFSCCode = clsCommon.myCstr(grow.Cells("IFSC Code").Value)


                            strBrachName = clsCommon.myCstr(grow.Cells("Branch Name").Value)
                            If clsCommon.myLen(strBrachName) > 100 Then
                                Throw New Exception("Branch Name should be max 100 character")
                            End If




                            If objCommonVar.ApplyDefaultsInMaster = True Then
                                CityCode = clsCommon.myCstr(clsCityMaster.GetDefault(trans))
                                CityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select City_Name from TSPL_CITY_MASTER where City_Code='" + CityCode + "'", trans))
                                PC_CODE = clsCommon.myCstr(clsPaymentCycleMaster.GetDefault(trans))
                            End If

                            coll = New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Vendor_Name", strvendorname)
                            clsCommon.AddColumnsForChange(coll, "Closing_Date", closing_date)
                            clsCommon.AddColumnsForChange(coll, "State", state)
                            clsCommon.AddColumnsForChange(coll, "Country", country)
                            clsCommon.AddColumnsForChange(coll, "form_type", "PTM")
                            clsCommon.AddColumnsForChange(coll, "state_code", statecode, True)
                            clsCommon.AddColumnsForChange(coll, "City_Code", CityCode, True)
                            clsCommon.AddColumnsForChange(coll, "City_Code_Desc", CityName, True)
                            clsCommon.AddColumnsForChange(coll, "PC_CODE", PC_CODE, True)
                            clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code", strgroupCode)
                            clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code_Desc", strgroupDes)
                            'clsCommon.AddColumnsForChange(coll, "branch_code", strIFSCCode)
                            'clsCommon.AddColumnsForChange(coll, "Branch_Name", strBrachName)
                            'clsCommon.AddColumnsForChange(coll, "Account_No", clsCommon.myCstr(grow.Cells("Account No").Value))
                            'clsCommon.AddColumnsForChange(coll, "IFSC_Code", strIFSCCode)
                            clsCommon.AddColumnsForChange(coll, "Start_Date", Nothing, True)
                            clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                            clsCommon.AddColumnsForChange(coll, "is_Head_Load", "F")
                            clsCommon.AddColumnsForChange(coll, "Status", "N")
                            clsCommon.AddColumnsForChange(coll, "Onhold", "N")
                            clsCommon.AddColumnsForChange(coll, "Transporter", "Y")
                            ' clsCommon.AddColumnsForChange(coll, "Bank_Code", strbank)
                            clsCommon.AddColumnsForChange(coll, "Currency_Code", objCommonVar.BaseCurrencyCode)
                            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                            sql1 = "select count(*) from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + strvendorname + "' and form_type='PTM' "
                            i2 = CInt(connectSql.RunScalar(trans, sql1))

                            StrVdrNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + strvendorname + "' and form_type='PTM'", trans))

                            If (i2 = 0) Then
                                StrVdrNo = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.PTMMASTER, "", "")
                                clsCommon.AddColumnsForChange(coll, "Vendor_Code", StrVdrNo)
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Update, "vendor_code='" + StrVdrNo + "' and form_type='PTM'", trans)
                            End If
                            trans.Commit()

                        Catch ex As Exception
                            trans.Rollback()
                            Throw New Exception(ex.Message)
                        End Try
                        ''end of Primary Transporter Master

                        ''Primary Transporter Vehiclee Master
                        trans = Nothing
                        Dim obj As clsfrmPrimaryTransporterVehicalMaster

                        obj = New clsfrmPrimaryTransporterVehicalMaster()
                        Dim index As Integer = 0

                        obj.docno = clsCommon.myCstr(grow.Cells("Vehicle").Value)
                        If clsCommon.myLen(obj.docno) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                            grow.Cells("Vehicle").Value = StrTempVSPName
                            obj.docno = StrTempVSPName
                            obj.primarycode = StrVdrNo
                            obj.primaryname = StrTempVSPName
                        Else
                            obj.primarycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + strvendorname + "'", trans))
                            obj.primaryname = strvendorname
                        End If

                        If clsCommon.myLen(obj.docno) <= 0 Then
                            Throw New Exception("Please Fill Vehicle No.(Code) At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.docno) > 30 Then
                            Throw New Exception("Length of Vehicle No.(Code) Should Not Exceed 30 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.docno) > 0 Then
                            index = obj.docno.IndexOf(" ")
                            If index > 0 AndAlso index < clsCommon.myLen(obj.docno) Then
                                Throw New Exception("There Should Be No white Space Between Vehicle No. At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If


                        If clsCommon.myLen(obj.primarycode) <= 0 AndAlso clsCommon.myLen(obj.primaryname) <= 0 Then
                            Throw New Exception("Please Fill Primary Transporter Code/Name At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.primarycode) > 0 Then
                            qry = "select count(*) from tspl_vendor_master where vendor_code='" + obj.primarycode + "' and form_type='PTM'"
                            index = clsDBFuncationality.getSingleValue(qry, trans)

                            If index <= 0 Then
                                qry = "select vendor_code from tspl_vendor_master where vendor_name='" + obj.primaryname + "' and form_type='PTM'"
                                obj.primarycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                                If obj.primarycode IsNot Nothing AndAlso clsCommon.myLen(obj.primarycode) <= 0 Then
                                    Throw New Exception("Filled Primary Transporter Code Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If
                        ElseIf clsCommon.myLen(obj.primaryname) > 0 Then
                            qry = "select vendor_code from tspl_vendor_master where vendor_name='" + obj.primaryname + "' and form_type='PTM'"
                            obj.primarycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                            If obj.primarycode IsNot Nothing AndAlso clsCommon.myLen(obj.primarycode) <= 0 Then
                                Throw New Exception("Filled Primary Transporter Code/Name Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        '-------------------------------------------------------------

                        obj.mcccode = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                        obj.mccname = clsCommon.myCstr(grow.Cells("MCC Name").Value).Replace("'", "`")
                        If clsCommon.myLen(obj.mcccode) <= 0 AndAlso clsCommon.myLen(obj.mccname) <= 0 Then
                            Throw New Exception("Please Fill MCC Code/Name At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.mcccode) > 0 Then
                            qry = "select count(*) from tspl_mcc_master where mcc_code='" + obj.mcccode + "'"
                            index = clsDBFuncationality.getSingleValue(qry, trans)

                            If index <= 0 Then
                                qry = "select mcc_code from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
                                obj.mcccode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                                If obj.mcccode IsNot Nothing AndAlso clsCommon.myLen(obj.mcccode) <= 0 Then
                                    Throw New Exception("Filled MCC Code Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If
                        ElseIf clsCommon.myLen(obj.mccname) > 0 Then
                            qry = "select mcc_code from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
                            obj.mcccode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                            If obj.mcccode IsNot Nothing AndAlso clsCommon.myLen(obj.mcccode) <= 0 Then
                                Throw New Exception("Filled MCC Code/Name Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        '---------------------

                        '------------check primary transporter mapped with other mcc-----------------
                        Dim checkmcccode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_code from tspl_primary_vehicle_master where vendor_code='" + obj.primarycode + "'", trans))
                        If clsCommon.myLen(checkmcccode) > 0 AndAlso clsCommon.CompairString(checkmcccode, obj.mcccode) <> CompairStringResult.Equal Then
                            Throw New Exception("Filled MCC Code/Name Is Invalid" + Environment.NewLine + "Primary Transporter Code Is Mapped With Other MCC Code i.e (" + checkmcccode + ") At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        '------------------------------------------------------------------------

                        obj.pricekm = clsCommon.myCdbl(grow.Cells("Payment Per Km").Value)


                        obj.status = clsCommon.myCstr(grow.Cells("Vehicle payment basis").Value)
                        If clsCommon.CompairString(obj.status, "Day/Diesel") = CompairStringResult.Equal Then
                            'If obj.chagrshift <= 0 Then
                            '    Throw New Exception("Please Fill Charges per Day At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                            'If obj.avgrate <= 0 Then
                            '    Throw New Exception("Please Fill Average KM per Ltr At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                            'If obj.dieselrate <= 0 Then
                            '    Throw New Exception("Please Fill Rate of Diesel At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                        ElseIf clsCommon.CompairString(obj.status, "Rental") = CompairStringResult.Equal Then
                            'If obj.RentalAmount <= 0 Then
                            '    Throw New Exception("Please Fill Rental Amount At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                            'If Not (clsCommon.CompairString(obj.RentalType, "Day") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "Month") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "Year") = CompairStringResult.Equal) Then
                            '    Throw New Exception("Rental Type should be Day,Month,Year  At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                        ElseIf clsCommon.CompairString(obj.status, "Rate/Ltr") = CompairStringResult.Equal Then
                            'If obj.Price_Ltr_KG <= 0 Then
                            '    Throw New Exception("Please Fill Price Ltr/KG At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                            'If Not (clsCommon.CompairString(obj.Rate_Type, "LTR") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "KG") = CompairStringResult.Equal) Then
                            '    Throw New Exception("Rate Type should be LTR,KG  At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                        ElseIf clsCommon.CompairString(obj.status, "Rate/K.M") = CompairStringResult.Equal Then
                            If obj.pricekm <= 0 Then
                                Throw New Exception("Please Fill Rate per KM At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(obj.status, "Rental/Diesel") = CompairStringResult.Equal Then
                            'If obj.RentalAmount <= 0 Then
                            '    Throw New Exception("Please Fill Rental Amount At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                            'If obj.avgrate <= 0 Then
                            '    Throw New Exception("Please Fill Average KM per Ltr At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                            'If obj.dieselrate <= 0 Then
                            '    Throw New Exception("Please Fill Rate of Diesel At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                        ElseIf clsCommon.CompairString(obj.status, "KM_Range") = CompairStringResult.Equal Then
                        ElseIf clsCommon.myLen(obj.status) > 0 Then
                            Throw New Exception("Payment method should be Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If settApplyEffectiveStartDate = True Then
                            If clsCommon.myLen(grow.Cells("Vehicle Effective Start Date").Value) <= 0 Then
                                Throw New Exception("Please Fill Vehicle Effective Start Date At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            obj.Effective_Start_Date = clsCommon.GetPrintDate(grow.Cells("Vehicle Effective Start Date").Value, "dd/MMM/yyyy")
                        End If

                        qry = "select count(*) from TSPL_Primary_Vehicle_Master where vehicle_code='" + obj.docno + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        Dim isNewEntry As Boolean = True
                        If check > 0 Then
                            isNewEntry = False
                        Else
                            isNewEntry = True
                        End If
                        If clsCommon.myLen(obj.docno) > 0 Then
                            'trans = clsDBFuncationality.GetTransactin()
                            If clsfrmPrimaryTransporterVehicalMaster.SaveData(obj.docno, isNewEntry, obj) Then
                            Else
                                Throw New Exception("No Data Transfer")
                            End If
                        End If


                        ''----------- end of Primary Transporter Vehicle Master


                        '' Milk Route Master
                        trans = Nothing
                        Dim objMRM As clsfrmMilkRouteMaster
                        objMRM = New clsfrmMilkRouteMaster()
                        clsfrmMilkRouteMaster.arr_VLC_Detail = Nothing
                        objMRM.code = clsCommon.myCstr(grow.Cells("Route Code").Value)
                        objMRM = clsfrmMilkRouteMaster.GetData(objMRM.code, Nothing, NavigatorType.Current)
                        If objMRM Is Nothing Then
                            objMRM = New clsfrmMilkRouteMaster
                        End If
                        If clsCommon.myLen(objMRM.code) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                            objMRM.code = StrTempVSPName
                            objMRM.desc = StrTempVSPName
                            objMRM.vehiclecode = StrTempVSPName
                        Else
                            objMRM.code = clsCommon.myCstr(grow.Cells("Route Code").Value)
                            objMRM.desc = clsCommon.myCstr(grow.Cells("Route Name").Value)
                            objMRM.vehiclecode = clsCommon.myCstr(grow.Cells("Vehicle").Value)
                        End If

                        If clsCommon.myLen(objMRM.desc) <= 0 Or clsCommon.myLen(objMRM.desc) > 150 Then
                            Throw New Exception("Please Fill Route Name(Max. 150 Characters) At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        check = 0


                        objMRM.Active = 1

                        If clsCommon.myLen(objMRM.vehiclecode) <= 0 Then
                            Throw New Exception("Please Fill Vehicle Details At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(objMRM.vehiclecode) > 0 Then
                            qry = "select count(*) from tspl_primary_vehicle_master where vehicle_code='" + objMRM.vehiclecode + "'"
                            check = clsDBFuncationality.getSingleValue(qry)
                            If check <= 0 Then
                                Throw New Exception("Filled Vehicle Code Is Invalid Or Does Not Exist in Master,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        objMRM.mcccode = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                        objMRM.mccname = clsCommon.myCstr(grow.Cells("MCC Name").Value)
                        If clsCommon.myLen(objMRM.mcccode) <= 0 AndAlso clsCommon.myLen(objMRM.mccname) <= 0 Then
                            Throw New Exception("Please Fill MCC Details At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(objMRM.mcccode) > 0 Then
                            qry = "select count(*) from tspl_mcc_master where mcc_code='" + objMRM.mcccode + "'"
                            check = clsDBFuncationality.getSingleValue(qry)

                            If check <= 0 Then
                                qry = "select count(*) from tspl_mcc_master where mcc_name='" + objMRM.mccname + "'"
                                check = clsDBFuncationality.getSingleValue(qry)

                                If check <= 0 Then
                                    objMRM.mcccode = ""
                                    Throw New Exception("Filled MCC Details Is Invalid Or Does Not Exist,See At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If
                        End If
                        If clsCommon.myLen(objMRM.mcccode) <= 0 Then
                            qry = "select count(*) from tspl_mcc_master where mcc_name='" + objMRM.mccname + "'"
                            check = clsDBFuncationality.getSingleValue(qry)

                            If check <= 0 Then
                                Throw New Exception("Filled MCC Details Is Invalid Or Does Not Exist,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        objMRM.kilometer = clsCommon.myCdbl(grow.Cells("Route Distance").Value)
                        If clsCommon.myLen(objMRM.kilometer) <= 0 Or clsCommon.myCdbl(objMRM.kilometer) <= 0 Then
                            Throw New Exception("Please Fill Route Distance And It Should Be Greater Than Zero(0) At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        qry = "select count(*) from tspl_mcc_route_master where route_Name='" + objMRM.desc + "'"
                        check = clsDBFuncationality.getSingleValue(qry)

                        If settApplyEffectiveStartDate = True Then
                            If clsCommon.myLen(grow.Cells("Route Effective Start Date").Value) <= 0 Then
                                Throw New Exception("Please Fill Route Effective Start Date At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            objMRM.Effective_Start_Date = clsCommon.GetPrintDate(grow.Cells("Route Effective Start Date").Value, "dd/MMM/yyyy")
                        End If

                        '' Dim isNewEntry As Boolean = True
                        If check <= 0 Then
                            isNewEntry = True
                        Else
                            isNewEntry = False
                        End If

                        'trans = clsDBFuncationality.GetTransactin()
                        If clsfrmMilkRouteMaster.SaveData(objMRM.code, objMRM, isNewEntry, True) Then
                        Else
                            Throw New Exception("No Data Transfer")
                        End If
                        ''end of Milk Route master

                        ''VSP Master

                        trans = clsDBFuncationality.GetTransactin()
                        Try

                            strvendorNo = clsCommon.myCstr(grow.Cells("VSP Code").Value)
                            If strvendorNo.Length > 12 Then
                                Throw New Exception("Check the length of VSP Code,")
                            End If


                            strvendorname1 = clsCommon.myCstr(grow.Cells("VSP Name").Value)
                            strvendorname = strvendorname1.Replace("'", "''")
                            If strvendorname.Length > 100 Then
                                Throw New Exception("Length of VSP Name can not be greater than 100.,")
                            End If

                            If String.IsNullOrEmpty(strvendorname) Then
                                Throw New Exception("VSP Name can not be blank")
                            End If
                            Dim add1 As String = clsCommon.myCstr(grow.Cells("VSP Address").Value)
                            closing_date = System.DateTime.Now.Date

                            statecode = clsCommon.myCstr(grow.Cells("State").Value)
                            check = 0


                            If clsCommon.myLen(statecode) > 0 Then
                                qry = "select STATE_CODE,STATE_NAME from tspl_state_master where  state_code='" + statecode + "'"
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                    Throw New Exception("State Code Does Not Exist,Please Make Its Master First")
                                End If
                                statecode = clsCommon.myCstr(dt.Rows(0)("STATE_CODE"))
                                state = clsCommon.myCstr(dt.Rows(0)("STATE_NAME"))
                            End If

                            Dim vsppaymnt As String = clsCommon.myCstr(grow.Cells("VSP Payment type").Value).Replace("'", "`")
                            'Dim jointname As String = clsCommon.myCstr(grow.Cells("Joint_Name").Value).Replace("'", "`")

                            If clsCommon.CompairString(vsppaymnt, "Different") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(vsppaymnt, "Self") <> CompairStringResult.Equal Then
                                Throw New Exception("Fill Self/Different in vsp payment at line no. " + clsCommon.myCstr(counter) + "")
                            End If
                            Dim NameOfBank As String = ""
                            Dim AccountNo As String = ""


                            strbank = clsCommon.myCstr(grow.Cells("Bank Code").Value)

                            If String.IsNullOrEmpty(strbank) Then
                                Throw New Exception("Bank Code can not be blank")
                            End If
                            Dim i5 As String
                            Dim EnableBankFromMaster As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableBankFromMaster, clsFixedParameterCode.EnableBankFromMaster, trans)) = 1, True, False)

                            If EnableBankFromMaster = True Then
                                Dim qry7 As String = "select COUNT(*) from tspl_vendor_bank_master  where Bank_Code ='" + strbank + "'"
                                i5 = connectSql.RunScalar(trans, qry7)
                                If i5 = 0 Then
                                    Throw New Exception("Bank code does not exist : " + strbank + "")
                                End If
                            End If

                            If strbank.Length > 30 Then
                                Throw New Exception("Check the length of bank code")
                            End If

                            Dim strAccNo As String = clsCommon.myCstr(grow.Cells("Account No").Value)
                            If clsCommon.myLen(strAccNo) > 50 Then
                                Throw New Exception("Account No. should be max 50 character.")
                            End If

                            Dim strBName As String = clsCommon.myCstr(grow.Cells("Bank Name").Value)
                            If clsCommon.myLen(strBName) > 50 Then
                                Throw New Exception("Bank Name should be max 50 character.")
                            End If

                            strIFSCCode = clsCommon.myCstr(grow.Cells("IFSC Code").Value)
                            If clsCommon.myLen(strIFSCCode) > 100 Then
                                Throw New Exception("IFSC Code should be max 100 character")
                            End If
                            strBrachName = clsCommon.myCstr(grow.Cells("Branch Name").Value)
                            If clsCommon.myLen(strBrachName) > 100 Then
                                Throw New Exception("Branch Name should be max 100 character")
                            End If
                            'If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where  TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & strBrachName & "'", trans) <= 0 Then
                            '    Throw New Exception("Branch Name Does Not Exist : " + strBrachName + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                            'End If
                            ' ''-------------------------
                            strgroupCode = clsCommon.myCstr(grow.Cells("VSP Group Code").Value)

                            If String.IsNullOrEmpty(strgroupCode) AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                strgroupCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ven_Group_Code from Tspl_vendor_group where Default_VSP=1", trans))
                            End If

                            If String.IsNullOrEmpty(strgroupCode) Then
                                Throw New Exception("VSP Group Code can not be blank")
                            End If
                            Dim i As Integer
                            qry = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                            i = connectSql.RunScalar(trans, qry)
                            If i = 0 Then
                                Throw New Exception("Vendor Group Code does not exist : " + strgroupCode + "")
                            Else
                            End If
                            If strgroupCode.Length > 12 Then
                                Throw New Exception("Check the length of VSP Group Code")
                            End If

                            strgroupDes = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  group_Desc from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'", trans))


                            coll = New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Vendor_Name", strvendorname)
                            clsCommon.AddColumnsForChange(coll, "add1", add1)
                            clsCommon.AddColumnsForChange(coll, "Closing_Date", closing_date)
                            clsCommon.AddColumnsForChange(coll, "State", state)
                            clsCommon.AddColumnsForChange(coll, "Country", country)
                            clsCommon.AddColumnsForChange(coll, "form_type", "VSP")
                            clsCommon.AddColumnsForChange(coll, "state_code", statecode)
                            clsCommon.AddColumnsForChange(coll, "City_Code", CityCode, True)
                            clsCommon.AddColumnsForChange(coll, "City_Code_Desc", CityName, True)
                            clsCommon.AddColumnsForChange(coll, "vsp_payment", vsppaymnt)
                            clsCommon.AddColumnsForChange(coll, "Branch_Name", strBrachName)
                            clsCommon.AddColumnsForChange(coll, "Account_No", strAccNo)
                            clsCommon.AddColumnsForChange(coll, "IFSC_Code", strIFSCCode)
                            clsCommon.AddColumnsForChange(coll, "Start_Date", Nothing, True)
                            clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                            clsCommon.AddColumnsForChange(coll, "Nature", "E")
                            clsCommon.AddColumnsForChange(coll, "is_Head_Load", "F")
                            clsCommon.AddColumnsForChange(coll, "Status", "N")
                            clsCommon.AddColumnsForChange(coll, "Onhold", "N")
                            clsCommon.AddColumnsForChange(coll, "Bank_Code", strbank)
                            clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code", strgroupCode)
                            clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code_Desc", strgroupDes)
                            clsCommon.AddColumnsForChange(coll, "Tip_Buffalo", clsCommon.myCdbl(grow.Cells("Buffalow TIP").Value))
                            clsCommon.AddColumnsForChange(coll, "Tip_Cow", clsCommon.myCdbl(grow.Cells("Cow TIP").Value))
                            clsCommon.AddColumnsForChange(coll, "Currency_Code", objCommonVar.BaseCurrencyCode)
                            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                            sql1 = "select count(*) from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + strvendorname + "' and form_type='VSP'"
                            i2 = CInt(connectSql.RunScalar(trans, sql1))
                            StrVdrNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + strvendorname + "' and form_type='VSP'", trans))
                            If (i2 = 0) Then
                                StrVdrNo = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, clsDocTransactionType.Registered, "")
                                clsCommon.AddColumnsForChange(coll, "Vendor_Code", StrVdrNo)
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Update, "vendor_code='" + StrVdrNo + "' and form_type='VSP'", trans)
                            End If



                            '' End of VSP Master
                            ''create customer as VSP
                            Dim createCustomer = clsCommon.myCstr(grow.Cells("Create customer").Value)
                            If clsCommon.CompairString(createCustomer, "0") <> CompairStringResult.Equal And clsCommon.CompairString(createCustomer, "1") <> CompairStringResult.Equal Then
                                Throw New Exception("Please Fill Create customer And It Should Be 0 or 1 At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If clsCommon.CompairString(createCustomer, "1") = CompairStringResult.Equal Then
                                Dim objCustomer As New clsCustomerMaster()
                                objCustomer.Cust_Code = StrVdrNo
                                objCustomer.Customer_Name = strvendorname
                                objCustomer.Add1 = add1
                                objCustomer.State = statecode
                                objCustomer.CUSTOMER_FORM_TYPE = "VSP"
                                strgroupCode = clsCommon.myCstr(grow.Cells("Customer Group Code").Value)
                                If String.IsNullOrEmpty(strgroupCode) AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                    strgroupCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Group_Code from TSPL_CUSTOMER_GROUP_MASTER where Default_VSP=1", trans))
                                End If

                                If String.IsNullOrEmpty(strgroupCode) Then
                                    Throw New Exception("Customer Group Code can not be blank")
                                End If

                                qry = "select Count(*) from TSPL_CUSTOMER_GROUP_MASTER  where Cust_Group_Code ='" + strgroupCode + "'"
                                i = connectSql.RunScalar(trans, qry)
                                If i = 0 Then
                                    Throw New Exception("Customer Group Code does not exist : " + strgroupCode + "")
                                Else
                                End If
                                If strgroupCode.Length > 12 Then
                                    Throw New Exception("Check the length of Customer Group Code")
                                End If

                                Dim strCmd1 As String = " SELECT Cust_Group_Code as [Customer Gruop Code],Cust_Group_Desc as [Description]," &
                                  " Tax_Group as [Tax Group],Cust_Account as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + strgroupCode + "' "
                                Dim myDs As DataSet = connectSql.RunSQLReturnDS(trans, strCmd1)
                                If myDs.Tables(0).Rows.Count > 0 Then
                                    Dim row As DataRow = myDs.Tables(0).Rows(0)
                                    objCustomer.Cust_Group_Code = clsCommon.myCstr(row(0).ToString().Trim())
                                    objCustomer.Tax_Group = clsCommon.myCstr(row(2).ToString().Trim())
                                    objCustomer.Cust_Account = clsCommon.myCstr(row(3).ToString().Trim())
                                    objCustomer.Terms_Code = clsCommon.myCstr(row(4).ToString().Trim())
                                End If
                                objCustomer.Credit_Customer = "N"

                                objCustomer.LastInvoice_No = Nothing
                                objCustomer.LastInvoice_Date = Nothing
                                objCustomer.Inter_Branch = "N"

                                objCustomer.IsDistributor = "N"

                                objCustomer.prntcustyn = "N"

                                objCustomer.CSA_Type = "N"
                                objCustomer.ManualCustomer = "N"

                                objCustomer.Comp_Code = objCommonVar.CurrentCompanyCode

                                Dim arrDBName As New List(Of String)
                                arrDBName.Add(clsCommon.myCstr(objCommonVar.CurrDatabase))

                                sql1 = "select count(*) from TSPL_CUSTOMER_MASTER where cust_code ='" + StrVdrNo + "' and CUSTOMER_FORM_TYPE='VSP'"
                                i2 = CInt(connectSql.RunScalar(trans, sql1))
                                If (i2 = 0) Then
                                    'objCustomer.SaveData(objCustomer, objCustomer.ArrVisi, True, arrDBName, trans)
                                    objCustomer.SaveData(objCustomer, objCustomer.ArrVisi, True, trans)
                                Else
                                    ' objCustomer.SaveData(objCustomer, objCustomer.ArrVisi, False, arrDBName, trans)
                                    objCustomer.SaveData(objCustomer, objCustomer.ArrVisi, False, trans)
                                End If

                                'Customer Vendor mapping
                                Dim ii As Integer = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_CUSTOMER_VENDOR_MAPPING WHERE cust_code='" + StrVdrNo + "'", trans)
                                If ii = 0 Then
                                    qry = "insert into TSPL_CUSTOMER_VENDOR_MAPPING values('" + StrVdrNo + "','" + StrVdrNo + "') "
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                End If


                            End If

                            ''-----create customer as VSP



                            '' Village Master
                            Dim objVillage As New clsfrmVillageMaster
                            ''objVillage.villcode = clsCommon.myCstr(grow.Cells("village_code").Value)

                            objVillage.villname = clsCommon.myCstr(grow.Cells("Village Name").Value)
                            If clsCommon.myLen(objVillage.villname) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                objVillage.villname = StrTempVSPName
                            End If
                            If clsCommon.myLen(objVillage.villname) <= 0 Then
                                Throw New Exception("Please Fill Village Name At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If clsCommon.myLen(objVillage.villname) > 150 Then
                                Throw New Exception("Length Of Village Name Should Not Exceed 150 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            ' objVillage.citycode = clsCommon.myCstr(grow.Cells("city_code").Value)

                            objVillage.statecode = clsCommon.myCstr(grow.Cells("State").Value)
                            If clsCommon.myLen(objVillage.statecode) > 0 Then
                                qry = "select state_code from tspl_state_master where state_code='" + objVillage.statecode + "'"
                                objVillage.statecode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                                If clsCommon.myLen(objVillage.statecode) <= 0 Then
                                    Throw New Exception("First Create State Master(" + objVillage.statecode + " Does Not Exist In Master) See Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If
                            If objCommonVar.ApplyDefaultsInMaster = True Then
                                objVillage.citycode = clsCommon.myCstr(clsCityMaster.GetDefault(trans))
                            End If
                            isNewEntry = True
                            objVillage.villcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Village_Code from TSPL_VILLAGE_MASTER  where  Village_Name ='" + objVillage.villname + "'", trans))
                            objVillage.countrycode = clsStateMaster.GetData(objVillage.statecode, NavigatorType.Current, trans).COUNTRY_CODE
                            If clsCommon.myLen(objVillage.villcode) > 0 Then

                                isNewEntry = False
                            End If
                            clsfrmVillageMaster.SaveData(objVillage, isNewEntry, trans)

                            '' End of Village MAster 

                            '' VLC Master


                            Dim mcccode As String = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                            If clsCommon.myLen(mcccode) <= 0 Then
                                Throw New Exception("Please Fill MCC Code At Line No. " + clsCommon.myCstr(counter) + "")
                            End If

                            If clsCommon.myLen(objVillage.villname) <= 0 Then
                                Throw New Exception("Please Fill Village Name At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If clsCommon.myLen(objVillage.villname) > 150 Then
                                Throw New Exception("Length Of Village Name Should Not Exceed 150 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                            End If

                            Dim VlcUploaderCode As String = clsCommon.myCstr(grow.Cells("VLC Uploader Code").Value)


                            If clsCommon.myLen(VlcUploaderCode) <= 0 Then
                                Throw New Exception("Length Of Village Name Should Not Exceed 150 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            Dim villcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ViLLage_Code from TSPL_VILLAGE_MASTER  where  Village_Name ='" + clsCommon.myCstr(grow.Cells("village name").Value) + "'", trans))

                            If clsCommon.myLen(villcode) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                villcode = objVillage.villcode
                            End If

                            Dim vspcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + clsCommon.myCstr(grow.Cells("VSP NAME").Value) + "' and Form_type='VSP'", trans))

                            Dim MilkRouteCode As String = clsDBFuncationality.getSingleValue("Select Route_Code from TSPL_MCC_ROUTE_MASTER where route_name='" & clsCommon.myCstr(grow.Cells("Route Name").Value) & "' ", trans)

                            If clsCommon.myLen(MilkRouteCode) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                MilkRouteCode = clsDBFuncationality.getSingleValue("Select Route_Code from TSPL_MCC_ROUTE_MASTER where route_name='" & StrTempVSPName & "' ", trans)
                            End If


                            Dim isSaved As Boolean = True
                            qry = "select VLC_Code from TSPL_VLC_MASTER_HEAD where vlc_Name='" + clsCommon.myCstr(grow.Cells("VLC Name").Value) + "'"
                            Dim VLCCode As String = clsDBFuncationality.getSingleValue(qry, trans)
                            If clsCommon.myLen(VLCCode) <= 0 Then
                                VLCCode = mcccode & "/" & clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VLCMASTER, "", "")
                            End If
                            coll = New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "vlc_code", VLCCode)
                            clsCommon.AddColumnsForChange(coll, "vlc_name", clsCommon.myCstr(grow.Cells("VLC Name").Value))

                            clsCommon.AddColumnsForChange(coll, "VSP_Code", vspcode)
                            clsCommon.AddColumnsForChange(coll, "village_code", villcode)

                            clsCommon.AddColumnsForChange(coll, "MCC", mcccode)
                            clsCommon.AddColumnsForChange(coll, "Route_Code", MilkRouteCode)

                            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                            qry = "select count(VLC_Code) from TSPL_VLC_MASTER_HEAD where vlc_Name='" + clsCommon.myCstr(grow.Cells("VLC Name").Value) + "'"
                            check = CInt(clsDBFuncationality.getSingleValue(qry, trans))

                            If check <= 0 Then
                                clsCommon.AddColumnsForChange(coll, "Price_Code", Nothing, True)
                                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AutoUpdateVLCUploaderCodeInVLCMaster, clsFixedParameterCode.AutoUpdateVLCUploaderCodeInVLCMaster, trans), "1") = CompairStringResult.Equal Then
                                    VlcUploaderCode = clsfrmVLCMaster.GetCodeNumPart(VLCCode)
                                    clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                                Else
                                    clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                                End If
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Insert, "", trans)

                            Else

                                clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Update, " TSPL_VLC_MASTER_HEAD.vlc_code='" + VLCCode + "'", trans)
                            End If

                            'Create User
                            qry = "select count(User_Code) from TSPL_USER_MASTER where User_Code='" + VlcUploaderCode + "'"
                            check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                            If check <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                coll = New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "User_Code", VlcUploaderCode)
                                clsCommon.AddColumnsForChange(coll, "User_Name", strvendorname)
                                clsCommon.AddColumnsForChange(coll, "Password", clsCommon.EncryptString(VlcUploaderCode))
                                clsCommon.AddColumnsForChange(coll, "Default_Location", mcccode, True)
                                clsCommon.AddColumnsForChange(coll, "User_APP_Type", "V", True)
                                clsCommon.AddColumnsForChange(coll, "Vendor_Code", vspcode, True)
                                clsCommon.AddColumnsForChange(coll, "User_Type", "")
                                clsCommon.AddColumnsForChange(coll, "EMP_CODE", "")
                                clsCommon.AddColumnsForChange(coll, "Emp_Name", "")
                                clsCommon.AddColumnsForChange(coll, "Comp_Code", companyCode)
                                clsCommon.AddColumnsForChange(coll, "Created_By", userCode)
                                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                                clsCommon.AddColumnsForChange(coll, "Modify_By", userCode)
                                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_USER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                            End If


                            clsfrmVLCMaster.SaveVLCPriceCode(VLCCode, vspcode, mcccode, trans)



                            '' End Of VLC Master

                            ''MILK ROUTE VLC MAPPING DETAIL


                            qry = "select count(*) from TSPL_MCC_ROUTE_VLC_MAPPING where route_code='" & MilkRouteCode & "' and vlc_code='" & VLCCode & "'"
                            check = clsDBFuncationality.getSingleValue(qry, trans)

                            coll = New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Is_Active", 1)
                            clsCommon.AddColumnsForChange(coll, "vlc_code", VLCCode)

                            If check <= 0 Then
                                clsCommon.AddColumnsForChange(coll, "route_code", MilkRouteCode)
                                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Update, " route_code='" & MilkRouteCode & "' and vlc_code='" & VLCCode & "'", trans)
                            End If

                            ''END OF MILK ROUTE VLC MAPPING DETAIL

                            trans.Commit()

                        Catch ex As Exception
                            trans.Rollback()
                            Throw New Exception(ex.Message)
                        End Try
                        clsCommon.ProgressBarUpdate("Imported Records  : " & counter & "/" & gv.Rows.Count)
                    Next

                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow(Me, "Error at Line: " + clsCommon.myCstr(LineNo) + " - " + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Upload Multiple Master", Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub chkOwnBMC_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkOwnBMC.ToggleStateChanged
        If chkOwnBMC.Checked = True AndAlso Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster = True Then
            lblOwnMCC.Visible = True
            txtMCCOwnBMC.Visible = True
            lblMCCOwnBMC.Visible = True
            txtOwnBMCDate.Enabled = True
            txtOwnBMCDate.Value = clsCommon.GETSERVERDATE()
        Else
            lblOwnMCC.Visible = False
            txtMCCOwnBMC.Visible = False
            lblMCCOwnBMC.Visible = False
            txtOwnBMCDate.Enabled = False
            txtOwnBMCDate.Value = Nothing
        End If
    End Sub

    Private Sub findfndbankcode2__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles findfndbankcode2._MYValidating
        GetBankDetails2(isButtonClicked)
    End Sub

    Private Sub txtMCCOwnBMC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCCOwnBMC._MYValidating
        Try
            Dim qry As String = "select tspl_mcc_master.mcc_code as Code,tspl_mcc_master.mcc_name as Name,tspl_mcc_master.Plant_Code as [Plant Code],TSPL_LOCATION_MASTER.Location_Desc AS [Plant Name] from tspl_mcc_master left outer join tspl_location_master on tspl_location_master.location_code=tspl_mcc_master.plant_code"
            txtMCCOwnBMC.Value = clsCommon.ShowSelectForm("MCCFND@VLCVSPM", qry, "Code", "", txtMCCOwnBMC.Value, "Code", isButtonClicked)
            lblMCCOwnBMC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select MCC_NAME from tspl_MCC_MASTER where MCC_Code = '" + txtMCCOwnBMC.Value + "' "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtSupervisiorRP__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSupervisiorRP._MYValidating
        Try
            Dim qry As String = " select EMP_CODE as Code, Emp_Name  as Name from TSPL_EMPLOYEE_MASTER  "
            txtSupervisiorRP.Value = clsCommon.ShowSelectForm("Emp111111@VLCVSPM", qry, "Code", " Emp_type = 'Salesman' and Emp_Status = 'Active'", txtSupervisiorRP.Value, "Code", isButtonClicked)
            lblSupervisiorRPName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  Emp_Name   from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + txtSupervisiorRP.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try

    End Sub

    Private Sub txtvendornameHindi_Enter(sender As Object, e As EventArgs) Handles txtvendornameHindi.Enter
        clsMccMaster.ToHindiInput()
    End Sub

    Private Sub txtvendornameHindi_Leave(sender As Object, e As EventArgs) Handles txtvendornameHindi.Leave
        clsMccMaster.ToEnglishInput()
    End Sub

    Private Sub txtDistrict__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDistrict._MYValidating
        Try
            Dim qry As String = "select TSPL_DISTRICT_MASTER.Code as Code,TSPL_DISTRICT_MASTER.Name as DistrictName,TSPL_State_MASTER.STATE_CODE as [State Code] ,TSPL_State_MASTER.STATE_NAME as [State] " &
            " from TSPL_DISTRICT_MASTER " &
            " left outer join TSPL_State_MASTER  on TSPL_State_MASTER.STATE_CODE=TSPL_DISTRICT_MASTER.State_Code " &
            " left outer join TSPL_State_MASTER_detail on TSPL_State_MASTER.state_code=TSPL_State_MASTER_detail.state_code "

            txtDistrict.Value = clsCommon.ShowSelectForm("DCS@Dis@Finder", qry, "Code", "", txtDistrict.Value, "", isButtonClicked)
            lblDistrict.Text = clsDistrictMaster.GetName(txtDistrict.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtZone__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtZone._MYValidating
        Try
            Dim qry As String = " select TSPL_ZONE_MASTER.Zone_Code as Code, TSPL_ZONE_MASTER.Description as Name ,TSPL_ZONE_MASTER.City_Code as [City Code],TSPL_CITY_MASTER.City_Name as [City Name]  from TSPL_ZONE_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code = TSPL_ZONE_MASTER.City_Code "

            txtZone.Value = clsCommon.ShowSelectForm("DCS@Zone@Finder", qry, "Code", "", txtZone.Value, "", isButtonClicked)
            lblZone.Text = clsDBFuncationality.getSingleValue(" select Description from TSPL_ZONE_MASTER where Zone_Code = '" + txtZone.Value + "' ")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtBlockCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBlockCode._MYValidating
        Try
            Dim qry As String = " select BLOCK_CODE as Code, BLOCK_NAME as Name from TSPL_BLOCK_MASTER  "

            txtBlockCode.Value = clsCommon.ShowSelectForm("DCS@Block@Finder", qry, "Code", "", txtBlockCode.Value, "", isButtonClicked)
            lblBlockCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select BLOCK_NAME from TSPL_BLOCK_MASTER where BLOCK_CODE = '" + txtBlockCode.Value + "' "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtCastCategory__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCastCategory._MYValidating
        Try
            Dim qry As String = " select CAST_CATEGORY_CODE as Code , CAST_CATEGORY_NAME as Name from TSPL_CAST_CATEGORY_MASTER  "

            txtCastCategory.Value = clsCommon.ShowSelectForm("DCS@CastCateg@Finder", qry, "Code", "", txtCastCategory.Value, "", isButtonClicked)
            'lblBlockCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select BLOCK_NAME from TSPL_BLOCK_MASTER where BLOCK_CODE = '" + txtBlockCode.Value + "' "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtRevenueVillage__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRevenueVillage._MYValidating
        Try
            Dim qry As String = " select REVENUE_VILLAGE_CODE as Code, REVENUE_VILLAGE_NAME as Name from TSPL_REVENUE_VILLAGE_MASTER  "

            txtRevenueVillage.Value = clsCommon.ShowSelectForm("DCS@RevenueVillage@Finder", qry, "Code", "", txtRevenueVillage.Value, "", isButtonClicked)
            lblRevenueVillage.Text = clsRevenueVillageMaster.GetName(txtRevenueVillage.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtGrampanchayat__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGrampanchayat._MYValidating
        Try
            Dim qry As String = " select GRAMPANCHAYAT_CODE as Code, GRAMPANCHAYAT_NAME as Name from TSPL_GRAMPANCHAYAT_MASTER  "

            txtGrampanchayat.Value = clsCommon.ShowSelectForm("DCS@Grampanchayat@Finder", qry, "Code", "", txtGrampanchayat.Value, "", isButtonClicked)
            lblGrampanchayat.Text = clsGrampanchayatMaster.GetName(txtGrampanchayat.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtPanchayatSamiti__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPanchayatSamiti._MYValidating
        Try
            Dim qry As String = " select PANCHAYAT_SAMITI_CODE as Code, PANCHAYAT_SAMITI_NAME as Name from TSPL_PANCHAYAT_SAMITI_MASTER  "

            txtPanchayatSamiti.Value = clsCommon.ShowSelectForm("DCS@PanchayatSamiti@Finder", qry, "Code", "", txtPanchayatSamiti.Value, "", isButtonClicked)
            lblPanchayatSamiti.Text = clsPanchayatSamitiMaster.GetName(txtPanchayatSamiti.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtVidhanSabha__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVidhanSabha._MYValidating
        Try
            Dim qry As String = " select VIDHAN_SABHA_CODE as Code, VIDHAN_SABHA_NAME as Name from TSPL_VIDHAN_SABHA_MASTER  "

            txtVidhanSabha.Value = clsCommon.ShowSelectForm("DCS@VidhanSabha@Finder", qry, "Code", "", txtVidhanSabha.Value, "", isButtonClicked)
            lblVidhanSabha.Text = clsVidhanSabhaMaster.GetName(txtVidhanSabha.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtDistrict__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDistrict._MYOpenMasterForm
        Try
            Frm_Open = New frmDistrictMaster()
            Frm_Open.SetUserMgmt(clsUserMgtCode.DistrictMaster)
            Frm_Open.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtBlockCode__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBlockCode._MYOpenMasterForm
        Try
            Frm_Open = New frmBlockMaster()
            Frm_Open.SetUserMgmt(clsUserMgtCode.frmBlockMaster)
            Frm_Open.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtZone__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtZone._MYOpenMasterForm
        Try
            Frm_Open = New FrmZoneMaster()
            Frm_Open.SetUserMgmt(clsUserMgtCode.FrmZoneMaster)
            Frm_Open.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtRevenueVillage__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRevenueVillage._MYOpenMasterForm
        Try
            Frm_Open = New frmRevenueVillageMaster()
            Frm_Open.SetUserMgmt(clsUserMgtCode.frmRevenueVillageMaster)
            Frm_Open.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtGrampanchayat__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGrampanchayat._MYOpenMasterForm
        Try
            Frm_Open = New frmGrampanchayatMaster()
            Frm_Open.SetUserMgmt(clsUserMgtCode.frmGrampanchayatMaster)
            Frm_Open.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtPanchayatSamiti__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPanchayatSamiti._MYOpenMasterForm
        Try
            Frm_Open = New frmPanchayatSamitiMaster()
            Frm_Open.SetUserMgmt(clsUserMgtCode.frmPanchayatSamitiMaster)
            Frm_Open.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtVidhanSabha__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVidhanSabha._MYOpenMasterForm
        Try
            Frm_Open = New frmVidhanSabhaMaster()
            Frm_Open.SetUserMgmt(clsUserMgtCode.frmVidhanSabhaMaster)
            Frm_Open.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub chkApplyCowPrice_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkApplyCowPrice.ToggleStateChanged
        Try
            If chkApplyCowPrice.Checked Then
                txtCowPriceDate.Enabled = True
            Else
                txtCowPriceDate.Enabled = False
                txtCowPriceDate.Value = Nothing
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub ChkHeadLoad_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkHeadLoad.ToggleStateChanged
        Try
            If ChkHeadLoad.Checked Then
                CmbHeadLoadServiceBasis.Enabled = True
                txtRateHeadLoad.Enabled = True
            Else
                CmbHeadLoadServiceBasis.Enabled = False
                txtRateHeadLoad.Enabled = False
                CmbHeadLoadServiceBasis.Text = ""
                txtRateHeadLoad.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub ExportBlankSheet_Click(sender As Object, e As EventArgs) Handles ExportBlankSheet.Click
        Try
            If clsCommon.myLen(txtroutecode.Value) > 0 Then
                OpenRouteAccRouteCode(txtroutecode.Value)
            End If
            Dim ExportSheet As String = "BlankSheet"
            clsfrmVLCMaster.ExportDataTable(Nothing, Me, ExportSheet)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub ExportData_Click(sender As Object, e As EventArgs) Handles ExportData.Click
        Try
            If clsCommon.myLen(txtroutecode.Value) > 0 Then
                OpenRouteAccRouteCode(txtroutecode.Value)
            End If

            Dim ExportSheet As String = "FillDataSheet"
            Dim MultiDCSCodeName As ArrayList = Nothing
            Dim Qry As String = Nothing
            Qry = "Select TSPL_VENDOR_MASTER.Vendor_Code As 'DCS Code',TSPL_VENDOR_MASTER.Vendor_Name As 'DCS Name',TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As 'Uploader Code' from TSPL_VENDOR_MASTER 
                   Left Outer Join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code"
            MultiDCSCodeName = clsCommon.ShowMultipleSelectForm("DCSMulSelect", Qry, "DCS Code", "DCS Name", MultiDCSCodeName, MultiDCSCodeName)

            If clsCommon.myLen(MultiDCSCodeName) > 0 Then
                clsfrmVLCMaster.ExportDataTable(MultiDCSCodeName, Me, ExportSheet)
                'Else
                '    clsfrmVLCMaster.ExportDataTable(fndvendorNo.Value, Me, ExportSheet)
            End If



        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtRegistrationDate_ValueChanged(sender As Object, e As EventArgs) Handles txtRegistrationDate.ValueChanged

    End Sub

    Private Sub findfndbankcode_Load(sender As Object, e As EventArgs) Handles findfndbankcode.Load

    End Sub

    Private Sub txtIFSCCode2_TextChanged(sender As Object, e As EventArgs) Handles txtIFSCCode2.TextChanged

    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndvendorNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select VSP Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndvendorNo.Value, "Vendor_Code", "TSPL_Vendor_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtSavingCompanyBank__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSavingCompanyBank._MYValidating
        Try
            txtSavingCompanyBank.Value = clsBankMaster.getFinder("", txtSavingCompanyBank.Value, isButtonClicked)
            lblSavingCompanyBank.Text = clsBankMaster.GetName(txtSavingCompanyBank.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtCurrentCompanyBank__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCurrentCompanyBank._MYValidating
        Try
            txtCurrentCompanyBank.Value = clsBankMaster.getFinder("", txtCurrentCompanyBank.Value, isButtonClicked)
            lblCurrentCompanyBank.Text = clsBankMaster.GetName(txtCurrentCompanyBank.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class