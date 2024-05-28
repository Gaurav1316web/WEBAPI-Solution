Imports System.IO
Imports common
Imports System.Text.RegularExpressions
Imports System.Text
'BHA/22/06/18-000080 by balwinder on 22/06/2018
' work to be done agaist ticket no. UDL/27/06/18-000196, UDL/27/06/18-000197,BHA/16/05/18-000026 by Parteek
'' Work to be done Related Work order setting Based UDL/30/10/18-000236
'' work to be done related to delivery date BHA/02/11/18-000661
'====Update by Preeti Gupta Against ticket no[ERO/26/06/19-000659]


Public Class frmPurchaseOrder
    Inherits FrmMainTranScreen

#Region "Variables"
    Public GSTExemptedAmount As Decimal = 0
    Private PurchaseModulePickFixTaxRate As Boolean = False
    Private isSettlementBankOnly As Boolean = False
    Public AllowModifcationByApprovalUser As Boolean = False
    Dim Schedule_ON As Boolean = False
    Public IsRGPAfterPO As Boolean = False
    Public UDLPurchaseOrderthroughAP As Boolean = False
    Public IsRemarkReasonMandatoryOnPO As Boolean = False
    Public atchqry As String = ""
    Public isCellValueChangedTaxOpen As Boolean = False
    Dim AllowPurchaseModulewithUniqueItem As Integer = 0
    Public isCellValueChangedOpen As Boolean = False
    Public IsFormLoad As Boolean = False
    Public IsLoadOk As Boolean = False
    Public isPOTypeLoad As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = True
    Public isInsideLoadData As Boolean = False
    Public isInsideLoadDatamt As Boolean = False
    Public SendMailForAdvancePaymenTerms As Boolean = False
    Dim arrLoc As String = ""

    Const colLineNo As String = "COLLNO"
    Const colRowType As String = "COLTYPE"
    Const colRGPNo As String = "COLRGPNo"
    Const colComplete As String = "COMPLETE"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colHSNNo As String = "COLHSNNo"
    Const colItemTaxable As String = "colItemTaxable"
    Const colAgainstItemWiseTaxCode As String = "colAgainstItemWiseTaxCode"
    Const colWOCategoy As String = "colWOCategoy"
    Const colWOCatDesc As String = "colWOCatDesc"
    Const colTerms As String = "colTerms"
    Const colTerms1 As String = "colTerms1"

    Const colPendingQty As String = "COLPENDINGQTY"
    Const colLastRateSameVendor As String = "LastSameVendorRate"
    Const colLastRateOtherVendor As String = "LastOtherVendorRate"
    Const colOrgRequitionQty As String = "COLORIGPEQQTY"
    Const colQty As String = "COLQTY"
    Const colUnit As String = "COLUNIT"
    Dim RequiredPOLimit As Boolean = False
    Const colRate As String = "COLRATE"

    Const colIsInsurance As String = "colIsInsurance"
    Const colInsuranceBaseAmt As String = "colInsuranceBaseAmt"
    Const colInsurancePer As String = "colInsurancePer"

    Const colItemInsuranceBaseAmt As String = "colItemInsuranceBaseAmt"
    Const colItemInsuranceApplyOn As String = "colItemInsuranceApplyOn"
    Const colItemInsurancePer As String = "colItemInsurancePer"
    Const colItemInsuranceAmt As String = "colItemInsuranceAmt"
    Const colItemAmtAfterInsurance As String = "colItemAmtAfterInsurance"

    Const colAmt As String = "COLAMT"

    Const colHeaderDiscountPer As String = "colHeaderDiscountPer"
    Const colHeaderDiscountAmt As String = "colHeaderDiscountAmt"
    Const colDisPerUnit As String = "COLDISPERUNIT"
    Const colDisAmtPerUnit As String = "COLDISAMTPERUNIT"
    Const colDisPer As String = "COLDISPER"
    Const colDetailDisAmt As String = "colDetailDisAmt"

    Const colDisAmt As String = "COLDISAMT"
    Const colAmtAfterDis As String = "COLAMTAFTERDIS"

    Const colTaxableAmount As String = "colTaxableAmount"
    Const colTaxableAmountPer As String = "colTaxableAmountPer"

    Public strFormId As String
    Const ReportID As String = "POItemGrid"
    Const colAbatementRate As String = "colAbatementRate"
    Const colAssesableMRP As String = "colAssesableMRP"
    Const colTotalAssesableMRP As String = "colTotalAssesableMRP"
    Public IsAbatementPO As Boolean
    Const colFatPerMT As String = "colFatPerMT"
    Const colSNFFPerMT As String = "colSNFFPerMT"
    Const colFatKGMT As String = "colFatKGMT"
    Const colSNFKGMT As String = "colSNFKGMT"
    Const colWeightUOMMT As String = "colWeightUOMMT"
    Const colItemWeightMT As String = "colItemWeightMT"
    Const colItemImage As String = "colItemImage"

    Const colTax1 As String = "COLTAX1"
    Const colTaxBaseAmt1 As String = "COLTAXBASEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"
    Const colIsTaxable1 As String = "ISTAXABLE1"
    Const colIsSurTax1 As String = "ISSURTAX1"
    Const colSurTaxCode1 As String = "SURTAXCODE1"
    Const colIsExcisable1 As String = "ISEXCISABLE1"
    Const colTaxOnBaseAmt1 As String = "colTaxOnBaseAmt1"

    Const colTax2 As String = "COLTAX2"
    Const colTaxBaseAmt2 As String = "COLTAXBASEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colIsTaxable2 As String = "ISTAXABLE2"
    Const colIsSurTax2 As String = "ISSURTAX2"
    Const colSurTaxCode2 As String = "SURTAXCODE2"
    Const colIsExcisable2 As String = "ISEXCISABLE2"
    Const colTaxOnBaseAmt2 As String = "colTaxOnBaseAmt2"

    Const colTax3 As String = "COLTAX3"
    Const colTaxBaseAmt3 As String = "COLTAXBASEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colIsTaxable3 As String = "ISTAXABLE3"
    Const colIsSurTax3 As String = "ISSURTAX3"
    Const colSurTaxCode3 As String = "SURTAXCODE3"
    Const colIsExcisable3 As String = "ISEXCISABLE3"
    Const colTaxOnBaseAmt3 As String = "colTaxOnBaseAmt3"

    Const colTax4 As String = "COLTAX4"
    Const colTaxBaseAmt4 As String = "COLTAXBASEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colIsTaxable4 As String = "ISTAXABLE4"
    Const colIsSurTax4 As String = "ISSURTAX4"
    Const colSurTaxCode4 As String = "SURTAXCODE4"
    Const colIsExcisable4 As String = "ISEXCISABLE4"
    Const colTaxOnBaseAmt4 As String = "colTaxOnBaseAmt4"

    Const colTax5 As String = "COLTAX5"
    Const colTaxBaseAmt5 As String = "COLTAXBASEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colIsTaxable5 As String = "ISTAXABLE5"
    Const colIsSurTax5 As String = "ISSURTAX5"
    Const colSurTaxCode5 As String = "SURTAXCODE5"
    Const colIsExcisable5 As String = "ISEXCISABLE5"
    Const colTaxOnBaseAmt5 As String = "colTaxOnBaseAmt5"

    Const colTax6 As String = "COLTAX6"
    Const colTaxBaseAmt6 As String = "COLTAXBASEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colIsTaxable6 As String = "ISTAXABLE6"
    Const colIsSurTax6 As String = "ISSURTAX6"
    Const colSurTaxCode6 As String = "SURTAXCODE6"
    Const colIsExcisable6 As String = "ISEXCISABLE6"
    Const colTaxOnBaseAmt6 As String = "colTaxOnBaseAmt6"

    Const colTax7 As String = "COLTAX7"
    Const colTaxBaseAmt7 As String = "COLTAXBASEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colIsTaxable7 As String = "ISTAXABLE7"
    Const colIsSurTax7 As String = "ISSURTAX7"
    Const colSurTaxCode7 As String = "SURTAXCODE7"
    Const colIsExcisable7 As String = "ISEXCISABLE7"
    Const colTaxOnBaseAmt7 As String = "colTaxOnBaseAmt7"

    Const colTax8 As String = "COLTAX8"
    Const colTaxBaseAmt8 As String = "COLTAXBASEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colIsTaxable8 As String = "ISTAXABLE8"
    Const colIsSurTax8 As String = "ISSURTAX8"
    Const colSurTaxCode8 As String = "SURTAXCODE8"
    Const colIsExcisable8 As String = "ISEXCISABLE8"
    Const colTaxOnBaseAmt8 As String = "colTaxOnBaseAmt8"

    Const colTax9 As String = "COLTAX9"
    Const colTaxBaseAmt9 As String = "COLTAXBASEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colIsTaxable9 As String = "ISTAXABLE9"
    Const colIsSurTax9 As String = "ISSURTAX9"
    Const colSurTaxCode9 As String = "SURTAXCODE9"
    Const colIsExcisable9 As String = "ISEXCISABLE9"
    Const colTaxOnBaseAmt9 As String = "colTaxOnBaseAmt9"

    Const colTax10 As String = "COLTAX10"
    Const colTaxBaseAmt10 As String = "COLTAXBASEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colIsTaxable10 As String = "ISTAXABLE10"
    Const colIsSurTax10 As String = "ISSURTAX10"
    Const colSurTaxCode10 As String = "SURTAXCODE10"
    Const colIsExcisable10 As String = "ISEXCISABLE10"
    Const colTaxOnBaseAmt10 As String = "colTaxOnBaseAmt10"

    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"
    Const colReqistionNo As String = "REQNO"
    Const colSpecification As String = "SPECIFICATION"
    Const colRemarks As String = "REMARKS"
    Const colCapacity As String = "Capacity"
    Const colMake As String = "Make"
    Const colModel As String = "Modle"

    Const colItemUsedINGRN As String = "USEDINGRN"
    Const colisMRPMandatory As String = "colisMRPMandatory"
    Const colMRP As String = "MRP"
    Const colBinNo As String = "colBinNo"
    Const colAssessableAmount As String = "ASSESSABLEAMT"
    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"
    Const colTTaxAssessableAmt As String = "COLTTAXASSESSABLEAMT"

    Const colACCode As String = "COLACCODE"
    Const colACName As String = "COLACNAME"
    Const colACApplyOn As String = "COLACAPPLYON"
    Const colACPer As String = "COLACPER"
    Const colACAmount As String = "COLACAMOUNT"

    Const colACInsuranceCode As String = "colACInsuranceCode"
    Const colACInsuranceName As String = "colACInsuranceName"
    Const colACInsuranceAmount As String = "colACInsuranceAmount"


    '' new columns for job work of non inventory type
    Const colQty_Desc As String = "colQty_Desc"
    Const colRate_Desc As String = "colRate_Desc"
    Const colAmount_Desc As String = "colAmount_Desc"
    'For pending POQty for GRN
    Const colPrevPONO As String = "colPrevPONO"
    Const colPrevPOQty As String = "colPrevPOQty"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim i As Integer
    Dim repoComplete As GridViewTextBoxColumn

    Dim closeyn As String
    Dim vaddnew As String

    Public isItemfromVendorItemDetails As Boolean = False
    Public ArrItem As List(Of clsItemMaster)
    Public PurchaseOrderNo As String

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
    Dim FORMTYPE As String = Nothing
    Dim isApplyBrachAccounting As Boolean = False
    Dim ShowPOCancelButton As Boolean = False
    Dim ShowCapexCodeandSubCode As Boolean = False
    Dim ShowItemInCaseofNonInventory As Boolean = False

    Dim ShowLastUnitCostZeroForNonInventoryItemOnPO As Boolean = False

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
    ''==================================================================

    Const colScheduleSNo As String = "colScheduleSNo"
    Const colScheduleParentSNo As String = "colScheduleParentSNo"
    Const colScheduleNo As String = "colScheduleNo"
    Const colScheduleFromDate As String = "colScheduleFromDate"
    Const colScheduleToDate As String = "colScheduleToDate"
    Const colScheduleICode As String = "colScheduleICode"
    Const colScheduleIName As String = "colScheduleIName"
    Const colScheduleQtyPer As String = "colScheduleQtyPer"
    Const colScheduleQty As String = "colScheduleQty"
    Const colScheduleShortPer As String = "colScheduleShortPer"
    Const colScheduleShort As String = "colScheduleShort"
    Const colScheduleLateDays As String = "colScheduleLateDays"
    Const colScheduleExtensionDays As String = "colScheduleExtensionDays"

    Dim ChkAutoDepOnPurchaseCycle As Boolean = False
    Dim SettingIndendFreePOClose As Boolean = False
    Dim DoNotAllowSavePOWhenQtyAndRateZero As Boolean = False
    Private objRemittance As clsRemittance
    Dim ShowMessageTDS As Boolean = False
    Dim dblPreviousTDSAmt As Double = 0
    Dim settCreatePOFromMultipleLocation As Boolean = True
    Dim CommentSetting As Boolean = False
    Dim strPdfAttachmentPath As String = ""
    Public ShowItemAllStructureWise As Boolean = False
#End Region

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''MIL/01/08/19-000115 by balwinder on 01/08/2019  
        ShowItemAllStructureWise = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowItemAllStructureWise, clsFixedParameterCode.ShowItemAllStructureWise, Nothing)) = 1, True, False)

        ShowMessageTDS = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowMessgForTDS, clsFixedParameterCode.ShowMessgForTDS, Nothing)) = "1", True, False))
        settCreatePOFromMultipleLocation = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreatePOFromMultipleLocation, clsFixedParameterCode.CreatePOFromMultipleLocation, Nothing)) > 0)
        CommentSetting = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CmtSetting, clsFixedParameterCode.CmtSetting, Nothing)) > 0)
        SettingIndendFreePOClose = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FreeIndentQtyAfterPOClose, clsFixedParameterCode.FreeIndentQtyAfterPOClose, Nothing)) > 0)
        DoNotAllowSavePOWhenQtyAndRateZero = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotAllowSavePOWhenQtyNRateZero, clsFixedParameterCode.DoNotAllowSavePOWhenQtyNRateZero, Nothing)) > 0)
        ShowLastUnitCostZeroForNonInventoryItemOnPO = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowLastUnitCostZeroForNonInventoryItemOnPO, clsFixedParameterCode.ShowLastUnitCostZeroForNonInventoryItemOnPO, Nothing)) = "1", True, False))
        GSTExemptedAmount = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GSTExemptedAmountForNonRegisteredVendor, clsFixedParameterCode.GSTExemptedAmountForNonRegisteredVendor, Nothing))
        AllowPurchaseModulewithUniqueItem = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseModulewithUniqueItem, clsFixedParameterCode.AllowPurchaseModulewithUniqueItem, Nothing))
        UcItemBalance1.CommitedQty = False
        UcItemBalance1.CommitedQtyLbl = False
        SetUserMgmtNew()
        txtDeliveryDate.ReadOnly = True
        btn_cancel.Visible = False
        ShowCapexCodeandSubCode = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapex, clsFixedParameterCode.ShowOptionforSelectingCapex, Nothing)) = "1", True, False))
        If ShowCapexCodeandSubCode Then
            SplitContainer2.Panel1Collapsed = False
            MyLabel39.Visible = True
            ddl_category.Visible = True
            chk_emergency.Visible = True
        Else
            SplitContainer2.Panel1Collapsed = True
            MyLabel39.Visible = False
            ddl_category.Visible = False
            chk_emergency.Visible = False
        End If
        LoadCategory()
        IsRGPAfterPO = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.IsRGPAfterPurchaseOrder, clsFixedParameterCode.IsRGPAfterPurchaseOrder, Nothing)) = "1", True, False))
        chkAgainst_RGP.Visible = True
        If IsRGPAfterPO Then
            chkAgainst_RGP.Visible = False
        End If
        Schedule_ON = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowPOScheduling, clsFixedParameterCode.AllowPOScheduling, Nothing)) = "1", True, False))
        IsRemarkReasonMandatoryOnPO = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsRemarkReasonMandatoryOnPO, clsFixedParameterCode.IsRemarkReasonMandatoryOnPO, Nothing)) = 1, True, False)
        isItemfromVendorItemDetails = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchasePickItemFromVendorItemDetails, clsFixedParameterCode.PurchasePickItemFromVendorItemDetails, Nothing)) = 1, True, False)
        PurchaseModulePickFixTaxRate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchaseModulePickFixTaxRate, clsFixedParameterCode.PurchaseModulePickFixTaxRate, Nothing)) = 1, True, False)

        txtVendorNo.MendatroryField = True
        LoadModeOfTrasport()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnForm_Update, "Press Alt+U for Update Forms Entry")
        IsAbatementPO = clsPurchaseOrderHead.GetPurchaseSetting().Rows(0).Item("IsAbatementPO")
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadBlankGrid()
        LoadBlankGridSchedule()
        LoadBlankRoadPermitGrid()
        LoadBlankCFORMGrid()
        LoadBlankGridTax()
        IsFormLoad = True
        LoadPOType()
        LoadCategory()
        isPOTypeLoad = True
        LoadItemType()
        LoadBlankGridAC()
        LoadBlankGridACInsurance()
        LoadWorkOrderValueGrid()
        LoadWorkOrderTermsGrid()

        'ToolStrip1.LayoutStyle = ToolStripLayoutStyle.Table
        SetLength()
        IsFormLoad = False

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        'txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        'If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
        '    lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        'End If
        ''End of For Custom Fields
        '' MultiCurrency
        SetMultiCurrencyVisibility()
        '' End of MultiCurrency

        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment
        'If Not objCommonVar.IsDemoERP Then
        'pnlPCJ.Visible = False
        'End If
        '' make editable/non editable Term Code
        txtTermCode.Enabled = clsPurchaseOrderHead.GetInventorySetting().Rows(0).Item("IsTermsEditableOnPurchase")
        ' AddNew()
        LoadItems()
        ''richa agarwal 15/07/2015 BM00000007399
        Dim WhrCls As String = String.Empty
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmPurchaseOrderMT) = CompairStringResult.Equal Then
            WhrCls = " and Location_Type='Virtual' "
        Else
            WhrCls = " and Location_Type='Physical' "
        End If
        ' txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' " & WhrCls & " "))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
            '' Anubhooti 30-Dec-2014
            txtRefNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))
        End If

        If clsCommon.CompairString(clsFixedParameter.GetData("MilkProc", "EnableMilkProc", Nothing), "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterCode.MCCPurchase, clsFixedParameterType.MCCPurchase, Nothing), "1") = CompairStringResult.Equal Then
            Panel2.Visible = True
        Else
            Panel2.Visible = False
        End If
        AddNew()
        'MyLabel7.Text = ""
        LoadDefaultTermDetail()

        'SANJAY
        'If clsCommon.myLen(PurchaseOrderNo) > 0 Then
        '    LoadData(PurchaseOrderNo, NavigatorType.Current)
        'End If
        'If clsCommon.myLen(Me.Tag) > 0 Then
        '    LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        'End If
        'SANJAY

        btnHistory.Enabled = False
        btnForm_Update.Enabled = False
        ''richa agarwal 16/12/2014
        'chkIsMerchantTrade.Visible = False
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.mbtnPurchaseOrder) = CompairStringResult.Equal OrElse clsCommon.CompairString(FORMTYPE, clsUserMgtCode.WorkOrderEng) = CompairStringResult.Equal Then
            chkIsMerchantTrade.Visible = False
            chkIsMerchantTrade.Checked = False
            RadPageView1.Pages("RdPaymentterms").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
        Else
            chkIsMerchantTrade.Visible = True
            chkIsMerchantTrade.Checked = True
            chkIsMerchantTrade.Enabled = False
            RadPageView1.Pages("RdPaymentterms").Item.Visibility = ElementVisibility.Visible
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        End If

        btnUnpost.Visible = False
        ''-----------------
        isApplyBrachAccounting = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, Nothing)) = 1, True, False)
        '----------Added by Preeti Against ticket No [BM00000007859]

        txtDate.Value = clsCommon.GETSERVERDATE()
        txtDeliveryDate.Value = txtDate.Value
        txtQuotationDate.Value = txtDate.Value
        TxtBuyerPODate.Value = txtDate.Value
        txtDueDate.Value = txtDate.Value
        dtpRenewal.Value = txtDate.Value
        dtpExpiryDate.Value = txtDate.Value
        '-----------
        '---------- added parteek 16-03-2016 related direct ap when po type is Services
        UDLPurchaseOrderthroughAP = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UDLPurchaseOrderthroughAP, clsFixedParameterCode.UDLPurchaseOrderthroughAP, Nothing)) = 1, True, False)
        If UDLPurchaseOrderthroughAP = True Then
            txtBillNo.Visible = True
            dtBillDate.Visible = True
            lblBillNo.Visible = True
            lblBillDate.Visible = True
        End If
        '---------End---
        LOCATIONRIGTHS()
        RequiredPOLimit = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RequiredPOLimit, clsFixedParameterCode.RequiredPOLimit, Nothing)) = 1, True, False)

        'Parteek Create Setting for Payment Advance agaist ticket no.BHA/21/06/18-000066
        SendMailForAdvancePaymenTerms = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MailForAdvancePaymentTerm, clsFixedParameterCode.MailForAdvancePaymentTerm, Nothing)) = 1, True, False)
        txtReferencePO.Text = "/DMM//Stores/"
        'SANJAY
        If clsCommon.myLen(PurchaseOrderNo) > 0 Then
            LoadData(PurchaseOrderNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        'SANJAY
        'Parteek added these functionality only for UDL regarding Work Order
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            RadPageView1.Pages("RadPageViewPage6").Enabled = True
            RadPageView1.Pages("RadPageViewPage6").Item.Visibility = ElementVisibility.Visible

        Else
            RadPageView1.Pages("RadPageViewPage6").Item.Visibility = ElementVisibility.Hidden

        End If

        ShowItemInCaseofNonInventory = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowItemInCaseofNonInventory, clsFixedParameterCode.ShowItemInCaseofNonInventory, Nothing)) = "1", True, False))

        If ShowItemInCaseofNonInventory = True Then
            rmImport.Visibility = ElementVisibility.Visible
            rmExport.Visibility = ElementVisibility.Visible
        End If


        gvCategoryValue.Rows.AddNew()
        gvTermsCdtion.Rows.AddNew()
        ' End

        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.WorkOrderEng) = CompairStringResult.Equal Then
            RadLabel23.Text = "Estimation"
            MyLabel55.Visible = True
            txtRefTendorNo.Visible = True
            txtTenderNo.Visible = False
            MyLabel59.Visible = False
        Else
            MyLabel55.Visible = False
            txtRefTendorNo.Visible = False
            txtTenderNo.Visible = True
            MyLabel59.Visible = True
        End If
        If clsCommon.CompairString(objCommonVar.CurrentIndustryType, "D") <> CompairStringResult.Equal Then
            chkBlanket.Visible = False
        End If
        'If clsFixedParameter.GetData(clsFixedParameterCode.CmtSetting, clsFixedParameterType.CmtSetting, Nothing) = "1" Then
        '    Dim qry As String = "SELECT TOP 1 Comments,Subject,Content_Subject,* FROM TSPL_PURCHASE_ORDER_HEAD ORDER BY PurchaseOrder_Date DESC"
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '    If dt.Rows.Count > 0 Then
        '        txtComment.Text = clsCommon.myCstr(dt.Rows(i)("Comments")) txtCmt1.Text = clsCommon.myCstr(dt.Rows(i)("Comment1")) txtCmt2.Text = clsCommon.myCstr(dt.Rows(i)("Comment2"))  txtCmt3.Text = clsCommon.myCstr(dt.Rows(i)("Comment3")) txtCmt4.Text = clsCommon.myCstr(dt.Rows(i)("Comment4")) txtCmt5.Text = clsCommon.myCstr(dt.Rows(i)("Comment5")) txtCmt6.Text = clsCommon.myCstr(dt.Rows(i)("Comment6"))  txtCmt7.Text = clsCommon.myCstr(dt.Rows(i)("Comment7")) txtCmt8.Text = clsCommon.myCstr(dt.Rows(i)("Comment8")) txtCmt9.Text = clsCommon.myCstr(dt.Rows(i)("Comment9")) txtCmt10.Text = clsCommon.myCstr(dt.Rows(i)("Comment10")) txtCmt11.Text = clsCommon.myCstr(dt.Rows(i)("Comment11")) txtCmt12.Text = clsCommon.myCstr(dt.Rows(i)("Comment12")) txtCmt13.Text = clsCommon.myCstr(dt.Rows(i)("Comment13"))'txtCmt14.Text = clsCommon.myCstr(dt.Rows(i)("Comment14")) txtSubject.Text = clsCommon.myCstr(dt.Rows(i)("Subject")) txtContentSubject.Text = clsCommon.myCstr(dt.Rows(i)("Content_Subject")) End If
        'Else
        '    txtFreight.Text = "" txtComment.Text = ""  txtCmt1.Text = ""  txtCmt2.Text = ""  txtCmt3.Text = "" txtCmt4.Text = ""  txtCmt5.Text = ""
        '     txtCmt6.Text = ""  txtCmt7.Text = ""  txtCmt8.Text = ""  txtCmt9.Text = ""  txtCmt10.Text = "" txtCmt11.Text = ""  txtCmt12.Text = ""
        '   txtCmt13.Text = ""  'txtCmt14.Text = "" 'RTComment.Text = ""  txtSubject.Text = ""  txtContentSubject.Text = ""  txtPaymentTerm.Text = ""
        '    txtInsuranceTerms.Text = "" txtPackingForward.Text = ""  txtInsurance.Text = ""
        '   End If
    End Sub

    Public Sub SetUserMgmtNew()
        ' 'MyBase.SetUserMgmt(clsUserMgtCode.mbtnPurchaseOrder)
        'MyBase.SetUserMgmt(FORMTYPE)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnForm_Update.Visible = False 'MyBase.isModifyFlag
        btnPrintNew.Visible = MyBase.isPrintFlag
        btnUnpost.Visible = False
        'If MyBase.isReverse Then
        '    btnUnpost.Enabled = True
        'Else
        '    btnUnpost.Enabled = False
        'End If
        If MyBase.isExport = True Then
            rmImport.Enabled = True
            rmExport.Enabled = True
        Else
            rmImport.Enabled = False
            rmExport.Enabled = False
        End If
    End Sub

    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                Dim check As Integer = 0
                check = clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where location_code='" + obj.Default_LocCode + "'")
                If check > 0 Then
                    txtBillToLocation.Value = obj.Default_LocCode
                    lblBillToLocation.Text = obj.Default_LocName
                End If
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub


    Sub LoadCategory()
        ddl_category.DataSource = Xtra.GetCapexCombo()
        ddl_category.ValueMember = "Code"
        ddl_category.DisplayMember = "Name"
        ddl_category.SelectedValue = ""
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

    Public Sub LoadItems()
        Try
            isInsideLoadData = True
            If ArrItem IsNot Nothing Then
                cboItemType.SelectedValue = "O"
                For Each obj As clsItemMaster In ArrItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                    '--------------richa 09/07/2014 Ticket No BM00000003045---------
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.RemainingQtyToPurchase
                    '-------------------------------------------------------
                    gv1.Rows.AddNew()
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub fndProject__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProject._MYValidating
        Dim qry As String = "select PROJECT_CODE as Code,SPECIFICATION,PROJECT_STATUS as Status from TSPL_PJC_PROJECT"
        fndProject.Value = clsCommon.ShowSelectForm("Project Code", qry, "Code", "", fndProject.Value, "", isButtonClicked)
        lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
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
            ''richa agarwal 24/12/2014
            'chkIsMerchantTrade.Enabled = True
            '---------------------
        Else
            ''richa agarwal 24/12/2014
            chkIsMerchantTrade.Enabled = False
            ''-------------------------
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
                    clsCommon.MyMessageBoxShow(Me, "Conversion rate not entered for currency '" & Me.txtCurrencyCode.Value & "'")
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
        txtRemarks.MaxLength = 200
        txtComment.MaxLength = 5000
        txtCmt1.MaxLength = 5000
        txtCmt2.MaxLength = 5000
        txtCmt3.MaxLength = 5000
        txtCmt4.MaxLength = 5000
        txtCmt5.MaxLength = 5000
        txtCmt6.MaxLength = 5000
        txtCmt7.MaxLength = 5000
        txtCmt8.MaxLength = 5000
        txtCmt9.MaxLength = 5000
        txtCmt10.MaxLength = 5000
        txtCmt11.MaxLength = 5000
        txtCmt12.MaxLength = 5000
        txtCmt13.MaxLength = 5000
        'txtCmt14.MaxLength = 5000
        'RTComment.MaxLength = 5000
        '' as per amit sir ticket no: BM00000005661
        cboModeOfTransport.MaxLength = 12
        cboPOType.MaxLength = 1
        cboItemType.MaxLength = 1


    End Sub

    Sub LoadPOType()
        cboPOType.DataSource = clsPurchaseOrderHead.LoadPurchaseType()
        cboPOType.ValueMember = "Code"
        cboPOType.DisplayMember = "Name"
    End Sub

    Public Shared Function GetItemall() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Z"
        dr("Name") = "All"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        Return dt
    End Function
    Sub LoadItemType()
        If ShowItemAllStructureWise = True Then
            cboItemType.DataSource = GetItemall()
            cboItemType.ValueMember = "Code"
            cboItemType.DisplayMember = "Name"
        Else
            cboItemType.DataSource = Nothing
        'cboItemType.DataSource = clsItemMaster.GetItemTypeWithNON_Inventory()
        Dim Whr = " AND ITEM_TYPE_CODE NOT IN('J') "
        cboItemType.DataSource = clsItemMaster.getItemTypeQuery(Whr)
        cboItemType.ValueMember = "Code"
            cboItemType.DisplayMember = "Name"
        End If
    End Sub

    Sub LoadModeOfTrasport()
        cboModeOfTransport.Items.Add("By Road")
        cboModeOfTransport.Items.Add("By Air")
        cboModeOfTransport.Items.Add("By Sea")
        cboModeOfTransport.Items.Add("By Train")
    End Sub

    Sub BlankAllControls()
        chkpoclose.Enabled = True
        btn_cancel.Visible = False
        btnPost.Visible = MyBase.isPostFlag
        txtBankCode.Value = ""
        lblBankDesc.Text = ""
        txtPaymentMode.Value = ""
        LoadBlankGridAC()
        LoadBlankGridACInsurance()
        txt_deliverydays.Text = 0
        lbl_capexcode.Text = ""
        lbl_capexsubcode.Text = ""
        fndcapexcode.Value = ""
        fndcapexsubcode.Value = ""
        lbl_budgetamt.Text = ""
        lbl_budgetamtwithtolerence.Text = ""
        lbl_rebudgetamt.Text = ""
        lbl_rebudgetamtwithtolerence.Text = ""
        lblAmtAfterTax.Text = ""
        MyLabel7.Text = ""
        txtKindAttentation.Text = ""
        Try
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterCode.CmtSetting, clsFixedParameterType.CmtSetting, Nothing)) = 1 Then
                Dim qry As String = "SELECT TOP 1 Comments,Subject,Content_Subject,Comment1,Comment2,Comment3,Comment4,Comment5,Comment6,Comment7,Comment8,Comment9,Comment10,Comment11,Comment12,Comment13,Subject,Content_Subject FROM TSPL_PURCHASE_ORDER_HEAD ORDER BY PurchaseOrder_Date DESC"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count > 0 Then
                    txtComment.Rtf = clsCommon.myCstr(dt.Rows(i)("Comments"))
                    txtCmt1.Rtf = clsCommon.myCstr(dt.Rows(i)("Comment1"))
                    txtCmt2.Rtf = clsCommon.myCstr(dt.Rows(i)("Comment2"))
                    txtCmt3.Rtf = clsCommon.myCstr(dt.Rows(i)("Comment3"))
                    txtCmt4.Rtf = clsCommon.myCstr(dt.Rows(i)("Comment4"))
                    txtCmt5.Rtf = clsCommon.myCstr(dt.Rows(i)("Comment5"))
                    txtCmt6.Rtf = clsCommon.myCstr(dt.Rows(i)("Comment6"))
                    txtCmt7.Rtf = clsCommon.myCstr(dt.Rows(i)("Comment7"))
                    txtCmt8.Rtf = clsCommon.myCstr(dt.Rows(i)("Comment8"))
                    txtCmt9.Rtf = clsCommon.myCstr(dt.Rows(i)("Comment9"))
                    txtCmt10.Rtf = clsCommon.myCstr(dt.Rows(i)("Comment10"))
                    txtCmt11.Rtf = clsCommon.myCstr(dt.Rows(i)("Comment11"))
                    txtCmt12.Rtf = clsCommon.myCstr(dt.Rows(i)("Comment12"))
                    txtCmt13.Rtf = clsCommon.myCstr(dt.Rows(i)("Comment13"))
                    'txtCmt14.Text = clsCommon.myCstr(dt.Rows(i)("Comment14"))
                    txtSubject.Text = clsCommon.myCstr(dt.Rows(i)("Subject"))
                    txtContentSubject.Text = clsCommon.myCstr(dt.Rows(i)("Content_Subject"))
                End If
            Else
                txtFreight.Text = ""
                txtComment.Rtf = ""
                txtCmt1.Rtf = ""
                txtCmt2.Rtf = ""
                txtCmt3.Rtf = ""
                txtCmt4.Rtf = ""
                txtCmt5.Rtf = ""
                txtCmt6.Rtf = ""
                txtCmt7.Rtf = ""
                txtCmt8.Rtf = ""
                txtCmt9.Rtf = ""
                txtCmt10.Rtf = ""
                txtCmt11.Rtf = ""
                txtCmt12.Rtf = ""
                txtCmt13.Rtf = ""
                'txtCmt14.Text = ""
                'RTComment.Text = ""
                txtSubject.Text = ""
                txtContentSubject.Text = ""
                txtPaymentTerm.Text = ""
                txtInsuranceTerms.Text = ""
                txtPackingForward.Text = ""
                txtInsurance.Text = ""
            End If
        Catch ex As Exception
        End Try

        txtDelivery_Code.Value = ""
        txtDeliveryDesc.Text = ""
        Chkroadpermit.Checked = False
        LoadBlankRoadPermitGrid()
        chk_c_form.Checked = False
        LoadBlankCFORMGrid()
        btnForm_Update.Enabled = False
        RadGroupBox3.Enabled = False
        RadGroupBox4.Enabled = False
        cboPOType.SelectedValue = ""
        chkOpenPO.Checked = False
        TxtBuyerPONo.Text = ""
        TxtBuyerPODate.Value = clsCommon.GETSERVERDATE(Nothing)
        txtDocNo.Value = ""
        txtDesc.Text = ""
        chkOnHold.Checked = False
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE(Nothing)
        '' Anubhooti 01-Nov-2014
        dtpExpiryDate.Value = clsCommon.GETSERVERDATE(Nothing).AddYears(1)
        txtHeaderDiscountAmount.Value = 0
        dblPreviousTDSAmt = 0
        txtBillToLocation.Value = ""
        lblBillToLocation.Text = ""
        txtShipToLocation.Value = ""
        lblShipToLocation.Text = ""
        txtDesc.Text = ""
        txtRemarks.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        txtTermCode.Value = ""
        txtTermRemark.Text = ""
        lblTermName.Text = ""
        txtDueDate.Value = txtDate.Value
        txtRefNo.Text = ""
        lblAmtWithDiscount.Text = ""
        lblDiscountAmt.Text = ""
        lblAmtAfterDiscount.Text = ""
        lblTaxAmt.Text = ""
        lblTotRAmt.Text = ""
        lblTotRAmtCopy.Text = ""
        cboModeOfTransport.Text = "By Road"
        chkTDSApplied.Checked = False
        chkTDSApplied.Tag = 0
        chkAgainst_RGP.Enabled = False
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDeliveryDate.Value = txtDate.Value
        txtDept.Value = ""
        lblDept.Text = ""
        cboItemType.SelectedIndex = 0
        txtReqNo.Value = ""
        cboItemType.Enabled = True
        txtBillToLocation.Enabled = True
        lblAmbendmentNoCaption.Visible = False
        lblAbandonmentNo.Text = ""
        fndState.Value = ""
        lblStateName.Text = ""
        chkMCCPurchase.Checked = False
        lblAddCharges.Text = ""
        lblAddCharges1.Text = ""
        lblAddChargesForInsurance.Text = ""
        lblAddChargesForInsurance1.Text = ""
        lblTotalInsuranceAmt.Text = ""
        rbtnTaxCalAutomatic.IsChecked = True
        chkExciseOnQty.Checked = False
        chkExciseOnQty.Enabled = True
        txtQuotationNo.Text = ""
        chkBlanket.Checked = False
        ChkISPO.Checked = False
        chkIsContent.Checked = False
        txtAmount.Text = ""
        vaddnew = "Y"
        chkpoclose.Checked = False
        txtQuotationDate.Value = txtDate.Value
        txtDeliveryDuration.Text = ""
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal Then
            lblDeliveryDate.Visible = False
            txtDeliveryDate.Visible = False
        Else
            Panel1.Visible = False
        End If
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        fndProject.Value = ""
        lblProject.Text = ""
        dtpExpiryDate.Checked = False

        LoadDefaultTermDetail()
        MyLabel7.Text = "Entered Purchase Order"

        dtpRenewal.Text = clsCommon.GETSERVERDATE(Nothing)
        dtpRenewal.Checked = False
        dtpRenewal.Enabled = True

        chkOpenPO.Enabled = True
        chkBlanket.Enabled = True
        cboPOType.Enabled = True

        ''richa agarwal 08/04/2015
        txtVendorNo.Enabled = True
        cboPOType.Enabled = True
        cboItemType.Enabled = True
        txtPINo.Enabled = True
        TxtBeneficiary.Enabled = True
        txtBillToLocation.Enabled = True
        TxtRetention.Text = ""


        ''------------------
        LoadStuffing()
        LoadAdvanceType()
        LoadTerms()
        LoadTerms_of_Payment()
        LoadPreCarriageBy()


        isInsideLoadDatamt = True
        ResetControlsonPaymentTerms()
        chkIsMerchantTrade.Checked = False
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.mbtnPurchaseOrder) = CompairStringResult.Equal OrElse clsCommon.CompairString(FORMTYPE, clsUserMgtCode.WorkOrderEng) = CompairStringResult.Equal Then
            chkIsMerchantTrade.Visible = False
            chkIsMerchantTrade.Checked = False
            RadPageView1.Pages("RdPaymentterms").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
        Else
            chkIsMerchantTrade.Visible = True
            chkIsMerchantTrade.Checked = True
            chkIsMerchantTrade.Enabled = False
            RadPageView1.Pages("RdPaymentterms").Item.Visibility = ElementVisibility.Visible
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        End If


        LoadWorkOrderValueGrid()
        LoadWorkOrderTermsGrid()

        lblWAddress.Text = ""
        lblWPhone.Text = ""
        lblWVendorName.Text = ""
        txtReferencePO.Text = "/DMM//Stores/"
        ''-----------------
        '--------------------------------------
    End Sub
    Sub LoadWorkOrderValueGrid()
        'gvCategoryValue.Rows.Clear()
        'gvCategoryValue.Columns.Clear()

        'Dim repoCName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoCName.FormatString = ""
        'repoCName.HeaderText = "Field Name"
        'repoCName.Name = colWOCategoy
        'repoCName.Width = 100
        'gvCategoryValue.MasterTemplate.Columns.Add(repoCName)

        'Dim repoCDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoCDesc.FormatString = ""
        'repoCDesc.HeaderText = "Description"
        'repoCDesc.Name = colWOCatDesc
        'repoCDesc.Width = 150
        'gvCategoryValue.MasterTemplate.Columns.Add(repoCDesc)

        'gvCategoryValue.AllowAddNewRow = False
        'gvCategoryValue.ShowGroupPanel = False
        'gvCategoryValue.AllowColumnReorder = True
        'gvCategoryValue.AllowRowReorder = False
        'gvCategoryValue.EnableSorting = False
        'gvCategoryValue.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        'gvCategoryValue.MasterTemplate.ShowRowHeaderColumn = False
        'gvCategoryValue.TableElement.TableHeaderHeight = 40
        LoadGridWork()
    End Sub
    Sub LoadGridWork()
        Try
            gvCategoryValue.DataSource = Nothing
            Dim qry As String = ""
            If IsLoadOk = True Then
                qry = "select '' as [Field Name],'' as [Field Desc] "
            Else
                qry = "select '' as [Field Name],'' as [Field Desc] "
                qry += " union all "
                qry += " select '' as [Field Name],'' as [Field Desc] "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gvCategoryValue.DataSource = dt
                gvCategoryValue.AllowAddNewRow = False
                gvCategoryValue.ShowGroupPanel = False
                gvCategoryValue.AllowColumnReorder = True
                gvCategoryValue.AllowRowReorder = False
                gvCategoryValue.EnableSorting = False
                gvCategoryValue.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
                gvCategoryValue.MasterTemplate.ShowRowHeaderColumn = False
                gvCategoryValue.TableElement.TableHeaderHeight = 40
                gvCategoryValue.BestFitColumns()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Sub LoadWorkOrderTermsGrid()
        gvTermsCdtion.Rows.Clear()
        gvTermsCdtion.Columns.Clear()

        Dim repoTerms As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTerms.FormatString = ""
        repoTerms.HeaderText = "Terms & Conditions"
        repoTerms.Name = colTerms
        repoTerms.Width = 300
        gvTermsCdtion.MasterTemplate.Columns.Add(repoTerms)


        gvTermsCdtion.AllowAddNewRow = True
        gvTermsCdtion.ShowGroupPanel = False
        gvTermsCdtion.AllowColumnReorder = True
        gvTermsCdtion.AllowRowReorder = False
        gvTermsCdtion.EnableSorting = False
        gvTermsCdtion.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvTermsCdtion.MasterTemplate.ShowRowHeaderColumn = False
        gvTermsCdtion.TableElement.TableHeaderHeight = 40

    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo) '0

        repoComplete = New GridViewTextBoxColumn()
        repoComplete.FormatString = ""
        repoComplete.HeaderText = "Complete"
        repoComplete.Width = 70
        repoComplete.Name = colComplete
        repoComplete.ReadOnly = True
        repoComplete.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoComplete) '1

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
        gv1.MasterTemplate.Columns.Add(repoRowType) '2


        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        ' If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboPOType.SelectedValue, "J") = CompairStringResult.Equal Then
        'repoICode.HeaderText = "Work Order Description "
        'Else
        repoICode.HeaderText = "Item Code"
        'End If
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode) '3

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoIName) '4

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

        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending Quantity"
        repoPendingQty.Name = colPendingQty
        repoPendingQty.IsVisible = False
        repoPendingQty.Minimum = 0
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPendingQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPendingQty) '6


        Dim repoOrgPeqQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrgPeqQty.WrapText = True
        repoOrgPeqQty.ReadOnly = True
        repoOrgPeqQty.FormatString = ""
        repoOrgPeqQty.HeaderText = "Requisition Quantity"
        repoOrgPeqQty.Name = colOrgRequitionQty
        repoOrgPeqQty.Width = 80
        repoOrgPeqQty.Minimum = 0
        repoOrgPeqQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgPeqQty)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "PO Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ShowUpDownButtons = False
        repoQty.Step = 0
        repoQty.DecimalPlaces = 3 ''UDL/27/06/18-000195 By balwinder on 28/06/2018 
        'If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmPurchaseOrderMT) = CompairStringResult.Equal Then
        '    repoQty.DecimalPlaces = 3
        'End If
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty) '8

        Dim repoQty_Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQty_Desc = New GridViewTextBoxColumn
        repoQty_Desc.FormatString = ""
        repoQty_Desc.HeaderText = "Quantity Description"
        repoQty_Desc.Name = colQty_Desc
        repoQty_Desc.Width = 100
        repoQty_Desc.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoQty_Desc)



        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 80
        repoUnit.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoUnit) '17

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Unit Cost"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.FormatString = "{0:n4}"
        repoRate.DecimalPlaces = 4
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate) '20

        Dim repoRate_Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRate_Desc = New GridViewTextBoxColumn
        repoRate_Desc.FormatString = ""
        repoRate_Desc.HeaderText = "Rate Description"
        repoRate_Desc.Name = colRate_Desc
        repoRate_Desc.Width = 100
        repoRate_Desc.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoRate_Desc)

        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Last Unit Cost(Same Vendor)"
        repoRate.Name = colLastRateSameVendor
        repoRate.Width = 80
        repoRate.ReadOnly = True
        repoRate.FormatString = "{0:n4}"
        repoRate.DecimalPlaces = 4
        repoRate.WrapText = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate) '20

        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Last Unit Cost(Other Vendor)"
        repoRate.Name = colLastRateOtherVendor
        repoRate.Width = 80
        repoRate.ReadOnly = True
        repoRate.FormatString = "{0:n4}"
        repoRate.DecimalPlaces = 4
        repoRate.WrapText = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate) '20

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
        gv1.MasterTemplate.Columns.Add(repoAmt) '21

        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Extended Cost"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = False
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt) '21

        Dim repoDisPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        Dim repoDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        Dim repoDisPerUnit As GridViewDecimalColumn = New GridViewDecimalColumn()
        Dim repoDisAmtPerUnit As GridViewDecimalColumn = New GridViewDecimalColumn()

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


        repoDisPerUnit = New GridViewDecimalColumn()
        repoDisPerUnit.FormatString = "{0:n2}"
        repoDisPerUnit.HeaderText = "Discount Per Unit"
        repoDisPerUnit.Minimum = 0
        repoDisPerUnit.Maximum = 100
        repoDisPerUnit.Name = colDisPerUnit
        repoDisPerUnit.Width = 80
        repoDisPerUnit.DecimalPlaces = 2
        repoDisPerUnit.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDisPerUnit)

        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = "{0:n2}"
        repoDisAmt.HeaderText = "Discount Amt UnitWise"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDisAmtPerUnit
        repoDisAmt.Width = 80
        repoDisAmt.DecimalPlaces = 2
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
        gv1.MasterTemplate.Columns.Add(repoDisPer) '22

        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDetailDisAmt
        repoDisAmt.Width = 80
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDP") = CompairStringResult.Equal Then
            repoDisAmt.ReadOnly = False
        Else
            repoDisAmt.ReadOnly = True
        End If
        gv1.MasterTemplate.Columns.Add(repoDisAmt) '23


        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Total Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDisAmt
        repoDisAmt.Width = 80
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDisAmt) '23

        Dim DecimalCol As GridViewDecimalColumn = New GridViewDecimalColumn()
        DecimalCol.FormatString = ""
        DecimalCol.HeaderText = "Amount After Discount"
        DecimalCol.Name = colAmtAfterDis
        DecimalCol.WrapText = True
        DecimalCol.Width = 80
        DecimalCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        DecimalCol.VisibleInColumnChooser = False
        DecimalCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(DecimalCol)

        DecimalCol = New GridViewDecimalColumn()
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


        DecimalCol = New GridViewDecimalColumn()
        DecimalCol.FormatString = ""
        DecimalCol.HeaderText = "Taxable Amount %"
        DecimalCol.Name = colTaxableAmountPer
        DecimalCol.WrapText = False
        DecimalCol.IsVisible = True
        DecimalCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        DecimalCol.VisibleInColumnChooser = False
        DecimalCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(DecimalCol)


        DecimalCol = New GridViewDecimalColumn()
        DecimalCol.FormatString = ""
        DecimalCol.HeaderText = "Taxable Amount"
        DecimalCol.Name = colTaxableAmount
        DecimalCol.WrapText = True
        DecimalCol.Width = 150
        DecimalCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        DecimalCol.VisibleInColumnChooser = False
        DecimalCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(DecimalCol)



        Dim repoAmount_Desc As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount_Desc = New GridViewDecimalColumn
        repoAmount_Desc.FormatString = ""
        repoAmount_Desc.HeaderText = "Amount Description"
        repoAmount_Desc.Name = colAmount_Desc
        repoAmount_Desc.Width = 100
        repoAmount_Desc.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAmount_Desc)


        Dim repoTax1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax1.FormatString = ""
        repoTax1.HeaderText = "Tax 1"
        repoTax1.Name = colTax1
        repoTax1.ReadOnly = True
        repoTax1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax1) '26

        Dim repoTaxBaseAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt1.FormatString = ""
        repoTaxBaseAmt1.HeaderText = "Tax Base Amount 1"
        repoTaxBaseAmt1.Name = colTaxBaseAmt1
        repoTaxBaseAmt1.ReadOnly = True
        repoTaxBaseAmt1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt1) '27

        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colTaxRate1
        repoTaxRate1.IsVisible = False
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate1) '28

        Dim repoTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1 = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.HeaderText = "Tax Amt 1"
        repoTaxAmt1.Name = colTaxAmt1
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt1) '29

        repoIsSurTax1 = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Surtax 1"
        repoIsSurTax1.Name = colIsSurTax1
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax1) '30

        Dim repoSurTaxCode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode1.FormatString = ""
        repoSurTaxCode1.HeaderText = "Surtax 1"
        repoSurTaxCode1.Name = colSurTaxCode1
        repoSurTaxCode1.ReadOnly = True
        repoSurTaxCode1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode1) '31

        Dim repoIsTaxable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Is Taxable 1"
        repoIsTaxable1.Name = colIsTaxable1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1) '32


        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 1"
        repoIsTaxable1.Name = colTaxOnBaseAmt1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1) '32




        Dim repoTax2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2.FormatString = ""
        repoTax2.HeaderText = "Tax 2"
        repoTax2.Name = colTax2
        repoTax2.ReadOnly = True
        repoTax2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax2) '34

        Dim repoTaxBaseAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt2.FormatString = ""
        repoTaxBaseAmt2.HeaderText = "Tax Base Amount 2"
        repoTaxBaseAmt2.Name = colTaxBaseAmt2
        repoTaxBaseAmt2.ReadOnly = True
        repoTaxBaseAmt2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt2) '35

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colTaxRate2
        repoTaxRate2.IsVisible = False
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate2) '36

        Dim repoTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2 = New GridViewDecimalColumn()
        repoTaxAmt2.FormatString = ""
        repoTaxAmt2.HeaderText = "Tax Amt 2"
        repoTaxAmt2.Name = colTaxAmt2
        repoTaxAmt2.IsVisible = False
        repoTaxAmt2.ReadOnly = True
        repoTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt2) '37

        Dim repoIsSurTax2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax2.HeaderText = "Is Surtax 2"
        repoIsSurTax2.Name = colIsSurTax2
        repoIsSurTax2.ReadOnly = True
        repoIsSurTax2.IsVisible = False
        repoIsSurTax2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax2) '38

        Dim repoSurTaxCode2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode2.FormatString = ""
        repoSurTaxCode2.HeaderText = "Surtax 2"
        repoSurTaxCode2.Name = colSurTaxCode2
        repoSurTaxCode2.ReadOnly = True
        repoSurTaxCode2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode2) '39

        Dim repoIsTaxable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable2.HeaderText = "Is Taxable 2"
        repoIsTaxable2.Name = colIsTaxable2
        repoIsTaxable2.ReadOnly = True
        repoIsTaxable2.IsVisible = False
        repoIsTaxable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable2) '40

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
        gv1.MasterTemplate.Columns.Add(repoTax3) '42

        Dim repoTaxBaseAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt3.FormatString = ""
        repoTaxBaseAmt3.HeaderText = "Tax Base Amount 3"
        repoTaxBaseAmt3.Name = colTaxBaseAmt3
        repoTaxBaseAmt3.ReadOnly = True
        repoTaxBaseAmt3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt3) '43

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colTaxRate3
        repoTaxRate3.IsVisible = False
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate3) '44

        Dim repoTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3 = New GridViewDecimalColumn()
        repoTaxAmt3.FormatString = ""
        repoTaxAmt3.HeaderText = "Tax Amt 3"
        repoTaxAmt3.Name = colTaxAmt3
        repoTaxAmt3.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt3) '45

        Dim repoIsSurTax3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax3.HeaderText = "Is Surtax 3"
        repoIsSurTax3.Name = colIsSurTax3
        repoIsSurTax3.ReadOnly = True
        repoIsSurTax3.IsVisible = False
        repoIsSurTax3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax3) '46

        Dim repoSurTaxCode3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode3.FormatString = ""
        repoSurTaxCode3.HeaderText = "Surtax 3"
        repoSurTaxCode3.Name = colSurTaxCode3
        repoSurTaxCode3.ReadOnly = True
        repoSurTaxCode3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode3) '47

        Dim repoIsTaxable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable3.HeaderText = "Is Taxable 3"
        repoIsTaxable3.Name = colIsTaxable3
        repoIsTaxable3.ReadOnly = True
        repoIsTaxable3.IsVisible = False
        repoIsTaxable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable3) '48



        Dim repoTax4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4.FormatString = ""
        repoTax4.HeaderText = "Tax 4"
        repoTax4.Name = colTax4
        repoTax4.ReadOnly = True
        repoTax4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax4) '50

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 3"
        repoIsTaxable1.Name = colTaxOnBaseAmt3
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoTaxBaseAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt4.FormatString = ""
        repoTaxBaseAmt4.HeaderText = "Tax Base Amount 4"
        repoTaxBaseAmt4.Name = colTaxBaseAmt4
        repoTaxBaseAmt4.ReadOnly = True
        repoTaxBaseAmt4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt4) '51

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colTaxRate4
        repoTaxRate4.IsVisible = False
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate4) '52

        Dim repoTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4 = New GridViewDecimalColumn()
        repoTaxAmt4.FormatString = ""
        repoTaxAmt4.HeaderText = "Tax Amt 4"
        repoTaxAmt4.Name = colTaxAmt4
        repoTaxAmt4.IsVisible = False
        repoTaxAmt4.ReadOnly = True
        repoTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt4) '53

        Dim repoIsSurTax4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax4.HeaderText = "Is Surtax 4"
        repoIsSurTax4.Name = colIsSurTax4
        repoIsSurTax4.ReadOnly = True
        repoIsSurTax4.IsVisible = False
        repoIsSurTax4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax4) '54

        Dim repoSurTaxCode4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode4.FormatString = ""
        repoSurTaxCode4.HeaderText = "Surtax 4"
        repoSurTaxCode4.Name = colSurTaxCode4
        repoSurTaxCode4.ReadOnly = True
        repoSurTaxCode4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode4) '55

        Dim repoIsTaxable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable4.HeaderText = "Is Taxable 4"
        repoIsTaxable4.Name = colIsTaxable4
        repoIsTaxable4.ReadOnly = True
        repoIsTaxable4.IsVisible = False
        repoIsTaxable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable4) '56

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
        gv1.MasterTemplate.Columns.Add(repoTax5) '58

        Dim repoTaxBaseAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt5.FormatString = ""
        repoTaxBaseAmt5.HeaderText = "Tax Base Amount 5"
        repoTaxBaseAmt5.Name = colTaxBaseAmt5
        repoTaxBaseAmt5.ReadOnly = True
        repoTaxBaseAmt5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt5) '59

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colTaxRate5
        repoTaxRate5.IsVisible = False
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate5) '60

        Dim repoTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5 = New GridViewDecimalColumn()
        repoTaxAmt5.FormatString = ""
        repoTaxAmt5.HeaderText = "Tax Amt 5"
        repoTaxAmt5.Name = colTaxAmt5
        repoTaxAmt5.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt5) '61

        Dim repoIsSurTax5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax5.HeaderText = "Is Surtax 5"
        repoIsSurTax5.Name = colIsSurTax5
        repoIsSurTax5.ReadOnly = True
        repoIsSurTax5.IsVisible = False
        repoIsSurTax5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax5) '62

        Dim repoSurTaxCode5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode5.FormatString = ""
        repoSurTaxCode5.HeaderText = "Surtax 5"
        repoSurTaxCode5.Name = colSurTaxCode5
        repoSurTaxCode5.ReadOnly = True
        repoSurTaxCode5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode5) '63

        Dim repoIsTaxable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable5.HeaderText = "Is Taxable 5"
        repoIsTaxable5.Name = colIsTaxable5
        repoIsTaxable5.ReadOnly = True
        repoIsTaxable5.IsVisible = False
        repoIsTaxable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable5) '64

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
        gv1.MasterTemplate.Columns.Add(repoTax6) '66

        Dim repoTaxBaseAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt6.FormatString = ""
        repoTaxBaseAmt6.HeaderText = "Tax Base Amount 6"
        repoTaxBaseAmt6.Name = colTaxBaseAmt6
        repoTaxBaseAmt6.ReadOnly = True
        repoTaxBaseAmt6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt6) '67

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
        gv1.MasterTemplate.Columns.Add(repoTax7) '74

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
        gv1.MasterTemplate.Columns.Add(repoTax8) '82

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
        gv1.MasterTemplate.Columns.Add(repoTax9) '90

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
        gv1.MasterTemplate.Columns.Add(repoTax10) '98

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

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 10"
        repoIsTaxable1.Name = colTaxOnBaseAmt10
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)



        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt) '116

        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Included Tax Amount"
        repoAmtAfterTax.Name = colAmtAfterTax
        repoAmtAfterTax.WrapText = True
        repoAmtAfterTax.Width = 80
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterTax) '117

        Dim repoRequition As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRequition.FormatString = ""
        repoRequition.HeaderText = "Requition No"
        repoRequition.Name = colReqistionNo
        repoRequition.ReadOnly = True
        repoRequition.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition) '138

        Dim repoBinNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBinNo.FormatString = ""
        repoBinNo.HeaderText = "Bin No"
        repoBinNo.Name = colBinNo
        repoBinNo.ReadOnly = False
        repoBinNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoBinNo) '124

        Dim repoIsMRPMandatory As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsMRPMandatory.HeaderText = "Is MRP Mandatory"
        repoIsMRPMandatory.Name = colisMRPMandatory
        repoIsMRPMandatory.IsVisible = False
        repoIsMRPMandatory.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoIsMRPMandatory.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIsMRPMandatory) '122


        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.WrapText = True
        repoMRP.ReadOnly = False
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.Minimum = 0
        repoMRP.IsVisible = True
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmPurchaseOrderMT) = CompairStringResult.Equal Then
            repoMRP.IsVisible = False
        End If
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRP) '123

        ''Dim repoAssessable As GridViewDecimalColumn = New GridViewDecimalColumn()
        ''repoAssessable.WrapText = True
        ''repoAssessable.ReadOnly = True
        ''repoAssessable.FormatString = ""
        ''repoAssessable.HeaderText = "Assessable"
        ''repoAssessable.Name = colAssessableRate
        ''repoAssessable.Width = 80
        ''repoAssessable.Minimum = 0
        ''repoAssessable.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        ''gv1.MasterTemplate.Columns.Add(repoAssessable)

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

        Dim repoSpecification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSpecification = New GridViewTextBoxColumn()
        repoSpecification.FormatString = ""
        repoSpecification.HeaderText = "Specification"
        repoSpecification.Name = colSpecification
        repoSpecification.Width = 100
        gv1.MasterTemplate.Columns.Add(repoSpecification) '128

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRemarks) '129
        '======Sanjeet (UDL)22/12/2016=============
        Dim repocapacity As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocapacity.FormatString = ""
        repocapacity.HeaderText = "Capacity"
        repocapacity.Name = colCapacity
        repocapacity.Width = 100
        gv1.MasterTemplate.Columns.Add(repocapacity)

        Dim repomake As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomake.FormatString = ""
        repomake.HeaderText = "Make"
        repomake.Name = colMake
        repomake.Width = 100
        gv1.MasterTemplate.Columns.Add(repomake)

        Dim repomodel As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomodel.FormatString = ""
        repomodel.HeaderText = "Model"
        repomodel.Name = colModel
        repomodel.Width = 100
        gv1.MasterTemplate.Columns.Add(repomodel)
        '==================================================
        Dim repoIsUsedInGRN As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsUsedInGRN.HeaderText = "Is Item Used IN GRN "
        repoIsUsedInGRN.Name = colItemUsedINGRN
        repoIsUsedInGRN.IsVisible = False
        repoIsUsedInGRN.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoIsUsedInGRN.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIsUsedInGRN)

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

        Dim repoRGPNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRGPNO.FormatString = ""
        repoRGPNO.HeaderText = "RGP No"
        repoRGPNO.Name = colRGPNo
        repoRGPNO.Width = 150
        repoRGPNO.ReadOnly = True
        repoRGPNO.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoRGPNO) '121

        '' for abatenment SRN
        Dim repoAbatementRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAbatementRate.WrapText = True
        repoAbatementRate.ReadOnly = True
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
        gv1.MasterTemplate.Columns.Add(repoAssesableMRP) '142

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
        gv1.MasterTemplate.Columns.Add(repoTotalAssesableMRP) '143

        ''richa agarwal 07-04-2015
        Dim repoFatperMT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatperMT.WrapText = True
        repoFatperMT.ReadOnly = True
        repoFatperMT.FormatString = "{0:n3}"
        repoFatperMT.Width = 150
        repoFatperMT.HeaderText = "Fat %"
        repoFatperMT.Name = colFatPerMT
        repoFatperMT.IsVisible = chkIsMerchantTrade.Checked
        repoFatperMT.Minimum = 0
        repoFatperMT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFatperMT)

        Dim repoSNFPerMT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFPerMT.WrapText = True
        repoSNFPerMT.ReadOnly = True
        repoSNFPerMT.FormatString = "{0:n3}"
        repoSNFPerMT.Width = 150
        repoSNFPerMT.HeaderText = "SNF %"
        repoSNFPerMT.Name = colSNFFPerMT
        repoSNFPerMT.IsVisible = chkIsMerchantTrade.Checked
        repoSNFPerMT.Minimum = 0
        repoSNFPerMT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFPerMT)

        Dim repoFatKGMT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatKGMT.WrapText = True
        repoFatKGMT.ReadOnly = True
        repoFatKGMT.FormatString = "{0:n3}"
        repoFatKGMT.Width = 150
        repoFatKGMT.HeaderText = "Fat KG"
        repoFatKGMT.Name = colFatKGMT
        repoFatKGMT.IsVisible = chkIsMerchantTrade.Checked
        repoFatKGMT.Minimum = 0
        repoFatKGMT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFatKGMT)

        Dim repoSNFKGMT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFKGMT.WrapText = True
        repoSNFKGMT.ReadOnly = True
        repoSNFKGMT.FormatString = "{0:n3}"
        repoSNFKGMT.Width = 150
        repoSNFKGMT.HeaderText = "SNF KG"
        repoSNFKGMT.Name = colSNFKGMT
        repoSNFKGMT.IsVisible = chkIsMerchantTrade.Checked
        repoSNFKGMT.Minimum = 0
        repoSNFKGMT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFKGMT)

        Dim repoItemWeightMT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Item Weight"
        repoItemWeightMT.Name = colItemWeightMT
        repoItemWeightMT.IsVisible = chkIsMerchantTrade.Checked
        repoItemWeightMT.Minimum = 0
        repoItemWeightMT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        Dim repoWeightUOMMT As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Weight UOM"
        repoWeightUOMMT.Name = colWeightUOMMT
        repoWeightUOMMT.Width = 150
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.IsVisible = chkIsMerchantTrade.Checked
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        ''-----------------------

        ''-----------------------

        '' Anubhooti 21-Aug-2014
        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Show"
        ShowBtn.HeaderText = "Image"
        ShowBtn.Name = colItemImage
        ShowBtn.Width = 70
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(ShowBtn) '144

        ''============19/10/2016--------------additional charge columns============================
        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code1"
        repoWeightUOMMT.Name = colItemACCode1
        repoWeightUOMMT.Width = 150
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
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

        'Sanjay

        Dim repoPendingPONumber As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPendingPONumber = New GridViewTextBoxColumn
        repoPendingPONumber.FormatString = ""
        repoPendingPONumber.HeaderText = "Prev PO NO"
        repoPendingPONumber.Name = colPrevPONO
        repoPendingPONumber.Width = 100
        repoPendingPONumber.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoPendingPONumber)

        Dim repoPendingQtyforGRN As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQtyforGRN = New GridViewDecimalColumn()
        repoPendingQtyforGRN.FormatString = ""
        repoPendingQtyforGRN.HeaderText = "Prev PO Qty"
        repoPendingQtyforGRN.Name = colPrevPOQty
        repoPendingQtyforGRN.IsVisible = False
        repoPendingQtyforGRN.Minimum = 0
        repoPendingQtyforGRN.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPendingQtyforGRN.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPendingQtyforGRN)

        ''==============================================================================================


        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()
        gv1.AutoSizeRows = False

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False

    End Sub

    Private Sub gv1_CellClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellClick
        Try
            isCellValueChangedOpen = False
            If e.Column Is gv1.Columns(colItemImage) Then
                If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                    Dim objImage As New frmPicture
                    If objImage.GetImage("tspl_Item_master", "Item_Image", "Item_Code", gv1.CurrentRow.Cells(colICode).Value.ToString) Then
                        objImage.ShowDialog()
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True

                    If e.Column Is gv1.Columns(colTotTaxAmt) Then
                        UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                        UpdateAllTotals()
                    End If
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If

                    If e.Column Is gv1.Columns(colItemInsuranceAmt) OrElse e.Column Is gv1.Columns(colItemInsurancePer) OrElse e.Column Is gv1.Columns(colInsurancePer) OrElse e.Column Is gv1.Columns(colDetailDisAmt) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colTotTaxAmt) OrElse e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colSpecification) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colDisPerUnit) OrElse (e.Column Is gv1.Columns(colDisPer) OrElse (e.Column Is gv1.Columns(colAmt)) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal) OrElse (e.Column Is gv1.Columns(colAmt)) AndAlso (clsCommon.CompairString(Me.cboPOType.SelectedValue, "J") = CompairStringResult.Equal And clsCommon.CompairString(Me.cboItemType.SelectedValue, "N") = CompairStringResult.Equal And chkAutoCalculate.Checked = False) Then
                        If (e.Column Is gv1.Columns(colQty) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colReqistionNo).Value) > 0) Then
                            Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPendingQty).Value)
                            Dim dblEnteredQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                            If dblEnteredQty > dblPendingQty Then
                                clsCommon.MyMessageBoxShow(Me, "Entered Quantity Can't be more than Pending Quantity." + Environment.NewLine + "Entered Quantity : " + clsCommon.myCstr(dblEnteredQty) + ". Pending Quantity : " + clsCommon.myCstr(dblPendingQty))
                                gv1.CurrentRow.Cells(colQty).Value = dblPendingQty
                            End If

                        ElseIf e.Column Is gv1.Columns(colICode) AndAlso clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal AndAlso ShowItemInCaseofNonInventory = True Then
                            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                                clsCommon.MyMessageBoxShow(Me, "Please select Vendor", Me.Text)
                                txtVendorNo.Focus()
                                Return
                            End If
                            OpenICodeList(False)
                            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(txtVendorNo.Value)) > 0 Then
                                gv1.CurrentRow.Cells(colPrevPOQty).Value = PendingQtyforGRN(clsCommon.myCstr(txtVendorNo.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
                                gv1.CurrentRow.Cells(colPrevPONO).Value = PendingPONOforGRN(clsCommon.myCstr(txtVendorNo.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
                            End If
                        ElseIf e.Column Is gv1.Columns(colICode) AndAlso Not clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal Then
                            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                                clsCommon.MyMessageBoxShow(Me, "Please select Vendor")
                                txtVendorNo.Focus()
                                Return
                            End If
                            OpenICodeList(False)
                            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(txtVendorNo.Value)) > 0 Then
                                gv1.CurrentRow.Cells(colPrevPOQty).Value = PendingQtyforGRN(clsCommon.myCstr(txtVendorNo.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
                                gv1.CurrentRow.Cells(colPrevPONO).Value = PendingPONOforGRN(clsCommon.myCstr(txtVendorNo.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
                            End If
                        ElseIf e.Column Is gv1.Columns(colICode) AndAlso clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal Then
                            SetitemWiseTaxSetting(True, True)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            If Not clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal Then
                                OpenUOMList(False)

                                If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(txtVendorNo.Value)) > 0 Then
                                    gv1.CurrentRow.Cells(colPrevPOQty).Value = PendingQtyforGRN(clsCommon.myCstr(txtVendorNo.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
                                    gv1.CurrentRow.Cells(colPrevPONO).Value = PendingPONOforGRN(clsCommon.myCstr(txtVendorNo.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
                                End If
                            End If
                            If chkIsMerchantTrade.Checked Then
                                CalculateFatSNfForMT()
                            End If
                        ElseIf e.Column Is gv1.Columns(colQty) Then
                            If chkIsMerchantTrade.Checked Then
                                CalculateFatSNfForMT()
                            End If
                            ''-------------
                        ElseIf e.Column Is gv1.Columns(colDetailDisAmt) Then
                            gv1.CurrentRow.Cells(colDisPer).Value = IIf(clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmt).Value) = 0, 0, Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells(colDetailDisAmt).Value) * 100 / clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmt).Value), 8, MidpointRounding.AwayFromZero))
                        End If
                        UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                        UpdateAllTotals()

                        If rbtnTaxCalManual.IsChecked OrElse Not chkGSTRegistered.Checked Then
                            For ii As Integer = 0 To gv1.Rows.Count - 1
                                UpdateCurrentRow(ii)
                            Next
                            UpdateAllTotals()
                        End If

                        '' Some update in case when Non-Iventry case in UDL Settting Based
                    ElseIf e.Column Is gv1.Columns(colRowType) Then
                        If ShowItemInCaseofNonInventory = True Then
                            ItemDescRaadonly(False)
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
                If clsCommon.myLen(gv1.CurrentRow.Cells(colReqistionNo).Value) > 0 Then
                    gv1.CurrentRow.Cells(colICode).ReadOnly = True
                Else
                    gv1.CurrentRow.Cells(colICode).ReadOnly = False
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Public Function PendingQtyforGRN(ByVal strVendor As String, ByVal strICode As String, ByVal strIUOM As String) As Double
        Dim qry As String = ""
        Try
            If clsCommon.myLen(strICode) > 0 Then
                qry = "SELECT SUM(qty * RI) as Balance from(  " &
                 " Select TSPL_PURCHASE_ORDER_DETAIL.PURCHASEORDER_NO " &
                 " ,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,1 as RI from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No  " &
                  " where TSPL_PURCHASE_ORDER_HEAD.Vendor_Code='" & strVendor & "'" &
                  " and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" & strICode & "'" &
                 " and TSPL_PURCHASE_ORDER_DETAIL.Unit_code='" & strIUOM & "'" &
                  " AND TSPL_PURCHASE_ORDER_HEAD.Status=1 " &
                   " union all " &
                 " select TSPL_GRN_DETAIL.PO_ID as PurchaseOrder_No " &
                 " ,TSPL_GRN_DETAIL.GRN_Qty  Qty ,-1 as RI  " &
                " from TSPL_GRN_DETAIL  " &
                " left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No  " &
                " where TSPL_GRN_HEAD.Vendor_Code='" & strVendor & "'" &
                 " and TSPL_GRN_DETAIL.Item_Code='" & strICode & "'" &
                 " and TSPL_GRN_DETAIL.Unit_code='" & strIUOM & "'" &
                 " AND TSPL_GRN_HEAD.Status=1 " &
                " )x "
                '" group by PurchaseOrder_No " & _
                '" having sum(Qty*RI)>0"
                Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Function PendingPONOforGRN(ByVal strVendor As String, ByVal strICode As String, ByVal strIUOM As String) As String
        Dim qry As String = ""
        Try
            If clsCommon.myLen(strICode) > 0 Then
                qry = "select stuff((select ',' + (Final.PurchaseOrder_No) FROM (SELECT PurchaseOrder_No as PurchaseOrder_No from(  " &
                 " Select TSPL_PURCHASE_ORDER_DETAIL.PURCHASEORDER_NO " &
                 " ,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,1 as RI from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No  " &
                  " where TSPL_PURCHASE_ORDER_HEAD.Vendor_Code='" & strVendor & "'" &
                  " and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" & strICode & "'" &
                 " and TSPL_PURCHASE_ORDER_DETAIL.Unit_code='" & strIUOM & "'" &
                 " AND TSPL_PURCHASE_ORDER_HEAD.Status=1 " &
                   " union all " &
                 " select TSPL_GRN_DETAIL.PO_ID as PurchaseOrder_No " &
                 " ,TSPL_GRN_DETAIL.GRN_Qty  Qty ,-1 as RI  " &
                " from TSPL_GRN_DETAIL  " &
                " left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No  " &
                " where TSPL_GRN_HEAD.Vendor_Code='" & strVendor & "'" &
                 " and TSPL_GRN_DETAIL.Item_Code='" & strICode & "'" &
                 " and TSPL_GRN_DETAIL.Unit_code='" & strIUOM & "'" &
                 " AND TSPL_GRN_HEAD.Status=1 " &
                " )x " &
                 " group by PurchaseOrder_No " &
                 " having sum(Qty*RI)>0  )Final for xml path ('')),1,1,'' )as PO_NO "
                Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function


    Sub ItemDescRaadonly(ByVal isButtonClick As Boolean)
        If clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value) <> "Item" Then
            gv1.CurrentRow.Cells(colIName).ReadOnly = False
        Else
            gv1.CurrentRow.Cells(colIName).ReadOnly = True
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
            End If
        End If
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim ItemType As String = Nothing

        If ShowItemInCaseofNonInventory = True Then
            If clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboPOType.SelectedValue, "J") = CompairStringResult.Equal Then
                ItemType = "O"
            Else
                ItemType = cboItemType.SelectedValue
            End If
        Else
            ItemType = cboItemType.SelectedValue
        End If

        Dim strItemType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value)
        If clsCommon.myLen(strItemType) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Row Type", Me.Text)
            Exit Sub
        End If

        If clsCommon.CompairString(strItemType, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
            If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Item Type", Me.Text)
                SetBlankOfItemColumns()
                cboItemType.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtTenderNo.Value) > 0 AndAlso (clsCommon.myCDecimal(txtTenderNo.Tag) = 2 OrElse clsCommon.myCDecimal(txtTenderNo.Tag) = 3) Then
                Dim obj As clsTenderDetail = clsTenderDetail.GetFinder(txtTenderNo.Value, txtVendorNo.Value, txtBillToLocation.Value)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                    gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
                    gv1.CurrentRow.Cells(colIName).Value = obj.Item_Name
                    gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    gv1.CurrentRow.Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                    gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_code
                    gv1.CurrentRow.Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(obj.Item_Code)
                    gv1.CurrentRow.Cells(colRate).Value = obj.Rate
                    gv1.CurrentRow.Cells(colDisPer).Value = obj.Discount
                Else
                    SetBlankOfItemColumns()
                End If
            ElseIf isItemfromVendorItemDetails Then
                If clsCommon.myLen(txtVendorNo.Value) > 0 AndAlso clsCommon.myLen(txtBillToLocation.Value) <= 0 AndAlso Not chkMCCPurchase.Checked Then
                    clsCommon.MyMessageBoxShow(Me, "Please Select Bill To Location", Me.Text)
                    txtBillToLocation.Focus()
                    txtBillToLocation.Select()
                    Return
                End If
                If clsCommon.myLen(txtVendorNo.Value) > 0 AndAlso clsCommon.myLen(fndState.Value) <= 0 AndAlso chkMCCPurchase.Checked Then
                    clsCommon.MyMessageBoxShow(Me, "Please Select State", Me.Text)
                    fndState.Focus()
                    fndState.Select()
                    Return
                End If
                Dim obj As clsVendorItemDetail = clsVendorItemDetail.Finder(txtVendorNo.Value, txtDate.Value, txtBillToLocation.Value, chkMCCPurchase.Checked, ItemType)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.item_code) > 0 Then
                    gv1.CurrentRow.Cells(colICode).Value = obj.item_code
                    gv1.CurrentRow.Cells(colIName).Value = obj.item_desc
                    gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.item_code, Nothing)
                    gv1.CurrentRow.Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.item_code, Nothing)
                    gv1.CurrentRow.Cells(colUnit).Value = obj.UOM
                    gv1.CurrentRow.Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(obj.item_code)
                    gv1.CurrentRow.Cells(colMRP).Value = obj.MRP
                    gv1.CurrentRow.Cells(colBinNo).Value = obj.Bin_No
                    gv1.CurrentRow.Cells(colRate).Value = obj.item_rate
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
                '' richa agarwal 09/04/2015 show items whose not of fresh type
                Dim strItemTypeincaseofMT As String = String.Empty
                If chkIsMerchantTrade.Checked Then
                    strItemTypeincaseofMT = " isnull(Is_FreshItem,0)=0"
                Else
                    strItemTypeincaseofMT = ""
                End If
                Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(ItemType), True, isButtonClick, txtVendorNo.Value, "", strItemTypeincaseofMT)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                    gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
                    gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
                    gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    gv1.CurrentRow.Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                    gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
                    gv1.CurrentRow.Cells(colisMRPMandatory).Value = obj.Is_MRP
                    ''richa Ticket No. BM00000003197 on 24/07/2014
                    gv1.CurrentRow.Cells(colBinNo).Value = obj.Rack_No
                    '' Anubhooti 21-Aug-2014 
                    'Dim Item_Image As Byte() = clsDBFuncationality.getSingleValue("Select isnull(Item_Image,0x) As Item_Image From TSPL_Item_Master Where Item_Code='" + obj.Item_Code + "'")
                    '------------------------------------------------
                Else
                    SetBlankOfItemColumns()
                End If
                ''End If

                Dim objVItem As clsVendorItemDetail = clsVendorItemDetail.GetItemRateAndMRP(txtVendorNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
                If objVItem IsNot Nothing Then
                    gv1.CurrentRow.Cells(colRate).Value = objVItem.item_rate
                    gv1.CurrentRow.Cells(colMRP).Value = objVItem.MRP
                End If
            End If
        Else
            ''For Open Misc Charges 
            Dim obj As clsAdditionalCharge = clsAdditionalCharge.GetFinder(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), isButtonClick)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                gv1.CurrentRow.Cells(colICode).Value = obj.Code
                gv1.CurrentRow.Cells(colIName).Value = obj.desc
                gv1.CurrentRow.Cells(colHSNNo).Value = obj.SACCode
                gv1.CurrentRow.Cells(colIsInsurance).Value = obj.Is_Insurance
                gv1.CurrentRow.Cells(colUnit).Value = Nothing
                gv1.CurrentRow.Cells(colQty).Value = Nothing
                gv1.CurrentRow.Cells(colRate).Value = Nothing
                gv1.CurrentRow.Cells(colisMRPMandatory).Value = False
                'gv1.CurrentRow.Cells(colAmt).Value = gv1.CurrentRow.Cells(colQty).Value * gv1.CurrentRow.Cells(colRate).Value
            Else
                SetBlankOfItemColumns()
            End If
            ''End of Misc Charges 

        End If

        GetLastRateItemWise(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), gv1.CurrentRow.Index)

        SetitemWiseTaxSetting(True, True)
        setBalance()
        ''richa agarwal 07/04/2015
        If chkIsMerchantTrade.Checked Then
            CalculateFatSNfForMT()
        End If
        ''-------------
        'SetVendorItemCostDetail()

    End Sub

    ''richa agarwal 07/04/2015
    Sub CalculateFatSNfForMT()
        Dim fatper As Double = 0
        Dim snfper As Double = 0
        Dim fatKG As Double = 0
        Dim snfKG As Double = 0
        For i As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(i).Cells(colICode).Value) > 0 Then
                fatper = clsBOM.GetFAT_PERS(clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value))
                snfper = clsBOM.GetSNF_PERS(clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value))
                fatKG = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(i).Cells(colUnit).Value), clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value), fatper, Nothing)
                snfKG = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(i).Cells(colUnit).Value), clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value), snfper, Nothing)
                gv1.Rows(i).Cells(colFatPerMT).Value = fatper
                gv1.Rows(i).Cells(colSNFFPerMT).Value = snfper
                gv1.Rows(i).Cells(colFatKGMT).Value = fatKG
                gv1.Rows(i).Cells(colSNFKGMT).Value = snfKG
            End If
        Next
    End Sub
    ''-------------- RICHA AGARWAL KDI/12/02/19-000450 12 fEB,2019
    Private Sub GetLastRateItemWise(ByVal ItemCode As String, ByVal intRow As Integer)
        Dim samevndrrate As Double = 0
        Dim othervndrrate As Double = 0
        If ShowLastUnitCostZeroForNonInventoryItemOnPO = True AndAlso clsCommon.CompairString(cboPOType.SelectedValue, "J") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal Then
            samevndrrate = 0
            othervndrrate = 0
        Else
            samevndrrate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Item_Cost from (select top 1 TSPL_PURCHASE_ORDER_DETAIL.Item_Cost,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No left outer join TSPL_UNIT_MASTER on TSPL_PURCHASE_ORDER_DETAIL.Unit_code=TSPL_UNIT_MASTER.Unit_Code where convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(clsCommon.myCDate(txtDate.Text), "dd/MMM/yyyy") + "',103) and TSPL_PURCHASE_ORDER_HEAD.Vendor_Code='" + clsCommon.myCstr(txtVendorNo.Value) + "' and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + clsCommon.myCstr(gv1.Rows(intRow).Cells(colICode).Value) + "' and TSPL_PURCHASE_ORDER_HEAD.purchaseorder_no<>'" + clsCommon.myCstr(txtDocNo.Value) + "' order by TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date desc,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No desc)axa"))
            othervndrrate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select a.item_cost from (select top 1 TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No left outer join TSPL_UNIT_MASTER on TSPL_PURCHASE_ORDER_DETAIL.Unit_code=TSPL_UNIT_MASTER.Unit_Code where convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(clsCommon.myCDate(txtDate.Text), "dd/MMM/yyyy") + "',103) and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + clsCommon.myCstr(gv1.Rows(intRow).Cells(colICode).Value) + "' and TSPL_PURCHASE_ORDER_HEAD.Vendor_Code<>'" + clsCommon.myCstr(txtVendorNo.Value) + "' order by TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date desc,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No desc)a "))
        End If
        gv1.Rows(intRow).Cells(colLastRateSameVendor).Value = samevndrrate
        gv1.Rows(intRow).Cells(colLastRateOtherVendor).Value = othervndrrate
    End Sub

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colUnit).Value = ""
        gv1.CurrentRow.Cells(colMRP).Value = 0
        gv1.CurrentRow.Cells(colLastRateOtherVendor).Value = 0
        gv1.CurrentRow.Cells(colLastRateSameVendor).Value = 0
        gv1.CurrentRow.Cells(colisMRPMandatory).Value = False
        gv1.CurrentRow.Cells(colHSNNo).Value = ""
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

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblAmt As Double = dblQty * dblRate

        If ShowItemInCaseofNonInventory = False Then
            If (clsCommon.CompairString(Me.cboPOType.SelectedValue, "J") = CompairStringResult.Equal And clsCommon.CompairString(Me.cboItemType.SelectedValue, "N") = CompairStringResult.Equal And chkAutoCalculate.Checked = False) Then
                dblAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmt).Value)
            Else
                dblAmt = dblQty * dblRate
            End If
        End If


        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
            gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        ElseIf clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(colIsInsurance).Value) Then
            dblAmt = (clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInsuranceBaseAmt).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInsurancePer).Value)) / 100
            gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        Else
            If ShowItemInCaseofNonInventory = True Then
                If (clsCommon.CompairString(Me.cboPOType.SelectedValue, "J") = CompairStringResult.Equal And clsCommon.CompairString(Me.cboItemType.SelectedValue, "N")) = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) <= 0 Then
                        dblAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmt).Value)
                    Else
                        gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
                    End If

                Else
                    dblAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmt).Value)
                End If
            Else
                dblAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmt).Value)
            End If

        End If

        Dim dblHeaderDisAmt As Decimal = 0

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
        If txtHeaderDiscountAmount.Value > 0 Then
            dblHeaderDisAmt = Math.Round(clsCommon.myCDivide(txtHeaderDiscountAmount.Value * dblAmt, dblTotAmt), 2, MidpointRounding.AwayFromZero)
        End If
        Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPer).Value)
        Dim dbldisperunit As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPerUnit).Value)
        Dim dbldisamtperunit As Decimal = (dblQty * dbldisperunit)
        Dim dblDetailDisAmt As Decimal = (dblAmt * dblDisPer) / 100
        Dim dblDisAmt As Decimal = dblDetailDisAmt + dblHeaderDisAmt + dbldisamtperunit
        Dim dblAmtAfterDis As Double = dblAmt - dblDisAmt

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

        Dim dblCurrentTaxableAmount As Decimal = 0
        If chkGSTRegistered.Checked Then
            dblCurrentTaxableAmount = dclItemAmtAfterInsurance
        Else
            Dim dblTotalTaxableAmount As Decimal = clsCommon.myCdbl(lblAmtAfterDiscount.Text) - GSTExemptedAmount
            If dblTotalTaxableAmount <= 0 Then
                dblCurrentTaxableAmount = 0
            Else
                If clsCommon.myCdbl(lblAmtAfterDiscount.Text) = 0 Then
                    dblCurrentTaxableAmount = 0
                Else
                    dblCurrentTaxableAmount = dblTotalTaxableAmount * dclItemAmtAfterInsurance / clsCommon.myCdbl(lblAmtAfterDiscount.Text)
                End If
            End If
        End If
        Dim dblCurrentTaxablePer As Decimal = clsCommon.myCDivide(dblCurrentTaxableAmount * 100, dclItemAmtAfterInsurance)


        '' abatement PO
        If IsAbatementPO Then
            gv1.Rows(IntRowNo).Cells(colAssesableMRP).Value = gv1.Rows(IntRowNo).Cells(colMRP).Value - (gv1.Rows(IntRowNo).Cells(colMRP).Value * gv1.Rows(IntRowNo).Cells(colAbatementRate).Value / 100)
            gv1.Rows(IntRowNo).Cells(colTotalAssesableMRP).Value = gv1.Rows(IntRowNo).Cells(colQty).Value * gv1.Rows(IntRowNo).Cells(colAssesableMRP).Value
        End If

        For ii As Integer = 1 To 10
            Dim Strii As String = clsCommon.myCstr(ii)

            If rbtnTaxCalAutomatic.IsChecked Then
                Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                If clsCommon.myLen(strTaxCode) > 0 AndAlso (gv1.Rows(IntRowNo).Cells(colItemTaxable).Value OrElse clsCommon.CompairString(gv1.Rows(IntRowNo).Cells(colRowType).Value, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal) Then
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
        gv1.Rows(IntRowNo).Cells(colHeaderDiscountPer).Value = Math.Round(clsCommon.myCDivide(dblHeaderDisAmt * 100, dblAmt), 10)
        gv1.Rows(IntRowNo).Cells(colDisAmtPerUnit).Value = Math.Round(dbldisamtperunit, 2)
        gv1.Rows(IntRowNo).Cells(colDetailDisAmt).Value = Math.Round(dblDetailDisAmt, 2)
        gv1.Rows(IntRowNo).Cells(colDisAmt).Value = Math.Round(dblDisAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterDis).Value = Math.Round(dblAmtAfterDis, 2)

        gv1.Rows(IntRowNo).Cells(colItemInsuranceBaseAmt).Value = Math.Round(dclItemInsuranceBaseAmt, 2)
        gv1.Rows(IntRowNo).Cells(colItemInsuranceAmt).Value = Math.Round(dclItemInsuranceAmt, 2)
        gv1.Rows(IntRowNo).Cells(colItemAmtAfterInsurance).Value = Math.Round(dclItemAmtAfterInsurance, 2)

        gv1.Rows(IntRowNo).Cells(colTaxableAmount).Value = Math.Round(dblCurrentTaxableAmount, 2)
        gv1.Rows(IntRowNo).Cells(colTaxableAmountPer).Value = Math.Round(dblCurrentTaxablePer, 10)

        gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)

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
                'gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
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
                'gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
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

    Private Sub UpdateAllTotals()
        Dim isInsuranceExists As Boolean = False
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                    If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsInsurance).Value) Then
                        isInsuranceExists = True
                        Exit For
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
                            gv1.Rows(ii).Cells(colInsuranceBaseAmt).Value = dblTotalInsuranceBaseAmt
                            UpdateCurrentRow(ii)
                        End If
                    End If
                End If
            Next
        End If





        Dim dblTotAmt As Double = 0
        Dim dblTotDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0

        Dim dblTaxAssessableAmt As Double = 0

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
        Dim dblTotalQuantity As Double = Nothing
        Dim dblTaxableAmount As Double = Nothing
        Dim dblItemInsuranceAmt As Decimal = 0
        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                dblTotalQuantity = dblTotalQuantity + clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                dblTaxableAmount = dblTaxableAmount + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxableAmount).Value)
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisAmt).Value)
                dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
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

        lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt)
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblAmtAfterDis)
        lblTotalInsuranceAmt.Text = clsCommon.myFormat(dblItemInsuranceAmt)
        lblTaxableAmount.Text = clsCommon.myFormat(dblTaxableAmount)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
        lblAmtAfterTax.Text = clsCommon.myFormat(dblNetAmt)

        Dim dblACAmount As Double = 0
        For ii As Integer = 0 To gvAC.Rows.Count - 1
            If (clsCommon.myLen(gvAC.Rows(ii).Cells(colACCode).Value) > 0) Then
                If clsCommon.CompairString(clsCommon.myCstr(gvAC.Rows(ii).Cells(colACApplyOn).Value), clsCalculationlApplyON.RowTypeApplyOnPercent) = CompairStringResult.Equal Then
                    gvAC.Rows(ii).Cells(colACAmount).Value = Math.Round(clsCommon.myCdbl(lblAmtAfterTax.Text) * clsCommon.myCdbl(gvAC.Rows(ii).Cells(colACPer).Value) / 100, 2, MidpointRounding.ToEven)
                End If
                dblACAmount = dblACAmount + clsCommon.myCdbl(gvAC.Rows(ii).Cells(colACAmount).Value)
            End If
            '' abatement PO
            If IsAbatementPO Then
                gv1.CurrentRow.Cells(colAssesableMRP).Value = gv1.CurrentRow.Cells(colMRP).Value - (gv1.CurrentRow.Cells(colMRP).Value * gv1.CurrentRow.Cells(colAbatementRate).Value / 100)
                gv1.CurrentRow.Cells(colTotalAssesableMRP).Value = gv1.CurrentRow.Cells(colQty).Value * gv1.CurrentRow.Cells(colAssesableMRP).Value
            End If
        Next

        lblAddCharges.Text = clsCommon.myFormat(dblACAmount)
        lblAddCharges1.Text = clsCommon.myFormat(dblACAmount)

        dblNetAmt = dblNetAmt + dblACAmount

        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        lblTotRAmtCopy.Text = clsCommon.myFormat(dblNetAmt)

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
            clsCommon.MyMessageBoxShow(Me, ex.Message)
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
        MyLabel7.Text = "Entered Purchase Order"
    End Sub

    Sub AddNew()
        btnViewTDSDetails.Enabled = False
        isCellValueChangedOpen = False
        txtDeliveryDate.ReadOnly = False    ' Ticket Ref :  BM00000010416  Modified By : Prabhakar 
        chk_emergency.Enabled = True
        ddl_category.Enabled = True
        ddl_category.SelectedValue = ""
        chk_emergency.Checked = False
        lblConfirmatory_PO_SRN_No.Text = ""
        btnPost.Visible = MyBase.isPostFlag

        BlankAllControls()
        txtBillToLocation.Enabled = True
        fndProject.Enabled = True
        lblProject.Enabled = True
        'LoadBlankGrid()
        LoadBlankGridTax()
        'LoadBlankGridAC()
        isNewEntry = True
        btnCopy.Enabled = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnAmendment.Enabled = False
        btnDelete.Enabled = True
        txtDate.Focus()
        txtRefTendorNo.Text = ""
        txtTenderNo.Value = ""
        TxtRetention.ReadOnly = False
        txtTenderNo.Tag = Nothing
        lblVendorQuotationNo.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        dtBillDate.Value = clsCommon.GETSERVERDATE()
        txtBillNo.Text = ""
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        If clsFixedParameter.GetData(clsFixedParameterCode.DisableShipToLocation, clsFixedParameterType.DisableShipToLocation, Nothing) = "1" Then
            txtShipToLocation.Enabled = False
        Else
            txtShipToLocation.Enabled = True
        End If
        chkReceiveControl.Checked = False
        chkTender.Checked = False
        ' ''richa agarwal 08/04/2015
        'txtVendorNo.Enabled = True
        'cboPOType.Enabled = True
        'cboItemType.Enabled = True
        'txtPINo.Enabled = True
        'TxtBeneficiary.Enabled = True
        'txtBillToLocation.Enabled = True
        ' ''------------------
        'LoadStuffing()
        'LoadTerms()
        'LoadTerms_of_Payment()
        'LoadPreCarriageBy()
        'chkAcceptance.Enabled = True
        'dtpAcceptance.Enabled = True
        'chkPartshipment.Enabled = True
        'chkTransshipment.Enabled = True
        'cmbTerms.Enabled = True
        'cmbTerms_Payment.Enabled = True
        'cboStuffing.Enabled = True
        'txtPIDueDate.Enabled = True
        'FndCreditTerms.Enabled = True
        'TxtCreditTermsName.Enabled = True
        'txtPre_Carriage_By.Enabled = True
        'txtPort_Discharge.Enabled = True
        'txtFinal_Destination.Enabled = True
        'fndCountry_Final_Destination.Enabled = True
        'fndCountry_Origin.Enabled = True
        'txtCarrier.Enabled = True
        'chkAcceptance.Checked = False
        'dtpAcceptance.Value = clsCommon.GETSERVERDATE()
        'chkPartshipment.Checked = False
        'chkTransshipment.Checked = False
        'cmbTerms.SelectedValue = ""
        'cmbTerms_Payment.SelectedValue = ""
        'cboStuffing.SelectedValue = ""
        'txtPIDate.Value = clsCommon.GETSERVERDATE()
        'FndCreditTerms.Value = ""
        'TxtCreditTermsName.Text = ""
        'txtPre_Carriage_By.SelectedValue = ""
        'txtPort_Discharge.Text = ""
        'txtFinal_Destination.Text = ""
        'fndCountry_Final_Destination.Value = ""
        'fndCountry_Origin.Value = ""
        'txtCarrier.Text = ""
        'gv1.ReadOnly = False
        'chkIsMerchantTrade.Checked = False
        ''--------------------------------------
        LOCATIONRIGTHS()
        AllowDepartmentMandatoryOnPurchaseCycle()

        gvCategoryValue.Rows.AddNew()
        gvTermsCdtion.Rows.AddNew()
        txtSubLocation.Enabled = False
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        chkGSTRegistered.Checked = True
        lblTaxableAmount.Text = ""
        chkJobWorkOutward.Enabled = True
        chkJobWorkOutward.Checked = False
        chkRepair.Visible = False
        chkRepair.Checked = False
        UcItemBalance1.ItemCode = ""
        UcItemBalance1.RefreshData()
        FillVendorDetails()
    End Sub


    Function AllowToSave() As Boolean
        Try
            '= KUNAL > TICKET : BM00000009580 ==============================
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If

            If clsCommon.myLen(lblConfirmatory_PO_SRN_No.Text) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "This PO is Generated By Confirmatory PO in SRN No[" + lblConfirmatory_PO_SRN_No.Text + "]" + Environment.NewLine + "Cannot Edit/Modify/Amendment in this PO")
                Return False
            End If



            'If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.mbtnPurchaseOrder) = CompairStringResult.Equal Then
            '    clsLocationWiseTax.IsValidTaxGroup(txtTaxGroup.Value, txtBillToLocation.Value, txtVendorNo.Value, "P", txtDate.Value, Nothing)
            'End If
            'done by stuti on 17/10/2016 against ticket no - BM00000009608
            If clsCommon.CompairString(clsCommon.myCstr(btnSave.Text), "Save") = CompairStringResult.Equal Then
                If AllowBackDateEntry(txtDate.Value, Nothing) = False Then
                    txtDate.Focus()
                    Return False
                End If
            End If
            '=============end here===============
            If MyLabel7.Text = "System Generated Purchase Order" Then
                Return True
            End If
            RadPageView1.SelectedPage = RadPageViewPage1
            '' check for the Maximum order level including tolerance 
            Dim proceed As Boolean = False
            For Each dr As GridViewRowInfo In gv1.Rows
                If proceed = True Then
                    Exit For
                End If
                Dim balQty As Decimal = clsItemLocationDetails.getBalanceWithUnapprove(dr.Cells(colICode).Value, clsCommon.myCstr(Me.txtBillToLocation.Value), clsCommon.myCdbl(dr.Cells(colMRP).Value), dr.Cells(colUnit).Value, Me.txtDocNo.Value, txtDate.Value)
                Dim strqLevel As String
                Dim strlocation As String = Nothing
                strlocation = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_USER_MASTER.Default_Location from TSPL_USER_MASTER where TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "'", Nothing))
                strqLevel = "select Max_Level,(((Max_Level+Max_Level * Max_Level_Tollerence/100)*uom1.Conversion_Factor)/uom2.Conversion_Factor) tol_Plus,(Max_Level-Max_Level * Max_Level_Tollerence/100) tol_Minus " &
                       " from TSPL_ITEM_REORDER_LEVEL_NEW left outer join TSPL_ITEM_UOM_DETAIL uom1 on TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code = uom1.Item_Code and uom1.UOM_Code=(case when isnull(TSPL_ITEM_REORDER_LEVEL_NEW.UOM_Code,'')='' then uom1.UOM_Code else TSPL_ITEM_REORDER_LEVEL_NEW.Uom_Code end) " &
                       " left outer join TSPL_ITEM_UOM_DETAIL uom2 on TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code = uom2.Item_Code  where TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code='" & dr.Cells(colICode).Value & "'  and Max_Level<>0 and TSPL_ITEM_REORDER_LEVEL_NEW.LOCATION_CODE='" + strlocation + "' and uom2.Stocking_Unit='Y' and TSPL_ITEM_REORDER_LEVEL_NEW.Apply='Y'"
                Dim dtLevel As DataTable
                dtLevel = clsDBFuncationality.GetDataTable(strqLevel)
                If dtLevel.Rows.Count > 0 Then
                    If balQty >= dtLevel.Rows(0).Item("tol_Plus") Then
                        If clsCommon.MyMessageBoxShow(Me, "Balance of item " & dr.Cells(colICode).Value & "  on Location " & clsCommon.myCstr(Me.txtBillToLocation.Value) & " is reached maximum level of (" & balQty & "," & dtLevel.Rows(0).Item("tol_Plus") & "). Still do you want to proceed ?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                            proceed = False
                            RadPageView1.SelectedPage = RadPageViewPage1
                            Return False
                        Else
                            proceed = True
                        End If
                    End If
                End If
            Next

            'If btnSave.Text = "Update" Then
            '    Dim strchk As String = "select Status from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No='" + txtDocNo.Value + "'"
            '    Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
            '    If chkpost = "1" Then
            '        clsCommon.MyMessageBoxShow(Me,Me,"Transection already posted")
            '        Return False
            '    End If
            'End If
            Dim dt As DataTable
            '===========Added By Rohit on Aug 12,2015=======
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
            '==================================================
            '' check for PO With Requisition Setting
            Dim strq As String = "SELECT Description FROM TSPL_FIXED_PARAMETER where Type= 'CreatePOWithRequisition'"
            dt = clsDBFuncationality.GetDataTable(strq)
            If dt.Rows.Count = 0 Then
                '' nothing to do
            ElseIf clsCommon.CompairString(dt.Rows(0).Item("Description"), "0") = CompairStringResult.Equal Then
                ' nothing to do ; no validation on req no
            Else
                If clsCommon.myLen(clsCommon.myCstr(txtReqNo.Value)) = 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    clsCommon.MyMessageBoxShow(Me, "Reqisition No is mandatory.", Me.Text)
                    Me.txtReqNo.Focus()
                    Return False
                End If
            End If
            Try
                If clsERPFuncationality.GetGSTStatus(clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")) Then
                    chkGSTRegistered.Checked = clsVendorMaster.IsGSTRegisteredVendor(txtVendorNo.Value, Nothing)
                Else
                    chkGSTRegistered.Checked = True
                End If

                isCellValueChangedOpen = True
                CalculateInsuranceTotal(True)
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If PurchaseModulePickFixTaxRate AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        If Not clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal Then
                            gv1.CurrentRow = gv1.Rows(ii)
                            SetitemWiseTaxSetting(True, True)
                        End If
                    End If

                    UpdateCurrentRow(ii)
                Next
                UpdateAllTotals()
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                isCellValueChangedOpen = False
                Return False
            Finally
                isCellValueChangedOpen = False
            End Try


            Dim isTDSOverride As Boolean = False
            If objRemittance IsNot Nothing Then
                If objRemittance.IsTDSOverride Then
                    isTDSOverride = True
                End If
            End If
            If ShowMessageTDS Then
                If (clsCommon.MyMessageBoxShow(Me, "Do you want to Deduct TDS", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No) Then
                    objRemittance = Nothing
                Else
                    If Not isTDSOverride AndAlso objRemittance IsNot Nothing Then
                        SetVendorTDSDetails()

                    End If
                End If
            Else
                If Not isTDSOverride AndAlso objRemittance IsNot Nothing Then
                    SetVendorTDSDetails()
                End If
            End If
            If Not objRemittance Is Nothing Then
                UpdateTDSAmount()
            End If
            If objRemittance Is Nothing AndAlso objCommonVar.TDSValidationFrom IsNot Nothing Then
                If txtDate.Value >= objCommonVar.TDSValidationFrom Then
                    Dim AmountToCheckVendorOutstandingForTCSTax As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckVendorOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckVendorOutstandingForTCSTax, Nothing))
                    If AmountToCheckVendorOutstandingForTCSTax > 0 Then
                        Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsVendorMaster.GetVendorOutstandingForTCSTaxApplicableOnFY(txtVendorNo.Value, txtDate.Value))
                        If dblOutstandingAmount > AmountToCheckVendorOutstandingForTCSTax Then
                            clsCommon.MyMessageBoxShow(Me, "Outstanding Amount for Vendor [" + txtVendorNo.Value + "] Crossed TDS Limit.Please Apply TDS on Same.", Me.Text, MessageBoxButtons.OK)
                        End If
                    End If
                End If
            End If



            If Not chkMCCPurchase.Checked Then
                If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please select Bill to Location", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    txtBillToLocation.Focus()
                    txtBillToLocation.Select()
                    Return False
                End If
            ElseIf chkMCCPurchase.Checked Then
                If clsCommon.myLen(fndState.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please select State", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    fndState.Focus()
                    fndState.Select()
                    Return False
                End If
            End If
            ''richa agarwal 24/12/2014
            If Not chkIsMerchantTrade.Checked Then
                If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please select Tax Group", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    txtTaxGroup.Focus()
                    Return False
                End If
            End If
            ''richa agarwal 09/02/2015
            If chkIsMerchantTrade.Checked Then
                Dim locationType As String = String.Empty
                locationType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Location_Type,'')  from TSPL_LOCATION_MASTER where Location_Code='" & clsCommon.myCstr(txtBillToLocation.Value) & "' "))
                If clsCommon.CompairString(locationType, "Virtual") <> CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Bill to Location should be Virtual", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    txtBillToLocation.Focus()
                    Return False
                End If

            End If
            ''----------------
            ''-----------------------

            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Vendor")
                RadPageView1.SelectedPage = RadPageViewPage1
                txtVendorNo.Focus()
                Return False
            End If
            If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.WorkOrderEng) = CompairStringResult.Equal Then
                If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, WorkEstimation_Date,103) from TSPL_WORK_ESTIMATION_HEAD where WorkEstimation_Id ='" + txtReqNo.Value + "'")) > clsCommon.myCDate(txtDate.Value) Then
                    txtDate.Focus()
                    Throw New Exception("Date cannot be less than from Work Estimation Date")
                End If
            Else
                If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, Requisition_Date,103) from TSPL_REQUISITION_HEAD where Requisition_Id ='" + txtReqNo.Value + "'")) > clsCommon.myCDate(txtDate.Value) Then
                    txtDate.Focus()
                    Throw New Exception("Date cannot be less than from Requisition Date")
                End If
            End If
            If clsCommon.myLen(cboPOType.SelectedValue) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Purchase Order Type", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage1
                cboPOType.Focus()
                Return False
            End If
            If chkJobWorkOutward.Checked = True Then  ' clsCommon.myCstr(cboPOType.SelectedValue) = "O"
                If clsCommon.myLen(txtSubLocation.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please select Sub Location", Me.Text)
                    Return False
                End If
            End If


            ''If clsCommon.myLen(txtDept.Value) <= 0 Then
            ''    clsCommon.MyMessageBoxShow(Me,Me,"Please select Department")
            ''    txtDept.Focus()
            ''    Return False
            ''End If
            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Purchase Order code Not found to save", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage1
                cboPOType.Focus()
                Return False
            End If
            'If clsCommon.CompairString("R", cboItemType.SelectedValue) = CompairStringResult.Equal AndAlso Not (clsLocation.isLocatinExcisable(txtBillToLocation.Value)) Then
            '    clsCommon.MyMessageBoxShow(Me,Me,"Location should be Excisable for Raw Material")
            '    txtBillToLocation.Focus()
            '    Return False
            'End If
            If clsCommon.myLen(cboItemType.SelectedValue) <= 0 And Not clsCommon.CompairString(cboPOType.SelectedValue, "j") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "Please select Item Type", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage1
                cboItemType.Focus()
                Return False
            End If
            'If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "F") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtBillToLocation.Value) > 0 AndAlso clsLocation.isLocatinExcisable(txtBillToLocation.Value) Then
            '    clsCommon.MyMessageBoxShow(Me,"Location Can't be excisable of finished goods")
            '    RadPageView1.SelectedPage = RadPageViewPage1
            '    Return False
            'End If

            If chkOpenPO.Checked Then
                If clsCommon.myCdbl(txtAmount.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Fill amount for Open PO", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    txtAmount.Focus()
                    txtAmount.Select()
                    Return False
                End If
            End If

            If chkBlanket.Checked Then
                If Not dtpExpiryDate.Checked Then
                    clsCommon.MyMessageBoxShow(Me, "Select expiry date of PO.", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    dtpExpiryDate.Focus()
                    dtpExpiryDate.Select()
                    Return False
                End If

                If clsCommon.myCdbl(txtAmount.Text) < clsCommon.myCdbl(lblTotRAmtCopy.Text) Then
                    clsCommon.MyMessageBoxShow(Me, "Document Amount cannot be greater than Amount", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    Return False
                End If
            End If

            '=Done By Monika===in case of open po expiry date is mandatory.
            If chkOpenPO.Checked AndAlso dtpExpiryDate.Checked = False Then
                clsCommon.MyMessageBoxShow(Me, "Select expiry date of PO.", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage1
                dtpExpiryDate.Focus()
                dtpExpiryDate.Select()
                Return False
            End If
            If chkOpenPO.Checked AndAlso clsCommon.myCDate(dtpExpiryDate.Text) < clsCommon.myCDate(txtDate.Text) Then
                clsCommon.MyMessageBoxShow(Me, "Expiry date should be greater than Document date.", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage1
                dtpExpiryDate.Focus()
                dtpExpiryDate.Select()
                Return False
            End If
            '=============================================================================

            ''Balwinder copied from last this function becuase partik next statement exit from allow to save.and not check the capex case.
            ''stuti
            ShowCapexCodeandSubCode = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapex, clsFixedParameterCode.ShowOptionforSelectingCapex, Nothing)) = "1", True, False))
            If ShowCapexCodeandSubCode AndAlso clsCommon.CompairString(clsCommon.myCstr(ddl_category.SelectedValue), "Capex") = CompairStringResult.Equal Then
                If clsCommon.myLen(fndcapexsubcode.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "please select capex sub code.", Me.Text)
                    fndcapexcode.Focus()
                    Return False
                End If
                If clsCommon.myCdbl(lbl_rebudgetamtwithtolerence.Text) < clsCommon.myCdbl(lblTotRAmtCopy.Text) Then
                    clsCommon.MyMessageBoxShow(Me, "Document amount exceed budget amount and above tolerence limit.", Me.Text)
                    Return False
                End If
                If clsCommon.myCdbl(lbl_rebudgetamt.Text) < clsCommon.myCdbl(lblTotRAmtCopy.Text) Then
                    clsCommon.MyMessageBoxShow(Me, "Warning: Document amount exceed budget amount but under tolerence limit.", Me.Text)
                End If
            End If
            If ShowCapexCodeandSubCode Then
                If clsCommon.CompairString(clsCommon.myCstr(ddl_category.SelectedValue), "") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "please select category.", Me.Text)
                    ddl_category.Focus()
                    Return False
                End If
            End If

            'Parteek added these functionality only for UDL regarding Work Order 18/09/2017
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then

                If clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboPOType.SelectedValue, "J") = CompairStringResult.Equal Then
                    For ii As Integer = 0 To gvCategoryValue.Rows.Count - 1
                        Dim strFDesc As String = clsCommon.myCstr(gvCategoryValue.Rows(ii).Cells(1).Value)
                        'If clsCommon.myLen(gvCategoryValue.Rows(ii).Cells("Field Desc").Value) <= 0 Then
                        '    If clsCommon.myLen(strFDesc) <= 0 Then
                        '        clsCommon.MyMessageBoxShow(Me,"Please Fill Field Description")
                        '        RadPageView1.SelectedPage = RadPageViewPage6
                        '        Return False
                        '    End If
                        'End If
                        Return True
                    Next

                End If

            End If

            ''End

            '' Anubhooti 01-Nov-2014
            Dim DocDateLessExp As Double
            Dim DeilvDateLessExp As Double
            Dim DocDateLessDelivr As Double
            If clsCommon.myLen(txtDate.Text) > 0 AndAlso clsCommon.myLen(dtpExpiryDate.Text) > 0 AndAlso clsCommon.myLen(txtDeliveryDate.Text) > 0 Then
                DocDateLessExp = Date.Compare(dtpExpiryDate.Text, txtDate.Text)
                DeilvDateLessExp = Date.Compare(dtpExpiryDate.Text, txtDeliveryDate.Text)
                DocDateLessDelivr = Date.Compare(txtDeliveryDate.Text, txtDate.Text)
                If dtpExpiryDate.Checked Then
                    If DocDateLessExp < 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Expiry date must be greater than from document date", Me.Text)
                        RadPageView1.SelectedPage = RadPageViewPage1
                        dtpExpiryDate.Focus()
                        dtpExpiryDate.Select()
                        Return False
                    End If
                    If DeilvDateLessExp < 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Expiry date must be greater than from delivery date", Me.Text)
                        RadPageView1.SelectedPage = RadPageViewPage1
                        dtpExpiryDate.Focus()
                        dtpExpiryDate.Select()
                        Return False
                    End If

                End If
                If DocDateLessDelivr < 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Delivery date must be greater than from document date", Me.Text)
                    RadPageView1.SelectedPage = RadPageViewPage1
                    txtDeliveryDate.Focus()
                    txtDeliveryDate.Select()
                    Return False
                End If
            End If
            ''


            Dim arrProjNo As New List(Of String)
            Dim arrReqNo As New List(Of String)
            Dim arrICode As New List(Of String)()
            Dim arrRowType As New List(Of String)()
            Dim itemCount As Integer = 0
            Dim itemApprovalCount As Integer = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colReqistionNo).Value)
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
                Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
                Dim strRowType As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value)
                Dim dblAmtAfterDis As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
                If clsCommon.CompairString(strRowType, "Item") = CompairStringResult.Equal Then
                    For jj As Integer = 0 To gvSchedule.Rows.Count - 1
                        If clsCommon.myCDecimal(gv1.Rows(ii).Cells(colLineNo).Value) = clsCommon.myCDecimal(gvSchedule.Rows(jj).Cells(colScheduleParentSNo).Value) Then
                            If Not clsCommon.CompairString(strICode, clsCommon.myCstr(gvSchedule.Rows(jj).Cells(colScheduleICode).Value)) = CompairStringResult.Equal Then
                                clsCommon.MyMessageBoxShow(Me, "Please refresh schedule tab. " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + "")
                                Return False
                            End If
                        End If
                    Next

                    Dim strAdvanceRequired As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Is_Advance_Required from Tspl_item_master where Item_code='" & clsCommon.myCstr(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)) & "'"))
                    If clsCommon.CompairString(strAdvanceRequired, "1") = CompairStringResult.Equal Then
                        itemApprovalCount = itemApprovalCount + 1
                    End If
                    If clsCommon.myLen(strICode) > 0 Then
                        itemCount = itemCount + 1
                    End If
                End If

                If dblAmtAfterDis < 0 Then
                    clsCommon.MyMessageBoxShow(Me, " Amount After discount Cannot be in Negative. ")
                    Return False
                End If


                '==========Added by preeti Gupta Against Ticket No[BHA/03/07/18-000129,BHA/16/07/18-000176]=====================
                If DoNotAllowSavePOWhenQtyAndRateZero Then
                    If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 And Not chkBlanket.Checked) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), "Item") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) <= 0 Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            clsCommon.MyMessageBoxShow(Me, "Fill Quantity at row no. " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + "")
                            Return False
                        End If
                        If clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value) <= 0 Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            If chkTender.Checked = True Then
                                clsCommon.MyMessageBoxShow(Me, "Please enter Rate for Item : " + strIName + " into Vendor Item Details Screen. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                            Else
                                clsCommon.MyMessageBoxShow(Me, "Please enter Rate for Item : " + strIName + " . At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                            End If

                            Return False
                        End If

                    End If
                End If

                '========================================================================

                ''richa agarwal against ticket no. BM00000006364
                If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmPurchaseOrderMT) <> CompairStringResult.Equal Then
                    If clsCommon.myCBool(gv1.Rows(ii).Cells(colisMRPMandatory).Value) AndAlso dblMRP <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Please enter MRP for  Item : " + strIName + " . At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Return False
                    End If
                End If

                ''RICHA AGARWAL AGAINST TICKET NO BM00000007116 BM00000007280
                If clsCommon.myLen(gv1.Rows(ii).Cells(colUnit).Value) <= 0 Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), "Item") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Please enter UOM for Item : " + strIName + " . At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Return False
                    End If
                End If
                ''-------------------------------

                ''Dim dblAssessableAmt As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAssessableRate).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                Dim dblBalQty As Double = clsItemLocationDetails.getBalance(strICode, txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, strUOM, dblMRP)

                '============Done By Monika=========in case of open po qty should be 0==
                If chkOpenPO.Checked AndAlso clsCommon.myLen(strICode) > 0 AndAlso dblQty <> 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    gv1.CurrentRow = gv1.Rows(ii)
                    clsCommon.MyMessageBoxShow(Me, "Quantity at row no. " + clsCommon.myCstr(ii + 1) + " should be 0(zero) for Open PO.")
                    Return False
                End If

                If Not chkOpenPO.Checked AndAlso clsCommon.myLen(strICode) > 0 AndAlso (chkAutoCalculate.Checked = True And chkAutoCalculate.Enabled = True) AndAlso dblQty <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    gv1.CurrentRow = gv1.Rows(ii)
                    clsCommon.MyMessageBoxShow(Me, "Fill Quantity at row no. " + clsCommon.myCstr(ii + 1) + ".")
                    Return False
                End If
                '=======================================================================
                Dim strProject As String
                If clsCommon.myLen(strReqNo) > 0 Then
                    If (Not clsRequistionHead.IsValidVendorForRequitionItem(strReqNo, strICode, txtVendorNo.Value)) Then
                        clsCommon.MyMessageBoxShow(Me, "Vendor :" + lblVendorName.Text + " is not valid for Requition No:" + strReqNo + " and Item : " + strIName + " At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Return False
                    End If
                    If dblQty > dblPendingQty Then
                        clsCommon.MyMessageBoxShow(Me, "Item " + strICode + "( " + strIName.Trim() + " ) Entered Quantity(" + clsCommon.myCstr(dblQty) + ") Can't be more than Pending Quantity(" + clsCommon.myCstr(dblPendingQty) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Return False
                    End If
                    If objCommonVar.IsDemoERP Then
                        'If clsCommon.myLen(fndProject.Value) > 0 Then
                        '    If (Not clsRequistionHead.IsValidProjectForRequitionItem(strReqNo, fndProject.Value)) Then
                        '        clsCommon.MyMessageBoxShow(Me,"Project :" + fndProject.Value + " is not valid for Requition No:" + strReqNo + " and Item : " + strIName + " At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        '        Return False
                        '    End If
                        'Else
                        strProject = clsRequistionHead.IsValidProjectForRequition(strReqNo, "")
                        If clsCommon.myLen(strProject) > 0 Then
                            If arrProjNo.Contains(strProject) And arrProjNo.Count > 0 Then
                                clsCommon.MyMessageBoxShow(Me, "Requition No:" + strReqNo + " and Item : " + strIName + " is not for PJC or not related to PJC At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                                RadPageView1.SelectedPage = RadPageViewPage1
                                Return False
                            Else
                                arrProjNo.Add(strProject)
                            End If

                        End If

                    End If
                End If

                If clsCommon.myLen(strICode) > 0 Then
                    If Not arrICode.Contains(strICode) Then
                        arrICode.Add(strICode)
                    End If
                End If
                If clsCommon.myLen(strRowType) > 0 Then
                    If Not arrRowType.Contains(strRowType) Then
                        arrRowType.Add(strRowType)
                    End If
                End If

                If clsCommon.myLen(strICode) > 0 Then

                    If clsCommon.myLen(txtReqNo.Value) <= 0 Then
                        Dim qry1 As String = "SELECT TSPL_PURCHASE_ACCOUNTS.Is_IndentRequired from TSPL_PURCHASE_ACCOUNTS left join TSPL_ITEM_MASTER on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + clsCommon.myCstr(strICode) + "'"
                        Dim isindentreq As Boolean = clsCommon.myCBool(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry1)))
                        If isindentreq Then
                            clsCommon.MyMessageBoxShow(Me, "Indent required for item : " + strICode + ". At Line No" + clsCommon.myCstr(ii + 1))
                            RadPageView1.SelectedPage = RadPageViewPage1
                            Return False
                        End If
                    End If

                    If clsCommon.myLen(txtTenderNo.Value) > 0 AndAlso clsCommon.myCDecimal(txtTenderNo.Tag) = 3 Then
                        Dim qry As String = "select Item_Code,TSPL_TENDER_DETAIL.Unit_code,TSPL_TENDER_DETAIL.Rate,TSPL_TENDER_DETAIL.Discount from TSPL_TENDER_DETAIL where DocumentCode='" + txtTenderNo.Value + "' and Vendor_Code='" + txtVendorNo.Value + "' and Item_Code='" + strICode + "' and Rate=" + clsCommon.myCstr(dblRate) + " and Unit_code='" + strUOM + "'"
                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "item : " + strICode + " is not for vendor: " + txtVendorNo.Value + " and Tender: " + txtTenderNo.Value + " Rate: " + clsCommon.myCstr(dblRate) + " and UOM: " + strUOM + ". At Line No" + clsCommon.myCstr(ii + 1))
                            RadPageView1.SelectedPage = RadPageViewPage1
                            Return False
                        End If
                    ElseIf isItemfromVendorItemDetails Then
                        Dim qry As String = "select 1 from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" + txtVendorNo.Value + "' and item_no in ('" + strICode + "')"
                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "item : " + strICode + " is not for vendor: " + txtVendorNo.Value + " . At Line No" + clsCommon.myCstr(ii + 1))
                            RadPageView1.SelectedPage = RadPageViewPage1
                            Return False
                        End If
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            If jj = ii Then
                                Continue For
                            End If
                            Dim strInnerICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                            Dim dblInnerMRP As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colMRP).Value)
                            Dim strInnerReqNo As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colReqistionNo).Value)

                            ''Dim dblInnerAssessableAmt As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colAssessableRate).Value)
                            Dim strInnerUOM As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)
                            If AllowPurchaseModulewithUniqueItem = 0 Then
                                If dblMRP = dblInnerMRP AndAlso clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, strInnerUOM) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strReqNo, strInnerReqNo) = CompairStringResult.Equal Then
                                    Dim Msg As String = "Same Item Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)

                                    Msg = Msg + Environment.NewLine + "Item: " + strICode + "(" + strIName + ")"
                                    Msg = Msg + Environment.NewLine + "UOM: " + strUOM
                                    If dblMRP > 0 Then
                                        Msg = Msg + Environment.NewLine + "MRP: " + clsCommon.myCstr(dblMRP)
                                    End If
                                    RadPageView1.SelectedPage = RadPageViewPage1
                                    clsCommon.MyMessageBoxShow(Me, Msg)
                                    Return False
                                End If
                            Else
                                If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strReqNo, strInnerReqNo) = CompairStringResult.Equal Then
                                    Dim Msg As String = "Same Item Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                                    Msg = Msg + Environment.NewLine + "Item: " + strICode + "(" + strIName + ")"
                                    RadPageView1.SelectedPage = RadPageViewPage1
                                    clsCommon.MyMessageBoxShow(Me, Msg)
                                    Return False
                                End If
                            End If

                        Next
                    End If
                End If


                If RequiredPOLimit = True AndAlso clsCommon.myLen(clsCommon.myCstr(txtReqNo.Value)) = 0 Then
                    Dim LimitPO As Decimal = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code = 'POLimit' and type='POLimit'")
                    If clsCommon.myCdbl(dblRate) > clsCommon.myCdbl(LimitPO) Then
                        Dim msg As String = "Unit Cost is greater then for limit"
                        clsCommon.MyMessageBoxShow(Me, msg)
                        Return False
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
                            clsCommon.MyMessageBoxShow(Me, "HSN Code is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                            Return False
                        End If

                    End If
                End If
                '' ===== ENd of code===


            Next
            ''richa agarwal 31 Dec,2019
            If itemCount > 0 Then
                If itemApprovalCount > 0 Then
                    If itemApprovalCount <> itemCount Then
                        clsCommon.MyMessageBoxShow(Me, "Item should be of Advance Required type.")
                        Return False
                    End If
                End If
            End If

            '------------road permit/c-form checking-----------------------------------
            AllowToSave_FormEntry()
            '---------------------------------------

            If clsCommon.CompairString(cboPOType.SelectedValue, "I") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtCurrencyCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select multi-currency for import entry.")
                RadPageView1.SelectedPage = RadPageViewPage4
                txtCurrencyCode.Focus()
                txtCurrencyCode.Select()
                Return False
            End If


            '' Changes by Parteek client UDl agaist work order
            If ShowItemAllStructureWise = False Then
                clsItemMaster.isItemOfSameType(clsCommon.myCstr(cboItemType.SelectedValue), cboItemType.Text, arrICode)
            End If
            '' end

            If clsCommon.CompairString(cboItemType.SelectedValue, "N") <> CompairStringResult.Equal Then
                For RowType As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myCstr(gv1.Rows(RowType).Cells(colRowType).Value) <> "Misc" Then
                        If arrICode IsNot Nothing AndAlso arrICode.Count > 0 Then
                            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  case when min(Is_Auto_Weighment)=max(Is_Auto_Weighment) then 1 else 0 end  from TSPL_ITEM_MASTER where Item_Code in (" + clsCommon.GetMulcallString(arrICode) + " )")) = 0 Then
                                clsCommon.MyMessageBoxShow(Me, "All Item should be of Weightment Type or Not", Me.Text)
                                Return False
                            End If
                        End If
                    End If
                Next
            End If
            ''For GST Skip
            Dim isSkipGST As Boolean = False
            dt = clsDBFuncationality.GetDataTable("select sum(case when isnull( Skip_GST,0)=1 then 1 else 0 end) as NoOfSkipGSTItem,sum(case when isnull( Skip_GST,0)=0 then 1 else 0 end) as NoOfNonSkipGSTItem from tspl_item_master where item_Code in (" + clsCommon.GetMulcallString(arrICode) + ")")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.myCdbl(dt.Rows(0)("NoOfSkipGSTItem")) > 0 Then
                    If clsCommon.myCdbl(dt.Rows(0)("NoOfNonSkipGSTItem")) > 0 Then
                        clsCommon.MyMessageBoxShow(Me, "All Item should be of Skip GST or Not", Me.Text)
                        Return False
                    End If
                    isSkipGST = True
                End If
            End If
            dt = Nothing
            If (Not isSkipGST) AndAlso (clsCommon.CompairString(FORMTYPE, clsUserMgtCode.mbtnPurchaseOrder) = CompairStringResult.Equal OrElse clsCommon.CompairString(FORMTYPE, clsUserMgtCode.WorkOrderEng) = CompairStringResult.Equal) Then
                clsLocationWiseTax.IsValidTaxGroup(txtTaxGroup.Value, txtBillToLocation.Value, txtVendorNo.Value, "P", txtDate.Value, Nothing)
            End If
            ''End of For GST Skip
            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()
            ''richa agarwal 24/12/2014
            If chkIsMerchantTrade.Checked Then
                'If clsCommon.myLen(txtPINo.Value) <= 0 Then
                '    txtPINo.Focus()
                '    clsCommon.MyMessageBoxShow(Me,"Please enter PI No")
                '    Return False
                'End If

                If clsCommon.myLen(TxtBeneficiary.Value) <= 0 Then
                    RadPageView1.SelectedPage = RdPaymentterms
                    TxtBeneficiary.Focus()
                    clsCommon.MyMessageBoxShow(Me, "Please select Beneficiary", Me.Text)
                    Return False
                End If
                If clsCommon.myLen(fndPaymenttermsGroup.Value) <= 0 Then
                    RadPageView1.SelectedPage = RdPaymentterms
                    fndPaymenttermsGroup.Focus()
                    clsCommon.MyMessageBoxShow(Me, "Please select Payment Terms Group.", Me.Text)
                    Return False
                End If

                If clsCommon.myLen(TxtBuyerPONo.Text) <= 0 Then
                    RadPageView1.SelectedPage = RdPaymentterms
                    TxtBuyerPONo.Focus()
                    clsCommon.MyMessageBoxShow(Me, "Please enter Buyer PO No.", Me.Text)
                    Return False
                End If
                If TxtBuyerPODate.Checked = False Then
                    RadPageView1.SelectedPage = RdPaymentterms
                    TxtBuyerPODate.Focus()
                    clsCommon.MyMessageBoxShow(Me, "Please select Buyer PO Date.", Me.Text)
                    Return False
                End If

                If clsCommon.CompairString(cmbTerms_Payment.SelectedValue, "AD") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(cmbAdvanceType.SelectedValue, "") = CompairStringResult.Equal Then
                        RadPageView1.SelectedPage = RdPaymentterms
                        cmbAdvanceType.Focus()
                        clsCommon.MyMessageBoxShow(Me, "Please Select Advance Type", Me.Text)
                        Return False
                    End If
                    If clsCommon.myCdbl(txtAdvance_Pers.Value) < 0 Then
                        RadPageView1.SelectedPage = RdPaymentterms
                        txtAdvance_Pers.Focus()
                        clsCommon.MyMessageBoxShow(Me, "Advance Value cannot be in negative", Me.Text)
                        Return False
                    End If
                    If clsCommon.myCdbl(txtAdvance_Pers.Value) = 0 Then
                        RadPageView1.SelectedPage = RdPaymentterms
                        txtAdvance_Pers.Focus()
                        clsCommon.MyMessageBoxShow(Me, "Advance Value cannot be zero", Me.Text)
                        Return False
                    End If
                End If
                If rdbAmountinrupees.Checked Then
                    If (clsCommon.myCdbl(TxtLC.Value) + clsCommon.myCdbl(TxtCAD.Value) + clsCommon.myCdbl(TxtOnAccount.Value) + clsCommon.myCdbl(txtRetained.Value) + clsCommon.myCdbl(TxtBalancePayment.Value) + clsCommon.myCdbl(txtAdvance.Value) + clsCommon.myCdbl(TxtCIF.Value)) <> clsCommon.myCdbl(lblTotRAmtCopy.Text) Then
                        RadPageView1.SelectedPage = RdPaymentterms
                        clsCommon.MyMessageBoxShow(Me, "Sum of " & IIf(txtAdvance.Enabled, "" & lblAdvance.Text & ",", "") & " " & IIf(TxtLC.Enabled, "" & lblLC.Text & ",", "") & " " & IIf(TxtBalancePayment.Enabled, "" & lblBalancePayment.Text & ",", "") & " " & IIf(TxtCAD.Enabled, "" & lblCad.Text & ",", "") & " " & IIf(TxtOnAccount.Enabled, "" & lblonAccount.Text & ",", "") & " " & IIf(txtRetained.Enabled, "" & lblretained.Text & ",", "") & " " & IIf(TxtCIF.Enabled, "" & lblCIF.Text & ",", "") & " should be same as document amount  ")
                        Return False
                    End If
                Else
                    If (clsCommon.myCdbl(TxtLC.Value) + clsCommon.myCdbl(TxtCAD.Value) + clsCommon.myCdbl(TxtOnAccount.Value) + clsCommon.myCdbl(txtRetained.Value) + clsCommon.myCdbl(TxtBalancePayment.Value) + clsCommon.myCdbl(txtAdvance.Value) + clsCommon.myCdbl(TxtCIF.Value)) <> 100 Then
                        RadPageView1.SelectedPage = RdPaymentterms
                        clsCommon.MyMessageBoxShow(Me, "Sum of " & IIf(txtAdvance.Enabled, "" & lblAdvance.Text & ",", "") & " " & IIf(TxtLC.Enabled, "" & lblLC.Text & ",", "") & " " & IIf(TxtBalancePayment.Enabled, "" & lblBalancePayment.Text & ",", "") & " " & IIf(TxtCAD.Enabled, "" & lblCad.Text & ",", "") & " " & IIf(TxtOnAccount.Enabled, "" & lblonAccount.Text & ",", "") & " " & IIf(txtRetained.Enabled, "" & lblretained.Text & ",", "") & " " & IIf(TxtCIF.Enabled, "" & lblCIF.Text & ",", "") & " should be 100 ")
                        Return False
                    End If
                End If

            End If
            ''-------------------------


            '====================end here================
            '--------------richa 09/07/2014 Ticket No BM00000003042---------
            If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.WorkOrderEng) <> CompairStringResult.Equal Then
                If Not (CheckQuantityForPurchaseOrder()) Then
                    Return False
                End If
            End If
            ''--------------------------------------------------------------
            If chkRepair.Checked AndAlso chkRepair.Visible AndAlso clsCommon.CompairString(clsCommon.myCstr(cboPOType.SelectedValue), "J") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "N") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "Repair cannot be on for Non inventory items", Me.Text)
                Return False
            End If
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtReferencePO.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Reference PO can not be blank.", Me.Text)
                    txtReferencePO.Focus()
                    Return False
                End If
            End If


            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Function

    '--------------richa 09/07/2014 Ticket No BM00000003045---------
    Function CheckQuantityForPurchaseOrder() As Boolean
        Dim desc As String = ""
        Dim strCondition As String = ""
        Dim strCondition1 As String = ""
        Dim StrMessage As String = Nothing

        Try


            desc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.NotificationSettingforReOrderInPO, clsFixedParameterCode.NotificationSettingforReOrderInPO, Nothing))
            If clsCommon.CompairString(desc, "0") = CompairStringResult.Equal Then

            Else
                If btnSave.Text = "Update" Then
                    strCondition = "  and TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No<>'" + txtDocNo.Value + "' "
                    'strCondition1 = " and TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id<>'" + txtReqNo.Value + "' "
                End If

                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim StockBalQty As Double = 0
                    Dim ReorderLevel As Double = 0
                    Dim MaxLevel As Double = 0
                    Dim MinLevel As Double = 0
                    Dim RequiredQty As Double = 0
                    Dim QtyAfterAddingStockQty As Double = 0
                    Dim SumOfQtyofRequest As Double = 0

                    If (clsCommon.myLen(grow.Cells(colICode).Value) > 0) Then
                        Dim StrItemCode = clsCommon.myCstr(grow.Cells(colICode).Value)
                        Dim PurchaseQty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        Dim Qry As String = ""
                        Qry = "Select TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code, TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code, FINAL.ActualBalanceQty As StockQty, (ISNUll(TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level,0)*uom1.conversion_factor) As ReOrder_Level,(ISNULL(TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level,0)*uom1.conversion_factor) As Min_Level,(ISNULL(TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level,0)*uom1.conversion_factor) As Max_Level  from (" &
                      " select ICode,SUM(Qty * case when TransType='' then 1 else 0 end)as BalanceQty,SUM(Qty * case when TransType='' then 0 else 1 end)as CommitQty,SUM(Qty *RI )as ActualBalanceQty from (select  xx.TransType,xx.TransCode,xx.DocNo, xx.ICode,xx.Location,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,( (xx.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) as Qty" &
                      " from (" &
                      " select '' as TransType,'' as TransCode,'' as DocNo, Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from( select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from( select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,TSPL_INVENTORY_MOVEMENT.Qty   ,TSPL_INVENTORY_MOVEMENT.UOM as UOMNew  from TSPL_INVENTORY_MOVEMENT  where TSPL_INVENTORY_MOVEMENT.Qty<>0 and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end)  )xxx   )xxxx group by Item_Code,Location_Code,UOMNew" &
                      " union all" &
                      " select 'Purchase Return' as TransType,'PurchaseReturn' as TransCode,TSPL_PR_HEAD.PR_No as DocNo, TSPL_PR_DETAIL.Item_Code as ICode,TSPL_PR_DETAIL.Location as Locaion,TSPL_PR_DETAIL.PR_Qty as Qty,-1 as RI,TSPL_PR_DETAIL.Unit_code AS Uom  from TSPL_PR_DETAIL  left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No where TSPL_PR_HEAD.Status=0 and TSPL_PR_DETAIL.PR_Qty<>0" &
                      " UNION ALL" &
                      " select 'IC-AD' as TransType,'ICAdj' as TransCode,TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo, TSPL_ADJUSTMENT_DETAIL.Item_Code as ICode,TSPL_ADJUSTMENT_HEADER.Loc_Code as Locaion,TSPL_ADJUSTMENT_DETAIL.Item_Quantity as Qty,-1 as RI,TSPL_ADJUSTMENT_DETAIL.Unit_Code AS Uom  from TSPL_ADJUSTMENT_DETAIL  left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No where TSPL_ADJUSTMENT_HEADER.Posted='N' and TSPL_ADJUSTMENT_DETAIL.Item_Quantity<>0  and TSPL_ADJUSTMENT_DETAIL.Adjustment_Type in ('BD','QD')" &
                      " UNION ALL" &
                      " select 'RGP' as TransType,'RGP' as TransCode,TSPL_RGP_HEAD.RGP_No as DocNo, TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_RGP_HEAD.Location as Locaion,TSPL_RGP_DETAIL.RGP_Qty as Qty,-1 as RI,TSPL_RGP_DETAIL.Unit_code AS Uom  from TSPL_RGP_DETAIL  left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_HEAD.Status=0 and TSPL_RGP_DETAIL.RGP_Qty<>0" &
                      " union all" &
                      " select 'Scrap' as TransType,'ScrapShipment' as TransCode,TSPL_SCRAPSALE_HEAD.shipment_No as DocNo, TSPL_SCRAPSALE_DETAIL.Item_Code as ICode,TSPL_SCRAPSALE_HEAD.Loc_Code as Locaion,TSPL_SCRAPSALE_DETAIL.shipped_Qty as Qty,-1 as RI,TSPL_SCRAPSALE_DETAIL.Unit_code AS Uom  from TSPL_SCRAPSALE_DETAIL  left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No where TSPL_SCRAPSALE_HEAD.IsPost=0 and TSPL_SCRAPSALE_DETAIL.shipped_Qty<>0" &
                      " union all" &
                      " select 'Issue/Return/Transfer' as TransType,'IssueReturnTransfer' as TransCode,TSPL_IssueReturn_HEAD.Doc_No as DocNo, TSPL_IssueReturn_DETAIL.Item_Code as ICode,TSPL_IssueReturn_HEAD.From_Location as Locaion,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,-1 as RI,TSPL_IssueReturn_DETAIL.Unit_code AS Uom  from TSPL_IssueReturn_DETAIL  left outer join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_HEAD.Doc_No=TSPL_IssueReturn_DETAIL.Doc_No where TSPL_IssueReturn_HEAD.Status=0 and TSPL_IssueReturn_DETAIL.Issued_Qty<>0" &
                      " union all" &
                      " select  'Shipment' as TransType,'SDShipment' as TransCode,TSPL_SD_SHIPMENT_HEAD.Document_Code as DocNo, TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location as Locaion,TSPL_SD_SHIPMENT_DETAIL.Qty as Qty,-1 as RI,TSPL_SD_SHIPMENT_DETAIL.Unit_code AS Uom   from TSPL_SD_SHIPMENT_DETAIL  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=0 and TSPL_SD_SHIPMENT_DETAIL.Qty<>0" &
                      " union all" &
                      " Select '' AS TransType, '' AS TransCode, TSPL_REQUISITION_DETAIL.Requisition_Id, Case When ISNULL(PurchaseOrder_No,'')='' Then TSPL_REQUISITION_DETAIL.Item_Code Else TSPL_PURCHASE_ORDER_DETAIL.Item_Code End As Item_Code, TSPL_REQUISITION_DETAIL.Location, Case When ISNULL(Requisition_Qty,0)>ISNULL(PurchaseOrder_Qty,0) Then Requisition_Qty Else PurchaseOrder_Qty End as Qty, 1 as RI, TSPL_REQUISITION_DETAIL.Unit_Code from TSPL_REQUISITION_DETAIL LEFT OUTER JOIN TSPL_PURCHASE_ORDER_DETAIL ON TSPL_REQUISITION_DETAIL.Requisition_Id=TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id  " &
                        " union all" &
                        " Select '' AS TransType, '' AS TransCode, TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No, Case When ISNULL(PurchaseOrder_No,'')='' Then TSPL_REQUISITION_DETAIL.Item_Code Else TSPL_PURCHASE_ORDER_DETAIL.Item_Code End As Item_Code, TSPL_PURCHASE_ORDER_DETAIL.Location, Case When ISNULL(Requisition_Qty,0)>ISNULL(PurchaseOrder_Qty,0) Then Requisition_Qty Else PurchaseOrder_Qty End as Qty, 1 as RI, TSPL_PURCHASE_ORDER_DETAIL.Unit_code from   TSPL_PURCHASE_ORDER_DETAIL LEFT OUTER JOIN TSPL_REQUISITION_DETAIL ON TSPL_REQUISITION_DETAIL.Requisition_Id=TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id WHERE ISNULL(TSPL_REQUISITION_DETAIL.Requisition_Id,'')='' " + strCondition + " " &
                        " union all" &
                      " Select 'SRN' As TransType,'SRN' As TransCode,TSPL_SRN_HEAD.SRN_No  AS DocNo,TSPL_SRN_DETAIL .Item_Code As ICODE,TSPL_SRN_HEAD.Bill_To_Location as Location, TSPL_SRN_DETAIL.SRN_Qty  As Qty,1 as RI,TSPL_SRN_DETAIL.Unit_code As UnitCode from TSPL_SRN_HEAD Left Outer Join TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_HEAD.Status<>1 and ISNULL(TSPL_SRN_HEAD.Against_PO,'')='' and ISNULL(TSPL_SRN_HEAD.Against_Requisition,'')='' " &
                      " union all" &
                      " select 'Assemblies' as TransType,'Assemblies' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI, BUILD_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES where TSPL_PJC_ASSEMBLIES.POSTED=0" &
                      " union all" &
                      " select  'Assemblies' as TransType,'Assemblies' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location, TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*(TSPL_PJC_ASSEMBLIES.QUANTITY/TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY) as Qty, (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI, TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES  inner join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_PJC_ASSEMBLIES.BOM_CODE inner JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE  where TSPL_PJC_ASSEMBLIES.POSTED=0)xx" &
                      " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM  left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode AND FinalUOM.Stocking_Unit='Y' and xx.Location='" + txtBillToLocation.Value + "')FinalQry group by ICode" &
                      " ) FINAL RIGHT OUTER JOIN TSPL_ITEM_REORDER_LEVEL_NEW ON TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code=FINAL.ICode LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code left outer join TSPL_ITEM_UOM_DETAIL uom1 on TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code = uom1.Item_Code and uom1.UOM_Code=(case when isnull(TSPL_ITEM_REORDER_LEVEL_NEW.UOM_Code,'')='' then uom1.UOM_Code else TSPL_ITEM_REORDER_LEVEL_NEW.Uom_Code end) WHERE TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code='" + StrItemCode + "' and TSPL_ITEM_REORDER_LEVEL_NEW.Apply='Y'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            StockBalQty = clsCommon.myCdbl(dt.Rows(0)("StockQty"))
                            ReorderLevel = clsCommon.myCdbl(dt.Rows(0)("ReOrder_Level"))
                            MaxLevel = clsCommon.myCdbl(dt.Rows(0)("Max_Level"))
                            MinLevel = clsCommon.myCdbl(dt.Rows(0)("Min_Level"))
                            If clsCommon.myLen(txtReqNo.Value) = 0 Then
                                QtyAfterAddingStockQty = StockBalQty + PurchaseQty
                            Else
                                QtyAfterAddingStockQty = StockBalQty
                            End If

                            If QtyAfterAddingStockQty > MaxLevel Then
                                RequiredQty = QtyAfterAddingStockQty - MaxLevel
                                StrMessage = "Some Items have reached above to their MAX LEVEL "
                            End If
                        End If
                    End If

                Next

                If StrMessage <> "" Then
                    desc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.NotificationSettingforReOrderInPO, clsFixedParameterCode.NotificationSettingforReOrderInPO, Nothing))
                    If clsCommon.CompairString(desc, "1") = CompairStringResult.Equal Then
                        StrMessage = StrMessage & " So you do not create/update Purchase Order"
                        clsCommon.MyMessageBoxShow(Me, StrMessage)
                        Return False
                    ElseIf clsCommon.CompairString(desc, "2") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, StrMessage)
                        Return True

                    End If


                End If
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Function

    ''---------------------Richa Code Ends Here-------------------------

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub
    '' Anubhooti 09-Sep-2014 BM00000003735
    Private Function FinYrCheck(ByVal Save As Boolean, ByVal Post As Boolean) As Boolean
        If (Save = True And Post = False) OrElse (Save = False And Post = True) Then
            If FrmMainTranScreen.ValidateTransactionAccToFinYear("Purchase Order", txtDate.Value) = False Then
                Return False
            End If
        End If
        Return True
        ''
    End Function

    Private Function SavingData(ByVal ChekBtnPost As Boolean) As Boolean
        If (SaveData(False)) Then
            If ChekBtnPost = False Then
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
                btnCopy.Enabled = False
            End If
            Return True
        Else
            Return False
        End If
    End Function

    Public Function SaveData(ByVal isDoAbandomentNo As Boolean, Optional ByVal isPOCancel As Boolean = False, Optional ByVal isAmendment As Boolean = False) As Boolean
        Try
            If FinYrCheck(True, False) = False Then
                Return False
            End If
            '==================check approval condition=============================
            ''BM00000008148 approval work 16/10/2015
            Dim totalqty As Decimal = 0
            If Not AllowModifcationByApprovalUser Then
                clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value))
            End If
            '=====================end here===================


            If (AllowToSave()) Then
                Dim obj As New clsPurchaseOrderHead()
                obj.roadpermit = "0"
                If Chkroadpermit.Checked Then
                    obj.roadpermit = "1"
                End If

                obj.Cform = "0"
                If chk_c_form.Checked Then
                    obj.Cform = "1"
                End If
                If isPOCancel Then
                    obj.IsCancel = 1
                Else
                    obj.IsCancel = 0
                End If


                obj.Is_Open_PO = CInt(clsCommon.myCdbl(IIf(chkOpenPO.Checked = True, 1, 0)))
                obj.Against_PO = clsCommon.myCstr(txtAgainstPO_No.Text)
                If dtpRenewal.Checked Then
                    obj.Renewal_Date = clsCommon.myCstr(dtpRenewal.Text)
                Else
                    obj.Renewal_Date = ""
                End If

                obj.ReferencePO = clsCommon.myCstr(txtReferencePO.Text)
                obj.Against_Vendor_Quotation = lblVendorQuotationNo.Text
                obj.Delivery_Terms_Code = clsCommon.myCstr(txtDelivery_Code.Value)
                obj.Payment_Terms = clsCommon.myCstr(txtPaymentTerm.Text)
                obj.Insurance_Terms = clsCommon.myCstr(txtInsuranceTerms.Text)
                obj.PurchaseOrder_No = txtDocNo.Value
                obj.PurchaseOrder_Date = txtDate.Value
                obj.Delivery_date = txtDeliveryDate.Value
                obj.Delivery_Duration = clsCommon.myCstr(txtDeliveryDuration.Text)
                obj.Vendor_Code = txtVendorNo.Value
                obj.Vendor_Name = lblVendorName.Text
                obj.Ref_No = txtRefNo.Text
                obj.Total_Tax_Amt = clsCommon.myCdbl(lblTaxAmt.Text)
                obj.Remarks = txtRemarks.Text
                obj.Bill_To_Location = txtBillToLocation.Value
                obj.Ship_To_Location = txtShipToLocation.Value
                obj.Sublocation_Code = txtSubLocation.Value

                ' Convert the RTF formatted text to HTML
                'Dim htmlText As String = RtfToHtml(formattedText)

                obj.Comments = txtComment.Rtf
                obj.Comment1 = txtCmt1.Rtf
                obj.Comment2 = txtCmt2.Rtf
                obj.Comment3 = txtCmt3.Rtf
                obj.Comment4 = txtCmt4.Rtf
                obj.Comment5 = txtCmt5.Rtf
                obj.Comment6 = txtCmt6.Rtf
                obj.Comment7 = txtCmt7.Rtf
                obj.Comment8 = txtCmt8.Rtf
                obj.Comment9 = txtCmt9.Rtf
                obj.Comment10 = txtCmt10.Rtf
                obj.Comment11 = txtCmt11.Rtf
                obj.Comment12 = txtCmt12.Rtf
                obj.Comment13 = txtCmt13.Rtf
                'obj.Comment14 = txtCmt14.Text
                'obj.Comments = RTComment.Rtf
                obj.On_Hold = chkOnHold.Checked
                obj.Mode_Of_Transport = cboModeOfTransport.Text
                obj.Description = txtDesc.Text
                obj.Tax_Group = txtTaxGroup.Value
                obj.GSTRegistered = chkGSTRegistered.Checked
                obj.PurchaseOrder_Type = clsCommon.myCstr(cboPOType.SelectedValue)
                obj.PROJECT_ID = fndProject.Value
                obj.MCC_Purchase = IIf(chkMCCPurchase.Checked, 1, 0)
                obj.State_Code = fndState.Value
                obj.PO_Amount = clsCommon.myCdbl(txtAmount.Text)
                obj.isBlanket = IIf(chkBlanket.Checked, 1, 0)
                obj.IsPO = IIf(ChkISPO.Checked, 1, 0)
                obj.IsContent = IIf(chkIsContent.Checked, 1, 0)
                obj.isJobWorkOutward = IIf(chkJobWorkOutward.Checked, 1, 0)
                obj.Auto_Calculate = IIf(chkAutoCalculate.Checked, 1, 0)
                obj.Subject = clsCommon.myCstr(txtSubject.Text)
                obj.Content_Subject = clsCommon.myCstr(txtContentSubject.Text)
                obj.Kind_Attentation = clsCommon.myCstr(txtKindAttentation.Text)
                obj.Against_Tender = IIf(chkTender.Checked = True, "Y", "N")
                obj.ServiceBill_Date = clsCommon.myCDate(dtBillDate.Value)
                obj.ServiceBill_No = clsCommon.myCstr(txtBillNo.Text)
                If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.WorkOrderEng) = CompairStringResult.Equal Then
                    obj.RefTendorNo = txtRefTendorNo.Text
                Else
                    obj.RefTendorNo = txtTenderNo.Value
                    If clsCommon.myLen(txtTenderNo.Value) > 0 Then
                        obj.Against_Tender = "Y"
                    End If
                End If
                obj.Form_ID = clsCommon.myCstr(FORMTYPE)

                If (clsCommon.CompairString(obj.PurchaseOrder_Type, "J") = CompairStringResult.Equal) Then
                    obj.Against_RGP = clsCommon.myCdbl(chkAgainst_RGP.Checked)
                End If
                If ShowItemAllStructureWise = False Then
                    obj.Item_Type = clsCommon.myCstr(cboItemType.SelectedValue)
                Else
                    obj.Item_Type = "A"
                End If
                obj.Dept = txtDept.Value
                    obj.Dept_Desc = lblDept.Text
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
                    obj.Terms_Remark = txtTermRemark.Text
                    obj.Due_Date = txtDueDate.Value
                    obj.Discount_Base = clsCommon.myCdbl(lblAmtWithDiscount.Text)
                    obj.Header_Discount_Amount = txtHeaderDiscountAmount.Value
                    obj.Discount_Amt = clsCommon.myCdbl(lblDiscountAmt.Text)

                    obj.Amount_Less_Discount = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
                    obj.Total_Taxable_Amount = clsCommon.myCdbl(lblTaxableAmount.Text)
                    obj.PO_Total_Amt = clsCommon.myCdbl(lblTotRAmt.Text)
                    obj.Abandonment_No = clsCommon.myCdbl(lblAbandonmentNo.Text)
                    If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.WorkOrderEng) = CompairStringResult.Equal Then
                        obj.Against_WorkEstimation_Id = txtReqNo.Value
                    Else
                        obj.Against_Requisition = txtReqNo.Value
                    End If
                    obj.Against_RGP_NO = txtRGPNo.Value
                    obj.Capex_Code = fndcapexcode.Value
                    obj.Capex_SubCode = fndcapexsubcode.Value
                    obj.Category = clsCommon.myCstr(ddl_category.SelectedValue.ToString())
                    obj.Emergency = IIf(chk_emergency.Checked, 1, 0)
                    obj.Deliverydays = CInt(txt_deliverydays.Text)
                    If chkpoclose.Checked = True Then
                        obj.close_yn = "Y"
                    ElseIf chkpoclose.Checked = False Then
                        obj.close_yn = "N"
                    End If
                    obj.Amt_After_Tax = clsCommon.myCdbl(lblAmtAfterTax.Text)
                    If (gvAC.Rows.Count > 0) Then
                        If clsCommon.myLen(gvAC.Rows(0).Cells(colACCode).Value) > 0 Then
                            obj.Add_Charge_Code1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colACCode).Value)
                            obj.Add_Charge_Name1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colACName).Value)
                            obj.Add_Charge_Amt1 = clsCommon.myCdbl(gvAC.Rows(0).Cells(colACAmount).Value)
                            obj.Add_Charge_Apply_On1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colACApplyOn).Value)
                            obj.Add_Charge_Per1 = clsCommon.myCdbl(gvAC.Rows(0).Cells(colACPer).Value)
                        End If
                    End If
                    If (gvAC.Rows.Count > 1) Then
                        If clsCommon.myLen(gvAC.Rows(1).Cells(colACCode).Value) > 0 Then
                            obj.Add_Charge_Code2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colACCode).Value)
                            obj.Add_Charge_Name2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colACName).Value)
                            obj.Add_Charge_Amt2 = clsCommon.myCdbl(gvAC.Rows(1).Cells(colACAmount).Value)
                            obj.Add_Charge_Apply_On2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colACApplyOn).Value)
                            obj.Add_Charge_Per2 = clsCommon.myCdbl(gvAC.Rows(1).Cells(colACPer).Value)
                        End If
                    End If
                    If (gvAC.Rows.Count > 2) Then
                        If clsCommon.myLen(gvAC.Rows(2).Cells(colACCode).Value) > 0 Then
                            obj.Add_Charge_Code3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colACCode).Value)
                            obj.Add_Charge_Name3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colACName).Value)
                            obj.Add_Charge_Amt3 = clsCommon.myCdbl(gvAC.Rows(2).Cells(colACAmount).Value)
                            obj.Add_Charge_Apply_On3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colACApplyOn).Value)
                            obj.Add_Charge_Per3 = clsCommon.myCdbl(gvAC.Rows(2).Cells(colACPer).Value)
                        End If
                    End If
                    If (gvAC.Rows.Count > 3) Then
                        If clsCommon.myLen(gvAC.Rows(3).Cells(colACCode).Value) > 0 Then
                            obj.Add_Charge_Code4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colACCode).Value)
                            obj.Add_Charge_Name4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colACName).Value)
                            obj.Add_Charge_Amt4 = clsCommon.myCdbl(gvAC.Rows(3).Cells(colACAmount).Value)
                            obj.Add_Charge_Apply_On4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colACApplyOn).Value)
                            obj.Add_Charge_Per4 = clsCommon.myCdbl(gvAC.Rows(3).Cells(colACPer).Value)
                        End If
                    End If
                    If (gvAC.Rows.Count > 4) Then
                        If clsCommon.myLen(gvAC.Rows(4).Cells(colACCode).Value) > 0 Then
                            obj.Add_Charge_Code5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colACCode).Value)
                            obj.Add_Charge_Name5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colACName).Value)
                            obj.Add_Charge_Amt5 = clsCommon.myCdbl(gvAC.Rows(4).Cells(colACAmount).Value)
                            obj.Add_Charge_Apply_On5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colACApplyOn).Value)
                            obj.Add_Charge_Per5 = clsCommon.myCdbl(gvAC.Rows(4).Cells(colACPer).Value)
                        End If
                    End If
                    If (gvAC.Rows.Count > 5) Then
                        If clsCommon.myLen(gvAC.Rows(5).Cells(colACCode).Value) > 0 Then
                            obj.Add_Charge_Code6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colACCode).Value)
                            obj.Add_Charge_Name6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colACName).Value)
                            obj.Add_Charge_Amt6 = clsCommon.myCdbl(gvAC.Rows(5).Cells(colACAmount).Value)
                            obj.Add_Charge_Apply_On6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colACApplyOn).Value)
                            obj.Add_Charge_Per6 = clsCommon.myCdbl(gvAC.Rows(5).Cells(colACPer).Value)
                        End If
                    End If
                    If (gvAC.Rows.Count > 6) Then
                        If clsCommon.myLen(gvAC.Rows(6).Cells(colACCode).Value) > 0 Then
                            obj.Add_Charge_Code7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colACCode).Value)
                            obj.Add_Charge_Name7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colACName).Value)
                            obj.Add_Charge_Amt7 = clsCommon.myCdbl(gvAC.Rows(6).Cells(colACAmount).Value)
                            obj.Add_Charge_Apply_On7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colACApplyOn).Value)
                            obj.Add_Charge_Per7 = clsCommon.myCdbl(gvAC.Rows(6).Cells(colACPer).Value)
                        End If
                    End If
                    If (gvAC.Rows.Count > 7) Then
                        If clsCommon.myLen(gvAC.Rows(7).Cells(colACCode).Value) > 0 Then
                            obj.Add_Charge_Code8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colACCode).Value)
                            obj.Add_Charge_Name8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colACName).Value)
                            obj.Add_Charge_Amt8 = clsCommon.myCdbl(gvAC.Rows(7).Cells(colACAmount).Value)
                            obj.Add_Charge_Apply_On8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colACApplyOn).Value)
                            obj.Add_Charge_Per8 = clsCommon.myCdbl(gvAC.Rows(7).Cells(colACPer).Value)
                        End If
                    End If
                    If (gvAC.Rows.Count > 8) Then
                        If clsCommon.myLen(gvAC.Rows(8).Cells(colACCode).Value) > 0 Then
                            obj.Add_Charge_Code9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colACCode).Value)
                            obj.Add_Charge_Name9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colACName).Value)
                            obj.Add_Charge_Amt9 = clsCommon.myCdbl(gvAC.Rows(8).Cells(colACAmount).Value)
                            obj.Add_Charge_Apply_On9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colACApplyOn).Value)
                            obj.Add_Charge_Per9 = clsCommon.myCdbl(gvAC.Rows(8).Cells(colACPer).Value)
                        End If
                    End If
                    If (gvAC.Rows.Count > 9) Then
                        If clsCommon.myLen(gvAC.Rows(9).Cells(colACCode).Value) > 0 Then
                            obj.Add_Charge_Code10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colACCode).Value)
                            obj.Add_Charge_Name10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colACName).Value)
                            obj.Add_Charge_Amt10 = clsCommon.myCdbl(gvAC.Rows(9).Cells(colACAmount).Value)
                            obj.Add_Charge_Apply_On10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colACApplyOn).Value)
                            obj.Add_Charge_Per10 = clsCommon.myCdbl(gvAC.Rows(9).Cells(colACPer).Value)
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
                    obj.Quotation_No = txtQuotationNo.Text
                    If clsCommon.myLen(txtQuotationNo.Text) > 0 Then
                        obj.Quotation_Date = txtQuotationDate.Value
                    End If
                    obj.is_Excise_On_Qty = chkExciseOnQty.Checked
                    If dtpExpiryDate.Checked Then
                        obj.Expiry_Date = dtpExpiryDate.Value
                    End If
                    ''richa agarwal 24/12/2014
                    If chkIsMerchantTrade.Checked Then
                        obj.MT_Is_Merchant_Trade = 1
                    Else
                        obj.MT_Is_Merchant_Trade = 0
                    End If
                    obj.MT_PI_No = clsCommon.myCstr(txtPINo.Value)
                    obj.MT_PI_Status = clsCommon.myCstr(cboPIStatus.SelectedValue)
                    obj.MT_Buyer_PO_No = clsCommon.myCstr(TxtBuyerPONo.Text)
                    If TxtBuyerPODate.Checked Then
                        obj.MT_Buyer_PO_Date = TxtBuyerPODate.Value
                    Else
                        obj.MT_Buyer_PO_Date = Nothing
                    End If
                    If txtPIDate.Checked Then
                        obj.MT_PI_Status_Date = txtPIDate.Value
                    Else
                        obj.MT_PI_Status_Date = Nothing
                    End If
                    obj.MT_Payment_Terms_Group_Code = clsCommon.myCstr(fndPaymenttermsGroup.Value)
                    obj.MT_HS_Classification_No = clsCommon.myCstr(TxtHSClassificationNo.Text)
                    If rdbAmountinrupees.Checked Then
                        obj.MT_Is_AmountinRs = 1
                    Else
                        obj.MT_Is_AmountinRs = 0
                    End If
                    obj.MT_LC = clsCommon.myCdbl(TxtLC.Value)
                    obj.MT_CAD = clsCommon.myCdbl(TxtCAD.Value)
                    obj.MT_On_Account = clsCommon.myCdbl(TxtOnAccount.Value)
                    obj.MT_Balance_Payment = clsCommon.myCdbl(TxtBalancePayment.Value)
                    obj.MT_RETAINED = clsCommon.myCdbl(txtRetained.Value)
                    ''richa agarwal 09/04/2015
                    obj.MT_CIF = clsCommon.myCdbl(TxtCIF.Value)
                    ''------------------
                    obj.MT_Advance = clsCommon.myCdbl(txtAdvance.Value)
                    obj.MT_Beneficiary_Code = clsCommon.myCstr(TxtBeneficiary.Value)
                    obj.MT_INCOTERMS = clsCommon.myCstr(TxtINCOTERMS.Text)
                    ''-----------------------
                    ''RICHA AGARWAL 08/04/2015
                    obj.MT_is_Accepted = clsCommon.myCstr(IIf(chkAcceptance.Checked = True, "Y", "N"))
                    obj.MT_Accepted_Date = clsCommon.myCDate(dtpAcceptance.Value)
                    obj.MT_is_Partshipment = clsCommon.myCstr(IIf(chkPartshipment.Checked = True, "Y", "N"))
                    obj.MT_is_Transshipment = clsCommon.myCstr(IIf(chkTransshipment.Checked = True, "Y", "N"))


                    obj.MT_EX_Term_Code = clsCommon.myCstr(cmbTerms.SelectedValue)
                    obj.MT_Payment_Terms = clsCommon.myCstr(cmbTerms_Payment.SelectedValue)
                    obj.MT_Stuffing_Status = clsCommon.myCstr(cboStuffing.SelectedValue)
                    obj.MT_Advance_Type = clsCommon.myCstr(cmbAdvanceType.SelectedValue)
                    obj.MT_is_Partpayment = clsCommon.myCstr(IIf(ChkPartPayment.Checked = True, "Y", "N"))
                    obj.MT_PT_Advance_Amount = clsCommon.myCdbl(txtAdvance_Pers.Value)
                    obj.MT_PI_Due_Date = clsCommon.myCDate(txtPIDueDate.Value)
                    obj.MT_CreditTerms_Code = clsCommon.myCstr(FndCreditTerms.Value)
                    obj.MT_CreditTermsName = clsCommon.myCstr(TxtCreditTermsName.Text)
                    obj.MT_Pre_Carriage_By = clsCommon.myCstr(txtPre_Carriage_By.SelectedValue)
                    obj.MT_Discharge_Port = clsCommon.myCstr(txtPort_Discharge.Text)
                    obj.MT_Final_Destination = clsCommon.myCstr(txtFinal_Destination.Text)
                    obj.MT_Final_Destination_Country = clsCommon.myCstr(fndCountry_Final_Destination.Value)
                    obj.MT_Origin_Country = clsCommon.myCstr(fndCountry_Origin.Value)
                    obj.MT_Carrier = clsCommon.myCstr(txtCarrier.Text)
                    ''---------------
                    obj.IsAbatementPO = IsAbatementPO

                    obj.Apply_Receive_Control = chkReceiveControl.Checked
                    obj.Bank_Code = txtBankCode.Value
                    obj.Payment_Code = txtPaymentMode.Value

                    '' Work done agaist ticket no.BHA/13/08/18-000419
                    obj.Insurance = clsCommon.myCstr(txtInsurance.Text)
                    obj.Packing_Forward = clsCommon.myCstr(txtPackingForward.Text)
                    obj.Freight = clsCommon.myCstr(txtFreight.Text)
                    obj.Retention = clsCommon.myCdbl(TxtRetention.Text)
                    If obj.Retention >= 0 AndAlso obj.Retention <= 100 Then
                        obj.Retention = clsCommon.myCdbl(TxtRetention.Text)
                    Else
                        clsCommon.MyMessageBoxShow("Please enter a value between 0 and 100.", Me.Text)
                        TxtRetention.Focus()
                        TxtRetention.Text = ""
                    End If


                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboPOType.SelectedValue, "J") = CompairStringResult.Equal Then
                            obj.WorkOrder_Vendor = lblWVendorName.Text
                            obj.WorkOrder_Vendor_Add = lblWAddress.Text
                            obj.WorkOrder_Vendor_Phn = lblWPhone.Text
                        End If
                    End If

                    obj.objPIRemittance = clsPIRemittance.Convert(objRemittance, dblPreviousTDSAmt)
                    obj.Arr = New List(Of clsPurchaseOrderDetail)


                    totalqty = 0
                    For Each grow As GridViewRowInfo In gv1.Rows
                        Dim objTr As New clsPurchaseOrderDetail()
                        objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.Row_Type = clsCommon.myCstr(grow.Cells(colRowType).Value)

                        ''richa agarwal 16/06/2016
                        If clsCommon.myLen(objTr.Row_Type) <= 0 Then
                            objTr.Row_Type = "Item"
                        End If
                        ''---------------------
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                        objTr.PurchaseOrder_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)

                        totalqty += objTr.PurchaseOrder_Qty

                        objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objTr.Requisition_Id = clsCommon.myCstr(grow.Cells(colReqistionNo).Value)
                        'objTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)
                        objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                        objTr.Last_Other_Vendor_Rate = clsCommon.myCdbl(grow.Cells(colLastRateOtherVendor).Value)
                        objTr.Last_Same_Vendor_Rate = clsCommon.myCdbl(grow.Cells(colLastRateSameVendor).Value)
                        objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)

                        objTr.Header_Discount_Per = clsCommon.myCdbl(grow.Cells(colHeaderDiscountPer).Value)
                        objTr.Header_Discount_Amount = clsCommon.myCdbl(grow.Cells(colHeaderDiscountAmt).Value)
                        objTr.Disc_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Value)
                        objTr.Detail_Discount_Amount = clsCommon.myCdbl(grow.Cells(colDetailDisAmt).Value)

                        objTr.Disc_Per_Unit = clsCommon.myCdbl(grow.Cells(colDisPerUnit).Value)
                        objTr.Disc_Amt_Per_Unit = clsCommon.myCdbl(grow.Cells(colDisAmtPerUnit).Value)

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
                        objTr.Specification = clsCommon.myCstr(grow.Cells(colSpecification).Value)
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objTr.Location = txtBillToLocation.Value 'clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                        objTr.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                        ''objTr.Assessable = clsCommon.myCdbl(grow.Cells(colAssessableRate).Value)
                        objTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
                        objTr.Bin_No = clsCommon.myCstr(grow.Cells(colBinNo).Value)

                        '' code for job work of type non inventory type auto calculation on
                        objTr.Qty_Desc = clsCommon.myCstr(grow.Cells(colQty_Desc).Value)
                        objTr.Rate_Desc = clsCommon.myCstr(grow.Cells(colRate_Desc).Value)
                        objTr.Amount_Desc = clsCommon.myCdbl(grow.Cells(colAmount_Desc).Value)
                        ''richa agarwal 07/04/2015
                        objTr.FatPer_MT = clsCommon.myCdbl(grow.Cells(colFatPerMT).Value)
                        objTr.SNFPer_MT = clsCommon.myCdbl(grow.Cells(colSNFFPerMT).Value)
                        objTr.FatKG_MT = clsCommon.myCdbl(grow.Cells(colFatKGMT).Value)
                        objTr.SNFKG_MT = clsCommon.myCdbl(grow.Cells(colSNFKGMT).Value)
                        If clsCommon.myLen(grow.Cells(colWeightUOMMT).Value) > 0 Then
                            objTr.Weight_UOM_MT = clsCommon.myCstr(grow.Cells(colWeightUOMMT).Value)
                        Else
                            objTr.Weight_UOM_MT = clsItemMaster.GetItemWeightUnit(objTr.Item_Code, Nothing)
                        End If
                        If clsCommon.myCdbl(grow.Cells(colItemWeightMT).Value) > 0 Then
                            objTr.Item_Weight_MT = clsCommon.myCstr(grow.Cells(colItemWeightMT).Value)
                        Else
                            objTr.Item_Weight_MT = clsItemMaster.GetItemWeightValue(objTr.Item_Code, Nothing)
                        End If

                        ''---------------
                        If IsAbatementPO Then
                            objTr.AbatementRate = clsCommon.myCdbl(grow.Cells(colAbatementRate).Value)
                            objTr.AssessableMRP = clsCommon.myCdbl(grow.Cells(colAssesableMRP).Value)
                            objTr.TotalAssessableMRP = clsCommon.myCdbl(grow.Cells(colTotalAssesableMRP).Value)
                        End If



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
                        '=====Sanjeet(22/12/2016)===========================
                        objTr.Capacity = clsCommon.myCstr(grow.Cells(colCapacity).Value)
                        objTr.Make = clsCommon.myCstr(grow.Cells(colMake).Value)
                        objTr.Model = clsCommon.myCstr(grow.Cells(colModel).Value)
                        '======================================================
                        objTr.Against_Item_Wise_Tax_Rate = clsCommon.myCstr(grow.Cells(colAgainstItemWiseTaxCode).Value)


                        objTr.Insurance_Base_Amt = clsCommon.myCdbl(grow.Cells(colInsuranceBaseAmt).Value)
                        objTr.Insurance_Per = clsCommon.myCdbl(grow.Cells(colInsurancePer).Value)

                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                    Next
                    'Parteek added these functionality only for UDL regarding Work Order 18/09/2017
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboPOType.SelectedValue, "J") = CompairStringResult.Equal) Then
                    Else
                        If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                            clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                            Return False
                        End If
                    End If

                    ''End


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
                        If clsCommon.myLen(txtApplicableFrom.Text) > 0 Then
                            obj.ApplicableFrom = Me.txtApplicableFrom.Text
                            'obj.ApplicableFrom = clsCommon.GetPrintDate(Me.txtApplicableFrom.Text, "dd/MM/yyyy")
                        Else
                            obj.ApplicableFrom = Nothing
                        End If

                    Else
                        obj.CURRENCY_CODE = Nothing
                        obj.ConvRate = 1
                        obj.ApplicableFrom = Nothing
                    End If
                    '' end CurrencyConversion

                    If clsCommon.CompairString(MyLabel7.Text, "Entered Purchase Order") = CompairStringResult.Equal Then
                        obj.Auto_PO = "0"
                    ElseIf clsCommon.CompairString(MyLabel7.Text, "Entered Purchase Order") <> CompairStringResult.Equal Then
                        obj.Auto_PO = "1"
                    End If
                    obj.Is_Repair = (chkRepair.Checked AndAlso chkRepair.Visible)
                    '----------------detail of roadpermit------------------------
                    If Chkroadpermit.Checked Then
                        obj.Arr_Road = New List(Of clsPurchaseOrderRoadDetail)
                        For Each grow As GridViewRowInfo In gv_roadpermit.Rows
                            Dim objtr As New clsPurchaseOrderRoadDetail()
                            objtr.roadpono = clsCommon.myCstr(txtDocNo.Value)
                            objtr.roadcode = clsCommon.myCstr(grow.Cells(colroadformcode).Value)
                            objtr.roadvendor = clsCommon.myCstr(txtVendorNo.Value)
                            objtr.roadissue_no = clsCommon.myCstr(grow.Cells(colroadformserialno).Value)
                            objtr.RoadpermitSRNNO = ""

                            If clsCommon.myLen(objtr.roadcode) > 0 Then
                                obj.Arr_Road.Add(objtr)
                            End If
                        Next
                    End If
                    '---------------detail of cform-----------------------------
                    If chk_c_form.Checked Then
                        obj.Arr_CFORM = New List(Of clsPurchaseOrderCFORMDetail)
                        For Each grow As GridViewRowInfo In gv_c_form.Rows
                            Dim objtr As New clsPurchaseOrderCFORMDetail()
                            objtr.cformpono = clsCommon.myCstr(txtDocNo.Value)
                            objtr.cformcode = clsCommon.myCstr(grow.Cells(colCFormformcode).Value)
                            objtr.cformvendor = clsCommon.myCstr(txtVendorNo.Value)
                            objtr.cformissue_no = clsCommon.myCstr(grow.Cells(colCFormformserialno).Value)
                            objtr.cformSRNNO = ""

                            If clsCommon.myLen(objtr.cformcode) > 0 Then
                                obj.Arr_CFORM.Add(objtr)
                            End If
                        Next
                    End If
                    '---------------------------------------------------------------
                    '' added functionality by Parteek 18/09/2017

                    obj.Arr_FieldCategory = New List(Of clsPurchaseOrderRoadDetail)
                    For Each grow As GridViewRowInfo In gvCategoryValue.Rows
                        Dim objtr As New clsPurchaseOrderRoadDetail()

                        objtr.FieldName = clsCommon.myCstr(grow.Cells(0).Value)
                        objtr.FieldDesc = clsCommon.myCstr(grow.Cells(1).Value)

                        If clsCommon.myLen(objtr.FieldName) > 0 Then
                            obj.Arr_FieldCategory.Add(objtr)
                        End If
                    Next

                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboPOType.SelectedValue, "J") = CompairStringResult.Equal Then
                            'If (obj.Arr_FieldCategory Is Nothing OrElse obj.Arr_FieldCategory.Count <= 0) Then
                            '    clsCommon.MyMessageBoxShow(Me,"Please Fill at list one Item for Work order")
                            '    Return False
                            'End If
                        End If

                    End If

                    obj.Arr_Terms_C = New List(Of clsPurchaseOrderRoadDetail)
                    For Each grow As GridViewRowInfo In gvTermsCdtion.Rows
                        Dim objtr As New clsPurchaseOrderRoadDetail()
                        objtr.Terms_C = clsCommon.myCstr(grow.Cells(colTerms).Value)
                        If clsCommon.myLen(objtr.Terms_C) > 0 Then
                            obj.Arr_Terms_C.Add(objtr)
                        End If
                    Next
                    '' End

                    obj.Arr_ACInsurance = New List(Of clsPurchaseOrderAdditionChargeInsurance)
                    For Each grow As GridViewRowInfo In gvACInsurance.Rows
                        Dim objtr As New clsPurchaseOrderAdditionChargeInsurance()
                        objtr.AC_Code = clsCommon.myCstr(grow.Cells(colACInsuranceCode).Value)
                        objtr.Amount = clsCommon.myCdbl(grow.Cells(colACInsuranceAmount).Value)
                        If clsCommon.myLen(objtr.AC_Code) > 0 Then
                            obj.Arr_ACInsurance.Add(objtr)
                        End If
                    Next

                    obj.ArrSchedule = New List(Of clsTenderSchedulePO)
                    For Each grow As GridViewRowInfo In gvSchedule.Rows
                        Dim objTr As New clsTenderSchedulePO()
                        objTr.SNo = clsCommon.myCDecimal(grow.Cells(colScheduleSNo).Value)
                        objTr.PSNo = clsCommon.myCDecimal(grow.Cells(colScheduleParentSNo).Value)
                        objTr.Schedule_No = clsCommon.myCDecimal(grow.Cells(colScheduleNo).Value)
                        objTr.From_Date = clsCommon.myCDate(grow.Cells(colScheduleFromDate).Value)
                        objTr.To_Date = clsCommon.myCDate(grow.Cells(colScheduleToDate).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colScheduleICode).Value)
                        objTr.Schedule_Qty_Per = clsCommon.myCDecimal(grow.Cells(colScheduleQtyPer).Value)
                        objTr.Schedule_Qty = clsCommon.myCDecimal(grow.Cells(colScheduleQty).Value)
                        objTr.Schedule_Short_Per = clsCommon.myCDecimal(grow.Cells(colScheduleShortPer).Value)
                        objTr.Schedule_Short = clsCommon.myCDecimal(grow.Cells(colScheduleShort).Value)
                        objTr.Late_Days = clsCommon.myCDecimal(grow.Cells(colScheduleLateDays).Value)
                        objTr.Extension_Days = clsCommon.myCDecimal(grow.Cells(colScheduleExtensionDays).Value)
                        objTr.Arr = TryCast(grow.Cells(colScheduleLateDays).Tag, List(Of clsTenderSchedulePeneltyPO))
                        obj.ArrSchedule.Add(objTr)
                    Next

                    Dim isSaved As Boolean = True
                    If dtpRenewal.Checked AndAlso dtpRenewal.Enabled = True Then
                        '===if document is renewed,then first time it is enabled and 2nd time disabled,so first time create new document,otherwise not
                        isSaved = isSaved AndAlso PORenewal(obj)
                    Else
                        isSaved = isSaved AndAlso obj.SaveData(obj, isNewEntry, isDoAbandomentNo)
                    End If
                    txtDocNo.Value = obj.PurchaseOrder_No
                    UcAttachment1.SaveData(txtDocNo.Value)
                    If obj.Auto_PO = "1" Then
                        frmSRN.IsPoSavedAuto = True
                        frmSRN.txtPONo.Value = txtDocNo.Value
                        AddNew()
                        Dim objPO As New clsPurchaseOrderHead
                        objPO.PostData(MyBase.Form_ID, txtDocNo.Value, True, False)
                        objPO = Nothing
                    Else
                        frmSRN.IsPoSavedAuto = False
                        ''BM00000008148 approval work 20/10/2015
                        Dim xNewDesc As String = ""
                        xNewDesc = "Party Name : " + obj.Vendor_Name
                        ''=====================capex cond==============
                        If ShowCapexCodeandSubCode Then
                            xNewDesc = xNewDesc + Environment.NewLine + "Type : " + ddl_category.SelectedValue
                            If chk_emergency.Checked Then
                                xNewDesc = xNewDesc + "       Sub-Type : Emergency"
                            End If
                        End If
                        xNewDesc = xNewDesc + Environment.NewLine + "Description : " + obj.Description
                        clsApply_Approval.CheckApprovalRequired(txtBillToLocation.Value, clsCommon.myCstr(ddl_category.SelectedValue), MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value), txtDate.Text, clsCommon.myCstr(xNewDesc), clsCommon.myCstr(txtRemarks.Text), clsCommon.myCdbl(lblTotRAmt.Text), clsCommon.myCdbl(totalqty), txtDept.Value, Nothing, 0, isAmendment)
                        '================================================================

                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                    '----------------------------------------------------

                    Return isSaved
                End If
        Catch ex As Exception
            frmSRN.IsPoSavedAuto = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            dtpRenewal.Enabled = True
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnAmendment.Enabled = False
            btnDelete.Enabled = True
            isInsideLoadData = True
            cboPOType.Enabled = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()
            LoadBlankGridSchedule()
            LoadBlankGridTax()
            LoadBlankGridAC()
            LoadBlankGridACInsurance()
            IsLoadOk = True
            cboItemType.Enabled = True
            txtBillToLocation.Enabled = True
            ShowPOCancelButton = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCancelButtonPO, clsFixedParameterCode.ShowCancelButtonPO, Nothing)) = "1", True, False))
            If ShowPOCancelButton Then
                btn_cancel.Visible = True
            Else
                btn_cancel.Visible = False
            End If
            Dim obj As New clsPurchaseOrderHead()
            obj = clsPurchaseOrderHead.GetData(strCode, NavTyep, arrLoc, IIf(clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmPurchaseOrderMT) = CompairStringResult.Equal, "MT", "PO"), FORMTYPE)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PurchaseOrder_No) > 0) Then
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    TxtRetention.ReadOnly = True
                    btnAmendment.Enabled = True
                    If ShowPOCancelButton AndAlso Not clsPurchaseOrderHead.CheckPOUsedInSRNorGRN(clsCommon.myCstr(obj.PurchaseOrder_No), Nothing) Then
                        btn_cancel.Visible = True
                    Else
                        btn_cancel.Visible = False
                    End If
                    repoComplete.IsVisible = True
                    'repoBalQty.IsVisible = True
                End If

                If CInt(obj.IsCancel) = CInt(1) Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btn_cancel.Visible = False
                    btnAmendment.Enabled = False
                End If

                cboPOType.Enabled = False
                cboItemType.Enabled = False
                txtBillToLocation.Enabled = False
                chkOpenPO.Checked = clsCommon.myCBool(IIf(obj.Is_Open_PO = 1, True, False))
                dtpRenewal.Checked = False
                dtpRenewal.Text = Nothing
                If clsCommon.myLen(obj.Renewal_Date) > 0 Then
                    dtpRenewal.Checked = True
                    dtpRenewal.Text = obj.Renewal_Date
                    dtpRenewal.Enabled = False
                End If
                txtAgainstPO_No.Text = obj.Against_PO
                'stutti
                txt_deliverydays.Text = obj.Deliverydays
                chk_emergency.Checked = clsCommon.myCBool(IIf(obj.Emergency = 1, True, False))
                If clsCommon.CompairString(clsCommon.myCstr(obj.Category), "") = CompairStringResult.Equal Then
                    ddl_category.SelectedValue = clsCommon.myCstr("")
                Else
                    ddl_category.SelectedValue = clsCommon.myCstr(obj.Category)
                End If
                chkTender.Checked = IIf(obj.Against_Tender = "Y", True, False)
                fndcapexcode.Value = obj.Capex_Code
                If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                    lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
                End If
                fndcapexsubcode.Value = obj.Capex_SubCode
                If clsCommon.myLen(Me.fndcapexsubcode.Value) > 0 Then
                    lbl_capexsubcode.Text = clsCapexBudget.GetName(Me.fndcapexsubcode.Value, Nothing)
                    lbl_budgetamt.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                    lbl_budgetamtwithtolerence.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                    lbl_rebudgetamt.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, obj.PurchaseOrder_No, Nothing)
                    lbl_rebudgetamtwithtolerence.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, obj.PurchaseOrder_No, Nothing)
                End If
                '================end here=================
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.PurchaseOrder_No
                txtDate.Value = obj.PurchaseOrder_Date
                If clsCommon.myLen(obj.Delivery_date) > 0 Then
                    txtDeliveryDate.Value = obj.Delivery_date
                End If

                '----------------------------------------------
                If obj.Auto_PO = "1" Then
                    MyLabel7.Text = "System Generated Purchase Order"
                Else
                    MyLabel7.Text = "Entered Purchase Order"
                End If

                If obj.roadpermit = "1" Then
                    Chkroadpermit.Checked = True
                Else
                    Chkroadpermit.Checked = False
                End If

                If obj.Cform = "1" Then
                    chk_c_form.Checked = True
                Else
                    chk_c_form.Checked = False
                End If
                '-------------------------------------------------
                txtReferencePO.Text = obj.ReferencePO
                lblVendorQuotationNo.Text = obj.Against_Vendor_Quotation
                txtPaymentTerm.Text = obj.Payment_Terms
                txtInsuranceTerms.Text = obj.Insurance_Terms
                txtDelivery_Code.Value = obj.Delivery_Terms_Code
                txtDeliveryDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_DELIVERY_TERMS_MASTER where code='" + txtDelivery_Code.Value + "'"))
                txtDeliveryDuration.Text = obj.Delivery_Duration
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                txtRefNo.Text = obj.Ref_No
                cboPOType.SelectedValue = obj.PurchaseOrder_Type
                chkAgainst_RGP.Checked = obj.Against_RGP
                chkOnHold.Checked = obj.On_Hold
                txtDesc.Text = obj.Description
                txtTaxGroup.Value = obj.Tax_Group
                txtRGPNo.Value = obj.Against_RGP_NO
                txtComment.Rtf = obj.Comments
                txtCmt1.Rtf = obj.Comment1
                txtCmt2.Rtf = obj.Comment2
                txtCmt3.Rtf = obj.Comment3
                txtCmt4.Rtf = obj.Comment4
                txtCmt5.Rtf = obj.Comment5
                txtCmt6.Rtf = obj.Comment6
                txtCmt7.Rtf = obj.Comment7
                txtCmt8.Rtf = obj.Comment8
                txtCmt9.Rtf = obj.Comment9
                txtCmt10.Rtf = obj.Comment10
                txtCmt11.Rtf = obj.Comment11
                txtCmt12.Rtf = obj.Comment12
                txtCmt13.Rtf = obj.Comment13
                'txtComment.Text = obj.Comment14
                'RTComment.Rtf = obj.Comments
                txtShipToLocation.Value = obj.Ship_To_Location
                txtBillToLocation.Value = obj.Bill_To_Location
                txtSubLocation.Value = obj.Sublocation_Code
                TxtRetention.Text = obj.Retention
                chkJobWorkOutward.Checked = obj.isJobWorkOutward
                If chkJobWorkOutward.Checked = True Then
                    txtSubLocation.Enabled = False

                End If
                chkJobWorkOutward.Enabled = False
                lblSubLocation.Text = obj.SubLocationName
                txtRemarks.Text = obj.Remarks
                txtSubject.Text = obj.Subject

                txtContentSubject.Text = obj.Content_Subject
                txtKindAttentation.Text = obj.Kind_Attentation
                chkMCCPurchase.Checked = IIf(obj.MCC_Purchase = 1, True, False)
                fndState.Value = obj.State_Code
                chkBlanket.Checked = If(obj.isBlanket = 1, True, False)
                ChkISPO.Checked = If(obj.IsPO = 1, True, False)
                chkIsContent.Checked = If(obj.IsContent = 1, True, False)
                'If clsCommon.CompairString(cboPOType.SelectedValue, "B") = CompairStringResult.Equal Then
                '    lblAmt.Visible = True
                '    txtAmount.Text = clsCommon.myCdbl(obj.PO_Amount)
                '    txtAmount.Visible = True
                'End If
                txtAmount.Text = clsCommon.myCdbl(obj.PO_Amount)
                lblConfirmatory_PO_SRN_No.Text = obj.Confirmatory_PO_SRN_No
                If ShowItemAllStructureWise = False Then
                    cboItemType.SelectedValue = obj.Item_Type
                Else
                    LoadItemType()
                End If




                'txtAmount.Visible = True
                If Not String.IsNullOrEmpty(fndState.Value) Then
                    lblStateName.Text = clsDBFuncationality.getSingleValue("select State_Name from TSPL_STATE_MASTER where STATE_CODE='" + fndState.Value + "'")
                End If
                cboModeOfTransport.Text = obj.Mode_Of_Transport
                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If


                txtDept.Value = obj.Dept
                lblDept.Text = obj.Dept_Desc

                txtTermCode.Value = obj.Terms_Code
                txtTermRemark.Text = obj.Terms_Remark
                'lblTermName.Text = obj.Terms_Description
                If clsCommon.myLen(obj.Due_Date) > 0 Then
                    txtDueDate.Value = obj.Due_Date
                End If

                lblAmtWithDiscount.Text = clsCommon.myFormat(obj.Discount_Base)
                txtHeaderDiscountAmount.Value = clsCommon.myFormat(obj.Header_Discount_Amount)
                lblDiscountAmt.Text = clsCommon.myFormat(obj.Discount_Amt)
                lblAmtAfterDiscount.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
                lblTaxableAmount.Text = clsCommon.myFormat(obj.Total_Taxable_Amount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                lblTotRAmt.Text = clsCommon.myFormat(obj.PO_Total_Amt)
                lblTotRAmtCopy.Text = clsCommon.myFormat(obj.PO_Total_Amt)

                lblTaxGrpName.Text = obj.TaxGroupName
                lblTermName.Text = obj.TermsName

                If obj.close_yn = "Y" Then
                    vaddnew = "Y"
                    chkpoclose.Checked = True
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btnAmendment.Enabled = False
                    vaddnew = "N"
                ElseIf obj.close_yn = "N" Then
                    chkpoclose.Checked = False
                    vaddnew = "N"
                End If


                If clsCommon.myLen(obj.Expiry_Date) > 0 Then
                    dtpExpiryDate.Checked = True
                    dtpExpiryDate.Value = obj.Expiry_Date
                End If

                fndProject.Value = obj.PROJECT_ID
                lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")

                If obj.Abandonment_No > 0 Then
                    lblAbandonmentNo.Text = clsCommon.myCstr(obj.Abandonment_No)
                    lblAmbendmentNoCaption.Visible = True
                End If
                If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.WorkOrderEng) = CompairStringResult.Equal Then
                    txtReqNo.Value = obj.Against_WorkEstimation_Id
                Else
                    txtReqNo.Value = obj.Against_Requisition
                End If
                If clsCommon.myLen(txtReqNo.Value) > 0 Then
                    lblProject.Enabled = False
                    fndProject.Enabled = False
                    ddl_category.Enabled = False
                End If
                ''richa agarwal 24/12/2014
                If obj.MT_Is_Merchant_Trade = 1 Then
                    chkIsMerchantTrade.Checked = True
                Else
                    chkIsMerchantTrade.Checked = False
                End If
                txtPINo.Value = clsCommon.myCstr(obj.MT_PI_No)
                cboPIStatus.SelectedValue = clsCommon.myCstr(obj.MT_PI_Status)

                If clsCommon.myLen(obj.MT_PI_Status_Date) > 0 Then
                    txtPIDate.Checked = True
                    txtPIDate.Value = obj.MT_PI_Status_Date
                End If
                TxtBuyerPONo.Text = clsCommon.myCstr(obj.MT_Buyer_PO_No)

                If clsCommon.myLen(obj.MT_Buyer_PO_Date) > 0 Then
                    TxtBuyerPODate.Checked = True
                    TxtBuyerPODate.Value = obj.MT_Buyer_PO_Date
                End If
                TxtHSClassificationNo.Text = clsCommon.myCstr(obj.MT_HS_Classification_No)
                fndPaymenttermsGroup.Value = clsCommon.myCstr(obj.MT_Payment_Terms_Group_Code)
                lblpaymenttermsgroup.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT where Group_Code='" & fndPaymenttermsGroup.Value & "'"))
                MTEnableDisablepaymentterms()
                If obj.MT_Is_AmountinRs = 1 Then
                    rdbAmountinrupees.Checked = True
                Else
                    rdbAmountinpercentage.Checked = True
                End If
                TxtLC.Value = clsCommon.myCdbl(obj.MT_LC)
                TxtCAD.Value = clsCommon.myCdbl(obj.MT_CAD)
                TxtOnAccount.Value = clsCommon.myCdbl(obj.MT_On_Account)
                TxtBalancePayment.Value = clsCommon.myCdbl(obj.MT_Balance_Payment)
                txtRetained.Value = clsCommon.myCdbl(obj.MT_RETAINED)
                ''richa agarwal 09/04/2015
                TxtCIF.Value = clsCommon.myCdbl(obj.MT_CIF)
                ''------------------
                txtAdvance.Value = clsCommon.myCdbl(obj.MT_Advance)
                TxtBeneficiary.Value = clsCommon.myCstr(obj.MT_Beneficiary_Code)
                lblBeneficiary.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code ='" & TxtBeneficiary.Value & "'"))
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name

                chkAcceptance.Checked = clsCommon.myCBool(IIf(obj.MT_is_Accepted = "Y", True, False))
                If clsCommon.myLen(obj.MT_Accepted_Date) > 0 Then
                    dtpAcceptance.Value = clsCommon.myCDate(obj.MT_Accepted_Date)
                Else
                    dtpAcceptance.Value = clsCommon.GETSERVERDATE(Nothing)
                End If
                chkPartshipment.Checked = clsCommon.myCBool(IIf(obj.MT_is_Partshipment = "Y", True, False))
                chkTransshipment.Checked = clsCommon.myCBool(IIf(obj.MT_is_Transshipment = "Y", True, False))
                cmbTerms.SelectedValue = obj.MT_EX_Term_Code
                cmbTerms_Payment.SelectedValue = obj.MT_Payment_Terms
                cboStuffing.SelectedValue = obj.MT_Stuffing_Status
                cmbAdvanceType.SelectedValue = obj.MT_Advance_Type
                ChkPartPayment.Checked = clsCommon.myCBool(IIf(obj.MT_is_Partpayment = "Y", True, False))
                txtAdvance_Pers.Value = clsCommon.myCdbl(obj.MT_PT_Advance_Amount)
                If clsCommon.myLen(obj.MT_PI_Due_Date) > 0 Then
                    txtPIDueDate.Value = clsCommon.myCDate(obj.MT_PI_Due_Date)
                Else
                    txtPIDueDate.Value = clsCommon.GETSERVERDATE(Nothing)
                End If
                FndCreditTerms.Value = obj.MT_CreditTerms_Code
                TxtCreditTermsName.Text = obj.MT_CreditTermsName
                txtPre_Carriage_By.SelectedValue = obj.MT_Pre_Carriage_By
                txtPort_Discharge.Text = obj.MT_Discharge_Port
                txtFinal_Destination.Text = obj.MT_Final_Destination
                fndCountry_Final_Destination.Value = obj.MT_Final_Destination_Country
                fndCountry_Origin.Value = obj.MT_Origin_Country

                txtBillNo.Text = obj.ServiceBill_No
                dtBillDate.Value = clsCommon.myCDate(obj.ServiceBill_Date)
                If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.WorkOrderEng) = CompairStringResult.Equal Then
                    txtRefTendorNo.Text = obj.RefTendorNo
                Else
                    txtTenderNo.Value = obj.RefTendorNo
                    txtTenderNo.Tag = clsTenderHead.GetTenderType(txtTenderNo.Value, Nothing)
                End If



                txtCarrier.Text = obj.MT_Carrier
                If clsCommon.myLen(txtPINo.Value) > 0 Then
                    chkAcceptance.Enabled = False
                    dtpAcceptance.Enabled = False
                    chkPartshipment.Enabled = False
                    chkTransshipment.Enabled = False
                    cmbTerms.Enabled = False
                    cmbTerms_Payment.Enabled = False
                    cboStuffing.Enabled = False
                    txtPIDueDate.Enabled = False
                    FndCreditTerms.Enabled = False
                    TxtCreditTermsName.Enabled = False
                    txtPre_Carriage_By.Enabled = False
                    txtPort_Discharge.Enabled = False
                    txtFinal_Destination.Enabled = False
                    fndCountry_Final_Destination.Enabled = False
                    fndCountry_Origin.Enabled = False
                    txtCarrier.Enabled = False
                    fndPaymenttermsGroup.Enabled = False
                    'TxtBuyerPODate.Enabled = False
                    'TxtBuyerPONo.Enabled = False
                End If
                ''-----------------------
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


                lblAmtAfterTax.Text = clsCommon.myFormat(obj.Amt_After_Tax)

                If (clsCommon.myLen(obj.Add_Charge_Code1) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACApplyOn).Value = obj.Add_Charge_Apply_On1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACPer).Value = obj.Add_Charge_Per1
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code2) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACApplyOn).Value = obj.Add_Charge_Apply_On2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACPer).Value = obj.Add_Charge_Per2
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code3) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACApplyOn).Value = obj.Add_Charge_Apply_On3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACPer).Value = obj.Add_Charge_Per3
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code4) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACApplyOn).Value = obj.Add_Charge_Apply_On4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACPer).Value = obj.Add_Charge_Per4
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code5) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACApplyOn).Value = obj.Add_Charge_Apply_On5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACPer).Value = obj.Add_Charge_Per5
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code6) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACApplyOn).Value = obj.Add_Charge_Apply_On6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACPer).Value = obj.Add_Charge_Per6
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code7) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACApplyOn).Value = obj.Add_Charge_Apply_On7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACPer).Value = obj.Add_Charge_Per7
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code8) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACApplyOn).Value = obj.Add_Charge_Apply_On8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACPer).Value = obj.Add_Charge_Per8
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code9) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACApplyOn).Value = obj.Add_Charge_Apply_On9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACPer).Value = obj.Add_Charge_Per9
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code10) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACApplyOn).Value = obj.Add_Charge_Apply_On10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACPer).Value = obj.Add_Charge_Per10
                End If

                lblAddCharges.Text = clsCommon.myFormat(obj.Total_Add_Charge)
                lblAddCharges1.Text = clsCommon.myFormat(obj.Total_Add_Charge)

                lblAddChargesForInsurance.Text = clsCommon.myFormat(obj.Total_Add_Charge_Insurance)
                lblAddChargesForInsurance1.Text = clsCommon.myFormat(obj.Total_Add_Charge_Insurance)
                lblTotalInsuranceAmt.Text = clsCommon.myFormat(obj.Total_Item_Insurance_Amt)

                txtInsurance.Text = obj.Insurance
                txtPackingForward.Text = obj.Packing_Forward
                txtFreight.Text = obj.Freight
                TxtRetention.Text = obj.Retention

                If obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                    rbtnTaxCalAutomatic.IsChecked = True
                    gv2.Columns(colTTaxRate).IsVisible = False
                ElseIf obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                    rbtnTaxCalManual.IsChecked = True
                    gv2.Columns(colTTaxRate).IsVisible = True
                End If

                txtQuotationNo.Text = obj.Quotation_No
                If obj.Quotation_Date.HasValue Then
                    txtQuotationDate.Value = obj.Quotation_Date
                End If
                chkExciseOnQty.Checked = obj.is_Excise_On_Qty
                chkAutoCalculate.Checked = obj.Auto_Calculate
                chkReceiveControl.Checked = obj.Apply_Receive_Control
                txtBankCode.Value = obj.Bank_Code
                lblBankDesc.Text = connectSql.RunScalar("select description from TSPL_BANK_MASTER where bank_code = '" + txtBankCode.Value + "'")
                txtPaymentMode.Value = obj.Payment_Code
                chkRepair.Checked = obj.Is_Repair
                objRemittance = clsRemittance.Convert(obj.objPIRemittance, dblPreviousTDSAmt)
                If objRemittance IsNot Nothing Then
                    btnViewTDSDetails.Enabled = True
                End If
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsPurchaseOrderDetail In obj.Arr

                        'If objCommonVar.RCDFCFP = True Then
                        '    If chkTender.Checked = False AndAlso objTr.PurchaseOrder_Qty <= 0 Then
                        '        Continue For
                        '    End If
                        'End If

                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Row_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComplete).Value = IIf(objTr.Status = 0, "No", "Yes")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(objTr.Item_Code, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsInsurance).Value = clsAdditionalCharge.GetIsInsurance(objTr.Item_Code, Nothing)
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objTr.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgRequitionQty).Value = objTr.OriginalReqQty
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = clsCommon.myCdbl(clsPurchaseOrderHead.GetBalanceQty(clsCommon.myCstr(obj.PurchaseOrder_No), clsCommon.myCstr(objTr.Item_Code)))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.PurchaseOrder_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReqistionNo).Value = objTr.Requisition_Id
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Location
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.LocationName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountPer).Value = objTr.Header_Discount_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountAmt).Value = objTr.Header_Discount_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = objTr.Disc_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDetailDisAmt).Value = objTr.Detail_Discount_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmt).Value = objTr.Disc_Amt

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPerUnit).Value = objTr.Disc_Per_Unit
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmtPerUnit).Value = objTr.Disc_Amt_Per_Unit

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

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        '====Sanjeet(22/12/2016)==========
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapacity).Value = objTr.Capacity
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMake).Value = objTr.Make
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colModel).Value = objTr.Model
                        '=================================
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemUsedINGRN).Value = objTr.IsUsedInGRN
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.MRP
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(objTr.Item_Code)
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableRate).Value = objTr.Assessable
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableAmount).Value = objTr.AssessableAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = objTr.Bin_No

                        If clsCommon.myLen(objTr.Requisition_Id) > 0 Then

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsRequistionDetail.GetBalanceRequitionQty(objTr.Requisition_Id, objTr.Item_Code, obj.PurchaseOrder_No, "", SettingIndendFreePOClose)
                        End If
                        '' abatement PO
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementRate).Value = objTr.AbatementRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssesableMRP).Value = objTr.AssessableMRP
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssesableMRP).Value = objTr.TotalAssessableMRP
                        ' code for po type job work of non inventory type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty_Desc).Value = objTr.Qty_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate_Desc).Value = objTr.Rate_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount_Desc).Value = objTr.Amount_Desc
                        ''richa agarwal 07/04/2015
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatPerMT).Value = objTr.FatPer_MT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFFPerMT).Value = objTr.SNFPer_MT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatKGMT).Value = objTr.FatKG_MT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKGMT).Value = objTr.SNFKG_MT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colWeightUOMMT).Value = objTr.Weight_UOM_MT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemWeightMT).Value = objTr.Item_Weight_MT
                        ''---------------
                        GetLastRateItemWise(objTr.Item_Code, gv1.Rows.Count - 1)

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

                    Next
                    txtBillToLocation.Value = obj.Bill_To_Location
                    '-------------------------RoadPermit grid fill=----------------------------
                    gv_roadpermit.Rows.Clear()
                    gv_c_form.Rows.Clear()
                    gv_roadpermit.Rows.AddNew()
                    If obj.roadpermit = "1" AndAlso obj.Arr_Road IsNot Nothing AndAlso obj.Arr_Road.Count > 0 Then
                        For Each objtr As clsPurchaseOrderRoadDetail In obj.Arr_Road
                            gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colRoadsno).Value = clsCommon.myCstr(gv_roadpermit.Rows.Count)
                            gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colroadformcode).Value = clsCommon.myCstr(objtr.roadcode)
                            gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colroadformdesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select form_name from tspl_form_master where form_code='" + objtr.roadcode + "'"))
                            gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colroadformserialno).Value = clsCommon.myCstr(objtr.roadissue_no)
                            gv_roadpermit.Rows(gv_roadpermit.Rows.Count - 1).Cells(colroadformrem).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select remarks from tspl_form_master where form_code='" + objtr.roadcode + "'"))
                            gv_roadpermit.Rows.AddNew()
                        Next
                    End If
                    '--------------CFORM grid fill------------------------------------------
                    gv_c_form.Rows.AddNew()
                    If obj.Cform = "1" AndAlso obj.Arr_CFORM IsNot Nothing AndAlso obj.Arr_CFORM.Count > 0 Then
                        For Each objtr As clsPurchaseOrderCFORMDetail In obj.Arr_CFORM
                            gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormsno).Value = clsCommon.myCstr(gv_c_form.Rows.Count)
                            gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormformcode).Value = clsCommon.myCstr(objtr.cformcode)
                            gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormformdesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select form_name from tspl_form_master where form_code='" + objtr.cformcode + "'"))
                            gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormformserialno).Value = clsCommon.myCstr(objtr.cformissue_no)
                            gv_c_form.Rows(gv_c_form.Rows.Count - 1).Cells(colCFormformrem).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select remarks from tspl_form_master where form_code='" + objtr.cformcode + "'"))
                            gv_c_form.Rows.AddNew()
                        Next
                    End If
                    '----------------------------------------------------------------------



                    btnForm_Update.Enabled = False
                    lblBillToLocation.Text = obj.BillToLocationName
                    lblShipToLocation.Text = obj.ShipToLocationName
                    If obj.Status = ERPTransactionStatus.Approved Then
                        btnForm_Update.Enabled = True
                        ''richa agarwal 08/04/2015
                        txtVendorNo.Enabled = False
                        cboPOType.Enabled = False
                        cboItemType.Enabled = False
                        txtPINo.Enabled = False
                        TxtBeneficiary.Enabled = False
                        txtBillToLocation.Enabled = False
                        ''------------------
                    End If
                    If obj.Status = ERPTransactionStatus.Pending OrElse obj.Status = ERPTransactionStatus.Approved Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                        gvAC.Rows.AddNew()
                        gvACInsurance.Rows.AddNew()
                    End If
                End If
                ' ==== added by Parteek 19/08/2017
                LoadGridWork()

                If obj.Arr_FieldCategory IsNot Nothing AndAlso obj.Arr_FieldCategory.Count > 0 Then
                    For Each objtr As clsPurchaseOrderRoadDetail In obj.Arr_FieldCategory
                        gvCategoryValue.Rows(gvCategoryValue.Rows.Count - 1).Cells("Field Name").Value = clsCommon.myCstr(objtr.FieldName)
                        gvCategoryValue.Rows(gvCategoryValue.Rows.Count - 1).Cells("Field Desc").Value = clsCommon.myCstr(objtr.FieldDesc)
                        gvCategoryValue.Rows.AddNew()
                        gvCategoryValue.BestFitColumns()
                    Next
                End If

                gvTermsCdtion.Rows.Clear()
                gvTermsCdtion.Rows.Clear()
                gvTermsCdtion.Rows.AddNew()

                If obj.Arr_Terms_C IsNot Nothing AndAlso obj.Arr_Terms_C.Count > 0 Then
                    For Each objtr As clsPurchaseOrderRoadDetail In obj.Arr_Terms_C
                        gvTermsCdtion.Rows(gvTermsCdtion.Rows.Count - 1).Cells(colTerms).Value = clsCommon.myCstr(objtr.Terms_C)
                        gvTermsCdtion.Rows.AddNew()
                    Next
                End If
                '======END

                If obj.Arr_ACInsurance IsNot Nothing AndAlso obj.Arr_ACInsurance.Count > 0 Then
                    For Each objtr As clsPurchaseOrderAdditionChargeInsurance In obj.Arr_ACInsurance
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceCode).Value = objtr.AC_Code
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceName).Value = objtr.AC_Name
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceAmount).Value = objtr.Amount
                        gvACInsurance.Rows.AddNew()
                    Next
                End If

                If obj.ArrSchedule IsNot Nothing AndAlso obj.ArrSchedule.Count > 0 Then
                    txtScheduleStartDate.Value = obj.ArrSchedule(0).From_Date
                    For Each objTr As clsTenderSchedulePO In obj.ArrSchedule
                        gvSchedule.Rows.AddNew()
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleSNo).Value = objTr.SNo
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleParentSNo).Value = objTr.PSNo
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleNo).Value = objTr.Schedule_No
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleFromDate).Value = objTr.From_Date
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleToDate).Value = objTr.To_Date
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleICode).Value = objTr.Item_Code
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleIName).Value = clsItemMaster.GetItemName(objTr.Item_Code, Nothing)
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleQtyPer).Value = objTr.Schedule_Qty_Per
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleQty).Value = objTr.Schedule_Qty
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleShortPer).Value = objTr.Schedule_Short_Per
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleShort).Value = objTr.Schedule_Short
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLateDays).Value = objTr.Late_Days
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleExtensionDays).Value = objTr.Extension_Days
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLateDays).Tag = objTr.Arr
                        If txtScheduleStartDate.Value > objTr.From_Date Then
                            txtScheduleStartDate.Value = objTr.From_Date
                        End If
                    Next
                End If

                SetitemWiseTaxOnlySetting()
                ''RefreshReqNo()

                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.PurchaseOrder_No)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.PurchaseOrder_No, MyBase.Form_ID, gv1)
                ''End of For Custom Fields

                '' MULTICURRENCY
                Me.txtCurrencyCode.Value = obj.CURRENCY_CODE
                Me.txtConversionRate.Text = obj.ConvRate
                Me.txtApplicableFrom.Text = obj.ApplicableFrom.ToString
                '' end  MULTICURRENCY
                UcAttachment1.LoadData(obj.PurchaseOrder_No)

            End If

            '=====================if document go for approval then no post button visible or if document contain related setting
            btnPost.Visible = MyBase.isPostFlag
            If Not clsApply_Approval.Visibility_PostButtonForApproval(txtBillToLocation.Value, clsCommon.myCstr(ddl_category.SelectedValue), MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value), clsCommon.myCdbl(lblTotRAmt.Text), 0, clsCommon.myCstr(txtDept.Value)) Then
                btnPost.Visible = False
                If UsLock1.Status = ERPTransactionStatus.Pending Then
                    UsLock1.Status = clsApply_Approval.ApprovalCondCheck_Doc(MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value), Nothing)
                End If
            End If
            '============================================
            If clsCommon.myLen(txtVendorNo.Value) > 0 Then

                If clsCommon.myLen(obj.WorkOrder_Vendor) <= 0 Then
                    lblWVendorName.Text = clsDBFuncationality.getSingleValue("select Vendor_Name from tspl_vendor_master where Vendor_Code='" & txtVendorNo.Value & "'")
                Else
                    lblWVendorName.Text = obj.WorkOrder_Vendor
                End If
                If clsCommon.myLen(obj.WorkOrder_Vendor_Add) <= 0 Then
                    lblWAddress.Text = clsDBFuncationality.getSingleValue("select TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_VENDOR_MASTER.City_Code_Desc)>0 then ', '+TSPL_VENDOR_MASTER.City_Code_Desc else ' ' end + case when len(TSPL_VENDOR_MASTER.State )>0 then TSPL_VENDOR_MASTER.State else '' end  as Address from tspl_vendor_master where Vendor_Code='" & txtVendorNo.Value & "'")
                Else
                    lblWAddress.Text = obj.WorkOrder_Vendor_Add
                End If
                If clsCommon.myLen(obj.WorkOrder_Vendor_Phn) <= 0 Then
                    lblWPhone.Text = clsDBFuncationality.getSingleValue("select Phone1 from tspl_vendor_master where Vendor_Code='" & txtVendorNo.Value & "'")
                Else
                    lblWPhone.Text = obj.WorkOrder_Vendor_Phn
                End If

            End If


            If gvAC.Rows.Count <= 0 Then
                gvAC.Rows.AddNew()
            End If
            If gvACInsurance.Rows.Count <= 0 Then
                gvACInsurance.Rows.AddNew()
            End If

            IsLoadOk = False
            FillVendorDetails()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()

    End Sub

    Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If rbtnTaxCalAutomatic.IsChecked AndAlso Not PurchaseModulePickFixTaxRate Then
                Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(IIf(clsCommon.myLen(clsCommon.myCstr(txtShipToLocation.Value)) <= 0, txtBillToLocation.Value, txtShipToLocation.Value), txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), txtVendorNo.Value, "P")
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

            If (myMessages.postConfirm()) Then
                If SavingData(True) Then
                    ''to check payment is required or not against PO
                    Dim dblCountOfAdvanceItem As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(item_Code) from TSPL_ITEM_MASTER where item_Code in (Select item_Code from TSPL_PURCHASE_ORDER_DETAIL where purchaseOrder_No ='" & clsCommon.myCstr(txtDocNo.Value) & "') and Is_Advance_Required =1"))
                    If dblCountOfAdvanceItem > 0 Then
                        Dim dtPayment As DataTable = clsDBFuncationality.GetDataTable("select Posted ,payment_no,Payment_Amount  from TSPL_PAYMENT_HEADER where PurchaseOrder_No ='" & clsCommon.myCstr(txtDocNo.Value) & "'")
                        If dtPayment IsNot Nothing AndAlso dtPayment.Rows.Count > 0 Then
                            If clsCommon.myLen(dtPayment.Rows(0)("payment_no")) > 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(dtPayment.Rows(0)("Posted")), "1") <> CompairStringResult.Equal Then
                                    Throw New Exception("Please Post payment entry " & dtPayment.Rows(0)("payment_no") & " before Posting of Purchase Order.")
                                ElseIf clsCommon.myCdbl(dtPayment.Rows(0)("Payment_Amount")) < clsCommon.myCdbl(lblTotRAmtCopy.Text) Then
                                    Throw New Exception("Payment entry amount should not be less than of Purchase Order amount.")
                                End If
                            Else
                                Throw New Exception("Please create payment entry before Posting of Purchase Order.")
                            End If
                        Else
                            Throw New Exception("Please create payment entry before Posting of Purchase Order.")
                        End If
                    End If


                    Dim objPO As New clsPurchaseOrderHead()
                    If (objPO.PostData(MyBase.Form_ID, txtDocNo.Value, True, Schedule_ON, arrLoc)) Then ''pass schedule value for creating auto schedule
                        msg = "Successfully Posted"
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
                    objPO = Nothing
                    If clsCommon.myLen(msg) > 0 Then
                        clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                    End If
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub


    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value))
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
                If (clsPurchaseOrderHead.DeleteData(txtDocNo.Value, IIf(clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmPurchaseOrderMT) = CompairStringResult.Equal, "MT", "PO"))) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

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
            vaddnew = "Y"
            Dim qst As String = "select count(*) from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No='" + txtDocNo.Value + "'"
            'If clsCommon.CompairString(clsUserMgtCode.mbtnPurchaseOrder, FORMTYPE) = CompairStringResult.Equal Then
            '    qst += " and TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade=0 "
            'Else
            '    qst += " and TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade=1 "
            'End If
            qst += " and TSPL_PURCHASE_ORDER_HEAD.From_Screen_Code='" + FORMTYPE + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            vaddnew = "N"
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        vaddnew = "Y"
        '===================update by preeti gupta [Add column created by for Jakson Clinet]
        Dim qry As String = "select PurchaseOrder_No as PONO,convert (varchar(10), PurchaseOrder_Date,103) as Date,TSPL_PURCHASE_ORDER_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],TSPL_PURCHASE_ORDER_HEAD.RefTendorNo as TendorNo ,PO_Total_Amt as Amount,case when TSPL_PURCHASE_ORDER_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status],case when TSPL_PURCHASE_ORDER_HEAD.close_yn='N' then 'Open' else 'Closed' end as [Close Status],Bill_To_Location as [Location],(select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code =Bill_To_Location ) as [Location Name],tspl_user_master.User_Name as [User Name],TSPL_RFQ_HEAD.RFQ_NO  as [RFQ NO],TSPL_RFQ_HEAD .RFQ_Date AS [RFQDate],TSPL_REQUISITION_HEAD.Requisition_Id as [Indent No],case when isnull(TSPL_PURCHASE_ORDER_HEAD.Against_Tender,'')='Y' then 'Yes' else 'No' end as [Against Tender],TSPL_PURCHASE_ORDER_HEAD.Confirmatory_PO_SRN_No as [Confirmatory PO SRN No],TSPL_PURCHASE_ORDER_HEAD.Against_WorkEstimation_Id as [Work Estimation No] from TSPL_PURCHASE_ORDER_HEAD"

        '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
        qry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_PURCHASE_ORDER_HEAD.Vendor_Code "
        qry += "  left join tspl_user_master on TSPL_USER_MASTER .User_Code =TSPL_PURCHASE_ORDER_HEAD.Created_By "
        'stuti
        qry += " left outer join TSPL_RFQ_HEAD on TSPL_RFQ_HEAD.Requisition_Id=TSPL_PURCHASE_ORDER_HEAD.Against_Requisition "
        '====end here====
        '====Sanjeet============
        qry += " left outer join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id=TSPL_PURCHASE_ORDER_HEAD.Against_Requisition"
        '=================
        qry += " left outer join TSPL_WORK_ESTIMATION_HEAD on TSPL_WORK_ESTIMATION_HEAD.WorkEstimation_Id=TSPL_PURCHASE_ORDER_HEAD.Against_WorkEstimation_Id"
        Dim whrClas As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            whrClas = " Bill_To_Location in (" + arrLoc + ") and"
        End If

        'If clsCommon.CompairString(clsUserMgtCode.mbtnPurchaseOrder, FORMTYPE) = CompairStringResult.Equal Then
        '    whrClas += "  TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade=0 "
        'Else
        '    whrClas += "  TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade=1 "
        'End If
        whrClas += " TSPL_PURCHASE_ORDER_HEAD.From_Screen_Code='" + FORMTYPE + "'"

        If objCommonVar.RCDFCFP = True Then
            whrClas += " And PO_Total_Amt>0 "
        End If


        LoadData(clsCommon.ShowSelectForm("POOrderNoFndd", qry, "PONO", whrClas, txtDocNo.Value, "PurchaseOrder_Date desc", isButtonClicked, "PurchaseOrder_Date"), NavigatorType.Current)
        btnCopy.Enabled = False
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colReqistionNo).Value) <= 0 Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) AndAlso Not clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal Then
                gv1.CurrentColumn = gv1.Columns(colIName)
                OpenICodeList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
            If gv_roadpermit.CurrentColumn Is gv_roadpermit.Columns(colroadformcode) Then
                OpenRoadPermit(True)
            End If
            If gv_c_form.CurrentColumn Is gv_c_form.Columns(colCFormformcode) Then
                OpenCFORM(True)
            End If
            ''setGridFocus()
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SavingData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf Not e.Alt AndAlso Not e.Shift AndAlso (e.Control AndAlso e.KeyCode = Keys.F7) Then ''because setting is open at Alt+Shift+Cntl+F7, and after this short-cut works automatically creates problem ,so do change
            SelectRequistionItems()
            ''richa agarwal 08/04/2015
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F10 Then
            If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.mbtnPurchaseOrder) = CompairStringResult.Equal Then
                If Not isSettlementBankOnly Then
                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = clsFixedParameterType.SettlementBankOnlyPWD
                    frm.strCode = clsFixedParameterCode.SettlementBankOnlyPWD
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        isSettlementBankOnly = True
                    End If
                Else
                    isSettlementBankOnly = False
                End If
            End If

        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F11 Then
            If AllowAmendmentWithPasssword(MyBase.Form_ID, Nothing) Then
                btnAmendment.Visible = True
            Else
                btnAmendment.Visible = False
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                'Add Tool tip Task No- TEC/22/05/18-000245
                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                                      "TSPL_PURCHASE_ORDER_HEAD " + Environment.NewLine +
                                                      "TSPL_PURCHASE_ORDER_DETAIL " + Environment.NewLine +
                                                      "TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL (If Road Permit) " + Environment.NewLine +
                                                      "TSPL_CFORM_ISSUE_RECEIVE_DETAIL (If C Form) " + Environment.NewLine +
                                                      "tspl_Purchase_Order_work_order " + Environment.NewLine +
                                                      "TSPL_PURCHASE_ORDER_WORK_ORDER_Terms " + Environment.NewLine +
                                                      "Press Alt+P for Post Trasnaction " + Environment.NewLine +
                                                      "TSPL_REQUISITION_DETAIL (Set balance qty) " + Environment.NewLine +
                                                      "TSPL_VENDOR_INVOICE_HEAD (Auto Vendor Invoice)" + Environment.NewLine +
                                                      "TSPL_VENDOR_INVOICE_DETAIL " + Environment.NewLine +
                                                      "TSPL_REMITTANCE " + Environment.NewLine +
                                                      "TSPL_AP_Invoice_Asset_EMI_Details " + Environment.NewLine +
                                                      "TSPL_AP_INVOICE_ADVANCE_INTEREST " + Environment.NewLine +
                                                      "TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL " + Environment.NewLine +
                                                      "TSPL_PROVISION_ENTRY_KNOCKOFF " + Environment.NewLine +
                                                      "TSPL_EXPIRY_DATE " + Environment.NewLine +
                                                      "TSPL_EX_PI_HEAD (If Merchant Trade then update Against PO)" + Environment.NewLine +
                                                      "Setting- AllowPOScheduling (For creating purchase schedule) " + Environment.NewLine +
                                                      "TSPL_PO_SCH_HEAD " + Environment.NewLine +
                                                      "TSPL_PO_SCH_DETAIL " + Environment.NewLine +
                                                      "TSPL_PO_VENDOR_SCH_DETAIL ")
                'Add Tool tip Task No- TEC/22/05/18-000245
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnUnpost.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER"
        txtTermCode.Value = clsCommon.ShowSelectForm("PoermCodefnd", qry, "Code", "", txtTermCode.Value, "Code", isButtonClicked)
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
        Try
            'Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
            'txtTaxGroup.Value = clsCommon.ShowSelectForm("POTaxGroupfndd", qry, "Code", "Tax_Group_Type='P'", txtTaxGroup.Value, "Code", isButtonClicked)
            txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(IIf(clsCommon.myLen(clsCommon.myCstr(txtShipToLocation.Value)) <= 0, txtBillToLocation.Value, txtShipToLocation.Value), txtVendorNo.Value, "P", txtTaxGroup.Value, isButtonClicked)
            SetTaxDetails()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetTax()
        If clsCommon.myLen(clsCommon.myCstr(txtShipToLocation.Value)) <= 0 Then
            txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txtBillToLocation.Value, txtVendorNo.Value, "P", txtDate.Value)
            lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfPurchaseType(txtTaxGroup.Value, Nothing)
            SetTaxDetails()
        Else
            txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txtShipToLocation.Value, txtVendorNo.Value, "P", txtDate.Value)
            lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfPurchaseType(txtTaxGroup.Value, Nothing)
            SetTaxDetails()
        End If
    End Sub

    Sub SetTaxDetails()
        LoadBlankGridTax()
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", txtVendorNo.Value, IIf(clsCommon.myLen(clsCommon.myCstr(txtShipToLocation.Value)) <= 0, txtBillToLocation.Value, txtShipToLocation.Value))
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If (dt.Rows.Count > 10) Then
                MessageBox.Show("Can't Handle More than 10 Tax Types in a Group")
                Return
            End If
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
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
        Dim dt As DataTable = Nothing
        If clsCommon.CompairString(gv1.CurrentRow.Cells(colRowType).Value, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
            dt = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", txtVendorNo.Value, IIf(clsCommon.myLen(clsCommon.myCstr(txtShipToLocation.Value)) <= 0, txtBillToLocation.Value, txtShipToLocation.Value), IIf(clsCommon.myLen(gv1.CurrentRow.Cells(colICode)) > 0, gv1.CurrentRow.Cells(colICode).Value, ""), txtDate.Value)
        Else
            dt = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", txtVendorNo.Value, IIf(clsCommon.myLen(clsCommon.myCstr(txtShipToLocation.Value)) <= 0, txtBillToLocation.Value, txtShipToLocation.Value))
        End If
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index, isChangeRate)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colICode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        If isChangeRate Then
                            If chkTDSApplied.Checked AndAlso clsCommon.CompairString(clsCommon.myCstr(dr("IS_TCS")), "Y") = CompairStringResult.Equal Then
                                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(chkTDSApplied.Tag)
                            ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(clsCommon.myCstr(colItemTaxable)).Value) AndAlso PurchaseModulePickFixTaxRate Then
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
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)

                        ii = ii + 1
                    Next
                End If
            Else
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    BlankTaxDetails(intRowNo)
                    If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colICode)) > 0 Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                If chkTDSApplied.Checked AndAlso clsCommon.CompairString(clsCommon.myCstr(dr("IS_TCS")), "Y") = CompairStringResult.Equal Then
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(chkTDSApplied.Tag)
                                ElseIf clsCommon.myCBool(gv1.Rows(intRowNo).Cells(clsCommon.myCstr(colItemTaxable)).Value) AndAlso PurchaseModulePickFixTaxRate Then
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

    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        Dim qry As String = ""
        Dim whrCls As String = ""
        whrCls = " TSPL_VENDOR_MASTER.Status='N' and TSPL_VENDOR_MASTER.Form_Type<>'VSP'"
        If objCommonVar.RCDFCFP = True Then
            whrCls += " and TSPL_VENDOR_MASTER.in_active_cf IS NULL OR TSPL_VENDOR_MASTER.in_active_cf = 'N'"
        End If
        ''richa agarwal 24/12/2014
        If chkIsMerchantTrade.Checked Then
            whrCls += "  And  TSPL_VENDOR_MASTER.CURRENCY_CODE<>(Select isnull(BaseCurrencyCode,'')  from TSPL_COMPANY_MASTER where Comp_Code ='" & objCommonVar.CurrentCompanyCode & "' )"
        End If
        ''-------------------------
        '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
        qry = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_VENDOR_MASTER.City_Code_Desc)>0 then ', '+TSPL_VENDOR_MASTER.City_Code_Desc else ' ' end + case when len(TSPL_VENDOR_MASTER.State )>0 then TSPL_VENDOR_MASTER.State else '' end  as Address,Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],TSPL_VENDOR_MASTER.Phone1 as Phone from TSPL_VENDOR_MASTER "
        If clsCommon.CompairString(cboPOType.SelectedValue, "L") = CompairStringResult.Equal Then
            'qry += "LEFT OUTER JOIN TSPL_COMPANY_MASTER ON  TSPL_COMPANY_MASTER.Comp_Code = TSPL_VENDOR_MASTER.Comp_Code "
            'whrCls = " isnull(TSPL_COMPANY_MASTER.BaseCurrencyCode,'') = isnull(TSPL_VENDOR_MASTER.CURRENCY_CODE,'')" ' AND TSPL_VENDOR_MASTER.CURRENCY_CODE IS NOT NULL  
        ElseIf clsCommon.CompairString(cboPOType.SelectedValue, "I") = CompairStringResult.Equal Then
            'qry += "LEFT OUTER JOIN TSPL_COMPANY_MASTER ON  TSPL_COMPANY_MASTER.Comp_Code = TSPL_VENDOR_MASTER.Comp_Code "
            'whrCls = " isnull(TSPL_COMPANY_MASTER.BaseCurrencyCode,'') <> isnull(TSPL_VENDOR_MASTER.CURRENCY_CODE,'')" ' AND TSPL_VENDOR_MASTER.CURRENCY_CODE IS NOT NULL  
        End If

        txtVendorNo.Value = clsCommon.ShowSelectForm("POVendorFndr", qry, "Code", whrCls, txtVendorNo.Value, "Code", isButtonClicked)
        ''lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'"))
        qry = "select  Vendor_Code,Vendor_Name,Terms_Code,Terms_Code_Desc ,Vendor_Account ,Tax_Group,Tax_Group_Desc,GSTRegistered,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_VENDOR_MASTER.City_Code_Desc)>0 then ', '+TSPL_VENDOR_MASTER.City_Code_Desc else ' ' end + case when len(TSPL_VENDOR_MASTER.State )>0 then TSPL_VENDOR_MASTER.State else '' end  as Address,TSPL_VENDOR_MASTER.Phone1 as Phone from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtVendorNo.Value + "' and TSPL_VENDOR_MASTER.Form_Type<>'VSP'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Code_Desc"))
            'txtDeliveryDate.Text = clsCommon.myCDate(txtDate.Text).AddDays(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TSPL_TERMS_MASTER.No_Days FROM TSPL_TERMS_MASTER WHERE TSPL_TERMS_MASTER.Terms_Code='" + clsCommon.myCstr(dt.Rows(0)("Terms_Code")) + "'")))
            'txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            'lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            If clsERPFuncationality.GetGSTStatus(clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")) Then
                If objCommonVar.TreatUnregisteredVendorAsRegisteredVendor Then
                    chkGSTRegistered.Checked = True
                Else
                    chkGSTRegistered.Checked = IIf(clsCommon.myCdbl(dt.Rows(0)("GSTRegistered")) = 1, True, False)
                End If
            Else
                chkGSTRegistered.Checked = True
            End If
            lblWVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            lblWAddress.Text = clsCommon.myCstr(dt.Rows(0)("Address"))
            lblWPhone.Text = clsCommon.myCstr(dt.Rows(0)("Phone"))
            SetMultiCurrencyVisibility()
        Else
            lblVendorName.Text = ""
            txtTermCode.Value = ""
            lblTermName.Text = ""
            txtTaxGroup.Value = ""
            lblTaxGrpName.Text = ""
            chkGSTRegistered.Checked = True
            Me.txtCurrencyCode.Value = ""
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""
            lblWAddress.Text = ""
            lblWPhone.Text = ""
            lblWVendorName.Text = ""

        End If
        ''richa agarwal 24/12/2014
        If Not chkIsMerchantTrade.Checked Then
            SetTax()
        End If
        '---------------------

        SetTermDetails()
        btnHistory.Enabled = True
        SetVendorTDSDetails()
        FillVendorDetails()


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


        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        ''richa agarwal 09/02/2015
        Dim WhrCls As String = String.Empty
        If chkIsMerchantTrade.Checked Then
            WhrCls = " Location_Type='Virtual'  "
        Else
            WhrCls = " Location_Type='Physical'  "

        End If
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        ''-------------------------
        txtBillToLocation.Value = clsCommon.ShowSelectForm("BILLTOLOCPO", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))
        '' Anubhooti 30-Dec-2014 (Deleivered To should autofill billtoloc desp)
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            If clsCommon.myLen(txtShipToLocation.Value) <= 0 Then
                txtRefNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))
            Else
                txtRefNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtShipToLocation.Value + "'"))
            End If
        Else
            If clsCommon.myLen(txtShipToLocation.Value) > 0 Then
                txtRefNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtShipToLocation.Value + "'"))
            Else
                txtRefNo.Text = ""
            End If
        End If
        ''
        ''richa 10/02/2014
        If Not chkIsMerchantTrade.Checked Then
            SetTax()
        End If
        ''-------------------
    End Sub

    Private Sub txtShipToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipToLocation._MYValidating
        'Dim qry As String = "select Ship_To_Code as Code,Ship_To_Desc as Description from TSPL_SHIP_TO_LOCATION"
        'txtShipToLocation.Value = clsCommon.ShowSelectForm("POShipToLocfnd", qry, "Code", "", txtShipToLocation.Value, "Code", isButtonClicked)
        'lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ship_To_Desc from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipToLocation.Value + "'"))

        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtShipToLocation.Value = clsCommon.ShowSelectForm("BILLTOLOCPO", qry, "Code", WhrCls, txtShipToLocation.Value, "Code", isButtonClicked)
        lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtShipToLocation.Value + "'"))
        '' Anubhooti 30-Dec-2014 (Deleivered To should autofill billtoloc desp)
        If clsCommon.myLen(txtShipToLocation.Value) > 0 Then
            txtRefNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtShipToLocation.Value + "'"))
        Else
            If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
                txtRefNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))
            Else
                txtRefNo.Text = ""
            End If
        End If
        If Not chkIsMerchantTrade.Checked Then
            SetTax()
        End If
    End Sub

    Private Sub btnRequistionItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Sub SelectRequistionItems()
        isInsideLoadData = True
        Dim frm As New FrmPendingRequistion()
        frm.VendorCode = txtVendorNo.Value
        frm.VendorName = lblVendorName.Text
        frm.strCurrCode = txtDocNo.Value
        frm.SettingIndendFreePOClose = SettingIndendFreePOClose
        frm.Against_Tendor = IIf(chkTender.Checked = True, "Y", "N")
        If chkTender.Checked = True Then
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Vendor first", Me.Text)
                Exit Sub
            End If
        End If
        frm.ShowDialog()
        chk_emergency.Enabled = True
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            Dim objReq As clsRequistionHead = clsRequistionHead.GetData(frm.ArrReturn(0).Requisition_Id, NavigatorType.Current, "")
            If objReq IsNot Nothing AndAlso clsCommon.myLen(objReq.Requisition_Id) > 0 Then
                chk_emergency.Checked = IIf(objReq.Emergency = 1, True, False)
                chk_emergency.Enabled = False
                If ShowCapexCodeandSubCode Then
                    If clsCommon.CompairString(objReq.Category, "") = CompairStringResult.Equal Then
                        ddl_category.SelectedValue = clsCommon.myCstr("")
                        ddl_category.Enabled = True

                    Else
                        ddl_category.SelectedValue = objReq.Category
                        ddl_category.Enabled = False
                    End If
                    fndcapexcode.Value = clsCommon.myCstr(objReq.Capex_Code)
                    If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                        lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
                    End If
                    fndcapexsubcode.Value = clsCommon.myCstr(objReq.Capex_SubCode)
                    If clsCommon.myLen(Me.fndcapexsubcode.Value) > 0 Then
                        lbl_capexsubcode.Text = clsCapexBudget.GetName(Me.fndcapexsubcode.Value, Nothing)
                        lbl_budgetamt.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                        lbl_budgetamtwithtolerence.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                        lbl_rebudgetamt.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, "", Nothing)
                        lbl_rebudgetamtwithtolerence.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, "", Nothing)
                    End If
                End If
                chkOpenPO.Checked = objReq.Is_Open_PO
                dtpExpiryDate.MendatroryField = chkOpenPO.Checked '=Done By Monika
                cboPOType.SelectedValue = objReq.Requisition_Type

                If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                    txtVendorNo.Value = frm.VendorCode
                    lblVendorName.Text = frm.VendorName
                    Dim qry As String = "select  Terms_Code,Terms_Code_Desc from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtVendorNo.Value + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                        lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Code_Desc"))
                        'txtDeliveryDate.Text = clsCommon.myCDate(txtDate.Text).AddDays(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TSPL_TERMS_MASTER.No_Days FROM TSPL_TERMS_MASTER WHERE TSPL_TERMS_MASTER.Terms_Code='" + clsCommon.myCstr(dt.Rows(0)("Terms_Code")) + "'")))
                    End If
                End If
                'If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
                If Not settCreatePOFromMultipleLocation Then
                    txtBillToLocation.Value = objReq.Location
                    lblBillToLocation.Text = objReq.LocationName
                    If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
                        txtBillToLocation.Enabled = False
                    End If
                    'End If
                End If


                If (clsCommon.myLen(txtDesc.Text) <= 0) Then
                    txtDesc.Text = objReq.Description
                End If
                If (clsCommon.myLen(txtDesc.Text) <= 0) Then
                    txtRemarks.Text = objReq.Remarks
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
                If (clsCommon.myLen(cboModeOfTransport.Text) <= 0) Then
                    cboModeOfTransport.Text = objReq.Mode_Of_Transport
                End If
                If (clsCommon.myLen(txtRemarks.Text) <= 0) Then
                    txtRemarks.Text = objReq.Remarks
                End If
                If (clsCommon.myLen(fndProject.Value) <= 0) Then
                    fndProject.Value = objReq.PROJECT_ID
                    lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
                    fndProject.Enabled = False
                    lblProject.Enabled = False
                End If
            End If
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
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_Code
                    If chkTender.Checked = True Then
                        gv1.CurrentRow.Cells(colRate).Value = clsVendorItemDetail.GetRate(txtVendorNo.Value, obj.Item_Code, obj.Unit_Code, gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value)
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(obj.Item_Code)
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = obj.Location
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = obj.LocationName
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgRequitionQty).Value = obj.Requisition_Qty
                    '==========Sanjeet(22/12/2016)=========================
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = obj.Specification
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = obj.Remarks
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCapacity).Value = obj.Capacity
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMake).Value = obj.Make
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colModel).Value = obj.Model
                    '=============================================

                    'gv1.CurrentRow.Cells(colPrevPOQty).Value = PendingQtyforGRN(txtVendorNo.Value, obj.Item_Code, obj.Unit_Code)
                    'gv1.CurrentRow.Cells(colPrevPONO).Value = PendingPONOforGRN(txtVendorNo.Value, obj.Item_Code, obj.Unit_Code)

                    GetLastRateItemWise(obj.Item_Code, gv1.Rows.Count - 1)

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
                clsCommon.MyMessageBoxShow(Me, "Requition No : " + obj.Requisition_Id + "  Item : " + obj.Item_Desc + Environment.NewLine + "Already exist at row no:" + clsCommon.myCstr(ii + 1))
                Return True
            End If
        Next
        Return False
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


            ElseIf (gv1.CurrentColumn Is gv1.Columns(colComplete) AndAlso (UsLock1.Status = ERPTransactionStatus.Approved)) Then
                Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                Dim intSNo As Integer = Convert.ToInt32((clsCommon.myCdbl(gv1.CurrentRow.Cells(colLineNo).Value)))
                Dim strStatus As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colComplete).Value)
                If clsCommon.myLen(txtDocNo.Value) > 0 AndAlso clsCommon.myLen(strICode) > 0 AndAlso intSNo > 0 AndAlso clsCommon.CompairString(strStatus, "No") = CompairStringResult.Equal Then
                    If clsCommon.MyMessageBoxShow(Me, "Do you want to complete the item " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value), Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        If clsPurchaseOrderDetail.CompletePO(txtDocNo.Value, strICode, intSNo) Then
                            clsCommon.MyMessageBoxShow(Me, "Successfully Completed", Me.Text)
                            LoadData(txtDocNo.Value, NavigatorType.Current)
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            myMessages.blankValue(Me, "Purchase Order No not found to Print", Me.Text)
        Else
            funprint(i)
        End If
    End Sub

    Public Sub funprint(ByVal i As Integer)
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Purchase Order No not found to Print", Me.Text)
            End If
            Dim arr As New ArrayList()
            arr.Add(txtDocNo.Value)
            FrmPurchaseOrderReport.PrintData("", "", True, arr, False, Nothing, False, "", i, txtReqNo.Value, cboPOType.Text, lblAddCharges.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
        RefreshReqNo()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colItemUsedINGRN).Value) Then
            clsCommon.MyMessageBoxShow(Me, "Can't Delete The Current Row.This Item is Used In GRN", Me.Text)
            e.Cancel = True
        End If
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If ShowItemInCaseofNonInventory = False Then

                If e.Column Is gv1.Columns(colICode) Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colReqistionNo).Value) > 0 OrElse clsCommon.myCBool(gv1.CurrentRow.Cells(colItemUsedINGRN).Value) Then
                        gv1.CurrentRow.Cells(colICode).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colICode).ReadOnly = False
                    End If
                ElseIf e.Column Is gv1.Columns(colUnit) Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colReqistionNo).Value) > 0 OrElse clsCommon.myCBool(gv1.CurrentRow.Cells(colItemUsedINGRN).Value) Then
                        gv1.CurrentRow.Cells(colUnit).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colUnit).ReadOnly = False
                    End If
                ElseIf e.Column Is gv1.Columns(colIName) Then
                    If clsCommon.CompairString(cboItemType.SelectedValue, "N") <> CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colIName).ReadOnly = True
                    ElseIf clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colIName).ReadOnly = False
                    End If
                ElseIf e.Column Is gv1.Columns(colQty) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colQty).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colQty).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colInsurancePer) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colInsurancePer).ReadOnly = True
                    ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colIsInsurance).Value) Then
                        gv1.CurrentRow.Cells(colInsurancePer).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colInsurancePer).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colRate) Then
                    If (clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal And chkTender.Checked = False) OrElse (btnAmendment.Visible = True AndAlso btnAmendment.Enabled = True) Then
                        gv1.CurrentRow.Cells(colRate).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colRate).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colAmt) Then

                    If (clsCommon.CompairString(Me.cboPOType.SelectedValue, "J") = CompairStringResult.Equal And clsCommon.CompairString(Me.cboItemType.SelectedValue, "N") = CompairStringResult.Equal And chkAutoCalculate.Checked = False) Then
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = False
                    ElseIf (clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal) Then
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = True
                    ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colIsInsurance).Value) Then
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = False
                    End If
                ElseIf e.Column Is gv1.Columns(colMRP) Then
                    If clsCommon.myCBool(gv1.CurrentRow.Cells(colisMRPMandatory).Value) Then
                        gv1.CurrentRow.Cells(colMRP).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colMRP).ReadOnly = True
                    End If
                ElseIf (e.Column Is gv1.Columns(colItemInsurancePer)) Then
                    gv1.CurrentRow.Cells(colItemInsurancePer).ReadOnly = IIf(clsCommon.CompairString(clsCalculationlApplyON.RowTypeApplyOnPercent, clsCommon.myCstr(gv1.CurrentRow.Cells(colItemInsuranceApplyOn).Value)) = CompairStringResult.Equal, False, True)
                ElseIf (e.Column Is gv1.Columns(colItemInsuranceAmt)) Then
                    gv1.CurrentRow.Cells(colItemInsuranceAmt).ReadOnly = IIf(clsCommon.CompairString(clsCalculationlApplyON.RowTypeApplyOnAmount, clsCommon.myCstr(gv1.CurrentRow.Cells(colItemInsuranceApplyOn).Value)) = CompairStringResult.Equal, False, True)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        If intCurrRow = gv1.Rows.Count - 1 Then
            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
            gv1.CurrentRow = gv1.Rows(intCurrRow)
        End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colQty)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colRate)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colRate) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colDisPer)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colDisPer) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colSpecification)

                ''ElseIf gv1.CurrentColumn Is gv1.Columns(colMRP) Then
                ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
                ''    gv1.CurrentColumn = gv1.Columns(colAssessableAmt)
                ''ElseIf gv1.CurrentColumn Is gv1.Columns(colAssessableAmt) Then
                ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
                ''    gv1.CurrentColumn = gv1.Columns(colSpecification)

            ElseIf gv1.CurrentColumn Is gv1.Columns(colSpecification) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colRemarks)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colRemarks) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
        End If
    End Sub

    Private Sub txtReqNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReqNo._MYValidating
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.WorkOrderEng) = CompairStringResult.Equal Then
            Try
                Dim qry As String = "select WorkEstimation_Id as Code,convert(varchar,WorkEstimation_Date,103) as Date,Description, case when Status='0' then 'Pending' else 'Approved' end as [Status],case when  Is_Internal='Y' then 'Internal' else 'External' end as Internal,TSPL_USER_MASTER.User_Name as [User Name] from TSPL_WORK_ESTIMATION_HEAD left join tspl_user_master on TSPL_USER_MASTER .User_Code =TSPL_WORK_ESTIMATION_HEAD.Created_By"
                Dim whrClas As String = " Is_Internal='N'"

                whrClas += " and TSPL_WORK_ESTIMATION_HEAD.Status=1 "
                whrClas += " and not exists (select 1 from TSPL_PURCHASE_ORDER_HEAD where From_Screen_Code='" + clsUserMgtCode.WorkOrderEng + "' and TSPL_PURCHASE_ORDER_HEAD.Against_WorkEstimation_Id=TSPL_WORK_ESTIMATION_HEAD.WorkEstimation_Id) "

                txtReqNo.Value = clsCommon.ShowSelectForm("WOfindNo", qry, "Code", whrClas, txtReqNo.Value, "WorkEstimation_Date desc", isButtonClicked)
                If clsCommon.myLen(txtReqNo.Value) > 0 Then
                    SelectWorkEstimation(txtReqNo.Value)
                End If

            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        Else
            SelectRequistionItems()
        End If
        FillVendorDetails()
    End Sub

    Sub RefreshReqNo()
        txtReqNo.Value = ""
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colReqistionNo).Value)
                If clsCommon.myLen(strReqNo) > 0 Then
                    txtReqNo.Value = strReqNo
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub PrintAmendment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            myMessages.blankValue(Me, "Purchase Order No not found to Print", Me.Text)
        Else
            FrmPurchaseOrderReport.PrintAbandoment(txtDocNo.Value)
            '' ''clsCommon.ProgressBarShow()
            '' ''For index As Integer = 1 To Integer.MaxValue - 1

            '' ''Next
            '' ''clsCommon.ProgressBarHide()
        End If
    End Sub



    Private Sub btnAmendment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAmendment.Click
        Try
            Dim isDoAbandomentNo As Boolean = False
            Dim Reason As String = ""
            If UsLock1.Status = ERPTransactionStatus.Approved Then
                If clsCommon.myLen(lblConfirmatory_PO_SRN_No.Text) > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "This PO is Generated By Confirmatory PO in SRN No[" + lblConfirmatory_PO_SRN_No.Text + "]" + Environment.NewLine + "Cannot Edit/Modify/Amendment in this PO")
                    Exit Sub
                End If



                If clsCommon.MyMessageBoxShow(Me, "Do you want to make Amendment", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    isDoAbandomentNo = True
                    If IsRemarkReasonMandatoryOnPO Then
                        '' REASON FOR Amendment 
                        Dim frm As New FrmFreeTxtBox1
                        frm.Text = "Remarks for Amendment"
                        frm.ShowDialog()
                        If clsCommon.myLen(frm.strRmks) <= 0 Then
                            Exit Sub
                        Else
                            Reason = frm.strRmks
                        End If
                    End If
                End If
            End If
            'Dim qry As String = "select IsAutoCreateGRNAndMRN from TSPL_INV_PARAMETERS"
            'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 1 Then
            '    qry = "select 1 from TSPL_SRN_HEAD where Against_PO='" + txtDocNo.Value + "'"
            'Else
            '    qry = "select 1 from TSPL_GRN_HEAD where Against_PO='" + txtDocNo.Value + "'"
            'End If
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    Throw New Exception("Can't Amendment because PO no is used.")
            'End If
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                '=====done by shivani tyagi
                Dim strSubject As String = txtSubject.Text
                Dim strContentSubject As String = txtContentSubject.Text
                Dim strKindAttention As String = txtKindAttentation.Text
                Dim strComment As String = txtComment.Rtf
                'Dim strComment As String = RTComment.Rtf
                Dim dblGRNQty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(grn_qty) from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where isnull(Against_PO,'')='" & txtDocNo.Value & "' and item_code='" & strICode & "'"))
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                If dblQty < dblGRNQty Then
                    clsCommon.MyMessageBoxShow(Me, "" & dblGRNQty & " Qty has been used in GRN.Quantity cannot be less than used GRN Qty")
                    Exit Sub
                End If
            Next
            Dim IsSavedData As Boolean = SaveData(isDoAbandomentNo, False, True)
            If IsRemarkReasonMandatoryOnPO Then
                saveCancelLog(Reason, "Amendment", Nothing)
            End If
            'IsSavedData = IsSavedData AndAlso clsPurchaseOrderHead.PostData("", txtDocNo.Value, False, False)

            If IsSavedData Then
                btnAmendment.Visible = False
                clsCommon.MyMessageBoxShow(Me, "Successfully Amendmented", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
    'Sub SetVendorItemCostDetail()
    '    UcVendorItemDetail1.VendorCode = clsCommon.myCstr(txtVendorNo.Value)
    '    UcVendorItemDetail1.TransNo = clsCommon.myCstr(txtDocNo.Value)
    '    UcVendorItemDetail1.TransDate = clsCommon.GetPrintDate(txtDate.Text, "dd/MM/yyyy")
    '    UcVendorItemDetail1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
    '    UcVendorItemDetail1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
    '    UcVendorItemDetail1.Uom = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
    '    UcVendorItemDetail1.btnothers.Text = "Same Vendor"
    '    UcVendorItemDetail1.FormID = "PO"
    '    UcVendorItemDetail1.RefreshData()
    'End Sub

    Sub setBalance()
        UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
        UcItemBalance1.ItemMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
        UcItemBalance1.LocationCode = txtBillToLocation.Value
        UcItemBalance1.LocationName = lblBillToLocation.Text
        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtDocNo.Value
        UcItemBalance1.TransDate = txtDate.Value
        UcItemBalance1.ShowPOQty = True
        UcItemBalance1.RefreshData()

    End Sub

    Private Sub gv1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.Click
        If gv1.CurrentRow IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) > 0 Then
            'If ShowItemInCaseofNonInventory = False Then
            'setBalance()
            'End If

            'SetVendorItemCostDetail()
        End If
    End Sub

    Private Sub gv1_CurrentRowChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged

        If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) > 0 Then
            'If ShowItemInCaseofNonInventory = False Then
            setBalance()
            'End If
            'SetVendorItemCostDetail()
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ''Printing the amendment
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Purchase Order No not found to Print", Me.Text)
        Else
            FrmPurchaseOrderReport.PrintAbandoment(txtDocNo.Value)
        End If
    End Sub

    Private Function GetItemType() As DataTable
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

    Private Sub setGridFocusAC()
        Try
            Dim intCurrRow As Integer = gvAC.CurrentRow.Index
            If intCurrRow = gvAC.Rows.Count - 1 AndAlso gvAC.Rows.Count <= 10 Then
                gvAC.Rows.AddNew()
                gvAC.CurrentRow = gvAC.Rows(intCurrRow)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvAC_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvAC.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gvAC.Columns(colACPer)) Then
                    gvAC.CurrentRow.Cells(colACPer).ReadOnly = IIf(clsCommon.CompairString(clsCalculationlApplyON.RowTypeApplyOnPercent, clsCommon.myCstr(gvAC.CurrentRow.Cells(colACApplyOn).Value)) = CompairStringResult.Equal, False, True)
                ElseIf (e.Column Is gvAC.Columns(colACAmount)) Then
                    gvAC.CurrentRow.Cells(colACAmount).ReadOnly = IIf(clsCommon.CompairString(clsCalculationlApplyON.RowTypeApplyOnAmount, clsCommon.myCstr(gvAC.CurrentRow.Cells(colACApplyOn).Value)) = CompairStringResult.Equal, False, True)
                End If
            End If
        Catch ex As Exception
            'clsCommon.MyMessageBoxShow(Me,ex.Message)
        End Try
    End Sub

    Private Sub gvAC_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvAC.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    'If e.Column Is gvAC.Columns(colACApplyOn) OrElse e.Column Is gvAC.Columns(colACPer) OrElse e.Column Is gvAC.Columns(colACAmount) Then
                    If e.Column Is gvAC.Columns(colACApplyOn) OrElse e.Column Is gvAC.Columns(colACPer) Then
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
        repoACCode.Width = 100
        repoACCode.ReadOnly = False
        gvAC.MasterTemplate.Columns.Add(repoACCode)

        Dim repoACName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACName.FormatString = ""
        repoACName.HeaderText = "Addition Charges Description"
        repoACName.Name = colACName
        repoACName.Width = 200
        repoACName.ReadOnly = True
        gvAC.MasterTemplate.Columns.Add(repoACName)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Apply On"
        repoRowType.Name = colACApplyOn
        repoRowType.Width = 50
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = clsCalculationlApplyON.GetApplyOnType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Name"
        gvAC.MasterTemplate.Columns.Add(repoRowType) '2

        Dim repoACAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoACAmt.FormatString = ""
        repoACAmt.HeaderText = "%"
        repoACAmt.Name = colACPer
        repoACAmt.Minimum = 0
        repoACAmt.Maximum = 100
        repoACAmt.Width = 100
        repoACAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoACAmt.ReadOnly = False
        gvAC.MasterTemplate.Columns.Add(repoACAmt)

        repoACAmt = New GridViewDecimalColumn()
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
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                If Not (chkAgainst_RGP.Checked = True AndAlso clsCommon.myLen(txtRGPNo.Value) > 0) Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceApplyOn).Value = clsCalculationlApplyON.RowTypeApplyOnPercent
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                End If
            End If
        End If
    End Sub

    Private Sub cboPOType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPOType.SelectedValueChanged
        If Not IsFormLoad Then

            If clsCommon.CompairString(cboPOType.SelectedValue, "J") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboPOType.SelectedValue, "S") = CompairStringResult.Equal Then
                chkAgainst_RGP.Enabled = True

            Else
                chkAgainst_RGP.Enabled = False
                chkAgainst_RGP.Checked = False
                If clsCommon.CompairString(cboPOType.SelectedValue, "") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal Then
                    cboItemType.SelectedValue = ""
                    clsCommon.MyMessageBoxShow(Me, "Non-Inventory applicable for job-work only.", Me.Text)
                End If
            End If

        End If
        SetRateDescCols()
    End Sub

    Private Sub rbtnTaxCalAutomatic_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnTaxCalAutomatic.ToggleStateChanged, rbtnTaxCalManual.ToggleStateChanged
        If Not isInsideLoadData Then
            If rbtnTaxCalAutomatic.IsChecked Then
                SetTaxDetails()
                chkExciseOnQty.Enabled = True
                gv2.Columns(colTTaxRate).IsVisible = False
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
                gv2.Columns(colTTaxRate).IsVisible = True ''BHA/02/08/18-000210 by balwinder on 02/08/2018
            End If
        End If
    End Sub

    Private Sub gv2_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv2.CellFormatting
        Try
            If ShowItemInCaseofNonInventory = False Then
                If e.Column.Index >= 0 Then
                    If (e.Column Is gv2.Columns(colTTaxAmt)) Then
                        gv2.CurrentRow.Cells(colTTaxAmt).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                    ElseIf (e.Column Is gv2.Columns(colTTaxRate)) Then
                        gv2.CurrentRow.Cells(colTTaxRate).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                    End If
                End If
            End If

            'Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
            'cell.GradientStyle = GradientStyles.Solid
            'cell.BackColor = Color.FromArgb(243, 181, 51)
            'End If
        Catch ex As Exception
            'clsCommon.MyMessageBoxShow(Me,ex.Message)
        End Try
    End Sub

    Private Sub gv2_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedTaxOpen Then
                    isCellValueChangedTaxOpen = True
                    'If (e.Column Is (gv2.Columns(colTTaxAmt)) AndAlso rbtnTaxCalManual.IsChecked) Then
                    If (rbtnTaxCalManual.IsChecked) Then
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRow(ii)
                        Next
                        UpdateAllTotals()
                    End If
                    isCellValueChangedTaxOpen = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Purchase Order No not found to Print", Me.Text)
        Else
            i = 1
            funprint(i)
            i = 0
        End If
    End Sub

    Private Sub btnpreprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Purchase Order No not found to Print", Me.Text)
        Else
            i = 2
            funprint(i)
            i = 0
        End If
    End Sub

    Private Sub txtRGPNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRGPNo._MYValidating
        SelectRGPItems()
    End Sub

    Sub SelectRGPItems()
        isInsideLoadData = True
        Dim frm As New frmPendingRGP()
        frm.VendorCode = txtVendorNo.Value
        frm.strCurrCode = txtDocNo.Value
        frm.ShowDialog()
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            If clsCommon.myLen(frm.ArrReturn(0).RGP_No) > 0 Then
                Dim objMRNHead As clsRGPHead = clsRGPHead.GetData(frm.ArrReturn(0).RGP_No, NavigatorType.Current)
                If objMRNHead IsNot Nothing AndAlso clsCommon.myLen(objMRNHead.RGP_No) > 0 Then

                    'If clsCommon.myLen(txtCarrier.Text) <= 0 Then
                    '    txtVehicleNo.Text = objMRNHead.VehicleNo
                    'End If
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

                End If
            End If
            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If
            For Each obj As clsRGPDetail In frm.ArrReturn
                If IsValidItemForRGP(obj) Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = obj.RGP_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    '  gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(obj.Item_Code)
                    cboItemType.SelectedIndex = GetItemType(obj.Item_Code)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = obj.Location
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = obj.LocationName
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.RGP_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.RGP_Qty
                End If
            Next
            SetitemWiseTaxSetting(False, False)
            For ii As Integer = 0 To gv1.RowCount - 1
                UpdateCurrentRow(ii)
            Next
        End If
        isInsideLoadData = False
        UpdateAllTotals()
        RefreshGRPNo()
        cboItemType.SelectedValue = "O"
    End Sub

    Sub RefreshGRPNo()
        txtRGPNo.Value = ""
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

    Public Function GetTaxGrp(ByVal strItmType As String) As String
        Dim qry As String = "select Tax_Group  from TSPL_VENDOR_MASTER where Vendor_Code ='" + strItmType + "'"
        strItmType = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return strItmType
    End Function

    Function IsValidItemForRGP(ByVal obj As clsRGPDetail)
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strAgaintRGPCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRGPNo).Value)
            If clsCommon.myLen(strAgaintRGPCode) > 0 AndAlso clsCommon.CompairString(strAgaintRGPCode, obj.RGP_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "RGP No : " + obj.RGP_No + "  Item : " + obj.Item_Desc + Environment.NewLine + "Already exist at row no:" + clsCommon.myCstr(ii + 1))
                Return False
            End If
        Next
        Return True
    End Function

    Public Function GetItemType(ByVal strItmType As String) As String
        Dim qry As String = "select distinct Item_Type  from TSPL_ITEM_MASTER where Item_Code ='" + strItmType + "'"
        strItmType = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        If strItmType = "F" Then
            strItmType = 0
        Else
            strItmType = 1
        End If
        Return strItmType
    End Function

    Private Sub chkAgainst_RGP_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAgainst_RGP.ToggleStateChanged
        txtRGPNo.Visible = chkAgainst_RGP.Checked
        'RadLabel30.Visible = chkAgainst_RGP.Checked
        If chkAgainst_RGP.Checked = False Then
            txtRGPNo.Value = ""
            LoadBlankGrid()
            LoadBlankGridSchedule()
            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
        End If
    End Sub
    'Ticket No : ERO/23/07/19-000959 By Prabhakar work on RPT
    Private Sub btnPrintNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintNew.Click
        Try
            Dim obj As New clsPurchaseOrderHead()
            obj.PrintData(txtDocNo.Value)
            obj = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnUnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnpost.Click
        Try

            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    If clsCommon.myLen(lblConfirmatory_PO_SRN_No.Text) > 0 Then
                        Throw New Exception("This PO is Generated By Confirmatory PO in SRN No[" + lblConfirmatory_PO_SRN_No.Text + "]" + Environment.NewLine + "Cannot Reverse in this PO")
                    End If
                    clsPurchaseOrderHead.ReverseAndUnpost(txtDocNo.Value, MyBase.Form_ID)
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
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

    Sub closepoorder()
        Try
            If (clsPurchaseOrderHead.closepodata(txtDocNo.Value, True, closeyn)) Then
                If closeyn = "Y" Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Closed", Me.Text)
                ElseIf closeyn = "N" Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Opened", Me.Text)
                End If
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkpoclose_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkpoclose.CheckedChanged
        If chkpoclose.Checked = True And vaddnew = "N" Then
            Dim response = MsgBox("Are you sure want to close the Purchase Order", MsgBoxStyle.YesNo, "Attention")
            If response = MsgBoxResult.Yes Then
                closeyn = "Y"
                closepoorder()
            ElseIf response = MsgBoxResult.No Then
                chkpoclose.Checked = False
            End If
        ElseIf chkpoclose.Checked = False And vaddnew = "N" Then
            closeyn = "N"
            closepoorder()
        End If
        vaddnew = "N"
        If chkpoclose.Checked Then
            Dim makereadonly As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MakeClosingofPOreadonlyforuser, clsFixedParameterCode.MakeClosingofPOreadonlyforuser, Nothing)) = "1", True, False))
            If makereadonly Then
                chkpoclose.Enabled = False
            Else
                chkpoclose.Enabled = True
            End If
        End If
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        LoadBlankGrid()
        LoadBlankGridSchedule()
        isInsideLoadData = True
        Dim frm As New frmCopyPO()
        frm.ShowDialog()

        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            Dim objReq As clsPurchaseOrderHead = clsPurchaseOrderHead.GetData(frm.strFirstPO, NavigatorType.Current, "", IIf(clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmPurchaseOrderMT) = CompairStringResult.Equal, "MT", "PO"), FORMTYPE)
            If objReq IsNot Nothing AndAlso clsCommon.myLen(objReq.PurchaseOrder_No) > 0 Then
                If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                    txtVendorNo.Value = objReq.Vendor_Code
                    lblVendorName.Text = objReq.Vendor_Name
                End If
                If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
                    txtBillToLocation.Value = objReq.Bill_To_Location
                    lblBillToLocation.Text = objReq.BillToLocationName
                End If

                If (clsCommon.myLen(cboPOType.SelectedValue) <= 0) Then
                    cboPOType.SelectedValue = objReq.PurchaseOrder_Type
                End If
                If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
                    cboItemType.SelectedValue = objReq.Item_Type
                End If
                If (clsCommon.myLen(cboModeOfTransport.Text) <= 0) Then
                    cboModeOfTransport.Text = objReq.Mode_Of_Transport
                End If
                If (clsCommon.myLen(txtRemarks.Text) <= 0) Then
                    txtRemarks.Text = objReq.Remarks
                End If
                If (clsCommon.myLen(txtTaxGroup.Value) <= 0) Then
                    txtTaxGroup.Value = objReq.Tax_Group
                    lblTaxGrpName.Text = objReq.TaxGroupName
                    SetTaxDetails()
                End If
            End If
            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If
            For Each obj As clsPurchaseOrderDetail In frm.ArrReturn
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(obj.Item_Code)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Balance_Qty
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsVendorItemDetail.GetRate(txtVendorNo.Value, obj.Item_Code, obj.Unit_code, obj.MRP)
                SetitemWiseTaxSetting(True, True)
                UpdateCurrentRow(gv1.Rows.Count - 1)
            Next
        End If
        isInsideLoadData = False
        UpdateAllTotals()
        RefreshReqNo()
    End Sub

    Private Sub ReportFooter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportFooter.Click
        Dim frm As New FrmCrptFooter
        frm.strFormId = MyBase.Form_ID

        frm.ShowDialog()

    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveLayoutbtn.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)

        clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
    End Sub

    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.Click
        Dim frm As New FrmPurchaseHistory
        frm.SetUserMgmt(clsUserMgtCode.FrmPurchaseHistory)
        frm.strFormId = MyBase.Form_ID
        frm.strVendorCode = txtVendorNo.Value
        frm.strVendorName = lblVendorName.Text
        Dim strvendor As String = txtVendorNo.Value
        frm.ShowDialog()
        frm.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub chkMCCPurchase_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMCCPurchase.ToggleStateChanged
        If chkMCCPurchase.Checked Then
            fndState.Value = ""
            lblStateName.Text = ""
            'txtBillToLocation.Value = ""
            'lblBillToLocation.Text = ""
            fndState.Enabled = True
            'txtBillToLocation.Enabled = False
            fndState.MendatroryField = True
        Else
            fndState.Value = ""
            lblStateName.Text = ""
            'txtBillToLocation.Value = ""
            'lblBillToLocation.Text = ""
            fndState.Enabled = False
            'txtBillToLocation.Enabled = True
            fndState.MendatroryField = False
        End If
    End Sub

    Private Sub fndState__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndState._MYValidating
        Dim qry As String = "select State_Code 'Code', State_Name 'Name', Country_Code 'Country Code' from TSPL_STATE_MASTER "
        fndState.Value = clsCommon.ShowSelectForm("StateMaster", qry, "Code", "", fndState.Value, "Code", isButtonClicked)
        If Not String.IsNullOrEmpty(fndState.Value) Then
            lblStateName.Text = clsDBFuncationality.getSingleValue("select State_Name from TSPL_STATE_MASTER where State_Code='" + fndState.Value + "' ")
        Else
            lblStateName.Text = ""
        End If
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

        If check <= 0 Then
            qry = "select TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code as Code,TSPL_Form_Master.Form_Name as [Form Name],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Form_No as [Issue No],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Date as [Issue Date],TSPL_Form_Master.Remarks  from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST left outer join TSPL_FORM_MASTER on TSPL_FORM_MASTER.Form_Code=TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code "
            qry += " where TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Vendor_code='" + txtVendorNo.Value + "' and (TSPL_Form_Master.Form_Type='38-Inward' or TSPL_Form_Master.Form_Type='Others') and (TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.form_code+TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.iss_rcv_form_no) not in (select distinct form_code+issue_no from TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL where vendor_code='" + txtVendorNo.Value + "' and purchaseorder_no<>'" + txtDocNo.Value + "')"
        Else
            qry = "select TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code as Code,TSPL_Form_Master.Form_Name as [Form Name],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Form_No as [Issue No],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Date as [Issue Date],TSPL_Form_Master.Remarks  from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST left outer join TSPL_FORM_MASTER on TSPL_FORM_MASTER.Form_Code=TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code "
            qry += " where TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Vendor_code='" + txtVendorNo.Value + "' and (TSPL_Form_Master.Form_Type='38-Inward' or TSPL_Form_Master.Form_Type='Others') and TSPL_Form_Master.form_code='" + formcode + "' and (TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.form_code+TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.iss_rcv_form_no) not in (select distinct form_code+issue_no from TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL where vendor_code='" + txtVendorNo.Value + "' and purchaseorder_no<>'" + txtDocNo.Value + "')"
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

        If check <= 0 Then
            qry = "select TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code as Code,TSPL_Form_Master.Form_Name as [Form Name],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Form_No as [Issue No],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Date as [Issue Date],TSPL_Form_Master.Remarks  from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST left outer join TSPL_FORM_MASTER on TSPL_FORM_MASTER.Form_Code=TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code "
            qry += " where TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Vendor_code='" + txtVendorNo.Value + "' and (TSPL_Form_Master.Form_Type='C' or TSPL_Form_Master.Form_Type='Others') and (TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.form_code+TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.iss_rcv_form_no) not in (select distinct form_code+issue_no from TSPL_CFORM_ISSUE_RECEIVE_DETAIL where vendor_code='" + txtVendorNo.Value + "' and purchaseorder_no<>'" + txtDocNo.Value + "')"
        Else
            qry = "select TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code as Code,TSPL_Form_Master.Form_Name as [Form Name],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Form_No as [Issue No],TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Iss_Rcv_Date as [Issue Date],TSPL_Form_Master.Remarks  from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST left outer join TSPL_FORM_MASTER on TSPL_FORM_MASTER.Form_Code=TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Form_Code "
            qry += " where TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Vendor_code='" + txtVendorNo.Value + "' and (TSPL_Form_Master.Form_Type='C' or TSPL_Form_Master.Form_Type='Others') and TSPL_Form_Master.form_code='" + formcode + "' and (TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.form_code++TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.iss_rcv_form_no) not in (select distinct form_code+issue_no from TSPL_CFORM_ISSUE_RECEIVE_DETAIL where vendor_code='" + txtVendorNo.Value + "' and purchaseorder_no<>'" + txtDocNo.Value + "')"
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

    Sub LoadDefaultTermDetail()
        Dim qry As String = "select top 1 Terms_Code from TSPL_PURCHASE_ORDER_HEAD where ISNULL(terms_code,'')<>'' order by PurchaseOrder_No"
        txtTermCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        If clsCommon.myLen(txtTermCode.Value) > 0 Then
            SetTermDetails()
        End If
    End Sub

    Private Sub btnForm_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForm_Update.Click
        Try
            If Chkroadpermit.Checked Or chk_c_form.Checked Then
                AllowToSave_FormEntry()

                Dim arr As New List(Of clsPurchaseOrderRoadDetail)

                For Each grow As GridViewRowInfo In gv_roadpermit.Rows
                    Dim objtr As New clsPurchaseOrderRoadDetail()
                    objtr.roadpono = clsCommon.myCstr(txtDocNo.Value)
                    objtr.roadcode = clsCommon.myCstr(grow.Cells(colroadformcode).Value)
                    objtr.roadvendor = clsCommon.myCstr(txtVendorNo.Value)
                    objtr.roadissue_no = clsCommon.myCstr(grow.Cells(colroadformserialno).Value)

                    If clsCommon.myLen(objtr.roadcode) > 0 Then
                        arr.Add(objtr)
                    End If
                Next


                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    If clsPurchaseOrderRoadDetail.SaveData_RoadPermit(txtDocNo.Value, arr, Nothing) Then
                        Dim qry As String = "update tspl_purchase_order_head set issue_road_permit='1' where purchaseorder_no='" + txtDocNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    End If
                End If

                Dim arr1 As New List(Of clsPurchaseOrderCFORMDetail)

                For Each grow As GridViewRowInfo In gv_c_form.Rows
                    Dim objtr As New clsPurchaseOrderCFORMDetail()
                    objtr.cformpono = clsCommon.myCstr(txtDocNo.Value)
                    objtr.cformcode = clsCommon.myCstr(grow.Cells(colCFormformcode).Value)
                    objtr.cformvendor = clsCommon.myCstr(txtVendorNo.Value)
                    objtr.cformissue_no = clsCommon.myCstr(grow.Cells(colCFormformserialno).Value)

                    If clsCommon.myLen(objtr.cformcode) > 0 Then
                        arr1.Add(objtr)
                    End If
                Next


                If arr1 IsNot Nothing AndAlso arr1.Count > 0 Then
                    If clsPurchaseOrderCFORMDetail.SaveData_CFORM(txtDocNo.Value, arr1, Nothing) Then
                        Dim qry As String = "update tspl_purchase_order_head set issue_c_form ='1' where purchaseorder_no='" + txtDocNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    End If
                End If
                clsCommon.MyMessageBoxShow(Me, "Forms Detail Updated Successfully", Me.Text)
            Else
                Dim str As String = "select count(*) from tspl_srn_head where Against_PO='" + txtDocNo.Value + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(str)

                If check > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Forms Detail Not Updated,First delete SRN", Me.Text)
                Else
                    If Not Chkroadpermit.Checked Then
                        Dim qry As String = "update tspl_purchase_order_head set issue_road_permit='0' where purchaseorder_no='" + txtDocNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)

                        qry = "delete from TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL where purchaseorder_no='" + txtDocNo.Value + "' and isnull(srn_no,'')='' and form_code in (select form_code from tspl_form_master where form_type in ('38-Inward','38-Outward','Others'))"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    End If
                    If Not chk_c_form.Checked Then
                        Dim qry As String = "update tspl_purchase_order_head set issue_c_form='0' where purchaseorder_no='" + txtDocNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)

                        qry = "delete from TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL where purchaseorder_no='" + txtDocNo.Value + "' and isnull(srn_no,'')='' and form_code in (select form_code from tspl_form_master where form_type in ('C','Others'))"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    End If
                    clsCommon.MyMessageBoxShow(Me, "Forms Detail Updated Successfully", Me.Text)
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ''richa agarwal --------------
    Private Sub fndPaymenttermsGroup__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndPaymenttermsGroup._MYValidating

        fndPaymenttermsGroup.Value = ClsMerchantPaymentTermsGroup.getFinder("", fndPaymenttermsGroup.Value, isButtonClicked)
        lblpaymenttermsgroup.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT where Group_Code='" & fndPaymenttermsGroup.Value & "'"))
        MTEnableDisablepaymentterms()
        'If clsCommon.myLen(fndPaymenttermsGroup.Value) > 0 Then
        '    txtAdvance.Enabled = False
        '    TxtLC.Enabled = False
        '    TxtBalancePayment.Enabled = False
        '    TxtCAD.Enabled = False
        '    TxtOnAccount.Enabled = False
        '    txtRetained.Enabled = False

        '    dt = clsDBFuncationality.GetDataTable("Select Terms_Code from TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT where Group_Code='" + fndPaymenttermsGroup.Value + "'")
        '    If dt.Rows.Count > 0 Then
        '        For Each drrow As DataRow In dt.Rows
        '            Dim Strtype As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TermsType from TSPL_PAYMENT_TERMS_Master_MT WHERE Code='" & drrow("Terms_Code") & "' "))
        '            If clsCommon.CompairString(Strtype, "L") = CompairStringResult.Equal Then
        '                TxtLC.Enabled = True
        '            ElseIf clsCommon.CompairString(Strtype, "C") = CompairStringResult.Equal Then
        '                TxtCAD.Enabled = True
        '            ElseIf clsCommon.CompairString(Strtype, "A") = CompairStringResult.Equal Then
        '                txtAdvance.Enabled = True
        '            ElseIf clsCommon.CompairString(Strtype, "B") = CompairStringResult.Equal Then
        '                TxtBalancePayment.Enabled = True
        '            ElseIf clsCommon.CompairString(Strtype, "O") = CompairStringResult.Equal Then
        '                TxtOnAccount.Enabled = True
        '            ElseIf clsCommon.CompairString(Strtype, "R") = CompairStringResult.Equal Then
        '                txtRetained.Enabled = True
        '            End If
        '        Next
        '    End If
        'End If
    End Sub

    Private Sub MTEnableDisablepaymentterms()
        If clsCommon.myLen(fndPaymenttermsGroup.Value) > 0 Then
            txtAdvance.Enabled = False
            TxtLC.Enabled = False
            TxtBalancePayment.Enabled = False
            TxtCAD.Enabled = False
            TxtOnAccount.Enabled = False
            txtRetained.Enabled = False
            ''richa agarwal 09/04/2015
            TxtCIF.Enabled = False
            ''----------------------
            Dim dt As DataTable = Nothing
            dt = clsDBFuncationality.GetDataTable("Select Terms_Code from TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT where Group_Code='" + fndPaymenttermsGroup.Value + "'")
            If dt.Rows.Count > 0 Then
                For Each drrow As DataRow In dt.Rows
                    Dim Strtype As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TermsType from TSPL_PAYMENT_TERMS_Master_MT WHERE Code='" & drrow("Terms_Code") & "' "))
                    If clsCommon.CompairString(Strtype, "L") = CompairStringResult.Equal Then
                        TxtLC.Enabled = True
                    ElseIf clsCommon.CompairString(Strtype, "C") = CompairStringResult.Equal Then
                        TxtCAD.Enabled = True
                    ElseIf clsCommon.CompairString(Strtype, "A") = CompairStringResult.Equal Then
                        txtAdvance.Enabled = True
                    ElseIf clsCommon.CompairString(Strtype, "B") = CompairStringResult.Equal Then
                        TxtBalancePayment.Enabled = True
                    ElseIf clsCommon.CompairString(Strtype, "O") = CompairStringResult.Equal Then
                        TxtOnAccount.Enabled = True
                    ElseIf clsCommon.CompairString(Strtype, "R") = CompairStringResult.Equal Then
                        txtRetained.Enabled = True
                        ''richa agarwal 09/04/2015
                    ElseIf clsCommon.CompairString(Strtype, "CI") = CompairStringResult.Equal Then
                        TxtCIF.Enabled = True
                        ''----------------------
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub TxtBeneficiary__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles TxtBeneficiary._MYValidating
        TxtBeneficiary.Value = txtVendorNo.Value
        lblBeneficiary.Text = lblVendorName.Text
        Dim qry As String = ""
        Dim whrCls As String = ""
        whrCls = " TSPL_VENDOR_MASTER.Status='N' and  TSPL_VENDOR_MASTER.CURRENCY_CODE<>(select isnull(BaseCurrencyCode,'')  from TSPL_COMPANY_MASTER where Comp_Code ='" & objCommonVar.CurrentCompanyCode & "' )"
        qry = "select Vendor_Code as Code,Vendor_Name as Name,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_VENDOR_MASTER.City_Code_Desc)>0 then ', '+TSPL_VENDOR_MASTER.City_Code_Desc else ' ' end + case when len(TSPL_VENDOR_MASTER.State )>0 then TSPL_VENDOR_MASTER.State else '' end  as Address,Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description] from TSPL_VENDOR_MASTER "
        TxtBeneficiary.Value = clsCommon.ShowSelectForm("POBenefieceryFndr", qry, "Code", whrCls, TxtBeneficiary.Value, "Code", isButtonClicked)
        qry = "select  Vendor_Code,Vendor_Name,Terms_Code,Terms_Code_Desc ,Vendor_Account ,Tax_Group,Tax_Group_Desc from TSPL_VENDOR_MASTER where Vendor_Code ='" + TxtBeneficiary.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblBeneficiary.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            SetMultiCurrencyVisibility()
        Else
            lblBeneficiary.Text = ""
        End If
    End Sub

    Private Sub chkIsMerchantTrade_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkIsMerchantTrade.ToggleStateChanged
        If chkIsMerchantTrade.Checked Then
            If clsModuleCurrencyMapping.CheckMultiCurrency("MMTSALE") = True Then
                RadPageView1.Pages("RdPaymentterms").Item.Visibility = ElementVisibility.Visible
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
                cboPOType.SelectedValue = "I"
                cboPOType.Enabled = False
                gv1.Columns(colFatPerMT).IsVisible = True
                gv1.Columns(colSNFFPerMT).IsVisible = True
                gv1.Columns(colFatKGMT).IsVisible = True
                gv1.Columns(colSNFKGMT).IsVisible = True
                gv1.Columns(colItemWeightMT).IsVisible = True
                gv1.Columns(colWeightUOMMT).IsVisible = True
            Else
                chkIsMerchantTrade.Checked = False
                clsCommon.MyMessageBoxShow(Me, "Please on multicurrency for merchant trade", Me.Text)
                cboPOType.Enabled = True
            End If
        Else
            RadPageView1.Pages("RdPaymentterms").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
            cboPOType.Enabled = True
            gv1.Columns(colFatPerMT).IsVisible = False
            gv1.Columns(colSNFFPerMT).IsVisible = False
            gv1.Columns(colFatKGMT).IsVisible = False
            gv1.Columns(colSNFKGMT).IsVisible = False
        End If
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        txtBillToLocation.Value = ""
        lblBillToLocation.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        gv2.Rows.Clear()
    End Sub

    Sub LoadMTPIStatus()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("", "Select")
        dt.Rows.Add("A", "Accepted")
        dt.Rows.Add("N", "Not Accepted  ")

        cboPIStatus.DataSource = dt
        cboPIStatus.ValueMember = "Code"
        cboPIStatus.DisplayMember = "Name"
    End Sub

    Private Sub btnMTUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMTUpdate.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim obj As New clsPurchaseOrderHead
                obj.PurchaseOrder_No = clsCommon.myCstr(txtDocNo.Value)
                obj.MT_PI_Status = clsCommon.myCstr(cboPIStatus.SelectedValue)
                If txtPIDate.Checked Then
                    obj.MT_PI_Status_Date = txtPIDate.Value
                Else
                    obj.MT_PI_Status_Date = Nothing
                End If

                If clsPurchaseOrderHead.UpdateAfterPosting(obj, Nothing) Then
                    clsCommon.MyMessageBoxShow(Me, "Information updated successfully.", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ''------------richa code ends here----------------------



    Private Function PORenewal(ByVal obj As clsPurchaseOrderHead) As Boolean
        Dim isSaved As Boolean = True

        Try
            If Not clsCommon.MyMessageBoxShow(Me, "Renew the current Purchase Order,Are You sure?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                isSaved = False
                Exit Function
            End If

            Dim qry As String = "select count(*) from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No='" + txtDocNo.Value + "' and CONVERT(date,expiry_date,103)<convert(date,'" + dtpRenewal.Text + "',103)"
            Dim check As Integer = CInt(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)))

            If check <= 0 Then
                If Not clsCommon.MyMessageBoxShow(Me, "Current document is not expired,would you like to continue?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    isSaved = False
                    Exit Function
                End If
            End If

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PurchaseOrder_No) > 0 Then
                obj.PurchaseOrder_No = ""
                obj.PurchaseOrder_Date = clsCommon.myCDate(dtpRenewal.Text)
                obj.Against_Requisition = ""
                obj.Against_PO = ""
                obj.Renewal_Date = ""

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For ii As Integer = 0 To obj.Arr.Count - 1
                        obj.Arr.Item(ii).Requisition_Id = ""
                        obj.Arr.Item(ii).MRN_No = ""
                    Next
                End If


                Dim objrenew As New clsPurchaseOrderHead()

                isSaved = isSaved AndAlso objrenew.SaveData(obj, True, False)

                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("Update tspl_purchase_order_head set Renewal_Date='" + clsCommon.GetPrintDate(dtpRenewal.Text, "dd/MMM/yyyy") + "',Against_PO='" + obj.PurchaseOrder_No + "' where purchaseorder_no='" + txtAgainstPO_No.Text + "'")

                txtDocNo.Value = objrenew.PurchaseOrder_No
            End If

            Return isSaved
        Catch ex As Exception
            isSaved = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub dtpRenewal_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles dtpRenewal.ToggleStateChanged
        If clsCommon.CompairString(btnSave.Text, "Save") <> CompairStringResult.Equal Then
            btnSave.Enabled = dtpRenewal.Checked
            If Not btnPost.Enabled AndAlso Not dtpRenewal.Checked Then 'if posted and not renew then save disable
                btnSave.Enabled = False
            End If
            If btnPost.Enabled AndAlso Not dtpRenewal.Checked Then 'if not posted and not renew then save enable
                btnSave.Enabled = True
            End If
            If dtpRenewal.Checked Then
                txtAgainstPO_No.Text = txtDocNo.Value
            Else
                txtAgainstPO_No.Text = ""
            End If
        End If
    End Sub

    Private Sub txtDelivery_Code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDelivery_Code._MYValidating
        txtDelivery_Code.Value = clsCommon.ShowSelectForm("PODELVRYTRMFND", "select Code,Description from TSPL_DELIVERY_TERMS_MASTER ", "Code", "", txtDelivery_Code.Value, "Code", isButtonClicked)
        txtDeliveryDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_DELIVERY_TERMS_MASTER where code='" + txtDelivery_Code.Value + "'"))
    End Sub

    Sub SetRateDescCols()
        If IsFormLoad = True Then
            Exit Sub
        End If

        If ShowItemInCaseofNonInventory = False Then
            If clsCommon.CompairString(Me.cboPOType.SelectedValue, "J") = CompairStringResult.Equal And clsCommon.CompairString(Me.cboItemType.SelectedValue, "N") = CompairStringResult.Equal Then
                ''BHA/28/08/18-000486 by balwinder on 29/08/2018
                Me.chkAutoCalculate.Enabled = True

                If chkAutoCalculate.Checked = False Then
                    gv1.Columns(colQty_Desc).IsVisible = True
                    gv1.Columns(colRate_Desc).IsVisible = True
                    gv1.Columns(colAmount_Desc).IsVisible = False
                    'gv1.Columns(colAmt).ReadOnly = False

                    'gv1.Columns(colQty).ReadOnly = True
                    'gv1.Columns(colRate).ReadOnly = True
                Else
                    gv1.Columns(colQty_Desc).IsVisible = False
                    gv1.Columns(colRate_Desc).IsVisible = False
                    gv1.Columns(colAmount_Desc).IsVisible = False
                    'gv1.Columns(colAmt).ReadOnly = True

                    'gv1.Columns(colQty).ReadOnly = False
                    'gv1.Columns(colRate).ReadOnly = False
                End If
            Else

                Me.chkAutoCalculate.Enabled = False
                gv1.Columns(colQty_Desc).IsVisible = False
                gv1.Columns(colRate_Desc).IsVisible = False
                gv1.Columns(colAmount_Desc).IsVisible = False
                'gv1.Columns(colAmt).ReadOnly = True

                'gv1.Columns(colQty).ReadOnly = False
                'gv1.Columns(colRate).ReadOnly = False
            End If
        End If



    End Sub

    Private Sub cboItemType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboItemType.SelectedValueChanged
        SetRateDescCols()
        If Not IsFormLoad Then
            If clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboPOType.SelectedValue, "J") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(cboPOType.SelectedValue, "") <> CompairStringResult.Equal Then

                cboPOType.SelectedValue = ""
                clsCommon.MyMessageBoxShow(Me, "Non-Inventory applicable for job-work only.", Me.Text)

            End If
        End If
    End Sub

    Private Sub chkAutoCalculate_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAutoCalculate.ToggleStateChanged
        SetRateDescCols()
    End Sub

    'richa agarwal 08/04/2015 against ticket no BM00000006178 15/04/2015
    Private Sub txtPINo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPINo._MYValidating
        Dim qry As String = "SELECT TSPL_EX_PI_HEAD.Document_Code as Code,convert(varchar,TSPL_EX_PI_HEAD.Document_Date ,103) as 'PI Date',TSPL_EX_PI_HEAD.Customer_Code as 'Customer Code',TSPL_CUSTOMER_MASTER.Customer_Name as 'Customer Name',TSPL_EX_PI_HEAD.Bill_To_Location as 'Bill to Location Code',TSPL_LOCATION_MASTER.Location_Desc as 'Bill to Location Name' FROM TSPL_EX_PI_HEAD Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_EX_PI_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER  on TSPL_EX_PI_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code"
        Dim strwhrcls As String = " TSPL_EX_PI_HEAD.Status=1 And Document_Type='MT' AND not exists (Select 1 from TSPL_PURCHASE_ORDER_HEAD where isnull(TSPL_PURCHASE_ORDER_HEAD.MT_PI_No,'')=TSPL_EX_PI_HEAD.Document_Code and isnull(TSPL_PURCHASE_ORDER_HEAD.MT_PI_No,'')<>'" & txtPINo.Value & "') and not exists (Select 1 from TSPL_LC_REQUEST_MT where isnull(TSPL_LC_REQUEST_MT.PurchaseInvoice_No,'')=TSPL_EX_PI_HEAD.Document_Code and isnull(TSPL_LC_REQUEST_MT.PurchaseInvoice_No,'')<>'" & txtPINo.Value & "')"
        txtPINo.Value = clsCommon.ShowSelectForm("PerformaInvoiceExport", qry, "Code", strwhrcls, txtPINo.Value, "Code", isButtonClicked)
        If clsCommon.myLen(txtPINo.Value) > 0 Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select TSPL_EX_PI_HEAD.Cust_PO_No,TSPL_EX_PI_HEAD.Cust_PO_Date,TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Code,TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Desc,TSPL_EX_PI_HEAD.MT_Is_AmountinRs,TSPL_EX_PI_HEAD.MT_LC,TSPL_EX_PI_HEAD.MT_CAD,TSPL_EX_PI_HEAD.MT_RETAINED,TSPL_EX_PI_HEAD.MT_Balance_Payment,TSPL_EX_PI_HEAD.MT_On_Account,TSPL_EX_PI_HEAD.MT_Advance,TSPL_EX_PI_HEAD.MT_CIF,TSPL_EX_PI_HEAD.MT_INCOTERMS,TSPL_EX_PI_HEAD.MT_HS_Classification_No,TSPL_EX_PI_HEAD.Item_Type,TSPL_EX_PI_HEAD.Pre_Carriage_By,TSPL_EX_PI_HEAD.Carrier ,TSPL_EX_PI_HEAD.Discharge_Port ,TSPL_EX_PI_HEAD.Final_Destination ,TSPL_EX_PI_HEAD.Origin_Country,TSPL_EX_PI_HEAD.Final_Destination_Country,TSPL_EX_PI_HEAD.Terms_Code,TSPL_TERMS_MASTER.Terms_Desc ,TSPL_EX_PI_HEAD.Due_Date ,TSPL_EX_PI_HEAD.Stuffing_Status ,TSPL_EX_PI_HEAD.EX_Term_Code,TSPL_EX_PI_HEAD.Payment_Terms,TSPL_EX_PI_HEAD.is_Accepted,TSPL_EX_PI_HEAD.Accepted_Date,TSPL_EX_PI_HEAD.is_Partshipment ,TSPL_EX_PI_HEAD.is_Transshipment,TSPL_EX_PI_HEAD.Bill_To_Location  from TSPL_EX_PI_HEAD Left Outer Join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_EX_PI_HEAD.Terms_Code where Document_Code ='" & txtPINo.Value & "'")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Cust_PO_Date")) = True Then
                    TxtBuyerPODate.Checked = False
                    TxtBuyerPODate.Value = clsCommon.GETSERVERDATE(Nothing)
                Else
                    TxtBuyerPODate.Checked = True
                    TxtBuyerPODate.Value = clsCommon.GetPrintDate(dt.Rows(0)("Cust_PO_Date"), "dd/MMM/yyyy")
                End If
                TxtBuyerPONo.Text = clsCommon.myCstr(dt.Rows(0)("Cust_PO_No"))
                chkAcceptance.Checked = clsCommon.myCBool(IIf(clsCommon.myCstr(dt.Rows(0)("is_Accepted")) = "Y", True, False))
                dtpAcceptance.Value = clsCommon.myCDate(dt.Rows(0)("Accepted_Date"))
                chkPartshipment.Checked = clsCommon.myCBool(IIf(clsCommon.myCstr(dt.Rows(0)("is_Partshipment")) = "Y", True, False))
                chkTransshipment.Checked = clsCommon.myCBool(IIf(clsCommon.myCstr(dt.Rows(0)("is_Transshipment")) = "Y", True, False))
                cmbTerms.SelectedValue = clsCommon.myCstr(dt.Rows(0)("EX_Term_Code"))
                cmbTerms_Payment.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Payment_Terms"))
                cboStuffing.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Stuffing_Status"))
                txtPIDate.Value = clsCommon.myCDate(dt.Rows(0)("Due_Date"))
                FndCreditTerms.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                TxtCreditTermsName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
                txtPre_Carriage_By.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Pre_Carriage_By"))
                txtPort_Discharge.Text = clsCommon.myCstr(dt.Rows(0)("Discharge_Port"))
                txtFinal_Destination.Text = clsCommon.myCstr(dt.Rows(0)("Final_Destination"))
                fndCountry_Final_Destination.Value = clsCommon.myCstr(dt.Rows(0)("Final_Destination_Country"))
                fndCountry_Origin.Value = clsCommon.myCstr(dt.Rows(0)("Origin_Country"))
                txtCarrier.Text = clsCommon.myCstr(dt.Rows(0)("Carrier"))
                cboItemType.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
                ''richa agarwal 15/04/2015
                TxtHSClassificationNo.Text = clsCommon.myCstr(dt.Rows(0)("MT_HS_Classification_No"))
                fndPaymenttermsGroup.Value = clsCommon.myCstr(dt.Rows(0)("MT_Payment_Terms_Group_Code"))
                lblpaymenttermsgroup.Text = clsCommon.myCstr(dt.Rows(0)("MT_Payment_Terms_Group_Desc"))
                'MTEnableDisablepaymentterms()
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("MT_Is_AmountinRs")), "1") = CompairStringResult.Equal Then
                    rdbAmountinrupees.Checked = True
                Else
                    rdbAmountinpercentage.Checked = True
                End If
                TxtLC.Value = clsCommon.myCdbl(dt.Rows(0)("MT_LC"))
                TxtCAD.Value = clsCommon.myCdbl(dt.Rows(0)("MT_CAD"))
                TxtOnAccount.Value = clsCommon.myCdbl(dt.Rows(0)("MT_On_Account"))
                TxtBalancePayment.Value = clsCommon.myCdbl(dt.Rows(0)("MT_Balance_Payment"))
                txtRetained.Value = clsCommon.myCdbl(dt.Rows(0)("MT_RETAINED"))
                TxtCIF.Value = clsCommon.myCdbl(dt.Rows(0)("MT_CIF"))
                txtAdvance.Value = clsCommon.myCdbl(dt.Rows(0)("MT_Advance"))
                TxtINCOTERMS.Text = clsCommon.myCstr(dt.Rows(0)("MT_INCOTERMS"))
                ''-------------------------------------------
                chkAcceptance.Enabled = False
                dtpAcceptance.Enabled = False
                chkPartshipment.Enabled = False
                chkTransshipment.Enabled = False
                cmbTerms.Enabled = False
                cmbTerms_Payment.Enabled = False
                cboStuffing.Enabled = False
                txtPIDueDate.Enabled = False
                FndCreditTerms.Enabled = False
                TxtCreditTermsName.Enabled = False
                txtPre_Carriage_By.Enabled = False
                txtPort_Discharge.Enabled = False
                txtFinal_Destination.Enabled = False
                fndCountry_Final_Destination.Enabled = False
                fndCountry_Origin.Enabled = False
                txtCarrier.Enabled = False
                cboItemType.Enabled = False
                ' TxtBuyerPONo.Enabled = False
                ' TxtBuyerPODate.Enabled = False
                ''richa agarwal 15/04/2015
                TxtHSClassificationNo.Enabled = False
                fndPaymenttermsGroup.Enabled = False
                rdbAmountinrupees.Enabled = False
                rdbAmountinpercentage.Enabled = False
                TxtLC.Enabled = False
                TxtCAD.Enabled = False
                TxtOnAccount.Enabled = False
                TxtBalancePayment.Enabled = False
                txtRetained.Enabled = False
                TxtCIF.Enabled = False
                txtAdvance.Enabled = False
                TxtINCOTERMS.Enabled = False

                txtBillToLocation.Value = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
                lblBillToLocation.Text = clsLocation.GetName(txtBillToLocation.Value, Nothing)
                ''-------------------------------------------
                funFilldataintoGridFromPI(txtPINo.Value)
            Else
                ResetControlsonPaymentTerms()
            End If
            dt = Nothing
        Else
            ResetControlsonPaymentTerms()

        End If
        qry = ""
        strwhrcls = ""
    End Sub

    Sub funFilldataintoGridFromPI(ByVal strPINO As String)
        LoadBlankGrid()
        LoadBlankGridSchedule()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select TSPL_EX_PI_DETAIL.Bin_No,TSPL_EX_PI_DETAIL.Specification ,TSPL_EX_PI_DETAIL.Remarks,TSPL_ITEM_MASTER.Item_Desc,TSPL_EX_PI_DETAIL.MRP ,TSPL_EX_PI_DETAIL.Line_No, TSPL_EX_PI_DETAIL.Row_Type,TSPL_EX_PI_DETAIL.Item_Code ,TSPL_EX_PI_DETAIL.Qty,TSPL_EX_PI_DETAIL.Unit_code ,TSPL_EX_PI_DETAIL.Location ,TSPL_EX_PI_DETAIL.Item_Cost ,TSPL_EX_PI_DETAIL.Amount ,TSPL_EX_PI_DETAIL.Disc_Per ,TSPL_EX_PI_DETAIL.Disc_Amt ,TSPL_EX_PI_DETAIL.Amt_Less_Discount ,TSPL_EX_PI_DETAIL.Item_Net_Amt ,TSPL_EX_PI_DETAIL.Conv_Factor,TSPL_EX_PI_DETAIL.Weight_UOM ,TSPL_EX_PI_DETAIL.Item_Weight from TSPL_EX_PI_DETAIL Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_EX_PI_DETAIL.Item_Code where DOCUMENT_CODE ='" & strPINO & "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(dt.Rows(i)("Line_No"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsCommon.myCstr(dt.Rows(i)("Row_Type"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dt.Rows(i)("Item_Desc"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dt.Rows(i)("Item_Code")), Nothing)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(clsCommon.myCstr(dt.Rows(i)("Item_Code")), Nothing)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dt.Rows(i)("Unit_code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dt.Rows(i)("Item_Cost"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = clsCommon.myCdbl(dt.Rows(i)("Amount"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = clsCommon.myCdbl(dt.Rows(i)("Disc_Per"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmt).Value = clsCommon.myCdbl(dt.Rows(i)("Disc_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterDis).Value = clsCommon.myCdbl(dt.Rows(i)("Amt_Less_Discount"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterTax).Value = clsCommon.myCdbl(dt.Rows(i)("Item_Net_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colWeightUOMMT).Value = clsCommon.myCstr(dt.Rows(i)("Weight_UOM"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemWeightMT).Value = clsCommon.myCdbl(dt.Rows(i)("Item_Weight"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = clsCommon.myCdbl(dt.Rows(i)("MRP"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = clsCommon.myCstr(dt.Rows(i)("Bin_No"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = clsCommon.myCstr(dt.Rows(i)("Remarks"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = clsCommon.myCstr(dt.Rows(i)("Specification"))
                gv1.ReadOnly = True
            Next
        End If
    End Sub

    Private Sub fndCountry_Origin__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCountry_Origin._MYValidating
        fndCountry_Origin.Value = clsCountryMaster.getFinder("", fndCountry_Origin.Value, isButtonClicked)
    End Sub

    Private Sub fndCountry_Final_Destination__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCountry_Final_Destination._MYValidating
        fndCountry_Final_Destination.Value = clsCountryMaster.getFinder("", fndCountry_Final_Destination.Value, isButtonClicked)
    End Sub

    Private Sub FndCreditTerms__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndCreditTerms._MYValidating
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER"
        FndCreditTerms.Value = clsCommon.ShowSelectForm("EXPTERMFNDMt", qry, "Code", "", FndCreditTerms.Value, "Code", isButtonClicked)
        SetTermDetailsMT()
    End Sub

    Sub SetTermDetailsMT()
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER where Terms_Code='" + FndCreditTerms.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            TxtCreditTermsName.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
            txtPIDueDate.Value = txtDate.Value.AddDays(clsCommon.myCdbl(dt.Rows(0)("No Of Days")))
        Else
            TxtCreditTermsName.Text = ""
        End If
    End Sub

    Private Sub cmbTerms_Payment_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbTerms_Payment.SelectedValueChanged
        If Not isInsideLoadDatamt Then
            Exit Sub
        End If
        If clsCommon.myLen(cmbTerms_Payment.SelectedValue) > 0 AndAlso clsCommon.CompairString(cmbTerms_Payment.SelectedValue, "AD") = CompairStringResult.Equal Then
            txtAdvance_Pers.Enabled = True
            cmbAdvanceType.Enabled = True
        Else
            txtAdvance_Pers.Enabled = False
            txtAdvance_Pers.Value = 0
            cmbAdvanceType.Enabled = False
            cmbAdvanceType.SelectedValue = ""
        End If
    End Sub

    Private Sub LoadTerms()
        Dim qry As String = "select '' as Code,'None' as Name union all select 'CIF' as Code,'Cost, Insurance & Freight' as Name union all select 'CFR' as Code,'Cost & Freight' as Name union all select 'FOB' as Code,'Free on Board' as Name" ' union all select 'C&F' as Code,'Cost & Freight' as Name
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cmbTerms.DataSource = Nothing
        cmbTerms.DataSource = dt

        cmbTerms.ValueMember = "Code"
        cmbTerms.DisplayMember = "Name"
    End Sub

    Private Sub LoadTerms_of_Payment()
        Dim qry As String = "select '' as Code,'None' as Name union all select 'LC' as Code,'Letter of Credit' as Name union all select 'DA' as Code,'Document against Acceptance' as Name union all select 'DP' as Code,'Document against Payment' as Name  union all select 'AD' as Code,'Advance' as Name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            cmbTerms_Payment.DataSource = dt

            cmbTerms_Payment.ValueMember = "Code"
            cmbTerms_Payment.DisplayMember = "Name"
        End If

    End Sub

    Sub LoadPreCarriageBy()
        Dim qry As String = "select 'By Road' as Code,'By Road' as Name union all select 'By Air' as Code,'By Air' as Name union all select 'By Sea' as Code,'By Sea' as Name union all select 'By Rail' as Code,'By Rail' as Name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        txtPre_Carriage_By.DataSource = Nothing
        txtPre_Carriage_By.DataSource = dt
        txtPre_Carriage_By.DisplayMember = "Name"
        txtPre_Carriage_By.ValueMember = "Code"
    End Sub

    Sub LoadStuffing()
        Dim qry As String = "select '' as Code,'None' as Name union all select '1' as Code,'Done' as Name union all select '0' as Code,'Not-Done' as Name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cboStuffing.DataSource = Nothing

        cboStuffing.DataSource = dt
        cboStuffing.DisplayMember = "Name"
        cboStuffing.ValueMember = "Code"
    End Sub

    Sub LoadAdvanceType()
        Dim qry As String = "select '' as Code,'None' as Name union all select 'Amount' as Code,'Amount' as Name union all select 'Percentage' as Code,'Percentage' as Name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cmbAdvanceType.DataSource = Nothing

        cmbAdvanceType.DataSource = dt
        cmbAdvanceType.DisplayMember = "Name"
        cmbAdvanceType.ValueMember = "Code"
    End Sub

    Private Sub cmbAdvanceType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbAdvanceType.SelectedValueChanged
        If Not isInsideLoadDatamt Then
            Exit Sub
        End If
        If clsCommon.myLen(cmbAdvanceType.SelectedValue) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(cmbAdvanceType.SelectedValue), "Amount") = CompairStringResult.Equal Then
            ChkPartPayment.Enabled = True
        Else
            ChkPartPayment.Enabled = False
            ChkPartPayment.Checked = False
        End If
    End Sub

    Sub ResetControlsonPaymentTerms()
        cmbTerms.SelectedValue = ""
        cmbTerms_Payment.SelectedValue = ""
        cboStuffing.SelectedValue = ""
        cmbAdvanceType.SelectedValue = ""
        txtPIDueDate.Value = clsCommon.GETSERVERDATE(Nothing)
        FndCreditTerms.Value = ""
        TxtCreditTermsName.Text = ""
        txtPre_Carriage_By.SelectedValue = ""
        txtPort_Discharge.Text = ""
        txtFinal_Destination.Text = ""
        fndCountry_Final_Destination.Value = ""
        fndCountry_Origin.Value = ""
        txtCarrier.Text = ""
        gv1.ReadOnly = False

        txtAdvance_Pers.Value = 0
        ChkPartPayment.Enabled = False
        ChkPartPayment.Checked = False
        chkAcceptance.Checked = False
        dtpAcceptance.Value = clsCommon.GETSERVERDATE(Nothing)
        chkPartshipment.Checked = False
        chkTransshipment.Checked = False
        TxtLC.Value = 0
        TxtOnAccount.Value = 0
        txtRetained.Value = 0
        txtAdvance.Value = 0
        TxtBalancePayment.Value = 0
        TxtCAD.Value = 0
        TxtCIF.Value = 0
        LoadMTPIStatus()
        txtPINo.Value = ""
        TxtHSClassificationNo.Text = ""
        txtPIDate.Checked = False
        txtPIDate.Value = clsCommon.GETSERVERDATE()
        cboPIStatus.SelectedValue = "Select"
        TxtBeneficiary.Value = ""
        lblBeneficiary.Text = ""
        TxtINCOTERMS.Text = ""
        fndPaymenttermsGroup.Value = ""
        lblpaymenttermsgroup.Text = ""
        chkAcceptance.Enabled = True
        dtpAcceptance.Enabled = True
        chkPartshipment.Enabled = True
        ' TxtBuyerPONo.Enabled = True
        '  TxtBuyerPODate.Enabled = True
        TxtBuyerPODate.Checked = False
        chkTransshipment.Enabled = True
        cmbTerms.Enabled = True
        cmbTerms_Payment.Enabled = True
        cboStuffing.Enabled = True
        txtPIDueDate.Enabled = True
        FndCreditTerms.Enabled = True
        TxtCreditTermsName.Enabled = True
        txtPre_Carriage_By.Enabled = True
        txtPort_Discharge.Enabled = True
        txtFinal_Destination.Enabled = True
        fndCountry_Final_Destination.Enabled = True
        TxtBuyerPODate.Value = clsCommon.GETSERVERDATE(Nothing)
        TxtBuyerPONo.Text = ""
        fndCountry_Origin.Enabled = True
        txtCarrier.Enabled = True
        cboItemType.Enabled = True
        ''richa agarwal 15/04/2015
        TxtHSClassificationNo.Enabled = True
        fndPaymenttermsGroup.Enabled = True
        rdbAmountinrupees.Enabled = True
        rdbAmountinpercentage.Enabled = True
        TxtLC.Enabled = True
        TxtCAD.Enabled = True
        TxtOnAccount.Enabled = True
        TxtBalancePayment.Enabled = True
        txtRetained.Enabled = True
        TxtCIF.Enabled = True
        txtAdvance.Enabled = True
        TxtINCOTERMS.Enabled = True
        LoadBlankGrid()
        LoadBlankGridSchedule()
        gv1.Rows.AddNew()
        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
        gvAC.Rows.AddNew()
        gvACInsurance.Rows.AddNew()
        gvCategoryValue.Rows.AddNew()
        gvTermsCdtion.Rows.AddNew()
    End Sub

    Private Sub txtDate_ValueChanged(sender As Object, e As EventArgs) Handles txtDate.ValueChanged
        If Not isInsideLoadData Then
            If clsCommon.myLen(txtVendorNo.Value) > 0 Then
                'txtDeliveryDate.Text = clsCommon.myCDate(txtDate.Text).AddDays(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TSPL_TERMS_MASTER.No_Days FROM TSPL_TERMS_MASTER WHERE TSPL_TERMS_MASTER.Terms_Code='" + clsCommon.myCstr(txtTermCode.Value) + "'")))
            End If
        End If
    End Sub

    Private Sub fndcapexsubcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndcapexsubcode._MYValidating
        Try
            lbl_capexcode.Text = ""
            fndcapexcode.Value = ""
            Me.fndcapexsubcode.Value = clsCapexBudget.getFinder("", fndcapexsubcode.Value, isButtonClicked)
            If clsCommon.myLen(Me.fndcapexsubcode.Value) > 0 Then
                lbl_capexsubcode.Text = clsCapexBudget.GetName(Me.fndcapexsubcode.Value, Nothing)
                fndcapexcode.Value = clsCapexBudget.GetCapexCode(Me.fndcapexsubcode.Value, Nothing)
                lbl_budgetamt.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                lbl_budgetamtwithtolerence.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                lbl_rebudgetamt.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, txtDocNo.Value, Nothing)
                lbl_rebudgetamtwithtolerence.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, txtDocNo.Value, Nothing)
                If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                    lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "PO Cancel"
                frm.strCode = "PO Cancel"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    Dim iscancel As Boolean = False
                    If clsCommon.MyMessageBoxShow(Me, "Do you want to cancel the PO?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        ''richa TEC/23/05/18-000257 12 June
                        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmPurchaseOrderMT) = CompairStringResult.Equal Then
                            Dim strSaleReturnNo As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(LCRequestNo) FROM TSPL_LC_REQUEST_MT WHERE Against ='Against PO' AND PurchaseOrder_No='" & txtDocNo.Value & "' "))
                            If strSaleReturnNo > 0 Then
                                Throw New Exception("PO can not be cancelled because it is used in LC Request.")
                            End If
                        End If

                        If clsPurchaseOrderHead.CheckPOUsedInSRNorGRN(clsCommon.myCstr(txtDocNo.Value), Nothing) Then
                            Throw New Exception("PO can not be cancelled because it is used in SRN/GRN.")
                        Else
                            If SaveData(False, True) Then
                                clsCommon.MyMessageBoxShow(Me, "PO cancelled successfully!", Me.Text)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ddl_category_SelectedValueChanged(sender As Object, e As EventArgs) Handles ddl_category.SelectedValueChanged
        If clsCommon.CompairString(clsCommon.myCstr(ddl_category.SelectedValue), "Capex") = CompairStringResult.Equal Then
            fndcapexsubcode.Enabled = True
        Else
            fndcapexsubcode.Enabled = False
        End If
    End Sub

    Private Sub txt_deliverydays_TextChanged(sender As Object, e As EventArgs) Handles txt_deliverydays.TextChanged
        If Not isInsideLoadData Then
            If clsCommon.myLen(txt_deliverydays.Text) > 0 Then
                txtDeliveryDate.Text = clsCommon.myCDate(txtDate.Text).AddDays(clsCommon.myCdbl(txt_deliverydays.Text))
            End If
        End If
    End Sub

    Private Sub txtBankCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBankCode._MYValidating
        Dim Qry As String = ""
        Dim strWhrclas As String = ""
        Qry = clsERPFuncationality.glbankqueryNew(strWhrclas)
        If isSettlementBankOnly Then
            strWhrclas += " and TSPL_BANK_MASTER.bank_type='S'"
        Else
            strWhrclas += "  and TSPL_BANK_MASTER.bank_type<>'S'"
        End If
        txtBankCode.Value = clsCommon.ShowSelectForm("BankSlctr@Payment", Qry, "Code", strWhrclas, txtBankCode.Value, "Code", isButtonClicked)
        lblBankDesc.Text = connectSql.RunScalar("select description from TSPL_BANK_MASTER where bank_code = '" + txtBankCode.Value + "'")
        txtPaymentMode.Value = connectSql.RunScalar("select TSPL_PAYMENT_CODE.Payment_Code   from TSPL_PAYMENT_CODE Where TSPL_PAYMENT_CODE.Payment_Code=  (select DISTINCT (case when Bank_type = 'C' THEN 'CASH' WHEN BANK_TYPE = 'B' THEN 'CHEQUE' WHEN BANK_TYPE = 'O' THEN 'OTHER' WHEN Bank_type = 'P' THEN 'PETTYCASH' END ) AS [Paymet Type] from TSPL_BANK_MASTER Where BANK_CODE='" + txtBankCode.Value + "' )")
    End Sub

    Private Sub txtPaymentMode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentMode._MYValidating
        Dim strbankcode As String
        If Not String.IsNullOrEmpty(connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + txtBankCode.Value + "'")) Then
            strbankcode = connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + txtBankCode.Value + "'")
            If strbankcode.Trim() = "C" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector1", Qry1, "PaymentMode", "PAYMENT_TYPE = 'CASH'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            ElseIf strbankcode.Trim() = "P" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector2", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Petty Cash'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            ElseIf strbankcode = "B" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector3", Qry1, "PaymentMode", "PAYMENT_TYPE IN ('Cheque', 'Other','NEFT','RTGS')", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            Else
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector4", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Other'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            End If
        End If
    End Sub

    Private Sub gv1_CurrentRowChanging(sender As Object, e As CurrentRowChangingEventArgs) Handles gv1.CurrentRowChanging

    End Sub

    Private Sub cboPOType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboPOType.SelectedIndexChanged
        Try
            If Not IsFormLoad Then
                If Not IsLoadOk Then
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboPOType.SelectedValue, "J") = CompairStringResult.Equal Then
                            gv1.Columns(colIName).ReadOnly = False
                        Else
                            gv1.Enabled = True
                            ResetControlsonPaymentTerms()
                        End If
                    End If
                End If
                If clsCommon.CompairString(cboPOType.SelectedValue, "J") = CompairStringResult.Equal Then ''BHA/27/06/18-000104,BHA/27/06/18-000103 By balwinder on 04/07/2018 
                    chkRepair.Visible = True
                Else
                    chkRepair.Visible = False
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub cboItemType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboItemType.SelectedIndexChanged
        Try
            If Not IsFormLoad Then
                If Not IsLoadOk Then
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboPOType.SelectedValue, "J") = CompairStringResult.Equal Then
                            'gv1.Columns(colIName).ReadOnly = False
                        Else
                            gv1.Enabled = True
                            ResetControlsonPaymentTerms()

                        End If
                    End If
                End If


            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvTerms_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvTermsCdtion.CurrentColumnChanged
        If gvTermsCdtion.RowCount > 0 Then
            Dim intCurrRow As Integer = gvTermsCdtion.CurrentRow.Index
            If intCurrRow = gvTermsCdtion.Rows.Count - 1 Then
                gvTermsCdtion.Rows.AddNew()
                gvTermsCdtion.CurrentRow = gvTermsCdtion.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvCategoryValue_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvCategoryValue.CurrentColumnChanged
        If gvCategoryValue.RowCount > 0 Then
            Dim intCurrRow As Integer = gvCategoryValue.CurrentRow.Index
            If intCurrRow = gvCategoryValue.Rows.Count - 1 Then
                gvCategoryValue.Rows.AddNew()
                gvCategoryValue.CurrentRow = gvCategoryValue.Rows(intCurrRow)
            End If
        End If
    End Sub

    Sub formatGrid_UDLWorkOrder()
        If clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboPOType.SelectedValue, "J") = CompairStringResult.Equal Then
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
            Next
            gv1.Columns(colICode).ReadOnly = False
            gv1.Columns(colAmt).ReadOnly = False

        Else
            LoadBlankGrid()
            LoadBlankGridSchedule()
            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
        End If
    End Sub

    Private Sub txtSubLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSubLocation._MYValidating
        Dim strLocations = String.Empty

        If clsCommon.myLen(txtBillToLocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Bill To location code before sub location", Me.Text)
            Exit Sub
        End If
        txtSubLocation.Value = clsLocation.getFinder("(Main_Location_Code='" & txtBillToLocation.Value & "' and Is_Jobwork=1 and isnull(Is_Sub_Location,'N')='Y')" & strLocations, txtSubLocation.Value, isButtonClicked)
        If clsCommon.myLen(txtSubLocation.Value) > 0 Then
            lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
        Else
            lblSubLocation.Text = ""
        End If
        strLocations = Nothing
    End Sub

    Private Sub chkJobWorkOutward_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkJobWorkOutward.ToggleStateChanged
        If chkJobWorkOutward.Checked = True Then
            txtSubLocation.Enabled = True
        Else
            txtSubLocation.Enabled = False
            txtSubLocation.Value = ""
            lblSubLocation.Text = ""
        End If
    End Sub

    ' Ticket No : KDI/02/05/18-000284 by Prabhakar
    Public Sub FillVendorDetails()
        lblRegisterOrUnregister.Text = clsVendorMaster.GetVendorRegisterORNonRegister(txtVendorNo.Value, Nothing)
        lblGstinNo.Text = clsVendorMaster.GetVendorGSTINNo(txtVendorNo.Value, Nothing)

        chkTDSApplied.Checked = clsVendorMaster.IsTCSApplied(txtVendorNo.Value, txtDate.Value, Nothing)
        If chkTDSApplied.Checked Then
            Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(PAN,'') as PAN from TSPL_VENDOR_MASTER where Vendor_Code='" & txtVendorNo.Value & "'"))
            If clsCommon.myLen(panno) > 0 Then
                chkTDSApplied.Tag = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforVendorWithPanNo, clsFixedParameterCode.TCSRateforVendorWithPanNo, Nothing))
            Else
                chkTDSApplied.Tag = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforVendorWithoutPanNo, clsFixedParameterCode.TCSRateforVendorWithoutPanNo, Nothing))
            End If
        End If

    End Sub

    Private Sub chkBlanket_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBlanket.ToggleStateChanged
        BlanketcontrolCheck()
    End Sub

    Private Sub chkOpenPO_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOpenPO.CheckStateChanged
        BlanketcontrolCheck()
    End Sub

    Sub BlanketcontrolCheck()
        If chkOpenPO.Checked OrElse chkBlanket.Checked Then
            lblAmt.Visible = True
            txtAmount.Visible = True
        Else
            lblAmt.Visible = False
            txtAmount.Visible = False
            txtAmount.Text = "0"
        End If
    End Sub

    Private Sub txtHeaderDiscountAmount_Validating(sender As Object, e As ComponentModel.CancelEventArgs) Handles txtHeaderDiscountAmount.Validating
        Try
            If txtHeaderDiscountAmount.Value > clsCommon.myCdbl(lblAmtWithDiscount.Text) Then
                txtHeaderDiscountAmount.Value = 0
                Throw New Exception("Header Discount amount can't be more than " + lblAmtWithDiscount.Text)
            End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim qry As String = "select '' as RowType,'' as Code,'' as Description,0 as [PO Qty],0 as [Unit Cost],0 as [Extd Cost]"
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Try

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()

            If transportSql.importExcel(gv1, "RowType", "Code", "Description", "PO Qty", "Unit Cost", "Extd Cost") Then

                clsCommon.ProgressBarPercentShow()

                Dim dt As New DataTable()
                dt = gv1.DataSource()
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                LoadBlankGrid()
                LoadBlankGridSchedule()
                For Each row As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsCommon.myCstr(row("RowType").ToString().Trim())
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(row("Code").ToString().Trim())
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(row("Description").ToString().Trim())
                    If clsCommon.CompairString(gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(clsCommon.myCstr(row("Code").ToString().Trim()), Nothing)

                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(row("Code").ToString().Trim()), Nothing)
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(row("PO Qty").ToString().Trim())
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(row("Unit Cost").ToString().Trim())
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = clsCommon.myCdbl(row("Extd Cost").ToString().Trim())


                Next
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfered Successfully.", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarPercentHide()

        End Try
    End Sub

    Sub SelectWorkEstimation(ByVal strDocNo As String)
        isInsideLoadData = True
        'Dim frm As New FrmPendingRequistion()
        'frm.VendorCode = txtVendorNo.Value
        'frm.VendorName = lblVendorName.Text
        'frm.strCurrCode = txtDocNo.Value
        'frm.SettingIndendFreePOClose = SettingIndendFreePOClose
        'frm.Against_Tendor = IIf(chkTender.Checked = True, "Y", "N")
        'If chkTender.Checked = True Then
        '    If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
        '        clsCommon.MyMessageBoxShow(Me,"Please select Vendor first")
        '        Exit Sub
        '    End If
        'End If
        'frm.ShowDialog()
        chk_emergency.Enabled = True
        'If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
        Dim objReq As clsWorkEstimationHead = clsWorkEstimationHead.GetData(strDocNo, NavigatorType.Current, "")
        If objReq IsNot Nothing AndAlso clsCommon.myLen(objReq.WorkEstimation_Id) > 0 Then
            chk_emergency.Checked = IIf(objReq.Emergency = 1, True, False)
            chk_emergency.Enabled = False
            If ShowCapexCodeandSubCode Then
                If clsCommon.CompairString(objReq.Category, "") = CompairStringResult.Equal Then
                    ddl_category.SelectedValue = clsCommon.myCstr("")
                    ddl_category.Enabled = True

                Else
                    ddl_category.SelectedValue = objReq.Category
                    ddl_category.Enabled = False
                End If
                fndcapexcode.Value = clsCommon.myCstr(objReq.Capex_Code)
                If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                    lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
                End If
                fndcapexsubcode.Value = clsCommon.myCstr(objReq.Capex_SubCode)
                If clsCommon.myLen(Me.fndcapexsubcode.Value) > 0 Then
                    lbl_capexsubcode.Text = clsCapexBudget.GetName(Me.fndcapexsubcode.Value, Nothing)
                    lbl_budgetamt.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                    lbl_budgetamtwithtolerence.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                    lbl_rebudgetamt.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, "", Nothing)
                    lbl_rebudgetamtwithtolerence.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, "", Nothing)
                End If
            End If
            chkOpenPO.Checked = objReq.Is_Open_PO
            dtpExpiryDate.MendatroryField = chkOpenPO.Checked '=Done By Monika
            cboPOType.SelectedValue = objReq.Requisition_Type

            If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                txtVendorNo.Value = objReq.ArrTr(0).Vendor_Code
                lblVendorName.Text = objReq.ArrTr(0).VendorName
                Dim qry As String = "select  Terms_Code,Terms_Code_Desc from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtVendorNo.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                    lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Code_Desc"))
                    'txtDeliveryDate.Text = clsCommon.myCDate(txtDate.Text).AddDays(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TSPL_TERMS_MASTER.No_Days FROM TSPL_TERMS_MASTER WHERE TSPL_TERMS_MASTER.Terms_Code='" + clsCommon.myCstr(dt.Rows(0)("Terms_Code")) + "'")))
                End If
            End If
            'If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
            txtBillToLocation.Value = objReq.Location
            lblBillToLocation.Text = objReq.LocationName
            If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
                txtBillToLocation.Enabled = False
            End If
            'End If
            If (clsCommon.myLen(txtDesc.Text) <= 0) Then
                txtDesc.Text = objReq.Description
            End If
            If (clsCommon.myLen(txtDesc.Text) <= 0) Then
                txtRemarks.Text = objReq.Remarks
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
            If (clsCommon.myLen(cboModeOfTransport.Text) <= 0) Then
                cboModeOfTransport.Text = objReq.Mode_Of_Transport
            End If
            If (clsCommon.myLen(txtRemarks.Text) <= 0) Then
                txtRemarks.Text = objReq.Remarks
            End If
            If (clsCommon.myLen(fndProject.Value) <= 0) Then
                fndProject.Value = objReq.PROJECT_ID
                lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
                fndProject.Enabled = False
                lblProject.Enabled = False
            End If
        End If
        If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
            gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
        End If
        For Each obj As clsWorkEstimationDetail In objReq.ArrTr
            'If Not IsItemExistInGrid(obj) Then
            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeMisc
            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
            'gv1.Rows(gv1.Rows.Count - 1).Cells(colReqistionNo).Value = obj.Requisition_Id
            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_Code
            If chkTender.Checked = True Then
                gv1.CurrentRow.Cells(colRate).Value = clsVendorItemDetail.GetRate(txtVendorNo.Value, obj.Item_Code, obj.Unit_Code, gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value)
            Else
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
            End If

            gv1.Rows(gv1.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(obj.Item_Code)
            ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = obj.Location
            ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = obj.LocationName
            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Balance_Qty
            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.Balance_Qty
            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgRequitionQty).Value = obj.Requisition_Qty
            gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = obj.Item_Net_Amt
            '==========Sanjeet(22/12/2016)=========================
            gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = obj.Specification
            gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = obj.Remarks
            gv1.Rows(gv1.Rows.Count - 1).Cells(colCapacity).Value = obj.Capacity
            gv1.Rows(gv1.Rows.Count - 1).Cells(colMake).Value = obj.Make
            gv1.Rows(gv1.Rows.Count - 1).Cells(colModel).Value = obj.Model
            '=============================================

            gv1.CurrentRow.Cells(colPrevPOQty).Value = PendingQtyforGRN(txtVendorNo.Value, obj.Item_Code, obj.Unit_Code)
            gv1.CurrentRow.Cells(colPrevPONO).Value = PendingPONOforGRN(txtVendorNo.Value, obj.Item_Code, obj.Unit_Code)

            GetLastRateItemWise(obj.Item_Code, gv1.Rows.Count - 1)

            SetitemWiseTaxSetting(True, True)
            UpdateCurrentRow(gv1.Rows.Count - 1)
            'End If
        Next
        'End If
        isInsideLoadData = False
        UpdateAllTotals()
        SetVendorTDSDetails()
        'RefreshReqNo()
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        If clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
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

    Private Sub btnViewTDSDetails_Click(sender As Object, e As EventArgs) Handles btnViewTDSDetails.Click
        ViewTDS()
    End Sub

    Sub ViewTDS()
        Try
            Dim frm As New FrmViewTDS()
            UpdateTDSAmount()
            frm.ObjIn = objRemittance
            frm.ShowDialog()
            'If (frm.ObjReturn IsNot Nothing) Then
            objRemittance = frm.ObjReturn
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetVendorTDSDetails()
        btnViewTDSDetails.Enabled = False
        objRemittance = Nothing
        Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(txtVendorNo.Value)
        If objVendor IsNot Nothing Then
            btnViewTDSDetails.Enabled = True
            Dim IncludeTax As String = clsDBFuncationality.getSingleValue("select ISNULL(TSPL_TDS_SECTION_MASTER.Include_Tax,'') AS Include_Tax from TSPL_TDS_VENDOR_DETAILS left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section where Vendor_Code='" + txtVendorNo.Value + "'")
            Dim appAmt As Double = 0
            If (clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal) Then
                appAmt = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
            Else
                appAmt = clsCommon.myCdbl(lblTotRAmt.Text)
            End If
            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objVendor.Nature_Of_Deduction, appAmt, Nothing, False, txtVendorNo.Value)
            If (objDedDetails IsNot Nothing) Then
                ''By Balwinder on 09/11/2016 against ticket no BM00000010070
                Dim isApplyTDS As Boolean = False
                Dim qry As String = "select Fiscal_Code,Start_Date,End_Date from TSPL_Fiscal_Year_Master where convert(date,'" + txtDate.Value + "',103)>=  convert(date,Start_Date,103)  and convert(date,'" + txtDate.Value + "',103)<=convert(date,End_Date,103) "
                Dim dtFY As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtFY Is Nothing OrElse dtFY.Rows.Count <= 0 Then
                    Throw New Exception("Please make fiscal year where document date exists")
                End If

                ''Check if any TDS entry found in Document Fiscal Year
                qry = "select top 1 Remittance_Code from TSPL_REMITTANCE  where Vendor_Code='" + txtVendorNo.Value + "' and convert(date, Document_Date,103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("Start_Date")), "dd/MMM/yyyy") + "' and  convert(date, Document_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("End_Date")), "dd/MMM/yyyy") + "' and Document_No not in ('" + txtDocNo.Value + "')"
                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                    isApplyTDS = True
                Else
                    qry = "select Cumm_Cutoff,Cumm_Cutoff_Document from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code='" + objVendor.Nature_Of_Deduction + "'"
                    dtTemp = clsDBFuncationality.GetDataTable(qry)
                    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        If clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) <= 0 AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) <= 0 Then
                            isApplyTDS = True
                        Else
                            qry = "select sum( " + IIf(clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal, "TSPL_VENDOR_INVOICE_HEAD.Amount_Less_Discount", "TSPL_VENDOR_INVOICE_HEAD.Document_Total") + ") as Document_Total from TSPL_VENDOR_INVOICE_HEAD where Vendor_Code='" + txtVendorNo.Value + "' and Document_Type in ('I','C') and Document_No not in ('" + txtDocNo.Value + "') and  convert(date, Invoice_Entry_Date,103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("Start_Date")), "dd/MMM/yyyy") + "' and  convert(date, Invoice_Entry_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("End_Date")), "dd/MMM/yyyy") + "' and TSPL_VENDOR_INVOICE_HEAD.Posting_Date is not null "
                            dblPreviousTDSAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                            If appAmt >= clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) > 0 Then
                                isApplyTDS = True
                            ElseIf (dblPreviousTDSAmt + appAmt) >= clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) > 0 Then
                                isApplyTDS = True
                            End If
                        End If
                    End If
                End If

                If isApplyTDS Then
                    objRemittance = New clsRemittance()
                    objRemittance.Branch_Code = objVendor.Branch_Code
                    objRemittance.Deduction_Code = objVendor.Nature_Of_Deduction
                    objRemittance.TDS_Per = objDedDetails.TDS
                    objRemittance.Surcharge_Per = objDedDetails.Surcharge
                    objRemittance.Edu_Cess_Per = objDedDetails.Educess
                    objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
                    objRemittance.IsTDSOverride = False
                    If isNewEntry Then
                        objRemittance.IsApplyTDS = True
                    Else
                        objRemittance.IsApplyTDS = clsPIRemittance.IsTDSApplied(txtDocNo.Value)
                    End If
                    objRemittance.Section_Code = objVendor.TDSSection
                    objRemittance.Section_Description = objVendor.TDSSectionDescription
                    objRemittance.Select_By = objVendor.VendorTypeCode
                    'objRemittance.Include_Tax = objVendor.Include_Tax

                    objRemittance.Fiscal_Year = clsCommon.myCstr(dtFY.Rows(0)("Fiscal_Code"))
                    objRemittance.Quarter = "First"
                End If
            End If
        End If
    End Sub
    Sub UpdateTDSAmount()
        If (objRemittance Is Nothing) Then
            SetVendorTDSDetails()
        End If
        If (objRemittance IsNot Nothing) Then
            Dim IncludeTax As String = clsDBFuncationality.getSingleValue("select ISNULL(TSPL_TDS_SECTION_MASTER.Include_Tax,'') AS Include_Tax from TSPL_TDS_VENDOR_DETAILS left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section where Vendor_Code='" + txtVendorNo.Value + "'")
            Dim applicableAmt As Double = 0
            If clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal Then
                applicableAmt = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
            Else
                applicableAmt = clsCommon.myCdbl(lblTotRAmt.Text)
            End If
            applicableAmt += dblPreviousTDSAmt


            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objRemittance.Deduction_Code, applicableAmt, Nothing, False, txtVendorNo.Value)
            If (objDedDetails IsNot Nothing AndAlso objRemittance.IsApplyTDS) Then
                objRemittance.TDS_Per = objDedDetails.TDS
                objRemittance.Surcharge_Per = objDedDetails.Surcharge
                objRemittance.Edu_Cess_Per = objDedDetails.Educess
                objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
            End If

            objRemittance.Vendor_Code = txtVendorNo.Value
            objRemittance.Vendor_Name = lblVendorName.Text
            objRemittance.Document_Date = txtDate.Value
            objRemittance.Document_Type = "I"
            objRemittance.Document_Amount = clsCommon.myCdbl(lblTotRAmt.Text)
            objRemittance.Calculated_TDS_Base = applicableAmt
            If Not objRemittance.IsTDSOverride Then
                objRemittance.Actual_TDS_Base = applicableAmt
            End If

            objRemittance.Calculated_TDS = (objRemittance.Calculated_TDS_Base * objRemittance.TDS_Per) / 100
            objRemittance.Actual_TDS = (objRemittance.Actual_TDS_Base * objRemittance.TDS_Per) / 100

            objRemittance.Calculated_Surcharge = (objRemittance.Calculated_TDS_Base * objRemittance.Surcharge_Per) / 100
            objRemittance.Actual_Surcharge = (objRemittance.Actual_TDS_Base * objRemittance.Surcharge_Per) / 100

            objRemittance.Calculated_Edu_Cess = (objRemittance.Calculated_TDS_Base * objRemittance.Edu_Cess_Per) / 100
            objRemittance.Actual_Edu_Cess = (objRemittance.Actual_TDS_Base * objRemittance.Edu_Cess_Per) / 100

            objRemittance.Calculated_Sec_Educess = (objRemittance.Calculated_TDS_Base * objRemittance.Sec_Educess_Per) / 100
            objRemittance.Actual_Sec_Educess = (objRemittance.Actual_TDS_Base * objRemittance.Sec_Educess_Per) / 100

            objRemittance.Calculated_Total_TDS = objRemittance.Calculated_TDS + objRemittance.Calculated_Surcharge + objRemittance.Calculated_Edu_Cess + objRemittance.Calculated_Sec_Educess
            objRemittance.Actual_Total_TDS = objRemittance.Actual_TDS + objRemittance.Actual_Surcharge + objRemittance.Actual_Edu_Cess + objRemittance.Actual_Sec_Educess
        End If
    End Sub

    Private Sub btnNewHistory_Click(sender As Object, e As EventArgs) Handles btnNewHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, "PurchaseOrder_No", "TSPL_PURCHASE_ORDER_HEAD", "TSPL_PURCHASE_ORDER_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtTenderNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTenderNo._MYValidating
        Try
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                Throw New Exception("Please select vendor")
            End If
            txtTenderNo.Value = clsTenderHead.getFinder("Tender_Type in (2,3) and Posted=1 and exists (select 1 from TSPL_TENDER_DETAIL where TSPL_TENDER_DETAIL.DocumentCode=TSPL_TENDER_HEADER.DocumentCode and TSPL_TENDER_DETAIL.Vendor_Code='" + txtVendorNo.Value + "') ", txtTenderNo.Value, isButtonClicked)
            txtTenderNo.Tag = clsTenderHead.GetTenderType(txtTenderNo.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        SetSchedule()
    End Sub

    Sub SetSchedule()
        Try
            isInsideLoadData = True
            LoadBlankGridSchedule()
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 AndAlso clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value) > 0 Then
                    Dim dtRunningDate As DateTime = txtScheduleStartDate.Value
                    Dim ArrSch As List(Of clsItemSchedule) = clsItemSchedule.GetData(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), Nothing)
                    If ArrSch IsNot Nothing AndAlso ArrSch.Count > 0 Then
                        For Each obj As clsItemSchedule In ArrSch
                            gvSchedule.Rows.AddNew()
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleSNo).Value = gvSchedule.Rows.Count
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleNo).Value = obj.SNo
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleFromDate).Value = dtRunningDate
                            If clsCommon.CompairString(clsCommon.myCstr(obj.Days), "30") = CompairStringResult.Equal Then
                                Dim daysInMonth As Integer = DateTime.DaysInMonth(dtRunningDate.Year, dtRunningDate.Month)
                                If daysInMonth = 31 Then
                                    dtRunningDate = New DateTime(dtRunningDate.Year, dtRunningDate.Month, daysInMonth)
                                    gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleToDate).Value = dtRunningDate
                                ElseIf daysInMonth = 28 OrElse daysInMonth = 29 Then
                                    dtRunningDate = New DateTime(dtRunningDate.Year, dtRunningDate.Month, daysInMonth)
                                    gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleToDate).Value = dtRunningDate
                                Else
                                    dtRunningDate = dtRunningDate.AddDays(obj.Days - 1)
                                    gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleToDate).Value = dtRunningDate
                                End If
                            Else
                                dtRunningDate = dtRunningDate.AddDays(obj.Days - 1)
                                gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleToDate).Value = dtRunningDate
                            End If
                            dtRunningDate = dtRunningDate.AddDays(1)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleParentSNo).Value = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colLineNo).Value)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleICode).Value = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleIName).Value = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleQtyPer).Value = obj.Qty_Per
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleQty).Value = ((clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value) * obj.Qty_Per) / 100)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleShortPer).Value = obj.Short_Per
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleShort).Value = ((clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value) * obj.Short_Per) / 100)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLateDays).Value = obj.Late_Days
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLateDays).Tag = SetSchedulePenalty(obj.Arr, dtRunningDate)
                        Next
                    End If
                End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Function SetSchedulePenalty(ByVal Arr As List(Of clsItemSchedulePenalty), ByVal dtRunningDate As DateTime) As List(Of clsTenderSchedulePeneltyPO)
        Dim ArrTemp As List(Of clsTenderSchedulePeneltyPO) = Nothing
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            ArrTemp = New List(Of clsTenderSchedulePeneltyPO)
            For Each objtr As clsItemSchedulePenalty In Arr
                Dim objTemp As New clsTenderSchedulePeneltyPO
                objTemp.Penalty_Date = dtRunningDate.AddDays(objtr.Penalty_Days - 1)
                objTemp.Penalty = objtr.Penalty
                ArrTemp.Add(objTemp)
            Next
        End If
        Return ArrTemp
    End Function
    Sub LoadBlankGridSchedule()
        gvSchedule.Rows.Clear()
        gvSchedule.Columns.Clear()


        Dim repoNum As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "SNo"
        repoNum.Name = colScheduleSNo
        repoNum.Width = 50
        repoNum.ReadOnly = True
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Schedule No"
        repoNum.Name = colScheduleNo
        repoNum.Width = 100
        repoNum.ReadOnly = True
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "From Date"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colScheduleFromDate
        repoDate.WrapText = True
        repoDate.ReadOnly = False
        repoDate.Width = 80
        gvSchedule.MasterTemplate.Columns.Add(repoDate)

        repoDate = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "To Date"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colScheduleToDate
        repoDate.WrapText = True
        repoDate.ReadOnly = False
        repoDate.Width = 80
        gvSchedule.MasterTemplate.Columns.Add(repoDate)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Parent SNo"
        repoNum.Name = colScheduleParentSNo
        repoNum.Width = 50
        repoNum.ReadOnly = True
        repoNum.IsVisible = False
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        Dim repotxt As GridViewTextBoxColumn = New GridViewTextBoxColumn()



        repotxt = New GridViewTextBoxColumn()
        repotxt.FormatString = ""
        repotxt.HeaderText = "Item Code"
        repotxt.Name = colScheduleICode
        repotxt.ReadOnly = True
        repotxt.Width = 100
        repotxt.IsVisible = True
        gvSchedule.MasterTemplate.Columns.Add(repotxt)

        repotxt = New GridViewTextBoxColumn()
        repotxt.FormatString = ""
        repotxt.HeaderText = "Item Description"
        repotxt.Name = colScheduleIName
        repotxt.Width = 150
        repotxt.ReadOnly = True
        repotxt.IsVisible = True
        gvSchedule.MasterTemplate.Columns.Add(repotxt)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Quantity %"
        repoNum.Name = colScheduleQtyPer
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Quantity"
        repoNum.Name = colScheduleQty
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Short %"
        repoNum.Name = colScheduleShortPer
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Short Quantity"
        repoNum.Name = colScheduleShort
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Late Days"
        repoNum.Name = colScheduleLateDays
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Extension Days"
        repoNum.Name = colScheduleExtensionDays
        repoNum.ReadOnly = False
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        gvSchedule.AllowDeleteRow = True
        gvSchedule.AllowAddNewRow = False
        gvSchedule.ShowGroupPanel = False
        gvSchedule.AllowColumnReorder = False
        gvSchedule.AllowRowReorder = False
        gvSchedule.EnableSorting = False
        gvSchedule.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvSchedule.MasterTemplate.ShowRowHeaderColumn = False
        gvSchedule.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub ShowPenalty()
        Try
            Dim dt As DataTable = New DataTable()
            dt.Columns.Add("Penalty Date", GetType(String))
            dt.Columns.Add("Penalty", GetType(Decimal))

            Dim arr As List(Of clsTenderSchedulePeneltyPO) = TryCast(gvSchedule.CurrentRow.Cells(colScheduleLateDays).Tag, List(Of clsTenderSchedulePeneltyPO))
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For ii As Integer = 0 To arr.Count - 1
                    Dim dr As DataRow = dt.NewRow
                    dr("Penalty Date") = clsCommon.GetPrintDate(arr(ii).Penalty_Date, "dd/MM/yyyy")
                    dr("Penalty") = arr(ii).Penalty
                    dt.Rows.Add(dr)
                Next
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frm As New FrmFreeGrid
                frm.dt = dt
                'frm.arrEditableColumn = New List(Of String)
                'frm.arrEditableColumn.Add("Penalty")
                frm.strFormName = "Show Penalty"
                frm.ReportID = "SchPenaltyD"
                frm.WindowState = FormWindowState.Maximized
                frm.ShowDialog()
                'If frm.dt IsNot Nothing AndAlso frm.dt.Rows.Count > 0 Then
                '    Dim ArrTemp As New List(Of clsItemSchedulePenalty)
                '    Dim obj As clsItemSchedulePenalty = Nothing
                '    For Each dr As DataRow In frm.dt.Rows
                '        obj = New clsItemSchedulePenalty()
                '        obj.Penalty_Days = clsCommon.myCDecimal(dr("Days"))
                '        obj.Penalty = clsCommon.myCDecimal(dr("Penalty"))
                '        ArrTemp.Add(obj)
                '    Next
                '    gvSchedule.CurrentRow.Cells(colScheduleLateDays).Tag = ArrTemp
                'Else
                '    gvSchedule.CurrentRow.Cells(colScheduleLateDays).Tag = Nothing
                'End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvSchedule_KeyDown(sender As Object, e As KeyEventArgs) Handles gvSchedule.KeyDown
        If e.KeyCode = Keys.F5 Then
            ShowPenalty()
        End If
    End Sub

    Dim isCellValueChangedOpenSchedule As Boolean = False
    Private Sub gvSchedule_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvSchedule.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpenSchedule Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvSchedule.Columns(colScheduleToDate) Then
                        Dim PKID As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PK_Id from (select ROW_NUMBER() over (Partition by Item_Code order by PK_Id) as SNO, * from TSPL_ITEM_SCHEDULE where Item_Code= '" + clsCommon.myCstr(gvSchedule.CurrentRow.Cells(colScheduleICode).Value) + "' )xx where SNO=" + clsCommon.myCstr(gvSchedule.CurrentRow.Cells(colScheduleSNo).Value) + ""))
                        If clsCommon.myLen(PKID) > 0 Then
                            Dim Arr As List(Of clsItemSchedulePenalty) = clsItemSchedulePenalty.GetData(PKID, Nothing)
                            gvSchedule.CurrentRow.Cells(colScheduleLateDays).Tag = SetSchedulePenalty(Arr, clsCommon.myCDate(gvSchedule.CurrentRow.Cells(colScheduleToDate).Value).AddDays(1))
                        End If
                    End If
                End If
                isCellValueChangedOpenSchedule = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpenSchedule = False
        End Try
    End Sub

    Private Sub ApplyBoldFormatting(textBox As RichTextBox, isBold As Boolean)
        If textBox.SelectionLength > 0 Then
            Dim start As Integer = textBox.SelectionStart
            Dim length As Integer = textBox.SelectionLength

            If isBold Then
                textBox.SelectionFont = New Font(textBox.Font, FontStyle.Bold)
                textBox.Font = New Font(txtCmt1.Font, FontStyle.Regular)

            Else
                textBox.SelectionFont = New Font(textBox.Font, FontStyle.Regular)
                textBox.Font = New Font(textBox.Font, FontStyle.Regular)

            End If
            textBox.Select(start, length)
        End If
    End Sub

    Private Sub ApplyUnderlineFormatting(textBox As RichTextBox, isUnderline As Boolean)
        If textBox.SelectionLength > 0 Then
            Dim start As Integer = textBox.SelectionStart
            Dim length As Integer = textBox.SelectionLength

            If isUnderline Then
                textBox.SelectionFont = New Font(textBox.Font, FontStyle.Underline)
            Else
                ' Remove underline style
                textBox.SelectionFont = New Font(textBox.Font, textBox.Font.Style And Not FontStyle.Underline)
            End If

            textBox.Select(start, length)
        End If
    End Sub

    Private Sub ApplyBoldUnderlineFormatting(textBox As RichTextBox, isBoldUnderline As Boolean)
        If textBox.SelectionLength > 0 Then
            Dim start As Integer = textBox.SelectionStart
            Dim length As Integer = textBox.SelectionLength

            If isBoldUnderline Then
                'textBox.SelectionFont = New Font(textBox.Font, FontStyle.Bold AndAlso FontStyle.Underline)
                textBox.SelectionFont = New Font(textBox.Font, FontStyle.Bold Or FontStyle.Underline)
                'textBox.SelectionFont = New Font(textBox.Font, FontStyle.Bold And FontStyle.Underline)
                textBox.Font = New Font(txtCmt1.Font, FontStyle.Regular)

            Else
                textBox.SelectionFont = New Font(textBox.Font, FontStyle.Regular)
                textBox.Font = New Font(textBox.Font, FontStyle.Regular)

            End If
            textBox.Select(start, length)
        End If
    End Sub

    Private Sub txtCmt1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtComment.KeyDown, txtCmt1.KeyDown, txtCmt2.KeyDown, txtCmt3.KeyDown, txtCmt4.KeyDown, txtCmt5.KeyDown, txtCmt6.KeyDown, txtCmt7.KeyDown, txtCmt8.KeyDown, txtCmt9.KeyDown, txtCmt10.KeyDown, txtCmt11.KeyDown, txtCmt12.KeyDown, txtCmt13.KeyDown
        Dim txtBox As RichTextBox = DirectCast(sender, System.Windows.Forms.Control)
        If txtBox.SelectionLength > 0 Then
            If e.Control AndAlso e.Alt AndAlso e.KeyCode = Keys.B Then
                'If e.KeyCode = Keys.B Then
                ApplyBoldFormatting(txtBox, True)
                e.SuppressKeyPress = True
            End If

            If e.Control AndAlso e.Alt AndAlso e.KeyCode = Keys.A Then
                'ElseIf e.KeyCode = Keys.F Then
                ApplyUnderlineFormatting(txtBox, True)
                e.SuppressKeyPress = True
            End If

            If e.Control AndAlso e.Alt AndAlso e.KeyCode = Keys.V Then
                'ElseIf e.KeyCode = Keys.F Then
                ApplyBoldUnderlineFormatting(txtBox, True)
                e.SuppressKeyPress = True
            End If
            'End If
            'If e.Control AndAlso e.Alt AndAlso e.KeyCode = Keys.B Then
            '    ApplyBoldFormatting(txtBox, True)

            '    e.SuppressKeyPress = True
            'End If
        Else
            txtBox.Font = New Font(txtBox.Font, FontStyle.Regular)
            txtBox.SelectionFont = New Font(txtBox.Font, FontStyle.Regular)
        End If
    End Sub


End Class

