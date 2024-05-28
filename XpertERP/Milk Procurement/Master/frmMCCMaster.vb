Imports System.Data.SqlClient
Imports common
Imports System.Text.RegularExpressions
Imports XpertERPHRandPayroll
Imports XpertERPCommanServices

'Ticket No  TEC/03/10/19-001030  ,Sanjay ,service detail should not be mandatory
Public Class FrmMCCMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim userCode As String = Nothing
    Dim compCode As String = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isShowAllMCC As Boolean = False
    Dim obj As clsMccMaster = Nothing
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim arrLoc As String = Nothing
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim isCellValueChangedSiloOpen As Boolean = False

    Const UOMColUnit As String = "Unit Code"
    Const UOMColUnitDesc As String = "Unit Description"
    Const UOMColConvFact As String = "Conversion Factor"
    Const UOMColStockUnit As String = "STOCKUNIT"
    Const UOMColStockUnitChangable As String = "STOCKUNITCHG"
    Dim sQuery As String = ""
    Dim Frm_Open As FrmMainTranScreen
    Dim checkPan As New System.Text.RegularExpressions.Regex("^([A-Z]){5}([0-9]){4}([A-Z]){1}")
    Public Const colSlNo As String = "colSlNo"
    Public colEmpCode As String = "colEmpCode"
    Public colEmpName As String = "colEmpName"
    Public AllowBankSectionEnableOnMCCMaster As Boolean = False
    Public SettApplyGaze As Boolean = False
    Dim AreaWiseBilling As Boolean = False

