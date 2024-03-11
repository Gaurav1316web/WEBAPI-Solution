Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls.Data
Imports Telerik.WinControls
Imports Telerik.WinControls.Enumerations

Public Class frmItemIssueToAssembledAsset
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoReqIssueNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoCCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoCCDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoReqQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoReturnQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoAssetCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Const colLineNo As String = "COLLNO"
    Const colReq_IssueNo As String = "Req_IssueNo"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colCCCode As String = "COLCCCode"
    Const colCCDesc As String = "COLCCDesc"
    Const colQty As String = "COLQTY"
    Const colRetQty As String = "COLRetQTY"
    Const colUnit As String = "COLUNIT"
    Const colReqQty As String = "COLREQQTY"
    Const colAssetCode As String = "colAssetCode"
    Const colAssetDate As String = "colAssetDate"

    Const colRate As String = "COLRATE"
    Const colAmt As String = "COLAMT"
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
    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"
    Const colIsSerialseItem As String = "COLISSERIALSEITEM"


    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public DocumentNo As String = Nothing
    '=============added by preeti gupta=====
    Dim ShowCapexCodeandSubCode As Boolean = False
    Const colCheckCapexLimit As String = "colCheckCapexLimit"
    Const colIsCategory As String = "colIsCategory"
    Const colCategoryType As String = "COLCATEGORYTYPE"
    Const colCapexSubCode As String = "COLCAPEXSUBCODE"
    Const colCapexCode As String = "COLCAPEXCODE"
    Const colCapexQty As String = "colCapexQty"
    '================end=========================
