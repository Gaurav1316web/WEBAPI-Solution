Imports System.Data.SqlClient
Imports System.IO
Imports common
Public Class frmPurchaseInvoice
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ItemCostTolerancePercentage As Decimal = 0
    Private PurchaseModulePickFixTaxRate As Boolean = False
    Dim ShowCapexCodeandSubCode As Boolean = False
    Dim SkipJobWorkSRN As Boolean = False
    Public AllowModifcationByApprovalUser As Boolean = False
    Dim ShowMessageTDS As Boolean = False
    Dim qry As String
    Dim atchqry As String = ""
    Public strPOInvoice As String = Nothing
    Private isCellValueChangedOpen As Boolean = False
    Private isCellValueChangedTaxOpen As Boolean = False
    Private objRemittance As clsRemittance
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim TDSAmt As Double = 0
    Const colLineNo As String = "COLLNO"
    Const colRowType As String = "COLTYPE"
    Const colComplete As String = "COMPLETE"
    Const colBalanceQty As String = "BALANCEQTY"
    Const colOrgSRNQty As String = "ORGINALSRNQTY"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colHSNNo As String = "COLHSNNo"
    Const colPendingQty As String = "COLPENDINGQTY"
    Const colQty As String = "COLQTY"
    Const colfreeQty As String = "COLFREEQTY"
    ''Const colRejectedQty As String = "COLREJECTEDQTY"
    Const colUnit As String = "COLUNIT"
    Const colRate As String = "COLRATE"

    Const colIsInsurance As String = "colIsInsurance"
    Const colInsuranceBaseAmt As String = "colInsuranceBaseAmt"
    Const colInsurancePer As String = "colInsurancePer"

    Const colAmt As String = "COLAMT"
    Const colDisType As String = "COLDISTYPE"
    Const colHeaderDiscountPer As String = "colHeaderDiscountPer"
    Const colHeaderDiscountAmt As String = "colHeaderDiscountAmt"
    Const colDisPer As String = "COLDISPER"
    Const colDetailDisAmt As String = "colDetailDisAmt"
    Const colDisPerUnit As String = "colDisPerUnit"
    Const colDisAmtPerUnit As String = "colDisAmtPerUnit"
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
    Const colTax2 As String = "COLTAX2"
    Const colTaxBaseAmt2 As String = "COLTAXBASEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colIsTaxable2 As String = "ISTAXABLE2"
    Const colIsSurTax2 As String = "ISSURTAX2"
    Const colSurTaxCode2 As String = "SURTAXCODE2"
    Const colTax3 As String = "COLTAX3"
    Const colTaxBaseAmt3 As String = "COLTAXBASEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colIsTaxable3 As String = "ISTAXABLE3"
    Const colIsSurTax3 As String = "ISSURTAX3"
    Const colSurTaxCode3 As String = "SURTAXCODE3"
    Const colTax4 As String = "COLTAX4"
    Const colTaxBaseAmt4 As String = "COLTAXBASEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colIsTaxable4 As String = "ISTAXABLE4"
    Const colIsSurTax4 As String = "ISSURTAX4"
    Const colSurTaxCode4 As String = "SURTAXCODE4"
    Const colTax5 As String = "COLTAX5"
    Const colTaxBaseAmt5 As String = "COLTAXBASEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colIsTaxable5 As String = "ISTAXABLE5"
    Const colIsSurTax5 As String = "ISSURTAX5"
    Const colSurTaxCode5 As String = "SURTAXCODE5"
    Const colTax6 As String = "COLTAX6"
    Const colTaxBaseAmt6 As String = "COLTAXBASEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colIsTaxable6 As String = "ISTAXABLE6"
    Const colIsSurTax6 As String = "ISSURTAX6"
    Const colSurTaxCode6 As String = "SURTAXCODE6"
    Const colTax7 As String = "COLTAX7"
    Const colTaxBaseAmt7 As String = "COLTAXBASEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colIsTaxable7 As String = "ISTAXABLE7"
    Const colIsSurTax7 As String = "ISSURTAX7"
    Const colSurTaxCode7 As String = "SURTAXCODE7"
    Const colTax8 As String = "COLTAX8"
    Const colTaxBaseAmt8 As String = "COLTAXBASEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colIsTaxable8 As String = "ISTAXABLE8"
    Const colIsSurTax8 As String = "ISSURTAX8"
    Const colSurTaxCode8 As String = "SURTAXCODE8"
    Const colTax9 As String = "COLTAX9"
    Const colTaxBaseAmt9 As String = "COLTAXBASEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colIsTaxable9 As String = "ISTAXABLE9"
    Const colIsSurTax9 As String = "ISSURTAX9"
    Const colSurTaxCode9 As String = "SURTAXCODE9"
    Const colTax10 As String = "COLTAX10"
    Const colTaxBaseAmt10 As String = "COLTAXBASEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colIsTaxable10 As String = "ISTAXABLE10"
    Const colIsSurTax10 As String = "ISSURTAX10"
    Const colSurTaxCode10 As String = "SURTAXCODE10"
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

    Const colTaxRecoverable1 As String = "RECOVERTABLETAX1"
    Const colTaxRecoverable2 As String = "RECOVERTABLETAX2"
    Const colTaxRecoverable3 As String = "RECOVERTABLETAX3"
    Const colTaxRecoverable4 As String = "RECOVERTABLETAX4"
    Const colTaxRecoverable5 As String = "RECOVERTABLETAX5"
    Const colTaxRecoverable6 As String = "RECOVERTABLETAX6"
    Const colTaxRecoverable7 As String = "RECOVERTABLETAX7"
    Const colTaxRecoverable8 As String = "RECOVERTABLETAX8"
    Const colTaxRecoverable9 As String = "RECOVERTABLETAX9"
    Const colTaxRecoverable10 As String = "RECOVERTABLETAX10"
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
    Dim arrLoc As String = ""

    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"
    Const colSRNNo As String = "SRNNO"
    Const colPOID As String = "colPOID"
    Const colGRNID As String = "colGRNID"

    Const colGRNDate As String = "colGRNDate"
    Const colWeighmentNo As String = "colWeighmentNo"
    Const colWeighmentDate As String = "colWeighmentDate"
    Const colQCNo As String = "colQCNo"
    Const colQCDate As String = "colQCDate"
    Const colVehicleNo As String = "colVehicleNo"
    Const colBillNo As String = "colBillNo"

    Const colMRNID As String = "colMRNID"
    Const colLeakQty As String = "COLEAKQTY"
    Const colBurstQty As String = "COLBURSTQTY"
    Const colShortQty As String = "COLSHORTQTY"
    Const colRejectQty As String = "COLREJECTQTY"

    Const colMRP As String = "MRP"
    ''Const colAssessableRate As String = "ASSESSABLERATE"
    Const colAssessableAmount As String = "ASSESSABLEAMT"
    Const colBatchNo As String = "BATCHNO"
    Const colBinNo As String = "colBinNo"
    Const colExpiry As String = "EXPIRYDATE"
    Const colManufactureDate As String = "MANUFACTUREDATE"
    Const colSpecification As String = "SPECIFICATION"
    Const colRemarks As String = "REMARKS"
    Const colIsMannualAmt As String = "ISMANNUALAMT"
    Const colAcceptedAmount As String = "colAcceptedAmount"
    Const colRejectedAmount As String = "colRejectedAmount"
    Const colShortageAmount As String = "colShortageAmount"
    Const colLeakAmount As String = "colLeakAmount"
    Const colBurstAmount As String = "colBurstAmount"
    Const colAmtLessDiscountWithoutShortage As String = "colAmtLessDiscountWithoutShortage"
    Const colItemTaxable As String = "colItemTaxable"
    Const colAgainstItemWiseTaxCode As String = "colAgainstItemWiseTaxCode"

    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    'Const colTIsTaxable As String = "ISTAXABLE"
    Const colTTaxRate As String = "TAXRATE"

    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"
    Const colTTaxAssessableAmt As String = "COLTTAXASSESSABLEAMT"


    Const colACCode As String = "COLACCODE"
    Const colACName As String = "COLACNAME"
    Const colACAmount As String = "COLACAMOUNT"

    Const colLandedRate As String = "LANDEDRATE"
    Const colLandedAmt As String = "LANDEDAMT"

    Const colUnitTotRecTax As String = "colUnitTotRecTax"
    Const colUnitTotNonRecTax As String = "colUnitTotNonRecTax"
    Const colUnitTotAddCost As String = "colUnitTotAddCost"
    '' for abatement PI
    Const colAbatementRate As String = "colAbatementRate"
    Const colAssesableMRP As String = "colAssesableMRP"
    Const colTotalAssesableMRP As String = "colTotalAssesableMRP"
    Const colSRNUnitCost As String = "colSRNUnitCost"
    Dim IsAbatementPO As Boolean

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim repoBalQty As GridViewDecimalColumn
    Dim repoComplete As GridViewTextBoxColumn
    Dim IsNotIncludeWasteQtyInCal As Boolean = False
    Dim iscalculationonrejqty As Boolean = False
    Dim Is_SRN_Rej_Store_true As Boolean = False
    Dim is_Load_MRN As Boolean = False

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
    ''==================================================================

    Const colItemInsuranceBaseAmt As String = "colItemInsuranceBaseAmt"
    Const colItemInsuranceApplyOn As String = "colItemInsuranceApplyOn"
    Const colItemInsurancePer As String = "colItemInsurancePer"
    Const colItemInsuranceAmt As String = "colItemInsuranceAmt"
    Const colItemAmtAfterInsurance As String = "colItemAmtAfterInsurance"

    Const colACInsuranceCode As String = "colACInsuranceCode"
    Const colACInsuranceName As String = "colACInsuranceName"
    Const colACInsuranceAmount As String = "colACInsuranceAmount"


    Dim dblPreviousTDSAmt As Double = 0
    Dim ChkAutoDepOnPurchaseCycle As Boolean = False
    Dim UnitInceaseValue As Boolean = False
    Private SettingAutoRoundOffSeprateAccountOnVendorTransaction As Boolean = False

    Public ShowItemAllStructureWise As Boolean = False
    Dim ConsiderPreviousandCurrentFYForTCSTaxCustOutstanding As Boolean = False
    Dim AmountToCheckVendorOutstandingForTCSTax As Decimal = 0
    Public AllowtoChangeTCSBaseAmountPurchase As Boolean = False
    Dim isAgainstTender As Boolean = False

