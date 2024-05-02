Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
'-----preeti gupta--ticket no.-BM00000003015
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports common
Imports System.IO
Imports XpertERPEngine




Public Class frmAssetScrapSale
    Inherits FrmMainTranScreen

#Region "Variables"
    Const ReportID As String = "AssetScrapSaleGrid"
    Private isCellValueChangedOpenAdd As Boolean = False
    Public strShipmentno As String = Nothing
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedTaxOpen As Boolean = False

    Const colLineNo As String = "COLLNO"
    Const colAssetCode As String = "colAssetCode"
    Const colAssetName As String = "colAssetName"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colHSN As String = "COLHSN"
    Const colQty As String = "shippedQty"
    Const colUnit As String = "COLUNIT"
    Const colRate As String = "price"
    Const colAmt As String = "itemamt"
    Const colDisPer As String = "discountper"
    Const colDisAmt As String = "discountamt "
    Const colAmtAfterDiscount As String = "totaxamt"
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
    Const colTaxAmt As String = "TotTaxAmt"
    Const colAmtAfterTax As String = "AmtAfterTax"

    Const colTTaxAutCode As String = "TTaxAutCode"
    Const colTTaxAutName As String = "TTaxAutName"
    Const colTBaseAmt As String = "TBaseAmt"
    Const colTTaxRate As String = "TTaxRate"
    Const colTTaxAmt As String = "TTaxAmt"

    Dim ButtonToolTip As ToolTip = New ToolTip()

    Const coladdcode As String = "COLADDCODE"
    Const coladddesc As String = "COLADDDESC"
    Const coladdamt As String = "COLADDAMT"
    Dim FlagDocumentIsTaxable As Integer = 0
    Dim EInvoiceType As String = ""
    Public objin As scrapinvoicehead
    Dim ApplyFinancialCostCenter As Boolean = False
    Const colHierarchyCode As String = "colHierarchyCode"
    Const colHierarchyName As String = "colHierarchyName"
    Const colHierarchyLevelNumber As String = "colHierarchyLevelNumber"
    Const colCostCenterCode As String = "colCostCenterCode"
    Const colCostCenterName As String = "colCostCenterName"
    Dim CostCenterAndHirerachyCodeUpdateAfterPost As Boolean = False
