Imports System.IO
Imports common
Public Class frmSRN
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ItemCostTolerancePercentage As Decimal = 0
    Dim AutoClosePOBasedOnSRNQtyWithTolerance As Boolean = False
    Private PurchaseModulePickFixTaxRate As Boolean = False
    Dim ShowCapexCodeandSubCode As Boolean = False
    Dim isRGPAfterPO As Boolean = False
    Dim EnableRackBin As Integer = 0
    Dim ScheduleSettingON As Boolean = False
    Public FORMTYPE As String = Nothing
    Dim IsRateEditable As String = "0"
    Public IsPoSavedAuto As Boolean = False
    Public Allow_AutoPO As Boolean = False
    Public IsfromRGP As Boolean = False
    Private isPO_GRN_MRN_Editable As Boolean = False
    Dim AllowPurchaseModulewithUniqueItem As Integer = 0
    Dim AllowRoadPermitNo As Integer = 0
    Dim AllowSRNWithoutShortageRejection As Integer = 0
    Const ReportID As String = "SRNItemGrid"
    Public strSRNno As String = Nothing
    Public isVendorItemDetailSetting As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Private isCellValueChangedTaxOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Public isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Public Const colLineNo As String = "COLLNO"
    Public Const colRowType As String = "COLTYPE"
    Const colComplete As String = "COMPLETE"
    Const colBalanceQty As String = "BALANCEQTY"
    Public Const colICode As String = "COLICODE"
    Public Const colIName As String = "COLINAME"
    Const colHSNNo As String = "COLHSNNo"
    Const colBarCode As String = "COLBARCODE"
    Public Const colPendingQty As String = "COLPENDINGQTY"
    Const colOrgPOQty As String = "COLORGPOQTY"
    Const colOrgGRNQty As String = "COLORGGRNQTY"
    Const colOrgMRNQty As String = "COLORGMRNQTY"
    Public Const colQty As String = "COLQTY"

    Const colLeakQty As String = "COLEAKQTY"
    Const colBurstQty As String = "COLBURSTQTY"
    Const colShortQty As String = "COLSHORTQTY"

    Const colRejectedQty As String = "COLREJECTEDQTY"
    Public Const colTenderNo As String = "COLTNO"
    Public Const colWeighmentNo As String = "COLWENO"
    Public Const colWeighmentDate As String = "COLWDATE"
    Public Const colMRNNo As String = "COLMRNO"
    Public Const colMRNDate As String = "COLMRDATE"
    Const colFreeQty As String = "COLFREEQTY"
    Public Const colUnit As String = "COLUNIT"
    Const colUOMWEIGHT As String = "COLUOMWEIGHT"
    Const colUOMWeightValue As String = "COLUOMWEIGHTVALUE"
    Public Const colRate As String = "COLRATE"


    Const colRack As String = "colRack"
    Const colBin As String = "colBin"

    Const colIsInsurance As String = "colIsInsurance"
    Const colInsuranceBaseAmt As String = "colInsuranceBaseAmt"
    Const colInsurancePer As String = "colInsurancePer"

    Const colAmt As String = "COLAMT"
    Const colDisType As String = "COLDISTYPE"
    Const colHeaderDiscountPer As String = "colHeaderDiscountPer"
    Const colHeaderDiscountAmt As String = "colHeaderDiscountAmt"
    Const colDisPer As String = "COLDISPER"
    Const colDetailDisAmt As String = "colDetailDisAmt"
    Const colDisAmt As String = "COLDISAMT"
    Const colAmtAfterDis As String = "COLAMTAFTERDIS"
    Const colTaxableAmount As String = "colTaxableAmount"
    Const colTaxableAmountPer As String = "colTaxableAmountPer"
    Const colTax1 As String = "COLTAX1"
    Const colTaxBaseAmt1 As String = "COLTAXBASEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"
    Const colIsTaxable1 As String = "ISTAXABLE1"
    Const colIsSurTax1 As String = "ISSURTAX1"
    Const colSurTaxCode1 As String = "SURTAXCODE1"
    Const colTaxRecoverable1 As String = "RECOVERTABLETAX1"
    Const colTax2 As String = "COLTAX2"
    Const colTaxBaseAmt2 As String = "COLTAXBASEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colIsTaxable2 As String = "ISTAXABLE2"
    Const colIsSurTax2 As String = "ISSURTAX2"
    Const colSurTaxCode2 As String = "SURTAXCODE2"
    Const colTaxRecoverable2 As String = "RECOVERTABLETAX2"
    Const colTax3 As String = "COLTAX3"
    Const colTaxBaseAmt3 As String = "COLTAXBASEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colIsTaxable3 As String = "ISTAXABLE3"
    Const colIsSurTax3 As String = "ISSURTAX3"
    Const colSurTaxCode3 As String = "SURTAXCODE3"
    Const colTaxRecoverable3 As String = "RECOVERTABLETAX3"
    Const colTax4 As String = "COLTAX4"
    Const colTaxBaseAmt4 As String = "COLTAXBASEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colIsTaxable4 As String = "ISTAXABLE4"
    Const colIsSurTax4 As String = "ISSURTAX4"
    Const colSurTaxCode4 As String = "SURTAXCODE4"
    Const colTaxRecoverable4 As String = "RECOVERTABLETAX4"
    Const colTax5 As String = "COLTAX5"
    Const colTaxBaseAmt5 As String = "COLTAXBASEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colIsTaxable5 As String = "ISTAXABLE5"
    Const colIsSurTax5 As String = "ISSURTAX5"
    Const colSurTaxCode5 As String = "SURTAXCODE5"
    Const colTaxRecoverable5 As String = "RECOVERTABLETAX5"
    Const colTax6 As String = "COLTAX6"
    Const colTaxBaseAmt6 As String = "COLTAXBASEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colIsTaxable6 As String = "ISTAXABLE6"
    Const colIsSurTax6 As String = "ISSURTAX6"
    Const colSurTaxCode6 As String = "SURTAXCODE6"
    Const colTaxRecoverable6 As String = "RECOVERTABLETAX6"
    Const colTax7 As String = "COLTAX7"
    Const colTaxBaseAmt7 As String = "COLTAXBASEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colIsTaxable7 As String = "ISTAXABLE7"
    Const colIsSurTax7 As String = "ISSURTAX7"
    Const colSurTaxCode7 As String = "SURTAXCODE7"
    Const colTaxRecoverable7 As String = "RECOVERTABLETAX7"
    Const colTax8 As String = "COLTAX8"
    Const colTaxBaseAmt8 As String = "COLTAXBASEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colIsTaxable8 As String = "ISTAXABLE8"
    Const colIsSurTax8 As String = "ISSURTAX8"
    Const colSurTaxCode8 As String = "SURTAXCODE8"
    Const colTaxRecoverable8 As String = "RECOVERTABLETAX8"
    Const colTax9 As String = "COLTAX9"
    Const colTaxBaseAmt9 As String = "COLTAXBASEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colIsTaxable9 As String = "ISTAXABLE9"
    Const colIsSurTax9 As String = "ISSURTAX9"
    Const colSurTaxCode9 As String = "SURTAXCODE9"
    Const colTaxRecoverable9 As String = "RECOVERTABLETAX9"
    Const colTax10 As String = "COLTAX10"
    Const colTaxBaseAmt10 As String = "COLTAXBASEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colIsTaxable10 As String = "ISTAXABLE10"
    Const colIsSurTax10 As String = "ISSURTAX10"
    Const colSurTaxCode10 As String = "SURTAXCODE10"
    Const colTaxRecoverable10 As String = "RECOVERTABLETAX10"
    Const colIsExcisable1 As String = "ISEXCISABLE1"
    Const colIsExcisable2 As String = "ISEXCISABLE2"
    Const colIsExcisable3 As String = "ISEXCISABLE3"
    Const colIsExcisable4 As String = "ISEXCISABLE4"
    Const colIsExcisable5 As String = "ISEXCISABLE5"
    Const colIsExcisable6 As String = "ISEXCISABLE6"
    Const colIsExcisable7 As String = "ISEXCISABLE7"
    Const colIsExcisable8 As String = "ISEXCISABLE8"
    Const colIsExcisable9 As String = "ISEXCISABLE9"
    Const colIsExcisable10 As String = "ISEXCISABLE10"
    Const colTaxOnBaseAmt1 As String = "colTaxOnBaseAmt1"
    Const colTaxOnBaseAmt2 As String = "colTaxOnBaseAmt2"
    Const colTaxOnBaseAmt3 As String = "colTaxOnBaseAmt3"
    Const colTaxOnBaseAmt4 As String = "colTaxOnBaseAmt4"
    Const colTaxOnBaseAmt5 As String = "colTaxOnBaseAmt5"
    Const colTaxOnBaseAmt6 As String = "colTaxOnBaseAmt6"
    Const colTaxOnBaseAmt7 As String = "colTaxOnBaseAmt7"
    Const colTaxOnBaseAmt8 As String = "colTaxOnBaseAmt8"
    Const colTaxOnBaseAmt9 As String = "colTaxOnBaseAmt9"
    Const colTaxOnBaseAmt10 As String = "colTaxOnBaseAmt10"

    Const colAcceptedAmount As String = "colAcceptedAmount"
    Const colRejectedAmount As String = "colRejectedAmount"
    Const colShortageAmount As String = "colShortageAmount"
    Const colLeakAmount As String = "colLeakAmount"
    Const colBurstAmount As String = "colBurstAmount"
    Const colAmtLessDiscountWithoutShortage As String = "colAmtLessDiscountWithoutShortage"

    Const colItemTaxable As String = "colItemTaxable"
    Const colAgainstItemWiseTaxCode As String = "colAgainstItemWiseTaxCode"

    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"
    Const colPONo As String = "PONO"
    Const colGRN_NO As String = "GRN_NO"
    Const colMRN_NO As String = "MRN_NO"
    Public Const colRGPNo As String = "RGPNO"
    Public Const colIsSerialseItem As String = "COLISSERIALSEITEM"
    Public Const colIsPickAutoSrNo As String = "colIsPickAutoSrNo"
    ''Const colLocationCode As String = "LOCATIONCODE"
    ''Const colLocationName As String = "LOCATIONNAME"

    Public Const colisMRPMandatory As String = "colisMRPMandatory"
    Public Const colMRP As String = "MRP"
    '' ''Const colAssessableRate As String = "ASSESSABLERATE"
    Const colAssessableAmount As String = "ASSESSABLEAMT"
    Const colBatchNo As String = "BATCHNO"
    Const colBinNo As String = "colBinNo"
    Const colExpiry As String = "EXPIRYDATE"
    Const colManufactureDate As String = "MANUFACTUREDATE"
    Const colLandedRate As String = "LANDEDRATE"
    Const colLandedAmt As String = "LANDEDAMT"
    Const colSpecification As String = "SPECIFICATION"
    Const colRemarks As String = "REMARKS"
    Const colIsMannualAmt As String = "ISMANNUALAMT"



    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    'Const colTIsTaxable As String = "ISTAXABLE"
    Const colTTaxRate As String = "TAXRATE"
    'Const colTIsSurTax As String = "ISSURTAX"
    'Const colTSurTaxCode As String = "SURTAXCODE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"
    Const colTTaxAssessableAmt As String = "COLTTAXASSESSABLEAMT"
    Const colReqistionNo As String = "REQNO"



    Const colFCode As String = "FCODE"
    Const colFRate As String = "FRATE"
    Const colFAmt As String = "FAMT"
    Const colItmCost As String = "ItmCost"

    Const colACCode As String = "COLACCODE"
    Const colACName As String = "COLACNAME"
    Const colACAmount As String = "COLACAMOUNT"

    Const colUnitTotRecTax As String = "colUnitTotRecTax"
    Const colUnitTotNonRecTax As String = "colUnitTotNonRecTax"
    Const colUnitTotAddCost As String = "colUnitTotAddCost"
    Public Const colIsEmptyValue As String = "ISEMPTYVALUE"
    '' for abatement SRN
    Const colAbatementRate As String = "colAbatementRate"
    Const colAssesableMRP As String = "colAssesableMRP"
    Const colTotalAssesableMRP As String = "colTotalAssesableMRP"
    Const colMRNUnitCost As String = "colMRNUnitCost"
    Dim IsAbatementPO As Boolean

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim repoBalQty As GridViewDecimalColumn
    Dim repoComplete As GridViewTextBoxColumn
    Dim IsNotIncludeWasteQtyInCal As Boolean = False
    Dim isItemfromVendorItemDetails As Boolean = False
    Public IsQCColumnRequiredonMRN As Boolean = False
    '--------------------------------------
    Const colRoadsno As String = "Sno"
    Const colroadformcode As String = "FormCode"
    Const colroadformdesc As String = "FormDesc"
    Const colroadformserialno As String = "FormSerialNo"
    Const colroadformrem As String = "Remarks"

    Const colCFormsno As String = "CSno"
    Const colCFormformcode As String = "CFormCode"
    Const colCFormformdesc As String = "CFormDesc"
    Const colCFormformserialno As String = "CFormSerialNo"
    Const colCFormformrem As String = "CRemarks"
    Const colRGPICode As String = "RGPICode"
    Const colSp As String = "RGPSp"
    Const colItemConType As String = "ItemConType"
    Dim is_Srn_rejQty_goes_in_Rejstore As Boolean = False
    Dim is_Load_MRN As Boolean = False
    Const ColSelect As String = "ColSelect"

    Const colScheduleNo As String = "ScheduleNo"
    Const colOrgSchQty As String = "OrgSchQty"
    Const colOrgRGPQty As String = "OrgRGPQty"

    ''=================BM00000009405======19/10/2016==========================
    Const colItemACCode1 As String = "COLItemACCODE1"
    Const colItemACAmount1 As String = "COLItemACAMOUNT1"
    Const colItemACCalcAmount1 As String = "COLItemACCalcAMOUNT1"

    Const colItemACCode2 As String = "COLItemACCODE2"
    Const colItemACAmount2 As String = "COLItemACAMOUNT2"
    Const colItemACCalcAmount2 As String = "COLItemACCalcAMOUNT2"

    Const colItemACCode3 As String = "COLItemACCODE3"
    Const colItemACAmount3 As String = "COLItemACAMOUNT3"
    Const colItemACCalcAmount3 As String = "COLItemACCalcAMOUNT3"

    Const colItemACCode4 As String = "COLItemACCODE4"
    Const colItemACAmount4 As String = "COLItemACAMOUNT4"
    Const colItemACCalcAmount4 As String = "COLItemACCalcAMOUNT4"

    Const colItemACCode5 As String = "COLItemACCODE5"
    Const colItemACAmount5 As String = "COLItemACAMOUNT5"
    Const colItemACCalcAmount5 As String = "COLItemACCalcAMOUNT5"

    Const colItemACCode6 As String = "COLItemACCODE6"
    Const colItemACAmount6 As String = "COLItemACAMOUNT6"
    Const colItemACCalcAmount6 As String = "COLItemACCalcAMOUNT6"

    Const colItemACCode7 As String = "COLItemACCODE7"
    Const colItemACAmount7 As String = "COLItemACAMOUNT7"
    Const colItemACCalcAmount7 As String = "COLItemACCalcAMOUNT7"

    Const colItemACCode8 As String = "COLItemACCODE8"
    Const colItemACAmount8 As String = "COLItemACAMOUNT8"
    Const colItemACCalcAmount8 As String = "COLItemACCalcAMOUNT8"

    Const colItemACCode9 As String = "COLItemACCODE9"
    Const colItemACAmount9 As String = "COLItemACAMOUNT9"
    Const colItemACCalcAmount9 As String = "COLItemACCalcAMOUNT9"

    Const colItemACCode10 As String = "COLItemACCODE10"
    Const colItemACAmount10 As String = "COLItemACAMOUNT10"
    Const colItemACCalcAmount10 As String = "COLItemACCalcAMOUNT10"
    Const colItemTotalAdditionalCharge As String = "ColItemAdditionalCHarge"


    Const colCategoryType As String = "COLCATEGORYTYPE"
    Const colEmergency As String = "COLEMERGENCY"
    Const colCapexSubCode As String = "COLCAPEXSUBCODE"
    Const colCapexCode As String = "COLCAPEXCODE"

    Const colItemInsuranceBaseAmt As String = "colItemInsuranceBaseAmt"
    Const colItemInsuranceApplyOn As String = "colItemInsuranceApplyOn"
    Const colItemInsurancePer As String = "colItemInsurancePer"
    Const colItemInsuranceAmt As String = "colItemInsuranceAmt"
    Const colItemAmtAfterInsurance As String = "colItemAmtAfterInsurance"

    ''==================================================================

    Const colACInsuranceCode As String = "colACInsuranceCode"
    Const colACInsuranceName As String = "colACInsuranceName"
    Const colACInsuranceAmount As String = "colACInsuranceAmount"

    Dim DtRGP As DataTable
    Dim isApplyBrachAccounting As Boolean = False
    Dim ChkAutoDepOnPurchaseCycle As Boolean = False

    Dim isAgainstTender As Boolean = False
#End Region

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.mbtnSRN)
        'MyBase.SetUserMgmt(FORMTYPE)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnForm_Update.Visible = False ''MyBase.isModifyFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnrejetprint.Visible = MyBase.isPrintFlag
        If MyBase.isReverse Then

            btnReverse.Enabled = True

        Else

            btnReverse.Enabled = False

        End If
        btnCancel.Visible = MyBase.isCancel_Flag_After_Posting
    End Sub
    Private Sub LoadRGPType()
        isInsideLoadData = True
        Dim qry As String = "select '' as Code,'Select' as Name union all select 'AR' as Code,'Against RGP' as Name union all select 'AB' as Code,'Against BOM' as Name union all select 'AI' as Code,'As It Is' as Name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cmbRGPType.DataSource = Nothing
        cmbRGPType.DataSource = dt
        cmbRGPType.DisplayMember = "Name"
        cmbRGPType.ValueMember = "Code"
        isInsideLoadData = False
    End Sub
    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ItemCostTolerancePercentage = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ItemCostTolerancePercentage, clsFixedParameterCode.ItemCostTolerancePercentage, Nothing))
        AutoClosePOBasedOnSRNQtyWithTolerance = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoClosePOBasedOnSRNQtyWithTolerance, clsFixedParameterCode.AutoClosePOBasedOnSRNQtyWithTolerance, Nothing)) = "1", True, False))
        PurchaseModulePickFixTaxRate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchaseModulePickFixTaxRate, clsFixedParameterCode.PurchaseModulePickFixTaxRate, Nothing)) = 1, True, False)
        IsQCColumnRequiredonMRN = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsQCColumnRequiredonMRN, clsFixedParameterCode.IsQCColumnRequiredonMRN, Nothing)) = 1, True, False)
        SetUserMgmtNew()
        AllowPurchaseModulewithUniqueItem = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseModulewithUniqueItem, clsFixedParameterCode.AllowPurchaseModulewithUniqueItem, Nothing))
        '===============ADded by preeti gupta[06/01/2017]==========================
        AllowRoadPermitNo = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowRoadPermitNo, clsFixedParameterCode.AllowRoadPermitNo, Nothing))
        ShowCapexCodeandSubCode = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapex, clsFixedParameterCode.ShowOptionforSelectingCapex, Nothing)) = "1", True, False))


        isItemfromVendorItemDetails = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchasePickItemFromVendorItemDetails, clsFixedParameterCode.PurchasePickItemFromVendorItemDetails, Nothing)) = 1, True, False)
        isVendorItemDetailSetting = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowLargerItemCostThenVendorItemCost, clsFixedParameterCode.AllowLargerItemCostThenVendorItemCost, Nothing)) = 1, True, False)
        isPO_GRN_MRN_Editable = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isMRNQtyEdiatableOnSRN from TSPL_inv_parameters")) = 0, False, True)
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.mbtnSRN) = CompairStringResult.Equal Then
            is_Load_MRN = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowGRN, clsFixedParameterCode.ShowGRN, Nothing)) = 1 And clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMRN, clsFixedParameterCode.ShowMRN, Nothing)) = 1, True, False)
            If is_Load_MRN Then
                lblPoNo.Text = "GRN No"
            End If
        ElseIf clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal Then
            is_Load_MRN = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipMRNGRNinCaseofMT, clsFixedParameterCode.SkipMRNGRNinCaseofMT, Nothing)) = 0 And clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowGRN, clsFixedParameterCode.ShowGRN, Nothing)) = 1 And clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMRN, clsFixedParameterCode.ShowMRN, Nothing)) = 1, True, False)
            If is_Load_MRN Then
                lblPoNo.Text = "GRN No"
            End If
        End If
        '===================================13/02/2015=========================================
        isRGPAfterPO = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.IsRGPAfterPurchaseOrder, clsFixedParameterCode.IsRGPAfterPurchaseOrder, Nothing)) = "1", True, False))
        ScheduleSettingON = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowPOScheduling, clsFixedParameterCode.AllowPOScheduling, Nothing)) = "1", True, False))
        MyLabel9.Visible = False
        cmbRGPType.Visible = False
        If is_Load_MRN = True Then
            If isRGPAfterPO Then 'when GRN ON then RGP Off,but only when "RGP after PO" ON otherwise rgp functionality as usual done.
                RadLabel30.Visible = False
                txtRGPNo.Visible = False
                chkRGPNonInventory.Visible = False
                MyLabel9.Visible = True
                cmbRGPType.Visible = True
            End If
            MyLabel6.Visible = False 'when GRN ON then Schedule Off
            txtScheduleNo.Visible = False
        Else
            If isRGPAfterPO Then 'when GRN off and RGP after PO ON
                RadLabel30.Visible = True
                txtRGPNo.Visible = True
                chkRGPNonInventory.Visible = True
                MyLabel9.Visible = True
                cmbRGPType.Visible = True
            End If
            If ScheduleSettingON = True Then 'when GRN off and Schedule ON
                MyLabel6.Visible = True
                txtScheduleNo.Visible = True
            End If
        End If
        '============================================================================
        btnUpdateRoadPermit.Enabled = False
        dtpChallan.Value = clsCommon.GETSERVERDATE
        dtpInvoice.Value = clsCommon.GETSERVERDATE
        chkVendorGrossReceipt.Visible = False
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        txtVendorNo.MendatroryField = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A Create Additional Cost")
        'ButtonToolTip.SetToolTip(btnRequistionItems, "Press Ctrl+F7 for Select Purchase Requistion Items")
        IsAbatementPO = clsPurchaseOrderHead.GetPurchaseSetting().Rows(0).Item("IsAbatementPO")

        RadPageView1.SelectedPage = RadPageViewPage1
        LoadRGPType()
        LoadSRNType()
        LoadBlankGrid()
        LoadBlankGridTax()
        LoadItemType()
        LoadBlankGridAC()
        LoadBlankGridACInsurance()
        LoadBlankRoadPermitGrid()
        LoadBlankCFORMGrid()
        AddNew()
        SetLength()
        btnShowInventory.Enabled = True
        btnShowInventory.Visible = True
        IsNotIncludeWasteQtyInCal = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsNotIncludeWasteQtyInCal, clsFixedParameterCode.IsNotIncludeWasteQtyInCal, Nothing)) = 1, True, False)
        EnableRackBin = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableRackBin, clsFixedParameterCode.EnableRackBin, Nothing))
        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields
        '' MultiCurrency
        SetMultiCurrencyVisibility()
        '' End of MultiCurrency
        ''richa agarwal 15/07/2015 BM00000007399
        Dim WhrCls As String = String.Empty
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal Then
            WhrCls = " and Location_Type='Virtual' "
        Else
            WhrCls = " and Location_Type='Physical' or Location_Type='WorkOrder'  "
        End If
        '  If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.mbtnSRN) = CompairStringResult.Equal Then
        ' txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' " & WhrCls & " "))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If
        '  End If
        ''For Attachment
        UcAttachment1.Form_ID = MyBase.Form_ID
        RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        ''End of For Attachment
        If Not objCommonVar.IsDemoERP Then
            pnlPCJ.Visible = False
        End If
        '' make editable/non editable Term Code
        txtTermCode.Enabled = clsPurchaseOrderHead.GetInventorySetting().Rows(0).Item("IsTermsEditableOnPurchase")
        btnhistory.Enabled = False
        btnForm_Update.Enabled = False

        '-----------------check auto  po allowed?----------------------
        Dim qry As String = "select Description from TSPL_FIXED_PARAMETER where Code='AUTOPOATSRN' and Type='AUTOPOATSRN'"
        Allow_AutoPO = IIf(clsDBFuncationality.getSingleValue(qry) = "1", True, False)
        '=================Rejected Qty=======================
        is_Srn_rejQty_goes_in_Rejstore = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SRN_Rejected_Store from TSPL_PURCHASE_SETTINGS")) = 0, False, True)
        '===========================================================
        '' Anubhooti 02-Dec-2014 (Unit Cost Editable Based On Settings)
        IsRateEditable = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description From  TSPL_FIXED_PARAMETER Where Code ='" & clsFixedParameterCode.IsRateEditableOnSRN & "' And Type ='" & clsFixedParameterType.IsRateEditableOnSRN & "'"))
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.mbtnSRN) = CompairStringResult.Equal Then
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        End If
        LoadDocumentType()
        cmbDocType.Enabled = False
        isApplyBrachAccounting = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, Nothing)) = 1, True, False)
        If clsCommon.myLen(strSRNno) > 0 Then
            LoadData(strSRNno, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        EmailSmsSetting.Visibility = ElementVisibility.Collapsed
    End Sub
    Sub AllowDepartmentMandatoryOnPurchaseCycle()
        ChkAutoDepOnPurchaseCycle = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoDepartmentMendatroryFieldOnPurcahseCycle, clsFixedParameterCode.AutoDepartmentMendatroryFieldOnPurcahseCycle, Nothing)) = "1", True, False)
        If ChkAutoDepOnPurchaseCycle Then
            txtDept.Enabled = False
            txtDept.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Segment_code from TSPL_USER_MASTER where User_Code ='" + objCommonVar.CurrentUserCode + "'"))
            ' Ticket No : UDL/22/05/18-000172 By Prabhakar
            lblDept.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Description from TSPL_GL_SEGMENT_CODE where Seg_No=3 and Segment_code='" + txtDept.Value + "'"))
        Else
            txtDept.Enabled = True
        End If
    End Sub
    Sub SetMultiCurrencyVisibility()
        Dim strq As String = ""
        '=======shivani
        Dim Currency As Integer = clsModuleCurrencyMapping.GetmulticurrencyDecimalPlaces()
        txtConversionRate.DecimalPlaces = clsCommon.myCdbl(Currency)
        '================
        If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
            pnlCurrConv.Visible = True

            If clsCommon.myLen(Me.txtVendorNo.Value) > 0 Then
                strq = "select currency_code from TSPL_VENDOR_MASTER where VENDOR_CODE='" & clsCommon.myCstr(Me.txtVendorNo.Value) & "'"
                Me.txtCurrencyCode.Value = clsDBFuncationality.getSingleValue(strq).ToString
                ShowCurrencyDetail()
            End If
            ShowCurrencyDetail()
        Else
            pnlCurrConv.Visible = False
        End If

    End Sub
    Sub ShowCurrencyDetail()
        ' Dim strq As String
        Dim dt As DataTable
        If clsCommon.myLen(clsCommon.myCstr(Me.txtVendorNo.Value)) = 0 Then
            Exit Sub
        End If

        If clsCommon.myLen(txtCurrencyCode.Value) > 0 Then
            dt = clsModuleCurrencyMapping.GetLatestCurConvRateDT(Me.txtDate.Value, txtCurrencyCode.Value)
            If dt.Rows.Count = 0 Then
                If Me.txtCurrencyCode.Value = objCommonVar.BaseCurrencyCode Then
                    Me.txtConversionRate.Text = 1
                    Me.txtApplicableFrom.Text = ""
                Else
                    clsCommon.MyMessageBoxShow("Conversion rate not entered for currency '" & Me.txtCurrencyCode.Value & "'")
                    Exit Sub
                End If
            Else
                Me.txtConversionRate.Text = dt.Rows(0).Item("Rate")
                Me.txtApplicableFrom.Text = clsCommon.GetPrintDate(dt.Rows(0).Item("FROM_DATE"), "dd/MMM/yyyy")
            End If
        Else
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""
        End If

    End Sub
    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtDesc.MaxLength = 200
        txtComment.MaxLength = 200
        txtRefNo.MaxLength = 50
        txtCarrier.MaxLength = 50
        txtVehicleNo.MaxLength = 50
        txtGRNo.MaxLength = 50
        txtGENo.MaxLength = 50



    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub LoadItemType()
        'cboItemType.DataSource = clsItemMaster.GetItemType()
        Dim Whr = " AND IS_NON_INVENTORY=0   AND ITEM_TYPE_CODE NOT IN('J') "
        cboItemType.DataSource = clsItemMaster.getItemTypeQuery(Whr)
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
    End Sub
    Sub BlankAllControls()
        chkExemptSecurityDedution.Checked = False
        chkJobWorkOutward.Checked = False
        Chkroadpermit.Checked = False
        chk_c_form.Checked = False
        LoadBlankRoadPermitGrid()
        LoadBlankCFORMGrid()
        btnForm_Update.Enabled = False
        RadGroupBox3.Enabled = False
        RadGroupBox4.Enabled = False
        chkConfirmatoryPO.Checked = False
        txtQCcode.Text = ""
        txtQcdate.Text = ""
        'stuti
        txt_RoadPermitDate.Text = clsCommon.GETSERVERDATE()
        txt_RoadPermitNo.Text = ""
        '====end here====
        txtautosrnremarks.Text = ""
        'chkreadytopost.Checked = False
        txtDocNo.Value = ""
        txtDesc.Text = ""
        chkOnHold.Checked = False
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        dtpChallan.Value = clsCommon.GETSERVERDATE()
        dtpInvoice.Value = clsCommon.GETSERVERDATE()
        txtBillToLocation.Value = ""
        lblBillToLocation.Text = ""
        txtShipToLocation.Value = ""
        lblShipToLocation.Text = ""
        txtBillToLocation.Enabled = True
        txtShipToLocation.Enabled = True
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        txtSubLocation.Enabled = True
        txtDesc.Text = ""
        txtInvNo.Text = ""
        txtComment.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        txtTermCode.Value = ""
        lblTermName.Text = ""
        txtDueDate.Value = txtDate.Value
        txtRefNo.Text = ""
        lblAmtWithDiscount.Text = ""
        lblDiscountAmt.Text = ""
        lblAmtAfterDiscount.Text = ""
        lblTaxAmt.Text = ""
        lblTotRAmt.Text = ""
        lblAmount.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtCarrier.Text = ""
        txtVehicleNo.Text = ""
        txtGRNo.Text = ""
        txtGENo.Text = ""
        txtGEDate.Checked = False
        txtGEDate.Value = txtDate.Value
        lblRMDANo.Text = ""
        txtRGPNo.Value = ""
        txtDept.Value = ""
        lblDept.Text = ""
        cboItemType.SelectedIndex = 0
        cboItemType.Enabled = True
        txtBillToLocation.Enabled = True
        txtPONo.Value = ""
        txtPONo.Tag = ""
        chkVendorGrossReceipt.Checked = False
        lblAddCharges1.Text = ""
        lblAddCharges1.Text = ""
        rbtnTaxCalAutomatic.IsChecked = True
        txtRequistionNo.Value = ""
        chkExciseOnQty.Checked = False
        chkExciseOnQty.Enabled = True
        chkRGPNonInventory.Checked = False
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
            chk_qc_accepted.Visible = True
        Else
            chk_qc_accepted.Visible = False
        End If
        chk_qc_accepted.Checked = False
        chk_qc_accepted.Enabled = True
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        fndProject.Value = ""
        lblProject.Text = ""

        txtScheduleNo.Value = ""
        txtScheduleNo.Enabled = True
        cboSRNType.SelectedValue = ""
        cboSRNType.Enabled = True
        lblTaxableAmount.Text = ""
        cmbRGPType.SelectedValue = ""
        cmbRGPType.Enabled = False
        ''RICHA AGARWAL AGAINST TICKET NO. BM00000006091 ON 04/05/2015
        txtCurrencyCode.Enabled = True
        txtCurrencyCode.Value = ""
        txtConversionRate.Value = 1
        txtApplicableFrom.Text = ""
        ''--------------------------
        fndProject.Enabled = True
        lblProject.Enabled = True
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = False
        btnDelete.Enabled = False

        lblAcceptedAmt.Text = clsCommon.myFormat(0)
        lblRejectedAmt.Text = clsCommon.myFormat(0)
        lblShortageAmt.Text = clsCommon.myFormat(0)
        lblLeakAmt.Text = clsCommon.myFormat(0)
        lblBurstAmt.Text = clsCommon.myFormat(0)

        txtDate.Focus()
        '==Added by preeti gupta Against Task [KDI/12/06/18-000356]
        'If clsCommon.CompairString(clsUserMgtCode.FrmSRNMT, FORMTYPE) = CompairStringResult.Equal Then
        btnCancel.Enabled = False
        'Else
        '    btnCancel.Visible = False
        'End If
        lblAddChargesForInsurance.Text = ""
        lblAddChargesForInsurance1.Text = ""
        lblTotalInsuranceAmt.Text = ""
    End Sub
    Private Sub LoadSRNType()
        isInsideLoadData = True
        cboSRNType.DataSource = Nothing
        cboSRNType.DataSource = clsPurchaseOrderHead.LoadPurchaseType()
        cboSRNType.ValueMember = "Code"
        cboSRNType.DisplayMember = "Name"
        isInsideLoadData = False
    End Sub
    Public Shared Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = clsItemRowType.RowTypeItem
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsItemRowType.RowTypeMisc
        dt.Rows.Add(dr)

        Return dt
    End Function
    Public Shared Function GetDiscountType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = 0
        dr("Name") = "Discount"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 1
        dr("Name") = "Deduction"
        dt.Rows.Add(dr)

        Return dt
    End Function
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        SplitContainer3.Panel1Collapsed = True

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)
        repoLineNo = Nothing

        repoComplete = New GridViewTextBoxColumn()
        repoComplete.FormatString = ""
        repoComplete.HeaderText = "Complete"
        repoComplete.Width = 70
        repoComplete.Name = colComplete
        repoComplete.ReadOnly = False
        repoComplete.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoComplete)


        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Row Type"
        repoRowType.Name = colRowType
        repoRowType.Width = 50
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetItemType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoRowType)
        repoRowType = Nothing

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)
        repoICode = Nothing


        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        repoIName.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoIName)
        repoIName = Nothing


        repoIName = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "HSN No/SAC Code"
        repoIName.Name = colHSNNo
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)


        Dim repoIsSurTax1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Item Taxable"
        repoIsSurTax1.Name = colItemTaxable
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax1)

        repoIName = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Against Item Wise Tax Code"
        repoIName.Name = colAgainstItemWiseTaxCode
        repoIName.IsVisible = False
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoBarcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBarcode.FormatString = ""
        repoBarcode.HeaderText = "BAR Code"
        repoBarcode.Name = colBarCode
        repoBarcode.IsVisible = False
        repoBarcode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBarcode)
        repoBarcode = Nothing

        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending Quantity"
        repoPendingQty.Name = colPendingQty
        repoPendingQty.IsVisible = False
        repoPendingQty.Minimum = 0
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPendingQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPendingQty)
        repoPendingQty = Nothing

        repoBalQty = New GridViewDecimalColumn()
        repoBalQty.FormatString = ""
        repoBalQty.WrapText = True
        repoBalQty.HeaderText = "Balance Quantity"
        repoBalQty.Name = colBalanceQty
        repoBalQty.Width = 80
        repoBalQty.Minimum = 0
        repoBalQty.IsVisible = False
        repoBalQty.ReadOnly = True
        repoBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBalQty)

        Dim repoOrgPOQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrgPOQty.FormatString = ""
        repoOrgPOQty.WrapText = True
        repoOrgPOQty.HeaderText = "PO Quantity"
        repoOrgPOQty.Name = colOrgPOQty
        repoOrgPOQty.DecimalPlaces = 3
        ' ''richa agarwal 
        'If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal Then
        '    repoOrgPOQty.DecimalPlaces = 3
        'End If
        repoOrgPOQty.Width = 80
        repoOrgPOQty.Minimum = 0
        repoOrgPOQty.ReadOnly = Not isPO_GRN_MRN_Editable
        repoOrgPOQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgPOQty)
        repoOrgPOQty = Nothing

        Dim repoOrgGRNQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrgGRNQty.FormatString = ""
        repoOrgGRNQty.WrapText = True
        repoOrgGRNQty.HeaderText = "Challan Qty"
        repoOrgGRNQty.Name = colOrgGRNQty
        repoOrgGRNQty.Width = 80
        repoOrgGRNQty.Minimum = 0
        repoOrgGRNQty.ReadOnly = True 'Not isPO_GRN_MRN_Editable
        repoOrgGRNQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOrgGRNQty.DecimalPlaces = 3
        gv1.MasterTemplate.Columns.Add(repoOrgGRNQty)
        repoOrgGRNQty = Nothing

        '===========================13/02/2015=======================================================
        repoOrgGRNQty = New GridViewDecimalColumn()
        repoOrgGRNQty.FormatString = ""
        repoOrgGRNQty.WrapText = True
        repoOrgGRNQty.HeaderText = "Schedule Qty"
        repoOrgGRNQty.Name = colOrgSchQty
        repoOrgGRNQty.Width = 80
        repoOrgGRNQty.Minimum = 0
        repoOrgGRNQty.ReadOnly = True
        repoOrgGRNQty.IsVisible = Not is_Load_MRN AndAlso ScheduleSettingON
        repoOrgGRNQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOrgGRNQty.DecimalPlaces = 3
        gv1.MasterTemplate.Columns.Add(repoOrgGRNQty)
        repoOrgGRNQty = Nothing

        repoOrgGRNQty = New GridViewDecimalColumn()
        repoOrgGRNQty.FormatString = ""
        repoOrgGRNQty.WrapText = True
        repoOrgGRNQty.HeaderText = "RGP Qty"
        repoOrgGRNQty.Name = colOrgRGPQty
        repoOrgGRNQty.Width = 80
        repoOrgGRNQty.Minimum = 0
        repoOrgGRNQty.ReadOnly = True
        repoOrgGRNQty.DecimalPlaces = 3
        repoOrgGRNQty.IsVisible = Not is_Load_MRN AndAlso isRGPAfterPO
        repoOrgGRNQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgGRNQty)
        repoOrgGRNQty = Nothing
        '====================end here======================================================

        Dim repoOrgSRNQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrgSRNQty.FormatString = "{0:N3}"
        repoOrgSRNQty.WrapText = True
        repoOrgSRNQty.HeaderText = "Received Qty"
        repoOrgSRNQty.Name = colOrgMRNQty
        repoOrgSRNQty.DecimalPlaces = 3
        ' ''richa agarwal 
        'If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal Then
        '    repoOrgSRNQty.DecimalPlaces = 3
        'End If
        repoOrgSRNQty.Width = 80
        repoOrgSRNQty.Minimum = 0
        repoOrgSRNQty.ReadOnly = False ' Not isPO_GRN_MRN_Editable
        repoOrgSRNQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgSRNQty)
        repoOrgSRNQty = Nothing

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = "{0:N3}"
        repoQty.HeaderText = "Accepted Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.DecimalPlaces = 10
        ' ''richa agarwal 
        'If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal Then
        '    repoQty.DecimalPlaces = 3
        'End If
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)
        repoQty = Nothing

        Dim repoLeadQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLeadQty.FormatString = "{0:N3}"
        repoLeadQty.HeaderText = "Leakage"
        repoLeadQty.Name = colLeakQty
        repoLeadQty.Width = 80
        repoLeadQty.Minimum = 0
        repoLeadQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLeadQty.DecimalPlaces = 3
        gv1.MasterTemplate.Columns.Add(repoLeadQty)
        repoLeadQty = Nothing

        Dim repoBurstQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBurstQty.FormatString = "{0:N3}"
        repoBurstQty.HeaderText = "Burst"
        repoBurstQty.Name = colBurstQty
        repoBurstQty.Width = 80
        repoBurstQty.Minimum = 0
        repoBurstQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBurstQty.DecimalPlaces = 3
        gv1.MasterTemplate.Columns.Add(repoBurstQty)
        repoBurstQty = Nothing

        Dim repoShortQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShortQty.FormatString = "{0:N3}"
        repoShortQty.HeaderText = "Shortage"
        repoShortQty.Name = colShortQty
        repoShortQty.Width = 80
        repoShortQty.Minimum = 0
        repoShortQty.ReadOnly = is_Load_MRN
        repoShortQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoShortQty.DecimalPlaces = 3
        gv1.MasterTemplate.Columns.Add(repoShortQty)
        repoShortQty = Nothing

        Dim repoRejectedQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRejectedQty.FormatString = "{0:N3}"
        repoRejectedQty.HeaderText = "Rejected Quantity"
        repoRejectedQty.ReadOnly = True
        repoRejectedQty.WrapText = True
        repoRejectedQty.Name = colRejectedQty
        repoRejectedQty.Width = 80
        repoRejectedQty.Minimum = 0
        repoRejectedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoRejectedQty.DecimalPlaces = 3
        gv1.MasterTemplate.Columns.Add(repoRejectedQty)
        repoRejectedQty = Nothing

        Dim repoTenderNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTenderNo = New GridViewDecimalColumn()
        repoTenderNo.FormatString = ""
        repoTenderNo.HeaderText = "Tender No"
        repoTenderNo.Name = colTenderNo
        repoTenderNo.Width = 50
        repoTenderNo.ReadOnly = True
        repoTenderNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTenderNo)
        repoTenderNo = Nothing

        Dim repoWeighmentNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoWeighmentNo = New GridViewDecimalColumn()
        repoWeighmentNo.FormatString = ""
        repoWeighmentNo.HeaderText = "Weighment No"
        repoWeighmentNo.Name = colWeighmentNo
        repoWeighmentNo.Width = 50
        repoWeighmentNo.ReadOnly = True
        repoWeighmentNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoWeighmentNo)
        repoWeighmentNo = Nothing

        Dim repoWeighmentDate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoWeighmentDate = New GridViewDecimalColumn()
        repoWeighmentDate.FormatString = ""
        repoWeighmentDate.HeaderText = "Weighment Date"
        repoWeighmentDate.Name = colWeighmentDate
        repoWeighmentDate.Width = 50
        repoWeighmentDate.ReadOnly = True
        repoWeighmentDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoWeighmentDate)
        repoWeighmentDate = Nothing

        Dim repoMRNNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRNNo = New GridViewDecimalColumn()
        repoMRNNo.FormatString = ""
        repoMRNNo.HeaderText = "MRN No"
        repoMRNNo.Name = colMRNNo
        repoMRNNo.Width = 50
        repoMRNNo.ReadOnly = True
        repoMRNNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRNNo)
        repoMRNNo = Nothing


        Dim repoMRNDate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRNDate = New GridViewDecimalColumn()
        repoMRNDate.FormatString = ""
        repoMRNDate.HeaderText = "MRN Date"
        repoMRNDate.Name = colMRNDate
        repoMRNDate.Width = 50
        repoMRNDate.ReadOnly = True
        repoMRNDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRNDate)
        repoMRNDate = Nothing

        Dim repoFreeQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFreeQty.FormatString = "{0:N3}"
        repoFreeQty.HeaderText = "Free Quantity"
        repoFreeQty.Name = colFreeQty
        repoFreeQty.Width = 80
        repoFreeQty.Minimum = 0
        repoFreeQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoFreeQty.DecimalPlaces = 3
        gv1.MasterTemplate.Columns.Add(repoFreeQty)
        repoFreeQty = Nothing

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 80
        repoUnit.ReadOnly = False
        repoUnit.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoUnit.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoUnit) '17
        repoUnit = Nothing

        Dim UOMWEIGHT As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        UOMWEIGHT.FormatString = ""
        UOMWEIGHT.HeaderText = "UOM Weight"
        UOMWEIGHT.Name = colUOMWEIGHT
        UOMWEIGHT.Width = 150
        UOMWEIGHT.IsVisible = True
        UOMWEIGHT.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(UOMWEIGHT)
        UOMWEIGHT = Nothing

        Dim UOMWeightValue As GridViewDecimalColumn = New GridViewDecimalColumn()
        UOMWeightValue = New GridViewDecimalColumn()
        UOMWeightValue.FormatString = ""
        UOMWeightValue.HeaderText = "UOM Weight Value"
        UOMWeightValue.Name = colUOMWeightValue
        UOMWeightValue.IsVisible = True
        UOMWeightValue.Width = 200
        UOMWeightValue.Minimum = 0
        UOMWeightValue.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        UOMWeightValue.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(UOMWeightValue)
        UOMWeightValue = Nothing

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Unit Cost"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.FormatString = "{0:n4}"
        repoRate.DecimalPlaces = 4
        repoRate.ReadOnly = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate) '20
        repoRate = Nothing

        repoIsSurTax1 = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Insurance"
        repoIsSurTax1.Name = colIsInsurance
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax1) '30

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Insurance Base Amt"
        repoAmt.Name = colInsuranceBaseAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        repoAmt.IsVisible = False
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt) '21

        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Insurance %"
        repoAmt.Name = colInsurancePer
        repoAmt.Width = 100
        repoAmt.Minimum = 0
        repoAmt.Maximum = 100
        repoAmt.ReadOnly = False
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Extended Cost"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = False
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)
        repoAmt = Nothing

        Dim repoDiscountType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoDiscountType.FormatString = ""
        repoDiscountType.HeaderText = "Discount Type"
        repoDiscountType.Name = colDisType
        repoDiscountType.Width = 50
        repoDiscountType.ReadOnly = False
        repoDiscountType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoDiscountType.DataSource = GetDiscountType()
        repoDiscountType.ValueMember = "Code"
        repoDiscountType.DisplayMember = "Name"
        gv1.MasterTemplate.Columns.Add(repoDiscountType)
        repoDiscountType = Nothing

        Dim repoDisPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        Dim repoDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()

        repoDisPer = New GridViewDecimalColumn()
        repoDisPer.FormatString = "{0:N2}"
        repoDisPer.HeaderText = "Header Discount %"
        repoDisPer.Minimum = 0
        repoDisPer.Maximum = 100
        repoDisPer.Name = colHeaderDiscountPer
        repoDisPer.IsVisible = False
        repoDisPer.ReadOnly = True
        repoDisPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDisPer)

        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Header Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colHeaderDiscountAmt
        repoDisAmt.Width = 80
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDisAmt)

        repoDisPer = New GridViewDecimalColumn()
        repoDisPer.FormatString = "{0:N2}"
        repoDisPer.HeaderText = "Discount %"
        repoDisPer.Minimum = 0
        repoDisPer.Maximum = 100
        repoDisPer.Name = colDisPer
        repoDisPer.Width = 80
        repoDisPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDisPer)
        repoDisPer = Nothing


        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDetailDisAmt
        repoDisAmt.Width = 80
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDisAmt)

        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Total Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDisAmt
        repoDisAmt.Width = 80
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDisAmt)
        repoDisAmt = Nothing

        Dim repoAmtAfterDis As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterDis = New GridViewDecimalColumn()
        repoAmtAfterDis.FormatString = ""
        repoAmtAfterDis.HeaderText = "Amount After Discount"
        repoAmtAfterDis.Name = colAmtAfterDis
        repoAmtAfterDis.WrapText = True
        repoAmtAfterDis.Width = 80
        repoAmtAfterDis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterDis.VisibleInColumnChooser = False
        repoAmtAfterDis.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterDis)
        repoAmtAfterDis = Nothing


        Dim DecimalCol As New GridViewDecimalColumn()
        DecimalCol.FormatString = "{0:N2}"
        DecimalCol.HeaderText = "Item Insurance Base Amount"
        DecimalCol.Name = colItemInsuranceBaseAmt
        DecimalCol.WrapText = True
        DecimalCol.Width = 80
        DecimalCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        DecimalCol.VisibleInColumnChooser = False
        DecimalCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(DecimalCol)

        repoRowType = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Item Insurance Apply On"
        repoRowType.Name = colItemInsuranceApplyOn
        repoRowType.Width = 50
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = clsCalculationlApplyON.GetApplyOnType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Name"
        repoRowType.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoRowType)

        DecimalCol = New GridViewDecimalColumn()
        DecimalCol.FormatString = "{0:N2}"
        DecimalCol.HeaderText = "Item Insurance %"
        DecimalCol.Name = colItemInsurancePer
        DecimalCol.WrapText = True
        DecimalCol.Width = 80
        DecimalCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        DecimalCol.VisibleInColumnChooser = False
        DecimalCol.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(DecimalCol)

        DecimalCol = New GridViewDecimalColumn()
        DecimalCol.FormatString = "{0:N2}"
        DecimalCol.HeaderText = "Item Insurance Amount"
        DecimalCol.Name = colItemInsuranceAmt
        DecimalCol.WrapText = True
        DecimalCol.Width = 80
        DecimalCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        DecimalCol.VisibleInColumnChooser = False
        DecimalCol.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(DecimalCol)

        DecimalCol = New GridViewDecimalColumn()
        DecimalCol.FormatString = "{0:N2}"
        DecimalCol.HeaderText = "Item Amount After Insurance"
        DecimalCol.Name = colItemAmtAfterInsurance
        DecimalCol.WrapText = True
        DecimalCol.Width = 80
        DecimalCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        DecimalCol.VisibleInColumnChooser = False
        DecimalCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(DecimalCol)



        repoAmtAfterDis = New GridViewDecimalColumn()
        repoAmtAfterDis.FormatString = ""
        repoAmtAfterDis.HeaderText = "Taxable Amount %"
        repoAmtAfterDis.Name = colTaxableAmountPer
        repoAmtAfterDis.WrapText = False
        repoAmtAfterDis.IsVisible = True
        repoAmtAfterDis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterDis.VisibleInColumnChooser = False
        repoAmtAfterDis.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterDis)


        repoAmtAfterDis = New GridViewDecimalColumn()
        repoAmtAfterDis.FormatString = ""
        repoAmtAfterDis.HeaderText = "Taxable Amount"
        repoAmtAfterDis.Name = colTaxableAmount
        repoAmtAfterDis.WrapText = True
        repoAmtAfterDis.Width = 150
        repoAmtAfterDis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterDis.VisibleInColumnChooser = False
        repoAmtAfterDis.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterDis)

        Dim repoAssessableAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessableAmt.WrapText = True
        repoAssessableAmt.ReadOnly = True
        repoAssessableAmt.FormatString = ""
        repoAssessableAmt.HeaderText = "Assessable Amount"
        repoAssessableAmt.Name = colAssessableAmount
        repoAssessableAmt.IsVisible = False
        repoAssessableAmt.Minimum = 0
        repoAssessableAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessableAmt) '25
        repoAssessableAmt = Nothing

        Dim repoTax1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax1.FormatString = ""
        repoTax1.HeaderText = "Tax 1"
        repoTax1.Name = colTax1
        repoTax1.ReadOnly = True
        repoTax1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax1)

        Dim repoTaxBaseAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt1.FormatString = ""
        repoTaxBaseAmt1.HeaderText = "Tax Base Amount 1"
        repoTaxBaseAmt1.Name = colTaxBaseAmt1
        repoTaxBaseAmt1.ReadOnly = True
        repoTaxBaseAmt1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt1)

        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colTaxRate1
        repoTaxRate1.IsVisible = False
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate1)

        Dim repoTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1 = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.HeaderText = "Tax Amt 1"
        repoTaxAmt1.Name = colTaxAmt1
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt1)

        repoIsSurTax1 = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Surtax 1"
        repoIsSurTax1.Name = colIsSurTax1
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax1)

        Dim repoSurTaxCode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode1.FormatString = ""
        repoSurTaxCode1.HeaderText = "Surtax 1"
        repoSurTaxCode1.Name = colSurTaxCode1
        repoSurTaxCode1.ReadOnly = True
        repoSurTaxCode1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode1)

        Dim repoIsTaxable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Is Taxable 1"
        repoIsTaxable1.Name = colIsTaxable1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1) '32



        Dim repoTaxRecoverable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable1.HeaderText = "Recoverable Tax 1"
        repoTaxRecoverable1.Name = colTaxRecoverable1
        repoTaxRecoverable1.ReadOnly = True
        repoTaxRecoverable1.IsVisible = False
        repoTaxRecoverable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable1)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 1"
        repoIsTaxable1.Name = colTaxOnBaseAmt1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoTax2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2.FormatString = ""
        repoTax2.HeaderText = "Tax 2"
        repoTax2.Name = colTax2
        repoTax2.ReadOnly = True
        repoTax2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax2)

        Dim repoTaxBaseAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt2.FormatString = ""
        repoTaxBaseAmt2.HeaderText = "Tax Base Amount 2"
        repoTaxBaseAmt2.Name = colTaxBaseAmt2
        repoTaxBaseAmt2.ReadOnly = True
        repoTaxBaseAmt2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt2)

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colTaxRate2
        repoTaxRate2.IsVisible = False
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate2)

        Dim repoTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2 = New GridViewDecimalColumn()
        repoTaxAmt2.FormatString = ""
        repoTaxAmt2.HeaderText = "Tax Amt 2"
        repoTaxAmt2.Name = colTaxAmt2
        repoTaxAmt2.IsVisible = False
        repoTaxAmt2.ReadOnly = True
        repoTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt2)

        Dim repoIsSurTax2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax2.HeaderText = "Is Surtax 2"
        repoIsSurTax2.Name = colIsSurTax2
        repoIsSurTax2.ReadOnly = True
        repoIsSurTax2.IsVisible = False
        repoIsSurTax2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax2)

        Dim repoSurTaxCode2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode2.FormatString = ""
        repoSurTaxCode2.HeaderText = "Surtax 2"
        repoSurTaxCode2.Name = colSurTaxCode2
        repoSurTaxCode2.ReadOnly = True
        repoSurTaxCode2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode2)

        Dim repoIsTaxable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable2.HeaderText = "Is Taxable 2"
        repoIsTaxable2.Name = colIsTaxable2
        repoIsTaxable2.ReadOnly = True
        repoIsTaxable2.IsVisible = False
        repoIsTaxable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable2) '40

        Dim repoTaxRecoverable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable2.HeaderText = "Recoverable Tax 2"
        repoTaxRecoverable2.Name = colTaxRecoverable2
        repoTaxRecoverable2.ReadOnly = True
        repoTaxRecoverable2.IsVisible = False
        repoTaxRecoverable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable2)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 2"
        repoIsTaxable1.Name = colTaxOnBaseAmt2
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)


        Dim repoTax3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax3.FormatString = ""
        repoTax3.HeaderText = "Tax 3"
        repoTax3.Name = colTax3
        repoTax3.ReadOnly = True
        repoTax3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax3)

        Dim repoTaxBaseAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt3.FormatString = ""
        repoTaxBaseAmt3.HeaderText = "Tax Base Amount 3"
        repoTaxBaseAmt3.Name = colTaxBaseAmt3
        repoTaxBaseAmt3.ReadOnly = True
        repoTaxBaseAmt3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt3)

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colTaxRate3
        repoTaxRate3.IsVisible = False
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate3)

        Dim repoTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3 = New GridViewDecimalColumn()
        repoTaxAmt3.FormatString = ""
        repoTaxAmt3.HeaderText = "Tax Amt 3"
        repoTaxAmt3.Name = colTaxAmt3
        repoTaxAmt3.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt3)

        Dim repoIsSurTax3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax3.HeaderText = "Is Surtax 3"
        repoIsSurTax3.Name = colIsSurTax3
        repoIsSurTax3.ReadOnly = True
        repoIsSurTax3.IsVisible = False
        repoIsSurTax3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax3)

        Dim repoSurTaxCode3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode3.FormatString = ""
        repoSurTaxCode3.HeaderText = "Surtax 3"
        repoSurTaxCode3.Name = colSurTaxCode3
        repoSurTaxCode3.ReadOnly = True
        repoSurTaxCode3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode3)

        Dim repoIsTaxable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable3.HeaderText = "Is Taxable 3"
        repoIsTaxable3.Name = colIsTaxable3
        repoIsTaxable3.ReadOnly = True
        repoIsTaxable3.IsVisible = False
        repoIsTaxable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable3) '48

        Dim repoTaxRecoverable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable3.HeaderText = "Recoverable Tax 3"
        repoTaxRecoverable3.Name = colTaxRecoverable3
        repoTaxRecoverable3.ReadOnly = True
        repoTaxRecoverable3.IsVisible = False
        repoTaxRecoverable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable3)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 3"
        repoIsTaxable1.Name = colTaxOnBaseAmt3
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoTax4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4.FormatString = ""
        repoTax4.HeaderText = "Tax 4"
        repoTax4.Name = colTax4
        repoTax4.ReadOnly = True
        repoTax4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax4)

        Dim repoTaxBaseAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt4.FormatString = ""
        repoTaxBaseAmt4.HeaderText = "Tax Base Amount 4"
        repoTaxBaseAmt4.Name = colTaxBaseAmt4
        repoTaxBaseAmt4.ReadOnly = True
        repoTaxBaseAmt4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt4)

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colTaxRate4
        repoTaxRate4.IsVisible = False
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate4)

        Dim repoTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4 = New GridViewDecimalColumn()
        repoTaxAmt4.FormatString = ""
        repoTaxAmt4.HeaderText = "Tax Amt 4"
        repoTaxAmt4.Name = colTaxAmt4
        repoTaxAmt4.IsVisible = False
        repoTaxAmt4.ReadOnly = True
        repoTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt4)

        Dim repoIsSurTax4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax4.HeaderText = "Is Surtax 4"
        repoIsSurTax4.Name = colIsSurTax4
        repoIsSurTax4.ReadOnly = True
        repoIsSurTax4.IsVisible = False
        repoIsSurTax4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax4)

        Dim repoSurTaxCode4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode4.FormatString = ""
        repoSurTaxCode4.HeaderText = "Surtax 4"
        repoSurTaxCode4.Name = colSurTaxCode4
        repoSurTaxCode4.ReadOnly = True
        repoSurTaxCode4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode4)

        Dim repoIsTaxable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable4.HeaderText = "Is Taxable 4"
        repoIsTaxable4.Name = colIsTaxable4
        repoIsTaxable4.ReadOnly = True
        repoIsTaxable4.IsVisible = False
        repoIsTaxable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable4) '56

        Dim repoTaxRecoverable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable4.HeaderText = "Recoverable Tax 4"
        repoTaxRecoverable4.Name = colTaxRecoverable4
        repoTaxRecoverable4.ReadOnly = True
        repoTaxRecoverable4.IsVisible = False
        repoTaxRecoverable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable4)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 4"
        repoIsTaxable1.Name = colTaxOnBaseAmt4
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)


        Dim repoTax5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax5.FormatString = ""
        repoTax5.HeaderText = "Tax 5"
        repoTax5.Name = colTax5
        repoTax5.ReadOnly = True
        repoTax5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax5)

        Dim repoTaxBaseAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt5.FormatString = ""
        repoTaxBaseAmt5.HeaderText = "Tax Base Amount 5"
        repoTaxBaseAmt5.Name = colTaxBaseAmt5
        repoTaxBaseAmt5.ReadOnly = True
        repoTaxBaseAmt5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt5)

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colTaxRate5
        repoTaxRate5.IsVisible = False
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate5)

        Dim repoTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5 = New GridViewDecimalColumn()
        repoTaxAmt5.FormatString = ""
        repoTaxAmt5.HeaderText = "Tax Amt 5"
        repoTaxAmt5.Name = colTaxAmt5
        repoTaxAmt5.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt5)

        Dim repoIsSurTax5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax5.HeaderText = "Is Surtax 5"
        repoIsSurTax5.Name = colIsSurTax5
        repoIsSurTax5.ReadOnly = True
        repoIsSurTax5.IsVisible = False
        repoIsSurTax5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax5)

        Dim repoSurTaxCode5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode5.FormatString = ""
        repoSurTaxCode5.HeaderText = "Surtax 5"
        repoSurTaxCode5.Name = colSurTaxCode5
        repoSurTaxCode5.ReadOnly = True
        repoSurTaxCode5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode5)

        Dim repoIsTaxable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable5.HeaderText = "Is Taxable 5"
        repoIsTaxable5.Name = colIsTaxable5
        repoIsTaxable5.ReadOnly = True
        repoIsTaxable5.IsVisible = False
        repoIsTaxable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable5) '64

        Dim repoTaxRecoverable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable5.HeaderText = "Recoverable Tax 5"
        repoTaxRecoverable5.Name = colTaxRecoverable5
        repoTaxRecoverable5.ReadOnly = True
        repoTaxRecoverable5.IsVisible = False
        repoTaxRecoverable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable5)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 5"
        repoIsTaxable1.Name = colTaxOnBaseAmt5
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoTax6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax6.FormatString = ""
        repoTax6.HeaderText = "Tax 6"
        repoTax6.Name = colTax6
        repoTax6.ReadOnly = True
        repoTax6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax6)

        Dim repoTaxBaseAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt6.FormatString = ""
        repoTaxBaseAmt6.HeaderText = "Tax Base Amount 6"
        repoTaxBaseAmt6.Name = colTaxBaseAmt6
        repoTaxBaseAmt6.ReadOnly = True
        repoTaxBaseAmt6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt6)

        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colTaxRate6
        repoTaxRate6.IsVisible = False
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate6)

        Dim repoTaxAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt6 = New GridViewDecimalColumn()
        repoTaxAmt6.FormatString = ""
        repoTaxAmt6.HeaderText = "Tax Amt 6"
        repoTaxAmt6.Name = colTaxAmt6
        repoTaxAmt6.IsVisible = False
        repoTaxAmt6.ReadOnly = True
        repoTaxAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt6)

        Dim repoIsSurTax6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax6.HeaderText = "Is Surtax 6"
        repoIsSurTax6.Name = colIsSurTax6
        repoIsSurTax6.ReadOnly = True
        repoIsSurTax6.IsVisible = False
        repoIsSurTax6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax6)

        Dim repoSurTaxCode6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode6.FormatString = ""
        repoSurTaxCode6.HeaderText = "Surtax 6"
        repoSurTaxCode6.Name = colSurTaxCode6
        repoSurTaxCode6.ReadOnly = True
        repoSurTaxCode6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode6)

        Dim repoIsTaxable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable6.HeaderText = "Is Taxable 6"
        repoIsTaxable6.Name = colIsTaxable6
        repoIsTaxable6.ReadOnly = True
        repoIsTaxable6.IsVisible = False
        repoIsTaxable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable6) '72

        Dim repoTaxRecoverable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable6.HeaderText = "Recoverable Tax 6"
        repoTaxRecoverable6.Name = colTaxRecoverable6
        repoTaxRecoverable6.ReadOnly = True
        repoTaxRecoverable6.IsVisible = False
        repoTaxRecoverable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable6)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 6"
        repoIsTaxable1.Name = colTaxOnBaseAmt6
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoTax7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax7.FormatString = ""
        repoTax7.HeaderText = "Tax 7"
        repoTax7.Name = colTax7
        repoTax7.ReadOnly = True
        repoTax7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax7)

        Dim repoTaxBaseAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt7.FormatString = ""
        repoTaxBaseAmt7.HeaderText = "Tax Base Amount 7"
        repoTaxBaseAmt7.Name = colTaxBaseAmt7
        repoTaxBaseAmt7.ReadOnly = True
        repoTaxBaseAmt7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt7)

        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colTaxRate7
        repoTaxRate7.IsVisible = False
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate7)

        Dim repoTaxAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt7 = New GridViewDecimalColumn()
        repoTaxAmt7.FormatString = ""
        repoTaxAmt7.HeaderText = "Tax Amt 7"
        repoTaxAmt7.Name = colTaxAmt7
        repoTaxAmt7.IsVisible = False
        repoTaxAmt7.ReadOnly = True
        repoTaxAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt7)

        Dim repoIsSurTax7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax7.HeaderText = "Is Surtax 7"
        repoIsSurTax7.Name = colIsSurTax7
        repoIsSurTax7.ReadOnly = True
        repoIsSurTax7.IsVisible = False
        repoIsSurTax7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax7)

        Dim repoSurTaxCode7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode7.FormatString = ""
        repoSurTaxCode7.HeaderText = "Surtax 7"
        repoSurTaxCode7.Name = colSurTaxCode7
        repoSurTaxCode7.ReadOnly = True
        repoSurTaxCode7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode7)

        Dim repoIsTaxable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable7.HeaderText = "Is Taxable 7"
        repoIsTaxable7.Name = colIsTaxable7
        repoIsTaxable7.ReadOnly = True
        repoIsTaxable7.IsVisible = False
        repoIsTaxable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable7) '80

        Dim repoTaxRecoverable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable7.HeaderText = "Recoverable Tax 7"
        repoTaxRecoverable7.Name = colTaxRecoverable7
        repoTaxRecoverable7.ReadOnly = True
        repoTaxRecoverable7.IsVisible = False
        repoTaxRecoverable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable7)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 7"
        repoIsTaxable1.Name = colTaxOnBaseAmt7
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoTax8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax8.FormatString = ""
        repoTax8.HeaderText = "Tax 8"
        repoTax8.Name = colTax8
        repoTax8.ReadOnly = True
        repoTax8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax8)

        Dim repoTaxBaseAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt8.FormatString = ""
        repoTaxBaseAmt8.HeaderText = "Tax Base Amount 8"
        repoTaxBaseAmt8.Name = colTaxBaseAmt8
        repoTaxBaseAmt8.ReadOnly = True
        repoTaxBaseAmt8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt8)

        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colTaxRate8
        repoTaxRate8.IsVisible = False
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate8)

        Dim repoTaxAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt8 = New GridViewDecimalColumn()
        repoTaxAmt8.FormatString = ""
        repoTaxAmt8.HeaderText = "Tax Amt 8"
        repoTaxAmt8.Name = colTaxAmt8
        repoTaxAmt8.IsVisible = False
        repoTaxAmt8.ReadOnly = True
        repoTaxAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt8)

        Dim repoIsSurTax8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax8.HeaderText = "Is Surtax 8"
        repoIsSurTax8.Name = colIsSurTax8
        repoIsSurTax8.ReadOnly = True
        repoIsSurTax8.IsVisible = False
        repoIsSurTax8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax8)

        Dim repoSurTaxCode8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode8.FormatString = ""
        repoSurTaxCode8.HeaderText = "Surtax 8"
        repoSurTaxCode8.Name = colSurTaxCode8
        repoSurTaxCode8.ReadOnly = True
        repoSurTaxCode8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode8)

        Dim repoIsTaxable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable8.HeaderText = "Is Taxable 8"
        repoIsTaxable8.Name = colIsTaxable8
        repoIsTaxable8.ReadOnly = True
        repoIsTaxable8.IsVisible = False
        repoIsTaxable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable8) '88

        Dim repoTaxRecoverable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable8.HeaderText = "Recoverable Tax 8"
        repoTaxRecoverable8.Name = colTaxRecoverable8
        repoTaxRecoverable8.ReadOnly = True
        repoTaxRecoverable8.IsVisible = False
        repoTaxRecoverable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable8)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 8"
        repoIsTaxable1.Name = colTaxOnBaseAmt8
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoTax9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax9.FormatString = ""
        repoTax9.HeaderText = "Tax 9"
        repoTax9.Name = colTax9
        repoTax9.ReadOnly = True
        repoTax9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax9)

        Dim repoTaxBaseAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt9.FormatString = ""
        repoTaxBaseAmt9.HeaderText = "Tax Base Amount 9"
        repoTaxBaseAmt9.Name = colTaxBaseAmt9
        repoTaxBaseAmt9.ReadOnly = True
        repoTaxBaseAmt9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt9)

        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colTaxRate9
        repoTaxRate9.IsVisible = False
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate9)

        Dim repoTaxAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt9 = New GridViewDecimalColumn()
        repoTaxAmt9.FormatString = ""
        repoTaxAmt9.HeaderText = "Tax Amt 9"
        repoTaxAmt9.Name = colTaxAmt9
        repoTaxAmt9.IsVisible = False
        repoTaxAmt9.ReadOnly = True
        repoTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt9)

        Dim repoIsSurTax9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax9.HeaderText = "Is Surtax 9"
        repoIsSurTax9.Name = colIsSurTax9
        repoIsSurTax9.ReadOnly = True
        repoIsSurTax9.IsVisible = False
        repoIsSurTax9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax9)

        Dim repoSurTaxCode9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode9.FormatString = ""
        repoSurTaxCode9.HeaderText = "Surtax 9"
        repoSurTaxCode9.Name = colSurTaxCode9
        repoSurTaxCode9.ReadOnly = True
        repoSurTaxCode9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode9)

        Dim repoIsTaxable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable9.HeaderText = "Is Taxable 9"
        repoIsTaxable9.Name = colIsTaxable9
        repoIsTaxable9.ReadOnly = True
        repoIsTaxable9.IsVisible = False
        repoIsTaxable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable9) '96

        Dim repoTaxRecoverable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable9.HeaderText = "Recoverable Tax 9"
        repoTaxRecoverable9.Name = colTaxRecoverable9
        repoTaxRecoverable9.ReadOnly = True
        repoTaxRecoverable9.IsVisible = False
        repoTaxRecoverable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable9)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 9"
        repoIsTaxable1.Name = colTaxOnBaseAmt9
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoTax10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax10.FormatString = ""
        repoTax10.HeaderText = "Tax 10"
        repoTax10.Name = colTax10
        repoTax10.ReadOnly = True
        repoTax10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax10)

        Dim repoTaxBaseAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt10.FormatString = ""
        repoTaxBaseAmt10.HeaderText = "Tax Base Amount 10"
        repoTaxBaseAmt10.Name = colTaxBaseAmt10
        repoTaxBaseAmt10.ReadOnly = True
        repoTaxBaseAmt10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt10)

        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colTaxRate10
        repoTaxRate10.IsVisible = False
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate10)

        Dim repoTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt10 = New GridViewDecimalColumn()
        repoTaxAmt10.FormatString = ""
        repoTaxAmt10.HeaderText = "Tax Amt 10"
        repoTaxAmt10.Name = colTaxAmt10
        repoTaxAmt10.IsVisible = False
        repoTaxAmt10.ReadOnly = True
        repoTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt10)

        Dim repoIsSurTax10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax10.HeaderText = "Is Surtax 10"
        repoIsSurTax10.Name = colIsSurTax10
        repoIsSurTax10.ReadOnly = True
        repoIsSurTax10.IsVisible = False
        repoIsSurTax10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax10)

        Dim repoSurTaxCode10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode10.FormatString = ""
        repoSurTaxCode10.HeaderText = "Surtax 10"
        repoSurTaxCode10.Name = colSurTaxCode10
        repoSurTaxCode10.ReadOnly = True
        repoSurTaxCode10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode10)

        Dim repoIsTaxable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable10.HeaderText = "Is Taxable 10"
        repoIsTaxable10.Name = colIsTaxable10
        repoIsTaxable10.ReadOnly = True
        repoIsTaxable10.IsVisible = False
        repoIsTaxable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable10) '104

        Dim repoTaxRecoverable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable10.HeaderText = "Recoverable Tax 10"
        repoTaxRecoverable10.Name = colTaxRecoverable10
        repoTaxRecoverable10.ReadOnly = True
        repoTaxRecoverable10.IsVisible = False
        repoTaxRecoverable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable10)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 10"
        repoIsTaxable1.Name = colTaxOnBaseAmt10
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoIsExcisable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable1.HeaderText = "Is Excisable 1"
        repoIsExcisable1.Name = colIsExcisable1
        repoIsExcisable1.ReadOnly = True
        repoIsExcisable1.IsVisible = False
        repoIsExcisable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable1) '106

        Dim repoIsExcisable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable2.HeaderText = "Is Excisable 2"
        repoIsExcisable2.Name = colIsExcisable2
        repoIsExcisable2.ReadOnly = True
        repoIsExcisable2.IsVisible = False
        repoIsExcisable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable2)

        Dim repoIsExcisable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable3.HeaderText = "Is Excisable 3"
        repoIsExcisable3.Name = colIsExcisable3
        repoIsExcisable3.ReadOnly = True
        repoIsExcisable3.IsVisible = False
        repoIsExcisable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable3)

        Dim repoIsExcisable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable4.HeaderText = "Is Excisable 4"
        repoIsExcisable4.Name = colIsExcisable4
        repoIsExcisable4.ReadOnly = True
        repoIsExcisable4.IsVisible = False
        repoIsExcisable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable4)

        Dim repoIsExcisable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable5.HeaderText = "Is Excisable 5"
        repoIsExcisable5.Name = colIsExcisable5
        repoIsExcisable5.ReadOnly = True
        repoIsExcisable5.IsVisible = False
        repoIsExcisable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable5)

        Dim repoIsExcisable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable6.HeaderText = "Is Excisable 6"
        repoIsExcisable6.Name = colIsExcisable6
        repoIsExcisable6.ReadOnly = True
        repoIsExcisable6.IsVisible = False
        repoIsExcisable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable6)

        Dim repoIsExcisable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable7.HeaderText = "Is Excisable 7"
        repoIsExcisable7.Name = colIsExcisable7
        repoIsExcisable7.ReadOnly = True
        repoIsExcisable7.IsVisible = False
        repoIsExcisable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable7)

        Dim repoIsExcisable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable8.HeaderText = "Is Excisable 8"
        repoIsExcisable8.Name = colIsExcisable8
        repoIsExcisable8.ReadOnly = True
        repoIsExcisable8.IsVisible = False
        repoIsExcisable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable8)

        Dim repoIsExcisable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable9.HeaderText = "Is Excisable 9"
        repoIsExcisable9.Name = colIsExcisable9
        repoIsExcisable9.ReadOnly = True
        repoIsExcisable9.IsVisible = False
        repoIsExcisable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable9)

        Dim repoIsExcisable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable10.HeaderText = "Is Excisable 10"
        repoIsExcisable10.Name = colIsExcisable10
        repoIsExcisable10.ReadOnly = True
        repoIsExcisable10.IsVisible = False
        repoIsExcisable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable10) '115


        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt) '116

        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Included Tax Amount"
        repoAmtAfterTax.Name = colAmtAfterTax
        repoAmtAfterTax.WrapText = True
        repoAmtAfterTax.Width = 80
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterTax) '117


        Dim repoLandedRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLandedRate.FormatString = ""
        repoLandedRate.HeaderText = "Landed Rate"
        repoLandedRate.Name = colLandedRate
        repoLandedRate.WrapText = True
        repoLandedRate.Width = 80
        repoLandedRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLandedRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLandedRate)

        Dim repoLandedAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLandedAmt.FormatString = ""
        repoLandedAmt.HeaderText = "Landed Amount"
        repoLandedAmt.Name = colLandedAmt
        repoLandedAmt.WrapText = True
        repoLandedAmt.Width = 80
        repoLandedAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLandedAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLandedAmt)

        Dim repoMRN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMRN.FormatString = ""
        repoMRN.HeaderText = "MRN No"
        repoMRN.Name = colPONo
        repoMRN.ReadOnly = True
        repoMRN.Width = 100
        gv1.MasterTemplate.Columns.Add(repoMRN)


        If is_Load_MRN Then
            repoMRN.HeaderText = "PO No"
            Dim repo_MRN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repo_MRN.FormatString = ""
            repo_MRN.HeaderText = "MRN No"
            repo_MRN.Name = colMRN_NO
            repo_MRN.ReadOnly = True
            repo_MRN.Width = 100
            gv1.MasterTemplate.Columns.Add(repo_MRN)

            repo_MRN = New GridViewTextBoxColumn()
            repo_MRN.FormatString = ""
            repo_MRN.HeaderText = "GRN No"
            repo_MRN.Name = colGRN_NO
            repo_MRN.ReadOnly = True
            repo_MRN.Width = 100
            gv1.MasterTemplate.Columns.Add(repo_MRN)

        End If

        Dim repoRGPNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRGPNo.FormatString = ""
        repoRGPNo.HeaderText = "RGP No"
        repoRGPNo.Name = colRGPNo
        repoRGPNo.ReadOnly = True
        repoRGPNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRGPNo) '121
        repoRGPNo = Nothing

        repoRGPNo = New GridViewTextBoxColumn()
        repoRGPNo.FormatString = ""
        repoRGPNo.HeaderText = "Schedule No"
        repoRGPNo.Name = colScheduleNo
        repoRGPNo.ReadOnly = True
        repoRGPNo.Width = 100
        repoRGPNo.IsVisible = Not is_Load_MRN AndAlso ScheduleSettingON
        gv1.MasterTemplate.Columns.Add(repoRGPNo) '121
        repoRGPNo = Nothing

        Dim repoIsMRPMandatory As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsMRPMandatory.HeaderText = "Is MRP Mandatory"
        repoIsMRPMandatory.Name = colisMRPMandatory
        repoIsMRPMandatory.IsVisible = False
        repoIsMRPMandatory.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoIsMRPMandatory.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIsMRPMandatory) ''122

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.FormatString = "{0:n4}"
        repoMRP.DecimalPlaces = 4
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = False
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal Then
            repoMRP.IsVisible = False
        Else
            repoMRP.IsVisible = True
        End If
        gv1.MasterTemplate.Columns.Add(repoMRP)

        ''Dim repoAssessable As GridViewDecimalColumn = New GridViewDecimalColumn()
        ''repoAssessable.FormatString = ""
        ''repoAssessable.HeaderText = "Assessable"
        ''repoAssessable.Name = colAssessableRate
        ''repoAssessable.Width = 80
        ''repoAssessable.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        ''repoAssessable.ReadOnly = True
        ''gv1.MasterTemplate.Columns.Add(repoAssessable)

        ''Dim repoAssessableAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        ''repoAssessableAmt.WrapText = True
        ''repoAssessableAmt.ReadOnly = True
        ''repoAssessableAmt.FormatString = ""
        ''repoAssessableAmt.HeaderText = "Assessable Amount"
        ''repoAssessableAmt.Name = colAssessableAmount
        ''repoAssessableAmt.Width = 80
        ''repoAssessableAmt.Minimum = 0
        ''repoAssessableAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        ''gv1.MasterTemplate.Columns.Add(repoAssessableAmt)

        Dim repoBinNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBinNo.FormatString = ""
        repoBinNo.HeaderText = "Bin No"
        repoBinNo.Name = colBinNo
        repoBinNo.ReadOnly = False
        repoBinNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoBinNo) '124

        Dim repoBatchNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBatchNo.FormatString = ""
        repoBatchNo.HeaderText = "Batch No"
        repoBatchNo.Name = colBatchNo
        repoBatchNo.ReadOnly = False
        repoBatchNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoBatchNo)

        Dim repoExpiry As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoExpiry.Format = DateTimePickerFormat.Custom
        repoExpiry.CustomFormat = "dd-MM-yyyy"
        repoExpiry.HeaderText = "Expiry Date"
        repoExpiry.FormatString = "{0:d}"
        repoExpiry.Name = colExpiry
        repoExpiry.WrapText = True
        repoExpiry.ReadOnly = False
        repoExpiry.Width = 80
        gv1.MasterTemplate.Columns.Add(repoExpiry)

        Dim repoManDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoManDate.Format = DateTimePickerFormat.Custom
        repoManDate.CustomFormat = "dd-MM-yyyy"
        repoManDate.HeaderText = "Manufacturer Date"
        repoManDate.WrapText = True
        repoManDate.FormatString = "{0:d}"
        repoManDate.Name = colManufactureDate
        repoManDate.ReadOnly = False
        repoManDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoManDate)

        Dim repoSpecification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSpecification.FormatString = ""
        repoSpecification.HeaderText = "Specification"
        repoSpecification.Name = colSpecification
        repoSpecification.Width = 100
        gv1.MasterTemplate.Columns.Add(repoSpecification) '128


        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRemarks)



        Dim repoFCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFCode.FormatString = ""
        repoFCode.HeaderText = "Father Code"
        repoFCode.Name = colFCode
        repoFCode.IsVisible = False
        repoFCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFCode)

        Dim repoFRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFRate.FormatString = ""
        repoFRate.HeaderText = "Father Rate"
        repoFRate.Name = colFRate
        repoFRate.IsVisible = False
        repoFRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoFRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFRate)

        Dim repoFAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFAmt.FormatString = ""
        repoFAmt.HeaderText = "Father Amount"
        repoFAmt.Name = colFAmt
        repoFAmt.IsVisible = False
        repoFAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoFAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFAmt) '132

        Dim repoUnitTotNonRecTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnitTotNonRecTax.FormatString = ""
        repoUnitTotNonRecTax.HeaderText = "Total Non-Recovered Tax Per Unit"
        repoUnitTotNonRecTax.Name = colUnitTotNonRecTax
        repoUnitTotNonRecTax.IsVisible = False
        repoUnitTotNonRecTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoUnitTotNonRecTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnitTotNonRecTax)


        Dim repoUnitTotRecTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnitTotRecTax.FormatString = ""
        repoUnitTotRecTax.HeaderText = "Total Recovered Tax Per Unit"
        repoUnitTotRecTax.Name = colUnitTotRecTax
        repoUnitTotRecTax.IsVisible = False
        repoUnitTotRecTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoUnitTotRecTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnitTotRecTax)


        Dim repoUnitTotAddCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnitTotAddCost.FormatString = ""
        repoUnitTotAddCost.HeaderText = "Total Addtional Cost Per Unit"
        repoUnitTotAddCost.Name = colUnitTotAddCost
        repoUnitTotAddCost.IsVisible = False
        repoUnitTotAddCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoUnitTotAddCost.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnitTotAddCost) '135

        Dim repoMannulaAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMannulaAmt.FormatString = ""
        repoMannulaAmt.HeaderText = "Is Mannual amount"
        repoMannulaAmt.Name = colIsMannualAmt
        repoMannulaAmt.IsVisible = False
        repoMannulaAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMannulaAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMannulaAmt)

        Dim repoIsEmptyValue As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsEmptyValue.HeaderText = "Is Empty Value"
        repoIsEmptyValue.Name = colIsEmptyValue
        repoIsEmptyValue.ReadOnly = True
        repoIsEmptyValue.IsVisible = False
        repoIsEmptyValue.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsEmptyValue) '137

        Dim repoRequition As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRequition.FormatString = ""
        repoRequition.HeaderText = "Requition No"
        repoRequition.Name = colReqistionNo
        repoRequition.ReadOnly = True
        repoRequition.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition) '138

        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Serialize Item"
        repoIsSerItem.Name = colIsSerialseItem
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSerItem)

        Dim repoIsPickAutoSerNo As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsPickAutoSerNo.HeaderText = "Is Pick Auto Serial"
        repoIsPickAutoSerNo.Name = colIsPickAutoSrNo
        repoIsPickAutoSerNo.ReadOnly = True
        repoIsPickAutoSerNo.IsVisible = False
        repoIsPickAutoSerNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsPickAutoSerNo) '140

        '' for abatenment SRN
        Dim repoAbatementRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAbatementRate.WrapText = True
        'repoAbatementRate.ReadOnly = True
        repoAbatementRate.FormatString = ""
        repoAbatementRate.Width = 100
        repoAbatementRate.HeaderText = "Abatement Rate"
        repoAbatementRate.Name = colAbatementRate
        repoAbatementRate.IsVisible = IsAbatementPO
        repoAbatementRate.Minimum = 0
        repoAbatementRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAbatementRate) '141

        Dim repoAssesableMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssesableMRP.WrapText = True
        repoAssesableMRP.ReadOnly = True
        repoAssesableMRP.FormatString = ""
        repoAssesableMRP.Width = 150
        repoAssesableMRP.HeaderText = "Assessable MRP"
        repoAssesableMRP.Name = colAssesableMRP
        repoAssesableMRP.IsVisible = IsAbatementPO
        repoAssesableMRP.Minimum = 0
        repoAssesableMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssesableMRP)

        Dim repoTotalAssesableMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalAssesableMRP.WrapText = True
        repoTotalAssesableMRP.ReadOnly = True
        repoTotalAssesableMRP.FormatString = ""
        repoTotalAssesableMRP.Width = 150
        repoTotalAssesableMRP.HeaderText = "Total Assessable MRP"
        repoTotalAssesableMRP.Name = colTotalAssesableMRP
        repoTotalAssesableMRP.IsVisible = IsAbatementPO
        repoTotalAssesableMRP.Minimum = 0
        repoTotalAssesableMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotalAssesableMRP)
        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Show"
        ShowBtn.HeaderText = "Image"
        ShowBtn.Name = "Image"
        ShowBtn.FieldName = "Image"
        ShowBtn.Width = 70
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(ShowBtn)

        '' Anubhooti 21-Oct-2014
        Dim repoMRNUCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRNUCost = New GridViewDecimalColumn()
        repoMRNUCost.FormatString = ""
        repoMRNUCost.HeaderText = "MRN Unit Cost"
        repoMRNUCost.Name = colMRNUnitCost
        repoMRNUCost.Width = 80
        repoMRNUCost.Minimum = 0
        repoMRNUCost.FormatString = "{0:n4}"
        repoMRNUCost.DecimalPlaces = 4
        repoMRNUCost.IsVisible = False
        repoMRNUCost.ReadOnly = True
        repoMRNUCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRNUCost)
        ''
        '' Anubhooti 09-Dec-2014 (RGP Item Code)
        Dim repoRGPICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRGPICode.FormatString = ""
        repoRGPICode.HeaderText = "RGP Item Code"
        repoRGPICode.Name = colRGPICode
        repoRGPICode.Width = 180
        repoRGPICode.ReadOnly = True
        repoRGPICode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoRGPICode)

        ''''''
        repoOrgPOQty = New GridViewDecimalColumn()
        repoOrgPOQty.FormatString = ""
        repoOrgPOQty.WrapText = True
        repoOrgPOQty.HeaderText = "Accepted Amount"
        repoOrgPOQty.Name = colAcceptedAmount
        repoOrgPOQty.Width = 80
        repoOrgPOQty.Minimum = 0
        repoOrgPOQty.ReadOnly = True
        repoOrgPOQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgPOQty)

        repoOrgPOQty = New GridViewDecimalColumn()
        repoOrgPOQty.FormatString = ""
        repoOrgPOQty.WrapText = True
        repoOrgPOQty.HeaderText = "Rejected Amount"
        repoOrgPOQty.Name = colRejectedAmount
        repoOrgPOQty.Width = 80
        repoOrgPOQty.Minimum = 0
        repoOrgPOQty.ReadOnly = True
        repoOrgPOQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgPOQty)

        repoOrgPOQty = New GridViewDecimalColumn()
        repoOrgPOQty.FormatString = ""
        repoOrgPOQty.WrapText = True
        repoOrgPOQty.HeaderText = "Shortage Amount"
        repoOrgPOQty.Name = colShortageAmount
        repoOrgPOQty.Width = 80
        repoOrgPOQty.Minimum = 0
        repoOrgPOQty.ReadOnly = True
        repoOrgPOQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgPOQty)

        repoOrgPOQty = New GridViewDecimalColumn()
        repoOrgPOQty.FormatString = ""
        repoOrgPOQty.WrapText = True
        repoOrgPOQty.HeaderText = "Leak Amount"
        repoOrgPOQty.Name = colLeakAmount
        repoOrgPOQty.Width = 80
        repoOrgPOQty.Minimum = 0
        repoOrgPOQty.ReadOnly = True
        repoOrgPOQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgPOQty)

        repoOrgPOQty = New GridViewDecimalColumn()
        repoOrgPOQty.FormatString = ""
        repoOrgPOQty.WrapText = True
        repoOrgPOQty.HeaderText = "Burst Amount"
        repoOrgPOQty.Name = colBurstAmount
        repoOrgPOQty.Width = 80
        repoOrgPOQty.Minimum = 0
        repoOrgPOQty.ReadOnly = True
        repoOrgPOQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgPOQty)

        repoOrgPOQty = New GridViewDecimalColumn()
        repoOrgPOQty.FormatString = ""
        repoOrgPOQty.WrapText = True
        repoOrgPOQty.HeaderText = "Amt Less Discount Without Shortage"
        repoOrgPOQty.Name = colAmtLessDiscountWithoutShortage
        repoOrgPOQty.Width = 80
        repoOrgPOQty.Minimum = 0
        repoOrgPOQty.ReadOnly = True
        repoOrgPOQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgPOQty)

        ''============19/10/2016--------------additional charge columns============================
        Dim repoWeightUOMMT As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code1"
        repoWeightUOMMT.Name = colItemACCode1
        repoWeightUOMMT.Width = 150
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        Dim repoItemWeightMT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Org Amt1"
        repoItemWeightMT.Name = colItemACAmount1
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt1"
        repoItemWeightMT.Name = colItemACCalcAmount1
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code2"
        repoWeightUOMMT.Name = colItemACCode2
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt2"
        repoItemWeightMT.Name = colItemACAmount2
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt2"
        repoItemWeightMT.Name = colItemACCalcAmount2
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code3"
        repoWeightUOMMT.Name = colItemACCode3
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt3"
        repoItemWeightMT.Name = colItemACAmount3
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt3"
        repoItemWeightMT.Name = colItemACCalcAmount3
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code4"
        repoWeightUOMMT.Name = colItemACCode4
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt4"
        repoItemWeightMT.Name = colItemACAmount4
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt4"
        repoItemWeightMT.Name = colItemACCalcAmount4
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code5"
        repoWeightUOMMT.Name = colItemACCode5
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt5"
        repoItemWeightMT.Name = colItemACAmount5
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt5"
        repoItemWeightMT.Name = colItemACCalcAmount5
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code6"
        repoWeightUOMMT.Name = colItemACCode6
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt6"
        repoItemWeightMT.Name = colItemACAmount6
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt6"
        repoItemWeightMT.Name = colItemACCalcAmount6
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code7"
        repoWeightUOMMT.Name = colItemACCode7
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt7"
        repoItemWeightMT.Name = colItemACAmount7
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt7"
        repoItemWeightMT.Name = colItemACCalcAmount7
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code8"
        repoWeightUOMMT.Name = colItemACCode8
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt8"
        repoItemWeightMT.Name = colItemACAmount8
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt8"
        repoItemWeightMT.Name = colItemACCalcAmount8
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code9"
        repoWeightUOMMT.Name = colItemACCode9
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt9"
        repoItemWeightMT.Name = colItemACAmount9
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt9"
        repoItemWeightMT.Name = colItemACCalcAmount9
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code10"
        repoWeightUOMMT.Name = colItemACCode10
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt10"
        repoItemWeightMT.Name = colItemACAmount10
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt10"
        repoItemWeightMT.Name = colItemACCalcAmount10
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Total ItemAdditional Amt"
        repoItemWeightMT.Name = colItemTotalAdditionalCharge
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)
        ''==============================================================================================
        ''done by stuti on 20/10/2016 against purchase points
        Dim repoCategoryType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoCategoryType.FormatString = ""
        repoCategoryType.HeaderText = "Category Type"
        repoCategoryType.Name = colCategoryType
        repoCategoryType.Width = 50
        repoCategoryType.IsVisible = ShowCapexCodeandSubCode
        repoCategoryType.VisibleInColumnChooser = ShowCapexCodeandSubCode
        repoCategoryType.DataSource = Xtra.GetCapexCombo()
        repoCategoryType.ValueMember = "Code"
        repoCategoryType.DisplayMember = "Name"
        gv1.MasterTemplate.Columns.Add(repoCategoryType)

        Dim repoEmergency As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoEmergency.Checked = ToggleState.Off
        repoEmergency.HeaderText = "Emergency"
        repoEmergency.Name = colEmergency
        repoEmergency.Width = 50
        repoEmergency.IsVisible = ShowCapexCodeandSubCode
        repoEmergency.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoEmergency)

        Dim repoCapexSubCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCapexSubCode.FormatString = ""
        repoCapexSubCode.HeaderText = "Capex Sub Code"
        repoCapexSubCode.Name = colCapexSubCode
        repoCapexSubCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCapexSubCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCapexSubCode.Width = 100
        repoCapexSubCode.IsVisible = ShowCapexCodeandSubCode
        repoCapexSubCode.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoCapexSubCode)

        Dim repoCapexCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCapexCode.FormatString = ""
        repoCapexCode.HeaderText = "Capex Code"
        repoCapexCode.Name = colCapexCode
        repoCapexCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCapexCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCapexCode.Width = 100
        repoCapexCode.IsVisible = ShowCapexCodeandSubCode
        repoCapexCode.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoCapexCode)

        Dim RackBin As String = clsFixedParameter.GetData(clsFixedParameterType.EnableRackBin, clsFixedParameterCode.EnableRackBin, Nothing)
        If EnableRackBin = "1" Then
            Dim repoRack As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoRack.FormatString = ""
            repoRack.HeaderText = "Rack No"
            repoRack.Name = colRack
            repoRack.ReadOnly = True
            repoRack.Width = 100
            gv1.MasterTemplate.Columns.Add(repoRack)

            Dim repoBin As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoBin.FormatString = ""
            repoBin.HeaderText = "Bin No"
            repoBin.Name = colBin
            repoBin.ReadOnly = True
            repoBin.Width = 100
            gv1.MasterTemplate.Columns.Add(repoBin)
        End If

        repoOrgPOQty = Nothing

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        'gv1.AutoSizeRows = True'=============BM00000003831
        ReStoreGridLayout()

        gv1.Rows.AddNew()
        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
        gv1.Rows(0).Cells(colRowType).Value = clsItemRowType.RowTypeItem
    End Sub
    Sub LoadBlankGridAC()
        gvAC.Rows.Clear()
        gvAC.Columns.Clear()

        Dim repoACCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACCode.FormatString = ""
        repoACCode.HeaderText = "Addition Charges Code"
        repoACCode.Name = colACCode
        repoACCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoACCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoACCode.Width = 150
        repoACCode.ReadOnly = False
        gvAC.MasterTemplate.Columns.Add(repoACCode)

        Dim repoACName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACName.FormatString = ""
        repoACName.HeaderText = "Addition Charges Description"
        repoACName.Name = colACName
        repoACName.Width = 300
        repoACName.ReadOnly = True
        gvAC.MasterTemplate.Columns.Add(repoACName)

        Dim repoACAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoACAmt.FormatString = ""
        repoACAmt.HeaderText = "Amount"
        repoACAmt.Name = colACAmount
        repoACAmt.Width = 100
        repoACAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoACAmt.ReadOnly = False
        gvAC.MasterTemplate.Columns.Add(repoACAmt)

        gvAC.AllowAddNewRow = False
        gvAC.ShowGroupPanel = False
        gvAC.AllowColumnReorder = True
        gvAC.AllowRowReorder = False
        gvAC.EnableSorting = False
        gvAC.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvAC.MasterTemplate.ShowRowHeaderColumn = False
        gvAC.TableElement.TableHeaderHeight = 40

        gvAC.Rows.AddNew()
    End Sub
    Sub LoadBlankGridTax()
        gv2.Rows.Clear()
        gv2.Columns.Clear()

        Dim repoTaxAuthCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthCode.FormatString = ""
        repoTaxAuthCode.HeaderText = "Tax Authority Code"
        repoTaxAuthCode.Name = colTTaxAutCode
        repoTaxAuthCode.Width = 150
        repoTaxAuthCode.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoTaxAuthCode)

        Dim repoTaxAuthName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthName.FormatString = ""
        repoTaxAuthName.HeaderText = "Tax Authority"
        repoTaxAuthName.Name = colTTaxAutName
        repoTaxAuthName.Width = 200
        repoTaxAuthName.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoTaxAuthName)

        Dim repoTaxAssessableAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAssessableAmt.FormatString = ""
        repoTaxAssessableAmt.HeaderText = "Assessable Amount"
        repoTaxAssessableAmt.Name = colTTaxAssessableAmt
        repoTaxAssessableAmt.Width = 100
        repoTaxAssessableAmt.ReadOnly = True
        repoTaxAssessableAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxAssessableAmt)

        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Base Amount"
        repoTaxBaseAmt.Name = colTBaseAmt
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        Dim repoTaxRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate.FormatString = ""
        repoTaxRate.HeaderText = "Tax Rate"
        repoTaxRate.Name = colTTaxRate
        repoTaxRate.Width = 100
        repoTaxRate.ReadOnly = False
        repoTaxRate.IsVisible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowTaxRateColumnOnTransaction, clsFixedParameterCode.ShowTaxRateColumnOnTransaction, Nothing)) = 1, True, False)
        repoTaxRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxRate)

        Dim repoTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt.FormatString = ""
        repoTaxAmt.HeaderText = "Tax Amount"
        repoTaxAmt.Name = colTTaxAmt
        repoTaxAmt.Width = 100
        repoTaxAmt.ReadOnly = False
        repoTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxAmt)

        gv1.AllowDeleteRow = True
        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        gv2.Rows.AddNew()
    End Sub
    Private Sub gv1_CellClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellClick
        Try
            If e.Column Is gv1.Columns("Image") And Not IsNothing(gv1.CurrentRow.Cells(colICode).Value) Then
                Dim objImage As New frmPicture
                If objImage.GetImage("tspl_Item_master", "Item_Image", "Item_Code", clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) Then
                    objImage.ShowDialog()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If e.Column Is gv1.Columns(colTotTaxAmt) Then
                        UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                        UpdateAllTotals()
                    End If

                    If e.Column Is gv1.Columns(colRate) Then
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowLargerItemCostThenVendorItemCost, clsFixedParameterCode.AllowLargerItemCostThenVendorItemCost, Nothing)) = 0 And clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchasePickItemFromVendorItemDetails, clsFixedParameterCode.PurchasePickItemFromVendorItemDetails, Nothing)) = 1 Then
                            Dim strCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                            Dim cellPrice As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value)
                            Dim vendorPrice As Double = clsDBFuncationality.getSingleValue("select item_rate from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' and item_no='" & strCode & "'")
                            If cellPrice > vendorPrice Then
                                clsCommon.MyMessageBoxShow(Me, "The Larger Price Of Item is not Allowed then the Vendor Item Price ", Me.Text)
                                gv1.CurrentRow.Cells(colRate).Value = vendorPrice
                            End If

                        End If
                    End If
                    If e.Column Is gv1.Columns(colItemInsuranceAmt) OrElse e.Column Is gv1.Columns(colItemInsurancePer) OrElse e.Column Is gv1.Columns(colInsurancePer) OrElse e.Column Is gv1.Columns(colCategoryType) OrElse e.Column Is gv1.Columns(colCapexSubCode) OrElse e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colAmt) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colLeakQty) OrElse e.Column Is gv1.Columns(colBurstQty) OrElse e.Column Is gv1.Columns(colShortQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colSpecification) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colDisPer) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colBatchNo) OrElse e.Column Is gv1.Columns(colExpiry) OrElse e.Column Is gv1.Columns(colManufactureDate) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colOrgMRNQty) OrElse (e.Column Is gv1.Columns(colAmt) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal) Then
                        If (e.Column Is gv1.Columns(colItemInsuranceAmt) OrElse e.Column Is gv1.Columns(colItemInsurancePer) OrElse e.Column Is gv1.Columns(colInsurancePer) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colLeakQty) OrElse e.Column Is gv1.Columns(colBurstQty) OrElse e.Column Is gv1.Columns(colShortQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colDisPer) OrElse e.Column Is gv1.Columns(colOrgMRNQty) OrElse (e.Column Is gv1.Columns(colAmt) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal)) Then
                            If ((e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colLeakQty) OrElse e.Column Is gv1.Columns(colBurstQty) OrElse e.Column Is gv1.Columns(colOrgMRNQty) OrElse e.Column Is gv1.Columns(colShortQty))) Then
                                If e.Column Is gv1.Columns(colOrgMRNQty) AndAlso (clsCommon.myLen(txtPONo.Value) > 0) Then
                                    If clsCommon.myCdbl(gv1.CurrentRow.Cells(colOrgGRNQty).Value) > 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colOrgMRNQty).Value) > clsCommon.myCdbl(gv1.CurrentRow.Cells(colOrgGRNQty).Value) Then
                                        clsCommon.MyMessageBoxShow("Received Qty can't be more than Challan Qty[" + clsCommon.myCstr(gv1.CurrentRow.Cells(colOrgGRNQty).Value) + "]")
                                        gv1.CurrentRow.Cells(colOrgMRNQty).Value = 0
                                    End If
                                End If
                                gv1.CurrentRow.Cells(colRejectedQty).Value = 0
                                Dim dblPendingQty As Double = 0
                                If (clsCommon.myLen(gv1.CurrentRow.Cells(colPONo).Value) > 0 OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colRGPNo).Value) > 0) Then 'OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colMRN_NO).Value) > 0) Then
                                    dblPendingQty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPendingQty).Value)
                                Else
                                    dblPendingQty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colOrgMRNQty).Value)
                                End If

                                If (clsCommon.myLen(gv1.CurrentRow.Cells(colPONo).Value) > 0 OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colRGPNo).Value) > 0 OrElse clsCommon.myCdbl(gv1.CurrentRow.Cells(colOrgMRNQty).Value) > 0) Then
                                    Dim dblEnteredQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                                    Dim dblRejected As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRejectedQty).Value)
                                    Dim dblDamageQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colShortQty).Value)

                                    '====================when rgp after job-work and against bom thencheck balance of raw-material=====
                                    If clsCommon.myLen(gv1.CurrentRow.Cells(colRGPNo).Value) > 0 AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "AB") = CompairStringResult.Equal AndAlso isRGPAfterPO AndAlso Not is_Load_MRN Then
                                        Dim strMsg As String = clsRGPHead.GetRGPTypeItemBalance(cmbRGPType.SelectedValue, dblEnteredQty, txtVendorNo.Value, Nothing, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), txtDate.Value, txtDocNo.Value, True)
                                        If clsCommon.myLen(strMsg) > 0 Then
                                            gv1.CurrentCell.Value = 0
                                            Throw New Exception(strMsg)
                                        End If
                                        isCellValueChangedOpen = False
                                        Exit Sub
                                    End If
                                    '============================================================================================================

                                    'If (dblEnteredQty + dblDamageQty) > dblPendingQty Then
                                    '    common.clsCommon.MyMessageBoxShow("Entered Quantity Can't be more than Pending Quantity." + Environment.NewLine + "Entered Quantity : " + clsCommon.myCstr(dblEnteredQty) + ". Pending Quantity : " + clsCommon.myCstr(dblPendingQty) + ". Damage Quantity : " + clsCommon.myCstr(dblDamageQty))
                                    '    gv1.CurrentCell.Value = 0
                                    '    ''gv1.CurrentRow.Cells(colQty).Value = dblPendingQty
                                    'ElseIf (dblEnteredQty + dblDamageQty) < dblPendingQty Then
                                    '    If clsCommon.myLen(gv1.CurrentRow.Cells(colRGPNo).Value) > 0 Then
                                    '        gv1.CurrentRow.Cells(colRejectedQty).Value = 0
                                    '    Else
                                    '        gv1.CurrentRow.Cells(colRejectedQty).Value = dblPendingQty - (dblEnteredQty + dblDamageQty)
                                    '    End If
                                    'End If
                                End If
                            End If
                            If (e.Column Is gv1.Columns(colQty) AndAlso Not clsCommon.myCBool(gv1.CurrentRow.Cells(colIsPickAutoSrNo).Value)) Then
                                OpenSerialItem()
                            End If

                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            If rbtnTaxCalManual.IsChecked Then
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRow(ii)
                                Next
                            End If
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            If rbtnTaxCalManual.IsChecked Then
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRow(ii)
                                Next
                            End If
                            UpdateAllTotals()

                        ElseIf e.Column Is gv1.Columns(colCategoryType) Then
                            If clsCommon.CompairString(gv1.CurrentRow.Cells(colCategoryType).Value, "Capex") = CompairStringResult.Equal Then
                                gv1.CurrentRow.Cells(colCapexSubCode).ReadOnly = False
                                gv1.CurrentRow.Cells(colCapexCode).ReadOnly = False
                            Else
                                gv1.CurrentRow.Cells(colCapexSubCode).ReadOnly = True
                                gv1.CurrentRow.Cells(colCapexCode).ReadOnly = True
                                gv1.CurrentRow.Cells(colCapexSubCode).Value = ""
                                gv1.CurrentRow.Cells(colCapexCode).Value = ""
                            End If
                        ElseIf e.Column Is gv1.Columns(colCapexSubCode) Then
                            If clsCommon.CompairString(gv1.CurrentRow.Cells(colCategoryType).Value, "Capex") = CompairStringResult.Equal Then
                                OpenCapexSubCodeList()
                            End If
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            If rbtnTaxCalManual.IsChecked Then
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRow(ii)
                                Next
                            End If
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colMRP) Then
                            Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                            Dim Rateqry As String = "select Item_Rate,MRP from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" + txtVendorNo.Value + "' and item_no='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "' and UOM='FC' and MRP='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colMRP).Value) + "' "
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Rateqry)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                Dim VendrItemRate As Double = clsCommon.myCdbl(dt.Rows(0)("Item_Rate"))
                                Dim conversionFact As Double = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(strICode, gv1.CurrentRow.Cells(colUnit).Value, Nothing))
                                If VendrItemRate <> 0 Then
                                    Dim Itemrate As Double = clsCommon.myCdbl(clsCommon.myCdbl(VendrItemRate) / clsCommon.myCdbl(conversionFact))
                                    gv1.CurrentRow.Cells(colRate).Value = Math.Round(Itemrate, 2)
                                End If
                                UpdateCurrentRow(gv1.CurrentRow.Index)
                                If rbtnTaxCalManual.IsChecked Then
                                    For ii As Integer = 0 To gv1.Rows.Count - 1
                                        UpdateCurrentRow(ii)
                                    Next
                                End If
                                UpdateAllTotals()
                            End If
                        ElseIf e.Column Is gv1.Columns(colAmt) Then
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            If rbtnTaxCalManual.IsChecked Then
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRow(ii)
                                Next
                            End If
                            UpdateAllTotals()
                        End If
                    End If
                    isCellValueChangedOpen = False

                    If clsCommon.myLen(txtPONo.Value) > 0 Then
                        gv1.Columns(colDisAmt).ReadOnly = True
                        gv1.Columns(colDisPer).ReadOnly = True
                    End If

                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub
    Sub OpenCapexSubCodeList()
        Try
            gv1.CurrentRow.Cells(colCapexSubCode).Value = clsCapexBudget.getFinder("", gv1.CurrentRow.Cells(colCapexSubCode).Value, False)
            If clsCommon.myLen(gv1.CurrentRow.Cells(colCapexSubCode).Value) > 0 Then
                gv1.CurrentRow.Cells(colCapexCode).Value = clsCapexBudget.GetCapexCode(gv1.CurrentRow.Cells(colCapexSubCode).Value, Nothing)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub OpenSerialItem()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSerialseItem).Value) Then
            Dim Item_type As String = clsDBFuncationality.getSingleValue("select Item_Type from TSPL_ITEM_MASTER where Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "'")
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colRGPNo).Value)) > 0 Then
                Dim frm As frmSerializeItemOut = New frmSerializeItemOut()
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)

                frm.strAgaintsDocNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colRGPNo).Value)
                frm.strLocationCode = txtBillToLocation.Value
                frm.strCurrDocNo = txtDocNo.Value
                frm.strCurrDocType = "SRN"
                frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                frm.strItemType = Item_type
                frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Tag = frm.arr
                End If
            Else
                Dim frm As FrmSerializeItemIn = New FrmSerializeItemIn()
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.strBinNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colBinNo).Value)
                frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
                frm.ShowDialog()
                gv1.CurrentRow.Cells(colQty).Value = frm.AcceptedQty
                gv1.CurrentRow.Cells(colRejectedQty).Value = frm.RejectedQty
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Tag = frm.arr
                End If
            End If
        End If
    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
            If isItemfromVendorItemDetails Then
                gv1.CurrentRow.Cells(colRate).Value = clsVendorItemDetail.GetRate(txtVendorNo.Value, strICode, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value))
            Else
                SetVendorItemDetails()
            End If
        End If
        SetItemFatherDetail()
    End Sub
    Private Sub SetVendorItemDetails()
        If clsCommon.myLen(gv1.CurrentRow.Cells(colUnit).Value) > 0 Then
            Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
            Dim Itemrate As Double
            Dim VendrItemRate As Double
            Dim objVItem As clsVendorItemDetail = clsVendorItemDetail.GetItemRateAndMRP(txtVendorNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
            If objVItem IsNot Nothing Then
                gv1.CurrentRow.Cells(colRate).Value = objVItem.item_rate
                gv1.CurrentRow.Cells(colMRP).Value = objVItem.MRP
            Else
                Dim Rateqry As String = "select Item_Rate,MRP from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" + txtVendorNo.Value + "' and item_no='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "' and UOM='FC' "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Rateqry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    VendrItemRate = clsCommon.myCdbl(dt.Rows(0)("Item_Rate"))
                    Dim conversionFact As Double = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(strICode, gv1.CurrentRow.Cells(colUnit).Value, Nothing))
                    If VendrItemRate <> 0 Then
                        Itemrate = clsCommon.myCdbl(clsCommon.myCdbl(VendrItemRate) / clsCommon.myCdbl(conversionFact))
                        gv1.CurrentRow.Cells(colRate).Value = Math.Round(Itemrate, 2)
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub setGridFocus()
        Try
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
            ''If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            ''    If gv1.CurrentColumn Is gv1.Columns(colICode) Then
            ''        gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        gv1.CurrentColumn = gv1.Columns(colQty)
            ''    ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
            ''        gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        gv1.CurrentColumn = gv1.Columns(colLeakQty)
            ''    ElseIf gv1.CurrentColumn Is gv1.Columns(colLeakQty) Then
            ''        gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        gv1.CurrentColumn = gv1.Columns(colBurstQty)
            ''    ElseIf gv1.CurrentColumn Is gv1.Columns(colBurstQty) Then
            ''        gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        gv1.CurrentColumn = gv1.Columns(colShortQty)
            ''    ElseIf gv1.CurrentColumn Is gv1.Columns(colShortQty) Then
            ''        gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        gv1.CurrentColumn = gv1.Columns(colRate)
            ''    ElseIf gv1.CurrentColumn Is gv1.Columns(colRate) Then
            ''        gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        gv1.CurrentColumn = gv1.Columns(colDisPer)
            ''    ElseIf gv1.CurrentColumn Is gv1.Columns(colDisPer) Then
            ''        gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
            ''        gv1.CurrentColumn = gv1.Columns(colICode)
            ''    End If
            ''End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)

        If IsfromRGP Then
            Exit Sub
        End If

        Dim strItemType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value)
        If clsCommon.myLen(strItemType) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Row Type", Me.Text)
            Exit Sub
        End If

        '================================================================================================================================
        'If isRGPAfterPO AndAlso clsCommon.CompairString(cboSRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "") = CompairStringResult.Equal Then
        '    clsCommon.MyMessageBoxShow("Select rgp type for job-work transaction.")
        '    RadPageView1.SelectedPage = RadPageViewPage1
        '    cmbRGPType.Select()
        '    cmbRGPType.Focus()
        '    Exit Sub
        'End If

        '==============================End here==================================================================================================

        If clsCommon.CompairString(strItemType, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
            If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Item Type", Me.Text)
                SetBlankOfItemColumns()
                cboItemType.Focus()
                Exit Sub
            End If
            If isItemfromVendorItemDetails Then
                If clsCommon.myLen(txtVendorNo.Value) > 0 AndAlso clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Select From Location First", Me.Text)
                    txtBillToLocation.Focus()
                    txtBillToLocation.Select()
                    Return
                End If

                Dim obj As clsVendorItemDetail = clsVendorItemDetail.Finder(txtVendorNo.Value, txtDate.Value, txtBillToLocation.Value, False, cboItemType.SelectedValue)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.item_code) > 0 Then
                    gv1.CurrentRow.Cells(colICode).Value = obj.item_code
                    gv1.CurrentRow.Cells(colIName).Value = obj.item_desc
                    gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.item_code, Nothing)
                    gv1.CurrentRow.Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.item_code, Nothing)
                    gv1.CurrentRow.Cells(colUnit).Value = obj.UOM
                    gv1.CurrentRow.Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(obj.item_code)
                    gv1.CurrentRow.Cells(colMRP).Value = obj.MRP
                    gv1.CurrentRow.Cells(colRate).Value = obj.item_rate
                    gv1.CurrentRow.Cells(colBinNo).Value = obj.Bin_No

                    gv1.CurrentRow.Cells(colUOMWEIGHT).Value = clsCommon.myCstr(obj.UOMWeight)
                    gv1.CurrentRow.Cells(colUOMWeightValue).Value = clsCommon.myCdbl(obj.UOMWeightValue)

                    '' add function related to Rack/Bin on 21/11/2017 by Parteek
                    If EnableRackBin Then
                        gv1.CurrentRow.Cells(colRack).Value = clsDBFuncationality.getSingleValue("select Rack_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & obj.item_code & "' and Location='" & txtBillToLocation.Value & "'")
                        gv1.CurrentRow.Cells(colBin).Value = clsDBFuncationality.getSingleValue("select Bin_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & obj.item_code & "' and Location='" & txtBillToLocation.Value & "'")
                    End If

                    '' End function

                Else
                    SetBlankOfItemColumns()
                End If
                Dim objVItem As clsVendorItemDetail = clsVendorItemDetail.GetItemRateAndMRP(txtVendorNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
                If objVItem IsNot Nothing Then
                    gv1.CurrentRow.Cells(colRate).Value = objVItem.item_rate
                    gv1.CurrentRow.Cells(colMRP).Value = objVItem.MRP
                    If IsAbatementPO Then
                        gv1.CurrentRow.Cells(colAbatementRate).Value = objVItem.AbatementRate
                        gv1.CurrentRow.Cells(colAssesableMRP).Value = gv1.CurrentRow.Cells(colMRP).Value - (gv1.CurrentRow.Cells(colMRP).Value * gv1.CurrentRow.Cells(colAbatementRate).Value / 100)
                        gv1.CurrentRow.Cells(colTotalAssesableMRP).Value = gv1.CurrentRow.Cells(colQty).Value * gv1.CurrentRow.Cells(colAssesableMRP).Value
                    End If
                Else
                End If
                ''End If
            Else
                'If isRGPAfterPO AndAlso clsCommon.CompairString(cboSRNType.SelectedValue, "J") = CompairStringResult.Equal Then
                '    Dim obj As clsRGPDetail = clsRGPHead.GetRGPTypeItemFInder(cmbRGPType.SelectedValue, txtVendorNo.Value, txtDocNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(cboItemType.SelectedValue))
                '    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                '        gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
                '        gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
                '        gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                '        gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_code
                '        gv1.CurrentRow.Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value))
                '        gv1.CurrentRow.Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(obj.Item_Code)
                '        gv1.CurrentRow.Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(obj.Item_Code)
                '        gv1.CurrentRow.Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(obj.Item_Code, Nothing)
                '        gv1.CurrentRow.Cells(colRGPNo).Value = obj.RGP_No
                '        gv1.CurrentRow.Cells(colQty).Value = obj.RGP_Qty
                '        gv1.CurrentRow.Cells(colPendingQty).Value = obj.RGP_Qty
                '        gv1.CurrentRow.Cells(colUOMWEIGHT).Value = clsItemMaster.GetItemWeightUnit(obj.Item_Code, Nothing)
                '        gv1.CurrentRow.Cells(colUOMWeightValue).Value = clsItemMaster.GetItemWeightValue(obj.Item_Code, Nothing)
                '        SetItemFatherDetail()
                '    Else
                '        SetBlankOfItemColumns()
                '    End If
                'Else
                Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(cboItemType.SelectedValue), True, isButtonClick, txtVendorNo.Value, "")
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                    gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
                    gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
                    gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    gv1.CurrentRow.Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                    gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
                    gv1.CurrentRow.Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value))
                    gv1.CurrentRow.Cells(colIsSerialseItem).Value = obj.Is_Serial_Item
                    gv1.CurrentRow.Cells(colIsPickAutoSrNo).Value = obj.Is_Pick_Auto_SrNo
                    gv1.CurrentRow.Cells(colisMRPMandatory).Value = obj.Is_MRP
                    gv1.CurrentRow.Cells(colBinNo).Value = obj.Rack_No
                    gv1.CurrentRow.Cells(colUOMWEIGHT).Value = clsCommon.myCstr(obj.Weight_UOM)
                    gv1.CurrentRow.Cells(colUOMWeightValue).Value = clsCommon.myCdbl(obj.Weight_Value)
                    SetItemFatherDetail()
                    '' add function related to Rack/Bin on 21/11/2017 by Parteek
                    If EnableRackBin Then
                        gv1.CurrentRow.Cells(colRack).Value = clsDBFuncationality.getSingleValue("select Rack_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & obj.Item_Code & "' and Location='" & txtBillToLocation.Value & "'")
                        gv1.CurrentRow.Cells(colBin).Value = clsDBFuncationality.getSingleValue("select Bin_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & obj.Item_Code & "' and Location='" & txtBillToLocation.Value & "'")
                    End If

                    '' End function
                Else
                    SetBlankOfItemColumns()
                End If
                'End If


                ''End If




            End If
        Else

            ''For Open Misc Charges 
            Dim obj As clsAdditionalCharge = clsAdditionalCharge.GetFinder(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), isButtonClick)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                gv1.CurrentRow.Cells(colICode).Value = obj.Code
                gv1.CurrentRow.Cells(colIName).Value = obj.desc
                gv1.CurrentRow.Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(obj.Code, Nothing)
                gv1.CurrentRow.Cells(colIsInsurance).Value = obj.Is_Insurance
                gv1.CurrentRow.Cells(colItemTaxable).Value = False
                gv1.CurrentRow.Cells(colUnit).Value = Nothing
                gv1.CurrentRow.Cells(colQty).Value = Nothing
                gv1.CurrentRow.Cells(colRate).Value = Nothing
            Else
                SetBlankOfItemColumns()
            End If
            ''End of Misc Charges 
        End If
        SetitemWiseTaxSetting(True, True)
        setBalance()
        'SetVendorItemCostDetail()
    End Sub
    Private Sub SetItemFatherDetail()
        gv1.CurrentRow.Cells(colFCode).Value = ""
        gv1.CurrentRow.Cells(colFRate).Value = 0
        gv1.CurrentRow.Cells(colFAmt).Value = 0
        If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "F") = CompairStringResult.Equal Then
            Dim strFCode As String = clsItemMaster.GetFatherCode(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing)
            gv1.CurrentRow.Cells(colFCode).Value = strFCode
            If clsCommon.myLen(strFCode) > 0 Then
                gv1.CurrentRow.Cells(colFRate).Value = clsItemPriceMaster.GetMRPOfFinishItem(strFCode, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
            End If
        End If
    End Sub
    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colHSNNo).Value = ""
        gv1.CurrentRow.Cells(colItemTaxable).Value = False
        gv1.CurrentRow.Cells(colUnit).Value = ""
        gv1.CurrentRow.Cells(colMRP).Value = 0
        gv1.CurrentRow.Cells(colUOMWEIGHT).Value = ""
        gv1.CurrentRow.Cells(colUOMWeightValue).Value = 0
        gv1.CurrentRow.Cells(colisMRPMandatory).Value = False
        gv1.CurrentRow.Cells(colRGPNo).Value = Nothing
        gv1.CurrentRow.Cells(colQty).Value = Nothing
        gv1.CurrentRow.Cells(colPendingQty).Value = Nothing
        gv1.CurrentRow.Cells(colFCode).Value = ""
        gv1.CurrentRow.Cells(colFRate).Value = 0
        gv1.CurrentRow.Cells(colFAmt).Value = 0
        ''gv1.CurrentRow.Cells(colAssessableRate).Value = 0
    End Sub
    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub
    Private Function GetCurrentRowTotalTaxAmt(ByVal IntRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            Else
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            End If
        Next
        Return dblTotTax
    End Function
    Private Function GetCurrentRowSurTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 1 To intEndCol
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            Else
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            End If
        Next
        Return 0
    End Function
    Private Function GetCurrentRowOtherTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                Dim strii As String = clsCommon.myCstr(ii)
                If IntRowNo < 0 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                Else
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                End If
            Next
        Next
        Return dblRet
    End Function
    Private Sub BlankTaxDetails(ByVal intRowNo As Integer)
        BlankTaxDetails(intRowNo, True)
    End Sub
    Private Sub BlankTaxDetails(ByVal intRowNo As Integer, ByVal isBlankRate As Boolean)
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            If intRowNo < 0 Then
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                If isBlankRate Then
                    gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                End If
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = Nothing
            Else
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                If isBlankRate Then
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                End If
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = Nothing
            End If
        Next
    End Sub
    Public Sub UpdateAllTotals()
        Dim isInsuranceExists As Boolean = False
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(IIf(is_Load_MRN, colMRN_NO, colPONo)).Value) <= 0 Then
                        If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsInsurance).Value) Then
                            isInsuranceExists = True
                            Exit For
                        End If
                    End If
                End If
            End If
        Next

        If isInsuranceExists Then
            Dim dblTotalInsuranceBaseAmt As Decimal = 0
            For ii As Integer = 0 To gv1.RowCount - 1
                If clsCommon.myCdbl(clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                    If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colIsInsurance).Value) Then
                        dblTotalInsuranceBaseAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                    End If
                End If
            Next
            For ii As Integer = 0 To gv1.RowCount - 1
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                        If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsInsurance).Value) Then
                            If clsCommon.myLen(gv1.Rows(ii).Cells(IIf(is_Load_MRN, colMRN_NO, colPONo)).Value) <= 0 Then
                                gv1.Rows(ii).Cells(colInsuranceBaseAmt).Value = dblTotalInsuranceBaseAmt
                                UpdateCurrentRow(ii)
                            End If
                        End If
                    End If
                End If
            Next
        End If

        Dim dblTotAmt As Double = 0
        Dim dblTotDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0
        Dim dblTotLandedCost As Double = 0
        Dim dblTotalQuantity As Double = Nothing
        Dim dblTaxAssessableAmt As Double = 0
        Dim dblTotalAssesableMRP As Double = 0

        Dim dblTaxBaseAmt1 As Double = 0
        Dim dblTaxBaseAmt2 As Double = 0
        Dim dblTaxBaseAmt3 As Double = 0
        Dim dblTaxBaseAmt4 As Double = 0
        Dim dblTaxBaseAmt5 As Double = 0
        Dim dblTaxBaseAmt6 As Double = 0
        Dim dblTaxBaseAmt7 As Double = 0
        Dim dblTaxBaseAmt8 As Double = 0
        Dim dblTaxBaseAmt9 As Double = 0
        Dim dblTaxBaseAmt10 As Double = 0

        Dim dblTaxAmt1 As Double = 0
        Dim dblTaxAmt2 As Double = 0
        Dim dblTaxAmt3 As Double = 0
        Dim dblTaxAmt4 As Double = 0
        Dim dblTaxAmt5 As Double = 0
        Dim dblTaxAmt6 As Double = 0
        Dim dblTaxAmt7 As Double = 0
        Dim dblTaxAmt8 As Double = 0
        Dim dblTaxAmt9 As Double = 0
        Dim dblTaxAmt10 As Double = 0
        Dim dblItemInsuranceAmt As Decimal = 0
        Dim dblTaxableAmount As Decimal = 0


        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                dblTaxableAmount = dblTaxableAmount + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxableAmount).Value)
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisAmt).Value)
                dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)

                dblTotalQuantity = dblTotalQuantity + clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)

                dblTaxAmt1 = dblTaxAmt1 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt1).Value)
                dblTaxAmt2 = dblTaxAmt2 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt2).Value)
                dblTaxAmt3 = dblTaxAmt3 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt3).Value)
                dblTaxAmt4 = dblTaxAmt4 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt4).Value)
                dblTaxAmt5 = dblTaxAmt5 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt5).Value)
                dblTaxAmt6 = dblTaxAmt6 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt6).Value)
                dblTaxAmt7 = dblTaxAmt7 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt7).Value)
                dblTaxAmt8 = dblTaxAmt8 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt8).Value)
                dblTaxAmt9 = dblTaxAmt9 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt9).Value)
                dblTaxAmt10 = dblTaxAmt10 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt10).Value)

                dblTaxAssessableAmt = dblTaxAssessableAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAssessableAmount).Value)
                dblTotalAssesableMRP = dblTotalAssesableMRP + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalAssesableMRP).Value)

                dblTaxBaseAmt1 = dblTaxBaseAmt1 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt1).Value)
                dblTaxBaseAmt2 = dblTaxBaseAmt2 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt2).Value)
                dblTaxBaseAmt3 = dblTaxBaseAmt3 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt3).Value)
                dblTaxBaseAmt4 = dblTaxBaseAmt4 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt4).Value)
                dblTaxBaseAmt5 = dblTaxBaseAmt5 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt5).Value)
                dblTaxBaseAmt6 = dblTaxBaseAmt6 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt6).Value)
                dblTaxBaseAmt7 = dblTaxBaseAmt7 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt7).Value)
                dblTaxBaseAmt8 = dblTaxBaseAmt8 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt8).Value)
                dblTaxBaseAmt9 = dblTaxBaseAmt9 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt9).Value)
                dblTaxBaseAmt10 = dblTaxBaseAmt10 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt10).Value)

                dblItemInsuranceAmt = dblItemInsuranceAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemInsuranceAmt).Value)

                dblTaxTotAmt = dblTaxTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value)
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterTax).Value)

                dblTotLandedCost = dblTotLandedCost + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLandedAmt).Value)
            End If
        Next

        If rbtnTaxCalAutomatic.IsChecked Then
            For ii As Integer = 1 To gv2.Rows.Count
                Select Case (ii)
                    Case 1
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt1, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt1, 2)
                        If chkExciseOnQty.Checked Then
                            If dblTaxAssessableAmt <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxAssessableAmt, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        ElseIf dblTaxBaseAmt1 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = Math.Round(dblTaxAssessableAmt, 2)
                    Case 2
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                        If dblTaxBaseAmt2 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 3
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                        If dblTaxBaseAmt3 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 4
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt4, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt4, 2)
                        If dblTaxBaseAmt4 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 5
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt5, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt5, 2)
                        If dblTaxBaseAmt5 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt5 * 100) / dblTaxBaseAmt5, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 6
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt6, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt6, 2)
                        If dblTaxBaseAmt6 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt6 * 100) / dblTaxBaseAmt6, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 7
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt7, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt7, 2)
                        If dblTaxBaseAmt7 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt7 * 100) / dblTaxBaseAmt7, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 8
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt8, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt8, 2)
                        If dblTaxBaseAmt8 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt8 * 100) / dblTaxBaseAmt8, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 9
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt9, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt9, 2)
                        If dblTaxBaseAmt9 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt9 * 100) / dblTaxBaseAmt9, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 10
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt10, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt10, 2)
                        If dblTaxBaseAmt10 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt10 * 100) / dblTaxBaseAmt10, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                End Select
            Next
        Else
            For ii As Integer = 1 To gv2.Rows.Count
                gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblAmtAfterDis, 2)
            Next
        End If

        Dim dblACAmount As Double = 0
        For ii As Integer = 0 To gvAC.Rows.Count - 1
            If (clsCommon.myLen(gvAC.Rows(ii).Cells(colACCode).Value) > 0) Then
                dblACAmount = dblACAmount + clsCommon.myCdbl(gvAC.Rows(ii).Cells(colACAmount).Value)
            End If
            '' abatement PO
            If IsAbatementPO Then
                If gv1.Rows.Count > 0 Then
                    gv1.CurrentRow.Cells(colAssesableMRP).Value = gv1.CurrentRow.Cells(colMRP).Value - (gv1.CurrentRow.Cells(colMRP).Value * gv1.CurrentRow.Cells(colAbatementRate).Value / 100)
                    gv1.CurrentRow.Cells(colTotalAssesableMRP).Value = gv1.CurrentRow.Cells(colQty).Value * gv1.CurrentRow.Cells(colAssesableMRP).Value
                End If
            End If
        Next
        lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt)
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblAmtAfterDis)

        lblTotalInsuranceAmt.Text = clsCommon.myFormat(dblItemInsuranceAmt)

        lblTaxableAmount.Text = clsCommon.myFormat(dblTaxableAmount)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
        lblAddCharges.Text = clsCommon.myFormat(dblACAmount)
        lblAddCharges1.Text = clsCommon.myFormat(dblACAmount)
        dblNetAmt = dblNetAmt + dblACAmount
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        lblAmount.Text = lblTotRAmt.Text
        lblLandedCost.Text = clsCommon.myFormat(clsCommon.myCdbl(dblTotLandedCost - dblAmtAfterDis))
        lblAmtAfterLandedCost.Text = clsCommon.myFormat(dblTotLandedCost)
        Calc_AddtionalCharge_Itemwise(dblTotalQuantity)
    End Sub
    Private Sub Calc_AddtionalCharge_Itemwise(ByVal TotalQty As Double)
        Try

            Dim add_code1 As String = ""
            Dim add_amt1 As Double = Nothing
            Dim add_code2 As String = ""
            Dim add_amt2 As Double = Nothing
            Dim add_code3 As String = ""
            Dim add_amt3 As Double = Nothing
            Dim add_code4 As String = ""
            Dim add_amt4 As Double = Nothing
            Dim add_code5 As String = ""
            Dim add_amt5 As Double = Nothing
            Dim add_code6 As String = ""
            Dim add_amt6 As Double = Nothing
            Dim add_code7 As String = ""
            Dim add_amt7 As Double = Nothing
            Dim add_code8 As String = ""
            Dim add_amt8 As Double = Nothing
            Dim add_code9 As String = ""
            Dim add_amt9 As Double = Nothing
            Dim add_code10 As String = ""
            Dim add_amt10 As Double = Nothing
            ''==========================================================================================
            If gvAC.Rows.Count > 0 Then
                If gvAC.Rows.Count > 0 AndAlso clsCommon.myLen(gvAC.Rows(0).Cells(colACCode).Value) > 0 Then
                    add_code1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colACCode).Value)
                    add_amt1 = clsCommon.myCdbl(gvAC.Rows(0).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 1 AndAlso clsCommon.myLen(gvAC.Rows(1).Cells(colACCode).Value) > 0 Then
                    add_code2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colACCode).Value)
                    add_amt2 = clsCommon.myCdbl(gvAC.Rows(1).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 2 AndAlso clsCommon.myLen(gvAC.Rows(2).Cells(colACCode).Value) > 0 Then
                    add_code3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colACCode).Value)
                    add_amt3 = clsCommon.myCdbl(gvAC.Rows(2).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 3 AndAlso clsCommon.myLen(gvAC.Rows(3).Cells(colACCode).Value) > 0 Then
                    add_code4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colACCode).Value)
                    add_amt4 = clsCommon.myCdbl(gvAC.Rows(3).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 4 AndAlso clsCommon.myLen(gvAC.Rows(4).Cells(colACCode).Value) > 0 Then
                    add_code5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colACCode).Value)
                    add_amt5 = clsCommon.myCdbl(gvAC.Rows(4).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 5 AndAlso clsCommon.myLen(gvAC.Rows(5).Cells(colACCode).Value) > 0 Then
                    add_code6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colACCode).Value)
                    add_amt6 = clsCommon.myCdbl(gvAC.Rows(5).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 6 AndAlso clsCommon.myLen(gvAC.Rows(6).Cells(colACCode).Value) > 0 Then
                    add_code7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colACCode).Value)
                    add_amt7 = clsCommon.myCdbl(gvAC.Rows(6).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 7 AndAlso clsCommon.myLen(gvAC.Rows(7).Cells(colACCode).Value) > 0 Then
                    add_code8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colACCode).Value)
                    add_amt8 = clsCommon.myCdbl(gvAC.Rows(7).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 8 AndAlso clsCommon.myLen(gvAC.Rows(8).Cells(colACCode).Value) > 0 Then
                    add_code9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colACCode).Value)
                    add_amt9 = clsCommon.myCdbl(gvAC.Rows(8).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 9 AndAlso clsCommon.myLen(gvAC.Rows(9).Cells(colACCode).Value) > 0 Then
                    add_code10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colACCode).Value)
                    add_amt10 = clsCommon.myCdbl(gvAC.Rows(9).Cells(colACAmount).Value)
                End If
            End If ''additional head level grid
            ''==========================================================================================
            Dim LastIndex As Integer = 0
            Dim TotalAmt1 As Double = Nothing
            Dim TotalAmt2 As Double = Nothing
            Dim TotalAmt3 As Double = Nothing
            Dim TotalAmt4 As Double = Nothing
            Dim TotalAmt5 As Double = Nothing
            Dim TotalAmt6 As Double = Nothing
            Dim TotalAmt7 As Double = Nothing
            Dim TotalAmt8 As Double = Nothing
            Dim TotalAmt9 As Double = Nothing
            Dim TotalAmt10 As Double = Nothing
            Dim qty As Double = Nothing

            For Each grow As GridViewRowInfo In gv1.Rows
                qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                ''=======================code=============================
                grow.Cells(colItemACCode1).Value = add_code1
                grow.Cells(colItemACCode2).Value = add_code2
                grow.Cells(colItemACCode3).Value = add_code3
                grow.Cells(colItemACCode4).Value = add_code4
                grow.Cells(colItemACCode5).Value = add_code5
                grow.Cells(colItemACCode6).Value = add_code6
                grow.Cells(colItemACCode7).Value = add_code7
                grow.Cells(colItemACCode8).Value = add_code8
                grow.Cells(colItemACCode9).Value = add_code9
                grow.Cells(colItemACCode10).Value = add_code10

                grow.Cells(colItemACAmount1).Value = System.Math.Round(add_amt1, 3)
                grow.Cells(colItemACAmount2).Value = System.Math.Round(add_amt2, 3)
                grow.Cells(colItemACAmount3).Value = System.Math.Round(add_amt3, 3)
                grow.Cells(colItemACAmount4).Value = System.Math.Round(add_amt4, 3)
                grow.Cells(colItemACAmount5).Value = System.Math.Round(add_amt5, 3)
                grow.Cells(colItemACAmount6).Value = System.Math.Round(add_amt6, 3)
                grow.Cells(colItemACAmount7).Value = System.Math.Round(add_amt7, 3)
                grow.Cells(colItemACAmount8).Value = System.Math.Round(add_amt8, 3)
                grow.Cells(colItemACAmount9).Value = System.Math.Round(add_amt9, 3)
                grow.Cells(colItemACAmount10).Value = System.Math.Round(add_amt10, 3)
                ''=============amount=========================================
                If TotalQty > 0 Then
                    grow.Cells(colItemACCalcAmount1).Value = System.Math.Round((qty * add_amt1) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount2).Value = System.Math.Round((qty * add_amt2) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount3).Value = System.Math.Round((qty * add_amt3) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount4).Value = System.Math.Round((qty * add_amt4) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount5).Value = System.Math.Round((qty * add_amt5) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount6).Value = System.Math.Round((qty * add_amt6) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount7).Value = System.Math.Round((qty * add_amt7) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount8).Value = System.Math.Round((qty * add_amt8) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount9).Value = System.Math.Round((qty * add_amt9) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount10).Value = System.Math.Round((qty * add_amt10) / TotalQty, 3)

                    TotalAmt1 = System.Math.Round(TotalAmt1 + System.Math.Round((qty * add_amt1) / TotalQty, 3), 3)
                    TotalAmt2 = System.Math.Round(TotalAmt2 + System.Math.Round((qty * add_amt2) / TotalQty, 3), 3)
                    TotalAmt3 = System.Math.Round(TotalAmt3 + System.Math.Round((qty * add_amt3) / TotalQty, 3), 3)
                    TotalAmt4 = System.Math.Round(TotalAmt4 + System.Math.Round((qty * add_amt4) / TotalQty, 3), 3)
                    TotalAmt5 = System.Math.Round(TotalAmt5 + System.Math.Round((qty * add_amt5) / TotalQty, 3), 3)
                    TotalAmt6 = System.Math.Round(TotalAmt6 + System.Math.Round((qty * add_amt6) / TotalQty, 3), 3)
                    TotalAmt7 = System.Math.Round(TotalAmt7 + System.Math.Round((qty * add_amt7) / TotalQty, 3), 3)
                    TotalAmt8 = System.Math.Round(TotalAmt8 + System.Math.Round((qty * add_amt8) / TotalQty, 3), 3)
                    TotalAmt9 = System.Math.Round(TotalAmt9 + System.Math.Round((qty * add_amt9) / TotalQty, 3), 3)
                    TotalAmt10 = System.Math.Round(TotalAmt10 + System.Math.Round((qty * add_amt10) / TotalQty, 3), 3)
                End If

                grow.Cells(colItemTotalAdditionalCharge).Value = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount1).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount2).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount3).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount4).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount5).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount6).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount7).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount8).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount9).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount10).Value)
            Next

            ''================check if grid amount not equal to header amount then adjust it on last item row==============
            If gv1.Rows.Count > 0 AndAlso TotalAmt1 > add_amt1 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount1).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount1).Value) - (TotalAmt1 - add_amt1), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt1 < add_amt1 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount1).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount1).Value) + (add_amt1 - TotalAmt1), 3)
            End If
            ''2.
            If gv1.Rows.Count > 0 AndAlso TotalAmt2 > add_amt2 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount2).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount2).Value) - (TotalAmt2 - add_amt2), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt2 < add_amt2 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount2).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount2).Value) + (add_amt2 - TotalAmt2), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt3 > add_amt3 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount3).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount3).Value) - (TotalAmt3 - add_amt3), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt3 < add_amt3 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount3).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount3).Value) + (add_amt3 - TotalAmt3), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt4 > add_amt4 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount4).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount4).Value) - (TotalAmt4 - add_amt4), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt4 < add_amt4 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount4).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount4).Value) + (add_amt4 - TotalAmt4), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt5 > add_amt5 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount5).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount5).Value) - (TotalAmt5 - add_amt5), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt5 < add_amt5 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount5).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount5).Value) + (add_amt5 - TotalAmt5), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt6 > add_amt6 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount6).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount6).Value) - (TotalAmt6 - add_amt6), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt6 < add_amt6 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount6).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount6).Value) + (add_amt6 - TotalAmt6), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt7 > add_amt7 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount7).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount7).Value) - (TotalAmt7 - add_amt7), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt7 < add_amt7 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount7).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount7).Value) + (add_amt7 - TotalAmt7), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt8 > add_amt8 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount8).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount8).Value) - (TotalAmt8 - add_amt8), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt8 < add_amt8 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount8).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount8).Value) + (add_amt8 - TotalAmt8), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt9 > add_amt9 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount9).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount9).Value) - (TotalAmt9 - add_amt9), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt9 < add_amt9 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount9).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount9).Value) + (add_amt9 - TotalAmt9), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt10 > add_amt10 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount10).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount10).Value) - (TotalAmt10 - add_amt10), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt10 < add_amt10 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount10).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount10).Value) + (add_amt10 - TotalAmt10), 3)
            End If

            If gv1.Columns(colItemTotalAdditionalCharge) IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                gv1.Rows(LastIndex).Cells(colItemTotalAdditionalCharge).Value = clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount1).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount2).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount3).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount4).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount5).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount6).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount7).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount8).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount9).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount10).Value)
            End If
            ''==========================================================================================================
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function GetBaseOtherTaxableAmount(ByVal intEndCol As Integer) As Double
        ''Dim dblRetVal As Double = 0
        ''For ii As Integer = 0 To intEndCol - 1
        ''    If clsCommon.myCBool(gv2.Rows(ii).Cells(colTIsTaxable).Value) Then
        ''        dblRetVal = dblRetVal + clsCommon.myCdbl(clsCommon.myCstr(gv2.Rows(ii).Cells(colTTaxAmt).Value))
        ''    End If
        ''Next
        ''Return dblRetVal
    End Function
    Private Function GetBaseTaxAmount(ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 0 To intEndCol
            If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv2.Rows(ii).Cells(colTTaxAutCode).Value)) = CompairStringResult.Equal Then
                Return clsCommon.myCdbl(clsCommon.myCstr(gv2.Rows(ii).Cells(colTTaxAmt).Value))
            End If
        Next
        Return 0
    End Function
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Sub AddNew()
        BlankAllControls()

        LoadBlankGrid()
        LoadBlankGridAC()
        LoadBlankGridACInsurance()
        LoadBlankGridTax()

        gv1.Rows.AddNew()
        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
        ''gv1.Rows.AddNew()
        chkInternal.Checked = False
        gvAC.Rows.AddNew()
        gvAC.Rows.AddNew()
        If clsFixedParameter.GetData(clsFixedParameterCode.DisableShipToLocation, clsFixedParameterType.DisableShipToLocation, Nothing) = "1" Then
            txtShipToLocation.Enabled = False
        Else
            txtShipToLocation.Enabled = True
        End If
        chkShorategeIncludeInLandedCost.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterCode.IsShortageIncludeInLandedCost, clsFixedParameterType.IsShortageIncludeInLandedCost, Nothing)) = 1, True, False)
        '===============Rohit============
        SplitContainer3.Panel1Collapsed = True
        '==========================================
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        btnReverse.Visible = False
        AllowDepartmentMandatoryOnPurchaseCycle()
        txtSubLocation.Enabled = True
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        txttender.Text = Nothing
        isAgainstTender = False
    End Sub
    Function checkVendorItemPrice() As Boolean
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowLargerItemCostThenVendorItemCost, clsFixedParameterCode.AllowLargerItemCostThenVendorItemCost, Nothing)) = 0 And clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchasePickItemFromVendorItemDetails, clsFixedParameterCode.PurchasePickItemFromVendorItemDetails, Nothing)) = 1 Then
            For i As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value)
                Dim cellPrice As Double = clsCommon.myCdbl(gv1.Rows(i).Cells(colRate).Value)
                Dim dblMrp As Double = clsCommon.myCdbl(gv1.Rows(i).Cells(colMRP).Value)
                Dim strUnit As String = clsCommon.myCstr(gv1.Rows(i).Cells(colUnit).Value)
                'Dim vendorPrice As Double = clsDBFuncationality.getSingleValue("select item_rate from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' and item_no='" & strCode & "'")
                Dim vendorPrice As Double = clsVendorItemDetail.GetRate(txtVendorNo.Value, strICode, strUnit, dblMrp)
                If cellPrice > vendorPrice Then
                    clsCommon.MyMessageBoxShow("The Larger Price Of Item is not Allowed then the Vendor Item Price  at Row no " & (i + 1))
                    Return False
                Else
                    Return True
                End If
            Next
        Else
            Return True
        End If

    End Function
    Sub MakeColumnReadOnly(ByVal Read As Boolean)
        For Each gvrow As GridViewRowInfo In gv1.Rows
            gvrow.Cells(colCategoryType).ReadOnly = Read
            gvrow.Cells(colCapexCode).ReadOnly = Read
            gvrow.Cells(colCapexSubCode).ReadOnly = Read
            gvrow.Cells(colEmergency).ReadOnly = Read
        Next

    End Sub
    Function AllowToSave() As Boolean
        Try
            Dim dt As DataTable
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                If Not chk_qc_accepted.Checked Then
                    Throw New Exception("QC must be accepted.")
                End If
            End If
            ''RICHA AGARWAL DONE ON 19 APR,2018 AGAINST TICKET NO UDL/13/04/18-000098
            If clsCommon.myLen(txtInvNo.Text) > 0 Then
                If clsCommon.GetDateWithStartTime(dtpInvoice.Value) > clsCommon.GetDateWithEndTime(txtDate.Value) Then
                    Throw New Exception("Invoice Date can't be greater than Document Date")
                End If
            End If
            ''--------
            RadPageView1.SelectedPage = RadPageViewPage1
            If (btnSave.Text = "Update" AndAlso RadButton1.Visible = False) Then
                Dim strchk As String = "select Status from TSPL_SRN_HEAD where SRN_No='" + txtDocNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    Throw New Exception("Transaction already posted")
                End If
            End If
            RefreshReqNo()
            RefreshGRPNo()
            CalculateInsuranceTotal(False)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If PurchaseModulePickFixTaxRate AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colMRN_NO).Value) <= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then ''ERO/21/10/19-001071 by balwinder on 19/11/2019
                    gv1.CurrentRow = gv1.Rows(ii)
                    SetitemWiseTaxSetting(True, True)
                End If
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()

            CalLandAmt()
            CalNonRectax()
            CalRectax()
            CalAddtionalAmt()

            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                txtVendorNo.Focus()
                Throw New Exception("Please select Vendor")
            End If

            'CLEINT : UDL > DATE : 27-01-2017 > ASKED BY BALWINDER SIR
            If clsCommon.myLen(txtGENo.Text) > 0 Then
                If txtGEDate.Checked = False Then
                    txtGEDate.Focus()
                    Throw New Exception("Please Select Gate Entry Date.")
                End If
                If txtGEDate.Value > txtDate.Value Then
                    txtGEDate.Focus()
                    Throw New Exception("Gate Entry Date Should not be greater than Document Date.")
                End If
            End If

            If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.mbtnSRN) = CompairStringResult.Equal Then
                If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                    txtTaxGroup.Focus()
                    Throw New Exception("Please select Tax Group")
                End If
            End If

            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                txtBillToLocation.Focus()
                Throw New Exception("Please select Bill to Location")
            End If
            If clsCommon.CompairString("O", cboSRNType.SelectedValue) = CompairStringResult.Equal Then
                If clsCommon.myLen(txtSubLocation.Value) <= 0 Then
                    Throw New Exception("Please select Sub Location")
                End If
            End If
            If clsCommon.myLen(clsCommon.myCstr(txtBillToLocation.Value)) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                    If chkJobWorkOutward.Checked = False Then
                        If clsCommon.myLen(txtSubLocation.Value) <= 0 Then
                            Throw New Exception("Please select Sub Location")
                        End If
                    End If
                End If
            End If


            If is_Load_MRN Then
                If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, MRN_Date,103) from TSPL_MRN_HEAD where Against_GRN ='" + txtPONo.Value + "' and isnull(TSPL_MRN_HEAD.isCancel,0)=0 ")) > clsCommon.myCDate(txtDate.Value) Then
                    txtDate.Focus()
                    Throw New Exception("Date cannot be less than from MRN Date")
                End If
            Else
                If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, PurchaseOrder_Date,103) from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" + txtPONo.Value + "' and isnull(TSPL_PURCHASE_ORDER_HEAD.isCancel,0)=0")) > clsCommon.myCDate(txtDate.Value) Then
                    txtDate.Focus()
                    Throw New Exception("Date cannot be less than from PO Date")
                End If
            End If
            If clsCommon.myLen(txtRGPNo.Value) > 0 AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "AB") <> CompairStringResult.Equal Then
                If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, RGP_Date,103) from TSPL_RGP_HEAD where RGP_No ='" + txtRGPNo.Value + "'")) > clsCommon.myCDate(txtDate.Value) Then
                    txtDate.Focus()
                    Throw New Exception("Date cannot be less than from RGP Date")
                End If
            End If
            If clsCommon.myLen(txtShipToLocation.Value) > 0 And Not isApplyBrachAccounting Then
                If Not clsCommon.CompairString(txtShipToLocation.Value, txtBillToLocation.Value) = CompairStringResult.Equal Then
                    Dim qry As String = "select [State] from TSPL_LOCATION_MASTER where Location_Code in ('" + txtShipToLocation.Value + "','" + txtBillToLocation.Value + "') group by State"
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please define State Location of bill to location and ship to location")
                    End If
                    If dt.Rows.Count > 1 Then
                        Throw New Exception("State should be same of bill to location and ship to location")
                    End If

                End If

            End If

            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtDocNo.Focus()
                Throw New Exception("SRN No Not found to save")
            End If

            If clsCommon.CompairString(cboSRNType.SelectedValue, "") = CompairStringResult.Equal Then
                RadPageView1.SelectedPage = RadPageViewPage1
                cboSRNType.Select()
                Throw New Exception("Select SRN Type.")
            End If

            If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                cboItemType.Focus()
                Throw New Exception("Please select Item Type")
            End If


            Dim arrPONo As New List(Of String)
            Dim arrICode As New List(Of String)()
            Dim arrProjNo As New List(Of String)
            Dim arrSchNo As New List(Of String)
            Dim arrRGPNo As New List(Of String)

            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strPONo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPONo).Value)
                Dim strRGPNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRGPNo).Value)
                Dim strSchNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colScheduleNo).Value)
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colReqistionNo).Value)
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)

                If clsCommon.myLen(strICode) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        If ShowCapexCodeandSubCode Then
                            Dim Category As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colCategoryType).Value)
                            Dim Emergency As String = CInt(gv1.Rows(ii).Cells(colEmergency).Value)
                            Dim CapexCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexCode).Value)
                            Dim CapexSubCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexSubCode).Value)
                            If clsCommon.CompairString(Category, "") = CompairStringResult.Equal Then
                                Throw New Exception("Fill category at row no. " + clsCommon.myCstr(ii + 1) + "")
                            ElseIf clsCommon.CompairString(Category, "Capex") = CompairStringResult.Equal Then
                                If clsCommon.myLen(CapexSubCode) <= 0 Then
                                    Throw New Exception("Fill capex sub code at row no. " + clsCommon.myCstr(ii + 1) + "")
                                End If
                            End If
                        End If
                        If chkConfirmatoryPO.Checked Then
                            If clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value) <= 0 Then
                                Throw New Exception("Please Define " + clsCommon.myCstr(gv1.Columns(colRate).HeaderText) + " at row no. " + clsCommon.myCstr(ii + 1) + "")
                            End If
                        End If
                    End If


                End If


                '' this function create error for UDL that wise Case Inserted

                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select AllowSRNWithoutShortReject from tspl_Item_Master where Item_code='" & strICode & "'")) = 0 Then
                    If Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colOrgMRNQty).Value), 3) <> Math.Round((clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colBurstQty).Value) + IIf(is_Load_MRN, 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colShortQty).Value)) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colRejectedQty).Value)), 3) Then
                        Throw New Exception("Received Quantity( " + clsCommon.myCstr(clsCommon.myCdbl(gv1.Rows(ii).Cells(colOrgMRNQty).Value)) + " ) of Item " + strICode + "( " + strIName.Trim() + " ) should be equal to Total Quantity(Accepted+Leak+Burst+" + IIf(is_Load_MRN, "", "short+") + "Reject)(" + clsCommon.myCstr((clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colBurstQty).Value) + IIf(is_Load_MRN, 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colShortQty).Value)) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colRejectedQty).Value))) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                    End If
                End If
                '' End Function

                Dim dblPendingQty As Double = 0
                If is_Load_MRN Then
                    dblPendingQty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colOrgMRNQty).Value)
                Else
                    dblPendingQty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value)
                End If

                If Not is_Load_MRN AndAlso clsCommon.myLen(txtScheduleNo.Value) > 0 Then
                    dblPendingQty = clsPurchaseScheduleDetail.GetBalanceScheduleQty(strSchNo, strICode, txtDocNo.Value, txtDate.Text, strUOM, True)
                End If

                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)

                Dim strProject As String = Nothing
                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                    If clsCommon.myLen(strUOM) <= 0 Then
                        Throw New Exception("Please enter UOM for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                    End If
                    If clsCommon.myLen(strPONo) > 0 Then
                        If Not (arrPONo.Contains(strPONo)) Then
                            arrPONo.Add(strPONo)
                        End If
                        If dblQty > dblPendingQty Then
                            Throw New Exception("Entered Quantity( " + clsCommon.myCstr(dblQty) + " ) of Item " + strICode + "( " + strIName.Trim() + " ) Can't be more than PO Quantity(" + clsCommon.myCstr(dblPendingQty) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        End If
                        If Not is_Load_MRN Then
                            If clsCommon.myCdbl(gv1.Rows(ii).Cells(colOrgMRNQty).Value) > clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value) Then
                                Throw New Exception("Received  Quantity( " + clsCommon.myCstr(clsCommon.myCdbl(gv1.Rows(ii).Cells(colOrgMRNQty).Value)) + " ) of Item " + strICode + "( " + strIName.Trim() + " ) Can't be more than Pending Quantity(" + clsCommon.myCstr(clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value)) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                            End If
                        End If
                    End If
                    If clsCommon.myLen(strReqNo) > 0 Then
                        If dblQty > dblPendingQty Then
                            Throw New Exception("Item " + strICode + "( " + strIName.Trim() + " ) Entered Quantity(" + clsCommon.myCstr(dblQty) + ") Can't be more than Pending Quantity(" + clsCommon.myCstr(dblPendingQty) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        End If
                    End If

                    '=================================16/02/2015=====================================================
                    If clsCommon.myLen(strSchNo) > 0 Then
                        If Not (arrSchNo.Contains(strSchNo)) Then
                            arrSchNo.Add(strSchNo)
                        End If
                        If dblQty > dblPendingQty Then
                            Throw New Exception("Entered Quantity( " + clsCommon.myCstr(dblQty) + " ) of Item " + strICode + "( " + strIName.Trim() + " ) Cannot be more than Schedule Quantity(" + clsCommon.myCstr(dblPendingQty) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        End If
                    End If
                    '=======================added by shivani=check item stock sent by vendor in case of against Bom when isRGPAfterPO setting is on  ==========================================================
                    If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myLen(strRGPNo) > 0 AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "AB") = CompairStringResult.Equal AndAlso isRGPAfterPO AndAlso Not is_Load_MRN Then
                        Dim strMsg As String = clsRGPHead.GetRGPTypeItemBalance(cmbRGPType.SelectedValue, dblQty, txtVendorNo.Value, Nothing, clsCommon.myCstr(strICode), clsCommon.myCstr(strUOM), txtDate.Value, txtDocNo.Value, True)
                        If clsCommon.myLen(strMsg) > 0 Then
                            gv1.Rows(ii).Cells(colQty).Value = 0
                            Throw New Exception(strMsg)
                        End If
                    End If

                    If clsCommon.myLen(strRGPNo) > 0 Then
                        If Not (arrRGPNo.Contains(strRGPNo)) Then
                            arrRGPNo.Add(strRGPNo)
                        End If
                        If dblQty > dblPendingQty AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "AB") <> CompairStringResult.Equal Then
                            Throw New Exception("Entered Quantity( " + clsCommon.myCstr(dblQty) + " ) of Item " + strICode + "( " + strIName.Trim() + " ) Cannot be more than RGP Quantity(" + clsCommon.myCstr(dblPendingQty) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        End If
                        If Not is_Load_MRN Then
                            If clsCommon.myCdbl(gv1.Rows(ii).Cells(colOrgMRNQty).Value) > clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value) Then
                                Throw New Exception("Received  Quantity( " + clsCommon.myCstr(clsCommon.myCdbl(gv1.Rows(ii).Cells(colOrgMRNQty).Value)) + " ) of Item " + strICode + "( " + strIName.Trim() + " ) Can't be more than Pending Quantity(" + clsCommon.myCstr(clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value)) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                            End If
                        End If
                    End If
                    '==============================================================
                    If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) <> CompairStringResult.Equal Then
                        If clsCommon.myCBool(gv1.Rows(ii).Cells(colisMRPMandatory).Value) AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value) <= 0 Then
                            Throw New Exception("Please enter MRP for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "F") = CompairStringResult.Equal AndAlso IsBatchDetailMandatory(gv1.Rows(ii).Cells(colUnit).Value) Then
                        '' validation for finished good for batch no ,mfgg ,exp
                        dt = clsPurchaseOrderHead.GetPurchaseSetting()
                        If dt.Rows(0).Item("MANDATE_BATCHNO_FG") = True Then
                            If clsCommon.myLen(gv1.Rows(ii).Cells(colBatchNo).Value) <= 0 Then
                                Throw New Exception("Please enter Batch No for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                        End If
                        If dt.Rows(0).Item("MANDATE_EXP_FG") = True Then
                            If clsCommon.myLen(gv1.Rows(ii).Cells(colExpiry).Value) <= 0 Then
                                Throw New Exception("Please enter Expiry Date for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                        End If
                        If dt.Rows(0).Item("MANDATE_MFG_FG") = True Then
                            If clsCommon.myLen(gv1.Rows(ii).Cells(colManufactureDate).Value) <= 0 Then
                                Throw New Exception("Please enter Manufacture Date for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                        End If

                    End If

                    If objCommonVar.IsDemoERP Then
                        If Not strProject Is Nothing Then
                            strProject = clsPurchaseOrderHead.IsValidProjectForPO(strReqNo, "")
                            If arrProjNo.Contains(strProject) And arrProjNo.Count > 0 Then
                                Throw New Exception("PO No:" + strReqNo + " and Item : " + strIName + " is not for PJC or not related to PJC At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                            Else
                                arrProjNo.Add(strProject)
                            End If
                        End If
                    End If

                    If Not arrICode.Contains(strICode) Then
                        arrICode.Add(strICode)
                    End If
                    If AllowPurchaseModulewithUniqueItem = 1 AndAlso clsCommon.myLen(txtRequistionNo.Value) = 0 Then
                        For jj As Integer = ii + 1 To gv1.Rows.Count - 1
                            Dim strInICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                            Dim strInUOM As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)
                            If clsCommon.CompairString(strICode, strInICode) = CompairStringResult.Equal Then
                                Throw New Exception("Item Code " + strICode + "  is repeated at Row No" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1))
                            End If
                        Next
                    End If

                    If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsSerialseItem).Value) Then
                        Dim arrSerailNo As List(Of clsSerializeInvenotry) = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                        If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsPickAutoSrNo).Value) Then
                            Dim arrOut As List(Of clsSerializeInvenotry) = New List(Of clsSerializeInvenotry)
                            If arrSerailNo Is Nothing OrElse arrSerailNo.Count <= 0 Then
                                For kk As Integer = 1 To dblQty
                                    Dim obj As clsSerializeInvenotry = New clsSerializeInvenotry()
                                    obj.Auto_Sr_No = clsItemMaster.GetItemSerialCounter(strICode, Nothing)
                                    arrOut.Add(obj)
                                Next
                            Else
                                For kk As Integer = 0 To arrSerailNo.Count - 1
                                    If kk > dblQty - 1 Then
                                        Exit For
                                    Else
                                        Dim obj As clsSerializeInvenotry = New clsSerializeInvenotry()
                                        If clsCommon.myLen(arrSerailNo(kk).Auto_Sr_No) > 0 Then
                                            obj.Auto_Sr_No = arrSerailNo(kk).Auto_Sr_No
                                        Else
                                            obj.Auto_Sr_No = clsItemMaster.GetItemSerialCounter(strICode, Nothing)
                                        End If
                                        arrOut.Add(obj)
                                    End If
                                Next
                                If arrOut.Count < dblQty Then
                                    For kk As Integer = arrOut.Count + 1 To dblQty
                                        Dim obj As clsSerializeInvenotry = New clsSerializeInvenotry()
                                        obj.Auto_Sr_No = clsItemMaster.GetItemSerialCounter(strICode, Nothing)
                                        arrOut.Add(obj)
                                    Next
                                End If
                            End If
                            gv1.Rows(ii).Tag = arrOut
                        End If
                        arrSerailNo = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                        If is_Srn_rejQty_goes_in_Rejstore Then
                            If arrSerailNo Is Nothing OrElse (dblQty <> arrSerailNo.Count And dblQty <> (arrSerailNo.Count - clsCommon.myCdbl(gv1.Rows(ii).Cells(colRejectedQty).Value))) Then
                                Throw New Exception("Please Provide serial No. for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                        Else
                            If arrSerailNo Is Nothing OrElse dblQty <> arrSerailNo.Count Then
                                Throw New Exception("Please Provide serial No. for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                        End If

                    End If

                    If isItemfromVendorItemDetails Then
                        Dim qry As String = "select 1 from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" + txtVendorNo.Value + "' and item_no in ('" + strICode + "')"
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("item : " + strICode + " is not for vendor: " + txtVendorNo.Value + " . At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                    End If
                End If

                '' added code by parteek HSN Code related
                Dim IsSkip As Boolean = False
                IsSkip = clsDBFuncationality.getSingleValue("select case when isnull( Skip_GST,0)=1 then 1 else 0 end as Skip_GST from tspl_item_master where item_code='" & clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) & "'")
                If clsERPFuncationality.GetGSTStatus(txtDate.Value) AndAlso IsSkip = False Then
                    If clsCommon.CompairString(cboItemType.SelectedValue, "N") <> CompairStringResult.Equal Then
                        Dim taxamt As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value)
                        Dim HSNCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colHSNNo).Value)

                        If clsCommon.myCdbl(taxamt) > 0 AndAlso clsCommon.myLen(HSNCode) <= 0 Then
                            Throw New Exception("HSN Code is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        End If

                    End If
                End If
                '' ===== ENd of code===
                ''richa agarwal 27 June,2019
                If dblQty > 0 AndAlso clsCommon.myCBool(clsItemMaster.IsBatchItem(gv1.Rows(ii).Cells(colICode).Value)) Then
                    Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(gv1.Rows(ii).Cells(colICode).Tag, List(Of clsBatchInventory))
                    If arrBatchNo Is Nothing Then
                        Throw New Exception("Please provide Batch no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                    Else
                        Dim tQty As Decimal = 0
                        For Each objBatch As clsBatchInventory In arrBatchNo
                            tQty += objBatch.Qty
                        Next
                        If tQty <> dblQty Then
                            Throw New Exception("Item : " + strICode + " Entered Qty " + clsCommon.myCstr(dblQty) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                    End If
                End If
            Next
            If arrPONo IsNot Nothing AndAlso arrPONo.Count > 0 Then
                If chkConfirmatoryPO.Checked Then
                    Throw New Exception("This Document is made by Purchase order. So it can not be of confimatory PO Type")
                End If
            End If

            For ii As Integer = 0 To gvAC.Rows.Count - 1
                If clsCommon.myLen(gvAC.Rows(ii).Cells(colACCode).Value) > 0 Then
                    For jj As Integer = 0 To gvAC.Rows.Count - 1
                        If ii = jj Then
                            Continue For
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gvAC.Rows(ii).Cells(colACCode).Value), clsCommon.myCstr(gvAC.Rows(jj).Cells(colACCode).Value)) = CompairStringResult.Equal Then
                            Throw New Exception("Additional Charges: " + clsCommon.myCstr(gvAC.Rows(ii).Cells(colACCode).Value) + "Repeated at Row No " + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1) + "")
                        End If
                    Next
                End If
            Next

            If Not checkVendorItemPrice() Then
                Return False
            End If
            If clsCommon.myLen(txtForm38.Text) > 0 Then
                Dim qry As String = "select SRN_No from TSPL_SRN_HEAD where Form_38='" + txtForm38.Text + "' and SRN_No not in ('" + txtDocNo.Value + "')"
                Dim strSrnNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                If clsCommon.myLen(strSrnNo) > 0 Then
                    Throw New Exception("Form 38 No : " + txtForm38.Text + " already used in SRN No" + strSrnNo)
                End If
            End If

            'done by stuti on 19/10/2016 against purchase points
            If clsCommon.myLen(txt_RoadPermitNo.Text) <= 0 Then
                Dim qry As String = Nothing
                If clsCommon.myLen(txtShipToLocation.Value) > 0 Then
                    qry = "select TSPL_VENDOR_MASTER.State_Code  from TSPL_VENDOR_MASTER left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.State=TSPL_VENDOR_MASTER.State_Code where TSPL_VENDOR_MASTER.Vendor_Code='" + clsCommon.myCstr(txtVendorNo.Value) + "' and TSPL_LOCATION_MASTER.Location_Code='" + clsCommon.myCstr(txtShipToLocation.Value) + "'"
                Else
                    qry = "select TSPL_VENDOR_MASTER.State_Code  from TSPL_VENDOR_MASTER left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.State=TSPL_VENDOR_MASTER.State_Code where TSPL_VENDOR_MASTER.Vendor_Code='" + clsCommon.myCstr(txtVendorNo.Value) + "' and TSPL_LOCATION_MASTER.Location_Code='" + clsCommon.myCstr(txtBillToLocation.Value) + "'"
                End If
                Dim strcheck As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                If AllowRoadPermitNo = 1 Then
                    If clsCommon.myLen(strcheck) <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage2
                        Throw New Exception("Please fill Road permit no.")
                    End If
                End If

            End If

            clsItemMaster.isItemOfSameType(clsCommon.myCstr(cboItemType.SelectedValue), cboItemType.Text, arrICode)
            clsMRNHead.IsValidVendorForMRN(arrPONo, txtVendorNo.Value)
            clsRGPHead.IsValidVendorForRGP(arrRGPNo, txtVendorNo.Value)
            clsPurchaseSchedule.IsValidVendorForSchedule(arrSchNo, txtVendorNo.Value)
            AllowToSave_FormEntry()
            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()
            ''For GST Skip
            Dim isSkipGST As Boolean = False
            dt = clsDBFuncationality.GetDataTable("select sum(case when isnull( Skip_GST,0)=1 then 1 else 0 end) as NoOfSkipGSTItem,sum(case when isnull( Skip_GST,0)=0 then 1 else 0 end) as NoOfNonSkipGSTItem from tspl_item_master where item_Code in (" + clsCommon.GetMulcallString(arrICode) + ")")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.myCdbl(dt.Rows(0)("NoOfSkipGSTItem")) > 0 Then
                    If clsCommon.myCdbl(dt.Rows(0)("NoOfNonSkipGSTItem")) > 0 Then
                        Throw New Exception("All Item should be of Skip GST or Not")
                    End If
                    isSkipGST = True
                End If
            End If
            dt = Nothing
            If (Not isSkipGST) AndAlso clsCommon.CompairString(FORMTYPE, clsUserMgtCode.mbtnSRN) = CompairStringResult.Equal Then
                clsLocationWiseTax.IsValidTaxGroup(txtTaxGroup.Value, txtBillToLocation.Value, txtVendorNo.Value, "P", txtDate.Value, Nothing)
            End If
            ''End of For GST Skip
            '' Ticket No - BM00000010231 By Parteek
            Dim SRNLim As Double = 0
            Dim SRNAmt As Double = 0
            Dim POSRNGRN As String = ""
            Dim IS_Check_GRN As String = txtPONo.Value
            SRNLim = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(SRN_Limit,0) AS SRN_Limit From TSPL_PURCHASE_SETTINGS"))
            POSRNGRN = clsCommon.myCstr(lblPoNo.Text)
            If SRNLim > 0 Then
                SRNAmt = clsCommon.myCdbl(lblTotRAmt.Text)
                If clsCommon.myLen(IS_Check_GRN) > 0 Then
                    Return True
                Else
                    If SRNAmt > SRNLim Then
                        Throw New Exception("Please first make " & POSRNGRN & " ! Document amount (" & clsCommon.myCstr(SRNAmt) & ") is more than SRN limit (" & clsCommon.myCstr(SRNLim) & ")")
                    End If
                End If
            End If

            If objCommonVar.RCDFCFP = True AndAlso ItemCostTolerancePercentage > 0 Then
                Dim dclEnterCost As Decimal = 0
                Dim dclItemMasterCost As Decimal = 0
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    dclEnterCost = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colRate).Value)
                    dclItemMasterCost = clsCommon.myCDecimal(clsItemMaster.GetItemCost(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value), Nothing))
                    If dclEnterCost > 0 AndAlso dclItemMasterCost > 0 Then
                        If dclEnterCost > (dclItemMasterCost + (dclItemMasterCost * ItemCostTolerancePercentage / 100)) OrElse dclEnterCost < (dclItemMasterCost - (dclItemMasterCost * ItemCostTolerancePercentage / 100)) Then
                            Throw New Exception("Item : " + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + " UOM- " + clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value) + " Rate- " + clsCommon.myCstr(gv1.Rows(ii).Cells(colRate).Value) + " At Line No " + clsCommon.myCstr(ii + 1) + Environment.NewLine + " Rate is beyound the tolerance level.")
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Sub CalAddtionalAmt()
        Dim dblLandedCost As Double = 0
        Dim dblAdditionalAmt As Double = 0
        Dim dblTotItemCost As String = 0

        For ii As Integer = 0 To gv1.Rows.Count - 1
            If gv1.Rows(ii).Cells(colRowType).Value = clsItemRowType.RowTypeItem Then
                dblTotItemCost = dblTotItemCost + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
            End If
            If gv1.Rows(ii).Cells(colRowType).Value = clsItemRowType.RowTypeMisc Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colAmt).Value)
            End If
        Next
        dblAdditionalAmt = dblAdditionalAmt + CDec(lblAddCharges.Text)
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If gv1.Rows(ii).Cells(colRowType).Value = clsItemRowType.RowTypeItem Then
                If dblAdditionalAmt = 0 Then
                    gv1.Rows(ii).Cells(colUnitTotAddCost).Value = 0
                Else
                    Dim dcldivide As Decimal = dblTotItemCost * dblAdditionalAmt
                    If dcldivide = 0 Then
                        dblLandedCost = 0
                    Else
                        dblLandedCost = gv1.Rows(ii).Cells(colAmt).Value / dcldivide
                    End If


                    If dblLandedCost <> 0 Then
                        gv1.Rows(ii).Cells(colUnitTotAddCost).Value = Math.Round(dblLandedCost / (CDec(gv1.Rows(ii).Cells(colQty).Value) + CDec(gv1.Rows(ii).Cells(colRejectedQty).Value) + CDec(gv1.Rows(ii).Cells(colLeakQty).Value) + CDec(gv1.Rows(ii).Cells(colShortQty).Value) + CDec(gv1.Rows(ii).Cells(colBurstQty).Value)), 6)
                    Else
                        gv1.Rows(ii).Cells(colUnitTotAddCost).Value = 0
                    End If
                End If
            End If
        Next
    End Sub
    Sub CalLandAmt()
        Dim dblLandedCost As Double = 0
        Dim dblLandedRate As Double = 0
        Dim dblAdditionalAmt As Double = 0
        Dim dblTotAmtAfterDiscount As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                    If chkShorategeIncludeInLandedCost.Checked Then
                        dblTotAmtAfterDiscount = dblTotAmtAfterDiscount + IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisType).Value) = 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemAmtAfterInsurance).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value))
                    Else
                        dblTotAmtAfterDiscount = dblTotAmtAfterDiscount + IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisType).Value) = 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtLessDiscountWithoutShortage).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value))
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                    dblAdditionalAmt = dblAdditionalAmt + IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisType).Value) = 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemAmtAfterInsurance).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value))
                    dblAdditionalAmt += GetNonRecoverableTax(ii)
                End If
            End If
        Next
        dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(lblAddCharges.Text)

        Dim dblTotalAcceptedAmt As Double = 0
        Dim dblTotalRejectedAmt As Double = 0
        Dim dblTotalShortageAmt As Double = 0
        Dim dblTotalLeakAmt As Double = 0
        Dim dblTotalBurstAmt As Double = 0

        For ii As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                If gv1.Rows(ii).Cells(colRowType).Value = clsItemRowType.RowTypeItem Then
                    Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colRejectedQty).Value)
                    Dim dblShortQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colShortQty).Value)
                    If dblQty > 0 OrElse dblShortQty > 0 Then
                        If chkShorategeIncludeInLandedCost.Checked Then
                            dblQty += dblShortQty
                        End If
                        Dim dblAmtAfterDiscount As Double = IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisType).Value) = 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemAmtAfterInsurance).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)) + GetNonRecoverableTax(ii)
                        Dim dblAmtAfterDiscountRatio As Double = 0
                        If chkShorategeIncludeInLandedCost.Checked Then
                            dblAmtAfterDiscountRatio = IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisType).Value) = 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemAmtAfterInsurance).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value))
                        Else
                            dblAmtAfterDiscountRatio = IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisType).Value) = 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtLessDiscountWithoutShortage).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value))
                        End If
                        dblLandedCost = dblAmtAfterDiscount + IIf(dblTotAmtAfterDiscount > 0, ((dblAdditionalAmt * dblAmtAfterDiscountRatio) / dblTotAmtAfterDiscount), 0)
                        If Not chkShorategeIncludeInLandedCost.Checked Then
                            gv1.Rows(ii).Cells(colShortageAmount).Value = Math.Round((dblAmtAfterDiscount * dblShortQty / (dblQty + dblShortQty)), 2)
                            dblLandedCost = dblLandedCost - clsCommon.myCdbl(gv1.Rows(ii).Cells(colShortageAmount).Value)
                        End If

                        dblLandedRate = IIf(dblQty = 0, 0, dblLandedCost / dblQty)
                        gv1.Rows(ii).Cells(colLandedAmt).Value = Math.Round(dblLandedCost, 2)
                        gv1.Rows(ii).Cells(colLandedRate).Value = Math.Round(dblLandedRate, 4)
                        gv1.Rows(ii).Cells(colAcceptedAmount).Value = Math.Round(dblLandedRate * clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value), 2)
                        gv1.Rows(ii).Cells(colRejectedAmount).Value = Math.Round(dblLandedRate * clsCommon.myCdbl(gv1.Rows(ii).Cells(colRejectedQty).Value), 2)
                        gv1.Rows(ii).Cells(colLeakAmount).Value = Math.Round(dblLandedRate * clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeakQty).Value), 2)
                        gv1.Rows(ii).Cells(colBurstAmount).Value = Math.Round(dblLandedRate * clsCommon.myCdbl(gv1.Rows(ii).Cells(colBurstQty).Value), 2)
                        If chkShorategeIncludeInLandedCost.Checked Then
                            gv1.Rows(ii).Cells(colShortageAmount).Value = Math.Round(dblLandedRate * clsCommon.myCdbl(gv1.Rows(ii).Cells(colShortQty).Value), 2)
                        End If
                    End If
                End If
                dblTotalAcceptedAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells(colAcceptedAmount).Value)
                dblTotalRejectedAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells(colRejectedAmount).Value)
                dblTotalShortageAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells(colShortageAmount).Value)
                dblTotalLeakAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeakAmount).Value)
                dblTotalBurstAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells(colBurstAmount).Value)
            End If
        Next

        lblAcceptedAmt.Text = clsCommon.myFormat(dblTotalAcceptedAmt)
        lblRejectedAmt.Text = clsCommon.myFormat(dblTotalRejectedAmt)
        lblShortageAmt.Text = clsCommon.myFormat(dblTotalShortageAmt)
        lblLeakAmt.Text = clsCommon.myFormat(dblTotalLeakAmt)
        lblBurstAmt.Text = clsCommon.myFormat(dblTotalBurstAmt)
    End Sub
    Function GetNonRecoverableTax(ByVal rowNo As Integer) As Double
        Dim dblAdditionalAmt As Double = 0
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable1).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt1).Value)
        End If
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable2).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt2).Value)
        End If
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable3).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt3).Value)
        End If
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable4).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt4).Value)
        End If
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable5).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt5).Value)
        End If
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable6).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt6).Value)
        End If
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable7).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt7).Value)
        End If
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable8).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt8).Value)
        End If
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable9).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt9).Value)
        End If
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable10).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt10).Value)
        End If
        Return dblAdditionalAmt
    End Function
    Sub CalNonRectax()
        Dim dblAdditionalAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            dblAdditionalAmt = GetNonRecoverableTax(ii)
            If dblAdditionalAmt > 0 Then
                Dim dblTotQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colShortQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colBurstQty).Value)
                If dblTotQty = 0 Then
                    dblTotQty = 1
                End If
                gv1.Rows(ii).Cells(colUnitTotNonRecTax).Value = dblAdditionalAmt / dblTotQty
            Else
                gv1.Rows(ii).Cells(colUnitTotNonRecTax).Value = 0
            End If
        Next
    End Sub
    Sub CalRectax()
        Dim dblAdditionalAmt As Double = 0

        For ii As Integer = 0 To gv1.Rows.Count - 1
            dblAdditionalAmt = 0
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable1).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt1).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable2).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt2).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable3).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt3).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable4).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt4).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable5).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt5).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable6).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt6).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable7).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt7).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable8).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt8).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable9).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt9).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable10).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt10).Value)
            End If
            If dblAdditionalAmt > 0 Then
                Dim dblTotQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colShortQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colBurstQty).Value)
                If dblTotQty = 0 Then
                    dblTotQty = 1
                End If
                gv1.Rows(ii).Cells(colUnitTotRecTax).Value = dblAdditionalAmt / dblTotQty
            Else
                gv1.Rows(ii).Cells(colUnitTotRecTax).Value = 0
            End If
        Next
    End Sub
    Function SaveDataForRGPSRN(ByVal itemtype As String) As Boolean
        Try
            isNewEntry = True
            cboItemType.DataSource = clsItemMaster.GetItemType()
            cboItemType.ValueMember = "Code"
            cboItemType.DisplayMember = "Name"
            Try
                cboItemType.SelectedValue = itemtype
            Catch exx As Exception
            End Try
            SaveData(False)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Sub SaveData(ByVal ChekPostBtn As Boolean)
        Dim obj As New clsSRNHead()
        Try
            '' Anubhooti 13-Sep-2014 BM00000003735
            If ChekPostBtn = False Then
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Store Received Note", txtDate.Value) = False Then
                    Exit Sub
                End If
            End If
            ''

            If (AllowToSave()) Then
                If txtDocNo.Value = "Rejected" Then
                    clsCommon.MyMessageBoxShow("Data cannot be saved.")
                Else
                End If

                obj = New clsSRNHead()
                obj.isExemptSecurityDedution = IIf(chkExemptSecurityDedution.Checked = True, 1, 0)
                obj.isJobWorkOutward = IIf(chkJobWorkOutward.Checked = True, 1, 0)
                obj.RGP_Type = clsCommon.myCstr(cmbRGPType.SelectedValue)
                If isRGPAfterPO AndAlso clsCommon.CompairString(cboSRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtRGPNo.Value) > 0 Then
                    obj.RGP_Type = "AR"
                End If
                obj.Document_Type = clsCommon.myCstr(cmbDocType.SelectedValue)
                obj.SRN_No = txtDocNo.Value
                obj.SRN_Date = txtDate.Value
                obj.Vendor_Code = txtVendorNo.Value
                obj.Vendor_Name = lblVendorName.Text
                obj.Ref_No = txtRefNo.Text
                obj.Inv_Date = clsCommon.GetPrintDate(dtpInvoice.Value, "dd/MMM/yyyy")
                obj.Challan_Date = clsCommon.GetPrintDate(dtpChallan.Value, "dd/MMM/yyyy")
                obj.Total_Tax_Amt = clsCommon.myCdbl(lblTaxAmt.Text)
                obj.Inv_No = txtInvNo.Text
                obj.is_RGP_Non_Inventory = chkRGPNonInventory.Checked
                obj.is_QCAccepted = chk_qc_accepted.Checked
                obj.Bill_To_Location = txtBillToLocation.Value
                obj.Ship_To_Location = txtShipToLocation.Value
                obj.Sublocation_Code = txtSubLocation.Value
                obj.Comments = txtComment.Text
                obj.On_Hold = chkOnHold.Checked
                obj.Description = txtDesc.Text
                obj.Tax_Group = txtTaxGroup.Value
                obj.Form_38 = txtForm38.Text
                obj.Is_Internal = chkInternal.Checked
                obj.Confirmatory_PO = chkConfirmatoryPO.Checked
                obj.Retention = clsCommon.myCdbl(TxtRetention.Text)
                'stuti
                If txt_RoadPermitDate.Text IsNot Nothing AndAlso clsCommon.myLen(txt_RoadPermitDate.Text) > 0 AndAlso IsDate(txt_RoadPermitDate.Text) Then
                    obj.RoadPermit_Date = clsCommon.myCDate(txt_RoadPermitDate.Text)
                Else
                    obj.RoadPermit_Date = clsCommon.GETSERVERDATE()
                End If
                obj.RoadPermit_No = clsCommon.myCstr(txt_RoadPermitNo.Text)
                '====end here===
                '---------------------------------------------------------
                'obj.isreadytopost = "0"
                obj.autosrnfromrgp = txtautosrnremarks.Text

                'If chkreadytopost.Checked Then
                '    obj.isreadytopost = "1"
                'End If

                obj.Against_QC_Code = clsCommon.myCstr(txtQCcode.Text)
                obj.Against_QC_Date = clsCommon.myCstr(txtQcdate.Text)
                If clsCommon.myLen(obj.Against_QC_Date) > 0 Then
                    obj.Against_QC_Date = clsCommon.GetPrintDate(obj.Against_QC_Date, "dd/MM/yyyy")
                End If
                '--------------------------------------------------------------

                If (gv2.Rows.Count > 0) Then
                    obj.TAX1 = clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value)
                    obj.TAX1_Rate = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxRate).Value)
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTBaseAmt).Value)
                    obj.TAX1_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxAmt).Value)
                    obj.AssessableAmt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxAssessableAmt).Value)
                End If
                If (gv2.Rows.Count > 1) Then
                    obj.TAX2 = clsCommon.myCstr(gv2.Rows(1).Cells(colTTaxAutCode).Value)
                    obj.TAX2_Rate = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxRate).Value)
                    obj.TAX2_Base_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTBaseAmt).Value)
                    obj.TAX2_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 2) Then
                    obj.TAX3 = clsCommon.myCstr(gv2.Rows(2).Cells(colTTaxAutCode).Value)
                    obj.TAX3_Rate = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxRate).Value)
                    obj.TAX3_Base_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTBaseAmt).Value)
                    obj.TAX3_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 3) Then
                    obj.TAX4 = clsCommon.myCstr(gv2.Rows(3).Cells(colTTaxAutCode).Value)
                    obj.TAX4_Rate = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxRate).Value)
                    obj.TAX4_Base_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTBaseAmt).Value)
                    obj.TAX4_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 4) Then
                    obj.TAX5 = clsCommon.myCstr(gv2.Rows(4).Cells(colTTaxAutCode).Value)
                    obj.TAX5_Rate = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxRate).Value)
                    obj.TAX5_Base_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTBaseAmt).Value)
                    obj.TAX5_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 5) Then
                    obj.TAX6 = clsCommon.myCstr(gv2.Rows(5).Cells(colTTaxAutCode).Value)
                    obj.TAX6_Rate = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxRate).Value)
                    obj.TAX6_Base_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTBaseAmt).Value)
                    obj.TAX6_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 6) Then
                    obj.TAX7 = clsCommon.myCstr(gv2.Rows(6).Cells(colTTaxAutCode).Value)
                    obj.TAX7_Rate = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxRate).Value)
                    obj.TAX7_Base_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTBaseAmt).Value)
                    obj.TAX7_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 7) Then
                    obj.TAX8 = clsCommon.myCstr(gv2.Rows(7).Cells(colTTaxAutCode).Value)
                    obj.TAX8_Rate = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxRate).Value)
                    obj.TAX8_Base_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTBaseAmt).Value)
                    obj.TAX8_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 8) Then
                    obj.TAX9 = clsCommon.myCstr(gv2.Rows(8).Cells(colTTaxAutCode).Value)
                    obj.TAX9_Rate = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxRate).Value)
                    obj.TAX9_Base_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTBaseAmt).Value)
                    obj.TAX9_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 9) Then
                    obj.TAX10 = clsCommon.myCstr(gv2.Rows(9).Cells(colTTaxAutCode).Value)
                    obj.TAX10_Rate = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxRate).Value)
                    obj.TAX10_Base_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTBaseAmt).Value)
                    obj.TAX10_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxAmt).Value)
                End If

                obj.Terms_Code = txtTermCode.Value
                obj.Due_Date = txtDueDate.Value
                obj.Discount_Base = clsCommon.myCdbl(lblAmtWithDiscount.Text)
                obj.Discount_Amt = clsCommon.myCdbl(lblDiscountAmt.Text)
                obj.Amount_Less_Discount = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
                obj.Total_Taxable_Amount = clsCommon.myCdbl(lblTaxableAmount.Text)
                obj.SRN_Total_Amt = clsCommon.myCdbl(lblTotRAmt.Text)

                obj.Carrier = txtCarrier.Text
                obj.VehicleNo = txtVehicleNo.Text
                obj.GRNo = txtGRNo.Text
                obj.GENo = txtGENo.Text
                obj.Against_RGP = txtRGPNo.Value
                obj.Landed_Add_Cost = clsCommon.myCdbl(lblLandedCost.Text)
                obj.Total_Landed_Cost = clsCommon.myCdbl(lblAmtAfterLandedCost.Text)
                If txtGEDate.Checked Then
                    obj.GEDate = txtGEDate.Value
                End If
                obj.Item_Type = clsCommon.myCstr(cboItemType.SelectedValue)
                obj.Dept = txtDept.Value
                obj.Dept_Desc = lblDept.Text

                obj.Against_PO = IIf(is_Load_MRN, txtPONo.Tag, txtPONo.Value)
                'obj.Against_MRN = IIf(is_Load_MRN, txtPONo.Value, Nothing)
                obj.PROJECT_ID = fndProject.Value

                obj.Against_Schedule_Code = clsCommon.myCstr(txtScheduleNo.Value)
                obj.PurchaseOrder_Type = clsCommon.myCstr(cboSRNType.SelectedValue)

                If (gvAC.Rows.Count > 0) Then
                    If clsCommon.myLen(gvAC.Rows(0).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colACCode).Value)
                        obj.Add_Charge_Name1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colACName).Value)
                        obj.Add_Charge_Amt1 = clsCommon.myCdbl(gvAC.Rows(0).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 1) Then
                    If clsCommon.myLen(gvAC.Rows(1).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colACCode).Value)
                        obj.Add_Charge_Name2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colACName).Value)
                        obj.Add_Charge_Amt2 = clsCommon.myCdbl(gvAC.Rows(1).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 2) Then
                    If clsCommon.myLen(gvAC.Rows(2).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colACCode).Value)
                        obj.Add_Charge_Name3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colACName).Value)
                        obj.Add_Charge_Amt3 = clsCommon.myCdbl(gvAC.Rows(2).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 3) Then
                    If clsCommon.myLen(gvAC.Rows(3).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colACCode).Value)
                        obj.Add_Charge_Name4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colACName).Value)
                        obj.Add_Charge_Amt4 = clsCommon.myCdbl(gvAC.Rows(3).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 4) Then
                    If clsCommon.myLen(gvAC.Rows(4).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colACCode).Value)
                        obj.Add_Charge_Name5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colACName).Value)
                        obj.Add_Charge_Amt5 = clsCommon.myCdbl(gvAC.Rows(4).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 5) Then
                    If clsCommon.myLen(gvAC.Rows(5).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colACCode).Value)
                        obj.Add_Charge_Name6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colACName).Value)
                        obj.Add_Charge_Amt6 = clsCommon.myCdbl(gvAC.Rows(5).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 6) Then
                    If clsCommon.myLen(gvAC.Rows(6).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colACCode).Value)
                        obj.Add_Charge_Name7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colACName).Value)
                        obj.Add_Charge_Amt7 = clsCommon.myCdbl(gvAC.Rows(6).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 7) Then
                    If clsCommon.myLen(gvAC.Rows(7).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colACCode).Value)
                        obj.Add_Charge_Name8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colACName).Value)
                        obj.Add_Charge_Amt8 = clsCommon.myCdbl(gvAC.Rows(7).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 8) Then
                    If clsCommon.myLen(gvAC.Rows(8).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colACCode).Value)
                        obj.Add_Charge_Name9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colACName).Value)
                        obj.Add_Charge_Amt9 = clsCommon.myCdbl(gvAC.Rows(8).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 9) Then
                    If clsCommon.myLen(gvAC.Rows(9).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colACCode).Value)
                        obj.Add_Charge_Name10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colACName).Value)
                        obj.Add_Charge_Amt10 = clsCommon.myCdbl(gvAC.Rows(9).Cells(colACAmount).Value)
                    End If
                End If
                obj.Total_Add_Charge = clsCommon.myCdbl(lblAddCharges.Text)

                obj.Total_Add_Charge_Insurance = clsCommon.myCdbl(lblAddChargesForInsurance.Text)
                obj.Total_Item_Insurance_Amt = clsCommon.myCdbl(lblTotalInsuranceAmt.Text)

                If rbtnTaxCalAutomatic.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                ElseIf rbtnTaxCalManual.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
                End If
                obj.is_Excise_On_Qty = chkExciseOnQty.Checked
                obj.IsAbatementPO = IsAbatementPO


                obj.Total_Accepted_Amount = clsCommon.myCdbl(lblAcceptedAmt.Text)
                obj.Total_Rejected_Amount = clsCommon.myCdbl(lblRejectedAmt.Text)
                obj.Total_Shortage_Amount = clsCommon.myCdbl(lblShortageAmt.Text)
                obj.Total_Leak_Amount = clsCommon.myCdbl(lblLeakAmt.Text)
                obj.Total_Burst_Amount = clsCommon.myCdbl(lblBurstAmt.Text)
                obj.Is_Shortage_Include_In_Landed_Cost = chkShorategeIncludeInLandedCost.Checked



                obj.Arr = New List(Of clsSRNDetail)
                Dim ii As Integer = 1 ''TEC/12/08/19-000990 by balwinder on 14/08/2019
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsSRNDetail()
                    'done by stuti n 20/10/2016 against purchase points
                    objTr.Category = clsCommon.myCstr(grow.Cells(colCategoryType).Value)
                    objTr.Emergency = CInt(clsCommon.myCdbl(grow.Cells(colEmergency).Value))
                    objTr.Capex_Code = clsCommon.myCstr(grow.Cells(colCapexCode).Value)
                    objTr.Capex_SubCode = clsCommon.myCstr(grow.Cells(colCapexSubCode).Value)


                    objTr.Row_Type = clsCommon.myCstr(grow.Cells(colRowType).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.UOMWeight = clsCommon.myCstr(grow.Cells(colUOMWEIGHT).Value)
                    objTr.UOMWeightValue = clsCommon.myCdbl(grow.Cells(colUOMWeightValue).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.Bar_Code = clsCommon.myCstr(grow.Cells(colBarCode).Value)
                    objTr.SRN_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Rejected_Qty = clsCommon.myCdbl(grow.Cells(colRejectedQty).Value)
                    objTr.Freeqty = clsCommon.myCdbl(grow.Cells(colFreeQty).Value)

                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    'If clsCommon.myLen(clsCommon.myCstr(cmbRGPType.SelectedValue)) > 0 Then
                    '    objTr.RGP_Id = clsCommon.myCstr(grow.Cells(colPONo).Value)
                    'Else
                    objTr.PO_ID = clsCommon.myCstr(grow.Cells(colPONo).Value)
                    ' End If
                    objTr.arrBatchItem = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventory))
                    If is_Load_MRN Then
                        objTr.MRN_Id = clsCommon.myCstr(grow.Cells(colMRN_NO).Value)
                        objTr.GRN_ID = clsCommon.myCstr(grow.Cells(colGRN_NO).Value)
                        If clsCommon.myLen(objTr.MRN_Id) > 0 AndAlso clsCommon.myLen(obj.Against_MRN) <= 0 Then
                            obj.Against_MRN = objTr.MRN_Id
                        End If

                    End If
                    objTr.RGP_Id = clsCommon.myCstr(grow.Cells(colRGPNo).Value)
                    objTr.Req_No = clsCommon.myCstr(grow.Cells(colReqistionNo).Value)
                    'objTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Disc_Type = clsCommon.myCdbl(grow.Cells(colDisType).Value)

                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                        objTr.Disc_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Tag)
                        objTr.Disc_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Value)
                    Else
                        objTr.Disc_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Value)
                    End If
                    objTr.Header_Discount_Per = clsCommon.myCdbl(grow.Cells(colHeaderDiscountPer).Value)
                    objTr.Header_Discount_Amount = clsCommon.myCdbl(grow.Cells(colHeaderDiscountAmt).Value)
                    objTr.Detail_Discount_Amount = clsCommon.myCdbl(grow.Cells(colDetailDisAmt).Value)

                    objTr.Disc_Amt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                    objTr.Amt_Less_Discount = clsCommon.myCdbl(grow.Cells(colAmtAfterDis).Value)


                    objTr.Item_Insurance_Base_Amt = clsCommon.myCdbl(grow.Cells(colItemInsuranceBaseAmt).Value)
                    objTr.Item_Insurance_Apply_On = clsCommon.myCstr(grow.Cells(colItemInsuranceApplyOn).Value)
                    objTr.Item_Insurance_Rate = clsCommon.myCdbl(grow.Cells(colItemInsurancePer).Value)
                    objTr.Item_Insurance_Amt = clsCommon.myCdbl(grow.Cells(colItemInsuranceAmt).Value)
                    objTr.Item_Amt_After_Insurance = clsCommon.myCdbl(grow.Cells(colItemAmtAfterInsurance).Value)


                    objTr.Taxable_Amount = clsCommon.myCdbl(grow.Cells(colTaxableAmount).Value)
                    objTr.Taxable_Amount_Per = clsCommon.myCdbl(grow.Cells(colTaxableAmountPer).Value)

                    objTr.TAX1 = clsCommon.myCstr(grow.Cells(colTax1).Value)
                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt1).Value)
                    objTr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate1).Value)
                    objTr.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt1).Value)
                    objTr.TAX2 = clsCommon.myCstr(grow.Cells(colTax2).Value)
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt2).Value)
                    objTr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate2).Value)
                    objTr.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt2).Value)
                    objTr.TAX3 = clsCommon.myCstr(grow.Cells(colTax3).Value)
                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt3).Value)
                    objTr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate3).Value)
                    objTr.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt3).Value)
                    objTr.TAX4 = clsCommon.myCstr(grow.Cells(colTax4).Value)
                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt4).Value)
                    objTr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate4).Value)
                    objTr.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt4).Value)
                    objTr.TAX5 = clsCommon.myCstr(grow.Cells(colTax5).Value)
                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt5).Value)
                    objTr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate5).Value)
                    objTr.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt5).Value)
                    objTr.TAX6 = clsCommon.myCstr(grow.Cells(colTax6).Value)
                    objTr.TAX6_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt6).Value)
                    objTr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate6).Value)
                    objTr.TAX6_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt6).Value)
                    objTr.TAX7 = clsCommon.myCstr(grow.Cells(colTax7).Value)
                    objTr.TAX7_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt7).Value)
                    objTr.TAX7_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate7).Value)
                    objTr.TAX7_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt7).Value)
                    objTr.TAX8 = clsCommon.myCstr(grow.Cells(colTax8).Value)
                    objTr.TAX8_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt8).Value)
                    objTr.TAX8_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate8).Value)
                    objTr.TAX8_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt8).Value)
                    objTr.TAX9 = clsCommon.myCstr(grow.Cells(colTax9).Value)
                    objTr.TAX9_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt9).Value)
                    objTr.TAX9_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate9).Value)
                    objTr.TAX9_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt9).Value)
                    objTr.TAX10 = clsCommon.myCstr(grow.Cells(colTax10).Value)
                    objTr.TAX10_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt10).Value)
                    objTr.TAX10_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate10).Value)
                    objTr.TAX10_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt10).Value)
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                    objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)
                    objTr.Location = txtBillToLocation.Value 'clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                    objTr.Landed_Cost_Rate = clsCommon.myCdbl(grow.Cells(colLandedRate).Value)
                    objTr.Landed_Cost_Amount = clsCommon.myCdbl(grow.Cells(colLandedAmt).Value)

                    objTr.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                    ''objTr.Assessable = clsCommon.myCdbl(grow.Cells(colAssessableRate).Value)
                    objTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
                    objTr.Batch_No = clsCommon.myCstr(grow.Cells(colBatchNo).Value)
                    objTr.Bin_No = clsCommon.myCstr(grow.Cells(colBinNo).Value)
                    If clsCommon.myLen(grow.Cells(colExpiry).Value) > 0 Then
                        objTr.Expiry_Date = clsCommon.myCDate(grow.Cells(colExpiry).Value, "dd-MM-yyyy")
                    End If
                    If clsCommon.myLen(grow.Cells(colManufactureDate).Value) > 0 Then
                        objTr.MFG_Date = clsCommon.myCDate(grow.Cells(colManufactureDate).Value)
                    End If
                    objTr.Specification = clsCommon.myCstr(grow.Cells(colSpecification).Value)
                    objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    objTr.Is_Mannual_Amt = clsCommon.myCdbl(grow.Cells(colIsMannualAmt).Value)
                    objTr.Fater_Code = clsCommon.myCstr(grow.Cells(colFCode).Value)
                    objTr.Fater_Rate = clsCommon.myCdbl(grow.Cells(colFRate).Value)
                    objTr.Fater_Amt = clsCommon.myCdbl(grow.Cells(colFAmt).Value)


                    objTr.Leak_Qty = clsCommon.myCdbl(grow.Cells(colLeakQty).Value)
                    objTr.Burst_Qty = clsCommon.myCdbl(grow.Cells(colBurstQty).Value)
                    objTr.Short_Qty = clsCommon.myCdbl(grow.Cells(colShortQty).Value)
                    objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colOrgGRNQty).Value) - clsCommon.myCdbl(grow.Cells(colQty).Value) ''+ clsCommon.myCdbl(grow.Cells(colLeakQty).Value) + clsCommon.myCdbl(grow.Cells(colBurstQty).Value) + clsCommon.myCdbl(grow.Cells(colShortQty).Value)


                    objTr.PO_Qty = clsCommon.myCdbl(grow.Cells(colOrgPOQty).Value)
                    objTr.GRN_Qty = clsCommon.myCdbl(grow.Cells(colOrgGRNQty).Value)
                    objTr.MRN_Qty = clsCommon.myCdbl(grow.Cells(colOrgMRNQty).Value)

                    objTr.RGP_Qty = clsCommon.myCdbl(grow.Cells(colOrgRGPQty).Value)
                    objTr.Schedule_Qty = clsCommon.myCdbl(grow.Cells(colOrgSchQty).Value)
                    objTr.Against_Schedule_Code = clsCommon.myCstr(grow.Cells(colScheduleNo).Value)

                    objTr.Total_AddtionalCost_PerUnit = clsCommon.myCdbl(grow.Cells(colUnitTotAddCost).Value)
                    objTr.Total_NonRecTax_PerUnit = clsCommon.myCdbl(grow.Cells(colUnitTotNonRecTax).Value)
                    objTr.Total_RecTax_PerUnit = clsCommon.myCdbl(grow.Cells(colUnitTotRecTax).Value)

                    objTr.Accepted_Amount = clsCommon.myCdbl(grow.Cells(colAcceptedAmount).Value)
                    objTr.Rejected_Amount = clsCommon.myCdbl(grow.Cells(colRejectedAmount).Value)
                    objTr.Shortage_Amount = clsCommon.myCdbl(grow.Cells(colShortageAmount).Value)
                    objTr.Leak_Amount = clsCommon.myCdbl(grow.Cells(colLeakAmount).Value)
                    objTr.Burst_Amount = clsCommon.myCdbl(grow.Cells(colBurstAmount).Value)
                    objTr.Amt_Less_Discount_Without_Shortage = clsCommon.myCdbl(grow.Cells(colAmtLessDiscountWithoutShortage).Value)

                    ''-----------------19/10/2016---------additional charge itemwise------------------------------------------
                    objTr.ItemAdd_Charge_Code1 = clsCommon.myCstr(grow.Cells(colItemACCode1).Value)
                    objTr.ItemAdd_Charge_Code2 = clsCommon.myCstr(grow.Cells(colItemACCode2).Value)
                    objTr.ItemAdd_Charge_Code3 = clsCommon.myCstr(grow.Cells(colItemACCode3).Value)
                    objTr.ItemAdd_Charge_Code4 = clsCommon.myCstr(grow.Cells(colItemACCode4).Value)
                    objTr.ItemAdd_Charge_Code5 = clsCommon.myCstr(grow.Cells(colItemACCode5).Value)
                    objTr.ItemAdd_Charge_Code6 = clsCommon.myCstr(grow.Cells(colItemACCode6).Value)
                    objTr.ItemAdd_Charge_Code7 = clsCommon.myCstr(grow.Cells(colItemACCode7).Value)
                    objTr.ItemAdd_Charge_Code8 = clsCommon.myCstr(grow.Cells(colItemACCode8).Value)
                    objTr.ItemAdd_Charge_Code9 = clsCommon.myCstr(grow.Cells(colItemACCode9).Value)
                    objTr.ItemAdd_Charge_Code10 = clsCommon.myCstr(grow.Cells(colItemACCode10).Value)
                    objTr.ItemAdd_Calc_Charge_Amt1 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount1).Value)
                    objTr.ItemAdd_Calc_Charge_Amt2 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount2).Value)
                    objTr.ItemAdd_Calc_Charge_Amt3 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount3).Value)
                    objTr.ItemAdd_Calc_Charge_Amt4 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount4).Value)
                    objTr.ItemAdd_Calc_Charge_Amt5 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount5).Value)
                    objTr.ItemAdd_Calc_Charge_Amt6 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount6).Value)
                    objTr.ItemAdd_Calc_Charge_Amt7 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount7).Value)
                    objTr.ItemAdd_Calc_Charge_Amt8 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount8).Value)
                    objTr.ItemAdd_Calc_Charge_Amt9 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount9).Value)
                    objTr.ItemAdd_Calc_Charge_Amt10 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount10).Value)
                    objTr.ItemAdd_Org_Charge_Amt1 = clsCommon.myCdbl(grow.Cells(colItemACAmount1).Value)
                    objTr.ItemAdd_Org_Charge_Amt2 = clsCommon.myCdbl(grow.Cells(colItemACAmount2).Value)
                    objTr.ItemAdd_Org_Charge_Amt3 = clsCommon.myCdbl(grow.Cells(colItemACAmount3).Value)
                    objTr.ItemAdd_Org_Charge_Amt4 = clsCommon.myCdbl(grow.Cells(colItemACAmount4).Value)
                    objTr.ItemAdd_Org_Charge_Amt5 = clsCommon.myCdbl(grow.Cells(colItemACAmount5).Value)
                    objTr.ItemAdd_Org_Charge_Amt6 = clsCommon.myCdbl(grow.Cells(colItemACAmount6).Value)
                    objTr.ItemAdd_Org_Charge_Amt7 = clsCommon.myCdbl(grow.Cells(colItemACAmount7).Value)
                    objTr.ItemAdd_Org_Charge_Amt8 = clsCommon.myCdbl(grow.Cells(colItemACAmount8).Value)
                    objTr.ItemAdd_Org_Charge_Amt9 = clsCommon.myCdbl(grow.Cells(colItemACAmount9).Value)
                    objTr.ItemAdd_Org_Charge_Amt10 = clsCommon.myCdbl(grow.Cells(colItemACAmount10).Value)
                    objTr.Total_ItemAdd_Charge = clsCommon.myCdbl(grow.Cells(colItemTotalAdditionalCharge).Value)
                    ''=======================================================================================
                    objTr.Against_Item_Wise_Tax_Rate = clsCommon.myCstr(grow.Cells(colAgainstItemWiseTaxCode).Value)

                    objTr.Insurance_Base_Amt = clsCommon.myCdbl(grow.Cells(colInsuranceBaseAmt).Value)
                    objTr.Insurance_Per = clsCommon.myCdbl(grow.Cells(colInsurancePer).Value)


                    objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        objTr.Line_No = ii ''BHA/28/06/19-000915 by balwinder on 13/08/2019
                        ii += 1
                        obj.Arr.Add(objTr)
                    End If
                    If IsAbatementPO Then
                        objTr.AbatementRate = clsCommon.myCdbl(grow.Cells(colAbatementRate).Value)
                        objTr.AssessableMRP = clsCommon.myCdbl(grow.Cells(colAssesableMRP).Value)
                        objTr.TotalAssessableMRP = clsCommon.myCdbl(grow.Cells(colTotalAssesableMRP).Value)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return
                End If

                If clsCommon.myLen(obj.Against_MRN) > 0 Then
                    obj.Against_GRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_GRN FROM TSPL_MRN_HEAD WHERE MRN_No='" + obj.Against_MRN + "' and isnull(TSPL_MRN_HEAD.iscancel,0)=0"))
                End If
                If clsCommon.myLen(obj.Against_GRN) > 0 Then
                    obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_PO FROM TSPL_GRN_HEAD WHERE GRN_No='" + obj.Against_GRN + "' and isnull(TSPL_GRN_HEAD.iscancel,0)=0"))
                    obj.Against_Schedule_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Schedule_Code FROM TSPL_GRN_HEAD WHERE GRN_No='" + obj.Against_GRN + "' and isnull(TSPL_GRN_HEAD.iscancel,0)=0"))
                    obj.Against_RGP = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_RGP_No FROM TSPL_GRN_HEAD WHERE GRN_No='" + obj.Against_GRN + "' and isnull(TSPL_GRN_HEAD.iscancel,0)=0"))
                End If
                If clsCommon.myLen(obj.Against_RGP) > 0 Then
                    obj.Against_Schedule_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Schedule_Code FROM TSPL_RGP_HEAD WHERE RGP_No='" + obj.Against_RGP + "'"))
                    obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PO_Id FROM TSPL_RGP_HEAD WHERE RGP_No='" + obj.Against_RGP + "'"))
                End If
                If clsCommon.myLen(obj.Against_Schedule_Code) > 0 Then
                    obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PO_Code FROM TSPL_PO_SCH_HEAD WHERE document_code='" + obj.Against_Schedule_Code + "'"))
                End If

                If clsCommon.myLen(obj.Against_PO) > 0 Then
                    obj.Against_Requisition = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PurchaseOrder_No='" + obj.Against_PO + "' and isnull(TSPL_PURCHASE_ORDER_HEAD.ISCANCEL,0)=0"))
                Else
                    obj.Against_Requisition = txtRequistionNo.Value
                End If

                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colICode)
                End If
                ''End of For Custom Fields

                '' CurrencConversion
                If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
                    obj.CURRENCY_CODE = Me.txtCurrencyCode.Value
                    obj.ConvRate = clsCommon.myCdbl(Me.txtConversionRate.Text)
                    '' Added Date Format conversion Pankaj jha
                    If clsCommon.myLen(txtApplicableFrom.Text) > 0 Then
                        ' obj.ApplicableFrom = clsCommon.GetPrintDate(Me.txtApplicableFrom.Text, "dd/MMM/yyyy")
                        obj.ApplicableFrom = Me.txtApplicableFrom.Text
                    Else
                        obj.ApplicableFrom = Nothing
                    End If
                Else
                    obj.CURRENCY_CODE = Nothing
                    obj.ConvRate = 1
                    obj.ApplicableFrom = Nothing
                End If
                '' end CurrencyConversion
                obj.against_roadpermit = "0"
                obj.Arr_Road = New List(Of clsSRNRoadPermitDetail)
                If Chkroadpermit.Checked Then
                    obj.against_roadpermit = "1"

                    For Each grow As GridViewRowInfo In gv_roadpermit.Rows
                        Dim objtr As New clsSRNRoadPermitDetail()
                        objtr.roadpono = clsCommon.myCstr(IIf(is_Load_MRN, txtPONo.Tag, txtPONo.Value))
                        objtr.roadcode = clsCommon.myCstr(grow.Cells(colroadformcode).Value)
                        objtr.roadvendor = clsCommon.myCstr(txtVendorNo.Value)
                        objtr.roadissue_no = clsCommon.myCstr(grow.Cells(colroadformserialno).Value)
                        objtr.RoadpermitSRNNO = clsCommon.myCstr(txtDocNo.Value)

                        If clsCommon.myLen(objtr.roadcode) > 0 Then
                            obj.Arr_Road.Add(objtr)
                        End If
                    Next
                End If

                obj.agnst_cform = "0"
                obj.Arr_CFORM = New List(Of clsSRNCFORMDetail)
                If chk_c_form.Checked Then
                    obj.agnst_cform = "1"

                    For Each grow As GridViewRowInfo In gv_c_form.Rows
                        Dim objtr As New clsSRNCFORMDetail()
                        objtr.cformpono = clsCommon.myCstr(IIf(is_Load_MRN, txtPONo.Tag, txtPONo.Value))
                        objtr.cformcode = clsCommon.myCstr(grow.Cells(colCFormformcode).Value)
                        objtr.cformvendor = clsCommon.myCstr(txtVendorNo.Value)
                        objtr.cformissue_no = clsCommon.myCstr(grow.Cells(colCFormformserialno).Value)
                        objtr.cformSRNNO = clsCommon.myCstr(txtDocNo.Value)

                        If clsCommon.myLen(objtr.cformcode) > 0 Then
                            obj.Arr_CFORM.Add(objtr)
                        End If
                    Next
                End If
                obj.Arr_ACInsurance = New List(Of clsSRNAdditionChargeInsurance)
                For Each grow As GridViewRowInfo In gvACInsurance.Rows
                    Dim objtr As New clsSRNAdditionChargeInsurance()
                    objtr.AC_Code = clsCommon.myCstr(grow.Cells(colACInsuranceCode).Value)
                    objtr.Amount = clsCommon.myCdbl(grow.Cells(colACInsuranceAmount).Value)
                    If clsCommon.myLen(objtr.AC_Code) > 0 Then
                        obj.Arr_ACInsurance.Add(objtr)
                    End If
                Next
                If (obj.SaveData(obj, isNewEntry)) Then
                    If IsfromRGP Then 'clsUserMgtCode.mbtnGatePass = "PO-GP"
                        frmRGP.IsSRNSaved = "Y"
                        Return
                    End If

                    UcAttachment1.SaveData(obj.SRN_No)
                    If ChekPostBtn = False AndAlso RadButton1.Visible = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.SRN_No, NavigatorType.Current)
                End If
            Else
                Return
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As New clsSRNHead()
        Try
            BlankAllControls()
            LoadBlankGrid()
            LoadBlankGridTax()
            LoadBlankGridAC()
            LoadBlankGridACInsurance()

            cboItemType.Enabled = True
            txtBillToLocation.Enabled = True
            txtSubLocation.Enabled = False
            obj = clsSRNHead.GetData(strCode, NavTyep, IIf(clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal, "MT", "SRN"))
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.SRN_No) > 0) Then
                cboItemType.Enabled = False
                txtBillToLocation.Enabled = False
                isNewEntry = False
                isInsideLoadData = True
                btnSave.Enabled = True
                btnSave.Text = "Update"
                chk_qc_accepted.Enabled = False
                btnPost.Enabled = True
                btnDelete.Enabled = True
                cmbDocType.SelectedValue = obj.Document_Type
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    repoComplete.IsVisible = True
                    repoBalQty.IsVisible = True
                    btnUpdateRoadPermit.Enabled = True
                Else
                    btnUpdateRoadPermit.Enabled = False
                End If
                '-------------------------------------------------------
                'If obj.isreadytopost = "1" Then
                '    chkreadytopost.Checked = True
                'Else
                '    chkreadytopost.Checked = False
                'End If
                chkExemptSecurityDedution.Checked = IIf(obj.isExemptSecurityDedution = 1, True, False)
                chkJobWorkOutward.Checked = IIf(obj.isJobWorkOutward = 1, True, False)
                txtautosrnremarks.Text = obj.autosrnfromrgp
                txtQCcode.Text = obj.Against_QC_Code
                txtQcdate.Text = obj.Against_QC_Date
                '---------------------------------------------------------------------
                'stuti
                If obj.RoadPermit_Date IsNot Nothing AndAlso clsCommon.myLen(obj.RoadPermit_Date) > 0 AndAlso IsDate(obj.RoadPermit_Date) Then
                    txt_RoadPermitDate.Text = obj.RoadPermit_Date
                End If
                txt_RoadPermitNo.Text = obj.RoadPermit_No

                '=======end here=====

                chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(obj.Vendor_Code)
                UsLock1.Status = obj.Status
                If obj.Status = "1" Then
                    'If clsCommon.CompairString(clsUserMgtCode.FrmSRNMT, FORMTYPE) = CompairStringResult.Equal Then
                    btnCancel.Enabled = True
                    'Else
                    '    btnCancel.Visible = False
                    'End If
                Else
                    'If clsCommon.CompairString(clsUserMgtCode.FrmSRNMT, FORMTYPE) = CompairStringResult.Equal Then
                    btnCancel.Enabled = False
                    'Else
                    '    btnCancel.Visible = False
                    'End If

                End If
                txtDocNo.Value = obj.SRN_No
                txtDate.Value = obj.SRN_Date
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                txtRefNo.Text = obj.Ref_No
                txttender.Text = obj.TenderNo
                If clsCommon.myLen(obj.Challan_Date) > 0 Then
                    dtpChallan.Value = obj.Challan_Date
                    dtpChallan.Checked = True
                Else
                    dtpChallan.Checked = False
                End If
                If clsCommon.myLen(obj.Inv_Date) > 0 Then
                    dtpInvoice.Value = obj.Inv_Date
                    dtpInvoice.Checked = True
                Else
                    dtpInvoice.Checked = False
                End If
                txtRequistionNo.Value = obj.Against_Requisition
                chkRGPNonInventory.Checked = obj.is_RGP_Non_Inventory
                chk_qc_accepted.Checked = obj.is_QCAccepted
                chkOnHold.Checked = obj.On_Hold
                txtDesc.Text = obj.Description
                txtTaxGroup.Value = obj.Tax_Group
                txtRGPNo.Value = obj.Against_RGP
                txtComment.Text = obj.Comments
                txtShipToLocation.Value = obj.Ship_To_Location
                txtBillToLocation.Value = obj.Bill_To_Location
                txtSubLocation.Value = obj.Sublocation_Code
                lblSubLocation.Text = obj.SubLocationName
                txtInvNo.Text = obj.Inv_No
                chkConfirmatoryPO.Checked = obj.Confirmatory_PO
                cboSRNType.SelectedValue = obj.PurchaseOrder_Type
                txtScheduleNo.Value = obj.Against_Schedule_Code
                txtScheduleNo.Enabled = False
                If clsCommon.myLen(obj.PurchaseOrder_Type) > 0 Then
                    cboSRNType.Enabled = False
                End If
                cmbRGPType.SelectedValue = obj.RGP_Type

                lblAcceptedAmt.Text = clsCommon.myFormat(obj.Total_Accepted_Amount)
                lblRejectedAmt.Text = clsCommon.myFormat(obj.Total_Rejected_Amount)
                lblShortageAmt.Text = clsCommon.myFormat(obj.Total_Shortage_Amount)
                lblLeakAmt.Text = clsCommon.myFormat(obj.Total_Leak_Amount)
                lblBurstAmt.Text = clsCommon.myFormat(obj.Total_Burst_Amount)
                chkShorategeIncludeInLandedCost.Checked = obj.Is_Shortage_Include_In_Landed_Cost
                TxtRetention.Text = obj.Retention


                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If
                chkInternal.Checked = obj.Is_Internal
                cboItemType.SelectedValue = obj.Item_Type
                txtDept.Value = obj.Dept
                lblDept.Text = obj.Dept_Desc

                txtTermCode.Value = obj.Terms_Code
                'lblTermName.Text = obj.Terms_Description
                '' richa agarwal condition to check due date is in object or not
                If clsCommon.myLen(obj.Due_Date) > 0 Then
                    txtDueDate.Value = obj.Due_Date
                End If
                lblAmtWithDiscount.Text = clsCommon.myFormat(obj.Discount_Base)
                lblDiscountAmt.Text = clsCommon.myFormat(obj.Discount_Amt)
                lblAmtAfterDiscount.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
                lblTaxableAmount.Text = clsCommon.myFormat(obj.Total_Taxable_Amount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                lblTotRAmt.Text = clsCommon.myFormat(obj.SRN_Total_Amt)
                lblAmount.Text = lblTotRAmt.Text
                lblBillToLocation.Text = obj.BillToLocationName

                lblTaxGrpName.Text = obj.TaxGroupName
                lblTermName.Text = obj.TermsName

                txtCarrier.Text = obj.Carrier
                txtVehicleNo.Text = obj.VehicleNo
                txtGRNo.Text = obj.GRNo

                If obj.GRN_Date IsNot Nothing Then
                    Grndate.Text = obj.GRN_Date
                End If
                txtGENo.Text = obj.GENo
                If obj.GEDate.HasValue Then
                    txtGEDate.Value = obj.GEDate
                    txtGEDate.Checked = True
                End If
                lblLandedCost.Text = clsCommon.myFormat(obj.Landed_Add_Cost)
                lblAmtAfterLandedCost.Text = clsCommon.myFormat(obj.Total_Landed_Cost)

                If is_Load_MRN Then
                    txtPONo.Tag = obj.Against_GRN
                    txtPONo.Value = obj.Against_GRN

                    isAgainstTender = clsPurchaseOrderHead.AgainstTender(obj.Against_GRN, 1, Nothing)
                Else
                    txtPONo.Value = obj.Against_PO
                End If
                If clsCommon.myLen(txtPONo.Value) > 0 Then
                    Me.txtCurrencyCode.Enabled = False
                    MakeColumnReadOnly(True)
                Else
                    Me.txtCurrencyCode.Enabled = True
                    MakeColumnReadOnly(False)
                End If
                txtForm38.Text = obj.Form_38
                fndProject.Value = obj.PROJECT_ID
                lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
                If clsCommon.myLen(IIf(is_Load_MRN, txtPONo.Tag, txtPONo.Value)) > 0 Then
                    lblProject.Enabled = False
                    fndProject.Enabled = False
                End If
                If clsCommon.myLen(obj.RMDA_No) > 0 Then
                    lblRMDANo.Text = "RMDA No: " + obj.RMDA_No.Trim() + " Date: " + obj.RMDA_Date.Trim()
                End If

                gv2.Rows.Clear()
                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX1
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX1_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX1_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX1_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.AssessableAmt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX1) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX2) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX2
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX2_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX2_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX2_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX2_Base_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX2) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX3) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX3
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX3_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX3_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX3_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX3_Base_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX3) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX4) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX4
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX4_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX4_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX4_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX4_Base_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX4) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX5) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX5
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX5_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX5_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX5_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX5_Base_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX5) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX6) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX6
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX6_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX6_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX6_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX6_Base_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX6) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX7) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX7
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX7_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX7_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX7_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX7_Base_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX7) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX8) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX8
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX8_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX8_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX8_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX8_Base_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX8) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX9) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX9
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX9_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX9_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX9_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX9_Base_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX9) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX10) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX10
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX10_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX10_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX10_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX10_Base_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX10) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If


                gvAC.Rows.Clear()
                If (clsCommon.myLen(obj.Add_Charge_Code1) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt1
                Else
                    gvAC.Rows.AddNew()
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code2) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt2
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code3) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt3
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code4) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt4
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code5) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt5
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code6) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt6
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code7) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt7
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code8) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt8
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code9) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt9
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code10) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt10
                End If

                lblAddCharges.Text = clsCommon.myFormat(obj.Total_Add_Charge)
                lblAddCharges1.Text = clsCommon.myFormat(obj.Total_Add_Charge)
                If obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                    rbtnTaxCalAutomatic.IsChecked = True
                ElseIf obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                    rbtnTaxCalManual.IsChecked = True
                End If
                chkExciseOnQty.Checked = obj.is_Excise_On_Qty

                lblAddChargesForInsurance.Text = clsCommon.myFormat(obj.Total_Add_Charge_Insurance)
                lblAddChargesForInsurance1.Text = clsCommon.myFormat(obj.Total_Add_Charge_Insurance)
                lblTotalInsuranceAmt.Text = clsCommon.myFormat(obj.Total_Item_Insurance_Amt)


                gv1.Rows.Clear()
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsSRNDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = objTr.Category
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = CInt(objTr.Emergency)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = objTr.Capex_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = objTr.Capex_SubCode

                        gv1.Rows(gv1.Rows.Count - 1).Tag = objTr.arrSrItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Row_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComplete).Value = IIf(objTr.Status = 0, "No", "Yes")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUOMWEIGHT).Value = clsCommon.myCstr(objTr.UOMWeight)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUOMWeightValue).Value = clsCommon.myCdbl(objTr.UOMWeightValue)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc

                        If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(objTr.Item_Code, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsInsurance).Value = clsAdditionalCharge.GetIsInsurance(objTr.Item_Code, Nothing)
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objTr.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBarCode).Value = objTr.Bar_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgPOQty).Value = objTr.PO_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgGRNQty).Value = objTr.GRN_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgMRNQty).Value = objTr.MRN_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgRGPQty).Value = objTr.RGP_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSchQty).Value = objTr.Schedule_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colScheduleNo).Value = objTr.Against_Schedule_Code

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = objTr.Balance_Qty
                        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "RCDFCF") = CompairStringResult.Equal AndAlso objTr.SRN_Qty = 0 AndAlso obj.Status = ERPTransactionStatus.Pending Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.MRN_Qty
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.SRN_Qty
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectedQty).Value = objTr.Rejected_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFreeQty).Value = objTr.Freeqty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        'If clsCommon.myLen(clsCommon.myCstr(cmbRGPType.SelectedValue)) > 0 Then
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = objTr.RGP_Id
                        'Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = objTr.PO_ID
                        'End If

                        If is_Load_MRN Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRN_NO).Value = objTr.MRN_Id
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colGRN_NO).Value = objTr.GRN_ID
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = objTr.arrBatchItem

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = objTr.RGP_Id
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        '' Anubhooti 27-Oct-2014
                        If clsCommon.myLen(obj.Against_MRN) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRNUnitCost).Value = MRNItemRate(clsCommon.myCstr(objTr.MRN_Id), clsCommon.myCstr(objTr.Item_Code))
                        ElseIf clsCommon.myLen(obj.Against_PO) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRNUnitCost).Value = MRNItemRate(clsCommon.myCstr(objTr.PO_ID), clsCommon.myCstr(objTr.Item_Code))
                        End If
                        'If clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colMRNUnitCost).Value) > 0 Then
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = True
                        'Else
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = False
                        'End If
                        ''
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Location
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.LocationName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisType).Value = objTr.Disc_Type

                        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Tag = objTr.Disc_Per
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = objTr.Disc_Per
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = objTr.Disc_Per
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountPer).Value = objTr.Header_Discount_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountAmt).Value = objTr.Header_Discount_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDetailDisAmt).Value = objTr.Detail_Discount_Amount


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmt).Value = objTr.Disc_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterDis).Value = objTr.Amt_Less_Discount


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceBaseAmt).Value = objTr.Item_Insurance_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceApplyOn).Value = objTr.Item_Insurance_Apply_On
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsurancePer).Value = objTr.Item_Insurance_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceAmt).Value = objTr.Item_Insurance_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemAmtAfterInsurance).Value = objTr.Item_Amt_After_Insurance

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmount).Value = objTr.Taxable_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = objTr.Taxable_Amount_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax1).Value = objTr.TAX1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt1).Value = objTr.TAX1_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = objTr.TAX1_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = objTr.TAX1_Amt

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReqistionNo).Value = objTr.Req_No

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax2).Value = objTr.TAX2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt2).Value = objTr.TAX2_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = objTr.TAX2_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = objTr.TAX2_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax3).Value = objTr.TAX3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt3).Value = objTr.TAX3_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = objTr.TAX3_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = objTr.TAX3_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax4).Value = objTr.TAX4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt4).Value = objTr.TAX4_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = objTr.TAX4_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = objTr.TAX4_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax5).Value = objTr.TAX5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt5).Value = objTr.TAX5_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = objTr.TAX5_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = objTr.TAX5_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax6).Value = objTr.TAX6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt6).Value = objTr.TAX6_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = objTr.TAX6_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = objTr.TAX6_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax7).Value = objTr.TAX7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt7).Value = objTr.TAX7_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = objTr.TAX7_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = objTr.TAX7_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax8).Value = objTr.TAX8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt8).Value = objTr.TAX8_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = objTr.TAX8_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = objTr.TAX8_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax9).Value = objTr.TAX9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt9).Value = objTr.TAX9_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = objTr.TAX9_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = objTr.TAX9_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax10).Value = objTr.TAX10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt10).Value = objTr.TAX10_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = objTr.TAX10_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = objTr.TAX10_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = objTr.Total_Tax_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterTax).Value = objTr.Item_Net_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.MRP
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableRate).Value = objTr.Assessable
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableAmount).Value = objTr.AssessableAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = objTr.Batch_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = objTr.Bin_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLeakQty).Value = objTr.Leak_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBurstQty).Value = objTr.Burst_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShortQty).Value = objTr.Short_Qty
                        If objTr.Expiry_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiry).Value = objTr.Expiry_Date
                        End If

                        If objTr.MFG_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colManufactureDate).Value = objTr.MFG_Date
                        End If



                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFCode).Value = objTr.Fater_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFRate).Value = objTr.Fater_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFAmt).Value = objTr.Fater_Amt


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLandedRate).Value = objTr.Landed_Cost_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLandedAmt).Value = objTr.Landed_Cost_Amount



                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitTotNonRecTax).Value = objTr.Total_NonRecTax_PerUnit
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitTotRecTax).Value = objTr.Total_RecTax_PerUnit
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitTotAddCost).Value = objTr.Total_AddtionalCost_PerUnit



                        If clsCommon.myLen(objTr.Req_No) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsRequistionDetail.GetBalanceRequitionQty(objTr.Req_No, objTr.Item_Code, "", obj.SRN_No, False)
                        End If
                        If clsCommon.myLen(objTr.PO_ID) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsPurchaseOrderDetail.GetBalancePOQtyBySRN(objTr.PO_ID, objTr.Item_Code, obj.SRN_No, objTr.Unit_code, objTr.MRP, objTr.Assessable)
                        End If

                        If clsCommon.myLen(objTr.Against_Schedule_Code) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsPurchaseScheduleDetail.GetBalanceScheduleQty(objTr.Against_Schedule_Code, objTr.Item_Code, obj.SRN_No, obj.SRN_Date, objTr.Unit_code, True)
                        End If
                        If clsCommon.myLen(objTr.RGP_Id) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsRGPDetail.GetBalanceRGPQty(objTr.RGP_Id, objTr.Item_Code, obj.SRN_No, objTr.Unit_code)
                            If isRGPAfterPO Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsRGPDetail.GetBalanceRGPQty(objTr.RGP_Id, objTr.Item_Code, obj.SRN_No, objTr.Unit_code, True)
                            End If
                        End If


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsMannualAmt).Value = objTr.Is_Mannual_Amt

                        If obj.Status = ERPTransactionStatus.Pending Then
                            If clsCommon.myLen(obj.TAX1) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRecoverable1).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX1)
                            End If
                            If clsCommon.myLen(obj.TAX2) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRecoverable2).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX2)
                            End If
                            If clsCommon.myLen(obj.TAX3) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRecoverable3).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX3)
                            End If
                            If clsCommon.myLen(obj.TAX4) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRecoverable4).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX4)
                            End If
                            If clsCommon.myLen(obj.TAX5) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRecoverable5).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX5)
                            End If
                            If clsCommon.myLen(obj.TAX6) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRecoverable6).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX6)
                            End If
                            If clsCommon.myLen(obj.TAX7) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRecoverable7).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX7)
                            End If
                            If clsCommon.myLen(obj.TAX8) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRecoverable8).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX8)
                            End If
                            If clsCommon.myLen(obj.TAX9) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRecoverable9).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX9)
                            End If
                            If clsCommon.myLen(obj.TAX10) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRecoverable10).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX10)
                            End If
                        End If
                        '' abatement PO
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementRate).Value = objTr.AbatementRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssesableMRP).Value = objTr.AssessableMRP
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssesableMRP).Value = objTr.TotalAssessableMRP


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAcceptedAmount).Value = objTr.Accepted_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectedAmount).Value = objTr.Rejected_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShortageAmount).Value = objTr.Shortage_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLeakAmount).Value = objTr.Leak_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBurstAmount).Value = objTr.Burst_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtLessDiscountWithoutShortage).Value = objTr.Amt_Less_Discount_Without_Shortage

                        ''-----------------19/10/2016---------additional charge itemwise------------------------------------------
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode1).Value = objTr.ItemAdd_Charge_Code1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode2).Value = objTr.ItemAdd_Charge_Code2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode3).Value = objTr.ItemAdd_Charge_Code3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode4).Value = objTr.ItemAdd_Charge_Code4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode5).Value = objTr.ItemAdd_Charge_Code5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode6).Value = objTr.ItemAdd_Charge_Code6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode7).Value = objTr.ItemAdd_Charge_Code7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode8).Value = objTr.ItemAdd_Charge_Code8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode9).Value = objTr.ItemAdd_Charge_Code9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode10).Value = objTr.ItemAdd_Charge_Code10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount1).Value = objTr.ItemAdd_Calc_Charge_Amt1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount2).Value = objTr.ItemAdd_Calc_Charge_Amt2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount3).Value = objTr.ItemAdd_Calc_Charge_Amt3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount4).Value = objTr.ItemAdd_Calc_Charge_Amt4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount5).Value = objTr.ItemAdd_Calc_Charge_Amt5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount6).Value = objTr.ItemAdd_Calc_Charge_Amt6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount7).Value = objTr.ItemAdd_Calc_Charge_Amt7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount8).Value = objTr.ItemAdd_Calc_Charge_Amt8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount9).Value = objTr.ItemAdd_Calc_Charge_Amt9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount10).Value = objTr.ItemAdd_Calc_Charge_Amt10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount1).Value = objTr.ItemAdd_Org_Charge_Amt1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount2).Value = objTr.ItemAdd_Org_Charge_Amt2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount3).Value = objTr.ItemAdd_Org_Charge_Amt3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount4).Value = objTr.ItemAdd_Org_Charge_Amt4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount5).Value = objTr.ItemAdd_Org_Charge_Amt5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount6).Value = objTr.ItemAdd_Org_Charge_Amt6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount7).Value = objTr.ItemAdd_Org_Charge_Amt7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount8).Value = objTr.ItemAdd_Org_Charge_Amt8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount9).Value = objTr.ItemAdd_Org_Charge_Amt9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount10).Value = objTr.ItemAdd_Org_Charge_Amt10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTotalAdditionalCharge).Value = objTr.Total_ItemAdd_Charge
                        ''=======================================================================================
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAgainstItemWiseTaxCode).Value = objTr.Against_Item_Wise_Tax_Rate

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInsuranceBaseAmt).Value = objTr.Insurance_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInsurancePer).Value = objTr.Insurance_Per

                        If EnableRackBin Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBin).Value = clsDBFuncationality.getSingleValue("select Bin_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & objTr.Item_Code & "' and location='" & txtBillToLocation.Value & "'")
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRack).Value = clsDBFuncationality.getSingleValue("select Rack_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & objTr.Item_Code & "' and location='" & txtBillToLocation.Value & "'")
                        End If

                    Next
                    If clsCommon.myLen(clsCommon.myCstr(txtBillToLocation.Value)) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                            If chkJobWorkOutward.Checked = False Then
                                txtSubLocation.Enabled = True
                            End If
                        End If
                    End If
                    '--------------forms detail---------------------------------------------
                    gv_roadpermit.Rows.Clear()
                    gv_c_form.Rows.Clear()
                    If obj.against_roadpermit = "1" Then
                        Chkroadpermit.Checked = True
                        gv_roadpermit.Rows.Clear()

                        If obj.Arr_Road IsNot Nothing AndAlso obj.Arr_Road.Count > 0 Then
                            For Each objtr As clsSRNRoadPermitDetail In obj.Arr_Road
                                gv_roadpermit.Rows.AddNew()

                                gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colRoadsno).Value = clsCommon.myCstr(gv_roadpermit.Rows.Count)
                                gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colroadformcode).Value = clsCommon.myCstr(objtr.roadcode)
                                gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colroadformdesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select form_name from tspl_form_master where form_code='" + objtr.roadcode + "'"))
                                gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colroadformserialno).Value = clsCommon.myCstr(objtr.roadissue_no)
                                gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colroadformrem).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select remarks from tspl_form_master where form_code='" + objtr.roadcode + "'"))
                            Next
                        End If
                    Else
                        Chkroadpermit.Checked = False
                    End If

                    If obj.agnst_cform = "1" Then
                        chk_c_form.Checked = True
                        gv_c_form.Rows.Clear()

                        If obj.Arr_CFORM IsNot Nothing AndAlso obj.Arr_CFORM.Count > 0 Then
                            For Each objtr As clsSRNCFORMDetail In obj.Arr_CFORM
                                gv_c_form.Rows.AddNew()

                                gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormsno).Value = clsCommon.myCstr(gv_c_form.Rows.Count)
                                gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormformcode).Value = clsCommon.myCstr(objtr.cformcode)
                                gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormformdesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select form_name from tspl_form_master where form_code='" + objtr.cformcode + "'"))
                                gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormformserialno).Value = clsCommon.myCstr(objtr.cformissue_no)
                                gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormformrem).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select remarks from tspl_form_master where form_code='" + objtr.cformcode + "'"))
                            Next
                        End If
                    Else
                        chk_c_form.Checked = False
                    End If
                    '-------------------------------------------------------------------
                    lblShipToLocation.Text = clsLocation.GetName(txtShipToLocation.Value, Nothing)
                    If obj.Status = ERPTransactionStatus.Pending Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                        gvAC.Rows.AddNew()
                    End If
                End If
                SetitemWiseTaxOnlySetting()
                ' ''RefreshReqNo()
                If clsCommon.myLen(txtPONo.Value) > 0 Then
                    txtBillToLocation.Enabled = False
                    txtShipToLocation.Enabled = False
                    MakeColumnReadOnly(True)
                Else
                    MakeColumnReadOnly(False)
                End If
                ' ''RefreshGRPNo()
                If obj.Arr_ACInsurance IsNot Nothing AndAlso obj.Arr_ACInsurance.Count > 0 Then
                    For Each objtr As clsSRNAdditionChargeInsurance In obj.Arr_ACInsurance
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceCode).Value = objtr.AC_Code
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceName).Value = objtr.AC_Name
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceAmount).Value = objtr.Amount
                        gvACInsurance.Rows.AddNew()
                    Next
                End If
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.SRN_No)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.SRN_No, MyBase.Form_ID, gv1)
                ''End of For Custom Fields

                '' MULTICURRENCY
                Me.txtCurrencyCode.Value = obj.CURRENCY_CODE
                Me.txtConversionRate.Text = obj.ConvRate
                If clsCommon.myLen(obj.ApplicableFrom) > 0 Then
                    Me.txtApplicableFrom.Text = clsCommon.myCstr(clsCommon.GetPrintDate(obj.ApplicableFrom.ToString, "dd/MMM/yyyy"))
                End If

                '' end  MULTICURRENCY

                cmbRGPType.Enabled = False

                btnForm_Update.Enabled = False
                If UsLock1.Status = ERPTransactionStatus.Approved Then
                    btnForm_Update.Enabled = True
                End If
                UcAttachment1.LoadData(obj.SRN_No)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub
    Private Function MRNItemRate(ByVal strSRNNo As String, ByVal strItemCode As String) As Double
        Dim ItemRate As Double = 0
        If lblPoNo.Text = "MRN No" Then
            ItemRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select TSPL_MRN_DETAIL.Item_Cost  From TSPL_SRN_DETAIL Left Outer Join TSPL_MRN_DETAIL On TSPL_MRN_DETAIL.MRN_No = TSPL_SRN_DETAIL.MRN_Id " &
                  " where MRN_Id='" & strSRNNo & "' And TSPL_SRN_DETAIL.Item_Code ='" & strItemCode & "'"))
        Else
            ItemRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select TSPL_PURCHASE_ORDER_DETAIL.Item_Cost   From TSPL_SRN_DETAIL Left Outer Join TSPL_PURCHASE_ORDER_DETAIL  On TSPL_SRN_DETAIL.PO_ID   = TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No Where PO_ID ='" & strSRNNo & "' And TSPL_SRN_DETAIL.Item_Code ='" & strItemCode & "'"))
        End If

        Return ItemRate
    End Function
    Public Shared Function IsBatchDetailMandatory(ByVal strUOMCode As String) As Boolean
        Dim qry As String = "select 1 from TSPL_UNIT_MASTER where Unit_Code='" + strUOMCode + "' and Empty='Y'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return False
        End If
        Return True
    End Function
    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ''Dim arrVN As New List(Of String)()
        ''Dim strCode As String = ""
        ''Try
        ''    Dim qry As String = "select Voucher_No from TSPL_JOURNAL_MASTER order by Voucher_No"
        ''    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        ''    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        ''        For Each dr As DataRow In dt.Rows
        ''            Try
        ''                strCode = clsCommon.myCstr(dr("Voucher_No"))
        ''                qry = "select convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103) from TSPL_JOURNAL_MASTER  where Voucher_No='" + strCode + "'"
        ''                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        ''            Catch ex As Exception
        ''                arrVN.Add(strCode)
        ''            End Try


        ''        Next
        ''    End If
        ''Catch ex As Exception

        ''End Try

        'Dim dt As DataTable = clsDBFuncationality.GetDataTable("select top 10 * from TSPL_JOURNAL_MASTER")
        'For ii As Integer = 0 To dt.Columns.Count - 1
        '    Dim oldColumnName As String = dt.Columns(ii).ColumnName
        '    Dim NewColumnName As String = ""
        '    If oldColumnName.Contains("_") Then
        '        NewColumnName = oldColumnName.Replace("_", Environment.NewLine)
        '    End If

        '    If clsCommon.myLen(NewColumnName) > 0 Then
        '        dt.Columns(ii).ColumnName = NewColumnName
        '    End If
        'Next

        CloseForm()
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub
    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If rbtnTaxCalAutomatic.IsChecked AndAlso Not PurchaseModulePickFixTaxRate Then
                'Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                'Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FndVendfnddxRate", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='P'", "", "", True))
                Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(txtBillToLocation.Value, txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), txtVendorNo.Value, "P")

                Dim intRowNo As Integer = gv2.CurrentRow.Index
                If gv1.RowCount > 0 AndAlso intRowNo >= 0 Then
                    Dim strII As String = clsCommon.myCstr(intRowNo + 1)
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        gv1.Rows(ii).Cells("COLTAXRATE" + strII).Value = dblNewRate
                    Next
                End If
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    UpdateCurrentRow(ii)
                Next
                UpdateAllTotals()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If Not clsCommon.MyMessageBoxShow("Do you want to post this SRN No- " + clsCommon.myCstr(txtDocNo.Value) + "", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
                If (myMessages.postConfirm()) Then
                    If AllowToSave() Then
                        SaveData(True)
                        '' Anubhooti 12-Sep-2014 BM00000003735
                        If FrmMainTranScreen.ValidateTransactionAccToFinYear("Store Received Note", txtDate.Value) = False Then
                            Exit Sub
                        End If
                        ''
                        '------------------auto PO------------------------------------
                        If Allow_AutoPO = True AndAlso Not is_Load_MRN AndAlso clsCommon.myLen(txtPONo.Value) <= 0 AndAlso clsCommon.myLen(txtScheduleNo.Value) <= 0 AndAlso clsCommon.myLen(txtRGPNo.Value) <= 0 Then
                            If Not clsCommon.MyMessageBoxShow("Want auto Purchase Order entry for SRN No. " + clsCommon.myCstr(txtDocNo.Value) + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                            Else
                                If SaveData_AutoPO() Then
                                    clsCommon.MyMessageBoxShow(Me, "Auto PO Saved Successfully", Me.Text)
                                Else
                                    clsCommon.MyMessageBoxShow(Me, "No Data Saved For Auto PO,No SRN Post", Me.Text)
                                    Return
                                End If
                            End If
                        End If
                        '----------------------------------------------------------------
                        If (clsSRNHead.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
                            msg = "Successfully Posted"
                            Dim autoclose As Boolean = False
                            Dim SHOWGRN As Boolean = False
                            autoclose = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoClosePO, clsFixedParameterCode.AutoClosePO, Nothing)) = "1", True, False))
                            SHOWGRN = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowGRN, clsFixedParameterCode.ShowGRN, Nothing)) = "1", True, False))

                            If AutoClosePOBasedOnSRNQtyWithTolerance Then
                                For Each grow As GridViewRowInfo In gv1.Rows
                                    Dim pono As String = Nothing
                                    pono = clsCommon.myCstr(grow.Cells(colPONo).Value)
                                    If clsCommon.myLen(pono) > 0 AndAlso clsSRNHead.IsPOQtyReceivedWithTolerance(pono, Nothing) Then
                                        clsPurchaseOrderHead.closepodata(pono, True, "Y")
                                    End If
                                Next
                            ElseIf autoclose AndAlso Not SHOWGRN Then
                                For Each grow As GridViewRowInfo In gv1.Rows
                                    Dim pono As String = Nothing
                                    pono = clsCommon.myCstr(grow.Cells(colPONo).Value)
                                    If clsSRNHead.IsPOQtyRecv(pono, Nothing) Then
                                        clsPurchaseOrderHead.closepodata(pono, True, "Y")
                                    End If
                                Next
                            End If
                        Else
                            qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                            dt = clsDBFuncationality.GetDataTable(qry)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                Dim level As String = dt.Rows(0)("LEVEL").ToString()
                                Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                                If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                                    msg = "Level 1 Approval done. "
                                    If NoOflevel = 1 Then
                                        msg += "Successfully Posted. "
                                    Else
                                        msg += "Level 2 Approval Required."
                                    End If
                                ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                                    msg = "Level 2 Approval done. "
                                    If NoOflevel = 2 Then
                                        msg += "Successfully Posted "
                                    Else
                                        msg += "Level 3 Approval Required."
                                    End If
                                Else
                                    msg = "Level 3 Approval done. Successfully Posted. "
                                End If
                            End If
                        End If
                        common.clsCommon.MyMessageBoxShow(msg)



                        LoadData(txtDocNo.Value, NavigatorType.Current)

                        '========================================================
                        If clsSMSAtPost_Purchase.SMSATPOST_PUR() Then
                            SMSENDONLY(True)
                        End If
                        '=====================================================

                        If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                            PrintDataNew(txtDocNo.Value)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsSRNHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Private Sub gv1_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEditorInitialized
        If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Name", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub
    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_SRN_HEAD where SRN_No='" + txtDocNo.Value + "'"

            If clsCommon.CompairString(clsUserMgtCode.mbtnSRN, FORMTYPE) = CompairStringResult.Equal Then
                qst += " and isnull(TSPL_SRN_HEAD.Against_PO,'') not in ( Select TSPL_SRN_HEAD.Against_PO  from TSPL_SRN_HEAD left Outer Join TSPL_PURCHASE_ORDER_HEAD on TSPL_SRN_HEAD.Against_PO =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No where TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade =1) " ''initial cond. is wrong that show only against po srn, not all so do change in cond. add "not in" and pout "merchant=1"
            Else
                qst += " and TSPL_SRN_HEAD.Against_PO not in ( Select TSPL_SRN_HEAD.Against_PO  from TSPL_SRN_HEAD left Outer Join TSPL_PURCHASE_ORDER_HEAD on TSPL_SRN_HEAD.Against_PO =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No where TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade =0)  "
            End If
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        '' changed by shivani against Ticket no : BM00000008106
        '===================update by preeti gupta [Add column created by for Jakson Clinet]
        Dim qry As String = "select TSPL_SRN_HEAD.SRN_No as Code,Max(FORMAT(CAST(SRN_Date AS DATETIME),'dd/MM/yyyy hh:mm tt')) as Date,max(TSPL_SRN_DETAIL.Item_Code) as [Item Code],max(TSPL_SRN_DETAIL.Item_Desc) as [Item Name],max(TSPL_SRN_HEAD.Vendor_Code) as [Vendor Code],max( TSPL_SRN_HEAD.Vendor_Name) as Vendor,max(ISNULL(TSPL_VENDOR_MASTER.alies_name,'')) As [Alies Name],sum (SRN_Total_Amt) as Amount,max(case when TSPL_SRN_HEAD.Status='0' then 'Pending' else 'Approved' end) as [Status],max(Against_QC_Code) as [Against QC Code],max(Against_QC_Date) as [Against QC Date],max(TSPL_USER_MASTER.User_Name) as [User Name] "
        '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
        If is_Load_MRN Then
            qry += ",max(Against_MRN) as [Against MRN Code],max(against_grn) as [Against GRN Code] , max(TSPL_SRN_HEAD.VehicleNo) AS VehicleNO, max(TSPL_GRN_HEAD.GRN_Date) as GRN_date"
        Else
            qry += ",max(TSPL_SRN_HEAD.Against_PO) as [Against PO Code], TSPL_PURCHASE_ORDER_HEAD.ReferencePO as [Reference PO]  "
        End If
        qry += " ,TSPL_PURCHASE_ORDER_HEAD.RefTendorNo as TenderNo,max(TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code) as Weighment_Code,max(TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date) as Weighment_Date from TSPL_SRN_HEAD LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_SRN_HEAD.Vendor_Code left join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code =TSPL_SRN_HEAD.Created_By  left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = TSPL_SRN_HEAD.Against_PO left outer join TSPL_GRN_HEAD ON TSPL_GRN_HEAD.GRN_No=TSPL_SRN_HEAD.Against_GRN left outer join TSPL_QC_CHECK_HEAD On TSPL_QC_CHECK_HEAD.Document_Code=TSPL_SRN_HEAD.Against_QC_Code left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No  left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No
               and TSPL_SRN_DETAIL.Item_code Not In ('PM0002','PM0001')
                 "


        Dim whrClas As String = ""
        'Dim GridItem As String = ""
        'Dim row As GridViewRowInfo = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " TSPL_SRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and "
        End If
        If clsCommon.CompairString(clsUserMgtCode.mbtnSRN, FORMTYPE) = CompairStringResult.Equal Then
            whrClas += " isnull(TSPL_SRN_HEAD.Against_PO,'') not in ( Select TSPL_SRN_HEAD.Against_PO  from TSPL_SRN_HEAD left Outer Join TSPL_PURCHASE_ORDER_HEAD on TSPL_SRN_HEAD.Against_PO =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No where TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade =1) and"
        Else
            whrClas += " TSPL_SRN_HEAD.Against_PO not in ( Select TSPL_SRN_HEAD.Against_PO  from TSPL_SRN_HEAD left Outer Join TSPL_PURCHASE_ORDER_HEAD on TSPL_SRN_HEAD.Against_PO =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No where TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade =0)  and"
        End If
        whrClas += " isnull(TSPL_SRN_HEAD.Against_PO,'') not in ( Select TSPL_SRN_HEAD.Against_PO  from TSPL_SRN_HEAD 
                left Outer Join TSPL_PURCHASE_ORDER_HEAD on TSPL_SRN_HEAD.Against_PO =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No where TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade =1) 
                 and TSPL_SRN_DETAIL.Item_code Not In ('PM0002','PM0001')
                   Group by TSPL_SRN_HEAD.SRN_No, TSPL_PURCHASE_ORDER_HEAD.RefTendorNo"
        'If gvRGP.Rows.Count > 0 Then
        '    For Each row In gvRGP.Rows
        '        GridItem = clsCommon.myCstr(row.Cells(2).Value)
        '    Next
        '    If clsCommon.myLen(GridItem) > 0 Then
        '        whrClas += "and TSPL_SRN_DETAIL.Item_code Not In (" + GridItem + ")
        '        Group by TSPL_SRN_HEAD.SRN_No, TSPL_PURCHASE_ORDER_HEAD.RefTendorNo"
        '    End If
        'End If


        LoadData(clsCommon.ShowSelectForm("SRNCofnd", qry, "Code", whrClas, txtDocNo.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub
    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colUnit) Then
            isCellValueChangedOpen = True
            gv1.CurrentColumn = gv1.Columns(colIName)
            OpenUOMList(True)
            gv1.CurrentColumn = gv1.Columns(colUnit)
            setGridFocus()
            isCellValueChangedOpen = False
        End If
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colPONo).Value) <= 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colRGPNo).Value) <= 0 Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentColumn = gv1.Columns(colIName)
                OpenICodeList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
            setGridFocus()
            isCellValueChangedOpen = False

        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                            "TSPL_SRN_HEAD " + Environment.NewLine +
                            "TSPL_SRN_DETAIL " + Environment.NewLine +
                            "TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL " + Environment.NewLine +
                            "TSPL_CFORM_ISSUE_RECEIVE_DETAIL " + Environment.NewLine +
                            "Press Alt+P for Post Trasnaction " + Environment.NewLine +
                            "TSPL_MRN_DETAIL(Update balance Qty) " + Environment.NewLine +
                            "TSPL_INVENTORY_MOVEMENT " + Environment.NewLine +
                            "TSPL_RGP_BOM_DETAIL " + Environment.NewLine +
                            "TSPL_JOURNAL_MASTER " + Environment.NewLine +
                            "TSPL_JOURNAL_DETAILS")
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
                RadButton1.Visible = True
            End If
        End If
    End Sub
    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER"
        txtTermCode.Value = clsCommon.ShowSelectForm("POTermCodefndd", qry, "Code", "", txtTermCode.Value, "Code", isButtonClicked)
        SetTermDetails()


    End Sub
    Sub SetTermDetails()
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER where Terms_Code='" + txtTermCode.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
            txtDueDate.Value = txtDate.Value.AddDays(clsCommon.myCdbl(dt.Rows(0)("No Of Days")))
        Else
            lblTermName.Text = ""
        End If
    End Sub
    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        'Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
        'txtTaxGroup.Value = clsCommon.ShowSelectForm("Pfndid", qry, "Code", "Tax_Group_Type='P'", txtTaxGroup.Value, "Code", isButtonClicked)
        txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(txtBillToLocation.Value, txtVendorNo.Value, "P", txtTaxGroup.Value, isButtonClicked)

        SetTaxDetails()

    End Sub
    Private Sub SetTax()
        txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txtBillToLocation.Value, txtVendorNo.Value, "P", txtDate.Value)
        lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfPurchaseType(txtTaxGroup.Value, Nothing)
        SetTaxDetails()
    End Sub
    Sub SetTaxDetails()
        LoadBlankGridTax()
        Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If (dt.Rows.Count > 10) Then
                MessageBox.Show("Can't Handle More than 10 Tax Types in a Group")
                Return
            End If
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))

            gv2.Rows.Clear()
            For Each dr As DataRow In dt.Rows
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dr("Tax_Code"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsCommon.myCstr(dr("Tax_Code_Desc"))
                If rbtnTaxCalAutomatic.IsChecked Then
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dr("TaxRate"))
                End If
            Next
            SetitemWiseTaxSetting(True, False)
        Else
            lblTaxGrpName.Text = ""
            For ii As Integer = 0 To gv1.Rows.Count - 1
                BlankTaxDetails(ii)
            Next
        End If

        For ii As Integer = 0 To gv1.Rows.Count - 1
            UpdateCurrentRow(ii)
        Next
        UpdateAllTotals()
    End Sub
    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        'Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", txtVendorNo.Value, txtBillToLocation.Value)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index, isChangeRate)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colICode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        If isChangeRate Then
                            If clsCommon.myCBool(gv1.CurrentRow.Cells(clsCommon.myCstr(colItemTaxable)).Value) AndAlso PurchaseModulePickFixTaxRate Then
                                Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr(colICode)).Value), txtTaxGroup.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value), txtDate.Value, "P")
                                If objTAXRate IsNot Nothing Then
                                    gv1.CurrentRow.Cells(clsCommon.myCstr(colAgainstItemWiseTaxCode)).Value = objTAXRate.HCODE
                                    gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = objTAXRate.TAX_Rate
                                End If
                            Else
                                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                        End If
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Else
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    BlankTaxDetails(intRowNo, isChangeRate)
                    If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colICode)) > 0 Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                If clsCommon.myCBool(gv1.Rows(intRowNo).Cells(clsCommon.myCstr(colItemTaxable)).Value) AndAlso PurchaseModulePickFixTaxRate Then
                                    Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr(colICode)).Value), txtTaxGroup.Value, clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value), txtDate.Value, "P")
                                    If objTAXRate IsNot Nothing Then
                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr(colAgainstItemWiseTaxCode)).Value = objTAXRate.HCODE
                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = objTAXRate.TAX_Rate
                                    End If
                                Else
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                            End If
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                            ii = ii + 1
                        Next
                    End If
                Next
            End If
        End If
    End Sub
    Sub SetitemWiseTaxOnlySetting()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colICode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Next
        End If
    End Sub
    Private Sub LoadDocumentType()
        cmbDocType.DataSource = Nothing
        Dim dt As DataTable = Nothing
        Dim qry As String = "select 'SRN' as Code,'Store Received Note' as Name union all select 'MT' as Code,'Merchant Trade' as Name"
        dt = clsDBFuncationality.GetDataTable(qry)
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.mbtnSRN) = CompairStringResult.Equal Then
            dt.Rows.RemoveAt(1) 'mt
        ElseIf clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal Then
            dt.Rows.RemoveAt(0) 'SRN
        End If

        cmbDocType.DataSource = dt
        cmbDocType.ValueMember = "Code"
        cmbDocType.DisplayMember = "Name"
    End Sub
    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        Dim whrCls As String = " TSPL_VENDOR_MASTER.Status='N' and TSPL_VENDOR_MASTER.Form_Type<>'VSP'"
        If isRGPAfterPO AndAlso clsCommon.CompairString(cboSRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "AB") = CompairStringResult.Equal Then
            whrCls += " and tspl_vendor_master.vendor_code in (select vendor_code from tspl_rgp_head where Against_BOM='1' and Against_JobWork='1' and Status='1')"
        End If

        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Add1 as [Address 1],Add2 as [Address 2],Add3 as [Address 3],City_Code_Desc as City,State,Country  from TSPL_VENDOR_MASTER"
        txtVendorNo.Value = clsCommon.ShowSelectForm("POVFND", qry, "Code", whrCls, txtVendorNo.Value, "Code", isButtonClicked)
        ''lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'"))

        qry = "select  Vendor_Code,Vendor_Name,Terms_Code,Terms_Code_Desc ,Vendor_Account ,Tax_Group,Tax_Group_Desc from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtVendorNo.Value + "' and TSPL_VENDOR_MASTER.Form_Type<>'VSP'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Code_Desc"))
            txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(txtVendorNo.Value)
            SetMultiCurrencyVisibility()
        Else
            lblVendorName.Text = ""
            txtTermCode.Value = ""
            lblTermName.Text = ""
            txtTaxGroup.Value = ""
            lblTaxGrpName.Text = ""
            chkVendorGrossReceipt.Checked = False

            Me.txtCurrencyCode.Value = ""
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""
        End If
        SetTax()

        SetTermDetails()
        btnhistory.Enabled = True
    End Sub
    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBillToLocation._MYValidating
        'Dim obj As clsLocation = clsLocation.FinderForPhysicalLoaction(txtBillToLocation.Value, isButtonClicked)
        'If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
        '    txtBillToLocation.Value = obj.Code
        '    lblBillToLocation.Text = obj.Name
        'Else
        '    txtBillToLocation.Value = ""
        '    lblBillToLocation.Text = ""
        'End If
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String
            If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal Then
                WhrCls = " Location_Type='Virtual' "
            Else
                WhrCls = " (Location_Type='Physical' or Location_Type='WorkOrder')  "
            End If
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If


            txtBillToLocation.Value = clsCommon.ShowSelectForm("VendorMasteidfndr", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
                lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))

                If clsCommon.CompairString(clsFixedParameter.GetData("MilkProc", "EnableMilkProc", Nothing), "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterCode.MCCPurchase, clsFixedParameterType.MCCPurchase, Nothing), "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(IIf(is_Load_MRN, txtPONo.Tag, txtPONo.Value)) > 0 Then
                    GetStateCode()
                End If
            Else
                lblBillToLocation.Text = ""
            End If
            SetTax()

            If clsCommon.myLen(clsCommon.myCstr(txtBillToLocation.Value)) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                    txtSubLocation.Enabled = True
                Else
                    txtSubLocation.Enabled = False
                End If
                txtSubLocation.Value = ""
                lblSubLocation.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtShipToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipToLocation._MYValidating
        'Dim qry As String = "select Ship_To_Code as Code,Ship_To_Desc as Description from TSPL_SHIP_TO_LOCATION"
        'txtShipToLocation.Value = clsCommon.ShowSelectForm("POShindrlter", qry, "Code", "", txtShipToLocation.Value, "Code", isButtonClicked)
        'lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ship_To_Desc from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipToLocation.Value + "'"))
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtShipToLocation.Value = clsCommon.ShowSelectForm("BILLTOLOCPO", qry, "Code", WhrCls, txtShipToLocation.Value, "Code", isButtonClicked)
        lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtShipToLocation.Value + "'"))

    End Sub
    Private Sub btnRequistionItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SelectMRNItems()
    End Sub
    Sub SelectMRNItems()
        isInsideLoadData = True
        Dim frm As New frmPendingPO()
        frm.VendorCode = txtVendorNo.Value
        frm.strCurrCode = txtDocNo.Value
        frm.PurchaseOrder_Type = cboSRNType.SelectedValue
        frm.Is_From_RGP = True
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal Then
            frm.IsMerchantTrade = "MT"
        Else
            frm.IsMerchantTrade = "PO"
        End If
        frm.ShowDialog()
        LoadBlankGrid()
        Dim objMRNHead As clsPurchaseOrderHead = Nothing
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            If clsCommon.myLen(frm.ArrReturn(0).PurchaseOrder_No) > 0 Then
                objMRNHead = clsPurchaseOrderHead.GetData(frm.ArrReturn(0).PurchaseOrder_No, NavigatorType.Current)
                If objMRNHead IsNot Nothing AndAlso clsCommon.myLen(objMRNHead.PurchaseOrder_No) > 0 Then
                    IsAbatementPO = objMRNHead.IsAbatementPO
                    LoadBlankGrid()
                    If ShowCapexCodeandSubCode Then
                        MakeColumnReadOnly(True)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = objMRNHead.Category
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = CInt(objMRNHead.Emergency)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = objMRNHead.Capex_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = objMRNHead.Capex_SubCode
                    End If
                    If clsCommon.myLen(txtRefNo.Text) <= 0 Then
                        txtRefNo.Text = objMRNHead.Ref_No
                    End If
                    If clsCommon.myLen(txtDesc.Text) <= 0 Then
                        txtDesc.Text = objMRNHead.Description
                    End If

                    txtShipToLocation.Value = objMRNHead.Ship_To_Location
                    lblShipToLocation.Text = clsLocation.GetName(objMRNHead.Ship_To_Location, Nothing)
                    txtBillToLocation.Enabled = False
                    txtShipToLocation.Enabled = False
                    txtSubLocation.Value = objMRNHead.Sublocation_Code
                    lblSubLocation.Text = clsLocation.GetName(objMRNHead.Sublocation_Code, Nothing)
                    txtSubLocation.Enabled = False
                    TxtRetention.Text = objMRNHead.Retention
                    If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
                        cboItemType.SelectedValue = objMRNHead.Item_Type
                    End If
                    If (clsCommon.myLen(txtDept.Value) <= 0) Then
                        txtDept.Value = objMRNHead.Dept
                        lblDept.Text = objMRNHead.Dept_Desc
                    End If
                    txtBillToLocation.Value = objMRNHead.Bill_To_Location
                    lblBillToLocation.Text = objMRNHead.BillToLocationName
                    If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                        txtVendorNo.Value = frm.VendorCode
                        lblVendorName.Text = frm.VendorName
                        chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(frm.VendorCode)
                    End If
                    If (clsCommon.myLen(txtTermCode.Value) <= 0) Then
                        txtTermCode.Value = objMRNHead.Terms_Code
                        lblTermName.Text = objMRNHead.TermsName
                        txtDueDate.Value = objMRNHead.Due_Date
                    End If
                    If (clsCommon.myLen(fndProject.Value) <= 0) Then
                        fndProject.Value = objMRNHead.PROJECT_ID
                        lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
                        fndProject.Enabled = False
                        lblProject.Enabled = False
                    End If

                    txtRequistionNo.Value = objMRNHead.Against_Requisition

                    Me.txtCurrencyCode.Value = objMRNHead.CURRENCY_CODE
                    txtConversionRate.Text = objMRNHead.ConvRate
                    If objMRNHead.ApplicableFrom IsNot Nothing Then
                        Me.txtApplicableFrom.Text = objMRNHead.ApplicableFrom
                    End If
                    Me.txtCurrencyCode.Enabled = False
                    cboSRNType.SelectedValue = objMRNHead.PurchaseOrder_Type
                    gv_roadpermit.Rows.Clear()
                    gv_roadpermit.Rows.AddNew()
                    gv_c_form.Rows.Clear()
                    gv_c_form.Rows.AddNew()
                    If objMRNHead.roadpermit = "1" Then
                        Chkroadpermit.Checked = True
                        gv_roadpermit.Rows.Clear()
                        gv_roadpermit.Rows.AddNew()
                        If objMRNHead.Arr_Road IsNot Nothing AndAlso objMRNHead.Arr_Road.Count > 0 Then
                            For Each objtr As clsPurchaseOrderRoadDetail In objMRNHead.Arr_Road
                                gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colRoadsno).Value = clsCommon.myCstr(gv_roadpermit.Rows.Count)
                                gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colroadformcode).Value = clsCommon.myCstr(objtr.roadcode)
                                gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colroadformdesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select form_name from tspl_form_master where form_code='" + objtr.roadcode + "'"))
                                gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colroadformserialno).Value = clsCommon.myCstr(objtr.roadissue_no)
                                gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colroadformrem).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select remarks from tspl_form_master where form_code='" + objtr.roadcode + "'"))

                                gv_roadpermit.Rows.AddNew()
                            Next
                        End If
                    Else
                        Chkroadpermit.Checked = False
                    End If

                    If objMRNHead.Cform = "1" Then
                        chk_c_form.Checked = True
                        gv_c_form.Rows.Clear()
                        gv_c_form.Rows.AddNew()
                        '--------------CFORM grid fill------------------------------------------
                        If objMRNHead.Arr_CFORM IsNot Nothing AndAlso objMRNHead.Arr_CFORM.Count > 0 Then
                            For Each objtr As clsPurchaseOrderCFORMDetail In objMRNHead.Arr_CFORM
                                gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormsno).Value = clsCommon.myCstr(gv_c_form.Rows.Count)
                                gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormformcode).Value = clsCommon.myCstr(objtr.cformcode)
                                gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormformdesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select form_name from tspl_form_master where form_code='" + objtr.cformcode + "'"))
                                gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormformserialno).Value = clsCommon.myCstr(objtr.cformissue_no)
                                gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormformrem).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select remarks from tspl_form_master where form_code='" + objtr.cformcode + "'"))

                                gv_c_form.Rows.AddNew()
                            Next
                        End If
                    Else
                        chk_c_form.Checked = False
                    End If
                    '----------------------------------------------------------------------------

                    LoadBlankGridAC()
                    LoadBlankGridACInsurance()

                    If objMRNHead.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                        rbtnTaxCalAutomatic.IsChecked = True
                    ElseIf objMRNHead.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                        rbtnTaxCalManual.IsChecked = True
                    End If
                    chkExciseOnQty.Checked = objMRNHead.is_Excise_On_Qty
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code1) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code1
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name1
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt1
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code2) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code2
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name2
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt2
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code3) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code3
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name3
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt3
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code4) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code4
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name4
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt4
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code5) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code5
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name5
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt5
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code6) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code6
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name6
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt6
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code7) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code7
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name7
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt7
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code8) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code8
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name8
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt8
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code9) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code9
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name9
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt9
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code10) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code10
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name10
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt10
                    End If
                    gvAC.Rows.AddNew()
                End If
            End If
            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If


            Dim mrnno As String = ""

            For Each obj As clsPurchaseOrderDetail In frm.ArrReturn
                If IsValidItem(obj) Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = obj.PurchaseOrder_No
                    '  gv1.Rows(gv1.Rows.Count - 1).Cells(colMRN_NO).Value = obj.MRN_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = obj.Row_Type
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(obj.Item_Code)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(obj.Item_Code)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(obj.Item_Code)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRNUnitCost).Value = obj.Item_Cost
                    If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "F") = CompairStringResult.Equal Then
                        Dim strFCode As String = clsItemMaster.GetFatherCode(obj.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFCode).Value = strFCode
                        If clsCommon.myLen(strFCode) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFRate).Value = clsItemPriceMaster.GetMRPOfFinishItem(strFCode, obj.Unit_code)
                        End If
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgPOQty).Value = obj.PurchaseOrder_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = obj.Taxable_Amount_Per
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = obj.TAX1_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = obj.TAX2_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = obj.TAX3_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = obj.TAX4_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = obj.TAX5_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = obj.TAX6_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = obj.TAX7_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = obj.TAX8_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = obj.TAX9_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = obj.TAX10_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisType).Value = 0

                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Tag = obj.Disc_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = obj.Disc_Per
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = obj.Disc_Per
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = obj.Bin_No
                    If clsCommon.myLen(txtPONo.Value) > 0 Then
                        Dim qry1 As String = "Select Specification,Remarks  from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No ='" & txtPONo.Value & "' and Item_Code ='" & obj.Item_Code & "'"
                        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry1)

                        If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                            For Each dr As DataRow In dt2.Rows
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = clsCommon.myCstr(dr("Remarks"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = clsCommon.myCstr(dr("Specification"))
                            Next
                        End If
                        qry1 = Nothing
                        dt2 = Nothing
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(obj.Item_Code)
                    If rbtnTaxCalManual.IsChecked Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = obj.TAX1_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = obj.TAX2_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = obj.TAX3_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = obj.TAX4_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = obj.TAX5_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = obj.TAX6_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = obj.TAX7_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = obj.TAX8_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = obj.TAX9_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = obj.TAX10_Amt
                    End If
                End If
                mrnno = obj.PurchaseOrder_No
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementRate).Value = obj.AbatementRate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAssesableMRP).Value = obj.AssessableMRP
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssesableMRP).Value = obj.TotalAssessableMRP

                '' added by parteek for Item Rack Bin wise on 21/11/2017
                If EnableRackBin Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBin).Value = clsDBFuncationality.getSingleValue("select Bin_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & obj.Item_Code & "' and location='" & txtBillToLocation.Value & "'")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRack).Value = clsDBFuncationality.getSingleValue("select Rack_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & obj.Item_Code & "' and location='" & txtBillToLocation.Value & "'")
                End If
                '' End

            Next



            If objMRNHead.Arr IsNot Nothing AndAlso objMRNHead.Arr.Count > 0 Then
                For Each objTr As clsPurchaseOrderDetail In objMRNHead.Arr
                    If objTr.Row_Type = "Misc" Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = mrnno
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Row_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(objTr.Item_Code, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsInsurance).Value = clsAdditionalCharge.GetIsInsurance(objTr.Item_Code, Nothing)
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objTr.Item_Code, Nothing)

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = 0
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = objTr.TAX1_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = objTr.TAX2_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = objTr.TAX3_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = objTr.TAX4_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = objTr.TAX5_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = objTr.TAX6_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = objTr.TAX7_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = objTr.TAX8_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = objTr.TAX9_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = objTr.TAX10_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = objTr.Taxable_Amount_Per
                        If rbtnTaxCalManual.IsChecked Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = objTr.TAX1_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = objTr.TAX2_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = objTr.TAX3_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = objTr.TAX4_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = objTr.TAX5_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = objTr.TAX6_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = objTr.TAX7_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = objTr.TAX8_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = objTr.TAX9_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = objTr.TAX10_Amt
                        End If
                        '' added by parteek for Item Rack Bin wise on 21/11/2017
                        If EnableRackBin Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBin).Value = clsDBFuncationality.getSingleValue("select Bin_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & objTr.Item_Code & "' and location='" & txtBillToLocation.Value & "'")
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRack).Value = clsDBFuncationality.getSingleValue("select Rack_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & objTr.Item_Code & "' and location='" & txtBillToLocation.Value & "'")
                        End If
                        '' End
                    End If
                Next
            End If

            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem

            If rbtnTaxCalManual.IsChecked Then
                For ii As Integer = 1 To 10
                    If gv2.Rows.Count >= ii Then
                        Dim dblTotTaxAmt As Double = 0
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            dblTotTaxAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells("COLTAXAMT" + clsCommon.myCstr(ii)).Value)
                        Next
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = dblTotTaxAmt
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(gv1.Rows(0).Cells("COLTAXRATE" + clsCommon.myCstr(ii)).Value)
                    End If
                Next
            End If
            SetitemWiseTaxSetting(False, False)
            For ii As Integer = 0 To gv1.RowCount - 1
                UpdateCurrentRow(ii)
            Next
            If rbtnTaxCalManual.IsChecked Then
                For ii As Integer = 0 To gv1.RowCount - 1
                    UpdateCurrentRow(ii)
                Next
            End If
        End If
        isInsideLoadData = False
        UpdateAllTotals()
        RefreshReqNo()
        Dim desc As String = ""
        desc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.InvoiceBasedPO, clsFixedParameterCode.InvoiceBasedPO, Nothing))
        If clsCommon.CompairString(desc, "1") = CompairStringResult.Equal Then
            txtInvNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Isnull(SaleInvoiceNo,'') from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No='" + txtPONo.Value + "' "))
        End If
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(txtPONo.Value) > 0 Then
                gv1.Rows(ii).Cells(colRate).ReadOnly = True
            End If
        Next
    End Sub
    Sub SelectMRNItemsWithoutPO()
        isInsideLoadData = True
        Dim frm As New frmPendingPO()
        frm.VendorCode = txtVendorNo.Value
        frm.strCurrCode = txtDocNo.Value
        frm.Is_Load_MRN = is_Load_MRN
        frm.PurchaseOrder_Type = cboSRNType.SelectedValue
        frm.Is_From_RGP = False
        frm.IsItemInsuranceColumn = True
        frm.ShowDialog()
        LoadBlankGrid()
        Dim objMRNHead As clsMRNHead = Nothing
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            If clsCommon.myLen(frm.ArrReturn(0).MRN_No) > 0 Then
                objMRNHead = clsMRNHead.GetData(frm.ArrReturn(0).MRN_No, NavigatorType.Current)
                If objMRNHead IsNot Nothing AndAlso clsCommon.myLen(objMRNHead.MRN_No) > 0 Then
                    IsAbatementPO = False
                    LoadBlankGrid()
                    'stuti
                    If (clsCommon.myLen(objMRNHead.RoadPermit_No) > 0) Then
                        If objMRNHead.RoadPermit_Date IsNot Nothing AndAlso clsCommon.myLen(objMRNHead.RoadPermit_Date) > 0 AndAlso IsDate(objMRNHead.RoadPermit_Date) Then
                            txt_RoadPermitDate.Text = objMRNHead.RoadPermit_Date
                        End If
                        txt_RoadPermitNo.Text = objMRNHead.RoadPermit_No
                    End If
                    '=======end here=====
                    TxtRetention.Text = objMRNHead.Retention
                    chkJobWorkOutward.Checked = IIf(objMRNHead.isJobWorkOutward = 1, True, False)
                    If clsCommon.myLen(txtCarrier.Text) <= 0 Then
                        txtCarrier.Text = objMRNHead.Carrier
                    End If
                    If clsCommon.myLen(txtVehicleNo.Text) <= 0 Then
                        txtVehicleNo.Text = objMRNHead.VehicleNo
                    End If
                    If clsCommon.myLen(txtGRNo.Text) <= 0 Then
                        txtGRNo.Text = objMRNHead.GRNo
                    End If
                    If clsCommon.myLen(txtGENo.Text) <= 0 Then
                        txtGENo.Text = objMRNHead.GENo
                    End If
                    If txtGEDate.Checked = False AndAlso objMRNHead.GEDate.HasValue Then
                        txtGEDate.Checked = True
                        txtGEDate.Value = clsCommon.GetPrintDate(objMRNHead.GEDate.Value, "dd-MM-yyyy")
                    End If
                    If clsCommon.myLen(txtCarrier.Text) <= 0 Then
                        txtVehicleNo.Text = objMRNHead.VehicleNo
                    End If

                    cmbRGPType.SelectedValue = objMRNHead.RGP_Type
                    If clsCommon.myLen(txtRefNo.Text) <= 0 Then
                        txtRefNo.Text = objMRNHead.Ref_No
                    End If
                    If clsCommon.myLen(txtDesc.Text) <= 0 Then
                        txtDesc.Text = objMRNHead.Description
                    End If
                    If clsCommon.myLen(txtInvNo.Text) <= 0 Then
                        'txtRemarks.Text = objMRNHead.Remarks
                    End If

                    If clsCommon.myLen(objMRNHead.InvoiceNo) > 0 Then
                        txtInvNo.Text = objMRNHead.InvoiceNo
                        txtInvNo.ReadOnly = True
                    Else
                        txtInvNo.Text = ""
                        txtInvNo.ReadOnly = False
                    End If

                    If clsCommon.myLen(objMRNHead.InvoiceDate) > 0 Then
                        dtpInvoice.Value = objMRNHead.InvoiceDate
                        dtpInvoice.Checked = True
                        dtpInvoice.ReadOnly = True
                    Else
                        dtpInvoice.Checked = False
                        dtpInvoice.ReadOnly = False
                    End If

                    txtShipToLocation.Value = objMRNHead.Ship_To_Location
                    lblShipToLocation.Text = clsLocation.GetName(objMRNHead.Ship_To_Location, Nothing)
                    txtBillToLocation.Enabled = False
                    txtShipToLocation.Enabled = False
                    txtSubLocation.Value = objMRNHead.Sublocation_Code
                    lblSubLocation.Text = clsLocation.GetName(objMRNHead.Sublocation_Code, Nothing)
                    txtSubLocation.Enabled = False
                    If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
                        cboItemType.SelectedValue = objMRNHead.Item_Type
                    End If
                    If (clsCommon.myLen(txtDept.Value) <= 0) Then
                        txtDept.Value = objMRNHead.Dept
                        lblDept.Text = objMRNHead.Dept_Desc
                    End If
                    'If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
                    txtBillToLocation.Value = objMRNHead.Bill_To_Location
                    lblBillToLocation.Text = objMRNHead.BillToLocationName

                    If clsCommon.myLen(clsCommon.myCstr(txtBillToLocation.Value)) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                            If chkJobWorkOutward.Checked = False Then
                                txtSubLocation.Enabled = True
                            End If
                        End If
                    End If
                    'End If
                    If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                        txtVendorNo.Value = objMRNHead.Vendor_Code
                        lblVendorName.Text = objMRNHead.Vendor_Name
                        chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(frm.VendorCode)
                    End If
                    If (clsCommon.myLen(txtTermCode.Value) <= 0) Then
                        txtTermCode.Value = objMRNHead.Terms_Code
                        lblTermName.Text = objMRNHead.TermsName
                        txtDueDate.Value = objMRNHead.Due_Date
                    End If

                    cboSRNType.SelectedValue = objMRNHead.PurchaseOrder_Type
                    txtRequistionNo.Value = objMRNHead.Against_Requisition
                    Me.txtCurrencyCode.Value = objMRNHead.CURRENCY_CODE
                    txtConversionRate.Text = objMRNHead.ConvRate
                    If objMRNHead.ApplicableFrom IsNot Nothing Then
                        Me.txtApplicableFrom.Text = objMRNHead.ApplicableFrom
                    End If
                    Me.txtCurrencyCode.Enabled = False
                    '---------------------Forms Detail-----------------------------
                    gv_roadpermit.Rows.Clear()
                    gv_roadpermit.Rows.AddNew()
                    gv_c_form.Rows.Clear()
                    gv_c_form.Rows.AddNew()
                    Chkroadpermit.Checked = False
                    chk_c_form.Checked = False

                    If objMRNHead.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                        rbtnTaxCalAutomatic.IsChecked = True
                    ElseIf objMRNHead.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                        rbtnTaxCalManual.IsChecked = True
                    End If

                    LoadBlankGridAC()
                    LoadBlankGridACInsurance()
                    If objMRNHead.Arr_ACInsurance IsNot Nothing AndAlso objMRNHead.Arr_ACInsurance.Count > 0 Then
                        For Each objtr As clsMRNAdditionChargeInsurance In objMRNHead.Arr_ACInsurance
                            gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceCode).Value = objtr.AC_Code
                            gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceName).Value = objtr.AC_Name
                            gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceAmount).Value = objtr.Amount
                            gvACInsurance.Rows.AddNew()
                        Next
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code1) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code1
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name1
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt1
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code2) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code2
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name2
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt2
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code3) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code3
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name3
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt3
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code4) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code4
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name4
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt4
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code5) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code5
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name5
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt5
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code6) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code6
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name6
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt6
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code7) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code7
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name7
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt7
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code8) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code8
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name8
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt8
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code9) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code9
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name9
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt9
                    End If
                    If (clsCommon.myLen(objMRNHead.Add_Charge_Code10) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code10
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name10
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt10
                    End If
                    gvAC.Rows.AddNew()
                End If
            End If
            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If
            Dim mrnno As String = ""
            For Each obj As clsPurchaseOrderDetail In frm.ArrReturn
                If IsValidItem(obj) Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                    If ShowCapexCodeandSubCode Then
                        MakeColumnReadOnly(True)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = obj.Category
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = obj.Emergency
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = obj.Capex_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = obj.Capex_SubCode
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                    If clsCommon.myLen(clsCommon.myCstr(cmbRGPType.SelectedValue)) > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = obj.PurchaseOrder_No
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = obj.PurchaseOrder_No
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReqistionNo).Value = obj.Requisition_Id
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRN_NO).Value = obj.MRN_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGRN_NO).Value = obj.GRN_No_Temp
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = obj.Row_Type
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                    If clsCommon.CompairString(obj.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(obj.Item_Code, Nothing)
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = obj.Taxable_Amount_Per
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(obj.Item_Code)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(obj.Item_Code)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(obj.Item_Code)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRNUnitCost).Value = obj.Item_Cost
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = frmPendingPO.Load_discount_for_GRN(obj.PurchaseOrder_No, obj.Item_Code)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                    If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "F") = CompairStringResult.Equal Then
                        Dim strFCode As String = clsItemMaster.GetFatherCode(obj.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFCode).Value = strFCode
                        If clsCommon.myLen(strFCode) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFRate).Value = clsItemPriceMaster.GetMRPOfFinishItem(strFCode, obj.Unit_code)
                        End If
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgPOQty).Value = obj.PurchaseOrder_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = obj.TAX1_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = obj.TAX2_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = obj.TAX3_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = obj.TAX4_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = obj.TAX5_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = obj.TAX6_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = obj.TAX7_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = obj.TAX8_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = obj.TAX9_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = obj.TAX10_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisType).Value = 0
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Tag = obj.Disc_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = obj.Disc_Per
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = obj.Disc_Per
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountPer).Value = obj.Header_Discount_Per
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = obj.Bin_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(obj.Item_Code)
                    If rbtnTaxCalManual.IsChecked Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = obj.TAX1_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = obj.TAX2_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = obj.TAX3_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = obj.TAX4_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = obj.TAX5_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = obj.TAX6_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = obj.TAX7_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = obj.TAX8_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = obj.TAX9_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = obj.TAX10_Amt
                    End If
                    Dim dt As DataTable = clsSRNHead.GetOriginalQty(obj.GRN_No_Temp, obj.MRN_No, obj.PurchaseOrder_No, obj.Item_Code, obj.Unit_code, obj.Assessable, obj.MRP, Nothing)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgPOQty).Value = clsCommon.myCdbl(dt.Rows(0)("PurchaseOrder_Qty"))
                        If clsFixedParameter.GetData(clsFixedParameterType.ShowQtySum_in_GRN_MRN_SRN, clsFixedParameterCode.ShowQtySum_in_GRN_MRN_SRN, Nothing) = "1" Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgGRNQty).Value = clsCommon.myCdbl(dt.Compute("Sum(GRN_Qty)", "")) 'clsCommon.myCdbl(dt.Rows(0)("GRN_Qty"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgMRNQty).Value = clsCommon.myCdbl(dt.Compute("Sum(GRN_Qty)", "")) ' clsCommon.myCdbl(dt.Rows(0)("MRN_Qty"))
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgGRNQty).Value = clsCommon.myCdbl(dt.Rows(0)("GRN_Qty"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgMRNQty).Value = clsCommon.myCdbl(dt.Rows(0)("MRN_Qty"))
                        End If
                        If IsQCColumnRequiredonMRN Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dt.Rows(0)("accept_qty"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colShortQty).Value = clsCommon.myCdbl(dt.Rows(0)("Short_Qty"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectedQty).Value = clsCommon.myCdbl(dt.Rows(0)("Reject_qty"))
                        End If
                    End If
                End If
                mrnno = obj.PurchaseOrder_No
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementRate).Value = obj.AbatementRate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAssesableMRP).Value = obj.AssessableMRP
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssesableMRP).Value = obj.TotalAssessableMRP
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAgainstItemWiseTaxCode).Value = obj.Against_Item_Wise_Tax_Rate

                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceApplyOn).Value = obj.Item_Insurance_Apply_On
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsurancePer).Value = obj.Item_Insurance_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceAmt).Value = obj.Item_Insurance_Amt
            Next



            If objMRNHead.Arr IsNot Nothing AndAlso objMRNHead.Arr.Count > 0 Then
                For Each objTr As clsMRNDetail In objMRNHead.Arr
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If clsCommon.myCstr(grow.Cells(colMRN_NO).Value) = clsCommon.myCstr(objTr.MRN_No) And clsCommon.myCstr(grow.Cells(colICode).Value) = clsCommon.myCstr(objTr.Item_Code) Then
                            grow.Cells(colLeakQty).Value = objTr.Leak_Qty
                            grow.Cells(colBurstQty).Value = objTr.Burst_Qty
                        End If
                    Next
                    If objTr.Row_Type = "Misc" Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        If ShowCapexCodeandSubCode Then
                            MakeColumnReadOnly(True)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = objTr.Category
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = objTr.Emergency
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = objTr.Capex_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = objTr.Capex_SubCode
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = mrnno
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReqistionNo).Value = objTr.Requisition_Id
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRN_NO).Value = objTr.MRN_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Row_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc

                        If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(objTr.Item_Code, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsInsurance).Value = clsAdditionalCharge.GetIsInsurance(objTr.Item_Code, Nothing)
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = objTr.Taxable_Amount_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objTr.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = 0
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = objTr.Disc_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountPer).Value = objTr.Header_Discount_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = objTr.TAX1_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = objTr.TAX2_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = objTr.TAX3_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = objTr.TAX4_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = objTr.TAX5_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = objTr.TAX6_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = objTr.TAX7_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = objTr.TAX8_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = objTr.TAX9_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = objTr.TAX10_Rate
                        If rbtnTaxCalManual.IsChecked Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = objTr.TAX1_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = objTr.TAX2_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = objTr.TAX3_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = objTr.TAX4_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = objTr.TAX5_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = objTr.TAX6_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = objTr.TAX7_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = objTr.TAX8_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = objTr.TAX9_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = objTr.TAX10_Amt
                        End If
                    End If
                Next
            End If

            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem

            If rbtnTaxCalManual.IsChecked Then
                For ii As Integer = 1 To 10
                    If gv2.Rows.Count >= ii Then
                        Dim dblTotTaxAmt As Double = 0
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            dblTotTaxAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells("COLTAXAMT" + clsCommon.myCstr(ii)).Value)
                        Next
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = dblTotTaxAmt
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(gv1.Rows(0).Cells("COLTAXRATE" + clsCommon.myCstr(ii)).Value)
                    End If
                Next
            End If


            SetitemWiseTaxSetting(False, False)
            For ii As Integer = 0 To gv1.RowCount - 1
                UpdateCurrentRow(ii)
            Next
            If rbtnTaxCalManual.IsChecked Then ''For Calcuation custom tax according to ratio of amount
                For ii As Integer = 0 To gv1.RowCount - 1
                    UpdateCurrentRow(ii)
                Next
            End If
        End If
        CalculateInsuranceTotal(True)
        isInsideLoadData = False
        UpdateAllTotals()
        RefreshReqNo()

        gv1.Columns(colDisPer).ReadOnly = True
        gv1.Columns(colDisAmt).ReadOnly = True

        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(txtPONo.Value) > 0 Then
                gv1.Rows(ii).Cells(colRate).ReadOnly = True
            End If
        Next
    End Sub
    Function IsValidItem(ByVal obj As clsPurchaseOrderDetail)
        If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
            txtTaxGroup.Value = obj.POTax_Group
            SetTaxDetails()
        End If
        If Not clsCommon.CompairString(txtTaxGroup.Value, obj.POTax_Group) = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Item : " + obj.Item_Desc + " not Added Current Tax Group : " + txtTaxGroup.Value + " PO No: " + obj.PurchaseOrder_No + "  contain Tax Group :" + obj.POTax_Group + Environment.NewLine)
            Return False
        End If
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPONo).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
            Dim strGRNCode As String = ""
            Dim strMRNCode As String = ""
            If is_Load_MRN Then
                strGRNCode = clsCommon.myCstr(gv1.Rows(ii).Cells(colGRN_NO).Value)
                strMRNCode = clsCommon.myCstr(gv1.Rows(ii).Cells(colMRN_NO).Value)
            End If

            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strReqCode, obj.PurchaseOrder_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, obj.Unit_code) = CompairStringResult.Equal AndAlso dblMRP = obj.MRP Then
                If is_Load_MRN Then
                    If clsCommon.CompairString(strGRNCode, obj.GRN_No_Temp) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strMRNCode, obj.MRN_No) = CompairStringResult.Equal Then
                        Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "PO No : " + obj.PurchaseOrder_No + ",GRN No : " + obj.GRN_No_Temp + " ,MRN No : " + obj.MRN_No + "  Item : " + obj.Item_Desc + Environment.NewLine + ""
                        If dblMRP > 0 Then
                            strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
                        End If
                        common.clsCommon.MyMessageBoxShow(strMsg)
                        Return False
                    End If
                Else
                    Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "PO No : " + obj.PurchaseOrder_No + "  Item : " + obj.Item_Desc + Environment.NewLine + ""
                    If dblMRP > 0 Then
                        strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
                    End If
                    common.clsCommon.MyMessageBoxShow(strMsg)
                    Return False
                End If
            End If


        Next
        Return True
    End Function
    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) AndAlso rbtnTaxCalAutomatic.IsChecked Then
                'If Not PurchaseModulePickFixTaxRate OrElse Not clsCommon.myCBool(gv1.CurrentRow.Cells(colItemTaxable).Value) Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
                frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmtAfterDis).Value)
                ''New Column for location wise
                frm.strTaxGroup = txtTaxGroup.Value
                frm.strTransLocation = txtBillToLocation.Value
                frm.strTaxType = "P"
                frm.strVendorCustomerCode = txtVendorNo.Value
                ''End of New Column for location wise
                frm.PurchaseModulePickFixTaxRate = PurchaseModulePickFixTaxRate
                frm.IsTaxableItem = clsCommon.myCBool(gv1.CurrentRow.Cells(colItemTaxable).Value)
                If clsCommon.myLen(frm.strItemCode) > 0 Then
                    frm.ArrIn = New List(Of clsTempItemTaxDetails)
                    For ii As Integer = 1 To 10
                        Dim strii As String = clsCommon.myCstr(ii)
                        Dim obj As New clsTempItemTaxDetails()
                        obj.AuthorityCode = clsCommon.myCstr(gv1.CurrentRow.Cells("COLTAX" + strii).Value)
                        If clsCommon.myLen(obj.AuthorityCode) > 0 Then
                            obj.AuthorityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Code_Desc from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.AuthorityCode + "'"))
                            obj.Rate = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXRATE" + strii).Value)
                            obj.BaseAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value)
                            obj.TaxAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXAMT" + strii).Value)
                            obj.isSurTax = clsCommon.myCBool(gv1.CurrentRow.Cells("ISSURTAX" + strii).Value)
                            obj.SurTax = clsCommon.myCstr(gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value)
                            obj.IsTaxable = clsCommon.myCBool(gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value)
                            obj.TaxOnBaseAmount = clsCommon.myCBool(gv1.CurrentRow.Cells("colTaxOnBaseAmt" + strii).Value)
                            frm.ArrIn.Add(obj)
                        End If
                    Next

                    frm.ShowDialog()
                    If frm.ArrOut IsNot Nothing AndAlso frm.ArrOut.Count > 0 Then
                        BlankTaxDetails(gv1.CurrentRow.Index)
                        For ii As Integer = 0 To frm.ArrOut.Count - 1
                            Dim strii As String = clsCommon.myCstr(ii + 1)
                            gv1.CurrentRow.Cells("COLTAX" + strii).Value = frm.ArrOut(ii).AuthorityCode
                            gv1.CurrentRow.Cells("COLTAXRATE" + strii).Value = frm.ArrOut(ii).Rate
                            gv1.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value = frm.ArrOut(ii).BaseAmt
                            gv1.CurrentRow.Cells("COLTAXAMT" + strii).Value = frm.ArrOut(ii).TaxAmt
                            gv1.CurrentRow.Cells("ISSURTAX" + strii).Value = frm.ArrOut(ii).isSurTax
                            gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value = frm.ArrOut(ii).SurTax
                            gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value = frm.ArrOut(ii).IsTaxable
                            gv1.CurrentRow.Cells("colTaxOnBaseAmt" + strii).Value = frm.ArrOut(ii).TaxOnBaseAmount
                        Next
                        gv1.CurrentRow.Cells(colTotTaxAmt).Value = frm.dblTotTax
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                        UpdateAllTotals()
                    End If
                End If
                'End If
            ElseIf gv1.CurrentColumn Is gv1.Columns(colComplete) AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
                Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                Dim intSNo As Integer = Convert.ToInt32((clsCommon.myCdbl(gv1.CurrentRow.Cells(colLineNo).Value)))
                Dim strStatus As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colComplete).Value)
                If clsCommon.myLen(txtDocNo.Value) > 0 AndAlso clsCommon.myLen(strICode) > 0 AndAlso intSNo > 0 AndAlso clsCommon.CompairString(strStatus, "No") = CompairStringResult.Equal Then
                    If common.clsCommon.MyMessageBoxShow("Do you want to complete the item " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value), Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        If clsSRNDetail.CompleteSRN(txtDocNo.Value, strICode, intSNo) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Successfully Completed", Me.Text)
                            LoadData(txtDocNo.Value, NavigatorType.Current)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Document number not found", Me.Text)
        Else
            PrintDataNew(txtDocNo.Value)
        End If
    End Sub
    Sub PrintDataNew(ByVal StrDocNo As String)
        Try

            If clsCommon.myLen(txtDocNo.Value) <= 0 AndAlso clsCommon.myLen(StrDocNo) <= 0 Then
                Throw New Exception("Document number not found")
            End If
            Dim ArrSrnNo As New ArrayList() '' Added By abhishek kumar as on 13 july 2012 For get DocNo
            ArrSrnNo.Add(StrDocNo)
            SRNPrintOut(Nothing, Nothing, False, ArrSrnNo, Nothing, Nothing)

            ''  commented panch raj on saying Amit Sir (print format must be same for all types of items)

            'Dim qry As String = "select Item_Type from TSPL_SRN_HEAD where SRN_No='" + txtDocNo.Value + "'"

            'If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "F") = CompairStringResult.Equal Then
            '    'PrintForFinishGoods()
            '    '' Added By abhishek kumar as on 13 july 2012 For Finished Goods.
            '    Obj.SRNPrintOut(Nothing, Nothing, False, ArrSrnNo, Nothing, Nothing)
            'Else
            '    'print()
            '    '' Added By abhishek kumar as on 13 july 2012 For RMOther Type
            '    Obj.SRNPrintOut(Nothing, Nothing, False, ArrSrnNo, Nothing, Nothing)

            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub SRNPrintOut(ByVal FromDate As Date?, ByVal ToDate As Date?, ByVal IsDocTypeFinsihGoods As Boolean, ByVal ArrSrnNo As ArrayList, ByVal ArrVendor As ArrayList, ByVal ArrLocation As ArrayList)
        Dim qry As String

        Try

            '' Anubhooti 28-Aug-2014 (Demo Setting For Status) BM00000003672
            '=============Added by preeti Gupta Against ticket No[BHA/21/05/18-000028]
            Dim frmCRV As New frmCrystalReportViewer()
            Dim QryShowStatus As String = ""
            Dim IsSRNReportQtyWise As Boolean = False
            IsSRNReportQtyWise = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SRNReportQuantityWise, clsFixedParameterCode.SRNReportQuantityWise, Nothing)) = 1, True, False)
            Dim ShowStatusForPurchase As Double = clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_FIXED_PARAMETER  WHERE Code ='ShowStatusForPurchase' And Type ='ShowStatusForPurchase'")
            If clsCommon.CompairString(clsCommon.myCstr(ShowStatusForPurchase), "1") = CompairStringResult.Equal Then
                QryShowStatus = " ,(case when TSPL_SRN_HEAD.status =1 then 'Approved' else 'Pending' end) as Status "
            Else
                QryShowStatus = ""
            End If

            If IsDocTypeFinsihGoods Then

                qry = "Select '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MM-yyyy") + "' as RunDate, XXXX.*, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Ecc_No, "
                qry += " (Case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else TSPL_COMPANY_MASTER.Add1 + case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Add2 + Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else TSPL_COMPANY_MASTER.Add3 + Case When ISNULL(TSPL_LOCATION_MASTER.add4,'')='' Then '' Else TSPL_LOCATION_MASTER.add4 End End End End) AS CompAddress, "
                qry += " TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Tin_No as Vendor_Tin_No,TSPL_VENDOR_MASTER.Phone1 as Vendor_Contact, "
                qry += " (Case When ISNULL(TSPL_VENDOR_MASTER.Add1,'')='' Then '' Else TSPL_VENDOR_MASTER.Add1 + case When ISNULL(TSPL_VENDOR_MASTER.Add1,'')='' Then '' Else ', '+ TSPL_VENDOR_MASTER.Add2 + Case When ISNULL(TSPL_VENDOR_MASTER.Add3,'')='' Then '' Else ', '+TSPL_VENDOR_MASTER.Add3 End End End) AS VendorAddress from ("

                qry += " select max(Description) as Description ,max(Comments) as Comments,  max(Created_By) as Created_By,max(Modify_By) as Modify_By,  SRN_No,MAX(ItemType )as ItemType, MAX(Inv_No) as Inv_No, MAX(MRN_Date) as SRN_Date, MAX(SRNTime) as SrnTime,  MAX(Vendor_Code) as Vendor_Code,MAX(GRNo) as GRNo,"
                qry += " MAX(GENo) as GENo,MAX(GEDate) as GEDate,Item_Code,MAX(Item_Desc) as Item_Desc, MAX(MRP) as MRP,MAX(VehicleNo) as VehicleNo, "
                qry += " SUM(ISNULL( FCS,0)) as FCS, SUM(isnull(FBS,0))as FBS, SUM(ISNULL( FSH,0)) as FSH, SUM(ISNULL( ECS,0)) as ECS, SUM(ISNULL( EBS,0)) as EBS, "
                qry += " SUM(Leak_Qty) as HF,SUM(Burst_Qty) as Burst,SUM(Short_Qty) as Short,MAX(Remarks) as Remarks,max(Ref_No)as Ref_No,"
                qry += " max(Against_PO) as Against_PO, MAX(Against_RGP) as GPNo, Max(Comp_Code) as Comp_Code,MAX(Carrier) as Carrier from( "

                qry += " select Description,Comments, TSPL_SRN_HEAD.Created_By,TSPL_SRN_HEAD .Modify_By, TSPL_SRN_HEAD.SRN_No, TSPL_SRN_HEAD.Item_Type as ItemType,TSPL_SRN_HEAD.Against_PO,TSPL_SRN_HEAD.Carrier , TSPL_SRN_HEAD.Inv_No ,"
                qry += " (replace( CONVERT(varchar(11), TSPL_SRN_HEAD.SRN_Date,104),'.','/')+' '+CONVERT(varchar(100),TSPL_SRN_HEAD.SRN_Date,108) )as MRN_Date, CONVERT(VARCHAR,SRN_Date,108) as SRNTime,"
                qry += " TSPL_SRN_HEAD.Vendor_Code ,TSPL_SRN_HEAD.GRNo,TSPL_SRN_HEAD.GENo,(case when LEN(TSPL_SRN_HEAD.GEDate)>0  then REPLACE( CONVERT(varchar(11), TSPL_SRN_HEAD.GEDate,104),'.','/') else '' end) as GEDate,"
                qry += " TSPL_SRN_HEAD.VehicleNo,TSPL_SRN_HEAD.Remarks ,TSPL_SRN_HEAD.Ref_No, TSPL_SRN_HEAD.Against_RGP, TSPL_SRN_DETAIL.Item_Code, "
                qry += " TSPL_SRN_DETAIL.MRP, TSPL_SRN_DETAIL.Item_Desc,TSPL_SRN_DETAIL.Unit_code,case when Unit_code='FC' then SRN_Qty + ISNULL( Free_Qty,0) end as FCS,"
                qry += " case when Unit_code='FB' then SRN_Qty + ISNULL( Free_Qty,0) end as FBS, case when Unit_code='SH' then SRN_Qty + ISNULL( Free_Qty,0) end as FSH,"
                qry += " case when Unit_code='EC' then SRN_Qty + ISNULL( Free_Qty,0) end as ECS,case when Unit_code='EB' then SRN_Qty + ISNULL( Free_Qty,0) end as EBS, TSPL_SRN_DETAIL.Leak_Qty,TSPL_SRN_DETAIL.Burst_Qty,TSPL_SRN_DETAIL.Short_Qty, TSPL_SRN_HEAD.Comp_Code "
                qry += " from TSPL_SRN_DETAIL "
                qry += " left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No= TSPL_SRN_DETAIL.SRN_No  "
                qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SRN_HEAD.Bill_To_Location"
                qry += " where Item_Type ='F' order by TSPL_SRN_DETAIL.line_no"

                If FromDate.HasValue AndAlso ToDate.HasValue Then
                    qry += " and Convert(date,TSPL_SRN_HEAD.SRN_Date,103)>=Convert(date,'" + FromDate + "',103)and Convert(date,TSPL_SRN_HEAD.SRN_Date,103)<=Convert(date,'" + ToDate + "',103) "
                End If

                If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
                    qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(ArrLocation) + ") "
                End If
                If ArrSrnNo IsNot Nothing AndAlso ArrSrnNo.Count > 0 Then
                    qry += " and TSPL_SRN_HEAD.SRN_No in (" + clsCommon.GetMulcallString(ArrSrnNo) + ")  "
                End If
                If ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
                    qry += " and TSPL_SRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")" 'ADDED BY ABHISHEK AS ON 30 AUG 2012
                End If
                qry += " )xxx group by SRN_No,Item_Code "
                qry += " ) XXXX LEFT OUTER JOIN TSPL_COMPANY_MASTER ON XXXX.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code"
                qry += " Left Outer Join TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=XXXX.Vendor_Code order by Item_Desc"


                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
                Else
                    'PurchaseOrderViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x6, "rptSRNCustomReport", "SRN Report")
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Vizag") = CompairStringResult.Equal Then
                        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptSRN_4_FG_Vizag", "SRN Report")
                    Else
                        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptSRN_4_FG_Guntur", "SRN Report")
                    End If
                End If
            Else ''For RM Other Print out
                Dim strquery As String = "SELECT TSPL_GRN_HEAD.Ref_No,TSPL_GRN_HEAD.GRN_Date, TSPL_SRN_HEAD.Against_QC_Code,TSPL_SRN_HEAD.Against_QC_Date,isnull(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,'') as PurchaseOrder_No,isnull(convert(varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103),'') as PurchaseOrder_Date,TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode, TSPL_SRN_HEAD.Against_MRN,tspl_srn_detail.short_Qty, Location_Code,convert(varchar,TSPL_SRN_HEAD.Challan_Date,103) as CHA_Date,TSPL_SRN_DETAIL.Specification as Det_Specification,TSPL_SRN_DETAIL.Remarks  as DetRemarks ,TSPL_VENDOR_MASTER.CST as vndr_cst,TSPL_VENDOR_MASTER.Tin_No as vndr_tin,TSPL_LOCATION_MASTER.CST_No as location_cst,TSPL_SRN_HEAD.Ship_To_Location,TSPL_SHIP_TO_LOCATION.Ship_To_Desc,(TSPL_SHIP_TO_LOCATION.Add1+' '+TSPL_SHIP_TO_LOCATION.add2+' '+TSPL_SHIP_TO_LOCATION.add3) as ship_addr,TSPL_SHIP_TO_LOCATION.City_Code as ship_city,TSPL_SHIP_TO_LOCATION.State as ship_state,TSPL_SRN_DETAIL.MRP,Location_Desc  as Location_Company ,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.add4,''))>0 then '- '+isnull(TSPL_LOCATION_MASTER.add4,'') else ' ' end   as company_address,TSPL_LOCATION_MASTER.City_Code as TSPL_LOCATION_MASTER_City_Code ,TSPL_LOCATION_MASTER.State as TSPL_LOCATION_MASTER_state ,TSPL_LOCATION_MASTER.Country as TSPL_LOCATION_MASTER_country, TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end   as vendor_address, TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end+case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then '- '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Location_Desc,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Location_Desc,'') else ' ' end    as address1, isnull(TSPL_LOCATION_MASTER.Location_Desc,'') as Location_Desc_Heading,TSPL_LOCATION_MASTER.add1 as Location_Add1_heading,  TSPL_LOCATION_MASTER.add2 as Location_Add2_heading, TSPL_LOCATION_MASTER.Add3  as Location_Add3_heading,TSPL_LOCATION_MASTER.Add4  as Location_Add4_heading , TSPL_LOCATION_MASTER.IsMainPlant ,TSPL_LOCATION_MASTER .TIN_No ,( case when TSPL_LOCATION_MASTER.Phone2 <> '' then TSPL_LOCATION_MASTER.Phone1 +','+TSPL_LOCATION_MASTER.Phone2 else TSPL_LOCATION_MASTER.Phone1 end) as Location_Phn, TSPL_SRN_HEAD.Description,TSPL_SRN_HEAD.Comments, user_master1.User_Name as Created_By,user_master2.User_Name as Modify_By, TSPL_SRN_HEAD.SRN_No, TSPL_SRN_HEAD.SRN_Date,TSPL_VENDOR_MASTER.Vendor_Name, TSPL_VENDOR_MASTER.Tin_No as Vendor_Tin_No, TSPL_VENDOR_MASTER.Phone1 as Vendor_Contact, (case when len(against_mrn)>0 then (select MRN_Date  from tspl_mrn_head where tspl_mrn_head.MRN_No =against_mrn) else SRN_Date end ) as Challan_Date, TSPL_SRN_HEAD.Ref_No  " &
                      "as Challan_No, TSPL_SRN_HEAD.Inv_No, convert(varchar,TSPL_SRN_HEAD.Inv_Date,103) as Inv_Date, TSPL_SRN_HEAD.GRNo,TSPL_SRN_HEAD.Amount_Less_Discount ,TSPL_SRN_HEAD.GENo,TSPL_SRN_HEAD.SRN_Total_Amt, " &
                      "convert(varchar,TSPL_SRN_HEAD.GEDate,103) as GEDate, TSPL_SRN_HEAD.VehicleNo,TSPL_SRN_HEAD.SRN_No, TSPL_SRN_HEAD.Carrier,TSPL_SRN_HEAD.Remarks,TSPL_SRN_DETAIL.Landed_Cost_Rate,TSPL_SRN_DETAIL.Landed_Cost_Amount , TSPL_SRN_DETAIL.Item_Code,TSPL_SRN_DETAIL.UOM_WEIGHT,TSPL_SRN_DETAIL.UOM_WEIGHT_VALUE,TSPL_SRN_DETAIL.Row_Type,TSPL_SRN_DETAIL.Amt_Less_Discount," &
"TSPL_SRN_DETAIL.Item_Cost as basicRate,TSPL_SRN_DETAIL.Item_Net_Amt as BasicTotal,TSPL_SRN_DETAIL.Unit_Cost_Tax_Rate as UCTR," &
"TSPL_SRN_DETAIL.Unit_Cost_Tax as uctax,TSPL_SRN_DETAIL.Item_Desc,TSPL_SRN_DETAIL.Unit_code,TSPL_SRN_DETAIL.SRN_Qty,TSPL_SRN_DETAIL.Rejected_Qty,TSPL_SRN_DETAIL.Short_Qty,TSPL_SRN_DETAIL.GRN_Qty,TSPL_SRN_HEAD.Vendor_Code,TSPL_SRN_HEAD.SRN_Total_Amt,TSPL_SRN_DETAIL.ITEM_COST," &
 "TSPL_VENDOR_MASTER.Add1 as venAdd1, TSPL_VENDOR_MASTER.Add2 as vanadd2, TSPL_VENDOR_MASTER.Add3 as venadd3, " &
"tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SRN_HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name," &
"isnull (TSPL_SRN_HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SRN_HEAD.tax3_amt,0) as txt3amt," &
"tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SRN_HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name," &
"isnull (TSPL_SRN_HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SRN_HEAD.tax6_amt,0) as txt6amt " &
",tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SRN_HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name," &
"isnull (TSPL_SRN_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SRN_HEAD.tax9_amt,0) as txt9amt," &
"tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SRN_HEAD.tax10_amt,0) as txt10amt, TSPL_COMPANY_MASTER.Comp_Name as compname,'" & objCommonVar.CurrentUser & "' as User_Name, " &
"TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_SRN_DETAIL.SRN_Qty," &
"case when tax1.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax1_amt else null end as Tax1Recoverable," &
"case when tax2.Tax_Recoverable='Y' then TSPL_SRN_HEAD.TAX2_Amt else null end as Tax2Recoverable, " &
"case when tax3.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax3_amt else null end as Tax3Recoverable, " &
"case when tax4.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax4_amt else null end as Tax4Recoverable, " &
"case when tax5.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax5_amt else null end as Tax5Recoverable, " &
"case when tax6.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax6_amt else null end as Tax6Recoverable," &
"case when tax7.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax7_amt else null end as Tax7Recoverable, " &
"case when tax8.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax8_amt else null end as Tax8Recoverable, " &
"case when tax9.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax9_amt else null end as Tax9Recoverable," &
"case when tax10.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax10_amt else null end as Tax10Recoverable, " &
"TSPL_SRN_HEAD.TAX1,TSPL_SRN_HEAD.TAX2,TSPL_SRN_HEAD.TAX3,TSPL_SRN_HEAD.TAX4,TSPL_SRN_HEAD.TAX5,TSPL_SRN_HEAD.tax6," &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX1_Rate ,0),103)+'%' as txt1Rate," &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX2_Rate   ,0),103)+'%' as txt2Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX3_Rate  ,0),103)+'%' as txt3Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX4_Rate  ,0),103)+'%' as txt4Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX5_Rate  ,0),103)+'%' as txt5Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX6_Rate  ,0),103)+'%' as txt6Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX7_Rate  ,0),103)+'%' as txt7Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX8_Rate  ,0),103)+'%' as txt8Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX9_Rate  ,0),103)+'%' as txt9Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX10_Rate  ,0),103)+'%' as txt10Rate," &
"TSPL_SRN_DETAIL.Amt_Less_Discount as Value,(select SUM(rejected_qty) from tspl_srn_detail where srn_no=TSPL_SRN_HEAD.SRN_No) as Rej_qty, (select SUM(TSPL_MRN_DETAIL.MRN_Qty) from TSPL_SRN_DETAIl left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL .MRN_No=TSPL_SRN_DETAIL.MRN_Id and TSPL_MRN_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No where SRN_No =TSPL_SRN_HEAD.SRN_No and TSPL_MRN_HEAD.IsCancel=0 )as MrnTotQty, (select SUM(SRN_qty) from tspl_srn_detail where srn_no=TSPL_SRN_HEAD.SRN_No) as SRNQtyTotal, (select case when COUNT(xxx.PI_No)>1 then Min(xxx.PI_No)+ ' *' else Min(xxx.PI_No)end as PINO from" &
" ( select TSPL_PI_DETAIL.PI_No from TSPL_PI_DETAIL  where  TSPL_PI_DETAIL.SRN_Id= TSPL_SRN_HEAD.SRN_No " &
" GROUP by TSPL_PI_DETAIL.PI_No)xxx) as PInvNo  ,    " &
       " TSPL_SRN_HEAD.Add_Charge_Name1 as Add1Name, " &
     " TSPL_SRN_HEAD.Add_Charge_Amt1 as Add1 , " &
     "     TSPL_SRN_HEAD.Add_Charge_Name2 as Add2Name, " &
     "   TSPL_SRN_HEAD.Add_Charge_Amt2 as Add2 , " &
     "    TSPL_SRN_HEAD.Add_Charge_Name3 as Add3Name, " &
     "   TSPL_SRN_HEAD.Add_Charge_Amt3 as Add3 , " &
     "    TSPL_SRN_HEAD.Add_Charge_Name4 as Add4Name, " &
     "    TSPL_SRN_HEAD.Add_Charge_Amt4 as Add4 , " &
     "     TSPL_SRN_HEAD.Add_Charge_Name5 as Add5Name, " &
      "     TSPL_SRN_HEAD.Add_Charge_Amt5 as Add5 , " &
      "     TSPL_SRN_HEAD.Add_Charge_Name6 as Add6Name, " &
      "    TSPL_SRN_HEAD.Add_Charge_Amt6 as Add6 , " &
      "    TSPL_SRN_HEAD.Add_Charge_Name7 as Add7Name, " &
      "     TSPL_SRN_HEAD.Add_Charge_Amt7 as Add7 , " &
      "       TSPL_SRN_HEAD.Add_Charge_Name8 as Add8Name, " &
      "      TSPL_SRN_HEAD.Add_Charge_Amt8 as Add8 , " &
       "      TSPL_SRN_HEAD.Add_Charge_Name9 as Add9Name, " &
       "      TSPL_SRN_HEAD.Add_Charge_Amt9 as Add9 , " &
       "      TSPL_SRN_HEAD.Add_Charge_Name10 as Add10Name, " &
       "     TSPL_SRN_HEAD.Add_Charge_Amt10 as Add10,TSPL_SRN_HEAD.Against_RGP,TSPL_SRN_DETAIL .Specification ,TSPL_SRN_HEAD.Against_Requisition ,TSPL_SRN_DETAIL.PO_Qty,TSPL_SRN_DETAIL.GRN_Qty,TSPL_SRN_DETAIL.MRN_Qty,TSPL_SRN_DETAIL.PO_id,TSPL_SRN_DETAIL.Req_No,TSPL_SRN_HEAD.Against_GRN,TSPL_SRN_HEAD.Form_38, "

                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Vizag") = CompairStringResult.Equal Then
                    strquery += " (Case when len(TSPL_SRN_HEAD.Against_PO)>0 then TSPL_SRN_HEAD.Against_PO else TSPL_SRN_HEAD.Against_Requisition end) as Against_PO "
                Else
                    strquery += " TSPL_SRN_HEAD.Against_PO "
                End If
                strquery += " " & QryShowStatus & " "
                strquery += ", case when tax1.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax1_amt else null end as Tax1NonRecoverable," &
                " case when tax2.Tax_Recoverable='N' then TSPL_SRN_HEAD.TAX2_Amt else null end as Tax2NonRecoverable, " &
                " case when tax3.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax3_amt else null end as Tax3NonRecoverable, " &
                " case when tax4.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax4_amt else null end as Tax4NonRecoverable, " &
                " case when tax5.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax5_amt else null end as Tax5NonRecoverable, " &
                " case when tax6.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax6_amt else null end as Tax6NonRecoverable," &
                " case when tax7.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax7_amt else null end as Tax7NonRecoverable, " &
                " case when tax8.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax8_amt else null end as Tax8NonRecoverable, " &
                " case when tax9.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax9_amt else null end as Tax9NonRecoverable," &
                " case when tax10.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax10_amt else null end as Tax10NonRecoverable"
                strquery += " ,TSPL_SRN_HEAD.Dept_desc
,CASE WHEN TSPL_SRN_DETAIL.disc_amt>0 THEN CONVERT(DECIMAL(18,3), (case when srn_qty>0 then ((TSPL_SRN_DETAIL.amount-TSPL_SRN_DETAIL.disc_amt)/srn_qty) else 0 end)) ELSE TSPL_SRN_DETAIL.ITEM_COST END as ITEM_COST_AFTER_DISC " + Environment.NewLine +
"FROM  TSPL_SRN_DETAIL INNER JOIN TSPL_SRN_HEAD ON TSPL_SRN_DETAIL.SRN_No = TSPL_SRN_HEAD.SRN_No " &
    "INNER JOIN TSPL_COMPANY_MASTER ON TSPL_SRN_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  " &
    "INNER JOIN TSPL_VENDOR_MASTER ON TSPL_SRN_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code " &
    "left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SRN_HEAD.tax1  " &
    "left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SRN_HEAD.tax2 " &
    "left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SRN_HEAD .TAX3 " &
    "left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SRN_HEAD .tax4 " &
    "left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SRN_HEAD .tax5 " &
    "left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SRN_HEAD .TAX6  " &
    "left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SRN_HEAD .TAX7  " &
    "left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SRN_HEAD .TAX8 " &
    "left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SRN_HEAD .TAX9 " &
    " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SRN_HEAD .TAX10  " &
    "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SRN_HEAD.Bill_To_Location  " &
    "left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SRN_HEAD.Ship_To_Location " &
    " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_SRN_DETAIL.Item_Code " &
    " left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state  " &
    "  left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code " &
    " left outer join tspl_user_master as user_master1 on user_master1.user_code=TSPL_SRN_HEAD.Created_By  " &
    " left join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_SRN_HEAD.Against_GRN  " &
    " left outer join tspl_user_master as user_master2 on user_master2.user_code=TSPL_SRN_HEAD.Modify_By " &
    " left join TSPL_PURCHASE_ORDER_HEAD on isnull(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,'') =isnull(TSPL_SRN_DETAIL.PO_ID,'')" &
" where 2=2"

                If FromDate.HasValue AndAlso ToDate.HasValue Then
                    strquery += " and Convert(date,TSPL_SRN_HEAD.SRN_Date,103)>=Convert(date,'" + FromDate + "',103)and Convert(date,TSPL_SRN_HEAD.SRN_Date,103)<=Convert(date,'" + ToDate + "',103) "

                End If
                If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
                    strquery += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(ArrLocation) + ") "
                End If
                If ArrSrnNo IsNot Nothing AndAlso ArrSrnNo.Count > 0 Then
                    strquery += " and TSPL_SRN_HEAD.SRN_No in (" + clsCommon.GetMulcallString(ArrSrnNo) + ")  "
                End If
                If ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
                    strquery += " and TSPL_SRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")  "

                End If

                strquery = strquery + " order by tspl_srn_detail.line_no"

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strquery)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                Else
                    If clsCommon.CompairString(clsCommon.myCstr(objCommonVar.CurrentCompanyCode), "KL") <> CompairStringResult.Equal Then
                        clsSRNHead.SetItemWiseTax(dt, ArrSrnNo(0))
                    End If

                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Vizag") = CompairStringResult.Equal Then
                        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "SRNReportThroughReport", "Store Receipt Report")
                    ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        If IsSRNReportQtyWise Then
                            frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "SRNReportThroughReportQtyWise", "Store Receipt Report", clsCommon.myCDate(txtDate.Value), "rptCompanyAddress.rpt")
                        Else
                            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "SRNReportThroughReport-G", "Store Receipt Report", clsCommon.myCDate(txtDate.Value))
                        End If
                    Else
                        If IsSRNReportQtyWise Then
                            frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "SRNReportThroughReportQtyWise", "Store Receipt Report", clsCommon.myCDate(txtDate.Value), "rptCompanyAddress.rpt")
                        Else
                            frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "SRNReportThroughReport-G", "Store Receipt Report", clsCommon.myCDate(clsCommon.GETSERVERDATE()), "rptCompanyAddress.rpt", "SubRptCmpnyMasterForERODE.rpt", clsERPFuncationality.CompanyAddresShowinHeaderPartForERODE()) 'update by preeti gupta Against Ticket No[ADV/27/07/18-000036]
                        End If
                        'SRNReportThroughReportQtyWise
                        'PurchaseOrderViewer.funreport(dt, "SRNReportThroughReport-G", "Store Receipt Report")
                    End If
                End If
            End If
            frmCRV = Nothing
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub PrintForFinishGoods()
        Dim qry As String
        '= "select SRN_No,MAX(MRN_Date) as SRN_Date,MAX(Vendor_Name) as Vendor_Name,MAX(GRNo) as GRNo,MAX(GENo) as GENo,MAX(GEDate) as GEDate,Item_Code,MAX(Item_Desc) as Item_Desc,MAX(VehicleNo) as VehicleNo, SUM(ISNULL( FCS,0)) as FCS, SUM(isnull(FBS,0))as FBS, SUM(ISNULL( FSH,0)) as FSH, SUM(ISNULL( ECS,0)) as ECS, SUM(ISNULL( EBS,0)) as EBS, SUM(Leak_Qty) as HF,SUM(Burst_Qty) as Burst,SUM(Short_Qty) as Short,MAX(Remarks) as Remarks,max(Ref_No)as Ref_No from( " & _
        '        "select TSPL_SRN_HEAD.SRN_No," & _
        '        "(replace( CONVERT(varchar(11), TSPL_SRN_HEAD.SRN_Date,104),'.','/')+' '+CONVERT(varchar(100),TSPL_SRN_HEAD.SRN_Date,108) )as MRN_Date,TSPL_SRN_HEAD.Vendor_Name,TSPL_SRN_HEAD.GRNo,TSPL_SRN_HEAD.GENo," & _
        '        "(case when LEN(TSPL_SRN_HEAD.GEDate)>0  then REPLACE( CONVERT(varchar(11), TSPL_SRN_HEAD.GEDate,104),'.','/') else '' end) as GEDate,TSPL_SRN_HEAD.VehicleNo,TSPL_SRN_HEAD.Remarks ,TSPL_SRN_HEAD.Ref_No,TSPL_SRN_DETAIL.Item_Code,(TSPL_SRN_DETAIL.Item_Desc+'.'+TSPL_SRN_DETAIL.Specification )as Item_Desc ,TSPL_SRN_DETAIL.Unit_code," & _
        '        "case when Unit_code='FC' then SRN_Qty end as FCS, " & _
        '        "case when Unit_code='FB' then SRN_Qty end as FBS, " & _
        '        "case when Unit_code='SH' then SRN_Qty end as FSH, " & _
        '        "case when Unit_code='EC' then SRN_Qty end as ECS," & _
        '        "case when Unit_code='EB' then SRN_Qty end as EBS, " & _
        '        "TSPL_SRN_DETAIL.Leak_Qty,TSPL_SRN_DETAIL.Burst_Qty,TSPL_SRN_DETAIL.Short_Qty from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No= TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_HEAD.SRN_No='" + txtDocNo.Value + "'" & _
        '        ")xxx group by SRN_No,Item_Code order by Item_Desc"


        qry = "select SRN_No,MAX(MRN_Date) as SRN_Date," &
"MAX(Vendor_Name) as Vendor_Name," &
"MAX(GRNo) as GRNo," &
"MAX(GENo) as GENo," &
"MAX(GEDate) as GEDate,Item_Code," &
"MAX(Item_Desc) as Item_Desc," &
"MAX(VehicleNo) as VehicleNo, " &
"SUM(ISNULL( FCS,0)) as FCS, " &
"SUM(isnull(FBS,0))as FBS, " &
"SUM(ISNULL( FSH,0)) as FSH," &
 "SUM(ISNULL( ECS,0)) as ECS, " &
 "SUM(ISNULL( EBS,0)) as EBS," &
  "SUM(Leak_Qty) as HF," &
  "SUM(Burst_Qty) as Burst," &
  "SUM(Short_Qty) as Short," &
  "MAX(Remarks) as Remarks," &
  "max(Ref_No)as Ref_No ," &
   "(select delivered_by from TSPL_RGP_HEAD where RGP_No=max( xxx.Against_RGP))as Delivered_by " &
  "from( select TSPL_SRN_HEAD.Against_RGP , TSPL_SRN_HEAD.SRN_No," &
  "(replace( CONVERT(varchar(11), TSPL_SRN_HEAD.SRN_Date,104),'.','/')+' '+" &
  "CONVERT(varchar(100),TSPL_SRN_HEAD.SRN_Date,108) )as MRN_Date,TSPL_SRN_HEAD.Vendor_Name,TSPL_SRN_HEAD.GRNo,TSPL_SRN_HEAD.GENo,(case when " &
  " LEN(TSPL_SRN_HEAD.GEDate)>0  then" &
   " REPLACE( CONVERT(varchar(11), TSPL_SRN_HEAD.GEDate,104),'.','/') else '' end) as GEDate,TSPL_SRN_HEAD.VehicleNo,TSPL_SRN_HEAD.Remarks ,TSPL_SRN_HEAD.Ref_No,TSPL_SRN_DETAIL.Item_Code,(TSPL_SRN_DETAIL.Item_Desc+'.'+TSPL_SRN_DETAIL.Specification )as Item_Desc  ,TSPL_SRN_DETAIL.Unit_code,case when Unit_code='FC' then SRN_Qty end as FCS, case when Unit_code='FB' then SRN_Qty end as FBS, case when Unit_code='SH' then SRN_Qty end as FSH, case when Unit_code='EC' then SRN_Qty end as ECS,case when Unit_code='EB' then SRN_Qty end as EBS, TSPL_SRN_DETAIL.Leak_Qty,TSPL_SRN_DETAIL.Burst_Qty,TSPL_SRN_DETAIL.Short_Qty from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No= TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_HEAD.SRN_No='" + txtDocNo.Value + "')xxx group by SRN_No,Item_Code order by Item_Desc "



        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.PaperSize10x6, "rptSRNCustom", "SRN Report")
        frmCRV = Nothing
    End Sub
    Public Sub print()
        ''
        ''Calculation Of Total Rejected_Quantity
        ''
        Dim sqlRejctdQTY As String = "select SUM(rejected_qty) from tspl_srn_detail where srn_no='" + txtDocNo.Value + "'"
        Dim strDocTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sqlRejctdQTY))

        ''
        ''Calculation Of Total MRN_Quantity   
        ''
        Dim TotalMRNQTY As String = "select SUM(TSPL_MRN_DETAIL.MRN_Qty) from TSPL_SRN_DETAIl left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL .MRN_No=TSPL_SRN_DETAIL.MRN_Id and TSPL_MRN_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code where SRN_No='" + txtDocNo.Value + "'"
        Dim MRNQtyTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(TotalMRNQTY))

        ''
        ''Calculation Of SRN_Qty
        ''
        Dim TotalSRNQTY As String = "select SUM(SRN_qty) from tspl_srn_detail where srn_no='" + txtDocNo.Value + "'"
        Dim SRNQtyTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(TotalSRNQTY))


        ''
        ''Checking Of PI_No. that it is single or more than single
        ''
        Dim chkPINo As String = "select case when COUNT(xxx.PI_No)>1 then Min(xxx.PI_No)+ ' *' else Min(xxx.PI_No)end as PINO from" &
"( select TSPL_PI_DETAIL.PI_No from TSPL_PI_DETAIL  where  TSPL_PI_DETAIL.SRN_Id= '" + txtDocNo.Value + "'" &
"GROUP by TSPL_PI_DETAIL.PI_No)xxx"
        Dim PInvNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(chkPINo))



        Dim strquery As String = "SELECT    TSPL_SRN_HEAD.SRN_No, TSPL_SRN_HEAD.SRN_Date, TSPL_SRN_HEAD.Vendor_Name,(case when len(against_mrn)>0 then (select MRN_Date  from tspl_mrn_head where tspl_mrn_head.MRN_No =against_mrn) else SRN_Date end ) as Challan_Date, TSPL_SRN_HEAD.Ref_No  " &
                      "as Challan_No, TSPL_SRN_HEAD.Inv_No, TSPL_SRN_HEAD.Inv_Date, TSPL_SRN_HEAD.GRNo,TSPL_SRN_HEAD.Amount_Less_Discount ,TSPL_SRN_HEAD.GENo,TSPL_SRN_HEAD.SRN_Total_Amt, " &
                      "TSPL_SRN_HEAD.GEDate, TSPL_SRN_HEAD.VehicleNo, TSPL_SRN_HEAD.Carrier,TSPL_SRN_HEAD.Remarks,TSPL_SRN_HEAD.Total_Landed_Cost as Total_Landed_Cost,TSPL_SRN_DETAIL .Landed_Cost_Rate as Landed_Cost_Rate,TSPL_SRN_DETAIL .Landed_Cost_Amount as Landed_Cost_Amount , TSPL_SRN_DETAIL.Item_Code,TSPL_SRN_DETAIL.Row_Type,TSPL_SRN_DETAIL.Amt_Less_Discount," &
"TSPL_SRN_DETAIL.Item_Cost as basicRate,TSPL_SRN_DETAIL.Item_Net_Amt as BasicTotal,TSPL_SRN_DETAIL.Unit_Cost_Tax_Rate as UCTR," &
"TSPL_SRN_DETAIL.Unit_Cost_Tax as uctax,(TSPL_SRN_DETAIL.Item_Desc) as Item_Desc,(TSPL_SRN_DETAIL.Specification )as Specification,TSPL_SRN_DETAIL.Unit_code,TSPL_SRN_DETAIL.SRN_Qty,TSPL_SRN_DETAIL.Rejected_Qty,TSPL_SRN_HEAD.Vendor_Code,TSPL_SRN_HEAD.SRN_Total_Amt,TSPL_SRN_DETAIL.ITEM_COST," &
 "TSPL_VENDOR_MASTER.Add1 as venAdd1, TSPL_VENDOR_MASTER.Add2 as vanadd2, TSPL_VENDOR_MASTER.Add3 as venadd3, " &
"tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SRN_HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name," &
"isnull (TSPL_SRN_HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SRN_HEAD.tax3_amt,0) as txt3amt," &
"tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SRN_HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name," &
"isnull (TSPL_SRN_HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SRN_HEAD.tax6_amt,0) as txt6amt " &
",tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SRN_HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name," &
"isnull (TSPL_SRN_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SRN_HEAD.tax9_amt,0) as txt9amt," &
"tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SRN_HEAD.tax10_amt,0) as txt10amt, TSPL_COMPANY_MASTER.Comp_Name as compname, " &
"TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_SRN_DETAIL.SRN_Qty," &
"case when tax1.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax1_amt else null end as Tax1Recoverable," &
"case when tax2.Tax_Recoverable='Y' then TSPL_SRN_HEAD.TAX2_Amt else null end as Tax2Recoverable, " &
"case when tax3.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax3_amt else null end as Tax3Recoverable, " &
"case when tax4.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax4_amt else null end as Tax4Recoverable, " &
"case when tax5.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax5_amt else null end as Tax5Recoverable, " &
"case when tax6.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax6_amt else null end as Tax6Recoverable," &
"case when tax7.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax7_amt else null end as Tax7Recoverable, " &
"case when tax8.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax8_amt else null end as Tax8Recoverable, " &
"case when tax9.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax9_amt else null end as Tax9Recoverable," &
"case when tax10.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax10_amt else null end as Tax10Recoverable, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX1_Rate ,0),103)+'%' as txt1Rate," &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX2_Rate   ,0),103)+'%' as txt2Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX3_Rate  ,0),103)+'%' as txt3Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX4_Rate  ,0),103)+'%' as txt4Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX5_Rate  ,0),103)+'%' as txt5Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX6_Rate  ,0),103)+'%' as txt6Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX7_Rate  ,0),103)+'%' as txt7Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX8_Rate  ,0),103)+'%' as txt8Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX9_Rate  ,0),103)+'%' as txt9Rate, " &
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX10_Rate  ,0),103)+'%' as txt10Rate," &
"TSPL_SRN_DETAIL.Amt_Less_Discount as Value,'" + strDocTotal + "' as Rej_qty, '" + MRNQtyTotal + "' " &
"as MrnTotQty, '" + SRNQtyTotal + "' as SRNQtyTotal, '" + PInvNo + "' as PInvNo  ,    " &
       " TSPL_SRN_HEAD.Add_Charge_Name1 as Add1Name, " &
     " TSPL_SRN_HEAD.Add_Charge_Amt1 as Add1 , " &
     "     TSPL_SRN_HEAD.Add_Charge_Name2 as Add2Name, " &
     "   TSPL_SRN_HEAD.Add_Charge_Amt2 as Add2 , " &
     "    TSPL_SRN_HEAD.Add_Charge_Name3 as Add3Name, " &
     "   TSPL_SRN_HEAD.Add_Charge_Amt3 as Add3 , " &
     "    TSPL_SRN_HEAD.Add_Charge_Name4 as Add4Name, " &
     "    TSPL_SRN_HEAD.Add_Charge_Amt4 as Add4 , " &
     "     TSPL_SRN_HEAD.Add_Charge_Name5 as Add5Name, " &
      "     TSPL_SRN_HEAD.Add_Charge_Amt5 as Add5 , " &
      "     TSPL_SRN_HEAD.Add_Charge_Name6 as Add6Name, " &
      "    TSPL_SRN_HEAD.Add_Charge_Amt6 as Add6 , " &
      "    TSPL_SRN_HEAD.Add_Charge_Name7 as Add7Name, " &
      "     TSPL_SRN_HEAD.Add_Charge_Amt7 as Add7 , " &
      "       TSPL_SRN_HEAD.Add_Charge_Name8 as Add8Name, " &
      "      TSPL_SRN_HEAD.Add_Charge_Amt8 as Add8 , " &
       "      TSPL_SRN_HEAD.Add_Charge_Name9 as Add9Name, " &
       "      TSPL_SRN_HEAD.Add_Charge_Amt9 as Add9 , " &
       "      TSPL_SRN_HEAD.Add_Charge_Name10 as Add10Name, " &
       "     TSPL_SRN_HEAD.Add_Charge_Amt10 as Add10,TSPL_SRN_HEAD.Against_RGP ,TSPL_RGP_HEAD .RGP_Date, TSPL_RGP_HEAD.Delivered_By    " &
 " FROM  TSPL_SRN_DETAIL INNER JOIN TSPL_SRN_HEAD ON TSPL_SRN_DETAIL.SRN_No = TSPL_SRN_HEAD.SRN_No " &
 "INNER JOIN TSPL_COMPANY_MASTER ON TSPL_SRN_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  " &
 "INNER JOIN TSPL_VENDOR_MASTER ON TSPL_SRN_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code " &
 "left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SRN_HEAD.tax1  " &
 "left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SRN_HEAD.tax2 " &
 "left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SRN_HEAD .TAX3 " &
 "left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SRN_HEAD .tax4 " &
 "left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SRN_HEAD .tax5 " &
 "left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SRN_HEAD .TAX6  " &
 "left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SRN_HEAD .TAX7  " &
 "left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SRN_HEAD .TAX8 " &
 "left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SRN_HEAD .TAX9 " &
 " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SRN_HEAD .TAX10 left outer join TSPL_RGP_HEAD on TSPL_SRN_HEAD .Against_RGP =TSPL_RGP_HEAD .RGP_No  " &
 " where TSPL_SRN_HEAD.SRN_No='" + txtDocNo.Value + "'"



        If txtDocNo.Value = "" Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select the SRN No.", Me.Text)
        Else
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strquery)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "SRNReport", "Store Receipt Report")
            frmCRV = Nothing
        End If


    End Sub
    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try

            If e.Column.Index >= 0 Then
                If (e.Column Is gv1.Columns(colExpiry)) OrElse (e.Column Is gv1.Columns(colManufactureDate)) Then
                    gv1.Columns(colExpiry).FormatString = "{0:d}"
                ElseIf e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colOrgMRNQty) OrElse e.Column Is gv1.Columns(colOrgGRNQty) OrElse e.Column Is gv1.Columns(colOrgPOQty) Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colPONo).Value) > 0 OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colScheduleNo).Value) > 0 OrElse (clsCommon.myLen(gv1.CurrentRow.Cells(colRGPNo).Value) > 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colRGPNo).Value) > 0) Then
                        gv1.CurrentRow.Cells(colICode).ReadOnly = True
                        If isVendorItemDetailSetting Then
                            gv1.CurrentRow.Cells(colMRP).ReadOnly = False
                            gv1.CurrentRow.Cells(colAbatementRate).ReadOnly = False
                        Else
                            gv1.CurrentRow.Cells(colMRP).ReadOnly = True
                            gv1.CurrentRow.Cells(colAbatementRate).ReadOnly = True
                        End If

                        'gv1.CurrentRow.Cells(colOrgGRNQty).ReadOnly = True
                        'gv1.CurrentRow.Cells(colOrgMRNQty).ReadOnly = True
                        gv1.CurrentRow.Cells(colOrgPOQty).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colICode).ReadOnly = False
                        gv1.CurrentRow.Cells(colMRP).ReadOnly = Not clsCommon.myCBool(gv1.CurrentRow.Cells(colisMRPMandatory).Value)
                        'gv1.CurrentRow.Cells(colOrgGRNQty).ReadOnly = False
                        'gv1.CurrentRow.Cells(colOrgMRNQty).ReadOnly = False
                        gv1.CurrentRow.Cells(colOrgPOQty).ReadOnly = False
                    End If
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colRGPNo).Value) > 0 Then
                        gv1.CurrentRow.Cells(colICode).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colICode).ReadOnly = False
                    End If
                ElseIf e.Column Is gv1.Columns(colQty) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colQty).ReadOnly = False
                        gv1.CurrentRow.Cells(colFreeQty).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colQty).ReadOnly = True
                        gv1.CurrentRow.Cells(colFreeQty).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colUnit) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colPONo).Value) <= 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colRGPNo).Value) <= 0 Then 'AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colMRN_NO).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colUnit).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colUnit).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colLeakQty) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.CompairString((cboItemType.SelectedValue), "F") = CompairStringResult.Equal AndAlso clsCommon.myCBool(gv1.CurrentRow.Cells(colIsEmptyValue).Value) Then
                        gv1.CurrentRow.Cells(colLeakQty).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colLeakQty).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colBurstQty) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.CompairString((cboItemType.SelectedValue), "F") = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colBurstQty).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colBurstQty).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colShortQty) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.CompairString((cboItemType.SelectedValue), "F") = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colShortQty).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colShortQty).ReadOnly = is_Load_MRN
                    End If
                ElseIf e.Column Is gv1.Columns(colRate) Then
                    If clsCommon.CompairString(IsRateEditable, "0") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRNUnitCost).Value) <= 0 Then
                            If clsCommon.myLen(txtPONo.Value) > 0 AndAlso Not chkConfirmatoryPO.Checked Then
                                gv1.CurrentRow.Cells(colRate).ReadOnly = True
                            Else
                                gv1.CurrentRow.Cells(colRate).ReadOnly = False
                            End If
                        Else
                            gv1.CurrentRow.Cells(colRate).ReadOnly = True
                        End If
                    Else
                        gv1.CurrentRow.Cells(colRate).ReadOnly = False
                    End If



                ElseIf e.Column Is gv1.Columns(colAmt) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsInsurance).Value) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(IIf(is_Load_MRN, colMRN_NO, colPONo)).Value) <= 0 Then
                            gv1.CurrentRow.Cells(colAmt).ReadOnly = True
                        Else
                            gv1.CurrentRow.Cells(colAmt).ReadOnly = False
                        End If

                    ElseIf clsCommon.myCdbl(gv1.CurrentRow.Cells(colIsMannualAmt).Value) = 1 Then
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colInsurancePer) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colInsurancePer).ReadOnly = True
                    ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colIsInsurance).Value) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(IIf(is_Load_MRN, colMRN_NO, colPONo)).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colInsurancePer).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colInsurancePer).ReadOnly = True
                    End If
                End If
                'Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                'cell.GradientStyle = GradientStyles.Solid
                'cell.BackColor = Color.FromArgb(243, 181, 51)

                If e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRejectedQty) OrElse e.Column Is gv1.Columns(colLeakQty) OrElse e.Column Is gv1.Columns(colBurstQty) OrElse e.Column Is gv1.Columns(colShortQty) Then
                    If clsCommon.myLen(txtQCcode.Text) > 0 Then
                        'gv1.CurrentRow.Cells(colQty).ReadOnly = True  'done against udl points by stuti on 23/02/2016
                        gv1.CurrentRow.Cells(colLeakQty).ReadOnly = True
                        gv1.CurrentRow.Cells(colBurstQty).ReadOnly = True
                        gv1.CurrentRow.Cells(colShortQty).ReadOnly = True
                        gv1.CurrentRow.Cells(colRejectedQty).ReadOnly = True
                        gv1.CurrentRow.Cells(colOrgMRNQty).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colOrgMRNQty).ReadOnly = False
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.CompairString((cboItemType.SelectedValue), "F") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colShortQty).ReadOnly = False
                        Else
                            gv1.CurrentRow.Cells(colShortQty).ReadOnly = is_Load_MRN
                        End If

                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.CompairString((cboItemType.SelectedValue), "F") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colBurstQty).ReadOnly = False
                        Else
                            gv1.CurrentRow.Cells(colBurstQty).ReadOnly = True
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colQty).ReadOnly = False
                            gv1.CurrentRow.Cells(colFreeQty).ReadOnly = False
                        Else
                            gv1.CurrentRow.Cells(colQty).ReadOnly = True
                            gv1.CurrentRow.Cells(colFreeQty).ReadOnly = True
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.CompairString((cboItemType.SelectedValue), "F") = CompairStringResult.Equal AndAlso clsCommon.myCBool(gv1.CurrentRow.Cells(colIsEmptyValue).Value) Then
                            gv1.CurrentRow.Cells(colLeakQty).ReadOnly = False
                        Else
                            gv1.CurrentRow.Cells(colLeakQty).ReadOnly = True
                        End If
                    End If ''end cond
                End If
                ''=======================================================
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnAddCost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCost.Click
        Dim frmInvoice As New FrmAPInvoiceEntry
        frmInvoice.SetUserMgmt(clsUserMgtCode.mbtnAPInvoiceEntry)
        frmInvoice.Show()
        frmInvoice.cmbRefType.SelectedValue = "S"
        frmInvoice.txtRefDocNo.Value = txtDocNo.Value
    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If

    End Sub
    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
        RefreshReqNo()
        RefreshGRPNo()
    End Sub
    Private Sub txtDept__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDept._MYValidating
        Try
            Dim obj As clsDepartment = clsDepartment.Finder(txtDept.Value, isButtonClicked)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                txtDept.Value = obj.Code
                lblDept.Text = obj.Name
            Else
                txtDept.Value = ""
                lblDept.Text = ""
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtReqNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPONo._MYValidating
        If is_Load_MRN Then
            SelectMRNItemsWithoutPO()
        Else
            SelectMRNItems()
        End If
    End Sub
    Sub RefreshReqNo()
        txtPONo.Value = ""
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String
                If is_Load_MRN Then
                    strReqNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT top 1 GRN_Id FROM TSPL_MRN_DETAIL WHERE MRN_No='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colMRN_NO).Value) + "' "))
                    txtPONo.Tag = strReqNo
                    isAgainstTender = clsPurchaseOrderHead.AgainstTender(strReqNo, 1, Nothing)
                Else
                    strReqNo = clsCommon.myCstr(gv1.Rows(ii).Cells(colPONo).Value)
                End If
                If clsCommon.myLen(strReqNo) > 0 Then
                    txtPONo.Value = clsCommon.myCstr(strReqNo)
                    Exit Sub
                End If
            Next
        End If
    End Sub


    Sub GetStateCode()
        Try
            Dim qry As String = "select TSPL_LOCATION_MASTER.Location_Code,tspl_purchase_order_head.MCC_Purchase from TSPL_LOCATION_MASTER left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.State_Code=TSPL_LOCATION_MASTER.State where TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No='" + IIf(is_Load_MRN, txtPONo.Tag, txtPONo.Value) + "' and TSPL_PURCHASE_ORDER_HEAD.MCC_Purchase='1'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim counter As Integer = 0

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.myCstr(dt.Rows(0)("MCC_Purchase")) = "1" Then
                    For Each dr As DataRow In dt.Rows()
                        If clsCommon.CompairString(dr("Location_Code"), clsCommon.myCstr(txtBillToLocation.Value)) = CompairStringResult.Equal Then
                            counter += 1
                        End If
                    Next
                Else
                    counter = 1
                End If
            End If

            If counter <= 0 Then
                txtBillToLocation.Value = ""
                lblBillToLocation.Text = ""
                Throw New Exception("Selected location does not match with mapped State on Purchase Order")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub RefreshGRPNo()
        If clsCommon.myLen(txtRGPNo.Value) <= 0 Then
            txtRGPNo.Value = ""
        End If

        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strRGPNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRGPNo).Value)
                If clsCommon.myLen(strRGPNo) > 0 Then
                    txtRGPNo.Value = clsCommon.myCstr(strRGPNo)
                    Exit Sub
                End If
            Next
        End If
    End Sub
    Private Sub btnrejetprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrejetprint.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) = 0 Then
                common.clsCommon.MyMessageBoxShow("Select the SRN No.")
            Else



                Dim qry As String = "Select rmda_no from tspl_srn_head where srn_no='" + txtDocNo.Value + "'"
                'Dim dr As SqlDataReader
                Dim val As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                'dr = connectSql.RunSqlReturnDR(qry)
                'While dr.Read()
                '    val = dr(0).ToString
                'End While
                If val <> "" Then
                    funrejprint()
                Else
                    common.clsCommon.MyMessageBoxShow("RMDA does not exist for this SRN No")
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub funrejprint()
        Try
            Dim qry As String = "SELECT TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode, TSPL_SRN_HEAD.Created_By ,TSPL_SRN_HEAD .Modify_By,  TSPL_SRN_HEAD.SRN_No, TSPL_SRN_HEAD.SRN_Date, TSPL_SRN_HEAD.Vendor_Name, TSPL_SRN_HEAD.Ship_To_Location, TSPL_SRN_HEAD.Bill_To_Location, TSPL_SRN_HEAD.RMDA_No, TSPL_SRN_HEAD.RMDA_Date,TSPL_SRN_HEAD.Remarks,TSPL_SRN_HEAD.Description, TSPL_SRN_DETAIL.Item_Code,   TSPL_SRN_DETAIL.Item_Desc, TSPL_SRN_DETAIL.Rejected_Qty, TSPL_SRN_DETAIL.Item_Cost,TSPL_SRN_DETAIL.Unit_code,TSPL_SRN_DETAIL.Rejected_Qty*TSPL_SRN_DETAIL.Item_Cost as Amount,TSPL_COMPANY_MASTER.comp_name,TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2  FROM         TSPL_SRN_HEAD INNER JOIN    TSPL_SRN_DETAIL ON TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_No LEFT OUTER JOIN     TSPL_COMPANY_MASTER ON TSPL_SRN_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SRN_HEAD.Bill_To_Location left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_SRN_HEAD.Vendor_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_SRN_DETAIL.Item_Code left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code   where TSPL_SRN_HEAD.SRN_No='" + txtDocNo.Value + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptMRDA", "MRDA Report", clsCommon.myCDate(dt.Rows(0)("SRN_Date")))
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtRGPNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRGPNo._MYValidating
        '==if rgp after po and against bom job-work then no pending window open.
        If clsCommon.CompairString(cboSRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso isRGPAfterPO AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "AB") = CompairStringResult.Equal Then
            Exit Sub
        End If
        '==========end here=========================
        SelectRGPItems()
    End Sub
    Public Shared Function GetItemType(ByVal strItmType As String) As String
        Dim qry As String = "select distinct Item_Type  from TSPL_ITEM_MASTER where Item_Code ='" + strItmType + "'"
        strItmType = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        If strItmType = "F" Then
            strItmType = 0
        Else
            strItmType = 1
        End If
        Return strItmType
    End Function
    Public Function GetTaxGrp(ByVal strItmType As String) As String
        Dim qry As String = "select Tax_Group  from TSPL_VENDOR_MASTER where Vendor_Code ='" + strItmType + "'"
        strItmType = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return strItmType
    End Function
    Sub SelectRGPItems()

        Load_RGP_DT()
        Dim IsJobWork As String = ""
        isInsideLoadData = True
        Dim frm As New frmPendingRGP()
        frm.VendorCode = clsCommon.myCstr(txtVendorNo.Value)
        frm.strCurrCode = clsCommon.myCstr(txtDocNo.Value)
        frm.strJobWorkType = clsCommon.myCstr(cmbRGPType.SelectedValue)
        If Not is_Load_MRN AndAlso isRGPAfterPO Then
            frm.strRGPType = clsCommon.myCstr(cboSRNType.SelectedValue)
            If clsCommon.CompairString(cboSRNType.SelectedValue, "J") <> CompairStringResult.Equal Then 'so that data fetch from rgp_detail table exclude job-work,because job-work data come from rgp_job_work_detail table.
                frm.strWhrCond = "J"
            End If
        ElseIf clsCommon.CompairString(cboSRNType.SelectedValue, "J") <> CompairStringResult.Equal Then 'this condition do for normal process as did earlier,where "rgp_job_work_detail" table is not used.
            frm.strRGPType = clsCommon.myCstr(cboSRNType.SelectedValue)
        End If
        frm.ShowDialog()

        If (frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0) OrElse (frm.ArrReturn_Job IsNot Nothing AndAlso frm.ArrReturn_Job.Count > 0) Then
            If frm.ArrReturn.Count > 0 AndAlso clsCommon.myLen(frm.ArrReturn(0).RGP_No) > 0 Then
                Dim objMRNHead As clsRGPHead = clsRGPHead.GetData(frm.ArrReturn(0).RGP_No, NavigatorType.Current)
                If objMRNHead IsNot Nothing AndAlso clsCommon.myLen(objMRNHead.RGP_No) > 0 Then
                    '' Anubhooti 11-Dec-2014 BM00000005133 (If Job Work Type then fill RGP grid else SRN grid)
                    IsJobWork = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Against_JobWork,0) AS Against_JobWork From TSPL_RGP_HEAD  Where RGP_No='" & clsCommon.myCstr(objMRNHead.RGP_No) & "'"))
                    'If clsCommon.CompairString(objMRNHead.Against_JobWork, "1") = CompairStringResult.Equal Then

                    'End If
                    If clsCommon.myLen(txtCarrier.Text) <= 0 Then
                        txtVehicleNo.Text = objMRNHead.VehicleNo
                    End If
                    txtRGPNo.Value = objMRNHead.RGP_No

                    'If clsCommon.myLen(txtRemarks.Text) <= 0 Then
                    '    txtRemarks.Text = objMRNHead.Remarks
                    'End If
                    If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
                        txtBillToLocation.Value = objMRNHead.Location
                        lblBillToLocation.Text = objMRNHead.LocationName
                    End If
                    If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                        txtVendorNo.Value = frm.VendorCode
                        lblVendorName.Text = frm.VendorName
                        txtTaxGroup.Value = GetTaxGrp(txtVendorNo.Value)
                        SetTaxDetails()
                    End If
                    chkRGPNonInventory.Checked = IIf(objMRNHead.Is_Non_Inventory = 1, True, False)

                    cboItemType.SelectedValue = objMRNHead.ItemType
                    cboSRNType.SelectedValue = "J"

                End If
            End If

            If frm.ArrReturn_Job.Count > 0 AndAlso clsCommon.myLen(frm.ArrReturn_Job(0).RGP_No) > 0 Then
                Dim objMRNHead As clsRGPHead = clsRGPHead.GetData(frm.ArrReturn_Job(0).RGP_No, NavigatorType.Current)
                If objMRNHead IsNot Nothing AndAlso clsCommon.myLen(objMRNHead.RGP_No) > 0 Then
                    '' Anubhooti 11-Dec-2014 BM00000005133 (If Job Work Type then fill RGP grid else SRN grid)
                    IsJobWork = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Against_JobWork,0) AS Against_JobWork From TSPL_RGP_HEAD  Where RGP_No='" & clsCommon.myCstr(objMRNHead.RGP_No) & "'"))
                    'If clsCommon.CompairString(objMRNHead.Against_JobWork, "1") = CompairStringResult.Equal Then

                    'End If
                    If clsCommon.myLen(txtCarrier.Text) <= 0 Then
                        txtVehicleNo.Text = objMRNHead.VehicleNo
                    End If
                    txtRGPNo.Value = objMRNHead.RGP_No

                    'If clsCommon.myLen(txtRemarks.Text) <= 0 Then
                    '    txtRemarks.Text = objMRNHead.Remarks
                    'End If
                    If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
                        txtBillToLocation.Value = objMRNHead.Location
                        lblBillToLocation.Text = objMRNHead.LocationName
                    End If
                    If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                        txtVendorNo.Value = frm.VendorCode
                        lblVendorName.Text = frm.VendorName
                        txtTaxGroup.Value = GetTaxGrp(txtVendorNo.Value)
                        SetTaxDetails()
                    End If
                    chkRGPNonInventory.Checked = IIf(objMRNHead.Is_Non_Inventory = 1, True, False)

                    cboItemType.SelectedValue = objMRNHead.ItemType
                    cboSRNType.SelectedValue = "J"
                    If objMRNHead.Against_JobWork = 1 AndAlso objMRNHead.Against_BOM = 0 AndAlso objMRNHead.Against_As_It_Is = 0 Then
                        cmbRGPType.SelectedValue = "AR"
                    ElseIf objMRNHead.Against_JobWork = 1 AndAlso objMRNHead.Against_BOM = 1 AndAlso objMRNHead.Against_As_It_Is = 0 Then
                        cmbRGPType.SelectedValue = "AB"
                    ElseIf objMRNHead.Against_JobWork = 1 AndAlso objMRNHead.Against_BOM = 0 AndAlso objMRNHead.Against_As_It_Is = 1 Then
                        cmbRGPType.SelectedValue = "AI"
                    End If
                End If
            End If

            gv1.Rows.Clear()
            '' Anubhooti 11-Dec-2014 BM00000005133 (If Job Work Type then fill RGP grid else SRN grid)
            If clsCommon.CompairString(IsJobWork, "1") = CompairStringResult.Equal Then
                If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                    gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                End If

                If Not is_Load_MRN AndAlso isRGPAfterPO Then
                    For Each objRGPDetail As clsRGPBOMItem In frm.ArrReturn_Job
                        If IsValidRGPJOBWORKItem(objRGPDetail) Then
                            gv1.Rows.AddNew()
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = objRGPDetail.PO_Id
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = objRGPDetail.RGP_No
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colScheduleNo).Value = objRGPDetail.Against_Schedule_Code

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objRGPDetail.Item_Code
                            cboItemType.SelectedValue = clsItemMaster.GetItemType(objRGPDetail.Item_Code, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objRGPDetail.Iname

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objRGPDetail.Item_Code, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objRGPDetail.Item_Code, Nothing)


                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(objRGPDetail.Item_Code)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objRGPDetail.Item_Code)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(objRGPDetail.Item_Code)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRNUnitCost).Value = Nothing
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = Nothing

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objRGPDetail.Rate
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgRGPQty).Value = objRGPDetail.RGP_Qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objRGPDetail.Unit_Code


                            If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "F") = CompairStringResult.Equal Then
                                Dim strFCode As String = clsItemMaster.GetFatherCode(objRGPDetail.Item_Code, Nothing)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colFCode).Value = strFCode
                                If clsCommon.myLen(strFCode) > 0 Then
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFRate).Value = clsItemPriceMaster.GetMRPOfFinishItem(strFCode, objRGPDetail.Unit_Code)
                                End If
                            End If

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgPOQty).Value = Nothing
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objRGPDetail.Balance_Qty


                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = objRGPDetail.Balance_Qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDisType).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objRGPDetail.MRP
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = Nothing
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(objRGPDetail.Item_Code)


                        End If 'valid cond.

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementRate).Value = Nothing
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssesableMRP).Value = Nothing
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssesableMRP).Value = Nothing
                    Next
                Else

                    LoadRGPBlankGrid()
                    LoadRGPData(txtRGPNo.Value, NavigatorType.Current)
                    gvRGP.Visible = True
                    For Each obj As clsRGPDetail In frm.ArrReturn
                        If IsValidItemForRGP(obj) Then
                            'gv1.Rows.AddNew()
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = obj.RGP_No
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(obj.Item_Code)
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(obj.Item_Code)
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(obj.Item_Code)
                            'cboItemType.SelectedIndex = GetItemType(obj.Item_Code)
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                            ' ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = obj.Location
                            ' ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = obj.LocationName
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.RGP_Qty
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.RGP_Qty
                            DtRGP.Rows.Add()
                            DtRGP.Rows(DtRGP.Rows.Count - 1).Item(colRGPNo) = obj.RGP_No
                            DtRGP.Rows(DtRGP.Rows.Count - 1).Item(colICode) = obj.Item_Code
                            ' DtRGP.Rows(DtRGP.Rows.Count - 1).Item(colLocationCode) = obj.Location
                            DtRGP.Rows(DtRGP.Rows.Count - 1).Item(colQty) = obj.RGP_Qty
                            'DtRGP.Rows(DtRGP.Rows.Count - 1).Item(colLocationName) = obj.RGP_Qty
                        End If
                    Next
                    SplitContainer3.Panel1Collapsed = False


                End If '' load mrn cond.

                SetitemWiseTaxSetting(False, False)
                For ii As Integer = 0 To gv1.RowCount - 1
                    UpdateCurrentRow(ii)
                Next
            End If '' job-work cond.

        End If
        isInsideLoadData = False
        UpdateAllTotals()
        ' RefreshGRPNo()


        If Not is_Load_MRN AndAlso isRGPAfterPO Then
        Else
            cboItemType.SelectedValue = "O"
            Dim IsMTO As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Item_Conversion_Type,'') AS Item_Conversion_Type From TSPL_RGP_HEAD  Where RGP_No='" & clsCommon.myCstr(txtRGPNo.Value) & "'"))

            If clsCommon.CompairString(IsJobWork, "1") = CompairStringResult.Equal Then
                If clsCommon.CompairString(IsMTO, "M") = CompairStringResult.Equal Then
                    gvRGP.Enabled = False
                    LoadRGP_Items_Data("", NavigatorType.Current, True)
                Else
                    gvRGP.Enabled = True
                End If
            Else
                LoadRGPDataWOJobWork(txtRGPNo.Value, NavigatorType.Current)
                gvRGP.Visible = False
            End If
        End If

    End Sub
    Sub LoadRGPDataWOJobWork(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As New clsRGPHead()
        Try
            'LoadRGPBlankGrid()
            'IsfromRGP = True

            obj = clsRGPHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.RGP_No) > 0) Then
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    gv1.Rows.Clear()
                    For Each objTr As clsRGPDetail In obj.Arr
                        isInsideLoadData = True
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColSelect).Value = False
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objTr.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        '' Anubhooti 09-Dec-2014 (Fetch More Columns)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.RGP_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsRGPDetail.GetBalanceRGPQty(objTr.RGP_No, objTr.Item_Code, txtDocNo.Value, objTr.Unit_code)
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colSp).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = objTr.RGP_No

                        If clsCommon.CompairString(clsCommon.myCstr(obj.Item_Conversion_Type), "N") = CompairStringResult.Equal Then
                            obj.Item_Conversion_Type = "None"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Item_Conversion_Type), "O") = CompairStringResult.Equal Then
                            obj.Item_Conversion_Type = "One To Many"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Item_Conversion_Type), "M") = CompairStringResult.Equal Then
                            obj.Item_Conversion_Type = "Many To One"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Item_Conversion_Type), "") = CompairStringResult.Equal Then
                            obj.Item_Conversion_Type = "None"
                        End If
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colItemConType).Value = obj.Item_Conversion_Type
                        'End If
                    Next
                    If obj.Status = ERPTransactionStatus.Pending Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                    End If
                    SetitemWiseTaxSetting(False, False)
                    For ii As Integer = 0 To gv1.RowCount - 1
                        UpdateCurrentRow(ii)
                    Next
                    isInsideLoadData = False
                    UpdateAllTotals()
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        Finally
            obj = Nothing
        End Try
    End Sub
    Sub Load_RGP_DT()
        DtRGP = New DataTable()
        DtRGP.Columns.Add(colRGPNo)
        DtRGP.Columns.Add(colICode)
        DtRGP.Columns.Add(colQty)
    End Sub
    Function IsValidItemForRGP(ByVal obj As clsRGPDetail)
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strAgaintRGPCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRGPNo).Value)
            If clsCommon.myLen(strAgaintRGPCode) > 0 AndAlso clsCommon.CompairString(strAgaintRGPCode, obj.RGP_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow("RGP No : " + obj.RGP_No + "  Item : " + obj.Item_Desc + Environment.NewLine + "Already exist at row no:" + clsCommon.myCstr(ii + 1))
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Try
            Dim arrTaxableAuth As New List(Of String)
            Dim strFCode As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFCode).Value)
            Dim dblFRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFRate).Value)
            Dim dblFAmt As Double = 0
            AllowSRNWithoutShortageRejection = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select AllowSRNWithoutShortReject from tspl_Item_Master where Item_code='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) & "'"))
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
            Dim dblReceivedQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOrgMRNQty).Value)
            Dim dblRejectedQty As Double = 0
            Dim dblShortageQty As Decimal = 0
            Dim dblAcceptedQty As Double = 0

            If AllowSRNWithoutShortageRejection = 0 Then
                If is_Load_MRN AndAlso isAgainstTender Then
                    dblShortageQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOrgGRNQty).Value) - clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOrgMRNQty).Value)
                    dblRejectedQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOrgMRNQty).Value) - clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
                ElseIf is_Load_MRN AndAlso clsCommon.myLen(gv1.Rows(IntRowNo).Cells(colMRN_NO).Value) > 0 Then
                    dblQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOrgGRNQty).Value)
                    dblShortageQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOrgGRNQty).Value) - clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOrgMRNQty).Value)
                    dblRejectedQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOrgMRNQty).Value) - clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
                Else
                    dblShortageQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colShortQty).Value)
                    dblRejectedQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOrgMRNQty).Value) - (clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) + dblShortageQty)
                End If

            Else
                dblShortageQty = 0
                dblRejectedQty = 0
            End If
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOrgMRNQty).Value) > clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOrgGRNQty).Value) Then
                    dblShortageQty = 0
                End If
            End If



            If dblRejectedQty < 0 Then
                dblRejectedQty = 0
                dblAcceptedQty = dblReceivedQty
                gv1.Rows(IntRowNo).Cells(colQty).Value = Math.Round(dblAcceptedQty, 3)
            End If


            Dim dblAmt As Double = 0
            Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
            dblAmt = (dblQty * dblRate)
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colIsMannualAmt).Value) = 0 Then
                gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
            ElseIf clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(colIsInsurance).Value) AndAlso clsCommon.myLen(gv1.Rows(IntRowNo).Cells(IIf(is_Load_MRN, colMRN_NO, colPONo)).Value) <= 0 Then
                dblAmt = (clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInsuranceBaseAmt).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInsurancePer).Value)) / 100
                gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
            Else
                dblAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmt).Value)
            End If

            Dim dblHeaderDisAmt As Decimal = Math.Round(dblAmt * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colHeaderDiscountPer).Value) / 100, 2, MidpointRounding.AwayFromZero)
            Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPer).Value)
            Dim dblDetailDisAmt As Decimal = (dblAmt * dblDisPer) / 100
            Dim dblDisAmt As Double = dblHeaderDisAmt + dblDetailDisAmt
            Dim dblAmtAfterDis As Double = dblAmt - dblDisAmt

            Dim dblTotAmt As Decimal = 0
            For jj As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(jj).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                    If jj = IntRowNo Then
                        dblTotAmt += dblAmt
                    Else
                        dblTotAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells(colAmt).Value)
                    End If
                End If
            Next
            Dim dclItemInsuranceAdditionalChargePart As Decimal = 0
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                dclItemInsuranceAdditionalChargePart = Math.Round(clsCommon.myCDivide((clsCommon.myCdbl(lblAddChargesForInsurance.Text)) * dblAmt, dblTotAmt), 2, MidpointRounding.AwayFromZero)
            Else
                gv1.Rows(IntRowNo).Cells(colItemInsurancePer).Value = 0
                gv1.Rows(IntRowNo).Cells(colItemInsuranceAmt).Value = 0
            End If
            Dim dclItemInsuranceBaseAmt As Decimal = dblAmtAfterDis + dclItemInsuranceAdditionalChargePart
            Dim dclItemInsuranceAmt As Decimal = 0
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colItemInsuranceApplyOn).Value), clsCalculationlApplyON.RowTypeApplyOnPercent) = CompairStringResult.Equal Then
                dclItemInsuranceAmt = dclItemInsuranceBaseAmt * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colItemInsurancePer).Value) / 100
            Else
                dclItemInsuranceAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colItemInsuranceAmt).Value)
            End If
            Dim dclItemAmtAfterInsurance As Decimal = dblAmtAfterDis + dclItemInsuranceAmt + dclItemInsuranceAdditionalChargePart


            Dim dblCurrentTaxablePer As Decimal = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colTaxableAmountPer).Value)
            Dim dblCurrentTaxableAmount As Decimal = dclItemAmtAfterInsurance * dblCurrentTaxablePer / 100

            '' abatement PO
            If IsAbatementPO Then
                gv1.Rows(IntRowNo).Cells(colAssesableMRP).Value = gv1.Rows(IntRowNo).Cells(colMRP).Value - (gv1.Rows(IntRowNo).Cells(colMRP).Value * gv1.Rows(IntRowNo).Cells(colAbatementRate).Value / 100)
                gv1.Rows(IntRowNo).Cells(colTotalAssesableMRP).Value = gv1.Rows(IntRowNo).Cells(colQty).Value * gv1.Rows(IntRowNo).Cells(colAssesableMRP).Value
            End If

            For ii As Integer = 1 To 10
                Dim Strii As String = clsCommon.myCstr(ii)
                If rbtnTaxCalAutomatic.IsChecked Then
                    Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        '' For abatement PO
                        Dim dtTax As DataTable = clsPurchaseOrderHead.GetTaxDetail(strTaxCode)
                        Dim IsExciseType As Boolean = False
                        If dtTax.Rows.Count > 0 Then
                            If clsCommon.CompairString(dtTax.Rows(0).Item("Tax Type"), "E", False) = CompairStringResult.Equal Then
                                IsExciseType = True
                            Else
                                IsExciseType = False
                            End If
                        Else
                            IsExciseType = False
                        End If
                        '' End For abatement PO
                        Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                        Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                        Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                        Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                        Dim IsExcisable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value)
                        Dim IsTaxOnBaseAmt As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + Strii)).Value)
                        Dim dblBaseAmt As Double = 0
                        Dim dblTaxAmt As Double = 0
                        If IsTaxOnBaseAmt Then
                            dblBaseAmt = dblCurrentTaxableAmount
                        ElseIf IsSurTax Then
                            Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                            dblBaseAmt = dblSurTaxAmt
                        Else
                            Dim dblOtherTaxAmt As Double = 0
                            dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                            If IsExciseType And IsAbatementPO Then
                                dblBaseAmt = (gv1.Rows(IntRowNo).Cells(colTotalAssesableMRP).Value + dblOtherTaxAmt)
                            Else
                                dblBaseAmt = (dblCurrentTaxableAmount + dblOtherTaxAmt)
                            End If
                        End If
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                        dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100

                        If ii = 1 Then
                            If chkExciseOnQty.Checked Then
                                gv1.Rows(IntRowNo).Cells(colAssessableAmount).Value = Math.Round(dblQty, 2)
                                dblTaxAmt = (dblQty * dblTaxRate) / 100
                            Else
                                gv1.Rows(IntRowNo).Cells(colAssessableAmount).Value = Math.Round(dblBaseAmt, 2)
                            End If
                        End If
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, IIf(objCommonVar.IsRoundOffTaxToZeroDecimal, 0, 2))
                        If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                            arrTaxableAuth.Add(strTaxCode.ToUpper())
                        End If
                    Else
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxRate" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + Strii)).Value = Nothing
                    End If
                ElseIf rbtnTaxCalManual.IsChecked Then
                    If gv2.Rows.Count >= ii Then
                        Dim dblTaxAmt As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxAmt).Value)
                        Dim dblTaxRate As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxRate).Value)
                        Dim dblCurrRowAmt As Double = clsCommon.myCdbl(gv1.Rows(clsCommon.myCdbl(IntRowNo)).Cells(colAmtAfterDis).Value)
                        dblTotAmt = 0
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            dblTotAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells(colAmtAfterDis).Value)
                        Next
                        Dim dblCurrCalTax As Double = 0
                        If dblTotAmt <> 0 Then
                            dblCurrCalTax = Math.Round(clsCommon.myCdbl(dblTaxAmt * dblCurrRowAmt / dblTotAmt), 2, MidpointRounding.ToEven)
                        End If
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = dblCurrRowAmt
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = dblCurrCalTax
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value = dblTaxRate
                    End If
                End If
            Next
            Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
            Dim dblAmtAfterTax As Double = dblAmtAfterDis + dclItemInsuranceAdditionalChargePart + dclItemInsuranceAmt + dblTotTaxAmt

            gv1.Rows(IntRowNo).Cells(colHeaderDiscountAmt).Value = Math.Round(dblHeaderDisAmt, 2)
            gv1.Rows(IntRowNo).Cells(colDetailDisAmt).Value = Math.Round(dblDetailDisAmt, 2)

            gv1.Rows(IntRowNo).Cells(colDisAmt).Value = Math.Round(dblDisAmt, 2)
            gv1.Rows(IntRowNo).Cells(colAmtAfterDis).Value = Math.Round(dblAmtAfterDis, 2)

            gv1.Rows(IntRowNo).Cells(colItemInsuranceBaseAmt).Value = Math.Round(dclItemInsuranceBaseAmt, 2)
            gv1.Rows(IntRowNo).Cells(colItemInsuranceAmt).Value = Math.Round(dclItemInsuranceAmt, 2)
            gv1.Rows(IntRowNo).Cells(colItemAmtAfterInsurance).Value = Math.Round(dclItemAmtAfterInsurance, 2)

            gv1.Rows(IntRowNo).Cells(colTaxableAmount).Value = Math.Round(dblCurrentTaxableAmount, 2)
            gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
            gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)
            gv1.Rows(IntRowNo).Cells(colFAmt).Value = Math.Round(dblFAmt, 2)

            'Asked By Ranjana mam
            If dblShortageQty < 0 Then
                gv1.Rows(IntRowNo).Cells(colShortQty).Value = 0
            Else
                dblShortageQty = Math.Round(dblShortageQty, 3)
                gv1.Rows(IntRowNo).Cells(colShortQty).Value = dblShortageQty
            End If
            gv1.Rows(IntRowNo).Cells(colRejectedQty).Value = Math.Round(dblRejectedQty, 3)

            Dim dblTQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRejectedQty).Value)
            'Dim dblAmtLessDiscountWithoutShortage As Decimal = Math.Round(clsCommon.myCDivide((dclItemAmtAfterInsurance * dblTQty), (dblTQty + dblShortageQty)), 2)
            'gv1.Rows(IntRowNo).Cells(colAmtLessDiscountWithoutShortage).Value = dblAmtLessDiscountWithoutShortage
            gv1.Rows(IntRowNo).Cells(colAmtLessDiscountWithoutShortage).Value = Math.Round((dblTQty * dblRate) * (100 - dblDisPer) / 100, 2)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub gvAC_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvAC.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvAC.Columns(colACAmount) Then

                        UpdateAllTotals()
                    ElseIf e.Column Is gvAC.Columns(colACCode) Then
                        Dim obj As clsAdditionalCharge = clsAdditionalCharge.GetFinder(clsCommon.myCstr(gvAC.CurrentRow.Cells(colACCode).Value), False)
                        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                            gvAC.CurrentRow.Cells(colACCode).Value = obj.Code
                            gvAC.CurrentRow.Cells(colACName).Value = obj.desc
                        Else
                            gvAC.CurrentRow.Cells(colACCode).Value = ""
                            gvAC.CurrentRow.Cells(colACName).Value = ""
                            gvAC.CurrentRow.Cells(colACAmount).Value = 0
                        End If
                    End If
                End If
                setGridFocusAC()
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub setGridFocusAC()
        Try
            Dim intCurrRow As Integer = gvAC.CurrentRow.Index
            If intCurrRow = gvAC.Rows.Count - 1 AndAlso gvAC.Rows.Count <= 10 Then
                gvAC.Rows.AddNew()
                gvAC.CurrentRow = gvAC.Rows(intCurrRow)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub
    Private Sub rbtnTaxCalAutomatic_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnTaxCalAutomatic.ToggleStateChanged, rbtnTaxCalManual.ToggleStateChanged
        If Not isInsideLoadData Then
            If rbtnTaxCalAutomatic.IsChecked Then
                SetTaxDetails()
                chkExciseOnQty.Enabled = True
            ElseIf rbtnTaxCalManual.IsChecked Then
                chkExciseOnQty.Checked = False
                chkExciseOnQty.Enabled = False
                For intRowNo As Integer = 0 To gv2.Rows.Count - 1
                    gv2.Rows(intRowNo).Cells(colTTaxRate).Value = Nothing
                    gv2.Rows(intRowNo).Cells(colTTaxAmt).Value = Nothing
                    gv2.Rows(intRowNo).Cells(colTBaseAmt).Value = Nothing
                Next
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    For ii As Integer = 1 To 10
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                    Next
                Next
            End If
        End If
    End Sub
    Private Sub gv2_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv2.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv2.Columns(colTTaxAmt)) Then
                    gv2.CurrentRow.Cells(colTTaxAmt).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                ElseIf (e.Column Is gv2.Columns(colTTaxRate)) Then
                    gv2.CurrentRow.Cells(colTTaxRate).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                End If
                ''Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                ''cell.GradientStyle = GradientStyles.Solid
                ''cell.BackColor = Color.FromArgb(243, 181, 51)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub gv2_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedTaxOpen Then
                    isCellValueChangedTaxOpen = True
                    If (e.Column Is (gv2.Columns(colTTaxAmt)) AndAlso rbtnTaxCalManual.IsChecked) Then
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRow(ii)
                        Next
                        UpdateAllTotals()
                    End If
                    isCellValueChangedTaxOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtRemarks_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInvNo.TextChanged

    End Sub
    Private Sub txtVendorNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVendorNo.Load

    End Sub
    Private Sub txtShipToLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShipToLocation.Load

    End Sub
    Private Sub txtDocNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDocNo.Load

    End Sub
    Private Sub gv1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.Alt AndAlso (e.KeyCode = Keys.F7) Then
            If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                gv1.CurrentRow.Cells(colIsMannualAmt).Value = IIf(clsCommon.myCdbl(gv1.CurrentRow.Cells(colIsMannualAmt).Value) = 1, 0, 1)
            End If

            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colIsMannualAmt).Value) = 0 Then
                UpdateCurrentRow(gv1.CurrentRow.Index)
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        ElseIf e.KeyCode = Keys.F5 Then

            OpenBatchItem()
        End If
    End Sub
    '===========Update by Richa Agarwal against ticket no ==========
    Sub OpenBatchItem()
        Dim blnBatchqty As Boolean = False
        If clsCommon.myCBool(clsItemMaster.IsBatchItem(gv1.CurrentRow.Cells(colICode).Value)) Then
            Dim frm As frmBatchItemIn = New frmBatchItemIn()
            frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
            frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
            frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
            frm.dblMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
            frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
            frm.ShowDialog()
            If Not frm.isCencelButtonClicked Then
                gv1.CurrentRow.Cells(colICode).Tag = frm.arr
            End If
        End If
    End Sub
    Private Sub gv1_RowFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gv1.RowFormatting
        Try
            If clsCommon.CompairString(clsCommon.myCstr(e.RowElement.RowInfo.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(e.RowElement.RowInfo.Cells(colIsMannualAmt).Value) > 0 Then
                e.RowElement.ForeColor = Color.Blue
            Else
                e.RowElement.ForeColor = Color.Black
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub txtRequistionNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRequistionNo._MYValidating
        SelectRequistionItems()
    End Sub
    Sub SelectRequistionItems()
        isInsideLoadData = True
        Dim frm As New FrmPendingRequistion()
        frm.VendorCode = txtVendorNo.Value
        frm.VendorName = lblVendorName.Text
        frm.strCurrCodeSRN = txtDocNo.Value
        frm.isFromSRN = True
        frm.ShowDialog()
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            Dim objReq As clsRequistionHead = clsRequistionHead.GetData(frm.ArrReturn(0).Requisition_Id, NavigatorType.Current, "")
            If objReq IsNot Nothing AndAlso clsCommon.myLen(objReq.Requisition_Id) > 0 Then

                If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                    txtVendorNo.Value = frm.VendorCode
                    lblVendorName.Text = frm.VendorName
                End If
                'If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
                txtBillToLocation.Value = objReq.Location
                lblBillToLocation.Text = objReq.LocationName
                cboSRNType.SelectedValue = objReq.Requisition_Type
                'End If
                If (clsCommon.myLen(txtDesc.Text) <= 0) Then
                    txtDesc.Text = objReq.Description
                End If
                If (clsCommon.myLen(txtRefNo.Text) <= 0) Then
                    txtRefNo.Text = objReq.Ref_No
                End If
                If (clsCommon.myLen(txtDept.Value) <= 0) Then
                    txtDept.Value = objReq.Dept
                    lblDept.Text = objReq.Dept_Desc
                End If
                If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
                    cboItemType.SelectedValue = objReq.Item_Type
                End If
                'richa 25/07/2014 Against Ticket No BM00000003211
                'If (clsCommon.myLen(txtRequistionNo.Value) <= 0) Then
                txtRequistionNo.Value = objReq.Requisition_Id
                'End If
                '----------------------------------------
            End If

            LoadBlankGrid()
            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If

            For Each obj As clsRequistionDetail In frm.ArrReturn
                If Not IsItemExistInGrid(obj) Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReqistionNo).Value = obj.Requisition_Id
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_Code
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = obj.Location
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = obj.LocationName
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.Balance_Qty
                    SetitemWiseTaxSetting(True, True)
                    UpdateCurrentRow(gv1.Rows.Count - 1)
                End If
            Next
        End If
        isInsideLoadData = False
        UpdateAllTotals()
        RefreshReqNo()
    End Sub
    Function IsItemExistInGrid(ByVal obj As clsRequistionDetail)
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colReqistionNo).Value)
            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strReqCode, obj.Requisition_Id) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow("Requition No : " + obj.Requisition_Id + "  Item : " + obj.Item_Desc + Environment.NewLine + "Already exist at row no:" + clsCommon.myCstr(ii + 1))
                Return True
            End If
        Next
        Return False
    End Function
    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' to check balance

                For ii As Integer = 0 To gv1.Rows.Count - 1
                    Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                    Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                    Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colOrgMRNQty).Value)
                    Dim dblRejQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRejectedQty).Value)
                    Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                    Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
                    If clsCommon.myLen(strICode) > 0 Then
                        Dim strLocation As String = txtShipToLocation.Value
                        Dim dblBalQty As Double = clsItemLocationDetails.getBalance(strICode, txtShipToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, strUOM, dblMRP)
                        If dblRejQty > 0 Then
                            Dim strLoc As String = clsDBFuncationality.getSingleValue("select Rejected_Location from TSPL_LOCATION_MASTER where Location_Code='" & txtShipToLocation.Value & "'")
                            If clsCommon.myLen(strLoc) > 0 Then
                                dblBalQty += clsItemLocationDetails.getBalance(strICode, strLoc, txtDocNo.Value, txtDate.Value, Nothing, strUOM, dblMRP)
                            End If
                        End If
                        If dblBalQty < (dblQty - dblRejQty) Then
                            Throw New Exception("You can't reverse this document because quantity of Item - " + strICode + " goes -ve")
                        End If
                    End If
                Next

                '---------------------
                If clsSRNHead.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub chkExciseOnQty_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkExciseOnQty.ToggleStateChanged
        If Not isInsideLoadData Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()
        End If
    End Sub
    Private Sub txtCurrencyCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCurrencyCode._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        txtCurrencyCode.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", txtCurrencyCode.Value, "CURRENCY_CODE", isButtonClicked)
        ShowCurrencyDetail()
    End Sub
    Sub setBalance()
        UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
        UcItemBalance1.ItemMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
        If clsCommon.myLen(txtShipToLocation.Value) > 0 Then
            UcItemBalance1.LocationCode = txtShipToLocation.Value
            UcItemBalance1.LocationName = lblShipToLocation.Text
        Else
            UcItemBalance1.LocationCode = txtBillToLocation.Value
            UcItemBalance1.LocationName = lblBillToLocation.Text
        End If


        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtDocNo.Value
        UcItemBalance1.TransDate = txtDate.Value
        UcItemBalance1.ShowPOQty = True
        UcItemBalance1.RefreshData()
    End Sub
    Private Sub gv1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv1.Click
        If gv1.CurrentRow IsNot Nothing Then
            setBalance()
            'SetVendorItemCostDetail()
        End If
    End Sub
    Private Sub gv1_CurrentRowChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 Then
            setBalance()
            'SetVendorItemCostDetail()
        End If
    End Sub
    Private Sub fndProject__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProject._MYValidating
        Dim qry As String = "select PROJECT_CODE as Code,SPECIFICATION,PROJECT_STATUS as Status from TSPL_PJC_PROJECT"
        fndProject.Value = clsCommon.ShowSelectForm("Project Code", qry, "Code", "", fndProject.Value, "", isButtonClicked)
        lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
    End Sub
    Private Sub txtBarCode_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBarCode.Validating
        If clsCommon.myLen(txtBarCode.Text) > 0 Then
            Dim obj As clsBarCodeGenerator = clsBarCodeGenerator.GetData(txtBarCode.Text)
            If obj Is Nothing Then
                clsCommon.MyMessageBoxShow(Me, "Not a Valid Barcode", Me.Text)
                txtBarCode.Text = ""
                Exit Sub
            End If

            Dim isFound As Boolean = False
            Dim CurrentRow As Integer = 1
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(txtBarCode.Text, clsCommon.myCstr(gv1.Rows(ii).Cells(colBarCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colQty).Value = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + 1
                    CurrentRow = ii
                    isFound = True
                    Exit For
                End If
            Next
            If Not isFound Then
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBarCode).Value = obj.Bar_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                OpenICodeList(False)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = 1
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Selling_Price
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.Item_MRP
                CurrentRow = gv1.Rows.Count - 1
                For ii As Integer = 1 To gv1.Rows.Count
                    gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
                Next
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
            End If

            UpdateCurrentRow(CurrentRow)
            UpdateAllTotals()
            txtBarCode.Text = ""
            txtBarCode.Focus()
        End If
    End Sub
    Private Sub btnhistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhistory.Click
        Dim frm As New FrmPurchaseHistory
        frm.strFormId = MyBase.Form_ID
        frm.strVendorCode = txtVendorNo.Value
        frm.strVendorName = lblVendorName.Text
        Dim strvendor As String = txtVendorNo.Value
        frm.ShowDialog()
        frm.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub EmailSmsSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmailSmsSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        ' frm.FormId = clsUserMgtCode.mbtnSRN
        frm.FormId = FORMTYPE
        frm.ShowDialog()
    End Sub
    Private Sub btnsend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSend.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
                txtDocNo.Focus()
                txtDocNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective SRN No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtDocNo.Value, NavigatorType.Current)
            Dim lstUsers As New List(Of String)
            lstUsers.Add(txtVendorNo.Value)
            SendSMSandEmail(lstUsers, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSendForApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSendForApproval.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
                txtDocNo.Focus()
                txtDocNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Sale Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtDocNo.Value, NavigatorType.Current)

            Dim qry As String = "Select * from TSPL_APPROVAL_LEVEL_SCREEN where 1=1 and Trans_Code='" + MyBase.Form_ID + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim lstUsers As New List(Of String)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    lstUsers.Add(dr("User_Code").ToString())
                Next
            End If

            If lstUsers.Count = 0 Then
                Throw New Exception("No Receiptent Found")
            End If
            SendSMSandEmail(lstUsers, True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPreview.Click
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
        'Task No-TEC/29/07/19-000966  
        Try
            ''Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.mbtnSRN)
            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(FORMTYPE)

            'If obj Is Nothing Then
            '    clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
            '    Return
            'End If
            'If clsCommon.myLen(obj.mailsubjct) <= 0 Then
            '    clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
            '    Return
            'End If

            'Dim strContactPerson As String = ""
            'Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
            'strSubject = strSubject.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))

            'Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
            'strbody = strbody.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
            'strbody = strbody.Replace(clsEmailSMSConstants.VendorNo, txtVendorNo.Value)
            'strbody = strbody.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)

            ''------------------------code for attchament-------------------------------------
            'Dim strRptPath As String = ""
            'obj.atchmnt = "N"
            'If obj.atchmnt = "Y" Then

            '    'atchqry = GetAtchmentPrintQuery(txtDocNo.Value)
            '    'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(atchqry)
            '    'If dt1.Rows.Count > 0 Then
            '    '    'SetItemWiseTax(dt1, txtDocNo.Value)
            '    '    strRptPath = NewSalesReportViewer.Emailreport(dt1, "crptSalesOrderReport", "Sales Order")
            '    'End If
            'End If
            ''---------------------------------------------------------------------------



            'For Each strUser As String In lstUsers
            '    'lstUsers.Add(dr("User_Code").ToString())
            '    Dim lstReceiptents As New List(Of String)
            '    Dim qry As String = ""
            '    Dim emailId As String = ""
            '    If isSendForApproval Then
            '        strContactPerson = strUser
            '        qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
            '        emailId = clsDBFuncationality.getSingleValue(qry)
            '    Else
            '        strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_VENDOR_MASTER where Vendor_code ='" & strUser & "' ")
            '        emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_VENDOR_MASTER where Vendor_code ='" & strUser & "' ")
            '    End If

            '    strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
            '    lstReceiptents.Add(emailId)

            '    Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

            '    clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strRptPath)
            'Next

            'sanjay
            Dim strContactPerson As String = ""

            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + FORMTYPE + "'", Nothing)
            Dim objEmailH As New clsEMailHead()
            objEmailH.arrEMail = New List(Of String)()

            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                    objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))

                    objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Code, txtVendorNo.Value)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, lblVendorName.Text)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.TotalAmount, clsCommon.myCstr(lblTotRAmt.Text))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Form_Code, FORMTYPE)




                    For Each strUser As String In lstUsers
                        'lstUsers.Add(dr("User_Code").ToString())
                        Dim lstReceiptents As New List(Of String)
                        Dim qry As String = ""
                        Dim emailId As String = ""
                        If isSendForApproval Then
                            strContactPerson = strUser
                            qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
                            emailId = clsDBFuncationality.getSingleValue(qry)
                        Else
                            strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_VENDOR_MASTER where vendor_code ='" & strUser & "' ")
                            emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_VENDOR_MASTER where vendor_code ='" & strUser & "' ")
                        End If

                        lstReceiptents.Add(emailId)
                        objEmailH.arrEMail.Add(clsCommon.myCstr(emailId))


                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ContactPerson, strContactPerson)
                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.UserCode, strUser)

                    Next


                    objEmailH.SaveData(FORMTYPE, objEmailH, Nothing)
                    objEmailH = Nothing
                    clsCommon.MyMessageBoxShow(Me, "E-Mail Send Successfully", Me.Text)
                End If
                'sanjay

            End If

            If Not clsSMSAtPost_Purchase.SMSATPOST_PUR() Then
                SMSENDONLY(False)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub SMSENDONLY(ByVal isPost As Boolean)
        Try
            ''Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.mbtnSRN)
            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(FORMTYPE)
            'If obj Is Nothing Then
            '    clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
            '    Return
            'End If


            'If clsCommon.myLen(obj.smsbody) <= 0 Then
            '    Return
            'End If
            'Dim strbody As String = obj.smsbody.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
            'strbody = strbody.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
            'strbody = strbody.Replace(clsEmailSMSConstants.VendorNo, txtVendorNo.Value)
            'strbody = strbody.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)

            'Dim strphone As String = clsDBFuncationality.getSingleValue("select Phone1 from tspl_vendor_master where vendor_code ='" & txtVendorNo.Value & "' ")

            ''If clsSMSSend.SendSMS(clsUserMgtCode.mbtnSRN, strbody, strphone) Then
            'If clsSMSSend.SendSMS(FORMTYPE, strbody, strphone) Then
            '    If Not isPost Then
            '        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
            '    End If
            'End If

            'sanjay
            Dim strContactPerson As String = ""
            Dim strotherno As String = Nothing
            strotherno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Phone1 from tspl_vendor_master where vendor_code ='" & txtVendorNo.Value & "' "))

            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + FORMTYPE + "'", Nothing)
            Dim objSMSH As New clsSMSHead()
            objSMSH.arrMobilNo = New List(Of String)()
            objSMSH.arrMobilNo.Add(clsCommon.myCstr(strotherno))
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then

                    objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))

                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Vendor_Code, txtVendorNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, lblVendorName.Text)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TotalAmount, clsCommon.myCstr(lblTotRAmt.Text))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Form_Code, FORMTYPE)
                End If



                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                    objSMSH.SaveData(FORMTYPE, objSMSH, Nothing)
                    objSMSH = Nothing
                    clsCommon.MyMessageBoxShow(Me, "SMS Send Successfully", Me.Text)
                End If
            End If
            'Sanjay
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub LoadBlankRoadPermitGrid()
        gv_roadpermit.Rows.Clear()
        gv_roadpermit.Columns.Clear()

        Dim reposno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposno.FormatString = ""
        reposno.HeaderText = "S.No."
        reposno.Name = colRoadsno
        reposno.ReadOnly = True
        reposno.Width = 50
        gv_roadpermit.MasterTemplate.Columns.Add(reposno)

        Dim reposcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposcode.FormatString = ""
        reposcode.HeaderText = "Form Code"
        reposcode.Name = colroadformcode
        reposcode.Width = 80
        reposcode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        reposcode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_roadpermit.MasterTemplate.Columns.Add(reposcode)

        Dim reponame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponame.FormatString = ""
        reponame.HeaderText = "Description"
        reponame.Name = colroadformdesc
        reponame.ReadOnly = True
        reponame.Width = 100
        gv_roadpermit.MasterTemplate.Columns.Add(reponame)

        Dim reposerialno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposerialno.FormatString = ""
        reposerialno.HeaderText = "Serial No"
        reposerialno.Name = colroadformserialno
        reposerialno.ReadOnly = True
        reposerialno.Width = 100
        gv_roadpermit.MasterTemplate.Columns.Add(reposerialno)

        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.FormatString = ""
        reporem.HeaderText = "Remarks"
        reporem.Name = colroadformrem
        reporem.ReadOnly = True
        reporem.Width = 100
        gv_roadpermit.MasterTemplate.Columns.Add(reporem)

        gv_roadpermit.AllowDeleteRow = True
        gv_roadpermit.AllowAddNewRow = False
        gv_roadpermit.ShowGroupPanel = False
        gv_roadpermit.AllowColumnReorder = False
        gv_roadpermit.AllowRowReorder = False
        gv_roadpermit.EnableSorting = False
        gv_roadpermit.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_roadpermit.MasterTemplate.ShowRowHeaderColumn = False
        gv_roadpermit.TableElement.TableHeaderHeight = 40
    End Sub
    Sub LoadBlankCFORMGrid()
        gv_c_form.Rows.Clear()
        gv_c_form.Columns.Clear()

        Dim reposno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposno.FormatString = ""
        reposno.HeaderText = "S.No."
        reposno.Name = colCFormsno
        reposno.ReadOnly = True
        reposno.Width = 50
        gv_c_form.MasterTemplate.Columns.Add(reposno)

        Dim reposcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposcode.FormatString = ""
        reposcode.HeaderText = "Form Code"
        reposcode.Name = colCFormformcode
        reposcode.Width = 80
        reposcode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        reposcode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_c_form.MasterTemplate.Columns.Add(reposcode)

        Dim reponame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponame.FormatString = ""
        reponame.HeaderText = "Description"
        reponame.Name = colCFormformdesc
        reponame.ReadOnly = True
        reponame.Width = 100
        gv_c_form.MasterTemplate.Columns.Add(reponame)

        Dim reposerialno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposerialno.FormatString = ""
        reposerialno.HeaderText = "Serial No"
        reposerialno.Name = colCFormformserialno
        reposerialno.ReadOnly = True
        reposerialno.Width = 100
        gv_c_form.MasterTemplate.Columns.Add(reposerialno)

        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.FormatString = ""
        reporem.HeaderText = "Remarks"
        reporem.Name = colCFormformrem
        reporem.ReadOnly = True
        reporem.Width = 100
        gv_c_form.MasterTemplate.Columns.Add(reporem)

        gv_c_form.AllowDeleteRow = True
        gv_c_form.AllowAddNewRow = False
        gv_c_form.ShowGroupPanel = False
        gv_c_form.AllowColumnReorder = False
        gv_c_form.AllowRowReorder = False
        gv_c_form.EnableSorting = False
        gv_c_form.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_c_form.MasterTemplate.ShowRowHeaderColumn = False
        gv_c_form.TableElement.TableHeaderHeight = 40
    End Sub
    Private Sub Chkroadpermit_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles Chkroadpermit.ToggleStateChanged
        If Chkroadpermit.Checked Then
            RadGroupBox3.Enabled = True
            gv_roadpermit.Rows.Clear()
            gv_roadpermit.Rows.AddNew()
        ElseIf Not Chkroadpermit.Checked Then
            RadGroupBox3.Enabled = False
            gv_roadpermit.Rows.Clear()
            gv_roadpermit.Rows.AddNew()
        End If
    End Sub
    Private Sub chk_c_form_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chk_c_form.ToggleStateChanged
        If chk_c_form.Checked Then
            RadGroupBox4.Enabled = True
            gv_c_form.Rows.Clear()
            gv_c_form.Rows.AddNew()
        ElseIf Not chk_c_form.Checked Then
            RadGroupBox4.Enabled = False
            gv_c_form.Rows.Clear()
            gv_c_form.Rows.AddNew()
        End If
    End Sub
    Private Sub btnForm_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForm_Update.Click
        Try
            If Chkroadpermit.Checked Or chk_c_form.Checked Then
                AllowToSave_FormEntry()

                Dim arr As New List(Of clsSRNRoadPermitDetail)

                For Each grow As GridViewRowInfo In gv_roadpermit.Rows
                    Dim objtr As New clsSRNRoadPermitDetail()
                    objtr.roadpono = clsCommon.myCstr(txtPONo.Value)
                    objtr.roadcode = clsCommon.myCstr(grow.Cells(colroadformcode).Value)
                    objtr.roadvendor = clsCommon.myCstr(txtVendorNo.Value)
                    objtr.roadissue_no = clsCommon.myCstr(grow.Cells(colroadformserialno).Value)
                    objtr.RoadpermitSRNNO = clsCommon.myCstr(txtDocNo.Value)
                    If clsCommon.myLen(objtr.roadcode) > 0 Then
                        arr.Add(objtr)
                    End If
                Next


                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    If clsSRNRoadPermitDetail.SaveData_RoadPermit(txtDocNo.Value, txtPONo.Value, arr, Nothing) Then
                        Dim qry As String = "update tspl_srn_head set issue_road_permit='1' where srn_no='" + txtDocNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)

                        qry = "update tspl_purchase_order_head set issue_road_permit='1' where purchaseorder_no='" + txtPONo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    End If
                End If

                Dim arr1 As New List(Of clsSRNCFORMDetail)

                For Each grow As GridViewRowInfo In gv_c_form.Rows
                    Dim objtr As New clsSRNCFORMDetail()
                    objtr.cformpono = clsCommon.myCstr(txtPONo.Value)
                    objtr.cformcode = clsCommon.myCstr(grow.Cells(colCFormformcode).Value)
                    objtr.cformvendor = clsCommon.myCstr(txtVendorNo.Value)
                    objtr.cformissue_no = clsCommon.myCstr(grow.Cells(colCFormformserialno).Value)
                    objtr.cformSRNNO = clsCommon.myCstr(txtDocNo.Value)

                    If clsCommon.myLen(objtr.cformcode) > 0 Then
                        arr1.Add(objtr)
                    End If
                Next


                If arr1 IsNot Nothing AndAlso arr1.Count > 0 Then
                    If clsSRNCFORMDetail.SaveData_CFORM(txtDocNo.Value, txtPONo.Value, arr1, Nothing) Then
                        Dim qry As String = "update tspl_srn_head set issue_c_form='1' where srn_no='" + txtDocNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)

                        qry = "update tspl_purchase_order_head set issue_c_form='1' where purchaseorder_no='" + txtPONo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    End If
                End If


            Else
                If Not Chkroadpermit.Checked Then
                    Dim qry As String = "update tspl_srn_head set issue_road_permit='0' where srn_no='" + txtDocNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)

                    qry = "delete from TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL where srn_no='" + txtDocNo.Value + "' and form_code in (select form_code from tspl_form_master where form_type in ('38-Inward','38-Outward','Others'))"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                End If
                If Not chk_c_form.Checked Then
                    Dim qry As String = "update tspl_srn_head set issue_c_form='0' where srn_no='" + txtDocNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)

                    qry = "delete from TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL where srn_no='" + txtDocNo.Value + "' and form_code in (select form_code from tspl_form_master where form_type in ('C','Others'))"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                End If
            End If
            clsCommon.MyMessageBoxShow(Me, "Forms Detail Updated Successfully", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv_roadpermit_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_roadpermit.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gv_roadpermit.Columns(colroadformcode) Then
                    OpenRoadPermit(False)
                End If
            End If
            isCellValueChangedOpen = False
        End If
    End Sub
    Sub OpenRoadPermit(ByVal isButtonclicked As Boolean)
        Dim qry As String = ""
        Dim check As Integer = 0
        Dim formcode As String = clsCommon.myCstr(gv_roadpermit.CurrentRow.Cells(colroadformcode).Value)
        If clsCommon.myLen(formcode) > 0 Then
            qry = "select count(*) from tspl_form_master where form_code='" + formcode + "'"
            check = clsDBFuncationality.getSingleValue(qry)
        End If

        If clsCommon.myLen(txtPONo.Value) <= 0 Then
            If check <= 0 Then
                qry = "select TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code as Code,TSPL_Form_Master.Form_Name as [Form Name],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Form_No as [Issue No],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Date as [Issue Date],TSPL_Form_Master.Remarks  from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST left outer join TSPL_FORM_MASTER on TSPL_FORM_MASTER.Form_Code=TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code "
                qry += " where TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Vendor_code='" + txtVendorNo.Value + "' and (TSPL_Form_Master.Form_Type='38-Inward' or TSPL_Form_Master.Form_Type='Others') and (TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.form_code++TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.iss_rcv_form_no) not in (select distinct form_code+issue_no from TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL where vendor_code='" + txtVendorNo.Value + "' and (srn_no<>'" + txtDocNo.Value + "' or PurchaseOrder_No<>''))"
            Else
                qry = "select TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code as Code,TSPL_Form_Master.Form_Name as [Form Name],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Form_No as [Issue No],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Date as [Issue Date],TSPL_Form_Master.Remarks  from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST left outer join TSPL_FORM_MASTER on TSPL_FORM_MASTER.Form_Code=TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code "
                qry += " where TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Vendor_code='" + txtVendorNo.Value + "' and (TSPL_Form_Master.Form_Type='38-Inward' or TSPL_Form_Master.Form_Type='Others') and TSPL_Form_Master.form_code='" + formcode + "' and (TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.form_code++TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.iss_rcv_form_no) not in (select distinct form_code+issue_no from TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL where vendor_code='" + txtVendorNo.Value + "' and (srn_no<>'" + txtDocNo.Value + "' or PurchaseOrder_No<>''))"
            End If
        ElseIf clsCommon.myLen(txtPONo.Value) > 0 Then
            If check <= 0 Then
                qry = "select TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code as Code,TSPL_Form_Master.Form_Name as [Form Name],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Form_No as [Issue No],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Date as [Issue Date],TSPL_Form_Master.Remarks  from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST left outer join TSPL_FORM_MASTER on TSPL_FORM_MASTER.Form_Code=TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code "
                qry += " where TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Vendor_code='" + txtVendorNo.Value + "' and (TSPL_Form_Master.Form_Type='38-Inward' or TSPL_Form_Master.Form_Type='Others') and (TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.form_code++TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.iss_rcv_form_no) not in (select distinct form_code+issue_no from TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL where vendor_code='" + txtVendorNo.Value + "' and srn_no<>'" + txtDocNo.Value + "' and purchaseorder_no<>'" + txtPONo.Value + "')"
            Else
                qry = "select TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code as Code,TSPL_Form_Master.Form_Name as [Form Name],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Form_No as [Issue No],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Date as [Issue Date],TSPL_Form_Master.Remarks  from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST left outer join TSPL_FORM_MASTER on TSPL_FORM_MASTER.Form_Code=TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code "
                qry += " where TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Vendor_code='" + txtVendorNo.Value + "' and (TSPL_Form_Master.Form_Type='38-Inward' or TSPL_Form_Master.Form_Type='Others') and TSPL_Form_Master.form_code='" + formcode + "' and (TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.form_code++TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.iss_rcv_form_no) not in (select distinct form_code+issue_no from TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL where vendor_code='" + txtVendorNo.Value + "' and srn_no<>'" + txtDocNo.Value + "' and purchaseorder_no<>'" + txtPONo.Value + "')"
            End If
        End If

        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("RODFND", qry) ', "Code", whrcls, formcode, "Code", isButtonclicked)

        If dr IsNot Nothing Then
            gv_roadpermit.CurrentRow.Cells(colroadformcode).Value = clsCommon.myCstr(dr("Code"))
            gv_roadpermit.CurrentRow.Cells(colroadformdesc).Value = clsCommon.myCstr(dr("Form Name"))
            gv_roadpermit.CurrentRow.Cells(colroadformserialno).Value = clsCommon.myCstr(dr("Issue No"))
            gv_roadpermit.CurrentRow.Cells(colroadformrem).Value = clsCommon.myCstr(dr("Remarks"))
        Else
            gv_roadpermit.CurrentRow.Cells(colroadformcode).Value = ""
            gv_roadpermit.CurrentRow.Cells(colroadformdesc).Value = ""
            gv_roadpermit.CurrentRow.Cells(colroadformserialno).Value = ""
            gv_roadpermit.CurrentRow.Cells(colroadformrem).Value = ""
        End If
    End Sub
    Private Sub gv_c_form_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_c_form.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gv_c_form.Columns(colCFormformcode) Then
                    OpenCFORM(False)
                End If
            End If
            isCellValueChangedOpen = False
        End If
    End Sub
    Sub OpenCFORM(ByVal isButtonClicked As Boolean)
        Dim qry As String = ""
        Dim check As Integer = 0
        Dim formcode As String = clsCommon.myCstr(gv_c_form.CurrentRow.Cells(colCFormformcode).Value)
        If clsCommon.myLen(formcode) > 0 Then
            qry = "select count(*) from tspl_form_master where form_code='" + formcode + "'"
            check = clsDBFuncationality.getSingleValue(qry)
        End If

        If clsCommon.myLen(txtPONo.Value) <= 0 Then
            If check <= 0 Then
                qry = "select TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code as Code,TSPL_Form_Master.Form_Name as [Form Name],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Form_No as [Issue No],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Date as [Issue Date],TSPL_Form_Master.Remarks  from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST left outer join TSPL_FORM_MASTER on TSPL_FORM_MASTER.Form_Code=TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code "
                qry += " where TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Vendor_code='" + txtVendorNo.Value + "' and (TSPL_Form_Master.Form_Type='C' or TSPL_Form_Master.Form_Type='Others') and (TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.form_code+TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.iss_rcv_form_no) not in (select distinct form_code+issue_no from TSPL_CFORM_ISSUE_RECEIVE_DETAIL where vendor_code='" + txtVendorNo.Value + "' and (srn_no<>'" + txtDocNo.Value + "' or purchaseorder_no<>''))"
            Else
                qry = "select TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code as Code,TSPL_Form_Master.Form_Name as [Form Name],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Form_No as [Issue No],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Date as [Issue Date],TSPL_Form_Master.Remarks  from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST left outer join TSPL_FORM_MASTER on TSPL_FORM_MASTER.Form_Code=TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code "
                qry += " where TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Vendor_code='" + txtVendorNo.Value + "' and (TSPL_Form_Master.Form_Type='C' or TSPL_Form_Master.Form_Type='Others') and TSPL_Form_Master.form_code='" + formcode + "' and (TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.form_code++TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.iss_rcv_form_no) not in (select distinct form_code+issue_no from TSPL_CFORM_ISSUE_RECEIVE_DETAIL where vendor_code='" + txtVendorNo.Value + "' and (srn_no<>'" + txtDocNo.Value + "' or purchaseorder_no<>''))"
            End If
        ElseIf clsCommon.myLen(txtPONo.Value) > 0 Then
            If check <= 0 Then
                qry = "select TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code as Code,TSPL_Form_Master.Form_Name as [Form Name],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Form_No as [Issue No],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Date as [Issue Date],TSPL_Form_Master.Remarks  from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST left outer join TSPL_FORM_MASTER on TSPL_FORM_MASTER.Form_Code=TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code "
                qry += " where TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Vendor_code='" + txtVendorNo.Value + "' and (TSPL_Form_Master.Form_Type='C' or TSPL_Form_Master.Form_Type='Others') and (TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.form_code+TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.iss_rcv_form_no) not in (select distinct form_code+issue_no from TSPL_CFORM_ISSUE_RECEIVE_DETAIL where vendor_code='" + txtVendorNo.Value + "' and srn_no<>'" + txtDocNo.Value + "' and purchaseorder_no<>'" + txtPONo.Value + "')"
            Else
                qry = "select TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code as Code,TSPL_Form_Master.Form_Name as [Form Name],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Form_No as [Issue No],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Date as [Issue Date],TSPL_Form_Master.Remarks  from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST left outer join TSPL_FORM_MASTER on TSPL_FORM_MASTER.Form_Code=TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code "
                qry += " where TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Vendor_code='" + txtVendorNo.Value + "' and (TSPL_Form_Master.Form_Type='C' or TSPL_Form_Master.Form_Type='Others') and TSPL_Form_Master.form_code='" + formcode + "' and (TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.form_code++TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.iss_rcv_form_no) not in (select distinct form_code+issue_no from TSPL_CFORM_ISSUE_RECEIVE_DETAIL where vendor_code='" + txtVendorNo.Value + "' and srn_no<>'" + txtDocNo.Value + "' and purchaseorder_no<>'" + txtPONo.Value + "')"
            End If
        End If

        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("CFRMFND", qry) ', "Code", whrcls, formcode, "Code", isButtonclicked)

        If dr IsNot Nothing Then
            gv_c_form.CurrentRow.Cells(colCFormformcode).Value = clsCommon.myCstr(dr("Code"))
            gv_c_form.CurrentRow.Cells(colCFormformdesc).Value = clsCommon.myCstr(dr("Form Name"))
            gv_c_form.CurrentRow.Cells(colCFormformserialno).Value = clsCommon.myCstr(dr("Issue No"))
            gv_c_form.CurrentRow.Cells(colCFormformrem).Value = clsCommon.myCstr(dr("Remarks"))
        Else
            gv_c_form.CurrentRow.Cells(colCFormformcode).Value = ""
            gv_c_form.CurrentRow.Cells(colCFormformdesc).Value = ""
            gv_c_form.CurrentRow.Cells(colCFormformserialno).Value = ""
            gv_c_form.CurrentRow.Cells(colCFormformrem).Value = ""
        End If
    End Sub
    Private Sub gv_c_form_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv_c_form.CurrentColumnChanged
        If gv_c_form.RowCount > 0 Then
            Dim intCurrRow As Integer = gv_c_form.CurrentRow.Index
            gv_c_form.CurrentRow.Cells(colCFormsno).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv_c_form.Rows.Count - 1 Then
                gv_c_form.Rows.AddNew()
                gv_c_form.CurrentRow = gv_c_form.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub gv_roadpermit_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv_roadpermit.CurrentColumnChanged
        If gv_roadpermit.RowCount > 0 Then
            Dim intCurrRow As Integer = gv_roadpermit.CurrentRow.Index
            gv_roadpermit.CurrentRow.Cells(colRoadsno).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv_roadpermit.Rows.Count - 1 Then
                gv_roadpermit.Rows.AddNew()
                gv_roadpermit.CurrentRow = gv_roadpermit.Rows(intCurrRow)
            End If
        End If
    End Sub
    Sub AllowToSave_FormEntry()
        Try
            Dim formcodeas As String = ""
            Dim oldformcode As String = ""
            Dim formserialno As String = ""
            Dim oldformserialno As String = ""


            If Chkroadpermit.Checked Then
                formcodeas = clsCommon.myCstr(gv_roadpermit.Rows(0).Cells(colroadformcode).Value)
                If clsCommon.myLen(formcodeas) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage5
                    Throw New Exception("Please fill atleast one row for Road Permit")
                End If
            End If


            If chk_c_form.Checked Then
                formcodeas = clsCommon.myCstr(gv_c_form.Rows(0).Cells(colCFormformcode).Value)
                If clsCommon.myLen(formcodeas) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage5
                    Throw New Exception("Please fill atleast one row for C-Form")
                End If
            End If

            If Chkroadpermit.Checked Then
                For i As Integer = 0 To gv_roadpermit.Rows.Count - 1
                    formcodeas = clsCommon.myCstr(gv_roadpermit.Rows(i).Cells(colroadformcode).Value)
                    formserialno = clsCommon.myCstr(gv_roadpermit.Rows(i).Cells(colroadformserialno).Value)

                    If clsCommon.myLen(formcodeas) > 0 Then
                        For j As Integer = i + 1 To gv_roadpermit.Rows.Count - 1
                            oldformcode = clsCommon.myCstr(gv_roadpermit.Rows(j).Cells(colroadformcode).Value)
                            oldformserialno = clsCommon.myCstr(gv_roadpermit.Rows(j).Cells(colroadformserialno).Value)

                            If clsCommon.CompairString(formcodeas, oldformcode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(formserialno, oldformserialno) = CompairStringResult.Equal Then
                                RadPageView1.SelectedPage = RadPageViewPage5
                                Throw New Exception("Duplicate Road Permit Form Code with same Serial No. does not allowed" + Environment.NewLine + "invalid selection at row no. " + clsCommon.myCstr(j + 1) + ".")
                            End If
                        Next
                    End If
                Next
            End If

            If chk_c_form.Checked Then
                For i As Integer = 0 To gv_c_form.Rows.Count - 1
                    formcodeas = clsCommon.myCstr(gv_c_form.Rows(i).Cells(colCFormformcode).Value)
                    formserialno = clsCommon.myCstr(gv_c_form.Rows(i).Cells(colCFormformserialno).Value)

                    If clsCommon.myLen(formcodeas) > 0 Then
                        For j As Integer = i + 1 To gv_c_form.Rows.Count - 1
                            oldformcode = clsCommon.myCstr(gv_c_form.Rows(j).Cells(colCFormformcode).Value)
                            oldformserialno = clsCommon.myCstr(gv_c_form.Rows(j).Cells(colCFormformserialno).Value)

                            If clsCommon.CompairString(formcodeas, oldformcode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(formserialno, oldformserialno) = CompairStringResult.Equal Then
                                RadPageView1.SelectedPage = RadPageViewPage5
                                Throw New Exception("Duplicate C-Form Code with same Serial No. does not allowed" + Environment.NewLine + "invalid selection at row no. " + clsCommon.myCstr(j + 1) + ".")
                            End If
                        Next
                    End If
                Next
            End If
            '------------------------------------------------

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Function SaveData_AutoPO() As Boolean
        Dim isSaved As Boolean = True
        Dim obj As New clsPurchaseOrderHead()
        Dim objtr As New clsPurchaseOrderDetail()
        Dim objCForm As New clsPurchaseOrderCFORMDetail()
        Dim objRoad As New clsPurchaseOrderRoadDetail()
        Dim objSRN As New clsSRNHead()
        Try
            objSRN = clsSRNHead.GetData(txtDocNo.Value, NavigatorType.Current)

            If objSRN IsNot Nothing AndAlso clsCommon.myLen(objSRN.SRN_No) > 0 Then
                '-----------Load Event---------------------------
                obj.IsAbatementPO = objSRN.IsAbatementPO
                obj.Is_Open_PO = "0"
                obj.Against_PO = Nothing
                obj.Renewal_Date = ""
                obj.Delivery_Terms_Code = Nothing
                obj.Payment_Terms = Nothing
                obj.Insurance_Terms = Nothing
                obj.PurchaseOrder_Date = clsCommon.GETSERVERDATE(Nothing)
                obj.Delivery_date = obj.PurchaseOrder_Date
                obj.Delivery_Duration = Nothing
                obj.Ref_No = Nothing
                obj.Total_Tax_Amt = objSRN.Total_Tax_Amt
                obj.Remarks = objSRN.Remarks
                obj.Vendor_Code = objSRN.Vendor_Code
                obj.Vendor_Name = objSRN.Vendor_Name
                obj.Bill_To_Location = objSRN.Bill_To_Location
                obj.BillToLocationName = objSRN.BillToLocationName
                obj.Ship_To_Location = objSRN.Ship_To_Location
                obj.ShipToLocationName = objSRN.ShipToLocationName
                obj.Comments = objSRN.Comments
                obj.Mode_Of_Transport = "By Road"
                obj.PurchaseOrder_Type = objSRN.PurchaseOrder_Type
                obj.PROJECT_ID = objSRN.PROJECT_ID
                obj.PO_Amount = 0
                obj.isBlanket = 0
                obj.Dept = objSRN.Dept
                obj.Dept_Desc = objSRN.Dept_Desc
                obj.Description = objSRN.Description
                obj.Item_Type = objSRN.Item_Type
                obj.Auto_PO = "1"
                obj.roadpermit = objSRN.against_roadpermit
                obj.Cform = objSRN.agnst_cform
                obj.Tax_Group = objSRN.Tax_Group
                obj.Tax_Calculation_Type = objSRN.Tax_Calculation_Type
                obj.Terms_Code = objSRN.Terms_Code
                obj.TermsName = objSRN.TermsName
                obj.Due_Date = objSRN.Due_Date
                obj.CURRENCY_CODE = objSRN.CURRENCY_CODE
                obj.ConvRate = objSRN.ConvRate
                obj.ApplicableFrom = objSRN.ApplicableFrom
                obj.Discount_Base = objSRN.Discount_Base
                obj.Discount_Amt = objSRN.Discount_Amt
                obj.Amount_Less_Discount = objSRN.Amount_Less_Discount
                obj.Total_Tax_Amt = objSRN.Total_Tax_Amt
                obj.Total_Add_Charge = objSRN.Total_Add_Charge
                obj.PO_Total_Amt = objSRN.SRN_Total_Amt
                obj.Against_Requisition = objSRN.Against_Requisition
                obj.close_yn = "N"
                obj.is_Excise_On_Qty = objSRN.is_Excise_On_Qty

                If clsCommon.myLen(objSRN.TAX1) > 0 Then
                    obj.TAX1 = objSRN.TAX1
                    obj.TAX1_Rate = objSRN.TAX1_Rate
                    obj.TAX1_Amt = objSRN.TAX1_Amt
                    obj.TAX1_Base_Amt = objSRN.TAX1_Base_Amt
                    obj.AssessableAmt = objSRN.AssessableAmt
                End If
                If clsCommon.myLen(objSRN.TAX2) > 0 Then
                    obj.TAX2 = objSRN.TAX2
                    obj.TAX2_Rate = objSRN.TAX2_Rate
                    obj.TAX2_Amt = objSRN.TAX2_Amt
                    obj.TAX2_Base_Amt = objSRN.TAX2_Base_Amt
                End If
                If clsCommon.myLen(objSRN.TAX3) > 0 Then
                    obj.TAX3 = objSRN.TAX3
                    obj.TAX3_Rate = objSRN.TAX3_Rate
                    obj.TAX3_Amt = objSRN.TAX3_Amt
                    obj.TAX3_Base_Amt = objSRN.TAX3_Base_Amt
                End If
                If clsCommon.myLen(objSRN.TAX4) > 0 Then
                    obj.TAX4 = objSRN.TAX4
                    obj.TAX4_Rate = objSRN.TAX4_Rate
                    obj.TAX4_Amt = objSRN.TAX4_Amt
                    obj.TAX4_Base_Amt = objSRN.TAX4_Base_Amt
                End If
                If clsCommon.myLen(objSRN.TAX5) > 0 Then
                    obj.TAX5 = objSRN.TAX5
                    obj.TAX5_Rate = objSRN.TAX5_Rate
                    obj.TAX5_Amt = objSRN.TAX5_Amt
                    obj.TAX5_Base_Amt = objSRN.TAX5_Base_Amt
                End If
                If clsCommon.myLen(objSRN.TAX6) > 0 Then
                    obj.TAX6 = objSRN.TAX6
                    obj.TAX6_Rate = objSRN.TAX6_Rate
                    obj.TAX6_Amt = objSRN.TAX6_Amt
                    obj.TAX6_Base_Amt = objSRN.TAX6_Base_Amt
                End If
                If clsCommon.myLen(objSRN.TAX7) > 0 Then
                    obj.TAX7 = objSRN.TAX7
                    obj.TAX7_Rate = objSRN.TAX7_Rate
                    obj.TAX7_Amt = objSRN.TAX7_Amt
                    obj.TAX7_Base_Amt = objSRN.TAX7_Base_Amt
                End If
                If clsCommon.myLen(objSRN.TAX8) > 0 Then
                    obj.TAX8 = objSRN.TAX8
                    obj.TAX8_Rate = objSRN.TAX8_Rate
                    obj.TAX8_Amt = objSRN.TAX8_Amt
                    obj.TAX8_Base_Amt = objSRN.TAX8_Base_Amt
                End If
                If clsCommon.myLen(objSRN.TAX9) > 0 Then
                    obj.TAX9 = objSRN.TAX9
                    obj.TAX9_Rate = objSRN.TAX9_Rate
                    obj.TAX9_Amt = objSRN.TAX9_Amt
                    obj.TAX9_Base_Amt = objSRN.TAX9_Base_Amt
                End If
                If clsCommon.myLen(objSRN.TAX10) > 0 Then
                    obj.TAX10 = objSRN.TAX10
                    obj.TAX10_Rate = objSRN.TAX10_Rate
                    obj.TAX10_Amt = objSRN.TAX10_Amt
                    obj.TAX10_Base_Amt = objSRN.TAX10_Base_Amt
                End If

                '==========================ac=======================
                If clsCommon.myLen(objSRN.Add_Charge_Code1) > 0 Then
                    obj.Add_Charge_Code1 = objSRN.Add_Charge_Code1
                    obj.Add_Charge_Amt1 = objSRN.Add_Charge_Amt1
                    obj.Add_Charge_Name1 = objSRN.Add_Charge_Name1
                End If
                If clsCommon.myLen(objSRN.Add_Charge_Code2) > 0 Then
                    obj.Add_Charge_Code2 = objSRN.Add_Charge_Code2
                    obj.Add_Charge_Amt2 = objSRN.Add_Charge_Amt2
                    obj.Add_Charge_Name2 = objSRN.Add_Charge_Name2
                End If
                If clsCommon.myLen(objSRN.Add_Charge_Code3) > 0 Then
                    obj.Add_Charge_Code3 = objSRN.Add_Charge_Code3
                    obj.Add_Charge_Amt3 = objSRN.Add_Charge_Amt3
                    obj.Add_Charge_Name3 = objSRN.Add_Charge_Name3
                End If
                If clsCommon.myLen(objSRN.Add_Charge_Code4) > 0 Then
                    obj.Add_Charge_Code4 = objSRN.Add_Charge_Code4
                    obj.Add_Charge_Amt4 = objSRN.Add_Charge_Amt4
                    obj.Add_Charge_Name4 = objSRN.Add_Charge_Name4
                End If
                If clsCommon.myLen(objSRN.Add_Charge_Code5) > 0 Then
                    obj.Add_Charge_Code5 = objSRN.Add_Charge_Code5
                    obj.Add_Charge_Amt5 = objSRN.Add_Charge_Amt5
                    obj.Add_Charge_Name5 = objSRN.Add_Charge_Name5
                End If
                If clsCommon.myLen(objSRN.Add_Charge_Code6) > 0 Then
                    obj.Add_Charge_Code6 = objSRN.Add_Charge_Code6
                    obj.Add_Charge_Amt6 = objSRN.Add_Charge_Amt6
                    obj.Add_Charge_Name6 = objSRN.Add_Charge_Name6
                End If
                If clsCommon.myLen(objSRN.Add_Charge_Code7) > 0 Then
                    obj.Add_Charge_Code7 = objSRN.Add_Charge_Code7
                    obj.Add_Charge_Amt7 = objSRN.Add_Charge_Amt7
                    obj.Add_Charge_Name7 = objSRN.Add_Charge_Name7
                End If
                If clsCommon.myLen(objSRN.Add_Charge_Code8) > 0 Then
                    obj.Add_Charge_Code8 = objSRN.Add_Charge_Code8
                    obj.Add_Charge_Amt8 = objSRN.Add_Charge_Amt8
                    obj.Add_Charge_Name8 = objSRN.Add_Charge_Name8
                End If
                If clsCommon.myLen(objSRN.Add_Charge_Code9) > 0 Then
                    obj.Add_Charge_Code9 = objSRN.Add_Charge_Code9
                    obj.Add_Charge_Amt9 = objSRN.Add_Charge_Amt9
                    obj.Add_Charge_Name9 = objSRN.Add_Charge_Name9
                End If
                If clsCommon.myLen(objSRN.Add_Charge_Code10) > 0 Then
                    obj.Add_Charge_Code10 = objSRN.Add_Charge_Code10
                    obj.Add_Charge_Amt10 = objSRN.Add_Charge_Amt10
                    obj.Add_Charge_Name10 = objSRN.Add_Charge_Name10
                End If
                '----------------------------------Grid Data--------------------------------

                obj.Arr = New List(Of clsPurchaseOrderDetail)


                If objSRN.Arr IsNot Nothing AndAlso objSRN.Arr.Count > 0 Then
                    For Each objtrSRN As clsSRNDetail In objSRN.Arr
                        objtr = New clsPurchaseOrderDetail()

                        objtr.Line_No = objtrSRN.Line_No
                        objtr.Row_Type = objtrSRN.Row_Type
                        objtr.Item_Code = objtrSRN.Item_Code
                        objtr.Item_Desc = objtrSRN.Item_Desc
                        objtr.PurchaseOrder_Qty = objtrSRN.SRN_Qty
                        objtr.Balance_Qty = objtrSRN.SRN_Qty
                        objtr.Unit_code = objtrSRN.Unit_code
                        objtr.Requisition_Id = objtrSRN.Req_No
                        objtr.Item_Cost = objtrSRN.Item_Cost
                        objtr.Last_Other_Vendor_Rate = Nothing
                        objtr.Last_Same_Vendor_Rate = Nothing
                        objtr.Amount = objtrSRN.Amount
                        objtr.Disc_Per = objtrSRN.Disc_Per
                        objtr.Disc_Amt = objtrSRN.Disc_Amt
                        objtr.Amt_Less_Discount = objtrSRN.Amt_Less_Discount
                        objtr.TAX1 = objtrSRN.TAX1
                        objtr.TAX1_Base_Amt = objtrSRN.TAX1_Base_Amt
                        objtr.TAX1_Rate = objtrSRN.TAX1_Rate
                        objtr.TAX1_Amt = objtrSRN.TAX1_Amt
                        objtr.TAX2 = objtrSRN.TAX2
                        objtr.TAX2_Base_Amt = objtrSRN.TAX2_Base_Amt
                        objtr.TAX2_Rate = objtrSRN.TAX2_Rate
                        objtr.TAX2_Amt = objtrSRN.TAX2_Amt
                        objtr.TAX3 = objtrSRN.TAX3
                        objtr.TAX3_Base_Amt = objtrSRN.TAX3_Base_Amt
                        objtr.TAX3_Rate = objtrSRN.TAX3_Rate
                        objtr.TAX3_Amt = objtrSRN.TAX3_Amt
                        objtr.TAX4 = objtrSRN.TAX4
                        objtr.TAX4_Base_Amt = objtrSRN.TAX4_Base_Amt
                        objtr.TAX4_Rate = objtrSRN.TAX4_Rate
                        objtr.TAX4_Amt = objtrSRN.TAX4_Amt
                        objtr.TAX5 = objtrSRN.TAX5
                        objtr.TAX5_Base_Amt = objtrSRN.TAX5_Base_Amt
                        objtr.TAX5_Rate = objtrSRN.TAX5_Rate
                        objtr.TAX5_Amt = objtrSRN.TAX5_Amt
                        objtr.TAX6 = objtrSRN.TAX6
                        objtr.TAX6_Base_Amt = objtrSRN.TAX6_Base_Amt
                        objtr.TAX6_Rate = objtrSRN.TAX6_Rate
                        objtr.TAX6_Amt = objtrSRN.TAX6_Amt
                        objtr.TAX7 = objtrSRN.TAX7
                        objtr.TAX7_Base_Amt = objtrSRN.TAX7_Base_Amt
                        objtr.TAX7_Rate = objtrSRN.TAX7_Rate
                        objtr.TAX7_Amt = objtrSRN.TAX7_Amt
                        objtr.TAX8 = objtrSRN.TAX8
                        objtr.TAX8_Base_Amt = objtrSRN.TAX8_Base_Amt
                        objtr.TAX8_Rate = objtrSRN.TAX8_Rate
                        objtr.TAX8_Amt = objtrSRN.TAX8_Amt
                        objtr.TAX9 = objtrSRN.TAX9
                        objtr.TAX9_Base_Amt = objtrSRN.TAX9_Base_Amt
                        objtr.TAX9_Rate = objtrSRN.TAX9_Rate
                        objtr.TAX9_Amt = objtrSRN.TAX9_Amt
                        objtr.TAX10 = objtrSRN.TAX10
                        objtr.TAX10_Base_Amt = objtrSRN.TAX10_Base_Amt
                        objtr.TAX10_Rate = objtrSRN.TAX10_Rate
                        objtr.TAX10_Amt = objtrSRN.TAX10_Amt
                        objtr.Total_Tax_Amt = objtrSRN.Total_Tax_Amt

                        objtr.Item_Net_Amt = objtrSRN.Item_Net_Amt
                        objtr.Specification = objtrSRN.Specification
                        objtr.Remarks = objtrSRN.Location
                        objtr.Location = objSRN.Bill_To_Location
                        objtr.MRP = objtrSRN.MRP
                        objtr.AssessableAmt = objtrSRN.AssessableAmt
                        objtr.Bin_No = objtrSRN.Bin_No

                        If IsAbatementPO Then
                            objtr.AbatementRate = objtrSRN.AbatementRate
                            objtr.AssessableMRP = objtrSRN.AssessableMRP
                            objtr.TotalAssessableMRP = objtrSRN.TotalAssessableMRP
                        End If
                        If (clsCommon.myLen(objtr.Item_Code) > 0) Then
                            obj.Arr.Add(objtr)
                        End If
                    Next
                End If ''objsrn.arr

                obj.Form_ID = "PO-ODR"

                obj.Arr_Road = New List(Of clsPurchaseOrderRoadDetail)
                obj.Arr_CFORM = New List(Of clsPurchaseOrderCFORMDetail)

                If objSRN.Arr_CFORM IsNot Nothing AndAlso objSRN.Arr_CFORM.Count > 0 Then
                    For Each objtrSRN As clsSRNCFORMDetail In objSRN.Arr_CFORM
                        objCForm = New clsPurchaseOrderCFORMDetail()

                        objCForm.cformpono = obj.PurchaseOrder_No
                        objCForm.cformcode = objtrSRN.cformcode
                        objCForm.cformvendor = objSRN.Vendor_Code
                        objCForm.cformissue_no = objtrSRN.cformissue_no
                        objCForm.cformSRNNO = objSRN.SRN_No

                        If clsCommon.myLen(objCForm.cformcode) > 0 Then
                            obj.Arr_CFORM.Add(objCForm)
                        End If
                    Next
                End If

                If objSRN.Arr_Road IsNot Nothing AndAlso objSRN.Arr_Road.Count > 0 Then
                    For Each objtrSRN As clsSRNRoadPermitDetail In objSRN.Arr_Road
                        objRoad = New clsPurchaseOrderRoadDetail()

                        objRoad.roadpono = obj.PurchaseOrder_No
                        objRoad.roadcode = objtrSRN.roadcode
                        objRoad.roadvendor = objSRN.Vendor_Code
                        objRoad.roadissue_no = objtrSRN.roadissue_no
                        objRoad.RoadpermitSRNNO = objSRN.SRN_No

                        If clsCommon.myLen(objRoad.roadcode) > 0 Then
                            obj.Arr_Road.Add(objRoad)
                        End If
                    Next
                End If

                If obj.SaveData(obj, True, False) Then
                    Dim objPO As New clsPurchaseOrderHead()
                    If objPO.PostData("PO-ODR", obj.PurchaseOrder_No, True, False) Then
                        objPO = Nothing
                        Dim qry As String = "update tspl_srn_head set Against_PO='" + obj.PurchaseOrder_No + "' where srn_no='" + txtDocNo.Value + "'"
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)

                        qry = "update tspl_srn_detail set PO_Id='" + obj.PurchaseOrder_No + "' where srn_no='" + txtDocNo.Value + "'"
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)

                        'qry = "update TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL set srn_no='" + txtDocNo.Value + "' where purchaseorder_NO='" + obj.PurchaseOrder_No + "'"
                        'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)

                        'qry = "update TSPL_CFORM_ISSUE_RECEIVE_DETAIL set srn_no='" + txtDocNo.Value + "' where purchaseorder_NO='" + obj.PurchaseOrder_No + "'"
                        'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
                    Else
                        isSaved = False
                    End If
                Else
                    isSaved = False
                End If
            End If 'objsrn

            Return isSaved
        Catch exx As Exception
            Return False
        Finally
            obj = Nothing
            objtr = Nothing
            objCForm = Nothing
            objRoad = Nothing
            objSRN = Nothing
        End Try
    End Function
    Sub LoadRGPData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            'LoadRGPBlankGrid()
            'IsfromRGP = True
            Dim obj As New clsRGPHead()
            obj = clsRGPHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.RGP_No) > 0) Then
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsRGPDetail In obj.Arr
                        gvRGP.Rows.AddNew()
                        gvRGP.Rows(gvRGP.Rows.Count - 1).Cells(ColSelect).Value = False
                        gvRGP.Rows(gvRGP.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gvRGP.Rows(gvRGP.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gvRGP.Rows(gvRGP.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        '' Anubhooti 09-Dec-2014 (Fetch More Columns)
                        gvRGP.Rows(gvRGP.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        gvRGP.Rows(gvRGP.Rows.Count - 1).Cells(colQty).Value = objTr.RGP_Qty
                        gvRGP.Rows(gvRGP.Rows.Count - 1).Cells(colSp).Value = objTr.Specification
                        gvRGP.Rows(gvRGP.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        'If clsCommon.myLen(obj.Item_Conversion_Type) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(obj.Item_Conversion_Type), "N") = CompairStringResult.Equal Then
                            obj.Item_Conversion_Type = "None"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Item_Conversion_Type), "O") = CompairStringResult.Equal Then
                            obj.Item_Conversion_Type = "One To Many"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Item_Conversion_Type), "M") = CompairStringResult.Equal Then
                            obj.Item_Conversion_Type = "Many To One"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Item_Conversion_Type), "") = CompairStringResult.Equal Then
                            obj.Item_Conversion_Type = "None"
                        End If
                        gvRGP.Rows(gvRGP.Rows.Count - 1).Cells(colItemConType).Value = obj.Item_Conversion_Type
                        'End If
                    Next
                    If obj.Status = ERPTransactionStatus.Pending Then
                        gvRGP.Rows.AddNew()
                    End If
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub
    Sub LoadRGP_Items_Data(ByVal strCode As String, ByVal NavTyep As NavigatorType, ByVal NewVal As Boolean)
        Try
            'LoadRGPBlankGrid()
            If NewVal Then
                Dim dr() As DataRow = DtRGP.Select("" & colICode & "='" & clsCommon.myCstr(strCode) & "' and " & colRGPNo & "='" & clsCommon.myCstr(txtRGPNo.Value) & "'")
                If clsCommon.CompairString(clsCommon.myCstr(gvRGP.CurrentRow.Cells(colItemConType).Value), "None") <> CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsCommon.myCstr(gvRGP.CurrentRow.Cells(colItemConType).Value), "One To Many") = CompairStringResult.Equal Then
                        Dim obj As New clsItemConversion()
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        obj = clsItemConversion.GetData_via_Item(strCode, NavTyep)
                        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
                            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                                gv1.Rows.Clear()
                                For Each objTr As clsItemConversionDetails In obj.Arr
                                    isInsideLoadData = True
                                    'For Each grow As GridViewRowInfo In gvRGP.Rows
                                    '    If grow.Cells(colICode).Value = objTr.Item_Code Then
                                    '        isInsideLoadData = False
                                    '        GoTo a
                                    '    End If
                                    'Next
                                    If clsCommon.CompairString(gvRGP.CurrentRow.Cells(colICode).Value, clsCommon.myCstr(objTr.Item_Code)) = CompairStringResult.Equal Then
                                        isInsideLoadData = False
                                        GoTo a
                                    End If
                                    gv1.Rows.AddNew()
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc

                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objTr.Item_Code, Nothing)
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.UOM
                                    '' Anubhooti 09-Dec-2014 (Fetch More Columns)
                                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCstr(gvRGP.CurrentRow.Cells(colIName).Value)
                                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                                    ''
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = clsCommon.myCstr(txtRGPNo.Value)
                                    If dr.Length > 0 Then
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = dr(0)(colRGPNo)
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = dr(0)(colQty)
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = dr(0)(colQty)
                                    End If

                                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = obj.RGP_No
                                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(obj.Item_Code)
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(obj.Item_Code)
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(obj.Item_Code)
                                    'cboItemType.SelectedIndex = GetItemType(obj.Item_Code)
                                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
                                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                                    ' ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = obj.Location
                                    ' ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = obj.LocationName
                                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.RGP_Qty
                                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.RGP_Qt
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPICode).Value = strCode
                                    isInsideLoadData = False
a:                              Next
                                If obj.POSTED = ERPTransactionStatus.Pending Then
                                    gv1.Rows.AddNew()
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                                End If
                            End If
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gvRGP.CurrentRow.Cells(colItemConType).Value), "Many To One") = CompairStringResult.Equal Then
                        Dim objMO As New clsItemConversion()
                        Dim ItemCount As Integer = 0
                        For Each grow As GridViewRowInfo In gvRGP.Rows
                            'If grow.Cells(colICode).Value = objMO.Item_Code Then
                            '    isInsideLoadData = False
                            '    GoTo b
                            'End If
                            If clsCommon.myLen(grow.Cells(colICode).Value) Then
                                Dim strItemCode As String = clsCommon.myCstr(grow.Cells(colICode).Value)
                                If clsCommon.myLen(strItemCode) > 0 Then
                                    ItemCount += 1
                                    strCode = strCode + "," + "'" + strItemCode + "'"
                                End If
                            End If
                        Next
                        If strCode.Length > 0 Then
                            If strCode.Substring(0, 1) = "," Then
                                strCode = strCode.Substring(1, strCode.Length - 1)
                            End If
                        End If
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        objMO = clsItemConversion.GetData_via_ItemForMTO(strCode, NavTyep, ItemCount)
                        If (objMO IsNot Nothing AndAlso clsCommon.myLen(objMO.Doc_Code) > 0) Then
                            'If objMO.Arr IsNot Nothing AndAlso objMO.Arr.Count > 0 Then
                            'For Each objTr As clsItemConversionDetails In obj.Arr
                            '    isInsideLoadData = True
                            'For Each grow As GridViewRowInfo In gvRGP.Rows
                            'If grow.Cells(colICode).Value = objMO.Item_Code Then
                            '    isInsideLoadData = False
                            '    GoTo b
                            'End If
                            'Next
                            If clsCommon.CompairString(gvRGP.CurrentRow.Cells(colICode).Value, clsCommon.myCstr(objMO.Item_Code)) = CompairStringResult.Equal Then
                                isInsideLoadData = False
                                GoTo b
                            End If
                            isInsideLoadData = True
                            gv1.Rows.Clear()
                            gv1.Rows.AddNew()
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objMO.Item_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objMO.Item_Desc
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objMO.Item_Code, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objMO.Item_Code, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objMO.UOMMO
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = clsCommon.myCstr(txtRGPNo.Value)
                            If dr.Length > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = dr(0)(colRGPNo)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = dr(0)(colQty)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = dr(0)(colQty)
                            End If

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(objMO.Item_Code)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objMO.Item_Code)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(objMO.Item_Code)

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPICode).Value = strCode
                            isInsideLoadData = False
b:                          ' Next
                            If objMO.POSTED = ERPTransactionStatus.Pending Then
                                gv1.Rows.AddNew()
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                            End If
                            'End If
                        End If
                    End If '' End Of One To Many Condition
                Else '' In Case of None(Neither One To Many Nor Many To One)
                    Dim objR As New clsRGPHead()
                    objR = clsRGPHead.GetItemDataForSRN(clsCommon.myCstr(txtRGPNo.Value), strCode, NavTyep)
                    If (objR IsNot Nothing AndAlso clsCommon.myLen(objR.RGP_No) > 0) Then
                        If objR.Arr IsNot Nothing AndAlso objR.Arr.Count > 0 Then
                            For Each objRTr As clsRGPDetail In objR.Arr
                                isInsideLoadData = True
                                gv1.Rows.AddNew()
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                                'gv1.Rows(gv1.Rows.Count - 1).Cells(ColSelect).Value = False
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objRTr.Item_Code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objRTr.Item_Desc
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objRTr.Item_Code, Nothing)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objRTr.Item_Code, Nothing)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objRTr.Unit_code
                                '' Anubhooti 09-Dec-2014 (Fetch More Columns)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objRTr.Item_Cost
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objRTr.RGP_Qty
                                'gv1.Rows(gv1.Rows.Count - 1).Cells(colSp).Value = objRTr.Specification
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objRTr.Amount
                                If dr.Length > 0 Then
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = dr(0)(colRGPNo)
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = dr(0)(colQty)
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = dr(0)(colQty)
                                End If

                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(objRTr.Item_Code)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objRTr.Item_Code)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(objRTr.Item_Code)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPICode).Value = strCode
                                isInsideLoadData = False
                            Next
                            If objR.Status = ERPTransactionStatus.Pending Then
                                gv1.Rows.AddNew()
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                            End If
                        End If
                    End If
                End If
            Else
                For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                    If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colRGPICode).Value)) = CompairStringResult.Equal Then
                        gv1.Rows.RemoveAt(ii)
                    End If
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub
    Sub LoadRGPBlankGrid()
        gvRGP.Rows.Clear()
        gvRGP.Columns.Clear()
        gvRGP.Enabled = True
        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = ColSelect
        ' repoSelect.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoSelect.TextImageRelation = TextImageRelation.TextBeforeImage
        repoSelect.Width = 50
        gvRGP.MasterTemplate.Columns.Add(repoSelect)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        ' repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gvRGP.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gvRGP.MasterTemplate.Columns.Add(repoIName)


        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = True
        gvRGP.MasterTemplate.Columns.Add(repoUnit)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Unit Cost"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.FormatString = "{0:n4}"
        repoRate.DecimalPlaces = 4
        repoRate.ReadOnly = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRGP.MasterTemplate.Columns.Add(repoRate)

        Dim repoRGPQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRGPQty.FormatString = ""
        repoRGPQty.WrapText = True
        repoRGPQty.HeaderText = "Quantity"
        repoRGPQty.Name = colQty
        repoRGPQty.Width = 80
        repoRGPQty.Minimum = 0
        repoRGPQty.ReadOnly = True
        repoRGPQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRGP.MasterTemplate.Columns.Add(repoRGPQty)

        Dim repoRGPAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRGPAmt = New GridViewDecimalColumn()
        repoRGPAmt.FormatString = ""
        repoRGPAmt.HeaderText = "Extended Cost"
        repoRGPAmt.Name = colAmt
        repoRGPAmt.Width = 80
        repoRGPAmt.Minimum = 0
        repoRGPAmt.ReadOnly = True
        repoRGPAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRGP.MasterTemplate.Columns.Add(repoRGPAmt)

        Dim repoRGPSP As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRGPSP = New GridViewTextBoxColumn()
        repoRGPSP.FormatString = ""
        repoRGPSP.HeaderText = "Specification"
        repoRGPSP.Name = colSp
        repoRGPSP.Width = 100
        repoRGPSP.ReadOnly = True
        repoRGPSP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRGP.MasterTemplate.Columns.Add(repoRGPSP)
        '' Anubhooti 10-Dec-2014 (Item Conversion Type)
        Dim repoItemCon As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemCon = New GridViewTextBoxColumn()
        repoItemCon.FormatString = ""
        repoItemCon.HeaderText = "Item Conversion Type"
        repoItemCon.Name = colItemConType
        repoItemCon.Width = 150
        repoItemCon.ReadOnly = True
        repoItemCon.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvRGP.MasterTemplate.Columns.Add(repoItemCon)

        gvRGP.AllowDeleteRow = False
        gvRGP.AllowAddNewRow = False
        gvRGP.AllowEditRow = True
        gvRGP.ShowGroupPanel = False
        gvRGP.AllowColumnReorder = True
        gvRGP.AllowRowReorder = False
        gvRGP.EnableSorting = False
        gvRGP.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvRGP.MasterTemplate.ShowRowHeaderColumn = False
        gvRGP.TableElement.TableHeaderHeight = 40
        'gvRGP.AutoSizeRows = True
    End Sub
    Private Sub gvRGP_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvRGP.CellValueChanged
        'Try
        '    If e.Column Is gvRGP.Columns(ColSelect) Then
        '        If clsCommon.myCBool(gvRGP.CurrentRow.Cells(ColSelect).Value) = True Then
        '            LoadRGP_Items_Data(clsCommon.myCstr(gvRGP.CurrentRow.Cells(colICode).Value), NavigatorType.Current, e.NewValue)
        '        Else
        '            LoadRGP_Items_Data(clsCommon.myCstr(gvRGP.CurrentRow.Cells(colICode).Value), NavigatorType.Current, e.NewValue)
        '        End If
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.ToString)
        'End Try
    End Sub
    Private Sub RadPageViewPage1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles RadPageViewPage1.Paint

    End Sub
    Private Sub gvRGP_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvRGP.ValueChanged

    End Sub
    Private Sub gvRGP_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvRGP.ValueChanging
        Try
            If Not isInsideLoadData Then
                If gvRGP.CurrentColumn Is gvRGP.Columns(ColSelect) Then
                    'If clsCommon.myCBool(gvRGP.CurrentRow.Cells(ColSelect).Value) = True Then
                    LoadRGP_Items_Data(clsCommon.myCstr(gvRGP.CurrentRow.Cells(colICode).Value), NavigatorType.Current, e.NewValue)
                    'Else
                    '    LoadRGP_Items_Data(clsCommon.myCstr(gvRGP.CurrentRow.Cells(colICode).Value), NavigatorType.Current, e.NewValue)
                    'End If
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub
    Private Sub txtScheduleNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtScheduleNo._MYValidating
        SelectScheduleItems()
    End Sub
    Private Sub SelectScheduleItems()
        Dim obj As New clsPurchaseSchedule()
        Try
            isInsideLoadData = True

            Dim frm As New FrmPendingPOSchedule()
            frm.VendorCode = txtVendorNo.Value
            frm.VendorName = lblVendorName.Text
            frm.strCurrCode = txtDocNo.Value
            frm.strCurrDate = clsCommon.myCDate(txtDate.Text)
            frm.PurchaseOrder_Type = cboSRNType.SelectedValue
            frm.Is_From_RGP = False
            frm.ShowDialog()

            txtRequistionNo.Value = Nothing
            txtPONo.Value = Nothing
            txtRGPNo.Value = Nothing
            txtScheduleNo.Enabled = True
            LoadBlankGrid()

            If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
                obj = clsPurchaseSchedule.GetData(frm.ArrReturn(0).Document_Code, NavigatorType.Current)

                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                    txtRefNo.Text = Nothing
                    If clsCommon.myLen(txtDesc.Text) <= 0 Then
                        txtDesc.Text = obj.Description
                    End If

                    If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
                        cboItemType.SelectedValue = clsItemMaster.GetItemType(frm.ArrReturn(0).Item_Code, Nothing)
                    End If

                    txtDept.Value = Nothing
                    lblDept.Text = Nothing

                    txtBillToLocation.Value = clsPurchaseSchedule.GetBillToLocation(obj.PO_Code)
                    lblBillToLocation.Text = clsLocation.GetName(txtBillToLocation.Value, Nothing)


                    If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                        txtVendorNo.Value = obj.Vendor_Code
                        lblVendorName.Text = clsVendorMaster.GetName(obj.Vendor_Code, Nothing)
                        chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(frm.VendorCode)
                    End If
                    Dim qry As String = "select  Vendor_Code,Vendor_Name,Terms_Code,Terms_Code_Desc ,Vendor_Account ,Tax_Group,Tax_Group_Desc from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtVendorNo.Value + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                        txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                        lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Code_Desc"))
                        txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
                        lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
                        chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(txtVendorNo.Value)
                        SetMultiCurrencyVisibility()
                    End If

                    txtRequistionNo.Value = Nothing
                    cboSRNType.SelectedValue = obj.PO_Type

                    gv_roadpermit.Rows.Clear()
                    gv_roadpermit.Rows.AddNew()
                    gv_c_form.Rows.Clear()
                    gv_c_form.Rows.AddNew()

                    Chkroadpermit.Checked = False

                    chk_c_form.Checked = False

                    LoadBlankGridAC()
                    LoadBlankGridACInsurance()

                    rbtnTaxCalAutomatic.IsChecked = True
                    gvAC.Rows.AddNew()

                    If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                        gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                    End If


                    Dim mrnno As String = ""

                    For Each objtr As clsPurchaseScheduleDetail In frm.ArrReturn
                        If IsValidScheduleItem(objtr) Then
                            gv1.Rows.AddNew()
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = objtr.PO_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colScheduleNo).Value = objtr.Document_Code
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colMRN_NO).Value = obj.MRN_No
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colGRN_NO).Value = obj.GRN_No_Temp
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.Item_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objtr.Item_Name
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objtr.Item_Code, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objtr.Item_Code, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(objtr.Item_Code)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objtr.Item_Code)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(objtr.Item_Code)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRNUnitCost).Value = Nothing
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = Nothing

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = Nothing
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objtr.Unit_Code


                            If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "F") = CompairStringResult.Equal Then
                                Dim strFCode As String = clsItemMaster.GetFatherCode(objtr.Item_Code, Nothing)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colFCode).Value = strFCode
                                If clsCommon.myLen(strFCode) > 0 Then
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFRate).Value = clsItemPriceMaster.GetMRPOfFinishItem(strFCode, objtr.Unit_Code)
                                End If
                            End If

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgPOQty).Value = objtr.PO_Qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSchQty).Value = objtr.Schedule_Qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objtr.balance_qty


                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = objtr.balance_qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDisType).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = Nothing
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(objtr.Item_Code)

                            dt = Nothing ' clsSRNHead.GetOriginalQty(obj.MRN_No, obj.PurchaseOrder_No, obj.Item_Code, obj.Unit_code, obj.Assessable, obj.MRP, Nothing)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgPOQty).Value = clsCommon.myCdbl(dt.Rows(0)("PurchaseOrder_Qty"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgGRNQty).Value = clsCommon.myCdbl(dt.Rows(0)("GRN_Qty"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgMRNQty).Value = clsCommon.myCdbl(dt.Rows(0)("MRN_Qty"))
                                If IsQCColumnRequiredonMRN Then
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dt.Rows(0)("accept_qty"))
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colShortQty).Value = clsCommon.myCdbl(dt.Rows(0)("Short_Qty"))
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectedQty).Value = clsCommon.myCdbl(dt.Rows(0)("Reject_qty"))
                                End If
                            End If

                        End If 'valid cond.
                        mrnno = objtr.Document_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementRate).Value = Nothing
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssesableMRP).Value = Nothing
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssesableMRP).Value = Nothing
                    Next 'grid loop
                End If 'obj cond.
            End If ' array cond.

            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem

            If rbtnTaxCalManual.IsChecked Then
                For ii As Integer = 1 To 10
                    If gv2.Rows.Count >= ii Then
                        Dim dblTotTaxAmt As Double = 0
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            dblTotTaxAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells("COLTAXAMT" + clsCommon.myCstr(ii)).Value)
                        Next
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = dblTotTaxAmt
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(gv1.Rows(0).Cells("COLTAXRATE" + clsCommon.myCstr(ii)).Value)
                    End If
                Next
            End If


            SetitemWiseTaxSetting(False, False)
            For ii As Integer = 0 To gv1.RowCount - 1
                UpdateCurrentRow(ii)
            Next
            If rbtnTaxCalManual.IsChecked Then ''For Calcuation custom tax according to ratio of amount
                For ii As Integer = 0 To gv1.RowCount - 1
                    UpdateCurrentRow(ii)
                Next
            End If

            UpdateAllTotals()
            RefreshSchNo()


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            obj = Nothing
        End Try
    End Sub
    Sub RefreshSchNo()
        txtScheduleNo.Value = ""
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String

                strReqNo = clsCommon.myCstr(gv1.Rows(ii).Cells(colScheduleNo).Value)

                If clsCommon.myLen(strReqNo) > 0 Then
                    txtScheduleNo.Value = clsCommon.myCstr(strReqNo)
                    Exit Sub
                End If
            Next
        End If
    End Sub
    Function IsValidScheduleItem(ByVal obj As clsPurchaseScheduleDetail)
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colScheduleNo).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strReqCode, obj.Document_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, obj.Unit_Code) = CompairStringResult.Equal Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "PO No : " + obj.Document_Code + "  Item : " + obj.Item_Name + Environment.NewLine + ""
                If dblMRP > 0 Then
                    strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
                End If
                common.clsCommon.MyMessageBoxShow(strMsg)
                Return False
            End If
        Next
        Return True
    End Function
    Function IsValidRGPJOBWORKItem(ByVal obj As clsRGPBOMItem)
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRGPNo).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strReqCode, obj.RGP_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, obj.Unit_Code) = CompairStringResult.Equal Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "RGP No : " + obj.RGP_No + "  Item : " + obj.Iname + Environment.NewLine + ""
                If dblMRP > 0 Then
                    strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
                End If
                common.clsCommon.MyMessageBoxShow(strMsg)
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub cboSRNType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboSRNType.SelectedValueChanged
        If isInsideLoadData Then
            Exit Sub
        End If
        If clsCommon.CompairString(cboSRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso isRGPAfterPO AndAlso Not is_Load_MRN Then
            cmbRGPType.Enabled = True
            cmbRGPType.SelectedValue = ""
        Else
            cmbRGPType.Enabled = False
            cmbRGPType.SelectedValue = ""
        End If
        'If clsCommon.CompairString(cboSRNType.SelectedValue, "O") = CompairStringResult.Equal Then
        '    txtSubLocation.Enabled = True
        'Else
        '    txtSubLocation.Enabled = False
        '    txtSubLocation.Value = ""
        '    lblShipToLocation.Text = ""
        'End If
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim qry As String = ""
        Dim dt As New DataTable()
        Try
            qry = "select * from TEMP_DELETE_SRN where srn_no not in (select srn_no from TEMP_CREATE_SRN_TRANS)"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim strErro As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If common.clsCommon.MyMessageBoxShow("Update New Colums of  " + clsCommon.myCstr(dt.Rows.Count) + " SRN", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    clsCommon.ProgressBarPercentShow()
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        Dim strSRNNo As String = clsCommon.myCstr(dt.Rows(ii)("srn_no"))
                        LoadData(strSRNNo, NavigatorType.Current)
                        SaveData(False)
                        'For jj As Integer = 0 To gv1.Rows.Count - 1
                        '    UpdateCurrentRow(jj)
                        'Next
                        'UpdateAllTotals()
                        'CalLandAmt()
                        'CalNonRectax()
                        'CalRectax()
                        'CalAddtionalAmt()

                        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        'Try
                        '    Dim coll As New Hashtable()
                        '    For Each grow As GridViewRowInfo In gv1.Rows
                        '        coll = New Hashtable()
                        '        clsCommon.AddColumnsForChange(coll, "Short_Qty", clsCommon.myCdbl(grow.Cells(colShortQty).Value))
                        '        clsCommon.AddColumnsForChange(coll, "Rejected_Qty", clsCommon.myCdbl(grow.Cells(colRejectedQty).Value))
                        '        clsCommon.AddColumnsForChange(coll, "Landed_Cost_Rate", clsCommon.myCdbl(grow.Cells(colLandedRate).Value))
                        '        clsCommon.AddColumnsForChange(coll, "Landed_Cost_Amount", clsCommon.myCdbl(grow.Cells(colLandedAmt).Value))
                        '        clsCommon.AddColumnsForChange(coll, "Accepted_Amount", clsCommon.myCdbl(grow.Cells(colAcceptedAmount).Value))
                        '        clsCommon.AddColumnsForChange(coll, "Rejected_Amount", clsCommon.myCdbl(grow.Cells(colRejectedAmount).Value))
                        '        clsCommon.AddColumnsForChange(coll, "Shortage_Amount", clsCommon.myCdbl(grow.Cells(colShortageAmount).Value))
                        '        clsCommon.AddColumnsForChange(coll, "Leak_Amount", clsCommon.myCdbl(grow.Cells(colLeakAmount).Value))
                        '        clsCommon.AddColumnsForChange(coll, "Burst_Amount", clsCommon.myCdbl(grow.Cells(colBurstAmount).Value))
                        '        clsCommon.AddColumnsForChange(coll, "Amt_Less_Discount_Without_Shortage", clsCommon.myCdbl(grow.Cells(colAmtLessDiscountWithoutShortage).Value))
                        '        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SRN_DETAIL", OMInsertOrUpdate.Update, "SRN_No='" + strSRNNo + "' and Line_No='" + clsCommon.myCstr(grow.Cells(colLineNo).Value) + "'", trans)
                        '    Next
                        '    coll = New Hashtable()
                        '    clsCommon.AddColumnsForChange(coll, "Total_Accepted_Amount", clsCommon.myCdbl(lblAcceptedAmt.Text))
                        '    clsCommon.AddColumnsForChange(coll, "Total_Rejected_Amount", clsCommon.myCdbl(lblRejectedAmt.Text))
                        '    clsCommon.AddColumnsForChange(coll, "Total_Shortage_Amount", clsCommon.myCdbl(lblShortageAmt.Text))
                        '    clsCommon.AddColumnsForChange(coll, "Total_Leak_Amount", clsCommon.myCdbl(lblLeakAmt.Text))
                        '    clsCommon.AddColumnsForChange(coll, "Total_Burst_Amount", clsCommon.myCdbl(lblBurstAmt.Text))
                        '    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SRN_HEAD", OMInsertOrUpdate.Update, "SRN_No='" + strSRNNo + "'", trans)

                        '    coll = New Hashtable()
                        '    clsCommon.AddColumnsForChange(coll, "SRN_No", strSRNNo)
                        '    clsCommonFunctionality.UpdateDataTable(coll, "TEMP_CREATE_SRN_TRANS", OMInsertOrUpdate.Insert, "", trans)

                        '    trans.Commit()
                        'Catch ex As Exception
                        '    trans.Rollback()
                        '    strErro += "Srn No - " + strSRNNo + ", Exception -" + ex.Message + Environment.NewLine
                        'End Try
                        clsCommon.ProgressBarPercentUpdate(ii * 100 / dt.Rows.Count - 1, "Recreate journal entry " + clsCommon.myCstr(dt.Rows.Count - 1) + "/" + clsCommon.myCstr(ii))
                    Next
                    clsCommon.ProgressBarPercentHide()
                End If
            End If
            If clsCommon.myLen(strErro) > 0 Then
                common.clsCommon.MyMessageBoxShow(strErro, Me.Text)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Task completed", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnUpdateRoadPermit_Click(sender As Object, e As EventArgs) Handles btnUpdateRoadPermit.Click
        Try
            Dim Qry As String = "update TSPL_SRN_HEAD set RoadPermit_No='" & txt_RoadPermitNo.Text & "' ,RoadPermit_Date='" & clsCommon.GetPrintDate(txt_RoadPermitDate.Value, "dd/MMM/yyyy") & "' where SRN_No='" & txtDocNo.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub chk_qc_accepted_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chk_qc_accepted.ToggleStateChanged

    End Sub
    Private Sub txtSubLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSubLocation._MYValidating
        Dim strLocations = String.Empty

        If clsCommon.myLen(txtBillToLocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Bill To location code before sub location", Me.Text)
            Exit Sub
        End If
        If chkJobWorkOutward.Checked Then
            txtSubLocation.Value = clsLocation.getFinder("(Main_Location_Code='" & txtBillToLocation.Value & "' and Is_Jobwork=1 and isnull(Is_Sub_Location,'N')='Y')" & strLocations, txtSubLocation.Value, isButtonClicked)
        Else
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                txtSubLocation.Value = clsLocation.getFinder(" (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code='" + txtBillToLocation.Value + "'", txtSubLocation.Value, isButtonClicked)
            End If
        End If

        If clsCommon.myLen(txtSubLocation.Value) > 0 Then
            lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
        Else
            lblSubLocation.Text = ""
        End If
        strLocations = Nothing
    End Sub
    Function CancelData() As Boolean
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Code is empty")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure to Cancel the Record?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Exit Function
            End If
            'Dim count As Integer = clsDBFuncationality.getSingleValue("select count(*)  from TSPL_PI_HEAD where Against_SRN ='" + txtDocNo.Value + "' ")
            'If count > 0 Then
            '    clsCommon.MyMessageBoxShow("You can't cancelled because this document used in Purchase Invoice")
            '    Exit Function
            'End If

            ''
            Dim Qry As String = "select distinct PI_No from TSPL_PI_DETAIL where SRN_Id='" + txtDocNo.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "CURRENT SRN IS USED IN FOLLOWING PURCHASE INVOICE -"
                For Each DR As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(DR("PI_NO"))
                Next
                clsCommon.MyMessageBoxShow(Qry)
                Exit Function
            End If
            ''
            clsSRNHead.CancelData(Me.Form_ID, txtDocNo.Value, IIf(clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal, "MT", "SRN"))
            clsCommon.MyMessageBoxShow(Me, "Successfully Cancelled", Me.Text)
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CancelData()
    End Sub
    Sub chkDocNoUseInInvoice()


    End Sub
    ' Ticket No : TEC/29/10/18-000354 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Value)
    End Sub
    Sub LoadBlankGridACInsurance()
        gvACInsurance.Rows.Clear()
        gvACInsurance.Columns.Clear()

        Dim repoACCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACCode.FormatString = ""
        repoACCode.HeaderText = "Addition Charges Code"
        repoACCode.Name = colACInsuranceCode
        repoACCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoACCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoACCode.Width = 100
        repoACCode.ReadOnly = False
        gvACInsurance.MasterTemplate.Columns.Add(repoACCode)

        Dim repoACName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACName.FormatString = ""
        repoACName.HeaderText = "Addition Charges Description"
        repoACName.Name = colACInsuranceName
        repoACName.Width = 150
        repoACName.ReadOnly = True
        gvACInsurance.MasterTemplate.Columns.Add(repoACName)

        Dim repoACAmt = New GridViewDecimalColumn()
        repoACAmt.FormatString = ""
        repoACAmt.HeaderText = "Amount"
        repoACAmt.Name = colACInsuranceAmount
        repoACAmt.Width = 100
        repoACAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoACAmt.ReadOnly = False
        gvACInsurance.MasterTemplate.Columns.Add(repoACAmt)

        gvACInsurance.AllowAddNewRow = False
        gvACInsurance.ShowGroupPanel = False
        gvACInsurance.AllowColumnReorder = True
        gvACInsurance.AllowRowReorder = False
        gvACInsurance.EnableSorting = False
        gvACInsurance.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvACInsurance.MasterTemplate.ShowRowHeaderColumn = False
        gvACInsurance.TableElement.TableHeaderHeight = 40
        gvACInsurance.Rows.AddNew()
    End Sub
    Private Sub gvACInsurance_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvACInsurance.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvACInsurance.Columns(colACInsuranceCode) Then
                        Dim obj As clsAdditionalCharge = clsAdditionalCharge.GetFinder(clsCommon.myCstr(gvACInsurance.CurrentRow.Cells(colACInsuranceCode).Value), False)
                        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                            gvACInsurance.CurrentRow.Cells(colACInsuranceCode).Value = obj.Code
                            gvACInsurance.CurrentRow.Cells(colACInsuranceName).Value = obj.desc
                        Else
                            gvACInsurance.CurrentRow.Cells(colACInsuranceCode).Value = ""
                            gvACInsurance.CurrentRow.Cells(colACInsuranceName).Value = ""
                            gvACInsurance.CurrentRow.Cells(colACInsuranceAmount).Value = 0
                        End If
                    ElseIf e.Column Is gvACInsurance.Columns(colACInsuranceAmount) Then
                        CalculateInsuranceTotal(True)
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub CalculateInsuranceTotal(ByVal CalculateItemRow As Boolean)
        Dim dblACAmount As Decimal = 0
        For ii As Integer = 0 To gvACInsurance.Rows.Count - 1
            If (clsCommon.myLen(gvACInsurance.Rows(ii).Cells(colACInsuranceAmount).Value) > 0) Then
                dblACAmount += clsCommon.myCdbl(gvACInsurance.Rows(ii).Cells(colACInsuranceAmount).Value)
            End If
        Next
        lblAddChargesForInsurance.Text = clsCommon.myFormat(dblACAmount)
        lblAddChargesForInsurance1.Text = clsCommon.myFormat(dblACAmount)
        If CalculateItemRow Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()
        End If
    End Sub
    Private Sub gvACInsurance_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvACInsurance.UserDeletedRow
        UpdateAllTotals()
    End Sub
    Private Sub gvACInsurance_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvACInsurance.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    Private Sub gvACInsurance_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvACInsurance.CurrentColumnChanged
        If gvACInsurance.RowCount > 0 Then
            Dim intCurrRow As Integer = gvACInsurance.CurrentRow.Index
            If intCurrRow = gvACInsurance.Rows.Count - 1 Then
                gvACInsurance.Rows.AddNew()
                gvACInsurance.CurrentRow = gvACInsurance.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub RadButton2_Click_1(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, "SRN_No", "TSPL_SRN_HEAD", "TSPL_SRN_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnJE_Click(sender As Object, e As EventArgs) Handles btnJE.Click
        ShowJE(MyBase.Form_ID, txtDocNo.Value)
    End Sub

    'Private Sub MyDateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles MyDateTimePicker1.ValueChanged
    '    txtDate.Text = clsCommon.myCDate(MyDateTimePicker1.Text)
    'End Sub

End Class