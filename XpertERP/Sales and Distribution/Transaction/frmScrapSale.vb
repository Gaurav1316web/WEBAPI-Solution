Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.IO
Imports common


Public Class frmScrapSale
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim RunBatchFifowise As Integer = 0
    Dim CalculateTaxRatefromItemwsieTaxOnSale As Integer = 0
    Dim EnableTCSRateValidityFrom01July2021 As Boolean = False
    Public AllowtoChangeTCSBaseAmount As Boolean = False
    Dim AmountToCheckCustomerOutstandingForTCSTax As Double = 0
    Dim ConsiderPreviousandCurrentFYForTCSTaxCustOutstanding As Boolean = False
    Dim GSTStatus As Boolean = False
    Const ReportID As String = "ScrapSaleGrid"
    Private isCellValueChangedOpenAdd As Boolean = False
    Public strShipmentno As String = Nothing
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedTaxOpen As Boolean = False
    Dim CreatVatSeriesOnExciseInvoice As Integer = 0
    Const colLineNo As String = "COLLNO"
    Const colRowType As String = "COLTYPE"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colHSNNo As String = "COLHSNNo"
    Const colQty As String = "shippedQty"
    Const colprice As String = "price"
    Const colDisPer As String = "discountper"
    Const colDisAmt As String = "discountamt "
    Const coltotaxamt As String = "totaxamt"
    Const colnetprice As String = "netprice "
    Const colAmt As String = "itemamt"
    Const coltotdisamt As String = "totdisamt"
    Const colitemnetamt As String = "itemnetamt"
    Const coltotamt As String = "totamt"
    Const coltax As String = "tax"
    Const colUnit As String = "COLUNIT"
    Const colSp As String = "COLSPECIFICATION"
    Const colItemwiseTaxCode As String = "colItemwiseTaxCode"

    Const colTax1 As String = "COLTAX1"
    Const colTaxBaseAmt1 As String = "COLTAXBASEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"
    Const colIsTaxable1 As String = "ISTAXABLE1"
    Const colIsSurTax1 As String = "ISSURTAX1"
    Const colSurTaxCode1 As String = "SURTAXCODE1"
    Const colTaxRecoverable1 As String = "RECOVERTABLETAX1"
    Const colTaxOnBaseAmt1 As String = "COLTAXONBASEAMT1"
    Const colTax2 As String = "COLTAX2"
    Const colTaxBaseAmt2 As String = "COLTAXBASEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colIsTaxable2 As String = "ISTAXABLE2"
    Const colIsSurTax2 As String = "ISSURTAX2"
    Const colSurTaxCode2 As String = "SURTAXCODE2"
    Const colTaxRecoverable2 As String = "RECOVERTABLETAX2"
    Const colTaxOnBaseAmt2 As String = "COLTAXONBASEAMT2"
    Const colTax3 As String = "COLTAX3"
    Const colTaxBaseAmt3 As String = "COLTAXBASEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colIsTaxable3 As String = "ISTAXABLE3"
    Const colIsSurTax3 As String = "ISSURTAX3"
    Const colSurTaxCode3 As String = "SURTAXCODE3"
    Const colTaxRecoverable3 As String = "RECOVERTABLETAX3"
    Const colTaxOnBaseAmt3 As String = "COLTAXONBASEAMT3"
    Const colTax4 As String = "COLTAX4"
    Const colTaxBaseAmt4 As String = "COLTAXBASEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colIsTaxable4 As String = "ISTAXABLE4"
    Const colIsSurTax4 As String = "ISSURTAX4"
    Const colSurTaxCode4 As String = "SURTAXCODE4"
    Const colTaxRecoverable4 As String = "RECOVERTABLETAX4"
    Const colTaxOnBaseAmt4 As String = "COLTAXONBASEAMT4"
    Const colTax5 As String = "COLTAX5"
    Const colTaxBaseAmt5 As String = "COLTAXBASEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colIsTaxable5 As String = "ISTAXABLE5"
    Const colIsSurTax5 As String = "ISSURTAX5"
    Const colSurTaxCode5 As String = "SURTAXCODE5"
    Const colTaxRecoverable5 As String = "RECOVERTABLETAX5"
    Const colTaxOnBaseAmt5 As String = "COLTAXONBASEAMT5"
    Const colTax6 As String = "COLTAX6"
    Const colTaxBaseAmt6 As String = "COLTAXBASEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colIsTaxable6 As String = "ISTAXABLE6"
    Const colIsSurTax6 As String = "ISSURTAX6"
    Const colSurTaxCode6 As String = "SURTAXCODE6"
    Const colTaxRecoverable6 As String = "RECOVERTABLETAX6"
    Const colTaxOnBaseAmt6 As String = "COLTAXONBASEAMT6"
    Const colTax7 As String = "COLTAX7"
    Const colTaxBaseAmt7 As String = "COLTAXBASEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colIsTaxable7 As String = "ISTAXABLE7"
    Const colIsSurTax7 As String = "ISSURTAX7"
    Const colSurTaxCode7 As String = "SURTAXCODE7"
    Const colTaxRecoverable7 As String = "RECOVERTABLETAX7"
    Const colTaxOnBaseAmt7 As String = "COLTAXONBASEAMT7"
    Const colTax8 As String = "COLTAX8"
    Const colTaxBaseAmt8 As String = "COLTAXBASEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colIsTaxable8 As String = "ISTAXABLE8"
    Const colIsSurTax8 As String = "ISSURTAX8"
    Const colSurTaxCode8 As String = "SURTAXCODE8"
    Const colTaxRecoverable8 As String = "RECOVERTABLETAX8"
    Const colTaxOnBaseAmt8 As String = "COLTAXONBASEAMT8"
    Const colTax9 As String = "COLTAX9"
    Const colTaxBaseAmt9 As String = "COLTAXBASEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colIsTaxable9 As String = "ISTAXABLE9"
    Const colIsSurTax9 As String = "ISSURTAX9"
    Const colSurTaxCode9 As String = "SURTAXCODE9"
    Const colTaxRecoverable9 As String = "RECOVERTABLETAX9"
    Const colTaxOnBaseAmt9 As String = "COLTAXONBASEAMT9"
    Const colTax10 As String = "COLTAX10"
    Const colTaxBaseAmt10 As String = "COLTAXBASEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colIsTaxable10 As String = "ISTAXABLE10"
    Const colIsSurTax10 As String = "ISSURTAX10"
    Const colSurTaxCode10 As String = "SURTAXCODE10"
    Const colTaxRecoverable10 As String = "RECOVERTABLETAX10"
    Const colTaxOnBaseAmt10 As String = "COLTAXONBASEAMT10"
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
    Const colTotTaxAmt As String = "TotTaxAmt"
    Const colAmtAfterTax As String = "AmtAfterTax"

    Const colTTaxAutCode As String = "TTaxAutCode"
    Const colTTaxAutName As String = "TTaxAutName"
    Const colTBaseAmt As String = "TBaseAmt"
    Const colTTaxRate As String = "TTaxRate"
    Const colTTaxAmt As String = "TTaxAmt"



    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim repoBalQty As GridViewDecimalColumn
    Dim repoComplete As GridViewTextBoxColumn

    Const coladdcode As String = "COLADDCODE"
    Const coladddesc As String = "COLADDDESC"
    Const coladdamt As String = "COLADDAMT"
    Private AllowChangeInvoiceType As Boolean = False
    Private isALlowVehicleGateOutValidation As Boolean = False
    Public strExcise As Boolean
    Public objin As scrapinvoicehead
    Dim GrossWtfromItemMaster As Boolean = False
    Dim AllowRoundOff_onInvoice As Boolean = False
    Dim AllowAssetItem As Boolean = False
    Dim strPrintType As String = String.Empty
    Private PickCostOFMaterialSaleFromPriceMaster As Boolean = False
    Dim MaterialSaleInvoiceEnablePrintOnPost As Boolean = False
    Dim EInvoiceType As String = ""
    Dim strPdfAttachmentPath As String = ""