#End Region

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FADisposalEntry)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnCancel.Visible = MyBase.isCancel_Flag_After_Posting
        btnPrintReport.Visible = MyBase.isPrintFlag
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        btnReverse.Visible = False
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        fndcustNo.MendatroryField = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A Create Additional Cost")
        'ButtonToolTip.SetToolTip(btnRequistionItems, "Press Ctrl+F7 for Select Purchase Requistion Items")
        RadPageView1.SelectedPage = RadPageViewPage1
        ApplyFinancialCostCenter = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyFinancialCostCenter, clsFixedParameterCode.ApplyFinancialCostCenter, Nothing)) = "1", True, False))
        CostCenterAndHirerachyCodeUpdateAfterPost = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CostCenterAndHirerachyCodeUpdateAfterPost, clsFixedParameterCode.CostCenterAndHirerachyCodeUpdateAfterPost, Nothing)) = 1, True, False)

        LoadBlankGrid()
        LoadBlankGridTax()
        AddNew()
        If clsCommon.myLen(strShipmentno) > 0 Then
            LoadData(strShipmentno, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        ReStoreGridLayout()
        SetLength()
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
        FlagDocumentIsTaxable = 0
        txtDocNo.Value = ""
        fndcustNo.Value = ""
        fndcustNo.Value = ""
        txtcustdesc.Text = ""
        dtpshipment.Value = clsCommon.GETSERVERDATE()
        dtppost.Value = clsCommon.GETSERVERDATE()
        txtdescription.Text = ""
        txtref.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        txtaddamt.Text = ""
        fndLocation.Value = ""
        gvadd.DataSource = Nothing
        gvadd.Rows.Clear()
        txtaddamt.Text = ""
        'txttotaladdamt.Text = ""
        lblAmtWithDiscount.Text = ""
        lblDiscountAmt.Text = ""
        lblAmtAfterDiscount.Text = ""
        lblTaxAmt.Text = ""

        lbladdcharges.Text = ""
        lbldocamt.Text = ""
        txtlocation.Text = ""
        UsLock1.Status = 0
        dtppost.Value = clsCommon.GETSERVERDATE()
        dtpshipment.Value = clsCommon.GETSERVERDATE()
        rbtnTaxCalAutomatic.IsChecked = True
        'fndcustNo.Enabled = True
        'fndLocation.Enabled = True
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

        Dim repoAssetCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAssetCode.FormatString = ""
        repoAssetCode.HeaderText = "Asset Code"
        repoAssetCode.Name = colAssetCode
        '   repoAssetCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoAssetCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAssetCode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoAssetCode)

        Dim repoAssetName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAssetName.FormatString = ""
        repoAssetName.HeaderText = "Asset Description"
        repoAssetName.Name = colAssetName
        repoAssetName.Width = 150
        repoAssetName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAssetName)

        Dim repoHierarchyLevelCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHierarchyLevelCode.FormatString = ""
        repoHierarchyLevelCode.HeaderText = "Hierarchy Level Code"
        repoHierarchyLevelCode.Name = colHierarchyCode
        repoHierarchyLevelCode.Width = 100
        repoHierarchyLevelCode.ReadOnly = False
        If ApplyFinancialCostCenter = True Then
            repoHierarchyLevelCode.IsVisible = True
        Else
            repoHierarchyLevelCode.IsVisible = False
        End If

        gv1.MasterTemplate.Columns.Add(repoHierarchyLevelCode)


        Dim repoHierarchyLevelName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHierarchyLevelName.FormatString = ""
        repoHierarchyLevelName.HeaderText = "Hierarchy Level Name"
        repoHierarchyLevelName.Name = colHierarchyName
        repoHierarchyLevelName.Width = 150
        repoHierarchyLevelName.ReadOnly = True
        If ApplyFinancialCostCenter = True Then
            repoHierarchyLevelName.IsVisible = True
        Else
            repoHierarchyLevelName.IsVisible = False
        End If

        repoHierarchyLevelName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoHierarchyLevelName)

        Dim repoHierarchyLevelNumber As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHierarchyLevelNumber.FormatString = ""
        repoHierarchyLevelNumber.HeaderText = "Hierarchy Level Number"
        repoHierarchyLevelNumber.Name = colHierarchyLevelNumber
        repoHierarchyLevelNumber.Width = 150
        repoHierarchyLevelNumber.ReadOnly = True
        repoHierarchyLevelNumber.IsVisible = False
        repoHierarchyLevelNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoHierarchyLevelNumber)

        Dim repoCostCenterCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCostCenterCode.FormatString = ""
        repoCostCenterCode.HeaderText = "Cost Center Code"
        repoCostCenterCode.Name = colCostCenterCode
        repoCostCenterCode.Width = 100
        repoCostCenterCode.ReadOnly = False
        repoCostCenterCode.IsVisible = ApplyFinancialCostCenter
        gv1.MasterTemplate.Columns.Add(repoCostCenterCode)

        Dim repoCostCenterName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCostCenterName.FormatString = ""
        repoCostCenterName.HeaderText = "Cost Center"
        repoCostCenterName.Name = colCostCenterName
        repoCostCenterName.Width = 150
        repoCostCenterName.ReadOnly = True
        repoCostCenterName.IsVisible = ApplyFinancialCostCenter
        repoCostCenterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoCostCenterName)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        ' repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.ReadOnly = True
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

        Dim repoHSNName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHSNName.FormatString = ""
        repoHSNName.HeaderText = "HSN Code"
        repoHSNName.Name = colHSN
        repoHSNName.Width = 100
        repoHSNName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHSNName)

        Dim shippedQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        shippedQty = New GridViewDecimalColumn()
        shippedQty.FormatString = ""
        shippedQty.HeaderText = "Quantity"
        shippedQty.Name = colQty
        shippedQty.IsVisible = True
        shippedQty.Minimum = 0
        shippedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        shippedQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(shippedQty)

        Dim Unit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Unit.FormatString = ""
        Unit.HeaderText = "UOM"
        Unit.Name = colUnit
        Unit.Width = 80
        Unit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(Unit)

        Dim price As GridViewDecimalColumn = New GridViewDecimalColumn()
        price.FormatString = ""
        price.WrapText = True
        price.HeaderText = "Disposal Rate"
        price.Name = colRate
        price.Width = 80
        price.Minimum = 0
        price.ReadOnly = False
        price.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(price)

        Dim ItemAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        ItemAmt.FormatString = ""
        ItemAmt.HeaderText = "Amount"
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

        Dim reptAmtAfterDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        reptAmtAfterDiscount.FormatString = ""
        reptAmtAfterDiscount.WrapText = True
        reptAmtAfterDiscount.HeaderText = "Amt After Discount"
        reptAmtAfterDiscount.Name = colAmtAfterDiscount
        reptAmtAfterDiscount.Width = 80
        reptAmtAfterDiscount.Minimum = 0
        reptAmtAfterDiscount.ReadOnly = True
        reptAmtAfterDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(reptAmtAfterDiscount)

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

        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)


        Dim itemnetamt As GridViewDecimalColumn = New GridViewDecimalColumn()
        itemnetamt.FormatString = ""
        itemnetamt.HeaderText = "Amt After Tax"
        itemnetamt.ReadOnly = True
        itemnetamt.WrapText = True
        itemnetamt.Name = colAmtAfterTax
        itemnetamt.Width = 80
        itemnetamt.Minimum = 0
        itemnetamt.ReadOnly = True
        itemnetamt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(itemnetamt)

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

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If gv1.CurrentColumn Is gv1.Columns(colICode) OrElse gv1.CurrentColumn Is gv1.Columns(colQty) OrElse gv1.CurrentColumn Is gv1.Columns(colRate) OrElse gv1.CurrentColumn Is gv1.Columns(colDisPer) OrElse gv1.CurrentColumn Is gv1.Columns(colDisAmt) OrElse gv1.CurrentColumn Is gv1.Columns(colAssetCode) OrElse gv1.CurrentColumn Is gv1.Columns(colHierarchyCode) OrElse gv1.CurrentColumn Is gv1.Columns(colCostCenterCode) Then
                        If gv1.CurrentColumn Is gv1.Columns(colQty) OrElse gv1.CurrentColumn Is gv1.Columns(colRate) OrElse gv1.CurrentColumn Is gv1.Columns(colDisPer) OrElse gv1.CurrentColumn Is gv1.Columns(colDisAmt) Then
                            UpdateCurrentRow(gv1.CurrentRow.Index)

                            UpdateAllTotals()
                        ElseIf gv1.CurrentColumn Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                        ElseIf gv1.CurrentColumn Is gv1.Columns(colAssetCode) Then
                            OpenAssetCodeList(False)

                        ElseIf e.Column Is gv1.Columns(colHierarchyCode) Then
                            OpenHierarchyCode(False)
                        ElseIf e.Column Is gv1.Columns(colCostCenterCode) Then

                            OpenCostCenterList(False)

                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenCostCenterList(ByVal isButtonClick As Boolean)

        If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value)) > 0 Then
            If ApplyFinancialCostCenter = True Then
                Dim qry As String = "select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL "
                gv1.CurrentRow.Cells(colCostCenterCode).Value = clsCommon.ShowSelectForm("TSPL_COST_CENTRE_FINANCIAL@AEFinder", qry, "Code", " Hirerachy_Level = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevelNumber).Value) + "' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value), "", isButtonClick)
                gv1.CurrentRow.Cells(colCostCenterName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Cost_Center_Fin_Name  from TSPL_COST_CENTRE_FINANCIAL  where  Cost_Center_Fin_Code= '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value) + "'")) ' ClsCostCenter.GetCostCenterDesc(gv1.CurrentRow.Cells(colCostCenter).Value)
            Else
                gv1.CurrentRow.Cells(colCostCenterCode).Value = ""
                gv1.CurrentRow.Cells(colCostCenterName).Value = ""
            End If

        Else
            clsCommon.MyMessageBoxShow(Me, "Please select hirerachy level first.", Me.Text)
        End If
    End Sub



    Private Sub OpenHierarchyCode(ByVal isButtonClick As Boolean)
        Dim qry As String = " select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE as [Code] ,TSPL_HIRERACHY_LEVEL_MASTER.Description as [Description],TSPL_HIRERACHY_LEVEL_MASTER.Level as [Level] ,TSPL_HIRERACHY_LEVEL_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Created_Date,103) as [Created Date] ,TSPL_HIRERACHY_LEVEL_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HIRERACHY_LEVEL_MASTER  "
        gv1.CurrentRow.Cells(colHierarchyCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value), "Code", isButtonClick)
        gv1.CurrentRow.Cells(colHierarchyLevelNumber).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(Level,0) AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))
        gv1.CurrentRow.Cells(colHierarchyName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Description,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))
        gv1.CurrentRow.Cells(colCostCenterCode).Value = ""
        gv1.CurrentRow.Cells(colCostCenterName).Value = ""
    End Sub


    Sub OpenAssetCodeList(ByVal isButtonClick As Boolean)

        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select location", Me.Text)
            fndLocation.Focus()
            Exit Sub
        End If

        Dim qry As String = "select Asset_Code as Code, Asset_Name as Description,Asset_Specification as [Asset Specification], TSPL_ACQUISITION_HEAD.Acquisition_Code as [Acquisition Code], Convert (varchar, TSPL_ACQUISITION_HEAD.Acquisition_Date,103) as  [Acquisition Date] from TSPL_ACQUISITION_DETAIL "
        qry += " left outer join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code "
        Dim whrclas As String = " TSPL_ACQUISITION_HEAD.Status=1 and TSPL_ACQUISITION_HEAD.Loc_Code='" + fndLocation.Value + "' and isnull(TSPL_ACQUISITION_DETAIL.asset_merged,0)<>1 and  Convert (date, TSPL_ACQUISITION_HEAD.Acquisition_Date,103) < convert (date, '" + dtpshipment.Value + "',103) and  not exists (select 1 from TSPL_ASSET_SCRAP_DETAIL where Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code)"

        gv1.CurrentRow.Cells(colAssetCode).Value = clsCommon.ShowSelectForm("AsseyFinderSC", qry, "Code", whrclas, clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value), "Code", isButtonClick)

        If clsCommon.myLen(gv1.CurrentRow.Cells(colAssetCode).Value) > 0 Then
            gv1.CurrentRow.Cells(colAssetName).Value = clsAcquisitionDetail.GetName(clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value))
            gv1.CurrentRow.Cells(colQty).Value = 1
            gv1.CurrentRow.Cells(colICode).Value = clsAcquisitionDetail.GetICode(clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value))
            gv1.CurrentRow.Cells(colIName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing)
            qry = "select Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            qry = "select HSN_Code from TSPL_ITEM_MASTER where Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "'"
            gv1.CurrentRow.Cells(colHSN).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            'qry = "select min(Asset_value) from TSPL_ASSET_DEPRECIATION where Asset_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value) + "'"
            'gv1.CurrentRow.Cells(colRate).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If chkAgainstScrap.Checked Then
                gv1.CurrentRow.Cells(colRate).Value = 0
            Else
                gv1.CurrentRow.Cells(colRate).Value = clsAssetDepreciation.GetAssetCurrentValue(gv1.CurrentRow.Cells(colAssetCode).Value, Nothing)
            End If
            dtpshipment.Enabled = False
        Else
            gv1.CurrentRow.Cells(colAssetName).Value = ""
        End If
        SetitemWiseTaxSetting(True, True)
        UpdateCurrentRow(gv1.CurrentRow.Index)
        '==============================Added by preeti Gupta=====================
        If clsCommon.myLen(gv1.CurrentRow.Cells(colAssetCode).Value) > 0 Then
            Dim arrLoc As New ArrayList()
            arrLoc.Add(fndLocation.Value)

            Dim arrAsset As New ArrayList()
            arrAsset.Add(clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value))
            Dim Arr As List(Of clsDepreciationCalculation) = Nothing
            clsAssetDepreciation.GetDepreciationCal(Arr, False, dtpshipment.Value, False, arrLoc, arrAsset, Nothing, Nothing, Nothing)

            'Dim obj As New clsDepreciationCalculation()
            'obj = clsDepreciationCalculation.GetDepreciationCal(dtpshipment.Value, False, fndLocation.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value), True, Nothing)
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                If clsCommon.myLen(Arr(0).colAssetCode) > 0 Then
                    gv1.CurrentRow.Cells(colAmt).Value = clsCommon.myCdbl(Arr(0).colAssetValue)
                    gv1.CurrentRow.Cells(colRate).Value = clsCommon.myCdbl(Arr(0).colAssetValue)
                End If
            End If
        End If
        '=========================================================================

        UpdateAllTotals()
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)

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
        butCostCenterAndHirerachy_Update_AfterPost.Visible = False
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        chkVendor.Checked = False
        gv1.Rows.AddNew()
        RadLabel2.Text = "Customer No"
        gvadd.AllowAddNewRow = False
        gvadd.Rows.AddNew()
        dtpshipment.Enabled = True
    End Sub

    Function AllowToSave() As Boolean
        '===============Preeti==================================
        If AllowFutureDateTransaction(dtpshipment.Value, Nothing) = False Then
            dtpshipment.Select()
            Return False
        End If
        '=======================================================

        If btnSave.Text = "Update" Then
            Dim strchk As String = "select Status from TSPL_ASSET_SCRAP_HEAD where Document_No='" + txtDocNo.Value + "'"
            Dim chkpost As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strchk))
            If chkpost = 1 Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Return False
            End If
        End If

        For ii As Integer = 0 To gv1.Rows.Count - 1
            UpdateCurrentRow(ii)
        Next

        UpdateAllTotals()
        If clsCommon.myLen(fndcustNo.Value) <= 0 AndAlso chkAgainstScrap.Checked = False Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Customer", Me.Text)
            fndcustNo.Focus()
            Return False
        End If

        If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group", Me.Text)
            txtTaxGroup.Focus()
            Return False
        End If
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select  From Location", Me.Text)
            fndLocation.Focus()
            Return False
        End If
        If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Shipment No Not found to save", Me.Text)
            txtDocNo.Focus()
            Return False
        End If
        Dim arrAsset As New List(Of String)

        For ii As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colAssetCode).Value) > 0 Then
                If arrAsset.Contains(clsCommon.myCstr(gv1.Rows(ii).Cells(colAssetCode).Value)) Then
                    Throw New Exception("Duplicate Asset Code " & clsCommon.myCstr(gv1.Rows(ii).Cells(colAssetCode).Value) & " Found at Row No " & (ii + 1))
                Else
                    arrAsset.Add(clsCommon.myCstr(gv1.Rows(ii).Cells(colAssetCode).Value))
                End If
            End If
        Next

        If arrAsset.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please enter at least one asset for Dispose", Me.Text)
            Return False
        End If
        Dim qry As String = "select TSPL_ACQUISITION_DETAIL.Asset_Code,TSPL_ACQUISITION_HEAD.Loc_Code from TSPL_ACQUISITION_DETAIL "
        qry += " left outer join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code	"
        qry += " where TSPL_ACQUISITION_DETAIL.Asset_Code in (" + clsCommon.GetMulcallString(arrAsset) + ") and TSPL_ACQUISITION_HEAD.Loc_Code not in ('" + fndLocation.Value + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim err As String = "Follwoing Asset's Location is not correct"
            For Each dr As DataRow In dt.Rows
                err += Environment.NewLine + "Asset " + clsCommon.myCstr(dr("Asset_Code")) + " and it's Location is" + clsCommon.myCstr(dr("Loc_Code"))
            Next
            common.clsCommon.MyMessageBoxShow(Me, err, Me.Text)
            Return False
        End If

        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            UpdateAllTotals()
            If (AllowToSave()) Then

                Dim obj As New clsAssetScrapSaleHead()
                obj.Document_No = txtDocNo.Value
                obj.Status = 0
                obj.cust_Code = fndcustNo.Value
                obj.cust_Name = txtcustdesc.Text
                obj.Document_Date = dtpshipment.Value
                obj.Posting_Date = dtppost.Value
                obj.Loc_Code = fndLocation.Value
                obj.Loc_Name = txtlocation.Text
                obj.Description = txtdescription.Text
                obj.reff = txtref.Text
                obj.Tax_Group = txtTaxGroup.Value
                obj.Tax_Desc = lblTaxGrpName.Text
                obj.Total_Add_Amt = clsCommon.myCdbl(txtaddamt.Text)
                obj.Against_Vendor = clsCommon.myCdbl(chkVendor.Checked)
                obj.Against_Scrap = clsCommon.myCdbl(chkAgainstScrap.Checked)
                obj.Discount_Base = clsCommon.myCdbl(lblAmtWithDiscount.Text)
                obj.Discount_Amt = clsCommon.myCdbl(lblDiscountAmt.Text)
                obj.Amount_Less_Discount = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
                obj.Total_Tax_Amt = clsCommon.myCdbl(lblTaxAmt.Text)

                obj.Doc_Amt = clsCommon.myCdbl(lbldocamt.Text)
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
                obj.Arr = New List(Of clsAssetScrapSaleDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsAssetScrapSaleDetail()
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Asset_Code = clsCommon.myCstr(grow.Cells(colAssetCode).Value)
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colHierarchyCode).Value)) > 0 Then
                        objTr.Hirerachy_Code = clsCommon.myCstr(grow.Cells(colHierarchyCode).Value)
                    End If
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colCostCenterCode).Value)) > 0 Then
                        objTr.CostCenter_Code = clsCommon.myCstr(grow.Cells(colCostCenterCode).Value)
                    End If
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.UOM = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Amt = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Discount_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Value)
                    objTr.Discount_Amt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                    objTr.Amt_After_Discount = clsCommon.myCdbl(grow.Cells(colAmtAfterDiscount).Value)
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
                    objTr.Tax_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt).Value)
                    objTr.Amt_After_Tax = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)

                    '=========================================================================
                    If clsCommon.myLen(objTr.Asset_Code) > 0 Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return
                End If
                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
                '==============================Added by preeti Gupta=====================

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
            dtpshipment.Enabled = False
            BlankAllControls()
            LoadBlankGrid()
            LoadBlankGridTax()
            gvadd.DataSource = Nothing
            gvadd.Rows.Clear()
            'fndcustNo.Enabled = False
            'fndLocation.Enabled = False
            Dim obj As New clsAssetScrapSaleHead()
            obj = clsAssetScrapSaleHead.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btnCancel.Enabled = True
                    If CostCenterAndHirerachyCodeUpdateAfterPost = True Then
                        butCostCenterAndHirerachy_Update_AfterPost.Visible = True
                    End If
                Else
                    btnCancel.Enabled = False
                    butCostCenterAndHirerachy_Update_AfterPost.Visible = False
                End If

                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_No

                dtpshipment.Value = obj.Document_Date
                dtppost.Value = obj.Posting_Date
                fndLocation.Value = obj.Loc_Code
                txtlocation.Text = obj.Loc_Name
                txtdescription.Text = obj.Description
                txtref.Text = obj.reff
                txtTaxGroup.Value = obj.Tax_Group
                lblTaxGrpName.Text = obj.Tax_Desc
                chkVendor.Checked = obj.Against_Vendor
                chkAgainstScrap.Checked = obj.Against_Scrap
                If (chkVendor.Checked) Then
                    RadLabel2.Text = "Vendor No"
                    fndcustNo.Value = obj.cust_Code
                    txtcustdesc.Text = obj.ven_Code
                Else
                    RadLabel2.Text = "Customer No"
                    fndcustNo.Value = obj.cust_Code
                    txtcustdesc.Text = obj.cust_Name
                End If
                lblAmtWithDiscount.Text = clsCommon.myFormat(obj.Discount_Base)
                lblDiscountAmt.Text = clsCommon.myFormat(obj.Discount_Amt)
                lblAmtAfterDiscount.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                txtaddamt.Text = clsCommon.myFormat(obj.Total_Add_Amt)
                lbldocamt.Text = clsCommon.myFormat(obj.Doc_Amt)

                txtTermCode.Value = obj.Terms_Code
                txtDueDate.Value = obj.Due_Date
                FlagDocumentIsTaxable = CheckIsTaxable(obj.Document_No)
                EInvoiceType = clsERPFuncationality.GetCustomerEInvoiceTypeFromTransationTable("TSPL_ASSET_SCRAP_HEAD", "Document_No", obj.Document_No, Nothing)

                If FlagDocumentIsTaxable = 1 AndAlso clsERPFuncationality.GetEInvoiceStatus(dtpshipment.Value) = True AndAlso clsCommon.CompairString(EInvoiceType, "BB") = CompairStringResult.Equal Then
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

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsAssetScrapSaleDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetCode).Value = objTr.Asset_Code
                        If clsCommon.myLen(objTr.Asset_Code) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetName).Value = clsAcquisitionDetail.GetName(objTr.Asset_Code)
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyCode).Value = objTr.Hirerachy_Code
                        If clsCommon.myLen(objTr.Hirerachy_Code) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevelNumber).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Level from TSPL_HIRERACHY_LEVEL_MASTER where Hirerachy_Code='" + objTr.Hirerachy_Code + "'"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_HIRERACHY_LEVEL_MASTER where Hirerachy_Code='" + objTr.Hirerachy_Code + "'"))
                        End If
                        If ApplyFinancialCostCenter = True Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterCode).Value = objTr.CostCenter_Code
                            If clsCommon.myLen(objTr.CostCenter_Code) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Name from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + objTr.CostCenter_Code + "'"))  ' objTr.CostCenter_Name
                            End If
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSN).Value = clsDBFuncationality.getSingleValue("select HSN_Code from tspl_item_master where Item_Code='" & objTr.Item_Code & "'")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = objTr.Discount_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmt).Value = objTr.Discount_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterDiscount).Value = objTr.Amt_After_Discount



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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt).Value = objTr.Tax_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterTax).Value = objTr.Amt_After_Tax

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
                    Next
                    If obj.Status = ERPTransactionStatus.Pending Then
                        gv1.Rows.AddNew()
                    End If
                End If
                SetitemWiseTaxOnlySetting()
                UpdateAllTotals()
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
                If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colAssetCode)) > 0 Then
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
                If (clsAssetScrapSaleHead.DeleteData(txtDocNo.Value)) Then
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

    Private Sub fndcustNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcustNo._MYValidating
        If chkVendor.Checked = True Then
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER"
            fndcustNo.Value = clsCommon.ShowSelectForm("VendorMstrIFND", qry, "Code", "", fndcustNo.Value, "Code", isButtonClicked)
            txtcustdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code ='" + fndcustNo.Value + "'"))
        Else
            Dim qry As String = "  select TSPL_customer_MASTER.cust_code as Code,TSPL_customer_MASTER.Customer_Name as Name,TSPL_customer_MASTER.Terms_Code as [Term Code] ,TSPL_TERMS_MASTER.Terms_Desc as [Term Description] ,TSPL_customer_MASTER.Tax_Group as [Tax Group],TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as [Tax Group Description] from TSPL_customer_MASTER  left outer join  TSPL_TERMS_MASTER on TSPL_customer_MASTER.Terms_Code=TSPL_TERMS_MASTER.Terms_Code  left outer join  TSPL_TAX_GROUP_MASTER on TSPL_customer_MASTER.Tax_Group=TSPL_TAX_GROUP_MASTER.Tax_Group_Code "
            Dim WhrCls As String = "TSPL_TAX_GROUP_MASTER.Tax_Group_Type='s' and  TSPL_customer_MASTER.Status ='N'"
            fndcustNo.Value = clsCommon.ShowSelectForm("CustmrMstrIFND", qry, "Code", WhrCls, fndcustNo.Value, "Code", isButtonClicked)
            qry = "  select TSPL_customer_MASTER.cust_code ,TSPL_customer_MASTER.Customer_Name ,TSPL_customer_MASTER.Terms_Code  ,TSPL_TERMS_MASTER.Terms_Desc  ,TSPL_customer_MASTER.Tax_Group ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,TSPL_customer_MASTER.Inter_Branch from TSPL_customer_MASTER  left outer join  TSPL_TERMS_MASTER on TSPL_customer_MASTER.Terms_Code=TSPL_TERMS_MASTER.Terms_Code  left outer join  TSPL_TAX_GROUP_MASTER on TSPL_customer_MASTER.Tax_Group=TSPL_TAX_GROUP_MASTER.Tax_Group_Code where TSPL_customer_MASTER.cust_code='" + fndcustNo.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                txtcustdesc.Text = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
                txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
                lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            Else
                txtcustdesc.Text = ""
                txtTermCode.Value = ""
                lblTermName.Text = ""
                txtTaxGroup.Value = ""
                lblTaxGrpName.Text = ""

            End If
        End If
        SetTaxDetails()
        SetTermDetails()
    End Sub

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
        Dim WhrCls As String = "Tax_Group_Type='S'"
        txtTaxGroup.Value = clsCommon.ShowSelectForm("POTaxFilterFND", qry, "Code", WhrCls, txtTaxGroup.Value, "Code", isButtonClicked)
        lblTaxGrpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" + txtTaxGroup.Value + "'"))
        SetTaxDetails()
    End Sub

    Sub SetTaxDetails()
        'Dim strTaxCode, StrExcisable As String
        Dim intCount As Integer = 0
        LoadBlankGridTax()
        Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='s') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='s' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='s' order by Trans_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
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
        Return True
    End Function

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsscrap(txtTaxGroup.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colAssetCode)) > 0 Then
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
                    If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colAssetCode)) > 0 Then
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
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        fndLocation.Value = clsCommon.ShowSelectForm("LocTnMstrFND", qry, "Code", WhrCls, fndLocation.Value, "Code", isButtonClicked)
        txtlocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblAmt As Double = dblQty * dblRate
        gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPer).Value)
        Dim dblDisAmt As Double = (dblAmt * dblDisPer) / 100
        Dim dblAmtAfterDis As Double = dblAmt - dblDisAmt


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
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 2)
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



        Dim dblAmtAfterTax As Double = dblAmtAfterDis + dblTotalTaxAmt
        gv1.Rows(IntRowNo).Cells(colDisAmt).Value = Math.Round(dblDisAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterDiscount).Value = Math.Round(dblAmtAfterDis, 2)
        gv1.Rows(IntRowNo).Cells(colTaxAmt).Value = Math.Round(dblTotalTaxAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentColumn Is gv1.Columns(colTaxAmt) AndAlso rbtnTaxCalAutomatic.IsChecked Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt).Value)
                frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmtAfterDiscount).Value)
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
                        gv1.CurrentRow.Cells(colTaxAmt).Value = frm.dblTotTax
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

        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colAssetCode).Value) > 0) Then
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisAmt).Value)
                dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDiscount).Value)
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

                dblTaxTotAmt = dblTaxTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt).Value)
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterTax).Value)
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
        Dim dblACAmount As Double = 0
        For ii As Integer = 0 To gvadd.Rows.Count - 1
            If (clsCommon.myLen(gvadd.Rows(ii).Cells(coladdcode).Value) > 0) Then
                dblACAmount = dblACAmount + clsCommon.myCdbl(gvadd.Rows(ii).Cells(coladdamt).Value)
            End If
        Next
        dblNetAmt += dblACAmount


        lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt)
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblAmtAfterDis)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
        lbladdcharges.Text = clsCommon.myFormat(dblACAmount)
        lbldocamt.Text = clsCommon.myFormat(dblNetAmt)
    End Sub

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If rbtnTaxCalAutomatic.IsChecked Then ''If rbtnTaxCalAutomatic.IsChecked AndAlso clsCommon.CompairString(gv2.CurrentCell.ColumnInfo.Name, colTTaxRate) = CompairStringResult.Equal Then
                Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FndVndrTxRatFND", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='P'", "", "", True))
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

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                'If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colAssetCode) Then
                '    'If chkAsset.Checked Then
                '    '    gv1.CurrentRow.Cells(colICode).ReadOnly = True
                '    '    gv1.CurrentRow.Cells(colAssetCode).ReadOnly = False
                '    'Else
                '    '    gv1.CurrentRow.Cells(colICode).ReadOnly = False
                '    '    gv1.CurrentRow.Cells(colAssetCode).ReadOnly = True
                '    'End If
                'End If
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub

    Private Sub gvadd_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvadd.CellValueChanged
        Try
            If Not isCellValueChangedOpenAdd Then
                isCellValueChangedOpenAdd = True
                If e.Column Is gvadd.Columns(coladdcode) Then
                    OpenAddCodeList(False)
                End If

                UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                UpdateAllTotals()

                setGridFocusAdd()
                isCellValueChangedOpenAdd = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub setGridFocusAdd()
        Dim intCurrRow As Integer = gvadd.CurrentRow.Index
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

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_ASSET_SCRAP_HEAD where Document_No='" + txtDocNo.Value + "'"
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
        Dim qry As String = " select Document_No as Code,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,TSPL_ASSET_SCRAP_HEAD.cust_Code as [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name as Customer,Doc_Amt as Amount,case when TSPL_ASSET_SCRAP_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status] from TSPL_ASSET_SCRAP_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_ASSET_SCRAP_HEAD.cust_Code "

        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " loc_code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        LoadData(clsCommon.ShowSelectForm("AssetscFiltrFND", qry, "Code", whrClas, txtDocNo.Value, "TSPL_ASSET_SCRAP_HEAD.Document_Date desc", isButtonClicked), NavigatorType.Current)
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
                If (clsAssetScrapSaleHead.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)


                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub layout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles layout1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            obj.GridColumns = gv1.ColumnCount
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
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
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            fundelete()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

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

    Sub Print(ByVal isPrint As Boolean)
        Try
            Dim frm As New frmCrystalReportViewer()
            Dim Address As String
            If clsCommon.myLen(fndLocation.Value) > 0 Then
                Address = "(Select  MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end  + Case When TSPL_LOCATION_MASTER.Add4 ='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add4 ,103) end) from TSPL_LOCATION_MASTER   where Location_Code = ('" + fndLocation.Value + "'))  "
            Else
                Address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "
            End If
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim InvoiceNo As String = clsDBFuncationality.getSingleValue("select Document_No from TSPL_ASSET_SCRAP_HEAD where Document_No='" + clsCommon.myCstr(txtDocNo.Value) + "' ")

                If clsCommon.myLen(InvoiceNo) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Invoice No does't exist for this loadout", Me.Text)
                Else
                    Dim IsEInvoiceApply As Integer = 0
                    If FlagDocumentIsTaxable = 1 AndAlso clsERPFuncationality.GetEInvoiceStatus(dtpshipment.Value) = True AndAlso clsCommon.CompairString(EInvoiceType, "BB") = CompairStringResult.Equal Then
                        IsEInvoiceApply = 1
                    End If

                    Dim qry As String
                    Dim dt As DataTable
                    'qry = "select h.Created_By ,h.Modify_By , tspl_company_master.logo_img,tspl_company_master.logo_img2,tspl_company_master.Comp_Name  as CompanyName,tspl_company_master.Tin_No as CompanyTin," + Address + " as  CompanyAddress,h.invoice_No as ScrapInvoice,h.Document_Date  as ScrapInvoiceDate," & _
                    '           "h.cust_Name as CustomerName,h.cust_Code as CustomerCode,case when len(cm.Add1)>0 then cm.Add1 else '' end +" & _
                    '           "case when len(cm.Add2)>0 then ',' else '' end + " & _
                    '           "case when len(cm.Add2)>0 then cm.Add2 else ''end +" & _
                    '           "case when len(cm.Add3)>0 then ',' else '' end +" & _
                    '           "case when len(cm.Add3)>0 then cm.Add3 else '' end   as CustomerAddress," & _
                    '           "TSPL_SCRAPSALE_HEAD.NRG_No  as NrgNo,cm.CST as CstNo,TSPL_LOCATION_MASTER.Tin_No as TinNo,d.Asset_Code as ItemCode," & _
                    '           " (Select ISNULL(Cheapter_Heads,'') from TSPL_ITEM_MASTER WHere TSPL_ITEM_MASTER.Item_Code=d.Item_Code)as [ChapterHead], " & _
                    '           "TSPL_ACQUISITION_DETAIL.Asset_Name as Desciption,TSPL_ACQUISITION_DETAIL.Asset_specification,d.invoice_Qty as Quantity ,d.unit_code as Uom,d.price as Rate," & _
                    '           "d.ItemAmt as Amount, d.TAX1_Amt as Dtax1_Amt, d.DiscountAmt ,h.TAX1 as TaxRateDesc1, ROUND(h.TAX1_Amt, 0) as TaxRate1,h.TAX2 as TaxRateDesc2,ROUND(h.TAX2_Amt,0) as TaxRate2,h.TAX3 as TaxRateDesc3 ,ROUND(h.TAX3_Amt,0) as TaxRate3,h.TAX4 as TaxRateDesc4,ROUND(h.TAX4_Amt,0) as TaxRate4,h.TAX5 as TaxRateDesc5,ROUND(h.TAX5_Amt,0) as TaxRate5,h.TAX6 as TaxRateDesc6,ROUND(h.TAX6_Amt,0) as TaxRate6,h.TAX7 as TaxRateDesc7,ROUND(h.TAX7_Amt,0) as TaxRate7,h.TAX8 as  TaxRateDesc8,ROUND(h.TAX8_Amt,0) as TaxRate8,h.TAX9 as TaxRateDesc9,ROUND(h.TAX9_Amt,0) as TaxRate9,h.TAX10 as TaxRateDesc10,ROUND(h.TAX10_Amt,0) as  TaxRate10, " & _
                    '           " h.TAX1_Rate , h.TAX2_Rate, h.TAX3_Rate, h.TAX4_Rate, h.TAX5_Rate, h.TAX6_Rate, h.TAX7_Rate, h.TAX8_Rate, h.TAX9_Rate, h.TAX10_Rate, " & _
                    '           " TSPL_SCRAPSALE_HEAD.Excisable " & _
                    '           "from TSPL_SCRAPINVOICE_HEAD h left outer join TSPL_SCRAPINVOICE_DETAIL d on h.invoice_No =d.invoice_No left outer join TSPL_ACQUISITION_DETAIL ON d.Asset_Code =  TSPL_ACQUISITION_DETAIL.Asset_Code " & _
                    '           "left outer join TSPL_CUSTOMER_MASTER   cm on h.cust_Code =cm.Cust_Code  left outer join tspl_company_master   on tspl_company_master.Comp_Code ='" + objCommonVar.CurrentCompanyCode + "'left outer join TSPL_SCRAPSALE_HEAD  on h.Document_No =TSPL_SCRAPSALE_HEAD.Document_No left outer join TSPL_LOCATION_MASTER on  h.Loc_Code=TSPL_LOCATION_MASTER.Location_Code where h.invoice_No='" + clsCommon.myCstr(InvoiceNo) + "'"

                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                        qry = "   select " + clsCommon.myCstr(FlagDocumentIsTaxable) + " as Is_Taxable,'1' as  CopyType ,cast(TSPL_ASSET_SCRAP_HEAD.BarCode_Img as image) As BarCode_Img,isnull (TSPL_ASSET_SCRAP_HEAD.IRN_No,'') as IRN_No,isnull (TSPL_ASSET_SCRAP_HEAD.Ack_No,'') as Ack_No,case when len(isnull (TSPL_ASSET_SCRAP_HEAD.Ack_No,'')) > 0 then convert (varchar, TSPL_ASSET_SCRAP_HEAD.Ack_Date,103) else ''  end as Ack_Date, " + clsCommon.myCstr(IsEInvoiceApply) + " as IsEInvoiceApply,"
                        qry += "  Case when tax1.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX1_Rate when  tax2.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX2_Rate when tax3.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX3_Rate when tax4.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX4_Rate  when tax5.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX5_Rate when tax6.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX6_Rate  when tax7.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX7_Rate when tax8.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX8_Rate when tax9.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX9_Rate when tax10.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX10_Rate end as TCS_Rate,Case when tax1.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX1_Amt when  tax2.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX2_Amt when tax3.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX3_Amt when tax4.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX4_Amt  when tax5.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX5_Amt when tax6.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX6_Amt  when tax7.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX7_Amt when tax8.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX8_Amt when tax9.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX9_Amt when tax10.Is_TCS = 'Y' then TSPL_ASSET_SCRAP_HEAD.TAX10_Amt end  as TCS_Amount,TSPL_ASSET_SCRAP_DETAIL.Line_No, tspl_company_master.comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1, TSPL_COMPANY_MASTER.Add2 as Comp_Add2, TSPL_COMPANY_MASTER.Add3 as Comp_Add3, TSPL_COMPANY_MASTER.Email as Comp_Email, TSPL_COMPANY_MASTER.Phone1 as Comp_Phone1, TSPL_COMPANY_MASTER.Phone2 as Comp_Phone2, TSPL_COMPANY_MASTER.Pan_No as Comp_Pan_No, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, TSPL_COMPANY_MASTER.GSTREg_No as Comp_GSTREg_No , TSPL_COMPANY_MASTER.CINNO as Comp_CINNO, TSPL_COMPANY_MASTER.Access_Officer as Comp_Access_Officer ,TSPL_ASSET_SCRAP_DETAIL.Amt_After_Tax as TotalAmt " &
                        ", TSPL_ITEM_MASTER.HSN_Code " &
                        ", FromState.GST_STATE_CODE as From_GstStateCode,FromLocation.GSTNO as From_Loc_GstinNo " &
                        ",Customer_State.GST_STATE_CODE as Cust_GstStateCode,Customer_State.STATE_NAME AS Cust_StateName,TSPL_CUSTOMER_MASTER.GSTNO as Cust_GstInNo " &
                        ",ISNULL(TSPL_ASSET_SCRAP_DETAIL.Discount_Amt,0) AS DiscountAmt,TSPL_ASSET_SCRAP_HEAD.Document_No as invoice_No, convert(varchar,TSPL_ASSET_SCRAP_HEAD.Document_Date,103) as Invoice_Date, tspl_customer_master.PAN as Cust_Pan " &
                        ", TSPL_ASSET_SCRAP_HEAD.Description,FromLocation.HOAdd1 as frmHO1,FromLocation.HOAdd2 as frmHO2 " &
                        ",TSPL_ASSET_SCRAP_HEAD.Loc_Code as From_Location,FromLocation.Location_Desc as [From Location Desc] " &
                        ",(FromLocation.Add1+FromLocation.Add2+FromLocation.Add3+FromLocation.Add4)as [From Address]  " &
                        ",FromLocation.Pin_Code,FromLocation.TIN_No,FromLocation.CST_No,FromLocation.State as From_State,FromState.State_Name as frm_State_name  " &
                        ",tspl_customer_master.Pin_Code as [To Pin Code],tspl_customer_master.TIN_No as [To TIN No],tspl_customer_master.CST as [To CST No],tspl_customer_master.Phone1 as [To phone] " &
                        ",TSPL_ASSET_SCRAP_DETAIL.Asset_code as Asset_code,TSPL_ASSET_SCRAP_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ASSET_SCRAP_DETAIL.Qty AS Shipped_Qty,TSPL_ASSET_SCRAP_DETAIL.UOM AS Unit_Code " &
                        ",TSPL_ASSET_SCRAP_DETAIL.Rate AS Price,TSPL_ASSET_SCRAP_DETAIL.AMT as ItemAmt " &
                        ",TSPL_ASSET_SCRAP_HEAD.Total_Tax_Amt,TSPL_ASSET_SCRAP_HEAD.Doc_Amt " &
                        ",	TAX1 .Tax_Code_Desc as tax1name,isnull (TSPL_ASSET_SCRAP_HEAD.tax1_rate,0) as txt1Rate,isnull (TSPL_ASSET_SCRAP_HEAD.tax1_amt,0) as txt1amt,  tax2.Tax_Code_Desc as tax2name,isnull (TSPL_ASSET_SCRAP_HEAD.tax2_rate,0) as txt2Rate,isnull (TSPL_ASSET_SCRAP_HEAD.tax2_amt,0) as txt2amt, tax3.Tax_Code_Desc as tax3name,isnull (TSPL_ASSET_SCRAP_HEAD.tax3_rate,0) as txt3Rate,isnull (TSPL_ASSET_SCRAP_HEAD.tax3_amt,0) as txt3amt,  tax4.Tax_Code_Desc as tax4name,isnull (TSPL_ASSET_SCRAP_HEAD.tax4_rate,0) as txt4Rate,isnull (TSPL_ASSET_SCRAP_HEAD.tax4_amt,0) as txt4amt,  tax5.Tax_Code_Desc as tax5name,isnull (TSPL_ASSET_SCRAP_HEAD.tax5_rate,0) as txt5Rate,isnull (TSPL_ASSET_SCRAP_HEAD.tax5_amt,0) as txt5amt,   tax6.Tax_Code_Desc as tax6name,isnull (TSPL_ASSET_SCRAP_HEAD.tax6_rate,0) as txt6Rate,isnull (TSPL_ASSET_SCRAP_HEAD.tax6_amt,0) as txt6amt,   tax7.Tax_Code_Desc as tax7name,isnull (TSPL_ASSET_SCRAP_HEAD.tax7_rate,0) as txt7Rate,isnull (TSPL_ASSET_SCRAP_HEAD.tax7_amt,0) as txt7amt,  tax8.Tax_Code_Desc as tax8name,isnull (TSPL_ASSET_SCRAP_HEAD.tax8_rate,0) as txt8Rate,isnull (TSPL_ASSET_SCRAP_HEAD.tax8_amt,0) as txt8amt,    tax9.Tax_Code_Desc as tax9name,isnull (TSPL_ASSET_SCRAP_HEAD.tax9_rate,0) as txt9Rate,isnull (TSPL_ASSET_SCRAP_HEAD.tax9_amt,0) as txt9amt,'' as cin,'' as pan, '' as companyaddress, tax10.Tax_Code_Desc as tax10name,isnull (TSPL_ASSET_SCRAP_HEAD.tax10_amt,0) as txt10amt ,TSPL_ASSET_SCRAP_HEAD.AddDesc1,isnull (TSPL_ASSET_SCRAP_HEAD.AddAmt1,0) as AddAmt1, TSPL_ASSET_SCRAP_HEAD.AddDesc2,isnull (TSPL_ASSET_SCRAP_HEAD.AddAmt2,0) as AddAmt2, TSPL_ASSET_SCRAP_HEAD.AddDesc3,isnull (TSPL_ASSET_SCRAP_HEAD.AddAmt3,0) as AddAmt3, TSPL_ASSET_SCRAP_HEAD.AddDesc4,isnull (TSPL_ASSET_SCRAP_HEAD.AddAmt4,0) as AddAmt4, TSPL_ASSET_SCRAP_HEAD.AddDesc5,isnull (TSPL_ASSET_SCRAP_HEAD.AddAmt5,0) as AddAmt5, TSPL_ASSET_SCRAP_HEAD.AddDesc6,isnull (TSPL_ASSET_SCRAP_HEAD.AddAmt6,0) as AddAmt6, TSPL_ASSET_SCRAP_HEAD.AddDesc7,isnull (TSPL_ASSET_SCRAP_HEAD.AddAmt7,0) as AddAmt7, TSPL_ASSET_SCRAP_HEAD.AddDesc8,isnull (TSPL_ASSET_SCRAP_HEAD.AddAmt8,0) as AddAmt8, TSPL_ASSET_SCRAP_HEAD.AddDesc9,isnull (TSPL_ASSET_SCRAP_HEAD.AddAmt9,0) as AddAmt9, TSPL_ASSET_SCRAP_HEAD.AddDesc10,isnull (TSPL_ASSET_SCRAP_HEAD.AddAmt10,0) as AddAmt10,dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type, isnull(TSPL_ASSET_SCRAP_DETAIL.TAX1_Amt ,0) as DTax1_Amt, isnull(TSPL_ASSET_SCRAP_DETAIL.TAX2_Amt ,0) as DTax2_Amt, isnull(TSPL_ASSET_SCRAP_DETAIL.TAX3_Amt ,0) as DTax3_Amt, isnull(TSPL_ASSET_SCRAP_DETAIL.TAX4_Amt ,0) as DTax4_Amt, isnull(TSPL_ASSET_SCRAP_DETAIL.TAX5_Amt ,0) as DTax5_Amt, isnull(TSPL_ASSET_SCRAP_DETAIL.TAX6_Amt ,0) as DTax6_Amt, isnull(TSPL_ASSET_SCRAP_DETAIL.TAX7_Amt ,0) as DTax7_Amt, isnull(TSPL_ASSET_SCRAP_DETAIL.TAX8_Amt ,0) as DTax8_Amt, isnull(TSPL_ASSET_SCRAP_DETAIL.TAX9_Amt ,0) as DTax9_Amt, isnull(TSPL_ASSET_SCRAP_DETAIL.TAX10_Amt ,0) as DTax10_Amt, isnull(TSPL_ASSET_SCRAP_DETAIL.TAX1_Rate,0) as DTax1_Rate,  isnull(TSPL_ASSET_SCRAP_DETAIL.TAX2_Rate,0) as DTax2_Rate,  isnull(TSPL_ASSET_SCRAP_DETAIL.TAX3_Rate,0) as DTax3_Rate,  isnull(TSPL_ASSET_SCRAP_DETAIL.TAX4_Rate,0) as DTax4_Rate, isnull(TSPL_ASSET_SCRAP_DETAIL.TAX5_Rate,0) as DTax5_Rate,  isnull(TSPL_ASSET_SCRAP_DETAIL.TAX6_Rate,0) as DTax6_Rate, isnull(TSPL_ASSET_SCRAP_DETAIL.TAX7_Rate,0) as DTax7_Rate,  isnull(TSPL_ASSET_SCRAP_DETAIL.TAX8_Rate,0) as DTax8_Rate, isnull(TSPL_ASSET_SCRAP_DETAIL.TAX9_Rate,0) as DTax9_Rate,  isnull(TSPL_ASSET_SCRAP_DETAIL.TAX10_Rate,0) as DTax10_Rate " &
                        ",TSPL_ASSET_SCRAP_HEAD.Cust_code,tspl_customer_master.Customer_Name AS cust_name,tspl_customer_master.add1,tspl_customer_master.add2,tspl_customer_master.add3,tspl_city_master.city_name " &
                        " from TSPL_ASSET_SCRAP_HEAD left join TSPL_ASSET_SCRAP_DETAIL on TSPL_ASSET_SCRAP_DETAIL.Document_No=TSPL_ASSET_SCRAP_HEAD.Document_No  " &
                        " Left Join TSPL_LOCATION_MASTER as FromLocation on FromLocation.Location_Code=TSPL_ASSET_SCRAP_HEAD.Loc_Code  " &
                        " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ASSET_SCRAP_DETAIL.Item_Code  " &
                        " Left Join TSPL_STATE_MASTER as  FromState on FromState.State_Code=FromLocation.State  " &
                        " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_ASSET_SCRAP_HEAD.tax1   left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_ASSET_SCRAP_HEAD.tax2   " &
                        " Left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_ASSET_SCRAP_HEAD .TAX3   left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_ASSET_SCRAP_HEAD .tax4   " &
                        " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_ASSET_SCRAP_HEAD .tax5  left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_ASSET_SCRAP_HEAD .TAX6   " &
                        " Left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_ASSET_SCRAP_HEAD .TAX7  left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_ASSET_SCRAP_HEAD .TAX8   " &
                        " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_ASSET_SCRAP_HEAD .TAX9  left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_ASSET_SCRAP_HEAD .TAX10 left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_ASSET_SCRAP_DETAIL.tax1   left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_ASSET_SCRAP_DETAIL.tax2 left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_ASSET_SCRAP_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_ASSET_SCRAP_DETAIL .tax4 left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_ASSET_SCRAP_DETAIL .tax5  left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_ASSET_SCRAP_DETAIL .TAX6  left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_ASSET_SCRAP_DETAIL .TAX7   left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_ASSET_SCRAP_DETAIL .TAX8   left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_ASSET_SCRAP_DETAIL .TAX9  left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_ASSET_SCRAP_DETAIL .TAX10  " &
                        " Left Join tspl_customer_master on tspl_customer_master.cust_code=TSPL_ASSET_SCRAP_HEAD.cust_code  left join TSPL_STATE_MASTER as Customer_State on tspl_customer_master .State=Customer_State.STATE_CODE  left join tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code  left outer join tspl_company_master on tspl_company_master.comp_code = TSPL_ASSET_SCRAP_HEAD.Comp_Code "
                        qry += "  where TSPL_ASSET_SCRAP_HEAD.Document_No = '" + clsCommon.myCstr(InvoiceNo) + "'"

                        qry = " Select * from ( " + qry + " )XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL: For Buyer' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE: For Transporter' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE: For Seller' as CopyType1  ) YYY ON YYY.COL1=XXX.CopyType  ORDER BY YYY.COL2,Line_No "

                        dt = clsDBFuncationality.GetDataTable(qry)
                        If isPrint Then
                            SetItemWiseTax(dt, InvoiceNo)
                            frm.funreport(CrystalReportFolder.FixedAssets, dt, "AssetScrapSale", "Asset Scrap Sale", clsCommon.myCDate(dt.Rows(0)("Invoice_Date")))
                        End If
                    Else
                        qry = "   select cast(h.BarCode_Img as image) As BarCode_Img,isnull (h.IRN_No,'') as IRN_No,isnull (h.Ack_No,'') as Ack_No,case when len(isnull (h.Ack_No,'')) > 0 then convert (varchar, h.Ack_Date,103) else ''  end as Ack_Date, " + clsCommon.myCstr(IsEInvoiceApply) + " as IsEInvoiceApply,"
                        qry += "          h.Created_By ,h.Modify_By , tspl_company_master.logo_img,tspl_company_master.logo_img2,tspl_company_master.Comp_Name  as CompanyName,tspl_company_master.Tin_No as CompanyTin,TSPL_COMPANY_MASTER.GSTReg_No as GSTINNo"
                        qry += "    ,(Select  MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end  + Case When TSPL_LOCATION_MASTER.Add4 ='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add4 ,103) end) from TSPL_LOCATION_MASTER   where Location_Code = ('" & fndLocation.Value & "'))   as  CompanyAddress"
                        qry += " ,h.Document_No as ScrapInvoice,convert(varchar(15),h.Document_Date,103)  as ScrapInvoiceDate,cm.Customer_Name  as CustomerName,h.cust_Code as CustomerCode, cm.GSTNO,customer_StateMaster.gst_state_code, " &
                            " case when len(cm.Add1)>0 then cm.Add1 else '' end +case when len(cm.Add2)>0 then ',' else '' end + case when len(cm.Add2)>0 then cm.Add2 else ''end +case when len(cm.Add3)>0 then ',' else '' end +case when len(cm.Add3)>0 then cm.Add3 else '' end   as CustomerAddress,cm.CST as CstNo,TSPL_LOCATION_MASTER.Tin_No as TinNo,d.Asset_Code as ItemCode,tspl_item_master.hsn_code,D.Item_code as Icode, (Select ISNULL(Cheapter_Heads,'') from TSPL_ITEM_MASTER WHere TSPL_ITEM_MASTER.Item_Code=d.Item_Code)as [ChapterHead], TSPL_ACQUISITION_DETAIL.Asset_Name as Desciption,TSPL_ACQUISITION_DETAIL.Asset_specification,d.Qty  as Quantity ,d.uom as Uom,d.Rate  as Rate,d.amt as Amount, d.TAX1_Amt as Dtax1_Amt, d.Discount_Amt as  DiscountAmt,TAX1.Tax_Code_Desc as TaxRateDesc1, (h.TAX1_Amt) as TaxRate1,TAX2.Tax_Code_Desc as TaxRateDesc2,(h.TAX2_Amt) as TaxRate2,TAX3.Tax_Code_Desc as TaxRateDesc3 ,(h.TAX3_Amt) as TaxRate3,TAX4.Tax_Code_Desc as TaxRateDesc4,(h.TAX4_Amt) as TaxRate4,TAX5.Tax_Code_Desc as TaxRateDesc5,(h.TAX5_Amt) as TaxRate5,TAX6.Tax_Code_Desc as TaxRateDesc6,(h.TAX6_Amt) as TaxRate6,TAX7.Tax_Code_Desc as TaxRateDesc7,(h.TAX7_Amt) as TaxRate7,TAX8.Tax_Code_Desc as  TaxRateDesc8,(h.TAX8_Amt) as TaxRate8,TAX9.Tax_Code_Desc as TaxRateDesc9,(h.TAX9_Amt) as TaxRate9,TAX10.Tax_Code_Desc as TaxRateDesc10,(h.TAX10_Amt) as  TaxRate10,  h.TAX1_Rate , h.TAX2_Rate, h.TAX3_Rate, h.TAX4_Rate, h.TAX5_Rate, h.TAX6_Rate, h.TAX7_Rate, h.TAX8_Rate, h.TAX9_Rate, h.TAX10_Rate ,h.Total_Tax_Amt,h.doc_amt,h.AddDesc1 as AddCode1 ,h.AddAmt1 ,h.AddDesc2 as AddCode2,h.AddAmt2 ,h.AddDesc3 as AddCode3 ,h.AddAmt3 ,h.AddDesc4 as AddCode4,h.AddAmt4,h.AddDesc5 as AddCode5 ,h.AddAmt5 ,h.AddDesc5 as AddCode5,h.AddAmt5,h.AddDesc6 as AddCode6 ,h.AddAmt6 ,h.AddDesc7 as AddCode7,h.AddAmt7,h.AddDesc8 as AddCode8 ,h.AddAmt8 ,h.AddDesc9 as AddCode9,h.AddAmt9 ,h.AddDesc10 as AddCode10 ,h.AddAmt10 ,h.Total_Add_Amt " &
                            " from TSPL_ASSET_SCRAP_HEAD h " &
                           " left outer join TSPL_ASSET_SCRAP_DETAIL d on h.Document_No =d.Document_No " &
                           " left outer join TSPL_ACQUISITION_DETAIL ON d.Asset_Code =  TSPL_ACQUISITION_DETAIL.Asset_Code " &
                             " left outer join TSPL_CUSTOMER_MASTER   cm on h.cust_Code =cm.Cust_Code " &
                            "  left outer join tspl_company_master   on tspl_company_master.Comp_Code ='" + objCommonVar.CurrentCompanyCode + "' " &
                           " left outer join TSPL_LOCATION_MASTER on  h.Loc_Code=TSPL_LOCATION_MASTER.Location_Code" &
                             "  left join tspl_tax_master as tax1 on tax1.Tax_Code =h.TAX1" &
                           " left join tspl_tax_master as tax2 on tax2.Tax_Code =h.TAX2" &
                             " left join tspl_tax_master as tax3 on tax3.Tax_Code =h.TAX3" &
                             "  left join tspl_tax_master as tax4 on tax4.Tax_Code =h.TAX4" &
                             "  left join tspl_tax_master as tax5 on tax5.Tax_Code =h.TAX5" &
                           " left join tspl_tax_master as tax6 on tax6.Tax_Code =h.TAX6" &
                            " left join tspl_tax_master as tax7 on tax7.Tax_Code =h.TAX7" &
                             "  left join tspl_tax_master as tax8 on tax8.Tax_Code =h.TAX8" &
                             "   left join tspl_tax_master as tax9 on tax9.Tax_Code =h.TAX9" &
                        "   left join tspl_tax_master as tax10 on tax10.Tax_Code =h.TAX10 " &
                        " LEFT OUTER JOIN TSPL_STATE_MASTER as customer_StateMaster on  customer_StateMaster.state_code=cm.State " &
                        "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=d.Item_Code " &
                         "  where h.Document_No = '" + clsCommon.myCstr(InvoiceNo) + "'"

                        dt = clsDBFuncationality.GetDataTable(qry)
                        If isPrint Then
                            SetItemWiseTax(dt, InvoiceNo)
                            frm.funreport(CrystalReportFolder.FixedAssets, dt, "frmAssetScrapSale", "Asset Scrap Sale", clsCommon.myCDate(dt.Rows(0)("ScrapInvoiceDate")))
                        End If
                    End If
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Please select one Invoice", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt2")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate2") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt2") = Math.Round(clsCommon.myCdbl(dr("TaxAmt")), 0, MidpointRounding.AwayFromZero)
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt3")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate3") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt3") = Math.Round(clsCommon.myCdbl(dr("TaxAmt")), 0, MidpointRounding.AwayFromZero)
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintReport.Click
        Print(True)
    End Sub

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
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FillNRGPDetails(ByVal nrgpNo As String)
        Try
            isInsideLoadData = True
            LoadBlankGrid()
            If clsCommon.myLen(nrgpNo) > 0 Then
                Dim qry As String = "Select Item_Code, Item_Desc, Unit_code, RGP_Qty, Item_Cost, Amount from TSPL_RGP_DETAIL  Where RGP_No='" + nrgpNo + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim LineNo As Integer = 0
                For Each dr As DataRow In dt.Rows
                    LineNo += 1
                    gv1.Rows.AddNew()
                    gv1.CurrentRow.Cells(colLineNo).Value = LineNo
                    gv1.CurrentRow.Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.CurrentRow.Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.CurrentRow.Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_code"))
                    gv1.CurrentRow.Cells(colQty).Value = clsCommon.myCdbl(dr("RGP_Qty"))
                    gv1.CurrentRow.Cells(colRate).Value = clsCommon.myCdbl(dr("Item_Cost"))
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

    Private Sub btnPrePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Print(False)
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Dim trans As SqlTransaction = Nothing
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' REASON FOR Reverse 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                trans = clsDBFuncationality.GetTransactin()
                If clsAssetScrapSaleHead.ReverseAndUnpost(txtDocNo.Value, trans) Then
                    saveCancelLog(Reason, "Reverse And Recreate", trans)
                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

        'Try
        '    If clsCommon.myLen(txtDocNo.Value) <= 0 Then
        '        Throw New Exception("Document Number not found to do this operation")
        '    End If
        '    If clsCommon.myLen(lblInvoiceNo.Text) <= 0 Then
        '        Throw New Exception("Invoice Number not found to do this operation")
        '    End If

        '    If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
        '        If ClsScrapInvoiceHead.ReverseAndUnpost(txtDocNo.Value, lblInvoiceNo.Text) Then
        '            common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
        '            LoadData(txtDocNo.Value, NavigatorType.Current)
        '        End If
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try
    End Sub

    Private Sub chkVendor_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendor.ToggleStateChanged
        fndcustNo.Value = ""
        txtcustdesc.Text = ""
        If (chkVendor.Checked) Then
            RadLabel2.Text = "Vendor No"
        Else
            RadLabel2.Text = "Customer No"
        End If
    End Sub

    Private Sub chkAgainstScrap_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAgainstScrap.ToggleStateChanged
        fndcustNo.Value = ""
        txtcustdesc.Text = ""
        If (chkAgainstScrap.Checked) Then
            fndcustNo.Enabled = False
        Else
            fndcustNo.Enabled = True
        End If
    End Sub

    Private Sub dtpshipment_ValueChanged(sender As Object, e As EventArgs) Handles dtpshipment.ValueChanged
        '==============================Added by preeti Gupta=====================
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows((ii)).Cells(colAssetCode).Value) > 0 Then
                dtpshipment.Enabled = False
                'Dim obj As New clsDepreciationCalculation()
                'obj = clsDepreciationCalculation.GetDepreciationCal(dtpshipment.Value, False, fndLocation.Value, clsCommon.myCstr(gv1.Rows(ii).Cells(colAssetCode).Value), True, Nothing)
                'If (obj IsNot Nothing AndAlso obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                '    If clsCommon.myLen(obj.Arr(0).colAssetCode) > 0 Then
                '        gv1.Rows(ii).Cells(colAmt).Value = clsCommon.myCdbl(obj.Arr(0).colAssetValue)
                '        gv1.Rows(ii).Cells(colRate).Value = clsCommon.myCdbl(obj.Arr(0).colAssetValue)
                '    End If
                'End If
            End If
        Next

        '=========================================================================
    End Sub

    Private Sub dtpshipment_ValueChanging(sender As Object, e As ValueChangingEventArgs) Handles dtpshipment.ValueChanging
        Try
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows((ii)).Cells(colAssetCode).Value) > 0 Then
                    dtpshipment.Enabled = False
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub

    Private Function CheckIsTaxable(ByVal StrCode As String) As Integer
        '''''''''''''''''Check document is taxable --------------------
        Dim IsTaxable As Decimal = 0
        If clsCommon.myCdbl(lblTaxAmt.Text) > 0 Then
            If clsCommon.myLen(txtTaxGroup.Value) > 0 Then
                Dim IsExempted = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Is_Tax_Exempted from TSPL_TAX_GROUP_MASTER WHERE TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' and Tax_Group_Code='" + txtTaxGroup.Value + "'"))
                If IsExempted = 1 Then
                    IsTaxable = 0
                Else
                    Dim TaxQuery As String = "select (case when isnull(tax1.Is_TCS,'N')='N' then TSPL_ASSET_SCRAP_HEAD.tax1_amt else 0 end " &
                        " + case when isnull(tax2.Is_TCS,'N')='N' then TSPL_ASSET_SCRAP_HEAD.tax2_amt else 0 end " &
                        " + case when isnull(tax3.Is_TCS,'N')='N' then TSPL_ASSET_SCRAP_HEAD.tax3_amt else 0 end " &
                        " + case when isnull(tax4.Is_TCS,'N')='N' then TSPL_ASSET_SCRAP_HEAD.tax4_amt else 0 end " &
                        " + case when isnull(tax5.Is_TCS,'N')='N' then TSPL_ASSET_SCRAP_HEAD.tax5_amt else 0 end " &
                        " + case when isnull(tax6.Is_TCS,'N')='N' then TSPL_ASSET_SCRAP_HEAD.tax6_amt else 0 end " &
                        " + case when isnull(tax7.Is_TCS,'N')='N' then TSPL_ASSET_SCRAP_HEAD.tax7_amt else 0 end " &
                        " + case when isnull(tax8.Is_TCS,'N')='N' then TSPL_ASSET_SCRAP_HEAD.tax8_amt else 0 end " &
                        " + case when isnull(tax9.Is_TCS,'N')='N' then TSPL_ASSET_SCRAP_HEAD.tax9_amt else 0 end " &
                        " + case when isnull(tax10.Is_TCS,'N')='N' then TSPL_ASSET_SCRAP_HEAD.tax10_amt else 0 end) as aa " &
                        " from TSPL_ASSET_SCRAP_HEAD " &
                        " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_ASSET_SCRAP_HEAD.tax1  " &
                         " left outer join tspl_tax_master As tax2 On tax2.tax_code =TSPL_ASSET_SCRAP_HEAD.tax2  " &
                         " left outer join tspl_tax_master as tax3 on tax3.Tax_Code =TSPL_ASSET_SCRAP_HEAD .TAX3  " &
                         " left outer join TSPL_TAX_MASTER As tax4 On tax4.Tax_Code =TSPL_ASSET_SCRAP_HEAD .tax4 " &
                         " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code =TSPL_ASSET_SCRAP_HEAD .tax5 " &
                         " left outer join TSPL_TAX_MASTER As tax6 On tax6.Tax_Code =TSPL_ASSET_SCRAP_HEAD .TAX6 " &
                         " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_ASSET_SCRAP_HEAD .TAX7 " &
                         " left outer join TSPL_TAX_MASTER As tax8 On tax8.Tax_Code =TSPL_ASSET_SCRAP_HEAD .TAX8 " &
                         " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_ASSET_SCRAP_HEAD .TAX9 " &
                         " left outer join TSPL_TAX_MASTER As tax10 On tax10.Tax_Code =TSPL_ASSET_SCRAP_HEAD .TAX10 " &
                         " where TSPL_ASSET_SCRAP_HEAD.Document_No ='" + StrCode + "'"
                    Dim TaxAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(TaxQuery))
                    If TaxAmount > 0 Then
                        IsTaxable = 1
                    End If
                End If
            End If
        End If
        Return IsTaxable
    End Function

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

            Dim strReceiptCount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select receipt_no from TSPL_RECEIPT_DETAIL where Document_No in (Select Document_No from TSPL_Customer_Invoice_Head  where Against_asset_Disposal='" & txtDocNo.Value & "') "))
            If clsCommon.myLen(strReceiptCount) > 0 Then
                Throw New Exception("You cannot cancelled this document because receiving (" + clsCommon.myCstr(strReceiptCount) + ") has been done against its AR Invoice.")
            End If

            If FlagDocumentIsTaxable = 1 AndAlso clsERPFuncationality.GetEInvoiceStatus(dtpshipment.Value) = True AndAlso clsCommon.CompairString(EInvoiceType, "BB") = CompairStringResult.Equal Then
                Dim EInvoiceCancelTimeValid As Int64 = 0
                EInvoiceCancelTimeValid = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select  isnull (DATEDIFF(hour,Posting_Date,GETDATE()),0) as PostedHours from TSPL_ASSET_SCRAP_HEAD where Document_No = '" + txtDocNo.Value + "'"))
                If EInvoiceCancelTimeValid >= 24 Then
                    Throw New Exception("Document can not be cancelled.It has been more than 24 hours.")
                End If
            End If

            clsAssetScrapSaleHead.CancelData(Me.Form_ID, txtDocNo.Value, NavigatorType.Current)
            clsCommon.MyMessageBoxShow(Me, "Successfully Cancelled", Me.Text)
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function

    Private Sub btnInvoiceJE_Click(sender As Object, e As EventArgs) Handles btnInvoiceJE.Click
        clsOpenJEAgainstInvoice.ShowJEAssetDisposal(txtDocNo.Value)
    End Sub

    Private Sub butCostCenterAndHirerachy_Update_AfterPost_Click(sender As Object, e As EventArgs) Handles butCostCenterAndHirerachy_Update_AfterPost.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strJEStatus As String = clsDBFuncationality.getSingleValue("select Authorized from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + txtDocNo.Value + "' ", trans)
            If clsCommon.CompairString(strJEStatus, "A") = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("ALTER TABLE TSPL_JOURNAL_DETAILS DISABLE TRIGGER TRG_JD_FiscaYearEndNoUpdateNoDelete", trans)
            End If

            For Each grow As GridViewRowInfo In gv1.Rows
                Dim coll As New Hashtable()
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colAssetCode).Value)) > 0 Then
                    Dim strAssetCode As String = clsCommon.myCstr(grow.Cells(colAssetCode).Value)
                    clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", clsCommon.myCstr(grow.Cells(colHierarchyCode).Value), True)
                    clsCommon.AddColumnsForChange(coll, "CostCenter_Code", clsCommon.myCstr(grow.Cells(colCostCenterCode).Value), True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_SCRAP_DETAIL", OMInsertOrUpdate.Update, "Document_No='" + txtDocNo.Value + "' and Asset_Code = '" + strAssetCode + "'", trans)
                    Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No  from TSPL_JOURNAL_MASTER where Source_Doc_No = '" + txtDocNo.Value + "' ", trans))
                    If clsCommon.myLen(strVoucherNo) > 0 Then
                        Dim qry As String = "update TSPL_JOURNAL_DETAILS SET Hirerachy_Code='" + clsCommon.myCstr(grow.Cells(colHierarchyCode).Value) + "',Cost_Centre_Code='" + clsCommon.myCstr(grow.Cells(colCostCenterCode).Value) + "' WHERE Voucher_No='" + strVoucherNo + "'  "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                    '=================AR=========================
                    Dim strARDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_Customer_Invoice_Head where Against_Asset_Disposal = '" + txtDocNo.Value + "'", trans))
                    If clsCommon.myLen(strARDocNo) > 0 Then
                        Dim qry As String = " select TSPL_Dep_AccountSet.Ac_Control from TSPL_ACQUISITION_DETAIL left outer join  TSPL_Dep_AccountSet on TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code where TSPL_ACQUISITION_DETAIL.Asset_Code='" & strAssetCode & "'"
                        Dim dtAccount As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If (dtAccount IsNot Nothing AndAlso dtAccount.Rows.Count > 0) Then
                            Dim strAssetCtrlAC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dtAccount.Rows(0)("Ac_Control")), fndLocation.Value, trans)
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_Customer_Invoice_Detail set Hirerachy_Code = '" + clsCommon.myCstr(grow.Cells(colHierarchyCode).Value) + "' , Cost_Centre_Code ='" + clsCommon.myCstr(grow.Cells(colCostCenterCode).Value) + "' where Document_No = '" + strARDocNo + "' and GL_Account_Code = '" + strAssetCtrlAC + "' ", trans)
                            Dim strARVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No  from TSPL_JOURNAL_MASTER where Source_Doc_No = '" + strARDocNo + "' ", trans))
                            Dim strAssetCtrlACForJE As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dtAccount.Rows(0)("Ac_Control")), fndLocation.Value, trans)
                            Dim qryARJE As String = "update TSPL_JOURNAL_DETAILS SET Hirerachy_Code='" + clsCommon.myCstr(grow.Cells(colHierarchyCode).Value) + "',Cost_Centre_Code='" + clsCommon.myCstr(grow.Cells(colCostCenterCode).Value) + "' WHERE Voucher_No='" + strARVoucherNo + "' and Account_code ='" + strAssetCtrlACForJE + "'  "
                            clsDBFuncationality.ExecuteNonQuery(qryARJE, trans)
                        End If

                    End If
                    '============================================
                End If
            Next
            If clsCommon.CompairString(strJEStatus, "A") = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("ALTER TABLE TSPL_JOURNAL_DETAILS ENABLE TRIGGER TRG_JD_FiscaYearEndNoUpdateNoDelete", trans)
            End If
            trans.Commit()
            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
