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

Public Class frmJobWorkDispatch
    Inherits FrmMainTranScreen

#Region "Variables"
    Const ReportID As String = "ScrapSaleGrid"
    Private isCellValueChangedOpenAdd As Boolean = False
    Private AllowtoenterrateIntoJobWorkDispatch As Boolean = False
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
    Const colItemrate As String = "colItemrate"

    Const colFAT As String = "colFAT"
    Const colSNF As String = "colSNF"

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
#End Region

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.JobWorkDispatch)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag

        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
            btnInvoicePrint.Visible = True
        Else
            btnInvoicePrint.Visible = False
        End If

        lblInvoiceNo.Text = ""
        fndcustNo.MendatroryField = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A Create Additional Cost")
        GrossWtfromItemMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GrossWtFromItemMasterONCSATransfer, clsFixedParameterCode.GrossWtFromItemMasterONCSATransfer, Nothing)) = 1, True, False)
        AllowtoenterrateIntoJobWorkDispatch = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoenterrateIntoJobWorkDispatch, clsFixedParameterCode.AllowtoenterrateIntoJobWorkDispatch, Nothing)) = 1, True, False)
        RadPageView1.SelectedPage = RadPageViewPage1
        'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
        '    chkScrapSale.Visible = True
        'End If
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
        ''End of For Attachment
        '' make editable/non editable Term Code
        txtTermCode.Enabled = clsPurchaseOrderHead.GetInventorySetting().Rows(0).Item("IsTermsEditableOnPurchase")
        ''richa agarwal 18/03/2015
        AllowChangeInvoiceType = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Allow_Change_InvoiceType from TSPL_inv_parameters")) = 0, False, True)
        isALlowVehicleGateOutValidation = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowVehicleGateOutValidationScrapSale, clsFixedParameterCode.AllowVehicleGateOutValidationScrapSale, Nothing)) = "1", True, False)
        RunBatchFifowise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBatchFifowise, clsFixedParameterCode.RunBatchFifowise, Nothing))
        '------------------------

        '-----Ravi---------------
        'CreatVatSeriesOnExciseInvoice = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateVatSeriesForProductExciseinvoice, clsFixedParameterCode.CreateVatSeriesForProductExciseinvoice, Nothing))
        'If CreatVatSeriesOnExciseInvoice = 1 Then
        '    lblSecondryInvNo.Visible = True
        '    txtVatInvNo.Visible = True
        '    btnPrint.Text = "Excise"
        '    btnPrePrint.Text = "Tax"
        'End If
        ''-------------------

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
        txtscrapinvoice.MaxLength = 30
        txtponumber.MaxLength = 30

    End Sub

    Sub BlankAllControls()
        txtDocNo.Value = ""
        txtponumber.Text = ""
        chkOnHold.Checked = False
        fndcustNo.Value = ""
        'txtinvoice.Text = ""
        fndcustNo.Value = ""
        txtcustdesc.Text = ""
        txtvehicle_mannual_no.Text = ""
        txtTransporter_Code.Value = Nothing
        txtTransporter_desc.Text = ""
        txtnrg.Value = ""
        chkinvoice.Checked = False
        chkExcisable.Checked = False
        chkOnHold.Checked = False
        chkScrapSale.Checked = False
        dtpshipment.Value = clsCommon.GETSERVERDATE()
        dtppost.Text = clsCommon.GETSERVERDATE()
        dtpexp.Text = clsCommon.GETSERVERDATE()
        txtdescription.Text = ""
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
        fndLocation.Enabled = True
        chkExcisable.Enabled = True
        lblGrossWeight.Text = ""
        lblNetWeight.Text = ""
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

        '============Added by preeti gupta[10/09/2018][GKD/10/09/18-000156]
        Dim repoHSN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHSN.FormatString = ""
        repoHSN.HeaderText = "HSN"
        repoHSN.Name = COLHSNNo
        repoHSN.Width = 150
        repoHSN.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHSN)



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


        Dim itemrate As GridViewDecimalColumn = New GridViewDecimalColumn()
        itemrate = New GridViewDecimalColumn()
        itemrate.FormatString = ""
        itemrate.HeaderText = "Item Rate"
        itemrate.Name = colItemrate
        itemrate.IsVisible = IIf(AllowtoenterrateIntoJobWorkDispatch = True, True, False)
        itemrate.Minimum = 0
        itemrate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        itemrate.ReadOnly = False
        itemrate.DecimalPlaces = 2
        gv1.MasterTemplate.Columns.Add(itemrate)


        shippedQty = New GridViewDecimalColumn()
        shippedQty.FormatString = ""
        shippedQty.HeaderText = "FAT %"
        shippedQty.Name = colFAT
        shippedQty.IsVisible = True
        shippedQty.Minimum = 0
        shippedQty.Maximum = 100
        shippedQty.ShowUpDownButtons = False
        shippedQty.Step = 0
        shippedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        shippedQty.ReadOnly = False
        shippedQty.DecimalPlaces = 2
        gv1.MasterTemplate.Columns.Add(shippedQty)

        shippedQty = New GridViewDecimalColumn()
        shippedQty.FormatString = ""
        shippedQty.HeaderText = "SNF %"
        shippedQty.Name = colSNF
        shippedQty.IsVisible = True
        shippedQty.Minimum = 0
        shippedQty.Maximum = 100
        shippedQty.ShowUpDownButtons = False
        shippedQty.Step = 0
        shippedQty.DecimalPlaces = 2
        shippedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        shippedQty.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(shippedQty)


        Dim price As GridViewDecimalColumn = New GridViewDecimalColumn()
        price.WrapText = True
        price.HeaderText = "Unit Cost"
        price.Name = colprice
        price.Width = 80
        price.Minimum = 0
        price.ReadOnly = True
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
        ItemAmt.ReadOnly = True
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
                    If gv1.CurrentColumn Is gv1.Columns(colICode) OrElse gv1.CurrentColumn Is gv1.Columns(colQty) OrElse gv1.CurrentColumn Is gv1.Columns(colprice) OrElse gv1.CurrentColumn Is gv1.Columns(colDisPer) OrElse gv1.CurrentColumn Is gv1.Columns(colDisAmt) OrElse gv1.CurrentColumn Is gv1.Columns(colUnit) OrElse gv1.CurrentColumn Is gv1.Columns(colItemrate) Then
                        If gv1.CurrentColumn Is gv1.Columns(colQty) Then
                            Dim stockqty As Double = 0
                            If clsCommon.myLen(fndLocation.Value) > 0 And clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                                'Dim str As String = "select sum(Item_Qty) from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "' and Location_Code='" + fndLocation.Value + "' "
                                Dim str As String = "select SUM(QTY) from TSPL_INVENTORY_MOVEMENT where Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "' and Location_Code='" + fndLocation.Value + "' "
                                stockqty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
                                If clsCommon.myLen(txtnrg.Value) <= 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) > stockqty Then
                                    common.clsCommon.MyMessageBoxShow("Qty more then stock qty not allowed.Availabe Qty : " + clsCommon.myCstr(stockqty))
                                    gv1.CurrentRow.Cells(colQty).Value = 0
                                End If
                            Else
                                common.clsCommon.MyMessageBoxShow("Select the Location")
                                gv1.CurrentRow.Cells(colQty).Value = 0
                            End If
                        End If
                        If gv1.CurrentColumn Is gv1.Columns(colQty) OrElse gv1.CurrentColumn Is gv1.Columns(colprice) OrElse gv1.CurrentColumn Is gv1.Columns(colDisPer) OrElse gv1.CurrentColumn Is gv1.Columns(colDisAmt) OrElse gv1.CurrentColumn Is gv1.Columns(colItemrate) Then
                            UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                            UpdateAllTotals()
                            If e.Column Is gv1.Columns(colQty) Then
                                If RunBatchFifowise = 0 Then
                                    OpenBatchItem()
                                End If
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("scrapsItefndn1", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
            gv1.CurrentRow.Cells(colprice).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Job_Work_Rate from TSPL_ITEM_UOM_DETAIL where Item_Code='" + strICode + "' and UOM_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value) + "'"))
        End If
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
        ''RICHA AGARWAL 23/03/2015 CHANGE ITEM TYPE FROM "F" TO "A"
        Dim obj As ClsScrapSaleDetail = ClsScrapSaleDetail.FinderItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "A", isButtonClick)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
            '=======Added by preeti gupta [10/09/2018]
            gv1.CurrentRow.Cells(COLHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
            '===============================================
            gv1.CurrentRow.Cells(colprice).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Job_Work_Rate from TSPL_ITEM_UOM_DETAIL where Item_Code='" + obj.Item_Code + "' and UOM_Code='" + obj.Unit_Code + "'"))
        Else
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
            gv1.CurrentRow.Cells(colprice).Value = 0
            gv1.CurrentRow.Cells(COLHSNNo).Value = ""

        End If

        SetitemWiseTaxSetting(True, True)

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
        LoadBlankGridTax()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        'txtDate.Focus()
        gv1.Rows.AddNew()

        gvadd.AllowAddNewRow = False
        gvadd.Rows.AddNew()
        ''richa agarwal 18/03/2015
        LoadInvoiceType()
        btnReverse.Visible = False
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
        Try
            ' KUNAL > TICKET : BM00000009580 ========
            If AllowFutureDateTransaction(dtpshipment.Value, Nothing) = False Then
                dtppost.Focus()
                Return False
            End If

            If btnSave.Text = "Update" Then
                Dim strchk As String = "select ispost from tspl_scrapsale_head where shipment_No='" + txtDocNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    Throw New Exception("Transaction already posted")
                    Return False
                End If
            End If
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()
            If clsCommon.myLen(fndcustNo.Value) <= 0 Then
                Throw New Exception("Please select Customer")
                fndcustNo.Focus()
                Return False
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
            If CreatVatSeriesOnExciseInvoice = 1 Then
                If clsCommon.myLen(fndShipToLocation.Value) <= 0 Then
                    Throw New Exception("Please select  To Location")
                    fndShipToLocation.Focus()
                    Return False
                End If
            End If
            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Shipment No Not found to save")
                txtDocNo.Focus()
                Return False
            End If
            'If isALlowVehicleGateOutValidation = True Then
            '    If clsCommon.myLen(txtvehicle_mannual_no.Text) > 0 Then
            '        Dim qry As String = String.Empty
            '        qry = "select count(*) from TSPL_SCRAPSALE_GATE_OUT where Shipment_No = ( select Top 1 shipment_No from  TSPL_SCRAPSALE_HEAD where Vehicle_code='" & txtvehicle_mannual_no.Text & "' order by shipment_Date desc)"
            '        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
            '            qry = "select Top 1 shipment_No from  TSPL_SCRAPSALE_HEAD where Vehicle_code='" & txtvehicle_mannual_no.Text & "' order by shipment_Date desc"
            '            Dim ShipmentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            '            Throw New Exception("Vehicle No ('" & txtvehicle_mannual_no.Text & "') use in Shipment No '" & ShipmentNo & "'. After Gate Out Shipment No '" & ShipmentNo & "' ,You can use this Vehicle No.  ")
            '            Return False

            '        End If
            '    End If

            'End If

            If isALlowVehicleGateOutValidation = True Then
                If clsCommon.myLen(txtvehicle_mannual_no.Text) > 0 Then
                    Dim qry1 As String = String.Empty
                    qry1 = " SELECT Stuff((SELECT N', ' + TSPL_SCRAPSALE_HEAD.shipment_No FROM TSPL_SCRAPSALE_HEAD left join TSPL_SCRAPSALE_GATE_OUT on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_GATE_OUT.Shipment_No  where TSPL_SCRAPSALE_HEAD.Vehicle_code='" & txtvehicle_mannual_no.Text & "' and  TSPL_SCRAPSALE_GATE_OUT.Shipment_No is null FOR XML PATH(''),TYPE).value('text()[1]','nvarchar(max)'),1,2,N'') "
                    Dim result As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1))
                    If clsCommon.myLen(result) > 0 Then
                        Throw New Exception("Vehicle No ('" & txtvehicle_mannual_no.Text & "') used in other Shipment No. You can create new Shipment with Vehicle No ('" & txtvehicle_mannual_no.Text & "')  After  Gate Out following Shipment No : '" & result & "'")
                        Return False

                    End If
                End If

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

            'If ExcisableTaxGroup() = False Then
            '    Return False
            'End If
            'richa agarwal 18/03/2015

            'If chkinvoice.Checked Then
            '    If AllowChangeInvoiceType Then
            '        If clsCommon.myLen(ddlInvoiceType.SelectedValue) <= 0 Then
            '            Throw New Exception("Please select invoice Type for creating auto invoice")
            '            ddlInvoiceType.Focus()
            '            Return False
            '        Else
            '            If InvoiceType() = False Then
            '                Return False
            '            End If
            '        End If
            '    Else
            '        InvoiceType()
            '    End If
            'End If
            ''----------------------------
            If RunBatchFifowise = 1 Then
                OpenBatchItem()
            End If
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)

                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myLen(txtnrg.Value) <= 0 Then
                    If clsCommon.myLen(strUOM) <= 0 Then
                        Throw New Exception("Please enter UOM of Item - " + strICode + ".At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
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
                        Throw New Exception("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                        Return False
                    End If

                    'done by stuti on 16/11/2016
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

        strloc = fndLocation.Value
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
                    If (common.clsCommon.MyMessageBoxShow("System is generating " & strInvoiceTypeDesc & "  Invoice Type.Do you still want to continue ?  ", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No) Then
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
            common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
        End If
    End Sub

    Function SaveData() As Boolean
        Try
            '' Anubhooti 13-Sep-2014 BM00000003735
            If FrmMainTranScreen.ValidateTransactionAccToFinYear("Job Work Dispatch", dtpshipment.Value) = False Then
                Return False
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
                obj.Doc_Type = obj.Doc_Type
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
                obj.Vehicle_code = txtvehicle_mannual_no.Text
                obj.ToLoc_Code = fndShipToLocation.Value
                If chkinvoice.Checked = True Then
                    obj.CreateInvoice = 1
                Else
                    obj.CreateInvoice = 0
                End If
                obj.Excisable = "N"
                obj.Invoice_Type = ddlInvoiceType.SelectedValue
                If clsCommon.CompairString(obj.Invoice_Type, "E") = CompairStringResult.Equal Then
                    obj.Excisable = "Y"
                End If
                ''--------------
                obj.Inter_Branch = chkInterBranch.Checked
                obj.Description = txtdescription.Text
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
                If chkScrapSale.Checked = True Then
                    obj.Is_Scrap = "Y"
                Else
                    obj.Is_Scrap = "N"
                End If
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
                obj.Doc_Type = "J"
                obj.Total_Gross_Weight = clsCommon.myCdbl(lblGrossWeight.Text)
                obj.Total_Net_Weight = clsCommon.myCdbl(lblNetWeight.Text)
                obj.Arr = New List(Of ClsScrapSaleDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New ClsScrapSaleDetail()
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.shipped_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.ItemRate = clsCommon.myCdbl(grow.Cells(colItemrate).Value)

                    objTr.FAT = clsCommon.myCdbl(grow.Cells(colFAT).Value)
                    objTr.SNF = clsCommon.myCdbl(grow.Cells(colSNF).Value)

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
                    objTr.arrBatchItem = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventory))
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

                If (obj.SaveData(obj, lblInvoiceNo.Text, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.shipment_No)
                    'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.shipment_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
            gvadd.DataSource = Nothing
            gvadd.Rows.Clear()
            fndcustNo.Enabled = False
            fndLocation.Enabled = False
            chkExcisable.Enabled = False
            'fndLocation.Enabled = False
            Dim obj As New ClsScrapSaleHead()
            obj = ClsScrapSaleHead.GetData(strCode, NavTyep, Nothing, False, "J")
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.shipment_No) > 0) Then
                If obj.ispost = 1 Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    'repoComplete.IsVisible = True
                    'repoBalQty.IsVisible = True
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
                txtvehicle_mannual_no.Text = obj.Vehicle_code
                txtTransporter_Code.Value = obj.Transporter_code
                txtTransporter_desc.Text = obj.Transporter_Name
                txtVehicleDesc.Text = ClsScrapSaleHead.GetVehicleDesc(TxtVehicleCode.Value, Nothing)
                fndShipToLocation.Value = obj.ToLoc_Code
                lblDocAmount.Text = obj.doc_Amt
                ''richa agarwal 19/03/2015
                ddlInvoiceType.SelectedValue = obj.Invoice_Type
                ''-------------

                '------Ravi---------
                txtVatInvNo.Text = obj.VAT_InvoiceNo
                If obj.Is_Scrap = "Y" Then
                    chkScrapSale.Checked = True
                Else
                    chkScrapSale.Checked = False
                End If

                ''--------------
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
                txtdescription.Text = obj.Description
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
                        ' gv1.Rows(gv1.Rows.Count - 1).Cells(colComplete).Value = IIf(objTr.Status = 0, "No", "Yes")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                        '==========Added by preeti Gupta======
                        gv1.Rows(gv1.Rows.Count - 1).Cells(COLHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        '======================================

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.shipped_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemrate).Value = objTr.ItemRate

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT).Value = objTr.FAT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).Value = objTr.SNF


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colprice).Value = objTr.price
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
            Else
                AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                If (ClsScrapSaleHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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



        Dim qry As String = "  select TSPL_customer_MASTER.cust_code as Code,TSPL_customer_MASTER.Customer_Name as Name,TSPL_customer_MASTER.Terms_Code as [Term Code] ,TSPL_TERMS_MASTER.Terms_Desc as [Term Description] ,TSPL_customer_MASTER.Tax_Group as [Tax Group],TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as [Tax Group Description] from TSPL_customer_MASTER  left outer join  TSPL_TERMS_MASTER on TSPL_customer_MASTER.Terms_Code=TSPL_TERMS_MASTER.Terms_Code  left outer join  TSPL_TAX_GROUP_MASTER on TSPL_customer_MASTER.Tax_Group=TSPL_TAX_GROUP_MASTER.Tax_Group_Code "
        Dim WhrCls As String = "TSPL_TAX_GROUP_MASTER.Tax_Group_Type='s' and  TSPL_customer_MASTER.Status ='N'"
        fndcustNo.Value = clsCommon.ShowSelectForm("CustmrMstrI1", qry, "Code", WhrCls, fndcustNo.Value, "Code", isButtonClicked)

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

        'SetTaxDetails()
        SetTermDetails()


    End Sub

    Private Sub SetTax()
        If clsCommon.myLen(clsCommon.myCstr(fndLocation.Value)) > 0 Then
            txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(fndLocation.Value, fndcustNo.Value, "S")
            lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfPurchaseType(txtTaxGroup.Value, Nothing)
            SetTaxDetails()
            'Else
            '    txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(fndLocation.Value, fndcustNo.Value, "S")
            '    lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfPurchaseType(txtTaxGroup.Value, Nothing)
            '    SetTaxDetails()
        End If
    End Sub

    Private Sub fndShipToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndShipToLocation._MYValidating
        'Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        'Dim WhrCls As String = "Location_Type='Physical'"

        'fndShipToLocation.Value = clsCommon.ShowSelectForm("VendrMastr1", qry, "Code", WhrCls, fndShipToLocation.Value, "Code", isButtonClicked)
        If clsCommon.myLen(fndcustNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("First Select Customer")
            Return
        End If
        Dim qry As String = "Select tspl_ship_To_location.Ship_To_Code as Code,tspl_ship_To_location.Ship_To_Desc as Name , tspl_ship_To_location.Add1 , tspl_ship_To_location.Add2 ,tspl_ship_To_location.Add3 , tspl_ship_To_location.Add4  from tspl_ship_To_location  "
        Dim WhrCls As String = "Ship_To_Type_Code='" + fndcustNo.Value + "'"

        fndShipToLocation.Value = clsCommon.ShowSelectForm("VendrMastr1", qry, "Code", WhrCls, fndShipToLocation.Value, "Code", isButtonClicked)

    End Sub

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
        'txtTaxGroup.Value = clsCommon.ShowSelectForm("POTaxFilterFND", qry, "Code", WhrCls, txtTaxGroup.Value, "Code", isButtonClicked)
        'lblTaxGrpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" + txtTaxGroup.Value + "'"))
        txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(fndLocation.Value, strCustomer, "S", txtTaxGroup.Value, isButtonClicked)
        SetTaxDetails()
    End Sub

    Sub SetTaxDetails()
        'Dim strTaxCode, StrExcisable As String
        Dim intCount As Integer = 0
        LoadBlankGridTax()
        Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='s') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='s' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='s' order by Trans_Code"
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", fndcustNo.Value, fndLocation.Value)
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If (dt.Rows.Count > 10) Then
                common.clsCommon.MyMessageBoxShow("Can't Handle More than 10 Tax Types in a Group")
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
        'ExcisableTaxGroup()
    End Sub

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
            common.clsCommon.MyMessageBoxShow("Please select exisable tax group")
            Return False
        ElseIf chkExcisable.Checked = False And intCount > 0 Then
            common.clsCommon.MyMessageBoxShow("Please select non excisable tax group")
            Return False
        End If

        Return True
    End Function

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        'Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsscrap(txtTaxGroup.Value)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", fndcustNo.Value, fndLocation.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colICode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        If isChangeRate Then
                            gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
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
    End Sub

    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocation._MYValidating
        'Dim obj As clsLocation = clsLocation.FinderForPhysicalLoaction(fndLocation.Value, isButtonClicked)
        'If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
        '    fndLocation.Value = obj.Code
        '    lblBillToLocation.Text = obj.Name
        'Else
        '    fndLocation.Value = ""
        '    lblBillToLocation.Text = ""
        'End If

        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        fndLocation.Value = clsCommon.ShowSelectForm("LocTnMstrFND1", qry, "Code", WhrCls, fndLocation.Value, "Code", isButtonClicked)
        txtlocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
        ''richa agarwal 19/03/2015
        If clsCommon.CompairString(clsDBFuncationality.getSingleValue("Select Excisable from tspl_location_master where Location_Code ='" & fndLocation.Value & "'"), "T") = CompairStringResult.Equal Then
            ddlInvoiceType.SelectedValue = "E"
        End If
        SetTax()
        ''------------------
    End Sub

    Private Sub TxtVehicleCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtVehicleCode._MYValidating
        Dim qry As String = "Select Segment_code as Code, Description from TSPL_GL_SEGMENT_CODE "
        Dim WhrCls As String = "Seg_No=2"
        TxtVehicleCode.Value = clsCommon.ShowSelectForm("VehicleFND1", qry, "Code", WhrCls, TxtVehicleCode.Value, "Code", isButtonClicked)
        txtVehicleDesc.Text = ClsScrapSaleHead.GetVehicleDesc(TxtVehicleCode.Value, Nothing)
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        Dim dblRate As Double = 0
        If AllowtoenterrateIntoJobWorkDispatch = True Then
            dblRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colItemrate).Value)
        Else
            dblRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colprice).Value)
        End If
        Dim dblAmt As Double = dblQty * dblRate
        gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
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
            If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) AndAlso rbtnTaxCalAutomatic.IsChecked Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
                frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colitemnetamt).Value)
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
                'ElseIf (gv1.CurrentColumn Is gv1.Columns(colComplete) AndAlso (UsLock1.Status = ERPTransactionStatus.Approved)) Then
                '    Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                '    Dim intSNo As Integer = Convert.ToInt32((clsCommon.myCdbl(gv1.CurrentRow.Cells(colLineNo).Value)))
                '    Dim strStatus As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colComplete).Value)
                '    If clsCommon.myLen(txtDocNo.Value) > 0 AndAlso clsCommon.myLen(strICode) > 0 AndAlso intSNo > 0 AndAlso clsCommon.CompairString(strStatus, "No") = CompairStringResult.Equal Then
                '        If common.clsCommon.MyMessageBoxShow("Do you want to complete the item " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value), Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                '            If clsPurchaseOrderDetail.CompletePO(txtDocNo.Value, strICode, intSNo) Then
                '                common.clsCommon.MyMessageBoxShow("Successfully Completed")
                '                LoadData(txtDocNo.Value, NavigatorType.Current)
                '            End If
                '        End If
                '    End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
        End If
        funShowAmt()
        lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt)
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblAmtAfterDis)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        lbldocamt.Text = clsCommon.myFormat(clsCommon.myCdbl(lbladdcharges.Text) + clsCommon.myCdbl(lblTotRAmt.Text))
        lblGrossWeight.Text = clsCommon.myCstr(Math.Round(dblGrossWeight, 3, MidpointRounding.AwayFromZero))
        lblNetWeight.Text = clsCommon.myCstr(Math.Round(dblNetWeight, 3, MidpointRounding.AwayFromZero))

    End Sub

    Private Sub gv2_DockChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv2.DockChanged

    End Sub

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
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
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    UpdateCurrentRow(ii)
                Next
                UpdateAllTotals()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        'Try

        '    Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
        '    cell.GradientStyle = GradientStyles.Solid
        '    cell.BackColor = Color.FromArgb(243, 181, 51)
        'Catch ex As Exception
        '    'common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
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
            common.clsCommon.MyMessageBoxShow(ex.Message)

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
            Dim qst As String = "select count(*) from tspl_scrapsale_head where shipment_No='" + txtDocNo.Value + "' and Doc_Type='J'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select shipment_No as Code,CONVERT(varchar(10), shipment_Date,103)+' '+ CONVERT(varchar(5), shipment_Date,114) as Date,cust_Code as [Customer Code], cust_Name as Customer,ship_Total_Amt as Amount,case when ispost='0' then 'Pending' else 'Approved' end as [Status],(select top 1 invoice_No from TSPL_SCRAPINVOICE_HEAD where TSPL_SCRAPINVOICE_HEAD.shipment_No=tspl_scrapsale_head.shipment_No) as [Invoice No] from tspl_scrapsale_head"

        Dim whrClas As String = " Doc_Type ='J'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += "  and loc_code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        LoadData(clsCommon.ShowSelectForm("ScrpCodFiltrF1", qry, "Code", whrClas, txtDocNo.Value, "shipment_Date desc", isButtonClicked), NavigatorType.Current)
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
                If SaveData() Then
                    If (ClsScrapSaleHead.PostData(txtDocNo.Value)) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Posted")
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
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
            common.clsCommon.MyMessageBoxShow(err.Message)
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
            'Add Tool tip Task No- TEC/22/05/18-000245
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                                  "TSPL_SCRAPSALE_HEAD " + Environment.NewLine + _
                                                  "TSPL_SCRAPSALE_DETAIL " + Environment.NewLine + _
                                                  "TSPL_SCRAPINVOICE_HEAD " + Environment.NewLine + _
                                                  "TSPL_SCRAPINVOICE_detail " + Environment.NewLine + _
                                                  "TSPL_CUSTOM_FIELD_VALUES " + Environment.NewLine + _
                                                  "Press Alt+P for Post Trasnaction " + Environment.NewLine + _
                                                  "TSPL_BATCH_ITEM " + Environment.NewLine + _
                                                  "TSPL_INVENTORY_MOVEMENT " + Environment.NewLine + _
                                                  "TSPL_Customer_Invoice_Head " + Environment.NewLine + _
                                                  "TSPL_Customer_Invoice_detail " + Environment.NewLine + _
                                                  "TSPL_REMITTANCE " + Environment.NewLine + _
                                                  "TSPL_JOURNAL_MASTER " + Environment.NewLine + _
                                                  "TSPL_JOURNAL_DETAILS")
            'Add Tool tip Task No- TEC/22/05/18-000245
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        End If
    End Sub

    Sub Print(ByVal isPrint As Boolean)
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
                Dim Query As String = PrintMaterialSale()
                Dim dtDocdate As Date?
                dtDocdate = Nothing
                dtDocdate = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue("select TSPL_SCRAPINVOICE_HEAD.shipment_Date from TSPL_SCRAPINVOICE_HEAD where shipment_no='" + txtDocNo.Value + "' "), "dd/MMM/yyyy")
                Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(Query)
                If dt3.Rows.Count > 0 Then
                    'If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "E") <> CompairStringResult.Equal Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt3, "rptJobWorkDispatch", "Tax Invoice", clsCommon.myCDate(dt3.Rows(0)("shipment_Date")))
                    'End If
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
                '                    frmCrystalReportViewer.funreport(CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleExciseInvoice", "Excise Invoice")
                '                Else
                '                    frmCrystalReportViewer.funreport(CrystalReportFolder.NewSalesReports, dt3, "rptProductSalesTaxInvoice", "Vat Invoice")
                '                End If
                '            Else
                '                frmCrystalReportViewer.funreport(CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleExciseInvoice", "Excise Invoice")
                '                frmCrystalReportViewer.funreport(CrystalReportFolder.NewSalesReports, dt3, "rptProductSalesTaxInvoice", "Vat Invoice")
                '            End If
                '        Else

                '            frmCrystalReportViewer.funreport(CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleExciseInvoice", "Excise Invoice")

                '        End If

                '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "T") = CompairStringResult.Equal Then
                '           c

                '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "R") = CompairStringResult.Equal Then
                '            frmCrystalReportViewer.funreport(CrystalReportFolder.NewSalesReports, dt3, "rptCentralTaxInvoice", "Retail Invoice")
                '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "I") = CompairStringResult.Equal Then
                '            frmCrystalReportViewer.funreport(CrystalReportFolder.NewSalesReports, dt3, "rptCentralTaxInvoice", "Interstate Invoice")
                '        End If
            ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                Dim Query As String = PrintQry()
                Dim dtDocdate As Date?
                dtDocdate = Nothing
                dtDocdate = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue("select TSPL_SCRAPINVOICE_HEAD.shipment_Date from TSPL_SCRAPINVOICE_HEAD where shipment_no='" + txtDocNo.Value + "' "), "dd/MMM/yyyy")
                Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(Query)
                If dt3.Rows.Count > 0 Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt3, "rptJobworkDispatchChalan", "Dispatch Chalan", clsCommon.myCDate(dt3.Rows(0)("shipment_Date")))
                End If

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
                        common.clsCommon.MyMessageBoxShow("Invoice No does't exist for this loadout")
                    Else
                        Dim qry As String
                        Dim dt As DataTable
                        qry = "select h.Description,h.Reff,TSPL_SCRAPSALE_HEAD.Vehicle_Id,TSPL_GL_SEGMENT_CODE.Description as Vdescription, h.Created_By ,h.Modify_By , tspl_company_master.logo_img,tspl_company_master.logo_img2,tspl_company_master.Comp_Name  as CompanyName,tspl_company_master.Tin_No as CompanyTin," + Address + " as  CompanyAddress,h.invoice_No as ScrapInvoice,h.shipment_Date  as ScrapInvoiceDate," & _
                                   "h.cust_Name as CustomerName,cm.PAN as Cust_PAN,h.cust_Code as CustomerCode,case when len(cm.Add1)>0 then cm.Add1 else '' end +" & _
                                   "case when len(cm.Add2)>0 then ',' else '' end + " & _
                                   "case when len(cm.Add2)>0 then cm.Add2 else ''end +" & _
                                   "case when len(cm.Add3)>0 then ',' else '' end +" & _
                                   "case when len(cm.Add3)>0 then cm.Add3 else '' end   as CustomerAddress," & _
                                   "TSPL_SCRAPSALE_HEAD.NRG_No  as NrgNo,cm.CST as CstNo,TSPL_LOCATION_MASTER.Tin_No as TinNo,d.Item_Code as ItemCode," & _
                                   " (Select ISNULL(Cheapter_Heads,'') from TSPL_ITEM_MASTER WHere TSPL_ITEM_MASTER.Item_Code=d.Item_Code)as [ChapterHead], " & _
                                   "d.Item_Desc as Desciption,d.invoice_Qty as Quantity ,d.unit_code as Uom,d.price as Rate," & _
                                   "d.ItemAmt as Amount, d.TAX1_Amt as Dtax1_Amt, d.DiscountAmt ,h.TAX1 as TaxRateDesc1, ROUND(h.TAX1_Amt, 0) as TaxRate1,h.TAX2 as TaxRateDesc2,ROUND(h.TAX2_Amt,0) as TaxRate2,h.TAX3 as TaxRateDesc3 ,ROUND(h.TAX3_Amt,0) as TaxRate3,h.TAX4 as TaxRateDesc4,ROUND(h.TAX4_Amt,0) as TaxRate4,h.TAX5 as TaxRateDesc5,ROUND(h.TAX5_Amt,0) as TaxRate5,h.TAX6 as TaxRateDesc6,ROUND(h.TAX6_Amt,0) as TaxRate6,h.TAX7 as TaxRateDesc7,ROUND(h.TAX7_Amt,0) as TaxRate7,h.TAX8 as  TaxRateDesc8,ROUND(h.TAX8_Amt,0) as TaxRate8,h.TAX9 as TaxRateDesc9,ROUND(h.TAX9_Amt,0) as TaxRate9,h.TAX10 as TaxRateDesc10,ROUND(h.TAX10_Amt,0) as  TaxRate10, " & _
                                   " h.TAX1_Rate , h.TAX2_Rate, h.TAX3_Rate, h.TAX4_Rate, h.TAX5_Rate, h.TAX6_Rate, h.TAX7_Rate, h.TAX8_Rate, h.TAX9_Rate, h.TAX10_Rate, " & _
                                   " TSPL_SCRAPSALE_HEAD.Excisable " & _
                                   "from TSPL_SCRAPINVOICE_HEAD h left outer join TSPL_SCRAPINVOICE_DETAIL d on h.invoice_No =d.invoice_No " & _
                                   "left outer join TSPL_CUSTOMER_MASTER   cm on h.cust_Code =cm.Cust_Code  left outer join tspl_company_master   on tspl_company_master.Comp_Code ='" + objCommonVar.CurrentCompanyCode + "'left outer join TSPL_SCRAPSALE_HEAD  on h.shipment_No =TSPL_SCRAPSALE_HEAD.shipment_No left outer join TSPL_LOCATION_MASTER on  h.Loc_Code=TSPL_LOCATION_MASTER.Location_Code left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code  =TSPL_SCRAPSALE_HEAD.Vehicle_Id where h.invoice_No='" + clsCommon.myCstr(InvoiceNo) + "'"
                        dt = clsDBFuncationality.GetDataTable(qry)
                        '====for KDIL(by shivani)==============='
                        Dim Qry1 As String = "select  convert(varchar,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) as shipment_Date, tspl_customer_master.PAN as Cust_Pan, TSPL_SCRAPINVOICE_HEAD.Description,FromLocation.HOAdd1 as frmHO1,FromLocation.HOAdd2 as frmHO2,ToLocation.HOAdd1 as to_HO1,ToLocation.HOAdd2 as to_HO2,TSPL_SCRAPINVOICE_HEAD.invoice_No,convert(varchar,TSPL_SCRAPINVOICE_HEAD.posting_Date,103) as Invoice_Date,TSPL_SCRAPSALE_HEAD.Loc_Code as From_Location,FromLocation.Location_Desc as [From Location Desc],(FromLocation.Add1+FromLocation.Add2+FromLocation.Add3+FromLocation.Add4)as [From Address] ,FromLocation.Pin_Code,FromLocation.TIN_No,FromLocation.CST_No,FromLocation.State as From_State,FromState.State_Name as frm_State_name  ,TSPL_SCRAPSALE_HEAD.ToLoc_Code as To_Location,ToLocation.Location_Desc as To_Location_Desc,(ToLocation.Add1+ToLocation.Add2+ToLocation.Add3+ToLocation.Add4)as [To Address],tspl_customer_master.Pin_Code as [To Pin Code],tspl_customer_master.TIN_No as [To TIN No],tspl_customer_master.CST as [To CST No],tspl_customer_master.Phone1 as [To phone],ToLocation.State as To_State,ToState.State_Name as To_state_name,TSPL_SCRAPSALE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,Shipped_Qty,TSPL_SCRAPSALE_DETAIL.Unit_Code,Price,ItemAmt,TSPL_SCRAPSALE_HEAD.Invoice_Type,TSPL_SCRAPSALE_HEAD.Total_Tax_Amt,TSPL_SCRAPSALE_HEAD.Doc_Amt,	TAX1 .Tax_Code_Desc as tax1name,isnull (TSPL_SCRAPSALE_HEAD.tax1_rate,0) as txt1Rate,isnull (TSPL_SCRAPSALE_HEAD.tax1_amt,0) as txt1amt,      "
                        Qry1 += " tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SCRAPSALE_HEAD.tax2_rate,0) as txt2Rate,isnull (TSPL_SCRAPSALE_HEAD.tax2_amt,0) as txt2amt,"
                        Qry1 += " tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SCRAPSALE_HEAD.tax3_rate,0) as txt3Rate,isnull (TSPL_SCRAPSALE_HEAD.tax3_amt,0) as txt3amt, "
                        Qry1 += " tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SCRAPSALE_HEAD.tax4_rate,0) as txt4Rate,isnull (TSPL_SCRAPSALE_HEAD.tax4_amt,0) as txt4amt, "
                        Qry1 += " tax5.Tax_Code_Desc as tax5name,isnull (TSPL_SCRAPSALE_HEAD.tax5_rate,0) as txt5Rate,isnull (TSPL_SCRAPSALE_HEAD.tax5_amt,0) as txt5amt, "
                        Qry1 += "  tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SCRAPSALE_HEAD.tax6_rate,0) as txt6Rate,isnull (TSPL_SCRAPSALE_HEAD.tax6_amt,0) as txt6amt, "
                        Qry1 += "  tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SCRAPSALE_HEAD.tax7_rate,0) as txt7Rate,isnull (TSPL_SCRAPSALE_HEAD.tax7_amt,0) as txt7amt, "
                        Qry1 += " tax8.Tax_Code_Desc as tax8name,isnull (TSPL_SCRAPSALE_HEAD.tax8_rate,0) as txt8Rate,isnull (TSPL_SCRAPSALE_HEAD.tax8_amt,0) as txt8amt,  "
                        Qry1 += "  tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SCRAPSALE_HEAD.tax9_rate,0) as txt9Rate,isnull (TSPL_SCRAPSALE_HEAD.tax9_amt,0) as txt9amt,'' as cin,'' as pan, '' as companyaddress,"
                        Qry1 += " tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SCRAPSALE_HEAD.tax10_amt,0) as txt10amt ,TSPL_SCRAPSALE_HEAD.AddDesc1,isnull (TSPL_SCRAPSALE_HEAD.AddAmt1,0) as AddAmt1,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc2,isnull (TSPL_SCRAPSALE_HEAD.AddAmt2,0) as AddAmt2,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc3,isnull (TSPL_SCRAPSALE_HEAD.AddAmt3,0) as AddAmt3,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc4,isnull (TSPL_SCRAPSALE_HEAD.AddAmt4,0) as AddAmt4,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc5,isnull (TSPL_SCRAPSALE_HEAD.AddAmt5,0) as AddAmt5,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc6,isnull (TSPL_SCRAPSALE_HEAD.AddAmt6,0) as AddAmt6,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc7,isnull (TSPL_SCRAPSALE_HEAD.AddAmt7,0) as AddAmt7,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc8,isnull (TSPL_SCRAPSALE_HEAD.AddAmt8,0) as AddAmt8,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc9,isnull (TSPL_SCRAPSALE_HEAD.AddAmt9,0) as AddAmt9,"
                        Qry1 += " TSPL_SCRAPSALE_HEAD.AddDesc10,isnull (TSPL_SCRAPSALE_HEAD.AddAmt10,0) as AddAmt10,TSPL_SCRAPSALE_HEAD.vehicle_code,TSPL_SCRAPSALE_HEAD.Transport_code,TSPL_TRANSPORT_MASTER.Transporter_name,TSPL_SCRAPSALE_HEAD.Cust_code,TSPL_SCRAPSALE_HEAD.cust_name,tspl_customer_master.add1,tspl_customer_master.add2,tspl_customer_master.add3,tspl_city_master.city_name from TSPL_SCRAPSALE_HEAD left join TSPL_SCRAPSALE_DETAIL on TSPL_SCRAPSALE_DETAIL.shipment_No=TSPL_SCRAPSALE_HEAD.shipment_No left join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD. shipment_No=TSPL_SCRAPSALE_HEAD.shipment_No left join TSPL_LOCATION_MASTER as FromLocation on FromLocation.Location_Code=TSPL_SCRAPSALE_HEAD.Loc_Code left join TSPL_LOCATION_MASTER as ToLocation on ToLocation.Location_Code=TSPL_SCRAPSALE_HEAD.ToLoc_Code left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code left join TSPL_STATE_MASTER as  FromState on FromState.State_Code=FromLocation.State"
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
                        Qry1 += " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX10 left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_SCRAPSALE_HEAD.Transport_code  left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SCRAPSALE_HEAD.cust_code "
                        Qry1 += " left join tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code  where TSPL_SCRAPSALE_HEAD.shipment_No='" & txtDocNo.Value & "'"
                        Dim dt1 As DataTable
                        dt1 = clsDBFuncationality.GetDataTable(Qry1)
                        If isPrint Then
                            SetItemWiseTax(dt, InvoiceNo)
                            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Excisable")), "Y") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "VIZAG") = CompairStringResult.Equal Then
                                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "ScrapsaleInvoice4ExcisePrintfor VIZAG", "ScrapSaleInvoiceRpt")
                                Else
                                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "ScrapSaleInvoice4ExcisePrint", "ScrapSaleInvoiceRpt")
                                End If
                            Else
                                If (clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "001") = CompairStringResult.Equal) Then
                                    frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt1, clsERPFuncationality.CompanyAddresShowinFooter(), "ScrapSaleInvoice", "ScrapnSale Invoice", "rptCompanyAddress.rpt")
                                    'PurchaseOrderViewer.funreport(dt1, "ScrapSaleInvoice", "ScrapSaleInvoiceRpt")
                                End If
                            End If
                        Else
                            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.PaperSize10x12, "ScrapSaleInvoice4Excise", "ScrapSaleInvoiceRpt")
                        End If
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow("Please select one Invoice")
                End If
            End If
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    'PrintQry
    ' Ticket No : ERO/17/04/19-000562 By prabhakar
    ' Ticket No : ERO/17/04/19-000563 by prabhakar
    Public Function PrintQry() As String
        Dim strQuery As String = Nothing
        strQuery = " Select * from ( " &
                   " select '1' as  CopyType ,TSPL_SCRAPSALE_detail.itemRate,TSPL_SCRAPSALE_detail.tax1,TSPL_SCRAPSALE_detail.TAX1_Rate ,TSPL_SCRAPSALE_detail.tax1_Amt,TSPL_SCRAPSALE_detail.tax2,TSPL_SCRAPSALE_detail.TAX2_Rate ,TSPL_SCRAPSALE_detail.tax2_Amt,TSPL_SCRAPSALE_detail.tax3,TSPL_SCRAPSALE_detail.TAX3_Rate ,TSPL_SCRAPSALE_detail.tax3_Amt,TSPL_SCRAPSALE_detail.tax4,TSPL_SCRAPSALE_detail.TAX4_Rate ,TSPL_SCRAPSALE_detail.tax4_Amt, TSPL_SCRAPINVOICE_HEAD.shipment_No , convert(varchar,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) as shipment_Date,TSPL_SCRAPINVOICE_HEAD.Description as Shipment_Description, " &
                   " TSPL_SCRAPINVOICE_HEAD.invoice_No as STN_No,convert(varchar,TSPL_SCRAPINVOICE_HEAD.posting_Date,103) as Invoice_Date, " &
                   " TSPL_SCRAPSALE_HEAD.Loc_Code  as From_Location_Code ,  TSPL_LOCATION_MASTER.Location_Desc as From_Location_Dec,TSPL_LOCATION_MASTER.Add1 as From_Location_Add1 , TSPL_LOCATION_MASTER.Add2 as From_Location_Add2 , TSPL_LOCATION_MASTER.Add3 as From_Location_Add3,TSPL_LOCATION_MASTER.Add4  as From_Location_Add4,TSPL_LOCATION_MASTER.City_Code as From_Location_City_Code,TSPL_City_Master_From_Location.City_Name as From_Location_City_Name , TSPL_STATE_MASTER_LOCATION.GST_STATE_Code as From_Gst_StateCode,TSPL_LOCATION_MASTER.GSTNO as From_Location_GSTIN_No,TSPL_LOCATION_MASTER.Pin_Code as From_Location_Pin_Code,  " &
                   " TSPL_SCRAPSALE_HEAD.cust_Code  as To_Locaton_Code, tspl_customer_master.Customer_Name as To_Location_Desc , tspl_customer_master.Add1 as To_Location_Add1, tspl_customer_master.Add2 as To_Location_Add2,tspl_customer_master.Add3 as To_Location_Add3,tspl_customer_master.State as To_Location_State_code, TSPL_STATE_MASTER_Customer.STATE_NAME as To_Location_State_Name, tspl_customer_master.GSTNO as To_Location_GSTNO , tspl_customer_master.PAN as To_Location_PAN, tspl_customer_master.Tin_No as To_Location_Tin, tspl_customer_master.Phone1 as To_Location_Phone1, tspl_customer_master.Phone2 as To_Location_Pone2, tspl_customer_master.Fax as To_Location_Fax, tspl_customer_master.Email as To_Location_Email, tspl_customer_master.City_Code as To_Location_City_Code,TSPL_City_Master_To_Location.City_Name as To_Location_City_Name, TSPL_STATE_MASTER_Customer.GST_STATE_Code as To_Location_GST_StateCode, " &
                   " TSPL_SCRAPINVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.HSN_Code,(CASE WHEN TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN TSPL_BATCH_ITEM.Qty ELSE TSPL_SCRAPINVOICE_DETAIL.shipped_Qty END )as Shipped_Qty,TSPL_SCRAPINVOICE_DETAIL.Line_No,TSPL_SCRAPINVOICE_DETAIL.FAT,TSPL_SCRAPINVOICE_DETAIL.SNF,TSPL_SCRAPINVOICE_DETAIL.Unit_Code, TSPL_SCRAPINVOICE_DETAIL.Price, TSPL_SCRAPINVOICE_DETAIL.ItemAmt, TSPL_SCRAPSALE_HEAD.Invoice_Type, TSPL_SCRAPSALE_HEAD.Total_Tax_Amt, TSPL_SCRAPSALE_HEAD.Doc_Amt, TSPL_LOCATION_MASTER.Loc_Short_Name,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Add1 as Company_Add1, TSPL_COMPANY_MASTER.add2 as Company_Add2, TSPL_COMPANY_MASTER.Add3 as Company_Add3,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,TSPL_COMPANY_MASTER.Tcan_No AS Website,TSPL_COMPANY_MASTER.Access_Officer as FSSAI_NO,TSPL_COMPANY_MASTER.Email AS Comp_Email,TSPL_COMPANY_MASTER.CINNo as CORP_NO,TSPL_COMPANY_MASTER.GSTReg_No,TSPL_STATE_MASTER_Company.GST_STATE_Code as Company_GST_STATE_Code,TSPL_COMPANY_MASTER.Pan_No AS Comp_PanNo,TSPL_COMPANY_MASTER.Tin_No as Comp_TinNo,CONVERT(VARCHAR(15),TSPL_COMPANY_MASTER.TinNo_Issue_Date,103) AS TinNo_Issue_Date ,CONVERT(VARCHAR(15),TSPL_COMPANY_MASTER.PanNo_Issue_Date,103) AS PanNo_Issue_Date ,TSPL_BATCH_ITEM.Batch_No,TSPL_SCRAPINVOICE_HEAD.Discount_Amt,TSPL_SCRAPINVOICE_HEAD.Balance_Amt,TSPL_SCRAPINVOICE_HEAD.Amount_Less_Discount,TSPL_SCRAPINVOICE_HEAD.ship_Total_Amt, TSPL_SCRAPSALE_HEAD.vehicle_code,TSPL_SCRAPSALE_HEAD.Transport_code,TSPL_TRANSPORT_MASTER.Transporter_name  " &
                   " ,tspl_ship_To_location.Ship_To_Code,tspl_ship_To_location.Ship_To_Desc , tspl_ship_To_location.Add1 as Ship_To_Add1, tspl_ship_To_location.Add2 as Ship_To_Add2,tspl_ship_To_location.Add3 as Ship_To_Add3, tspl_ship_To_location.Add4 as Ship_To_Add4 ,TSPL_SCRAPSALE_HEAD.Reff as Reff_As_Transpoter_Name " &
                   " from TSPL_SCRAPSALE_HEAD  " &
                   " left join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD. shipment_No=TSPL_SCRAPSALE_HEAD.shipment_No  " &
                   " left join TSPL_SCRAPINVOICE_DETAIL on TSPL_SCRAPINVOICE_DETAIL.invoice_No=TSPL_SCRAPINVOICE_HEAD.invoice_No  " &
                   " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code  " &
                   " left join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPSALE_HEAD.Loc_Code  " &
                   " left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SCRAPSALE_HEAD.cust_code  " &
                   " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_SCRAPSALE_HEAD.Transport_code   " &
                   " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SCRAPSALE_HEAD.Comp_Code  " &
                   " left outer join TSPL_City_Master as TSPL_City_Master_From_Location on TSPL_City_Master_From_Location.City_Code = TSPL_LOCATION_MASTER.City_Code " &
                   " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_LOCATION  on TSPL_STATE_MASTER_LOCATION.STATE_CODE=TSPL_LOCATION_MASTER.State  " &
                   " left outer join TSPL_City_Master as TSPL_City_Master_To_Location on TSPL_City_Master_To_Location.City_Code = tspl_customer_master.City_Code " &
                   " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_Customer  on TSPL_STATE_MASTER_Customer.STATE_CODE=tspl_customer_master.State  " &
                   " LEFT JOIN TSPL_BATCH_ITEM  ON TSPL_BATCH_ITEM.Document_Code=TSPL_SCRAPINVOICE_DETAIL.invoice_No AND TSPL_BATCH_ITEM.Parent_Line_No=TSPL_SCRAPINVOICE_DETAIL.Line_No AND TSPL_BATCH_ITEM.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code AND TSPL_BATCH_ITEM.In_Out_Type='O' " &
                   " Left outer Join tspl_ship_To_location on tspl_ship_To_location.Ship_To_Type_Code = TSPL_SCRAPSALE_HEAD.cust_code and tspl_ship_To_location.Ship_To_Code = TSPL_SCRAPSALE_HEAD.ToLoc_Code " &
                   " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_Company on  TSPL_STATE_MASTER_Company.STATE_CODE = TSPL_COMPANY_MASTER.STATE " &
                   " left outer join tspl_scrapsale_detail on tspl_scrapsale_detail. shipment_No=TSPL_SCRAPSALE_HEAD.shipment_No AND tspl_scrapsale_detail.Line_No=TSPL_SCRAPINVOICE_DETAIL.Line_No AND tspl_scrapsale_detail.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code " &
                   "  WHERE TSPL_SCRAPSALE_HEAD.shipment_No= '" & txtDocNo.Value & "' " &
                   "  )XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL: For Buyer' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE: For Transporter' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE: For Seller' as CopyType1  ) YYY ON YYY.COL1=XXX.CopyType  ORDER BY YYY.COL2,Line_No "
        Return strQuery
    End Function
    Public Function PrintMaterialSale() As String
        Dim strQuery As String = Nothing
        strQuery = "select   TSPL_SCRAPINVOICE_HEAD.Total_Gross_Weight,TSPL_SCRAPINVOICE_HEAD.Total_Net_Weight, convert(varchar,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) as shipment_Date, tspl_customer_master.PAN as Cust_Pan, TSPL_SCRAPINVOICE_HEAD.Description,FromLocation.HOAdd1 as frmHO1,FromLocation.HOAdd2 as frmHO2,ToLocation.HOAdd1 as to_HO1,ToLocation.HOAdd2 as to_HO2,TSPL_SCRAPINVOICE_HEAD.invoice_No,convert(varchar,TSPL_SCRAPINVOICE_HEAD.posting_Date,103) as Invoice_Date,TSPL_SCRAPSALE_HEAD.Loc_Code as From_Location,FromLocation.Location_Desc as [From Location Desc],(FromLocation.Add1+FromLocation.Add2+FromLocation.Add3+FromLocation.Add4)as [From Address] ,FromLocation.GSTNO as FromGSTNo,FromLocation.Pin_Code,FromLocation.TIN_No,FromLocation.CST_No,FromLocation.State as From_State,FromState.State_Name as frm_State_name  ,TSPL_SCRAPSALE_HEAD.ToLoc_Code as To_Location,ToLocation.Location_Desc as To_Location_Desc,(ToLocation.Add1+ToLocation.Add2+ToLocation.Add3+ToLocation.Add4)as [To Address],tspl_customer_master.Pin_Code as [To Pin Code],tspl_customer_master.TIN_No as [To TIN No],tspl_customer_master.CST as [To CST No],tspl_customer_master.Phone1 as [To phone],ToLocation.State as To_State,ToState.State_Name as To_state_name,TSPL_SCRAPINVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.HSN_Code,(CASE WHEN TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN TSPL_BATCH_ITEM.Qty ELSE TSPL_SCRAPINVOICE_DETAIL.shipped_Qty END )as Shipped_Qty,TSPL_SCRAPINVOICE_DETAIL.FAT,TSPL_SCRAPINVOICE_DETAIL.SNF," &
        "TSPL_SCRAPINVOICE_DETAIL.Unit_Code, TSPL_SCRAPINVOICE_DETAIL.Price, TSPL_SCRAPINVOICE_DETAIL.ItemAmt, TSPL_SCRAPSALE_HEAD.Invoice_Type, TSPL_SCRAPSALE_HEAD.Total_Tax_Amt, TSPL_SCRAPSALE_HEAD.Doc_Amt, " &
        "tspl_customer_master.Customer_Name,tspl_customer_master.Add1,tspl_customer_master.Add2,tspl_customer_master.Add3,tspl_customer_master.State,tspl_customer_master.Cust_Code,tspl_customer_master.CST,tspl_customer_master.GSTNO as Cust_GSTNo,tspl_customer_master.Lst_No,FromLocation.Loc_Short_Name," &
        "TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,TSPL_COMPANY_MASTER.Tcan_No AS Website,TSPL_COMPANY_MASTER.Access_Officer as FSSAI_NO,TSPL_COMPANY_MASTER.Email AS Comp_Email,TSPL_COMPANY_MASTER.CINNo as CORP_NO,TSPL_COMPANY_MASTER.Pan_No AS Comp_PanNo,TSPL_COMPANY_MASTER.Tin_No as Comp_TinNo,CONVERT(VARCHAR(15),TSPL_COMPANY_MASTER.TinNo_Issue_Date,103) AS TinNo_Issue_Date ,CONVERT(VARCHAR(15),TSPL_COMPANY_MASTER.PanNo_Issue_Date,103) AS PanNo_Issue_Date ," &
        "TSPL_BATCH_ITEM.Batch_No,TSPL_SCRAPINVOICE_HEAD.Discount_Amt,TSPL_SCRAPINVOICE_HEAD.Balance_Amt,TSPL_SCRAPINVOICE_HEAD.Amount_Less_Discount,TSPL_SCRAPINVOICE_HEAD.ship_Total_Amt, " &
        "TAX2= REPLACE((CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX1!='' AND tax1.Type!='E' THEN(tax1.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX1_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX1_Amt)) ELSE '' END)+' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX2!='' AND tax2.Type!='E' THEN(tax2.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX2_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX2_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX3!='' AND tax3.Type!='E' THEN(tax3.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX3_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX3_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX4!='' AND tax4.Type!='E' THEN(tax4.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX4_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX4_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX5!='' AND tax5.Type!='E' THEN(tax5.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX5_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX5_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX6!='' AND tax6.Type!='E' THEN(tax6.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX6_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX6_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX7!='' AND tax7.Type!='E' THEN(tax7.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX7_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX7_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX8!='' AND tax8.Type!='E' THEN(tax8.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX8_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX8_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX9!='' AND tax9.Type!='E' THEN(tax9.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX9_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX9_Amt))ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.TAX10!='' AND tax10.Type!='E' THEN(tax10.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX10_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_SCRAPINVOICE_HEAD.TAX10_Amt))ELSE '' END),CHAR(13),'')," &
        " Add_Charge_Name1=(CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc1!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc1+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt1)) ELSE '' END)+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc2!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc2+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt2)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc3!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc3+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt3)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc4!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc4+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt4)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc5!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc5+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt5)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc6!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc6+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt6)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc7!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc7+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt7)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc8!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc8+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt8)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc9!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc9+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt9)) ELSE '' END) +' '+CHAR(13)+ (CASE WHEN TSPL_SCRAPINVOICE_HEAD.AddDesc10!='' THEN (TSPL_SCRAPINVOICE_HEAD.AddDesc10+' : '+CONVERT(VARCHAR(10),TSPL_SCRAPINVOICE_HEAD.AddAmt10)) ELSE '' END)," &