#End Region

    Public Sub SetUserMgmtNew()
        '--preeti gupta--ticket no[BM00000003178]
        'MyBase.SetUserMgmt(clsUserMgtCode.ScrapSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnCancel.Visible = MyBase.isCancel_Flag_After_Posting
        btnPrint.Visible = MyBase.isPrintFlag
        btnReverse.Visible = False
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        If MyBase.isExport = True Then
            rmiImport.Enabled = True
            rmiExport.Enabled = True
        Else
            rmiImport.Enabled = False
            rmiExport.Enabled = False
        End If
    End Sub


    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RunBatchFifowise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBatchFifowise, clsFixedParameterCode.RunBatchFifowise, Nothing))
        CalculateTaxRatefromItemwsieTaxOnSale = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateTaxRatefromItemwsieTaxOnSale, clsFixedParameterCode.CalculateTaxRatefromItemwsieTaxOnSale, Nothing))
        EnableTCSRateValidityFrom01July2021 = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableTCSRateValidityFrom01July2021, clsFixedParameterCode.EnableTCSRateValidityFrom01July2021, Nothing)) = 0, False, True)
        AmountToCheckCustomerOutstandingForTCSTax = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckCustomerOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckCustomerOutstandingForTCSTax, Nothing))
        AllowtoChangeTCSBaseAmount = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoChangeTCSBaseAmount, clsFixedParameterCode.AllowtoChangeTCSBaseAmount, Nothing)) = 0, False, True)
        ConsiderPreviousandCurrentFYForTCSTaxCustOutstanding = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ConsiderPreviousCurrentFYForTCSTaxCustOutstanding, clsFixedParameterCode.ConsiderPreviousCurrentFYForTCSTaxCustOutstanding, Nothing)) = "1", True, False)
        chkCashSale.Visible = True
        SetUserMgmtNew()
        fndcustNo.MendatroryField = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A Create Additional Cost")
        'ButtonToolTip.SetToolTip(btnRequistionItems, "Press Ctrl+F7 for Select Purchase Requistion Items")

        ''====================================================================
        clsFixedParameter.InsertDefaultValueFixedParameter(clsFixedParameterType.AllowAssetItemOnMiscSale, clsFixedParameterCode.AllowAssetItemOnMiscSale, "1", "0:Off,1:On; when on then asset items show in Misc. Sale,otherwise not seen.")
        ''====================================================================

        AllowAssetItem = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowAssetItemOnMiscSale, clsFixedParameterCode.AllowAssetItemOnMiscSale, Nothing)) = 1, True, False)
        MaterialSaleInvoiceEnablePrintOnPost = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaterialSaleInvoiceEnablePrintOnPost, clsFixedParameterCode.MaterialSaleInvoiceEnablePrintOnPost, Nothing)) = 1, True, False)
        GrossWtfromItemMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GrossWtFromItemMasterONCSATransfer, clsFixedParameterCode.GrossWtFromItemMasterONCSATransfer, Nothing)) = 1, True, False)
        RadPageView1.SelectedPage = RadPageViewPage1
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            chkScrapSale.Visible = True
        End If
        LoadBlankGrid()
        LoadBlankGridTax()
        '=====Sanjeet(10/02/2017)===================
        AllowRoundOff_onInvoice = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowRoundOff_OnCSASalePatti, clsFixedParameterCode.AllowRoundOff_OnCSASalePatti, Nothing)) = "1", True, False))
        lblRound_Off.Visible = AllowRoundOff_onInvoice
        txtRoundOff.Visible = AllowRoundOff_onInvoice

        '========End=======================

        AddNew()
        If clsCommon.myLen(strShipmentno) > 0 Then
            LoadData(strShipmentno, NavigatorType.Current)
        End If


        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

        ReStoreGridLayout()
        SetLength()

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields

        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment
        '' make editable/non editable Term Code
        txtTermCode.Enabled = clsPurchaseOrderHead.GetInventorySetting().Rows(0).Item("IsTermsEditableOnPurchase")
        ''richa agarwal 18/03/2015
        AllowChangeInvoiceType = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Allow_Change_InvoiceType from TSPL_inv_parameters")) = 0, False, True)
        isALlowVehicleGateOutValidation = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowVehicleGateOutValidationScrapSale, clsFixedParameterCode.AllowVehicleGateOutValidationScrapSale, Nothing)) = "1", True, False)
        PickCostOFMaterialSaleFromPriceMaster = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PickCostOFMaterialSaleFromPriceMaster, clsFixedParameterCode.PickCostOFMaterialSaleFromPriceMaster, Nothing)) = "1", True, False)
        '------------------------

        '-----Ravi---------------
        CreatVatSeriesOnExciseInvoice = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateVatSeriesForProductExciseinvoice, clsFixedParameterCode.CreateVatSeriesForProductExciseinvoice, Nothing))
        If CreatVatSeriesOnExciseInvoice = 1 Then
            lblSecondryInvNo.Visible = True
            txtVatInvNo.Visible = True
            btnPrint.Text = "Excise"
            ' btnPrePrint.Text = "Tax"
        End If
        ''-------------------
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        If chkTaxable.Checked = True Then
            RadPageView1.Pages("RadPageViewPage5").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("RadPageViewPage5").Item.Visibility = ElementVisibility.Collapsed

        End If
    End Sub
    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtcustdesc.MaxLength = 50
        txtlocation.MaxLength = 50
        'txtdescription.MaxLength = 200
        txtref.MaxLength = 200
        txtscrapinvoice.MaxLength = 30
        txtponumber.MaxLength = 30

    End Sub

    Sub BlankAllControls()
        txtFreightDistance.Value = 0
        btnCancel.Enabled = False
        EInvoiceType = ""
        txtDocNo.Value = ""
        txtponumber.Text = ""
        chkOnHold.Checked = False
        txtGWeighmentNo.Value = ""
        fndcustNo.Value = ""
        'txtinvoice.Text = ""
        fndcustNo.Value = ""
        txtcustdesc.Text = ""
        'txtvehicle_mannual_no.Text = ""
        txtTransporter_Code.Value = Nothing
        txtTransporter_desc.Text = ""
        txtnrg.Value = ""
        chkinvoice.Checked = False
        chkExcisable.Checked = False
        chkOnHold.Checked = False
        chkScrapSale.Checked = False
        chkCashSale.Checked = False
        chkCashSale.Enabled = True
        chkScrapSale.Enabled = True
        chkBuyBack.Checked = False
        dtpshipment.Value = clsCommon.GETSERVERDATE()
        dtppost.Text = clsCommon.GETSERVERDATE()
        dtpexp.Text = clsCommon.GETSERVERDATE()
        'txtdescription.Text = ""
        txtEWayBillNo.Text = ""
        txtEWayBillRemarks.Text = ""
        txtEwayValidDate.Value = clsCommon.GETSERVERDATE()
        txtEWayBillDate.Value = clsCommon.GETSERVERDATE()
        txtref.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        TxtVehicleCode.Value = ""
        txtVehicleDesc.Text = ""
        txtaddamt.Text = ""
        fndLocation.Value = ""
        txtponumber.Text = ""
        gvadd.DataSource = Nothing
        gvadd.Rows.Clear()
        txtaddamt.Text = ""
        'txttotaladdamt.Text = ""
        lblAmtWithDiscount.Text = ""
        lblDiscountAmt.Text = ""
        lblAmtAfterDiscount.Text = ""
        lblTaxAmt.Text = ""
        lblTotRAmt.Text = ""
        lbladdcharges.Text = ""
        txtRoundOff.Text = ""
        lblDocAmount.Text = ""
        lbldocamt.Text = ""
        txtlocation.Text = ""
        fndShipToLocation.Value = ""
        chkinvoice.Checked = True
        UsLock1.Status = 0
        dtpexp.Value = clsCommon.GETSERVERDATE()
        dtppost.Value = clsCommon.GETSERVERDATE()
        dtpshipment.Value = clsCommon.GETSERVERDATE()
        txtscrapinvoice.Text = ""
        rbtnTaxCalAutomatic.IsChecked = True
        chkInterBranch.Checked = False
        lblInvoiceNo.Text = ""
        txtVatInvNo.Text = ""
        fndcustNo.Enabled = True
        chkTaxable.Enabled = True
        fndLocation.Enabled = True
        chkExcisable.Enabled = True
        lblGrossWeight.Text = ""
        lblNetWeight.Text = ""
        EinvoiceAckNo.Text = ""
        EInvoiceIRNNo.Text = ""
        EInvoiceQrCode.Text = ""
        txtAckDate.Value = clsCommon.GETSERVERDATE()
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

        chkTaxable.Checked = False
        txtEWayBillNo.Text = ""
        txtEWayBillDate.Checked = False
        txtEWayBillDate.Value = dtpshipment.Value
        txtEWayBillNo.ReadOnly = False
        txtEWayBillDate.ReadOnly = False
        ' txtElectronicRefNo.Text = ""
        If AllowtoChangeTCSBaseAmount = True Then
            txttcstaxbaseamount.Enabled = True
        Else
            txttcstaxbaseamount.Enabled = False
        End If
        txttcstaxbaseamount.Value = 0
        lblActualTCSTaxBaseAmt.Text = "0"
        txtTCSTaxRate.Value = 0
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
        repoRowType = Nothing

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
        repoIName.HeaderText = "HSN No"
        repoIName.Name = colHSNNo
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim Unit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Unit.FormatString = ""
        Unit.HeaderText = "UOM"
        Unit.Name = colUnit
        Unit.Width = 80
        Unit.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Unit)



        Dim shippedQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        shippedQty = New GridViewDecimalColumn()
        shippedQty.FormatString = ""
        shippedQty.HeaderText = "Shipped Quantity"
        shippedQty.Name = colQty
        shippedQty.IsVisible = True
        shippedQty.Minimum = 0
        shippedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        shippedQty.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(shippedQty)


        Dim price As GridViewDecimalColumn = New GridViewDecimalColumn()
        price.WrapText = True
        price.HeaderText = "Unit Cost"
        price.Name = colprice
        price.Width = 80
        price.Minimum = 0
        price.ReadOnly = False
        price.FormatString = "{0:n4}"
        price.DecimalPlaces = 4
        price.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(price)

        Dim ItemAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        ItemAmt.FormatString = ""
        ItemAmt.HeaderText = "Item Amt"
        ItemAmt.Name = colAmt
        ItemAmt.Width = 80
        ItemAmt.Minimum = 0
        ItemAmt.ReadOnly = False
        ItemAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(ItemAmt)

        Dim discountper As GridViewDecimalColumn = New GridViewDecimalColumn()
        discountper.FormatString = ""
        discountper.WrapText = True
        discountper.HeaderText = "Discount%"
        discountper.Name = colDisPer
        discountper.Width = 80
        discountper.Minimum = 0
        discountper.ReadOnly = False
        discountper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(discountper)

        Dim discount As GridViewDecimalColumn = New GridViewDecimalColumn()
        discount.FormatString = ""
        discount.WrapText = True
        discount.HeaderText = "Discount Amt"
        discount.Name = colDisAmt
        discount.Width = 80
        discount.Minimum = 0
        discount.ReadOnly = True
        discount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(discount)


        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)



        Dim netPriceAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        netPriceAmt = New GridViewDecimalColumn()
        netPriceAmt.FormatString = ""
        netPriceAmt.HeaderText = "Net Price Amt"
        netPriceAmt.Name = colnetprice
        netPriceAmt.Width = 80
        netPriceAmt.Minimum = 0
        netPriceAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(netPriceAmt)



        Dim totDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        totDisAmt.FormatString = ""
        totDisAmt.HeaderText = "Total Discount Amt"
        totDisAmt.Name = coltotdisamt
        totDisAmt.Width = 80
        totDisAmt.Minimum = 0
        totDisAmt.ReadOnly = True
        totDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(totDisAmt)

        'Dim totaxamt As GridViewDecimalColumn = New GridViewDecimalColumn()
        'totaxamt.FormatString = ""
        'totaxamt.HeaderText = "Total Tax Amount"
        'totaxamt.Name = coltotaxamt
        'totaxamt.Width = 80
        'totaxamt.Minimum = 0
        'totaxamt.ReadOnly = True
        'totaxamt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.MasterTemplate.Columns.Add(totaxamt)

        Dim itemnetamt As GridViewDecimalColumn = New GridViewDecimalColumn()
        itemnetamt.FormatString = ""
        itemnetamt.HeaderText = "Item Net Amt"
        itemnetamt.ReadOnly = True
        itemnetamt.WrapText = True
        itemnetamt.Name = colitemnetamt
        itemnetamt.Width = 80
        itemnetamt.Minimum = 0
        itemnetamt.ReadOnly = True
        itemnetamt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(itemnetamt)





        Dim totamt As GridViewDecimalColumn = New GridViewDecimalColumn()
        totamt = New GridViewDecimalColumn()
        totamt.FormatString = ""
        totamt.HeaderText = "Total Amt"
        totamt.Name = coltotamt
        totamt.Width = 80
        totamt.Minimum = 0
        totamt.ReadOnly = True
        totamt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(totamt)






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

        Dim repoIsSurTax1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable2)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable3)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable4)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable5)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable6)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable7)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable8)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable9)

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
        gv1.MasterTemplate.Columns.Add(repoIsTaxable10)

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

        Dim repoSP As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSP = New GridViewTextBoxColumn()
        repoSP.FormatString = ""
        repoSP.HeaderText = "Specification"
        repoSP.Name = colSp
        repoSP.Width = 100
        'repoSP.Minimum = 0
        repoSP.ReadOnly = False
        repoSP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSP)

        Dim repoItemTaxCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemTaxCode = New GridViewTextBoxColumn()
        repoItemTaxCode.FormatString = ""
        repoItemTaxCode.HeaderText = "Item Tax Code"
        repoItemTaxCode.Name = colItemwiseTaxCode
        repoItemTaxCode.Width = 100
        repoItemTaxCode.IsVisible = False
        repoItemTaxCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemTaxCode)

        'Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoAmtAfterTax.FormatString = ""
        'repoAmtAfterTax.HeaderText = "Included Tax Amount"
        'repoAmtAfterTax.Name = colAmtAfterTax
        'repoAmtAfterTax.WrapText = True
        'repoAmtAfterTax.Width = 80
        'repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'repoAmtAfterTax.ReadOnly = True
        'gv1.MasterTemplate.Columns.Add(repoAmtAfterTax)

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
        repoTaxRate.ReadOnly = True
        repoTaxRate.IsVisible = True
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

    End Sub
    Sub setBalance()
        UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
        'UcItemBalance1.ItemMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colm).Value)
        UcItemBalance1.LocationCode = fndLocation.Value
        UcItemBalance1.LocationName = txtlocation.Text
        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtDocNo.Value
        UcItemBalance1.TransDate = dtpshipment.Value
        UcItemBalance1.ShowSOQty = True
        UcItemBalance1.CommitedQty = True
        UcItemBalance1.CommitedQtyLbl = True
        UcItemBalance1.RefreshData()
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If gv1.CurrentColumn Is gv1.Columns(colICode) OrElse gv1.CurrentColumn Is gv1.Columns(colQty) OrElse gv1.CurrentColumn Is gv1.Columns(colprice) OrElse gv1.CurrentColumn Is gv1.Columns(colDisPer) OrElse gv1.CurrentColumn Is gv1.Columns(colDisAmt) OrElse gv1.CurrentColumn Is gv1.Columns(colUnit) OrElse gv1.CurrentColumn Is gv1.Columns(colAmt) Then
                        If gv1.CurrentColumn Is gv1.Columns(colQty) Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                                Dim stockqty As Double = 0
                                If clsCommon.myLen(fndLocation.Value) > 0 And clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 And clsCommon.myLen(gv1.CurrentRow.Cells(colUnit).Value) > 0 Then
                                    stockqty = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), fndLocation.Value, txtDocNo.Value, dtpshipment.Value, Nothing, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), 0)
                                    If clsCommon.myLen(txtnrg.Value) <= 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) > stockqty Then
                                        common.clsCommon.MyMessageBoxShow("Qty more then stock qty not allowed.Availabe Qty : " + clsCommon.myCstr(stockqty))
                                        gv1.CurrentRow.Cells(colQty).Value = 0
                                    End If
                                Else
                                    common.clsCommon.MyMessageBoxShow(Me, "Select the Location", Me.Text)
                                    gv1.CurrentRow.Cells(colQty).Value = 0
                                End If
                            End If
                        End If
                        If gv1.CurrentColumn Is gv1.Columns(colQty) OrElse gv1.CurrentColumn Is gv1.Columns(colprice) OrElse gv1.CurrentColumn Is gv1.Columns(colDisPer) OrElse gv1.CurrentColumn Is gv1.Columns(colDisAmt) OrElse gv1.CurrentColumn Is gv1.Columns(colAmt) Then
                            UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                            UpdateAllTotals()
                            If e.Column Is gv1.Columns(colQty) Then
                                OpenBatchItem()
                            End If
                        ElseIf gv1.CurrentColumn Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                        ElseIf gv1.CurrentColumn Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("scrapsItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
            ''richa agarwal 10 Jan,2020
            If PickCostOFMaterialSaleFromPriceMaster = True Then
                gv1.CurrentRow.Cells(colprice).Value = GetRateMaterialSale(fndLocation.Value, fndcustNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCDate(dtpshipment.Value))
                gv1.CurrentRow.Cells(colprice).ReadOnly = True
            Else
                gv1.CurrentRow.Cells(colprice).ReadOnly = False
            End If
        End If
    End Sub

    ''If Not isCellValueChangedOpen Then
    ''    isCellValueChangedOpen = True
    ''    If gv1.CurrentColumn Is gv1.Columns(colICode) Then
    ''        OpenICodeList(False)


    ''    End If
    ''    isCellValueChangedOpen = False
    ''End If

    ''If Not isCellValueChangedOpen Then
    ''    isCellValueChangedOpen = True

    ''    UpdateCurrentRow((gv1.CurrentRow.Index))
    ''    UpdateAllTotals()
    ''    isCellValueChangedOpen = False
    ''    'gv1.Rows.AddNew()
    ''End If


    ''Catch ex As Exception
    ''    common.clsCommon.MyMessageBoxShow(ex.Message)
    ''End Try
    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colQty)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colprice)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colprice) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colDisPer)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colDisPer) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colDisAmt)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colDisAmt) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colTotTaxAmt)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
        End If
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Try

            Dim strItemType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value)
            If clsCommon.myLen(strItemType) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Row Type", Me.Text)
                Exit Sub
            End If
            If clsCommon.CompairString(strItemType, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                ''RICHA AGARWAL 23/03/2015 CHANGE ITEM TYPE FROM "F" TO "A"
                ''=======================30/03/2017  Monika===================================
                Dim strFAItemType As String = "A"
                If AllowAssetItem Then
                    strFAItemType = ""
                End If
                ''============================================================================

                Dim obj As ClsScrapSaleDetail = ClsScrapSaleDetail.FinderItemGST(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), strFAItemType, isButtonClick, dtpshipment.Value, chkTaxable.Checked, chkBuyBack.Checked)


                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                    gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
                    gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
                    gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
                    gv1.CurrentRow.Cells(colprice).Value = obj.price
                    ''richa agarwal 10 Jan,2020
                    If PickCostOFMaterialSaleFromPriceMaster = True Then
                        Dim dblRate As Double = GetRateMaterialSale(fndLocation.Value, fndcustNo.Value, obj.Item_Code, obj.Unit_Code, clsCommon.myCDate(dtpshipment.Value))
                        If dblRate > 0 Then
                            gv1.CurrentRow.Cells(colprice).Value = dblRate
                            gv1.CurrentRow.Cells(colprice).ReadOnly = True
                        Else
                            Throw New Exception("please create item selling price for item " & obj.Item_Code & "")
                        End If
                    Else
                        gv1.CurrentRow.Cells(colprice).ReadOnly = False
                    End If
                    SetitemWiseTaxSetting(True, True)
                Else
                    gv1.CurrentRow.Cells(colICode).Value = ""
                    gv1.CurrentRow.Cells(colIName).Value = ""
                    gv1.CurrentRow.Cells(colHSNNo).Value = ""
                    gv1.CurrentRow.Cells(colUnit).Value = ""
                    gv1.CurrentRow.Cells(colprice).Value = 0
                    gv1.CurrentRow.Cells(colprice).ReadOnly = False
                End If
            Else
                Dim obj As clsAdditionalCharge = clsAdditionalCharge.GetFinder(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), isButtonClick)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                    gv1.CurrentRow.Cells(colICode).Value = obj.Code
                    gv1.CurrentRow.Cells(colIName).Value = obj.desc
                    gv1.CurrentRow.Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(obj.Code, Nothing)
                    gv1.CurrentRow.Cells(colUnit).Value = Nothing
                    gv1.CurrentRow.Cells(colQty).Value = Nothing
                    gv1.CurrentRow.Cells(colprice).Value = Nothing
                Else
                    SetBlankOfItemColumns()
                End If

            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    '' richa agarwal 10 Jan,2019
    Public Shared Function GetRateMaterialSale(ByVal LocationCode As String, ByVal CustomerCode As String, ByVal Itemcode As String, ByVal Unitcode As String, ByVal Effctv_date As Date)
        Dim tranDate As String = clsCommon.GetPrintDate(Effctv_date, "dd/MMM/yyyy")
        Dim Rate As Double = 0
        Dim qry As String = "select top 1 Price from tspl_material_sale_rate_master inner join tspl_material_sale_rate_location on " _
              & " tspl_material_sale_rate_location.location_code='" & LocationCode & "' and   tspl_material_sale_rate_master.Code=tspl_material_sale_rate_location.Code " _
              & " left outer join tspl_material_sale_rate_customer on tspl_material_sale_rate_customer.customer_code='" & CustomerCode & "'  and   tspl_material_sale_rate_master.Code=tspl_material_sale_rate_customer.Code" _
              & " left join tspl_material_sale_rate_detail on tspl_material_sale_rate_detail.Code=tspl_material_sale_rate_master.code where Item_Code='" & Itemcode & "' " _
              & " and tspl_material_sale_rate_detail.RATE_UOM='" & Unitcode & "' and convert(date,tspl_material_sale_rate_master.Date,103) <=convert(date,'" & tranDate & "',103) and convert(date,tspl_material_sale_rate_master.Effective_date,103) >=convert(date,'" & tranDate & "',103) order by date desc ,tspl_material_sale_rate_master.code desc "
        Rate = clsDBFuncationality.getSingleValue(qry)
        If Rate <= 0 Then
            qry = "select top 1 TSPL_ITEM_UOM_DETAIL.Item_Code,Price,TSPL_ITEM_UOM_DETAIL.Conversion_Factor from tspl_material_sale_rate_master inner join tspl_material_sale_rate_location on " _
             & " tspl_material_sale_rate_location.location_code='" & LocationCode & "' and   tspl_material_sale_rate_master.Code=tspl_material_sale_rate_location.Code " _
             & " left join tspl_material_sale_rate_detail on tspl_material_sale_rate_detail.Code=tspl_material_sale_rate_master.code inner join TSPL_ITEM_UOM_DETAIL on " _
             & " TSPL_ITEM_UOM_DETAIL.Item_Code=tspl_material_sale_rate_detail.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=tspl_material_sale_rate_detail.RATE_UOM where tspl_material_sale_rate_detail.Item_Code='" & Itemcode & "' " _
             & " and convert(date,tspl_material_sale_rate_master.Date,103) <=convert(date,'" & tranDate & "',103) and convert(date,tspl_material_sale_rate_master.Effective_date,103) >=convert(date,'" & tranDate & "',103) order by date desc ,tspl_material_sale_rate_master.code desc "
            Dim Dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If Dt.Rows.Count > 0 Then
                Dim Conv_Fac As Double = clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" & Itemcode & "'  and Uom_Code='" & Unitcode & "' ")
                Rate = Conv_Fac * clsCommon.myCdbl(Dt.Rows(0)("Price")) / IIf(clsCommon.myCdbl(Dt.Rows(0)("Conversion_Factor")) > 0, clsCommon.myCdbl(Dt.Rows(0)("Conversion_Factor")), 1)
                Return Rate
            Else
                Return Rate
            End If
        End If
        Return Rate
    End Function
    'Sub OpenICodeList(ByVal isButtonClick As Boolean)
    '    If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
    '        common.clsCommon.MyMessageBoxShow("Please select Item Type")
    '        SetBlankOfItemColumns()
    '        cboItemType.Focus()
    '        Exit Sub
    '    End If
    '    Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(cboItemType.SelectedValue), isButtonClick)
    '    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
    '        gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
    '        gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
    '        gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
    '        If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "F") = CompairStringResult.Equal Then
    '            Dim strFCode As String = clsItemMaster.GetFatherCode(obj.Item_Code, Nothing)
    '            gv1.CurrentRow.Cells(colFCode).Value = strFCode
    '            If clsCommon.myLen(strFCode) > 0 Then
    '                gv1.CurrentRow.Cells(colFRate).Value = clsItemPriceMaster.GetMRPOfFinishItem(strFCode, obj.Unit_Code)
    '            End If
    '        End If
    '    Else
    '        SetBlankOfItemColumns()
    '    End If
    '    ''End If
    '    Dim objVItem As clsVendorItemDetail = clsVendorItemDetail.GetItemRateAndMRP(fndcustNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
    '    If objVItem IsNot Nothing Then
    '        gv1.CurrentRow.Cells(colRate).Value = objVItem.item_rate
    '        gv1.CurrentRow.Cells(colMRP).Value = objVItem.MRP
    '    End If
    '    SetitemWiseTaxSetting(True, True)
    'End Sub

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colHSNNo).Value = ""
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    'Private Function GetCurrentRowTotalTaxAmt(ByVal IntRowNo As Integer) As Double
    '    Dim dblTotTax As Double = 0
    '    For ii As Integer = 1 To 10
    '        Dim strii As String = clsCommon.myCstr(ii)
    '        If IntRowNo < 0 Then
    '            dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
    '        Else
    '            dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
    '        End If
    '    Next
    '    Return dblTotTax
    'End Function

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
                If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    dblRet = dblRet + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
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
                    gv1.CurrentRow.Cells(colItemwiseTaxCode).Value = Nothing
                End If
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing

            Else
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                If isBlankRate Then
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(colItemwiseTaxCode).Value = Nothing
                End If
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing

            End If
        Next
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
        btnInvoiceJE.Visible = False
        BlankAllControls()

        LoadBlankGrid()
        LoadBlankGridTax()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        'txtDate.Focus()
        gv1.Rows.AddNew()
        gv1.Rows(gv1.RowCount - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem

        gvadd.AllowAddNewRow = False
        gvadd.Rows.AddNew()
        ''richa agarwal 18/03/2015
        LoadInvoiceType()
        btnReverse.Visible = False
        chkBuyBack.Visible = True
        chkBuyBack.Checked = False
        btnPrint.Visible = True
        ''------------------
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

    End Sub

    Private Sub gv1_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEditorInitialized
        If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Description", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub

    Function AllowToSave() As Boolean
        'Try
        ' KUNAL > TICKET : BM00000009580 ========
        If AllowFutureDateTransaction(dtpshipment.Value, Nothing) = False Then
            dtppost.Focus()
            Return False
        End If
        If CalculateTaxRatefromItemwsieTaxOnSale = 1 Then
            SetitemWiseTaxSetting(True, False)
        End If
        If btnSave.Text = "Update" Then
            Dim strchk As String = "select ispost from tspl_scrapsale_head where shipment_No='" + txtDocNo.Value + "'"
            Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow(Me, "Transaction already posted", Me.Text)
                Return False
            End If
        End If
        For ii As Integer = 0 To gv1.Rows.Count - 1
            UpdateCurrentRow(ii)
        Next
        UpdateAllTotals()
        If clsCommon.myLen(fndcustNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Customer", Me.Text)
            fndcustNo.Focus()
            Return False
        End If
        If chkTaxable.Checked Then
            ' Check if the vehicle number is empty
            If clsCommon.myLen(TxtVehicleCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Vehicle No", Me.Text)
                TxtVehicleCode.Focus()
                Return False
            End If
        End If

        GSTStatus = clsERPFuncationality.GetGSTStatus(dtpshipment.Value)
        If GSTStatus = False OrElse (chkTaxable.Checked AndAlso GSTStatus = True) Then
            If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group", Me.Text)
                txtTaxGroup.Focus()
                Return False
            End If
        End If

        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select  From Location", Me.Text)
            fndLocation.Focus()
            Return False
        End If
        If AllowtoChangeTCSBaseAmount Then
            If clsCommon.myCdbl(txttcstaxbaseamount.Value) > clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text) Then
                Throw New Exception("TCS Tax Base amount should not be greater than Actual TCS Tax Base Amount")
            End If
        End If
        If CreatVatSeriesOnExciseInvoice = 1 Then
            If clsCommon.myLen(fndShipToLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select  To Location", Me.Text)
                fndShipToLocation.Focus()
                Return False
            End If
        End If
        If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Shipment No Not found to save", Me.Text)
            txtDocNo.Focus()
            Return False
        End If

        'If isALlowVehicleGateOutValidation = True Then
        '    If clsCommon.myLen(txtvehicle_mannual_no.Text) > 0 Then
        '        Dim qry1 As String = String.Empty
        '        qry1 = " SELECT Stuff((SELECT N', ' + TSPL_SCRAPSALE_HEAD.shipment_No FROM TSPL_SCRAPSALE_HEAD left join TSPL_SCRAPSALE_GATE_OUT on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_GATE_OUT.Shipment_No  where TSPL_SCRAPSALE_HEAD.Vehicle_code='" & txtvehicle_mannual_no.Text & "' and  TSPL_SCRAPSALE_GATE_OUT.Shipment_No is null FOR XML PATH(''),TYPE).value('text()[1]','nvarchar(max)'),1,2,N'') "
        '        Dim result As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1))
        '        If clsCommon.myLen(result) > 0 Then
        '            common.clsCommon.MyMessageBoxShow("Vehicle No ('" & txtvehicle_mannual_no.Text & "') used in other Shipment No. You can create new Shipment with Vehicle No ('" & txtvehicle_mannual_no.Text & "')  After  Gate Out following Shipment No : '" & result & "'")
        '            Return False

        '        End If
        '    End If
        'End If


        For i As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Dim qty As Decimal = clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value)
                Dim Rate As Decimal = clsCommon.myCdbl(gv1.Rows(i).Cells(colprice).Value)
                If clsCommon.myLen(gv1.Rows(i).Cells(colICode).Value) > 0 And qty = 0 Then
                    Dim str As String = clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value)
                    common.clsCommon.MyMessageBoxShow(Me, "Shipped qty can't be zero for Item '" + str + "'", Me.Text)
                    Return False
                End If

                'Sanjay 03/07/2018 Check Rate
                If clsCommon.myLen(gv1.Rows(i).Cells(colICode).Value) > 0 And Rate = 0 Then
                    Dim str As String = clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value)
                    common.clsCommon.MyMessageBoxShow(Me, "Rate can't be zero for Item '" + str + "'", Me.Text)
                    Return False
                End If
            Else
                If clsCommon.myLen(gv1.Rows(i).Cells(colAmt).Value) <= 0 Then
                    Dim str As String = clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value)
                    common.clsCommon.MyMessageBoxShow(Me, "Shipped Amount can't be zero for '" + str + "'", Me.Text)
                    Return False
                End If
            End If
        Next


        Dim arrItem As New List(Of String)
        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)

            If clsCommon.myLen(strICode) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                    If Not arrItem.Contains(strICode) Then
                        arrItem.Add(strICode)
                    End If

                    If clsCommon.myLen(strUOM) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please enter UOM of Item - " + strICode + ".At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ", Me.Text)
                        Return False
                    End If
                    ''For RM Other balance Qty check And works only for one unit.
                    Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                    Dim dblBalQty As Double = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(strICode, fndLocation.Value, txtDocNo.Value, dtpshipment.Value, Nothing, strUOM)
                    Dim dblEnteredQty As Double = dblQty
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        If ii = jj Then
                            Continue For
                        End If
                        Dim strICodeInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                        Dim strUOMInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)
                        Dim dblQtyInner As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colQty).Value)
                        Dim dblInnerConvFac As Double = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, Nothing)
                        If dblQtyInner > 0 AndAlso clsCommon.CompairString(strICodeInner, strICode) = CompairStringResult.Equal Then
                            dblEnteredQty += dblQtyInner
                        End If
                    Next
                    dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                    If dblEnteredQty > dblBalQty Then
                        common.clsCommon.MyMessageBoxShow(Me, "Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty), Me.Text)
                        Return False
                    End If
                    If RunBatchFifowise = 1 Then
                        gv1.CurrentRow = gv1.Rows(ii)
                        OpenBatchItem()
                    End If
                    If dblQty > 0 AndAlso clsCommon.myCBool(clsDBFuncationality.getSingleValue("select TSPL_ITEM_MASTER.Is_Batch_Item  from TSPL_ITEM_MASTER where TSPL_ITEM_MASTER.Item_Code ='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + "'", Nothing)) Then
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
                End If
            End If
        Next

        If clsERPFuncationality.GetGSTStatus(dtpshipment.Value) Then
            clsItemMaster.isItemTaxableOrNonTaxable(arrItem, chkTaxable.Checked)
        Else
            Dim strItemcode As String = String.Empty
            Dim count As Double = 0
            For i As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(i).Cells(colICode).Value) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        strItemcode = clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value)
                        If clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_item_master where Is_Tax_Exempted =2 and Item_Code = ('" & strItemcode & "')") = 1 Then
                            count = count + 1
                        Else
                            count = 0
                        End If
                    End If
                End If
            Next
            If ddlInvoiceType.SelectedValue = "E" And count <> 0 Then
                Dim qry As String = "select 1 from TSPL_TAX_GROUP_MASTER where Tax_Group_Type='s' and Excisable='Y' and Tax_Group_Code='" + txtTaxGroup.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Tax Group should be of excise type", Me.Text)
                    Return False
                End If
            End If
            If chkinvoice.Checked Then
                If AllowChangeInvoiceType Then
                    If clsCommon.myLen(ddlInvoiceType.SelectedValue) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please select invoice Type for creating auto invoice", Me.Text)
                        ddlInvoiceType.Focus()
                        Return False
                    Else
                        If InvoiceType() = False Then
                            Return False
                        End If
                    End If
                Else
                    InvoiceType()
                End If
            End If
        End If
        ''----------------------------

        UcCustomFields1.AllowToSave()
        UcAttachment1.AllowToSave()
        If GSTStatus = True AndAlso chkTaxable.Checked Then
            clsLocationWiseTax.IsValidTaxGroup(txtTaxGroup.Value, fndLocation.Value, fndcustNo.Value, "S", dtpshipment.Value, Nothing)
        End If
        If GSTStatus Then
            If chkTaxable.Checked Then
                ddlInvoiceType.SelectedValue = "T"
            Else
                ddlInvoiceType.SelectedValue = "N"
            End If
        End If

        ' txtFreightDistance.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Distance from TSPL_LOCATION_DISTANCE_MAPPING where TransType='S' and Location_Code='" & fndLocation.Value & "' and Customer_Code='" & fndcustNo.Value & "'", Nothing))
        Dim ECustomerType As String = clsERPFuncationality.GetCustomerEInvoiceType(fndcustNo.Value, Nothing)
        If objCommonVar.GenerateEWayBillWithEInvoice = True AndAlso clsCommon.CompairString(ECustomerType, "BB") = CompairStringResult.Equal AndAlso chkTaxable.Checked = True AndAlso clsERPFuncationality.GetEInvoiceStatus(dtpshipment.Value) = True Then
            'If clsCommon.myCdbl(txtFreightDistance.Value) <= 0 Then
            '    Throw New Exception("Please define Freight Distance in EWay Bill Distance Master.")
            'End If
            If clsCommon.myLen(txtTransporter_Code.Value) <= 0 Then
                Throw New Exception("Pls Select Transporter")
                txtTransporter_Code.Focus()
                Return False
            End If
            If clsCommon.myLen(TxtVehicleCode.Value) <= 0 Then
                Throw New Exception("Please Select Vehicle")
                TxtVehicleCode.Focus()
                Return False
            End If
            'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select GSTRegistered from tspl_vendor_master where vendor_code='" & txtTransporter_Code.Value & "'", Nothing)) = 0 Then
            '    Throw New Exception("Please Update GSTIN in Transpoter/Vendor Master")
            '    Return False
            'End If
        ElseIf clsCommon.CompairString(ECustomerType, "BC") = CompairStringResult.Equal AndAlso chkTaxable.Checked = True Then
            Throw New Exception("Please Update GSTIN in Customer Master")
            Return False
        End If
        Return True
    End Function
    ''richa agarwal 18/03/2015
    Sub LoadInvoiceType()
        ddlInvoiceType.DataSource = GetInvoiceType()
        ddlInvoiceType.ValueMember = "Code"
        ddlInvoiceType.DisplayMember = "Name"
    End Sub
    Public Shared Function GetInvoiceType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Retail"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "T"
        dr("Name") = "Tax"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Excise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "I"
        dr("Name") = "Invoice"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "Non Taxable"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Function InvoiceType() As Boolean

        Dim dt As DataTable
        Dim strloc As String
        Dim qry As String

        strloc = fndLocation.Value
        qry = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State, " &
          "TSPL_LOCATION_MASTER.Sales_Tax_Group as LocalTaxGroup,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Local_Tax_GroupName, " &
          "TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as InterstateTaxGroup,TSPL_TAX_GROUP_MASTERIS.Tax_Group_Desc as Interstate_Tax_GroupName " &
          "FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_TAX_GROUP_MASTER as TSPL_TAX_GROUP_MASTERIS on TSPL_TAX_GROUP_MASTERIS.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTERIS.Tax_Group_Type='S' " &
          "WHERE TSPL_LOCATION_MASTER.Location_Code = '" + strloc + "'"


        dt = clsDBFuncationality.GetDataTable(qry)
        Dim strLocState As String = clsCommon.myCstr(dt.Rows(0)("State"))

        qry = "select Price_Code,price_CodeNon,State,Tin_No from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndcustNo.Value + "'"
        dt = clsDBFuncationality.GetDataTable(qry)
        Dim strCustState As String = clsCommon.myCstr(dt.Rows(0)("State"))
        Dim strTinNo As String = clsCommon.myCstr(dt.Rows(0)("Tin_No"))

        If AllowChangeInvoiceType = False Then
            If clsCommon.myLen(strTinNo) > 0 AndAlso clsCommon.CompairString(strLocState, strCustState) = CompairStringResult.Equal Then
                ddlInvoiceType.SelectedValue = "T"
            Else
                ddlInvoiceType.SelectedValue = "R"
                If CreatVatSeriesOnExciseInvoice = 1 Then
                    If clsCommon.CompairString(strLocState, strCustState) = CompairStringResult.Equal Then
                        ddlInvoiceType.SelectedValue = "R"
                    Else
                        ddlInvoiceType.SelectedValue = "I" 'Interstate series
                    End If
                End If

            End If

            ''richa agarwal 
            Dim strItemcode As String = String.Empty
            Dim count As Double = 0
            For i As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(i).Cells(colICode).Value) > 0 Then
                    strItemcode = clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value)

                    If clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_item_master where Is_Tax_Exempted =2 and Item_Code = ('" & strItemcode & "')") = 1 Then
                        count = count + 1
                    Else
                        count = 0
                    End If
                End If
            Next
            ''---------------------
            strExcise = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'")) = "T", True, False)
            If strExcise = True AndAlso count <> 0 Then
                ddlInvoiceType.SelectedValue = "E"
                'Else
                '    clsCommon.MyMessageBoxShow("Please select Excisable Location and Item to make invoice type Excise")
                '    Return False
            End If
            ''richa agarwal
            'If clsCommon.CompairString(ddlInvoiceType.SelectedValue, "E") = CompairStringResult.Equal AndAlso strExcise = False Then
            '    clsCommon.MyMessageBoxShow("Please select Excisable Location and Item to make invoice type Excise")
            '    Return False
            'End If
            ''------------------------


        Else
            If clsCommon.myLen(ddlInvoiceType.SelectedValue) > 0 Then
                Dim strInvoiceType As String = String.Empty
                Dim strInvoiceTypeDesc As String = String.Empty
                If clsCommon.myLen(strTinNo) > 0 AndAlso clsCommon.CompairString(strLocState, strCustState) = CompairStringResult.Equal Then
                    strInvoiceType = "T"
                Else
                    strInvoiceType = "R"
                End If
                ''richa agarwal 
                Dim strItemcode As String = String.Empty
                Dim count As Double = 0
                For i As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myLen(gv1.Rows(i).Cells(colICode).Value) > 0 Then
                        strItemcode = clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value)

                        If clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_item_master where Is_Tax_Exempted =2 and Item_Code = ('" & strItemcode & "')") = 1 Then
                            count = count + 1
                        Else
                            count = 0
                        End If
                    End If
                Next
                ''------------------
                strExcise = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'")) = "T", True, False)
                If strExcise = True AndAlso count <> 0 Then
                    strInvoiceType = "E"
                    'Else
                    '    clsCommon.MyMessageBoxShow("Please select Excisable Location and Item to make invoice type Excise")
                    '    Return False
                End If
                ''richa agarwal
                'If clsCommon.CompairString(ddlInvoiceType.SelectedValue, "E") = CompairStringResult.Equal AndAlso strExcise = False Then
                '    clsCommon.MyMessageBoxShow("Please select Excisable Location and Item to make invoice type Excise")
                '    Return False
                'End If
                ''------------------------
                If Not clsCommon.CompairString(strInvoiceType, ddlInvoiceType.SelectedValue) = CompairStringResult.Equal Then
                    If strInvoiceType = "T" Then
                        strInvoiceTypeDesc = "Tax"
                    ElseIf strInvoiceType = "R" Then
                        strInvoiceTypeDesc = "Retail"
                    ElseIf strInvoiceType = "E" Then
                        strInvoiceTypeDesc = "Excise"
                    ElseIf strInvoiceType = "I" Then
                        strInvoiceTypeDesc = "Invoice"
                    End If
                    If (common.clsCommon.MyMessageBoxShow(Me, "System is generating " & strInvoiceTypeDesc & "  Invoice Type.Do you still want to continue ?  ", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No) Then
                        Return False
                    Else
                        ddlInvoiceType.SelectedValue = strInvoiceType
                        Return True
                    End If
                End If
            End If

        End If

        Return True
    End Function
    ''-----------------------------
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub


    Sub SaveData()
        Try
            '' Anubhooti 13-Sep-2014 BM00000003735
            If FrmMainTranScreen.ValidateTransactionAccToFinYear("Material Sales", dtpshipment.Value) = False Then
                Exit Sub
            End If
            ''
            If (AllowToSave()) Then

                If clsCommon.myLen(fndLocation.Value) > 0 Then
                    Dim LocSegmentCode As String = fndLocation.Value
                    Dim locDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + LocSegmentCode + "'")
                End If

                Dim obj As New ClsScrapSaleHead()
                obj.shipment_No = txtDocNo.Value
                obj.strInvoiceNo = lblInvoiceNo.Text
                If chkOnHold.Checked = True Then
                    obj.Status = 1
                Else
                    obj.Status = 0
                End If
                obj.Weighment_Code = txtGWeighmentNo.Value
                obj.Po_No = txtponumber.Text
                obj.NRG_No = clsCommon.myCstr(txtnrg.Value)
                obj.cust_Code = fndcustNo.Value
                obj.cust_Name = txtcustdesc.Text
                obj.shipment_Date = dtpshipment.Value
                obj.posting_Date = dtppost.Value
                obj.expship_Date = dtpexp.Value
                obj.Loc_Code = fndLocation.Value
                obj.Vehicle_Id = TxtVehicleCode.Value
                obj.Loc_Name = txtlocation.Text
                obj.Transporter_code = txtTransporter_Code.Value
                obj.Transporter_Name = txtTransporter_desc.Text
                obj.Vehicle_code = TxtVehicleCode.Text
                obj.Freight_Distance = txtFreightDistance.Value
                obj.ToLoc_Code = fndShipToLocation.Value
                obj.Is_Taxable = chkTaxable.Checked
                obj.IsBuyBack = chkBuyBack.Checked
                If chkinvoice.Checked = True Then
                    obj.CreateInvoice = 1
                Else
                    obj.CreateInvoice = 0
                End If
                'If chkExcisable.Checked = True Then
                '    obj.Excisable = "Y"
                'Else
                '    obj.Excisable = "N"
                'End If

                obj.ActualTCSBaseAmount = clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text)
                obj.ChangedTCSBaseAmount = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                obj.Excisable = "N"
                obj.Invoice_Type = ddlInvoiceType.SelectedValue
                If clsCommon.CompairString(obj.Invoice_Type, "E") = CompairStringResult.Equal Then
                    obj.Excisable = "Y"
                End If
                ''--------------
                obj.Inter_Branch = chkInterBranch.Checked
                'obj.Description = txtdescription.Text
                obj.reff = txtref.Text
                obj.Tax_Group = txtTaxGroup.Value
                obj.Tax_Desc = lblTaxGrpName.Text
                obj.Add_Amt = txtaddamt.Text
                obj.Before_Add_Amt = lblTotRAmt.Text
                obj.Discount_Base = lblAmtWithDiscount.Text
                obj.Discount_Amt = lblDiscountAmt.Text
                obj.Amount_Less_Discount = lblAmtAfterDiscount.Text
                obj.Total_Tax_Amt = lblTaxAmt.Text
                obj.ship_Total_Amt = lblTotRAmt.Text
                obj.doc_Amt = lbldocamt.Text

                obj.RoundOffAmount = clsCommon.myCdbl(txtRoundOff.Text)

                obj.Is_CashSale = IIf(chkCashSale.Checked, "Y", "N")
                'obj.On_Hold = chkOnHold.Checked
                If chkScrapSale.Checked = True Then
                    obj.Is_Scrap = "Y"
                Else
                    obj.Is_Scrap = "N"
                End If
                obj.Doc_Type = "S"
                If (gv2.Rows.Count > 0) Then
                    obj.TAX1 = clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value)
                    obj.TAX1_Rate = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxRate).Value)
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTBaseAmt).Value)
                    obj.TAX1_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxAmt).Value)
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

                If (gvadd.Rows.Count > 0) Then
                    obj.AddCode1 = clsCommon.myCstr(gvadd.Rows(0).Cells(coladdcode).Value)
                    obj.AddDesc1 = clsCommon.myCstr(gvadd.Rows(0).Cells(coladddesc).Value)
                    obj.AddAmt1 = clsCommon.myCdbl(gvadd.Rows(0).Cells(coladdamt).Value)
                End If
                If (gvadd.Rows.Count > 1) Then
                    obj.AddCode2 = clsCommon.myCstr(gvadd.Rows(1).Cells(coladdcode).Value)
                    obj.AddDesc2 = clsCommon.myCstr(gvadd.Rows(1).Cells(coladddesc).Value)
                    obj.AddAmt2 = clsCommon.myCdbl(gvadd.Rows(1).Cells(coladdamt).Value)
                End If
                If (gvadd.Rows.Count > 2) Then
                    obj.AddCode3 = clsCommon.myCstr(gvadd.Rows(2).Cells(coladdcode).Value)
                    obj.AddDesc3 = clsCommon.myCstr(gvadd.Rows(2).Cells(coladddesc).Value)
                    obj.AddAmt3 = clsCommon.myCdbl(gvadd.Rows(2).Cells(coladdamt).Value)
                End If
                If (gvadd.Rows.Count > 3) Then
                    obj.AddCode4 = clsCommon.myCstr(gvadd.Rows(3).Cells(coladdcode).Value)
                    obj.AddDesc4 = clsCommon.myCstr(gvadd.Rows(3).Cells(coladddesc).Value)
                    obj.AddAmt4 = clsCommon.myCdbl(gvadd.Rows(3).Cells(coladdamt).Value)
                End If
                If (gvadd.Rows.Count > 4) Then
                    obj.AddCode5 = clsCommon.myCstr(gvadd.Rows(4).Cells(coladdcode).Value)
                    obj.AddDesc5 = clsCommon.myCstr(gvadd.Rows(4).Cells(coladddesc).Value)
                    obj.AddAmt5 = clsCommon.myCdbl(gvadd.Rows(4).Cells(coladdamt).Value)
                End If
                If (gvadd.Rows.Count > 5) Then
                    obj.AddCode6 = clsCommon.myCstr(gvadd.Rows(5).Cells(coladdcode).Value)
                    obj.AddDesc6 = clsCommon.myCstr(gvadd.Rows(5).Cells(coladddesc).Value)
                    obj.AddAmt6 = clsCommon.myCdbl(gvadd.Rows(5).Cells(coladdamt).Value)
                End If
                If (gvadd.Rows.Count > 6) Then
                    obj.AddCode7 = clsCommon.myCstr(gvadd.Rows(6).Cells(coladdcode).Value)
                    obj.AddDesc7 = clsCommon.myCstr(gvadd.Rows(6).Cells(coladddesc).Value)
                    obj.AddAmt7 = clsCommon.myCdbl(gvadd.Rows(6).Cells(coladdamt).Value)
                End If
                If (gvadd.Rows.Count > 7) Then
                    obj.AddCode8 = clsCommon.myCstr(gvadd.Rows(7).Cells(coladdcode).Value)
                    obj.AddDesc8 = clsCommon.myCstr(gvadd.Rows(7).Cells(coladddesc).Value)
                    obj.AddAmt8 = clsCommon.myCdbl(gvadd.Rows(7).Cells(coladdamt).Value)
                End If
                If (gvadd.Rows.Count > 8) Then
                    obj.AddCode9 = clsCommon.myCstr(gvadd.Rows(8).Cells(coladdcode).Value)
                    obj.AddDesc9 = clsCommon.myCstr(gvadd.Rows(8).Cells(coladddesc).Value)
                    obj.AddAmt9 = clsCommon.myCdbl(gvadd.Rows(8).Cells(coladdamt).Value)
                End If
                If (gvadd.Rows.Count > 9) Then
                    obj.AddCode10 = clsCommon.myCstr(gvadd.Rows(8).Cells(coladdcode).Value)
                    obj.AddDesc10 = clsCommon.myCstr(gvadd.Rows(8).Cells(coladddesc).Value)
                    obj.AddAmt10 = clsCommon.myCdbl(gvadd.Rows(8).Cells(coladdamt).Value)
                End If
                obj.Terms_Code = txtTermCode.Value
                obj.Due_Date = txtDueDate.Value
                If rbtnTaxCalAutomatic.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                ElseIf rbtnTaxCalManual.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
                End If

                obj.Total_Gross_Weight = clsCommon.myCdbl(lblGrossWeight.Text)
                obj.Total_Net_Weight = clsCommon.myCdbl(lblNetWeight.Text)
                obj.Arr = New List(Of ClsScrapSaleDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New ClsScrapSaleDetail()
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Row_Type = clsCommon.myCstr(grow.Cells(colRowType).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.shipped_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.price = clsCommon.myCdbl(grow.Cells(colprice).Value)
                    objTr.DiscountPer = clsCommon.myCdbl(grow.Cells(colDisPer).Value)
                    objTr.DiscountAmt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                    'objTr.Tax = clsCommon.myCdbl(grow.Cells(coltax).Value)
                    objTr.ItemAmt = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.TotalTaxAmt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                    objTr.NetPriceAmt = clsCommon.myCdbl(grow.Cells(colnetprice).Value)
                    objTr.TotalDiscountAmt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                    objTr.ItemNetAmt = clsCommon.myCdbl(grow.Cells(colitemnetamt).Value)
                    objTr.TotalAmt = clsCommon.myCdbl(grow.Cells(coltotamt).Value)
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
                    objTr.pending_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Specification = clsCommon.myCstr(grow.Cells(colSp).Value)
                    objTr.ItemwiseTaxCode = clsCommon.myCstr(grow.Cells(colItemwiseTaxCode).Value)
                    objTr.arrBatchItem = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventory))
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
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

                If (obj.SaveData(obj, lblInvoiceNo.Text, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.shipment_No)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.shipment_No, NavigatorType.Current)
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()
            LoadBlankGridTax()
            gvadd.DataSource = Nothing
            gvadd.Rows.Clear()
            fndcustNo.Enabled = False
            chkTaxable.Enabled = False
            fndLocation.Enabled = False
            chkExcisable.Enabled = False
            'fndLocation.Enabled = False

            Dim objin As ClsScrapInvoiceHead = ClsScrapInvoiceHead.GetDataShipment(strCode)
            If (objin IsNot Nothing AndAlso clsCommon.myLen(objin.shipment_No) > 0) Then
                If clsCommon.myLen(objin.EWayBillNo) > 0 Then
                    txtEWayBillNo.Text = objin.EWayBillNo
                    txtEWayBillRemarks.Text = objin.EwayBillRemarks
                    If clsCommon.myLen(objin.EwayBillValidDate) > 0 Then
                        txtEwayValidDate.Value = objin.EwayBillValidDate
                    End If
                    btnEWaybillUpdate.Enabled = False
                    If clsCommon.myLen(objin.EWayBillDate) > 0 Then
                        txtEWayBillDate.Value = objin.EWayBillDate
                    End If

                Else
                    btnEWaybillUpdate.Enabled = True
                End If

                If clsCommon.myLen(objin.EInvoiceIRNNo) > 0 Then
                    EInvoiceIRNNo.Text = objin.EInvoiceIRNNo
                    EinvoiceAckNo.Text = objin.EInvoiceAckNo
                    If clsCommon.myLen(objin.EInvoiceAckDate) > 0 Then
                        txtAckDate.Value = objin.EInvoiceAckDate
                    End If
                    EInvoiceQrCode.Text = objin.EInvoiceQRCode
                    EinvoiceBtnUpdate.Enabled = False
                Else
                    EinvoiceBtnUpdate.Enabled = True
                End If
            End If
            '  End If
            Dim obj As New ClsScrapSaleHead()
            obj = ClsScrapSaleHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.shipment_No) > 0) Then
                If obj.ispost = 1 Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    'repoComplete.IsVisible = True
                    'repoBalQty.IsVisible = True
                    btnCancel.Enabled = True
                Else
                    btnCancel.Enabled = False
                End If

                UsLock1.Status = obj.ispost
                txtDocNo.Value = obj.shipment_No
                'txtinvoice.Text = obj.invoice_No
                'chkOnHold.Checked = obj.Status
                If obj.Status = 1 Then
                    chkOnHold.Checked = True
                Else
                    chkOnHold.Checked = False
                End If
                txtGWeighmentNo.Value = obj.Weighment_Code
                lblInvoiceNo.Text = obj.strInvoiceNo
                txtponumber.Text = obj.Po_No
                txtnrg.Value = obj.NRG_No
                fndcustNo.Value = obj.cust_Code
                txtcustdesc.Text = obj.cust_Name
                dtpshipment.Value = clsCommon.myCDate(obj.shipment_Date)
                dtppost.Value = clsCommon.myCDate(obj.posting_Date)
                dtpexp.Value = obj.expship_Date
                fndLocation.Value = obj.Loc_Code
                txtlocation.Text = obj.Loc_Name
                TxtVehicleCode.Value = obj.Vehicle_Id
                'txt.Text = obj.Vehicle_code
                txtTransporter_Code.Value = obj.Transporter_code
                txtTransporter_desc.Text = obj.Transporter_Name
                txtFreightDistance.Value = obj.Freight_Distance
                txtVehicleDesc.Text = ClsScrapSaleHead.GetVehicleDesc(TxtVehicleCode.Value, Nothing)
                fndShipToLocation.Value = obj.ToLoc_Code
                lblDocAmount.Text = obj.doc_Amt
                txtRoundOff.Text = obj.RoundOffAmount
                ''richa agarwal 19/03/2015
                ddlInvoiceType.SelectedValue = obj.Invoice_Type
                txtVatInvNo.Text = obj.VAT_InvoiceNo
                If obj.Is_Scrap = "Y" Then
                    chkScrapSale.Checked = True
                Else
                    chkScrapSale.Checked = False
                End If
                chkCashSale.Checked = IIf(obj.Is_CashSale = "Y", True, False)
                chkCashSale.Enabled = False
                chkScrapSale.Enabled = False


                If obj.CreateInvoice = 1 Then
                    chkinvoice.Checked = True
                Else
                    chkinvoice.Checked = False
                End If

                If obj.Excisable = "Y" Then
                    chkExcisable.Checked = True
                Else
                    chkExcisable.Checked = False
                End If

                lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(obj.ActualTCSBaseAmount)
                txttcstaxbaseamount.Value = clsCommon.myCdbl(obj.ChangedTCSBaseAmount)
                'txtdescription.Text = obj.Description
                txtref.Text = obj.reff
                txtTaxGroup.Value = obj.Tax_Group
                lblTaxGrpName.Text = obj.Tax_Desc
                txtaddamt.Text = obj.Add_Amt
                lblTotRAmt.Text = obj.Before_Add_Amt
                lblAmtWithDiscount.Text = obj.Discount_Base
                lblDiscountAmt.Text = obj.Discount_Amt
                lblAmtAfterDiscount.Text = obj.Amount_Less_Discount
                lblTaxAmt.Text = obj.Total_Tax_Amt
                lblTotRAmt.Text = obj.ship_Total_Amt
                lbldocamt.Text = obj.doc_Amt
                txtTermCode.Value = obj.Terms_Code
                txtDueDate.Value = obj.Due_Date
                chkInterBranch.Checked = obj.Inter_Branch
                chkTaxable.Checked = obj.Is_Taxable
                chkBuyBack.Checked = obj.IsBuyBack

                '' If clsCommon.myLen(obj.EWayBillNo) > 0 Then
                'txtEWayBillNo.Text = obj.EWayBillNo
                'txtEWayBillRemarks.Text = obj.EwayBillRemarks
                'If clsCommon.myLen(obj.EwayBillValidDate) > 0 Then
                '    txtEwayValidDate.Value = obj.EwayBillValidDate
                'End If
                ''btnEWaybillUpdate.Enabled = False
                'If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                '    txtEWayBillDate.Value = obj.EWayBillDate
                'End If

                ''Else
                'btnEWaybillUpdate.Enabled = True
                ''  End If

                ''If clsCommon.myLen(obj.EInvoiceIRNNo) > 0 Then
                'EInvoiceIRNNo.Text = obj.EInvoiceIRNNo
                'EinvoiceAckNo.Text = obj.EInvoiceAckNo
                'If clsCommon.myLen(obj.EInvoiceAckDate) > 0 Then
                '    txtAckDate.Value = obj.EInvoiceAckDate
                'End If
                'EInvoiceQrCode.Text = obj.EInvoiceQRCode
                'EinvoiceBtnUpdate.Enabled = False
                '' Else
                ''      EinvoiceBtnUpdate.Enabled = True
                '' End If

                'txtElectronicRefNo.Text = obj.Electronic_Ref_No
                EInvoiceType = clsERPFuncationality.GetCustomerEInvoiceTypeFromTransationTable("TSPL_SCRAPINVOICE_HEAD", "invoice_no", lblInvoiceNo.Text, Nothing)

                If chkTaxable.Checked = True AndAlso clsERPFuncationality.GetEInvoiceStatus(dtpshipment.Value) = True AndAlso clsCommon.CompairString(EInvoiceType, "BB") = CompairStringResult.Equal Then
                    btnReverse.Enabled = False
                    If objCommonVar.GenerateEWayBillWithEInvoice = True Then
                        txtEWayBillNo.ReadOnly = True
                        txtEWayBillDate.ReadOnly = True
                    End If
                    If obj.ispost = ERPTransactionStatus.Approved Then
                        btnCancel.Enabled = True
                    ElseIf obj.ispost = ERPTransactionStatus.Pending Then
                        btnCancel.Enabled = False
                    End If
                End If

                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForSale(obj.Tax_Group)
                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX1
                    If clsCommon.CompairString(obj.TAX1, "TCS") = CompairStringResult.Equal Then
                        lblActualTCSTaxBaseAmt.Text = obj.TAX1_Base_Amt
                    End If
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX1_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX1_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX1_Amt
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
                    If clsCommon.CompairString(obj.TAX2, "TCS") = CompairStringResult.Equal Then
                        lblActualTCSTaxBaseAmt.Text = obj.TAX2_Base_Amt
                    End If
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX2_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX2_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX2_Amt
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
                    If clsCommon.CompairString(obj.TAX3, "TCS") = CompairStringResult.Equal Then
                        lblActualTCSTaxBaseAmt.Text = obj.TAX3_Base_Amt
                    End If
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX3_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX3_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX3_Amt
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
                    If clsCommon.CompairString(obj.TAX4, "TCS") = CompairStringResult.Equal Then
                        lblActualTCSTaxBaseAmt.Text = obj.TAX4_Base_Amt
                    End If
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX4_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX4_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX4_Amt
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

                'If (clsCommon.myLen(obj.AddCode1) = 0) Then
                '    gvadd.Rows.AddNew()
                'End If

                If (clsCommon.myLen(obj.AddCode1) > 0) Then
                    gvadd.Rows.AddNew()
                    isCellValueChangedOpenAdd = True
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdcode).Value = obj.AddCode1
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladddesc).Value = obj.AddDesc1
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdamt).Value = obj.AddAmt1
                    isCellValueChangedOpenAdd = False

                End If
                If (clsCommon.myLen(obj.AddCode2) > 0) Then
                    gvadd.Rows.AddNew()
                    isCellValueChangedOpenAdd = True
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdcode).Value = obj.AddCode2
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladddesc).Value = obj.AddDesc2
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdamt).Value = obj.AddAmt2
                    isCellValueChangedOpenAdd = False
                End If
                If (clsCommon.myLen(obj.AddCode3) > 0) Then
                    gvadd.Rows.AddNew()
                    isCellValueChangedOpenAdd = True
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdcode).Value = obj.AddCode3
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladddesc).Value = obj.AddDesc3
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdamt).Value = obj.AddAmt3
                    isCellValueChangedOpenAdd = False
                End If
                If (clsCommon.myLen(obj.AddCode4) > 0) Then
                    gvadd.Rows.AddNew()
                    isCellValueChangedOpenAdd = True
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdcode).Value = obj.AddCode4
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladddesc).Value = obj.AddDesc4
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdamt).Value = obj.AddAmt4
                    isCellValueChangedOpenAdd = False
                End If
                If (clsCommon.myLen(obj.AddCode5) > 0) Then
                    gvadd.Rows.AddNew()
                    isCellValueChangedOpenAdd = True
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdcode).Value = obj.AddCode5
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladddesc).Value = obj.AddDesc5
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdamt).Value = obj.AddAmt5
                    isCellValueChangedOpenAdd = False
                End If
                If (clsCommon.myLen(obj.AddCode6) > 0) Then
                    gvadd.Rows.AddNew()
                    isCellValueChangedOpenAdd = True
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdcode).Value = obj.AddCode6
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladddesc).Value = obj.AddDesc6
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdamt).Value = obj.AddAmt6
                    isCellValueChangedOpenAdd = False
                End If
                If (clsCommon.myLen(obj.AddCode7) > 0) Then
                    gvadd.Rows.AddNew()
                    isCellValueChangedOpenAdd = True
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdcode).Value = obj.AddCode7
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladddesc).Value = obj.AddDesc7
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdamt).Value = obj.AddAmt7
                    isCellValueChangedOpenAdd = False
                End If
                If (clsCommon.myLen(obj.AddCode8) > 0) Then
                    gvadd.Rows.AddNew()
                    isCellValueChangedOpenAdd = True
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdcode).Value = obj.AddCode8
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladddesc).Value = obj.AddDesc8
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdamt).Value = obj.AddAmt8
                    isCellValueChangedOpenAdd = False
                End If
                If (clsCommon.myLen(obj.AddCode9) > 0) Then
                    gvadd.Rows.AddNew()
                    isCellValueChangedOpenAdd = True
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdcode).Value = obj.AddCode9
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladddesc).Value = obj.AddDesc9
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdamt).Value = obj.AddAmt9
                    isCellValueChangedOpenAdd = False
                End If
                If (clsCommon.myLen(obj.AddCode10) > 0) Then
                    gvadd.Rows.AddNew()
                    isCellValueChangedOpenAdd = True
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdcode).Value = obj.AddCode10
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladddesc).Value = obj.AddDesc10
                    gvadd.Rows(gvadd.Rows.Count - 1).Cells(coladdamt).Value = obj.AddAmt10
                    isCellValueChangedOpenAdd = False
                End If
                gvadd.Rows.AddNew()

                If obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                    rbtnTaxCalAutomatic.IsChecked = True
                ElseIf obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                    rbtnTaxCalManual.IsChecked = True
                End If


                lblGrossWeight.Text = clsCommon.myCstr(obj.Total_Gross_Weight)
                lblNetWeight.Text = clsCommon.myCstr(obj.Total_Net_Weight)


                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As ClsScrapSaleDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.RowCount - 1).Cells(colRowType).Value = objTr.Row_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(objTr.Item_Code, Nothing)
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        End If


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.shipped_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colprice).Value = objTr.price
                        If PickCostOFMaterialSaleFromPriceMaster = True Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colprice).ReadOnly = True
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colprice).ReadOnly = False
                        End If
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Location
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.LocationName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.ItemAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = objTr.DiscountPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmt).Value = objTr.DiscountAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colitemnetamt).Value = objTr.ItemNetAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSp).Value = objTr.Specification

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

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colnetprice).Value = objTr.NetPriceAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(coltotdisamt).Value = objTr.TotalDiscountAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = objTr.TotalTaxAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colitemnetamt).Value = objTr.ItemNetAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(coltotamt).Value = objTr.TotalAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = objTr.arrBatchItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemwiseTaxCode).Value = objTr.ItemwiseTaxCode
                        If obj.ispost = 0 Then
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
                    Next
                    If obj.ispost = ERPTransactionStatus.Pending Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                    End If

                End If
                SetitemWiseTaxOnlySetting()
                ' ''RefreshReqNo()
                ' ''RefreshGRPNo()

                UpdateAllTotals()

                Dim strinvoice As String = "select invoice_No from TSPL_SCRAPINVOICE_HEAD where shipment_No='" + strCode + "' "
                Dim invoiceNo As String = clsDBFuncationality.getSingleValue(strinvoice)
                If clsCommon.myLen(invoiceNo) <= 0 Then
                    txtscrapinvoice.Text = ""

                Else
                    txtscrapinvoice.Text = invoiceNo
                End If

                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.shipment_No)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.shipment_No, MyBase.Form_ID, gv1)
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.shipment_No)
                If clsCommon.myLen(lblInvoiceNo.Text) > 0 Then
                    btnInvoiceJE.Visible = True
                Else
                    btnInvoiceJE.Visible = False
                End If
                If MaterialSaleInvoiceEnablePrintOnPost = True Then
                    If obj.ispost = 1 Then
                        ' btnPrint.Visible = True
                    Else
                        ' btnPrint.Visible = False
                    End If
                End If
            Else
                AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub SetitemWiseTaxOnlySetting()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsscrap(txtTaxGroup.Value)
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
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXONBASEAMT" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    'Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
    '    Try
    '        If clsCommon.CompairString(gv2.CurrentCell.ColumnInfo.Name, colTTaxRate) = CompairStringResult.Equal Then
    '            Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
    '            Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FndVendorTaxRate", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='P'", "", "", True))
    '            Dim intRowNo As Integer = gv2.CurrentRow.Index
    '            If gv1.RowCount > 0 AndAlso intRowNo >= 0 Then
    '                Dim strII As String = clsCommon.myCstr(intRowNo + 1)
    '                For ii As Integer = 0 To gv1.Rows.Count - 1
    '                    gv1.Rows(ii).Cells("COLTAXRATE" + strII).Value = dblNewRate
    '                Next
    '            End If
    '            For ii As Integer = 0 To gv1.Rows.Count - 1
    '                UpdateCurrentRow(ii)
    '            Next
    '            UpdateAllTotals()
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        fundelete()
    End Sub

    Public Sub fundelete()
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
                If (ClsScrapSaleHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub fndcustNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcustNo._MYValidating
        fndcustNo.Focus()
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            Exit Sub
        End If



        Dim qry As String = "  select TSPL_customer_MASTER.cust_code as Code,TSPL_customer_MASTER.Customer_Name as Name,TSPL_customer_MASTER.Terms_Code as [Term Code] ,TSPL_TERMS_MASTER.Terms_Desc as [Term Description] ,TSPL_customer_MASTER.Tax_Group as [Tax Group],TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as [Tax Group Description] from TSPL_customer_MASTER  left outer join  TSPL_TERMS_MASTER on TSPL_customer_MASTER.Terms_Code=TSPL_TERMS_MASTER.Terms_Code  left outer join  TSPL_TAX_GROUP_MASTER on TSPL_customer_MASTER.Tax_Group=TSPL_TAX_GROUP_MASTER.Tax_Group_Code left outer join TSPL_CUSTOMER_LOCATION_MAPPING on TSPL_CUSTOMER_LOCATION_MAPPING.Customer_Code= TSPL_CUSTOMER_MASTER.Cust_Code "
        Dim WhrCls As String = "TSPL_TAX_GROUP_MASTER.Tax_Group_Type='s' and  TSPL_customer_MASTER.Status ='N'"
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
            WhrCls += " and TSPL_CUSTOMER_LOCATION_MAPPING.Location_Code in ('" + fndLocation.Value + "')"
        End If
        fndcustNo.Value = clsCommon.ShowSelectForm("CustmrMstrIFND", qry, "Code", WhrCls, fndcustNo.Value, "Code", isButtonClicked)

        qry = "  select TSPL_customer_MASTER.cust_code ,TSPL_customer_MASTER.Customer_Name ,TSPL_customer_MASTER.Terms_Code  ,TSPL_TERMS_MASTER.Terms_Desc  ,TSPL_customer_MASTER.Tax_Group ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,TSPL_customer_MASTER.Inter_Branch from TSPL_customer_MASTER  left outer join  TSPL_TERMS_MASTER on TSPL_customer_MASTER.Terms_Code=TSPL_TERMS_MASTER.Terms_Code  left outer join  TSPL_TAX_GROUP_MASTER on TSPL_customer_MASTER.Tax_Group=TSPL_TAX_GROUP_MASTER.Tax_Group_Code where TSPL_customer_MASTER.cust_code='" + fndcustNo.Value + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtcustdesc.Text = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
            txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            chkInterBranch.Checked = IIf(clsCommon.CompairString("Y", clsCommon.myCstr(dt.Rows(0)("Inter_Branch"))) = CompairStringResult.Equal, True, False)
        Else
            txtcustdesc.Text = ""
            txtTermCode.Value = ""
            lblTermName.Text = ""
            txtTaxGroup.Value = ""
            lblTaxGrpName.Text = ""
            chkInterBranch.Checked = False
        End If

        ' '========================Added by Preeti Gupta===========================
        ' qry = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State, " & _
        '"TSPL_LOCATION_MASTER.Sales_Tax_Group as LocalTaxGroup,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Local_Tax_GroupName, " & _
        '"TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as InterstateTaxGroup,TSPL_TAX_GROUP_MASTERIS.Tax_Group_Desc as Interstate_Tax_GroupName " & _
        '"FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_TAX_GROUP_MASTER as TSPL_TAX_GROUP_MASTERIS on TSPL_TAX_GROUP_MASTERIS.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTERIS.Tax_Group_Type='S' WHERE TSPL_LOCATION_MASTER.Location_Code = '" + Convert.ToString(fndLocation.Value) + "'"
        ' Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable(qry)
        ' Dim loc As String = clsCommon.myCstr(dtLocation.Rows(0)("Excisable"))
        ' Dim strLocState As String = clsCommon.myCstr(dtLocation.Rows(0)("State"))
        ' If clsCommon.CompairString(loc, "T") = CompairStringResult.Equal Then
        '     strExcise = True
        ' Else
        '     strExcise = False
        ' End If
        ' If clsCommon.myLen(fndcustNo.Value) > 0 Then
        '     qry = "select Price_Code,price_CodeNon,State,price_group_code from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndcustNo.Value + "'"
        '     dt = clsDBFuncationality.GetDataTable(qry)

        '     'If clsCommon.CompairString(loc, "T") = CompairStringResult.Equal OrElse clsCommon.CompairString(loc, "Y") = CompairStringResult.Equal Then
        '     '    txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
        '     'Else
        '     '    txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("price_CodeNon"))
        '     'End If

        '     'If clsCommon.myLen(txtPriceCode.Text) = 0 Then
        '     '    txtPriceGroupCode.Text = clsCommon.myCstr(dt.Rows(0)("price_group_code"))
        '     'End If
        '     'txtVendorNo.Enabled = False

        '     If clsCommon.CompairString(clsCommon.myCstr(dtLocation.Rows(0)("State")), clsCommon.myCstr(dt.Rows(0)("State"))) = CompairStringResult.Equal Then
        '         txtTaxGroup.Value = clsCommon.myCstr(dtLocation.Rows(0)("LocalTaxGroup"))
        '         lblTaxGrpName.Text = clsCommon.myCstr(dtLocation.Rows(0)("Local_Tax_GroupName"))
        '     Else
        '         txtTaxGroup.Value = clsCommon.myCstr(dtLocation.Rows(0)("InterstateTaxGroup"))
        '         lblTaxGrpName.Text = clsCommon.myCstr(dtLocation.Rows(0)("Interstate_Tax_GroupName"))
        '     End If

        ' End If
        '[===========================================================================

        SetTax()
        If chkTaxable.Checked = False AndAlso clsCommon.myLen(txtTaxGroup.Value) = 0 Then
            fndcustNo.Value = ""
            clsCommon.MyMessageBoxShow(Me, "Please Map exempted Tax Group on Location " & fndLocation.Value)
            Exit Sub
        End If
        'SetTaxDetails()
        SetTermDetails()


    End Sub
    Private Sub SetTax()
        If clsCommon.myLen(clsCommon.myCstr(fndLocation.Value)) > 0 Then
            GSTStatus = clsERPFuncationality.GetGSTStatus(dtpshipment.Value)
            If GSTStatus = False OrElse (chkTaxable.Checked AndAlso GSTStatus) Then
                txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(fndLocation.Value, fndcustNo.Value, "S", dtpshipment.Value)
                lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)
            Else
                If chkTaxable.Checked = False Then
                    txtTaxGroup.Value = clsLocationWiseTax.GetExempedDefaultTaxGroup(True, fndLocation.Value, fndcustNo.Value, "S", dtpshipment.Value)
                    lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)
                End If
            End If
            SetTaxDetails()
        End If
    End Sub
    Private Sub fndShipToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndShipToLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = "Location_Type='Physical'"

        fndShipToLocation.Value = clsCommon.ShowSelectForm("VendrMastrFND", qry, "Code", WhrCls, fndShipToLocation.Value, "Code", isButtonClicked)

    End Sub

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating

        Try
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                Throw New Exception("Please first select Transaction location")
            End If
            If clsCommon.myLen(fndcustNo.Value) <= 0 Then
                Throw New Exception("Please first select Vendor / Customer ")
            End If
            Dim Without_State_Condition As Boolean = False
            Dim strLocationQry As String = "select State from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"
            Dim strCustLocationQry As String = "select State from TSPL_CUSTOMER_MASTER where cust_code='" + fndcustNo.Value + "'"
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(strLocationQry)), clsCommon.myCstr(clsDBFuncationality.getSingleValue(strCustLocationQry))) = CompairStringResult.Equal Then
                Without_State_Condition = True
            End If
            Dim qry As String = "select Distinct TSPL_TAX_GROUP_MASTER.Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER 