#End Region
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnPurchaseInvoice)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnprintjvl.Visible = MyBase.isPrintFlag
        btnPrintInv.Visible = MyBase.isPrintFlag
        btnReverse.Visible = False

        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        btncancel.Visible = MyBase.isCancel_Flag
    End Sub
    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LOCATIONRIGTHS()
        SetUserMgmtNew()
        SetMailRight()
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

        btnprintjvl.Enabled = False
        ConsiderPreviousandCurrentFYForTCSTaxCustOutstanding = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsiderPreviousCurrentFYForTCSTaxVendOutstanding, clsFixedParameterCode.ConsiderPreviousCurrentFYForTCSTaxVendOutstanding, Nothing)) > 0)
        AmountToCheckVendorOutstandingForTCSTax = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckVendorOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckVendorOutstandingForTCSTax, Nothing))
        AllowtoChangeTCSBaseAmountPurchase = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoChangeTCSBaseAmountPurchase, clsFixedParameterCode.AllowtoChangeTCSBaseAmountPurchase, Nothing)) > 0)
        ItemCostTolerancePercentage = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ItemCostTolerancePercentage, clsFixedParameterCode.ItemCostTolerancePercentage, Nothing))
        ShowItemAllStructureWise = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowItemAllStructureWise, clsFixedParameterCode.ShowItemAllStructureWise, Nothing)) = 1, True, False)


        SettingAutoRoundOffSeprateAccountOnVendorTransaction = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoRoundOffSeprateAccountOnVendorTransaction, clsFixedParameterCode.AutoRoundOffSeprateAccountOnVendorTransaction, Nothing)) = 1)
        ShowCapexCodeandSubCode = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapex, clsFixedParameterCode.ShowOptionforSelectingCapex, Nothing)) = "1", True, False))
        ShowMessageTDS = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowMessgForTDS, clsFixedParameterCode.ShowMessgForTDS, Nothing)) = "1", True, False))
        PurchaseModulePickFixTaxRate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchaseModulePickFixTaxRate, clsFixedParameterCode.PurchaseModulePickFixTaxRate, Nothing)) = 1, True, False)
        txtVendorNo.MendatroryField = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btncancel, "Press Alt+L Cancel Trasnaction")

        'ButtonToolTip.SetToolTip(btnRequistionItems, "Press Ctrl+F7 for Select Purchase Requistion Items")
        is_Load_MRN = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowGRN, clsFixedParameterCode.ShowGRN, Nothing)) = 1 And clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMRN, clsFixedParameterCode.ShowMRN, Nothing)) = 1, True, False)
        IsAbatementPO = clsPurchaseOrderHead.GetPurchaseSetting().Rows(0).Item("IsAbatementPO")
        Is_SRN_Rej_Store_true = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SRN_Rejected_Store from TSPL_PURCHASE_SETTINGS")) = 0, False, True)
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadAgainstForm()
        LoadBlankGrid()
        LoadBlankGridTax()
        AddNew()
        SetLength()
        LoadItemType()
        LoadBlankGridAC()
        LoadBlankGridACInsurance()
        CboxITCType.SelectedIndex = 0

        IsNotIncludeWasteQtyInCal = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsNotIncludeWasteQtyInCal, clsFixedParameterCode.IsNotIncludeWasteQtyInCal, Nothing)) = 1, True, False)

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields
        RadPageView1.Pages("TabDedDetail").Item.Visibility = IIf(objCommonVar.RCDFCFP, ElementVisibility.Visible, ElementVisibility.Collapsed)

        '' MultiCurrency
        SetMultiCurrencyVisibility()
        '' End of MultiCurrency
        '===Commented by Rohit on 13 Aug 2014,Because C Form will be Taken According to Tax Rate and will be Define in tax rate Screen. 
        'If objCommonVar.IsDemoERP Then
        '    chkAgainstCForm.Visible = True
        'Else
        '    chkAgainstCForm.Visible = False
        'End If
        '==========================================================
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If
        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment
        If Not objCommonVar.IsDemoERP Then
            pnlPCJ.Visible = False
        Else
            fndProject.Enabled = False
            lblProject.Enabled = False
        End If
        '' make editable/non editable Term Code
        txtTermCode.Enabled = clsPurchaseOrderHead.GetInventorySetting().Rows(0).Item("IsTermsEditableOnPurchase")

        If clsCommon.myLen(strPOInvoice) > 0 Then
            LoadData(strPOInvoice, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        LoadDocumentType()
        Dim strIndustryType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description from TSPL_FIXED_PARAMETER where Type ='" & clsFixedParameterType.INDUSTRYTYPE & "' and Code='" & clsFixedParameterCode.INDUSTRYTYPE & "' "))
        If clsCommon.CompairString(strIndustryType, "D") = CompairStringResult.Equal Then
            cmbDocType.Enabled = True
        Else
            cmbDocType.SelectedValue = "PI"
            cmbDocType.Enabled = False
        End If
        txtDept.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select segment_code from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtDept.Value) > 0 Then
            lblDept.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_GL_SEGMENT_CODE where Seg_No=3 and segment_code='" + txtDept.Value + "' "))
        End If
        UnitInceaseValue = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UnitCostIncreasePurchaseInvoice, clsFixedParameterCode.UnitCostIncreasePurchaseInvoice, Nothing)) = 1, True, False)
        SkipJobWorkSRN = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SkipJobWorkSRNInPI, clsFixedParameterCode.SkipJobWorkSRNInPI, Nothing)) = 1, True, False))
        RadMenuItem1.Visibility = ElementVisibility.Collapsed
        btncancel.Enabled = False
        If objCommonVar.RCDFCFP Then
            MyLabel24.Visible = True
            MyLabel25.Visible = True
            MyLabel27.Visible = True
            MyLabel26.Visible = True
            MyLabel28.Visible = True
            MyLabel29.Visible = True
            lblSecurityDeduction.Visible = True
            lblQualityDeduction.Visible = True
            lblRMLate.Visible = True
            lblTdsAmt.Visible = True
            lblPIAmt.Visible = True
            dbltotamtafterded.Visible = True
        End If
    End Sub
    Sub AllowDepartmentMandatoryOnPurchaseCycle()
        ChkAutoDepOnPurchaseCycle = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoDepartmentMendatroryFieldOnPurcahseCycle, clsFixedParameterCode.AutoDepartmentMendatroryFieldOnPurcahseCycle, Nothing)) = "1", True, False)
        If ChkAutoDepOnPurchaseCycle Then
            txtDept.Enabled = False
            txtDept.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Segment_code from TSPL_USER_MASTER where User_Code ='" + objCommonVar.CurrentUserCode + "'"))
            lblDept.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Segment_name from TSPL_GL_SEGMENT_CODE where Seg_No=3 and Segment_code='" + txtDept.Value + "'"))
        Else
            txtDept.Enabled = True
        End If
        LoadPIType()

    End Sub
    Public Sub SetMailRight()
        'Dim obj As clsCheckMailSetting = clsCheckMailSetting.CheckMailRight()
        If objCommonVar.IsMailSend Then
            btnsend.Enabled = True
        Else
            btnsend.Enabled = False
        End If

    End Sub
    Private Sub LoadDocumentType()
        cmbDocType.DataSource = Nothing
        Dim dt As DataTable = Nothing
        Dim qry As String = "select 'PI' as Code,'Purchase Invoice' as Name union all select 'MT' as Code,'Merchant Trade ' as Name"
        dt = clsDBFuncationality.GetDataTable(qry)
        cmbDocType.DataSource = dt
        cmbDocType.ValueMember = "Code"
        cmbDocType.DisplayMember = "Name"
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
        'Dim strq As String
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
        txtRemarks.MaxLength = 200
        txtComment.MaxLength = 200
        txtRefNo.MaxLength = 50
        txtCarrier.MaxLength = 50
        txtGRNo.MaxLength = 50
        txtGENo.MaxLength = 50
        txtVendorInvoiceNo.MaxLength = 30
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
            cboItemType.Enabled = False
        Else
            'cboItemType.DataSource = clsItemMaster.GetItemType()
            Dim Whr = " AND IS_NON_INVENTORY=0   AND ITEM_TYPE_CODE NOT IN('J') "
            cboItemType.DataSource = clsItemMaster.getItemTypeQuery(Whr)
            cboItemType.ValueMember = "Code"
            cboItemType.DisplayMember = "Name"
            cboItemType.Enabled = True
        End If
    End Sub
    Sub LoadPIType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "L"
        dr("Name") = "Domestic"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "I"
        dr("Name") = "Import"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "J"
        dr("Name") = "Job Work"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "O"
        dr("Name") = "Job Work Outward"
        dt.Rows.Add(dr)


        cboPOType.DataSource = dt
        cboPOType.ValueMember = "Code"
        cboPOType.DisplayMember = "Name"
    End Sub
    Sub LoadITC_Elibible()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()

        If CboxITCType.SelectedIndex = 0 Then

            dr = dt.NewRow()
            dr("Code") = "ITC for Both Taxable or Non-Taxable"
            dr("Name") = "ITC for Both Taxable or Non-Taxable"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "ITC for 100% Taxable"
            dr("Name") = "ITC for 100% Taxable"
            dt.Rows.Add(dr)
        Else
            dr = dt.NewRow()
            dr("Code") = "ITC for 100% Non-Taxable"
            dr("Name") = "ITC for 100% Non-Taxable"
            dt.Rows.Add(dr)
        End If


        CboxITCCateogory.DataSource = dt
        CboxITCCateogory.ValueMember = "Code"
        CboxITCCateogory.DisplayMember = "Name"
    End Sub
    Sub BlankAllControls()
        chkJobWorkOutward.Checked = False
        txtDocNo.Value = ""
        txtDesc.Text = ""
        chkOnHold.Checked = False
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        InvDate1.Value = clsCommon.GETSERVERDATE()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtBillToLocation.Value = ""
        lblBillToLocation.Text = ""
        txtShipToLocation.Value = ""
        lblShipToLocation.Text = ""
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        TxtRetention.Text = ""
        txtDesc.Text = ""
        txtRemarks.Text = ""
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
        lblRoundOff.Text = ""
        lblTotRAmt.Text = ""
        lblDocAmount.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtVendorInvoiceNo.Text = ""
        txtCarrier.Text = ""
        txtGENo.Text = ""
        txtGEDate.Checked = False
        txtGEDate.Value = txtDate.Value
        lblAddCharges.Text = ""
        lblAddCharges1.Text = ""
        cboItemType.SelectedIndex = 0
        cboItemType.Enabled = True
        txtReqNo.Value = ""
        cboItemType.Enabled = True
        txtBillToLocation.Enabled = True
        txtSubLocation.Enabled = True
        rbtnTaxCalAutomatic.IsChecked = True
        lblPJVNo.Text = ""
        txtDept.Value = ""
        lblDept.Text = ""
        chkExciseOnQty.Checked = False
        chkExciseOnQty.Enabled = True
        ''RICHA AGARWAL AGAINST TICKET NO. BM00000006091 ON 04/05/2015
        txtCurrencyCode.Enabled = True
        txtCurrencyCode.Value = ""
        txtConversionRate.Value = 1
        txtApplicableFrom.Text = ""
        ''--------------------------
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        fndProject.Text = ""
        lblProject.Text = ""
        chkAgainstForm.Checked = False
        ddlForm.Enabled = False
        txtGRNo.Text = ""
        dtpGRDate.Value = clsCommon.GETSERVERDATE()
        txtVehicleNo.Value = ""
        lblVehicle.Text = ""
        txtTransporter.Value = ""
        lblTransporterName.Text = ""
        lblTransporterAddress.Text = ""
        dtpTransportDate.Value = clsCommon.GETSERVERDATE
        lblTaxableAmount.Text = ""
        lblAcceptedAmt.Text = clsCommon.myFormat(0)
        lblRejectedAmt.Text = clsCommon.myFormat(0)
        lblShortageAmt.Text = clsCommon.myFormat(0)
        lblLeakAmt.Text = clsCommon.myFormat(0)
        lblBurstAmt.Text = clsCommon.myFormat(0)
        dblPreviousTDSAmt = 0
        lblSecurityDeduction.Text = clsCommon.myFormat(0)
        lblQualityDeduction.Text = clsCommon.myFormat(0)
        lblRMLate.Text = clsCommon.myFormat(0)
        lblPIAmt.Text = clsCommon.myFormat(0)
        dbltotamtafterded.Text = clsCommon.myFormat(0)
        lblTdsAmt.Text = clsCommon.myFormat(0)
        chkITCEligible.Checked = False
        CboxITCType.SelectedIndex = 0
        dtImportEntryDate.Value = clsCommon.GETSERVERDATE()
        txtport.Text = ""
        txtImportEntryNo.Text = ""
        txtDataAndTimeSelection.Value = clsCommon.GETSERVERDATE()
        txtTapalNo.Text = ""
        txtDataAndTimeSelection.Checked = False

        If AllowtoChangeTCSBaseAmountPurchase = True Then
            txttcstaxbaseamount.Enabled = True
        Else
            txttcstaxbaseamount.Enabled = False
        End If
        txttcstaxbaseamount.Value = 0
        lblActualTCSTaxBaseAmt.Text = "0"
        txtTCSTaxRate.Value = 0

        lblAddChargesForInsurance.Text = ""
        lblAddChargesForInsurance1.Text = ""
        lblTotalInsuranceAmt.Text = ""
        'btncancel.Enabled = False

        gvDeduction.DataSource = Nothing
        gvAPInvoice.DataSource = Nothing
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
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        repoComplete = New GridViewTextBoxColumn()
        repoComplete.FormatString = ""
        repoComplete.HeaderText = "Complete"
        repoComplete.Width = 70
        repoComplete.Name = colComplete
        repoComplete.ReadOnly = True
        repoComplete.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoComplete)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Row Type"
        repoRowType.Name = colRowType
        repoRowType.Width = 50
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = frmSRN.GetItemType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoRowType)


        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        repoIName = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "HSN No/SAC Code"
        repoIName.Name = colHSNNo
        repoIName.Width = 150
        repoIName.ReadOnly = False
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
        gv1.MasterTemplate.Columns.Add(repoPendingQty)

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

        Dim repoOrgSRNQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrgSRNQty.FormatString = "{0:N3}"
        repoOrgSRNQty.WrapText = True
        'repoOrgSRNQty.HeaderText = "Original SRN Quantity"
        repoOrgSRNQty.HeaderText = "Accepted Quantity"
        repoOrgSRNQty.Name = colOrgSRNQty
        repoOrgSRNQty.Width = 80
        repoOrgSRNQty.Minimum = 0
        repoOrgSRNQty.ReadOnly = True
        repoOrgSRNQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgSRNQty)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = "{0:N3}"
        repoQty.HeaderText = "PI Quantity"
        repoQty.Name = colQty
        repoQty.DecimalPlaces = 10
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ReadOnly = True
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoFreeQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFreeQty = New GridViewDecimalColumn()
        repoFreeQty.FormatString = "{0:N3}"
        repoFreeQty.HeaderText = "Free Quantity"
        repoFreeQty.Name = colfreeQty
        repoFreeQty.Width = 80
        repoFreeQty.Minimum = 0
        repoFreeQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFreeQty)

        ''Dim repoLocationCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ''repoLocationCode.FormatString = ""
        ''repoLocationCode.HeaderText = "Location Code"
        ''repoLocationCode.Name = colLocationCode
        ''repoLocationCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        ''repoLocationCode.TextImageRelation = TextImageRelation.TextBeforeImage
        ''repoLocationCode.Width = 100
        ''gv1.MasterTemplate.Columns.Add(repoLocationCode)

        ''Dim repoLocationName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ''repoLocationName.FormatString = ""
        ''repoLocationName.HeaderText = "Location"
        ''repoLocationName.Name = colLocationName
        ''repoLocationName.ReadOnly = True
        ''repoLocationName.Width = 150
        ''gv1.MasterTemplate.Columns.Add(repoLocationName)

        Dim repoLeadQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLeadQty.FormatString = "{0:N3}"
        repoLeadQty.HeaderText = "Leakage"
        repoLeadQty.Name = colLeakQty
        repoLeadQty.Width = 80
        repoLeadQty.Minimum = 0
        repoLeadQty.ReadOnly = True
        repoLeadQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLeadQty)

        Dim repoBurstQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBurstQty.FormatString = "{0:N3}"
        repoBurstQty.HeaderText = "Burst"
        repoBurstQty.Name = colBurstQty
        repoBurstQty.Width = 80
        repoBurstQty.Minimum = 0
        repoBurstQty.ReadOnly = True
        repoBurstQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBurstQty)

        Dim repoShortQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShortQty.FormatString = "{0:N3}"
        repoShortQty.HeaderText = "Shortage"
        repoShortQty.Name = colShortQty
        repoShortQty.Width = 80
        repoShortQty.Minimum = 0
        repoShortQty.ReadOnly = True
        repoShortQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoShortQty)

        Dim repoRejectQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRejectQty.FormatString = "{0:N3}"
        repoRejectQty.HeaderText = "Reject"
        repoRejectQty.Name = colRejectQty
        repoRejectQty.Width = 80
        repoRejectQty.Minimum = 0
        repoRejectQty.ReadOnly = True
        repoShortQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRejectQty)


        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 80
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

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
        gv1.MasterTemplate.Columns.Add(repoRate)
        'gv1.Columns(colRate).ReadOnly = False


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


        Dim repoDiscountType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoDiscountType.FormatString = ""
        repoDiscountType.HeaderText = "Discount Type"
        repoDiscountType.Name = colDisType
        repoDiscountType.Width = 50
        repoDiscountType.ReadOnly = False
        repoDiscountType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoDiscountType.DataSource = frmSRN.GetDiscountType()
        repoDiscountType.ValueMember = "Code"
        repoDiscountType.DisplayMember = "Name"
        gv1.MasterTemplate.Columns.Add(repoDiscountType)

        repoDisPer = New GridViewDecimalColumn()
        repoDisPer.FormatString = "{0:N2}"
        repoDisPer.HeaderText = "Discount %"
        repoDisPer.Minimum = 0
        repoDisPer.Maximum = 100
        repoDisPer.Name = colDisPer
        repoDisPer.Width = 80
        repoDisPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDisPer)

        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDetailDisAmt
        repoDisAmt.Width = 80
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = False
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
        gv1.MasterTemplate.Columns.Add(repoAssessableAmt)


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

        Dim repoTaxRecoverable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable1.HeaderText = "Recoverable Tax 1"
        repoTaxRecoverable1.Name = colTaxRecoverable1
        repoTaxRecoverable1.ReadOnly = True
        repoTaxRecoverable1.IsVisible = False
        repoTaxRecoverable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable1)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

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

        Dim repoTaxRecoverable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable2.HeaderText = "Recoverable Tax 2"
        repoTaxRecoverable2.Name = colTaxRecoverable2
        repoTaxRecoverable2.ReadOnly = True
        repoTaxRecoverable2.IsVisible = False
        repoTaxRecoverable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable2)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable2)

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

        Dim repoTaxRecoverable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable3.HeaderText = "Recoverable Tax 3"
        repoTaxRecoverable3.Name = colTaxRecoverable3
        repoTaxRecoverable3.ReadOnly = True
        repoTaxRecoverable3.IsVisible = False
        repoTaxRecoverable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable3)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable3)

        Dim repoTax4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4.FormatString = ""
        repoTax4.HeaderText = "Tax 4"
        repoTax4.Name = colTax4
        repoTax4.ReadOnly = True
        repoTax4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax4)

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

        Dim repoTaxRecoverable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable4.HeaderText = "Recoverable Tax 4"
        repoTaxRecoverable4.Name = colTaxRecoverable4
        repoTaxRecoverable4.ReadOnly = True
        repoTaxRecoverable4.IsVisible = False
        repoTaxRecoverable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable4)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable4)

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

        Dim repoTaxRecoverable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable5.HeaderText = "Recoverable Tax 5"
        repoTaxRecoverable5.Name = colTaxRecoverable5
        repoTaxRecoverable5.ReadOnly = True
        repoTaxRecoverable5.IsVisible = False
        repoTaxRecoverable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable5)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable5)

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


        Dim repoTaxRecoverable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable6.HeaderText = "Recoverable Tax 6"
        repoTaxRecoverable6.Name = colTaxRecoverable6
        repoTaxRecoverable6.ReadOnly = True
        repoTaxRecoverable6.IsVisible = False
        repoTaxRecoverable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable6)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable6)

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

        Dim repoTaxRecoverable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable7.HeaderText = "Recoverable Tax 7"
        repoTaxRecoverable7.Name = colTaxRecoverable7
        repoTaxRecoverable7.ReadOnly = True
        repoTaxRecoverable7.IsVisible = False
        repoTaxRecoverable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable7)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable7)

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

        Dim repoTaxRecoverable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable8.HeaderText = "Recoverable Tax 8"
        repoTaxRecoverable8.Name = colTaxRecoverable8
        repoTaxRecoverable8.ReadOnly = True
        repoTaxRecoverable8.IsVisible = False
        repoTaxRecoverable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable8)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable8)

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


        Dim repoTaxRecoverable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable9.HeaderText = "Recoverable Tax 9"
        repoTaxRecoverable9.Name = colTaxRecoverable9
        repoTaxRecoverable9.ReadOnly = True
        repoTaxRecoverable9.IsVisible = False
        repoTaxRecoverable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable9)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable9)

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

        Dim repoTaxRecoverable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable10.HeaderText = "Recoverable Tax 10"
        repoTaxRecoverable10.Name = colTaxRecoverable10
        repoTaxRecoverable10.ReadOnly = True
        repoTaxRecoverable10.IsVisible = False
        repoTaxRecoverable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable10)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable10)

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
        gv1.MasterTemplate.Columns.Add(repoIsExcisable1)

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
        gv1.MasterTemplate.Columns.Add(repoIsExcisable10)


        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)

        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Included Tax Amount"
        repoAmtAfterTax.Name = colAmtAfterTax
        repoAmtAfterTax.WrapText = True
        repoAmtAfterTax.Width = 80
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterTax)

        Dim repoRequition As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRequition.FormatString = ""
        repoRequition.HeaderText = "SRN No"
        repoRequition.Name = colSRNNo
        repoRequition.ReadOnly = True
        repoRequition.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition)

        Dim repoPOID As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPOID.FormatString = ""
        repoPOID.HeaderText = "PO No"
        repoPOID.Name = colPOID
        repoPOID.ReadOnly = True
        repoPOID.Width = 100
        gv1.MasterTemplate.Columns.Add(repoPOID)
        Dim repoGRNNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGRNNo.FormatString = ""
        repoGRNNo.HeaderText = "GRN No"
        repoGRNNo.Name = colGRNID
        repoGRNNo.ReadOnly = True
        repoGRNNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoGRNNo)

        If objCommonVar.RCDFCFP = True Then
            Dim repoGRNDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            repoGRNDate.Format = DateTimePickerFormat.Custom
            repoGRNDate.CustomFormat = "dd-MM-yyyy"
            repoGRNDate.HeaderText = "GRN Date"
            repoGRNDate.FormatString = "{0:d}"
            repoGRNDate.Name = colGRNDate
            repoGRNDate.WrapText = True
            repoGRNDate.ReadOnly = True
            repoGRNDate.Width = 80
            repoGRNDate.IsVisible = objCommonVar.RCDFCFP
            gv1.MasterTemplate.Columns.Add(repoGRNDate)

            Dim repoWeighmentNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoWeighmentNo.FormatString = ""
            repoWeighmentNo.HeaderText = "Weighment No"
            repoWeighmentNo.Name = colWeighmentNo
            repoWeighmentNo.ReadOnly = True
            repoWeighmentNo.Width = 100
            repoWeighmentNo.IsVisible = objCommonVar.RCDFCFP
            gv1.MasterTemplate.Columns.Add(repoWeighmentNo)

            Dim repoWeighmentDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            repoWeighmentDate.Format = DateTimePickerFormat.Custom
            repoWeighmentDate.CustomFormat = "dd-MM-yyyy"
            repoWeighmentDate.HeaderText = "Weighment Date"
            repoWeighmentDate.FormatString = "{0:d}"
            repoWeighmentDate.Name = colWeighmentDate
            repoWeighmentDate.WrapText = True
            repoWeighmentDate.ReadOnly = True
            repoWeighmentDate.Width = 80
            repoWeighmentDate.IsVisible = objCommonVar.RCDFCFP
            gv1.MasterTemplate.Columns.Add(repoWeighmentDate)


            Dim repoQCNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoQCNo.FormatString = ""
            repoQCNo.HeaderText = "QC No"
            repoQCNo.Name = colQCNo
            repoQCNo.ReadOnly = True
            repoQCNo.Width = 100
            repoQCNo.IsVisible = objCommonVar.RCDFCFP
            gv1.MasterTemplate.Columns.Add(repoQCNo)

            Dim repoQCDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            repoQCDate.Format = DateTimePickerFormat.Custom
            repoQCDate.CustomFormat = "dd-MM-yyyy"
            repoQCDate.HeaderText = "QC Date"
            repoQCDate.FormatString = "{0:d}"
            repoQCDate.Name = colQCDate
            repoQCDate.WrapText = True
            repoQCDate.ReadOnly = True
            repoQCDate.Width = 80
            repoQCDate.IsVisible = objCommonVar.RCDFCFP
            gv1.MasterTemplate.Columns.Add(repoQCDate)

            Dim repoVehicleNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoVehicleNo.FormatString = ""
            repoVehicleNo.HeaderText = "Vehicle No"
            repoVehicleNo.Name = colVehicleNo
            repoVehicleNo.ReadOnly = True
            repoVehicleNo.Width = 100
            repoVehicleNo.IsVisible = objCommonVar.RCDFCFP
            gv1.MasterTemplate.Columns.Add(repoVehicleNo)

            Dim repoBillNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoBillNo.FormatString = ""
            repoBillNo.HeaderText = "Bill No"
            repoBillNo.Name = colBillNo
            repoBillNo.ReadOnly = True
            repoBillNo.Width = 100
            repoBillNo.IsVisible = objCommonVar.RCDFCFP
            gv1.MasterTemplate.Columns.Add(repoBillNo)

        End If

        Dim repoMRNNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMRNNo.FormatString = ""
        repoMRNNo.HeaderText = "MRN No"
        repoMRNNo.Name = colMRNID
        repoMRNNo.ReadOnly = True
        repoMRNNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoMRNNo)

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMRP)


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

        ''Dim repoAssessable As GridViewDecimalColumn = New GridViewDecimalColumn()
        ''repoAssessable = New GridViewDecimalColumn()
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
        gv1.MasterTemplate.Columns.Add(repoBinNo)

        Dim repoBatchNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBatchNo.FormatString = ""
        repoBatchNo.HeaderText = "Batch No"
        repoBatchNo.Name = colBatchNo
        repoBatchNo.ReadOnly = True
        repoBatchNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoBatchNo)

        Dim repoExpiry As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoExpiry.Format = DateTimePickerFormat.Custom
        repoExpiry.CustomFormat = "dd-MM-yyyy"
        repoExpiry.HeaderText = "Expiry Date"
        repoExpiry.FormatString = "{0:d}"
        repoExpiry.Name = colExpiry
        repoExpiry.WrapText = True
        repoExpiry.ReadOnly = True
        repoExpiry.Width = 80
        gv1.MasterTemplate.Columns.Add(repoExpiry)

        Dim repoManDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoManDate.Format = DateTimePickerFormat.Custom
        repoManDate.CustomFormat = "dd-MM-yyyy"
        repoManDate.HeaderText = "Manufacturer Date"
        repoManDate.WrapText = True
        repoManDate.FormatString = "{0:d}"
        repoManDate.Name = colManufactureDate
        repoManDate.ReadOnly = True
        repoManDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoManDate)

        Dim repoSpecification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSpecification.FormatString = ""
        repoSpecification.HeaderText = "Specification"
        repoSpecification.Name = colSpecification
        repoSpecification.Width = 100
        gv1.MasterTemplate.Columns.Add(repoSpecification)


        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRemarks)

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
        gv1.MasterTemplate.Columns.Add(repoUnitTotAddCost)

        Dim repoMannulaAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMannulaAmt.FormatString = ""
        repoMannulaAmt.HeaderText = "Is Mannual amount"
        repoMannulaAmt.Name = colIsMannualAmt
        repoMannulaAmt.IsVisible = False
        repoMannulaAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMannulaAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMannulaAmt)

        '' for abatenment PI
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
        gv1.MasterTemplate.Columns.Add(repoAbatementRate)

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
        'clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)
        '' Anubhooti 21-Oct-2014 BM00000004222
        Dim repoSRNUCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSRNUCost = New GridViewDecimalColumn()
        repoSRNUCost.FormatString = ""
        repoSRNUCost.HeaderText = "SRN Unit Cost"
        repoSRNUCost.Name = colSRNUnitCost
        repoSRNUCost.Width = 80
        repoSRNUCost.Minimum = 0
        repoSRNUCost.FormatString = "{0:n4}"
        repoSRNUCost.DecimalPlaces = 4
        repoSRNUCost.IsVisible = False
        repoSRNUCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSRNUCost)
        ''

        ''''''
        repoSRNUCost = New GridViewDecimalColumn()
        repoSRNUCost.FormatString = ""
        repoSRNUCost.WrapText = True
        repoSRNUCost.HeaderText = "Accepted Amount"
        repoSRNUCost.Name = colAcceptedAmount
        repoSRNUCost.Width = 80
        repoSRNUCost.Minimum = 0
        repoSRNUCost.ReadOnly = True
        repoSRNUCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSRNUCost)

        repoSRNUCost = New GridViewDecimalColumn()
        repoSRNUCost.FormatString = ""
        repoSRNUCost.WrapText = True
        repoSRNUCost.HeaderText = "Rejected Amount"
        repoSRNUCost.Name = colRejectedAmount
        repoSRNUCost.Width = 80
        repoSRNUCost.Minimum = 0
        repoSRNUCost.ReadOnly = True
        repoSRNUCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSRNUCost)

        repoSRNUCost = New GridViewDecimalColumn()
        repoSRNUCost.FormatString = ""
        repoSRNUCost.WrapText = True
        repoSRNUCost.HeaderText = "Shortage Amount"
        repoSRNUCost.Name = colShortageAmount
        repoSRNUCost.Width = 80
        repoSRNUCost.Minimum = 0
        repoSRNUCost.ReadOnly = True
        repoSRNUCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSRNUCost)

        repoSRNUCost = New GridViewDecimalColumn()
        repoSRNUCost.FormatString = ""
        repoSRNUCost.WrapText = True
        repoSRNUCost.HeaderText = "Leak Amount"
        repoSRNUCost.Name = colLeakAmount
        repoSRNUCost.Width = 80
        repoSRNUCost.Minimum = 0
        repoSRNUCost.ReadOnly = True
        repoSRNUCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSRNUCost)

        repoSRNUCost = New GridViewDecimalColumn()
        repoSRNUCost.FormatString = ""
        repoSRNUCost.WrapText = True
        repoSRNUCost.HeaderText = "Burst Amount"
        repoSRNUCost.Name = colBurstAmount
        repoSRNUCost.Width = 80
        repoSRNUCost.Minimum = 0
        repoSRNUCost.ReadOnly = True
        repoSRNUCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSRNUCost)

        repoSRNUCost = New GridViewDecimalColumn()
        repoSRNUCost.FormatString = ""
        repoSRNUCost.WrapText = True
        repoSRNUCost.HeaderText = "Amt Less Discount Without Shortage"
        repoSRNUCost.Name = colAmtLessDiscountWithoutShortage
        repoSRNUCost.Width = 80
        repoSRNUCost.Minimum = 0
        repoSRNUCost.ReadOnly = True
        repoSRNUCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSRNUCost)

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
        ''==============================================================================================

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
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
        repoTaxAmt.Minimum = 0
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
    Function checkVendorItemPrice() As Boolean
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowLargerItemCostThenVendorItemCost, clsFixedParameterCode.AllowLargerItemCostThenVendorItemCost, Nothing)) = 0 And clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchasePickItemFromVendorItemDetails, clsFixedParameterCode.PurchasePickItemFromVendorItemDetails, Nothing)) = 1 Then
            For i As Integer = 0 To gv1.Rows.Count - 1
                Dim strCode As String = clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value)
                Dim cellPrice As Double = clsCommon.myCdbl(gv1.Rows(i).Cells(colRate).Value)
                Dim vendorPrice As Double = clsDBFuncationality.getSingleValue("select item_rate from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' and item_no='" & strCode & "'")
                If cellPrice > vendorPrice Then
                    clsCommon.MyMessageBoxShow("The Larger Price Of Item is not Allowed then the Vendor Item Price  at Row no " & (i + 1))
                    Return False
                End If
            Next
        Else
            Return True
        End If
        Return True
    End Function
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    ''richa agarwal ERO/09/08/19-000990 add discount amount directly i.e. if dis percent is not given by user and discount amount is provided then it will be reflected into amin amoount
                    If e.Column Is gv1.Columns(colItemInsuranceAmt) OrElse e.Column Is gv1.Columns(colItemInsurancePer) OrElse e.Column Is gv1.Columns(colInsurancePer) OrElse e.Column Is gv1.Columns(colCategoryType) OrElse e.Column Is gv1.Columns(colCapexSubCode) OrElse e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colAmt) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colSpecification) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colDisPer) OrElse e.Column Is gv1.Columns(colDisPerUnit) OrElse e.Column Is gv1.Columns(colDetailDisAmt) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colBatchNo) OrElse e.Column Is gv1.Columns(colExpiry) OrElse e.Column Is gv1.Columns(colManufactureDate) OrElse (e.Column Is gv1.Columns(colAmt) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal) Then
                        If (e.Column Is gv1.Columns(colItemInsuranceAmt) OrElse e.Column Is gv1.Columns(colItemInsurancePer) OrElse e.Column Is gv1.Columns(colInsurancePer) OrElse (clsCommon.CompairString(e.Column.Name, colQty) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colRate) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDisPer) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDisPerUnit) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDetailDisAmt) = CompairStringResult.Equal) OrElse (e.Column Is gv1.Columns(colAmt) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal)) Then
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
                            If (clsCommon.CompairString(e.Column.Name, colQty) = CompairStringResult.Equal AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colSRNNo).Value) > 0) Then

                                Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPendingQty).Value)
                                Dim dblEnteredQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                                If dblEnteredQty > dblPendingQty Then
                                    common.clsCommon.MyMessageBoxShow("Entered Quantity Can't be more than Pending Quantity." + Environment.NewLine + "Entered Quantity : " + clsCommon.myCstr(dblEnteredQty) + ". Pending Quantity : " + clsCommon.myCstr(dblPendingQty))
                                    gv1.CurrentRow.Cells(colQty).Value = dblPendingQty
                                End If
                            End If

                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            If rbtnTaxCalManual.IsChecked Then
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRow(ii)
                                Next
                            End If
                            UpdateAllTotals()

                        ElseIf (clsCommon.CompairString(e.Column.Name, colICode) = CompairStringResult.Equal) Then
                            OpenICodeList(False)
                        ElseIf e.Column Is gv1.Columns(colAmt) Then

                            ' ''If clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) > 0 Then
                            ' ''    gv1.CurrentRow.Cells(colRate).Value = Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmt).Value) / clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), 2, MidpointRounding.ToEven)
                            ' ''Else
                            ' ''    gv1.CurrentRow.Cells(colAmt).Value = 0
                            ' ''End If

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

                        End If
                        setGridFocus()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim strItemType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value)
        If clsCommon.myLen(strItemType) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Row Type", Me.Text)
            Exit Sub
        End If
        If clsCommon.CompairString(strItemType, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow(Me, "Row type should be Misc", Me.Text)
            Exit Sub
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
    End Sub
    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colHSNNo).Value = ""
        gv1.CurrentRow.Cells(colItemTaxable).Value = False
        gv1.CurrentRow.Cells(colUnit).Value = ""
        gv1.CurrentRow.Cells(colMRP).Value = 0
        gv1.CurrentRow.Cells(colQty).Value = Nothing
        gv1.CurrentRow.Cells(colPendingQty).Value = Nothing
    End Sub
    Sub OpenCapexSubCodeList()
        Try
            gv1.CurrentRow.Cells(colCapexSubCode).Value = clsCapexBudget.getFinder("", gv1.CurrentRow.Cells(colCapexSubCode).Value, False)
            If clsCommon.myLen(gv1.CurrentRow.Cells(colCapexSubCode).Value) > 0 Then
                gv1.CurrentRow.Cells(colCapexCode).Value = clsCapexBudget.GetCapexCode(gv1.CurrentRow.Cells(colCapexSubCode).Value, Nothing)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub MakeColumnReadOnly(ByVal Read As Boolean)
        For Each gvrow As GridViewRowInfo In gv1.Rows
            gvrow.Cells(colCategoryType).ReadOnly = Read
            gvrow.Cells(colCapexCode).ReadOnly = Read
            gvrow.Cells(colCapexSubCode).ReadOnly = Read
            gvrow.Cells(colEmergency).ReadOnly = Read
        Next

    End Sub
    Private Sub setGridFocus()
        Try
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
    Private Sub UpdateAllTotals()
        Dim isInsuranceExists As Boolean = False
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colSRNNo).Value) <= 0 Then
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
                            If clsCommon.myLen(gv1.Rows(ii).Cells(colSRNNo).Value) <= 0 Then
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
        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        Dim dblTaxableAmount As Decimal = 0
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
            End If
        Next


        If rbtnTaxCalAutomatic.IsChecked Then
            For ii As Integer = 1 To gv2.Rows.Count
                Select Case (ii)
                    Case 1
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmountPurchase = True Then
                            'lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1)
                            dblTaxBaseAmt1 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxAmt1 = (dblTaxBaseAmt1 * txtTCSTaxRate.Value) / 100
                        End If
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
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmountPurchase = True Then
                            'lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1)
                            dblTaxBaseAmt2 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxAmt2 = (dblTaxBaseAmt2 * txtTCSTaxRate.Value) / 100
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                        If dblTaxBaseAmt2 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 3
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmountPurchase = True Then
                            'lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1 + dblTaxAmt2)
                            dblTaxBaseAmt3 = txttcstaxbaseamount.Value
                            dblTaxAmt3 = (dblTaxBaseAmt3 * txtTCSTaxRate.Value) / 100
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                        If dblTaxBaseAmt3 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 4
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmountPurchase = True Then
                            'lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1 + dblTaxAmt2 + dblTaxAmt3)
                            dblTaxBaseAmt4 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxAmt4 = (dblTaxBaseAmt4 * txtTCSTaxRate.Value) / 100
                        End If
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
                gv1.CurrentRow.Cells(colAssesableMRP).Value = gv1.CurrentRow.Cells(colMRP).Value - (gv1.CurrentRow.Cells(colMRP).Value * gv1.CurrentRow.Cells(colAbatementRate).Value / 100)
                gv1.CurrentRow.Cells(colTotalAssesableMRP).Value = gv1.CurrentRow.Cells(colQty).Value * gv1.CurrentRow.Cells(colAssesableMRP).Value
            End If
        Next

        Dim dblSecurityDed As Double = 0
        Dim dblQualityDed As Double = 0
        Dim dblRMLate As Double = 0

        'For Each gvrow As GridViewRowInfo In gv1.Rows
        For Each row As GridViewRowInfo In gvDeduction.Rows
            ' Check if the row is not a new row and type column is not null
            If row.Cells("type").Value IsNot Nothing Then
                Dim type As String = clsCommon.myCstr(row.Cells("type").Value)

                ' Depending on the type, accumulate the amount
                Dim amount As Decimal = clsCommon.myCdbl(row.Cells("amount").Value)

                Select Case type
                    Case "Security Deduction"
                        dblSecurityDed += amount
                    Case "Quality Deduction"
                        dblQualityDed += amount
                    Case "RM Late Penalty"
                        dblRMLate += amount
                        ' Add more cases if there are additional types
                End Select
            End If
        Next
        lblSecurityDeduction.Text = clsCommon.myFormat(dblSecurityDed)
        lblQualityDeduction.Text = clsCommon.myFormat(dblQualityDed)
        lblRMLate.Text = clsCommon.myFormat(dblRMLate)

        Dim dblTdsAmt As Double = 0
        Dim qry As String = " Select Is_TDS_Applicable from TSPL_VENDOR_MASTER WHERE VENDOR_CODE='" + txtVendorNo.Value + "' "
        Dim TDS As Integer = clsDBFuncationality.getSingleValue(qry)

        If TDS = 1 Then
            'lblSecurityDeduction.Text = clsCommon.myFormat(dblSecurityDed)
            dblTdsAmt = clsCommon.myFormat(lblAmtAfterDiscount.Text) * 0.1 / 100
            lblTdsAmt.Text = dblTdsAmt
            'lblTdsAmt.Text = lblAmtAfterDiscount * 0.1 / 100
        Else
            lblTdsAmt.Text = clsCommon.myFormat(0)
        End If

        lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt)
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblAmtAfterDis)
        lblTotalInsuranceAmt.Text = clsCommon.myFormat(dblItemInsuranceAmt)
        lblTaxableAmount.Text = clsCommon.myFormat(dblTaxableAmount)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
        lblAddCharges.Text = clsCommon.myFormat(dblACAmount)
        lblAddCharges1.Text = clsCommon.myFormat(dblACAmount)
        dblNetAmt = dblNetAmt + dblACAmount


        Dim Totalamtafded As Decimal = 0
        Totalamtafded = dblNetAmt - (dblSecurityDed + dblQualityDed + dblRMLate + dblTdsAmt)
        Dim dclROAmt As Decimal = 0
        Dim dblpiamt As Decimal = 0
        If SettingAutoRoundOffSeprateAccountOnVendorTransaction AndAlso objCommonVar.RCDFCFP Then
            'dclROAmt = Math.Round(dblNetAmt, 0, MidpointRounding.AwayFromZero) - dblNetAmt
            dclROAmt = Math.Round(Totalamtafded, 0, MidpointRounding.AwayFromZero) - Totalamtafded
            'dblNetAmt = Math.Round(dblNetAmt, 0, MidpointRounding.AwayFromZero)
            dblpiamt = Math.Round(Totalamtafded, 0, MidpointRounding.AwayFromZero)
        Else
            dclROAmt = Math.Round(dblNetAmt, 0, MidpointRounding.AwayFromZero) - dblNetAmt
            dblNetAmt = Math.Round(dblNetAmt, 0, MidpointRounding.AwayFromZero)
        End If


        If objCommonVar.RCDFCFP Then
            lblRoundOff.Text = clsCommon.myFormat(dclROAmt)
            lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
            'lblDocAmount.Text = lblTotRAmt.Text
            lblDocAmount.Text = clsCommon.myFormat(dblpiamt)
            lblPIAmt.Text = clsCommon.myFormat(dblpiamt)
            dbltotamtafterded.Text = clsCommon.myFormat(Totalamtafded)
        Else
            lblRoundOff.Text = clsCommon.myFormat(dclROAmt)
            lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
            lblDocAmount.Text = lblTotRAmt.Text
        End If


        Calc_AddtionalCharge_Itemwise(dblTotalQuantity)
    End Sub
    Private Sub Calc_AddtionalCharge_Itemwise(ByVal TotalQty As Double)
        Try
            Dim dblTotLandedCost As Double = 0
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(colICode).Value) > 0 Then
                    dblTotLandedCost += clsCommon.myCdbl(grow.Cells(colLandedAmt).Value)
                End If
            Next

            TotalQty = dblTotLandedCost ''now additional is calc. on landed amt based

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
                qty = clsCommon.myCdbl(grow.Cells(colLandedAmt).Value) 'clsCommon.myCdbl(grow.Cells(colQty).Value)
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
        LOCATIONRIGTHS()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnPost.Visible = True
        btnDelete.Enabled = True
        txtVehicleNo.Enabled = True
        lblVehicle.ReadOnly = False
        chkTDSProvision.Checked = False
        txtDate.Focus()
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        chkAgainstCForm.Checked = False
        If clsFixedParameter.GetData(clsFixedParameterCode.DisableShipToLocation, clsFixedParameterType.DisableShipToLocation, Nothing) = "1" Then
            txtShipToLocation.Enabled = False
        Else
            txtShipToLocation.Enabled = True
        End If
        chkShorategeIncludeInLandedCost.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterCode.IsShortageIncludeInLandedCost, clsFixedParameterType.IsShortageIncludeInLandedCost, Nothing)) = 1, True, False)
        UcAttachment1.BlankAllControls()
        AllowDepartmentMandatoryOnPurchaseCycle()
        txtDept.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select segment_code from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtDept.Value) > 0 Then
            lblDept.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_GL_SEGMENT_CODE where Seg_No=3 and segment_code='" + txtDept.Value + "' "))
        End If
        btnPurchaseTaxInvoice.Visible = False
        FillVendorDetails()
        isAgainstTender = False
    End Sub
    Function AllowToSave() As Boolean
        Try
            '= KUNAL > TICKET : BM00000009580 =======
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If

            If InvDate1.Checked = True Then
                If clsCommon.GetDateWithStartTime(InvDate1.Value) > clsCommon.GetDateWithEndTime(txtDate.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Invoice Date can't be greater than Document Date", Me.Text)
                    Return False
                End If
            End If

            If btnSave.Text = "Update" Then
                Dim strchk As String = "select Status from TSPL_PI_HEAD where PI_No='" + txtDocNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow(Me, "Transaction already posted", Me.Text)
                    Return False
                End If
            End If
            CalculateInsuranceTotal(False)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If PurchaseModulePickFixTaxRate AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colSRNNo).Value) <= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
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
            Dim isTDSOverride As Boolean = False
            If objRemittance IsNot Nothing Then
                If objRemittance.IsTDSOverride Then
                    isTDSOverride = True
                End If
            End If
            '==================Added by preeti gupta[11/01/2017]
            If ShowMessageTDS Then
                If (common.clsCommon.MyMessageBoxShow("Do you want to Deduct TDS", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No) Then
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



            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Vendor", Me.Text)
                txtVendorNo.Focus()
                Return False
            End If

            If clsCommon.CompairString(cmbDocType.SelectedValue, "PI") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group", Me.Text)
                    txtTaxGroup.Focus()
                    Return False
                End If
            End If
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Bill to Location", Me.Text)
                txtBillToLocation.Focus()
                Return False
            End If

            If clsCommon.CompairString("O", cboPOType.SelectedValue) = CompairStringResult.Equal Then
                If clsCommon.myLen(txtSubLocation.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select Sub Location", Me.Text)
                    Return False
                End If
            End If
            'If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "F") = CompairStringResult.Equal AndAlso clsLocation.isLocatinExcisable(txtBillToLocation.Value) Then
            '    common.clsCommon.MyMessageBoxShow("Location Can't be excisable of finished goods")
            '    Return False
            'End If
            If clsCommon.myLen(clsCommon.myCstr(txtBillToLocation.Value)) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                    If clsCommon.myLen(txtSubLocation.Value) <= 0 And chkJobWorkOutward.Checked = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please select Sub Location", Me.Text)
                        txtSubLocation.Focus()
                        Return False
                    End If
                End If
            End If
            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "PI No Not found to save", Me.Text)
                txtDocNo.Focus()
                Return False
            End If

            If clsCommon.myLen(txtVendorInvoiceNo.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please enter Invoice No", Me.Text)
                txtVendorInvoiceNo.Focus()
                Return False
            End If
            If ShowItemAllStructureWise = False Then
                If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select Item Type", Me.Text)
                    cboItemType.Focus()
                    Return False
                End If
            End If

            If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, SRN_Date,103) from TSPL_SRN_HEAD where SRN_No ='" + txtReqNo.Value + "'")) > clsCommon.myCDate(txtDate.Value) Then
                txtDate.Focus()
                Throw New Exception("Date cannot be less than from SRN Date")
            End If

            Dim dtCurrDate As Date = clsCommon.GETSERVERDATE()
            If txtDate.Value > dtCurrDate Then
                common.clsCommon.MyMessageBoxShow(Me, "Invoice Date can't be more then Current Date", Me.Text)
                txtDate.Focus()
                Return False
            End If

            If dtImportEntryDate.Value > dtCurrDate Then
                common.clsCommon.MyMessageBoxShow(Me, "Import Bill Entry Date can't be more then Current Date", Me.Text)
                dtImportEntryDate.Focus()
                Return False
            End If

            If clsCommon.myLen(txtReqNo.Value) > 0 Then
                dtCurrDate = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select SRN_Date from TSPL_SRN_HEAD where SRN_No='" + txtReqNo.Value + "'"))
                If clsCommon.GetDateWithStartTime(dtCurrDate) > clsCommon.GetDateWithEndTime(txtDate.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Purchase invoice Date can't be less than SRN Date", Me.Text)
                    txtDate.Focus()
                    Return False
                End If
            End If



            ''If clsCommon.CompairString("R", cboItemType.SelectedValue) = CompairStringResult.Equal AndAlso Not (clsLocation.isLocatinExcisable(txtBillToLocation.Value)) Then
            ''    common.clsCommon.MyMessageBoxShow("Location should be Excisable for Raw Material")
            ''    txtBillToLocation.Focus()
            ''    Return False
            ''End If

            Dim arrReqNo As New List(Of String)
            Dim arrICode As New List(Of String)()
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSRNNo).Value)
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)

                Dim dblUnitCost As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
                Dim dblAmtAfterDis As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)

                If clsCommon.myLen(strReqNo) > 0 Then
                    If Not (arrReqNo.Contains(strReqNo)) Then
                        arrReqNo.Add(strReqNo)
                    End If
                    If dblQty > dblPendingQty Then
                        common.clsCommon.MyMessageBoxShow("Item " + strICode + "( " + strIName.Trim() + " ) Entered Quantity(" + clsCommon.myCstr(dblQty) + ") Can't be more Pending Quantity(" + clsCommon.myCstr(dblPendingQty) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        Return False
                    End If
                End If
                If Not arrICode.Contains(strICode) Then
                    arrICode.Add(strICode)
                End If

                ''-----------richa agarwal 31/03/2016
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Item " + strICode + "( " + strIName.Trim() + " ) Entered Unit Cost Can't be Zero.At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        Return False
                    End If
                End If

                If dblAmtAfterDis < 0 Then
                    clsCommon.MyMessageBoxShow(Me, " Amount After discount Cannot be in Negative. ")
                    Return False
                End If
                ''-----------------
                If clsCommon.myLen(strICode) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        If ShowCapexCodeandSubCode Then
                            Dim Category As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colCategoryType).Value)
                            Dim Emergency As String = CInt(gv1.Rows(ii).Cells(colEmergency).Value)
                            Dim CapexCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexCode).Value)
                            Dim CapexSubCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexSubCode).Value)
                            If clsCommon.CompairString(Category, "") = CompairStringResult.Equal Then
                                clsCommon.MyMessageBoxShow("Fill category at row no. " + clsCommon.myCstr(ii + 1) + "")
                                Return False
                            ElseIf clsCommon.CompairString(Category, "Capex") = CompairStringResult.Equal Then
                                If clsCommon.myLen(CapexSubCode) <= 0 Then
                                    clsCommon.MyMessageBoxShow("Fill capex sub code at row no. " + clsCommon.myCstr(ii + 1) + "")
                                    Return False
                                End If
                            End If
                        End If
                    End If
                End If

                '' added code by parteek HSN Code related
                Dim IsSkip As Boolean = False
                IsSkip = clsDBFuncationality.getSingleValue("select case when isnull( Skip_GST,0)=1 then 1 else 0 end as Skip_GST from tspl_item_master where item_code='" & clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) & "'")
                If clsERPFuncationality.GetGSTStatus(txtDate.Value) AndAlso IsSkip = False Then
                    If ShowItemAllStructureWise = False Then
                        If clsCommon.CompairString(cboItemType.SelectedValue, "N") <> CompairStringResult.Equal Then
                            Dim taxamt As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value)
                            Dim HSNCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colHSNNo).Value)

                            If clsCommon.myCdbl(taxamt) > 0 AndAlso clsCommon.myLen(HSNCode) <= 0 Then
                                clsCommon.MyMessageBoxShow("HSN Code is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                                Return False
                            End If

                        End If
                    End If
                End If
                '' ===== ENd of code===




            Next

            For ii As Integer = 0 To gvAC.Rows.Count - 1
                If clsCommon.myLen(gvAC.Rows(ii).Cells(colACCode).Value) > 0 Then
                    For jj As Integer = 0 To gvAC.Rows.Count - 1
                        If ii = jj Then
                            Continue For
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gvAC.Rows(ii).Cells(colACCode).Value), clsCommon.myCstr(gvAC.Rows(jj).Cells(colACCode).Value)) = CompairStringResult.Equal Then
                            common.clsCommon.MyMessageBoxShow("Additional Charges: " + clsCommon.myCstr(gvAC.Rows(ii).Cells(colACCode).Value) + "Repeated at Row No " + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1) + "")
                            Return False
                        End If
                    Next
                End If
            Next

            ''richa agarwal 
            Dim strcurrentfisyearstartdate As Date? = Nothing
            Dim strcurrentfisyearenddate As Date? = Nothing
            Dim strcurrentfisyear As String = String.Empty
            Dim strmonth As Integer = Convert.ToDateTime(txtDate.Value).Month()
            Dim stryear As Integer = Convert.ToDateTime(txtDate.Value).Year()
            If strmonth <= 3 Then
                strcurrentfisyearstartdate = "01-Apr-" + clsCommon.myCstr(stryear - 1)
                strcurrentfisyearenddate = "31-Mar-" + clsCommon.myCstr(stryear)
                strcurrentfisyear = clsCommon.myCstr(stryear - 1) + "-" + clsCommon.myCstr(stryear)
            Else
                strcurrentfisyearstartdate = "01-Apr-" + clsCommon.myCstr(stryear)
                strcurrentfisyearenddate = "31-Mar-" + clsCommon.myCstr(stryear + 1)
                strcurrentfisyear = clsCommon.myCstr(stryear) + "-" + clsCommon.myCstr(stryear + 1)
            End If
            ''------------------


            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select PI_No from TSPL_PI_HEAD where Vendor_Invoice_No='" + txtVendorInvoiceNo.Text + "' and Vendor_Code='" + txtVendorNo.Value + "' and convert(date,PI_Date,103)>= convert(date,'" & strcurrentfisyearstartdate & "',103)  and convert(datetime,PI_Date,103)<= convert(datetime ,'" & strcurrentfisyearenddate & "',103)  and PI_No not in('" + txtDocNo.Value + "')")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                common.clsCommon.MyMessageBoxShow("Vendor Invoice No:" + txtVendorInvoiceNo.Text + " Already used in Purchase Invoice No: " + clsCommon.myCstr(dt.Rows(0)("PI_No")) + " in current financial year " & strcurrentfisyear & ".")
                txtVendorInvoiceNo.Focus()
                Return False
            End If
            If Not checkVendorItemPrice() Then
                Return False
            End If
            If ShowItemAllStructureWise = False Then
                clsItemMaster.isItemOfSameType(clsCommon.myCstr(cboItemType.SelectedValue), cboItemType.Text, arrICode)
            End If
            clsSRNHead.IsValidVendorForSRN(arrReqNo, txtVendorNo.Value)
            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()

            'clsLocationWiseTax.IsValidTaxGroup(txtTaxGroup.Value, txtBillToLocation.Value, txtVendorNo.Value, "P", txtDate.Value, Nothing)
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
            If Not isSkipGST AndAlso clsCommon.CompairString(cmbDocType.SelectedValue, "PI") = CompairStringResult.Equal Then
                clsLocationWiseTax.IsValidTaxGroup(txtTaxGroup.Value, txtBillToLocation.Value, txtVendorNo.Value, "P", txtDate.Value, Nothing)
            End If
            ''End of For GST Skip

            If AllowtoChangeTCSBaseAmountPurchase Then
                If clsCommon.myCdbl(txttcstaxbaseamount.Value) > clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text) Then
                    Throw New Exception("TCS Tax Base amount should not be greater than Actual TCS Tax Base Amount")
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
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Is_Provisional from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'")) = 1 Then
                If common.clsCommon.MyMessageBoxShow("Do You Want to Apply TDS Provision", "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                    chkTDSProvision.Checked = True
                Else
                    chkTDSProvision.Checked = False
                End If

            End If
            'If isRCDFRateControl Then
            '    If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
            '        Dim strItem As String = ""
            '        Dim isCheck As Boolean = False
            '        For Each grow As GridViewRowInfo In gv1.Rows
            '            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colICode).Value)) > 0 Then
            '                Dim Qry As String = "Select TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM.Code,TSPL_RCDF_RATE_CONTROL_DETAIL.Item_Code,TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM.UOM,TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM.Min_Rate,TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM.Max_Rate from TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM
            '                            Inner Join TSPL_RCDF_RATE_CONTROL_DETAIL On TSPL_RCDF_RATE_CONTROL_DETAIL.PK_Id=TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM.Against_PK_Id
            '                            Where TSPL_RCDF_RATE_CONTROL_DETAIL.Item_Code='" + clsCommon.myCstr(grow.Cells(colICode).Value) + "'"
            '                Dim dtt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            '                If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
            '                    For Each rows As DataRow In dtt.Rows
            '                        If clsCommon.CompairString(clsCommon.myCstr(rows("Item_Code")), clsCommon.myCstr(grow.Cells(colICode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(rows("UOM")), clsCommon.myCstr(grow.Cells(colUnit).Value)) = CompairStringResult.Equal Then
            '                            If clsCommon.myCDecimal(grow.Cells(colRate).Value) < clsCommon.myCDecimal(rows("Min_Rate")) OrElse clsCommon.myCDecimal(grow.Cells(colRate).Value) > clsCommon.myCDecimal(rows("Max_Rate")) Then
            '                                strItem += "Item : " + clsCommon.myCstr(grow.Cells(colICode).Value) + " " + clsCommon.myCstr(grow.Cells(colIName).Value) + " " + Environment.NewLine
            '                                strItem += "Unit Cost : " + clsCommon.myCstr(grow.Cells(colRate).Value) + " " + Environment.NewLine
            '                                strItem += "According to RCDF Rate Control unit cost should be in range (" + clsCommon.myCstr(rows("Min_Rate")) + " to " + clsCommon.myCstr(rows("Max_Rate")) + ") . " + Environment.NewLine
            '                                isCheck = True
            '                            End If
            '                        End If
            '                    Next
            '                End If
            '            End If
            '        Next
            '        If isCheck Then
            '            clsCommon.MyMessageBoxShow(Me, strItem, Me.Text)
            '            Return False
            '        End If
            '        isCheck = False
            '    End If
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Private Function SRNItemRate(ByVal strSRNNo As String, ByVal strItemCode As String) As Double
        Dim ItemRate As Double = 0
        ItemRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select TSPL_SRN_DETAIL.Item_Cost  From TSPL_PI_DETAIL  Left Outer Join TSPL_SRN_DETAIL  On TSPL_SRN_DETAIL.SRN_No    = TSPL_PI_DETAIL.SRN_Id  Where SRN_Id ='" & strSRNNo & "' And TSPL_SRN_DETAIL.Item_Code ='" & strItemCode & "'"))
        Return ItemRate
    End Function
    Sub SaveData(ByVal ChekPostBtn As Boolean)
        Try
            '' Anubhooti 13-Sep-2014 BM00000003735
            If ChekPostBtn = False Then
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Purchase Invoice", txtDate.Value) = False Then
                    Exit Sub
                End If
            End If
            ''


            If (AllowToSave()) Then

                '==================check approval condition=============================
                Dim totalqty As Decimal = 0
                Dim count As Integer = 0
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPOID).Value)
                    Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                    Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                    Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value)
                    Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)

                    Dim dblUnitCost As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)


                    If UnitInceaseValue = True Then
                        Dim PurchaseRate As Decimal = clsDBFuncationality.getSingleValue("select item_cost from TSPL_PURCHASE_ORDER_DETAIL where item_code='" & strICode & "' and PurchaseOrder_No='" & strReqNo & "'")
                        If clsCommon.myCdbl(dblUnitCost) <> clsCommon.myCdbl(PurchaseRate) Then
                            clsCommon.MyMessageBoxShow("Unit Cost does not match with Purchase Order. " + clsCommon.myCstr(ii + 1) + "")

                            If Not AllowModifcationByApprovalUser Then
                                clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value))
                                count += 1
                            End If
                            Continue For
                        End If
                    End If
                Next



                '=====================end here===================

                Dim obj As New clsPurchaseInvoiceHead()
                obj.isJobWorkOutward = IIf(chkJobWorkOutward.Checked = True, 1, 0)
                obj.PI_No = txtDocNo.Value
                obj.PI_Date = txtDate.Value
                obj.Vendor_Code = txtVendorNo.Value
                obj.Vendor_Name = lblVendorName.Text
                obj.Ref_No = txtRefNo.Text
                obj.Total_Tax_Amt = clsCommon.myCdbl(lblTaxAmt.Text)
                obj.Remarks = txtRemarks.Text
                obj.Dept = txtDept.Value
                obj.Dept_Desc = lblDept.Text
                obj.Bill_To_Location = txtBillToLocation.Value
                obj.Ship_To_Location = txtShipToLocation.Value
                obj.Sublocation_Code = txtSubLocation.Value
                obj.Comments = txtComment.Text
                obj.On_Hold = chkOnHold.Checked
                obj.Description = txtDesc.Text
                obj.Vendor_Invoice_No = txtVendorInvoiceNo.Text
                obj.Tax_Group = txtTaxGroup.Value
                'If ShowItemAllStructureWise = True Then
                '    If gv1.Rows.Count > 0 Then
                '        Dim itemcode As String = clsCommon.myCstr(gv1.Rows(0).Cells(colICode).Value)
                '        Dim itemtype As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 item_type from TSPL_ITEM_MASTER where Item_Code ='" + itemcode + "'"))
                '        obj.Item_Type = itemtype
                '    End If
                'Else
                obj.Item_Type = clsCommon.myCstr(cboItemType.SelectedValue)
                '   End If
                obj.Against_C_Form = chkAgainstCForm.Checked
                obj.Document_Type = clsCommon.myCstr(cmbDocType.SelectedValue)
                If InvDate1.Checked Then
                    obj.Invdate = InvDate1.Value
                End If
                obj.TDS_Provision = chkTDSProvision.Checked
                obj.PROJECT_ID = fndProject.Text

                obj.TapalNo = clsCommon.myCstr(txtTapalNo.Text)
                If txtDataAndTimeSelection.Checked Then
                    obj.DateAndTime = txtDataAndTimeSelection.Value
                End If
                If clsCommon.myCBool(chkITCEligible.Checked) = True Then
                    obj.ITC_Elibible = IIf(chkITCEligible.Checked, 1, 0)
                    If clsCommon.myCdbl(CboxITCType.SelectedIndex) = 0 Then
                        obj.ITC_Type = 1
                    Else
                        obj.ITC_Type = 0
                    End If

                    obj.ITC_Type_Category = CboxITCCateogory.SelectedValue
                End If

                obj.Import_Entry_No = txtImportEntryNo.Text
                If clsCommon.myLen(txtImportEntryNo.Text) > 0 Then
                    obj.Import_Entry_Date = dtImportEntryDate.Value
                End If
                obj.Port = txtport.Text
                obj.PI_Type = cboPOType.SelectedValue

                obj.Against_SRN = txtReqNo.Value
                If clsCommon.myLen(obj.Against_SRN) > 0 Then
                    obj.Against_MRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_MRN FROM TSPL_SRN_HEAD WHERE SRN_No='" + obj.Against_SRN + "'"))
                End If
                If clsCommon.myLen(obj.Against_MRN) > 0 Then
                    obj.Against_GRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_GRN FROM TSPL_MRN_HEAD WHERE MRN_No='" + obj.Against_MRN + "'"))
                End If
                If clsCommon.myLen(obj.Against_GRN) > 0 Then
                    obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_PO FROM TSPL_GRN_HEAD WHERE GRN_No='" + obj.Against_GRN + "'"))
                End If
                If clsCommon.myLen(obj.Against_PO) > 0 Then
                    obj.Against_Requisition = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PurchaseOrder_No='" + obj.Against_PO + "'"))
                    obj.GSTRegistered = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select GSTRegistered  from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" & obj.Against_PO & "' "))
                Else
                    obj.GSTRegistered = IIf(clsVendorMaster.IsGSTRegisteredVendor(obj.Vendor_Code, Nothing), 1, 0)
                End If

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
                obj.Total_Add_Charge_Insurance = clsCommon.myCdbl(lblAddChargesForInsurance.Text)
                obj.Total_Item_Insurance_Amt = clsCommon.myCdbl(lblTotalInsuranceAmt.Text)
                obj.Retention = clsCommon.myCdbl(TxtRetention.Text)
                If rbtnTaxCalAutomatic.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                ElseIf rbtnTaxCalManual.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
                End If

                obj.is_Excise_On_Qty = chkExciseOnQty.Checked
                obj.objPIRemittance = clsPIRemittance.Convert(objRemittance, dblPreviousTDSAmt)

                obj.Terms_Code = txtTermCode.Value
                ''RICHA AGARWAL CHANGES DONE ON 16/08/2016 ACCORDING TO RANJANA MAM CHNAGES DONE ON PURCHASE INVOICE ONLE NOT ON SRN, MRN,GRN
                Dim DBLNOOFDAYS As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select TSPL_TERMS_MASTER.No_Days from  TSPL_TERMS_MASTER WHERE TSPL_TERMS_MASTER.Terms_Code ='" & clsCommon.myCstr(obj.Terms_Code) & "'"))
                obj.Due_Date = clsCommon.myCDate(txtDate.Value).AddDays(DBLNOOFDAYS)

                'obj.Due_Date = txtDueDate.Value
                ''----------------------
                obj.ActualTCSBaseAmount = clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text)
                obj.ChangedTCSBaseAmount = clsCommon.myCdbl(txttcstaxbaseamount.Value)

                obj.Discount_Base = clsCommon.myCdbl(lblAmtWithDiscount.Text)
                obj.Discount_Amt = clsCommon.myCdbl(lblDiscountAmt.Text)
                obj.Amount_Less_Discount = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
                obj.Total_Taxable_Amount = clsCommon.myCdbl(lblTaxableAmount.Text)
                obj.PI_Total_Amt = clsCommon.myCdbl(lblTotRAmt.Text)
                obj.RoundOffAmt = clsCommon.myCdbl(lblRoundOff.Text)
                obj.Carrier = txtCarrier.Text
                obj.GENo = txtGENo.Text
                If txtGEDate.Checked Then
                    obj.GEDate = txtGEDate.Value
                End If

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
                obj.Tot_Empty_Amount = 0
                obj.IsAbatementPO = IsAbatementPO
                obj.Is_Against_Form = chkAgainstForm.Checked
                obj.Against_Form = ddlForm.SelectedValue
                obj.GRNo = clsCommon.myCstr(txtGRNo.Text)
                obj.GR_Date = dtpGRDate.Value
                obj.VehicleNo = clsCommon.myCstr(txtVehicleNo.Value)
                obj.Transporter = clsCommon.myCstr(txtTransporter.Value)
                obj.Transport_Date = dtpTransportDate.Value
                obj.TransporterDesc = Me.lblTransporterName.Text
                obj.VehicleDesc = Me.lblVehicle.Text
                obj.LRNo = txtLRNo.Text

                obj.Total_Accepted_Amount = clsCommon.myCdbl(lblAcceptedAmt.Text)
                obj.Total_Rejected_Amount = clsCommon.myCdbl(lblRejectedAmt.Text)
                obj.Total_Shortage_Amount = clsCommon.myCdbl(lblShortageAmt.Text)
                obj.Total_Leak_Amount = clsCommon.myCdbl(lblLeakAmt.Text)
                obj.Total_Burst_Amount = clsCommon.myCdbl(lblBurstAmt.Text)
                obj.Is_Shortage_Include_In_Landed_Cost = chkShorategeIncludeInLandedCost.Checked

                obj.Arr = New List(Of clsPurchaseInvoiceDetail)
                Dim objhsn As New ClsHSNMaster()
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsPurchaseInvoiceDetail()
                    Dim hsn As String = Nothing

                    hsn = clsCommon.myCstr(grow.Cells(colHSNNo).Value)
                    Dim hsncode As String = clsItemMaster.checkHSNCode(hsn, Nothing)
                    If clsCommon.CompairString(hsn, hsncode) = CompairStringResult.Equal Then

                    Else
                        Dim isnew As Boolean = True
                        objhsn.Code = hsn
                        ClsHSNMaster.SaveData(objhsn, isnew)
                        clsItemMaster.UpdateHSNCode(hsn, clsCommon.myCstr(grow.Cells(colICode).Value), Nothing)
                    End If

                    objTr.Category = clsCommon.myCstr(grow.Cells(colCategoryType).Value)
                    objTr.Emergency = CInt(clsCommon.myCdbl(grow.Cells(colEmergency).Value))
                    objTr.Capex_Code = clsCommon.myCstr(grow.Cells(colCapexCode).Value)
                    objTr.Capex_SubCode = clsCommon.myCstr(grow.Cells(colCapexSubCode).Value)

                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.PI_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Row_Type = clsCommon.myCstr(grow.Cells(colRowType).Value)
                    ''objTr.Rejected_Qty = clsCommon.myCdbl(grow.Cells(colRejectedQty).Value)
                    objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    ''added bu usha--------
                    objTr.Free_qty = clsCommon.myCdbl(grow.Cells(colfreeQty).Value)
                    '---end-----
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.SRN_Id = clsCommon.myCstr(grow.Cells(colSRNNo).Value)
                    objTr.PO_ID = clsCommon.myCstr(grow.Cells(colPOID).Value)
                    objTr.GRN_ID = clsCommon.myCstr(grow.Cells(colGRNID).Value)
                    objTr.MRN_ID = clsCommon.myCstr(grow.Cells(colMRNID).Value)

                    'objTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Disc_Type = clsCommon.myCdbl(grow.Cells(colDisType).Value)
                    objTr.Disc_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Value)

                    objTr.Disc_Per_Unit = clsCommon.myCdbl(grow.Cells(colDisPerUnit).Value)
                    objTr.Disc_Amt_Per_Unit = clsCommon.myCdbl(grow.Cells(colDisAmtPerUnit).Value)

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
                    objTr.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                    ''objTr.Assessable = clsCommon.myCdbl(grow.Cells(colAssessableRate).Value)
                    ''objTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
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

                    objTr.Leak_Qty = clsCommon.myCdbl(grow.Cells(colLeakQty).Value)
                    objTr.Burst_Qty = clsCommon.myCdbl(grow.Cells(colBurstQty).Value)
                    objTr.Short_Qty = clsCommon.myCdbl(grow.Cells(colShortQty).Value)
                    objTr.Reject_Qty = clsCommon.myCdbl(grow.Cells(colRejectQty).Value)
                    objTr.Is_Mannual_Amt = clsCommon.myCdbl(grow.Cells(colIsMannualAmt).Value)
                    objTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)


                    objTr.Landed_Cost_Rate = clsCommon.myCdbl(grow.Cells(colLandedRate).Value)
                    objTr.Landed_Cost_Amount = clsCommon.myCdbl(grow.Cells(colLandedAmt).Value)


                    objTr.Total_AddtionalCost_PerUnit = clsCommon.myCdbl(grow.Cells(colUnitTotAddCost).Value)
                    objTr.Total_NonRecTax_PerUnit = clsCommon.myCdbl(grow.Cells(colUnitTotNonRecTax).Value)
                    objTr.Total_RecTax_PerUnit = clsCommon.myCdbl(grow.Cells(colUnitTotRecTax).Value)

                    If clsItemMaster.IsItemTypeEmpty(objTr.Item_Code, objTr.Unit_code, Nothing) Then
                        Dim dblVal As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DefaultValue, objTr.Unit_code, Nothing))
                        objTr.Empty_Amount = dblVal * objTr.PI_Qty
                        obj.Tot_Empty_Amount += objTr.Empty_Amount
                    End If
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
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
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
                    Else
                        obj.ApplicableFrom = Nothing
                    End If
                Else
                    obj.CURRENCY_CODE = Nothing
                    obj.ConvRate = 1
                    obj.ApplicableFrom = Nothing
                End If

                obj.Arr_ACInsurance = New List(Of clsPIAdditionChargeInsurance)
                For Each grow As GridViewRowInfo In gvACInsurance.Rows
                    Dim objtr As New clsPIAdditionChargeInsurance()
                    objtr.AC_Code = clsCommon.myCstr(grow.Cells(colACInsuranceCode).Value)
                    objtr.Amount = clsCommon.myCdbl(grow.Cells(colACInsuranceAmount).Value)
                    If clsCommon.myLen(objtr.AC_Code) > 0 Then
                        obj.Arr_ACInsurance.Add(objtr)
                    End If
                Next

                'obj.objJVC = SetPJVData(obj)
                '' end CurrencyConversion
                ' Dim trans As SqlTransaction
                If iscalculationonrejqty Then
                    'By Balwinder on 18/12/2014 no need to make debit note.
                    'If ChekPostBtn And Is_SRN_Rej_Store_true Then
                    '    Dim trans As SqlTransaction
                    '    obj.SaveDebitNoteEntry(obj, trans)
                    'End If
                Else
                    If (obj.SaveData(obj, isNewEntry)) Then
                        ''richa agarwal code commented after discussion with Balwinder sir
                        '                        If ChekPostBtn And Is_SRN_Rej_Store_true Then
                        '                            iscalculationonrejqty = True
                        '                            ' Dim rejqty As Double
                        '                            If iscalculationonrejqty Then
                        '                                Dim Dtrej As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_SRN_DETAIL where SRN_No='" & obj.Against_SRN & "'")
                        '                                If Dtrej.Select("Rejected_Qty>0").Length <= 0 Then
                        '                                    iscalculationonrejqty = False
                        '                                    GoTo a
                        '                                End If
                        '                                For Each dr As DataRow In Dtrej.Rows()
                        '                                    If dr("Rejected_Qty") > 0 Then
                        '                                        For Each grow As GridViewRowInfo In gv1.Rows
                        '                                            If clsCommon.myCstr(grow.Cells(colICode).Value) = dr("Item_Code") Then
                        '                                                grow.Cells(colQty).Value = dr("Rejected_Qty")
                        '                                            End If
                        '                                        Next
                        '                                        For ii As Integer = 0 To gv1.Rows.Count - 1
                        '                                            UpdateCurrentRow(ii)
                        '                                        Next
                        '                                        UpdateAllTotals()
                        '                                    End If
                        '                                Next

                        '                                SaveData(ChekPostBtn)
                        '                                iscalculationonrejqty = False
                        '                            End If
                        '                        End If
                        'a:                      UcAttachment1.SaveData(obj.PI_No)

                        If ChekPostBtn = False Then
                            UcAttachment1.SaveData(obj.PI_No)
                            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        End If
                        If count > 0 Then
                            Dim xNewDesc As String = ""
                            xNewDesc = "Party Name : " + obj.Vendor_Name

                            xNewDesc = xNewDesc + Environment.NewLine + "Description : " + obj.Description

                            txtDocNo.Value = obj.PI_No

                            clsApply_Approval.CheckApprovalRequired(MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value), txtDate.Text, clsCommon.myCstr(xNewDesc), clsCommon.myCstr(txtRemarks.Text), clsCommon.myCdbl(lblTotRAmt.Text), clsCommon.myCdbl(totalqty), txtDept.Value)
                        End If

                        LoadData(obj.PI_No, NavigatorType.Current)

                    End If
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim obj As New clsPurchaseInvoiceHead
                obj.PI_No = clsCommon.myCstr(txtDocNo.Value)
                obj.Is_Against_Form = chkAgainstForm.Checked
                obj.Against_Form = ddlForm.SelectedValue
                obj.GRNo = txtGRNo.Text
                obj.GR_Date = dtpGRDate.Value
                obj.VehicleNo = txtVehicleNo.Value
                obj.Transporter = clsCommon.myCstr(txtTransporter.Value)
                obj.Transport_Date = dtpTransportDate.Value
                obj.TransporterDesc = Me.lblTransporterName.Text
                obj.VehicleDesc = Me.lblVehicle.Text
                obj.LRNo = txtLRNo.Text
                If clsPurchaseInvoiceHead.UpdateSecondaryInfo(obj, Nothing) Then
                    clsCommon.MyMessageBoxShow(Me, "Information updated successfully.", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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

                '' richa agarwal 31/03/2016  -------------- BM00000009025
                If dblTotItemCost <> 0 Then
                    dblLandedCost = gv1.Rows(ii).Cells(colAmt).Value / dblTotItemCost * dblAdditionalAmt
                End If
                ''----------------------------

                If dblAdditionalAmt = 0 Then
                    gv1.Rows(ii).Cells(colUnitTotAddCost).Value = 0
                Else
                    If dblLandedCost <> 0 Then
                        gv1.Rows(ii).Cells(colUnitTotAddCost).Value = Math.Round(dblLandedCost / (CDec(gv1.Rows(ii).Cells(colQty).Value) + CDec(gv1.Rows(ii).Cells(colLeakQty).Value) + CDec(gv1.Rows(ii).Cells(colShortQty).Value) + CDec(gv1.Rows(ii).Cells(colRejectQty).Value) + CDec(gv1.Rows(ii).Cells(colBurstQty).Value)), 6)
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
                        dblTotAmtAfterDiscount = dblTotAmtAfterDiscount + IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisType).Value) = 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value))
                    Else
                        dblTotAmtAfterDiscount = dblTotAmtAfterDiscount + IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisType).Value) = 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtLessDiscountWithoutShortage).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value))
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                    dblAdditionalAmt = dblAdditionalAmt + IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisType).Value) = 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value))
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
                    Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colRejectQty).Value)
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
                        gv1.Rows(ii).Cells(colRejectedAmount).Value = Math.Round(dblLandedRate * clsCommon.myCdbl(gv1.Rows(ii).Cells(colRejectQty).Value), 2)
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


        Calc_AddtionalCharge_Itemwise(Nothing)
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
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType, Optional ByVal rejqty As Double = 0)
        Try
            Dim ListSRN_No As New List(Of String)
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()
            LoadBlankGridTax()
            LoadBlankGridAC()
            LoadBlankGridACInsurance()
            cboItemType.Enabled = False
            txtBillToLocation.Enabled = False
            txtSubLocation.Enabled = False
            Dim obj As New clsPurchaseInvoiceHead()
            obj = clsPurchaseInvoiceHead.GetData(strCode, NavTyep, arrLoc)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PI_No) > 0) Then
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btnprintjvl.Enabled = True
                    repoComplete.IsVisible = True
                    repoBalQty.IsVisible = True
                    btncancel.Enabled = True
                    btncancel.Visible = True

                Else
                    btnprintjvl.Enabled = False
                    btncancel.Enabled = False
                End If
                btncancel.Visible = True

                chkJobWorkOutward.Checked = IIf(obj.isJobWorkOutward = 1, True, False)
                cmbDocType.SelectedValue = obj.Document_Type
                txtVendorInvoiceNo.Text = obj.Vendor_Invoice_No
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.PI_No
                txtDate.Value = obj.PI_Date
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                txtRefNo.Text = obj.Ref_No
                chkOnHold.Checked = obj.On_Hold
                txtDesc.Text = obj.Description
                txtTaxGroup.Value = obj.Tax_Group

                chkTDSProvision.Checked = obj.TDS_Provision
                txtDept.Value = obj.Dept
                lblDept.Text = obj.Dept_Desc

                txtComment.Text = obj.Comments
                txtShipToLocation.Value = obj.Ship_To_Location
                txtBillToLocation.Value = obj.Bill_To_Location
                txtSubLocation.Value = obj.Sublocation_Code
                txtRemarks.Text = obj.Remarks
                TxtRetention.Text = obj.Retention

                If clsCommon.myLen(obj.DateAndTime) > 0 Then
                    txtDataAndTimeSelection.Value = obj.DateAndTime
                    txtDataAndTimeSelection.Checked = True
                End If
                txtTapalNo.Text = clsCommon.myCstr(obj.TapalNo)
                If clsCommon.myCBool(obj.ITC_Elibible) = True Then
                    chkITCEligible.Checked = IIf(obj.ITC_Elibible = 1, True, False)
                    If clsCommon.myCdbl(obj.ITC_Type) = 1 Then
                        CboxITCType.SelectedIndex = 0
                    Else
                        CboxITCType.SelectedIndex = 1
                    End If

                    CboxITCCateogory.SelectedValue = obj.ITC_Type_Category
                    RadGroupBox4.Enabled = True
                End If

                txtport.Text = obj.Port
                txtImportEntryNo.Text = obj.Import_Entry_No
                If obj.Import_Entry_Date IsNot Nothing Then
                    dtImportEntryDate.Value = obj.Import_Entry_Date
                End If
                cboPOType.SelectedValue = obj.PI_Type

                If obj.Invdate IsNot Nothing Then
                    InvDate1.Value = obj.Invdate
                    InvDate1.Checked = True
                Else
                    InvDate1.Checked = False
                End If
                fndProject.Text = obj.PROJECT_ID
                lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Text + "'")


                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If

                If obj.objJVC IsNot Nothing AndAlso clsCommon.myLen(obj.objJVC.PJV_No) > 0 Then
                    lblPJVNo.Text = obj.objJVC.PJV_No
                End If

                If obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                    rbtnTaxCalAutomatic.IsChecked = True
                ElseIf obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                    rbtnTaxCalManual.IsChecked = True
                End If

                txtTermCode.Value = obj.Terms_Code
                'lblTermName.Text = obj.Terms_Description
                txtDueDate.Value = obj.Due_Date
                lblAmtWithDiscount.Text = clsCommon.myFormat(obj.Discount_Base)
                lblDiscountAmt.Text = clsCommon.myFormat(obj.Discount_Amt)
                lblAmtAfterDiscount.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
                lblTaxableAmount.Text = clsCommon.myFormat(obj.Total_Taxable_Amount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                lblTotRAmt.Text = clsCommon.myFormat(obj.PI_Total_Amt)
                lblRoundOff.Text = clsCommon.myFormat(obj.RoundOffAmt)
                lblDocAmount.Text = lblTotRAmt.Text

                lblBillToLocation.Text = obj.BillToLocationName
                lblShipToLocation.Text = clsLocation.GetName(obj.Ship_To_Location, Nothing)
                lblSubLocation.Text = obj.SubLocationName
                lblTaxGrpName.Text = obj.TaxGroupName
                lblTermName.Text = obj.TermsName

                txtReqNo.Value = obj.Against_SRN
                isAgainstTender = clsPurchaseOrderHead.AgainstTender(obj.Against_SRN, 2, Nothing)
                chkAgainstCForm.Checked = obj.Against_C_Form
                txtCarrier.Text = obj.Carrier
                txtGENo.Text = obj.GENo
                If obj.GEDate.HasValue Then
                    txtGEDate.Value = obj.GEDate
                    txtGEDate.Checked = True
                End If
                If ShowItemAllStructureWise = False Then
                    cboItemType.SelectedValue = obj.Item_Type
                Else
                    LoadItemType()
                End If

                cboItemType.Enabled = False
                chkExciseOnQty.Checked = obj.is_Excise_On_Qty
                chkAgainstForm.Checked = obj.Is_Against_Form
                If obj.Is_Against_Form Then
                    ddlForm.SelectedValue = obj.Against_Form
                End If
                txtGRNo.Text = obj.GRNo
                If clsCommon.myLen(obj.GRNo) > 0 Then
                    dtpGRDate.Value = obj.GR_Date
                End If
                txtVehicleNo.Value = obj.VehicleNo
                lblVehicle.Text = GetVehicleNo(obj.VehicleNo, Nothing)
                If clsCommon.myLen(obj.Against_SRN) > 0 Then
                    txtVehicleNo.Enabled = False
                    lblVehicle.ReadOnly = True
                End If
                txtTransporter.Value = obj.Transporter
                lblTransporterName.Text = obj.TransporterDesc
                lblVehicle.Text = obj.VehicleDesc
                txtLRNo.Text = obj.LRNo

                lblAcceptedAmt.Text = clsCommon.myFormat(obj.Total_Accepted_Amount)
                lblRejectedAmt.Text = clsCommon.myFormat(obj.Total_Rejected_Amount)
                lblShortageAmt.Text = clsCommon.myFormat(obj.Total_Shortage_Amount)
                lblLeakAmt.Text = clsCommon.myFormat(obj.Total_Leak_Amount)
                lblBurstAmt.Text = clsCommon.myFormat(obj.Total_Burst_Amount)
                chkShorategeIncludeInLandedCost.Checked = obj.Is_Shortage_Include_In_Landed_Cost

                lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(obj.ActualTCSBaseAmount)
                txttcstaxbaseamount.Value = clsCommon.myCdbl(obj.ChangedTCSBaseAmount)

                If clsCommon.myLen(obj.Transporter) > 0 Then
                    LoadTransporterData(obj.Transporter)
                    dtpGRDate.Value = obj.Transport_Date
                End If

                lblAddChargesForInsurance.Text = clsCommon.myFormat(obj.Total_Add_Charge_Insurance)
                lblAddChargesForInsurance1.Text = clsCommon.myFormat(obj.Total_Add_Charge_Insurance)
                lblTotalInsuranceAmt.Text = clsCommon.myFormat(obj.Total_Item_Insurance_Amt)

                objRemittance = clsRemittance.Convert(obj.objPIRemittance, dblPreviousTDSAmt)

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
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(obj.TAX1) & "' ")), "Y") = CompairStringResult.Equal Then
                        txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
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
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(obj.TAX2) & "' ")), "Y") = CompairStringResult.Equal Then
                        txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
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
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(obj.TAX3) & "' ")), "Y") = CompairStringResult.Equal Then
                        txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
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
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(obj.TAX4) & "' ")), "Y") = CompairStringResult.Equal Then
                        txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
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



                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsPurchaseInvoiceDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = objTr.Category
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = CInt(objTr.Emergency)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = objTr.Capex_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = objTr.Capex_SubCode

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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSRNQty).Value = objTr.OrgSRNQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = objTr.Balance_Qty
                        '-------added by usha
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colfreeQty).Value = objTr.Free_qty
                        '------ends here--------
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = IIf(iscalculationonrejqty And rejqty > 0, rejqty, objTr.PI_Qty)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNNo).Value = objTr.SRN_Id

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPOID).Value = objTr.PO_ID
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGRNID).Value = objTr.GRN_ID
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRNID).Value = objTr.MRN_ID

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        '' Anubhooti 27-Oct-2014
                        If clsCommon.myLen(obj.Against_SRN) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNUnitCost).Value = SRNItemRate(clsCommon.myCstr(objTr.SRN_Id), clsCommon.myCstr(objTr.Item_Code))
                            'If clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNUnitCost).Value) > 0 Then
                            '    'gv1.Columns(colRate).ReadOnly = True
                            '    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = True
                            'Else
                            '    'gv1.Columns(colRate).ReadOnly = False
                            '    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = False
                            'End If
                        End If
                        ''

                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Location
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.LocationName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisType).Value = objTr.Disc_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = objTr.Disc_Per

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountPer).Value = objTr.Header_Discount_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountAmt).Value = objTr.Header_Discount_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDetailDisAmt).Value = objTr.Detail_Discount_Amount

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPerUnit).Value = objTr.Disc_Per_Unit
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmtPerUnit).Value = objTr.Disc_Amt_Per_Unit

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

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.MRP
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableRate).Value = objTr.Assessable
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableAmount).Value = objTr.AssessableAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = objTr.Batch_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = objTr.Bin_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        If objTr.Expiry_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiry).Value = objTr.Expiry_Date
                        End If

                        If objTr.MFG_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colManufactureDate).Value = objTr.MFG_Date
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLeakQty).Value = objTr.Leak_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBurstQty).Value = objTr.Burst_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShortQty).Value = objTr.Short_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectQty).Value = objTr.Reject_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLandedRate).Value = objTr.Landed_Cost_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLandedAmt).Value = objTr.Landed_Cost_Amount

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitTotNonRecTax).Value = objTr.Total_NonRecTax_PerUnit
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitTotRecTax).Value = objTr.Total_RecTax_PerUnit
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitTotAddCost).Value = objTr.Total_AddtionalCost_PerUnit

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsMannualAmt).Value = objTr.Is_Mannual_Amt

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableAmount).Value = objTr.AssessableAmt
                        If clsCommon.myLen(objTr.SRN_Id) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsSRNDetail.GetBalanceSRNQty(objTr.SRN_Id, objTr.Item_Code, obj.PI_No, objTr.Unit_code, objTr.MRP, objTr.Assessable)
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
                        If objCommonVar.RCDFCFP AndAlso Not ListSRN_No.Contains(objTr.SRN_Id) Then
                            ListSRN_No.Add(objTr.SRN_Id)
                        End If
                    Next

                End If
                SetitemWiseTaxOnlySetting()
                gvAC.Rows.AddNew()

                If obj.Arr_ACInsurance IsNot Nothing AndAlso obj.Arr_ACInsurance.Count > 0 Then
                    For Each objtr As clsPIAdditionChargeInsurance In obj.Arr_ACInsurance
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceCode).Value = objtr.AC_Code
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceName).Value = objtr.AC_Name
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceAmount).Value = objtr.Amount
                        gvACInsurance.Rows.AddNew()
                    Next
                End If
                If objCommonVar.RCDFCFP Then
                    LoadAPInvoice()
                    If obj.Status = ERPTransactionStatus.Pending Then
                        LoadDeduction(ListSRN_No)
                    Else
                        qry = "select x.SRN_No as [SRNNo],x.Type,x.Item_Code as ItemCode,TSPL_ITEM_MASTER.Item_Desc as Item,x.Amount from (
