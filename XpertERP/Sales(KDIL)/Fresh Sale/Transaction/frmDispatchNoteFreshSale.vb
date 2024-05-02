
'by vipin for some feild hidden on 07/03/2013 
'---preeti Gupta---Ticket No.-BM00000003015--
''updation by richa agarwal against ticket no BM00000004376
Imports common
Imports System.Globalization
Imports System.Data.SqlClient

Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net

Public Class frmDispatchNoteFreshSale
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim CreateFreshInvoiceOnDispatchSave As Integer = 0
    Private strPost As Boolean = False
    Public strTransporter As String = ""
    Public strPaymentType As String = ""
    Public strPaymentRate As String = ""
    Public strCharges As String = ""
    Public dblTotalAmt As Double = 0
    Const colSampling As String = "colSampling"
    Private blnChangeCustomer As Boolean = False
    Private StrSql As String
    Private AutoScheme As Boolean = False
    Private attachQry As String = ""
    Private blnBackCalculation As Boolean = False
    Private AllowChangeInvoiceType As Boolean = False
    Private IsBatchMFDEXDmandatory As Boolean = False
    Private PurchaseOneItemOneVendor As Boolean = False
    Private ItemRateEditable As Boolean = False
    Private ItemMRPEditable As Boolean = False

    Public strExcise As Boolean
    Public intMRPwithabatement As Integer
    Private isPO_GRN_MRN_Editable As Boolean = False
    Public Const RowTypeItem As String = "Item"
    Public Const RowTypeMisc As String = "Misc"

    Const colOrgUnit As String = "COLORGUNIT"
    Const ReportID As String = "DispatchFSItemGrid"
    Public strSRNno As String = Nothing
    Private isCellValueChangedOpen As Boolean = False
    Private isCellValueChangedTaxOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colRowType As String = "COLTYPE"
    Const colComplete As String = "COMPLETE"
    Const colBalanceQty As String = "BALANCEQTY"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colIHSN As String = "colIHSN"
    Const colBarCode As String = "COLBARCODE"
    Const colPendingQty As String = "COLPENDINGQTY"
    'Const colOrgSOQty As String = "COLORGSOQTY"
    Const colQty As String = "COLQTY"
    Const colDelQty As String = "colDelQty"

    Const colFreeQty As String = "COLFREEQTY"
    Const colUnit As String = "COLUNIT"
    Const colRate As String = "COLRATE"
    Const colAmt As String = "COLAMT"
    Const colDisPer As String = "COLDISPER"
    Const colDisAmt As String = "COLDISAMT"
    Const colAmtAfterDis As String = "COLAMTAFTERDIS"
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

    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"
    Const colOrderNo As String = "ORDERNO"
    Const colHeaDDisPer As String = "colHeaDDisPer"
    Const colHeadDisPerAmt As String = "colHeadDisPerAmt"


    ''Const colLocationCode As String = "LOCATIONCODE"
    ''Const colLocationName As String = "LOCATIONNAME"


    Const colMRP As String = "MRP"
    '' ''Const colAssessableRate As String = "ASSESSABLERATE"
    '' ''Const colAssessableAmount As String = "ASSESSABLEAMT"
    Const colBatchNo As String = "BATCHNO"
    Const colExpiry As String = "EXPIRYDATE"
    Const colManufactureDate As String = "MANUFACTUREDATE"
    Const colLandedRate As String = "LANDEDRATE"
    Const colLandedAmt As String = "LANDEDAMT"
    Const colSpecification As String = "SPECIFICATION"
    Const colRemarks As String = "REMARKS"
    Const colIsMannualAmt As String = "ISMANNUALAMT"
    Const colIsSerialseItem As String = "COLISSERIALSEITEM"


    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    'Const colTIsTaxable As String = "ISTAXABLE"
    Const colTTaxRate As String = "TAXRATE"
    'Const colTIsSurTax As String = "ISSURTAX"
    'Const colTSurTaxCode As String = "SURTAXCODE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"



    Const colItmCost As String = "ItmCost"

    Const colACCode As String = "COLACCODE"
    Const colACName As String = "COLACNAME"
    Const colACAmount As String = "COLACAMOUNT"


    Const colIsEmptyValue As String = "ISEMPTYVALUE"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim repoBalQty As GridViewDecimalColumn
    Dim repoComplete As GridViewTextBoxColumn

    Public DocumentNo As String = Nothing


    Const colBinNo As String = "colBinNo"
    Const colPricipleCode As String = "colPricipleCode"
    Const colPricipleDesc As String = "colPricipleDesc"
    Const colvendorCode As String = "colvendorCode"
    Const colvendorDesc As String = "colvendorDesc"
    Const ColActualBalQty As String = "ColActualBalQty"
    Const colPriceDateColumn As String = "pricedate"
    Const colItemWeight As String = "colItemWeight"
    Const colConvF As String = "colConvF"
    Const colTotItemWt As String = "colTotItemWt"
    Const ColFOC As String = "ColFOC"
    Const colSchemeApplicable As String = "colSchemeApplicable"
    Const colDiscountAmount As String = "colDiscountAmount"
    Const colcustDiscount As String = "colcustDiscount"
    Const colActualCost As String = "colActualCost"
    Const colTotalMRP As String = "totalMRP"
    Const colTotalBasicAmount As String = "totalBasicAmount"
    Const colTotalDiscountAmount As String = "totalDiscountAmount"
    Const colTotalCustDiscount As String = "totalCustDiscount"
    Const colSchemeItem As String = "colSchemeItem"
    Const colFromSchemeCode As String = "colFromSchemeCode"
    Const ColCustDiscountQty As String = "ColCustDiscountQty"
    Const colAbatementPer As String = "colAbatementPer"
    Const colAbatementAmount As String = "colAbatementamount"
    Const colPriceCOde As String = "colPriceCOde"
    Const colMarkUpPercentage As String = "colMarkUpPercentage"
    Const colLandingCost As String = "colLandingCost"
    Const colMarkupOn As String = "colMarkupOn"
    Const colCustDiscPercentage As String = "colCustDiscPercentage"
    Const colCashDiscSchemeCode As String = "colCashDiscSchemeCode"
    Const colHeadDiscamt As String = "colHeadDiscamt"
    Const colPurCost As String = "colPurCost"
    Const colOrgCost As String = "colOrgCost"
    Const colCrate As String = "colCrate"

    Const colCashSchemeCode As String = "CashSchemeCode"
    Const colCashSchemeType As String = "CashSchType"
    Const colCash_Pers As String = "CashScPers"
    Const colCash_Amt As String = "CashSc_Amt"
    Const colSchmCodeType As String = "SchmCodeType"
    Const colMainIcode As String = "MainIcode"
    Const colMainIQty As String = "MainIQty"
    Const colMainIUOM As String = "MainIUOM"
    Private ConvFactMsg As Boolean = False
    Dim atchqry As String = ""
    Public IsDataImported As Boolean = False
    Public gvExcel As New RadGridView
    Public row_index As Integer
    Public DtExcel As DataTable