#End Region

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmIssueItemsToAsset)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnprint.Visible = MyBase.isPrintFlag
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        btnReverse.Visible = False

    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        '===============================Added by preeti gupta========================================================
        ShowCapexCodeandSubCode = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapexForFA, clsFixedParameterCode.ShowOptionforSelectingCapexForFA, Nothing)) = "1", True, False))
        '============================================================END==================================================================
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A Create Additional Cost")
        'ButtonToolTip.SetToolTip(btnRequistionItems, "Press Ctrl+F7 for Select Purchase Requistion Items")
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadBlankGrid()
        LoadDocType()
        AddNew()
        SetLength()
        LoadAssetType()
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
        If clsCommon.myLen(DocumentNo) > 0 Then
            LoadData(DocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtRemarks.MaxLength = 200
        txtComment.MaxLength = 200

    End Sub

    Sub LoadDocType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Issue"
        dr("Name") = "Issue"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Return"
        dr("Name") = "Return"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "TransferCX"
        dr("Name") = "Transfer Capex"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Sale"
        'dr("Name") = "Sale"
        'dt.Rows.Add(dr)

        cboDocType.DataSource = dt
        cboDocType.ValueMember = "Code"
        cboDocType.DisplayMember = "Name"
    End Sub

    Sub BlankAllControls()
        txtDocNo.Value = ""
        txtComment.Text = ""
        chkOnHold.Checked = False
        txtHCode.Value = ""
        lblHDesc.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtComment.Text = ""
        txtRemarks.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        cboDocType.SelectedIndex = 0
        cboDocType.Enabled = True
        txtRequestBy.Enabled = True
        txtRequestBy.Value = ""
        lblRequestBy.Text = ""
        txtFromLocation.Value = ""
        lblFromLocation.Text = ""
        fndAssetCode.Value = ""
        lblAssetDesc.Text = ""
        'txtDepartment.Value = ""
        'lblDepartment.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        lblAmtAfterDiscount.Text = ""
        lblTaxAmt.Text = ""
        lblTotRAmt.Text = ""
        'TxtVehicle.Value = ""
        'TxtMachinery.Value = ""
        'lblVehicleDesc.Text = ""
        'lblMachineDesc.Text = ""
        txtTaxGroup.Enabled = False
        lblTaxGrpName.Enabled = False
        'fndReqNo.Value = ""
        'lblReqDate.Text = ""

        ''added by priti
        'fndReqNo.Enabled = True
        txtFromLocation.Enabled = True
        fndAssetCode.Enabled = True
        cboDocType.Enabled = True
        'fndReqNo.Visible = True
        'lblReqDate.Visible = True
        'lblReq.Visible = True
        'lblReq.Text = "Requisition No"
        cboAssetType.Enabled = True
        ' ended by priti

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

        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        repoReqIssueNo = New GridViewTextBoxColumn()
        repoReqIssueNo.FormatString = ""
        repoReqIssueNo.HeaderText = "Req. / Issue No"
        repoReqIssueNo.Name = colReq_IssueNo
        repoReqIssueNo.Width = 100
        repoReqIssueNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoReqIssueNo)

        repoAssetCode.FormatString = ""
        repoAssetCode.HeaderText = "Asset Code"
        repoAssetCode.Name = colAssetCode
        repoAssetCode.ReadOnly = False
        '  repoAssetCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoAssetCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAssetCode.Width = 200
        gv1.MasterTemplate.Columns.Add(repoAssetCode)

        repoAssetCode = New GridViewTextBoxColumn
        repoAssetCode.FormatString = ""
        repoAssetCode.HeaderText = "Asset Date"
        repoAssetCode.Name = colAssetDate
        repoAssetCode.ReadOnly = True
        'repoAssetCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoAssetCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAssetCode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoAssetCode)


        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        '  repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 200
        gv1.MasterTemplate.Columns.Add(repoICode)

        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 300
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        repoCCCode.HeaderText = "Cost Center"
        repoCCCode.Name = colCCCode
        '  repoCCCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCCCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCCCode.Width = 100
        repoCCCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoCCCode)

        repoCCDesc.FormatString = ""
        repoCCDesc.HeaderText = "Description"
        repoCCDesc.Name = colCCDesc
        repoCCDesc.Width = 150
        repoCCDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCCDesc)

        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoUnit)

        repoReqQty = New GridViewDecimalColumn()
        repoReqQty.FormatString = ""
        repoReqQty.HeaderText = "Required Quantity"
        repoReqQty.Name = colReqQty
        repoReqQty.Width = 100
        repoReqQty.Minimum = 0
        repoReqQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoReqQty)

        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 100
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        repoReturnQty = New GridViewDecimalColumn()
        repoReturnQty.FormatString = ""
        repoReturnQty.HeaderText = "Return Quantity"
        repoReturnQty.Name = colRetQty
        repoReturnQty.Width = 100
        repoReturnQty.Minimum = 0
        repoReturnQty.IsVisible = False
        repoReturnQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoReturnQty)



        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Unit Cost"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.ReadOnly = False
        repoRate.Minimum = 0
        repoRate.FormatString = "{0:n4}"
        repoRate.DecimalPlaces = 4
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Extended Cost"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

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

        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Serialize Item"
        repoIsSerItem.Name = colIsSerialseItem
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSerItem)

        ''=============== ''done by preeti Gupta=========================================
        Dim repoIsCategory As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsCategory.Checked = ToggleState.Off
        repoIsCategory.HeaderText = "Is Category"
        repoIsCategory.Name = colIsCategory
        repoIsCategory.Width = 50
        repoIsCategory.IsVisible = ShowCapexCodeandSubCode
        repoIsCategory.VisibleInColumnChooser = ShowCapexCodeandSubCode
        repoIsCategory.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIsCategory)

        Dim repoCheckCapexLimit As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoCheckCapexLimit.Checked = ToggleState.Off
        repoCheckCapexLimit.HeaderText = "Check Capex Limit"
        repoCheckCapexLimit.Name = colCheckCapexLimit
        repoCheckCapexLimit.Width = 80
        repoCheckCapexLimit.IsVisible = True
        'repoCheckCapexLimit.VisibleInColumnChooser = ShowCapexCodeandSubCode
        repoCheckCapexLimit.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoCheckCapexLimit)

        Dim repoCategoryType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoCategoryType.FormatString = ""
        repoCategoryType.HeaderText = "Category Type"
        repoCategoryType.Name = colCategoryType
        repoCategoryType.Width = 50
        repoCategoryType.IsVisible = ShowCapexCodeandSubCode
        repoCategoryType.VisibleInColumnChooser = ShowCapexCodeandSubCode
        repoCategoryType.DataSource = LoadCategory()
        repoCategoryType.ValueMember = "Code"
        repoCategoryType.DisplayMember = "Name"
        repoCategoryType.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCategoryType)



        Dim repoCapexSubCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCapexSubCode.FormatString = ""
        repoCapexSubCode.HeaderText = "Capex Sub Code"
        repoCapexSubCode.Name = colCapexSubCode
        '  repoCapexSubCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCapexSubCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCapexSubCode.Width = 100
        repoCapexSubCode.IsVisible = ShowCapexCodeandSubCode
        repoCapexSubCode.VisibleInColumnChooser = ShowCapexCodeandSubCode
        repoCapexSubCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCapexSubCode)

        Dim repoCapexCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCapexCode.FormatString = ""
        repoCapexCode.HeaderText = "Capex Code"
        repoCapexCode.Name = colCapexCode
        ' repoCapexCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCapexCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCapexCode.Width = 100
        repoCapexCode.IsVisible = ShowCapexCodeandSubCode
        repoCapexCode.VisibleInColumnChooser = ShowCapexCodeandSubCode
        repoCapexCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCapexCode)

        Dim repoCapexQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCapexQty.FormatString = ""
        repoCapexQty.HeaderText = "Capex Quantity"
        repoCapexQty.Name = colCapexQty
        repoCapexQty.Width = 100
        repoCapexQty.Minimum = 0
        repoCapexQty.IsVisible = ShowCapexCodeandSubCode
        repoCapexQty.VisibleInColumnChooser = ShowCapexCodeandSubCode
        repoCapexQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCapexQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCapexQty)


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
    End Sub

    Sub OpenSerialItem()
        If Not clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
            If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSerialseItem).Value) Then
                Dim frm As frmSerializeItemOut = New frmSerializeItemOut()
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.strLocationCode = txtFromLocation.Value
                'frm.strCurrDocNo = txtDocNo.Value
                'frm.strCurrDocType = "ISSTRAN"
                frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Tag = frm.arr
                End If
            End If
        End If
    End Sub

    Sub OpenSerialItemReturn()
        If Not clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
            If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSerialseItem).Value) Then
                Dim frm As New frmSerializeItemOutCheckBox() ''= New frmSerializeItemOutCheckBox()
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.strLocationCode = txtFromLocation.Value
                frm.strAgaintsDocNo = fndReqNo.Value
                frm.strCurrDocType = "ISSTRAN"
                frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRetQty).Value)
                frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Tag = frm.arr
                End If
            End If
        End If
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colCategoryType) OrElse e.Column Is gv1.Columns(colCapexSubCode) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colAssetCode) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRetQty) OrElse e.Column Is gv1.Columns(colReqQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colCCCode) Then
                        If gv1.CurrentColumn Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colICode) Then
                            Dim stockqty As Double = 0
                            Dim ActualQty As Double = 0
                            If clsCommon.myLen(txtFromLocation.Value) <> 0 And clsCommon.myLen(clsCommon.myCdbl(gv1.CurrentRow.Cells(colICode).Value)) <> 0 Then
                                Dim str As String = "select sum(Item_Qty) from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "' and Location_Code='" + txtFromLocation.Value + "' "
                                stockqty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
                                Dim item As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                                'If stockqty = 0 Then
                                '    common.clsCommon.MyMessageBoxShow("Stock Qty  not available at this location ")
                                '    gv1.CurrentRow.Cells(colQty).Value = 0
                                'Else
                                If clsCommon.myLen(fndReqNo.Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal And clsCommon.CompairString(btnSave.Text, "Update") = CompairStringResult.Equal Then
                                    str = "SELECT Requisition_Qty - (select isnull(SUM(Issued_Qty),0) from TSPL_IssueItemToAssembledAsset_Head inner join " & _
                                    "TSPL_IssueItemToAssembledAsset_Detail on TSPL_IssueItemToAssembledAsset_Head.Doc_No=TSPL_IssueItemToAssembledAsset_Detail.Doc_No and " & _
                                    "TSPL_IssueItemToAssembledAsset_Head.RequisitionNo=TSPL_REQUISITION_DETAIL.Requisition_Id where Doc_Type='Issue' and " & _
                                    "TSPL_IssueItemToAssembledAsset_Head.Doc_No <> '" & txtDocNo.Value & "' ) +   (select isnull(SUM(Issued_Qty),0) from TSPL_IssueItemToAssembledAsset_Head inner join " & _
                                    "TSPL_IssueItemToAssembledAsset_Detail on TSPL_IssueItemToAssembledAsset_Head.Doc_No=TSPL_IssueItemToAssembledAsset_Detail.Doc_No and " & _
                                    "TSPL_IssueItemToAssembledAsset_Head.RequisitionNo=TSPL_REQUISITION_DETAIL.Requisition_Id where Doc_Type='Return') FROM " & _
                                    "TSPL_REQUISITION_DETAIL"
                                    Dim ReqQty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
                                    ActualQty = ReqQty - clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                                    If ReqQty < clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) Then
                                        common.clsCommon.MyMessageBoxShow(Me, "Qty more then Req qty not allowed,balance qty of  '" + item + "' is '" + clsCommon.myCstr(ReqQty) + "' ")
                                        gv1.CurrentRow.Cells(colQty).Value = 0
                                    End If

                                    'If stockqty < clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) Then
                                    '    common.clsCommon.MyMessageBoxShow("Qty more then stock qty not allowed,balance qty of  '" + item + "' is '" + clsCommon.myCstr(stockqty) + "' ")
                                    '    gv1.CurrentRow.Cells(colQty).Value = 0
                                    'End If
                                Else
                                    'If stockqty < clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) Then
                                    '    common.clsCommon.MyMessageBoxShow("Qty more then stock qty not allowed,balance qty of  '" + item + "' is '" + clsCommon.myCstr(stockqty) + "' ")
                                    '    gv1.CurrentRow.Cells(colQty).Value = 0
                                    'End If
                                End If

                                'End If
                            Else
                                common.clsCommon.MyMessageBoxShow(Me, "Select the Location", Me.Text)
                                gv1.CurrentRow.Cells(colQty).Value = 0
                            End If
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
                            If (e.Column Is gv1.Columns(colQty)) Then
                                OpenSerialItem()
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                            If (e.Column Is gv1.Columns(colRetQty)) Then
                                OpenSerialItemReturn()
                            End If
                        End If




                        If e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colReqQty) OrElse e.Column Is gv1.Columns(colRetQty) OrElse e.Column Is gv1.Columns(colRate) Then

                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateAllTotals()
                            '====================added by preeti gupta=============
                        ElseIf e.Column Is gv1.Columns(colCapexSubCode) Then
                            OpenCapexSubCodeList()
                            '===========================END===========================
                        ElseIf e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        ElseIf e.Column Is gv1.Columns(colAssetCode) Then
                            OpenAssetCode(False)
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        ElseIf e.Column Is gv1.Columns(colCCCode) Then
                            OpenCCList(False)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                        End If
                        setGridFocus()
                    End If
                    If gv1.CurrentRow.Cells(colIsCategory).Value = True Then
                        gv1.Columns(colCategoryType).ReadOnly = False
                        gv1.Columns(colCapexQty).ReadOnly = False
                        gv1.Columns(colCapexSubCode).ReadOnly = False
                        gv1.Columns(colCapexCode).ReadOnly = False
                    Else
                        gv1.Columns(colCategoryType).ReadOnly = True
                        gv1.Columns(colCapexQty).ReadOnly = True
                        gv1.Columns(colCapexSubCode).ReadOnly = True
                        gv1.Columns(colCapexCode).ReadOnly = True
                        gv1.CurrentRow.Cells(colCategoryType).Value = ""
                        gv1.CurrentRow.Cells(colCapexCode).Value = ""
                        gv1.CurrentRow.Cells(colCapexSubCode).Value = ""
                        gv1.CurrentRow.Cells(colCapexQty).Value = 0.0
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenAssetCode(ByVal isButtonClick As Boolean)

        Dim qry As String = "select Asset_Code as Code,Asset_Name as Name,Book_Estimated_Life as [Estimated Life],Book_Source_value as [Book Source Value]," & _
                         " Book_Source_Original_value as [Book Org Value],Item_Net_Amt as [Item Net Value],Book_Salvage_Value as [Book Salvage Value],TSPL_ACQUISITION_DETAIL.IsCapex ,TSPL_ACQUISITION_DETAIL.CapexType ,TSPL_ACQUISITION_DETAIL.Capex_Code ,TSPL_ACQUISITION_DETAIL.Capex_SubCode  " & _
                         " from TSPL_ACQUISITION_DETAIL INNER JOIN TSPL_ACQUISITION_HEAD ON TSPL_ACQUISITION_DETAIL.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code"
        If clsCommon.CompairString(cboAssetType.SelectedValue, "Assembled") = CompairStringResult.Equal Then
            gv1.CurrentRow.Cells(colAssetCode).Value = clsCommon.ShowSelectForm("TSPL_ACQUISITION_DETAIL", qry, "Code", "isnull(TSPL_ACQUISITION_DETAIL.asset_merged,0)<>1 and TSPL_ACQUISITION_DETAIL.Is_Assembled='1' AND TSPL_ACQUISITION_HEAD.STATUS=0 and Acquisition_Type = 'Assembled' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value), "", isButtonClick)
        Else
            gv1.CurrentRow.Cells(colAssetCode).Value = clsCommon.ShowSelectForm("TSPL_ACQUISITION_DETAIL", qry, "Code", "isnull(TSPL_ACQUISITION_DETAIL.asset_merged,0)<>1 and TSPL_ACQUISITION_DETAIL.Is_Assembled='1' AND TSPL_ACQUISITION_HEAD.STATUS=0 and Acquisition_Type <> 'Assembled' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value), "", isButtonClick)
        End If
        gv1.CurrentRow.Cells(colAssetDate).Value = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue("select TSPL_ACQUISITION_HEAD.Acquisition_Date from TSPL_ACQUISITION_DETAIL INNER JOIN TSPL_ACQUISITION_HEAD ON TSPL_ACQUISITION_DETAIL.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code where TSPL_ACQUISITION_DETAIL.Asset_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value) & "'")), "dd-MMM-yyyy")
        '==================Added by preeti gupta=========================================

        gv1.CurrentRow.Cells(colIsCategory).Value = clsDBFuncationality.getSingleValue("select IsCapex from TSPL_ACQUISITION_DETAIL where Asset_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value) & "'")
        gv1.CurrentRow.Cells(colCategoryType).Value = clsDBFuncationality.getSingleValue("select CapexType from TSPL_ACQUISITION_DETAIL where Asset_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value) & "'")
        gv1.CurrentRow.Cells(colCapexSubCode).Value = clsDBFuncationality.getSingleValue("select Capex_SubCode from TSPL_ACQUISITION_DETAIL where Asset_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value) & "'")
        gv1.CurrentRow.Cells(colCapexCode).Value = clsDBFuncationality.getSingleValue("select Capex_Code from TSPL_ACQUISITION_DETAIL where Asset_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value) & "'")
        ' ===================================================================================

    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("IRTItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
        End If
    End Sub

    Private Sub setGridFocus()
        Try
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            'If intCurrRow = gv1.Rows.Count - 1 Then
            '    gv1.Rows.AddNew()
            '    gv1.CurrentRow = gv1.Rows(intCurrRow)
            'End If
            If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer") = CompairStringResult.Equal Then
                        gv1.CurrentColumn = gv1.Columns(colQty)
                    Else
                        gv1.CurrentColumn = gv1.Columns(colReqQty)
                    End If

                ElseIf gv1.CurrentColumn Is gv1.Columns(colReqQty) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    gv1.CurrentColumn = gv1.Columns(colQty)
                ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) AndAlso clsCommon.myLen(fndReqNo.Value) <= 0 Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                    gv1.CurrentColumn = gv1.Columns(colICode)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        'Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", isButtonClick)
        Dim Sql As String = ""
        Dim RecordCount As Integer = 0
        If clsCommon.myLen(fndReqNo.Value) <= 0 Then
            If clsCommon.myLen(txtFromLocation.Value) = 0 Then

                common.clsCommon.MyMessageBoxShow(Me, "Select the from location", Me.Text)
                gv1.CurrentRow.Cells(colICode).Value = ""
                gv1.CurrentRow.Cells(colIName).Value = ""
                gv1.CurrentRow.Cells(colUnit).Value = ""
                gv1.CurrentRow.Cells(colRate).Value = 0

            Else
                Dim obj As ClsScrapSaleDetail = ClsScrapSaleDetail.FinderItemwithBalanceQty(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "F", isButtonClick, txtFromLocation.Value)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                    gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
                    gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
                    gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code

                    Dim strDate As String = txtDate.Value
                    Dim dblUnitCost As Double = 0
                    Dim dblCostMethod As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Costing_Method as Costing_Method from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Item_Code='" & clsCommon.myCstr(obj.Item_Code) & "' "))
                    If dblCostMethod <= 0 Then
                        dblCostMethod = 1
                    End If
                    If dblCostMethod <> 0 Then
                        dblUnitCost = clsInventoryMovement.GetCost(dblCostMethod, clsCommon.myCstr(obj.Item_Code), txtFromLocation.Value, 1, strDate, strDate, False, Nothing)
                        gv1.CurrentRow.Cells(colRate).Value = dblUnitCost
                    Else
                        gv1.CurrentRow.Cells(colRate).Value = 0
                    End If
                    gv1.CurrentRow.Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(obj.Item_Code)

                    'sanjay
                    Sql = "select count(*) as Counts from TSPL_PURCHASE_ORDER_HEAD " & _
                         "inner join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & _
                        "where TSPL_PURCHASE_ORDER_HEAD.Capex_Code Is Not null " & _
                        "and TSPL_PURCHASE_ORDER_HEAD.Capex_SubCode is not null " & _
                        "and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + obj.Item_Code + "'"
                    RecordCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sql))
                    If RecordCount > 0 Then
                        gv1.CurrentRow.Cells(colCheckCapexLimit).Value = False
                    Else
                        gv1.CurrentRow.Cells(colCheckCapexLimit).Value = True
                    End If
                    'sanjay
                Else
                    gv1.CurrentRow.Cells(colICode).Value = ""
                    gv1.CurrentRow.Cells(colIName).Value = ""
                    gv1.CurrentRow.Cells(colUnit).Value = ""
                    gv1.CurrentRow.Cells(colRate).Value = 0
                End If
                '==============================
                '    Dim srtcost As String = "select  convert(decimal(18,2),sum(case when  Item_Qty =0 or Amount=0 then 0 else  (Amount/Item_Qty )end)) as cost  from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + obj.Item_Code + "' and location_code='" + txtFromLocation.Value + "'  "
                '    Dim cost As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(srtcost))
                '    'cost = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, obj.Item_Code, Me.txtFromLocation.Value, gv1.CurrentRow.Cells(colQty).Value, txtDate.Value, clsCommon.GETSERVERDATE(), False, Nothing)
                '    ''COMMENTED BY PRITI
                '    'If cost <= 0 Then
                '    '    common.clsCommon.MyMessageBoxShow(" '" + obj.Item_Code + "' item don't have unit cost")
                '    'End If
                '    gv1.CurrentRow.Cells(colRate).Value = cost
                '    gv1.CurrentRow.Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(obj.Item_Code)
                'Else
                '    gv1.CurrentRow.Cells(colICode).Value = ""
                '    gv1.CurrentRow.Cells(colIName).Value = ""
                '    gv1.CurrentRow.Cells(colUnit).Value = ""
                '    gv1.CurrentRow.Cells(colRate).Value = 0
                'End If
                '================================
                SetitemWiseTaxSetting(True, True)
            End If
        Else

            SetitemWiseTaxSetting(True, True)
        End If
        setBalance()
    End Sub

    Private Sub OpenCCList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Cost_Code as Code,Cost_name as Name from  TSPL_CostCenter_MASTER"
        gv1.CurrentRow.Cells(colCCCode).Value = clsCommon.ShowSelectForm("TSPL_CostCenter_MASTER", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colCCCode).Value), "", isButtonClick)
        gv1.CurrentRow.Cells(colCCDesc).Value = ClsCostCenter.GetCostCenterDesc(gv1.CurrentRow.Cells(colCCCode).Value)
    End Sub

    Private Sub UpdateAllTotals()
        Dim dblTotAmt As Double = 0

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
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
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
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterTax).Value)
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
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)

    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        lblReq2.Visible = False
        lblReq3.Visible = False
        fndReqNo.Value = Nothing
        BlankAllControls()
        LoadBlankGrid()
        LoadBlankGridTax()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        If clsCommon.myLen(fndReqNo.Value) <= 0 Then
            gv1.Rows.AddNew()
        End If
        chkWithoutRefNo.Checked = False
        chkWithoutRefNo.Enabled = False
        chkWithoutRefNoChanged()

        txtHCode.Value = Nothing
        lblHDesc.Text = ""
        txtCostCenter.Value = Nothing
        lblCostCenterDesc.Text = ""
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

            '===============Preeti Gupta==================================
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Select()
                Return False
            End If
            '=======================================================
            If btnSave.Text = "Update" Then
                Dim strchk As String = "select Status from TSPL_IssueItemToAssembledAsset_Head where Doc_No='" + txtDocNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    Throw New Exception("Transection already posted")
                End If
            End If
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next

            UpdateAllTotals()





            If clsCommon.myLen(txtFromLocation.Value) <= 0 Then
                txtFromLocation.Focus()
                Throw New Exception("Please Enter From Location")
            End If

            'If clsCommon.myLen(fndAssetCode.Value) <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please Enter Asset Code to Issue Items")
            '    fndAssetCode.Focus()
            '    Return False
            'End If
            'If clsCommon.CompairString(txtFromLocation.Value, fndAssetCode.Value) = CompairStringResult.Equal Then
            '    common.clsCommon.MyMessageBoxShow("From Location and To Location should not be same")
            '    fndAssetCode.Focus()
            '    Return False
            'End If

            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                txtDocNo.Focus()
                Throw New Exception("Document No Not found to save")
            End If
            If clsCommon.myLen(clsCommon.myCstr(cboDocType.SelectedValue)) <= 0 Then
                cboDocType.Focus()
                Throw New Exception("Please select Document Type")
            End If


            '-----------Added By ---Pankaj Kumar---on-----04/05/2012--For Inserting Vehicle Mannually----------

            'If clsCommon.myLen(TxtVehicle.Value) <= 0 Then
            '    'common.clsCommon.MyMessageBoxShow("Please select Cost Center value")
            '    'TxtVehicle.Focus()
            '    'Return False
            'Else
            '    If clsCommon.myLen(TxtVehicle.Value) > 0 Then
            '        Dim count As Decimal = 0
            '        Dim segno As String = String.Empty
            '        Dim strvehiclenum As String = clsCommon.myCstr(TxtVehicle.Value)
            '        Dim Sql As String = "select Description from TSPL_GL_SEGMENT_CODE where segment_code  = '" + Convert.ToString(TxtVehicle.Value) + "' or Description = '" + Convert.ToString(TxtVehicle.Value) + "'"
            '        If Not String.IsNullOrEmpty(connectSql.RunScalar(Sql)) Then

            '        Else
            '            Dim strmessage As String = "This vehicle code doesn't exist" + Environment.NewLine
            '            Dim strVehicalNo As String = clsCommon.myCstr(TxtVehicle.Value)
            '            strmessage += "Do you want to continue "
            '            If common.clsCommon.MyMessageBoxShow(strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            '                count = connectSql.RunScalar("select COUNT(*) from TSPL_GL_SEGMENT_CODE where Segment_name = 'Vehicles'")
            '                TxtVehicle.Value = Convert.ToString(count + 1) + "-Man"
            '                Sql = "select seg_no from tspl_gl_segment where seg_name='Vehicles'"
            '                segno = CStr(connectSql.RunScalar(Sql))
            '                connectSql.RunSpTransaction("sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", segno), New SqlParameter("@segmentname", "Vehicles"), New SqlParameter("@segmentcode", clsCommon.myCstr(TxtVehicle.Value)), New SqlParameter("@desc", strVehicalNo), New SqlParameter("@acccode", "NULL"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
            '                connectSql.RunSpTransaction("SP_TSPL_VEHICLE_MASTER_INSERT", New SqlParameter("@Vehicle_Id", clsCommon.myCstr(TxtVehicle.Value)), New SqlParameter("@Model", ""), New SqlParameter("@Number", strvehiclenum), New SqlParameter("@Description", strvehiclenum), New SqlParameter("@Type", "H"), New SqlParameter("@Start_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@End_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Vehicle_Reg_No", ""), New SqlParameter("@Vehicle_Chesis_No", ""), New SqlParameter("@Capacity", ""), New SqlParameter("@Insurance", ""), New SqlParameter("@Pollution_Check", ""), New SqlParameter("@Fitness", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Trans_Type", ""), New SqlParameter("@Road_Tax", ""), New SqlParameter("@Transport_Id", ""), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modified_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modified_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
            '                lblVehicleDesc.Text = strvehiclenum
            '            Else
            '                TxtVehicle.Value = String.Empty
            '                Return False
            '            End If
            '        End If
            '    End If
            'End If
            '---------------------------------------Code Ends Here-------------------------------------------

            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer") = CompairStringResult.Equal Then
                For ii As Integer = 0 To gv1.Rows.Count - 1

                    Dim Icode As String = gv1.Rows(ii).Cells(colICode).Value
                    Dim CCCode As String = gv1.Rows(ii).Cells(colCCCode).Value
                    For jj As Integer = 0 To ii - 1
                        Dim Icode1 As String = gv1.Rows(jj).Cells(colICode).Value
                        Dim CCCode1 As String = gv1.Rows(jj).Cells(colCCCode).Value
                        If clsCommon.myLen(Icode) > 0 Then
                            If clsCommon.CompairString(Icode + CCCode, Icode1 + CCCode1) = CompairStringResult.Equal Then
                                Throw New Exception("'Item' And 'Cost Code' are repeated on line '" + clsCommon.myCstr(jj + 1) + "'AND '" + clsCommon.myCstr(ii + 1) + "'")
                                'Return False
                            End If
                        End If
                    Next
                Next
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colRetQty).Value) > clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) Then
                        Throw New Exception("Return Qty can not be greater than Issue Qty at row no-" & (ii + 1) & " ")
                        'Return False
                    ElseIf clsCommon.myCdbl(gv1.Rows(ii).Cells(colRetQty).Value) <= 0 Then
                        Throw New Exception("Return Qty can not be zero at row no-" & (ii + 1) & " ")
                        'Return False
                    End If
                Next
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    Dim Icode As String = gv1.Rows(ii).Cells(colICode).Value
                    If clsCommon.myLen(Icode) <= 0 Then
                        Continue For
                    End If
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) > clsCommon.myCdbl(gv1.Rows(ii).Cells(colReqQty).Value) Then
                        Throw New Exception("Transfer capex Qty can not be greater than return Qty at row no-" & (ii + 1) & " ")
                    ElseIf clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) <= 0 Then
                        If clsCommon.myCBool(chkWithoutRefNo.Checked) = False Then
                            Throw New Exception("Transfer capex Qty can not be zero at row no-" & (ii + 1) & " ")
                        End If
                    End If
                Next
            Else
                For ii As Integer = 0 To gv1.Rows.Count - 1

                    Dim Icode As String = gv1.Rows(ii).Cells(colICode).Value
                    Dim CCCode As String = gv1.Rows(ii).Cells(colCCCode).Value
                    Dim itemL As Integer = clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value)
                    Dim qq As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                    Dim Reqqty As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colReqQty).Value)
                    If itemL <= 0 Then
                    Else
                        If Reqqty <> 0 Then
                            If qq = 0 Then
                                Throw New Exception("Qty can not be zero for '" + Icode + "' Item ")
                                'Return False
                            End If
                        End If
                    End If
                    For jj As Integer = 0 To ii - 1
                        Dim Icode1 As String = gv1.Rows(jj).Cells(colICode).Value
                        Dim CCCode1 As String = gv1.Rows(jj).Cells(colCCCode).Value
                        If clsCommon.myLen(Icode) > 0 Then
                            If clsCommon.CompairString(Icode + CCCode, Icode1 + CCCode1) = CompairStringResult.Equal Then
                                Throw New Exception("'Item' And 'Cost Code' are repeated on line '" + clsCommon.myCstr(jj + 1) + "'AND '" + clsCommon.myCstr(ii + 1) + "'")
                                'Return False
                            End If
                        End If
                    Next
                Next
            End If
            Dim arrSubCapexCode As New ArrayList
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblQty As Double = 0
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
                    dblQty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                    dblQty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRetQty).Value)
                    Continue For '' no need of balance check in case of in type as well as capex limit
                    'AndAlso cboDocType.SelectedValue = "Issue"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
                    Continue For ''No need to check balance for transfer capex
                End If
                If clsCommon.myCDate(gv1.Rows(ii).Cells(colAssetDate).Value) > txtDate.Value Then
                    Throw New Exception("Asset Code - " & gv1.Rows(ii).Cells(colAssetCode).Value & " at line no- " & (ii + 1) & " is acquired on -" & gv1.Rows(ii).Cells(colAssetDate).Value & ". It can be assembled on or after acquisition date. ")
                End If
                If clsCommon.myLen(strICode) > 0 Then
                    If clsCommon.myLen(strUOM) <= 0 Then
                        Throw New Exception("Please enter UOM of Item - " + strICode + ".At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        'Return False
                    End If
                    ''For RM Other balance Qty check And works only for one unit.
                    Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                    Dim dblBalQty As Double = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(strICode, txtFromLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, strUOM)
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
                        'Return False
                    End If

                    If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsSerialseItem).Value) Then
                        Dim arrSerailNo As List(Of clsSerializeInvenotry) = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                        If arrSerailNo Is Nothing OrElse dblQty <> arrSerailNo.Count Then
                            Throw New Exception("Please provice serial no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                            'Return False
                        End If
                    End If
                End If
                '=============================added by preeti gupta==================================

                If ShowCapexCodeandSubCode Then

                    If clsCommon.myLen(strICode) > 0 Then

                        If gv1.Rows(ii).Cells(colIsCategory).Value = True Then

                            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colCategoryType).Value), "") = CompairStringResult.Equal Then
                                clsCommon.MyMessageBoxShow(Me, "please select Capex Type.", Me.Text)
                                Return False
                            End If

                            If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexCode).Value)) <= 0 Then
                                clsCommon.MyMessageBoxShow(Me, "please select capex code. At Line No" + clsCommon.myCstr(ii + 1))
                                Return False
                            End If
                            If gv1.Rows(ii).Cells(colCheckCapexLimit).Value = True Then
                                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colCategoryType).Value), "Capex") = CompairStringResult.Equal AndAlso gv1.Rows(ii).Cells(colIsCategory).Value = True Then
                                    If arrSubCapexCode.Contains(clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexSubCode).Value)) Then
                                        Continue For
                                    Else
                                        arrSubCapexCode.Add(clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexSubCode).Value))
                                    End If
                                    Dim dtcheck As New DataTable()
                                    dtcheck = ChkLimitBudget(ii)

                                    'Dim ChkCapexQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCapexQty).Value)
                                    'Dim ChkItemCost As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
                                    'Dim ChkCostRate As Double = ChkCapexQty * ChkItemCost

                                    ' VALIDATION COMMENTED BY PANCH RAJ ON SAYING RANJANA MAM:MANISH
                                    Dim ChkCostRate As Double = GetDocSubCapexValue(clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexSubCode).Value))
                                    If dtcheck IsNot Nothing AndAlso dtcheck.Rows.Count > 0 Then
                                        If clsCommon.myCdbl(dtcheck.Rows(1)(0)) < ChkCostRate AndAlso clsCommon.myCdbl(dtcheck.Rows(2)(0)) > ChkCostRate Then
                                            clsCommon.MyMessageBoxShow(Me, "Warning:  Amount exceed budget amount but under tolerence limit for sub capex Code-" & clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexSubCode).Value) & " at Line No" + clsCommon.myCstr(ii + 1))
                                        End If
                                        If clsCommon.myCdbl(dtcheck.Rows(2)(0)) < ChkCostRate Then
                                            clsCommon.MyMessageBoxShow(Me, "Amount exceed budget amount and above tolerence limit for sub capex Code-" & clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexSubCode).Value) & " at Line No" + clsCommon.myCstr(ii + 1))
                                            Return False
                                        End If
                                    End If
                                End If
                            End If
                        End If

                    End If
                End If

                '====================end here================

                '====================================================================================
            Next
            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub SaveData(ByVal ChekPostBTn As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsItemIssueToAssembledAsset()
                obj.Doc_No = txtDocNo.Value
                obj.Doc_Date = txtDate.Value
                obj.Doc_Type = clsCommon.myCstr(cboDocType.SelectedValue)
                obj.Issue_To = txtHCode.Value
                obj.Request_By = txtRequestBy.Value
                obj.Remarks = txtRemarks.Text
                obj.Comment = txtComment.Text
                obj.On_Hold = chkOnHold.Checked
                obj.From_Location = txtFromLocation.Value
                obj.Asset_Code = fndAssetCode.Value
                If clsCommon.CompairString(obj.Doc_Type, "TransferCX") = CompairStringResult.Equal Then
                    obj.Against_Return_No = fndReqNo.Value
                Else
                    obj.Req_IssueNo = fndReqNo.Value
                End If
                obj.RequisitionNo = lblReq3.Text
                obj.Asset_Type = clsCommon.myCstr(cboAssetType.SelectedValue)
                'obj.Dept = txtDepartment.Value
                'obj.Dept_Desc = lblDepartment.Text

                obj.Tax_Group = txtTaxGroup.Value
                obj.Tax_Desc = lblTaxGrpName.Text
                obj.H_Code = txtHCode.Value
                obj.CC_Code = txtCostCenter.Value

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
                obj.BeforeTax_Amt = lblAmtAfterDiscount.Text
                obj.Total_Tax_Amt = lblTaxAmt.Text
                obj.doc_Amt = lblTotRAmt.Text
                obj.Vehicle_Id = ""
                'obj.Machine_Id = clsCommon.myCstr(TxtMachinery.Value)

                obj.Arr = New List(Of clsItemIssueToAssembledAssetDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsItemIssueToAssembledAssetDetail()
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Req_IssueNo = clsCommon.myCstr(grow.Cells(colReq_IssueNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.Cost_Code = clsCommon.myCstr(grow.Cells(colCCCode).Value)
                    objTr.Required_Qty = clsCommon.myCdbl(grow.Cells(colReqQty).Value)

                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)

                    If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                        If clsCommon.myLen(fndReqNo.Value) <= 0 Then
                            objTr.Issued_Qty = clsCommon.myCdbl(grow.Cells(colRetQty).Value)
                            objTr.Issued_Qty_AgainstRet = 0
                        Else
                            objTr.Issued_Qty = clsCommon.myCdbl(grow.Cells(colRetQty).Value)
                            objTr.Issued_Qty_AgainstRet = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
                        objTr.Issued_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    Else
                        objTr.Issued_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Issued_Qty_AgainstRet = clsCommon.myCdbl(grow.Cells(colRetQty).Value)
                    End If
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

                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                    objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)
                    objTr.Asset_Code = clsCommon.myCstr(grow.Cells(colAssetCode).Value)
                    '==========================added by preeti gupta======================
                    objTr.IsCapex = clsCommon.myCdbl(grow.Cells(colIsCategory).Value)
                    objTr.CheckCapexLimit = clsCommon.myCdbl(grow.Cells(colCheckCapexLimit).Value)
                    objTr.CapexType = clsCommon.myCstr(grow.Cells(colCategoryType).Value)
                    objTr.Capex_Code = clsCommon.myCstr(grow.Cells(colCapexCode).Value)
                    objTr.Capex_SubCode = clsCommon.myCstr(grow.Cells(colCapexSubCode).Value)
                    objTr.CapexQty = clsCommon.myCdbl(grow.Cells(colCapexQty).Value)
                    '=====================================================================
                    objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))
                    If clsCommon.myLen(objTr.Item_Code) > 0 Then
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

                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Doc_No)
                    If ChekPostBTn = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If

                    LoadData(obj.Doc_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True

            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()
            LoadBlankGridTax()

            Dim obj As New clsItemIssueToAssembledAsset()
            obj = clsItemIssueToAssembledAsset.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_No) > 0) Then
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Doc_No
                txtDate.Value = obj.Doc_Date
                txtHCode.Value = obj.Issue_To
                lblHDesc.Text = obj.Issue_ToName
                txtRequestBy.Value = obj.Request_By
                lblRequestBy.Text = obj.Request_ByName
                chkOnHold.Checked = obj.On_Hold
                txtComment.Text = obj.Comment
                txtRemarks.Text = obj.Remarks
                cboDocType.Enabled = False
                cboDocType.SelectedValue = obj.Doc_Type
                cboAssetType.Enabled = False
                cboAssetType.SelectedValue = obj.Asset_Type
                txtFromLocation.Value = obj.From_Location
                lblFromLocation.Text = obj.From_LocationName
                fndAssetCode.Value = obj.Asset_Code
                lblAssetDesc.Text = obj.Asset_Desc
                txtHCode.Value = obj.H_Code
                txtCostCenter.Value = obj.CC_Code
                lblHDesc.Text = ClsHirerachyLevelMaster.GetName(obj.H_Code, Nothing)
                lblCostCenterDesc.Text = ClsCostCentreFinancial.GetName(obj.CC_Code, Nothing)

                If clsCommon.CompairString(obj.Doc_Type, "TransferCX") = CompairStringResult.Equal Then
                    fndReqNo.Value = obj.Against_Return_No
                Else
                    fndReqNo.Value = obj.Req_IssueNo
                    lblReq3.Text = obj.RequisitionNo
                End If


                If clsCommon.myLen(fndReqNo.Value) > 0 Then
                    LoadReqDataHead(fndReqNo.Value)
                    gv1.Rows.Clear()
                End If
                'txtDepartment.Value = obj.Dept
                'lblDepartment.Text = obj.Dept_Desc

                txtTaxGroup.Value = obj.Tax_Group
                lblTaxGrpName.Text = obj.Tax_Desc
                lblAmtAfterDiscount.Text = obj.BeforeTax_Amt
                lblTaxAmt.Text = obj.Total_Tax_Amt
                lblTotRAmt.Text = obj.doc_Amt
                'TxtVehicle.Value = obj.Vehicle_Id
                'lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Description From TSPL_GL_SEGMENT_CODE Where  Segment_Code= '" + TxtVehicle.Value + "'")
                'TxtMachinery.Value = obj.Machine_Id
                'lblMachineDesc.Text = clsDBFuncationality.getSingleValue("select Description From TSPL_GL_SEGMENT_CODE Where Seg_No= '5' AND Segment_Code= '" + TxtMachinery.Value + "'")


                'added by priti
                fndReqNo.Enabled = False
                txtFromLocation.Enabled = False
                'added by priti on 25/07/2013  to allow  for wrong entry
                'txtToLocation.Enabled = False
                cboDocType.Enabled = False
                If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
                    lblReq2.Visible = False
                    lblReq3.Visible = False
                    lblReq.Visible = True
                    fndReqNo.Visible = True
                    lblReqDate.Visible = True
                    lblReq.Text = "Requisition No"
                    repoReqIssueNo.HeaderText = "Requisition No"
                    chkWithoutRefNo.Enabled = False
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                    lblReq2.Visible = True
                    lblReq3.Visible = True
                    lblReq.Visible = True
                    fndReqNo.Visible = True
                    lblReqDate.Visible = True
                    lblReq.Text = "Issue No"
                    repoReqIssueNo.HeaderText = "Issue No"
                    If clsCommon.myLen(obj.Req_IssueNo) <= 0 Then
                        chkWithoutRefNo.Checked = True
                        chkWithoutRefNo.Enabled = True
                        chkWithoutRefNoChanged()
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
                    lblReq2.Visible = True
                    lblReq3.Visible = True
                    lblReq.Visible = True
                    fndReqNo.Visible = True
                    lblReqDate.Visible = True
                    lblReq.Text = "Return No"
                    repoReqIssueNo.HeaderText = "Return No"

                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer") = CompairStringResult.Equal Then
                    lblReq2.Visible = False
                    lblReq3.Visible = False
                    lblReq.Visible = False
                    fndReqNo.Visible = False
                    lblReqDate.Visible = False
                    chkWithoutRefNo.Enabled = False
                End If
                ' ended by priti

                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(obj.Tax_Group)
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



                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsItemIssueToAssembledAssetDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Tag = objTr.arrSrItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReq_IssueNo).Value = objTr.Req_IssueNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCCCode).Value = objTr.Cost_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCCDesc).Value = ClsCostCenter.GetCostCenterDesc(objTr.Cost_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = objTr.Required_Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Unit_Cost

                        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "return") = CompairStringResult.Equal Then
                            If clsCommon.myLen(fndReqNo.Value) <= 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = 0
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = objTr.Issued_Qty
                            Else
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Issued_Qty_AgainstRet
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = objTr.Issued_Qty
                            End If
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Issued_Qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = objTr.Issued_Qty_AgainstRet
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetCode).Value = objTr.Asset_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetDate).Value = objTr.Asset_Date
                        '==================added by preeti gupta===========================================
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsCategory).Value = objTr.IsCapex
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCheckCapexLimit).Value = objTr.CheckCapexLimit
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = objTr.CapexType
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = objTr.Capex_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = objTr.Capex_SubCode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexQty).Value = objTr.CapexQty
                        '===================================================================================
                        If clsCommon.CompairString(obj.Doc_Type, "TransferCX") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = clsItemIssueToAssembledAssetDetail.GetBalanceQtyReturnForTranfserCapex(obj.Doc_No, obj.Against_Return_No, objTr.Item_Code, Nothing)
                        End If
                    Next
                    If obj.Status = ERPTransactionStatus.Pending AndAlso clsCommon.myLen(fndReqNo.Value) <= 0 Then
                        gv1.Rows.AddNew()
                    End If
                End If

                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Doc_No)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.Doc_No, MyBase.Form_ID, gv1)
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.Doc_No)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SaveData(True)
                '' commented for urgent checkin
                If (clsItemIssueToAssembledAsset.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                    ''If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                    ''    print()
                    ''End If
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
                If (clsItemIssueToAssembledAsset.DeleteData(txtDocNo.Value)) Then
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

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_IssueItemToAssembledAsset_Head where Doc_No='" + txtDocNo.Value + "'"
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
    '==Added by preeti Gupta Against No[UDL/10/01/19-000251]
    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select Doc_No as Code,Doc_Date as Date,Doc_Type as Type,case when Status='0' then 'Pending' else 'Approved' end as [Status],TSPL_IssueItemToAssembledAsset_Head.Req_IssueNo  as [Req Issue No],TSPL_IssueItemToAssembledAsset_Head.RequisitionNo as [Requisition No] from TSPL_IssueItemToAssembledAsset_Head"
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " from_location in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        LoadData(clsCommon.ShowSelectForm("IRTCodeFilter", qry, "Code", whrClas, txtDocNo.Value, "TSPL_IssueItemToAssembledAsset_Head.Doc_Date desc", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
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

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gv1.Columns(colUnit) Then
                    If clsCommon.myLen(fndReqNo.Value) <= 0 Then
                        gv1.CurrentRow.Cells(colUnit).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colUnit).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colICode) Then
                    If clsCommon.myLen(fndReqNo.Value) <= 0 Then
                        gv1.CurrentRow.Cells(colICode).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colICode).ReadOnly = True
                    End If
                End If
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print()
    End Sub

    Public Sub Print()
        Try
            '            Dim qry As String = "SELECT     TSPL_IssueItemToAssembledAsset_Head.Doc_No, TSPL_IssueItemToAssembledAsset_Head.Doc_Date,TSPL_IssueItemToAssembledAsset_Head.Doc_Type, TSPL_IssueItemToAssembledAsset_Head.Remarks, TSPL_IssueItemToAssembledAsset_Head.Comment, " & _
            '                     " case when  TSPL_IssueItemToAssembledAsset_Head.Status=0 then 'Pending' else 'Approved' end as Status, TSPL_IssueItemToAssembledAsset_Head.Posting_Date, TSPL_IssueItemToAssembledAsset_Detail.Item_Code,  " & _
            '                      " TSPL_IssueItemToAssembledAsset_Detail.Item_Desc, TSPL_IssueItemToAssembledAsset_Detail.Required_Qty, TSPL_IssueItemToAssembledAsset_Detail.Issued_Qty,  " & _
            '                      " TSPL_IssueItemToAssembledAsset_Detail.Unit_code, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2,  " & _
            '                      " TSPL_COMPANY_MASTER.Add3, TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2,  " & _
            '                      " loc1.Location_Desc as Fromloc,loc2.Location_Desc as Toloc, emp1.Emp_Name as IssuesTo,emp2.Emp_Name as RequestBy  " & _
            '    " FROM  TSPL_IssueItemToAssembledAsset_Head INNER JOIN TSPL_IssueItemToAssembledAsset_Detail ON TSPL_IssueItemToAssembledAsset_Head.Doc_No = TSPL_IssueItemToAssembledAsset_Detail.Doc_No  " & _
            '" LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp1 ON TSPL_IssueItemToAssembledAsset_Head.Issue_To = emp1.EMP_CODE  " & _
            '"  LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp2 ON TSPL_IssueItemToAssembledAsset_Head.Request_By = emp2.EMP_CODE  " & _
            '"   LEFT OUTER JOIN  TSPL_LOCATION_MASTER as loc1 ON TSPL_IssueItemToAssembledAsset_Head.From_Location = loc1.Location_Code  " & _
            ' "   LEFT OUTER JOIN  TSPL_LOCATION_MASTER  as loc2 ON TSPL_IssueItemToAssembledAsset_Head.To_Location = loc2.Location_Code  " & _
            ' "   LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_IssueItemToAssembledAsset_Head.comp_code = TSPL_COMPANY_MASTER.Comp_Code  " & _
            '"  where TSPL_IssueItemToAssembledAsset_Head.Doc_No='" + txtDocNo.Value + "'"
            ''------------------------------ Changes by shipra on 24/06/13--------------------------------------------''

            ''Dim qry As String = " SELECT     TSPL_IssueItemToAssembledAsset_Head.Doc_No, TSPL_IssueItemToAssembledAsset_Head.Doc_Date,TSPL_IssueItemToAssembledAsset_Head.Doc_Type, TSPL_IssueItemToAssembledAsset_Head.Remarks, TSPL_IssueItemToAssembledAsset_Head.Comment,  case when  TSPL_IssueItemToAssembledAsset_Head.Status=0 then 'Pending' else 'Approved' end as Status, TSPL_IssueItemToAssembledAsset_Head.Posting_Date, TSPL_IssueItemToAssembledAsset_Detail.Item_Code,   TSPL_IssueItemToAssembledAsset_Detail.Item_Desc, TSPL_IssueItemToAssembledAsset_Detail.Required_Qty, TSPL_IssueItemToAssembledAsset_Detail.Issued_Qty as returnqty,   TSPL_IssueItemToAssembledAsset_Detail.Unit_code, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2,   TSPL_COMPANY_MASTER.Add3, TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2, loc1.Location_Desc as Fromloc,loc2.Location_Desc as Toloc, emp1.Emp_Name as IssuesTo,emp2.Emp_Name as RequestBy,(select xxxx.Issued_Qty  from TSPL_IssueItemToAssembledAsset_Detail  xxxx where xxxx.Doc_No=TSPL_IssueItemToAssembledAsset_Head.Req_IssueNo and xxxx.Item_Code=TSPL_IssueItemToAssembledAsset_Detail .Item_Code  )as [Issued_Qty]     FROM  TSPL_IssueItemToAssembledAsset_Head "
            ''qry += " INNER JOIN TSPL_IssueItemToAssembledAsset_Detail ON TSPL_IssueItemToAssembledAsset_Head.Doc_No = TSPL_IssueItemToAssembledAsset_Detail.Doc_No"
            ''qry += " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_IssueItemToAssembledAsset_Head.comp_code = TSPL_COMPANY_MASTER.Comp_Code"
            ''qry += " LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp1 ON TSPL_IssueItemToAssembledAsset_Head .Issue_To = emp1.EMP_CODE  "
            ''qry += " LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp2 ON TSPL_IssueItemToAssembledAsset_Head.Request_By = emp2.EMP_CODE    "
            ''qry += " LEFT OUTER JOIN  TSPL_LOCATION_MASTER as loc1 ON TSPL_IssueItemToAssembledAsset_Head.From_Location = loc1.Location_Code"
            ''qry += " LEFT OUTER JOIN  TSPL_LOCATION_MASTER  as loc2 ON TSPL_IssueItemToAssembledAsset_Head.To_Location = loc2.Location_Code    "
            ''qry += " where TSPL_IssueItemToAssembledAsset_Head.Doc_No='" + txtDocNo.Value + "'"

            Dim qry As String = "     SELECT  TSPL_CostCenter_MASTER.cost_name,  TSPL_IssueItemToAssembledAsset_Head.Created_By ,TSPL_IssueItemToAssembledAsset_Head.Modify_By ,   TSPL_IssueItemToAssembledAsset_Head.Doc_No, TSPL_IssueItemToAssembledAsset_Head.Doc_Date,TSPL_IssueItemToAssembledAsset_Head.Doc_Type, TSPL_IssueItemToAssembledAsset_Head.Remarks, TSPL_IssueItemToAssembledAsset_Head.Comment,  case when  TSPL_IssueItemToAssembledAsset_Head.Status=0 then 'Pending' else 'Approved' end as Status, TSPL_IssueItemToAssembledAsset_Head.Posting_Date, TSPL_IssueItemToAssembledAsset_Detail.Item_Code,   TSPL_IssueItemToAssembledAsset_Detail.Item_Desc, TSPL_IssueItemToAssembledAsset_Detail.Required_Qty, TSPL_IssueItemToAssembledAsset_Detail.Issued_Qty_AgainstRet as returnqty,   TSPL_IssueItemToAssembledAsset_Detail.Unit_code, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2,   TSPL_COMPANY_MASTER.Add3, TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2, loc1.Location_Desc as Fromloc,loc2.Asset_Code as Toloc, emp1.Emp_Name as IssuesTo,emp2.Emp_Name as RequestBy,"
            'qry += " --(select xxxx.Issued_Qty  from TSPL_IssueItemToAssembledAsset_Detail  xxxx where xxxx.Doc_No=TSPL_IssueItemToAssembledAsset_Head.Req_IssueNo and xxxx.Item_Code=TSPL_IssueItemToAssembledAsset_Detail .Item_Code  )"
            qry += " TSPL_IssueItemToAssembledAsset_Detail.Issued_Qty as [Issued_Qty]     FROM  TSPL_IssueItemToAssembledAsset_Head  INNER JOIN TSPL_IssueItemToAssembledAsset_Detail ON TSPL_IssueItemToAssembledAsset_Head.Doc_No = TSPL_IssueItemToAssembledAsset_Detail.Doc_No LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_IssueItemToAssembledAsset_Head.comp_code = TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp1 ON TSPL_IssueItemToAssembledAsset_Head .Issue_To = emp1.EMP_CODE   LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp2 ON TSPL_IssueItemToAssembledAsset_Head.Request_By = emp2.EMP_CODE     LEFT OUTER JOIN  TSPL_LOCATION_MASTER as loc1 ON TSPL_IssueItemToAssembledAsset_Head.From_Location = loc1.Location_Code LEFT OUTER JOIN  TSPL_ACQUISITION_DETAIL  as loc2 ON TSPL_IssueItemToAssembledAsset_Detail.Asset_Code = loc2.Asset_Code              LEFT OUTER JOIN  TSPL_CostCenter_MASTER   ON TSPL_IssueItemToAssembledAsset_Detail.Cost_Code  = TSPL_CostCenter_MASTER.Cost_Code  "
            qry += " where TSPL_IssueItemToAssembledAsset_Head.Doc_No='" + txtDocNo.Value + "'"



            ''------------------------------ Changes by shipra on 24/06/13--------------------------------------------''

            ''Dim demo As String
            ''If demo = "" Then
            ''    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            ''    demo = dt.Rows(0)("Doc_type").ToString


            ''End If

            Dim frm As New frmCrystalReportViewer()
            Dim type As String = "select Doc_type from TSPL_IssueItemToAssembledAsset_Head where Doc_No='" + txtDocNo.Value + "'"
            Dim val As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(type))
            'Dim dr As SqlDataReader
            'dr = connectSql.RunSqlReturnDR(type)
            'While dr.Read()
            '    val = dr(0).ToString
            'End While

            If val = "Issue" Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                frm.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptissueNewV", "Issur/Return/Transfer")
                'PurchaseOrderViewer.funreport(dt, "rptissue", "Issur/Return/Transfer")
            ElseIf val = "Return" Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                frm.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptreturnNewV", "Issur/Return/Transfer")

                ' PurchaseOrderViewer.funreport(dt, "rptreturn", "Issur/Return/Transfer")

            ElseIf val = "Transfer" Then
                '---------------------Added By ----Pankaj Kumar----on 04/03/2012------------------------
                Dim QryTrnsfr As String = "select TSPL_IssueItemToAssembledAsset_Head.Created_By,TSPL_IssueItemToAssembledAsset_Head.Modify_By, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as RunDate, TSPL_COMPANY_MASTER.logo_img, TSPL_COMPANY_MASTER.logo_img2, TSPL_COMPANY_MASTER.Comp_Name  as CompanyName, " & _
    " TSPL_COMPANY_MASTER.Tin_No as CompanyTin,Case when len(TSPL_COMPANY_MASTER.Add1)>0 then TSPL_COMPANY_MASTER.Add1 else '' end +case when len(TSPL_COMPANY_MASTER.Add2)>0 then ','  else  '' end  +case when len(TSPL_COMPANY_MASTER.Add2)>0 then TSPL_COMPANY_MASTER.Add2  else  '' end + case when len(TSPL_COMPANY_MASTER.Add3)>0 then ','  else  '' end +case when len(TSPL_COMPANY_MASTER.Add3)>0 then TSPL_COMPANY_MASTER.Add3  else  '' end as CompanyAddress, " & _
    " TSPL_IssueItemToAssembledAsset_Head.Doc_No, TSPL_IssueItemToAssembledAsset_Head.Doc_Date, TSPL_IssueItemToAssembledAsset_Head.Doc_Type, " & _
    " (select Case when (len(TSPL_LOCATION_MASTER .Add1)>0) then convert(varchar(20),TSPL_LOCATION_MASTER.Add1,103) else '' end + " & _
