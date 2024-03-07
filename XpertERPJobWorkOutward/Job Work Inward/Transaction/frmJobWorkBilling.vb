'' work done agaist ticket no.BHA/21/08/18-000468
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports common
Imports System.IO
Imports XpertERPEngine


Public Class frmJobWorkBillig
    Inherits FrmMainTranScreen

#Region "Variables"
    Const colItemwiseTaxCode As String = "colItemwiseTaxCode"
    Dim CalculateTaxRatefromItemwsieTaxOnSale As Integer = 0
    Const ReportID As String = "ScrapSaleGrid"
    Private isCellValueChangedOpenAdd As Boolean = False
    Public strShipmentno As String = Nothing
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Dim RunBatchFifowise As Integer = 0
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedTaxOpen As Boolean = False
    Dim CreatVatSeriesOnExciseInvoice As Integer = 0
    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const COLHSNNo As String = "COLHSNNo"
    Const colQty As String = "shippedQty"
    Const colConvKgQty As String = "colConvKgQty"
    Const colFAT As String = "colFAT"
    Const colSNF As String = "colSNF"
    Const colprice As String = "price"
    Const colPriceCode As String = "colPriceCode"
    Const colConvKgPrice As String = "colConvKgPrice"
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

    Const colJWEstimateNo As String = "colJWEstimateNo"
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
    Dim settJWIRateofFGasPerRM As Boolean = False ''ERO/01/04/19-000535 by balwinder on 03/04/2019
    Dim EInvoiceType As String = ""