select SRN_No,'Security Deduction' as Type,Item_Code,Ded_Amt as Amount from TSPL_SRN_DEDUCTION_SECURITY where SRN_No in (select SRN_ID from tspl_PI_Detail where PI_No='" + obj.PI_No + "')
union all
select SRN_No,'Quality Deduction' as Type,Item_Code,Ded_Amt as Amount from TSPL_SRN_DEDUCTION where SRN_No in (select SRN_ID from tspl_PI_Detail where PI_No='" + obj.PI_No + "')
union all
select SRN_No,'RM Late Penalty' as Type,Item_Code,Penalty as Amount from TSPL_SRN_TENDER where isnull(Penalty,0)>0 and  SRN_No in (select SRN_ID from tspl_PI_Detail where PI_No='" + obj.PI_No + "')
union all
select SRN_No,'RM Late Penalty [ Recalculate ]' as Type,Item_Code,Penalty as Amount from TSPL_SRN_TENDER_CALC where isnull(Penalty,0)>0 and  SRN_No in (select SRN_ID from tspl_PI_Detail where PI_No='" + obj.PI_No + "')
)x left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=x.Item_Code"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            gvDeduction.DataSource = dt
                            For ii As Integer = 0 To gvDeduction.Columns.Count - 1
                                gvDeduction.Columns(ii).ReadOnly = True
                                gvDeduction.Columns(ii).BestFit()
                            Next
                            gvDeduction.AllowAddNewRow = False
                        End If
                    End If
                    CalculateDeductionTotal(True)
                    CalculateTDS()

                    'Dim dblSecurityDed As Double = 0
                    'Dim dblQualityDed As Double = 0
                    'Dim dblRMLate As Double = 0

                    ''For Each gvrow As GridViewRowInfo In gv1.Rows
                    'For Each row As GridViewRowInfo In gvDeduction.Rows
                    '    ' Check if the row is not a new row and type column is not null
                    '    If row.Cells("type").Value IsNot Nothing Then
                    '        Dim type As String = clsCommon.myCstr(row.Cells("type").Value)

                    '        ' Depending on the type, accumulate the amount
                    '        Dim amount As Decimal = clsCommon.myCdbl(row.Cells("amount").Value)

                    '        Select Case type
                    '            Case "Security Deduction"
                    '                dblSecurityDed += amount
                    '            Case "Quality Deduction"
                    '                dblQualityDed += amount
                    '            Case "RM Late Penalty"
                    '                dblRMLate += amount
                    '                ' Add more cases if there are additional types
                    '        End Select
                    '    End If
                    'Next
                    'lblSecurityDeduction.Text = clsCommon.myFormat(dblSecurityDed)
                    'lblQualityDeduction.Text = clsCommon.myFormat(dblQualityDed)
                    'lblRMLate.Text = clsCommon.myFormat(dblRMLate)

                End If
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.PI_No)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.PI_No, MyBase.Form_ID, gv1)
                ''End of For Custom Fields

                '' MULTICURRENCY
                Me.txtCurrencyCode.Value = obj.CURRENCY_CODE
                Me.txtConversionRate.Text = obj.ConvRate
                If clsCommon.myLen(obj.ApplicableFrom) > 0 Then
                    Me.txtApplicableFrom.Text = clsCommon.myCstr(clsCommon.GetPrintDate(obj.ApplicableFrom, "dd/MMM/yyyy"))
                End If

                If clsCommon.myLen(txtReqNo.Value) > 0 Then
                    Me.txtCurrencyCode.Enabled = False
                    MakeColumnReadOnly(True)
                Else
                    Me.txtCurrencyCode.Enabled = True
                    MakeColumnReadOnly(False)
                End If
                '' end  MULTICURRENCY
                UcAttachment1.LoadData(obj.PI_No)

            End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPOID).Value)
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)

                Dim dblUnitCost As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)


                If UnitInceaseValue = True Then
                    Dim PurchaseRate As Decimal = clsDBFuncationality.getSingleValue("select item_cost from TSPL_PURCHASE_ORDER_DETAIL where item_code='" & strICode & "' and PurchaseOrder_No='" & strReqNo & "'")
                    If clsCommon.myCdbl(dblUnitCost) > clsCommon.myCdbl(PurchaseRate) Then
                        '=====================if document go for approval then no post button visible or if document contain related setting
                        btnPost.Visible = MyBase.isPostFlag
                        If Not clsApply_Approval.Visibility_PostButtonForApproval(MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value), clsCommon.myCdbl(lblTotRAmt.Text), 0, clsCommon.myCstr(txtDept.Value)) Then
                            btnPost.Visible = False
                            If UsLock1.Status = ERPTransactionStatus.Pending Then
                                UsLock1.Status = clsApply_Approval.ApprovalCondCheck_Doc(MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value), Nothing)
                                If UsLock1.Status = ERPTransactionStatus.Approved Then
                                    btnSave.Enabled = False
                                    btnPost.Enabled = False
                                    btnDelete.Enabled = False

                                    repoComplete.IsVisible = True
                                    repoBalQty.IsVisible = True
                                End If
                            End If
                        End If
                        '============================================
                        Continue For
                    End If
                End If
            Next
            Dim strPurchaseTaxInvoiceNo As String = clsDBFuncationality.getSingleValue(" select isnull (Purchase_Tax_Invoice,'') from TSPL_PI_head where PI_No = '" + txtDocNo.Value + "'  ")
            If clsCommon.myLen(strPurchaseTaxInvoiceNo) > 0 Then
                btnPurchaseTaxInvoice.Visible = True
            Else
                btnPurchaseTaxInvoice.Visible = False
            End If

            FillVendorDetails()

            If objCommonVar.RCDFCFP = True Then
                LoadRALData()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub LoadRALData()
        Try
            For ii As Integer = 0 To gv1.Rows.Count - 1
                'For Each grow As GridViewRowInfo In gv_MRN.Rows
                If clsCommon.myLen(gv1.Rows(ii).Cells(colGRNID).Value) > 0 Then
                    Dim strSql As String = "Select TSPL_MRN_Head.Vendor_Name As [Vendor Name],tspl_item_master.Item_Desc As [Item Name],TSPL_MRN_Head.VehicleNo
                                            ,TSPL_GRN_HEAD.GRN_No as [GRN No],convert(varchar, TSPL_GRN_HEAD.GRN_Date,103) as [GRN Date]
                                            ,TSPL_GRN_HEAD.Ref_No as [RAL No] ,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code as [Weighment No],convert(varchar,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as [Weighment Date]
                                            ,TSPL_QC_CHECK_HEAD.Document_Code as QCNo,TSPL_QC_CHECK_HEAD.Document_Date as QCDate,TSPL_GRN_HEAD.[Invoice/Challan_No] AS BillNo                                            
                                            From TSPL_MRN_Head
                                            Left Join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_MRN_Head.Against_GRN
                                            Left Join TSPL_GRN_HEAD on  TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No
                                            Left Join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
                                            left join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Gate_Entry_No=TSPL_MRN_Head.Against_GRN                                            
                                            Left outer join tspl_item_master on tspl_item_master.Item_Code=TSPL_PO_WEIGHTMENT_DETAIL.Item_Code
                                            where TSPL_GRN_HEAD.GRN_No ='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colGRNID).Value) + "'"
                    Dim dtDetail As DataTable = clsDBFuncationality.GetDataTable(strSql)
                    If dtDetail IsNot Nothing AndAlso dtDetail.Rows.Count > 0 Then
                        If clsCommon.myLen(dtDetail.Rows(0).Item("GRN Date")) > 0 Then
                            gv1.Rows(ii).Cells(colGRNDate).Value = clsCommon.GetPrintDate(dtDetail.Rows(0).Item("GRN Date"), "dd-MM-yyyy")
                        End If
                        gv1.Rows(ii).Cells(colWeighmentNo).Value = clsCommon.myCstr(dtDetail.Rows(0).Item("Weighment No"))
                        If clsCommon.myLen(dtDetail.Rows(0).Item("Weighment Date")) > 0 Then
                            gv1.Rows(ii).Cells(colWeighmentDate).Value = clsCommon.GetPrintDate(dtDetail.Rows(0).Item("Weighment Date"), "dd-MM-yyyy")
                        End If
                        gv1.Rows(ii).Cells(colQCNo).Value = clsCommon.myCstr(dtDetail.Rows(0).Item("QCNo"))
                        If clsCommon.myLen(dtDetail.Rows(0).Item("QCDate")) > 0 Then
                            gv1.Rows(ii).Cells(colQCDate).Value = clsCommon.GetPrintDate(dtDetail.Rows(0).Item("QCDate"), "dd-MM-yyyy")
                        End If
                        gv1.Rows(ii).Cells(colVehicleNo).Value = clsCommon.myCstr(dtDetail.Rows(0).Item("VehicleNo"))
                        gv1.Rows(ii).Cells(colBillNo).Value = clsCommon.myCstr(dtDetail.Rows(0).Item("BillNo"))
                        txtRefNo.Text = clsCommon.myCstr(dtDetail.Rows(0).Item("RAL No"))
                    End If

                End If
            Next
            'Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub
    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If rbtnTaxCalAutomatic.IsChecked AndAlso Not PurchaseModulePickFixTaxRate Then
                'Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                'Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FndVeRateTAXFND", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='P'", "", "", True))
                Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(txtBillToLocation.Value, txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), txtVendorNo.Value, "P")

                Dim intRowNo As Integer = gv2.CurrentRow.Index
                If gv1.RowCount > 0 AndAlso intRowNo >= 0 Then
                    Dim strII As String = clsCommon.myCstr(intRowNo + 1)
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        gv1.Rows(ii).Cells("COLTAXRATE" + strII).Value = dblNewRate
                    Next
                End If
                If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmountPurchase = True Then
                    txtTCSTaxRate.Value = dblNewRate
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
            ''changes by shivani[BM00000008206]
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                'SaveData(True) ''By balwinder becuase when saving data Transaction approval is inserted.if Transaction approval given by mgmt and save function calling then it will again remove permission.
                '' Anubhooti 12-Sep-2014 BM00000003735
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Purchase Invoice", txtDate.Value) = False Then
                    Exit Sub
                End If
                ''
                If (clsPurchaseInvoiceHead.PostData(MyBase.Form_ID, txtDocNo.Value, arrLoc)) Then
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
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(txtDocNo.Value, NavigatorType.Current)

                If clsSMSAtPost_Purchase.SMSATPOST_PUR() Then
                    SMSSENDONLY(True)
                End If

                If (common.clsCommon.MyMessageBoxShow("Do you  want to print", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                    print(txtDocNo.Value)
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
                If (clsPurchaseInvoiceHead.DeleteData(txtDocNo.Value)) Then
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
            Dim qst As String = "select count(*) from TSPL_PI_HEAD where PI_No='" + txtDocNo.Value + "'"

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
        '===================update by preeti gupta [Add column created by for Jakson Clinet]
        qry = "select PI_No as Code,convert(varchar,PI_Date,103) as Date,TSPL_PI_HEAD.Vendor_Code as [Vendor Code], TSPL_PI_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Vendor_Invoice_No as [Vendor Invoice No],Convert(Varchar(12),InvoiceDate,103) as [Invoice Date],PI_Total_Amt as Amount,case when TSPL_PI_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status] ,(select top 1 PJV_No from TSPL_PJV_HEAD where Invoice_No=PI_No) as [PJV No],Against_SRN as [SRN No],case when TSPL_PI_HEAD.Document_Type='PI' then 'Purchase Invoice' else 'Merchant Trade' end as [Document Type],TSPL_USER_MASTER.User_Name as [User Name],TSPL_PURCHASE_ORDER_HEAD.RefTendorNo as TenderNo,VehicleNo,Against_GRN  from TSPL_PI_HEAD"
        '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
        qry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_PI_HEAD.Vendor_Code left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = TSPL_PI_HEAD.Against_PO    "
        qry += "  left join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code =TSPL_PI_HEAD.Created_By "
        Dim whrClas As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            whrClas = " TSPL_PI_HEAD.Bill_To_Location in (" + arrLoc + ")"
        End If

        LoadData(clsCommon.ShowSelectForm("PICoerFND", qry, "Code", whrClas, txtDocNo.Value, "PI_Date desc", isButtonClicked, "PI_Date"), NavigatorType.Current)
    End Sub
    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        ''If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colSRNNo).Value) <= 0 Then
        ''    isCellValueChangedOpen = True
        ''    If gv1.CurrentColumn Is gv1.Columns(colICode) Then
        ''        gv1.CurrentColumn = gv1.Columns(colIName)
        ''        OpenICodeList(True)
        ''        gv1.CurrentColumn = gv1.Columns(colICode)
        ''    End If
        ''    setGridFocus()
        ''    isCellValueChangedOpen = False
        ''Else
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.L AndAlso MyBase.isCancel_Flag_After_Posting AndAlso btncancel.Enabled Then
            CancelPI()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                                     "TSPL_PI_HEAD " + Environment.NewLine +
                                                     "TSPL_PI_DETAIL " + Environment.NewLine +
                                                     "TSPL_PI_REMITTANCE " + Environment.NewLine +
                                                     "TSPL_PJV_HEAD " + Environment.NewLine +
                                                     "TSPL_PJV_Detail " + Environment.NewLine +
                                                     "TSPL_TRANSACTION_APPROVAL " + Environment.NewLine +
                                                     "Press Alt+P for Post Trasnaction " + Environment.NewLine +
                                                     "TSPL_SRN_DETAIL(Update Balance Qty) " + Environment.NewLine +
                                                     "TSPL_PI_REMITTANCE " + Environment.NewLine +
                                                     "TSPL_VENDOR_INVOICE_HEAD " + Environment.NewLine +
                                                     "TSPL_VENDOR_INVOICE_DETAIL " + Environment.NewLine +
                                                     "TSPL_AP_Invoice_Asset_EMI_Details " + Environment.NewLine +
                                                     "TSPL_AP_Invoice_Advance_Interest " + Environment.NewLine +
                                                     "TSPL_PROVISION_ENTRY_KNOCKOFF " + Environment.NewLine +
                                                     "TSPL_JOURNAL_MASTER " + Environment.NewLine +
                                                     "TSPL_JOURNAL_DETAILS " + Environment.NewLine +
                                                     "TSPL_PR_HEAD(Purchase return in case of short Qty) " + Environment.NewLine +
                                                     "TSPL_PR_DETAIL " + Environment.NewLine +
                                                     "TSPL_ADJUSTMENT_HEADER(Adjustment Entry) " + Environment.NewLine +
                                                     "TSPL_ADJUSTMENT_DETAIL " + Environment.NewLine +
                                                     "TSPL_INVENTORY_MOVEMENT_new " + Environment.NewLine +
                                                     "TSPL_ACQUISITION_DETAIL(Vendor Service charge against asset for assembled asset) " + Environment.NewLine +
                                                     "TSPL_SALE_INVOICE_HEAD(Update Balance Amount) ")
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                    RadButton1.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER"
        txtTermCode.Value = clsCommon.ShowSelectForm("POTermCodeNEW", qry, "Code", "", txtTermCode.Value, "Code", isButtonClicked)
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
        'txtTaxGroup.Value = clsCommon.ShowSelectForm("POFNDID", qry, "Code", "Tax_Group_Type='P'", txtTaxGroup.Value, "Code", isButtonClicked)

        If clsCommon.CompairString(cmbDocType.SelectedValue, "PI") = CompairStringResult.Equal Then
            txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(txtBillToLocation.Value, txtVendorNo.Value, "P", txtTaxGroup.Value, isButtonClicked)
            SetTaxDetails()
        Else
            clsCommon.MyMessageBoxShow(Me, "You cannot select Tax Group on Merchant Trade", Me.Text)
        End If


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
            For Each dr As DataRow In dt.Rows
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dr("Tax_Code"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsCommon.myCstr(dr("Tax_Code_Desc"))
                If rbtnTaxCalAutomatic.IsChecked Then
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_VENDOR_MASTER where Vendor_Code ='" & txtVendorNo.Value & "'")), "0") = CompairStringResult.Equal Then
                            If AmountToCheckVendorOutstandingForTCSTax > 0 Then
                                Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsVendorMaster.GetVendorOutstandingForTCSTaxApplicableOnFY(txtVendorNo.Value, txtDate.Value))
                                If dblOutstandingAmount < AmountToCheckVendorOutstandingForTCSTax Then
                                    dblOutstandingAmount = dblOutstandingAmount + clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text))
                                    If dblOutstandingAmount > AmountToCheckVendorOutstandingForTCSTax Then
                                        If clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text)) > 0 Then
                                            txttcstaxbaseamount.Value = Math.Round(clsCommon.myCdbl(dblOutstandingAmount - AmountToCheckVendorOutstandingForTCSTax), 2)
                                        End If
                                    End If
                                End If
                                If dblOutstandingAmount > AmountToCheckVendorOutstandingForTCSTax Then
                                    Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(PAN,'') as PAN from TSPL_VENDOR_MASTER where Vendor_Code='" & txtVendorNo.Value & "'"))
                                    If clsCommon.myLen(panno) > 0 Then
                                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforVendorWithPanNo, clsFixedParameterCode.TCSRateforVendorWithPanNo, Nothing))
                                    Else
                                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforVendorWithoutPanNo, clsFixedParameterCode.TCSRateforVendorWithoutPanNo, Nothing))
                                    End If
                                Else
                                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = 0
                                End If
                            Else
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = dr("TaxRate")
                            End If
                        Else
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = 0
                        End If

                        txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
                    Else
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dr("TaxRate"))
                    End If
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
        Try
            Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", txtVendorNo.Value, txtBillToLocation.Value)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                If isForCurrentRow Then
                    BlankTaxDetails(gv1.CurrentRow.Index, isChangeRate)
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colICode)) > 0 Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                            gv1.CurrentRow.Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            If rbtnTaxCalAutomatic.IsChecked Then
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
                                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                            End If
                            ii = ii + 1
                        Next
                    End If
                Else
                    For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                        BlankTaxDetails(intRowNo, isChangeRate)
                        If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colICode)) > 0 OrElse (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code IN (select Tax_Code  from TSPL_TAX_GROUP_DETAILS WHERE TAX_GROUP_CODE='" & txtTaxGroup.Value & "') AND Is_TCS ='Y'")) > 0) Then
                            Dim ii As Integer = 1
                            For Each dr As DataRow In dt.Rows
                                Dim strII As String = clsCommon.myCstr(ii)
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                                If rbtnTaxCalAutomatic.IsChecked Then
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

                                    ''tcs tax rate
                                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_vendor_master where Vendor_Code ='" & txtVendorNo.Value & "'")), "0") = CompairStringResult.Equal Then
                                            If AmountToCheckVendorOutstandingForTCSTax > 0 Then
                                                Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsVendorMaster.GetVendorOutstandingForTCSTaxApplicableOnFY(txtVendorNo.Value, txtDate.Value))
                                                If dblOutstandingAmount < AmountToCheckVendorOutstandingForTCSTax Then
                                                    dblOutstandingAmount = dblOutstandingAmount + clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text))
                                                    If dblOutstandingAmount > AmountToCheckVendorOutstandingForTCSTax Then
                                                        If clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text)) > 0 Then
                                                            txttcstaxbaseamount.Value = Math.Round(clsCommon.myCdbl(dblOutstandingAmount - AmountToCheckVendorOutstandingForTCSTax), 2)
                                                        End If
                                                    End If
                                                End If
                                                If dblOutstandingAmount > AmountToCheckVendorOutstandingForTCSTax Then
                                                    Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(PAN,'')  as PanNoAdhar from TSPL_vendor_master where Vendor_Code='" & txtVendorNo.Value & "'"))
                                                    If clsCommon.myLen(panno) > 0 Then
                                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforVendorWithPanNo, clsFixedParameterCode.TCSRateforVendorWithPanNo, Nothing))
                                                    Else
                                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforVendorWithoutPanNo, clsFixedParameterCode.TCSRateforVendorWithoutPanNo, Nothing))
                                                    End If
                                                Else
                                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = 0
                                                End If
                                            Else
                                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = dr("TaxRate")
                                            End If
                                        Else
                                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = 0
                                        End If
                                    End If

                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                                End If
                                ii = ii + 1
                            Next
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

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
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Next
        End If
    End Sub
    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Add1 as [Address 1],Add2 as [Address 2],Add3 as [Address 3],City_Code_Desc as City,State,Country from TSPL_VENDOR_MASTER"
        txtVendorNo.Value = clsCommon.ShowSelectForm("POVeFNDID", qry, "Code", " TSPL_VENDOR_MASTER.Status='N'", txtVendorNo.Value, "Code", isButtonClicked)
        ''lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'"))
        qry = "select  Vendor_Code,Vendor_Name,Terms_Code,Terms_Code_Desc ,Vendor_Account ,Tax_Group,Tax_Group_Desc from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtVendorNo.Value + "'and TSPL_VENDOR_MASTER.Form_Type<>'VSP'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Code_Desc"))
            txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            SetMultiCurrencyVisibility()
        Else
            lblVendorName.Text = ""
            txtTermCode.Value = ""
            lblTermName.Text = ""
            txtTaxGroup.Value = ""
            lblTaxGrpName.Text = ""

            Me.txtCurrencyCode.Value = ""
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""
        End If
        SetTax()

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
        Dim WhrCls As String = " Location_Type='Physical'  "
        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        'End If
        If clsCommon.myLen(arrLoc) > 0 Then
            WhrCls += " and tspl_location_master.location_code in (" + arrLoc + ")"
        End If

        txtBillToLocation.Value = clsCommon.ShowSelectForm("VendRFNDster", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))

        SetTax()

    End Sub
    Private Sub txtShipToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipToLocation._MYValidating
        'Dim qry As String = "select Ship_To_Code as Code,Ship_To_Desc as Description from TSPL_SHIP_TO_LOCATION"
        'txtShipToLocation.Value = clsCommon.ShowSelectForm("POShiPFND", qry, "Code", "", txtShipToLocation.Value, "Code", isButtonClicked)
        'lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ship_To_Desc from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipToLocation.Value + "'"))

        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtShipToLocation.Value = clsCommon.ShowSelectForm("BILLTOLOCPO", qry, "Code", WhrCls, txtShipToLocation.Value, "Code", isButtonClicked)
        lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtShipToLocation.Value + "'"))

    End Sub
    Sub SelectSRNItems()
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
            If clsCommon.myLen(clsCommon.myCstr(txtSubLocation.Value)) <= 0 AndAlso chkJobWorkOutward.Checked = False Then
                clsCommon.MyMessageBoxShow(Me, "Please select Sub Location", Me.Text)
                Exit Sub
            End If
        End If
        Dim ListSRN_No As New List(Of String)
        Dim objMRNHead As clsSRNHead = Nothing
        isInsideLoadData = True
        Dim frm As New frmPendingSRN()
        If objCommonVar.RCDFCFP = True Then
            Dim objItemMaster As clsItemMaster
            objItemMaster = clsItemMaster.FinderForItem("", "", True)
            If objItemMaster IsNot Nothing AndAlso clsCommon.myLen(objItemMaster.Item_Code) > 0 Then
                frm.ItemForDocumentFilter = objItemMaster.Item_Code
            Else
                frm.ItemForDocumentFilter = Nothing
            End If
        End If
        frm.VendorCode = txtVendorNo.Value
        frm.strCurrCode = txtDocNo.Value
        If clsCommon.CompairString(cmbDocType.SelectedValue, "MT") = CompairStringResult.Equal Then
            frm.IsMerchantTrade = "MT"
        Else
            frm.IsMerchantTrade = "PI"
        End If
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
            If clsCommon.myLen(clsCommon.myCstr(txtSubLocation.Value)) >= 0 AndAlso chkJobWorkOutward.Checked = False Then
                frm.SubLocationCode = txtSubLocation.Value
            End If
        End If
        frm.ShowDialog()
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            If clsCommon.myLen(frm.ArrReturn(0).SRN_No) > 0 Then
                objMRNHead = clsSRNHead.GetData(frm.ArrReturn(0).SRN_No, NavigatorType.Current)
                If objMRNHead IsNot Nothing AndAlso clsCommon.myLen(objMRNHead.SRN_No) > 0 Then
                    IsAbatementPO = objMRNHead.IsAbatementPO
                    isAgainstTender = clsPurchaseOrderHead.AgainstTender(objMRNHead.SRN_No, 2, Nothing)
                    LoadBlankGrid()


                    chkJobWorkOutward.Checked = IIf(objMRNHead.isJobWorkOutward = 1, True, False)
                    If clsCommon.myLen(txtCarrier.Text) <= 0 Then
                        txtCarrier.Text = objMRNHead.Carrier
                    End If
                    If clsCommon.myLen(txtVehicleNo.Text) <= 0 Then
                        'txtVehicleNo.Text = objMRNHead.VehicleNo
                        txtVehicleNo.Enabled = False
                        lblVehicle.ReadOnly = True
                        txtVehicleNo.Value = objMRNHead.VehicleNo
                        lblVehicle.Text = objMRNHead.VehicleNo
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
                    If clsCommon.myLen(txtRefNo.Text) <= 0 Then
                        txtRefNo.Text = objMRNHead.Ref_No
                    End If
                    If clsCommon.myLen(txtDesc.Text) <= 0 Then
                        txtDesc.Text = objMRNHead.Description
                    End If
                    If clsCommon.myLen(objMRNHead.Inv_No) > 0 Then
                        txtVendorInvoiceNo.Text = objMRNHead.Inv_No
                        txtVendorInvoiceNo.ReadOnly = False
                    Else
                        txtVendorInvoiceNo.Text = ""
                        txtVendorInvoiceNo.ReadOnly = False
                    End If

                    If clsCommon.myLen(txtDept.Value) <= 0 AndAlso clsCommon.myLen(objMRNHead.Dept) > 0 Then
                        txtDept.Value = objMRNHead.Dept
                        lblDept.Text = objMRNHead.Dept_Desc
                    End If
                    'Dim InVdate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Date  from TSPL_SRN_HEAD where SRN_No='" + frm.ArrReturn(0).SRN_No + "' And Remarks='" + txtVendorInvoiceNo.Text + "'"))
                    If clsCommon.myLen(objMRNHead.Inv_Date) > 0 Then
                        InvDate1.Value = objMRNHead.Inv_Date
                        InvDate1.Checked = True
                    Else
                        InvDate1.Checked = False
                    End If
                    'If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                    txtBillToLocation.Value = objMRNHead.Bill_To_Location
                    lblBillToLocation.Text = objMRNHead.BillToLocationName
                    txtSubLocation.Value = objMRNHead.Sublocation_Code
                    txtSubLocation.Enabled = False
                    lblSubLocation.Text = objMRNHead.SubLocationName
                    TxtRetention.Text = objMRNHead.Retention
                    'End If

                    txtShipToLocation.Value = objMRNHead.Ship_To_Location
                    lblShipToLocation.Text = clsLocation.GetName(objMRNHead.Ship_To_Location, Nothing)
                    'If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                    txtVendorNo.Value = frm.VendorCode
                    lblVendorName.Text = frm.VendorName
                    'End If
                    cboPOType.SelectedValue = objMRNHead.PurchaseOrder_Type

                    If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
                        cboItemType.SelectedValue = objMRNHead.Item_Type
                    End If
                    If (clsCommon.myLen(txtTermCode.Value) <= 0) Then
                        txtTermCode.Value = objMRNHead.Terms_Code
                        lblTermName.Text = objMRNHead.TermsName
                        txtDueDate.Value = objMRNHead.Due_Date
                    End If
                    If objMRNHead.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                        rbtnTaxCalAutomatic.IsChecked = True
                        chkExciseOnQty.Checked = objMRNHead.is_Excise_On_Qty
                    ElseIf objMRNHead.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                        rbtnTaxCalManual.IsChecked = True
                    End If
                    If (clsCommon.myLen(lblProject.Text) <= 0) Then
                        fndProject.Text = objMRNHead.PROJECT_ID
                        lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Text + "'")
                    End If
                    '' multicurrency
                    Me.txtCurrencyCode.Value = objMRNHead.CURRENCY_CODE
                    txtConversionRate.Text = objMRNHead.ConvRate
                    Me.txtCurrencyCode.Enabled = False
                    If objMRNHead.ApplicableFrom IsNot Nothing Then
                        Me.txtApplicableFrom.Text = objMRNHead.ApplicableFrom
                    End If

                    LoadBlankGridACInsurance()
                    If objMRNHead.Arr_ACInsurance IsNot Nothing AndAlso objMRNHead.Arr_ACInsurance.Count > 0 Then
                        For Each objtr As clsSRNAdditionChargeInsurance In objMRNHead.Arr_ACInsurance
                            gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceCode).Value = objtr.AC_Code
                            gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceName).Value = objtr.AC_Name
                            gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceAmount).Value = objtr.Amount
                            gvACInsurance.Rows.AddNew()
                        Next
                    End If

                    If gvAC.Rows.Count < 1 Then
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
                    End If
                    If gvAC.Rows.Count < 1 Then
                        gvAC.Rows.AddNew()
                    End If
                End If
            End If

            For Each obj As clsSRNDetail In frm.ArrReturn
                If IsValidItem(obj) Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                    If ShowCapexCodeandSubCode Then
                        MakeColumnReadOnly(True)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = obj.Category
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = CInt(obj.Emergency)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = obj.Capex_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = obj.Capex_SubCode
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNNo).Value = obj.SRN_No

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPOID).Value = obj.PO_ID
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGRNID).Value = obj.GRN_ID
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRNID).Value = obj.MRN_Id


                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = obj.Location
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = obj.LocationName
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSRNQty).Value = obj.SRN_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = obj.Taxable_Amount_Per
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colfreeQty).Value = obj.Freeqty
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
                    '' Anubhooti 21-Oct-2014 BM00000004222
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNUnitCost).Value = obj.Item_Cost
                    'If clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNUnitCost).Value) > 0 Then
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = True
                    'Else
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = False
                    'End If
                    ''
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

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLeakQty).Value = obj.Leak_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBurstQty).Value = obj.Burst_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colShortQty).Value = obj.Short_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectQty).Value = obj.Rejected_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisType).Value = obj.Disc_Type
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = obj.Disc_Per

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = frmPendingSRN.Load_discount_for_PI(obj.SRN_No, obj.Item_Code)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPerUnit).Value = frmPendingSRN.Load_discount_per_unit_for_PI(obj.SRN_No, obj.Item_Code)



                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableRate).Value = obj.Assessable
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = obj.Batch_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = obj.Bin_No
                    If obj.MFG_Date.HasValue Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colManufactureDate).Value = obj.MFG_Date
                    End If
                    If obj.Expiry_Date.HasValue Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiry).Value = obj.Expiry_Date
                    End If

                    If obj.Is_Mannual_Amt = 1 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsMannualAmt).Value = obj.Is_Mannual_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = obj.Amount
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountPer).Value = obj.Header_Discount_Per
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementRate).Value = obj.AbatementRate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssesableMRP).Value = obj.AssessableMRP
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssesableMRP).Value = obj.TotalAssessableMRP

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAgainstItemWiseTaxCode).Value = obj.Against_Item_Wise_Tax_Rate


                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceApplyOn).Value = obj.Item_Insurance_Apply_On
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsurancePer).Value = obj.Item_Insurance_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceAmt).Value = obj.Item_Insurance_Amt
                    If objCommonVar.RCDFCFP AndAlso Not ListSRN_No.Contains(obj.SRN_No) Then
                        ListSRN_No.Add(obj.SRN_No)
                    End If
                End If
            Next
        End If
        ''For Filling Additional Charges

        If objMRNHead IsNot Nothing Then
            For Each obj As clsSRNDetail In objMRNHead.Arr
                ''If IsValidItem(obj) Then
                If clsCommon.CompairString(obj.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNNo).Value = obj.SRN_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeMisc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsInsurance).Value = clsAdditionalCharge.GetIsInsurance(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = False
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = obj.Amount
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
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = obj.Taxable_Amount_Per
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

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisType).Value = obj.Disc_Type
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = obj.Disc_Per
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementRate).Value = obj.AbatementRate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssesableMRP).Value = obj.AssessableMRP
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssesableMRP).Value = obj.TotalAssessableMRP
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountPer).Value = obj.Header_Discount_Per
                    If ShowCapexCodeandSubCode Then
                        MakeColumnReadOnly(True)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = obj.Category
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = obj.Emergency
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = obj.Capex_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = obj.Capex_SubCode
                    End If
                    If objCommonVar.RCDFCFP AndAlso Not ListSRN_No.Contains(obj.SRN_No) Then
                        ListSRN_No.Add(obj.SRN_No)
                    End If
                End If
            Next
        End If

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
        CalculateInsuranceTotal(True)

        isInsideLoadData = False
        UpdateAllTotals()
        RefreshReqNo()
        SetVendorTDSDetails()
        ActualTCSTaxBaseAmt()

        If objCommonVar.RCDFCFP Then
            LoadDeduction(ListSRN_No)
            CalculateDeductionTotal(True)
            CalculateTDS()
        End If

        'objRemittance = clsRemittance.Convert(obj.objPIRemittance, dblPreviousTDSAmt)
        ' CalculateDeductionTotal(True)
    End Sub

    Private Sub LoadDeduction(ByVal ListSRN_No As List(Of String))
        Try
            qry = "select x.SRN_No as [SRNNo],x.Type,x.Item_Code as ItemCode,TSPL_ITEM_MASTER.Item_Desc as Item,x.Amount from (