" case when (len(TSPL_LOCATION_MASTER.Add2)>0) then ', '+ convert(varchar(20),TSPL_LOCATION_MASTER.Add2,103)  else  '' end + " & _
" case when (len(TSPL_LOCATION_MASTER.Add3)>0) then ', '+convert(varchar(20),TSPL_LOCATION_MASTER.Add3,103)  else  '' end + " & _
" case when (len(TSPL_LOCATION_MASTER.Add4)>0) then ', '+convert(varchar(20),TSPL_LOCATION_MASTER.Add4,103)  else  '' end + " & _
" case when (len(TSPL_LOCATION_MASTER.City_Code )>0) then ', '+convert(varchar(20),TSPL_LOCATION_MASTER.City_Code,103) else  ''  end + " & _
" case when (len(TSPL_LOCATION_MASTER.State )>0) then ', '+convert(varchar(20),TSPL_LOCATION_MASTER.State,103)  else  ''  end + " & _
" case when (len(TSPL_LOCATION_MASTER.Pin_Code )>0) then ', '+convert(varchar(10),TSPL_LOCATION_MASTER.Pin_Code,103)  else  ''  end + " & _
" case when (len(TSPL_LOCATION_MASTER.Country )>0) then ', '+TSPL_LOCATION_MASTER.Country  else  ''  end  from TSPL_LOCATION_MASTER where location_Code='L001' ) as Address, " & _
    " (select Location_Desc from TSPL_LOCATION_MASTER where location_Code=TSPL_IssueItemToAssembledAsset_Head.To_Location) as ToLocDesc, " & _
    " (select Loc_Segment_Code from TSPL_LOCATION_MASTER where location_Code=TSPL_IssueItemToAssembledAsset_Head.To_Location) as ToLocSegCode, " & _
    " (select TIN_No from TSPL_LOCATION_MASTER where location_Code=TSPL_IssueItemToAssembledAsset_Head.To_Location) as TinNo, " & _
    " (select TCAN_No from TSPL_LOCATION_MASTER where location_Code=TSPL_IssueItemToAssembledAsset_Head.To_Location) as CstNo, " & _
    " (select TIN_No from TSPL_LOCATION_MASTER where location_Code=TSPL_IssueItemToAssembledAsset_Head.From_Location) as CompanyTin, " & _
    " '' as NRG_No, TSPL_IssueItemToAssembledAsset_Detail.Item_Code AS ItemCode, TSPL_IssueItemToAssembledAsset_Detail.Item_Desc AS Desciption, " & _
    " TSPL_IssueItemToAssembledAsset_Detail.Issued_Qty AS Quantity, TSPL_IssueItemToAssembledAsset_Detail.Unit_code AS Uom, TSPL_IssueItemToAssembledAsset_Detail.Unit_Cost AS Rate, " & _
    " TSPL_IssueItemToAssembledAsset_Detail.Amount AS Amount, TSPL_IssueItemToAssembledAsset_Head.TAX1 AS TaxRateDesc1, TSPL_IssueItemToAssembledAsset_Head.TAX1_Amt as TaxRate1, " & _
    " TSPL_IssueItemToAssembledAsset_Head.TAX2 as TaxRateDesc2, TSPL_IssueItemToAssembledAsset_Head.TAX2_Amt as TaxRate2, TSPL_IssueItemToAssembledAsset_Head.TAX3 as TaxRateDesc3, " & _
    " TSPL_IssueItemToAssembledAsset_Head.TAX3_Amt as TaxRate3, TSPL_IssueItemToAssembledAsset_Head.TAX4 as TaxRateDesc4, TSPL_IssueItemToAssembledAsset_Head.TAX4_Amt as TaxRate4, " & _
    " TSPL_IssueItemToAssembledAsset_Head.TAX5 as TaxRateDesc5, TSPL_IssueItemToAssembledAsset_Head.TAX5_Amt as TaxRate5, TSPL_IssueItemToAssembledAsset_Head.TAX6 as TaxRateDesc6, " & _
    " TSPL_IssueItemToAssembledAsset_Head.TAX6_Amt as TaxRate6, TSPL_IssueItemToAssembledAsset_Head.TAX7 as TaxRateDesc7, TSPL_IssueItemToAssembledAsset_Head.TAX7_Amt as TaxRate7, " & _
    " TSPL_IssueItemToAssembledAsset_Head.TAX8 as TaxRateDesc8, TSPL_IssueItemToAssembledAsset_Head.TAX8_Amt as TaxRate8, TSPL_IssueItemToAssembledAsset_Head.TAX9 as TaxRateDesc9, " & _
    " TSPL_IssueItemToAssembledAsset_Head.TAX9_Amt as TaxRate9, TSPL_IssueItemToAssembledAsset_Detail.TAX10 as TaxRateDesc10, TSPL_IssueItemToAssembledAsset_Detail.TAX10_Amt as  TaxRate10 " & _
    " FROM TSPL_IssueItemToAssembledAsset_Head " & _
    " INNER JOIN TSPL_IssueItemToAssembledAsset_Detail ON TSPL_IssueItemToAssembledAsset_Head.Doc_No = TSPL_IssueItemToAssembledAsset_Detail.Doc_No " & _
    " LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp1 ON TSPL_IssueItemToAssembledAsset_Head.Issue_To = emp1.EMP_CODE " & _
    " LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp2 ON TSPL_IssueItemToAssembledAsset_Head.Request_By = emp2.EMP_CODE " & _
    " LEFT OUTER JOIN  TSPL_LOCATION_MASTER as loc1 ON TSPL_IssueItemToAssembledAsset_Head.From_Location = loc1.Location_Code " & _
    " LEFT OUTER JOIN  TSPL_LOCATION_MASTER  as loc2 ON TSPL_IssueItemToAssembledAsset_Head.To_Location = loc2.Location_Code " & _
    " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_IssueItemToAssembledAsset_Head.comp_code = TSPL_COMPANY_MASTER.Comp_Code " & _
    " where TSPL_IssueItemToAssembledAsset_Head.Doc_No='" + txtDocNo.Value + "' and TSPL_IssueItemToAssembledAsset_Head.Doc_Type='" + val + "' "
                '--------------------------------------------------Code Ends Here--------------------------------------------------
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(QryTrnsfr)
                frm.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptscrapTransfer", "Issur/Return/Transfer")

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtIssueTo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtHCode._MYValidating

        txtHCode.Value = ClsHirerachyLevelMaster.getFinder("", txtHCode.Value, isButtonClicked)
        If clsCommon.myLen(txtHCode.Value) > 0 Then
            lblHDesc.Text = ClsHirerachyLevelMaster.GetName(txtHCode.Value, Nothing)
        Else
            txtHCode.Value = ""
            lblHDesc.Text = ""
        End If
    End Sub

    Private Sub txtRequestBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRequestBy._MYValidating
        Dim obj As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployee(txtRequestBy.Value, isButtonClicked)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
            txtRequestBy.Value = obj.EMP_CODE
            lblRequestBy.Text = obj.Emp_Name
        Else
            txtRequestBy.Value = ""
            lblRequestBy.Text = ""
        End If
    End Sub

    Private Sub txtFromLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromLocation._MYValidating
        'Dim obj As clsLocation = clsLocation.FinderForPhysicalLoaction(txtFromLocation.Value, isButtonClicked)
        'If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
        '    txtFromLocation.Value = obj.Code
        '    lblFromLocation.Text = obj.Name
        'Else
        '    txtFromLocation.Value = ""
        '    lblFromLocation.Text = ""
        'End If


        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        txtFromLocation.Value = clsCommon.ShowSelectForm("IRFROMLOC", qry, "Code", WhrCls, txtFromLocation.Value, "Code", isButtonClicked)
        lblFromLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLocation.Value + "'"))

    End Sub

    Private Sub txtToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndAssetCode._MYValidating
        'If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Sale") = CompairStringResult.Equal Then
        '    Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER "
        '    fndAssetCode.Value = clsCommon.ShowSelectForm("IRTCustCoode", qry, "Code", "Inter_Branch='Y'", fndAssetCode.Value, "", isButtonClicked)
        '    lblAssetDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndAssetCode.Value + "' "))
        'Else
        '    Dim obj As clsLocation = clsLocation.FinderForPhysicalLoaction(fndAssetCode.Value, isButtonClicked)
        '    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
        '        fndAssetCode.Value = obj.Code
        '        lblAssetDesc.Text = obj.Name
        '    Else
        '        fndAssetCode.Value = ""
        '        lblAssetDesc.Text = ""
        '    End If
        'End If

        Dim qry As String = " select Asset_Code as Code,Asset_Name as [Asset Name],Book_Estimated_Life as [Estimated Life],Book_Source_value as [Book Source Value]," & _
                            " Book_Source_Original_value as [Book Org Value],Item_Net_Amt as [Item Net Value],Book_Salvage_Value as [Book Salvage Value] " & _
                            " from TSPL_ACQUISITION_DETAIL INNER JOIN TSPL_ACQUISITION_HEAD ON TSPL_ACQUISITION_DETAIL.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code   "
        Dim whrcls As String = "TSPL_ACQUISITION_DETAIL.Is_Assembled='1' AND TSPL_ACQUISITION_HEAD.STATUS=0 and isnull(TSPL_ACQUISITION_DETAIL.asset_merged,0)<>1 "
        fndAssetCode.Value = clsCommon.ShowSelectForm("Asset", qry, "Code", whrcls, fndAssetCode.Value, "", isButtonClicked)
        lblAssetDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Asset_Name from TSPL_ACQUISITION_DETAIL where Asset_Code='" + fndAssetCode.Value + "' "))
    End Sub

    Private Sub cboDocType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocType.SelectedValueChanged
        repoReqQty.IsVisible = True
        repoReturnQty.IsVisible = False
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
            repoReqQty.HeaderText = "Required Quantity"
            repoQty.HeaderText = "Issue Quantity"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
            repoReturnQty.IsVisible = True
            repoReqQty.HeaderText = "Requisition Quantity"
            repoQty.HeaderText = "Issue Quantity"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
            repoReturnQty.IsVisible = False
            repoReqQty.HeaderText = "Return Quantity"
            repoQty.HeaderText = "Transfer Quantity"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer") = CompairStringResult.Equal Then
            repoReqQty.IsVisible = False
            repoQty.HeaderText = "Transfer Quantity"
        End If

    End Sub

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
        txtTaxGroup.Value = clsCommon.ShowSelectForm("IRTAXGP", qry, "Code", "Tax_Group_Type='P'", txtTaxGroup.Value, "Code", isButtonClicked)
        SetTaxDetails()
    End Sub

    Sub SetTaxDetails()
        LoadBlankGridTax()
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
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
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblQty As Double = 0
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
            dblQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
            dblQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRetQty).Value)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
            dblQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        End If

        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblAmt As Double = dblQty * dblRate
        gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        For ii As Integer = 1 To 10
            Dim Strii As String = clsCommon.myCstr(ii)
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
                    ''If IsTaxable Then
                    dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                    ''End If
                    dblBaseAmt = (dblAmt + dblOtherTaxAmt)
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
        Next
        Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
        Dim dblAmtAfterTax As Double = dblAmt + dblTotTaxAmt
        gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)
        '==================Added by preeti gupta=========================================

        'gv1.Rows(IntRowNo).Cells(colIsCategory).Value = Math.Round(dblTotTaxAmt, 2)
        'gv1.Rows(IntRowNo).Cells(colCategoryType).Value = Math.Round(dblAmtAfterTax, 2)
        'gv1.Rows(IntRowNo).Cells(colCapexSubCode).Value = Math.Round(dblTotTaxAmt, 2)
        'gv1.Rows(IntRowNo).Cells(colCapexCode).Value = Math.Round(dblAmtAfterTax, 2)
        '===================================================================================
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
        repoTaxAmt.ReadOnly = True
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

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
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

    Private Sub BlankTaxDetails(ByVal intRowNo As Integer)
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            If intRowNo < 0 Then
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
            Else
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
            End If
        Next
    End Sub

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

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If True Then ''clsCommon.CompairString(gv2.CurrentCell.ColumnInfo.Name, colTTaxRate) = CompairStringResult.Equal Then
                Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FDTaxRate", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='P'", "", "", True))
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

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
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

    Private Sub cboDocType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboDocType.SelectedIndexChanged
        If isInsideLoadData = False Then
            lblReq2.Visible = False
            lblReq3.Visible = False
            chkWithoutRefNo.Checked = False
            chkWithoutRefNo.Enabled = False
            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
                AddNew()
                txtTaxGroup.Enabled = False
                lblTaxGrpName.Enabled = False
                lblReq.Text = "Requisition No"

                lblReq.Visible = True
                fndReqNo.Visible = True
                lblReqDate.Visible = True
                repoReqIssueNo.HeaderText = "Req. No"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                AddNew()
                chkWithoutRefNo.Enabled = True
                txtTaxGroup.Enabled = False
                lblTaxGrpName.Enabled = False
                lblReq.Text = "Issue No"
                lblReq.Visible = True
                fndReqNo.Visible = True
                lblReqDate.Visible = True
                repoReqIssueNo.HeaderText = "Issue No"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
                AddNew()
                chkWithoutRefNo.Enabled = True
                txtTaxGroup.Enabled = False
                lblTaxGrpName.Enabled = False
                lblReq.Text = "Return No"
                lblReq.Visible = True
                fndReqNo.Visible = True
                lblReqDate.Visible = True
                repoReqIssueNo.HeaderText = "Return No"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer") = CompairStringResult.Equal Then
                AddNew()
                txtTaxGroup.Enabled = True
                lblTaxGrpName.Enabled = True
                lblReq.Visible = False
                fndReqNo.Visible = False
                lblReqDate.Visible = False
            End If
        End If
    End Sub

    Private Sub TxtVehicle__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        'Dim Check As String = "select Segment_code, Description From TSPL_GL_SEGMENT_CODE Where Seg_No= '2' And Segment_code='" + TxtVehicle.Value + "'"
        'Dim Check As String = "select Segment_code, Description From TSPL_GL_SEGMENT_CODE Where Segment_code='" + TxtVehicle.Value + "'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(Check)
        'If dt.Rows.Count <= 0 And isButtonClicked = False Then
        '    lblVehicleDesc.Text = ""
        'Else
        '    Dim Qry As String = "select Segment_code as [Code], Description,Segment_name as [Segment Name] From TSPL_GL_SEGMENT_CODE  "
        '    Dim WhrCls As String = " seg_no <>'7'  "
        '    TxtVehicle.Value = clsCommon.ShowSelectForm("Vehicle Selector", Qry, "Code", WhrCls, TxtVehicle.Value, "", isButtonClicked)
        '    lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Description From TSPL_GL_SEGMENT_CODE Where   Segment_Code= '" + TxtVehicle.Value + "'")
        'End If

        Dim qry As String = "select Cost_Code as Code,Cost_name as Name from  TSPL_CostCenter_MASTER"
        'TxtVehicle.Value = clsCommon.ShowSelectForm("TSPL_CostCenter_MASTER", qry, "Code", "", TxtVehicle.Value, "", isButtonClicked)
        'lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Cost_name From TSPL_CostCenter_MASTER Where   Cost_Code= '" + TxtVehicle.Value + "'")


    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If clsCommon.myLen(fndReqNo.Value) <= 0 Then
            If gv1.RowCount > 0 Then

                Dim intCurrRow As Integer = gv1.CurrentRow.Index
                gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                If intCurrRow = gv1.Rows.Count - 1 Then
                    gv1.Rows.AddNew()
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                End If
            End If
        End If

    End Sub

    Private Sub LoadReqDataHead(ByVal strReqNo As String)
        Dim qry As String
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
            qry = "select Requisition_Date,Location,Location_Desc from TSPL_REQUISITION_HEAD inner join " & _
               "TSPL_LOCATION_MASTER on TSPL_REQUISITION_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code where Requisition_Id='" & fndReqNo.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                lblReqDate.Text = clsCommon.myCstr(dt.Rows(0)("Requisition_Date"))
                txtFromLocation.Value = clsCommon.myCstr(dt.Rows(0)("Location"))
                lblFromLocation.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
            qry = "SELECT TSPL_IssueItemToAssembledAsset_Head.Doc_Date,TSPL_IssueItemToAssembledAsset_Head.From_Location,FLocation.Location_Desc as FromLocationName, " & _
            "TSPL_IssueItemToAssembledAsset_Head.ASSET_CODE,ACQ.ASSET_NAME,TSPL_IssueItemToAssembledAsset_Head.Issue_To, " & _
            "IssueEmp.Emp_Name as IssueToName ,TSPL_IssueItemToAssembledAsset_Head.Request_By,RequestEmp.Emp_Name as RequestByName,TSPL_IssueItemToAssembledAsset_Head.vehicle_Id,Req_IssueNo " & _
            "FROM TSPL_IssueItemToAssembledAsset_Head left outer join TSPL_LOCATION_MASTER as FLocation on  " & _
            "FLocation.Location_Code=TSPL_IssueItemToAssembledAsset_Head.From_Location " & _
            "  left outer join TSPL_ACQUISITION_DETAIL as ACQ on  ACQ.Asset_Code=TSPL_IssueItemToAssembledAsset_Head.ASSET_CODE and isnull(ACQ.asset_merged,0)<>1 " & _
            " left outer join TSPL_EMPLOYEE_MASTER as IssueEmp on " & _
            "IssueEmp.EMP_CODE= TSPL_IssueItemToAssembledAsset_Head.issue_To left outer join  " & _
            "TSPL_EMPLOYEE_MASTER as RequestEmp on RequestEmp.EMP_CODE= TSPL_IssueItemToAssembledAsset_Head.Request_By where TSPL_IssueItemToAssembledAsset_Head.doc_no='" & fndReqNo.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtDate.Value = clsCommon.myCstr(dt.Rows(0)("Doc_Date"))
                txtFromLocation.Value = clsCommon.myCstr(dt.Rows(0)("From_Location"))
                lblFromLocation.Text = clsCommon.myCstr(dt.Rows(0)("FromLocationName"))
                fndAssetCode.Value = clsCommon.myCstr(dt.Rows(0)("ASSET_CODE"))
                lblAssetDesc.Text = clsCommon.myCstr(dt.Rows(0)("ASSET_NAME"))

                txtHCode.Value = clsCommon.myCstr(dt.Rows(0)("Issue_To"))
                lblHDesc.Text = clsCommon.myCstr(dt.Rows(0)("IssueToName"))
                txtRequestBy.Value = clsCommon.myCstr(dt.Rows(0)("Request_By"))
                lblRequestBy.Text = clsCommon.myCstr(dt.Rows(0)("RequestByName"))
                'TxtVehicle.Value = clsCommon.myCstr(dt.Rows(0)("vehicle_Id"))
                lblReq3.Text = clsCommon.myCstr(dt.Rows(0)("Req_IssueNo"))
            End If

        End If

    End Sub

    Private Sub LoadReqDataDetail(ByVal strReqNo As String)
        gv1.Rows.Clear()
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
            Dim Qry As String = "select Item_Code,Item_Desc,Unit_Code,Requisition_Qty - (select isnull(SUM(Issued_Qty),0) from TSPL_IssueItemToAssembledAsset_Head inner join " & _
            "TSPL_IssueItemToAssembledAsset_Detail on TSPL_IssueItemToAssembledAsset_Head.Doc_No=TSPL_IssueItemToAssembledAsset_Detail.Doc_No and " & _
            "TSPL_IssueItemToAssembledAsset_Head.RequisitionNo=TSPL_REQUISITION_DETAIL.Requisition_Id where Doc_Type='Issue') + " & _
            "(select isnull(SUM(Issued_Qty),0) from TSPL_IssueItemToAssembledAsset_Head inner join TSPL_IssueItemToAssembledAsset_Detail on " & _
            "TSPL_IssueItemToAssembledAsset_Head.Doc_No=TSPL_IssueItemToAssembledAsset_Detail.Doc_No and " & _
            "TSPL_IssueItemToAssembledAsset_Head.RequisitionNo=TSPL_REQUISITION_DETAIL.Requisition_Id where Doc_Type='Return') as Requisition_Qty  " & _
            ",case when (TSPL_PURCHASE_ORDER_HEAD.Capex_Code is not null and TSPL_PURCHASE_ORDER_HEAD.Capex_SubCode is not null) then 0 else 1 end as CheckCapexLimit" & _
            " from TSPL_REQUISITION_DETAIL " & _
            "left outer join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id=TSPL_REQUISITION_DETAIL.Requisition_Id " & _
            "left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.Against_Requisition=TSPL_REQUISITION_HEAD.Requisition_Id " & _
            " where TSPL_REQUISITION_DETAIL.Requisition_Id='" & fndReqNo.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReq_IssueNo).Value = clsCommon.myCstr(strReqNo)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = clsCommon.myCstr(dr("Requisition_Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCheckCapexLimit).Value = clsCommon.myCdbl(dr("CheckCapexLimit"))
                    Dim strDate As String = txtDate.Value
                    Dim dblUnitCost As Double = 0
                    Dim dblCostMethod As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Costing_Method as Costing_Method from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Item_Code='" & clsCommon.myCstr(dr("Item_Code")) & "' "))
                    If dblCostMethod <> 0 Then
                        dblUnitCost = clsInventoryMovement.GetCost(dblCostMethod, clsCommon.myCstr(dr("Item_Code")), txtFromLocation.Value, 1, strDate, strDate, False, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = dblUnitCost
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = 0
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(clsCommon.myCstr(dr("Item_Code")))
                Next
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
            isCellValueChangedOpen = True
            Dim Qry As String = "select Asset_Code,Item_Code,Item_Desc,Unit_code,Required_Qty,Issued_Qty,Unit_Cost,Amount,Item_Net_Amt,IsCapex,CheckCapexLimit,CapexType,Capex_Code,Capex_SubCode,CapexQty from TSPL_IssueItemToAssembledAsset_Detail where Doc_No='" & fndReqNo.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReq_IssueNo).Value = clsCommon.myCstr(strReqNo)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetCode).Value = clsCommon.myCstr(dr("Asset_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetCode).ReadOnly = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).ReadOnly = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).ReadOnly = True

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = clsCommon.myCdbl(dr("Required_Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("Issued_Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dr("Unit_Cost"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = clsCommon.myCdbl(dr("Unit_Cost")) * gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterTax).Value = clsCommon.myCdbl(dr("Unit_Cost")) * gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(clsCommon.myCstr(dr("Item_Code")))

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsCategory).Value = clsCommon.myCdbl(dr("IsCapex")) ' objTr.IsCapex
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCheckCapexLimit).Value = clsCommon.myCdbl(dr("CheckCapexLimit")) ' objTr.CheckCapexLimit
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = clsCommon.myCstr(dr("CapexType")) 'objTr.CapexType
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = clsCommon.myCstr(dr("Capex_Code")) ' objTr.Capex_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = clsCommon.myCstr(dr("Capex_SubCode")) 'objTr.Capex_SubCode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexQty).Value = clsCommon.myCdbl(dr("CapexQty")) 'objTr.CapexQty
                Next
            End If
            isCellValueChangedOpen = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
            isCellValueChangedOpen = True
            Dim Qry As String = "select Asset_Code,Item_Code,Item_Desc,Unit_code,Issued_Qty as Required_Qty,Issued_Qty_AgainstRet as Issued_Qty,Unit_Cost,Amount,Item_Net_Amt,IsCapex,CheckCapexLimit,CapexType,Capex_Code,Capex_SubCode,CapexQty from TSPL_IssueItemToAssembledAsset_Detail where Doc_No='" & fndReqNo.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReq_IssueNo).Value = clsCommon.myCstr(strReqNo)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetCode).Value = clsCommon.myCstr(dr("Asset_Code"))

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetCode).ReadOnly = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).ReadOnly = False
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).ReadOnly = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).ReadOnly = True

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))

                    Dim dblBalQty As Decimal = clsItemIssueToAssembledAssetDetail.GetBalanceQtyReturnForTranfserCapex(txtDocNo.Value, fndReqNo.Value, clsCommon.myCstr(dr("Item_Code")), Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = dblBalQty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = dblBalQty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dr("Unit_Cost"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = clsCommon.myCdbl(dr("Unit_Cost")) * gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterTax).Value = clsCommon.myCdbl(dr("Unit_Cost")) * gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(clsCommon.myCstr(dr("Item_Code")))

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsCategory).Value = clsCommon.myCdbl(dr("IsCapex"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCheckCapexLimit).Value = clsCommon.myCdbl(dr("CheckCapexLimit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = clsCommon.myCstr(dr("CapexType"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = clsCommon.myCstr(dr("Capex_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = clsCommon.myCstr(dr("Capex_SubCode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexQty).Value = clsCommon.myCdbl(dr("CapexQty"))
                    UpdateCurrentRow(gv1.Rows.Count - 1)
                Next
                UpdateAllTotals()
            End If
            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub gv1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.Click
        If gv1.CurrentRow IsNot Nothing Then
            setBalance()
        End If
    End Sub

    Sub setBalance()
        UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
        UcItemBalance1.ItemMRP = -1
        UcItemBalance1.LocationCode = txtFromLocation.Value
        UcItemBalance1.LocationName = lblFromLocation.Text
        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtDocNo.Value
        UcItemBalance1.TransDate = txtDate.Value
        'UcItemBalance1.ShowPOQty = True
        UcItemBalance1.RefreshData()
    End Sub

    Private Sub gv1_CurrentRowChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 Then
            setBalance()
        End If

    End Sub

    Private Sub chkWithoutRefNo_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkWithoutRefNo.ToggleStateChanged
        chkWithoutRefNoChanged()
    End Sub

    Private Sub chkWithoutRefNoChanged()
        gv1.Columns(colReq_IssueNo).IsVisible = Not chkWithoutRefNo.Checked
        gv1.Columns(colReqQty).IsVisible = Not chkWithoutRefNo.Checked
        gv1.Columns(colQty).IsVisible = Not chkWithoutRefNo.Checked
        fndReqNo.Enabled = Not chkWithoutRefNo.Checked
        txtHCode.Enabled = Not chkWithoutRefNo.Checked
        If chkWithoutRefNo.Checked Then
            fndReqNo.Value = ""
            txtHCode.Value = ""
        End If
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
                Dim Reason As String = ""
                'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If

                If clsItemIssueToAssembledAsset.ReverseAndUnpost(txtDocNo.Value) Then
                    saveCancelLog(Reason, "Reverse and Recreate", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F4 Then
            'OpenSerialItem()
            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
                OpenSerialItem()
            Else
                OpenSerialItemReturn()
            End If
        End If
    End Sub

    Private Sub fndReqNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndReqNo._MYValidating
        If clsCommon.myLen(txtFromLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Location First", Me.Text)
            Exit Sub
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
            Dim qry As String = "select Requisition_Id as Code,Requisition_Date as Date from TSPL_REQUISITION_HEAD "
            Dim whrclas As String = "is_internal='Y' and Requisition_Type ='A' and Location = '" + txtFromLocation.Value + "' "
            fndReqNo.Value = clsCommon.ShowSelectForm("Req no", qry, "Code", whrclas, fndReqNo.Value, "", isButtonClicked)
            lblReq3.Text = fndReqNo.Value
            If clsCommon.myLen(fndReqNo.Value) > 0 Then
                LoadReqDataHead(fndReqNo.Value)
                LoadReqDataDetail(fndReqNo.Value)
                lblReq2.Visible = False
                lblReq3.Visible = False
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
            Dim qry As String = "select Doc_No as Code,Doc_date as Date from TSPL_IssueItemToAssembledAsset_Head "
            Dim whrclas As String = "doc_type='Issue' and Posting_Date <> ''"
            fndReqNo.Value = clsCommon.ShowSelectForm("Req no", qry, "Code", whrclas, fndReqNo.Value, "", isButtonClicked)
            If clsCommon.myLen(fndReqNo.Value) > 0 Then
                LoadReqDataHead(fndReqNo.Value)
                LoadReqDataDetail(fndReqNo.Value)
                lblReq2.Visible = True
                lblReq3.Visible = True
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
            Dim qry As String = "select Doc_No as Code,Doc_date as Date from TSPL_IssueItemToAssembledAsset_Head "
            Dim whrclas As String = "doc_type='Return' and Posting_Date <> ''"
            fndReqNo.Value = clsCommon.ShowSelectForm("Req no", qry, "Code", whrclas, fndReqNo.Value, "", isButtonClicked)
            If clsCommon.myLen(fndReqNo.Value) > 0 Then
                LoadReqDataHead(fndReqNo.Value)
                LoadReqDataDetail(fndReqNo.Value)
                lblReq2.Visible = True
                lblReq3.Visible = True
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No Requisition No. Found", Me.Text)
        End If
    End Sub

    Sub LoadAssetType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Assembled"
        dr("Name") = "Assembled"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Other"
        dr("Name") = "Other"
        dt.Rows.Add(dr)


        cboAssetType.DataSource = dt
        cboAssetType.ValueMember = "Code"
        cboAssetType.DisplayMember = "Name"
    End Sub
    '===============================Added by preeti gupta=========================
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
            gvrow.Cells(colIsCategory).ReadOnly = Read
        Next

    End Sub

    Private Function LoadCategory() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Capex"
        dr("Name") = "Capex"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Regular"
        dr("Name") = "Regular"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Function ChkLimitBudget(ByVal rowindex As Integer) As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(Double))

        Dim dr As DataRow = dt.NewRow()


        Dim budgetamtwithtolerence As Double
        Dim rebudgetamt As Double
        Dim rebudgetamtwithtolerence As Double
        dr = dt.NewRow()

        budgetamtwithtolerence = clsCapexBudget.GetBudgetWithTolerence(clsCommon.myCstr(gv1.Rows(rowindex).Cells(colCapexSubCode).Value), Nothing)
        rebudgetamt = clsCapexBudget.GetReBudget(clsCommon.myCstr(gv1.Rows(rowindex).Cells(colCapexSubCode).Value), txtDocNo.Value, Nothing)
        rebudgetamtwithtolerence = clsCapexBudget.GetReBudgetWithTolerence(clsCommon.myCstr(gv1.Rows(rowindex).Cells(colCapexSubCode).Value), txtDocNo.Value, Nothing)
        dr("Code") = budgetamtwithtolerence
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = rebudgetamt
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = rebudgetamtwithtolerence
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Function GetDocSubCapexValue(ByVal Capex_Code As String) As Decimal
        Dim capexValue As Decimal = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.CompairString(grow.Cells(colCapexSubCode).Value, Capex_Code) = CompairStringResult.Equal Then
                capexValue = capexValue + clsCommon.myCdbl(grow.Cells(colCapexQty).Value) * clsCommon.myCdbl(grow.Cells(colRate).Value)
            End If
        Next
        Return capexValue
    End Function
    '=============================================================================


    Private Sub txtCostCenter__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCostCenter._MYValidating

        txtCostCenter.Value = ClsCostCentreFinancial.getFinder("Hirerachy_Level_Code='" & txtHCode.Value & "'", txtCostCenter.Value, isButtonClicked)
        If clsCommon.myLen(txtHCode.Value) > 0 Then
            lblCostCenterDesc.Text = ClsCostCentreFinancial.GetName(txtCostCenter.Value, Nothing)
        Else
            txtHCode.Value = ""
            lblHDesc.Text = ""
        End If
    End Sub
End Class