"	TAX1 .Tax_Code_Desc as tax1name,isnull (TSPL_SCRAPSALE_HEAD.tax1_rate,0) as txt1Rate,isnull (TSPL_SCRAPSALE_HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SCRAPSALE_HEAD.tax2_rate,0) as txt2Rate,isnull (TSPL_SCRAPSALE_HEAD.tax2_amt,0) as txt2amt, tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SCRAPSALE_HEAD.tax3_rate,0) as txt3Rate,isnull (TSPL_SCRAPSALE_HEAD.tax3_amt,0) as txt3amt,  tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SCRAPSALE_HEAD.tax4_rate,0) as txt4Rate,isnull (TSPL_SCRAPSALE_HEAD.tax4_amt,0) as txt4amt,  tax5.Tax_Code_Desc as tax5name,isnull (TSPL_SCRAPSALE_HEAD.tax5_rate,0) as txt5Rate,isnull (TSPL_SCRAPSALE_HEAD.tax5_amt,0) as txt5amt,   tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SCRAPSALE_HEAD.tax6_rate,0) as txt6Rate,isnull (TSPL_SCRAPSALE_HEAD.tax6_amt,0) as txt6amt,   tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SCRAPSALE_HEAD.tax7_rate,0) as txt7Rate,isnull (TSPL_SCRAPSALE_HEAD.tax7_amt,0) as txt7amt,  tax8.Tax_Code_Desc as tax8name,isnull (TSPL_SCRAPSALE_HEAD.tax8_rate,0) as txt8Rate,isnull (TSPL_SCRAPSALE_HEAD.tax8_amt,0) as txt8amt,    tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SCRAPSALE_HEAD.tax9_rate,0) as txt9Rate,isnull (TSPL_SCRAPSALE_HEAD.tax9_amt,0) as txt9amt,'' as cin,'' as pan, '' as companyaddress, tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SCRAPSALE_HEAD.tax10_amt,0) as txt10amt ,TSPL_SCRAPSALE_HEAD.AddDesc1,isnull (TSPL_SCRAPSALE_HEAD.AddAmt1,0) as AddAmt1, TSPL_SCRAPSALE_HEAD.AddDesc2,isnull (TSPL_SCRAPSALE_HEAD.AddAmt2,0) as AddAmt2, TSPL_SCRAPSALE_HEAD.AddDesc3,isnull (TSPL_SCRAPSALE_HEAD.AddAmt3,0) as AddAmt3, TSPL_SCRAPSALE_HEAD.AddDesc4,isnull (TSPL_SCRAPSALE_HEAD.AddAmt4,0) as AddAmt4, TSPL_SCRAPSALE_HEAD.AddDesc5,isnull (TSPL_SCRAPSALE_HEAD.AddAmt5,0) as AddAmt5, TSPL_SCRAPSALE_HEAD.AddDesc6,isnull (TSPL_SCRAPSALE_HEAD.AddAmt6,0) as AddAmt6, TSPL_SCRAPSALE_HEAD.AddDesc7,isnull (TSPL_SCRAPSALE_HEAD.AddAmt7,0) as AddAmt7, TSPL_SCRAPSALE_HEAD.AddDesc8,isnull (TSPL_SCRAPSALE_HEAD.AddAmt8,0) as AddAmt8, TSPL_SCRAPSALE_HEAD.AddDesc9,isnull (TSPL_SCRAPSALE_HEAD.AddAmt9,0) as AddAmt9, TSPL_SCRAPSALE_HEAD.AddDesc10,isnull (TSPL_SCRAPSALE_HEAD.AddAmt10,0) as AddAmt10,TSPL_SCRAPSALE_HEAD.vehicle_code,TSPL_SCRAPSALE_HEAD.Transport_code,TSPL_TRANSPORT_MASTER.Transporter_name,TSPL_SCRAPSALE_HEAD.cust_name,tspl_customer_master.add1,tspl_customer_master.add2,tspl_customer_master.add3,tspl_city_master.city_name from " &
        "TSPL_SCRAPSALE_HEAD  " &
 "left join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD. shipment_No=TSPL_SCRAPSALE_HEAD.shipment_No " &
 "left join TSPL_SCRAPINVOICE_DETAIL on TSPL_SCRAPINVOICE_DETAIL.invoice_No=TSPL_SCRAPINVOICE_HEAD.invoice_No " &
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
       "left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX10 " &
       "left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_SCRAPSALE_HEAD.Transport_code  " &
       "left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SCRAPSALE_HEAD.cust_code " &
       "left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SCRAPSALE_HEAD.Comp_Code " &
        "left join tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code  " &
        "LEFT JOIN TSPL_BATCH_ITEM  ON TSPL_BATCH_ITEM.Document_Code=TSPL_SCRAPINVOICE_DETAIL.invoice_No AND " &
        "TSPL_BATCH_ITEM.Parent_Line_No=TSPL_SCRAPINVOICE_DETAIL.Line_No AND TSPL_BATCH_ITEM.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code AND TSPL_BATCH_ITEM.In_Out_Type='O' WHERE TSPL_SCRAPSALE_HEAD.shipment_No='" & txtDocNo.Value & "' "
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub TxtFinder1__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtnrg._MYValidating
        Dim qry As String = "Select RGP_No as [Code], RGP_Date as [NRGP Date] from TSPL_RGP_HEAD "
        txtnrg.Value = clsCommon.ShowSelectForm("NRGP_FND1", qry, "Code", "RGP_No NOT IN (Select Distinct NRG_No from TSPL_SCRAPSALE_HEAD) AND Status=1 and Doc_Type='NRGP' AND Against_Sale=1", txtnrg.Value, "Code", isButtonClicked)
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
                        txtdescription.Text = clsCommon.myCstr(dr("Reason"))
                        txtref.Text = clsCommon.myCstr(dr("Remarks"))
                        TxtVehicleCode.Value = clsCommon.myCstr(dr("Vehicle_Id"))
                        txtVehicleDesc.Text = clsCommon.myCstr(dr("Description"))
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
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
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
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then


                '' REASON FOR Reverse 
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select shipment_Date,Loc_Code from TSPL_SCRAPSALE_HEAD where shipment_No='" + txtDocNo.Value + "'")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.JobWorkDispatchProduction, clsCommon.myCstr(dt.Rows()("Loc_Code")), clsCommon.myCDate(dt.Rows()("shipment_Date")), Nothing)

                End If
                If ClsScrapInvoiceHead.ReverseAndUnpost(txtDocNo.Value, lblInvoiceNo.Text) Then
                    saveCancelLog(Reason, "Reverse And Recreate", Nothing)
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub txtTransporter_Code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtTransporter_Code._MYValidating
        Dim Qry As String = "select Transport_Id as Code,Transporter_Name as Description,City,State,Pincode,Phone from TSPL_TRANSPORT_MASTER"
        txtTransporter_Code.Value = clsCommon.ShowSelectForm("TRANSPnsfer_KDIL1", Qry, "Code", "", txtTransporter_Code.Value, "Code", isButtonClicked)
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
                clsCommon.MyMessageBoxShow(strBatchunion, Me.Text)
            End If
        End If
    End Sub
    Sub OpenBatchItem()
        Dim TransType_Str As String = ""
        Dim blnBatchqty As Boolean = False
        If RunBatchFifowise = 0 Then
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
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                End If
            End If
        Else
            ' fifo start

            TransType_Str = TransType_Str & "-SH"
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                    If clsCommon.myCBool(clsDBFuncationality.getSingleValue("select TSPL_ITEM_MASTER.Is_Batch_Item  from TSPL_ITEM_MASTER where TSPL_ITEM_MASTER.Item_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "'", Nothing)) Then
                        Dim strBatchunion As String = ""
                        If RunBatchFifowise = 1 Then
                            If ii > 0 Then
                                Dim strICodeOuter As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                                For jj As Integer = 0 To ii - 1
                                    Dim strICodeInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                                    If clsCommon.CompairString(strICodeOuter, strICodeInner) = CompairStringResult.Equal Then
                                        Dim arr As List(Of clsBatchInventory) = Nothing
                                        arr = TryCast(gv1.Rows(jj).Cells(colICode).Tag, List(Of clsBatchInventory))
                                        For Each obj As clsBatchInventory In arr
                                            strBatchunion += " union all select '" & clsCommon.myCstr(obj.Batch_No) & "' as Batch_No, " & _
                                                "'" & clsCommon.myCstr(obj.Manual_BatchNo) & "' as Manual_BatchNo,'O' as In_Out_Type, " & _
                                                "'" & clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value) & "' as OrgUOM," & obj.Qty & " as OrgQty,0 as OrgMRP, " & _
                                                "'" & clsCommon.GetPrintDate(obj.Expiry_Date, "dd/MMM/yyyy") & "' as Expiry_Date, " & _
                                                "'" & clsCommon.GetPrintDate(obj.Manufacture_Date, "dd/MMM/yyyy") & "' as Manufacture_Date, " & _
                                                "" & obj.Qty & " as Qty, 0 as MRP "
                                        Next

                                    End If
                                Next
                            End If
                            gv1.CurrentRow = gv1.Rows(ii)

                            Dim frm As frmBatchItemOut = New frmBatchItemOut()
                            frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                            frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                            frm.strLocationCode = fndLocation.Value
                            frm.strCurrDocNo = txtDocNo.Value
                            frm.strCurrDocType = TransType_Str
                            '"PS-SH"
                            frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                            frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                            frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))

                            If frm.OpenSerialList(0, "", strBatchunion) Then
                                gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                                blnBatchqty = True
                            Else
                                Dim batchQty As Double = 0
                                For Each obj As clsBatchInventory In frm.arr
                                    batchQty += obj.Qty
                                Next
                                clsCommon.MyMessageBoxShow("Please increase stock Item Code - " & frm.strItemCode & " , Entered Qty - " & clsCommon.myCstr(frm.dblqty) & " Batch Qty - " & clsCommon.myCstr(batchQty), Me.Text)
                                blnBatchqty = False
                                Exit Sub
                            End If

                        End If
                    End If
                End If
            Next
        End If

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Print(True)
    End Sub
    ' Ticket No : TEC/29/10/18-000354 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(lblInvoiceNo.Text)
    End Sub
    ' Ticket No : GKD/15/02/19-000176 by Prabhakar - Add Print Invoice Button for GK
    Private Sub btnInvoicePrint_Click(sender As Object, e As EventArgs) Handles btnInvoicePrint.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Document is not found.", Me.Text)
                Return
            End If
            Dim frmCRV As New frmCrystalReportViewer()
            Dim qry As String = " Select TSPL_CUSTOMER_MASTER.GSTNO , TSPL_SCRAPSALE_HEAD.Transport_code ,TSPL_TRANSPORT_MASTER.Transporter_Name,  TSPL_Vehicle_Master.Vehicle_ID, TSPL_Vehicle_Master.Number, TSPL_SCRAPSALE_HEAD.Vehicle_Id, isnull (TSPL_SCRAPSALE_HEAD.EWayBillNo,'') as   EWayBillNo,isnull (convert (varchar,TSPL_SCRAPSALE_HEAD.EWayBillDate,103),'') as EWayBillDate ,isnull(TSPL_SCRAPSALE_HEAD.Electronic_Ref_No,'') as Electronic_Ref_No, TSPL_SCRAPSALE_HEAD.Terms_Code, TSPL_SCRAPINVOICE_HEAD.invoice_No as invoiceNo,TSPL_SCRAPINVOICE_HEAD.Shipment_Date as invoiceDate,TSPL_SCRAPSALE_HEAD.Shipment_No as ChalanNo,TSPL_SCRAPSALE_HEAD.Shipment_Date as Chalan_Date, TSPL_CUSTOMER_MASTER  .Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3, Customer_State.GST_STATE_Code as Cust_GST_SATE_CODE,TSPL_CUSTOMER_MASTER.PAN  as Customer_Pan, TSPL_COMPANY_MASTER.Comp_Name as CompName,tspl_Company_Master.Logo_Img , tspl_Company_Master.Logo_Img2,TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code ,TSPL_LOCATION_MASTER.Email as Loc_Email,Location_State.gst_state_code as Loc_GST_StateCode,tspl_location_master.gstno as LocGstNo,TSPL_LOCATION_MASTER.Add3  as Loc_Add3, TSPL_SCRAPSALE_DETAIL .Line_No , TSPL_SCRAPSALE_HEAD.Shipment_No,TSPL_SCRAPSALE_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc as itemdesc,TSPL_ITEM_MASTER.HSN_Code , TSPL_SCRAPSALE_DETAIL.Unit_Code as uom,TSPL_SCRAPSALE_DETAIL.Shipped_Qty as Qty, TSPL_SCRAPSALE_DETAIL.Price as itemcost,TSPL_SCRAPSALE_DETAIL.NetPriceAmt as Amount,TSPL_SCRAPSALE_DETAIL.TotalDiscountAmt as disc_Amt, (isnull(TSPL_SCRAPSALE_DETAIL.NetPriceAmt,0) - isnull(TSPL_SCRAPSALE_DETAIL.TotalDiscountAmt,0)) as TaxableValue, TotalTaxamt,TotalAmt  , " & _
                                " tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SCRAPSALE_HEAD.tax1_amt,0) as txt1amt, tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SCRAPSALE_HEAD.tax2_amt,0) as txt2amt, tax3.Tax_Code_Desc as tax3name,  isnull (TSPL_SCRAPSALE_HEAD.tax3_amt,0) as txt3amt, tax4.Tax_Code_Desc as tax4name, isnull (TSPL_SCRAPSALE_HEAD.tax4_amt,0) as txt4amt,  tax5.Tax_Code_Desc as tax5name, isnull (TSPL_SCRAPSALE_HEAD.tax5_amt,0) as txt5amt,  tax6.Tax_Code_Desc as tax6name, isnull (TSPL_SCRAPSALE_HEAD.tax6_amt,0) as txt6amt,tax7.Tax_Code_Desc as tax7name, isnull (TSPL_SCRAPSALE_HEAD.tax7_amt,0) as txt7amt, tax8.Tax_Code_Desc as tax8name, isnull (TSPL_SCRAPSALE_HEAD.tax8_amt,0) as txt8amt,  tax9.Tax_Code_Desc as tax9name, isnull (TSPL_SCRAPSALE_HEAD.tax9_amt,0) as txt9amt,   tax10.Tax_Code_Desc as tax10name, isnull (TSPL_SCRAPSALE_HEAD.tax10_amt,0) as txt10amt,  TSPL_SCRAPSALE_HEAD.TAX1_Rate ,TSPL_SCRAPSALE_HEAD.TAX2_Rate ,TSPL_SCRAPSALE_HEAD.TAX3_Rate,TSPL_SCRAPSALE_HEAD.TAX4_Rate,TSPL_SCRAPSALE_HEAD.TAX5_Rate,TSPL_SCRAPSALE_HEAD.TAX6_Rate,TSPL_SCRAPSALE_HEAD.TAX7_Rate,TSPL_SCRAPSALE_HEAD.TAX8_Rate,TSPL_SCRAPSALE_HEAD.TAX9_Rate,TSPL_SCRAPSALE_HEAD.TAX10_Rate, " & _
                                " TSPL_SCRAPSALE_DETAIL.TAX1 as dTAX1, TSPL_SCRAPSALE_DETAIL.TAX2 as dTAX2, TSPL_SCRAPSALE_DETAIL.TAX3 as  dTAX3, TSPL_SCRAPSALE_DETAIL.TAX4 as  dTAX4, TSPL_SCRAPSALE_DETAIL.TAX5 as  dTAX5, TSPL_SCRAPSALE_DETAIL.TAX6 as  dTAX6, TSPL_SCRAPSALE_DETAIL.TAX7 as  dTAX7, TSPL_SCRAPSALE_DETAIL.TAX8 as dTAX8, TSPL_SCRAPSALE_DETAIL.TAX9 as dTAX9, TSPL_SCRAPSALE_DETAIL.TAX10 as  dTAX10, TSPL_SCRAPSALE_DETAIL.TAX1_Amt, TSPL_SCRAPSALE_DETAIL.TAX2_Amt, TSPL_SCRAPSALE_DETAIL.TAX3_Amt, TSPL_SCRAPSALE_DETAIL.TAX4_Amt, TSPL_SCRAPSALE_DETAIL.TAX5_Amt, TSPL_SCRAPSALE_DETAIL.TAX6_Amt, TSPL_SCRAPSALE_DETAIL.TAX7_Amt, TSPL_SCRAPSALE_DETAIL.TAX8_Amt, TSPL_SCRAPSALE_DETAIL.TAX9_Amt, TSPL_SCRAPSALE_DETAIL.TAX10_Amt, " & _
                                " TSPL_SCRAPSALE_DETAIL.TAX1_Rate as dTAX1_Rate, TSPL_SCRAPSALE_DETAIL.TAX2_Rate as dTAX2_Rate, TSPL_SCRAPSALE_DETAIL.TAX3_Rate as dTAX3_Rate ,TSPL_SCRAPSALE_DETAIL.TAX4_Rate as dTAX4_Rate ,TSPL_SCRAPSALE_DETAIL.TAX5_Rate as dTAX5_Rate  ,TSPL_SCRAPSALE_DETAIL.TAX6_Rate as dTAX6_Rate ,TSPL_SCRAPSALE_DETAIL.TAX7_Rate as dTAX7_Rate ,TSPL_SCRAPSALE_DETAIL.TAX8_Rate as dTAX8_Rate ,TSPL_SCRAPSALE_DETAIL.TAX9_Rate as dTAX9_Rate ,TSPL_SCRAPSALE_DETAIL.TAX10_Rate as dTAX10_Rate,  dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type " & _
                                " from TSPL_SCRAPSALE_DETAIL INNER  JOIN TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.Shipment_No = TSPL_SCRAPSALE_DETAIL.Shipment_No " & _
                                " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SCRAPSALE_HEAD.tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SCRAPSALE_HEAD.tax2    left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SCRAPSALE_HEAD .TAX3   left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SCRAPSALE_HEAD .tax4  left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SCRAPSALE_HEAD .tax5   left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX6  left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX7    left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX8  left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX9    left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SCRAPSALE_HEAD .TAX10  " & _
                                " left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_SCRAPSALE_DETAIL .tax1 left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_SCRAPSALE_DETAIL.tax2     left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_SCRAPSALE_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as dtax4 on tax4.Tax_Code= TSPL_SCRAPSALE_DETAIL .tax4  " & _
                                " left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_SCRAPSALE_DETAIL .tax5   left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX6    " & _
                                " left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX7    left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX8  " & _
                                " left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX9    left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_SCRAPSALE_DETAIL .TAX10 Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE = TSPL_SCRAPSALE_DETAIL.Item_Code " & _
                                " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SCRAPSALE_HEAD.comp_code " & _
                                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code  = TSPL_SCRAPSALE_HEAD.Loc_Code " & _
                                " left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state " & _
                                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SCRAPSALE_HEAD.Cust_Code  " & _
                                " left outer join tspl_state_master as Customer_State on Customer_State.state_code=tspl_customer_Master.state " & _
                                " left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.Shipment_No = TSPL_SCRAPSALE_HEAD.Shipment_No  " & _
                                " left outer join TSPL_Vehicle_Master on TSPL_Vehicle_Master.Vehicle_ID =  TSPL_SCRAPSALE_HEAD.Vehicle_ID  " & _
                                " left outer join TSPL_TRANSPORT_MASTER on TSPL_SCRAPSALE_HEAD.Transport_code = TSPL_TRANSPORT_MASTER.Transport_Id " & _
                                " where TSPL_SCRAPSALE_HEAD.Shipment_No = '" + txtDocNo.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim isCGST As Boolean = False
                Dim isSGST As Boolean = False
                Dim isIGST As Boolean = False
                Dim isExcisable As Boolean = False
                Dim i As Integer
                For i = 1 To 10
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("tax" + clsCommon.myCstr(i) + "Type")), "CGST") = CompairStringResult.Equal Then
                        isCGST = True
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("tax" + clsCommon.myCstr(i) + "Type")), "SGST") = CompairStringResult.Equal Then
                        isSGST = True
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("tax" + clsCommon.myCstr(i) + "Type")), "IGST") = CompairStringResult.Equal Then
                        isIGST = True
                    End If
                    'If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("tax" + clsCommon.myCstr(i) + "Type")), "O") = CompairStringResult.Equal Then
                    '    isExcisable = True
                    'End If
                Next

                If isCGST = True AndAlso isSGST = True Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptJobworkDispatchSGST_CGST", "Job Work Dispatch", clsCommon.myCDate(dt.Rows(0)("Chalan_Date")), "rptCompanyAddress.rpt", "MMM.rpt", Nothing, )
                ElseIf isIGST = True Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptJobworkDispatchIGST", "Job Work Dispatch", clsCommon.myCDate(dt.Rows(0)("Chalan_Date")), "rptCompanyAddress.rpt", "MMM.rpt", Nothing, )
                    'ElseIf isExcisable = True Then
                Else
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptJobworkDispatchExcisable", "Job Work Dispatch", clsCommon.myCDate(dt.Rows(0)("Chalan_Date")), "rptCompanyAddress.rpt", "MMM.rpt", Nothing, )
                End If
            End If



        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document Code")
                Exit Sub
            End If
            clsERPFuncationality.ShowTransHistoryData(txtDocNo.Value, "shipment_No", "TSPL_SCRAPSALE_HEAD", "TSPL_SCRAPSALE_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class