select SRN_No,'Security Deduction' as Type,Item_Code,Ded_Amt as Amount from TSPL_SRN_DEDUCTION_SECURITY where SRN_No in (" + clsCommon.GetMulcallString(ListSRN_No) + ")
union all
select SRN_No,'Quality Deduction' as Type,Item_Code,Ded_Amt as Amount from TSPL_SRN_DEDUCTION where SRN_No in (" + clsCommon.GetMulcallString(ListSRN_No) + ")
union all
select SRN_No,'RM Late Penalty' as Type,Item_Code,Penalty as Amount from TSPL_SRN_TENDER_CALC where isnull(Penalty,0)>0 and  SRN_No in (" + clsCommon.GetMulcallString(ListSRN_No) + ")
)x left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=x.Item_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvDeduction.DataSource = dt
                For ii As Integer = 0 To gvDeduction.Columns.Count - 1
                    gvDeduction.Columns(ii).ReadOnly = True
                    gvDeduction.Columns(ii).BestFit()
                Next
                gvDeduction.AllowAddNewRow = False
            End If
            CalculateDeductionTotal(True)
            CalculateTDS()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadAPInvoice()
        Try
            qry = "select Document_No as APInvoiceNo,convert(varchar,Posting_Date,103) as APInvoiceDate,Document_Type as Type,Description,Document_Total as Amount from (