left join TSPL_TAX_GROUP_DETAILS on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code
left join TSPL_TAX_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Code=TSPL_TAX_MASTER.Tax_Code "
            Dim WhrCls As String = " TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'"
            If chkTaxable.Checked Then
                WhrCls += " and TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted=0 "
                If Without_State_Condition Then
                    WhrCls += " and TSPL_TAX_MASTER.Type='SGST'"
                Else
                    WhrCls += " and TSPL_TAX_MASTER.Type='IGST'"

                End If

            Else
                WhrCls += " and TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted=1 "
            End If
            'If chkExcisable.Checked Then
            '    WhrCls += " and Excisable='Y'"
            'End If
            '''richa agarwal 
            txtTaxGroup.Value = clsCommon.ShowSelectForm("POTaxGroupfndd", qry, "Code", WhrCls, txtTaxGroup.Value, "Code", isButtonClicked)


            SetTaxDetails()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try


    End Sub

    Sub SetTaxDetails()
        'Dim strTaxCode, StrExcisable As String
        Dim intCount As Integer = 0
        LoadBlankGridTax()

        Dim Without_State_Condition As Boolean = False
        Dim strLocationQry As String = "select State from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"
        Dim strCustLocationQry As String = "select State from TSPL_CUSTOMER_MASTER where cust_code='" + fndcustNo.Value + "'"
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(strLocationQry)), clsCommon.myCstr(clsDBFuncationality.getSingleValue(strCustLocationQry))) = CompairStringResult.Equal Then
            Without_State_Condition = True
        End If
        'Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='s') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='s' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='s' order by Trans_Code"
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", fndcustNo.Value, fndLocation.Value, Without_State_Condition)
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If (dt.Rows.Count > 10) Then
                common.clsCommon.MyMessageBoxShow(Me, "Can't Handle More than 10 Tax Types in a Group", Me.Text)
                Return
            End If
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            For Each dr As DataRow In dt.Rows
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dr("Tax_Code"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsCommon.myCstr(dr("Tax_Code_Desc"))

                If rbtnTaxCalAutomatic.IsChecked Then

                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & fndcustNo.Value & "'")), "0") = CompairStringResult.Equal Then
                            If AmountToCheckCustomerOutstandingForTCSTax > 0 Then
                                Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsCustomerMaster.GetCustomerOutstandingForTCSTaxApplicableOnFY(fndcustNo.Value, dtpshipment.Value))
                                If dblOutstandingAmount < AmountToCheckCustomerOutstandingForTCSTax Then
                                    dblOutstandingAmount = dblOutstandingAmount + clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text))
                                    If dblOutstandingAmount > AmountToCheckCustomerOutstandingForTCSTax Then
                                        If clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text)) > 0 Then
                                            txttcstaxbaseamount.Value = clsCommon.myCdbl(dblOutstandingAmount - AmountToCheckCustomerOutstandingForTCSTax)
                                        End If
                                    End If
                                End If

                                If dblOutstandingAmount > AmountToCheckCustomerOutstandingForTCSTax Then
                                    If EnableTCSRateValidityFrom01July2021 Then
                                        Dim Is_ITR_Filled_And_TCSAmountGreater50K As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT CASE WHEN ISNULL(IsTCSGreaterthan50K,0)=1 AND ISNULL(IsITRfilledinLast2Years,0)=1 THEN 1 ELSE 0 END FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code='" & fndcustNo.Value & "'")) = 1, True, False)
                                        If Is_ITR_Filled_And_TCSAmountGreater50K = True Then
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                        Else
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                        End If
                                    Else
                                        Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(pan,'')+isnull(Additional3 ,'') as PanNoAdhar from tspl_customer_master where cust_code='" & fndcustNo.Value & "'"))
                                        If clsCommon.myLen(panno) > 0 Then
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                        Else
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                        End If
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
        'ExcisableTaxGroup()
    End Sub

    ''''''   added by priti on 16/05/12

    Private Function ExcisableTaxGroup()
        Dim strTaxCode, StrExcisable As String
        Dim intCount As Integer = 0
        For Each grow As GridViewRowInfo In gv2.Rows
            strTaxCode = grow.Cells(0).Value
            StrExcisable = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Type from TSPL_TAX_MASTER where  Tax_Code='" + strTaxCode + "'"))
            If StrExcisable = "E" Then
                intCount = intCount + 1
            End If
        Next
        If chkExcisable.Checked = True And intCount = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select exisable tax group", Me.Text)
            Return False
        ElseIf chkExcisable.Checked = False And intCount > 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select non excisable tax group", Me.Text)
            Return False
        End If

        Return True
    End Function
    ''''''   CODE ENDS HERE
    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean, Optional ByVal isSkipTCSRateonDoubleClick As Boolean = False)
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        'Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsscrap(txtTaxGroup.Value)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", fndcustNo.Value, fndLocation.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        If clsCommon.CompairString(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value, "TCS") <> CompairStringResult.Equal Then

                            If CalculateTaxRatefromItemwsieTaxOnSale = 0 Then
                                If isChangeRate Then
                                    gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                            Else
                                Dim objTM As clsItemWiseTaxAuthority
                                objTM = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(txtTaxGroup.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value), dtpshipment.Value, "S")
                                If objTM IsNot Nothing Then
                                    gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = objTM.TAX_Rate
                                    gv1.CurrentRow.Cells(colItemwiseTaxCode).Value = objTM.HCODE
                                End If
                            End If

                            gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv1.CurrentRow.Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXONBASEAMT" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                            ii = ii + 1
                        End If

                    Next
                End If
            Else
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    If isChangeRate Then
                        BlankTaxDetails(intRowNo)
                    End If
                    If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colICode).Value) > 0 Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If CalculateTaxRatefromItemwsieTaxOnSale = 0 Then
                                If isChangeRate Then
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                            Else
                                If isChangeRate Then
                                    Dim objTM As clsItemWiseTaxAuthority
                                    objTM = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.Rows(intRowNo).Cells(colICode).Value), clsCommon.myCstr(txtTaxGroup.Value), clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value), dtpshipment.Value, "S")
                                    If objTM IsNot Nothing Then
                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = objTM.TAX_Rate
                                        gv1.Rows(intRowNo).Cells(colItemwiseTaxCode).Value = objTM.HCODE
                                    End If
                                End If
                            End If
                            ''UDL/08/07/21-001040
                            If isSkipTCSRateonDoubleClick = False Then
                                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & fndcustNo.Value & "'")), "0") = CompairStringResult.Equal Then
                                        If AmountToCheckCustomerOutstandingForTCSTax > 0 Then
                                            Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsCustomerMaster.GetCustomerOutstandingForTCSTaxApplicableOnFY(fndcustNo.Value, dtpshipment.Value))
                                            If dblOutstandingAmount < AmountToCheckCustomerOutstandingForTCSTax Then
                                                dblOutstandingAmount = dblOutstandingAmount + clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text))
                                                If dblOutstandingAmount > AmountToCheckCustomerOutstandingForTCSTax Then
                                                    If clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text)) > 0 Then
                                                        txttcstaxbaseamount.Value = clsCommon.myCdbl(dblOutstandingAmount - AmountToCheckCustomerOutstandingForTCSTax)
                                                    End If
                                                End If
                                            End If

                                            If dblOutstandingAmount > AmountToCheckCustomerOutstandingForTCSTax Then

                                                If EnableTCSRateValidityFrom01July2021 Then
                                                    Dim Is_ITR_Filled_And_TCSAmountGreater50K As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT CASE WHEN ISNULL(IsTCSGreaterthan50K,0)=1 AND ISNULL(IsITRfilledinLast2Years,0)=1 THEN 1 ELSE 0 END FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code='" & fndcustNo.Value & "'")) = 1, True, False)
                                                    If Is_ITR_Filled_And_TCSAmountGreater50K = True Then
                                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                                    Else
                                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                                    End If
                                                Else
                                                    Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(pan,'')+isnull(Additional3 ,'') as PanNoAdhar from tspl_customer_master where cust_code='" & fndcustNo.Value & "'"))
                                                    If clsCommon.myLen(panno) > 0 Then
                                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                                    Else
                                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                                    End If
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
                            End If



                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXONBASEAMT" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                            ii = ii + 1
                        Next
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocation._MYValidating

        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        fndLocation.Value = clsCommon.ShowSelectForm("LocTnMstrFND", qry, "Code", WhrCls, fndLocation.Value, "Code", isButtonClicked)
        txtlocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
        ''richa agarwal 19/03/2015
        If clsCommon.CompairString(clsDBFuncationality.getSingleValue("Select Excisable from tspl_location_master where Location_Code ='" & fndLocation.Value & "'"), "T") = CompairStringResult.Equal Then
            ddlInvoiceType.SelectedValue = "E"
        End If
        Try
            SetTax()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub TxtVehicleCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtVehicleCode._MYValidating
        Dim qry As String = "Select distinct  vehicle_id as Code ,Description,Transport_id ,Vendor_Name from TSPL_VEHICLE_MASTER left outer join TSPL_VENDOR_MASTER on tspl_vehicle_master.transport_id=TSPL_VENDOR_MASTER.vendor_code "
        Dim WhrCls As String = ""
        TxtVehicleCode.Value = clsCommon.ShowSelectForm("VehicleFND", qry, "Code", WhrCls, TxtVehicleCode.Value, "Code", isButtonClicked)
        txtVehicleDesc.Text = ClsScrapSaleHead.GetVehicleDesc(TxtVehicleCode.Value, Nothing)
        'lblVehicleNo.Text = connectSql.RunScalar("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(TxtVehicleCode.Value) + "'")
        txtTransporter_desc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name as Name from TSPL_VENDOR_MASTER left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Transport_id=TSPL_VENDOR_MASTER.vendor_code where Vehicle_id ='" + TxtVehicleCode.Value + "'"))
        txtTransporter_Code.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code as Code from TSPL_VENDOR_MASTER left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Transport_id=TSPL_VENDOR_MASTER.vendor_code where Vehicle_id ='" + TxtVehicleCode.Value + "'"))
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colprice).Value)
        Dim dblAmt As Decimal = dblQty * dblRate
        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
            gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        Else
            dblAmt = clsCommon.myCDecimal(gv1.Rows(IntRowNo).Cells(colAmt).Value)
        End If

        Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPer).Value)
        Dim dblDisAmt As Double = (dblAmt * dblDisPer) / 100
        Dim dblAmtAfterDis As Double = dblAmt - dblDisAmt
        Dim dbnetpriceamt As Double = dblAmt - dblDisAmt
        Dim dbtotdisamt As Double = dblQty * dblDisAmt
        Dim dblTotalTaxAmt As Double = 0
        For ii As Integer = 1 To 10
            Dim Strii As String = clsCommon.myCstr(ii)
            If rbtnTaxCalAutomatic.IsChecked Then
                Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    If clsCommon.CompairString(strTaxCode, "TCS") <> CompairStringResult.Equal Then
                        Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                        Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                        Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                        Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                        Dim IsTaxOnBaseAmt As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXONBASEAMT" + Strii)).Value)
                        ''Dim IsExcisable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value)
                        Dim dblBaseAmt As Double = 0
                        Dim dblTaxAmt As Double = 0
                        If IsSurTax Then
                            Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                            dblBaseAmt = dblSurTaxAmt
                        Else
                            Dim dblOtherTaxAmt As Double = 0
                            If Not IsTaxOnBaseAmt Then
                                dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                            End If

                            If Not IsTaxOnBaseAmt AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then

                                Dim dblTotalBasicPrice As Double = 0
                                For n As Integer = 0 To gv1.Rows.Count - 1
                                    If clsCommon.myLen(gv1.Rows(n).Cells(colICode).Value) > 0 Then
                                        dblTotalBasicPrice = dblTotalBasicPrice + clsCommon.myCdbl(gv1.Rows(n).Cells(colAmt).Value)
                                    End If
                                Next
                                dblBaseAmt = (clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmt).Value) * clsCommon.myCdbl(txttcstaxbaseamount.Value)) / dblTotalBasicPrice
                            Else
                                dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                            End If

                            'dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)


                        End If
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                        dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, IIf(objCommonVar.IsRoundOffTaxToZeroDecimal, 0, 2))
                        If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                            arrTaxableAuth.Add(strTaxCode.ToUpper())
                        End If
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
                    Dim dblCurrRowAmt As Double = clsCommon.myCdbl(gv1.Rows(clsCommon.myCdbl(IntRowNo)).Cells(colAmt).Value)
                    Dim dblTotAmt As Double = 0
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        dblTotAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells(colAmt).Value)
                    Next
                    Dim dblCurrCalTax As Double = 0
                    If dblTotAmt <> 0 Then
                        dblCurrCalTax = Math.Round(clsCommon.myCdbl(dblTaxAmt * dblCurrRowAmt / dblTotAmt), 2, MidpointRounding.ToEven)
                    End If
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = dblCurrCalTax
                End If
            End If
            dblTotalTaxAmt += clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value)
        Next

        gv1.Rows(IntRowNo).Cells(colitemnetamt).Value = dblAmtAfterDis
        gv1.Rows(IntRowNo).Cells(colnetprice).Value = dbnetpriceamt
        gv1.Rows(IntRowNo).Cells(coltotdisamt).Value = dbtotdisamt

        'Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
        Dim dblAmtAfterTax As Double = dblAmtAfterDis + dblTotalTaxAmt
        gv1.Rows(IntRowNo).Cells(colDisAmt).Value = Math.Round(dblDisAmt, 2)
        'gv1.Rows(IntRowNo).Cells(colAmtAfterDis).Value = Math.Round(dblAmtAfterDis, 2)
        gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotalTaxAmt, 2)
        gv1.Rows(IntRowNo).Cells(coltotamt).Value = Math.Round(dblAmtAfterTax, 2)
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If CalculateTaxRatefromItemwsieTaxOnSale = 0 Then
                If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) AndAlso rbtnTaxCalAutomatic.IsChecked Then
                    Dim frm As New FrmPOItemTaxDetails()
                    frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                    frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
                    frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colitemnetamt).Value)
                    frm.strTaxGroup = clsCommon.myCstr(txtTaxGroup.Value)
                    frm.strTransLocation = clsCommon.myCstr(fndLocation.Value)
                    frm.strTaxType = "S"
                    'frm.strTaxType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Type from TSPL_TAX_group_MASTER where Tax_Group_Code='" & txtTaxGroup.Value & "'"))
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
                            Next
                            gv1.CurrentRow.Cells(colTotTaxAmt).Value = frm.dblTotTax
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateAllTotals()
                        End If
                    End If


                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub UpdateAllTotals()
        Dim dblTotAmt As Double = 0
        Dim dblTotDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0

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

        Dim dblGrossWeight As Double = 0
        Dim dblNetWeight As Double = 0

        Dim dblTaxTotAmt As Double = 0
        Dim dblTCSTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisAmt).Value)
                dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colitemnetamt).Value)
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

                dblTaxTotAmt = dblTaxTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value)
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(coltotamt).Value)
                If GrossWtfromItemMaster Then
                    Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable("select gross_weight,Net_Weight from tspl_item_uom_detail where item_code='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + "' and uom_code='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value) + "'")
                    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        If clsCommon.myCdbl(dtTemp.Rows(0)("gross_weight")) <= 0 Then
                            Throw New Exception("Please set gross weight for item:" + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + " and UOM:" + clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value))
                        End If
                        If clsCommon.myCdbl(dtTemp.Rows(0)("Net_Weight")) <= 0 Then
                            Throw New Exception("Please set net weight for item:" + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + " and UOM:" + clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value))
                        End If
                        dblGrossWeight += clsCommon.myCdbl(dtTemp.Rows(0)("gross_weight")) * clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                        dblNetWeight += clsCommon.myCdbl(dtTemp.Rows(0)("Net_Weight")) * clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                    End If

                End If
            End If
        Next


        If rbtnTaxCalAutomatic.IsChecked Then
            For ii As Integer = 1 To gv2.Rows.Count
                Select Case (ii)
                    Case 1
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) Then 'AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                            lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1)
                            'dblTaxBaseAmt1 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxBaseAmt1 = clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text)
                            dblTaxAmt1 = (dblTaxBaseAmt1 * clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)) / 100
                            dblTCSTotAmt = dblTCSTotAmt + dblTaxAmt1
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt1, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt1, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt1, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt1, 2)
                            If dblTaxBaseAmt1 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        End If


                    Case 2
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) Then ' AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                            lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1)
                            'dblTaxBaseAmt2 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxBaseAmt2 = clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text)
                            dblTaxAmt2 = (dblTaxBaseAmt2 * clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)) / 100
                            dblTCSTotAmt = dblTCSTotAmt + dblTaxAmt2
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                            If dblTaxBaseAmt2 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        End If

                    Case 3
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) Then ' AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                            lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1 + dblTaxAmt2)
                            'dblTaxBaseAmt3 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxBaseAmt3 = clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text)
                            dblTaxAmt3 = (dblTaxBaseAmt3 * clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)) / 100
                            dblTCSTotAmt = dblTCSTotAmt + dblTaxAmt3
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                            If dblTaxBaseAmt3 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        End If

                    Case 4
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) Then ' AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                            lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1 + dblTaxAmt2 + dblTaxAmt3)
                            'dblTaxBaseAmt4 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxBaseAmt4 = clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text)
                            dblTaxAmt4 = (dblTaxBaseAmt4 * clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)) / 100
                            dblTCSTotAmt = dblTCSTotAmt + dblTaxAmt4
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt4, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt4, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt4, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt4, 2)
                            If dblTaxBaseAmt4 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        End If
                    Case 5
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt5, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt5, 2)
                        If dblTaxBaseAmt5 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt5 * 100) / dblTaxBaseAmt5, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 6
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt6, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt6, 2)
                        If dblTaxBaseAmt6 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt6 * 100) / dblTaxBaseAmt6, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 7
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt7, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt7, 2)
                        If dblTaxBaseAmt7 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt7 * 100) / dblTaxBaseAmt7, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 8
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt8, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt8, 2)
                        If dblTaxBaseAmt8 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt8 * 100) / dblTaxBaseAmt8, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 9
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt9, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt9, 2)
                        If dblTaxBaseAmt9 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt9 * 100) / dblTaxBaseAmt9, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 10
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt10, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt10, 2)
                        If dblTaxBaseAmt10 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt10 * 100) / dblTaxBaseAmt10, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                End Select
            Next
        End If
        funShowAmt()
        lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt)
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblAmtAfterDis)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt + dblTCSTotAmt)
        If rbtnTaxCalAutomatic.IsChecked Then
            lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt + dblTCSTotAmt)
        ElseIf rbtnTaxCalManual.IsChecked Then
            lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        End If

        lbldocamt.Text = clsCommon.myFormat(clsCommon.myCdbl(lbladdcharges.Text) + clsCommon.myCdbl(lblTotRAmt.Text))
        '====Sanjeet(check for Roud Off Amount)====
        If AllowRoundOff_onInvoice Then
            Dim lstDecml As New List(Of Decimal)
            lstDecml = ClsScrapSaleHead.Calculate_RoundOffAmt(clsCommon.myCdbl(lblTotRAmt.Text), Nothing)
            Dim AmtAfterRoundOff As Decimal = 0
            If lstDecml IsNot Nothing AndAlso lstDecml.Count > 0 Then
                AmtAfterRoundOff = clsCommon.myCdbl(lstDecml(0))
                txtRoundOff.Text = clsCommon.myCdbl(lstDecml(1))
                lbldocamt.Text = clsCommon.myFormat(clsCommon.myCdbl(lbladdcharges.Text) + clsCommon.myCdbl(AmtAfterRoundOff))
            End If
        Else
            txtRoundOff.Text = 0
        End If

        lblGrossWeight.Text = clsCommon.myCstr(Math.Round(dblGrossWeight, 3, MidpointRounding.AwayFromZero))
        lblNetWeight.Text = clsCommon.myCstr(Math.Round(dblNetWeight, 3, MidpointRounding.AwayFromZero))

    End Sub

    Private Sub gv2_DockChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv2.DockChanged

    End Sub

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If CalculateTaxRatefromItemwsieTaxOnSale = 0 Then
                If rbtnTaxCalAutomatic.IsChecked Then
                    Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                    'Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FndVndrTxRatFND", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='S'", "", "", True))
                    'Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(IIf(clsCommon.myLen(clsCommon.myCstr(fndLocation.Value)) <= 0, fndLocation.Value, txtShipToLocation.Value), txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), fndcustNo.Value, "s")
                    Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(fndLocation.Value, txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), fndcustNo.Value, "S")
                    Dim intRowNo As Integer = gv2.CurrentRow.Index
                    If gv1.RowCount > 0 AndAlso intRowNo >= 0 Then
                        Dim strII As String = clsCommon.myCstr(intRowNo + 1)
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            gv1.Rows(ii).Cells("COLTAXRATE" + strII).Value = dblNewRate
                        Next
                    End If
                    txttcstaxbaseamount.Value = 1
                    txttcstaxbaseamount.Value = 0
                    ''UDL/08/07/21-001040
                    SetitemWiseTaxSetting(False, False, True)
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        UpdateCurrentRow(ii)
                    Next
                    If clsCommon.myCdbl(txttcstaxbaseamount.Value) <= 0 Then
                        txttcstaxbaseamount.Value = 1
                        txttcstaxbaseamount.Value = 0
                    End If
                    UpdateAllTotals()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gv1.Columns(colICode) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colICode).ReadOnly = (clsCommon.myLen(txtGWeighmentNo.Value) > 0)
                    Else
                        gv1.CurrentRow.Cells(colICode).ReadOnly = False
                    End If
                ElseIf e.Column Is gv1.Columns(colUnit) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colUnit).ReadOnly = (clsCommon.myLen(txtGWeighmentNo.Value) > 0)
                    Else
                        gv1.CurrentRow.Cells(colUnit).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colprice) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colprice).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colprice).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colQty) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colQty).ReadOnly = (clsCommon.myLen(txtGWeighmentNo.Value) > 0)
                    Else
                        gv1.CurrentRow.Cells(colQty).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colAmt) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = True
                    End If
                End If
            End If

        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gvadd_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvadd.CellValueChanged
        Try
            If Not isCellValueChangedOpenAdd Then
                isCellValueChangedOpenAdd = True
                If e.Column Is gvadd.Columns(coladdcode) Then
                    OpenAddCodeList(False)
                    'ElseIf e.Column Is gvadd.Columns(coladdamt) Then
                    '         funShowAmt()

                End If
                UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                UpdateAllTotals()
                funShowAmt()
                setGridFocusAdd()
                isCellValueChangedOpenAdd = False
            End If
            '
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub setGridFocusAdd()
        Dim intCurrRow As Integer = gvadd.CurrentRow.Index
        'gvadd.CurrentRow.Cells(0).Value = clsCommon.myCdbl(intCurrRow + 1)
        If intCurrRow = gvadd.Rows.Count - 1 Then
            gvadd.Rows.AddNew()
            gvadd.CurrentRow = gvadd.Rows(intCurrRow)
        End If
        If clsCommon.myLen(gvadd.CurrentRow.Cells(0).Value) > 0 Then
            If gvadd.CurrentColumn Is gvadd.Columns(0) Then
                gvadd.CurrentRow = gvadd.Rows(intCurrRow)
                gvadd.CurrentColumn = gvadd.Columns(1)
            ElseIf gvadd.CurrentColumn Is gvadd.Columns(1) Then
                gvadd.CurrentRow = gvadd.Rows(intCurrRow)
                gvadd.CurrentColumn = gvadd.Columns(2)

            ElseIf gvadd.CurrentColumn Is gvadd.Columns(2) Then
                gvadd.CurrentRow = gvadd.Rows(intCurrRow + 1)
                gvadd.CurrentColumn = gvadd.Columns(1)
            End If
        End If
    End Sub

    Sub OpenAddCodeList(ByVal isButtonClick As Boolean)

        Dim obj As ClsScrapSaleDetail = ClsScrapSaleDetail.FinderAdditional(clsCommon.myCstr(gvadd.CurrentRow.Cells(coladdcode).Value), isButtonClick)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
            gvadd.CurrentRow.Cells(coladdcode).Value = obj.Code
            gvadd.CurrentRow.Cells(coladddesc).Value = obj.Description

        Else
            gvadd.CurrentRow.Cells(coladdcode).Value = ""
            gvadd.CurrentRow.Cells(coladdcode).Value = ""

        End If
        SetitemWiseTaxSetting(True, True)

    End Sub

    Public Sub funShowAmt()

        Dim addtotalamt As Decimal = 0

        For i As Integer = 0 To gvadd.Rows.Count - 1
            Dim addamt As Decimal = clsCommon.myCdbl(gvadd.Rows(i).Cells(2).Value)

            addtotalamt = addamt + addtotalamt

        Next
        txtaddamt.Text = addtotalamt
        lbladdcharges.Text = addtotalamt

    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from tspl_scrapsale_head where shipment_No='" + txtDocNo.Value + "' and Doc_Type='S' "
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
        Dim qry As String = "select shipment_No as Code,CONVERT(varchar(10), shipment_Date,103)+' '+ CONVERT(varchar(5), shipment_Date,114) as Date,cust_Code as [Customer Code], cust_Name as Customer,ship_Total_Amt as Amount,case when ispost='0' then 'Pending' else 'Approved' end as [Status],(select top 1 invoice_No from TSPL_SCRAPINVOICE_HEAD where TSPL_SCRAPINVOICE_HEAD.shipment_No=tspl_scrapsale_head.shipment_No) as [Invoice No] from tspl_scrapsale_head"

        Dim whrClas As String = " Doc_Type ='S' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " and loc_code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        LoadData(clsCommon.ShowSelectForm("ScrpCodFiltrFND", qry, "Code", whrClas, txtDocNo.Value, "shipment_Date desc", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER"
        txtTermCode.Value = clsCommon.ShowSelectForm("TermCodIDS", qry, "Code", "", txtTermCode.Value, "Code", isButtonClicked)
        SetTermDetails()

    End Sub

    Sub SetTermDetails()
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER where Terms_Code='" + txtTermCode.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
            txtDueDate.Value = dtpshipment.Value.AddDays(clsCommon.myCdbl(dt.Rows(0)("No Of Days")))
        Else
            lblTermName.Text = ""
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                '' Anubhooti 12-Sep-2014 BM00000003735
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Material Sales", dtpshipment.Value) = False Then
                    Exit Sub
                End If
                ''
                If (ClsScrapSaleHead.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                    Dim lstUsers As New List(Of String)
                    lstUsers.Add(fndcustNo.Value)
                    SendSMSandEmail(lstUsers, False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
        Try
            Dim strContactperson As String = ""
            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.ScrapSale + "'", Nothing)
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                Dim objEmailH As New clsEMailHead()
                objEmailH.arrEMail = New List(Of String)()
                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(dtpshipment.Text, "dd/MMM/yyyy"))

                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(dtpshipment.Text, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.CustomerNo, fndcustNo.Value)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.CustomerName, txtcustdesc.Text)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.TotalAmount, lblDocAmount.Text)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Form_Code, clsUserMgtCode.ScrapSale)

                '------------------------code for attchament-------------------------------------
                strPdfAttachmentPath = ""
                strPdfAttachmentPath = Print(True, False, True)
                objEmailH.Attachment_1_Path = strPdfAttachmentPath
                '---------------------------------------------------------------------------

                For Each strUser As String In lstUsers
                    Dim lstReceiptents As New List(Of String)
                    Dim qry As String = ""
                    Dim emailId As String = ""
                    If isSendForApproval Then
                        strContactperson = strUser
                        qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
                        emailId = clsDBFuncationality.getSingleValue(qry)
                    Else
                        strContactperson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
                        emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
                    End If

                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ContactPerson, strContactperson)
                    If clsCommon.myLen(emailId) > 0 Then
                        objEmailH.arrEMail.Add(emailId)
                    End If
                Next

                If objEmailH.SaveData(clsUserMgtCode.ScrapSale, objEmailH, Nothing) Then
                    clsCommon.MyMessageBoxShow(Me, "E-Mail Send Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "E-Mail Send Failed !", Me.Text)
                End If
                objEmailH = Nothing
                'SMSSENDONLY()


            Else
                clsCommon.MyMessageBoxShow(Me, "First do email and sms setting", Me.Text)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub




    Private Sub layout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles layoutrbtn.Click
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
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
            common.clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmScrapSale_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
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
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            fundelete()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                'Add Tool tip Task No- TEC/22/05/18-000245
                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                       "TSPL_SCRAPSALE_HEAD " + Environment.NewLine +
                                       "TSPL_SCRAPSALE_DETAIL " + Environment.NewLine +
                                       "TSPL_SCRAPINVOICE_HEAD " + Environment.NewLine +
                                       "TSPL_SCRAPINVOICE_DETAIL " + Environment.NewLine +
                                       "Press Alt+P for Post Trasnaction" + Environment.NewLine +
                                       "TSPL_INVENTORY_MOVEMENT " + Environment.NewLine +
                                       "TSPL_JOURNAL_MASTER " + Environment.NewLine +
                                       "TSPL_JOURNAL_DETAILS " + Environment.NewLine +
                                       "TSPL_Customer_Invoice_Head " + Environment.NewLine +
                                       "TSPL_Customer_Invoice_Detail ")
                'Add Tool tip Task No- TEC/22/05/18-000245
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Public Function Print(ByVal isPrint As Boolean, ByVal ischallan As Boolean, ByVal isPDFPath As Boolean) As String
        Dim filePath As String = ""
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            Dim IsMandiTax As Double = 0
            IsMandiTax = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" & txtTaxGroup.Value & "' and Tax_Code in(select Tax_Code from TSPL_TAX_MASTER where Is_Mandi_Tax='Y')"))
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                Dim Query As String = PrintMaterialSale()
                Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(Query)
                If dt3.Rows.Count > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Is_CashSale")), "Y") = CompairStringResult.Equal Then
                        If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dt3.Rows(0)("Invoice_Date"))) Then
                            If clsCommon.myCdbl(dt3.Rows(0)("Is_Taxable")) = 1 Then
                                If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("frm_State_name")), clsCommon.myCstr(dt3.Rows(0)("Cust_StateName"))) = CompairStringResult.Equal Then
                                    filePath = frmCRV.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt3, "rptMaterialSaleCashMemo_Intrastate", "Material sale cash Memo", clsCommon.myCDate(dt3.Rows(0)("Invoice_Date")))
                                Else
                                    filePath = frmCRV.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt3, "rptMaterialSaleCashMemo_Interstate", "Material sale cash Memo", clsCommon.myCDate(dt3.Rows(0)("Invoice_Date")))
                                End If
                            Else
                                filePath = frmCRV.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt3, "rptMaterialSaleCashMemo_NonTaxable", "Material sale cash Memo", clsCommon.myCDate(dt3.Rows(0)("Invoice_Date")))
                            End If
                        Else
                            filePath = frmCRV.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt3, "rptMaterialSaleCashMemo", "Material sale cash Memo")
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Is_Scrap")), "Y") = CompairStringResult.Equal Then
                        If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dt3.Rows(0)("Invoice_Date"))) Then
                            If clsCommon.myCdbl(dt3.Rows(0)("Is_Taxable")) = 1 Then
                                If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("frm_State_name")), clsCommon.myCstr(dt3.Rows(0)("Cust_StateName"))) = CompairStringResult.Equal Then
                                    filePath = frmCRV.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt3, "RptScrapInvoiceIfY_Intrastate", "Material sale Scrap Invoice", clsCommon.myCDate(dt3.Rows(0)("Invoice_Date")))
                                Else
                                    filePath = frmCRV.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt3, "RptScrapInvoiceIfY_Interstate", "Material sale Scrap Invoice", clsCommon.myCDate(dt3.Rows(0)("Invoice_Date")))
                                End If
                            Else
                                filePath = frmCRV.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt3, "RptScrapInvoiceIfY_NonTaxable", "Material sale Scrap Invoice", clsCommon.myCDate(dt3.Rows(0)("Invoice_Date")))
                            End If
                        Else
                            filePath = frmCRV.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt3, "RptScrapInvoiceIfY", "Material sale Scrap Invoice")
                        End If

                        'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                    ElseIf clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dt3.Rows(0)("Invoice_Date"))) Then
                        If clsCommon.myCdbl(dt3.Rows(0)("Is_Taxable")) = 1 Then
                            If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("frm_State_name")), clsCommon.myCstr(dt3.Rows(0)("Cust_StateName"))) = CompairStringResult.Equal Then
                                filePath = frmCRV.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt3, clsERPFuncationality.CompanyAddresShowinFooter(), "RptMaterialSale_Intrastate", "ScrapnSale Invoice Local", clsCommon.myCDate(dt3.Rows(0)("Invoice_Date")), "rptCompanyAddress.rpt")
                            Else
                                filePath = frmCRV.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt3, clsERPFuncationality.CompanyAddresShowinFooter(), "RptMaterialSale_Interstate", "ScrapnSale Invoice InterState", clsCommon.myCDate(dt3.Rows(0)("Invoice_Date")), "rptCompanyAddress.rpt")
                            End If
                        Else
                            filePath = frmCRV.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt3, clsERPFuncationality.CompanyAddresShowinFooter(), "RptMaterialSale_NonTaxable", "ScrapnSale Invoice Non Taxable", clsCommon.myCDate(dt3.Rows(0)("Invoice_Date")), "rptCompanyAddress.rpt")
                        End If
                        ' Else
                        'frmCrystalReportViewer.funsubreportWithdt(isPDFPath,CrystalReportFolder.PurchaseOrder, dt1, clsERPFuncationality.CompanyAddresShowinFooter(), "ScrapSaleInvoice", "ScrapnSale Invoice", "rptCompanyAddress.rpt")
                        ''PurchaseOrderViewer.funreport(isPDFPath,dt1, "ScrapSaleInvoice", "ScrapSaleInvoiceRpt")
                        'End If
                        'End If
                        'End If

                    Else
                        If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "E") = CompairStringResult.Equal Then
                            filePath = frmCRV.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt3, "RptMaterialSaleExcisable", "Tax Invoice")
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "E") <> CompairStringResult.Equal Then
                            filePath = frmCRV.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt3, "rptScrapSalesInvoiceWithoutTinNo", "Tax Invoice")
                        End If
                    End If
                End If
                'Dim query As String = "SELECT CM.Comp_Name,L.Loc_Short_Name,COMP_ADDRESS=(CM.Add1+' '+CM.Add2+' '+CM.Add3+' '+CM.State),Loc_Address=(L.Add1+' '+L.Add2+' '+L.Add3+' '+L.Add4+' '+L.State), L.City_Code,L.State,L.Pin_Code AS Location_Pincode,COMP_PIN=('PIN '+CM.Pincode+' CORP ID NO :-'+CM.CINNo),(case when len(CM.TinNo_Issue_Date)>0 then CM.Tin_No + ' DT ' +CONVERT(VARCHAR(15),CM.TinNo_Issue_Date,103) ELSE CM.Tin_No END )AS Tin_No ,CONVERT(VARCHAR(15),CM.TinNo_Issue_Date,103)AS TinNo_Issue_Date ,CM.CST_LST,CM.CINNo, (case when len(CM.PanNo_Issue_Date)>0 then CM.Pan_No + ' DT ' +CONVERT(VARCHAR(15),CM.PanNo_Issue_Date,103) ELSE CM.Pan_No END )AS Pan_No,CONVERT(VARCHAR(15),CM.PanNo_Issue_Date,103) AS PanNo_Issue_Date ,CM.Phone1,CM.Pincode,CM.Email,CM.Tcan_No AS WebSite, " & _
                '                    " TR`AN_SACTION='', " & _
                '                    "  CM.CE_Division,CM.CE_Commissionerate,'' as Tariff,CM.Access_Officer as FSSAI,Description=i.Description,Remarks='', case when coalesce(C.parent_customer_no,'')<>'' then C2.Customer_Name else C.Customer_Name end as PCust_Name ,case when coalesce(C.parent_customer_no,'')<>'' then C.Customer_Name else 'Same As Buyer' end as C_Cust_Name, C.Customer_Name,CUST_ADD=(C.Add1+' '+C.Add2+' '+C.Add3), case when coalesce(C.parent_customer_no,'')<>'' then (C2.Add1+' '+C2.Add2+' '+C2.Add3+' '+C2.STATE) else (C.Add1+' '+C.Add2+' '+C.Add3+' '+C.STATE) end as Pcust_Address ,CT.City_Name,C.CST,C.Cust_Code,Cust_Tin=(C.Tin_No),Cust_cst=c.CST,Document_Type='',I.shipment_No as Document_Code,I.VAT_InvoiceNo,I.Excisable,I.Invoice_Type,I.VatInvoice_Type, (CASE WHEN I.Invoice_Type='E' AND I.VatInvoice_Type='T' THEN (case when I.Is_Scrap='Y' then 'SCRAP ' else '' end)+'TAX INVOICE' WHEN I.Invoice_Type='E' AND I.VatInvoice_Type='R' THEN (case when I.Is_Scrap='Y' then 'SCRAP ' else '' end)+'SALE INVOICE-WITHIN STATE' WHEN I.Invoice_Type='E' AND I.VatInvoice_Type='I' THEN (case when I.Is_Scrap='Y' then 'SCRAP ' else '' end)+'SALE INVOICE-INTERSTATE' END) AS VAT_HEADER, (CASE WHEN I.Invoice_Type='T'  THEN (case when I.Is_Scrap='Y' then 'SCRAP ' else '' end)+'TAX INVOICE' WHEN I.Invoice_Type='R' THEN (case when I.Is_Scrap='Y' then 'SCRAP ' else '' end)+'SALE INVOICE-WITHIN STATE' WHEN I.Invoice_Type='I' THEN (case when I.Is_Scrap='Y' then 'SCRAP ' else '' end)+'SALE INVOICE-INTERSTATE' END) AS SALE_HEADER, Document_Date=CONVERT(VARCHAR(100),I.shipment_Date,103),CONVERT(VARCHAR(5),I.Modify_Date,108) AS DOCUMENT_TIME, i.PO_No  AS Cust_PO_No,CONVERT(VARCHAR(100),'',103) AS Cust_PO_Date, GE_DATE=CONVERT(VARCHAR(100),'',103),GENo='',TP.Transporter_Name AS Transporter_Name ,Freight_Amount='',Vehicle_Code=VM.NUMBER ,VM.Vehicle_Name,'' AS Final_Destination,'' AS Road_Permit_no,I.Modify_Date AS Time_of_Prepration,'' AS Time_of_Removal, C_FORM=(CASE WHEN I.TAX1!='' and T1.Type='C' THEN 'FORM '+T1.Type + ' DUE' WHEN I.TAX2!='' and T2.Type='C' THEN 'FORM '+T2.Type + ' DUE' WHEN I.TAX3!='' and T3.Type='C' THEN 'FORM '+T3.Type + ' DUE' WHEN I.TAX4!='' and T4.Type='C' THEN 'FORM '+T4.Type + ' DUE' WHEN I.TAX5!='' and T5.Type='C' THEN 'FORM '+T5.Type + ' DUE' WHEN I.TAX6!='' and T6.Type='C' THEN 'FORM '+T6.Type + ' DUE' WHEN I.TAX7!='' and T7.Type='C' THEN 'FORM '+T7.Type + ' DUE' WHEN I.TAX8!='' and T8.Type='C' THEN 'FORM '+T8.Type + ' DUE' WHEN I.TAX9!='' and T9.Type='C' THEN 'FORM '+T9.Type + ' DUE' WHEN I.TAX10!='' and T10.Type='C' THEN 'FORM '+T10.Type + ' DUE' END ), TAX1=REPLACE((CASE WHEN I.TAX1!='' AND T1.Type='E' THEN(T1.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX1_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX1_Amt)) ELSE '' END)+CHAR(13)+ (CASE WHEN I.TAX2!='' AND T2.Type='E' THEN(T2.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX2_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX2_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX3!='' AND T3.Type='E' THEN(T3.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX3_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX3_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX4!='' AND T4.Type='E' THEN(T4.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX4_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX4_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX5!='' AND T5.Type='E' THEN(T5.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX5_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX5_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX6!='' AND T6.Type='E' THEN(T6.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX6_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX6_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX7!='' AND T7.Type='E' THEN(T7.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX7_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX7_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX8!='' AND T8.Type='E' THEN(T8.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX8_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX8_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX9!='' AND T9.Type='E' THEN(T9.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX9_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX9_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX10!='' AND T10.Type='E' THEN(T10.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX10_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX10_Amt))ELSE '' END),CHAR(13),''), TAX2= REPLACE((CASE WHEN I.TAX1!='' AND T1.Type!='E' THEN(T1.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX1_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX1_Amt)) ELSE '' END)+CHAR(13)+ (CASE WHEN I.TAX2!='' AND T2.Type!='E' THEN(T2.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX2_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX2_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX3!='' AND T3.Type!='E' THEN(T3.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX3_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX3_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX4!='' AND T4.Type!='E' THEN(T4.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX4_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX4_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX5!='' AND T5.Type!='E' THEN(T5.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX5_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX5_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX6!='' AND T6.Type!='E' THEN(T6.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX6_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX6_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX7!='' AND T7.Type!='E' THEN(T7.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX7_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX7_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX8!='' AND T8.Type!='E' THEN(T8.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX8_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX8_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX9!='' AND T9.Type!='E' THEN(T9.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX9_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX9_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN I.TAX10!='' AND T10.Type!='E' THEN(T10.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX10_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX10_Amt))ELSE '' END),CHAR(13),''),  " & _
                '                    " Add_Charge_Name1=(CASE WHEN I.AddDesc1!='' THEN (I.AddDesc1+' : '+CONVERT(VARCHAR(10),I.AddDesc1)) ELSE '' END)+CHAR(13)+ (CASE WHEN I.AddDesc2!='' THEN (I.AddDesc2+' : '+CONVERT(VARCHAR(10),I.AddAmt2)) ELSE '' END) +CHAR(13)+ (CASE WHEN I.AddDesc3!='' THEN (I.AddDesc3+' : '+CONVERT(VARCHAR(10),I.AddAmt3)) ELSE '' END) +CHAR(13)+ (CASE WHEN I.AddDesc4!='' THEN (I.AddDesc4+' : '+CONVERT(VARCHAR(10),I.AddAmt4)) ELSE '' END) +CHAR(13)+ (CASE WHEN I.AddDesc5!='' THEN (I.AddDesc5+' : '+CONVERT(VARCHAR(10),I.AddAmt5)) ELSE '' END) +CHAR(13)+ (CASE WHEN I.AddDesc6!='' THEN (I.AddDesc6+' : '+CONVERT(VARCHAR(10),I.AddAmt6)) ELSE '' END) +CHAR(13)+ (CASE WHEN I.AddDesc7!='' THEN (I.AddDesc7+' : '+CONVERT(VARCHAR(10),I.AddAmt7)) ELSE '' END) +CHAR(13)+ (CASE WHEN I.AddDesc8!='' THEN (I.AddDesc8+' : '+CONVERT(VARCHAR(10),I.AddAmt8)) ELSE '' END) +CHAR(13)+ (CASE WHEN I.AddDesc9!='' THEN (I.AddDesc9+' : '+CONVERT(VARCHAR(10),I.AddAmt9)) ELSE '' END) +CHAR(13)+ (CASE WHEN I.AddDesc10!='' THEN (I.AddDesc10+' : '+CONVERT(VARCHAR(10),I.AddAmt10)) ELSE '' END), C.State as CustomerState,L.State as LocationState, 0 AS Total_Add_Charge,0 AS Gross_Wt,0 AS Final_Gross_Wt,I.Discount_Amt,I.Discount_Amt AS Head_DiscAmt,I.Discount_Amt as CasH_DiscountAmt,C.ECC,C.Form_Type,CM.Ecc_No,L.Location_Desc,RoundOffAmount=(ship_Total_Amt-ROUND(ship_Total_Amt,0))  ,I.Total_Tax_Amt,0 AS Abatement_Amt, AM.Abatement_Percent, IT.Item_Desc, D.shipped_Qty Qty,bi.Batch_No AS Batch_No,D.Unit_Code, (CASE WHEN IT.Is_Tax_Exempted=2 THEN D.ItemAmt ELSE 0.00 END)AS MRP,I.Tax_Group,D.price AS Item_Cost ,D.shipped_Qty*D.price as Amount,I.ship_Total_Amt AS Total_Amt,'' AS Packaging_Details,'' as Book_No,'' AS GRNo,'' AS GR_Date,'' AS Mandi_Receipt_No ,'' AS Mandi_Receipt_Date,'' AS EXC_PLA,'' AS MODVAT FROM TSPL_SCRAPSALE_HEAD I  JOIN TSPL_SCRAPSALE_DETAIL D ON D.shipment_No=I.shipment_No  " & _
                '                    " JOIN TSPL_ITEM_MASTER IT ON D.Item_Code=IT.Item_Code JOIN TSPL_CUSTOMER_MASTER C ON C.Cust_Code=I.cust_Code LEFT outer JOIN TSPL_CUSTOMER_MASTER C2 ON C2.Cust_Code=C.Parent_Customer_No LEFT JOIN TSPL_CITY_MASTER CT ON CT.City_Code=C.City_Code JOIN TSPL_COMPANY_MASTER CM ON CM.Comp_Code=I.Comp_Code  JOIN TSPL_LOCATION_MASTER L ON  L.Location_Code=I.ToLoc_Code LEFT JOIN TSPL_ABATEMENT_MASTER AM ON AM.Comp_Code=CM.Comp_Code LEFT JOIN TSPL_TAX_MASTER T1 ON T1.Tax_Code=I.TAX1 LEFT JOIN TSPL_TAX_MASTER T2 ON T2.Tax_Code=I.TAX2 LEFT JOIN TSPL_TAX_MASTER T3 ON T3.Tax_Code=I.TAX4 LEFT JOIN TSPL_TAX_MASTER T4 ON T4.Tax_Code=I.TAX5 LEFT JOIN TSPL_TAX_MASTER T5 ON T5.Tax_Code=I.TAX6 LEFT JOIN TSPL_TAX_MASTER T6 ON T6.Tax_Code=I.TAX7 LEFT JOIN TSPL_TAX_MASTER T7 ON T7.Tax_Code=I.TAX8 LEFT JOIN TSPL_TAX_MASTER T8 ON T8.Tax_Code=I.TAX9 LEFT JOIN TSPL_TAX_MASTER T9 ON T9.Tax_Code=I.TAX10 LEFT JOIN TSPL_TAX_MASTER T10 ON T10.Tax_Code=I.TAX1 LEFT JOIN TSPL_VEHICLE_MASTER AS VM ON VM.Vehicle_Id=i.Vehicle_Code  " & _
                '                    " LEFT JOIN TSPL_TRANSPORT_MASTER TP ON TP.Transport_Id=I.Transport_code  " & _
                '                    " LEFT JOIN TSPL_BATCH_ITEM as BI ON BI.Document_Code=d.shipment_No AND BI.Parent_Line_No=d.Line_No AND BI.Item_Code=d.Item_Code AND BI.Document_Type='PS-SH' and bi .In_Out_Type ='O' " & _
                '                    " WHERE I.shipment_No='" & txtDocNo.Value & "'"
                'Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(query)
                'If dt3.Rows.Count > 0 Then

                '    If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "E") = CompairStringResult.Equal Then
                '        If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("VatInvoice_Type")), "T") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("VatInvoice_Type")), "R") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("VatInvoice_Type")), "I") = CompairStringResult.Equal Then
                '            If CreatVatSeriesOnExciseInvoice = 1 Then
                '                If (btnPrint.Text = "Excise") Then
                '                    frmCrystalReportViewer.funreport(isPDFPath,CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleExciseInvoice", "Excise Invoice")
                '                Else
                '                    frmCrystalReportViewer.funreport(isPDFPath,CrystalReportFolder.NewSalesReports, dt3, "rptProductSalesTaxInvoice", "Vat Invoice")
                '                End If
                '            Else
                '                frmCrystalReportViewer.funreport(isPDFPath,CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleExciseInvoice", "Excise Invoice")
                '                frmCrystalReportViewer.funreport(isPDFPath,CrystalReportFolder.NewSalesReports, dt3, "rptProductSalesTaxInvoice", "Vat Invoice")
                '            End If
                '        Else

                '            frmCrystalReportViewer.funreport(isPDFPath,CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleExciseInvoice", "Excise Invoice")

                '        End If

                '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "T") = CompairStringResult.Equal Then
                '           c

                '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "R") = CompairStringResult.Equal Then
                '            frmCrystalReportViewer.funreport(isPDFPath,CrystalReportFolder.NewSalesReports, dt3, "rptCentralTaxInvoice", "Retail Invoice")
                '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "I") = CompairStringResult.Equal Then
                '            frmCrystalReportViewer.funreport(isPDFPath,CrystalReportFolder.NewSalesReports, dt3, "rptCentralTaxInvoice", "Interstate Invoice")
                '        End If

            Else

                Dim Address As String
                If clsCommon.myLen(fndLocation.Value) > 0 Then
                    Address = "(Select  MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end  + Case When TSPL_LOCATION_MASTER.Add4 ='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add4 ,103) end) from TSPL_LOCATION_MASTER   where Location_Code = ('" + fndLocation.Value + "'))  "
                Else
                    Address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "

                End If

                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    Dim InvoiceNo As String = clsDBFuncationality.getSingleValue("select invoice_No from TSPL_SCRAPINVOICE_HEAD where shipment_No='" + clsCommon.myCstr(txtDocNo.Value) + "' ")
                    If clsCommon.myLen(InvoiceNo) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Invoice No does't exist for this loadout", Me.Text)
                    Else
                        Dim qry As String
                        Dim dt As DataTable
                        qry = "select h.Description,h.Reff,TSPL_SCRAPSALE_HEAD.Vehicle_Id,TSPL_GL_SEGMENT_CODE.Description as Vdescription, h.Created_By ,h.Modify_By , cast(tspl_company_master.logo_img as image) as logo_img ,cast(tspl_company_master.logo_img2 as image) as logo_img2,tspl_company_master.Comp_Name  as CompanyName,tspl_company_master.Tin_No as CompanyTin," + Address + " as  CompanyAddress,h.invoice_No as ScrapInvoice,h.shipment_Date  as ScrapInvoiceDate," &
                                   "h.cust_Name as CustomerName,cm.PAN as Cust_PAN,h.cust_Code as CustomerCode,case when len(cm.Add1)>0 then cm.Add1 else '' end +" &
                                   "case when len(cm.Add2)>0 then ',' else '' end + " &
                                   "case when len(cm.Add2)>0 then cm.Add2 else ''end +" &
                                   "case when len(cm.Add3)>0 then ',' else '' end +" &
                                   "case when len(cm.Add3)>0 then cm.Add3 else '' end   as CustomerAddress," &
                                   "TSPL_SCRAPSALE_HEAD.NRG_No  as NrgNo,cm.CST as CstNo,TSPL_LOCATION_MASTER.Tin_No as TinNo,d.Item_Code as ItemCode," &
                                   " (Select ISNULL(Cheapter_Heads,'') from TSPL_ITEM_MASTER WHere TSPL_ITEM_MASTER.Item_Code=d.Item_Code)as [ChapterHead], " &
                                   "d.Item_Desc as Desciption,d.invoice_Qty as Quantity ,d.unit_code as Uom,d.price as Rate," &
                                   "d.ItemAmt as Amount, d.TAX1_Amt as Dtax1_Amt, d.DiscountAmt ,h.TAX1 as TaxRateDesc1, ROUND(h.TAX1_Amt, 0) as TaxRate1,h.TAX2 as TaxRateDesc2,ROUND(h.TAX2_Amt,0) as TaxRate2,h.TAX3 as TaxRateDesc3 ,ROUND(h.TAX3_Amt,0) as TaxRate3,h.TAX4 as TaxRateDesc4,ROUND(h.TAX4_Amt,0) as TaxRate4,h.TAX5 as TaxRateDesc5,ROUND(h.TAX5_Amt,0) as TaxRate5,h.TAX6 as TaxRateDesc6,ROUND(h.TAX6_Amt,0) as TaxRate6,h.TAX7 as TaxRateDesc7,ROUND(h.TAX7_Amt,0) as TaxRate7,h.TAX8 as  TaxRateDesc8,ROUND(h.TAX8_Amt,0) as TaxRate8,h.TAX9 as TaxRateDesc9,ROUND(h.TAX9_Amt,0) as TaxRate9,h.TAX10 as TaxRateDesc10,ROUND(h.TAX10_Amt,0) as  TaxRate10, " &
                                   " h.TAX1_Rate , h.TAX2_Rate, h.TAX3_Rate, h.TAX4_Rate, h.TAX5_Rate, h.TAX6_Rate, h.TAX7_Rate, h.TAX8_Rate, h.TAX9_Rate, h.TAX10_Rate, " &
                                   " TSPL_SCRAPSALE_HEAD.Excisable " &
                                   "from TSPL_SCRAPINVOICE_HEAD h left outer join TSPL_SCRAPINVOICE_DETAIL d on h.invoice_No =d.invoice_No " &
                                   "left outer join TSPL_CUSTOMER_MASTER   cm on h.cust_Code =cm.Cust_Code  left outer join tspl_company_master   on tspl_company_master.Comp_Code ='" + objCommonVar.CurrentCompanyCode + "'left outer join TSPL_SCRAPSALE_HEAD  on h.shipment_No =TSPL_SCRAPSALE_HEAD.shipment_No left outer join TSPL_LOCATION_MASTER on  h.Loc_Code=TSPL_LOCATION_MASTER.Location_Code left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code  =TSPL_SCRAPSALE_HEAD.Vehicle_Id where h.invoice_No='" + clsCommon.myCstr(InvoiceNo) + "'"
                        dt = clsDBFuncationality.GetDataTable(qry)

                        Dim DateOfEInvoiceImplementation As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DateOfEInvoiceImplementation, clsFixedParameterCode.DateOfEInvoiceImplementation, Nothing))
                        '====for KDIL(by shivani)==============='KDI/18/09/18-000431 richa 
                        Dim Qry1 As String = "select RIGHT(TSPL_SCRAPINVOICE_HEAD.shipment_No,4) as GatePass,TSPL_SCRAPINVOICE_HEAD.reff as Remark, TSPL_SCRAPINVOICE_HEAD.Vehicle_Id as VehicleNo,'1' as  CopyType" &
                                            " ,cast(TSPL_SCRAPINVOICE_HEAD.BarCode_Img as image) As BarCode_Img,isnull (TSPL_SCRAPINVOICE_HEAD.IRN_No,'') as IRN_No,isnull (TSPL_SCRAPINVOICE_HEAD.Ack_No,'') as Ack_No,case when len(isnull (TSPL_SCRAPINVOICE_HEAD.Ack_No,'')) > 0 then convert (varchar, TSPL_SCRAPINVOICE_HEAD.Ack_Date,103) else ''  end as Ack_Date, case when TSPL_SCRAPINVOICE_HEAD.Is_Taxable=1 and isnull(TSPL_SCRAPINVOICE_HEAD.EInvoice_Type,'')='BB' AND convert(date ,TSPL_SCRAPINVOICE_HEAD.SHIPMENT_DATE,103)>=convert(date ,'" + clsCommon.myCstr(DateOfEInvoiceImplementation) + "',103) then 1 else 0 end as  IsEInvoiceApply," &
                                            " Case when tax1.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX1_Rate when  tax2.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX2_Rate when tax3.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX3_Rate when tax4.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX4_Rate  when tax5.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX5_Rate when tax6.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX6_Rate  when tax7.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX7_Rate when tax8.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX8_Rate when tax9.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX9_Rate when tax10.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX10_Rate end as TCS_Rate,Case when tax1.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX1_Amt when  tax2.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX2_Amt when tax3.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX3_Amt when tax4.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX4_Amt  when tax5.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX5_Amt when tax6.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX6_Amt  when tax7.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX7_Amt when tax8.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX8_Amt when tax9.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX9_Amt when tax10.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX10_Amt end  as TCS_Amount,TSPL_SCRAPSALE_DETAIL.Line_No, tspl_company_master.comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1, TSPL_COMPANY_MASTER.Add2 as Comp_Add2, TSPL_COMPANY_MASTER.Add3 as Comp_Add3, TSPL_COMPANY_MASTER.Email as Comp_Email, TSPL_COMPANY_MASTER.Phone1 as Comp_Phone1, TSPL_COMPANY_MASTER.Phone2 as Comp_Phone2, TSPL_COMPANY_MASTER.Pan_No as Comp_Pan_No, cast(TSPL_COMPANY_MASTER.Logo_Img as image) as Logo_Img, cast(TSPL_COMPANY_MASTER.Logo_Img2 as image) as Logo_Img2 , TSPL_COMPANY_MASTER.GSTREg_No as Comp_GSTREg_No , TSPL_COMPANY_MASTER.CINNO as Comp_CINNO, TSPL_COMPANY_MASTER.Access_Officer as Comp_Access_Officer ,TSPL_SCRAPSALE_DETAIL.TotalAmt,isnull(TSPL_SCRAPSALE_HEAD.RoundOffAmount,0) as RoundOffAmount, TSPL_SCRAPSALE_HEAD.Is_Taxable,TSPL_SCRAPINVOICE_HEAD.EWayBillNo,convert(varchar,TSPL_SCRAPINVOICE_HEAD.EWayBillDate,103) as EWayBillDate
                                                                    ,case when TSPL_SCRAPSALE_DETAIL.Row_Type='Item' then TSPL_ITEM_MASTER.HSN_Code else tspl_Additional_Charges.SAC_Code end as HSN_Code" &
                                            ",FromState.GST_STATE_CODE as From_GstStateCode,FromLocation.GSTNO as From_Loc_GstinNo,Customer_State.GST_STATE_CODE as Cust_GstStateCode,Customer_State.STATE_NAME AS Cust_StateName,TSPL_CUSTOMER_MASTER.GSTNO as Cust_GstInNo,ISNULL(TSPL_SCRAPSALE_DETAIL.DiscountAmt,0) AS DiscountAmt," &
                                            " convert(varchar,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) as shipment_Date, tspl_customer_master.PAN as Cust_Pan, TSPL_SCRAPINVOICE_HEAD.Description,FromLocation.HOAdd1 as frmHO1,FromLocation.HOAdd2 as frmHO2,ToLocation.HOAdd1 as to_HO1,ToLocation.HOAdd2 as to_HO2," &
                                        "TSPL_SCRAPINVOICE_HEAD.invoice_No,convert(varchar,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) as Invoice_Date,TSPL_SCRAPSALE_HEAD.Loc_Code as From_Location,FromLocation.Location_Desc as [From Location Desc],(FromLocation.Add1+FromLocation.Add2+FromLocation.Add3+FromLocation.Add4)as [From Address] ," &
                                        "FromLocation.Pin_Code,FromLocation.TIN_No,FromLocation.CST_No,FromLocation.State as From_State,FromState.State_Name as frm_State_name  ,TSPL_SCRAPSALE_HEAD.ToLoc_Code as To_Location,ToLocation.Location_Desc as To_Location_Desc,(ToLocation.Add1+ToLocation.Add2+ToLocation.Add3+ToLocation.Add4)as [To Address]," &
                                        "tspl_customer_master.Pin_Code as [To Pin Code],tspl_customer_master.TIN_No as [To TIN No],tspl_customer_master.CST as [To CST No],tspl_customer_master.Phone1 as [To phone],ToLocation.State as To_State,ToState.State_Name as To_state_name,TSPL_SCRAPSALE_DETAIL.Item_Code
                                                                ,case when TSPL_SCRAPSALE_DETAIL.Row_Type='Item' then TSPL_ITEM_MASTER.Item_Desc else tspl_Additional_Charges.Description end as Item_Desc ,FromLocation.Add1 as address1,FromLocation.Add2 as address2,FromLocation.Add3 as address3
                                                                ,Shipped_Qty," &
                                        "TSPL_SCRAPSALE_DETAIL.Unit_Code, Price, ItemAmt, TSPL_SCRAPSALE_HEAD.Invoice_Type,TSPL_SCRAPSALE_HEAD.Total_Tax_Amt,TSPL_SCRAPSALE_HEAD.Doc_Amt,	TAX1 .Tax_Code As tax1name,isnull (TSPL_SCRAPSALE_HEAD.tax1_rate,0) As txt1Rate,isnull (TSPL_SCRAPSALE_HEAD.tax1_amt,0) As txt1amt, "
                        Qry1 += " tax2.Tax_Code As tax2name,isnull (TSPL_SCRAPSALE_HEAD.tax2_rate,0) As txt2Rate,isnull (TSPL_SCRAPSALE_HEAD.tax2_amt,0) As txt2amt,"
                        Qry1 += " tax3.Tax_Code As tax3name,isnull (TSPL_SCRAPSALE_HEAD.tax3_rate,0) As txt3Rate,isnull (TSPL_SCRAPSALE_HEAD.tax3_amt,0) As txt3amt, "
                        Qry1 += " tax4.Tax_Code As tax4name,isnull (TSPL_SCRAPSALE_HEAD.tax4_rate,0) As txt4Rate,isnull (TSPL_SCRAPSALE_HEAD.tax4_amt,0) As txt4amt, "
                        Qry1 += " tax5.Tax_Code As tax5name,isnull (TSPL_SCRAPSALE_HEAD.tax5_rate,0) As txt5Rate,isnull (TSPL_SCRAPSALE_HEAD.tax5_amt,0) As txt5amt, "
                        Qry1 += "  tax6.Tax_Code  As tax6name,isnull (TSPL_SCRAPSALE_HEAD.tax6_rate,0) As txt6Rate,isnull (TSPL_SCRAPSALE_HEAD.tax6_amt,0) As txt6amt, "
                        Qry1 += "  tax7.Tax_Code  As tax7name,isnull (TSPL_SCRAPSALE_HEAD.tax7_rate,0) As txt7Rate,isnull (TSPL_SCRAPSALE_HEAD.tax7_amt,0) As txt7amt, "
                        Qry1 += " tax8.Tax_Code As tax8name,isnull (TSPL_SCRAPSALE_HEAD.tax8_rate,0) As txt8Rate,isnull (TSPL_SCRAPSALE_HEAD.tax8_amt,0) As txt8amt,  "
                        Qry1 += "  tax9.Tax_Code As tax9name,isnull (TSPL_SCRAPSALE_HEAD.tax9_rate,0) As txt9Rate,isnull (TSPL_SCRAPSALE_HEAD.tax9_amt,0) As txt9amt,'' as cin,'' as pan, '' as companyaddress,"
                        Qry1 += " tax10.Tax_Code as tax10name,isnull (TSPL_SCRAPSALE_HEAD.tax10_amt,0) as txt10amt ,TSPL_SCRAPSALE_HEAD.AddDesc1,isnull (TSPL_SCRAPSALE_HEAD.AddAmt1,0) as AddAmt1,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc2,isnull (TSPL_SCRAPSALE_HEAD.AddAmt2,0) as AddAmt2,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc3,isnull (TSPL_SCRAPSALE_HEAD.AddAmt3,0) as AddAmt3,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc4,isnull (TSPL_SCRAPSALE_HEAD.AddAmt4,0) as AddAmt4,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc5,isnull (TSPL_SCRAPSALE_HEAD.AddAmt5,0) as AddAmt5,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc6,isnull (TSPL_SCRAPSALE_HEAD.AddAmt6,0) as AddAmt6,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc7,isnull (TSPL_SCRAPSALE_HEAD.AddAmt7,0) as AddAmt7,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc8,isnull (TSPL_SCRAPSALE_HEAD.AddAmt8,0) as AddAmt8,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc9,isnull (TSPL_SCRAPSALE_HEAD.AddAmt9,0) as AddAmt9,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc10,isnull (TSPL_SCRAPSALE_HEAD.AddAmt10,0) as AddAmt10,"

                        '    Qry1 += "dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type," &
                        '     " isnull(TSPL_SCRAPSALE_DETAIL.TAX1_Amt ,0) as DTax1_Amt, isnull(TSPL_SCRAPSALE_DETAIL.TAX2_Amt ,0) as DTax2_Amt," &
                        '     " isnull(TSPL_SCRAPSALE_DETAIL.TAX3_Amt ,0) as DTax3_Amt, isnull(TSPL_SCRAPSALE_DETAIL.TAX4_Amt ,0) as DTax4_Amt," &
                        '     " isnull(TSPL_SCRAPSALE_DETAIL.TAX5_Amt ,0) as DTax5_Amt, isnull(TSPL_SCRAPSALE_DETAIL.TAX6_Amt ,0) as DTax6_Amt," &
                        '     " isnull(TSPL_SCRAPSALE_DETAIL.TAX7_Amt ,0) as DTax7_Amt, isnull(TSPL_SCRAPSALE_DETAIL.TAX8_Amt ,0) as DTax8_Amt," &
                        '     " isnull(TSPL_SCRAPSALE_DETAIL.TAX9_Amt ,0) as DTax9_Amt, isnull(TSPL_SCRAPSALE_DETAIL.TAX10_Amt ,0) as DTax10_Amt," &
                        '     " isnull(TSPL_SCRAPSALE_DETAIL.TAX1_Rate,0) as DTax1_Rate,  isnull(TSPL_SCRAPSALE_DETAIL.TAX2_Rate,0) as DTax2_Rate, " &
                        '      " isnull(TSPL_SCRAPSALE_DETAIL.TAX3_Rate,0) as DTax3_Rate,  isnull(TSPL_SCRAPSALE_DETAIL.TAX4_Rate,0) as DTax4_Rate," &
                        '       " isnull(TSPL_SCRAPSALE_DETAIL.TAX5_Rate,0) as DTax5_Rate,  isnull(TSPL_SCRAPSALE_DETAIL.TAX6_Rate,0) as DTax6_Rate," &
                        '        " isnull(TSPL_SCRAPSALE_DETAIL.TAX7_Rate,0) as DTax7_Rate,  isnull(TSPL_SCRAPSALE_DETAIL.TAX8_Rate,0) as DTax8_Rate," &
                        '         " isnull(TSPL_SCRAPSALE_DETAIL.TAX9_Rate,0) as DTax9_Rate,  isnull(TSPL_SCRAPSALE_DETAIL.TAX10_Rate,0) as DTax10_Rate,"

                        Qry1 += " TSPL_GL_SEGMENT_CODE.Description as vehicle_code,TSPL_SCRAPSALE_HEAD.Transport_code,TSPL_TRANSPORT_MASTER.Transporter_name,TSPL_SCRAPSALE_HEAD.Cust_code,TSPL_SCRAPSALE_HEAD.cust_name,tspl_customer_master.add1,tspl_customer_master.add2,tspl_customer_master.add3,tspl_city_master.city_name,TSPL_SCRAPSALE_HEAD.Electronic_ref_No,FromLocation.accountholdername,FromLocation.Bank,FromLocation.Branch,FromLocation.ACType,FromLocation.bankaccno,FromLocation.bankifsccode,fromlocation.Pin_Code as PinNo,
	  fromlocation.Phone1 as LPhone,fromlocation.Service_Tax_Reg_No as Registration_No
                            from TSPL_SCRAPSALE_HEAD 
                            left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code  =TSPL_SCRAPSALE_HEAD.Vehicle_Id and TSPL_GL_SEGMENT_CODE.Seg_No='2'
                            left join TSPL_SCRAPSALE_DETAIL on TSPL_SCRAPSALE_DETAIL.shipment_No=TSPL_SCRAPSALE_HEAD.shipment_No left join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD. shipment_No=TSPL_SCRAPSALE_HEAD.shipment_No left join TSPL_LOCATION_MASTER as FromLocation on FromLocation.Location_Code=TSPL_SCRAPSALE_HEAD.Loc_Code left join TSPL_LOCATION_MASTER as ToLocation on ToLocation.Location_Code=TSPL_SCRAPSALE_HEAD.ToLoc_Code 
                            left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                            left join tspl_Additional_Charges on tspl_Additional_Charges.Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                            left join TSPL_STATE_MASTER as  FromState on FromState.State_Code=FromLocation.State"
                        Qry1 += " left join TSPL_STATE_MASTER as  ToState on ToState.State_Code=ToLocation.State"
                        Qry1 += " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SCRAPSALE_HEAD.tax1 "
                        Qry1 += "  left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SCRAPSALE_HEAD.tax2 "
                        Qry1 += " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SCRAPSALE_HEAD .TAX3 "
                        Qry1 += "  left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SCRAPSALE_HEAD .tax4 "
                        Qry1 += " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SCRAPSALE_HEAD .tax5 "
                        Qry1 += " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX6 "
                        Qry1 += " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX7 "
                        Qry1 += " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX8 "
                        Qry1 += " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX9 "
                        Qry1 += " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX10 "

                        '    Qry1 += "left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_SCRAPSALE_DETAIL.tax1  " &
                        ' " left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_SCRAPSALE_DETAIL.tax2 " &
                        '  "left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_SCRAPSALE_DETAIL .TAX3  " &
                        '   " left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_SCRAPSALE_DETAIL .tax4 " &
                        '    "left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_SCRAPSALE_DETAIL .tax5 " &
                        '     " left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX6  " &
                        '     "left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX7  " &
                        '     " left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX8  " &
                        '     " left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX9 " &
                        '      " left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX10 " &
                        Qry1 += " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_SCRAPSALE_HEAD.Transport_code  left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SCRAPSALE_HEAD.cust_code "
                        Qry1 += " left join TSPL_STATE_MASTER as Customer_State on tspl_customer_master .State=Customer_State.STATE_CODE "
                        Qry1 += " left join tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code  left outer join tspl_company_master on tspl_company_master.comp_code = TSPL_SCRAPSALE_HEAD.Comp_Code  where TSPL_SCRAPSALE_HEAD.shipment_No='" & txtDocNo.Value & "'"
                        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "RCDFCF") = CompairStringResult.Equal Then
                            Qry1 = " Select * from ( " + Qry1 + " )XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE' as CopyType1  ) YYY ON YYY.COL1=XXX.CopyType  ORDER BY YYY.COL2,Line_No "
                        End If
                        Dim dt1 As DataTable
                        dt1 = clsDBFuncationality.GetDataTable(Qry1)

                        ' ==============================Ticket No  ERO/27/11/18-000422 By Prabhakar for  Customer Dashboard on Print ==============================================
                        Dim dtCustomerOutstanding As DataTable = Nothing
                        Dim itemSummnary As DataTable = Nothing
                        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "RCDFCF") = CompairStringResult.Equal Then
                            Dim strQry1 As String = "select max(TSPL_SCRAPINVOICE_HEAD.invoice_No) as Invoice, max(TSPL_ITEM_MASTER.Item_Desc) AS Item_Name,TSPL_SCRAPSALE_DETAIL.Unit_Code as UOM, SUM( Shipped_Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor ) as WeightInKg, sum( ItemAmt ) as Amt from TSPL_SCRAPSALE_HEAD left join TSPL_SCRAPSALE_DETAIL on TSPL_SCRAPSALE_DETAIL.shipment_No = TSPL_SCRAPSALE_HEAD.shipment_No left join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.shipment_No = TSPL_SCRAPSALE_HEAD.shipment_No left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SCRAPSALE_DETAIL.Item_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SCRAPSALE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code = 'KG' where TSPL_SCRAPSALE_HEAD.shipment_No = '" & txtDocNo.Value & "' group by TSPL_SCRAPSALE_DETAIL.Item_Code, TSPL_SCRAPSALE_DETAIL.Unit_code"
                            itemSummnary = clsDBFuncationality.GetDataTable(strQry1)
                        Else
                            dtCustomerOutstanding = clsCustomerMaster.getCustomerOutstandingOfAmt_Can_Crate("'" & clsCommon.myCstr(dt1.Rows(0)("Cust_code")) & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dt1.Rows(0)("Invoice_Date")).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dt1.Rows(0)("Invoice_Date")), "dd/MMM/yyyy"))

                        End If
                        '=============================================================================

                        If isPrint Then
                            SetItemWiseTax(dt, InvoiceNo)
                            'If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Excisable")), "Y") = CompairStringResult.Equal Then
                            '    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "VIZAG") = CompairStringResult.Equal Then
                            '        filePath=frmCRV.funreport(isPDFPath,CrystalReportFolder.PurchaseOrder, dt, "ScrapsaleInvoice4ExcisePrintfor VIZAG", "ScrapSaleInvoiceRpt")
                            '    Else
                            '        filePath=frmCRV.funreport(isPDFPath,CrystalReportFolder.PurchaseOrder, dt, "ScrapSaleInvoice4ExcisePrint", "ScrapSaleInvoiceRpt")
                            '    End If
                            'Else
                            ' Ticket No : SHR/14/06/18-000030 By Prabhakar  , Add client-VIJAYA
                            If (clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "RCDFCF") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "001") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal) Or clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal Or clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "PSFI") = CompairStringResult.Equal Or clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
                                'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                                '    filePath=frmCRV.funsubreportWithdt(isPDFPath,CrystalReportFolder.PurchaseOrder, dt1, Nothing, "rptMaterialSaleInvoice", "ScrapnSale Invoice Local", clsCommon.myCDate(dt1.Rows(0)("Invoice_Date")), Nothing, "rptCustomerOutstandingErode.rpt", dtCustomerOutstanding)
                                '    Return
                                'End If

                                If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dt1.Rows(0)("Invoice_Date"))) Then
                                    If clsCommon.myCdbl(dt1.Rows(0)("Is_Taxable")) = 1 Then
                                        'If clsCommon.CompairString(clsCommon.myCstr(dt1.Rows(0)("frm_State_name")), clsCommon.myCstr(dt1.Rows(0)("Cust_StateName"))) = CompairStringResult.Equal Then
                                        '    filePath=frmCRV.funsubreportWithdt(isPDFPath,CrystalReportFolder.PurchaseOrder, dt1, clsERPFuncationality.CompanyAddresShowinFooter(), "rptMaterialSaleInvoice", "ScrapnSale Invoice Local", clsCommon.myCDate(dt1.Rows(0)("Invoice_Date")), "rptCompanyAddress.rpt", "rptCustomerOutstandingErode.rpt", dtCustomerOutstanding)
                                        'Else
                                        '    If IsMandiTax > 0 Then
                                        '        filePath=frmCRV.funsubreportWithdt(isPDFPath,CrystalReportFolder.PurchaseOrder, dt1, clsERPFuncationality.CompanyAddresShowinFooter(), "rptMaterialSaleInvoice_InterState_WithMandi", "ScrapnSale Invoice InterState", clsCommon.myCDate(dt1.Rows(0)("Invoice_Date")), "rptCompanyAddress.rpt", "rptCustomerOutstandingErode.rpt", dtCustomerOutstanding)
                                        '    Else
                                        '        'filePath=frmCRV.funsubreportWithdt(isPDFPath,CrystalReportFolder.PurchaseOrder, dt1, clsERPFuncationality.CompanyAddresShowinFooter(), "rptMaterialSaleInvoice_InterState", "ScrapnSale Invoice InterState", clsCommon.myCDate(dt1.Rows(0)("Invoice_Date")), "rptCompanyAddress.rpt", "rptCustomerOutstandingErode.rpt", dtCustomerOutstanding)
                                        If ischallan = True Then
                                            filePath = frmCRV.funsubreportWithdt(isPDFPath, CrystalReportFolder.KwalitySalesReport, dt1, itemSummnary, "rptScrapSaleInvoice_RCDFCF", "ScrapnSale Invoice ", clsCommon.myCDate(dt1.Rows(0)("Invoice_Date")), "rptSubItemSummary.rpt", )
                                        Else
                                            filePath = frmCRV.funsubreportWithdt(isPDFPath, CrystalReportFolder.KwalitySalesReport, dt1, itemSummnary, "rptScrapSaleChallan_RCDFCF", "ScrapnSale Invoice ", clsCommon.myCDate(dt1.Rows(0)("Invoice_Date")), "rptSubItemSummary.rpt", )
                                        End If

                                        'End If
                                        'filePath=frmCRV.funsubreportWithdt(isPDFPath,CrystalReportFolder.KwalitySalesReport, dt1, itemSummnary, "rptScrapSaleInvoice_RCDFCF", "ScrapnSale Invoice ", clsCommon.myCDate(dt1.Rows(0)("Invoice_Date")), "rptSubItemSummary.rpt", )

                                    Else
                                        If ischallan = False Then
                                            filePath = frmCRV.funsubreportWithdt(isPDFPath, CrystalReportFolder.KwalitySalesReport, dt1, itemSummnary, "rptScrapSaleChallan_RCDFCF", "ScrapnSale Invoice ", clsCommon.myCDate(dt1.Rows(0)("Invoice_Date")), "rptSubItemSummary.rpt", )
                                        Else
                                            'filePath=frmCRV.funsubreportWithdt(isPDFPath,CrystalReportFolder.PurchaseOrder, dt1, clsERPFuncationality.CompanyAddresShowinFooter(), "rptMaterialSaleInvoice_NonTaxable", "ScrapnSale Invoice Non Taxable", clsCommon.myCDate(dt1.Rows(0)("Invoice_Date")), "rptCompanyAddress.rpt", "rptCustomerOutstandingErode.rpt", dtCustomerOutstanding)
                                            filePath = frmCRV.funsubreportWithdt(isPDFPath, CrystalReportFolder.KwalitySalesReport, dt1, itemSummnary, "rptScrapSaleInvoice_RCDFCF_NT", "ScrapnSale Invoice ", clsCommon.myCDate(dt1.Rows(0)("Invoice_Date")), "rptSubItemSummary.rpt", )
                                        End If
                                    End If
                                    'Else
                                    '    filePath=frmCRV.funsubreportWithdt(isPDFPath,CrystalReportFolder.PurchaseOrder, dt1, clsERPFuncationality.CompanyAddresShowinFooter(), "ScrapSaleInvoice", "ScrapnSale Invoice", "rptCompanyAddress.rpt")
                                    '    'PurchaseOrderViewer.funreport(isPDFPath,dt1, "ScrapSaleInvoice", "ScrapSaleInvoiceRpt")
                                End If
                            End If
                            'End If
                            'Else
                            'EnumTecxpertPaperSize.PaperSize10x12
                            filePath = frmCRV.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, "ScrapSaleInvoice4Excise", "ScrapSaleInvoiceRpt")
                        End If
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "Please select one Invoice", Me.Text)
                End If
            End If
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return filePath
    End Function

    'Done  by sanjay in UDL Plant 
    Public Function PrintMaterialSale() As String
        '"ToState.GST_STATE_CODE as Cust_GstStateCode,ToState.STATE_NAME AS Cust_StateName"
        Dim strQuery As String = Nothing
        'Dim TempInvoiceDate = clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select shipment_Date from TSPL_SCRAPINVOICE_HEAD where invoice_no='" & lblInvoiceNo.Text & "'"))
        'Dim IsTaxable = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Is_Taxable from TSPL_SCRAPINVOICE_HEAD where invoice_no='" & lblInvoiceNo.Text & "'"))
        'Dim ECustomerType = clsERPFuncationality.GetCustomerEInvoiceTypeFromTransationTable("TSPL_SCRAPINVOICE_HEAD", "invoice_no", lblInvoiceNo.Text, Nothing)
        Dim IsEInvoiceApply As Integer = 0
        If chkTaxable.Checked = True AndAlso clsERPFuncationality.GetEInvoiceStatus(dtpshipment.Value) = True AndAlso clsCommon.CompairString(EInvoiceType, "BB") = CompairStringResult.Equal Then
            IsEInvoiceApply = 1
        End If

        strQuery = "SELECT cast(TSPL_SCRAPINVOICE_HEAD.BarCode_Img as image) As BarCode_Img,isnull (TSPL_SCRAPINVOICE_HEAD.IRN_No,'') as IRN_No,isnull (TSPL_SCRAPINVOICE_HEAD.Ack_No,'') as Ack_No,case when len(isnull (TSPL_SCRAPINVOICE_HEAD.Ack_No,'')) > 0 then convert (varchar, TSPL_SCRAPINVOICE_HEAD.Ack_Date,103) else ''  end as Ack_Date, " + clsCommon.myCstr(IsEInvoiceApply) + " as  IsEInvoiceApply," &
        " tax1.Is_TCS as tax1_Is_TCS, tax2.Is_TCS as tax2_Is_TCS,tax3.Is_TCS as tax3_Is_TCS,tax4.Is_TCS as tax4_Is_TCS,tax5.Is_TCS as tax5_Is_TCS,tax6.Is_TCS as tax6_Is_TCS,tax7.Is_TCS as tax7_Is_TCS,tax8.Is_TCS as tax8_Is_TCS,tax9.Is_TCS as tax9_Is_TCS,tax10.Is_TCS as tax10_Is_TCS,Case when tax1.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX1_Rate when  tax2.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX2_Rate when tax3.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX3_Rate when tax4.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX4_Rate  when tax5.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX5_Rate when tax6.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX6_Rate  when tax7.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX7_Rate when tax8.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX8_Rate when tax9.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX9_Rate when tax10.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX10_Rate end as TCS_Rate,Case when tax1.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX1_Amt when  tax2.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX2_Amt when tax3.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX3_Amt when tax4.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX4_Amt  when tax5.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX5_Amt when tax6.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX6_Amt  when tax7.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX7_Amt when tax8.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX8_Amt when tax9.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX9_Amt when tax10.Is_TCS = 'Y' then TSPL_SCRAPSALE_HEAD.TAX10_Amt end  as TCS_Amount, " &
            "TSPL_SCRAPINVOICE_DETAIL.shipped_Qty as Net_Qty,isnull(TSPL_ITEM_MASTER.Is_Batch_Item,0) as Is_Batch_Item,TSPL_SCRAPSALE_HEAD.Is_Taxable,'" + strPrintType + "' as Print_Type ,TSPL_COMPANY_MASTER.Insurance_Comp_Name ,TSPL_COMPANY_MASTER.Insurance_No ,convert(varchar,TSPL_COMPANY_MASTER.Insurance_Valid_Date ,103) AS Insurance_Valid_Date, TSPL_COMPANY_MASTER.State as comp_state,TSPL_COMPANY_MASTER.Pincode as copm_pincode,TSPL_COMPANY_MASTER.Add1 as comp_Add1 ,TSPL_COMPANY_MASTER.Add2 as comp_Add2 ,TSPL_COMPANY_MASTER.Add3  as comp_add3,TSPL_COMPANY_MASTER.Fax  as comp_fax , TSPL_COMPANY_MASTER.Circle_No AS TarrifNo,TSPL_COMPANY_MASTER.CE_Division as Divison,TSPL_COMPANY_MASTER.CE_Commissionerate ,TSPL_COMPANY_MASTER.CE_Range ,TSPL_COMPANY_MASTER.Ecc_No as ExciseRegdNo , TSPL_CUSTOMER_MASTER.ECC," &
         " ( case when TSPL_COMPANY_MASTER.Phone2  <> '' then TSPL_COMPANY_MASTER.Phone1 +','+TSPL_COMPANY_MASTER.Phone2 else TSPL_COMPANY_MASTER.Phone1 end) as Comp_Phn,convert(varchar,TSPL_COMPANY_MASTER.TinNo_Issue_Date,103) as TinNo_Issue_Date,convert(varchar,TSPL_COMPANY_MASTER.PanNo_Issue_Date,103) PanNo_Issue_Date ," &
         " TSPL_SCRAPINVOICE_HEAD.Total_Gross_Weight,TSPL_SCRAPINVOICE_HEAD.Total_Net_Weight, convert(varchar,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) as shipment_Date, tspl_customer_master.PAN as Cust_Pan, TSPL_SCRAPINVOICE_HEAD.Description,FromLocation.HOAdd1 as frmHO1,FromLocation.HOAdd2 as frmHO2,ToLocation.HOAdd1 as to_HO1,ToLocation.HOAdd2 as to_HO2,TSPL_SCRAPINVOICE_HEAD.invoice_No,convert(varchar,TSPL_SCRAPINVOICE_HEAD.posting_Date,103) as Invoice_Date,TSPL_SCRAPSALE_HEAD.Loc_Code as From_Location,FromLocation.Location_Desc as [From Location Desc],(FromLocation.Add1+FromLocation.Add2+FromLocation.Add3+FromLocation.Add4)as [From Address] ,FromLocation.Pin_Code,FromLocation.TIN_No,FromLocation.CST_No,FromLocation.State as From_State,FromState.State_Name as frm_State_name  ,TSPL_SCRAPSALE_HEAD.ToLoc_Code as To_Location,ToLocation.Location_Desc as To_Location_Desc,(ToLocation.Add1+ToLocation.Add2+ToLocation.Add3+ToLocation.Add4)as [To Address],tspl_customer_master.Pin_Code as [To Pin Code],tspl_customer_master.TIN_No as [To TIN No],tspl_customer_master.CST as [To CST No],tspl_customer_master.Phone1 as [To phone],ToLocation.State as To_State,ToState.State_Name as To_state_name,TSPL_SCRAPINVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,(CASE WHEN TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN TSPL_BATCH_ITEM.Qty ELSE TSPL_SCRAPINVOICE_DETAIL.shipped_Qty END )as Shipped_Qty," &
        "TSPL_SCRAPINVOICE_DETAIL.Unit_Code, TSPL_SCRAPINVOICE_DETAIL.Price, TSPL_SCRAPINVOICE_DETAIL.ItemAmt, TSPL_SCRAPSALE_HEAD.Invoice_Type, TSPL_SCRAPSALE_HEAD.Total_Tax_Amt, TSPL_SCRAPSALE_HEAD.Doc_Amt, " &
        "tspl_customer_master.Customer_Name,tspl_customer_master.Add1,tspl_customer_master.Add2,tspl_customer_master.Add3,tspl_customer_master.State,tspl_customer_master.Cust_Code,tspl_customer_master.CST,tspl_customer_master.Lst_No,FromLocation.Loc_Short_Name," &
        "TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,TSPL_COMPANY_MASTER.Tcan_No AS Website,TSPL_COMPANY_MASTER.Access_Officer as FSSAI_NO,TSPL_COMPANY_MASTER.Email AS Comp_Email,TSPL_COMPANY_MASTER.CINNo as CORP_NO,TSPL_COMPANY_MASTER.Pan_No AS Comp_PanNo,TSPL_COMPANY_MASTER.Tin_No as Comp_TinNo," &
        "TSPL_BATCH_ITEM.Batch_No,TSPL_SCRAPINVOICE_HEAD.Discount_Amt,TSPL_SCRAPINVOICE_HEAD.Balance_Amt,TSPL_SCRAPINVOICE_HEAD.Amount_Less_Discount,TSPL_SCRAPINVOICE_HEAD.ship_Total_Amt, " &
        "TAX2= REPLACE((CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX1!='' AND tax1.Type!='E' THEN(tax1.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX1_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX1_Amt)) ELSE '' END)+' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX2!='' AND tax2.Type!='E' THEN(tax2.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX2_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX2_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX3!='' AND tax3.Type!='E' THEN(tax3.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX3_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX3_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX4!='' AND tax4.Type!='E' THEN(tax4.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX4_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX4_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX5!='' AND tax5.Type!='E' THEN(tax5.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX5_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX5_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX6!='' AND tax6.Type!='E' THEN(tax6.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX6_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX6_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX7!='' AND tax7.Type!='E' THEN(tax7.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX7_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX7_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX8!='' AND tax8.Type!='E' THEN(tax8.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX8_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX8_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX9!='' AND tax9.Type!='E' THEN(tax9.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX9_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX9_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX10!='' AND tax10.Type!='E' THEN(tax10.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX10_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX10_Amt))ELSE '' END),CHAR(13),'')," &
        " Add_Charge_Name1=(CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc1!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc1+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt1)) ELSE '' END)+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc2!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc2+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt2)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc3!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc3+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt3)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc4!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc4+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt4)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc5!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc5+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt5)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc6!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc6+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt6)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc7!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc7+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt7)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc8!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc8+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt8)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc9!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc9+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt9)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc10!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc10+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt10)) ELSE '' END)," &
       "	TAX1 .Tax_Code_Desc as tax1name,isnull (TSPL_SCRAPSALE_HEAD.tax1_rate,0) as txt1Rate,isnull (TSPL_SCRAPSALE_HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SCRAPSALE_HEAD.tax2_rate,0) as txt2Rate,isnull (TSPL_SCRAPSALE_HEAD.tax2_amt,0) as txt2amt, tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SCRAPSALE_HEAD.tax3_rate,0) as txt3Rate,isnull (TSPL_SCRAPSALE_HEAD.tax3_amt,0) as txt3amt,  tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SCRAPSALE_HEAD.tax4_rate,0) as txt4Rate,isnull (TSPL_SCRAPSALE_HEAD.tax4_amt,0) as txt4amt,  tax5.Tax_Code_Desc as tax5name,isnull (TSPL_SCRAPSALE_HEAD.tax5_rate,0) as txt5Rate,isnull (TSPL_SCRAPSALE_HEAD.tax5_amt,0) as txt5amt,   tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SCRAPSALE_HEAD.tax6_rate,0) as txt6Rate,isnull (TSPL_SCRAPSALE_HEAD.tax6_amt,0) as txt6amt,   tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SCRAPSALE_HEAD.tax7_rate,0) as txt7Rate,isnull (TSPL_SCRAPSALE_HEAD.tax7_amt,0) as txt7amt,  tax8.Tax_Code_Desc as tax8name,isnull (TSPL_SCRAPSALE_HEAD.tax8_rate,0) as txt8Rate,isnull (TSPL_SCRAPSALE_HEAD.tax8_amt,0) as txt8amt,    tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SCRAPSALE_HEAD.tax9_rate,0) as txt9Rate,isnull (TSPL_SCRAPSALE_HEAD.tax9_amt,0) as txt9amt,'' as cin,'' as pan, '' as companyaddress, tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SCRAPSALE_HEAD.tax10_amt,0) as txt10amt ,TSPL_SCRAPSALE_HEAD.AddDesc1,isnull (TSPL_SCRAPSALE_HEAD.AddAmt1,0) as AddAmt1, TSPL_SCRAPSALE_HEAD.AddDesc2,isnull (TSPL_SCRAPSALE_HEAD.AddAmt2,0) as AddAmt2, TSPL_SCRAPSALE_HEAD.AddDesc3,isnull (TSPL_SCRAPSALE_HEAD.AddAmt3,0) as AddAmt3, TSPL_SCRAPSALE_HEAD.AddDesc4,isnull (TSPL_SCRAPSALE_HEAD.AddAmt4,0) as AddAmt4, TSPL_SCRAPSALE_HEAD.AddDesc5,isnull (TSPL_SCRAPSALE_HEAD.AddAmt5,0) as AddAmt5, TSPL_SCRAPSALE_HEAD.AddDesc6,isnull (TSPL_SCRAPSALE_HEAD.AddAmt6,0) as AddAmt6, TSPL_SCRAPSALE_HEAD.AddDesc7,isnull (TSPL_SCRAPSALE_HEAD.AddAmt7,0) as AddAmt7, TSPL_SCRAPSALE_HEAD.AddDesc8,isnull (TSPL_SCRAPSALE_HEAD.AddAmt8,0) as AddAmt8, TSPL_SCRAPSALE_HEAD.AddDesc9,isnull (TSPL_SCRAPSALE_HEAD.AddAmt9,0) as AddAmt9, TSPL_SCRAPSALE_HEAD.AddDesc10,isnull (TSPL_SCRAPSALE_HEAD.AddAmt10,0) as AddAmt10,TSPL_SCRAPSALE_HEAD.vehicle_code,TSPL_SCRAPSALE_HEAD.Transport_code,TSPL_TRANSPORT_MASTER.Transporter_name,TSPL_SCRAPSALE_HEAD.cust_name,tspl_customer_master.add1,tspl_customer_master.add2,tspl_customer_master.add3,tspl_city_master.city_name,TSPL_SCRAPSALE_HEAD.Is_CashSale,TSPL_SCRAPSALE_HEAD.Is_Scrap,isnull (TSPL_SCRAPSALE_HEAD.TAX10_Rate,0) as txt10Rate  "

        strQuery += " ,TSPL_SCRAPSALE_HEAD.EWayBillNo,convert(varchar,TSPL_SCRAPSALE_HEAD.EWayBillDate,103) as EWayBillDate " &
                   " ,TSPL_SCRAPSALE_HEAD.Electronic_ref_No as Electronic_Ref_No" &
                    " , TSPL_ITEM_MASTER.HSN_Code,FromState.GST_STATE_CODE as From_GstStateCode,FromLocation.GSTNO as From_Loc_GstinNo," &
                   "STATEMASTER_CUSTOMER.GST_STATE_CODE as Cust_GstStateCode,STATEMASTER_CUSTOMER.STATE_NAME AS Cust_StateName" &
                    ",TSPL_CUSTOMER_MASTER.GSTNO as Cust_GstInNo,ISNULL(TSPL_SCRAPINVOICE_DETAIL.DiscountAmt,0) AS DeatilDiscountAmt" &
                     ",dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type," &
                                " isnull(TSPL_SCRAPSALE_DETAIL.TAX1_Amt ,0) as DTax1_Amt, isnull(TSPL_SCRAPSALE_DETAIL.TAX2_Amt ,0) as DTax2_Amt," &
                                " isnull(TSPL_SCRAPSALE_DETAIL.TAX3_Amt ,0) as DTax3_Amt, isnull(TSPL_SCRAPSALE_DETAIL.TAX4_Amt ,0) as DTax4_Amt," &
                                " isnull(TSPL_SCRAPSALE_DETAIL.TAX5_Amt ,0) as DTax5_Amt, isnull(TSPL_SCRAPSALE_DETAIL.TAX6_Amt ,0) as DTax6_Amt," &
                                " isnull(TSPL_SCRAPSALE_DETAIL.TAX7_Amt ,0) as DTax7_Amt, isnull(TSPL_SCRAPSALE_DETAIL.TAX8_Amt ,0) as DTax8_Amt," &
                                " isnull(TSPL_SCRAPSALE_DETAIL.TAX9_Amt ,0) as DTax9_Amt, isnull(TSPL_SCRAPSALE_DETAIL.TAX10_Amt ,0) as DTax10_Amt," &
                                " isnull(TSPL_SCRAPSALE_DETAIL.TAX1_Rate,0) as DTax1_Rate,  isnull(TSPL_SCRAPSALE_DETAIL.TAX2_Rate,0) as DTax2_Rate, " &
                                 " isnull(TSPL_SCRAPSALE_DETAIL.TAX3_Rate,0) as DTax3_Rate,  isnull(TSPL_SCRAPSALE_DETAIL.TAX4_Rate,0) as DTax4_Rate," &
                                  " isnull(TSPL_SCRAPSALE_DETAIL.TAX5_Rate,0) as DTax5_Rate,  isnull(TSPL_SCRAPSALE_DETAIL.TAX6_Rate,0) as DTax6_Rate," &
                                   " isnull(TSPL_SCRAPSALE_DETAIL.TAX7_Rate,0) as DTax7_Rate,  isnull(TSPL_SCRAPSALE_DETAIL.TAX8_Rate,0) as DTax8_Rate," &
                                    " isnull(TSPL_SCRAPSALE_DETAIL.TAX9_Rate,0) as DTax9_Rate,  isnull(TSPL_SCRAPSALE_DETAIL.TAX10_Rate,0) as DTax10_Rate"

        strQuery += " ,tax1.Is_MandiTaxCess as tax1_MandiTaxCess,tax2.Is_MandiTaxCess as tax2_MandiTaxCess,tax3.Is_MandiTaxCess as tax3_MandiTaxCess,tax4.Is_MandiTaxCess as tax4_MandiTaxCess,tax5.Is_MandiTaxCess as tax5_MandiTaxCess,tax6.Is_MandiTaxCess as tax6_MandiTaxCess,tax7.Is_MandiTaxCess as tax7_MandiTaxCess,tax8.Is_MandiTaxCess as tax8_MandiTaxCess,tax9.Is_MandiTaxCess as tax9_MandiTaxCess,tax10.Is_MandiTaxCess as tax10_MandiTaxCess "

        strQuery += " , isnull(TSPL_SCRAPSALE_DETAIL.TAX1_Base_Amt,0) as DBase1_Amt,  isnull(TSPL_SCRAPSALE_DETAIL.TAX2_Base_Amt,0) as DBase2_Amt,  " &
                    " isnull(TSPL_SCRAPSALE_DETAIL.TAX3_Base_Amt,0) as DBase3_Amt,  isnull(TSPL_SCRAPSALE_DETAIL.TAX4_Base_Amt,0) as DBase4_Amt, " &
                    " isnull(TSPL_SCRAPSALE_DETAIL.TAX5_Base_Amt,0) as DBase5_Amt,  isnull(TSPL_SCRAPSALE_DETAIL.TAX6_Base_Amt,0) as DBase6_Amt, " &
                    " isnull(TSPL_SCRAPSALE_DETAIL.TAX7_Base_Amt,0) as DBase7_Amt,  isnull(TSPL_SCRAPSALE_DETAIL.TAX8_Base_Amt,0) as DBase8_Amt, " &
                    " isnull(TSPL_SCRAPSALE_DETAIL.TAX9_Base_Amt,0) as DBase9_Amt,  isnull(TSPL_SCRAPSALE_DETAIL.TAX10_Base_Amt,0) as DBase10_Amt "

        strQuery += " from TSPL_SCRAPSALE_HEAD  left join TSPL_SCRAPSALE_DETAIL on TSPL_SCRAPSALE_DETAIL.shipment_No=TSPL_SCRAPSALE_HEAD.shipment_No " &
 "left join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD. shipment_No=TSPL_SCRAPSALE_HEAD.shipment_No " &
 "left join TSPL_SCRAPINVOICE_DETAIL on TSPL_SCRAPINVOICE_DETAIL.invoice_No=TSPL_SCRAPINVOICE_HEAD.invoice_No " &
" and TSPL_SCRAPSALE_DETAIL.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code " &
" and TSPL_SCRAPSALE_DETAIL.Unit_code=TSPL_SCRAPINVOICE_DETAIL.Unit_code  and TSPL_SCRAPSALE_DETAIL.Line_No=TSPL_SCRAPINVOICE_DETAIL.Line_No   " &
"left join TSPL_LOCATION_MASTER as FromLocation on FromLocation.Location_Code=TSPL_SCRAPSALE_HEAD.Loc_Code " &
  "left join TSPL_LOCATION_MASTER as ToLocation on ToLocation.Location_Code=TSPL_SCRAPSALE_HEAD.ToLoc_Code " &
  "left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code " &
  "left join TSPL_STATE_MASTER as  FromState on FromState.State_Code=FromLocation.State  " &
  "left join TSPL_STATE_MASTER as  ToState on ToState.State_Code=ToLocation.State  " &
   "left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SCRAPSALE_HEAD.tax1 " &
    "left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SCRAPSALE_HEAD.tax2 " &
     "left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SCRAPSALE_HEAD .TAX3   " &
     "left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SCRAPSALE_HEAD .tax4 " &
      "left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SCRAPSALE_HEAD .tax5 " &
       "left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX6  " &
       "left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX7  " &
       "left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX8  " &
       "left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX9  " &
       "left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX10 "

        strQuery += "left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_SCRAPSALE_DETAIL.tax1  " &
                   " left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_SCRAPSALE_DETAIL.tax2 " &
                    "left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_SCRAPSALE_DETAIL .TAX3  " &
                     " left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_SCRAPSALE_DETAIL .tax4 " &
                      "left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_SCRAPSALE_DETAIL .tax5 " &
                       " left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX6  " &
                       "left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX7  " &
                       " left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX8  " &
                       " left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX9 " &
                        " left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX10 "

        strQuery += "left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_SCRAPSALE_HEAD.Transport_code  " &
       "left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SCRAPSALE_HEAD.cust_code " &
       " LEFT OUTER JOIN TSPL_STATE_MASTER STATEMASTER_CUSTOMER ON STATEMASTER_CUSTOMER.State_Code=TSPL_CUSTOMER_MASTER.State " &
       "left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SCRAPSALE_HEAD.Comp_Code " &
        "left join tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code  " &
        "LEFT JOIN TSPL_BATCH_ITEM  ON TSPL_BATCH_ITEM.Document_Code=TSPL_SCRAPINVOICE_DETAIL.invoice_No AND " &
        "TSPL_BATCH_ITEM.Parent_Line_No=TSPL_SCRAPINVOICE_DETAIL.Line_No AND TSPL_BATCH_ITEM.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code AND TSPL_BATCH_ITEM.UOM=TSPL_SCRAPINVOICE_DETAIL.Unit_Code WHERE TSPL_SCRAPSALE_HEAD.shipment_No='" & txtDocNo.Value & "' "
        Return strQuery
    End Function


    Private Function SetItemWiseTax(ByVal dtAfterModify As DataTable, ByVal strDocumentNo As String) As DataTable
        dtAfterModify.Columns.Add("TAX1_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX2_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX3_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX4_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX5_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt3", GetType(Double))




        Dim qry As String = "select Tax,Rate,SUM(Amt) as TaxAmt"
        qry += " from ("
        qry += " select TAX1 as Tax,TAX1_Rate as Rate,TAX1_Amt as Amt"
        qry += " from TSPL_SCRAPINVOICE_DETAIL where invoice_No='" + strDocumentNo + "' "
        qry += " union all "
        qry += " select TAX2 as Tax,TAX2_Rate as Rate,TAX2_Amt as Amt "
        qry += " from TSPL_SCRAPINVOICE_DETAIL where invoice_No='" + strDocumentNo + "'  "
        qry += " union all "
        qry += " select TAX3 as Tax,TAX3_Rate as Rate,TAX3_Amt as Amt "
        qry += " from TSPL_SCRAPINVOICE_DETAIL where invoice_No='" + strDocumentNo + "'  "
        qry += " union all "
        qry += " select TAX4 as Tax,TAX4_Rate as Rate,TAX4_Amt as Amt "
        qry += " from TSPL_SCRAPINVOICE_DETAIL where invoice_No='" + strDocumentNo + "'  "
        qry += " union all "
        qry += " select TAX5 as Tax,TAX5_Rate as Rate,TAX5_Amt as Amt "
        qry += " from TSPL_SCRAPINVOICE_DETAIL where invoice_No='" + strDocumentNo + "'   "
        qry += " union all "
        qry += " select TAX6 as Tax,TAX6_Rate as Rate,TAX6_Amt as Amt "
        qry += " from TSPL_SCRAPINVOICE_DETAIL where invoice_No='" + strDocumentNo + "'   "
        qry += " )xxx "
        qry += " group by Tax,Rate   having SUM(Amt)>0   "


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                For ii As Integer = 1 To 5
                    Dim strCol As String = "TaxRateDesc" + clsCommon.myCstr(ii) + ""
                    If clsCommon.CompairString(clsCommon.myCstr(dtAfterModify.Rows(0)(strCol)), clsCommon.myCstr(dr("Tax"))) = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt1")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate1") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt1") = Math.Round(clsCommon.myCdbl(dr("TaxAmt")), 0, MidpointRounding.AwayFromZero)
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt2") = 0
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt3") = 0
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt2")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate2") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt2") = Math.Round(clsCommon.myCdbl(dr("TaxAmt")), 0, MidpointRounding.AwayFromZero)
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt1") = 0
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt3") = 0
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt3")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate3") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt3") = Math.Round(clsCommon.myCdbl(dr("TaxAmt")), 0, MidpointRounding.AwayFromZero)
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt1") = 0
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt2") = 0
                            Next
                        Else
                            Throw New Exception("Printing Support only 3 Diffent Rates")
                        End If

                    End If
                Next
            Next
        End If
        Return dtAfterModify
    End Function

    'Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    strPrintType = "Excise"
    '    Print(True)
    'End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        btnSave.Visible = True
    '        btnDelete.Visible = True
    '        btnPost.Visible = True

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "SCRAP-SALE"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
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
    '            btnSave.Visible = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Visible = False
    '        End If
    '        If strTemp(3) = "0" Then 'Grant Authorize access
    '            btnPost.Visible = False
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    'Private Sub gv2_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
    '    Try

    '        If (Not isInsideLoadData) Then
    '            If Not isCellValueChangedTaxOpen Then
    '                isCellValueChangedTaxOpen = True
    '                If (e.Column Is (gv2.Columns(colTTaxAmt)) AndAlso rbtnTaxCalManual.IsChecked) Then
    '                    For ii As Integer = 0 To gv1.Rows.Count - 1
    '                        UpdateCurrentRow(ii)
    '                    Next
    '                    UpdateAllTotals()
    '                End If
    '                isCellValueChangedTaxOpen = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    'Private Sub rbtnTaxCalAutomatic_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnTaxCalAutomatic.ToggleStateChanged
    '    If Not isInsideLoadData Then
    '        If rbtnTaxCalAutomatic.IsChecked Then
    '            SetTaxDetails()
    '        ElseIf rbtnTaxCalManual.IsChecked Then
    '            For intRowNo As Integer = 0 To gv2.Rows.Count - 1
    '                gv2.Rows(intRowNo).Cells(colTTaxRate).Value = Nothing
    '                gv2.Rows(intRowNo).Cells(colTTaxAmt).Value = Nothing
    '                gv2.Rows(intRowNo).Cells(colTBaseAmt).Value = Nothing
    '            Next
    '            For intRowNo As Integer = 0 To gv1.Rows.Count - 1
    '                For ii As Integer = 1 To 10
    '                    Dim strII As String = clsCommon.myCstr(ii)
    '                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
    '                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
    '                Next
    '            Next
    '        End If
    '    End If
    'End Sub

    Private Sub gv2_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv2.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv2.Columns(colTTaxAmt)) Then
                    gv2.CurrentRow.Cells(colTTaxAmt).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                End If

                Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                cell.GradientStyle = GradientStyles.Solid
                cell.BackColor = Color.FromArgb(243, 181, 51)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    'Private Sub chkExcisable_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkExcisable.ToggleStateChanged
    '    ExcisableTaxGroup()
    'End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then

            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub rbtnTaxCalManual_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnTaxCalManual.ToggleStateChanged, rbtnTaxCalAutomatic.ToggleStateChanged
        If Not isInsideLoadData Then
            If rbtnTaxCalAutomatic.IsChecked Then
                SetTaxDetails()
            ElseIf rbtnTaxCalManual.IsChecked Then
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


    Private Sub TxtFinder1__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtnrg._MYValidating
        Dim qry As String = "Select RGP_No as [Code], RGP_Date as [NRGP Date] from TSPL_RGP_HEAD "
        txtnrg.Value = clsCommon.ShowSelectForm("NRGP_FND", qry, "Code", "RGP_No NOT IN (Select Distinct NRG_No from TSPL_SCRAPSALE_HEAD) AND Status=1 and Doc_Type='NRGP' AND Against_Sale=1", txtnrg.Value, "Code", isButtonClicked)
        FillNRGPDetails(txtnrg.Value)
    End Sub

    Private Sub FillNRGPDetails(ByVal nrgpNo As String)
        Try
            isInsideLoadData = True
            LoadBlankGrid()
            If clsCommon.myLen(nrgpNo) > 0 Then
                Dim qry As String = "Select TSPL_RGP_HEAD.Location, TSPL_LOCATION_MASTER.Location_Desc, (Select MAX(TSPL_VEHICLE_MASTER.Vehicle_Id) FRom TSPL_VEHICLE_MASTER WHERE TSPL_VEHICLE_MASTER.Number=TSPL_RGP_HEAD.VehicleNo) As Vehicle_Id, (Select MAX(TSPL_VEHICLE_MASTER.Description) FRom TSPL_VEHICLE_MASTER WHERE TSPL_VEHICLE_MASTER.Number=TSPL_RGP_HEAD.VehicleNo) As Description, Item_Code, "
                qry += " TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name, Reason, Remarks, Item_Desc, Unit_code, RGP_Qty, Item_Cost, Amount from "
                qry += " TSPL_RGP_HEAD LEFT OUTER JOIN TSPL_RGP_DETAIL ON TSPL_RGP_DETAIL.RGP_No=TSPL_RGP_HEAD.RGP_No"
                qry += " LEFT OUTER JOIN TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_RGP_HEAD.Location"
                qry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RGP_HEAD.Vendor_Code"
                qry += " WHERE TSPL_RGP_HEAD.RGP_No='" + nrgpNo + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim LineNo As Integer = 0
                For Each dr As DataRow In dt.Rows
                    LineNo += 1
                    If LineNo = 1 Then
                        fndcustNo.Value = clsCommon.myCstr(dr("Cust_Code"))
                        txtcustdesc.Text = clsCommon.myCstr(dr("Customer_Name"))
                        fndLocation.Value = clsCommon.myCstr(dr("Location"))
                        txtlocation.Text = clsCommon.myCstr(dr("Location_Desc"))
                        'txtdescription.Text = clsCommon.myCstr(dr("Reason"))
                        txtref.Text = clsCommon.myCstr(dr("Remarks"))
                        TxtVehicleCode.Value = clsCommon.myCstr(dr("Vehicle_Id"))
                        txtVehicleDesc.Text = clsCommon.myCstr(dr("Description"))
                    End If
                    gv1.Rows.AddNew()
                    gv1.CurrentRow.Cells(colRowType).Value = clsItemRowType.RowTypeItem
                    gv1.CurrentRow.Cells(colLineNo).Value = LineNo
                    gv1.CurrentRow.Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.CurrentRow.Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dr("Item_Code")), Nothing)
                    gv1.CurrentRow.Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_code"))
                    gv1.CurrentRow.Cells(colQty).Value = clsCommon.myCdbl(dr("RGP_Qty"))
                    gv1.CurrentRow.Cells(colprice).Value = clsCommon.myCdbl(dr("Item_Cost"))
                    gv1.CurrentRow.Cells(colAmt).Value = clsCommon.myCdbl(dr("Amount"))
                Next
                qry = "Select Vendor_code,vendor_name from TSPL_RGP_HEAD  Where RGP_No='" + nrgpNo + "'"
                Dim Dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                If (Dt1.Rows.Count > 0) Then
                    Dim dr1 As DataRow = Dt1.Rows(0)
                    fndcustNo.Value = dr1("Vendor_code")
                    txtcustdesc.Text = dr1("vendor_name")
                End If
            Else
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub


    Private Sub btnPrePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strPrintType = "Tax"
        Print(False, False, False)
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Document Number not found to do this operation")
            End If
            If clsCommon.myLen(lblInvoiceNo.Text) <= 0 Then
                Throw New Exception("Invoice Number not found to do this operation")
            End If
            Dim Reason As String = ""
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then


                '' REASON FOR Reverse 
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If

                If ClsScrapInvoiceHead.ReverseAndUnpost(txtDocNo.Value, lblInvoiceNo.Text) Then
                    saveCancelLog(Reason, "Reverse And Recreate", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
        Dim qry As String = "Select '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "' as [Date], '' as [Customer], '' as [Location], '' as [Vehicle]," &
                            " '' as [Item], '' as [UOM], 0 as [Shipped Qty], 0 as [Unit Cost], '' as [Specification]"
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub rmiImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImport.Click
        funImport()
    End Sub

    Public Sub funImport()

        Dim gv As New RadGridView()

        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Date", "Customer", "Location", "Vehicle", "Item", "UOM", "Shipped Qty", "Unit Cost", "Specification") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                clsCommon.ProgressBarShow()
                Dim HeadCustCode As String = ""
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim strCusCode As String = clsCommon.myCstr(grow.Cells("Customer").Value)
                    Dim shipment_No As String = ""
                    If clsCommon.myLen(strCusCode) > 0 Then
                        If Not clsCommon.CompairString(HeadCustCode, strCusCode) = CompairStringResult.Equal Then
                            HeadCustCode = strCusCode
                            Dim coll As New Hashtable()
                            Dim cust_Code As String = clsDBFuncationality.getSingleValue("Select Cust_Code  from TSPL_CUSTOMER_MASTER Where Cust_Code='" + strCusCode + "'", trans)
                            If Not clsCommon.CompairString(cust_Code, strCusCode) = CompairStringResult.Equal Then
                                Throw New Exception("The Customer '" + strCusCode + "' at line " + LineNo + " Does Not Exist In Customer Master")
                                trans.Rollback()
                            End If

                            Dim strLocation As String = clsCommon.myCstr(grow.Cells("Location").Value)
                            Dim Loc_Code As String
                            If clsCommon.myLen(strLocation) > 0 Then
                                Loc_Code = clsDBFuncationality.getSingleValue("Select Location_Code from TSPL_LOCATION_MASTER Where Location_Code='" + strLocation + "'", trans)
                                If Not clsCommon.CompairString(Loc_Code, strLocation) = CompairStringResult.Equal Then
                                    Throw New Exception("The Location '" + strLocation + "' at line " + LineNo + " Does Not Exist In Location Master")
                                    trans.Rollback()
                                    Exit Sub
                                End If
                            Else
                                Throw New Exception("Please enter location at line " + LineNo + "")
                            End If
                            Dim strVehicleId As String = clsCommon.myCstr(grow.Cells("Vehicle").Value)
                            Dim Vehicle_Id As String
                            If clsCommon.myLen(strVehicleId) > 0 Then
                                Vehicle_Id = clsDBFuncationality.getSingleValue("Select Vehicle_Id from TSPL_VEHICLE_MASTER  WHERE Vehicle_Id='" + strVehicleId + "'", trans)
                                If Not clsCommon.CompairString(Vehicle_Id, strVehicleId) = CompairStringResult.Equal Then
                                    trans.Rollback()
                                    Exit Sub
                                End If
                            End If
                            Dim docDate As String = clsCommon.myCstr(grow.Cells("Date").Value)
                            If clsCommon.myLen(docDate) <= 0 Then
                                Throw New Exception("Please enter Date at line " + LineNo + "")
                            End If

                            shipment_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(docDate, "dd/MMM/yyyy  hh:mm tt"), clsDocType.Scrap, "", Loc_Code)

                            clsCommon.AddColumnsForChange(coll, "shipment_No", shipment_No)
                            clsCommon.AddColumnsForChange(coll, "invoice_No", "")
                            clsCommon.AddColumnsForChange(coll, "Status", 0)
                            clsCommon.AddColumnsForChange(coll, "Po_No", "")
                            clsCommon.AddColumnsForChange(coll, "cust_Code", cust_Code)
                            Dim cust_Name As String = clsDBFuncationality.getSingleValue("Select Customer_Name  from TSPL_CUSTOMER_INFO Where Cust_Code='" + strCusCode + "'", trans)
                            clsCommon.AddColumnsForChange(coll, "cust_Name", cust_Name)
                            clsCommon.AddColumnsForChange(coll, "shipment_Date", clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy hh:mm tt"))
                            clsCommon.AddColumnsForChange(coll, "posting_Date", clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy hh:mm tt"))
                            clsCommon.AddColumnsForChange(coll, "expship_Date", clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy hh:mm tt"))
                            clsCommon.AddColumnsForChange(coll, "Loc_Code", Loc_Code)
                            clsCommon.AddColumnsForChange(coll, "Loc_Name", clsLocation.GetName(Loc_Code, trans))
                            clsCommon.AddColumnsForChange(coll, "ToLoc_Code", "")
                            clsCommon.AddColumnsForChange(coll, "CreateInvoice", 0)
                            clsCommon.AddColumnsForChange(coll, "Description", "")
                            clsCommon.AddColumnsForChange(coll, "Reff", "")
                            clsCommon.AddColumnsForChange(coll, "Tax_Group", "")
                            clsCommon.AddColumnsForChange(coll, "Tax_Desc", "")
                            For ii As Integer = 1 To 10
                                clsCommon.AddColumnsForChange(coll, "Tax" + clsCommon.myCstr(ii) + "", "")
                                clsCommon.AddColumnsForChange(coll, "Tax" + clsCommon.myCstr(ii) + "_Rate", 0)
                                clsCommon.AddColumnsForChange(coll, "Tax" + clsCommon.myCstr(ii) + "_Amt", 0)
                                clsCommon.AddColumnsForChange(coll, "Tax" + clsCommon.myCstr(ii) + "_Base_Amt", 0)

                                clsCommon.AddColumnsForChange(coll, "AddCode" + clsCommon.myCstr(ii) + "", "")
                                clsCommon.AddColumnsForChange(coll, "AddDesc" + clsCommon.myCstr(ii) + "", "")
                                clsCommon.AddColumnsForChange(coll, "AddAmt" + clsCommon.myCstr(ii) + "", 0)
                            Next
                            clsCommon.AddColumnsForChange(coll, "Addcost", 0)
                            clsCommon.AddColumnsForChange(coll, "AddcostDesc", "")
                            clsCommon.AddColumnsForChange(coll, "Add_Amt", 0)
                            clsCommon.AddColumnsForChange(coll, "Before_Add_Amt", 0)
                            clsCommon.AddColumnsForChange(coll, "After_Add_Amt", 0)
                            clsCommon.AddColumnsForChange(coll, "Discount_Base", 0)
                            clsCommon.AddColumnsForChange(coll, "Discount_Amt", 0)
                            clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", 0)
                            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", 0)
                            clsCommon.AddColumnsForChange(coll, "ship_Total_Amt", 0)
                            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(docDate, "dd/MM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(docDate, "dd/MM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "ispost", 0)
                            clsCommon.AddColumnsForChange(coll, "Doc_Amt", 0)
                            clsCommon.AddColumnsForChange(coll, "Terms_Code", "")
                            clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(docDate, "dd/MM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "NRG_No", "")
                            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", 0)
                            clsCommon.AddColumnsForChange(coll, "Excisable", "N")
                            clsCommon.AddColumnsForChange(coll, "Inter_Branch", 0)
                            clsCommon.AddColumnsForChange(coll, "Vehicle_Id", "")
                            clsCommon.AddColumnsForChange(coll, "is_Asset_Type", 0)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPSALE_HEAD", OMInsertOrUpdate.Insert, "", trans)
                        End If
                        Dim coll2 As New Hashtable()
                        clsCommon.AddColumnsForChange(coll2, "shipment_No", shipment_No)
                        clsCommon.AddColumnsForChange(coll2, "Line_No", LineNo - 1)
                        Dim strItemCode As String = clsCommon.myCstr(grow.Cells("Item").Value)
                        Dim Item_Code As String
                        If clsCommon.myLen(strItemCode) > 0 Then
                            Item_Code = clsDBFuncationality.getSingleValue("Select Item_Code from TSPL_ITEM_MASTER Where Item_Code='" + strItemCode + "'", trans)
                            If Not clsCommon.CompairString(Item_Code, strItemCode) = CompairStringResult.Equal Then
                                Throw New Exception("The Item '" + strItemCode + "' at line " + LineNo + " Does Not Exist In Item Master")
                                trans.Rollback()
                                Exit Sub
                            End If
                        Else
                            Throw New Exception("Please enter Item at line " + LineNo + "")
                        End If
                        clsCommon.AddColumnsForChange(coll2, "Item_Code", Item_Code)
                        clsCommon.AddColumnsForChange(coll2, "Item_Desc", clsItemMaster.GetItemName(Item_Code, trans))
                        Dim Unit_Code As String
                        Dim strUnitCode As String = clsCommon.myCstr(grow.Cells("UOM").Value)
                        If clsCommon.myLen(strUnitCode) > 0 Then
                            Unit_Code = clsDBFuncationality.getSingleValue("Select UOM_Code from TSPL_ITEM_UOM_DETAIL Where Item_Code='" + strItemCode + "' AND UOM_Code='" + strUnitCode + "'", trans)
                            If Not clsCommon.CompairString(Unit_Code, strUnitCode) = CompairStringResult.Equal Then
                                Throw New Exception("The UOM '" + strUnitCode + "' at line " + LineNo + " Does Not Exist In UOM Master")
                                trans.Rollback()
                                Exit Sub
                            End If
                        Else
                            Throw New Exception("Please enter UOM at line " + LineNo + "")
                        End If
                        clsCommon.AddColumnsForChange(coll2, "Unit_Code", Unit_Code)
                        Dim shipped_Qty As Double = clsCommon.myCdbl(grow.Cells("Shipped Qty").Value)
                        If shipped_Qty <= 0 Then
                            Throw New Exception("Please enter Shipped Qty at line " + LineNo + "")
                        Else
                            clsCommon.AddColumnsForChange(coll2, "shipped_Qty", shipped_Qty)
                        End If
                        Dim price As Double = clsCommon.myCdbl(grow.Cells("Unit Cost").Value)
                        If price <= 0 Then
                            Throw New Exception("Please enter Unit Cost at line " + LineNo + "")
                        Else
                            clsCommon.AddColumnsForChange(coll2, "price", price)
                        End If
                        clsCommon.AddColumnsForChange(coll2, "DiscountPer", 0)
                        clsCommon.AddColumnsForChange(coll2, "DiscountAmt", 0)
                        clsCommon.AddColumnsForChange(coll2, "Tax", 0)
                        clsCommon.AddColumnsForChange(coll2, "NetPriceAmt", 0)
                        clsCommon.AddColumnsForChange(coll2, "ItemAmt", shipped_Qty * price)
                        clsCommon.AddColumnsForChange(coll2, "TotalDiscountAmt", 0)
                        clsCommon.AddColumnsForChange(coll2, "TotalTaxAmt", 0)
                        clsCommon.AddColumnsForChange(coll2, "ItemNetAmt", shipped_Qty * price)
                        clsCommon.AddColumnsForChange(coll2, "TotalAmt", shipped_Qty * price)
                        For ii As Integer = 1 To 10
                            clsCommon.AddColumnsForChange(coll2, "Tax" + clsCommon.myCstr(ii) + "", "")
                            clsCommon.AddColumnsForChange(coll2, "Tax" + clsCommon.myCstr(ii) + "_Rate", 0)
                            clsCommon.AddColumnsForChange(coll2, "Tax" + clsCommon.myCstr(ii) + "_Amt", 0)
                            clsCommon.AddColumnsForChange(coll2, "Tax" + clsCommon.myCstr(ii) + "_Base_Amt", 0)
                        Next
                        clsCommon.AddColumnsForChange(coll2, "pending_qty", 0)
                        Dim Specification As String = clsCommon.myCstr(grow.Cells("Specification").Value)
                        If clsCommon.myLen(Specification) > 100 Then
                            Throw New Exception("Length of Specification can not be greater than 100 at line " + LineNo + "")
                        End If
                        clsCommon.AddColumnsForChange(coll2, "Specification", Specification)
                        clsCommon.AddColumnsForChange(coll2, "Asset_Code", Nothing, True)
                        clsCommonFunctionality.UpdateDataTable(coll2, "TSPL_SCRAPSALE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub


    Private Sub txtTransporter_Code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtTransporter_Code._MYValidating
        Dim Qry As String = "select Transport_Id as Code,Transporter_Name as Description,City,State,Pincode,Phone from TSPL_TRANSPORT_MASTER"
        txtTransporter_Code.Value = clsCommon.ShowSelectForm("TRANSPORTER_Transfer_KDIL", Qry, "Code", "", txtTransporter_Code.Value, "Code", isButtonClicked)
        txtTransporter_desc.Text = clsTransferDCC.GetTransporterName(txtTransporter_Code.Value)
    End Sub

    Private Sub gv1_Click(sender As Object, e As EventArgs) Handles gv1.Click
        If gv1.CurrentRow IsNot Nothing Then
            setBalance()
        End If
    End Sub

    Private Sub gv1_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 Then
            setBalance()
        End If
    End Sub

    Private Sub gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F5 Then
            '======update by preeti gupta 17/10/2018
            If RunBatchFifowise = 0 Then
                OpenBatchItem()
            Else
                OpenBatchItemIfFIFIOSettingON()
            End If
        End If
    End Sub
    '============created by preeti gupta===============
    Public Sub OpenBatchItemIfFIFIOSettingON()
        Dim arr As List(Of clsBatchInventory) = Nothing
        Dim strBatchunion As String = ""
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
        End If
        If Not arr Is Nothing Then
            If arr.Count > 0 Then
                For Each obj As clsBatchInventory In arr
                    strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                Next
                clsCommon.MyMessageBoxShow(Me, strBatchunion, Me.Text)
            End If
        End If
    End Sub
    ' done by priti BHA/12/07/18-000148 to apply batch wise fifo functionality
    Sub OpenBatchItem()
        If clsCommon.myCBool(clsDBFuncationality.getSingleValue("select TSPL_ITEM_MASTER.Is_Batch_Item  from TSPL_ITEM_MASTER where TSPL_ITEM_MASTER.Item_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "'", Nothing)) Then
            Dim frm As frmBatchItemOut = New frmBatchItemOut()
            frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
            frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
            frm.strLocationCode = fndLocation.Value
            frm.strCurrDocNo = txtDocNo.Value
            frm.strCurrDocType = "ScrapIn"
            frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
            frm.dblMRP = 0
            frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
            If RunBatchFifowise = 0 Then
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                End If
            Else
                frm.OpenSerialList(0, "")
                gv1.CurrentRow.Cells(colICode).Tag = frm.arr
            End If
        End If
    End Sub
    Private Sub dtpshipment_Validating(sender As Object, e As CancelEventArgs) Handles dtpshipment.Validating
        SETGSTControl()
        If clsCommon.myCDate(dtpshipment.Value).Date() > clsCommon.GETSERVERDATE().Date() Then
            clsCommon.MyMessageBoxShow(Me, "Cannot allow future date -  " & clsCommon.myCDate(dtpshipment.Value).Date())
            e.Cancel = True
        End If
    End Sub
    Sub SETGSTControl()
        GSTStatus = clsERPFuncationality.GetGSTStatus(dtpshipment.Value)
        If (GSTStatus AndAlso chkTaxable.Checked = True) OrElse (GSTStatus = False) Then
            txtTaxGroup.Enabled = True
        Else
            txtTaxGroup.Enabled = False
        End If
        If GSTStatus Then
            ddlInvoiceType.Enabled = False
        Else
            ddlInvoiceType.Enabled = True
        End If
    End Sub
    Private Sub chkTaxable_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkTaxable.ToggleStateChanged
        SETGSTControl()
        If clsCommon.myLen(txtDocNo.Value) = 0 Then
            SetTax()
        End If
    End Sub

    Private Sub btnInvoiceJE_Click(sender As Object, e As EventArgs) Handles btnInvoiceJE.Click
        clsOpenJEAgainstInvoice.ShowInvoiceJEForMiscSale(lblInvoiceNo.Text)
    End Sub
    ' Ticket No : TEC/29/10/18-000354 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(lblInvoiceNo.Text)
    End Sub

    Private Sub txtGWeighmentNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGWeighmentNo._MYValidating
        Try
            Dim qry As String = "select * from (select Weighment_No,Weighment_Date,Location_Code,Vehicle_No_Manual,Gross_Weight,Tare_Weight,Net_Weight,Item_Code from TSPL_GENERAL_WEIGHMENT_DETAIL where" + Environment.NewLine +
              "Posted=1 and is_scrap=1 and not exists(select 1 from TSPL_SCRAPSALE_HEAD where TSPL_SCRAPSALE_HEAD.Weighment_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No AND TSPL_SCRAPSALE_HEAD.shipment_No not in ('" + txtDocNo.Value + "')))x "
            txtGWeighmentNo.Value = clsCommon.ShowSelectForm("sc@gwe", qry, "Weighment_No", "", txtGWeighmentNo.Value, "Weighment_No", isButtonClicked)
            Dim obj As ClsGeneralWeighment = ClsGeneralWeighment.GetData(txtGWeighmentNo.Value, NavigatorType.Current)
            LoadBlankGrid()
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Weighment_No) > 0 Then
                fndLocation.Value = obj.Location_Code
                txtlocation.Text = clsLocation.GetName(obj.Location_Code, Nothing)
                gv1.Rows.AddNew()
                gv1.Rows(gv1.RowCount - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                gv1.Rows(gv1.RowCount - 1).Cells(colICode).Value = obj.Item_Code
                qry = "select Item_Code,Item_Desc,Unit_Code,cost,HSN_Code,IsTaxable from TSPL_ITEM_MASTER where Item_Code='" + obj.Item_Code + "' "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv1.Rows(gv1.RowCount - 1).Cells(colIName).Value = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                    gv1.Rows(gv1.RowCount - 1).Cells(colUnit).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                    gv1.Rows(gv1.RowCount - 1).Cells(colprice).Value = clsCommon.myCdbl(dt.Rows(0)("cost"))
                    gv1.Rows(gv1.RowCount - 1).Cells(colHSNNo).Value = clsCommon.myCstr(dt.Rows(0)("HSN_Code"))
                    Dim convFat As Decimal = clsWeightConversionInfo.GetWeightConverionFactor(clsCommon.myCstr(gv1.Rows(gv1.RowCount - 1).Cells(colICode).Value), "KG", clsCommon.myCstr(gv1.Rows(gv1.RowCount - 1).Cells(colUnit).Value), Nothing)
                    gv1.Rows(gv1.RowCount - 1).Cells(colQty).Value = Math.Round(obj.Net_Weight * convFat, 3, MidpointRounding.AwayFromZero)
                    chkTaxable.Checked = (clsCommon.myCdbl(dt.Rows(0)("IsTaxable")) = 1)
                    If PickCostOFMaterialSaleFromPriceMaster = True Then
                        Dim dblRate As Double = GetRateMaterialSale(fndLocation.Value, fndcustNo.Value, obj.Item_Code, clsCommon.myCstr(dt.Rows(0)("Unit_Code")), clsCommon.myCDate(dtpshipment.Value))
                        If dblRate > 0 Then
                            gv1.Rows(gv1.RowCount - 1).Cells(colprice).Value = dblRate
                            gv1.Rows(gv1.RowCount - 1).Cells(colprice).ReadOnly = True
                        Else
                            Throw New Exception("please create item selling price for item " & obj.Item_Code & "")
                        End If
                    Else
                        gv1.Rows(gv1.RowCount - 1).Cells(colprice).ReadOnly = False
                    End If
                    SetitemWiseTaxSetting(True, True)
                    UpdateCurrentRow(gv1.RowCount - 1)
                    UpdateAllTotals()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CancelData()
    End Sub

    Function CancelData() As Boolean
        Try
            If clsCommon.myLen(lblInvoiceNo.Text) <= 0 Then
                Throw New Exception("Code is empty")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure to Cancel the Record?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Return False
            End If
            Dim strSaleReturnNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_No from TSPL_SCRAPSALE_HEAD_RETURN where Invoice_No='" & lblInvoiceNo.Text & "' "))
            If clsCommon.myLen(strSaleReturnNo) > 0 Then
                Throw New Exception("You cannot cancelled this document because its Sale Return (" + clsCommon.myCstr(strSaleReturnNo) + ") has been created.")
            End If

            Dim strReceiptCount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select receipt_no from TSPL_RECEIPT_DETAIL where Document_No in (Select Document_No from TSPL_Customer_Invoice_Head  where AgainstScrap='" & lblInvoiceNo.Text & "') "))
            If clsCommon.myLen(strReceiptCount) > 0 Then
                Throw New Exception("You cannot cancelled this document because receiving (" + clsCommon.myCstr(strReceiptCount) + ") has been done against its AR Invoice.")
            End If

            If chkTaxable.Checked = True AndAlso clsERPFuncationality.GetEInvoiceStatus(dtpshipment.Value) = True AndAlso clsCommon.CompairString(EInvoiceType, "BB") = CompairStringResult.Equal Then
                Dim EInvoiceCancelTimeValid As Int64 = 0
                EInvoiceCancelTimeValid = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select  isnull (DATEDIFF(hour,EInvoice_Posting_Date,GETDATE()),0) as PostedHours from TSPL_SCRAPINVOICE_HEAD where  invoice_No = '" + lblInvoiceNo.Text + "'"))
                If EInvoiceCancelTimeValid >= 24 Then
                    Throw New Exception("Invoice can not be cancelled.It has been more than 24 hours.")
                End If
            End If

            ClsScrapSaleHead.CancelData(Me.Form_ID, txtDocNo.Value, lblInvoiceNo.Text, NavigatorType.Current)
            clsCommon.MyMessageBoxShow(Me, "Successfully Cancelled", Me.Text)
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function

    Private Sub txttcstaxbaseamount_TextChanged(sender As Object, e As EventArgs) Handles txttcstaxbaseamount.TextChanged
        Try
            If AllowtoChangeTCSBaseAmount Then
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

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, "shipment_No", "TSPL_SCRAPSALE_HEAD", "TSPL_SCRAPSALE_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub UpdateEwaybillNo()
        Try

            Dim obj As New ClsScrapSaleHead
            obj.shipment_No = clsCommon.myCstr(txtDocNo.Value)
            obj.EWayBillNo = txtEWayBillNo.Text
            obj.EWayBillDate = txtEWayBillDate.Value
            obj.EwayBillValidDate = txtEwayValidDate.Value
            obj.EwayBillRemarks = txtEWayBillRemarks.Text

            ' End If
            '  obj.Electronic_Ref_No = txtElectronicRefNo.Text
            ClsScrapSaleHead.UpdateAfterPosting(obj, txtDocNo.Value, Nothing)
            clsCommon.MyMessageBoxShow(Me, "E-Waybill updated successfully.", Me.Text)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub UpdateEInvoice()
        Try

            Dim obj As New ClsScrapSaleHead
            obj.shipment_No = clsCommon.myCstr(txtDocNo.Value)
            obj.EInvoiceIRNNo = EInvoiceIRNNo.Text
            obj.EInvoiceAckNo = EinvoiceAckNo.Text
            obj.EInvoiceAckDate = txtAckDate.Value
            obj.EInvoiceQRCode = EInvoiceQrCode.Text
            ClsScrapSaleHead.UpdateEInvoiceAfterPosting(obj, txtDocNo.Value, Nothing)
            clsCommon.MyMessageBoxShow(Me, "E-Invoice Updated Successfully", Me.Text)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnEWaybillUpdate_Click(sender As Object, e As EventArgs) Handles btnEWaybillUpdate.Click
        UpdateEwaybillNo()
    End Sub

    Private Sub EinvoiceBtnUpdate_Click(sender As Object, e As EventArgs) Handles EinvoiceBtnUpdate.Click
        UpdateEInvoice()
    End Sub

    Private Sub chkTaxable_CheckStateChanged(sender As Object, e As EventArgs) Handles chkTaxable.CheckStateChanged
        Try
            If chkTaxable.Checked = True Then
                RadPageView1.Pages("RadPageViewPage5").Item.Visibility = ElementVisibility.Visible
            Else
                RadPageView1.Pages("RadPageViewPage5").Item.Visibility = ElementVisibility.Collapsed
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        strPrintType = "Excise"
        Print(True, True, False)

    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        strPrintType = "Excise"
        Print(True, False, False)

    End Sub
End Class