#End Region
#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        compCode = company
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmMCCMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SettApplyGaze = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyGaze, clsFixedParameterCode.ApplyGaze, Nothing)) = 1)
        fndLocation.Visible = True
        lblLocation.Visible = True
        MyLabel78.Visible = True
        isShowAllMCC = clsMccMaster.isCurrentUserHO()
        SetUserMgmtNew()
        AllowBankSectionEnableOnMCCMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBankSectionEnableOnMCCMaster, clsFixedParameterCode.AllowBankSectionEnableOnMCCMaster, Nothing)) = 1, True, False)
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S/U for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(rbtnReset, "Press Alt+N For New")

        MCCLOCATIONFINDER()
        Pg_bank.Text = "Bank Details" & Environment.NewLine & "For Payment"
        clsPortSetting.GetWeighingMachineNames(CboMachine)
        clsPortSetting.GetMachineType(cboSampleMachine)
        clsPortSetting.GetMachineType(cboSampleMachine2)
        clsPortSetting.GetMachineType(cboSampleMachine3)
        clsPortSetting.GetMachineType(cboSampleMachine4)

        clsPortSetting.GetWeighingMachineNames(CboMachineCow)
        clsPortSetting.GetMachineType(cboSampleMachineCow)
        clsPortSetting.GetMachineType(cboSampleMachine2Cow)
        LoadCollectionMethod()
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        RadButton1.Visible = True
        AreaWiseBilling = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)


    End Sub

    Sub loadBlankGvEmployee()
        isInsideLoadData = True
        gvEmp.Rows.Clear()
        gvEmp.Columns.Clear()

        Dim repoSLno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSLno.HeaderText = "SL.No "
        repoSLno.Name = colSlNo
        repoSLno.ReadOnly = True
        repoSLno.Width = 100
        repoSLno.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvEmp.MasterTemplate.Columns.Add(repoSLno)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Employee Code"
        repoCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCode.Name = colEmpCode
        repoCode.Width = 280
        repoCode.ReadOnly = False
        gvEmp.MasterTemplate.Columns.Add(repoCode)

        repoCode = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Employee Name"
        'repoCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCode.Name = colEmpName
        repoCode.Width = 400
        repoCode.ReadOnly = True
        gvEmp.MasterTemplate.Columns.Add(repoCode)

        gvEmp.AllowAddNewRow = True
        gvEmp.AllowEditRow = True
        gvEmp.AllowDeleteRow = True
        gvEmp.AllowRowResize = True
        gvEmp.AllowRowReorder = True
        gvEmp.AllowColumnResize = True
        gvEmp.AllowColumnChooser = True
        gvEmp.AllowAutoSizeColumns = True
        gvEmp.ShowGroupPanel = False
        gvEmp.AddNewRowPosition = SystemRowPosition.Bottom
        isInsideLoadData = False
    End Sub

    Sub reset()
        Try
            '------------------25/06/2014 Monika
            txtMCCNameHindi.Text = ""
            txtMCCCopy.Value = ""
            isInsideLoadData = False
            rbtnprop.IsChecked = False
            rbtnpartnership.IsChecked = False
            rbtnpvt.IsChecked = False
            rbtnpublic.IsChecked = False
            ChkIsTruckSheet.Checked = False
            ChkInactive.Checked = False
            ChkEMPONAmount.Checked = False
            chkAutoMilkIn.Checked = False
            chkDefault.Checked = False
            fndAutoInLoc.Value = ""
            txtAutoInLoc.Text = ""
            fndSiloInLoc.Value = ""
            txtSiloInLoc.Text = ""
            DtpShiftOpeningTime.Value = clsCommon.GETSERVERDATE(Nothing, "hh:mm:ss tt")
            DtpShiftClosingTime.Value = clsCommon.GETSERVERDATE(Nothing, "hh:mm:ss tt")
            txtDefaultTimeMorningShift.Value = clsCommon.GETSERVERDATE(Nothing, "hh:mm:ss tt")
            txtDefaultTimeEveningShift.Value = clsCommon.GETSERVERDATE(Nothing, "hh:mm:ss tt")
            DtpEveShiftOpenTiming.Value = clsCommon.GETSERVERDATE(Nothing, "hh:mm:ss tt")
            DtpEveShiftClosingTime.Value = clsCommon.GETSERVERDATE(Nothing, "hh:mm:ss tt")
            txtprop_name.Text = ""
            txtpartner_name.Text = ""
            txtdirectr_name.Text = ""
            CboMachine.SelectedValue = "P"
            cboSampleMachine.SelectedValue = "E"
            CmbSampleComport.Text = ""
            cboCollectionMethod.SelectedValue = 0
            cboSampleMachine2.SelectedValue = "E"
            cboSampleMachine3.SelectedValue = "E"
            cboSampleMachine4.SelectedValue = "E"
            CmbSampleComport2.Text = ""
            CmbSampleComport3.Text = ""
            CmbSampleComport4.Text = ""
            CmbWeighingComport.Text = ""
            txtchillingRate.Text = ""
            chkChillingMonthly.Checked = False
            txtTubCapacity.Value = 0
            txtTubCapacity.Text = ""
            txtchilling_qty.Text = ""
            txtchilling_kg_ltr.Text = ""
            txtchillingassur_qty.Text = ""
            txtchilling_period.Text = ""
            txtlease_chrg.Text = ""
            cmbagreemnt.Text = "NO"
            cmbsecurity.Text = "NO"
            txtagrmnt_date.Text = clsCommon.GETSERVERDATE()
            txtexpir_date.Text = clsCommon.GETSERVERDATE()
            txtchq_amt.Text = ""
            txtchq_date.Text = clsCommon.GETSERVERDATE()
            txtchq_no.Text = ""
            TxtFatSnfCalcDecimal.Text = 0
            TxtFatSNFSaveDecimal.Text = 0
            '-------------Code ENd Here------------------

            TxtMcc_In_Charge.Text = ""
            FndMccCharge.Value = Nothing
            TxtSiloWiseCapacity.Text = ""
            TxtPaymentCycle.Text = 0
            fndMCCCode.Value = ""
            ddlMCCType.SelectedIndex = 0
            txtMCCName.Text = ""
            txtShortDescription.Text = ""
            txtChillingVendorName.Text = ""
            fndChillingVendor.Value = ""
            fndpaymentCycle.Value = ""
            FndIncentive.Value = ""
            txtChillingVendorName.Enabled = False
            ' fndChillingVendor.Enabled = False
            txtAdd1.Text = ""
            txtAdd2.Text = ""
            txtTehsil.Text = ""
            txtCityName.Text = ""
            fndCity.Value = ""
            fndArea.Value = ""
            TxtUnitCode.Value = ""
            txtUnit.Text = ""
            fndState.Value = ""
            txtStateName.Text = ""
            fndCountry.Value = clsDBFuncationality.getSingleValue("select country_Code from tspl_Country_Master where Country_Code='INDIA'") '""
            txtCountryName.Text = clsDBFuncationality.getSingleValue("select country_Name from tspl_Country_Master where Country_Code='INDIA'") '""
            txtPinCode.Text = ""
            txtTelephone.Text = ""
            txtFax.Text = ""
            txtEmail.Text = ""
            txtMCCArea.Text = ""
            txtAreaOfStore.Text = ""
            txtAreaOfOffice.Text = ""
            txtOpenAreaForTankerMovement.Text = ""
            txtAreaOfLab.Text = ""
            TxtNoofSiloo.Text = ""
            txtTotalStorageCapacity.Text = ""
            txtAreaOfReceivingDock.Text = ""
            txtNoofChillero.Text = ""
            txtChillerBrandName.Text = ""
            txtChillerCapacity.Text = ""
            txtNoofMilkPumpo.Text = ""
            txtCapacityOfMilkPump.Text = ""
            ddlDripSaver.SelectedIndex = 0
            ddlCanWasher.SelectedIndex = 0
            ddlCanScrubber.SelectedIndex = 0
            txtFSSAINo.Text = ""
            ddlETP.SelectedIndex = 0
            ddlEarthing.SelectedIndex = 0
            txtCoilLength.Text = ""
            txtElectricityConnection.Text = ""
            ddlBoiler.SelectedIndex = 0
            txtNoOfDG.Text = ""
            'loadBlankDgvGenSet()

            txtNoOfCompressor.Text = ""
            loadBlankDgvCompressor()
            UcAttachment1.Form_ID = Me.Form_ID
            RadPageView1.Pages("RadPageViewPage5").Item.Visibility = ElementVisibility.Visible
            UcAttachment1.BlankAllControls()
            txtPayeeName.Text = ""
            txtBankName.Text = ""
            txtBankCode.Value = ""
            txtBankCityName.Text = ""
            fndBankCity.Value = ""
            fndBankState.Value = ""
            txtBankStateName.Text = ""
            txtBankBranchName.Text = ""
            TxtBranchCode.Text = ""
            txtIFCICode.Text = ""
            txtAccountNo.Text = ""
            btnSave.Text = "&Save"
            btnDelete.Enabled = False
            chkIsGateEntryRequired.Checked = False
            CmbArea_of_lab.SelectedValue = 0
            CmbArea_of_Office.SelectedValue = 0
            CmbArea_of_receiving.SelectedValue = 0
            CmbArea_of_Store.SelectedValue = 0
            CmbChillerOn.SelectedValue = 0
            CmbChilleronQty.SelectedValue = 0
            CmbChillerPeriod.SelectedValue = 0
            CmbRateOfLeasedCharges.SelectedValue = 0
            CmbOpenArea.SelectedValue = 0
            CmbMccArea.SelectedValue = 0
            CmbRateOfLeasedCharges.SelectedValue = 0
            txtpan.Text = Nothing

            txtEmployeeName.Text = ""
            fndEmployee.Value = ""

            TxtMccInchargeMobileNo.Text = Nothing
            TxtMccInchargeMailId.Text = Nothing
            DtpStartingDate.Value = Nothing

            chkMilkReceiptWeightTolerance.Checked = False
            txtMilkReceiptWeightTolerance.Value = 0

            loadBlankDgvCompressor()
            loadBlankDgvGenSet()
            LoadBlankGvChiller()
            LoadBlankGvMilkPump()
            LoadBlankGvSilo()
            LoadBlankGvCheque()
            LoadBlankGridUOM()
            LoadBlankBankGuaranteeGrid()
            loadBlankGvEmployee()
            txtMccCodeVlcUploader.Text = ""
            TxtSiloWiseCapacity.Text = ""
            TxtGuranteeAmount.Text = 0
            TxtSecurityAmount.Text = 0
            txtSecurityDeductedAmount.Text = 0
            RadPageView1.SelectedPage = RadPageViewPage1
            gvUOM.Rows.Clear()
            gvUOM.Rows.AddNew()
            gvSilo.Rows.Clear()
            gvMilkPump.Rows.Clear()
            gvChiller.Rows.Clear()
            GVPaymentEntry.Rows.Clear()

            GetSqMtrandFt(CmbArea_of_lab)
            GetSqMtrandFt(CmbArea_of_Office)
            GetSqMtrandFt(CmbArea_of_receiving)
            GetSqMtrandFt(CmbArea_of_Store)
            GetSqMtrandFt(CmbMccArea)
            GetSqMtrandFt(CmbOpenArea)

            GetKgandLtr(CmbChillerOn)
            GetHandledandDispatched(CmbChilleronQty)
            GetMonthandYearandDays(CmbChillerPeriod)
            GetMonthandYearandDays(CmbRateOfLeasedCharges)
            FndMpTermsCode.Value = Nothing
            FndMPPaymentCycle.Value = Nothing
            FndMPPaymentCode.Value = Nothing
            FndMPGrpCode.Value = Nothing

            TxtMPGrpCode.Text = Nothing
            txtMPtermcodedes.Text = Nothing
            TxtMPPaymentCode.Text = Nothing
            TxtMPPaymentCycle.Text = Nothing
            gvBankG.Rows.Clear()
            TxtGuranteeAmount.Text = 0
            TxtStandardSec_Amt.Text = 0

            chkSeprateDockForCowAndBuffalo.Checked = False
            chkAskSiloatShiftEnd.Checked = False
            setPanenVisiable()

            CboMachineCow.SelectedValue = "P"
            cboSampleMachineCow.SelectedValue = "E"
            CmbSampleComportCow.Text = ""
            cboSampleMachine2Cow.SelectedValue = "E"
            CmbSampleComport2Cow.Text = ""
            CmbWeighingComportCow.Text = ""

            txtFlusingAdjQty.Value = 0
            chkMCCInPlant.Checked = False

            chkFailedSampleApply.Checked = False
            chkSuspense.Checked = False
            txtFailedSampleFAT.Value = 0
            txtFailedSampleSNF.Value = 0
            fndLocation.Value = ""
            lblLocation.Text = ""
            rbtn_mcc.IsChecked = True
            txtMCCCopy.Enabled = True

            txtCommissionRate.Value = 0
            txtCommissionMinimumShiftInPaymentCycle.Value = 0
            txtCommissionMinimumQtyInShift.Value = 0
            txtCommissionNoOfPaymentCycleForNewVSP.Value = 0

            txtDeductionRate.Value = 0
            txtDeductionMinimumFATPer.Value = 0
            txtDeductionMinimumSNFPer.Value = 0
            txtDeductionNoOfPaymentCycleForNewVSP.Value = 0

            txtDWIFrom1.Value = 0
            txtDWIFrom2.Value = 0
            txtDWIFrom3.Value = 0
            txtDWIFrom4.Value = 0
            txtDWIFrom5.Value = 0


            txtDWITo1.Value = 0
            txtDWITo2.Value = 0
            txtDWITo3.Value = 0
            txtDWITo4.Value = 0
            txtDWITo5.Value = 0

            txtDWIRate1.Value = 0
            txtDWIRate2.Value = 0
            txtDWIRate3.Value = 0
            txtDWIRate4.Value = 0
            txtDWIRate5.Value = 0

            txtNonCompanyVSPDeduction.Value = 0
            txtCompanyVSPDeduction.Value = 0
            GroupBox3.Enabled = AllowBankSectionEnableOnMCCMaster

            chkSegment.Checked = False
            txtSegmentCode.Text = ""
            txtSegmentDesc.Text = ""
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.BlankAllControls()
                UcCustomFields1.SetDefaultValues()
            End If
            If objCommonVar.ApplyDefaultsInMaster = True Then
                SetDefaultValues()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub loadBlankDgvGenSet()
        Try
            dgvGenSet.Rows.Clear()
            dgvGenSet.Columns.Clear()
            dgvGenSet.Columns.Add("COLSLNO", "SL. NO")
            dgvGenSet.Columns.Add("COLGENSETDESC", "Genretor Description")
            dgvGenSet.Columns.Add("COLGENSETMake", "Make")
            dgvGenSet.Columns.Add("COLGENSETKVA", "KVA")
            dgvGenSet.Columns.Add("COLGENSETYear", "Year")
            dgvGenSet.Columns("COLSLNO").Width = 100
            dgvGenSet.Columns("COLGENSETDESC").Width = 150
            dgvGenSet.Columns("COLGENSETKVA").Width = 100
            dgvGenSet.Columns("COLGENSETYear").Width = 100
            dgvGenSet.Columns("COLGENSETMake").Width = 100
            dgvGenSet.AllowAddNewRow = False
            dgvGenSet.AllowEditRow = True
            dgvGenSet.AllowDeleteRow = True
            dgvGenSet.AllowRowResize = False
            dgvGenSet.AllowRowReorder = False
            dgvGenSet.AllowColumnResize = True
            dgvGenSet.AllowColumnChooser = False
            dgvGenSet.AllowAutoSizeColumns = False
            dgvGenSet.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Sub loadBlankDgvCompressor()
        Try
            dgvCompressor.Rows.Clear()
            dgvCompressor.Columns.Clear()
            dgvCompressor.Columns.Add("COLSLNO", "SL. NO")
            dgvCompressor.Columns.Add("COLCOMPNAME", "Compressor Name")
            dgvCompressor.Columns.Add("COLMake", "Make")
            dgvCompressor.Columns.Add("COLKVA", "KVA")
            dgvCompressor.Columns.Add("COLYear", "Year")
            dgvCompressor.Columns("COLSLNO").Width = 100
            dgvCompressor.Columns("COLCOMPNAME").Width = 150
            dgvCompressor.Columns("COLKVA").Width = 100
            dgvCompressor.Columns("COLYear").Width = 100
            dgvCompressor.Columns("COLMake").Width = 100
            dgvCompressor.AllowAddNewRow = False
            dgvCompressor.AllowEditRow = True
            dgvCompressor.AllowDeleteRow = True
            dgvCompressor.AllowRowResize = False
            dgvCompressor.AllowRowReorder = False
            dgvCompressor.AllowColumnResize = True
            dgvCompressor.AllowColumnChooser = False
            dgvCompressor.AllowAutoSizeColumns = False
            dgvCompressor.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankPaymentEntry()
        Try
            GVPaymentEntry.Rows.Clear()
            GVPaymentEntry.Columns.Clear()
            GVPaymentEntry.Columns.Add("COLPaymentNO", "Payment NO")
            GVPaymentEntry.Columns.Add("COLPaymentDate", "Date")
            GVPaymentEntry.Columns.Add("COLDesc", "Description")
            GVPaymentEntry.Columns.Add("COLBankName", "Bank Name")
            GVPaymentEntry.Columns.Add("COLPaymentType", "Payment Type")
            GVPaymentEntry.Columns.Add("COLBankCharges", "Bank Charges")
            GVPaymentEntry.Columns.Add("COLVendorCode", "Vendor Code")
            GVPaymentEntry.Columns.Add("COLVendorName", "Vendor Name")
            GVPaymentEntry.Columns.Add("COLChequeNo", "Cheque No")
            GVPaymentEntry.Columns.Add("COLChequeDate", "Cheque Date")
            GVPaymentEntry.Columns.Add("COLpaymentMode", "Payment Mode")
            GVPaymentEntry.Columns.Add("COLPaymentAmount", "Payment Amount")


            GVPaymentEntry.Columns("COLPaymentNO").Width = 100
            GVPaymentEntry.Columns("COLPaymentDate").Width = 100
            GVPaymentEntry.Columns("COLDesc").Width = 100
            GVPaymentEntry.Columns("COLBankName").Width = 100
            GVPaymentEntry.Columns("COLPaymentType").Width = 100
            GVPaymentEntry.Columns("COLBankCharges").Width = 100
            GVPaymentEntry.Columns("COLVendorCode").Width = 100
            GVPaymentEntry.Columns("COLVendorName").Width = 100
            GVPaymentEntry.Columns("COLChequeNo").Width = 100
            GVPaymentEntry.Columns("COLChequeDate").Width = 100
            GVPaymentEntry.Columns("COLpaymentMode").Width = 80
            GVPaymentEntry.Columns("COLPaymentAmount").Width = 100

            GVPaymentEntry.AllowAddNewRow = False
            GVPaymentEntry.AllowEditRow = False
            GVPaymentEntry.AllowDeleteRow = False
            GVPaymentEntry.AllowRowResize = False
            GVPaymentEntry.AllowRowReorder = False
            GVPaymentEntry.AllowColumnResize = False
            GVPaymentEntry.AllowColumnChooser = False
            GVPaymentEntry.AllowAutoSizeColumns = True
            GVPaymentEntry.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMCCMaster)
        If Not (MyBase.isReadFlag) Then
            clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        'RadMenu1.Visible = MyBase.isExport
        If MyBase.isExport = True Then
            mnuExport.Enabled = True
            mnuImport.Enabled = True
        Else
            mnuExport.Enabled = False
            mnuImport.Enabled = False
        End If
    End Sub
    Function allowToSave() As Boolean
        Try
            If ddlMCCType.Text <> "Co. Owned" And ddlMCCType.Text <> "Co. Leased" And ddlMCCType.Text <> "Chilling Basis" And ddlMCCType.Text <> "Federation" And ddlMCCType.Text <> "PPP" And ddlMCCType.Text <> "IKP" And ddlMCCType.Text <> "MPCS" Then
                clsCommon.MyMessageBoxShow(Me, " MCC Type Should Be Either of the Co. Owned/Co. Leased/Chilling Basis/Federation/PPP/IKP/MPCS (Under General Tab)")
                RadPageView1.SelectedPage = RadPageViewPage1
                'ErrorProvider1.SetError(TryCast(ddlMCCType, Control), "MCC Type Should Be Either of the Co. Owned/Co. Leased/Chilling Basis")
                errorControl.SetError(ddlMCCType, "MCC Type Should Be Either of the Co. Owned/Co. Leased/Chilling Basis/Federation/PPP/IKP/MPCS")
                Return False
            Else
                errorControl.ResetError(ddlMCCType)
            End If
            If clsCommon.myLen(txtMCCName.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, " MCC Name Must Not be Blank (Under General Tab)")
                RadPageView1.SelectedPage = RadPageViewPage1
                txtMCCName.Focus()
                errorControl.SetError(txtMCCName, "MCC Name Must Not be Blank ")
                Return False
            Else
                errorControl.SetError(txtMCCName, "")
            End If

            'If clsCommon.myLen(fndEmployee.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, " RM Code Must Not be Blank (Under General Tab)")
            '    RadPageView1.SelectedPage = RadPageViewPage1
            '    fndEmployee.Focus()
            '    errorControl.SetError(fndEmployee, "RM Code Must Not be Blank ")
            '    Return False
            'Else
            '    errorControl.SetError(fndEmployee, "")
            'End If

            If (clsCommon.CompairString(ddlMCCType.Text, "Chilling Basis") = CompairStringResult.Equal Or clsCommon.CompairString(ddlMCCType.Text, "Co. Leased") = CompairStringResult.Equal) And clsCommon.myLen(fndChillingVendor.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, " When Mcc Type is Chilling Basis/Co. Leased, Chilling Vendor  is Required (Under General Tab)")
                RadPageView1.SelectedPage = RadPageViewPage1
                fndChillingVendor.Focus()
                errorControl.SetError(fndChillingVendor, "When Mcc Type is Chilling Basis/Co. Leased, Chilling Vendor  is Required  ")
                Return False
            Else
                errorControl.SetError(fndChillingVendor, "")
            End If
            If clsCommon.myLen(txtAdd1.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, " Please Enter Address Line 1, It is Manadatory (Under General Tab)")
                RadPageView1.SelectedPage = RadPageViewPage1
                txtAdd1.Focus()
                errorControl.SetError(txtAdd1, "Please Enter Address Line 1, It is Manadatory ")
                Return False
            Else
                errorControl.SetError(txtAdd1, "")
            End If
            If clsCommon.myLen(fndCountry.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, " Please Enter Country Name, It is Manadatory (Under General Tab)")
                RadPageView1.SelectedPage = RadPageViewPage1
                fndCountry.Focus()
                errorControl.SetError(fndCountry, "Please Enter Country Name, It is Manadatory")
                Return False
            Else
                errorControl.SetError(fndCountry, "")
            End If
            If clsCommon.myLen(fndState.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, " Please Enter State Name, It is Manadatory (Under General Tab)")
                RadPageView1.SelectedPage = RadPageViewPage1
                fndState.Focus()
                errorControl.SetError(fndState, "Please Enter State Name, It is Manadatory")
                Return False
            Else
                errorControl.SetError(fndState, "")
            End If
            If clsCommon.myLen(fndCity.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, " Please Enter City Name, It is Mandatory (Under General Tab)")
                RadPageView1.SelectedPage = RadPageViewPage1
                fndCity.Focus()
                errorControl.SetError(fndCity, "Please Enter City Name, It is Mandatory")
                Return False
            Else
                errorControl.SetError(fndCity, "")
            End If
            If clsCommon.myLen(txtPinCode.Text) > 0 AndAlso (clsCommon.myLen(txtPinCode.Text) < 6 Or clsCommon.myLen(txtPinCode.Text) > 6) Then
                clsCommon.MyMessageBoxShow(Me, " Please Enter Pincode (Under General Tab),Must be 6 Char Length")
                RadPageView1.SelectedPage = RadPageViewPage1
                txtPinCode.Focus()
                errorControl.SetError(txtPinCode, "Please Enter Pincode.")
                Return False
            Else
                errorControl.SetError(txtPinCode, "")
            End If

            If clsCommon.myLen(txtMccCodeVlcUploader.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, " Please MCC Code For VLC Uploader, It is Mandatory (Under General Tab)")
                RadPageView1.SelectedPage = RadPageViewPage1
                txtMccCodeVlcUploader.Focus()
                errorControl.SetError(txtMccCodeVlcUploader, "Please MCC Code For VLC Uploader, It is Mandatory")
                Return False
            Else
                errorControl.SetError(txtMccCodeVlcUploader, "")
            End If


            If isDuplicateMccCode(IIf(clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal, False, True)) Then

                clsCommon.MyMessageBoxShow(Me, " Duplicate MCC Code for VLC Uploader")
                RadPageView1.SelectedPage = RadPageViewPage1
                txtMccCodeVlcUploader.Focus()
                errorControl.SetError(txtMccCodeVlcUploader, "Duplicate MCC Code for VLC Uploader")
                Return False
            Else
                errorControl.SetError(txtMccCodeVlcUploader, "")
            End If


            Dim check As Match = Regex.Match(txtEmail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            If check.Success = False AndAlso txtEmail.Text <> "" Then
                clsCommon.MyMessageBoxShow(Me, " Please Enter Valid Email, It is in Invalid Format (Under General Tab)")
                RadPageView1.SelectedPage = RadPageViewPage1
                txtEmail.Focus()
                errorControl.SetError(txtEmail, "Please Enter Valid Email, It is in Invalid Format")
                Return False
            Else
                errorControl.SetError(txtEmail, "")
            End If

            '------------------------------------------------------25/06/2014---Monika-------------
            If GrpIndustry.Visible And Not rbtnprop.IsChecked AndAlso Not rbtnpartnership.IsChecked AndAlso Not rbtnpvt.IsChecked AndAlso Not rbtnpublic.IsChecked Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Industry Type", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage1
                GrpIndustry.Focus()
                GrpIndustry.Select()
                errorControl.SetError(GrpIndustry, "Please Select Industry Type")
                Return False
            Else
                errorControl.ResetError(GrpIndustry)
            End If
            If clsCommon.CompairString(ddlMCCType.Text, "Co. Leased") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlMCCType.Text, "Chilling Basis") = CompairStringResult.Equal Then
                If rbtnprop.IsChecked AndAlso clsCommon.myLen(txtprop_name.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Fill Prop. Name", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    txtprop_name.Focus()
                    txtprop_name.Select()
                    errorControl.SetError(txtprop_name, "Please Fill Prop. Name")
                    Return False
                Else
                    errorControl.ResetError(txtprop_name)
                End If

                If rbtnpartnership.IsChecked AndAlso clsCommon.myLen(txtpartner_name.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Fill Partner Name", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    txtpartner_name.Focus()
                    txtpartner_name.Select()
                    errorControl.SetError(txtpartner_name, "Please Fill Partner Name")
                    Return False
                Else
                    errorControl.ResetError(txtpartner_name)
                End If

                If (rbtnpublic.IsChecked Or rbtnpvt.IsChecked) AndAlso clsCommon.myLen(txtdirectr_name.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Fill Director Name", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    txtdirectr_name.Focus()
                    txtdirectr_name.Select()
                    errorControl.SetError(txtdirectr_name, "Please Fill Director Name")
                    Return False
                Else
                    errorControl.ResetError(txtdirectr_name)
                End If
            End If




            If clsCommon.CompairString(cmbagreemnt.Text, "YES") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtagrmnt_date.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Fill Agreement Date", Me.Text)
                RadPageView1.SelectedPage = Pg_bank
                txtagrmnt_date.Focus()
                txtagrmnt_date.Select()
                errorControl.SetError(txtagrmnt_date, "Please Fill Agreement Date")
                Return False
            Else
                errorControl.ResetError(txtagrmnt_date)
            End If

            If clsCommon.CompairString(cmbsecurity.Text, "YES") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(txtchq_amt.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Fill Cheque Amount", Me.Text)
                RadPageView1.SelectedPage = Pg_bank
                txtchq_amt.Focus()
                txtchq_amt.Select()
                errorControl.SetError(txtchq_amt, "Please Fill Cheque Amount")
                Return False
            Else
                errorControl.ResetError(txtchq_amt)
            End If

            If clsCommon.CompairString(cmbsecurity.Text, "YES") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtchq_no.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Fill Cheque No.", Me.Text)
                RadPageView1.SelectedPage = Pg_bank
                txtchq_no.Focus()
                txtchq_no.Select()
                errorControl.SetError(txtchq_no, "Please Fill Cheque No.")
                Return False
            Else
                errorControl.ResetError(txtchq_no)
            End If

            If clsCommon.CompairString(cmbsecurity.Text, "YES") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtchq_date.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Fill Cheque Date", Me.Text)
                RadPageView1.SelectedPage = Pg_bank
                txtchq_date.Focus()
                txtchq_date.Select()
                errorControl.SetError(txtchq_date, "Please Fill Cheque Date")
                Return False
            Else
                errorControl.ResetError(txtchq_date)
            End If

            If (clsCommon.CompairString(cmbagreemnt.Text, "YES") = CompairStringResult.Equal Or clsCommon.CompairString(cmbsecurity.Text, "YES") = CompairStringResult.Equal) AndAlso UcAttachment1.gv1.Rows.Count < 1 Then
                clsCommon.MyMessageBoxShow(Me, "Please Attach Document For Agreement Or Security Cheque", Me.Text)
                RadPageView1.SelectedPage = Pg_bank
                errorControl.SetError(UcAttachment1.gv1, "Please Attach Document For Agreement Or Security Cheque")
                Return False
            Else
                errorControl.ResetError(UcAttachment1.gv1)
            End If


            'If clsCommon.myLen(txtPayeeName.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, "Please Fill Payee Name", Me.Text)
            '    RadPageView1.SelectedPage = Pg_bank
            '    txtPayeeName.Focus()
            '    txtPayeeName.Select()
            '    errorControl.SetError(txtPayeeName, "Please Fill Payee Name")
            '    Return False
            'Else
            '    errorControl.ResetError(txtPayeeName)
            'End If

            'If clsCommon.myLen(txtBankCode.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, "Please Select Bank Name", Me.Text)
            '    RadPageView1.SelectedPage = Pg_bank
            '    txtBankCode.Focus()
            '    txtBankCode.Select()
            '    errorControl.SetError(txtBankName, "Please Select Bank Name")
            '    Return False
            'Else
            '    errorControl.ResetError(txtBankName)
            'End If

            'If clsCommon.myLen(txtBranchCode.text) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, "Please Select Bank Branch Name", Me.Text)
            '    RadPageView1.SelectedPage = Pg_bank
            '    txtBankBranchCode.Focus()
            '    txtBankBranchCode.Select()
            '    errorControl.SetError(txtBankBranchName, "Please Select Bank Name")
            '    Return False
            'Else
            '    errorControl.ResetError(txtBankBranchName)
            'End If

            If clsCommon.myCdbl(txtNoOfDG.Text) > 0 AndAlso dgvGenSet.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Click On Go Button And Fill The Grid", Me.Text)
                btnDgGo.Focus()
                btnDgGo.Select()
                errorControl.SetError(btnDgGo, "Please Click On Go Button And Fill The Grid")
                Return False
            Else
                errorControl.ResetError(btnDgGo)
            End If
            If clsCommon.myCdbl(txtNoOfCompressor.Text) > 0 AndAlso dgvCompressor.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Click On Go Button And Fill The Grid", Me.Text)
                btnCompressorGo.Focus()
                btnCompressorGo.Select()
                errorControl.SetError(btnCompressorGo, "Please Click On Go Button And Fill The Grid")
                Return False
            Else
                errorControl.ResetError(btnCompressorGo)
            End If

            If clsCommon.myCdbl(TxtNoofSiloo.Text) > 0 AndAlso gvSilo.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Click On Go Button And Fill The Grid", Me.Text)
                BtnSilo.Focus()
                BtnSilo.Select()
                errorControl.SetError(BtnSilo, "Please Click On Go Button And Fill The Grid")
                Return False
            Else
                errorControl.ResetError(BtnSilo)
            End If

            If clsCommon.myCdbl(txtNoofMilkPumpo.Text) > 0 AndAlso gvMilkPump.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Click On Go Button And Fill The Grid", Me.Text)
                BtnMilkPump.Focus()
                BtnMilkPump.Select()
                errorControl.SetError(BtnMilkPump, "Please Click On Go Button And Fill The Grid")
                Return False
            Else
                errorControl.ResetError(BtnMilkPump)
            End If

            If clsCommon.myCdbl(txtNoofChillero.Text) > 0 AndAlso gvChiller.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Click On Go Button And Fill The Grid", Me.Text)
                BtnChiller.Focus()
                BtnChiller.Select()
                errorControl.SetError(BtnChiller, "Please Click On Go Button And Fill The Grid")
                Return False
            Else
                errorControl.ResetError(BtnChiller)
            End If
            '------------------------------------------------------------------------------

            'If clsCommon.myCdbl(txtMCCArea.Text) = 0 Or clsCommon.myLen(txtMCCArea.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, " Please Enter MCCArea, It is Manadatory (Under Area and Capacity Tab)")
            '    RadPageView1.SelectedPage = RadPageViewPage3
            '    errorControl.SetError(txtMCCArea, "Please Enter MCCArea, It is Manadatory")
            '    Return False
            'Else
            '    errorControl.SetError(txtMCCArea, "")

            'End If

            If clsCommon.myLen(txtpan.Text) > 0 Then
                Dim qry As String = "select count(*) from tspl_Mcc_master where PAN_No='" + clsCommon.myCstr(txtpan.Text) + "'"
                Dim checkPanExits As Integer = clsDBFuncationality.getSingleValue(qry)

                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal AndAlso checkPanExits > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    txtpan.Focus()
                    txtpan.Select()
                    errorControl.SetError(txtpan, "Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.")
                    Return False
                Else
                    errorControl.ResetError(txtpan)
                End If

                If clsCommon.CompairString(btnSave.Text, "Save") <> CompairStringResult.Equal AndAlso checkPanExits > 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    txtpan.Focus()
                    txtpan.Select()
                    errorControl.SetError(txtpan, "Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.")
                    Return False
                Else
                    errorControl.ResetError(txtpan)
                End If

                If clsCommon.myLen(txtpan.Text) > 0 Then
                    If Not checkPan.IsMatch(txtpan.Text) Then
                        txtpan.Focus()
                        Throw New Exception("Please check ! PAN numbers need to have 5 characters followed by 4 numbers then a final character")
                    End If
                End If
            End If



            'If clsCommon.myCdbl(txtNoOfSilo.Text) = 0 Or clsCommon.myLen(txtNoOfSilo.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, " Please Enter NO. Of Silo, It is Manadatory (Under Area and Capacity Tab)")
            '    RadPageView1.SelectedPage = RadPageViewPage3
            '    txtNoOfSilo.Focus()
            '    errorControl.SetError(txtNoOfSilo, "Please Enter NO. Of Silo, It is Manadatory")
            '    Return False
            'Else
            '    errorControl.SetError(txtNoOfSilo, "")
            'End If
            'If clsCommon.myCdbl(TxtSiloWiseCapacity.Text) = 0 Or clsCommon.myLen(TxtSiloWiseCapacity.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, " Please Enter Silo Capacity, It is Manadatory (Under Area and Capacity Tab)")
            '    RadPageView1.SelectedPage = RadPageViewPage3
            '    TxtSiloWiseCapacity.Focus()
            '    errorControl.SetError(TxtSiloWiseCapacity, "Please Enter Silo Capacity, It is Manadatory")
            '    Return False
            'Else
            '    errorControl.SetError(TxtSiloWiseCapacity, "")
            'End If

            'If clsCommon.myCdbl(txtTotalStorageCapacity.Text) = 0 Or clsCommon.myLen(txtTotalStorageCapacity.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, " Please Enter Total Storage Capacity, It is Manadatory (Under Area and Capacity Tab)")
            '    RadPageView1.SelectedPage = RadPageViewPage3
            '    txtTotalStorageCapacity.Focus()
            '    errorControl.SetError(txtTotalStorageCapacity, " Please Enter Total Storage Capacity, It is Manadatory")
            '    Return False
            'Else
            '    errorControl.SetError(txtTotalStorageCapacity, "")
            'End If

            'If clsCommon.myLen(ddlDripSaver.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, " Please Select Drip Saver either Yes Or No, It is Manadatory (Under Area and Capacity Tab)")
            '    RadPageView1.SelectedPage = RadPageViewPage3
            '    errorControl.SetError(ddlDripSaver, " Please Select Drip Saver either Yes Or No, It is Manadatory ")
            '    Return False
            'Else
            '    errorControl.SetError(ddlDripSaver, "")
            'End If

            'If clsCommon.myLen(ddlCanWasher.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, " Please Select Can Washer either Yes Or No, It is Manadatory (Under Area and Capacity Tab)")
            '    RadPageView1.SelectedPage = RadPageViewPage3
            '    errorControl.SetError(ddlCanWasher, " Please Select Can Washer either Yes Or No, It is Manadatory")
            '    Return False
            'Else
            '    errorControl.SetError(ddlCanWasher, "")
            'End If
            'If clsCommon.myLen(ddlCanScrubber.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, " Please Select Can Scrubber either Yes Or No, It is Manadatory (Under Area and Capacity Tab)")
            '    RadPageView1.SelectedPage = RadPageViewPage3
            '    errorControl.SetError(ddlCanScrubber, "Please Select Can Scrubber either Yes Or No, It is Manadatory")
            '    Return False
            'Else
            '    errorControl.SetError(ddlCanScrubber, "")
            'End If
            'If clsCommon.myLen(ddlETP.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, " Please Select ETP either Yes Or No, It is Manadatory (Under Area and Capacity Tab)")
            '    RadPageView1.SelectedPage = RadPageViewPage3
            '    errorControl.SetError(ddlETP, "Please Select ETP either Yes Or No, It is Manadatory ")
            '    Return False
            'Else
            '    errorControl.SetError(ddlETP, "")
            'End If
            'If clsCommon.myLen(ddlEarthing.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, " Please Select Earthing either Yes Or No, It is Manadatory (Under Area and Capacity Tab)")
            '    RadPageView1.SelectedPage = RadPageViewPage3
            '    errorControl.SetError(ddlEarthing, "Please Select Earthing either Yes Or No, It is Manadatory  ")
            '    Return False
            'Else
            '    errorControl.SetError(ddlEarthing, "")
            'End If
            'If clsCommon.myLen(ddlBoiler.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, " Please Select Boiler either Yes Or No, It is Manadatory (Under Area and Capacity Tab)")
            '    RadPageView1.SelectedPage = RadPageViewPage3
            '    errorControl.SetError(ddlBoiler, "Please Select Boiler either Yes Or No, It is Manadatory  ")
            '    Return False
            'Else
            '    errorControl.SetError(ddlBoiler, "")

            'End If
            If chkAutoMilkIn.Checked Then
                If clsCommon.myLen(fndAutoInLoc.Value) = 0 Then
                    clsCommon.MyMessageBoxShow(Me, " Please Select Auto In Location, It is Manadatory for Auto Milk In MCC", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    errorControl.SetError(fndAutoInLoc, " Please Select Auto In Location, It is Manadatory for Auto Milk In MCC")
                    Return False
                Else
                    errorControl.SetError(fndAutoInLoc, "")
                End If
                Dim qry As String = "select 1  from TSPL_LOCATION_MASTER where Location_Code='" + fndAutoInLoc.Value + "' and Location_Category='MCC'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    If clsCommon.myLen(fndSiloInLoc.Value) = 0 Then
                        clsCommon.MyMessageBoxShow(Me, " Please Select Auto Silo In Location, It is Manadatory for Auto Milk In MCC", Me.Text)
                        RadPageView1.SelectedPage = RadPageViewPage1
                        errorControl.SetError(fndSiloInLoc, " Please Select Auto Silo In Location, It is Manadatory for Auto Milk In MCC")
                        Return False
                    Else
                        errorControl.SetError(fndSiloInLoc, "")
                    End If
                Else
                    fndSiloInLoc.Value = ""
                End If
            End If
            Dim i As Integer = 0
            'If clsCommon.myCdbl(txtNoOfDG.Text) = 0 Or clsCommon.myLen(txtNoOfDG.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, " Please Enter NO. Of DG, It is Manadatory (Under DG Generator Tab)")
            '    RadPageView2.SelectedPage = RadPageViewPage4
            '    txtNoOfDG.Focus()
            '    errorControl.SetError(txtNoOfDG, "Please Enter NO. Of DG, It is Manadatory  ")
            '    Return False
            'Else
            '    errorControl.SetError(txtNoOfDG, "")
            'End If
            If dgvGenSet.Rows.Count > 0 Then
                For i = 0 To dgvGenSet.Rows.Count - 1
                    If clsCommon.myLen(clsCommon.myCstr(dgvGenSet.Rows(i).Cells("COLGENSETDESC").Value)) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, " Please Enter DG Set Detail For Row No " & (i + 1) & ", It is Manadatory (Under DG Generator Tab)")
                        RadPageView2.SelectedPage = RadPageViewPage4
                        errorControl.SetError(dgvGenSet, "Please Enter DG Set Detail For Row No " & (i + 1) & ", It is Manadatory  ")
                        Return False
                    Else
                        errorControl.SetError(dgvGenSet, "")
                    End If
                Next
            End If

            If gvSilo.Rows.Count > 0 Then
                For i = 0 To gvSilo.Rows.Count - 1
                    errorControl.SetError(gvSilo, "")
                    If clsCommon.myLen(clsCommon.myCstr(gvSilo.Rows(i).Cells("COLSiloDESC").Value)) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, " Please Enter Silo Set Detail For Row No " & (i + 1) & ", It is Manadatory (Under Silo Tab)")
                        RadPageView2.SelectedPage = Pg_Silo
                        errorControl.SetError(gvSilo, "Please Enter Silo Set Detail For Row No " & (i + 1) & ", It is Manadatory  ")
                        Return False
                    ElseIf SettApplyGaze Then
                        If clsCommon.myLen(clsCommon.myCstr(gvSilo.Rows(i).Cells("COLSILOGAZEREADING").Value)) <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, " Please Enter Gaze Reading For Row No " & (i + 1) & ", It is Manadatory (Under Silo Tab)")
                            RadPageView2.SelectedPage = Pg_Silo
                            errorControl.SetError(gvSilo, "Please Enter Gaze Reading For Row No " & (i + 1) & ", It is Manadatory  ")
                            Return False
                        Else
                            Dim qry As String = "select Code,Description,Capacity from TSPL_GAZE_READING Where Code='" + clsCommon.myCstr(gvSilo.CurrentRow.Cells("COLSILOGAZEREADING").Value) + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                If clsCommon.myCDecimal(gvSilo.CurrentRow.Cells("COLArea").Value) <> clsCommon.myCDecimal(dt.Rows(0)("Capacity")) Then
                                    clsCommon.MyMessageBoxShow(Me, "Capacity of Gaze Reading [" + clsCommon.myCstr(gvSilo.CurrentRow.Cells("COLSILOGAZEREADING").Value) + "] is [" + clsCommon.myCstr(dt.Rows(0)("Capacity")) + "] and entered capacity is [" + clsCommon.myCstr(gvSilo.CurrentRow.Cells("COLArea").Value) + "] For Row No " & (i + 1) & ", Please correct it (Under Silo Tab)")
                                    RadPageView2.SelectedPage = Pg_Silo
                                    errorControl.SetError(gvSilo, "Capacity of Gaze Reading [" + clsCommon.myCstr(gvSilo.CurrentRow.Cells("COLSILOGAZEREADING").Value) + "] is [" + clsCommon.myCstr(dt.Rows(0)("Capacity")) + "] and entered capacity is [" + clsCommon.myCstr(gvSilo.CurrentRow.Cells("COLArea").Value) + "] For Row No " & (i + 1) & ", Please correct it (Under Silo Tab)")
                                    Return False
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            If gvChiller.Rows.Count > 0 Then
                For i = 0 To gvChiller.Rows.Count - 1
                    If clsCommon.myLen(clsCommon.myCstr(gvChiller.Rows(i).Cells("COLChillerSETDESC").Value)) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, " Please Enter Chiller Set Detail For Row No " & (i + 1) & ", It Is Manadatory (Under Chiller Tab)")
                        RadPageView2.SelectedPage = Pg_Chillers
                        errorControl.SetError(gvChiller, "Please Enter Chiller Set Detail For Row No " & (i + 1) & ", It Is Manadatory  ")
                        Return False
                    Else
                        errorControl.SetError(gvChiller, "")
                    End If
                Next
            End If
            If gvMilkPump.Rows.Count > 0 Then
                For i = 0 To gvMilkPump.Rows.Count - 1
                    If clsCommon.myLen(clsCommon.myCstr(gvMilkPump.Rows(i).Cells("COLPumpDESC").Value)) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, " Please Enter Milk Pump Set Detail For Row No " & (i + 1) & ", It Is Manadatory (Under Milk Pump Tab)")
                        RadPageView2.SelectedPage = Pg_MilkPump
                        errorControl.SetError(gvMilkPump, "Please Enter Milk Pump Set Detail For Row No " & (i + 1) & ", It Is Manadatory  ")
                        Return False
                    Else
                        errorControl.SetError(gvMilkPump, "")
                    End If
                Next
            End If


            'If clsCommon.myCdbl(txtNoOfCompressor.Text) = 0 Or clsCommon.myLen(txtNoOfCompressor.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, " Please Enter NO. Of Compressors, It Is Manadatory (Under Compressor  Tab)")
            '    RadPageView2.SelectedPage = RadPageViewPage2
            '    txtNoOfCompressor.Focus()
            '    errorControl.SetError(txtNoOfCompressor, "Please Enter NO. Of Compressors, It Is Manadatory")
            '    Return False
            'Else
            '    errorControl.SetError(txtNoOfCompressor, "")
            'End If
            If dgvCompressor.Rows.Count > 0 Then
                For i = 0 To dgvCompressor.Rows.Count - 1
                    If clsCommon.myLen(clsCommon.myCstr(dgvCompressor.Rows(i).Cells("COLCOMPNAME").Value)) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, " Please Enter Compressor Name For Row No " & (i + 1) & ", It Is Manadatory (Under Compressor Tab)")
                        RadPageView2.SelectedPage = RadPageViewPage2
                        errorControl.SetError(dgvCompressor, " Please Enter Compressor Name For Row No " & (i + 1) & ", It Is Manadatory ")
                        Return False
                    Else
                        errorControl.SetError(dgvCompressor, "")
                    End If
                Next
            End If

            Dim isMultipleUOM As Boolean = False
            Dim strUOM As String = ""
            Dim TotalRecevingUnit As Integer = 0
            For ii As Integer = 0 To gvUOM.RowCount - 1

                If clsCommon.myLen(gvUOM.Rows(ii).Cells(UOMColUnit).Value) > 0 Then

                    If clsCommon.CompairString("Y", clsCommon.myCstr(gvUOM.Rows(ii).Cells(UOMColStockUnit).Value)) = CompairStringResult.Equal Then
                        TotalRecevingUnit += 1
                        'If gvUOM.Rows(ii).Cells(UOMColConvFact).Value <> 1 Then
                        '    RadPageView1.SelectedPage = RadPageViewPage2
                        '    Throw New Exception("The Coversion Unit Should be [1] For Stocking Unit [Yes]")
                        'End If
                        'If clsCommon.myLen(txtUOM.Value) > 0 Then
                        '    RadPageView1.SelectedPage = RadPageViewPage2
                        '    Throw New Exception("There should be only one stock unit")
                        'End If
                        'txtUOM.Value = clsCommon.myCstr(gvUOM.Rows(ii).Cells(UOMColUnit).Value)
                        'lblUOM.Text = clsCommon.myCstr(gvUOM.Rows(ii).Cells(UOMColUnitDesc).Value)
                    End If
                    If clsCommon.myLen(strUOM) > 0 Then
                        strUOM += ","
                        isMultipleUOM = True
                    End If
                    strUOM += "'" + clsCommon.myCstr(gvUOM.Rows(ii).Cells(UOMColUnit).Value) + "'"
                End If
            Next
            If clsCommon.myLen(strUOM) <= 0 Then
                RadPageView1.SelectedPage = Pg_Uom
                Throw New Exception("Please Enter UOM Details")
            End If

            If clsCommon.myCdbl(TotalRecevingUnit) <= 0 Then
                RadPageView1.SelectedPage = Pg_Uom
                Throw New Exception("Please Enter Receiving Unit UOM Details")
            End If

            If clsCommon.myCdbl(TotalRecevingUnit) > 1 Then
                RadPageView1.SelectedPage = Pg_Uom
                Throw New Exception("Enter Only One Receiving Unit in UOM Details")
                Return False
            End If

            For ix As Integer = 0 To gvUOM.Rows.Count - 1
                If clsCommon.myLen(gvUOM.Rows(ix).Cells(UOMColUnit).Value) > 0 Then
                    Dim UOM As String = gvUOM.Rows(ix).Cells(UOMColUnit).Value
                    For j As Integer = ix + 1 To gvUOM.Rows.Count - 1
                        Dim SecondUOM As String = gvUOM.Rows(j).Cells(UOMColUnit).Value
                        If UOM = SecondUOM Then
                            clsCommon.MyMessageBoxShow(Me, "Please check ! duplicate UOM in grid", Me.Text)
                            Return False
                        End If
                    Next
                End If
            Next
            If clsCommon.myLen(txtFSSAINo.Text) <= 0 Then
                If clsCommon.MyMessageBoxShow(Me, " FSSAI No Must Not be Blank ! Do You want to Save Auto Generate FSSAINo.?", "FASSI No", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    sQuery = "Select coalesce(Max(Fssai_no),'') from tspl_Mcc_Master where fssai_no like 'Auto%'"
                    Dim Fassi_No As String = clsDBFuncationality.getSingleValue(sQuery)
                    If Fassi_No = "" Then
                        txtFSSAINo.Text = "FSSAI00000001"
                    Else
                        txtFSSAINo.Text = clsCommon.incval(Fassi_No)
                    End If
                Else
                    RadPageView1.SelectedPage = RadPageViewPage1
                    txtMCCName.Focus()
                    errorControl.SetError(txtFSSAINo, "FSSAI No Must Not be Blank ")
                    Return False
                End If

            Else
                errorControl.SetError(txtFSSAINo, "")
            End If
            If chkMilkReceiptWeightTolerance.Checked Then
                If txtMilkReceiptWeightTolerance.Value < 0 Then
                    txtMilkReceiptWeightTolerance.Focus()
                    Throw New Exception("Milk Receipt Weight Tolerance can't be -ve")
                End If
            End If

            If chkFailedSampleApply.Checked Then
                If clsCommon.myLen(txtFailedSampleFAT.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please provide +ve Failed Sample FAT %", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    errorControl.SetError(txtFailedSampleFAT, "Please provide +ve Failed Sample FAT %")
                    Return False
                Else
                    errorControl.SetError(txtFailedSampleFAT, "")
                End If

                If clsCommon.myLen(txtFailedSampleSNF.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please provide +ve Failed Sample SNF %", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    errorControl.SetError(txtFailedSampleSNF, "Please provide +ve Failed Sample SNF %")
                    Return False
                Else
                    errorControl.SetError(txtFailedSampleSNF, "")
                End If
            End If
            If clsCommon.myLen(fndpaymentCycle.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select payment cycle", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage1
                errorControl.SetError(fndpaymentCycle, "Please select payment cycle")
                Return False
            Else
                errorControl.SetError(fndpaymentCycle, "")
            End If

            If chkSegment.Checked = True Then
                If clsCommon.myLen(txtSegmentCode.Text.Trim()) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please enter segment code")
                    RadPageView1.SelectedPage = RadPageViewPage1
                    errorControl.SetError(txtSegmentCode, "Please enter segment code")
                    Return False
                Else
                    errorControl.SetError(txtSegmentCode, "")
                End If
                If clsCommon.myLen(txtSegmentCode.Text.Trim()) > 0 Then
                    If clsCommon.myLen(txtSegmentCode.Text.Trim()) < 3 Then
                        clsCommon.MyMessageBoxShow(Me, "Segment code must be 3 charater", Me.Text)
                        errorControl.SetError(txtSegmentCode, "Segment code must be 3 charater")
                        Return False
                    Else
                        errorControl.SetError(txtSegmentCode, "")
                    End If
                    If clsCommon.myCstr(txtSegmentCode.Text.Trim().Contains(" ")) = True Then
                        clsCommon.MyMessageBoxShow(Me, "Segment code should not contain space", Me.Text)
                        errorControl.SetError(txtSegmentCode, "Segment code should not contain space")
                        Return False
                    Else
                        errorControl.SetError(txtSegmentCode, "")
                    End If

                End If


                If clsCommon.myLen(txtSegmentDesc.Text.Trim()) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please enter segment description", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    errorControl.SetError(txtSegmentDesc, "Please select segment description")
                    Return False
                Else
                    errorControl.SetError(txtSegmentDesc, "")
                End If
            End If


            UcCustomFields1.AllowToSave()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function
    Function isDuplicateMccCode(ByVal isUpdate As Boolean) As Boolean
        Dim qry As String = "select COUNT(*) from TSPL_MCC_MASTER where MCC_Code_vlc_uploader='" & txtMccCodeVlcUploader.Text & "' and mcc_code<>'" & fndMCCCode.Value & "'"
        Dim rvalue As Boolean = False
        Dim cnt As Integer = 0
        cnt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If cnt >= 1 Then 'isUpdate And cnt >= 2
            rvalue = True
            'ElseIf (Not isUpdate) And cnt >= 1 Then
            '    rvalue = True
        Else
            rvalue = False
        End If
        Return rvalue
    End Function
#End Region
#Region "Event Routines"
    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click, btnClose.Click
        Try
            Me.Close()
            GC.Collect()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        Try
            reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadAgreement()
        cmbagreemnt.DataSource = Nothing
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "NO"
        dr("Name") = "NO"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "YES"
        dr("Name") = "YES"
        dt.Rows.Add(dr)


        cmbagreemnt.DataSource = dt
        cmbagreemnt.DisplayMember = "Name"
        cmbagreemnt.ValueMember = "Code"
    End Sub

    Sub LoadSecurity()
        cmbsecurity.DataSource = Nothing
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "NO"
        dr("Name") = "NO"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "YES"
        dr("Name") = "YES"
        dt.Rows.Add(dr)


        cmbsecurity.DataSource = dt
        cmbsecurity.DisplayMember = "Name"
        cmbsecurity.ValueMember = "Code"
    End Sub

    Sub LoadCollectionMethod()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow
        dr("Code") = 0
        dr("Name") = "Tub System"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = 1
        dr("Name") = "Can Tare Weight"
        dt.Rows.Add(dr)

        cboCollectionMethod.DataSource = dt
        cboCollectionMethod.ValueMember = "Code"
        cboCollectionMethod.DisplayMember = "Name"
    End Sub

    ' Modify By : Prabhakar Ref Ticket : BM00000010125
    Private Sub txtPinCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPinCode.KeyPress, MyTextBox20.KeyPress, MyTextBox14.KeyPress
        'e.Handled = Not clsNumberValidate.IntValidate(e.KeyChar)
        'MessageBox.Show(e.KeyChar)
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = Chr(Keys.Back) Then e.Handled = True
    End Sub

#End Region

    '------------BM00000003414-------------------
    Private Sub MCCLOCATIONFINDER()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                arrLoc = obj.arrLocCodes
            Else
                'fndMCCCode.Enabled = False
                'Throw New Exception("Please Set Default Location Of LogIn User")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    '------------------------------------------------

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If allowToSave() Then
                save()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub save()
        Try
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmMCCMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            obj = New clsMccMaster()
            If clsCommon.CompairString(btnSave.Text, "&Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
            If obj.isNewEntry Then
                If rbtn_Bmcu.IsChecked = True Then
                    obj.MCC_Code = clsERPFuncationality.GetNextCode(Nothing, dt, clsDocType.MCCMaster, clsDocTransactionType.BMCU, fndState.Value, False, True, True)
                Else
                    obj.MCC_Code = clsERPFuncationality.GetNextCode(Nothing, dt, clsDocType.MCCMaster, clsDocTransactionType.MCC, fndState.Value, False, True, True)
                End If

                If clsCommon.myLen(obj.MCC_Code) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Error In Document Code Genertion", Me.Text)
                    Exit Sub
                End If
                'obj.MCC_Code = Microsoft.VisualBasic.Left(objCommonVar.CurrentCompanyCode, 2) & "/" & clsCommon.myCstr(fndState.Value) & "/" & obj.MCC_Code
                'obj.MCC_Code = Microsoft.VisualBasic.Left(fndState.Value, 2) & obj.MCC_Code 'Microsoft.VisualBasic.Left(objCommonVar.CurrentCompanyCode, 2) & clsCommon.myCstr(fndState.Value) &
            Else
                obj.MCC_Code = clsCommon.myCstr(fndMCCCode.Value)
            End If

            fndMCCCode.Value = obj.MCC_Code
            obj.MCC_Type = clsCommon.myCstr(ddlMCCType.Text)
            obj.Chilling_Vendor = clsCommon.myCstr(fndChillingVendor.Value)
            obj.MCC_NAME = txtMCCName.Text
            obj.MCC_Name_Hindi = txtMCCNameHindi.Text
            obj.Short_Description = txtShortDescription.Text
            'obj.Payment_Cycle = clsCommon.myCdbl(TxtPaymentCycle.Text)
            obj.Payment_Cycle = clsCommon.myCstr(fndpaymentCycle.Value)
            obj.Incentive_Code = clsCommon.myCstr(FndIncentive.Value)
            obj.Add1 = clsCommon.myCstr(txtAdd1.Text)
            obj.Add2 = clsCommon.myCstr(txtAdd2.Text)
            obj.Tehsil = clsCommon.myCstr(txtTehsil.Text)
            obj.City_code = clsCommon.myCstr(fndCity.Value)
            obj.is_Reuired_Gate_Entry = chkIsGateEntryRequired.Checked
            obj.Mcc_In_Charge = clsCommon.myCstr(FndMccCharge.Value)
            obj.State_Code = clsCommon.myCstr(fndState.Value)
            obj.Country_code = clsCommon.myCstr(fndCountry.Value)
            obj.Pin_code = clsCommon.myCstr(txtPinCode.Text)
            obj.Pan_No = clsCommon.myCstr(txtpan.Text)
            obj.Telphone = clsCommon.myCstr(txtTelephone.Text)
            obj.Email = clsCommon.myCstr(txtEmail.Text)
            obj.Fax = clsCommon.myCstr(txtFax.Text)
            obj.MCC_Area = clsCommon.myCdbl(txtMCCArea.Text)
            obj.Area_Of_Store = clsCommon.myCdbl(txtAreaOfStore.Text)
            obj.Area_Of_Office = clsCommon.myCdbl(txtAreaOfOffice.Text)
            obj.Open_Area_For_tanker = clsCommon.myCdbl(txtOpenAreaForTankerMovement.Text)
            obj.Area_Of_LAB = clsCommon.myCdbl(txtAreaOfLab.Text)
            obj.No_Of_SILO = clsCommon.myCdbl(TxtNoofSiloo.Text)
            'obj.Silo_Capacity = clsCommon.myCstr(TxtSiloWiseCapacity.Text)
            obj.Total_Storage_capacity = clsCommon.myCdbl(txtTotalStorageCapacity.Text)
            obj.Area_Of_Receiving_DOCK = clsCommon.myCdbl(txtAreaOfReceivingDock.Text)
            obj.No_Of_Chiller = clsCommon.myCdbl(txtNoofChillero.Text)
            obj.Chiller_Brand_Name = clsCommon.myCstr(txtChillerBrandName.Text)
            obj.Chiller_Capacity = clsCommon.myCdbl(txtChillerCapacity.Text)
            obj.No_Of_MilkPump = clsCommon.myCdbl(txtNoofMilkPumpo.Text)
            obj.MilkPump_Capacity = clsCommon.myCdbl(txtCapacityOfMilkPump.Text)
            obj.DripSaver = clsCommon.myCstr(ddlDripSaver.Text)
            obj.CanWasher = clsCommon.myCstr(ddlCanWasher.Text)
            obj.CanScrubber = clsCommon.myCstr(ddlCanScrubber.Text)
            obj.FSSAI_NO = clsCommon.myCstr(txtFSSAINo.Text)
            obj.ETP = clsCommon.myCstr(ddlETP.Text)
            obj.Earthing = clsCommon.myCstr(ddlEarthing.Text)
            obj.Coil_Length = clsCommon.myCdbl(txtCoilLength.Text)
            obj.Electricity_Connection = clsCommon.myCstr(txtElectricityConnection.Text)
            obj.Boiler = clsCommon.myCstr(ddlBoiler.Text)
            obj.NoOfDG = clsCommon.myCdbl(txtNoOfDG.Text)
            obj.MCC_Code_VLC_Uploader = clsCommon.myCstr(txtMccCodeVlcUploader.Text)
            obj.EMP_CODE = clsCommon.myCstr(fndEmployee.Value)
            obj.Plant_Code = clsCommon.myCstr(fndLocation.Value)
            obj.Area_Location_Code = clsCommon.myCstr(fndArea.Value)
            '------------------------25/06/2014 Monika--------------------------
            obj.bankcode = clsCommon.myCstr(txtBankCode.Value)
            obj.BankName = clsCommon.myCstr(txtBankName.Text)
            obj.BankStateCode = clsCommon.myCstr(fndBankState.Value)
            obj.AutoIn_Location = fndAutoInLoc.Value
            obj.SILOIn_Location = fndSiloInLoc.Value
            obj.AllowAutoMilkIn = IIf(chkAutoMilkIn.Checked, 1, 0)
            obj.IsDefault = chkDefault.Checked
            'obj.Start_Date = clsCommon.GetPrintDate(DtpEndDate.Value, "dd-MMM-yyyy")
            'obj.End_Date = clsCommon.GetPrintDate(DtpStartDate.Value, "dd-MMM-yyyy")
            obj.Guarantee_Amount = clsCommon.myCstr(TxtGuranteeAmount.Text)
            obj.Security_Amount = clsCommon.myCstr(TxtStandardSec_Amt.Text)

            obj.BankCityCode = clsCommon.myCstr(fndBankCity.Value)
            obj.BankBranch = clsCommon.myCstr(TxtBranchCode.Text)
            obj.AccountNO = clsCommon.myCstr(txtAccountNo.Text)
            obj.FAT_SNF_SAVED_DECIMAL = clsCommon.myCstr(TxtFatSNFSaveDecimal.Text)
            obj.FAT_SNF_CALC_DECIMAL = clsCommon.myCstr(TxtFatSnfCalcDecimal.Text)
            obj.IFCICode = clsCommon.myCstr(txtIFCICode.Text)
            obj.Is_MCC = rbtn_mcc.IsChecked
            obj.agreemnt = clsCommon.myCstr(cmbagreemnt.Text)
            obj.agrmnt_date = txtagrmnt_date.Text
            obj.expired_date = txtexpir_date.Text
            obj.secutiy = clsCommon.myCstr(cmbsecurity.Text)
            obj.chq_date = txtchq_date.Text
            obj.chq_no = clsCommon.myCdbl(txtchq_no.Text)
            obj.chq_amt = clsCommon.myCdbl(txtchq_amt.Text)
            obj.chilling_assur_period = clsCommon.myCdbl(txtchilling_period.Text)
            obj.Collection_Method = clsCommon.myCdbl(cboCollectionMethod.SelectedValue)
            If clsCommon.myCdbl(txtchilling_period.Text) > 0 Then
                obj.Chilling_Period_Starting_Date = clsCommon.GetPrintDate(DtpStartingDate.Value, "dd-MMM-yyyy")
            End If
            obj.chilling_assur_qty = clsCommon.myCdbl(txtchillingassur_qty.Text)
            ' obj.chilling_kg_ltr = clsCommon.myCdbl(txtchilling_kg_ltr.Text)
            ' obj.chilling_qty = clsCommon.myCdbl(txtchilling_qty.Text)
            obj.chilling_rate = clsCommon.myCdbl(txtchillingRate.Text)
            obj.is_Chilling_Provision_Monthly = chkChillingMonthly.Checked
            obj.lease_rate = clsCommon.myCdbl(txtlease_chrg.Text)
            obj.Tub_Capacity = txtTubCapacity.Value
            obj.industry_ppersn = ""
            obj.industry_type = ""
            If rbtnpartnership.IsChecked Then
                obj.industry_type = "Partnership"
                obj.industry_ppersn = clsCommon.myCstr(txtpartner_name.Text)
            ElseIf rbtnprop.IsChecked Then
                obj.industry_type = "Prop."
                obj.industry_ppersn = clsCommon.myCstr(txtprop_name.Text)
            ElseIf rbtnpublic.IsChecked Then
                obj.industry_type = "Public"
                obj.industry_ppersn = clsCommon.myCstr(txtdirectr_name.Text)
            ElseIf rbtnpvt.IsChecked Then
                obj.industry_type = "Pvt"
                obj.industry_ppersn = clsCommon.myCstr(txtdirectr_name.Text)
            End If
            '===============Rohit===================================
            obj.Unit_OpenAreaForTankerMovement = clsCommon.myCstr(CmbOpenArea.SelectedValue)
            obj.Unit_MccSuperArea = clsCommon.myCstr(CmbMccArea.SelectedValue)
            obj.Unit_ChillingOnQty = clsCommon.myCstr(CmbChilleronQty.SelectedValue)
            obj.Unit_ChillingOn = clsCommon.myCstr(CmbChillerOn.SelectedValue)
            obj.Unit_ChillingMinGuaranteePeriod = clsCommon.myCstr(CmbChillerPeriod.SelectedValue)
            obj.Unit_RateOfLeasedCharges = clsCommon.myCstr(CmbRateOfLeasedCharges.SelectedValue)
            obj.Unit_AreaOfStore = clsCommon.myCstr(CmbArea_of_Store.SelectedValue)
            obj.Unit_AreaOfReceivingDock = clsCommon.myCstr(CmbArea_of_receiving.SelectedValue)
            obj.Unit_AreaOfOffice = clsCommon.myCstr(CmbArea_of_Office.SelectedValue)
            obj.Unit_AreaOfLab = clsCommon.myCstr(CmbArea_of_lab.SelectedValue)

            obj.Weighing_Machine = clsCommon.myCstr(CboMachine.SelectedValue)
            obj.Sample_Machine = clsCommon.myCstr(cboSampleMachine.SelectedValue)
            obj.Default_Sample_Machine_2 = clsCommon.myCstr(cboSampleMachine2.SelectedValue)
            obj.Default_Sample_Machine_3 = clsCommon.myCstr(cboSampleMachine3.SelectedValue)
            obj.Default_Sample_Machine_4 = clsCommon.myCstr(cboSampleMachine4.SelectedValue)
            obj.Weighing_Comport = clsCommon.myCstr(CmbWeighingComport.Text)
            obj.Sample_comport = clsCommon.myCstr(CmbSampleComport.Text)
            obj.Default_Sample_Comport_2 = clsCommon.myCstr(CmbSampleComport2.Text)
            obj.Default_Sample_Comport_3 = clsCommon.myCstr(CmbSampleComport3.Text)
            obj.Default_Sample_Comport_4 = clsCommon.myCstr(CmbSampleComport4.Text)

            obj.Is_Seprate_Dock_Cow_Buffalo = chkSeprateDockForCowAndBuffalo.Checked
            If chkSeprateDockForCowAndBuffalo.Checked Then
                obj.Weighing_Machine_Cow = clsCommon.myCstr(CboMachineCow.SelectedValue)
                obj.Sample_Machine_Cow = clsCommon.myCstr(cboSampleMachineCow.SelectedValue)
                obj.Default_Sample_Machine_2_Cow = clsCommon.myCstr(cboSampleMachine2Cow.SelectedValue)
                obj.Weighing_Comport_Cow = clsCommon.myCstr(CmbWeighingComportCow.Text)
                obj.Sample_comport_Cow = clsCommon.myCstr(CmbSampleComportCow.Text)
                obj.Default_Sample_Comport_2_Cow = clsCommon.myCstr(CmbSampleComport2Cow.Text)
            End If




            obj.AskSiloatShiftEnd = chkAskSiloatShiftEnd.Checked

            obj.Deafault_MP_Grp_Code = clsCommon.myCstr(FndMPGrpCode.Value)
            obj.Deafault_MP_Payment_Code = clsCommon.myCstr(FndMPPaymentCode.Value)
            obj.Deafault_MP_Payment_Cycle = clsCommon.myCstr(FndMPPaymentCycle.Value)
            obj.Deafault_MP_Terms_Code = clsCommon.myCstr(FndMpTermsCode.Value)

            'obj.Payment_Cycle = clsCommon.myCstr(fndpaymentCycle.Value)
            obj.Is_Truck_Sheet = IIf(ChkIsTruckSheet.Checked = True, 1, 0)
            obj.Inactive = IIf(ChkInactive.Checked = True, 1, 0)
            obj.EmpOnAmountOnly = IIf(ChkEMPONAmount.Checked = True, 1, 0)
            obj.Shift_Opening_Time = clsCommon.myCstr(clsCommon.GetPrintDate(DtpShiftOpeningTime.Value, "HH:mm:ss tt"))
            obj.Shift_Closing_Time = clsCommon.myCstr(clsCommon.GetPrintDate(DtpShiftClosingTime.Value, "HH:mm:ss tt"))
            obj.Shift_Eve_Opening_Time = clsCommon.myCstr(clsCommon.GetPrintDate(DtpEveShiftOpenTiming.Value, "HH:mm:ss tt"))
            obj.Shift_Eve_Closing_Time = clsCommon.myCstr(clsCommon.GetPrintDate(DtpEveShiftClosingTime.Value, "HH:mm:ss tt"))

            obj.Shift_Default_Time_Morning = txtDefaultTimeMorningShift.Value
            obj.Shift_Default_Time_Evening = txtDefaultTimeEveningShift.Value
            '========================================================
            '---------code ended------------------------------------------------


            obj.Receipt_Weight_tolerance_Apply = chkMilkReceiptWeightTolerance.Checked
            obj.Receipt_Weight_tolerance_Value = txtMilkReceiptWeightTolerance.Value

            obj.MCC_in_Plant = chkMCCInPlant.Checked
            obj.Flusing_Adj_Qty_Shift_End = txtFlusingAdjQty.Value


            obj.Commission_Rate = txtCommissionRate.Value
            obj.Commission_Minimum_Shift_In_Payment_Cycle = txtCommissionMinimumShiftInPaymentCycle.Value
            obj.Commission_Minimum_Qty_In_Shift = txtCommissionMinimumQtyInShift.Value
            obj.Commission_No_Of_Payment_Cycle_For_New_VSP = txtCommissionNoOfPaymentCycleForNewVSP.Value

            obj.Deduction_Rate = txtDeductionRate.Value
            obj.Deduction_Minimum_FAT_Per = txtDeductionMinimumFATPer.Value
            obj.Deduction_Minimum_SNF_Per = txtDeductionMinimumSNFPer.Value
            obj.Deduction_No_Of_Payment_Cycle_For_New_VSP = txtDeductionNoOfPaymentCycleForNewVSP.Value



            obj.Day_Wise_Incentive_From_1 = txtDWIFrom1.Value
            obj.Day_Wise_Incentive_From_2 = txtDWIFrom2.Value
            obj.Day_Wise_Incentive_From_3 = txtDWIFrom3.Value
            obj.Day_Wise_Incentive_From_4 = txtDWIFrom4.Value
            obj.Day_Wise_Incentive_From_5 = txtDWIFrom5.Value


            obj.Day_Wise_Incentive_To_1 = txtDWITo1.Value
            obj.Day_Wise_Incentive_To_2 = txtDWITo2.Value
            obj.Day_Wise_Incentive_To_3 = txtDWITo3.Value
            obj.Day_Wise_Incentive_To_4 = txtDWITo4.Value
            obj.Day_Wise_Incentive_To_5 = txtDWITo5.Value

            obj.Day_Wise_Incentive_Rate_1 = txtDWIRate1.Value
            obj.Day_Wise_Incentive_Rate_2 = txtDWIRate2.Value
            obj.Day_Wise_Incentive_Rate_3 = txtDWIRate3.Value
            obj.Day_Wise_Incentive_Rate_4 = txtDWIRate4.Value
            obj.Day_Wise_Incentive_Rate_5 = txtDWIRate5.Value

            obj.Company_VSP_Deduction = txtCompanyVSPDeduction.Value
            obj.Non_Company_VSP_Deduction = txtNonCompanyVSPDeduction.Value

            Dim i As Integer = 0
            Dim objgenset As New clsGenSetDetail
            obj.arrGenSetDetail = New List(Of clsGenSetDetail)
            If clsCommon.myCdbl(txtNoOfDG.Text) > 0 Then
                For i = 0 To dgvGenSet.Rows.Count - 1
                    objgenset = New clsGenSetDetail
                    objgenset.Prog_Code = Me.Form_ID
                    objgenset.Trans_Code = obj.MCC_Code
                    objgenset.Line_No = clsCommon.myCdbl(dgvGenSet.Rows(i).Cells("COLSLNO").Value)
                    objgenset.Gen_Set_Desc = clsCommon.myCstr(dgvGenSet.Rows(i).Cells("COLGENSETDESC").Value)
                    objgenset.Gen_Set_Make = clsCommon.myCstr(dgvGenSet.Rows(i).Cells("COLGENSETMake").Value)
                    objgenset.Gen_Set_KVA = clsCommon.myCstr(dgvGenSet.Rows(i).Cells("COLGENSETKVA").Value)
                    objgenset.Gen_Set_Year = clsCommon.myCstr(dgvGenSet.Rows(i).Cells("COLGENSETYear").Value)
                    obj.arrGenSetDetail.Add(objgenset)
                Next
            End If

            Dim objMccEmp As New clsMccEmployee
            obj.arrMccEmployee = New List(Of clsMccEmployee)
            For i = 0 To gvEmp.Rows.Count - 1
                If clsCommon.myLen(gvEmp.Rows(i).Cells(colEmpCode).Value) > 0 Then
                    objMccEmp = New clsMccEmployee
                    objMccEmp.Prog_Code = Me.Form_ID
                    objMccEmp.Trans_Code = obj.MCC_Code
                    objMccEmp.Line_No = clsCommon.myCdbl(gvEmp.Rows(i).Cells(colSlNo).Value)
                    objMccEmp.Emp_Code = clsCommon.myCstr(gvEmp.Rows(i).Cells(colEmpCode).Value)
                    objMccEmp.Emp_Name = clsCommon.myCstr(gvEmp.Rows(i).Cells(colEmpName).Value)
                    obj.arrMccEmployee.Add(objMccEmp)
                End If
            Next

            obj.NoOfCompressor = clsCommon.myCdbl(txtNoOfCompressor.Text)
            Dim objcompressor As New clsCompressorDetail
            obj.arrCompressorDetail = New List(Of clsCompressorDetail)
            If clsCommon.myCdbl(txtNoOfCompressor.Text) > 0 Then
                For i = 0 To dgvCompressor.Rows.Count - 1
                    objcompressor = New clsCompressorDetail
                    objcompressor.Prog_Code = Me.Form_ID
                    objcompressor.Trans_Code = obj.MCC_Code
                    objcompressor.Line_No = clsCommon.myCdbl(dgvCompressor.Rows(i).Cells("COLSLNO").Value)
                    objcompressor.Compressor_Desc = clsCommon.myCstr(dgvCompressor.Rows(i).Cells("COLCOMPNAME").Value)

                    objcompressor.Compressor_Make = clsCommon.myCstr(dgvCompressor.Rows(i).Cells("COLMake").Value)
                    objcompressor.Compressor_KVA = clsCommon.myCstr(dgvCompressor.Rows(i).Cells("COLKVA").Value)
                    objcompressor.Compressor_Year = clsCommon.myCstr(dgvCompressor.Rows(i).Cells("COLYEAR").Value)
                    obj.arrCompressorDetail.Add(objcompressor)
                Next
            End If
            obj.No_Of_SILO = clsCommon.myCdbl(TxtNoofSiloo.Text)
            Dim objSilo As New clsSiloDetail
            obj.arrSiloDetail = New List(Of clsSiloDetail)
            If clsCommon.myCdbl(TxtNoofSiloo.Text) > 0 Then
                For i = 0 To gvSilo.Rows.Count - 1
                    objSilo = New clsSiloDetail
                    objSilo.Prog_Code = Me.Form_ID
                    objSilo.Trans_Code = obj.MCC_Code
                    objSilo.Line_No = clsCommon.myCdbl(gvSilo.Rows(i).Cells("COLSLNO").Value)
                    objSilo.Silo_Desc = clsCommon.myCstr(gvSilo.Rows(i).Cells("COLSiloDesc").Value)
                    objSilo.Silo_Unit = clsCommon.myCstr(gvSilo.Rows(i).Cells("COLUnit").Value)
                    objSilo.Silo_Area = clsCommon.myCstr(gvSilo.Rows(i).Cells("COLArea").Value)
                    objSilo.Gaze_Reading_Code = clsCommon.myCstr(gvSilo.Rows(i).Cells("COLSILOGAZEREADING").Value)
                    obj.arrSiloDetail.Add(objSilo)
                Next
            End If
            obj.No_Of_MilkPump = clsCommon.myCdbl(txtNoofMilkPumpo.Text)
            Dim objMilkPump As New clsMilkPumpDetail
            obj.arrMilkPumpDetail = New List(Of clsMilkPumpDetail)
            If clsCommon.myCdbl(txtNoofMilkPumpo.Text) > 0 Then
                For i = 0 To gvMilkPump.Rows.Count - 1
                    objMilkPump = New clsMilkPumpDetail
                    objMilkPump.Prog_Code = Me.Form_ID
                    objMilkPump.Trans_Code = obj.MCC_Code
                    objMilkPump.Line_No = clsCommon.myCdbl(gvMilkPump.Rows(i).Cells("COLSLNO").Value)
                    objMilkPump.Pump_Desc = clsCommon.myCstr(gvMilkPump.Rows(i).Cells("COLPumpDesc").Value)
                    objMilkPump.Pump_Unit = clsCommon.myCstr(gvMilkPump.Rows(i).Cells("COLUnit").Value)

                    objMilkPump.Pump_Area = clsCommon.myCstr(gvMilkPump.Rows(i).Cells("COLArea").Value)

                    obj.arrMilkPumpDetail.Add(objMilkPump)
                Next
            End If
            obj.No_Of_Chiller = clsCommon.myCdbl(txtNoofChillero.Text)
            Dim objChiller As New clsChillerDetail
            obj.arrChillerDetail = New List(Of clsChillerDetail)
            If clsCommon.myCdbl(txtNoofChillero.Text) > 0 Then
                For i = 0 To gvChiller.Rows.Count - 1
                    objChiller = New clsChillerDetail
                    objChiller.Prog_Code = Me.Form_ID
                    objChiller.Trans_Code = obj.MCC_Code
                    objChiller.Line_No = clsCommon.myCdbl(gvChiller.Rows(i).Cells("COLSLNO").Value)
                    objChiller.Chiller_Desc = clsCommon.myCstr(gvChiller.Rows(i).Cells("COLChillerSetDesc").Value)
                    objChiller.Chiller_Capacity = clsCommon.myCstr(gvChiller.Rows(i).Cells("COLChillerCapacity").Value)
                    objChiller.Chiller_Brand = clsCommon.myCstr(gvChiller.Rows(i).Cells("COLChillerBrand").Value)

                    obj.arrChillerDetail.Add(objChiller)
                Next
            End If
            Dim objCheck As New clsMCCChequeDetails
            obj.arrChequeDetail = New List(Of clsMCCChequeDetails)
            For i = 0 To gvCheque.Rows.Count - 1
                objCheck = New clsMCCChequeDetails
                objCheck.Prog_Code = obj.MCC_Code
                objCheck.Check_No = clsCommon.myCstr(gvCheque.Rows(i).Cells("COLCheck_No").Value)
                objCheck.Check_date = clsCommon.myCstr(gvCheque.Rows(i).Cells("COLCheck_Date").Value)

                obj.arrChequeDetail.Add(objCheck)
            Next


            obj.PayeeName = clsCommon.myCstr(txtPayeeName.Text)

            obj.Modified_By = objCommonVar.CurrentUserCode
            obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
            obj.Comp_Code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
            End If
            obj.ArrUomDetails = New List(Of clsMccUOMDetails)
            For ii As Integer = 0 To gvUOM.RowCount - 1
                Dim objtr As New clsMccUOMDetails()
                objtr.UOM_Code = clsCommon.myCstr(gvUOM.Rows(ii).Cells(UOMColUnit).Value)
                objtr.UOM_Description = clsMccUOMDetails.GetName(clsCommon.myCstr(gvUOM.Rows(ii).Cells(UOMColUnit).Value), Nothing)
                objtr.Stocking_Unit = clsCommon.myCstr(gvUOM.Rows(ii).Cells(UOMColStockUnit).Value)
                If clsCommon.CompairString(objtr.Stocking_Unit, "Y") = CompairStringResult.Equal Then
                    objtr.Conversion_Factor = 1
                    TxtUnitCode.Value = objtr.UOM_Code
                Else
                    objtr.Conversion_Factor = clsCommon.myCdbl(gvUOM.Rows(ii).Cells(UOMColConvFact).Value)
                End If
                If clsCommon.myLen(objtr.UOM_Code) > 0 Then
                    obj.ArrUomDetails.Add(objtr)
                End If
            Next
            obj.Unit_Code = clsCommon.myCstr(TxtUnitCode.Value)

            obj.IsSuspense = chkSuspense.Checked
            obj.Failed_Sample_Apply = chkFailedSampleApply.Checked
            obj.Failed_Sample_FAT = txtFailedSampleFAT.Value
            obj.Failed_Sample_SNF = txtFailedSampleSNF.Value

            obj.Loc_Segment_Code = txtSegmentCode.Text.Trim()
            obj.Loc_Segment_Description = txtSegmentDesc.Text.Trim()
            ''For Custom Fields
            obj.Form_ID = MyBase.Form_ID
            obj.arrCustomFields = New List(Of clsCustomFieldValues)
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.GetData(obj.arrCustomFields)
            End If
            'If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
            '    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colICode)
            'End If
            ''End of For Custom Fields
            If clsMccMaster.SaveData(obj) Then
                UcAttachment1.SaveData(obj.MCC_Code)
                If clsCommon.CompairString(btnSave.Text, "&Save") = CompairStringResult.Equal Then '--26/06/2014 Monika
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If

                If chkSegment.Checked = True Then
                    MCCLOCATIONFINDER()
                    chkSegment.Checked = False
                    txtSegmentCode.Text = ""
                    txtSegmentDesc.Text = ""
                End If

                loadData(obj.MCC_Code, NavigatorType.Current)
                If clsCommon.CompairString(clsCommon.myCstr(fndMCCCode.Value), "") = CompairStringResult.Equal Then
                    reset()
                    btnDelete.Enabled = False
                    btnSave.Text = "&Save"
                    fndMCCCode.MyReadOnly = False
                Else
                    btnSave.Text = "&Update"
                    fndMCCCode.MyReadOnly = True
                    btnDelete.Enabled = True
                End If
                Exit Sub

            End If
            clsCommon.MyMessageBoxShow(Me, "Data Not Saved ", Me.Text)
            btnSave.Text = "&Save"
            btnDelete.Enabled = False
            fndMCCCode.MyReadOnly = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndMCCCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndMCCCode._MYValidating
        Try
            Dim whrcls As String = ""
            If clsCommon.myLen(arrLoc) > 0 Then
                whrcls = " tspl_mcc_master.mcc_code in (" + arrLoc + ")"
            End If
            fndMCCCode.Value = clsMccMaster.getFinder(whrcls, fndMCCCode.Value, isButtonClicked)
            If clsCommon.myLen(fndMCCCode.Value) > 0 Then
                loadData(fndMCCCode.Value, NavigatorType.Current)
                btnSave.Text = "&Update"
                btnDelete.Enabled = True
                fndMCCCode.MyReadOnly = True
                txtMCCCopy.Enabled = False
            Else
                reset()
                fndMCCCode.MyReadOnly = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenEmployeeFinder(ByVal flag As Boolean)
        gvEmp.CurrentRow.Cells(colEmpCode).Value = clsEmployeeMaster.getFinder(" Emp_Status='Active' ", gvEmp.CurrentRow.Cells(colEmpCode).Value, False)
        gvEmp.CurrentRow.Cells(colEmpName).Value = clsCommon.myCstr(clsEmployeeMaster.GetName(gvEmp.CurrentRow.Cells(colEmpCode).Value, Nothing))
    End Sub
    Sub loadData(ByVal strCode As String, ByVal nav As NavigatorType)
        Try
            DtpShiftOpeningTime.Value = Nothing
            DtpShiftClosingTime.Value = Nothing
            DtpEveShiftOpenTiming.Value = Nothing
            DtpEveShiftClosingTime.Value = Nothing
            Dim obj As New clsMccMaster
            obj = clsMccMaster.loadData(fndMCCCode.Value, nav, isShowAllMCC, Me.Form_ID, arrLoc)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.MCC_Code) > 0 Then
                fndMCCCode.Value = obj.MCC_Code
                ddlMCCType.SelectedValue = obj.MCC_Type
                txtMCCName.Text = obj.MCC_NAME
                txtMCCNameHindi.Text = obj.MCC_Name_Hindi
                txtShortDescription.Text = obj.Short_Description
                fndChillingVendor.Value = obj.Chilling_Vendor
                If clsCommon.myLen(fndChillingVendor.Value) > 0 Then
                    txtChillingVendorName.Text = clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" & obj.Chilling_Vendor & "'")
                End If
                txtAdd1.Text = obj.Add1
                txtAdd2.Text = obj.Add2
                txtTehsil.Text = obj.Tehsil
                fndCity.Value = obj.City_code
                FndMccCharge.Value = obj.Mcc_In_Charge
                If clsCommon.myLen(FndMccCharge.Value) > 0 Then
                    TxtMcc_In_Charge.Text = clsEmployeeMaster.GetName(obj.Mcc_In_Charge, Nothing)
                End If
                ' TxtPaymentCycle.Text = obj.Payment_Cycle
                If clsCommon.myLen(fndCity.Value) > 0 Then
                    txtCityName.Text = clsDBFuncationality.getSingleValue("select city_name from tspl_city_master where city_code='" & obj.City_code & "'")
                End If
                fndState.Value = obj.State_Code
                If clsCommon.myLen(fndState.Value) > 0 Then
                    txtStateName.Text = clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" & obj.State_Code & "'")
                End If
                fndCountry.Value = obj.Country_code
                If clsCommon.myLen(fndCountry.Value) > 0 Then
                    txtCountryName.Text = clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" & obj.Country_code & "'")
                End If

                TxtUnitCode.Value = obj.Unit_Code
                If clsCommon.myLen(TxtUnitCode.Value) > 0 Then
                    txtUnit.Text = clsDBFuncationality.getSingleValue(" select Unit_Desc  from TSPL_UNIT_MASTER where Unit_Code ='" & obj.Unit_Code & "'")
                End If
                txtPinCode.Text = obj.Pin_code
                txtTelephone.Text = obj.Telphone
                txtEmail.Text = obj.Email
                txtFax.Text = obj.Fax
                txtMCCArea.Text = obj.MCC_Area
                txtAreaOfStore.Text = obj.Area_Of_Store
                txtAreaOfOffice.Text = obj.Area_Of_Office
                txtOpenAreaForTankerMovement.Text = obj.Open_Area_For_tanker
                txtAreaOfLab.Text = obj.Area_Of_LAB
                txtNoOfSilo.Text = obj.No_Of_SILO
                fndEmployee.Value = obj.EMP_CODE
                txtEmployeeName.Text = obj.EMP_Name
                fndLocation.Value = obj.Plant_Code
                fndArea.Value = obj.Area_Location_Code
                If clsCommon.myLen(obj.Plant_Code) > 0 Then
                    lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code = '" + obj.Plant_Code + "'"))
                End If
                ' TxtSiloWiseCapacity.Text = obj.SILO_Capacity
                chkIsGateEntryRequired.Checked = obj.is_Reuired_Gate_Entry
                'If obj.Start_Date <> "" Then
                '    DtpStartDate.Value = obj.Start_Date
                'End If
                'If obj.End_Date <> "" Then
                '    DtpEndDate.Value = obj.End_Date
                'End If
                TxtGuranteeAmount.Text = obj.Guarantee_Amount
                TxtStandardSec_Amt.Text = obj.Security_Amount
                If obj.Is_MCC = True Then
                    rbtn_mcc.IsChecked = True
                Else
                    rbtn_Bmcu.IsChecked = True
                End If
                txtTubCapacity.Value = obj.Tub_Capacity
                txtTotalStorageCapacity.Text = obj.Total_Storage_capacity
                txtAreaOfReceivingDock.Text = obj.Area_Of_Receiving_DOCK
                txtNoOfChiller.Text = obj.No_Of_Chiller
                txtChillerBrandName.Text = obj.Chiller_Brand_Name
                txtChillerCapacity.Text = obj.Chiller_Capacity
                txtNoOfMilkPump.Text = obj.No_Of_MilkPump
                txtCapacityOfMilkPump.Text = obj.MilkPump_Capacity
                ddlDripSaver.Text = obj.DripSaver
                ddlCanWasher.Text = obj.CanWasher
                ddlCanScrubber.Text = obj.CanScrubber
                txtFSSAINo.Text = obj.FSSAI_NO
                ddlETP.Text = obj.ETP
                txtMccCodeVlcUploader.Text = obj.MCC_Code_VLC_Uploader
                ddlEarthing.Text = obj.Earthing
                txtCoilLength.Text = obj.Coil_Length
                txtElectricityConnection.Text = obj.Electricity_Connection
                ddlBoiler.Text = obj.Boiler
                txtNoOfDG.Text = obj.NoOfDG
                chkAutoMilkIn.Checked = IIf(obj.AllowAutoMilkIn = 1, True, False)
                chkDefault.Checked = obj.IsDefault
                fndAutoInLoc.Value = obj.AutoIn_Location
                fndSiloInLoc.Value = obj.SILOIn_Location

                cboCollectionMethod.SelectedValue = obj.Collection_Method
                chkMCCInPlant.Checked = obj.MCC_in_Plant
                txtFlusingAdjQty.Value = obj.Flusing_Adj_Qty_Shift_End

                If clsCommon.myLen(fndAutoInLoc.Value) > 0 Then
                    txtAutoInLoc.Text = clsLocation.GetName(fndAutoInLoc.Value, Nothing)
                End If
                If clsCommon.myLen(fndSiloInLoc.Value) > 0 Then
                    txtSiloInLoc.Text = clsLocation.GetName(fndSiloInLoc.Value, Nothing)
                End If
                isInsideLoadData = True
                '--------------------25/06/2014----------
                loadBlankDgvGenSet()
                loadBlankDgvCompressor()
                LoadBlankPaymentEntry()
                LoadBlankGvChiller()
                LoadBlankGridUOM()
                LoadBlankBankGuaranteeGrid()
                LoadBlankGvMilkPump()
                LoadBlankGvSilo()
                LoadBlankGvCheque()
                loadBlankGvEmployee()
                TxtSecurityAmount.Text = 0
                txtSecurityDeductedAmount.Text = 0
                '----------------------------------
                If obj.arrGenSetDetail.Count > 0 Then

                    For i As Integer = 0 To obj.arrGenSetDetail.Count - 1
                        dgvGenSet.Rows.Add(obj.arrGenSetDetail.Item(i).Line_No, obj.arrGenSetDetail.Item(i).Gen_Set_Desc, obj.arrGenSetDetail.Item(i).Gen_Set_Make, obj.arrGenSetDetail.Item(i).Gen_Set_KVA, obj.arrGenSetDetail.Item(i).Gen_Set_Year)
                    Next
                    dgvGenSet.BestFitColumns()
                End If
                isInsideLoadData = True
                If obj.arrMccEmployee.Count > 0 Then
                    For i As Integer = 0 To obj.arrMccEmployee.Count - 1
                        gvEmp.Rows.Add(obj.arrMccEmployee.Item(i).Line_No, obj.arrMccEmployee.Item(i).Emp_Code, obj.arrMccEmployee.Item(i).Emp_Name)
                    Next
                End If

                txtNoOfCompressor.Text = obj.NoOfCompressor
                If obj.arrCompressorDetail.Count > 0 Then

                    For i As Integer = 0 To obj.arrCompressorDetail.Count - 1
                        dgvCompressor.Rows.Add(obj.arrCompressorDetail.Item(i).Line_No, obj.arrCompressorDetail.Item(i).Compressor_Desc, obj.arrCompressorDetail.Item(i).Compressor_Make, obj.arrCompressorDetail.Item(i).Compressor_KVA, obj.arrCompressorDetail.Item(i).Compressor_Year)
                    Next
                    dgvCompressor.BestFitColumns()
                End If
                '==================Load Payment Entry===========================
                If Not IsNothing(obj.arrPaymentDetail) Then
                    If obj.arrPaymentDetail.Count > 0 Then

                        For i As Integer = 0 To obj.arrPaymentDetail.Count - 1
                            GVPaymentEntry.Rows.Add(obj.arrPaymentDetail.Item(i).Payment_No, obj.arrPaymentDetail.Item(i).Payment_Date, obj.arrPaymentDetail.Item(i).Description, obj.arrPaymentDetail.Item(i).Bank_Name, obj.arrPaymentDetail.Item(i).Payment_Type, obj.arrPaymentDetail.Item(i).Bank_Charges, obj.arrPaymentDetail.Item(i).Vendor_Code, obj.arrPaymentDetail.Item(i).Vendor_Name, obj.arrPaymentDetail.Item(i).Cheque_No, obj.arrPaymentDetail.Item(i).Cheque_Date, obj.arrPaymentDetail.Item(i).Payment_Mode, obj.arrPaymentDetail.Item(i).Payment_Amount)
                            If clsCommon.myCdbl(obj.arrPaymentDetail.Item(i).Payment_Amount) >= 0 Then
                                TxtSecurityAmount.Text += clsCommon.myCdbl(obj.arrPaymentDetail.Item(i).Payment_Amount)
                            Else
                                txtSecurityDeductedAmount.Text -= clsCommon.myCdbl(obj.arrPaymentDetail.Item(i).Payment_Amount)
                            End If

                        Next
                        GVPaymentEntry.BestFitColumns()
                    End If
                End If
                '========================================================
                '================Silo detail=================================================
                TxtNoofSiloo.Text = obj.No_Of_SILO
                If obj.arrSiloDetail.Count > 0 Then
                    For i As Integer = 0 To obj.arrSiloDetail.Count - 1
                        gvSilo.Rows.Add(obj.arrSiloDetail.Item(i).Line_No, obj.arrSiloDetail.Item(i).Silo_Desc, obj.arrSiloDetail.Item(i).Gaze_Reading_Code, obj.arrSiloDetail.Item(i).Silo_Area, obj.arrSiloDetail.Item(i).Silo_Unit)
                    Next
                    gvSilo.BestFitColumns()
                End If
                '=================================================================================
                '================Milk Pump detail=================================================
                txtNoofMilkPumpo.Text = obj.No_Of_MilkPump
                If obj.arrMilkPumpDetail.Count > 0 Then

                    For i As Integer = 0 To obj.arrMilkPumpDetail.Count - 1
                        gvMilkPump.Rows.Add(obj.arrMilkPumpDetail.Item(i).Line_No, obj.arrMilkPumpDetail.Item(i).Pump_Desc, obj.arrMilkPumpDetail.Item(i).Pump_Area, obj.arrMilkPumpDetail.Item(i).Pump_Unit)
                    Next
                    gvMilkPump.BestFitColumns()
                End If
                '=================================================================================
                '================Chiller detail=================================================
                txtNoofChillero.Text = obj.No_Of_Chiller
                If obj.arrChillerDetail.Count > 0 Then

                    For i As Integer = 0 To obj.arrChillerDetail.Count - 1
                        gvChiller.Rows.Add(obj.arrChillerDetail.Item(i).Line_No, obj.arrChillerDetail.Item(i).Chiller_Desc, obj.arrChillerDetail.Item(i).Chiller_Brand, obj.arrChillerDetail.Item(i).Chiller_Capacity)
                    Next
                    gvChiller.BestFitColumns()
                End If
                '=================================================================================
                '================Cheque detail=================================================
                If obj.arrChequeDetail.Count > 0 Then

                    For i As Integer = 0 To obj.arrChequeDetail.Count - 1
                        gvCheque.Rows.Add(obj.arrChequeDetail.Item(i).Check_No, obj.arrChequeDetail.Item(i).Check_date)
                    Next
                    gvCheque.BestFitColumns()
                End If
                '=================================================================================
                '===============================UOM==============================================
                If obj.ArrUomDetails IsNot Nothing AndAlso obj.ArrUomDetails.Count > 0 Then
                    For Each objtr As clsMccUOMDetails In obj.ArrUomDetails
                        gvUOM.Rows.AddNew()
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMColUnit).Value = objtr.UOM_Code
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMColUnitDesc).Value = objtr.UOM_Description
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMColConvFact).Value = objtr.Conversion_Factor
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMColStockUnit).Value = objtr.Stocking_Unit
                        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQtyForIsUOMUsed("'" + objtr.UOM_Code + "'", True))

                        'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        '    gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMColStockUnitChangable).Value = 1
                        'End If

                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMColStockUnitChangable).Value = 1

                    Next
                Else
                    gvUOM.Rows.AddNew()
                End If
                '=============================================================================
                '======================Bank Guarantee Details===========================
                Dim objB As List(Of clsBankGuaranteeMaster) = clsMccMaster.GetDataBankGuarantee(fndChillingVendor.Value, NavigatorType.Current)
                If Not IsNothing(objB) Then
                    TxtGuranteeAmount.Text = 0
                    For i As Integer = 0 To objB.Count - 1
                        gvBankG.Rows.Add(objB.Item(i).code, objB.Item(i).docdate, objB.Item(i).desc, objB.Item(i).Bank_Guarantee_Type, objB.Item(i).strtdate, objB.Item(i).enddate, objB.Item(i).extnddate, objB.Item(i).amount, objB.Item(i).remarks)
                        ' TxtGuranteeAmount.Text += clsCommon.myCdbl(objB.Item(i).amount)
                        If clsCommon.CompairString(objB.Item(i).Bank_Guarantee_Type, "Receiving") = CompairStringResult.Equal Then
                            TxtGuranteeAmount.Text += clsCommon.myCdbl(objB.Item(i).amount)
                        Else
                            txtBankrefundedAmount.Text += clsCommon.myCdbl(objB.Item(i).amount)
                        End If
                    Next
                    gvBankG.BestFitColumns()
                End If
                '==================================================================================================
                txtPayeeName.Text = obj.PayeeName
                txtBankCode.Value = obj.bankcode
                If clsCommon.myLen(txtBankCode.Value) > 0 Then
                    txtBankName.Text = clsDBFuncationality.getSingleValue("select Bank_Name from tspl_vendor_bank_master where bank_code='" & obj.bankcode & "'")
                End If
                TxtBranchCode.Text = obj.BankBranch
                txtBankBranchName.Text = clsDBFuncationality.getSingleValue("select branch_name from tspl_bank_branch_master where branch_code='" & TxtBranchCode.Text & "'")

                fndBankCity.Value = obj.BankCityCode
                If clsCommon.myLen(fndBankCity.Value) > 0 Then
                    txtBankCityName.Text = clsDBFuncationality.getSingleValue("select city_name from tspl_city_master where city_code='" & obj.BankCityCode & "'")
                End If
                fndBankState.Value = obj.BankStateCode
                If clsCommon.myLen(fndBankState.Value) > 0 Then
                    txtBankStateName.Text = clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" & obj.BankStateCode & "'")
                End If
                txtIFCICode.Text = obj.IFCICode
                txtAccountNo.Text = obj.AccountNO
                TxtFatSNFSaveDecimal.Text = obj.FAT_SNF_SAVED_DECIMAL
                TxtFatSnfCalcDecimal.Text = obj.FAT_SNF_CALC_DECIMAL

                '------------------25/06/2014 Monika--------BM00000002953--------------------
                txtBankCode.Value = obj.bankcode
                cmbagreemnt.SelectedValue = obj.agreemnt
                txtagrmnt_date.Text = obj.agrmnt_date
                txtexpir_date.Text = obj.expired_date
                cmbsecurity.SelectedValue = obj.secutiy
                txtchq_amt.Text = obj.chq_amt
                txtchq_date.Text = obj.chq_date
                txtchq_no.Text = obj.chq_no
                txtchillingRate.Text = obj.chilling_rate
                chkChillingMonthly.Checked = obj.is_Chilling_Provision_Monthly
                ' txtchilling_kg_ltr.Text = obj.chilling_kg_ltr
                'txtchilling_qty.Text = obj.chilling_qty
                txtchillingassur_qty.Text = obj.chilling_assur_qty
                txtchilling_period.Text = obj.chilling_assur_period
                DtpStartingDate.Value = clsCommon.myCDate(obj.Chilling_Period_Starting_Date)
                txtlease_chrg.Text = obj.lease_rate
                Dim indtrytype As String = ""
                Dim indutrypersn As String = ""
                Dim agrmnt As String = ""

                indtrytype = obj.industry_type
                indutrypersn = obj.industry_ppersn

                If clsCommon.CompairString(indtrytype, "Prop.") = CompairStringResult.Equal Then
                    rbtnprop.IsChecked = True
                    txtprop_name.Text = indutrypersn
                ElseIf clsCommon.CompairString(indtrytype, "Partnership") = CompairStringResult.Equal Then
                    rbtnpartnership.IsChecked = True
                    txtpartner_name.Text = indutrypersn
                ElseIf clsCommon.CompairString(indtrytype, "Public") = CompairStringResult.Equal Then
                    rbtnpublic.IsChecked = True
                    txtdirectr_name.Text = indutrypersn
                ElseIf clsCommon.CompairString(indtrytype, "Pvt") = CompairStringResult.Equal Then
                    rbtnpvt.IsChecked = True
                    txtdirectr_name.Text = indutrypersn
                End If
                '===================Rohit,22 Sep====================
                CmbOpenArea.SelectedValue = clsCommon.myCstr(obj.Unit_OpenAreaForTankerMovement)
                CmbMccArea.SelectedValue = clsCommon.myCstr(obj.Unit_MccSuperArea)
                CmbChilleronQty.SelectedValue = clsCommon.myCstr(obj.Unit_ChillingOnQty)
                CmbChillerOn.SelectedValue = clsCommon.myCstr(obj.Unit_ChillingOn)
                CmbChillerPeriod.SelectedValue = clsCommon.myCstr(obj.Unit_ChillingMinGuaranteePeriod)
                CmbRateOfLeasedCharges.SelectedValue = clsCommon.myCstr(obj.Unit_RateOfLeasedCharges)
                CmbArea_of_Store.SelectedValue = clsCommon.myCstr(obj.Unit_AreaOfStore)
                CmbArea_of_receiving.SelectedValue = clsCommon.myCstr(obj.Unit_AreaOfReceivingDock)
                CmbArea_of_Office.SelectedValue = clsCommon.myCstr(obj.Unit_AreaOfOffice)
                CmbArea_of_lab.SelectedValue = clsCommon.myCstr(obj.Unit_AreaOfLab)

                CboMachine.SelectedValue = IIf(clsCommon.myCstr(obj.Weighing_Machine) = "", "P", clsCommon.myCstr(obj.Weighing_Machine))
                cboSampleMachine.SelectedValue = IIf(clsCommon.myCstr(obj.Sample_Machine) = "", "E", clsCommon.myCstr(obj.Sample_Machine))
                CmbSampleComport.Text = obj.Sample_comport
                CmbSampleComport2.Text = obj.Default_Sample_Comport_2
                cboSampleMachine2.SelectedValue = IIf(obj.Default_Sample_Machine_2 = "", "E", clsCommon.myCstr(obj.Default_Sample_Machine_2))

                CmbSampleComport3.Text = obj.Default_Sample_Comport_3
                cboSampleMachine3.SelectedValue = IIf(obj.Default_Sample_Machine_3 = "", "E", clsCommon.myCstr(obj.Default_Sample_Machine_3))

                CmbSampleComport4.Text = obj.Default_Sample_Comport_4
                cboSampleMachine4.SelectedValue = IIf(obj.Default_Sample_Machine_4 = "", "E", clsCommon.myCstr(obj.Default_Sample_Machine_4))


                CmbWeighingComport.Text = obj.Weighing_Comport

                chkSeprateDockForCowAndBuffalo.Checked = obj.Is_Seprate_Dock_Cow_Buffalo
                chkAskSiloatShiftEnd.Checked = obj.AskSiloatShiftEnd
                CboMachineCow.SelectedValue = IIf(clsCommon.myCstr(obj.Weighing_Machine_Cow) = "", "P", clsCommon.myCstr(obj.Weighing_Machine_Cow))
                cboSampleMachineCow.SelectedValue = IIf(clsCommon.myCstr(obj.Sample_Machine_Cow) = "", "E", clsCommon.myCstr(obj.Sample_Machine_Cow))
                CmbSampleComportCow.Text = obj.Sample_comport_Cow
                CmbSampleComport2Cow.Text = obj.Default_Sample_Comport_2_Cow
                cboSampleMachine2Cow.SelectedValue = IIf(obj.Default_Sample_Machine_2_Cow = "", "E", clsCommon.myCstr(obj.Default_Sample_Machine_2_Cow))
                CmbWeighingComportCow.Text = obj.Weighing_Comport_Cow



                FndMpTermsCode.Value = obj.Deafault_MP_Terms_Code
                FndMPPaymentCycle.Value = obj.Deafault_MP_Payment_Cycle
                FndMPPaymentCode.Value = obj.Deafault_MP_Payment_Code
                FndMPGrpCode.Value = obj.Deafault_MP_Grp_Code

                TxtMPGrpCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select group_desc from Tspl_vendor_group where ven_group_code='" + clsCommon.myCstr(FndMPGrpCode.Value) + "'"))
                txtMPtermcodedes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Terms_Desc from TSPL_TERMS_MASTER where Terms_Code='" + clsCommon.myCstr(FndMpTermsCode.Value) + "'"))
                TxtMPPaymentCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select payment_desc from tspl_payment_code where payment_code='" + clsCommon.myCstr(FndMPPaymentCode.Value) + "'"))
                GetPaymentCycleData(False)
                fndpaymentCycle.Value = obj.Payment_Cycle
                FndIncentive.Value = obj.Incentive_Code
                ChkIsTruckSheet.Checked = IIf(clsCommon.myCstr(obj.Is_Truck_Sheet) = 1, True, False)
                ChkInactive.Checked = IIf(clsCommon.myCstr(obj.Inactive) = 1, True, False)
                ChkEMPONAmount.Checked = IIf(clsCommon.myCstr(obj.EmpOnAmountOnly) = "1", True, False)
                txtpan.Text = clsCommon.myCstr(obj.Pan_No)

                chkMilkReceiptWeightTolerance.Checked = obj.Receipt_Weight_tolerance_Apply
                txtMilkReceiptWeightTolerance.Value = obj.Receipt_Weight_tolerance_Value


                txtCommissionRate.Value = obj.Commission_Rate
                txtCommissionMinimumShiftInPaymentCycle.Value = obj.Commission_Minimum_Shift_In_Payment_Cycle
                txtCommissionMinimumQtyInShift.Value = obj.Commission_Minimum_Qty_In_Shift
                txtCommissionNoOfPaymentCycleForNewVSP.Value = obj.Commission_No_Of_Payment_Cycle_For_New_VSP

                txtDeductionRate.Value = obj.Deduction_Rate
                txtDeductionMinimumFATPer.Value = obj.Deduction_Minimum_FAT_Per
                txtDeductionMinimumSNFPer.Value = obj.Deduction_Minimum_SNF_Per
                txtDeductionNoOfPaymentCycleForNewVSP.Value = obj.Deduction_No_Of_Payment_Cycle_For_New_VSP


                txtDWIFrom1.Value = obj.Day_Wise_Incentive_From_1
                txtDWIFrom2.Value = obj.Day_Wise_Incentive_From_2
                txtDWIFrom3.Value = obj.Day_Wise_Incentive_From_3
                txtDWIFrom4.Value = obj.Day_Wise_Incentive_From_4
                txtDWIFrom5.Value = obj.Day_Wise_Incentive_From_5


                txtDWITo1.Value = obj.Day_Wise_Incentive_To_1
                txtDWITo2.Value = obj.Day_Wise_Incentive_To_2
                txtDWITo3.Value = obj.Day_Wise_Incentive_To_3
                txtDWITo4.Value = obj.Day_Wise_Incentive_To_4
                txtDWITo5.Value = obj.Day_Wise_Incentive_To_5

                txtDWIRate1.Value = obj.Day_Wise_Incentive_Rate_1
                txtDWIRate2.Value = obj.Day_Wise_Incentive_Rate_2
                txtDWIRate3.Value = obj.Day_Wise_Incentive_Rate_3
                txtDWIRate4.Value = obj.Day_Wise_Incentive_Rate_4
                txtDWIRate5.Value = obj.Day_Wise_Incentive_Rate_5

                txtCompanyVSPDeduction.Value = obj.Company_VSP_Deduction
                txtNonCompanyVSPDeduction.Value = obj.Non_Company_VSP_Deduction


                If clsCommon.myLen(clsCommon.myCstr(obj.Shift_Opening_Time)) > 0 Then
                    DtpShiftOpeningTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy ") & obj.Shift_Opening_Time
                End If
                If clsCommon.myLen(clsCommon.myCstr(obj.Shift_Closing_Time)) > 0 Then
                    DtpShiftClosingTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy ") & obj.Shift_Closing_Time
                End If

                If clsCommon.myLen(clsCommon.myCstr(obj.Shift_Eve_Opening_Time)) > 0 Then
                    DtpEveShiftOpenTiming.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy ") & obj.Shift_Eve_Opening_Time
                End If
                If clsCommon.myLen(clsCommon.myCstr(obj.Shift_Eve_Closing_Time)) > 0 Then
                    DtpEveShiftClosingTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy ") & obj.Shift_Eve_Closing_Time
                End If

                If obj.Shift_Default_Time_Morning IsNot Nothing Then
                    txtDefaultTimeMorningShift.Value = obj.Shift_Default_Time_Morning
                End If
                If obj.Shift_Default_Time_Evening IsNot Nothing Then
                    txtDefaultTimeEveningShift.Value = obj.Shift_Default_Time_Evening
                End If


                chkSuspense.Checked = obj.IsSuspense
                chkFailedSampleApply.Checked = obj.Failed_Sample_Apply
                txtFailedSampleFAT.Value = obj.Failed_Sample_FAT
                txtFailedSampleSNF.Value = obj.Failed_Sample_SNF

                '========================================
                '----------------------------------------------------------------
                btnSave.Text = "&Update"
                btnDelete.Enabled = True
                fndMCCCode.MyReadOnly = True
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.BlankAllControls()
                    UcCustomFields1.LoadData(obj.MCC_Code)
                End If
                UcAttachment1.LoadData(fndMCCCode.Value)
            Else
                reset()
                btnDelete.Enabled = False
                btnSave.Text = "&Save"
                fndMCCCode.MyReadOnly = False
                Throw New Exception("User have dont see  this '" & fndMCCCode.Value & "' MCC because user have not permission")
            End If
            isInsideLoadData = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndMCCCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndMCCCode._MYNavigator
        Try
            'Dim qst As String = " select tspl_mcc_master.MCC_Code as [Code]   From tspl_mcc_master   where 2=2"
            'Dim whrcls As String = ""
            'If clsCommon.myLen(arrLoc) > 0 Then
            '    whrcls = " and mcc_code in (" + arrLoc + ")"
            'End If
            'qst = qst + " " + whrcls
            'Select Case NavType
            '    Case NavigatorType.Current
            '        qst += " and tspl_mcc_master.MCC_Code in ('" + fndMCCCode.Value + "')"
            '    Case NavigatorType.Next
            '        qst += " and tspl_mcc_master.MCC_Code in (select min(MCC_Code ) from tspl_mcc_master where MCC_Code  >'" + fndMCCCode.Value + "' " + whrcls + ")"
            '    Case NavigatorType.First
            '        qst += " and tspl_mcc_master.MCC_Code in (select MIN(MCC_Code ) from tspl_mcc_master where 2=2 " + whrcls + ")"
            '    Case NavigatorType.Last
            '        qst += " and tspl_mcc_master.MCC_Code in (select Max(MCC_Code ) from tspl_mcc_master where 2=2 " + whrcls + ")"
            '    Case NavigatorType.Previous
            '        qst += " and tspl_mcc_master.MCC_Code in (select Max(MCC_Code ) from tspl_mcc_master where MCC_Code  <'" + fndMCCCode.Value + "' " + whrcls + ")"
            'End Select
            'fndMCCCode.Value = clsDBFuncationality.getSingleValue(qst)
            'If clsCommon.myLen(fndMCCCode.Value) > 0 Then
            '    loadData(fndMCCCode.Value, NavigatorType.Current)
            '    btnSave.Text = "&Update"
            '    fndMCCCode.MyReadOnly = True
            '    btnDelete.Enabled = True
            '    UcAttachment1.LoadData(fndMCCCode.Value)
            'Else
            '    btnSave.Text = "&Save"
            '    btnDelete.Enabled = False
            '    fndMCCCode.MyReadOnly = False
            '    reset()
            'End If
            '---no need of doing above code because the same code did at loaddata() of class
            loadData(fndMCCCode.Value, NavType)
            txtMCCCopy.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndChillingVendor__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndChillingVendor._MYOpenMasterForm
        Dim objvendor As New frmVendorMaster(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        objvendor.ChkChillingVendor.Checked = True
        objvendor.ChkChillingVendor.ReadOnly = True
        objvendor.is_For_Chilling_Vendor = True
        objvendor.Show()
    End Sub

    Private Sub fndChillingVendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndChillingVendor._MYValidating
        Try
            If clsCommon.CompairString(ddlMCCType.Text, "Co. Leased") = CompairStringResult.Equal Then
                fndChillingVendor.Value = clsVendorMaster.getFinder(" coalesce(Is_Chilling_Vendor,'0')='0' and coalesce(Vendor_Type_CHA,'')<>'CSA' and coalesce(Vendor_Type_CHA,'')<>'CHA' ", fndChillingVendor.Value, isButtonClicked)
                'fndChillingVendor.Value = clsVendorMaster.getFinder(" Is_Chilling_Vendor='1' ", fndChillingVendor.Value, isButtonClicked)
            Else
                fndChillingVendor.Value = clsVendorMaster.getFinder(" Is_Chilling_Vendor='1' ", fndChillingVendor.Value, isButtonClicked)
            End If

            If clsCommon.myLen(fndChillingVendor.Value) > 0 Then
                txtChillingVendorName.Text = clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" & fndChillingVendor.Value & "'")
                Dim objVendor As clsMccMaster = clsMccMaster.GetBankDetails(fndChillingVendor.Value)
                If Not IsNothing(objVendor) Then
                    txtPayeeName.Text = txtChillingVendorName.Text
                    txtBankCode.Value = objVendor.Vendor_Bank_Code
                    txtBankName.Text = objVendor.Vendor_Bank_name
                    txtBankBranchName.Text = objVendor.Vendor_Branch_Name
                    TxtBranchCode.Text = objVendor.Vendor_Branch_Name
                    fndBankCity.Value = objVendor.Vendor_Bank_City_Code
                    txtBankCityName.Text = objVendor.Vendor_Bank_City_Name
                    fndBankState.Value = objVendor.Vendor_Bank_State_Code
                    txtBankStateName.Text = objVendor.Vendor_Bank_State_Name
                    txtAccountNo.Text = objVendor.Vendor_Account_No
                    txtIFCICode.Text = objVendor.Vendor_IFSC_Code
                    txtpan.Text = objVendor.Pan_No
                End If
                TxtGuranteeAmount.Text = 0
                Dim obj As List(Of clsBankGuaranteeMaster) = clsMccMaster.GetDataBankGuarantee(fndChillingVendor.Value, NavigatorType.Current)
                If Not IsNothing(obj) Then
                    For i As Integer = 0 To obj.Count - 1
                        gvBankG.Rows.Add(obj.Item(i).code, obj.Item(i).docdate, obj.Item(i).desc, obj.Item(i).Bank_Guarantee_Type, obj.Item(i).strtdate, obj.Item(i).enddate, obj.Item(i).extnddate, obj.Item(i).amount, obj.Item(i).remarks)
                        TxtGuranteeAmount.Text += clsCommon.myCdbl(obj.Item(i).amount)
                    Next
                    gvBankG.BestFitColumns()
                End If
                'txtChillingVendorName.Text = obj.vndrname
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankBankGuaranteeGrid()
        Try
            gvBankG.Rows.Clear()
            gvBankG.Columns.Clear()
            gvBankG.Columns.Add("COLBankNO", "Bank Guarantee NO")
            gvBankG.Columns.Add("COLBankDate", "Date")
            gvBankG.Columns.Add("COLDesc", "Description")
            gvBankG.Columns.Add("COLGuaranteeType", "Bank Guarantee Type")
            gvBankG.Columns.Add("COLStartDate", "Start Date")
            gvBankG.Columns.Add("COLEndDate", "End Date")
            gvBankG.Columns.Add("COLExtEndDate", "Extended End Date")
            gvBankG.Columns.Add("COLAmount", "Amount")
            gvBankG.Columns.Add("COLremarks", "Remarks")


            gvBankG.Columns("COLBankNO").Width = 100
            gvBankG.Columns("COLBankDate").Width = 100
            gvBankG.Columns("COLDesc").Width = 100
            gvBankG.Columns("COLStartDate").Width = 100
            gvBankG.Columns("COLEndDate").Width = 100
            gvBankG.Columns("COLExtEndDate").Width = 100
            gvBankG.Columns("COLAmount").Width = 100
            gvBankG.Columns("COLremarks").Width = 100
            gvBankG.Columns("COLGuaranteeType").Width = 100

            gvBankG.AllowAddNewRow = False
            gvBankG.AllowEditRow = False
            gvBankG.AllowDeleteRow = False
            gvBankG.AllowRowResize = False
            gvBankG.AllowRowReorder = False
            gvBankG.AllowColumnResize = False
            gvBankG.AllowColumnChooser = False
            gvBankG.AllowAutoSizeColumns = True
            gvBankG.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCountry__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCountry._MYOpenMasterForm
        Frm_Open = New frmCountryMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmCountryMaster)
        Frm_Open.Show()
    End Sub

    Private Sub fndCountry__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCountry._MYValidating, TxtFinder9._MYValidating, TxtFinder4._MYValidating
        Try
            fndCountry.Value = clsCountryMaster.getFinder("", fndCountry.Value, isButtonClicked)
            If clsCommon.myLen(fndCountry.Value) > 0 Then
                txtCountryName.Text = clsDBFuncationality.getSingleValue("select Country_name from tspl_country_master where country_code='" & fndCountry.Value & "'")
                txtStateName.Text = ""
                txtCityName.Text = ""
                fndState.Value = ""
                fndCity.Value = ""
            Else '-------25/06/2014 Monika
                txtCountryName.Text = ""
                txtStateName.Text = ""
                txtCityName.Text = ""
                fndState.Value = ""
                fndCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndState__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndState._MYValidating, TxtFinder8._MYValidating, TxtFinder5._MYValidating
        Try
            If clsCommon.myLen(fndCountry.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Country First..", Me.Text)
                fndCountry.Focus()
                Exit Sub
            End If
            Dim whrcls As String = "country_code='" & clsCommon.myCstr(fndCountry.Value) & "'"

            fndState.Value = clsStateMaster.getFinder(whrcls, fndState.Value, isButtonClicked)
            If clsCommon.myLen(fndState.Value) > 0 Then
                txtStateName.Text = clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" & fndState.Value & "'")
                fndCity.Value = ""
                txtCityName.Text = ""
            Else '--------25/06/2014 Monika
                txtStateName.Text = ""
                txtCityName.Text = ""
                fndState.Value = ""
                fndCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCity__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCity._MYValidating, TxtFinder7._MYValidating, TxtFinder6._MYValidating
        Try
            'If clsCommon.myLen(fndCountry.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, "Please Select Country First..")
            '    fndCountry.Focus()
            '    Exit Sub
            'End If
            'If clsCommon.myLen(fndState.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow(me, "Please Select state First..")
            '    fndState.Focus()
            '    Exit Sub
            'End If
            'Dim whrcls As String = "state_code='" & clsCommon.myCstr(fndState.Value) & "'"
            Dim whrcls As String = ""
            fndCity.Value = clsCityMaster.getFinder(whrcls, fndCity.Value, isButtonClicked)
            If clsCommon.myLen(fndCity.Value) > 0 Then
                Dim obj As clsCityMaster = clsCityMaster.GetData(fndCity.Value, NavigatorType.Current)
                If Not IsNothing(obj) Then
                    txtCityName.Text = obj.City_Name
                    fndState.Value = obj.STATE_CODE
                    txtStateName.Text = obj.STATE_NAME
                    fndCountry.Value = clsDBFuncationality.getSingleValue("select country_code from tspl_State_master where state_Code='" & fndState.Value & "'")
                    txtCountryName.Text = clsDBFuncationality.getSingleValue("select country_name from tspl_Country_master where Country_Code='" & fndCountry.Value & "'")
                End If
                'txtCityName.Text = clsDBFuncationality.getSingleValue("select  city_name from tspl_city_master where city_code='" & fndCity.Value & "'")
            Else '------25/06/2014 Monika
                txtCityName.Text = ""
                fndCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBankCode__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtBankCode._MYOpenMasterForm
        Frm_Open = New FrmVendorBankMaster
        Frm_Open.Show()
    End Sub

    Private Sub txtBankCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBankCode._MYValidating
        Try
            txtBankCode.Value = clsVendorBankMaster.GetFinder("", txtBankCode.Value, isButtonClicked)
            If clsCommon.myLen(txtBankCode.Value) >= 0 Then
                txtBankName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Bank_Name from tspl_vendor_bank_master where Bank_Code='" & txtBankCode.Value & "'"))
                'fndBankState.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state from tspl_bank_master where bank_code='" & txtBankCode.Value & "'"))
                'txtBankStateName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" & fndBankState.Value & "'"))
                'fndBankCity.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select city from tspl_bank_master where bank_code='" & txtBankCode.Value & "'"))
                'txtBankCityName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  city_name from tspl_city_master where city_code='" & fndBankCity.Value & "' and state_code='" & fndBankState.Value & "'"))
                'txtAccountNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANKACCNUMBER from tspl_bank_master where bank_code='" & txtBankCode.Value & "'"))
                'TxtBranchCode.Text = ""
                'txtBankBranchName.Text = ""
                'txtIFCICode.Text = ""
                'TxtBranchCode.Focus()
            Else
                txtBankName.Text = ""
                'TxtBranchCode.Text = ""
                'txtBankBranchName.Text = ""
                'txtIFCICode.Text = ""
                'fndBankState.Value = ""
                'txtBankStateName.Text = ""
                'fndBankCity.Value = ""
                'txtBankCityName.Text = ""
                'txtAccountNo.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndBankState__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBankState._MYOpenMasterForm, fndState._MYOpenMasterForm
        Frm_Open = New frmStateMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmStateMaster)
        Frm_Open.Show()
    End Sub

    Private Sub fndBankState__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndBankState._MYValidating
        Try
            fndBankState.Value = clsStateMaster.getFinder("", fndBankState.Value, isButtonClicked)
            If clsCommon.myLen(fndBankState.Value) > 0 Then
                txtBankStateName.Text = clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" & fndBankState.Value & "'")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub fndBankCity__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBankCity._MYOpenMasterForm, fndCity._MYOpenMasterForm
        Frm_Open = New frmCityMaster(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        Frm_Open.SetUserMgmt(clsUserMgtCode.cityMaster)
        Frm_Open.Show()
    End Sub

    Private Sub fndBankCity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndBankCity._MYValidating
        Try
            If clsCommon.myLen(fndBankState.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select state of Bank  First..", Me.Text)
                fndBankState.Focus()
                Exit Sub
            End If
            Dim whrcls As String = "state_code='" & clsCommon.myCstr(fndBankState.Value) & "'"
            fndBankCity.Value = clsCityMaster.getFinder(whrcls, fndBankCity.Value, isButtonClicked)
            If clsCommon.myLen(fndBankCity.Value) > 0 Then
                txtBankCityName.Text = clsDBFuncationality.getSingleValue("select  city_name from tspl_city_master where city_code='" & fndBankCity.Value & "'")
            Else
                txtBankCityName.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDgGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDgGo.Click
        Try
            If clsCommon.myCdbl(txtNoOfDG.Text) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Value Of  No of DG Set must be >0", Me.Text)
                txtNoOfDG.Focus()
                Exit Sub
            End If
            Dim i As Integer = 0
            If clsCommon.myCdbl(txtNoOfDG.Text) > dgvGenSet.Rows.Count Then
                For i = dgvGenSet.Rows.Count + 1 To clsCommon.myCdbl(txtNoOfDG.Text)
                    dgvGenSet.Rows.AddNew()
                    dgvGenSet.Rows(i - 1).Cells("COLSLNO").Value = i
                    dgvGenSet.Rows(i - 1).Cells("COLSLNO").ReadOnly = True
                Next
            ElseIf clsCommon.myCdbl(txtNoOfDG.Text) < dgvGenSet.Rows.Count Then
                For i = dgvGenSet.Rows.Count - 1 To clsCommon.myCdbl(txtNoOfDG.Text) Step -1
                    dgvGenSet.Rows.RemoveAt(i)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnCompressorGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompressorGo.Click
        Try
            If clsCommon.myCdbl(txtNoOfCompressor.Text) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Value of No of Compressor must be >0", Me.Text)
                txtNoOfCompressor.Focus()
                Exit Sub
            End If
            Dim i As Integer = 0
            If clsCommon.myCdbl(txtNoOfCompressor.Text) > dgvCompressor.Rows.Count Then
                For i = dgvCompressor.Rows.Count + 1 To clsCommon.myCdbl(txtNoOfCompressor.Text)
                    dgvCompressor.Rows.AddNew()
                    dgvCompressor.Rows(i - 1).Cells("COLSLNO").Value = i
                    dgvCompressor.Rows(i - 1).Cells("COLSLNO").ReadOnly = True
                Next
            ElseIf clsCommon.myCdbl(txtNoOfCompressor.Text) < dgvCompressor.Rows.Count Then
                For i = dgvCompressor.Rows.Count - 1 To clsCommon.myCdbl(txtNoOfCompressor.Text) Step -1
                    dgvCompressor.Rows.RemoveAt(i)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub dgvGenSet_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles dgvGenSet.UserDeletedRow
        Try
            Dim i As Integer = 0
            For i = 0 To dgvGenSet.Rows.Count - 1
                dgvGenSet.Rows(i).Cells("COLSLNO").Value = (i + 1)
                dgvGenSet.Rows(i).Cells("COLSLNO").ReadOnly = True
            Next
            txtNoOfDG.Text = dgvGenSet.Rows.Count
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub dgvGenSet_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvGenSet.UserDeletingRow
        Try
            If clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub dgvCompressor_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles dgvCompressor.UserDeletedRow
        Try
            Dim i As Integer = 0
            For i = 0 To dgvCompressor.Rows.Count - 1
                dgvCompressor.Rows(i).Cells("COLSLNO").Value = (i + 1)
                dgvCompressor.Rows(i).Cells("COLSLNO").ReadOnly = True
            Next
            txtNoOfCompressor.Text = dgvCompressor.Rows.Count
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub dgvCompressor_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvCompressor.UserDeletingRow
        Try
            If clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ddlMCCType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlMCCType.SelectedIndexChanged
        Try
            If clsCommon.CompairString(ddlMCCType.Text, "Chilling Basis") = CompairStringResult.Equal Then
                fndChillingVendor.Visible = True
                '-------25/06/2014 Monika
                txtNoOfChiller.Visible = True
                txtChillerBrandName.Visible = True
                txtChillerCapacity.Visible = True
                txtchillingRate.Visible = True
                chkChillingMonthly.Visible = True
                ' txtchilling_kg_ltr.Visible = True
                'txtchilling_qty.Visible = True
                txtchillingassur_qty.Visible = True
                txtchilling_period.Visible = True

                lblChillingVendor.Visible = True
                LblChillingQt.Visible = True
                LblChillingMuranteeQty.Visible = True
                LblChillingMinPeriod.Visible = True
                LblChillingCharges.Visible = True
                LblChillingOn.Visible = True
                MyLabel53.Visible = True
                CmbChillerOn.Visible = True
                CmbChilleronQty.Visible = True
                CmbChillerPeriod.Visible = True
                txtChillingVendorName.Visible = True
                txtChillingVendorName.Visible = True
                CmbRateOfLeasedCharges.Visible = False 'True
                MyLabel33.Visible = False
                txtlease_chrg.Visible = False
                DtpStartingDate.Visible = True
                DtpStartingDate.Enabled = True


                ' BtnAddNewVendor.Visible = True
                RadPageView1.Pages("Pg_bank").Item.Visibility = ElementVisibility.Visible
                RadPageView1.Pages("Pg_Bankg").Item.Visibility = ElementVisibility.Visible
                'RadPageView1.Pages("Pg_Chiller").Item.Visibility = ElementVisibility.Visible
                RadPageView2.Pages("Pg_Chillers").Item.Visibility = ElementVisibility.Visible
                RadPageView1.Pages("Payment_Entry").Item.Visibility = ElementVisibility.Visible
                GrpIndustry.Visible = True
                fndChillingVendor.Value = Nothing
                txtChillingVendorName.Text = ""
                txtChillerCapacity.Text = Nothing
                'txtchilling_kg_ltr.Text = Nothing
                txtchilling_period.Text = Nothing
                'txtchilling_qty.Text = Nothing
                txtchillingassur_qty.Text = Nothing
                txtchillingRate.Text = Nothing
                chkChillingMonthly.Checked = False

                txtChillingVendorName.Text = Nothing
            ElseIf clsCommon.CompairString(ddlMCCType.Text, "Co. Leased") = CompairStringResult.Equal Then

                MyLabel33.Visible = True
                txtNoOfChiller.Visible = False 'True
                txtChillerBrandName.Visible = False 'True
                txtChillerCapacity.Visible = False 'True
                txtchillingRate.Visible = False 'True
                chkChillingMonthly.Visible = False
                'txtchilling_kg_ltr.Visible = True
                'txtchilling_qty.Visible = True
                txtchillingassur_qty.Visible = False 'True
                txtchilling_period.Visible = False 'True
                ' BtnAddNewVendor.Visible = True
                DtpStartingDate.Visible = False

                MyLabel53.Visible = False

                txtlease_chrg.Visible = True
                fndChillingVendor.Visible = True
                lblChillingVendor.Visible = True
                LblChillingQt.Visible = False 'True
                LblChillingMuranteeQty.Visible = False 'True
                LblChillingMinPeriod.Visible = False 'True
                LblChillingCharges.Visible = False 'True
                LblChillingOn.Visible = False 'True
                'MyLabel53.Visible = True
                CmbChillerOn.Visible = False ' True
                CmbChilleronQty.Visible = False 'True
                CmbChillerPeriod.Visible = False 'True
                txtChillingVendorName.Visible = True
                'txtChillingVendorName.Visible = True
                CmbRateOfLeasedCharges.Visible = True

                ' BtnAddNewVendor.Visible = True
                RadPageView1.Pages("Pg_bank").Item.Visibility = ElementVisibility.Visible
                RadPageView1.Pages("Pg_Bankg").Item.Visibility = ElementVisibility.Visible
                RadPageView1.Pages("Payment_Entry").Item.Visibility = ElementVisibility.Visible
                'RadPageView1.Pages("Pg_Chiller").Item.Visibility = ElementVisibility.Visible
                RadPageView2.Pages("Pg_Chillers").Item.Visibility = ElementVisibility.Visible
                GrpIndustry.Visible = True
                fndChillingVendor.Value = Nothing
                txtChillingVendorName.Text = ""
                txtChillerCapacity.Text = Nothing
                'txtchilling_kg_ltr.Text = Nothing
                txtchilling_period.Text = Nothing
                'txtchilling_qty.Text = Nothing
                txtchillingassur_qty.Text = Nothing
                txtchillingRate.Text = Nothing
                chkChillingMonthly.Checked = False

                txtChillingVendorName.Text = Nothing
                '---------------------------------End Here
            Else
                MyLabel33.Visible = False
                txtlease_chrg.Visible = False
                fndChillingVendor.Visible = False
                txtNoOfChiller.Visible = False
                txtChillerBrandName.Visible = False
                txtChillerCapacity.Visible = False
                txtchillingRate.Visible = False
                chkChillingMonthly.Visible = False
                'txtchilling_kg_ltr.Visible = False
                'txtchilling_qty.Visible = False
                txtchillingassur_qty.Visible = False
                txtchilling_period.Visible = False
                BtnAddNewVendor.Visible = False
                DtpStartingDate.Visible = False
                fndChillingVendor.Value = Nothing
                txtChillingVendorName.Text = ""
                txtChillerCapacity.Text = Nothing
                'txtchilling_kg_ltr.Text = Nothing
                txtchilling_period.Text = Nothing
                'txtchilling_qty.Text = Nothing
                txtchillingassur_qty.Text = Nothing
                txtchillingRate.Text = Nothing
                chkChillingMonthly.Checked = False

                txtChillingVendorName.Text = Nothing
                RadPageView1.Pages("Pg_bank").Item.Visibility = ElementVisibility.Collapsed
                RadPageView1.Pages("Pg_Bankg").Item.Visibility = ElementVisibility.Collapsed
                RadPageView1.Pages("Payment_Entry").Item.Visibility = ElementVisibility.Collapsed
                'RadPageView1.Pages("Pg_Chiller").Item.Visibility = ElementVisibility.Collapsed
                RadPageView2.Pages("Pg_Chillers").Item.Visibility = ElementVisibility.Collapsed
                GrpIndustry.Visible = False
                lblChillingVendor.Visible = False
                LblChillingQt.Visible = False
                LblChillingMuranteeQty.Visible = False
                LblChillingMinPeriod.Visible = False
                LblChillingCharges.Visible = False
                CmbChillerOn.Visible = False
                CmbChilleronQty.Visible = False
                CmbChillerPeriod.Visible = False
                CmbRateOfLeasedCharges.Visible = False

                txtChillingVendorName.Visible = False
                txtChillingVendorName.Visible = False
                LblChillingOn.Visible = False
                MyLabel53.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If clsMccMaster.deleteData(fndMCCCode.Value, Me.Form_ID) Then
                clsCommon.MyMessageBoxShow(Me, "Deleted Successfully", Me.Text)
                reset()
            Else
                clsCommon.MyMessageBoxShow(Me, "Delete Unsuccessful.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadPageViewPage1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles RadPageViewPage1.Paint

    End Sub




    Private Sub txtMCCArea_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtMCCArea.Validating

        If clsCommon.myCdbl(txtMCCArea.Text) = 0 Or clsCommon.myLen(txtMCCArea.Text) <= 0 Then
            ErrorProvider1.SetError(TryCast(sender, Control), "Please Enter A Valid Numeric Value and it is Manadatory...")
        Else

            ErrorProvider1.SetError(TryCast(sender, Control), "")
        End If
    End Sub


    Private Sub mnuMccDetailExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFile.Click

    End Sub


    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Try
            '---------------26/06/2014-----------Monika--------------blank export option and new fields addition
            Dim str As String = "select count(*) from tspl_mcc_master"
            Dim check As Integer = clsDBFuncationality.getSingleValue(str)

            If check > 0 Then
                str = "select TSPL_MCC_MASTER.MCC_Code as [Mcc Code] ,TSPL_MCC_MASTER.MCC_Type as [Mcc Type] ,TSPL_MCC_MASTER.MCC_NAME as [Mcc Name] ," _
                & " TSPL_MCC_MASTER.Chilling_Vendor as [Chilling Vendor Code] ,TSPL_MCC_MASTER.Add1 as [Address1] ,TSPL_MCC_MASTER.Add2 as [Address2] ," _
                & " TSPL_MCC_MASTER.Tehsil as [Tehsil] ,TSPL_MCC_MASTER.City_code as [City Code] ,TSPL_MCC_MASTER.State_Code as [State Code] ," _
                & " TSPL_MCC_MASTER.Country_code as [Country Code] ,TSPL_MCC_MASTER.Pin_code as [Pin Code] ,TSPL_MCC_MASTER.Telphone as [Telphone] ," _
                & " TSPL_MCC_MASTER.Email as [Email] ,TSPL_MCC_MASTER.Fax as [Fax] ,TSPL_MCC_MASTER.MCC_Area as [Mcc Super Area],case when Tspl_mcc_master.unit_MccSuperArea='M' then 'Sq. Mt.' else 'Sq. Ft.' end as [Mcc Super Area UOM(Sq. Ft./Sq. Mt.)]  ,TSPL_MCC_MASTER.Area_Of_Store" _
               & " as [Area Of Store],case when Tspl_mcc_master.unit_AreaofStore='M' then 'Sq. Mt.' else 'Sq. Ft.' end as [Area of Store UOM(Sq. Ft./Sq. Mt.)]  ,TSPL_MCC_MASTER.Area_Of_Office as [Area Of Office],case when Tspl_mcc_master.unit_AreaofOffice='M' then 'Sq. Mt.' else 'Sq. Ft.' end as [Area of Office UOM(Sq. Ft./Sq. Mt.)] ,TSPL_MCC_MASTER.Open_Area_For_tanker as [Open Area For Tanker],case when Tspl_mcc_master.unit_OpenAReaforTankermovement='M' then 'Sq. Mt.' else 'Sq. Ft.' end as [Open Area For Tanker UOM(Sq. Ft./Sq. Mt.)]" _
                & " ,TSPL_MCC_MASTER.Area_Of_LAB as [Area Of Lab],case when Tspl_mcc_master.unit_AreaofLab='M' then 'Sq. Mt.' else 'Sq. Ft.' end as [Area of Lab UOM(Sq. Ft./Sq. Mt.)]  ,TSPL_MCC_MASTER.Total_Storage_capacity as " _
                & " [Total Storage Capacity] ,TSPL_MCC_MASTER.Area_Of_Receiving_DOCK as [Area Of Receiving Dock],case when Tspl_mcc_master.unit_AreaofReceivingDock='M' then 'Sq. Mt.' else 'Sq. Ft.' end as [Area of Receiving Dock UOM(Sq. Ft./Sq. Mt.)]  ," _
                & " TSPL_MCC_MASTER.DripSaver as [Drip Saver (Yes/No)] ,TSPL_MCC_MASTER.CanWasher as [Can Washer (Yes/No)] ," _
                & " TSPL_MCC_MASTER.CanScrubber as [Can Scrubber (Yes/No)] ,TSPL_MCC_MASTER.FSSAI_NO as [Fssai No] ,TSPL_MCC_MASTER.ETP as [ETP (Yes/No)] " _
                & " ,TSPL_MCC_MASTER.Earthing as [Earthing (Yes/No)] ,TSPL_MCC_MASTER.Coil_Length as [Coil Length] ,TSPL_MCC_MASTER.Electricity_Connection " _
                & " as [Electricity Connection] ,TSPL_MCC_MASTER.Boiler as [Boiler (Yes/No)],TSPL_MCC_MASTER.industry_type as " _
                & " [Industry Type],(case when TSPL_MCC_MASTER.industry_type='Prop.' then TSPL_MCC_MASTER.industry_person else '' end) as [Prop Name]," _
                & " (case when TSPL_MCC_MASTER.industry_type='partnership' then TSPL_MCC_MASTER.industry_person else '' end) as [Partner Name]," _
                & " (case when (TSPL_MCC_MASTER.industry_type='Public' or TSPL_MCC_MASTER.industry_type='Pvt') then TSPL_MCC_MASTER.industry_person else '' " _
                & " end) as [Director Name],case when isnull( TSPL_MCC_MASTER.is_Chilling_Provision_Monthly,0)=1 then 'Y' else 'N' end as [Monthly Provision(Y/N)] , TSPL_MCC_MASTER.chilling_rate as [Chilling Charges],TSPL_MCC_MASTER.chilling_kg_ltr as [Chilling On],Case When TSPL_MCC_MASTER.Unit_Chillingon='K' then 'KG' else 'LTR' end as [Chilling On UOM(KG/LTR)]," _
                & " TSPL_MCC_MASTER.chilling_dispatch_qty as [Chilling On Qty],Case when TSPL_MCC_MASTER.Unit_ChillingOnQty='H' then 'Handled' else 'Dispatched' end as [Chilling On UOM(Handled/Dispatched)],TSPL_MCC_MASTER.chilling_assure_qty as [Chilling Min. Guaranteed Avg. Qty]," _
                & " TSPL_MCC_MASTER.chilling_assure_period as [Chilling Min. Guaranteed Period],TSPL_MCC_MASTER.chilling_assure_period as [Chilling Min. Guaranteed Period UOM (Day/Month/Year)],Chilling_Period_Starting_Date as [Chilling Starting Date],TSPL_MCC_MASTER.lease_rate as [Rate of Lease Charges],Case when tspl_mcc_master.Unit_rateofLeasedCharges='D' then 'Day' when tspl_mcc_master.Unit_rateofLeasedCharges='M' then 'Month' else 'Year' end as [Rate of Leased Charges UOM(Day/Month/Year)],TSPL_MCC_MASTER.Agreement_Status" _
                & " ,TSPL_MCC_MASTER.Agreement_Date,TSPL_MCC_MASTER.agrmnt_expired_date as [Agreement Expiry Date],TSPL_MCC_MASTER.Security_Status," _
                & " TSPL_MCC_MASTER.Cheque_Amt,TSPL_MCC_MASTER.Cheque_No,TSPL_MCC_MASTER.Cheque_Date,case when coalesce(Is_Truck_Sheet_Mandatory,0)=1 then 'Yes' Else 'No' end as [Is Truck Sheet Mandatory],case when Default_Weighing_machine='P' then 'Prompt' when Default_Weighing_machine='D' then 'Delta' when Default_Weighing_machine='B' then 'Panasonic' when Default_Weighing_machine='S' then 'Supertech'  when Default_Weighing_machine='C' then 'Everest' end as [Weighing Machine],case when Default_Sample_machine='E' then 'Everest Old'  when Default_Sample_machine='N' then 'Everest New' when Default_Sample_machine='K' then 'Kanha' end as [Sample Machine],Default_Weighing_Comport as [Weighing ComPort],Default_Sample_Comport as [Sample ComPort],Payment_Cycle as [Payment Cycle],Incentive_Code as [Incentive Code],Shift_Opening_Time as [Shift Morning Opening Time],Shift_Closing_Time as [Shift Morning Closing Time],Shift_Eve_Opening_Time as [Shift Evening Opening Time],Shift_Eve_Closing_Time as [Shift Evening Closing Time] ,TSPL_MCC_MASTER.emp_code as [RM],(case when TSPL_MCC_MASTER.is_Reuired_Gate_Entry=1 then 'Yes' else 'No' end ) as [Required Gate Entry(Yes/No)], " &
                " TSPL_MCC_MASTER.AllowAutoMilkIn,TSPL_MCC_MASTER.AutoIn_Location, TSPL_MCC_MASTER.SILOIn_Location,case when TSPL_MCC_MASTER.Receipt_Weight_tolerance_Apply=1 then 'Y' else 'N' end as [ApplyReceiptWeightTolerance(Y/N)],TSPL_MCC_MASTER.Receipt_Weight_tolerance_Value as ReceiptWeightToleranceValue,case when isnull(Failed_Sample_Apply,0)=0 then 'N' else 'Y' end as [Apply Failed Sample(Y/N)],Failed_Sample_FAT as [Failed Sample FAT %],Failed_Sample_SNF as [Failed Sample SNF %], TSPL_LOCATION_MASTER.Loc_Segment_Code as [Loc Segment Code],TSPL_MCC_MASTER.Is_MCC as [MCC/BMCC] " + Environment.NewLine +
                " ,TSPL_MCC_MASTER.Commission_Rate as CommissionRate,TSPL_MCC_MASTER.Commission_Minimum_Shift_In_Payment_Cycle as CommissionMinimumShiftInPaymentCycle,TSPL_MCC_MASTER.Commission_Minimum_Qty_In_Shift as CommissionMinimumQtyInShift,TSPL_MCC_MASTER.Commission_No_Of_Payment_Cycle_For_New_VSP as CommissionNoOfPaymentCycleForNewVSP,TSPL_MCC_MASTER.Deduction_Minimum_FAT_Per as DeductionMinimumFATPer,TSPL_MCC_MASTER.Deduction_Minimum_SNF_Per as DeductionMinimumSNFPer,TSPL_MCC_MASTER.Deduction_No_Of_Payment_Cycle_For_New_VSP as DeductionNoOfPaymentCycleForNewVSP,TSPL_MCC_MASTER.Tub_Capacity as [Tub Capacity] " + Environment.NewLine +
                " From TSPL_MCC_MASTER left outer join TSPL_LOCATION_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_LOCATION_MASTER.Location_Code"
            Else
                str = "select '' as [Mcc Code] ,'' as [Mcc Type] ,'' as [Mcc Name] ," _
                & " '' as [Chilling Vendor Code] ,'' as [Address1] ,'' as [Address2] ," _
                & " '' as [Tehsil] ,'' as [City Code] ,'' as [State Code] ," _
                & " '' as [Country Code] ,'' as [Pin Code] ,'' as [Telphone] ," _
                & " '' as [Email] ,'' as [Fax] ,'' as [Mcc Super Area],'' as [Mcc Super Area UOM(Sq. Ft./Sq. Mt.)] ,''" _
                & " as [Area Of Store] ,'' as [Area of Store UOM(Sq. Ft./Sq. Mt.)],'' as [Area Of Office] ,'' as [Area of Office UOM(Sq. Ft./Sq. Mt.)],'' as [Open Area For Tanker],'' as [Open Area For Tanker UOM(Sq. Ft./Sq. Mt.)]" _
                & " ,'' as [Area Of Lab] ,'' as [Area of Lab UOM(Sq. Ft./Sq. Mt.)],'' as " _
                & " [Total Storage Capacity] ,'' as [Area Of Receiving Dock] ,'' as [Area of Receiving Dock UOM(Sq. Ft./Sq. Mt.)]," _
                & " '' as [Drip Saver (Yes/No)] ,'' as [Can Washer (Yes/No)] ," _
                & " '' as [Can Scrubber (Yes/No)] ,'' as [Fssai No] ,'' as [ETP (Yes/No)] " _
                & " ,'' as [Earthing (Yes/No)] ,'' as [Coil Length] ,'' " _
                & " as [Electricity Connection] ,'' as [Boiler (Yes/No)],'' as " _
                & " [Industry Type],'' as [Prop Name]," _
                & " '' as [Partner Name]," _
                & " '' as [Director Name],'N' as [Monthly Provision(Y/N)],'' as [Chilling Charges],'' as [Chilling On],'' as [Chilling On UOM(KG\LTR)]," _
                & " '' as [Chilling On Qty],'' as [Chilling On UOM(Handled/Dispatched)],'' as [Chilling Min. Guaranteed Avg. Qty]," _
                & " '' as [Chilling Min. Guaranteed Period],'' as [Chilling Starting Date],'' as [Chilling Min. Guaranteed Period UOM (Day/Month/Year)],'' as [Rate of Lease Charges],'' as [Rate of Leased Charges UOM(Day/Month/Year],'' as Agreement_Status" _
                & " ,'' as Agreement_Date,'' as  [Agreement Expiry Date],'' as Security_Status," _
                & " '' as Cheque_Amt,'' as Cheque_No,'' as Cheque_Date,'' as [Weighing Machine],'' as [Sample Machine],'' as [Truck Sheet Mandatory],'' as [Weighing ComPort],'' as [Sample ComPort],''  as [Payment Cycle],''  as [Incentive Code],''  as [Shift Morning Opening Time],'' as [Shift Morning Closing Time] ,''  as [Shift Evening Opening Time],'' as [Shift Evening Closing Time],'' as RM,'' as [Required Gate Entry(Yes/No)],0 as AllowAutoMilkIn,'' as AutoIn_Location,'' as SILOIn_Location,'' as [ApplyReceiptWeightTolerance(Y/N)],'' as ReceiptWeightToleranceValue,'' as [Apply Failed Sample(Y/N)],0 as [Failed Sample FAT %],0 as [Failed Sample SNF %], '' as [Loc Segment Code],1 as [MCC/BMCC]  " _
                & " ,0 as CommissionRate,0 as CommissionMinimumShiftInPaymentCycle,0 as CommissionMinimumQtyInShift,0 as CommissionNoOfPaymentCycleForNewVSP,0 as DeductionMinimumFATPer,0 as DeductionMinimumSNFPer,0 as DeductionNoOfPaymentCycleForNewVSP,0 as [Tub Capacity] "
            End If
            ListImpExpColumnsMandatory = New List(Of String)({"Mcc Code", "Mcc Type", "Chilling Vendor Code", "Address1", "City Code", "State Code", "Country Code", "Pin Code", "Mcc Name", "Mcc Super Area", "Total Storage Capacity", "Drip Saver (Yes/No)", "Can Washer (Yes/No)", "Can Scrubber (Yes/No)", "ETP (Yes/No)", "Earthing (Yes/No)", "Boiler (Yes/No)"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"Mcc Code"})
            transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub mnuGenSetDetailExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGenSetDetailExport.Click
        Try
            '--------26/06/2014---Monika
            Dim str As String = "select count(*) from tspl_gen_set_detail"
            Dim check As Integer = clsDBFuncationality.getSingleValue(str)

            If check > 0 Then '----------ended
                str = "select tspl_gen_set_detail.Trans_Code as [MCC Code] ,tspl_gen_set_detail.Line_No as [Line No] ,tspl_gen_set_detail.Gen_Set_Desc as [Gen Set Desc]  From tspl_gen_set_detail"
            Else
                str = "select '' as [MCC Code] ,0 as [Line No] ,'' as [Gen Set Desc]"
            End If
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub mnuCompressorDetailExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuCompressorDetailExport.Click
        Try
            '--------26/06/2014---Monika
            Dim str As String = "select count(*) from TSPL_Compressor_Detail"
            Dim check As Integer = clsDBFuncationality.getSingleValue(str)

            If check > 0 Then
                str = "select TSPL_Compressor_Detail.Trans_Code as [MCC Code] ,TSPL_Compressor_Detail.Line_No as [Line No] ,TSPL_Compressor_Detail.Compressor_Desc as [Compressor Desc]  From TSPL_Compressor_Detail"
            Else
                str = "select '' as [MCC Code] ,0 as [Line No] ,'' as [Compressor Desc]"
            End If
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub mnuMccDetailsImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuMccDetailsImport.Click
        Dim qry As String
        Dim Strqry As String
        Dim dtDefaultUOM As DataTable = Nothing
        If objCommonVar.ApplyDefaultsInMaster = True Then
            Strqry = "select * from TSPL_UNIT_MASTER  WHERE IsDefault=1"
            dtDefaultUOM = clsDBFuncationality.GetDataTable(Strqry)
        End If

        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim i As Integer = 0
        connectSql.OpenConnection()

        Dim strdate As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        If transportSql.importExcel(gv, "Mcc Code", "Mcc Type", "Mcc Name", "Chilling Vendor Code", "Address1", "Address2", "Tehsil", "City Code", "State Code", "Country Code", "Pin Code", "Telphone", "Email", "Fax", "Mcc Super Area", "Area Of Store", "Area Of Office", "Open Area For Tanker", "Area Of Lab", "Total Storage Capacity", "Area Of Receiving Dock", "Drip Saver (Yes/No)", "Can Washer (Yes/No)", "Can Scrubber (Yes/No)", "Fssai No", "ETP (Yes/No)", "Earthing (Yes/No)", "Coil Length", "Electricity Connection", "Boiler (Yes/No)", "Industry Type", "Prop Name", "Partner Name", "Director Name", "Monthly Provision(Y/N)", "Chilling Charges", "Chilling On", "Chilling On UOM(KG/LTR)", "Chilling On Qty", "Chilling On UOM(Handled/Dispatched)", "Chilling Min. Guaranteed Period", "Chilling Min. Guaranteed Period UOM (Day/Month/Year)", "Rate of Lease Charges", "Agreement_Status", "Agreement_Date", "Agreement Expiry Date", "Security_Status", "Cheque_Amt", "Cheque_No", "Cheque_Date", "Chilling Starting Date", "Is Truck Sheet Mandatory", "Weighing ComPort", "Sample ComPort", "Payment Cycle", "Incentive Code", "Shift Morning Opening Time", "Shift Morning Closing Time", "Shift Evening Opening Time", "Shift Evening Closing Time", "RM", "Required Gate Entry(Yes/No)", "AllowAutoMilkIn", "AutoIn_Location", "SILOIn_Location", "ApplyReceiptWeightTolerance(Y/N)", "ReceiptWeightToleranceValue", "Apply Failed Sample(Y/N)", "Failed Sample FAT %", "Failed Sample SNF %", "Loc Segment Code", "MCC/BMCC", "CommissionRate", "CommissionMinimumShiftInPaymentCycle", "CommissionMinimumQtyInShift", "CommissionNoOfPaymentCycleForNewVSP", "DeductionMinimumFATPer", "DeductionMinimumSNFPer", "DeductionNoOfPaymentCycleForNewVSP", "Tub Capacity") Then

            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsMccMaster()
                    i = i + 1

                    Dim strData As String = clsCommon.myCstr(grow.Cells("State Code").Value)
                    If clsCommon.myLen(strData) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                        strData = clsStateMaster.GetDefault()
                    End If
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("State Code Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(strData) > 20 Then
                        Throw New Exception("State Code Can Not Be Larger Then 20 Charachter")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_state_master where state_code='" & strData & "'") = 0 Then
                        Throw New Exception("State Code Could Not Found In Master")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_state_master where state_code='" & strData & "' and country_code='" & clsCommon.myCstr(grow.Cells("Country Code").Value) & "'") = 0 Then
                        Throw New Exception("Invaid State Code : " & strData & " Against Country Code: " & clsCommon.myCstr(grow.Cells("Country Code").Value) & Environment.NewLine & " This State Is not Mapped With Specified Country First Map it in State Master ")
                    End If
                    obj.State_Code = strData
                    grow.Cells("State Code").Value = strData

                    obj.Is_MCC = IIf(clsCommon.myCdbl(grow.Cells("MCC/BMCC").Value) = 0, 0, 1)
                    strData = clsCommon.myCstr(grow.Cells("Mcc Code").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        If obj.Is_MCC = 1 Then
                            strData = clsERPFuncationality.GetNextCode(Nothing, strdate, clsDocType.MCCMaster, clsDocTransactionType.MCC, obj.State_Code, False, True, True)
                        Else
                            strData = clsERPFuncationality.GetNextCode(Nothing, strdate, clsDocType.MCCMaster, clsDocTransactionType.BMCU, obj.State_Code, False, True, True)
                        End If

                        If clsCommon.myLen(strData) <= 0 Then
                            Throw New Exception("Error In Document Code Genertion")
                        End If
                    End If
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Mcc Code Can Not Be Left Blank")
                    End If
                    'strData = clsCommon.myCstr(grow.Cells("MCC Code(For VLC Uploader)").Value)
                    'If clsCommon.myLen(strData) <= 0 Then
                    '    Throw New Exception("Mcc Code For VLC Uploader Can Not Be Left Blank")
                    'End If
                    'obj.MCC_Code_VLC_Uploader = strData
                    ''MCC Code(For VLC Uploader)
                    If clsCommon.myLen(strData) > 30 Then
                        Throw New Exception("Mcc Code Can Not Be Larger Then 30 Charachter")
                    End If
                    obj.MCC_Code = strData
                    strData = clsCommon.myCstr(grow.Cells("Mcc Type").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("MCC Type Can Not Be Left Blank")
                    End If
                    If clsCommon.CompairString(strData, "Co. Owned") = CompairStringResult.Equal Or clsCommon.CompairString(strData, "Co. Leased") = CompairStringResult.Equal Or clsCommon.CompairString(strData, "Chilling Basis") = CompairStringResult.Equal Or clsCommon.CompairString(strData, "Federation") = CompairStringResult.Equal Or clsCommon.CompairString(strData, "PPP") = CompairStringResult.Equal Or clsCommon.CompairString(strData, "IKP") = CompairStringResult.Equal Or clsCommon.CompairString(strData, "MPCS") = CompairStringResult.Equal Then
                        If clsCommon.myLen(grow.Cells("Chilling Vendor Code").Value) <= 0 AndAlso (clsCommon.CompairString(strData, "Chilling Basis") = CompairStringResult.Equal Or clsCommon.CompairString(strData, "Co. Leased") = CompairStringResult.Equal) Then
                            Throw New Exception("When MCC type is Chilling Basis/Co. Leased, It Must be Specified the chilling Vendor Code ")
                        End If
                    Else
                        Throw New Exception("MCC Type Can be Either of Co. Owned/Co. Leased/Chilling Basis/Federation/PPP/IKP/MPCS ")
                    End If
                    obj.MCC_Type = strData
                    obj.Chilling_Vendor = clsCommon.myCstr(grow.Cells("Chilling Vendor Code").Value)
                    strData = clsCommon.myCstr(grow.Cells("Mcc Name").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Mcc Name Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(strData) > 50 Then
                        Throw New Exception("Mcc Name Can Not Be Larger Then 50 Charachter")
                    End If
                    obj.MCC_NAME = strData
                    strData = clsCommon.myCstr(grow.Cells("Address1").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Address1 Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(strData) > 50 Then
                        Throw New Exception("Address1 Can Not Be Larger Then 50 Charachter")
                    End If
                    obj.Add1 = strData
                    If clsCommon.myLen(grow.Cells("Address2").Value) > 0 Then
                        obj.Add2 = clsCommon.myCstr(grow.Cells("Address2").Value)
                    End If
                    If clsCommon.myLen(grow.Cells("Tehsil").Value) > 0 Then
                        obj.Tehsil = grow.Cells("Tehsil").Value
                    End If
                    strData = clsCommon.myCstr(grow.Cells("Pin Code").Value)
                    'If clsCommon.myLen(strData) <= 0 Then
                    '    Throw New Exception("Pin Code Can Not Be Left Blank")
                    'End If
                    'If clsCommon.myLen(strData) > 20 Then
                    '    Throw New Exception("Pin code Can Not Be Larger Then 20 Charachter")
                    'End If
                    If clsCommon.myLen(strData) > 0 AndAlso (clsCommon.myLen(strData) < 6 Or clsCommon.myLen(strData) > 6) Then
                        Throw New Exception("Pin Code Must be 6 Char Length")
                    End If
                    obj.Pin_code = strData

                    'strData = clsCommon.myCstr(grow.Cells("State Code").Value)
                    'If clsCommon.myLen(strData) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                    '    strData = clsStateMaster.GetDefault()
                    'End If
                    'If clsCommon.myLen(strData) <= 0 Then
                    '    Throw New Exception("State Code Can Not Be Left Blank")
                    'End If
                    'If clsCommon.myLen(strData) > 20 Then
                    '    Throw New Exception("State Code Can Not Be Larger Then 20 Charachter")
                    'End If
                    'If clsDBFuncationality.getSingleValue("select count(*) from tspl_state_master where state_code='" & strData & "'") = 0 Then
                    '    Throw New Exception("State Code Could Not Found In Master")
                    'End If
                    'If clsDBFuncationality.getSingleValue("select count(*) from tspl_state_master where state_code='" & strData & "' and country_code='" & clsCommon.myCstr(grow.Cells("Country Code").Value) & "'") = 0 Then
                    '    Throw New Exception("Invaid State Code : " & strData & " Against Country Code: " & clsCommon.myCstr(grow.Cells("Country Code").Value) & Environment.NewLine & " This State Is not Mapped With Specified Country First Map it in State Master ")
                    'End If
                    'obj.State_Code = strData
                    'grow.Cells("State Code").Value = strData

                    strData = clsCommon.myCstr(grow.Cells("City Code").Value)
                    If clsCommon.myLen(strData) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                        strData = clsCityMaster.GetDefault()
                    End If
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("City Code Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(strData) > 20 Then
                        Throw New Exception("City Code Can Not Be Larger Then 20 Charachter")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_city_master where city_code='" & strData & "'") = 0 Then
                        Throw New Exception("City Code Could Not Found In Master")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_city_master where city_code='" & strData & "' and state_code='" & clsCommon.myCstr(grow.Cells("State Code").Value) & "'") = 0 Then
                        Throw New Exception("Invaid City Code : " & strData & " Against State Code: " & clsCommon.myCstr(grow.Cells("State Code").Value) & Environment.NewLine & " This City Is not Mapped With Specified State First Map it in City Master ")
                    End If
                    obj.City_code = strData

                    strData = clsCommon.myCstr(grow.Cells("Country Code").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Country Code Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(strData) > 20 Then
                        Throw New Exception("Country Code Can Not Be Larger Then 20 Charachter")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_country_master where country_code='" & strData & "'") = 0 Then
                        Throw New Exception("Country Code Could Not Found In Master")
                    End If
                    obj.Country_code = strData
                    If clsCommon.myLen(grow.Cells("Telphone").Value) > 0 Then
                        obj.Telphone = grow.Cells("Telphone").Value
                    End If
                    If clsCommon.myLen(grow.Cells("Email").Value) > 0 Then
                        Dim check As Match = Regex.Match(grow.Cells("Email").Value, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
                        If check.Success Then
                            obj.Email = clsCommon.myCstr(grow.Cells("Email").Value)
                        Else
                            Throw New Exception("Email Is In Invalid Format. It Shoud Be as UserName@Domain Format ")
                        End If
                    End If
                    If clsCommon.myLen(grow.Cells("Fax").Value) > 0 Then
                        obj.Fax = clsCommon.myCstr(grow.Cells("Fax").Value)
                    End If

                    'If clsCommon.myCdbl(grow.Cells("Mcc Super Area").Value) = 0 Then
                    '    Throw New Exception("MCC Super Area is Manadatory, It Must Not Be Zero")
                    'End If
                    obj.MCC_Area = clsCommon.myCdbl(grow.Cells("Mcc Super Area").Value)
                    obj.Area_Of_Store = clsCommon.myCdbl(grow.Cells("Area Of Store").Value)
                    obj.Area_Of_Office = clsCommon.myCdbl(grow.Cells("Area Of Office").Value)
                    obj.Open_Area_For_tanker = clsCommon.myCdbl(grow.Cells("Open Area For tanker").Value)
                    obj.Area_Of_LAB = clsCommon.myCdbl(grow.Cells("Area Of LAB").Value)
                    'If clsCommon.myCdbl(grow.Cells("No Of Silo").Value) = 0 Then
                    '    Throw New Exception("No Of SILO is Manadatory, It Must Not Be Zero")
                    'End If

                    obj.Tub_Capacity = clsCommon.myCdbl(grow.Cells("Tub Capacity").Value)

                    'If clsCommon.myCdbl(grow.Cells("Total Storage Capacity").Value) = 0 Then
                    '    Throw New Exception("Total Storage Capacity is Manadatory, It Must Not Be Zero")
                    'End If
                    obj.Total_Storage_capacity = clsCommon.myCdbl(grow.Cells("Total Storage Capacity").Value)
                    obj.Area_Of_Receiving_DOCK = clsCommon.myCdbl(grow.Cells("Area Of Receiving DOCK").Value)
                    'obj.No_Of_Chiller = clsCommon.myCdbl(grow.Cells("No Of Chiller").Value)
                    'obj.Chiller_Brand_Name = clsCommon.myCstr(grow.Cells("Chiller Brand Name").Value)
                    'obj.Chiller_Capacity = clsCommon.myCdbl(grow.Cells("Chiller Capacity").Value)
                    'obj.No_Of_MilkPump = clsCommon.myCdbl(grow.Cells("No Of Milk Pump").Value)
                    'obj.MilkPump_Capacity = clsCommon.myCdbl(grow.Cells("Milk Pump Capacity").Value)
                    obj.FSSAI_NO = clsCommon.myCstr(grow.Cells("FSSAI NO").Value)
                    obj.DripSaver = clsCommon.myCstr(grow.Cells("Drip Saver (Yes/No)").Value)
                    If clsCommon.myLen(obj.DripSaver) <= 0 Then
                        obj.DripSaver = "No"
                        ' Throw New Exception("Drip Saver is Manadatory, and it must not be blank")
                    ElseIf clsCommon.CompairString(obj.DripSaver, "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(obj.DripSaver, "No") = CompairStringResult.Equal Then

                    Else
                        Throw New Exception("Drip Saver is Manadatory, and it must be Either Yes Or No")
                    End If

                    obj.CanWasher = clsCommon.myCstr(grow.Cells("Can Washer (Yes/No)").Value)
                    If clsCommon.myLen(obj.CanWasher) <= 0 Then
                        obj.CanWasher = "No"
                        'Throw New Exception("Can Washer is Manadatory, and it must  not be Blank")
                    ElseIf clsCommon.CompairString(obj.CanWasher, "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(obj.CanWasher, "No") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Can Wahser is Manadatory, and it must be Either Yes Or No")
                    End If
                    obj.CanScrubber = clsCommon.myCstr(grow.Cells("Can Scrubber (Yes/No)").Value)
                    If clsCommon.myLen(obj.CanScrubber) <= 0 Then
                        obj.CanScrubber = "No"
                        'Throw New Exception("Can Scrubber is Manadatory, and it must  not be Blank")
                    ElseIf clsCommon.CompairString(obj.CanScrubber, "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(obj.CanScrubber, "No") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Can Scrubber is Manadatory, and it must be Either Yes Or No")
                    End If
                    obj.ETP = clsCommon.myCstr(grow.Cells("ETP (Yes/No)").Value)
                    If clsCommon.myLen(obj.ETP) <= 0 Then
                        obj.ETP = "No"
                        'Throw New Exception("ETP is Manadatory, and it must  not be Blank")
                    ElseIf clsCommon.CompairString(obj.ETP, "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(obj.ETP, "No") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("ETP is Manadatory, and it must be Either Yes Or No")
                    End If

                    obj.Earthing = clsCommon.myCstr(grow.Cells("Earthing (Yes/No)").Value)
                    If clsCommon.myLen(obj.Earthing) <= 0 Then
                        'Throw New Exception("Earthing is Manadatory, and it must  not be Blank")
                        obj.Earthing = "No"
                    ElseIf clsCommon.CompairString(obj.Earthing, "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Earthing, "No") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Earthing is Manadatory, and it must be Either Yes Or No")
                    End If























                    obj.Boiler = clsCommon.myCstr(grow.Cells("Boiler (Yes/No)").Value)
                    If clsCommon.myLen(obj.Boiler) <= 0 Then
                        'Throw New Exception("Boiler is Manadatory, and it must  not be Blank")
                        obj.Boiler = "No"
                    ElseIf clsCommon.CompairString(obj.Boiler, "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Boiler, "No") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Boiler is Manadatory, and it must be Either Yes Or No")
                    End If
                    obj.Failed_Sample_Apply = (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Apply Failed Sample(Y/N)").Value), "Y") = CompairStringResult.Equal)
                    If obj.Failed_Sample_Apply Then
                        obj.Failed_Sample_FAT = clsCommon.myCstr(grow.Cells("Failed Sample FAT %").Value)
                        If obj.Failed_Sample_FAT <= 0 Then
                            Throw New Exception("Please provide Failed Sample FAT %")
                        End If
                        obj.Failed_Sample_SNF = clsCommon.myCstr(grow.Cells("Failed Sample SNF %").Value)
                        If obj.Failed_Sample_SNF <= 0 Then
                            Throw New Exception("Please provide Failed Sample SNF %")
                        End If
                    End If

                    'obj.NoOfDG = clsCommon.myCdbl(grow.Cells("No of DG").Value)
                    'If obj.NoOfDG = 0 Then
                    '    Throw New Exception("No Of DG is Manadatory, and it must  not be Zero")
                    'End If
                    'obj.NoOfCompressor = clsCommon.myCdbl(grow.Cells("No of Compressor").Value)
                    'If obj.NoOfCompressor = 0 Then
                    '    Throw New Exception("No Of Compressor is Manadatory, and it must  not be Zero")
                    'End If

                    'obj.PayeeName = clsCommon.myCstr(grow.Cells("Payee Name").Value)
                    'If clsCommon.myLen(grow.Cells("Bank Code").Value) > 0 Then
                    '    If clsDBFuncationality.getSingleValue("select count(*) from tspl_bank_master where bank_code='" & grow.Cells("Bank Code").Value & "'") > 0 Then
                    '        If clsCommon.myLen(grow.Cells("Bank Branch Code").Value) > 0 Then
                    '            If clsDBFuncationality.getSingleValue("select count(*) from tspl_bank_Branch_master where branch_code='" & grow.Cells("Bank Branch Code").Value & "' and bank_code='" & grow.Cells("Bank Code").Value & "'") > 0 Then
                    '                Dim bnkDt As DataTable = clsDBFuncationality.GetDataTable("select * from tspl_bank_master where bank_code='" & grow.Cells("Bank Code").Value & "'")
                    '                obj.BankName = clsCommon.myCstr(bnkDt.Rows(0)("Description"))
                    '                obj.bankcode = clsCommon.myCstr(bnkDt.Rows(0)("bank_code"))
                    '                obj.BankCityCode = clsCommon.myCstr(bnkDt.Rows(0)("CITY"))
                    '                obj.BankStateCode = clsCommon.myCstr(bnkDt.Rows(0)("STATE"))
                    '                obj.AccountNO = clsCommon.myCstr(bnkDt.Rows(0)("BANKACCNUMBER"))
                    '                obj.BankBranch = clsCommon.myCstr(grow.Cells("Bank Branch Code").Value)
                    '                obj.IFCICode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IFSC_Code from tspl_bank_Branch_master where branch_code='" & grow.Cells("Bank Branch Code").Value & "'"))
                    '            Else
                    '                Throw New Exception("Invalid Bank Brach Code.. It Is not Found In Master")
                    '            End If

                    '        Else
                    '            Throw New Exception("When Entering Bank Detail, Bank Branch Must Be Entered")
                    '        End If

                    '    End If
                    'End If

                    '-------------------------------26/06/2014-----------------Monika----------------------
                    obj.agreemnt = clsCommon.myCstr(grow.Cells("Agreement_Status").Value)
                    If clsCommon.CompairString(obj.agreemnt, "YES") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.agreemnt, "NO") <> CompairStringResult.Equal Then
                        Throw New Exception("Status Of Agreement Should Be Either YES Or NO")
                    End If
                    Try
                        obj.agrmnt_date = grow.Cells("Agreement_Date").Value
                    Catch exx As Exception
                        obj.agrmnt_date = clsCommon.GETSERVERDATE()
                    End Try
                    Try
                        obj.expired_date = grow.Cells("Agreement Expiry Date").Value
                    Catch exx As Exception
                        obj.expired_date = clsCommon.GETSERVERDATE()
                    End Try

                    obj.secutiy = clsCommon.myCstr(grow.Cells("Security_Status").Value)
                    If clsCommon.CompairString(obj.secutiy, "YES") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.secutiy, "NO") <> CompairStringResult.Equal Then
                        Throw New Exception("Status Of Security Should Be Either YES Or NO")
                    End If
                    obj.chq_amt = clsCommon.myCdbl(grow.Cells("Cheque_Amt").Value)
                    obj.chq_no = clsCommon.myCdbl(grow.Cells("Cheque_No").Value)
                    Try
                        obj.chq_date = grow.Cells("Cheque_Date").Value
                    Catch exx As Exception
                        obj.chq_date = clsCommon.GETSERVERDATE()
                    End Try
                    If clsCommon.CompairString(obj.secutiy, "YES") = CompairStringResult.Equal AndAlso (clsCommon.myCdbl(obj.chq_amt) <= 0 Or clsCommon.myLen(obj.chq_no) <= 0) Then
                        Throw New Exception("Please Fill Cheque Amount And Cheque No./Date")
                    End If

                    obj.industry_type = clsCommon.myCstr(grow.Cells("Industry Type").Value)
                    'If clsCommon.CompairString(obj.industry_type, "Prop.") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.industry_type, "Partnership") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.industry_type, "Public") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.industry_type, "Pvt") <> CompairStringResult.Equal Then
                    '    Throw New Exception("Status Of Industry Type Should Any One From Prop./Partnership/Public/Pvt")
                    'End If
                    'If clsCommon.CompairString(obj.industry_type, "Prop.") = CompairStringResult.Equal Then
                    '    obj.industry_type = clsCommon.myCstr(grow.Cells("Prop Name").Value)
                    'ElseIf clsCommon.CompairString(obj.industry_type, "Partnership") = CompairStringResult.Equal Then
                    '    obj.industry_type = clsCommon.myCstr(grow.Cells("Partner Name").Value)
                    'ElseIf clsCommon.CompairString(obj.industry_type, "Public") = CompairStringResult.Equal Then
                    '    obj.industry_type = clsCommon.myCstr(grow.Cells("Director Name").Value)
                    'ElseIf clsCommon.CompairString(obj.industry_type, "Pvt") = CompairStringResult.Equal Then
                    '    obj.industry_type = clsCommon.myCstr(grow.Cells("Director Name").Value)
                    'End If



                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Monthly Provision(Y/N)").Value), "Y") = CompairStringResult.Equal Then
                        obj.is_Chilling_Provision_Monthly = True
                    Else
                        obj.is_Chilling_Provision_Monthly = False
                    End If

                    obj.chilling_rate = clsCommon.myCdbl(grow.Cells("Chilling Charges").Value)
                    obj.chilling_qty = clsCommon.myCdbl(grow.Cells("Chilling On Qty").Value)
                    obj.chilling_kg_ltr = clsCommon.myCdbl(grow.Cells("Chilling on").Value)
                    obj.chilling_assur_qty = clsCommon.myCdbl(grow.Cells("Chilling Min. Guaranteed Avg. Qty").Value)
                    obj.chilling_assur_period = clsCommon.myCstr(grow.Cells("Chilling Min. Guaranteed Period").Value)
                    If clsCommon.myLen(grow.Cells("Chilling Starting Date").Value) > 0 Then
                        obj.Chilling_Period_Starting_Date = clsCommon.GetPrintDate(grow.Cells("Chilling Starting Date").Value, "dd-MMM-yyyy")
                    End If
                    obj.lease_rate = clsCommon.myCdbl(grow.Cells("Rate of Lease Charges").Value)
                    '===============Rohit===================================
                    obj.Unit_ChillingOnQty = IIf(clsCommon.myCstr(grow.Cells("Chilling On UOM(Handled/Dispatched)").Value) = "Handled", "H", "D")
                    obj.Unit_ChillingOn = IIf(clsCommon.myCstr(grow.Cells("Chilling On UOM(KG/LTR)").Value) = "KG", "K", "L")
                    obj.Unit_ChillingMinGuaranteePeriod = IIf(clsCommon.myCstr(grow.Cells("Chilling Min. Guaranteed Period UOM (Day/Month/Year)").Value) = "Day", "D", IIf(clsCommon.myCstr(grow.Cells("Chilling Min. Guaranteed Period UOM (Day/Month/Year)").Value) = "Month", "M", "Y"))
                    obj.Unit_RateOfLeasedCharges = IIf(clsCommon.myCstr(grow.Cells("Rate of Leased Charges UOM(Day/Month/Year)").Value) = "Day", "D", IIf(clsCommon.myCstr(grow.Cells("Rate of Leased Charges UOM(Day/Month/Year)").Value) = "Month", "M", "Y"))
                    obj.Unit_AreaOfStore = IIf(clsCommon.myCstr(grow.Cells("Area of Store UOM(Sq. Ft./Sq. Mt.)").Value) = "Sq. Mt.", "M", "F")
                    obj.Unit_AreaOfReceivingDock = IIf(clsCommon.myCstr(grow.Cells("Area of Receiving Dock UOM(Sq. Ft./Sq. Mt.)").Value) = "Sq. Mt.", "M", "F")
                    obj.Unit_AreaOfOffice = IIf(clsCommon.myCstr(grow.Cells("Area of Office UOM(Sq. Ft./Sq. Mt.)").Value) = "Sq. Mt.", "M", "F")
                    obj.Unit_AreaOfLab = IIf(clsCommon.myCstr(grow.Cells("Area of Lab UOM(Sq. Ft./Sq. Mt.)").Value) = "Sq. Mt.", "M", "F")
                    obj.Unit_OpenAreaForTankerMovement = IIf(clsCommon.myCstr(grow.Cells("Open Area For Tanker UOM(Sq. Ft./Sq. Mt.)").Value) = "Sq. Mt.", "M", "F")
                    obj.Unit_MccSuperArea = IIf(clsCommon.myCstr(grow.Cells("Mcc Super Area UOM(Sq. Ft./Sq. Mt.)").Value) = "Sq. Mt.", "M", "F")

                    obj.Weighing_Machine = IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Weighing Machine").Value), "Prompt") = CompairStringResult.Equal, "P", IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Weighing Machine").Value), "Delta") = CompairStringResult.Equal, "D", IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Weighing Machine").Value), "Panasonic") = CompairStringResult.Equal, "B", IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Weighing Machine").Value), "Supertech") = CompairStringResult.Equal, "S", "C"))))
                    obj.Sample_Machine = IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Sample Machine").Value), "Kanha") = CompairStringResult.Equal, "K", IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Sample Machine").Value), "Everest New") = CompairStringResult.Equal, "N", "E"))
                    obj.Weighing_Comport = clsCommon.myCstr(grow.Cells("Weighing Comport").Value)
                    obj.Sample_comport = clsCommon.myCstr(grow.Cells("Sample Comport").Value)
                    obj.Payment_Cycle = clsCommon.myCstr(grow.Cells("Payment Cycle").Value)
                    If clsCommon.myLen(obj.Payment_Cycle) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                        obj.Payment_Cycle = clsPaymentCycleMaster.GetDefault()
                    End If

                    obj.Shift_Opening_Time = clsCommon.myCstr(grow.Cells("Shift Morning Opening Time").Value)
                    obj.Shift_Closing_Time = clsCommon.myCstr(grow.Cells("Shift Morning Closing Time").Value)
                    obj.Shift_Eve_Opening_Time = clsCommon.myCstr(grow.Cells("Shift Evening Opening Time").Value)
                    obj.Shift_Eve_Closing_Time = clsCommon.myCstr(grow.Cells("Shift Evening Closing Time").Value)
                    obj.is_Reuired_Gate_Entry = IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Required Gate Entry(Yes/No)").Value), "Yes") = CompairStringResult.Equal, True, False)
                    '==================ADded by preeti Gupta==================
                    'If clsCommon.myLen(grow.Cells("RM").Value) <= 0 Then
                    '    Throw New Exception("RM Code Can Not Be Blank")
                    'End If
                    'If clsCommon.myLen(grow.Cells("RM").Value) > 12 Then
                    '    Throw New Exception("RM Code Can Not Be Larger Then 12 Charachter")
                    'End If
                    'If clsDBFuncationality.getSingleValue("select count(*) from TSPL_EMPLOYEE_MASTER where EMP_CODE='" & grow.Cells("RM").Value & "'") = 0 Then
                    '    Throw New Exception("RM Code Could Not Found In Employee Master")
                    'End If
                    obj.EMP_CODE = clsCommon.myCstr(grow.Cells("RM").Value)
                    '=========================================================

                    obj.Loc_Segment_Code = clsCommon.myCstr(grow.Cells("Loc Segment Code").Value)
                    If objCommonVar.ApplyDefaultsInMaster = True Then
                        obj.MCC_Code_VLC_Uploader = clsCommon.myCstr(grow.Cells("Loc Segment Code").Value)
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(obj.Loc_Segment_Code)) <= 0 Then
                        Throw New Exception("Loc Segment Code is Manadatory, and it must  not be Blank")
                    Else
                        Dim isValidSegmentcode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select Count (*) from TSPL_GL_SEGMENT_CODE where Seg_No='7' and Segment_code = '" + obj.Loc_Segment_Code + "'"))
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

                    obj.Is_MCC = IIf(clsCommon.myCdbl(grow.Cells("MCC/BMCC").Value) = 0, 0, 1)
                    'obj.Payment_Cycle = clsCommon.myCstr(grow.Cells("Incen").Value)
                    obj.Is_Truck_Sheet = IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Is Truck Sheet Mandatory").Value), "Yes") = CompairStringResult.Equal, 1, 0)
                    '========================================================
                    '----------------------code ended--------------------------------------------------
                    obj.AllowAutoMilkIn = clsCommon.myCdbl(grow.Cells("AllowAutoMilkIn").Value)
                    obj.AutoIn_Location = clsCommon.myCstr(grow.Cells("AutoIn_Location").Value)
                    qry = "select 1  from TSPL_LOCATION_MASTER where Location_Code='" + fndAutoInLoc.Value + "' and Location_Category='MCC'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        obj.SILOIn_Location = clsCommon.myCstr(grow.Cells("SILOIn_Location").Value)
                    Else
                        obj.SILOIn_Location = ""
                    End If


                    obj.Receipt_Weight_tolerance_Apply = IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("ApplyReceiptWeightTolerance(Y/N)").Value), "Y") = CompairStringResult.Equal, True, False)
                    obj.Receipt_Weight_tolerance_Value = clsCommon.myCdbl(grow.Cells("ReceiptWeightToleranceValue").Value)
                    If obj.Receipt_Weight_tolerance_Apply Then
                        If obj.Receipt_Weight_tolerance_Value < 0 Then
                            Throw New Exception("Value of ReceiptWeightToleranceValue can't be -ve")
                        End If
                    End If


                    obj.Commission_Rate = clsCommon.myCdbl(grow.Cells("CommissionRate").Value)
                    obj.Commission_Minimum_Shift_In_Payment_Cycle = clsCommon.myCdbl(grow.Cells("CommissionMinimumShiftInPaymentCycle").Value)
                    obj.Commission_Minimum_Qty_In_Shift = clsCommon.myCdbl(grow.Cells("CommissionMinimumQtyInShift").Value)
                    obj.Commission_No_Of_Payment_Cycle_For_New_VSP = clsCommon.myCdbl(grow.Cells("CommissionNoOfPaymentCycleForNewVSP").Value)

                    obj.Deduction_Minimum_FAT_Per = clsCommon.myCdbl(grow.Cells("DeductionMinimumFATPer").Value)
                    obj.Deduction_Minimum_SNF_Per = clsCommon.myCdbl(grow.Cells("DeductionMinimumSNFPer").Value)
                    obj.Deduction_No_Of_Payment_Cycle_For_New_VSP = clsCommon.myCdbl(grow.Cells("DeductionNoOfPaymentCycleForNewVSP").Value)


                    If clsCommon.myCdbl(obj.AllowAutoMilkIn) = 1 Then
                        If clsCommon.myLen(grow.Cells("AutoIn_Location").Value) <= 0 Then
                            Throw New Exception("Allow auto Milk is true So Auto In Location cannot be blank")
                        End If
                        If clsCommon.myLen(grow.Cells("SILOIn_Location").Value) <= 0 Then
                            Throw New Exception("Allow auto Milk is true So Silo In Location cannot be blank")
                        End If
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_mcc_master where mcc_code='" & obj.MCC_Code & "'") = 0 Then
                        Dim objgn As clsGenSetDetail
                        obj.arrGenSetDetail = New List(Of clsGenSetDetail)
                        For j As Integer = 0 To obj.NoOfDG
                            objgn = New clsGenSetDetail()
                            objgn.Prog_Code = Me.Form_ID
                            objgn.Trans_Code = obj.MCC_Code
                            objgn.Line_No = (j + 1)
                            objgn.Gen_Set_Desc = "N/A"
                            obj.arrGenSetDetail.Add(objgn)
                        Next
                        Dim objcomp As clsCompressorDetail
                        obj.arrCompressorDetail = New List(Of clsCompressorDetail)
                        For j As Integer = 0 To obj.NoOfCompressor
                            objcomp = New clsCompressorDetail
                            objcomp.Prog_Code = Me.Form_ID
                            objcomp.Trans_Code = obj.MCC_Code
                            objcomp.Line_No = (j + 1)
                            objcomp.Compressor_Desc = "N/A"
                            obj.arrCompressorDetail.Add(objcomp)
                        Next
                        Dim objSilo As clsSiloDetail
                        obj.arrSiloDetail = New List(Of clsSiloDetail)
                        For j As Integer = 0 To obj.No_Of_SILO
                            objSilo = New clsSiloDetail()
                            objSilo.Prog_Code = Me.Form_ID
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
                            objmilkpump.Prog_Code = Me.Form_ID
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
                            objChiller.Prog_Code = Me.Form_ID
                            objChiller.Trans_Code = obj.MCC_Code
                            objChiller.Chiller_Desc = "N/A"
                            objChiller.Chiller_Brand = ""
                            objChiller.Chiller_Capacity = 0
                            obj.arrChillerDetail.Add(objChiller)
                        Next
                        'Dim objMccUOM As clsMccUOMDetails
                        'obj.ArrUomDetails = New List(Of clsMccUOMDetails)
                        'For j As Integer = 0 To 0
                        '    objMccUOM = New clsMccUOMDetails()
                        '    objMccUOM.Mcc_Code = dt.Rows(j)("Mcc_Code")
                        '    objMccUOM.UOM_Code = dt.Rows(j)("UOM_Code")
                        '    objMccUOM.UOM_Description = dt.Rows(j)("UOM_Description")
                        '    objMccUOM.Stocking_Unit = dt.Rows(j)("Stocking_Unit")
                        '    objMccUOM.Conversion_Factor = dt.Rows(j)("Conversion_Factor")
                        '    obj.ArrUomDetails.Add(objMccUOM)
                        'Next

                        Dim objMccUOM As clsMccUOMDetails
                        obj.ArrUomDetails = New List(Of clsMccUOMDetails)
                        If clsCommon.myLen(grow.Cells("Chilling On UOM(KG/LTR)").Value) > 0 Then
                            Strqry = "select * from TSPL_UNIT_MASTER  WHERE unit_code='" + clsCommon.myCstr(grow.Cells("Chilling On UOM(KG/LTR)").Value) + "'"
                            Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(Strqry)
                            objMccUOM = New clsMccUOMDetails()
                            objMccUOM.Mcc_Code = obj.MCC_Code
                            objMccUOM.UOM_Code = clsCommon.myCstr(dttemp.Rows(0)("Unit_code"))
                            objMccUOM.UOM_Description = clsCommon.myCstr(dttemp.Rows(0)("Unit_Desc"))
                            objMccUOM.Stocking_Unit = "Y"
                            objMccUOM.Conversion_Factor = clsCommon.myCdbl(dttemp.Rows(0)("Conv_Factor"))
                            obj.ArrUomDetails.Add(objMccUOM)
                        Else
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
                            End If
                        End If

                        'Dim objMccCheque As clsMCCChequeDetails
                        obj.arrChequeDetail = New List(Of clsMCCChequeDetails)
                        'For j As Integer = 0 To 0
                        '    objMccCheque = New clsMCCChequeDetails()
                        '    objMccCheque.Prog_Code = obj.MCC_Code
                        '    objMccCheque.Check_No = "N/A"
                        '    objMccCheque.Check_date = Today.Date
                        '    obj.arrChequeDetail.Add(objMccCheque)
                        'Next

                        obj.isNewEntry = True
                        obj.Modified_By = objCommonVar.CurrentUserCode
                        obj.Modified_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")
                        obj.Comp_Code = objCommonVar.CurrentCompanyCode

                        obj.Created_By = objCommonVar.CurrentUserCode
                        obj.Created_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")
                        'obj.isNewEntry = False
                        clsMccMaster.SaveData(obj)
                    Else
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_Gen_Set_Detail where prog_code='" & Me.Form_ID & "' and trans_code='" & obj.MCC_Code & "'")
                        Dim objgn As clsGenSetDetail
                        obj.arrGenSetDetail = New List(Of clsGenSetDetail)
                        If dt IsNot Nothing And dt.Rows.Count > 0 Then

                            For j As Integer = 0 To dt.Rows.Count - 1
                                objgn = New clsGenSetDetail()
                                objgn.Prog_Code = dt.Rows(j)("Prog_Code")
                                objgn.Trans_Code = dt.Rows(j)("Trans_Code")
                                objgn.Line_No = dt.Rows(j)("Line_No")
                                objgn.Gen_Set_Desc = dt.Rows(j)("Gen_Set_Desc")
                                obj.arrGenSetDetail.Add(objgn)
                            Next
                        End If
                        If dt.Rows.Count > obj.NoOfDG Then
                            obj.NoOfDG = dt.Rows.Count
                        ElseIf dt.Rows.Count < obj.NoOfDG Then
                            For j As Integer = dt.Rows.Count + 1 To obj.NoOfDG
                                objgn = New clsGenSetDetail()
                                objgn.Prog_Code = Me.Form_ID
                                objgn.Trans_Code = obj.MCC_Code
                                objgn.Line_No = j
                                objgn.Gen_Set_Desc = "N/A"
                                obj.arrGenSetDetail.Add(objgn)
                            Next
                        End If

                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_Compressor_Detail where prog_code='" & Me.Form_ID & "' and trans_code='" & obj.MCC_Code & "'")
                        Dim objcomp As clsCompressorDetail
                        obj.arrCompressorDetail = New List(Of clsCompressorDetail)
                        If dt IsNot Nothing And dt.Rows.Count > 0 Then

                            For j As Integer = 0 To dt.Rows.Count - 1
                                objcomp = New clsCompressorDetail()
                                objcomp.Prog_Code = dt.Rows(j)("Prog_Code")
                                objcomp.Trans_Code = dt.Rows(j)("Trans_Code")
                                objcomp.Line_No = dt.Rows(j)("Line_No")
                                objcomp.Compressor_Desc = dt.Rows(j)("Compressor_Desc")
                                obj.arrCompressorDetail.Add(objcomp)
                            Next
                        End If
                        If dt.Rows.Count > obj.NoOfCompressor Then
                            obj.NoOfCompressor = dt.Rows.Count
                        ElseIf dt.Rows.Count < obj.NoOfCompressor Then
                            For j As Integer = dt.Rows.Count + 1 To obj.NoOfCompressor
                                objcomp = New clsCompressorDetail
                                objcomp.Prog_Code = Me.Form_ID
                                objcomp.Trans_Code = obj.MCC_Code
                                objcomp.Line_No = j
                                objcomp.Compressor_Desc = "N/A"
                                obj.arrCompressorDetail.Add(objcomp)
                            Next
                        End If
                        '===============================Silo Details====================================================
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_Silo_Detail where prog_code='" & Me.Form_ID & "' and trans_code='" & obj.MCC_Code & "'")
                        Dim objSilo As clsSiloDetail
                        obj.arrSiloDetail = New List(Of clsSiloDetail)
                        If dt IsNot Nothing And dt.Rows.Count > 0 Then

                            For j As Integer = 0 To dt.Rows.Count - 1
                                objSilo = New clsSiloDetail()
                                objSilo.Prog_Code = dt.Rows(j)("Prog_Code")
                                objSilo.Trans_Code = dt.Rows(j)("Trans_Code")
                                objSilo.Line_No = dt.Rows(j)("Line_No")
                                objSilo.Silo_Desc = dt.Rows(j)("Silo_Desc")
                                objSilo.Silo_Area = dt.Rows(j)("Silo_Area")
                                objSilo.Silo_Unit = dt.Rows(j)("Silo_Unit")
                                obj.arrSiloDetail.Add(objSilo)
                            Next
                        End If
                        If dt.Rows.Count > obj.No_Of_SILO Then
                            obj.No_Of_SILO = dt.Rows.Count
                        ElseIf dt.Rows.Count < obj.No_Of_SILO Then
                            For j As Integer = dt.Rows.Count + 1 To obj.No_Of_SILO
                                objSilo = New clsSiloDetail
                                objSilo.Prog_Code = Me.Form_ID
                                objSilo.Trans_Code = obj.MCC_Code
                                objSilo.Line_No = j
                                objSilo.Silo_Desc = "N/A"
                                objSilo.Silo_Area = "0"
                                objSilo.Silo_Unit = "M"
                                obj.arrSiloDetail.Add(objSilo)
                            Next
                        End If
                        '=========================================================================================================

                        '===============================Milk Pump Details====================================================
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_Milk_Pump_Detail where prog_code='" & Me.Form_ID & "' and trans_code='" & obj.MCC_Code & "'")
                        Dim objmilkpump As clsMilkPumpDetail
                        obj.arrMilkPumpDetail = New List(Of clsMilkPumpDetail)
                        If dt IsNot Nothing And dt.Rows.Count > 0 Then

                            For j As Integer = 0 To dt.Rows.Count - 1
                                objmilkpump = New clsMilkPumpDetail()
                                objmilkpump.Prog_Code = dt.Rows(j)("Prog_Code")
                                objmilkpump.Trans_Code = dt.Rows(j)("Trans_Code")
                                objmilkpump.Line_No = dt.Rows(j)("Line_No")
                                objmilkpump.Pump_Desc = dt.Rows(j)("Milk_Pump_Desc")
                                objmilkpump.Pump_Area = dt.Rows(j)("Milk_Pump_Area")
                                objmilkpump.Pump_Unit = dt.Rows(j)("Milk_Pump_Unit")
                                obj.arrMilkPumpDetail.Add(objmilkpump)
                            Next
                        End If
                        If dt.Rows.Count > obj.No_Of_MilkPump Then
                            obj.No_Of_MilkPump = dt.Rows.Count
                        ElseIf dt.Rows.Count < obj.No_Of_MilkPump Then
                            For j As Integer = dt.Rows.Count + 1 To obj.No_Of_MilkPump
                                objmilkpump = New clsMilkPumpDetail
                                objmilkpump.Prog_Code = Me.Form_ID
                                objmilkpump.Trans_Code = obj.MCC_Code
                                objmilkpump.Line_No = j
                                objmilkpump.Pump_Desc = "N/A"
                                objmilkpump.Pump_Area = "0"
                                objmilkpump.Pump_Unit = "M"
                                obj.arrMilkPumpDetail.Add(objmilkpump)
                            Next
                        End If
                        '=========================================================================================================
                        '===============================Chiller Details====================================================
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_Chiller_Detail where prog_code='" & Me.Form_ID & "' and trans_code='" & obj.MCC_Code & "'")
                        Dim objChiller As clsChillerDetail
                        obj.arrChillerDetail = New List(Of clsChillerDetail)
                        If dt IsNot Nothing And dt.Rows.Count > 0 Then

                            For j As Integer = 0 To dt.Rows.Count - 1
                                objChiller = New clsChillerDetail()
                                objChiller.Prog_Code = dt.Rows(j)("Prog_Code")
                                objChiller.Trans_Code = dt.Rows(j)("Trans_Code")
                                objChiller.Line_No = dt.Rows(j)("Line_No")
                                objChiller.Chiller_Desc = dt.Rows(j)("Chiller_Desc")
                                objChiller.Chiller_Brand = dt.Rows(j)("Chiller_Brand")
                                objChiller.Chiller_Capacity = dt.Rows(j)("Chiller_Capacity")
                                obj.arrChillerDetail.Add(objChiller)
                            Next
                        End If
                        If dt.Rows.Count > obj.No_Of_Chiller Then
                            obj.No_Of_Chiller = dt.Rows.Count
                        ElseIf dt.Rows.Count < obj.No_Of_Chiller Then
                            For j As Integer = dt.Rows.Count + 1 To obj.No_Of_Chiller
                                objChiller = New clsChillerDetail
                                objChiller.Prog_Code = Me.Form_ID
                                objChiller.Trans_Code = obj.MCC_Code
                                objChiller.Line_No = j
                                objChiller.Chiller_Desc = "N/A"
                                objChiller.Chiller_Brand = ""
                                objChiller.Chiller_Capacity = "0"
                                obj.arrChillerDetail.Add(objChiller)
                            Next
                        End If
                        '=========================================================================================================
                        '===========================UOM Details==================================================================
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_MCC_UOM_Detail where MCC_code='" & obj.MCC_Code & "' ")
                        Dim objMccUOM As clsMccUOMDetails
                        obj.ArrUomDetails = New List(Of clsMccUOMDetails)
                        If dt IsNot Nothing And dt.Rows.Count > 0 Then

                            For j As Integer = 0 To dt.Rows.Count - 1
                                objMccUOM = New clsMccUOMDetails()
                                objMccUOM.Mcc_Code = dt.Rows(j)("Mcc_Code")
                                objMccUOM.UOM_Code = dt.Rows(j)("UOM_Code")
                                objMccUOM.UOM_Description = dt.Rows(j)("UOM_Description")
                                objMccUOM.Stocking_Unit = dt.Rows(j)("Stocking_Unit")
                                objMccUOM.Conversion_Factor = dt.Rows(j)("Conversion_Factor")
                                obj.ArrUomDetails.Add(objMccUOM)
                            Next
                        Else
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
                            End If
                        End If

                        '===============================================================================================
                        '===========================Cheque Details==================================================================
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_MCC_Cheque_Detail where Prog_Code='" & obj.MCC_Code & "' ")
                        Dim objMccCheque As clsMCCChequeDetails
                        obj.arrChequeDetail = New List(Of clsMCCChequeDetails)
                        If dt IsNot Nothing And dt.Rows.Count > 0 Then
                            For j As Integer = 0 To dt.Rows.Count - 1
                                objMccCheque = New clsMCCChequeDetails()
                                objMccCheque.Prog_Code = dt.Rows(j)("Prog_Code")
                                objMccCheque.Check_No = dt.Rows(j)("Cheque_No")
                                objMccCheque.Check_date = dt.Rows(j)("Cheque_Date")
                                obj.arrChequeDetail.Add(objMccCheque)
                            Next
                        End If
                        '===============================================================================================
                        obj.Modified_By = objCommonVar.CurrentUserCode
                        obj.Modified_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")
                        obj.Created_By = objCommonVar.CurrentUserCode
                        obj.Created_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")


                        obj.isNewEntry = False
                        clsMccMaster.SaveData(obj)
                    End If
                Next


                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!, " + Environment.NewLine + " Only Data Regarding [DG set Detail],[Compressor Detail],[Silo Detail],[Milk Pump Detail],[Chiller Detail],[UOM Detail] is Not Updated Please Import Respective Sheets ", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message & " At Line No : " & i, Me.Text)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub mnuGenSetDetailsImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGenSetDetailsImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim i As Integer = 0
        connectSql.OpenConnection()
        Dim strdate As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        If transportSql.importExcel(gv, "MCC Code", "Line No", "Gen Set Desc") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsGenSetDetail()
                    i = i + 1
                    Dim strData As String = clsCommon.myCstr(grow.Cells("Mcc Code").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Mcc Code Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(strData) > 30 Then
                        Throw New Exception("Mcc Code Can Not Be Larger Then 30 Charachter")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_mcc_master where mcc_code='" & strData & "'") = 0 Then
                        Throw New Exception("Invalid MCC Code. Code Not Found In Master")
                    End If
                    obj.Trans_Code = strData

                    strData = clsCommon.myCstr(grow.Cells("Line No").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Line No Can Not Be Left Blank")
                    End If
                    obj.Line_No = strData
                    strData = clsCommon.myCstr(grow.Cells("Gen Set Desc").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Gen Set Desc Can Not Be Left Blank")
                    End If
                    obj.Gen_Set_Desc = strData
                    obj.Prog_Code = Me.Form_ID
                    clsGenSetDetail.SaveData(obj)
                Next

                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!, ", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message & " At Line No : " & i)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub mnuCompressorDetailsImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuCompressorDetailsImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim i As Integer = 0

        connectSql.OpenConnection()

        Dim strdate As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        If transportSql.importExcel(gv, "MCC Code", "Line No", "Compressor Desc") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsCompressorDetail()
                    i = i + 1
                    Dim strData As String = clsCommon.myCstr(grow.Cells("Mcc Code").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Mcc Code Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(strData) > 30 Then
                        Throw New Exception("Mcc Code Can Not Be Larger Then 30 Charachter")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_mcc_master where mcc_code='" & strData & "'") = 0 Then
                        Throw New Exception("Invalid MCC Code. Code Not Found In Master")
                    End If
                    obj.Trans_Code = strData

                    strData = clsCommon.myCstr(grow.Cells("Line No").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Line No Can Not Be Left Blank")
                    End If
                    obj.Line_No = strData
                    strData = clsCommon.myCstr(grow.Cells("Compressor Desc").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Compressor Name Can Not Be Left Blank")
                    End If
                    obj.Compressor_Desc = strData
                    obj.Prog_Code = Me.Form_ID
                    clsCompressorDetail.SaveData(obj)
                Next

                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!, ", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message & " At Line No : " & i)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rbtnprop_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnprop.ToggleStateChanged
        txtprop_name.Enabled = False
        txtpartner_name.Enabled = False
        txtdirectr_name.Enabled = False
        txtprop_name.MendatroryField = False
        txtpartner_name.MendatroryField = False
        txtdirectr_name.MendatroryField = False
        txtprop_name.Text = ""
        txtpartner_name.Text = ""
        txtdirectr_name.Text = ""
        If rbtnprop.IsChecked Then
            txtprop_name.Enabled = True
            txtprop_name.MendatroryField = True
            txtpartner_name.Enabled = False
            txtdirectr_name.Enabled = False
            txtpartner_name.MendatroryField = False
            txtdirectr_name.MendatroryField = False
        End If
    End Sub

    Private Sub rbtnpartnership_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnpartnership.ToggleStateChanged
        txtprop_name.Enabled = False
        txtpartner_name.Enabled = False
        txtdirectr_name.Enabled = False
        txtprop_name.MendatroryField = False
        txtpartner_name.MendatroryField = False
        txtdirectr_name.MendatroryField = False
        txtprop_name.Text = ""
        txtpartner_name.Text = ""
        txtdirectr_name.Text = ""
        If rbtnpartnership.IsChecked Then
            txtprop_name.Enabled = False
            txtprop_name.MendatroryField = False
            txtpartner_name.Enabled = True
            txtdirectr_name.Enabled = False
            txtpartner_name.MendatroryField = True
            txtdirectr_name.MendatroryField = False
        End If
    End Sub

    Private Sub rbtnpvt_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnpvt.ToggleStateChanged
        txtprop_name.Enabled = False
        txtpartner_name.Enabled = False
        txtdirectr_name.Enabled = False
        txtprop_name.MendatroryField = False
        txtpartner_name.MendatroryField = False
        txtdirectr_name.MendatroryField = False
        txtprop_name.Text = ""
        txtpartner_name.Text = ""
        txtdirectr_name.Text = ""
        If rbtnpvt.IsChecked Then
            txtprop_name.Enabled = False
            txtprop_name.MendatroryField = False
            txtpartner_name.Enabled = False
            txtdirectr_name.Enabled = True
            txtpartner_name.MendatroryField = False
            txtdirectr_name.MendatroryField = True
        End If
    End Sub

    Private Sub rbtnpublic_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnpublic.ToggleStateChanged
        txtprop_name.Enabled = False
        txtpartner_name.Enabled = False
        txtdirectr_name.Enabled = False
        txtprop_name.MendatroryField = False
        txtpartner_name.MendatroryField = False
        txtdirectr_name.MendatroryField = False
        txtprop_name.Text = ""
        txtpartner_name.Text = ""
        txtdirectr_name.Text = ""
        If rbtnpublic.IsChecked Then
            txtprop_name.Enabled = False
            txtprop_name.MendatroryField = False
            txtpartner_name.Enabled = False
            txtdirectr_name.Enabled = True
            txtpartner_name.MendatroryField = False
            txtdirectr_name.MendatroryField = True
        End If
    End Sub


    Private Sub txtPinCode_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPinCode.Validating
        Try
            If clsCommon.myLen(txtPinCode.Text) > 0 Then
                Convert.ToDecimal(txtPinCode.Text)
                errorControl.ResetError(txtPinCode)
            End If
        Catch ex As Exception
            RadPageView1.SelectedPage = RadPageViewPage1
            txtPinCode.Text = ""
            txtPinCode.Focus()
            txtPinCode.Select()
            errorControl.SetError(txtPinCode, "Pin Code Should be Numeric")
            Throw New Exception("Pin Code Should be Numeric")
        End Try
    End Sub

    Private Sub cmbagreemnt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbagreemnt.SelectedIndexChanged
        If clsCommon.CompairString(cmbagreemnt.Text, "YES") = CompairStringResult.Equal Then
            txtagrmnt_date.Enabled = True
            txtexpir_date.Enabled = True
            txtagrmnt_date.MendatroryField = True
            txtexpir_date.MendatroryField = True
        Else
            txtagrmnt_date.Enabled = False
            txtexpir_date.Enabled = False
            txtagrmnt_date.MendatroryField = False
            txtexpir_date.MendatroryField = False
            txtagrmnt_date.Text = clsCommon.GETSERVERDATE()
            txtexpir_date.Text = clsCommon.GETSERVERDATE()
        End If
    End Sub

    Private Sub cmbsecurity_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbsecurity.SelectedIndexChanged
        If clsCommon.CompairString(cmbsecurity.Text, "YES") = CompairStringResult.Equal Then
            txtchq_amt.Enabled = True
            txtchq_date.Enabled = True
            txtchq_no.Enabled = True
            txtchq_amt.MendatroryField = True
            txtchq_date.MendatroryField = True
            txtchq_no.MendatroryField = True

            gvCheque.AllowAddNewRow = True
            gvCheque.AllowEditRow = True
            gvCheque.AllowDeleteRow = True
        Else
            txtchq_amt.Enabled = False
            txtchq_date.Enabled = False
            txtchq_no.Enabled = False
            txtchq_amt.MendatroryField = False
            txtchq_date.MendatroryField = False
            txtchq_no.MendatroryField = False
            txtchq_amt.Text = ""
            txtchq_date.Text = clsCommon.GETSERVERDATE()
            txtchq_no.Text = ""

            gvCheque.AllowAddNewRow = False
            gvCheque.AllowEditRow = False
            gvCheque.AllowDeleteRow = False
        End If
    End Sub

    Private Sub txtBankBranchCode__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
        Frm_Open = New FrmBankBrachMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.bankBranchMaster)
        Frm_Open.Show()
    End Sub

    Private Sub txtBankBranchCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        If clsCommon.myLen(txtBankCode.Value) > 0 Then
            TxtBranchCode.Text = clsBankBranchMaster.getFinder("bank_code='" & txtBankCode.Value & "'", TxtBranchCode.Text, isButtonClicked)
            If clsCommon.myLen(TxtBranchCode.Text) > 0 Then
                txtBankBranchName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_name from tspl_bank_branch_master where branch_code='" & TxtBranchCode.Text & "'"))
                txtIFCICode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IFSC_Code from tspl_bank_branch_master where branch_code='" & TxtBranchCode.Text & "'"))
            Else
                txtBankBranchName.Text = ""
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "Please Select Bank First", Me.Text)
            txtBankCode.Focus()
        End If
    End Sub

    Private Sub BtnAddNewVendor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddNewVendor.Click
        Dim objvendor As New frmVendorMaster(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        objvendor.ChkChillingVendor.Checked = True
        objvendor.ChkChillingVendor.ReadOnly = True
        objvendor.is_For_Chilling_Vendor = True
        objvendor.Show()
    End Sub

    Private Sub TxtUnitCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtUnitCode.Load

    End Sub

    Private Sub TxtUnitCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtUnitCode._MYValidating
        Try
            Dim sQuery As String = "select Unit_Code as [Code],Unit_Desc as [Desc] from TSPL_UNIT_MASTER "
            TxtUnitCode.Value = clsCommon.ShowSelectForm("Training_Master", sQuery, "Code", "", TxtUnitCode.Value, "Code", isButtonClicked)
            If TxtUnitCode.Value <> "" Then
                txtUnit.Text = clsDBFuncationality.getSingleValue("select Unit_Desc from TSPL_UNIT_MASTER where Unit_Code='" & TxtUnitCode.Value & "'")
            Else
                txtUnit.Text = ""
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    Private Sub TxtPaymentCycle_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtPaymentCycle.KeyPress
        If Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Dim colIndex As Integer = 0
    Private Sub gvUOM_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvUOM.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvUOM.Columns(UOMColUnit) OrElse e.Column Is gvUOM.Columns(UOMColConvFact) Then
                        If e.Column Is gvUOM.Columns(UOMColUnit) Then
                            OpenUOMCodeList(False)
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenUOMCodeList(ByVal isButtonClick As Boolean)
        gvUOM.CurrentRow.Cells(UOMColUnitDesc).Value = ""
        gvUOM.CurrentRow.Cells(UOMColConvFact).Value = 0
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description,Conv_Factor as [Conversion Factor] from TSPL_UNIT_MASTER"
        gvUOM.CurrentRow.Cells(UOMColUnit).Value = clsCommon.ShowSelectForm("IMRMUOM", qry, "Code", "", clsCommon.myCstr(gvUOM.CurrentRow.Cells(UOMColUnit).Value), "Code", isButtonClick)
        If clsCommon.myLen(gvUOM.CurrentRow.Cells(UOMColUnit).Value) > 0 Then
            qry = "select Unit_Desc,Conv_Factor from TSPL_UNIT_MASTER  WHERE Unit_Code ='" + clsCommon.myCstr(gvUOM.CurrentRow.Cells(UOMColUnit).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvUOM.CurrentRow.Cells(UOMColUnitDesc).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Desc"))
                gvUOM.CurrentRow.Cells(UOMColConvFact).Value = clsCommon.myCdbl(dt.Rows(0)("Conv_Factor"))
            End If
        End If
    End Sub

    Private Sub gvUOM_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvUOM.CurrentColumnChanged
        If gvUOM.RowCount > 0 Then
            Dim intCurrRow As Integer = gvUOM.CurrentRow.Index
            If intCurrRow = gvUOM.Rows.Count - 1 Then
                gvUOM.Rows.AddNew()
                gvUOM.CurrentRow = gvUOM.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvUOM_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvUOM.UserDeletingRow
        If clsCommon.myCdbl(gvUOM.CurrentRow.Cells(UOMColStockUnitChangable).Value) = 1 Then
            clsCommon.MyMessageBoxShow(Me, "The MCC '" + fndMCCCode.Value + "' with UOM '" + clsCommon.myCstr(gvUOM.CurrentRow.Cells(UOMColUnit).Value) + "' is in use.")
            e.Cancel = True
        End If
    End Sub

    Private Sub gvUOM_RowFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gvUOM.RowFormatting
        Try
            ' e.RowElement.Enabled = IIf(clsCommon.myCdbl(e.RowElement.RowInfo.Cells(UOMColStockUnitChangable).Value) = "1", False, True)
        Catch ex As Exception
        End Try
    End Sub

    Sub LoadBlankGridUOM()
        gvUOM.Rows.Clear()
        gvUOM.Columns.Clear()

        Dim repoUOMCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOMCode.FormatString = ""
        repoUOMCode.HeaderText = "UOM"
        repoUOMCode.Name = UOMColUnit
        repoUOMCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoUOMCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoUOMCode.Width = 100
        gvUOM.MasterTemplate.Columns.Add(repoUOMCode)

        Dim repoUOMName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOMName.FormatString = ""
        repoUOMName.HeaderText = "UOM Descriiption"
        repoUOMName.Name = UOMColUnitDesc
        repoUOMName.Width = 150
        repoUOMName.ReadOnly = True
        gvUOM.MasterTemplate.Columns.Add(repoUOMName)

        Dim repoConvFactor As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoConvFactor.FormatString = ""
        repoConvFactor.HeaderText = "Conversion Factor"
        repoConvFactor.Name = UOMColConvFact
        repoConvFactor.Minimum = 0
        repoConvFactor.Width = 0
        repoConvFactor.IsVisible = False
        repoConvFactor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoConvFactor.DecimalPlaces = 6
        repoConvFactor.FormatString = "{0:n6}"
        gvUOM.MasterTemplate.Columns.Add(repoConvFactor)


        Dim repoStockUnit As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoStockUnit.FormatString = ""
        repoStockUnit.HeaderText = "Receiving UOM"
        repoStockUnit.Name = UOMColStockUnit
        repoStockUnit.Width = 100
        repoStockUnit.ReadOnly = False
        repoStockUnit.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoStockUnit.DataSource = GetStockUnit()
        repoStockUnit.ValueMember = "Code"
        repoStockUnit.DisplayMember = "Name"
        gvUOM.MasterTemplate.Columns.Add(repoStockUnit)

        Dim repoStockUnitChangable As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoStockUnitChangable.FormatString = ""
        repoStockUnitChangable.HeaderText = "Stock Unit Changable"
        repoStockUnitChangable.Name = UOMColStockUnitChangable
        repoStockUnitChangable.Minimum = 0
        repoStockUnitChangable.Width = 100
        repoStockUnitChangable.IsVisible = False
        repoStockUnitChangable.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvUOM.MasterTemplate.Columns.Add(repoStockUnitChangable)


        gvUOM.AllowAddNewRow = True
        gvUOM.ShowGroupPanel = False
        gvUOM.AllowColumnReorder = False
        gvUOM.AllowRowReorder = True
        gvUOM.AllowDeleteRow = True
        gvUOM.AllowEditRow = True
        gvUOM.EnableSorting = False
        gvUOM.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvUOM.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Function GetStockUnit() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "Y"
        dr("Name") = "Yes"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "No"
        dt.Rows.Add(dr)
        gvUOM.AllowDeleteRow = True

        Return dt
    End Function

    Private Sub gvChiller_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvChiller.UserDeletedRow
        Try
            Dim i As Integer = 0
            For i = 0 To gvChiller.Rows.Count - 1
                gvChiller.Rows(i).Cells("COLSLNO").Value = (i + 1)
                gvChiller.Rows(i).Cells("COLSLNO").ReadOnly = True
            Next
            txtNoofChillero.Text = gvChiller.Rows.Count
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvChiller_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvChiller.UserDeletingRow
        Try
            If clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvMilkPump_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvMilkPump.UserDeletedRow
        Try
            Dim i As Integer = 0
            For i = 0 To gvMilkPump.Rows.Count - 1
                gvMilkPump.Rows(i).Cells("COLSLNO").Value = (i + 1)
                gvMilkPump.Rows(i).Cells("COLSLNO").ReadOnly = True
            Next
            txtNoofMilkPumpo.Text = gvMilkPump.Rows.Count
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvMilkPump_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvMilkPump.UserDeletingRow
        Try
            If clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvSilo_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvSilo.CellValueChanged
        Try
            If gvSilo.Rows.Count > 0 Then
                If (Not isInsideLoadData) Then
                    If Not isCellValueChangedSiloOpen Then
                        isCellValueChangedSiloOpen = True
                        If e.Column Is gvSilo.Columns("COLSILOGAZEREADING") Then
                            OpenGazeReading(False)
                        End If
                        Dim totstrg As Double = 0
                        For Each row As GridViewRowInfo In gvSilo.Rows
                            totstrg += clsCommon.myCdbl(row.Cells("COLArea").Value)
                        Next
                        txtTotalStorageCapacity.Text = totstrg
                        isCellValueChangedSiloOpen = True
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChangedSiloOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenGazeReading(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gvSilo.CurrentRow.Cells("COLSILOGAZEREADING").Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select Code,Description,Capacity from TSPL_GAZE_READING"
            gvSilo.CurrentRow.Cells("COLSILOGAZEREADING").Value = clsCommon.ShowSelectForm("MCC@GazeRe", qry, "Code", "", clsCommon.myCstr(gvSilo.CurrentRow.Cells("COLSILOGAZEREADING").Value), "Code", isButtonClick)
            If clsCommon.myLen(gvSilo.CurrentRow.Cells("COLSILOGAZEREADING").Value) > 0 Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " Where Code='" + clsCommon.myCstr(gvSilo.CurrentRow.Cells("COLSILOGAZEREADING").Value) + "'")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gvSilo.CurrentRow.Cells("COLArea").Value = clsCommon.myCDecimal(dt.Rows(0)("Capacity"))
                End If
            End If
        End If
    End Sub

    Private Sub gvSilo_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvSilo.UserDeletedRow
        Try
            Dim i As Integer = 0
            For i = 0 To gvSilo.Rows.Count - 1
                gvSilo.Rows(i).Cells("COLSLNO").Value = (i + 1)
                gvSilo.Rows(i).Cells("COLSLNO").ReadOnly = True
            Next
            TxtNoofSiloo.Text = gvSilo.Rows.Count
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvSilo_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvSilo.UserDeletingRow
        Try
            If clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGvSilo()
        Try
            gvSilo.Rows.Clear()
            gvSilo.Columns.Clear()
            gvSilo.Columns.Add("COLSLNO", "SL. NO")
            gvSilo.Columns.Add("COLSiloDESC", "Silo Description")
            ' gvSilo.Columns.Add("COLArea", "Area")

            Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoICode.FormatString = ""
            repoICode.HeaderText = "Gaze Reading"
            repoICode.Name = "COLSILOGAZEREADING"
            repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
            repoICode.Width = 100
            gvSilo.MasterTemplate.Columns.Add(repoICode)

            Dim repoRowSilo As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoRowSilo.FormatString = ""
            repoRowSilo.HeaderText = "Silo Capacity"
            repoRowSilo.Name = "COLArea"
            repoRowSilo.Width = 100
            repoRowSilo.ReadOnly = False
            repoRowSilo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gvSilo.MasterTemplate.Columns.Add(repoRowSilo)


            Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
            repoRowType.FormatString = ""
            repoRowType.HeaderText = "U.O.M."
            repoRowType.Name = "ColUnit"
            repoRowType.Width = 100
            repoRowType.ReadOnly = False
            repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            repoRowType.DataSource = GetKgandLtr()
            repoRowType.ValueMember = "Code"
            repoRowType.DisplayMember = "Name"
            gvSilo.MasterTemplate.Columns.Add(repoRowType)




            gvSilo.Columns("COLSLNO").Width = 100
            gvSilo.Columns("COLSiloDESC").Width = 150
            gvSilo.Columns("COLArea").Width = 100
            gvSilo.Columns("COLUnit").Width = 100

            gvSilo.AllowAddNewRow = False
            gvSilo.AllowEditRow = True
            gvSilo.AllowDeleteRow = True
            gvSilo.AllowRowResize = False
            gvSilo.AllowRowReorder = False
            gvSilo.AllowColumnResize = True
            gvSilo.AllowColumnChooser = False
            gvSilo.AllowAutoSizeColumns = False
            gvSilo.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGvCheque()
        Try
            gvCheque.Rows.Clear()
            gvCheque.Columns.Clear()
            gvCheque.Columns.Add("COLCheck_No", "Cheque No")
            'gvCheque.Columns.Add("COLCheck_Date", "Cheque Date")

            Dim repoCode As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            repoCode.FormatString = ""
            repoCode.HeaderText = "Cheque Date"
            repoCode.Name = "COLCheck_Date"
            repoCode.Width = 150
            gvCheque.MasterTemplate.Columns.Add(repoCode)

            gvCheque.Columns("COLCheck_No").Width = 100
            gvCheque.Columns("COLCheck_Date").Width = 150


            gvCheque.AllowAddNewRow = False
            gvCheque.MasterTemplate.AddNewRowPosition = SystemRowPosition.Bottom
            gvCheque.AllowEditRow = False
            gvCheque.AllowDeleteRow = False
            gvCheque.AllowRowResize = False
            gvCheque.AllowRowReorder = False
            gvCheque.AllowColumnResize = True
            gvCheque.AllowColumnChooser = False
            gvCheque.AllowAutoSizeColumns = False
            gvCheque.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGvMilkPump()
        Try
            gvMilkPump.Rows.Clear()
            gvMilkPump.Columns.Clear()
            gvMilkPump.Columns.Add("COLSLNO", "SL. NO")
            gvMilkPump.Columns.Add("COLPumpDESC", "Milk Pump Description")
            gvMilkPump.Columns.Add("COLArea", "Capacity of Milk Pump")

            Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
            repoRowType.FormatString = ""
            repoRowType.HeaderText = "U.O.M."
            repoRowType.Name = "ColUnit"
            repoRowType.Width = 200
            repoRowType.ReadOnly = False
            repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            repoRowType.DataSource = GetHP()
            repoRowType.ValueMember = "Code"
            repoRowType.DisplayMember = "Name"
            gvMilkPump.MasterTemplate.Columns.Add(repoRowType)

            gvMilkPump.Columns("COLSLNO").Width = 100
            gvMilkPump.Columns("COLPumpDESC").Width = 150
            gvMilkPump.Columns("COLArea").Width = 300
            gvMilkPump.Columns("COLUnit").Width = 100

            gvMilkPump.AllowAddNewRow = False
            gvMilkPump.AllowEditRow = True
            gvMilkPump.AllowDeleteRow = True
            gvMilkPump.AllowRowResize = False
            gvMilkPump.AllowRowReorder = False
            gvMilkPump.AllowColumnResize = True
            gvMilkPump.AllowColumnChooser = False
            gvMilkPump.AllowAutoSizeColumns = False
            gvMilkPump.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGvChiller()
        Try
            gvChiller.Rows.Clear()
            gvChiller.Columns.Clear()
            gvChiller.Columns.Add("COLSLNO", "SL. NO")
            gvChiller.Columns.Add("COLChillerSETDESC", "Chiller Description")
            gvChiller.Columns.Add("COLChillerBrand", "Chiller Brand Name")
            gvChiller.Columns.Add("COLChillerCapacity", "Chiller Capacity")

            gvChiller.Columns("COLSLNO").Width = 100
            gvChiller.Columns("COLChillerSETDESC").Width = 150
            gvChiller.Columns("COLChillerBrand").Width = 150
            gvChiller.Columns("COLChillerCapacity").Width = 150

            gvChiller.AllowAddNewRow = False
            gvChiller.AllowEditRow = True
            gvChiller.AllowDeleteRow = True
            gvChiller.AllowRowResize = False
            gvChiller.AllowRowReorder = False
            gvChiller.AllowColumnResize = True
            gvChiller.AllowColumnChooser = False
            gvChiller.AllowAutoSizeColumns = False
            gvChiller.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnSilo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSilo.Click
        Try
            If clsCommon.myCdbl(TxtNoofSiloo.Text) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Value Of  No of Silo Set must be >0", Me.Text)
                TxtNoofSiloo.Focus()
                Exit Sub
            End If
            Dim i As Integer = 0
            If clsCommon.myCdbl(TxtNoofSiloo.Text) > gvSilo.Rows.Count Then
                For i = gvSilo.Rows.Count + 1 To clsCommon.myCdbl(TxtNoofSiloo.Text)
                    gvSilo.Rows.AddNew()
                    gvSilo.Rows(i - 1).Cells("COLSLNO").Value = i
                    gvSilo.Rows(i - 1).Cells("COLSLNO").ReadOnly = True
                Next
            ElseIf clsCommon.myCdbl(TxtNoofSiloo.Text) < gvSilo.Rows.Count Then
                For i = gvSilo.Rows.Count - 1 To clsCommon.myCdbl(TxtNoofSiloo.Text) Step -1
                    gvSilo.Rows.RemoveAt(i)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnChiller_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnChiller.Click
        Try
            If clsCommon.myCdbl(txtNoofChillero.Text) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Value Of  No of Chiller Set must be >0", Me.Text)
                txtNoofChillero.Focus()
                Exit Sub
            End If
            Dim i As Integer = 0
            If clsCommon.myCdbl(txtNoofChillero.Text) > gvChiller.Rows.Count Then
                For i = gvChiller.Rows.Count + 1 To clsCommon.myCdbl(txtNoofChillero.Text)
                    gvChiller.Rows.AddNew()
                    gvChiller.Rows(i - 1).Cells("COLSLNO").Value = i
                    gvChiller.Rows(i - 1).Cells("COLSLNO").ReadOnly = True
                Next
            ElseIf clsCommon.myCdbl(txtNoofChillero.Text) < gvChiller.Rows.Count Then
                For i = gvChiller.Rows.Count - 1 To clsCommon.myCdbl(txtNoofChillero.Text) Step -1
                    gvChiller.Rows.RemoveAt(i)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnMilkPump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMilkPump.Click
        Try
            If clsCommon.myCdbl(txtNoofMilkPumpo.Text) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Value Of  No of Milk Pump Set must be >0")
                txtNoofMilkPumpo.Focus()
                Exit Sub
            End If
            Dim i As Integer = 0
            If clsCommon.myCdbl(txtNoofMilkPumpo.Text) > gvMilkPump.Rows.Count Then
                For i = gvMilkPump.Rows.Count + 1 To clsCommon.myCdbl(txtNoofMilkPumpo.Text)
                    gvMilkPump.Rows.AddNew()
                    gvMilkPump.Rows(i - 1).Cells("COLSLNO").Value = i
                    gvMilkPump.Rows(i - 1).Cells("COLSLNO").ReadOnly = True
                Next
            ElseIf clsCommon.myCdbl(txtNoofMilkPumpo.Text) < gvMilkPump.Rows.Count Then
                For i = gvMilkPump.Rows.Count - 1 To clsCommon.myCdbl(txtNoofMilkPumpo.Text) Step -1
                    gvMilkPump.Rows.RemoveAt(i)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FndMccCharge__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndMccCharge._MYOpenMasterForm
        Frm_Open = New frmEmployee_Master
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmEmployee_Master)
        Frm_Open.Show()
    End Sub

    Private Sub FndMccCharge__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndMccCharge._MYValidating
        Dim dr As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployee(FndMccCharge.Value, True, "PRESENT_STATE_CODE='" & clsCommon.myCstr(fndState.Value) & "' and PRESENT_City_CODE='" & clsCommon.myCstr(fndCity.Value) & "'")
        If Not IsNothing(dr) Then
            FndMccCharge.Value = dr.EMP_CODE
            TxtMcc_In_Charge.Text = dr.Emp_Name
            TxtMccInchargeMobileNo.Text = dr.Phone
            TxtMccInchargeMailId.Text = dr.EMail_ID
            'TxtMccInchargeMobileNo.Text=
        End If
    End Sub

    Public Function GetSqMtrandFt(Optional ByRef cmb As common.Controls.MyComboBox = Nothing)
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Name") = "Sq. Mtr."
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "F"
        dr("Name") = "Sq. Ft."
        dt.Rows.Add(dr)

        If IsNothing(cmb) Then
            Return dt
        Else
            cmb.DataSource = dt
            cmb.ValueMember = "Code"
            cmb.DisplayMember = "Name"
        End If
        Return Nothing
    End Function

    Public Function GetHP(Optional ByRef cmb As common.Controls.MyComboBox = Nothing)
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "H"
        dr("Name") = "Horse Power(HP)"
        dt.Rows.Add(dr)

        If IsNothing(cmb) Then
            Return dt
        Else
            cmb.DataSource = dt
            cmb.ValueMember = "Code"
            cmb.DisplayMember = "Name"
        End If
        Return Nothing
    End Function

    Public Function GetKgandLtr(Optional ByRef cmb As common.Controls.MyComboBox = Nothing) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "K"
        dr("Name") = "KG."
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "L"
        dr("Name") = "Ltr."
        dt.Rows.Add(dr)

        'cmb.DataSource = dt
        'cmb.ValueMember = "Code"
        'cmb.DisplayMember = "Name"
        If IsNothing(cmb) Then
            Return dt
        Else
            cmb.DataSource = dt
            cmb.ValueMember = "Code"
            cmb.DisplayMember = "Name"
        End If
        Return Nothing
    End Function

    Public Sub GetHandledandDispatched(ByRef cmb As common.Controls.MyComboBox)
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "H"
        dr("Name") = "Handled"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "D"
        dr("Name") = "Dispatched"
        dt.Rows.Add(dr)

        cmb.DataSource = dt
        cmb.ValueMember = "Code"
        cmb.DisplayMember = "Name"
    End Sub

    Public Sub GetMonthandYearandDays(ByRef cmb As common.Controls.MyComboBox)
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "Y"
        dr("Name") = "Year"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "M"
        dr("Name") = "Month"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "D"
        dr("Name") = "Days"
        dt.Rows.Add(dr)

        cmb.DataSource = dt
        cmb.ValueMember = "Code"
        cmb.DisplayMember = "Name"
    End Sub

    Private Sub BtnChillerExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnChillerExport.Click
        Try
            Dim str As String = "select count(*) from tspl_Chiller_detail"
            Dim check As Integer = clsDBFuncationality.getSingleValue(str)

            If check > 0 Then '----------ended
                str = "select tspl_Chiller_detail.Trans_Code as [MCC Code] ,tspl_Chiller_detail.Line_No as [Line No] ,tspl_Chiller_detail.Chiller_Desc " _
                & " as [Chiller Desc],tspl_Chiller_detail.Chiller_Brand as [Chiller Brand],tspl_Chiller_detail.Chiller_Capacity as [Chiller Capacity] " _
                & " From tspl_Chiller_detail"
            Else
                str = "select '' as [MCC Code] ,0 as [Line No] ,'' as [Chiller Desc],'' as [Chiller Brand],'' as [Chiller Capacity]"
            End If
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnMilkPumpExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnMilkPumpExport.Click
        Try
            Dim str As String = "select count(*) from tspl_Milk_Pump_detail"
            Dim check As Integer = clsDBFuncationality.getSingleValue(str)

            If check > 0 Then '----------ended
                str = "select tspl_Milk_Pump_detail.Trans_Code as [MCC Code] ,tspl_Milk_Pump_detail.Line_No as [Line No] ,tspl_Milk_Pump_detail.Milk_Pump_Desc " _
                & " as [Milk Pump Desc],tspl_Milk_Pump_detail.Milk_Pump_Area as [Milk Pump Area],case when tspl_Milk_Pump_detail.Milk_Pump_Unit='M' then 'Sq. Mtr.' else 'Sq. Ft.' end as [Milk Pump Unit] " _
                & " From tspl_Milk_Pump_detail"
            Else
                str = "select '' as [MCC Code] ,0 as [Line No] ,'' as [Milk Pump Desc],'' as [Milk Pump Area],'' as [Milk Pump Unit]"
            End If
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub BtnSiloExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSiloExport.Click
        Try
            Dim str As String = "select count(*) from tspl_Silo_detail"
            Dim check As Integer = clsDBFuncationality.getSingleValue(str)
            If check > 0 Then '----------ended
                str = "select TSPL_SILO_DETAIL.Trans_Code as [MCC Code] ,TSPL_SILO_DETAIL.Line_No as [Line No] ,TSPL_SILO_DETAIL.Silo_Desc  as [Silo Desc],TSPL_SILO_DETAIL.Silo_Area as [Silo Area],case when TSPL_SILO_DETAIL.Silo_Unit='K' then 'KG.' else 'Ltr.' end as [Silo Unit],TSPL_SILO_DETAIL.Gaze_Reading_Code as [Gaze Reading] From TSPL_SILO_DETAIL "
            Else
                str = "select '' as [MCC Code] ,0 as [Line No] ,'' as [Silo Desc],'' as [Silo Area],'' as [Silo Unit],'' as [Gaze Reading]"
            End If
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnUomExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnUomExport.Click
        Try
            Dim qry As String = "Select TSPL_MCC_MASTER.Mcc_Code as [Mcc Code], UOM_Code as [UOM], Conversion_Factor as [Conversion Factor], Stocking_Unit as [Stocking Unit], Weight " _
            & " from TSPL_MCC_MASTER LEFT OUTER JOIN TSPL_MCC_UOM_DETAIL ON TSPL_MCC_UOM_DETAIL.Mcc_Code=TSPL_ITEM_MASTER.MCC_Code "
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "UOM")
        End Try
    End Sub

    Private Sub txtchq_amt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtchq_amt.TextChanged, TxtGuranteeAmount.TextChanged, TxtSecurityAmount.TextChanged, txtBankrefundedAmount.TextChanged
        GettotalAmount()
    End Sub

    Public Sub GettotalAmount()
        TxtTotalAmount.Text = clsCommon.myCdbl(TxtSecurityAmount.Text) + clsCommon.myCdbl(TxtGuranteeAmount.Text) - clsCommon.myCdbl(txtSecurityDeductedAmount.Text) - clsCommon.myCdbl(txtBankrefundedAmount.Text) '+ clsCommon.myCdbl(txtchq_amt.Text)
    End Sub

    Private Sub ImportMCCUOMDetails()
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        If transportSql.importExcel(gv1, "MCC Code", "UOM", "Conversion Factor", "Stocking Unit", "Weight") Then
            Dim isSaved As Boolean = True
            Dim currentdate As Date = Date.Today
            Dim trans As SqlTransaction = Nothing
            clsCommon.ProgressBarShow()
            Try
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim coll As New Hashtable()

                    Dim strMCCCode As String    '-----Item COde-------
                    Dim MCCCode As String = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                    If clsCommon.myLen(MCCCode) >= 0 Then
                        strMCCCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_Code from TSPL_MCC_MASTER Where MCC_Code='" + MCCCode + "'", trans))
                        If clsCommon.myLen(strMCCCode) <= 0 Then
                            Throw New Exception("The MCC '" + MCCCode + "' at Line No '" + LineNo + "' Does Not Exist")
                        End If
                    Else
                        Throw New Exception("Please Insert MCC Code at Line No '" + LineNo + "' ")
                    End If

                    Dim strUOM As String                                                                '-------Unit Code------
                    Dim UnitCode As String = clsCommon.myCstr(grow.Cells("UOM").Value)
                    If clsCommon.myLen(UnitCode) > 0 Then
                        strUOM = clsDBFuncationality.getSingleValue("Select Unit_Code from TSPL_UNIT_MASTER Where Unit_Code='" + UnitCode + "'", trans)
                        If clsCommon.CompairString(strUOM, UnitCode) = CompairStringResult.Equal Then
                            'clsCommon.AddColumnsForChange(coll, "UOM_Code", strUOM)
                        Else
                            Throw New Exception("The UOM '" + UnitCode + "' at Line No '" + LineNo + "' Does Not Exist")
                        End If
                    Else
                        Throw New Exception("Please Insert UOM at Line No '" + LineNo + "' ")
                    End If

                    '' Anubhooti 11-Sep-2014 BM00000003891 Duplication Check
                    For i As Integer = 0 To gv1.Rows.Count - 1
                        If clsCommon.myLen(gv1.Rows(i).Cells("UOM").Value) > 0 Then
                            Dim UOM As String = clsCommon.myCstr(gv1.Rows(i).Cells("UOM").Value)
                            Dim FirstMCCCode As String = clsCommon.myCstr(gv1.Rows(i).Cells("Item Code").Value)
                            For j As Integer = i + 1 To gv1.Rows.Count - 1
                                Dim SecondUOM As String = clsCommon.myCstr(gv1.Rows(j).Cells("UOM").Value)
                                Dim SecondMCCCode As String = clsCommon.myCstr(gv1.Rows(j).Cells("MCC Code").Value)
                                If clsCommon.CompairString(UOM, SecondUOM) = CompairStringResult.Equal And clsCommon.CompairString(FirstMCCCode, SecondMCCCode) = CompairStringResult.Equal Then
                                    Throw New Exception("Please check ! duplicate UOM between Line No '" + clsCommon.myCstr(i + 1) + "' and '" + clsCommon.myCstr(j + 1) + "'")
                                End If
                            Next
                        End If
                    Next
                    ''

                    '------------UOM DESCRIPTION--------------------
                    Dim UOM_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Unit_Desc from TSPL_UNIT_MASTER Where Unit_Code='" + strUOM + "'", trans))
                    clsCommon.AddColumnsForChange(coll, "UOM_Description", UOM_Description)
                    '----------------------------------------------

                    If clsCommon.myCstr(grow.Cells("Stocking Unit").Value) = "Y" Then
                        clsCommon.AddColumnsForChange(coll, "Stocking_Unit", "Y")
                    Else
                        clsCommon.AddColumnsForChange(coll, "Stocking_Unit", "N")
                    End If

                    Dim ConversionFactor As Double = clsCommon.myCdbl(grow.Cells("Conversion Factor").Value)
                    If ConversionFactor > 0 Then
                        clsCommon.AddColumnsForChange(coll, "COnversion_Factor", ConversionFactor)
                    Else
                        Throw New Exception("Please Insert Convrsion Factor at Line No '" + LineNo + "' ")
                    End If

                    If clsCommon.myLen(grow.Cells("Weight").Value) > 0 Then
                        If Not IsNumeric(grow.Cells("Weight").Value) Then
                            Throw New Exception("Please insert decimal data in Weight at Line No '" + LineNo + "' ")
                        Else
                            clsCommon.AddColumnsForChange(coll, "Weight", clsCommon.myCdbl(grow.Cells("Weight").Value))
                        End If
                    Else
                        clsCommon.AddColumnsForChange(coll, "Weight", clsCommon.myCdbl(grow.Cells("Weight").Value))
                    End If

                    Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_MCC_UOM_DETAIL Where MCC_Code='" + strMCCCode + "' AND UOM_Code='" + strUOM + "'", trans))
                    If Count <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "MCC_Code", strMCCCode)
                        clsCommon.AddColumnsForChange(coll, "UOM_Code", strUOM)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_UOM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Dim whrClas As String = "MCC_Code = '" + strMCCCode + "' and uom_code='" + strUOM + "'"
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_UOM_DETAIL", OMInsertOrUpdate.Update, whrClas, trans)
                    End If
                Next
                If isSaved Then
                    clsCommon.ProgressBarHide()
                    trans.Commit()
                    RadMessageBox.Show(Me, "Data Imported Successfully ...")
                Else
                    Throw New Exception("Error in Import")
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                RadMessageBox.Show(Me, ex.Message, Me.Text)
            Finally
                Me.Controls.Remove(gv1)
            End Try
        End If
    End Sub



    Private Sub ImportMilkPUmp()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim i As Integer = 0
        connectSql.OpenConnection()
        Dim strdate As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        If transportSql.importExcel(gv, "MCC Code", "Line No", "Milk Pump Desc", "Milk Pump Area", "Milk Pump Unit") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsMilkPumpDetail()
                    i = i + 1
                    Dim strData As String = clsCommon.myCstr(grow.Cells("Mcc Code").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Mcc Code Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(strData) > 30 Then
                        Throw New Exception("Mcc Code Can Not Be Larger Then 30 Charachter")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_mcc_master where mcc_code='" & strData & "'") = 0 Then
                        Throw New Exception("Invalid MCC Code. Code Not Found In Master")
                    End If
                    obj.Trans_Code = strData

                    strData = clsCommon.myCstr(grow.Cells("Line No").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Line No Can Not Be Left Blank")
                    End If
                    obj.Line_No = strData
                    strData = clsCommon.myCstr(grow.Cells("Milk Pump Desc").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Milk Pump Desc Can Not Be Left Blank")
                    End If
                    obj.Pump_Desc = strData
                    strData = clsCommon.myCstr(grow.Cells("Milk Pump Area").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Milk Pump Area Can Not Be Left Blank")
                    End If
                    obj.Pump_Area = strData
                    strData = IIf(clsCommon.myCstr(grow.Cells("Milk Pump Unit").Value).Contains("Mtr"), "M", IIf(clsCommon.myCstr(grow.Cells("Milk Pump Unit").Value).Contains("Ft."), "F", ""))
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Milk Pump Unit Can Not Be Left Blank")
                    End If
                    obj.Pump_Unit = strData
                    obj.Prog_Code = Me.Form_ID
                    clsMilkPumpDetail.SaveData(obj)
                Next

                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!, ", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message & " At Line No : " & i)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub ImportChiller()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim i As Integer = 0
        connectSql.OpenConnection()
        Dim strdate As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        If transportSql.importExcel(gv, "MCC Code", "Line No", "Chiller Desc", "Chiller Brand", "Chiller Capacity") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsChillerDetail()
                    i = i + 1
                    Dim strData As String = clsCommon.myCstr(grow.Cells("Mcc Code").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Mcc Code Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(strData) > 30 Then
                        Throw New Exception("Mcc Code Can Not Be Larger Then 30 Charachter")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_mcc_master where mcc_code='" & strData & "'") = 0 Then
                        Throw New Exception("Invalid MCC Code. Code Not Found In Master")
                    End If
                    obj.Trans_Code = strData

                    strData = clsCommon.myCstr(grow.Cells("Line No").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Line No Can Not Be Left Blank")
                    End If
                    obj.Line_No = strData
                    strData = clsCommon.myCstr(grow.Cells("Chiller Desc").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Chiller Desc Can Not Be Left Blank")
                    End If
                    obj.Chiller_Desc = strData
                    strData = clsCommon.myCstr(grow.Cells("Chiller Brand").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Chiller Brand Can Not Be Left Blank")
                    End If
                    obj.Chiller_Brand = strData
                    strData = clsCommon.myCstr(grow.Cells("Chiller Capacity").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Chiller Capacity Can Not Be Left Blank")
                    End If
                    obj.Chiller_Capacity = strData
                    obj.Prog_Code = Me.Form_ID
                    clsChillerDetail.SaveData(obj)
                Next

                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!, ", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message & " At Line No : " & i)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub BtnChillerImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnChillerImport.Click
        ImportChiller()
    End Sub

    Private Sub BtnMilkPumpImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnMilkPumpImport.Click
        ImportMilkPUmp()
    End Sub

    Private Sub BtnSiloImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSiloImport.Click
        ImportSilo()
    End Sub
    Private Sub ImportSilo()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim i As Integer = 0

        connectSql.OpenConnection()

        Dim strdate As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        If transportSql.importExcel(gv, "MCC Code", "Line No", "Silo Desc", "Silo Area", "Silo Unit", "Gaze Reading") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsSiloDetail()
                    i = i + 1
                    Dim strData As String = clsCommon.myCstr(grow.Cells("Mcc Code").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Mcc Code Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(strData) > 30 Then
                        Throw New Exception("Mcc Code Can Not Be Larger Then 30 Charachter")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_mcc_master where mcc_code='" & strData & "'") = 0 Then
                        Throw New Exception("Invalid MCC Code. Code Not Found In Master")
                    End If
                    obj.Trans_Code = strData

                    strData = clsCommon.myCstr(grow.Cells("Line No").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Line No Can Not Be Left Blank")
                    End If
                    obj.Line_No = strData
                    strData = clsCommon.myCstr(grow.Cells("Silo Desc").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Silo Desc Can Not Be Left Blank")
                    End If
                    obj.Silo_Desc = strData
                    strData = clsCommon.myCstr(grow.Cells("Silo Area").Value)
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Silo Area Can Not Be Left Blank")
                    End If
                    obj.Silo_Area = strData
                    strData = IIf(clsCommon.myCstr(grow.Cells("Silo Unit").Value).Contains("KG"), "K", "L")
                    If clsCommon.myLen(strData) <= 0 Then
                        Throw New Exception("Silo Unit Can Not Be Left Blank")
                    End If

                    obj.Silo_Unit = strData
                    obj.Prog_Code = Me.Form_ID

                    strData = clsCommon.myCstr(grow.Cells("Gaze Reading").Value)
                    If clsCommon.myLen(strData) <= 0 AndAlso SettApplyGaze Then
                        Throw New Exception("Gaze Reading Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(strData) > 0 Then
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Code,Description,Capacity from TSPL_GAZE_READING where Code='" & strData & "'")
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Invalid Gaze reading code [" + grow.Cells("Gaze Reading").Value + "]")
                        End If
                        If clsCommon.myCDecimal(dt.Rows(0)("Capacity")) <> clsCommon.myCDecimal(obj.Silo_Area) Then
                            Throw New Exception("Gaze reading Capacity [" + clsCommon.myCstr(dt.Rows(0)("Capacity")) + "] and entered Capacity [" + obj.Silo_Area + "]")
                        End If
                        strData = clsCommon.myCstr(dt.Rows(0)("Code"))
                        obj.Gaze_Reading_Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                    End If
                    clsSiloDetail.SaveData(obj)
                Next

                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!, ", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message & " At Line No : " & i)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    Public Sub GetMccFssai()
        Try
            Dim Mcc_Name As String = ""
            MCCLOCATIONFINDER()
            Dim obj As New clsMccMaster
            obj.Days_For_FSSAI = clsFixedParameter.GetData(clsFixedParameterType.MCCFSSAI_DAYS, clsFixedParameterCode.MilkSetting, Nothing)
            If obj.Days_For_FSSAI = "" Then
                obj.Days_For_FSSAI = "3"
            End If
            Dim Dt As DataTable
            If Today.AddDays(CType(obj.Days_For_FSSAI, Double)).Day Mod CType(obj.Days_For_FSSAI, Double) = 0 Then
                If clsMccMaster.isCurrentUserHO() Then
                    '    Dim sQuery As String = "select * from tspl_mcc_master  where FSSAI_NO " _
                    '& " like '%FSSAI%'  "
                    Dim squery As String = " select substring(value,2,len(value)-1) from (select distinct ( " _
                     & " select ','+MCC_NAME from tspl_mcc_master  where FSSAI_NO like '%FSSAI%'  for xml path('')) as value) a"
                    'Dim Dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
                    Mcc_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue(squery))
                    'If Dt.Rows.Count > 0 Then
                    'For Each row As DataRow In Dt.Rows
                    '    Mcc_Name &= IIf(Mcc_Name = "", row("Mcc_Name"), "," & row("Mcc_Name"))
                    'Next
                    If clsCommon.myLen(Mcc_Name) > 0 Then
                        If clsCommon.MyMessageBoxShow(Me, "MCC : [" & Mcc_Name & "] have Auto Generated FSSAI Code" & Environment.NewLine _
                                           & " Do You want to Open Mcc Master.", "MCC FSSAI", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                            squery = "select * from TSPL_PROGRAM_MASTER where Program_Code = 'MCC-MST'"
                            Dt = clsDBFuncationality.GetDataTable(squery)
                            MDI.ShowForm(Dt.Rows(0).Item("Program_Code"), Dt.Rows(0).Item("Program_Name"), True)
                            'Dim objMcc As New FrmMCCMaster
                            'objMcc.Show()
                        End If
                    End If
                    ' End If

                Else

                    If clsCommon.myLen(arrLoc) > 0 Then
                        Dim sQuery As String = "select * from tspl_mcc_master inner join tspl_location_master on location_Code=mcc_Code and FSSAI_NO " _
               & " like '%FSSAI%' and (loc_segment_Code in (" & arrLoc & ") or mcc_Code in (" & arrLoc & ")) "
                        Dt = clsDBFuncationality.GetDataTable(sQuery)
                        If Dt.Rows.Count > 0 Then
                            For Each row As DataRow In Dt.Rows
                                Mcc_Name &= IIf(Mcc_Name = "", row("Mcc_Name"), "," & row("Mcc_Name"))
                            Next
                            If clsCommon.myLen(Mcc_Name) > 0 Then
                                If clsCommon.MyMessageBoxShow(Me, "MCC : [" & Mcc_Name & "] have Auto Generated FSSAI Code" & Environment.NewLine _
                                                   & " Do You want to Open Mcc Master.", "MCC FSSAI", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                                    sQuery = "select * from TSPL_PROGRAM_MASTER where Program_Code = 'MCC-MST'"
                                    Dt = clsDBFuncationality.GetDataTable(sQuery)
                                    MDI.ShowForm(Dt.Rows(0).Item("Program_Code"), Dt.Rows(0).Item("Program_Name"), True)
                                    'Dim objMcc As New FrmMCCMaster
                                    'objMcc.Show()
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvBankG_CellDoubleClicK(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvBankG.CellDoubleClick
        'Try
        '    If clsCommon.myLen(gvBankG.CurrentRow.Cells("COLBankNO").Value) > 0 Then
        '        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBankGuaranteeMaster1, gvBankG.CurrentRow.Cells("COLBankNO").Value)
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(Me, ex.ToString)
        'End Try
    End Sub

    Private Sub GVPaymentEntry_CellDoubleClicK(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles GVPaymentEntry.CellDoubleClick
        Try
            If clsCommon.myLen(GVPaymentEntry.CurrentRow.Cells("COLPaymentNO").Value) > 0 Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, GVPaymentEntry.CurrentRow.Cells("COLPaymentNO").Value)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Private Sub gvEmp_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvEmp.CellValueChanged
        If Not isInsideLoadData Then '------when on loaddata then it should not run
            If Not isCellValueChangedOpen Then
                If gvEmp.CurrentColumn Is gvEmp.Columns(colEmpCode) Then
                    isCellValueChangedOpen = True
                    OpenEmployeeFinder(True)
                    isCellValueChangedOpen = False
                End If
            End If
        End If
    End Sub

    Private Sub gvEmp_UserAddedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvEmp.UserAddedRow
        updateSlnoEmpGrid()
    End Sub

    Private Sub gvEmp_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvEmp.UserDeletedRow
        updateSlnoEmpGrid()
    End Sub
    Sub updateSlnoEmpGrid()
        If gvEmp.Rows.Count > 0 Then
            For i As Integer = 0 To gvEmp.Rows.Count - 1
                gvEmp.Rows(i).Cells(colSlNo).Value = (i + 1)
            Next
        End If
    End Sub
    Private Sub gvEmp_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvEmp.UserDeletingRow
        If Not deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmployeeDetailsExport.Click
        clsCommon.MyMessageBoxShow(Me, "Under Development")
    End Sub

    Private Sub mnuEmployeeDetailsImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmployeeDetailsImport.Click
        clsCommon.MyMessageBoxShow(Me, "Under Development")
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
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
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

    Private Sub FndIncentive__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndIncentive._MYOpenMasterForm
        Frm_Open = New frmIncentiveMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmIncentiveMaster)
        Frm_Open.Show()
    End Sub

    Private Sub FndIncentive__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndIncentive._MYValidating
        GetIIncentiveDetails(isButtonClicked)
    End Sub

    Sub GetIIncentiveDetails(ByVal isBUttonclicked As Boolean)
        'If clsCommon.myLen(txtBankCode.Value) > 0 Then
        If isBUttonclicked Then
            FndIncentive.Value = clsIncentiveMaster.GetFinder("", FndIncentive.Value, isBUttonclicked)
        End If
        'If clsCommon.myLen(FndIncentive.Value) > 0 Then
        '    Dim obj As clsIncentiveMaster
        '    obj = clsIncentiveMaster.GetData(FndIncentive.Value, NavigatorType.Current)
        '    If Not IsNothing(obj) Then
        '        LblIncentive.Text = obj.DESCRIPTION
        '    End If
        'Else
        '    LblIncentive.Text = ""
        '    FndIncentive.Value = Nothing
        'End If
        'Else
        'If isBUttonclicked Then
        '    clsCommon.MyMessageBoxShow(me, "Please Select Bank First")
        'End If
        'txtBankCode.Focus()
        'End If
    End Sub

    Private Sub RadMenuItem1_Click_1(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmMCCMaster
        frm.ShowDialog()
    End Sub

    Private Sub FndMPGrpCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndMPGrpCode._MYValidating
        FndMPGrpCode.Value = clsVendorGroupMaster.getFinder("", FndMPGrpCode.Value, isButtonClicked)
        fndgroupcode_text_Changed()
    End Sub

    Sub fndgroupcode_text_Changed()
        Try
            Dim strquery As String = "select ven_Group_code,group_desc  from Tspl_vendor_group where ven_group_code='" + FndMPGrpCode.Value + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then

                strvalue = dr.Rows(0)("ven_Group_code").ToString()
            End If
            If strvalue <> "" Then
                funfillfndGroupCode()
            Else
                TxtMPGrpCode.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Public Sub funfillfndGroupCode()
        Try
            Dim strquery As String = "select group_desc,tax_Group_Code,Acct_Set_code,Terms_COde,Bank_Code ,payment_code from Tspl_vendor_group where ven_group_code='" + FndMPGrpCode.Value + "'"
            Dim dr As DataTable
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                TxtMPGrpCode.Text = dr.Rows(0)("group_desc").ToString()

                FndMpTermsCode.Value = dr.Rows(0)("Terms_COde").ToString()
                txtMPtermcodedes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Terms_Desc from TSPL_TERMS_MASTER where Terms_Code='" + clsCommon.myCstr(FndMpTermsCode.Value) + "'"))
                If clsCommon.myLen(txtMPtermcodedes.Text) <= 0 Then
                    FndMpTermsCode.Value = ""
                    txtMPtermcodedes.Text = ""
                End If
                FndMPPaymentCode.Value = dr.Rows(0)("payment_code").ToString()
                TxtMPPaymentCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select payment_desc from tspl_payment_code where payment_code='" + clsCommon.myCstr(FndMPPaymentCode.Value) + "'"))
                If clsCommon.myLen(TxtMPPaymentCode.Text) <= 0 Then
                    FndMPPaymentCode.Value = ""
                    TxtMPPaymentCode.Text = ""
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub FndMPPaymentCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndMPPaymentCode._MYValidating
        FndMPPaymentCode.Value = clsPaymentCode.getFinder("", FndMPPaymentCode.Value, isButtonClicked)
        TxtMPPaymentCode.Text = clsDBFuncationality.getSingleValue("Select payment_Desc from TSPL_PAYMENT_CODE where Payment_Code='" + FndMPPaymentCode.Value + "'")
    End Sub
    Private Sub fndMPpaymentCycle__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndMPPaymentCycle._MYOpenMasterForm
        Frm_Open = New frmPaymentCycleMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmPaymentCycleMaster)
        Frm_Open.Show()
    End Sub

    Private Sub fndMPpaymentCycle__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndMPPaymentCycle._MYValidating
        Try
            GetMPPaymentCycleData(isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Sub GetMPPaymentCycleData(ByVal isButtonClicked As Boolean)
        If isButtonClicked Then
            FndMPPaymentCycle.Value = clsPaymentCycleMaster.getFinder(IIf(clsCommon.myLen(FndMPPaymentCycle.Value) > 0, "TSPL_PAYMENT_CYCLE_MASTER.PC_CODE='" & FndMPPaymentCycle.Value & "'", ""), FndMPPaymentCycle.Value, isButtonClicked)
        End If
        If clsCommon.myLen(FndMPPaymentCycle.Value) > 0 Then
            Dim obj As clsPaymentCycleMaster = clsPaymentCycleMaster.GetData(FndMPPaymentCycle.Value, NavigatorType.Current)
            TxtMPPaymentCycle.Text = obj.Description
        End If
    End Sub

    Private Sub FndMpTermsCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndMpTermsCode._MYValidating
        Dim qry As String = "SELECT [Terms_Code] as [Terms Code],[Terms_Desc] as [Description],[No_Days] as [No. of Days] FROM [TSPL_TERMS_MASTER]"
        FndMpTermsCode.Value = clsCommon.ShowSelectForm("fndtrms", qry, "Terms Code", "", FndMpTermsCode.Value, "", isButtonClicked)
        txtMPtermcodedes.Text = clsDBFuncationality.getSingleValue("Select Terms_Desc from TSPL_TERMS_MASTER where Terms_Code='" + FndMpTermsCode.Value + "'")
    End Sub


    Private Sub fndEmployee__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndEmployee._MYValidating
        fndEmployee.Value = clsEmployeeMaster.getFinder(" emp_status='Active' ", fndEmployee.Value, isButtonClicked)
        txtEmployeeName.Text = clsDBFuncationality.getSingleValue("select TSPL_EMPLOYEE_MASTER.emp_name from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" + fndEmployee.Value + "'")
    End Sub


    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
            fndMCCCode.Focus()
            clsCommon.MyMessageBoxShow(Me, "Please first select MCC Code")
            Exit Sub
        End If
        Dim frm As New frmMCCWiseVehicleAndFreightChargesMapping
        frm.strMCCCode = fndMCCCode.Value
        frm.ShowDialog()
    End Sub

    Private Sub chkSeprateDockForCowAndBuffalo_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSeprateDockForCowAndBuffalo.ToggleStateChanged
        setPanenVisiable()
    End Sub

    Sub setPanenVisiable()
        Panel1.Visible = chkSeprateDockForCowAndBuffalo.Checked
    End Sub
    Sub setAutoMilkInPanelVisible()
        Panel2.Visible = chkAutoMilkIn.Checked
    End Sub

    Private Sub chkAutoMilkIn_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAutoMilkIn.ToggleStateChanged
        setAutoMilkInPanelVisible()
    End Sub

    Private Sub fndAutoInLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndAutoInLoc._MYValidating

        Dim whrCls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " location_code in (" & objCommonVar.strCurrUserLocations & ") " ''BHA/26/06/18-000088 By balwinder on 29/06/2018 Remove 'and' becuase it make error.
            End If
        End If
        fndAutoInLoc.Value = clsLocation.getFinder(whrCls, fndAutoInLoc.Value, isButtonClicked)
        If clsCommon.myLen(fndAutoInLoc.Value) > 0 Then
            txtAutoInLoc.Text = clsLocation.GetName(fndAutoInLoc.Value, Nothing)
        Else
            txtAutoInLoc.Text = ""
        End If
    End Sub

    Private Sub fndSiloInLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSiloInLoc._MYValidating
        If clsCommon.myLen(fndAutoInLoc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please select a In Location First ")
            Exit Sub
        End If
        Dim whrCls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        fndSiloInLoc.Value = clsLocation.getFinder("is_sub_location='Y' and Main_Location_Code='" & fndAutoInLoc.Value & "' " & whrCls, fndSiloInLoc.Value, isButtonClicked)
        If clsCommon.myLen(fndSiloInLoc.Value) > 0 Then
            txtSiloInLoc.Text = clsLocation.GetName(fndSiloInLoc.Value, Nothing)
        Else
            txtSiloInLoc.Text = ""
        End If
    End Sub



    Private Sub chkMilkReceiptWeightTolerance_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMilkReceiptWeightTolerance.ToggleStateChanged
        txtMilkReceiptWeightTolerance.Visible = chkMilkReceiptWeightTolerance.Checked
    End Sub



    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select MCC")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndMCCCode.Value, "MCC_Code", "TSPL_MCC_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub chkApplyFailedSample_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkFailedSampleApply.ToggleStateChanged
        pnlFailedSample.Enabled = chkFailedSampleApply.Checked
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            Dim sQuery As String = " Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc, Type from TSPL_LOCATION_MASTER  "
            fndLocation.Value = clsCommon.ShowSelectForm("Location@Plant@Master", sQuery, "Code", "TSPL_LOCATION_MASTER .Type = 'PLANT'", fndLocation.Value, "Code", isButtonClicked)
            If fndLocation.Value <> "" Then
                lblLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'")
            Else
                lblLocation.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Private Sub txtMCCCopy__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCCCopy._MYValidating
        Try
            Dim whrcls As String = ""
            If clsCommon.myLen(arrLoc) > 0 Then
                whrcls = " tspl_mcc_master.mcc_code in (" + arrLoc + ")"
            End If
            fndMCCCode.Value = clsMccMaster.getFinder(whrcls, fndMCCCode.Value, isButtonClicked)
            txtMCCCopy.Value = fndMCCCode.Value
            If clsCommon.myLen(fndMCCCode.Value) > 0 Then
                loadData(fndMCCCode.Value, NavigatorType.Current)
                btnSave.Text = "&Save"
                fndMCCCode.Value = ""
                txtMCCName.Text = ""
                txtAdd1.Text = ""
                txtAdd2.Text = ""
                'btnDelete.Enabled = True
                'fndMCCCode.MyReadOnly = True
            Else
                reset()
                fndMCCCode.MyReadOnly = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub MyNumBox1_TextChanged(sender As Object, e As EventArgs) Handles txtDWIFrom1.TextChanged

    End Sub

    Private Sub MyNumBox6_TextChanged(sender As Object, e As EventArgs) Handles txtDWITo2.TextChanged

    End Sub

    Private Sub txtCommissionRate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtCommissionRate.Validating
        If txtCommissionRate.Value > 100 Then
            txtCommissionRate.Value = 100
        End If
    End Sub

    Private Sub SetDefaultValues()
        fndCity.Value = clsCommon.myCstr(clsCityMaster.GetDefault())
        If clsCommon.myLen(fndCity.Value) > 0 Then
            txtCityName.Text = clsCityMaster.GetName(fndCity.Value)
        End If
        fndState.Value = clsCommon.myCstr(clsStateMaster.GetDefault())
        If clsCommon.myLen(fndState.Value) > 0 Then
            txtStateName.Text = clsStateMaster.GetName(fndState.Value)
        End If
        fndpaymentCycle.Value = clsCommon.myCstr(clsPaymentCycleMaster.GetDefault())
        gvUOM.Rows(0).Cells(UOMColUnit).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Unit_code from tspl_unit_master where IsDefault=1"))
        If clsCommon.myLen(gvUOM.Rows(0).Cells(UOMColUnit).Value) > 0 Then
            Dim qry As String = "select Unit_Desc,Conv_Factor from TSPL_UNIT_MASTER  WHERE Unit_Code ='" + clsCommon.myCstr(gvUOM.Rows(0).Cells(UOMColUnit).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvUOM.Rows(0).Cells(UOMColUnitDesc).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Desc"))
                gvUOM.Rows(0).Cells(UOMColConvFact).Value = clsCommon.myCdbl(dt.Rows(0)("Conv_Factor"))
                gvUOM.Rows(0).Cells(UOMColStockUnit).Value = "Y"
            End If
        End If
    End Sub
    'Public Sub ToHindiInput()
    '    Dim CName As String = ""

    '    For Each lang As InputLanguage In InputLanguage.InstalledInputLanguages
    '        CName = lang.Culture.EnglishName.ToString()

    '        If CName.StartsWith("Hindi") Then
    '            InputLanguage.CurrentInputLanguage = lang
    '        End If
    '    Next
    'End Sub
    'Public Sub ToEnglishInput()
    '    Dim CName As String = ""

    '    For Each lang As InputLanguage In InputLanguage.InstalledInputLanguages
    '        CName = lang.Culture.EnglishName.ToString()

    '        If CName.StartsWith("English") Then
    '            InputLanguage.CurrentInputLanguage = lang
    '        End If
    '    Next
    'End Sub

    Private Sub txtMCCNameHindi_Enter(sender As Object, e As EventArgs) Handles txtMCCNameHindi.Enter
        clsMccMaster.ToHindiInput()
    End Sub

    Private Sub txtMCCNameHindi_Leave(sender As Object, e As EventArgs) Handles txtMCCNameHindi.Leave
        clsMccMaster.ToEnglishInput()
    End Sub

    Private Sub fndArea__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndArea._MYValidating
        Try
            Dim sQuery As String = " Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc, Type from TSPL_LOCATION_MASTER
     "
            fndArea.Value = clsCommon.ShowSelectForm("Location@Plant@Master", sQuery, "Code", "TSPL_LOCATION_MASTER.Type <> 'PLANT' OR TSPL_LOCATION_MASTER.Location_Category <> 'Mcc'", fndArea.Value, "Code", isButtonClicked)
            'If fndLocation.Value <> "" Then
            '    lblLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndArea.Value & "'")
            'Else
            '    lblLocation.Text = ""
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
            fndMCCCode.Focus()
            clsCommon.MyMessageBoxShow(Me, "Please first select MCC Code")
            Exit Sub
        End If
        Dim frm As New Enter_password
        frm.strMCCCode = fndMCCCode.Value
        frm.ShowDialog()
    End Sub
End Class