#End Region

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmDispatchFreshSale)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isReverse Then
            btnReverseAndUnpost.Enabled = True
        Else
            btnReverseAndUnpost.Enabled = False
        End If
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnSave.Visible = False
        btnPost.Visible = False
        SetUserMgmtNew()
        btnHistory.Enabled = False
        SetMailRight()
        ItemRateEditable = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IsItemRateEditableOnSales & "'")) = 0, False, True)
        ItemMRPEditable = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IsItemMRPEditableOnSales & "'")) = 0, False, True)
        CreateFreshInvoiceOnDispatchSave = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateFreshInvoiceOnDispatchSave, clsFixedParameterCode.CreateFreshInvoiceOnDispatchSave, Nothing))
        AutoScheme = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AutoSchemeOn & "'")) = 0, False, True)
        blnBackCalculation = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsRateBackCalculation from TSPL_inv_parameters")) = 0, False, True)
        PurchaseOneItemOneVendor = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='PurchaseOneItemOneVendor'")) = 0, False, True)
        intMRPwithabatement = clsDBFuncationality.getSingleValue("select IsMRPwithAbatement from TSPL_INV_PARAMETERS")
        IsBatchMFDEXDmandatory = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsBatchNo_MFD_EXD_Mandatory from TSPL_inv_parameters")) = 0, False, True)
        AllowChangeInvoiceType = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Allow_Change_InvoiceType from TSPL_inv_parameters")) = 0, False, True)
        isPO_GRN_MRN_Editable = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isMRNQtyEdiatableOnSRN from TSPL_inv_parameters")) = 0, False, True)
        dtpChallan.Value = clsCommon.GETSERVERDATE
        dtpInvoice.Value = clsCommon.GETSERVERDATE
        chkVendorGrossReceipt.Visible = False
        txtVendorNo.MendatroryField = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A Create Additional Cost")
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadBlankGrid()
        LoadBlankGridTax()
        LoadItemType()
        LoadInvoiceType()
        LoadBlankGridAC()
        AddNew()
        SetLength()
        If clsCommon.myLen(strSRNno) > 0 Then
            LoadData(strSRNno, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        chkRateDefaultSetting.ToggleState = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SalesRateEditable, clsFixedParameterCode.SalesRateEditable, Nothing)) = 1, ToggleState.On, ToggleState.Off)

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = False

        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields

        '' MultiCurrency
        SetMultiCurrencyVisibility()
        '' End of MultiCurrency
        'txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        'If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
        '    lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        'End If
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
        ''richa agarwal against ticket no BM00000004375
        RadPageView1.Pages(1).Item.Visibility = ElementVisibility.Collapsed
        ''---------------------------------
        If IsDataImported = True Then
            Me.CenterToParent()
            If LoadImportData(gvExcel, row_index, DtExcel) Then
                Me.Close()
            End If
        End If
        RadMenuItem5.Visibility = ElementVisibility.Collapsed
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
                strq = "select currency_code from TSPL_CUSTOMER_MASTER where cust_code='" & clsCommon.myCstr(Me.txtVendorNo.Value) & "'"
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
        txtComment.MaxLength = 200
        txtRefNo.MaxLength = 50
        txtCarrier.MaxLength = 50
        txtGRNo.MaxLength = 50
        txtGENo.MaxLength = 50

        txtPONo.MaxLength = 200

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
        cboItemType.DataSource = clsItemMaster.GetItemType()
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
        cboItemType.SelectedIndex = 2
        cboItemType.Visible = False
        RadLabel29.Visible = False
    End Sub

    Sub BlankAllControls()
        fndBookingNo.Value = ""
        lblBookingDate.Text = ""
        txtVehicleCapacity.Text = ""
        txtLorryNo.Text = ""
        txtRoadPermitNo.Text = ""
        txtTransporterName.Text = ""
        txtFreightAmt.Text = ""
        lblTotalWeight.Text = ""
        txtComment.Text = ""
        lblTotRAmt1.Text = ""
        txtCrateQty.Value = 0
        ddlFreight.Text = "Party"

        txtDiscAmt.Text = 0
        txtDiscPer.Text = 0
        lblDiscountAmt.Text = 0
        lblInvoiceDiscAmt.Text = 0
        chkDiscountOnRate.IsChecked = True
        txtpodate.Text = ""
        txtForm38.Text = ""
        txtPONo.Text = ""
        txtPriceGroupCode.Text = ""
        chkAutoTransfer.Checked = False
        txtFromLoc.Value = ""
        ddlInvoiceType.SelectedValue = ""
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
        lblTotRAmt1.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtCarrier.Text = ""
        txtVehicleCode.Value = ""
        lblVehicleNo.Text = ""
        txtGRNo.Text = ""
        txtGENo.Text = ""
        txtGEDate.Checked = False
        txtGEDate.Value = txtDate.Value

        txtDept.Value = ""
        lblDept.Text = ""
        cboItemType.SelectedIndex = 2
        cboItemType.Enabled = True
        txtBillToLocation.Enabled = True
        txtReqNo.Value = ""
        chkVendorGrossReceipt.Checked = False
        lblAddCharges1.Text = ""
        lblAddCharges1.Text = ""
        rbtnTaxCalAutomatic.IsChecked = True
        txtSalesman.Value = ""
        lblSalesman.Text = ""
        ' -----------richa 27/06/2014 Ticket No .BM00000002982-----------
        txtInvoiceNo.Text = ""
        txtMannaulInvoiceNo.Value = 0
        TxtInvoiceManualNoWithPrefix.Text = ""
        '--------------------------------------------------------------
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        fndProject.Value = ""
        lblProject.Text = ""
        txtPriceCode.Text = ""
        txtRouteNo.Value = ""
        lblRouteDesc.Text = ""
    End Sub

    Public Shared Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = RowTypeItem
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = RowTypeMisc
        dt.Rows.Add(dr)

        Return dt
    End Function

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
        repoRowType.IsVisible = False
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

        Dim repoIHSN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIHSN.FormatString = ""
        repoIHSN.HeaderText = "HSN Code"
        repoIHSN.Name = colIHSN
        repoIHSN.Width = 150
        repoIHSN.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIHSN)

        Dim repoPriceDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoPriceDate.Format = DateTimePickerFormat.Custom
        repoPriceDate.CustomFormat = "dd-MM-yyyy"
        repoPriceDate.HeaderText = "Price Date"
        repoPriceDate.WrapText = True
        repoPriceDate.FormatString = "{0:d}"
        repoPriceDate.Name = colPriceDateColumn
        repoPriceDate.ReadOnly = True
        repoPriceDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoPriceDate)


        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.FormatString = ""
        repoPriceCode.HeaderText = "Price Code"
        repoPriceCode.Name = colPriceCOde
        repoPriceCode.IsVisible = False
        repoPriceCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPriceCode)

        Dim repoBarcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBarcode.FormatString = ""
        repoBarcode.HeaderText = "BAR Code"
        repoBarcode.Name = colBarCode
        repoBarcode.IsVisible = False
        repoBarcode.ReadOnly = True
        repoBarcode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoBarcode)

        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending Quantity"
        repoPendingQty.Name = colPendingQty
        repoPendingQty.IsVisible = False
        repoPendingQty.Minimum = 0
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPendingQty.ReadOnly = True
        repoPendingQty.IsVisible = False
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
        repoBalQty.IsVisible = False
        repoBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBalQty)

        'Dim repoOrgSRNQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoOrgSRNQty.FormatString = ""
        'repoOrgSRNQty.WrapText = True
        'repoOrgSRNQty.HeaderText = "Order Qty"
        'repoOrgSRNQty.Name = colOrgSOQty
        'repoOrgSRNQty.Width = 80
        'repoOrgSRNQty.Minimum = 0
        'repoOrgSRNQty.ReadOnly = Not isPO_GRN_MRN_Editable
        'repoOrgSRNQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.MasterTemplate.Columns.Add(repoOrgSRNQty)
        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 80
        repoUnit.ReadOnly = False
        repoUnit.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoUnit.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoUnit)


        Dim repoOrgUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOrgUnit.FormatString = ""
        repoOrgUnit.HeaderText = "ORG UOM"
        repoOrgUnit.Name = colOrgUnit
        repoOrgUnit.Width = 80
        repoOrgUnit.ReadOnly = False
        repoOrgUnit.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoOrgUnit)

        Dim repoDelQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDelQty = New GridViewDecimalColumn()
        repoDelQty.FormatString = ""
        repoDelQty.HeaderText = "Deliver Qty"
        repoDelQty.Name = colDelQty
        repoDelQty.Width = 80
        repoDelQty.Minimum = 0
        repoDelQty.ReadOnly = True
        repoDelQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDelQty)


        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Dispatch Qty"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        '' Anubhooti 11-Sep-2014 BM00000003847
        Dim repoCrate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCrate.FormatString = ""
        repoCrate.HeaderText = "Crate"
        repoCrate.Name = colCrate
        'repoCrate.IsVisible = False
        repoCrate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCrate)
        ''

        Dim repoActualBalQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActualBalQty.FormatString = ""
        repoActualBalQty.HeaderText = "Actual Balance"
        repoActualBalQty.Name = ColActualBalQty
        repoActualBalQty.Width = 80
        repoActualBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoActualBalQty.ReadOnly = True
        repoActualBalQty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoActualBalQty)

        Dim repoSchemeApp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeApp.AllowSort = False
        repoSchemeApp.FormatString = ""
        repoSchemeApp.HeaderText = "App. Qty Dis."
        repoSchemeApp.Name = colSchemeApplicable
        repoSchemeApp.ReadOnly = True
        repoSchemeApp.Width = 75
        gv1.MasterTemplate.Columns.Add(repoSchemeApp)

        Dim repoItemWt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemWt = New GridViewDecimalColumn()
        repoItemWt.FormatString = ""
        repoItemWt.HeaderText = "Item Weight"
        repoItemWt.Name = colItemWeight
        repoItemWt.Width = 80
        repoItemWt.Minimum = 0
        repoItemWt.ReadOnly = False
        repoItemWt.IsVisible = False
        repoItemWt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemWt)

        Dim repoConv As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoConv = New GridViewDecimalColumn()
        repoConv.FormatString = ""
        repoConv.HeaderText = "Conv. Factor"
        repoConv.Name = colConvF
        repoConv.Width = 80
        repoConv.Minimum = 0
        repoConv.ReadOnly = False
        repoConv.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoConv)

        Dim repoTotItemWt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotItemWt = New GridViewDecimalColumn()
        repoTotItemWt.FormatString = ""
        repoTotItemWt.HeaderText = "Tot Item Weight"
        repoTotItemWt.Name = colTotItemWt
        repoTotItemWt.Width = 80
        repoTotItemWt.Minimum = 0
        repoTotItemWt.ReadOnly = False
        repoTotItemWt.IsVisible = False
        repoTotItemWt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotItemWt)


        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = False
        repoMRP.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoMRP)


        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Basic Rate"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoOrgRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrgRate = New GridViewDecimalColumn()
        repoOrgRate.FormatString = ""
        repoOrgRate.HeaderText = "Org Basic Rate"
        repoOrgRate.Name = colOrgCost
        repoOrgRate.Width = 80
        repoOrgRate.Minimum = 0
        repoOrgRate.ReadOnly = True
        repoOrgRate.IsVisible = False
        repoOrgRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgRate)

        Dim repoSamplingItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSamplingItem.AllowSort = False
        repoSamplingItem.HeaderText = "Sampling"
        repoSamplingItem.Name = colSampling
        repoSamplingItem.ReadOnly = True
        repoSamplingItem.Width = 96
        gv1.MasterTemplate.Columns.Add(repoSamplingItem)

        Dim repoIsSchmItem13 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoIsSchmItem13.FormatString = ""
        repoIsSchmItem13.HeaderText = "Scheme Type"
        repoIsSchmItem13.Name = colSchmCodeType
        repoIsSchmItem13.Width = 50
        repoIsSchmItem13.ReadOnly = False
        repoIsSchmItem13.DataSource = clsSchemeApplyOnDairy.GetSchemeTypes()
        repoIsSchmItem13.DisplayMember = "Code"
        repoIsSchmItem13.ValueMember = "Name"
        gv1.MasterTemplate.Columns.Add(repoIsSchmItem13)

        Dim repoQtySchemeItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQtySchemeItem.AllowSort = False
        repoQtySchemeItem.HeaderText = "Qty Scheme Item"
        repoQtySchemeItem.Name = colSchemeItem
        repoQtySchemeItem.ReadOnly = True
        repoQtySchemeItem.Width = 96
        gv1.MasterTemplate.Columns.Add(repoQtySchemeItem)

        Dim repoFromSchemeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFromSchemeCode.HeaderText = "Scheme Code"
        repoFromSchemeCode.Name = colFromSchemeCode
        repoFromSchemeCode.Width = 80
        repoFromSchemeCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFromSchemeCode)

        Dim repoIsSchmItem3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem3.FormatString = ""
        repoIsSchmItem3.HeaderText = "Main Item Code"
        repoIsSchmItem3.Name = colMainIcode
        repoIsSchmItem3.Width = 50
        repoIsSchmItem3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoIsSchmItem3)

        Dim repoIsSchmItem4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem4.FormatString = ""
        repoIsSchmItem4.HeaderText = "Main Item UOM"
        repoIsSchmItem4.Name = colMainIUOM
        repoIsSchmItem4.Width = 50
        repoIsSchmItem4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoIsSchmItem4)

        Dim repoIsSchmItem5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem5.FormatString = ""
        repoIsSchmItem5.HeaderText = "Main Item Qty"
        repoIsSchmItem5.Name = colMainIQty
        repoIsSchmItem5.Width = 50
        repoIsSchmItem5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoIsSchmItem5)

        Dim repoIsSchmItem1c As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem1c.FormatString = ""
        repoIsSchmItem1c.HeaderText = "Cash Scheme Code"
        repoIsSchmItem1c.Name = colCashSchemeCode
        repoIsSchmItem1c.Width = 50
        repoIsSchmItem1c.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIsSchmItem1c)

        Dim repoIsSchmItem1c1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem1c1.FormatString = ""
        repoIsSchmItem1c1.HeaderText = "Cash Scheme Type"
        repoIsSchmItem1c1.Name = colCashSchemeType
        repoIsSchmItem1c1.Width = 50
        repoIsSchmItem1c1.ReadOnly = True
        repoIsSchmItem1c1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoIsSchmItem1c1)

        Dim repoIsSchmItem1c2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIsSchmItem1c2.FormatString = ""
        repoIsSchmItem1c2.HeaderText = "Cash %"
        repoIsSchmItem1c2.Name = colCash_Pers
        repoIsSchmItem1c2.Width = 50
        repoIsSchmItem1c2.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIsSchmItem1c2)

        Dim repoIsSchmItem1c3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIsSchmItem1c3.FormatString = ""
        repoIsSchmItem1c3.HeaderText = "Cash Amount"
        repoIsSchmItem1c3.Name = colCash_Amt
        repoIsSchmItem1c3.Width = 50
        repoIsSchmItem1c3.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIsSchmItem1c3)


        Dim repoFreeQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFreeQty.FormatString = ""
        repoFreeQty.HeaderText = "Free Quantity"
        repoFreeQty.Name = colFreeQty
        repoFreeQty.IsVisible = False
        repoFreeQty.Width = 80
        repoFreeQty.Minimum = 0
        repoFreeQty.IsVisible = False
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



        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Extended Cost"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = False
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        Dim repoAbatementRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAbatementRate = New GridViewDecimalColumn()
        repoAbatementRate.FormatString = ""
        repoAbatementRate.HeaderText = "Abatement %"
        repoAbatementRate.Name = colAbatementPer
        repoAbatementRate.Width = 80
        repoAbatementRate.Minimum = 0
        repoAbatementRate.ReadOnly = False
        repoAbatementRate.IsVisible = False
        repoAbatementRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAbatementRate)

        Dim repoAbatementamount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAbatementamount = New GridViewDecimalColumn()
        repoAbatementamount.FormatString = ""
        repoAbatementamount.HeaderText = "Abatement Amount"
        repoAbatementamount.Name = colAbatementAmount
        repoAbatementamount.Width = 80
        repoAbatementamount.Minimum = 0
        repoAbatementamount.ReadOnly = False
        repoAbatementamount.IsVisible = False
        repoAbatementamount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAbatementamount)

        Dim repoDisPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisPer = New GridViewDecimalColumn()
        repoDisPer.FormatString = ""
        repoDisPer.HeaderText = "Discount %"
        repoDisPer.Minimum = 0
        repoDisPer.Name = colDisPer
        repoDisPer.Width = 100
        repoDisPer.Maximum = 100
        repoDisPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDisPer)

        Dim repoDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDisAmt
        repoDisAmt.Width = 80
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDisAmt)

        Dim repoHDisPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoHDisPer = New GridViewDecimalColumn()
        repoHDisPer.FormatString = ""
        repoHDisPer.HeaderText = "Head Discount %"
        repoHDisPer.Minimum = 0
        repoHDisPer.Name = colHeaDDisPer
        repoHDisPer.Width = 100
        repoHDisPer.ReadOnly = True
        repoHDisPer.IsVisible = True
        repoHDisPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoHDisPer)

        Dim repoHDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoHDisAmt = New GridViewDecimalColumn()
        repoHDisAmt.FormatString = ""
        repoHDisAmt.HeaderText = "Head Discount % Amount"
        repoHDisAmt.WrapText = True
        repoHDisAmt.Name = colHeadDisPerAmt
        repoHDisAmt.Width = 80
        repoHDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoHDisAmt.VisibleInColumnChooser = False
        repoHDisAmt.ReadOnly = True
        repoHDisAmt.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoHDisAmt)

        Dim repoHeadDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoHeadDisAmt = New GridViewDecimalColumn()
        repoHeadDisAmt.FormatString = ""
        repoHeadDisAmt.HeaderText = "Head Disc Amt"
        repoHeadDisAmt.WrapText = True
        repoHeadDisAmt.Name = colHeadDiscamt
        repoHeadDisAmt.Width = 80
        repoHeadDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoHeadDisAmt.VisibleInColumnChooser = False
        repoHeadDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHeadDisAmt)


        Dim repoCustDiscountQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDiscountQty.HeaderText = "Cash Dis Qty."
        repoCustDiscountQty.MinWidth = 4
        repoCustDiscountQty.Name = ColCustDiscountQty
        repoCustDiscountQty.ReadOnly = True
        repoCustDiscountQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCustDiscountQty.Width = 54
        gv1.MasterTemplate.Columns.Add(repoCustDiscountQty)

        Dim repoCustDiscountPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDiscountPer.HeaderText = "Cash Dis %."
        repoCustDiscountPer.MinWidth = 4
        repoCustDiscountPer.Name = colCustDiscPercentage
        repoCustDiscountPer.ReadOnly = True
        repoCustDiscountPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCustDiscountPer.Width = 54
        gv1.MasterTemplate.Columns.Add(repoCustDiscountPer)


        Dim repoCustDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDiscount.HeaderText = "Cash Dis Amt."
        repoCustDiscount.MinWidth = 4
        repoCustDiscount.Name = colcustDiscount
        repoCustDiscount.ReadOnly = True
        repoCustDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCustDiscount.Width = 54
        gv1.MasterTemplate.Columns.Add(repoCustDiscount)

        Dim repoCashSchemeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCashSchemeCode.HeaderText = "Cash Scheme Code"
        repoCashSchemeCode.Name = colCashDiscSchemeCode
        repoCashSchemeCode.Width = 80
        repoCashSchemeCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCashSchemeCode)


        Dim repoAcualCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAcualCost.AllowSort = False
        repoAcualCost.HeaderText = "Net Price"
        repoAcualCost.Name = colActualCost
        repoAcualCost.ReadOnly = True
        repoAcualCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAcualCost.Width = 55
        gv1.MasterTemplate.Columns.Add(repoAcualCost)




        Dim repoTotalMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalMRP.AllowSort = False
        repoTotalMRP.HeaderText = "Total MRP"
        repoTotalMRP.Name = colTotalMRP
        repoTotalMRP.ReadOnly = True
        repoTotalMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalMRP.Width = 62
        repoTotalMRP.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTotalMRP)

        Dim repoTotalBasicAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalBasicAmt.AllowSort = False
        repoTotalBasicAmt.HeaderText = "Total Basic Amount"
        repoTotalBasicAmt.Name = colTotalBasicAmount
        repoTotalBasicAmt.ReadOnly = True
        repoTotalBasicAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalBasicAmt.Width = 106
        repoTotalBasicAmt.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTotalBasicAmt)

        Dim repoTotalDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalDiscount.AllowSort = False
        repoTotalDiscount.HeaderText = "Total Discount"
        repoTotalDiscount.Name = colTotalDiscountAmount
        repoTotalDiscount.ReadOnly = True
        repoTotalDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalDiscount.Width = 81
        gv1.MasterTemplate.Columns.Add(repoTotalDiscount)

        Dim repoTotalCustDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalCustDiscount.HeaderText = "Total Cash Dis"
        repoTotalCustDiscount.Name = colTotalCustDiscount
        repoTotalCustDiscount.ReadOnly = True
        repoTotalCustDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalCustDiscount.Width = 79
        gv1.MasterTemplate.Columns.Add(repoTotalCustDiscount)

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


        Dim repoPrincipleCOde As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPrincipleCOde.FormatString = ""
        repoPrincipleCOde.HeaderText = "Principle Code"
        repoPrincipleCOde.Name = colPricipleCode
        repoPrincipleCOde.Width = 150
        repoPrincipleCOde.ReadOnly = True
        repoPrincipleCOde.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoPrincipleCOde)

        Dim repoPrincipleDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPrincipleDesc.FormatString = ""
        repoPrincipleDesc.HeaderText = "Principle Desc"
        repoPrincipleDesc.Name = colPricipleDesc
        repoPrincipleDesc.Width = 150
        repoPrincipleDesc.ReadOnly = True
        repoPrincipleDesc.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoPrincipleDesc)

        Dim repoVCOde As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVCOde.FormatString = ""
        repoVCOde.HeaderText = "Vendor Code"
        repoVCOde.Name = colvendorCode
        repoVCOde.Width = 150
        repoVCOde.ReadOnly = True
        repoVCOde.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoVCOde)

        Dim repoVDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVDesc.FormatString = ""
        repoVDesc.HeaderText = "Vendor Desc"
        repoVDesc.Name = colvendorDesc
        repoVDesc.Width = 150
        repoVDesc.ReadOnly = True
        repoVDesc.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoVDesc)

        Dim repoMarkupPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMarkupPer = New GridViewDecimalColumn()
        repoMarkupPer.FormatString = ""
        repoMarkupPer.HeaderText = "Mark Up %"
        repoMarkupPer.Name = colMarkUpPercentage
        repoMarkupPer.WrapText = True
        repoMarkupPer.Width = 80
        repoMarkupPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMarkupPer.VisibleInColumnChooser = False
        repoMarkupPer.ReadOnly = True
        repoMarkupPer.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoMarkupPer)



        Dim repoLandingCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLandingCost = New GridViewDecimalColumn()
        repoLandingCost.FormatString = ""
        repoLandingCost.HeaderText = "Landing Cost"
        repoLandingCost.Name = colLandingCost
        repoLandingCost.WrapText = True
        repoLandingCost.Width = 80
        repoLandingCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLandingCost.VisibleInColumnChooser = False
        repoLandingCost.ReadOnly = True
        repoLandingCost.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoLandingCost)

        Dim repoPurCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPurCost = New GridViewDecimalColumn()
        repoPurCost.FormatString = ""
        repoPurCost.HeaderText = "purchase Cost"
        repoPurCost.Name = colPurCost
        repoPurCost.WrapText = True
        repoPurCost.Width = 80
        repoPurCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPurCost.VisibleInColumnChooser = False
        repoPurCost.ReadOnly = True
        repoPurCost.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoPurCost)

        Dim repoMarkupOn As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMarkupOn.FormatString = ""
        repoMarkupOn.HeaderText = "MarkUp On"
        repoMarkupOn.Name = colMarkupOn
        repoMarkupOn.ReadOnly = True
        repoMarkupOn.Width = 100
        repoMarkupOn.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoMarkupOn)


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
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.IsVisible = False
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)

        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Included Tax Amount"
        repoAmtAfterTax.Name = colAmtAfterTax
        repoAmtAfterTax.WrapText = True
        repoAmtAfterTax.Width = 80
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        repoAmtAfterTax.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAmtAfterTax)


        Dim repoLandedRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLandedRate.FormatString = ""
        repoLandedRate.HeaderText = "Landed Rate"
        repoLandedRate.Name = colLandedRate
        repoLandedRate.WrapText = True
        repoLandedRate.Width = 80
        repoLandedRate.IsVisible = False
        repoLandedRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLandedRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLandedRate)

        Dim repoLandedAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLandedAmt.FormatString = ""
        repoLandedAmt.HeaderText = "Landed Amount"
        repoLandedAmt.Name = colLandedAmt
        repoLandedAmt.WrapText = True
        repoLandedAmt.Width = 80
        repoLandedAmt.IsVisible = False
        repoLandedAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLandedAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLandedAmt)

        Dim repoRequition As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRequition.FormatString = ""
        repoRequition.HeaderText = "Delivery No"
        repoRequition.Name = colOrderNo
        repoRequition.ReadOnly = True
        repoRequition.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition)



        Dim repoFOC As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFOC.FormatString = ""
        repoFOC.HeaderText = "FOC"
        repoFOC.Name = ColFOC
        repoFOC.ReadOnly = True
        repoFOC.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoFOC)

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
        gv1.MasterTemplate.Columns.Add(repoBinNo)

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
        gv1.MasterTemplate.Columns.Add(repoSpecification)


        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRemarks)










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
        gv1.MasterTemplate.Columns.Add(repoIsEmptyValue)

        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Serialize Item"
        repoIsSerItem.Name = colIsSerialseItem
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSerItem)


        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        ReStoreGridLayout()
    End Sub

    Sub OpenSerialItem()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSerialseItem).Value) Then
            Dim frm As frmSerializeItemOut = New frmSerializeItemOut()
            frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
            frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
            frm.strLocationCode = txtBillToLocation.Value
            frm.strCurrDocNo = txtDocNo.Value
            frm.strCurrDocType = "SD-IN"
            frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
            frm.ShowDialog()
            If Not frm.isCencelButtonClicked Then
                gv1.CurrentRow.Tag = frm.arr
            End If


        End If
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

    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
        If e.Column.Name = "complete" Then
            If grow.Cells(colComplete).Value = "No" Then
                grow.Cells(colComplete).Value = "Yes"
            ElseIf grow.Cells(colComplete).Value = "Yes" Then
                grow.Cells(colComplete).Value = "No"
            End If
        ElseIf e.Column.Name = colSchemeApplicable And grow.Cells(colSchemeItem).Value = "No" Then
            If grow.Cells(colSchemeApplicable).Value = "Yes" Then
                grow.Cells(colSchemeApplicable).Value = "No"

            ElseIf grow.Cells(colSchemeApplicable).Value = "No" Then
                grow.Cells(colSchemeApplicable).Value = "Yes"
            End If
        End If
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
                        UpdateCurrentRow(gv1.CurrentRow.Index, False) ''-1 is for current row
                        UpdateAllTotals()
                    End If
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colHeaDDisPer) OrElse (e.Column Is gv1.Columns(colHeadDiscamt)) OrElse e.Column Is gv1.Columns(colSchemeApplicable) OrElse e.Column Is gv1.Columns(colAmt) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colSpecification) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colDisPer) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colBatchNo) OrElse e.Column Is gv1.Columns(colExpiry) OrElse e.Column Is gv1.Columns(colManufactureDate) OrElse e.Column Is gv1.Columns(colUnit) OrElse (e.Column Is gv1.Columns(colAmt) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal) Then
                        If (e.Column Is gv1.Columns(colQty) OrElse (e.Column Is gv1.Columns(colHeadDiscamt)) OrElse e.Column Is gv1.Columns(colHeaDDisPer) OrElse e.Column Is gv1.Columns(colDisPer) OrElse (e.Column Is gv1.Columns(colAmt) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal)) Then
                            If ((e.Column Is gv1.Columns(colQty))) Then
                                Dim dblPendingQty As Double = 0
                                If (clsCommon.myLen(gv1.CurrentRow.Cells(colOrderNo).Value) > 0) Then
                                    dblPendingQty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPendingQty).Value)

                                    Dim dblEnteredQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)

                                    Dim dblDamageQty As Double = 0 'clsCommon.myCdbl(gv1.CurrentRow.Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colShortQty).Value)
                                    If (dblEnteredQty + dblDamageQty) > dblPendingQty Then
                                        common.clsCommon.MyMessageBoxShow("Entered Quantity Can't be more than Pending Quantity." + Environment.NewLine + "Entered Quantity : " + clsCommon.myCstr(dblEnteredQty) + ". Pending Quantity : " + clsCommon.myCstr(dblPendingQty))
                                        gv1.CurrentCell.Value = dblPendingQty
                                    End If
                                ElseIf clsCommon.myLen(gv1.CurrentRow.Cells(colOrderNo).Value) = 0 Then
                                    If AutoScheme Then
                                        gv1.CurrentRow.Cells(colSchemeApplicable).Value = "Yes"
                                    End If
                                End If

                                findQtyandPromoSchemeCode(False)
                            End If
                            If (e.Column Is gv1.Columns(colQty)) Then
                                OpenSerialItem()
                            End If
                            UpdateCurrentRow(gv1.CurrentRow.Index, True)

                            If rbtnTaxCalManual.IsChecked Then
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRow(ii, False)
                                Next
                            End If
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colICode) Then
                            If blnBackCalculation = True Then
                                OpenICodeList(False)
                            Else
                                OpenICodeListCurrentCalaculation(False)
                            End If
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)

                            'ElseIf e.Column Is gv1.Columns(colMRP) Then
                            '    Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                            '    Dim Rateqry As String = "select Item_Rate,MRP from TSPL_VENDOR_ITEM_DETAIL where Customer_Code='" + txtVendorNo.Value + "' and item_no='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "' and UOM='FC' and MRP='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colMRP).Value) + "' "
                            '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Rateqry)
                            '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            '        Dim VendrItemRate As Double = clsCommon.myCdbl(dt.Rows(0)("Item_Rate"))
                            '        Dim conversionFact As Double = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(strICode, gv1.CurrentRow.Cells(colUnit).Value, Nothing))
                            '        If VendrItemRate <> 0 Then
                            '            Dim Itemrate As Double = clsCommon.myCdbl(clsCommon.myCdbl(VendrItemRate) / clsCommon.myCdbl(conversionFact))
                            '            gv1.CurrentRow.Cells(colRate).Value = Math.Round(Itemrate, 2)
                            '        End If

                            '        UpdateCurrentRow(gv1.CurrentRow.Index)
                            '        If rbtnTaxCalManual.IsChecked Then
                            '            For ii As Integer = 0 To gv1.Rows.Count - 1
                            '                UpdateCurrentRow(ii)
                            '            Next
                            '        End If
                            '        UpdateAllTotals()

                            '    End If
                        ElseIf e.Column Is gv1.Columns(colSchemeApplicable) Then
                            findQtyandPromoSchemeCode(False)
                        ElseIf e.Column Is gv1.Columns(colMRP) Then
                            OpenGetbalance(False)
                        ElseIf e.Column Is gv1.Columns(colRate) Then
                            gv1.CurrentRow.Cells(colOrgCost).Value = gv1.CurrentRow.Cells(colRate).Value
                            UpdateCurrentRow(gv1.CurrentRow.Index, False)
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colAmt) Then

                            ' ''If clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) > 0 Then
                            ' ''    gv1.CurrentRow.Cells(colRate).Value = Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmt).Value) / clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), 2, MidpointRounding.ToEven)
                            ' ''Else
                            ' ''    gv1.CurrentRow.Cells(colAmt).Value = 0
                            ' ''End If

                            UpdateCurrentRow(gv1.CurrentRow.Index, False)
                            If rbtnTaxCalManual.IsChecked Then
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRow(ii, False)
                                Next
                            End If
                            UpdateAllTotals()
                        End If
                        'setGridFocus()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenGetbalance(ByVal isButtonClick As Boolean)
        gv1.CurrentRow.Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value))
    End Sub
    Private Sub findQtyandPromoSchemeCode(ByVal isButtonClick As Boolean)
        'Dim dr1 As DataTable
        'Dim schemeCodeCol As String
        Dim LocCodeCol As String = String.Empty
        Dim LocNameCol As String = String.Empty
        Dim intRow As Integer = 0
        Dim strOrderCode As String = ""
        Try
            Dim Index As Integer = gv1.CurrentRow.Index

            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colSchemeApplicable).Value), "No") = CompairStringResult.Equal OrElse clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) = 0 Then
                For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colSchemeApplicable).Value), "No") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(ColFOC).Value), 1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colMainIcode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) = CompairStringResult.Equal Then
                        gv1.Rows.RemoveAt(schemeRow)
                    End If
                Next
                gv1.Rows(Index).Cells(colCash_Amt).Value = Nothing
                gv1.Rows(Index).Cells(colCash_Pers).Value = Nothing
                gv1.Rows(Index).Cells(colCashSchemeCode).Value = Nothing
                gv1.Rows(Index).Cells(colCashSchemeType).Value = Nothing
                gv1.Rows(Index).Cells(colFromSchemeCode).Value = Nothing
                gv1.Rows(Index).Cells(colSchmCodeType).Value = Nothing
                gv1.Rows(Index).Cells(colMainIcode).Value = Nothing
                gv1.Rows(Index).Cells(colMainIQty).Value = Nothing
                gv1.Rows(Index).Cells(colMainIUOM).Value = Nothing
                gv1.Rows(Index).Cells(colSchemeApplicable).Value = "No"
                'gv1.Rows(Index).Cells(colSchemeItem).Value = "No"

                gv1.Rows(Index).Cells(ColFOC).Value = 0

                RefreshSerialNo()
            End If


            'gv1.CurrentRow.Cells(colSchemeItem).Value = "No"

            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(Index).Cells(colSchmCodeType).Value), "None") <> CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(Index).Cells(colSchmCodeType).Value), "") <> CompairStringResult.Equal Then

                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colSchemeApplicable).Value), "Yes") = CompairStringResult.Equal Then
                    For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colSchemeApplicable).Value), "No") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(ColFOC).Value), 1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colMainIcode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) = CompairStringResult.Equal Then
                            gv1.Rows.RemoveAt(schemeRow)
                        End If
                    Next
                    Dim objD As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colQty).Value), txtVendorNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colSchmCodeType).Value))
                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                        For Each objtr As clsSchemeApplyOnDairy In objD.Arr
                            If clsCommon.myLen(LocCodeCol) = 0 Then
                                LocCodeCol = txtBillToLocation.Value
                                LocNameCol = lblBillToLocation.Text
                            End If
                            'Dim dblSchemeItemActualQTy As Double = clsItemLocationDetails.getBalance(objtr.Schm_Icode, LocCodeCol, txtDocNo.Value, txtDate.Value, Nothing, objtr.Schm_IUnit_Rate, Nothing)
                            '--------------update free itemcode in main item row------------------
                            gv1.Rows(Index).Cells(colFromSchemeCode).Value = objtr.Schm_Code
                            gv1.Rows(Index).Cells(colSchmCodeType).Value = objtr.schm_Type
                            gv1.Rows(Index).Cells(colMainIcode).Value = objtr.Schm_Icode
                            gv1.Rows(Index).Cells(colMainIQty).Value = objtr.Schm_Qty
                            gv1.Rows(Index).Cells(colMainIUOM).Value = objtr.Schm_Item_Uom
                            gv1.Rows(Index).Cells(ColFOC).Value = 0
                            gv1.Rows(Index).Cells(colSchemeApplicable).Value = "Yes"
                            Dim MainItemCode = gv1.Rows(Index).Cells(colICode).Value
                            Dim MainItemUnit = gv1.Rows(Index).Cells(colUnit).Value
                            Dim MainItemQty As Double = gv1.Rows(Index).Cells(colQty).Value
                            Dim MainSaleOrderCode As String = gv1.Rows(Index).Cells(colOrderNo).Value

                            gv1.Rows.AddNew()



                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = intRow + 1
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.Schm_Icode
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objtr.Schm_Iname
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = lblPriceCode.Text
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = ""
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objtr.Schm_Item_Uom
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objtr.Schm_Qty
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colItemWeight).Value = clsCommon.myCdbl(tdr("Weight_Value"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = "No"
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeItem).Value = "Yes"
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFromSchemeCode).Value = objtr.Schm_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = 1

                            Dim dblConvF As Double
                            dblConvF = GetConvFactor(gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value, gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colConvF).Value = dblConvF
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotItemWt).Value = dblConvF * clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colItemWeight).Value) * clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value)

                            'gv1.Rows(gv1.Rows.Count - 1).Cells(ColActualBalQty).Value = Math.Round(clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), 0), 2)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).Value = objtr.schm_Type
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIcode).Value = MainItemCode
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIQty).Value = MainItemQty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIUOM).Value = MainItemUnit
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = MainSaleOrderCode

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).ReadOnly = True

                            Dim qry As String = ""
                            qry = " Select RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code from ( " & _
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " & _
                            "Start_Date,Item_Price_ID Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " & _
                            "Item_Basic_Price,Item_Basic_Net,Price_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " & _
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " & _
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and  " & _
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & txtPriceCode.Text & "' and UOM='" & objtr.Schm_Item_Uom & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objtr.Schm_Icode & "' " & _
                            ") XXXE WHERE RowNo=1  "

                            Dim dt As New DataTable()
                            dt = clsDBFuncationality.GetDataTable(qry)
                            If dt.Rows.Count > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Net"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = clsCommon.myCstr(dt.Rows(0).Item("Start_Date"))
                            End If


                            Dim Weight_UOM As String = ""
                            Dim SKU_VALUE As Decimal = 0

                            qry = "select Item_Code,Weight_UOM,Weight_Value from TSPL_ITEM_MASTER where Item_Code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "'"
                            Dim dtWt As New DataTable()
                            dtWt = clsDBFuncationality.GetDataTable(qry)
                            If dtWt.Rows.Count > 0 Then
                                SKU_VALUE = clsCommon.myCdbl(dtWt.Rows(0).Item("Weight_Value"))
                                Weight_UOM = clsCommon.myCstr(dtWt.Rows(0).Item("Weight_UOM"))
                            End If

                            UpdateCurrentRow(gv1.Rows(gv1.Rows.Count - 1).Index, False)
                            UpdateAllTotals()

                            gv1.Rows.Move(gv1.Rows.Count - 1, Index + 1)
                        Next
                    Else
                        gv1.Rows(Index).Cells(colFromSchemeCode).Value = Nothing
                        gv1.Rows(Index).Cells(colSchmCodeType).Value = Nothing
                        gv1.Rows(Index).Cells(colMainIcode).Value = Nothing
                        gv1.Rows(Index).Cells(colMainIQty).Value = Nothing
                        gv1.Rows(Index).Cells(colMainIUOM).Value = Nothing
                        gv1.Rows(Index).Cells(ColFOC).Value = 0
                        gv1.Rows(Index).Cells(colSchemeApplicable).Value = "No"
                    End If
                End If
            End If

            RefreshSerialNo()
        Catch ex As Exception
            myMessages.myExceptions(ex)
            gv1.CurrentRow.Cells(colSchemeApplicable).Value = "No"
            gv1.CurrentRow.Cells(colFromSchemeCode).Value = String.Empty
            Exit Sub
        End Try

    End Sub
    Sub RefreshSerialNo()
        Dim intSerialNo As Integer
        For intCount As Integer = 0 To gv1.Rows.Count - 1
            intSerialNo += 1
            gv1.Rows(intCount).Cells(colLineNo).Value = intSerialNo
        Next


    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("ShipmentItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)

            Dim dblConvF As Double
            dblConvF = GetConvFactor(gv1.CurrentRow.Cells(colUnit).Value, gv1.CurrentRow.Cells(colICode).Value)
            gv1.CurrentRow.Cells(colConvF).Value = dblConvF

            UpdateCurrentRow(gv1.CurrentRow.Index, True) ''-1 is for current row
            UpdateAllTotals()
            If AutoScheme Then
                gv1.CurrentRow.Cells(colSchemeApplicable).Value = "Yes"
            End If

            findQtyandPromoSchemeCode(False)

        End If
    End Sub



    Private Sub setGridFocus()
        Try
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
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
        gv1.CurrentRow.Cells(colRowType).Value = RowTypeItem
        Dim strItemType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value)
        If clsCommon.myLen(strItemType) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Row Type", Me.Text)
            Exit Sub
        End If

        Dim strPriceGrp As String = String.Empty
        Dim strPriceCondition As String = String.Empty
        Dim strPriceGrpStatus As String = String.Empty
        If clsCommon.myLen(txtPriceCode.Text) > 0 Then
            strPriceGrp = "''"
            strPriceGrpStatus = "''"
            strPriceCondition = "Price_Code='" & txtPriceCode.Text & "'"
        ElseIf clsCommon.myLen(txtPriceGroupCode.Text) Then
            strPriceGrp = "TSPL_PRICE_GROUP_MAPPING.price_group_code "
            strPriceCondition = "(priceGroup ='" & txtPriceGroupCode.Text & "' and PriceGroupStatus='Y')"
            strPriceGrpStatus = "TSPL_PRICE_GROUP_MAPPING.status"
        End If


        If clsCommon.CompairString(strItemType, RowTypeItem) = CompairStringResult.Equal Then
            Dim qry As String
            Dim strTaxRate As String = ""
            Dim dr As DataRow
            Dim COUNT As Integer = 0
            For Each grow As GridViewRowInfo In gv2.Rows
                COUNT += 1
                Dim Strii As String = clsCommon.myCstr(COUNT)
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colTTaxAutCode).Value)) > 0 Then
                    Dim intTaxRate As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT 1 FROM TSPL_LOCATION_SALE_TAX_MASTER WHERE Location_Code='" & txtBillToLocation.Value & "' AND Tax_Group_Code='" & txtTaxGroup.Value & "' AND Tax_Code='" & grow.Cells(colTTaxAutCode).Value & "'"))
                    If intTaxRate = 1 Then
                        If clsCommon.myLen(strTaxRate) > 0 Then
                            strTaxRate += " Or "
                        End If
                        strTaxRate += " exists (select 1 from TSPL_LOCATION_SALE_TAX_MASTER where Location_Code='" & txtBillToLocation.Value & "' and Tax_group=xxx.TAX" & Strii & " and TAX_Rate=xxx.TAX" & Strii & "_Rate  ) "
                    End If
                End If
            Next
            If clsCommon.myLen(strTaxRate) > 0 Then
                strTaxRate = " and ( " & strTaxRate & " )"
            End If

            If PurchaseOneItemOneVendor = True Then
                qry = "SELECT Item_Code as Item,Item_Desc as ItemDesc,Customer_item_no as [Customer Item Code],PrincipleCode,PrincipleDesc,vendor_code as [Vendor code],vendor_desc as [Vendor Desc],Price_Code,Start_Date AS Start_Date,UOM as Unit,Item_Basic_Net as MRP,abatement_rate, " & _
                "Item_Basic_Price as BasicRate,ITF_CODE as [ITF CODE], " & _
         "Weight_Value,markup_on,markup_percent,landing_cost,Purchase_Cost,TAX1_Rate as Tax1Rate,TAX2_Rate as  Tax2Rate,TAX3_Rate as Tax3Rate,TAX4_Rate as Tax4Rate, " & _
         "TAX5_Rate as Tax5Rate,TAX6_Rate as Tax6Rate,TAX7_Rate as Tax7Rate,TAX8_Rate as Tax8Rate, " & _
         "TAX9_Rate as Tax9Rate,TAX10_Rate as Tax10Rate,TAX1 ,TAX2,TAX3,TAX4,TAX5,TAX6,TAX7, TAX8,TAX9,TAX10  " & _
         "FROM ( SELECT distinct TSPL_VENDOR_ITEM_DETAIL.vendor_code,TSPL_VENDOR_ITEM_DETAIL.vendor_desc,Customer_item_no,TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE as PrincipleCode,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as PrincipleDesc," & strPriceGrpStatus & " as PriceGroupStatus," & strPriceGrp & " as priceGroup,TSPL_ITEM_PRICE_MASTER.Purchase_Cost,TSPL_ITEM_MASTER.Weight_Value,TSPL_ITEM_PRICE_MASTER.abatement_rate,TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.ITF_CODE,  " & _
         "CONVERT(varchar(10), TSPL_ITEM_PRICE_MASTER.Start_Date, 103) AS Start_Date, TSPL_ITEM_PRICE_MASTER.UOM, " & _
         "TSPL_ITEM_PRICE_MASTER.Price_Code, TSPL_ITEM_PRICE_MASTER.Item_Basic_Net,TSPL_ITEM_MASTER.Batch_No , TSPL_ITEM_PRICE_MASTER.Tax_group  , " & _
         "TSPL_ITEM_PRICE_MASTER.Item_Basic_Price, markup_on,markup_percent,landing_cost, " & _
         "TSPL_ITEM_MASTER.Item_Type, TSPL_ITEM_MASTER.show, TSPL_ITEM_MASTER.Sku_Seq, TSPL_ITEM_PRICE_MASTER.TAX1_Rate, " & _
         "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate, " & _
         "TSPL_ITEM_PRICE_MASTER.TAX6_Rate,TSPL_ITEM_PRICE_MASTER.TAX7_Rate,TSPL_ITEM_PRICE_MASTER.TAX8_Rate,TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " & _
         "TSPL_ITEM_PRICE_MASTER.TAX10_Rate,TSPL_ITEM_PRICE_MASTER.TAX1 ,TSPL_ITEM_PRICE_MASTER.TAX2,TSPL_ITEM_PRICE_MASTER.TAX3, " & _
         "TSPL_ITEM_PRICE_MASTER.TAX4,TSPL_ITEM_PRICE_MASTER.TAX5,TSPL_ITEM_PRICE_MASTER.TAX6,TSPL_ITEM_PRICE_MASTER.TAX7, " & _
         "TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10   FROM TSPL_ITEM_PRICE_MASTER " & _
         "INNER Join  (SELECT     Item_Code, UOM, MAX(Start_Date) AS MaxDateTime, Item_Basic_Net,  Price_Code, Tax_group  FROM  " & _
         "TSPL_ITEM_PRICE_MASTER  where Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' GROUP BY Item_Code, UOM, Item_Basic_Net,  Price_Code, Tax_group   " & _
         ")  AS groupedP  ON TSPL_ITEM_PRICE_MASTER.Item_Code = groupedP.Item_Code AND  TSPL_ITEM_PRICE_MASTER.Start_Date = groupedP.MaxDateTime AND " & _
         "TSPL_ITEM_PRICE_MASTER.UOM = groupedP.UOM AND TSPL_ITEM_PRICE_MASTER.Item_Basic_Net = groupedP.Item_Basic_Net  AND  " & _
         "TSPL_ITEM_PRICE_MASTER.Price_Code = groupedP.Price_Code and TSPL_ITEM_PRICE_MASTER.Tax_group = groupedP.Tax_group  left outer join TSPL_PRICE_GROUP_MAPPING on TSPL_ITEM_PRICE_MASTER.Price_Code=TSPL_PRICE_GROUP_MAPPING.price_code " & _
         "INNER JOIN TSPL_ITEM_MASTER AS TSPL_ITEM_MASTER ON TSPL_ITEM_PRICE_MASTER.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
         "left outer join TSPL_VENDOR_ITEM_DETAIL on TSPL_VENDOR_ITEM_DETAIL.item_no=TSPL_ITEM_MASTER.Item_Code left  outer join " & _
         "TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_MASTER_CATEGORY.SNO=1 left outer join " & _
         "TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
         "left outer join TSPL_CUSTOMER_ITEM_MAPPING on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_CUSTOMER_ITEM_MAPPING.item_no and TSPL_CUSTOMER_ITEM_MAPPING.Customer_Code='" & txtVendorNo.Value & "' " & _
         " where TSPL_ITEM_MASTER.Active=1 and is_freshitem=1 )xxx Where  ( " & strPriceCondition & " ) " & _
         "Order By Item_Code,Start_Date,UOM desc"
                dr = clsCommon.ShowSelectFormForRow("ICode", qry)
            Else

                qry = "SELECT Item_Code as Item,Item_Desc as ItemDesc,Customer_item_no as [Customer Item Code],PrincipleCode,PrincipleDesc,Price_Code,Start_Date AS Start_Date,UOM as Unit,Item_Basic_Net as MRP,abatement_rate,Item_Basic_Price as BasicRate,ITF_CODE as [ITF CODE], " & _
         "Weight_Value,markup_on,markup_percent,landing_cost,Purchase_Cost,TAX1_Rate as Tax1Rate,TAX2_Rate as  Tax2Rate,TAX3_Rate as Tax3Rate,TAX4_Rate as Tax4Rate, " & _
         "TAX5_Rate as Tax5Rate,TAX6_Rate as Tax6Rate,TAX7_Rate as Tax7Rate,TAX8_Rate as Tax8Rate, " & _
         "TAX9_Rate as Tax9Rate,TAX10_Rate as Tax10Rate,TAX1 ,TAX2,TAX3,TAX4,TAX5,TAX6,TAX7, TAX8,TAX9,TAX10  " & _
         "FROM ( SELECT distinct Customer_item_no,TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE as PrincipleCode,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as PrincipleDesc," & strPriceGrpStatus & " as PriceGroupStatus," & strPriceGrp & " as priceGroup,TSPL_ITEM_PRICE_MASTER.Purchase_Cost,TSPL_ITEM_MASTER.Weight_Value,TSPL_ITEM_PRICE_MASTER.abatement_rate,TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.ITF_CODE,  " & _
         "CONVERT(varchar(10), TSPL_ITEM_PRICE_MASTER.Start_Date, 103) AS Start_Date, TSPL_ITEM_PRICE_MASTER.UOM, " & _
         "TSPL_ITEM_PRICE_MASTER.Price_Code, TSPL_ITEM_PRICE_MASTER.Item_Basic_Net,TSPL_ITEM_MASTER.Batch_No , TSPL_ITEM_PRICE_MASTER.Tax_group  , " & _
         "TSPL_ITEM_PRICE_MASTER.Item_Basic_Price, markup_on,markup_percent,landing_cost, " & _
         "TSPL_ITEM_MASTER.Item_Type, TSPL_ITEM_MASTER.show, TSPL_ITEM_MASTER.Sku_Seq, TSPL_ITEM_PRICE_MASTER.TAX1_Rate, " & _
         "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate, " & _
         "TSPL_ITEM_PRICE_MASTER.TAX6_Rate,TSPL_ITEM_PRICE_MASTER.TAX7_Rate,TSPL_ITEM_PRICE_MASTER.TAX8_Rate,TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " & _
         "TSPL_ITEM_PRICE_MASTER.TAX10_Rate,TSPL_ITEM_PRICE_MASTER.TAX1 ,TSPL_ITEM_PRICE_MASTER.TAX2,TSPL_ITEM_PRICE_MASTER.TAX3, " & _
         "TSPL_ITEM_PRICE_MASTER.TAX4,TSPL_ITEM_PRICE_MASTER.TAX5,TSPL_ITEM_PRICE_MASTER.TAX6,TSPL_ITEM_PRICE_MASTER.TAX7, " & _
         "TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10   FROM TSPL_ITEM_PRICE_MASTER " & _
         "INNER Join  (SELECT     Item_Code, UOM, MAX(Start_Date) AS MaxDateTime, Item_Basic_Net,  Price_Code, Tax_group  FROM  " & _
         "TSPL_ITEM_PRICE_MASTER  where Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' GROUP BY Item_Code, UOM, Item_Basic_Net,  Price_Code, Tax_group   " & _
         ")  AS groupedP  ON TSPL_ITEM_PRICE_MASTER.Item_Code = groupedP.Item_Code AND  TSPL_ITEM_PRICE_MASTER.Start_Date = groupedP.MaxDateTime AND " & _
         "TSPL_ITEM_PRICE_MASTER.UOM = groupedP.UOM AND TSPL_ITEM_PRICE_MASTER.Item_Basic_Net = groupedP.Item_Basic_Net  AND  " & _
         "TSPL_ITEM_PRICE_MASTER.Price_Code = groupedP.Price_Code and TSPL_ITEM_PRICE_MASTER.Tax_group = groupedP.Tax_group  left outer join TSPL_PRICE_GROUP_MAPPING on TSPL_ITEM_PRICE_MASTER.Price_Code=TSPL_PRICE_GROUP_MAPPING.price_code " & _
         "INNER JOIN TSPL_ITEM_MASTER AS TSPL_ITEM_MASTER ON TSPL_ITEM_PRICE_MASTER.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
         "left  outer join " & _
         "TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_MASTER_CATEGORY.SNO=1 left outer join " & _
         "TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
         "left outer join TSPL_CUSTOMER_ITEM_MAPPING on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_CUSTOMER_ITEM_MAPPING.item_no and TSPL_CUSTOMER_ITEM_MAPPING.Customer_Code='" & txtVendorNo.Value & "' " & _
         " where TSPL_ITEM_MASTER.Active=1  and is_freshitem=1)xxx Where  ( " & strPriceCondition & " )  " & _
         "Order By Item_Code,Start_Date,UOM desc"

                dr = clsCommon.ShowSelectFormForRow("ICode1", qry)
            End If


            If Not dr Is Nothing Then
                If clsCommon.myLen(clsCommon.myCstr(dr("Item"))) > 0 Then
                    SetitemWiseTaxSetting(True, True)
                    gv1.CurrentRow.Cells(colRowType).Value = RowTypeItem
                    gv1.CurrentRow.Cells(colICode).Value = clsCommon.myCstr(dr("Item"))
                    'gv1.CurrentRow.Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(dr("Unit")), clsCommon.myCdbl(dr("MRP")))
                    'gv1.CurrentRow.Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalanceWithUnapprove(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtBillToLocation.Value, clsCommon.myCdbl(dr("MRP")), clsCommon.myCstr(dr("Unit")), txtDocNo.Value, txtDate.Value)
                    'If gv1.CurrentRow.Cells(ColActualBalQty).Value = 0 Then
                    '    isCellValueChangedOpen = False

                    '    Throw New Exception("Qty is not avaliable for item " & gv1.CurrentRow.Cells(colICode).Value & " at location " & txtBillToLocation.Value & " ")
                    'End If
                    If PurchaseOneItemOneVendor = True Then
                        gv1.CurrentRow.Cells(colPricipleCode).Value = clsCommon.myCstr(dr("PrincipleCode"))
                        gv1.CurrentRow.Cells(colPricipleDesc).Value = clsCommon.myCstr(dr("PrincipleDesc"))
                        gv1.CurrentRow.Cells(colvendorCode).Value = clsCommon.myCstr(dr("Vendor code"))
                        gv1.CurrentRow.Cells(colvendorDesc).Value = clsCommon.myCstr(dr("Vendor Desc"))
                    End If
                    gv1.CurrentRow.Cells(colIName).Value = clsCommon.myCstr(dr("ItemDesc"))
                    gv1.CurrentRow.Cells(colPriceCOde).Value = clsCommon.myCstr(dr("Price_Code"))
                    gv1.CurrentRow.Cells(colPriceDateColumn).Value = clsCommon.myCstr(dr("Start_Date"))
                    gv1.CurrentRow.Cells(colUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.CurrentRow.Cells(colOrgUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.CurrentRow.Cells(colMRP).Value = clsCommon.myCdbl(dr("MRP"))
                    gv1.CurrentRow.Cells(colRate).Value = clsCommon.myCdbl(dr("BasicRate"))
                    gv1.CurrentRow.Cells(colAbatementPer).Value = clsCommon.myCdbl(dr("abatement_rate"))
                    gv1.CurrentRow.Cells(colSchemeApplicable).Value = "No"
                    gv1.CurrentRow.Cells(colSchemeItem).Value = "No"
                    gv1.CurrentRow.Cells(colActualCost).Value = clsCommon.myCdbl(dr("BasicRate"))
                    gv1.CurrentRow.Cells(colTaxRate1).Value = clsCommon.myCdbl(dr("Tax1Rate"))
                    gv1.CurrentRow.Cells(colTaxRate2).Value = clsCommon.myCdbl(dr("Tax2Rate"))
                    gv1.CurrentRow.Cells(colTaxRate3).Value = clsCommon.myCdbl(dr("Tax3Rate"))
                    gv1.CurrentRow.Cells(colTaxRate4).Value = clsCommon.myCdbl(dr("Tax4Rate"))
                    gv1.CurrentRow.Cells(colTaxRate5).Value = clsCommon.myCdbl(dr("Tax5Rate"))
                    gv1.CurrentRow.Cells(colTaxRate6).Value = clsCommon.myCdbl(dr("Tax6Rate"))
                    gv1.CurrentRow.Cells(colTaxRate7).Value = clsCommon.myCdbl(dr("Tax7Rate"))
                    gv1.CurrentRow.Cells(colTaxRate8).Value = clsCommon.myCdbl(dr("Tax8Rate"))
                    gv1.CurrentRow.Cells(colTaxRate9).Value = clsCommon.myCdbl(dr("Tax9Rate"))
                    gv1.CurrentRow.Cells(colTaxRate10).Value = clsCommon.myCdbl(dr("Tax10Rate"))

                    'gv1.CurrentRow.Cells(colTax1).Value = clsCommon.myCstr(dr("Tax1"))
                    'gv1.CurrentRow.Cells(colTax2).Value = clsCommon.myCstr(dr("Tax2"))
                    'gv1.CurrentRow.Cells(colTax3).Value = clsCommon.myCstr(dr("Tax3"))
                    'gv1.CurrentRow.Cells(colTax4).Value = clsCommon.myCstr(dr("Tax4"))
                    'gv1.CurrentRow.Cells(colTax5).Value = clsCommon.myCstr(dr("Tax5"))
                    'gv1.CurrentRow.Cells(colTax6).Value = clsCommon.myCstr(dr("Tax6"))
                    'gv1.CurrentRow.Cells(colTax7).Value = clsCommon.myCstr(dr("Tax7"))
                    'gv1.CurrentRow.Cells(colTax8).Value = clsCommon.myCstr(dr("Tax8"))
                    'gv1.CurrentRow.Cells(colTax9).Value = clsCommon.myCstr(dr("Tax9"))
                    'gv1.CurrentRow.Cells(colTax10).Value = clsCommon.myCstr(dr("Tax10"))

                    gv1.CurrentRow.Cells(ColFOC).Value = 0
                    gv1.CurrentRow.Cells(colItemWeight).Value = clsCommon.myCdbl(dr("Weight_Value"))
                    Dim dblConvF As Double
                    dblConvF = GetConvFactor(gv1.CurrentRow.Cells(colUnit).Value, gv1.CurrentRow.Cells(colICode).Value)
                    gv1.CurrentRow.Cells(colConvF).Value = dblConvF

                    gv1.CurrentRow.Cells(colMarkupOn).Value = clsCommon.myCstr(dr("markup_on"))
                    gv1.CurrentRow.Cells(colMarkUpPercentage).Value = clsCommon.myCdbl(dr("markup_percent"))
                    gv1.CurrentRow.Cells(colLandingCost).Value = clsCommon.myCdbl(dr("landing_cost"))
                    gv1.CurrentRow.Cells(colPurCost).Value = clsCommon.myCdbl(dr("Purchase_Cost"))
                    gv1.CurrentRow.Cells(colOrgCost).Value = clsCommon.myCdbl(dr("BasicRate"))
                End If
            Else
                SetBlankOfItemColumns()
            End If


        End If


        setBalance()
        '    Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(cboItemType.SelectedValue), True, isButtonClick, "", txtVendorNo.Value)
        '    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
        '        gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
        '        gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
        '        gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
        '        gv1.CurrentRow.Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value))
        '        gv1.CurrentRow.Cells(colIsSerialseItem).Value = obj.Is_Serial_Item
        '        Dim objCustItem As clsCustomeritemDetails = clsCustomeritemDetails.GetItemRateAndDiscount(txtVendorNo.Value, obj.Item_Code, obj.Unit_Code, txtDate.Value)
        '        If objCustItem IsNot Nothing Then
        '            gv1.CurrentRow.Cells(colRate).Value = objCustItem.Approval_Item_Rate
        '            gv1.CurrentRow.Cells(colDisPer).Value = objCustItem.Discount_Per
        '        End If
        '    Else
        '        SetBlankOfItemColumns()
        '    End If
        '    ''End If
        '    Dim objVItem As clsVendorItemDetail = clsVendorItemDetail.GetItemRateAndMRP(txtVendorNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
        '    If objVItem IsNot Nothing Then
        '        gv1.CurrentRow.Cells(colRate).Value = objVItem.item_rate
        '        gv1.CurrentRow.Cells(colMRP).Value = objVItem.MRP
        '    Else

        '    End If
        'Else
        '    ''For Open Misc Charges 
        '    Dim obj As clsAdditionalCharge = clsAdditionalCharge.GetFinder(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), isButtonClick)
        '    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
        '        gv1.CurrentRow.Cells(colICode).Value = obj.Code
        '        gv1.CurrentRow.Cells(colIName).Value = obj.desc
        '        gv1.CurrentRow.Cells(colUnit).Value = Nothing
        '        gv1.CurrentRow.Cells(colQty).Value = Nothing
        '        gv1.CurrentRow.Cells(colRate).Value = Nothing
        '    Else
        '        SetBlankOfItemColumns()
        '    End If
        '    ''End of Misc Charges 
        'End If
        'SetitemWiseTaxSetting(True, True)
        'setBalance()
    End Sub
    Sub OpenICodeListCurrentCalaculation(ByVal isButtonClick As Boolean)
        gv1.CurrentRow.Cells(colRowType).Value = RowTypeItem
        Dim strItemType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value)
        If clsCommon.myLen(strItemType) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Row Type", Me.Text)
            Exit Sub
        End If

        If clsCommon.CompairString(strItemType, RowTypeItem) = CompairStringResult.Equal Then
            Dim qry As String
            If PurchaseOneItemOneVendor = True Then
                qry = "select TSPL_CUSTOMER_ITEM_DETAIL.item_no as Item,TSPL_CUSTOMER_ITEM_DETAIL.item_desc as [ItemDesc],TSPL_CUSTOMER_ITEM_DETAIL.uom as Unit , " & _
                "TSPL_CUSTOMER_ITEM_DETAIL.item_rate as BasicRate,TSPL_CUSTOMER_ITEM_DETAIL.Item_MRP as MRP, " & _
                "TSPL_CUSTOMER_ITEM_DETAIL.Start_Date as [Start Date],vendor_code as [Vendor code],vendor_desc as [Vendor Desc]  , " & _
                "TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE as PrincipleCode,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as PrincipleDesc, " & _
                "Weight_Value as [Weight Value] from TSPL_CUSTOMER_ITEM_DETAIL left outer join TSPL_ITEM_MASTER on " & _
                "TSPL_ITEM_MASTER.Item_Code=TSPL_CUSTOMER_ITEM_DETAIL.item_no left outer join " & _
                "TSPL_VENDOR_ITEM_DETAIL on TSPL_CUSTOMER_ITEM_DETAIL.item_no=TSPL_VENDOR_ITEM_DETAIL.item_no  left  outer join " & _
                "TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code and " & _
                "TSPL_ITEM_MASTER_CATEGORY.SNO=1 left outer join " & _
                "TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
                "where Customer_Code='" & txtVendorNo.Value & "' and TSPL_ITEM_MASTER.Active=1   and is_freshitem=1"
            Else

                qry = "select TSPL_CUSTOMER_ITEM_DETAIL.item_no as Item,TSPL_CUSTOMER_ITEM_DETAIL.item_desc as [ItemDesc],TSPL_CUSTOMER_ITEM_DETAIL.uom as Unit , " & _
                "TSPL_CUSTOMER_ITEM_DETAIL.item_rate as BasicRate,TSPL_CUSTOMER_ITEM_DETAIL.Item_MRP as MRP, " & _
                "TSPL_CUSTOMER_ITEM_DETAIL.Start_Date as [Start Date], " & _
                "Weight_Value as [Weight Value] from TSPL_CUSTOMER_ITEM_DETAIL left outer join TSPL_ITEM_MASTER on " & _
                "TSPL_ITEM_MASTER.Item_Code=TSPL_CUSTOMER_ITEM_DETAIL.item_no  " & _
                "where Customer_Code='" & txtVendorNo.Value & "' and TSPL_ITEM_MASTER.Active=1  and is_freshitem=1 "

            End If



            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("DNFS@It", qry)
            If Not dr Is Nothing Then
                If clsCommon.myLen(clsCommon.myCstr(dr("Item"))) > 0 Then
                    SetitemWiseTaxSetting(True, True)
                    gv1.CurrentRow.Cells(colRowType).Value = RowTypeItem
                    gv1.CurrentRow.Cells(colICode).Value = clsCommon.myCstr(dr("Item"))
                    gv1.CurrentRow.Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(dr("Unit")), clsCommon.myCdbl(dr("MRP")))

                    If PurchaseOneItemOneVendor = True Then
                        gv1.CurrentRow.Cells(colPricipleCode).Value = clsCommon.myCstr(dr("PrincipleCode"))
                        gv1.CurrentRow.Cells(colPricipleDesc).Value = clsCommon.myCstr(dr("PrincipleDesc"))
                        gv1.CurrentRow.Cells(colvendorCode).Value = clsCommon.myCstr(dr("Vendor code"))
                        gv1.CurrentRow.Cells(colvendorDesc).Value = clsCommon.myCstr(dr("Vendor Desc"))
                    End If
                    gv1.CurrentRow.Cells(colIName).Value = clsCommon.myCstr(dr("ItemDesc"))
                    gv1.CurrentRow.Cells(colPriceCOde).Value = ""
                    gv1.CurrentRow.Cells(colPriceDateColumn).Value = clsCommon.myCstr(dr("Start Date"))
                    gv1.CurrentRow.Cells(colUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.CurrentRow.Cells(colMRP).Value = clsCommon.myCdbl(dr("MRP"))
                    gv1.CurrentRow.Cells(colRate).Value = clsCommon.myCdbl(dr("BasicRate"))
                    'gv1.CurrentRow.Cells(colAbatementPer).Value = clsCommon.myCdbl(dr("abatement_rate"))
                    gv1.CurrentRow.Cells(colSchemeApplicable).Value = "No"
                    gv1.CurrentRow.Cells(colSchemeItem).Value = "No"
                    gv1.CurrentRow.Cells(colActualCost).Value = clsCommon.myCdbl(dr("BasicRate"))
                    'gv1.CurrentRow.Cells(colTaxRate1).Value = 
                    'gv1.CurrentRow.Cells(colTaxRate2).Value = clsCommon.myCdbl(dr("Tax2Rate"))
                    'gv1.CurrentRow.Cells(colTaxRate3).Value = clsCommon.myCdbl(dr("Tax3Rate"))
                    'gv1.CurrentRow.Cells(colTaxRate4).Value = clsCommon.myCdbl(dr("Tax4Rate"))
                    'gv1.CurrentRow.Cells(colTaxRate5).Value = clsCommon.myCdbl(dr("Tax5Rate"))
                    'gv1.CurrentRow.Cells(colTaxRate6).Value = clsCommon.myCdbl(dr("Tax6Rate"))
                    'gv1.CurrentRow.Cells(colTaxRate7).Value = clsCommon.myCdbl(dr("Tax7Rate"))
                    'gv1.CurrentRow.Cells(colTaxRate8).Value = clsCommon.myCdbl(dr("Tax8Rate"))
                    'gv1.CurrentRow.Cells(colTaxRate9).Value = clsCommon.myCdbl(dr("Tax9Rate"))
                    'gv1.CurrentRow.Cells(colTaxRate10).Value = clsCommon.myCdbl(dr("Tax10Rate"))

                    'gv1.CurrentRow.Cells(colMarkupOn).Value = clsCommon.myCstr(dr("markup_on"))
                    'gv1.CurrentRow.Cells(colMarkUpPercentage).Value = clsCommon.myCdbl(dr("markup_percent"))
                    'gv1.CurrentRow.Cells(colLandingCost).Value = clsCommon.myCdbl(dr("landing_cost"))

                    gv1.CurrentRow.Cells(ColFOC).Value = 0
                    gv1.CurrentRow.Cells(colItemWeight).Value = clsCommon.myCdbl(dr("Weight Value"))
                    'gv1.CurrentRow.Cells(colPurCost).Value = clsCommon.myCdbl(dr("Purchase_Cost"))
                    gv1.CurrentRow.Cells(colOrgCost).Value = clsCommon.myCdbl(dr("BasicRate"))
                    gv1.CurrentRow.Cells(colOrgUnit).Value = clsCommon.myCstr(dr("Unit"))

                    Dim dblConvF As Double
                    dblConvF = GetConvFactor(gv1.CurrentRow.Cells(colUnit).Value, gv1.CurrentRow.Cells(colICode).Value)
                    gv1.CurrentRow.Cells(colConvF).Value = dblConvF
                End If
            Else
                SetBlankOfItemColumns()
            End If


        End If


        setBalance()

    End Sub
    Private Function GetConvFactor(ByVal strUnit As String, ByVal strItem As String) As Double
        Dim dblConvF As Double = 0
        dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strUnit & "'"))
        Return dblConvF
    End Function
    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colUnit).Value = ""
        gv1.CurrentRow.Cells(colMRP).Value = 0


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

    Private Sub UpdateAllTotals()
        Dim dblCashDisAmt As Double = 0
        Dim dblHeadDisPerAmt As Double = 0

        Dim dblTotAmt As Double = 0
        Dim dblTotDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0
        Dim dblTotLandedCost As Double = 0
        Dim dblHeadDisAmt As Double = 0

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
        Dim dblTotalWt As Double = 0


        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        Dim dblCrateQty As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            dblTotalWt = dblTotalWt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotItemWt).Value)
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(ColFOC).Value) = 1 Then  '''' And clsCommon.myCdbl(gv1.Rows(ii).Cells(ColFOC).Value) = 0
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                'dblAmtAfterDis = dblTotDisAmt + dblAmtAfterDis - clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
                'dblNetAmt = dblNetAmt - clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                dblCrateQty = dblCrateQty + clsCommon.myCdbl(gv1.Rows(ii).Cells(colCrate).Value)
            Else
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisAmt).Value)
                dblCashDisAmt = dblCashDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colCash_Amt).Value)
                dblHeadDisAmt = dblHeadDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colHeadDiscamt).Value)
                dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
                dblHeadDisPerAmt = dblHeadDisPerAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colHeadDisPerAmt).Value)
                dblCrateQty = dblCrateQty + clsCommon.myCdbl(gv1.Rows(ii).Cells(colCrate).Value)
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

                dblTotLandedCost = dblTotLandedCost + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLandedAmt).Value)
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
        For ii As Integer = 0 To gvAC.Rows.Count - 1
            If (clsCommon.myLen(gvAC.Rows(ii).Cells(colACCode).Value) > 0) Then
                dblACAmount = dblACAmount + clsCommon.myCdbl(gvAC.Rows(ii).Cells(colACAmount).Value)
            End If
        Next

        lblTotalWeight.Text = clsCommon.myFormat(dblTotalWt)

        lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt + dblCashDisAmt)
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblAmtAfterDis)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)

        lblAddCharges.Text = clsCommon.myFormat(dblACAmount)
        lblAddCharges1.Text = clsCommon.myFormat(dblACAmount)

        dblNetAmt = dblNetAmt + dblACAmount
        lblInvoiceDiscAmt.Text = dblHeadDisAmt + dblHeadDisPerAmt
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        lblTotRAmt1.Text = lblTotRAmt.Text
        txtCrateQty.Value = dblCrateQty
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
        fndProject.Enabled = True
        lblProject.Enabled = True
        LoadBlankGrid()
        LoadBlankGridAC()
        LoadBlankGridTax()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        gv1.Rows.AddNew()
        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
        ''gv1.Rows.AddNew()
        chkInternal.Checked = False
        gvAC.Rows.AddNew()
        gvAC.Rows.AddNew()
        txtDate.Enabled = True
        txtVendorNo.Enabled = True
        btnHistory.Enabled = False
        txtSalesman.Value = ""
        lblSalesman.Text = ""
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        btnSave.Enabled = False
        btnShowInvoiceJE.Enabled = False
    End Sub
    Private Sub isValid_CashScheme()
        Dim scheme_Code As String = ""
        Dim isSchemeApply As String = ""
        Dim cash_scheme_code As String = ""
        Dim cash_amt As Decimal = 0
        Dim amount As Decimal = 0

        For Each grow As GridViewRowInfo In gv1.Rows
            cash_scheme_code = clsCommon.myCstr(grow.Cells(colCashSchemeCode).Value)
            cash_amt = clsCommon.myCdbl(grow.Cells(colCash_Amt).Value)
            amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)

            '================if cash scheme amount exceed total basic amount than scheme not applicable.
            If cash_amt > amount Then
                grow.Cells(colCash_Amt).Value = 0
                grow.Cells(colCash_Pers).Value = 0
                grow.Cells(colCashSchemeCode).Value = Nothing
                grow.Cells(colCashSchemeType).Value = Nothing
            End If
        Next
    End Sub
    Function AllowToSave() As Boolean
        Try
            'KUNAL > TICKET : BM00000009580 > DATE :  18 - OCTOBER - 2016
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Select()
                Return False
            End If

            RefreshReqNo()
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii, True)
            Next

            UpdateAllTotals()

            If clsCommon.myLen(txtPONo.Text) > 0 AndAlso clsCommon.myLen(txtpodate.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Fill Customer PO Date", Me.Text)
                txtpodate.Focus()
                txtpodate.Select()
                Return False
            End If

            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Customer", Me.Text)
                txtVendorNo.Focus()
                Return False
            End If
            ''richa agarwal against ticket no BM00000004375
            'If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select Tax Group")
            '    txtTaxGroup.Focus()
            '    Return False
            'End If
            ''------------------------------------------
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Bill to Location", Me.Text)
                txtBillToLocation.Focus()
                Return False
            End If
            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Shipment No Not found to save", Me.Text)
                txtDocNo.Focus()
                Return False
            End If
            'If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select Item Type")
            '    cboItemType.Focus()
            '    Return False
            'End If

            Dim arrReqNo As New List(Of String)
            Dim arrICode As New List(Of String)()
            Dim arrProjNo As New List(Of String)

            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colOrderNo).Value)
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                Dim strSchemeItem As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemeItem).Value)
                Dim strProject As String = String.Empty
                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal Then
                    If clsCommon.myLen(strUOM) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Please enter UOM for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If

                    Dim objCustItem As clsCustomeritemDetails = clsCustomeritemDetails.GetItemRateAndDiscount(txtVendorNo.Value, strICode, clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value), txtDate.Value)
                    If objCustItem IsNot Nothing Then
                        If objCustItem.Min_Rate > (clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value) / dblQty) Then
                            common.clsCommon.MyMessageBoxShow("Minimum Rate Can't be Less Then " + clsCommon.myCstr(objCustItem.Min_Rate) + " of Item " + strICode.Trim() + "( " + strIName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)))
                            Return False
                        End If
                    End If

                    If clsCommon.myLen(strReqNo) > 0 Then
                        If Not (arrReqNo.Contains(strReqNo)) Then
                            arrReqNo.Add(strReqNo)
                        End If
                        If dblQty > dblPendingQty AndAlso clsCommon.CompairString(strSchemeItem, "No") = CompairStringResult.Equal Then
                            common.clsCommon.MyMessageBoxShow("Item " + strICode + "( " + strIName.Trim() + " ) Entered Quantity(" + clsCommon.myCstr(dblQty) + ") Can't be more Pending Quantity(" + clsCommon.myCstr(dblPendingQty) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                            Return False
                        End If

                        If objCommonVar.IsDemoERP Then
                            If Not strProject Is Nothing Then
                                strProject = clsSNSalesOrderHead.IsValidProjectForSO(strReqNo, "")
                                If arrProjNo.Contains(strProject) And arrProjNo.Count > 0 Then
                                    common.clsCommon.MyMessageBoxShow("SO No:" + strReqNo + " and Item : " + strIName + " is not for PJC or not related to PJC At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                                    Return False
                                Else
                                    arrProjNo.Add(strProject)
                                End If
                            End If
                        End If

                    End If
                    'If clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value) <= 0 Then
                    '    common.clsCommon.MyMessageBoxShow("Please enter MRP No for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                    '    Return False
                    'End If
                    If IsBatchMFDEXDmandatory Then
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colBatchNo).Value) <= 0 Then
                            common.clsCommon.MyMessageBoxShow("Please enter Batch No for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                            Return False
                        End If
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colManufactureDate).Value) <= 0 Then
                            common.clsCommon.MyMessageBoxShow("Please enter Manufacture Date for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                            Return False
                        End If
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colExpiry).Value) <= 0 Then
                            common.clsCommon.MyMessageBoxShow("Please enter Expiry Date for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                            Return False
                        End If
                        If clsCommon.GetDateWithStartTime(clsCommon.myCDate(gv1.Rows(ii).Cells(colManufactureDate).Value)) > clsCommon.GetDateWithStartTime(clsCommon.myCDate(gv1.Rows(ii).Cells(colExpiry).Value)) Then
                            common.clsCommon.MyMessageBoxShow("Please enter Expiry Date greater than Manufacturing Date in " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                            Return False
                        End If
                    End If
                    ddlInvoiceType.SelectedValue = "R"
                    If chkCreateAutoInvoice.Checked Then
                        If AllowChangeInvoiceType Then
                            If clsCommon.myLen(ddlInvoiceType.SelectedValue) <= 0 Then
                                common.clsCommon.MyMessageBoxShow("Please select invoice  Type for creating auto invoice")
                                cboItemType.Focus()
                                Return False
                            End If
                        Else
                            InvoiceType()
                        End If
                    End If

                    If Not arrICode.Contains(strICode) Then
                        arrICode.Add(strICode)
                    End If

                    If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.CompairString(strSchemeItem, "No") = CompairStringResult.Equal Then
                        For jj As Integer = ii + 1 To gv1.Rows.Count - 1
                            Dim strInICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                            Dim strInUOM As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)
                            Dim strInDocNo As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colOrderNo).Value)


                            If clsCommon.CompairString(strICode, strInICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(jj).Cells(colSchemeItem).Value), "No") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strReqNo, strInDocNo) = CompairStringResult.Equal Then
                                common.clsCommon.MyMessageBoxShow("Item Code " + strICode + " and Unit " + strUOM + " is repeat at Row No" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1), Me.Text)
                                Return False
                            End If
                        Next
                    End If


                    Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                    Dim dblBalQty As Double = clsItemLocationDetails.getBalance(strICode, txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, strUOM, dblMRP)
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

                    If dblEnteredQty > dblBalQty AndAlso dblEnteredQty > 0 AndAlso clsCommon.CompairString(strSchemeItem, "No") = CompairStringResult.Equal Then
                        common.clsCommon.MyMessageBoxShow("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty), Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsSerialseItem).Value) Then
                    Dim arrSerailNo As List(Of clsSerializeInvenotry) = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                    If arrSerailNo Is Nothing OrElse dblQty <> arrSerailNo.Count Then
                        common.clsCommon.MyMessageBoxShow("Please provice serial no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                End If
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


            'clsItemMaster.isItemOfSameType(clsCommon.myCstr(cboItemType.SelectedValue), cboItemType.Text, arrICode)
            clsSNSalesOrderHead.IsValidCustomer(arrReqNo, txtVendorNo.Value)

            ''richa agarwal against ticket no BM00000004375

            'If clsLocation.isLocatinExcisable(txtBillToLocation.Value) Then
            '    Dim qry As String = "select Type  from TSPL_TAX_MASTER where Tax_Code ='" + clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value) + "' "
            '    If Not clsCommon.CompairString("E", clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))) = CompairStringResult.Equal Then
            '        common.clsCommon.MyMessageBoxShow("Tax should be excisable for excisable location")
            '        Return False
            '    End If
            'End If
            ''------------------------------------
            '' Anubhooti 11-Sep-2014 BM00000003847

            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) & "'"))
                If ItemCrateType = 1 Then
                    Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value) & "'"))
                    'UpdateCurrentRow(ii, True)
                    If IsStockingUnit = "Y" Then
                        Dim ConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) & "' and tspl_unit_master.Crate_Type ='Y' "))
                        If ConvFactor <= 0 Then
                            common.clsCommon.MyMessageBoxShow("Please fill conversion factor for item (" & clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) & ") at line no." & ii + 1 & "")
                            Return False
                        End If

                    End If
                End If
            Next
            ''
            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()

            If funvalidatevehicle() = False Then
                Return False
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Sub InvoiceType()
        If AllowChangeInvoiceType = False Then
            Dim dt As DataTable
            Dim strloc As String
            Dim qry As String
            If clsCommon.myLen(txtShipToLocation.Value) > 0 Then
                strloc = txtShipToLocation.Value
                qry = "select State from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" & strloc & "'"
            Else
                strloc = txtBillToLocation.Value
                qry = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State, " & _
                  "TSPL_LOCATION_MASTER.Sales_Tax_Group as LocalTaxGroup,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Local_Tax_GroupName, " & _
                  "TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as InterstateTaxGroup,TSPL_TAX_GROUP_MASTERIS.Tax_Group_Desc as Interstate_Tax_GroupName " & _
                  "FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_TAX_GROUP_MASTER as TSPL_TAX_GROUP_MASTERIS on TSPL_TAX_GROUP_MASTERIS.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTERIS.Tax_Group_Type='S' " & _
                  "WHERE TSPL_LOCATION_MASTER.Location_Code = '" + strloc + "'"
            End If

            dt = clsDBFuncationality.GetDataTable(qry)
            Dim strLocState As String = clsCommon.myCstr(dt.Rows(0)("State"))

            qry = "select Price_Code,price_CodeNon,State,Tin_No from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtVendorNo.Value + "'"
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim strCustState As String = clsCommon.myCstr(dt.Rows(0)("State"))
            Dim strTinNo As String = clsCommon.myCstr(dt.Rows(0)("Tin_No"))
            If clsCommon.myLen(strTinNo) > 0 AndAlso clsCommon.CompairString(strLocState, strCustState) = CompairStringResult.Equal Then
                ddlInvoiceType.SelectedValue = "T"
            Else
                ddlInvoiceType.SelectedValue = "R"
            End If
        Else
            ddlInvoiceType.SelectedValue = ""
        End If
    End Sub
    Function SaveData(ByVal ChekPostBtn As Boolean) As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsDispatchNoteFreshSale()
                'getProvisionBooking()
                obj.AlternateVehicle = clsCommon.myCstr(txtAlternateVehcile.Value)
                obj.ManualVehicle = clsCommon.myCstr(txtManualVehicle.Text)
                obj.Payment_Type = clsCommon.myCstr(strPaymentType)
                obj.Payment_Rate = clsCommon.myCstr(strPaymentRate)
                obj.Charge_For = clsCommon.myCstr(strCharges)
                obj.Payment_Amount = clsCommon.myCdbl(dblTotalAmt)
                obj.Total_Item_Weight = clsCommon.myCdbl(lblTotalWeight.Text)
                obj.Booking_No = fndBookingNo.Value
                If clsCommon.myLen(lblBookingDate.Text) > 0 Then
                    obj.Booking_Date = lblBookingDate.Text
                End If
                obj.Vehicle_Capacity = clsCommon.myCdbl(txtVehicleCapacity.Text)
                obj.Lorry_No = txtLorryNo.Text
                obj.Road_Permit_No = txtRoadPermitNo.Text
                obj.Transporter_Name = txtTransporterName.Text
                obj.Freight = ddlFreight.Text
                obj.Freight_Amount = clsCommon.myCdbl(txtFreightAmt.Text)
                obj.CrateQty = txtCrateQty.Text

                obj.Form_38_No = txtForm38.Text
                obj.Cust_PO_No = txtPONo.Text
                obj.Invoice_Type = ddlInvoiceType.SelectedValue
                obj.Route_No = txtRouteNo.Value
                obj.Route_Desc = lblRouteDesc.Text
                obj.Vehicle_Code = txtVehicleCode.Value
                obj.VehicleNo = lblVehicleNo.Text
                obj.Price_Group_Code = txtPriceGroupCode.Text
                obj.Price_Code = txtPriceCode.Text
                obj.HeadDisc_Per = txtDiscPer.Text
                If obj.HeadDisc_Per > 0 Then
                    obj.HeadDisc_PerAmt = lblInvoiceDiscAmt.Text
                    obj.HeadDisc_Amt = 0
                Else
                    obj.HeadDisc_Amt = lblInvoiceDiscAmt.Text
                    obj.HeadDisc_PerAmt = 0
                End If
                obj.Podate = txtpodate.Text
                '-----------------richa 27/06/2014 Ticket No .BM00000002982-------  
                If clsCommon.myCdbl(txtMannaulInvoiceNo.Value) > 0 Then
                    obj.Mannual_Invoice_No = txtMannaulInvoiceNo.Value
                Else
                    obj.InvoiceManualNowithPrefix = TxtInvoiceManualNoWithPrefix.Text
                End If
                '----------------------------------------------------------------
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Customer_Code = txtVendorNo.Value
                obj.Customer_Name = lblVendorName.Text
                obj.Ref_No = txtRefNo.Text
                obj.Inv_Date = clsCommon.GetPrintDate(dtpInvoice.Value, "dd/MMM/yyyy")
                obj.Challan_Date = clsCommon.GetPrintDate(dtpChallan.Value, "dd/MMM/yyyy")
                obj.Total_Tax_Amt = clsCommon.myCdbl(lblTaxAmt.Text)
                obj.Inv_No = txtInvNo.Text
                obj.Bill_To_Location = txtBillToLocation.Value
                obj.Ship_To_Location = txtShipToLocation.Value
                obj.Comments = txtComment.Text
                obj.On_Hold = chkOnHold.Checked
                obj.Description = txtDesc.Text
                obj.Tax_Group = txtTaxGroup.Value
                obj.Salesman_Code = txtSalesman.Value
                obj.Salesman_Name = lblSalesman.Text
                obj.Is_Internal = chkInternal.Checked
                obj.PROJECT_ID = fndProject.Value

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
                obj.Due_Date = txtDueDate.Value
                obj.Discount_Base = clsCommon.myCdbl(lblAmtWithDiscount.Text)
                obj.Discount_Amt = clsCommon.myCdbl(lblDiscountAmt.Text)
                obj.Amount_Less_Discount = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
                obj.Total_Amt = clsCommon.myCdbl(lblTotRAmt.Text)

                obj.Carrier = txtCarrier.Text
                obj.Vehicle_Code = txtVehicleCode.Value
                obj.VehicleNo = lblVehicleNo.Text
                obj.GRNo = txtGRNo.Text
                obj.GENo = txtGENo.Text

                If txtGEDate.Checked Then
                    obj.GEDate = txtGEDate.Value
                End If
                obj.Item_Type = clsCommon.myCstr(cboItemType.SelectedValue)
                obj.Dept = txtDept.Value
                obj.Dept_Desc = lblDept.Text

                'obj.Against_Sales_Order = txtReqNo.Value
                obj.Against_Delivery_Code = txtReqNo.Value


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
                If rbtnTaxCalAutomatic.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                ElseIf rbtnTaxCalManual.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
                End If
                obj.Is_Create_Auto_Invoice = chkCreateAutoInvoice.Checked
                obj.Is_Create_Auto_Receipt = chkCreateAutoInvoice.Checked AndAlso chkCreateAutoReceipt.Checked
                obj.PROJECT_ID = fndProject.Value
                obj.Arr = New List(Of clsDispatchNoteFreshSaleDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsDispatchNoteFreshSaleDetail()
                    objTr.Sampling = IIf(clsCommon.myCstr(grow.Cells(colSampling).Value) = "Yes", 1, 0)
                    objTr.Scheme_Item_Code = clsCommon.myCstr(grow.Cells(colMainIcode).Value)
                    objTr.Scheme_Item_UOM = clsCommon.myCstr(grow.Cells(colMainIUOM).Value)
                    objTr.Scheme_Qty = clsCommon.myCdbl(grow.Cells(colMainIQty).Value)
                    objTr.Scheme_Type = clsCommon.myCstr(grow.Cells(colSchmCodeType).Value)
                    objTr.Cash_Scheme_Code = clsCommon.myCstr(grow.Cells(colCashSchemeCode).Value)
                    objTr.Cash_Scheme_Type = clsCommon.myCstr(grow.Cells(colCashSchemeType).Value)
                    objTr.Cash_Scheme_Pers = clsCommon.myCdbl(grow.Cells(colCash_Pers).Value)
                    objTr.Cash_Scheme_Amount = clsCommon.myCdbl(grow.Cells(colCash_Amt).Value)


                    objTr.OrgUnit_code = clsCommon.myCstr(grow.Cells(colOrgUnit).Value)
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Row_Type = clsCommon.myCstr(grow.Cells(colRowType).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    'objTr.Bar_Code = clsCommon.myCstr(grow.Cells(colBarCode).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.DeliverQty = clsCommon.myCdbl(grow.Cells(colDelQty).Value)
                    objTr.Free_Qty = clsCommon.myCdbl(grow.Cells(colFreeQty).Value)

                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    'objTr.Order_Code = clsCommon.myCstr(grow.Cells(colOrderNo).Value)
                    objTr.Delivery_Code = clsCommon.myCstr(grow.Cells(colOrderNo).Value)
                    'objTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Disc_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Value)
                    objTr.Disc_Amt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                    objTr.Amt_Less_Discount = clsCommon.myCdbl(grow.Cells(colAmtAfterDis).Value)
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

                    objTr.Scheme_Applicable = clsCommon.myCstr(grow.Cells(colSchemeApplicable).Value)
                    objTr.Scheme_Code = clsCommon.myCstr(grow.Cells(colFromSchemeCode).Value)
                    objTr.Scheme_Item = clsCommon.myCstr(grow.Cells(colSchemeItem).Value)
                    objTr.Item_Tax = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                    objTr.Total_MRP_Amt = clsCommon.myCdbl(grow.Cells(colTotalMRP).Value)
                    objTr.Total_Basic_Amt = clsCommon.myCdbl(grow.Cells(colTotalBasicAmount).Value)
                    objTr.Total_Disc_Amt = clsCommon.myCdbl(grow.Cells(colTotalDiscountAmount).Value)
                    objTr.Cust_Discount = clsCommon.myCdbl(grow.Cells(colcustDiscount).Value)
                    objTr.Total_Cust_Discount = clsCommon.myCdbl(grow.Cells(colTotalCustDiscount).Value)
                    objTr.ActualRate = clsCommon.myCdbl(grow.Cells(colActualCost).Value)
                    objTr.Cust_DiscountQty = clsCommon.myCdbl(grow.Cells(ColCustDiscountQty).Value)
                    objTr.Price_Date = clsCommon.myCDate(grow.Cells(colPriceDateColumn).Value)
                    objTr.Price_code = clsCommon.myCstr(grow.Cells(colPriceCOde).Value)
                    objTr.Abatement_Per = clsCommon.myCdbl(grow.Cells(colAbatementPer).Value)
                    objTr.Abatement_Amt = clsCommon.myCdbl(grow.Cells(colAbatementAmount).Value)
                    objTr.FOC_Item = clsCommon.myCdbl(grow.Cells(ColFOC).Value)
                    objTr.Item_Weight = clsCommon.myCdbl(grow.Cells(colItemWeight).Value)
                    objTr.Conv_Factor = clsCommon.myCdbl(grow.Cells(colConvF).Value)
                    objTr.TotalItem_Weight = clsCommon.myCdbl(grow.Cells(colTotItemWt).Value)
                    objTr.Markup_On = clsCommon.myCstr(grow.Cells(colMarkupOn).Value)
                    objTr.Markup_Percent = clsCommon.myCdbl(grow.Cells(colMarkUpPercentage).Value)
                    objTr.Landing_Cost = clsCommon.myCdbl(grow.Cells(colLandingCost).Value)
                    objTr.CustDiscPer = clsCommon.myCdbl(grow.Cells(colCustDiscPercentage).Value)
                    objTr.HeadDiscAmt = clsCommon.myCdbl(grow.Cells(colHeadDiscamt).Value)
                    objTr.CasdDiscScheme_Code = clsCommon.myCstr(grow.Cells(colCashDiscSchemeCode).Value)
                    objTr.Purchase_Cost = clsCommon.myCdbl(grow.Cells(colPurCost).Value)
                    objTr.OrgRate = clsCommon.myCdbl(grow.Cells(colOrgCost).Value)
                    objTr.PrincipleCode = clsCommon.myCstr(grow.Cells(colPricipleCode).Value)
                    objTr.PrincipleDesc = clsCommon.myCstr(grow.Cells(colPricipleDesc).Value)
                    objTr.vendor_code = clsCommon.myCstr(grow.Cells(colvendorCode).Value)
                    objTr.vendor_desc = clsCommon.myCstr(grow.Cells(colvendorDesc).Value)
                    objTr.HeadDiscPer = clsCommon.myCdbl(grow.Cells(colHeaDDisPer).Value)
                    objTr.HeadDiscPerAmt = clsCommon.myCdbl(grow.Cells(colHeadDisPerAmt).Value)
                    ''objTr.Assessable = clsCommon.myCdbl(grow.Cells(colAssessableRate).Value)
                    ''objTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
                    objTr.Batch_No = clsCommon.myCstr(grow.Cells(colBatchNo).Value)
                    objTr.Bin_No = clsCommon.myCstr(grow.Cells(colBinNo).Value)
                    '' Anubhooti 12-Sep-2014 BM00000003847
                    objTr.Crate = clsCommon.myCdbl(grow.Cells(colCrate).Value)
                    If clsCommon.myLen(grow.Cells(colExpiry).Value) > 0 Then
                        objTr.Expiry_Date = clsCommon.myCDate(grow.Cells(colExpiry).Value, "dd-MM-yyyy")
                    End If
                    If clsCommon.myLen(grow.Cells(colManufactureDate).Value) > 0 Then
                        objTr.MFG_Date = clsCommon.myCDate(grow.Cells(colManufactureDate).Value)
                    End If
                    objTr.Specification = clsCommon.myCstr(grow.Cells(colSpecification).Value)
                    objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    objTr.Is_Mannual_Amt = clsCommon.myCdbl(grow.Cells(colIsMannualAmt).Value)

                    objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)


                    objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))


                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
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

                '' CurrencConversion
                If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
                    obj.CURRENCY_CODE = Me.txtCurrencyCode.Value
                    ''obj.ConvRate = clsCommon.myCdbl(Me.txtConversionRate.Text)
                    If clsCommon.myLen(txtApplicableFrom.Text) > 0 Then
                        obj.ApplicableFrom = Me.txtApplicableFrom.Text
                    Else
                        obj.ApplicableFrom = Nothing
                    End If
                    obj.ConvRate = clsCommon.myCdbl(Me.txtConversionRate.Text)
                Else
                    obj.CURRENCY_CODE = Nothing
                    obj.ConvRate = 1
                    obj.ApplicableFrom = Nothing
                End If
                '' end CurrencyConversion

                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Document_Code)
                    If ChekPostBtn = False And IsDataImported = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub fndProject__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProject._MYValidating
        Dim qry As String = "select PROJECT_CODE as Code,SPECIFICATION,PROJECT_STATUS as Status from TSPL_PJC_PROJECT"
        fndProject.Value = clsCommon.ShowSelectForm("Project Code", qry, "Code", "", fndProject.Value, "", isButtonClicked)
        lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try

            Dim obj As New clsDispatchNoteFreshSale()
            obj = clsDispatchNoteFreshSale.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                btnShowInvoiceJE.Enabled = True
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
                cboItemType.Enabled = False
                txtBillToLocation.Enabled = False

                btnUpdateCustomer.Enabled = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    repoComplete.IsVisible = True
                    repoBalQty.IsVisible = True
                    strPost = True
                End If
                btnPost.Enabled = False
                chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(obj.Customer_Code)
                strPaymentType = clsCommon.myCstr(obj.Payment_Type)
                strPaymentRate = clsCommon.myCstr(obj.Payment_Rate)
                strCharges = clsCommon.myCstr(obj.Charge_For)
                dblTotalAmt = clsCommon.myCdbl(obj.Payment_Amount)
                txtAlternateVehcile.Value = obj.AlternateVehicle
                txtManualVehicle.Text = obj.ManualVehicle
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_Code
                txtpodate.Text = obj.Podate
                txtDate.Value = obj.Document_Date
                txtVendorNo.Value = obj.Customer_Code
                txtPONo.Text = obj.Cust_PO_No
                txtForm38.Text = obj.Form_38_No
                txtDate.Enabled = False
                txtVendorNo.Enabled = False
                chkRateUserCustomer.ToggleState = ClsUserCustomerSettings.GetUserCustomerRateSetting(txtVendorNo.Value)

                lblVendorName.Text = obj.Customer_Name
                txtRefNo.Text = obj.Ref_No
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
                chkOnHold.Checked = obj.On_Hold
                txtDesc.Text = obj.Description
                txtTaxGroup.Value = obj.Tax_Group

                txtComment.Text = obj.Comments
                txtShipToLocation.Value = obj.Ship_To_Location
                txtBillToLocation.Value = obj.Bill_To_Location
                txtInvNo.Text = obj.Inv_No
                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForSale(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If
                chkInternal.Checked = obj.Is_Internal
                cboItemType.SelectedValue = obj.Item_Type
                txtDept.Value = obj.Dept
                lblDept.Text = obj.Dept_Desc

                txtTermCode.Value = obj.Terms_Code

                'lblTermName.Text = obj.Terms_Description

                If obj.Due_Date IsNot Nothing Then
                    txtDueDate.Value = obj.Due_Date
                End If


                lblAmtWithDiscount.Text = clsCommon.myFormat(obj.Discount_Base)
                lblDiscountAmt.Text = clsCommon.myFormat(obj.Discount_Amt)
                lblAmtAfterDiscount.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                lblTotRAmt.Text = clsCommon.myFormat(obj.Total_Amt)
                lblTotalWeight.Text = clsCommon.myFormat(obj.Total_Item_Weight)
                lblTotRAmt1.Text = lblTotRAmt.Text
                lblBillToLocation.Text = obj.BillToLocationName
                lblShipToLocation.Text = obj.ShipToLocationName
                lblTaxGrpName.Text = obj.TaxGroupName
                lblTermName.Text = obj.TermsName

                txtCarrier.Text = obj.Carrier
                lblVehicleNo.Text = obj.VehicleNo
                txtVehicleCode.Value = obj.Vehicle_Code
                txtGRNo.Text = obj.GRNo
                txtGENo.Text = obj.GENo
                If obj.GEDate.HasValue Then
                    txtGEDate.Value = obj.GEDate
                    txtGEDate.Checked = True
                End If
                txtInvoiceNo.Text = obj.Invoice_No
                txtSalesman.Value = obj.Salesman_Code
                lblSalesman.Text = obj.Salesman_Name
                'richa Ticket No.BM00000002982
                txtMannaulInvoiceNo.Value = obj.Mannual_Invoice_No
                TxtInvoiceManualNoWithPrefix.Text = obj.InvoiceManualNowithPrefix
                '
                txtReqNo.Value = obj.Against_Delivery_Code
                fndProject.Value = obj.PROJECT_ID
                lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
                If clsCommon.myLen(txtReqNo.Value) > 0 Then
                    lblProject.Enabled = False
                    fndProject.Enabled = False
                End If
                txtRouteNo.Value = obj.Route_No
                lblRouteDesc.Text = obj.Route_Desc
                txtPriceCode.Text = obj.Price_Code
                txtPriceGroupCode.Text = obj.Price_Group_Code
                txtDiscPer.Text = obj.HeadDisc_Per
                txtDiscAmt.Text = obj.HeadDisc_Amt
                If clsCommon.myLen(txtDiscAmt.Text) <= 0 OrElse clsCommon.myLen(txtDiscPer.Text) <= 0 OrElse clsCommon.myCdbl(txtDiscAmt.Text) = 0 OrElse clsCommon.myCdbl(txtDiscPer.Text) = 0 Then
                    txtDiscPer.Text = obj.HeadDisc_Per
                    If clsCommon.myCdbl(txtDiscPer.Text) = 0 Then
                        txtDiscAmt.Text = obj.HeadDisc_Amt
                        chkDiscountOnAmt.IsChecked = True
                        lblInvoiceDiscAmt.Text = obj.HeadDisc_Amt
                    Else
                        chkDiscountOnRate.IsChecked = True
                        lblInvoiceDiscAmt.Text = obj.HeadDisc_PerAmt
                    End If
                End If
                ddlInvoiceType.SelectedValue = obj.Invoice_Type

                fndBookingNo.Value = obj.Booking_No
                If clsCommon.myLen(obj.Booking_Date) > 0 Then
                    lblBookingDate.Text = obj.Booking_Date
                End If
                txtVehicleCapacity.Text = obj.Vehicle_Capacity
                txtLorryNo.Text = obj.Lorry_No
                txtRoadPermitNo.Text = obj.Road_Permit_No
                txtTransporterName.Text = obj.Transporter_Name
                ddlFreight.Text = obj.Freight
                txtFreightAmt.Text = obj.Freight_Amount
                txtCrateQty.Text = obj.CrateQty
                txtVehicleCode.Value = obj.Vehicle_Code
                lblVehicleNo.Text = obj.VehicleNo

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



                If (clsCommon.myLen(obj.Add_Charge_Code1) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt1
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



                chkCreateAutoInvoice.Checked = obj.Is_Create_Auto_Invoice
                'chkCreateAutoReceipt.Visible = chkCreateAutoInvoice.Checked
                chkCreateAutoReceipt.Checked = obj.Is_Create_Auto_Receipt


                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsDispatchNoteFreshSaleDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Tag = objTr.arrSrItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSampling).Value = IIf(objTr.Sampling = 1, "Yes", "No")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCash_Amt).Value = objTr.Cash_Scheme_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCash_Pers).Value = objTr.Cash_Scheme_Pers
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCashSchemeCode).Value = objTr.Cash_Scheme_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCashSchemeType).Value = objTr.Cash_Scheme_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).Value = objTr.Scheme_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIcode).Value = objTr.Scheme_Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIQty).Value = objTr.Scheme_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIUOM).Value = objTr.Scheme_Item_UOM

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Row_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComplete).Value = IIf(objTr.Status = 0, "No", "Yes")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colBarCode).Value = objTr.Bar_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.Item_Code)
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSOQty).Value = objTr.so_Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = objTr.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFreeQty).Value = objTr.Free_Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = objTr.Delivery_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDelQty).Value = objTr.DeliverQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Location
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.LocationName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = objTr.Disc_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmt).Value = objTr.Disc_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterDis).Value = objTr.Amt_Less_Discount
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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaDDisPer).Value = objTr.HeadDiscPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeadDisPerAmt).Value = objTr.HeadDiscPerAmt
                        If objTr.Expiry_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiry).Value = objTr.Expiry_Date
                        End If

                        If objTr.MFG_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colManufactureDate).Value = objTr.MFG_Date
                        End If


                        If clsCommon.myLen(objTr.Delivery_Code) > 0 Then
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsDeliveryNoteFreshSale.GetBalanceDeliveryQty(objTr.Delivery_Code, objTr.Item_Code, obj.Document_Code, objTr.Unit_code, objTr.MRP)
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsMannualAmt).Value = objTr.Is_Mannual_Amt

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = IIf(objTr.Scheme_Applicable = "Y", "Yes", "No")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFromSchemeCode).Value = objTr.Scheme_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeItem).Value = IIf(objTr.Scheme_Item = "Y", "Yes", "No")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = objTr.Item_Tax
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalMRP).Value = objTr.Total_MRP_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalBasicAmount).Value = objTr.Total_Basic_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalDiscountAmount).Value = objTr.Total_Disc_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colcustDiscount).Value = objTr.Cust_Discount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalCustDiscount).Value = objTr.Total_Cust_Discount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colActualCost).Value = objTr.ActualRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCustDiscountQty).Value = objTr.Cust_DiscountQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = objTr.Price_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = objTr.Price_code
                        txtPriceCode.Text = objTr.Price_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementPer).Value = objTr.Abatement_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementAmount).Value = objTr.Abatement_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = objTr.FOC_Item
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value), clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = objTr.Batch_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemWeight).Value = objTr.Item_Weight
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colConvF).Value = objTr.Conv_Factor
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotItemWt).Value = objTr.TotalItem_Weight
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMarkupOn).Value = objTr.Markup_On
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMarkUpPercentage).Value = objTr.Markup_Percent
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLandingCost).Value = objTr.Landing_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustDiscPercentage).Value = objTr.CustDiscPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeadDiscamt).Value = objTr.HeadDiscAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCashDiscSchemeCode).Value = objTr.CasdDiscScheme_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPurCost).Value = objTr.Purchase_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgCost).Value = objTr.OrgRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPricipleCode).Value = objTr.PrincipleCode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPricipleDesc).Value = objTr.PrincipleDesc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colvendorCode).Value = objTr.vendor_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colvendorDesc).Value = objTr.vendor_desc
                        '' Anubhooti 12-Sep-2014 BM00000003847
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCrate).Value = objTr.Crate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnit).Value = objTr.OrgUnit_code
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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                        gvAC.Rows.AddNew()
                    End If

                    ''For Custom Fields
                    If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                        UcCustomFields1.LoadData(obj.Document_Code)
                    End If
                    clsCustomFieldGrid.FillDataInGrid(obj.Document_Code, MyBase.Form_ID, gv1)
                    ''End of For Custom Fields

                    '' MULTICURRENCY
                    Me.txtCurrencyCode.Value = obj.CURRENCY_CODE
                    Me.txtConversionRate.Text = obj.ConvRate
                    Me.txtApplicableFrom.Text = obj.ApplicableFrom.ToString
                    '' end  MULTICURRENCY
                End If
                SetitemWiseTaxOnlySetting()

                ' ''RefreshReqNo()
                ' ''RefreshGRPNo()
                UcAttachment1.LoadData(obj.Document_Code)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

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
            If rbtnTaxCalAutomatic.IsChecked Then
                Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("ShipmentVendfnddxRate", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='S'", "", "", True))
                Dim intRowNo As Integer = gv2.CurrentRow.Index
                If gv1.RowCount > 0 AndAlso intRowNo >= 0 Then
                    Dim strII As String = clsCommon.myCstr(intRowNo + 1)
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        gv1.Rows(ii).Cells("COLTAXRATE" + strII).Value = dblNewRate
                    Next
                End If
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    UpdateCurrentRow(ii, False)
                Next
                UpdateAllTotals()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub getProvisionBooking()
        Try


            strTransporter = txtTransporterName.Text
            strPaymentType = getPaymentType(txtVehicleCode.Value)
            strPaymentRate = getPaymentRate(txtVehicleCode.Value)
            strCharges = getChargesFor(txtVehicleCode.Value)
            dblTotalAmt = getPaymentAmount()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Function getPaymentType(ByVal strTankerNo As String) As String
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Status  from TSPL_VEHICLE_MASTER where Vehicle_Id ='" & strTankerNo & "'"))
        If clsCommon.CompairString(str, "KM_Range") = CompairStringResult.Equal Then
            str = "Slab Wise KM-Range"
        ElseIf clsCommon.CompairString(str, "Rate/K.M") = CompairStringResult.Equal Then
            str = "Rate Per KM"
        ElseIf clsCommon.CompairString(str, "Day/Diesel") = CompairStringResult.Equal Then
            str = "Rate Per Day + Diesel"
        ElseIf clsCommon.CompairString(str, "Rental") = CompairStringResult.Equal Then
            str = "" '"Rental Basis"
        ElseIf clsCommon.CompairString(str, "Rate/Ltr") = CompairStringResult.Equal Then
            str = "Rate Ltr/KG"
        Else
            str = ""
        End If
        Return str
    End Function
    Function getPaymentRate(ByVal strTankerNo As String) As String
        Dim rValue As String = String.Empty
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select *  from TSPL_VEHICLE_MASTER where Vehicle_Id ='" & strTankerNo & "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim str As String = clsCommon.myCstr(dt.Rows(0)("Status"))
            If clsCommon.myLen(str) > 0 Then
                If clsCommon.CompairString(str, "KM_Range") = CompairStringResult.Equal Then
                    rValue = getSlabDetail(strTankerNo)
                ElseIf clsCommon.CompairString(str, "Rate/K.M") = CompairStringResult.Equal Then
                    rValue = " Rate  : " & clsCommon.myCdbl(dt.Rows(0)("Price_KM")) & " Per KM "
                ElseIf clsCommon.CompairString(str, "Day/Diesel") = CompairStringResult.Equal Then
                    rValue = " Charges Per Day: " & clsCommon.myCdbl(dt.Rows(0)("Shift_Charges")) & Environment.NewLine
                    rValue = rValue & " Avg. KM Per Ltr : " & clsCommon.myCdbl(dt.Rows(0)("Avg_KM_Ltr")) & Environment.NewLine
                    rValue = rValue & " Diesel Rate Per/Ltr : " & clsCommon.myCdbl(dt.Rows(0)("Diesel_Rate"))
                    rValue = ""
                ElseIf clsCommon.CompairString(str, "Rental") = CompairStringResult.Equal Then
                    rValue = "" '" Rent  : " & clsCommon.myCdbl(dt.Rows(0)("Rental_Amount")) & " Per " & clsCommon.myCstr(dt.Rows(0)("Rental_Type"))
                ElseIf clsCommon.CompairString(str, "Rate/Ltr") = CompairStringResult.Equal Then
                    rValue = " Rate : " & clsCommon.myCdbl(dt.Rows(0)("Price_Ltr_KG")) & " Per " & clsCommon.myCstr(dt.Rows(0)("Rate_Type"))
                Else
                    rValue = ""
                End If
            End If
        End If

        Return rValue

    End Function
    Function getSlabDetail(ByVal strTankerNo As String) As String
        Dim rValue As String = String.Empty
        Dim fromValue As Double = 0
        Dim qry As String = "select * from tspl_slab_range_detail where form_id='" & clsUserMgtCode.vhicleMaster & "' and trans_id='" & strTankerNo & "' order by slab_upto"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If i = 0 Then
                    fromValue = 1
                Else
                    fromValue = clsCommon.myCdbl(dt.Rows(i - 1)("Slab_Upto")) + 1
                End If
                rValue = rValue & "From " & CInt(fromValue) & " KM  To  " & CInt(clsCommon.myCdbl(dt.Rows(i)("Slab_Upto"))) & " KM,  Rate: " & clsCommon.myCdbl(dt.Rows(i)("Slab_Rate")) & Environment.NewLine
            Next
        End If
        Return rValue
    End Function
    Function getChargesFor(ByVal strTankerNo As String) As String
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Status  from TSPL_VEHICLE_MASTER where Vehicle_Id ='" & strTankerNo & "'"))
        If clsCommon.CompairString(str, "KM_Range") = CompairStringResult.Equal Then
            str = "Slab From " & getSlabLowerRange() & "  To " & getSlabUpperRange()
        ElseIf clsCommon.CompairString(str, "Rate/K.M") = CompairStringResult.Equal Then
            If getDistance() = -1 Then
                Throw New Exception("Please Map Distance  for route " & txtRouteNo.Value & " ")
            End If
            str = " Total  " & getDistance() & "  KM "
        ElseIf clsCommon.CompairString(str, "Day/Diesel") = CompairStringResult.Equal Then
            str = ""
        ElseIf clsCommon.CompairString(str, "Rental") = CompairStringResult.Equal Then
            str = ""
        ElseIf clsCommon.CompairString(str, "Rate/Ltr") = CompairStringResult.Equal Then
            Dim tCap As Double = lblTotalWeight.Text
            Dim QtyApply As Double = 0
            QtyApply = tCap
            'If tCap > clsCommon.myCdbl(txtNetQty.Text) AndAlso clsCommon.myCdbl(txtNetQty.Text) <> 0 Then
            '    QtyApply = tCap
            'Else
            '    QtyApply = clsCommon.myCdbl(txtNetQty.Text)
            'End If
            str = " Total  " & QtyApply & " KG "
        Else
            str = ""
        End If
        Return str
    End Function
    Function getDistance() As Double
        Dim Distance As Double = 0
        Dim qry As String = " select isnull(Distance,0) from TSPL_ROUTE_MASTER where Route_No='" & txtRouteNo.Value & "'"
        Distance = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If Distance = 0 Then
            Distance = -1
        End If
        Return Distance
    End Function
    Function getSlabLowerRange() As Double
        Dim qry As String = " select  Top 1 Slab_Upto + 1 as [From Range]   from tspl_slab_range_detail  where Trans_ID ='" & txtVehicleCode.Value & "' and Slab_Upto < " & getDistance() & " order by Slab_Upto desc "
        Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If value <= 0 Then
            Throw New Exception(" No Slab found Of this range ")
        End If
        Return value
    End Function
    Function getSlabUpperRange() As Double
        Dim qry As String = " select Top 1 Slab_Upto   from tspl_slab_range_detail  where Trans_ID ='" & txtVehicleCode.Value & "' and Slab_Upto >= " & getDistance() & " order by Slab_Upto  "
        Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If value <= 0 Then
            Throw New Exception(" No Slab found Of this range ")
        End If
        Return value
    End Function

    Function getSlabPaymentAmount() As Double
        Dim dist As Double = getDistance()
        If dist = -1 Then
            Throw New Exception("Please Map Distance for Route No " & txtRouteNo.Value)
        End If
        Dim qry As String = " select Top 1 Slab_Rate   from tspl_slab_range_detail  where Trans_ID ='" & txtVehicleCode.Value & "' and Slab_Upto >= " & getDistance() & " order by Slab_Upto  "
        Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If value <= 0 Then
            Throw New Exception(" No Slab found Of this range ")
        End If
        Return (value * dist)

    End Function

    Function getRatePerKMPaymentAmount() As Double
        Dim qry As String = " select Price_KM  from TSPL_VEHICLE_MASTER  where Vehicle_Id='" & txtVehicleCode.Value & "'"
        Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        Return value

    End Function

    Function getRatePerLTRKGPaymentAmount() As Double
        Dim qry1 As String = " select Rate_Type  from TSPL_VEHICLE_MASTER  where Vehicle_Id='" & txtVehicleCode.Value & "'"
        Dim qry2 As String = " select Price_Ltr_KG  from TSPL_VEHICLE_MASTER  where Vehicle_Id='" & txtVehicleCode.Value & "'"
        Dim Price_Ltr_KG As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry2))
        Dim ItemUom As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1))
        Dim value As Double = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            value += Math.Round(Price_Ltr_KG / clsItemMaster.GetConvertionFactor(grow.Cells(colICode).Value, ItemUom, Nothing), 2)
        Next


        'value = Math.Round(value / clsItemMaster.GetConvertionFactor(fndItemCode.Value, ItemUom, Nothing), 2)
        Return value

    End Function
    Function getPaymentAmount() As Double
        Dim rValue As Double = 0
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Status  from TSPL_VEHICLE_MASTER where Vehicle_Id ='" & txtVehicleCode.Value & "'"))
        If clsCommon.CompairString(str, "KM_Range") = CompairStringResult.Equal Then
            rValue = getSlabPaymentAmount()
        ElseIf clsCommon.CompairString(str, "Rate/K.M") = CompairStringResult.Equal Then
            rValue = getRatePerKMPaymentAmount() * getDistance()
        ElseIf clsCommon.CompairString(str, "Day/Diesel") = CompairStringResult.Equal Then
            rValue = 0
        ElseIf clsCommon.CompairString(str, "Rental") = CompairStringResult.Equal Then
            rValue = 0
        ElseIf clsCommon.CompairString(str, "Rate/Ltr") = CompairStringResult.Equal Then
            Dim tCap As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select capacity  from TSPL_VEHICLE_MASTER  where Vehicle_Id='" & txtVehicleCode.Value & "' "))
            Dim QtyApply As Double = lblTotalWeight.Text
            'QtyApply = tCap
            If tCap > clsCommon.myCdbl(QtyApply) AndAlso clsCommon.myCdbl(QtyApply) <> 0 Then
                QtyApply = tCap
            Else
                QtyApply = clsCommon.myCdbl(QtyApply)
            End If
            rValue = getRatePerLTRKGPaymentAmount() * QtyApply
        Else
            rValue = 0
        End If
        Return rValue
    End Function
    'Function getTransporterName(ByVal strTankerNo As String) As String
    '    Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id ='" & strTankerNo & "'"))
    '    Return str
    'End Function
    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If SaveData(True) Then
                    If (clsDispatchNoteFreshSale.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
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
                                msg = "Level 3 Approval done. Successfully Posted"
                            End If
                        End If
                    End If
                    common.clsCommon.MyMessageBoxShow(msg)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                    If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                        funPrint(txtDocNo.Value)
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
                If (clsDispatchNoteFreshSale.DeleteData(txtDocNo.Value)) Then
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
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Description", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + txtDocNo.Value + "'  and TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS'"
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
    '===changes by shivani [BM00000007739]

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        ' Dim qry As String = "select Document_Code as Code,Sale_Invoice_No as InvoiceNo,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,Customer_Code as [Customer Code], Customer_Name as Customer,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_SD_SHIPMENT_HEAD.Vehicle_Code as Vehicle,TSPL_SD_SHIPMENT_HEAD.Comments,Total_Amt as Amount,case when TSPL_SD_SHIPMENT_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status],stuff((select ',' + isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No   ,'') from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code =TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No where TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE =TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE and TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS' group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No  for xml path ('')),1,1,'' )as [Delivery Code]  from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code  "
        Dim qry As String = "select Document_Code as Code,Sale_Invoice_No as InvoiceNo,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,Customer_Code as [Customer Code], Customer_Name as Customer,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_SD_SHIPMENT_HEAD.Vehicle_Code as Vehicle,TSPL_SD_SHIPMENT_HEAD.Comments,Total_Amt as Amount,case when TSPL_SD_SHIPMENT_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status],stuff((select ',' + isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code   ,'') from  TSPL_SD_SHIPMENT_DETAIL where TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE =TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE  group by TSPL_SD_SHIPMENT_DETAIL.Delivery_Code for xml path ('')),1,1,'' ) as [Delivery Code]  from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code  "

        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " TSPL_SD_SHIPMENT_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS'"
        Else
            whrClas = " TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS' "
        End If


        LoadData(clsCommon.ShowSelectForm("FreshShipmentDocfnd", qry, "Code", whrClas, txtDocNo.Value, "TSPL_SD_SHIPMENT_HEAD.Document_Date desc", isButtonClicked), NavigatorType.Current)
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
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colOrderNo).Value) <= 0 Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentColumn = gv1.Columns(colIName)
                If blnBackCalculation = True Then
                    OpenICodeList(True)
                Else
                    OpenICodeListCurrentCalaculation(True)
                End If
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
            setGridFocus()
            isCellValueChangedOpen = False

        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.T Then
            chkRateDefaultSetting.Visible = Not chkRateDefaultSetting.Visible
            chkRateUserCustomer.Visible = Not chkRateUserCustomer.Visible
        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F6 Then
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If strPost = True Then
                    txtVendorNo.Enabled = True
                    btnUpdateCustomer.Enabled = True
                    blnChangeCustomer = True
                Else
                    clsCommon.MyMessageBoxShow(Me, "Please First create invoice for this dispatch.", Me.Text)
                End If
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIRC
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                '-----------------richa 27/06/2014 Ticket No .BM00000002982------------
                pnlMannualInvoiceNo.Visible = True
                Dim desc As String = ""
                Dim trans As SqlTransaction
                trans = clsDBFuncationality.GetTransactin()
                'desc = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.InvoiceManualNoWithPrefix & "'")) = 0, False, True)

                desc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.InvoiceManualNoWithPrefix, clsFixedParameterCode.InvoiceManualNoWithPrefix, trans))

                If clsCommon.CompairString(desc, "0") = CompairStringResult.Equal Then
                    txtMannaulInvoiceNo.Visible = True
                    TxtInvoiceManualNoWithPrefix.Visible = False
                Else
                    txtMannaulInvoiceNo.Visible = False
                    TxtInvoiceManualNoWithPrefix.Visible = True
                End If
                '-----------------------------------------------------------
                btnReverseAndUnpost.Visible = True
            End If
        End If
    End Sub

    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        txtTermCode.Value = ClsReceivablePaymentTerms.getFinderWithSaleType(txtTermCode.Value, "F", isButtonClicked)
        lblTermName.Text = ClsReceivablePaymentTerms.GetName(txtTermCode.Value)
        SetTermDetails()


    End Sub

    Sub SetTermDetails()
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER where Terms_Code='" + txtTermCode.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
            txtDueDate.Value = txtDate.Value.AddDays(clsCommon.myCdbl(dt.Rows(0)("No Of Days")))
        Else
            lblTermName.Text = ""
        End If
    End Sub

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
        txtTaxGroup.Value = clsCommon.ShowSelectForm("Shipmentfndid", qry, "Code", "Tax_Group_Type='S'", txtTaxGroup.Value, "Code", isButtonClicked)
        SetTaxDetails()

    End Sub

    Sub SetTaxDetails()
        LoadBlankGridTax()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsscrap(txtTaxGroup.Value)
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
            UpdateCurrentRow(ii, False)
        Next
        UpdateAllTotals()
    End Sub

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsscrap(txtTaxGroup.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index, isChangeRate)
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
                        gv1.CurrentRow.Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
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
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            ii = ii + 1
                        Next
                    End If
                Next
            End If
        End If
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

    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        Dim strScheme = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Scheme_Code from (select distinct Scheme_Code from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where Trans_Type='FS' and TSPL_SD_SHIPMENT_HEAD.Document_Code='" & txtDocNo.Value & "'  and Scheme_Code <> ''union all select distinct Cash_Scheme_Code from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where Trans_Type='FS' and TSPL_SD_SHIPMENT_HEAD.Document_Code='" & txtDocNo.Value & "'  and Cash_Scheme_Code <> '' ) aa"))
        Dim qry As String = ""
        If clsCommon.myLen(strScheme) = 0 Then
            qry = " select Cust_Code as Code,Customer_Name as Name,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],Add1 ,Add2,Add3,City_Code as [City],Closing_Date as [Closing Date],Cust_Category_Code as [Customer Category Code],Cust_Group_Code as [Customer Group Code],Cust_Type_Code as [Customer Type Code],Route_No as [Route No],Route_Desc as [Route Description],Price_Code as [Price Code],CSA_Type as [CSA Type],City_Code as [City Code],State,Country,Phone1,Phone2,Fax,Email,WebSite,Contact_Person_Name as [Contact Person Name],Contact_Person_Phone as [Contact Person Phone],Contact_Person_Fax as [Contact Person Fax],Contact_Person_Website as [Contact Person Website],Contact_Person_Email as [Contact Person Email],Terms_Code as [Terms Code],Cust_Account as [Customer Account],Tax_Group as [Tax Group],TAX1,TAX1_Rate as [Tax1 Rate],TAX2,TAX2_Rate as [Tax2 Rate],TAX3,TAX3_Rate as [Tax3 Rate],TAX4,TAX4_Rate as [Tax4 Rate],TAX5,TAX5_Rate as [Tax5 Rate],TAX6,TAX6_Rate as [Tax6 Rate],TAX7,TAX7_Rate as [Tax7 Rate],TAX8,TAX8_Rate as [Tax8 Rate],TAX9,TAX9_Rate as [Tax9 Rate],TAX10,TAX10_Rate as [Tax10 Rate],Payment_Code as [Payment Code],Service_Tax_No as [Service Tax No],Tin_No as [Tin No],Lst_No as [LST No],Form_Type as [Form Type],Channel_Code as [Channel Code],Channel_Desc as [Channel Description],(select case when Status='N' then 'Active' else 'In Active' end ) as [Status],OnHold as [On Hold],Remarks1,Remarks2,Additional1,Additional2,Additional3,Salesman_Code as [Salesman Code],Salesman_Desc as [Salesman Description],Visi_Id as [Visi ID],Visi_Desc as [Visi Description],OutLet_Commossion as [Outlet Commission], Balance_ToDate as [Balance To Date],Credit_Limit as [Credit Limit],Created_By as [Created By],Created_Date as [Created Date],Modify_By as[Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Route_Group as [Route Group],CST,ECC,Range,Collectorate,PAN,Division,Parent_Customer_No as [Parent Customer No],Customer_Class as [Customer Class],Credit_Customer as [Credit Customer],LastInvoice_No as [Last Invoice No],LastInvoice_Date as [Last Invoice Date],price_CodeNon as [Price Code Non],Inter_Branch as [Inter Branch],TRANSACTION_TYPE as [Transaction Type],Credit_Limit_Alert_Type as [Credit Limit alert Type],PIN_Code as [Pin Code],Cust_DOB as [Customer DOB],Cust_Spouse_DOB as [Customer Spouse DOB],Anniversary_Date as [Anniversary Date],Gender,Occation,Agg_Made_Date as [Agg Made Date],Agg_Close_Date as [Agg Close Date],CURRENCY_CODE as [Currency Code],Parent_Customer_YN as [Is Parent Customer],Service_Dealer_Code as [Service Dealer Code],TDM_Code as [TDM Code],Distributor_Code as [Distributor Code],IsDistributor as [Is Distributor],Price_Group_Code as [Price Group Code] from tspl_customer_master"
            txtVendorNo.Value = clsCommon.ShowSelectForm("FreshDispatchCustomer", qry, "Code", "TSPL_CUSTOMER_MASTER.price_CodeNon='" & txtPriceCode.Text & "' and Cust_Code not in (select distinct Cust_Code from TSPL_SCHEME_BENEFICIARY left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_BENEFICIARY.Scheme_Code=TSPL_SCHEME_DETAIL_NEW.Scheme_Code left outer join TSPL_SCHEME_MASTER_NEW  on TSPL_SCHEME_MASTER_NEW.Scheme_Code=TSPL_SCHEME_DETAIL_NEW.Scheme_Code where MainItem_Code in (select Item_Code from TSPL_SD_SHIPMENT_DETAIL where DOCUMENT_CODE='" & txtDocNo.Value & "')  and Status='Active') ", txtVendorNo.Value, "Code", isButtonClicked)
        Else
            qry = " select final.Cust_Code as Code  ,TSPL_CUSTOMER_MASTER.Customer_Name as Name,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],Add1 ,Add2,Add3,City_Code as [City],Closing_Date as [Closing Date],Cust_Category_Code as [Customer Category Code],Cust_Group_Code as [Customer Group Code],Cust_Type_Code as [Customer Type Code],Route_No as [Route No],Route_Desc as [Route Description],Price_Code as [Price Code],CSA_Type as [CSA Type],City_Code as [City Code],State,Country,Phone1,Phone2,Fax,Email,WebSite,Contact_Person_Name as [Contact Person Name],Contact_Person_Phone as [Contact Person Phone],Contact_Person_Fax as [Contact Person Fax],Contact_Person_Website as [Contact Person Website],Contact_Person_Email as [Contact Person Email],Terms_Code as [Terms Code],Cust_Account as [Customer Account],Tax_Group as [Tax Group],TAX1,TAX1_Rate as [Tax1 Rate],TAX2,TAX2_Rate as [Tax2 Rate],TAX3,TAX3_Rate as [Tax3 Rate],TAX4,TAX4_Rate as [Tax4 Rate],TAX5,TAX5_Rate as [Tax5 Rate],TAX6,TAX6_Rate as [Tax6 Rate],TAX7,TAX7_Rate as [Tax7 Rate],TAX8,TAX8_Rate as [Tax8 Rate],TAX9,TAX9_Rate as [Tax9 Rate],TAX10,TAX10_Rate as [Tax10 Rate],Payment_Code as [Payment Code],Service_Tax_No as [Service Tax No],Tin_No as [Tin No],Lst_No as [LST No],Form_Type as [Form Type],Channel_Code as [Channel Code],Channel_Desc as [Channel Description],(select case when Status='N' then 'Active' else 'In Active' end ) as [Status],OnHold as [On Hold],Remarks1,Remarks2,Additional1,Additional2,Additional3,Salesman_Code as [Salesman Code],Salesman_Desc as [Salesman Description],Visi_Id as [Visi ID],Visi_Desc as [Visi Description],OutLet_Commossion as [Outlet Commission], Balance_ToDate as [Balance To Date],Credit_Limit as [Credit Limit],Created_By as [Created By],Created_Date as [Created Date],Modify_By as[Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Route_Group as [Route Group],CST,ECC,Range,Collectorate,PAN,Division,Parent_Customer_No as [Parent Customer No],Customer_Class as [Customer Class],Credit_Customer as [Credit Customer],LastInvoice_No as [Last Invoice No],LastInvoice_Date as [Last Invoice Date],price_CodeNon as [Price Code Non],Inter_Branch as [Inter Branch],TRANSACTION_TYPE as [Transaction Type],Credit_Limit_Alert_Type as [Credit Limit alert Type],PIN_Code as [Pin Code],Cust_DOB as [Customer DOB],Cust_Spouse_DOB as [Customer Spouse DOB],Anniversary_Date as [Anniversary Date],Gender,Occation,Agg_Made_Date as [Agg Made Date],Agg_Close_Date as [Agg Close Date],CURRENCY_CODE as [Currency Code],Parent_Customer_YN as [Is Parent Customer],Service_Dealer_Code as [Service Dealer Code],TDM_Code as [TDM Code],Distributor_Code as [Distributor Code],IsDistributor as [Is Distributor],Price_Group_Code as [Price Group Code] from (select distinct TSPL_SCHEME_BENEFICIARY.Cust_Code from TSPL_SCHEME_BENEFICIARY  where Scheme_Code in (select Scheme_Code from (select distinct Scheme_Code from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where Trans_Type='FS' and TSPL_SD_SHIPMENT_HEAD.Document_Code='" & txtDocNo.Value & "'  and Scheme_Code <> ''" & _
      "union all " & _
      "select distinct Cash_Scheme_Code from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where Trans_Type='FS' and TSPL_SD_SHIPMENT_HEAD.Document_Code='" & txtDocNo.Value & "'  and Cash_Scheme_Code <> '' ) aa) ) final left outer join tspl_customer_master on final.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code "
            txtVendorNo.Value = clsCommon.ShowSelectForm("FreshDispatchCustomer", qry, "Code", "TSPL_CUSTOMER_MASTER.price_CodeNon='" & txtPriceCode.Text & "' ", txtVendorNo.Value, "Code", isButtonClicked)

        End If


        qry += " where 2=2 and TSPL_CUSTOMER_MASTER.Cust_Code ='" + txtVendorNo.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Name"))
        End If
    End Sub
    Private Sub btnUpdateCustomer_Click(sender As Object, e As EventArgs) Handles btnUpdateCustomer.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 And btnPost.Enabled = False Then
                If UpdateCustomerAfterPosting() Then
                    clsCommon.MyMessageBoxShow(Me, "Customer updated successfully.", Me.Text)
                    btnUpdateCustomer.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function UpdateCustomerAfterPosting() As Boolean
        Dim strARInvoiceNo As String = clsDBFuncationality.getSingleValue("select Document_No from  TSPL_Customer_Invoice_Head where against_sale_no='" & clsCommon.myCstr(txtInvoiceNo.Text) & "'")
        Dim strRemarks = " AR invoice for customer: " + txtVendorNo.Value + " - " + lblVendorName.Text + "  For Sale Invoice No " & txtInvoiceNo.Text & " "
        Dim strReceiptNo = clsDBFuncationality.getSingleValue("select Receipt_No from TSPL_RECEIPT_DETAIL where Document_No='" & strARInvoiceNo & "'")
        Dim strSaleReturnNo = clsDBFuncationality.getSingleValue("select Invoice_Code from TSPL_SD_SALE_RETURN_DETAIL where Invoice_Code='" & txtInvoiceNo.Text & "'")
        If clsCommon.myLen(strReceiptNo) > 0 Then
            clsCommon.MyMessageBoxShow("Receipt has been created for this invoice " & txtInvoiceNo.Text)
            Exit Function
        ElseIf clsCommon.myLen(strSaleReturnNo) > 0 Then
            clsCommon.MyMessageBoxShow("Sale Return has been created for this invoice " & txtInvoiceNo.Text)
            Exit Function
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 AndAlso blnChangeCustomer = True AndAlso strPost = True Then

                Dim qry = "update TSPL_JOURNAL_MASTER  set CustVend_Code= '" & txtVendorNo.Value & "',CustVend_Name='" & lblVendorName.Text & "',Remarks='" & strRemarks & "'  where Source_Doc_No='" + clsCommon.myCstr(strARInvoiceNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update TSPL_Customer_Invoice_Head  set Customer_Code= '" & txtVendorNo.Value & "',Customer_Name='" & lblVendorName.Text & "'  where Document_No='" + clsCommon.myCstr(strARInvoiceNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update TSPL_SD_SALE_INVOICE_HEAD  set Customer_Code= '" & txtVendorNo.Value & "',Is_CustomerChanged=1  where Document_Code='" + clsCommon.myCstr(txtInvoiceNo.Text) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update TSPL_JOURNAL_MASTER  set CustVend_Code= '" & txtVendorNo.Value & "',CustVend_Name='" & lblVendorName.Text & "'  where Source_Doc_No='" + clsCommon.myCstr(txtDocNo.Value) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update TSPL_INVENTORY_MOVEMENT  set Cust_Code= '" & txtVendorNo.Value & "',Cust_Name='" & lblVendorName.Text & "'  where Source_Doc_No='" + clsCommon.myCstr(txtDocNo.Value) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update TSPL_SD_SHIPMENT_HEAD  set Customer_Code= '" & txtVendorNo.Value & "',Is_CustomerChanged=1  where Document_Code='" + clsCommon.myCstr(txtDocNo.Value) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "select distinct Delivery_Code from  tspl_sd_shipment_detail where DOCUMENT_CODE='" & txtDocNo.Value & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt.Rows.Count > 0 Then
                    For Each row As DataRow In dt.Rows
                        Dim Sql = "update TSPL_DELIVERY_NOTE_MASTER_FRESHSALE  set Customer_Code= '" & txtVendorNo.Value & "',Is_CustomerChanged=1  where Document_No='" + clsCommon.myCstr(row("Delivery_Code")) + "'"
                        clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                        Dim strBookingNo As String = clsDBFuncationality.getSingleValue("select Booking_No from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where Document_No='" & clsCommon.myCstr(row("Delivery_Code")) & "'", trans)
                        Sql = "update TSPL_BOOKING_DETAIL  set Cust_Code= '" & txtVendorNo.Value & "',Is_CustomerChanged=1  where Document_No='" & strBookingNo & "' and Delivery_No='" + clsCommon.myCstr(row("Delivery_Code")) + "'"
                        clsDBFuncationality.ExecuteNonQuery(Sql, trans)
                    Next
                End If

                trans.Commit()

            End If
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Sub SetTaxGroup()
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address,TSPL_CUSTOMER_MASTER.Terms_Code as [Term Code] , TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER.Terms_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Salesman_Code as [Salesman Code],Salesman_Desc as Salesman  "
        qry += " from TSPL_CUSTOMER_MASTER "
        qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
        qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
        qry += " left outer join TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER on TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
        qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'"

        qry += " where 2=2 and TSPL_CUSTOMER_MASTER.Cust_Code ='" + txtVendorNo.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Name"))
            txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Term Code"))
            lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Term Description"))
            txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax Group"))
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax Group Description"))
            txtSalesman.Value = clsCommon.myCstr(dt.Rows(0)("Salesman Code"))
            lblSalesman.Text = clsCommon.myCstr(dt.Rows(0)("Salesman"))

            txtDate.Enabled = False
            txtVendorNo.Enabled = False
            chkRateUserCustomer.ToggleState = ClsUserCustomerSettings.GetUserCustomerRateSetting(txtVendorNo.Value)
            SetMultiCurrencyVisibility()
        Else
            lblVendorName.Text = ""
            txtTermCode.Value = ""
            lblTermName.Text = ""
            txtTaxGroup.Value = ""
            lblTaxGrpName.Text = ""
            txtSalesman.Value = ""
            lblSalesman.Text = ""
            Me.txtCurrencyCode.Value = ""
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""
        End If

        '' priti change start here
        qry = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State, " & _
        "TSPL_LOCATION_MASTER.Sales_Tax_Group as LocalTaxGroup,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Local_Tax_GroupName, " & _
        "TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as InterstateTaxGroup,TSPL_TAX_GROUP_MASTERIS.Tax_Group_Desc as Interstate_Tax_GroupName " & _
        "FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_TAX_GROUP_MASTER as TSPL_TAX_GROUP_MASTERIS on TSPL_TAX_GROUP_MASTERIS.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTERIS.Tax_Group_Type='S' WHERE TSPL_LOCATION_MASTER.Location_Code = '" + Convert.ToString(txtBillToLocation.Value) + "'"
        Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim loc As String = clsCommon.myCstr(dtLocation.Rows(0)("Excisable"))
        Dim strLocState As String = clsCommon.myCstr(dtLocation.Rows(0)("State"))
        If clsCommon.CompairString(loc, "T") = CompairStringResult.Equal Then
            strExcise = True
        Else
            strExcise = False
        End If
        If clsCommon.myLen(txtVendorNo.Value) > 0 Then
            qry = "select Price_Code,price_CodeNon,State,price_group_code from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtVendorNo.Value + "'"
            dt = clsDBFuncationality.GetDataTable(qry)

            If clsCommon.CompairString(loc, "T") = CompairStringResult.Equal OrElse clsCommon.CompairString(loc, "Y") = CompairStringResult.Equal Then
                txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            Else
                txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("price_CodeNon"))
            End If
            If clsCommon.myLen(txtPriceCode.Text) = 0 Then
                txtPriceGroupCode.Text = clsCommon.myCstr(dt.Rows(0)("price_group_code"))
            End If
            txtVendorNo.Enabled = False

            If clsCommon.CompairString(clsCommon.myCstr(dtLocation.Rows(0)("State")), clsCommon.myCstr(dt.Rows(0)("State"))) = CompairStringResult.Equal Then
                txtTaxGroup.Value = clsCommon.myCstr(dtLocation.Rows(0)("LocalTaxGroup"))
                lblTaxGrpName.Text = clsCommon.myCstr(dtLocation.Rows(0)("Local_Tax_GroupName"))
            Else
                txtTaxGroup.Value = clsCommon.myCstr(dtLocation.Rows(0)("InterstateTaxGroup"))
                lblTaxGrpName.Text = clsCommon.myCstr(dtLocation.Rows(0)("Interstate_Tax_GroupName"))
            End If

        End If


        '' priti change ends here
        SetTaxDetails()
        SetTermDetails()
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
        Dim WhrCls As String = "  Location_Type='Physical' and CSA_Type='N' and Is_Section='N' and Is_Sub_Location='N' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        txtBillToLocation.Value = clsCommon.ShowSelectForm("ShipmentMasteidfndr", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))




    End Sub

    Private Sub txtShipToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipToLocation._MYValidating
        If clsCommon.myLen(txtBillToLocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Location first", Me.Text)
            txtBillToLocation.Focus()
            Exit Sub
        End If
        If clsCommon.myLen(txtVendorNo.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Customer first", Me.Text)
            txtVendorNo.Focus()
            Exit Sub
        End If

        Dim qry As String = " select TSPL_SHIP_TO_LOCATION.Ship_To_Code as [Code],TSPL_SHIP_TO_LOCATION.Ship_To_Desc as [Description],TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code as[Customer Code] ,TSPL_SHIP_TO_LOCATION.Ship_To_Type_Desc  as [Customer Discription], replace(case when ISNULL (TSPL_CUSTOMER_MASTER.Add1,'')='' then '' else TSPL_CUSTOMER_MASTER.add1 +',' end + case when ISNULL (TSPL_CUSTOMER_MASTER.Add2,'')='' then '' else TSPL_CUSTOMER_MASTER.add2 +',' end + case when ISNULL (TSPL_CUSTOMER_MASTER.Add3,'')='' then '' else TSPL_CUSTOMER_MASTER.add3 +',' end  ,',,',',') as [Customer Address] ,replace(case when ISNULL (TSPL_SHIP_TO_LOCATION.Add1,'')='' then '' else TSPL_SHIP_TO_LOCATION.add1 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add2,'')='' then '' else TSPL_SHIP_TO_LOCATION.add2 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add3,'')='' then '' else TSPL_SHIP_TO_LOCATION.add3 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add4,'')='' then '' else TSPL_SHIP_TO_LOCATION.add4 +',' end ,',,',',') as [Ship to Address],TSPL_SHIP_TO_LOCATION.CST_No as [CST NO],TSPL_SHIP_TO_LOCATION.Tin_No as [TIN No]  from TSPL_SHIP_TO_LOCATION left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code "
        txtShipToLocation.Value = clsCommon.ShowSelectForm("ShipmentShindrlter", qry, "Code", "Ship_To_Type_Code='" & clsCommon.myCstr(txtVendorNo.Value) & "' and loc_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'", txtShipToLocation.Value, "Code", isButtonClicked)
        'txtShipToLocation.Value = clsShipToLocation.getFinder("Ship_To_Type_Code='" & clsCommon.myCstr(txtVendorNo.Value) & "' and loc_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'", txtShipToLocation.Value, isButtonClicked)
        lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ship_To_Desc from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipToLocation.Value + "'"))
    End Sub

    Private Sub btnRequistionItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SelectMRNItems()
    End Sub

    Sub SelectMRNItems()
        isInsideLoadData = True
        Dim frm As New frmPendingDelivery()
        frm.VendorCode = txtVendorNo.Value
        frm.strCurrCode = txtDocNo.Value

        frm.ShowDialog()
        LoadBlankGrid()
        Dim objOrderHead As clsDeliveryNoteFreshSale
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            If clsCommon.myLen(frm.ArrReturn(0).Document_No) > 0 Then
                objOrderHead = clsDeliveryNoteFreshSale.GetData(frm.ArrReturn(0).Document_No, NavigatorType.Current)
                If objOrderHead IsNot Nothing AndAlso clsCommon.myLen(objOrderHead.Document_No) > 0 Then
                    If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
                        txtBillToLocation.Value = objOrderHead.Location_Code
                        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtBillToLocation.Value & "'"))
                    End If
                    If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                        txtVendorNo.Value = frm.VendorCode
                        lblVendorName.Text = frm.VendorName
                    End If

                    If (clsCommon.myLen(fndBookingNo.Value) <= 0) Then
                        fndBookingNo.Value = objOrderHead.Booking_No
                        lblBookingDate.Text = objOrderHead.Booking_Date
                    End If
                    If (clsCommon.myLen(txtRouteNo.Value)) <= 0 Then
                        txtRouteNo.Value = objOrderHead.Route_No
                        lblRouteDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT isnull(Route_Desc,'') As Route_No FROM TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code  ='" + txtRouteNo.Value + "'"))
                    End If
                    If (clsCommon.myLen(txtVehicleCode.Value)) <= 0 Then
                        txtVehicleCode.Value = objOrderHead.Lorry_No
                        lblVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT isnull(Number,'') As Number FROM TSPL_VEHICLE_MASTER where TSPL_VEHICLE_MASTER.Vehicle_Id  ='" + txtVehicleCode.Value + "'"))
                    End If
                    If (clsCommon.myLen(txtVehicleCapacity.Text) <= 0) Then
                        txtVehicleCapacity.Text = objOrderHead.Vehicle_Capacity
                        txtLorryNo.Text = objOrderHead.Lorry_No
                        txtRoadPermitNo.Text = objOrderHead.Road_Permit_No
                        txtTransporterName.Text = objOrderHead.Transporter_Name
                        ddlFreight.Text = objOrderHead.Freight
                        txtFreightAmt.Text = objOrderHead.Freight_Amount
                        txtComment.Text = objOrderHead.Comments
                    End If

                    LoadBlankGridAC()
                    gvAC.Rows.AddNew()


                End If

            End If
            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If


            'Dim mrnno As String

            Dim arr As New List(Of String)
            For Each obj As clsDeliveryNoteFreshSaleDetail In frm.ArrReturn
                If IsValidItem(obj) Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = "Item"
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = obj.Document_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDelQty).Value = obj.Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = obj.Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = "No"
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeItem).Value = "No"
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = obj.Price_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
                    If obj.Price_Date.HasValue Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = obj.Price_Date
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colConvF).Value = obj.Conv_Factor
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgCost).Value = obj.Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnit).Value = obj.Unit_code
                End If
            Next
            'End If



            SetitemWiseTaxSetting(False, False)
            For ii As Integer = 0 To gv1.RowCount - 1
                UpdateCurrentRow(ii, False)
            Next

            UpdateAllTotals()
            SetTaxGroup()
        End If
        isInsideLoadData = False
        UpdateAllTotals()
        RefreshReqNo()

    End Sub

    Function IsValidItem(ByVal obj As clsDeliveryNoteFreshSaleDetail)
        'If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
        '    txtTaxGroup.Value = obj.SOTax_Group
        '    SetTaxDetails()
        'End If
        ''If Not clsCommon.CompairString(txtTaxGroup.Value, obj.MRNTax_Group) = CompairStringResult.Equal Then
        ''    common.clsCommon.MyMessageBoxShow("Item : " + obj.Item_Desc + " not Added Current Tax Group : " + txtTaxGroup.Value + " MRN No: " + obj.MRN_No + "  contain Tax Group :" + obj.MRNTax_Group + Environment.NewLine)
        ''    Return False
        ''End If
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colOrderNo).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
            Dim strSchemeItem As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemeItem).Value)
            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strReqCode, obj.Document_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "Order No : " + obj.Document_No + "  Item : " + obj.Item_Desc + Environment.NewLine + ""
                If dblMRP > 0 Then
                    strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
                End If
                common.clsCommon.MyMessageBoxShow(strMsg)
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) AndAlso rbtnTaxCalAutomatic.IsChecked Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
                frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmtAfterDis).Value)
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
                        UpdateCurrentRow(gv1.CurrentRow.Index, False)
                        UpdateAllTotals()
                    End If
                End If
            ElseIf gv1.CurrentColumn Is gv1.Columns(colComplete) AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
                Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                Dim intSNo As Integer = Convert.ToInt32((clsCommon.myCdbl(gv1.CurrentRow.Cells(colLineNo).Value)))
                Dim strStatus As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colComplete).Value)
                If clsCommon.myLen(txtDocNo.Value) > 0 AndAlso clsCommon.myLen(strICode) > 0 AndAlso intSNo > 0 AndAlso clsCommon.CompairString(strStatus, "No") = CompairStringResult.Equal Then
                    If common.clsCommon.MyMessageBoxShow("Do you want to complete the item " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value), Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        If clsDispatchNoteFreshSaleDetail.CompleteSRN(txtDocNo.Value, strICode, intSNo) Then
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
            myMessages.blankValue(Me, "Invoice not found to Print", Me.Text)
        Else
            funPrint(txtDocNo.Value)
        End If
    End Sub

    Public Function GetAttachQry(ByVal StrCode As String) As String

        'Dim Qry As String = "  select TSPL_SD_SHIPMENT_HEAD.Document_Code ,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SHIPMENT_HEAD.Lorry_No ,TSPL_SD_SHIPMENT_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc as Particulars,TSPL_SD_SHIPMENT_DETAIL.Crate as QtyCrates ,TSPL_SD_SHIPMENT_DETAIL.Unit_code ,TSPL_SD_SHIPMENT_DETAIL.Qty as QtyPCS,TSPL_SD_SHIPMENT_DETAIL.Scheme_Item as FreeSchemeInLitres ,TSPL_SD_SHIPMENT_DETAIL.Item_Cost as RatePerPcs,convert(Decimal,TSPL_SD_SHIPMENT_DETAIL.Qty*TSPL_SD_SHIPMENT_DETAIL.Item_Cost,2) as valueInRs,'' as schemeInCrates ,'' GrandTotalCrates ,TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_add1 ,TSPL_COMPANY_MASTER.Add2 as comp_add2 ,TSPL_COMPANY_MASTER.Add3 as comp_add3 ,TSPL_COMPANY_MASTER.Fax as comp_Fax ,TSPL_COMPANY_MASTER.Email as comp_Email,ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+  Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,TSPL_COMPANY_MASTER.Tin_No as comp_tinNo,TSPL_SD_SHIPMENT_HEAD.Customer_Code  as cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as cust_add1 ,TSPL_CUSTOMER_MASTER.Add2 as cust_add2 ,TSPL_CUSTOMER_MASTER.Add3 cust_add3,ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')+  Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End as CustPhone ,TSPL_CUSTOMER_MASTER.Fax as cust_fax ,TSPL_CUSTOMER_MASTER.Email as cust_Email,TSPL_CUSTOMER_MASTER.WebSite as cust_website "

        'Qry += "  from TSPL_SD_SHIPMENT_DETAIL "
        'Qry += " LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD .Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE "
        'Qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code"
        'Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_SD_SHIPMENT_HEAD.Comp_Code "
        'Qry += " left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER .Cust_Code  =TSPL_SD_SHIPMENT_HEAD .Customer_Code "

        'Qry += " where 2=2 "
        'Qry += "  and  TSPL_SD_SHIPMENT_HEAD.Document_Code = '" + txtDocNo.Value + "'"

        'Dim Qry As String = "  select TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end"

        'Qry += "  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end"
        'Qry += "  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end"
        'Qry += "  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end"
        'Qry += "  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end"

        'Qry += "+ Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end "
        'Qry += "+  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End "
        'Qry += "  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end "


        'Qry += " as Comp_Address, TSPL_SD_SHIPMENT_HEAD.RoundOffAmount, TSPL_LOCATION_MASTER.Add1  as Loc_ADd1,TSPL_LOCATION_MASTER.Add2  as LOC_ADD2,TSPL_LOCATION_MASTER.Add3 as LOC_ADD3,"
        Dim Qry As String = " select TSPL_ITEM_MASTER.HSN_Code,TSPL_CUSTOMER_MASTER.GSTNO as Cust_GSTIN_No,Customer_State_Master.GST_STATE_Code as Cust_Gst_State,TSPL_LOCATION_MASTER.GSTNO as Loc_GSTIN_No,Tspl_state_master_for_Location.GST_STATE_Code as Loc_Gst_State, TSPL_SD_SHIPMENT_Head.Sale_Invoice_No,Case When ISNULL(TSPL_SD_SHIPMENT_Head.ManualVehicle,'')<>'' Then TSPL_SD_SHIPMENT_Head.ManualVehicle WHEN ISNULL(TSPL_SD_SHIPMENT_Head.AlternateVehicle,'')<>'' Then TSPL_VEHICLE_MASTER.Number Else TSPL_SD_SHIPMENT_Head.vehicleNo End as   VehicleNo,TSPL_LOCATION_MASTER.State  as Loc_State_code,Tspl_state_master_for_Location.STATE_NAME  as Loc_State_name ,Convert(varchar,TSPL_SD_SHIPMENT_Head.Sale_Invoice_Date,103) as Sale_Invoice_Date, TSPL_SD_SHIPMENT_HEAD.RoundOffAmount, TSPL_LOCATION_MASTER.Add1  as Loc_ADd1,TSPL_LOCATION_MASTER.Add2  as LOC_ADD2,TSPL_LOCATION_MASTER.Add3 as LOC_ADD3,"
        Qry += " case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When  "
        Qry += " ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as LOCPhone,TSPL_LOCATION_MASTER.TIN_No as Loc_TIN_NO, "
        Qry += " TSPL_SD_SHIPMENT_HEAD.Document_Code ,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date ,"
        Qry += " TSPL_SD_SHIPMENT_HEAD.Lorry_No ,TSPL_SD_SHIPMENT_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc as Particulars,"
        Qry += " TSPL_SD_SHIPMENT_DETAIL.Crate as QtyCrates ,TSPL_SD_SHIPMENT_DETAIL.Unit_code ,convert(Decimal(18,2), TSPL_SD_SHIPMENT_DETAIL.Qty"
        Qry += " *TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as QtyPCS, "
        Qry += " (select SUM(Qty ) from TSPL_SD_SHIPMENT_DETAIL as inn where DOCUMENT_CODE='" + StrCode + "' and inn.Scheme_Item='Y' and inn.Scheme_Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code) as free_qty,"
        Qry += " TSPL_SD_SHIPMENT_DETAIL.Scheme_Item as FreeSchemeInLitres ,convert(DECIMAL(18,2),TSPL_SD_SHIPMENT_DETAIL.Amount/"
        Qry += " (TSPL_SD_SHIPMENT_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) as RatePerPcs,TSPL_SD_SHIPMENT_DETAIL.Amount  as valueInRs, "
        ' Qry += " ,(case when TSPL_SD_SHIPMENT_DETAIL.Scheme_Item='N' then axa1.qty else 0 end) as schemeInCrates ,"
        Qry += "   (select SUM(Crate) from TSPL_SD_SHIPMENT_DETAIL as inn where DOCUMENT_CODE='" + StrCode + "' and inn.Scheme_Item='Y' and inn.Scheme_Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code) as schemeInCrates, "
        Qry += " '' GrandTotalCrates , TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_add1 ,TSPL_COMPANY_MASTER.Add2 as "
        Qry += " comp_add2 ,TSPL_COMPANY_MASTER.Add3 as comp_add3 ,TSPL_COMPANY_MASTER.Fax as comp_Fax ,TSPL_COMPANY_MASTER.Email as comp_Email,"
        Qry += " case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL"
        Qry += " (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,"
        Qry += " TSPL_COMPANY_MASTER.Tin_No as comp_tinNo,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,TSPL_SD_SHIPMENT_HEAD."
        Qry += " Customer_Code  as cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as cust_add1 ,TSPL_CUSTOMER_MASTER."
        Qry += " Add2 as cust_add2 ,TSPL_CUSTOMER_MASTER.Add3 cust_add3,case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then ''"
        Qry += " else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ "
        Qry += " TSPL_CUSTOMER_MASTER.Phone2 Else'' End as CustPhone ,TSPL_CUSTOMER_MASTER.Fax as cust_fax ,TSPL_CUSTOMER_MASTER.Email as "
        Qry += " cust_Email,TSPL_CUSTOMER_MASTER.WebSite as cust_website  "
        Qry += " from TSPL_SD_SHIPMENT_DETAIL  "
        Qry += " LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD .Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  "
        Qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL"
        Qry += " .UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code"
        Qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code"
        Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_SD_SHIPMENT_HEAD.Comp_Code  "
        Qry += " left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER .Cust_Code  =TSPL_SD_SHIPMENT_HEAD .Customer_Code"
        'Qry += " left outer join (select DOCUMENT_CODE,sum(isnull(Qty,0)) as qty from TSPL_SD_SHIPMENT_DETAIL where Scheme_Item='Y' group by "
        'Qry += " DOCUMENT_CODE) axa on axa.DOCUMENT_CODE=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE "
        'Qry += "  left outer join (select DOCUMENT_CODE,sum(isnull(Crate,0)) as qty from TSPL_SD_SHIPMENT_DETAIL where Scheme_Item='Y' group"
        'Qry += " by DOCUMENT_CODE) axa1 on axa1.DOCUMENT_CODE=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  "
        Qry += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SHIPMENT_HEAD .Bill_To_Location "
        'Qry += " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code "
        'Qry += " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State "
        Qry += " left outer join TSPL_STATE_MASTER as Tspl_state_master_for_Location on Tspl_state_master_for_Location.STATE_CODE =TSPL_LOCATION_MASTER.State "
        Qry += " LEFT OUTER JOIN TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle "
        Qry += " left outer join TSPL_STATE_MASTER as Customer_State_Master on TSPL_CUSTOMER_MASTER.State=Customer_State_Master.STATE_CODE "

        Qry += " where 2=2 and  TSPL_SD_SHIPMENT_HEAD.Document_Code = '" + StrCode + "' and TSPL_SD_SHIPMENT_DETAIL.Scheme_Item='N' order by tspl_item_master.sku_seq"



        Return Qry

    End Function
    Public Sub funPrint(ByVal StrCode As String)

        Try
            atchqry = GetAttachQry(StrCode)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)

            If dt.Rows.Count > 0 Then
                SetItemWiseTax(dt, StrCode)
                'KwalitySalesReportViewer.funreport(dt, "DispatchChallanFresh", "Dispatch")
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "DispatchChallanFresh", "Dispatch", txtDate.Value, "rptCompanyAddress.rpt")
                frmCRV = Nothing
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function SetItemWiseTax(ByVal dtAfterModify As DataTable, ByVal strShipFrm As String) As DataTable
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
        qry += " from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + strShipFrm + "' "
        qry += " union all "
        qry += " select TAX2 as Tax,TAX2_Rate as Rate,TAX2_Amt as Amt "
        qry += " from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX3 as Tax,TAX3_Rate as Rate,TAX3_Amt as Amt "
        qry += " from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX4 as Tax,TAX4_Rate as Rate,TAX4_Amt as Amt "
        qry += " from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX5 as Tax,TAX5_Rate as Rate,TAX5_Amt as Amt "
        qry += " from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + strShipFrm + "'   "
        qry += " union all "
        qry += " select TAX6 as Tax,TAX6_Rate as Rate,TAX6_Amt as Amt "
        qry += " from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + strShipFrm + "'   "
        qry += " )xxx "
        qry += " group by Tax,Rate   having SUM(Amt)>0   "


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                For ii As Integer = 1 To 5
                    Dim strCol As String = "TAX" + clsCommon.myCstr(ii) + ""
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

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv1.Columns(colExpiry)) OrElse (e.Column Is gv1.Columns(colManufactureDate)) Then
                    gv1.Columns(colExpiry).FormatString = "{0:d}"
                ElseIf e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colMRP) Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colOrderNo).Value) > 0 Then
                        gv1.CurrentRow.Cells(colICode).ReadOnly = True
                        gv1.CurrentRow.Cells(colMRP).ReadOnly = True

                        'gv1.CurrentRow.Cells(colOrgSOQty).ReadOnly = True

                    Else
                        gv1.CurrentRow.Cells(colICode).ReadOnly = False
                        gv1.CurrentRow.Cells(colMRP).ReadOnly = False

                        'gv1.CurrentRow.Cells(colOrgSOQty).ReadOnly = False

                    End If

                ElseIf e.Column Is gv1.Columns(colQty) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal Then
                        If clsCommon.CompairString(gv1.CurrentRow.Cells(colSchemeItem).Value, "Yes") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colQty).ReadOnly = True
                            gv1.CurrentRow.Cells(colFreeQty).ReadOnly = True
                        Else
                            gv1.CurrentRow.Cells(colQty).ReadOnly = False
                            gv1.CurrentRow.Cells(colFreeQty).ReadOnly = False
                        End If
                        gv1.CurrentRow.Cells(colQty).ReadOnly = False
                        gv1.CurrentRow.Cells(colFreeQty).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colQty).ReadOnly = True
                        gv1.CurrentRow.Cells(colFreeQty).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colUnit) Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colOrderNo).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colUnit).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colUnit).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colRate) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal Then
                        If chkRateUserCustomer.ToggleState = ToggleState.Indeterminate Then
                            gv1.CurrentRow.Cells(colRate).ReadOnly = Not chkRateDefaultSetting.Checked
                        Else
                            gv1.CurrentRow.Cells(colRate).ReadOnly = Not chkRateUserCustomer.Checked
                        End If
                        If ItemRateEditable Then
                            gv1.CurrentRow.Cells(colRate).ReadOnly = False
                        Else
                            gv1.CurrentRow.Cells(colRate).ReadOnly = True
                        End If
                    Else
                        gv1.CurrentRow.Cells(colRate).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colDisPer) Then
                    If ItemMRPEditable Then
                        gv1.CurrentRow.Cells(colMRP).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colMRP).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colAmt) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal OrElse clsCommon.myCdbl(gv1.CurrentRow.Cells(colIsMannualAmt).Value) = 1 Then
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = True
                    End If



                    ''ElseIf (e.Column Is gv1.Columns(colOrgPOQty) OrElse e.Column Is gv1.Columns(colOrgSOQty) OrElse e.Column Is gv1.Columns(colOrgGRNQty)) Then
                    ''    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal AndAlso isPO_GRN_MRN_Editable Then
                    ''        gv1.CurrentRow.Cells(colOrgPOQty).ReadOnly = False
                    ''        gv1.CurrentRow.Cells(colOrgGRNQty).ReadOnly = False
                    ''        gv1.CurrentRow.Cells(colOrgSOQty).ReadOnly = False
                    ''    Else
                    ''        gv1.CurrentRow.Cells(colQty).ReadOnly = True
                    ''    End If
                End If
                'Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                'cell.GradientStyle = GradientStyles.Solid
                'cell.BackColor = Color.FromArgb(243, 181, 51)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
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

    Private Sub txtReqNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReqNo._MYValidating
        SelectMRNItems()
    End Sub

    Sub RefreshReqNo()
        txtReqNo.Value = ""
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colOrderNo).Value)
                If clsCommon.myLen(strReqNo) > 0 Then
                    txtReqNo.Value = clsCommon.myCstr(strReqNo)
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Public Sub funrejprint()
        Try
            Dim qry As String = "SELECT     TSPL_SD_SHIPMENT_HEAD.Document_Code, TSPL_SD_SHIPMENT_HEAD.Document_Date, TSPL_SD_SHIPMENT_HEAD.Customer_Name, TSPL_SD_SHIPMENT_HEAD.Ship_To_Location, TSPL_SD_SHIPMENT_HEAD.Bill_To_Location, TSPL_SD_SHIPMENT_HEAD.RMDA_No, TSPL_SD_SHIPMENT_HEAD.RMDA_Date,TSPL_SD_SHIPMENT_HEAD.Remarks,TSPL_SD_SHIPMENT_HEAD.Description, TSPL_SRN_DETAIL.Item_Code,   TSPL_SRN_DETAIL.Item_Desc, TSPL_SRN_DETAIL.Rejected_Qty, TSPL_SRN_DETAIL.Item_Cost,TSPL_SRN_DETAIL.Unit_code,TSPL_SRN_DETAIL.Rejected_Qty*TSPL_SRN_DETAIL.Item_Cost as Amount,TSPL_COMPANY_MASTER.comp_name,TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2  FROM         TSPL_SD_SHIPMENT_HEAD INNER JOIN    TSPL_SRN_DETAIL ON TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SRN_DETAIL.Document_Code LEFT OUTER JOIN     TSPL_COMPANY_MASTER ON TSPL_SD_SHIPMENT_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  where TSPL_SD_SHIPMENT_HEAD.Document_Code='" + txtDocNo.Value + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptMRDA", "MRDA Report")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

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

    Public Function GetTaxGrp(ByVal strItmType As String) As String
        Dim qry As String = "select Tax_Group  from TSPL_VENDOR_MASTER where Customer_Code ='" + strItmType + "'"
        strItmType = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return strItmType
    End Function
    Private Function GetConvRate(ByVal IntRowNo As Integer) As Double
        Dim strItem As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
        Dim strOrgUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colOrgUnit).Value)
        Dim strCurrentUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value)
        Dim dblCurrentConvF As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colConvF).Value)
        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblConvRate As Double = 0
        If clsCommon.myLen(strOrgUnit) > 0 Then
            Dim dblOrgConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strOrgUnit & "'"))
            Dim dblStockingUnitConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and Stocking_Unit='Y' "))
            dblConvRate = (dblRate / dblOrgConvF) * dblStockingUnitConvF * dblCurrentConvF
        End If
        Return dblConvRate
    End Function
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer, ByVal CheckConFactor As Boolean)
        Try
            Dim arrTaxableAuth As New List(Of String)

            Dim dblFAmt As Double = 0
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
            Dim strUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value)
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMRP).Value)
            Dim dblBasicRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
            Dim dblConvF As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colConvF).Value)
            Dim dblItemWeight As Double = 0
            If clsCommon.myLen(strICode) > 0 Then
                dblItemWeight = clsItemMaster.getTotalItemWeight(strICode, strUnit, dblQty, Nothing)
            End If

            Dim dblheadDiscamt As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colHeadDiscamt).Value)
            Dim dblOrgBasicRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOrgCost).Value)

            'Dim dblConvBasicRate As Double = 0
            'If dblConvF = 1 And dblOrgBasicRate Then
            '    dblConvBasicRate = dblOrgBasicRate
            'Else
            '    dblConvBasicRate = dblOrgBasicRate * dblConvF
            'End If

            'Dim dblBasicAmt As Double = dblQty * dblConvBasicRate
            'dblBasicRate = dblConvBasicRate
            If clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColFOC).Value) = 0 Then
                dblRate = GetConvRate(IntRowNo)
            End If
            Dim strOrgUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value)
            Dim dblBasicAmt As Double = dblQty * dblRate


            Dim dblAmt As Double = (dblQty * dblRate) ''+ dblFAmt
            Dim dblMRPAmt As Double = dblQty * dblMRP
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colIsMannualAmt).Value) = 0 Then
                gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
            Else
                dblAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmt).Value)
            End If

            '''' to calculate customer disc
            Dim dt As New DataTable
            Dim dblOrderQty As Double = 0
            Dim dblCustDiscQty As Double = 0
            Dim dblCustDiscAmt As Double = 0
            Dim dblCustDiscPercentage As Double = 0
            Dim dblApplyCustDisc As Double = 0
            Dim dblTotCustDisc As Double = 0

            If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColFOC).Value) = 0 Then
                Dim obj_Cash As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetPriceSchemeData(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value), clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value), txtVendorNo.Value)
                If obj_Cash IsNot Nothing Then
                    gv1.Rows(IntRowNo).Cells(colCash_Amt).Value = obj_Cash.Cash_Amt
                    gv1.Rows(IntRowNo).Cells(colCash_Pers).Value = obj_Cash.Cash_Pers
                    gv1.Rows(IntRowNo).Cells(colCashSchemeCode).Value = obj_Cash.Schm_Code
                    If clsCommon.myCdbl(obj_Cash.Cash_Pers) > 0 Then
                        gv1.Rows(IntRowNo).Cells(colCashSchemeType).Value = "P"
                        gv1.Rows(IntRowNo).Cells(colCash_Amt).Value = System.Math.Round((clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colCash_Pers).Value)) / 100, 2)
                    ElseIf clsCommon.myCdbl(obj_Cash.Cash_Amt) > 0 Then
                        gv1.Rows(IntRowNo).Cells(colCashSchemeType).Value = "A"
                    End If
                    'gv1.Rows(Index).Cells(colSchemeItem).Value = "Yes"
                Else
                    gv1.Rows(IntRowNo).Cells(colCash_Amt).Value = Nothing
                    gv1.Rows(IntRowNo).Cells(colCash_Pers).Value = Nothing
                    gv1.Rows(IntRowNo).Cells(colCashSchemeCode).Value = Nothing
                    gv1.Rows(IntRowNo).Cells(colCashSchemeType).Value = Nothing
                    'gv1.Rows(IntRowNo).Cells(colSchemeItem).Value = "No"
                End If
                If clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colCash_Amt).Value) > dblAmt Then
                    gv1.Rows(IntRowNo).Cells(colCash_Amt).Value = 0
                    gv1.Rows(IntRowNo).Cells(colCash_Pers).Value = 0
                    gv1.Rows(IntRowNo).Cells(colCashSchemeCode).Value = Nothing
                    gv1.Rows(IntRowNo).Cells(colCashSchemeType).Value = Nothing
                End If
                '    StrSql = "select top 1 Item_Qty,Amount,percentage,TSPL_SCHEME_MASTER_NEW.scheme_code from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_BENEFICIARY on  " & _
                '"TSPL_SCHEME_MASTER_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where Scheme_Type='Cash' and Cust_Code='" & txtVendorNo.Value & "' and " & _
                '"Start_Date < = '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' and Item_Code='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) & "' and " & _
                '"Unit_Code='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value) & "' and " & _
                '"MRP='" & clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMRP).Value) & "' " & _
                '"and Basic_Price='" & clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value) & "'  order by TSPL_SCHEME_MASTER_NEW.scheme_code desc "
                '    dt = clsDBFuncationality.GetDataTable(StrSql)
                '    If dt IsNot Nothing And dt.Rows.Count > 0 Then

                '        dblOrderQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
                '        dblCustDiscQty = clsCommon.myCdbl(dt.Rows(0)("Item_Qty"))
                '        dblCustDiscAmt = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                '        dblCustDiscPercentage = clsCommon.myCdbl(dt.Rows(0)("percentage"))
                '        If dblCustDiscPercentage = 0 Then
                '            gv1.Rows(IntRowNo).Cells(ColCustDiscountQty).Value = dblCustDiscQty
                '            gv1.Rows(IntRowNo).Cells(colcustDiscount).Value = dblCustDiscAmt
                '            dblApplyCustDisc = Math.Truncate(dblOrderQty / dblCustDiscQty)
                '            dblTotCustDisc = dblApplyCustDisc * dblCustDiscAmt
                '        Else
                '            gv1.Rows(IntRowNo).Cells(ColCustDiscountQty).Value = dblCustDiscQty
                '            gv1.Rows(IntRowNo).Cells(colCustDiscPercentage).Value = dblCustDiscPercentage
                '            dblApplyCustDisc = (dblAmt * dblCustDiscPercentage) / 100
                '            dblTotCustDisc = dblApplyCustDisc
                '        End If
                '        If dblTotCustDisc > 0 Then
                '            gv1.Rows(IntRowNo).Cells(colCashDiscSchemeCode).Value = clsCommon.myCstr(dt.Rows(0)("scheme_code"))
                '        End If
                '        gv1.Rows(IntRowNo).Cells(colTotalCustDiscount).Value = dblTotCustDisc
                '    End If

            End If
            '''' end 
            Dim dblCashAmt As Double = gv1.Rows(IntRowNo).Cells(colCash_Amt).Value
            Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPer).Value)
            Dim dblDisAmt As Double = (dblAmt * dblDisPer) / 100
            Dim dblHeadDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colHeaDDisPer).Value)
            Dim dblHeadPerDisAmt As Double = (dblAmt * dblHeadDisPer) / 100
            Dim dblTotDiscAmt As Double = 0
            Dim dblAmtAfterDis As Double = 0
            If clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColFOC).Value) = 0 Then
                dblTotDiscAmt = dblheadDiscamt + dblHeadPerDisAmt + dblDisAmt + dblCashAmt
                dblAmtAfterDis = dblAmt - dblDisAmt - dblTotCustDisc - dblheadDiscamt - dblHeadPerDisAmt - dblCashAmt
            Else
                dblTotDiscAmt = dblheadDiscamt + dblHeadPerDisAmt + dblDisAmt + dblAmt
                dblAmtAfterDis = dblAmt
            End If




            Dim dblAbatementRate As Double = gv1.CurrentRow.Cells(colAbatementPer).Value
            Dim dblAbatementAmt As Double = ((dblMRP * dblAbatementRate) / 100) * dblQty

            If clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColFOC).Value) = 0 Then
                For ii As Integer = 1 To 10
                    Dim Strii As String = clsCommon.myCstr(ii)
                    If rbtnTaxCalAutomatic.IsChecked Then
                        Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                        If clsCommon.myLen(strTaxCode) > 0 Then
                            Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                            Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                            Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                            Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                            Dim IsExcisable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value)
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


                                If strExcise = True AndAlso intMRPwithabatement = 1 AndAlso IsExcisable = True Then
                                    dblBaseAmt = (dblAbatementAmt + dblOtherTaxAmt)
                                Else
                                    dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                                End If
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
                Next
            End If
            If dblQty > 0 Then
                Dim dblNetPrice As Double = dblAmtAfterDis / dblQty
                gv1.Rows(IntRowNo).Cells(colActualCost).Value = dblNetPrice
            End If

            Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
            Dim dblAmtAfterTax As Double = dblAmtAfterDis + dblTotTaxAmt

            gv1.Rows(IntRowNo).Cells(colDisAmt).Value = Math.Round(dblDisAmt, 2)
            gv1.Rows(IntRowNo).Cells(colAmtAfterDis).Value = Math.Round(dblAmtAfterDis, 2)
            gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
            gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)
            gv1.Rows(IntRowNo).Cells(colAbatementAmount).Value = Math.Round(dblAbatementAmt, 2)
            gv1.Rows(IntRowNo).Cells(colTotalMRP).Value = Math.Round(dblMRPAmt, 2)
            gv1.Rows(IntRowNo).Cells(colTotalBasicAmount).Value = Math.Round(dblBasicAmt, 2)
            gv1.Rows(IntRowNo).Cells(colTotalBasicAmount).Value = 0
            gv1.Rows(IntRowNo).Cells(colTotItemWt).Value = Math.Round(dblItemWeight, 2)
            gv1.Rows(IntRowNo).Cells(colTotalCustDiscount).Value = Math.Round(dblTotCustDisc, 2)
            gv1.Rows(IntRowNo).Cells(colRate).Value = dblRate
            gv1.Rows(IntRowNo).Cells(colHeadDisPerAmt).Value = Math.Round(dblHeadPerDisAmt, 2)
            gv1.Rows(IntRowNo).Cells(colTotalDiscountAmount).Value = Math.Round(dblTotDiscAmt, 2)
            gv1.Rows(IntRowNo).Cells(colOrgUnit).Value = strOrgUnit

            If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColFOC).Value) = 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colSampling).Value), "No") = CompairStringResult.Equal Then

                '' Anubhooti 11-Sep-2014 BM00000003847
                Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) & "'"))
                If ItemCrateType = 1 Then
                    Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value) & "'"))
                    If IsStockingUnit = "Y" Then
                        Dim ConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) & "' and tspl_unit_master.Crate_Type ='Y' "))
                        If ConvFactor > 0 Then
                            Dim DispatchQty As Double = gv1.Rows(IntRowNo).Cells(colQty).Value
                            If DispatchQty >= ConvFactor Then
                                gv1.Rows(IntRowNo).Cells(colCrate).Value = Math.Floor(DispatchQty / ConvFactor)
                            Else
                                gv1.Rows(IntRowNo).Cells(colCrate).Value = Math.Floor(DispatchQty / ConvFactor)
                            End If
                        Else
                            If CheckConFactor = True Then
                                ConvFactMsg = True
                                clsCommon.MyMessageBoxShow("Please fill conversion factor for this unit at line no." & IntRowNo + 1 & "")
                            Else
                                ConvFactMsg = False
                            End If


                        End If
                    Else
                        Dim CrateUOMCase As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(tspl_unit_master.Crate_Type) As Row  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) & "' and tspl_unit_master.Crate_Type ='Y' "))
                        If CrateUOMCase > 0 Then
                            gv1.Rows(IntRowNo).Cells(colCrate).Value = Math.Floor(gv1.Rows(IntRowNo).Cells(colQty).Value)
                        End If
                    End If
                End If
            End If
            Dim TotalCrate As Integer = 0
            For i As Integer = 0 To gv1.Rows.Count - 1
                TotalCrate = TotalCrate + gv1.Rows(i).Cells(colCrate).Value
            Next
            If clsCommon.myCdbl(TotalCrate) > 0 Then
                txtCrateQty.Value = TotalCrate
            Else
                txtCrateQty.Value = 0
            End If

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
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
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
                        Dim obj As clsAdditionalCharge = clsAdditionalCharge.getFinder(clsCommon.myCstr(gvAC.CurrentRow.Cells(colACCode).Value), False)
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

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub rbtnTaxCalAutomatic_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnTaxCalAutomatic.ToggleStateChanged, rbtnTaxCalManual.ToggleStateChanged
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

    Private Sub gv2_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedTaxOpen Then
                    isCellValueChangedTaxOpen = True
                    If (e.Column Is (gv2.Columns(colTTaxAmt)) AndAlso rbtnTaxCalManual.IsChecked) Then
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRow(ii, False)
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

    Private Sub gv1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F7 Then
            If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                gv1.CurrentRow.Cells(colIsMannualAmt).Value = IIf(clsCommon.myCdbl(gv1.CurrentRow.Cells(colIsMannualAmt).Value) = 1, 0, 1)
            End If

            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colIsMannualAmt).Value) = 0 Then
                UpdateCurrentRow(gv1.CurrentRow.Index, False)
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        ElseIf e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentColumn = gv1.Columns(colIName)
                OpenICodeList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colUnit) Then
                gv1.CurrentColumn = gv1.Columns(colOrgUnit)
                OpenUOMList(True)
                gv1.CurrentColumn = gv1.Columns(colUnit)
            End If
        End If
    End Sub

    Private Sub gv1_RowFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gv1.RowFormatting
        Try
            If clsCommon.CompairString(clsCommon.myCstr(e.RowElement.RowInfo.Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(e.RowElement.RowInfo.Cells(colIsMannualAmt).Value) > 0 Then
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
            If Not clsCommon.myLen(txtReqNo.Value) > 0 Then
                Dim intCurrRow As Integer = gv1.CurrentRow.Index
                gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                If intCurrRow = gv1.Rows.Count - 1 Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                End If
            End If
        End If
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


        Return dt
    End Function
    Sub LoadInvoiceType()
        ddlInvoiceType.DataSource = GetInvoiceType()
        ddlInvoiceType.ValueMember = "Code"
        ddlInvoiceType.DisplayMember = "Name"
    End Sub
    Private Sub chkCreateAutoInvoice_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCreateAutoInvoice.ToggleStateChanged
        'chkCreateAutoReceipt.Visible = chkCreateAutoInvoice.Checked
        If AllowChangeInvoiceType Then
            'lblInvoiceType.Visible = True
            'ddlInvoiceType.Visible = True
        Else
            lblInvoiceType.Visible = False
            ddlInvoiceType.Visible = False

        End If
        If chkCreateAutoInvoice.Checked Then
            btnPrintInvoice.Visible = True
            lblInvoiceNo.Visible = True
            txtInvoiceNo.Visible = True
            '-----------------richa 27/06/2014 Ticket No .BM00000002982------------
            'ddlInvoiceType.Visible = True
            'lblInvoiceType.Visible = True
            '---------------------------------------------------------------------
        Else
            btnPrintInvoice.Visible = True
            lblInvoiceNo.Visible = False
            txtInvoiceNo.Visible = False
            '-----------------richa 27/06/2014 Ticket No .BM00000002982------------
            ddlInvoiceType.Visible = False
            lblInvoiceType.Visible = False
            '---------------------------------------------------------------------
        End If
    End Sub

    Private Sub txtSalesman__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSalesman._MYValidating
        Dim qry As String = "select EMP_CODE as Code,Emp_Name as Name from TSPL_EMPLOYEE_MASTER"
        Dim whrcls As String = "Emp_type='Salesman'"
        txtSalesman.Value = clsCommon.ShowSelectForm("SNOSaleman", qry, "Code", whrcls, txtSalesman.Value, "Code", isButtonClicked)
        lblSalesman.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name as Name from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" + txtSalesman.Value + "' and Emp_type='Salesman'"))
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
        UcItemBalance1.LocationCode = txtBillToLocation.Value
        UcItemBalance1.LocationName = lblBillToLocation.Text
        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtDocNo.Value
        UcItemBalance1.TransDate = txtDate.Value
        UcItemBalance1.ShowSOQty = True
        UcItemBalance1.RefreshData()
    End Sub

    Private Sub gv1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv1.Click
        If gv1.CurrentRow IsNot Nothing Then
            setBalance()
        End If
    End Sub

    Private Sub gv1_CurrentRowChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 Then
            setBalance()
        End If
    End Sub

    Private Sub RadGroupBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox2.Click

    End Sub


    Public Shared Function GetCost(ByVal CostMethod As EnumCostingMethod, ByVal strICode As String, ByVal strLocation As String, ByVal dblqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction) As Double
        Dim dblRetCost As Double = 0
        If Not CostMethod = EnumCostingMethod.NA AndAlso dblqty > 0 Then
            Dim strSymbolCost As String = " >= "
            If CostMethod = EnumCostingMethod.LIFO Then
                strSymbolCost = " <= "
            End If

            Dim strDateColumn As String = " Punching_Date "
            Dim strDateForCheck As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt")
            If isApplyCostOnPostDate Then
                strDateColumn = " Posting_Date "
                strDateForCheck = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtPostingDate), "dd/MMM/yyyy hh:mm tt")
            End If

            Dim qry As String
            If CostMethod = EnumCostingMethod.Averege Then
                qry = "select case when Qty=0 then 0 else abs(Amt/Qty)*" + clsCommon.myCstr(dblqty) + "  end as AvgCost from( select  sum(Amt * RI) as Amt,sum(Qty * RI) as Qty from(" + Environment.NewLine
                qry += " select Stock_Qty as Qty,( Avg_Cost) as Amt,case when InOut='O' then -1 else 1 end as RI  from TSPL_INVENTORY_MOVEMENT where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and  " + strDateColumn + " <= '" + strDateForCheck + "' " + Environment.NewLine
                qry += " )xxx )xxxx" + Environment.NewLine
                dblRetCost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            Else
                qry = ";WITH cteStockSum AS ( " + Environment.NewLine
                qry += " SELECT   Item_Code ,SUM(Stock_Qty * CASE WHEN  InOut = 'O' THEN -1 ELSE 1 END) AS TotalStock FROM  TSPL_INVENTORY_MOVEMENT where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and " + strDateColumn + " <= '" + strDateForCheck + "'  GROUP BY Item_Code)," + Environment.NewLine

                qry += " cteReverseInSum AS (" + Environment.NewLine
                qry += " SELECT  s.Item_Code ,s." + strDateColumn + " as TranDate ,(SELECT SUM(i.Stock_Qty) FROM TSPL_INVENTORY_MOVEMENT AS i  WHERE i.Item_Code = s.Item_Code AND i.InOut IN ( 'I' ) and i." + strDateColumn + " <= '" + strDateForCheck + "' AND i." + strDateColumn + " " + strSymbolCost + " s." + strDateColumn + " --for FIFO  >= " + Environment.NewLine
                qry += " ) AS RollingStock ,s.Stock_Qty AS ThisStock FROM TSPL_INVENTORY_MOVEMENT AS s WHERE  s.Item_Code='" + strICode + "' and s.Location_Code='" + strLocation + "' and s." + strDateColumn + " <= '" + strDateForCheck + "'  and s.InOut IN ( 'I' ))," + Environment.NewLine

                qry += " cteWithLastTranDate  AS ( " + Environment.NewLine
                qry += " SELECT   w.Item_Code ,w.TotalStock ,LastPartialStock. TranDate ,LastPartialStock.StockToUse ,LastPartialStock.RunningTotal ,w.TotalStock -LastPartialStock.RunningTotal+ LastPartialStock.StockToUse AS UseThisStock FROM cteStockSum AS w" + Environment.NewLine
                qry += " CROSS APPLY ( SELECT TOP ( 1 )z. TranDate ,z.ThisStock AS StockToUse ,z.RollingStock AS RunningTotal FROM  cteReverseInSum AS z WHERE z.Item_Code = w.Item_Code AND z.RollingStock >= w.TotalStock ORDER BY  z.TranDate " + IIf(CostMethod = EnumCostingMethod.FIFO, "DESC", "") + " --for FIFO DESC" + Environment.NewLine
                qry += " ) AS LastPartialStock" + Environment.NewLine
                qry += " )" + Environment.NewLine

                qry += " select *  from (" + Environment.NewLine
                qry += " SELECT  y.Item_Code ,y.TotalStock AS CurrentItems ,e.Basic_Cost,e." + strDateColumn + " as TranDate,(CASE WHEN e." + strDateColumn + " = y.TranDate THEN y.UseThisStock" + Environment.NewLine
                qry += " ELSE e.Stock_Qty END * Price.Basic_Cost) AS CurrentValue,(CASE WHEN e. " + strDateColumn + "  = y.TranDate THEN y.UseThisStock  ELSE e.Stock_Qty END  ) as BalanceQty FROM cteWithLastTranDate AS y INNER JOIN TSPL_INVENTORY_MOVEMENT AS e ON e.Item_Code = y.Item_Code and e." + strDateColumn + " <= '" + strDateForCheck + "' AND e." + strDateColumn + " " + strSymbolCost + " y.TranDate -- for Fifo >=" + Environment.NewLine
                qry += " AND e.InOut IN ('I')" + Environment.NewLine
                qry += " CROSS APPLY ( SELECT TOP ( 1 ) case when Stock_Qty =0 then 0 else  (p.Basic_Cost*p.Qty)/p.Stock_Qty end as Basic_Cost FROM TSPL_INVENTORY_MOVEMENT AS p  WHERE p.Item_Code = e.Item_Code " + Environment.NewLine
                qry += " AND p." + strDateColumn + " <= e." + strDateColumn + "  " + Environment.NewLine
                qry += " AND p.InOut = 'I'  ORDER BY p." + strDateColumn + " DESC ) AS Price" + Environment.NewLine
                qry += ")xxx   " + IIf(CostMethod = EnumCostingMethod.FIFO, " order by TranDate ", IIf(CostMethod = EnumCostingMethod.LIFO, "order by TranDate DESC", "")) + " --For Fifo not Desc order" + Environment.NewLine

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim dblbalanceQty As Double = dblqty
                    For Each dr As DataRow In dt.Rows
                        Dim dblCurrQty As Double = clsCommon.myCdbl(dr("BalanceQty"))
                        If dblbalanceQty >= dblCurrQty Then
                            dblRetCost += clsCommon.myCdbl(dr("CurrentValue"))
                        Else
                            dblRetCost += (clsCommon.myCdbl(dr("CurrentValue")) * dblbalanceQty) / dblCurrQty
                        End If
                        dblbalanceQty -= dblCurrQty
                        If dblbalanceQty <= 0 Then
                            dblRetCost = dblRetCost
                            Exit For
                        End If
                    Next
                    If dblbalanceQty > 0 Then
                        Throw New Exception("Quantity Not available for " + strICode)
                    End If
                End If
            End If
        End If
        'Throw New Exception("cost " + clsCommon.myCstr(dblRetCost))
        Return dblRetCost
    End Function

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
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBarCode).Value = obj.Bar_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                If blnBackCalculation = True Then
                    OpenICodeList(False)
                Else
                    OpenICodeListCurrentCalaculation(False)
                End If
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = 1
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Selling_Price
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.Item_MRP
                CurrentRow = gv1.Rows.Count - 1
                For ii As Integer = 1 To gv1.Rows.Count
                    gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
                Next
                gv1.Rows.AddNew()

            End If

            UpdateCurrentRow(CurrentRow, False)
            UpdateAllTotals()
            txtBarCode.Text = ""
            txtBarCode.Focus()
        End If
    End Sub
    Private Sub fndRouteNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRouteNo._MYValidating

        Dim qry As String = "Select Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
        txtRouteNo.Value = clsCommon.ShowSelectForm("ShipRouteFinder", qry, "Code", "", txtRouteNo.Value, "", isButtonClicked)
        If clsCommon.myLen(txtRouteNo.Value) > 0 Then
            qry = "SELECT Route_Desc,vehicle_code,Number,Transporter_Name,Capacity  FROM TSPL_ROUTE_MASTER left outer join " & _
            "TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join " & _
            "TSPL_TRANSPORT_MASTER on TSPL_VEHICLE_MASTER.Transport_Id=TSPL_TRANSPORT_MASTER.Transport_Id where  Route_No  ='" + clsCommon.myCstr(txtRouteNo.Value) + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblRouteDesc.Text = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
                txtLorryNo.Text = clsCommon.myCstr(dt.Rows(0)("vehicle_code"))
                txtVehicleCode.Value = clsCommon.myCstr(dt.Rows(0)("vehicle_code"))
                lblVehicleNo.Text = clsCommon.myCstr(dt.Rows(0)("Number"))
                txtVehicleCapacity.Text = clsCommon.myCstr(dt.Rows(0)("Capacity"))
                txtTransporterName.Text = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            End If
        Else
            lblRouteNo.Text = ""
        End If
    End Sub
    Private Sub fndRouteNo_TextChanged()
        Dim sql As String = "Select Route_Desc,Employee_Code from TSPL_ROUTE_MASTER where Route_No='" + txtRouteNo.Value + "'"
        Dim dr1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then

            lblRouteDesc.Text = dr1.Rows(0)(0).ToString()
            txtSalesman.Value = dr1.Rows(0)(1).ToString()

        Else
            lblRouteDesc.Text = String.Empty
        End If
    End Sub
    Private Sub chkDiscountOnAmt_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDiscountOnAmt.ToggleStateChanged, chkDiscountOnRate.ToggleStateChanged
        If chkDiscountOnAmt.IsChecked Then
            txtDiscAmt.Enabled = True
            txtDiscPer.Enabled = False
            txtDiscPer.Text = 0
            For Each gro As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(gro.Cells(colICode).Value) > 0 And clsCommon.myCdbl(gro.Cells(ColFOC).Value) = 0 Then
                    gro.Cells(colHeaDDisPer).Value = 0
                Else

                End If
            Next
            lblInvoiceDiscAmt.Text = 0
        Else
            txtDiscAmt.Enabled = False
            txtDiscPer.Enabled = True
            txtDiscAmt.Text = 0
            For Each gro As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(gro.Cells(colICode).Value) > 0 And clsCommon.myCdbl(gro.Cells(ColFOC).Value) = 0 Then
                    gro.Cells(colHeadDiscamt).Value = 0
                Else

                End If
            Next
            lblInvoiceDiscAmt.Text = 0

        End If
    End Sub
    Private Sub txtDiscAmt_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscAmt.Leave
        CalculateDiscountAmount()
    End Sub
    Private Sub txtDiscAmt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscAmt.Click

    End Sub
    Private Sub txtDiscPer_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscPer.Leave
        CalculateDiscountAmount()
    End Sub
    Private Sub txtDiscPer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscPer.Click

    End Sub
    'Private Sub CalculateDiscountAmount()
    '    If clsCommon.myCdbl(txtDiscAmt.Text) > clsCommon.myCdbl(lblAmtWithDiscount.Text) Then
    '        isCellValueChangedOpen = False
    '        Throw New Exception("Discount amount cannot be greater than Doc amount")

    '    End If
    '    Dim discountrate As Decimal = Decimal.Parse(txtDiscPer.Text)
    '    'If chkDiscountOnAmt.IsChecked AndAlso clsCommon.myCdbl(lblAmtWithDiscount.Text) > 0 Then
    '    '    discountrate = clsCommon.myCstr(Math.Round(((txtDiscAmt.Value * 100) / clsCommon.myCdbl(lblAmtWithDiscount.Text)), 5, MidpointRounding.ToEven))
    '    '    txtDiscPer.Value = discountrate
    '    'End If
    '    Dim dblDiscountAmtPerUnit As Decimal = 0
    '    Dim dblDiscountAmt As Decimal = 0
    '    Dim dblCustDiscountNoTax As Double = 0
    '    If String.IsNullOrEmpty(lblAmtWithDiscount.Text) Then
    '        lblAmtWithDiscount.Text = 0
    '    End If

    '    If chkDiscountOnAmt.IsChecked Then
    '        For Each gro As GridViewRowInfo In gv1.Rows
    '            gv1.CurrentRow = gro.Cells(colHeadDiscamt).RowInfo
    '            If clsCommon.myLen(gro.Cells(colICode).Value) > 0 And clsCommon.myCdbl(gro.Cells(ColFOC).Value) = 0 Then

    '                dblDiscountAmt = Math.Round((clsCommon.myCdbl(gro.Cells(colAmt).Value) * txtDiscAmt.Value) / clsCommon.myCdbl(lblAmtWithDiscount.Text), 2)
    '                gro.Cells(colHeadDiscamt).Value = Math.Round((dblDiscountAmt), 2)
    '            Else
    '                gro.Cells(colHeadDiscamt).Value = 0

    '            End If

    '        Next
    '    Else
    '        For Each gro As GridViewRowInfo In gv1.Rows
    '            gv1.CurrentRow = gro.Cells(colHeadDiscamt).RowInfo
    '            If clsCommon.myLen(gro.Cells(colICode).Value) > 0 And clsCommon.myCdbl(gro.Cells(ColFOC).Value) = 0 Then
    '                gro.Cells(colDisPer).Value = Math.Round((discountrate), 2)
    '            Else

    '            End If
    '        Next
    '    End If

    'End Sub

    Private Sub CalculateDiscountAmount()
        If clsCommon.myCdbl(txtDiscAmt.Text) > clsCommon.myCdbl(lblAmtWithDiscount.Text) Then
            isCellValueChangedOpen = False
            Throw New Exception("Discount amount cannot be greater than Doc amount")

        End If
        Dim discountrate As Decimal
        'Dim discountrate As Decimal = Decimal.Parse(txtDiscPer.Text)
        'If chkDiscountOnAmt.IsChecked AndAlso clsCommon.myCdbl(lblAmtWithDiscount.Text) > 0 Then
        '    discountrate = clsCommon.myCstr(Math.Round(((txtDiscAmt.Value * 100) / clsCommon.myCdbl(lblAmtWithDiscount.Text)), 5, MidpointRounding.ToEven))
        '    txtDiscPer.Value = discountrate
        'End If
        If clsCommon.myCdbl(txtDiscPer.Text) > 0 Then
            discountrate = Decimal.Parse(txtDiscPer.Text)
            txtDiscAmt.Text = 0

        ElseIf clsCommon.myCdbl(txtDiscAmt.Text) > 0 Then
            txtDiscPer.Text = 0
        End If
        Dim dblDiscountAmt As Decimal = 0
        Dim dblCustDiscountNoTax As Double = 0
        If String.IsNullOrEmpty(lblAmtWithDiscount.Text) Then
            lblAmtWithDiscount.Text = 0
        End If

        If chkDiscountOnAmt.IsChecked Then
            For Each gro As GridViewRowInfo In gv1.Rows
                gv1.CurrentRow = gro.Cells(colHeadDiscamt).RowInfo
                If clsCommon.myLen(gro.Cells(colICode).Value) > 0 And clsCommon.myCdbl(gro.Cells(ColFOC).Value) = 0 Then

                    dblDiscountAmt = Math.Round((clsCommon.myCdbl(gro.Cells(colAmt).Value) * txtDiscAmt.Value) / clsCommon.myCdbl(lblAmtWithDiscount.Text), 2)
                    gro.Cells(colHeadDiscamt).Value = Math.Round((dblDiscountAmt), 2)
                Else
                    gro.Cells(colHeadDiscamt).Value = 0

                End If

            Next
        Else
            For Each gro As GridViewRowInfo In gv1.Rows
                gv1.CurrentRow = gro.Cells(colHeadDiscamt).RowInfo
                If clsCommon.myLen(gro.Cells(colICode).Value) > 0 And clsCommon.myCdbl(gro.Cells(ColFOC).Value) = 0 Then
                    gro.Cells(colHeaDDisPer).Value = Math.Round((discountrate), 2)
                Else

                End If
            Next
        End If

    End Sub

    Private Sub lblVendorName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblVendorName.Click

    End Sub

    Private Sub RadLabel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadLabel2.Click

    End Sub

    Private Sub txtVendorNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVendorNo.Load

    End Sub

    Private Sub txtFromLoc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtFromLoc._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtFromLoc.Value = clsCommon.ShowSelectForm("Transferfndr", qry, "Code", WhrCls, txtFromLoc.Value, "Code", isButtonClicked)
    End Sub

    Private Sub RadPageViewPage1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles RadPageViewPage1.Paint

    End Sub


    Private Sub txtVehicleCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtVehicleCode._MYValidating
        Try
            Dim qry As String = "Select distinct  vehicle_id ,Description from TSPL_VEHICLE_MASTER"
            txtVehicleCode.Value = clsCommon.ShowSelectForm("Vehicle No", qry, "vehicle_id", "", txtVehicleCode.Value, "vehicle_id", isButtonClicked)
            lblVehicleNo.Text = connectSql.RunScalar("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtVehicleCode.Value) + "'")
            txtVehicleCapacity.Text = clsDBFuncationality.getSingleValue("Select Capacity  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtVehicleCode.Value) + "'")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function funvalidatevehicle() As Boolean
        Dim count As Decimal = 0
        Dim segno As String = String.Empty
        Dim strvehiclenum As String = lblVehicleNo.Text
        Dim sql As String = "select segment_code from TSPL_GL_SEGMENT_CODE where segment_code  = '" + Convert.ToString(txtVehicleCode.Value) + "' "
        If Not String.IsNullOrEmpty(connectSql.RunScalar(sql)) Then
            sql = "Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + txtVehicleCode.Value + "'"
            lblVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
            Return True
        Else
            Dim strmessage As String = "This vehicle code doesn't exist" + Environment.NewLine
            strmessage += "Do you want to continue "



            If common.clsCommon.MyMessageBoxShow(strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

                If clsCommon.myLen(lblVehicleNo.Text) <= 0 Then
                    lblVehicleNo.Focus()
                    Throw New Exception("Please Enter Vehicle No")
                End If


                txtVehicleCode.Value = clsCommon.incval(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Max(Segment_code) from TSPL_GL_SEGMENT_CODE where Seg_No = 2 ")))
                Dim strSegmentName = clsDBFuncationality.getSingleValue("select Seg_Name from TSPL_GL_SEGMENT where Seg_No=2")
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    connectSql.RunSpTransaction(trans, "sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", "2"), New SqlParameter("@segmentname", strSegmentName), New SqlParameter("@segmentcode", txtVehicleCode.Value), New SqlParameter("@desc", strvehiclenum), New SqlParameter("@acccode", "NULL"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
                    connectSql.RunSpTransaction(trans, "SP_TSPL_VEHICLE_MASTER_INSERT", New SqlParameter("@Vehicle_Id", txtVehicleCode.Value), New SqlParameter("@Model", ""), New SqlParameter("@Number", strvehiclenum), New SqlParameter("@Description", strvehiclenum), New SqlParameter("@Type", "H"), New SqlParameter("@Start_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@End_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Vehicle_Reg_No", ""), New SqlParameter("@Vehicle_Chesis_No", ""), New SqlParameter("@Capacity", "0"), New SqlParameter("@Insurance", ""), New SqlParameter("@Pollution_Check", ""), New SqlParameter("@Fitness", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Trans_Type", ""), New SqlParameter("@Road_Tax", ""), New SqlParameter("@Transport_Id", ""), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modified_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modified_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))

                    trans.Commit()
                Catch ex As Exception
                    txtVehicleCode.Value = ""
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try

                'lblVehicleNo.Text = txtVehicleCode.Text + "-Hired"
                txtVehicleCode.Text = txtVehicleCode.Value
                Return True
            Else
                txtVehicleCode.Value = String.Empty
                txtVehicleCode.Text = txtVehicleCode.Value
                Return False
            End If
        End If
    End Function


    Private Sub btnPrintInvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintInvoice.Click

        If clsCommon.myLen(txtInvoiceNo.Text) <= 0 Then
            myMessages.blankValue(Me, "Invoice not found to Print", Me.Text)
        Else
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "WHOLLY") = CompairStringResult.Equal Then
                Dim Qry As String = " select TSPL_SD_SHIPMENT_Head.Sale_Invoice_No,Case When ISNULL(TSPL_SD_SHIPMENT_Head.ManualVehicle,'')<>'' Then TSPL_SD_SHIPMENT_Head.ManualVehicle WHEN ISNULL(TSPL_SD_SHIPMENT_Head.AlternateVehicle,'')<>'' Then TSPL_VEHICLE_MASTER.Number Else TSPL_SD_SHIPMENT_Head.vehicleNo End as vehicleNo ,Convert(varchar,TSPL_SD_SHIPMENT_Head.Sale_Invoice_Date,103) as Sale_Invoice_Date, TSPL_SD_SHIPMENT_HEAD.RoundOffAmount, TSPL_LOCATION_MASTER.Add1  as Loc_ADd1,TSPL_LOCATION_MASTER.Add2  as LOC_ADD2,TSPL_LOCATION_MASTER.Add3 as LOC_ADD3, case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as LOCPhone,TSPL_LOCATION_MASTER.TIN_No as Loc_TIN_NO,  TSPL_SD_SHIPMENT_HEAD.Document_Code ,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date , TSPL_SD_SHIPMENT_HEAD.Lorry_No ,TSPL_SD_SHIPMENT_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc as Particulars, TSPL_SD_SHIPMENT_DETAIL.Crate as QtyCrates ,TSPL_SD_SHIPMENT_DETAIL.Unit_code ,convert(Decimal(18,2), TSPL_SD_SHIPMENT_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as QtyPCS, isnull( SUB_QTY,0) as free_qty, TSPL_SD_SHIPMENT_DETAIL.Scheme_Item as FreeSchemeInLitres ,convert(DECIMAL(18,2),TSPL_SD_SHIPMENT_DETAIL.Amount/ (TSPL_SD_SHIPMENT_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) as RatePerPcs,TSPL_SD_SHIPMENT_DETAIL.Amount  as valueInRs,coalesce(TSPL_SD_SHIPMENT_DETAIL.Cash_Scheme_Amount,0) as Cash_Scheme_Amount,isnull(schemeInCrates,0)as schemeInCrates,  "
                Qry += " '' GrandTotalCrates , TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_add1 ,TSPL_COMPANY_MASTER.Add2 as  comp_add2 ,TSPL_COMPANY_MASTER.Add3 as comp_add3 ,TSPL_COMPANY_MASTER.Fax as comp_Fax ,TSPL_COMPANY_MASTER.Email as comp_Email, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone , TSPL_COMPANY_MASTER.Tin_No as comp_tinNo ,TSPL_SD_SHIPMENT_HEAD. Customer_Code  as cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as cust_add1 ,TSPL_CUSTOMER_MASTER. Add2 as cust_add2 ,TSPL_CUSTOMER_MASTER.Add3 cust_add3,case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' "
                Qry += " else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+  TSPL_CUSTOMER_MASTER.Phone2 Else'' End as CustPhone ,TSPL_CUSTOMER_MASTER.Fax as cust_fax ,TSPL_CUSTOMER_MASTER.Email as  cust_Email,TSPL_CUSTOMER_MASTER.WebSite as cust_website " & _
                    "from TSPL_SD_SHIPMENT_DETAIL   LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD .Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " & _
                    "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code and " & _
                    "TSPL_ITEM_UOM_DETAIL .UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code LEFT OUTER JOIN TSPL_ITEM_MASTER  ON " & _
                    "TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code left outer join TSPL_COMPANY_MASTER on " & _
                    "TSPL_COMPANY_MASTER.Comp_Code =TSPL_SD_SHIPMENT_HEAD.Comp_Code   left outer join TSPL_CUSTOMER_MASTER  on " & _
                    "TSPL_CUSTOMER_MASTER .Cust_Code  =TSPL_SD_SHIPMENT_HEAD .Customer_Code left outer join TSPL_LOCATION_MASTER  on " & _
                    "TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SHIPMENT_HEAD .Bill_To_Location  " & _
                    " LEFT OUTER JOIN TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" & _
                    " left join  (select DOCUMENT_CODE,Scheme_Item_Code,SUM(Qty ) AS SUB_QTY,SUM(Crate) AS schemeInCrates from TSPL_SD_SHIPMENT_DETAIL as inn where DOCUMENT_CODE in ('" + txtDocNo.Value + "') and inn.Scheme_Item='Y'   group by DOCUMENT_CODE,Scheme_Item_Code) "
                Qry += " TSPL_SD_SHIPMENT_DETAIL_Sub on TSPL_SD_SHIPMENT_DETAIL_sub.DOCUMENT_CODE=TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL_sub.Scheme_Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code where 2=2 and  TSPL_SD_SHIPMENT_HEAD.Document_Code in  ('" + txtDocNo.Value + "') "

                Qry += " and TSPL_SD_SHIPMENT_DETAIL.Scheme_Item='N'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

                If dt.Rows.Count > 0 Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoice(New)", "Fresh Invoice Statement", txtDate.Value, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                End If

            Else
                Dim objInvoice As New FrmPrintFreshInvoice
                Dim qry As String = Nothing
                If clsCommon.myLen(objInvoice.LoadPrintQuery(txtDocNo.Value)) > 0 Then
                    If CreateFreshInvoiceOnDispatchSave = 0 Then
                        qry = objInvoice.LoadPrintQuery(txtDocNo.Value)
                    Else
                        qry = objInvoice.LoadPrintQuery(txtInvoiceNo.Text)
                    End If
                End If

                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)
                If dt1.Rows.Count > 0 Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt1, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoice(New)", "Fresh Invoice Statement", txtDate.Value, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                End If
            End If
            frmCRV = Nothing


        End If
    End Sub

    Private Sub btnReverseAndUnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseAndUnpost.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsDispatchNoteFreshSale.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDrillDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrillDown.Click
        If clsCommon.myLen(txtReqNo.Value) > 0 Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSalesOrder, txtReqNo.Value)
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
        End If
    End Sub

    '-------------------------Done By Monika For Mailing System--------------------------
#Region "Mail SMS Setting"

    'Private Sub SendSMSandEmail()
    '    Try
    '        'Dim strEmail, strphone, strMes As String
    '        'Dim strCustomer, strContactperson, strVendorName As String
    '        'Dim decAmt As Decimal=0
    '        Dim strContactperson As String = String.Empty

    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmSNShipment)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do setting of email/sms", Me.Text)
    '            Return
    '        End If

    '        If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '            clsCommon.MyMessageBoxShow("First do setting of email/sms", Me.Text)
    '            Return
    '        End If

    '        Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
    '        strSubject = strSubject.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))

    '        Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
    '        strbody = strbody.Replace(clsEmailSMSConstants.CustomerNo, txtVendorNo.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.CustomerName, lblVendorName.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactperson)
    '        strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)

    '        '------------------------code for attchament-------------------------------------
    '        Dim strRptPath As String = ""
    '        If obj.atchmnt = "Y" Then
    '            attachQry = GetAttachQry(txtDocNo.Value)
    '            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachQry)
    '            If dt1.Rows.Count > 0 Then
    '                SetItemWiseTax(dt1, txtDocNo.Value)
    '                Dim frmCRV As New frmCrystalReportViewer()
    '                strRptPath = frmCRV.EmailAttachment(CrystalReportFolder.NewSalesReports, dt1, "crptShipment", "Shipment Detail")
    '                frmCRV = Nothing
    '            End If
    '        End If
    '        '---------------------------------------------------------------------------

    '        Dim qry As String = "Select * from TSPL_APPROVAL_LEVEL_SCREEN where 1=1 and Trans_Code='" + MyBase.Form_ID + "' "
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

    '        Dim lstUsers As New List(Of String)
    '        Dim lstReceiptents As New List(Of String)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows

    '                qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + dr("User_Code").ToString() + "') "
    '                Dim emailId As String = clsDBFuncationality.getSingleValue(qry)
    '                lstReceiptents.Add(emailId)

    '                Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, dr("User_Code").ToString())

    '                clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strRptPath)
    '            Next
    '        End If
    '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try


    '    'Try
    '    '    Dim client As New System.Net.WebClient()
    '    '    'strMes = "Dear " & strContactperson & " (" & strCustomer & ")" & "   your Bill/Invoice No " & txtDocNo.Value & "  dated  " & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "  has been dispatched with amount  " & lblTotRAmt.Text

    '    '    If clsCommon.myLen(obj.smsbody) <= 0 Then
    '    '        Return
    '    '    End If

    '    '    strMes = obj.smsbody
    '    '    If strMes.Contains(clsEmailSMSConstants.SaleOrderNo) Then
    '    '        strMes = strMes.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
    '    '    End If
    '    '    If strMes.Contains(clsEmailSMSConstants.SaleOrderDate) Then
    '    '        strMes = strMes.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
    '    '    End If
    '    '    If strMes.Contains(clsEmailSMSConstants.VendorNo) Then
    '    '        strMes = strMes.Replace(clsEmailSMSConstants.VendorNo, txtVendorNo.Value)
    '    '    End If
    '    '    If strMes.Contains(clsEmailSMSConstants.VendorName) Then
    '    '        strMes = strMes.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
    '    '    End If

    '    '    If strMes.Contains(clsEmailSMSConstants.TotalAmount) Then
    '    '        strMes = strMes.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
    '    '    End If

    '    '    strphone = clsDBFuncationality.getSingleValue("select Phone1 from TSPL_customer_MASTER where cust_code ='" & txtVendorNo.Value & "' ")

    '    '    Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=vipin&mobileno=91" + strphone + "&message=" + strMes
    '    '    Dim data As Stream = client.OpenRead(baseurl)
    '    '    Dim reader As StreamReader = New StreamReader(data)
    '    '    Dim s As String = reader.ReadToEnd()
    '    '    data.Close()
    '    '    reader.Close()
    '    '    clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
    '    'Catch ex As Exception
    '    '    Throw New Exception(ex.Message)
    '    'End Try
    'End Sub

    Public Sub SetMailRight()
        'Dim obj As clsCheckMailSetting = clsCheckMailSetting.CheckMailRight()
        If objCommonVar.IsMailSend Then
            btnsend.Enabled = True
        Else
            btnsend.Enabled = False
        End If
    End Sub

    Public Function GetMailPrint(ByVal strCode As String)
        atchqry = "  select '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy") + "' as RunDate, TSPL_SHIP_TO_LOCATION.Ship_To_Desc as shipName, TSPL_SHIP_TO_LOCATION.add1 as ship_Add1, TSPL_SHIP_TO_LOCATION.Add2 as ship_add2 ,TSPL_SHIP_TO_LOCATION.Add3 as ship_add3  ,TSPL_SHIP_TO_LOCATION.Pin_Code,TSPL_CITY_MASTER.STATE_CODE  ,City_Name,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesManName,TSPL_SD_SHIPMENT_HEAD.Inv_No, TSPL_SD_SHIPMENT_HEAD.Dept_Desc , TSPL_SD_SHIPMENT_HEAD.Remarks ,  TSPL_SD_SHIPMENT_HEAD.Terms_Code,TSPL_SD_SHIPMENT_HEAD.VehicleNo , "
        atchqry += " TSPL_SD_SHIPMENT_DETAIL .Specification as  specification,   TSPL_SD_SHIPMENT_HEAD.Document_Code as DocNo , TSPL_SD_SHIPMENT_HEAD.Description, "
        atchqry += "  convert(varchar ,TSPL_SD_SHIPMENT_HEAD .Document_Date,103)as Document_Date , TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order, TSPL_SD_SHIPMENT_HEAD.Item_Type ,  TSPL_SD_SHIPMENT_HEAD.Customer_Code, "
        atchqry += " TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Add1 as Customer_Add1,TSPL_CUSTOMER_MASTER.add2 as customer_Add2,TSPL_CUSTOMER_MASTER.Add3 as customer_Add3 ,TSPL_CUSTOMER_MASTER.State as customer_city_State ,TSPL_CUSTOMER_MASTER.PIN_Code as Customer_Pin_Code , TSPL_SD_SHIPMENT_HEAD .Terms_Code as termscode ,TSPL_SD_SHIPMENT_HEAD .Ref_No as ref_no ,"
        atchqry += " TSPL_SD_SHIPMENT_HEAD .Comments as comments ,  TSPL_SD_SHIPMENT_HEAD .Discount_Amt as dis_amt,TSPL_SD_SHIPMENT_DETAIL .Disc_Amt  as dis_amt1,"
        atchqry += " TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount  as aftrdiscount ,TSPL_SD_SHIPMENT_HEAD .Total_Amt as Total_amount,"
        atchqry += " TSPL_SD_SHIPMENT_HEAD.Discount_Base as bfrdisc_amount,  "
        atchqry += " tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SHIPMENT_HEAD.tax1_amt,0) as txt1amt,  "
        atchqry += " tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SHIPMENT_HEAD.tax2_amt,0) as txt2amt,  "
        atchqry += " tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SD_SHIPMENT_HEAD.tax3_amt,0) as txt3amt,  "
        atchqry += " tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SD_SHIPMENT_HEAD.tax4_amt,0) as txt4amt,  "
        atchqry += " tax5.Tax_Code_Desc as tax5name,isnull (TSPL_SD_SHIPMENT_HEAD.tax5_amt,0) as txt5amt,  "
        atchqry += " tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SD_SHIPMENT_HEAD.tax6_amt,0) as txt6amt,  "
        atchqry += " tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SD_SHIPMENT_HEAD.tax7_amt,0) as txt7amt,  "
        atchqry += " tax8.Tax_Code_Desc as tax8name,isnull (TSPL_SD_SHIPMENT_HEAD.tax8_amt,0) as txt8amt,   "
        atchqry += " tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SD_SHIPMENT_HEAD.tax9_amt,0) as txt9amt,  "
        atchqry += " tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SD_SHIPMENT_HEAD.tax10_amt,0) as txt10amt,  "
        atchqry += " isnull(TSPL_SD_SHIPMENT_HEAD .Total_Tax_Amt,0) as total_tax_amt, TSPL_SD_SHIPMENT_HEAD.Total_Amt as DocAmt,  TSPL_COMPANY_MASTER.Comp_Name as compname,ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+ Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as Phone,TSPL_COMPANY_MASTER.Fax ,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,ISNULL(tspl_company_Master.ADD1,'') as address1,"
        atchqry += " TSPL_SD_SHIPMENT_DETAIL.item_code as item_code, TSPL_ITEM_MASTER.Item_Desc   as itemdesc, TSPL_SD_SHIPMENT_DETAIL.Row_Type,TSPL_SD_SHIPMENT_DETAIL.Qty as qty,TSPL_SD_SHIPMENT_DETAIL.unit_code as uom,TSPL_SD_SHIPMENT_DETAIL.item_cost as itemcost,TSPL_SD_SHIPMENT_DETAIL.amount as amount,TSPL_SD_SHIPMENT_HEAD.TAX1,TSPL_SD_SHIPMENT_HEAD.TAX2,TSPL_SD_SHIPMENT_HEAD.TAX3,TSPL_SD_SHIPMENT_HEAD.TAX4,TSPL_SD_SHIPMENT_HEAD.TAX5,TSPL_SD_SHIPMENT_HEAD.Total_Add_Charge from TSPL_SD_SHIPMENT_DETAIL  "
        atchqry += " left outer join TSPL_SD_SHIPMENT_HEAD  on TSPL_SD_SHIPMENT_HEAD.Document_Code  =TSPL_SD_SHIPMENT_DETAIL.Document_Code   "
        atchqry += " left outer join  TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SHIPMENT_HEAD .Ship_To_Location "
        atchqry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_SHIP_TO_LOCATION.City_Code "
        atchqry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_SD_SHIPMENT_HEAD.Salesman_Code "
        atchqry += " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SHIPMENT_HEAD.tax1  "
        atchqry += " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SHIPMENT_HEAD.tax2  "
        atchqry += " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SHIPMENT_HEAD .TAX3  "
        atchqry += " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SHIPMENT_HEAD .tax4  "
        atchqry += " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SHIPMENT_HEAD .tax5  "
        atchqry += " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SHIPMENT_HEAD .TAX6  "
        atchqry += " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SHIPMENT_HEAD .TAX7  "
        atchqry += " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SHIPMENT_HEAD .TAX8  "
        atchqry += " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SHIPMENT_HEAD .TAX9 "
        atchqry += " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SHIPMENT_HEAD .TAX10     "
        atchqry += " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SHIPMENT_HEAD.comp_code  "
        atchqry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SHIPMENT_HEAD.Customer_Code   "
        atchqry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=  TSPL_SD_SHIPMENT_HEAD.Bill_To_Location "
        atchqry += " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code  where 2=2 "
        atchqry += "  and  TSPL_SD_SHIPMENT_HEAD.Document_Code = '" + strCode + "'"

        SetItemWiseTax(clsDBFuncationality.GetDataTable(atchqry), txtDocNo.Value)

        Return atchqry
    End Function

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem5.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.FrmDispatchFreshSale
        frm.ShowDialog()
    End Sub

    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "First Select Shipment Document No.", Me.Text)
            txtDocNo.Focus()
            txtDocNo.Focus()
            Return
        End If

        atchqry = GetMailPrint(txtDocNo.Value)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            System.Diagnostics.Process.Start(frmCRV.EmailAttachment(CrystalReportFolder.NewSalesReports, dt, "crptShipment", "Shipment Detail"))
            frmCRV = Nothing
        End If
    End Sub

    Private Sub btnsend_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsend.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
                txtReqNo.Focus()
                txtReqNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Sale Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtDocNo.Value, NavigatorType.Current)
            Dim lstUsers As New List(Of String)
            lstUsers.Add(txtVendorNo.Value)
            SendEmail(lstUsers, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
#End Region


    Private Sub btnSendForApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendForApproval.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
                txtReqNo.Focus()
                txtReqNo.Select()
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
            SendEmail(lstUsers, True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SendEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
        Try
            'sanjay
            Dim strContactPerson As String = ""
            Dim strotherno As String = Nothing
            strotherno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Phone1 from TSPL_CUSTOMER_MASTER where cust_code='" + txtVendorNo.Value + "'"))

            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + Form_ID + "'", Nothing)
            Dim objEmailH As New clsEMailHead()
            objEmailH.arrEMail = New List(Of String)()
            Dim objSMSH As New clsSMSHead()
            objSMSH.arrMobilNo = New List(Of String)()
            objSMSH.arrMobilNo.Add(clsCommon.myCstr(strotherno))
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                    objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.SaleOrderNo, txtDocNo.Value)
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))

                    objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.SaleOrderNo, txtDocNo.Value)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.CustomerNo, txtVendorNo.Value)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.CustomerName, lblVendorName.Text)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.TotalAmount, clsCommon.myCstr(lblTotRAmt.Text))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Form_Code, MyBase.Form_ID)
                End If


                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then

                    objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))

                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SaleOrderNo, txtDocNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.CustomerNo, txtVendorNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.CustomerName, lblVendorName.Text)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TotalAmount, clsCommon.myCstr(lblTotRAmt.Text))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Form_Code, MyBase.Form_ID)
                End If

            End If

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                Dim strRptPath As String = ""
                'If obj.atchmnt = "Y" Then
                attachQry = GetAttachQry(txtDocNo.Value)
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachQry)
                If dt1.Rows.Count > 0 Then
                    'SetItemWiseTax(dt1, txtDocNo.Value)
                    Dim frmCRV As New frmCrystalReportViewer()
                    'strRptPath = frmCRV.EmailAttachment(CrystalReportFolder.NewSalesReports, dt1, "crptShipment", "Shippment Detail")
                    strRptPath = frmCRV.funsubreportWithdt(True, CrystalReportFolder.KwalitySalesReport, dt1, clsERPFuncationality.CompanyAddresShowinFooter(), "DispatchChallanFresh", "Dispatch", txtDate.Value, "rptCompanyAddress.rpt")
                    frmCRV = Nothing
                    objEmailH.Attachment_1_Path = strRptPath
                End If
                'End If
            End If

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
                    strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
                    emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
                End If

                lstReceiptents.Add(emailId)
                objEmailH.arrEMail.Add(clsCommon.myCstr(emailId))

                If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ContactPerson, strContactPerson)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.UserCode, strUser)
                End If
                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ContactPerson, strContactPerson)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.UserCode, strUser)
                End If
            Next

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                objEmailH.SaveData(MyBase.Form_ID, objEmailH, Nothing)
                objEmailH = Nothing
                clsCommon.MyMessageBoxShow(Me, "E-Mail Send Successfully", Me.Text)
            End If
            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                objSMSH.SaveData(Form_ID, objSMSH, Nothing)
                objSMSH = Nothing
                clsCommon.MyMessageBoxShow(Me, "SMS Send Successfully", Me.Text)
            End If

            'sanjay

            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmSNShipment)

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
            'strbody = strbody.Replace(clsEmailSMSConstants.CustomerNo, txtVendorNo.Value)
            'strbody = strbody.Replace(clsEmailSMSConstants.CustomerName, lblVendorName.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)

            'Dim strRptPath As String = ""
            'If obj.atchmnt = "Y" Then
            '    attachQry = GetAttachQry(txtDocNo.Value)
            '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachQry)
            '    If dt1.Rows.Count > 0 Then
            '        SetItemWiseTax(dt1, txtDocNo.Value)
            '        Dim frmCRV As New frmCrystalReportViewer()
            '        strRptPath = frmCRV.EmailAttachment(CrystalReportFolder.NewSalesReports, dt1, "crptShipment", "Shippment Detail")
            '        frmCRV = Nothing
            '    End If
            'End If

            'Dim strPath As String = String.Empty
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
            '        strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
            '        emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
            '    End If

            '    strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
            '    lstReceiptents.Add(emailId)

            '    Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

            '    clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strPath)
            'Next
            'clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    '----------------------Done By Preeti Gupta 29/05/2014-------BM00000002659----------
    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.Click
        Dim frm As New FrmSaleHistory
        frm.strFormId = MyBase.Form_ID
        frm.strCustId = txtVendorNo.Value
        frm.strCustName = lblVendorName.Text
        Dim strvendor As String = txtVendorNo.Value
        frm.ShowDialog()
        frm.WindowState = FormWindowState.Maximized
    End Sub

    'Public Function ExporttoExcel_transaction(ByVal sql As String, ByVal whrClaus As String, ByVal OrderByClaus As String, ByVal frm As RadForm) As Boolean

    '    Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
    '    Dim wBook As Microsoft.Office.Interop.Excel.Workbook
    '    'Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet
    '    ''************* Filter Block Start
    '    Dim sfd As SaveFileDialog = New SaveFileDialog()
    '    Dim path As String
    '    sfd.FileName = frm.Text
    '    sfd.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx"
    '    If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
    '        path = sfd.FileName
    '    Else
    '        Return False
    '    End If
    '    Dim frmFilter As New frmFilterToExport()
    '    Dim gv As New RadGridView()
    '    wBook = excel.Workbooks.Add()
    '    Dim iQueryIndex As Integer = 0
    '    For Each row_sql As String In Regex.Split(sql, "####")
    '        Try
    '            iQueryIndex += 1
    '            Dim filename As String = row_sql.Substring(row_sql.IndexOf("From ") + 5, row_sql.Length - row_sql.IndexOf("From ") - 5)
    '            frmFilter.qry = row_sql
    '            frmFilter.whrCls = " Where 2=2 " + whrClaus
    '            If clsCommon.myLen(OrderByClaus) > 0 Then
    '                frmFilter.orderByClause = " Order by " + OrderByClaus
    '            End If
    '            frmFilter.ShowDialog()
    '            If frmFilter.isCancel Then
    '                Return False
    '            End If
    '            row_sql = frmFilter.qry
    '            ''************* Filter Block End


    '            If Not path.Equals(String.Empty) Then
    '                Try
    '                    Dim exporter As New RadGridViewExcelExporter()
    '                    gv.Name = "gTax"
    '                    frm.Controls.Add(gv)
    '                    FillGridView(row_sql, gv)
    '                    If gv.Rows.Count = 0 Then
    '                        common.clsCommon.MyMessageBoxShow("There is no data to transfer.")
    '                        Return False
    '                    End If

    '                    Dim i As Integer = 0
    '                    For i = 0 To gv.ColumnCount - 1
    '                        Dim grow As GridViewRowInfo = TryCast(gv.Rows(0), GridViewRowInfo)
    '                        If TypeOf grow.Cells(i).Value Is DateTime Then
    '                            Dim datecol As GridViewDateTimeColumn = TryCast(gv.Columns(i), GridViewDateTimeColumn)
    '                            datecol.ExcelExportType = DisplayFormatType.ShortDate
    '                        End If
    '                    Next i

    '                    'AddHandler exporter.Progress, AddressOf PB.exporter_Progress
    '                    'ShowPb(gv.Rows.Count)

    '                    'exporter.Export(gv, path, frm.Text)
    '                    'HidePb()
    '                    clsCommon.ProgressBarShow()


    '                    '=================Export================
    '                    exportdata_transaction(gv, path, filename, wBook, excel, iQueryIndex) 'path.Substring(path.LastIndexOf("\") + 1, path.Length - path.LastIndexOf("\") - 1), wBook) 'frm.Text)
    '                    '===============================================================
    '                    'MessageBox.Show(wBook.Worksheets.Count)
    '                Catch ex As Exception
    '                    frm.Controls.Remove(gv)
    '                    clsCommon.ProgressBarHide()
    '                    'HidePb()
    '                    Throw New Exception(ex.Message)
    '                    Return False
    '                End Try
    '            End If
    '        Catch ex As Exception
    '            common.clsCommon.MyMessageBoxShow("No data transfered." + Environment.NewLine + ex.Message, "Export Error", MessageBoxButtons.OK)
    '        End Try
    '    Next
    '    Dim strFileName As String = path
    '    Dim blnFileOpen As Boolean = False
    '    Try
    '        Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
    '        fileTemp.Close()

    '    Catch ex As Exception
    '        blnFileOpen = False
    '    End Try

    '    If System.IO.File.Exists(strFileName) Then
    '        System.IO.File.Delete(strFileName)

    '    End If


    '    wBook.SaveAs(strFileName)
    '    Dim dp As Object = wBook.BuiltinDocumentProperties
    '    dp("Keywords").Value = clsUserMgtCode.frmComplaintMaster
    '    excel.DisplayAlerts = False
    '    wBook.Close(True)
    '    'Dim exporter As New ExportToExcelML(gv)

    '    'AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
    '    'exporter.ExportHierarchy = True
    '    '' exporter.ExportVisualSettings = True
    '    'exporter.SheetMaxRows = ExcelMaxRows._65536
    '    'exporter.SheetName = frm.Text
    '    'exporter.RunExport(path)

    '    frm.Controls.Remove(gv)
    '    clsCommon.ProgressBarHide()
    '    common.clsCommon.MyMessageBoxShow("Data transfer Completed!", "Export", MessageBoxButtons.OK)
    '    'Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
    '    excel.Workbooks.Open(path)
    '    excel.Visible = True

    '    Return True
    'End Function

    'Public Function exportdata_transaction(ByVal gv As RadGridView, ByVal flname As String, ByVal sname As String, ByVal wBook As Microsoft.Office.Interop.Excel.Workbook, ByVal excel As Microsoft.Office.Interop.Excel.ApplicationClass, ByVal Queryindex As Integer)
    '    If ((gv.Columns.Count = 0) Or (gv.Rows.Count = 0)) Then
    '        Exit Function
    '    End If

    '    'Creating dataset to export
    '    Dim dset As New DataSet
    '    'add table to dataset
    '    dset.Tables.Add()
    '    'add column to that table
    '    For i As Integer = 0 To gv.ColumnCount - 1
    '        dset.Tables(0).Columns.Add(gv.Columns(i).HeaderText)
    '    Next
    '    'add rows to the table
    '    Dim dr1 As DataRow
    '    For i As Integer = 0 To gv.RowCount - 1
    '        dr1 = dset.Tables(0).NewRow
    '        For j As Integer = 0 To gv.Columns.Count - 1
    '            dr1(j) = gv.Rows(i).Cells(j).Value.ToString
    '        Next
    '        dset.Tables(0).Rows.Add(dr1)
    '    Next

    '    'Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
    '    'Dim wBook As Microsoft.Office.Interop.Excel.Workbook
    '    Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet

    '    ' wBook = excel.Workbooks.Add()

    '    wSheet = wBook.Sheets("Sheet" & Queryindex & "") 'wBook.ActiveSheet()
    '    ' wSheet = wBook.ActiveSheet
    '    If clsCommon.myLen(sname) > 31 Then
    '        sname = sname.Substring(0, 31)
    '    End If
    '    'Dim dp As Object
    '    ' dp = wBook.BuiltinDocumentProperties
    '    'dp("Tags").Value = clsUserMgtCode.frmComplaintMaster
    '    wSheet.Name = sname
    '    wSheet.Cells.NumberFormat = "@"
    '    Try
    '        '  CType(wBook.Sheets("Sheet2"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
    '        ' CType(wBook.Sheets("Sheet3"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
    '    Catch ex As Exception

    '    End Try
    '    wSheet.Activate()
    '    Dim dt As System.Data.DataTable = dset.Tables(0)
    '    Dim dc As System.Data.DataColumn
    '    Dim dr As System.Data.DataRow
    '    Dim colIndex As Integer = 0
    '    Dim rowIndex As Integer = 0

    '    For Each dc In dt.Columns
    '        colIndex = colIndex + 1
    '        wSheet.Cells(1, colIndex) = dc.ColumnName 'excel.
    '    Next

    '    For Each dr In dt.Rows
    '        rowIndex = rowIndex + 1
    '        colIndex = 0
    '        For Each dc In dt.Columns
    '            colIndex = colIndex + 1
    '            wSheet.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName).ToString 'excel.

    '        Next
    '    Next

    '    wSheet.Columns.AutoFit()
    '    'Dim strFileName As String = flname
    '    'Dim blnFileOpen As Boolean = False
    '    'Try
    '    '    Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
    '    '    fileTemp.Close()

    '    'Catch ex As Exception
    '    '    blnFileOpen = False
    '    'End Try

    '    'If System.IO.File.Exists(strFileName) Then
    '    '    System.IO.File.Delete(strFileName)

    '    'End If


    '    'wBook.SaveAs(strFileName)
    '    'Dim dp As Object = wBook.BuiltinDocumentProperties
    '    'dp("Keywords").Value = clsUserMgtCode.frmComplaintMaster
    '    'excel.DisplayAlerts = False
    '    'wBook.Close(True)




    'End Function

    Private Sub Export_Head_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export_Head.Click
        Dim sQuery As String = "select Document_Code as [Document Code],RANK() over(order by Document_Code desc) as [S No],Document_Date AS [Document Date],Customer_Code as [Customer Code],Description,Remarks,Bill_To_Location as [Bill To Location]," _
        & " Ship_To_Location as [Ship To Location],Tax_Group as [Tax Group],PROJECT_ID as [Project Id],Carrier,VehicleNo,Vehicle_Code" _
        & " as [Vehicle Code],GRNo,GENo,GEDate,Dept,Salesman_Code as [Salesman Code],Salesman_Name as [Salesman Name],Price_code as [Price Code]," _
        & " Route_No as [Route No],Add_Charge_Code1,Add_Charge_Amt1,Add_Charge_Code2,Add_Charge_Amt2,Add_Charge_Code3,Add_Charge_Amt3" _
        & " ,Add_Charge_Code4,Add_Charge_Amt4,Add_Charge_Code5,Add_Charge_Amt5,Add_Charge_Code6,Add_Charge_Amt6,Add_Charge_Code7,Add_Charge_Amt7" _
        & " ,Add_Charge_Code8,Add_Charge_Amt8,Add_Charge_Code9,Add_Charge_Amt9,Add_Charge_Code10,Add_Charge_Amt10 from TSPL_SD_SHIPMENT_HEAD"
        transportSql.ExporttoExcel(sQuery, Me)
    End Sub

    Private Sub Export_details_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export_details.Click
        Dim sQuery As String = "select DOCUMENT_CODE as [Document Code],RANK() over(order by Document_Code desc) as [S No],Item_Code as [Item Code],Price_Date as [Price Date]," _
        & " Unit_code as [UOM],Qty as [Quantity],MRP,Item_Cost  as [Item Cost],Location,Remarks,Specification from tspl_SD_Shipment_detail"
        transportSql.ExporttoExcel(sQuery, Me)
    End Sub

    Function LoadImportData(ByVal gv As RadGridView, ByVal rowindex As Integer, ByVal DtExcel As DataTable)
        Dim iswithouterror As Boolean = False
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = True
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()
            LoadBlankGridTax()
            LoadBlankGridAC()
            cboItemType.Enabled = False
            txtBillToLocation.Enabled = False
            Dim obj As New frmShipmentImportExport()
            obj.SaveData(gv, rowindex, DtExcel)
            If (obj IsNot Nothing) Then 'AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    repoComplete.IsVisible = True
                    repoBalQty.IsVisible = True
                End If
                chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(obj.Customer_Code)
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_Code
                txtpodate.Text = obj.Podate
                txtDate.Value = obj.Document_Date
                txtVendorNo.Value = obj.Customer_Code
                txtPONo.Text = obj.Cust_PO_No
                txtForm38.Text = obj.Form_38_No
                txtDate.Enabled = False
                txtVendorNo.Enabled = False
                chkRateUserCustomer.ToggleState = ClsUserCustomerSettings.GetUserCustomerRateSetting(txtVendorNo.Value)

                lblVendorName.Text = obj.Customer_Name
                txtRefNo.Text = obj.Ref_No
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
                chkOnHold.Checked = obj.On_Hold
                txtDesc.Text = obj.Description
                txtTaxGroup.Value = obj.Tax_Group

                txtComment.Text = obj.Comments
                txtShipToLocation.Value = obj.Ship_To_Location
                txtBillToLocation.Value = obj.Bill_To_Location
                txtInvNo.Text = obj.Inv_No
                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForSale(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If
                chkInternal.Checked = obj.Is_Internal
                cboItemType.SelectedValue = obj.Item_Type
                txtDept.Value = obj.Dept
                lblDept.Text = obj.Dept_Desc

                txtTermCode.Value = obj.Terms_Code

                'lblTermName.Text = obj.Terms_Description

                If obj.Due_Date IsNot Nothing Then
                    txtDueDate.Value = obj.Due_Date
                End If


                lblAmtWithDiscount.Text = clsCommon.myFormat(obj.Discount_Base)
                lblDiscountAmt.Text = clsCommon.myFormat(obj.Discount_Amt)
                lblAmtAfterDiscount.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                lblTotRAmt.Text = clsCommon.myFormat(obj.Total_Amt)
                lblTotRAmt1.Text = lblTotRAmt.Text
                lblBillToLocation.Text = obj.BillToLocationName
                lblShipToLocation.Text = obj.ShipToLocationName
                lblTaxGrpName.Text = obj.TaxGroupName
                lblTermName.Text = obj.TermsName

                txtCarrier.Text = obj.Carrier
                lblVehicleNo.Text = obj.VehicleNo
                txtVehicleCode.Value = obj.Vehicle_Code
                txtGRNo.Text = obj.GRNo
                txtGENo.Text = obj.GENo
                If obj.GEDate.HasValue Then
                    txtGEDate.Value = obj.GEDate
                    txtGEDate.Checked = True
                End If
                txtInvoiceNo.Text = obj.Invoice_No
                txtSalesman.Value = obj.Salesman_Code
                lblSalesman.Text = obj.Salesman_Name

                txtReqNo.Value = obj.Against_Sales_Order
                fndProject.Value = obj.PROJECT_ID
                lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
                If clsCommon.myLen(txtReqNo.Value) > 0 Then
                    lblProject.Enabled = False
                    fndProject.Enabled = False
                End If
                txtRouteNo.Value = obj.Route_No
                lblRouteDesc.Text = obj.Route_Desc
                txtPriceCode.Text = obj.Price_Code
                txtPriceGroupCode.Text = obj.Price_Group_Code
                txtDiscPer.Text = obj.HeadDisc_Per
                txtDiscAmt.Text = obj.HeadDisc_Amt
                ddlInvoiceType.SelectedValue = obj.Invoice_Type
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



                If (clsCommon.myLen(obj.Add_Charge_Code1) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt1
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



                chkCreateAutoInvoice.Checked = obj.Is_Create_Auto_Invoice
                'chkCreateAutoReceipt.Visible = chkCreateAutoInvoice.Checked
                chkCreateAutoReceipt.Checked = obj.Is_Create_Auto_Receipt

                If obj.arrList IsNot Nothing AndAlso obj.arrList.Count > 0 Then
                    For Each objTr As frmShipmentImportExport In obj.arrList
                        gv1.Rows.AddNew()
                        ' gv1.Rows(gv1.Rows.Count - 1).Tag = objTr.arrSrItem

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Row_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComplete).Value = IIf(objTr.Status = 0, "No", "Yes")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colBarCode).Value = objTr.Bar_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.Item_Code)
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSOQty).Value = objTr.so_Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(objTr.Unit_code), 0)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(objTr.Unit_code), 0)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFreeQty).Value = objTr.Free_Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = objTr.Order_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Location
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.LocationName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = objTr.Disc_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmt).Value = objTr.Disc_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterDis).Value = objTr.Amt_Less_Discount
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

                        If objTr.Expiry_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiry).Value = objTr.Expiry_Date
                        End If

                        If objTr.MFG_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colManufactureDate).Value = objTr.MFG_Date
                        End If


                        If clsCommon.myLen(objTr.Order_Code) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsSNSalesOrderDetail.GetBalancePOQty(objTr.Order_Code, objTr.Item_Code, obj.Document_Code, objTr.Unit_code, objTr.MRP, objTr.Assessable)
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsMannualAmt).Value = objTr.Is_Mannual_Amt

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = IIf(objTr.Scheme_Applicable = "Y", "Yes", "No")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFromSchemeCode).Value = objTr.Scheme_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeItem).Value = IIf(objTr.Scheme_Item = "Y", "Yes", "No")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = objTr.Item_Tax
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalMRP).Value = objTr.Total_MRP_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalBasicAmount).Value = objTr.Total_Basic_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalDiscountAmount).Value = objTr.Total_Disc_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colcustDiscount).Value = objTr.Cust_Discount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalCustDiscount).Value = objTr.Total_Cust_Discount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colActualCost).Value = objTr.ActualRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCustDiscountQty).Value = objTr.Cust_DiscountQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = objTr.Price_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = objTr.Price_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementPer).Value = objTr.Abatement_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementAmount).Value = objTr.Abatement_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = objTr.FOC_Item
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value), 0)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = objTr.Batch_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemWeight).Value = objTr.Item_Weight
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colConvF).Value = objTr.Conv_Factor
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotItemWt).Value = objTr.TotalItem_Weight
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMarkupOn).Value = objTr.Markup_On
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMarkUpPercentage).Value = objTr.Markup_Percent
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLandingCost).Value = objTr.Landing_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustDiscPercentage).Value = objTr.CustDiscPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeadDiscamt).Value = objTr.HeadDiscAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCashDiscSchemeCode).Value = objTr.CasdDiscScheme_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPurCost).Value = objTr.Purchase_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgCost).Value = objTr.OrgRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPricipleCode).Value = objTr.PrincipleCode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPricipleDesc).Value = objTr.PrincipleDesc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colvendorCode).Value = objTr.vendor_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colvendorDesc).Value = objTr.vendor_desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = "Yes"
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeItem).Value = "Yes"


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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                        gvAC.Rows.AddNew()
                    End If

                    ''For Custom Fields
                    If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                        UcCustomFields1.LoadData(obj.Document_Code)
                    End If
                    clsCustomFieldGrid.FillDataInGrid(obj.Document_Code, MyBase.Form_ID, gv1)
                    ''End of For Custom Fields

                    '' MULTICURRENCY
                    Me.txtCurrencyCode.Value = obj.CURRENCY_CODE
                    Me.txtConversionRate.Text = obj.ConvRate
                    Me.txtApplicableFrom.Text = obj.ApplicableFrom.ToString
                    '' end  MULTICURRENCY
                End If
                SetitemWiseTaxOnlySetting()
                For Each row As GridViewRowInfo In gv1.Rows
                    UpdateCurrentRow(row.Index, False)
                Next
                UpdateAllTotals()
                Dim ee As New System.EventArgs
                If AllowToSave() = False Then
                    iswithouterror = False
                    GoTo a
                End If
                btnSave_Click("", ee)
                DocumentNo = txtDocNo.Value
                ' ''RefreshReqNo()
                ' ''RefreshGRPNo()
                '  UcAttachment1.LoadData(obj.Document_Code)
                iswithouterror = True
a:          End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            iswithouterror = False
        Finally
            isInsideLoadData = False
        End Try
        Return iswithouterror
    End Function

    Private Sub txtForm38_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtForm38.TextChanged

    End Sub

    Private Sub UsLock1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsLock1.Load

    End Sub

    Private Sub gv1_RowsChanged(sender As Object, e As GridViewCollectionChangedEventArgs) Handles gv1.RowsChanged

    End Sub
    'select distinct TSPL_SCHEME_BENEFICIARY.Cust_Code,Customer_Name from TSPL_SCHEME_BENEFICIARY left outer join tspl_customer_master on TSPL_SCHEME_BENEFICIARY.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code where Scheme_Code in (select Scheme_Code from (select distinct Scheme_Code from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where Trans_Type='FS' and TSPL_SD_SHIPMENT_HEAD.Document_Code='FDI-003/15-16/000300'  and Scheme_Code <> ''
    'union all
    'select distinct Cash_Scheme_Code from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where Trans_Type='FS' and TSPL_SD_SHIPMENT_HEAD.Document_Code='FDI-003/15-16/000300'  and Cash_Scheme_Code <> '' ) aa)

    Private Sub btnShowInvoiceJE_Click(sender As Object, e As EventArgs) Handles btnShowInvoiceJE.Click
        'Dim strCode As String = txtInvoiceNo.Text
        'strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_Customer_Invoice_Head where Against_Sale_No ='" + txtInvoiceNo.Text + "'"))
        'If clsCommon.myLen(strCode) <= 0 Then
        '    clsCommon.MyMessageBoxShow("No Invoice No Found on Current Screen")
        '    Exit Sub
        'End If
        'Dim qry As String = " select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & strCode & "' "
        'strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        'If clsCommon.myLen(strCode) <= 0 Then
        '    clsCommon.MyMessageBoxShow("No Journal Entry Found For Current Document")
        '    Exit Sub
        'Else
        '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strCode)
        'End If
        clsOpenJEAgainstInvoice.ShowInvoiceJE(txtInvoiceNo.Text)
    End Sub
    ' Ticket No : TEC/29/10/18-000349 By Prabhakar 
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Value)
    End Sub
End Class