select Document_No,Posting_Date,Document_Type,Description,Document_Total
from TSPL_VENDOR_INVOICE_HEAD where RefDocType in('QC-DED','SEC-DED','SCH-PNT','RE-TEN') and RefDocNo='" + txtDocNo.Value + "' 
union all
select Document_No,Posting_Date,Document_Type,Description,Document_Total
from TSPL_VENDOR_INVOICE_HEAD where RefDocType in('REV-SPT') and RefDocNo in (select Document_No from TSPL_VENDOR_INVOICE_HEAD where RefDocType in('SCH-PNT') and RefDocNo='" + txtDocNo.Value + "' ) 
)x order by Posting_Date"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvAPInvoice.DataSource = dt
                For ii As Integer = 0 To gvAPInvoice.Columns.Count - 1
                    gvAPInvoice.Columns(ii).ReadOnly = True
                    gvAPInvoice.Columns(ii).BestFit()
                Next
                gvAPInvoice.AllowAddNewRow = False
                gvAPInvoice.BestFitColumns()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function IsValidItem(ByVal obj As clsSRNDetail)
        If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
            txtTaxGroup.Value = obj.SRNTax_Group
            SetTaxDetails()
        End If
        If Not clsCommon.CompairString(txtTaxGroup.Value, obj.SRNTax_Group) = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Item : " + obj.Item_Desc + " not Added Current Tax Group : " + txtTaxGroup.Value + " SRN No: " + obj.SRN_No + "  contain Tax Group :" + obj.SRNTax_Group + Environment.NewLine)
            Return False
        End If
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strSRNCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSRNNo).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
            ''Dim dblAssessable As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAssessableRate).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
            Dim strPONo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPOID).Value)
            Dim strGRNNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colGRNID).Value)
            Dim strMRNNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colMRNID).Value)
            If clsCommon.myLen(strSRNCode) > 0 AndAlso clsCommon.CompairString(strPONo, obj.PO_ID) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strSRNCode, obj.SRN_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strGRNNo, obj.GRN_ID) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strMRNNo, obj.MRN_Id) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, obj.Unit_code) = CompairStringResult.Equal AndAlso dblMRP = obj.MRP And dblRate = obj.Item_Cost Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "SRN No : " + obj.SRN_No + "  Item : " + obj.Item_Desc + Environment.NewLine + ""
                If dblMRP > 0 Then
                    strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
                End If
                ''If dblAssessable > 0 Then
                ''    strMsg = strMsg + Environment.NewLine + "Assessable : " + clsCommon.myCstr(dblAssessable)
                ''End If
                common.clsCommon.MyMessageBoxShow(strMsg)
                Return False
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
                    If common.clsCommon.MyMessageBoxShow("Do you want to complete the item " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value), Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        If clsPurchaseInvoiceDetail.CompletePI(txtDocNo.Value, strICode, intSNo) Then
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
    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        ''Try
        ''    If e.Column.Index >= 0 Then
        ''        If (e.Column Is gv1.Columns(colExpiry)) OrElse (e.Column Is gv1.Columns(colManufactureDate)) Then
        ''            gv1.Columns(colExpiry).FormatString = "{0:d}"
        ''        ElseIf e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colMRP) Then
        ''            If clsCommon.myLen(gv1.CurrentRow.Cells(colSRNNo).Value) > 0 Then
        ''                gv1.CurrentRow.Cells(colICode).ReadOnly = True
        ''                gv1.CurrentRow.Cells(colMRP).ReadOnly = True
        ''                ''gv1.CurrentRow.Cells(colAssessableRate).ReadOnly = True
        ''            Else
        ''                gv1.CurrentRow.Cells(colICode).ReadOnly = False
        ''                gv1.CurrentRow.Cells(colMRP).ReadOnly = False
        ''                ''gv1.CurrentRow.Cells(colAssessableRate).ReadOnly = False
        ''            End If
        ''        End If
        ''        Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
        ''        cell.GradientStyle = GradientStyles.Solid
        ''        cell.BackColor = Color.FromArgb(243, 181, 51)
        ''    End If
        ''Catch ex As Exception
        ''    common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        ''End Try

        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv1.Columns(colExpiry)) OrElse (e.Column Is gv1.Columns(colManufactureDate)) Then
                    gv1.Columns(colExpiry).FormatString = "{0:d}"
                ElseIf e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colMRP) Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colSRNNo).Value) > 0 Then
                        gv1.CurrentRow.Cells(colICode).ReadOnly = True
                        gv1.CurrentRow.Cells(colMRP).ReadOnly = True
                        ''gv1.CurrentRow.Cells(colAssessableRate).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colICode).ReadOnly = False
                        gv1.CurrentRow.Cells(colMRP).ReadOnly = False
                        ''gv1.CurrentRow.Cells(colAssessableRate).ReadOnly = False
                    End If
                ElseIf e.Column Is gv1.Columns(colfreeQty) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colfreeQty).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colfreeQty).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colQty) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colQty).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colQty).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colRate) Then

                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colRate).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colRate).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colAmt) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsInsurance).Value) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colSRNNo).Value) <= 0 Then
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
                    ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colIsInsurance).Value) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colSRNNo).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colInsurancePer).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colInsurancePer).ReadOnly = True
                    End If
                End If
                Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                cell.GradientStyle = GradientStyles.Solid
                cell.BackColor = Color.FromArgb(243, 181, 51)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub
    Private Sub btnViewTDS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewTDSDetails.Click
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
            'TDSAmt = frm.TDSAmt
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            'TDSAmt = objRemittance.Actual_Total_TDS
        End If

    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
        RefreshReqNo()
    End Sub
    Private Sub txtReqNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReqNo._MYValidating
        SelectSRNItems()
        FillVendorDetails()
        If objCommonVar.RCDFCFP = True Then
            LoadRALData()
        End If
    End Sub
    Sub RefreshReqNo()
        txtReqNo.Value = ""
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSRNNo).Value)
                If clsCommon.myLen(strReqNo) > 0 Then
                    txtReqNo.Value = clsCommon.myCstr(strReqNo)
                    isAgainstTender = clsPurchaseOrderHead.AgainstTender(strReqNo, 2, Nothing)
                    Exit Sub
                End If
            Next
        End If
    End Sub
    Private Sub btnprintjvl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprintjvl.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
        Else
            print(txtDocNo.Value)
        End If


        ''       Try
        ''           '           Dim qry As String = "SELECT TSPL_PJV_HEAD.Vendor_Code, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2, " & _
        ''           '                     "TSPL_COMPANY_MASTER.Add3, TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.CST_LST, " & _
        ''           '                     "TSPL_VENDOR_MASTER.Vendor_Name, TSPL_VENDOR_MASTER.Add1 AS Expr1, TSPL_VENDOR_MASTER.Add2 AS Expr2, " & _
        ''           '                     "TSPL_VENDOR_MASTER.Add3 AS Expr3, TSPL_VENDOR_MASTER.City_Code_Desc, TSPL_PJV_HEAD.PJV_No, TSPL_PJV_HEAD.PJV_Date, " & _
        ''           '                     "TSPL_PJV_HEAD.Vendor_Name AS Expr4, TSPL_PJV_HEAD.PO_No, TSPL_PJV_HEAD.PO_Date, TSPL_PJV_HEAD.SRN_No, " & _
        ''           '           "TSPL_PJV_HEAD.SRN_Date, TSPL_PJV_HEAD.Invoice_No, TSPL_PJV_HEAD.Invoice_Date, TSPL_PJV_HEAD.Status, TSPL_PJV_HEAD.Posting_Date, " & _
        ''           '           "TSPL_PJV_HEAD.PJV_Amount, TSPL_PJV_HEAD.PJV_TDS_Amount, TSPL_PJV_HEAD.PJV_Net_Amount, TSPL_PJV_HEAD.Narration, " & _
        ''           '                    " TSPL_PJV_HEAD.Due_Date, TSPL_PJV_Detail.PJV_No AS Expr5, TSPL_PJV_Detail.Line_No, TSPL_PJV_Detail.GL_Account_Code, " & _
        ''           '                     "TSPL_PJV_Detail.GL_Account_Desc, TSPL_PJV_Detail.PJV_Amount AS Expr6,TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, TSPL_PJV_HEAD.Created_By " & _
        ''           '" FROM         TSPL_PJV_HEAD INNER JOIN " & _
        ''           '                     "TSPL_PJV_Detail ON TSPL_PJV_HEAD.PJV_No = TSPL_PJV_Detail.pjv_No INNER JOIN " & _
        ''           '                     "TSPL_VENDOR_MASTER ON TSPL_PJV_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code LEFT OUTER JOIN " & _
        ''           '                     "TSPL_COMPANY_MASTER ON TSPL_PJV_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code   where TSPL_PJV_HEAD.Invoice_No='" + txtDocNo.Value + "'"


        ''           Dim qry As String = "  SELECT     TSPL_PJV_HEAD.PJV_No, TSPL_PJV_HEAD.PJV_Date, TSPL_PJV_HEAD.Vendor_Code, TSPL_PJV_HEAD.Vendor_Name, TSPL_PJV_HEAD.PO_No, " & _
        ''                    " TSPL_PJV_HEAD.PO_Date, TSPL_PJV_HEAD.SRN_No, TSPL_PJV_HEAD.SRN_Date, TSPL_PJV_HEAD.Invoice_No, TSPL_PJV_HEAD.Invoice_Date, " & _
        ''                   "  case when TSPL_PJV_HEAD.Status=0 then 'Pending' else 'Approved'end as status, TSPL_PJV_HEAD.Posting_Date, TSPL_PJV_HEAD.PJV_Amount, TSPL_PJV_HEAD.PJV_TDS_Amount, " & _
        ''                    " TSPL_PJV_HEAD.PJV_Net_Amount, TSPL_PJV_HEAD.Narration, TSPL_PJV_HEAD.Created_By, TSPL_PJV_Detail.Line_No, " & _
        ''                     " TSPL_PJV_Detail.GL_Account_Code, TSPL_PJV_Detail.GL_Account_Desc, TSPL_PJV_Detail.PJV_Amount AS Expr1,case when TSPL_PJV_Detail.PJV_Amount<0 then -1 * TSPL_PJV_Detail.PJV_Amount else 0 end as Credit,case when TSPL_PJV_Detail.PJV_Amount>=0 then TSPL_PJV_Detail.PJV_Amount else 0 end as Debit , " & _
        ''                     " TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2, TSPL_COMPANY_MASTER.Add3, " & _
        ''                   "  TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, " & _
        ''                    " TSPL_VENDOR_MASTER.Vendor_Name AS Expr2, TSPL_VENDOR_MASTER.Add1 AS Expr3, TSPL_VENDOR_MASTER.Add2 AS Expr4, " & _
        ''                    " TSPL_VENDOR_MASTER.Add3 AS Expr5, TSPL_VENDOR_MASTER.City_Code_Desc " & _
        ''" FROM         TSPL_PJV_HEAD LEFT OUTER JOIN " & _
        ''                   "  TSPL_PJV_Detail ON TSPL_PJV_HEAD.PJV_No = TSPL_PJV_Detail.PJV_No LEFT OUTER JOIN " & _
        ''                   "  TSPL_VENDOR_MASTER ON TSPL_PJV_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code LEFT OUTER JOIN " & _
        ''                    " TSPL_COMPANY_MASTER ON TSPL_PJV_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code where TSPL_PJV_HEAD.Invoice_No='" + txtDocNo.Value + "' "


        ''           Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        ''           PurchaseOrderViewer.funreport(dt, "rptPJV", "PJV Report")

        ''       Catch ex As Exception

        ''       End Try
    End Sub
    Public Sub print(ByVal StrDocNo As String)


        If clsCommon.myLen(StrDocNo) <= 0 Then
            Exit Sub
        End If


        Try
            Dim ChkPost As Integer = 0
            Dim qry As String = Nothing
            Dim qry1 As String = Nothing
            ChkPost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select status from TSPL_PI_HEAD where PI_No='" & StrDocNo & "'"))
            If ChkPost > 0 Then
                qry = "	 select T1.*,TSPL_COMPANY_MASTER.Logo_Img as Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2 as Logo_Img2 from 
			   ( SELECT  'Purchase Invoice' as PJVGroup, (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name,  (tspl_state_master_for_location_state.GST_STATE_Code) as LOC_GST_State_Code,(TSPL_LOCATION_MASTER.GSTNO) as Loc_GstInNo ,(TSPL_VENDOR_MASTER.GSTFinalNo) AS Vendor_GSTIN_NO,(TSPL_STATE_MASTER.GST_STATE_Code) AS Vendor_GST_StateCode,
               (TSPL_JOURNAL_MASTER.Modify_By) AS Modify_By,(TSPL_JOURNAL_MASTER.Voucher_No) AS  PJV_No, convert(varchar,(TSPL_JOURNAL_MASTER.Voucher_Date),103) as PJV_Date , (TSPL_VENDOR_MASTER.Vendor_Code) AS Vendor_Code, (TSPL_VENDOR_MASTER.Vendor_Name) AS Vendor_Name, (TSPL_PI_HEAD.PI_No) AS  Invoice_No, (TSPL_SRN_HEAD.Against_PO) AS Against_PO, (TSPL_PI_HEAD.Against_PO) AS PO_No, convert(varchar,(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date),103) AS  PO_Date, isnull(LEFT((el.files) ,LEN((el.files ))-1),'NoFile') as SRN_No,
               convert(varchar,(TSPL_SRN_HEAD.SRN_Date),103) AS SRN_Date , (TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No) AS Vendor_Invoice_No, convert(varchar,(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date),103) AS  Invoice_Date,   	 case when (TSPL_PI_HEAD.Status)=0 then 'Pending' else 'Approved'end as status,  convert(varchar,( TSPL_PI_HEAD.Posting_Date),103) AS Posting_Date, (TSPL_PI_HEAD.PI_Total_Amt) as PJV_Amount, isnull((TSPL_PI_REMITTANCE.Actual_Total_TDS),0)as PJV_TDS_Amount,  (TSPL_PI_HEAD.PI_Total_Amt) as PJV_Net_Amount, 
               (TSPL_JOURNAL_MASTER.Source_Narration) as  Narration, (TSPL_JOURNAL_MASTER.Created_By) AS Created_By, (TSPL_JOURNAL_DETAILS.Detail_Line_No) AS Line_No,  (TSPL_JOURNAL_DETAILS.Account_code) AS  GL_Account_Code, (TSPL_JOURNAL_DETAILS.Account_Desc) AS GL_Account_Desc,  (TSPL_JOURNAL_DETAILS.Amount) AS Expr1,(TSPL_JOURNAL_DETAILS.Amount) as Amount,
               (TSPL_SRN_HEAD.Bill_To_Location) AS Bill_To_Location, ( TSPL_COMPANY_MASTER.Comp_Name) AS Comp_Name, (TSPL_COMPANY_MASTER.Add1) AS Add1, (TSPL_COMPANY_MASTER.Add2) as Add2, (TSPL_COMPANY_MASTER.Add3) as Add3,   (TSPL_COMPANY_MASTER.State) as State, (TSPL_COMPANY_MASTER.CST_LST) as CST_LST,(TSPL_COMPANY_MASTER.Comp_Code) AS Comp_Code, (TSPL_VENDOR_MASTER.Vendor_Name) AS Expr2, (TSPL_VENDOR_MASTER.Add1) AS Expr3, (TSPL_VENDOR_MASTER.Add2) AS Expr4,  (TSPL_VENDOR_MASTER.Add3) AS Expr5, (TSPL_VENDOR_MASTER.City_Code_Desc) as City_Code_Desc ,
               convert(varchar,(TSPL_PI_HEAD.Due_Date),103) as Due_Date,convert(varchar,(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date),103)  as Vendor_Invoice_Date,  (TSPL_PI_HEAD.Comments) AS Comments ,(TSPL_PI_HEAD.Dept_Desc) AS Dept_Desc,(TSPL_PI_HEAD.TransporterDesc) AS TransporterDesc ,(TSPL_PI_HEAD.VehicleDesc ) AS VehicleDesc  ,(TSPL_PI_HEAD.TapalNo) as TapalNo,(TSPL_PI_HEAD.DateAndTime) as DateAndTime FROM  TSPL_PI_HEAD 
               LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_PI_HEAD.PI_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I'  LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No  LEFT OUTER JOIN  TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  LEFT OUTER JOIN  TSPL_SRN_HEAD on TSPL_PI_HEAD.Against_SRN= TSPL_SRN_HEAD.SRN_NO  LEFT OUTER JOIN  TSPL_VENDOR_MASTER ON TSPL_JOURNAL_MASTER.CustVend_Code = TSPL_VENDOR_MASTER.Vendor_Code
               left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SRN_HEAD.Bill_To_Location   left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code  LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PI_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No   left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By 
               on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PI_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_PI_HEAD.Modify_By  left outer join TSPL_PI_REMITTANCE on TSPL_PI_HEAD.PI_No=TSPL_PI_REMITTANCE.Document_No  LEFT OUTER JOIN  TSPL_COMPANY_MASTER ON TSPL_PI_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code CROSS APPLY " &
               " (SELECT distinct TSPL_PI_DETAIL.SRN_Id + ',' AS [text()]   FROM TSPL_PI_HEAD left outer join  TSPL_PI_DETAIL on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No  where TSPL_PI_DETAIL.PI_No='" & StrDocNo & "' FOR XML PATH(''))el(files) where TSPL_PI_HEAD.PI_No='" & StrDocNo & "' 
               " & Environment.NewLine & " UNION ALL " & Environment.NewLine & " SELECT 'SRN' as PJVGroup, (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name,  (tspl_state_master_for_location_state.GST_STATE_Code) as LOC_GST_State_Code,(TSPL_LOCATION_MASTER.GSTNO) as Loc_GstInNo ,(TSPL_VENDOR_MASTER.GSTFinalNo) AS Vendor_GSTIN_NO,(TSPL_STATE_MASTER.GST_STATE_Code) AS Vendor_GST_StateCode,  (tab2.Modify_By) AS Modify_By,(tab2.Voucher_No) AS  PJV_No, convert(varchar,(tab2.Voucher_Date),103) as PJV_Date ,
               (TSPL_VENDOR_MASTER.Vendor_Code) AS Vendor_Code, (TSPL_VENDOR_MASTER.Vendor_Name) AS Vendor_Name, (TSPL_SRN_HEAD.SRN_No) AS  Invoice_No, (TSPL_SRN_HEAD.Against_PO) AS Against_PO, (TSPL_PI_HEAD.Against_PO) AS PO_No, convert(varchar,(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date),103) AS  PO_Date, isnull(LEFT((el.files) ,LEN((el.files ))-1),'NoFile') as SRN_No, convert(varchar,(TSPL_SRN_HEAD.SRN_Date),103) AS SRN_Date , '' AS Vendor_Invoice_No, ''  AS  Invoice_Date,   	 case when (TSPL_SRN_HEAD.Status)=0 then 'Pending' else 'Approved'end as status,
               convert(varchar,( TSPL_SRN_HEAD.Posting_Date),103) AS Posting_Date, (TSPL_SRN_HEAD.SRN_Total_Amt) as PJV_Amount, 0 as PJV_TDS_Amount,  0 as PJV_Net_Amount, (tab2.Source_Narration) as  Narration, (tab2.Created_By) AS Created_By, (TSPL_JOURNAL_DETAILS.Detail_Line_No) AS Line_No,  (TSPL_JOURNAL_DETAILS.Account_code) AS  GL_Account_Code, (TSPL_JOURNAL_DETAILS.Account_Desc) AS GL_Account_Desc,  (TSPL_JOURNAL_DETAILS.Amount) AS Expr1,(TSPL_JOURNAL_DETAILS.Amount) as Amount, (TSPL_SRN_HEAD.Bill_To_Location) AS Bill_To_Location, ( TSPL_COMPANY_MASTER.Comp_Name) AS Comp_Name, (TSPL_COMPANY_MASTER.Add1) AS Add1, (TSPL_COMPANY_MASTER.Add2) as Add2, (TSPL_COMPANY_MASTER.Add3) as Add3,   (TSPL_COMPANY_MASTER.State) as State, (TSPL_COMPANY_MASTER.CST_LST) as CST_LST,(TSPL_COMPANY_MASTER.Comp_Code) AS Comp_Code, (TSPL_VENDOR_MASTER.Vendor_Name) AS Expr2, (TSPL_VENDOR_MASTER.Add1) AS Expr3, (TSPL_VENDOR_MASTER.Add2) AS Expr4,  (TSPL_VENDOR_MASTER.Add3) AS Expr5,
               (TSPL_VENDOR_MASTER.City_Code_Desc) as City_Code_Desc ,convert(varchar,(TSPL_SRN_HEAD.Due_Date),103) as Due_Date,''   as Vendor_Invoice_Date,  (TSPL_SRN_HEAD.Comments) AS Comments ,(TSPL_SRN_HEAD.Dept_Desc) AS Dept_Desc,'' AS TransporterDesc ,(TSPL_SRN_HEAD.VehicleNo ) AS VehicleDesc  ,'' as TapalNo,NULL as DateAndTime FROM  TSPL_SRN_HEAD  left outer join  TSPL_PI_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_PI_DETAIL.SRN_Id  LEFT OUTER JOIN  TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No= TSPL_PI_DETAIL.PI_No 
               left join (select SRN_Id from TSPL_PI_DETAIL where PI_No='" & StrDocNo & "' )as tab1 on tab1.SRN_Id =  TSPL_PI_DETAIL.SRN_Id
               LEFT  JOIN (select * from  TSPL_JOURNAL_MASTER  where Source_Doc_No in ( select SRN_Id from TSPL_PI_DETAIL where PI_No='" & StrDocNo & "') and Source_Code='PO-RC' ) as tab2 on tab1.SRN_Id = tab2.Source_Doc_No  LEFT OUTER JOIN  TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_DETAILS.Voucher_No = tab2.Voucher_No    LEFT OUTER JOIN  TSPL_VENDOR_MASTER ON tab2.CustVend_Code = TSPL_VENDOR_MASTER.Vendor_Code   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SRN_HEAD.Bill_To_Location   left outer join tspl_state_master 
               as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code  LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PI_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By on TSPL_USER_MASTER_Created_By.User_Code = TSPL_SRN_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_SRN_HEAD.Modify_By 
                LEFT OUTER JOIN  TSPL_COMPANY_MASTER ON TSPL_SRN_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code CROSS APPLY  (  SELECT distinct TSPL_PI_DETAIL.SRN_Id + ',' AS [text()]   FROM TSPL_PI_DETAIL 
               left outer join  TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No 
               where TSPL_PI_DETAIL.PI_No ='" & StrDocNo & "'  FOR XML PATH(''))el(files) where TSPL_PI_HEAD.PI_No='" & StrDocNo & "'
               " & Environment.NewLine & " UNION ALL " & Environment.NewLine & " select 'SRN Deductions' as PJVGroup,(TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name,  (tspl_state_master_for_location_state.GST_STATE_Code) as LOC_GST_State_Code,(TSPL_LOCATION_MASTER.GSTNO) as Loc_GstInNo ,(TSPL_VENDOR_MASTER.GSTFinalNo) AS Vendor_GSTIN_NO,(TSPL_STATE_MASTER.GST_STATE_Code) AS Vendor_GST_StateCode,  (TABJOURNAL.Modify_By) AS Modify_By,(TABJOURNAL.Voucher_No) AS  PJV_No, convert(varchar,(TABJOURNAL.Voucher_Date),103) as PJV_Date ,
               (TSPL_VENDOR_MASTER.Vendor_Code) AS Vendor_Code, (TSPL_VENDOR_MASTER.Vendor_Name) AS Vendor_Name, (TSPL_SRN_DEDUCTION_SECURITY.SRN_No) AS  Invoice_No, (TSPL_SRN_HEAD.Against_PO) AS Against_PO, (TSPL_PI_HEAD.Against_PO) AS PO_No, convert(varchar,(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date),103) AS  PO_Date, isnull(LEFT((el.files) ,LEN((el.files ))-1),'NoFile') as SRN_No, convert(varchar,(TSPL_SRN_HEAD.SRN_Date),103) AS SRN_Date , (tabInvoice.Vendor_Invoice_No) AS Vendor_Invoice_No, convert(varchar,(tabInvoice.Vendor_Invoice_Date),103) AS  Invoice_Date, 
               case when (TSPL_SRN_HEAD.Status)=0 then 'Pending' else 'Approved'end as status,  convert(varchar,( TSPL_SRN_HEAD.Posting_Date),103) AS Posting_Date, (TSPL_SRN_DEDUCTION_SECURITY.Ded_Amt) as PJV_Amount, 0 as PJV_TDS_Amount,  0 as PJV_Net_Amount, (TABJOURNAL.Source_Narration) as  Narration, (TABJOURNAL.Created_By) AS Created_By, (TSPL_JOURNAL_DETAILS.Detail_Line_No) AS Line_No,  (TSPL_JOURNAL_DETAILS.Account_code) AS  GL_Account_Code, (TSPL_JOURNAL_DETAILS.Account_Desc) AS GL_Account_Desc, 
               (TSPL_JOURNAL_DETAILS.Amount) AS Expr1,(TSPL_JOURNAL_DETAILS.Amount) as Amount, (TSPL_SRN_HEAD.Bill_To_Location) AS Bill_To_Location, ( TSPL_COMPANY_MASTER.Comp_Name) AS Comp_Name, (TSPL_COMPANY_MASTER.Add1) AS Add1, (TSPL_COMPANY_MASTER.Add2) as Add2, (TSPL_COMPANY_MASTER.Add3) as Add3,   (TSPL_COMPANY_MASTER.State) as State, (TSPL_COMPANY_MASTER.CST_LST) as CST_LST,(TSPL_COMPANY_MASTER.Comp_Code) AS Comp_Code, 
               (TSPL_VENDOR_MASTER.Vendor_Name) AS Expr2, (TSPL_VENDOR_MASTER.Add1) AS Expr3, (TSPL_VENDOR_MASTER.Add2) AS Expr4,  (TSPL_VENDOR_MASTER.Add3) AS Expr5, (TSPL_VENDOR_MASTER.City_Code_Desc) as City_Code_Desc ,convert(varchar,(TSPL_SRN_HEAD.Due_Date),103) as Due_Date,convert(varchar,(tabInvoice.Vendor_Invoice_Date),103) AS  Invoice_Date,   (TSPL_SRN_HEAD.Comments) AS Comments ,(TSPL_SRN_HEAD.Dept_Desc) AS Dept_Desc,'' AS TransporterDesc ,(TSPL_SRN_HEAD.VehicleNo ) AS VehicleDesc  ,'' as TapalNo, NULL as DateAndTime FROM  TSPL_SRN_DEDUCTION_SECURITY
               LEFT JOIN (select max(SRN_No)SRN_No , TSPL_PI_DETAIL.PI_No from TSPL_SRN_DEDUCTION_SECURITY LEFT OUTER JOIN TSPL_PI_DETAIL ON TSPL_SRN_DEDUCTION_SECURITY.SRN_No = TSPL_PI_DETAIL.SRN_Id where PI_No='" & StrDocNo & "' group by PI_No ) AS tab1 on tab1.SRN_No = TSPL_SRN_DEDUCTION_SECURITY.SRN_No
               left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No = tab1.PI_No  LEFT  JOIN (select * from TSPL_VENDOR_INVOICE_HEAD where  TSPL_VENDOR_INVOICE_HEAD.Document_Type= 'D' and  TSPL_VENDOR_INVOICE_HEAD.RefDocType = 'SEC-DED' and TSPL_VENDOR_INVOICE_HEAD.RefDocNo = '" & StrDocNo & "')as tabInvoice ON TSPL_PI_HEAD.PI_No =tabInvoice.RefDocNo LEFT JOIN ( select *   from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Document_No from TSPL_VENDOR_INVOICE_HEAD where RefDocNo='" & StrDocNo & "' and RefDocType in ('SEC-DED')) and Source_Code='AP-DN')AS TABJOURNAL ON TABJOURNAL.Source_Doc_No = tabInvoice.Document_No
               LEFT OUTER JOIN  TSPL_JOURNAL_DETAILS ON TABJOURNAL.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  LEFT OUTER JOIN  TSPL_VENDOR_MASTER ON TABJOURNAL.CustVend_Code = TSPL_VENDOR_MASTER.Vendor_Code LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DEDUCTION_SECURITY.SRN_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SRN_HEAD.Bill_To_Location   left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state left outer join TSPL_STATE_MASTER
               on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code  LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PI_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By on TSPL_USER_MASTER_Created_By.User_Code = TSPL_SRN_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_SRN_HEAD.Modify_By   LEFT OUTER JOIN  TSPL_COMPANY_MASTER 
               ON TSPL_SRN_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code CROSS APPLY  (  SELECT distinct tspl_PI_Detail.SRN_Id + ',' AS [text()]   FROM tspl_PI_Detail left outer join TSPL_SRN_DEDUCTION_SECURITY  on TSPL_SRN_DEDUCTION_SECURITY.SRN_No = tspl_PI_Detail.SRN_Id    WHERE  SRN_No in (select SRN_ID from tspl_PI_Detail where PI_No='" & StrDocNo & "' ) 
               FOR XML PATH(''))el(files) WHERE TSPL_PI_HEAD.PI_No = '" & StrDocNo & "'
               " & Environment.NewLine & " UNION ALL " & Environment.NewLine & "  select 'SRN Deductions' as PJVGroup, (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name,  (tspl_state_master_for_location_state.GST_STATE_Code) as LOC_GST_State_Code,(TSPL_LOCATION_MASTER.GSTNO) as Loc_GstInNo ,(TSPL_VENDOR_MASTER.GSTFinalNo) AS Vendor_GSTIN_NO,(TSPL_STATE_MASTER.GST_STATE_Code) AS Vendor_GST_StateCode,  (TABJOURNAL.Modify_By) AS Modify_By,(TABJOURNAL.Voucher_No) AS  PJV_No, convert(varchar,(TABJOURNAL.Voucher_Date),103) as PJV_Date , (TSPL_VENDOR_MASTER.Vendor_Code) AS Vendor_Code,
               (TSPL_VENDOR_MASTER.Vendor_Name) AS Vendor_Name, (TSPL_SRN_DEDUCTION.SRN_No) AS  Invoice_No, (TSPL_SRN_HEAD.Against_PO) AS Against_PO, (TSPL_PI_HEAD.Against_PO) AS PO_No, convert(varchar,(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date),103) AS  PO_Date, isnull(LEFT((el.files) ,LEN((el.files ))-1),'NoFile') as SRN_No, convert(varchar,(TSPL_SRN_HEAD.SRN_Date),103) AS SRN_Date , (tabInvoice.Vendor_Invoice_No) AS Vendor_Invoice_No, convert(varchar,(tabInvoice.Vendor_Invoice_Date),103) AS  Invoice_Date,   	 case when (TSPL_SRN_HEAD.Status)=0 then 'Pending' else 'Approved'end as status,  convert(varchar,( TSPL_SRN_HEAD.Posting_Date),103) AS Posting_Date,
               (TSPL_SRN_DEDUCTION.Ded_Amt) as PJV_Amount, 0 as PJV_TDS_Amount,  0 as PJV_Net_Amount, (TABJOURNAL.Source_Narration) as  Narration, (TABJOURNAL.Created_By) AS Created_By, (TSPL_JOURNAL_DETAILS.Detail_Line_No) AS Line_No,  (TSPL_JOURNAL_DETAILS.Account_code) AS  GL_Account_Code, (TSPL_JOURNAL_DETAILS.Account_Desc) AS GL_Account_Desc,  (TSPL_JOURNAL_DETAILS.Amount) AS Expr1,(TSPL_JOURNAL_DETAILS.Amount) as Amount, 
               (TSPL_SRN_HEAD.Bill_To_Location) AS Bill_To_Location, ( TSPL_COMPANY_MASTER.Comp_Name) AS Comp_Name, (TSPL_COMPANY_MASTER.Add1) AS Add1, (TSPL_COMPANY_MASTER.Add2) as Add2, (TSPL_COMPANY_MASTER.Add3) as Add3,   (TSPL_COMPANY_MASTER.State) as State, (TSPL_COMPANY_MASTER.CST_LST) as CST_LST,(TSPL_COMPANY_MASTER.Comp_Code) AS Comp_Code, (TSPL_VENDOR_MASTER.Vendor_Name) AS Expr2, (TSPL_VENDOR_MASTER.Add1) AS Expr3, (TSPL_VENDOR_MASTER.Add2) AS Expr4,  (TSPL_VENDOR_MASTER.Add3) AS Expr5, (TSPL_VENDOR_MASTER.City_Code_Desc) as City_Code_Desc ,convert(varchar,(TSPL_SRN_HEAD.Due_Date),103) as Due_Date,convert(varchar,(tabInvoice.Vendor_Invoice_Date),103) AS  Invoice_Date, 
               (TSPL_SRN_HEAD.Comments) AS Comments ,(TSPL_SRN_HEAD.Dept_Desc) AS Dept_Desc,'' AS TransporterDesc ,(TSPL_SRN_HEAD.VehicleNo ) AS VehicleDesc  ,'' as TapalNo,NULL  as DateAndTime from TSPL_SRN_DEDUCTION LEFT JOIN (select max(SRN_No)SRN_No , TSPL_PI_DETAIL.PI_No from TSPL_SRN_DEDUCTION LEFT OUTER JOIN TSPL_PI_DETAIL ON TSPL_SRN_DEDUCTION.SRN_No = TSPL_PI_DETAIL.SRN_Id where PI_No='" & StrDocNo & "' group by PI_No ) AS tab1 on tab1.SRN_No = TSPL_SRN_DEDUCTION.SRN_No
               left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No = tab1.PI_No  LEFT  JOIN (select * from TSPL_VENDOR_INVOICE_HEAD where  TSPL_VENDOR_INVOICE_HEAD.Document_Type= 'D' and  TSPL_VENDOR_INVOICE_HEAD.RefDocType = 'QC-DED' and TSPL_VENDOR_INVOICE_HEAD.RefDocNo = '" & StrDocNo & "')as tabInvoice ON TSPL_PI_HEAD.PI_No =tabInvoice.RefDocNo 
               LEFT JOIN ( select *   from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Document_No from TSPL_VENDOR_INVOICE_HEAD where RefDocNo='" & StrDocNo & "' and RefDocType in ('QC-DED')) and Source_Code='AP-DN')AS TABJOURNAL ON TABJOURNAL.Source_Doc_No = tabInvoice.Document_No LEFT OUTER JOIN  TSPL_JOURNAL_DETAILS ON TABJOURNAL.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  LEFT OUTER JOIN  TSPL_VENDOR_MASTER ON TABJOURNAL.CustVend_Code = TSPL_VENDOR_MASTER.Vendor_Code 
               LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DEDUCTION.SRN_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SRN_HEAD.Bill_To_Location   left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code  LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PI_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No 
               left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By on TSPL_USER_MASTER_Created_By.User_Code = TSPL_SRN_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_SRN_HEAD.Modify_By   LEFT OUTER JOIN  TSPL_COMPANY_MASTER ON TSPL_SRN_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code CROSS APPLY  (SELECT distinct tspl_PI_Detail.SRN_Id + ',' AS [text()]   FROM tspl_PI_Detail left outer join TSPL_SRN_DEDUCTION  on TSPL_SRN_DEDUCTION.SRN_No = tspl_PI_Detail.SRN_Id
                where SRN_No in (select SRN_Id from TSPL_PI_DETAIL where PI_No='" & StrDocNo & "') FOR XML PATH(''))el(files) WHERE TSPL_PI_HEAD.PI_No = '" & StrDocNo & "'
                " & Environment.NewLine & " UNION ALL " & Environment.NewLine & " select  'SRN Deductions' as PJVGroup, (TSPL_USER_MASTER_Created_By.User_Name) as Invoice_Created_By_Name,(TSPL_USER_MASTER_Modified_By.User_Name) as Invoice_Modifiy_By_Name,  (tspl_state_master_for_location_state.GST_STATE_Code) as LOC_GST_State_Code,(TSPL_LOCATION_MASTER.GSTNO) as Loc_GstInNo ,(TSPL_VENDOR_MASTER.GSTFinalNo) AS Vendor_GSTIN_NO,(TSPL_STATE_MASTER.GST_STATE_Code) AS Vendor_GST_StateCode,  (TABJOURNAL.Modify_By) AS Modify_By,(TABJOURNAL.Voucher_No) AS  PJV_No, convert(varchar,(TABJOURNAL.Voucher_Date),103) as PJV_Date ,
                (TSPL_VENDOR_MASTER.Vendor_Code) AS Vendor_Code, (TSPL_VENDOR_MASTER.Vendor_Name) AS Vendor_Name, (TSPL_SRN_TENDER.SRN_No) AS  Invoice_No, (TSPL_SRN_HEAD.Against_PO) AS Against_PO, (TSPL_PI_HEAD.Against_PO) AS PO_No, convert(varchar,(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date),103) AS  PO_Date, isnull(LEFT((el.files) ,LEN((el.files ))-1),'NoFile') as SRN_No, convert(varchar,(TSPL_SRN_HEAD.SRN_Date),103) AS SRN_Date , (tabInvoice.Vendor_Invoice_No) AS Vendor_Invoice_No, convert(varchar,(tabInvoice.Vendor_Invoice_Date),103) AS  Invoice_Date, case when (TSPL_SRN_HEAD.Status)=0 then 'Pending' else 'Approved'end as status,  convert(varchar,( TSPL_SRN_HEAD.Posting_Date),103) AS Posting_Date,
                (TSPL_SRN_TENDER.Penalty) as PJV_Amount, 0 as PJV_TDS_Amount,  0 as PJV_Net_Amount, (TABJOURNAL.Source_Narration) as  Narration, (TABJOURNAL.Created_By) AS Created_By, (TSPL_JOURNAL_DETAILS.Detail_Line_No) AS Line_No,  (TSPL_JOURNAL_DETAILS.Account_code) AS  GL_Account_Code, (TSPL_JOURNAL_DETAILS.Account_Desc) AS GL_Account_Desc,  (TSPL_JOURNAL_DETAILS.Amount) AS Expr1,(TSPL_JOURNAL_DETAILS.Amount) as Amount, (TSPL_SRN_HEAD.Bill_To_Location) AS Bill_To_Location, ( TSPL_COMPANY_MASTER.Comp_Name) AS Comp_Name, 
                (TSPL_COMPANY_MASTER.Add1) AS Add1, (TSPL_COMPANY_MASTER.Add2) as Add2, (TSPL_COMPANY_MASTER.Add3) as Add3,   (TSPL_COMPANY_MASTER.State) as State, (TSPL_COMPANY_MASTER.CST_LST) as CST_LST,(TSPL_COMPANY_MASTER.Comp_Code) AS Comp_Code, (TSPL_VENDOR_MASTER.Vendor_Name) AS Expr2, (TSPL_VENDOR_MASTER.Add1) AS Expr3, (TSPL_VENDOR_MASTER.Add2) AS Expr4,  (TSPL_VENDOR_MASTER.Add3) AS Expr5, (TSPL_VENDOR_MASTER.City_Code_Desc) as City_Code_Desc ,convert(varchar,(TSPL_SRN_HEAD.Due_Date),103) as Due_Date,convert(varchar,(tabInvoice.Vendor_Invoice_Date),103) AS  Invoice_Date, (TSPL_SRN_HEAD.Comments) AS Comments ,(TSPL_SRN_HEAD.Dept_Desc) AS Dept_Desc,'' AS TransporterDesc ,(TSPL_SRN_HEAD.VehicleNo ) AS VehicleDesc  ,'' as TapalNo,NULL as DateAndTime FROM  
                 TSPL_SRN_TENDER LEFT JOIN (select max(SRN_No)SRN_No , TSPL_PI_DETAIL.PI_No from TSPL_SRN_TENDER LEFT OUTER JOIN TSPL_PI_DETAIL ON TSPL_SRN_TENDER.SRN_No = TSPL_PI_DETAIL.SRN_Id where PI_No='" & StrDocNo & "' group by PI_No ) AS tab1 on tab1.SRN_No = TSPL_SRN_TENDER.SRN_No left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No = tab1.PI_No LEFT  JOIN (select * from TSPL_VENDOR_INVOICE_HEAD where  TSPL_VENDOR_INVOICE_HEAD.Document_Type= 'D' and  TSPL_VENDOR_INVOICE_HEAD.RefDocType = 'SCH-PNT' and TSPL_VENDOR_INVOICE_HEAD.RefDocNo = '" & StrDocNo & "')as tabInvoice ON TSPL_PI_HEAD.PI_No =tabInvoice.RefDocNo 
                LEFT JOIN ( select *   from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Document_No from TSPL_VENDOR_INVOICE_HEAD where RefDocNo='" & StrDocNo & "' and RefDocType in ('SCH-PNT')) and Source_Code='AP-DN')AS TABJOURNAL ON TABJOURNAL.Source_Doc_No = tabInvoice.Document_No LEFT OUTER JOIN  TSPL_JOURNAL_DETAILS ON TABJOURNAL.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No   LEFT OUTER JOIN  TSPL_VENDOR_MASTER ON TABJOURNAL.CustVend_Code = TSPL_VENDOR_MASTER.Vendor_Code  LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.SRN_No = tab1.SRN_No
                left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SRN_HEAD.Bill_To_Location   left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code  LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PI_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No 
                left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By on TSPL_USER_MASTER_Created_By.User_Code = TSPL_SRN_HEAD.Created_By  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified_By on TSPL_USER_MASTER_Modified_By.User_Code = TSPL_SRN_HEAD.Modify_By    LEFT OUTER JOIN  TSPL_COMPANY_MASTER ON TSPL_SRN_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code CROSS APPLY  (SELECT distinct tspl_PI_Detail.SRN_Id + ',' AS [text()]   FROM tspl_PI_Detail left outer join TSPL_SRN_TENDER  on TSPL_SRN_TENDER.SRN_No = tspl_PI_Detail.SRN_Id  
                where SRN_No in (select SRN_ID from tspl_PI_Detail where PI_No='" & StrDocNo & "' ) and TSPL_SRN_TENDER.Penalty > 0 FOR XML PATH(''))el(files) WHERE TSPL_PI_HEAD.PI_No = '" & StrDocNo & "'
                      ) as t1 " &
                      " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON T1.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code"

                qry1 = "select TSPL_ITEM_MASTER.HSN_Code, TSPL_PI_detail.Unit_code,TSPL_PI_DETAIL.line_no,TSPL_PI_HEAD.Created_By,TSPL_PI_HEAD.Modify_By, isnull(LEFT(el.files ,LEN(el.files )-1),'NoFile')  as srn_no,TSPL_PI_DETAIL .Item_Code as Item_Code , " &
            "TSPL_PI_DETAIL .Item_Desc as Item_Desc,TSPL_PI_DETAIL .Item_GL_Account_Desc as FaAccount, " &
            "isnull(TSPL_PI_DETAIL .PI_Qty,0) as PI_Qty, ISNULL(TSPL_PI_DETAIL .Burst_Qty ,0) as Burst_Qty, ISNULL( TSPL_PI_DETAIL .Leak_Qty ,0) as Leak_Qty,isnull(tspl_pi_detail.Short_Qty ,0) as Short_Qty,isnull(tspl_pi_detail.Reject_Qty ,0) AS Reject_Qty," &
            "(isnull(TSPL_PI_DETAIL .PI_Qty,0)+ISNULL(TSPL_PI_DETAIL .Burst_Qty ,0)+ISNULL( TSPL_PI_DETAIL .Leak_Qty ,0)+isnull(tspl_pi_detail.Short_Qty ,0))as  SRN_Qty, " &
            "tspl_PI_detail.Landed_Cost_Rate as Landed_Cost_Rate,Tspl_PI_Detail.Landed_Cost_Amount as Landed_Cost_Amount , " &
            "(case TSPL_SRN_HEAD .Item_Type  when 'F'then'Finished Goods'when 'R' then 'Raw Material' when 'O' then 'Other'when 'P' then " &
            "'Promotional Item' else '' end) as Item_Type from TSPL_PI_HEAD left outer join TSPL_PI_DETAIL ON TSPL_PI_HEAD.PI_NO= TSPL_PI_DETAIL.pi_no left outer join TSPL_SRN_HEAD  on " &
            " TSPL_SRN_HEAD .SRN_No =TSPL_PI_DETAIL .SRN_Id  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_PI_DETAIL.Item_Code  " &
            " CROSS APPLY  (SELECT distinct SRN_Id + ',' AS [text()] FROM TSPL_PI_DETAIL  where TSPL_PI_DETAIL.PI_No='" + StrDocNo + "' FOR XML PATH(''))el(files)  " &
            " where TSPL_PI_DETAIL .Row_Type <>'Misc' and TSPL_PI_DETAIL.PI_No   ='" + StrDocNo + "' and 1=1 "
                qry1 += "union all"
                qry1 += "  select TSPL_ITEM_MASTER.HSN_Code,TSPL_PI_detail.Unit_code, TSPL_PI_DETAIL.line_no,TSPL_PI_HEAD.Created_By,TSPL_PI_HEAD.Modify_By, isnull(LEFT(el.files ,LEN(el.files )-1),'NoFile')  as srn_no,TSPL_PI_DETAIL .Item_Code as Item_Code , TSPL_PI_DETAIL .Item_Desc as Item_Desc, " &
                "TSPL_PI_DETAIL .Item_GL_Account_Desc as FaAccount,0 as PI_Qty ,0 as Burst_Qty ,0 as Leak_Qty ,0 as Short_Qty,0 AS Reject_Qty,(isnull(tspl_pi_detail.Free_Qty  ,0))as  SRN_Qty, '0' as Landed_Cost_Rate, " &
                "'0' as Landed_Cost_Amount ,(case TSPL_SRN_HEAD .Item_Type  when 'F'then'Finished Goods'when 'R' then 'Raw Material' when  " &
                "'O' then 'Other'when 'P' then 'Promotional Item' else '' end) as Item_Type from TSPL_PI_HEAD left outer join TSPL_PI_DETAIL ON TSPL_PI_HEAD.PI_NO= TSPL_PI_DETAIL.pi_no left outer join  " &
                "TSPL_SRN_HEAD  on TSPL_SRN_HEAD .SRN_No =TSPL_PI_DETAIL .SRN_Id  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_PI_DETAIL.Item_Code  " &
                " CROSS APPLY  (SELECT distinct SRN_Id + ',' AS [text()]   FROM TSPL_PI_DETAIL  where TSPL_PI_DETAIL .PI_No='" + StrDocNo + "' FOR XML PATH(''))el(files)  " &
                " where TSPL_PI_DETAIL .Row_Type <>'Misc' and " &
                "TSPL_PI_DETAIL.PI_No   ='" + StrDocNo + "' and 1=1  and Free_Qty >0 order by line_no"

            Else

                qry = "  SELECT  TSPL_USER_MASTER_Created_By.User_Name as Invoice_Created_By_Name,'' as Invoice_Modifiy_By_Name,  tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode,  TSPL_PJV_HEAD.Modify_By,TSPL_PJV_HEAD.PJV_No, convert(varchar, TSPL_PJV_HEAD.PJV_Date,103) as PJV_Date , TSPL_PJV_HEAD.Vendor_Code, TSPL_PJV_HEAD.Vendor_Name, TSPL_PJV_HEAD.Invoice_No, TSPL_SRN_HEAD.Against_PO, TSPL_PJV_HEAD.PO_No, " &
                         " convert(varchar,TSPL_PJV_HEAD.PO_Date,103) as PO_Date, isnull(LEFT(el.files ,LEN(el.files )-1),'NoFile') as SRN_No, convert(varchar,TSPL_PJV_HEAD.SRN_Date,103) as SRN_Date, TSPL_PJV_HEAD.Vendor_Invoice_No, convert(varchar,TSPL_PJV_HEAD.Invoice_Date,103) as Invoice_Date, " &
                        "  case when TSPL_PI_HEAD.Status=0 then 'Pending' else 'Approved'end as status, convert(varchar,TSPL_PJV_HEAD.Posting_Date,103) as Posting_Date, TSPL_PJV_HEAD.PJV_Amount, TSPL_PJV_HEAD.PJV_TDS_Amount, " &
                         " TSPL_PJV_HEAD.PJV_Net_Amount, TSPL_PJV_HEAD.Narration, TSPL_PJV_HEAD.Created_By, TSPL_PJV_Detail.Line_No, " &
                          " TSPL_PJV_Detail.GL_Account_Code, TSPL_PJV_Detail.GL_Account_Desc, TSPL_PJV_Detail.PJV_Amount AS Expr1,case when TSPL_PJV_Detail.PJV_Amount<0 then -1 * TSPL_PJV_Detail.PJV_Amount else 0 end as Credit,case when TSPL_PJV_Detail.PJV_Amount>=0 then TSPL_PJV_Detail.PJV_Amount else 0 end as 
                          ,TSPL_SRN_HEAD.Bill_To_Location, " &
                          " TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2, TSPL_COMPANY_MASTER.Add3, " &
                        "  TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, " &
                         " TSPL_VENDOR_MASTER.Vendor_Name AS Expr2, TSPL_VENDOR_MASTER.Add1 AS Expr3, TSPL_VENDOR_MASTER.Add2 AS Expr4, " &
                         " TSPL_VENDOR_MASTER.Add3 AS Expr5, TSPL_VENDOR_MASTER.City_Code_Desc,convert(varchar,TSPL_PJV_HEAD.Due_Date,103) as Due_Date,(select top 1 InvoiceDate from TSPL_PI_HEAD " &
                         " where PI_No=TSPL_PJV_HEAD.Invoice_No) as Vendor_Invoice_Date, TSPL_PI_HEAD.Comments,TSPL_PJV_HEAD.Dept_Desc,TSPL_PI_HEAD.TransporterDesc,TSPL_PI_HEAD.VehicleDesc " &
                        " ,TSPL_PI_HEAD.TapalNo,TSPL_PI_HEAD.DateAndTime FROM         TSPL_PJV_HEAD LEFT OUTER JOIN " &
                        "  TSPL_PJV_Detail ON TSPL_PJV_HEAD.PJV_No = TSPL_PJV_Detail.PJV_No LEFT OUTER JOIN " &
                         " TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PJV_HEAD.SRN_No " &
                         "LEFT OUTER JOIN TSPL_PI_HEAD  ON TSPL_PI_HEAD.PI_No = TSPL_PJV_HEAD.Invoice_No " &
                        "  LEFT OUTER JOIN  TSPL_VENDOR_MASTER ON TSPL_PJV_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SRN_HEAD.Bill_To_Location left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code LEFT OUTER JOIN " &
                         " TSPL_COMPANY_MASTER ON TSPL_PJV_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code " &
                         " left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created_By on TSPL_USER_MASTER_Created_By.User_Code = TSPL_PI_HEAD.Created_By " &
                         "  " &
                        "CROSS APPLY  (SELECT distinct TSPL_PI_DETAIL.SRN_Id + ',' AS [text()]   FROM TSPL_PJV_HEAD left outer join  TSPL_PI_DETAIL on TSPL_PJV_HEAD.Invoice_No=TSPL_PI_DETAIL.PI_No  where TSPL_PI_DETAIL.PI_No='" & StrDocNo & "' FOR XML PATH(''))el(files) " &
                         "where TSPL_PJV_HEAD.Invoice_No='" + StrDocNo + "' "

                qry1 = "select TSPL_ITEM_MASTER.HSN_Code, TSPL_PI_detail.Unit_code,TSPL_PI_DETAIL.line_no,TSPL_PJV_HEAD.Created_By,TSPL_PJV_HEAD.Modify_By, isnull(LEFT(el.files ,LEN(el.files )-1),'NoFile')  as srn_no,TSPL_PI_DETAIL .Item_Code as Item_Code , " &
                "TSPL_PI_DETAIL .Item_Desc as Item_Desc,TSPL_PI_DETAIL .Item_GL_Account_Desc as FaAccount, " &
                "isnull(TSPL_PI_DETAIL .PI_Qty,0) as PI_Qty, ISNULL(TSPL_PI_DETAIL .Burst_Qty ,0) as Burst_Qty, ISNULL( TSPL_PI_DETAIL .Leak_Qty ,0) as Leak_Qty,isnull(tspl_pi_detail.Short_Qty ,0) as Short_Qty,isnull(tspl_pi_detail.Reject_Qty ,0) AS Reject_Qty," &
                "(isnull(TSPL_PI_DETAIL .PI_Qty,0)+ISNULL(TSPL_PI_DETAIL .Burst_Qty ,0)+ISNULL( TSPL_PI_DETAIL .Leak_Qty ,0)+isnull(tspl_pi_detail.Short_Qty ,0))as  SRN_Qty, " &
                "tspl_PI_detail.Landed_Cost_Rate as Landed_Cost_Rate,Tspl_PI_Detail.Landed_Cost_Amount as Landed_Cost_Amount , " &
                "(case TSPL_SRN_HEAD .Item_Type  when 'F'then'Finished Goods'when 'R' then 'Raw Material' when 'O' then 'Other'when 'P' then " &
                "'Promotional Item' else '' end) as Item_Type from TSPL_PI_DETAIL left outer join TSPL_SRN_HEAD  on " &
                " TSPL_SRN_HEAD .SRN_No =TSPL_PI_DETAIL .SRN_Id left outer join TSPL_PJV_HEAD on TSPL_PJV_HEAD .SRN_No =TSPL_SRN_HEAD .SRN_No left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_PI_DETAIL.Item_Code  " &
                " CROSS APPLY  (SELECT distinct SRN_Id + ',' AS [text()] FROM TSPL_PI_DETAIL  where TSPL_PI_DETAIL.PI_No='" + StrDocNo + "' FOR XML PATH(''))el(files)  " &
                " where TSPL_PI_DETAIL .Row_Type <>'Misc' and TSPL_PI_DETAIL.PI_No   ='" + StrDocNo + "' and 1=1 "
                qry1 += "union all"
                qry1 += "  select TSPL_ITEM_MASTER.HSN_Code, TSPL_PI_detail.Unit_code,TSPL_PI_DETAIL.line_no,TSPL_PJV_HEAD.Created_By,TSPL_PJV_HEAD.Modify_By, isnull(LEFT(el.files ,LEN(el.files )-1),'NoFile')  as srn_no,TSPL_PI_DETAIL .Item_Code as Item_Code , TSPL_PI_DETAIL .Item_Desc as Item_Desc, " &
                "TSPL_PI_DETAIL .Item_GL_Account_Desc as FaAccount,0 as PI_Qty ,0 as Burst_Qty ,0 as Leak_Qty ,0 as Short_Qty,0 AS Reject_Qty,(isnull(tspl_pi_detail.Free_Qty  ,0))as  SRN_Qty, '0' as Landed_Cost_Rate, " &
                "'0' as Landed_Cost_Amount ,(case TSPL_SRN_HEAD .Item_Type  when 'F'then'Finished Goods'when 'R' then 'Raw Material' when  " &
                "'O' then 'Other'when 'P' then 'Promotional Item' else '' end) as Item_Type from TSPL_PI_DETAIL left outer join  " &
                "TSPL_SRN_HEAD  on TSPL_SRN_HEAD .SRN_No =TSPL_PI_DETAIL .SRN_Id left outer join TSPL_PJV_HEAD on  " &
                "TSPL_PJV_HEAD .SRN_No =TSPL_SRN_HEAD .SRN_No left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_PI_DETAIL.Item_Code  " &
                " CROSS APPLY  (SELECT distinct SRN_Id + ',' AS [text()]   FROM TSPL_PI_DETAIL  where TSPL_PI_DETAIL .PI_No='" + StrDocNo + "' FOR XML PATH(''))el(files)  " &
                " where TSPL_PI_DETAIL .Row_Type <>'Misc' and " &
                "TSPL_PI_DETAIL.PI_No   ='" + StrDocNo + "' and 1=1  and Free_Qty >0 order by line_no"
            End If
            qry = "select  ROW_NUMBER() over(order by (PJVGroup)) as Rownum, PJVGroup1 ,PJVGroup , Invoice_Created_By_Name , Invoice_Modifiy_By_Name , LOC_GST_State_Code , Loc_GstInNo , Vendor_GSTIN_NO , Vendor_GST_StateCode , Modify_By , PJV_No , PJV_Date , Vendor_Code ,Vendor_Name,Invoice_No,Against_PO,PO_No,SRN_No,SRN_Date,Vendor_Invoice_No,Invoice_Date,status,Posting_Date,PJV_Amount,PJV_TDS_Amount,PJV_Net_Amount,Narration,Created_By,Line_No,GL_Account_Code,GL_Account_Desc,Expr1, case when (Amount)<0 then -1 * (Amount) else 0 end as Credit,case when (Amount)>=0 then (Amount) else 0 end as Debit, Bill_To_Location,Comp_Name,add1,add2,add3,State,CST_LST,Comp_Code,expr2,expr3,expr4,expr5,City_Code_Desc,Due_Date,Vendor_Invoice_Date,Comments,Dept_Desc,TransporterDesc,VehicleDesc,TapalNo,DateAndTime,Amount from ( SELECT 'Purchase Invoice' as PJVGroup1,max(PJVGroup)PJVGroup , MAX(Invoice_Created_By_Name)Invoice_Created_By_Name , MAX(Invoice_Modifiy_By_Name)Invoice_Modifiy_By_Name,MAX(LOC_GST_State_Code)LOC_GST_State_Code , MAX(Loc_GstInNo)Loc_GstInNo, MAX(Vendor_GSTIN_NO)Vendor_GSTIN_NO , MAX(Vendor_GST_StateCode)Vendor_GST_StateCode, MAX(Modify_By)Modify_By , max(PJV_No)PJV_No , MAX(PJV_Date)PJV_Date , MAX(Vendor_Code)Vendor_Code , MAX(Vendor_Name)Vendor_Name , MAX(Invoice_No)Invoice_No , MAX(Against_PO)Against_PO, MAX(PO_No)PO_No , max(PO_Date)PO_Date , max(SRN_No)SRN_No , max(SRN_Date)SRN_Date , MAX(Vendor_Invoice_No)Vendor_Invoice_No, MAX(Invoice_Date)Invoice_Date , MAX(status)AS status ,
            max(Posting_Date)Posting_Date , sum(PJV_Amount)PJV_Amount , sum(PJV_TDS_Amount)PJV_TDS_Amount , sum(PJV_Net_Amount)PJV_Net_Amount , max(Narration)Narration , max(Created_By)Created_By, max(Line_No)Line_No,GL_Account_Code , max(GL_Account_Desc)GL_Account_Desc , sum(Expr1)Expr1 , sum(Amount)Amount , max(Bill_To_Location)Bill_To_Location , max(Comp_Name)Comp_Name , max(Add1)Add1 , max(Add2)Add2 , max(Add3)Add3  , max(State)State , max(CST_LST)CST_LST , max(Comp_Code)Comp_Code, max(Expr2)Expr2 , max(Expr3)Expr3 ,max(Expr4)Expr4,max(Expr5)Expr5 , max(City_Code_Desc)City_Code_Desc , max(Due_Date)Due_Date , max(Vendor_Invoice_Date)Vendor_Invoice_Date, max(Comments)Comments , 
            max(Dept_Desc)Dept_Desc , max(TransporterDesc)TransporterDesc,max(VehicleDesc)VehicleDesc, max(TapalNo)TapalNo , max(DateAndTime)DateAndTime  FROM (
               select aa.* , isnull (TBL_PUrchaseSNR.RefrencePO,'') as RefrencePO from (" + qry + ")  aa  left outer join (select  SRN_No, max(RefrencePO) as RefrencePO from (  SELECT  TSPL_SRN_DETAIL2.SRN_No , STUFF((SELECT distinct ',' + QUOTENAME(TSPL_PURCHASE_ORDER_HEAD.ReferencePO) as RefrencePO   from TSPL_SRN_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on  TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No =TSPL_SRN_DETAIL.PO_ID and len(isnull(TSPL_SRN_DETAIL.PO_ID,'')) >0  where TSPL_SRN_DETAIL2.SRN_No = TSPL_SRN_DETAIL.SRN_No and len(isnull (ReferencePO,'')) > 0  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  as RefrencePO
            from TSPL_SRN_DETAIL  as TSPL_SRN_DETAIL2  where len(isnull(TSPL_SRN_DETAIL2.PO_ID,'')) >0 )xx group by SRN_No )  TBL_PUrchaseSNR on TBL_PUrchaseSNR.SRN_No = aa.SRN_No  ) XX group by  XX.GL_Account_Code ) xxx where (Amount) <> 0 order by PJVGroup"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1)
            Dim frmCRV As New frmCrystalReportViewer()
            'frmCRV.funsubreport(CrystalReportFolder.PurchaseOrder, qry, qry1, "rptPJV-V", "PJV Report", "PurchaseDetails1.rpt", clsCommon.myCDate(txtDate.Value), "rptCompanyAddress.rpt", "SubRptCmpnyMasterForERODE.rpt", clsERPFuncationality.CompanyAddresShowinHeaderPartForERODE())

            frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, dt1, "rptPJV-V", "PJV Report", clsCommon.myCDate(clsCommon.GETSERVERDATE()), "PurchaseDetails1.rpt", "SubRptCmpnyMasterForERODE.rpt", clsERPFuncationality.CompanyAddresShowinHeaderPartForERODE())
            frmCRV = Nothing


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)

        If is_Load_MRN And Not isAgainstTender Then
            dblQty = dblQty + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colShortQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRejectQty).Value)
        End If
        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblAmt As Double = dblQty * dblRate
        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colIsMannualAmt).Value) = 0 Then
            gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        ElseIf clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(colIsInsurance).Value) AndAlso clsCommon.myLen(gv1.Rows(IntRowNo).Cells(colSRNNo).Value) <= 0 Then
            dblAmt = (clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInsuranceBaseAmt).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInsurancePer).Value)) / 100
            gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        Else
            dblAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmt).Value)
        End If

        Dim dblHeaderDisAmt As Decimal = Math.Round(dblAmt * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colHeaderDiscountPer).Value) / 100, 2, MidpointRounding.AwayFromZero)
        Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPer).Value)
        Dim dbldisperunit As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPerUnit).Value)
        Dim dbldisamtperunit As Decimal = (dblQty * dbldisperunit)
        ''richa agarwal 16 Aug,2019 ERO/09/08/19-000990
        Dim dblDetailDisAmt As Decimal
        If dblDisPer = 0 AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDetailDisAmt).Value) > 0 Then
            dblDetailDisAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDetailDisAmt).Value)
        Else
            dblDetailDisAmt = (dblAmt * dblDisPer) / 100
        End If

        Dim dblDisAmt As Double = dblHeaderDisAmt + dblDetailDisAmt + dbldisamtperunit
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
                        If clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmountPurchase Then
                            Dim dblTotalBasicPrice As Double = 0
                            For n As Integer = 0 To gv1.Rows.Count - 1
                                If clsCommon.myLen(gv1.Rows(n).Cells(colICode).Value) > 0 Then
                                    dblTotalBasicPrice = dblTotalBasicPrice + clsCommon.myCdbl(gv1.Rows(n).Cells(colAmt).Value)
                                End If
                            Next
                            dblBaseAmt = (clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmt).Value) * clsCommon.myCdbl(txttcstaxbaseamount.Value)) / dblTotalBasicPrice
                        ElseIf IsExciseType And IsAbatementPO Then
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
        gv1.Rows(IntRowNo).Cells(colDisAmtPerUnit).Value = Math.Round(dbldisamtperunit, 2)

        gv1.Rows(IntRowNo).Cells(colItemInsuranceBaseAmt).Value = Math.Round(dclItemInsuranceBaseAmt, 2)
        gv1.Rows(IntRowNo).Cells(colItemInsuranceAmt).Value = Math.Round(dclItemInsuranceAmt, 2)
        gv1.Rows(IntRowNo).Cells(colItemAmtAfterInsurance).Value = Math.Round(dclItemAmtAfterInsurance, 2)

        gv1.Rows(IntRowNo).Cells(colTaxableAmount).Value = Math.Round(dblCurrentTaxableAmount, 2)
        gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)

        Dim dblTQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRejectQty).Value)
        ''richa agarwal 16 Aug,2019 ERO/09/08/19-000990
        If dblDisPer = 0 AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDetailDisAmt).Value) > 0 Then
            gv1.Rows(IntRowNo).Cells(colAmtLessDiscountWithoutShortage).Value = Math.Round((dblTQty * dblRate) - clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDetailDisAmt).Value), 2)
        Else
            gv1.Rows(IntRowNo).Cells(colAmtLessDiscountWithoutShortage).Value = Math.Round((dblTQty * dblRate) * (100 - dblDisPer) / 100, 2)
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rbtnTaxCalAutomatic_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnTaxCalManual.ToggleStateChanged, rbtnTaxCalAutomatic.ToggleStateChanged
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
    Private Sub gv2_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv2.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv2.Columns(colTTaxAmt)) Then
                    gv2.CurrentRow.Cells(colTTaxAmt).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                ElseIf (e.Column Is gv2.Columns(colTTaxRate)) Then
                    gv2.CurrentRow.Cells(colTTaxRate).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                End If

                'Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                'cell.GradientStyle = GradientStyles.Solid
                'cell.BackColor = Color.FromArgb(243, 181, 51)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub
    Private Sub gv1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F7 Then
            If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                gv1.CurrentRow.Cells(colIsMannualAmt).Value = IIf(clsCommon.myCdbl(gv1.CurrentRow.Cells(colIsMannualAmt).Value) = 1, 0, 1)
            End If

            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colIsMannualAmt).Value) = 0 Then
                UpdateCurrentRow(gv1.CurrentRow.Index)
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
    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            Dim dtAcquisition As DataTable = clsDBFuncationality.GetDataTable("SELECT Acquisition_Code FROM TSPL_ACQUISITION_HEAD WHERE PI_No='" + txtDocNo.Value + "'")
            Dim TempAcqNO As String = ""
            If dtAcquisition IsNot Nothing AndAlso dtAcquisition.Rows.Count > 0 Then
                TempAcqNO = "Purchase Invoice - " + txtDocNo.Value + " is used in following Acquisition Entry -"
                For Each drAcq As DataRow In dtAcquisition.Rows
                    TempAcqNO += Environment.NewLine + clsCommon.myCstr(drAcq("Acquisition_Code"))
                Next
                clsCommon.MyMessageBoxShow(TempAcqNO, Me.Text)
                Exit Sub
            End If
            Dim qry As String = "select TSPL_PI_HEAD.PI_No,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_PJV_HEAD.PJV_No from TSPL_PI_HEAD "
            qry += "  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No"
            qry += "  left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_JOURNAL_MASTER.Source_Code in ('AP-IN','AP-DN','PI-CM')"
            qry += "  left outer join TSPL_PJV_HEAD on TSPL_PJV_HEAD.Invoice_No=TSPL_PI_HEAD.PI_No "
            qry += "  where PI_No='" + txtDocNo.Value + "' and TSPL_PI_HEAD.Status=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.MyMessageBoxShow("Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    '' reason for reverse
                    Dim Reason As String = ""
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Reverse"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If

                    'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    'Try
                    '    For Each dr As DataRow In dt.Rows
                    '        ''Delete AP Journal Entry 
                    '        Dim docNo As String = clsCommon.myCstr(dr("Voucher_No"))
                    '        qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No  in ('" + docNo + "')"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '        qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No in ('" + docNo + "')"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)


                    '        ''Delete AP Invoice ( changes by richa AGARWAL use Purchase Invoice No. in place of vendor Invoice no 08/07/2015)

                    '        docNo = clsCommon.myCstr(dr("Document_No"))
                    '        qry = "select TSPL_PAYMENT_DETAIL.Payment_No from TSPL_PAYMENT_DETAIL inner Join TSPL_PAYMENT_hEADER on TSPL_PAYMENT_DETAIL.Payment_no=TSPL_PAYMENT_hEADER.Payment_no where TSPL_PAYMENT_DETAIL.Document_No in ('" + docNo + "') AND ISNULL(TSPL_PAYMENT_hEADER.iscHKrEVERSE,'')<>'Y' "
                    '        Dim dtAP As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    '        If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                    '            qry = "AP-Invoice " + docNo + " is used in following Payment -"
                    '            For Each drAP As DataRow In dtAP.Rows
                    '                qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                    '            Next
                    '            Throw New Exception(qry)
                    '        End If

                    '        ''richa BHA/04/09/18-000505
                    '        docNo = clsCommon.myCstr(dr("PI_No"))
                    '        qry = "Delete from TSPL_REMITTANCE WHERE Document_No in (select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_POInvoice_No in ('" + docNo + "'))"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)


                    '        'qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No='" + docNo + "'"
                    '        'clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '        'qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Document_No='" + docNo + "'"
                    '        'clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '        ''''''''''''''''''
                    '        qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No in (select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_POInvoice_No in ('" + docNo + "'))"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    '        qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Against_POInvoice_No in ('" + docNo + "')"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '        qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No =(select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_POInvoice_No in ('" + docNo + "')))"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    '        qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_POInvoice_No in ('" + docNo + "'))"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '        qry = "delete from TSPL_PR_DETAIL where PR_No in (select PR_No from TSPL_PR_HEAD where Auto_Gen_Againnt_PI_No in ('" + docNo + "'))"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    '        qry = "delete from TSPL_PR_HEAD where Auto_Gen_Againnt_PI_No in ('" + docNo + "')"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '        qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No in (select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_PurchaseReturn_No=(select PR_No from TSPL_PR_HEAD where Auto_Gen_Againnt_PI_No in ('" + docNo + "')))"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    '        qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Against_PurchaseReturn_No in (select PR_No from TSPL_PR_HEAD where Auto_Gen_Againnt_PI_No in ('" + docNo + "'))"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '        qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No=(select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_PurchaseReturn_No=(select PR_No from TSPL_PR_HEAD where Auto_Gen_Againnt_PI_No in ('" + docNo + "'))))"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    '        qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No in  (select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_PurchaseReturn_No=(select PR_No from TSPL_PR_HEAD where Auto_Gen_Againnt_PI_No in ('" + docNo + "')))"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '        qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PI-CM' and Source_Doc_No in ('" + docNo + "'))"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    '        qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='PI-CM' and Source_Doc_No in ('" + docNo + "')"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '        '==========BM00000008086
                    '        qry = "delete from TSPL_Inventory_Movement where Trans_Type='IC-AD' and Source_Doc_No in ( select Adjustment_No  from TSPL_ADJUSTMENT_HEADER where Against_PI_No_Difference in ('" + docNo + "'))"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '        qry = "delete from TSPL_ADJUSTMENT_DETAIL where  TSPL_ADJUSTMENT_DETAIL.Adjustment_No in ( select Adjustment_No  from TSPL_ADJUSTMENT_HEADER where Against_PI_No_Difference in ('" + docNo + "'))"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    '        qry = "delete from TSPL_ADJUSTMENT_HEADER where Against_PI_No_Difference in ('" + docNo + "')"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '        ''''''''''''''''''
                    '        ''Change status to unpost of PJV
                    '        docNo = clsCommon.myCstr(dr("PJV_No"))
                    '        qry = "update TSPL_PJV_HEAD set Status=0,Posting_Date=null where PJV_No in ('" + docNo + "')"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '        ''Change status to unpost
                    '        docNo = clsCommon.myCstr(dr("PI_No"))
                    '        qry = "update TSPL_PI_HEAD set Status=0 where PI_No in ('" + docNo + "')"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '        qry = "Delete from TSPL_PO_ADVANCE_ADJUSTMENT_KNOCKOFF where PI_No='" + docNo + "'"
                    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    '    Next
                    If clsPurchaseInvoiceHead.ReverseAndUnpost(txtDocNo.Value) Then
                        saveCancelLog(Reason, "Reverse and Recreate", Nothing)
                        'trans.Commit()
                        clsCommon.MyMessageBoxShow(Me, "Task done Successfully", Me.Text)
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If

                    'Catch ex As Exception
                    '    trans.Rollback()
                    '    Throw New Exception(ex.Message)
                    'End Try
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
    Private Sub gv2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.Click

    End Sub
    Private Sub txtCurrencyCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCurrencyCode._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        txtCurrencyCode.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", txtCurrencyCode.Value, "CURRENCY_CODE", isButtonClicked)
        ShowCurrencyDetail()
    End Sub
    Private Sub chkAgainstForm_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAgainstForm.ToggleStateChanged
        ddlForm.Enabled = chkAgainstForm.Checked
        If chkAgainstForm.Checked Then
            chkAgainstCForm.Checked = False
        End If
    End Sub
    Private Sub txtVehicleNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtVehicleNo._MYValidating
        qry = "Select Vehicle_Id as Code, Number, Description from TSPL_VEHICLE_MASTER"
        txtVehicleNo.Value = clsCommon.ShowSelectForm("Vehicle@PI", qry, "Code", "", txtVehicleNo.Value, "Code", isButtonClicked)
        lblVehicle.Text = GetVehicleNo(txtVehicleNo.Value, Nothing)
    End Sub
    Private Function GetVehicleNo(ByVal strVehicleId As String, ByVal trans As SqlTransaction) As String
        Try
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Number from TSPL_VEHICLE_MASTER WHERE Vehicle_Id='" + strVehicleId + "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Private Sub txtTransporter__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtTransporter._MYValidating
        qry = "Select Transport_Id as Id, Transporter_Name as Name, ISNULL(Add1,'')+ISNULL(Add2,'') as Address  from TSPL_TRANSPORT_MASTER"
        txtTransporter.Value = clsCommon.ShowSelectForm("Transporter@PI", qry, "Id", "", txtTransporter.Value, "Id", isButtonClicked)
        LoadTransporterData(txtTransporter.Value)
    End Sub
    Private Sub LoadTransporterData(ByVal strTransporterId As String)
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Transporter_Name as Name, ISNULL(Add1,'')+ISNULL(Add2,'') as Address  from TSPL_TRANSPORT_MASTER WHERE Transport_Id='" + strTransporterId + "'")
            If dt.Rows.Count > 0 Then
                lblTransporterName.Text = clsCommon.myCstr(dt.Rows(0)("Name"))
                lblTransporterAddress.Text = clsCommon.myCstr(dt.Rows(0)("Address"))
            Else
                lblTransporterName.Text = ""
                lblTransporterAddress.Text = ""
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub LoadAgainstForm()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Form_Code as Code, Form_Name as Name from TSPL_Form_Master")
        If dt.Rows.Count > 0 Then
            ddlForm.DataSource = dt
            ddlForm.ValueMember = "Code"
            ddlForm.DisplayMember = "Name"
        Else
            chkAgainstForm.Enabled = False
            chkAgainstForm.Checked = False
        End If
    End Sub
    Private Sub DeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
    End Sub
    Private Sub SaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveLayoutrb.Click
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.mbtnPurchaseInvoice
        frm.ShowDialog()
    End Sub
    Private Sub btnsend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsend.Click
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
            Dim lstUsers As New List(Of String)
            lstUsers.Add(txtVendorNo.Value)
            SendSMSandEmail(lstUsers, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSendForApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendForApproval.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
                txtReqNo.Focus()
                txtReqNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow(Me, "Send E-Mail/SMS Of Respective Sale Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
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
    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)

        Try
            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.mbtnPurchaseInvoice)

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
            'clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
            ''Catch ex As Exception
            ''    Throw New Exception(ex.Message)
            ''End Try

            ''Try

            'Ticket No-TEC/30/07/19-000972 sanjay
            Dim strContactPerson As String = ""

            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnPurchaseInvoice + "'", Nothing)
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
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Form_Code, clsUserMgtCode.mbtnPurchaseInvoice)


                    For Each strUser As String In lstUsers
                        'lstUsers.Add(dr("User_Code").ToString())
                        'Dim lstReceiptents As New List(Of String)
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

                        'lstReceiptents.Add(emailId)
                        objEmailH.arrEMail.Add(clsCommon.myCstr(emailId))


                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ContactPerson, strContactPerson)
                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.UserCode, strUser)

                    Next


                    objEmailH.SaveData(clsUserMgtCode.mbtnPurchaseInvoice, objEmailH, Nothing)
                    objEmailH = Nothing
                    clsCommon.MyMessageBoxShow(Me, "E-Mail Send Successfully", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "First do email and sms setting", Me.Text)

            End If

            If Not clsSMSAtPost_Purchase.SMSATPOST_PUR() Then
                SMSSENDONLY(False)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub SMSSENDONLY(ByVal isPost As Boolean)
        Try
            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.mbtnPurchaseInvoice)

            'If obj Is Nothing Then
            '    clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
            '    Return
            'End If
            'If clsCommon.myLen(obj.smsbody) <= 0 Then
            '    Return
            'End If

            ''strMes = "Dear  " & strCustomer & "  your order No " & txtDocNo.Value & "  dated  " & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "has been booked with amount" & lblTotRAmt.Text

            'Dim strMes As String = obj.smsbody
            'strMes = strMes.Replace("'", " ").Replace("`", "/")

            'If strMes.Contains(clsEmailSMSConstants.SaleOrderNo) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.SaleOrderDate) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
            'End If
            'If strMes.Contains(clsEmailSMSConstants.VendorNo) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.VendorNo, txtVendorNo.Value)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.VendorName) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.ContactPerson) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.ContactPerson, "")
            'End If
            'If strMes.Contains(clsEmailSMSConstants.TotalAmount) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
            'End If


            'Dim strphone As String = clsDBFuncationality.getSingleValue("select Phone1 from tspl_vendor_master where vendor_code ='" & txtVendorNo.Value & "' ")

            'If clsSMSSend.SendSMS(clsUserMgtCode.mbtnPurchaseInvoice, strMes, strphone) Then
            '    If Not isPost Then
            '        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
            '    End If
            'End If

            'sanjay
            Dim strContactPerson As String = ""
            Dim strotherno As String = Nothing
            strotherno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Phone1 from tspl_vendor_master where vendor_code ='" & txtVendorNo.Value & "' "))

            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnPurchaseInvoice + "'", Nothing)
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
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Form_Code, clsUserMgtCode.mbtnPurchaseInvoice)
                End If



                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                    objSMSH.SaveData(clsUserMgtCode.mbtnPurchaseInvoice, objSMSH, Nothing)
                    objSMSH = Nothing
                    clsCommon.MyMessageBoxShow(Me, "SMS Send Successfully", Me.Text)
                End If
            End If
            'Sanjay

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub BtnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHistory.Click
        Dim frm As New FrmPurchaseHistory
        frm.SetUserMgmt(clsUserMgtCode.FrmPurchaseHistory)
        frm.strFormId = MyBase.Form_ID
        frm.strVendorCode = txtVendorNo.Value
        frm.strVendorName = lblVendorName.Text
        Dim strvendor As String = txtVendorNo.Value
        frm.ShowDialog()
        frm.WindowState = FormWindowState.Maximized

    End Sub
    Private Sub chkAgainstCForm_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAgainstCForm.ToggleStateChanged
        If chkAgainstCForm.Checked Then
            chkAgainstForm.Checked = False
        End If
    End Sub
    Private Sub print()
        Throw New NotImplementedException
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim qry As String = ""
        Dim dt As New DataTable()
        Try
            qry = "select * from TEMP_DELETE_PI where Doc_No not in (select Doc_No from TEMP_CREATE_PI_TRANS)"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim strErro As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If common.clsCommon.MyMessageBoxShow("Update New Colums of  " + clsCommon.myCstr(dt.Rows.Count) + " Purchase Invoice", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    clsCommon.ProgressBarPercentShow()
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        Dim strDocNo As String = clsCommon.myCstr(dt.Rows(ii)("Doc_No"))
                        LoadData(strDocNo, NavigatorType.Current)
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRow(jj)
                        Next
                        UpdateAllTotals()
                        CalLandAmt()
                        CalNonRectax()
                        CalRectax()
                        CalAddtionalAmt()

                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Try
                            Dim coll As New Hashtable()
                            For Each grow As GridViewRowInfo In gv1.Rows
                                coll = New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "Landed_Cost_Rate", clsCommon.myCdbl(grow.Cells(colLandedRate).Value))
                                clsCommon.AddColumnsForChange(coll, "Landed_Cost_Amount", clsCommon.myCdbl(grow.Cells(colLandedAmt).Value))
                                clsCommon.AddColumnsForChange(coll, "Accepted_Amount", clsCommon.myCdbl(grow.Cells(colAcceptedAmount).Value))
                                clsCommon.AddColumnsForChange(coll, "Rejected_Amount", clsCommon.myCdbl(grow.Cells(colRejectedAmount).Value))
                                clsCommon.AddColumnsForChange(coll, "Shortage_Amount", clsCommon.myCdbl(grow.Cells(colShortageAmount).Value))
                                clsCommon.AddColumnsForChange(coll, "Leak_Amount", clsCommon.myCdbl(grow.Cells(colLeakAmount).Value))
                                clsCommon.AddColumnsForChange(coll, "Burst_Amount", clsCommon.myCdbl(grow.Cells(colBurstAmount).Value))
                                clsCommon.AddColumnsForChange(coll, "Amt_Less_Discount_Without_Shortage", clsCommon.myCdbl(grow.Cells(colAmtLessDiscountWithoutShortage).Value))
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PI_DETAIL", OMInsertOrUpdate.Update, "PI_No='" + strDocNo + "' and Line_No='" + clsCommon.myCstr(grow.Cells(colLineNo).Value) + "'", trans)
                            Next
                            coll = New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Total_Accepted_Amount", clsCommon.myCdbl(lblAcceptedAmt.Text))
                            clsCommon.AddColumnsForChange(coll, "Total_Rejected_Amount", clsCommon.myCdbl(lblRejectedAmt.Text))
                            clsCommon.AddColumnsForChange(coll, "Total_Shortage_Amount", clsCommon.myCdbl(lblShortageAmt.Text))
                            clsCommon.AddColumnsForChange(coll, "Total_Leak_Amount", clsCommon.myCdbl(lblLeakAmt.Text))
                            clsCommon.AddColumnsForChange(coll, "Total_Burst_Amount", clsCommon.myCdbl(lblBurstAmt.Text))
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PI_HEAD", OMInsertOrUpdate.Update, "PI_No='" + strDocNo + "'", trans)

                            coll = New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Doc_No", strDocNo)
                            clsCommonFunctionality.UpdateDataTable(coll, "TEMP_CREATE_PI_TRANS", OMInsertOrUpdate.Insert, "", trans)

                            trans.Commit()
                        Catch ex As Exception
                            trans.Rollback()
                            strErro += "PI No - " + strDocNo + ", Exception -" + ex.Message + Environment.NewLine
                        End Try
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
    Public Function GetQueryForTaxInvoice(Optional ByVal strDocNo As String = Nothing) As String

        Dim Qry As String = "select isnull(TSPL_PI_HEAD.Purchase_Tax_Invoice_Type,'') as Purchase_Tax_Invoice_Type, isnull (Ship_to_Location.City_Code,'') as Ship_To_City,isnull(Location_Ship_State.STATE_NAME,'') as Ship_to_State_Name,isnull(Bill_Location.City_Code,'') as Bill_To_City,isnull(bill_Location_State.STATE_NAME,0) as Bill_To_State_Name, '1' as CopyType,Vendor_State.is_GST_UT as Vendor_IS_GST_UT,bill_Location_State.is_GST_UT as Bill_IS_GST_UT,Location_Ship_State.is_GST_UT as Ship_IS_GST_UT," &
" Ship_to_Location.add1 as Ship_Add1,Ship_to_Location.add2 as Ship_Add2,Ship_to_Location.add3 as Ship_Add3,Ship_to_Location.gstno as Ship_GSTNo,Location_Ship_State.GST_STATE_Code as Ship_GST_StateCode,isnull (TSPL_PI_head.Purchase_Tax_Invoice,'') as InvoiceNo, convert (varchar,TSPL_PI_HEAD.Posting_Date,103) as PI_Date ,Bill_Location .Add1 as Loc_Add1,Bill_Location.Add2 as Loc_ADd2,Bill_Location.Add3  as Loc_Add3," &
                    " bill_Location_State.gst_state_code as Loc_GST_StateCode,Bill_Location.gstno as LocGstNo,TSPL_PI_HEAD.Vendor_Name,TSPL_VENDOR_MASTER.Add1 as Ven_Add1,TSPL_VENDOR_MASTER.Add2 as Ven_Add2,TSPL_VENDOR_MASTER.Add3 as Ven_Add3,Vendor_State.GST_STATE_Code as Vendor_GST_State_Code,TSPL_VENDOR_MASTER.GSTFinalNo as Vendor_GST_No" &
" ,TSPL_VENDOR_MASTER.PAN as Ven_PAN_no,TSPL_PI_HEAD.Purchase_Tax_Invoice ,TSPL_PI_HEAD.grno,TSPL_PI_HEAD.GR_Date,tspl_item_master.Item_Code ,tspl_item_master.Item_Desc " &
" ,tspl_item_master.HSN_Code ,TSPL_PI_detail.PI_Qty as qty ,TSPL_PI_detail.Item_Cost as itemcost  ,TSPL_PI_detail.Amount ,TSPL_PI_detail.Disc_Amt ,TSPL_PI_detail.Amt_Less_Discount ," &
" TSPL_PI_HEAD.Add_Charge_Amt1 ,TSPL_PI_HEAD.Add_Charge_Amt2 ,TSPL_PI_HEAD.Add_Charge_Amt3 ,TSPL_PI_HEAD.Add_Charge_Amt4 ,TSPL_PI_HEAD.Add_Charge_Amt5 ,TSPL_PI_HEAD.Add_Charge_Amt6 ,TSPL_PI_HEAD.Add_Charge_Amt7 ,TSPL_PI_HEAD.Add_Charge_Amt8 ,TSPL_PI_HEAD.Add_Charge_Amt9 ,TSPL_PI_HEAD.Add_Charge_Amt10 ,TSPL_PI_HEAD.Add_Charge_Name1,TSPL_PI_HEAD.Add_Charge_Name2 ,TSPL_PI_HEAD.Add_Charge_Name3 ,TSPL_PI_HEAD.Add_Charge_Name4 ,TSPL_PI_HEAD.Add_Charge_Name5 ,TSPL_PI_HEAD.Add_Charge_Name6 ,TSPL_PI_HEAD.Add_Charge_Name7 ,TSPL_PI_HEAD.Add_Charge_Name8,TSPL_PI_HEAD.Add_Charge_Name9,TSPL_PI_HEAD.Add_Charge_Name10, " &
" TSPL_PI_detail.TAX1 as dTAX1, TSPL_PI_detail.TAX2 as dTAX2, TSPL_PI_detail.TAX3 as  dTAX3, TSPL_PI_detail.TAX4 as  dTAX4, TSPL_PI_detail.TAX5 as  dTAX5, TSPL_PI_detail.TAX6 as  dTAX6, TSPL_PI_detail.TAX7 as  dTAX7, TSPL_PI_detail.TAX8 as dTAX8, TSPL_PI_detail.TAX9 as dTAX9, TSPL_PI_detail.TAX10 as  dTAX10, TSPL_PI_detail.TAX1_Amt, TSPL_PI_detail.TAX2_Amt, TSPL_PI_detail.TAX3_Amt, TSPL_PI_detail.TAX4_Amt, TSPL_PI_detail.TAX5_Amt, TSPL_PI_detail.TAX6_Amt, TSPL_PI_detail.TAX7_Amt, TSPL_PI_detail.TAX8_Amt, TSPL_PI_detail.TAX9_Amt, TSPL_PI_detail.TAX10_Amt,  TSPL_PI_detail.TAX1_Rate as dTAX1_Rate, TSPL_PI_detail.TAX2_Rate as dTAX2_Rate, TSPL_PI_detail.TAX3_Rate as dTAX3_Rate ,TSPL_PI_detail.TAX4_Rate as dTAX4_Rate ,TSPL_PI_detail.TAX5_Rate as dTAX5_Rate  ,TSPL_PI_detail.TAX6_Rate as dTAX6_Rate ,TSPL_PI_detail.TAX7_Rate as dTAX7_Rate ,TSPL_PI_detail.TAX8_Rate as dTAX8_Rate ,TSPL_PI_detail.TAX9_Rate as dTAX9_Rate ,TSPL_PI_detail.TAX10_Rate as dTAX10_Rate,  dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type" &
" ,TSPL_PI_HEAD.Vehicle_Id ,TSPL_PI_HEAD.VehicleNo ,TSPL_PI_HEAD.Transporter,TSPL_PI_HEAD.Terms_Code ,TSPL_PI_HEAD.Against_PO, convert ( varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as PurchaseOrder_Date , TSPL_PI_detail.Unit_code  " &
       " from TSPL_PI_HEAD " &
" left join TSPL_PI_detail on TSPL_PI_detail.pi_no=TSPL_PI_HEAD.pi_no " &
" left join tspl_item_master on tspl_item_master.item_code=TSPL_PI_detail.item_code" &
" left join tspl_location_master as Bill_Location on Bill_Location.location_code=TSPL_PI_HEAD.bill_to_location" &
" left join tspl_state_master as bill_Location_State on bill_Location_State.STATE_CODE =Bill_Location.State " &
" left join tspl_location_master as Ship_to_Location on Ship_to_Location.location_code=TSPL_PI_HEAD.ship_To_location" &
" left join tspl_state_master as Location_Ship_State on Location_Ship_State.STATE_CODE =Ship_to_Location.State " &
" left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_PI_HEAD.vendor_code" &
" left join tspl_state_master as Vendor_State on Vendor_State.STATE_CODE =TSPL_VENDOR_MASTER.State_Code " &
" left join TSPL_COMPANY_MASTER on tspl_company_master.comp_code=TSPL_PI_HEAD.Comp_Code left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PI_HEAD.Against_PO = TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " &
" left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_PI_detail .tax1 " &
 " left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_PI_detail.tax2  " &
 "  left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_PI_detail .TAX3   " &
  " left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_PI_detail .tax4   " &
  " left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_PI_detail .tax5  " &
  "  left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_PI_detail .TAX6  " &
    "  left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_PI_detail .TAX7   " &
     "  left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_PI_detail .TAX8  " &
      " left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_PI_detail .TAX9   " &
     "   left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_PI_detail .TAX10 " &
    " where TSPL_PI_HEAD.PI_No ='" + strDocNo + "'"
        Dim QryCopy As String = " Select * from (" & Qry & ") XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 4 as COL2,  'QUADRUPLICATE COPY' as CopyType1) YYY ON YYY.COL1=XXX.CopyType  ORDER BY YYY.COL2"


        Return QryCopy
    End Function
    Public Sub funPrint(ByVal strDocNo As String)
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            Dim strReportType As String = Nothing
            Dim strLocState As String = Nothing
            Dim strShipLocState As String = Nothing
            Dim strVendState As String = Nothing
            Dim strLocCode As String = Nothing
            Dim strShipLocCode As String = Nothing
            Dim strVendorCode As String = Nothing
            Dim IsTaxable As Double = 0
            Dim IsMandiTax As Double = 0
            Dim IsEXEMPTED As Double = 0
            Dim dtDocdate As Date?
            dtDocdate = Nothing
            Dim strTaxGroup As String = Nothing
            Dim StrSql = "select bill_to_location,ship_to_location,vendor_code,tax_group,pi_date   from TSPL_PI_head where pi_no='" & strDocNo & "'"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(StrSql)
            If dt1.Rows.Count > 0 Then
                strLocCode = clsCommon.myCstr(dt1.Rows(0)("bill_to_location"))
                strVendorCode = clsCommon.myCstr(dt1.Rows(0)("vendor_code"))
                ' IsTaxable = clsCommon.myCdbl(dt1.Rows(0)("is_taxable"))
                dtDocdate = clsCommon.myCDate(dt1.Rows(0)("pi_date"))
                strTaxGroup = clsCommon.myCstr(dt1.Rows(0)("tax_group"))
                strShipLocCode = clsCommon.myCstr(dt1.Rows(0)("ship_to_location"))
            End If
            IsMandiTax = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" & strTaxGroup & "' and Tax_Code in(select Tax_Code from TSPL_TAX_MASTER where Is_Mandi_Tax='Y')"))
            strLocState = clsDBFuncationality.getSingleValue("Select TSPL_LOCATION_MASTER.State from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Location_Code = '" + strLocCode + "'")
            strShipLocState = clsDBFuncationality.getSingleValue("Select TSPL_LOCATION_MASTER.State from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Location_Code = '" + strShipLocCode + "'")
            strVendState = clsDBFuncationality.getSingleValue("Select State_Code from TSPL_VENDOR_MASTER where Vendor_Code='" + strVendorCode + "'")
            ' IsEXEMPTED = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" + strTaxGroup + "' and  Is_Tax_Exempted=1 and Tax_Group_Type='P'")
            If clsCommon.CompairString(strTaxGroup.ToUpper(), "EXEMPTED") = CompairStringResult.Equal Then   ' IsEXEMPTED > 0
                strReportType = "NT"
            ElseIf clsCommon.CompairString(strLocState, strVendState) = CompairStringResult.Equal Then
                strReportType = "L"
            ElseIf clsCommon.CompairString(strShipLocState, strVendState) = CompairStringResult.Equal Then
                strReportType = "L"
            Else
                strReportType = "I"
            End If

            Dim Qry As String = GetQueryForTaxInvoice(strDocNo)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dt.Rows(0)("PI_Date"))) Then
                    If clsCommon.CompairString(strReportType, "L") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCdbl(dt.Rows(0)("Ship_IS_GST_UT")), 1) = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCdbl(dt.Rows(0)("Bill_IS_GST_UT")), 1) = CompairStringResult.Equal Then
                            frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptPurchaseInvoice_UT", "Tax Invoice For UT", clsCommon.myCDate(dt.Rows(0)("PI_Date")), "rptCompanyAddress.rpt", "MMM.rpt")

                        Else
                            If IsMandiTax > 0 Then
                                frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptPurchaseInvoice_WithMandiTax", "Tax Invoice with MandiTax", clsCommon.myCDate(dt.Rows(0)("PI_Date")), "rptCompanyAddress.rpt", "MMM.rpt")
                            Else
                                frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptPurchaseInvoice", "Tax Invoice", clsCommon.myCDate(dt.Rows(0)("PI_Date")), "rptCompanyAddress.rpt", "MMM.rpt")
                            End If

                        End If
                    ElseIf clsCommon.CompairString(strReportType, "I") = CompairStringResult.Equal Then
                        If IsMandiTax > 0 Then
                            frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptPurchaseInvoice_InterstateWithMandiTax", "Tax Invoice With Mandi Tax", clsCommon.myCDate(dt.Rows(0)("PI_Date")), "rptCompanyAddress.rpt", "MMM.rpt")
                        Else
                            frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptPurchaseInvoice_Interstate", "Tax Invoice", clsCommon.myCDate(dt.Rows(0)("PI_Date")), "rptCompanyAddress.rpt", "MMM.rpt")
                        End If
                    ElseIf clsCommon.CompairString(strReportType, "NT") = CompairStringResult.Equal Then
                        frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptPurchaseInvoice _NonTaxable", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("PI_Date")), "rptCompanyAddress.rpt", "MMM.rpt")

                    End If
                Else
                    ' frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptPurchaseInvoice", "Retail Invoice", "rptCompanyAddress.rpt", "MMM.rpt")
                End If

            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
            End If

            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub btnPurchaseTaxInvoice_Click(sender As Object, e As EventArgs) Handles btnPurchaseTaxInvoice.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
        Else
            Dim isVendorRegister As Boolean = clsDBFuncationality.getSingleValue("select  GSTRegistered from TSPL_VENDOR_MASTER where vendor_code='" + txtVendorNo.Value + "' ")
            If isVendorRegister = False Then
                Dim strPurchaseTaxInvoiceNo As String = clsDBFuncationality.getSingleValue(" select isnull (Purchase_Tax_Invoice,'') from TSPL_PI_head where PI_No = '" + txtDocNo.Value + "'  ")
                If clsCommon.myLen(strPurchaseTaxInvoiceNo) > 0 Then
                    funPrint(txtDocNo.Value)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
            End If

        End If
    End Sub
    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeMisc
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub chkITCEligible_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkITCEligible.ToggleStateChanged
        Try
            If chkITCEligible.Checked = True Then
                RadGroupBox4.Enabled = True
            Else
                RadGroupBox4.Enabled = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub CboxITCType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles CboxITCType.SelectedIndexChanged
        Try
            LoadITC_Elibible()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
    Public Sub FillVendorDetails()
        lblRegisterOrUnregister.Text = clsVendorMaster.GetVendorRegisterORNonRegister(txtVendorNo.Value, Nothing)
        lblGstinNo.Text = clsVendorMaster.GetVendorGSTINNo(txtVendorNo.Value, Nothing)
    End Sub
    Private Sub txttcstaxbaseamount_Validating(sender As Object, e As ComponentModel.CancelEventArgs) Handles txttcstaxbaseamount.Validating
        Try
            If AllowtoChangeTCSBaseAmountPurchase Then
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    UpdateCurrentRow(ii)
                Next
                UpdateAllTotals()
            Else
                txttcstaxbaseamount.Value = 0
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If gv1.CurrentRow.Index >= 0 Then
                If e.Column Is gv1.Columns(colSRNNo) AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colSRNNo).Value)) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, clsCommon.myCstr(gv1.CurrentRow.Cells(colSRNNo).Value))
                ElseIf e.Column Is gv1.Columns(colPOID) AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colPOID).Value)) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseOrder, clsCommon.myCstr(gv1.CurrentRow.Cells(colPOID).Value))
                ElseIf e.Column Is gv1.Columns(colGRNID) AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colGRNID).Value)) > 0 Then
                    If objCommonVar.RCDFCFP = True Then
                        Dim Reason As String = ""
                        Dim frm1 As New FrmFreeTxtBox1
                        frm1.Text = "Enter new Invoice/Challan No"
                        frm1.ShowDialog()
                        If clsCommon.myLen(frm1.strRmks) <= 0 Then
                            Exit Sub
                        Else
                            Reason = frm1.strRmks
                        End If
                        clsDBFuncationality.ExecuteNonQuery("update tspl_grn_head set TSPL_GRN_HEAD.[Invoice/Challan_No]='" + Reason + "' where TSPL_GRN_HEAD.GRN_NO='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colGRNID).Value) + "'")
                        saveCancelLog(Reason, "Update", Nothing)

                    Else
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGRN, clsCommon.myCstr(gv1.CurrentRow.Cells(colGRNID).Value))
                    End If
                ElseIf e.Column Is gv1.Columns(colMRNID) AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colMRNID).Value)) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnMRN, clsCommon.myCstr(gv1.CurrentRow.Cells(colMRNID).Value))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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

    Sub CalculateTDS()
        Try
            Dim dblTdsAmt As Double = 0
            Dim qry As String = " Select Is_TDS_Applicable from TSPL_VENDOR_MASTER WHERE VENDOR_CODE='" + txtVendorNo.Value + "' "
            Dim TDS As Integer = clsDBFuncationality.getSingleValue(qry)

            If TDS = 1 Then
                'lblSecurityDeduction.Text = clsCommon.myFormat(dblSecurityDed)
                dblTdsAmt = Math.Round(clsCommon.myFormat(lblAmtAfterDiscount.Text) * 0.1 / 100, 0)
                lblTdsAmt.Text = dblTdsAmt
                'lblTdsAmt.Text = lblAmtAfterDiscount * 0.1 / 100
            Else
                lblTdsAmt.Text = clsCommon.myFormat(0)
            End If
        Catch ex As Exception

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

    Sub CalculateDeductionTotal(ByVal CalculateItemRow As Boolean)
        Dim dblSecurityDed As Double = 0
        Dim dblQualityDed As Double = 0
        Dim dblRMLate As Double = 0

        'For Each gvrow As GridViewRowInfo In gv1.Rows
        For Each row As GridViewRowInfo In gvDeduction.Rows
            ' Check if the row is not a new row and type column is not null
            If row.Cells("type").Value IsNot Nothing Then
                Dim type As String = clsCommon.myCstr(row.Cells("type").Value)

                ' Depending on the type, accumulate the amount
                Dim amount As Decimal = clsCommon.myCdbl(row.Cells("amount").Value)

                Select Case type
                    Case "Security Deduction"
                        dblSecurityDed += amount
                    Case "Quality Deduction"
                        dblQualityDed += amount
                    Case "RM Late Penalty"
                        dblRMLate += amount
                        ' Add more cases if there are additional types
                End Select
            End If
        Next

        'For ii As Integer = 0 To gvDeduction.Rows.Count - 1
        '    For jj As Integer = 0 To gvDeduction.Rows.Count - 1
        '        ' Check if the cell value is not Nothing before adding to the totals
        '        Dim securityCellValue As Object = gvDeduction.Rows(jj).Cells("Security Deduction" + clsCommon.myCstr(ii)).Value
        '        Dim qualityCellValue As Object = gvDeduction.Rows(jj).Cells("Quality Deduction" + clsCommon.myCstr(ii)).Value
        '        Dim rmLateCellValue As Object = gvDeduction.Rows(jj).Cells("RM Late Penalty" + clsCommon.myCstr(ii)).Value

        '        If securityCellValue IsNot Nothing Then
        '            dblSecurityDed += clsCommon.myCdbl(securityCellValue)
        '        End If

        '        If qualityCellValue IsNot Nothing Then
        '            dblQualityDed += clsCommon.myCdbl(qualityCellValue)
        '        End If

        '        If rmLateCellValue IsNot Nothing Then
        '            dblRMLate += clsCommon.myCdbl(rmLateCellValue)
        '        End If
        '    Next
        'Next

        ' Assign the totals to the labels outside of the loops
        'lblSecurityDeduction.Text = dblSecurityDed.ToString()
        'lblQualityDeduction.Text = dblQualityDed.ToString()
        'lblRMLate.Text = dblRMLate.ToString()

        lblSecurityDeduction.Text = clsCommon.myFormat(dblSecurityDed)
        lblQualityDeduction.Text = clsCommon.myFormat(dblQualityDed)
        lblRMLate.Text = clsCommon.myFormat(dblRMLate)

        If CalculateItemRow Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()
        End If

        'Dim dblSecurityDed As Double = 0
        'Dim dblQualityDed As Double = 0
        'Dim dblRMLate As Double = 0
        'For ii As Integer = 0 To gvDeduction.Rows.Count - 1
        '    For jj As Integer = 0 To gvDeduction.Rows.Count - 1
        '        dblSecurityDed += clsCommon.myCdbl(gvDeduction.Rows(jj).Cells("Security Deduction" + clsCommon.myCstr(ii)).Value)
        '        dblQualityDed += clsCommon.myCdbl(gvDeduction.Rows(jj).Cells("Quality Deduction" + clsCommon.myCstr(ii)).Value)
        '        dblRMLate += clsCommon.myCdbl(gvDeduction.Rows(jj).Cells("RM Late Penalty" + clsCommon.myCstr(ii)).Value)
        '    Next
        '    lblSecurityDeduction.Text = dblSecurityDed
        '    lblQualityDeduction.Text = dblQualityDed
        '    lblRMLate.Text = dblRMLate
        'Next
        'If CalculateItemRow Then
        '    For ii As Integer = 0 To gv1.Rows.Count - 1
        '        UpdateCurrentRow(ii)
        '    Next
        '    UpdateAllTotals()
        'End If
        ''Dim dblACAmount As Decimal = 0
        'For ii As Integer = 0 To gvACInsurance.Rows.Count - 1
        '    If (clsCommon.myLen(gvACInsurance.Rows(ii).Cells(colACInsuranceAmount).Value) > 0) Then
        '        dblACAmount += clsCommon.myCdbl(gvACInsurance.Rows(ii).Cells(colACInsuranceAmount).Value)
        '    End If
        'Next
        'lblAddChargesForInsurance.Text = clsCommon.myFormat(dblACAmount)
        'lblAddChargesForInsurance1.Text = clsCommon.myFormat(dblACAmount)
        'If CalculateItemRow Then
        '    For ii As Integer = 0 To gv1.Rows.Count - 1
        '        UpdateCurrentRow(ii)
        '    Next
        '    UpdateAllTotals()
        'End If
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

    Private Sub lblAmtAfterDiscount_TextChanged(sender As Object, e As EventArgs) Handles lblAmtAfterDiscount.TextChanged
        ActualTCSTaxBaseAmt()
    End Sub

    Sub ActualTCSTaxBaseAmt()
        Try
            If Not isInsideLoadData Then
                If ConsiderPreviousandCurrentFYForTCSTaxCustOutstanding = True Then
                    If clsCommon.myLen(txtTaxGroup.Value) > 0 Then
                        lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(lblAmtAfterDiscount.Text)
                        SetTaxDetails()
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txttcstaxbaseamount_TextChanged(sender As Object, e As EventArgs) Handles txttcstaxbaseamount.TextChanged
        Try
            If Not isInsideLoadData Then
                If AllowtoChangeTCSBaseAmountPurchase Then
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        UpdateCurrentRow(ii)
                    Next
                    UpdateAllTotals()
                Else
                    txttcstaxbaseamount.Value = 0
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub CancelPI()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Code is empty")
            End If

            Dim dtAcquisition As DataTable = clsDBFuncationality.GetDataTable("SELECT Acquisition_Code FROM TSPL_ACQUISITION_HEAD WHERE PI_No='" + txtDocNo.Value + "'")
            Dim TempAcqNO As String = ""
            If dtAcquisition IsNot Nothing AndAlso dtAcquisition.Rows.Count > 0 Then
                TempAcqNO = "Purchase Invoice - " + txtDocNo.Value + " is used in following Acquisition Entry -"
                For Each drAcq As DataRow In dtAcquisition.Rows
                    TempAcqNO += Environment.NewLine + clsCommon.myCstr(drAcq("Acquisition_Code"))
                Next
                clsCommon.MyMessageBoxShow(TempAcqNO, Me.Text)
                Exit Sub
            End If

            If clsCommon.MyMessageBoxShow("Are you sure to Cancel the Record?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            If clsPurchaseInvoiceHead.CancelData(Me.Form_ID, txtDocNo.Value) Then
                clsCommon.MyMessageBoxShow(Me, "Purchase Invoice cancelled successfully!", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            CancelPI()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnNewHistory_Click(sender As Object, e As EventArgs) Handles btnNewHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, "PI_No", "TSPL_PI_HEAD", "TSPL_PI_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnJE_Click(sender As Object, e As EventArgs) Handles btnJE.Click
        clsOpenJEAgainstInvoice.ShowJEForPurchase(txtDocNo.Value)
    End Sub

    Private Sub btnPrintInv_Click(sender As Object, e As EventArgs) Handles btnPrintInv.Click
        Try
            Dim ItemCode As String = Nothing

            If gv1.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each rows As GridViewRowInfo In gv1.Rows
                    arr.Add(clsCommon.myCstr(rows.Cells(colICode).Value))
                Next
                ItemCode = clsCommon.GetMulcallString(arr)
            End If
            Dim strTabSRNTender As String = "TSPL_SRN_TENDER"
            If UsLock1.Status = ERPTransactionStatus.Pending Then
                strTabSRNTender = "TSPL_SRN_TENDER_CALC"
            End If
            Dim frmCRV As New frmCrystalReportViewer()
            Dim qry As String = ""
            Dim dt As DataTable
            Dim SRNQTTY As String = ""
            Dim SRNQTTY1stsch As String = ""
            If objCommonVar.RCDFCFP Then
                SRNQTTY = clsDBFuncationality.getSingleValue("select cast(SUM(SRN_Qty) as decimal(18,3)) QTY from TSPL_SRN_HEAD 
                    left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.srn_no=TSPL_SRN_head.srn_no
                    LEFT OUTER JOIN TSPL_GRN_HEAD ON TSPL_GRN_HEAD.GRN_NO=TSPL_SRN_HEAD.AGAINST_GRN
                    where TSPL_SRN_head.Bill_To_Location='" + txtBillToLocation.Value + "' AND TSPL_SRN_DETAIL.Item_Code in (" + ItemCode + ") AND TSPL_SRN_HEAD.Vendor_Code='" + txtVendorNo.Value + "' AND TSPL_GRN_HEAD.REF_NO='" + txtRefNo.Text + "'
                    AND GRN_Date<=(Select MAX(GRN_Date) GRNDATE  from TSPL_GRN_HEAD 
                    WHERE TSPL_GRN_HEAD.GRN_No IN (SELECT GRN_Id FROM TSPL_PI_DETAIL WHERE pi_no='" + txtDocNo.Value + "'))")

                SRNQTTY1stsch = clsDBFuncationality.getSingleValue("select isnull(cast(SUM(SRN_Qty) as decimal(18,3)),0) QTY from TSPL_SRN_HEAD 
                    left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.srn_no=TSPL_SRN_head.srn_no
                    LEFT OUTER JOIN TSPL_GRN_HEAD ON TSPL_GRN_HEAD.GRN_NO=TSPL_SRN_HEAD.AGAINST_GRN
					LEFT OUTER JOIN TSPL_TENDER_SCHEDULE ON TSPL_TENDER_SCHEDULE.DocumentCode='" + txtRefNo.Text + "'
						and TSPL_TENDER_SCHEDULE.item_code in(" + ItemCode + ") and TSPL_TENDER_SCHEDULE.Location_Code='" + txtBillToLocation.Value + "' and TSPL_TENDER_SCHEDULE.Vendor_Code ='" + txtVendorNo.Value + "' AND
						TSPL_TENDER_SCHEDULE.Schedule_No=1
                    where TSPL_SRN_head.Bill_To_Location='" + txtBillToLocation.Value + "' AND TSPL_SRN_DETAIL.Item_Code in (" + ItemCode + ") AND TSPL_SRN_HEAD.Vendor_Code='" + txtVendorNo.Value + "' AND TSPL_GRN_HEAD.REF_NO='" + txtRefNo.Text + "'
                    AND convert(date,GRN_Date,103)<=(Select MAX(convert(date,GRN_Date,103)) GRNDATE  from TSPL_GRN_HEAD 
                    --left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.srn_no=TSPL_SRN_head.srn_no
                    WHERE TSPL_GRN_HEAD.GRN_No IN (SELECT GRN_ID FROM TSPL_PI_DETAIL WHERE pi_no='" + txtDocNo.Value + "'))
					 AND convert(date,TSPL_GRN_HEAD.GRN_Date,103)<= dateadd(day,TSPL_TENDER_SCHEDULE.Extension_Days,TSPL_TENDER_SCHEDULE.TO_DATE)")
            End If
            If objCommonVar.RCDFCFP Then
                qry = " SELECT ss.*," + SRNQTTY + " as SRNQTTY," + SRNQTTY1stsch + " as SRNQtyInQtl1stsch,ss.RALQty - " + SRNQTTY + " as QWNSWNQTY,(select  CAST(sum(TSPL_SRN_DETAIL.SRN_Qty) AS DECIMAL(18,2)) as SRNQtyInQtl from TSPL_PI_DETAIL 
                        left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No = TSPL_PI_DETAIL.SRN_Id and TSPL_SRN_DETAIL.Item_Code = TSPL_PI_DETAIL.Item_Code
                        left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_No
                        left outer join TSPL_GRN_HEAD GG on GG.GRN_No = TSPL_SRN_DETAIL.GRN_ID
                        where GG.ref_no=SS.Ref_No
                        and TSPL_SRN_DETAIL.item_code=SS.Item_Code and GG.BILL_TO_LOCATION=SS.BILL_TO_LOCATION and ss.vendor_code =TSPL_SRN_HEAD.vendor_code)as SRNQtyInQtl,
                         CONVERT(varchar(10), CONVERT(date, ss.schedule_from_date, 111), 105) as schedule_from_date,
                          CONVERT(varchar(10), CONVERT(date, ss.schedule_to_date, 111), 105) as schedule_to_date, ss.qc_no,ss.grn_date
                        FROM (select '' as RAL_Period,isnull(TSPL_PI_REMITTANCE.Actual_Total_TDS,0) as Actual_Total_TDS,cast(case when isnull(TSPL_PI_REMITTANCE.Actual_Total_TDS,0)>0 and isnull(TSPL_PI_DETAIL.Taxable_Amount,0)>0 then
                         ( isnull(TSPL_PI_REMITTANCE.Actual_Total_TDS,0) 
                          / (select sum(isnull(TSPL_PI_DETAIL.Taxable_Amount,0)) from TSPL_PI_DETAIL where PI_NO='" + txtDocNo.Value + "'  ))
                          * isnull(TSPL_PI_DETAIL.Taxable_Amount,0) else 0 end as decimal(18,2)) as TDS,
                           isnull (" + strTabSRNTender + ".Penalty,0) as Penalty ,
                           isnull (TSPL_SRN_DEDUCTION.Ded_Amt,0) as Ded_Amt,
                           convert (varchar, min (TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date ) over (Partition by TSPL_PI_HEAD.PI_No  ) ,103) as MinDate,
                           convert (varchar, max (TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date ) over (Partition by TSPL_PI_HEAD.PI_No  ) ,103) as MaxDate,
                    tspl_item_master.Item_Desc, UPPER( TSPL_COMPANY_MASTER.Comp_Name) as Comp_Name, TSPL_PI_HEAD.PI_No ,convert (varchar,TSPL_PI_HEAD.PI_Date,103) as PI_Date , TSPL_GRN_HEAD.GRN_No , TSPL_MRN_HEAD.MRN_No ,TSPL_SRN_HEAD.SRN_No  ,TSPL_PI_HEAD.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Name,TSPL_PI_HEAD.Vendor_Invoice_No as BillNo , TSPL_SRN_HEAD.Against_QC_Code as QualityReportNo,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code as WeighingSlipNo, TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date as WeighmentDate,TSPL_GRN_HEAD.VehicleNo  as TruckNo  
                    , cast  (( TSPL_PI_DETAIL.PI_Qty * Source_UOM .Conversion_Factor / Target_UOM.Conversion_Factor)  as decimal(18,2)) as QtyInKg 
                    , TSPL_PI_DETAIL.PI_Qty as QtyInQtl
                    ,cast ((TSPL_PI_DETAIL.Item_Net_Amt / cast  (( TSPL_PI_DETAIL.PI_Qty * Source_UOM .Conversion_Factor / Target_UOM.Conversion_Factor)  as decimal(18,2)) ) as decimal(18,2)) as RateInKg, cast ((TSPL_PI_DETAIL.Item_Net_Amt /TSPL_PI_DETAIL.PI_Qty) as decimal(18,2)) as RateInQtl
                    , TSPL_PI_DETAIL.Total_Tax_Amt  as GST_RATE , TSPL_PI_DETAIL.Item_Net_Amt as Amount , isnull ( TSPL_QC_CHECK_SRN_DETAIL.InputDataDeductionPer,0) as  Per_QLT,  isnull (TSPL_SRN_DEDUCTION.Ded_Amt,0) as QualityDeduction, isnull(TSPL_SRN_DEDUCTION_SECURITY.Ded_Amt,0) as Securitys , TSPL_PURCHASE_ORDER_HEAD.RefTendorNo as Ref_No ,   isnull (TSPL_SRN_DEDUCTION.Ded_Amt,0) + isnull (" + strTabSRNTender + ".Penalty,0)  +case when  TSPL_ITEM_MASTER.Security_Deduction > 0 then cast ( ( TSPL_PI_DETAIL.Item_Net_Amt * TSPL_ITEM_MASTER.Security_Deduction/100 ) as decimal(18,2) ) else 0 end   as TotalDeduction    ,TSPL_PI_DETAIL.Item_Net_Amt  -  ( isnull (TSPL_SRN_DEDUCTION.Ded_Amt,0) + isnull (" + strTabSRNTender + ".Penalty,0)  +case when  TSPL_ITEM_MASTER.Security_Deduction > 0 then cast ( ( TSPL_PI_DETAIL.Item_Net_Amt * TSPL_ITEM_MASTER.Security_Deduction/100 ) as decimal(18,2) ) else 0 end ) as PayableAmount
                    ,TSPL_LOCATION_MASTER.Location_Desc	
                    ,tspl_grn_head.grn_date
                    ,TSPL_GRN_HEAD.[Invoice/Challan_No] as GRNChallan_No,
					TSPL_PO_WEIGHTMENT_GUNNY.JuteBagWeight,TSPL_PO_WEIGHTMENT_GUNNY.PPBagWeight
                     ,TSPL_TENDER_DETAIL.Qty as RALQty	
                    ,TSPL_PI_DETAIL.Item_Code,TSPL_PI_HEAD.Bill_To_Location
                    , TAC1.Description as TAC1name,isnull (TSPL_PI_HEAD.Add_Charge_Amt1,0) as TAC1amt
                    , TAC2.Description as TAC2name,isnull (TSPL_PI_HEAD.Add_Charge_Amt2,0) as TAC2amt
                    , TAC3.Description as TAC3name,isnull (TSPL_PI_HEAD.Add_Charge_Amt3,0) as TAC3amt
                    , TAC4.Description as TAC4name,isnull (TSPL_PI_HEAD.Add_Charge_Amt4,0) as TAC4amt
                    , TAC5.Description as TAC5name,isnull (TSPL_PI_HEAD.Add_Charge_Amt5,0) as TAC5amt
                    ,RoundOffAmt
                    ,dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type
                    ,TSPL_PI_DETAIL.TAX1_Rate as dTAX1_Rate, TSPL_PI_DETAIL.TAX2_Rate as dTAX2_Rate, TSPL_PI_DETAIL.TAX3_Rate as dTAX3_Rate ,TSPL_PI_DETAIL.TAX4_Rate as dTAX4_Rate ,TSPL_PI_DETAIL.TAX5_Rate as dTAX5_Rate
                    , TSPL_PI_DETAIL.TAX1_Amt, TSPL_PI_DETAIL.TAX2_Amt, TSPL_PI_DETAIL.TAX3_Amt, TSPL_PI_DETAIL.TAX4_Amt, TSPL_PI_DETAIL.TAX5_Amt
                    ,TSPL_PI_HEAD.Description,TSPL_PI_HEAD.Remarks,TSPL_PI_DETAIL.Item_Cost  
                          ,TSPL_TENDER_SCHEDULE.schedule_from_date					
					      ,TSPL_TENDER_SCHEDULE.schedule_to_date
                          ,TSPL_QC_CHECK_HEAD.Document_Code as QC_NO
                          from TSPL_PI_DETAIL 
                    left outer join TSPL_PI_HEAD on TSPL_PI_DETAIL.PI_No = TSPL_PI_HEAD.PI_No
                    left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No = TSPL_PI_DETAIL.SRN_Id and TSPL_SRN_DETAIL.Item_Code = TSPL_PI_DETAIL.Item_Code
                    left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_No
                    left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No = TSPL_SRN_DETAIL.GRN_ID
                    left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Gate_Entry_No=TSPL_GRN_HEAD.GRN_No
                    left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.Against_GRN = TSPL_GRN_HEAD.GRN_No
                    left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No = TSPL_GRN_HEAD.GRN_No
                    left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code = TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
                    left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_PI_DETAIL .tax1 left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_PI_DETAIL.tax2    left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_PI_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_PI_DETAIL .tax4   left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_PI_DETAIL .tax5
                    left join TSPL_Additional_Charges TAC1 ON TSPL_PI_HEAD.Add_Charge_Code1=TAC1.Code
                    left join TSPL_Additional_Charges TAC2 ON TSPL_PI_HEAD.Add_Charge_Code2=TAC2.Code
                    left join TSPL_Additional_Charges TAC3 ON TSPL_PI_HEAD.Add_Charge_Code3=TAC3.Code
                    left join TSPL_Additional_Charges TAC4 ON TSPL_PI_HEAD.Add_Charge_Code4=TAC4.Code
                    left join TSPL_Additional_Charges TAC5 ON TSPL_PI_HEAD.Add_Charge_Code5=TAC5.Code
                    left outer join (select MRN_No , sum(InputDataDeductionPer) as InputDataDeductionPer from TSPL_QC_CHECK_SRN_DETAIL group by MRN_No ) as  TSPL_QC_CHECK_SRN_DETAIL on TSPL_QC_CHECK_SRN_DETAIL.MRN_No = TSPL_MRN_HEAD.MRN_No
                    left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_PI_DETAIL.Item_Code
                    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_PI_HEAD.Vendor_Code
                    left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_PI_HEAD.Comp_Code
                    left outer join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = TSPL_PI_DETAIL.Item_Code and Source_UOM.UOM_Code = TSPL_PI_DETAIL.UNIT_CODE
                    left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = TSPL_PI_DETAIL.Item_Code and Target_UOM.UOM_Code = 'KG'
                    left outer join (SELECT SRN_NO,Item_Code,ISNULL(SUM(Penalty),0) AS Penalty FROM " + strTabSRNTender + " GROUP BY SRN_NO,Item_Code)" + strTabSRNTender + " on " + strTabSRNTender + ".SRN_No = TSPL_PI_DETAIL.SRN_Id and " + strTabSRNTender + ".Item_Code = TSPL_PI_DETAIL.Item_Code
                    left outer join TSPL_SRN_DEDUCTION on TSPL_SRN_DEDUCTION.SRN_No = TSPL_PI_DETAIL.SRN_Id and TSPL_SRN_DEDUCTION.Item_Code = TSPL_PI_DETAIL.Item_Code
                    left outer join TSPL_PI_REMITTANCE on TSPL_PI_REMITTANCE.Document_No=TSPL_PI_HEAD.pi_no
                    left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_HEAD.Against_PO
                    left outer join TSPL_LOCATION_MASTER on TSPL_PI_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code
                    left join (SELECT Weighment_Code, ISNULL([PM0001],0) AS 'JuteBagWeight',ISNULL([PM0002],0) AS 'PPBagWeight'  FROM (SELECT TSPL_PO_WEIGHTMENT_GUNNY.Weighment_Code,TSPL_PO_WEIGHTMENT_GUNNY.Item_Code,CAST(ISNULL(TSPL_PO_WEIGHTMENT_GUNNY.Qty,0)*ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)/100 AS decimal(18,2)) AS QTY FROM   TSPL_PO_WEIGHTMENT_GUNNY
					left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_PO_WEIGHTMENT_GUNNY.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='KG' ) AS PO_WEIGHTMENT_GUNNY
					PIVOT (SUM(QTY) FOR ITEM_CODE IN ([PM0001],[PM0002])) AS TSPL_PO_WEIGHTMENT_GUNNY) TSPL_PO_WEIGHTMENT_GUNNY on TSPL_PO_WEIGHTMENT_GUNNY.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
                    --left join TSPL_ITEM_MASTER as GUNNY_TSPL_ITEM_MASTER ON GUNNY_TSPL_ITEM_MASTER.ITEM_CODE=TSPL_PO_WEIGHTMENT_GUNNY.ITEM_CODE
                    LEFT JOIN TSPL_TENDER_DETAIL ON TSPL_GRN_HEAD.Ref_No=TSPL_TENDER_DETAIL.DocumentCode AND TSPL_TENDER_DETAIL.Location=TSPL_GRN_HEAD.Bill_To_Location
                    and TSPL_TENDER_DETAIL.Item_Code=TSPL_PI_DETAIL.Item_Code AND TSPL_TENDER_DETAIL.Vendor_Code=TSPL_GRN_HEAD.Vendor_Code
                    left join TSPL_SRN_DEDUCTION_SECURITY on TSPL_SRN_DEDUCTION_SECURITY.SRN_No=TSPL_SRN_HEAD.SRN_No
                     left outer join TSPL_TENDER_HEADER on TSPL_TENDER_HEADER.DocumentCode=TSPL_grn_head.ref_no
					 left outer join  ( select DocumentCode,min(From_Date) as schedule_from_date,max(DATEADD(day, Extension_Days, to_date)) as schedule_to_date,max(Schedule_Qty) as Schedule_Qty,max(Schedule_Qty_Per) as Schedule_Qty_Per,max(Schedule_Short) as Schedule_Short,max(Schedule_Short_Per) as Schedule_Short_Per,max(Schedule_No ) as Schedule_No,count(*) as NoOfSchedule,min(Schedule_No) as Schedule_No_Min from TSPL_TENDER_SCHEDULE 
                     where TSPL_TENDER_SCHEDULE.DocumentCode ='" + txtRefNo.Text + "'and 
					 TSPL_TENDER_SCHEDULE.Item_Code in (" + ItemCode + ")and
					 TSPL_TENDER_SCHEDULE.Vendor_Code='" + txtVendorNo.Value + "' and
					 TSPL_TENDER_SCHEDULE.Location_Code='" + txtBillToLocation.Value + "' GROUP BY DocumentCode) TSPL_TENDER_SCHEDULE on
					 TSPL_TENDER_SCHEDULE.DocumentCode=TSPL_TENDER_HEADER.DocumentCode
                    where TSPL_PI_HEAD.PI_No = '" + txtDocNo.Value + "' )ss WHERE 1=1 order by convert(date,ss.GRN_Date,103) "

                dt = clsDBFuncationality.GetDataTable(qry)

                Dim TempSumTDS_SRN_Wise As Decimal = clsCommon.myCdbl(dt.Compute("SUM(TDS)", " TDS is not null"))
                If dt.Rows(0).Item("Actual_Total_TDS") <> TempSumTDS_SRN_Wise Then
                    dt.Rows(dt.Rows.Count - 1).Item("TDS") = dt.Rows(dt.Rows.Count - 1).Item("TDS") + (clsCommon.myCdbl(dt.Rows(0).Item("Actual_Total_TDS")) - TempSumTDS_SRN_Wise)
                End If

                'Ral Period
                Dim strQry As String = "select From_Date,To_Date,Schedule_Qty
                                        from TSPL_TENDER_SCHEDULE where DocumentCode='" + clsCommon.myCstr(dt.Rows(0).Item("Ref_No")) + "'
                                        AND Vendor_Code='" + clsCommon.myCstr(dt.Rows(0).Item("Vendor_Code")) + "' and Location_Code='" + clsCommon.myCstr(dt.Rows(0).Item("Bill_To_Location")) + "'
                                        and item_code='" + clsCommon.myCstr(dt.Rows(0).Item("Item_Code")) + "'
                                        order by Schedule_No"
                Dim dtRalScheduleDeatil As DataTable = clsDBFuncationality.GetDataTable(strQry)
                Dim TempSumSRNQty As Decimal = clsCommon.myCdbl(dt.Compute("SUM(QtyInQtl)", " QtyInQtl is not null"))
                Dim RALPeriodDetail As String = ""
                Dim TempTotalQty As Decimal = 0.0
                Dim TempTotalQtyPrev As Decimal = 0.0
                For i As Int16 = 0 To dtRalScheduleDeatil.Rows.Count - 1
                    TempTotalQty = TempTotalQty + clsCommon.myCDecimal(dtRalScheduleDeatil.Rows(i).Item("Schedule_Qty"))
                    If TempSumSRNQty >= TempTotalQtyPrev AndAlso TempSumSRNQty <= TempTotalQty Then
                        RALPeriodDetail = RALPeriodDetail + "[" + clsCommon.GetPrintDate(dtRalScheduleDeatil.Rows(i).Item("From_Date"), "dd/MM/yyyy") + " - " + clsCommon.GetPrintDate(dtRalScheduleDeatil.Rows(i).Item("To_Date"), "dd/MM/yyyy") + "]"
                    End If
                    TempTotalQtyPrev = TempTotalQtyPrev + clsCommon.myCDecimal(dtRalScheduleDeatil.Rows(i).Item("Schedule_Qty"))
                Next

                dt.Rows(0).Item("RAL_Period") = RALPeriodDetail

                qry = "select Document_No as APInvoiceNo,convert(varchar,Posting_Date,103) as APInvoiceDate,Document_Type as Type,Description,Document_Total as Amount  from (
select   Document_No,Posting_Date,Document_Type,Description,-1*Document_Total as Document_Total,Document_No as SNo
from TSPL_VENDOR_INVOICE_HEAD where RefDocType in( 'SCH-PNT' ) and RefDocNo='" + txtDocNo.Value + "' 
union all
select Document_No,Posting_Date,Document_Type,Description,Document_Total,RefDocNo as SNo
from TSPL_VENDOR_INVOICE_HEAD where RefDocType in('REV-SPT') and RefDocNo in (select Document_No from TSPL_VENDOR_INVOICE_HEAD where RefDocType in('SCH-PNT') and RefDocNo='" + txtDocNo.Value + "' )
)x order by Posting_Date,x.SNo,Document_No"
                Dim dtAPDocs As DataTable = clsDBFuncationality.GetDataTable(qry)
                frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, dtAPDocs, "rptPurchaseInvoicePrintNew", "Purchase Invoice", "SubPurchaseInvoice.rpt")


            Else
                qry = " select isnull(TSPL_PI_REMITTANCE.Actual_Total_TDS,0) as TDS,isnull (" + strTabSRNTender + ".Penalty,0) as Penalty ,
                           isnull (TSPL_SRN_DEDUCTION.Ded_Amt,0) as Ded_Amt,
                           convert (varchar, min (TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date ) over (Partition by TSPL_PI_HEAD.PI_No  ) ,103) as MinDate,
                           convert (varchar, max (TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date ) over (Partition by TSPL_PI_HEAD.PI_No  ) ,103) as MaxDate,
                    tspl_item_master.Item_Desc, UPPER( TSPL_COMPANY_MASTER.Comp_Name) as Comp_Name, TSPL_PI_HEAD.PI_No ,convert (varchar,TSPL_PI_HEAD.PI_Date,103) as PI_Date , TSPL_GRN_HEAD.GRN_No , TSPL_MRN_HEAD.MRN_No ,TSPL_SRN_HEAD.SRN_No,TSPL_PI_HEAD.PI_Date as PI_DATE  ,TSPL_PI_HEAD.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Name,TSPL_PI_HEAD.Vendor_Invoice_No as BillNo , TSPL_SRN_HEAD.Against_QC_Code as QualityReportNo,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code as WeighingSlipNo, TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date as WeighmentDate,TSPL_GRN_HEAD.VehicleNo  as TruckNo  , cast  (( TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight * Source_UOM .Conversion_Factor / Target_UOM.Conversion_Factor)  as decimal(18,2)) as QtyInKg  , TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight as QtyInQtl,cast ((TSPL_PI_DETAIL.Amount / cast  (( TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight * Source_UOM .Conversion_Factor / Target_UOM.Conversion_Factor)  as decimal(18,2)) ) as decimal(18,2)) as RateInKg, cast ((TSPL_PI_DETAIL.Amount /TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight) as decimal(18,2)) as RateInQtl, TSPL_PI_DETAIL.Total_Tax_Amt  as GST_RATE , TSPL_PI_DETAIL.Item_Net_Amt as Amount , isnull ( TSPL_QC_CHECK_SRN_DETAIL.InputDataDeductionPer,0) as  Per_QLT,  isnull (TSPL_SRN_DEDUCTION.Ded_Amt,0) as QualityDeduction,  case when  TSPL_ITEM_MASTER.Security_Deduction > 0 then cast ( ( TSPL_PI_DETAIL.Item_Net_Amt * TSPL_ITEM_MASTER.Security_Deduction/100 ) as decimal(18,2) ) else 0 end as Securitys , TSPL_GRN_HEAD.Ref_No ,   isnull (TSPL_SRN_DEDUCTION.Ded_Amt,0) + isnull (" + strTabSRNTender + ".Penalty,0)  +case when  TSPL_ITEM_MASTER.Security_Deduction > 0 then cast ( ( TSPL_PI_DETAIL.Item_Net_Amt * TSPL_ITEM_MASTER.Security_Deduction/100 ) as decimal(18,2) ) else 0 end   as TotalDeduction    ,TSPL_PI_DETAIL.Item_Net_Amt  -  ( isnull (TSPL_SRN_DEDUCTION.Ded_Amt,0) + isnull (" + strTabSRNTender + ".Penalty,0)  +case when  TSPL_ITEM_MASTER.Security_Deduction > 0 then cast ( ( TSPL_PI_DETAIL.Item_Net_Amt * TSPL_ITEM_MASTER.Security_Deduction/100 ) as decimal(18,2) ) else 0 end ) as PayableAmount from TSPL_PI_DETAIL 
                    left outer join TSPL_PI_HEAD on TSPL_PI_DETAIL.PI_No = TSPL_PI_HEAD.PI_No
                    left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No = TSPL_PI_DETAIL.SRN_Id and TSPL_SRN_DETAIL.Item_Code = TSPL_PI_DETAIL.Item_Code
                    left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_No
                    left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No = TSPL_SRN_DETAIL.GRN_ID
                    left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.Against_GRN = TSPL_GRN_HEAD.GRN_No
                    left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No = TSPL_GRN_HEAD.GRN_No
                    left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code = TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
                    
                    left outer join (select MRN_No , sum(InputDataDeductionPer) as InputDataDeductionPer from TSPL_QC_CHECK_SRN_DETAIL group by MRN_No ) as  TSPL_QC_CHECK_SRN_DETAIL on TSPL_QC_CHECK_SRN_DETAIL.MRN_No = TSPL_MRN_HEAD.MRN_No
                    left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_PI_DETAIL.Item_Code
                    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_PI_HEAD.Vendor_Code
                    left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_PI_HEAD.Comp_Code
                    left outer join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = TSPL_PO_WEIGHTMENT_DETAIL.Item_Code and Source_UOM.UOM_Code = TSPL_PO_WEIGHTMENT_DETAIL.UOM
                    left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = TSPL_PO_WEIGHTMENT_DETAIL.Item_Code and Target_UOM.UOM_Code = 'KG'
                    left outer join (SELECT SRN_NO,Item_Code,ISNULL(SUM(Penalty),0) AS Penalty FROM " + strTabSRNTender + " GROUP BY SRN_NO,Item_Code)" + strTabSRNTender + " on " + strTabSRNTender + ".SRN_No = TSPL_PI_DETAIL.SRN_Id and " + strTabSRNTender + ".Item_Code = TSPL_PI_DETAIL.Item_Code
                    left outer join TSPL_SRN_DEDUCTION on TSPL_SRN_DEDUCTION.SRN_No = TSPL_PI_DETAIL.SRN_Id and TSPL_SRN_DEDUCTION.Item_Code = TSPL_PI_DETAIL.Item_Code
                    left outer join TSPL_PI_REMITTANCE on TSPL_PI_REMITTANCE.Document_No=TSPL_PI_HEAD.pi_no
                    where TSPL_PI_HEAD.PI_No = '" + txtDocNo.Value + "'  "

                dt = clsDBFuncationality.GetDataTable(qry)


                frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, Nothing, "rptPurchaseInvoicePrint", "Purchase Invoice")
            End If
            frmCRV = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
End Class
