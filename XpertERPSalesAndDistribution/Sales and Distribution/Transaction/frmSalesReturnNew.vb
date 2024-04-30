Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports XpertERPEngine
Imports Telerik.WinControls.UI

'----------------- Done BY Abhishek KUmar 1 Nov 2012 1:11 Pm For Print Report -----------------

'---saving of Ref No,Description,comment on 22/11/2012
'-----------saving shippment_type in sale return on 30th jan 2013-------done by usha ----------
'--BM00000000441
Public Class frmSalesReturnNew
    Inherits FrmMainTranScreen
#Region "Variables"
    Public strPOInvoice As String = Nothing
    Public Shippment_type As String = Nothing

    Private isCellValueChangedOpen As Boolean = False
    Private objRemittance As clsRemittance
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False



    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colPriceDate As String = "COLPRICEDATE"
    Const colUnit As String = "COLUNIT"
    Const colLocation As String = "COLLOCATION"
    Const colPriceCode As String = "COLPRICECODE"
    Const colSchemeApplicable As String = "SchemeApplicable"
    Const colSchemeCodeQty As String = "COLSCHEMEQTY"
    Const colPendingQty As String = "COLPENDINGQTY"
    Const colActualReturnQty As String = "COLACTUALRETURNQTY"
    Const colLeakQty As String = "COLLEAKQTY"
    Const colBurstQty As String = "COLBURTSQTY"
    Const colShortQty As String = "COLSHORTQTY"
    Const colReturnQty As String = "COLRETURNQTY"
    Const colSchemeItem As String = "COLSCHEMEITEM"
    Const colPromoSchemeApplicable As String = "COLPROMOSCHEMEAPPLICABLE"
    Const colPromoSchemeCode As String = "COLPROMOSCHEMECODE"
    Const colPromoSchemeItem As String = "COLPROMOSCHEMEITEM"
    Const colSchemeDiscApplicable As String = "COLSCHEMEDISCAPPLICABLE"
    Const colSchemeCodeCash As String = "COLSCHEMECODECASH"
    Const colSamplingItem As String = "COLSAMPLINGITEM"
    Const colMRPAmt As String = "COLMRPAMT"
    Const colBasicRate As String = "COLBASICRATE"
    Const colItemAssessableRate As String = "COLITEMASSESSABLERATE"
    Const colDiscAmt As String = "COLDISCAMT"
    Const colItemNetAmt As String = "COLITEMNETAMT"
    Const colTax1 As String = "COLTAX1"
    Const colTaxAssessableAmt1 As String = "COLASSESSABLEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"

    Const colTax2 As String = "COLTAX2"
    Const colTaxAssessableAmt2 As String = "COLASSESSABLEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colTax3 As String = "COLTAX3"
    Const colTaxAssessableAmt3 As String = "COLASSESSABLEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colTax4 As String = "COLTAX4"
    Const colTaxAssessableAmt4 As String = "COLASSESSABLEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colTax5 As String = "COLTAX5"
    Const colTaxAssessableAmt5 As String = "COLASSESSABLEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colTax6 As String = "COLTAX6"
    Const colTaxAssessableAmt6 As String = "COLASSESSABLEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colTax7 As String = "COLTAX7"
    Const colTaxAssessableAmt7 As String = "COLASSESSABLEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colTax8 As String = "COLTAX8"
    Const colTaxAssessableAmt8 As String = "COLASSESSABLEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colTax9 As String = "COLTAX9"
    Const colTaxAssessableAmt9 As String = "COLASSESSABLEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colTax10 As String = "COLTAX10"
    Const colTaxAssessableAmt10 As String = "COLASSESSABLEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colItemTax As String = "COLITEMTAX"
    Const colTotalAssessableAmt As String = "COLTOTALASSESSABLEAMT"
    Const colTotalMRPAmt As String = "COLTOTALMRPAMT"
    Const colTotalBasicAmt As String = "COLTOTALBASICAMT"
    Const colTotalDiscAmt As String = "COLTOTALDISCAMT"
    Const colTotalnetAmt As String = "COLTOTALNETAMT"
    Const colTotalTaxAmt As String = "COLTOTALTAXAMT"
    Const colTotalItemAmt As String = "COLTOTALITEMAMT"
    Const colEmptyValue As String = "COLEMPTYVALUE"
    Const colTPT As String = "COLTPT"
    Const colTotalTPT As String = "COLTOTALTPT"
    Const colEmptyValueShell As String = "COLEMPTYVALUESHELL"
    Const colEmptyValueBottle As String = "COLEMPTYVALUEBOTTLE"
    Const colCustDiscount As String = "COLCUSTDISCOUNT"
    Const colTotalCustDiscount As String = "COLTOTALCUSTDISCOUNT"
    Const colUnitCogs As String = "COLUNITCOGS"
    Const colTotTaxAmt1 As String = "COLTOTTAXAMT1"
    Const colTotTaxAmt2 As String = "COLTOTTAXAMT2"
    Const colTotTaxAmt3 As String = "COLTOTTAXAMT3"
    Const colTotTaxAmt4 As String = "COLTOTTAXAMT4"
    Const colTotTaxAmt5 As String = "COLTOTTAXAMT5"
    Const colTotTaxAmt6 As String = "COLTOTTAXAMT6"
    Const colTotTaxAmt7 As String = "COLTOTTAXAMT7"
    Const colTotTaxAmt8 As String = "COLTOTTAXAMT8"
    Const colTotTaxAmt9 As String = "COLTOTTAXAMT9"
    Const colTotTaxAmt10 As String = "COLTOTTAXAMT10"
    'Tax Grid Details
    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTAssessAmt As String = "TAXASSESSAMT"
    Const colTTaxAmt As String = "TAXAMT"
    'price amount-------usha---18/10/12
    Const colpriceamount1 As String = "PRICEAMOUNT1"
    Const colpriceamount2 As String = "PRICEAMOUNT2"
    Const colpriceamount3 As String = "PRICEAMOUNT3"
    Const colpriceamount4 As String = "PRICEAMOUNT4"
    Const colpriceamount5 As String = "PRICEAMOUNT5"
    Const colpriceamount6 As String = "PRICEAMOUNT6"
    Const colpriceamount7 As String = "PRICEAMOUNT7"
    Const colpriceamount8 As String = "PRICEAMOUNT8"
    Const colpriceamount9 As String = "PRICEAMOUNT9"
    Const colpriceamount10 As String = "PRICEAMOUNT10"
    Const colmainItem As String = "MainItem"
    Const coldiscountcode As String = "DiscountCode"
    Const colCustDiscountNoTax As String = "CustomerDiscountNoTax"

    Const ColFromSchemeCode As String = "fromSchemeCode"
    Const ColTargetDisAmt As String = "TargetDiscountAmount"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim repoBalQty As GridViewDecimalColumn
    Dim repoComplete As GridViewTextBoxColumn
    Public clicked As Boolean = False