#End Region
    'richa 20200616
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.JobWorkDispatch)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnCancel.Visible = MyBase.isCancel_Flag_After_Posting
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        btnReverse.Visible = False
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        settJWIRateofFGasPerRM = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.JWIRateofFGasPerRM, clsFixedParameterCode.JWIRateofFGasPerRM, Nothing)) = 1)

        CalculateTaxRatefromItemwsieTaxOnSale = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateTaxRatefromItemwsieTaxOnSale, clsFixedParameterCode.CalculateTaxRatefromItemwsieTaxOnSale, Nothing))
        SetUserMgmtNew()
        fndcustNo.MendatroryField = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A Create Additional Cost")
        GrossWtfromItemMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GrossWtFromItemMasterONCSATransfer, clsFixedParameterCode.GrossWtFromItemMasterONCSATransfer, Nothing)) = 1, True, False)
        RadPageView1.SelectedPage = RadPageViewPage1

        LoadBlankGrid()
        LoadBlankGridTax()

        AddNew()
        If clsCommon.myLen(strShipmentno) > 0 Then
            LoadData(strShipmentno, NavigatorType.Current)
        End If


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

        txtTermCode.Enabled = clsPurchaseOrderHead.GetInventorySetting().Rows(0).Item("IsTermsEditableOnPurchase")

        AllowChangeInvoiceType = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Allow_Change_InvoiceType from TSPL_inv_parameters")) = 0, False, True)
        isALlowVehicleGateOutValidation = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowVehicleGateOutValidationScrapSale, clsFixedParameterCode.AllowVehicleGateOutValidationScrapSale, Nothing)) = "1", True, False)
        RunBatchFifowise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBatchFifowise, clsFixedParameterCode.RunBatchFifowise, Nothing))

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtcustdesc.MaxLength = 50
        txtlocation.MaxLength = 50
        txtdescription.MaxLength = 200
        txtref.MaxLength = 200    
    End Sub

    Sub BlankAllControls()
        EInvoiceType = ""
        btnOk.Enabled = True
        TxtRoundoff.Text = 0
        lblDocAmount.Text = 0
        txtDocNo.Value = ""
        fndcustNo.Value = ""
        'txtinvoice.Text = ""
        fndcustNo.Value = ""
        txtcustdesc.Text = ""

        txtDocDate.Value = clsCommon.GETSERVERDATE()
        txtFromdate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()

        txtdescription.Text = ""
        txtref.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        fndLocation.Value = ""
        TxtBillingLocation.Value = ""
        lblBillingLocation.Text = ""
        lblAmtWithDiscount.Text = ""
        lblTaxAmt.Text = ""
        lblTotRAmt.Text = ""
        lbldocamt.Text = ""
        txtlocation.Text = ""
        UsLock1.Status = 0
        txtFromdate.Value = clsCommon.GETSERVERDATE()
        txtDocDate.Value = clsCommon.GETSERVERDATE()
        fndLocation.Enabled = True

        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
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



        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        'repoICode.HeaderImage = Global.XpertERPJobWorkOutward.My.Resources.Resources.new1
        'repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        '============Added by preeti gupta[10/09/2018][GKD/10/09/18-000156]
        Dim repoHSN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHSN.FormatString = ""
        repoHSN.HeaderText = "HSN"
        repoHSN.Name = COLHSNNo
        repoHSN.Width = 150
        repoHSN.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHSN)

        Dim repoJWEstNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoJWEstNo.FormatString = ""
        repoJWEstNo.HeaderText = "JW Estimate No"
        repoJWEstNo.Name = colJWEstimateNo
        repoJWEstNo.Width = 150
        repoJWEstNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoJWEstNo)

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
        shippedQty.HeaderText = "Invoice Quantity"
        shippedQty.Name = colQty
        shippedQty.IsVisible = True
        shippedQty.Minimum = 0
        shippedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        shippedQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(shippedQty)

        shippedQty = New GridViewDecimalColumn()
        shippedQty.FormatString = ""
        shippedQty.HeaderText = "Invoice Quantity(Kg)"
        shippedQty.Name = colConvKgQty
        shippedQty.IsVisible = True
        shippedQty.Minimum = 0
        shippedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        shippedQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(shippedQty)

        shippedQty = New GridViewDecimalColumn()
        shippedQty.FormatString = ""
        shippedQty.HeaderText = "FAT Kg"
        shippedQty.Name = colFAT
        shippedQty.IsVisible = True
        shippedQty.Minimum = 0
        shippedQty.Maximum = 100
        shippedQty.ShowUpDownButtons = False
        shippedQty.Step = 0
        shippedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        shippedQty.ReadOnly = True
        shippedQty.DecimalPlaces = 2
        gv1.MasterTemplate.Columns.Add(shippedQty)

        shippedQty = New GridViewDecimalColumn()
        shippedQty.FormatString = ""
        shippedQty.HeaderText = "SNF Kg"
        shippedQty.Name = colSNF
        shippedQty.IsVisible = True
        shippedQty.Minimum = 0
        shippedQty.Maximum = 100
        shippedQty.ShowUpDownButtons = False
        shippedQty.Step = 0
        shippedQty.DecimalPlaces = 2
        shippedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        shippedQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(shippedQty)


        Dim price As GridViewDecimalColumn = New GridViewDecimalColumn()
        price.WrapText = True
        price.HeaderText = "Unit Cost"
        price.Name = colprice
        price.Width = 80
        price.Minimum = 0
        price.ReadOnly = True
        price.FormatString = "{0:n2}"
        price.DecimalPlaces = 2
        price.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(price)

        Unit = New GridViewTextBoxColumn()
        Unit.FormatString = ""
        Unit.HeaderText = "JWI Price Code"
        Unit.Name = colPriceCode
        Unit.Width = 80
        Unit.ReadOnly = True
        Unit.IsVisible = False
        gv1.MasterTemplate.Columns.Add(Unit)

        Dim Convprice As GridViewDecimalColumn = New GridViewDecimalColumn()
        Convprice.WrapText = True
        Convprice.HeaderText = "Unit Cost(Kg)"
        Convprice.Name = colConvKgPrice
        Convprice.Width = 80
        Convprice.Minimum = 0
        Convprice.ReadOnly = True
        Convprice.FormatString = "{0:n2}"
        Convprice.DecimalPlaces = 2
        Convprice.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(Convprice)

        Dim ItemAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        ItemAmt.FormatString = ""
        ItemAmt.HeaderText = "Item Amt"
        ItemAmt.Name = colAmt
        ItemAmt.Width = 80
        ItemAmt.Minimum = 0
        ItemAmt.ReadOnly = True
        ItemAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(ItemAmt)


        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)

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

        Dim repoItemTaxCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemTaxCode = New GridViewTextBoxColumn()
        repoItemTaxCode.FormatString = ""
        repoItemTaxCode.HeaderText = "Item Tax Code"
        repoItemTaxCode.Name = colItemwiseTaxCode
        repoItemTaxCode.Width = 100
        repoItemTaxCode.IsVisible = False
        repoItemTaxCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemTaxCode)

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

        'Dim repoSP As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoSP = New GridViewTextBoxColumn()
        'repoSP.FormatString = ""
        'repoSP.HeaderText = "Specification"
        'repoSP.Name = colSp
        'repoSP.Width = 100
        ''repoSP.Minimum = 0
        'repoSP.ReadOnly = False
        'repoSP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.MasterTemplate.Columns.Add(repoSP)


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
        UcItemBalance1.TransDate = txtDocDate.Value
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
                    If gv1.CurrentColumn Is gv1.Columns(colICode) OrElse gv1.CurrentColumn Is gv1.Columns(colQty) OrElse gv1.CurrentColumn Is gv1.Columns(colprice) OrElse gv1.CurrentColumn Is gv1.Columns(colUnit) Then
                        If gv1.CurrentColumn Is gv1.Columns(colQty) Then
                            Dim stockqty As Double = 0
                            If clsCommon.myLen(fndLocation.Value) > 0 And clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                                'Dim str As String = "select sum(Item_Qty) from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "' and Location_Code='" + fndLocation.Value + "' "
                                Dim str As String = "select SUM(QTY) from TSPL_INVENTORY_MOVEMENT where Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "' and Location_Code='" + fndLocation.Value + "' "
                                stockqty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))

                            Else
                                common.clsCommon.MyMessageBoxShow(Me, "Select the Location", Me.Text)
                                gv1.CurrentRow.Cells(colQty).Value = 0
                            End If
                        End If
                        If gv1.CurrentColumn Is gv1.Columns(colQty) OrElse gv1.CurrentColumn Is gv1.Columns(colprice) Then
                            UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                            UpdateAllTotals()

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

   

    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        'If intCurrRow = gv1.Rows.Count - 1 Then
        '    gv1.Rows.AddNew()
        '    gv1.CurrentRow = gv1.Rows(intCurrRow)
        'End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colQty)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colprice)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colprice) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
        End If
    End Sub

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""

    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

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
                End If
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
            End If
        Next
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
        LoadBlankGridTax()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        gv1.Rows.AddNew()
        LoadInvoiceType()
        btnReverse.Visible = False
        btnInvoiceJE.Visible = False
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
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
        Try
            ' KUNAL > TICKET : BM00000009580 ========
            If AllowFutureDateTransaction(txtDocDate.Value, Nothing) = False Then
                txtDocDate.Focus()
                Return False
            End If

            If btnSave.Text = "Update" Then
                Dim strchk As String = "select status from TSPL_JOBWORK_BILLING_HEAD where Document_Code='" + txtDocNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    Throw New Exception("Transaction already posted")
                    Return False
                End If
            End If
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim IsTaxable As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where item_code='" & clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) & "'"))
                If IsTaxable = 1 And clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value) = 0 Then
                    SetitemWiseTaxSetting(True, True)
                End If
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()
            If clsCommon.myLen(fndcustNo.Value) <= 0 Then
                fndcustNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code from TSPL_LOCATION_MASTER left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_LOCATION_MASTER.Jobwork_Vendor= TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code  where Location_Code='" & fndLocation.Value & "'"))
                txtcustdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name  from TSPL_customer_master where Cust_Code='" & fndcustNo.Value & "'"))
                If clsCommon.myLen(fndcustNo.Value) <= 0 Then
                    Throw New Exception("Please Map Jobwork Vendor with Location in Location master and Map Vendor with Customer in customer vendor mapping.")
                    fndcustNo.Focus()
                    Return False
                End If
            End If

            Dim strcustomernumber As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_customer_master.Cust_Code from tspl_customer_master where tspl_customer_master.Status ='N' and tspl_customer_master.Cust_Code='" & fndcustNo.Value & "'"))
            If clsCommon.myLen(clsCommon.myCstr(strcustomernumber)) <= 0 Then
                Throw New Exception("Customer should be active")
                fndcustNo.Focus()
            End If

            If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                Throw New Exception("Please select Tax Group")
                txtTaxGroup.Focus()
                Return False
            End If
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                Throw New Exception("Please select  From Location")
                fndLocation.Focus()
                Return False
            End If
            If clsCommon.myLen(TxtBillingLocation.Value) <= 0 Then
                Throw New Exception("Please select  Billing Location")
                TxtBillingLocation.Focus()
            End If

            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Shipment No Not found to save")
                txtDocNo.Focus()
                Return False
            End If


            For i As Integer = 0 To gv1.Rows.Count - 1
                Dim qty As Decimal = clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value)
                If clsCommon.myLen(gv1.Rows(i).Cells("colICode").Value) > 0 And qty = 0 Then
                    Dim str As String = clsCommon.myCstr(gv1.Rows(i).Cells("colICode").Value)
                    Throw New Exception("Shipped qty can't be zero for Item '" + str + "'")
                    Return False
                End If
            Next
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
            If ddlInvoiceType.SelectedValue = "E" And count <> 0 Then
                Dim qry As String = "select 1 from TSPL_TAX_GROUP_MASTER where Tax_Group_Type='s' and Excisable='Y' and Tax_Group_Code='" + txtTaxGroup.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Tax Group should be of excise type")
                    Return False
                End If
            End If


            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)

                If clsCommon.myLen(strICode) > 0 Then
                    If clsCommon.myLen(strUOM) <= 0 Then
                        Throw New Exception("Please enter UOM of Item - " + strICode + ".At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        Return False
                    End If
                End If
            Next
            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

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

        Return dt
    End Function

    Function InvoiceType() As Boolean

        Dim dt As DataTable
        Dim strloc As String
        Dim qry As String

        ' strloc = fndLocation.Value
        strloc = TxtBillingLocation.Value
        qry = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State, " & _
          "TSPL_LOCATION_MASTER.Sales_Tax_Group as LocalTaxGroup,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Local_Tax_GroupName, " & _
          "TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as InterstateTaxGroup,TSPL_TAX_GROUP_MASTERIS.Tax_Group_Desc as Interstate_Tax_GroupName " & _
          "FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_TAX_GROUP_MASTER as TSPL_TAX_GROUP_MASTERIS on TSPL_TAX_GROUP_MASTERIS.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTERIS.Tax_Group_Type='S' " & _
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
            'strExcise = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'")) = "T", True, False)
            strExcise = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + TxtBillingLocation.Value + "'")) = "T", True, False)
            If strExcise = True AndAlso count <> 0 Then
                ddlInvoiceType.SelectedValue = "E"

            End If

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
                'strExcise = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'")) = "T", True, False)
                strExcise = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + TxtBillingLocation.Value + "'")) = "T", True, False)
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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If SaveData() Then
            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
        End If
    End Sub

    Function SaveData() As Boolean
        Try
            '' Anubhooti 13-Sep-2014 BM00000003735
            If FrmMainTranScreen.ValidateTransactionAccToFinYear("Job Work Dispatch", txtDocDate.Value) = False Then
                Return False
            End If
            ''
            If (AllowToSave()) Then
                If clsCommon.myLen(fndLocation.Value) > 0 Then
                    Dim LocSegmentCode As String = fndLocation.Value
                    Dim locDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + LocSegmentCode + "'")
                End If
                Dim obj As New ClsJobWorkBilling()
                obj.Document_Code = txtDocNo.Value
                obj.cust_Code = fndcustNo.Value
                obj.cust_Name = txtcustdesc.Text
                obj.Document_Date = txtDocDate.Value
                obj.To_Date = txtToDate.Value
                obj.From_Date = txtFromdate.Value
                obj.Loc_Code = fndLocation.Value
                obj.Loc_Name = txtlocation.Text
                obj.Invoice_Type = "T"
                obj.Is_Taxable = True
                obj.Description = txtdescription.Text
                obj.reff = txtref.Text
                obj.Tax_Group = txtTaxGroup.Value
                obj.Tax_Desc = lblTaxGrpName.Text
                obj.Billing_Loc_Code = TxtBillingLocation.Value
                obj.Billing_Loc_Name = lblBillingLocation.Text
                If Math.Round(clsCommon.myCdbl(lbldocamt.Text), 0) > clsCommon.myCdbl(lbldocamt.Text) Then
                    TxtRoundoff.Text = Math.Round(Math.Round(clsCommon.myCdbl(lbldocamt.Text), 0) - clsCommon.myCdbl(lbldocamt.Text), 2)
                    lbldocamt.Text = Math.Round(clsCommon.myCdbl(lbldocamt.Text), 0)
                    lblDocAmount.Text = Math.Round(clsCommon.myCdbl(lbldocamt.Text), 0)
                Else
                    TxtRoundoff.Text = Math.Round(Math.Round(clsCommon.myCdbl(lbldocamt.Text)) - clsCommon.myCdbl(lbldocamt.Text), 2)
                    lbldocamt.Text = Math.Round(clsCommon.myCdbl(lbldocamt.Text), 0)
                    lblDocAmount.Text = Math.Round(clsCommon.myCdbl(lbldocamt.Text), 0)
                End If
                obj.RoundOffAmount = clsCommon.myCdbl(TxtRoundoff.Text)
                obj.Amount = lblAmtWithDiscount.Text
                obj.Total_Tax_Amt = lblTaxAmt.Text
                obj.doc_Amt = lbldocamt.Text
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


                obj.Terms_Code = txtTermCode.Value

                obj.Arr = New List(Of ClsJobWorkBillingDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New ClsJobWorkBillingDetail()
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.JW_EstimationNo = clsCommon.myCstr(grow.Cells(colJWEstimateNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Invoice_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.FATKg = clsCommon.myCdbl(grow.Cells(colFAT).Value)
                    objTr.SNFKg = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                    objTr.price = clsCommon.myCdbl(grow.Cells(colprice).Value)
                    objTr.JWI_Price_Code = clsCommon.myCstr(grow.Cells(colPriceCode).Value)
                    objTr.ConvKG_Price = clsCommon.myCdbl(grow.Cells(colConvKgPrice).Value)
                    objTr.ConvKG_Qty = clsCommon.myCdbl(grow.Cells(colConvKgQty).Value)
                    objTr.ItemAmt = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.TotalTaxAmt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
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
                    'objTr.Specification = clsCommon.myCstr(grow.Cells(colSp).Value)
                    objTr.ItemwiseTaxCode = clsCommon.myCstr(grow.Cells(colItemwiseTaxCode).Value)
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                    Return False
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

                If (obj.SaveData(obj, "", isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Document_Code)
                    'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

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

            fndcustNo.Enabled = False
            fndLocation.Enabled = False

            'fndLocation.Enabled = False
            Dim obj As New ClsJobWorkBilling()
            obj = ClsJobWorkBilling.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                If obj.Status = 1 Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btnCancel.Enabled = True
                Else
                    btnCancel.Enabled = False
                End If
                'btnOk.Enabled = False
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_Code
                fndcustNo.Value = obj.cust_Code
                txtcustdesc.Text = obj.cust_Name
                txtDocDate.Value = clsCommon.myCDate(obj.Document_Date)
                txtFromdate.Value = obj.From_Date
                txtToDate.Value = obj.To_Date
                fndLocation.Value = obj.Loc_Code
                txtlocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
                TxtBillingLocation.Value = obj.Billing_Loc_Code
                lblBillingLocation.Text = obj.Billing_Loc_Name
                ddlInvoiceType.SelectedValue = "T"
                txtdescription.Text = obj.Description
                txtref.Text = obj.reff
                txtTaxGroup.Value = obj.Tax_Group
                lblTaxGrpName.Text = obj.Tax_Desc
                lblAmtWithDiscount.Text = obj.Amount
                lblTaxAmt.Text = obj.Total_Tax_Amt
                lbldocamt.Text = obj.doc_Amt
                txtTermCode.Value = obj.Terms_Code
                TxtRoundoff.Text = obj.RoundOffAmount
                lblDocAmount.Text = obj.doc_Amt
                EInvoiceType = clsERPFuncationality.GetCustomerEInvoiceTypeFromTransationTable("TSPL_JOBWORK_BILLING_HEAD", "Document_Code", obj.Document_Code, Nothing)
                If clsERPFuncationality.GetEInvoiceStatus(txtDocDate.Value) = True AndAlso clsCommon.CompairString(EInvoiceType, "BB") = CompairStringResult.Equal Then
                    btnReverse.Enabled = False
                    If obj.Status = ERPTransactionStatus.Approved Then
                        btnCancel.Enabled = True
                    ElseIf obj.Status = ERPTransactionStatus.Pending Then
                        btnCancel.Enabled = False
                    End If
                End If
                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForSale(obj.Tax_Group)
                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX1
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX1_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX1_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX1_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX1) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
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
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX2) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
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
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX3) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
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
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX4) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
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
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX5) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
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

                                Exit For
                            End If
                        Next
                    End If
                End If

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As ClsJobWorkBillingDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colJWEstimateNo).Value = objTr.JW_EstimationNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(COLHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Invoice_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT).Value = objTr.FATKg
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).Value = objTr.SNFKg
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colprice).Value = objTr.price
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCode).Value = objTr.JWI_Price_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.ItemAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colConvKgPrice).Value = objTr.ConvKG_Price
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colConvKgQty).Value = objTr.ConvKG_Qty
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

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = objTr.TotalTaxAmt
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colitemnetamt).Value = objTr.ItemAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(coltotamt).Value = objTr.TotalAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemwiseTaxCode).Value = objTr.ItemwiseTaxCode

                        If obj.Status = 0 Then
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
                    If obj.Status = ERPTransactionStatus.Pending Then
                        gv1.Rows.AddNew()
                        btnInvoiceJE.Visible = False
                    Else
                        btnInvoiceJE.Visible = True
                    End If

                End If
                'SetitemWiseTaxOnlySetting()
                ' ''RefreshReqNo()
                ' ''RefreshGRPNo()

                'UpdateAllTotals()


                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Document_Code)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.Document_Code, MyBase.Form_ID, gv1)
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.Document_Code)
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
                If (ClsJobWorkBilling.DeleteData(txtDocNo.Value)) Then
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

    End Sub

    Private Sub SetTax()
        If clsCommon.myLen(clsCommon.myCstr(TxtBillingLocation.Value)) > 0 Then
            txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(TxtBillingLocation.Value, fndcustNo.Value, "S")
            lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfPurchaseType(txtTaxGroup.Value, Nothing)
            SetTaxDetails()
        End If
    End Sub

    'Private Sub SetTax()
    '    If clsCommon.myLen(clsCommon.myCstr(fndLocation.Value)) > 0 Then
    '        txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(fndLocation.Value, fndcustNo.Value, "S")
    '        lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfPurchaseType(txtTaxGroup.Value, Nothing)
    '        SetTaxDetails()
    '        'Else
    '        '    txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(fndLocation.Value, fndcustNo.Value, "S")
    '        '    lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfPurchaseType(txtTaxGroup.Value, Nothing)
    '        '    SetTaxDetails()
    '    End If
    'End Sub

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
        Dim WhrCls As String = "Tax_Group_Type='S'"
        'If chkExcisable.Checked Then
        '    WhrCls += " and Excisable='Y'"
        'End If
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
        If ddlInvoiceType.SelectedValue = "E" And count <> 0 Then
            WhrCls += " and Excisable='Y'"
        End If
        Dim strCustomer As String = ""

        If clsCommon.myLen(strCustomer) <= 0 Then
            strCustomer = fndcustNo.Value
        End If
        'txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(fndLocation.Value, strCustomer, "S", txtTaxGroup.Value, isButtonClicked)
        txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(TxtBillingLocation.Value, strCustomer, "S", txtTaxGroup.Value, isButtonClicked)
        SetTaxDetails()
    End Sub

    Sub SetTaxDetails()
        'Dim strTaxCode, StrExcisable As String
        Dim intCount As Integer = 0
        LoadBlankGridTax()
        Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='s') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='s' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='s' order by Trans_Code"
        ' Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", fndcustNo.Value, fndLocation.Value)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", fndcustNo.Value, TxtBillingLocation.Value)
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
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dr("TaxRate"))
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

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        Try
            'Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", fndcustNo.Value, fndLocation.Value)
            Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", fndcustNo.Value, TxtBillingLocation.Value)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                If isForCurrentRow Then
                    BlankTaxDetails(gv1.CurrentRow.Index)
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colICode)) > 0 Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If (CalculateTaxRatefromItemwsieTaxOnSale = 0) Then
                                If isChangeRate Then
                                    gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                            Else
                                If isChangeRate Then
                                    Dim objTM As clsItemWiseTaxAuthority
                                    objTM = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(txtTaxGroup.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value), txtDocDate.Value, "S")
                                    If objTM IsNot Nothing Then
                                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = objTM.TAX_Rate
                                        gv1.CurrentRow.Cells(colItemwiseTaxCode).Value = objTM.HCODE
                                    End If
                                End If

                            End If
                            gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv1.CurrentRow.Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
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
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                                ii = ii + 1
                            Next
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical' and Is_Jobwork=1  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        fndLocation.Value = clsCommon.ShowSelectForm("LocTnMstrFND1", qry, "Code", WhrCls, fndLocation.Value, "Code", isButtonClicked)
        txtlocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
        Dim strcustNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code from TSPL_LOCATION_MASTER left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_LOCATION_MASTER.Jobwork_Vendor= TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code  where Location_Code='" & fndLocation.Value & "'"))
        If clsCommon.myLen(clsCommon.myCstr(strcustNo)) > 0 Then
            Dim strcustomernumber As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_customer_master.Cust_Code from tspl_customer_master where tspl_customer_master.Status ='N' and tspl_customer_master.Cust_Code='" & strcustNo & "'"))
            If clsCommon.myLen(clsCommon.myCstr(strcustomernumber)) > 0 Then
                fndcustNo.Value = strcustomernumber
                txtcustdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name  from TSPL_customer_master where Cust_Code='" & fndcustNo.Value & "'"))
            Else
                clsCommon.MyMessageBoxShow(Me, "Customer should be active", Me.Text)
            End If
        End If

        '' SetTax()
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim strItem As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
        Dim strUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value)
        Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colprice).Value)
        Dim dblAmt As Double = dblQty * dblRate
        Dim dblKgQty As Double = clsItemMaster.GetKGConvQty(strItem, strUnit, dblQty)
        Dim dblKgRate As Double = clsItemMaster.GetKGConvQty(strItem, strUnit, dblRate)
        gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        Dim dblDisPer As Double = 0
        Dim dblDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = dblAmt - dblDisAmt
        Dim dbnetpriceamt As Double = dblAmt - dblDisAmt
        Dim dbtotdisamt As Double = dblQty * dblDisAmt
        Dim dblTotalTaxAmt As Double = 0
        For ii As Integer = 1 To 10
            Dim Strii As String = clsCommon.myCstr(ii)

            Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
            If clsCommon.myLen(strTaxCode) > 0 Then
                Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                ''Dim IsExcisable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value)
                Dim dblBaseAmt As Double = 0
                Dim dblTaxAmt As Double = 0
                If IsSurTax Then
                    Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                    dblBaseAmt = dblSurTaxAmt
                Else
                    Dim dblOtherTaxAmt As Double = 0
                    dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                    dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                End If
                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
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

            dblTotalTaxAmt += clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value)
        Next

        'gv1.Rows(IntRowNo).Cells(colitemnetamt).Value = dblAmtAfterDis
        'gv1.Rows(IntRowNo).Cells(colnetprice).Value = dbnetpriceamt
        'gv1.Rows(IntRowNo).Cells(coltotdisamt).Value = dbtotdisamt

        'Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
        Dim dblAmtAfterTax As Double = dblAmtAfterDis + dblTotalTaxAmt
        gv1.Rows(IntRowNo).Cells(colConvKgQty).Value = dblKgQty
        gv1.Rows(IntRowNo).Cells(colConvKgPrice).Value = dblKgRate
        gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotalTaxAmt, 2)
        gv1.Rows(IntRowNo).Cells(coltotamt).Value = Math.Round(dblAmtAfterTax, 2)
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
                'frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colitemnetamt).Value)
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
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                'dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colitemnetamt).Value)
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



        For ii As Integer = 1 To gv2.Rows.Count
            Select Case (ii)
                Case 1
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt1, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt1, 2)
                    If dblTaxBaseAmt1 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 2
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                    If dblTaxBaseAmt2 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 3
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                    If dblTaxBaseAmt3 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 4
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt4, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt4, 2)
                    If dblTaxBaseAmt4 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 5
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt5, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt5, 2)
                    If dblTaxBaseAmt5 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt5 * 100) / dblTaxBaseAmt5, 2)
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

        lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        lbldocamt.Text = clsCommon.myFormat(clsCommon.myCdbl(lblTotRAmt.Text))

    End Sub

    Private Sub gv2_DockChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv2.DockChanged

    End Sub

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
            'Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(fndLocation.Value, txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), fndcustNo.Value, "S")
            Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(TxtBillingLocation.Value, txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), fndcustNo.Value, "S")
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

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_JOBWORK_BILLING_HEAD where Document_Code='" + txtDocNo.Value + "'"
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
        Dim qry As String = "select Document_Code as Code,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,cust_Code as [Customer Code], cust_Name as Customer,Loc_Name as [JobWork Location Name],Billing_Loc_Code as [Billing Location],Billing_Loc_Name as [Billing Location Name],doc_Amt as Amount,case when Status='0' then 'Pending' else 'Approved' end as [Status] from TSPL_JOBWORK_BILLING_HEAD"

        Dim whrClas As String = " 2=2"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += "  and loc_code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        LoadData(clsCommon.ShowSelectForm("JobBillinfnd", qry, "Code", whrClas, txtDocNo.Value, "Document_Date desc", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER"
        txtTermCode.Value = clsCommon.ShowSelectForm("TermCodIDS1", qry, "Code", "", txtTermCode.Value, "Code", isButtonClicked)
        SetTermDetails()

    End Sub

    Sub SetTermDetails()
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER where Terms_Code='" + txtTermCode.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
            txtDueDate.Value = txtDocDate.Value.AddDays(clsCommon.myCdbl(dt.Rows(0)("No Of Days")))
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
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Material Sales", txtDocDate.Value) = False Then
                    Exit Sub
                End If
                If SaveData() = True Then
                    If (ClsJobWorkBilling.PostData(txtDocNo.Value)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                        LoadData(txtDocNo.Value, NavigatorType.Current)

                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
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
                                                      "TSPL_JOBWORK_BILLING_HEAD " + Environment.NewLine +
                                                      "TSPL_JOBWORK_BILLING_DETAIL " + Environment.NewLine +
                                                      "TSPL_CUSTOM_FIELD_VALUES " + Environment.NewLine +
                                                      "Press Alt+P for Post Trasnaction " + Environment.NewLine +
                                                      "TSPL_BATCH_ITEM " + Environment.NewLine +
                                                      "TSPL_Customer_Invoice_Head " + Environment.NewLine +
                                                      "TSPL_Customer_Invoice_detail " + Environment.NewLine +
                                                      "TSPL_JOURNAL_MASTER " + Environment.NewLine +
                                                      "TSPL_JOURNAL_DETAILS")
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
        qry += " from TSPL_JOBWORK_BILLING_DETAIL where invoice_No='" + strDocumentNo + "' "
        qry += " union all "
        qry += " select TAX2 as Tax,TAX2_Rate as Rate,TAX2_Amt as Amt "
        qry += " from TSPL_JOBWORK_BILLING_DETAIL where invoice_No='" + strDocumentNo + "'  "
        qry += " union all "
        qry += " select TAX3 as Tax,TAX3_Rate as Rate,TAX3_Amt as Amt "
        qry += " from TSPL_JOBWORK_BILLING_DETAIL where invoice_No='" + strDocumentNo + "'  "
        qry += " union all "
        qry += " select TAX4 as Tax,TAX4_Rate as Rate,TAX4_Amt as Amt "
        qry += " from TSPL_JOBWORK_BILLING_DETAIL where invoice_No='" + strDocumentNo + "'  "
        qry += " union all "
        qry += " select TAX5 as Tax,TAX5_Rate as Rate,TAX5_Amt as Amt "
        qry += " from TSPL_JOBWORK_BILLING_DETAIL where invoice_No='" + strDocumentNo + "'   "
        qry += " union all "
        qry += " select TAX6 as Tax,TAX6_Rate as Rate,TAX6_Amt as Amt "
        qry += " from TSPL_JOBWORK_BILLING_DETAIL where invoice_No='" + strDocumentNo + "'   "
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

    Private Sub gv2_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv2.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv2.Columns(colTTaxAmt)) Then
                    gv2.CurrentRow.Cells(colTTaxAmt).ReadOnly = True
                End If

                Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                cell.GradientStyle = GradientStyles.Solid
                cell.BackColor = Color.FromArgb(243, 181, 51)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then

            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub rbtnTaxCalManual_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        If Not isInsideLoadData Then
            SetTaxDetails()
        End If
    End Sub

    Private Sub gv2_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedTaxOpen Then
                    isCellValueChangedTaxOpen = True
                    If (e.Column Is (gv2.Columns(colTTaxAmt))) Then
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRow(ii)
                        Next
                        UpdateAllTotals()
                    End If
                    isCellValueChangedTaxOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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
                        txtdescription.Text = clsCommon.myCstr(dr("Reason"))
                        txtref.Text = clsCommon.myCstr(dr("Remarks"))

                    End If
                    gv1.Rows.AddNew()
                    gv1.CurrentRow.Cells(colLineNo).Value = LineNo
                    gv1.CurrentRow.Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.CurrentRow.Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.CurrentRow.Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_code"))
                    '==========Added by preeti gupta
                    gv1.CurrentRow.Cells(COLHSNNo).Value = clsItemMaster.GetItemHSNCode(gv1.CurrentRow.Cells(colICode).Value, Nothing)
                    '===================================
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
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Document Number not found to do this operation")
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

                If ClsJobWorkBilling.ReverseAndUnpost(txtDocNo.Value) Then
                    saveCancelLog(Reason, "Reverse And Recreate", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
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

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click ''ERO/29/03/19-000527 by balwinder on 01/Apr/2019
        Try
            isInsideLoadData = True
            LoadBlankGrid()
            Dim qry As String = " select aa.item_code,max(TSPL_ITEM_MASTER.Item_Desc) as ItemDesc,max(TSPL_ITEM_MASTER.HSN_Code) as HSN, " & _
                "aa.document_no as EstimateNo,sum(Qty) as Qty,max(UOM) as unit,max(Job_Work_Rate) as Rate,sum(FatKg) as FatKg, " & _
                "avg(FatPer) as FatPer,sum(SNFKg) as SNFKg,sum(SNFPer) as SNFPer,max(TabRMItem.Item_Code) as RMItem,max(Document_Date) as Document_Date from ( " & _
                "select item_code,uom,qty,TSPL_JWI_ESTIMATION_HEAD.document_no,TSPL_JWI_ESTIMATION_HEAD.Document_Date,FAT_KG as FatKg,FAT_Per as FatPer,0 as SNFKg, " & _
                "0 as SNFPer from TSPL_JWI_ESTIMATION_HEAD left join   TSPL_JWI_ESTIMATION_FAT_PRODUCTION on " & _
                "TSPL_JWI_ESTIMATION_HEAD.Document_NO=TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Document_NO  " & _
                "where CONVERT(date, tr_date,103) > = '" & clsCommon.GetPrintDate(txtFromdate.Value, "dd/MMM/yyyy") & "' and " & _
                "CONVERT(date, tr_date,103) < ='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' and " & _
                "TSPL_JWI_ESTIMATION_HEAD.Location_Code='" & fndLocation.Value & "'  and TSPL_JWI_ESTIMATION_HEAD.Status=1 and len(isnull( TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Item_Code,''))>0 and TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Qty>0 " & _
                "union all " & _
                "select item_code,uom,qty,TSPL_JWI_ESTIMATION_HEAD.document_no,TSPL_JWI_ESTIMATION_HEAD.Document_Date,0 as FatKg,0 as FatPer,SNF_KG as SNFKg,SNF_Per as SNFPer " & _
                "from TSPL_JWI_ESTIMATION_HEAD left join   TSPL_JWI_ESTIMATION_SNF_PRODUCTION on " & _
                "TSPL_JWI_ESTIMATION_HEAD.Document_NO=TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Document_NO " & _
                "where convert(date, tr_date,103) > = '" & clsCommon.GetPrintDate(txtFromdate.Value, "dd/MMM/yyyy") & "' and " & _
                "CONVERT(date, tr_date,103) < = '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' and " & _
                "TSPL_JWI_ESTIMATION_HEAD.Location_Code='" & fndLocation.Value & "'  and TSPL_JWI_ESTIMATION_HEAD.Status=1 and len(isnull( TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Item_Code,''))>0 and TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Qty>0 ) aa " & _
                "left join TSPL_ITEM_MASTER on aa.Item_Code=TSPL_ITEM_MASTER.Item_Code left join TSPL_ITEM_UOM_DETAIL on " & _
                "aa.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and aa.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code " + Environment.NewLine & _
                "left join (select Document_NO,max(Item_Code) as Item_Code  from TSPL_JWI_ESTIMATION_WEIGHMENT group by  TSPL_JWI_ESTIMATION_WEIGHMENT.Document_NO ) as TabRMItem on TabRMItem.Document_NO=aa.Document_NO " + Environment.NewLine + _
                "group by aa.item_code,Uom,aa.document_no having (aa.Document_NO + aa.Item_Code) not in  (select JW_EstimationNo + Item_Code from TSPL_JOBWORK_BILLING_detail where  TSPL_JOBWORK_BILLING_detail.Document_Code not in ('" + txtDocNo.Value + "'))"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim LineNo As Integer = 0
            For Each dr As DataRow In dt.Rows
                LineNo += 1
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = LineNo
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("ItemDesc"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("unit"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(COLHSNNo).Value = clsCommon.myCstr(dr("HSN"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colJWEstimateNo).Value = clsCommon.myCstr(dr("EstimateNo"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("Qty"))
                If settJWIRateofFGasPerRM Then
                    Dim objt As clsJWIItemPriceDetail = clsJWIItemPriceDetail.GetJobPrice(clsCommon.myCstr(dr("RMItem")), clsCommon.myCstr(dr("Item_Code")), clsCommon.myCstr(dr("unit")), clsCommon.myCDate(dr("Document_Date")), clsCommon.myCstr(fndcustNo.Value), Nothing)
                    If objt IsNot Nothing Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colprice).Value = objt.FG_Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCode).Value = objt.Price_Code
                    End If
                    objt = Nothing
                Else
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colprice).Value = clsCommon.myCdbl(dr("Rate"))
                End If


                gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT).Value = clsCommon.myCdbl(dr("FatKg"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).Value = clsCommon.myCdbl(dr("SNFKg"))
                Dim IsTaxable As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where item_code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) & "'"))
                If IsTaxable = 1 Then
                    SetitemWiseTaxSetting(True, True)
                End If
                UpdateCurrentRow(gv1.Rows(gv1.Rows.Count - 1).Index)
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ' Ticket No : BHA/14/11/18-000680, BHA/17/12/18-000757 By Prabhakar 
    ' Ticket No : BHA/12/03/19-000838 By Prabhakar Work on RPT File
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Document Not Found For Print", Me.Text)
                Return
            End If
            Dim qry As String = Nothing
            'Ticket No : ERO/18/04/19-000564 By prabhakar
            Dim DateOfEInvoiceImplementation As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DateOfEInvoiceImplementation, clsFixedParameterCode.DateOfEInvoiceImplementation, Nothing))
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                qry = " select * from ( select cast(TSPL_JOBWORK_BILLING_HEAD.BarCode_Img as image) As BarCode_Img,isnull (TSPL_JOBWORK_BILLING_HEAD.IRN_No,'') as IRN_No,isnull (TSPL_JOBWORK_BILLING_HEAD.Ack_No,'') as Ack_No,case when len(isnull (TSPL_JOBWORK_BILLING_HEAD.Ack_No,'')) > 0 then convert (varchar, TSPL_JOBWORK_BILLING_HEAD.Ack_Date,103) else ''  end as Ack_Date, case when TSPL_JOBWORK_BILLING_HEAD.Is_Taxable=1 and isnull(TSPL_JOBWORK_BILLING_HEAD.EInvoice_Type,'')='BB' AND convert(date ,TSPL_JOBWORK_BILLING_HEAD.Document_Date,103)>=convert(date ,'" + clsCommon.myCstr(DateOfEInvoiceImplementation) + "',103) then 1 else 0 end as  IsEInvoiceApply," &
                      "  pppp.*, TSPL_COMPANY_MASTER_Logo.Logo_Img , '1' as  CopyType from (  select max(TSPL_COMPANY_MASTER.Access_Officer) as FSSAI_NO,max (TSPL_COMPANY_MASTER.Comp_Code) as Comp_Code, max( tspl_company_master.Comp_Name ) as  Comp_Name ,max (tspl_company_master.Add1) as Comp_Add1 , max (tspl_company_master.Add2) as Comp_Add2, max (tspl_company_master.Add3) as Comp_Add3, max (tspl_company_master.City_code) as Comp_City_Code,max (tspl_company_master.Fax) as Comp_Fax, max (tspl_company_master.Email) as Comp_Email, max (tspl_company_master.Pincode) as comp_Pincode, max (tspl_company_master.State) as Comp_State,  max (tspl_company_master.Phone1) as Comp_Phone1, max (tspl_company_master.Phone2) as comp_Phone2, max( tspl_company_master.GSTINNO) as comp_GSTINNO, max( tspl_company_master.GSTReg_No) as Company_GSTReg_No,max( tspl_company_master.CINNO) as Company_CINNO,max(tspl_company_master.PAN_No) as Company_PAN_No,max(tspl_company_master.Tin_NO) as Company_Tin_NO ,max( tspl_Customer_Master.GSTNO) as Customer_GSTIN_NO,max(TSPL_STATE_Master_For_Location.GST_STATE_Code) as Location_GST_State_Code,  max(TSPL_STATE_Master_For_Location.State_Name) as Location_State_Name, max(tspl_Location_Master.GSTNO) as Location_GSTIN_NO,  max(TSPL_STATE_Master_Customer_Location.GST_STATE_Code) as Customer_GST_State_Code,max(TSPL_STATE_Master_Customer_Location.State_Name) as Customer_State_Name,  " &
                      "  max( TSPL_JOBWORK_BILLING_HEAD.Total_Tax_Amt) as Total_Tax_Amt,max(TSPL_JOBWORK_BILLING_HEAD.Doc_amt) as Doc_amt, max ( TSPL_JOBWORK_BILLING_DETAIL.Document_code) as InvoiceNo, max( convert(varchar,TSPL_JOBWORK_BILLING_HEAD.Document_Date,103)) as Document_Date ,max(convert (varchar, TSPL_JOBWORK_BILLING_HEAD.From_Date,103)) as From_Date  , max(convert (varchar,TSPL_JOBWORK_BILLING_HEAD.To_Date,103)) as To_Date , max( TSPL_JOBWORK_BILLING_HEAD.cust_Code) as cust_Code , max (tspl_Customer_Master.Customer_Name) as Customer_Name, max (TSPL_CUSTOMER_MASTER.Add1) as Customer_Add1,max ( TSPL_CUSTOMER_MASTER.Add2) as customer_Add2, max ( TSPL_CUSTOMER_MASTER.Add3) as customer_Add3,   " &
                      "  max (TSPL_CUSTOMER_MASTER.City_Code) as Cust_City_Code,max ( TSPL_CUSTOMER_MASTER.State) as Cust_State_Code,  max (TSPL_CUSTOMER_MASTER.Phone1) as Cust_Phone1, max (TSPL_CUSTOMER_MASTER.Phone2) as Cust_Phone2,max (TSPL_JOBWORK_BILLING_HEAD.Loc_Code) as Loc_Code , max ( TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc ,   " &
                      "  max ( TSPL_LOCATION_MASTER.Add1) as Loc_Add1, max (TSPL_LOCATION_MASTER.Add2) as Loc_ADd2, max (TSPL_LOCATION_MASTER.Add3) as Loc_Add3, max( TSPL_LOCATION_MASTER.City_Code) as Loc_City_Code  ,max ( TSPL_LOCATION_MASTER.State) as Loc_State_Code,max(TSPL_LOCATION_MASTER.PIN_Code) as Loc_Pin_Code,  max(TSPL_JOBWORK_BILLING_HEAD.Status) as Status,   " &
                      "  max ( TSPL_JOBWORK_BILLING_HEAD.Description) as Description, max( TSPL_JOBWORK_BILLING_HEAD.Reff) as  Reff, max (TSPL_JOBWORK_BILLING_HEAD.Tax_Group) as Tax_Group, max (TSPL_JOBWORK_BILLING_HEAD.Tax_Desc) as Tax_Desc, max ( TSPL_JOBWORK_BILLING_HEAD.EWayBillNo) as EWayBillNo ,max (TSPL_JOBWORK_BILLING_HEAD.EWayBillDate) as EWayBillDate,  " &
                      "  TSPL_JOBWORK_BILLING_DETAIL.Item_Code, max (TSPL_ITEM_MASTER.Alies_Name) as itemdesc, max (TSPL_Item_Master.HSN_Code) as HSN_Code , max (TSPL_ITEM_MASTER.ITF_CODE) as ITF_CODE  ,TSPL_JOBWORK_BILLING_DETAIL.Unit_Code , sum( TSPL_JOBWORK_BILLING_DETAIL.convKG_Qty) as qty,  " &
                      "  TSPL_JOBWORK_BILLING_DETAIL.Price as itemcost, sum(TSPL_JOBWORK_BILLING_DETAIL.ItemAmt) as amount, sum( TSPL_JOBWORK_BILLING_DETAIL.TotalTaxAmt) as TotalTaxAmt, sum(TSPL_JOBWORK_BILLING_DETAIL.TotalAmt) as TotalAmt , max( tax1.Tax_Code_Desc) as tax1name,max( isnull (TSPL_JOBWORK_BILLING_HEAD.tax1_amt,0)) as txt1amt, max(  tax2.Tax_Code_Desc) as tax2name,max(isnull (TSPL_JOBWORK_BILLING_HEAD.tax2_amt,0)) as txt2amt, max( tax3.Tax_Code_Desc) as tax3name, max( isnull (TSPL_JOBWORK_BILLING_HEAD.tax3_amt,0)) as txt3amt,  " &
                      "  max(  tax4.Tax_Code_Desc) as tax4name, max(isnull (TSPL_JOBWORK_BILLING_HEAD.tax4_amt,0)) as txt4amt,  max(tax5.Tax_Code_Desc) as tax5name,max( isnull (TSPL_JOBWORK_BILLING_HEAD.tax5_amt,0) )as txt5amt, max( tax6.Tax_Code_Desc) as tax6name, max(isnull (TSPL_JOBWORK_BILLING_HEAD.tax6_amt,0)) as txt6amt, max( tax7.Tax_Code_Desc) as tax7name,max( isnull (TSPL_JOBWORK_BILLING_HEAD.tax7_amt,0) ) as txt7amt,max( tax8.Tax_Code_Desc )as tax8name,max( isnull (TSPL_JOBWORK_BILLING_HEAD.tax8_amt,0) )as txt8amt, max( tax9.Tax_Code_Desc) as tax9name,  " &
                      "  max( isnull (TSPL_JOBWORK_BILLING_HEAD.tax9_amt,0)) as txt9amt,max( tax10.Tax_Code_Desc )as tax10name,max( isnull (TSPL_JOBWORK_BILLING_HEAD.tax10_amt,0)) as txt10amt,  max(TSPL_JOBWORK_BILLING_HEAD.TAX1_Rate) as TAX1_Rate  ,max(TSPL_JOBWORK_BILLING_HEAD.TAX2_Rate) as TAX2_Rate ,max(TSPL_JOBWORK_BILLING_HEAD.TAX3_Rate) as TAX3_Rate,  max(TSPL_JOBWORK_BILLING_HEAD.TAX4_Rate) as TAX4_Rate,max(TSPL_JOBWORK_BILLING_HEAD.TAX5_Rate) as TAX5_Rate,max(TSPL_JOBWORK_BILLING_HEAD.TAX6_Rate) as TAX6_Rate,  max(TSPL_JOBWORK_BILLING_HEAD.TAX7_Rate) as TAX7_Rate ,max(TSPL_JOBWORK_BILLING_HEAD.TAX8_Rate) as TAX8_Rate,max(TSPL_JOBWORK_BILLING_HEAD.TAX9_Rate) as TAX9_Rate,max (TSPL_JOBWORK_BILLING_HEAD.TAX10_Rate) as  TAX10_Rate  " &
                      "  from TSPL_JOBWORK_BILLING_DETAIL   left outer join TSPL_JOBWORK_BILLING_HEAD on TSPL_JOBWORK_BILLING_HEAD.Document_Code= TSPL_JOBWORK_BILLING_DETAIL.Document_code  left outer join tspl_Customer_Master on tspl_Customer_Master.cust_Code = TSPL_JOBWORK_BILLING_HEAD.Cust_Code  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_JOBWORK_BILLING_HEAD.Loc_Code  left outer join TSPL_Item_Master on TSPL_Item_Master.Item_Code = TSPL_JOBWORK_BILLING_DETAIL.Item_Code  left outer join tspl_company_master on tspl_company_master.comp_Code = TSPL_JOBWORK_BILLING_HEAD.comp_code   " &
                      "  left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_JOBWORK_BILLING_HEAD.tax1  left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_JOBWORK_BILLING_HEAD.tax2  left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_JOBWORK_BILLING_HEAD .TAX3  left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_JOBWORK_BILLING_HEAD .tax4   left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_JOBWORK_BILLING_HEAD .tax5   left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_JOBWORK_BILLING_HEAD .TAX6  left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_JOBWORK_BILLING_HEAD .TAX7  left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_JOBWORK_BILLING_HEAD .TAX8   " &
                      "  left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_JOBWORK_BILLING_HEAD .TAX9   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_JOBWORK_BILLING_HEAD .TAX10  left outer join TSPL_STATE_MASTER  as TSPL_STATE_Master_Customer_Location on TSPL_STATE_Master_Customer_Location.STATE_CODE =tspl_Customer_Master.state   left outer join TSPL_STATE_MASTER  as TSPL_STATE_Master_For_Location on TSPL_STATE_Master_For_Location.STATE_CODE =tspl_Location_Master.state  left outer join TSPL_JWI_ESTIMATION_SNF_PRODUCTION on TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Document_No = TSPL_JOBWORK_BILLING_DETAIL.JW_EstimationNo and TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Item_Code= TSPL_JOBWORK_BILLING_DETAIL.Item_Code  left outer join TSPL_JWI_ESTIMATION_FAT_PRODUCTION on TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Document_No = TSPL_JOBWORK_BILLING_DETAIL.JW_EstimationNo and TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Item_Code= TSPL_JOBWORK_BILLING_DETAIL.Item_Code  " &
                      "  where TSPL_JOBWORK_BILLING_HEAD.Document_Code in ('" + txtDocNo.Value + "')  " &
                      "  group by TSPL_JOBWORK_BILLING_DETAIL.item_Code,TSPL_JOBWORK_BILLING_DETAIL.Unit_Code, TSPL_JOBWORK_BILLING_DETAIL.Price   " &
                      "  ) pppp left outer join TSPL_COMPANY_MASTER as TSPL_COMPANY_MASTER_Logo on TSPL_COMPANY_MASTER_Logo.Comp_Code = pppp.Comp_Code  left outer join TSPL_JOBWORK_BILLING_HEAD on TSPL_JOBWORK_BILLING_HEAD.document_code=pppp.InvoiceNo " &
                      "  ) XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1  UNION Select '1' as COL1, 4 as COL2,  'QUADRUPLICATE COPY' as CopyType1  ) YYY ON YYY.COL1=XXX.CopyType  ORDER BY YYY.COL2, XXX.Item_Code,XXX.Unit_Code,XXX.itemcost  "

            Else
                qry = " select  tspl_company_master.Comp_Name ,tspl_company_master.Add1 as Comp_Add1 , tspl_company_master.Add2 as Comp_Add2, tspl_company_master.Add3 as Comp_Add3, tspl_company_master.City_code as Comp_City_Code,tspl_company_master.Fax as Comp_Fax, tspl_company_master.Email as Comp_Email, tspl_company_master.Pincode as comp_Pincode, tspl_company_master.State as Comp_State,  tspl_company_master.Phone1 as Comp_Phone1, tspl_company_master.Phone2 as comp_Phone2,  tspl_company_master.GSTINNO as comp_GSTINNO,  tspl_company_master.GSTReg_No as Company_GSTReg_No, tspl_company_master.CINNO as Company_CINNO,tspl_company_master.PAN_No as Company_PAN_No,tspl_company_master.Tin_NO as Company_Tin_NO ," & _
                                    " tspl_Customer_Master.GSTNO as Customer_GSTIN_NO,TSPL_STATE_Master_For_Location.GST_STATE_Code as Location_GST_State_Code, " & _
                                    " TSPL_STATE_Master_For_Location.State_Name as Location_State_Name, tspl_Location_Master.GSTNO as Location_GSTIN_NO, " & _
                                    " TSPL_STATE_Master_Customer_Location.GST_STATE_Code as Customer_GST_State_Code,TSPL_STATE_Master_Customer_Location.State_Name as Customer_State_Name," & _
                                    " TSPL_STATE_Master_Customer_Location.GST_STATE_Code as Customer_GST_State_Code,TSPL_STATE_Master_Customer_Location.State_Name as Customer_State_Name, " & _
                                    " tspl_Customer_Master.GSTNO as Customer_GSTIN_NO,TSPL_STATE_Master_For_Location.GST_STATE_Code as Location_GST_State_Code, " & _
                                    " TSPL_STATE_Master_For_Location.State_Name as Location_State_Name, tspl_Location_Master.GSTNO as Location_GSTIN_NO, " & _
                                    " TSPL_JOBWORK_BILLING_HEAD.Total_Tax_Amt,TSPL_JOBWORK_BILLING_HEAD.Doc_amt,  TSPL_JOBWORK_BILLING_DETAIL.Document_code as InvoiceNo, " & _
                                    " convert(varchar,TSPL_JOBWORK_BILLING_HEAD.Document_Date,103) as Document_Date ,TSPL_JOBWORK_BILLING_HEAD.From_Date , TSPL_JOBWORK_BILLING_HEAD.To_Date , " & _
                                    " TSPL_JOBWORK_BILLING_HEAD.cust_Code, tspl_Customer_Master.Customer_Name,TSPL_CUSTOMER_MASTER.Add1 as Customer_Add1, TSPL_CUSTOMER_MASTER.Add2 as customer_Add2, " & _
                                    " TSPL_CUSTOMER_MASTER.Add3 as customer_Add3, TSPL_CUSTOMER_MASTER.City_Code as Cust_City_Code, TSPL_CUSTOMER_MASTER.State as Cust_State_Code, " & _
                                    " TSPL_CUSTOMER_MASTER.Phone1 as Cust_Phone1, TSPL_CUSTOMER_MASTER.Phone2 as Cust_Phone2,TSPL_JOBWORK_BILLING_HEAD.Loc_Code , TSPL_LOCATION_MASTER.Location_Desc , " & _
                                    " TSPL_LOCATION_MASTER.Add1 as Loc_Add1, TSPL_LOCATION_MASTER.Add2 as Loc_ADd2, TSPL_LOCATION_MASTER.Add3 as Loc_Add3, " & _
                                    " TSPL_LOCATION_MASTER.City_Code as Loc_City_Code  , TSPL_LOCATION_MASTER.State as Loc_State_Code,TSPL_LOCATION_MASTER.PIN_Code as Loc_Pin_Code, " & _
                                    " TSPL_JOBWORK_BILLING_HEAD.Status, TSPL_JOBWORK_BILLING_HEAD.Description, " & _
                                    " TSPL_JOBWORK_BILLING_HEAD.Reff , TSPL_JOBWORK_BILLING_HEAD.Tax_Group, TSPL_JOBWORK_BILLING_HEAD.Tax_Desc, " & _
                                    " TSPL_JOBWORK_BILLING_HEAD.EWayBillNo, TSPL_JOBWORK_BILLING_HEAD.EWayBillDate, " & _
                                    " TSPL_JOBWORK_BILLING_DETAIL.JW_EstimationNo , TSPL_JOBWORK_BILLING_DETAIL.Item_Code, TSPL_Item_Master.Item_Desc as itemdesc, TSPL_Item_Master.HSN_Code, ( isnull(TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Batch_No,'' )+  isnull(TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Batch_No , ''))  as Batch_No ,  " & _
                                    " TSPL_JOBWORK_BILLING_DETAIL.Unit_Code , TSPL_JOBWORK_BILLING_DETAIL.Invoice_Qty , TSPL_JOBWORK_BILLING_DETAIL.convKG_Qty as qty, " & _
                                    " TSPL_JOBWORK_BILLING_DETAIL.FATKg, TSPL_JOBWORK_BILLING_DETAIL.SNFKg, TSPL_JOBWORK_BILLING_DETAIL.Price as itemcost, TSPL_JOBWORK_BILLING_DETAIL.ItemAmt as amount, " & _
                                    " TSPL_JOBWORK_BILLING_DETAIL.TotalTaxAmt, TSPL_JOBWORK_BILLING_DETAIL.TotalAmt , " & _
                                    " tax1.Tax_Code_Desc as tax1name,isnull (TSPL_JOBWORK_BILLING_HEAD.tax1_amt,0) as txt1amt, " & _
                                    " tax2.Tax_Code_Desc as tax2name,isnull (TSPL_JOBWORK_BILLING_HEAD.tax2_amt,0) as txt2amt, " & _
                                    " tax3.Tax_Code_Desc as tax3name,  isnull (TSPL_JOBWORK_BILLING_HEAD.tax3_amt,0) as txt3amt, " & _
                                    " tax4.Tax_Code_Desc as tax4name, isnull (TSPL_JOBWORK_BILLING_HEAD.tax4_amt,0) as txt4amt, " & _
                                    " tax5.Tax_Code_Desc as tax5name, isnull (TSPL_JOBWORK_BILLING_HEAD.tax5_amt,0) as txt5amt, " & _
                                    " tax6.Tax_Code_Desc as tax6name, isnull (TSPL_JOBWORK_BILLING_HEAD.tax6_amt,0) as txt6amt, " & _
                                    " tax7.Tax_Code_Desc as tax7name, isnull (TSPL_JOBWORK_BILLING_HEAD.tax7_amt,0) as txt7amt," & _
                                    " tax8.Tax_Code_Desc as tax8name, isnull (TSPL_JOBWORK_BILLING_HEAD.tax8_amt,0) as txt8amt, " & _
                                    " tax9.Tax_Code_Desc as tax9name, isnull (TSPL_JOBWORK_BILLING_HEAD.tax9_amt,0) as txt9amt," & _
                                    " tax10.Tax_Code_Desc as tax10name, isnull (TSPL_JOBWORK_BILLING_HEAD.tax10_amt,0) as txt10amt, " & _
                                    " TSPL_JOBWORK_BILLING_HEAD.TAX1_Rate ,TSPL_JOBWORK_BILLING_HEAD.TAX2_Rate ,TSPL_JOBWORK_BILLING_HEAD.TAX3_Rate, " & _
                                    " TSPL_JOBWORK_BILLING_HEAD.TAX4_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX5_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX6_Rate, " & _
                                    " TSPL_JOBWORK_BILLING_HEAD.TAX7_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX8_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX9_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX10_Rate, " & _
                                    " tspl_company_master.Comp_Name  " & _
                                    " from TSPL_JOBWORK_BILLING_DETAIL  " & _
                                    " left outer join TSPL_JOBWORK_BILLING_HEAD on TSPL_JOBWORK_BILLING_HEAD.Document_Code= TSPL_JOBWORK_BILLING_DETAIL.Document_code " & _
                                    " left outer join tspl_Customer_Master on tspl_Customer_Master.cust_Code = TSPL_JOBWORK_BILLING_HEAD.Cust_Code " & _
                                    " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_JOBWORK_BILLING_HEAD.Loc_Code " & _
                                    " left outer join TSPL_Item_Master on TSPL_Item_Master.Item_Code = TSPL_JOBWORK_BILLING_DETAIL.Item_Code " & _
                                    " left outer join tspl_company_master on tspl_company_master.comp_Code = TSPL_JOBWORK_BILLING_HEAD.comp_code " & _
                                    " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_JOBWORK_BILLING_HEAD.tax1 " & _
                                    " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_JOBWORK_BILLING_HEAD.tax2 " & _
                                    " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_JOBWORK_BILLING_HEAD .TAX3 " & _
                                    " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_JOBWORK_BILLING_HEAD .tax4  " & _
                                    " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_JOBWORK_BILLING_HEAD .tax5  " & _
                                    " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_JOBWORK_BILLING_HEAD .TAX6 " & _
                                    " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_JOBWORK_BILLING_HEAD .TAX7 " & _
                                    " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_JOBWORK_BILLING_HEAD .TAX8 " & _
                                    " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_JOBWORK_BILLING_HEAD .TAX9  " & _
                                    " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_JOBWORK_BILLING_HEAD .TAX10 " & _
                                    " left outer join TSPL_STATE_MASTER  as TSPL_STATE_Master_Customer_Location on TSPL_STATE_Master_Customer_Location.STATE_CODE =tspl_Customer_Master.state  " & _
                                    " left outer join TSPL_STATE_MASTER  as TSPL_STATE_Master_For_Location on TSPL_STATE_Master_For_Location.STATE_CODE =tspl_Location_Master.state " & _
                                    " left outer join TSPL_JWI_ESTIMATION_SNF_PRODUCTION on TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Document_No = TSPL_JOBWORK_BILLING_DETAIL.JW_EstimationNo and TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Item_Code= TSPL_JOBWORK_BILLING_DETAIL.Item_Code " & _
                                    " left outer join TSPL_JWI_ESTIMATION_FAT_PRODUCTION on TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Document_No = TSPL_JOBWORK_BILLING_DETAIL.JW_EstimationNo and TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Item_Code= TSPL_JOBWORK_BILLING_DETAIL.Item_Code " & _
                                    " where TSPL_JOBWORK_BILLING_HEAD.Document_Code in ('" + txtDocNo.Value + "')"

            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                Dim dtHashCodeSummary_qry As String = " select max(final.InvoiceNo) as InvoiceNo, final.HSN_Code, sum (Final.Amount- Final.Disc_Amt) as  Amount, max(TAX1_Rate) as TAX1_Rate, sum (Final.TAX1_Amt) as TAX1_Amt ,max( Final.tax1name) as tax1name , max (TAX2_Rate ) as  TAX2_Rate,sum( TAX2_Amt) as TAX2_Amt  , max( tax2name) as tax2name " & _
                                                      "   ,  max (TAX3_Rate ) as  TAX3_Rate,sum( TAX3_Amt) as TAX3_Amt  , max( isnull(tax3name,'')) as tax3name " & _
                                                      "  ,  max (TAX4_Rate ) as  TAX4_Rate,sum( TAX4_Amt) as TAX4_Amt  , max( isnull( tax4name,'')) as tax4name  " & _
                                                      "     ,  max (TAX5_Rate ) as  TAX5_Rate,sum( TAX5_Amt) as TAX5_Amt  , max( isnull(tax5name,'')) as tax5name " & _
                                                      "      ,  max (TAX6_Rate ) as  TAX6_Rate,sum( TAX6_Amt) as TAX6_Amt  , max( isnull(tax6name,'')) as tax6name " & _
                                                      "      ,  max (TAX7_Rate ) as  TAX7_Rate,sum( TAX7_Amt) as TAX7_Amt  , max( isnull(tax7name,'')) as tax7name " & _
                                                      "      ,  max (TAX8_Rate ) as  TAX8_Rate,sum( TAX8_Amt) as TAX8_Amt  , max( isnull(tax8name,'')) as tax8name " & _
                                                      "      ,  max (TAX9_Rate ) as  TAX9_Rate,sum( TAX9_Amt) as TAX9_Amt  , max( isnull(tax9name,'')) as tax9name " & _
                                                      "      ,  max (TAX10_Rate ) as  TAX10_Rate,sum( TAX10_Amt) as TAX10_Amt  , max( isnull(tax10name,'')) as tax10name  " & _
                                                      "      from (select TSPL_JOBWORK_BILLING_HEAD.DOCUMENT_CODE  as InvoiceNo, TSPL_ITEM_MASTER.HSN_Code ,TSPL_JOBWORK_BILLING_DETAIL.ItemAmt as amount, 0 as Disc_Amt,  " & _
                                                      "      TSPL_JOBWORK_BILLING_DETAIL.TAX1 ,TSPL_JOBWORK_BILLING_DETAIL.TAX1_Amt ,TSPL_JOBWORK_BILLING_DETAIL.TAX1_Rate ,  " & _
                                                      "      TSPL_JOBWORK_BILLING_DETAIL.TAX2 ,TSPL_JOBWORK_BILLING_DETAIL.TAX2_Amt ,TSPL_JOBWORK_BILLING_DETAIL.TAX2_Rate ,  " & _
                                                      "      TSPL_JOBWORK_BILLING_DETAIL.TAX3 ,TSPL_JOBWORK_BILLING_DETAIL.TAX3_Amt ,TSPL_JOBWORK_BILLING_DETAIL.TAX3_Rate,  " & _
                                                      "      TSPL_JOBWORK_BILLING_DETAIL.TAX4 ,TSPL_JOBWORK_BILLING_DETAIL.TAX4_Amt ,TSPL_JOBWORK_BILLING_DETAIL.TAX4_Rate,  " & _
                                                      "      TSPL_JOBWORK_BILLING_DETAIL.TAX5 ,TSPL_JOBWORK_BILLING_DETAIL.TAX5_Amt ,TSPL_JOBWORK_BILLING_DETAIL.TAX5_Rate,  " & _
                                                      "      TSPL_JOBWORK_BILLING_DETAIL.TAX6 ,TSPL_JOBWORK_BILLING_DETAIL.TAX6_Amt ,TSPL_JOBWORK_BILLING_DETAIL.TAX6_Rate,  " & _
                                                      "      TSPL_JOBWORK_BILLING_DETAIL.TAX7 ,TSPL_JOBWORK_BILLING_DETAIL.TAX7_Amt ,TSPL_JOBWORK_BILLING_DETAIL.TAX7_Rate,  " & _
                                                      "      TSPL_JOBWORK_BILLING_DETAIL.TAX8 ,TSPL_JOBWORK_BILLING_DETAIL.TAX8_Amt ,TSPL_JOBWORK_BILLING_DETAIL.TAX8_Rate,  " & _
                                                      "     TSPL_JOBWORK_BILLING_DETAIL.TAX9 ,TSPL_JOBWORK_BILLING_DETAIL.TAX9_Amt ,TSPL_JOBWORK_BILLING_DETAIL.TAX9_Rate,  " & _
                                                      "      TSPL_JOBWORK_BILLING_DETAIL.TAX10 ,TSPL_JOBWORK_BILLING_DETAIL.TAX10_Amt ,TSPL_JOBWORK_BILLING_DETAIL.TAX10_Rate,  " & _
                                                      "      tax1.Type as tax1name,tax2.Type as tax2name,tax3.Type as tax3name " & _
                                                      "      ,tax4.Type as tax4name ,tax5.Type as tax5name, tax6.Type as tax6name, tax7.Type as tax7name, tax8.Type as tax8name, tax9.Type as tax9name, tax10.Type as tax10name  " & _
                                                      "      from TSPL_JOBWORK_BILLING_DETAIL  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_JOBWORK_BILLING_DETAIL.Item_Code  left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_JOBWORK_BILLING_DETAIL.tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_JOBWORK_BILLING_DETAIL.tax2  left outer join tspl_tax_master as tax3 on tax3.Tax_Code =TSPL_JOBWORK_BILLING_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_JOBWORK_BILLING_DETAIL .tax4   left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_JOBWORK_BILLING_DETAIL .tax5   left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_JOBWORK_BILLING_DETAIL .TAX6    left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_JOBWORK_BILLING_DETAIL .TAX7   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code= TSPL_JOBWORK_BILLING_DETAIL .TAX8  left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_JOBWORK_BILLING_DETAIL .TAX9   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_JOBWORK_BILLING_DETAIL .TAX10  left outer join TSPL_JOBWORK_BILLING_HEAD on TSPL_JOBWORK_BILLING_HEAD.Document_Code =TSPL_JOBWORK_BILLING_DETAIL.DOCUMENT_CODE        where TSPL_JOBWORK_BILLING_HEAD.Document_Code in ('" + txtDocNo.Value + "') ) final group by final.HSN_Code  "
                Dim dtHashCodeSummary As DataTable = clsDBFuncationality.GetDataTable(dtHashCodeSummary_qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptJobWorkBilling", "Invoice", clsCommon.myCDate(dt.Rows(0)("Document_Date")), "rptCompanyAddress.rpt", "rptItemHashCodeSummaryForJW.rpt", dtHashCodeSummary, "rptItemHashCodeSummaryForJW.rpt", dtHashCodeSummary)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    ' Ticket No : BHA/22/11/18-000699  By Prabhakar
    Private Sub btnInvoiceJE_Click(sender As Object, e As EventArgs) Handles btnInvoiceJE.Click
        clsOpenJEAgainstInvoice.ShowInvoiceJEForMiscSale(txtDocNo.Value)
    End Sub

    '' richa ERO/05/03/20-001201
    Private Sub TxtBillingLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtBillingLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical' and Is_Jobwork=0  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        TxtBillingLocation.Value = clsCommon.ShowSelectForm("LocTnMstrFND1", qry, "Code", WhrCls, TxtBillingLocation.Value, "Code", isButtonClicked)
        lblBillingLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + TxtBillingLocation.Value + "'"))
        SetTax()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CancelData()
    End Sub

    Function CancelData() As Boolean
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Code is empty")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Are you sure to Cancel the Record?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Return False
            End If

            Dim strReceiptCount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select receipt_no from TSPL_RECEIPT_DETAIL where Document_No in (Select Document_No from TSPL_Customer_Invoice_Head  where AgainstScrap='" & txtDocNo.Value & "') "))
            If clsCommon.myLen(strReceiptCount) > 0 Then
                Throw New Exception("You cannot cancelled this document because receiving (" + clsCommon.myCstr(strReceiptCount) + ") has been done against its AR Invoice.")
            End If

            If clsERPFuncationality.GetEInvoiceStatus(txtDocDate.Value) = True AndAlso clsCommon.CompairString(EInvoiceType, "BB") = CompairStringResult.Equal Then
                Dim EInvoiceCancelTimeValid As Int64 = 0
                EInvoiceCancelTimeValid = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select  isnull (DATEDIFF(hour,Posting_Date,GETDATE()),0) as PostedHours from TSPL_JOBWORK_BILLING_HEAD where Document_Code = '" + txtDocNo.Value + "'"))
                If EInvoiceCancelTimeValid >= 24 Then
                    Throw New Exception("Document can not be cancelled.It has been more than 24 hours.")
                End If
            End If

            ClsJobWorkBilling.CancelData(Me.Form_ID, txtDocNo.Value, NavigatorType.Current)
            clsCommon.MyMessageBoxShow(Me, "Successfully Cancelled", Me.Text)
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function

End Class