#End Region

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.saleReturn)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Public Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtCustomerPONO.MaxLength = 30
        txtRefNo.MaxLength = 100
        txtDescription.MaxLength = 100
        txtRemarks.MaxLength = 200
        txtSchemeSampleCode.MaxLength = 12
        txtModeOfTransport.MaxLength = 12

    End Sub
    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        SetUserMgmtNew()
        If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
            If funSetUserAccess() = False Then Exit Sub
        End If
        txtCustomerNo.MendatroryField = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")


        RadPageView1.SelectedPage = RadPageViewPage1
        txtDocNo.MyReadOnly = True
        LoadBlankGrid()
        LoadBlankGridTax()
        AddNew()
        If clsCommon.myLen(strPOInvoice) > 0 Then
            LoadData(strPOInvoice, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub BlankAllControls()
        txtshellqty.Value = 0
        txtDocNo.Value = ""
        txtCustomerNo.Value = ""
        lblCustomerName.Text = ""
        txtCustomerPONO.Text = ""
        txtTaxGroup.Value = ""
        txtLocation.Value = ""
        'lblLocation.Text=obj.LocationName 
        txtInvoiceNo.Value = ""
        txtRouteNo.Value = ""
        lblRouteNo.Text = ""
        txtSalesman.Value = ""
        'lblSalesman.Text=obj.SalesManName 
        txtModeOfTransport.Text = ""
        txtVehicleNo.Value = ""
        lblVehicleNo.Text = ""
        txtSchemeSampleCode.Text = ""
        txtKMReading.Text = ""
        txtInvoiceDate.Text = ""
        lblAssessableAmt.Text = ""
        lblDetailDiscountAmt.Text = ""
        lblTaxAmt.Text = ""
        lblFreightAmount.Text = ""
        lblOtherCharges.Text = ""
        lblAdditionalCharges.Text = ""
        lblDetaolTotAmt.Text = ""
        lblDiscountAmt.Text = ""
        lblTotAmt.Text = ""
        lblTPT.Text = ""
        lblEmptyValue.Text = ""
        txtTermCode.Value = ""
        txtPriceCode.Text = ""
        lblLevel1_User_code.Text = ""
        lblLevel2_User_code.Text = ""
        lblLevel3_User_code.Text = ""
        lblLevel4_User_code.Text = ""
        lblLevel5_User_code.Text = ""
        lblCustAccount.Text = ""
        lblNetAmt.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtDueDate.Value = txtDate.Value
        UsLock1.Status = ERPTransactionStatus.Pending
        txtLocation.Enabled = True
        txtRefNo.Text = ""
        txtDescription.Text = ""
        txtRemarks.Text = ""


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
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
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

        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending Qty"
        repoPendingQty.Name = colPendingQty
        repoPendingQty.Width = 100

        repoPendingQty.Minimum = 0
        repoPendingQty.ReadOnly = True
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPendingQty)

        Dim repoReturnQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoReturnQty.FormatString = ""
        repoReturnQty.HeaderText = "Return Qty"
        repoReturnQty.Name = colActualReturnQty
        repoReturnQty.Width = 100
        repoReturnQty.Minimum = 0
        repoReturnQty.ReadOnly = False
        repoReturnQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoReturnQty)

        Dim repoLeakQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLeakQty.FormatString = ""
        repoLeakQty.HeaderText = "Leak Qty"
        repoLeakQty.Name = colLeakQty
        repoLeakQty.Width = 100
        repoLeakQty.Minimum = 0
        repoLeakQty.ReadOnly = False
        repoLeakQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLeakQty)


        Dim repoBurstQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBurstQty.FormatString = ""
        repoBurstQty.HeaderText = "Burst Qty"
        repoBurstQty.Name = colBurstQty
        repoBurstQty.Width = 100
        repoBurstQty.Minimum = 0
        repoBurstQty.ReadOnly = False
        repoBurstQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBurstQty)

        Dim repoShortQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShortQty.FormatString = ""
        repoShortQty.HeaderText = "Short Qty"
        repoShortQty.Name = colShortQty
        repoShortQty.Width = 100
        repoShortQty.Minimum = 0
        repoShortQty.ReadOnly = False
        repoShortQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoShortQty)

        Dim repoTotalQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalQty.FormatString = ""
        repoTotalQty.HeaderText = "Total Qty"
        repoTotalQty.Name = colReturnQty
        repoTotalQty.Width = 100
        repoTotalQty.Minimum = 0
        repoTotalQty.ReadOnly = True
        repoTotalQty.IsVisible = False
        repoTotalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotalQty)


        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.FormatString = ""
        repoPriceCode.HeaderText = "Price Code"
        repoPriceCode.Name = colPriceCode
        repoPriceCode.Width = 150
        repoPriceCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPriceCode)

        Dim repoPriceDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceDate.FormatString = ""
        repoPriceDate.HeaderText = "Price Date"
        repoPriceDate.Name = colPriceDate
        repoPriceDate.Width = 150
        repoPriceDate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPriceDate)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colUnit
        repoUnit.Width = 150
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocation.FormatString = ""
        repoLocation.HeaderText = "Location"
        repoLocation.Name = colLocation
        repoLocation.Width = 150
        repoLocation.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLocation)

        Dim repoSchemeApplicable As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeApplicable.FormatString = ""
        repoSchemeApplicable.HeaderText = "Scheme Applicable"
        repoSchemeApplicable.Name = colSchemeApplicable
        repoSchemeApplicable.Width = 150
        repoSchemeApplicable.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSchemeApplicable)

        Dim repoSchemeCodeQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSchemeCodeQty.FormatString = ""
        repoSchemeCodeQty.HeaderText = "Scheme Code Qty"
        repoSchemeCodeQty.Name = colSchemeCodeQty
        repoSchemeCodeQty.Width = 150
        repoSchemeCodeQty.ReadOnly = True
        repoSchemeCodeQty.Minimum = 0
        repoSchemeCodeQty.ReadOnly = True
        repoSchemeCodeQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSchemeCodeQty)

        Dim repoSchemeItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeItem.FormatString = ""
        repoSchemeItem.HeaderText = "Scheme Item"
        repoSchemeItem.Name = colSchemeItem
        repoSchemeItem.Width = 150
        repoSchemeItem.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSchemeItem)

        Dim repoPromoSchemeApplicable As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPromoSchemeApplicable.FormatString = ""
        repoPromoSchemeApplicable.HeaderText = "Promo Scheme Applicable"
        repoPromoSchemeApplicable.Name = colPromoSchemeApplicable
        repoPromoSchemeApplicable.Width = 150
        repoPromoSchemeApplicable.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPromoSchemeApplicable)

        Dim repoPromoSchemeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPromoSchemeCode.FormatString = ""
        repoPromoSchemeCode.HeaderText = "Promo Scheme Code"
        repoPromoSchemeCode.Name = colPromoSchemeCode
        repoPromoSchemeCode.Width = 150
        repoPromoSchemeCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPromoSchemeCode)

        Dim repoPromoSchemeItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPromoSchemeItem.FormatString = ""
        repoPromoSchemeItem.HeaderText = "Promo Schem Item"
        repoPromoSchemeItem.Name = colPromoSchemeItem
        repoPromoSchemeItem.Width = 150
        repoPromoSchemeItem.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPromoSchemeItem)


        Dim repoSchemeDiscApplicable As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeDiscApplicable.FormatString = ""
        repoSchemeDiscApplicable.HeaderText = "Schem Disc Applicable"
        repoSchemeDiscApplicable.Name = colSchemeDiscApplicable
        repoSchemeDiscApplicable.Width = 150
        repoSchemeDiscApplicable.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSchemeDiscApplicable)

        Dim repoSchemeCodeCash As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeCodeCash.FormatString = ""
        repoSchemeCodeCash.HeaderText = "Scheme Code Cash"
        repoSchemeCodeCash.Name = colSchemeCodeCash
        repoSchemeCodeCash.Width = 150
        repoSchemeCodeCash.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSchemeCodeCash)

        Dim repoSamplingItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSamplingItem.FormatString = ""
        repoSamplingItem.HeaderText = "Sampling Item"
        repoSamplingItem.Name = colSamplingItem
        repoSamplingItem.Width = 150
        repoSamplingItem.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSamplingItem)

        Dim repoMRPAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRPAmt.FormatString = ""
        repoMRPAmt.HeaderText = "MRP Item"
        repoMRPAmt.Name = colMRPAmt
        repoMRPAmt.Width = 150
        repoMRPAmt.ReadOnly = True
        repoMRPAmt.Minimum = 0
        repoMRPAmt.ReadOnly = True
        repoMRPAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRPAmt)

        Dim repoBasicRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBasicRate.FormatString = ""
        repoBasicRate.HeaderText = "Basic Rate"
        repoBasicRate.Name = colBasicRate
        repoBasicRate.Width = 150
        repoBasicRate.ReadOnly = True
        repoBasicRate.Minimum = 0
        repoBasicRate.ReadOnly = True
        repoBasicRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBasicRate)

        Dim repoItemAssessableRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemAssessableRate.FormatString = ""
        repoItemAssessableRate.HeaderText = "Assessable Rate"
        repoItemAssessableRate.Name = colItemAssessableRate
        repoItemAssessableRate.Width = 150
        repoItemAssessableRate.ReadOnly = True
        repoItemAssessableRate.Minimum = 0
        repoItemAssessableRate.ReadOnly = True
        repoItemAssessableRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemAssessableRate)

        Dim repoDiscAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiscAmt.FormatString = ""
        repoDiscAmt.HeaderText = "Discount Amount"
        repoDiscAmt.Name = colDiscAmt
        repoDiscAmt.Width = 150
        repoDiscAmt.ReadOnly = True
        repoDiscAmt.Minimum = 0
        repoDiscAmt.ReadOnly = True
        repoDiscAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDiscAmt)

        Dim repoItemNetAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemNetAmt.FormatString = ""
        repoItemNetAmt.HeaderText = "Item Net Amount"
        repoItemNetAmt.Name = colItemNetAmt
        repoItemNetAmt.Width = 150
        repoItemNetAmt.ReadOnly = True
        repoItemNetAmt.Minimum = 0
        repoItemNetAmt.ReadOnly = True
        repoItemNetAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemNetAmt)

        Dim repoTax1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax1.FormatString = ""
        repoTax1.HeaderText = "Tax 1"
        repoTax1.Name = colTax1
        repoTax1.IsVisible = False
        repoTax1.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax1)

        Dim repoAssessableAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessableAmt1.FormatString = ""
        repoAssessableAmt1.WrapText = True
        repoAssessableAmt1.HeaderText = "Assessable 1"
        repoAssessableAmt1.Name = colTaxAssessableAmt1
        repoAssessableAmt1.IsVisible = False
        repoAssessableAmt1.Minimum = 0
        repoAssessableAmt1.ReadOnly = True
        repoAssessableAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessableAmt1)

        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.WrapText = True
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colTaxRate1
        repoTaxRate1.IsVisible = False
        repoTaxRate1.Minimum = 0
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate1)

        Dim repoTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.WrapText = True
        repoTaxAmt1.HeaderText = "Tax Amount 1"
        repoTaxAmt1.Name = colTaxAmt1
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.Minimum = 0
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt1)

        Dim repoTotTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt1.FormatString = ""
        repoTotTaxAmt1.WrapText = True
        repoTotTaxAmt1.HeaderText = "Total Tax Amount 1"
        repoTotTaxAmt1.Name = colTotTaxAmt1
        repoTotTaxAmt1.IsVisible = False
        repoTotTaxAmt1.Minimum = 0
        repoTotTaxAmt1.ReadOnly = True
        repoTotTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt1)

        Dim repoTax2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2.FormatString = ""
        repoTax2.HeaderText = "Tax 2"
        repoTax2.Name = colTax2
        repoTax2.IsVisible = False
        repoTax2.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax2)

        Dim repoAssessableAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessableAmt2.FormatString = ""
        repoAssessableAmt2.WrapText = True
        repoAssessableAmt2.HeaderText = "Assessable 2"
        repoAssessableAmt2.Name = colTaxAssessableAmt2
        repoAssessableAmt2.IsVisible = False
        repoAssessableAmt2.Minimum = 0
        repoAssessableAmt2.ReadOnly = True
        repoAssessableAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessableAmt2)

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.WrapText = True
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colTaxRate2
        repoTaxRate2.IsVisible = False
        repoTaxRate2.Minimum = 0
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate2)

        Dim repoTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2.FormatString = ""
        repoTaxAmt2.WrapText = True
        repoTaxAmt2.HeaderText = "Tax Amount 2"
        repoTaxAmt2.Name = colTaxAmt2
        repoTaxAmt2.IsVisible = False
        repoTaxAmt2.Minimum = 0
        repoTaxAmt2.ReadOnly = True
        repoTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt2)

        Dim repoTotTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt2.FormatString = ""
        repoTotTaxAmt2.WrapText = True
        repoTotTaxAmt2.HeaderText = "Total Tax Amount 2"
        repoTotTaxAmt2.Name = colTotTaxAmt2
        repoTotTaxAmt2.IsVisible = False
        repoTotTaxAmt2.Minimum = 0
        repoTotTaxAmt2.ReadOnly = True
        repoTotTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt2)

        Dim repoTax3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax3.FormatString = ""
        repoTax3.HeaderText = "Tax 3"
        repoTax3.Name = colTax3
        repoTax3.IsVisible = False
        repoTax3.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax3)

        Dim repoAssessableAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessableAmt3.FormatString = ""
        repoAssessableAmt3.WrapText = True
        repoAssessableAmt3.HeaderText = "Assessable 3"
        repoAssessableAmt3.Name = colTaxAssessableAmt3
        repoAssessableAmt3.IsVisible = False
        repoAssessableAmt3.Minimum = 0
        repoAssessableAmt3.ReadOnly = True
        repoAssessableAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessableAmt3)

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.WrapText = True
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colTaxRate3
        repoTaxRate3.IsVisible = False
        repoTaxRate3.Minimum = 0
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate3)

        Dim repoTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3.FormatString = ""
        repoTaxAmt3.WrapText = True
        repoTaxAmt3.HeaderText = "Tax Amount 3"
        repoTaxAmt3.Name = colTaxAmt3
        repoTaxAmt3.IsVisible = False
        repoTaxAmt3.Minimum = 0
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt3)

        Dim repoTotTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt3.FormatString = ""
        repoTotTaxAmt3.WrapText = True
        repoTotTaxAmt3.HeaderText = "Total Tax Amount 3"
        repoTotTaxAmt3.Name = colTotTaxAmt3
        repoTotTaxAmt3.IsVisible = False
        repoTotTaxAmt3.Minimum = 0
        repoTotTaxAmt3.ReadOnly = True
        repoTotTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt3)

        Dim repoTax4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4.FormatString = ""
        repoTax4.HeaderText = "Tax 4"
        repoTax4.Name = colTax4
        repoTax4.IsVisible = False
        repoTax4.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax4)

        Dim repoAssessableAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessableAmt4.FormatString = ""
        repoAssessableAmt4.WrapText = True
        repoAssessableAmt4.HeaderText = "Assessable 4"
        repoAssessableAmt4.Name = colTaxAssessableAmt4
        repoAssessableAmt4.IsVisible = False
        repoAssessableAmt4.Minimum = 0
        repoAssessableAmt4.ReadOnly = True
        repoAssessableAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessableAmt4)

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.WrapText = True
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colTaxRate4
        repoTaxRate4.IsVisible = False
        repoTaxRate4.Minimum = 0
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate4)

        Dim repoTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4.FormatString = ""
        repoTaxAmt4.WrapText = True
        repoTaxAmt4.HeaderText = "Tax Amount 4"
        repoTaxAmt4.Name = colTaxAmt4
        repoTaxAmt4.IsVisible = False
        repoTaxAmt4.Minimum = 0
        repoTaxAmt4.ReadOnly = True
        repoTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt4)

        Dim repoTotTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt4.FormatString = ""
        repoTotTaxAmt4.WrapText = True
        repoTotTaxAmt4.HeaderText = "Total Tax Amount 4"
        repoTotTaxAmt4.Name = colTotTaxAmt4
        repoTotTaxAmt4.IsVisible = False
        repoTotTaxAmt4.Minimum = 0
        repoTotTaxAmt4.ReadOnly = True
        repoTotTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt4)

        Dim repoTax5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax5.FormatString = ""
        repoTax5.HeaderText = "Tax 5"
        repoTax5.Name = colTax5
        repoTax5.IsVisible = False
        repoTax5.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax5)

        Dim repoAssessableAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessableAmt5.FormatString = ""
        repoAssessableAmt5.WrapText = True
        repoAssessableAmt5.HeaderText = "Assessable 5"
        repoAssessableAmt5.Name = colTaxAssessableAmt5
        repoAssessableAmt5.IsVisible = False
        repoAssessableAmt5.Minimum = 0
        repoAssessableAmt5.ReadOnly = True
        repoAssessableAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessableAmt5)

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.WrapText = True
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colTaxRate5
        repoTaxRate5.IsVisible = False
        repoTaxRate5.Minimum = 0
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate5)

        Dim repoTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5.FormatString = ""
        repoTaxAmt5.WrapText = True
        repoTaxAmt5.HeaderText = "Tax Amount 5"
        repoTaxAmt5.Name = colTaxAmt5
        repoTaxAmt5.IsVisible = False
        repoTaxAmt5.Minimum = 0
        repoTaxAmt5.ReadOnly = True
        repoTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt5)

        Dim repoTotTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt5.FormatString = ""
        repoTotTaxAmt5.WrapText = True
        repoTotTaxAmt5.HeaderText = "Total Tax Amount 5"
        repoTotTaxAmt5.Name = colTotTaxAmt5
        repoTotTaxAmt5.IsVisible = False
        repoTotTaxAmt5.Minimum = 0
        repoTotTaxAmt5.ReadOnly = True
        repoTotTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt5)

        Dim repoTax6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax6.FormatString = ""
        repoTax6.HeaderText = "Tax 6"
        repoTax6.Name = colTax6
        repoTax6.IsVisible = False
        repoTax6.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax6)

        Dim repoAssessableAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessableAmt6.FormatString = ""
        repoAssessableAmt6.WrapText = True
        repoAssessableAmt6.HeaderText = "Assessable 6"
        repoAssessableAmt6.Name = colTaxAssessableAmt6
        repoAssessableAmt6.IsVisible = False
        repoAssessableAmt6.Minimum = 0
        repoAssessableAmt6.ReadOnly = True
        repoAssessableAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessableAmt6)

        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.WrapText = True
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colTaxRate6
        repoTaxRate6.IsVisible = False
        repoTaxRate6.Minimum = 0
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate6)

        Dim repoTaxAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt6.FormatString = ""
        repoTaxAmt6.WrapText = True
        repoTaxAmt6.HeaderText = "Tax Amount 6"
        repoTaxAmt6.Name = colTaxAmt6
        repoTaxAmt6.IsVisible = False
        repoTaxAmt6.Minimum = 0
        repoTaxAmt6.ReadOnly = True
        repoTaxAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt6)

        Dim repoTotTaxAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt6.FormatString = ""
        repoTotTaxAmt6.WrapText = True
        repoTotTaxAmt6.HeaderText = "Total Tax Amount 6"
        repoTotTaxAmt6.Name = colTotTaxAmt6
        repoTotTaxAmt6.IsVisible = False
        repoTotTaxAmt6.Minimum = 0
        repoTotTaxAmt6.ReadOnly = True
        repoTotTaxAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt6)

        Dim repoTax7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax7.FormatString = ""
        repoTax7.HeaderText = "Tax 7"
        repoTax7.Name = colTax7
        repoTax7.IsVisible = False
        repoTax7.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax7)

        Dim repoAssessableAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessableAmt7.FormatString = ""
        repoAssessableAmt7.WrapText = True
        repoAssessableAmt7.HeaderText = "Assessable 7"
        repoAssessableAmt7.Name = colTaxAssessableAmt7
        repoAssessableAmt7.IsVisible = False
        repoAssessableAmt7.Minimum = 0
        repoAssessableAmt7.ReadOnly = True
        repoAssessableAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessableAmt7)

        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.WrapText = True
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colTaxRate7
        repoTaxRate7.IsVisible = False
        repoTaxRate7.Minimum = 0
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate7)

        Dim repoTaxAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt7.FormatString = ""
        repoTaxAmt7.WrapText = True
        repoTaxAmt7.HeaderText = "Tax Amount 7"
        repoTaxAmt7.Name = colTaxAmt7
        repoTaxAmt7.IsVisible = False
        repoTaxAmt7.Minimum = 0
        repoTaxAmt7.ReadOnly = True
        repoTaxAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt7)

        Dim repoTotTaxAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt7.FormatString = ""
        repoTotTaxAmt7.WrapText = True
        repoTotTaxAmt7.HeaderText = "Total Tax Amount 7"
        repoTotTaxAmt7.Name = colTotTaxAmt7
        repoTotTaxAmt7.IsVisible = False
        repoTotTaxAmt7.Minimum = 0
        repoTotTaxAmt7.ReadOnly = True
        repoTotTaxAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt7)

        Dim repoTax8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax8.FormatString = ""
        repoTax8.HeaderText = "Tax 8"
        repoTax8.Name = colTax8
        repoTax8.IsVisible = False
        repoTax8.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax8)

        Dim repoAssessableAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessableAmt8.FormatString = ""
        repoAssessableAmt8.WrapText = True
        repoAssessableAmt8.HeaderText = "Assessable 8"
        repoAssessableAmt8.Name = colTaxAssessableAmt8
        repoAssessableAmt8.IsVisible = False
        repoAssessableAmt8.Minimum = 0
        repoAssessableAmt8.ReadOnly = True
        repoAssessableAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessableAmt8)

        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.WrapText = True
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colTaxRate8
        repoTaxRate8.IsVisible = False
        repoTaxRate8.Minimum = 0
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate8)

        Dim repoTaxAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt8.FormatString = ""
        repoTaxAmt8.WrapText = True
        repoTaxAmt8.HeaderText = "Tax Amount 8"
        repoTaxAmt8.Name = colTaxAmt8
        repoTaxAmt8.IsVisible = False
        repoTaxAmt8.Minimum = 0
        repoTaxAmt8.ReadOnly = True
        repoTaxAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt8)

        Dim repoTotTaxAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt8.FormatString = ""
        repoTotTaxAmt8.WrapText = True
        repoTotTaxAmt8.HeaderText = "Total Tax Amount 8"
        repoTotTaxAmt8.Name = colTotTaxAmt8
        repoTotTaxAmt8.IsVisible = False
        repoTotTaxAmt8.Minimum = 0
        repoTotTaxAmt8.ReadOnly = True
        repoTotTaxAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt8)

        Dim repoTax9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax9.FormatString = ""
        repoTax9.HeaderText = "Tax 9"
        repoTax9.Name = colTax9
        repoTax9.IsVisible = False
        repoTax9.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax9)



        Dim repoAssessableAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessableAmt9.FormatString = ""
        repoAssessableAmt9.WrapText = True
        repoAssessableAmt9.HeaderText = "Assessable 9"
        repoAssessableAmt9.Name = colTaxAssessableAmt9
        repoAssessableAmt9.IsVisible = False
        repoAssessableAmt9.Minimum = 0
        repoAssessableAmt9.ReadOnly = True
        repoAssessableAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessableAmt9)

        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.WrapText = True
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colTaxRate9
        repoTaxRate9.IsVisible = False
        repoTaxRate9.Minimum = 0
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate9)

        Dim repoTaxAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt9.FormatString = ""
        repoTaxAmt9.WrapText = True
        repoTaxAmt9.HeaderText = "Tax Amount 9"
        repoTaxAmt9.Name = colTaxAmt9
        repoTaxAmt9.IsVisible = False
        repoTaxAmt9.Minimum = 0
        repoTaxAmt9.ReadOnly = True
        repoTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt9)

        Dim repoTotTaxAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt9.FormatString = ""
        repoTotTaxAmt9.WrapText = True
        repoTotTaxAmt9.HeaderText = "Total Tax Amount 9"
        repoTotTaxAmt9.Name = colTotTaxAmt9
        repoTotTaxAmt9.IsVisible = False
        repoTotTaxAmt9.Minimum = 0
        repoTotTaxAmt9.ReadOnly = True
        repoTotTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt9)

        Dim repoTax10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax10.FormatString = ""
        repoTax10.HeaderText = "Tax 10"
        repoTax10.Name = colTax10
        repoTax10.IsVisible = False
        repoTax10.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax10)

        Dim repoAssessableAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessableAmt10.FormatString = ""
        repoAssessableAmt10.WrapText = True
        repoAssessableAmt10.HeaderText = "Assessable 10"
        repoAssessableAmt10.Name = colTaxAssessableAmt10
        repoAssessableAmt10.IsVisible = False
        repoAssessableAmt10.Minimum = 0
        repoAssessableAmt10.ReadOnly = True
        repoAssessableAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessableAmt10)

        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.WrapText = True
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colTaxRate10
        repoTaxRate10.IsVisible = False
        repoTaxRate10.Minimum = 0
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate10)

        Dim repoTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt10.FormatString = ""
        repoTaxAmt10.WrapText = True
        repoTaxAmt10.HeaderText = "Tax Amount 10"
        repoTaxAmt10.Name = colTaxAmt10
        repoTaxAmt10.IsVisible = False
        repoTaxAmt10.Minimum = 0
        repoTaxAmt10.ReadOnly = True
        repoTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt10)

        Dim repoTotTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt10.FormatString = ""
        repoTotTaxAmt10.WrapText = True
        repoTotTaxAmt10.HeaderText = "Total Tax Amount 10"
        repoTotTaxAmt10.Name = colTotTaxAmt10
        repoTotTaxAmt10.IsVisible = False
        repoTotTaxAmt10.Minimum = 0
        repoTotTaxAmt10.ReadOnly = True
        repoTotTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt10)

        Dim repoItemTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemTax.FormatString = ""
        repoItemTax.WrapText = True
        repoItemTax.HeaderText = "Item Tax"
        repoItemTax.Name = colItemTax
        repoItemTax.Width = 80
        repoItemTax.Minimum = 0
        repoItemTax.ReadOnly = True
        repoItemTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemTax)

        Dim repoTotAssessableAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotAssessableAmt.FormatString = ""
        repoTotAssessableAmt.WrapText = True
        repoTotAssessableAmt.HeaderText = "Total Assessable Amt"
        repoTotAssessableAmt.Name = colTotalAssessableAmt
        repoTotAssessableAmt.Width = 80
        repoTotAssessableAmt.Minimum = 0
        repoTotAssessableAmt.ReadOnly = True
        repoTotAssessableAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotAssessableAmt)

        Dim repoTotMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotMRP.FormatString = ""
        repoTotMRP.WrapText = True
        repoTotMRP.HeaderText = "Total MRP"
        repoTotMRP.Name = colTotalMRPAmt
        repoTotMRP.Width = 80
        repoTotMRP.Minimum = 0
        repoTotMRP.ReadOnly = True
        repoTotMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotMRP)

        Dim repoTotBasicAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotBasicAmt.FormatString = ""
        repoTotBasicAmt.WrapText = True
        repoTotBasicAmt.HeaderText = "Total Basic Amount"
        repoTotBasicAmt.Name = colTotalBasicAmt
        repoTotBasicAmt.Width = 80
        repoTotBasicAmt.Minimum = 0
        repoTotBasicAmt.ReadOnly = True
        repoTotBasicAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotBasicAmt)

        Dim repoTotDiscAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotDiscAmt.FormatString = ""
        repoTotDiscAmt.WrapText = True
        repoTotDiscAmt.HeaderText = "Total Discount Amount"
        repoTotDiscAmt.Name = colTotalDiscAmt
        repoTotDiscAmt.Width = 80
        repoTotDiscAmt.Minimum = 0
        repoTotDiscAmt.ReadOnly = True
        repoTotDiscAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotDiscAmt)

        Dim repoTotNetAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotNetAmt.FormatString = ""
        repoTotNetAmt.WrapText = True
        repoTotNetAmt.HeaderText = "Total Net Amount"
        repoTotNetAmt.Name = colTotalnetAmt
        repoTotNetAmt.Width = 80
        repoTotNetAmt.Minimum = 0
        repoTotNetAmt.ReadOnly = True
        repoTotNetAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotNetAmt)

        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.WrapText = True
        repoTotTaxAmt.HeaderText = "Total Tax Amount"
        repoTotTaxAmt.Name = colTotalTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.Minimum = 0
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)

        Dim repoTotItemAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotItemAmt.FormatString = ""
        repoTotItemAmt.WrapText = True
        repoTotItemAmt.HeaderText = "Total Item Amount"
        repoTotItemAmt.Name = colTotalItemAmt
        repoTotItemAmt.Width = 80
        repoTotItemAmt.Minimum = 0
        repoTotItemAmt.ReadOnly = True
        repoTotItemAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotItemAmt)

        Dim repoEmptyValue As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoEmptyValue.FormatString = ""
        repoEmptyValue.WrapText = True
        repoEmptyValue.HeaderText = "Empty Value"
        repoEmptyValue.Name = colEmptyValue
        repoEmptyValue.Width = 80
        repoEmptyValue.Minimum = 0
        repoEmptyValue.ReadOnly = True
        repoEmptyValue.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoEmptyValue)

        Dim repoTPT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTPT.FormatString = ""
        repoTPT.WrapText = True
        repoTPT.HeaderText = "TPT"
        repoTPT.Name = colTPT
        repoTPT.Width = 80
        repoTPT.Minimum = 0
        repoTPT.ReadOnly = True
        repoTPT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTPT)


        Dim repoTotalTPT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalTPT.FormatString = ""
        repoTotalTPT.WrapText = True
        repoTotalTPT.HeaderText = "Total TPT"
        repoTotalTPT.Name = colTotalTPT
        repoTotalTPT.Width = 80
        repoTotalTPT.Minimum = 0
        repoTotalTPT.ReadOnly = True
        repoTotalTPT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotalTPT)

        Dim repoEmptyValueShell As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoEmptyValueShell.FormatString = ""
        repoEmptyValueShell.WrapText = True
        repoEmptyValueShell.HeaderText = "Empty Value Shell"
        repoEmptyValueShell.Name = colEmptyValueShell
        repoEmptyValueShell.Width = 80
        repoEmptyValueShell.Minimum = 0
        repoEmptyValueShell.ReadOnly = True
        repoEmptyValueShell.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoEmptyValueShell)

        Dim repoEmptyValueBottle As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoEmptyValueBottle.FormatString = ""
        repoEmptyValueBottle.WrapText = True
        repoEmptyValueBottle.HeaderText = "Empty Value Bottle"
        repoEmptyValueBottle.Name = colEmptyValueBottle
        repoEmptyValueBottle.Width = 80
        repoEmptyValueBottle.Minimum = 0
        repoEmptyValueBottle.ReadOnly = True
        repoEmptyValueBottle.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoEmptyValueBottle)

        Dim repoCustDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDiscount.FormatString = ""
        repoCustDiscount.WrapText = True
        repoCustDiscount.HeaderText = "Customer Discount"
        repoCustDiscount.Name = colCustDiscount
        repoCustDiscount.Width = 80
        repoCustDiscount.Minimum = 0
        repoCustDiscount.ReadOnly = True
        repoCustDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCustDiscount)

        Dim repoTotCustDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotCustDiscount.FormatString = ""
        repoTotCustDiscount.WrapText = True
        repoTotCustDiscount.HeaderText = "Total Customer Discount"
        repoTotCustDiscount.Name = colTotalCustDiscount
        repoTotCustDiscount.Width = 80
        repoTotCustDiscount.Minimum = 0
        repoTotCustDiscount.ReadOnly = True
        repoTotCustDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotCustDiscount)

        Dim repoUnitCogs As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnitCogs.FormatString = ""
        repoUnitCogs.WrapText = True
        repoUnitCogs.HeaderText = "Unit Cogs"
        repoUnitCogs.Name = colUnitCogs
        repoUnitCogs.Width = 80
        repoUnitCogs.Minimum = 0
        repoUnitCogs.ReadOnly = True
        repoUnitCogs.IsVisible = False
        repoUnitCogs.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoUnitCogs)

        Dim repopriceamt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopriceamt1.FormatString = ""
        repopriceamt1.WrapText = True
        repopriceamt1.HeaderText = "PRICE AMOUNT1"
        repopriceamt1.Name = colpriceamount1
        repopriceamt1.Width = 80
        repopriceamt1.Minimum = 0
        repopriceamt1.ReadOnly = True
        repopriceamt1.IsVisible = False
        repopriceamt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopriceamt1)

        Dim repopriceamt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopriceamt2.FormatString = ""
        repopriceamt2.WrapText = True
        repopriceamt2.HeaderText = "PRICE AMOUNT2"
        repopriceamt2.Name = colpriceamount2
        repopriceamt2.Width = 80
        repopriceamt2.Minimum = 0
        repopriceamt2.ReadOnly = True
        repopriceamt2.IsVisible = False
        repopriceamt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopriceamt2)

        Dim repopriceamt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopriceamt3.FormatString = ""
        repopriceamt3.WrapText = True
        repopriceamt3.HeaderText = "PRICE AMOUNT3"
        repopriceamt3.Name = colpriceamount3
        repopriceamt3.Width = 80
        repopriceamt3.Minimum = 0
        repopriceamt3.ReadOnly = True
        repopriceamt3.IsVisible = False
        repopriceamt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopriceamt3)

        Dim repopriceamt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopriceamt4.FormatString = ""
        repopriceamt4.WrapText = True
        repopriceamt4.HeaderText = "PRICE AMOUNT4"
        repopriceamt4.Name = colpriceamount4
        repopriceamt4.Width = 80
        repopriceamt4.Minimum = 0
        repopriceamt4.ReadOnly = True
        repopriceamt4.IsVisible = False
        repopriceamt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopriceamt4)

        Dim repopriceamt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopriceamt5.FormatString = ""
        repopriceamt5.WrapText = True
        repopriceamt5.HeaderText = "PRICE AMOUNT5"
        repopriceamt5.Name = colpriceamount5
        repopriceamt5.Width = 80
        repopriceamt5.Minimum = 0
        repopriceamt5.ReadOnly = True
        repopriceamt5.IsVisible = False
        repopriceamt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopriceamt5)

        Dim repopriceamt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopriceamt6.FormatString = ""
        repopriceamt6.WrapText = True
        repopriceamt6.HeaderText = "PRICE AMOUNT6"
        repopriceamt6.Name = colpriceamount6
        repopriceamt6.Width = 80
        repopriceamt6.Minimum = 0
        repopriceamt6.ReadOnly = True
        repopriceamt6.IsVisible = False
        repopriceamt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopriceamt6)

        Dim repopriceamt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopriceamt7.FormatString = ""
        repopriceamt7.WrapText = True
        repopriceamt7.HeaderText = "PRICE AMOUNT7"
        repopriceamt7.Name = colpriceamount7
        repopriceamt7.Width = 80
        repopriceamt7.Minimum = 0
        repopriceamt7.ReadOnly = True
        repopriceamt7.IsVisible = False
        repopriceamt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopriceamt7)

        Dim repopriceamt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopriceamt8.FormatString = ""
        repopriceamt8.WrapText = True
        repopriceamt8.HeaderText = "PRICE AMOUNT8"
        repopriceamt8.Name = colpriceamount8
        repopriceamt8.Width = 80
        repopriceamt8.Minimum = 0
        repopriceamt8.ReadOnly = True
        repopriceamt8.IsVisible = False
        repopriceamt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopriceamt8)

        Dim repopriceamt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopriceamt9.FormatString = ""
        repopriceamt9.WrapText = True
        repopriceamt9.HeaderText = "PRICE AMOUNT9"
        repopriceamt9.Name = colpriceamount9
        repopriceamt9.Width = 80
        repopriceamt9.Minimum = 0
        repopriceamt9.ReadOnly = True
        repopriceamt9.IsVisible = False
        repopriceamt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopriceamt9)

        Dim repopriceamt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopriceamt10.FormatString = ""
        repopriceamt10.WrapText = True
        repopriceamt10.HeaderText = "PRICE AMOUNT10"
        repopriceamt10.Name = colpriceamount10
        repopriceamt10.Width = 80
        repopriceamt10.Minimum = 0
        repopriceamt10.ReadOnly = True
        repopriceamt10.IsVisible = False
        repopriceamt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopriceamt10)



        Dim repomainItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomainItem.FormatString = ""
        repomainItem.HeaderText = "Main Item"
        repomainItem.Name = colmainItem
        repomainItem.Width = 100
        repomainItem.IsVisible = False
        repomainItem.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repomainItem)

        Dim repodiscountCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repodiscountCode.FormatString = ""
        repodiscountCode.HeaderText = "Discount Code"
        repodiscountCode.Name = coldiscountcode
        repodiscountCode.Width = 100
        repodiscountCode.IsVisible = False
        repodiscountCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repodiscountCode)

        Dim repoNoTax As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNoTax.FormatString = ""
        repoNoTax.HeaderText = "Customer Discount NoTax"
        repoNoTax.Name = colCustDiscountNoTax
        repoNoTax.WrapText = True
        repoNoTax.Width = 80
        repoNoTax.IsVisible = True
        repoNoTax.ReadOnly = True
        repoNoTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNoTax)


        Dim repoFromSchemeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFromSchemeCode.FormatString = ""
        repoFromSchemeCode.HeaderText = "From Scheme Code"
        repoFromSchemeCode.Name = ColFromSchemeCode
        repoFromSchemeCode.Width = 100
        repoFromSchemeCode.IsVisible = False
        repoFromSchemeCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFromSchemeCode)

        Dim repoTargetDisAmt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTargetDisAmt.FormatString = ""
        repoTargetDisAmt.HeaderText = "Target Discount Amount"
        repoTargetDisAmt.Name = ColTargetDisAmt
        repoTargetDisAmt.WrapText = True
        repoTargetDisAmt.Width = 80
        repoTargetDisAmt.IsVisible = False
        repoTargetDisAmt.ReadOnly = True
        repoTargetDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTargetDisAmt)

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
        repoTaxBaseAmt.Name = colTAssessAmt
        repoTaxBaseAmt.IsVisible = False
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
        txtDate.Focus()
        btnPrint.Enabled = False

    End Sub

    Function AllowToSave() As Boolean
        Dim isCalculateShall As Boolean = IIf(txtshellqty.Value > 0, False, True)
        If clsLocation.isLocatinExcisable(txtLocation.Value, Nothing) Then
            isCalculateShall = True
        End If
        Dim arrShall As New Dictionary(Of Integer, Double)
        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colReturnQty).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            If dblQty > 0 Then
                If isCalculateShall AndAlso clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colEmptyValue).Value) > 0 Then
                    Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                    If Not arrShall.ContainsKey(dblConvFac) Then
                        arrShall.Add(dblConvFac, 0)
                    End If
                    arrShall(dblConvFac) += dblQty
                End If
            End If
        Next
        If isCalculateShall AndAlso arrShall.Count > 0 Then
            Dim dblTotShall As Double
            For Each Keys As Integer In arrShall.Keys
                dblTotShall += Math.Ceiling(arrShall(Keys) / Keys)
            Next
            txtshellqty.Value = clsCommon.myCstr(dblTotShall)
        End If


        Dim isFullyRetun As Boolean = True
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                isFullyRetun = isFullyRetun AndAlso IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(colReturnQty).Value) = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value), True, False)
            End If
            If Not isFullyRetun Then
                Exit For
            End If
        Next

        For ii As Integer = 0 To gv1.Rows.Count - 1
            UpdateCurrentRow(ii, isFullyRetun)
        Next
        UpdateAllTotals()


        If clsCommon.myLen(txtInvoiceNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Sales Invoice No")
            txtCustomerNo.Focus()
            Return False
        End If

        If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Document No Not found to save")
            txtDocNo.Focus()
            Return False
        End If

        If btnSave.Text = "Update" Then
            Dim strchk As String = "select Is_Post from TSPL_SALE_RETURN_HEAD where Sale_Return_No='" + txtDocNo.Value + "'"
            Dim chkpost As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strchk))
            If chkpost = "Y" Then
                clsCommon.MyMessageBoxShow("Transection already posted")
                Return False
            End If
        End If
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please enter location")
            txtLocation.Focus()
            Return False
        End If

        Dim qry As String = ""
        For ii As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(ii).Cells(ColTargetDisAmt).Value = 0
            Dim strSchemeCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(ColFromSchemeCode).Value)
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim dblMRP As String = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRPAmt).Value)
            Dim strPriceDate As String = clsCommon.GetPrintDate(clsCommon.myCDate(gv1.Rows(ii).Cells(colPriceDate).Value), "yyyy-MM-dd")
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)

            Dim strPriceCode As String = txtPriceCode.Text
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colActualReturnQty).Value)

            If clsCommon.myLen(strSchemeCode) >= 2 Then
                Dim strTwoCharacher As String = strSchemeCode.Substring(0, 2)
                If clsCommon.CompairString(strTwoCharacher, "MS") = CompairStringResult.Equal Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colmainItem).Value) <= 0 AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(coldiscountcode).Value) <= 0 Then
                        Throw New Exception("Please fill the Main Item/Discount Code at Row No" + clsCommon.myCstr(ii + 1))
                    End If
                    Dim strDisCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(coldiscountcode).Value).Trim()
                    If clsCommon.myLen(strDisCode) > 0 Then
                        qry = "select Price_Amount1,Price_Amount4,Price_Amount5,Price_Amount6,Price_Amount7,NetLTPT,Price_Amount10 from TSPL_ITEM_PRICE_MASTER where Item_Code='" + strICode + "' and Start_Date='" + strPriceDate + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + clsCommon.myCstr(dblMRP) + "' and  Price_Code='" + strPriceCode + "' "
                        Dim dtPC As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dtPC Is Nothing OrElse dtPC.Rows.Count <= 0 Then
                            Throw New Exception("Price Component not found for item " + strICode + " at Row No " + (clsCommon.myCstr(ii + 1)))
                        End If
                        Dim dblActMRP As Double = clsCommon.myCdbl(dtPC.Rows(0)("NetLTPT")) + clsCommon.myCdbl(dtPC.Rows(0)("Price_Amount10"))
                        Dim dblTargetDiscountAmt As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colActualReturnQty).Value) * dblActMRP
                        gv1.Rows(ii).Cells(ColTargetDisAmt).Value = dblTargetDiscountAmt
                    End If
                End If
            End If
        Next
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegtiveOfSaleInvoiceBlanceAmt, clsFixedParameterCode.AllowNegtiveOfSaleInvoiceBlanceAmt, Nothing)) = 0 Then
            qry = "select Total_Invoice_Amt from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + txtInvoiceNo.Value + "'"
            Dim dblSaleInvoiceAmt As Double = clsDBFuncationality.getSingleValue(qry)

            If dblSaleInvoiceAmt < clsCommon.myCdbl(lblTotAmt.Text) Then
                clsCommon.MyMessageBoxShow("Sale Return Amount can't be more than Invoice Amount ." + Environment.NewLine + "i.e. Invoice Amount :" + clsCommon.myFormat(dblSaleInvoiceAmt) + " and Sale Return Amount is : " + lblTotAmt.Text)
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(clicked)
    End Sub

    Sub SaveData(ByVal clicked As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsSalesReturnHead()

                obj.Sale_Return_No = txtDocNo.Value
                obj.Sale_Return_Date = txtDate.Value
                obj.Cust_Code = txtCustomerNo.Value
                obj.Cust_Name = lblCustomerName.Text
                obj.Cust_PONo = txtCustomerPONO.Text
                obj.Tax_Group = txtTaxGroup.Value
                obj.Location = txtLocation.Value
                'obj.LocationName = lblLocation.Text
                obj.Route_No = txtRouteNo.Value
                obj.Route_Desc = lblRouteNo.Text
                obj.Salesman_Code = txtSalesman.Value
                'obj.SalesManName = lblSalesman.Text
                obj.Mode_Of_Transport = txtModeOfTransport.Text
                obj.Vehicle_Code = txtVehicleNo.Value
                obj.shippmentType = Shippment_type

                obj.Vehicle_No = lblVehicleNo.Text
                obj.Scheme_Sample_Code = txtSchemeSampleCode.Text
                obj.KM_Reading = txtKMReading.Text
                obj.Invoice_No = txtInvoiceNo.Value
                obj.Invoice_Date = txtInvoiceDate.Value
                obj.Total_Assessable_Amount = clsCommon.myCdbl(lblAssessableAmt.Text)
                obj.Inv_Detail_Disc_Amt = lblDetailDiscountAmt.Text
                obj.Inv_Tax_Amt = lblTaxAmt.Text
                obj.Freight_Amt = lblFreightAmount.Text
                obj.Other_Charges = lblOtherCharges.Text
                obj.Add_Charges = lblAdditionalCharges.Text
                obj.Inv_Detail_Total_Amt = lblDetaolTotAmt.Text
                obj.Inv_Discount_Amt = lblDiscountAmt.Text
                obj.Total_Invoice_Amt = lblTotAmt.Text
                obj.TPT = lblTPT.Text
                obj.Empty_Value = lblEmptyValue.Text
                obj.Terms_Code = txtTermCode.Value
                obj.Price_Code = txtPriceCode.Text
                obj.Cust_Account = lblCustAccount.Text

                obj.Level1_User_code = lblLevel1_User_code.Text
                obj.Level2_User_code = lblLevel2_User_code.Text
                obj.Level3_User_code = lblLevel3_User_code.Text
                obj.Level4_User_code = lblLevel4_User_code.Text
                obj.Level5_User_code = lblLevel5_User_code.Text

                obj.Ref_No = txtRefNo.Text
                obj.Description = txtDescription.Text
                obj.Comments = txtRemarks.Text
                obj.Shell_Qty = txtshellqty.Value

                ''obj.Level1_User_Commission = lblLevel1_User_Commission.Text
                ''obj.Level2_User_Commission = lblLevel2_User_Commission.Text
                ''obj.Level3_User_Commission = lblLevel3_User_Commission.Text
                ''obj.Level4_User_Commission = lblLevel4_User_Commission.Text
                ''obj.Level5_User_Commission = lblLevel5_User_Commission.Text

                If (gv2.Rows.Count > 0) Then
                    obj.TAX1 = clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value)
                    obj.TAX1_Rate = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxRate).Value)
                    obj.TAX1_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTAssessAmt).Value)
                    obj.TAX1_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 1) Then
                    obj.TAX2 = clsCommon.myCstr(gv2.Rows(1).Cells(colTTaxAutCode).Value)
                    obj.TAX2_Rate = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxRate).Value)
                    obj.TAX2_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTAssessAmt).Value)
                    obj.TAX2_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 2) Then
                    obj.TAX3 = clsCommon.myCstr(gv2.Rows(2).Cells(colTTaxAutCode).Value)
                    obj.TAX3_Rate = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxRate).Value)
                    obj.TAX3_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTAssessAmt).Value)
                    obj.TAX3_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 3) Then
                    obj.TAX4 = clsCommon.myCstr(gv2.Rows(3).Cells(colTTaxAutCode).Value)
                    obj.TAX4_Rate = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxRate).Value)
                    obj.TAX4_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTAssessAmt).Value)
                    obj.TAX4_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 4) Then
                    obj.TAX5 = clsCommon.myCstr(gv2.Rows(4).Cells(colTTaxAutCode).Value)
                    obj.TAX5_Rate = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxRate).Value)
                    obj.TAX5_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTAssessAmt).Value)
                    obj.TAX5_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 5) Then
                    obj.TAX6 = clsCommon.myCstr(gv2.Rows(5).Cells(colTTaxAutCode).Value)
                    obj.TAX6_Rate = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxRate).Value)
                    obj.TAX6_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTAssessAmt).Value)
                    obj.TAX6_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 6) Then
                    obj.TAX7 = clsCommon.myCstr(gv2.Rows(6).Cells(colTTaxAutCode).Value)
                    obj.TAX7_Rate = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxRate).Value)
                    obj.TAX7_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTAssessAmt).Value)
                    obj.TAX7_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 7) Then
                    obj.TAX8 = clsCommon.myCstr(gv2.Rows(7).Cells(colTTaxAutCode).Value)
                    obj.TAX8_Rate = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxRate).Value)
                    obj.TAX8_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTAssessAmt).Value)
                    obj.TAX8_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 8) Then
                    obj.TAX9 = clsCommon.myCstr(gv2.Rows(8).Cells(colTTaxAutCode).Value)
                    obj.TAX9_Rate = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxRate).Value)
                    obj.TAX9_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTAssessAmt).Value)
                    obj.TAX9_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 9) Then
                    obj.TAX10 = clsCommon.myCstr(gv2.Rows(9).Cells(colTTaxAutCode).Value)
                    obj.TAX10_Rate = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxRate).Value)
                    obj.TAX10_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTAssessAmt).Value)
                    obj.TAX10_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxAmt).Value)
                End If

                obj.Arr = New List(Of clsSalesReturnDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsSalesReturnDetail()
                    objTr.Sale_Return_Id = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)

                    objTr.Actual_Return_Qty = clsCommon.myCdbl(grow.Cells(colActualReturnQty).Value)
                    objTr.Invoice_Qty = clsCommon.myCdbl(grow.Cells(colPendingQty).Value)
                    objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colActualReturnQty).Value)


                    objTr.Leak_Qty = clsCommon.myCdbl(grow.Cells(colLeakQty).Value)
                    objTr.Burst_Qty = clsCommon.myCdbl(grow.Cells(colBurstQty).Value)
                    objTr.Short_Qty = clsCommon.myCdbl(grow.Cells(colShortQty).Value)
                    objTr.Return_Qty = clsCommon.myCdbl(grow.Cells(colReturnQty).Value)

                    objTr.Price_Date = clsCommon.myCDate(grow.Cells(colPriceDate).Value)
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Price_code = clsCommon.myCstr(grow.Cells(colPriceCode).Value)
                    objTr.Scheme_Applicable = clsCommon.myCstr(grow.Cells(colSchemeApplicable).Value)
                    objTr.Scheme_Code_Qty = clsCommon.myCdbl(grow.Cells(colSchemeCodeQty).Value)
                    objTr.Scheme_Item = clsCommon.myCstr(grow.Cells(colSchemeItem).Value)
                    objTr.Promo_Scheme_Applicable = clsCommon.myCstr(grow.Cells(colPromoSchemeApplicable).Value)
                    objTr.Promo_Scheme_Item = clsCommon.myCstr(grow.Cells(colPromoSchemeApplicable).Value) ''Promo_Scheme_Applicable and Promo_Scheme_Item Both are same
                    objTr.Promo_Scheme_Code = clsCommon.myCstr(grow.Cells(colPromoSchemeCode).Value)
                    objTr.Scheme_Disc_Applicable = clsCommon.myCstr(grow.Cells(colSchemeDiscApplicable).Value)
                    objTr.Scheme_Code_Cash = clsCommon.myCstr(grow.Cells(colSchemeCodeCash).Value)
                    objTr.Sampling_Item = clsCommon.myCstr(grow.Cells(colSamplingItem).Value)
                    objTr.MRP_Amt = clsCommon.myCdbl(grow.Cells(colMRPAmt).Value)
                    objTr.Basic_Rate = clsCommon.myCdbl(grow.Cells(colBasicRate).Value)
                    objTr.Item_Assessable_Rate = clsCommon.myCdbl(grow.Cells(colItemAssessableRate).Value)
                    objTr.Disc_Amt = clsCommon.myCdbl(grow.Cells(colDiscAmt).Value)
                    objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colItemNetAmt).Value)
                    objTr.TAX1 = clsCommon.myCstr(grow.Cells(colTax1).Value)
                    objTr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate1).Value)
                    objTr.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt1).Value)
                    objTr.TAX1_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colTaxAssessableAmt1).Value)
                    objTr.TAX2 = clsCommon.myCstr(grow.Cells(colTax2).Value)
                    objTr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate2).Value)
                    objTr.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt2).Value)
                    objTr.TAX2_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colTaxAssessableAmt2).Value)
                    objTr.TAX3 = clsCommon.myCstr(grow.Cells(colTax3).Value)
                    objTr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate3).Value)
                    objTr.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt3).Value)
                    objTr.TAX3_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colTaxAssessableAmt3).Value)
                    objTr.TAX4 = clsCommon.myCstr(grow.Cells(colTax4).Value)
                    objTr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate4).Value)
                    objTr.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt4).Value)
                    objTr.TAX4_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colTaxAssessableAmt4).Value)
                    objTr.TAX5 = clsCommon.myCstr(grow.Cells(colTax5).Value)
                    objTr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate5).Value)
                    objTr.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt5).Value)
                    objTr.TAX5_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colTaxAssessableAmt5).Value)
                    objTr.TAX6 = clsCommon.myCstr(grow.Cells(colTax6).Value)
                    objTr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate6).Value)
                    objTr.TAX6_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt6).Value)
                    objTr.TAX6_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colTaxAssessableAmt6).Value)
                    objTr.TAX7 = clsCommon.myCstr(grow.Cells(colTax7).Value)
                    objTr.TAX7_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate7).Value)
                    objTr.TAX7_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt7).Value)
                    objTr.TAX7_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colTaxAssessableAmt7).Value)
                    objTr.TAX8 = clsCommon.myCstr(grow.Cells(colTax8).Value)
                    objTr.TAX8_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate8).Value)
                    objTr.TAX8_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt8).Value)
                    objTr.TAX8_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colTaxAssessableAmt8).Value)
                    objTr.TAX9 = clsCommon.myCstr(grow.Cells(colTax9).Value)
                    objTr.TAX9_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate9).Value)
                    objTr.TAX9_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt9).Value)
                    objTr.TAX9_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colTaxAssessableAmt9).Value)
                    objTr.TAX10 = clsCommon.myCstr(grow.Cells(colTax10).Value)
                    objTr.TAX10_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate10).Value)
                    objTr.TAX10_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt10).Value)
                    objTr.TAX10_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colTaxAssessableAmt10).Value)


                    objTr.TOT_TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt1).Value)
                    objTr.TOT_TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt2).Value)
                    objTr.TOT_TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt3).Value)
                    objTr.TOT_TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt4).Value)
                    objTr.TOT_TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt5).Value)
                    objTr.TOT_TAX6_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt6).Value)
                    objTr.TOT_TAX7_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt7).Value)
                    objTr.TOT_TAX8_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt8).Value)
                    objTr.TOT_TAX9_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt9).Value)
                    objTr.TOT_TAX10_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt10).Value)


                    objTr.Item_Tax = clsCommon.myCdbl(grow.Cells(colItemTax).Value)
                    objTr.Total_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colTotalAssessableAmt).Value)
                    objTr.Total_MRP_Amt = clsCommon.myCdbl(grow.Cells(colTotalMRPAmt).Value)
                    objTr.Total_Basic_Amt = clsCommon.myCdbl(grow.Cells(colTotalBasicAmt).Value)
                    objTr.Total_Disc_Amt = clsCommon.myCdbl(grow.Cells(colTotalDiscAmt).Value)
                    objTr.Total_net_Amt = clsCommon.myCdbl(grow.Cells(colTotalnetAmt).Value)
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTotalTaxAmt).Value)
                    objTr.Total_Item_Amt = clsCommon.myCdbl(grow.Cells(colTotalItemAmt).Value)
                    objTr.Empty_Value = clsCommon.myCdbl(grow.Cells(colEmptyValue).Value)
                    objTr.TPT = clsCommon.myCdbl(grow.Cells(colTPT).Value)
                    objTr.Total_TPT = clsCommon.myCdbl(grow.Cells(colTotalTPT).Value)
                    objTr.Empty_Value_Shell = clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value)
                    objTr.Empty_Value_Bottle = clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value)
                    objTr.Cust_Discount = clsCommon.myCdbl(grow.Cells(colCustDiscount).Value)
                    objTr.Total_Cust_Discount = clsCommon.myCdbl(grow.Cells(colTotalCustDiscount).Value)
                    objTr.Unit_Cogs = clsCommon.myCdbl(grow.Cells(colUnitCogs).Value)
                    objTr.Price_Amount1 = clsCommon.myCdbl(grow.Cells(colpriceamount1).Value)
                    objTr.Price_Amount2 = clsCommon.myCdbl(grow.Cells(colpriceamount2).Value)
                    objTr.Price_Amount3 = clsCommon.myCdbl(grow.Cells(colpriceamount3).Value)
                    objTr.Price_Amount4 = clsCommon.myCdbl(grow.Cells(colpriceamount4).Value)
                    objTr.Price_Amount5 = clsCommon.myCdbl(grow.Cells(colpriceamount5).Value)
                    objTr.Price_Amount6 = clsCommon.myCdbl(grow.Cells(colpriceamount6).Value)
                    objTr.Price_Amount7 = clsCommon.myCdbl(grow.Cells(colpriceamount7).Value)
                    objTr.Price_Amount8 = clsCommon.myCdbl(grow.Cells(colpriceamount8).Value)
                    objTr.Price_Amount9 = clsCommon.myCdbl(grow.Cells(colpriceamount9).Value)
                    objTr.Price_Amount10 = clsCommon.myCdbl(grow.Cells(colpriceamount10).Value)
                    objTr.Main_item = clsCommon.myCstr(grow.Cells(colmainItem).Value)
                    objTr.Discount_Code = clsCommon.myCstr(grow.Cells(coldiscountcode).Value)
                    objTr.Cust_Item_Discount_NoTax = clsCommon.myCdbl(grow.Cells(colCustDiscountNoTax).Value)


                    objTr.From_Scheme_Code = clsCommon.myCstr(grow.Cells(ColFromSchemeCode).Value)
                    objTr.Target_Discount_Amt = clsCommon.myCdbl(grow.Cells(ColTargetDisAmt).Value)

                    obj.Arr.Add(objTr)
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                    Return
                End If
                If (obj.SaveData(obj, isNewEntry)) Then
                    If clicked = False Then
                        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    End If

                    LoadData(obj.Sale_Return_No, NavigatorType.Current)
                    btnPrint.Enabled = True
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
            btnPrint.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()
            LoadBlankGridTax()
            txtLocation.Enabled = True
            Dim obj As New clsSalesReturnHead()
            obj = clsSalesReturnHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Sale_Return_No) > 0) Then
                If clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
                txtDate.Value = obj.Sale_Return_Date
                txtDocNo.Value = obj.Sale_Return_No
                txtCustomerNo.Value = obj.Cust_Code
                lblCustomerName.Text = obj.Cust_Name
                txtCustomerPONO.Text = obj.Cust_PONo
                txtTaxGroup.Value = obj.Tax_Group
                txtLocation.Value = obj.Location
                lblLocation.Text = clsLocation.GetName(obj.Location, Nothing)
                txtInvoiceNo.Value = obj.Invoice_No
                txtRouteNo.Value = obj.Route_No
                lblRouteNo.Text = obj.Route_Desc
                txtSalesman.Value = obj.Salesman_Code
                lblSalesman.Text = clsEmployeeMaster.GetName(obj.Salesman_Code, Nothing)
                txtModeOfTransport.Text = obj.Mode_Of_Transport
                txtVehicleNo.Value = obj.Vehicle_Code
                lblVehicleNo.Text = obj.Vehicle_No

                txtSchemeSampleCode.Text = obj.Scheme_Sample_Code
                txtKMReading.Text = obj.KM_Reading
                txtInvoiceDate.Text = obj.Invoice_Date
                lblCustAccount.Text = obj.Cust_Account
                lblAssessableAmt.Text = obj.Total_Assessable_Amount
                lblDetailDiscountAmt.Text = obj.Inv_Detail_Disc_Amt
                lblTaxAmt.Text = clsCommon.myFormat(obj.Inv_Tax_Amt)
                lblFreightAmount.Text = obj.Freight_Amt
                lblOtherCharges.Text = obj.Other_Charges
                lblAdditionalCharges.Text = obj.Add_Charges


                txtRefNo.Text = obj.Ref_No
                txtDescription.Text = obj.Description
                txtRemarks.Text = obj.Comments

                lblDetaolTotAmt.Text = clsCommon.myFormat(obj.Inv_Detail_Total_Amt)
                lblDiscountAmt.Text = clsCommon.myFormat(obj.Inv_Discount_Amt)
                lblTotAmt.Text = clsCommon.myFormat(obj.Total_Invoice_Amt)
                lblTPT.Text = clsCommon.myFormat(obj.TPT)
                lblEmptyValue.Text = clsCommon.myFormat(obj.Empty_Value)
                txtTermCode.Value = obj.Terms_Code
                txtPriceCode.Text = obj.Price_Code
                lblNetAmt.Text = clsCommon.myFormat(clsCommon.myCdbl(lblEmptyValue.Text) + clsCommon.myCdbl(lblTotAmt.Text))
                lblLevel1_User_code.Text = obj.Level1_User_code
                lblLevel2_User_code.Text = obj.Level2_User_code
                lblLevel3_User_code.Text = obj.Level3_User_code
                lblLevel4_User_code.Text = obj.Level4_User_code
                lblLevel5_User_code.Text = obj.Level5_User_code
                txtshellqty.Value = obj.Shell_Qty
                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX1
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(obj.TAX1)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX1_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = obj.TAX1_Assessable_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX1_Amt
                End If
                If (clsCommon.myLen(obj.TAX2) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX2
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(obj.TAX2)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX2_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = obj.TAX2_Assessable_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX2_Amt
                End If
                If (clsCommon.myLen(obj.TAX3) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX3
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(obj.TAX3)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX3_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = obj.TAX3_Assessable_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX3_Amt
                End If
                If (clsCommon.myLen(obj.TAX4) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX4
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(obj.TAX4)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX4_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = obj.TAX4_Assessable_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX4_Amt
                End If
                If (clsCommon.myLen(obj.TAX5) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX5
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(obj.TAX5)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX5_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = obj.TAX5_Assessable_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX5_Amt
                End If
                If (clsCommon.myLen(obj.TAX6) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX6
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(obj.TAX6)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX6_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = obj.TAX6_Assessable_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX6_Amt
                End If
                If (clsCommon.myLen(obj.TAX7) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX7
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(obj.TAX7)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX7_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = obj.TAX7_Assessable_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX7_Amt
                End If
                If (clsCommon.myLen(obj.TAX8) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX8
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(obj.TAX8)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX8_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = obj.TAX8_Assessable_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX8_Amt
                End If
                If (clsCommon.myLen(obj.TAX9) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX9
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(obj.TAX9)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX9_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = obj.TAX9_Assessable_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX9_Amt
                End If
                If (clsCommon.myLen(obj.TAX10) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX10
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(obj.TAX10)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX10_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = obj.TAX10_Assessable_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX10_Amt
                End If

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsSalesReturnDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Sale_Return_Id
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colActualReturnQty).Value = objTr.Actual_Return_Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLeakQty).Value = objTr.Leak_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBurstQty).Value = objTr.Burst_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShortQty).Value = objTr.Short_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReturnQty).Value = objTr.Return_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDate).Value = objTr.Price_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCode).Value = objTr.Price_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = objTr.Scheme_Applicable
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeCodeQty).Value = objTr.Scheme_Code_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeItem).Value = objTr.Scheme_Item
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPromoSchemeApplicable).Value = objTr.Promo_Scheme_Applicable
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPromoSchemeCode).Value = objTr.Promo_Scheme_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeDiscApplicable).Value = objTr.Scheme_Disc_Applicable
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeCodeCash).Value = objTr.Scheme_Code_Cash
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSamplingItem).Value = objTr.Sampling_Item
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRPAmt).Value = objTr.MRP_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBasicRate).Value = objTr.Basic_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemAssessableRate).Value = objTr.Item_Assessable_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDiscAmt).Value = objTr.Disc_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemNetAmt).Value = objTr.Item_Net_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax1).Value = objTr.TAX1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = objTr.TAX1_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = objTr.TAX1_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt1).Value = objTr.TAX1_Assessable_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax2).Value = objTr.TAX2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = objTr.TAX2_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = objTr.TAX2_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt2).Value = objTr.TAX2_Assessable_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax3).Value = objTr.TAX3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = objTr.TAX3_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = objTr.TAX3_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt3).Value = objTr.TAX3_Assessable_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax4).Value = objTr.TAX4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = objTr.TAX4_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = objTr.TAX4_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt4).Value = objTr.TAX4_Assessable_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax5).Value = objTr.TAX5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = objTr.TAX5_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = objTr.TAX5_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt5).Value = objTr.TAX5_Assessable_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax6).Value = objTr.TAX6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = objTr.TAX6_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = objTr.TAX6_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt6).Value = objTr.TAX6_Assessable_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax7).Value = objTr.TAX7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = objTr.TAX7_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = objTr.TAX7_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt7).Value = objTr.TAX7_Assessable_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax8).Value = objTr.TAX8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = objTr.TAX8_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = objTr.TAX8_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt8).Value = objTr.TAX8_Assessable_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax9).Value = objTr.TAX9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = objTr.TAX9_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = objTr.TAX9_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt9).Value = objTr.TAX9_Assessable_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax10).Value = objTr.TAX10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = objTr.TAX10_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = objTr.TAX10_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt10).Value = objTr.TAX10_Assessable_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTax).Value = objTr.Item_Tax
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssessableAmt).Value = objTr.Total_Assessable_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalMRPAmt).Value = objTr.Total_MRP_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalBasicAmt).Value = objTr.Total_Basic_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalDiscAmt).Value = objTr.Total_Disc_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalnetAmt).Value = objTr.Total_net_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalTaxAmt).Value = objTr.Total_Tax_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalItemAmt).Value = objTr.Total_Item_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmptyValue).Value = objTr.Empty_Value
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTPT).Value = objTr.TPT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalTPT).Value = objTr.Total_TPT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmptyValueShell).Value = objTr.Empty_Value_Shell
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmptyValueBottle).Value = objTr.Empty_Value_Bottle
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustDiscount).Value = objTr.Cust_Discount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalCustDiscount).Value = objTr.Total_Cust_Discount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCogs).Value = objTr.Unit_Cogs
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount1).Value = objTr.Price_Amount1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount2).Value = objTr.Price_Amount2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount3).Value = objTr.Price_Amount3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount4).Value = objTr.Price_Amount4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount5).Value = objTr.Price_Amount5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount6).Value = objTr.Price_Amount6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount7).Value = objTr.Price_Amount7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount8).Value = objTr.Price_Amount8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount9).Value = objTr.Price_Amount9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount10).Value = objTr.Price_Amount10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colmainItem).Value = objTr.Main_item
                        gv1.Rows(gv1.Rows.Count - 1).Cells(coldiscountcode).Value = objTr.Discount_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustDiscountNoTax).Value = objTr.Cust_Item_Discount_NoTax


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt1).Value = objTr.TOT_TAX1_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt2).Value = objTr.TOT_TAX2_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt3).Value = objTr.TOT_TAX3_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt4).Value = objTr.TOT_TAX4_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt5).Value = objTr.TOT_TAX5_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt6).Value = objTr.TOT_TAX6_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt7).Value = objTr.TOT_TAX7_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt8).Value = objTr.TOT_TAX8_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt9).Value = objTr.TOT_TAX9_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt10).Value = objTr.TOT_TAX10_Amt


                        Dim qry As String = "select  Invoice_Qty from TSPL_SALE_INVOICE_DETAIL where Sale_Invoice_No='" + obj.Invoice_No + "' and Item_Code='" + objTr.Item_Code + "' and Unit_code='" + objTr.Unit_code + "'"
                        If clsCommon.myLen(objTr.Main_item) > 0 Then
                            qry += " and Main_Item='" + objTr.Main_item + "'"
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColFromSchemeCode).Value = objTr.From_Scheme_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColTargetDisAmt).Value = objTr.Target_Discount_Amt

                    Next
                End If
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
        clicked = True
        PostData()
    End Sub

    Sub PostData()

        Try
            If (myMessages.postConfirm()) Then
                SaveData(clicked)
                If (clsSalesReturnHead.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                    btnPrint.Enabled = True
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        clicked = False
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Public Sub DeleteData()
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
                If (clsSalesReturnHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = "Delete"
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Function funSetUserAccess() As Boolean
        Try
            btnSave.Visible = True
            btnDelete.Visible = True
            btnPost.Visible = True

            Dim strRights As String
            Dim strTemp() As String
            Dim strProgCode = "SALE-RET"
            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
            strTemp = Split(strRights, ",")
            If strTemp(0) = "0" Then
                MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
                funSetUserAccess = False
                blnRead = False
                Me.Close()
                Exit Function
            Else
                blnRead = True
            End If
            If strTemp(1) = "0" Then 'Grant modify access
                btnSave.Visible = False
            End If
            If strTemp(2) = "0" Then 'Grant modify access
                btnDelete.Visible = False
            End If
            If strTemp(3) = "0" Then 'Grant Authorize access
                btnPost.Visible = False
            End If
            funSetUserAccess = True
        Catch er As Exception
            Throw New Exception(er.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            LoadData(txtDocNo.Value, NavType)
            btnPrint.Enabled = True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select Sale_Return_No as [ReturnNo],Sale_Return_Date as [Return Date],Invoice_No as [Invoice No],Invoice_Date as [Invoice Date],Cust_Code as [Customer Code],Cust_Name as [Customer Name],case when Is_Post='Y' then 'Posted' else 'Pending' end as status from TSPL_SALE_RETURN_HEAD"
        LoadData(clsCommon.ShowSelectForm("SaleRtrnCodeFnd", qry, "ReturnNo", "", txtDocNo.Value, "ReturnNo", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(clicked)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            CloseForm()
        ElseIf e.Control And e.Alt And e.Shift And e.KeyCode = Keys.F11 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnRecreateJournalEntry.Visible = True
            End If
        ElseIf e.Control And e.Alt And e.Shift And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverseAndRecreate.Visible = True
                BntContainerDeposit.Visible = True
            End If
        End If

    End Sub

    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER"
        txtTermCode.Value = clsCommon.ShowSelectForm("POTermCodefnd", qry, "Code", "", txtTermCode.Value, "Code", isButtonClicked)
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

    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        'Dim obj As clsLocation = clsLocation.FinderForPhysicalLoaction(txtLocation.Value, isButtonClicked)
        'If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
        '    txtLocation.Value = obj.Code
        '    lblLocation.Text = obj.Name
        'Else
        '    txtLocation.Value = ""
        '    lblLocation.Text = ""
        'End If



        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical' and Excisable='F' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("VendorMasteidfndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))

    End Sub

    Private Sub txtReqNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtInvoiceNo._MYValidating
        Dim qry As String = "select Sale_Invoice_No as Code ,Sale_Invoice_Date as Date,Cust_Code as [Customer Code],Cust_Name as Customer,Total_Invoice_Amt as [Invoice Amount] from TSPL_SALE_INVOICE_HEAD"
        Dim WhrCls As String = "Shipment_Type='Sale' and Is_Post='Y' and  Sale_Invoice_No not in( select Invoice_No from TSPL_SALE_RETURN_HEAD where Sale_Return_No not in('" + txtDocNo.Value + "') ) "
        txtInvoiceNo.Value = clsCommon.ShowSelectForm("FNDSRFnd", qry, "Code", WhrCls, txtInvoiceNo.Value, "Code", isButtonClicked)
        SelectSaleInvoiceItems()
    End Sub

    Sub SelectSaleInvoiceItems()
        isInsideLoadData = True
        LoadBlankGrid()
        LoadBlankGridTax()
        Dim qry As String = "select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,TSPL_SALE_INVOICE_HEAD.Cust_Account,TSPL_SALE_INVOICE_HEAD.Cust_Code,TSPL_SALE_INVOICE_HEAD.Cust_Name,TSPL_SALE_INVOICE_HEAD.Cust_PONo, TSPL_SALE_INVOICE_HEAD.Tax_Group,TSPL_SALE_INVOICE_HEAD.Location, TSPL_LOCATION_MASTER.Location_Desc as LocationName,Invoice_Type,TSPL_SALE_INVOICE_HEAD.Due_Date,TSPL_SALE_INVOICE_HEAD.Shell_Qty,TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Route_Desc,TSPL_SALE_INVOICE_HEAD.Salesman_Code,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesManName,TSPL_SALE_INVOICE_DETAIL.Empty_Value,TSPL_SALE_INVOICE_DETAIL.TPT,TSPL_SALE_INVOICE_HEAD.Mode_Of_Transport," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_HEAD.Vehicle_Code,TSPL_SALE_INVOICE_HEAD.Shipment_Type ,TSPL_SALE_INVOICE_HEAD.Price_Code as HPrice_Code,TSPL_SALE_INVOICE_HEAD.KM_Reading,TSPL_SALE_INVOICE_HEAD.Scheme_Sample_Code ,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,TSPL_SALE_INVOICE_HEAD.Vehicle_No,TSPL_SALE_INVOICE_HEAD.Total_Assessable_Amount,TSPL_SALE_INVOICE_HEAD.Inv_Discount_Amt,TSPL_SALE_INVOICE_HEAD.Inv_Detail_Disc_Amt,TSPL_SALE_INVOICE_HEAD.Inv_Tax_Amt,TSPL_SALE_INVOICE_HEAD.Freight_Amt, TSPL_SALE_INVOICE_HEAD.Other_Charges,TSPL_SALE_INVOICE_HEAD.Add_Charges,TSPL_SALE_INVOICE_HEAD.Terms_Code, TSPL_SALE_INVOICE_HEAD.TPT as HTPT,TSPL_SALE_INVOICE_HEAD.Empty_Value as HEmpty_Value,TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt,TSPL_SALE_INVOICE_HEAD.Inv_Detail_Total_Amt," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_HEAD.TAX1 as HTAX1,TSPL_SALE_INVOICE_HEAD.TAX1_Rate as HTAX1_Rate,TSPL_SALE_INVOICE_HEAD.TAX1_Assessable_Amt as HTAX1_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX1_Amt as HTAX1_Amt," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_HEAD.TAX2 as HTAX2,TSPL_SALE_INVOICE_HEAD.TAX2_Rate as HTAX2_Rate,TSPL_SALE_INVOICE_HEAD.TAX2_Assessable_Amt as HTAX2_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX2_Amt as HTAX2_Amt," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_HEAD.TAX3 as HTAX3,TSPL_SALE_INVOICE_HEAD.TAX3_Rate as HTAX3_Rate,TSPL_SALE_INVOICE_HEAD.TAX3_Assessable_Amt as HTAX3_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX3_Amt as HTAX3_Amt," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_HEAD.TAX4 as HTAX4,TSPL_SALE_INVOICE_HEAD.TAX4_Rate as HTAX4_Rate,TSPL_SALE_INVOICE_HEAD.TAX4_Assessable_Amt as HTAX4_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX4_Amt as HTAX4_Amt," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_HEAD.TAX5 as HTAX5,TSPL_SALE_INVOICE_HEAD.TAX5_Rate as HTAX5_Rate,TSPL_SALE_INVOICE_HEAD.TAX5_Assessable_Amt as HTAX5_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX5_Amt as HTAX5_Amt," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_HEAD.TAX6 as HTAX6,TSPL_SALE_INVOICE_HEAD.TAX6_Rate as HTAX6_Rate,TSPL_SALE_INVOICE_HEAD.TAX6_Assessable_Amt as HTAX6_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX6_Amt as HTAX6_Amt," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_HEAD.TAX7 as HTAX7,TSPL_SALE_INVOICE_HEAD.TAX7_Rate as HTAX7_Rate,TSPL_SALE_INVOICE_HEAD.TAX7_Assessable_Amt as HTAX7_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX7_Amt as HTAX7_Amt," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_HEAD.TAX8 as HTAX8,TSPL_SALE_INVOICE_HEAD.TAX8_Rate as HTAX8_Rate,TSPL_SALE_INVOICE_HEAD.TAX8_Assessable_Amt as HTAX8_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX8_Amt as HTAX8_Amt," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_HEAD.TAX9 as HTAX9,TSPL_SALE_INVOICE_HEAD.TAX9_Rate as HTAX9_Rate,TSPL_SALE_INVOICE_HEAD.TAX9_Assessable_Amt as HTAX9_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX9_Amt as HTAX9_Amt," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_HEAD.TAX10 as HTAX10,TSPL_SALE_INVOICE_HEAD.TAX10_Rate as HTAX10_Rate,TSPL_SALE_INVOICE_HEAD.TAX10_Assessable_Amt as HTAX10_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX10_Amt as HTAX10_Amt," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_HEAD.Level1_User_code as HLevel1_User_code,TSPL_SALE_INVOICE_HEAD.Level2_User_code as HLevel2_User_code,TSPL_SALE_INVOICE_HEAD.Level3_User_code as HLevel3_User_code,TSPL_SALE_INVOICE_HEAD.Level4_User_code as HLevel4_User_code,TSPL_SALE_INVOICE_HEAD.Level5_User_code as HLevel5_User_code,TSPL_SALE_INVOICE_HEAD.Level1_User_Commission as HLevel1_User_Commission,TSPL_SALE_INVOICE_HEAD.Level2_User_Commission as HLevel2_User_Commission,TSPL_SALE_INVOICE_HEAD.Level3_User_Commission as HLevel3_User_Commission,TSPL_SALE_INVOICE_HEAD.Level4_User_Commission as HLevel4_User_Commission,TSPL_SALE_INVOICE_HEAD.Level5_User_Commission  as HLevel5_User_Commission, "

        qry += " TSPL_SALE_INVOICE_DETAIL.Item_Code as DItem_Code,TSPL_SALE_INVOICE_DETAIL.Item_Desc as DItem_Desc,TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as DInvoice_Qty,TSPL_SALE_INVOICE_DETAIL.Price_Date as DPrice_Date,TSPL_SALE_INVOICE_DETAIL.Unit_code as DUnit_code,TSPL_SALE_INVOICE_DETAIL.Location as DLocation,TSPL_SALE_INVOICE_DETAIL.Price_code as DPrice_code,TSPL_SALE_INVOICE_DETAIL.Scheme_Applicable as DScheme_Applicable,TSPL_SALE_INVOICE_DETAIL.Scheme_Code_Qty as DScheme_Code_Qty,TSPL_SALE_INVOICE_DETAIL.Scheme_Item as DScheme_Item,TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Applicable as DPromo_Scheme_Applicable,TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Code as DPromo_Scheme_Code,TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item as DPromo_Scheme_Item,TSPL_SALE_INVOICE_DETAIL.Scheme_Disc_Applicable as DScheme_Disc_Applicable,TSPL_SALE_INVOICE_DETAIL.Scheme_Code_Cash as DScheme_Code_Cash,TSPL_SALE_INVOICE_DETAIL.Sampling_Item as DSampling_Item,TSPL_SALE_INVOICE_DETAIL.MRP_Amt as DMRP_Amt,TSPL_SALE_INVOICE_DETAIL.Basic_Rate as DBasic_Rate,TSPL_SALE_INVOICE_DETAIL.Item_Assessable_Rate as DItem_Assessable_Rate,TSPL_SALE_INVOICE_DETAIL.Disc_Amt as DDisc_Amt,TSPL_SALE_INVOICE_DETAIL.Item_Net_Amt as DItem_Net_Amt," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_DETAIL.TAX1 as DTAX1,TSPL_SALE_INVOICE_DETAIL.TAX1_Rate as DTAX1_Rate,TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt as DTAX1_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX1_Amt as DTAX1_Amt,TSPL_SALE_INVOICE_DETAIL.TAX2 as DTAX2,TSPL_SALE_INVOICE_DETAIL.TAX2_Rate as DTAX2_Rate,TSPL_SALE_INVOICE_DETAIL.TAX2_Assessable_Amt as DTAX2_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX2_Amt as DTAX2_Amt,TSPL_SALE_INVOICE_DETAIL.Item_Tax as DItem_Tax,TSPL_SALE_INVOICE_DETAIL.Total_Assessable_Amt as DTotal_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
        qry += " TAX3 as DTAX3,TSPL_SALE_INVOICE_DETAIL.TAX3_Rate as DTAX3_Rate,TSPL_SALE_INVOICE_DETAIL.TAX3_Assessable_Amt as DTAX3_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX3_Amt as DTAX3_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
        qry += " TAX4 as DTAX4,TSPL_SALE_INVOICE_DETAIL.TAX4_Rate as DTAX4_Rate,TSPL_SALE_INVOICE_DETAIL.TAX4_Assessable_Amt as DTAX4_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX4_Amt as DTAX4_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
        qry += " TAX5 as DTAX5,TSPL_SALE_INVOICE_DETAIL.TAX5_Rate as DTAX5_Rate,TSPL_SALE_INVOICE_DETAIL.TAX5_Assessable_Amt as DTAX5_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX5_Amt as DTAX5_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
        qry += " TAX6 as DTAX6,TSPL_SALE_INVOICE_DETAIL.TAX6_Rate as DTAX6_Rate,TSPL_SALE_INVOICE_DETAIL.TAX6_Assessable_Amt as DTAX6_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX6_Amt as DTAX6_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
        qry += " TAX7 as DTAX7,TSPL_SALE_INVOICE_DETAIL.TAX7_Rate as DTAX7_Rate,TSPL_SALE_INVOICE_DETAIL.TAX7_Assessable_Amt as DTAX7_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX7_Amt as DTAX7_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
        qry += " TAX8 as DTAX8,TSPL_SALE_INVOICE_DETAIL.TAX8_Rate as DTAX8_Rate,TSPL_SALE_INVOICE_DETAIL.TAX8_Assessable_Amt as DTAX8_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX8_Amt as DTAX8_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
        qry += " TAX9 as DTAX9,TSPL_SALE_INVOICE_DETAIL.TAX9_Rate as DTAX9_Rate,TSPL_SALE_INVOICE_DETAIL.TAX9_Assessable_Amt as DTAX9_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX9_Amt as DTAX9_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
        qry += " TAX10 as DTAX10,TSPL_SALE_INVOICE_DETAIL.TAX10_Rate as DTAX10_Rate,TSPL_SALE_INVOICE_DETAIL.TAX10_Assessable_Amt as DTAX10_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX10_Amt as DTAX10_Amt,TSPL_SALE_INVOICE_DETAIL.Item_Tax as DItem_Tax,TSPL_SALE_INVOICE_DETAIL.Total_Assessable_Amt as DTotal_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
        qry += " Total_MRP_Amt as DTotal_MRP_Amt,TSPL_SALE_INVOICE_DETAIL.Total_Basic_Amt as DTotal_Basic_Amt,TSPL_SALE_INVOICE_DETAIL.Total_Disc_Amt as DTotal_Disc_Amt,TSPL_SALE_INVOICE_DETAIL.Total_net_Amt as DTotal_net_Amt,TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt as DTotal_Tax_Amt,TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt as DTotal_Item_Amt,TSPL_SALE_INVOICE_DETAIL.Empty_Value as DEmpty_Value,TSPL_SALE_INVOICE_DETAIL.TPT as DTPT,TSPL_SALE_INVOICE_DETAIL.Total_TPT as DTotal_TPT,TSPL_SALE_INVOICE_DETAIL.Empty_Value_Shell as DEmpty_Value_Shell,TSPL_SALE_INVOICE_DETAIL.Empty_Value_Bottle as DEmpty_Value_Bottle,TSPL_SALE_INVOICE_DETAIL.Cust_Discount as DCust_Discount,TSPL_SALE_INVOICE_DETAIL.Total_Cust_Discount as DTotal_Cust_Discount,TSPL_SALE_INVOICE_DETAIL.Unit_Cogs as DUnit_cogs " + Environment.NewLine
        '------------------
        qry += ",TSPL_SALE_INVOICE_DETAIL.price_amount1 as DPrice_Amount1,TSPL_SALE_INVOICE_DETAIL.price_amount2 as DPrice_Amount2,TSPL_SALE_INVOICE_DETAIL.price_amount3 as DPrice_Amount3,TSPL_SALE_INVOICE_DETAIL.price_amount4 as DPrice_Amount4,TSPL_SALE_INVOICE_DETAIL.price_amount5 as DPrice_Amount5,TSPL_SALE_INVOICE_DETAIL.price_amount6 as DPrice_Amount6,TSPL_SALE_INVOICE_DETAIL.price_amount7 as DPrice_Amount7,TSPL_SALE_INVOICE_DETAIL.price_amount8 as DPrice_Amount8,TSPL_SALE_INVOICE_DETAIL.price_amount9 as DPrice_Amount9,TSPL_SALE_INVOICE_DETAIL.price_amount10 as DPrice_Amount10,TSPL_SALE_INVOICE_DETAIL.Main_Item as Main_item,TSPL_SALE_INVOICE_DETAIL.Discount_Code as Discount_code " + Environment.NewLine
        '--------------

        qry += ",TSPL_SALE_INVOICE_DETAIL.From_Scheme_Code "
        qry += " from TSPL_SALE_INVOICE_DETAIL" + Environment.NewLine
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SALE_INVOICE_HEAD.Location" + Environment.NewLine
        qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER .EMP_CODE=TSPL_SALE_INVOICE_HEAD.Salesman_Code" + Environment.NewLine
        qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No='" + txtInvoiceNo.Value + "' order by TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            qry = "Select OnHold from TSPL_CUSTOMER_MASTER Where Cust_Code='" + clsCommon.myCstr(dt.Rows(0)("Cust_Code")) + "'"
            Dim CheckForFinalSettlement As String = clsDBFuncationality.getSingleValue(qry)
            If clsCommon.CompairString(CheckForFinalSettlement, "Y") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("The Customer [" + clsCommon.myCstr(dt.Rows(0)("Cust_Code")) + "] attached with this Invoice is found for Full && Final Settlement")
                Exit Sub
            End If
            txtInvoiceDate.Value = clsCommon.myCstr(dt.Rows(0)("Sale_Invoice_Date"))
            txtCustomerNo.Value = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            lblCustomerName.Text = clsCommon.myCstr(dt.Rows(0)("Cust_Name"))
            txtCustomerPONO.Text = clsCommon.myCstr(dt.Rows(0)("Cust_PONo"))
            txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))

            If Not clsLocation.isLocatinExcisable(clsCommon.myCstr(dt.Rows(0)("Location")), Nothing) Then
                txtLocation.Value = clsCommon.myCstr(dt.Rows(0)("Location"))
            End If


            lblLocation.Text = clsCommon.myCstr(dt.Rows(0)("LocationName"))
            lblCustAccount.Text = clsCommon.myCstr(dt.Rows(0)("Cust_Account"))
            txtRouteNo.Value = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            lblRouteNo.Text = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            txtSalesman.Value = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            lblSalesman.Text = clsCommon.myCstr(dt.Rows(0)("SalesManName"))
            txtModeOfTransport.Text = clsCommon.myCstr(dt.Rows(0)("Mode_Of_Transport"))
            txtVehicleNo.Value = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            Shippment_type = clsCommon.myCstr(dt.Rows(0)("Shipment_Type"))
            lblVehicleNo.Text = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))

            txtSchemeSampleCode.Text = clsCommon.myCstr(dt.Rows(0)("Scheme_Sample_Code"))
            txtKMReading.Text = clsCommon.myCstr(dt.Rows(0)("KM_Reading"))
            txtInvoiceDate.Text = clsCommon.myCDate(dt.Rows(0)("Sale_Invoice_Date"))

            lblAssessableAmt.Text = clsCommon.myFormat(dt.Rows(0)("Total_Assessable_Amount"))
            lblDetailDiscountAmt.Text = clsCommon.myFormat(dt.Rows(0)("Inv_Detail_Disc_Amt"))
            lblTaxAmt.Text = clsCommon.myFormat(dt.Rows(0)("Inv_Tax_Amt"))
            lblFreightAmount.Text = clsCommon.myFormat(dt.Rows(0)("Freight_Amt"))
            lblOtherCharges.Text = clsCommon.myFormat(dt.Rows(0)("Other_Charges"))
            lblAdditionalCharges.Text = clsCommon.myFormat(dt.Rows(0)("Add_Charges"))

            lblDetaolTotAmt.Text = clsCommon.myFormat(dt.Rows(0)("Inv_Detail_Total_Amt"))
            lblDiscountAmt.Text = clsCommon.myFormat(dt.Rows(0)("Inv_Discount_Amt"))
            lblTotAmt.Text = clsCommon.myFormat(dt.Rows(0)("Total_Invoice_Amt"))
            lblTPT.Text = clsCommon.myFormat(dt.Rows(0)("HTPT"))
            lblEmptyValue.Text = clsCommon.myFormat(dt.Rows(0)("HEmpty_Value"))
            txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("HPrice_Code"))

            lblLevel1_User_code.Text = clsCommon.myCstr(dt.Rows(0)("HLevel1_User_code"))
            lblLevel2_User_code.Text = clsCommon.myCstr(dt.Rows(0)("HLevel2_User_code"))
            lblLevel3_User_code.Text = clsCommon.myCstr(dt.Rows(0)("HLevel3_User_code"))
            lblLevel4_User_code.Text = clsCommon.myCstr(dt.Rows(0)("HLevel4_User_code"))
            lblLevel5_User_code.Text = clsCommon.myCstr(dt.Rows(0)("HLevel5_User_code"))

            lblLevel1_User_Commission.Text = clsCommon.myFormat(dt.Rows(0)("HLevel1_User_Commission"))
            lblLevel2_User_Commission.Text = clsCommon.myFormat(dt.Rows(0)("HLevel2_User_Commission"))
            lblLevel3_User_Commission.Text = clsCommon.myFormat(dt.Rows(0)("HLevel3_User_Commission"))
            lblLevel4_User_Commission.Text = clsCommon.myFormat(dt.Rows(0)("HLevel4_User_Commission"))
            lblLevel5_User_Commission.Text = clsCommon.myFormat(dt.Rows(0)("HLevel5_User_Commission"))

            lblNetAmt.Text = clsCommon.myFormat(clsCommon.myCdbl(lblEmptyValue.Text) + clsCommon.myCdbl(lblTotAmt.Text))

            If (clsCommon.myLen(dt.Rows(0)("HTAX1")) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX1"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX1")))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX1_Rate"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX1_Assessable_Amt"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX1_Amt"))
            End If
            If (clsCommon.myLen(dt.Rows(0)("HTAX2")) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX2"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX2")))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX2_Rate"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX2_Assessable_Amt"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX2_Amt"))
            End If
            If (clsCommon.myLen(dt.Rows(0)("HTAX3")) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX3"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX3")))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX3_Rate"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX3_Assessable_Amt"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX3_Amt"))
            End If
            If (clsCommon.myLen(dt.Rows(0)("HTAX4")) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX4"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX4")))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX4_Rate"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX4_Assessable_Amt"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX4_Amt"))
            End If
            If (clsCommon.myLen(dt.Rows(0)("HTAX5")) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX5"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX5")))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX5_Rate"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX5_Assessable_Amt"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX5_Amt"))
            End If
            If (clsCommon.myLen(dt.Rows(0)("HTAX6")) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX6"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX6")))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX6_Rate"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX6_Assessable_Amt"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX6_Amt"))
            End If
            If (clsCommon.myLen(dt.Rows(0)("HTAX7")) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX7"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX7")))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX7_Rate"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX7_Assessable_Amt"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX7_Amt"))
            End If
            If (clsCommon.myLen(dt.Rows(0)("HTAX8")) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX8"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX8")))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX8_Rate"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX8_Assessable_Amt"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX8_Amt"))
            End If
            If (clsCommon.myLen(dt.Rows(0)("HTAX9")) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX9"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX9")))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX9_Rate"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX9_Assessable_Amt"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX9_Amt"))
            End If
            If (clsCommon.myLen(dt.Rows(0)("HTAX10")) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX10"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX10")))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX10_Rate"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX10_Assessable_Amt"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX10_Amt"))
            End If


            For Each dr As DataRow In dt.Rows
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("DItem_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("DItem_Desc"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsCommon.myCstr(dr("DInvoice_Qty"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colActualReturnQty).Value = clsCommon.myCstr(dr("DInvoice_Qty"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDate).Value = clsCommon.GetPrintDate(dr("DPrice_Date"), "dd/MM/yyyy")
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("DUnit_code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCode).Value = clsCommon.myCstr(dr("DPrice_code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = clsCommon.myCstr(dr("DScheme_Applicable"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeCodeQty).Value = clsCommon.myCdbl(dr("DScheme_Code_Qty"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeItem).Value = clsCommon.myCstr(dr("DScheme_Item"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPromoSchemeApplicable).Value = clsCommon.myCstr(dr("DPromo_Scheme_Item"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPromoSchemeCode).Value = clsCommon.myCstr(dr("DPromo_Scheme_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeDiscApplicable).Value = clsCommon.myCstr(dr("DPromo_Scheme_Applicable"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeCodeCash).Value = clsCommon.myCstr(dr("DScheme_Code_Cash"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSamplingItem).Value = clsCommon.myCstr(dr("DSampling_Item"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMRPAmt).Value = clsCommon.myCdbl(dr("DMRP_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBasicRate).Value = clsCommon.myCdbl(dr("DBasic_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemAssessableRate).Value = clsCommon.myCdbl(dr("DItem_Assessable_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDiscAmt).Value = clsCommon.myCdbl(dr("DDisc_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemNetAmt).Value = clsCommon.myCdbl(dr("DItem_Net_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax1).Value = clsCommon.myCstr(dr("DTAX1"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = clsCommon.myCdbl(dr("DTAX1_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = clsCommon.myCdbl(dr("DTAX1_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt1).Value = clsCommon.myCdbl(dr("DTAX1_Assessable_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax2).Value = clsCommon.myCstr(dr("DTAX2"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = clsCommon.myCdbl(dr("DTAX2_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = clsCommon.myCdbl(dr("DTAX2_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt2).Value = clsCommon.myCdbl(dr("DTAX2_Assessable_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax3).Value = clsCommon.myCstr(dr("DTAX3"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = clsCommon.myCdbl(dr("DTAX3_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = clsCommon.myCdbl(dr("DTAX3_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt3).Value = clsCommon.myCdbl(dr("DTAX3_Assessable_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax4).Value = clsCommon.myCstr(dr("DTAX4"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = clsCommon.myCdbl(dr("DTAX4_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = clsCommon.myCdbl(dr("DTAX4_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt4).Value = clsCommon.myCdbl(dr("DTAX4_Assessable_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax5).Value = clsCommon.myCstr(dr("DTAX5"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = clsCommon.myCdbl(dr("DTAX5_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = clsCommon.myCdbl(dr("DTAX5_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt5).Value = clsCommon.myCdbl(dr("DTAX5_Assessable_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax6).Value = clsCommon.myCstr(dr("DTAX6"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = clsCommon.myCdbl(dr("DTAX6_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = clsCommon.myCdbl(dr("DTAX6_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt6).Value = clsCommon.myCdbl(dr("DTAX6_Assessable_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax7).Value = clsCommon.myCstr(dr("DTAX7"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = clsCommon.myCdbl(dr("DTAX7_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = clsCommon.myCdbl(dr("DTAX7_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt7).Value = clsCommon.myCdbl(dr("DTAX7_Assessable_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax8).Value = clsCommon.myCstr(dr("DTAX8"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = clsCommon.myCdbl(dr("DTAX8_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = clsCommon.myCdbl(dr("DTAX8_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt8).Value = clsCommon.myCdbl(dr("DTAX8_Assessable_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax9).Value = clsCommon.myCstr(dr("DTAX9"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = clsCommon.myCdbl(dr("DTAX9_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = clsCommon.myCdbl(dr("DTAX9_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt9).Value = clsCommon.myCdbl(dr("DTAX9_Assessable_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax10).Value = clsCommon.myCstr(dr("DTAX10"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = clsCommon.myCdbl(dr("DTAX10_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = clsCommon.myCdbl(dr("DTAX10_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt10).Value = clsCommon.myCdbl(dr("DTAX10_Assessable_Amt"))

                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTax).Value = clsCommon.myCdbl(dr("DTAX1_Amt")) + clsCommon.myCdbl(dr("DTAX2_Amt")) + clsCommon.myCdbl(dr("DTAX3_Amt")) + clsCommon.myCdbl(dr("DTAX4_Amt")) + clsCommon.myCdbl(dr("DTAX5_Amt")) + clsCommon.myCdbl(dr("DTAX6_Amt")) + clsCommon.myCdbl(dr("DTAX7_Amt")) + clsCommon.myCdbl(dr("DTAX8_Amt")) + clsCommon.myCdbl(dr("DTAX9_Amt")) + clsCommon.myCdbl(dr("DTAX10_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTax).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTax).Value), 4, MidpointRounding.ToEven)

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssessableAmt).Value = 0 '' clsCommon.myCdbl(dr("DTotal_Assessable_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalMRPAmt).Value = clsCommon.myCdbl(dr("DTotal_MRP_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalBasicAmt).Value = clsCommon.myCdbl(dr("DTotal_Basic_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalDiscAmt).Value = clsCommon.myCdbl(dr("DTotal_Disc_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalnetAmt).Value = clsCommon.myCdbl(dr("DTotal_net_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalTaxAmt).Value = clsCommon.myCdbl(dr("DTotal_Tax_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalItemAmt).Value = clsCommon.myCdbl(dr("DTotal_Item_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colEmptyValue).Value = clsCommon.myCdbl(dr("DEmpty_Value"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTPT).Value = clsCommon.myCdbl(dr("DTPT"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalTPT).Value = clsCommon.myCdbl(dr("DTotal_TPT"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colEmptyValueShell).Value = clsCommon.myCdbl(dr("DEmpty_Value_Shell"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colEmptyValueBottle).Value = clsCommon.myCdbl(dr("DEmpty_Value_Bottle"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCustDiscount).Value = clsCommon.myCdbl(dr("DCust_Discount"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalCustDiscount).Value = clsCommon.myCdbl(dr("DTotal_Cust_Discount"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCogs).Value = clsCommon.myCdbl(dr("DUnit_cogs"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount1).Value = clsCommon.myCdbl(dr("DPrice_Amount1"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount2).Value = clsCommon.myCdbl(dr("DPrice_Amount2"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount3).Value = clsCommon.myCdbl(dr("DPrice_Amount3"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount4).Value = clsCommon.myCdbl(dr("DPrice_Amount4"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount5).Value = clsCommon.myCdbl(dr("DPrice_Amount5"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount6).Value = clsCommon.myCdbl(dr("DPrice_Amount6"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount7).Value = clsCommon.myCdbl(dr("DPrice_Amount7"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount8).Value = clsCommon.myCdbl(dr("DPrice_Amount8"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount9).Value = clsCommon.myCdbl(dr("DPrice_Amount9"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount10).Value = clsCommon.myCdbl(dr("DPrice_Amount10"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colmainItem).Value = clsCommon.myCstr(dr("Main_item"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(coldiscountcode).Value = clsCommon.myCstr(dr("Discount_code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(ColFromSchemeCode).Value = clsCommon.myCstr(dr("From_Scheme_Code"))
            Next

            For ii As Integer = 0 To gv1.Rows.Count - 1
                gv1.Rows(ii).Cells(colActualReturnQty).Value = 0
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()
        End If
        isInsideLoadData = False



    End Sub

    Private Sub RadLabel31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblLevel1_User_code.Click

    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colActualReturnQty) OrElse e.Column Is gv1.Columns(colLeakQty) OrElse e.Column Is gv1.Columns(colBurstQty) OrElse e.Column Is gv1.Columns(colShortQty) Then
                        Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPendingQty).Value)
                        Dim dblEnteredQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colActualReturnQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colShortQty).Value)
                        If dblEnteredQty > dblPendingQty Then
                            common.clsCommon.MyMessageBoxShow("Entered Quantity Can't be more than Pending Quantity." + Environment.NewLine + "Entered Quantity : " + clsCommon.myCstr(dblEnteredQty) + ". Pending Quantity : " + clsCommon.myCstr(dblPendingQty))
                            gv1.CurrentRow.Cells(e.Column.Index).Value = 0
                        End If
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                        UpdateAllTotals()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        UpdateCurrentRow(IntRowNo, False)
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer, ByVal isFullyRetun As Boolean)
        Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colActualReturnQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colShortQty).Value)
        'Dim dblReturnAndLeakQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colActualReturnQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colLeakQty).Value)
        gv1.Rows(IntRowNo).Cells(colReturnQty).Value = Math.Round(dblQty, 2)
        Dim dblDiscountPerUnit As Double = IIf(isFullyRetun, 0, clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDiscAmt).Value))
        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colSchemeItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colPromoSchemeApplicable).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colSamplingItem).Value), "N") = CompairStringResult.Equal Then
            Dim dblTotalTaxAmt As Double = 0
            For ii As Integer = 1 To 10
                Dim strII As String = clsCommon.myCstr(ii)
                If clsCommon.myLen(gv1.Rows(IntRowNo).Cells("COLTAX" + strII).Value) > 0 Then
                    If clsTaxMaster.IsTaxExcisable(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells("COLTAX" + strII).Value), Nothing) Then
                        gv1.Rows(IntRowNo).Cells("COLTOTTAXAMT" + strII).Value = Math.Round((clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells("COLTAXAMT" + strII).Value)) * dblQty, 2)
                    Else
                        Dim dblTaxAmt As Double = Math.Round(dblDiscountPerUnit * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells("colTaxRate" + strII).Value) / 100, 5)
                        gv1.Rows(IntRowNo).Cells("COLTOTTAXAMT" + strII).Value = Math.Round((clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells("COLTAXAMT" + strII).Value) + dblTaxAmt) * dblQty, 2)
                    End If
                Else
                    gv1.Rows(IntRowNo).Cells("COLTOTTAXAMT" + strII).Value = 0
                End If
                dblTotalTaxAmt += clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells("COLTOTTAXAMT" + strII).Value)
            Next


            gv1.Rows(IntRowNo).Cells(colTotalTaxAmt).Value = Math.Round(dblTotalTaxAmt, 2) ''Math.Round((clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colItemTax).Value)) * dblQty, 2) ''+ IIf(isFullyRetun, 0, dblDiscountPerUnit)
            gv1.Rows(IntRowNo).Cells(colTotalMRPAmt).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMRPAmt).Value) * dblQty, 2)
            gv1.Rows(IntRowNo).Cells(colTotalBasicAmt).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colBasicRate).Value) * dblQty, 2)
            gv1.Rows(IntRowNo).Cells(colTotalAssessableAmt).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colItemAssessableRate).Value) * dblQty, 2)

            gv1.Rows(IntRowNo).Cells(colTotalDiscAmt).Value = IIf(isFullyRetun, Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDiscAmt).Value) * dblQty, 2), 0)
            gv1.Rows(IntRowNo).Cells(colTotalnetAmt).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colTotalBasicAmt).Value) - clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colTotalDiscAmt).Value), 2)

        Else
            For ii As Integer = 1 To 10
                gv1.Rows(IntRowNo).Cells("COLTOTTAXAMT" + clsCommon.myCstr(ii)).Value = 0
            Next
            gv1.Rows(IntRowNo).Cells(colTotalTaxAmt).Value = 0
            gv1.Rows(IntRowNo).Cells(colTotalMRPAmt).Value = 0
            gv1.Rows(IntRowNo).Cells(colTotalBasicAmt).Value = 0
            gv1.Rows(IntRowNo).Cells(colTotalDiscAmt).Value = 0
            gv1.Rows(IntRowNo).Cells(colTotalnetAmt).Value = 0
            gv1.Rows(IntRowNo).Cells(colEmptyValue).Value = 0
            gv1.Rows(IntRowNo).Cells(colTotalAssessableAmt).Value = 0
            'gv1.Rows(IntRowNo).Cells(colTotalTPT).Value = 0
            'gv1.Rows(IntRowNo).Cells(colTotalItemAmt).Value = 0
        End If
        gv1.Rows(IntRowNo).Cells(colEmptyValue).Value = Math.Round((clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colEmptyValueBottle).Value) + (clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colEmptyValueShell).Value))) * dblQty, 2)
        gv1.Rows(IntRowNo).Cells(colTotalTPT).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colTPT).Value) * dblQty, 2)
        gv1.Rows(IntRowNo).Cells(colTotalItemAmt).Value = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colTotalTaxAmt).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colTotalnetAmt).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colTotalTPT).Value)

    End Sub
    Private Sub UpdateAllTotals()
        UpdateAllTotals(False)
    End Sub

    Private Sub UpdateAllTotals(ByVal isFullyRetun As Boolean)
        Dim dblTotDisAmt As Double = 0
        Dim dblDetailAmt As Double = 0
        Dim dblTaxAmt As Double = 0
        Dim dblTPT As Double = 0
        Dim dblContainerDeposit As Double = 0
        Dim dblReturnAmt As Double = 0
        Dim dblNetAmt As Double = 0

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

        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemeItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colPromoSchemeApplicable).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colSamplingItem).Value), "N") = CompairStringResult.Equal Then

                    Dim dblDiscountPerUnit As Double = IIf(isFullyRetun, 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colDiscAmt).Value))


                    Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colActualReturnQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colShortQty).Value)
                    dblTaxAmt1 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt1).Value), 4, MidpointRounding.ToEven)
                    dblTaxAmt2 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt2).Value), 4, MidpointRounding.ToEven)
                    dblTaxAmt3 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt3).Value), 4, MidpointRounding.ToEven)
                    dblTaxAmt4 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt4).Value), 4, MidpointRounding.ToEven)
                    dblTaxAmt5 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt5).Value), 4, MidpointRounding.ToEven)
                    dblTaxAmt6 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt6).Value), 4, MidpointRounding.ToEven)
                    dblTaxAmt7 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt7).Value), 4, MidpointRounding.ToEven)
                    dblTaxAmt8 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt8).Value), 4, MidpointRounding.ToEven)
                    dblTaxAmt9 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt9).Value), 4, MidpointRounding.ToEven)
                    dblTaxAmt10 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt10).Value), 4, MidpointRounding.ToEven)


                    'dblTaxBaseAmt2 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt2).Value) * dblQty, 2, MidpointRounding.ToEven)
                    'dblTaxBaseAmt3 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt3).Value) * dblQty, 2, MidpointRounding.ToEven)
                    'dblTaxBaseAmt4 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt4).Value) * dblQty, 2, MidpointRounding.ToEven)
                    'dblTaxBaseAmt5 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt5).Value) * dblQty, 2, MidpointRounding.ToEven)
                    'dblTaxBaseAmt6 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt6).Value) * dblQty, 2, MidpointRounding.ToEven)
                    'dblTaxBaseAmt7 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt7).Value) * dblQty, 2, MidpointRounding.ToEven)
                    'dblTaxBaseAmt8 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt8).Value) * dblQty, 2, MidpointRounding.ToEven)
                    'dblTaxBaseAmt9 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt9).Value) * dblQty, 2, MidpointRounding.ToEven)
                    'dblTaxBaseAmt10 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt10).Value) * dblQty, 2, MidpointRounding.ToEven)

                    If clsCommon.myLen(gv1.Rows(ii).Cells(colTax1).Value) > 0 Then
                        If clsTaxMaster.IsTaxExcisable(clsCommon.myCstr(gv1.Rows(ii).Cells(colTax1).Value), Nothing) Then
                            dblTaxBaseAmt1 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt1).Value) * dblQty, 2, MidpointRounding.ToEven)
                        Else
                            dblTaxBaseAmt1 += Math.Round((clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt1).Value) + dblDiscountPerUnit) * dblQty, 2, MidpointRounding.ToEven)
                        End If
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colTax2).Value) > 0 Then
                        If clsTaxMaster.IsTaxExcisable(clsCommon.myCstr(gv1.Rows(ii).Cells(colTax2).Value), Nothing) Then
                            dblTaxBaseAmt2 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt2).Value) * dblQty, 2, MidpointRounding.ToEven)
                        Else
                            dblTaxBaseAmt2 += Math.Round((clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt2).Value) + dblDiscountPerUnit) * dblQty, 2, MidpointRounding.ToEven)
                        End If
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colTax3).Value) > 0 Then
                        If clsTaxMaster.IsTaxExcisable(clsCommon.myCstr(gv1.Rows(ii).Cells(colTax3).Value), Nothing) Then
                            dblTaxBaseAmt3 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt3).Value) * dblQty, 3, MidpointRounding.ToEven)
                        Else
                            dblTaxBaseAmt3 += Math.Round((clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt3).Value) + dblDiscountPerUnit) * dblQty, 3, MidpointRounding.ToEven)
                        End If
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colTax4).Value) > 0 Then
                        If clsTaxMaster.IsTaxExcisable(clsCommon.myCstr(gv1.Rows(ii).Cells(colTax4).Value), Nothing) Then
                            dblTaxBaseAmt4 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt4).Value) * dblQty, 4, MidpointRounding.ToEven)
                        Else
                            dblTaxBaseAmt4 += Math.Round((clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt4).Value) + dblDiscountPerUnit) * dblQty, 4, MidpointRounding.ToEven)
                        End If
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colTax5).Value) > 0 Then
                        If clsTaxMaster.IsTaxExcisable(clsCommon.myCstr(gv1.Rows(ii).Cells(colTax5).Value), Nothing) Then
                            dblTaxBaseAmt5 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt5).Value) * dblQty, 5, MidpointRounding.ToEven)
                        Else
                            dblTaxBaseAmt5 += Math.Round((clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt5).Value) + dblDiscountPerUnit) * dblQty, 5, MidpointRounding.ToEven)
                        End If
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colTax6).Value) > 0 Then
                        If clsTaxMaster.IsTaxExcisable(clsCommon.myCstr(gv1.Rows(ii).Cells(colTax6).Value), Nothing) Then
                            dblTaxBaseAmt6 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt6).Value) * dblQty, 6, MidpointRounding.ToEven)
                        Else
                            dblTaxBaseAmt6 += Math.Round((clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt6).Value) + dblDiscountPerUnit) * dblQty, 6, MidpointRounding.ToEven)
                        End If
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colTax7).Value) > 0 Then
                        If clsTaxMaster.IsTaxExcisable(clsCommon.myCstr(gv1.Rows(ii).Cells(colTax7).Value), Nothing) Then
                            dblTaxBaseAmt7 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt7).Value) * dblQty, 7, MidpointRounding.ToEven)
                        Else
                            dblTaxBaseAmt7 += Math.Round((clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt7).Value) + dblDiscountPerUnit) * dblQty, 7, MidpointRounding.ToEven)
                        End If
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colTax8).Value) > 0 Then
                        If clsTaxMaster.IsTaxExcisable(clsCommon.myCstr(gv1.Rows(ii).Cells(colTax8).Value), Nothing) Then
                            dblTaxBaseAmt8 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt8).Value) * dblQty, 8, MidpointRounding.ToEven)
                        Else
                            dblTaxBaseAmt8 += Math.Round((clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt8).Value) + dblDiscountPerUnit) * dblQty, 8, MidpointRounding.ToEven)
                        End If
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colTax9).Value) > 0 Then
                        If clsTaxMaster.IsTaxExcisable(clsCommon.myCstr(gv1.Rows(ii).Cells(colTax9).Value), Nothing) Then
                            dblTaxBaseAmt9 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt9).Value) * dblQty, 9, MidpointRounding.ToEven)
                        Else
                            dblTaxBaseAmt9 += Math.Round((clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt9).Value) + dblDiscountPerUnit) * dblQty, 9, MidpointRounding.ToEven)
                        End If
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colTax10).Value) > 0 Then
                        If clsTaxMaster.IsTaxExcisable(clsCommon.myCstr(gv1.Rows(ii).Cells(colTax10).Value), Nothing) Then
                            dblTaxBaseAmt10 += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt10).Value) * dblQty, 10, MidpointRounding.ToEven)
                        Else
                            dblTaxBaseAmt10 += Math.Round((clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAssessableAmt10).Value) + dblDiscountPerUnit) * dblQty, 10, MidpointRounding.ToEven)
                        End If
                    End If
                    dblTotDisAmt += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalDiscAmt).Value), 2, MidpointRounding.ToEven)
                    dblDetailAmt += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalnetAmt).Value), 2, MidpointRounding.ToEven)
                    dblTPT += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalTPT).Value), 2, MidpointRounding.ToEven)
                End If
                dblContainerDeposit += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colEmptyValue).Value), 2, MidpointRounding.ToEven)
                dblReturnAmt += Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalItemAmt).Value), 2, MidpointRounding.ToEven)

            End If
        Next





        dblTaxBaseAmt1 = Math.Round(dblTaxBaseAmt1, 2, MidpointRounding.ToEven)
        dblTaxBaseAmt2 = Math.Round(dblTaxBaseAmt2, 2, MidpointRounding.ToEven)
        dblTaxBaseAmt3 = Math.Round(dblTaxBaseAmt3, 2, MidpointRounding.ToEven)
        dblTaxBaseAmt4 = Math.Round(dblTaxBaseAmt4, 2, MidpointRounding.ToEven)
        dblTaxBaseAmt5 = Math.Round(dblTaxBaseAmt5, 2, MidpointRounding.ToEven)
        dblTaxBaseAmt6 = Math.Round(dblTaxBaseAmt6, 2, MidpointRounding.ToEven)
        dblTaxBaseAmt7 = Math.Round(dblTaxBaseAmt7, 2, MidpointRounding.ToEven)
        dblTaxBaseAmt8 = Math.Round(dblTaxBaseAmt8, 2, MidpointRounding.ToEven)
        dblTaxBaseAmt9 = Math.Round(dblTaxBaseAmt9, 2, MidpointRounding.ToEven)
        dblTaxBaseAmt10 = Math.Round(dblTaxBaseAmt10, 2, MidpointRounding.ToEven)

        dblTaxAmt1 = Math.Round(dblTaxAmt1, 2, MidpointRounding.ToEven)
        dblTaxAmt2 = Math.Round(dblTaxAmt2, 2, MidpointRounding.ToEven)
        dblTaxAmt3 = Math.Round(dblTaxAmt3, 2, MidpointRounding.ToEven)
        dblTaxAmt4 = Math.Round(dblTaxAmt4, 2, MidpointRounding.ToEven)
        dblTaxAmt5 = Math.Round(dblTaxAmt5, 2, MidpointRounding.ToEven)
        dblTaxAmt6 = Math.Round(dblTaxAmt6, 2, MidpointRounding.ToEven)
        dblTaxAmt7 = Math.Round(dblTaxAmt7, 2, MidpointRounding.ToEven)
        dblTaxAmt8 = Math.Round(dblTaxAmt8, 2, MidpointRounding.ToEven)
        dblTaxAmt9 = Math.Round(dblTaxAmt9, 2, MidpointRounding.ToEven)
        dblTaxAmt10 = Math.Round(dblTaxAmt10, 2, MidpointRounding.ToEven)


        For ii As Integer = 1 To gv2.Rows.Count
            Select Case (ii)
                Case 1
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = dblTaxAmt1
                    gv2.Rows(ii - 1).Cells(colTAssessAmt).Value = dblTaxBaseAmt1
                    If dblTaxBaseAmt1 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 2, MidpointRounding.ToEven)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 2
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = dblTaxAmt2
                    gv2.Rows(ii - 1).Cells(colTAssessAmt).Value = dblTaxBaseAmt2
                    If dblTaxBaseAmt2 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 2, MidpointRounding.ToEven)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 3
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = dblTaxAmt3
                    gv2.Rows(ii - 1).Cells(colTAssessAmt).Value = dblTaxBaseAmt3
                    If dblTaxBaseAmt3 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 2, MidpointRounding.ToEven)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 4
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = dblTaxAmt4
                    gv2.Rows(ii - 1).Cells(colTAssessAmt).Value = dblTaxBaseAmt4
                    If dblTaxBaseAmt4 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 2, MidpointRounding.ToEven)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 5
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = dblTaxAmt5
                    gv2.Rows(ii - 1).Cells(colTAssessAmt).Value = dblTaxBaseAmt5
                    If dblTaxBaseAmt5 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt5 * 100) / dblTaxBaseAmt5, 2, MidpointRounding.ToEven)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 6
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = dblTaxAmt6
                    gv2.Rows(ii - 1).Cells(colTAssessAmt).Value = dblTaxBaseAmt6
                    If dblTaxBaseAmt6 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt6 * 100) / dblTaxBaseAmt6, 2, MidpointRounding.ToEven)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 7
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = dblTaxAmt7
                    gv2.Rows(ii - 1).Cells(colTAssessAmt).Value = dblTaxBaseAmt7
                    If dblTaxBaseAmt7 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt7 * 100) / dblTaxBaseAmt7, 2, MidpointRounding.ToEven)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 8
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = dblTaxAmt8
                    gv2.Rows(ii - 1).Cells(colTAssessAmt).Value = dblTaxBaseAmt8
                    If dblTaxBaseAmt8 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt8 * 100) / dblTaxBaseAmt8, 2, MidpointRounding.ToEven)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 9
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = dblTaxAmt9
                    gv2.Rows(ii - 1).Cells(colTAssessAmt).Value = dblTaxBaseAmt9
                    If dblTaxBaseAmt9 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt9 * 100) / dblTaxBaseAmt9, 2, MidpointRounding.ToEven)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 10
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = dblTaxAmt10
                    gv2.Rows(ii - 1).Cells(colTAssessAmt).Value = dblTaxBaseAmt10
                    If dblTaxBaseAmt10 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt10 * 100) / dblTaxBaseAmt10, 2, MidpointRounding.ToEven)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
            End Select
        Next
        dblTaxAmt = dblTaxAmt1 + dblTaxAmt2 + dblTaxAmt3 + dblTaxAmt4 + dblTaxAmt5 + dblTaxAmt6 + dblTaxAmt7 + dblTaxAmt8 + dblTaxAmt9 + dblTaxAmt10
        lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt)
        lblDetaolTotAmt.Text = clsCommon.myFormat(dblDetailAmt)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxAmt)
        lblTPT.Text = clsCommon.myFormat(dblTPT)
        dblContainerDeposit += txtshellqty.Value * 100
        lblEmptyValue.Text = clsCommon.myFormat(dblContainerDeposit)
        lblTotAmt.Text = clsCommon.myFormat(dblDetailAmt + dblTPT + dblTaxAmt) ''clsCommon.myFormat(dblReturnAmt)

        lblNetAmt.Text = clsCommon.myFormat(clsCommon.myCdbl(lblEmptyValue.Text) + clsCommon.myCdbl(lblTotAmt.Text))
    End Sub


    '----------------- Done BY Abhishek KUmar 1 Nov 2012 1:11 Pm -----------------

    Private Sub btnPrint_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim IsLocation As Boolean
        Dim Excise As String
        Dim qry As String = "select Excisable   from tspl_location_master where Location_Code = ( select Location  from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No ='" & txtInvoiceNo.Value & "' )"
        Excise = clsDBFuncationality.getSingleValue(qry)
        If Excise = "T" Then
            IsLocation = True
        Else
            IsLocation = False
        End If

        funPrintReport(txtDocNo.Value, IsLocation)
    End Sub

    Public Sub funPrintReport(ByVal strDocNo As String, ByVal isLocationExcisable As Boolean)
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Document No not found to print")
            End If

            Dim whereclause As String
            whereclause = " where TSPL_SALE_RETURN_HEAD.Sale_Return_No= '" + strDocNo + "' and isnull( TSPL_SALE_RETURN_DETAIL.Return_Qty,0)>0"
            Dim strWithoutShall As String = ",'' as totalqty,"
            Dim str As String = ",'' as totalqty,"
            Dim qry As String = ""
            Dim dt As DataTable
            If isLocationExcisable Then
                qry = "select SUM(Qty) as Qty,MAX(Unit_Code) as Unit from("
                qry += " select Balance_Qty/(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_RETURN_DETAIL.Item_Code and UOM_Code=TSPL_SALE_RETURN_DETAIL.Unit_code ) as Qty,(select UOM_Code  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_RETURN_DETAIL.Item_Code and Conversion_Factor=1)as Unit_Code  from TSPL_SALE_RETURN_DETAIL where Sale_Return_No = '" + strDocNo + "' "
                qry += " ) xxx"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    str = ",'" + clsCommon.myCstr(dt.Rows(0)("Unit")) + " - " + clsCommon.myFormat(dt.Rows(0)("Qty")) + " ' as totalqty,"
                End If
            Else
                qry = "select  Unit_code as Unit,sum(Balance_qty) as Qty from TSPL_SALE_RETURN_DETAIL where Sale_Return_No= '" + strDocNo + "' group by Unit_code"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    str = ",'"
                    For Each dr As DataRow In dt.Rows
                        str = str + clsCommon.myCstr(dr("Unit")) + " - " + clsCommon.myCstr(dr("Qty")) + " "
                    Next
                    strWithoutShall = str + "'" + " as totalqty,"
                    'str = str + " SH - " + clsCommon.myCstr(dt.Rows(0)("SH"))
                    str = str + "'" + " as totalqty,"
                End If
            End If


            Dim qryForGettingTax As String = "select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code"
            Dim strIsFOCItem As String = "(CASE WHEN TSPL_SALE_RETURN_DETAIL.Scheme_Item='Y' or TSPL_SALE_RETURN_DETAIL.Promo_Scheme_Item='Y' or TSPL_SALE_RETURN_DETAIL.Sampling_Item='Y' THEN 'Y' ELSE 'N' END) as FOCItem "
            Dim strInvoiceType As String = "(Case when  CONVERT(varchar(12),(select Excisable   from tspl_location_master where Location_Code = ( select Location  from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No =TSPL_SALE_RETURN_HEAD.Invoice_No )))=convert(varchar(12),'Y') then 'Excise Sale Return ' else   'Non-Excise Sale Return' end) as Invoice_Type"
            Dim strQuery As String = ""
            Dim qryForShipToAdd As String = "(TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3  )"

            If isLocationExcisable Then

                strQuery = " SELECT '' as Cust_PONo,TSPL_CHAPTER_HEAD.Description as ChapterName," + qryForShipToAdd + " AS Address, " & _
                        "  TSPL_SALE_RETURN_HEAD.Invoice_No  as Sale_Invoice_No,  convert(varchar(10),TSPL_SALE_RETURN_HEAD.Invoice_Date ,103) as Sale_Invoice_Date, TSPL_SALE_RETURN_HEAD.Cust_Name, " & _
                        "  (" + qryForGettingTax + "=TSPL_SALE_RETURN_HEAD.TAX1) as TAX1, TSPL_SALE_RETURN_HEAD.TAX1_Rate, TSPL_SALE_RETURN_HEAD.TAX1_Assessable_Amt, " & _
                         " TSPL_SALE_RETURN_HEAD.TAX1_Amt,(" + qryForGettingTax + " = TSPL_SALE_RETURN_HEAD.TAX2) as TAX2 , TSPL_SALE_RETURN_HEAD.TAX2_Rate, " & _
                         " TSPL_SALE_RETURN_HEAD.TAX2_Assessable_Amt, TSPL_SALE_RETURN_HEAD.TAX2_Amt  as TAX2_Amt, (" + qryForGettingTax + " =TSPL_SALE_RETURN_HEAD.TAX3) as TAX3, " & _
                         " TSPL_SALE_RETURN_HEAD.TAX3_Rate, TSPL_SALE_RETURN_HEAD.TAX3_Assessable_Amt, TSPL_SALE_RETURN_HEAD.TAX3_Amt  as TAX3_Amt, " & _
                         " (" + qryForGettingTax + " =TSPL_SALE_RETURN_HEAD.TAX4) as TAX4, TSPL_SALE_Return_HEAD.TAX4_Rate, TSPL_SALE_RETURN_HEAD.TAX4_Assessable_Amt, " & _
                         "  TSPL_SALE_RETURN_HEAD.TAX4_Amt  as TAX4_Amt ,isnull((" + qryForGettingTax + " = TSPL_SALE_RETURN_HEAD.TAX5),'') as TAX5, TSPL_SALE_RETURN_HEAD.TAX5_Rate, " & _
                         " TSPL_SALE_RETURN_HEAD.TAX5_Assessable_Amt, TSPL_SALE_RETURN_HEAD.TAX5_Amt  as TAX5_Amt ,TSPL_SALE_RETURN_HEAD.Total_Invoice_Amt  as Total_Invoice_Amt , TSPL_SALE_RETURN_DETAIL.Balance_Qty as Invoice_Qty, " & _
                         " TSPL_SALE_RETURN_DETAIL.Item_Code, TSPL_SALE_RETURN_DETAIL.Item_Desc as Item_Desc, TSPL_SALE_RETURN_DETAIL.MRP_Amt, " & _
                         "  TSPL_SALE_RETURN_DETAIL.Basic_Rate  as Basic_Rate , TSPL_SALE_RETURN_DETAIL.Item_Assessable_Rate, TSPL_SALE_RETURN_DETAIL.Item_Net_Amt, " & _
                         " TSPL_SALE_RETURN_DETAIL.TAX1 AS 'DTax1', TSPL_SALE_RETURN_DETAIL.TAX1_Rate AS 'DTax1Rate', " & _
                         " TSPL_SALE_RETURN_DETAIL.TAX1_Assessable_Amt AS 'DTax1Ass'," & _
                         " TSPL_SALE_RETURN_DETAIL.TAX1_Amt AS Dtax1Amt, " & _
                         " TSPL_SALE_RETURN_DETAIL.Total_Assessable_Amt, TSPL_SALE_RETURN_DETAIL.Total_MRP_Amt, TSPL_SALE_RETURN_DETAIL.Total_Basic_Amt  as Total_Basic_Amt , " & _
                         " TSPL_SALE_RETURN_DETAIL.Total_net_Amt, TSPL_SALE_RETURN_DETAIL.Total_Tax_Amt, TSPL_SALE_RETURN_DETAIL.Total_Item_Amt, " & _
                         " TSPL_SALE_RETURN_HEAD.Empty_Value,TSPL_SALE_RETURN_DETAIL.Total_TPT,TSPL_SALE_RETURN_HEAD.TPT as 'ttlTPT', " & _
                         " TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 " & _
                         " " + str + "TSPL_CUSTOMER_MASTER.Tin_No,TSPL_SALE_RETURN_HEAD.Comments  ,TSPL_SALE_RETURN_HEAD.Description," + strInvoiceType + ",(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_RETURN_DETAIL.Item_Code and UOM_Code = (case when TSPL_SALE_RETURN_DETAIL.Unit_code='FC' then 'FB' else  case when TSPL_SALE_RETURN_DETAIL.Unit_code='FB' then 'FC' else '' end end) ) as [Conversion] ,(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_RETURN_DETAIL.Item_Code and UOM_Code=TSPL_SALE_RETURN_DETAIL.Unit_code ) as OrgConversionFactor,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesmanName," + strIsFOCItem + "" & _
                         ",  TSPL_SALE_RETURN_HEAD.Inv_Discount_Amt as Inv_Discount_Amt,TSPL_SALE_RETURN_HEAD.TAX1 as Tax1Code,TSPL_SALE_RETURN_HEAD.TAX2 as Tax2Code,TSPL_SALE_RETURN_HEAD.TAX3 as Tax3Code,TSPL_SALE_RETURN_HEAD.TAX4 as Tax4Code,TSPL_SALE_RETURN_HEAD.TAX5 as Tax5Code ,(select [USER_NAME] from TSPL_USER_MASTER where TSPL_USER_MASTER.User_Code=TSPL_SALE_RETURN_HEAD.Created_By) as CreateByName" & _
                         ",(case when TSPL_SALE_RETURN_HEAD.Is_Post='Y' then (select [USER_NAME] from TSPL_USER_MASTER where User_Code=TSPL_SALE_RETURN_HEAD.Modify_By) else '' end) as PostByName,TSPL_SALE_RETURN_HEAD.Salesman_Code,TSPL_SALE_RETURN_HEAD.Route_No,TSPL_SALE_RETURN_HEAD.Route_Desc, '' as TransferNo," & _
                         "'' as CustomerInvNo ,'' as VerifyByName,TSPL_SALE_RETURN_DETAIL.Leak_Qty,TSPL_SALE_RETURN_DETAIL.Burst_Qty,TSPL_SALE_RETURN_DETAIL.Short_Qty,TSPL_SALE_RETURN_DETAIL.Actual_Return_Qty ,TSPL_SALE_RETURN_HEad.Sale_Return_No, convert(varchar(10),TSPL_SALE_RETURN_HEad.Sale_Return_Date ,103) as Sale_Return_Date FROM  TSPL_CUSTOMER_MASTER INNER JOIN " & _
                         " TSPL_SALE_RETURN_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_RETURN_HEAD.Cust_Code INNER JOIN " & _
                         " TSPL_SALE_RETURN_DETAIL ON TSPL_SALE_RETURN_HEAD.Sale_Return_No  = TSPL_SALE_RETURN_DETAIL.Sale_Return_No  left outer join TSPL_ITEM_MASTER on TSPL_SALE_RETURN_DETAIL.Item_Code =TSPL_ITEM_MASTER .Item_Code left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code=TSPL_ITEM_MASTER.Cheapter_Heads " & _
                         " left outer join TSPL_COMPANY_MASTER on TSPL_SALE_RETURN_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_SALE_RETURN_HEAD.Salesman_Code left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code=TSPL_SALE_RETURN_HEAD.Cust_Code and TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SALE_RETURN_HEAD.Location "
                strQuery = strQuery & whereclause

                strQuery += " order by TSPL_ITEM_MASTER.Sku_Seq"
                Dim dtBasic As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                Dim dtAfterModify As DataTable = dtBasic.Clone()


                Dim drAfterModify As DataRow
                For Each drBasic As DataRow In dtBasic.Rows
                    drAfterModify = dtAfterModify.NewRow()
                    For ColNo As Integer = 0 To dtBasic.Columns.Count - 1
                        drAfterModify(ColNo) = drBasic(ColNo)
                    Next
                    Dim dblConvFactor As Double = clsCommon.myCdbl(drBasic("OrgConversionFactor"))
                    If dblConvFactor <> 1 Then
                        drAfterModify("Invoice_Qty") = Math.Round(clsCommon.myCdbl(drBasic("Invoice_Qty")) / dblConvFactor, 2, MidpointRounding.ToEven)
                        Dim isFound As Boolean = False
                        For Each drBasicTemp As DataRow In dtBasic.Rows
                            If clsCommon.CompairString(clsCommon.myCstr(drBasicTemp("Item_Code")), clsCommon.myCstr(drBasic("Item_Code"))) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(drBasicTemp("OrgConversionFactor")) = 1 Then
                                drAfterModify("MRP_Amt") = clsCommon.myCdbl(drBasicTemp("MRP_Amt"))
                                drAfterModify("Basic_Rate") = clsCommon.myCdbl(drBasicTemp("Basic_Rate"))
                                isFound = True
                                Exit For
                            End If
                        Next
                        If Not isFound Then
                            drAfterModify("MRP_Amt") = Math.Round(clsCommon.myCdbl(drBasic("MRP_Amt")) * dblConvFactor, 2, MidpointRounding.ToEven)
                            drAfterModify("Basic_Rate") = Math.Round(clsCommon.myCdbl(drBasic("Basic_Rate")) * dblConvFactor, 2, MidpointRounding.ToEven)
                        End If
                    End If
                    dtAfterModify.Rows.Add(drAfterModify)
                Next

                SetItemWiseTax(dtAfterModify, strDocNo)
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtAfterModify, EnumTecxpertPaperSize.NA, "crptSaleReturnExcise", "Sales Report", True)
            Else
                strQuery = "  SELECT  '' as Cust_PONo," + qryForShipToAdd + " AS Address, " & _
               " TSPL_SALE_RETURN_HEad.Sale_Return_No, convert(varchar(10),TSPL_SALE_RETURN_HEad.Sale_Return_Date ,103) as Sale_Return_Date, TSPL_SALE_RETURN_HEad.Invoice_No  as Sale_Invoice_No, convert(varchar(10),TSPL_SALE_RETURN_HEad.Invoice_Date  ,103) as Sale_Invoice_Date, TSPL_SALE_RETURN_HEad.Cust_Name, " & _
               " (" + qryForGettingTax + "= TSPL_SALE_RETURN_HEAD.TAX1) as TAX1, TSPL_SALE_RETURN_HEAD.TAX1_Rate, TSPL_SALE_RETURN_HEAD.TAX1_Assessable_Amt, " & _
               " TSPL_SALE_RETURN_HEAD.TAX1_Amt, (" + qryForGettingTax + "=TSPL_SALE_RETURN_HEAD.TAX2) as TAX2, TSPL_SALE_RETURN_HEAD.TAX2_Rate, " & _
               " TSPL_SALE_RETURN_HEAD.TAX2_Assessable_Amt, TSPL_SALE_RETURN_HEAD.TAX2_Amt,(" + qryForGettingTax + "= TSPL_SALE_RETURN_HEAD.TAX3) as TAX3, " & _
               " TSPL_SALE_RETURN_HEAD.TAX3_Rate, TSPL_SALE_RETURN_HEAD.TAX3_Assessable_Amt, TSPL_SALE_RETURN_HEAD.TAX3_Amt, " & _
               " (" + qryForGettingTax + "= TSPL_SALE_RETURN_HEAD.TAX4) as TAX4, TSPL_SALE_RETURN_HEAD.TAX4_Rate, TSPL_SALE_RETURN_HEAD.TAX4_Assessable_Amt, " & _
               " TSPL_SALE_RETURN_HEAD.TAX4_Amt, (" + qryForGettingTax + "= TSPL_SALE_RETURN_HEAD.TAX5) as TAX5, TSPL_SALE_RETURN_HEAD.TAX5_Rate, " & _
               " TSPL_SALE_RETURN_HEAD.TAX5_Assessable_Amt, TSPL_SALE_RETURN_HEAD.TAX5_Amt,TSPL_SALE_RETURN_HEad.Total_Invoice_Amt  as Total_Invoice_Amt,TSPL_SALE_RETURN_HEad.Vehicle_No,TSPL_SALE_RETURN_HEad.KM_Reading , TSPL_SALE_RETURN_DETAIL.Balance_Qty  as Invoice_Qty, " & _
               " TSPL_SALE_RETURN_DETAIL.Item_Code, TSPL_SALE_RETURN_DETAIL.Item_Desc   + ' (' + TSPL_SALE_RETURN_DETAIL.Unit_Code + ')' as Item_Desc,TSPL_SALE_RETURN_DETAIL.MRP_Amt/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SALE_RETURN_DETAIL.Item_Code and UOM_Code = (case when TSPL_SALE_RETURN_DETAIL.Unit_code='FC' then 'FB' else  case when TSPL_SALE_RETURN_DETAIL.Unit_code='FB' then 'FC' else '' end end))  as [MRP_Amt], " & _
               " TSPL_SALE_RETURN_DETAIL.Basic_Rate, TSPL_SALE_RETURN_DETAIL.Item_Assessable_Rate, TSPL_SALE_RETURN_DETAIL.Item_Net_Amt, " & _
               "  TSPL_SALE_RETURN_DETAIL.TAX1 AS 'DTax1', TSPL_SALE_RETURN_DETAIL.TAX1_Rate AS 'DTax1Rate', " & _
               " TSPL_SALE_RETURN_DETAIL.TAX1_Assessable_Amt AS 'DTax1Ass', '0.00' AS 'Dtax1Amt', " & _
               "  TSPL_SALE_RETURN_DETAIL.Total_Assessable_Amt, TSPL_SALE_RETURN_DETAIL.Total_MRP_Amt, TSPL_SALE_RETURN_DETAIL.Total_Basic_Amt, " & _
               " TSPL_SALE_RETURN_DETAIL.Total_net_Amt, TSPL_SALE_RETURN_DETAIL.Total_Tax_Amt, TSPL_SALE_RETURN_DETAIL.Total_Item_Amt, " & _
               " TSPL_SALE_RETURN_HEAD.Empty_Value, TSPL_SALE_RETURN_DETAIL.Total_TPT,TSPL_SALE_RETURN_HEAD.TPT as 'ttlTPT', " & _
               "  TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 " & _
               " " + str + "TSPL_CUSTOMER_MASTER.Tin_No,TSPL_SALE_RETURN_HEAD.comments as Remarks  ,TSPL_SALE_RETURN_HEAD.Description," + strInvoiceType + " , (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_RETURN_DETAIL.Item_Code and UOM_Code = (case when TSPL_SALE_RETURN_DETAIL.Unit_code='FC' then 'FB' else  case when TSPL_SALE_RETURN_DETAIL.Unit_code='FB' then 'FC' else '' end end) ) as [Conversion],TSPL_EMPLOYEE_MASTER.Emp_Name as SalesmanName , " & _
                "" + strIsFOCItem + ",TSPL_SALE_RETURN_HEAD.Inv_Discount_Amt  as Inv_Discount_Amt,TSPL_SALE_RETURN_HEAD.TAX1 as Tax1Code,TSPL_SALE_RETURN_HEAD.TAX2 as Tax2Code,TSPL_SALE_RETURN_HEAD.TAX3 as Tax3Code,TSPL_SALE_RETURN_HEAD.TAX4 as Tax4Code,TSPL_SALE_RETURN_HEAD.TAX5 as Tax5Code" & _
               ",(select [USER_NAME] from TSPL_USER_MASTER where TSPL_USER_MASTER.User_Code=TSPL_SALE_RETURN_HEAD.Created_By) as CreateByName" & _
               ",(case when TSPL_SALE_RETURN_HEAD.Is_Post='Y' then (select [USER_NAME] from TSPL_USER_MASTER where User_Code=TSPL_SALE_RETURN_HEAD.Modify_By) else '' end) as PostByName,TSPL_SALE_RETURN_HEAD.Salesman_Code,TSPL_SALE_RETURN_HEAD.Route_No,TSPL_SALE_RETURN_HEAD.Route_Desc,'' as TransferNo,'' as CustomerInvNo ,'' as VerifyByName,TSPL_SALE_RETURN_DETAIL.Leak_Qty,TSPL_SALE_RETURN_DETAIL.Burst_Qty,TSPL_SALE_RETURN_DETAIL.Short_Qty,TSPL_SALE_RETURN_DETAIL.Actual_Return_Qty FROM TSPL_CUSTOMER_MASTER INNER JOIN  " & _
               "  TSPL_SALE_RETURN_HEAD  ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_RETURN_HEAD.Cust_Code INNER JOIN" & _
               " TSPL_SALE_RETURN_DETAIL  ON TSPL_SALE_RETURN_HEAD .Sale_Return_No  =TSPL_SALE_RETURN_DETAIL .Sale_Return_No   left outer join TSPL_ITEM_MASTER on TSPL_SALE_RETURN_DETAIL.Item_Code =TSPL_ITEM_MASTER .Item_Code " & _
               " left outer join TSPL_COMPANY_MASTER on TSPL_SALE_RETURN_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_SALE_RETURN_Head.Salesman_Code left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code=TSPL_SALE_RETURN_HEAD.Cust_Code and TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SALE_RETURN_HEAD.Location "
                strQuery = strQuery & whereclause

                strQuery += " order by TSPL_ITEM_MASTER.Sku_Seq"
                Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                SetItemWiseTax(dtNew, strDocNo)
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtNew, EnumTecxpertPaperSize.NA, "crptSaleReturnNonExcise", "Sales Report", True)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub



    Public Shared Function SetItemWiseTax(ByVal dtAfterModify As DataTable, ByVal strShipFrm As String) As DataTable
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



        Dim qry As String = "select Tax,Rate,SUM(Amt ) as TaxAmt" + Environment.NewLine
        qry += " from (" + Environment.NewLine
        qry += " select TAX1 as Tax,TAX1_Rate as Rate,TOT_TAX1_Amt as Amt,Balance_Qty" + Environment.NewLine
        qry += " from TSPL_SALE_RETURN_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1" + Environment.NewLine
        qry += " where Sale_Return_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TAX2 as Tax,TAX2_Rate as Rate,TOT_TAX2_Amt as Amt,Balance_Qty " + Environment.NewLine
        qry += " from TSPL_SALE_RETURN_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2" + Environment.NewLine
        qry += " where Sale_Return_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TAX3 as Tax,TAX3_Rate as Rate,TOT_TAX3_Amt as Amt,Balance_Qty " + Environment.NewLine
        qry += " from TSPL_SALE_RETURN_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3" + Environment.NewLine
        qry += " where Sale_Return_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += "  union all " + Environment.NewLine
        qry += " select TAX4 as Tax,TAX4_Rate as Rate,TOT_TAX4_Amt as Amt,Balance_Qty " + Environment.NewLine
        qry += " from TSPL_SALE_RETURN_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4" + Environment.NewLine
        qry += " where Sale_Return_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TAX5 as Tax,TAX5_Rate as Rate,TOT_TAX5_Amt as Amt,Balance_Qty " + Environment.NewLine
        qry += " from TSPL_SALE_RETURN_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5" + Environment.NewLine
        qry += " where Sale_Return_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TAX6 as Tax,TAX6_Rate as Rate,TOT_TAX6_Amt as Amt,Balance_Qty " + Environment.NewLine
        qry += " from TSPL_SALE_RETURN_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6" + Environment.NewLine
        qry += " where Sale_Return_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " )xxx " + Environment.NewLine
        qry += " group by Tax,Rate   " + Environment.NewLine

        Dim qryMain As String = qry + " having Tax in( select Tax from(" + qry + ")xxxx group by tax having SUM(1)>1)"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryMain)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                For ii As Integer = 1 To 5
                    Dim strCol As String = "TAX" + clsCommon.myCstr(ii) + "Code"
                    If clsCommon.CompairString(clsCommon.myCstr(dtAfterModify.Rows(0)(strCol)), clsCommon.myCstr(dr("Tax"))) = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt1")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate1") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt1") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt2")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate2") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt2") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt3")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate3") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt3") = clsCommon.myCdbl(dr("TaxAmt"))
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

    '----------------- Code End Here -----------------


    'Exist item in fb.
    'Sub SelectSaleInvoiceItems()
    '    isInsideLoadData = True
    '    LoadBlankGrid()
    '    LoadBlankGridTax()
    '    Dim qry As String = "select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,TSPL_SALE_INVOICE_HEAD.Cust_Account,TSPL_SALE_INVOICE_HEAD.Cust_Code,TSPL_SALE_INVOICE_HEAD.Cust_Name,TSPL_SALE_INVOICE_HEAD.Cust_PONo, TSPL_SALE_INVOICE_HEAD.Tax_Group,TSPL_SALE_INVOICE_HEAD.Location, TSPL_LOCATION_MASTER.Location_Desc as LocationName,Invoice_Type,TSPL_SALE_INVOICE_HEAD.Due_Date,TSPL_SALE_INVOICE_HEAD.Shell_Qty,TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Route_Desc,TSPL_SALE_INVOICE_HEAD.Salesman_Code,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesManName,TSPL_SALE_INVOICE_DETAIL.Empty_Value,TSPL_SALE_INVOICE_DETAIL.TPT,TSPL_SALE_INVOICE_HEAD.Mode_Of_Transport," + Environment.NewLine
    '    qry += " TSPL_SALE_INVOICE_HEAD.Vehicle_Code,TSPL_SALE_INVOICE_HEAD.Shipment_Type ,TSPL_SALE_INVOICE_HEAD.Price_Code as HPrice_Code,TSPL_SALE_INVOICE_HEAD.KM_Reading,TSPL_SALE_INVOICE_HEAD.Scheme_Sample_Code ,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,TSPL_SALE_INVOICE_HEAD.Vehicle_No,TSPL_SALE_INVOICE_HEAD.Total_Assessable_Amount,TSPL_SALE_INVOICE_HEAD.Inv_Discount_Amt,TSPL_SALE_INVOICE_HEAD.Inv_Detail_Disc_Amt,TSPL_SALE_INVOICE_HEAD.Inv_Tax_Amt,TSPL_SALE_INVOICE_HEAD.Freight_Amt, TSPL_SALE_INVOICE_HEAD.Other_Charges,TSPL_SALE_INVOICE_HEAD.Add_Charges,TSPL_SALE_INVOICE_HEAD.Terms_Code, TSPL_SALE_INVOICE_HEAD.TPT as HTPT,TSPL_SALE_INVOICE_HEAD.Empty_Value as HEmpty_Value,TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt,TSPL_SALE_INVOICE_HEAD.Inv_Detail_Total_Amt," + Environment.NewLine
    '    qry += " TSPL_SALE_INVOICE_HEAD.TAX1 as HTAX1,TSPL_SALE_INVOICE_HEAD.TAX1_Rate as HTAX1_Rate,TSPL_SALE_INVOICE_HEAD.TAX1_Assessable_Amt as HTAX1_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX1_Amt as HTAX1_Amt," + Environment.NewLine
    '    qry += " TSPL_SALE_INVOICE_HEAD.TAX2 as HTAX2,TSPL_SALE_INVOICE_HEAD.TAX2_Rate as HTAX2_Rate,TSPL_SALE_INVOICE_HEAD.TAX2_Assessable_Amt as HTAX2_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX2_Amt as HTAX2_Amt," + Environment.NewLine
    '    qry += " TSPL_SALE_INVOICE_HEAD.TAX3 as HTAX3,TSPL_SALE_INVOICE_HEAD.TAX3_Rate as HTAX3_Rate,TSPL_SALE_INVOICE_HEAD.TAX3_Assessable_Amt as HTAX3_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX3_Amt as HTAX3_Amt," + Environment.NewLine
    '    qry += " TSPL_SALE_INVOICE_HEAD.TAX4 as HTAX4,TSPL_SALE_INVOICE_HEAD.TAX4_Rate as HTAX4_Rate,TSPL_SALE_INVOICE_HEAD.TAX4_Assessable_Amt as HTAX4_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX4_Amt as HTAX4_Amt," + Environment.NewLine
    '    qry += " TSPL_SALE_INVOICE_HEAD.TAX5 as HTAX5,TSPL_SALE_INVOICE_HEAD.TAX5_Rate as HTAX5_Rate,TSPL_SALE_INVOICE_HEAD.TAX5_Assessable_Amt as HTAX5_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX5_Amt as HTAX5_Amt," + Environment.NewLine
    '    qry += " TSPL_SALE_INVOICE_HEAD.TAX6 as HTAX6,TSPL_SALE_INVOICE_HEAD.TAX6_Rate as HTAX6_Rate,TSPL_SALE_INVOICE_HEAD.TAX6_Assessable_Amt as HTAX6_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX6_Amt as HTAX6_Amt," + Environment.NewLine
    '    qry += " TSPL_SALE_INVOICE_HEAD.TAX7 as HTAX7,TSPL_SALE_INVOICE_HEAD.TAX7_Rate as HTAX7_Rate,TSPL_SALE_INVOICE_HEAD.TAX7_Assessable_Amt as HTAX7_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX7_Amt as HTAX7_Amt," + Environment.NewLine
    '    qry += " TSPL_SALE_INVOICE_HEAD.TAX8 as HTAX8,TSPL_SALE_INVOICE_HEAD.TAX8_Rate as HTAX8_Rate,TSPL_SALE_INVOICE_HEAD.TAX8_Assessable_Amt as HTAX8_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX8_Amt as HTAX8_Amt," + Environment.NewLine
    '    qry += " TSPL_SALE_INVOICE_HEAD.TAX9 as HTAX9,TSPL_SALE_INVOICE_HEAD.TAX9_Rate as HTAX9_Rate,TSPL_SALE_INVOICE_HEAD.TAX9_Assessable_Amt as HTAX9_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX9_Amt as HTAX9_Amt," + Environment.NewLine
    '    qry += " TSPL_SALE_INVOICE_HEAD.TAX10 as HTAX10,TSPL_SALE_INVOICE_HEAD.TAX10_Rate as HTAX10_Rate,TSPL_SALE_INVOICE_HEAD.TAX10_Assessable_Amt as HTAX10_Assessable_Amt,TSPL_SALE_INVOICE_HEAD.TAX10_Amt as HTAX10_Amt," + Environment.NewLine
    '    qry += " TSPL_SALE_INVOICE_HEAD.Level1_User_code as HLevel1_User_code,TSPL_SALE_INVOICE_HEAD.Level2_User_code as HLevel2_User_code,TSPL_SALE_INVOICE_HEAD.Level3_User_code as HLevel3_User_code,TSPL_SALE_INVOICE_HEAD.Level4_User_code as HLevel4_User_code,TSPL_SALE_INVOICE_HEAD.Level5_User_code as HLevel5_User_code,TSPL_SALE_INVOICE_HEAD.Level1_User_Commission as HLevel1_User_Commission,TSPL_SALE_INVOICE_HEAD.Level2_User_Commission as HLevel2_User_Commission,TSPL_SALE_INVOICE_HEAD.Level3_User_Commission as HLevel3_User_Commission,TSPL_SALE_INVOICE_HEAD.Level4_User_Commission as HLevel4_User_Commission,TSPL_SALE_INVOICE_HEAD.Level5_User_Commission  as HLevel5_User_Commission, "

    '    qry += " TSPL_SALE_INVOICE_DETAIL.Item_Code as DItem_Code,TSPL_SALE_INVOICE_DETAIL.Item_Desc as DItem_Desc,TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as DInvoice_Qty,TSPL_SALE_INVOICE_DETAIL.Price_Date as DPrice_Date,TSPL_SALE_INVOICE_DETAIL.Unit_code as DUnit_code,TSPL_SALE_INVOICE_DETAIL.Location as DLocation,TSPL_SALE_INVOICE_DETAIL.Price_code as DPrice_code,TSPL_SALE_INVOICE_DETAIL.Scheme_Applicable as DScheme_Applicable,TSPL_SALE_INVOICE_DETAIL.Scheme_Code_Qty as DScheme_Code_Qty,TSPL_SALE_INVOICE_DETAIL.Scheme_Item as DScheme_Item,TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Applicable as DPromo_Scheme_Applicable,TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Code as DPromo_Scheme_Code,TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item as DPromo_Scheme_Item,TSPL_SALE_INVOICE_DETAIL.Scheme_Disc_Applicable as DScheme_Disc_Applicable,TSPL_SALE_INVOICE_DETAIL.Scheme_Code_Cash as DScheme_Code_Cash,TSPL_SALE_INVOICE_DETAIL.Sampling_Item as DSampling_Item,TSPL_SALE_INVOICE_DETAIL.MRP_Amt as DMRP_Amt,TSPL_SALE_INVOICE_DETAIL.Basic_Rate as DBasic_Rate,TSPL_SALE_INVOICE_DETAIL.Item_Assessable_Rate as DItem_Assessable_Rate,TSPL_SALE_INVOICE_DETAIL.Disc_Amt as DDisc_Amt,TSPL_SALE_INVOICE_DETAIL.Item_Net_Amt as DItem_Net_Amt," + Environment.NewLine
    '    qry += " TSPL_SALE_INVOICE_DETAIL.TAX1 as DTAX1,TSPL_SALE_INVOICE_DETAIL.TAX1_Rate as DTAX1_Rate,TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt as DTAX1_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX1_Amt as DTAX1_Amt,TSPL_SALE_INVOICE_DETAIL.TAX2 as DTAX2,TSPL_SALE_INVOICE_DETAIL.TAX2_Rate as DTAX2_Rate,TSPL_SALE_INVOICE_DETAIL.TAX2_Assessable_Amt as DTAX2_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX2_Amt as DTAX2_Amt,TSPL_SALE_INVOICE_DETAIL.Item_Tax as DItem_Tax,TSPL_SALE_INVOICE_DETAIL.Total_Assessable_Amt as DTotal_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
    '    qry += " TAX3 as DTAX3,TSPL_SALE_INVOICE_DETAIL.TAX3_Rate as DTAX3_Rate,TSPL_SALE_INVOICE_DETAIL.TAX3_Assessable_Amt as DTAX3_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX3_Amt as DTAX3_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
    '    qry += " TAX4 as DTAX4,TSPL_SALE_INVOICE_DETAIL.TAX4_Rate as DTAX4_Rate,TSPL_SALE_INVOICE_DETAIL.TAX4_Assessable_Amt as DTAX4_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX4_Amt as DTAX4_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
    '    qry += " TAX5 as DTAX5,TSPL_SALE_INVOICE_DETAIL.TAX5_Rate as DTAX5_Rate,TSPL_SALE_INVOICE_DETAIL.TAX5_Assessable_Amt as DTAX5_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX5_Amt as DTAX5_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
    '    qry += " TAX6 as DTAX6,TSPL_SALE_INVOICE_DETAIL.TAX6_Rate as DTAX6_Rate,TSPL_SALE_INVOICE_DETAIL.TAX6_Assessable_Amt as DTAX6_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX6_Amt as DTAX6_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
    '    qry += " TAX7 as DTAX7,TSPL_SALE_INVOICE_DETAIL.TAX7_Rate as DTAX7_Rate,TSPL_SALE_INVOICE_DETAIL.TAX7_Assessable_Amt as DTAX7_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX7_Amt as DTAX7_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
    '    qry += " TAX8 as DTAX8,TSPL_SALE_INVOICE_DETAIL.TAX8_Rate as DTAX8_Rate,TSPL_SALE_INVOICE_DETAIL.TAX8_Assessable_Amt as DTAX8_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX8_Amt as DTAX8_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
    '    qry += " TAX9 as DTAX9,TSPL_SALE_INVOICE_DETAIL.TAX9_Rate as DTAX9_Rate,TSPL_SALE_INVOICE_DETAIL.TAX9_Assessable_Amt as DTAX9_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX9_Amt as DTAX9_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
    '    qry += " TAX10 as DTAX10,TSPL_SALE_INVOICE_DETAIL.TAX10_Rate as DTAX10_Rate,TSPL_SALE_INVOICE_DETAIL.TAX10_Assessable_Amt as DTAX10_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL.TAX10_Amt as DTAX10_Amt,TSPL_SALE_INVOICE_DETAIL.Item_Tax as DItem_Tax,TSPL_SALE_INVOICE_DETAIL.Total_Assessable_Amt as DTotal_Assessable_Amt,TSPL_SALE_INVOICE_DETAIL." + Environment.NewLine
    '    qry += " Total_MRP_Amt as DTotal_MRP_Amt,TSPL_SALE_INVOICE_DETAIL.Total_Basic_Amt as DTotal_Basic_Amt,TSPL_SALE_INVOICE_DETAIL.Total_Disc_Amt as DTotal_Disc_Amt,TSPL_SALE_INVOICE_DETAIL.Total_net_Amt as DTotal_net_Amt,TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt as DTotal_Tax_Amt,TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt as DTotal_Item_Amt,TSPL_SALE_INVOICE_DETAIL.Empty_Value as DEmpty_Value,TSPL_SALE_INVOICE_DETAIL.TPT as DTPT,TSPL_SALE_INVOICE_DETAIL.Total_TPT as DTotal_TPT,TSPL_SALE_INVOICE_DETAIL.Empty_Value_Shell as DEmpty_Value_Shell,TSPL_SALE_INVOICE_DETAIL.Empty_Value_Bottle as DEmpty_Value_Bottle,TSPL_SALE_INVOICE_DETAIL.Cust_Discount as DCust_Discount,TSPL_SALE_INVOICE_DETAIL.Total_Cust_Discount as DTotal_Cust_Discount,TSPL_SALE_INVOICE_DETAIL.Unit_Cogs as DUnit_cogs " + Environment.NewLine
    '    '------------------
    '    qry += ",TSPL_SALE_INVOICE_DETAIL.price_amount1 as DPrice_Amount1,TSPL_SALE_INVOICE_DETAIL.price_amount2 as DPrice_Amount2,TSPL_SALE_INVOICE_DETAIL.price_amount3 as DPrice_Amount3,TSPL_SALE_INVOICE_DETAIL.price_amount4 as DPrice_Amount4,TSPL_SALE_INVOICE_DETAIL.price_amount5 as DPrice_Amount5,TSPL_SALE_INVOICE_DETAIL.price_amount6 as DPrice_Amount6,TSPL_SALE_INVOICE_DETAIL.price_amount7 as DPrice_Amount7,TSPL_SALE_INVOICE_DETAIL.price_amount8 as DPrice_Amount8,TSPL_SALE_INVOICE_DETAIL.price_amount9 as DPrice_Amount9,TSPL_SALE_INVOICE_DETAIL.price_amount10 as DPrice_Amount10,TSPL_SALE_INVOICE_DETAIL.Main_Item as Main_item,TSPL_SALE_INVOICE_DETAIL.Discount_Code as Discount_code " + Environment.NewLine
    '    '--------------
    '    qry += ",isnull(case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FB' then 1 else (select 1 from TSPL_SALE_INVOICE_DETAIL as inn"
    '    qry += " left outer join TSPL_SALE_INVOICE_HEAD as InnHead on InnHead.Sale_Invoice_No=inn.Sale_Invoice_No"
    '    qry += " where inn.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No and inn.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and inn.Unit_code='FB' and 'N'= (CASE WHEN inn.Scheme_Item='Y' or inn.Promo_Scheme_Item='Y' or inn.Sampling_Item='Y' or InnHead.Inv_Disc_Percent=100 THEN 'Y' ELSE 'N' END))end,0)  as IsFBRowExist"
    '    qry += ",TSPL_SALE_INVOICE_DETAIL.From_Scheme_Code "
    '    qry += " from TSPL_SALE_INVOICE_DETAIL" + Environment.NewLine
    '    qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
    '    qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SALE_INVOICE_HEAD.Location" + Environment.NewLine
    '    qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER .EMP_CODE=TSPL_SALE_INVOICE_HEAD.Salesman_Code" + Environment.NewLine
    '    qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No='" + txtInvoiceNo.Value + "' order by TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id "
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

    '        txtInvoiceDate.Value = clsCommon.myCstr(dt.Rows(0)("Sale_Invoice_Date"))
    '        txtCustomerNo.Value = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
    '        lblCustomerName.Text = clsCommon.myCstr(dt.Rows(0)("Cust_Name"))
    '        txtCustomerPONO.Text = clsCommon.myCstr(dt.Rows(0)("Cust_PONo"))
    '        txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
    '        txtLocation.Value = clsCommon.myCstr(dt.Rows(0)("Location"))
    '        lblLocation.Text = clsCommon.myCstr(dt.Rows(0)("LocationName"))
    '        lblCustAccount.Text = clsCommon.myCstr(dt.Rows(0)("Cust_Account"))
    '        txtRouteNo.Value = clsCommon.myCstr(dt.Rows(0)("Route_No"))
    '        lblRouteNo.Text = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
    '        txtSalesman.Value = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
    '        lblSalesman.Text = clsCommon.myCstr(dt.Rows(0)("SalesManName"))
    '        txtModeOfTransport.Text = clsCommon.myCstr(dt.Rows(0)("Mode_Of_Transport"))
    '        txtVehicleNo.Value = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
    '        Shippment_type = clsCommon.myCstr(dt.Rows(0)("Shipment_Type"))
    '        lblVehicleNo.Text = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))

    '        txtSchemeSampleCode.Text = clsCommon.myCstr(dt.Rows(0)("Scheme_Sample_Code"))
    '        txtKMReading.Text = clsCommon.myCstr(dt.Rows(0)("KM_Reading"))
    '        txtInvoiceDate.Text = clsCommon.myCDate(dt.Rows(0)("Sale_Invoice_Date"))

    '        lblAssessableAmt.Text = clsCommon.myFormat(dt.Rows(0)("Total_Assessable_Amount"))
    '        lblDetailDiscountAmt.Text = clsCommon.myFormat(dt.Rows(0)("Inv_Detail_Disc_Amt"))
    '        lblTaxAmt.Text = clsCommon.myFormat(dt.Rows(0)("Inv_Tax_Amt"))
    '        lblFreightAmount.Text = clsCommon.myFormat(dt.Rows(0)("Freight_Amt"))
    '        lblOtherCharges.Text = clsCommon.myFormat(dt.Rows(0)("Other_Charges"))
    '        lblAdditionalCharges.Text = clsCommon.myFormat(dt.Rows(0)("Add_Charges"))

    '        lblDetaolTotAmt.Text = clsCommon.myFormat(dt.Rows(0)("Inv_Detail_Total_Amt"))
    '        lblDiscountAmt.Text = clsCommon.myFormat(dt.Rows(0)("Inv_Discount_Amt"))
    '        lblTotAmt.Text = clsCommon.myFormat(dt.Rows(0)("Total_Invoice_Amt"))
    '        lblTPT.Text = clsCommon.myFormat(dt.Rows(0)("HTPT"))
    '        lblEmptyValue.Text = clsCommon.myFormat(dt.Rows(0)("HEmpty_Value"))
    '        txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
    '        txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("HPrice_Code"))

    '        lblLevel1_User_code.Text = clsCommon.myCstr(dt.Rows(0)("HLevel1_User_code"))
    '        lblLevel2_User_code.Text = clsCommon.myCstr(dt.Rows(0)("HLevel2_User_code"))
    '        lblLevel3_User_code.Text = clsCommon.myCstr(dt.Rows(0)("HLevel3_User_code"))
    '        lblLevel4_User_code.Text = clsCommon.myCstr(dt.Rows(0)("HLevel4_User_code"))
    '        lblLevel5_User_code.Text = clsCommon.myCstr(dt.Rows(0)("HLevel5_User_code"))

    '        lblLevel1_User_Commission.Text = clsCommon.myFormat(dt.Rows(0)("HLevel1_User_Commission"))
    '        lblLevel2_User_Commission.Text = clsCommon.myFormat(dt.Rows(0)("HLevel2_User_Commission"))
    '        lblLevel3_User_Commission.Text = clsCommon.myFormat(dt.Rows(0)("HLevel3_User_Commission"))
    '        lblLevel4_User_Commission.Text = clsCommon.myFormat(dt.Rows(0)("HLevel4_User_Commission"))
    '        lblLevel5_User_Commission.Text = clsCommon.myFormat(dt.Rows(0)("HLevel5_User_Commission"))

    '        lblNetAmt.Text = clsCommon.myFormat(clsCommon.myCdbl(lblEmptyValue.Text) + clsCommon.myCdbl(lblTotAmt.Text))

    '        If (clsCommon.myLen(dt.Rows(0)("HTAX1")) > 0) Then
    '            gv2.Rows.AddNew()
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX1"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX1")))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX1_Rate"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX1_Assessable_Amt"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX1_Amt"))
    '        End If
    '        If (clsCommon.myLen(dt.Rows(0)("HTAX2")) > 0) Then
    '            gv2.Rows.AddNew()
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX2"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX2")))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX2_Rate"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX2_Assessable_Amt"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX2_Amt"))
    '        End If
    '        If (clsCommon.myLen(dt.Rows(0)("HTAX3")) > 0) Then
    '            gv2.Rows.AddNew()
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX3"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX3")))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX3_Rate"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX3_Assessable_Amt"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX3_Amt"))
    '        End If
    '        If (clsCommon.myLen(dt.Rows(0)("HTAX4")) > 0) Then
    '            gv2.Rows.AddNew()
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX4"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX4")))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX4_Rate"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX4_Assessable_Amt"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX4_Amt"))
    '        End If
    '        If (clsCommon.myLen(dt.Rows(0)("HTAX5")) > 0) Then
    '            gv2.Rows.AddNew()
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX5"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX5")))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX5_Rate"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX5_Assessable_Amt"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX5_Amt"))
    '        End If
    '        If (clsCommon.myLen(dt.Rows(0)("HTAX6")) > 0) Then
    '            gv2.Rows.AddNew()
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX6"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX6")))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX6_Rate"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX6_Assessable_Amt"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX6_Amt"))
    '        End If
    '        If (clsCommon.myLen(dt.Rows(0)("HTAX7")) > 0) Then
    '            gv2.Rows.AddNew()
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX7"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX7")))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX7_Rate"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX7_Assessable_Amt"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX7_Amt"))
    '        End If
    '        If (clsCommon.myLen(dt.Rows(0)("HTAX8")) > 0) Then
    '            gv2.Rows.AddNew()
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX8"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX8")))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX8_Rate"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX8_Assessable_Amt"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX8_Amt"))
    '        End If
    '        If (clsCommon.myLen(dt.Rows(0)("HTAX9")) > 0) Then
    '            gv2.Rows.AddNew()
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX9"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX9")))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX9_Rate"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX9_Assessable_Amt"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX9_Amt"))
    '        End If
    '        If (clsCommon.myLen(dt.Rows(0)("HTAX10")) > 0) Then
    '            gv2.Rows.AddNew()
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("HTAX10"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("HTAX10")))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX10_Rate"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTAssessAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX10_Assessable_Amt"))
    '            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("HTAX10_Amt"))
    '        End If


    '        For Each dr As DataRow In dt.Rows
    '            gv1.Rows.AddNew()
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("DItem_Code"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("DItem_Desc"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsCommon.myCstr(dr("DInvoice_Qty"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colReturnQty).Value = clsCommon.myCstr(dr("DInvoice_Qty"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDate).Value = clsCommon.GetPrintDate(dr("DPrice_Date"), "dd/MM/yyyy")
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("DUnit_code"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCode).Value = clsCommon.myCstr(dr("DPrice_code"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = clsCommon.myCstr(dr("DScheme_Applicable"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeCodeQty).Value = clsCommon.myCdbl(dr("DScheme_Code_Qty"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeItem).Value = clsCommon.myCstr(dr("DScheme_Item"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colPromoSchemeApplicable).Value = clsCommon.myCstr(dr("DPromo_Scheme_Item"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colPromoSchemeCode).Value = clsCommon.myCstr(dr("DPromo_Scheme_Code"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeDiscApplicable).Value = clsCommon.myCstr(dr("DPromo_Scheme_Applicable"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeCodeCash).Value = clsCommon.myCstr(dr("DScheme_Code_Cash"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colSamplingItem).Value = clsCommon.myCstr(dr("DSampling_Item"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRPAmt).Value = clsCommon.myCdbl(dr("DMRP_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colBasicRate).Value = clsCommon.myCdbl(dr("DBasic_Rate"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemAssessableRate).Value = clsCommon.myCdbl(dr("DItem_Assessable_Rate"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colDiscAmt).Value = clsCommon.myCdbl(dr("DDisc_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemNetAmt).Value = clsCommon.myCdbl(dr("DItem_Net_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTax1).Value = clsCommon.myCstr(dr("DTAX1"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = clsCommon.myCdbl(dr("DTAX1_Rate"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = clsCommon.myCdbl(dr("DTAX1_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt1).Value = clsCommon.myCdbl(dr("DTAX1_Assessable_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTax2).Value = clsCommon.myCstr(dr("DTAX2"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = clsCommon.myCdbl(dr("DTAX2_Rate"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = clsCommon.myCdbl(dr("DTAX2_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt2).Value = clsCommon.myCdbl(dr("DTAX2_Assessable_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTax3).Value = clsCommon.myCstr(dr("DTAX3"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = clsCommon.myCdbl(dr("DTAX3_Rate"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = clsCommon.myCdbl(dr("DTAX3_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt3).Value = clsCommon.myCdbl(dr("DTAX3_Assessable_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTax4).Value = clsCommon.myCstr(dr("DTAX4"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = clsCommon.myCdbl(dr("DTAX4_Rate"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = clsCommon.myCdbl(dr("DTAX4_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt4).Value = clsCommon.myCdbl(dr("DTAX4_Assessable_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTax5).Value = clsCommon.myCstr(dr("DTAX5"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = clsCommon.myCdbl(dr("DTAX5_Rate"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = clsCommon.myCdbl(dr("DTAX5_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt5).Value = clsCommon.myCdbl(dr("DTAX5_Assessable_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTax6).Value = clsCommon.myCstr(dr("DTAX6"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = clsCommon.myCdbl(dr("DTAX6_Rate"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = clsCommon.myCdbl(dr("DTAX6_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt6).Value = clsCommon.myCdbl(dr("DTAX6_Assessable_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTax7).Value = clsCommon.myCstr(dr("DTAX7"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = clsCommon.myCdbl(dr("DTAX7_Rate"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = clsCommon.myCdbl(dr("DTAX7_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt7).Value = clsCommon.myCdbl(dr("DTAX7_Assessable_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTax8).Value = clsCommon.myCstr(dr("DTAX8"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = clsCommon.myCdbl(dr("DTAX8_Rate"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = clsCommon.myCdbl(dr("DTAX8_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt8).Value = clsCommon.myCdbl(dr("DTAX8_Assessable_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTax9).Value = clsCommon.myCstr(dr("DTAX9"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = clsCommon.myCdbl(dr("DTAX9_Rate"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = clsCommon.myCdbl(dr("DTAX9_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt9).Value = clsCommon.myCdbl(dr("DTAX9_Assessable_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTax10).Value = clsCommon.myCstr(dr("DTAX10"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = clsCommon.myCdbl(dr("DTAX10_Rate"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = clsCommon.myCdbl(dr("DTAX10_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt10).Value = clsCommon.myCdbl(dr("DTAX10_Assessable_Amt"))

    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTax).Value = clsCommon.myCdbl(dr("DTAX1_Amt")) + clsCommon.myCdbl(dr("DTAX2_Amt")) + clsCommon.myCdbl(dr("DTAX3_Amt")) + clsCommon.myCdbl(dr("DTAX4_Amt")) + clsCommon.myCdbl(dr("DTAX5_Amt")) + clsCommon.myCdbl(dr("DTAX6_Amt")) + clsCommon.myCdbl(dr("DTAX7_Amt")) + clsCommon.myCdbl(dr("DTAX8_Amt")) + clsCommon.myCdbl(dr("DTAX9_Amt")) + clsCommon.myCdbl(dr("DTAX10_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTax).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTax).Value), 3, MidpointRounding.ToEven)

    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssessableAmt).Value = clsCommon.myCdbl(dr("DTotal_Assessable_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalMRPAmt).Value = clsCommon.myCdbl(dr("DTotal_MRP_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalBasicAmt).Value = clsCommon.myCdbl(dr("DTotal_Basic_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalDiscAmt).Value = clsCommon.myCdbl(dr("DTotal_Disc_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalnetAmt).Value = clsCommon.myCdbl(dr("DTotal_net_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalTaxAmt).Value = clsCommon.myCdbl(dr("DTotal_Tax_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalItemAmt).Value = clsCommon.myCdbl(dr("DTotal_Item_Amt"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colEmptyValue).Value = clsCommon.myCdbl(dr("DEmpty_Value"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTPT).Value = clsCommon.myCdbl(dr("DTPT"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalTPT).Value = clsCommon.myCdbl(dr("DTotal_TPT"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colEmptyValueShell).Value = clsCommon.myCdbl(dr("DEmpty_Value_Shell"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colEmptyValueBottle).Value = clsCommon.myCdbl(dr("DEmpty_Value_Bottle"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colCustDiscount).Value = clsCommon.myCdbl(dr("DCust_Discount"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalCustDiscount).Value = clsCommon.myCdbl(dr("DTotal_Cust_Discount"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCogs).Value = clsCommon.myCdbl(dr("DUnit_cogs"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount1).Value = clsCommon.myCdbl(dr("DPrice_Amount1"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount2).Value = clsCommon.myCdbl(dr("DPrice_Amount2"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount3).Value = clsCommon.myCdbl(dr("DPrice_Amount3"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount4).Value = clsCommon.myCdbl(dr("DPrice_Amount4"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount5).Value = clsCommon.myCdbl(dr("DPrice_Amount5"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount6).Value = clsCommon.myCdbl(dr("DPrice_Amount6"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount7).Value = clsCommon.myCdbl(dr("DPrice_Amount7"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount8).Value = clsCommon.myCdbl(dr("DPrice_Amount8"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount9).Value = clsCommon.myCdbl(dr("DPrice_Amount9"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount10).Value = clsCommon.myCdbl(dr("DPrice_Amount10"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colmainItem).Value = clsCommon.myCstr(dr("Main_item"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(coldiscountcode).Value = clsCommon.myCstr(dr("Discount_code"))
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(ColFromSchemeCode).Value = clsCommon.myCstr(dr("From_Scheme_Code"))

    '            If clsCommon.myCdbl(dr("IsFBRowExist")) = 0 Then
    '                Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(dr("DItem_Code")), "FB", Nothing)
    '                If dblConvFac > 1 Then
    '                    gv1.Rows.AddNew()
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("DItem_Code"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("DItem_Desc"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = 0
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReturnQty).Value = 0
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDate).Value = clsCommon.GetPrintDate(dr("DPrice_Date"), "dd/MM/yyyy")
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = "FB"
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCode).Value = clsCommon.myCstr(dr("DPrice_code"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = "N"
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeCodeQty).Value = 0
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeItem).Value = ""
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPromoSchemeApplicable).Value = "N"
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPromoSchemeCode).Value = ""
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeDiscApplicable).Value = "N"
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeCodeCash).Value = ""
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSamplingItem).Value = "N"
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRPAmt).Value = clsCommon.myCdbl(dr("DMRP_Amt")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBasicRate).Value = clsCommon.myCdbl(dr("DBasic_Rate")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemAssessableRate).Value = clsCommon.myCdbl(dr("DItem_Assessable_Rate")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiscAmt).Value = 0 ''clsCommon.myCdbl(dr("DDisc_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemNetAmt).Value = 0 ''clsCommon.myCdbl(dr("DItem_Net_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax1).Value = clsCommon.myCstr(dr("DTAX1"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = clsCommon.myCdbl(dr("DTAX1_Rate"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = clsCommon.myCdbl(dr("DTAX1_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt1).Value = 0 'clsCommon.myCdbl(dr("DTAX1_Assessable_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax2).Value = clsCommon.myCstr(dr("DTAX2"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = clsCommon.myCdbl(dr("DTAX2_Rate"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = 0 'clsCommon.myCdbl(dr("DTAX2_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt2).Value = 0 ' clsCommon.myCdbl(dr("DTAX2_Assessable_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax3).Value = clsCommon.myCstr(dr("DTAX3"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = clsCommon.myCdbl(dr("DTAX3_Rate"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = clsCommon.myCdbl(dr("DTAX3_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt3).Value = 0 ' clsCommon.myCdbl(dr("DTAX3_Assessable_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax4).Value = clsCommon.myCstr(dr("DTAX4"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = clsCommon.myCdbl(dr("DTAX4_Rate"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = 0 'clsCommon.myCdbl(dr("DTAX4_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt4).Value = 0 ' clsCommon.myCdbl(dr("DTAX4_Assessable_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax5).Value = clsCommon.myCstr(dr("DTAX5"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = clsCommon.myCdbl(dr("DTAX5_Rate"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = 0 'clsCommon.myCdbl(dr("DTAX5_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt5).Value = 0 ' clsCommon.myCdbl(dr("DTAX5_Assessable_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax6).Value = clsCommon.myCstr(dr("DTAX6"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = clsCommon.myCdbl(dr("DTAX6_Rate"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = 0 'clsCommon.myCdbl(dr("DTAX6_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt6).Value = 0 'clsCommon.myCdbl(dr("DTAX6_Assessable_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax7).Value = clsCommon.myCstr(dr("DTAX7"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = clsCommon.myCdbl(dr("DTAX7_Rate"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = 0 'clsCommon.myCdbl(dr("DTAX7_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt7).Value = 0 ' clsCommon.myCdbl(dr("DTAX7_Assessable_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax8).Value = clsCommon.myCstr(dr("DTAX8"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = clsCommon.myCdbl(dr("DTAX8_Rate"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = 0 'clsCommon.myCdbl(dr("DTAX8_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt8).Value = 0 'clsCommon.myCdbl(dr("DTAX8_Assessable_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax9).Value = clsCommon.myCstr(dr("DTAX9"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = clsCommon.myCdbl(dr("DTAX9_Rate"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = 0 'clsCommon.myCdbl(dr("DTAX9_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt9).Value = 0 ' clsCommon.myCdbl(dr("DTAX9_Assessable_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax10).Value = clsCommon.myCstr(dr("DTAX10"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = clsCommon.myCdbl(dr("DTAX10_Rate"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = 0 'clsCommon.myCdbl(dr("DTAX10_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAssessableAmt10).Value = 0 ' clsCommon.myCdbl(dr("DTAX10_Assessable_Amt"))

    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTax).Value = clsCommon.myCdbl(dr("DTAX1_Amt")) + clsCommon.myCdbl(dr("DTAX2_Amt")) + clsCommon.myCdbl(dr("DTAX3_Amt")) + clsCommon.myCdbl(dr("DTAX4_Amt")) + clsCommon.myCdbl(dr("DTAX5_Amt")) + clsCommon.myCdbl(dr("DTAX6_Amt")) + clsCommon.myCdbl(dr("DTAX7_Amt")) + clsCommon.myCdbl(dr("DTAX8_Amt")) + clsCommon.myCdbl(dr("DTAX9_Amt")) + clsCommon.myCdbl(dr("DTAX10_Amt"))
    '                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTax).Value = 0 'Math.Round(clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTax).Value), 3, MidpointRounding.ToEven)

    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssessableAmt).Value = 0 'clsCommon.myCdbl(dr("DTotal_Assessable_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalMRPAmt).Value = 0 ' clsCommon.myCdbl(dr("DTotal_MRP_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalBasicAmt).Value = 0 'clsCommon.myCdbl(dr("DTotal_Basic_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalDiscAmt).Value = 0 ' clsCommon.myCdbl(dr("DTotal_Disc_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalnetAmt).Value = 0 'clsCommon.myCdbl(dr("DTotal_net_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalTaxAmt).Value = 0 'clsCommon.myCdbl(dr("DTotal_Tax_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalItemAmt).Value = 0 'clsCommon.myCdbl(dr("DTotal_Item_Amt"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEmptyValue).Value = 0 'clsCommon.myCdbl(dr("DEmpty_Value")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTPT).Value = clsCommon.myCdbl(dr("DTPT")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalTPT).Value = 0 'clsCommon.myCdbl(dr("DTotal_TPT"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEmptyValueShell).Value = 0 ' clsCommon.myCdbl(dr("DEmpty_Value_Shell")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEmptyValueBottle).Value = clsCommon.myCdbl(dr("DEmpty_Value_Bottle")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustDiscount).Value = clsCommon.myCdbl(dr("DCust_Discount")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalCustDiscount).Value = 0 'clsCommon.myCdbl(dr("DTotal_Cust_Discount"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCogs).Value = clsCommon.myCdbl(dr("DUnit_cogs")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount1).Value = clsCommon.myCdbl(dr("DPrice_Amount1")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount2).Value = clsCommon.myCdbl(dr("DPrice_Amount2")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount3).Value = clsCommon.myCdbl(dr("DPrice_Amount3")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount4).Value = clsCommon.myCdbl(dr("DPrice_Amount4")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount5).Value = clsCommon.myCdbl(dr("DPrice_Amount5")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount6).Value = clsCommon.myCdbl(dr("DPrice_Amount6")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount7).Value = clsCommon.myCdbl(dr("DPrice_Amount7")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount8).Value = clsCommon.myCdbl(dr("DPrice_Amount8")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount9).Value = clsCommon.myCdbl(dr("DPrice_Amount9")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceamount10).Value = clsCommon.myCdbl(dr("DPrice_Amount10")) / dblConvFac
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colmainItem).Value = "" 'clsCommon.myCstr(dr("Main_item"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(coldiscountcode).Value = clsCommon.myCstr(dr("Discount_code"))
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColFromSchemeCode).Value = clsCommon.myCstr(dr("From_Scheme_Code"))
    '                End If
    '            End If
    '        Next
    '    End If
    '    isInsideLoadData = False
    'End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv1.Columns(colLeakQty)) Then
                    gv1.CurrentRow.Cells(colLeakQty).ReadOnly = IIf(clsCommon.myCdbl(gv1.CurrentRow.Cells(colEmptyValueBottle).Value) > 0, False, True)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRecreateJournalEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecreateJournalEntry.Click
        If clsCommon.myLen(txtDocNo.Value) > 0 AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
            If common.clsCommon.MyMessageBoxShow("Recreate Journal Entry" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim obj As clsSalesReturnHead = clsSalesReturnHead.GetData(txtDocNo.Value, NavigatorType.Current, trans)
                    If (obj Is Nothing OrElse clsCommon.myLen(obj.Sale_Return_No) <= 0) Then
                        Throw New Exception("No Data found to Create Journal entry")
                    End If
                    Dim strVouccherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='SD-SR' and Source_Doc_No='" + txtDocNo.Value + "'", trans)

                    If clsCommon.myLen(strVouccherNo) <= 0 Then
                        Throw New Exception("Journal voucher no not found to recreate journal Enry")
                    End If

                    clsSalesReturnHead.CreateJournalEntry(strVouccherNo, obj, trans)
                    trans.Commit()

                    common.clsCommon.MyMessageBoxShow("Journal Entry created successfully", Me.Text)
                Catch ex As Exception
                    trans.Rollback()
                    common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
        End If
    End Sub

    Private Sub btnReverseAndRecreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseAndRecreate.Click
        If clsCommon.myLen(txtDocNo.Value) > 0 AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
            If common.clsCommon.MyMessageBoxShow("Reverse and Recreate Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim obj As clsSalesReturnHead = clsSalesReturnHead.GetData(txtDocNo.Value, NavigatorType.Current, trans)
                    If (obj Is Nothing OrElse clsCommon.myLen(obj.Sale_Return_No) <= 0) Then
                        Throw New Exception("No Data found to Create Journal entry")
                    End If
                    Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='SD-SR' and Source_Doc_No='" + txtDocNo.Value + "'", trans)
                    If clsCommon.myLen(VoucherNo) <= 0 Then
                        Throw New Exception("Journal voucher no not found to recreate journal Enry")
                    End If

                    Dim Qry As String = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                    Qry = "select InOut,Trans_Type,Item_Code,Item_Desc,Location_Code,case when InOut='I' then -1 else 1 end *Qty as Qty ,UOM,MRP,ItemType,case when InOut='I' then -1 else 1 end* Basic_Cost as Basic_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + txtDocNo.Value + "' and Trans_Type='Sale Return'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
                    Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)
                    For Each objtr As DataRow In dt.Rows
                        Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(objtr("Item_Code")), clsCommon.myCstr(objtr("UOM")), trans)
                        Dim objLocationDetails As New clsItemLocationDetails()
                        objLocationDetails.Item_Code = clsCommon.myCstr(objtr("Item_Code"))
                        objLocationDetails.Item_Desc = clsCommon.myCstr(objtr("Item_Desc"))
                        objLocationDetails.Location_Code = clsCommon.myCstr(objtr("Location_Code"))
                        objLocationDetails.Location_Desc = clsLocation.GetName(objLocationDetails.Location_Code, trans)
                        objLocationDetails.Item_Qty = clsCommon.myCdbl(objtr("Qty")) / dblConvFac
                        objLocationDetails.Amount = clsCommon.myCdbl(objtr("Basic_Cost"))
                        objLocationDetails.MRP = clsCommon.myCdbl(objtr("MRP")) * dblConvFac
                        objLocationDetails.ItemType = clsCommon.myCstr(objtr("ItemType"))
                        ArrLocationDetails.Add(objLocationDetails)
                    Next
                    Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                    clsItemLocationDetails.SaveData(strPostDate, ArrLocationDetails, trans)

                    Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + txtDocNo.Value + "' and Trans_Type='Sale Return'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                    Qry = "Update TSPL_SALE_RETURN_HEAD set Is_Post = null where Sale_Return_No='" + txtDocNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    Xtra.UpdateSaleInvoiceBalanceAmt(trans)
                    trans.Commit()

                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                Catch ex As Exception
                    trans.Rollback()
                    common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
        End If
    End Sub

    'Container Deposit error scripts
    '    insert into  xxx_Sale_Return 
    'select distinct TSPL_SALE_RETURN_DETAIL.Sale_Return_No from TSPL_SALE_RETURN_DETAIL 
    'left outer join TSPL_SALE_RETURN_HEAD on TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_RETURN_DETAIL.Sale_Return_No
    'where ( isnull(TSPL_SALE_RETURN_DETAIL.Leak_Qty,0)>0 or isnull(TSPL_SALE_RETURN_DETAIL.Burst_Qty,0)>0 or isnull(TSPL_SALE_RETURN_DETAIL.Short_Qty,0)>0
    ') 
    'and (ISNULL( TSPL_SALE_RETURN_DETAIL.Empty_Value_Bottle,0)+isnull(TSPL_SALE_RETURN_DETAIL.Empty_Value_Shell,0))>0 and TSPL_SALE_RETURN_HEAD.Is_Post='Y'


    'select * from xxx_Sale_Return


    'update TSPL_SALE_RETURN_DETAIL set TSPL_SALE_RETURN_DETAIL.Basic_Rate=xxx.Basic_Rate,TSPL_SALE_RETURN_DETAIL.Item_Net_Amt=xxx.Item_Net_Amt From (

    'select Basic_Rate,Item_Net_Amt,Item_Code,Price_code,Unit_code,Price_Date,MRP_Amt,(select top 1 Sale_Return_No from TSPL_SALE_RETURN_HEAD where Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No) as ReturnNo from TSPL_SALE_INVOICE_DETAIL where Sale_Invoice_No in(
    'select distinct TSPL_SALE_RETURN_HEAD.Invoice_No from TSPL_SALE_RETURN_DETAIL 
    'left outer join TSPL_SALE_RETURN_HEAD on TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_RETURN_DETAIL.Sale_Return_No
    'where ( isnull(TSPL_SALE_RETURN_DETAIL.Leak_Qty,0)>0 or isnull(TSPL_SALE_RETURN_DETAIL.Burst_Qty,0)>0 or isnull(TSPL_SALE_RETURN_DETAIL.Short_Qty,0)>0
    ') and (ISNULL( TSPL_SALE_RETURN_DETAIL.Empty_Value_Bottle,0)+isnull(TSPL_SALE_RETURN_DETAIL.Empty_Value_Shell,0))>0)
    ')xxx
    'inner join TSPL_SALE_RETURN_DETAIL on TSPL_SALE_RETURN_DETAIL.Sale_Return_No=xxx.ReturnNo 
    'and TSPL_SALE_RETURN_DETAIL.Item_Code=xxx.Item_Code 
    'and TSPL_SALE_RETURN_DETAIL.Price_code=xxx.Price_code 
    'and TSPL_SALE_RETURN_DETAIL.Unit_code=xxx.Unit_code 
    'and TSPL_SALE_RETURN_DETAIL.Price_Date=xxx.Price_Date 
    'and TSPL_SALE_RETURN_DETAIL.MRP_Amt=xxx.MRP_Amt


    'select * from xxx_Sale_Return
    'where docNo not in (
    'select * from xxx_Sale_Return_Complete)
    'End of Container Deposit error scripts

    Private Sub BntContainerDeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BntContainerDeposit.Click
        Try
            Dim qry As String = "select DocNo from xxx_Sale_Return where not exists(select 1 from xxx_Sale_Return_Complete where xxx_Sale_Return_Complete.DocNo=xxx_Sale_Return.docNo)"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If common.clsCommon.MyMessageBoxShow("Pending Task " + clsCommon.myCstr(dt.Rows.Count) + Environment.NewLine + "Do you want to Continue", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    For Each dr As DataRow In dt.Rows
                        Try
                            Dim strReturnNo As String = clsCommon.myCstr(dr("DocNo"))
                            Try

                                LoadData(strReturnNo, NavigatorType.Current)
                                btnSave.Text = "Save"
                                SaveData(True)

                                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                                Try
                                    Dim obj As clsSalesReturnHead = clsSalesReturnHead.GetData(txtDocNo.Value, NavigatorType.Current, trans)
                                    If (obj Is Nothing OrElse clsCommon.myLen(obj.Sale_Return_No) <= 0) Then
                                        Throw New Exception("No Data found to Create Journal entry")
                                    End If
                                    Dim strVouccherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='SD-SR' and Source_Doc_No='" + txtDocNo.Value + "'", trans)

                                    If clsCommon.myLen(strVouccherNo) <= 0 Then
                                        Throw New Exception("Journal voucher no not found to recreate journal Enry")
                                    End If

                                    clsSalesReturnHead.CreateJournalEntry(strVouccherNo, obj, trans)
                                    clsDBFuncationality.ExecuteNonQuery("insert into xxx_Sale_Return_Complete values('" + strReturnNo + "')", trans)
                                    trans.Commit()
                                Catch ex As Exception
                                    trans.Rollback()
                                    Throw New Exception(ex.Message)
                                End Try

                            Catch ex As Exception
                                Throw New Exception(ex.Message)
                            End Try
                        Catch ex As Exception
                            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
                        End Try
                    Next
                    common.clsCommon.MyMessageBoxShow("Task Completed", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class
